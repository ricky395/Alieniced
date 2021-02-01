﻿// <copyright file="JsonInterfaces.cs" company="Trollpants Game Studio AS">
// Copyright (c) 2016 Trollpants Game Studio AS. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Trollpants.CloudOnce.Internal
{
    public interface IJsonConvertible : IJsonSerializeable, IJsonDeserializable
    {
    }

    public interface IJsonSerializeable
    {
        JSONObject ToJSONObject();
    }

    public interface IJsonDeserializable
    {
        void FromJSONObject(JSONObject jsonObject);
    }
}
