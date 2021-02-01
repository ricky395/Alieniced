// <copyright file="IPersistent.cs" company="Trollpants Game Studio AS">
// Copyright (c) 2016 Trollpants Game Studio AS. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Trollpants.CloudOnce.Internal
{
    /// <summary>
    /// Used by cloud preferences to facilitate loading and saving of data.
    /// </summary>
    public interface IPersistent
    {
        /// <summary>
        /// Invokes the cloud preference's <c>delegate</c> used to save data.
        /// </summary>
        void Flush();

        /// <summary>
        /// Invokes the cloud preference's <c>delegate</c> used to load data.
        /// </summary>
        void Load();

        /// <summary>
        /// Resets the <see cref="IPersistent"/> to default/initial value.
        /// </summary>
        void Reset();
    }
}