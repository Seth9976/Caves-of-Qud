using System;

namespace XRL.World.Effects
{
	// Token: 0x02000D2B RID: 3371
	[Serializable]
	public class DistractionImmunity : Effect
	{
		// Token: 0x060084E4 RID: 34020 RVA: 0x003001B6 File Offset: 0x002FE3B6
		public override string GetDescription()
		{
			return null;
		}

		// Token: 0x060084E5 RID: 34021 RVA: 0x003001B9 File Offset: 0x002FE3B9
		public override string GetDetails()
		{
			return null;
		}

		// Token: 0x060084E6 RID: 34022 RVA: 0x003001BC File Offset: 0x002FE3BC
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x040039A4 RID: 14756
		public int SourceID;

		// Token: 0x040039A5 RID: 14757
		public int DistractionID;
	}
}
