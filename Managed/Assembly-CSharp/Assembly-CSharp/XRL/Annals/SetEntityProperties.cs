using System;
using System.Collections.Generic;
using HistoryKit;

namespace XRL.Annals
{
	// Token: 0x020005F2 RID: 1522
	[Serializable]
	public class SetEntityProperties : HistoricEvent
	{
		// Token: 0x0600415D RID: 16733 RVA: 0x00149885 File Offset: 0x00147A85
		public SetEntityProperties(Dictionary<string, string> _stringProperties, Dictionary<string, string> _listProperties)
		{
			this.stringProperties = _stringProperties;
			this.listProperties = _listProperties;
		}

		// Token: 0x0600415E RID: 16734 RVA: 0x0014989C File Offset: 0x00147A9C
		public override void Generate()
		{
			if (this.stringProperties != null)
			{
				foreach (string text in this.stringProperties.Keys)
				{
					base.SetEntityProperty(text, this.stringProperties[text]);
				}
			}
			if (this.listProperties != null)
			{
				foreach (string text2 in this.listProperties.Keys)
				{
					base.AddEntityListItem(text2, this.listProperties[text2], false);
				}
			}
			this.duration = 0L;
		}

		// Token: 0x04002013 RID: 8211
		private Dictionary<string, string> stringProperties;

		// Token: 0x04002014 RID: 8212
		private Dictionary<string, string> listProperties;
	}
}
