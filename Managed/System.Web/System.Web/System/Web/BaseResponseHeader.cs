using System;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x020000A8 RID: 168
	internal abstract class BaseResponseHeader
	{
		// Token: 0x17000377 RID: 887
		// (get) Token: 0x060008F7 RID: 2295 RVA: 0x00015FFB File Offset: 0x000141FB
		// (set) Token: 0x060008F8 RID: 2296 RVA: 0x00016004 File Offset: 0x00014204
		public string Value
		{
			get
			{
				return this.headerValue;
			}
			set
			{
				string text;
				string text2;
				global::System.Web.Util.HttpEncoder.Current.HeaderNameValueEncode(null, value, out text, out text2);
				this.headerValue = text2;
			}
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x00016028 File Offset: 0x00014228
		internal BaseResponseHeader(string val)
		{
			this.Value = val;
		}

		// Token: 0x060008FA RID: 2298
		internal abstract void SendContent(HttpWorkerRequest wr);

		// Token: 0x04000FEA RID: 4074
		private string headerValue;
	}
}
