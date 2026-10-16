using System;
using System.CodeDom.Compiler;
using System.Text;
using Occult.Engine.CodeGeneration;
using XRL.World.Anatomy;

namespace XRL.World.Parts
{
	// Token: 0x02000DCA RID: 3530
	[GenerateSerializationPartial]
	[Serializable]
	public class Armor : IPart
	{
		// Token: 0x06008F78 RID: 36728 RVA: 0x0033AFD4 File Offset: 0x003391D4
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Write(GameObject Basis, SerializationWriter Writer)
		{
			Writer.WriteOptimized(this.WornOn);
			Writer.WriteOptimized(this._AV);
			Writer.WriteOptimized(this._DV);
			Writer.WriteOptimized(this.MA);
			Writer.WriteOptimized(this.Acid);
			Writer.WriteOptimized(this.Elec);
			Writer.WriteOptimized(this.Cold);
			Writer.WriteOptimized(this.Heat);
			Writer.WriteOptimized(this.Strength);
			Writer.WriteOptimized(this.Agility);
			Writer.WriteOptimized(this.Toughness);
			Writer.WriteOptimized(this.Intelligence);
			Writer.WriteOptimized(this.Ego);
			Writer.WriteOptimized(this.Willpower);
			Writer.WriteOptimized(this.ToHit);
			Writer.WriteOptimized(this.SpeedPenalty);
			Writer.WriteOptimized(this.SpeedBonus);
			Writer.WriteOptimized(this.CarryBonus);
			Writer.WriteOptimized(this.AVApplied);
			Writer.WriteOptimized(this.DVApplied);
			Writer.Write(this.AVAveraged);
			Writer.Write(this.DVAveraged);
			Writer.Write(this.BonusApplied);
			Writer.Write(this.Describe);
		}

		// Token: 0x06008F79 RID: 36729 RVA: 0x0033B104 File Offset: 0x00339304
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Read(GameObject Basis, SerializationReader Reader)
		{
			this.WornOn = Reader.ReadOptimizedString();
			this._AV = Reader.ReadOptimizedInt32();
			this._DV = Reader.ReadOptimizedInt32();
			this.MA = Reader.ReadOptimizedInt32();
			this.Acid = Reader.ReadOptimizedInt32();
			this.Elec = Reader.ReadOptimizedInt32();
			this.Cold = Reader.ReadOptimizedInt32();
			this.Heat = Reader.ReadOptimizedInt32();
			this.Strength = Reader.ReadOptimizedInt32();
			this.Agility = Reader.ReadOptimizedInt32();
			this.Toughness = Reader.ReadOptimizedInt32();
			this.Intelligence = Reader.ReadOptimizedInt32();
			this.Ego = Reader.ReadOptimizedInt32();
			this.Willpower = Reader.ReadOptimizedInt32();
			this.ToHit = Reader.ReadOptimizedInt32();
			this.SpeedPenalty = Reader.ReadOptimizedInt32();
			this.SpeedBonus = Reader.ReadOptimizedInt32();
			this.CarryBonus = Reader.ReadOptimizedInt32();
			this.AVApplied = Reader.ReadOptimizedInt32();
			this.DVApplied = Reader.ReadOptimizedInt32();
			this.AVAveraged = Reader.ReadBoolean();
			this.DVAveraged = Reader.ReadBoolean();
			this.BonusApplied = Reader.ReadBoolean();
			this.Describe = Reader.ReadBoolean();
		}

		// Token: 0x17000A70 RID: 2672
		// (get) Token: 0x06008F7B RID: 36731 RVA: 0x0033B24D File Offset: 0x0033944D
		// (set) Token: 0x06008F7A RID: 36730 RVA: 0x0033B231 File Offset: 0x00339431
		public int AV
		{
			get
			{
				return this._AV;
			}
			set
			{
				if (value != this._AV)
				{
					this._AV = value;
					this.RecalculateArmor(null, null, true);
				}
			}
		}

		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x06008F7D RID: 36733 RVA: 0x0033B271 File Offset: 0x00339471
		// (set) Token: 0x06008F7C RID: 36732 RVA: 0x0033B255 File Offset: 0x00339455
		public int DV
		{
			get
			{
				return this._DV;
			}
			set
			{
				if (value != this._DV)
				{
					this._DV = value;
					this.RecalculateArmor(null, null, true);
				}
			}
		}

		// Token: 0x06008F7E RID: 36734 RVA: 0x0033B27C File Offset: 0x0033947C
		public override bool SameAs(IPart p)
		{
			Armor armor = p as Armor;
			return !(armor.WornOn != this.WornOn) && armor.AV == this.AV && armor.DV == this.DV && armor.MA == this.MA && armor.Acid == this.Acid && armor.Elec == this.Elec && armor.Cold == this.Cold && armor.Heat == this.Heat && armor.Strength == this.Strength && armor.Agility == this.Agility && armor.Toughness == this.Toughness && armor.Intelligence == this.Intelligence && armor.Ego == this.Ego && armor.Willpower == this.Willpower && armor.ToHit == this.ToHit && armor.SpeedPenalty == this.SpeedPenalty && armor.SpeedBonus == this.SpeedBonus && armor.CarryBonus == this.CarryBonus && base.SameAs(p);
		}

		// Token: 0x06008F7F RID: 36735 RVA: 0x0033B3BC File Offset: 0x003395BC
		public bool RecalculateArmor(GameObject who = null, BodyPart onPart = null, bool Cascade = false)
		{
			if (who == null)
			{
				if (this.ParentObject != null)
				{
					who = this.ParentObject.Equipped;
				}
				if (who == null)
				{
					return false;
				}
			}
			if (onPart == null)
			{
				onPart = who.FindEquippedObject(this.ParentObject);
				if (onPart == null)
				{
					return false;
				}
			}
			Body body = who.Body;
			if (body == null)
			{
				return false;
			}
			int num;
			int num2;
			if (this.ParentObject.IsWorn(onPart))
			{
				bool? noArmorAveraging = onPart.VariantTypeModel().NoArmorAveraging;
				bool flag = true;
				if ((noArmorAveraging.GetValueOrDefault() == flag) & (noArmorAveraging != null))
				{
					this.AVAveraged = false;
					this.DVAveraged = false;
					num = this.AV;
					num2 = this.DV;
				}
				else
				{
					GameObject gameObject = null;
					int num3 = 0;
					int num4 = 0;
					int num5 = 0;
					string text = ((this.WornOn == "*") ? onPart.Type : this.WornOn);
					body.GetTypeArmorInfo(text, ref gameObject, ref num3, ref num4, ref num5);
					if (Cascade && gameObject != null)
					{
						if (this.ParentObject == gameObject)
						{
							body.RecalculateTypeArmorExcept(text, this.ParentObject);
						}
						else
						{
							Armor part = gameObject.GetPart<Armor>();
							if (part != null)
							{
								part.RecalculateArmor(null, null, false);
							}
						}
					}
					int num6 = ((num3 > 0) ? ((num3 > 1) ? ((int)Math.Round((double)num4 / (double)num3)) : num4) : 0);
					int num7 = ((num3 > 0) ? ((num3 > 1) ? ((int)Math.Round((double)num5 / (double)num3)) : num5) : 0);
					this.AVAveraged = this.AV != 0 && num6 != num4;
					this.DVAveraged = this.DV != 0 && num7 != num5;
					num = ((this.ParentObject == gameObject) ? num6 : 0);
					num2 = ((this.ParentObject == gameObject) ? num7 : 0);
				}
			}
			else
			{
				this.AVAveraged = false;
				this.DVAveraged = false;
				num = 0;
				num2 = 0;
			}
			if (num != this.AVApplied)
			{
				if (this.AVApplied > 0)
				{
					who.GetStat("AV").Bonus -= this.AVApplied;
				}
				else if (this.AVApplied < 0)
				{
					who.GetStat("AV").Penalty -= -this.AVApplied;
				}
				if (num > 0)
				{
					who.GetStat("AV").Bonus += num;
				}
				else if (num < 0)
				{
					who.GetStat("AV").Penalty += -num;
				}
				this.AVApplied = num;
			}
			if (num2 != this.DVApplied)
			{
				if (this.DVApplied > 0)
				{
					who.GetStat("DV").Bonus -= this.DVApplied;
				}
				else if (this.DVApplied < 0)
				{
					who.GetStat("DV").Penalty -= -this.DVApplied;
				}
				if (num2 > 0)
				{
					who.GetStat("DV").Bonus += num2;
				}
				else if (num2 < 0)
				{
					who.GetStat("DV").Penalty += -num2;
				}
				this.DVApplied = num2;
			}
			return num != 0 || num2 != 0;
		}

		// Token: 0x06008F80 RID: 36736 RVA: 0x0033B6C4 File Offset: 0x003398C4
		public bool RemoveArmor(GameObject who = null)
		{
			if (who == null)
			{
				if (this.ParentObject == null || this.ParentObject.Physics == null)
				{
					return false;
				}
				who = this.ParentObject.Equipped;
				if (who == null)
				{
					return false;
				}
			}
			if (this.AVApplied != 0)
			{
				if (this.AVApplied > 0)
				{
					who.Statistics["AV"].Bonus -= this.AVApplied;
				}
				else
				{
					who.Statistics["AV"].Penalty -= -this.AVApplied;
				}
				this.AVApplied = 0;
			}
			if (this.DVApplied != 0)
			{
				if (this.DVApplied > 0)
				{
					who.Statistics["DV"].Bonus -= this.DVApplied;
				}
				else
				{
					who.Statistics["DV"].Penalty -= -this.DVApplied;
				}
				this.DVApplied = 0;
			}
			Body body = who.Body;
			if (body != null)
			{
				if (this.WornOn == "*")
				{
					body.RecalculateArmor();
				}
				else
				{
					body.RecalculateTypeArmor(this.WornOn);
				}
			}
			this.AVAveraged = false;
			this.DVAveraged = false;
			return true;
		}

		// Token: 0x06008F81 RID: 36737 RVA: 0x0033B7FC File Offset: 0x003399FC
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AdjustTotalWeightEvent.ID || (ID == GetMaxCarriedWeightEvent.ID && this.CarryBonus != 0) || ID == EquippedEvent.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == PooledEvent<GetItemElementsEvent>.ID || (ID == GetShortDescriptionEvent.ID && this.Describe) || (ID == PooledEvent<GetToHitModifierEvent>.ID && this.ToHit != 0) || ID == QueryEquippableListEvent.ID || ID == UnequippedEvent.ID;
		}

		// Token: 0x06008F82 RID: 36738 RVA: 0x0033B878 File Offset: 0x00339A78
		public override bool HandleEvent(GetToHitModifierEvent E)
		{
			if (E.Actor == this.ParentObject.Equipped && E.Checking == "Actor" && E.Melee && this.ParentObject.IsWorn(null))
			{
				E.Modifier += this.ToHit;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008F83 RID: 36739 RVA: 0x0033B8DC File Offset: 0x00339ADC
		public override bool HandleEvent(AdjustTotalWeightEvent E)
		{
			BodyPart bodyPart = this.ParentObject.EquippedOn();
			if (bodyPart != null && !bodyPart.Contact)
			{
				E.AdjustWeight(0.0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008F84 RID: 36740 RVA: 0x0033B916 File Offset: 0x00339B16
		public override bool HandleEvent(GetMaxCarriedWeightEvent E)
		{
			if (this.BonusApplied)
			{
				E.AdjustWeight((100.0 + (double)this.CarryBonus) / 100.0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008F85 RID: 36741 RVA: 0x0033B948 File Offset: 0x00339B48
		public override bool HandleEvent(QueryEquippableListEvent E)
		{
			if (E.Item == this.ParentObject && !E.Item.HasTagOrProperty("CannotEquip"))
			{
				if (!E.RequirePossible)
				{
					if (!E.List.Contains(this.ParentObject))
					{
						E.List.Add(E.Item);
					}
				}
				else if (E.List.Contains(this.ParentObject))
				{
					if (this.WornOn == "Floating Nearby" && E.SlotType != this.WornOn)
					{
						E.List.Remove(E.Item);
						return false;
					}
				}
				else if (this.WornOn == "*" || E.SlotType == this.WornOn)
				{
					E.List.Add(E.Item);
				}
				else if (this.WornOn == "Floating Nearby")
				{
					return false;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008F86 RID: 36742 RVA: 0x0033BA50 File Offset: 0x00339C50
		public override bool HandleEvent(EquippedEvent E)
		{
			GameObject actor = E.Actor;
			this.RecalculateArmor(actor, null, true);
			if (this.UpdateStatShifts(actor) && this.CarryBonus != 0)
			{
				CarryingCapacityChangedEvent.Send(actor);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008F87 RID: 36743 RVA: 0x0033BA8C File Offset: 0x00339C8C
		public override bool HandleEvent(UnequippedEvent E)
		{
			GameObject actor = E.Actor;
			this.RemoveArmor(actor);
			if (this.BonusApplied)
			{
				this.BonusApplied = false;
				base.StatShifter.RemoveStatShifts(actor);
				if (this.CarryBonus != 0)
				{
					CarryingCapacityChangedEvent.Send(actor);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008F88 RID: 36744 RVA: 0x0033BAD8 File Offset: 0x00339CD8
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && (!this.ParentObject.HasPropertyOrTag("DisplayArmorStatsOnlyWhenEquipped") || this.ParentObject.Equipped != null))
			{
				string propertyOrTag = this.ParentObject.GetPropertyOrTag("ArmorStatsOverride", null);
				if (propertyOrTag != null)
				{
					if (propertyOrTag != "")
					{
						E.AddTag(propertyOrTag, -40);
					}
				}
				else
				{
					StringBuilder stringBuilder = Event.NewStringBuilder(null);
					stringBuilder.Append("{{b|").Append('\u0004').Append("}}");
					if (this.AVAveraged)
					{
						stringBuilder.Append((this.AV > 0) ? "{{r|" : "{{g|");
					}
					int @for = GetDisplayStatBonusEvent.GetFor(this.ParentObject, this, "AV", this.AV);
					stringBuilder.Append(@for);
					if (this.AVAveraged)
					{
						stringBuilder.Append("}}");
					}
					stringBuilder.Append(" {{K|").Append('\t').Append("}}");
					if (this.DVAveraged)
					{
						stringBuilder.Append((this.DV > 0) ? "{{r|" : "{{g|");
					}
					int for2 = GetDisplayStatBonusEvent.GetFor(this.ParentObject, this, "DV", this.DV);
					stringBuilder.Append(for2);
					if (this.DVAveraged)
					{
						stringBuilder.Append("}}");
					}
					E.AddTag(stringBuilder.ToString(), -40);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008F89 RID: 36745 RVA: 0x0033BC4C File Offset: 0x00339E4C
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (!this.Describe)
			{
				return base.HandleEvent(E);
			}
			StringBuilder postfix = E.Postfix;
			this.DescribeBonus(postfix, this.Heat, "Heat Resistance", 'R', 'r', false);
			this.DescribeBonus(postfix, this.Cold, "Cold Resistance", 'C', 'c', false);
			this.DescribeBonus(postfix, this.Elec, "Electrical Resistance", 'W', 'w', false);
			this.DescribeBonus(postfix, this.Acid, "Acid Resistance", 'G', 'g', false);
			this.DescribeBonus(postfix, this.MA, "MA", 'C', 'R', false);
			this.DescribeBonus(postfix, this.Strength, "Strength", 'C', 'R', false);
			this.DescribeBonus(postfix, this.Agility, "Agility", 'C', 'R', false);
			this.DescribeBonus(postfix, this.Toughness, "Toughness", 'C', 'R', false);
			this.DescribeBonus(postfix, this.Intelligence, "Intelligence", 'C', 'R', false);
			this.DescribeBonus(postfix, this.Willpower, "Willpower", 'C', 'R', false);
			this.DescribeBonus(postfix, this.Ego, "Ego", 'C', 'R', false);
			this.DescribeBonus(postfix, this.SpeedBonus - this.SpeedPenalty, "Quickness", 'C', 'R', false);
			this.DescribeBonus(postfix, this.CarryBonus, "carry capacity", 'C', 'R', true);
			this.DescribeBonus(postfix, this.ToHit, "to hit in melee", 'C', 'R', false);
			if (this.AVAveraged && this.DVAveraged)
			{
				if (this.AV > 0 && this.DV > 0)
				{
					postfix.AppendRules("This item's AV and DV bonuses are being averaged across all body parts of the same type.");
				}
				else if (this.AV < 0 && this.DV < 0)
				{
					postfix.AppendRules("This item's AV and DV penalties are being averaged across all body parts of the same type.");
				}
				else
				{
					postfix.AppendRules("This item's AV and DV modifiers are being averaged across all body parts of the same type.");
				}
			}
			else if (this.AVAveraged)
			{
				if (this.AV > 0)
				{
					postfix.AppendRules("This item's AV bonus is being averaged across all body parts of the same type.");
				}
				else
				{
					postfix.AppendRules("This item's AV penalty is being averaged across all body parts of the same type.");
				}
			}
			else if (this.DVAveraged)
			{
				if (this.DV > 0)
				{
					postfix.AppendRules("This item's DV bonus is being averaged across all body parts of the same type.");
				}
				else
				{
					postfix.AppendRules("This item's DV penalty is being averaged across all body parts of the same type.");
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008F8A RID: 36746 RVA: 0x0033BE7C File Offset: 0x0033A07C
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantObject(this.ParentObject))
			{
				if (this.AV + this.DV >= 8 || this.Strength > 0)
				{
					E.Add("might", 1);
				}
				if (this.SpeedBonus - this.SpeedPenalty > 0)
				{
					E.Add("time", 1);
				}
				if (this.Intelligence > 0)
				{
					E.Add("scholarship", 1);
				}
				if (this.CarryBonus > 0)
				{
					E.Add("travel", 1);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008F8B RID: 36747 RVA: 0x0033BF0C File Offset: 0x0033A10C
		public bool UpdateStatShifts(GameObject who = null)
		{
			if (who == null)
			{
				who = this.ParentObject.Equipped;
				if (who == null)
				{
					return false;
				}
			}
			if (!this.ParentObject.IsWorn(null))
			{
				base.StatShifter.RemoveStatShifts();
				this.BonusApplied = false;
				return false;
			}
			base.StatShifter.SetStatShift(who, "MA", this.MA, false);
			base.StatShifter.SetStatShift(who, "Speed", this.SpeedBonus - this.SpeedPenalty, false);
			base.StatShifter.SetStatShift(who, "Ego", this.Ego, false);
			base.StatShifter.SetStatShift(who, "Intelligence", this.Intelligence, false);
			base.StatShifter.SetStatShift(who, "Agility", this.Agility, false);
			base.StatShifter.SetStatShift(who, "Toughness", this.Toughness, false);
			base.StatShifter.SetStatShift(who, "Strength", this.Strength, false);
			base.StatShifter.SetStatShift(who, "Willpower", this.Willpower, false);
			base.StatShifter.SetStatShift(who, "ElectricResistance", this.Elec, false);
			base.StatShifter.SetStatShift(who, "AcidResistance", this.Acid, false);
			base.StatShifter.SetStatShift(who, "HeatResistance", this.Heat, false);
			base.StatShifter.SetStatShift(who, "ColdResistance", this.Cold, false);
			this.BonusApplied = true;
			return true;
		}

		// Token: 0x06008F8C RID: 36748 RVA: 0x0033C08C File Offset: 0x0033A28C
		private void DescribeBonus(StringBuilder SB, int amount, string what, char posColor = 'C', char negColor = 'R', bool isPerc = false)
		{
			if (amount != 0 && ShouldDescribeStatBonusEvent.Check(this.ParentObject, this, what, amount))
			{
				SB.Append("\n{{");
				if (amount > 0)
				{
					SB.Append(posColor).Append('|').Append('+');
				}
				else
				{
					SB.Append(negColor).Append('|');
				}
				SB.Append(amount);
				if (isPerc)
				{
					SB.Append('%');
				}
				SB.Append(' ').Append(what);
				SB.Append("}}");
			}
		}

		// Token: 0x04003BD1 RID: 15313
		public string WornOn = "Body";

		// Token: 0x04003BD2 RID: 15314
		public int _AV;

		// Token: 0x04003BD3 RID: 15315
		public int _DV;

		// Token: 0x04003BD4 RID: 15316
		public int MA;

		// Token: 0x04003BD5 RID: 15317
		public int Acid;

		// Token: 0x04003BD6 RID: 15318
		public int Elec;

		// Token: 0x04003BD7 RID: 15319
		public int Cold;

		// Token: 0x04003BD8 RID: 15320
		public int Heat;

		// Token: 0x04003BD9 RID: 15321
		public int Strength;

		// Token: 0x04003BDA RID: 15322
		public int Agility;

		// Token: 0x04003BDB RID: 15323
		public int Toughness;

		// Token: 0x04003BDC RID: 15324
		public int Intelligence;

		// Token: 0x04003BDD RID: 15325
		public int Ego;

		// Token: 0x04003BDE RID: 15326
		public int Willpower;

		// Token: 0x04003BDF RID: 15327
		public int ToHit;

		// Token: 0x04003BE0 RID: 15328
		public int SpeedPenalty;

		// Token: 0x04003BE1 RID: 15329
		public int SpeedBonus;

		// Token: 0x04003BE2 RID: 15330
		public int CarryBonus;

		// Token: 0x04003BE3 RID: 15331
		public int AVApplied;

		// Token: 0x04003BE4 RID: 15332
		public int DVApplied;

		// Token: 0x04003BE5 RID: 15333
		public bool AVAveraged;

		// Token: 0x04003BE6 RID: 15334
		public bool DVAveraged;

		// Token: 0x04003BE7 RID: 15335
		public bool BonusApplied;

		// Token: 0x04003BE8 RID: 15336
		public bool Describe = true;
	}
}
