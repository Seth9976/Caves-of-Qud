using System;

namespace XRL.World.Parts
{
	// Token: 0x020011F8 RID: 4600
	[Serializable]
	public class NoEffects : IPart
	{
		// Token: 0x0600B717 RID: 46871 RVA: 0x0042B329 File Offset: 0x00429529
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ApplyEffectEvent.ID;
		}

		// Token: 0x0600B718 RID: 46872 RVA: 0x0042B33F File Offset: 0x0042953F
		public override bool HandleEvent(ApplyEffectEvent E)
		{
			return false;
		}
	}
}
