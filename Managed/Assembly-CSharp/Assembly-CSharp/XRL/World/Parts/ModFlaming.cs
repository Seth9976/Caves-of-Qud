using System;
using XRL.Rules;

namespace XRL.World.Parts
{
	/// <remarks>
	/// overload behavior: if <see cref="!:IsPowerLoadSensitive" /> is true,
	/// which it is by default, the item's tier for purposes of calculating
	/// damage is increased by the standard power load bonus, i.e. 2 for the
	/// standard overload power load of 400.
	/// </remarks>
	// Token: 0x0200113B RID: 4411
	[Serializable]
	public class ModFlaming : IMeleeModification
	{
		// Token: 0x0600AEDD RID: 44765 RVA: 0x004090E6 File Offset: 0x004072E6
		public ModFlaming()
		{
		}

		// Token: 0x0600AEDE RID: 44766 RVA: 0x004090EE File Offset: 0x004072EE
		public ModFlaming(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600AEDF RID: 44767 RVA: 0x004090F7 File Offset: 0x004072F7
		public override void Configure()
		{
			this.WorksOnSelf = true;
			this.ChargeUse = 10;
			this.IsEMPSensitive = true;
			this.IsBootSensitive = true;
			this.IsPowerLoadSensitive = true;
			base.IsTechScannable = true;
			this.NameForStatus = "HeatingElement";
		}

		// Token: 0x0600AEE0 RID: 44768 RVA: 0x0040912F File Offset: 0x0040732F
		public override void ApplyModification(GameObject Object)
		{
			Object.RequirePart<EnergyCellSocket>(false);
			base.IncreaseDifficultyAndComplexity(1, 1, null);
		}

		// Token: 0x0600AEE1 RID: 44769 RVA: 0x00409142 File Offset: 0x00407342
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<BeforeMeleeAttackEvent>.ID || ID == ExamineCriticalFailureEvent.ID || ID == ExamineFailureEvent.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600AEE2 RID: 44770 RVA: 0x00409178 File Offset: 0x00407378
		public override bool HandleEvent(BeforeMeleeAttackEvent E)
		{
			if (E.Weapon == this.ParentObject && base.WasReady())
			{
				base.PlayWorldSound("Sounds/Enhancements/sfx_enhancement_fire_attack", 0.5f, 0f, true, null, 0f, 1f, 1f, int.MaxValue);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AEE3 RID: 44771 RVA: 0x004091CD File Offset: 0x004073CD
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddAdjective("{{fiery|flaming}}", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AEE4 RID: 44772 RVA: 0x004091F7 File Offset: 0x004073F7
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(this.GetInstanceDescription(), base.GetEventSensitiveAddStatusSummary(E));
			return base.HandleEvent(E);
		}

		// Token: 0x0600AEE5 RID: 44773 RVA: 0x00409219 File Offset: 0x00407419
		public override bool HandleEvent(ExamineFailureEvent E)
		{
			return !this.ExamineFailure(E, 25) && base.HandleEvent(E);
		}

		// Token: 0x0600AEE6 RID: 44774 RVA: 0x0040922F File Offset: 0x0040742F
		public override bool HandleEvent(ExamineCriticalFailureEvent E)
		{
			return !this.ExamineFailure(E, 50) && base.HandleEvent(E);
		}

		// Token: 0x0600AEE7 RID: 44775 RVA: 0x00409245 File Offset: 0x00407445
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600AEE8 RID: 44776 RVA: 0x00409248 File Offset: 0x00407448
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("WeaponHit");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600AEE9 RID: 44777 RVA: 0x00409260 File Offset: 0x00407460
		public override bool FireEvent(Event E)
		{
			if (E.ID == "WeaponHit")
			{
				int num = this.MyPowerLoadLevel();
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
					GameObject gameObjectParameter = E.GetGameObjectParameter("Attacker");
					GameObject gameObjectParameter2 = E.GetGameObjectParameter("Defender");
					int num4 = this.GetTemperatureChangeRange(num).RollCached();
					GameObject gameObject = gameObjectParameter;
					bool flag11 = false;
					bool flag12 = false;
					bool flag13 = false;
					bool flag14 = false;
					int num5 = 0;
					num3 = null;
					int? num6 = num3;
					num3 = null;
					gameObjectParameter2.TemperatureChange(num4, gameObject, flag11, flag12, flag13, flag14, num5, num6, num3);
					gameObjectParameter2.TakeDamage(Stat.Random(this.GetLowDamage(num), this.GetHighDamage(num)), "from %t flaming weapon!", "Fire", null, null, null, gameObjectParameter, null, null, null, false, false, false, false, false, false, false, false, false, 5, null);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600AEEA RID: 44778 RVA: 0x00409330 File Offset: 0x00407530
		private bool ExamineFailure(IExamineEvent E, int Chance)
		{
			if (E.Pass == 1 && GlobalConfig.GetBoolSetting("ContextualExamineFailures", false) && Chance.in100())
			{
				int num = this.MyPowerLoadLevel();
				bool flag = true;
				bool flag2 = false;
				bool flag3 = false;
				bool flag4 = false;
				bool flag5 = false;
				bool flag6 = false;
				bool flag7 = false;
				bool flag8 = false;
				bool flag9 = true;
				bool flag10 = false;
				int num2 = 1;
				int? num3 = new int?(num);
				if (base.IsReady(flag, flag2, flag3, flag4, flag5, flag6, flag7, flag8, flag9, flag10, num2, null, false, 0L, num3))
				{
					GameObject actor = E.Actor;
					int num4 = Stat.Random(this.GetLowDamage(num), this.GetHighDamage(num));
					GameObject actor2 = E.Actor;
					if (actor.TakeDamage(num4, "from " + this.ParentObject.t(2147483647, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + "!", "Fire", null, null, null, actor2, null, null, null, false, false, false, false, false, false, true, false, E.Actor.IsPlayer(), 5, "{{fiery|heat damage}}"))
					{
						GameObject actor3 = E.Actor;
						int num5 = this.GetTemperatureChangeRange(num).RollCached();
						GameObject actor4 = E.Actor;
						bool flag11 = false;
						bool flag12 = false;
						bool flag13 = false;
						bool flag14 = false;
						int num6 = 0;
						num3 = null;
						int? num7 = num3;
						num3 = null;
						actor3.TemperatureChange(num5, actor4, flag11, flag12, flag13, flag14, num6, num7, num3);
						if (this.ParentObject.IsBlueprintUnderstood())
						{
							E.Identify = true;
						}
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600AEEB RID: 44779 RVA: 0x00409468 File Offset: 0x00407668
		public static int GetLowDamage(int Tier, int PowerLoad = 100)
		{
			return (int)Math.Round((double)(Tier + IComponent<GameObject>.PowerLoadBonus(PowerLoad, 100, 150)) * 0.8);
		}

		// Token: 0x0600AEEC RID: 44780 RVA: 0x0040948A File Offset: 0x0040768A
		public int GetLowDamage(int PowerLoad = 100)
		{
			return ModFlaming.GetLowDamage(this.Tier, PowerLoad);
		}

		// Token: 0x0600AEED RID: 44781 RVA: 0x00409498 File Offset: 0x00407698
		public static int GetHighDamage(int Tier, int PowerLoad = 100)
		{
			return (int)Math.Round((double)(Tier + IComponent<GameObject>.PowerLoadBonus(PowerLoad, 100, 150)) * 1.2);
		}

		// Token: 0x0600AEEE RID: 44782 RVA: 0x004094BA File Offset: 0x004076BA
		public int GetHighDamage(int PowerLoad = 100)
		{
			return ModFlaming.GetHighDamage(this.Tier, PowerLoad);
		}

		// Token: 0x0600AEEF RID: 44783 RVA: 0x004094C8 File Offset: 0x004076C8
		public static string GetDamageRange(int Tier, int PowerLoad = 100)
		{
			int lowDamage = ModFlaming.GetLowDamage(Tier, PowerLoad);
			int highDamage = ModFlaming.GetHighDamage(Tier, PowerLoad);
			if (lowDamage == highDamage)
			{
				return lowDamage.ToString();
			}
			return lowDamage.ToString() + "-" + highDamage.ToString();
		}

		// Token: 0x0600AEF0 RID: 44784 RVA: 0x00409509 File Offset: 0x00407709
		public string GetDamageRange(int PowerLoad = 100)
		{
			return ModFlaming.GetDamageRange(this.Tier, PowerLoad);
		}

		// Token: 0x0600AEF1 RID: 44785 RVA: 0x00409518 File Offset: 0x00407718
		public string GetTemperatureChangeRange(int PowerLoad = 100)
		{
			return (2 * (this.Tier + IComponent<GameObject>.PowerLoadBonus(PowerLoad, 100, 150))).ToString() + "d8";
		}

		// Token: 0x0600AEF2 RID: 44786 RVA: 0x0040954D File Offset: 0x0040774D
		public static string GetDescription(int Tier)
		{
			return "Flaming: When powered, this weapon deals " + ((Tier > 0) ? ("an additional " + ModFlaming.GetDamageRange(Tier, 100)) : "additional") + " heat damage on hit.";
		}

		// Token: 0x0600AEF3 RID: 44787 RVA: 0x0040957C File Offset: 0x0040777C
		public string GetInstanceDescription()
		{
			int num = this.MyPowerLoadLevel();
			return "Flaming: When powered, this weapon deals an additional " + ModFlaming.GetDamageRange(this.Tier, num) + " heat damage on hit.";
		}
	}
}
