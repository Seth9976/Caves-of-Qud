using System;

namespace XRL.World.Parts
{
	// Token: 0x020012FE RID: 4862
	[Serializable]
	public class HolyPlace : IPart
	{
		// Token: 0x0600BE91 RID: 48785 RVA: 0x0045C4F2 File Offset: 0x0045A6F2
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ZoneActivatedEvent.ID;
		}

		// Token: 0x0600BE92 RID: 48786 RVA: 0x0045C508 File Offset: 0x0045A708
		public override bool HandleEvent(ZoneActivatedEvent E)
		{
			if (!this.Faction.IsNullOrEmpty())
			{
				Faction ifExists = Factions.GetIfExists(this.Faction);
				if (ifExists != null)
				{
					HolyPlaceSystem system = The.Game.GetSystem<HolyPlaceSystem>();
					if (system != null)
					{
						system.SetHolyZone(this.ParentObject.CurrentZone, ifExists);
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04004CB8 RID: 19640
		public string Faction;
	}
}
