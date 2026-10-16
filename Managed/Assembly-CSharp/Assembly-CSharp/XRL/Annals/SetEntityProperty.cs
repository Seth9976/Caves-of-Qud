using System;
using HistoryKit;

namespace XRL.Annals
{
	// Token: 0x020005F3 RID: 1523
	[Serializable]
	public class SetEntityProperty : HistoricEvent
	{
		// Token: 0x0600415F RID: 16735 RVA: 0x0014996C File Offset: 0x00147B6C
		public SetEntityProperty(string name, string value)
		{
			this._name = name;
			this._value = value;
		}

		// Token: 0x06004160 RID: 16736 RVA: 0x00149982 File Offset: 0x00147B82
		public override void Generate()
		{
			base.SetEntityProperty(this._name, this._value);
			this.duration = 0L;
		}

		// Token: 0x04002015 RID: 8213
		public string _name;

		// Token: 0x04002016 RID: 8214
		public string _value;
	}
}
