// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dapplo.ActiveDirectory.Enums;

namespace Dapplo.ActiveDirectory.Finder.Entities;

public interface IUser : IAdObject
{
    [AdProperty(UserProperties.IpTelephoneNumber)]
    string AgentId
    {
        get;
        set;
    }

    [AdProperty(UserProperties.DisplayName)]
    string DisplayName
    {
        get;
        set;
    }

    [AdProperty(UserProperties.GivenName)]
    string Firstname
    {
        get;
        set;
    }

    [AdProperty(UserProperties.Username)]
    string Username
    {
        get;
        set;
    }

    [AdProperty(UserProperties.Surname)]
    string Name
    {
        get;
        set;
    }

    [AdProperty(UserProperties.Location)]
    string Location
    {
        get;
        set;
    }

    [AdProperty(UserProperties.Department)]
    string Department
    {
        get;
        set;
    }

    [AdProperty(UserProperties.Pager)]
    string TeamKz
    {
        get;
        set;
    }

    [AdProperty(UserProperties.Thumbnail)]
    byte[] ThumbnailBytes
    {
        get;
        set;
    }
}