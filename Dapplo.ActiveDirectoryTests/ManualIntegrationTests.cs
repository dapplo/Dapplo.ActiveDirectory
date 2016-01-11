using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dapplo.ActiveDirectory;
using System.Linq;
using System.Diagnostics;

namespace Dapplo.ActiveDirectoryTests
{
	/// <summary>
	/// The following tests will only work in a Domain environment, and should not be started automatically
	/// </summary>
	//[TestClass]
	public class ManualIntegrationTests
	{
		[TestMethod]
		public void TestActiveDirectoryCall()
		{
			var result = Query.CreateAnd().IsUser().EqualTo(UserProperties.Username, Environment.UserName).FindAll(Environment.UserDomainName, new Enum[] { UserProperties.MemberOfGroups });
			Debug.WriteLine("");
		}
	}
}
