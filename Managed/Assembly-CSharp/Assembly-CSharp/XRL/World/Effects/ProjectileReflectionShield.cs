using System;
using XRL.Core;
using XRL.Rules;

namespace XRL.World.Effects
{
	// Token: 0x02000DB0 RID: 3504
	[Serializable]
	public class ProjectileReflectionShield : Effect, ITierInitialized
	{
		// Token: 0x06008BEE RID: 35822 RVA: 0x0031AF50 File Offset: 0x00319150
		public ProjectileReflectionShield()
		{
			this.DisplayName = "{{Y|reflectively shielded}}";
			this.Duration = 9999;
			this.Chance = 100;
			this.Details = "Reflects incoming projectiles and thrown objects at a " + this.Chance.ToString() + "% chance.";
		}

		// Token: 0x06008BEF RID: 35823 RVA: 0x0031AFCA File Offset: 0x003191CA
		public ProjectileReflectionShield(int Chance, string RetroVariance = "0", string Verb = "reflect")
			: this()
		{
			this.Chance = Chance;
			this.RetroVariance = RetroVariance;
			this.Details = "Reflects incoming projectiles and thrown objects at a " + Chance.ToString() + "% chance.";
			this.Duration = 9999;
		}

		// Token: 0x06008BF0 RID: 35824 RVA: 0x0031B007 File Offset: 0x00319207
		public void Initialize(int Tier)
		{
			this.Duration = Stat.Random(50, 300);
		}

		// Token: 0x06008BF1 RID: 35825 RVA: 0x0031B01B File Offset: 0x0031921B
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x06008BF2 RID: 35826 RVA: 0x0031B01E File Offset: 0x0031921E
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x06008BF3 RID: 35827 RVA: 0x0031B021 File Offset: 0x00319221
		public override string GetDetails()
		{
			return this.Details;
		}

		// Token: 0x06008BF4 RID: 35828 RVA: 0x0031B029 File Offset: 0x00319229
		public override int GetEffectType()
		{
			return 64;
		}

		// Token: 0x06008BF5 RID: 35829 RVA: 0x0031B030 File Offset: 0x00319230
		public override bool Apply(GameObject Object)
		{
			if (Object != null)
			{
				Object.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_defensiveBuff", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			return true;
		}

		// Token: 0x06008BF6 RID: 35830 RVA: 0x0031B06B File Offset: 0x0031926B
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ReflectProjectile");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008BF7 RID: 35831 RVA: 0x0031B080 File Offset: 0x00319280
		public override bool FireEvent(Event E)
		{
			if (E.ID == "ReflectProjectile" && this.Chance.in100())
			{
				E.SetParameter("By", base.Object);
				if (!string.IsNullOrEmpty(this.RetroVariance))
				{
					float num = (float)E.GetParameter("Angle");
					E.SetParameter("Direction", (int)num + 180 + this.RetroVariance.RollCached());
				}
				if (!string.IsNullOrEmpty(this.Verb))
				{
					E.SetParameter("Verb", this.Verb);
				}
				return false;
			}
			return true;
		}

		// Token: 0x06008BF8 RID: 35832 RVA: 0x0031B120 File Offset: 0x00319320
		public override bool Render(RenderEvent E)
		{
			if (this.Duration > 0)
			{
				int num = XRLCore.CurrentFrame % 60;
				if (num > 40 && num < 60)
				{
					E.RenderString = "\t";
					E.ColorString = "&Y";
				}
			}
			return true;
		}

		// Token: 0x04003AE6 RID: 15078
		public int Chance = 100;

		// Token: 0x04003AE7 RID: 15079
		public string RetroVariance = "0";

		// Token: 0x04003AE8 RID: 15080
		public string Details = "";

		// Token: 0x04003AE9 RID: 15081
		public string Verb = "reflect";
	}
}
