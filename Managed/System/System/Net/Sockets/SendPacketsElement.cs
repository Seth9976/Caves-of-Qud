using System;

namespace System.Net.Sockets
{
	/// <summary>Represents an element in a <see cref="T:System.Net.Sockets.SendPacketsElement" /> array.</summary>
	// Token: 0x020007B3 RID: 1971
	public class SendPacketsElement
	{
		// Token: 0x06003EC2 RID: 16066 RVA: 0x0000219B File Offset: 0x0000039B
		private SendPacketsElement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.SendPacketsElement" /> class using the specified file.</summary>
		/// <param name="filepath">The filename of the file to be transmitted using the <see cref="M:System.Net.Sockets.Socket.SendPacketsAsync(System.Net.Sockets.SocketAsyncEventArgs)" /> method.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="filepath" /> parameter cannot be null</exception>
		// Token: 0x06003EC3 RID: 16067 RVA: 0x000D6984 File Offset: 0x000D4B84
		public SendPacketsElement(string filepath)
			: this(filepath, 0, 0, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.SendPacketsElement" /> class using the specified filename path, offset, and count.</summary>
		/// <param name="filepath">The filename of the file to be transmitted using the <see cref="M:System.Net.Sockets.Socket.SendPacketsAsync(System.Net.Sockets.SocketAsyncEventArgs)" /> method.</param>
		/// <param name="offset">The offset, in bytes, from the beginning of the file to the location in the file to start sending the file specified in the <paramref name="filepath" /> parameter.</param>
		/// <param name="count">The number of bytes to send starting from the <paramref name="offset" /> parameter. If <paramref name="count" /> is zero, the entire file is sent. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="filepath" /> parameter cannot be null</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> and <paramref name="count" /> parameters must be greater than or equal to zero. The <paramref name="offset" /> and <paramref name="count" /> must be less than the size of the file indicated by the <paramref name="filepath" /> parameter.</exception>
		// Token: 0x06003EC4 RID: 16068 RVA: 0x000D6990 File Offset: 0x000D4B90
		public SendPacketsElement(string filepath, int offset, int count)
			: this(filepath, offset, count, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.SendPacketsElement" /> class using the specified filename path, buffer offset, and count with an option to combine this element with the next element in a single send request from the sockets layer to the transport. </summary>
		/// <param name="filepath">The filename of the file to be transmitted using the <see cref="M:System.Net.Sockets.Socket.SendPacketsAsync(System.Net.Sockets.SocketAsyncEventArgs)" /> method.</param>
		/// <param name="offset">The offset, in bytes, from the beginning of the file to the location in the file to start sending the file specified in the <paramref name="filepath" /> parameter.</param>
		/// <param name="count">The number of bytes to send starting from the <paramref name="offset" /> parameter. If <paramref name="count" /> is zero, the entire file is sent.</param>
		/// <param name="endOfPacket">A Boolean value that specifies that this element should not be combined with the next element in a single send request from the sockets layer to the transport. This flag is used for granular control of the content of each message on a datagram or message-oriented socket.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="filepath" /> parameter cannot be null</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> and <paramref name="count" /> parameters must be greater than or equal to zero. The <paramref name="offset" /> and <paramref name="count" /> must be less than the size of the file indicated by the <paramref name="filepath" /> parameter.</exception>
		// Token: 0x06003EC5 RID: 16069 RVA: 0x000D699C File Offset: 0x000D4B9C
		public SendPacketsElement(string filepath, int offset, int count, bool endOfPacket)
		{
			if (filepath == null)
			{
				throw new ArgumentNullException("filepath");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			this.Initialize(filepath, null, offset, count, endOfPacket);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.SendPacketsElement" /> class using the specified buffer.</summary>
		/// <param name="buffer">A byte array of data to send using the <see cref="M:System.Net.Sockets.Socket.SendPacketsAsync(System.Net.Sockets.SocketAsyncEventArgs)" /> method.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> parameter cannot be null</exception>
		// Token: 0x06003EC6 RID: 16070 RVA: 0x000D69DC File Offset: 0x000D4BDC
		public SendPacketsElement(byte[] buffer)
			: this(buffer, 0, (buffer != null) ? buffer.Length : 0, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.SendPacketsElement" /> class using the specified buffer, buffer offset, and count.</summary>
		/// <param name="buffer">A byte array of data to send using the <see cref="M:System.Net.Sockets.Socket.SendPacketsAsync(System.Net.Sockets.SocketAsyncEventArgs)" /> method.</param>
		/// <param name="offset">The offset, in bytes, from the beginning of the <paramref name="buffer" /> to the location in the <paramref name="buffer" /> to start sending the data specified in the <paramref name="buffer" /> parameter.</param>
		/// <param name="count">The number of bytes to send starting from the <paramref name="offset" /> parameter. If <paramref name="count" /> is zero, no bytes are sent.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> parameter cannot be null</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> and <paramref name="count" /> parameters must be greater than or equal to zero. The <paramref name="offset" /> and <paramref name="count" /> must be less than the size of the buffer</exception>
		// Token: 0x06003EC7 RID: 16071 RVA: 0x000D69F0 File Offset: 0x000D4BF0
		public SendPacketsElement(byte[] buffer, int offset, int count)
			: this(buffer, offset, count, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.SendPacketsElement" /> class using the specified buffer, buffer offset, and count with an option to combine this element with the next element in a single send request from the sockets layer to the transport. </summary>
		/// <param name="buffer">A byte array of data to send using the <see cref="M:System.Net.Sockets.Socket.SendPacketsAsync(System.Net.Sockets.SocketAsyncEventArgs)" /> method.</param>
		/// <param name="offset">The offset, in bytes, from the beginning of the <paramref name="buffer" /> to the location in the <paramref name="buffer" /> to start sending the data specified in the <paramref name="buffer" /> parameter.</param>
		/// <param name="count">The number bytes to send starting from the <paramref name="offset" /> parameter. If <paramref name="count" /> is zero, no bytes are sent.</param>
		/// <param name="endOfPacket">A Boolean value that specifies that this element should not be combined with the next element in a single send request from the sockets layer to the transport. This flag is used for granular control of the content of each message on a datagram or message-oriented socket. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> parameter cannot be null</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> and <paramref name="count" /> parameters must be greater than or equal to zero. The <paramref name="offset" /> and <paramref name="count" /> must be less than the size of the buffer</exception>
		// Token: 0x06003EC8 RID: 16072 RVA: 0x000D69FC File Offset: 0x000D4BFC
		public SendPacketsElement(byte[] buffer, int offset, int count, bool endOfPacket)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || count > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			this.Initialize(null, buffer, offset, count, endOfPacket);
		}

		// Token: 0x06003EC9 RID: 16073 RVA: 0x000D6A55 File Offset: 0x000D4C55
		private void Initialize(string filePath, byte[] buffer, int offset, int count, bool endOfPacket)
		{
			this.m_FilePath = filePath;
			this.m_Buffer = buffer;
			this.m_Offset = offset;
			this.m_Count = count;
			this.m_endOfPacket = endOfPacket;
		}

		/// <summary>Gets the filename of the file to send if the <see cref="T:System.Net.Sockets.SendPacketsElement" /> class was initialized with a <paramref name="filepath" /> parameter.</summary>
		/// <returns>The filename of the file to send if the <see cref="T:System.Net.Sockets.SendPacketsElement" /> class was initialized with a <paramref name="filepath" /> parameter.</returns>
		// Token: 0x17000E38 RID: 3640
		// (get) Token: 0x06003ECA RID: 16074 RVA: 0x000D6A7C File Offset: 0x000D4C7C
		public string FilePath
		{
			get
			{
				return this.m_FilePath;
			}
		}

		/// <summary>Gets the buffer to be sent if the <see cref="T:System.Net.Sockets.SendPacketsElement" /> class was initialized with a <paramref name="buffer" /> parameter.</summary>
		/// <returns>The byte buffer to send if the <see cref="T:System.Net.Sockets.SendPacketsElement" /> class was initialized with a <paramref name="buffer" /> parameter.</returns>
		// Token: 0x17000E39 RID: 3641
		// (get) Token: 0x06003ECB RID: 16075 RVA: 0x000D6A84 File Offset: 0x000D4C84
		public byte[] Buffer
		{
			get
			{
				return this.m_Buffer;
			}
		}

		/// <summary>Gets the count of bytes to be sent. </summary>
		/// <returns>The count of bytes to send if the <see cref="T:System.Net.Sockets.SendPacketsElement" /> class was initialized with a <paramref name="count" /> parameter.</returns>
		// Token: 0x17000E3A RID: 3642
		// (get) Token: 0x06003ECC RID: 16076 RVA: 0x000D6A8C File Offset: 0x000D4C8C
		public int Count
		{
			get
			{
				return this.m_Count;
			}
		}

		/// <summary>Gets the offset, in bytes, from the beginning of the data buffer or file to the location in the buffer or file to start sending the data. </summary>
		/// <returns>The offset, in bytes, from the beginning of the data buffer or file to the location in the buffer or file to start sending the data.</returns>
		// Token: 0x17000E3B RID: 3643
		// (get) Token: 0x06003ECD RID: 16077 RVA: 0x000D6A94 File Offset: 0x000D4C94
		public int Offset
		{
			get
			{
				return this.m_Offset;
			}
		}

		/// <summary>Gets a Boolean value that indicates if this element should not be combined with the next element in a single send request from the sockets layer to the transport.</summary>
		/// <returns>A Boolean value that indicates if this element should not be combined with the next element in a single send request.</returns>
		// Token: 0x17000E3C RID: 3644
		// (get) Token: 0x06003ECE RID: 16078 RVA: 0x000D6A9C File Offset: 0x000D4C9C
		public bool EndOfPacket
		{
			get
			{
				return this.m_endOfPacket;
			}
		}

		// Token: 0x0400254D RID: 9549
		internal string m_FilePath;

		// Token: 0x0400254E RID: 9550
		internal byte[] m_Buffer;

		// Token: 0x0400254F RID: 9551
		internal int m_Offset;

		// Token: 0x04002550 RID: 9552
		internal int m_Count;

		// Token: 0x04002551 RID: 9553
		private bool m_endOfPacket;
	}
}
