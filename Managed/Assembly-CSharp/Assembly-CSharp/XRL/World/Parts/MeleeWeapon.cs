using System;
using System.CodeDom.Compiler;
using System.Text;
using Occult.Engine.CodeGeneration;
using XRL.Rules;
using XRL.UI;
using XRL.World.Anatomy;
using XRL.World.Effects;
using XRL.World.Parts.Skill;

namespace XRL.World.Parts
{
	// Token: 0x02000DC3 RID: 3523
	[GeneratePoolingPartial]
	[GenerateSerializationPartial]
	[Serializable]
	public class MeleeWeapon : IPart
	{
		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x06008E4A RID: 36426 RVA: 0x003300EF File Offset: 0x0032E2EF
		[GeneratedCode("PoolPartialsGenerator", "1.0.0.0")]
		public override IPartPool Pool
		{
			get
			{
				return MeleeWeapon.MeleeWeaponPool;
			}
		}

		// Token: 0x06008E4B RID: 36427 RVA: 0x003300F8 File Offset: 0x0032E2F8
		[GeneratedCode("PoolPartialsGenerator", "1.0.0.0")]
		public override void Reset()
		{
			base.Reset();
			this.MaxStrengthBonus = 0;
			this.PenBonus = 0;
			this.HitBonus = 0;
			this.BaseDamage = "5";
			this.Ego = 0;
			this.Skill = "Cudgel";
			this.Stat = "Strength";
			this.Slot = "Hand";
			this.Attributes = null;
		}

		// Token: 0x06008E4C RID: 36428 RVA: 0x0033015C File Offset: 0x0032E35C
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Write(GameObject Basis, SerializationWriter Writer)
		{
			Writer.WriteOptimized(this.MaxStrengthBonus);
			Writer.WriteOptimized(this.PenBonus);
			Writer.WriteOptimized(this.HitBonus);
			Writer.WriteOptimized(this.BaseDamage);
			Writer.WriteOptimized(this.Ego);
			Writer.WriteOptimized(this.Skill);
			Writer.WriteOptimized(this.Stat);
			Writer.WriteOptimized(this.Slot);
			Writer.WriteOptimized(this.Attributes);
		}

		// Token: 0x06008E4D RID: 36429 RVA: 0x003301D8 File Offset: 0x0032E3D8
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Read(GameObject Basis, SerializationReader Reader)
		{
			this.MaxStrengthBonus = Reader.ReadOptimizedInt32();
			this.PenBonus = Reader.ReadOptimizedInt32();
			this.HitBonus = Reader.ReadOptimizedInt32();
			this.BaseDamage = Reader.ReadOptimizedString();
			this.Ego = Reader.ReadOptimizedInt32();
			this.Skill = Reader.ReadOptimizedString();
			this.Stat = Reader.ReadOptimizedString();
			this.Slot = Reader.ReadOptimizedString();
			this.Attributes = Reader.ReadOptimizedString();
		}

		// Token: 0x06008E4E RID: 36430 RVA: 0x00330254 File Offset: 0x0032E454
		public override bool SameAs(IPart p)
		{
			MeleeWeapon meleeWeapon = p as MeleeWeapon;
			return meleeWeapon.MaxStrengthBonus == this.MaxStrengthBonus && meleeWeapon.PenBonus == this.PenBonus && meleeWeapon.HitBonus == this.HitBonus && !(meleeWeapon.BaseDamage != this.BaseDamage) && meleeWeapon.Ego == this.Ego && !(meleeWeapon.Skill != this.Skill) && !(meleeWeapon.Stat != this.Stat) && !(meleeWeapon.Slot != this.Slot) && !(meleeWeapon.Attributes != this.Attributes) && base.SameAs(p);
		}

		// Token: 0x06008E4F RID: 36431 RVA: 0x00330318 File Offset: 0x0032E518
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || (ID == EquippedEvent.ID && this.Ego != 0) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == PooledEvent<GetItemElementsEvent>.ID || ID == GetShortDescriptionEvent.ID || (ID == PooledEvent<GetToHitModifierEvent>.ID && this.HitBonus != 0) || ID == QueryEquippableListEvent.ID || (ID == UnequippedEvent.ID && this.Ego != 0);
		}

		// Token: 0x06008E50 RID: 36432 RVA: 0x00330384 File Offset: 0x0032E584
		public override bool HandleEvent(GetToHitModifierEvent E)
		{
			if (E.Weapon == this.ParentObject && E.Checking == "Actor")
			{
				E.Modifier += this.HitBonus;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008E51 RID: 36433 RVA: 0x003303C0 File Offset: 0x0032E5C0
		public override bool HandleEvent(EquippedEvent E)
		{
			base.StatShifter.SetStatShift(E.Actor, "Ego", this.Ego, false);
			return base.HandleEvent(E);
		}

		// Token: 0x06008E52 RID: 36434 RVA: 0x003303E7 File Offset: 0x0032E5E7
		public override bool HandleEvent(UnequippedEvent E)
		{
			base.StatShifter.RemoveStatShifts(E.Actor);
			return base.HandleEvent(E);
		}

		// Token: 0x06008E53 RID: 36435 RVA: 0x00330401 File Offset: 0x0032E601
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (this.ParentObject.HasTagOrProperty("ShowMeleeWeaponStats") && E.Understood())
			{
				E.AddTag(this.GetSimplifiedStats(Options.ShowDetailedWeaponStats), -20);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008E54 RID: 36436 RVA: 0x00330438 File Offset: 0x0032E638
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			bool flag = this.ParentObject.HasTagOrProperty("ShowMeleeWeaponStats");
			if (flag)
			{
				if (this.Ego != 0)
				{
					E.Postfix.AppendRules(this.Ego.Signed(false) + " Ego");
				}
				if (this.HitBonus != 0)
				{
					E.Postfix.AppendRules(this.HitBonus.Signed(false) + " To-Hit");
				}
				E.Postfix.AppendRules(this.GetDetailedStats());
			}
			this.AppendOffhandChance(E.Postfix, flag);
			return base.HandleEvent(E);
		}

		// Token: 0x06008E55 RID: 36437 RVA: 0x003304D4 File Offset: 0x0032E6D4
		public override bool HandleEvent(QueryEquippableListEvent E)
		{
			if (E.SlotType == this.Slot && E.Item == this.ParentObject && !E.List.Contains(E.Item) && (!E.RequireDesirable || (!this.IsImprovisedWeapon() && !E.Item.HasPropertyOrTag("UndesirableWeapon")) || E.Item.IsNatural() || (E.Item.HasPartDescendedFrom<ILightSource>() && !E.Item.HasPart<Armor>() && !E.Item.HasPart<Shield>() && !E.Item.HasPart<CyberneticsBaseItem>())))
			{
				if (!E.RequirePossible)
				{
					E.List.Add(E.Item);
				}
				else
				{
					string usesSlots = E.Item.UsesSlots;
					if (!usesSlots.IsNullOrEmpty() && (E.SlotType != "Thrown Weapon" || usesSlots.Contains("Thrown Weapon")) && (E.SlotType != "Hand" || usesSlots.Contains("Hand")))
					{
						if (E.Actor.IsGiganticCreature)
						{
							if (E.Item.IsGiganticEquipment || E.Item.HasPropertyOrTag("GiganticEquippable") || E.Item.IsNatural())
							{
								E.List.Add(E.Item);
							}
						}
						else if (E.SlotType == "Hand" || E.SlotType == "Missile Weapon" || !E.Item.IsGiganticEquipment || !E.Item.IsNatural())
						{
							E.List.Add(E.Item);
						}
					}
					else if (!E.Actor.IsGiganticCreature || E.Item.IsGiganticEquipment || E.Item.HasPropertyOrTag("GiganticEquippable") || E.Item.IsNatural())
					{
						int slotsRequiredFor = E.Item.GetSlotsRequiredFor(E.Actor, this.Slot, false);
						if (slotsRequiredFor > 0 && slotsRequiredFor <= E.Actor.GetBodyPartCount(E.SlotType))
						{
							E.List.Add(E.Item);
						}
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008E56 RID: 36438 RVA: 0x00330730 File Offset: 0x0032E930
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantObject(this.ParentObject))
			{
				if (this.Ego > 0 || this.MaxStrengthBonus >= 5)
				{
					E.Add("might", 1);
				}
				if (this.Stat == "Intelligence")
				{
					E.Add("scholarship", 1);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008E57 RID: 36439 RVA: 0x0033078E File Offset: 0x0032E98E
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06008E58 RID: 36440 RVA: 0x00330794 File Offset: 0x0032E994
		public bool AttackFromPart(BodyPart Part)
		{
			return Part.PreferredPrimary || string.IsNullOrEmpty(this.Slot) || Part.Type == null || this.Slot == Part.Type || this.Slot == Part.VariantType;
		}

		// Token: 0x06008E59 RID: 36441 RVA: 0x003307EC File Offset: 0x0032E9EC
		public string GetDetailedStats()
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			if (this.MaxStrengthBonus > 0 || this.PenBonus > 0)
			{
				stringBuilder.Compound(this.Stat, "\n").Append(" Bonus Cap: ");
				if (this.MaxStrengthBonus == 999)
				{
					stringBuilder.Append("no limit");
				}
				else
				{
					stringBuilder.Append(this.MaxStrengthBonus);
				}
			}
			BaseSkill genericSkill = Skills.GetGenericSkill(this.Skill, null);
			string text = ((genericSkill != null) ? genericSkill.GetWeaponCriticalDescription() : null);
			if (!string.IsNullOrEmpty(text))
			{
				stringBuilder.Compound("Weapon Class: " + text, "\n");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06008E5A RID: 36442 RVA: 0x00330894 File Offset: 0x0032EA94
		public void AppendOffhandChance(StringBuilder SB, bool Detailed = false)
		{
			GameObject gameObject = this.ParentObject.Equipped ?? The.Player;
			BodyPart bodyPart;
			if (gameObject == null)
			{
				bodyPart = null;
			}
			else
			{
				Body body = gameObject.Body;
				bodyPart = ((body != null) ? body.FindDefaultOrEquippedItem(this.ParentObject) : null);
			}
			BodyPart bodyPart2 = bodyPart;
			if (bodyPart2 != null)
			{
				bool flag = bodyPart2.Primary || gameObject.HasEquippedOnPrimary(this.ParentObject);
				int num = 0;
				if (this.AttackFromPart(bodyPart2))
				{
					num = GetMeleeAttackChanceEvent.GetFor(gameObject, this.ParentObject, -1, 0, 1.0, null, bodyPart2, bodyPart2.Primary ? bodyPart2 : null, flag, true, false);
				}
				if (flag)
				{
					if (num < 100)
					{
						SB.Append("\n{{rules|Attack Chance: ").Append(num).Append("%}}");
						return;
					}
				}
				else if (Detailed || num > 0)
				{
					SB.Append("\n{{rules|Offhand Attack Chance: ").Append(num).Append("%}}");
				}
			}
		}

		// Token: 0x06008E5B RID: 36443 RVA: 0x00330970 File Offset: 0x0032EB70
		public void GetNormalPenetration(GameObject Actor, out int BasePenetration, out int StatMod)
		{
			BasePenetration = this.PenBonus;
			StatMod = 0;
			if ((this.Skill == "LongBlades" || this.Skill == "ShortBlades") && Actor != null && Actor.HasPart(typeof(LongBladesCore)) && Actor.HasEffect(typeof(LongbladeStance_Aggressive)) && Actor.GetPart<LongBladesCore>().IsPrimaryBladeEquipped())
			{
				if (Actor.HasPart(typeof(LongBladesImprovedAggressiveStance)))
				{
					BasePenetration += 2;
				}
				else
				{
					BasePenetration++;
				}
			}
			if (Actor != null && !base.HasTag("WeaponIgnoreStrength"))
			{
				if (this.Stat.Contains(","))
				{
					StatMod = int.MinValue;
					foreach (string text in this.Stat.CachedCommaExpansion())
					{
						if (Actor.HasStat(text))
						{
							int num = Actor.StatMod(text, 0);
							if (num > StatMod)
							{
								StatMod = num;
							}
						}
					}
					if (StatMod == -2147483648)
					{
						StatMod = 0;
					}
				}
				else if (Actor.HasStat(this.Stat))
				{
					StatMod = Actor.StatMod(this.Stat, 0);
				}
				if (StatMod > this.MaxStrengthBonus)
				{
					StatMod = this.MaxStrengthBonus;
				}
			}
		}

		// Token: 0x06008E5C RID: 36444 RVA: 0x00330ACC File Offset: 0x0032ECCC
		public int GetNormalPenetration(GameObject Actor)
		{
			int num;
			int num2;
			this.GetNormalPenetration(Actor, out num, out num2);
			return num + num2;
		}

		// Token: 0x06008E5D RID: 36445 RVA: 0x00330AE8 File Offset: 0x0032ECE8
		public string GetSimplifiedStats(bool WithStrCapDetails)
		{
			GameObject gameObject = this.ParentObject.Equipped ?? IComponent<GameObject>.ThePlayer;
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			string @for = GetDisplayNamePenetrationColorEvent.GetFor(this.ParentObject, "c");
			if (!@for.IsNullOrEmpty())
			{
				stringBuilder.Append("{{").Append(@for).Append('|')
					.Append('\u001a')
					.Append("}}");
			}
			else
			{
				stringBuilder.Append('\u001a');
			}
			int num;
			int num2;
			this.GetNormalPenetration(gameObject, out num, out num2);
			int num3 = 0;
			string text = "÷";
			if (IsAdaptivePenetrationActiveEvent.Check(this.ParentObject, ref num3, ref text))
			{
				stringBuilder.Append(text);
				int num4 = num + num3;
				if (num4 > 0)
				{
					stringBuilder.Append('+').Append(num4);
				}
				else if (num4 < 0)
				{
					stringBuilder.Append(num4);
				}
			}
			else
			{
				stringBuilder.Append(num + num2 + RuleSettings.VISUAL_PENETRATION_BONUS);
				if (WithStrCapDetails)
				{
					if (this.MaxStrengthBonus == 999)
					{
						stringBuilder.Append("{{K|/").Append('ì').Append("}}");
					}
					else
					{
						stringBuilder.Append("{{K|/").Append(num + this.MaxStrengthBonus + RuleSettings.VISUAL_PENETRATION_BONUS).Append("}}");
					}
				}
			}
			stringBuilder.Append(" {{r|").Append('\u0003').Append("}}")
				.Append(this.BaseDamage);
			return stringBuilder.ToString();
		}

		// Token: 0x06008E5E RID: 36446 RVA: 0x00330C54 File Offset: 0x0032EE54
		public bool AdjustDamageDieSize(int Amount)
		{
			this.BaseDamage = DieRoll.AdjustDieSize(this.BaseDamage, Amount);
			DamageDieSizeAdjustedEvent.Send(this.ParentObject, this, Amount);
			return true;
		}

		// Token: 0x06008E5F RID: 36447 RVA: 0x00330C76 File Offset: 0x0032EE76
		public bool AdjustDamage(int Amount)
		{
			this.BaseDamage = DieRoll.AdjustResult(this.BaseDamage, Amount);
			DamageConstantAdjustedEvent.Send(this.ParentObject, this, Amount);
			return true;
		}

		// Token: 0x06008E60 RID: 36448 RVA: 0x00330C98 File Offset: 0x0032EE98
		public bool AdjustBonusCap(int Amount)
		{
			if (this.MaxStrengthBonus == 999)
			{
				return false;
			}
			this.MaxStrengthBonus += Amount;
			return true;
		}

		// Token: 0x06008E61 RID: 36449 RVA: 0x00330CB8 File Offset: 0x0032EEB8
		public bool IsEquippedOnPrimary()
		{
			if (this.ParentObject == null)
			{
				return false;
			}
			GameObject equipped = this.ParentObject.Equipped;
			return equipped != null && equipped.HasEquippedOnPrimary(this.ParentObject);
		}

		// Token: 0x06008E62 RID: 36450 RVA: 0x00330CEC File Offset: 0x0032EEEC
		public bool IsImprovisedWeapon()
		{
			return this.MaxStrengthBonus == 0 && this.PenBonus == 0 && this.HitBonus == 0 && this.BaseDamage == "1d2" && this.Ego == 0 && this.Skill == "Cudgel" && this.Stat == "Strength" && this.Slot == "Hand" && this.Attributes.IsNullOrEmpty();
		}

		// Token: 0x04003B67 RID: 15207
		[GeneratedCode("PoolPartialsGenerator", "1.0.0.0")]
		private static readonly IPartPool MeleeWeaponPool = new IPartPool();

		// Token: 0x04003B68 RID: 15208
		public const int BONUS_CAP_UNLIMITED = 999;

		// Token: 0x04003B69 RID: 15209
		public int MaxStrengthBonus;

		// Token: 0x04003B6A RID: 15210
		public int PenBonus;

		// Token: 0x04003B6B RID: 15211
		public int HitBonus;

		// Token: 0x04003B6C RID: 15212
		public string BaseDamage = "5";

		// Token: 0x04003B6D RID: 15213
		public int Ego;

		// Token: 0x04003B6E RID: 15214
		public string Skill = "Cudgel";

		// Token: 0x04003B6F RID: 15215
		public string Stat = "Strength";

		// Token: 0x04003B70 RID: 15216
		public string Slot = "Hand";

		// Token: 0x04003B71 RID: 15217
		public string Attributes;
	}
}
