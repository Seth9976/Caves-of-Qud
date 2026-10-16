using System;

namespace XRL.World.Parts
{
	/// <remarks>
	/// overload behavior: temperature changes are increased by a percentage
	/// equal to ((power load - 100) / 10), i.e. 30% for the standard overload
	/// power load of 400.
	/// </remarks>
	// Token: 0x020010F9 RID: 4345
	[Serializable]
	public class TemperatureOnHit : IPart
	{
		// Token: 0x0600ABFB RID: 44027 RVA: 0x003F09DA File Offset: 0x003EEBDA
		public TemperatureOnHit()
		{
		}

		// Token: 0x0600ABFC RID: 44028 RVA: 0x003F09F8 File Offset: 0x003EEBF8
		public TemperatureOnHit(string Amount, int Max)
			: this()
		{
			this.Amount = Amount;
			this.MaxTemp = Max;
		}

		// Token: 0x0600ABFD RID: 44029 RVA: 0x003F0A10 File Offset: 0x003EEC10
		public override bool SameAs(IPart p)
		{
			TemperatureOnHit temperatureOnHit = p as TemperatureOnHit;
			return !(temperatureOnHit.Amount != this.Amount) && temperatureOnHit.Max == this.Max && temperatureOnHit.MaxTemp == this.MaxTemp && temperatureOnHit.OnWielderHit == this.OnWielderHit && temperatureOnHit.RequiresLit == this.RequiresLit && base.SameAs(p);
		}

		// Token: 0x0600ABFE RID: 44030 RVA: 0x003F0A80 File Offset: 0x003EEC80
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<BeforeMeleeAttackEvent>.ID || ID == PooledEvent<GetItemElementsEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600ABFF RID: 44031 RVA: 0x003F0AA8 File Offset: 0x003EECA8
		public override bool HandleEvent(BeforeMeleeAttackEvent E)
		{
			if (E.Weapon == this.ParentObject && this.CheckRequirements())
			{
				int num = this.Amount.RollMaxCached();
				if (num > 0)
				{
					base.PlayWorldSound("Sounds/Enhancements/sfx_enhancement_fire_attack", 0.5f, 0f, true, null, 0f, 1f, 1f, int.MaxValue);
				}
				else if (num < 0)
				{
					base.PlayWorldSound("Sounds/Enhancements/sfx_enhancement_cold", 0.5f, 0f, true, null, 0f, 1f, 1f, int.MaxValue);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AC00 RID: 44032 RVA: 0x003F0B40 File Offset: 0x003EED40
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantObject(this.ParentObject))
			{
				if (this.Amount.RollMax() < 0)
				{
					E.Add("ice", 2);
				}
				else if (this.Amount.RollMin() > 0)
				{
					E.Add("salt", 1);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AC01 RID: 44033 RVA: 0x003F0B98 File Offset: 0x003EED98
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ProjectileHit");
			if (!this.OnWielderHit)
			{
				Registrar.Register("WeaponDealDamage");
			}
			Registrar.Register("WeaponHit");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600AC02 RID: 44034 RVA: 0x003F0BCC File Offset: 0x003EEDCC
		private bool CheckRequirements()
		{
			if (this.RequiresLit)
			{
				LightSource part = this.ParentObject.GetPart<LightSource>();
				if (part == null)
				{
					return false;
				}
				if (!part.Lit)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600AC03 RID: 44035 RVA: 0x003F0C00 File Offset: 0x003EEE00
		public override bool FireEvent(Event E)
		{
			if ((E.ID == "WeaponHit" || E.ID == "WeaponDealDamage" || E.ID == "ProjectileHit") && this.CheckRequirements())
			{
				E.GetGameObjectParameter("Attacker");
				GameObject gameObjectParameter = E.GetGameObjectParameter("Defender");
				if (gameObjectParameter != null && (!this.Max || ((this.Amount.RollMaxCached() >= 0) ? (gameObjectParameter.Physics.Temperature < this.MaxTemp) : (gameObjectParameter.Physics.Temperature > this.MaxTemp))))
				{
					int num = this.Amount.RollCached();
					int num2 = this.MyPowerLoadBonus(int.MinValue, 100, 10);
					if (num2 != 0)
					{
						num = num * (100 + num2) / 100;
					}
					gameObjectParameter.TemperatureChange(num, E.GetGameObjectParameter("Attacker"), false, false, false, false, this.ParentObject.GetPhase(), null, null);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004729 RID: 18217
		public string Amount = "0";

		// Token: 0x0400472A RID: 18218
		public bool Max;

		// Token: 0x0400472B RID: 18219
		public int MaxTemp = 400;

		// Token: 0x0400472C RID: 18220
		public bool OnWielderHit;

		// Token: 0x0400472D RID: 18221
		public bool RequiresLit;
	}
}
