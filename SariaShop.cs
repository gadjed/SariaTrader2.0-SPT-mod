using System.Reflection;
using SariaShop.Generators;
using SariaShop.Helpers;
using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.DI;
using SPTarkov.Server.Core.Helpers;
using SPTarkov.Server.Core.Models.Eft.Common.Tables;
using SPTarkov.Server.Core.Models.Logging;
using SPTarkov.Server.Core.Models.Spt.Config;
using SPTarkov.Server.Core.Models.Spt.Mod;
using SPTarkov.Server.Core.Models.Utils;
using SPTarkov.Server.Core.Routers;
using SPTarkov.Server.Core.Servers;
using SPTarkov.Server.Core.Services;
using Path = System.IO.Path;

namespace SariaShop;

public record ModMetadata : AbstractModMetadata
{
    public override string ModGuid { get; init; } = "nameless.sariashop";
    public override string Name { get; init; } = "Saria Trader 2.0";
    public override string Author { get; init; } = "gadjed";
    public override List<string>? Contributors { get; init; } = ["nameless"];
    public override SemanticVersioning.Version Version { get; init; } = new("2.0.1");
    public override SemanticVersioning.Range SptVersion { get; init; } = new("~4.0.0");
    public override List<string>? Incompatibilities { get; init; }
    public override Dictionary<string, SemanticVersioning.Range>? ModDependencies { get; init; }
    public override string? Url { get; init; } = "https://github.com/gadjed/SariaTrader2.0-SPT-mod";
    public override bool? IsBundleMod { get; init; } = false;
    public override string? License { get; init; } = "MIT";
}

[Injectable(TypePriority = OnLoadOrder.PostDBModLoader + 1)]
public class Saria(
    ISptLogger<Saria> logger,
    ModHelper modHelper,
    ImageRouter imageRouter,
    ConfigServer configServer,
    DatabaseService databaseService,
    SariaTraderHelper addCustomTraderHelper,
    SariaAssortGenerator sariaGenerator
) : IOnLoad
{
    private readonly TraderConfig _traderConfig = configServer.GetConfig<TraderConfig>();
    private readonly RagfairConfig _ragfairConfig = configServer.GetConfig<RagfairConfig>();

    public ModConfig? config;

    public Task OnLoad()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var pathToMod = modHelper.GetAbsolutePathToModFolder(assembly);
        var traderImagePath = Path.Combine(pathToMod, "Assets/Saria.jpg");
        var traderBase = modHelper.GetJsonDataFromFile<TraderBase>(pathToMod, "Assets/base.json");

        imageRouter.AddRoute(traderBase.Avatar.Replace(".jpg", ""), traderImagePath);
        addCustomTraderHelper.SetTraderUpdateTime(_traderConfig, traderBase, 1800, 7200);
        _ragfairConfig.Traders.TryAdd(traderBase.Id, true);
        addCustomTraderHelper.AddTraderWithEmptyAssortToDb(traderBase);
        addCustomTraderHelper.AddTraderToLocales(
            traderBase,
            "Saria",
            "A soldier with questionable motives, an unknown background, and a large supply of military goods. She's willing to trade, for a price of course."
        );

        config = modHelper.GetJsonDataFromFile<ModConfig>(pathToMod, "config.json");
        sariaGenerator.PassConfig(config);
        sariaGenerator.CreateSariaAssort();

        ApplyLoyaltyLevelChanges(traderBase.Id);

        logger.LogWithColor("[Saria] Mission accomplished, returning to base.", LogTextColor.Cyan);

        return Task.CompletedTask;
    }

    private void ApplyLoyaltyLevelChanges(string traderId)
    {
        if (config == null)
        {
            return;
        }

        var trader = databaseService.GetTrader(traderId);
        var traderLoyaltyLevels = trader?.Base.LoyaltyLevels;

        if (traderLoyaltyLevels == null)
        {
            return;
        }

        if (config.RemoveLevelLlRequirements)
        {
            foreach (var level in traderLoyaltyLevels)
            {
                level.MinLevel = 1;
            }
        }

        if (config.RemoveMoneyLlRequirements)
        {
            foreach (var level in traderLoyaltyLevels)
            {
                level.MinSalesSum = 0;
            }
        }
    }
}

public class ModConfig
{
    public bool RandomizeStockCount { get; set; }
    public bool RemoveMoneyLlRequirements { get; set; }
    public bool RemoveLevelLlRequirements { get; set; }
}
