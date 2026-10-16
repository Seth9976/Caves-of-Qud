using System;

namespace XRL.World.Parts
{
	// Token: 0x020011F9 RID: 4601
	[Serializable]
	public class NoForcedEffects : IPart
	{
		// Token: 0x0600B71A RID: 46874 RVA: 0x0042B34A File Offset: 0x0042954A
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ForceApplyEffectEvent.ID;
		}

		// Token: 0x0600B71B RID: 46875 RVA: 0x0042B360 File Offset: 0x00429560
		public override bool HandleEvent(ForceApplyEffectEvent E)
		{
			return false;
		}

		// Token: 0x0600B71C RID: 46876 RVA: 0x0042B363 File Offset: 0x00429563
		public override bool AllowStaticRegistration()
		{
			return true;
		}
	}
}
