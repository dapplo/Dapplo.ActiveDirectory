# Dapplo.ActiveDirectory
A library for easy Active Directory access

- Documentation can be found [here](http://www.dapplo.net/Dapplo.ActiveDirectory)
- Current build status: [![Build status](https://ci.appveyor.com/api/projects/status/sbpswhfxasr6gm4y?svg=true)](https://ci.appveyor.com/project/dapplo/dapplo-ActiveDirectory)
- Coverage Status: [![Coverage Status](https://coveralls.io/repos/github/dapplo/Dapplo.ActiveDirectory/badge.svg?branch=master)](https://coveralls.io/github/dapplo/Dapplo.ActiveDirectory?branch=master)
- NuGet package: [![NuGet package](https://badge.fury.io/nu/Dapplo.ActiveDirectory.svg)](https://badge.fury.io/nu/Dapplo.ActiveDirectory)

Although it's not extremely hard to access the Active Directory from .NET, sometimes a little bit of help is nice.

The idea behind this library is that you can build queries via a fluent API (some shortcuts available).
And you don't need to understand all of the details of the Active Directory.

Fluent API:
```
	var query = Query.AND.Where("objectClass", "user", Comparisons.EqualTo).Where(UserProperties.Username, Environment.UserName, Comparisons.EqualTo);
```
Easier readable:
```
	var query = Query.AND.WhereIsUser().WhereEqualTo(UserProperties.Username, Environment.UserName);
```
Shortest version:
```
	var query = Query.ForUser(Environment.UserName);
```

Or query for at all the active users that have a telephone number:
Complete version
```
	var query = Query.AND.Where("objectClass", "user", Comparisons.EqualTo).WhereNot(Property.BitAnd(UserProperties.UserAccountControl), (int)UserAccountControlFlags.AccountDisabled).Where(UserProperties.TelephoneNumber, Value.Any, Comparisons.EqualTo);
```
Shortest version:
```
	var query = Query.ForUser().WhereAccountEnabled().WhereAny(UserProperties.TelephoneNumber);
```

The properties are defined via enums, some are already pre-defined in the library, but you can use any enum you like.
Either the Enum value is the property name in the AD, or you can use EnumValue attribute on your enum value to mark the property name.

Here is an enum example:
```
	public enum UserProperties
	{
		[EnumMember(Value = "displayName")]
		DisplayName,
		[EnumMember(Value = "memberOf")]
		MemberOfGroups,
		[EnumMember(Value = "sAMAccountname")]
		Username,
		
		...
	}
```

Now, we can use this enum in the query but also to define a result object (use interfaces) like this:
```
	public interface IUserInfo : IAdObject
	{
		[AdProperty(UserProperties.DisplayName)]
		string Name { get; set; }

		[AdProperty(UserProperties.MemberOfGroups)]
		IList<DistinguishedName> Groups { get; set; }
		
		...
	}
```

Get your user info object from the current domain:
```
	foreach(var user in query.Execute<IUserInfo>()) {
		Console.WriteLine(user.Name);
	}
```
Short explanation: Execute is called, as an extensions method, on the query and the result type is specified.
To be able to tell the AD what it needs to return, it will take the properties from the type.
The result from the AD, with use of the properties, is than mapped into instances of your result type.

Changing values in the active directory is possible, but is far from completed, and works as follows:
Add ONE property marked with [AdProperty(AdProperties.Id)] to your result object.
Query for the information you want to change, change the properties and call the ActiveDirectoryExtensions.Update(object, string domain) method.
It will update all the values that are writable, but there is no conversion in the code yet so it will not always work.

Notes:
There is no support yet for a single value query, but this should not be used anyway for performance reasons.
The property names and values are specials classes with implicit cast from enum & string, said differently: you can use enums and strings mixed.
These classes can help with using special values, like Value.StartsWith and Value.Endwith...

