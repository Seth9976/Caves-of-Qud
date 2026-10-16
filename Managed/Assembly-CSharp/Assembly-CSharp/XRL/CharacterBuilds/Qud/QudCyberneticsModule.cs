using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleLib.Console;
using XRL.UI.Framework;
using XRL.World;
using XRL.World.Anatomy;

namespace XRL.CharacterBuilds.Qud
{
	// Token: 0x020005A1 RID: 1441
	public class QudCyberneticsModule : QudEmbarkBuilderModule<QudCyberneticsModuleData>
	{
		// Token: 0x06003D94 RID: 15764 RVA: 0x0012DAB8 File Offset: 0x0012BCB8
		public override bool shouldBeEditable()
		{
			return this.builder.IsEditableGameMode();
		}

		// Token: 0x06003D95 RID: 15765 RVA: 0x0012DAC8 File Offset: 0x0012BCC8
		public override bool shouldBeEnabled()
		{
			QudGenotypeModule module = this.builder.GetModule<QudGenotypeModule>();
			bool flag;
			if (module == null)
			{
				flag = false;
			}
			else
			{
				QudGenotypeModuleData data = module.data;
				int? num;
				if (data == null)
				{
					num = null;
				}
				else
				{
					GenotypeEntry entry = data.Entry;
					num = ((entry != null) ? new int?(entry.CyberneticsLicensePoints) : null);
				}
				int? num2 = num;
				int num3 = 0;
				flag = (num2.GetValueOrDefault() > num3) & (num2 != null);
			}
			if (flag)
			{
				EmbarkBuilder builder = this.builder;
				object obj;
				if (builder == null)
				{
					obj = null;
				}
				else
				{
					QudSubtypeModule module2 = builder.GetModule<QudSubtypeModule>();
					if (module2 == null)
					{
						obj = null;
					}
					else
					{
						QudSubtypeModuleData data2 = module2.data;
						obj = ((data2 != null) ? data2.Subtype : null);
					}
				}
				return obj != null;
			}
			return false;
		}

		// Token: 0x06003D96 RID: 15766 RVA: 0x0012DB5F File Offset: 0x0012BD5F
		public override string DataErrors()
		{
			if (base.data != null && !this.cybernetics.Any<QudCyberneticsModule.CyberneticsChoice>(new Func<QudCyberneticsModule.CyberneticsChoice, bool>(this.IsSelected)))
			{
				return "Invalid choice selected";
			}
			return base.DataErrors();
		}

		// Token: 0x06003D97 RID: 15767 RVA: 0x0012DB90 File Offset: 0x0012BD90
		public override void handleModuleDataChange(AbstractEmbarkBuilderModule module, AbstractEmbarkBuilderModuleData oldValues, AbstractEmbarkBuilderModuleData newValues)
		{
			if (module is QudSubtypeModule || module is QudGenotypeModule)
			{
				this.cybernetics.Clear();
				IEnumerable<GameObjectBlueprint> blueprintsWithTag = GameObjectFactory.Factory.GetBlueprintsWithTag("StartingCybernetic:General", true);
				GameObjectFactory factory = GameObjectFactory.Factory;
				string text = "StartingCybernetic:";
				QudSubtypeModuleData data = this.builder.GetModule<QudSubtypeModule>().data;
				string text2;
				if (data == null)
				{
					text2 = null;
				}
				else
				{
					SubtypeEntry entry = data.Entry;
					text2 = ((entry != null) ? entry.Name : null);
				}
				IEnumerable<GameObjectBlueprint> enumerable = blueprintsWithTag.Concat<GameObjectBlueprint>(factory.GetBlueprintsWithTag(text + text2, true));
				GameObjectFactory factory2 = GameObjectFactory.Factory;
				string text3 = "StartingCybernetic:";
				QudGenotypeModuleData data2 = this.builder.GetModule<QudGenotypeModule>().data;
				string text4;
				if (data2 == null)
				{
					text4 = null;
				}
				else
				{
					GenotypeEntry entry2 = data2.Entry;
					text4 = ((entry2 != null) ? entry2.Name : null);
				}
				IEnumerable<QudCyberneticsModule.CyberneticsChoice> enumerable2 = enumerable.Concat<GameObjectBlueprint>(factory2.GetBlueprintsWithTag(text3 + text4, true)).Distinct<GameObjectBlueprint>().SelectMany<GameObjectBlueprint, QudCyberneticsModule.CyberneticsChoice>((GameObjectBlueprint go) => from slot in go.GetPartParameter<string>("CyberneticsBaseItem", "Slots", null).Split(',', StringSplitOptions.None)
					select new QudCyberneticsModule.CyberneticsChoice
					{
						blueprint = go,
						slot = slot
					});
				this.cybernetics.AddRange(enumerable2);
				this.cybernetics.Add(new QudCyberneticsModule.CyberneticsChoice
				{
					blueprint = null,
					slot = null
				});
			}
		}

		// Token: 0x06003D98 RID: 15768 RVA: 0x0012DCA9 File Offset: 0x0012BEA9
		public override void InitFromSeed(string seed)
		{
		}

		// Token: 0x06003D99 RID: 15769 RVA: 0x0012DCAC File Offset: 0x0012BEAC
		public override object handleBootEvent(string id, XRLGame game, EmbarkInfo info, object element = null)
		{
			if (id == QudGameBootModule.BOOTEVENT_BOOTPLAYEROBJECT)
			{
				GameObject gameObject = element as GameObject;
				if (base.data == null || base.data.selections == null)
				{
					MetricsManager.LogWarning("Cybernetics module was active but data or selections was null");
					return element;
				}
				string text = ((base.data.selections.Count == 0) ? null : base.data.selections[0].Cybernetic);
				string text2 = ((base.data.selections.Count == 0) ? null : base.data.selections[0].Variant);
				if (text == null)
				{
					gameObject.SetIntProperty("CyberneticsLicenses", 0, false);
					Statistic statistic = gameObject.Statistics["Toughness"];
					int baseValue = statistic.BaseValue;
					statistic.BaseValue = baseValue + 1;
				}
				else
				{
					List<BodyPart> part = gameObject.Body.GetPart(text2);
					if (part.Count > 0)
					{
						GameObject gameObject2 = GameObject.Create(text);
						gameObject2.MakeUnderstood(false);
						part.GetRandomElement<BodyPart>(null).Implant(gameObject2, false, false);
					}
				}
			}
			return base.handleBootEvent(id, game, info, element);
		}

		// Token: 0x06003D9A RID: 15770 RVA: 0x0012DDC4 File Offset: 0x0012BFC4
		public bool IsSelected(QudCyberneticsModule.CyberneticsChoice c)
		{
			if (base.data == null)
			{
				return false;
			}
			if (base.data.selections.Count == 0)
			{
				return c.blueprint == null;
			}
			QudCyberneticsModuleDataRow qudCyberneticsModuleDataRow = base.data.selections[0];
			string cybernetic = qudCyberneticsModuleDataRow.Cybernetic;
			GameObjectBlueprint blueprint = c.blueprint;
			return cybernetic == ((blueprint != null) ? blueprint.Name : null) && qudCyberneticsModuleDataRow.Variant == c.slot;
		}

		// Token: 0x06003D9B RID: 15771 RVA: 0x0012DE3B File Offset: 0x0012C03B
		public QudCyberneticsModule.CyberneticsChoice SelectedChoice()
		{
			return this.cybernetics.Find(new Predicate<QudCyberneticsModule.CyberneticsChoice>(this.IsSelected));
		}

		// Token: 0x06003D9C RID: 15772 RVA: 0x0012DE54 File Offset: 0x0012C054
		public override SummaryBlockData GetSummaryBlock()
		{
			QudCyberneticsModule.CyberneticsChoice cyberneticsChoice = this.cybernetics.Find(new Predicate<QudCyberneticsModule.CyberneticsChoice>(this.IsSelected));
			cyberneticsChoice.GetRenderable();
			return new SummaryBlockData
			{
				Id = base.GetType().FullName,
				Title = "Cybernetics",
				Description = cyberneticsChoice.GetDescription(),
				SortOrder = 100
			};
		}

		// Token: 0x04001E85 RID: 7813
		protected static StringBuilder sb = new StringBuilder();

		// Token: 0x04001E86 RID: 7814
		public List<QudCyberneticsModule.CyberneticsChoice> cybernetics = new List<QudCyberneticsModule.CyberneticsChoice>();

		// Token: 0x02001995 RID: 6549
		public struct CyberneticsChoice
		{
			// Token: 0x0600E414 RID: 58388 RVA: 0x00529450 File Offset: 0x00527650
			public string GetDescription()
			{
				if (this.blueprint == null)
				{
					return "<none>";
				}
				QudCyberneticsModule.sb.Clear();
				QudCyberneticsModule.sb.Append(this.blueprint.GetPartParameter<string>("Render", "DisplayName", this.blueprint.Name));
				QudCyberneticsModule.sb.Append(" (");
				QudCyberneticsModule.sb.Append(this.slot);
				QudCyberneticsModule.sb.Append(")");
				return QudCyberneticsModule.sb.ToString();
			}

			// Token: 0x0600E415 RID: 58389 RVA: 0x005294DC File Offset: 0x005276DC
			public string GetLongDescription()
			{
				if (this.blueprint == null)
				{
					return "{{C|-2 License Tier\n+1 Toughness}}";
				}
				QudCyberneticsModule.sb.Clear();
				QudCyberneticsModule.sb.Append(this.blueprint.GetPartParameter<string>("Description", "Short", null));
				QudCyberneticsModule.sb.Append("\n\n");
				QudCyberneticsModule.sb.Append("{{rules|").Append(this.blueprint.GetPartParameter<string>("CyberneticsBaseItem", "BehaviorDescription", null)).Append("}}");
				return QudCyberneticsModule.sb.ToString();
			}

			// Token: 0x0600E416 RID: 58390 RVA: 0x00529572 File Offset: 0x00527772
			public Renderable GetRenderable()
			{
				if (this._renderable == null && this.blueprint != null)
				{
					this._renderable = new Renderable(this.blueprint);
				}
				return this._renderable;
			}

			// Token: 0x04006652 RID: 26194
			public GameObjectBlueprint blueprint;

			// Token: 0x04006653 RID: 26195
			public string slot;

			// Token: 0x04006654 RID: 26196
			private Renderable _renderable;
		}
	}
}
