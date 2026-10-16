using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleLib.Console;
using Genkit;
using HistoryKit;
using Qud.API;
using XRL;
using XRL.UI.Framework;
using XRL.World;

namespace Qud.UI
{
	// Token: 0x0200046C RID: 1132
	[HasModSensitiveStaticCache]
	public class JournalLineData : PooledFrameworkDataElement<JournalLineData>
	{
		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06002B0E RID: 11022 RVA: 0x000E4920 File Offset: 0x000E2B20
		public Location2D mapTarget
		{
			get
			{
				if (this._mapTarget != null)
				{
					return this._mapTarget;
				}
				JournalMapNote journalMapNote = this.entry as JournalMapNote;
				if (journalMapNote != null)
				{
					this._mapTarget = Location2D.Get(journalMapNote.ParasangX, journalMapNote.ParasangY);
				}
				else
				{
					IBaseJournalEntry baseJournalEntry = this.entry;
					JournalVillageNote villageNote = baseJournalEntry as JournalVillageNote;
					if (villageNote != null)
					{
						if (JournalLineData.villageLocations == null)
						{
							JournalLineData.villageLocations = new Dictionary<JournalVillageNote, Location2D>();
						}
						if (!JournalLineData.villageLocations.ContainsKey(villageNote))
						{
							if (villageNote.VillageID == "Joppa")
							{
								this._mapTarget = The.ZoneManager.GetZone("JoppaWorld").GetCellsWithObject("TerrainJoppa").First<Cell>()
									.Location;
							}
							else if (villageNote.VillageID == "The Yd Freehold")
							{
								this._mapTarget = The.ZoneManager.GetZone("JoppaWorld").GetCellsWithObject("TerrainPalladiumReef 3l").First<Cell>()
									.Location;
							}
							else if (villageNote.VillageID == "Kyakukya")
							{
								this._mapTarget = The.ZoneManager.GetZone("JoppaWorld").GetCellsWithObject("TerrainKyakukya").First<Cell>()
									.Location;
							}
							else
							{
								JournalMapNote journalMapNote2 = JournalAPI.GetMapNotes((JournalMapNote note) => note.Text == HistoryAPI.GetEntityName(villageNote.VillageID)).FirstOrDefault<JournalMapNote>();
								if (journalMapNote2 != null)
								{
									this._mapTarget = Location2D.Get(journalMapNote2.ParasangX, journalMapNote2.ParasangY);
								}
								else
								{
									this._mapTarget = null;
								}
							}
							JournalLineData.villageLocations.Add(villageNote, this._mapTarget);
							return this._mapTarget;
						}
						return JournalLineData.villageLocations[villageNote];
					}
				}
				return this._mapTarget;
			}
		}

		// Token: 0x06002B0F RID: 11023 RVA: 0x000E4AF0 File Offset: 0x000E2CF0
		public JournalLineData set(IBaseJournalEntry entry, JournalStatusScreen screen, bool category = false, bool categoryExpanded = false, string categoryName = null, IRenderable renderable = null, int sultanPeriod = 0, int categoryOffset = 0)
		{
			this.screen = screen;
			this._mapTarget = null;
			this.sultanPeriod = sultanPeriod;
			this.entry = entry;
			this.category = category;
			this.categoryExpanded = categoryExpanded;
			this.categoryName = categoryName;
			this.renderable = renderable;
			string text;
			if (entry == null)
			{
				text = null;
			}
			else
			{
				string displayText = entry.GetDisplayText();
				text = ((displayText != null) ? displayText.ToLower() : null);
			}
			this.searchText = text;
			this.categoryOffset = categoryOffset;
			JournalSultanNote journalSultanNote = entry as JournalSultanNote;
			if (journalSultanNote != null)
			{
				if (!JournalLineData.sultanPeriods.ContainsKey(journalSultanNote.SultanID))
				{
					Dictionary<string, int> dictionary = JournalLineData.sultanPeriods;
					string sultanID = journalSultanNote.SultanID;
					HistoricEntitySnapshot entityCurrentSnapshot = HistoryAPI.GetEntityCurrentSnapshot(journalSultanNote.SultanID);
					dictionary.Add(sultanID, Convert.ToInt32(((entityCurrentSnapshot != null) ? entityCurrentSnapshot.GetProperty("period", "0") : null) ?? "0"));
				}
				this.sultanPeriod = JournalLineData.sultanPeriods[journalSultanNote.SultanID];
			}
			JournalRecipeNote journalRecipeNote = entry as JournalRecipeNote;
			if (journalRecipeNote != null)
			{
				this.renderable = journalRecipeNote.Recipe.GetRenderable();
			}
			return this;
		}

		// Token: 0x06002B10 RID: 11024 RVA: 0x000E4BEC File Offset: 0x000E2DEC
		public override void free()
		{
			this.screen = null;
			this.sultanPeriod = -1;
			this.categoryOffset = 0;
			this._mapTarget = null;
			this.renderable = null;
			this.category = false;
			this.categoryExpanded = false;
			this.categoryName = null;
			this.searchText = null;
			this.entry = null;
			base.free();
		}

		// Token: 0x04001726 RID: 5926
		public string searchText;

		// Token: 0x04001727 RID: 5927
		public bool category;

		// Token: 0x04001728 RID: 5928
		public bool categoryExpanded;

		// Token: 0x04001729 RID: 5929
		public string categoryName;

		// Token: 0x0400172A RID: 5930
		public IBaseJournalEntry entry;

		// Token: 0x0400172B RID: 5931
		public IRenderable renderable;

		// Token: 0x0400172C RID: 5932
		public int categoryOffset;

		// Token: 0x0400172D RID: 5933
		public Location2D _mapTarget;

		// Token: 0x0400172E RID: 5934
		public int sultanPeriod;

		// Token: 0x0400172F RID: 5935
		public JournalStatusScreen screen;

		// Token: 0x04001730 RID: 5936
		[ModSensitiveStaticCache(true)]
		public static Dictionary<string, int> sultanPeriods = new Dictionary<string, int>();

		// Token: 0x04001731 RID: 5937
		[ModSensitiveStaticCache(true)]
		private static Dictionary<JournalVillageNote, Location2D> villageLocations = new Dictionary<JournalVillageNote, Location2D>();
	}
}
