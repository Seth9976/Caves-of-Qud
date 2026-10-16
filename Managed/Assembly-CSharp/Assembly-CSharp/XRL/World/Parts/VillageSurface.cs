using System;
using HistoryKit;
using Qud.API;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x0200129C RID: 4764
	[Serializable]
	public class VillageSurface : IPart
	{
		// Token: 0x0600BC54 RID: 48212 RVA: 0x0044D897 File Offset: 0x0044BA97
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BC55 RID: 48213 RVA: 0x0044D89A File Offset: 0x0044BA9A
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EnteredCellEvent.ID || ID == ZoneActivatedEvent.ID || ID == ZoneThawedEvent.ID;
		}

		// Token: 0x0600BC56 RID: 48214 RVA: 0x0044D8C0 File Offset: 0x0044BAC0
		public override bool HandleEvent(EnteredCellEvent E)
		{
			GameObject player = The.Player;
			if (player != null)
			{
				player.RegisterPartEvent(this, "EnteredCell");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BC57 RID: 48215 RVA: 0x0044D8DF File Offset: 0x0044BADF
		public override bool HandleEvent(ZoneActivatedEvent E)
		{
			GameObject player = The.Player;
			if (player != null)
			{
				player.RegisterPartEvent(this, "EnteredCell");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BC58 RID: 48216 RVA: 0x0044D8FE File Offset: 0x0044BAFE
		public override bool HandleEvent(ZoneThawedEvent E)
		{
			GameObject player = The.Player;
			if (player != null)
			{
				player.RegisterPartEvent(this, "EnteredCell");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BC59 RID: 48217 RVA: 0x0044D91D File Offset: 0x0044BB1D
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600BC5A RID: 48218 RVA: 0x0044D920 File Offset: 0x0044BB20
		public override void TurnTick(long TimeTick, int Amount)
		{
			this.CheckReveal();
		}

		// Token: 0x0600BC5B RID: 48219 RVA: 0x0044D928 File Offset: 0x0044BB28
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EnteredCell")
			{
				GameObject player = The.Player;
				if (player != null)
				{
					player.UnregisterPartEvent(this, "EnteredCell");
				}
				this.CheckReveal();
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600BC5C RID: 48220 RVA: 0x0044D960 File Offset: 0x0044BB60
		public void CheckReveal()
		{
			if (The.Player == null)
			{
				return;
			}
			Zone currentZone = this.ParentObject.GetCurrentZone();
			if (!The.Player.InZone(currentZone))
			{
				return;
			}
			string text = "Visit " + this.VillageName;
			string text2 = "Visited_" + this.VillageName;
			if (The.Game.HasQuest(text))
			{
				The.Game.CompleteQuest(text);
				if (The.Game.GetIntGameState(text2, 0) != 1)
				{
					if (!this.IsVillageZero)
					{
						Achievement.VILLAGES_100.Progress.Increment(1);
					}
					The.Game.SetIntGameState(text2, 1);
					if (The.Player != null && !this.IsVillageZero)
					{
						JournalAPI.AddAccomplishment("You visited the village of " + this.VillageName + ".", HistoricStringExpander.ExpandString(string.Concat(new string[]
						{
							"In the month of ",
							Calendar.GetMonth(),
							" of ",
							Calendar.GetYear().ToString(),
							" AR, =name= founded the village of ",
							this.VillageName,
							" to <spice.history.gospels.HumblePractice.LateSultanate.!random>."
						}), null), string.Concat(new string[]
						{
							"Acting against the prohibition on the practice of <spice.elements.",
							The.Player.GetMythicDomain(5, null),
							".practices.!random>, =name= led an army to the gates of ",
							this.VillageName,
							". =name= <spice.commonPhrases.liberated.!random> its citizens, and in ",
							The.Player.GetPronounProvider(false).PossessiveAdjective,
							" honor they <spice.history.gospels.Celebration.LateSultanate.!random>."
						}), null, "general", MuralCategory.BecomesLoved, MuralWeight.Medium, null, -1L, true);
					}
				}
			}
			else if (The.Game.GetIntGameState(text2, 0) != 1)
			{
				if (!this.IsVillageZero)
				{
					GameObject player = The.Player;
					if (player != null)
					{
						player.AwardXP(currentZone.NewTier * 250, -1, 0, int.MaxValue, null, null, null, null, currentZone.ZoneID, null);
					}
					Achievement.VILLAGES_100.Progress.Increment(1);
				}
				The.Game.SetIntGameState(text2, 1);
				if (The.Player != null && !this.IsVillageZero)
				{
					JournalAPI.AddAccomplishment("You visited the village of " + this.VillageName + ".", string.Concat(new string[]
					{
						"In the month of ",
						Calendar.GetMonth(),
						" of ",
						Calendar.GetYear().ToString(),
						" AR, =name= founded the village of ",
						this.VillageName,
						" to <spice.history.gospels.HumblePractice.LateSultanate.!random>."
					}), string.Concat(new string[]
					{
						"Acting against the prohibition on the practice of <spice.elements.",
						The.Player.GetMythicDomain(5, null),
						".practices.!random>, =name= led an army to the gates of ",
						this.VillageName,
						". =name= <spice.commonPhrases.liberated.!random> its citizens, and in ",
						The.Player.GetPronounProvider(false).PossessiveAdjective,
						" honor they <spice.history.gospels.Celebration.LateSultanate.!random>."
					}), null, "general", MuralCategory.BecomesLoved, MuralWeight.Medium, null, -1L, true);
				}
			}
			if (!The.Game.HasIntGameState(this.RevealKey))
			{
				The.Game.SetIntGameState(this.RevealKey, 1);
				if (this.RevealLocation != null && The.Game.ZoneManager.GetZone("JoppaWorld").GetCell(this.RevealLocation.x, this.RevealLocation.y).FireEvent("VillageReveal") && this.RevealString != null && !this.IsVillageZero && The.Player != null)
				{
					Popup.Show(this.RevealString, null, "sfx_newLocation_discovered", true, true, true, true, null);
				}
				JournalMapNote mapNote = JournalAPI.GetMapNote(this.RevealSecret);
				if (mapNote != null && !mapNote.Revealed)
				{
					JournalAPI.RevealMapNote(mapNote, false, null);
				}
			}
			Zone currentZone2 = this.ParentObject.CurrentZone;
			if (currentZone2 != null)
			{
				currentZone2.GetWorldTerrainObject().SetStringProperty("MinimapColor", "g", false);
			}
			GameObject player2 = The.Player;
			if (player2 != null)
			{
				player2.UnregisterPartEvent(this, "EnteredCell");
			}
			this.ParentObject.Obliterate(null, false, null);
		}

		// Token: 0x04004B75 RID: 19317
		public string VillageName;

		// Token: 0x04004B76 RID: 19318
		public string RevealString;

		// Token: 0x04004B77 RID: 19319
		public string RevealSecret;

		// Token: 0x04004B78 RID: 19320
		public string RevealKey;

		// Token: 0x04004B79 RID: 19321
		public bool IsVillageZero;

		// Token: 0x04004B7A RID: 19322
		public Vector2i RevealLocation;

		// Token: 0x04004B7B RID: 19323
		public int region = -1;
	}
}
