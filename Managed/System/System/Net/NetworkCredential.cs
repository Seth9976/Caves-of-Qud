using System;
using System.Security;

namespace System.Net
{
	/// <summary>Provides credentials for password-based authentication schemes such as basic, digest, NTLM, and Kerberos authentication.</summary>
	// Token: 0x02000602 RID: 1538
	public class NetworkCredential : ICredentials, ICredentialsByHost
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkCredential" /> class.</summary>
		// Token: 0x06003086 RID: 12422 RVA: 0x000A575D File Offset: 0x000A395D
		public NetworkCredential()
			: this(string.Empty, string.Empty, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkCredential" /> class with the specified user name and password.</summary>
		/// <param name="userName">The user name associated with the credentials. </param>
		/// <param name="password">The password for the user name associated with the credentials. </param>
		// Token: 0x06003087 RID: 12423 RVA: 0x000A7035 File Offset: 0x000A5235
		public NetworkCredential(string userName, string password)
			: this(userName, password, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkCredential" /> class with the specified user name and password.</summary>
		/// <param name="userName">The user name associated with the credentials.</param>
		/// <param name="password">The password for the user name associated with the credentials.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Security.SecureString" /> class is not supported on this platform.</exception>
		// Token: 0x06003088 RID: 12424 RVA: 0x000A7044 File Offset: 0x000A5244
		public NetworkCredential(string userName, SecureString password)
			: this(userName, password, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkCredential" /> class with the specified user name, password, and domain.</summary>
		/// <param name="userName">The user name associated with the credentials. </param>
		/// <param name="password">The password for the user name associated with the credentials. </param>
		/// <param name="domain">The domain associated with these credentials. </param>
		// Token: 0x06003089 RID: 12425 RVA: 0x000A7053 File Offset: 0x000A5253
		public NetworkCredential(string userName, string password, string domain)
		{
			this.UserName = userName;
			this.Password = password;
			this.Domain = domain;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkCredential" /> class with the specified user name, password, and domain.</summary>
		/// <param name="userName">The user name associated with the credentials.</param>
		/// <param name="password">The password for the user name associated with the credentials.</param>
		/// <param name="domain">The domain associated with these credentials.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Security.SecureString" /> class is not supported on this platform.</exception>
		// Token: 0x0600308A RID: 12426 RVA: 0x000A7070 File Offset: 0x000A5270
		public NetworkCredential(string userName, SecureString password, string domain)
		{
			this.UserName = userName;
			this.SecurePassword = password;
			this.Domain = domain;
		}

		/// <summary>Gets or sets the user name associated with the credentials.</summary>
		/// <returns>The user name associated with the credentials.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x0600308B RID: 12427 RVA: 0x000A708D File Offset: 0x000A528D
		// (set) Token: 0x0600308C RID: 12428 RVA: 0x000A7095 File Offset: 0x000A5295
		public string UserName
		{
			get
			{
				return this.InternalGetUserName();
			}
			set
			{
				if (value == null)
				{
					this.m_userName = string.Empty;
					return;
				}
				this.m_userName = value;
			}
		}

		/// <summary>Gets or sets the password for the user name associated with the credentials.</summary>
		/// <returns>The password associated with the credentials. If this <see cref="T:System.Net.NetworkCredential" /> instance was initialized with the <paramref name="password" /> parameter set to null, then the <see cref="P:System.Net.NetworkCredential.Password" /> property will return an empty string.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x0600308D RID: 12429 RVA: 0x000A70AD File Offset: 0x000A52AD
		// (set) Token: 0x0600308E RID: 12430 RVA: 0x000A70B5 File Offset: 0x000A52B5
		public string Password
		{
			get
			{
				return this.InternalGetPassword();
			}
			set
			{
				this.m_password = UnsafeNclNativeMethods.SecureStringHelper.CreateSecureString(value);
			}
		}

		/// <summary>Gets or sets the password as a <see cref="T:System.Security.SecureString" /> instance.</summary>
		/// <returns>The password for the user name associated with the credentials.</returns>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Security.SecureString" /> class is not supported on this platform.</exception>
		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x0600308F RID: 12431 RVA: 0x000A70C3 File Offset: 0x000A52C3
		// (set) Token: 0x06003090 RID: 12432 RVA: 0x000A70D0 File Offset: 0x000A52D0
		public SecureString SecurePassword
		{
			get
			{
				return this.InternalGetSecurePassword().Copy();
			}
			set
			{
				if (value == null)
				{
					this.m_password = new SecureString();
					return;
				}
				this.m_password = value.Copy();
			}
		}

		/// <summary>Gets or sets the domain or computer name that verifies the credentials.</summary>
		/// <returns>The name of the domain associated with the credentials.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x06003091 RID: 12433 RVA: 0x000A70ED File Offset: 0x000A52ED
		// (set) Token: 0x06003092 RID: 12434 RVA: 0x000A70F5 File Offset: 0x000A52F5
		public string Domain
		{
			get
			{
				return this.InternalGetDomain();
			}
			set
			{
				if (value == null)
				{
					this.m_domain = string.Empty;
					return;
				}
				this.m_domain = value;
			}
		}

		// Token: 0x06003093 RID: 12435 RVA: 0x000A710D File Offset: 0x000A530D
		internal string InternalGetUserName()
		{
			return this.m_userName;
		}

		// Token: 0x06003094 RID: 12436 RVA: 0x000A7115 File Offset: 0x000A5315
		internal string InternalGetPassword()
		{
			return UnsafeNclNativeMethods.SecureStringHelper.CreateString(this.m_password);
		}

		// Token: 0x06003095 RID: 12437 RVA: 0x000A7122 File Offset: 0x000A5322
		internal SecureString InternalGetSecurePassword()
		{
			return this.m_password;
		}

		// Token: 0x06003096 RID: 12438 RVA: 0x000A712A File Offset: 0x000A532A
		internal string InternalGetDomain()
		{
			return this.m_domain;
		}

		// Token: 0x06003097 RID: 12439 RVA: 0x000A7134 File Offset: 0x000A5334
		internal string InternalGetDomainUserName()
		{
			string text = this.InternalGetDomain();
			if (text.Length != 0)
			{
				text += "\\";
			}
			return text + this.InternalGetUserName();
		}

		/// <summary>Returns an instance of the <see cref="T:System.Net.NetworkCredential" /> class for the specified Uniform Resource Identifier (URI) and authentication type.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkCredential" /> object.</returns>
		/// <param name="uri">The URI that the client provides authentication for. </param>
		/// <param name="authType">The type of authentication requested, as defined in the <see cref="P:System.Net.IAuthenticationModule.AuthenticationType" /> property. </param>
		// Token: 0x06003098 RID: 12440 RVA: 0x000075E1 File Offset: 0x000057E1
		public NetworkCredential GetCredential(Uri uri, string authType)
		{
			return this;
		}

		/// <summary>Returns an instance of the <see cref="T:System.Net.NetworkCredential" /> class for the specified host, port, and authentication type.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkCredential" /> for the specified host, port, and authentication protocol, or null if there are no credentials available for the specified host, port, and authentication protocol.</returns>
		/// <param name="host">The host computer that authenticates the client.</param>
		/// <param name="port">The port on the <paramref name="host" /> that the client communicates with.</param>
		/// <param name="authenticationType">The type of authentication requested, as defined in the <see cref="P:System.Net.IAuthenticationModule.AuthenticationType" /> property. </param>
		// Token: 0x06003099 RID: 12441 RVA: 0x000075E1 File Offset: 0x000057E1
		public NetworkCredential GetCredential(string host, int port, string authenticationType)
		{
			return this;
		}

		// Token: 0x04001C50 RID: 7248
		private string m_domain;

		// Token: 0x04001C51 RID: 7249
		private string m_userName;

		// Token: 0x04001C52 RID: 7250
		private SecureString m_password;
	}
}
