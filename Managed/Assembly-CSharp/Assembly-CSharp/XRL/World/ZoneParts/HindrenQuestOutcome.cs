using System;
using System.Collections.Generic;
using System.Linq;
using XRL.Rules;
using XRL.World.AI;
using XRL.World.AI.GoalHandlers;
using XRL.World.Parts;

namespace XRL.World.ZoneParts
{
	// Token: 0x020009C3 RID: 2499
	[Serializable]
	public class HindrenQuestOutcome : IZonePart
	{
		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x06006B4F RID: 27471 RVA: 0x00228D7B File Offset: 0x00226F7B
		public string outcome
		{
			get
			{
				return string.Format("outcome-{0}-{1}-{2}", this.thief, this.climate, this.loveState);
			}
		}

		// Token: 0x06006B50 RID: 27472 RVA: 0x00228D99 File Offset: 0x00226F99
		public override bool SameAs(IZonePart p)
		{
			return false;
		}

		// Token: 0x06006B51 RID: 27473 RVA: 0x00228D9C File Offset: 0x00226F9C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ObjectCreatedEvent.ID || ID == ZoneActivatedEvent.ID;
		}

		// Token: 0x06006B52 RID: 27474 RVA: 0x00228DBA File Offset: 0x00226FBA
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			if (The.Game != null)
			{
				this.startTick = The.Game.TimeTicks;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06006B53 RID: 27475 RVA: 0x00228DDC File Offset: 0x00226FDC
		public override bool HandleEvent(ZoneActivatedEvent E)
		{
			if (!this.outcomeApplied && The.Game.TimeTicks - this.startTick >= this.ticksToApply)
			{
				this.outcomeApplied = true;
				this.ApplyOutcome(this.climate, this.thief, this.circumstance, this.motive, this.loveState);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06006B54 RID: 27476 RVA: 0x00228E3C File Offset: 0x0022703C
		public override void AddedAfterCreation()
		{
			base.AddedAfterCreation();
			if (The.Game != null)
			{
				this.startTick = The.Game.TimeTicks;
			}
		}

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x06006B55 RID: 27477 RVA: 0x00228E5B File Offset: 0x0022705B
		private Zone zone
		{
			get
			{
				return KithAndKinGameState.Instance.getBeyLahZone();
			}
		}

		// Token: 0x06006B56 RID: 27478 RVA: 0x00228E68 File Offset: 0x00227068
		public string CurrentHindriarch()
		{
			if (this.thief == "keh")
			{
				return "esk";
			}
			if (this.thief == "kese" && (!(this.loveState == "love") || !(this.climate == "tumultuous")))
			{
				return "esk";
			}
			return "keh";
		}

		// Token: 0x06006B57 RID: 27479 RVA: 0x00228ED0 File Offset: 0x002270D0
		public void ApplyPrologue()
		{
			string text = this.CurrentHindriarch();
			if (text == "esk")
			{
				this.EskhindReturns();
				this.KehIsReplacedByEskhind();
				Achievement.BEY_LAH_COMEDY.Unlock();
			}
			if (text == "keh")
			{
				this.KehRemainsInPower();
				Achievement.BEY_LAH_HISTORY.Unlock();
			}
			this.zone.FindObject("Neelahind").RequirePart<GivesRep>(false);
			if (this.climate == "prosperous")
			{
				The.Game.PlayerReputation.Modify("Hindren", 200, "Quest", null, null, false, false);
			}
			else if (this.climate == "tumultuous")
			{
				The.Game.PlayerReputation.Modify("Hindren", 50, "Quest", null, null, false, false);
				if (!(this.loveState == "love") || !(this.thief == "keh"))
				{
					this.VillageIsDoomed();
				}
			}
			else if (this.climate == "mixed")
			{
				The.Game.PlayerReputation.Modify("Hindren", 100, "Quest", null, null, false, false);
			}
			if (this.thief == "esk" || this.thief == "kendren")
			{
				this.EskhindRemainsInExile();
				if (this.loveState == "love")
				{
					this.NeelahindJoinsEskhindInExile();
				}
			}
			else if (this.thief == "keh")
			{
				this.KehIsExiled();
			}
			else if (this.thief == "kese")
			{
				this.KesehindIsExiled();
				if (this.loveState == "love" && this.climate == "tumultuous")
				{
					this.EskhindRemainsInExile();
					this.NeelahindJoinsEskhindInExile();
				}
			}
			foreach (GameObject gameObject in this.zone.GetObjects((GameObject o) => o.HasPart<HindrenClueItem>() && !o.HasPart<HindrenMysteryCriticalNPC>()))
			{
				gameObject.Destroy(null, false, false, null);
			}
		}

		// Token: 0x06006B58 RID: 27480 RVA: 0x00229118 File Offset: 0x00227318
		public void ApplyOutcome(string climate, string thief, string circumstance, string motive, string loveState)
		{
			The.Game.SetBooleanGameState("HindrenQuestFullyResolved", true);
			if (climate == "prosperous")
			{
				if (thief == "esk" && loveState == "love")
				{
					this.VillageSurvives();
					return;
				}
				if (thief == "kese" && loveState == "nolove")
				{
					this.VillageSurvives();
					return;
				}
				this.VillageProspers();
				return;
			}
			else
			{
				if (!(climate == "mixed"))
				{
					if (climate == "tumultuous")
					{
						if (thief == "keh" && loveState == "love")
						{
							this.VillageSurvives();
							return;
						}
						this.VillageRavaged();
					}
					return;
				}
				if (thief == "keh" && loveState == "love")
				{
					this.VillageProspers();
					return;
				}
				this.VillageSurvives();
				return;
			}
		}

		// Token: 0x06006B59 RID: 27481 RVA: 0x002291F8 File Offset: 0x002273F8
		public void VillageProspers()
		{
			The.Game.SetBooleanGameState("HindrenVillageProspers", true);
			GameObject gameObject = this.zone.FindObject("Isahind");
			if (gameObject != null)
			{
				GenericInventoryRestocker genericInventoryRestocker = gameObject.RequirePart<GenericInventoryRestocker>(false);
				genericInventoryRestocker.Table = "Tier2Wares";
				genericInventoryRestocker.PerformRestock(true);
			}
		}

		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x06006B5A RID: 27482 RVA: 0x00229241 File Offset: 0x00227441
		public List<GameObject> allVillagers
		{
			get
			{
				return this.zone.GetObjects((GameObject o) => o.GetBlueprint(true).InheritsFrom("BaseHindren"));
			}
		}

		// Token: 0x06006B5B RID: 27483 RVA: 0x00229270 File Offset: 0x00227470
		public static void Exile(GameObject Object, GlobalLocation Destination = null)
		{
			Object.AddPart<HindrenMysteryExile>(new HindrenMysteryExile
			{
				Destination = Destination
			}, true, false);
			Object.RequirePart<SocialRoles>(false).RequireRole("Hindren Pariah");
			Object.RemovePart<GivesRep>();
			CleanStack<GoalHandler> goals = Object.Brain.Goals;
			if (goals == null)
			{
				return;
			}
			goals.Clear();
		}

		// Token: 0x06006B5C RID: 27484 RVA: 0x002292C0 File Offset: 0x002274C0
		public void Exile(string blueprint, GlobalLocation destination = null)
		{
			this.zone.FindObjects(blueprint).ForEach(delegate(GameObject o)
			{
				HindrenQuestOutcome.Exile(o, destination);
			});
		}

		// Token: 0x06006B5D RID: 27485 RVA: 0x002292F7 File Offset: 0x002274F7
		public void VillageSurvives()
		{
			The.Game.SetBooleanGameState("HindrenVillageSurvives", true);
		}

		// Token: 0x06006B5E RID: 27486 RVA: 0x0022930C File Offset: 0x0022750C
		public void VillageRavaged()
		{
			this.allVillagers.ForEach(delegate(GameObject o)
			{
				o.Destroy(null, false, false, null);
			});
			int i = 0;
			int num = Stat.Random(6, 8);
			while (i < num)
			{
				this.zone.GetEmptyCellsShuffled().First<Cell>().AddObject("Hindren Corpse", null, null, null, null);
				i++;
			}
			int j = 0;
			int num2 = Stat.Random(6, 8);
			while (j < num2)
			{
				this.zone.GetEmptyCellsShuffled().First<Cell>().AddObject("Bloodsplatter", null, null, null, null);
				j++;
			}
			int k = 0;
			int num3 = Stat.Random(6, 8);
			while (k < num3)
			{
				this.zone.GetEmptyCellsShuffled().First<Cell>().AddObject("Firestarter", null, null, null, null);
				k++;
			}
			The.Game.SetBooleanGameState("HindrenVillageRavaged", true);
		}

		// Token: 0x06006B5F RID: 27487 RVA: 0x002293F2 File Offset: 0x002275F2
		public void VillageIsDoomed()
		{
			The.Game.SetBooleanGameState("HindrenVillageDoomed", true);
			Achievement.BEY_LAH_TRAGEDY.Unlock();
		}

		// Token: 0x06006B60 RID: 27488 RVA: 0x00229410 File Offset: 0x00227610
		public void VillageCollapses()
		{
			this.allVillagers.ForEach(delegate(GameObject o)
			{
				o.Destroy(null, false, false, null);
			});
			The.Game.SetBooleanGameState("HindrenVillageCollapses", true);
			int i = 0;
			int num = Stat.Random(6, 8);
			while (i < num)
			{
				this.zone.GetEmptyCellsShuffled().First<Cell>().AddObject("HindrenAfflicted", null, null, null, null);
				i++;
			}
			int j = 0;
			int num2 = Stat.Random(6, 8);
			while (j < num2)
			{
				this.zone.GetEmptyCellsShuffled().First<Cell>().AddObject("Hindren Corpse", null, null, null, null);
				j++;
			}
			int k = 0;
			int num3 = Stat.Random(30, 40);
			while (k < num3)
			{
				this.zone.GetEmptyCellsShuffled().First<Cell>().AddObject("PutrescencePool", null, null, null, null);
				k++;
			}
		}

		// Token: 0x06006B61 RID: 27489 RVA: 0x002294F8 File Offset: 0x002276F8
		public void KehIsReplacedByEskhind()
		{
			GameObject gameObject = this.zone.FindObject("Keh");
			if (gameObject != null)
			{
				GameObjectBlueprint blueprint = GameObjectFactory.Factory.GetBlueprint("PariahKeh");
				gameObject.DisplayName = blueprint.GetPartParameter<string>("Render", "DisplayName", null);
				gameObject.RequirePart<Description>(false).Short = blueprint.GetPartParameter<string>("Description", "Short", null);
				gameObject.Brain.PushGoal(new MoveTo(this.zone.GetCell(30, 20), false, false, 0, false, false, false, -1, -1, 95));
				gameObject.Brain.StartingCell = this.zone.GetCell(30, 20).GetGlobalLocation();
				gameObject.RemoveStringProperty("Mayor");
			}
			GameObject gameObject2 = this.zone.FindObject("Eskhind");
			if (gameObject2 != null)
			{
				gameObject2.DisplayName = "Hindriarch Esk";
				gameObject2.SetStringProperty("WaterRitual_Skill", "Multiweapon_Fighting", false);
				gameObject2.Brain.Allegiance.Clear();
				gameObject2.Brain.Allegiance.Add("Hindren", 100);
				gameObject2.RemovePart<GivesRep>();
				gameObject2.AddPart<GivesRep>(new GivesRep(), true, false);
				gameObject2.SetStringProperty("Mayor", "Hindren", false);
			}
			GameObject gameObject3 = this.zone.FindObject("Meyehind");
			if (gameObject3 != null)
			{
				gameObject3.Brain.Allegiance.Clear();
				gameObject3.Brain.Allegiance.Add("Hindren", 100);
			}
			GameObject gameObject4 = this.zone.FindObject("Liihart");
			if (gameObject4 != null)
			{
				gameObject4.Brain.Allegiance.Clear();
				gameObject4.Brain.Allegiance.Add("Hindren", 100);
			}
			this.EskhindIsAppointedAdvisor();
		}

		// Token: 0x06006B62 RID: 27490 RVA: 0x002296B0 File Offset: 0x002278B0
		public void KehRemainsInPower()
		{
			GameObject gameObject = this.zone.FindObject("Keh");
			if (gameObject != null)
			{
				gameObject.SetStringProperty("WaterRitual_Skill", "Persuasion_Berate", false);
				gameObject.Brain.Allegiance.Clear();
				gameObject.Brain.Allegiance.Add("Hindren", 100);
				gameObject.RemovePart<GivesRep>();
				gameObject.AddPart<GivesRep>(new GivesRep(), true, false);
			}
		}

		// Token: 0x06006B63 RID: 27491 RVA: 0x0022971E File Offset: 0x0022791E
		public void KehIsExiled()
		{
			this.Exile("Keh", null);
		}

		// Token: 0x06006B64 RID: 27492 RVA: 0x0022972C File Offset: 0x0022792C
		public void EskhindIsAppointedAdvisor()
		{
			GameObject gameObject = this.zone.FindObject("Eskhind");
			if (gameObject != null)
			{
				Cell cell = this.zone.GetCell(73, 7);
				gameObject.Brain.PushGoal(new MoveTo(cell, false, true, 0, false, false, false, -1, -1, 95));
				gameObject.Brain.StartingCell = cell.GetGlobalLocation();
			}
			GameObject gameObject2 = this.zone.FindObject("Meyehind");
			if (gameObject2 != null)
			{
				Cell cell2 = this.zone.GetCell(73, 9);
				gameObject2.Brain.PushGoal(new MoveTo(cell2, false, false, 0, false, false, false, -1, -1, 95));
				gameObject2.Brain.StartingCell = cell2.GetGlobalLocation();
			}
			GameObject gameObject3 = this.zone.FindObject("Liihart");
			if (gameObject3 != null)
			{
				Cell cell3 = this.zone.GetCell(72, 10);
				gameObject3.Brain.PushGoal(new MoveTo(cell3, false, false, 0, false, false, false, -1, -1, 95));
				gameObject3.Brain.StartingCell = cell3.GetGlobalLocation();
			}
		}

		// Token: 0x06006B65 RID: 27493 RVA: 0x00229834 File Offset: 0x00227A34
		public void EskhindRemainsInExile()
		{
			this.Exile("Eskhind", GlobalLocation.FromZoneId(The.Game.GetStringGameState("HollowTreeZoneId", ""), 35, 9));
			this.Exile("Meyehind", GlobalLocation.FromZoneId(The.Game.GetStringGameState("HollowTreeZoneId", ""), 35, 10));
			this.Exile("Liihart", GlobalLocation.FromZoneId(The.Game.GetStringGameState("HollowTreeZoneId", ""), 34, 9));
		}

		// Token: 0x06006B66 RID: 27494 RVA: 0x002298B9 File Offset: 0x00227AB9
		public void EskhindReturns()
		{
			The.Game.SetBooleanGameState("EskhindReturned", true);
			GameObject gameObject = this.zone.FindObject("Eshkind");
			if (gameObject == null)
			{
				return;
			}
			gameObject.SetIntProperty("ReturnedEskhind", 1, false);
		}

		// Token: 0x06006B67 RID: 27495 RVA: 0x002298ED File Offset: 0x00227AED
		public void NeelahindJoinsEskhindInExile()
		{
			The.Game.SetBooleanGameState("HindrenMysteryLovebirdsEloped", true);
			this.Exile("Neelahind", GlobalLocation.FromZoneId(The.Game.GetStringGameState("HollowTreeZoneId", ""), 35, 10));
		}

		// Token: 0x06006B68 RID: 27496 RVA: 0x00229927 File Offset: 0x00227B27
		public void KesehindIsExiled()
		{
			this.Exile("Kesehind", null);
		}

		// Token: 0x06006B69 RID: 27497 RVA: 0x00229938 File Offset: 0x00227B38
		public string DetermineClimate()
		{
			List<string> list = new List<string> { "trade", "craft" };
			List<string> list2 = new List<string> { "illness", "violence" };
			if (list.Contains(this.circumstance) && list.Contains(this.motive))
			{
				return this.climate = "prosperous";
			}
			if (list2.Contains(this.circumstance) && list2.Contains(this.motive))
			{
				return this.climate = "tumultuous";
			}
			return this.climate = "mixed";
		}

		// Token: 0x0400311C RID: 12572
		public long startTick;

		// Token: 0x0400311D RID: 12573
		public long ticksToApply = 3600L;

		// Token: 0x0400311E RID: 12574
		public string thief;

		// Token: 0x0400311F RID: 12575
		public string circumstance;

		// Token: 0x04003120 RID: 12576
		public string motive;

		// Token: 0x04003121 RID: 12577
		public string climate;

		// Token: 0x04003122 RID: 12578
		public string loveState;

		// Token: 0x04003123 RID: 12579
		public bool outcomeApplied;
	}
}
