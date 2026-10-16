using System;
using HistoryKit;

namespace XRL.Annals
{
	// Token: 0x020005F0 RID: 1520
	[Serializable]
	public class AddEntityListItem : HistoricEvent
	{
		// Token: 0x06004159 RID: 16729 RVA: 0x00149820 File Offset: 0x00147A20
		public AddEntityListItem(string list, string value)
		{
			this._list = list;
			this._value = value;
		}

		// Token: 0x0600415A RID: 16730 RVA: 0x00149836 File Offset: 0x00147A36
		public override void Generate()
		{
			base.AddEntityListItem(this._list, this._value, false);
			this.duration = 0L;
		}

		// Token: 0x0400200F RID: 8207
		private string _list;

		// Token: 0x04002010 RID: 8208
		private string _value;
	}
}
