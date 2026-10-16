using System;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000EE7 RID: 3815
	[Serializable]
	public class NoBleed : IPart
	{
		// Token: 0x06009892 RID: 39058 RVA: 0x0037693A File Offset: 0x00374B3A
		public override bool WantEvent(int ID, int Cascade)
		{
			return base.WantEvent(ID, Cascade) || ID == CanApplyEffectEvent.ID;
		}

		// Token: 0x06009893 RID: 39059 RVA: 0x00376950 File Offset: 0x00374B50
		public override bool HandleEvent(CanApplyEffectEvent E)
		{
			return E.Type != typeof(Bleeding);
		}
	}
}
