using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Net
{
	/// <summary>The exception that is thrown when an error occurs processing an HTTP request.</summary>
	// Token: 0x020005D1 RID: 1489
	[Serializable]
	public class HttpListenerException : Win32Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.HttpListenerException" /> class. </summary>
		// Token: 0x06003016 RID: 12310 RVA: 0x0007B6E4 File Offset: 0x000798E4
		public HttpListenerException()
			: base(Marshal.GetLastWin32Error())
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.HttpListenerException" /> class using the specified error code.</summary>
		/// <param name="errorCode">A <see cref="T:System.Int32" /> value that identifies the error that occurred.</param>
		// Token: 0x06003017 RID: 12311 RVA: 0x000A5C75 File Offset: 0x000A3E75
		public HttpListenerException(int errorCode)
			: base(errorCode)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.HttpListenerException" /> class using the specified error code and message.</summary>
		/// <param name="errorCode">A <see cref="T:System.Int32" /> value that identifies the error that occurred.</param>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error that occurred.</param>
		// Token: 0x06003018 RID: 12312 RVA: 0x000A5C7E File Offset: 0x000A3E7E
		public HttpListenerException(int errorCode, string message)
			: base(errorCode, message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.HttpListenerException" /> class from the specified instances of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> classes.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the information required to deserialize the new <see cref="T:System.Net.HttpListenerException" /> object. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object. </param>
		// Token: 0x06003019 RID: 12313 RVA: 0x000A5C88 File Offset: 0x000A3E88
		protected HttpListenerException(SerializationInfo serializationInfo, StreamingContext streamingContext)
			: base(serializationInfo, streamingContext)
		{
		}

		/// <summary>Gets a value that identifies the error that occurred.</summary>
		/// <returns>A <see cref="T:System.Int32" /> value.</returns>
		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x0600301A RID: 12314 RVA: 0x000A5C92 File Offset: 0x000A3E92
		public override int ErrorCode
		{
			get
			{
				return base.NativeErrorCode;
			}
		}
	}
}
