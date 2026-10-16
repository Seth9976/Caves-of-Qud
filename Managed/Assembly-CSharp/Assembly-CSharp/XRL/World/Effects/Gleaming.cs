using System;

namespace XRL.World.Effects
{
	// Token: 0x02000DB5 RID: 3509
	[Serializable]
	public class Gleaming : Effect
	{
		// Token: 0x06008C15 RID: 35861 RVA: 0x0031B6BF File Offset: 0x003198BF
		public Gleaming()
		{
			this.DisplayName = "{{K|gleaming}}";
			this.Duration = 1;
		}

		// Token: 0x06008C16 RID: 35862 RVA: 0x0031B6D9 File Offset: 0x003198D9
		public override int GetEffectType()
		{
			return 16777280;
		}

		// Token: 0x06008C17 RID: 35863 RVA: 0x0031B6E0 File Offset: 0x003198E0
		public override string GetDetails()
		{
			return "Coruscating with the dim light of a trillion shattered suns.";
		}
	}
}
