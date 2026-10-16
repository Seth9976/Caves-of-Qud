using System;
using System.Security;
using System.Security.Permissions;

namespace System.Net
{
	/// <summary>Specifies security actions to control <see cref="T:System.Net.Sockets.Socket" /> connections. This class cannot be inherited.</summary>
	// Token: 0x020006BA RID: 1722
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class SocketPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.SocketPermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" /> value.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="action" /> is not a valid <see cref="T:System.Security.Permissions.SecurityAction" /> value. </exception>
		// Token: 0x06003758 RID: 14168 RVA: 0x000A979A File Offset: 0x000A799A
		public SocketPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		/// <summary>Gets or sets the network access method that is allowed by this <see cref="T:System.Net.SocketPermissionAttribute" />.</summary>
		/// <returns>A string that contains the network access method that is allowed by this instance of <see cref="T:System.Net.SocketPermissionAttribute" />. Valid values are "Accept" and "Connect." </returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Net.SocketPermissionAttribute.Access" /> property is not null when you attempt to set the value. To specify more than one Access method, use an additional attribute declaration statement. </exception>
		// Token: 0x17000B87 RID: 2951
		// (get) Token: 0x06003759 RID: 14169 RVA: 0x000C23BF File Offset: 0x000C05BF
		// (set) Token: 0x0600375A RID: 14170 RVA: 0x000C23C7 File Offset: 0x000C05C7
		public string Access
		{
			get
			{
				return this.m_access;
			}
			set
			{
				if (this.m_access != null)
				{
					this.AlreadySet("Access");
				}
				this.m_access = value;
			}
		}

		/// <summary>Gets or sets the DNS host name or IP address that is specified by this <see cref="T:System.Net.SocketPermissionAttribute" />.</summary>
		/// <returns>A string that contains the DNS host name or IP address that is associated with this instance of <see cref="T:System.Net.SocketPermissionAttribute" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <see cref="P:System.Net.SocketPermissionAttribute.Host" /> is not null when you attempt to set the value. To specify more than one host, use an additional attribute declaration statement. </exception>
		// Token: 0x17000B88 RID: 2952
		// (get) Token: 0x0600375B RID: 14171 RVA: 0x000C23E3 File Offset: 0x000C05E3
		// (set) Token: 0x0600375C RID: 14172 RVA: 0x000C23EB File Offset: 0x000C05EB
		public string Host
		{
			get
			{
				return this.m_host;
			}
			set
			{
				if (this.m_host != null)
				{
					this.AlreadySet("Host");
				}
				this.m_host = value;
			}
		}

		/// <summary>Gets or sets the port number that is associated with this <see cref="T:System.Net.SocketPermissionAttribute" />.</summary>
		/// <returns>A string that contains the port number that is associated with this instance of <see cref="T:System.Net.SocketPermissionAttribute" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Net.SocketPermissionAttribute.Port" /> property is null when you attempt to set the value. To specify more than one port, use an additional attribute declaration statement. </exception>
		// Token: 0x17000B89 RID: 2953
		// (get) Token: 0x0600375D RID: 14173 RVA: 0x000C2407 File Offset: 0x000C0607
		// (set) Token: 0x0600375E RID: 14174 RVA: 0x000C240F File Offset: 0x000C060F
		public string Port
		{
			get
			{
				return this.m_port;
			}
			set
			{
				if (this.m_port != null)
				{
					this.AlreadySet("Port");
				}
				this.m_port = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Net.TransportType" /> that is specified by this <see cref="T:System.Net.SocketPermissionAttribute" />.</summary>
		/// <returns>A string that contains the <see cref="T:System.Net.TransportType" /> that is associated with this <see cref="T:System.Net.SocketPermissionAttribute" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <see cref="P:System.Net.SocketPermissionAttribute.Transport" /> is not null when you attempt to set the value. To specify more than one transport type, use an additional attribute declaration statement. </exception>
		// Token: 0x17000B8A RID: 2954
		// (get) Token: 0x0600375F RID: 14175 RVA: 0x000C242B File Offset: 0x000C062B
		// (set) Token: 0x06003760 RID: 14176 RVA: 0x000C2433 File Offset: 0x000C0633
		public string Transport
		{
			get
			{
				return this.m_transport;
			}
			set
			{
				if (this.m_transport != null)
				{
					this.AlreadySet("Transport");
				}
				this.m_transport = value;
			}
		}

		/// <summary>Creates and returns a new instance of the <see cref="T:System.Net.SocketPermission" /> class.</summary>
		/// <returns>An instance of the <see cref="T:System.Net.SocketPermission" /> class that corresponds to the security declaration.</returns>
		/// <exception cref="T:System.ArgumentException">One or more of the current instance's <see cref="P:System.Net.SocketPermissionAttribute.Access" />, <see cref="P:System.Net.SocketPermissionAttribute.Host" />, <see cref="P:System.Net.SocketPermissionAttribute.Transport" />, or <see cref="P:System.Net.SocketPermissionAttribute.Port" /> properties is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06003761 RID: 14177 RVA: 0x000C2450 File Offset: 0x000C0650
		public override IPermission CreatePermission()
		{
			if (base.Unrestricted)
			{
				return new SocketPermission(PermissionState.Unrestricted);
			}
			string text = string.Empty;
			if (this.m_access == null)
			{
				text += "Access, ";
			}
			if (this.m_host == null)
			{
				text += "Host, ";
			}
			if (this.m_port == null)
			{
				text += "Port, ";
			}
			if (this.m_transport == null)
			{
				text += "Transport, ";
			}
			if (text.Length > 0)
			{
				string text2 = global::Locale.GetText("The value(s) for {0} must be specified.");
				text = text.Substring(0, text.Length - 2);
				throw new ArgumentException(string.Format(text2, text));
			}
			int num = -1;
			NetworkAccess networkAccess;
			if (string.Compare(this.m_access, "Connect", true) == 0)
			{
				networkAccess = NetworkAccess.Connect;
			}
			else
			{
				if (string.Compare(this.m_access, "Accept", true) != 0)
				{
					throw new ArgumentException(string.Format(global::Locale.GetText("The parameter value for 'Access', '{1}, is invalid."), this.m_access));
				}
				networkAccess = NetworkAccess.Accept;
			}
			if (string.Compare(this.m_port, "All", true) != 0)
			{
				try
				{
					num = int.Parse(this.m_port);
				}
				catch
				{
					throw new ArgumentException(string.Format(global::Locale.GetText("The parameter value for 'Port', '{1}, is invalid."), this.m_port));
				}
				new IPEndPoint(1L, num);
			}
			TransportType transportType;
			try
			{
				transportType = (TransportType)Enum.Parse(typeof(TransportType), this.m_transport, true);
			}
			catch
			{
				throw new ArgumentException(string.Format(global::Locale.GetText("The parameter value for 'Transport', '{1}, is invalid."), this.m_transport));
			}
			SocketPermission socketPermission = new SocketPermission(PermissionState.None);
			socketPermission.AddPermission(networkAccess, transportType, this.m_host, num);
			return socketPermission;
		}

		// Token: 0x06003762 RID: 14178 RVA: 0x000C25F4 File Offset: 0x000C07F4
		internal void AlreadySet(string property)
		{
			throw new ArgumentException(string.Format(global::Locale.GetText("The parameter '{0}' can be set only once."), property), property);
		}

		// Token: 0x0400205B RID: 8283
		private string m_access;

		// Token: 0x0400205C RID: 8284
		private string m_host;

		// Token: 0x0400205D RID: 8285
		private string m_port;

		// Token: 0x0400205E RID: 8286
		private string m_transport;
	}
}
