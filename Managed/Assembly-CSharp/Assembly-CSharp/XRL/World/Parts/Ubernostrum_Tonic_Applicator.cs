using System;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000DEA RID: 3562
	[Serializable]
	public class Ubernostrum_Tonic_Applicator : IPart
	{
		// Token: 0x06009086 RID: 36998 RVA: 0x003422F6 File Offset: 0x003404F6
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x06009087 RID: 36999 RVA: 0x003422F9 File Offset: 0x003404F9
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetUtilityScoreEvent.ID;
		}

		// Token: 0x06009088 RID: 37000 RVA: 0x00342310 File Offset: 0x00340510
		public override bool HandleEvent(GetUtilityScoreEvent E)
		{
			if (E.Damage == null)
			{
				Body body = E.Actor.Body;
				if (body != null && body.FindRegenerablePart(null, null, null, null, null) != null)
				{
					E.ApplyScore(80);
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
					else if (num < 0.1)
					{
						E.ApplyScore((int)(0.6 / num));
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009089 RID: 37001 RVA: 0x003423B4 File Offset: 0x003405B4
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ApplyTonic");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600908A RID: 37002 RVA: 0x003423CC File Offset: 0x003405CC
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
				int @for = GetTonicDurationEvent.GetFor(this.ParentObject, gameObjectParameter, gameObjectParameter2, "Ubernostrum", 10 * intParameter, intParameter, true);
				gameObjectParameter2.FireEvent("ApplyingUbernostrum");
				Ubernostrum_Tonic ubernostrum_Tonic = new Ubernostrum_Tonic(@for);
				if (!gameObjectParameter2.ApplyEffect(ubernostrum_Tonic, null))
				{
					return false;
				}
				gameObjectParameter2.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_positiveVitality_big", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			return base.FireEvent(E);
		}
	}
}
