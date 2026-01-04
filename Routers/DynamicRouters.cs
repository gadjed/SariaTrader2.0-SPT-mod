using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.Callbacks;
using SPTarkov.Server.Core.DI;
using SPTarkov.Server.Core.Generators;
using SPTarkov.Server.Core.Helpers;
using SPTarkov.Server.Core.Models.Eft.Common;
using SPTarkov.Server.Core.Models.Eft.Common.Tables;
using SPTarkov.Server.Core.Models.Utils;
using SPTarkov.Server.Core.Services;
using SPTarkov.Server.Core.Utils;

namespace SariaShop;

[Injectable(TypePriority = OnLoadOrder.PostDBModLoader + 2)]
public class CustomDynamicRouter : DynamicRouter
{
    private static TraderCallbacks _traderCallbacks;
    private static ISptLogger<CustomDynamicRouter> _logger;
    private static DatabaseService _databaseService;
    private static RagfairOfferGenerator _ragfairOfferGenerator;
    private static RandomUtil _randomUtil;
    private static ModConfig _modConfig;
    private static ItemHelper _itemHelper;

    public CustomDynamicRouter(
        JsonUtil jsonUtil,
        TraderCallbacks traderCallbacks,
        ISptLogger<CustomDynamicRouter> logger,
        DatabaseService databaseService,
        RagfairOfferGenerator ragfairOfferGenerator,
        RandomUtil randomUtil,
        ItemHelper itemHelper
    )
        : base(jsonUtil, GetCustomRoutes())
    {
        _traderCallbacks = traderCallbacks;
        _logger = logger;
        _databaseService = databaseService;
        _ragfairOfferGenerator = ragfairOfferGenerator;
        _randomUtil = randomUtil;
        _itemHelper = itemHelper;
    }

    public void PassConfig(ModConfig config)
    {
        _modConfig = config;
    }

    private static List<RouteAction> GetCustomRoutes()
    {
        return
        [
            new RouteAction(
                "/client/trading/api/getTraderAssort/66f4db5ca4958508883d700c",
                async (url, info, sessionId, output) =>
                {
                    var trader = _databaseService.GetTrader("66f4db5ca4958508883d700c");
                    var traderAssortItems = trader.Assort.Items;
                    var traderLoyaltyLevels = trader.Base.LoyaltyLevels;

                    if (_modConfig.RemoveLevelLlRequirements)
                        RemoveLevelReqs(traderLoyaltyLevels);
                    if (_modConfig.RemoveMoneyLlRequirements)
                        RemoveMoneyReqs(traderLoyaltyLevels);
                    if (_modConfig.RandomizeStockCount)
                        RandomizeStock(traderAssortItems);

                    _ragfairOfferGenerator.GenerateFleaOffersForTrader("66f4db5ca4958508883d700c");

                    return await _traderCallbacks.GetAssort(url, info as EmptyRequestData, sessionId);
                }
            ),
        ];
    }

    private static void RemoveLevelReqs(List<TraderLoyaltyLevel> traderLoyaltyLevels)
    {
        foreach (var level in traderLoyaltyLevels)
        {
            level.MinLevel = 1;
        }
    }

    private static void RemoveMoneyReqs(List<TraderLoyaltyLevel> traderLoyaltyLevels)
    {
        foreach (var level in traderLoyaltyLevels)
        {
            level.MinSalesSum = 0;
        }
    }

    private static void RandomizeStock(List<Item> assortItems)
    {
        foreach (var item in assortItems)
        {
            if (item.ParentId != "hideout")
                return;

            item.Upd.UnlimitedCount = false;
            item.Upd.StackObjectsCount = 1;

            var isOutOfStock = _randomUtil.GetChance100(25);
            if (isOutOfStock)
            {
                item.Upd.StackObjectsCount = 0;
            }
            if (_itemHelper.IsOfBaseclass(item.Template, BaseClasses.AMMO))
            {
                var newStockCount = _randomUtil.RandInt(1, 300);
                item.Upd.StackObjectsCount = newStockCount;
            }
            else
            {
                var newStockCount = _randomUtil.RandInt(1, 10);
                item.Upd.StackObjectsCount = newStockCount;
            }
        }
    }
}
