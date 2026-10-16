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
	// Token: 0x02001163 RID: 4451
	[Serializable]
	public class ModRelicFreezing : IModification
	{
		// Token: 0x0600B0FF RID: 45311 RVA: 0x00410915 File Offset: 0x0040EB15
		public ModRelicFreezing()
		{
		}

		// Token: 0x0600B100 RID: 45312 RVA: 0x0041091D File Offset: 0x0040EB1D
		public ModRelicFreezing(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B101 RID: 45313 RVA: 0x00410926 File Offset: 0x0040EB26
		public override void Configure()
		{
			this.WorksOnSelf = true;
			this.ChargeUse = 8;
			this.IsEMPSensitive = true;
			this.IsBootSensitive = true;
			this.IsPowerLoadSensitive = true;
			base.IsTechScannable = true;
			this.NameForStatus = "HeatPump";
		}

		// Token: 0x0600B102 RID: 45314 RVA: 0x0041095D File Offset: 0x0040EB5D
		public override bool ModificationApplicable(GameObject Object)
		{
			return !Object.HasPart<ModFreezing>();
		}

		// Token: 0x0600B103 RID: 45315 RVA: 0x0041096A File Offset: 0x0040EB6A
		public override void ApplyModification(GameObject Object)
		{
			Object.RequirePart<EnergyCellSocket>(false);
		}

		// Token: 0x0600B104 RID: 45316 RVA: 0x00410974 File Offset: 0x0040EB74
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ExamineCriticalFailureEvent.ID || ID == ExamineFailureEvent.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600B105 RID: 45317 RVA: 0x0041099A File Offset: 0x0040EB9A
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(this.GetInstanceDescription(), base.GetEventSensitiveAddStatusSummary(E));
			return base.HandleEvent(E);
		}

		// Token: 0x0600B106 RID: 45318 RVA: 0x004109BC File Offset: 0x0040EBBC
		public override bool HandleEvent(ExamineFailureEvent E)
		{
			return !this.ExamineFailure(E, 25) && base.HandleEvent(E);
		}

		// Token: 0x0600B107 RID: 45319 RVA: 0x004109D2 File Offset: 0x0040EBD2
		public override bool HandleEvent(ExamineCriticalFailureEvent E)
		{
			return !this.ExamineFailure(E, 25) && base.HandleEvent(E);
		}

		// Token: 0x0600B108 RID: 45320 RVA: 0x004109E8 File Offset: 0x0040EBE8
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600B109 RID: 45321 RVA: 0x004109EB File Offset: 0x0040EBEB
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("WeaponHit");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600B10A RID: 45322 RVA: 0x00410A00 File Offset: 0x0040EC00
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

		// Token: 0x0600B10B RID: 45323 RVA: 0x00410AD0 File Offset: 0x0040ECD0
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

		// Token: 0x0600B10C RID: 45324 RVA: 0x00410C08 File Offset: 0x0040EE08
		public static int GetLowDamage(int Tier, int PowerLoad = 100)
		{
			return (int)Math.Round((double)(Tier + IComponent<GameObject>.PowerLoadBonus(PowerLoad, 100, 150)) * 1.6) + 3;
		}

		// Token: 0x0600B10D RID: 45325 RVA: 0x00410C2C File Offset: 0x0040EE2C
		public int GetLowDamage(int PowerLoad = 100)
		{
			return ModRelicFreezing.GetLowDamage(this.Tier, PowerLoad);
		}

		// Token: 0x0600B10E RID: 45326 RVA: 0x00410C3A File Offset: 0x0040EE3A
		public static int GetHighDamage(int Tier, int PowerLoad = 100)
		{
			return (int)Math.Round((double)(Tier + IComponent<GameObject>.PowerLoadBonus(PowerLoad, 100, 150)) * 2.4) + 3;
		}

		// Token: 0x0600B10F RID: 45327 RVA: 0x00410C5E File Offset: 0x0040EE5E
		public int GetHighDamage(int PowerLoad = 100)
		{
			return ModRelicFreezing.GetHighDamage(this.Tier, PowerLoad);
		}

		// Token: 0x0600B110 RID: 45328 RVA: 0x00410C6C File Offset: 0x0040EE6C
		public static string GetDamageRange(int Tier, int PowerLoad = 100)
		{
			int lowDamage = ModRelicFreezing.GetLowDamage(Tier, PowerLoad);
			int highDamage = ModRelicFreezing.GetHighDamage(Tier, PowerLoad);
			if (lowDamage == highDamage)
			{
				return lowDamage.ToString();
			}
			return lowDamage.ToString() + "-" + highDamage.ToString();
		}

		// Token: 0x0600B111 RID: 45329 RVA: 0x00410CAD File Offset: 0x0040EEAD
		public string GetDamageRange(int PowerLoad = 100)
		{
			return ModRelicFreezing.GetDamageRange(this.Tier, PowerLoad);
		}

		// Token: 0x0600B112 RID: 45330 RVA: 0x00410CBC File Offset: 0x0040EEBC
		public string GetTemperatureChangeRange(int PowerLoad = 100)
		{
			return "-" + (this.Tier + IComponent<GameObject>.PowerLoadBonus(PowerLoad, 100, 150)).ToString() + "d4";
		}

		// Token: 0x0600B113 RID: 45331 RVA: 0x00410CF4 File Offset: 0x0040EEF4
		public static string GetDescription(int Tier)
		{
			return "Freezing: When powered, this weapon deals " + ((Tier > 0) ? ("an additional " + ModRelicFreezing.GetDamageRange(Tier, 100)) : "") + " cold damage on hit.";
		}

		// Token: 0x0600B114 RID: 45332 RVA: 0x00410D24 File Offset: 0x0040EF24
		public string GetInstanceDescription()
		{
			int num = this.MyPowerLoadLevel();
			return "Freezing: When powered, this weapon deals an additional " + ModRelicFreezing.GetDamageRange(this.Tier, num) + " cold damage on hit.";
		}
	}
}
