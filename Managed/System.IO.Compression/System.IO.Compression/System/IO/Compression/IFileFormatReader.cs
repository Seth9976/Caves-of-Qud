using System;

namespace System.IO.Compression
{
	// Token: 0x02000021 RID: 33
	internal interface IFileFormatReader
	{
		// Token: 0x060000CA RID: 202
		bool ReadHeader(InputBuffer input);

		// Token: 0x060000CB RID: 203
		bool ReadFooter(InputBuffer input);

		// Token: 0x060000CC RID: 204
		void UpdateWithBytesRead(byte[] buffer, int offset, int bytesToCopy);

		// Token: 0x060000CD RID: 205
		void Validate();
	}
}
