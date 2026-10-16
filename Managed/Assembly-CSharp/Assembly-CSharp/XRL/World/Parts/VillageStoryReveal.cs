using System;
using System.Collections.Generic;
using ConsoleLib.Console;
using HistoryKit;
using Qud.API;
using XRL.Language;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x02000F78 RID: 3960
	[Serializable]
	public class VillageStoryReveal : IPart
	{
		// Token: 0x06009D9A RID: 40346 RVA: 0x0039164A File Offset: 0x0038F84A
		public VillageStoryReveal()
		{
		}

		// Token: 0x06009D9B RID: 40347 RVA: 0x00391674 File Offset: 0x0038F874
		public VillageStoryReveal(JournalVillageNote note, string style)
			: this()
		{
			this.villageId = note.VillageID;
			this.villageName = HistoryAPI.GetEntityName(note.VillageID);
			this.eventText = note.Text.Split('|', StringSplitOptions.None)[0];
			this.secretId = note.ID;
			this.style = style;
		}

		// Token: 0x06009D9C RID: 40348 RVA: 0x003916CD File Offset: 0x0038F8CD
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009D9D RID: 40349 RVA: 0x003916D0 File Offset: 0x0038F8D0
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AutoexploreObjectEvent.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetShortDescriptionEvent.ID || ID == GetUnknownShortDescriptionEvent.ID || ID == ObjectCreatedEvent.ID;
		}

		// Token: 0x06009D9E RID: 40350 RVA: 0x00391706 File Offset: 0x0038F906
		public override bool HandleEvent(AutoexploreObjectEvent E)
		{
			if (!E.AutogetOnlyMode && E.Command != "Look" && this.IsVillageUnrevealed())
			{
				E.Command = "Look";
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009D9F RID: 40351 RVA: 0x0039173C File Offset: 0x0038F93C
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (this.style == "painting")
			{
				E.AddAdjective("{{painted|painted}}", 0);
			}
			else if (this.style == "engraving")
			{
				E.AddAdjective("{{engraved|engraved}}", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009DA0 RID: 40352 RVA: 0x0039178E File Offset: 0x0038F98E
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			this.AddStory(E);
			return base.HandleEvent(E);
		}

		// Token: 0x06009DA1 RID: 40353 RVA: 0x0039179E File Offset: 0x0038F99E
		public override bool HandleEvent(GetUnknownShortDescriptionEvent E)
		{
			this.AddStory(E);
			return base.HandleEvent(E);
		}

		// Token: 0x06009DA2 RID: 40354 RVA: 0x003917AE File Offset: 0x0038F9AE
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			this.GenerateStory();
			return base.HandleEvent(E);
		}

		// Token: 0x06009DA3 RID: 40355 RVA: 0x003917BD File Offset: 0x0038F9BD
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterLookedAt");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009DA4 RID: 40356 RVA: 0x003917D2 File Offset: 0x0038F9D2
		public override bool FireEvent(Event E)
		{
			if (E.ID == "AfterLookedAt" && !this.bLookedAt)
			{
				this.bLookedAt = true;
				if (this.eventText != null)
				{
					this.RevealVillage();
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x06009DA5 RID: 40357 RVA: 0x0039180C File Offset: 0x0038FA0C
		private void GenerateStory()
		{
			if (this.eventText == null && The.Game != null)
			{
				History sultanHistory = The.Game.sultanHistory;
				if (sultanHistory == null)
				{
					return;
				}
				HistoricEntity randomElement = sultanHistory.GetEntitiesWherePropertyEquals("type", "village").GetRandomElement(Stat.Rand);
				this.villageName = randomElement.GetCurrentSnapshot().GetProperty("name", "unknown");
				this.villageId = randomElement.id;
				List<string> list = randomElement.GetCurrentSnapshot().GetList("Gospels");
				if (list.Count > 0)
				{
					this.eventText = list[Stat.Random(0, list.Count - 1)];
					return;
				}
				this.eventText = "<marred and unreadable>";
			}
		}

		// Token: 0x06009DA6 RID: 40358 RVA: 0x003918BF File Offset: 0x0038FABF
		public JournalVillageNote GetVillageNote()
		{
			return JournalAPI.VillageNotes.Find((JournalVillageNote n) => n.ID == this.secretId);
		}

		// Token: 0x06009DA7 RID: 40359 RVA: 0x003918D8 File Offset: 0x0038FAD8
		public bool IsVillageUnrevealed()
		{
			JournalVillageNote villageNote = this.GetVillageNote();
			return villageNote != null && !villageNote.Revealed;
		}

		// Token: 0x06009DA8 RID: 40360 RVA: 0x003918FA File Offset: 0x0038FAFA
		public void RevealVillage()
		{
			JournalVillageNote villageNote = this.GetVillageNote();
			if (villageNote == null)
			{
				return;
			}
			villageNote.Reveal(null, false);
		}

		// Token: 0x06009DA9 RID: 40361 RVA: 0x00391910 File Offset: 0x0038FB10
		private void AddStory(IShortDescriptionEvent E)
		{
			if (this.eventText == null)
			{
				this.GenerateStory();
			}
			if (this.style == "painting")
			{
				E.Postfix.Append("\n{{C|Painted: This object is painted with a scene from the history of the village {{M|").Append(this.villageName).Append("}}:\n\n")
					.Append(this.eventText)
					.Append("}}\n");
				return;
			}
			if (this.style == "engraving")
			{
				E.Postfix.Append("\n{{C|Engraved: This object is engraved with a scene from the history of the village {{M|").Append(this.villageName).Append("}}:\n\n")
					.Append(this.eventText)
					.Append("}}\n");
				return;
			}
			if (this.style == "monument")
			{
				E.Postfix.Append("\n{{C|This object is a monument to a scene from the history of the village {{M|").Append(this.villageName).Append("}}:\n\n")
					.Append(this.eventText)
					.Append("}}\n");
				return;
			}
			if (!(this.style == "tattoo"))
			{
				if (this.style == "light-pattern")
				{
					E.Postfix.Append("\n{{C|Holographic: This hologram depicts a scene from the history of the village {{M|").Append(this.villageName).Append("}}:\n\n")
						.Append(this.eventText)
						.Append("}}\n");
					return;
				}
				if (this.style != null)
				{
					E.Postfix.Append("\n{{C|").Append(ColorUtility.CapitalizeExceptFormatting(this.style)).Append(": This object bears ")
						.Append(Grammar.A(this.style, false))
						.Append(" commemorating a scene from the history of the village {{M|")
						.Append(this.villageName)
						.Append("}}:\n\n")
						.Append(this.eventText)
						.Append("}}\n");
				}
			}
		}

		// Token: 0x04004127 RID: 16679
		public string style = "engraving";

		// Token: 0x04004128 RID: 16680
		public string villageName = "";

		// Token: 0x04004129 RID: 16681
		public string villageId = "";

		// Token: 0x0400412A RID: 16682
		public string eventText;

		// Token: 0x0400412B RID: 16683
		public string secretId;

		// Token: 0x0400412C RID: 16684
		public bool bLookedAt;
	}
}
