// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Linq;
using Dapplo.ActiveDirectory.Entities;
using Dapplo.ActiveDirectory.Enums;
using Dapplo.Log.XUnit;
using Dapplo.Log;
using Xunit;
using Xunit.Abstractions;

namespace Dapplo.ActiveDirectory.Tests;

/// <summary>
///     Test the DistinguishedName class
/// </summary>
public class DistinguishedNameTests
{
	private const string TestDnString = "CN=Karen Berge,CN=admin,DC=corp,DC=Fabrikam,DC=COM";

	public DistinguishedNameTests(ITestOutputHelper testOutputHelper)
	{
		LogSettings.RegisterDefaultLogger<XUnitLogger>(LogLevels.Verbose, testOutputHelper);
	}

	[Fact]
	public void TestBuildDistinguishedName()
	{
		var dn = DistinguishedName.Create().Cn("Karen Berge").Cn("admin").Dc("corp").Dc("Fabrikam").Dc("COM");

		Assert.Equal(5, dn.RelativeDistinguishedNames.Count());
		Assert.Equal(TestDnString, dn.ToString());

		var dc = string.Join(".", dn.Where(x => x.Key == DistinguishedNameAttributes.DomainComponent).Select(x => x.Value));
		Assert.Equal("corp.Fabrikam.COM", dc);
	}

	[Fact]
	public void TestCastDistinguishedName()
	{
		var dn = (DistinguishedName) TestDnString;
		Assert.Equal(5, dn.RelativeDistinguishedNames.Count());
		Assert.Equal(TestDnString, dn);
	}

	[Fact]
	public void TestDistinguishedName()
	{
		var dn = DistinguishedName.CreateFrom(TestDnString);
		Assert.Equal(5, dn.RelativeDistinguishedNames.Count());
		Assert.Equal(TestDnString, dn.ToString());
	}
}