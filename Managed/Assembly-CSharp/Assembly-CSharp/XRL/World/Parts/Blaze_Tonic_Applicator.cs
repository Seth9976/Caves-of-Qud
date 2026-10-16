using System;
using XRL.Rules;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000DDF RID: 3551
	[Serializable]
	public class Blaze_Tonic_Applicator : IPart
	{
		// Token: 0x0600904C RID: 36940 RVA: 0x00340FE0 File Offset: 0x0033F1E0
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x0600904D RID: 36941 RVA: 0x00340FE3 File Offset: 0x0033F1E3
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetUtilityScoreEvent.ID;
		}

		// Token: 0x0600904E RID: 36942 RVA: 0x00340FFC File Offset: 0x0033F1FC
		public override bool HandleEvent(GetUtilityScoreEvent E)
		{
			if (E.Damage != null)
			{
				if (E.Damage.HasAttribute("Heat"))
				{
					if (E.Damage.Amount >= E.Actor.baseHitpoints / 2)
					{
						E.ApplyScore(30 * E.Damage.Amount / E.Actor.baseHitpoints);
					}
					else if (E.Damage.Amount >= E.Actor.hitpoints * 2 / 3)
					{
						E.ApplyScore(30 * E.Damage.Amount / E.Actor.hitpoints);
					}
				}
			}
			else if (E.Actor.IsFrozen())
			{
				E.ApplyScore(100);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600904F RID: 36943 RVA: 0x003410BF File Offset: 0x0033F2BF
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ApplyTonic");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009050 RID: 36944 RVA: 0x003410D4 File Offset: 0x0033F2D4
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
				Blaze_Tonic blaze_Tonic = new Blaze_Tonic(GetTonicDurationEvent.GetFor(this.ParentObject, gameObjectParameter, gameObjectParameter2, "Blaze", Stat.Random(41 * intParameter, 50 * intParameter), intParameter, false));
				if (!gameObjectParameter2.ApplyEffect(blaze_Tonic, null))
				{
					return false;
				}
				gameObjectParameter2.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_blazeTonic", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			return base.FireEvent(E);
		}
	}
}
