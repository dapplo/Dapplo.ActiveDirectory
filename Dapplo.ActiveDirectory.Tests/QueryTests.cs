//  Dapplo - building blocks for desktop applications
//  Copyright (C) 2015-2016 Dapplo
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

using System;
using Dapplo.ActiveDirectory.Entities;
using Dapplo.ActiveDirectory.Enums;
using Xunit;

#endregion

namespace Dapplo.ActiveDirectory.Tests
{
	public class QueryTests
	{
		[Fact]
		public void TestQueryBuilder()
		{
			var targetFilter = $"(&(objectClass=user)(sAMAccountname={Environment.UserName}))";

			var userFilterComplex = Query.AND.WhereIsUser().WhereEqualTo(UserProperties.Username, Environment.UserName).Build();
			Assert.Equal(targetFilter, userFilterComplex);

			var userFilterSimple = Query.ForUser(Environment.UserName).Build();
			Assert.Equal(targetFilter, userFilterSimple);
		}

		[Fact]
		public void TestSubQueryBuilder()
		{
			const string targetFilter = "(&(objectClass=person)(|(ou:dn:=ResearchAndDevelopment)(ou:dn:=HumanResources)))";

			var userFilterComplex = Query.AND.WhereEqualTo("objectClass", "person").Or.WhereEqualTo("ou:dn:", "ResearchAndDevelopment").WhereEqualTo("ou:dn:", "HumanResources").Build();

			Assert.Equal(targetFilter, userFilterComplex);
		}

		[Fact]
		public void TestSubQueryBuilder_MoreComplex()
		{
			const string targetFilter = "(&(&(!(cn:dn:=jbond))(|(ou:dn:=ResearchAndDevelopment)(ou:dn:=HumanResources)))(objectclass=Person))";

			var userFilterComplex =
				Query.AND.And.WhereNot("cn:dn:", "jbond").Or.WhereEqualTo("ou:dn:", "ResearchAndDevelopment").WhereEqualTo("ou:dn:", "HumanResources").Parent.Parent.WhereEqualTo("objectclass", "Person");

			Assert.Equal(targetFilter, userFilterComplex.Build());
		}
	}
}