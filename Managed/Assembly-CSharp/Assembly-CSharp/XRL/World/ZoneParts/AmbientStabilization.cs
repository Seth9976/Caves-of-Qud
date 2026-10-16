using System;
using XRL.UI;
using XRL.World.Effects;

namespace XRL.World.ZoneParts
{
	// Token: 0x020009C2 RID: 2498
	[Serializable]
	public class AmbientStabilization : IZonePart
	{
		// Token: 0x06006B49 RID: 27465 RVA: 0x00228CCA File Offset: 0x00226ECA
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<EndTurnEvent>.ID || ID == ZoneActivatedEvent.ID;
		}

		// Token: 0x06006B4A RID: 27466 RVA: 0x00228CE8 File Offset: 0x00226EE8
		public override bool HandleEvent(EndTurnEvent E)
		{
			this.Stabilize();
			return base.HandleEvent(E);
		}

		// Token: 0x06006B4B RID: 27467 RVA: 0x00228CF7 File Offset: 0x00226EF7
		public override bool HandleEvent(ZoneActivatedEvent E)
		{
			this.Stabilize();
			return base.HandleEvent(E);
		}

		// Token: 0x06006B4C RID: 27468 RVA: 0x00228D06 File Offset: 0x00226F06
		public override void AddedAfterCreation()
		{
			base.AddedAfterCreation();
			this.Stabilize();
		}

		// Token: 0x06006B4D RID: 27469 RVA: 0x00228D14 File Offset: 0x00226F14
		public void Stabilize()
		{
			if (this.ParentZone.HasObject(The.Player) && !The.Player.HasEffect<AmbientRealityStabilized>())
			{
				Popup.Show("You feel some ambient astral friction here.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				The.Player.ApplyEffect(new AmbientRealityStabilized(this.Strength), null);
			}
		}

		// Token: 0x0400311B RID: 12571
		public int Strength = 40;
	}
}
