using System;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x020000AA RID: 170
	internal sealed class UnknownResponseHeader : BaseResponseHeader
	{
		// Token: 0x17000378 RID: 888
		// (get) Token: 0x060008FD RID: 2301 RVA: 0x0001605B File Offset: 0x0001425B
		// (set) Token: 0x060008FE RID: 2302 RVA: 0x00016064 File Offset: 0x00014264
		public string Name
		{
			get
			{
				return this.headerName;
			}
			set
			{
				string text;
				string text2;
				global::System.Web.Util.HttpEncoder.Current.HeaderNameValueEncode(value, null, out text, out text2);
				this.headerName = text;
			}
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x00016088 File Offset: 0x00014288
		public UnknownResponseHeader(string name, string val)
			: base(val)
		{
			this.Name = name;
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x00016098 File Offset: 0x00014298
		internal override void SendContent(HttpWorkerRequest wr)
		{
			wr.SendUnknownResponseHeader(this.Name, base.Value);
		}

		// Token: 0x04000FEC RID: 4076
		private string headerName;
	}
}
