using System;
using XRL.Rules;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000DE9 RID: 3561
	[Serializable]
	public class SphynxSalt_Tonic_Applicator : IPart
	{
		// Token: 0x06009080 RID: 36992 RVA: 0x0034207D File Offset: 0x0034027D
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009081 RID: 36993 RVA: 0x00342080 File Offset: 0x00340280
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ApplyTonic");
			Registrar.Register("GameRestored");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009082 RID: 36994 RVA: 0x003420A0 File Offset: 0x003402A0
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetUtilityScoreEvent.ID;
		}

		// Token: 0x06009083 RID: 36995 RVA: 0x003420B8 File Offset: 0x003402B8
		public override bool HandleEvent(GetUtilityScoreEvent E)
		{
			if (E.ForPermission)
			{
				E.ApplyScore(1);
			}
			else if (E.Damage != null)
			{
				if (E.Damage.Amount >= E.Actor.baseHitpoints * 2 / 3)
				{
					E.ApplyScore(8 * E.Damage.Amount / E.Actor.baseHitpoints);
				}
				else if (E.Damage.Amount >= E.Actor.hitpoints * 3 / 4)
				{
					E.ApplyScore(8 * E.Damage.Amount / E.Actor.hitpoints);
				}
			}
			else if (E.Actor.HasEffect<Confused>())
			{
				E.ApplyScore(100);
			}
			else
			{
				double num = E.Actor.Health();
				if (num < 0.1)
				{
					E.ApplyScore((int)(0.7 / num));
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009084 RID: 36996 RVA: 0x003421A4 File Offset: 0x003403A4
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
			int @for = GetTonicDurationEvent.GetFor(this.ParentObject, gameObjectParameter, gameObjectParameter2, "SphynxSalt", Stat.Random(18 * intParameter, 22 * intParameter), intParameter, false);
			GameObject gameObjectParameter3 = E.GetGameObjectParameter("Owner");
			if (gameObjectParameter3 != null && !gameObjectParameter3.IsPlayer() && IComponent<GameObject>.Visible(gameObjectParameter3) && !E.HasFlag("External") && !E.HasFlag("Involuntary"))
			{
				IComponent<GameObject>.AddPlayerMessage(gameObjectParameter3.Does("apply", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " " + this.ParentObject.an(int.MaxValue, null, null, false, true, false, false, false, false, true, false, false, true, false, null, false, null, false) + ".", null, true);
			}
			this.ParentObject.Destroy(null, false, false, null);
			gameObjectParameter2.ApplyEffect(new SphynxSalt_Tonic(@for), null);
			return false;
		}
	}
}
