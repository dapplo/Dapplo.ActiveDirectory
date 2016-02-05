using Dapplo.ActiveDirectory;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Dapplo.ActiveDirectoryTests
{
	[DataContract]
	public class QueryResult
	{
		[DataMember(Name = "displayName")]
		public string Name { get; set; }

		[DataMember(Name = "memberOf")]
		public IList<DistinguishedName> Groups { get; set; }

	}
}
