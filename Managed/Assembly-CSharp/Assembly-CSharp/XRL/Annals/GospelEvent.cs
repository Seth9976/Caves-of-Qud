using System;
using HistoryKit;

namespace XRL.Annals
{
	// Token: 0x02000625 RID: 1573
	[Serializable]
	public class GospelEvent : HistoricEvent
	{
		// Token: 0x060041E0 RID: 16864 RVA: 0x00152030 File Offset: 0x00150230
		public GospelEvent()
		{
		}

		// Token: 0x060041E1 RID: 16865 RVA: 0x00152038 File Offset: 0x00150238
		public GospelEvent(string Gospel)
		{
			this.Gospel = Gospel;
		}

		// Token: 0x060041E2 RID: 16866 RVA: 0x00152048 File Offset: 0x00150248
		public override void Generate()
		{
			base.SetEventProperty("gospel", this.Gospel ?? "[NO GOSPEL]");
			base.AddEntityListItem("Gospels", string.Format("{0}|{1}", this.Gospel ?? "[NO GOSPEL]", this.id), false);
		}

		// Token: 0x04002051 RID: 8273
		[NonSerialized]
		private string Gospel;
	}
}
