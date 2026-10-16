using System;
using XRL.Rules;

namespace XRL.World.Parts
{
	/// <remarks>
	/// overload behavior: temperature changes are increased by a percentage
	/// equal to ((power load - 100) / 10), i.e. 30% for the standard overload
	/// power load of 400.
	/// </remarks>
	// Token: 0x020010F8 RID: 4344
	[Serializable]
	public class TemperatureOnEntering : IPart
	{
		// Token: 0x0600ABF7 RID: 44023 RVA: 0x003F080C File Offset: 0x003EEA0C
		public override bool SameAs(IPart Part)
		{
			TemperatureOnEntering temperatureOnEntering = Part as TemperatureOnEntering;
			return !(temperatureOnEntering.Amount != this.Amount) && temperatureOnEntering.Max == this.Max && temperatureOnEntering.MaxTemp == this.MaxTemp && temperatureOnEntering.OnWielderHit == this.OnWielderHit && base.SameAs(Part);
		}

		// Token: 0x0600ABF8 RID: 44024 RVA: 0x003F086C File Offset: 0x003EEA6C
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ProjectileEntering");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600ABF9 RID: 44025 RVA: 0x003F0884 File Offset: 0x003EEA84
		public override bool FireEvent(Event E)
		{
			if (E.ID == "ProjectileEntering")
			{
				Cell cell = E.GetParameter("Cell") as Cell;
				GameObject gameObjectParameter = E.GetGameObjectParameter("Attacker");
				int phase = this.ParentObject.GetPhase();
				int num = this.MyPowerLoadBonus(int.MinValue, 100, 10);
				foreach (GameObject gameObject in cell.GetObjectsWithPart("Physics"))
				{
					if (!this.Max || (Stat.RollMax(this.Amount) > 0 && gameObject.Physics.Temperature < this.MaxTemp) || (Stat.RollMax(this.Amount) < 0 && gameObject.Physics.Temperature > this.MaxTemp))
					{
						int num2 = this.Amount.RollCached();
						if (num != 0)
						{
							num2 = num2 * (100 + num) / 100;
						}
						gameObject.TemperatureChange(num2, gameObjectParameter, false, false, false, false, phase, null, null);
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004725 RID: 18213
		public string Amount = "0";

		// Token: 0x04004726 RID: 18214
		public bool Max;

		// Token: 0x04004727 RID: 18215
		public int MaxTemp = 400;

		// Token: 0x04004728 RID: 18216
		public bool OnWielderHit;
	}
}
