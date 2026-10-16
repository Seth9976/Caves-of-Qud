using System;
using System.Collections.Generic;
using System.Linq;
using HistoryKit;
using Qud.API;
using UnityEngine;
using XRL.Core;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x02001290 RID: 4752
	[Serializable]
	public class SultanMuralController : IPart
	{
		// Token: 0x0600BBF6 RID: 48118 RVA: 0x0044A174 File Offset: 0x00448374
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BBF7 RID: 48119 RVA: 0x0044A177 File Offset: 0x00448377
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EnteredCellEvent.ID || ID == ZoneActivatedEvent.ID;
		}

		// Token: 0x0600BBF8 RID: 48120 RVA: 0x0044A195 File Offset: 0x00448395
		public override bool HandleEvent(EnteredCellEvent E)
		{
			if (this.ParentObject.CurrentZone.IsActive() && !this.initialized)
			{
				this.initializeMurals();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BBF9 RID: 48121 RVA: 0x0044A1BE File Offset: 0x004483BE
		public override bool HandleEvent(ZoneActivatedEvent E)
		{
			if (!this.initialized)
			{
				this.initializeMurals();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BBFA RID: 48122 RVA: 0x0044A1D8 File Offset: 0x004483D8
		public int LeftToRightTopToBottom(List<Cell> a, List<Cell> b)
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
			if (a[0].Location == b[0].Location)
			{
				return 0;
			}
			if (a[0].X == b[0].X)
			{
				return a[0].Y.CompareTo(b[0].Y);
			}
			return a[0].X.CompareTo(b[0].X);
		}

		// Token: 0x0600BBFB RID: 48123 RVA: 0x0044A278 File Offset: 0x00448478
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

		// Token: 0x0600BBFC RID: 48124 RVA: 0x0044A354 File Offset: 0x00448554
		public void initializeMurals()
		{
			if (this.ParentObject.Physics.CurrentCell != null)
			{
				global::System.Random seededRandom = this.ParentObject.GetSeededRandom("SultanMural");
				this.initialized = true;
				List<List<Cell>> list = new List<List<Cell>>();
				Zone parentZone = this.ParentObject.Physics.currentCell.ParentZone;
				string[] supportedPeriods = null;
				if (!string.IsNullOrEmpty(this.PeriodFilter))
				{
					supportedPeriods = this.PeriodFilter.Split(',', StringSplitOptions.None);
				}
				HashSet<Cell> used = new HashSet<Cell>();
				Func<Cell, bool> <>9__0;
				Action<Cell> <>9__1;
				for (int i = 0; i < parentZone.Height; i++)
				{
					for (int j = 0; j < parentZone.Width; j++)
					{
						Cell cell = parentZone.GetCell(j, i);
						if (!used.Contains(cell) && cell.HasObjectWithPart("SultanMural") && !cell.HasObjectWithPropertyOrTag("_wasEngraved"))
						{
							List<Cell> list2 = new List<Cell>(3);
							list2.Add(cell);
							list2.Add(cell.GetCellFromDirection("E", true));
							if (j < parentZone.Width - 1)
							{
								list2.Add(cell.GetCellFromDirection("E", true).GetCellFromDirection("E", true));
							}
							IEnumerable<Cell> enumerable = list2;
							Func<Cell, bool> func;
							if ((func = <>9__0) == null)
							{
								func = (<>9__0 = (Cell c) => c != null && c.HasObjectWithPart("SultanMural") && !used.Contains(c) && !c.HasObjectWithPropertyOrTag("_wasEngraved") && (supportedPeriods == null || !c.GetFirstObjectWithPart("SultanMural").HasTag("SultanatePeriod") || supportedPeriods.Contains(c.GetFirstObjectWithPart("SultanMural").GetTag("SultanatePeriod", null))));
							}
							if (enumerable.All<Cell>(func))
							{
								list.Add(list2);
								List<Cell> list3 = list2;
								Action<Cell> action;
								if ((action = <>9__1) == null)
								{
									action = (<>9__1 = delegate(Cell c)
									{
										used.Add(c);
									});
								}
								list3.ForEach(action);
							}
							else
							{
								this.blankMural(list2);
							}
						}
					}
				}
				foreach (List<Cell> list4 in list)
				{
					this.blankMural(list4);
				}
				if (this.Layout.StartsWith("chronological:"))
				{
					list.Sort(new Comparison<List<Cell>>(this.LeftToRightTopToBottom));
					List<HistoricEvent> list5 = new List<HistoricEvent>();
					int num = Convert.ToInt32(this.Layout.Split(':', StringSplitOptions.None)[1]);
					list5 = this.GetMuralEventsForPeriod(num);
					if (this.Reversed)
					{
						list5.Reverse();
					}
					int num2 = 0;
					if (list5.Count < list.Count)
					{
						num2 = (list.Count - list5.Count) / 2;
					}
					int num3 = 0;
					while (num3 < list5.Count && num3 < list.Count)
					{
						List<Cell> list6 = list[num3 + num2];
						HistoricEvent historicEvent = list5[num3];
						if (seededRandom.Next(1, 101) <= this.RuinedChance)
						{
							SultanMuralController.ruinMural(list6, historicEvent);
						}
						else
						{
							this.updateHistoricMural(list6, historicEvent);
						}
						num3++;
					}
				}
				if (this.Layout.StartsWith("random:"))
				{
					List<HistoricEvent> list7 = new List<HistoricEvent>();
					int num4 = Convert.ToInt32(this.Layout.Split(':', StringSplitOptions.None)[1]);
					list7 = this.GetMuralEventsForPeriod(num4);
					list.ShuffleInPlace<List<Cell>>(seededRandom);
					list7.ShuffleInPlace<HistoricEvent>(seededRandom);
					int num5 = 0;
					while (num5 < list7.Count && num5 < list.Count)
					{
						List<Cell> list8 = list[num5];
						HistoricEvent historicEvent2 = list7[num5];
						if (seededRandom.Next(1, 101) <= this.RuinedChance)
						{
							SultanMuralController.ruinMural(list8, historicEvent2);
						}
						else
						{
							this.updateHistoricMural(list8, historicEvent2);
						}
						num5++;
					}
				}
				this.ParentObject.Destroy(null, false, false, null);
			}
		}

		// Token: 0x0600BBFD RID: 48125 RVA: 0x0044A6EC File Offset: 0x004488EC
		public static void ruinMural(List<Cell> muralCells, HistoricEvent ev)
		{
			if (muralCells.Count != 3)
			{
				Debug.LogError("mural not 3 cells long!");
				return;
			}
			string eventProperty = ev.GetEventProperty("tombInscriptionCategory", null);
			HistoricEntitySnapshot currentSnapshot = ev.entity.GetCurrentSnapshot();
			string text = currentSnapshot.GetProperty("name", "<unknown name>");
			string randomElementFromListProperty = currentSnapshot.GetRandomElementFromListProperty("cognomen", null, Stat.Rand);
			if (randomElementFromListProperty != null)
			{
				text = text + ", " + randomElementFromListProperty;
			}
			int num = Stat.Random(1, 6);
			for (int i = 0; i < 3; i++)
			{
				string text2 = "c";
				int num2 = 64;
				if (i == 0)
				{
					text2 = "l";
					num2 = 213;
				}
				if (i == 1)
				{
					text2 = "c";
					num2 = 209;
				}
				if (i == 2)
				{
					text2 = "r";
					num2 = 207;
				}
				GameObject firstObjectWithPart = muralCells[i].GetFirstObjectWithPart("SultanMural");
				firstObjectWithPart.SetIntProperty("_wasEngraved", 1, false);
				firstObjectWithPart.DisplayName = "ruined mural of " + text;
				firstObjectWithPart.Render.RenderString = ((char)num2).ToString();
				firstObjectWithPart.Render.Tile = string.Concat(new string[]
				{
					"Walls/sw_mural_ruined",
					num.ToString(),
					"_",
					text2,
					".bmp"
				});
				firstObjectWithPart.Physics.Category = eventProperty;
				firstObjectWithPart.GetPart<Description>().Short = "The tomb mural depicts a significant event from the life of the ancient sultan " + text + ", but it's no longer legible.";
			}
		}

		// Token: 0x0600BBFE RID: 48126 RVA: 0x0044A86C File Offset: 0x00448A6C
		public void blankMural(List<Cell> muralCells)
		{
			if (muralCells.Count == 3)
			{
				if (!muralCells.Any<Cell>((Cell C) => C == null))
				{
					for (int i = 0; i < 3; i++)
					{
						string text = "c";
						if (i == 0)
						{
							text = "l";
						}
						if (i == 1)
						{
							text = "c";
						}
						if (i == 2)
						{
							text = "r";
						}
						GameObject firstObjectWithPart = muralCells[i].GetFirstObjectWithPart("SultanMural");
						if (firstObjectWithPart != null)
						{
							firstObjectWithPart.DisplayName = "blank mural slate";
							firstObjectWithPart.Render.RenderString = 'ÿ'.ToString();
							firstObjectWithPart.Render.Tile = "Walls/sw_mural_blank_" + text + ".bmp";
						}
					}
					return;
				}
			}
			foreach (Cell cell in muralCells)
			{
				if (cell != null)
				{
					GameObject firstObjectWithPart2 = cell.GetFirstObjectWithPart("SultanMural");
					if (firstObjectWithPart2 != null)
					{
						string text2 = "c";
						if (cell.GetCellFromDirection("W", true) == null || !cell.GetCellFromDirection("W", true).HasWall())
						{
							text2 = "l";
						}
						if (cell.GetCellFromDirection("E", true) == null || !cell.GetCellFromDirection("E", true).HasWall())
						{
							text2 = "r";
						}
						firstObjectWithPart2.DisplayName = "ruined mural slate";
						firstObjectWithPart2.Render.RenderString = 'ÿ'.ToString();
						firstObjectWithPart2.Render.Tile = string.Concat(new string[]
						{
							"Walls/sw_mural_ruined",
							Stat.Random(1, 6).ToString(),
							"_",
							text2,
							".bmp"
						});
					}
				}
			}
		}

		// Token: 0x0600BBFF RID: 48127 RVA: 0x0044AA60 File Offset: 0x00448C60
		public void updateHistoricMural(List<Cell> muralCells, HistoricEvent ev)
		{
			if (muralCells.Count != 3)
			{
				Debug.LogError("mural not 3 cells long!");
				return;
			}
			string text = ev.GetEventProperty("tombInscriptionCategory", null).ToLower();
			HistoricEntitySnapshot currentSnapshot = ev.entity.GetCurrentSnapshot();
			string text2 = currentSnapshot.GetProperty("name", "<unknown name>");
			string randomElementFromListProperty = currentSnapshot.GetRandomElementFromListProperty("cognomen", null, Stat.Rand);
			if (randomElementFromListProperty != null)
			{
				text2 = text2 + ", " + randomElementFromListProperty;
			}
			Func<JournalSultanNote, bool> <>9__0;
			for (int i = 0; i < 3; i++)
			{
				string text3 = "c";
				if (i == 0)
				{
					text3 = "l";
				}
				if (i == 1)
				{
					text3 = "c";
				}
				if (i == 2)
				{
					text3 = "r";
				}
				GameObject firstObjectWithPart = muralCells[i].GetFirstObjectWithPart("SultanMural");
				firstObjectWithPart.SetIntProperty("_wasEngraved", 1, false);
				firstObjectWithPart.DisplayName = "mural of " + text2;
				if (SultanMuralController.muralAscii.ContainsKey(text))
				{
					firstObjectWithPart.Render.RenderString = ((char)SultanMuralController.muralAscii[text][i]).ToString();
				}
				firstObjectWithPart.Render.Tile = string.Concat(new string[] { "Walls/sw_mural_", text, "_", text3, ".bmp" });
				firstObjectWithPart.Physics.Category = text;
				firstObjectWithPart.GetPart<Description>().Short = "The tomb mural depicts a significant event from the life of the ancient sultan " + text2 + ":\n\n" + ev.GetEventProperty("tombInscription", null);
				IEnumerable<JournalSultanNote> sultanNotes = JournalAPI.SultanNotes;
				Func<JournalSultanNote, bool> func;
				if ((func = <>9__0) == null)
				{
					func = (<>9__0 = (JournalSultanNote e) => e.EventID == ev.id && e.Has("sultanTombPropaganda"));
				}
				JournalSultanNote journalSultanNote = sultanNotes.Where<JournalSultanNote>(func).First<JournalSultanNote>();
				if (journalSultanNote != null)
				{
					firstObjectWithPart.GetPart<SultanMural>().secretID = journalSultanNote.ID;
					firstObjectWithPart.GetPart<SultanMural>().secretEvent = ev;
				}
			}
		}

		// Token: 0x04004B50 RID: 19280
		public string PeriodFilter;

		// Token: 0x04004B51 RID: 19281
		public string Layout = "chronological:1";

		// Token: 0x04004B52 RID: 19282
		public int RuinedChance = 15;

		// Token: 0x04004B53 RID: 19283
		public bool initialized;

		// Token: 0x04004B54 RID: 19284
		public bool Reversed;

		// Token: 0x04004B55 RID: 19285
		public static Dictionary<string, int[]> muralAscii = new Dictionary<string, int[]>
		{
			{
				"isborn",
				new int[] { 37, 26, 64 }
			},
			{
				"hasinspiringexperience",
				new int[] { 64, 13, 14 }
			},
			{
				"treats",
				new int[] { 134, 132, 134 }
			},
			{
				"createssomething",
				new int[] { 227, 64, 15 }
			},
			{
				"commitsfolly",
				new int[] { 168, 64, 63 }
			},
			{
				"weirdthinghappens",
				new int[] { 64, 21, 21 }
			},
			{
				"endureshardship",
				new int[] { 16, 64, 17 }
			},
			{
				"bodyexperiencebad",
				new int[] { 64, 170, 31 }
			},
			{
				"bodyexperiencegood",
				new int[] { 64, 170, 30 }
			},
			{
				"bodyexperienceneutral",
				new int[] { 64, 170, 22 }
			},
			{
				"trysts",
				new int[] { 145, 201, 185 }
			},
			{
				"visitslocation",
				new int[] { 64, 157, 157 }
			},
			{
				"doesbureaucracy",
				new int[] { 64, 227, 172 }
			},
			{
				"learnssecret",
				new int[] { 63, 64, 19 }
			},
			{
				"findsobject",
				new int[] { 64, 15, 157 }
			},
			{
				"doessomethingrad",
				new int[] { 19, 64, 19 }
			},
			{
				"doessomethinghumble",
				new int[] { 64, 250, 210 }
			},
			{
				"doessomethingdestructive",
				new int[] { 64, 235, 127 }
			},
			{
				"becomesloved",
				new int[] { 3, 64, 3 }
			},
			{
				"slays",
				new int[] { 64, 41, 37 }
			},
			{
				"resists",
				new int[] { 250, 64, 251 }
			},
			{
				"appeasesbaetyl",
				new int[] { 64, 175, 4 }
			},
			{
				"dies",
				new int[] { 64, 26, 37 }
			},
			{
				"crownedsultan",
				new int[] { 134, 15, 64 }
			},
			{
				"wieldsiteminbattle",
				new int[] { 91, 64, 41 }
			},
			{
				"meetswithcounselors",
				new int[] { 166, 167, 64 }
			}
		};
	}
}
