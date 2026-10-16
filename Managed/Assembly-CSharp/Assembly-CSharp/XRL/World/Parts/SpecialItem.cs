using System;

namespace XRL.World.Parts
{
	// Token: 0x0200109A RID: 4250
	[Serializable]
	public class SpecialItem : IPart
	{
		// Token: 0x0600A91F RID: 43295 RVA: 0x003DD621 File Offset: 0x003DB821
		public override bool WantEvent(int ID, int cascade)
		{
			return (this.RemovePlayerTake && (ID == TakenEvent.ID || ID == EquippedEvent.ID)) || base.WantEvent(ID, cascade);
		}

		// Token: 0x0600A920 RID: 43296 RVA: 0x003DD645 File Offset: 0x003DB845
		public override bool HandleEvent(TakenEvent E)
		{
			if (this.RemovePlayerTake && E.Actor != null && E.Actor.IsPlayerControlled())
			{
				this.ParentObject.RemovePart(this);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A921 RID: 43297 RVA: 0x003DD677 File Offset: 0x003DB877
		public override bool HandleEvent(EquippedEvent E)
		{
			if (this.RemovePlayerTake && E.Actor != null && E.Actor.IsPlayerControlled())
			{
				this.ParentObject.RemovePart(this);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x040045D6 RID: 17878
		public bool RemovePlayerTake = true;
	}
}
