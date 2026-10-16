using System;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000DE6 RID: 3558
	[Serializable]
	public class Salve_Tonic_Applicator : IPart
	{
		// Token: 0x06009070 RID: 36976 RVA: 0x00341CE3 File Offset: 0x0033FEE3
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x06009071 RID: 36977 RVA: 0x00341CE6 File Offset: 0x0033FEE6
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetUtilityScoreEvent.ID;
		}

		// Token: 0x06009072 RID: 36978 RVA: 0x00341CFC File Offset: 0x0033FEFC
		public override bool HandleEvent(GetUtilityScoreEvent E)
		{
			if (E.Damage == null)
			{
				double num = E.Actor.Health();
				if (E.ForPermission)
				{
					if (num < 1.0)
					{
						E.ApplyScore(1);
					}
				}
				else if (num < 0.5)
				{
					E.ApplyScore((int)(5.0 / num));
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009073 RID: 36979 RVA: 0x00341D5F File Offset: 0x0033FF5F
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ApplyTonic");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009074 RID: 36980 RVA: 0x00341D74 File Offset: 0x0033FF74
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
				int @for = GetTonicDurationEvent.GetFor(this.ParentObject, gameObjectParameter, gameObjectParameter2, "Salve", 5 * intParameter, intParameter, true);
				gameObjectParameter2.FireEvent("ApplyingSalve");
				Salve_Tonic salve_Tonic = new Salve_Tonic(@for);
				if (!gameObjectParameter2.ApplyEffect(salve_Tonic, null))
				{
					return false;
				}
				gameObjectParameter2.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_positiveVitality", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			return base.FireEvent(E);
		}
	}
}
