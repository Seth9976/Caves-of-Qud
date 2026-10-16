using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleLib.Console;
using Genkit;
using XRL.Language;
using XRL.Rules;
using XRL.UI;
using XRL.World.AI;
using XRL.World.AI.GoalHandlers;
using XRL.World.Capabilities;
using XRL.World.Effects;
using XRL.World.Parts;
using XRL.World.Parts.Mutation;
using XRL.World.Quests;
using XRL.World.ZoneBuilders;

namespace XRL.World.ZoneParts
{
	// Token: 0x020009C6 RID: 2502
	[Serializable]
	public class ScriptCallToArms : IZonePart
	{
		// Token: 0x06006B73 RID: 27507 RVA: 0x00229AC6 File Offset: 0x00227CC6
		public override bool SameAs(IZonePart Part)
		{
			return false;
		}

		// Token: 0x06006B74 RID: 27508 RVA: 0x00229AC9 File Offset: 0x00227CC9
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<EndTurnEvent>.ID || ID == PowerUpdatedEvent.ID;
		}

		// Token: 0x06006B75 RID: 27509 RVA: 0x00229AE8 File Offset: 0x00227CE8
		public override bool HandleEvent(EndTurnEvent E)
		{
			this.updatePower();
			string text = "Music/Imminent II";
			if (this.n == 0)
			{
				SoundManager.PlayMusic("Music/Imminent II", "music", false, 12f, 1f, null, true);
				this.partyArrivalTurnOffset = Stat.Roll(2, 8);
				this.FlagScoreables();
				this.generatyPartyLocations();
				this.ShowWarning();
				The.Game.SetStringGameState("CallToArmsStarted", "1");
				this.n++;
				GritGateScripts.OpenGritGateDoors(2);
			}
			this.n++;
			if (this.n == 250)
			{
				SoundManager.PlayMusic("Music/Attack on Grit Gate", "music", true, 12f, 1f, null, true);
				text = "Music/Attack on Grit Gate";
				this.spawnParties(0);
			}
			if (this.n == 250 + this.partyArrivalTurnOffset && this.partyTypes[0] != this.partyTypes[1])
			{
				this.spawnParties(1);
			}
			if (this.n > 250)
			{
				if (this.ParentZone.GetFirstObjectWithPropertyOrTag("TemplarWarParty") == null)
				{
					The.Game.FinishQuestStep("A Call to Arms", "Defend Grit Gate", -1, true, null);
					The.Game.ForceFinishQuest("Grave Thoughts");
					int intGameState = The.Game.GetIntGameState("CallToArmsScore", 0);
					if (intGameState <= 150)
					{
						The.Game.SetIntGameState("ACallToArms_TopScore", 1);
					}
					else if (intGameState <= 800)
					{
						The.Game.SetIntGameState("ACallToArms_MidScore", 1);
					}
					else
					{
						The.Game.SetIntGameState("ACallToArms_BottomScore", 1);
					}
					StringBuilder stringBuilder = Event.NewStringBuilder(null);
					stringBuilder.AppendLine("Destruction Score: " + The.Game.GetIntGameState("CallToArmsScore", 0).ToString());
					stringBuilder.AppendLine("  Chromelings: " + The.Game.GetIntGameState("CallToArmsChromelings", 0).ToString());
					stringBuilder.AppendLine("  Barathrumites: " + The.Game.GetIntGameState("CallToArmsBarathrumites", 0).ToString());
					if (The.Game.GetStringGameState("SlynthSettlementFaction", "") == "Barathrumites")
					{
						stringBuilder.AppendLine("  Slynth: " + The.Game.GetIntGameState("CallToArmsSlynth", 0).ToString());
					}
					stringBuilder.AppendLine("  Bookshelves: " + The.Game.GetIntGameState("CallToArmsBookshelves", 0).ToString());
					stringBuilder.AppendLine("  Books: " + The.Game.GetIntGameState("CallToArmsBooks", 0).ToString());
					stringBuilder.AppendLine("  Crops: " + The.Game.GetIntGameState("CallToArmsCrops", 0).ToString());
					stringBuilder.AppendLine("  Gadgets: " + The.Game.GetIntGameState("CallToArmsGadgets", 0).ToString());
					stringBuilder.AppendLine("  Furniture: " + The.Game.GetIntGameState("CallToArmsFurniture", 0).ToString());
					GeneralAmnestyEvent.Send();
					this.everyoneReturnHome();
					SoundManager.PlayMusic("Music/Stoic Porridge", "music", true, 12f, 1f, null, true);
					this.ParentZone.RemovePart(this);
				}
			}
			else
			{
				GameObject player = The.Player;
				if (((player != null) ? player.CurrentZone : null) == this.ParentZone)
				{
					SoundManager.PlayMusic(text, "music", true, 12f, 1f, null, true);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06006B76 RID: 27510 RVA: 0x00229E9C File Offset: 0x0022809C
		public void everyoneReturnHome()
		{
			foreach (GameObject gameObject in this.ParentZone.GetZoneFromDirection("D").GetObjectsWithTag("ReturnToGritGateAfterAttack"))
			{
				string propertyOrTag = gameObject.GetPropertyOrTag("ReturnToGritGateAfterAttack", null);
				if (!propertyOrTag.IsNullOrEmpty())
				{
					string[] array = propertyOrTag.Split(',', StringSplitOptions.None);
					int num = int.Parse(array[0]);
					int num2 = int.Parse(array[1]);
					gameObject.TeleportTo(this.ParentZone.GetCell(25, 21).getClosestPassableCell(), new int?(0), true, false, false, false, "disappear", "appear");
					The.ActionManager.AddActiveObject(gameObject);
					gameObject.Brain.Goals.Clear();
					gameObject.Brain.PushGoal(new MoveTo(this.ParentZone.GetCell(num, num2), false, false, 0, false, false, false, -1, -1, 95));
				}
			}
			foreach (GameObject gameObject2 in this.ParentZone.FindObjects((GameObject o) => o.BelongsToFaction("Barathrumites")))
			{
				gameObject2.ModIntProperty("PreventIdleBehavior", -1, true);
				if (gameObject2.HasStringProperty("GritGateDefenseOriginalLocation"))
				{
					string[] array2 = gameObject2.GetStringProperty("GritGateDefenseOriginalLocation", null).Split(',', StringSplitOptions.None);
					gameObject2.Brain.PushGoal(new MoveTo(this.ParentZone.GetCell(Convert.ToInt32(array2[0]), Convert.ToInt32(array2[1])), false, false, 0, false, false, false, -1, -1, 95));
				}
			}
			ZoneRepair zoneRepair = this.ParentZone.RequirePart<ZoneRepair>(false);
			zoneRepair.FileName = "GritGate.rpm";
			zoneRepair.TurnsPerObject = 50L;
			zoneRepair.RequiresObjectOfFaction = "Barathrumites";
			zoneRepair.CheckZoneRepair();
		}

		// Token: 0x06006B77 RID: 27511 RVA: 0x0022A0A8 File Offset: 0x002282A8
		public void updatePower()
		{
			if (this.generator == null)
			{
				GameObject gameObject = this.ParentZone.FindObject((GameObject o) => o.Blueprint == "GritGateFusionPowerStation" && o.CurrentCell.Y >= 10);
				if (gameObject != null)
				{
					this.generator = gameObject.GetPart<ElectricalPowerTransmission>();
				}
			}
			string text;
			if (this.generator == null)
			{
				text = "[{{R|!!! ERROR: POWER SYSTEMS HAVE FAILED !!!}}]";
			}
			else
			{
				int num = 4000;
				int totalDraw = this.generator.GetTotalDraw();
				int num2 = num - totalDraw;
				if (num2 < 0)
				{
					text = "[{{W|!!! WARNING: INSUFFICIENT POWER !!!}}]";
				}
				else
				{
					text = " Available power: " + ((float)num2 * 0.1f).ToString() + " amps ";
				}
			}
			GritGateAmperageImposter.display = text;
		}

		// Token: 0x06006B78 RID: 27512 RVA: 0x0022A150 File Offset: 0x00228350
		public void scan()
		{
			for (int i = 0; i < this.partyLocations.Count; i++)
			{
				Location2D location2D = this.partyLocations[i];
				if (this.partyTypes[i] == "*")
				{
					List<Cell> list = this.ParentZone.GetCell(location2D).GetAdjacentCells(3, true, true).FindAll((Cell c) => c.IsPassable(null, true));
					if (list.Count == 0)
					{
						list.Add(this.ParentZone.GetCell(location2D));
					}
					list.ShuffleInPlace<Cell>(null);
					list[0].SetExplored();
					list[0].AddObject("AlarmCircle", null, null, null, null);
				}
				else
				{
					List<Cell> list2 = this.ParentZone.GetCell(location2D).GetAdjacentCells(1, true, true).FindAll((Cell c) => c.IsPassable(null, true));
					if (list2.Count == 0)
					{
						list2.Add(this.ParentZone.GetCell(location2D));
					}
					list2.ShuffleInPlace<Cell>(null);
					list2[0].SetExplored();
					list2[0].AddObject("AlarmCircleSmall", null, null, null, null);
				}
			}
			this.ParentZone.GetCell(4, 23).SetExplored();
			this.ParentZone.GetCell(4, 23).AddObject("GritGatePowerDisplay", null, null, null, null);
			this.startBarathrumiteAlertBehaviors();
		}

		// Token: 0x06006B79 RID: 27513 RVA: 0x0022A2D0 File Offset: 0x002284D0
		public void startBarathrumiteAlertBehaviors()
		{
			string[] array = new string[] { "aggressive", "fearful", "crops", "mainframe" };
			int num = 0;
			Cell cell = null;
			foreach (Cell cell2 in this.ParentZone.GetReachableCells())
			{
				int num2 = cell2.PathDistanceTo(this.ParentZone.GetCell(this.partyLocations[0])) + cell2.PathDistanceTo(this.ParentZone.GetCell(this.partyLocations[1]));
				if (num2 > num)
				{
					cell = cell2;
					num = num2;
				}
			}
			foreach (GameObject gameObject in this.ParentZone.FindObjects((GameObject o) => o.BelongsToFaction("Barathrumites") && o.Blueprint != "Otho"))
			{
				try
				{
					string randomElement = gameObject.GetTag("GritGateDefenseBehavior", array.GetRandomElement<string>(null)).Split(',', StringSplitOptions.None).GetRandomElement<string>(null);
					gameObject.RemoveEffect<Asleep>();
					gameObject.SetStringProperty("GritGateDefenseOriginalLocation", gameObject.CurrentCell.X.ToString() + "," + gameObject.CurrentCell.Y.ToString(), false);
					if (randomElement == "aggressive")
					{
						gameObject.Brain.Goals.Clear();
						gameObject.Brain.PushGoal(new Guard()).PushChildGoal(new MoveTo(this.ParentZone.GetCell(this.partyLocations.GetRandomElement<Location2D>(null)).GetAdjacentCells(6, true, true).FindAll((Cell c) => c.IsReachable())
							.GetRandomElement<Cell>(null), false, false, 0, false, false, false, -1, -1, 95));
					}
					else if (randomElement == "fearful")
					{
						gameObject.Brain.Goals.Clear();
						gameObject.Brain.PushGoal(new Guard()).PushChildGoal(new MoveTo(cell.GetAdjacentCells(5, true, true).FindAll((Cell c) => c.IsReachable()).GetRandomElement<Cell>(null), false, false, 0, false, false, false, -1, -1, 95));
					}
					else if (randomElement == "crops")
					{
						GameObject gameObject2 = this.ParentZone.FindClosestObjectWithTag(gameObject, "LivePlant", false);
						if (gameObject2 != null)
						{
							gameObject.Brain.Goals.Clear();
							gameObject.Brain.PushGoal(new Guard()).PushChildGoal(new MoveTo(gameObject2.CurrentCell.GetAdjacentCells(3, true, true).FindAll((Cell c) => c.IsReachable()).GetRandomElement<Cell>(null), false, false, 0, false, false, false, -1, -1, 95));
						}
					}
					else if (randomElement == "mainframe")
					{
						GameObject gameObject3 = this.ParentZone.FindClosestObjectWithTag(gameObject, "Mainframe", false);
						if (gameObject3 != null)
						{
							gameObject.Brain.Goals.Clear();
							gameObject.Brain.PushGoal(new Guard()).PushChildGoal(new MoveTo(gameObject3.CurrentCell.GetAdjacentCells(3, true, true).FindAll((Cell c) => c.IsReachable()).GetRandomElement<Cell>(null), false, false, 0, false, false, false, -1, -1, 95));
						}
					}
				}
				catch (Exception ex)
				{
					MetricsManager.LogError("A Call to Arms behavior setup for " + gameObject.DebugName, ex);
				}
			}
		}

		// Token: 0x06006B7A RID: 27514 RVA: 0x0022A6FC File Offset: 0x002288FC
		public void generatyPartyLocations()
		{
			for (int i = 0; i < 2; i++)
			{
				string blueprint = PopulationManager.RollOneFrom("Grit Gate Attack Location", null, null).Blueprint;
				Location2D location2D = null;
				if (blueprint == "F")
				{
					location2D = Location2D.Get(40, 21);
				}
				else if (blueprint == "E")
				{
					location2D = Location2D.Get(75, 4);
				}
				else if (blueprint == "W")
				{
					location2D = Location2D.Get(6, 7);
				}
				else if (blueprint == "*")
				{
					List<Cell> list = (from c in this.ParentZone.GetEmptyReachableCells()
						where c.X > 4 && c.X < 76 && c.Y > 4 && c.Y < 20
						select c).ToList<Cell>();
					if (list.Count == 0)
					{
						list = this.ParentZone.GetEmptyCells(null, null);
					}
					if (list.Count == 0)
					{
						list = this.ParentZone.GetCells((Cell c) => !c.HasWall());
					}
					if (list.Count == 0)
					{
						list = this.ParentZone.GetCells();
					}
					location2D = list.GetRandomElement<Cell>(null).Location;
				}
				this.partyTypes.Add(blueprint);
				this.partyLocations.Add(location2D);
			}
		}

		// Token: 0x06006B7B RID: 27515 RVA: 0x0022A848 File Offset: 0x00228A48
		public void spawnParties(int p)
		{
			if (!The.Game.HasQuest("A Call to Arms"))
			{
				The.Game.StartQuest("A Call to Arms", null, null, null);
			}
			AutoAct.Interrupt(null, null, null, false);
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			this.ParentZone.FindObjects("AlarmCircle").ForEach(delegate(GameObject o)
			{
				o.Obliterate(null, false, null);
			});
			this.ParentZone.FindObjects("AlarmCircleSmall").ForEach(delegate(GameObject o)
			{
				o.Obliterate(null, false, null);
			});
			this.ParentZone.FindObjects("GritGatePowerDisplay").ForEach(delegate(GameObject o)
			{
				o.Obliterate(null, false, null);
			});
			string text = this.partyTypes[p];
			Location2D location2D = this.partyLocations[p];
			List<PopulationResult> list = null;
			if (p == 0)
			{
				list = PopulationManager.Generate("Templar War Party Major", null, null, null, null, null);
			}
			if (p == 1)
			{
				list = PopulationManager.Generate("Templar War Party Minor", null, null, null, null, null);
			}
			int partycount = 0;
			list.ForEach(delegate(PopulationResult r)
			{
				partycount += r.Number;
			});
			List<Cell> spawnCells = null;
			for (int i = 3; i < 80; i++)
			{
				spawnCells = this.ParentZone.GetCell(location2D).GetEmptyAdjacentCells(1, i);
				if (spawnCells.Count >= partycount)
				{
					break;
				}
			}
			if (text == "*")
			{
				Zone zoneFromDirection = this.ParentZone.GetZoneFromDirection("U");
				for (int j = location2D.X - 3; j < location2D.X + 3; j++)
				{
					for (int k = location2D.Y - 3; k < location2D.Y + 3; k++)
					{
						Cell cell = zoneFromDirection.GetCell(j, k);
						if (cell != null)
						{
							cell.ClearWalls();
						}
					}
				}
				for (int l = location2D.X - 3; l < location2D.X + 3; l++)
				{
					for (int m2 = location2D.Y - 3; m2 < location2D.Y + 3; m2++)
					{
						Cell cell2 = this.ParentZone.GetCell(l, m2);
						if (25.in100())
						{
							cell2.AddObject(PopulationManager.RollOneFrom("Rocky Debris", null, null).Blueprint, null, null, null, null);
						}
					}
				}
				Popup.Show("{{r|The ceiling collapses and rocks come pouring in!}}", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				zoneFromDirection.GetCell(location2D).AddObject("OpenShaft", null, null, null, null);
				new ForceConnectionsPlus().BuildZone(zoneFromDirection);
			}
			if (text == "F" && !flag)
			{
				ElectromagneticPulse.EMP(this.ParentZone.GetCell(location2D), 4, 30, true, 1);
				foreach (Cell cell3 in this.ParentZone.GetCells())
				{
					foreach (GameObject gameObject in cell3.GetObjectsInCell())
					{
						if (gameObject.Blueprint == "Forcefield_GritGateRank1")
						{
							gameObject.Destroy(null, false, false, null);
						}
					}
				}
			}
			if (text == "E" && !flag3)
			{
				flag3 = true;
				ElectromagneticPulse.EMP(this.ParentZone.GetCell(location2D), 4, 30, true, 1);
				foreach (Cell cell4 in this.ParentZone.GetCells())
				{
					foreach (GameObject gameObject2 in cell4.GetObjectsInCell())
					{
						if (gameObject2.Blueprint == "Forcefield_GritGate")
						{
							gameObject2.Destroy(null, false, false, null);
						}
					}
				}
			}
			if (text == "W" && !flag2)
			{
				flag2 = true;
				for (int n = 4; n < 8; n++)
				{
					for (int num = 0; num < 7; num++)
					{
						Cell cell5 = this.ParentZone.GetCell(n, num);
						if (cell5 != null)
						{
							cell5.ClearWalls();
							if (20.in100())
							{
								cell5.AddObject(PopulationManager.RollOneFrom("Rocky Debris", null, null).Blueprint, null, null, null, null);
							}
						}
					}
				}
				Zone zoneFromDirection2 = this.ParentZone.GetZoneFromDirection("N");
				for (int num2 = 4; num2 < 8; num2++)
				{
					for (int num3 = this.ParentZone.Height - 4; num3 < this.ParentZone.Height; num3++)
					{
						Cell cell6 = zoneFromDirection2.GetCell(num2, num3);
						if (cell6 != null)
						{
							cell6.ClearWalls();
						}
					}
				}
				new ForceConnectionsPlus().BuildZone(zoneFromDirection2);
				location2D = Location2D.Get(6, 7);
			}
			list.ForEach(delegate(PopulationResult m)
			{
				for (int num4 = 0; num4 < m.Number; num4++)
				{
					spawnCells.RemoveRandomElement<Cell>(null).AddObject(m.Blueprint, null, null, null, null).AddAsActiveObject()
						.SetIntProperty("TemplarWarParty", 1, false)
						.SetIntProperty("AllowIdleBehavior", 1, false)
						.Brain.Wanders = false;
				}
			});
			foreach (Cell cell7 in this.ParentZone.GetCells())
			{
				foreach (GameObject gameObject3 in cell7.GetObjectsInCell())
				{
					if (gameObject3.Blueprint == "GritGateDoor1")
					{
						gameObject3.Destroy(null, false, false, null);
					}
				}
			}
			if (p == 0 && this.partyTypes[0] == this.partyTypes[1])
			{
				this.spawnParties(1);
				return;
			}
			ScreenBuffer.GetScrapBuffer1(true).Shake(1500, 25, Popup._TextConsole);
		}

		// Token: 0x06006B7C RID: 27516 RVA: 0x0022AE74 File Offset: 0x00229074
		public void FlagScoreables()
		{
			foreach (GameObject gameObject in this.ParentZone.IterateObjects())
			{
				this.FlagScoreable(gameObject);
			}
		}

		// Token: 0x06006B7D RID: 27517 RVA: 0x0022AEB0 File Offset: 0x002290B0
		public void FlagScoreable(GameObject o)
		{
			GameObjectBlueprint blueprint = o.GetBlueprint(true);
			if ((blueprint.InheritsFrom("BarathrumiteRobot") || blueprint.HasTagOrProperty("BarathrumiteRobot")) && !o.HasTag("CallToArmsBarathrumite"))
			{
				o.AddPart<CallToArmsScore>(new CallToArmsScore(20, "CallToArmsChromelings", false), true, false);
				return;
			}
			if ((o.IsCombat && o.BelongsToFaction("Barathrumites")) || o.HasTag("CallToArmsBarathrumite"))
			{
				o.AddPart<CallToArmsScore>(new CallToArmsScore(200, "CallToArmsBarathrumites", true), true, false);
				return;
			}
			if (blueprint.InheritsFrom("BaseSlynth"))
			{
				o.AddPart<CallToArmsScore>(new CallToArmsScore(200, "CallToArmsSlynth", true), true, false);
				return;
			}
			if (blueprint.InheritsFrom("Bookshelf"))
			{
				o.AddPart<CallToArmsScore>(new CallToArmsScore(20, "CallToArmsBookshelves", false), true, false);
				o.AddPart<BurnMe>(new BurnMe("Templar"), true, false);
				Inventory inventory = o.Inventory;
				if (inventory == null)
				{
					return;
				}
				List<GameObject> objects = inventory.Objects;
				if (objects == null)
				{
					return;
				}
				objects.ForEach(new Action<GameObject>(this.FlagScoreable));
				return;
			}
			else
			{
				if (blueprint.InheritsFrom("Book"))
				{
					o.AddPart<CallToArmsScore>(new CallToArmsScore(20, "CallToArmsBooks", false), true, false);
					o.AddPart<BurnMe>(new BurnMe("Templar"), true, false);
					return;
				}
				if (o.HasTag("CallToArmsCrop") && !o.IsCombat && o.CurrentCell != null)
				{
					o.AddPart<CallToArmsScore>(new CallToArmsScore(20, "CallToArmsCrops", false), true, false);
					o.AddPart<BurnMe>(new BurnMe("Templar"), true, false);
					return;
				}
				if (o.HasTag("LivePlant") && !o.IsCombat && o.CurrentCell != null)
				{
					o.AddPart<BurnMe>(new BurnMe("Templar"), true, false);
					return;
				}
				if (blueprint.InheritsFrom("Furniture"))
				{
					if (blueprint.TechTier >= 2 || o.HasTag("Gadget"))
					{
						o.AddPart<CallToArmsScore>(new CallToArmsScore(20, "CallToArmsGadgets", false), true, false);
					}
					else
					{
						o.AddPart<CallToArmsScore>(new CallToArmsScore(20, "CallToArmsFurniture", false), true, false);
					}
					o.AddPart<DestroyMe>(new DestroyMe("Templar"), true, false);
					Inventory inventory2 = o.Inventory;
					if (inventory2 == null)
					{
						return;
					}
					List<GameObject> objects2 = inventory2.Objects;
					if (objects2 == null)
					{
						return;
					}
					objects2.ForEach(new Action<GameObject>(this.FlagScoreable));
				}
				return;
			}
		}

		// Token: 0x06006B7E RID: 27518 RVA: 0x0022B0FC File Offset: 0x002292FC
		public void ShowWarning()
		{
			ScreenBuffer.GetScrapBuffer1(true).Shake(1500, 25, Popup._TextConsole);
			Popup.Show("The whole compound rumbles around you.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			Popup.Show("The walls creak, loose objects skid about, and dust is stirred up in iridescent clouds.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			Popup.Show("Otho yells, '{{W|" + The.Game.PlayerName + "! Come back here!}}'", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			AutoAct.Interrupt(null, null, null, false);
			foreach (GameObject gameObject in this.ParentZone.FindObjects((GameObject o) => o.BelongsToFaction("Barathrumites")))
			{
				Brain brain = gameObject.Brain;
				if (brain != null)
				{
					CleanStack<GoalHandler> goals = brain.Goals;
					if (goals != null)
					{
						goals.Clear();
					}
				}
				gameObject.ModIntProperty("PreventIdleBehavior", 1, true);
			}
			The.Game.SetIntGameState("GritGatePower", 1000);
		}

		// Token: 0x06006B7F RID: 27519 RVA: 0x0022B21C File Offset: 0x0022941C
		public static string GenerateResultConversation()
		{
			string text = "=name=, I've assessed the losses we suffered in the attack.\n\n";
			string text2 = "friend";
			string text3 = "deaths";
			string text4 = "";
			if (The.Game.HasStringGameState("CallToArmsPersonsKilled"))
			{
				List<string> list = The.Game.GetStringGameState("CallToArmsPersonsKilled", "").Split(',', StringSplitOptions.None).ToList<string>();
				if (list.Count == 1)
				{
					text2 = "friend";
					text3 = "death";
					text4 = list[0];
				}
				else
				{
					HashSet<string> hashSet = new HashSet<string>();
					text2 = "friends";
					for (int i = 0; i < list.Count; i++)
					{
						if (list[i] != null && !hashSet.Contains(list[i]))
						{
							hashSet.Add(list[i]);
							if (i > 0)
							{
								if (i < list.Count - 1)
								{
									text4 += ", ";
								}
								else
								{
									text4 += " and ";
								}
							}
							text4 += list[i];
						}
					}
				}
			}
			if (text4 != "")
			{
				text = string.Concat(new string[] { text, "First and foremost, let us lament the ", text3, " of our ", text2, ": ", text4, ". The loss to us is incalculable.\n\n" });
				text += "Beyond that, ";
			}
			int intGameState = The.Game.GetIntGameState("CallToArmsChromelings", 0);
			int intGameState2 = The.Game.GetIntGameState("CallToArmsBooks", 0);
			int intGameState3 = The.Game.GetIntGameState("CallToArmsBookshelves", 0);
			int intGameState4 = The.Game.GetIntGameState("CallToArmsCrops", 0);
			int intGameState5 = The.Game.GetIntGameState("CallToArmsGadgets", 0);
			int intGameState6 = The.Game.GetIntGameState("CallToArmsFurniture", 0);
			string text5 = string.Concat(new string[]
			{
				Grammar.CardinalNo(intGameState),
				" ",
				(intGameState == 1) ? "robot" : "robots",
				" perished, ",
				Grammar.CardinalNo(intGameState2),
				" ",
				(intGameState2 == 1) ? "book was" : "books were",
				" burned, ",
				Grammar.CardinalNo(intGameState3),
				" ",
				(intGameState3 == 1) ? "bookshelf was" : "bookshelves were",
				" destroyed, ",
				Grammar.CardinalNo(intGameState4),
				" ",
				(intGameState4 == 1) ? "crop was" : "crops were",
				" ruined, ",
				Grammar.CardinalNo(intGameState5),
				" ",
				(intGameState5 == 1) ? "gadget was" : "gadgets were",
				" shattered, and ",
				Grammar.CardinalNo(intGameState6),
				" ",
				(intGameState6 == 1) ? "piece" : "pieces",
				" of furniture ",
				(intGameState6 == 1) ? "was" : "were",
				" destroyed."
			});
			if (text4 == "")
			{
				text5 = Grammar.InitCap(text5);
			}
			return text + text5;
		}

		// Token: 0x0400312D RID: 12589
		public int n;

		// Token: 0x0400312E RID: 12590
		public const int TOPSCORE = 150;

		// Token: 0x0400312F RID: 12591
		public const int MIDSCORE = 800;

		// Token: 0x04003130 RID: 12592
		public const int ATTACK_TIMER = 250;

		// Token: 0x04003131 RID: 12593
		[NonSerialized]
		private ElectricalPowerTransmission generator;

		// Token: 0x04003132 RID: 12594
		public List<string> partyTypes = new List<string>();

		// Token: 0x04003133 RID: 12595
		public List<Location2D> partyLocations = new List<Location2D>();

		// Token: 0x04003134 RID: 12596
		private int partyArrivalTurnOffset = 4;
	}
}
