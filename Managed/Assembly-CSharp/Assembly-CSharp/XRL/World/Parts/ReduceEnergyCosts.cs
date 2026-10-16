using System;
using System.Collections.Generic;
using System.Text;
using XRL.Language;

namespace XRL.World.Parts
{
	// Token: 0x0200108C RID: 4236
	[Serializable]
	public class ReduceEnergyCosts : IPoweredPart
	{
		// Token: 0x0600A898 RID: 43160 RVA: 0x003DA8DF File Offset: 0x003D8ADF
		public ReduceEnergyCosts()
		{
			this.WorksOnEquipper = true;
		}

		// Token: 0x0600A899 RID: 43161 RVA: 0x003DA8F8 File Offset: 0x003D8AF8
		public override bool SameAs(IPart p)
		{
			ReduceEnergyCosts reduceEnergyCosts = p as ReduceEnergyCosts;
			return !(reduceEnergyCosts.LinearReduction != this.LinearReduction) && !(reduceEnergyCosts.PercentageReduction != this.PercentageReduction) && !(reduceEnergyCosts.IncludeTypes != this.IncludeTypes) && !(reduceEnergyCosts.ExcludeTypes != this.ExcludeTypes) && reduceEnergyCosts.UsesChargePerTurn == this.UsesChargePerTurn && reduceEnergyCosts.UsesChargePerEffect == this.UsesChargePerEffect && reduceEnergyCosts.GenerateShortDescription == this.GenerateShortDescription && base.SameAs(p);
		}

		// Token: 0x0600A89A RID: 43162 RVA: 0x003DA997 File Offset: 0x003D8B97
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<GetEnergyCostEvent>.ID || (ID == GetShortDescriptionEvent.ID && this.GenerateShortDescription);
		}

		// Token: 0x0600A89B RID: 43163 RVA: 0x003DA9C0 File Offset: 0x003D8BC0
		public override bool HandleEvent(GetEnergyCostEvent E)
		{
			if (this.Applicable(E.Type) && this.IsObjectActivePartSubject(E.Actor) && base.IsReady(this.UsesChargePerEffect, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				if (!string.IsNullOrEmpty(this.PercentageReduction))
				{
					E.PercentageReduction += this.PercentageReduction.RollCached();
				}
				if (!string.IsNullOrEmpty(this.LinearReduction))
				{
					E.LinearReduction += this.LinearReduction.RollCached();
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A89C RID: 43164 RVA: 0x003DAA6C File Offset: 0x003D8C6C
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (this.GenerateShortDescription && (this.WorksOnEquipper || this.WorksOnWearer || this.WorksOnHolder || this.WorksOnCarrier || this.WorksOnImplantee || this.WorksOnSelf) && (!string.IsNullOrEmpty(this.PercentageReduction) || !string.IsNullOrEmpty(this.LinearReduction)))
			{
				StringBuilder postfix = E.Postfix;
				postfix.Append("\n{{rules|");
				bool flag = false;
				bool flag2 = false;
				bool flag3 = (this.UsesChargePerTurn || this.UsesChargePerEffect) && this.ChargeUse > 0;
				if (flag3)
				{
					postfix.Append("When powered, ");
				}
				if (!string.IsNullOrEmpty(this.PercentageReduction))
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
				if (!string.IsNullOrEmpty(this.LinearReduction))
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
					postfix.Append(" in ");
					if (!string.IsNullOrEmpty(this.ScopeDescription))
					{
						postfix.Append(this.ScopeDescription);
					}
					else if (!string.IsNullOrEmpty(this.IncludeTypes))
					{
						postfix.Append("the action costs of ").Append(Grammar.MakeAndList(this.IncludeTypes.CachedCommaExpansion(), true)).Append(" actions");
						if (!string.IsNullOrEmpty(this.ExcludeTypes))
						{
							postfix.Append(", except for ").Append(Grammar.MakeAndList(this.ExcludeTypes.CachedCommaExpansion(), true)).Append(" ones");
						}
					}
					else if (!string.IsNullOrEmpty(this.ExcludeTypes))
					{
						postfix.Append("the action costs of actions other than ").Append(Grammar.MakeAndList(this.ExcludeTypes.CachedCommaExpansion(), true)).Append(" ones");
					}
					else
					{
						postfix.Append("action costs");
					}
					postfix.Append('.');
				}
				if (!E.NoStatus)
				{
					base.AddStatusSummary(postfix);
				}
				postfix.Append("}}");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A89D RID: 43165 RVA: 0x003DAFBC File Offset: 0x003D91BC
		private bool Applicable(string Type)
		{
			if (!string.IsNullOrEmpty(this.IncludeTypes))
			{
				if (Type == null)
				{
					return false;
				}
				List<string> list = this.IncludeTypes.CachedCommaExpansion();
				bool flag = false;
				int i = 0;
				int count = list.Count;
				while (i < count)
				{
					if (Type.Contains(list[i]))
					{
						flag = true;
						break;
					}
					i++;
				}
				if (!flag)
				{
					return false;
				}
			}
			if (Type != null && !string.IsNullOrEmpty(this.ExcludeTypes))
			{
				List<string> list2 = this.ExcludeTypes.CachedCommaExpansion();
				int j = 0;
				int count2 = list2.Count;
				while (j < count2)
				{
					if (Type.Contains(list2[j]))
					{
						return false;
					}
					j++;
				}
			}
			return true;
		}

		// Token: 0x0600A89E RID: 43166 RVA: 0x003DB05F File Offset: 0x003D925F
		public override bool WantTurnTick()
		{
			return this.UsesChargePerTurn;
		}

		// Token: 0x0600A89F RID: 43167 RVA: 0x003DB068 File Offset: 0x003D9268
		public override void TurnTick(long TimeTick, int Amount)
		{
			if (this.UsesChargePerTurn && !base.OnWorldMap)
			{
				base.ConsumeChargeIfOperational(false, false, false, false, false, false, false, false, false, Amount, null, false, 0, false, null);
			}
		}

		// Token: 0x0600A8A0 RID: 43168 RVA: 0x003DB0AB File Offset: 0x003D92AB
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x040045AE RID: 17838
		public string PercentageReduction;

		// Token: 0x040045AF RID: 17839
		public string LinearReduction;

		// Token: 0x040045B0 RID: 17840
		public string IncludeTypes;

		// Token: 0x040045B1 RID: 17841
		public string ExcludeTypes;

		// Token: 0x040045B2 RID: 17842
		public string ScopeDescription;

		// Token: 0x040045B3 RID: 17843
		public bool UsesChargePerTurn;

		// Token: 0x040045B4 RID: 17844
		public bool UsesChargePerEffect;

		// Token: 0x040045B5 RID: 17845
		public bool GenerateShortDescription = true;
	}
}
