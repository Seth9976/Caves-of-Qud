using System;
using System.Web.Services.Protocols;

namespace System.Web.Services.Description
{
	// Token: 0x020000C5 RID: 197
	internal class HttpPostProtocolImporter : HttpProtocolImporter
	{
		// Token: 0x06000513 RID: 1299 RVA: 0x00017518 File Offset: 0x00015718
		public HttpPostProtocolImporter()
			: base(true)
		{
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000514 RID: 1300 RVA: 0x00017521 File Offset: 0x00015721
		public override string ProtocolName
		{
			get
			{
				return "HttpPost";
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x00017528 File Offset: 0x00015728
		internal override Type BaseClass
		{
			get
			{
				if (base.Style == ServiceDescriptionImportStyle.Client)
				{
					return typeof(HttpPostClientProtocol);
				}
				return typeof(WebService);
			}
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x00017548 File Offset: 0x00015748
		protected override bool IsBindingSupported()
		{
			HttpBinding httpBinding = (HttpBinding)base.Binding.Extensions.Find(typeof(HttpBinding));
			return httpBinding != null && !(httpBinding.Verb != "POST");
		}
	}
}
