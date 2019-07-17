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

#endregion

namespace Dapplo.ActiveDirectory.Tests
{
	/// <summary>
	///     The following tests will only work in a Domain environment, and should not be started automatically
	/// </summary>
	public class ManualIntegrationTests
	{
		private static readonly LogSource Log = new LogSource();

		public ManualIntegrationTests(ITestOutputHelper testOutputHelper)
		{
			LogSettings.RegisterDefaultLogger<XUnitLogger>(LogLevels.Verbose, testOutputHelper);
		}

		//[Fact]
		public void TestActiveDirectoryQuery_Computer()
		{
			var query = Query.ForComputer(Environment.MachineName);
			var computerResult = query.Execute<Computer>(Environment.UserDomainName).ToList();
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
			var computerResult = query.Execute<IComputer>(Environment.UserDomainName).ToList();
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
			var userResult = query.Execute<User>(Environment.UserDomainName).FirstOrDefault();
			Assert.NotNull(userResult);

			userResult.TelephoneNumber = "911";

			userResult.Update(Environment.UserDomainName);
		}
	}
}