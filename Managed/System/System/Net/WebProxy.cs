using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Net.NetworkInformation;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text.RegularExpressions;
using Mono.Net;

namespace System.Net
{
	/// <summary>Contains HTTP proxy settings for the <see cref="T:System.Net.WebRequest" /> class.</summary>
	// Token: 0x02000660 RID: 1632
	[Serializable]
	public class WebProxy : IAutoWebProxy, IWebProxy, ISerializable
	{
		/// <summary>Initializes an empty instance of the <see cref="T:System.Net.WebProxy" /> class.</summary>
		// Token: 0x0600336A RID: 13162 RVA: 0x000B3388 File Offset: 0x000B1588
		public WebProxy()
			: this(null, false, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebProxy" /> class from the specified <see cref="T:System.Uri" /> instance.</summary>
		/// <param name="Address">A <see cref="T:System.Uri" /> instance that contains the address of the proxy server. </param>
		// Token: 0x0600336B RID: 13163 RVA: 0x000B3394 File Offset: 0x000B1594
		public WebProxy(Uri Address)
			: this(Address, false, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebProxy" /> class with the <see cref="T:System.Uri" /> instance and bypass setting.</summary>
		/// <param name="Address">A <see cref="T:System.Uri" /> instance that contains the address of the proxy server. </param>
		/// <param name="BypassOnLocal">true to bypass the proxy for local addresses; otherwise, false. </param>
		// Token: 0x0600336C RID: 13164 RVA: 0x000B33A0 File Offset: 0x000B15A0
		public WebProxy(Uri Address, bool BypassOnLocal)
			: this(Address, BypassOnLocal, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebProxy" /> class with the specified <see cref="T:System.Uri" /> instance, bypass setting, and list of URIs to bypass.</summary>
		/// <param name="Address">A <see cref="T:System.Uri" /> instance that contains the address of the proxy server. </param>
		/// <param name="BypassOnLocal">true to bypass the proxy for local addresses; otherwise, false. </param>
		/// <param name="BypassList">An array of regular expression strings that contains the URIs of the servers to bypass. </param>
		// Token: 0x0600336D RID: 13165 RVA: 0x000B33AC File Offset: 0x000B15AC
		public WebProxy(Uri Address, bool BypassOnLocal, string[] BypassList)
			: this(Address, BypassOnLocal, BypassList, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebProxy" /> class with the specified <see cref="T:System.Uri" /> instance, bypass setting, list of URIs to bypass, and credentials.</summary>
		/// <param name="Address">A <see cref="T:System.Uri" /> instance that contains the address of the proxy server. </param>
		/// <param name="BypassOnLocal">true to bypass the proxy for local addresses; otherwise, false. </param>
		/// <param name="BypassList">An array of regular expression strings that contains the URIs of the servers to bypass. </param>
		/// <param name="Credentials">An <see cref="T:System.Net.ICredentials" /> instance to submit to the proxy server for authentication. </param>
		// Token: 0x0600336E RID: 13166 RVA: 0x000B33B8 File Offset: 0x000B15B8
		public WebProxy(Uri Address, bool BypassOnLocal, string[] BypassList, ICredentials Credentials)
		{
			this._ProxyAddress = Address;
			this._BypassOnLocal = BypassOnLocal;
			if (BypassList != null)
			{
				this._BypassList = new ArrayList(BypassList);
				this.UpdateRegExList(true);
			}
			this._Credentials = Credentials;
			this.m_EnableAutoproxy = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebProxy" /> class with the specified host and port number.</summary>
		/// <param name="Host">The name of the proxy host. </param>
		/// <param name="Port">The port number on <paramref name="Host" /> to use. </param>
		/// <exception cref="T:System.UriFormatException">The URI formed by combining <paramref name="Host" /> and <paramref name="Port" /> is not a valid URI. </exception>
		// Token: 0x0600336F RID: 13167 RVA: 0x000B33F3 File Offset: 0x000B15F3
		public WebProxy(string Host, int Port)
			: this(new Uri("http://" + Host + ":" + Port.ToString(CultureInfo.InvariantCulture)), false, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebProxy" /> class with the specified URI.</summary>
		/// <param name="Address">The URI of the proxy server. </param>
		/// <exception cref="T:System.UriFormatException">
		///   <paramref name="Address" /> is an invalid URI. </exception>
		// Token: 0x06003370 RID: 13168 RVA: 0x000B341F File Offset: 0x000B161F
		public WebProxy(string Address)
			: this(WebProxy.CreateProxyUri(Address), false, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebProxy" /> class with the specified URI and bypass setting.</summary>
		/// <param name="Address">The URI of the proxy server. </param>
		/// <param name="BypassOnLocal">true to bypass the proxy for local addresses; otherwise, false. </param>
		/// <exception cref="T:System.UriFormatException">
		///   <paramref name="Address" /> is an invalid URI. </exception>
		// Token: 0x06003371 RID: 13169 RVA: 0x000B3430 File Offset: 0x000B1630
		public WebProxy(string Address, bool BypassOnLocal)
			: this(WebProxy.CreateProxyUri(Address), BypassOnLocal, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebProxy" /> class with the specified URI, bypass setting, and list of URIs to bypass.</summary>
		/// <param name="Address">The URI of the proxy server. </param>
		/// <param name="BypassOnLocal">true to bypass the proxy for local addresses; otherwise, false. </param>
		/// <param name="BypassList">An array of regular expression strings that contain the URIs of the servers to bypass. </param>
		/// <exception cref="T:System.UriFormatException">
		///   <paramref name="Address" /> is an invalid URI. </exception>
		// Token: 0x06003372 RID: 13170 RVA: 0x000B3441 File Offset: 0x000B1641
		public WebProxy(string Address, bool BypassOnLocal, string[] BypassList)
			: this(WebProxy.CreateProxyUri(Address), BypassOnLocal, BypassList, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebProxy" /> class with the specified URI, bypass setting, list of URIs to bypass, and credentials.</summary>
		/// <param name="Address">The URI of the proxy server. </param>
		/// <param name="BypassOnLocal">true to bypass the proxy for local addresses; otherwise, false. </param>
		/// <param name="BypassList">An array of regular expression strings that contains the URIs of the servers to bypass. </param>
		/// <param name="Credentials">An <see cref="T:System.Net.ICredentials" /> instance to submit to the proxy server for authentication. </param>
		/// <exception cref="T:System.UriFormatException">
		///   <paramref name="Address" /> is an invalid URI. </exception>
		// Token: 0x06003373 RID: 13171 RVA: 0x000B3452 File Offset: 0x000B1652
		public WebProxy(string Address, bool BypassOnLocal, string[] BypassList, ICredentials Credentials)
			: this(WebProxy.CreateProxyUri(Address), BypassOnLocal, BypassList, Credentials)
		{
		}

		/// <summary>Gets or sets the address of the proxy server.</summary>
		/// <returns>A <see cref="T:System.Uri" /> instance that contains the address of the proxy server.</returns>
		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x06003374 RID: 13172 RVA: 0x000B3464 File Offset: 0x000B1664
		// (set) Token: 0x06003375 RID: 13173 RVA: 0x000B346C File Offset: 0x000B166C
		public Uri Address
		{
			get
			{
				return this._ProxyAddress;
			}
			set
			{
				this._UseRegistry = false;
				this.DeleteScriptEngine();
				this._ProxyHostAddresses = null;
				this._ProxyAddress = value;
			}
		}

		// Token: 0x17000A64 RID: 2660
		// (set) Token: 0x06003376 RID: 13174 RVA: 0x000B3489 File Offset: 0x000B1689
		internal bool AutoDetect
		{
			set
			{
				if (this.ScriptEngine == null)
				{
					this.ScriptEngine = new AutoWebProxyScriptEngine(this, false);
				}
				this.ScriptEngine.AutomaticallyDetectSettings = value;
			}
		}

		// Token: 0x17000A65 RID: 2661
		// (set) Token: 0x06003377 RID: 13175 RVA: 0x000B34AC File Offset: 0x000B16AC
		internal Uri ScriptLocation
		{
			set
			{
				if (this.ScriptEngine == null)
				{
					this.ScriptEngine = new AutoWebProxyScriptEngine(this, false);
				}
				this.ScriptEngine.AutomaticConfigurationScript = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether to bypass the proxy server for local addresses.</summary>
		/// <returns>true to bypass the proxy server for local addresses; otherwise, false. The default value is false.</returns>
		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x06003378 RID: 13176 RVA: 0x000B34CF File Offset: 0x000B16CF
		// (set) Token: 0x06003379 RID: 13177 RVA: 0x000B34D7 File Offset: 0x000B16D7
		public bool BypassProxyOnLocal
		{
			get
			{
				return this._BypassOnLocal;
			}
			set
			{
				this._UseRegistry = false;
				this.DeleteScriptEngine();
				this._BypassOnLocal = value;
			}
		}

		/// <summary>Gets or sets an array of addresses that do not use the proxy server.</summary>
		/// <returns>An array that contains a list of regular expressions that describe URIs that do not use the proxy server when accessed.</returns>
		// Token: 0x17000A67 RID: 2663
		// (get) Token: 0x0600337A RID: 13178 RVA: 0x000B34ED File Offset: 0x000B16ED
		// (set) Token: 0x0600337B RID: 13179 RVA: 0x000B351C File Offset: 0x000B171C
		public string[] BypassList
		{
			get
			{
				if (this._BypassList == null)
				{
					this._BypassList = new ArrayList();
				}
				return (string[])this._BypassList.ToArray(typeof(string));
			}
			set
			{
				this._UseRegistry = false;
				this.DeleteScriptEngine();
				this._BypassList = new ArrayList(value);
				this.UpdateRegExList(true);
			}
		}

		/// <summary>Gets or sets the credentials to submit to the proxy server for authentication.</summary>
		/// <returns>An <see cref="T:System.Net.ICredentials" /> instance that contains the credentials to submit to the proxy server for authentication.</returns>
		/// <exception cref="T:System.InvalidOperationException">You attempted to set this property when the <see cref="P:System.Net.WebProxy.UseDefaultCredentials" />  property was set to true. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x0600337C RID: 13180 RVA: 0x000B353E File Offset: 0x000B173E
		// (set) Token: 0x0600337D RID: 13181 RVA: 0x000B3546 File Offset: 0x000B1746
		public ICredentials Credentials
		{
			get
			{
				return this._Credentials;
			}
			set
			{
				this._Credentials = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that controls whether the <see cref="P:System.Net.CredentialCache.DefaultCredentials" /> are sent with requests.</summary>
		/// <returns>true if the default credentials are used; otherwise, false. The default value is false.</returns>
		/// <exception cref="T:System.InvalidOperationException">You attempted to set this property when the <see cref="P:System.Net.WebProxy.Credentials" /> property contains credentials other than the default credentials. For more information, see the Remarks section.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="USERNAME" />
		/// </PermissionSet>
		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x0600337E RID: 13182 RVA: 0x000B354F File Offset: 0x000B174F
		// (set) Token: 0x0600337F RID: 13183 RVA: 0x000B3561 File Offset: 0x000B1761
		public bool UseDefaultCredentials
		{
			get
			{
				return this.Credentials is SystemNetworkCredential;
			}
			set
			{
				this._Credentials = (value ? CredentialCache.DefaultCredentials : null);
			}
		}

		/// <summary>Gets a list of addresses that do not use the proxy server.</summary>
		/// <returns>An <see cref="T:System.Collections.ArrayList" /> that contains a list of <see cref="P:System.Net.WebProxy.BypassList" /> arrays that represents URIs that do not use the proxy server when accessed.</returns>
		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x06003380 RID: 13184 RVA: 0x000B3574 File Offset: 0x000B1774
		public ArrayList BypassArrayList
		{
			get
			{
				if (this._BypassList == null)
				{
					this._BypassList = new ArrayList();
				}
				return this._BypassList;
			}
		}

		// Token: 0x06003381 RID: 13185 RVA: 0x000B358F File Offset: 0x000B178F
		internal void CheckForChanges()
		{
			if (this.ScriptEngine != null)
			{
				this.ScriptEngine.CheckForChanges();
			}
		}

		/// <summary>Returns the proxied URI for a request.</summary>
		/// <returns>The <see cref="T:System.Uri" /> instance of the Internet resource, if the resource is on the bypass list; otherwise, the <see cref="T:System.Uri" /> instance of the proxy.</returns>
		/// <param name="destination">The <see cref="T:System.Uri" /> instance of the requested Internet resource. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="destination" /> parameter is null. </exception>
		// Token: 0x06003382 RID: 13186 RVA: 0x000B35A4 File Offset: 0x000B17A4
		public Uri GetProxy(Uri destination)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			Uri uri;
			if (this.GetProxyAuto(destination, out uri))
			{
				return uri;
			}
			if (this.IsBypassedManual(destination))
			{
				return destination;
			}
			Hashtable proxyHostAddresses = this._ProxyHostAddresses;
			Uri uri2 = ((proxyHostAddresses != null) ? (proxyHostAddresses[destination.Scheme] as Uri) : this._ProxyAddress);
			if (!(uri2 != null))
			{
				return destination;
			}
			return uri2;
		}

		// Token: 0x06003383 RID: 13187 RVA: 0x000B360D File Offset: 0x000B180D
		private static Uri CreateProxyUri(string address)
		{
			if (address == null)
			{
				return null;
			}
			if (address.IndexOf("://") == -1)
			{
				address = "http://" + address;
			}
			return new Uri(address);
		}

		// Token: 0x06003384 RID: 13188 RVA: 0x000B3638 File Offset: 0x000B1838
		private void UpdateRegExList(bool canThrow)
		{
			Regex[] array = null;
			ArrayList bypassList = this._BypassList;
			try
			{
				if (bypassList != null && bypassList.Count > 0)
				{
					array = new Regex[bypassList.Count];
					for (int i = 0; i < bypassList.Count; i++)
					{
						array[i] = new Regex((string)bypassList[i], RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
					}
				}
			}
			catch
			{
				if (!canThrow)
				{
					this._RegExBypassList = null;
					return;
				}
				throw;
			}
			this._RegExBypassList = array;
		}

		// Token: 0x06003385 RID: 13189 RVA: 0x000B36B8 File Offset: 0x000B18B8
		private bool IsMatchInBypassList(Uri input)
		{
			this.UpdateRegExList(false);
			if (this._RegExBypassList == null)
			{
				return false;
			}
			string text = input.Scheme + "://" + input.Host + ((!input.IsDefaultPort) ? (":" + input.Port.ToString()) : "");
			for (int i = 0; i < this._BypassList.Count; i++)
			{
				if (this._RegExBypassList[i].IsMatch(text))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003386 RID: 13190 RVA: 0x000B3740 File Offset: 0x000B1940
		private bool IsLocal(Uri host)
		{
			string host2 = host.Host;
			IPAddress ipaddress;
			if (IPAddress.TryParse(host2, out ipaddress))
			{
				return IPAddress.IsLoopback(ipaddress) || NclUtilities.IsAddressLocal(ipaddress);
			}
			int num = host2.IndexOf('.');
			if (num == -1)
			{
				return true;
			}
			string text = "." + IPGlobalProperties.InternalGetIPGlobalProperties().DomainName;
			return text != null && text.Length == host2.Length - num && string.Compare(text, 0, host2, num, text.Length, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x06003387 RID: 13191 RVA: 0x000B37BC File Offset: 0x000B19BC
		private bool IsLocalInProxyHash(Uri host)
		{
			Hashtable proxyHostAddresses = this._ProxyHostAddresses;
			return proxyHostAddresses != null && (Uri)proxyHostAddresses[host.Scheme] == null;
		}

		/// <summary>Indicates whether to use the proxy server for the specified host.</summary>
		/// <returns>true if the proxy server should not be used for <paramref name="host" />; otherwise, false.</returns>
		/// <param name="host">The <see cref="T:System.Uri" /> instance of the host to check for proxy use. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="host" /> parameter is null. </exception>
		// Token: 0x06003388 RID: 13192 RVA: 0x000B37F0 File Offset: 0x000B19F0
		public bool IsBypassed(Uri host)
		{
			if (host == null)
			{
				throw new ArgumentNullException("host");
			}
			bool flag;
			if (this.IsBypassedAuto(host, out flag))
			{
				return flag;
			}
			return this.IsBypassedManual(host);
		}

		// Token: 0x06003389 RID: 13193 RVA: 0x000B3828 File Offset: 0x000B1A28
		private bool IsBypassedManual(Uri host)
		{
			return host.IsLoopback || (this._ProxyAddress == null && this._ProxyHostAddresses == null) || (this._BypassOnLocal && this.IsLocal(host)) || this.IsMatchInBypassList(host) || this.IsLocalInProxyHash(host);
		}

		/// <summary>Reads the Internet Explorer nondynamic proxy settings.</summary>
		/// <returns>A <see cref="T:System.Net.WebProxy" /> instance that contains the nondynamic proxy settings from Internet Explorer 5.5 and later.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600338A RID: 13194 RVA: 0x000B3878 File Offset: 0x000B1A78
		[Obsolete("This method has been deprecated. Please use the proxy selected for you by default. http://go.microsoft.com/fwlink/?linkid=14202")]
		public static WebProxy GetDefaultProxy()
		{
			return new WebProxy(true);
		}

		/// <summary>Initializes an instance of the <see cref="T:System.Net.WebProxy" /> class using previously serialized content.</summary>
		/// <param name="serializationInfo">The serialization data. </param>
		/// <param name="streamingContext">The context for the serialized data. </param>
		// Token: 0x0600338B RID: 13195 RVA: 0x000B3880 File Offset: 0x000B1A80
		protected WebProxy(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			bool flag = false;
			try
			{
				flag = serializationInfo.GetBoolean("_UseRegistry");
			}
			catch
			{
			}
			if (flag)
			{
				this.UnsafeUpdateFromRegistry();
				return;
			}
			this._ProxyAddress = (Uri)serializationInfo.GetValue("_ProxyAddress", typeof(Uri));
			this._BypassOnLocal = serializationInfo.GetBoolean("_BypassOnLocal");
			this._BypassList = (ArrayList)serializationInfo.GetValue("_BypassList", typeof(ArrayList));
			try
			{
				this.UseDefaultCredentials = serializationInfo.GetBoolean("_UseDefaultCredentials");
			}
			catch
			{
			}
		}

		/// <summary>Creates the serialization data and context that are used by the system to serialize a <see cref="T:System.Net.WebProxy" /> object.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object to populate with data. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure that indicates the destination for this serialization. </param>
		// Token: 0x0600338C RID: 13196 RVA: 0x000B3934 File Offset: 0x000B1B34
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter, SerializationFormatter = true)]
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.GetObjectData(serializationInfo, streamingContext);
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data that is needed to serialize the target object.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that specifies the destination for this serialization.</param>
		// Token: 0x0600338D RID: 13197 RVA: 0x000B3940 File Offset: 0x000B1B40
		[SecurityPermission(SecurityAction.LinkDemand, SerializationFormatter = true)]
		protected virtual void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			serializationInfo.AddValue("_BypassOnLocal", this._BypassOnLocal);
			serializationInfo.AddValue("_ProxyAddress", this._ProxyAddress);
			serializationInfo.AddValue("_BypassList", this._BypassList);
			serializationInfo.AddValue("_UseDefaultCredentials", this.UseDefaultCredentials);
			if (this._UseRegistry)
			{
				serializationInfo.AddValue("_UseRegistry", true);
			}
		}

		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x0600338E RID: 13198 RVA: 0x000B39A5 File Offset: 0x000B1BA5
		// (set) Token: 0x0600338F RID: 13199 RVA: 0x000B39AD File Offset: 0x000B1BAD
		internal AutoWebProxyScriptEngine ScriptEngine
		{
			get
			{
				return this.m_ScriptEngine;
			}
			set
			{
				this.m_ScriptEngine = value;
			}
		}

		// Token: 0x06003390 RID: 13200 RVA: 0x000B39B8 File Offset: 0x000B1BB8
		public static IWebProxy CreateDefaultProxy()
		{
			if (Platform.IsMacOS)
			{
				IWebProxy defaultProxy = CFNetwork.GetDefaultProxy();
				if (defaultProxy != null)
				{
					return defaultProxy;
				}
			}
			return new WebProxy(true);
		}

		// Token: 0x06003391 RID: 13201 RVA: 0x000B39DD File Offset: 0x000B1BDD
		internal WebProxy(bool enableAutoproxy)
		{
			this.m_EnableAutoproxy = enableAutoproxy;
			this.UnsafeUpdateFromRegistry();
		}

		// Token: 0x06003392 RID: 13202 RVA: 0x000B39F2 File Offset: 0x000B1BF2
		internal void DeleteScriptEngine()
		{
			if (this.ScriptEngine != null)
			{
				this.ScriptEngine.Close();
				this.ScriptEngine = null;
			}
		}

		// Token: 0x06003393 RID: 13203 RVA: 0x000B3A10 File Offset: 0x000B1C10
		internal void UnsafeUpdateFromRegistry()
		{
			this._UseRegistry = true;
			this.ScriptEngine = new AutoWebProxyScriptEngine(this, true);
			WebProxyData webProxyData = this.ScriptEngine.GetWebProxyData();
			this.Update(webProxyData);
		}

		// Token: 0x06003394 RID: 13204 RVA: 0x000B3A44 File Offset: 0x000B1C44
		internal void Update(WebProxyData webProxyData)
		{
			lock (this)
			{
				this._BypassOnLocal = webProxyData.bypassOnLocal;
				this._ProxyAddress = webProxyData.proxyAddress;
				this._ProxyHostAddresses = webProxyData.proxyHostAddresses;
				this._BypassList = webProxyData.bypassList;
				this.ScriptEngine.AutomaticallyDetectSettings = this.m_EnableAutoproxy && webProxyData.automaticallyDetectSettings;
				this.ScriptEngine.AutomaticConfigurationScript = (this.m_EnableAutoproxy ? webProxyData.scriptLocation : null);
			}
		}

		// Token: 0x06003395 RID: 13205 RVA: 0x000B3AE4 File Offset: 0x000B1CE4
		ProxyChain IAutoWebProxy.GetProxies(Uri destination)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			return new ProxyScriptChain(this, destination);
		}

		// Token: 0x06003396 RID: 13206 RVA: 0x000B3B04 File Offset: 0x000B1D04
		private bool GetProxyAuto(Uri destination, out Uri proxyUri)
		{
			proxyUri = null;
			if (this.ScriptEngine == null)
			{
				return false;
			}
			IList<string> list = null;
			if (!this.ScriptEngine.GetProxies(destination, out list))
			{
				return false;
			}
			if (list.Count > 0)
			{
				if (WebProxy.AreAllBypassed(list, true))
				{
					proxyUri = destination;
				}
				else
				{
					proxyUri = WebProxy.ProxyUri(list[0]);
				}
			}
			return true;
		}

		// Token: 0x06003397 RID: 13207 RVA: 0x000B3B58 File Offset: 0x000B1D58
		private bool IsBypassedAuto(Uri destination, out bool isBypassed)
		{
			isBypassed = true;
			if (this.ScriptEngine == null)
			{
				return false;
			}
			IList<string> list;
			if (!this.ScriptEngine.GetProxies(destination, out list))
			{
				return false;
			}
			if (list.Count == 0)
			{
				isBypassed = false;
			}
			else
			{
				isBypassed = WebProxy.AreAllBypassed(list, true);
			}
			return true;
		}

		// Token: 0x06003398 RID: 13208 RVA: 0x000B3B9C File Offset: 0x000B1D9C
		internal Uri[] GetProxiesAuto(Uri destination, ref int syncStatus)
		{
			if (this.ScriptEngine == null)
			{
				return null;
			}
			IList<string> list = null;
			if (!this.ScriptEngine.GetProxies(destination, out list, ref syncStatus))
			{
				return null;
			}
			Uri[] array;
			if (list.Count == 0)
			{
				array = new Uri[0];
			}
			else if (WebProxy.AreAllBypassed(list, false))
			{
				array = new Uri[1];
			}
			else
			{
				array = new Uri[list.Count];
				for (int i = 0; i < list.Count; i++)
				{
					array[i] = WebProxy.ProxyUri(list[i]);
				}
			}
			return array;
		}

		// Token: 0x06003399 RID: 13209 RVA: 0x000B3C1A File Offset: 0x000B1E1A
		internal void AbortGetProxiesAuto(ref int syncStatus)
		{
			if (this.ScriptEngine != null)
			{
				this.ScriptEngine.Abort(ref syncStatus);
			}
		}

		// Token: 0x0600339A RID: 13210 RVA: 0x000B3C30 File Offset: 0x000B1E30
		internal Uri GetProxyAutoFailover(Uri destination)
		{
			if (this.IsBypassedManual(destination))
			{
				return null;
			}
			Uri uri = this._ProxyAddress;
			Hashtable proxyHostAddresses = this._ProxyHostAddresses;
			if (proxyHostAddresses != null)
			{
				uri = proxyHostAddresses[destination.Scheme] as Uri;
			}
			return uri;
		}

		// Token: 0x0600339B RID: 13211 RVA: 0x000B3C6C File Offset: 0x000B1E6C
		private static bool AreAllBypassed(IEnumerable<string> proxies, bool checkFirstOnly)
		{
			bool flag = true;
			foreach (string text in proxies)
			{
				flag = string.IsNullOrEmpty(text);
				if (checkFirstOnly)
				{
					break;
				}
				if (!flag)
				{
					break;
				}
			}
			return flag;
		}

		// Token: 0x0600339C RID: 13212 RVA: 0x000B3CC0 File Offset: 0x000B1EC0
		private static Uri ProxyUri(string proxyName)
		{
			if (proxyName != null && proxyName.Length != 0)
			{
				return new Uri("http://" + proxyName);
			}
			return null;
		}

		// Token: 0x04001E42 RID: 7746
		private bool _UseRegistry;

		// Token: 0x04001E43 RID: 7747
		private bool _BypassOnLocal;

		// Token: 0x04001E44 RID: 7748
		private bool m_EnableAutoproxy;

		// Token: 0x04001E45 RID: 7749
		private Uri _ProxyAddress;

		// Token: 0x04001E46 RID: 7750
		private ArrayList _BypassList;

		// Token: 0x04001E47 RID: 7751
		private ICredentials _Credentials;

		// Token: 0x04001E48 RID: 7752
		private Regex[] _RegExBypassList;

		// Token: 0x04001E49 RID: 7753
		private Hashtable _ProxyHostAddresses;

		// Token: 0x04001E4A RID: 7754
		private AutoWebProxyScriptEngine m_ScriptEngine;
	}
}
