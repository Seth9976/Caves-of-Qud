using System;

namespace XRL
{
	// Token: 0x02000508 RID: 1288
	[Serializable]
	public class SubtypeStat
	{
		// Token: 0x0600374E RID: 14158 RVA: 0x0010C458 File Offset: 0x0010A658
		public void MergeWith(SubtypeStat newStat)
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

		// Token: 0x04001BA2 RID: 7074
		public string Name = "";

		// Token: 0x04001BA3 RID: 7075
		public int Minimum;

		// Token: 0x04001BA4 RID: 7076
		public int Maximum;

		// Token: 0x04001BA5 RID: 7077
		public int Bonus;
	}
}
