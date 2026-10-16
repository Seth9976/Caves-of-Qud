using System;
using XRL.Rules;

namespace XRL.World.Parts
{
	/// <remarks>
	/// overload behavior: damage and voltage are increased by the standard
	/// power load bonus, i.e. 2 for the standard overload power load of 400.
	/// </remarks>
	// Token: 0x020010D9 RID: 4313
	[Serializable]
	public class DischargeOnHit : IActivePart
	{
		// Token: 0x0600AB4F RID: 43855 RVA: 0x003EAFD0 File Offset: 0x003E91D0
		public DischargeOnHit()
		{
			this.WorksOnSelf = true;
			this.NameForStatus = "DischargeGenerator";
		}

		// Token: 0x0600AB50 RID: 43856 RVA: 0x003EB000 File Offset: 0x003E9200
		public DischargeOnHit(string Voltage, string DamageRange)
			: this()
		{
			this.Voltage = Voltage;
			this.DamageRange = DamageRange;
		}

		// Token: 0x0600AB51 RID: 43857 RVA: 0x003EB016 File Offset: 0x003E9216
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600AB52 RID: 43858 RVA: 0x003EB01C File Offset: 0x003E921C
		public override bool SameAs(IPart p)
		{
			DischargeOnHit dischargeOnHit = p as DischargeOnHit;
			return !(dischargeOnHit.Voltage != this.Voltage) && !(dischargeOnHit.DamageRange != this.DamageRange) && base.SameAs(p);
		}

		// Token: 0x0600AB53 RID: 43859 RVA: 0x003EB061 File Offset: 0x003E9261
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AdjustWeaponScore");
			Registrar.Register("WeaponHit");
			Registrar.Register("ProjectileHit");
			Registrar.Register("WeaponThrowHit");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600AB54 RID: 43860 RVA: 0x003EB098 File Offset: 0x003E9298
		public override bool FireEvent(Event E)
		{
			if (E.ID == "WeaponHit" || E.ID == "ProjectileHit" || E.ID == "WeaponThrowHit")
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
					int num4 = this.MyPowerLoadBonus(num, 100, 150);
					int num5 = this.Voltage.RollCached() + num4;
					string text = this.DamageRange;
					if (num4 != 0)
					{
						text = DieRoll.AdjustResult(text, num4);
					}
					if (E.ID == "WeaponHit")
					{
						GameObject gameObject = gameObjectParameter;
						Cell cell = null;
						GameObject gameObject2 = gameObjectParameter2;
						gameObject.Discharge(cell, num5, 0, text, null, gameObjectParameter, this.ParentObject, gameObject2, null, null, null, null, 0, false, false, false);
					}
					else
					{
						GameObject gameObject3 = gameObjectParameter2;
						Cell cell2 = null;
						GameObject gameObject2 = gameObjectParameter2;
						gameObject3.Discharge(cell2, num5, 0, text, null, gameObjectParameter, this.ParentObject, gameObject2, null, null, null, null, 0, false, false, false);
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
				int num6 = 1;
				int? num3 = null;
				int? num7 = num3;
				bool flag21 = false;
				long num8 = 0L;
				num3 = null;
				if (base.IsReady(flag11, flag12, flag13, flag14, flag15, flag16, flag17, flag18, flag19, flag20, num6, num7, flag21, num8, num3))
				{
					int intParameter = E.GetIntParameter("Score", 0);
					int num9 = this.MyPowerLoadBonus(int.MinValue, 100, 150);
					int num10 = Math.Max(this.Voltage.RollMinCached() / 2 + this.Voltage.RollMaxCached() / 4 + num9 + (this.DamageRange.RollMinCached() + this.DamageRange.RollMaxCached() / 2 + num9), 1);
					E.SetParameter("Score", intParameter + num10);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x040046A8 RID: 18088
		public string Voltage = "2d4";

		// Token: 0x040046A9 RID: 18089
		public string DamageRange = "1d4";
	}
}
