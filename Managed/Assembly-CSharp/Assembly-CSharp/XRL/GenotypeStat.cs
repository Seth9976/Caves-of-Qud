using System;

namespace XRL
{
	// Token: 0x02000502 RID: 1282
	[Serializable]
	public class GenotypeStat
	{
		// Token: 0x06003729 RID: 14121 RVA: 0x0010B0C0 File Offset: 0x001092C0
		public void MergeWith(GenotypeStat newStat)
		{
			if (newStat.Minimum != -999)
			{
				this.Minimum = newStat.Minimum;
			}
			if (newStat.Maximum != -999)
			{
				this.Maximum = newStat.Maximum;
			}
			if (newStat.Bonus != -999)
			{
				this.Bonus = newStat.Bonus;
			}
		}

		// Token: 0x04001B74 RID: 7028
		public string Name = "";

		// Token: 0x04001B75 RID: 7029
		public string ChargenDescription = "";

		// Token: 0x04001B76 RID: 7030
		public int Minimum;

		// Token: 0x04001B77 RID: 7031
		public int Maximum;

		// Token: 0x04001B78 RID: 7032
		public int Bonus;
	}
}
