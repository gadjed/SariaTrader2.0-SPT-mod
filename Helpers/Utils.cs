using System.Reflection;
using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.DI;
using SPTarkov.Server.Core.Helpers;
using SPTarkov.Server.Core.Models.Common;
using SPTarkov.Server.Core.Models.Eft.Common.Tables;
using SPTarkov.Server.Core.Services;
using Path = System.IO.Path;

namespace SariaShop.Helpers;

[Injectable(TypePriority = OnLoadOrder.PostDBModLoader + 1)]
public class SariaHelpers(DatabaseService databaseService, ModHelper modHelper)
{
    public TemplateItem GetItemInTables(string itemId)
    {
        var tables = databaseService.GetTables();
        var item = tables.Templates.Items[itemId];

        return item;
    }

    public string FetchIdFromMap(string key, Dictionary<string, MongoId> map)
    {
        if (MongoId.IsValidMongoId(key))
        {
            return key;
        }

        if (map.TryGetValue(key, out var fetchedKey))
        {
            var finalKey = fetchedKey.ToString();

            return finalKey;
        }
        throw new ArgumentException($"'{key}' was not found in map.");
    }

    public T LoadConfig<T>(Assembly assembly, string pathFromAssets, string configName)
    {
        var pathToMod = modHelper.GetAbsolutePathToModFolder(assembly);
        var finalPath = Path.Combine(pathToMod, "Assets", pathFromAssets);
        var config = modHelper.GetJsonDataFromFile<T>(finalPath, configName);

        return config;
    }
}
