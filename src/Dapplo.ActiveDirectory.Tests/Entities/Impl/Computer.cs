// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dapplo.ActiveDirectory.Tests.Entities.Impl;

/// <inheritdoc />
public class Computer : IComputer
{
    public string Description { get; set; }
    public string Hostname { get; set; }
    public string Location { get; set; }
    public string OperatingSystem { get; set; }
    public string OperatingSystemServicePack { get; set; }
    public DateTimeOffset WhenCreated { get; set; }
    public string Id { get; set; }
}