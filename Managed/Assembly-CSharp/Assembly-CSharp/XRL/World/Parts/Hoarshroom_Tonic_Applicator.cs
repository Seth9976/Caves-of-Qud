using System;
using XRL.Rules;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000DE1 RID: 3553
	[Serializable]
	public class Hoarshroom_Tonic_Applicator : IPart
	{
		// Token: 0x06009057 RID: 36951 RVA: 0x003412C0 File Offset: 0x0033F4C0
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetUtilityScoreEvent.ID;
		}

		// Token: 0x06009058 RID: 36952 RVA: 0x003412D8 File Offset: 0x0033F4D8
		public override bool HandleEvent(GetUtilityScoreEvent E)
		{
			if (E.Damage != null)
			{
				if (E.Damage.HasAttribute("Cold"))
				{
					if (E.Damage.Amount >= E.Actor.baseHitpoints / 2)
					{
						E.ApplyScore(3 * E.Damage.Amount / E.Actor.baseHitpoints - E.Actor.Physics.Temperature / 50);
					}
					else if (E.Damage.Amount >= E.Actor.hitpoints * 2 / 3)
					{
						E.ApplyScore(3 * E.Damage.Amount / E.Actor.hitpoints - E.Actor.Physics.Temperature / 50);
					}
				}
			}
			else
			{
				double num = E.Actor.Health();
				if (E.ForPermission)
				{
					if (num < 1.0)
					{
						E.ApplyScore(1);
					}
				}
				else if (num < 0.2)
				{
					E.ApplyScore((int)(1.0 / num));
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009059 RID: 36953 RVA: 0x003413F6 File Offset: 0x0033F5F6
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ApplyTonic");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600905A RID: 36954 RVA: 0x0034140C File Offset: 0x0033F60C
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
				Hoarshroom_Tonic hoarshroom_Tonic = new Hoarshroom_Tonic(GetTonicDurationEvent.GetFor(this.ParentObject, gameObjectParameter, gameObjectParameter2, "Hoarshroom", 180 * intParameter + Stat.Random(0, 40 * intParameter), intParameter, true));
				if (!gameObjectParameter2.ApplyEffect(hoarshroom_Tonic, null))
				{
					return false;
				}
			}
			return base.FireEvent(E);
		}
	}
}
