using System.Reflection;
using System.Text.Json.Serialization;
using SariaShop.Helpers;
using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.DI;
using SPTarkov.Server.Core.Helpers;
using SPTarkov.Server.Core.Models.Eft.Common.Tables;
using SPTarkov.Server.Core.Models.Enums;
using SPTarkov.Server.Core.Models.Utils;
using SPTarkov.Server.Core.Services;
using SPTarkov.Server.Core.Utils;
using Path = System.IO.Path;

namespace SariaShop.Generators;

public class PresetData
{
    [JsonPropertyName("_id")]
    public string Id { get; set; }

    [JsonPropertyName("_type")]
    public string Type { get; set; }

    [JsonPropertyName("_changeWeaponName")]
    public bool ChangeWeaponName { get; set; }

    [JsonPropertyName("_name")]
    public string Name { get; set; }

    [JsonPropertyName("_parent")]
    public string Parent { get; set; }

    [JsonPropertyName("_items")]
    public List<ItemData> Items { get; set; }

    [JsonPropertyName("_encyclopedia")]
    public string Encyclopedia { get; set; }
}

public class ItemData
{
    [JsonPropertyName("_id")]
    public string Id { get; set; }

    [JsonPropertyName("_tpl")]
    public string Tpl { get; set; }

    [JsonPropertyName("parentId")]
    public string ParentId { get; set; }

    [JsonPropertyName("slotId")]
    public string SlotId { get; set; }

    [JsonPropertyName("upd")]
    public object Upd { get; set; }
}

[Injectable(TypePriority = OnLoadOrder.PostDBModLoader + 1)]
public class SariaAssortGenerator(
    SariaFluentTraderAssortHelper assortUtils,
    ModHelper modHelper,
    DatabaseService databaseService,
    RandomUtil randomUtil,
    ItemHelper itemHelper,
    ISptLogger<SariaAssortGenerator> logger
)
{
    private Dictionary<string, PresetData>? _presetMap;
    private static ModConfig _modConfig;

    public void PassConfig(ModConfig config)
    {
        _modConfig = config;
    }

    private void LoadPresetMap()
    {
        if (_presetMap != null)
        {
            return;
        }

        var assembly = Assembly.GetExecutingAssembly();
        var pathToMod = modHelper.GetAbsolutePathToModFolder(assembly);
        var presetMapPath = Path.Combine(pathToMod, "Assets");

        _presetMap = modHelper.GetJsonDataFromFile<Dictionary<string, PresetData>>(presetMapPath, "presetMap.json");
    }

    public void CreateSariaAssort()
    {
        LoadPresetMap();
        const string saria = "66f4db5ca4958508883d700c";

        // LL1 — early survival kit, soft targets, starter kits
        #region LL1 Meds
        assortUtils.CreateSingleItemOffer("5751a25924597722c463c472", 999999, 1, 1_700, Money.ROUBLES, saria); // Army bandage
        assortUtils.CreateSingleItemOffer("60098af40accd37ef2175f27", 999999, 1, 4_500, Money.ROUBLES, saria); // CAT
        assortUtils.CreateSingleItemOffer("5af0454c86f7746bf20992e8", 999999, 1, 3_900, Money.ROUBLES, saria); // Alu splint
        assortUtils.CreateSingleItemOffer("544fb3f34bdc2d03748b456a", 999999, 1, 13_500, Money.ROUBLES, saria); // Morphine
        assortUtils.CreateSingleItemOffer("5d02778e86f774203e7dedbe", 999999, 1, 28_000, Money.ROUBLES, saria); // CMS
        assortUtils.CreateSingleItemOffer("544fb45d4bdc2dee738b4568", 999999, 1, 20_000, Money.ROUBLES, saria); // Salewa
        #endregion

        #region LL1 Grenades
        assortUtils.CreateSingleItemOffer("5448be9a4bdc2dfd2f8b456a", 999999, 1, 9_000, Money.ROUBLES, saria); // RGD-5
        assortUtils.CreateSingleItemOffer("5710c24ad2720bc3458b45a3", 999999, 1, 13_500, Money.ROUBLES, saria); // F-1
        #endregion

        #region LL1 Ammo
        assortUtils.CreateSingleItemOffer("56d59d3ad2720bdb418b4577", 999999, 1, 50, Money.ROUBLES, saria); // 9x19 Pst
        assortUtils.CreateSingleItemOffer("56dff2ced2720bb4668b4567", 999999, 1, 100, Money.ROUBLES, saria); // 5.45 PP
        assortUtils.CreateSingleItemOffer("59e6906286f7746c9f75e847", 999999, 1, 150, Money.ROUBLES, saria); // 5.56 M856A1
        assortUtils.CreateSingleItemOffer("5e023e53d4353e3302577c4c", 999999, 1, 125, Money.ROUBLES, saria); // 7.62x51 BCP FMJ
        assortUtils.CreateSingleItemOffer("5fbe3ffdf8b6a877a729ea82", 999999, 1, 180, Money.ROUBLES, saria); // .300 BCP FMJ
        #endregion

        #region LL1 Optics
        assortUtils.CreateSingleItemOffer("59f9d81586f7744c7506ee62", 999999, 1, 31_500, Money.ROUBLES, saria); // Vortex Razor UH-1
        #endregion

        #region LL1 Magazines (top capacity per LL1 caliber)
        assortUtils.CreateSingleItemOffer("5a718f958dc32e00094b97e7", 999999, 1, 31_500, Money.ROUBLES, saria); // 9x19 Glock SGMT 50
        assortUtils.CreateSingleItemOffer("55d482194bdc2d1d4e8b456b", 999999, 1, 36_000, Money.ROUBLES, saria); // 5.45 6L31 60
        assortUtils.CreateSingleItemOffer("5aaa5dfee5b5b000140293d3", 999999, 1, 9_000, Money.ROUBLES, saria); // 5.56 PMAG 30 GEN M3
        assortUtils.CreateSingleItemOffer("59c1383d86f774290a37e0ca", 999999, 1, 43_000, Money.ROUBLES, saria); // 5.56/.300 PMAG D-60
        assortUtils.CreateSingleItemOffer("5a3501acc4a282000d72293a", 999999, 1, 25_000, Money.ROUBLES, saria); // 7.62x51 PMAG 20 SR-LR
        assortUtils.CreateSingleItemOffer("6761770e48fa5c377e06fc3c", 999999, 1, 51_000, Money.ROUBLES, saria); // 7.62x51 X-25 50 drum
        #endregion

        #region LL1 Headphones
        assortUtils.CreateSingleItemOffer("5645bcc04bdc2d363b8b4572", 999999, 1, 31_500, Money.ROUBLES, saria); // ComTac 2
        #endregion

        #region LL1 Weapon Presets
        AddPresetByName("vector_9_default", Money.ROUBLES, 73_000, 1);
        AddPresetByName("MP5_SilentOps", Money.ROUBLES, 81_000, 1);
        AddPresetByName("mcx_short_default", Money.ROUBLES, 96_000, 1);
        AddPresetByName("DTMDR308_DEFAULT", Money.ROUBLES, 107_000, 1);
        AddPresetByName("REM700_AICS", Money.ROUBLES, 99_000, 1);
        #endregion

        #region LL1 Gear Presets
        AddPresetByName("Helmet LShZ Standart", Money.ROUBLES, 40_000, 1);
        AddPresetByName("Helmet Diamond Age Bastion Standart", Money.ROUBLES, 62_000, 1);
        AddPresetByName("Vest ANA Tactical M1 Standard", Money.ROUBLES, 79_000, 1);
        #endregion

        // LL2 — mid-pen ammo, solid kits, impact nades start
        #region LL2 Meds
        assortUtils.CreateSingleItemOffer("590c678286f77426c9660122", 999999, 2, 31_500, Money.ROUBLES, saria); // IFAK
        assortUtils.CreateSingleItemOffer("5d02797c86f774203f38e30a", 999999, 2, 73_000, Money.ROUBLES, saria); // Surv12
        assortUtils.CreateSingleItemOffer("5c0e530286f7747fa1419862", 999999, 2, 40_000, Money.ROUBLES, saria); // Propital
        assortUtils.CreateSingleItemOffer("5e8488fa988a8701445df1e4", 999999, 2, 25_000, Money.ROUBLES, saria); // CALOK-B
        #endregion

        #region LL2 Grenades
        assortUtils.CreateSingleItemOffer("58d3db5386f77426186285a0", 999999, 2, 17_000, Money.ROUBLES, saria); // M67
        assortUtils.CreateSingleItemOffer("5e32f56fcb6d5863cc5e5ee4", 999999, 2, 20_000, Money.ROUBLES, saria); // VOG-17
        #endregion

        #region LL2 Ammo
        assortUtils.CreateSingleItemOffer("5c925fa22e221601da359b7b", 999999, 2, 200, Money.ROUBLES, saria); // 9x19 AP 6.3
        assortUtils.CreateSingleItemOffer("56dff061d2720bb5668b4567", 999999, 2, 250, Money.ROUBLES, saria); // 5.45 BT
        assortUtils.CreateSingleItemOffer("54527ac44bdc2d36668b4567", 999999, 2, 290, Money.ROUBLES, saria); // 5.56 M855A1
        assortUtils.CreateSingleItemOffer("59e0d99486f7744a32234762", 999999, 2, 340, Money.ROUBLES, saria); // 7.62x39 BP
        assortUtils.CreateSingleItemOffer("58dd3ad986f77403051cba8f", 999999, 2, 380, Money.ROUBLES, saria); // 7.62x51 M80
        assortUtils.CreateSingleItemOffer("619636be6db0f2477964e710", 999999, 2, 430, Money.ROUBLES, saria); // .300 M62
        #endregion

        #region LL2 Optics
        assortUtils.CreateSingleItemOffer("617151c1d92c473c770214ab", 999999, 2, 107_000, Money.ROUBLES, saria); // S&B PM II 1-8x24
        #endregion

        #region LL2 Weapon Presets
        AddPresetByName("HK416 default", Money.ROUBLES, 124_000, 2);
        AddPresetByName("M4A1_USASOC2", Money.ROUBLES, 141_000, 2);
        AddPresetByName("MP7_DEVGRU", Money.ROUBLES, 130_000, 2);
        AddPresetByName("knight_mk47", Money.ROUBLES, 146_000, 2);
        AddPresetByName("M1A_DEFAULT", Money.ROUBLES, 152_000, 2);
        #endregion

        #region LL2 Gear Presets
        AddPresetByName("Body armor Korund VM Standard", Money.ROUBLES, 107_000, 2);
        AddPresetByName("Body armor HighCom Trooper Standard", Money.ROUBLES, 124_000, 2);
        AddPresetByName("Helmet HighCom Striker ACHHC IIIA Black Standart", Money.ROUBLES, 62_000, 2);
        #endregion

        #region LL2 Backpacks
        assortUtils.CreateSingleItemOffer("545cdae64bdc2d39198b4568", 999999, 2, 62_000, Money.ROUBLES, saria); // Tri-Zip
        #endregion

        // LL3 — hard-pen ammo, high-tier kits, impact RGN/RGO + VOG
        #region LL3 Meds
        assortUtils.CreateSingleItemOffer("60098ad7c2240c0fe85c570a", 999999, 3, 54_000, Money.ROUBLES, saria); // AFAK
        assortUtils.CreateSingleItemOffer("5c0e533786f7747fa23f4d47", 999999, 3, 36_000, Money.ROUBLES, saria); // Zagustin
        assortUtils.CreateSingleItemOffer("5c0e534186f7747fa1419867", 999999, 3, 62_000, Money.ROUBLES, saria); // eTG-change
        #endregion

        #region LL3 Grenades
        assortUtils.CreateSingleItemOffer("617fd91e5539a84ec44ce155", 999999, 3, 25_000, Money.ROUBLES, saria); // RGN
        assortUtils.CreateSingleItemOffer("618a431df1eb8e24b8741deb", 999999, 3, 28_000, Money.ROUBLES, saria); // RGO
        assortUtils.CreateSingleItemOffer("5656eb674bdc2d35148b457c", 999999, 3, 16_000, Money.ROUBLES, saria); // VOG-25
        assortUtils.CreateSingleItemOffer("62e7e7bbe6da9612f743f1e0", 999999, 3, 62_000, Money.ROUBLES, saria); // GP-25
        #endregion

        #region LL3 Ammo
        assortUtils.CreateSingleItemOffer("5efb0da7a29a85116f6ea05f", 999999, 3, 360, Money.ROUBLES, saria); // 9x19 PBP
        assortUtils.CreateSingleItemOffer("56dff026d2720bb8668b4567", 999999, 3, 470, Money.ROUBLES, saria); // 5.45 BS
        assortUtils.CreateSingleItemOffer("5a608bf24f39f98ffc77720e", 999999, 3, 540, Money.ROUBLES, saria); // 7.62x51 M62
        assortUtils.CreateSingleItemOffer("5fd20ff893a8961fc660a954", 999999, 3, 730, Money.ROUBLES, saria); // .300 AP
        #endregion

        #region LL3 Optics
        assortUtils.CreateSingleItemOffer("618ba27d9008e4636a67f61d", 999999, 3, 163_000, Money.ROUBLES, saria); // Vortex Razor HD Gen.2
        #endregion

        #region LL3 Weapon Presets
        AddPresetByName("sig_mcx_spear_cqb", Money.ROUBLES, 197_000, 3);
        AddPresetByName("SCARH MK17 CQC", Money.ROUBLES, 180_000, 3);
        AddPresetByName("G28 Patrol", Money.ROUBLES, 208_000, 3);
        #endregion

        #region LL3 Gear Presets
        AddPresetByName("Vest FirstSpear Strandhogg Standard", Money.ROUBLES, 146_000, 3);
        AddPresetByName("Body armor LBT 6094A Slick Plate Carrier Black Standard", Money.ROUBLES, 180_000, 3);
        AddPresetByName("Helmet Ops Core Fast MT Black Standart", Money.ROUBLES, 85_000, 3);
        #endregion

        // LL4 — crown: top ammo, NVG, meta kits, RShG
        #region LL4 Grenades
        assortUtils.CreateSingleItemOffer("676bf44c5539167c3603e869", 999999, 4, 141_000, Money.ROUBLES, saria); // RShG-2
        #endregion

        #region LL4 Ammo
        assortUtils.CreateSingleItemOffer("5c0d5e4486f77478390952fe", 999999, 4, 960, Money.ROUBLES, saria); // 5.45 PPBS Igolnik
        assortUtils.CreateSingleItemOffer("59e690b686f7746c9f75e848", 999999, 4, 620, Money.ROUBLES, saria); // 5.56 M995
        assortUtils.CreateSingleItemOffer("5a6086ea4f39f99cd479502f", 999999, 4, 880, Money.ROUBLES, saria); // 7.62x51 M61
        #endregion

        #region LL4 Optics / NVG
        assortUtils.CreateSingleItemOffer("5c0558060db834001b735271", 999999, 4, 315_000, Money.ROUBLES, saria); // GPNVG-18
        #endregion

        #region LL4 Weapon Presets
        AddPresetByName("sig_mcx_spear_default", Money.ROUBLES, 270_000, 4);
        AddPresetByName("birdeye_rsass", Money.ROUBLES, 293_000, 4);
        AddPresetByName("M60E6_DEFAULT", Money.ROUBLES, 259_000, 4);
        AddPresetByName("mjolnir_default", Money.ROUBLES, 248_000, 4);
        #endregion

        #region LL4 Gear Presets
        AddPresetByName("Body armor BNTI Zhuk 6a Standard", Money.ROUBLES, 236_000, 4);
        AddPresetByName("Vest Ars Arma A18 Skanda Standard", Money.ROUBLES, 186_000, 4);
        AddPresetByName("Helmet Crye Precision AirFrame Standart", Money.ROUBLES, 101_000, 4);
        AddPresetByName("Helmet Maska 1 Sha Killa Standard", Money.ROUBLES, 113_000, 4);
        #endregion

        #region LL4 Backpacks
        assortUtils.CreateSingleItemOffer("5c0e774286f77468413cc5b2", 999999, 4, 135_000, Money.ROUBLES, saria); // Blackjack 50
        #endregion

        if (_modConfig?.RandomizeStockCount == true)
        {
            RandomizeStock();
        }
    }

    private void RandomizeStock()
    {
        var trader = databaseService.GetTrader("66f4db5ca4958508883d700c");
        var traderAssortItems = trader?.Assort.Items;

        if (traderAssortItems == null)
        {
            logger.Warning("Unable to randomize stock: trader assort items not found");
            return;
        }

        foreach (var item in traderAssortItems)
        {
            if (item.ParentId != "hideout")
            {
                continue;
            }

            item.Upd.UnlimitedCount = false;

            var isOutOfStock = randomUtil.GetChance100(25);
            if (isOutOfStock)
            {
                item.Upd.StackObjectsCount = 0;
                continue;
            }

            if (itemHelper.IsOfBaseclass(item.Template, BaseClasses.AMMO))
            {
                item.Upd.StackObjectsCount = randomUtil.RandInt(1, 300);
            }
            else
            {
                item.Upd.StackObjectsCount = randomUtil.RandInt(1, 10);
            }
        }
    }

    private void AddPresetByName(string presetName, string currency, int cost, int loyaltyLevel)
    {
        var key = presetName.ToUpper().Replace(" ", "_");
        if (_presetMap == null || !_presetMap.TryGetValue(key, out var presetData))
        {
            logger.Warning($"Preset name '{presetName}' (key: '{key}') not found in presetMap.json, skipping...");
            return;
        }

        var items = presetData
            .Items.Select(itemData => new Item
            {
                Id = itemData.Id,
                Template = itemData.Tpl,
                ParentId = itemData.ParentId,
                SlotId = itemData.SlotId,
            })
            .ToList();

        assortUtils
            .CreateComplexAssortItem(items)
            .AddMoneyCost(currency, cost)
            .AddLoyaltyLevel(loyaltyLevel)
            .AddStackCount(999999)
            .Export("66f4db5ca4958508883d700c");
    }
}
