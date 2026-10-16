using System;

namespace XRL.World.Parts
{
	// Token: 0x02000FDC RID: 4060
	[Obsolete("Replaced by GameUnique")]
	[Serializable]
	public class Eskhind : IPart
	{
		// Token: 0x0600A141 RID: 41281 RVA: 0x003AB0CA File Offset: 0x003A92CA
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600A142 RID: 41282 RVA: 0x003AB0CD File Offset: 0x003A92CD
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ZoneActivatedEvent.ID;
		}

		// Token: 0x0600A143 RID: 41283 RVA: 0x003AB0E3 File Offset: 0x003A92E3
		public override bool HandleEvent(ZoneActivatedEvent E)
		{
			this.ParentObject.RemovePart(this);
			The.Game.RemoveStringGameState("EskhindMoved");
			return base.HandleEvent(E);
		}
	}
}
