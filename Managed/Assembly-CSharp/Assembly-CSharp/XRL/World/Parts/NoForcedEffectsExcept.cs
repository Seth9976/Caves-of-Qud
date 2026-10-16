using System;

namespace XRL.World.Parts
{
	// Token: 0x020011FB RID: 4603
	[Serializable]
	public class NoForcedEffectsExcept : IPart
	{
		// Token: 0x0600B721 RID: 46881 RVA: 0x0042B3AF File Offset: 0x004295AF
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ForceApplyEffectEvent.ID;
		}

		// Token: 0x0600B722 RID: 46882 RVA: 0x0042B3C5 File Offset: 0x004295C5
		public override bool HandleEvent(ForceApplyEffectEvent E)
		{
			return this.Exception.CachedCommaExpansion().Contains(E.Name) && base.HandleEvent(E);
		}

		// Token: 0x0600B723 RID: 46883 RVA: 0x0042B3E8 File Offset: 0x004295E8
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x040049C1 RID: 18881
		public string Exception;
	}
}
