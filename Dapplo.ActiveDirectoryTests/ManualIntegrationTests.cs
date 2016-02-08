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
	//[TestClass]
	public class ManualIntegrationTests
	{
		private static readonly LogSource Log = new LogSource();

		[TestMethod]
		public void TestActiveDirectoryCall()
		{
			var query = Query.CreateAnd().IsUser().EqualTo(UserProperties.Username, Environment.UserName);
			var result = query.Execute<QueryResult>(Environment.UserDomainName).ToList();
			Assert.IsTrue(result.Any());

			// Just something to generate some output
			foreach (var user in result)
			{
				Log.Info().WriteLine("Found name: {0}", user.Displayname);
				Log.Info().WriteLine("Has thumbnail: {0}", user.Thumbnail != null);
				Log.Info().WriteLine("Is member of {0} groups", user.Groups.Count());
			}
		}
	}
}
