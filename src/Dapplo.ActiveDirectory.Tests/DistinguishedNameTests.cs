//  Dapplo - building blocks for desktop applications
//  Copyright (C) 2015-2019 Dapplo
// 
//  For more information see: http://dapplo.net/
//  Dapplo repositories are hosted on GitHub: https://github.com/dapplo
// 
//  This file is part of Dapplo.ActiveDirectory
// 
//  Dapplo.ActiveDirectory is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  Dapplo.ActiveDirectory is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
// 
//  You should have a copy of the GNU Lesser General Public License
//  along with Dapplo.ActiveDirectory. If not, see <http://www.gnu.org/licenses/lgpl.txt>.

#region using

using System.Linq;
using Dapplo.ActiveDirectory.Entities;
using Dapplo.ActiveDirectory.Enums;
using Dapplo.Log.XUnit;
using Dapplo.Log;
using Xunit;
using Xunit.Abstractions;

#endregion

namespace Dapplo.ActiveDirectory.Tests
{
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
}