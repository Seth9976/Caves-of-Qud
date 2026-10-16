using System;
using System.Collections.Specialized;
using System.IO;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Security.Principal;
using System.Web.Configuration;
using System.Web.Util;
using Unity;

namespace System.Web.Security
{
	/// <summary>Represents security information for the current HTTP request, including role membership. This class cannot be inherited.</summary>
	// Token: 0x020004C5 RID: 1221
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[Serializable]
	public class RolePrincipal : IPrincipal, ISerializable
	{
		/// <summary>Instantiates a <see cref="T:System.Web.Security.RolePrincipal" /> object for the specified <paramref name="identity" />.</summary>
		/// <param name="identity">The user identity to create the <see cref="T:System.Web.Security.RolePrincipal" /> for.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="identity" /> is null.</exception>
		// Token: 0x06003723 RID: 14115 RVA: 0x0008E180 File Offset: 0x0008C380
		public RolePrincipal(IIdentity identity)
		{
			this._version = 1;
			base..ctor();
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			this._identity = identity;
			this._cookiePath = this.RoleManagerConfig.CookiePath;
			this._issueDate = DateTime.Now;
			this._expireDate = this._issueDate.Add(this.RoleManagerConfig.CookieTimeout);
		}

		/// <summary>Instantiates a <see cref="T:System.Web.Security.RolePrincipal" /> object for the specified <paramref name="identity" /> with role information from the specified <paramref name="encryptedTicket" />.</summary>
		/// <param name="identity">The user identity to create the <see cref="T:System.Web.Security.RolePrincipal" /> for.</param>
		/// <param name="encryptedTicket">A string that contains encrypted role information.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="identity" /> is null.-or-<paramref name="encryptedTicket" /> is null.</exception>
		// Token: 0x06003724 RID: 14116 RVA: 0x0008E1E7 File Offset: 0x0008C3E7
		public RolePrincipal(IIdentity identity, string encryptedTicket)
			: this(identity)
		{
			this.DecryptTicket(encryptedTicket);
		}

		/// <summary>Instantiates a <see cref="T:System.Web.Security.RolePrincipal" /> object for the specified <paramref name="identity" /> using the specified <paramref name="providerName" />.</summary>
		/// <param name="providerName">The name of the role provider for the user.</param>
		/// <param name="identity">The user identity to create the <see cref="T:System.Web.Security.RolePrincipal" /> for.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="identity" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="providerName" /> is null.-or-<paramref name="providerName" /> refers to a role provider that does not exist in the configuration for the application.</exception>
		// Token: 0x06003725 RID: 14117 RVA: 0x0008E1F7 File Offset: 0x0008C3F7
		public RolePrincipal(string providerName, IIdentity identity)
			: this(identity)
		{
			if (providerName == null)
			{
				throw new ArgumentNullException("providerName");
			}
			this._providerName = providerName;
		}

		/// <summary>Instantiates a <see cref="T:System.Web.Security.RolePrincipal" /> object for the specified <paramref name="identity" /> using the specified <paramref name="providerName" /> and role information from the specified <paramref name="encryptedTicket" />.</summary>
		/// <param name="providerName">The name of the role provider for the user.</param>
		/// <param name="identity">The user identity to create the <see cref="T:System.Web.Security.RolePrincipal" /> for.</param>
		/// <param name="encryptedTicket">A string that contains encrypted role information.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="identity" /> is null.-or-<paramref name="encryptedTicket" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="providerName" /> is null.-or-<paramref name="providerName" /> refers to a role provider that does not exist in the configuration for the application.</exception>
		// Token: 0x06003726 RID: 14118 RVA: 0x0008E215 File Offset: 0x0008C415
		public RolePrincipal(string providerName, IIdentity identity, string encryptedTicket)
			: this(providerName, identity)
		{
			this.DecryptTicket(encryptedTicket);
		}

		/// <summary>Gets a list of roles that the <see cref="T:System.Web.Security.RolePrincipal" /> is a member of.</summary>
		/// <returns>The list of roles that the <see cref="T:System.Web.Security.RolePrincipal" /> is a member of.</returns>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">The <see cref="P:System.Web.Security.RolePrincipal.Identity" /> property is null.</exception>
		// Token: 0x06003727 RID: 14119 RVA: 0x0008E228 File Offset: 0x0008C428
		public string[] GetRoles()
		{
			if (!this._identity.IsAuthenticated)
			{
				return new string[0];
			}
			if (!this.IsRoleListCached || this.Expired)
			{
				this._cachedArray = this.Provider.GetRolesForUser(this._identity.Name);
				this._cachedRoles = new HybridDictionary(true);
				foreach (string text in this._cachedArray)
				{
					this._cachedRoles.Add(text, text);
				}
				this._listChanged = true;
			}
			return this._cachedArray;
		}

		/// <summary>Gets a value indicating whether the user represented by the <see cref="T:System.Web.Security.RolePrincipal" /> is in the specified role.</summary>
		/// <returns>true if user represented by the <see cref="T:System.Web.Security.RolePrincipal" /> is in the specified role; otherwise, false.</returns>
		/// <param name="role">The role to search for.</param>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">The <see cref="P:System.Web.Security.RolePrincipal.Identity" /> property is null.</exception>
		// Token: 0x06003728 RID: 14120 RVA: 0x0008E2B4 File Offset: 0x0008C4B4
		public bool IsInRole(string role)
		{
			if (!this._identity.IsAuthenticated)
			{
				return false;
			}
			this.GetRoles();
			return this._cachedRoles[role] != null;
		}

		/// <summary>Returns the role information cached with the <see cref="T:System.Web.Security.RolePrincipal" /> object encrypted based on the <see cref="P:System.Web.Security.Roles.CookieProtectionValue" />.</summary>
		/// <returns>The role information cached with the <see cref="T:System.Web.Security.RolePrincipal" /> object encrypted based on the <see cref="P:System.Web.Security.Roles.CookieProtectionValue" />.</returns>
		// Token: 0x06003729 RID: 14121 RVA: 0x0008E2DC File Offset: 0x0008C4DC
		public string ToEncryptedTicket()
		{
			string text = string.Join(",", this.GetRoles());
			string cookiePath = this.RoleManagerConfig.CookiePath;
			int num = text.Length + cookiePath.Length + 64;
			if (this._cachedArray.Length > Roles.MaxCachedResults)
			{
				return null;
			}
			MemoryStream memoryStream = new MemoryStream(num);
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			binaryWriter.Write(this.Version);
			binaryWriter.Write(DateTime.Now.Ticks);
			binaryWriter.Write(this._expireDate.Ticks);
			binaryWriter.Write(cookiePath);
			binaryWriter.Write(text);
			CookieProtection cookieProtection = this.RoleManagerConfig.CookieProtection;
			byte[] array = memoryStream.GetBuffer();
			if (cookieProtection == CookieProtection.All)
			{
				array = MachineKeySectionUtils.EncryptSign(this.MachineConfig, array);
			}
			else if (cookieProtection == CookieProtection.Encryption)
			{
				array = MachineKeySectionUtils.Encrypt(this.MachineConfig, array);
			}
			else if (cookieProtection == CookieProtection.Validation)
			{
				array = MachineKeySectionUtils.Sign(this.MachineConfig, array);
			}
			return RolePrincipal.GetBase64FromBytes(array, 0, array.Length);
		}

		// Token: 0x0600372A RID: 14122 RVA: 0x0008E3D4 File Offset: 0x0008C5D4
		private void DecryptTicket(string encryptedTicket)
		{
			if (encryptedTicket == null || encryptedTicket == string.Empty)
			{
				throw new ArgumentException("Invalid encrypted ticket", "encryptedTicket");
			}
			byte[] bytesFromBase = RolePrincipal.GetBytesFromBase64(encryptedTicket);
			byte[] array = null;
			CookieProtection cookieProtection = this.RoleManagerConfig.CookieProtection;
			if (cookieProtection == CookieProtection.All)
			{
				array = MachineKeySectionUtils.VerifyDecrypt(this.MachineConfig, bytesFromBase);
			}
			else if (cookieProtection == CookieProtection.Encryption)
			{
				array = MachineKeySectionUtils.Decrypt(this.MachineConfig, bytesFromBase);
			}
			else if (cookieProtection == CookieProtection.Validation)
			{
				array = MachineKeySectionUtils.Verify(this.MachineConfig, bytesFromBase);
			}
			if (array == null)
			{
				throw new HttpException("ticket validation failed");
			}
			BinaryReader binaryReader = new BinaryReader(new MemoryStream(array));
			this._version = binaryReader.ReadInt32();
			this._issueDate = new DateTime(binaryReader.ReadInt64());
			this._expireDate = new DateTime(binaryReader.ReadInt64());
			this._cookiePath = binaryReader.ReadString();
			string text = binaryReader.ReadString();
			if (!this.Expired)
			{
				this.InitializeRoles(text);
				if (Roles.CookieSlidingExpiration && this._expireDate - DateTime.Now < TimeSpan.FromTicks(this.RoleManagerConfig.CookieTimeout.Ticks / 2L))
				{
					this._issueDate = DateTime.Now;
					this._expireDate = DateTime.Now.Add(this.RoleManagerConfig.CookieTimeout);
					this.SetDirty();
					return;
				}
			}
			else
			{
				this._issueDate = DateTime.Now;
				this._expireDate = this._issueDate.Add(this.RoleManagerConfig.CookieTimeout);
			}
		}

		// Token: 0x0600372B RID: 14123 RVA: 0x0008E550 File Offset: 0x0008C750
		private void InitializeRoles(string decryptedRoles)
		{
			this._cachedArray = decryptedRoles.Split(',', StringSplitOptions.None);
			this._cachedRoles = new HybridDictionary(true);
			foreach (string text in this._cachedArray)
			{
				this._cachedRoles.Add(text, text);
			}
		}

		/// <summary>Gets a value indicating whether the list of role names cached with the <see cref="T:System.Web.Security.RolePrincipal" /> object has been modified.</summary>
		/// <returns>true if the list of role names cached with the <see cref="T:System.Web.Security.RolePrincipal" /> object has been modified; otherwise, false.</returns>
		// Token: 0x17001156 RID: 4438
		// (get) Token: 0x0600372C RID: 14124 RVA: 0x0008E59E File Offset: 0x0008C79E
		public bool CachedListChanged
		{
			get
			{
				return this._listChanged;
			}
		}

		/// <summary>Gets the path for the cached role names cookie.</summary>
		/// <returns>The path of the cookie where role names are cached. The default is /.</returns>
		// Token: 0x17001157 RID: 4439
		// (get) Token: 0x0600372D RID: 14125 RVA: 0x0008E5A6 File Offset: 0x0008C7A6
		public string CookiePath
		{
			get
			{
				return this._cookiePath;
			}
		}

		/// <summary>Gets a value indicating whether the roles cookie has expired.</summary>
		/// <returns>true if the roles cookie has expired; otherwise, false.</returns>
		// Token: 0x17001158 RID: 4440
		// (get) Token: 0x0600372E RID: 14126 RVA: 0x0008E5AE File Offset: 0x0008C7AE
		public bool Expired
		{
			get
			{
				return this.ExpireDate < DateTime.Now;
			}
		}

		/// <summary>Gets the date and time when the roles cookie will expire.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> value when the roles cookie will expire.</returns>
		// Token: 0x17001159 RID: 4441
		// (get) Token: 0x0600372F RID: 14127 RVA: 0x0008E5C0 File Offset: 0x0008C7C0
		public DateTime ExpireDate
		{
			get
			{
				return this._expireDate;
			}
		}

		/// <summary>Gets the security identity for the current HTTP request.</summary>
		/// <returns>The security identity for the current HTTP request.</returns>
		// Token: 0x1700115A RID: 4442
		// (get) Token: 0x06003730 RID: 14128 RVA: 0x0008E5C8 File Offset: 0x0008C7C8
		public IIdentity Identity
		{
			get
			{
				return this._identity;
			}
		}

		/// <summary>Gets a value indicating whether the list of roles for the user has been cached in a cookie.</summary>
		/// <returns>true if role names are cached in a cookie; otherwise, false.</returns>
		// Token: 0x1700115B RID: 4443
		// (get) Token: 0x06003731 RID: 14129 RVA: 0x0008E5D0 File Offset: 0x0008C7D0
		public bool IsRoleListCached
		{
			get
			{
				return this._cachedRoles != null && this.RoleManagerConfig.CacheRolesInCookie;
			}
		}

		/// <summary>Gets the date and time that the roles cookie was issued.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> that the roles cookie was issued.</returns>
		// Token: 0x1700115C RID: 4444
		// (get) Token: 0x06003732 RID: 14130 RVA: 0x0008E5E7 File Offset: 0x0008C7E7
		public DateTime IssueDate
		{
			get
			{
				return this._issueDate;
			}
		}

		/// <summary>Gets the name of the role provider that stores and retrieves role information for the user.</summary>
		/// <returns>The name of the role provider that stores and retrieves role information for the user.</returns>
		// Token: 0x1700115D RID: 4445
		// (get) Token: 0x06003733 RID: 14131 RVA: 0x0008E5EF File Offset: 0x0008C7EF
		public string ProviderName
		{
			get
			{
				if (!string.IsNullOrEmpty(this._providerName))
				{
					return this._providerName;
				}
				return this.Provider.Name;
			}
		}

		/// <summary>Gets the version number of the roles cookie.</summary>
		/// <returns>The version number of the roles cookie.</returns>
		// Token: 0x1700115E RID: 4446
		// (get) Token: 0x06003734 RID: 14132 RVA: 0x0008E610 File Offset: 0x0008C810
		public int Version
		{
			get
			{
				return this._version;
			}
		}

		// Token: 0x1700115F RID: 4447
		// (get) Token: 0x06003735 RID: 14133 RVA: 0x0008E618 File Offset: 0x0008C818
		private RoleProvider Provider
		{
			get
			{
				if (string.IsNullOrEmpty(this._providerName))
				{
					return Roles.Provider;
				}
				return Roles.Providers[this._providerName];
			}
		}

		/// <summary>Marks the cached role list as having been changed.</summary>
		// Token: 0x06003736 RID: 14134 RVA: 0x0008E63D File Offset: 0x0008C83D
		public void SetDirty()
		{
			this._listChanged = true;
			this._cachedRoles = null;
			this._cachedArray = null;
		}

		// Token: 0x06003737 RID: 14135 RVA: 0x0008E654 File Offset: 0x0008C854
		private static string GetBase64FromBytes(byte[] bytes, int offset, int len)
		{
			return Convert.ToBase64String(bytes, offset, len);
		}

		// Token: 0x06003738 RID: 14136 RVA: 0x0008E65E File Offset: 0x0008C85E
		private static byte[] GetBytesFromBase64(string base64String)
		{
			return Convert.FromBase64String(base64String);
		}

		// Token: 0x17001160 RID: 4448
		// (get) Token: 0x06003739 RID: 14137 RVA: 0x0008E666 File Offset: 0x0008C866
		private RoleManagerSection RoleManagerConfig
		{
			get
			{
				return (RoleManagerSection)WebConfigurationManager.GetSection("system.web/roleManager");
			}
		}

		// Token: 0x17001161 RID: 4449
		// (get) Token: 0x0600373A RID: 14138 RVA: 0x0008E677 File Offset: 0x0008C877
		private MachineKeySection MachineConfig
		{
			get
			{
				return (MachineKeySection)WebConfigurationManager.GetSection("system.web/machineKey");
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Security.RolePrincipal" /> class using information that is contained in the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object and using the specified streaming context.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object to populate with data.</param>
		/// <param name="context">The destination for this serialization.</param>
		// Token: 0x0600373B RID: 14139 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected RolePrincipal(SerializationInfo info, StreamingContext context)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the data that is required in order to serialize the target object using the specified streaming context.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object to populate with data.</param>
		/// <param name="context">The destination for this serialization.</param>
		// Token: 0x0600373C RID: 14140 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Runtime.Serialization.ISerializationSurrogate.GetObjectData(System.Object,System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)" />.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="context">The destination for this serialization.</param>
		// Token: 0x0600373D RID: 14141 RVA: 0x0000B3D8 File Offset: 0x000095D8
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001DE8 RID: 7656
		private IIdentity _identity;

		// Token: 0x04001DE9 RID: 7657
		private bool _listChanged;

		// Token: 0x04001DEA RID: 7658
		private string[] _cachedArray;

		// Token: 0x04001DEB RID: 7659
		private HybridDictionary _cachedRoles;

		// Token: 0x04001DEC RID: 7660
		private readonly string _providerName;

		// Token: 0x04001DED RID: 7661
		private int _version;

		// Token: 0x04001DEE RID: 7662
		private string _cookiePath;

		// Token: 0x04001DEF RID: 7663
		private DateTime _issueDate;

		// Token: 0x04001DF0 RID: 7664
		private DateTime _expireDate;
	}
}
