using System;
using System.Globalization;
using System.Net.Sockets;
using System.Text;

namespace System.Net
{
	/// <summary>Stores serialized information from <see cref="T:System.Net.EndPoint" /> derived classes.</summary>
	// Token: 0x02000604 RID: 1540
	public class SocketAddress
	{
		/// <summary>Gets the <see cref="T:System.Net.Sockets.AddressFamily" /> enumerated value of the current <see cref="T:System.Net.SocketAddress" />.</summary>
		/// <returns>One of the <see cref="T:System.Net.Sockets.AddressFamily" /> enumerated values.</returns>
		// Token: 0x170009B7 RID: 2487
		// (get) Token: 0x0600309F RID: 12447 RVA: 0x000A7185 File Offset: 0x000A5385
		public AddressFamily Family
		{
			get
			{
				return (AddressFamily)((int)this.m_Buffer[0] | ((int)this.m_Buffer[1] << 8));
			}
		}

		/// <summary>Gets the underlying buffer size of the <see cref="T:System.Net.SocketAddress" />.</summary>
		/// <returns>The underlying buffer size of the <see cref="T:System.Net.SocketAddress" />.</returns>
		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x060030A0 RID: 12448 RVA: 0x000A719A File Offset: 0x000A539A
		public int Size
		{
			get
			{
				return this.m_Size;
			}
		}

		/// <summary>Gets or sets the specified index element in the underlying buffer.</summary>
		/// <returns>The value of the specified index element in the underlying buffer.</returns>
		/// <param name="offset">The array index element of the desired information. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The specified index does not exist in the buffer. </exception>
		// Token: 0x170009B9 RID: 2489
		public byte this[int offset]
		{
			get
			{
				if (offset < 0 || offset >= this.Size)
				{
					throw new IndexOutOfRangeException();
				}
				return this.m_Buffer[offset];
			}
			set
			{
				if (offset < 0 || offset >= this.Size)
				{
					throw new IndexOutOfRangeException();
				}
				if (this.m_Buffer[offset] != value)
				{
					this.m_changed = true;
				}
				this.m_Buffer[offset] = value;
			}
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Net.SocketAddress" /> class for the given address family.</summary>
		/// <param name="family">An <see cref="T:System.Net.Sockets.AddressFamily" /> enumerated value. </param>
		// Token: 0x060030A3 RID: 12451 RVA: 0x000A71EF File Offset: 0x000A53EF
		public SocketAddress(AddressFamily family)
			: this(family, 32)
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Net.SocketAddress" /> class using the specified address family and buffer size.</summary>
		/// <param name="family">An <see cref="T:System.Net.Sockets.AddressFamily" /> enumerated value. </param>
		/// <param name="size">The number of bytes to allocate for the underlying buffer. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="size" /> is less than 2. These 2 bytes are needed to store <paramref name="family" />. </exception>
		// Token: 0x060030A4 RID: 12452 RVA: 0x000A71FC File Offset: 0x000A53FC
		public SocketAddress(AddressFamily family, int size)
		{
			if (size < 2)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			this.m_Size = size;
			this.m_Buffer = new byte[(size / IntPtr.Size + 2) * IntPtr.Size];
			this.m_Buffer[0] = (byte)family;
			this.m_Buffer[1] = (byte)(family >> 8);
		}

		// Token: 0x060030A5 RID: 12453 RVA: 0x000A725C File Offset: 0x000A545C
		internal SocketAddress(IPAddress ipAddress)
			: this(ipAddress.AddressFamily, (ipAddress.AddressFamily == AddressFamily.InterNetwork) ? 16 : 28)
		{
			this.m_Buffer[2] = 0;
			this.m_Buffer[3] = 0;
			if (ipAddress.AddressFamily == AddressFamily.InterNetworkV6)
			{
				this.m_Buffer[4] = 0;
				this.m_Buffer[5] = 0;
				this.m_Buffer[6] = 0;
				this.m_Buffer[7] = 0;
				long scopeId = ipAddress.ScopeId;
				this.m_Buffer[24] = (byte)scopeId;
				this.m_Buffer[25] = (byte)(scopeId >> 8);
				this.m_Buffer[26] = (byte)(scopeId >> 16);
				this.m_Buffer[27] = (byte)(scopeId >> 24);
				byte[] addressBytes = ipAddress.GetAddressBytes();
				for (int i = 0; i < addressBytes.Length; i++)
				{
					this.m_Buffer[8 + i] = addressBytes[i];
				}
				return;
			}
			int num;
			ipAddress.TryWriteBytes(this.m_Buffer.AsSpan<byte>(4), out num);
		}

		// Token: 0x060030A6 RID: 12454 RVA: 0x000A733A File Offset: 0x000A553A
		internal SocketAddress(IPAddress ipaddress, int port)
			: this(ipaddress)
		{
			this.m_Buffer[2] = (byte)(port >> 8);
			this.m_Buffer[3] = (byte)port;
		}

		// Token: 0x060030A7 RID: 12455 RVA: 0x000A735C File Offset: 0x000A555C
		internal IPAddress GetIPAddress()
		{
			if (this.Family == AddressFamily.InterNetworkV6)
			{
				byte[] array = new byte[16];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this.m_Buffer[i + 8];
				}
				long num = (long)(((int)this.m_Buffer[27] << 24) + ((int)this.m_Buffer[26] << 16) + ((int)this.m_Buffer[25] << 8) + (int)this.m_Buffer[24]);
				return new IPAddress(array, num);
			}
			if (this.Family == AddressFamily.InterNetwork)
			{
				return new IPAddress((long)((int)(this.m_Buffer[4] & byte.MaxValue) | (((int)this.m_Buffer[5] << 8) & 65280) | (((int)this.m_Buffer[6] << 16) & 16711680) | ((int)this.m_Buffer[7] << 24)) & (long)((ulong)(-1)));
			}
			throw new SocketException(SocketError.AddressFamilyNotSupported);
		}

		// Token: 0x060030A8 RID: 12456 RVA: 0x000A742C File Offset: 0x000A562C
		internal IPEndPoint GetIPEndPoint()
		{
			IPAddress ipaddress = this.GetIPAddress();
			int num = (((int)this.m_Buffer[2] << 8) & 65280) | (int)this.m_Buffer[3];
			return new IPEndPoint(ipaddress, num);
		}

		// Token: 0x060030A9 RID: 12457 RVA: 0x000A7460 File Offset: 0x000A5660
		internal void CopyAddressSizeIntoBuffer()
		{
			this.m_Buffer[this.m_Buffer.Length - IntPtr.Size] = (byte)this.m_Size;
			this.m_Buffer[this.m_Buffer.Length - IntPtr.Size + 1] = (byte)(this.m_Size >> 8);
			this.m_Buffer[this.m_Buffer.Length - IntPtr.Size + 2] = (byte)(this.m_Size >> 16);
			this.m_Buffer[this.m_Buffer.Length - IntPtr.Size + 3] = (byte)(this.m_Size >> 24);
		}

		// Token: 0x060030AA RID: 12458 RVA: 0x000A74EB File Offset: 0x000A56EB
		internal int GetAddressSizeOffset()
		{
			return this.m_Buffer.Length - IntPtr.Size;
		}

		// Token: 0x060030AB RID: 12459 RVA: 0x000A74FB File Offset: 0x000A56FB
		internal unsafe void SetSize(IntPtr ptr)
		{
			this.m_Size = *(int*)(void*)ptr;
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Net.SocketAddress" /> instance.</summary>
		/// <returns>true if the specified object  is equal to the current object; otherwise, false.</returns>
		/// <param name="comparand">The specified <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Net.SocketAddress" /> instance.</param>
		// Token: 0x060030AC RID: 12460 RVA: 0x000A750C File Offset: 0x000A570C
		public override bool Equals(object comparand)
		{
			SocketAddress socketAddress = comparand as SocketAddress;
			if (socketAddress == null || this.Size != socketAddress.Size)
			{
				return false;
			}
			for (int i = 0; i < this.Size; i++)
			{
				if (this[i] != socketAddress[i])
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Serves as a hash function for a particular type, suitable for use in hashing algorithms and data structures like a hash table.</summary>
		/// <returns>A hash code for the current object.</returns>
		// Token: 0x060030AD RID: 12461 RVA: 0x000A7558 File Offset: 0x000A5758
		public override int GetHashCode()
		{
			if (this.m_changed)
			{
				this.m_changed = false;
				this.m_hash = 0;
				int num = this.Size & -4;
				int i;
				for (i = 0; i < num; i += 4)
				{
					this.m_hash ^= (int)this.m_Buffer[i] | ((int)this.m_Buffer[i + 1] << 8) | ((int)this.m_Buffer[i + 2] << 16) | ((int)this.m_Buffer[i + 3] << 24);
				}
				if ((this.Size & 3) != 0)
				{
					int num2 = 0;
					int num3 = 0;
					while (i < this.Size)
					{
						num2 |= (int)this.m_Buffer[i] << num3;
						num3 += 8;
						i++;
					}
					this.m_hash ^= num2;
				}
			}
			return this.m_hash;
		}

		/// <summary>Returns information about the socket address.</summary>
		/// <returns>A string that contains information about the <see cref="T:System.Net.SocketAddress" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060030AE RID: 12462 RVA: 0x000A7618 File Offset: 0x000A5818
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 2; i < this.Size; i++)
			{
				if (i > 2)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append(this[i].ToString(NumberFormatInfo.InvariantInfo));
			}
			return string.Concat(new string[]
			{
				this.Family.ToString(),
				":",
				this.Size.ToString(NumberFormatInfo.InvariantInfo),
				":{",
				stringBuilder.ToString(),
				"}"
			});
		}

		// Token: 0x04001C53 RID: 7251
		internal const int IPv6AddressSize = 28;

		// Token: 0x04001C54 RID: 7252
		internal const int IPv4AddressSize = 16;

		// Token: 0x04001C55 RID: 7253
		internal int m_Size;

		// Token: 0x04001C56 RID: 7254
		internal byte[] m_Buffer;

		// Token: 0x04001C57 RID: 7255
		private const int WriteableOffset = 2;

		// Token: 0x04001C58 RID: 7256
		private const int MaxSize = 32;

		// Token: 0x04001C59 RID: 7257
		private bool m_changed = true;

		// Token: 0x04001C5A RID: 7258
		private int m_hash;
	}
}
