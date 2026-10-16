using System;
using System.Collections.Generic;
using System.Text;
using XRL.CharacterBuilds.Qud.UI;
using XRL.UI.Framework;
using XRL.World;

namespace XRL.CharacterBuilds.Qud
{
	// Token: 0x02000594 RID: 1428
	public class QudAttributesModule : QudEmbarkBuilderModule<QudAttributesModuleData>
	{
		// Token: 0x06003D42 RID: 15682 RVA: 0x0012C274 File Offset: 0x0012A474
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

		// Token: 0x06003D43 RID: 15683 RVA: 0x0012C2A3 File Offset: 0x0012A4A3
		public override bool shouldBeEditable()
		{
			return this.builder.IsEditableGameMode();
		}

		// Token: 0x06003D44 RID: 15684 RVA: 0x0012C2B0 File Offset: 0x0012A4B0
		public override string DataWarnings()
		{
			int num = (int)this.builder.handleUIEvent(QudAttributesModuleWindow.EID_GET_BASE_AP, 0);
			QudAttributesModuleData data = base.data;
			bool flag;
			if (data == null)
			{
				flag = true;
			}
			else
			{
				int apRemaining = data.apRemaining;
				flag = false;
			}
			if (flag)
			{
				return "You have unspent attribute points.";
			}
			if (base.data.apRemaining > 0)
			{
				return "You have unspent attribute points.";
			}
			return base.DataWarnings();
		}

		// Token: 0x06003D45 RID: 15685 RVA: 0x0012C310 File Offset: 0x0012A510
		public override string DataErrors()
		{
			int num = (int)this.builder.handleUIEvent(QudAttributesModuleWindow.EID_GET_BASE_AP, 0);
			QudAttributesModuleData data = base.data;
			bool flag;
			if (data == null)
			{
				flag = true;
			}
			else
			{
				int apRemaining = data.apRemaining;
				flag = false;
			}
			if (flag)
			{
				return base.DataErrors();
			}
			if (base.data.apRemaining < 0)
			{
				return "You have spent too many attribute points!";
			}
			return base.DataErrors();
		}

		// Token: 0x06003D46 RID: 15686 RVA: 0x0012C370 File Offset: 0x0012A570
		public override void InitFromSeed(string seed)
		{
		}

		// Token: 0x06003D47 RID: 15687 RVA: 0x0012C374 File Offset: 0x0012A574
		public Dictionary<string, int> GetTypeStats(GenotypeEntry GenotypeEntry, SubtypeEntry SubtypeEntry)
		{
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			if (GenotypeEntry != null)
			{
				foreach (KeyValuePair<string, GenotypeStat> keyValuePair in GenotypeEntry.Stats)
				{
					if (keyValuePair.Value.Minimum == -999)
					{
						dictionary.TryAdd(keyValuePair.Key, 0);
					}
					else
					{
						dictionary[keyValuePair.Key] = keyValuePair.Value.Minimum;
					}
				}
			}
			if (SubtypeEntry != null)
			{
				foreach (KeyValuePair<string, SubtypeStat> keyValuePair2 in SubtypeEntry.Stats)
				{
					if (keyValuePair2.Value.Minimum == -999)
					{
						dictionary.TryAdd(keyValuePair2.Key, 0);
					}
					else
					{
						dictionary[keyValuePair2.Key] = keyValuePair2.Value.Minimum;
					}
				}
			}
			if (GenotypeEntry != null)
			{
				foreach (KeyValuePair<string, GenotypeStat> keyValuePair3 in GenotypeEntry.Stats)
				{
					int num;
					dictionary.TryGetValue(keyValuePair3.Key, out num);
					dictionary[keyValuePair3.Key] = num + ((keyValuePair3.Value.Bonus == -999) ? 0 : keyValuePair3.Value.Bonus);
				}
			}
			if (SubtypeEntry != null)
			{
				foreach (KeyValuePair<string, SubtypeStat> keyValuePair4 in SubtypeEntry.Stats)
				{
					int num2;
					dictionary.TryGetValue(keyValuePair4.Key, out num2);
					dictionary[keyValuePair4.Key] = num2 + ((keyValuePair4.Value.Bonus == -999) ? 0 : keyValuePair4.Value.Bonus);
				}
			}
			return dictionary;
		}

		// Token: 0x06003D48 RID: 15688 RVA: 0x0012C588 File Offset: 0x0012A788
		public Dictionary<string, int> GetFinalStats(EmbarkBuilder Builder)
		{
			QudGenotypeModule module = Builder.GetModule<QudGenotypeModule>();
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
			QudSubtypeModule module2 = Builder.GetModule<QudSubtypeModule>();
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
			return this.GetFinalStats(genotypeEntry, subtypeEntry);
		}

		// Token: 0x06003D49 RID: 15689 RVA: 0x0012C5D7 File Offset: 0x0012A7D7
		public Dictionary<string, int> GetFinalStats(EmbarkInfo Info)
		{
			QudGenotypeModuleData data = Info.getData<QudGenotypeModuleData>();
			GenotypeEntry genotypeEntry = ((data != null) ? data.Entry : null);
			QudSubtypeModuleData data2 = Info.getData<QudSubtypeModuleData>();
			return this.GetFinalStats(genotypeEntry, (data2 != null) ? data2.Entry : null);
		}

		// Token: 0x06003D4A RID: 15690 RVA: 0x0012C604 File Offset: 0x0012A804
		public Dictionary<string, int> GetFinalStats(GenotypeEntry GenotypeEntry, SubtypeEntry SubtypeEntry)
		{
			Dictionary<string, int> typeStats = this.GetTypeStats(GenotypeEntry, SubtypeEntry);
			QudAttributesModuleData data = base.data;
			Dictionary<string, int> dictionary = ((data != null) ? data.PointsPurchased : null);
			if (dictionary != null)
			{
				foreach (KeyValuePair<string, int> keyValuePair in dictionary)
				{
					int num;
					typeStats.TryGetValue(keyValuePair.Key, out num);
					typeStats[keyValuePair.Key] = keyValuePair.Value + num;
				}
			}
			return typeStats;
		}

		// Token: 0x06003D4B RID: 15691 RVA: 0x0012C694 File Offset: 0x0012A894
		public override object handleBootEvent(string id, XRLGame game, EmbarkInfo info, object element = null)
		{
			if (id == QudGameBootModule.BOOTEVENT_BEFOREBOOTPLAYEROBJECT)
			{
				GameObject gameObject = element as GameObject;
				if (gameObject != null)
				{
					foreach (KeyValuePair<string, int> keyValuePair in this.GetFinalStats(info))
					{
						gameObject.GetStat(keyValuePair.Key).BaseValue = keyValuePair.Value;
					}
				}
			}
			return base.handleBootEvent(id, game, info, element);
		}

		// Token: 0x06003D4C RID: 15692 RVA: 0x0012C720 File Offset: 0x0012A920
		public override SummaryBlockData GetSummaryBlock()
		{
			StringBuilder stringBuilder = XRL.World.Event.NewStringBuilder(null);
			foreach (KeyValuePair<string, int> keyValuePair in this.GetFinalStats(this.builder))
			{
				stringBuilder.Compound(Statistic.GetStatCapitalizedDisplayName(keyValuePair.Key), '\n').Append(": ").Append(keyValuePair.Value);
			}
			return new SummaryBlockData
			{
				Id = base.GetType().FullName,
				Title = "Attributes",
				Description = stringBuilder.ToString(),
				SortOrder = -100
			};
		}
	}
}
