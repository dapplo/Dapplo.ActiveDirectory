// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Linq;
using System.Reflection;
using Dapplo.ActiveDirectory.Entities;
using Dapplo.ActiveDirectory.Tests.Entities;
using Dapplo.Log.XUnit;
using Dapplo.Log;
using Xunit;
using Xunit.Abstractions;
using Dapplo.ActiveDirectory.Tests.Entities.Impl;

namespace Dapplo.ActiveDirectory.Tests;

/// <summary>
///	 The following tests will only work in a Domain environment, and should not be started automatically
/// </summary>
public class ManualIntegrationTests
{
	private static readonly LogSource Log = new LogSource();

	public ManualIntegrationTests(ITestOutputHelper testOutputHelper)
	{
		LogSettings.RegisterDefaultLogger<XUnitLogger>(LogLevels.Verbose, testOutputHelper);
	}

	//[Fact]
	public void TestActiveDirectoryQuery_User()
	{
		var query = Query.ForUser(Environment.UserName);
		var userResult = query.Execute<IUser>().ToList();
		Assert.True(userResult.Any());

		// Just something to generate some output
		foreach (var user in userResult)
		{
			Assert.True(user is DispatchProxy);
			Log.Info().WriteLine("Id: {0}", user.Id);
			Log.Info().WriteLine("Name: {0}", user.Name);
			Log.Info().WriteLine("Account expires: {0}", user.AccountExpires);
			ActiveDirectoryExtensions.GetByAdsPath(user.Id);
		}
	}

	//[Fact]
	public void TestActiveDirectoryQuery_Computer()
	{
		var query = Query.ForComputer(Environment.MachineName);
		var computerResult = query.Execute<Computer>().ToList();
		Assert.True(computerResult.Any());

		// Just something to generate some output
		foreach (var computer in computerResult)
		{
#pragma warning disable 184
			Assert.False(computer is DispatchProxy);
#pragma warning restore 184
			Log.Info().WriteLine("Id: {0}", computer.Id);
			Log.Info().WriteLine("Name: {0}", computer.Hostname);
			Log.Info().WriteLine("Description: {0}", computer.Description);
			Log.Info().WriteLine("Location: {0}", computer.Location);
			Log.Info().WriteLine("OperatingSystem: {0}", computer.OperatingSystem);
			Log.Info().WriteLine("OperatingSystemServicePack: {0}", computer.OperatingSystemServicePack);
			Log.Info().WriteLine("WhenCreated: {0}", computer.WhenCreated);
			ActiveDirectoryExtensions.GetByAdsPath(computer.Id);
		}
	}

	//[Fact]
	public void TestActiveDirectoryQuery_IComputer()
	{
		var query = Query.ForComputer(Environment.MachineName);
		var computerResult = query.Execute<IComputer>().ToList();
		Assert.True(computerResult.Any());

		// Just something to generate some output
		foreach (var computer in computerResult)
		{
			// ReSharper disable once SuspiciousTypeConversion.Global
			Assert.True(computer is DispatchProxy);
			Log.Info().WriteLine("Id: {0}", computer.Id);
			Log.Info().WriteLine("Name: {0}", computer.Hostname);
			Log.Info().WriteLine("Description: {0}", computer.Description);
			Log.Info().WriteLine("Location: {0}", computer.Location);
			Log.Info().WriteLine("OperatingSystem: {0}", computer.OperatingSystem);
			Log.Info().WriteLine("OperatingSystemServicePack: {0}", computer.OperatingSystemServicePack);
			Log.Info().WriteLine("WhenCreated: {0}", computer.WhenCreated);
			ActiveDirectoryExtensions.GetByAdsPath(computer.Id);
		}
	}


	//[Fact]
	public void TestActiveDirectoryQuery_ChangeUser()
	{
		var query = Query.ForUser(Environment.UserName);
		var userResult = query.Execute<IUser>().FirstOrDefault();
		Assert.NotNull(userResult);

		userResult.TelephoneNumber = "911";

		userResult.Update(Environment.UserName);
	}
}