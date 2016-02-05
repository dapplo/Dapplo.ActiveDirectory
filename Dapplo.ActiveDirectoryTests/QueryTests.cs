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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Dapplo.ActiveDirectoryTests
{
	[TestClass]
	public class QueryTests
	{
		[TestMethod]
		public void TestQueryBuilder()
		{
			var targetFilter = $"(&(objectCategory=user)(sAMAccountname={Environment.UserName}))";

			var userFilterComplex = Query.CreateAnd().IsUser().EqualTo(UserProperties.Username, Environment.UserName).Build();
			Assert.AreEqual(targetFilter, userFilterComplex);

			var userFilterSimple = Query.UsernameFilter(Environment.UserName).Build();
			Assert.AreEqual(targetFilter, userFilterSimple);
		}

		[TestMethod]
		public void TestSubQueryBuilder()
		{
			var targetFilter = "(&(objectClass=person)(|(ou:dn:=ResearchAndDevelopment)(ou:dn:=HumanResources)))";

			var userFilterComplex = Query.CreateAnd().EqualTo("objectClass", "person").Or().EqualTo("ou:dn:", "ResearchAndDevelopment").EqualTo("ou:dn:", "HumanResources").Build();

			Assert.AreEqual(targetFilter, userFilterComplex);
		}

		[TestMethod]
		public void TestSubQueryBuilder_MoreComplex()
		{
			var targetFilter = "(&(&(!(cn:dn:=jbond))(|(ou:dn:=ResearchAndDevelopment)(ou:dn:=HumanResources)))(objectclass=Person))";

			var userFilterComplex = Query.CreateAnd().And().NotEqualTo("cn:dn:", "jbond").Or().EqualTo("ou:dn:", "ResearchAndDevelopment").EqualTo("ou:dn:", "HumanResources").Parent.Parent.EqualTo("objectclass", "Person");

			Assert.AreEqual(targetFilter, userFilterComplex.Build());
		}

	}
}
