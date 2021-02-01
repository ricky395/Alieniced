// <copyright file="CloudCurrencyFloat.cs" company="Trollpants Game Studio AS">
// Copyright (c) 2016 Trollpants Game Studio AS. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Trollpants.CloudOnce.CloudPrefs
{
    using Internal;

    /// <summary>
    /// Used to create virtual currencies that gets stored in the cloud. This type uses <see cref="float"/> values.
    /// It has a special conflict resolution system based on this article:
    /// <c>https://developer.android.com/training/cloudsave/conflict-res.html</c>
    /// </summary>
    public sealed class CloudCurrencyFloat : PersistentCurrency
    {
        /// <summary>
        /// Used to create virtual currencies that gets stored in the cloud. This type uses <see cref="float"/> values.
        /// It has a special conflict resolution system based on this article:
        /// <c>https://developer.android.com/training/cloudsave/conflict-res.html</c>
        /// </summary>
        /// <param name="key">A unique identifier for this particular currency.</param>
        /// <param name="defaultValue">The currency's default/starting value.</param>
        /// <param name="allowNegative">If the value of this currency is allowed to be negative.</param>
        public CloudCurrencyFloat(string key, float defaultValue = 0f, bool allowNegative = false)
            : base(key, defaultValue, allowNegative)
        {
            DataManager.InitializeCurrency(key);
            Load();
        }
    }
}
