using System;
using System.Collections.Generic;
using HistoryKit;

namespace XRL.Annals
{
	// Token: 0x020005EF RID: 1519
	public class RemoveEntityListProperties : HistoricEvent
	{
		// Token: 0x06004157 RID: 16727 RVA: 0x0014979C File Offset: 0x0014799C
		public RemoveEntityListProperties(Dictionary<string, string> _listProperties)
		{
			this.listProperties = _listProperties;
		}

		// Token: 0x06004158 RID: 16728 RVA: 0x001497AC File Offset: 0x001479AC
		public override void Generate()
		{
			if (this.listProperties != null)
			{
				foreach (string text in this.listProperties.Keys)
				{
					base.RemoveEntityListItem(text, this.listProperties[text]);
				}
			}
			this.duration = 0L;
		}

		// Token: 0x0400200E RID: 8206
		private Dictionary<string, string> listProperties;
	}
}
