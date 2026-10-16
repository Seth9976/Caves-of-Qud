using System;
using HistoryKit;

namespace XRL.Annals
{
	// Token: 0x020005F1 RID: 1521
	[Serializable]
	public class RemoveEntityListItem : HistoricEvent
	{
		// Token: 0x0600415B RID: 16731 RVA: 0x00149853 File Offset: 0x00147A53
		public RemoveEntityListItem(string list, string value)
		{
			this._list = list;
			this._value = value;
		}

		// Token: 0x0600415C RID: 16732 RVA: 0x00149869 File Offset: 0x00147A69
		public override void Generate()
		{
			base.RemoveEntityListItem(this._list, this._value);
			this.duration = 0L;
		}

		// Token: 0x04002011 RID: 8209
		private string _list;

		// Token: 0x04002012 RID: 8210
		private string _value;
	}
}
