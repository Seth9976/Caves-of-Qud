using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Qud.API;
using XRL.UI;
using XRL.World.Anatomy;
using XRL.World.Parts;
using XRL.World.Tinkering;
using XRL.World.ZoneParts;

namespace XRL.World.Conversations.Parts
{
	// Token: 0x02000C08 RID: 3080
	public class KithAndKinFinale : IKithAndKinPart
	{
		// Token: 0x06007BA4 RID: 31652 RVA: 0x002E6A71 File Offset: 0x002E4C71
		public override bool WantEvent(int ID, int Propagation)
		{
			return base.WantEvent(ID, Propagation) || ID == EnteredElementEvent.ID || ID == LeftElementEvent.ID || ID == PrepareTextEvent.ID || ID == GetTextElementEvent.ID;
		}

		// Token: 0x06007BA5 RID: 31653 RVA: 0x002E6AA0 File Offset: 0x002E4CA0
		public override bool HandleEvent(EnteredElementEvent E)
		{
			this.Outcome = new HindrenQuestOutcome
			{
				loveState = ((The.Game.GetQuestFinishTime("Love and Fear") > 0L) ? "love" : "nolove"),
				thief = base.Thief,
				circumstance = base.CircumstanceInfluence,
				motive = base.MotiveInfluence
			};
			The.Game.SetStringGameState("HindrenMysteryOutcomeClimate", IKithAndKinPart.KeyOf(this.Outcome.DetermineClimate()));
			The.Game.SetStringGameState("HindrenMysteryOutcomeCircumstance", IKithAndKinPart.KeyOf(this.Outcome.circumstance));
			The.Game.SetStringGameState("HindrenMysteryOutcomeThief", IKithAndKinPart.KeyOf(this.Outcome.thief));
			The.Game.SetStringGameState("HindrenMysteryOutcomeHindriarch", IKithAndKinPart.KeyOf(this.Outcome.CurrentHindriarch()));
			The.Game.RemoveStringGameState("KithAndKinEliminated");
			this.Key = IKithAndKinPart.KeyOf(this.Outcome.thief) + IKithAndKinPart.KeyOf(this.Outcome.climate) + IKithAndKinPart.KeyOf(this.Outcome.loveState);
			return base.HandleEvent(E);
		}

		// Token: 0x06007BA6 RID: 31654 RVA: 0x002E6BD0 File Offset: 0x002E4DD0
		public override bool HandleEvent(LeftElementEvent E)
		{
			KithAndKinGameState.Instance.getBeyLahZone().AddPart<HindrenQuestOutcome>(this.Outcome, false);
			The.Game.FinishQuestStep("Kith and Kin", "Accuse the thief", -1, true, null);
			The.Game.FinishQuest("Find Eskhind");
			this.Outcome.ApplyPrologue();
			this.Reward();
			return base.HandleEvent(E);
		}

		// Token: 0x06007BA7 RID: 31655 RVA: 0x002E6C33 File Offset: 0x002E4E33
		public override bool HandleEvent(GetTextElementEvent E)
		{
			E.Selected = E.Visible.FirstOrDefault<ConversationText>((ConversationText x) => x.ID == this.Key) ?? E.Selected;
			return base.HandleEvent(E);
		}

		// Token: 0x06007BA8 RID: 31656 RVA: 0x002E6C64 File Offset: 0x002E4E64
		public override bool HandleEvent(PrepareTextEvent E)
		{
			string circumstanceInfluence = base.CircumstanceInfluence;
			string motiveInfluence = base.MotiveInfluence;
			string text = ((circumstanceInfluence == motiveInfluence) ? circumstanceInfluence : (circumstanceInfluence + " and " + motiveInfluence));
			E.Text.StartReplace().AddReplacer("all.influence", text).AddReplacer("motive.influence", motiveInfluence)
				.AddReplacer("thief.name", base.ThiefName)
				.Execute();
			return base.HandleEvent(E);
		}

		// Token: 0x06007BA9 RID: 31657 RVA: 0x002E6CD8 File Offset: 0x002E4ED8
		public void Reward()
		{
			string climate = this.Outcome.climate;
			string motive = this.Outcome.motive;
			string circumstance = this.Outcome.circumstance;
			string thief = this.Outcome.thief;
			List<GameObject> list = new List<GameObject>();
			List<string> list2 = new List<string>();
			bool flag = false;
			if (thief == "keh")
			{
				if (motive == "violence")
				{
					if (circumstance == "violence")
					{
						list.Add(GameObjectFactory.Factory.CreateObject(GameObjectFactory.Factory.Blueprints["Dagger3"], 0, 2, null, null, null, null, null));
					}
					else
					{
						list.Add(GameObjectFactory.Factory.CreateObject("Dagger3"));
					}
				}
				else if (motive == "illness")
				{
					int num = 4;
					if (circumstance == "violence")
					{
						num = 8;
					}
					for (int i = 0; i < num; i++)
					{
						list.Add(GameObject.Create(PopulationManager.RollOneFrom("DynamicObjectsTable:Tonics_NonRare", null, null).Blueprint));
					}
				}
				else if (motive == "craft")
				{
					if (circumstance == "violence")
					{
						list.Add(GameObjectFactory.Factory.CreateObject(GameObjectFactory.Factory.Blueprints["Homoelectric Wrist Warmer"], 0, 2, null, null, null, null, null));
					}
					else
					{
						list.Add(GameObjectFactory.Factory.CreateObject("Homoelectric Wrist Warmer"));
					}
				}
				else if (motive == "trade")
				{
					int num2 = 3;
					if (circumstance == "violence")
					{
						num2 = 8;
					}
					if (circumstance == "trade")
					{
						num2 = 6;
						flag = true;
					}
					for (int j = 0; j < num2; j++)
					{
						list.Add(GameObject.Create(PopulationManager.RollOneFrom("BooksAndRandomBooks", null, null).Blueprint));
					}
				}
			}
			else if (thief == "esk")
			{
				if (motive == "violence")
				{
					if (circumstance == "violence")
					{
						list.Add(GameObjectFactory.Factory.CreateObject(GameObjectFactory.Factory.Blueprints["Chain Pistol"], 0, 2, null, null, null, null, null));
					}
					else
					{
						list.Add(GameObjectFactory.Factory.CreateObject("Chain Pistol"));
					}
				}
				else if (motive == "illness")
				{
					list.Add(GameObjectFactory.Factory.CreateObject("FluxPhial"));
					if (circumstance == "violence")
					{
						list.Add(GameObjectFactory.Factory.CreateObject("FluxPhial"));
					}
				}
				else if (motive == "craft")
				{
					if (circumstance == "violence")
					{
						list.Add(GameObjectFactory.Factory.CreateObject(GameObjectFactory.Factory.Blueprints["Magnetized Boots"], 0, 2, null, null, null, null, null));
					}
					else
					{
						list.Add(GameObjectFactory.Factory.CreateObject("Magnetized Boots"));
					}
				}
				else if (motive == "trade")
				{
					int num3 = 3;
					for (int k = 0; k < num3; k++)
					{
						list.Add(GameObjectFactory.Factory.CreateObject(GameObjectFactory.Factory.Blueprints[PopulationManager.RollOneFrom("DynamicObjectsTable:EnergyCells:Tier0-4", null, null).Blueprint], 0, 2, null, null, null, null, null));
					}
				}
			}
			else if (thief == "kese")
			{
				if (motive == "violence")
				{
					if (circumstance == "violence")
					{
						list.Add(GameObjectFactory.Factory.CreateObject(GameObjectFactory.Factory.Blueprints["Ari"], 0, 2, null, null, null, null, null));
					}
					else
					{
						list.Add(GameObjectFactory.Factory.CreateObject("Ari"));
					}
					if (circumstance == "craft")
					{
						list2.Add("mod:ModFlaming");
					}
				}
				else if (motive == "illness")
				{
					BodyPart.MakeSeveredBodyParts((circumstance == "violence") ? 4 : 2, null, "Face", null, null, null, null, list);
				}
				else if (motive == "craft")
				{
					if (circumstance == "violence")
					{
						list.Add(GameObjectFactory.Factory.CreateObject("Carbide Plate Armor", 0, 2, null, null, null, null, null));
					}
					else
					{
						list.Add(GameObjectFactory.Factory.CreateObject("Carbide Plate Armor"));
					}
				}
				else if (motive == "trade")
				{
					int num4 = ((circumstance == "violence") ? 8 : 4);
					for (int l = 0; l < num4; l++)
					{
						list.Add(GameObject.Create(PopulationManager.RollOneFrom("DynamicObjectsTable:Grenades", null, null).Blueprint));
					}
				}
			}
			else if (thief == "kendren")
			{
				if (motive == "violence")
				{
					List<GameObject> list3 = new List<GameObject>();
					while (list3.Count < 3)
					{
						string blueprint2 = EncountersAPI.GetAnItemBlueprint((GameObjectBlueprint b) => b.Tier == 3 && b.DescendsFrom("MeleeWeapon") && EncountersAPI.IsEligibleForDynamicEncounters(b));
						if (!list3.Any<GameObject>((GameObject o) => o.Blueprint == blueprint2))
						{
							if (circumstance == "violence")
							{
								list3.Add(GameObjectFactory.Factory.CreateObject(GameObjectFactory.Factory.Blueprints[blueprint2], 0, 2, null, null, null, null, null));
							}
							else
							{
								list3.Add(GameObjectFactory.Factory.Blueprints[blueprint2].createOne());
							}
						}
					}
					list3.ForEach(delegate(GameObject o)
					{
						o.MakeUnderstood(false);
					});
					list.Add(ConversationsAPI.chooseOneItem(list3, "Choose an item", false));
				}
				else if (motive == "illness")
				{
					if (circumstance == "violence")
					{
						list.Add(GameObjectFactory.Factory.CreateObject(GameObjectFactory.Factory.Blueprints["Bio-Scanning Bracelet"], 0, 2, null, null, null, null, null));
					}
					else
					{
						list.Add(GameObjectFactory.Factory.CreateObject("Bio-Scanning Bracelet"));
					}
				}
				else if (motive == "craft")
				{
					List<GameObject> list4 = new List<GameObject>();
					while (list4.Count < 3)
					{
						string blueprint = EncountersAPI.GetAnItemBlueprint((GameObjectBlueprint b) => b.Tier == 3 && b.DescendsFrom("Armor") && EncountersAPI.IsEligibleForDynamicEncounters(b));
						if (!list4.Any<GameObject>((GameObject o) => o.Blueprint == blueprint))
						{
							if (circumstance == "violence")
							{
								list4.Add(GameObjectFactory.Factory.CreateObject(GameObjectFactory.Factory.Blueprints[blueprint], 0, 2, null, null, null, null, null));
							}
							else
							{
								list4.Add(GameObjectFactory.Factory.Blueprints[blueprint].createOne());
							}
						}
					}
					list4.ForEach(delegate(GameObject o)
					{
						o.MakeUnderstood(false);
					});
					list.Add(ConversationsAPI.chooseOneItem(list4, "Choose an item", false));
				}
				else if (motive == "trade")
				{
					list.Add(GameObjectFactory.Factory.CreateObject("Hoversled"));
					if (circumstance == "violence")
					{
						list.Add(GameObjectFactory.Factory.CreateObject("Hoversled"));
					}
				}
			}
			if (circumstance == "illness")
			{
				for (int m = 0; m < 4; m++)
				{
					list.Add(GameObject.Create(PopulationManager.RollOneFrom("DynamicObjectsTable:Tonics_NonRare", null, null).Blueprint));
				}
			}
			if (circumstance == "trade" && !flag)
			{
				List<GameObject> list5 = new List<GameObject>();
				foreach (GameObject gameObject in list)
				{
					list5.Add(gameObject.DeepCopy(false, true, null));
				}
				list.AddRange(list5);
			}
			list.ForEach(delegate(GameObject o)
			{
				o.MakeUnderstood(false);
			});
			list.Sort((GameObject o1, GameObject o2) => string.Compare(o1.DisplayNameOnlyDirect, o2.DisplayNameOnlyDirect, StringComparison.Ordinal));
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			int num5 = 0;
			stringBuilder.AppendLine("In return for your service, you receive:\n");
			foreach (GameObject gameObject2 in list)
			{
				stringBuilder.AppendLine(gameObject2.DisplayName);
				The.Player.ReceiveObject(gameObject2, false, null);
				if (circumstance == "craft" && (!(thief == "kese") || !(motive == "violence")))
				{
					if (gameObject2.HasPart<TinkerItem>() && gameObject2.GetPart<TinkerItem>().CanBuild)
					{
						list2.Add(gameObject2.Blueprint);
					}
					else
					{
						num5 = 1;
					}
				}
			}
			if (num5 > 0)
			{
				stringBuilder.AppendLine("failed energy relay x" + num5.ToString());
				stringBuilder.AppendLine("fried processing core x" + num5.ToString());
				stringBuilder.AppendLine("cracked robotics housing x" + (num5 * 2).ToString());
				for (int n = 0; n < num5; n++)
				{
					The.Player.ReceiveObject("Scrap 1", false, 0, 0, null, null, null, null);
				}
				for (int num6 = 0; num6 < num5; num6++)
				{
					The.Player.ReceiveObject("Scrap 2", false, 0, 0, null, null, null, null);
				}
				for (int num7 = 0; num7 < num5 * 2; num7++)
				{
					The.Player.ReceiveObject("Scrap 2", false, 0, 0, null, null, null, null);
				}
			}
			if (list2.Count > 0)
			{
				foreach (string text in list2)
				{
					GameObject gameObject3 = TinkerData.createDataDisk(text);
					gameObject3.MakeUnderstood(false);
					The.Player.ReceiveObject(gameObject3, false, null);
					stringBuilder.AppendLine(gameObject3.DisplayName);
				}
			}
			Popup.Show(stringBuilder.ToString(), null, "Sounds/UI/ui_notification", true, true, true, true, null);
		}

		// Token: 0x04003834 RID: 14388
		private HindrenQuestOutcome Outcome;

		// Token: 0x04003835 RID: 14389
		private string Key;
	}
}
