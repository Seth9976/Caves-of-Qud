using System;
using XRL.UI;

namespace XRL.World.Quests
{
	// Token: 0x02000B27 RID: 2855
	[Serializable]
	public class PaxKlanqIPresumeSystem : IQuestSystem
	{
		// Token: 0x060072B6 RID: 29366 RVA: 0x002B6B48 File Offset: 0x002B4D48
		public PaxKlanqIPresumeSystem()
		{
			this.QuestID = "Pax Klanq, I Presume?";
		}

		// Token: 0x060072B7 RID: 29367 RVA: 0x002B6B5B File Offset: 0x002B4D5B
		public override void Register(XRLGame Game, IEventRegistrar Registrar)
		{
			Registrar.Register(ZoneActivatedEvent.ID, 0, false);
		}

		// Token: 0x060072B8 RID: 29368 RVA: 0x002B6B6A File Offset: 0x002B4D6A
		public override void RegisterPlayer(GameObject Player, IEventRegistrar Registrar)
		{
			Registrar.Register(AfterConsumeEvent.ID, 0, false);
		}

		// Token: 0x060072B9 RID: 29369 RVA: 0x002B6B7C File Offset: 0x002B4D7C
		public override bool HandleEvent(ZoneActivatedEvent E)
		{
			if (E.Zone.X == 1 && E.Zone.Y == 1 && E.Zone.Z == 10)
			{
				GameObject terrainObject = E.Zone.GetTerrainObject();
				if (((terrainObject != null) ? terrainObject.Blueprint : null) == "TerrainFungalCenter")
				{
					The.Game.FinishQuestStep("Pax Klanq, I Presume?", "Seek the Heart of the Rainbow", -1, true, null);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060072BA RID: 29370 RVA: 0x002B6BF6 File Offset: 0x002B4DF6
		public override bool HandleEvent(AfterConsumeEvent E)
		{
			if (E.Ingest && E.Object.Blueprint == "Godshroom Cap")
			{
				The.Game.FinishQuestStep("Pax Klanq, I Presume?", "Eat the God's Flesh", -1, true, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060072BB RID: 29371 RVA: 0x002B6C36 File Offset: 0x002B4E36
		public override GameObject GetInfluencer()
		{
			return GameObject.FindByBlueprint("Pax Klanq");
		}

		// Token: 0x060072BC RID: 29372 RVA: 0x002B6C42 File Offset: 0x002B4E42
		public static bool UnderConstructionMessage()
		{
			Popup.ShowSpace("You've reached the temporary end of the main questline.\n\nYou may continue to explore the world, and stay tuned for updates as we prepare to leave Early Access.", null, "Sounds/UI/ui_notification", null, true, true, null);
			return true;
		}

		// Token: 0x0400352A RID: 13610
		public const string PaxQuestID = "Pax Klanq, I Presume?";
	}
}
