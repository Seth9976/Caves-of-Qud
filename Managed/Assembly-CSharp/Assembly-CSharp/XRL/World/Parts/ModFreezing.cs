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
	// Token: 0x0200113D RID: 4413
	[Serializable]
	public class ModFreezing : IMeleeModification
	{
		// Token: 0x0600AEFF RID: 44799 RVA: 0x00409733 File Offset: 0x00407933
		public ModFreezing()
		{
		}

		// Token: 0x0600AF00 RID: 44800 RVA: 0x0040973B File Offset: 0x0040793B
		public ModFreezing(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600AF01 RID: 44801 RVA: 0x00409744 File Offset: 0x00407944
		public override void Configure()
		{
			this.WorksOnSelf = true;
			this.ChargeUse = 10;
			this.IsEMPSensitive = true;
			this.IsBootSensitive = true;
			this.IsPowerLoadSensitive = true;
			base.IsTechScannable = true;
			this.NameForStatus = "HeatPump";
		}

		// Token: 0x0600AF02 RID: 44802 RVA: 0x0040977C File Offset: 0x0040797C
		public override bool ModificationApplicable(GameObject Object)
		{
			return !Object.HasPart<ModRelicFreezing>();
		}

		// Token: 0x0600AF03 RID: 44803 RVA: 0x00409789 File Offset: 0x00407989
		public override void ApplyModification(GameObject Object)
		{
			if (this.ChargeUse > 0)
			{
				Object.RequirePart<EnergyCellSocket>(false);
			}
			if (base.IsTechScannable)
			{
				base.IncreaseDifficultyAndComplexity(1, 1, null);
			}
		}

		// Token: 0x0600AF04 RID: 44804 RVA: 0x004097AD File Offset: 0x004079AD
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<BeforeMeleeAttackEvent>.ID || ID == ExamineCriticalFailureEvent.ID || ID == ExamineFailureEvent.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == PooledEvent<GetItemElementsEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600AF05 RID: 44805 RVA: 0x004097EC File Offset: 0x004079EC
		public override bool HandleEvent(BeforeMeleeAttackEvent E)
		{
			if (E.Weapon == this.ParentObject && base.WasReady())
			{
				base.PlayWorldSound("Sounds/Enhancements/sfx_enhancement_cold", 0.5f, 0f, true, null, 0f, 1f, 1f, int.MaxValue);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AF06 RID: 44806 RVA: 0x00409841 File Offset: 0x00407A41
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddAdjective("{{freezing|freezing}}", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AF07 RID: 44807 RVA: 0x0040986B File Offset: 0x00407A6B
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(this.GetInstanceDescription(), base.GetEventSensitiveAddStatusSummary(E));
			return base.HandleEvent(E);
		}

		// Token: 0x0600AF08 RID: 44808 RVA: 0x0040988D File Offset: 0x00407A8D
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantObject(this.ParentObject))
			{
				E.Add("ice", 5);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AF09 RID: 44809 RVA: 0x004098B0 File Offset: 0x00407AB0
		public override bool HandleEvent(ExamineFailureEvent E)
		{
			return !this.ExamineFailure(E, 25) && base.HandleEvent(E);
		}

		// Token: 0x0600AF0A RID: 44810 RVA: 0x004098C6 File Offset: 0x00407AC6
		public override bool HandleEvent(ExamineCriticalFailureEvent E)
		{
			return !this.ExamineFailure(E, 50) && base.HandleEvent(E);
		}

		// Token: 0x0600AF0B RID: 44811 RVA: 0x004098DC File Offset: 0x00407ADC
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600AF0C RID: 44812 RVA: 0x004098DF File Offset: 0x00407ADF
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("WeaponHit");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600AF0D RID: 44813 RVA: 0x004098F4 File Offset: 0x00407AF4
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
					gameObjectParameter2.TakeDamage(Stat.Random(this.GetLowDamage(num), this.GetHighDamage(num)), "from %t freezing weapon!", "Cold", null, null, null, gameObjectParameter, null, null, null, false, false, false, false, false, false, false, false, false, 5, null);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600AF0E RID: 44814 RVA: 0x004099C4 File Offset: 0x00407BC4
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
					if (actor.TakeDamage(num4, "from " + this.ParentObject.t(2147483647, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + "!", "Cold", null, null, null, actor2, null, null, null, false, false, false, false, false, false, true, false, E.Actor.IsPlayer(), 5, "{{icy|cold damage}}"))
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

		// Token: 0x0600AF0F RID: 44815 RVA: 0x00409AFC File Offset: 0x00407CFC
		public static int GetLowDamage(int Tier, int PowerLoad = 100)
		{
			return (int)Math.Round((double)(Tier + IComponent<GameObject>.PowerLoadBonus(PowerLoad, 100, 150)) * 0.8);
		}

		// Token: 0x0600AF10 RID: 44816 RVA: 0x00409B1E File Offset: 0x00407D1E
		public int GetLowDamage(int PowerLoad = 100)
		{
			return ModFreezing.GetLowDamage(this.Tier, PowerLoad);
		}

		// Token: 0x0600AF11 RID: 44817 RVA: 0x00409B2C File Offset: 0x00407D2C
		public static int GetHighDamage(int Tier, int PowerLoad = 100)
		{
			return (int)Math.Round((double)(Tier + IComponent<GameObject>.PowerLoadBonus(PowerLoad, 100, 150)) * 1.2);
		}

		// Token: 0x0600AF12 RID: 44818 RVA: 0x00409B4E File Offset: 0x00407D4E
		public int GetHighDamage(int PowerLoad = 100)
		{
			return ModFreezing.GetHighDamage(this.Tier, PowerLoad);
		}

		// Token: 0x0600AF13 RID: 44819 RVA: 0x00409B5C File Offset: 0x00407D5C
		public static string GetDamageRange(int Tier, int PowerLoad = 100)
		{
			int lowDamage = ModFreezing.GetLowDamage(Tier, PowerLoad);
			int highDamage = ModFreezing.GetHighDamage(Tier, PowerLoad);
			if (lowDamage == highDamage)
			{
				return lowDamage.ToString();
			}
			return lowDamage.ToString() + "-" + highDamage.ToString();
		}

		// Token: 0x0600AF14 RID: 44820 RVA: 0x00409B9D File Offset: 0x00407D9D
		public string GetDamageRange(int PowerLoad = 100)
		{
			return ModFreezing.GetDamageRange(this.Tier, PowerLoad);
		}

		// Token: 0x0600AF15 RID: 44821 RVA: 0x00409BAC File Offset: 0x00407DAC
		public string GetTemperatureChangeRange(int PowerLoad = 100)
		{
			return "-" + (this.Tier + IComponent<GameObject>.PowerLoadBonus(PowerLoad, 100, 150)).ToString() + "d4";
		}

		// Token: 0x0600AF16 RID: 44822 RVA: 0x00409BE4 File Offset: 0x00407DE4
		public static string GetDescription(int Tier)
		{
			return "Freezing: When powered, this weapon deals " + ((Tier > 0) ? ("an additional " + ModFreezing.GetDamageRange(Tier, 100)) : "additional") + " cold damage on hit.";
		}

		// Token: 0x0600AF17 RID: 44823 RVA: 0x00409C14 File Offset: 0x00407E14
		public string GetInstanceDescription()
		{
			int num = this.MyPowerLoadLevel();
			return "Freezing: When powered, this weapon deals an additional " + ModFreezing.GetDamageRange(this.Tier, num) + " cold damage on hit.";
		}
	}
}
