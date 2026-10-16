using System;

namespace System.IO.Compression
{
	// Token: 0x02000020 RID: 32
	internal interface IFileFormatWriter
	{
		// Token: 0x060000C7 RID: 199
		byte[] GetHeader();

		// Token: 0x060000C8 RID: 200
		void UpdateWithBytesRead(byte[] buffer, int offset, int bytesToCopy);

		// Token: 0x060000C9 RID: 201
		byte[] GetFooter();
	}
}
