using System;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
	/// This part is not used in the base game.
	// Token: 0x02001066 RID: 4198
	[Serializable]
	public class MentalScreen : IPoweredPart
	{
		// Token: 0x17000B04 RID: 2820
		// (get) Token: 0x0600A72B RID: 42795 RVA: 0x003D164C File Offset: 0x003CF84C
		public int IncludeBitTypes
		{
			get
			{
				if (this._IncludeBitTypes == -2147483648)
				{
					this._IncludeBitTypes = Mental.TypeExpansion(this.IncludeTypes);
				}
				return this._IncludeBitTypes;
			}
		}

		// Token: 0x17000B05 RID: 2821
		// (get) Token: 0x0600A72C RID: 42796 RVA: 0x003D1672 File Offset: 0x003CF872
		public int ExcludeBitTypes
		{
			get
			{
				if (this._ExcludeBitTypes == -2147483648)
				{
					this._ExcludeBitTypes = Mental.TypeExpansion(this.IncludeTypes);
				}
				return this._ExcludeBitTypes;
			}
		}

		// Token: 0x0600A72D RID: 42797 RVA: 0x003D1698 File Offset: 0x003CF898
		public MentalScreen()
		{
			this.WorksOnEquipper = true;
		}

		// Token: 0x0600A72E RID: 42798 RVA: 0x003D16C8 File Offset: 0x003CF8C8
		public override bool SameAs(IPart p)
		{
			MentalScreen mentalScreen = p as MentalScreen;
			return !(mentalScreen.LinearReduction != this.LinearReduction) && !(mentalScreen.PercentageReduction != this.PercentageReduction) && !(mentalScreen.IncludeTypes != this.IncludeTypes) && !(mentalScreen.ExcludeTypes != this.ExcludeTypes) && mentalScreen.ComputePowerFactor == this.ComputePowerFactor && base.SameAs(p);
		}

		// Token: 0x0600A72F RID: 42799 RVA: 0x003D1747 File Offset: 0x003CF947
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetShortDescriptionEvent.ID || ID == BeforeMentalDefendEvent.ID;
		}

		// Token: 0x0600A730 RID: 42800 RVA: 0x003D1768 File Offset: 0x003CF968
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if ((this.WorksOnEquipper || this.WorksOnWearer || this.WorksOnHolder || this.WorksOnCarrier || this.WorksOnImplantee) && (!string.IsNullOrEmpty(this.PercentageReduction) || !string.IsNullOrEmpty(this.LinearReduction)))
			{
				E.Postfix.Append("\n{{rules|");
				bool flag = false;
				bool flag2 = false;
				if (!string.IsNullOrEmpty(this.PercentageReduction))
				{
					int num = this.PercentageReduction.RollMin();
					int num2 = this.PercentageReduction.RollMax();
					if (num >= 0 && num2 > 0)
					{
						E.Postfix.Append("Provides ").Append(num);
						if (num != num2)
						{
							E.Postfix.Append('-').Append(num2);
						}
						E.Postfix.Append('%');
						flag = true;
					}
					else if (num < 0 && num2 <= 0)
					{
						E.Postfix.Append("Causes ").Append(-num2).Append('%');
						if (num != num2)
						{
							E.Postfix.Append(" to ").Append(-num).Append('%');
						}
						flag2 = true;
					}
					else if (num != 0 || num2 != 0)
					{
						E.Postfix.Append("Confers ").Append(num).Append('%');
						if (num != num2)
						{
							E.Postfix.Append(" to ").Append(num2).Append('%');
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
							E.Postfix.Append(" vulnerability");
						}
						E.Postfix.Append((flag || flag2) ? " plus " : "Provides ");
						E.Postfix.Append(num3);
						if (num3 != num4)
						{
							E.Postfix.Append('-').Append(num4);
						}
						E.Postfix.Append(' ').Append((num3 != 1 || num4 != 1) ? "points" : "point");
						if (!flag && !flag2)
						{
							E.Postfix.Append(" of");
						}
						E.Postfix.Append(" resistance");
						flag = true;
					}
					else if (num3 < 0 && num4 <= 0)
					{
						if (flag)
						{
							E.Postfix.Append(" resistance");
						}
						E.Postfix.Append((flag || flag2) ? " minus " : "Causes ");
						E.Postfix.Append(-num4);
						if (num3 != num4)
						{
							E.Postfix.Append('-').Append(-num3);
						}
						E.Postfix.Append(' ').Append((num3 != -1 || num4 != -1) ? "points" : "point");
						if (!flag && !flag2)
						{
							E.Postfix.Append(" of");
						}
						E.Postfix.Append(" vulnerability");
						flag2 = true;
					}
					else if (num3 != 0 || num4 != 0)
					{
						if (!flag || !flag2)
						{
							if (flag)
							{
								E.Postfix.Append(" resistance");
							}
							if (flag2)
							{
								E.Postfix.Append(" vulnerability");
							}
						}
						E.Postfix.Append((flag || flag2) ? " plus " : "Confers ");
						E.Postfix.Append(num3);
						if (num3 != num4)
						{
							E.Postfix.Append(" to ").Append(num4);
						}
						E.Postfix.Append(' ').Append((num3 != num4 && num4 != 1 && num4 != -1) ? "points" : "point");
						if (!flag && !flag2)
						{
							E.Postfix.Append(" of");
						}
						E.Postfix.Append(" resistance/vulnerability");
						flag = true;
						flag2 = true;
					}
				}
				else if (flag && flag2)
				{
					E.Postfix.Append(" resistance/vulnerability");
				}
				else
				{
					if (flag)
					{
						E.Postfix.Append(" resistance");
					}
					if (flag2)
					{
						E.Postfix.Append(" vulnerability");
					}
				}
				if (flag || flag2)
				{
					E.Postfix.Append(" to ");
					if (!string.IsNullOrEmpty(this.IncludeTypes))
					{
						E.Postfix.Append("some forms of ");
					}
					else if (!string.IsNullOrEmpty(this.ExcludeTypes))
					{
						E.Postfix.Append("most forms of ");
					}
					E.Postfix.Append("mental intrusion.");
				}
				base.AddStatusSummary(E.Postfix);
				E.Postfix.Append("}}");
			}
			E.Postfix.AppendRules("Compute power on the local lattice increases this item's effectiveness.");
			return base.HandleEvent(E);
		}

		// Token: 0x0600A731 RID: 42801 RVA: 0x003D1C48 File Offset: 0x003CFE48
		public override bool HandleEvent(BeforeMentalDefendEvent E)
		{
			if (this.IsObjectActivePartSubject(E.Defender) && base.WasReady() && this.Applicable(E.Type) && E.Magnitude != -2147483648)
			{
				if (!string.IsNullOrEmpty(this.PercentageReduction))
				{
					E.Magnitude = E.Magnitude * (100 - GetAvailableComputePowerEvent.AdjustUp(this, this.PercentageReduction.RollCached(), this.ComputePowerFactor)) / 100;
				}
				if (!string.IsNullOrEmpty(this.LinearReduction))
				{
					E.Magnitude -= GetAvailableComputePowerEvent.AdjustUp(this, this.LinearReduction.RollCached(), this.ComputePowerFactor);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A732 RID: 42802 RVA: 0x003D1CFB File Offset: 0x003CFEFB
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600A733 RID: 42803 RVA: 0x003D1CFE File Offset: 0x003CFEFE
		private bool Applicable(int Type)
		{
			return this.IncludeBitTypes.HasBit(Type) && !this.ExcludeBitTypes.HasBit(Type);
		}

		// Token: 0x0600A734 RID: 42804 RVA: 0x003D1D1F File Offset: 0x003CFF1F
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600A735 RID: 42805 RVA: 0x003D1D24 File Offset: 0x003CFF24
		public override void TurnTick(long TimeTick, int Amount)
		{
			if (!base.OnWorldMap)
			{
				base.ConsumeChargeIfOperational(false, false, false, false, false, false, false, false, false, Amount, null, false, 0, false, null);
			}
		}

		// Token: 0x04004504 RID: 17668
		public string PercentageReduction;

		// Token: 0x04004505 RID: 17669
		public string LinearReduction;

		// Token: 0x04004506 RID: 17670
		public string IncludeTypes = "Psionic";

		// Token: 0x04004507 RID: 17671
		public string ExcludeTypes;

		// Token: 0x04004508 RID: 17672
		public float ComputePowerFactor;

		// Token: 0x04004509 RID: 17673
		private int _IncludeBitTypes = int.MinValue;

		// Token: 0x0400450A RID: 17674
		private int _ExcludeBitTypes = int.MinValue;
	}
}
