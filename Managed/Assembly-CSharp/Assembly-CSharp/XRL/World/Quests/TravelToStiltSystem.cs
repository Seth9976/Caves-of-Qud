using System;

namespace XRL.World.Quests
{
	// Token: 0x02000B29 RID: 2857
	[Serializable]
	public class TravelToStiltSystem : IQuestSystem
	{
		// Token: 0x060072C0 RID: 29376 RVA: 0x002B6CA9 File Offset: 0x002B4EA9
		public override void Register(XRLGame Game, IEventRegistrar Registrar)
		{
			Registrar.Register(ZoneActivatedEvent.ID, 0, false);
		}

		// Token: 0x060072C1 RID: 29377 RVA: 0x002B6CB8 File Offset: 0x002B4EB8
		public override bool HandleEvent(ZoneActivatedEvent E)
		{
			Zone zone = E.Zone;
			GameObject gameObject = ((zone != null) ? zone.GetTerrainObject() : null);
			if (((gameObject != null) ? gameObject.Blueprint : null) == "TerrainSixDayStilt")
			{
				The.Game.FinishQuestStep("O Glorious Shekhinah!", "Make a Pilgrimage to the Six Day Stilt", -1, true, E.Zone.ZoneID);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060072C2 RID: 29378 RVA: 0x002B6D18 File Offset: 0x002B4F18
		public override GameObject GetInfluencer()
		{
			if (50.in100())
			{
				return GameObject.FindByBlueprint("Wardens Esther");
			}
			return GameObject.FindByBlueprint("Tszappur");
		}
	}
}
