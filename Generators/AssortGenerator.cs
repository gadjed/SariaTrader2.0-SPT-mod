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
        logger.Info($"Loaded {_presetMap.Count} presets from presetMap.json");
    }

    public void CreateSariaAssort()
    {
        LoadPresetMap();

        #region LL1 Items
        assortUtils.CreateSingleItemOffer("56eabcd4d2720b66698b4574", 999999, 1, 29999, Money.ROUBLES, "66f4db5ca4958508883d700c");
        assortUtils.CreateSingleItemOffer("584924ec24597768f12ae244", 999999, 1, 45999, Money.ROUBLES, "66f4db5ca4958508883d700c");
        assortUtils.CreateSingleItemOffer("5a3501acc4a282000d72293a", 999999, 1, 15999, Money.ROUBLES, "66f4db5ca4958508883d700c");
        assortUtils.CreateSingleItemOffer("56eabf3bd2720b75698b4569", 999999, 1, 44999, Money.ROUBLES, "66f4db5ca4958508883d700c");
        assortUtils.CreateSingleItemOffer("59db3a1d86f77429e05b4e92", 999999, 1, 34999, Money.ROUBLES, "66f4db5ca4958508883d700c");
        assortUtils.CreateSingleItemOffer("5a718f958dc32e00094b97e7", 999999, 1, 30999, Money.ROUBLES, "66f4db5ca4958508883d700c");
        assortUtils.CreateSingleItemOffer("628e4e576d783146b124c64d", 999999, 1, 87999, Money.ROUBLES, "66f4db5ca4958508883d700c");
        assortUtils.CreateSingleItemOffer("5fb651dc85f90547f674b6f4", 999999, 1, 28999, Money.ROUBLES, "66f4db5ca4958508883d700c");
        assortUtils.CreateSingleItemOffer("5b057b4f5acfc4771e1bd3e9", 999999, 1, 34999, Money.ROUBLES, "66f4db5ca4958508883d700c");
        assortUtils.CreateSingleItemOffer("58d2912286f7744e27117493", 999999, 1, 69999, Money.ROUBLES, "66f4db5ca4958508883d700c");
        assortUtils.CreateSingleItemOffer("5b2388675acfc4771e1be0be", 999999, 1, 37999, Money.ROUBLES, "66f4db5ca4958508883d700c");
        assortUtils.CreateSingleItemOffer("618b9671d14d6d5ab879c5ea", 999999, 1, 9999, Money.ROUBLES, "66f4db5ca4958508883d700c");
        assortUtils.CreateSingleItemOffer("618b9643526131765025ab35", 999999, 1, 16999, Money.ROUBLES, "66f4db5ca4958508883d700c");
        assortUtils.CreateSingleItemOffer("593962ca86f774068014d9af", 999999, 1, 19999, Money.ROUBLES, "66f4db5ca4958508883d700c");
        assortUtils.CreateSingleItemOffer("5937ee6486f77408994ba448", 999999, 1, 19999, Money.ROUBLES, "66f4db5ca4958508883d700c");
        assortUtils.CreateSingleItemOffer("5aafbcd986f7745e590fff23", 999999, 1, 379999, Money.ROUBLES, "66f4db5ca4958508883d700c");
        assortUtils.CreateSingleItemOffer("5c6d46132e221601da357d56", 999999, 1, 10999, Money.ROUBLES, "66f4db5ca4958508883d700c");
        #endregion

        #region LL1 Ammo
        assortUtils.CreateSingleItemOffer("5e023e53d4353e3302577c4c", 999999, 1, 199, Money.ROUBLES, "66f4db5ca4958508883d700c");
        assortUtils.CreateSingleItemOffer("59e6906286f7746c9f75e847", 999999, 1, 449, Money.ROUBLES, "66f4db5ca4958508883d700c");
        assortUtils.CreateSingleItemOffer("5c925fa22e221601da359b7b", 999999, 1, 399, Money.ROUBLES, "66f4db5ca4958508883d700c");
        assortUtils.CreateSingleItemOffer("6529302b8c26af6326029fb7", 999999, 1, 549, Money.ROUBLES, "66f4db5ca4958508883d700c");
        #endregion

        #region LL1 Barters
        assortUtils
            .CreateSingleAssortItem("544a11ac4bdc2d470e8b456a")
            .AddLoyaltyLevel(1)
            .AddStackCount(999999)
            .AddBarterCost(Money.ROUBLES, 599999)
            .AddBarterCost("5732ee6a24597719ae0c0281", 1)
            .Export("66f4db5ca4958508883d700c");
        #endregion

        #region LL1 Weapon Presets
        AddPresetByName("sig_mcx_spear_cqb", Money.DOLLARS, 899, 1);
        AddPresetByName("vector_9_default", Money.DOLLARS, 599, 1);
        AddPresetByName("vector_45_default", Money.DOLLARS, 749, 1);
        AddPresetByName("HK416 default", Money.DOLLARS, 699, 1);
        AddPresetByName("M4A1_USASOC2", Money.DOLLARS, 1099, 1);
        AddPresetByName("birdeye_sr25", Money.DOLLARS, 1649, 1);
        AddPresetByName("mcx_short_default", Money.DOLLARS, 849, 1);
        AddPresetByName("SR25_default", Money.DOLLARS, 799, 1);
        AddPresetByName("MP5_SilentOps", Money.DOLLARS, 649, 1);
        #endregion

        #region LL1 Gear Presets
        AddPresetByName("Vest Eagle Industries MMAC Standard", Money.ROUBLES, 84999, 1);
        AddPresetByName("Vest Shellback Tactical Banshee Standard", Money.ROUBLES, 114999, 1);
        AddPresetByName("Vest ANA Tactical M1 Standard", Money.ROUBLES, 114999, 1);
        AddPresetByName("Helmet Class Tor 2 Standart", Money.ROUBLES, 57999, 1);
        AddPresetByName("Helmet Diamond Age Bastion Standart", Money.ROUBLES, 94999, 1);
        AddPresetByName("Helmet LShZ Standart", Money.ROUBLES, 47999, 1);
        #endregion

        #region LL2 Items
        assortUtils.CreateSingleItemOffer("5ea058e01dbce517f324b3e2", 999999, 2, 134999, Money.ROUBLES, "66f4db5ca4958508883d700c");
        assortUtils.CreateSingleItemOffer("628bc7fb408e2b2e9c0801b1", 999999, 2, 229999, Money.ROUBLES, "66f4db5ca4958508883d700c");
        assortUtils.CreateSingleItemOffer("5c793fb92e221644f31bfb64", 999999, 2, 74999, Money.ROUBLES, "66f4db5ca4958508883d700c");
        assortUtils.CreateSingleItemOffer("618ba27d9008e4636a67f61d", 999999, 2, 139999, Money.ROUBLES, "66f4db5ca4958508883d700c");
        assortUtils.CreateSingleItemOffer("5b3b99475acfc432ff4dcbee", 999999, 2, 109999, Money.ROUBLES, "66f4db5ca4958508883d700c");
        assortUtils.CreateSingleItemOffer("5d235bb686f77443f4331278", 999999, 2, 599999, Money.ROUBLES, "66f4db5ca4958508883d700c");
        assortUtils.CreateSingleItemOffer("59fb042886f7746c5005a7b2", 999999, 2, 2999999, Money.ROUBLES, "66f4db5ca4958508883d700c");
        assortUtils.CreateSingleItemOffer("59fb023c86f7746d0d4b423c", 999999, 2, 19999, Money.DOLLARS, "66f4db5ca4958508883d700c");
        assortUtils.CreateSingleItemOffer("619cbf7d23893217ec30b689", 999999, 2, 499999, Money.ROUBLES, "66f4db5ca4958508883d700c");
        assortUtils.CreateSingleItemOffer("5648a69d4bdc2ded0b8b457b", 999999, 2, 67999, Money.ROUBLES, "66f4db5ca4958508883d700c");
        assortUtils.CreateSingleItemOffer("5df8a42886f77412640e2e75", 999999, 2, 44999, Money.ROUBLES, "66f4db5ca4958508883d700c");
        #endregion

        #region LL2 Ammo
        assortUtils.CreateSingleItemOffer("5cc80f38e4a949001152b560", 999999, 2, 8, Money.DOLLARS, "66f4db5ca4958508883d700c");
        assortUtils.CreateSingleItemOffer("58dd3ad986f77403051cba8f", 999999, 2, 449, Money.ROUBLES, "66f4db5ca4958508883d700c");
        assortUtils.CreateSingleItemOffer("54527ac44bdc2d36668b4567", 999999, 2, 599, Money.ROUBLES, "66f4db5ca4958508883d700c");
        assortUtils.CreateSingleItemOffer("5efb0cabfb3e451d70735af5", 999999, 2, 599, Money.ROUBLES, "66f4db5ca4958508883d700c");
        assortUtils.CreateSingleItemOffer("5efb0da7a29a85116f6ea05f", 999999, 2, 599, Money.ROUBLES, "66f4db5ca4958508883d700c");
        #endregion

        #region LL2 Barters
        assortUtils
            .CreateSingleAssortItem("5c94bbff86f7747ee735c08f")
            .AddLoyaltyLevel(2)
            .AddStackCount(999999)
            .AddBarterCost("5c0e531d86f7747fa23f4d42", 1)
            .AddBarterCost("5ed51652f6c34d2cc26336a1", 1)
            .AddBarterCost("5c0e530286f7747fa1419862", 1)
            .Export("66f4db5ca4958508883d700c");

        assortUtils
            .CreateSingleAssortItem("5857a8b324597729ab0a0e7d")
            .AddLoyaltyLevel(2)
            .AddStackCount(999999)
            .AddBarterCost(Money.ROUBLES, 999999)
            .AddBarterCost("544a11ac4bdc2d470e8b456a", 1)
            .Export("66f4db5ca4958508883d700c");
        #endregion

        #region LL2 Weapon Presets
        AddPresetByName("9A91_tactical", Money.DOLLARS, 1099, 2);
        AddPresetByName("VSK94_tactical", Money.DOLLARS, 1199, 2);
        AddPresetByName("SA-58_OSW", Money.DOLLARS, 1099, 2);
        AddPresetByName("tx-15 default", Money.DOLLARS, 799, 2);
        AddPresetByName("knight_mk47", Money.DOLLARS, 1099, 2);
        AddPresetByName("MP7_DEVGRU", Money.DOLLARS, 899, 2);
        #endregion

        #region LL2 Gear Presets
        AddPresetByName("Body armor Korund VM Standard", Money.ROUBLES, 149999, 2);
        AddPresetByName("Body armor HighCom Trooper Standard", Money.ROUBLES, 179999, 2);
        AddPresetByName("Helmet MSA Gallet TC 800 Standart", Money.ROUBLES, 64999, 2);
        AddPresetByName("Helmet HighCom Striker ACHHC IIIA Black Standart", Money.ROUBLES, 78999, 2);
        #endregion

        #region LL3 Items
        assortUtils.CreateSingleItemOffer("59bfe68886f7746004266202", 999999, 3, 89999, Money.ROUBLES, "66f4db5ca4958508883d700c");
        assortUtils.CreateSingleItemOffer("5d00ec68d7ad1a04a067e5be", 999999, 3, 58999, Money.ROUBLES, "66f4db5ca4958508883d700c");
        assortUtils.CreateSingleItemOffer("5c0126f40db834002a125382", 999999, 3, 3299999, Money.ROUBLES, "66f4db5ca4958508883d700c");
        assortUtils.CreateSingleItemOffer("5c0e66e2d174af02a96252f4", 999999, 3, 99999, Money.ROUBLES, "66f4db5ca4958508883d700c");
        assortUtils.CreateSingleItemOffer("628baf0b967de16aab5a4f36", 999999, 3, 89999, Money.ROUBLES, "66f4db5ca4958508883d700c");
        #endregion

        #region LL3 Ammo
        assortUtils.CreateSingleItemOffer("5a608bf24f39f98ffc77720e", 999999, 3, 1299, Money.ROUBLES, "66f4db5ca4958508883d700c");
        assortUtils.CreateSingleItemOffer("59e0d99486f7744a32234762", 999999, 3, 1799, Money.ROUBLES, "66f4db5ca4958508883d700c");
        assortUtils.CreateSingleItemOffer("59e690b686f7746c9f75e848", 999999, 3, 1199, Money.ROUBLES, "66f4db5ca4958508883d700c");
        assortUtils.CreateSingleItemOffer("6529243824cbe3c74a05e5c1", 999999, 3, 799, Money.ROUBLES, "66f4db5ca4958508883d700c");
        #endregion

        #region LL3 Barters
        assortUtils
            .CreateSingleAssortItem("5b6d9ce188a4501afc1b2b25")
            .AddLoyaltyLevel(3)
            .AddStackCount(999999)
            .AddBarterCost(Money.DOLLARS, 19999)
            .AddBarterCost("59fb023c86f7746d0d4b423c", 1)
            .Export("66f4db5ca4958508883d700c");

        assortUtils
            .CreateSingleAssortItem("5857a8bc2459772bad15db29")
            .AddLoyaltyLevel(3)
            .AddStackCount(999999)
            .AddBarterCost(Money.ROUBLES, 1499999)
            .AddBarterCost("5857a8b324597729ab0a0e7d", 1)
            .Export("66f4db5ca4958508883d700c");

        assortUtils
            .CreateSingleAssortItem("59db794186f77448bc595262")
            .AddLoyaltyLevel(3)
            .AddStackCount(999999)
            .AddBarterCost(Money.ROUBLES, 1999999)
            .AddBarterCost("5857a8bc2459772bad15db29", 1)
            .Export("66f4db5ca4958508883d700c");
        #endregion

        #region LL3 Weapon Presets
        AddPresetByName("sig_mcx_spear_default", Money.DOLLARS, 1699, 3);
        AddPresetByName("p90 SBRT", Money.DOLLARS, 1399, 3);
        AddPresetByName("SCARH MK17 CQC", Money.DOLLARS, 1099, 3);
        AddPresetByName("G28 Patrol", Money.DOLLARS, 1459, 3);
        AddPresetByName("M1A 2018 new year", Money.DOLLARS, 999, 3);
        AddPresetByName("SCARL MK16 CW", Money.DOLLARS, 949, 3);
        AddPresetByName("rpd_short", Money.DOLLARS, 1699, 3);
        AddPresetByName("Akys_defense_velociraptor_default", Money.DOLLARS, 1999, 3);
        AddPresetByName("AA-12_gen_1_labs", Money.DOLLARS, 1499, 3);
        #endregion

        #region LL3 Gear Presets
        AddPresetByName("Vest FirstSpear Strandhogg Standard", Money.ROUBLES, 189999, 3);
        AddPresetByName("Body armor LBT 6094A Slick Plate Carrier Black Standard", Money.ROUBLES, 249999, 3);
        AddPresetByName("Helmet Ops Core Fast MT Black Standart", Money.ROUBLES, 99999, 3);
        AddPresetByName("Helmet Ops Core Fast MT Sand Standart", Money.ROUBLES, 99999, 3);
        #endregion

        #region LL4 Items
        assortUtils.CreateSingleItemOffer("59c1383d86f774290a37e0ca", 999999, 4, 47999, Money.ROUBLES, "66f4db5ca4958508883d700c");
        assortUtils.CreateSingleItemOffer("5a1ead28fcdbcb001912fa9f", 999999, 4, 179999, Money.ROUBLES, "66f4db5ca4958508883d700c");
        assortUtils.CreateSingleItemOffer("5a1eaa87fcdbcb001865f75e", 999999, 4, 499999, Money.ROUBLES, "66f4db5ca4958508883d700c");
        #endregion

        #region LL4 Ammo
        assortUtils.CreateSingleItemOffer("5efb0c1bd79ff02a1f5e68d9", 999999, 4, 1899, Money.ROUBLES, "66f4db5ca4958508883d700c");
        assortUtils.CreateSingleItemOffer("5fc382a9d724d907e2077dab", 999999, 4, 5999, Money.ROUBLES, "66f4db5ca4958508883d700c");
        assortUtils.CreateSingleItemOffer("601949593ae8f707c4608daa", 999999, 4, 1799, Money.ROUBLES, "66f4db5ca4958508883d700c");
        assortUtils.CreateSingleItemOffer("5fc23426900b1d5091531e15", 999999, 4, 19999, Money.ROUBLES, "66f4db5ca4958508883d700c");
        #endregion

        #region LL4 Barters
        assortUtils
            .CreateSingleAssortItem("5c0a840b86f7742ffa4f2482")
            .AddLoyaltyLevel(4)
            .AddStackCount(999999)
            .AddBarterCost(Money.DOLLARS, 29999)
            .AddBarterCost("59fb042886f7746c5005a7b2", 2)
            .Export("66f4db5ca4958508883d700c");

        assortUtils
            .CreateSingleAssortItem("5c093ca986f7740a1867ab12")
            .AddLoyaltyLevel(4)
            .AddStackCount(999999)
            .AddBarterCost(Money.ROUBLES, 2999999)
            .AddBarterCost("59db794186f77448bc595262", 1)
            .Export("66f4db5ca4958508883d700c");
        #endregion

        #region LL4 Weapon Presets
        AddPresetByName("mjolnir_default", Money.ROUBLES, 309999, 4);
        AddPresetByName("birdeye_rsass", Money.ROUBLES, 349999, 4);
        AddPresetByName("RSASS", Money.ROUBLES, 209999, 4);
        AddPresetByName("akm_kreb_thermal_silenced", Money.ROUBLES, 699999, 4);
        AddPresetByName("M60E6_DEFAULT", Money.ROUBLES, 299999, 4);
        #endregion

        #region LL4 Gear Presets
        AddPresetByName("Helmet Maska 1 Sha Killa Standard", Money.ROUBLES, 129999, 4);
        AddPresetByName("Helmet Crye Precision AirFrame Standart", Money.ROUBLES, 89999, 4);
        AddPresetByName("Vest Tasmanian Tiger SK Standard", Money.ROUBLES, 229999, 4);
        AddPresetByName("Body armor 6B13 M Killa Standard", Money.ROUBLES, 279999, 4);
        AddPresetByName("Vest Ars Arma A18 Skanda Standard", Money.ROUBLES, 229999, 4);
        AddPresetByName("Body armor BNTI Zhuk 6a Standard", Money.ROUBLES, 304999, 4);
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
