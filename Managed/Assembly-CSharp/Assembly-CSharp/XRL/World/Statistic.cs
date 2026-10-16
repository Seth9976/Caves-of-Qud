using System;
using System.Collections.Generic;
using System.Text;
using XRL.Core;
using XRL.Rules;
using XRL.UI;

namespace XRL.World
{
	// Token: 0x0200096F RID: 2415
	[Serializable]
	public class Statistic
	{
		// Token: 0x060063DB RID: 25563 RVA: 0x001EC958 File Offset: 0x001EAB58
		public static bool IsMental(string Stat)
		{
			return Statistic.MentalStats.Contains(Stat);
		}

		// Token: 0x060063DC RID: 25564 RVA: 0x001EC965 File Offset: 0x001EAB65
		public static bool IsInverseBenefit(string Stat)
		{
			return Statistic.InverseBenefitStats.Contains(Stat);
		}

		// Token: 0x060063DD RID: 25565 RVA: 0x001EC974 File Offset: 0x001EAB74
		public static string GetStatTitleCaseShortNames(string Stat)
		{
			string text;
			if (!Statistic.StatTitleCaseShortNames.TryGetValue(Stat, out text))
			{
				return Stat;
			}
			return text;
		}

		// Token: 0x060063DE RID: 25566 RVA: 0x001EC994 File Offset: 0x001EAB94
		public static string GetStatShortName(string Stat)
		{
			string text;
			if (!Statistic.StatShortNames.TryGetValue(Stat, out text))
			{
				return Stat.Substring(0, 2).ToUpper();
			}
			return text;
		}

		// Token: 0x060063DF RID: 25567 RVA: 0x001EC9C0 File Offset: 0x001EABC0
		public static string GetStatDisplayName(string Stat)
		{
			string text;
			if (!Statistic.StatDisplayNames.TryGetValue(Stat, out text))
			{
				return Stat;
			}
			return text;
		}

		// Token: 0x060063E0 RID: 25568 RVA: 0x001EC9E0 File Offset: 0x001EABE0
		public static string GetStatCapitalizedDisplayName(string Stat)
		{
			string text;
			if (!Statistic.StatCapitalizedDisplayNames.TryGetValue(Stat, out text))
			{
				return Stat;
			}
			return text;
		}

		// Token: 0x060063E1 RID: 25569 RVA: 0x001EC9FF File Offset: 0x001EABFF
		public static bool IsStatPlural(string Stat)
		{
			return Statistic.StatPlurality.Contains(Stat);
		}

		// Token: 0x060063E2 RID: 25570 RVA: 0x001ECA0C File Offset: 0x001EAC0C
		public static string GetStatNarration(string Stat)
		{
			string text;
			if (!Statistic.StatNarration.TryGetValue(Stat, out text))
			{
				return Statistic.GetStatDisplayName(Stat);
			}
			return text;
		}

		// Token: 0x060063E3 RID: 25571 RVA: 0x001ECA30 File Offset: 0x001EAC30
		public string GetDisplayValue()
		{
			if (Statistic.InverseBenefitStats.Contains(this.Name))
			{
				return (200 - this.Value).ToStringCached();
			}
			return this.Value.ToStringCached();
		}

		// Token: 0x060063E4 RID: 25572 RVA: 0x001ECA64 File Offset: 0x001EAC64
		public string GetHelpText()
		{
			if (this.Name == "Strength")
			{
				return "Your {{W|Strength}} determines how much melee damage you do (by improving your armor penetration), your ability to resist forced movement, and your carry capacity.";
			}
			if (this.Name == "Agility")
			{
				return "Your {{W|Agility}} determines your accuracy with both melee and ranged weapons and your chance to dodge attacks.";
			}
			if (this.Name == "Toughness")
			{
				return "Your {{W|Toughness}} determines your number of hit points, your hit point regeneration rate, and your ability to resist poison and disease.";
			}
			if (this.Name == "Intelligence")
			{
				return "Your {{W|Intelligence}} determines your number of skill points and your ability to examine artifacts.";
			}
			if (this.Name == "Willpower")
			{
				return "Your {{W|Willpower}} modifies the cooldowns of your activated abilities, determines your ability to resist mental attacks, and modifies your hit point regeneration rate.";
			}
			if (this.Name == "Ego")
			{
				return "Your {{W|Ego}} determines the potency of your mental mutations, your ability to haggle with merchants, and your ability to dominate the wills of other living creatures.";
			}
			if (this.Name == "AV")
			{
				return "Your {{W|Armor Value (AV)}} measures how well protected you are against physical attacks that hit you. The higher your score, the fewer times an opponent's attack will penetrate your armor and do damage to you. Your base AV is 0.";
			}
			if (this.Name == "DV")
			{
				return "Your {{W|Dodge Value (DV)}} measures how likely you are to be hit by physical attacks. The higher your score, the less likely an opponent's attack will hit you. Your DV is modified by your Agility modifier. Your base DV is 6.";
			}
			if (this.Name == "MA")
			{
				return "Your {{W|mental armor (MA)}} measures how well protected you are against mental attacks. The higher your score, the less likely an opponent's mental attack will penetrate your defenses and harm you. Your MA is modified by your Willpower modifier. Your base MA is 4.";
			}
			if (this.Name == "Speed")
			{
				return "Your {{W|Quickness}} measures the speed you perform every action you take. Your base Quickness score is 100.";
			}
			if (this.Name == "MoveSpeed")
			{
				return "Your {{W|Move Speed}} measures how fast you walk or fly. Your base move speed is 100.";
			}
			if (this.Name == "T")
			{
				return "Your {{W|Temperature}} measures how hot or cold you are. Being too cold can reduce your Quickness and prevent you from taking physical actions. Being too hot can set you aflame and cause damage every round.";
			}
			if (this.Name == "AcidResistance")
			{
				return "Your {{W|Acid Resist}} determines how much acid damage you ablate. Your base acid resist score is 0. At 100, you are immune to acid damage.";
			}
			if (this.Name == "ColdResistance")
			{
				return "Your {{W|Cold Resist}} determines how much cold damage you ablate and how insulated you are from effects that reduce your temperature. Your base Cold Resist score is 0. At 100, you are immune to cold damage and your temperature cannot be reduced.";
			}
			if (this.Name == "ElectricResistance")
			{
				return "Your {{W|Electrical Resist}} determines how much electrical damage you ablate and how resistive you are to electric current. Your base Electrical Resist score is 0. At 100, you are immune to electrical damage and you do not conduct electricity.";
			}
			if (this.Name == "HeatResistance")
			{
				return "Your {{W|Heat Resist}} determines how much heat damage you ablate and how insulated you are from effects that increase your temperature. Your base Heat Resist score is 0. At 100, you are immune to heat damage and your temperature cannot be increased.";
			}
			return "";
		}

		// Token: 0x060063E5 RID: 25573 RVA: 0x001ECBF6 File Offset: 0x001EADF6
		[Obsolete("Use AppendStatAdjustDescription")]
		public static void GetStatAdjustDescription(StringBuilder SB, string Stat, int Adjust, bool Activated = false, bool Percent = false)
		{
			Statistic.AppendStatAdjustDescription(SB, Stat, Adjust, Activated, Percent);
		}

		// Token: 0x060063E6 RID: 25574 RVA: 0x001ECC04 File Offset: 0x001EAE04
		public static void AppendStatAdjustDescription(StringBuilder SB, string Stat, int Adjust, bool Activated = false, bool Percent = false)
		{
			if (Activated)
			{
				SB.Append("When activated, ");
			}
			if (Statistic.IsInverseBenefit(Stat))
			{
				Adjust = -Adjust;
			}
			if (Adjust > 0)
			{
				SB.Append('+');
			}
			SB.Append(Adjust);
			if (Percent)
			{
				SB.Append('%');
			}
			SB.Append(' ').Append(Statistic.GetStatDisplayName(Stat));
		}

		// Token: 0x060063E7 RID: 25575 RVA: 0x001ECC62 File Offset: 0x001EAE62
		public static void CompoundStatAdjustDescription(StringBuilder SB, string Stat, int Adjust, char With = ' ', bool Activated = false, bool Percent = false)
		{
			if (SB.Length > 0)
			{
				SB.Append(With);
			}
			Statistic.AppendStatAdjustDescription(SB, Stat, Adjust, Activated, Percent);
		}

		// Token: 0x060063E8 RID: 25576 RVA: 0x001ECC81 File Offset: 0x001EAE81
		public static string GetStatAdjustDescription(string Stat, int Adjust, bool Activated = false)
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			Statistic.AppendStatAdjustDescription(stringBuilder, Stat, Adjust, Activated, false);
			return Event.FinalizeString(stringBuilder);
		}

		// Token: 0x060063E9 RID: 25577 RVA: 0x001ECC98 File Offset: 0x001EAE98
		private static int StatisticComparer(string a, string b)
		{
			if (a == b)
			{
				return 0;
			}
			int num = Statistic.Attributes.IndexOf(a);
			int num2 = Statistic.Attributes.IndexOf(b);
			if (num != -1)
			{
				if (num2 != -1)
				{
					return num.CompareTo(num2);
				}
				return -1;
			}
			else
			{
				if (num2 != -1)
				{
					return 1;
				}
				return a.CompareTo(b);
			}
		}

		// Token: 0x060063EA RID: 25578 RVA: 0x001ECCE8 File Offset: 0x001EAEE8
		public static void SortStatistics(List<string> list)
		{
			list.Sort(new Comparison<string>(Statistic.StatisticComparer));
		}

		// Token: 0x060063EB RID: 25579 RVA: 0x001ECCFC File Offset: 0x001EAEFC
		public Statistic()
		{
		}

		// Token: 0x060063EC RID: 25580 RVA: 0x001ECD1C File Offset: 0x001EAF1C
		public Statistic(Statistic Source)
			: this()
		{
			this.Name = Source.Name;
			this.Min = Source.Min;
			this.Max = Source.Max;
			this.Penalty = Source.Penalty;
			this.Bonus = Source.Bonus;
			this.BaseValue = Source.BaseValue;
			this.Owner = Source.Owner;
			this.sValue = Source.sValue;
			this.Boost = Source.Boost;
			if (Source.Shifts != null)
			{
				this.Shifts = new List<Statistic.StatShift>();
				using (List<Statistic.StatShift>.Enumerator enumerator = Source.Shifts.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Statistic.StatShift statShift = enumerator.Current;
						this.Shifts.Add(new Statistic.StatShift
						{
							ID = statShift.ID,
							Amount = statShift.Amount,
							DisplayName = statShift.DisplayName,
							BaseValue = statShift.BaseValue
						});
					}
					return;
				}
			}
			this.Shifts = null;
		}

		// Token: 0x060063ED RID: 25581 RVA: 0x001ECE40 File Offset: 0x001EB040
		public Statistic(string name, int min, int max, int val, GameObject parent)
		{
			this.Name = name;
			this.Min = min;
			this.Max = max;
			this.Owner = parent;
			this.Penalty = 0;
			this.Bonus = 0;
			this.BaseValue = val;
			this.Boost = 0;
		}

		// Token: 0x060063EE RID: 25582 RVA: 0x001ECEA4 File Offset: 0x001EB0A4
		public void BoostStat(int Amount)
		{
			if (Amount > 0)
			{
				this.BaseValue += (int)Math.Ceiling((double)this.BaseValue * 0.25 * (double)Amount);
				return;
			}
			if (Amount < 0)
			{
				this.BaseValue += (int)Math.Ceiling((double)this.BaseValue * 0.2 * (double)Amount);
			}
		}

		// Token: 0x060063EF RID: 25583 RVA: 0x001ECF08 File Offset: 0x001EB108
		public void BoostStat(double Amount)
		{
			if (Amount > 0.0)
			{
				this.BaseValue += (int)Math.Ceiling((double)this.BaseValue * 0.25 * Amount);
				return;
			}
			if (Amount < 0.0)
			{
				this.BaseValue += (int)Math.Ceiling((double)this.BaseValue * 0.2 * Amount);
			}
		}

		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x060063F0 RID: 25584 RVA: 0x001ECF7C File Offset: 0x001EB17C
		public string ValueOrSValue
		{
			get
			{
				if (this.sValue != "")
				{
					return this.sValue;
				}
				return this.Value.ToString();
			}
		}

		// Token: 0x060063F1 RID: 25585 RVA: 0x001ECFB0 File Offset: 0x001EB1B0
		public void Save(SerializationWriter Writer)
		{
			Writer.WriteOptimized(this.Name);
			Writer.WriteOptimized(this._Bonus);
			Writer.WriteOptimized(this._Penalty);
			Writer.WriteOptimized(this._Value);
			List<Statistic.StatShift> shifts = this.Shifts;
			int num = ((shifts != null) ? shifts.Count : 0);
			Writer.WriteOptimized(num);
			for (int i = 0; i < num; i++)
			{
				this.Shifts[i].Write(Writer);
			}
		}

		// Token: 0x060063F2 RID: 25586 RVA: 0x001ED028 File Offset: 0x001EB228
		public static Statistic Load(SerializationReader Reader, GameObject Owner)
		{
			Statistic statistic = new Statistic();
			statistic.Owner = Owner;
			statistic.Name = Reader.ReadOptimizedString();
			statistic._Bonus = Reader.ReadOptimizedInt32();
			statistic._Penalty = Reader.ReadOptimizedInt32();
			statistic._Value = Reader.ReadOptimizedInt32();
			int num = Reader.ReadOptimizedInt32();
			if (num > 0)
			{
				statistic.Shifts = new List<Statistic.StatShift>(num);
				for (int i = 0; i < num; i++)
				{
					Statistic.StatShift statShift = default(Statistic.StatShift);
					statShift.Read(Reader);
					statistic.Shifts.Add(statShift);
				}
			}
			return statistic;
		}

		// Token: 0x060063F3 RID: 25587 RVA: 0x001ED0B4 File Offset: 0x001EB2B4
		public bool SameAs(Statistic S)
		{
			return !(this.Name != S.Name) && this.Min == S.Min && this.Max == S.Max && this.Penalty == S.Penalty && this.Bonus == S.Bonus && this.Value == S.Value;
		}

		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x060063F4 RID: 25588 RVA: 0x001ED127 File Offset: 0x001EB327
		// (set) Token: 0x060063F5 RID: 25589 RVA: 0x001ED158 File Offset: 0x001EB358
		public int Max
		{
			get
			{
				if (this._MaxValue != null || Statistic._Max.TryGetValue(this.Name, out this._MaxValue))
				{
					return this._MaxValue.i;
				}
				return 30;
			}
			set
			{
				if (this._MaxValue == null && !Statistic._Max.TryGetValue(this.Name, out this._MaxValue))
				{
					Statistic._Max[this.Name] = (this._MaxValue = new Statistic.IntBox(value));
					return;
				}
				this._MaxValue.i = value;
			}
		}

		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x060063F6 RID: 25590 RVA: 0x001ED1B1 File Offset: 0x001EB3B1
		// (set) Token: 0x060063F7 RID: 25591 RVA: 0x001ED1E0 File Offset: 0x001EB3E0
		public int Min
		{
			get
			{
				if (this._MinValue != null || Statistic._Min.TryGetValue(this.Name, out this._MinValue))
				{
					return this._MinValue.i;
				}
				return 0;
			}
			set
			{
				if (this._MinValue == null && !Statistic._Min.TryGetValue(this.Name, out this._MinValue))
				{
					Statistic._Min[this.Name] = (this._MinValue = new Statistic.IntBox(value));
					return;
				}
				this._MinValue.i = value;
			}
		}

		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x060063F8 RID: 25592 RVA: 0x001ED239 File Offset: 0x001EB439
		// (set) Token: 0x060063F9 RID: 25593 RVA: 0x001ED244 File Offset: 0x001EB444
		public int BaseValue
		{
			get
			{
				return this._Value;
			}
			set
			{
				if (this._Value != value)
				{
					int value2 = this.Value;
					int baseValue = this.BaseValue;
					this._Value = value;
					this.NotifyChange(value2, baseValue, "BaseValue");
				}
			}
		}

		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x060063FA RID: 25594 RVA: 0x001ED27C File Offset: 0x001EB47C
		public int Modifier
		{
			get
			{
				return Stat.GetScoreModifier(this.Value);
			}
		}

		// Token: 0x060063FB RID: 25595 RVA: 0x001ED28C File Offset: 0x001EB48C
		public void NotifyChange(int OldValue, int OldBaseValue, string Type)
		{
			if (this.Owner != null)
			{
				if (this.StatChangeID == null)
				{
					this.StatChangeID = "StatChange_" + this.Name;
				}
				if (this.Owner.HasRegisteredEvent(this.StatChangeID))
				{
					if (this.eStatChange == null)
					{
						this.eStatChange = new Event(this.StatChangeID, 0, 2, 4);
					}
					this.eStatChange.SetParameter("Stat", this.Name);
					this.eStatChange.SetParameter("OldValue", OldValue);
					this.eStatChange.SetParameter("NewValue", this.Value);
					this.eStatChange.SetParameter("OldBaseValue", OldBaseValue);
					this.eStatChange.SetParameter("NewBaseValue", this.BaseValue);
					this.eStatChange.SetParameter("Type", Type);
					this.Owner.FireEvent(this.eStatChange);
				}
				if (this.Owner.WantEvent(PooledEvent<StatChangeEvent>.ID, MinEvent.CascadeLevel))
				{
					StatChangeEvent statChangeEvent = StatChangeEvent.FromPool(this.Owner, this.Name, Type, OldValue, this.Value, OldBaseValue, this.BaseValue, this);
					this.Owner.HandleEvent(statChangeEvent);
				}
			}
		}

		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x060063FC RID: 25596 RVA: 0x001ED3C8 File Offset: 0x001EB5C8
		public int Value
		{
			get
			{
				int num = this._Value + this._Bonus - this._Penalty;
				if (num < this.Min)
				{
					return this.Min;
				}
				if (num > this.Max)
				{
					return this.Max;
				}
				return num;
			}
		}

		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x060063FD RID: 25597 RVA: 0x001ED40B File Offset: 0x001EB60B
		// (set) Token: 0x060063FE RID: 25598 RVA: 0x001ED414 File Offset: 0x001EB614
		public int Bonus
		{
			get
			{
				return this._Bonus;
			}
			set
			{
				if (this._Bonus != Math.Max(0, value))
				{
					int value2 = this.Value;
					int baseValue = this.BaseValue;
					this._Bonus = value;
					if (this._Bonus < 0)
					{
						this._Bonus = 0;
					}
					this.NotifyChange(value2, baseValue, "Bonus");
				}
			}
		}

		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x060063FF RID: 25599 RVA: 0x001ED462 File Offset: 0x001EB662
		// (set) Token: 0x06006400 RID: 25600 RVA: 0x001ED46C File Offset: 0x001EB66C
		public int Penalty
		{
			get
			{
				return this._Penalty;
			}
			set
			{
				if (this._Penalty != Math.Max(0, value))
				{
					int value2 = this.Value;
					int baseValue = this.BaseValue;
					this._Penalty = value;
					if (this._Penalty < 0)
					{
						this._Penalty = 0;
					}
					this.NotifyChange(value2, baseValue, "Penalty");
				}
			}
		}

		// Token: 0x06006401 RID: 25601 RVA: 0x001ED4BC File Offset: 0x001EB6BC
		public override string ToString()
		{
			return this.Name + ": " + this.Value.ToString();
		}

		// Token: 0x06006402 RID: 25602 RVA: 0x001ED4E7 File Offset: 0x001EB6E7
		public string GetDisplayName()
		{
			return Statistic.GetStatDisplayName(this.Name);
		}

		// Token: 0x06006403 RID: 25603 RVA: 0x001ED4F4 File Offset: 0x001EB6F4
		public string GetShortDisplayName()
		{
			return Statistic.GetStatShortName(this.Name);
		}

		// Token: 0x06006404 RID: 25604 RVA: 0x001ED504 File Offset: 0x001EB704
		public Statistic.StatShift GetShift(Guid shiftId)
		{
			return this.Shifts.Find((Statistic.StatShift s) => s.ID == shiftId);
		}

		// Token: 0x06006405 RID: 25605 RVA: 0x001ED538 File Offset: 0x001EB738
		public Guid AddShift(int amount, string DisplayName, bool baseValue = false)
		{
			Guid guid = Guid.NewGuid();
			Statistic.StatShift statShift = new Statistic.StatShift
			{
				ID = guid,
				Amount = amount,
				DisplayName = DisplayName,
				BaseValue = baseValue
			};
			if (this.Shifts == null)
			{
				this.Shifts = new List<Statistic.StatShift>(1);
			}
			if (Options.DebugStatShift && The.Game != null)
			{
				The.Game.Player.Messages.Add(string.Format("DEBUG: NEW STAT SHIFT - {0}'s {1} was shifted {2} by the {3}.", new object[]
				{
					this.Owner.DisplayNameOnly,
					this.Name,
					statShift.Amount,
					statShift.DisplayName
				}));
			}
			this.Shifts.Add(statShift);
			if (baseValue)
			{
				this.BaseValue += amount;
			}
			else if (amount > 0)
			{
				this.Bonus += amount;
			}
			else
			{
				this.Penalty += -amount;
			}
			return guid;
		}

		// Token: 0x06006406 RID: 25606 RVA: 0x001ED62C File Offset: 0x001EB82C
		public void RemoveShift(Guid id)
		{
			if (this.Shifts == null)
			{
				return;
			}
			int num = this.Shifts.FindIndex((Statistic.StatShift s) => s.ID == id);
			if (num != -1)
			{
				Statistic.StatShift statShift = this.Shifts[num];
				if (Options.DebugStatShift && The.Game != null)
				{
					The.Game.Player.Messages.Add(string.Format("DEBUG: REMOVED STAT SHIFT - {0}'s {1} was shifted {2} by the {3}.", new object[]
					{
						this.Owner.DisplayNameOnlyDirect,
						this.Name,
						statShift.Amount,
						statShift.DisplayName
					}));
				}
				if (statShift.BaseValue)
				{
					this.BaseValue -= statShift.Amount;
				}
				if (statShift.Amount > 0)
				{
					this.Bonus -= statShift.Amount;
				}
				else
				{
					this.Penalty -= -statShift.Amount;
				}
				this.Shifts.RemoveAt(num);
				if (this.Shifts.Count == 0 && !this.Owner.IsPlayerControlled())
				{
					this.Shifts = null;
				}
			}
		}

		// Token: 0x06006407 RID: 25607 RVA: 0x001ED758 File Offset: 0x001EB958
		public bool UpdateShift(Guid id, int newAmount)
		{
			if (this.Shifts == null)
			{
				return false;
			}
			int num = this.Shifts.FindIndex((Statistic.StatShift s) => s.ID == id);
			if (num == -1)
			{
				return false;
			}
			Statistic.StatShift statShift = this.Shifts[num];
			int amount = statShift.Amount;
			int num2 = newAmount - amount;
			if (Options.DebugStatShift)
			{
				XRLCore.Core.Game.Player.Messages.Add(string.Format("DEBUG: UPDATED STAT SHIFT - {0}'s {1} shift was {2}, now {3}, change {4}, from {5}.", new object[]
				{
					this.Owner.DebugName,
					this.Name,
					amount,
					newAmount,
					num2,
					statShift.DisplayName
				}));
			}
			if (num2 == 0)
			{
				return true;
			}
			if (statShift.BaseValue)
			{
				this.BaseValue += num2;
			}
			else if (newAmount == 0)
			{
				if (amount > 0)
				{
					this.Bonus -= amount;
				}
				else
				{
					this.Penalty -= -amount;
				}
			}
			else if (amount == 0 || amount >= 0 == newAmount >= 0)
			{
				if (newAmount >= 0)
				{
					this.Bonus += num2;
				}
				else
				{
					this.Penalty += -num2;
				}
			}
			else
			{
				if (newAmount > 0)
				{
					this.Bonus += newAmount;
				}
				if (amount > 0)
				{
					this.Bonus -= amount;
				}
				else
				{
					this.Penalty -= -amount;
				}
				if (newAmount <= 0)
				{
					this.Penalty += -newAmount;
				}
			}
			statShift.Amount = newAmount;
			this.Shifts.RemoveAt(num);
			this.Shifts.Add(statShift);
			return true;
		}

		// Token: 0x04002EE1 RID: 12001
		[NonSerialized]
		public static List<string> Attributes = new List<string>(6) { "Strength", "Agility", "Toughness", "Intelligence", "Willpower", "Ego" };

		// Token: 0x04002EE2 RID: 12002
		[NonSerialized]
		public static List<string> MentalStats = new List<string>(4) { "Ego", "Intelligence", "Willpower", "MA" };

		// Token: 0x04002EE3 RID: 12003
		[NonSerialized]
		public static List<string> InverseBenefitStats = new List<string>(1) { "MoveSpeed" };

		// Token: 0x04002EE4 RID: 12004
		[NonSerialized]
		public static Dictionary<string, string> StatDisplayNames = new Dictionary<string, string>
		{
			{ "AcidResistance", "acid resistance" },
			{ "ColdResistance", "cold resistance" },
			{ "ElectricResistance", "electric resistance" },
			{ "HeatResistance", "heat resistance" },
			{ "Hitpoints", "hit points" },
			{ "MoveSpeed", "move speed" },
			{ "Speed", "quickness" },
			{ "AP", "attribute points" },
			{ "MP", "mutation points" },
			{ "SP", "skill points" },
			{ "XP", "experience points" }
		};

		// Token: 0x04002EE5 RID: 12005
		[NonSerialized]
		public static Dictionary<string, string> StatCapitalizedDisplayNames = new Dictionary<string, string>
		{
			{ "AcidResistance", "Acid Resistance" },
			{ "ColdResistance", "Cold Resistance" },
			{ "ElectricResistance", "Electric Resistance" },
			{ "HeatResistance", "Heat Resistance" },
			{ "Hitpoints", "Hit Points" },
			{ "MoveSpeed", "Move Speed" },
			{ "Speed", "Quickness" },
			{ "AP", "Attribute Points" },
			{ "MP", "Mutation Points" },
			{ "SP", "Skill Points" },
			{ "XP", "Experience Points" }
		};

		// Token: 0x04002EE6 RID: 12006
		[NonSerialized]
		public static Dictionary<string, string> StatShortNames = new Dictionary<string, string>
		{
			{ "AcidResistance", "AR" },
			{ "ColdResistance", "CR" },
			{ "ElectricResistance", "ER" },
			{ "HeatResistance", "HR" },
			{ "MoveSpeed", "MS" },
			{ "Speed", "QN" },
			{ "Hitpoints", "HP" },
			{ "Strength", "STR" },
			{ "Intelligence", "INT" },
			{ "Agility", "AGI" },
			{ "Toughness", "TOU" },
			{ "Willpower", "WIL" },
			{ "Ego", "EGO" }
		};

		// Token: 0x04002EE7 RID: 12007
		[NonSerialized]
		public static Dictionary<string, string> StatTitleCaseShortNames = new Dictionary<string, string>
		{
			{ "AcidResistance", "AR" },
			{ "ColdResistance", "CR" },
			{ "ElectricResistance", "ER" },
			{ "HeatResistance", "HR" },
			{ "MoveSpeed", "MS" },
			{ "Speed", "QN" },
			{ "Hitpoints", "HP" },
			{ "Strength", "Str" },
			{ "Intelligence", "Int" },
			{ "Agility", "Agi" },
			{ "Toughness", "Tou" },
			{ "Willpower", "Wil" },
			{ "Ego", "Ego" }
		};

		// Token: 0x04002EE8 RID: 12008
		[NonSerialized]
		public static List<string> StatPlurality = new List<string> { "Hitpoints", "AP", "MP", "SP", "XP" };

		// Token: 0x04002EE9 RID: 12009
		[NonSerialized]
		public static Dictionary<string, string> StatNarration = new Dictionary<string, string> { { "Hitpoints", "life" } };

		// Token: 0x04002EEA RID: 12010
		public GameObject Owner;

		// Token: 0x04002EEB RID: 12011
		public string Name = "";

		// Token: 0x04002EEC RID: 12012
		public string sValue = "";

		// Token: 0x04002EED RID: 12013
		public int Boost;

		// Token: 0x04002EEE RID: 12014
		public static Dictionary<string, Statistic.IntBox> _Max = new Dictionary<string, Statistic.IntBox>();

		// Token: 0x04002EEF RID: 12015
		public static Dictionary<string, Statistic.IntBox> _Min = new Dictionary<string, Statistic.IntBox>();

		// Token: 0x04002EF0 RID: 12016
		private Statistic.IntBox _MaxValue;

		// Token: 0x04002EF1 RID: 12017
		private Statistic.IntBox _MinValue;

		// Token: 0x04002EF2 RID: 12018
		public int _Value;

		// Token: 0x04002EF3 RID: 12019
		[NonSerialized]
		private string StatChangeID;

		// Token: 0x04002EF4 RID: 12020
		[NonSerialized]
		private Event eStatChange;

		// Token: 0x04002EF5 RID: 12021
		public int _Bonus;

		// Token: 0x04002EF6 RID: 12022
		public int _Penalty;

		// Token: 0x04002EF7 RID: 12023
		public List<Statistic.StatShift> Shifts;

		// Token: 0x02001A71 RID: 6769
		public class IntBox
		{
			// Token: 0x0600E796 RID: 59286 RVA: 0x00536530 File Offset: 0x00534730
			public IntBox(int _i)
			{
				this.i = _i;
			}

			// Token: 0x04006954 RID: 26964
			public int i;
		}

		// Token: 0x02001A72 RID: 6770
		[Serializable]
		public struct StatShift
		{
			// Token: 0x0600E797 RID: 59287 RVA: 0x0053653F File Offset: 0x0053473F
			public void Write(SerializationWriter Writer)
			{
				Writer.Write(this.ID);
				Writer.WriteOptimized(this.Amount);
				Writer.Write(this.DisplayName);
				Writer.Write(this.BaseValue);
			}

			// Token: 0x0600E798 RID: 59288 RVA: 0x00536571 File Offset: 0x00534771
			public void Read(SerializationReader Reader)
			{
				this.ID = Reader.ReadGuid();
				this.Amount = Reader.ReadOptimizedInt32();
				this.DisplayName = Reader.ReadString();
				this.BaseValue = Reader.ReadBoolean();
			}

			// Token: 0x04006955 RID: 26965
			public Guid ID;

			// Token: 0x04006956 RID: 26966
			public int Amount;

			// Token: 0x04006957 RID: 26967
			public string DisplayName;

			// Token: 0x04006958 RID: 26968
			public bool BaseValue;
		}
	}
}
