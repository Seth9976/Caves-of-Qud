using System;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02001073 RID: 4211
	[Serializable]
	public class NoBreak : IPart
	{
		// Token: 0x0600A7BB RID: 42939 RVA: 0x003D4D98 File Offset: 0x003D2F98
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == CanApplyEffectEvent.ID || ID == ApplyEffectEvent.ID;
		}

		// Token: 0x0600A7BC RID: 42940 RVA: 0x003D4DB6 File Offset: 0x003D2FB6
		public override bool HandleEvent(CanApplyEffectEvent E)
		{
			return !(E.Name == "Broken") && base.HandleEvent(E);
		}

		// Token: 0x0600A7BD RID: 42941 RVA: 0x003D4DD3 File Offset: 0x003D2FD3
		public override bool HandleEvent(ApplyEffectEvent E)
		{
			return !(E.Effect is Broken) && base.HandleEvent(E);
		}

		// Token: 0x0600A7BE RID: 42942 RVA: 0x003D4DEB File Offset: 0x003D2FEB
		public override bool AllowStaticRegistration()
		{
			return true;
		}
	}
}
