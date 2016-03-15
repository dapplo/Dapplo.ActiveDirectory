/*
	Dapplo - building blocks for desktop applications
	Copyright (C) 2015-2016 Dapplo

	For more information see: http://dapplo.net/
	Dapplo repositories are hosted on GitHub: https://github.com/dapplo

	This file is part of Dapplo.ActiveDirectory

	Dapplo.ActiveDirectory is free software: you can redistribute it and/or modify
	it under the terms of the GNU General Public License as published by
	the Free Software Foundation, either version 3 of the License, or
	(at your option) any later version.

	Dapplo.ActiveDirectory is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
	GNU General Public License for more details.

	You should have received a copy of the GNU General Public License
	along with Dapplo.ActiveDirectory.  If not, see <http://www.gnu.org/licenses/>.
 */

using Dapplo.ActiveDirectory;
using Xunit;
using System.Linq;
using Xunit.Abstractions;
using Dapplo.ActiveDirectory.Tests;
using Dapplo.LogFacade;

namespace Dapplo.ActiveDirectoryTests
{
	/// <summary>
	/// Test the DistinguishedName class
	/// </summary>
	public class DistinguishedNameTests
	{
		private const string TestDnString = "CN=Karen Berge,CN=admin,DC=corp,DC=Fabrikam,DC=COM";

		public DistinguishedNameTests(ITestOutputHelper testOutputHelper)
		{
			XUnitLogger.RegisterLogger(testOutputHelper, LogLevel.Verbose);
		}

		[Fact]
		public void TestDistinguishedName()
		{
			var dn = DistinguishedName.CreateFrom(TestDnString);
			Assert.Equal(5, dn.RelativeDistinguishedNames.Count);
			Assert.Equal(TestDnString, dn.ToString());
		}

		[Fact]
		public void TestBuildDistinguishedName()
		{
			var dn = DistinguishedName.Create().CN("Karen Berge").CN("admin").DC("corp").DC("Fabrikam").DC("COM");

			Assert.Equal(5, dn.RelativeDistinguishedNames.Count);
			Assert.Equal(TestDnString, dn.ToString());

			var dc = string.Join(".", dn.Where(x => x.Key == DistinguishedNameAttributes.DomainComponent).Select(x => x.Value));
			Assert.Equal("corp.Fabrikam.COM", dc);
		}

		[Fact]
		public void TestCastDistinguishedName()
		{
			var dn = (DistinguishedName)TestDnString;
			Assert.Equal(5, dn.RelativeDistinguishedNames.Count);
			Assert.Equal(TestDnString, (string)dn);
		}
	}
}
