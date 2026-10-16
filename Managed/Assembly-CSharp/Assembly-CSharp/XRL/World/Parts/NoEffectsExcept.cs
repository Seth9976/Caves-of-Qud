using System;

namespace XRL.World.Parts
{
	// Token: 0x020011FA RID: 4602
	[Serializable]
	public class NoEffectsExcept : IPart
	{
		// Token: 0x0600B71E RID: 46878 RVA: 0x0042B36E File Offset: 0x0042956E
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ApplyEffectEvent.ID;
		}

		// Token: 0x0600B71F RID: 46879 RVA: 0x0042B384 File Offset: 0x00429584
		public override bool HandleEvent(ApplyEffectEvent E)
		{
			return this.Exception.CachedCommaExpansion().Contains(E.Name) && base.HandleEvent(E);
		}

		// Token: 0x040049C0 RID: 18880
		public string Exception;
	}
}
