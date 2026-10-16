using System;

namespace System.Web
{
	// Token: 0x020000E0 RID: 224
	internal sealed class NameValueTraceData
	{
		// Token: 0x06000BF3 RID: 3059 RVA: 0x0001F7D3 File Offset: 0x0001D9D3
		public NameValueTraceData(string name, string value)
		{
			this.Name = name;
			this.Value = value;
		}

		// Token: 0x040010D8 RID: 4312
		public string Name;

		// Token: 0x040010D9 RID: 4313
		public string Value;
	}
}
