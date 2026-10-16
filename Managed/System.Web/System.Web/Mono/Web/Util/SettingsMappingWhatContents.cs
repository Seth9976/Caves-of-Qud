using System;
using System.Collections.Generic;
using System.Xml.XPath;

namespace Mono.Web.Util
{
	// Token: 0x0200000D RID: 13
	public class SettingsMappingWhatContents
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002AB9 File Offset: 0x00000CB9
		public SettingsMappingWhatOperation Operation
		{
			get
			{
				return this._operation;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002AC1 File Offset: 0x00000CC1
		public Dictionary<string, string> Attributes
		{
			get
			{
				return this._attributes;
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002ACC File Offset: 0x00000CCC
		public SettingsMappingWhatContents(XPathNavigator nav, SettingsMappingWhatOperation operation)
		{
			this._operation = operation;
			if (nav.HasAttributes)
			{
				nav.MoveToFirstAttribute();
				this._attributes.Add(nav.Name, nav.Value);
				while (nav.MoveToNextAttribute())
				{
					this._attributes.Add(nav.Name, nav.Value);
				}
			}
		}

		// Token: 0x04000D3F RID: 3391
		private SettingsMappingWhatOperation _operation;

		// Token: 0x04000D40 RID: 3392
		private Dictionary<string, string> _attributes = new Dictionary<string, string>();
	}
}
