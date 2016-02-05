﻿/*
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
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dapplo.ActiveDirectoryTests
{
	/// <summary>
	/// Test the DistinguishedName class
	/// </summary>
	[TestClass]
	public class DistinguishedNameTests
	{
		private const string testDN = "CN=Karen Berge,CN=admin,DC=corp,DC=Fabrikam,DC=COM";

		[TestMethod]
		public void TestDistinguishedName()
		{
			var dn = DistinguishedName.CreateFrom(testDN);
			Assert.AreEqual(5, dn.RelativeDistinguishedNames.Count);
			Assert.AreEqual(testDN, dn.ToString());
		}

		[TestMethod]
		public void TestBuildDistinguishedName()
		{
			
			var dn = DistinguishedName.Create().CN("Karen Berge").CN("admin").DC("corp").DC("Fabrikam").DC("COM");

			Assert.AreEqual(5, dn.RelativeDistinguishedNames.Count);
			Assert.AreEqual(testDN, dn.ToString());
		}
	}
}