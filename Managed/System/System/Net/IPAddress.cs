using System;
using System.Buffers.Binary;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Net
{
	/// <summary>Provides an Internet Protocol (IP) address.</summary>
	// Token: 0x0200057F RID: 1407
	[Serializable]
	public class IPAddress
	{
		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x06002D5D RID: 11613 RVA: 0x0009B3C3 File Offset: 0x000995C3
		private bool IsIPv4
		{
			get
			{
				return this._numbers == null;
			}
		}

		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x06002D5E RID: 11614 RVA: 0x0009B3CE File Offset: 0x000995CE
		private bool IsIPv6
		{
			get
			{
				return this._numbers != null;
			}
		}

		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x06002D5F RID: 11615 RVA: 0x0009B3D9 File Offset: 0x000995D9
		// (set) Token: 0x06002D60 RID: 11616 RVA: 0x0009B3E1 File Offset: 0x000995E1
		private uint PrivateAddress
		{
			get
			{
				return this._addressOrScopeId;
			}
			set
			{
				this._toString = null;
				this._hashCode = 0;
				this._addressOrScopeId = value;
			}
		}

		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x06002D61 RID: 11617 RVA: 0x0009B3D9 File Offset: 0x000995D9
		// (set) Token: 0x06002D62 RID: 11618 RVA: 0x0009B3E1 File Offset: 0x000995E1
		private uint PrivateScopeId
		{
			get
			{
				return this._addressOrScopeId;
			}
			set
			{
				this._toString = null;
				this._hashCode = 0;
				this._addressOrScopeId = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.IPAddress" /> class with the address specified as an <see cref="T:System.Int64" />.</summary>
		/// <param name="newAddress">The long value of the IP address. For example, the value 0x2414188f in big-endian format would be the IP address "143.24.20.36". </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="newAddress" /> &lt; 0 or <paramref name="newAddress" /> &gt; 0x00000000FFFFFFFF </exception>
		// Token: 0x06002D63 RID: 11619 RVA: 0x0009B3F8 File Offset: 0x000995F8
		public IPAddress(long newAddress)
		{
			if (newAddress < 0L || newAddress > (long)((ulong)(-1)))
			{
				throw new ArgumentOutOfRangeException("newAddress");
			}
			this.PrivateAddress = (uint)newAddress;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.IPAddress" /> class with the address specified as a <see cref="T:System.Byte" /> array and the specified scope identifier.</summary>
		/// <param name="address">The byte array value of the IP address. </param>
		/// <param name="scopeid">The long value of the scope identifier. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="address" /> contains a bad IP address. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="scopeid" /> &lt; 0 or <paramref name="scopeid" /> &gt; 0x00000000FFFFFFFF </exception>
		// Token: 0x06002D64 RID: 11620 RVA: 0x0009B41D File Offset: 0x0009961D
		public IPAddress(byte[] address, long scopeid)
			: this(new ReadOnlySpan<byte>(address ?? IPAddress.ThrowAddressNullException()), scopeid)
		{
		}

		// Token: 0x06002D65 RID: 11621 RVA: 0x0009B438 File Offset: 0x00099638
		public unsafe IPAddress(ReadOnlySpan<byte> address, long scopeid)
		{
			if (address.Length != 16)
			{
				throw new ArgumentException("An invalid IP address was specified.", "address");
			}
			if (scopeid < 0L || scopeid > (long)((ulong)(-1)))
			{
				throw new ArgumentOutOfRangeException("scopeid");
			}
			this._numbers = new ushort[8];
			for (int i = 0; i < 8; i++)
			{
				this._numbers[i] = (ushort)((int)(*address[i * 2]) * 256 + (int)(*address[i * 2 + 1]));
			}
			this.PrivateScopeId = (uint)scopeid;
		}

		// Token: 0x06002D66 RID: 11622 RVA: 0x0009B4C4 File Offset: 0x000996C4
		internal unsafe IPAddress(ushort* numbers, int numbersLength, uint scopeid)
		{
			ushort[] array = new ushort[8];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = numbers[i];
			}
			this._numbers = array;
			this.PrivateScopeId = scopeid;
		}

		// Token: 0x06002D67 RID: 11623 RVA: 0x0009B504 File Offset: 0x00099704
		private IPAddress(ushort[] numbers, uint scopeid)
		{
			this._numbers = numbers;
			this.PrivateScopeId = scopeid;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.IPAddress" /> class with the address specified as a <see cref="T:System.Byte" /> array.</summary>
		/// <param name="address">The byte array value of the IP address. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="address" /> contains a bad IP address. </exception>
		// Token: 0x06002D68 RID: 11624 RVA: 0x0009B51A File Offset: 0x0009971A
		public IPAddress(byte[] address)
			: this(new ReadOnlySpan<byte>(address ?? IPAddress.ThrowAddressNullException()))
		{
		}

		// Token: 0x06002D69 RID: 11625 RVA: 0x0009B534 File Offset: 0x00099734
		public unsafe IPAddress(ReadOnlySpan<byte> address)
		{
			if (address.Length == 4)
			{
				this.PrivateAddress = (uint)((long)(((int)(*address[3]) << 24) | ((int)(*address[2]) << 16) | ((int)(*address[1]) << 8) | (int)(*address[0])) & (long)((ulong)(-1)));
				return;
			}
			if (address.Length == 16)
			{
				this._numbers = new ushort[8];
				for (int i = 0; i < 8; i++)
				{
					this._numbers[i] = (ushort)((int)(*address[i * 2]) * 256 + (int)(*address[i * 2 + 1]));
				}
				return;
			}
			throw new ArgumentException("An invalid IP address was specified.", "address");
		}

		// Token: 0x06002D6A RID: 11626 RVA: 0x0009B5E7 File Offset: 0x000997E7
		internal IPAddress(int newAddress)
		{
			this.PrivateAddress = (uint)newAddress;
		}

		/// <summary>Determines whether a string is a valid IP address.</summary>
		/// <returns>true if <paramref name="ipString" /> is a valid IP address; otherwise, false.</returns>
		/// <param name="ipString">The string to validate.</param>
		/// <param name="address">The <see cref="T:System.Net.IPAddress" /> version of the string.</param>
		// Token: 0x06002D6B RID: 11627 RVA: 0x0009B5F6 File Offset: 0x000997F6
		public static bool TryParse(string ipString, out IPAddress address)
		{
			if (ipString == null)
			{
				address = null;
				return false;
			}
			address = IPAddressParser.Parse(ipString.AsSpan(), true);
			return address != null;
		}

		// Token: 0x06002D6C RID: 11628 RVA: 0x0009B613 File Offset: 0x00099813
		public static bool TryParse(ReadOnlySpan<char> ipSpan, out IPAddress address)
		{
			address = IPAddressParser.Parse(ipSpan, true);
			return address != null;
		}

		/// <summary>Converts an IP address string to an <see cref="T:System.Net.IPAddress" /> instance.</summary>
		/// <returns>An <see cref="T:System.Net.IPAddress" /> instance.</returns>
		/// <param name="ipString">A string that contains an IP address in dotted-quad notation for IPv4 and in colon-hexadecimal notation for IPv6. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="ipString" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="ipString" /> is not a valid IP address. </exception>
		// Token: 0x06002D6D RID: 11629 RVA: 0x0009B623 File Offset: 0x00099823
		public static IPAddress Parse(string ipString)
		{
			if (ipString == null)
			{
				throw new ArgumentNullException("ipString");
			}
			return IPAddressParser.Parse(ipString.AsSpan(), false);
		}

		// Token: 0x06002D6E RID: 11630 RVA: 0x0009B63F File Offset: 0x0009983F
		public static IPAddress Parse(ReadOnlySpan<char> ipSpan)
		{
			return IPAddressParser.Parse(ipSpan, false);
		}

		// Token: 0x06002D6F RID: 11631 RVA: 0x0009B648 File Offset: 0x00099848
		public bool TryWriteBytes(Span<byte> destination, out int bytesWritten)
		{
			if (this.IsIPv6)
			{
				if (destination.Length < 16)
				{
					bytesWritten = 0;
					return false;
				}
				this.WriteIPv6Bytes(destination);
				bytesWritten = 16;
			}
			else
			{
				if (destination.Length < 4)
				{
					bytesWritten = 0;
					return false;
				}
				this.WriteIPv4Bytes(destination);
				bytesWritten = 4;
			}
			return true;
		}

		// Token: 0x06002D70 RID: 11632 RVA: 0x0009B694 File Offset: 0x00099894
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe void WriteIPv6Bytes(Span<byte> destination)
		{
			int num = 0;
			for (int i = 0; i < 8; i++)
			{
				*destination[num++] = (byte)((this._numbers[i] >> 8) & 255);
				*destination[num++] = (byte)(this._numbers[i] & 255);
			}
		}

		// Token: 0x06002D71 RID: 11633 RVA: 0x0009B6EC File Offset: 0x000998EC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe void WriteIPv4Bytes(Span<byte> destination)
		{
			uint privateAddress = this.PrivateAddress;
			*destination[0] = (byte)privateAddress;
			*destination[1] = (byte)(privateAddress >> 8);
			*destination[2] = (byte)(privateAddress >> 16);
			*destination[3] = (byte)(privateAddress >> 24);
		}

		/// <summary>Provides a copy of the <see cref="T:System.Net.IPAddress" /> as an array of bytes.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array.</returns>
		// Token: 0x06002D72 RID: 11634 RVA: 0x0009B734 File Offset: 0x00099934
		public byte[] GetAddressBytes()
		{
			if (this.IsIPv6)
			{
				byte[] array = new byte[16];
				this.WriteIPv6Bytes(array);
				return array;
			}
			byte[] array2 = new byte[4];
			this.WriteIPv4Bytes(array2);
			return array2;
		}

		/// <summary>Gets the address family of the IP address.</summary>
		/// <returns>Returns <see cref="F:System.Net.Sockets.AddressFamily.InterNetwork" /> for IPv4 or <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6" /> for IPv6.</returns>
		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x06002D73 RID: 11635 RVA: 0x0009B773 File Offset: 0x00099973
		public AddressFamily AddressFamily
		{
			get
			{
				if (!this.IsIPv4)
				{
					return AddressFamily.InterNetworkV6;
				}
				return AddressFamily.InterNetwork;
			}
		}

		/// <summary>Gets or sets the IPv6 address scope identifier.</summary>
		/// <returns>A long integer that specifies the scope of the address.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">AddressFamily = InterNetwork. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="scopeId" /> &lt; 0- or -<paramref name="scopeId" /> &gt; 0x00000000FFFFFFFF  </exception>
		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x06002D74 RID: 11636 RVA: 0x0009B781 File Offset: 0x00099981
		// (set) Token: 0x06002D75 RID: 11637 RVA: 0x0009B79D File Offset: 0x0009999D
		public long ScopeId
		{
			get
			{
				if (this.IsIPv4)
				{
					throw new SocketException(SocketError.OperationNotSupported);
				}
				return (long)((ulong)this.PrivateScopeId);
			}
			set
			{
				if (this.IsIPv4)
				{
					throw new SocketException(SocketError.OperationNotSupported);
				}
				if (value < 0L || value > (long)((ulong)(-1)))
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.PrivateScopeId = (uint)value;
			}
		}

		/// <summary>Converts an Internet address to its standard notation.</summary>
		/// <returns>A string that contains the IP address in either IPv4 dotted-quad or in IPv6 colon-hexadecimal notation.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">The address family is <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6" /> and the address is bad. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002D76 RID: 11638 RVA: 0x0009B7CF File Offset: 0x000999CF
		public override string ToString()
		{
			if (this._toString == null)
			{
				this._toString = (this.IsIPv4 ? IPAddressParser.IPv4AddressToString(this.PrivateAddress) : IPAddressParser.IPv6AddressToString(this._numbers, this.PrivateScopeId));
			}
			return this._toString;
		}

		// Token: 0x06002D77 RID: 11639 RVA: 0x0009B80B File Offset: 0x00099A0B
		public bool TryFormat(Span<char> destination, out int charsWritten)
		{
			if (!this.IsIPv4)
			{
				return IPAddressParser.IPv6AddressToString(this._numbers, this.PrivateScopeId, destination, out charsWritten);
			}
			return IPAddressParser.IPv4AddressToString(this.PrivateAddress, destination, out charsWritten);
		}

		/// <summary>Converts a long value from host byte order to network byte order.</summary>
		/// <returns>A long value, expressed in network byte order.</returns>
		/// <param name="host">The number to convert, expressed in host byte order. </param>
		// Token: 0x06002D78 RID: 11640 RVA: 0x0009B836 File Offset: 0x00099A36
		public static long HostToNetworkOrder(long host)
		{
			if (!BitConverter.IsLittleEndian)
			{
				return host;
			}
			return BinaryPrimitives.ReverseEndianness(host);
		}

		/// <summary>Converts an integer value from host byte order to network byte order.</summary>
		/// <returns>An integer value, expressed in network byte order.</returns>
		/// <param name="host">The number to convert, expressed in host byte order. </param>
		// Token: 0x06002D79 RID: 11641 RVA: 0x0009B847 File Offset: 0x00099A47
		public static int HostToNetworkOrder(int host)
		{
			if (!BitConverter.IsLittleEndian)
			{
				return host;
			}
			return BinaryPrimitives.ReverseEndianness(host);
		}

		/// <summary>Converts a short value from host byte order to network byte order.</summary>
		/// <returns>A short value, expressed in network byte order.</returns>
		/// <param name="host">The number to convert, expressed in host byte order. </param>
		// Token: 0x06002D7A RID: 11642 RVA: 0x0009B858 File Offset: 0x00099A58
		public static short HostToNetworkOrder(short host)
		{
			if (!BitConverter.IsLittleEndian)
			{
				return host;
			}
			return BinaryPrimitives.ReverseEndianness(host);
		}

		/// <summary>Converts a long value from network byte order to host byte order.</summary>
		/// <returns>A long value, expressed in host byte order.</returns>
		/// <param name="network">The number to convert, expressed in network byte order. </param>
		// Token: 0x06002D7B RID: 11643 RVA: 0x0009B869 File Offset: 0x00099A69
		public static long NetworkToHostOrder(long network)
		{
			return IPAddress.HostToNetworkOrder(network);
		}

		/// <summary>Converts an integer value from network byte order to host byte order.</summary>
		/// <returns>An integer value, expressed in host byte order.</returns>
		/// <param name="network">The number to convert, expressed in network byte order. </param>
		// Token: 0x06002D7C RID: 11644 RVA: 0x0009B871 File Offset: 0x00099A71
		public static int NetworkToHostOrder(int network)
		{
			return IPAddress.HostToNetworkOrder(network);
		}

		/// <summary>Converts a short value from network byte order to host byte order.</summary>
		/// <returns>A short value, expressed in host byte order.</returns>
		/// <param name="network">The number to convert, expressed in network byte order. </param>
		// Token: 0x06002D7D RID: 11645 RVA: 0x0009B879 File Offset: 0x00099A79
		public static short NetworkToHostOrder(short network)
		{
			return IPAddress.HostToNetworkOrder(network);
		}

		/// <summary>Indicates whether the specified IP address is the loopback address.</summary>
		/// <returns>true if <paramref name="address" /> is the loopback address; otherwise, false.</returns>
		/// <param name="address">An IP address. </param>
		// Token: 0x06002D7E RID: 11646 RVA: 0x0009B884 File Offset: 0x00099A84
		public static bool IsLoopback(IPAddress address)
		{
			if (address == null)
			{
				IPAddress.ThrowAddressNullException();
			}
			if (address.IsIPv6)
			{
				return address.Equals(IPAddress.IPv6Loopback);
			}
			return ((ulong)address.PrivateAddress & 255UL) == ((ulong)IPAddress.Loopback.PrivateAddress & 255UL);
		}

		/// <summary>Gets whether the address is an IPv6 multicast global address.</summary>
		/// <returns>true if the IP address is an IPv6 multicast global address; otherwise, false.</returns>
		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x06002D7F RID: 11647 RVA: 0x0009B8D0 File Offset: 0x00099AD0
		public bool IsIPv6Multicast
		{
			get
			{
				return this.IsIPv6 && (this._numbers[0] & 65280) == 65280;
			}
		}

		/// <summary>Gets whether the address is an IPv6 link local address.</summary>
		/// <returns>true if the IP address is an IPv6 link local address; otherwise, false.</returns>
		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x06002D80 RID: 11648 RVA: 0x0009B8F1 File Offset: 0x00099AF1
		public bool IsIPv6LinkLocal
		{
			get
			{
				return this.IsIPv6 && (this._numbers[0] & 65472) == 65152;
			}
		}

		/// <summary>Gets whether the address is an IPv6 site local address.</summary>
		/// <returns>true if the IP address is an IPv6 site local address; otherwise, false.</returns>
		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x06002D81 RID: 11649 RVA: 0x0009B912 File Offset: 0x00099B12
		public bool IsIPv6SiteLocal
		{
			get
			{
				return this.IsIPv6 && (this._numbers[0] & 65472) == 65216;
			}
		}

		/// <summary>Gets whether the address is an IPv6 Teredo address.</summary>
		/// <returns>true if the IP address is an IPv6 Teredo address; otherwise, false.</returns>
		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x06002D82 RID: 11650 RVA: 0x0009B933 File Offset: 0x00099B33
		public bool IsIPv6Teredo
		{
			get
			{
				return this.IsIPv6 && this._numbers[0] == 8193 && this._numbers[1] == 0;
			}
		}

		/// <summary>Gets whether the IP address is an IPv4-mapped IPv6 address.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the IP address is an IPv4-mapped IPv6 address; otherwise, false.</returns>
		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x06002D83 RID: 11651 RVA: 0x0009B95C File Offset: 0x00099B5C
		public bool IsIPv4MappedToIPv6
		{
			get
			{
				if (this.IsIPv4)
				{
					return false;
				}
				for (int i = 0; i < 5; i++)
				{
					if (this._numbers[i] != 0)
					{
						return false;
					}
				}
				return this._numbers[5] == ushort.MaxValue;
			}
		}

		/// <summary>An Internet Protocol (IP) address.</summary>
		/// <returns>The long value of the IP address.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">The address family is <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6" />. </exception>
		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x06002D84 RID: 11652 RVA: 0x0009B99A File Offset: 0x00099B9A
		// (set) Token: 0x06002D85 RID: 11653 RVA: 0x0009B9B8 File Offset: 0x00099BB8
		[Obsolete("This property has been deprecated. It is address family dependent. Please use IPAddress.Equals method to perform comparisons. https://go.microsoft.com/fwlink/?linkid=14202")]
		public long Address
		{
			get
			{
				if (this.AddressFamily == AddressFamily.InterNetworkV6)
				{
					throw new SocketException(SocketError.OperationNotSupported);
				}
				return (long)((ulong)this.PrivateAddress);
			}
			set
			{
				if (this.AddressFamily == AddressFamily.InterNetworkV6)
				{
					throw new SocketException(SocketError.OperationNotSupported);
				}
				if ((ulong)this.PrivateAddress != (ulong)value)
				{
					if (this is IPAddress.ReadOnlyIPAddress)
					{
						throw new SocketException(SocketError.OperationNotSupported);
					}
					this.PrivateAddress = (uint)value;
				}
			}
		}

		// Token: 0x06002D86 RID: 11654 RVA: 0x0009B9F4 File Offset: 0x00099BF4
		internal bool Equals(object comparandObj, bool compareScopeId)
		{
			IPAddress ipaddress = comparandObj as IPAddress;
			if (ipaddress == null)
			{
				return false;
			}
			if (this.AddressFamily != ipaddress.AddressFamily)
			{
				return false;
			}
			if (this.IsIPv6)
			{
				for (int i = 0; i < 8; i++)
				{
					if (ipaddress._numbers[i] != this._numbers[i])
					{
						return false;
					}
				}
				return ipaddress.PrivateScopeId == this.PrivateScopeId || !compareScopeId;
			}
			return ipaddress.PrivateAddress == this.PrivateAddress;
		}

		/// <summary>Compares two IP addresses.</summary>
		/// <returns>true if the two addresses are equal; otherwise, false.</returns>
		/// <param name="comparand">An <see cref="T:System.Net.IPAddress" /> instance to compare to the current instance. </param>
		// Token: 0x06002D87 RID: 11655 RVA: 0x0009BA68 File Offset: 0x00099C68
		public override bool Equals(object comparand)
		{
			return this.Equals(comparand, true);
		}

		/// <summary>Returns a hash value for an IP address.</summary>
		/// <returns>An integer hash value.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002D88 RID: 11656 RVA: 0x0009BA74 File Offset: 0x00099C74
		public unsafe override int GetHashCode()
		{
			if (this._hashCode != 0)
			{
				return this._hashCode;
			}
			int num;
			if (this.IsIPv6)
			{
				Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)20], 20);
				MemoryMarshal.AsBytes<ushort>(new ReadOnlySpan<ushort>(this._numbers)).CopyTo(span);
				BitConverter.TryWriteBytes(span.Slice(16), this._addressOrScopeId);
				num = Marvin.ComputeHash32(span, Marvin.DefaultSeed);
			}
			else
			{
				num = Marvin.ComputeHash32(MemoryMarshal.AsBytes<uint>(MemoryMarshal.CreateReadOnlySpan<uint>(ref this._addressOrScopeId, 1)), Marvin.DefaultSeed);
			}
			this._hashCode = num;
			return this._hashCode;
		}

		/// <summary>Maps the <see cref="T:System.Net.IPAddress" /> object to an IPv6 address.</summary>
		/// <returns>Returns <see cref="T:System.Net.IPAddress" />.An IPv6 address.</returns>
		// Token: 0x06002D89 RID: 11657 RVA: 0x0009BB10 File Offset: 0x00099D10
		public IPAddress MapToIPv6()
		{
			if (this.IsIPv6)
			{
				return this;
			}
			uint privateAddress = this.PrivateAddress;
			return new IPAddress(new ushort[]
			{
				0,
				0,
				0,
				0,
				0,
				ushort.MaxValue,
				(ushort)(((privateAddress & 65280U) >> 8) | ((privateAddress & 255U) << 8)),
				(ushort)(((privateAddress & 4278190080U) >> 24) | ((privateAddress & 16711680U) >> 8))
			}, 0U);
		}

		/// <summary>Maps the <see cref="T:System.Net.IPAddress" /> object to an IPv4 address.</summary>
		/// <returns>Returns <see cref="T:System.Net.IPAddress" />.An IPv4 address.</returns>
		// Token: 0x06002D8A RID: 11658 RVA: 0x0009BB74 File Offset: 0x00099D74
		public IPAddress MapToIPv4()
		{
			if (this.IsIPv4)
			{
				return this;
			}
			return new IPAddress((long)((ulong)(((uint)(this._numbers[6] & 65280) >> 8) | (uint)((uint)(this._numbers[6] & 255) << 8) | ((((uint)(this._numbers[7] & 65280) >> 8) | (uint)((uint)(this._numbers[7] & 255) << 8)) << 16))));
		}

		// Token: 0x06002D8B RID: 11659 RVA: 0x0009BBD7 File Offset: 0x00099DD7
		private static byte[] ThrowAddressNullException()
		{
			throw new ArgumentNullException("address");
		}

		/// <summary>Provides an IP address that indicates that the server must listen for client activity on all network interfaces. This field is read-only.</summary>
		// Token: 0x04001906 RID: 6406
		public static readonly IPAddress Any = new IPAddress.ReadOnlyIPAddress(0L);

		/// <summary>Provides the IP loopback address. This field is read-only.</summary>
		// Token: 0x04001907 RID: 6407
		public static readonly IPAddress Loopback = new IPAddress.ReadOnlyIPAddress(16777343L);

		/// <summary>Provides the IP broadcast address. This field is read-only.</summary>
		// Token: 0x04001908 RID: 6408
		public static readonly IPAddress Broadcast = new IPAddress.ReadOnlyIPAddress((long)((ulong)(-1)));

		/// <summary>Provides an IP address that indicates that no network interface should be used. This field is read-only.</summary>
		// Token: 0x04001909 RID: 6409
		public static readonly IPAddress None = IPAddress.Broadcast;

		// Token: 0x0400190A RID: 6410
		internal const long LoopbackMask = 255L;

		/// <summary>The <see cref="M:System.Net.Sockets.Socket.Bind(System.Net.EndPoint)" /> method uses the <see cref="F:System.Net.IPAddress.IPv6Any" /> field to indicate that a <see cref="T:System.Net.Sockets.Socket" /> must listen for client activity on all network interfaces.</summary>
		// Token: 0x0400190B RID: 6411
		public static readonly IPAddress IPv6Any = new IPAddress(new byte[16], 0L);

		/// <summary>Provides the IP loopback address. This property is read-only.</summary>
		// Token: 0x0400190C RID: 6412
		public static readonly IPAddress IPv6Loopback = new IPAddress(new byte[]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 1
		}, 0L);

		/// <summary>Provides an IP address that indicates that no network interface should be used. This property is read-only.</summary>
		// Token: 0x0400190D RID: 6413
		public static readonly IPAddress IPv6None = new IPAddress(new byte[16], 0L);

		// Token: 0x0400190E RID: 6414
		private uint _addressOrScopeId;

		// Token: 0x0400190F RID: 6415
		private readonly ushort[] _numbers;

		// Token: 0x04001910 RID: 6416
		private string _toString;

		// Token: 0x04001911 RID: 6417
		private int _hashCode;

		// Token: 0x04001912 RID: 6418
		internal const int NumberOfLabels = 8;

		// Token: 0x02000580 RID: 1408
		private sealed class ReadOnlyIPAddress : IPAddress
		{
			// Token: 0x06002D8D RID: 11661 RVA: 0x0009BC61 File Offset: 0x00099E61
			public ReadOnlyIPAddress(long newAddress)
				: base(newAddress)
			{
			}
		}
	}
}
