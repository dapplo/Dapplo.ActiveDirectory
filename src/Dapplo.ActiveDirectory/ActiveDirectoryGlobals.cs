﻿// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using Dapplo.ActiveDirectory.Internal;

namespace Dapplo.ActiveDirectory;

/// <summary>
///     Some configuration settings
/// </summary>
public static class ActiveDirectoryGlobals
{
	/// <summary>
	/// Specify the global page size for searches
	/// Note: 0 means no page size, and retrieve everything up to the SizeLimit
	/// Everything smaller than the SizeLimit (but gt 0) will make the search ignore the size limit!
	/// </summary>
	public static int PageSize { get; set; } = 50;

	/// <summary>
	/// Specify the global size limit for searches
	/// Note:
	/// 0 means no limit
	/// If size limit is large than page size, this limit is ignored!!!
	/// </summary>
	public static int SizeLimit { get; set; }

	/// <summary>
	/// Specify if the search is cached
	/// </summary>
	public static bool CacheResults { get; set; } = false;

	/// <summary>
	/// The format used for the LDAP URI building
	/// </summary>
	public static string LdapUriFormat { get; set; } = @"LDAP://{0}";

	/// <summary>
	/// Specify the value for AuthenticationTypes to be used when none is supplied.
	/// The default according to <a href="https://docs.microsoft.com/en-us/dotnet/api/system.directoryservices.authenticationtypes">here</a> is Secure
	/// </summary>
	public static AuthenticationTypes AuthenticationType { get; set; } = AuthenticationTypes.Secure;

	/// <summary>
	/// This is the factory used to generate all the objects in the result
	/// </summary>
	public static IAdObjectFactory Factory { get; set; } = new SimpleFactory();

	/// <summary>
	/// This is the default domain server for the current user, which is cached but can be overwritten by the application.
	/// </summary>
	/// <returns>string</returns>
	public static string DefaultDomainServerName { get; set; } = BuildDefaultDomainServerName();

	/// <summary>
	/// Supporting method to help to build the default domain server name
	/// </summary>
	/// <returns>string with the default domain server name</returns>
	private static string BuildDefaultDomainServerName()
	{
		string domain = null;
		// Use build in functionality to do it right
		try
		{
			domain = Domain.GetCurrentDomain().Name;
		}
		catch (Exception)
		{
			// Couldn't get the current domain, e.g. when computer is not domain joined
			// This is ignored as we have a fallback, and it's not logged, as we do not have a logger here.
		}

		// Fallback solution
		if (string.IsNullOrEmpty(domain))
		{
			// Use the USERDNSDOMAIN for the domain server dns name
			domain = Environment.GetEnvironmentVariable("USERDNSDOMAIN");
		}

		return domain;
	}
}