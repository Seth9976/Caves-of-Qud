using System;
using XRL.Messages;
using XRL.World;

namespace XRL
{
	// Token: 0x020004F1 RID: 1265
	[Serializable]
	public class HolyPlaceSystem : IGameSystem
	{
		// Token: 0x0600369D RID: 13981 RVA: 0x001079A2 File Offset: 0x00105BA2
		public override void Register(XRLGame Game, IEventRegistrar Registrar)
		{
			Registrar.Register(ZoneActivatedEvent.ID, 0, false);
			Registrar.Register(SingletonEvent<AfterGameLoadedEvent>.ID, 0, false);
		}

		// Token: 0x0600369E RID: 13982 RVA: 0x001079C0 File Offset: 0x00105BC0
		public override bool HandleEvent(ZoneActivatedEvent E)
		{
			if (E.Zone.IsWorldMap())
			{
				return base.HandleEvent(E);
			}
			if (E.Zone.ZoneID == HolyPlaceSystem.LastZoneID)
			{
				return base.HandleEvent(E);
			}
			this.SetHolyZone(E.Zone, Factions.GetZoneHolyFaction(E.Zone.ZoneID));
			return base.HandleEvent(E);
		}

		// Token: 0x0600369F RID: 13983 RVA: 0x00107A24 File Offset: 0x00105C24
		public override bool HandleEvent(AfterGameLoadedEvent E)
		{
			Zone activeZone = The.ActiveZone;
			if (activeZone != null)
			{
				this.SetHolyZone(activeZone, Factions.GetZoneHolyFaction(activeZone.ZoneID));
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060036A0 RID: 13984 RVA: 0x00107A53 File Offset: 0x00105C53
		public void SetHolyZone(Zone Z, Faction Faction = null)
		{
			if (Faction != null && Faction != HolyPlaceSystem.LastHolyFaction)
			{
				MessageQueue.AddPlayerMessage("You feel a sense of holiness here.", null, true);
			}
			HolyPlaceSystem.LastHolyFaction = Faction;
			HolyPlaceSystem.LastZoneID = Z.ZoneID;
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x060036A1 RID: 13985 RVA: 0x00107A7D File Offset: 0x00105C7D
		public override bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04001B4B RID: 6987
		public static Faction LastHolyFaction;

		// Token: 0x04001B4C RID: 6988
		public static string LastZoneID;
	}
}
