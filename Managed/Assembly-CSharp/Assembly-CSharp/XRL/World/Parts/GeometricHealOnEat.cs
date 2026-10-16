using System;
using XRL.Rules;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000F82 RID: 3970
	[Serializable]
	public class GeometricHealOnEat : IPart
	{
		// Token: 0x06009DD7 RID: 40407 RVA: 0x00392A9F File Offset: 0x00390C9F
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetDefensiveItemListEvent.ID;
		}

		// Token: 0x06009DD8 RID: 40408 RVA: 0x00392AB8 File Offset: 0x00390CB8
		public override bool HandleEvent(AIGetDefensiveItemListEvent E)
		{
			if (!this.ParentObject.IsImportant() && E.Actor.HasPart<Stomach>() && !E.Actor.HasEffect<GeometricHeal>())
			{
				int hitpoints = E.Actor.hitpoints;
				int baseHitpoints = E.Actor.baseHitpoints;
				if (hitpoints < baseHitpoints)
				{
					int num = 0;
					int num2 = baseHitpoints - hitpoints;
					if (num2 >= baseHitpoints * 7 / 10)
					{
						num++;
					}
					if (num2 >= baseHitpoints * 8 / 10)
					{
						num++;
					}
					if (num2 >= baseHitpoints * 9 / 10)
					{
						num++;
					}
					if (num > 0)
					{
						E.Add("Eat", num, this.ParentObject, true, true, null, null);
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009DD9 RID: 40409 RVA: 0x00392B55 File Offset: 0x00390D55
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009DDA RID: 40410 RVA: 0x00392B58 File Offset: 0x00390D58
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("OnEat");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009DDB RID: 40411 RVA: 0x00392B70 File Offset: 0x00390D70
		public override bool FireEvent(Event E)
		{
			if (E.ID == "OnEat")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Eater");
				if (gameObjectParameter != null && CanApplyEffectEvent.Check<GeometricHeal>(gameObjectParameter, 0))
				{
					gameObjectParameter.ApplyEffect(new GeometricHeal((double)Stat.Roll(this.Amount, null), Stat.Roll(this.Ratio, null), Stat.Roll(this.Duration, null)), null);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004147 RID: 16711
		public string Amount;

		// Token: 0x04004148 RID: 16712
		public string Ratio;

		// Token: 0x04004149 RID: 16713
		public string Duration;
	}
}
