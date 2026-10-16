using System;
using HistoryKit;
using Qud.API;
using XRL.Core;
using XRL.Language;

namespace XRL.World.Parts
{
	// Token: 0x0200129D RID: 4765
	[Serializable]
	public class VillageTerrain : IPart
	{
		// Token: 0x0600BC5E RID: 48222 RVA: 0x0044DD49 File Offset: 0x0044BF49
		public VillageTerrain()
		{
		}

		// Token: 0x0600BC5F RID: 48223 RVA: 0x0044DD51 File Offset: 0x0044BF51
		public VillageTerrain(HistoricEntity village)
		{
			this.Village = village;
		}

		// Token: 0x0600BC60 RID: 48224 RVA: 0x0044DD60 File Offset: 0x0044BF60
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BC61 RID: 48225 RVA: 0x0044DD63 File Offset: 0x0044BF63
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Object.SetIntProperty("ForceMutableSave", 1, false);
			Registrar.Register("VillageReveal");
			base.Register(Object, Registrar);
		}

		// Token: 0x17000B44 RID: 2884
		// (get) Token: 0x0600BC62 RID: 48226 RVA: 0x0044DD86 File Offset: 0x0044BF86
		// (set) Token: 0x0600BC63 RID: 48227 RVA: 0x0044DDBE File Offset: 0x0044BFBE
		public HistoricEntity Village
		{
			get
			{
				if (this._Village == null && !this.VillageEntityID.IsNullOrEmpty())
				{
					this._Village = The.Game.sultanHistory.GetEntity(this.VillageEntityID);
				}
				return this._Village;
			}
			set
			{
				this._Village = value;
				this.VillageEntityID = ((value != null) ? value.id : null);
			}
		}

		// Token: 0x0600BC64 RID: 48228 RVA: 0x0044DDDC File Offset: 0x0044BFDC
		public override bool FireEvent(Event E)
		{
			if (!(E.ID == "VillageReveal"))
			{
				return base.FireEvent(E);
			}
			if (this.revealed)
			{
				return false;
			}
			this.revealed = true;
			History sultanHistory = XRLCore.Core.Game.sultanHistory;
			HistoricEntitySnapshot currentSnapshot = this.Village.GetCurrentSnapshot();
			string tag = this.ParentObject.GetTag("AlternateTerrainName", this.ParentObject.GetTag("Terrain", null));
			string text = HistoricStringExpander.ExpandString("<spice.history.regions.terrain." + tag + ".over.!random>", currentSnapshot, sultanHistory, null, null);
			string text2;
			do
			{
				text2 = HistoricStringExpander.ExpandString("<spice.history.regions.terrain." + tag + ".over.!random>", currentSnapshot, sultanHistory, null, null);
			}
			while (text.Equals(text2) && !string.IsNullOrEmpty(text));
			if (If.d100(30, null, null))
			{
				text = text + " and " + text2;
			}
			string text3 = ((currentSnapshot.GetList("sacredThings").Count > 0) ? currentSnapshot.GetList("sacredThings").GetRandomElement<string>(null) : currentSnapshot.GetProperty("defaultSacredThing", "unknown"));
			string text4 = ((currentSnapshot.GetList("profaneThings").Count > 0) ? currentSnapshot.GetList("profaneThings").GetRandomElement<string>(null) : currentSnapshot.GetProperty("defaultProfaneThing", "unknown"));
			this.ParentObject.GetPart<Description>().Short = Grammar.InitCap(HistoricStringExpander.ExpandString("<spice.villages.description.!random>.", null).Replace("*terrainFragment*", text).Replace("*sacredThing*", text3)
				.Replace("*profaneThing*", text4)
				.Replace("*faction*", Faction.GetFormattedName(currentSnapshot.GetProperty("baseFaction", "unknown"))));
			this.ParentObject.Render.Tile = "Terrain/sw_joppa.bmp";
			this.ParentObject.Render.ColorString = "&" + Crayons.GetRandomColorAll(new Random(currentSnapshot.GetProperty("name", "unknown").GetHashCode()));
			this.ParentObject.GiveProperName(Grammar.MakeTitleCase(currentSnapshot.GetProperty("name", "unknown")), true, null, false, null, null, null, null);
			this.ParentObject.SetStringProperty("IndefiniteArticle", "", false);
			this.ParentObject.SetStringProperty("DefiniteArticle", "", false);
			this.ParentObject.SetStringProperty("OverrideIArticle", "", false);
			this.ParentObject.SetStringProperty("Gender", "nonspecific", false);
			this.ParentObject.SetGender("nonspecific");
			Random random = new Random(currentSnapshot.GetProperty("name", "unknown").GetHashCode() + 1);
			do
			{
				this.ParentObject.Render.DetailColor = Crayons.GetRandomColorAll(random);
			}
			while (this.ParentObject.Render.DetailColor == this.ParentObject.Render.ColorString.Substring(1));
			this.ParentObject.Render.RenderString = "#";
			TerrainTravel part = this.ParentObject.GetPart<TerrainTravel>();
			if (part != null)
			{
				part.ClearEncounters();
			}
			this.ParentObject.SetStringProperty("OverlayColor", "&W", false);
			if (this.secretId != null)
			{
				JournalMapNote mapNote = JournalAPI.GetMapNote(this.secretId);
				if (!mapNote.Revealed)
				{
					mapNote.Reveal(null, false);
				}
			}
			return true;
		}

		// Token: 0x04004B7C RID: 19324
		public string VillageEntityID;

		// Token: 0x04004B7D RID: 19325
		public string secretId;

		// Token: 0x04004B7E RID: 19326
		public bool revealed;

		// Token: 0x04004B7F RID: 19327
		[NonSerialized]
		private HistoricEntity _Village;
	}
}
