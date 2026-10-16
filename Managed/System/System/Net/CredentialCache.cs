using System;
using System.Collections;

namespace System.Net
{
	/// <summary>Provides storage for multiple credentials.</summary>
	// Token: 0x020005C8 RID: 1480
	public class CredentialCache : ICredentials, ICredentialsByHost, IEnumerable
	{
		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x06002FEA RID: 12266 RVA: 0x000A52AE File Offset: 0x000A34AE
		internal bool IsDefaultInCache
		{
			get
			{
				return this.m_NumbDefaultCredInCache != 0;
			}
		}

		/// <summary>Adds a <see cref="T:System.Net.NetworkCredential" /> instance to the credential cache for use with protocols other than SMTP and associates it with a Uniform Resource Identifier (URI) prefix and authentication protocol. </summary>
		/// <param name="uriPrefix">A <see cref="T:System.Uri" /> that specifies the URI prefix of the resources that the credential grants access to. </param>
		/// <param name="authType">The authentication scheme used by the resource named in <paramref name="uriPrefix" />. </param>
		/// <param name="cred">The <see cref="T:System.Net.NetworkCredential" /> to add to the credential cache. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="uriPrefix" /> is null. -or- <paramref name="authType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The same credentials are added more than once. </exception>
		// Token: 0x06002FEC RID: 12268 RVA: 0x000A52D8 File Offset: 0x000A34D8
		public void Add(Uri uriPrefix, string authType, NetworkCredential cred)
		{
			if (uriPrefix == null)
			{
				throw new ArgumentNullException("uriPrefix");
			}
			if (authType == null)
			{
				throw new ArgumentNullException("authType");
			}
			if (cred is SystemNetworkCredential)
			{
				throw new ArgumentException(SR.GetString("Default credentials cannot be supplied for the {0} authentication scheme.", new object[] { authType }), "authType");
			}
			this.m_version++;
			CredentialKey credentialKey = new CredentialKey(uriPrefix, authType);
			this.cache.Add(credentialKey, cred);
			if (cred is SystemNetworkCredential)
			{
				this.m_NumbDefaultCredInCache++;
			}
		}

		/// <summary>Adds a <see cref="T:System.Net.NetworkCredential" /> instance for use with SMTP to the credential cache and associates it with a host computer, port, and authentication protocol. Credentials added using this method are valid for SMTP only. This method does not work for HTTP or FTP requests.</summary>
		/// <param name="host">A <see cref="T:System.String" /> that identifies the host computer.</param>
		/// <param name="port">A <see cref="T:System.Int32" /> that specifies the port to connect to on <paramref name="host" />.</param>
		/// <param name="authenticationType">A <see cref="T:System.String" /> that identifies the authentication scheme used when connecting to <paramref name="host" /> using <paramref name="cred" />. See Remarks.</param>
		/// <param name="credential">The <see cref="T:System.Net.NetworkCredential" /> to add to the credential cache. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="host" /> is null. -or-<paramref name="authType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="authType" /> not an accepted value. See Remarks. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="port" /> is less than zero.</exception>
		// Token: 0x06002FED RID: 12269 RVA: 0x000A5368 File Offset: 0x000A3568
		public void Add(string host, int port, string authenticationType, NetworkCredential credential)
		{
			if (host == null)
			{
				throw new ArgumentNullException("host");
			}
			if (authenticationType == null)
			{
				throw new ArgumentNullException("authenticationType");
			}
			if (host.Length == 0)
			{
				throw new ArgumentException(SR.GetString("The parameter '{0}' cannot be an empty string.", new object[] { "host" }));
			}
			if (port < 0)
			{
				throw new ArgumentOutOfRangeException("port");
			}
			if (credential is SystemNetworkCredential)
			{
				throw new ArgumentException(SR.GetString("Default credentials cannot be supplied for the {0} authentication scheme.", new object[] { authenticationType }), "authenticationType");
			}
			this.m_version++;
			CredentialHostKey credentialHostKey = new CredentialHostKey(host, port, authenticationType);
			this.cacheForHosts.Add(credentialHostKey, credential);
			if (credential is SystemNetworkCredential)
			{
				this.m_NumbDefaultCredInCache++;
			}
		}

		/// <summary>Deletes a <see cref="T:System.Net.NetworkCredential" /> instance from the cache if it is associated with the specified Uniform Resource Identifier (URI) prefix and authentication protocol.</summary>
		/// <param name="uriPrefix">A <see cref="T:System.Uri" /> that specifies the URI prefix of the resources that the credential is used for. </param>
		/// <param name="authType">The authentication scheme used by the host named in <paramref name="uriPrefix" />. </param>
		// Token: 0x06002FEE RID: 12270 RVA: 0x000A542C File Offset: 0x000A362C
		public void Remove(Uri uriPrefix, string authType)
		{
			if (uriPrefix == null || authType == null)
			{
				return;
			}
			this.m_version++;
			CredentialKey credentialKey = new CredentialKey(uriPrefix, authType);
			if (this.cache[credentialKey] is SystemNetworkCredential)
			{
				this.m_NumbDefaultCredInCache--;
			}
			this.cache.Remove(credentialKey);
		}

		/// <summary>Deletes a <see cref="T:System.Net.NetworkCredential" /> instance from the cache if it is associated with the specified host, port, and authentication protocol.</summary>
		/// <param name="host">A <see cref="T:System.String" /> that identifies the host computer.</param>
		/// <param name="port">A <see cref="T:System.Int32" /> that specifies the port to connect to on <paramref name="host" />.</param>
		/// <param name="authenticationType">A <see cref="T:System.String" /> that identifies the authentication scheme used when connecting to <paramref name="host" />. See Remarks.</param>
		// Token: 0x06002FEF RID: 12271 RVA: 0x000A548C File Offset: 0x000A368C
		public void Remove(string host, int port, string authenticationType)
		{
			if (host == null || authenticationType == null)
			{
				return;
			}
			if (port < 0)
			{
				return;
			}
			this.m_version++;
			CredentialHostKey credentialHostKey = new CredentialHostKey(host, port, authenticationType);
			if (this.cacheForHosts[credentialHostKey] is SystemNetworkCredential)
			{
				this.m_NumbDefaultCredInCache--;
			}
			this.cacheForHosts.Remove(credentialHostKey);
		}

		/// <summary>Returns the <see cref="T:System.Net.NetworkCredential" /> instance associated with the specified Uniform Resource Identifier (URI) and authentication type.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkCredential" /> or, if there is no matching credential in the cache, null.</returns>
		/// <param name="uriPrefix">A <see cref="T:System.Uri" /> that specifies the URI prefix of the resources that the credential grants access to. </param>
		/// <param name="authType">The authentication scheme used by the resource named in <paramref name="uriPrefix" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="uriPrefix" /> or <paramref name="authType" /> is null. </exception>
		// Token: 0x06002FF0 RID: 12272 RVA: 0x000A54EC File Offset: 0x000A36EC
		public NetworkCredential GetCredential(Uri uriPrefix, string authType)
		{
			if (uriPrefix == null)
			{
				throw new ArgumentNullException("uriPrefix");
			}
			if (authType == null)
			{
				throw new ArgumentNullException("authType");
			}
			int num = -1;
			NetworkCredential networkCredential = null;
			IDictionaryEnumerator enumerator = this.cache.GetEnumerator();
			while (enumerator.MoveNext())
			{
				CredentialKey credentialKey = (CredentialKey)enumerator.Key;
				if (credentialKey.Match(uriPrefix, authType))
				{
					int uriPrefixLength = credentialKey.UriPrefixLength;
					if (uriPrefixLength > num)
					{
						num = uriPrefixLength;
						networkCredential = (NetworkCredential)enumerator.Value;
					}
				}
			}
			return networkCredential;
		}

		/// <summary>Returns the <see cref="T:System.Net.NetworkCredential" /> instance associated with the specified host, port, and authentication protocol.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkCredential" /> or, if there is no matching credential in the cache, null.</returns>
		/// <param name="host">A <see cref="T:System.String" /> that identifies the host computer.</param>
		/// <param name="port">A <see cref="T:System.Int32" /> that specifies the port to connect to on <paramref name="host" />.</param>
		/// <param name="authenticationType">A <see cref="T:System.String" /> that identifies the authentication scheme used when connecting to <paramref name="host" />. See Remarks.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="host" /> is null. -or- <paramref name="authType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="authType" /> not an accepted value. See Remarks. -or-<paramref name="host" /> is equal to the empty string ("").</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="port" /> is less than zero.</exception>
		// Token: 0x06002FF1 RID: 12273 RVA: 0x000A5568 File Offset: 0x000A3768
		public NetworkCredential GetCredential(string host, int port, string authenticationType)
		{
			if (host == null)
			{
				throw new ArgumentNullException("host");
			}
			if (authenticationType == null)
			{
				throw new ArgumentNullException("authenticationType");
			}
			if (host.Length == 0)
			{
				throw new ArgumentException(SR.GetString("The parameter '{0}' cannot be an empty string.", new object[] { "host" }));
			}
			if (port < 0)
			{
				throw new ArgumentOutOfRangeException("port");
			}
			NetworkCredential networkCredential = null;
			IDictionaryEnumerator enumerator = this.cacheForHosts.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (((CredentialHostKey)enumerator.Key).Match(host, port, authenticationType))
				{
					networkCredential = (NetworkCredential)enumerator.Value;
				}
			}
			return networkCredential;
		}

		/// <summary>Returns an enumerator that can iterate through the <see cref="T:System.Net.CredentialCache" /> instance.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Net.CredentialCache" />.</returns>
		// Token: 0x06002FF2 RID: 12274 RVA: 0x000A5600 File Offset: 0x000A3800
		public IEnumerator GetEnumerator()
		{
			return new CredentialCache.CredentialEnumerator(this, this.cache, this.cacheForHosts, this.m_version);
		}

		/// <summary>Gets the system credentials of the application.</summary>
		/// <returns>An <see cref="T:System.Net.ICredentials" /> that represents the system credentials of the application.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="USERNAME" />
		/// </PermissionSet>
		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x06002FF3 RID: 12275 RVA: 0x000A561A File Offset: 0x000A381A
		public static ICredentials DefaultCredentials
		{
			get
			{
				return SystemNetworkCredential.defaultCredential;
			}
		}

		/// <summary>Gets the network credentials of the current security context.</summary>
		/// <returns>An <see cref="T:System.Net.NetworkCredential" /> that represents the network credentials of the current user or application.</returns>
		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x06002FF4 RID: 12276 RVA: 0x000A561A File Offset: 0x000A381A
		public static NetworkCredential DefaultNetworkCredentials
		{
			get
			{
				return SystemNetworkCredential.defaultCredential;
			}
		}

		// Token: 0x04001A83 RID: 6787
		private Hashtable cache = new Hashtable();

		// Token: 0x04001A84 RID: 6788
		private Hashtable cacheForHosts = new Hashtable();

		// Token: 0x04001A85 RID: 6789
		internal int m_version;

		// Token: 0x04001A86 RID: 6790
		private int m_NumbDefaultCredInCache;

		// Token: 0x020005C9 RID: 1481
		private class CredentialEnumerator : IEnumerator
		{
			// Token: 0x06002FF5 RID: 12277 RVA: 0x000A5624 File Offset: 0x000A3824
			internal CredentialEnumerator(CredentialCache cache, Hashtable table, Hashtable hostTable, int version)
			{
				this.m_cache = cache;
				this.m_array = new ICredentials[table.Count + hostTable.Count];
				table.Values.CopyTo(this.m_array, 0);
				hostTable.Values.CopyTo(this.m_array, table.Count);
				this.m_version = version;
			}

			// Token: 0x17000997 RID: 2455
			// (get) Token: 0x06002FF6 RID: 12278 RVA: 0x000A5690 File Offset: 0x000A3890
			object IEnumerator.Current
			{
				get
				{
					if (this.m_index < 0 || this.m_index >= this.m_array.Length)
					{
						throw new InvalidOperationException(SR.GetString("Enumeration has either not started or has already finished."));
					}
					if (this.m_version != this.m_cache.m_version)
					{
						throw new InvalidOperationException(SR.GetString("Collection was modified; enumeration operation may not execute."));
					}
					return this.m_array[this.m_index];
				}
			}

			// Token: 0x06002FF7 RID: 12279 RVA: 0x000A56F8 File Offset: 0x000A38F8
			bool IEnumerator.MoveNext()
			{
				if (this.m_version != this.m_cache.m_version)
				{
					throw new InvalidOperationException(SR.GetString("Collection was modified; enumeration operation may not execute."));
				}
				int num = this.m_index + 1;
				this.m_index = num;
				if (num < this.m_array.Length)
				{
					return true;
				}
				this.m_index = this.m_array.Length;
				return false;
			}

			// Token: 0x06002FF8 RID: 12280 RVA: 0x000A5754 File Offset: 0x000A3954
			void IEnumerator.Reset()
			{
				this.m_index = -1;
			}

			// Token: 0x04001A87 RID: 6791
			private CredentialCache m_cache;

			// Token: 0x04001A88 RID: 6792
			private ICredentials[] m_array;

			// Token: 0x04001A89 RID: 6793
			private int m_index = -1;

			// Token: 0x04001A8A RID: 6794
			private int m_version;
		}
	}
}
