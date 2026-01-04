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
using Path = System.IO.Path;

namespace SariaShop;

public record ModMetadata : AbstractModMetadata
{
    public override string ModGuid { get; init; } = "nameless.sariashop";
    public override string Name { get; init; } = "Saria Shop";
    public override string Author { get; init; } = "nameless";
    public override List<string>? Contributors { get; init; }
    public override SemanticVersioning.Version Version { get; init; } = new("1.8.0");
    public override SemanticVersioning.Range SptVersion { get; init; } = new("~4.0.0");
    public override List<string>? Incompatibilities { get; init; }
    public override Dictionary<string, SemanticVersioning.Range>? ModDependencies { get; init; }
    public override string? Url { get; init; }
    public override bool? IsBundleMod { get; init; } = false;
    public override string? License { get; init; } = "MIT";
}

[Injectable(TypePriority = OnLoadOrder.PostDBModLoader + 1)]
public class Saria(
    ISptLogger<Saria> logger,
    ModHelper modHelper,
    ImageRouter imageRouter,
    ConfigServer configServer,
    SariaTraderHelper addCustomTraderHelper,
    SariaAssortGenerator sariaGenerator,
    CustomDynamicRouter dynamicRouter
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
        sariaGenerator.CreateSariaAssort();

        config = modHelper.GetJsonDataFromFile<ModConfig>(pathToMod, "config.json");
        dynamicRouter.PassConfig(config);

        logger.LogWithColor("[Saria] Mission accomplished, returning to base.", LogTextColor.Cyan);

        return Task.CompletedTask;
    }
}

public class ModConfig
{
    public bool RandomizeStockCount { get; set; }
    public bool RemoveMoneyLlRequirements { get; set; }
    public bool RemoveLevelLlRequirements { get; set; }
}
