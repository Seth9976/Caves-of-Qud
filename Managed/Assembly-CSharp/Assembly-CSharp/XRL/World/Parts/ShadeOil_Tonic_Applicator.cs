using System;
using XRL.Rules;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000DE7 RID: 3559
	[Serializable]
	public class ShadeOil_Tonic_Applicator : IPart
	{
		// Token: 0x06009076 RID: 36982 RVA: 0x00341E2F File Offset: 0x0034002F
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x06009077 RID: 36983 RVA: 0x00341E32 File Offset: 0x00340032
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetUtilityScoreEvent.ID;
		}

		// Token: 0x06009078 RID: 36984 RVA: 0x00341E48 File Offset: 0x00340048
		public override bool HandleEvent(GetUtilityScoreEvent E)
		{
			if (E.Damage != null)
			{
				if (E.Damage.Amount >= E.Actor.baseHitpoints * 2 / 3)
				{
					E.ApplyScore((E.Actor.IsTrueKin() ? 10 : 4) * E.Damage.Amount / E.Actor.baseHitpoints);
				}
				else if (E.Damage.Amount >= E.Actor.hitpoints * 3 / 4)
				{
					E.ApplyScore((E.Actor.IsTrueKin() ? 10 : 4) * E.Damage.Amount / E.Actor.hitpoints);
				}
			}
			else
			{
				double num = E.Actor.Health();
				if (num < 0.1)
				{
					E.ApplyScore((int)(0.8 / num));
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009079 RID: 36985 RVA: 0x00341F2D File Offset: 0x0034012D
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ApplyTonic");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600907A RID: 36986 RVA: 0x00341F44 File Offset: 0x00340144
		public override bool FireEvent(Event E)
		{
			if (E.ID == "ApplyTonic")
			{
				int intParameter = E.GetIntParameter("Dosage", 0);
				if (intParameter <= 0)
				{
					return false;
				}
				GameObject gameObjectParameter = E.GetGameObjectParameter("Actor");
				GameObject gameObjectParameter2 = E.GetGameObjectParameter("Subject");
				ShadeOil_Tonic shadeOil_Tonic = new ShadeOil_Tonic(GetTonicDurationEvent.GetFor(this.ParentObject, gameObjectParameter, gameObjectParameter2, "ShadeOil", Stat.Random(intParameter, 10 * intParameter) + 40 * intParameter, intParameter, false));
				if (!gameObjectParameter2.ApplyEffect(shadeOil_Tonic, null))
				{
					return false;
				}
			}
			return base.FireEvent(E);
		}
	}
}
