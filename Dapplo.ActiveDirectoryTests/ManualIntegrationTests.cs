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
	along with Dapplo.ActiveDirectory. If not, see <http://www.gnu.org/licenses/>.
 */

using Dapplo.ActiveDirectory;
using Dapplo.LogFacade;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Dapplo.ActiveDirectoryTests
{
	/// <summary>
	/// The following tests will only work in a Domain environment, and should not be started automatically
	/// </summary>
	[TestClass]
	public class ManualIntegrationTests
	{
		private static readonly LogSource Log = new LogSource();

		[TestMethod]
		public void TestActiveDirectoryQuery_Computer()
		{
			var query = Query.ForComputer(Environment.MachineName);
			var computerResult = query.Execute<Computer>(Environment.UserDomainName).ToList();
			Assert.IsTrue(computerResult.Any());

			// Just something to generate some output
			foreach (var computer in computerResult)
			{
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

		[TestMethod]
		public void TestActiveDirectoryQuery_User()
		{
			var query = Query.ForUser().WhereAccountEnabled().WhereAny(UserProperties.IpTelephoneNumber).WhereAny(UserProperties.TelephoneNumber);
			var userResult = query.Execute<User>(Environment.UserDomainName).ToList();
			Assert.IsTrue(userResult.Any());

			// Just something to generate some output
			foreach (var user in userResult)
			{
				Log.Info().WriteLine("Id: {0}", user.Id);
				Log.Info().WriteLine("Name: {0}", user.Displayname);
				Log.Info().WriteLine("DistinguishedName: {0}", user.DistinguishedName);
				Log.Info().WriteLine("Found name: {0}", user.Displayname);
				Log.Info().WriteLine("Has thumbnail: {0}", user.Thumbnail != null);
				Log.Info().WriteLine("Is member of {0} groups", user.Groups.Count());
				ActiveDirectoryExtensions.GetByAdsPath(user.Id);
			}
		}
	}
}
