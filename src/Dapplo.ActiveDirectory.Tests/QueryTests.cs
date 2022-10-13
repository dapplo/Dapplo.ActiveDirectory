// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dapplo.ActiveDirectory.Entities;
using Dapplo.ActiveDirectory.Enums;
using Xunit;

namespace Dapplo.ActiveDirectory.Tests;

public class QueryTests
{
	[Fact]
	public void TestQueryBuilder()
	{
		var targetFilter = $"(&(objectClass=user)(sAMAccountname={Environment.UserName}))";

		var userFilterComplex = Query.AND.WhereIsUser().WhereEqualTo(UserProperties.Username, Environment.UserName).Build();
		Assert.Equal(targetFilter.ToLowerInvariant(), userFilterComplex.ToLowerInvariant());

		var userFilterSimple = Query.ForUser(Environment.UserName).Build();
		Assert.Equal(targetFilter.ToLowerInvariant(), userFilterSimple.ToLowerInvariant());
	}

	[Fact]
	public void TestSubQueryBuilder()
	{
		const string targetFilter = "(&(objectClass=person)(|(ou:dn:=ResearchAndDevelopment)(ou:dn:=HumanResources)))";

		var userFilterComplex = Query.AND.WhereEqualTo("objectClass", "person").Or.WhereEqualTo("ou:dn:", "ResearchAndDevelopment").WhereEqualTo("ou:dn:", "HumanResources").Build();

		Assert.Equal(targetFilter.ToLowerInvariant(), userFilterComplex.ToLowerInvariant());
	}

	[Fact]
	public void TestSubQueryBuilder_MoreComplex()
	{
		const string targetFilter = "(&(&(!(cn:dn:=jbond))(|(ou:dn:=ResearchAndDevelopment)(ou:dn:=HumanResources)))(objectclass=Person))";

		var userFilterComplex =
			Query.AND.And.WhereNot("cn:dn:", "jbond").Or.WhereEqualTo("ou:dn:", "ResearchAndDevelopment").WhereEqualTo("ou:dn:", "HumanResources").Parent.Parent.WhereEqualTo("objectclass", "Person").Build();

		Assert.Equal(targetFilter.ToLowerInvariant(), userFilterComplex.ToLowerInvariant());
	}
}