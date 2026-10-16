using System;
using System.Collections.Generic;
using System.Linq;
using Genkit;
using HistoryKit;
using Qud.API;
using UnityEngine;
using XRL.Core;
using XRL.Rules;
using XRL.UI;
using XRL.World.AI;
using XRL.World.AI.GoalHandlers;

namespace XRL.World.Parts
{
	// Token: 0x02001291 RID: 4753
	[Serializable]
	public class PlayerMuralController : IPart
	{
		// Token: 0x0600BC02 RID: 48130 RVA: 0x0044AF69 File Offset: 0x00449169
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BC03 RID: 48131 RVA: 0x0044AF6C File Offset: 0x0044916C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<EndTurnEvent>.ID || ID == EnteredCellEvent.ID || ID == ZoneActivatedEvent.ID;
		}

		// Token: 0x0600BC04 RID: 48132 RVA: 0x0044AF94 File Offset: 0x00449194
		public override bool HandleEvent(EndTurnEvent E)
		{
			this.turnTick++;
			if (this.turnTick == 2)
			{
				if (this.carvingStage == "done")
				{
					return true;
				}
				if (this.currentMural >= this.murals.Count || this.currentMural >= this.playerMuralEventList.Count)
				{
					if (XRLCore.Core.Game.HasIntGameState("ReshephDisguise"))
					{
						Popup.Show("Herododicus says '&WI'm finished, Moloch! Praise =name= Resheph, all who canter in this House!&Y'".Replace("=name=", XRLCore.Core.Game.PlayerName), null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
					else
					{
						Popup.Show("Herododicus says '&WI'm done!&Y'", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
					this.carvingStage = "done";
					if (XRLCore.Core.Game.HasIntGameState("ReshephDisguise"))
					{
						XRLCore.Core.Game.SetIntGameState("PlayerEngravingsDone_ReshephDisguise", 1);
					}
					else
					{
						XRLCore.Core.Game.SetIntGameState("PlayerEngravingsDone", 1);
					}
					foreach (GameObject gameObject in this.ParentObject.Physics.CurrentCell.ParentZone.GetObjectsWithPart("ReshephsCrypt"))
					{
						gameObject.RemoveIntProperty("Sealed");
						gameObject.FireEvent("SyncOpened");
						gameObject.Physics.PlayWorldSound("Sounds/Misc/sfx_ancientDoor_open", 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
					}
				}
				if (this.carvingStage == "go")
				{
					if (this.getBiographer().Physics.CurrentCell == this.getCurrentTargetCell())
					{
						this.getBiographer().Physics.PlayWorldSound("Sounds/Interact/sfx_interact_engraver", 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
						this.getBiographer().Physics.CurrentCell.GetCellFromDirection("N", true).GetObjectsWithPart("Physics")[0].Sparksplatter();
						this.updatePlayerMural(this.murals[this.currentMural], this.playerMuralEventList[this.currentMural], this.currentPanel);
						this.currentPanel++;
						if (this.currentPanel > 2)
						{
							this.currentPanel = 0;
							this.currentMural++;
						}
					}
					else if (!this.getBiographer().Brain.Goals.Items.Any<GoalHandler>((GoalHandler g) => g.GetType() == typeof(MoveTo) || g.GetType() == typeof(Step)))
					{
						this.getBiographer().Brain.Goals.Clear();
						this.getBiographer().Brain.MoveTo(this.getCurrentTargetCell(), true);
					}
				}
				this.turnTick = 0;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BC05 RID: 48133 RVA: 0x0044B2A4 File Offset: 0x004494A4
		public override bool HandleEvent(EnteredCellEvent E)
		{
			if (this.ParentObject.CurrentCell.ParentZone.IsActive() && (!this.initialized || this.murals == null))
			{
				this.initializeMurals();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BC06 RID: 48134 RVA: 0x0044B2DA File Offset: 0x004494DA
		public override bool HandleEvent(ZoneActivatedEvent E)
		{
			if (!this.initialized || this.murals == null)
			{
				this.initializeMurals();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BC07 RID: 48135 RVA: 0x0044B2F9 File Offset: 0x004494F9
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeginPlayerMural");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600BC08 RID: 48136 RVA: 0x0044B310 File Offset: 0x00449510
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeginPlayerMural")
			{
				this.getBiographer().Brain.Wanders = false;
				this.getBiographer().Brain.WandersRandomly = false;
				this.getBiographer().Brain.StartingCell = null;
				this.getBiographer().SetIntProperty("NoStay", 1, false);
				this.carvingStage = "go";
				this.currentMural = 0;
				this.currentPanel = 0;
				this.turnTick = 1;
			}
			return true;
		}

		// Token: 0x0600BC09 RID: 48137 RVA: 0x0044B398 File Offset: 0x00449598
		public int RightToLeftTopToBottom(List<Location2D> a, List<Location2D> b)
		{
			if (a[0] == b[0])
			{
				return 0;
			}
			if (a == null)
			{
				return 1;
			}
			if (b == null)
			{
				return -1;
			}
			if (a[0].X == b[0].X)
			{
				return a[0].Y.CompareTo(b[0].Y);
			}
			return b[0].X.CompareTo(a[0].X);
		}

		// Token: 0x0600BC0A RID: 48138 RVA: 0x0044B424 File Offset: 0x00449624
		public List<HistoricEvent> GetMuralEventsForPeriod(int period)
		{
			HistoricEntityList entitiesWherePropertyEquals = XRLCore.Core.Game.sultanHistory.GetEntitiesWherePropertyEquals("type", "sultan");
			for (int i = 0; i < entitiesWherePropertyEquals.entities.Count; i++)
			{
				if (int.Parse(entitiesWherePropertyEquals.entities[i].GetCurrentSnapshot().GetProperty("period", "unknown")) == period)
				{
					return (from e in entitiesWherePropertyEquals.entities[i].events
						where e.HasEventProperty("tombInscription")
						orderby e.year
						select e).ToList<HistoricEvent>().ShuffleInPlace<HistoricEvent>(null);
				}
			}
			return new List<HistoricEvent>();
		}

		// Token: 0x0600BC0B RID: 48139 RVA: 0x0044B4FE File Offset: 0x004496FE
		public void clearMurals()
		{
		}

		// Token: 0x0600BC0C RID: 48140 RVA: 0x0044B500 File Offset: 0x00449700
		public void initializeMurals()
		{
			if (this.initialized)
			{
				return;
			}
			if (this.ParentObject.Physics.CurrentCell != null)
			{
				this.murals = new List<List<Location2D>>();
				this.initialized = true;
				Zone zone = this.ParentObject.Physics.currentCell.ParentZone;
				HashSet<Cell> used = new HashSet<Cell>();
				Func<Location2D, bool> <>9__2;
				Action<Location2D> <>9__3;
				for (int i = 0; i < zone.Height; i++)
				{
					for (int j = 0; j < zone.Width - 2; j++)
					{
						Cell cell = zone.GetCell(j, i);
						if (!used.Contains(cell) && cell.HasObjectWithPart("SultanMural"))
						{
							List<Location2D> list = new List<Location2D>(3);
							list.Add(cell.Location);
							list.Add(cell.GetCellFromDirection("E", true).Location);
							list.Add(cell.GetCellFromDirection("E", true).GetCellFromDirection("E", true).Location);
							IEnumerable<Location2D> enumerable = list;
							Func<Location2D, bool> func;
							if ((func = <>9__2) == null)
							{
								func = (<>9__2 = (Location2D c) => c != null && zone.GetCell(c).HasObjectWithPart("SultanMural") && !used.Contains(zone.GetCell(c)));
							}
							if (enumerable.All<Location2D>(func))
							{
								this.murals.Add(list);
								List<Location2D> list2 = list;
								Action<Location2D> action;
								if ((action = <>9__3) == null)
								{
									action = (<>9__3 = delegate(Location2D c)
									{
										used.Add(zone.GetCell(c));
									});
								}
								list2.ForEach(action);
								this.blankMural(list);
							}
						}
					}
				}
				this.murals.Sort(new Comparison<List<Location2D>>(this.RightToLeftTopToBottom));
				BallBag<JournalAccomplishment> ballBag = new BallBag<JournalAccomplishment>(0);
				BallBag<JournalAccomplishment> ballBag2 = new BallBag<JournalAccomplishment>(0);
				List<JournalAccomplishment> original = JournalAPI.Accomplishments;
				List<JournalAccomplishment> list3 = original.Where<JournalAccomplishment>((JournalAccomplishment x) => !x.MuralText.IsNullOrEmpty()).ToList<JournalAccomplishment>();
				HashSet<string> hashSet = new HashSet<string>();
				list3.ShuffleInPlace<JournalAccomplishment>(null);
				foreach (JournalAccomplishment journalAccomplishment in list3)
				{
					int muralWeight = (int)journalAccomplishment.MuralWeight;
					if (!journalAccomplishment.AggregateWith.IsNullOrEmpty() && !hashSet.Add(journalAccomplishment.AggregateWith))
					{
						ballBag2.Add(journalAccomplishment, muralWeight);
					}
					else
					{
						ballBag.Add(journalAccomplishment, muralWeight);
					}
				}
				this.playerMuralEventList = new List<JournalAccomplishment>();
				while (ballBag.Count > 0)
				{
					if (this.playerMuralEventList.Count >= 16)
					{
						break;
					}
					this.playerMuralEventList.Add(ballBag.PickOne());
				}
				while (ballBag2.Count > 0 && this.playerMuralEventList.Count < 16)
				{
					this.playerMuralEventList.Add(ballBag2.PickOne());
				}
				this.playerMuralEventList.Sort((JournalAccomplishment a, JournalAccomplishment b) => original.IndexOf(a).CompareTo(original.IndexOf(b)));
				if (original.Count > 0)
				{
					this.playerMuralEventList.Remove(original[0]);
					this.playerMuralEventList.Insert(0, original[0]);
				}
				else
				{
					MetricsManager.LogError("no origial mural available");
				}
				JournalAccomplishment journalAccomplishment2 = new JournalAccomplishment();
				journalAccomplishment2.Category = "Dies";
				journalAccomplishment2.MuralCategory = MuralCategory.Dies;
				journalAccomplishment2.MuralText = string.Concat(new string[]
				{
					"On the ",
					Calendar.GetDay(),
					" of ",
					Calendar.GetMonth(),
					", ",
					XRLCore.Core.Game.PlayerName,
					" died peacefully and was laid to rest in the Tomb of Sultans."
				});
				journalAccomplishment2.GospelText = null;
				this.playerMuralEventList.Add(journalAccomplishment2);
				PlayerMuralGameState.Instance.SetAccomplishments(this.playerMuralEventList);
			}
			int count = this.playerMuralEventList.Count;
			int num = 9 - count / 2;
			if (num > 0)
			{
				for (int k = 0; k < num; k++)
				{
					this.murals.RemoveAt(0);
				}
			}
		}

		// Token: 0x0600BC0D RID: 48141 RVA: 0x0044B910 File Offset: 0x00449B10
		public void blankMural(List<Location2D> muralCells)
		{
			Zone parentZone = this.ParentObject.Physics.CurrentCell.ParentZone;
			if (muralCells.Count != 3)
			{
				foreach (Location2D location2D in muralCells)
				{
					GameObject firstObjectWithPart = parentZone.GetCell(location2D).GetFirstObjectWithPart("SultanMural");
					string text = "c";
					if (parentZone.GetCell(location2D).GetCellFromDirection("W", true) == null || !parentZone.GetCell(location2D).GetCellFromDirection("W", true).HasWall())
					{
						text = "l";
					}
					if (parentZone.GetCell(location2D).GetCellFromDirection("E", true) == null || !parentZone.GetCell(location2D).GetCellFromDirection("E", true).HasWall())
					{
						text = "r";
					}
					firstObjectWithPart.DisplayName = "ruined mural slate";
					firstObjectWithPart.Render.RenderString = 'ÿ'.ToString();
					firstObjectWithPart.Render.Tile = string.Concat(new string[]
					{
						"Walls/sw_mural_ruined",
						Stat.Random(1, 6).ToString(),
						"_",
						text,
						".bmp"
					});
				}
				return;
			}
			for (int i = 0; i < 3; i++)
			{
				string text2 = "c";
				if (i == 0)
				{
					text2 = "l";
				}
				if (i == 1)
				{
					text2 = "c";
				}
				if (i == 2)
				{
					text2 = "r";
				}
				GameObject firstObjectWithPart2 = parentZone.GetCell(muralCells[i]).GetFirstObjectWithPart("SultanMural");
				firstObjectWithPart2.DisplayName = "blank mural slate";
				firstObjectWithPart2.Render.RenderString = 'ÿ'.ToString();
				firstObjectWithPart2.Render.Tile = "Walls/sw_mural_blank_" + text2 + ".bmp";
			}
		}

		// Token: 0x0600BC0E RID: 48142 RVA: 0x0044BB08 File Offset: 0x00449D08
		public static void SetStateReshephDisguise()
		{
			XRLCore.Core.Game.SetIntGameState("ReshephDisguise", 1);
		}

		// Token: 0x0600BC0F RID: 48143 RVA: 0x0044BB1F File Offset: 0x00449D1F
		public static void RevealMarkOfDeath()
		{
			JournalAPI.RevealObservation("MarkOfDeathSecret", true);
		}

		// Token: 0x0600BC10 RID: 48144 RVA: 0x0044BB2C File Offset: 0x00449D2C
		public static void BeginPlayerMuralSequence()
		{
			IComponent<GameObject>.ThePlayer.CurrentZone.FireEvent("BeginPlayerMural");
		}

		// Token: 0x0600BC11 RID: 48145 RVA: 0x0044BB44 File Offset: 0x00449D44
		public void updatePlayerMural(List<Location2D> muralCells, JournalAccomplishment a, int panel)
		{
			if (muralCells.Count != 3)
			{
				Debug.LogError("mural not 3 cells long!");
				return;
			}
			string text = a.MuralCategory.ToString().ToLower();
			string text2 = The.Player.BaseDisplayName;
			string text3 = null;
			if (!string.IsNullOrEmpty(text3))
			{
				text2 = text2 + ", " + text3;
			}
			Zone currentZone = this.ParentObject.CurrentZone;
			for (int i = 0; i < 3; i++)
			{
				if (i == panel)
				{
					string text4 = "c";
					if (i == 0)
					{
						text4 = "l";
					}
					if (i == 1)
					{
						text4 = "c";
					}
					if (i == 2)
					{
						text4 = "r";
					}
					GameObject firstObjectWithPart = currentZone.GetCell(muralCells[i]).GetFirstObjectWithPart("SultanMural");
					Debug.Log(text);
					if (SultanMuralController.muralAscii.ContainsKey(text))
					{
						firstObjectWithPart.Render.RenderString = ((char)SultanMuralController.muralAscii[text][i]).ToString();
					}
					firstObjectWithPart.SetIntProperty("_wasEngraved", 1, false);
					firstObjectWithPart.DisplayName = "mural of " + text2;
					firstObjectWithPart.Render.Tile = string.Concat(new string[] { "Walls/sw_mural_", text, "_", text4, ".bmp" });
					firstObjectWithPart.Physics.Category = text;
					firstObjectWithPart.GetPart<Description>().Short = "The tomb mural depicts a significant event from the life of the sultan " + text2 + ":\n\n" + a.MuralText;
				}
			}
		}

		// Token: 0x0600BC12 RID: 48146 RVA: 0x0044BCCB File Offset: 0x00449ECB
		private GameObject getBiographer()
		{
			return GameObject.FindByBlueprint("Biographer");
		}

		// Token: 0x0600BC13 RID: 48147 RVA: 0x0044BCD7 File Offset: 0x00449ED7
		private Cell getCurrentTargetCell()
		{
			return this.ParentObject.CurrentZone.GetCell(this.murals[this.currentMural][this.currentPanel]).GetCellFromDirection("S", true);
		}

		// Token: 0x04004B56 RID: 19286
		public bool initialized;

		// Token: 0x04004B57 RID: 19287
		public bool carving;

		// Token: 0x04004B58 RID: 19288
		public string carvingStage = "waiting";

		// Token: 0x04004B59 RID: 19289
		public int currentMural;

		// Token: 0x04004B5A RID: 19290
		public int currentPanel;

		// Token: 0x04004B5B RID: 19291
		public int turnTick;

		// Token: 0x04004B5C RID: 19292
		public List<List<Location2D>> murals;

		// Token: 0x04004B5D RID: 19293
		public List<JournalAccomplishment> playerMuralEventList;
	}
}
