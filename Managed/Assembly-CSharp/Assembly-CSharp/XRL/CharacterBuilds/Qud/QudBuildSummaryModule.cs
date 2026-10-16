using System;
using System.Text;
using ConsoleLib.Console;
using XRL.UI.Framework;
using XRL.World;

namespace XRL.CharacterBuilds.Qud
{
	// Token: 0x02000598 RID: 1432
	public class QudBuildSummaryModule : QudEmbarkBuilderModule<QudBuildSummaryModuleData>
	{
		// Token: 0x06003D56 RID: 15702 RVA: 0x0012C85E File Offset: 0x0012AA5E
		public override bool IncludeInBuildCodes()
		{
			return false;
		}

		// Token: 0x06003D57 RID: 15703 RVA: 0x0012C861 File Offset: 0x0012AA61
		public override bool shouldBeEnabled()
		{
			EmbarkBuilder builder = this.builder;
			object obj;
			if (builder == null)
			{
				obj = null;
			}
			else
			{
				QudSubtypeModule module = builder.GetModule<QudSubtypeModule>();
				if (module == null)
				{
					obj = null;
				}
				else
				{
					QudSubtypeModuleData data = module.data;
					obj = ((data != null) ? data.Subtype : null);
				}
			}
			return obj != null;
		}

		// Token: 0x06003D58 RID: 15704 RVA: 0x0012C890 File Offset: 0x0012AA90
		public override string DataErrors()
		{
			return null;
		}

		// Token: 0x06003D59 RID: 15705 RVA: 0x0012C893 File Offset: 0x0012AA93
		public override string DataWarnings()
		{
			return null;
		}

		// Token: 0x06003D5A RID: 15706 RVA: 0x0012C896 File Offset: 0x0012AA96
		public override void InitFromSeed(string seed)
		{
		}

		// Token: 0x06003D5B RID: 15707 RVA: 0x0012C898 File Offset: 0x0012AA98
		public override void onNext()
		{
			this.builder.advance(false, true, false);
		}

		// Token: 0x06003D5C RID: 15708 RVA: 0x0012C8A8 File Offset: 0x0012AAA8
		public override SummaryBlockData GetSummaryBlock()
		{
			string text = "creatures/caste_16.bmp";
			string text2 = "Y";
			string text3 = "k";
			string text4 = "w";
			string text5 = "Humanoid";
			StringBuilder stringBuilder = new StringBuilder();
			QudGenotypeModule module = this.builder.GetModule<QudGenotypeModule>();
			string text6;
			if (module == null)
			{
				text6 = null;
			}
			else
			{
				QudGenotypeModuleData data = module.data;
				text6 = ((data != null) ? data.Genotype : null);
			}
			GenotypeEntry genotypeEntry;
			if (GenotypeFactory.TryGetGenotypeEntry(text6, out genotypeEntry))
			{
				stringBuilder.Compound(genotypeEntry.DisplayName, '\n');
				text5 = (genotypeEntry.BodyObject.IsNullOrEmpty() ? text5 : genotypeEntry.BodyObject);
			}
			QudSubtypeModule module2 = this.builder.GetModule<QudSubtypeModule>();
			string text7;
			if (module2 == null)
			{
				text7 = null;
			}
			else
			{
				QudSubtypeModuleData data2 = module2.data;
				text7 = ((data2 != null) ? data2.Subtype : null);
			}
			SubtypeEntry subtypeEntry;
			if (SubtypeFactory.TryGetSubtypeEntry(text7, out subtypeEntry))
			{
				if (stringBuilder.Length != 0)
				{
					stringBuilder.Insert(0, '\n');
				}
				stringBuilder.Insert(0, subtypeEntry.DisplayName);
				text5 = (subtypeEntry.BodyObject.IsNullOrEmpty() ? text5 : subtypeEntry.BodyObject);
			}
			text5 = this.builder.fireBootEvent<string>(QudGameBootModule.BOOTEVENT_BOOTPLAYEROBJECTBLUEPRINT, text5);
			text = this.builder.fireBootEvent<string>(QudGameBootModule.BOOTEVENT_BOOTPLAYERTILE, null) ?? text;
			text2 = this.builder.fireBootEvent<string>(QudGameBootModule.BOOTEVENT_BOOTPLAYERTILEFOREGROUND, null) ?? text2;
			string text8 = this.builder.fireBootEvent<string>(QudGameBootModule.BOOTEVENT_BOOTPLAYERTILEBACKGROUND, null) ?? text3;
			text4 = this.builder.fireBootEvent<string>(QudGameBootModule.BOOTEVENT_BOOTPLAYERTILEDETAIL, null) ?? text4;
			GameObjectBlueprint gameObjectBlueprint;
			if (GameObjectFactory.Factory.Blueprints.TryGetValue(text5, out gameObjectBlueprint))
			{
				stringBuilder.Compound(gameObjectBlueprint.GetTag("BodyDisplayName", text5), '\n');
			}
			return new SummaryBlockData
			{
				Id = "overall",
				Description = stringBuilder.ToString(),
				IconPath = text,
				IconDetailColor = ColorUtility.colorFromChar(text4[0]),
				IconForegroundColor = ColorUtility.colorFromChar(text2[0]),
				SortOrder = 0
			};
		}
	}
}
