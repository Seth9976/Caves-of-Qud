using System;
using System.Collections;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000355 RID: 853
	internal class ContentBuilderInternal : TemplateBuilder
	{
		// Token: 0x06001F97 RID: 8087 RVA: 0x0004DAC8 File Offset: 0x0004BCC8
		public override void Init(TemplateParser parser, ControlBuilder parentBuilder, Type type, string tagName, string ID, IDictionary attribs)
		{
			base.Init(parser, parentBuilder, type, tagName, ID, attribs);
			this.placeHolderID = attribs["ContentPlaceHolderID"] as string;
			if (string.IsNullOrEmpty(this.placeHolderID))
			{
				throw new HttpException("Missing required 'ContentPlaceHolderID' attribute");
			}
		}

		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x06001F98 RID: 8088 RVA: 0x0004DB08 File Offset: 0x0004BD08
		public string ContentPlaceHolderID
		{
			get
			{
				return this.placeHolderID;
			}
		}

		// Token: 0x04001883 RID: 6275
		private string placeHolderID;
	}
}
