using System;
using XRL.Rules;

namespace XRL.World.Parts
{
	/// <remarks>
	/// Adds elemental damage on <c>"WeaponHit"</c> when part is active. 
	/// Uses the standard <see cref="T:XRL.World.Parts.IActivePart" /> interfaces, defaulting <see cref="!:WorksOnSelf" /> to true.
	/// overload behavior: if <see cref="!:IsPowerLoadSensitive" /> is true,
	/// which it is not by default, chance to activate is increased by a
	/// percentage equal to ((power load - 100) / 10), i.e. 30% for
	/// the standard overload power load of 400, and damage is increased
	/// by the standard power load bonus, i.e. 2 for the standard overload
	/// power load of 400.
	/// </remarks>
	// Token: 0x020010DD RID: 4317
	[Serializable]
	public class ElementalDamage : IActivePart
	{
		// Token: 0x0600AB61 RID: 43873 RVA: 0x003EB737 File Offset: 0x003E9937
		public ElementalDamage()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600AB62 RID: 43874 RVA: 0x003EB764 File Offset: 0x003E9964
		public override bool SameAs(IPart p)
		{
			ElementalDamage elementalDamage = p as ElementalDamage;
			return elementalDamage.Chance == this.Chance && !(elementalDamage.Damage != this.Damage) && !(elementalDamage.Attributes != this.Attributes) && base.SameAs(p);
		}

		// Token: 0x0600AB63 RID: 43875 RVA: 0x003EB7B9 File Offset: 0x003E99B9
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<BeforeMeleeAttackEvent>.ID || ID == PooledEvent<GetItemElementsEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600AB64 RID: 43876 RVA: 0x003EB7E0 File Offset: 0x003E99E0
		public override bool HandleEvent(BeforeMeleeAttackEvent E)
		{
			if (E.Weapon == this.ParentObject && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				GameObject actor = E.Actor;
				GameObject target = E.Target;
				GameObject weapon = E.Weapon;
				int effectiveChance = this.GetEffectiveChance(null, actor, target, weapon);
				this._WillActivateOnNextHit = effectiveChance.in100();
				if (this._WillActivateOnNextHit)
				{
					if (XRL.World.Damage.ContainsElectricDamage(this.Attributes))
					{
						base.PlayWorldSound("Sounds/Enhancements/sfx_enhancement_electric_attack", 0.5f, 0f, true, null, 0f, 1f, 1f, int.MaxValue);
					}
					if (XRL.World.Damage.ContainsColdDamage(this.Attributes))
					{
						base.PlayWorldSound("Sounds/Enhancements/sfx_enhancement_cold", 0.5f, 0f, true, null, 0f, 1f, 1f, int.MaxValue);
					}
					if (XRL.World.Damage.ContainsHeatDamage(this.Attributes))
					{
						base.PlayWorldSound("Sounds/Enhancements/sfx_enhancement_fire_attack", 0.5f, 0f, true, null, 0f, 1f, 1f, int.MaxValue);
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AB65 RID: 43877 RVA: 0x003EB91C File Offset: 0x003E9B1C
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantObject(this.ParentObject))
			{
				if (XRL.World.Damage.IsColdDamage(this.Attributes))
				{
					E.Add("ice", 1);
				}
				else if (XRL.World.Damage.IsElectricDamage(this.Attributes))
				{
					E.Add("circuitry", 1);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AB66 RID: 43878 RVA: 0x003EB974 File Offset: 0x003E9B74
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			int num = this.MyPowerLoadBonus(int.MinValue, 100, 150);
			string text = this.Damage;
			if (num != 0)
			{
				text = DieRoll.AdjustResult(text, num);
			}
			int effectiveChance = this.GetEffectiveChance(null, null, null, null);
			E.Postfix.AppendRules(string.Concat(new string[]
			{
				"This weapon deals ",
				text,
				" ",
				this.Attributes.ToLower(),
				" damage on hit",
				(effectiveChance < 100) ? (" " + effectiveChance.ToString() + "% of the time") : "",
				"."
			}));
			return base.HandleEvent(E);
		}

		// Token: 0x0600AB67 RID: 43879 RVA: 0x003EBA2E File Offset: 0x003E9C2E
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600AB68 RID: 43880 RVA: 0x003EBA31 File Offset: 0x003E9C31
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AdjustWeaponScore");
			Registrar.Register("WeaponHit");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600AB69 RID: 43881 RVA: 0x003EBA54 File Offset: 0x003E9C54
		public override bool FireEvent(Event E)
		{
			if (E.ID == "WeaponHit")
			{
				int num = this.MyPowerLoadLevel();
				GameObject gameObjectParameter = E.GetGameObjectParameter("Attacker");
				GameObject gameObjectParameter2 = E.GetGameObjectParameter("Defender");
				GameObject gameObjectParameter3 = E.GetGameObjectParameter("Weapon");
				if (this._WillActivateOnNextHit)
				{
					bool flag = true;
					bool flag2 = false;
					bool flag3 = false;
					bool flag4 = false;
					bool flag5 = false;
					bool flag6 = false;
					bool flag7 = false;
					bool flag8 = false;
					bool flag9 = false;
					bool flag10 = false;
					int num2 = 1;
					int? num3 = new int?(num);
					if (base.IsReady(flag, flag2, flag3, flag4, flag5, flag6, flag7, flag8, flag9, flag10, num2, null, false, 0L, num3))
					{
						string text;
						if (gameObjectParameter3 == null || IComponent<GameObject>.TerseMessages)
						{
							text = "from %t attack.";
						}
						else
						{
							text = "from " + gameObjectParameter.poss(gameObjectParameter3, true, null) + ".";
						}
						GameObject gameObject = gameObjectParameter2;
						int num4 = this.Damage.RollCached() + IComponent<GameObject>.PowerLoadBonus(num, 100, 150);
						string attributes = this.Attributes;
						GameObject gameObject2 = gameObjectParameter;
						GameObject gameObject3 = gameObjectParameter3;
						string text2 = this.Attributes.ToLower() + " damage";
						if (gameObject.TakeDamage(num4, text, attributes, null, null, gameObject2, null, gameObject3, null, null, false, false, false, false, false, false, false, false, false, 5, text2))
						{
							E.SetFlag("DidSpecialEffect", true);
						}
						if (!string.IsNullOrEmpty(this.Attributes))
						{
							if (this.Attributes.Contains("Umbral"))
							{
								gameObjectParameter2.ParticleBlip("&K-", 10, 0L, false);
								gameObjectParameter2.Acidsplatter();
							}
							if (XRL.World.Damage.ContainsAcidDamage(this.Attributes))
							{
								gameObjectParameter2.ParticleBlip("&G*", 10, 0L, false);
								gameObjectParameter2.Acidsplatter();
							}
							if (XRL.World.Damage.ContainsElectricDamage(this.Attributes))
							{
								gameObjectParameter2.ParticleBlip("&W*", 10, 0L, false);
								gameObjectParameter2.Sparksplatter();
							}
							if (XRL.World.Damage.ContainsColdDamage(this.Attributes))
							{
								gameObjectParameter2.ParticleBlip("&C*", 10, 0L, false);
								gameObjectParameter2.Icesplatter();
							}
							if (XRL.World.Damage.ContainsHeatDamage(this.Attributes))
							{
								gameObjectParameter2.ParticleBlip("&C*", 10, 0L, false);
								gameObjectParameter2.Firesplatter();
							}
						}
					}
				}
			}
			else if (E.ID == "AdjustWeaponScore")
			{
				bool flag11 = false;
				bool flag12 = false;
				bool flag13 = false;
				bool flag14 = false;
				bool flag15 = false;
				bool flag16 = false;
				bool flag17 = false;
				bool flag18 = false;
				bool flag19 = false;
				bool flag20 = false;
				int num5 = 1;
				int? num3 = null;
				int? num6 = num3;
				bool flag21 = false;
				long num7 = 0L;
				num3 = null;
				if (base.IsReady(flag11, flag12, flag13, flag14, flag15, flag16, flag17, flag18, flag19, flag20, num5, num6, flag21, num7, num3))
				{
					int @for = GetSpecialEffectChanceEvent.GetFor(E.GetGameObjectParameter("User"), this.ParentObject, "Part ElementalDamage Activation", this.Chance, null, null, true, false);
					int num8 = this.MyPowerLoadBonus(int.MinValue, 100, 150);
					int num9 = ((this.Damage.RollMinCached() + num8) * 2 + (this.Damage.RollMaxCached() + num8)) * 2;
					if (@for < 100)
					{
						num9 = num9 * @for / 100;
					}
					E.SetParameter("Score", E.GetIntParameter("Score", 0) + num9);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600AB6A RID: 43882 RVA: 0x003EBD1C File Offset: 0x003E9F1C
		public int GetEffectiveChance(int? PowerLoadLevel = null, GameObject Attacker = null, GameObject Defender = null, GameObject Weapon = null)
		{
			int num = this.Chance;
			int num2 = IComponent<GameObject>.PowerLoadBonus(PowerLoadLevel ?? this.MyPowerLoadLevel(), 100, 10);
			if (num2 != 0)
			{
				num = num * (100 + num2) / 100;
			}
			return GetSpecialEffectChanceEvent.GetFor(Attacker, Weapon, "Part ElementalDamage Activation", num, Defender, null, true, false);
		}

		// Token: 0x040046AF RID: 18095
		public int Chance = 100;

		// Token: 0x040046B0 RID: 18096
		public string Damage = "1d4";

		// Token: 0x040046B1 RID: 18097
		public string Attributes = "Heat";

		// Token: 0x040046B2 RID: 18098
		private bool _WillActivateOnNextHit;
	}
}
