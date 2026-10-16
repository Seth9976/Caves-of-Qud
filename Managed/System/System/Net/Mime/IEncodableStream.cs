using System;
using System.IO;

namespace System.Net.Mime
{
	// Token: 0x020007D7 RID: 2007
	internal interface IEncodableStream
	{
		// Token: 0x06004053 RID: 16467
		int DecodeBytes(byte[] buffer, int offset, int count);

		// Token: 0x06004054 RID: 16468
		int EncodeBytes(byte[] buffer, int offset, int count);

		// Token: 0x06004055 RID: 16469
		string GetEncodedString();

		// Token: 0x06004056 RID: 16470
		Stream GetStream();
	}
}
