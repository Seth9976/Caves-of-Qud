using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Net.Sockets
{
	/// <summary>The exception that is thrown when a socket error occurs.</summary>
	// Token: 0x020007A7 RID: 1959
	[Serializable]
	public class SocketException : Win32Exception
	{
		// Token: 0x06003E9C RID: 16028
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int WSAGetLastError_icall();

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.SocketException" /> class with the last operating system error code.</summary>
		// Token: 0x06003E9D RID: 16029 RVA: 0x000D66B1 File Offset: 0x000D48B1
		public SocketException()
			: base(SocketException.WSAGetLastError_icall())
		{
		}

		// Token: 0x06003E9E RID: 16030 RVA: 0x000A5C7E File Offset: 0x000A3E7E
		internal SocketException(int error, string message)
			: base(error, message)
		{
		}

		// Token: 0x06003E9F RID: 16031 RVA: 0x000D66BE File Offset: 0x000D48BE
		internal SocketException(EndPoint endPoint)
			: base(Marshal.GetLastWin32Error())
		{
			this.m_EndPoint = endPoint;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.SocketException" /> class with the specified error code.</summary>
		/// <param name="errorCode">The error code that indicates the error that occurred. </param>
		// Token: 0x06003EA0 RID: 16032 RVA: 0x000A5C75 File Offset: 0x000A3E75
		public SocketException(int errorCode)
			: base(errorCode)
		{
		}

		// Token: 0x06003EA1 RID: 16033 RVA: 0x000D66D2 File Offset: 0x000D48D2
		internal SocketException(int errorCode, EndPoint endPoint)
			: base(errorCode)
		{
			this.m_EndPoint = endPoint;
		}

		// Token: 0x06003EA2 RID: 16034 RVA: 0x000A5C75 File Offset: 0x000A3E75
		internal SocketException(SocketError socketError)
			: base((int)socketError)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.SocketException" /> class from the specified instances of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> classes.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance that contains the information that is required to serialize the new <see cref="T:System.Net.Sockets.SocketException" /> instance. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the source of the serialized stream that is associated with the new <see cref="T:System.Net.Sockets.SocketException" /> instance. </param>
		// Token: 0x06003EA3 RID: 16035 RVA: 0x000A5C88 File Offset: 0x000A3E88
		protected SocketException(SerializationInfo serializationInfo, StreamingContext streamingContext)
			: base(serializationInfo, streamingContext)
		{
		}

		/// <summary>Gets the error code that is associated with this exception.</summary>
		/// <returns>An integer error code that is associated with this exception.</returns>
		// Token: 0x17000E2C RID: 3628
		// (get) Token: 0x06003EA4 RID: 16036 RVA: 0x000A5C92 File Offset: 0x000A3E92
		public override int ErrorCode
		{
			get
			{
				return base.NativeErrorCode;
			}
		}

		/// <summary>Gets the error message that is associated with this exception.</summary>
		/// <returns>A string that contains the error message. </returns>
		// Token: 0x17000E2D RID: 3629
		// (get) Token: 0x06003EA5 RID: 16037 RVA: 0x000D66E2 File Offset: 0x000D48E2
		public override string Message
		{
			get
			{
				if (this.m_EndPoint == null)
				{
					return base.Message;
				}
				return base.Message + " " + this.m_EndPoint.ToString();
			}
		}

		/// <summary>Gets the error code that is associated with this exception.</summary>
		/// <returns>An integer error code that is associated with this exception.</returns>
		// Token: 0x17000E2E RID: 3630
		// (get) Token: 0x06003EA6 RID: 16038 RVA: 0x000A5C92 File Offset: 0x000A3E92
		public SocketError SocketErrorCode
		{
			get
			{
				return (SocketError)base.NativeErrorCode;
			}
		}

		// Token: 0x040024B2 RID: 9394
		[NonSerialized]
		private EndPoint m_EndPoint;
	}
}
