using System;
using System.Collections.Generic;
using System.Text;
using ConsoleLib.Console;
using XRL.Language;

namespace XRL.World.Parts
{
	// Token: 0x0200108B RID: 4235
	[Serializable]
	public class ReduceCooldowns : IPoweredPart
	{
		// Token: 0x0600A890 RID: 43152 RVA: 0x003DA150 File Offset: 0x003D8350
		public ReduceCooldowns()
		{
			this.WorksOnEquipper = true;
		}

		// Token: 0x0600A891 RID: 43153 RVA: 0x003DA160 File Offset: 0x003D8360
		public override bool SameAs(IPart p)
		{
			ReduceCooldowns reduceCooldowns = p as ReduceCooldowns;
			return !(reduceCooldowns.LinearReduction != this.LinearReduction) && !(reduceCooldowns.PercentageReduction != this.PercentageReduction) && !(reduceCooldowns.IncludeAbilities != this.IncludeAbilities) && !(reduceCooldowns.ExcludeAbilities != this.ExcludeAbilities) && reduceCooldowns.UsesChargePerTurn == this.UsesChargePerTurn && reduceCooldowns.UsesChargePerEffect == this.UsesChargePerEffect && base.SameAs(p);
		}

		// Token: 0x0600A892 RID: 43154 RVA: 0x003DA1EF File Offset: 0x003D83EF
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetCooldownEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600A893 RID: 43155 RVA: 0x003DA210 File Offset: 0x003D8410
		public override bool HandleEvent(GetCooldownEvent E)
		{
			if (this.Applicable(E.Ability) && this.IsObjectActivePartSubject(E.Actor) && base.IsReady(this.UsesChargePerEffect && !E.StoreCalculations, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				int num = 0;
				int num2 = 0;
				if (!this.PercentageReduction.IsNullOrEmpty())
				{
					num = this.PercentageReduction.RollCached();
					E.PercentageReduction += num;
				}
				if (!this.LinearReduction.IsNullOrEmpty())
				{
					num2 = this.LinearReduction.RollCached();
					E.LinearReduction += num2;
				}
				if (E.StoreCalculations)
				{
					E.AddCalculation(this.ParentObject.GetDisplayName(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, false, false, null, false, false, false, false, null, false, null, false, true), num, num2);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A894 RID: 43156 RVA: 0x003DA318 File Offset: 0x003D8518
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if ((this.WorksOnEquipper || this.WorksOnWearer || this.WorksOnHolder || this.WorksOnCarrier || this.WorksOnImplantee) && (!this.PercentageReduction.IsNullOrEmpty() || !this.LinearReduction.IsNullOrEmpty()))
			{
				StringBuilder postfix = E.Postfix;
				postfix.Append("\n{{rules|");
				bool flag = false;
				bool flag2 = false;
				bool flag3 = this.ChargeMinimum > 0 || ((this.UsesChargePerTurn || this.UsesChargePerEffect) && this.ChargeUse > 0);
				if (flag3)
				{
					postfix.Append("When powered, ");
				}
				if (!this.PercentageReduction.IsNullOrEmpty())
				{
					int num = this.PercentageReduction.RollMin();
					int num2 = this.PercentageReduction.RollMax();
					if (num >= 0 && num2 > 0)
					{
						postfix.Append(flag3 ? "provides " : "Provides ").Append(num);
						if (num != num2)
						{
							postfix.Append('-').Append(num2);
						}
						postfix.Append('%');
						flag = true;
					}
					else if (num < 0 && num2 <= 0)
					{
						postfix.Append(flag3 ? "causes " : "Causes ").Append(-num2).Append('%');
						if (num != num2)
						{
							postfix.Append(" to ").Append(-num).Append('%');
						}
						flag2 = true;
					}
					else if (num != 0 || num2 != 0)
					{
						postfix.Append(flag3 ? "confers " : "Confers ").Append(num).Append('%');
						if (num != num2)
						{
							postfix.Append(" to ").Append(num2).Append('%');
						}
						flag = true;
						flag2 = true;
					}
				}
				if (!this.LinearReduction.IsNullOrEmpty())
				{
					int num3 = this.LinearReduction.RollMin();
					int num4 = this.LinearReduction.RollMax();
					if (num3 >= 0 && num4 > 0)
					{
						if (flag2)
						{
							postfix.Append(" increase");
						}
						postfix.Append((flag || flag2) ? " plus " : (flag3 ? "provides " : "Provides "));
						postfix.Append(num3);
						if (num3 != num4)
						{
							postfix.Append('-').Append(num4);
						}
						postfix.Append(' ').Append((num3 != 1 || num4 != 1) ? "points" : "point");
						if (!flag && !flag2)
						{
							postfix.Append(" of");
						}
						postfix.Append(" reduction");
						flag = true;
					}
					else if (num3 < 0 && num4 <= 0)
					{
						if (flag)
						{
							postfix.Append(" reduction");
						}
						postfix.Append((flag || flag2) ? " minus " : (flag3 ? "causes " : "Causes "));
						postfix.Append(-num4);
						if (num3 != num4)
						{
							postfix.Append('-').Append(-num3);
						}
						postfix.Append(' ').Append((num3 != -1 || num4 != -1) ? "points" : "point");
						if (!flag && !flag2)
						{
							postfix.Append(" of");
						}
						postfix.Append(" increase");
						flag2 = true;
					}
					else if (num3 != 0 || num4 != 0)
					{
						if (!flag || !flag2)
						{
							if (flag)
							{
								postfix.Append(" reduction");
							}
							if (flag2)
							{
								postfix.Append(" increase");
							}
						}
						postfix.Append((flag || flag2) ? " plus " : (flag3 ? "confers " : "Confers "));
						postfix.Append(num3);
						if (num3 != num4)
						{
							postfix.Append(" to ").Append(num4);
						}
						postfix.Append(' ').Append((num3 != num4 && num4 != 1 && num4 != -1) ? "points" : "point");
						if (!flag && !flag2)
						{
							postfix.Append(" of");
						}
						postfix.Append(" reduction/increase");
						flag = true;
						flag2 = true;
					}
				}
				else if (flag && flag2)
				{
					postfix.Append(" reduction/increase");
				}
				else
				{
					if (flag)
					{
						postfix.Append(" reduction");
					}
					if (flag2)
					{
						postfix.Append(" increase");
					}
				}
				if (flag || flag2)
				{
					postfix.Append(" in the ");
					if (!this.IncludeAbilities.IsNullOrEmpty())
					{
						List<string> list = this.IncludeAbilities.CachedCommaExpansion();
						postfix.Append((list.Count == 1) ? "cooldown of the ability " : "cooldowns of the abilities ").Append(ColorUtility.StripFormatting((list.Count == 1) ? list[0] : Grammar.MakeAndList(list, true)));
					}
					else if (!this.ExcludeAbilities.IsNullOrEmpty())
					{
						List<string> list2 = this.ExcludeAbilities.CachedCommaExpansion();
						postfix.Append("cooldowns of activated abilities other than ").Append(ColorUtility.StripFormatting((list2.Count == 1) ? list2[0] : Grammar.MakeAndList(list2, true)));
					}
					else
					{
						postfix.Append("cooldowns of activated abilities");
					}
					postfix.Append('.');
				}
				base.AddStatusSummary(postfix);
				postfix.Append("}}");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A895 RID: 43157 RVA: 0x003DA838 File Offset: 0x003D8A38
		private bool Applicable(ActivatedAbilityEntry Ability)
		{
			return (this.IncludeAbilities.IsNullOrEmpty() || this.IncludeAbilities.CachedCommaExpansion().Contains(Ability.DisplayName)) && (this.ExcludeAbilities.IsNullOrEmpty() || !this.ExcludeAbilities.CachedCommaExpansion().Contains(Ability.DisplayName));
		}

		// Token: 0x0600A896 RID: 43158 RVA: 0x003DA894 File Offset: 0x003D8A94
		public override bool WantTurnTick()
		{
			return this.UsesChargePerTurn;
		}

		// Token: 0x0600A897 RID: 43159 RVA: 0x003DA89C File Offset: 0x003D8A9C
		public override void TurnTick(long TimeTick, int Amount)
		{
			if (this.UsesChargePerTurn && !base.OnWorldMap)
			{
				base.ConsumeChargeIfOperational(false, false, false, false, false, false, false, false, false, Amount, null, false, 0, false, null);
			}
		}

		// Token: 0x040045A8 RID: 17832
		public string PercentageReduction;

		// Token: 0x040045A9 RID: 17833
		public string LinearReduction;

		// Token: 0x040045AA RID: 17834
		public string IncludeAbilities;

		// Token: 0x040045AB RID: 17835
		public string ExcludeAbilities;

		// Token: 0x040045AC RID: 17836
		public bool UsesChargePerTurn;

		// Token: 0x040045AD RID: 17837
		public bool UsesChargePerEffect;
	}
}
