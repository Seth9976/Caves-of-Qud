using System;

namespace Mono.Btls
{
	// Token: 0x020000D3 RID: 211
	internal interface IMonoBtlsBioMono
	{
		// Token: 0x06000424 RID: 1060
		int Read(byte[] buffer, int offset, int size, out bool wantMore);

		// Token: 0x06000425 RID: 1061
		bool Write(byte[] buffer, int offset, int size);

		// Token: 0x06000426 RID: 1062
		void Flush();

		// Token: 0x06000427 RID: 1063
		void Close();
	}
}
