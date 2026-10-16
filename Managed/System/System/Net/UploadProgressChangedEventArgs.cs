using System;
using System.ComponentModel;
using Unity;

namespace System.Net
{
	/// <summary>Provides data for the <see cref="E:System.Net.WebClient.UploadProgressChanged" /> event of a <see cref="T:System.Net.WebClient" />.</summary>
	// Token: 0x020005C3 RID: 1475
	public class UploadProgressChangedEventArgs : ProgressChangedEventArgs
	{
		// Token: 0x06002FD2 RID: 12242 RVA: 0x000A5194 File Offset: 0x000A3394
		internal UploadProgressChangedEventArgs(int progressPercentage, object userToken, long bytesSent, long totalBytesToSend, long bytesReceived, long totalBytesToReceive)
			: base(progressPercentage, userToken)
		{
			this.BytesReceived = bytesReceived;
			this.TotalBytesToReceive = totalBytesToReceive;
			this.BytesSent = bytesSent;
			this.TotalBytesToSend = totalBytesToSend;
		}

		/// <summary>Gets the number of bytes received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that indicates the number of bytes received.</returns>
		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x06002FD3 RID: 12243 RVA: 0x000A51BD File Offset: 0x000A33BD
		public long BytesReceived { get; }

		/// <summary>Gets the total number of bytes in a <see cref="T:System.Net.WebClient" /> data upload operation.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that indicates the number of bytes that will be received.</returns>
		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x06002FD4 RID: 12244 RVA: 0x000A51C5 File Offset: 0x000A33C5
		public long TotalBytesToReceive { get; }

		/// <summary>Gets the number of bytes sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that indicates the number of bytes sent.</returns>
		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x06002FD5 RID: 12245 RVA: 0x000A51CD File Offset: 0x000A33CD
		public long BytesSent { get; }

		/// <summary>Gets the total number of bytes to send.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that indicates the number of bytes that will be sent.</returns>
		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x06002FD6 RID: 12246 RVA: 0x000A51D5 File Offset: 0x000A33D5
		public long TotalBytesToSend { get; }

		// Token: 0x06002FD7 RID: 12247 RVA: 0x00013BAE File Offset: 0x00011DAE
		internal UploadProgressChangedEventArgs()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
