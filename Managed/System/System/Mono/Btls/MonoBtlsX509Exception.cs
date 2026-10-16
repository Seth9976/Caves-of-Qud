using System;

namespace Mono.Btls
{
	// Token: 0x020000FB RID: 251
	internal class MonoBtlsX509Exception : Exception
	{
		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060005BC RID: 1468 RVA: 0x0001060C File Offset: 0x0000E80C
		// (set) Token: 0x060005BD RID: 1469 RVA: 0x00010614 File Offset: 0x0000E814
		public MonoBtlsX509Error ErrorCode { get; private set; }

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060005BE RID: 1470 RVA: 0x0001061D File Offset: 0x0000E81D
		// (set) Token: 0x060005BF RID: 1471 RVA: 0x00010625 File Offset: 0x0000E825
		public string ErrorMessage { get; private set; }

		// Token: 0x060005C0 RID: 1472 RVA: 0x0001062E File Offset: 0x0000E82E
		public MonoBtlsX509Exception(MonoBtlsX509Error code, string message)
			: base(message)
		{
			this.ErrorCode = code;
			this.ErrorMessage = message;
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x00010645 File Offset: 0x0000E845
		public override string ToString()
		{
			return string.Format("[MonoBtlsX509Exception: ErrorCode={0}, ErrorMessage={1}]", this.ErrorCode, this.ErrorMessage);
		}
	}
}
