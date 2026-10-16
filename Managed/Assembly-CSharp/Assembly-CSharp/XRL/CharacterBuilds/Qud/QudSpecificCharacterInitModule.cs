using System;
using XRL.UI;
using XRL.World;

namespace XRL.CharacterBuilds.Qud
{
	// Token: 0x0200059A RID: 1434
	public class QudSpecificCharacterInitModule : AbstractEmbarkBuilderModule
	{
		// Token: 0x06003D5F RID: 15711 RVA: 0x0012CA9A File Offset: 0x0012AC9A
		public override void InitFromSeed(string seed)
		{
		}

		// Token: 0x06003D60 RID: 15712 RVA: 0x0012CA9C File Offset: 0x0012AC9C
		public override void Init()
		{
		}

		// Token: 0x06003D61 RID: 15713 RVA: 0x0012CAA0 File Offset: 0x0012ACA0
		public override object handleBootEvent(string id, XRLGame game, EmbarkInfo info, object element = null)
		{
			if (!(id == QudGameBootModule.BOOTEVENT_BEFOREBOOTPLAYEROBJECT))
			{
				return base.handleBootEvent(id, game, info, element);
			}
			string text = "Humanoid";
			QudGenotypeModule module = this.builder.GetModule<QudGenotypeModule>();
			GenotypeEntry genotypeEntry;
			if (module == null)
			{
				genotypeEntry = null;
			}
			else
			{
				QudGenotypeModuleData data = module.data;
				genotypeEntry = ((data != null) ? data.Entry : null);
			}
			GenotypeEntry genotypeEntry2 = genotypeEntry;
			if (genotypeEntry2 != null)
			{
				text = (genotypeEntry2.BodyObject.IsNullOrEmpty() ? text : genotypeEntry2.BodyObject);
			}
			QudSubtypeModule module2 = this.builder.GetModule<QudSubtypeModule>();
			SubtypeEntry subtypeEntry;
			if (module2 == null)
			{
				subtypeEntry = null;
			}
			else
			{
				QudSubtypeModuleData data2 = module2.data;
				subtypeEntry = ((data2 != null) ? data2.Entry : null);
			}
			SubtypeEntry subtypeEntry2 = subtypeEntry;
			if (subtypeEntry2 != null)
			{
				text = (subtypeEntry2.BodyObject.IsNullOrEmpty() ? text : subtypeEntry2.BodyObject);
			}
			text = info.fireBootEvent<string>(QudGameBootModule.BOOTEVENT_BOOTPLAYEROBJECTBLUEPRINT, game, text);
			GameObject gameObject = GameObject.Create(text);
			if (gameObject == null)
			{
				Popup.ShowAsync("Error creating player body. Unknown blueprint \"" + text + "\"", true, true, true, true, false).Wait();
				throw new Exception("Unknown blueprint " + text);
			}
			gameObject.BaseID = 1;
			gameObject.InjectGeneID("OriginalPlayer");
			gameObject.Brain.Allegiance.Clear();
			gameObject.Brain.Allegiance["Player"] = 100;
			string text2 = ((subtypeEntry2 != null) ? subtypeEntry2.Species : null) ?? ((genotypeEntry2 != null) ? genotypeEntry2.Species : null);
			if (!string.IsNullOrEmpty(text2))
			{
				gameObject.SetStringProperty("Species", text2, false);
			}
			gameObject.SetStringProperty("OriginalPlayerBody", "1", false);
			gameObject.SetIntProperty("Renamed", 1, false);
			return gameObject;
		}
	}
}
