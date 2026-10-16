using System;

namespace System.Net.Sockets
{
	/// <summary>Presents UDP receive result information from a call to the <see cref="M:System.Net.Sockets.UdpClient.ReceiveAsync" /> method.</summary>
	// Token: 0x020007C2 RID: 1986
	public struct UdpReceiveResult : IEquatable<UdpReceiveResult>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.UdpReceiveResult" /> class.</summary>
		/// <param name="buffer">A buffer for data to receive in the UDP packet.</param>
		/// <param name="remoteEndPoint">The remote endpoint of the UDP packet.</param>
		// Token: 0x06003F56 RID: 16214 RVA: 0x000D8699 File Offset: 0x000D6899
		public UdpReceiveResult(byte[] buffer, IPEndPoint remoteEndPoint)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (remoteEndPoint == null)
			{
				throw new ArgumentNullException("remoteEndPoint");
			}
			this.m_buffer = buffer;
			this.m_remoteEndPoint = remoteEndPoint;
		}

		/// <summary>Gets a buffer with the data received in the UDP packet.</summary>
		/// <returns>Returns <see cref="T:System.Byte" />.A <see cref="T:System.Byte" /> array with the data received in the UDP packet.</returns>
		// Token: 0x17000E5B RID: 3675
		// (get) Token: 0x06003F57 RID: 16215 RVA: 0x000D86C5 File Offset: 0x000D68C5
		public byte[] Buffer
		{
			get
			{
				return this.m_buffer;
			}
		}

		/// <summary>Gets the remote endpoint from which the UDP packet was received. </summary>
		/// <returns>Returns <see cref="T:System.Net.IPEndPoint" />.The remote endpoint from which the UDP packet was received.</returns>
		// Token: 0x17000E5C RID: 3676
		// (get) Token: 0x06003F58 RID: 16216 RVA: 0x000D86CD File Offset: 0x000D68CD
		public IPEndPoint RemoteEndPoint
		{
			get
			{
				return this.m_remoteEndPoint;
			}
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>Returns <see cref="T:System.Int32" />.The hash code.</returns>
		// Token: 0x06003F59 RID: 16217 RVA: 0x000D86D5 File Offset: 0x000D68D5
		public override int GetHashCode()
		{
			if (this.m_buffer == null)
			{
				return 0;
			}
			return this.m_buffer.GetHashCode() ^ this.m_remoteEndPoint.GetHashCode();
		}

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if <paramref name="obj" /> is an instance of <see cref="T:System.Net.Sockets.UdpReceiveResult" /> and equals the value of the instance; otherwise, false.</returns>
		/// <param name="obj">The object to compare with this instance.</param>
		// Token: 0x06003F5A RID: 16218 RVA: 0x000D86F8 File Offset: 0x000D68F8
		public override bool Equals(object obj)
		{
			return obj is UdpReceiveResult && this.Equals((UdpReceiveResult)obj);
		}

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if <paramref name="other" /> is an instance of <see cref="T:System.Net.Sockets.UdpReceiveResult" /> and equals the value of the instance; otherwise, false.</returns>
		/// <param name="other">The object to compare with this instance.</param>
		// Token: 0x06003F5B RID: 16219 RVA: 0x000D8710 File Offset: 0x000D6910
		public bool Equals(UdpReceiveResult other)
		{
			return object.Equals(this.m_buffer, other.m_buffer) && object.Equals(this.m_remoteEndPoint, other.m_remoteEndPoint);
		}

		/// <summary>Tests whether two specified <see cref="T:System.Net.Sockets.UdpReceiveResult" /> instances are equivalent.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if <paramref name="left" /> and <paramref name="right" /> are equal; otherwise, false.</returns>
		/// <param name="left">The <see cref="T:System.Net.Sockets.UdpReceiveResult" /> instance that is to the left of the equality operator.</param>
		/// <param name="right">The <see cref="T:System.Net.Sockets.UdpReceiveResult" /> instance that is to the right of the equality operator.</param>
		// Token: 0x06003F5C RID: 16220 RVA: 0x000D8738 File Offset: 0x000D6938
		public static bool operator ==(UdpReceiveResult left, UdpReceiveResult right)
		{
			return left.Equals(right);
		}

		/// <summary>Tests whether two specified <see cref="T:System.Net.Sockets.UdpReceiveResult" /> instances are not equal.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if <paramref name="left" /> and <paramref name="right" /> are unequal; otherwise, false.</returns>
		/// <param name="left">The <see cref="T:System.Net.Sockets.UdpReceiveResult" /> instance that is to the left of the not equal operator.</param>
		/// <param name="right">The <see cref="T:System.Net.Sockets.UdpReceiveResult" /> instance that is to the right of the not equal operator.</param>
		// Token: 0x06003F5D RID: 16221 RVA: 0x000D8742 File Offset: 0x000D6942
		public static bool operator !=(UdpReceiveResult left, UdpReceiveResult right)
		{
			return !left.Equals(right);
		}

		// Token: 0x040025F4 RID: 9716
		private byte[] m_buffer;

		// Token: 0x040025F5 RID: 9717
		private IPEndPoint m_remoteEndPoint;
	}
}
