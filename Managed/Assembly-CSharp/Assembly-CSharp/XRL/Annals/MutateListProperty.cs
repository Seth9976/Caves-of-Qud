using System;
using System.Collections.Generic;
using System.Linq;
using HistoryKit;

namespace XRL.Annals
{
	// Token: 0x020005F4 RID: 1524
	[Serializable]
	public class MutateListProperty : HistoricEvent
	{
		// Token: 0x06004161 RID: 16737 RVA: 0x0014999E File Offset: 0x00147B9E
		public MutateListProperty(string _name, Func<string, string> _mutation, HistoricEntitySnapshot _snapshot)
		{
			this.name = _name;
			this.mutation = _mutation;
			this.snapshot = _snapshot;
		}

		// Token: 0x06004162 RID: 16738 RVA: 0x001499BC File Offset: 0x00147BBC
		public override void Generate()
		{
			List<string> list = this.snapshot.GetList(this.name);
			list = list.Select<string, string>((string s) => this.mutation(s)).ToList<string>();
			base.ChangeListProperty(this.name, this.snapshot.GetList(this.name), list);
			this.duration = 0L;
		}

		// Token: 0x04002017 RID: 8215
		public string name;

		// Token: 0x04002018 RID: 8216
		[NonSerialized]
		public Func<string, string> mutation;

		// Token: 0x04002019 RID: 8217
		[NonSerialized]
		public HistoricEntitySnapshot snapshot;
	}
}
