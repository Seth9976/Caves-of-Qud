using System;
using System.Collections;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using Mono.Net.Dns;

namespace System.Net
{
	/// <summary>Provides simple domain name resolution functionality.</summary>
	// Token: 0x0200067A RID: 1658
	public static class Dns
	{
		// Token: 0x0600341E RID: 13342 RVA: 0x000B5C4C File Offset: 0x000B3E4C
		static Dns()
		{
			if (Environment.GetEnvironmentVariable("MONO_DNS") != null)
			{
				Dns.resolver = new SimpleResolver();
				Dns.use_mono_dns = true;
			}
		}

		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x0600341F RID: 13343 RVA: 0x000B5C6A File Offset: 0x000B3E6A
		internal static bool UseMonoDns
		{
			get
			{
				return Dns.use_mono_dns;
			}
		}

		// Token: 0x06003420 RID: 13344 RVA: 0x000B5C74 File Offset: 0x000B3E74
		private static void OnCompleted(object sender, SimpleResolverEventArgs e)
		{
			DnsAsyncResult dnsAsyncResult = (DnsAsyncResult)e.UserToken;
			IPHostEntry hostEntry = e.HostEntry;
			if (hostEntry == null || e.ResolverError != ResolverError.NoError)
			{
				dnsAsyncResult.SetCompleted(false, new Exception("Error: " + e.ResolverError.ToString()));
				return;
			}
			dnsAsyncResult.SetCompleted(false, hostEntry);
		}

		// Token: 0x06003421 RID: 13345 RVA: 0x000B5CD4 File Offset: 0x000B3ED4
		private static IAsyncResult BeginAsyncCallAddresses(string host, AsyncCallback callback, object state)
		{
			SimpleResolverEventArgs e = new SimpleResolverEventArgs();
			e.Completed += Dns.OnCompleted;
			e.HostName = host;
			DnsAsyncResult dnsAsyncResult = new DnsAsyncResult(callback, state);
			e.UserToken = dnsAsyncResult;
			if (!Dns.resolver.GetHostAddressesAsync(e))
			{
				dnsAsyncResult.SetCompleted(true, e.HostEntry);
			}
			return dnsAsyncResult;
		}

		// Token: 0x06003422 RID: 13346 RVA: 0x000B5D2C File Offset: 0x000B3F2C
		private static IAsyncResult BeginAsyncCall(string host, AsyncCallback callback, object state)
		{
			SimpleResolverEventArgs e = new SimpleResolverEventArgs();
			e.Completed += Dns.OnCompleted;
			e.HostName = host;
			DnsAsyncResult dnsAsyncResult = new DnsAsyncResult(callback, state);
			e.UserToken = dnsAsyncResult;
			if (!Dns.resolver.GetHostEntryAsync(e))
			{
				dnsAsyncResult.SetCompleted(true, e.HostEntry);
			}
			return dnsAsyncResult;
		}

		// Token: 0x06003423 RID: 13347 RVA: 0x000B5D84 File Offset: 0x000B3F84
		private static IPHostEntry EndAsyncCall(DnsAsyncResult ares)
		{
			if (ares == null)
			{
				throw new ArgumentException("Invalid asyncResult");
			}
			if (!ares.IsCompleted)
			{
				ares.AsyncWaitHandle.WaitOne();
			}
			if (ares.Exception != null)
			{
				throw ares.Exception;
			}
			IPHostEntry hostEntry = ares.HostEntry;
			if (hostEntry == null || hostEntry.AddressList == null || hostEntry.AddressList.Length == 0)
			{
				Dns.Error_11001(hostEntry.HostName);
			}
			return hostEntry;
		}

		/// <summary>Begins an asynchronous request for <see cref="T:System.Net.IPHostEntry" /> information about the specified DNS host name.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> instance that references the asynchronous request.</returns>
		/// <param name="hostName">The DNS name of the host. </param>
		/// <param name="requestCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the operation is complete.</param>
		/// <param name="stateObject">A user-defined object that contains information about the operation. This object is passed to the <paramref name="requestCallback" /> delegate when the operation is complete.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hostName" /> is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error was encountered executing the DNS query. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Net.DnsPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003424 RID: 13348 RVA: 0x000B5DE9 File Offset: 0x000B3FE9
		[Obsolete("Use BeginGetHostEntry instead")]
		public static IAsyncResult BeginGetHostByName(string hostName, AsyncCallback requestCallback, object stateObject)
		{
			if (hostName == null)
			{
				throw new ArgumentNullException("hostName");
			}
			if (Dns.use_mono_dns)
			{
				return Dns.BeginAsyncCall(hostName, requestCallback, stateObject);
			}
			return new Dns.GetHostByNameCallback(Dns.GetHostByName).BeginInvoke(hostName, requestCallback, stateObject);
		}

		/// <summary>Begins an asynchronous request to resolve a DNS host name or IP address to an <see cref="T:System.Net.IPAddress" /> instance.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> instance that references the asynchronous request.</returns>
		/// <param name="hostName">The DNS name of the host. </param>
		/// <param name="requestCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the operation is complete. </param>
		/// <param name="stateObject">A user-defined object that contains information about the operation. This object is passed to the <paramref name="requestCallback" /> delegate when the operation is complete.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hostName" /> is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">The caller does not have permission to access DNS information. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Net.DnsPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003425 RID: 13349 RVA: 0x000B5E1D File Offset: 0x000B401D
		[Obsolete("Use BeginGetHostEntry instead")]
		public static IAsyncResult BeginResolve(string hostName, AsyncCallback requestCallback, object stateObject)
		{
			if (hostName == null)
			{
				throw new ArgumentNullException("hostName");
			}
			if (Dns.use_mono_dns)
			{
				return Dns.BeginAsyncCall(hostName, requestCallback, stateObject);
			}
			return new Dns.ResolveCallback(Dns.Resolve).BeginInvoke(hostName, requestCallback, stateObject);
		}

		/// <summary>Asynchronously returns the Internet Protocol (IP) addresses for the specified host.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> instance that references the asynchronous request.</returns>
		/// <param name="hostNameOrAddress">The host name or IP address to resolve.</param>
		/// <param name="requestCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the operation is complete. </param>
		/// <param name="state">A user-defined object that contains information about the operation. This object is passed to the <paramref name="requestCallback" /> delegate when the operation is complete.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hostNameOrAddress" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="hostNameOrAddress" /> is greater than 255 characters. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error is encountered when resolving <paramref name="hostNameOrAddress" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="hostNameOrAddress" /> is an invalid IP address.</exception>
		// Token: 0x06003426 RID: 13350 RVA: 0x000B5E54 File Offset: 0x000B4054
		public static IAsyncResult BeginGetHostAddresses(string hostNameOrAddress, AsyncCallback requestCallback, object state)
		{
			if (hostNameOrAddress == null)
			{
				throw new ArgumentNullException("hostName");
			}
			if (hostNameOrAddress == "0.0.0.0" || hostNameOrAddress == "::0")
			{
				throw new ArgumentException("Addresses 0.0.0.0 (IPv4) and ::0 (IPv6) are unspecified addresses. You cannot use them as target address.", "hostNameOrAddress");
			}
			if (Dns.use_mono_dns)
			{
				return Dns.BeginAsyncCallAddresses(hostNameOrAddress, requestCallback, state);
			}
			return new Dns.GetHostAddressesCallback(Dns.GetHostAddresses).BeginInvoke(hostNameOrAddress, requestCallback, state);
		}

		/// <summary>Asynchronously resolves a host name or IP address to an <see cref="T:System.Net.IPHostEntry" /> instance.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> instance that references the asynchronous request.</returns>
		/// <param name="hostNameOrAddress">The host name or IP address to resolve.</param>
		/// <param name="requestCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the operation is complete. </param>
		/// <param name="stateObject">A user-defined object that contains information about the operation. This object is passed to the <paramref name="requestCallback" /> delegate when the operation is complete.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hostNameOrAddress" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="hostNameOrAddress" /> is greater than 255 characters. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error is encountered when resolving <paramref name="hostNameOrAddress" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="hostNameOrAddress" /> is an invalid IP address.</exception>
		// Token: 0x06003427 RID: 13351 RVA: 0x000B5EC0 File Offset: 0x000B40C0
		public static IAsyncResult BeginGetHostEntry(string hostNameOrAddress, AsyncCallback requestCallback, object stateObject)
		{
			if (hostNameOrAddress == null)
			{
				throw new ArgumentNullException("hostName");
			}
			if (hostNameOrAddress == "0.0.0.0" || hostNameOrAddress == "::0")
			{
				throw new ArgumentException("Addresses 0.0.0.0 (IPv4) and ::0 (IPv6) are unspecified addresses. You cannot use them as target address.", "hostNameOrAddress");
			}
			if (Dns.use_mono_dns)
			{
				return Dns.BeginAsyncCall(hostNameOrAddress, requestCallback, stateObject);
			}
			return new Dns.GetHostEntryNameCallback(Dns.GetHostEntry).BeginInvoke(hostNameOrAddress, requestCallback, stateObject);
		}

		/// <summary>Asynchronously resolves an IP address to an <see cref="T:System.Net.IPHostEntry" /> instance.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> instance that references the asynchronous request.</returns>
		/// <param name="address">The IP address to resolve.</param>
		/// <param name="requestCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the operation is complete. </param>
		/// <param name="stateObject">A user-defined object that contains information about the operation. This object is passed to the <paramref name="requestCallback" /> delegate when the operation is complete.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error is encountered when resolving <paramref name="address" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="address" /> is an invalid IP address.</exception>
		// Token: 0x06003428 RID: 13352 RVA: 0x000B5F29 File Offset: 0x000B4129
		public static IAsyncResult BeginGetHostEntry(IPAddress address, AsyncCallback requestCallback, object stateObject)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (Dns.use_mono_dns)
			{
				return Dns.BeginAsyncCall(address.ToString(), requestCallback, stateObject);
			}
			return new Dns.GetHostEntryIPCallback(Dns.GetHostEntry).BeginInvoke(address, requestCallback, stateObject);
		}

		/// <summary>Ends an asynchronous request for DNS information.</summary>
		/// <returns>An <see cref="T:System.Net.IPHostEntry" /> object that contains DNS information about a host.</returns>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> instance that is returned by a call to the <see cref="M:System.Net.Dns.BeginGetHostByName(System.String,System.AsyncCallback,System.Object)" /> method.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06003429 RID: 13353 RVA: 0x000B5F62 File Offset: 0x000B4162
		[Obsolete("Use EndGetHostEntry instead")]
		public static IPHostEntry EndGetHostByName(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			if (Dns.use_mono_dns)
			{
				return Dns.EndAsyncCall(asyncResult as DnsAsyncResult);
			}
			return ((Dns.GetHostByNameCallback)((AsyncResult)asyncResult).AsyncDelegate).EndInvoke(asyncResult);
		}

		/// <summary>Ends an asynchronous request for DNS information.</summary>
		/// <returns>An <see cref="T:System.Net.IPHostEntry" /> object that contains DNS information about a host.</returns>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> instance that is returned by a call to the <see cref="M:System.Net.Dns.BeginResolve(System.String,System.AsyncCallback,System.Object)" /> method.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600342A RID: 13354 RVA: 0x000B5F9B File Offset: 0x000B419B
		[Obsolete("Use EndGetHostEntry instead")]
		public static IPHostEntry EndResolve(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			if (Dns.use_mono_dns)
			{
				return Dns.EndAsyncCall(asyncResult as DnsAsyncResult);
			}
			return ((Dns.ResolveCallback)((AsyncResult)asyncResult).AsyncDelegate).EndInvoke(asyncResult);
		}

		/// <summary>Ends an asynchronous request for DNS information.</summary>
		/// <returns>An array of type <see cref="T:System.Net.IPAddress" /> that holds the IP addresses for the host specified by the <paramref name="hostNameOrAddress" /> parameter of <see cref="M:System.Net.Dns.BeginGetHostAddresses(System.String,System.AsyncCallback,System.Object)" />.</returns>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> instance returned by a call to the <see cref="M:System.Net.Dns.BeginGetHostAddresses(System.String,System.AsyncCallback,System.Object)" /> method.</param>
		// Token: 0x0600342B RID: 13355 RVA: 0x000B5FD4 File Offset: 0x000B41D4
		public static IPAddress[] EndGetHostAddresses(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			if (!Dns.use_mono_dns)
			{
				return ((Dns.GetHostAddressesCallback)((AsyncResult)asyncResult).AsyncDelegate).EndInvoke(asyncResult);
			}
			IPHostEntry iphostEntry = Dns.EndAsyncCall(asyncResult as DnsAsyncResult);
			if (iphostEntry == null)
			{
				return null;
			}
			return iphostEntry.AddressList;
		}

		/// <summary>Ends an asynchronous request for DNS information.</summary>
		/// <returns>An <see cref="T:System.Net.IPHostEntry" /> instance that contains address information about the host.</returns>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> instance returned by a call to an <see cref="Overload:System.Net.Dns.BeginGetHostEntry" /> method.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null. </exception>
		// Token: 0x0600342C RID: 13356 RVA: 0x000B6024 File Offset: 0x000B4224
		public static IPHostEntry EndGetHostEntry(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			if (Dns.use_mono_dns)
			{
				return Dns.EndAsyncCall(asyncResult as DnsAsyncResult);
			}
			AsyncResult asyncResult2 = (AsyncResult)asyncResult;
			if (asyncResult2.AsyncDelegate is Dns.GetHostEntryIPCallback)
			{
				return ((Dns.GetHostEntryIPCallback)asyncResult2.AsyncDelegate).EndInvoke(asyncResult);
			}
			return ((Dns.GetHostEntryNameCallback)asyncResult2.AsyncDelegate).EndInvoke(asyncResult);
		}

		// Token: 0x0600342D RID: 13357
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetHostByName_icall(string host, out string h_name, out string[] h_aliases, out string[] h_addr_list, int hint);

		// Token: 0x0600342E RID: 13358
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetHostByAddr_icall(string addr, out string h_name, out string[] h_aliases, out string[] h_addr_list, int hint);

		// Token: 0x0600342F RID: 13359
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetHostName_icall(out string h_name);

		// Token: 0x06003430 RID: 13360 RVA: 0x000B6089 File Offset: 0x000B4289
		private static void Error_11001(string hostName)
		{
			throw new SocketException(11001, string.Format("Could not resolve host '{0}'", hostName));
		}

		// Token: 0x06003431 RID: 13361 RVA: 0x000B60A0 File Offset: 0x000B42A0
		private static IPHostEntry hostent_to_IPHostEntry(string originalHostName, string h_name, string[] h_aliases, string[] h_addrlist)
		{
			IPHostEntry iphostEntry = new IPHostEntry();
			ArrayList arrayList = new ArrayList();
			iphostEntry.HostName = h_name;
			iphostEntry.Aliases = h_aliases;
			for (int i = 0; i < h_addrlist.Length; i++)
			{
				try
				{
					IPAddress ipaddress = IPAddress.Parse(h_addrlist[i]);
					if ((Socket.OSSupportsIPv6 && ipaddress.AddressFamily == AddressFamily.InterNetworkV6) || (Socket.OSSupportsIPv4 && ipaddress.AddressFamily == AddressFamily.InterNetwork))
					{
						arrayList.Add(ipaddress);
					}
				}
				catch (ArgumentNullException)
				{
				}
			}
			if (arrayList.Count == 0)
			{
				Dns.Error_11001(originalHostName);
			}
			iphostEntry.AddressList = arrayList.ToArray(typeof(IPAddress)) as IPAddress[];
			return iphostEntry;
		}

		/// <summary>Creates an <see cref="T:System.Net.IPHostEntry" /> instance from the specified <see cref="T:System.Net.IPAddress" />.</summary>
		/// <returns>An <see cref="T:System.Net.IPHostEntry" />.An <see cref="T:System.Net.IPHostEntry" /> instance.</returns>
		/// <param name="address">An <see cref="T:System.Net.IPAddress" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error is encountered when resolving <paramref name="address" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Net.DnsPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003432 RID: 13362 RVA: 0x000B6148 File Offset: 0x000B4348
		[Obsolete("Use GetHostEntry instead")]
		public static IPHostEntry GetHostByAddress(IPAddress address)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return Dns.GetHostByAddressFromString(address.ToString(), false);
		}

		/// <summary>Creates an <see cref="T:System.Net.IPHostEntry" /> instance from an IP address.</summary>
		/// <returns>An <see cref="T:System.Net.IPHostEntry" /> instance.</returns>
		/// <param name="address">An IP address. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error is encountered when resolving <paramref name="address" />. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="address" /> is not a valid IP address. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Net.DnsPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003433 RID: 13363 RVA: 0x000B6164 File Offset: 0x000B4364
		[Obsolete("Use GetHostEntry instead")]
		public static IPHostEntry GetHostByAddress(string address)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return Dns.GetHostByAddressFromString(address, true);
		}

		// Token: 0x06003434 RID: 13364 RVA: 0x000B617C File Offset: 0x000B437C
		private static IPHostEntry GetHostByAddressFromString(string address, bool parse)
		{
			if (address.Equals("0.0.0.0"))
			{
				address = "127.0.0.1";
				parse = false;
			}
			if (parse)
			{
				IPAddress.Parse(address);
			}
			string text;
			string[] array;
			string[] array2;
			if (!Dns.GetHostByAddr_icall(address, out text, out array, out array2, Socket.FamilyHint))
			{
				Dns.Error_11001(address);
			}
			return Dns.hostent_to_IPHostEntry(address, text, array, array2);
		}

		/// <summary>Resolves a host name or IP address to an <see cref="T:System.Net.IPHostEntry" /> instance.</summary>
		/// <returns>An <see cref="T:System.Net.IPHostEntry" /> instance that contains address information about the host specified in <paramref name="hostNameOrAddress" />.</returns>
		/// <param name="hostNameOrAddress">The host name or IP address to resolve.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="hostNameOrAddress" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="hostNameOrAddress" /> parameter is greater than 255 characters. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error was encountered when resolving the <paramref name="hostNameOrAddress" /> parameter. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="hostNameOrAddress" /> parameter is an invalid IP address. </exception>
		// Token: 0x06003435 RID: 13365 RVA: 0x000B61CC File Offset: 0x000B43CC
		public static IPHostEntry GetHostEntry(string hostNameOrAddress)
		{
			if (hostNameOrAddress == null)
			{
				throw new ArgumentNullException("hostNameOrAddress");
			}
			if (hostNameOrAddress == "0.0.0.0" || hostNameOrAddress == "::0")
			{
				throw new ArgumentException("Addresses 0.0.0.0 (IPv4) and ::0 (IPv6) are unspecified addresses. You cannot use them as target address.", "hostNameOrAddress");
			}
			IPAddress ipaddress;
			if (hostNameOrAddress.Length > 0 && IPAddress.TryParse(hostNameOrAddress, out ipaddress))
			{
				return Dns.GetHostEntry(ipaddress);
			}
			return Dns.GetHostByName(hostNameOrAddress);
		}

		/// <summary>Resolves an IP address to an <see cref="T:System.Net.IPHostEntry" /> instance.</summary>
		/// <returns>An <see cref="T:System.Net.IPHostEntry" /> instance that contains address information about the host specified in <paramref name="address" />.</returns>
		/// <param name="address">An IP address.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error is encountered when resolving <paramref name="address" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="address" /> is an invalid IP address.</exception>
		// Token: 0x06003436 RID: 13366 RVA: 0x000B6148 File Offset: 0x000B4348
		public static IPHostEntry GetHostEntry(IPAddress address)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return Dns.GetHostByAddressFromString(address.ToString(), false);
		}

		/// <summary>Returns the Internet Protocol (IP) addresses for the specified host.</summary>
		/// <returns>An array of type <see cref="T:System.Net.IPAddress" /> that holds the IP addresses for the host that is specified by the <paramref name="hostNameOrAddress" /> parameter.</returns>
		/// <param name="hostNameOrAddress">The host name or IP address to resolve.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hostNameOrAddress" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="hostNameOrAddress" /> is greater than 255 characters. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error is encountered when resolving <paramref name="hostNameOrAddress" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="hostNameOrAddress" /> is an invalid IP address.</exception>
		// Token: 0x06003437 RID: 13367 RVA: 0x000B6234 File Offset: 0x000B4434
		public static IPAddress[] GetHostAddresses(string hostNameOrAddress)
		{
			if (hostNameOrAddress == null)
			{
				throw new ArgumentNullException("hostNameOrAddress");
			}
			if (hostNameOrAddress == "0.0.0.0" || hostNameOrAddress == "::0")
			{
				throw new ArgumentException("Addresses 0.0.0.0 (IPv4) and ::0 (IPv6) are unspecified addresses. You cannot use them as target address.", "hostNameOrAddress");
			}
			IPAddress ipaddress;
			if (hostNameOrAddress.Length > 0 && IPAddress.TryParse(hostNameOrAddress, out ipaddress))
			{
				return new IPAddress[] { ipaddress };
			}
			return Dns.GetHostEntry(hostNameOrAddress).AddressList;
		}

		/// <summary>Gets the DNS information for the specified DNS host name.</summary>
		/// <returns>An <see cref="T:System.Net.IPHostEntry" /> object that contains host information for the address specified in <paramref name="hostName" />.</returns>
		/// <param name="hostName">The DNS name of the host. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hostName" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="hostName" /> is greater than 255 characters. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error is encountered when resolving <paramref name="hostName" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Net.DnsPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003438 RID: 13368 RVA: 0x000B62A4 File Offset: 0x000B44A4
		[Obsolete("Use GetHostEntry instead")]
		public static IPHostEntry GetHostByName(string hostName)
		{
			if (hostName == null)
			{
				throw new ArgumentNullException("hostName");
			}
			string text;
			string[] array;
			string[] array2;
			if (!Dns.GetHostByName_icall(hostName, out text, out array, out array2, Socket.FamilyHint))
			{
				Dns.Error_11001(hostName);
			}
			return Dns.hostent_to_IPHostEntry(hostName, text, array, array2);
		}

		/// <summary>Gets the host name of the local computer.</summary>
		/// <returns>A string that contains the DNS host name of the local computer.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error is encountered when resolving the local host name. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Net.DnsPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003439 RID: 13369 RVA: 0x000B62E4 File Offset: 0x000B44E4
		public static string GetHostName()
		{
			string text;
			if (!Dns.GetHostName_icall(out text))
			{
				Dns.Error_11001(text);
			}
			return text;
		}

		/// <summary>Resolves a DNS host name or IP address to an <see cref="T:System.Net.IPHostEntry" /> instance.</summary>
		/// <returns>An <see cref="T:System.Net.IPHostEntry" /> instance that contains address information about the host specified in <paramref name="hostName" />.</returns>
		/// <param name="hostName">A DNS-style host name or IP address. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hostName" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="hostName" /> is greater than 255 characters. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error is encountered when resolving <paramref name="hostName" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Net.DnsPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600343A RID: 13370 RVA: 0x000B6304 File Offset: 0x000B4504
		[Obsolete("Use GetHostEntry instead")]
		public static IPHostEntry Resolve(string hostName)
		{
			if (hostName == null)
			{
				throw new ArgumentNullException("hostName");
			}
			IPHostEntry iphostEntry = null;
			try
			{
				iphostEntry = Dns.GetHostByAddress(hostName);
			}
			catch
			{
			}
			if (iphostEntry == null)
			{
				iphostEntry = Dns.GetHostByName(hostName);
			}
			return iphostEntry;
		}

		/// <summary>Returns the Internet Protocol (IP) addresses for the specified host as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns an array of type <see cref="T:System.Net.IPAddress" /> that holds the IP addresses for the host that is specified by the <paramref name="hostNameOrAddress" /> parameter.</returns>
		/// <param name="hostNameOrAddress">The host name or IP address to resolve.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hostNameOrAddress" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="hostNameOrAddress" /> is greater than 255 characters. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error is encountered when resolving <paramref name="hostNameOrAddress" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="hostNameOrAddress" /> is an invalid IP address.</exception>
		// Token: 0x0600343B RID: 13371 RVA: 0x000B6348 File Offset: 0x000B4548
		public static Task<IPAddress[]> GetHostAddressesAsync(string hostNameOrAddress)
		{
			return Task<IPAddress[]>.Factory.FromAsync<string>(new Func<string, AsyncCallback, object, IAsyncResult>(Dns.BeginGetHostAddresses), new Func<IAsyncResult, IPAddress[]>(Dns.EndGetHostAddresses), hostNameOrAddress, null);
		}

		/// <summary>Resolves an IP address to an <see cref="T:System.Net.IPHostEntry" /> instance as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns an <see cref="T:System.Net.IPHostEntry" /> instance that contains address information about the host specified in <paramref name="address" />.</returns>
		/// <param name="address">An IP address.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error is encountered when resolving <paramref name="address" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="address" /> is an invalid IP address.</exception>
		// Token: 0x0600343C RID: 13372 RVA: 0x000B636E File Offset: 0x000B456E
		public static Task<IPHostEntry> GetHostEntryAsync(IPAddress address)
		{
			return Task<IPHostEntry>.Factory.FromAsync<IPAddress>(new Func<IPAddress, AsyncCallback, object, IAsyncResult>(Dns.BeginGetHostEntry), new Func<IAsyncResult, IPHostEntry>(Dns.EndGetHostEntry), address, null);
		}

		/// <summary>Resolves a host name or IP address to an <see cref="T:System.Net.IPHostEntry" /> instance as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns an <see cref="T:System.Net.IPHostEntry" /> instance that contains address information about the host specified in <paramref name="hostNameOrAddress" />.</returns>
		/// <param name="hostNameOrAddress">The host name or IP address to resolve.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="hostNameOrAddress" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="hostNameOrAddress" /> parameter is greater than 255 characters. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error was encountered when resolving the <paramref name="hostNameOrAddress" /> parameter. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="hostNameOrAddress" /> parameter is an invalid IP address. </exception>
		// Token: 0x0600343D RID: 13373 RVA: 0x000B6394 File Offset: 0x000B4594
		public static Task<IPHostEntry> GetHostEntryAsync(string hostNameOrAddress)
		{
			return Task<IPHostEntry>.Factory.FromAsync<string>(new Func<string, AsyncCallback, object, IAsyncResult>(Dns.BeginGetHostEntry), new Func<IAsyncResult, IPHostEntry>(Dns.EndGetHostEntry), hostNameOrAddress, null);
		}

		// Token: 0x04001EA5 RID: 7845
		private static bool use_mono_dns;

		// Token: 0x04001EA6 RID: 7846
		private static SimpleResolver resolver;

		// Token: 0x0200067B RID: 1659
		// (Invoke) Token: 0x0600343F RID: 13375
		private delegate IPHostEntry GetHostByNameCallback(string hostName);

		// Token: 0x0200067C RID: 1660
		// (Invoke) Token: 0x06003443 RID: 13379
		private delegate IPHostEntry ResolveCallback(string hostName);

		// Token: 0x0200067D RID: 1661
		// (Invoke) Token: 0x06003447 RID: 13383
		private delegate IPHostEntry GetHostEntryNameCallback(string hostName);

		// Token: 0x0200067E RID: 1662
		// (Invoke) Token: 0x0600344B RID: 13387
		private delegate IPHostEntry GetHostEntryIPCallback(IPAddress hostAddress);

		// Token: 0x0200067F RID: 1663
		// (Invoke) Token: 0x0600344F RID: 13391
		private delegate IPAddress[] GetHostAddressesCallback(string hostName);
	}
}
