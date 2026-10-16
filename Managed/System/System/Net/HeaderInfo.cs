using System;

namespace System.Net
{
	// Token: 0x0200062B RID: 1579
	internal class HeaderInfo
	{
		// Token: 0x060031D9 RID: 12761 RVA: 0x000ACC01 File Offset: 0x000AAE01
		internal HeaderInfo(string name, bool requestRestricted, bool responseRestricted, bool multi, HeaderParser p)
		{
			this.HeaderName = name;
			this.IsRequestRestricted = requestRestricted;
			this.IsResponseRestricted = responseRestricted;
			this.Parser = p;
			this.AllowMultiValues = multi;
		}

		// Token: 0x04001D43 RID: 7491
		internal readonly bool IsRequestRestricted;

		// Token: 0x04001D44 RID: 7492
		internal readonly bool IsResponseRestricted;

		// Token: 0x04001D45 RID: 7493
		internal readonly HeaderParser Parser;

		// Token: 0x04001D46 RID: 7494
		internal readonly string HeaderName;

		// Token: 0x04001D47 RID: 7495
		internal readonly bool AllowMultiValues;
	}
}
