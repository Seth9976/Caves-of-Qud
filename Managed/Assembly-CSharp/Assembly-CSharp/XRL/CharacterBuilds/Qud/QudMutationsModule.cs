using System;
using System.Collections.Generic;
using System.Text;
using XRL.CharacterBuilds.Qud.UI;
using XRL.UI.Framework;
using XRL.World;
using XRL.World.Parts;
using XRL.World.Parts.Mutation;

namespace XRL.CharacterBuilds.Qud
{
	// Token: 0x020005A9 RID: 1449
	public class QudMutationsModule : QudEmbarkBuilderModule<QudMutationsModuleData>
	{
		// Token: 0x06003DCF RID: 15823 RVA: 0x0012F898 File Offset: 0x0012DA98
		public override bool shouldBeEnabled()
		{
			if ((int)this.builder.handleUIEvent(QudMutationsModuleWindow.EID_GET_BASE_MP, 0) > 0)
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
			return false;
		}

		// Token: 0x06003DD0 RID: 15824 RVA: 0x0012F8F2 File Offset: 0x0012DAF2
		public override bool shouldBeEditable()
		{
			return this.builder.IsEditableGameMode();
		}

		// Token: 0x06003DD1 RID: 15825 RVA: 0x0012F8FF File Offset: 0x0012DAFF
		public override string DataWarnings()
		{
			if (base.data.mp > 0)
			{
				return "You have unspent mutation points.";
			}
			return base.DataWarnings();
		}

		// Token: 0x06003DD2 RID: 15826 RVA: 0x0012F91B File Offset: 0x0012DB1B
		public override string DataErrors()
		{
			if (base.data.mp < 0)
			{
				return "You have spent too many mutation points.";
			}
			return base.DataErrors();
		}

		// Token: 0x06003DD3 RID: 15827 RVA: 0x0012F937 File Offset: 0x0012DB37
		public override void handleModuleDataChange(AbstractEmbarkBuilderModule module, AbstractEmbarkBuilderModuleData oldValues, AbstractEmbarkBuilderModuleData newValues)
		{
			base.handleModuleDataChange(module, oldValues, newValues);
		}

		// Token: 0x06003DD4 RID: 15828 RVA: 0x0012F944 File Offset: 0x0012DB44
		public override SummaryBlockData GetSummaryBlock()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (base.data != null)
			{
				using (List<QudMutationModuleDataRow>.Enumerator enumerator = base.data.selections.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						QudMutationModuleDataRow qudMutationModuleDataRow = enumerator.Current;
						if (qudMutationModuleDataRow.Count > 0)
						{
							stringBuilder.Append(qudMutationModuleDataRow.DisplayName);
							if (qudMutationModuleDataRow.Count > 1)
							{
								stringBuilder.AppendFormat("x{0}", qudMutationModuleDataRow.Count);
							}
							stringBuilder.AppendLine();
						}
					}
					goto IL_008B;
				}
			}
			stringBuilder.AppendLine("none");
			IL_008B:
			return new SummaryBlockData
			{
				Title = "Mutations",
				Description = stringBuilder.ToString(),
				SortOrder = 100
			};
		}

		// Token: 0x06003DD5 RID: 15829 RVA: 0x0012FA10 File Offset: 0x0012DC10
		public override void InitFromSeed(string seed)
		{
		}

		// Token: 0x06003DD6 RID: 15830 RVA: 0x0012FA14 File Offset: 0x0012DC14
		public override object handleBootEvent(string id, XRLGame game, EmbarkInfo info, object element = null)
		{
			if (id == QudGameBootModule.BOOTEVENT_BOOTPLAYEROBJECT && base.data != null)
			{
				GameObject gameObject = element as GameObject;
				Mutations mutations = gameObject.RequirePart<Mutations>(false);
				foreach (QudMutationModuleDataRow qudMutationModuleDataRow in base.data.selections)
				{
					if (qudMutationModuleDataRow.Entry.Mutation != null)
					{
						BaseMutation baseMutation = qudMutationModuleDataRow.Entry.CreateInstance();
						baseMutation.SetVariant(qudMutationModuleDataRow.Variant);
						mutations.AddMutation(baseMutation, qudMutationModuleDataRow.Count, true);
					}
					else if (qudMutationModuleDataRow.Entry.Name == "Chimera")
					{
						gameObject.SetStringProperty("MutationLevel", "Chimera", false);
					}
					else if (qudMutationModuleDataRow.Entry.Name == "Esper")
					{
						gameObject.SetStringProperty("MutationLevel", "Esper", false);
					}
				}
			}
			return base.handleBootEvent(id, game, info, element);
		}
	}
}
