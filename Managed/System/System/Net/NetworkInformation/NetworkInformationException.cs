using System;
using System.ComponentModel;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Net.NetworkInformation
{
	/// <summary>The exception that is thrown when an error occurs while retrieving network information.</summary>
	// Token: 0x020006F7 RID: 1783
	[Serializable]
	public class NetworkInformationException : Win32Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.NetworkInformationException" /> class.</summary>
		// Token: 0x0600394B RID: 14667 RVA: 0x0007B6E4 File Offset: 0x000798E4
		public NetworkInformationException()
			: base(Marshal.GetLastWin32Error())
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.NetworkInformationException" /> class with the specified error code.</summary>
		/// <param name="errorCode">A Win32 error code. </param>
		// Token: 0x0600394C RID: 14668 RVA: 0x000A5C75 File Offset: 0x000A3E75
		public NetworkInformationException(int errorCode)
			: base(errorCode)
		{
		}

		// Token: 0x0600394D RID: 14669 RVA: 0x000A5C75 File Offset: 0x000A3E75
		internal NetworkInformationException(SocketError socketError)
			: base((int)socketError)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.NetworkInformationException" /> class with serialized data.</summary>
		/// <param name="serializationInfo">A SerializationInfo object that contains the serialized exception data. </param>
		/// <param name="streamingContext">A StreamingContext that contains contextual information about the serialized exception. </param>
		// Token: 0x0600394E RID: 14670 RVA: 0x000A5C88 File Offset: 0x000A3E88
		protected NetworkInformationException(SerializationInfo serializationInfo, StreamingContext streamingContext)
			: base(serializationInfo, streamingContext)
		{
		}

		/// <summary>Gets the Win32 error code for this exception.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that contains the Win32 error code.</returns>
		// Token: 0x17000C67 RID: 3175
		// (get) Token: 0x0600394F RID: 14671 RVA: 0x000A5C92 File Offset: 0x000A3E92
		public override int ErrorCode
		{
			get
			{
				return base.NativeErrorCode;
			}
		}
	}
}
