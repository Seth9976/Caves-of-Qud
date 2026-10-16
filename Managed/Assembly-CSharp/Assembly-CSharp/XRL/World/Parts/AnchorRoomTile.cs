using System;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x020012F7 RID: 4855
	[Serializable]
	public class AnchorRoomTile : IPart
	{
		// Token: 0x0600BE76 RID: 48758 RVA: 0x0045C05B File Offset: 0x0045A25B
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BE77 RID: 48759 RVA: 0x0045C05E File Offset: 0x0045A25E
		public override bool WantEvent(int ID, int Cascade)
		{
			return base.WantEvent(ID, Cascade) || ID == ObjectEnteredCellEvent.ID;
		}

		// Token: 0x0600BE78 RID: 48760 RVA: 0x0045C074 File Offset: 0x0045A274
		public override bool HandleEvent(ObjectEnteredCellEvent E)
		{
			if (GameObject.Validate(E.Object) && E.Object.HasMarkOfDeath(null) && !E.Object.HasEffect<CorpseTethered>())
			{
				E.Object.ApplyEffect(new CorpseTethered(), null);
				if (E.Object.IsPlayer())
				{
					E.Object.ParticleText("Tomb-tethered!", 'G', false, 1.5f, -8f);
				}
			}
			return base.HandleEvent(E);
		}
	}
}
