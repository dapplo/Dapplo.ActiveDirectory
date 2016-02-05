# Dapplo.ActiveDirectory
A library for easy Active Directory access

Although it's not hard to access the Active Directory from .NET, sometimes a little bit of help is nice.
The idea behind this library is that you can build queries via a fluent API (some shortcuts available).
And you don't need to understand all of the details of the Active Directory.

Fluent API:
```
	var query = Query.CreateAnd().IsUser().EqualTo(UserProperties.Username, Environment.UserName);
```
Shortcut:
```
	var query = Query.UsernameFilter(Environment.UserName);
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
		Username
	}
```

Now, we can use this enum in the query but also to define a result object like this:
```
	public class UserInfo
	{
		[AdProperty(UserProperties.DisplayName)]
		public string Name { get; set; }

		[AdProperty(UserProperties.MemberOfGroups)]
		public IList<DistinguishedName> Groups { get; set; }
	}
```

Get your user info object from the current domain:
```
	foreach(var user in query.Execute<UserInfo>()) {
		Console.WriteLine(user.Name);
	}
```
Short explanation: Execute is called, as an extensions method, on the query and the result type is specified.
To be able to tell the AD what it needs to return, it will take the properties from the type.
The result from the AD, with use of the properties, is than mapped into instances of your result type.

P.S.
Writing is currently not supported, I don't know if I want to add this in the near future but if someone makes a pull-request... I'll gladly merge it.
