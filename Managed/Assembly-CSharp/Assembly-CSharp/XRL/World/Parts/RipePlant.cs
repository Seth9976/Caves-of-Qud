using System;

namespace XRL.World.Parts
{
	// Token: 0x02001285 RID: 4741
	[Serializable]
	public class RipePlant : IPart
	{
		// Token: 0x0600BBA0 RID: 48032 RVA: 0x004486FC File Offset: 0x004468FC
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeDeathRemovalEvent.ID;
		}

		// Token: 0x0600BBA1 RID: 48033 RVA: 0x00448712 File Offset: 0x00446912
		public override bool HandleEvent(BeforeDeathRemovalEvent E)
		{
			The.Game.IncrementIntGameState("ripe plants killed", 1);
			return base.HandleEvent(E);
		}
	}
}
