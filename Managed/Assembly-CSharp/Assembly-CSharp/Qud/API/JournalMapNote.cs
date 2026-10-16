using System;
using System.CodeDom.Compiler;
using Genkit;
using Occult.Engine.CodeGeneration;
using XRL;
using XRL.Language;
using XRL.UI;
using XRL.World;

namespace Qud.API
{
	// Token: 0x020003FA RID: 1018
	[GenerateSerializationPartial]
	[Serializable]
	public class JournalMapNote : IBaseJournalEntry
	{
		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06002603 RID: 9731 RVA: 0x000C46FA File Offset: 0x000C28FA
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002604 RID: 9732 RVA: 0x000C4700 File Offset: 0x000C2900
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Write(SerializationWriter Writer)
		{
			Writer.WriteOptimized(this.WorldID);
			Writer.WriteOptimized(this.ParasangX);
			Writer.WriteOptimized(this.ParasangY);
			Writer.WriteOptimized(this.ZoneX);
			Writer.WriteOptimized(this.ZoneY);
			Writer.WriteOptimized(this.ZoneZ);
			Writer.WriteOptimized(this.Category);
			Writer.WriteOptimized(this.Time);
			Writer.Write(this.Tracked);
			Writer.WriteOptimized(this.ID);
			Writer.WriteOptimized(this.History);
			Writer.WriteOptimized(this.Text);
			Writer.WriteOptimized(this.LearnedFrom);
			Writer.WriteOptimized(this.Weight);
			Writer.Write(this.Revealed);
			Writer.Write(this.Tradable);
			Writer.Write(this.Attributes);
		}

		// Token: 0x06002605 RID: 9733 RVA: 0x000C47DC File Offset: 0x000C29DC
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Read(SerializationReader Reader)
		{
			this.WorldID = Reader.ReadOptimizedString();
			this.ParasangX = Reader.ReadOptimizedInt32();
			this.ParasangY = Reader.ReadOptimizedInt32();
			this.ZoneX = Reader.ReadOptimizedInt32();
			this.ZoneY = Reader.ReadOptimizedInt32();
			this.ZoneZ = Reader.ReadOptimizedInt32();
			this.Category = Reader.ReadOptimizedString();
			this.Time = Reader.ReadOptimizedInt64();
			this.Tracked = Reader.ReadBoolean();
			this.ID = Reader.ReadOptimizedString();
			this.History = Reader.ReadOptimizedString();
			this.Text = Reader.ReadOptimizedString();
			this.LearnedFrom = Reader.ReadOptimizedString();
			this.Weight = Reader.ReadOptimizedInt32();
			this.Revealed = Reader.ReadBoolean();
			this.Tradable = Reader.ReadBoolean();
			this.Attributes = Reader.ReadList<string>();
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06002606 RID: 9734 RVA: 0x000C48B8 File Offset: 0x000C2AB8
		// (set) Token: 0x06002607 RID: 9735 RVA: 0x000C495F File Offset: 0x000C2B5F
		public string ZoneID
		{
			get
			{
				if (this._ZoneID == null)
				{
					IBaseJournalEntry.SB.Clear().Append(this.WorldID ?? "NULL").Append('.')
						.Append(this.ParasangX)
						.Append('.')
						.Append(this.ParasangY)
						.Append('.')
						.Append(this.ZoneX)
						.Append('.')
						.Append(this.ZoneY)
						.Append('.')
						.Append(this.ZoneZ);
					this._ZoneID = IBaseJournalEntry.SB.ToString();
				}
				return this._ZoneID;
			}
			set
			{
				this._ZoneID = value;
				global::XRL.World.ZoneID.Parse(value, out this.WorldID, out this.ParasangX, out this.ParasangY, out this.ZoneX, out this.ZoneY, out this.ZoneZ);
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06002608 RID: 9736 RVA: 0x000C4993 File Offset: 0x000C2B93
		public Location2D Location
		{
			get
			{
				return Location2D.Get(this.ParasangX, this.ParasangY);
			}
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06002609 RID: 9737 RVA: 0x000C49A6 File Offset: 0x000C2BA6
		public Location2D ResolvedLocation
		{
			get
			{
				return Location2D.Get(this.ResolvedX, this.ResolvedY);
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x0600260A RID: 9738 RVA: 0x000C49B9 File Offset: 0x000C2BB9
		public int ResolvedX
		{
			get
			{
				return this.ParasangX * 3 + this.ZoneX;
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x0600260B RID: 9739 RVA: 0x000C49CA File Offset: 0x000C2BCA
		public int ResolvedY
		{
			get
			{
				return this.ParasangY * 3 + this.ZoneY;
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x0600260C RID: 9740 RVA: 0x000C49DB File Offset: 0x000C2BDB
		public bool Visited
		{
			get
			{
				return this.ZoneID != null && The.ZoneManager.VisitedTime.ContainsKey(this.ZoneID);
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x0600260D RID: 9741 RVA: 0x000C49FC File Offset: 0x000C2BFC
		public long LastVisit
		{
			get
			{
				long num;
				if (this.ZoneID == null || !The.ZoneManager.VisitedTime.TryGetValue(this.ZoneID, out num))
				{
					return -1L;
				}
				return num;
			}
		}

		// Token: 0x0600260E RID: 9742 RVA: 0x000C4A30 File Offset: 0x000C2C30
		public bool SameAs(JournalMapNote Note)
		{
			return Note != null && this.WorldID == Note.WorldID && this.ParasangX == Note.ParasangX && this.ParasangY == Note.ParasangY && this.ZoneX == Note.ZoneX && this.ZoneY == Note.ZoneY && this.ZoneZ == Note.ZoneZ && this.Category == Note.Category && this.Text == Note.Text && this.ID == Note.ID;
		}

		// Token: 0x0600260F RID: 9743 RVA: 0x000C4AD5 File Offset: 0x000C2CD5
		public override bool Forgettable()
		{
			return false;
		}

		// Token: 0x06002610 RID: 9744 RVA: 0x000C4AD8 File Offset: 0x000C2CD8
		public override bool CanSell()
		{
			return base.CanSell() && The.ActiveZone.ZoneID != this.ZoneID;
		}

		// Token: 0x06002611 RID: 9745 RVA: 0x000C4AF9 File Offset: 0x000C2CF9
		public override string GetShareText()
		{
			return "The location of " + Grammar.LowerArticles(this.GetShortText());
		}

		// Token: 0x06002612 RID: 9746 RVA: 0x000C4B10 File Offset: 0x000C2D10
		public override string GetDisplayText()
		{
			if (this._displayText == null)
			{
				IBaseJournalEntry.SB.Clear().Append(this.Text);
				IBaseJournalEntry.SB.Compound(LoreGenerator.GenerateLandmarkDirectionsTo(this.ZoneID), '\n');
				long lastVisit = this.LastVisit;
				if (lastVisit > 0L)
				{
					IBaseJournalEntry.SB.Compound("Last visited on the ", '\n').Append(Calendar.GetDay(lastVisit)).Append(" of ")
						.Append(Calendar.GetMonth(lastVisit));
				}
				if (Options.DebugInternals)
				{
					IBaseJournalEntry.SB.Compound("\n{{internals|", '\n').Append('Ã').Append(' ');
					for (int i = 0; i < this.Attributes.Count; i++)
					{
						if (i != 0)
						{
							IBaseJournalEntry.SB.Append(", ");
						}
						IBaseJournalEntry.SB.Append(this.Attributes[i]);
					}
					if (this.History.Length > 0)
					{
						IBaseJournalEntry.SB.Compound(this.History, '\n');
					}
					IBaseJournalEntry.SB.Append("}}");
				}
				this._displayText = IBaseJournalEntry.SB.ToString();
			}
			return this._displayText;
		}

		// Token: 0x06002613 RID: 9747 RVA: 0x000C4C48 File Offset: 0x000C2E48
		public override void Reveal(string LearnedFrom = null, bool Silent = false)
		{
			if (!this.Revealed)
			{
				JournalAPI._mapNoteCategories = null;
				base.Reveal(LearnedFrom, Silent);
				this.Time = Calendar.TotalTimeTicks;
				if (!JournalAPI.MapNotes.Contains(this))
				{
					JournalAPI.MapNotes.Add(this);
				}
				string text;
				if (this.Category == "Artifacts")
				{
					text = string.Concat(new string[]
					{
						"You note the location of ",
						Grammar.InitLowerIfArticle(this.Text),
						" in the {{W|",
						JournalScreen.STR_LOCATIONS,
						" > Artifacts}} section of your journal."
					});
				}
				else if (this.Category == "Historic Sites")
				{
					text = string.Concat(new string[]
					{
						"You note the location of ",
						Grammar.MakeTitleCaseWithArticle(this.Text),
						" in the {{W|",
						JournalScreen.STR_LOCATIONS,
						" > Historic Sites}} section of your journal."
					});
				}
				else if (this.Category == "Lairs")
				{
					text = string.Concat(new string[]
					{
						"You note the location of ",
						Grammar.InitLowerIfArticle(this.Text),
						" in the {{W|",
						JournalScreen.STR_LOCATIONS,
						" > Lairs}} section of your journal."
					});
				}
				else if (this.Category == "Merchants")
				{
					text = string.Concat(new string[]
					{
						"You note the location of ",
						Grammar.InitLowerIfArticle(this.Text),
						" in the {{W|",
						JournalScreen.STR_LOCATIONS,
						" > Merchants}} section of your journal."
					});
				}
				else if (this.Category == "Natural Features")
				{
					text = string.Concat(new string[]
					{
						"You note the location of ",
						Grammar.InitLowerIfArticle(this.Text),
						" in the {{W|",
						JournalScreen.STR_LOCATIONS,
						" > Natural Features}} section of your journal."
					});
				}
				else if (this.Category == "Oddities")
				{
					text = string.Concat(new string[]
					{
						"You note the location of ",
						Grammar.InitLowerIfArticle(this.Text),
						" in the {{W|",
						JournalScreen.STR_LOCATIONS,
						" > Oddities}} section of your journal."
					});
				}
				else if (this.Category == "Baetyls")
				{
					text = string.Concat(new string[]
					{
						"You note the location of ",
						Grammar.InitLowerIfArticle(this.Text),
						" in the {{W|",
						JournalScreen.STR_LOCATIONS,
						" > Baetyls}} section of your journal."
					});
				}
				else if (this.Category == "Ruins")
				{
					string text2 = ((this.Text == "some forgotten ruins") ? Grammar.InitLower(this.Text) : Grammar.MakeTitleCaseWithArticle(this.Text));
					text = string.Concat(new string[]
					{
						"You note the location of ",
						text2,
						" in the {{W|",
						JournalScreen.STR_LOCATIONS,
						" > Ruins}} section of your journal."
					});
				}
				else if (this.Category == "Settlements")
				{
					text = string.Concat(new string[]
					{
						"You note the location of ",
						Grammar.InitLowerIfArticle(this.Text),
						" in the {{W|",
						JournalScreen.STR_LOCATIONS,
						" > Settlements}} section of your journal."
					});
				}
				else if (this.Category == "Named Locations")
				{
					text = string.Concat(new string[]
					{
						"You note the location of ",
						this.Text,
						" in the {{W|",
						JournalScreen.STR_LOCATIONS,
						" > Named Locations}} section of your journal."
					});
				}
				else if (this.Category == "Ruins with Becoming Nooks")
				{
					string text3 = ((this.Text == "some forgotten ruins") ? Grammar.InitLower(this.Text) : Grammar.MakeTitleCaseWithArticle(this.Text));
					text = string.Concat(new string[]
					{
						"You note the location of ",
						text3,
						" in the {{W|",
						JournalScreen.STR_LOCATIONS,
						" > Ruins with Becoming Nooks}} section of your journal."
					});
				}
				else
				{
					text = "tell support@freeholdgames.com unknown location category: " + this.Category;
				}
				if (this.Category != "Miscellaneous" && !Silent)
				{
					IBaseJournalEntry.DisplayMessage(text, null);
				}
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06002614 RID: 9748 RVA: 0x000C5075 File Offset: 0x000C3275
		[Obsolete]
		public Location2D location
		{
			get
			{
				return Location2D.Get(this.ResolvedX, this.ResolvedY);
			}
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06002615 RID: 9749 RVA: 0x000C5088 File Offset: 0x000C3288
		[Obsolete]
		public int x
		{
			get
			{
				return this.ParasangX * 3 + this.ZoneX;
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06002616 RID: 9750 RVA: 0x000C5099 File Offset: 0x000C3299
		[Obsolete]
		public int y
		{
			get
			{
				return this.ParasangY * 3 + this.ZoneY;
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06002617 RID: 9751 RVA: 0x000C50AA File Offset: 0x000C32AA
		// (set) Token: 0x06002618 RID: 9752 RVA: 0x000C50B2 File Offset: 0x000C32B2
		[Obsolete("use ZoneID")]
		public string zoneid
		{
			get
			{
				return this.ZoneID;
			}
			set
			{
				this.ZoneID = value;
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06002619 RID: 9753 RVA: 0x000C50BB File Offset: 0x000C32BB
		// (set) Token: 0x0600261A RID: 9754 RVA: 0x000C50C3 File Offset: 0x000C32C3
		[Obsolete("use WorldID")]
		public string w
		{
			get
			{
				return this.WorldID;
			}
			set
			{
				this.WorldID = value;
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x0600261B RID: 9755 RVA: 0x000C50CC File Offset: 0x000C32CC
		// (set) Token: 0x0600261C RID: 9756 RVA: 0x000C50D4 File Offset: 0x000C32D4
		[Obsolete("use ParasangX")]
		public int wx
		{
			get
			{
				return this.ParasangX;
			}
			set
			{
				this.ParasangX = value;
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x0600261D RID: 9757 RVA: 0x000C50DD File Offset: 0x000C32DD
		// (set) Token: 0x0600261E RID: 9758 RVA: 0x000C50E5 File Offset: 0x000C32E5
		[Obsolete("use ParasangY")]
		public int wy
		{
			get
			{
				return this.ParasangY;
			}
			set
			{
				this.ParasangY = value;
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x0600261F RID: 9759 RVA: 0x000C50EE File Offset: 0x000C32EE
		// (set) Token: 0x06002620 RID: 9760 RVA: 0x000C50F6 File Offset: 0x000C32F6
		[Obsolete("use ZoneX")]
		public int cx
		{
			get
			{
				return this.ZoneX;
			}
			set
			{
				this.ZoneX = value;
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06002621 RID: 9761 RVA: 0x000C50FF File Offset: 0x000C32FF
		// (set) Token: 0x06002622 RID: 9762 RVA: 0x000C5107 File Offset: 0x000C3307
		[Obsolete("use ZoneY")]
		public int cy
		{
			get
			{
				return this.ZoneY;
			}
			set
			{
				this.ZoneY = value;
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06002623 RID: 9763 RVA: 0x000C5110 File Offset: 0x000C3310
		// (set) Token: 0x06002624 RID: 9764 RVA: 0x000C5118 File Offset: 0x000C3318
		[Obsolete("use ZoneZ")]
		public int cz
		{
			get
			{
				return this.ZoneZ;
			}
			set
			{
				this.ZoneZ = value;
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06002625 RID: 9765 RVA: 0x000C5121 File Offset: 0x000C3321
		// (set) Token: 0x06002626 RID: 9766 RVA: 0x000C5129 File Offset: 0x000C3329
		[Obsolete("use Category")]
		public string category
		{
			get
			{
				return this.Category;
			}
			set
			{
				this.Category = value;
			}
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06002627 RID: 9767 RVA: 0x000C5132 File Offset: 0x000C3332
		// (set) Token: 0x06002628 RID: 9768 RVA: 0x000C5135 File Offset: 0x000C3335
		[Obsolete("always false")]
		public bool shown
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06002629 RID: 9769 RVA: 0x000C5137 File Offset: 0x000C3337
		// (set) Token: 0x0600262A RID: 9770 RVA: 0x000C513F File Offset: 0x000C333F
		[Obsolete]
		public bool tracked
		{
			get
			{
				return this.tracked;
			}
			set
			{
				this.tracked = value;
			}
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x0600262B RID: 9771 RVA: 0x000C5148 File Offset: 0x000C3348
		// (set) Token: 0x0600262C RID: 9772 RVA: 0x000C5150 File Offset: 0x000C3350
		[Obsolete]
		public long time
		{
			get
			{
				return this.Time;
			}
			set
			{
				this.Time = value;
			}
		}

		// Token: 0x04001307 RID: 4871
		public string WorldID;

		// Token: 0x04001308 RID: 4872
		public int ParasangX;

		// Token: 0x04001309 RID: 4873
		public int ParasangY;

		// Token: 0x0400130A RID: 4874
		public int ZoneX;

		// Token: 0x0400130B RID: 4875
		public int ZoneY;

		// Token: 0x0400130C RID: 4876
		public int ZoneZ;

		// Token: 0x0400130D RID: 4877
		public string Category;

		// Token: 0x0400130E RID: 4878
		public long Time;

		// Token: 0x0400130F RID: 4879
		public bool Tracked;

		// Token: 0x04001310 RID: 4880
		[NonSerialized]
		private string _ZoneID;

		// Token: 0x04001311 RID: 4881
		private string _displayText;
	}
}
