using System;

namespace System.Text.RegularExpressions
{
	// Token: 0x0200020D RID: 525
	internal readonly struct RegexPrefix
	{
		// Token: 0x06000F0E RID: 3854 RVA: 0x00043318 File Offset: 0x00041518
		internal RegexPrefix(string prefix, bool ci)
		{
			this.Prefix = prefix;
			this.CaseInsensitive = ci;
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000F0F RID: 3855 RVA: 0x00043328 File Offset: 0x00041528
		internal bool CaseInsensitive { get; }

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000F10 RID: 3856 RVA: 0x00043330 File Offset: 0x00041530
		internal static RegexPrefix Empty { get; } = new RegexPrefix(string.Empty, false);

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000F11 RID: 3857 RVA: 0x00043337 File Offset: 0x00041537
		internal string Prefix { get; }
	}
}
