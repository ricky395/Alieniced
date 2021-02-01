// <copyright file="CurrencyValue.cs" company="Trollpants Game Studio AS">
// Copyright (c) 2016 Trollpants Game Studio AS. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Trollpants.CloudOnce.Internal
{
    using Utils;

    /// <summary>
    ///  Data-class to store currency value.
    /// </summary>
    public class CurrencyValue : IJsonConvertible
    {
        private const string c_oldAliasAdditions = "cdAdd";
        private const string c_oldAliasSubtractions = "cdSub";

        private const string c_aliasAdditions = "a";
        private const string c_aliasSubtractions = "s";

        /// <summary>
        /// Data-class to store currency value.
        /// </summary>
        public CurrencyValue()
        {
        }

        /// <summary>
        /// Data-class to store currency value.
        /// </summary>
        /// <param name="additions">Total additions made to this currency.</param>
        /// <param name="subtractions">Total subtractions made to this currency.</param>
        public CurrencyValue(float additions, float subtractions)
        {
            Additions = additions;
            Subtractions = subtractions;
        }

        /// <summary>
        /// Data-class to store currency value.
        /// </summary>
        /// <param name="value">Total current value of this currency.</param>
        public CurrencyValue(float value)
        {
            Value = value;
        }

        /// <summary>
        /// Data-class to store currency value.
        /// </summary>
        /// <param name="jsonObject"><see cref="JSONObject"/> containing a <see cref="CurrencyValue"/></param>
        public CurrencyValue(JSONObject jsonObject)
        {
            FromJSONObject(jsonObject);
        }

        /// <summary>
        /// Total additions made to this currency.
        /// </summary>
        public float Additions { get; set; }

        /// <summary>
        /// Total subtractions made to this currency.
        /// </summary>
        public float Subtractions { get; set; }

        /// <summary>
        /// Current value of this currency.
        /// </summary>
        public float Value
        {
            get
            {
                return Additions + Subtractions;
            }

            set
            {
                var delta = value - Value;
                if (delta > 0f)
                {
                    Additions += delta;
                }
                else
                {
                    Subtractions += delta;
                }
            }
        }

        /// <summary>
        /// Converts the <see cref="CurrencyValue"/> into a <see cref="JSONObject"/>.
        /// </summary>
        /// <returns><see cref="JSONObject"/> containing a <see cref="CurrencyValue"/></returns>
        public JSONObject ToJSONObject()
        {
            var jsonObject = new JSONObject(JSONObject.Type.Object);

            jsonObject.AddField(c_aliasAdditions, Additions);
            jsonObject.AddField(c_aliasSubtractions, Subtractions);

            return jsonObject;
        }

        /// <summary>
        /// Reconstructs a <see cref="CurrencyValue"/> from a <see cref="JSONObject"/>.
        /// </summary>
        /// <param name="jsonObject"><see cref="JSONObject"/> containing a <see cref="CurrencyValue"/></param>
        public void FromJSONObject(JSONObject jsonObject)
        {
            var addAlias = CloudOnceUtils.GetAlias(typeof(CurrencyValue).Name, jsonObject, c_aliasAdditions, c_oldAliasAdditions);
            var subAlias = CloudOnceUtils.GetAlias(typeof(CurrencyValue).Name, jsonObject, c_aliasSubtractions, c_oldAliasSubtractions);

            Additions = jsonObject[addAlias].F;
            Subtractions = jsonObject[subAlias].F;
        }
    }
}
