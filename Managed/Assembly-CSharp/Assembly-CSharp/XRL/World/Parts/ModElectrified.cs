using System;
using XRL.Rules;

namespace XRL.World.Parts
{
	/// <remarks>
	/// overload behavior: if <see cref="!:IsPowerLoadSensitive" /> is true,
	/// which it is by default, the item's tier for purposes of calculating
	/// damage behavior is increased by the standard power load bonus, i.e.
	/// 2 for the standard overload power load of 400.
	/// </remarks>
	// Token: 0x02001134 RID: 4404
	[Serializable]
	public class ModElectrified : IMeleeModification
	{
		// Token: 0x0600AE72 RID: 44658 RVA: 0x00406F62 File Offset: 0x00405162
		public ModElectrified()
		{
		}

		// Token: 0x0600AE73 RID: 44659 RVA: 0x00406F6A File Offset: 0x0040516A
		public ModElectrified(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600AE74 RID: 44660 RVA: 0x00406F73 File Offset: 0x00405173
		public override void Configure()
		{
			this.WorksOnSelf = true;
			this.ChargeUse = 10;
			this.IsEMPSensitive = true;
			this.IsBootSensitive = true;
			this.IsPowerLoadSensitive = true;
			base.IsTechScannable = true;
			this.NameForStatus = "ArcDischarger";
		}

		// Token: 0x0600AE75 RID: 44661 RVA: 0x00406FAB File Offset: 0x004051AB
		public override void ApplyModification(GameObject Object)
		{
			Object.RequirePart<EnergyCellSocket>(false);
			base.IncreaseDifficultyAndComplexity(1, 1, null);
		}

		// Token: 0x0600AE76 RID: 44662 RVA: 0x00406FBE File Offset: 0x004051BE
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<BeforeMeleeAttackEvent>.ID || ID == ExamineCriticalFailureEvent.ID || ID == ExamineFailureEvent.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == PooledEvent<GetItemElementsEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600AE77 RID: 44663 RVA: 0x00406FFC File Offset: 0x004051FC
		public override bool HandleEvent(BeforeMeleeAttackEvent E)
		{
			if (E.Weapon == this.ParentObject && base.WasReady())
			{
				base.PlayWorldSound("Sounds/Enhancements/sfx_enhancement_electric_attack", 0.5f, 0f, true, null, 0f, 1f, 1f, int.MaxValue);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AE78 RID: 44664 RVA: 0x00407051 File Offset: 0x00405251
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddAdjective("{{electrical|electrified}}", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AE79 RID: 44665 RVA: 0x0040707B File Offset: 0x0040527B
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(this.GetInstanceDescription(), base.GetEventSensitiveAddStatusSummary(E));
			return base.HandleEvent(E);
		}

		// Token: 0x0600AE7A RID: 44666 RVA: 0x0040709D File Offset: 0x0040529D
		public override bool HandleEvent(ExamineFailureEvent E)
		{
			return !this.ExamineFailure(E, 25) && base.HandleEvent(E);
		}

		// Token: 0x0600AE7B RID: 44667 RVA: 0x004070B3 File Offset: 0x004052B3
		public override bool HandleEvent(ExamineCriticalFailureEvent E)
		{
			return !this.ExamineFailure(E, 50) && base.HandleEvent(E);
		}

		// Token: 0x0600AE7C RID: 44668 RVA: 0x004070C9 File Offset: 0x004052C9
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantObject(this.ParentObject))
			{
				E.Add("circuitry", 5);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AE7D RID: 44669 RVA: 0x004070EC File Offset: 0x004052EC
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600AE7E RID: 44670 RVA: 0x004070EF File Offset: 0x004052EF
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("WeaponHit");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600AE7F RID: 44671 RVA: 0x00407104 File Offset: 0x00405304
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
					int lowDamage = this.GetLowDamage(num);
					int highDamage = this.GetHighDamage(num);
					int num4 = Stat.Random(lowDamage, highDamage);
					string damageRange = this.GetDamageRange(num);
					GameObject gameObject = gameObjectParameter;
					Cell cell = null;
					GameObject gameObject2 = gameObjectParameter2;
					gameObject.Discharge(cell, num4, 0, damageRange, null, gameObjectParameter, this.ParentObject, gameObject2, null, null, null, null, 0, false, false, false);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600AE80 RID: 44672 RVA: 0x004071BC File Offset: 0x004053BC
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
				bool flag9 = false;
				bool flag10 = false;
				int num2 = 1;
				int? num3 = new int?(num);
				if (base.IsReady(flag, flag2, flag3, flag4, flag5, flag6, flag7, flag8, flag9, flag10, num2, null, false, 0L, num3))
				{
					int lowDamage = this.GetLowDamage(num);
					int highDamage = this.GetHighDamage(num);
					int num4 = Stat.Random(lowDamage, highDamage);
					string damageRange = this.GetDamageRange(num);
					GameObject parentObject = this.ParentObject;
					Cell cell = null;
					GameObject actor = E.Actor;
					if (parentObject.Discharge(cell, num4, 0, damageRange, null, E.Actor, this.ParentObject, actor, this.ParentObject, null, null, null, 0, false, false, E.Actor.IsPlayer()) > 0)
					{
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

		// Token: 0x0600AE81 RID: 44673 RVA: 0x00407298 File Offset: 0x00405498
		public static int GetLowDamage(int Tier, int PowerLoad = 100)
		{
			return Tier + IComponent<GameObject>.PowerLoadBonus(PowerLoad, 100, 150);
		}

		// Token: 0x0600AE82 RID: 44674 RVA: 0x004072A9 File Offset: 0x004054A9
		public int GetLowDamage(int PowerLoad = 100)
		{
			return ModElectrified.GetLowDamage(this.Tier, PowerLoad);
		}

		// Token: 0x0600AE83 RID: 44675 RVA: 0x004072B7 File Offset: 0x004054B7
		public static int GetHighDamage(int Tier, int PowerLoad = 100)
		{
			return (Tier + IComponent<GameObject>.PowerLoadBonus(PowerLoad, 100, 150)) * 3 / 2;
		}

		// Token: 0x0600AE84 RID: 44676 RVA: 0x004072CC File Offset: 0x004054CC
		public int GetHighDamage(int PowerLoad = 100)
		{
			return ModElectrified.GetHighDamage(this.Tier, PowerLoad);
		}

		// Token: 0x0600AE85 RID: 44677 RVA: 0x004072DC File Offset: 0x004054DC
		public static string GetDamageRange(int Tier, int PowerLoad = 100)
		{
			int lowDamage = ModElectrified.GetLowDamage(Tier, PowerLoad);
			int highDamage = ModElectrified.GetHighDamage(Tier, PowerLoad);
			if (lowDamage == highDamage)
			{
				return lowDamage.ToString();
			}
			return lowDamage.ToString() + "-" + highDamage.ToString();
		}

		// Token: 0x0600AE86 RID: 44678 RVA: 0x0040731D File Offset: 0x0040551D
		public string GetDamageRange(int PowerLoad = 100)
		{
			return ModElectrified.GetDamageRange(this.Tier, PowerLoad);
		}

		// Token: 0x0600AE87 RID: 44679 RVA: 0x0040732B File Offset: 0x0040552B
		public static string GetDescription(int Tier)
		{
			return "Electrified: When powered, this weapon deals " + ((Tier > 0) ? ("an additional " + ModElectrified.GetDamageRange(Tier, 100)) : "additional") + " electrical damage on hit.";
		}

		// Token: 0x0600AE88 RID: 44680 RVA: 0x0040735C File Offset: 0x0040555C
		public string GetInstanceDescription()
		{
			int num = this.MyPowerLoadLevel();
			return "Electrified: When powered, this weapon deals an additional " + ModElectrified.GetDamageRange(this.Tier, num) + " electrical damage on hit.";
		}
	}
}
