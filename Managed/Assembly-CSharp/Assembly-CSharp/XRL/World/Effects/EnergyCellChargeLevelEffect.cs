using System;

namespace XRL.World.Effects
{
	// Token: 0x02000DB7 RID: 3511
	[Serializable]
	public class EnergyCellChargeLevelEffect : Effect
	{
		// Token: 0x06008C1D RID: 35869 RVA: 0x0031B7DC File Offset: 0x003199DC
		public EnergyCellChargeLevelEffect()
		{
			this.Duration = 1;
		}

		// Token: 0x06008C1E RID: 35870 RVA: 0x0031B7FD File Offset: 0x003199FD
		public override string GetDetails()
		{
			return this.Details;
		}

		// Token: 0x04003AF3 RID: 15091
		public int Level = -1;

		// Token: 0x04003AF4 RID: 15092
		public string Details = "";
	}
}
