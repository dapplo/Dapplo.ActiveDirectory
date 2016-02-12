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
	along with Dapplo.ActiveDirectory.  If not, see <http://www.gnu.org/licenses/>.
 */


using System.Runtime.Serialization;

namespace Dapplo.ActiveDirectory
{
	/// <summary>
	/// This enum specifiesmost known ObjectClass or ObjectCategory values.
	/// They are not testet, and the name in the information "might" not represent the name in the LDAP.
	/// See for a complete list <a href="https://msdn.microsoft.com/en-us/library/windows/desktop/ms680938.aspx">here</a>
	/// </summary>
	public enum ClassNames
	{
		[EnumMember(Value = "account")]
		Account,
		ACSPolicy,
		ACSResourceLimits,
		ACSSubnet,
		AddressBookContainer,
		AddressTemplate,
		ApplicationEntity,
		ApplicationProcess,
		ApplicationSettings,
		ApplicationSiteSettings,
		ApplicationVersion,
		AttributeSchema,
		BootableDevice,
		BuiltinDomain,
		CategoryRegistration,
		CertificationAuthority,
		ClassRegistration,
		ClassSchema,
		ClassStore,
		ComConnectionPoint,
		Computer,
		Configuration,
		ConnectionPoint,
		Contact,
		Container,
		ControlAccessRight,
		Country,
		CRLDistributionPoint,
		CrossRef,
		CrossRefContainer,
		Device,
		DfsConfiguration,
		DHCPClass,
		DisplaySpecifier,
		DisplayTemplate,
		DMD,
		DnsNode,
		DnsZone,
		Document,
		DocumentSeries,
		Domain,
		DomainDNS,
		DomainPolicy,
		DomainRelatedObject,
		DSA,
		DSUISettings,
		DynamicObject,
		FileLinkTracking,
		FileLinkTrackingEntry,
		ForeignSecurityPrincipal,
		friendlyCountry,
		FTDfs,
		Group,
		GroupOfNames,
		groupOfUniqueNames,
		GroupPolicyContainer,
		Ieee802Device,
		IndexServerCatalog,
		inetOrgPerson,
		InfrastructureUpdate,
		IntellimirrorGroup,
		IntellimirrorSCP,
		InterSiteTransport,
		InterSiteTransportContainer,
		IpHost,
		IpNetwork,
		IpProtocol,
		IpsecBase,
		IpsecFilter,
		IpsecISAKMPPolicy,
		IpsecNegotiationPolicy,
		IpsecNFA,
		IpsecPolicy,
		IpService,
		Leaf,
		LicensingSiteSettings,
		LinkTrackObjectMoveTable,
		LinkTrackOMTEntry,
		LinkTrackVolEntry,
		LinkTrackVolumeTable,
		Locality,
		LostAndFound,
		MailRecipient,
		Meeting,
		MsAuthzCentralAccessPolicies,
		MsAuthzCentralAccessPolicy,
		MsAuthzCentralAccessRule,
		MsAuthzCentralAccessRules,
		MsCOMPartition,
		MsCOMPartitionSet,
		MsDFSDeletedLinkv2,
		MsDFSLinkv2,
		MsDFSNamespaceAnchor,
		MsDFSNamespacev2,
		MsDFSRConnection,
		MsDFSRContent,
		MsDFSRContentSet,
		MsDFSRGlobalSettings,
		MsDFSRLocalSettings,
		MsDFSRMember,
		MsDFSRReplicationGroup,
		MsDFSRSubscriber,
		MsDFSRSubscription,
		MsDFSRTopology,
		MsDNSServerSettings,
		MsDSAppConfiguration,
		MsDSAppData,
		MsDSAzAdminManager,
		MsDSAzApplication,
		MsDSAzOperation,
		MsDSAzRole,
		MsDSAzScope,
		MsDSAzTask,
		MsDSBindableObject,
		MsDSBindProxy,
		MsDSClaimsTransformationPolicies,
		MsDSClaimsTransformationPolicyType,
		MsDSClaimType,
		MsDSClaimTypePropertyBase,
		MsDSClaimTypes,
		MsDSGroupManagedServiceAccount,
		MsDSManagedServiceAccount,
		MsDSOptionalFeature,
		MsDSPasswordSettings,
		MsDSPasswordSettingsContainer,
		MsDSQuotaContainer,
		MsDSQuotaControl,
		MsDSResourceProperties,
		MsDSResourceProperty,
		MsDSResourcePropertyList,
		MsDSServiceConnectionPointPublicationService,
		MsDSValueType,
		MsExchConfigurationContainer,
		MsFVERecoveryInformation,
		Msieee80211Policy,
		MsImagingPostScanProcess,
		MsImagingPSPs,
		MsKdsProvRootKey,
		MsKdsProvServerConfiguration,
		MSMQConfiguration,
		MSMQCustomRecipient,
		MSMQEnterpriseSettings,
		MSMQGroup,
		MSMQMigratedUser,
		MSMQQueue,
		MSMQSettings,
		MSMQSiteLink,
		Msnetieee80211GroupPolicy,
		Msnetieee8023GroupPolicy,
		MsPKIEnterpriseOid,
		MsPKIKeyRecoveryAgent,
		MsPKIPrivateKeyRecoveryAgent,
		MsPrintConnectionPolicy,
		MsSFU30DomainInfo,
		MsSFU30MailAliases,
		MsSFU30NetId,
		MsSFU30NetworkUser,
		MsSFU30NISMapConfig,
		MsSPPActivationObject,
		MsSPPActivationObjectsContainer,
		MSSQLOLAPCube,
		MSSQLOLAPDatabase,
		MSSQLOLAPServer,
		MSSQLSQLDatabase,
		MSSQLSQLPublication,
		MSSQLSQLRepository,
		MSSQLSQLServer,
		MsTAPIRtConference,
		MsTAPIRtPerson,
		MsTPMInformationObject,
		MsTPMInformationObjectsContainer,
		MsWMIIntRangeParam,
		MsWMIIntSetParam,
		MsWMIMergeablePolicyTemplate,
		MsWMIObjectEncoding,
		MsWMIPolicyTemplate,
		MsWMIPolicyType,
		MsWMIRangeParam,
		MsWMIRealRangeParam,
		MsWMIRule,
		MsWMIShadowObject,
		MsWMISimplePolicyTemplate,
		MsWMISom,
		MsWMIStringSetParam,
		MsWMIUintRangeParam,
		MsWMIUintSetParam,
		MsWMIUnknownRangeParam,
		MsWMIWMIGPO,
		NisMap,
		NisNetgroup,
		NisObject,
		NTDSConnection,
		NTDSDSA,
		NTDSDSARO,
		NTDSService,
		NTDSSiteSettings,
		NTFRSMember,
		NTFRSReplicaSet,
		NTFRSSettings,
		NTFRSSubscriber,
		NTFRSSubscriptions,
		OncRpc,
		Organization,
		OrganizationalPerson,
		OrganizationalRole,
		OrganizationalUnit,
		PackageRegistration,
		[EnumMember(Value = "person")]
		Person,
		PhysicalLocation,
		PKICertificateTemplate,
		PKIEnrollmentService,
		PosixAccount,
		PosixGroup,
		PrintQueue,
		QueryPolicy,
		RemoteMailRecipient,
		RemoteStorageServicePoint,
		ResidentialPerson,
		RFC822LocalPart,
		RIDManager,
		RIDSet,
		Room,
		RpcContainer,
		RpcEntry,
		RpcGroup,
		RpcProfile,
		RpcProfileElement,
		RpcServer,
		RpcServerElement,
		RRASAdministrationConnectionPoint,
		RRASAdministrationDictionary,
		SamDomain,
		SamDomainBase,
		SamServer,
		Secret,
		SecurityObject,
		SecurityPrincipal,
		Server,
		ServersContainer,
		ServiceAdministrationPoint,
		ServiceClass,
		ServiceConnectionPoint,
		ServiceInstance,
		ShadowAccount,
		SimpleSecurityObject,
		Site,
		SiteLink,
		SiteLinkBridge,
		SitesContainer,
		Storage,
		Subnet,
		SubnetContainer,
		SubSchema,
		Top,
		TrustedDomain,
		TypeLibrary,
		[EnumMember(Value = "user")]
		User,
		Volume
	}
}
