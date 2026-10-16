using System;
using HistoryKit;
using Qud.API;
using XRL.Annals;
using XRL.Language;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x02001293 RID: 4755
	[Serializable]
	public class SultanRegion : IPart
	{
		// Token: 0x0600BC20 RID: 48160 RVA: 0x0044BE6C File Offset: 0x0044A06C
		public SultanRegion()
		{
		}

		// Token: 0x0600BC21 RID: 48161 RVA: 0x0044BE74 File Offset: 0x0044A074
		public SultanRegion(HistoricEntity region)
		{
			this.Region = region;
		}

		// Token: 0x0600BC22 RID: 48162 RVA: 0x0044BE83 File Offset: 0x0044A083
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BC23 RID: 48163 RVA: 0x0044BE86 File Offset: 0x0044A086
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("SultanReveal");
			Object.SetIntProperty("ForceMutableSave", 1, false);
			base.Register(Object, Registrar);
		}

		// Token: 0x17000B42 RID: 2882
		// (get) Token: 0x0600BC24 RID: 48164 RVA: 0x0044BEA9 File Offset: 0x0044A0A9
		// (set) Token: 0x0600BC25 RID: 48165 RVA: 0x0044BEE1 File Offset: 0x0044A0E1
		public HistoricEntity Region
		{
			get
			{
				if (this._Region == null && !this.RegionEntityID.IsNullOrEmpty())
				{
					this._Region = The.Game.sultanHistory.GetEntity(this.RegionEntityID);
				}
				return this._Region;
			}
			set
			{
				this._Region = value;
				this.RegionEntityID = ((value != null) ? value.id : null);
			}
		}

		// Token: 0x0600BC26 RID: 48166 RVA: 0x0044BEFC File Offset: 0x0044A0FC
		public override bool FireEvent(Event E)
		{
			if (E.ID == "SultanReveal")
			{
				if (this.revealed)
				{
					return false;
				}
				this.revealed = true;
				History sultanHistory = The.Game.sultanHistory;
				HistoricEntitySnapshot currentSnapshot = this.Region.GetCurrentSnapshot();
				string property = QudHistoryHelpers.GetRegionalizationParametersSnapshot(sultanHistory).GetProperty("organizingPrinciple", "unknown");
				string tag = this.ParentObject.GetTag("AlternateTerrainName", this.ParentObject.GetTag("Terrain", null));
				if (50.in100())
				{
					string text = HistoricStringExpander.ExpandString("<spice.history.regions.terrain." + tag + ".over.!random>", currentSnapshot, sultanHistory, null, null);
					string text2;
					do
					{
						text2 = HistoricStringExpander.ExpandString("<spice.history.regions.terrain." + tag + ".over.!random>", currentSnapshot, sultanHistory, null, null);
					}
					while (text.Equals(text2) && !string.IsNullOrEmpty(text));
					string text3 = HistoricStringExpander.ExpandString(string.Concat(new string[]
					{
						"<spice.history.regions.organizingPrinciple.",
						property,
						".",
						currentSnapshot.GetProperty("organizingPrincipleType", "unknown"),
						".thingsTheyDid.!random>"
					}), currentSnapshot, sultanHistory, null, null);
					this.ParentObject.GetPart<Description>().Short = Grammar.InitCap(HistoricStringExpander.ExpandString(string.Concat(new string[]
					{
						text,
						" and ",
						text2,
						", here <spice.commonPhrases.stretches.!random> the ancient ",
						currentSnapshot.GetProperty("governmentType", "unknown"),
						" where ",
						text3,
						"."
					}), currentSnapshot, sultanHistory, null, null));
				}
				else
				{
					string text4 = HistoricStringExpander.ExpandString("<spice.history.regions.terrain." + tag + ".over.!random>", currentSnapshot, sultanHistory, null, null);
					string text5 = HistoricStringExpander.ExpandString(string.Concat(new string[]
					{
						"<spice.history.regions.organizingPrinciple.",
						property,
						".",
						currentSnapshot.GetProperty("organizingPrincipleType", "unknown"),
						".thingsTheyDid.!random>"
					}), currentSnapshot, sultanHistory, null, null);
					this.ParentObject.GetPart<Description>().Short = Grammar.InitCap(HistoricStringExpander.ExpandString(string.Concat(new string[]
					{
						text5,
						" in the <spice.commonPhrases.lost.!random> ",
						currentSnapshot.GetProperty("governmentType", "unknown"),
						" whose ruins lie ",
						text4,
						"."
					}), currentSnapshot, sultanHistory, null, null));
				}
				string text6 = null;
				string property2 = currentSnapshot.GetProperty("organizingPrincipleType", "unknown");
				if (property2 == "poor")
				{
					int num = Stat.Random(0, 1);
					if (num == 0)
					{
						text6 = "HistoricHuts";
					}
					else if (num == 1)
					{
						text6 = "HistoricHills";
					}
				}
				else if (property2 == "middle-class")
				{
					int num2 = Stat.Random(0, 2);
					if (num2 == 0)
					{
						text6 = "HistoricHouse";
					}
					else if (num2 == 1)
					{
						text6 = "HistoricHouses";
					}
					else if (num2 == 2)
					{
						text6 = "HistoricGrid";
					}
				}
				else if (property2 == "rich")
				{
					int num3 = Stat.Random(0, 2);
					if (num3 == 0)
					{
						text6 = "HistoricPantheon";
					}
					else if (num3 == 1)
					{
						text6 = "HistoricBuilding";
					}
					else if (num3 == 2)
					{
						text6 = "HistoricCastle";
					}
				}
				else if (property == "religion" || property == "profession")
				{
					int num4 = Stat.Random(0, 15);
					if (num4 == 0)
					{
						text6 = "HistoricPantheon";
					}
					else if (num4 == 1)
					{
						text6 = "HistoricBuilding";
					}
					else if (num4 == 2)
					{
						text6 = "HistoricCastle";
					}
					else if (num4 == 3)
					{
						text6 = "HistoricHuts";
					}
					else if (num4 == 4)
					{
						text6 = "HistoricHills";
					}
					else if (num4 == 5)
					{
						text6 = "HistoricHouse";
					}
					else if (num4 == 6)
					{
						text6 = "HistoricHouses";
					}
					else if (num4 == 7)
					{
						text6 = "HistoricGrid";
					}
					else if (num4 == 8)
					{
						text6 = "HistoricBuilding2";
					}
					else if (num4 == 9)
					{
						text6 = "HistoricBuilding3";
					}
					else if (num4 == 10)
					{
						text6 = "HistoricArch";
					}
					else if (num4 == 11)
					{
						text6 = "HistoricPond";
					}
					else if (num4 == 12)
					{
						text6 = "HistoricPools";
					}
					else if (num4 == 13)
					{
						text6 = "HistoricTower";
					}
					else if (num4 == 14)
					{
						text6 = "HistoricArms";
					}
					else if (num4 == 15)
					{
						this.ParentObject.Render.Tile = "HistoricRing";
					}
				}
				else if (property2 == "gardens")
				{
					text6 = "HistoricGardens";
				}
				else if (property2 == "pools")
				{
					text6 = "HistoricPools";
				}
				else if (property2 == "museums")
				{
					text6 = "HistoricPond";
				}
				else if (property2 == "college")
				{
					text6 = "HistoricPantheon";
				}
				else if (property2 == "waste")
				{
					text6 = "HistoricPools";
				}
				else if (property2 == "consulate")
				{
					text6 = "HistoricBuilding";
				}
				else if (property2 == "market")
				{
					text6 = "HistoricRing";
				}
				else if (property2 == "forums")
				{
					text6 = "HistoricArch";
				}
				else if (property2 == "residential")
				{
					text6 = "HistoricHouses";
				}
				else if (property2 == "theaters")
				{
					text6 = "HistoricRing";
				}
				else if (property2 == "food storage")
				{
					text6 = "HistoricBuilding2";
				}
				else if (property2 == "pipe hub")
				{
					text6 = "HistoricArms";
				}
				else if (property2 == "prison")
				{
					text6 = "HistoricTower";
				}
				else if (property2 == "barracks")
				{
					text6 = "HistoricGrid";
				}
				else if (property2 == "temple")
				{
					text6 = "HistoricPantheon";
				}
				else if (property2 == "tavern")
				{
					text6 = "HistoricBuilding3";
				}
				GameObjectBlueprint gameObjectBlueprint = ((text6 != null) ? GameObjectFactory.Factory.Blueprints[text6] : null);
				if (gameObjectBlueprint != null)
				{
					this.ParentObject.Render.Tile = gameObjectBlueprint.GetPartParameter<string>("Render", "Tile", null);
					this.ParentObject.Render.ColorString = "&" + Crayons.GetRandomColorAll(null);
				}
				else
				{
					this.ParentObject.Render.Tile = "Terrain/sw_historic_pantheon.bmp";
					this.ParentObject.Render.ColorString = "&" + Crayons.GetRandomColorAll(null);
				}
				do
				{
					this.ParentObject.Render.DetailColor = Crayons.GetRandomColorAll(null);
				}
				while (this.ParentObject.Render.DetailColor == this.ParentObject.Render.ColorString.Substring(1));
				this.ParentObject.Render.RenderString = "#";
				this.ParentObject.SetStringProperty("OverlayColor", "&W", false);
				string.Concat(new string[]
				{
					"JoppaWorld.",
					this.ParentObject.CurrentCell.X.ToString(),
					".",
					this.ParentObject.CurrentCell.Y.ToString(),
					".1.1.10"
				});
				string text7 = Grammar.MakeTitleCase(currentSnapshot.GetProperty("newName", "unknown"));
				string text8 = "";
				if (text7.StartsWith("the ") || text7.StartsWith("The "))
				{
					text7 = text7.Substring(4);
					text8 = "the";
				}
				this.ParentObject.GiveProperName(text7, true, null, false, null, null, null, null);
				this.ParentObject.SetStringProperty("IndefiniteArticle", text8, false);
				this.ParentObject.SetStringProperty("DefiniteArticle", text8, false);
				this.ParentObject.SetStringProperty("OverrideIArticle", text8, false);
				this.ParentObject.SetStringProperty("Gender", "nonspecific", false);
				this.ParentObject.SetGender("nonspecific");
				TerrainTravel part = this.ParentObject.GetPart<TerrainTravel>();
				if (part != null)
				{
					part.ClearEncounters();
				}
				if (this.secretId != null)
				{
					JournalMapNote mapNote = JournalAPI.GetMapNote(this.secretId);
					if (!mapNote.Revealed)
					{
						mapNote.Reveal(null, false);
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004B61 RID: 19297
		public string RegionEntityID;

		// Token: 0x04004B62 RID: 19298
		public string secretId;

		// Token: 0x04004B63 RID: 19299
		public bool revealed;

		// Token: 0x04004B64 RID: 19300
		[NonSerialized]
		private HistoricEntity _Region;
	}
}
