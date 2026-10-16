using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Unity;

namespace System.Net.Sockets
{
	/// <summary>Represents an asynchronous socket operation.</summary>
	// Token: 0x020007C4 RID: 1988
	public class SocketAsyncEventArgs : EventArgs, IDisposable
	{
		/// <summary>Gets the exception in the case of a connection failure when a <see cref="T:System.Net.DnsEndPoint" /> was used.</summary>
		/// <returns>An <see cref="T:System.Exception" /> that indicates the cause of the connection error when a <see cref="T:System.Net.DnsEndPoint" /> was specified for the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.RemoteEndPoint" /> property.</returns>
		// Token: 0x17000E5D RID: 3677
		// (get) Token: 0x06003F64 RID: 16228 RVA: 0x000D8A97 File Offset: 0x000D6C97
		// (set) Token: 0x06003F65 RID: 16229 RVA: 0x000D8A9F File Offset: 0x000D6C9F
		public Exception ConnectByNameError { get; private set; }

		/// <summary>Gets or sets the socket to use or the socket created for accepting a connection with an asynchronous socket method.</summary>
		/// <returns>The <see cref="T:System.Net.Sockets.Socket" /> to use or the socket created for accepting a connection with an asynchronous socket method.</returns>
		// Token: 0x17000E5E RID: 3678
		// (get) Token: 0x06003F66 RID: 16230 RVA: 0x000D8AA8 File Offset: 0x000D6CA8
		// (set) Token: 0x06003F67 RID: 16231 RVA: 0x000D8AB0 File Offset: 0x000D6CB0
		public Socket AcceptSocket { get; set; }

		/// <summary>Gets the number of bytes transferred in the socket operation.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that contains the number of bytes transferred in the socket operation.</returns>
		// Token: 0x17000E5F RID: 3679
		// (get) Token: 0x06003F68 RID: 16232 RVA: 0x000D8AB9 File Offset: 0x000D6CB9
		// (set) Token: 0x06003F69 RID: 16233 RVA: 0x000D8AC1 File Offset: 0x000D6CC1
		public int BytesTransferred { get; private set; }

		/// <summary>Gets or sets a value that specifies if socket can be reused after a disconnect operation.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> that specifies if socket can be reused after a disconnect operation.</returns>
		// Token: 0x17000E60 RID: 3680
		// (get) Token: 0x06003F6A RID: 16234 RVA: 0x000D8ACA File Offset: 0x000D6CCA
		// (set) Token: 0x06003F6B RID: 16235 RVA: 0x000D8AD2 File Offset: 0x000D6CD2
		public bool DisconnectReuseSocket { get; set; }

		/// <summary>Gets the type of socket operation most recently performed with this context object.</summary>
		/// <returns>A <see cref="T:System.Net.Sockets.SocketAsyncOperation" /> instance that indicates the type of socket operation most recently performed with this context object.</returns>
		// Token: 0x17000E61 RID: 3681
		// (get) Token: 0x06003F6C RID: 16236 RVA: 0x000D8ADB File Offset: 0x000D6CDB
		// (set) Token: 0x06003F6D RID: 16237 RVA: 0x000D8AE3 File Offset: 0x000D6CE3
		public SocketAsyncOperation LastOperation { get; private set; }

		/// <summary>Gets or sets the remote IP endpoint for an asynchronous operation.</summary>
		/// <returns>An <see cref="T:System.Net.EndPoint" /> that represents the remote IP endpoint for an asynchronous operation.</returns>
		// Token: 0x17000E62 RID: 3682
		// (get) Token: 0x06003F6E RID: 16238 RVA: 0x000D8AEC File Offset: 0x000D6CEC
		// (set) Token: 0x06003F6F RID: 16239 RVA: 0x000D8AF4 File Offset: 0x000D6CF4
		public EndPoint RemoteEndPoint
		{
			get
			{
				return this.remote_ep;
			}
			set
			{
				this.remote_ep = value;
			}
		}

		/// <summary>Gets the IP address and interface of a received packet.</summary>
		/// <returns>An <see cref="T:System.Net.Sockets.IPPacketInformation" /> instance that contains the destination IP address and interface of a received packet.</returns>
		// Token: 0x17000E63 RID: 3683
		// (get) Token: 0x06003F70 RID: 16240 RVA: 0x000D8AFD File Offset: 0x000D6CFD
		// (set) Token: 0x06003F71 RID: 16241 RVA: 0x000D8B05 File Offset: 0x000D6D05
		public IPPacketInformation ReceiveMessageFromPacketInfo { get; private set; }

		/// <summary>Gets or sets an array of buffers to be sent for an asynchronous operation used by the <see cref="M:System.Net.Sockets.Socket.SendPacketsAsync(System.Net.Sockets.SocketAsyncEventArgs)" /> method.</summary>
		/// <returns>An array of <see cref="T:System.Net.Sockets.SendPacketsElement" /> objects that represent an array of buffers to be sent.</returns>
		// Token: 0x17000E64 RID: 3684
		// (get) Token: 0x06003F72 RID: 16242 RVA: 0x000D8B0E File Offset: 0x000D6D0E
		// (set) Token: 0x06003F73 RID: 16243 RVA: 0x000D8B16 File Offset: 0x000D6D16
		public SendPacketsElement[] SendPacketsElements { get; set; }

		/// <summary>Gets or sets a bitwise combination of <see cref="T:System.Net.Sockets.TransmitFileOptions" /> values for an asynchronous operation used by the <see cref="M:System.Net.Sockets.Socket.SendPacketsAsync(System.Net.Sockets.SocketAsyncEventArgs)" /> method.</summary>
		/// <returns>A <see cref="T:System.Net.Sockets.TransmitFileOptions" /> that contains a bitwise combination of values that are used with an asynchronous operation.</returns>
		// Token: 0x17000E65 RID: 3685
		// (get) Token: 0x06003F74 RID: 16244 RVA: 0x000D8B1F File Offset: 0x000D6D1F
		// (set) Token: 0x06003F75 RID: 16245 RVA: 0x000D8B27 File Offset: 0x000D6D27
		public TransmitFileOptions SendPacketsFlags { get; set; }

		/// <summary>Gets or sets the size, in bytes, of the data block used in the send operation.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that contains the size, in bytes, of the data block used in the send operation.</returns>
		// Token: 0x17000E66 RID: 3686
		// (get) Token: 0x06003F76 RID: 16246 RVA: 0x000D8B30 File Offset: 0x000D6D30
		// (set) Token: 0x06003F77 RID: 16247 RVA: 0x000D8B38 File Offset: 0x000D6D38
		[MonoTODO("unused property")]
		public int SendPacketsSendSize { get; set; }

		/// <summary>Gets or sets the result of the asynchronous socket operation.</summary>
		/// <returns>A <see cref="T:System.Net.Sockets.SocketError" /> that represents the result of the asynchronous socket operation.</returns>
		// Token: 0x17000E67 RID: 3687
		// (get) Token: 0x06003F78 RID: 16248 RVA: 0x000D8B41 File Offset: 0x000D6D41
		// (set) Token: 0x06003F79 RID: 16249 RVA: 0x000D8B49 File Offset: 0x000D6D49
		public SocketError SocketError { get; set; }

		/// <summary>Gets the results of an asynchronous socket operation or sets the behavior of an asynchronous operation.</summary>
		/// <returns>A <see cref="T:System.Net.Sockets.SocketFlags" /> that represents the results of an asynchronous socket operation.</returns>
		// Token: 0x17000E68 RID: 3688
		// (get) Token: 0x06003F7A RID: 16250 RVA: 0x000D8B52 File Offset: 0x000D6D52
		// (set) Token: 0x06003F7B RID: 16251 RVA: 0x000D8B5A File Offset: 0x000D6D5A
		public SocketFlags SocketFlags { get; set; }

		/// <summary>Gets or sets a user or application object associated with this asynchronous socket operation.</summary>
		/// <returns>An object that represents the user or application object associated with this asynchronous socket operation.</returns>
		// Token: 0x17000E69 RID: 3689
		// (get) Token: 0x06003F7C RID: 16252 RVA: 0x000D8B63 File Offset: 0x000D6D63
		// (set) Token: 0x06003F7D RID: 16253 RVA: 0x000D8B6B File Offset: 0x000D6D6B
		public object UserToken { get; set; }

		/// <summary>The created and connected <see cref="T:System.Net.Sockets.Socket" /> object after successful completion of the <see cref="Overload:System.Net.Sockets.Socket.ConnectAsync" /> method.</summary>
		/// <returns>The connected <see cref="T:System.Net.Sockets.Socket" /> object.</returns>
		// Token: 0x17000E6A RID: 3690
		// (get) Token: 0x06003F7E RID: 16254 RVA: 0x000D8B74 File Offset: 0x000D6D74
		public Socket ConnectSocket
		{
			get
			{
				if (this.SocketError == SocketError.AccessDenied)
				{
					return null;
				}
				return this.current_socket;
			}
		}

		/// <summary>The event used to complete an asynchronous operation.</summary>
		// Token: 0x14000076 RID: 118
		// (add) Token: 0x06003F7F RID: 16255 RVA: 0x000D8B8C File Offset: 0x000D6D8C
		// (remove) Token: 0x06003F80 RID: 16256 RVA: 0x000D8BC4 File Offset: 0x000D6DC4
		public event EventHandler<SocketAsyncEventArgs> Completed;

		/// <summary>Creates an empty <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> instance.</summary>
		/// <exception cref="T:System.NotSupportedException">The platform is not supported. </exception>
		// Token: 0x06003F81 RID: 16257 RVA: 0x000D8BF9 File Offset: 0x000D6DF9
		public SocketAsyncEventArgs()
		{
			this.SendPacketsSendSize = -1;
		}

		// Token: 0x06003F82 RID: 16258 RVA: 0x000D8C13 File Offset: 0x000D6E13
		internal SocketAsyncEventArgs(bool flowExecutionContext)
		{
		}

		// Token: 0x06003F83 RID: 16259 RVA: 0x000D8C28 File Offset: 0x000D6E28
		~SocketAsyncEventArgs()
		{
			this.Dispose(false);
		}

		// Token: 0x06003F84 RID: 16260 RVA: 0x000D8C58 File Offset: 0x000D6E58
		private void Dispose(bool disposing)
		{
			this.disposed = true;
			if (disposing)
			{
				int num = this.in_progress;
				return;
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> instance and optionally disposes of the managed resources.</summary>
		// Token: 0x06003F85 RID: 16261 RVA: 0x000D8C6E File Offset: 0x000D6E6E
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06003F86 RID: 16262 RVA: 0x000D8C7D File Offset: 0x000D6E7D
		internal void SetConnectByNameError(Exception error)
		{
			this.ConnectByNameError = error;
		}

		// Token: 0x06003F87 RID: 16263 RVA: 0x000D8C86 File Offset: 0x000D6E86
		internal void SetBytesTransferred(int value)
		{
			this.BytesTransferred = value;
		}

		// Token: 0x17000E6B RID: 3691
		// (get) Token: 0x06003F88 RID: 16264 RVA: 0x000D8C8F File Offset: 0x000D6E8F
		internal Socket CurrentSocket
		{
			get
			{
				return this.current_socket;
			}
		}

		// Token: 0x06003F89 RID: 16265 RVA: 0x000D8C97 File Offset: 0x000D6E97
		internal void SetCurrentSocket(Socket socket)
		{
			this.current_socket = socket;
		}

		// Token: 0x06003F8A RID: 16266 RVA: 0x000D8CA0 File Offset: 0x000D6EA0
		internal void SetLastOperation(SocketAsyncOperation op)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("System.Net.Sockets.SocketAsyncEventArgs");
			}
			if (Interlocked.Exchange(ref this.in_progress, 1) != 0)
			{
				throw new InvalidOperationException("Operation already in progress");
			}
			this.LastOperation = op;
		}

		// Token: 0x06003F8B RID: 16267 RVA: 0x000D8CD5 File Offset: 0x000D6ED5
		internal void Complete_internal()
		{
			this.in_progress = 0;
			this.OnCompleted(this);
		}

		/// <summary>Represents a method that is called when an asynchronous operation completes.</summary>
		/// <param name="e">The event that is signaled.</param>
		// Token: 0x06003F8C RID: 16268 RVA: 0x000D8CE8 File Offset: 0x000D6EE8
		protected virtual void OnCompleted(SocketAsyncEventArgs e)
		{
			if (e == null)
			{
				return;
			}
			EventHandler<SocketAsyncEventArgs> completed = e.Completed;
			if (completed != null)
			{
				completed(e.current_socket, e);
			}
		}

		// Token: 0x06003F8D RID: 16269 RVA: 0x000D8C97 File Offset: 0x000D6E97
		internal void StartOperationCommon(Socket socket)
		{
			this.current_socket = socket;
		}

		// Token: 0x06003F8E RID: 16270 RVA: 0x000D8D10 File Offset: 0x000D6F10
		internal void StartOperationWrapperConnect(MultipleConnectAsync args)
		{
			this.SetLastOperation(SocketAsyncOperation.Connect);
		}

		// Token: 0x06003F8F RID: 16271 RVA: 0x000D8D19 File Offset: 0x000D6F19
		internal void FinishConnectByNameSyncFailure(Exception exception, int bytesTransferred, SocketFlags flags)
		{
			this.SetResults(exception, bytesTransferred, flags);
			if (this.current_socket != null)
			{
				this.current_socket.is_connected = false;
			}
			this.Complete_internal();
		}

		// Token: 0x06003F90 RID: 16272 RVA: 0x000D8D19 File Offset: 0x000D6F19
		internal void FinishOperationAsyncFailure(Exception exception, int bytesTransferred, SocketFlags flags)
		{
			this.SetResults(exception, bytesTransferred, flags);
			if (this.current_socket != null)
			{
				this.current_socket.is_connected = false;
			}
			this.Complete_internal();
		}

		// Token: 0x06003F91 RID: 16273 RVA: 0x000D8D3E File Offset: 0x000D6F3E
		internal void FinishWrapperConnectSuccess(Socket connectSocket, int bytesTransferred, SocketFlags flags)
		{
			this.SetResults(SocketError.Success, bytesTransferred, flags);
			this.current_socket = connectSocket;
			this.Complete_internal();
		}

		// Token: 0x06003F92 RID: 16274 RVA: 0x000D8D56 File Offset: 0x000D6F56
		internal void SetResults(SocketError socketError, int bytesTransferred, SocketFlags flags)
		{
			this.SocketError = socketError;
			this.ConnectByNameError = null;
			this.BytesTransferred = bytesTransferred;
			this.SocketFlags = flags;
		}

		// Token: 0x06003F93 RID: 16275 RVA: 0x000D8D74 File Offset: 0x000D6F74
		internal void SetResults(Exception exception, int bytesTransferred, SocketFlags flags)
		{
			this.ConnectByNameError = exception;
			this.BytesTransferred = bytesTransferred;
			this.SocketFlags = flags;
			if (exception == null)
			{
				this.SocketError = SocketError.Success;
				return;
			}
			SocketException ex = exception as SocketException;
			if (ex != null)
			{
				this.SocketError = ex.SocketErrorCode;
				return;
			}
			this.SocketError = SocketError.SocketError;
		}

		/// <summary>Gets the data buffer to use with an asynchronous socket method.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array that represents the data buffer to use with an asynchronous socket method.</returns>
		// Token: 0x17000E6C RID: 3692
		// (get) Token: 0x06003F94 RID: 16276 RVA: 0x000D8DC0 File Offset: 0x000D6FC0
		public byte[] Buffer
		{
			get
			{
				if (this._bufferIsExplicitArray)
				{
					ArraySegment<byte> arraySegment;
					MemoryMarshal.TryGetArray<byte>(this._buffer, out arraySegment);
					return arraySegment.Array;
				}
				return null;
			}
		}

		// Token: 0x17000E6D RID: 3693
		// (get) Token: 0x06003F95 RID: 16277 RVA: 0x000D8DF1 File Offset: 0x000D6FF1
		public Memory<byte> MemoryBuffer
		{
			get
			{
				return this._buffer;
			}
		}

		/// <summary>Gets the offset, in bytes, into the data buffer referenced by the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.Buffer" /> property.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that contains the offset, in bytes, into the data buffer referenced by the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.Buffer" /> property.</returns>
		// Token: 0x17000E6E RID: 3694
		// (get) Token: 0x06003F96 RID: 16278 RVA: 0x000D8DF9 File Offset: 0x000D6FF9
		public int Offset
		{
			get
			{
				return this._offset;
			}
		}

		/// <summary>Gets the maximum amount of data, in bytes, to send or receive in an asynchronous operation.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that contains the maximum amount of data, in bytes, to send or receive.</returns>
		// Token: 0x17000E6F RID: 3695
		// (get) Token: 0x06003F97 RID: 16279 RVA: 0x000D8E01 File Offset: 0x000D7001
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		/// <summary>Gets or sets an array of data buffers to use with an asynchronous socket method.</summary>
		/// <returns>An <see cref="T:System.Collections.IList" /> that represents an array of data buffers to use with an asynchronous socket method.</returns>
		/// <exception cref="T:System.ArgumentException">There are ambiguous buffers specified on a set operation. This exception occurs if the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.Buffer" /> property has been set to a non-null value and an attempt was made to set the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.BufferList" /> property to a non-null value.</exception>
		// Token: 0x17000E70 RID: 3696
		// (get) Token: 0x06003F98 RID: 16280 RVA: 0x000D8E09 File Offset: 0x000D7009
		// (set) Token: 0x06003F99 RID: 16281 RVA: 0x000D8E14 File Offset: 0x000D7014
		public IList<ArraySegment<byte>> BufferList
		{
			get
			{
				return this._bufferList;
			}
			set
			{
				if (value != null)
				{
					if (!this._buffer.Equals(default(Memory<byte>)))
					{
						throw new ArgumentException(SR.Format("Buffer and BufferList properties cannot both be non-null.", "Buffer"));
					}
					int count = value.Count;
					if (this._bufferListInternal == null)
					{
						this._bufferListInternal = new List<ArraySegment<byte>>(count);
					}
					else
					{
						this._bufferListInternal.Clear();
					}
					for (int i = 0; i < count; i++)
					{
						ArraySegment<byte> arraySegment = value[i];
						RangeValidationHelpers.ValidateSegment(arraySegment);
						this._bufferListInternal.Add(arraySegment);
					}
				}
				else
				{
					List<ArraySegment<byte>> bufferListInternal = this._bufferListInternal;
					if (bufferListInternal != null)
					{
						bufferListInternal.Clear();
					}
				}
				this._bufferList = value;
			}
		}

		/// <summary>Sets the data buffer to use with an asynchronous socket method.</summary>
		/// <param name="offset">The offset, in bytes, in the data buffer where the operation starts.</param>
		/// <param name="count">The maximum amount of data, in bytes, to send or receive in the buffer.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">An argument was out of range. This exception occurs if the <paramref name="offset" /> parameter is less than zero or greater than the length of the array in the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.Buffer" /> property. This exception also occurs if the <paramref name="count" /> parameter is less than zero or greater than the length of the array in the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.Buffer" /> property minus the <paramref name="offset" /> parameter.</exception>
		// Token: 0x06003F9A RID: 16282 RVA: 0x000D8EB8 File Offset: 0x000D70B8
		public void SetBuffer(int offset, int count)
		{
			if (!this._buffer.Equals(default(Memory<byte>)))
			{
				if ((ulong)offset > (ulong)((long)this._buffer.Length))
				{
					throw new ArgumentOutOfRangeException("offset");
				}
				if ((ulong)count > (ulong)((long)(this._buffer.Length - offset)))
				{
					throw new ArgumentOutOfRangeException("count");
				}
				if (!this._bufferIsExplicitArray)
				{
					throw new InvalidOperationException("This operation may only be performed when the buffer was set using the SetBuffer overload that accepts an array.");
				}
				this._offset = offset;
				this._count = count;
			}
		}

		// Token: 0x06003F9B RID: 16283 RVA: 0x000D8F34 File Offset: 0x000D7134
		internal void CopyBufferFrom(SocketAsyncEventArgs source)
		{
			this._buffer = source._buffer;
			this._offset = source._offset;
			this._count = source._count;
			this._bufferIsExplicitArray = source._bufferIsExplicitArray;
		}

		/// <summary>Sets the data buffer to use with an asynchronous socket method.</summary>
		/// <param name="buffer">The data buffer to use with an asynchronous socket method.</param>
		/// <param name="offset">The offset, in bytes, in the data buffer where the operation starts.</param>
		/// <param name="count">The maximum amount of data, in bytes, to send or receive in the buffer.</param>
		/// <exception cref="T:System.ArgumentException">There are ambiguous buffers specified. This exception occurs if the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.Buffer" /> property is also not null and the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.BufferList" /> property is also not null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">An argument was out of range. This exception occurs if the <paramref name="offset" /> parameter is less than zero or greater than the length of the array in the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.Buffer" /> property. This exception also occurs if the <paramref name="count" /> parameter is less than zero or greater than the length of the array in the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.Buffer" /> property minus the <paramref name="offset" /> parameter.</exception>
		// Token: 0x06003F9C RID: 16284 RVA: 0x000D8F68 File Offset: 0x000D7168
		public void SetBuffer(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				this._buffer = default(Memory<byte>);
				this._offset = 0;
				this._count = 0;
				this._bufferIsExplicitArray = false;
				return;
			}
			if (this._bufferList != null)
			{
				throw new ArgumentException(SR.Format("Buffer and BufferList properties cannot both be non-null.", "BufferList"));
			}
			if ((ulong)offset > (ulong)((long)buffer.Length))
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if ((ulong)count > (ulong)((long)(buffer.Length - offset)))
			{
				throw new ArgumentOutOfRangeException("count");
			}
			this._buffer = buffer;
			this._offset = offset;
			this._count = count;
			this._bufferIsExplicitArray = true;
		}

		// Token: 0x06003F9D RID: 16285 RVA: 0x000D9000 File Offset: 0x000D7200
		public void SetBuffer(Memory<byte> buffer)
		{
			if (buffer.Length != 0 && this._bufferList != null)
			{
				throw new ArgumentException(SR.Format("Buffer and BufferList properties cannot both be non-null.", "BufferList"));
			}
			this._buffer = buffer;
			this._offset = 0;
			this._count = buffer.Length;
			this._bufferIsExplicitArray = false;
		}

		// Token: 0x17000E71 RID: 3697
		// (get) Token: 0x06003F9E RID: 16286 RVA: 0x000D9055 File Offset: 0x000D7255
		internal bool HasMultipleBuffers
		{
			get
			{
				return this._bufferList != null;
			}
		}

		/// <summary>Gets or sets the protocol to use to download the socket client access policy file. </summary>
		/// <returns>Returns <see cref="T:System.Net.Sockets.SocketClientAccessPolicyProtocol" />.The protocol to use to download the socket client access policy file.</returns>
		// Token: 0x17000E72 RID: 3698
		// (get) Token: 0x06003F9F RID: 16287 RVA: 0x000D9060 File Offset: 0x000D7260
		// (set) Token: 0x06003FA0 RID: 16288 RVA: 0x00013BAE File Offset: 0x00011DAE
		public SocketClientAccessPolicyProtocol SocketClientAccessPolicyProtocol
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return SocketClientAccessPolicyProtocol.Tcp;
			}
			[CompilerGenerated]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x040025FC RID: 9724
		private bool disposed;

		// Token: 0x040025FD RID: 9725
		internal volatile int in_progress;

		// Token: 0x040025FE RID: 9726
		private EndPoint remote_ep;

		// Token: 0x040025FF RID: 9727
		private Socket current_socket;

		// Token: 0x04002600 RID: 9728
		internal SocketAsyncResult socket_async_result = new SocketAsyncResult();

		// Token: 0x0400260E RID: 9742
		private Memory<byte> _buffer;

		// Token: 0x0400260F RID: 9743
		private int _offset;

		// Token: 0x04002610 RID: 9744
		private int _count;

		// Token: 0x04002611 RID: 9745
		private bool _bufferIsExplicitArray;

		// Token: 0x04002612 RID: 9746
		private IList<ArraySegment<byte>> _bufferList;

		// Token: 0x04002613 RID: 9747
		private List<ArraySegment<byte>> _bufferListInternal;
	}
}
