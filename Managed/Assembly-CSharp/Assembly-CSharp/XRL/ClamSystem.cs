using System;
using System.Collections.Generic;
using Qud.API;
using XRL.World;

namespace XRL
{
	// Token: 0x020004EF RID: 1263
	[Serializable]
	public class ClamSystem : IGameSystem
	{
		// Token: 0x0600368D RID: 13965 RVA: 0x001076E7 File Offset: 0x001058E7
		public Zone GetClamZone()
		{
			return The.ZoneManager.GetZone(this.ClamWorldId);
		}

		// Token: 0x0600368E RID: 13966 RVA: 0x001076F9 File Offset: 0x001058F9
		public Zone GetJoppaZone(int ClamID)
		{
			if (ClamID >= 0 && ClamID < this.clamJoppaZone.Count)
			{
				return The.ZoneManager.GetZone(this.clamJoppaZone[ClamID]);
			}
			return this.GetJoppaZone(null);
		}

		// Token: 0x0600368F RID: 13967 RVA: 0x0010772C File Offset: 0x0010592C
		public Zone GetJoppaZone(string Exclude = null)
		{
			string text = Exclude;
			int num = 0;
			while (num < 100 && text == Exclude)
			{
				text = this.clamJoppaZone.GetRandomElement<string>(null);
				num++;
			}
			return The.ZoneManager.GetZone(text);
		}

		// Token: 0x06003690 RID: 13968 RVA: 0x00107769 File Offset: 0x00105969
		public override void Register(XRLGame Game, IEventRegistrar Registrar)
		{
			Registrar.Register(GaveDirectionsEvent.ID, 0, false);
		}

		// Token: 0x06003691 RID: 13969 RVA: 0x00107778 File Offset: 0x00105978
		public override bool HandleEvent(GaveDirectionsEvent E)
		{
			if (E.Actor == The.Player)
			{
				Zone currentZone = E.Actor.CurrentZone;
				if (((currentZone != null) ? currentZone.ZoneWorld : null) == "Tzimtzlum")
				{
					JournalAPI.RevealObservation("$Tzimtzlum", true);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04001B47 RID: 6983
		public string ClamWorldId = "Tzimtzlum.40.12.1.1.10";

		// Token: 0x04001B48 RID: 6984
		public List<string> clamJoppaZone = new List<string>();
	}
}
