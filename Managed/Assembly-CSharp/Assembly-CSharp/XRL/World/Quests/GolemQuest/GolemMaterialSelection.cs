using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleLib.Console;
using Qud.UI;
using XRL.UI;
using XRL.World.Parts;
using XRL.World.Units;

namespace XRL.World.Quests.GolemQuest
{
	// Token: 0x02000B31 RID: 2865
	[Serializable]
	public abstract class GolemMaterialSelection<M, S> : GolemQuestSelection
	{
		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x06007385 RID: 29573
		// (set) Token: 0x06007386 RID: 29574
		public abstract M Material { get; set; }

		// Token: 0x06007387 RID: 29575 RVA: 0x002B9573 File Offset: 0x002B7773
		public override bool IsValid()
		{
			return this.IsValid(this.Material);
		}

		// Token: 0x06007388 RID: 29576 RVA: 0x002B9581 File Offset: 0x002B7781
		public virtual bool IsValid(M Material)
		{
			return Material != null;
		}

		// Token: 0x06007389 RID: 29577
		public abstract List<M> GetValidMaterials();

		// Token: 0x0600738A RID: 29578
		public abstract string GetNameFor(M Material);

		// Token: 0x0600738B RID: 29579
		public abstract IRenderable GetIconFor(M Material);

		// Token: 0x0600738C RID: 29580 RVA: 0x002B958C File Offset: 0x002B778C
		public virtual IEnumerable<GameObjectUnit> YieldEffectsOf(M Material)
		{
			return Enumerable.Empty<GameObjectUnit>();
		}

		// Token: 0x0600738D RID: 29581 RVA: 0x002B9593 File Offset: 0x002B7793
		public override IEnumerable<GameObjectUnit> YieldAllEffects()
		{
			if (GolemMaterialSelection<M, S>.Units == null)
			{
				yield break;
			}
			foreach (KeyValuePair<S, GolemMaterialSelection<M, S>.UnitGenerator> keyValuePair in GolemMaterialSelection<M, S>.Units)
			{
				foreach (GameObjectUnit gameObjectUnit in keyValuePair.Value(default(M)))
				{
					yield return gameObjectUnit;
				}
				IEnumerator<GameObjectUnit> enumerator2 = null;
			}
			Dictionary<S, GolemMaterialSelection<M, S>.UnitGenerator>.Enumerator enumerator = default(Dictionary<S, GolemMaterialSelection<M, S>.UnitGenerator>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x0600738E RID: 29582 RVA: 0x002B959C File Offset: 0x002B779C
		public override IEnumerable<GameObjectUnit> YieldRandomEffects()
		{
			if (GolemMaterialSelection<M, S>.Units == null)
			{
				return Enumerable.Empty<GameObjectUnit>();
			}
			S[] array = new S[GolemMaterialSelection<M, S>.Units.Count];
			array.Fill<S>(GolemMaterialSelection<M, S>.Units.Keys);
			S randomElement = array.GetRandomElement<S>(null);
			return GolemMaterialSelection<M, S>.Units[randomElement](default(M));
		}

		// Token: 0x0600738F RID: 29583 RVA: 0x002B95F5 File Offset: 0x002B77F5
		public override string GetOptionChoice()
		{
			if (this.Material != null)
			{
				return this.GetNameFor(this.Material);
			}
			return null;
		}

		// Token: 0x06007390 RID: 29584 RVA: 0x002B9614 File Offset: 0x002B7814
		public override void Pick()
		{
			List<M> validMaterials = this.GetValidMaterials();
			if (validMaterials.IsNullOrEmpty<M>())
			{
				Popup.ShowFail("You have nothing that meets the requirement of the " + this.DisplayName + ".", true, true, true);
				return;
			}
			int num = 0;
			int num2 = 250;
			int num3 = Math.Min(validMaterials.Count, num2);
			List<QudMenuItem> list = new List<QudMenuItem>(2);
			QudMenuItem qudMenuItem = new QudMenuItem
			{
				text = "Previous Page",
				command = "option:-2"
			};
			QudMenuItem qudMenuItem2 = new QudMenuItem
			{
				text = "Next Page",
				command = "option:-3"
			};
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			List<string> list2 = new List<string>(num3);
			List<IRenderable> list3 = new List<IRenderable>(num3);
			int num5;
			for (;;)
			{
				list2.Clear();
				list3.Clear();
				int num4 = Math.Min(validMaterials.Count - num, num2);
				for (int i = num; i < num + num4; i++)
				{
					stringBuilder.Clear().Append(this.GetNameFor(validMaterials[i]));
					this.AppendEffectsOf(validMaterials[i], stringBuilder, 0);
					list2.Add(stringBuilder.ToString());
					list3.Add(this.GetIconFor(validMaterials[i]));
				}
				list.Clear();
				if (num > 0)
				{
					list.Add(qudMenuItem);
				}
				if (validMaterials.Count - num > num2)
				{
					list.Add(qudMenuItem2);
				}
				num5 = Popup.PickOption(base.Title, null, "", "Sounds/UI/ui_notification", list2, null, list3, list, null, null, null, 0, 60, 0, -1, true, true, false, true, false, null, null);
				if (num5 == -2)
				{
					num = Math.Max(num - num2, 0);
				}
				else
				{
					if (num5 != -3)
					{
						break;
					}
					num += num2;
				}
			}
			if (num5 >= 0)
			{
				this.Material = validMaterials[num + num5];
			}
		}

		// Token: 0x06007391 RID: 29585 RVA: 0x002B97D4 File Offset: 0x002B79D4
		public void AppendEffectsOf(M Material, StringBuilder SB, int Indent = 0)
		{
			if (Material == null)
			{
				return;
			}
			bool flag = true;
			foreach (GameObjectUnit gameObjectUnit in this.YieldEffectsOf(Material))
			{
				string description = gameObjectUnit.GetDescription(false);
				int num = 0;
				int num2 = description.IndexOf('\n');
				if (SB.Length != 0)
				{
					SB.Append('\n');
				}
				SB.Append('ÿ', Indent).Append(flag ? "{{rules|--}}" : "{{rules|OR}}").Append(" {{rules|");
				if (num2 == -1)
				{
					SB.Append(description);
				}
				else
				{
					do
					{
						SB.Append(description, num, num2 - num + 1).Append('ÿ', 3 + Indent);
						num = num2 + 1;
						num2 = description.IndexOf('\n', num);
					}
					while (num2 != -1);
					SB.Append(description, num, description.Length - num);
				}
				SB.Append("}}");
				flag = false;
			}
		}

		// Token: 0x06007392 RID: 29586 RVA: 0x002B98DC File Offset: 0x002B7ADC
		public override void AppendEffects(StringBuilder SB, int Indent = 0)
		{
			this.AppendEffectsOf(this.Material, SB, Indent);
		}

		// Token: 0x06007393 RID: 29587 RVA: 0x002B98EC File Offset: 0x002B7AEC
		public override void Apply(GameObject Object)
		{
			RulesDescription rulesDescription = Object.RequirePart<RulesDescription>(false);
			StringBuilder stringBuilder = Event.NewStringBuilder(rulesDescription.Text);
			if (GolemQuestSelection.WISH_ALL)
			{
				using (IEnumerator<GameObjectUnit> enumerator = this.YieldAllEffects().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						GameObjectUnit gameObjectUnit = enumerator.Current;
						gameObjectUnit.Apply(Object);
						this.AppendUnitDesc(stringBuilder, gameObjectUnit);
					}
					goto IL_00C3;
				}
			}
			if (GolemQuestSelection.WISH_RANDOM)
			{
				using (IEnumerator<GameObjectUnit> enumerator = this.YieldRandomEffects().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						GameObjectUnit gameObjectUnit2 = enumerator.Current;
						gameObjectUnit2.Apply(Object);
						this.AppendUnitDesc(stringBuilder, gameObjectUnit2);
					}
					goto IL_00C3;
				}
			}
			GameObjectUnit randomElement = this.YieldEffectsOf(this.Material).ToArray<GameObjectUnit>().GetRandomElement<GameObjectUnit>(null);
			if (randomElement != null)
			{
				randomElement.Apply(Object);
				this.AppendUnitDesc(stringBuilder, randomElement);
			}
			IL_00C3:
			rulesDescription.Text = Event.FinalizeString(stringBuilder);
		}

		// Token: 0x06007394 RID: 29588 RVA: 0x002B99E4 File Offset: 0x002B7BE4
		private void AppendUnitDesc(StringBuilder SB, GameObjectUnit Unit)
		{
			if (Unit.CanInscribe())
			{
				SB.Append('\n').Append(Unit.GetDescription(true));
			}
		}

		// Token: 0x0400353F RID: 13631
		[NonSerialized]
		public static Dictionary<S, GolemMaterialSelection<M, S>.UnitGenerator> Units;

		// Token: 0x02001BE5 RID: 7141
		// (Invoke) Token: 0x0600EF51 RID: 61265
		public delegate IEnumerable<GameObjectUnit> UnitGenerator(M Material);
	}
}
