// <copyright file="GameData.cs" company="Trollpants Game Studio AS">
// Copyright (c) 2016 Trollpants Game Studio AS. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Trollpants.CloudOnce.Internal
{
    using System.Collections.Generic;
    using Utils;

    /// <summary>
    /// Container class for the data the <see cref="DataManager"/> manages ;)
    /// </summary>
    public class GameData
    {
        private const string c_oldSyncableItemsKey = "SIs";
        private const string c_oldSyncableCurrenciesKey = "SCs";

        private const string c_syncableItemsKey = "i";
        private const string c_syncableCurrenciesKey = "c";

        public GameData()
        {
            SyncableItems = new Dictionary<string, SyncableItem>();
            SyncableCurrencies = new Dictionary<string, SyncableCurrency>();
        }

        public GameData(string serializedData)
        {
            if (string.IsNullOrEmpty(serializedData))
            {
                SyncableItems = new Dictionary<string, SyncableItem>();
                SyncableCurrencies = new Dictionary<string, SyncableCurrency>();
                return;
            }

            var jsonObject = new JSONObject(serializedData);

            var itemsAlias = CloudOnceUtils.GetAlias(typeof(GameData).Name, jsonObject, c_syncableItemsKey, c_oldSyncableItemsKey);
            var currencyAlias = CloudOnceUtils.GetAlias(typeof(GameData).Name, jsonObject, c_syncableCurrenciesKey, c_oldSyncableCurrenciesKey);

            SyncableItems = JsonHelper.Convert<Dictionary<string, SyncableItem>>(jsonObject[itemsAlias]);
            SyncableCurrencies = JsonHelper.Convert<Dictionary<string, SyncableCurrency>>(jsonObject[currencyAlias]);
        }

        public Dictionary<string, SyncableItem> SyncableItems { get; set; }

        public Dictionary<string, SyncableCurrency> SyncableCurrencies { get; set; }

        public bool IsDirty { get; set; }

        public string[] GetAllKeys()
        {
            var keys = new List<string>();
            foreach (var syncableItem in SyncableItems)
            {
                keys.Add(syncableItem.Key);
            }

            foreach (var syncableCurrency in SyncableCurrencies)
            {
                keys.Add(syncableCurrency.Key);
            }

            return keys.ToArray();
        }

        public string Serialize()
        {
            var jsonObject = new JSONObject(JSONObject.Type.Object);

            jsonObject.AddField(c_syncableItemsKey, JsonHelper.ToJsonObject(SyncableItems));
            jsonObject.AddField(c_syncableCurrenciesKey, JsonHelper.ToJsonObject(SyncableCurrencies));

            return jsonObject.ToString();
        }

        /// <summary>
        /// Merges this <see cref="GameData"/> with another.
        /// </summary>
        /// <param name="otherData">The <see cref="GameData"/> to merge into this one.</param>
        /// <returns>A <see cref="string"/> array of the changed keys. Will be empty if no data changed.</returns>
        public string[] MergeWith(GameData otherData)
        {
            var changedKeys = new List<string>();
            foreach (var otherItem in otherData.SyncableItems)
            {
                SyncableItem localItem;
                if (SyncableItems.TryGetValue(otherItem.Key, out localItem))
                {
                    var selectedItem = ConflictResolver.ResolveConflict(localItem, otherItem.Value);
                    if (!selectedItem.Equals(localItem))
                    {
                        SyncableItems[otherItem.Key] = selectedItem;
                        changedKeys.Add(otherItem.Key);
                    }
                }
                else
                {
                    SyncableItems.Add(otherItem.Key, otherItem.Value);
                    changedKeys.Add(otherItem.Key);
                }
            }

            foreach (var otherCurrency in otherData.SyncableCurrencies)
            {
                SyncableCurrency localCurrency;
                if (SyncableCurrencies.TryGetValue(otherCurrency.Key, out localCurrency))
                {
                    var mergeResult = localCurrency.MergeWith(otherCurrency.Value);
                    if (mergeResult)
                    {
                        changedKeys.Add(otherCurrency.Key);
                    }
                }
                else
                {
                    SyncableCurrencies.Add(otherCurrency.Key, otherCurrency.Value);
                    changedKeys.Add(otherCurrency.Key);
                }
            }

            return changedKeys.ToArray();
        }
    }
}
