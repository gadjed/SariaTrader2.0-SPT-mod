using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.DI;
using SPTarkov.Server.Core.Models.Common;
using SPTarkov.Server.Core.Models.Eft.Common.Tables;
using SPTarkov.Server.Core.Models.Utils;
using SPTarkov.Server.Core.Services;

namespace SariaShop.Helpers;

[Injectable(TypePriority = OnLoadOrder.PostDBModLoader + 1)]
public class SariaFluentTraderAssortHelper(DatabaseService databaseService, ISptLogger<SariaFluentTraderAssortHelper> logger)
{
    private readonly List<Item> _itemsToSell = [];
    private readonly Dictionary<string, List<List<BarterScheme>>> _barterScheme = new();
    private readonly Dictionary<string, int> _loyaltyLevel = new();

    public SariaFluentTraderAssortHelper CreateSingleAssortItem(MongoId itemTpl, MongoId? itemId = null)
    {
        var newItemToAdd = new Item
        {
            Id = itemId ?? new MongoId(),
            Template = itemTpl,
            ParentId = "hideout",
            SlotId = "hideout",
            Upd = new Upd { UnlimitedCount = false, StackObjectsCount = 100 },
        };

        _itemsToSell.Add(newItemToAdd);

        return this;
    }

    public SariaFluentTraderAssortHelper CreateComplexAssortItem(List<Item> items)
    {
        items[0].ParentId = "hideout";
        items[0].SlotId = "hideout";

        items[0].Upd ??= new Upd();

        items[0].Upd.UnlimitedCount = false;
        items[0].Upd.StackObjectsCount = 100;

        _itemsToSell.AddRange(items);

        return this;
    }

    public SariaFluentTraderAssortHelper AddStackCount(int stackCount)
    {
        _itemsToSell[0].Upd.StackObjectsCount = stackCount;

        return this;
    }

    public SariaFluentTraderAssortHelper AddUnlimitedStackCount()
    {
        _itemsToSell[0].Upd.StackObjectsCount = 999999;
        _itemsToSell[0].Upd.UnlimitedCount = true;

        return this;
    }

    public SariaFluentTraderAssortHelper MakeStackCountUnlimited()
    {
        _itemsToSell[0].Upd.StackObjectsCount = 999999;

        return this;
    }

    public SariaFluentTraderAssortHelper AddBuyRestriction(int maxBuyLimit)
    {
        _itemsToSell[0].Upd.BuyRestrictionMax = maxBuyLimit;
        _itemsToSell[0].Upd.BuyRestrictionCurrent = 0;

        return this;
    }

    public SariaFluentTraderAssortHelper AddLoyaltyLevel(int level)
    {
        _loyaltyLevel[_itemsToSell[0].Id] = level;

        return this;
    }

    public SariaFluentTraderAssortHelper AddMoneyCost(string currencyType, int amount)
    {
        var dataToAdd = new BarterScheme { Count = amount, Template = currencyType };

        if (
            !_barterScheme.TryAdd(
                _itemsToSell[0].Id,
                [
                    [dataToAdd],
                ]
            )
        )
        {
            logger.Warning($"Unable to add barter scheme currency: {currencyType}");
        }

        return this;
    }

    public SariaFluentTraderAssortHelper AddBarterCost(MongoId itemTpl, int count)
    {
        var sellableItemId = _itemsToSell[0].Id;

        if (_barterScheme.Count == 0)
        {
            var dataToAdd = new BarterScheme { Count = count, Template = itemTpl };

            _barterScheme[sellableItemId] =
            [
                [dataToAdd],
            ];
        }
        else
        {
            var existingData = _barterScheme[sellableItemId][0].FirstOrDefault(x => x.Template == itemTpl);
            if (existingData is not null)
            {
                existingData.Count += count;
            }
            else
            {
                _barterScheme[sellableItemId][0].Add(new BarterScheme { Count = count, Template = itemTpl });
            }
        }

        return this;
    }

    public SariaFluentTraderAssortHelper? Export(string traderId)
    {
        var traderData = databaseService.GetTables().Traders.GetValueOrDefault(traderId);

        var rootItemAddedId = _itemsToSell.FirstOrDefault().Id;
        if (traderData.Assort.Items.Exists(x => x.Id == rootItemAddedId))
        {
            logger.Error($"Unable to add complex item with item key: {_itemsToSell[0].Id}, key already in use");

            _itemsToSell.Clear();
            _barterScheme.Clear();
            _loyaltyLevel.Clear();

            return null;
        }

        traderData.Assort.Items.AddRange(_itemsToSell);
        traderData.Assort.BarterScheme[rootItemAddedId] = _barterScheme[rootItemAddedId];
        traderData.Assort.LoyalLevelItems[rootItemAddedId] = _loyaltyLevel[rootItemAddedId];

        _itemsToSell.Clear();
        _barterScheme.Clear();
        _loyaltyLevel.Clear();

        return this;
    }

    public void CreateSingleItemOffer(
        string ItemToAdd,
        int stackCount,
        int loyaltyLevelToPush,
        int reqCost,
        MongoId currencyToUse,
        MongoId traderToUse
    )
    {
        CreateSingleAssortItem(ItemToAdd)
            .AddBarterCost(currencyToUse, reqCost)
            .AddStackCount(stackCount)
            .AddLoyaltyLevel(loyaltyLevelToPush)
            .Export(traderToUse);
    }
}
