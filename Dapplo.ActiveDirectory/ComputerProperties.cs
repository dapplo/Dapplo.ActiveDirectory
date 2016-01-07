using System.Runtime.Serialization;

namespace Dapplo.ActiveDirectory
{
	public enum ComputerProperties
	{
		[EnumMember(Value = "operatingSystem")]
		OperatingSystem,
		[EnumMember(Value = "dNSHostName")]
		HostName,
	}
}
