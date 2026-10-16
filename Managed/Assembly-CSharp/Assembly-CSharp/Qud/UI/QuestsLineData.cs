using System;
using System.Collections.Generic;
using System.Text;
using XRL.UI.Framework;
using XRL.World;

namespace Qud.UI
{
	// Token: 0x02000473 RID: 1139
	public class QuestsLineData : PooledFrameworkDataElement<QuestsLineData>
	{
		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06002B63 RID: 11107 RVA: 0x000E7228 File Offset: 0x000E5428
		// (set) Token: 0x06002B64 RID: 11108 RVA: 0x000E7358 File Offset: 0x000E5558
		public string searchText
		{
			get
			{
				if (this._searchText == null)
				{
					StringBuilder stringBuilder = new StringBuilder();
					StringBuilder stringBuilder2 = stringBuilder;
					Quest quest = this.quest;
					stringBuilder2.Append((quest != null) ? quest.DisplayName : null);
					stringBuilder.Append(" ");
					StringBuilder stringBuilder3 = stringBuilder;
					Quest quest2 = this.quest;
					stringBuilder3.Append((quest2 != null) ? quest2.QuestGiverLocationName : null);
					stringBuilder.Append(" ");
					StringBuilder stringBuilder4 = stringBuilder;
					Quest quest3 = this.quest;
					stringBuilder4.Append((quest3 != null) ? quest3.QuestGiverName : null);
					stringBuilder.Append(" ");
					if (this.quest != null)
					{
						foreach (KeyValuePair<string, QuestStep> keyValuePair in this.quest.StepsByID)
						{
							stringBuilder.Append(" ");
							stringBuilder.Append(keyValuePair.Value.Name);
							stringBuilder.Append(" ");
							stringBuilder.Append(keyValuePair.Value.Text);
						}
					}
					this._searchText = stringBuilder.ToString().ToLower();
				}
				return this._searchText;
			}
			set
			{
				this._searchText = value;
			}
		}

		// Token: 0x06002B65 RID: 11109 RVA: 0x000E7361 File Offset: 0x000E5561
		public QuestsLineData set(Quest quest, bool expanded)
		{
			this.quest = quest;
			this.expanded = expanded;
			return this;
		}

		// Token: 0x06002B66 RID: 11110 RVA: 0x000E7372 File Offset: 0x000E5572
		public override void free()
		{
			this._searchText = null;
			this.expanded = true;
			this.quest = null;
			base.free();
		}

		// Token: 0x0400178B RID: 6027
		public bool expanded = true;

		// Token: 0x0400178C RID: 6028
		public Quest quest;

		// Token: 0x0400178D RID: 6029
		public QuestsLine line;

		// Token: 0x0400178E RID: 6030
		public string _searchText;
	}
}
