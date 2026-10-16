using System;

namespace XRL.World.Parts
{
	// Token: 0x020011FE RID: 4606
	[Serializable]
	public class OozeProperties : IPart
	{
		// Token: 0x0600B732 RID: 46898 RVA: 0x0042B5EA File Offset: 0x004297EA
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == CanApplyEffectEvent.ID || ID == ApplyEffectEvent.ID;
		}

		// Token: 0x0600B733 RID: 46899 RVA: 0x0042B608 File Offset: 0x00429808
		public override bool HandleEvent(CanApplyEffectEvent E)
		{
			return !(E.Name == "CardiacArrest") && base.HandleEvent(E);
		}

		// Token: 0x0600B734 RID: 46900 RVA: 0x0042B625 File Offset: 0x00429825
		public override bool HandleEvent(ApplyEffectEvent E)
		{
			return !(E.Name == "CardiacArrest") && base.HandleEvent(E);
		}
	}
}
