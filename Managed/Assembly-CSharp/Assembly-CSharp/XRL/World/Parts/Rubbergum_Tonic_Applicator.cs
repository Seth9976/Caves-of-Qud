using System;
using XRL.Rules;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000DE5 RID: 3557
	[Serializable]
	public class Rubbergum_Tonic_Applicator : IPart
	{
		// Token: 0x0600906A RID: 36970 RVA: 0x003419E4 File Offset: 0x0033FBE4
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetUtilityScoreEvent.ID;
		}

		// Token: 0x0600906B RID: 36971 RVA: 0x003419FC File Offset: 0x0033FBFC
		private void ApplyModifiedScore(GetUtilityScoreEvent E, int Score)
		{
			Bleeding effect = E.Actor.GetEffect<Bleeding>();
			if (effect != null)
			{
				Score -= (effect.Damage.RollMinCached() + effect.Damage.RollMaxCached()) / 2;
			}
			if (Score > 0)
			{
				E.ApplyScore(Score);
			}
		}

		// Token: 0x0600906C RID: 36972 RVA: 0x00341A40 File Offset: 0x0033FC40
		public override bool HandleEvent(GetUtilityScoreEvent E)
		{
			if (E.Damage != null)
			{
				if (E.Damage.IsElectricDamage())
				{
					if (E.Damage.Amount >= E.Actor.baseHitpoints / 2)
					{
						this.ApplyModifiedScore(E, 30 * E.Damage.Amount / E.Actor.baseHitpoints);
					}
					else if (E.Damage.Amount >= E.Actor.hitpoints * 2 / 3)
					{
						this.ApplyModifiedScore(E, 30 * E.Damage.Amount / E.Actor.hitpoints);
					}
				}
				else if (Rubbergum_Tonic.AffectsDamage(E.Damage))
				{
					if (E.Damage.Amount >= E.Actor.baseHitpoints / 2)
					{
						this.ApplyModifiedScore(E, 20 * E.Damage.Amount / E.Actor.baseHitpoints);
					}
					else if (E.Damage.Amount >= E.Actor.hitpoints * 2 / 3)
					{
						this.ApplyModifiedScore(E, 20 * E.Damage.Amount / E.Actor.hitpoints);
					}
				}
				else if (E.Damage.HasAttribute("Cold"))
				{
					if (E.Damage.Amount >= E.Actor.baseHitpoints / 2)
					{
						this.ApplyModifiedScore(E, 10 * E.Damage.Amount / E.Actor.baseHitpoints - E.Actor.Physics.Temperature / 50);
					}
					else if (E.Damage.Amount >= E.Actor.hitpoints * 2 / 3)
					{
						this.ApplyModifiedScore(E, 10 * E.Damage.Amount / E.Actor.hitpoints - E.Actor.Physics.Temperature / 50);
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600906D RID: 36973 RVA: 0x00341C40 File Offset: 0x0033FE40
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ApplyTonic");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600906E RID: 36974 RVA: 0x00341C58 File Offset: 0x0033FE58
		public override bool FireEvent(Event E)
		{
			if (!(E.ID == "ApplyTonic"))
			{
				return base.FireEvent(E);
			}
			int intParameter = E.GetIntParameter("Dosage", 0);
			if (intParameter <= 0)
			{
				return false;
			}
			GameObject gameObjectParameter = E.GetGameObjectParameter("Actor");
			GameObject gameObjectParameter2 = E.GetGameObjectParameter("Subject");
			Rubbergum_Tonic rubbergum_Tonic = new Rubbergum_Tonic(GetTonicDurationEvent.GetFor(this.ParentObject, gameObjectParameter, gameObjectParameter2, "Rubbergum", Stat.Random(intParameter, 10 * intParameter) + 40 * intParameter, intParameter, false));
			return gameObjectParameter2.ApplyEffect(rubbergum_Tonic, null);
		}
	}
}
