using System;
using XRL.Rules;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000DE2 RID: 3554
	[Serializable]
	public class HulkHoney_Tonic_Applicator : IPart
	{
		// Token: 0x0600905C RID: 36956 RVA: 0x0034149D File Offset: 0x0033F69D
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetUtilityScoreEvent.ID;
		}

		// Token: 0x0600905D RID: 36957 RVA: 0x003414B4 File Offset: 0x0033F6B4
		public override bool HandleEvent(GetUtilityScoreEvent E)
		{
			if (E.Damage == null)
			{
				if (E.Actor.HasEffect<Terrified>())
				{
					E.ApplyScore(100);
				}
				else
				{
					double num = E.Actor.Health();
					if (num < 0.2)
					{
						E.ApplyScore((int)(1.0 / num));
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600905E RID: 36958 RVA: 0x00341511 File Offset: 0x0033F711
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ApplyTonic");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600905F RID: 36959 RVA: 0x00341528 File Offset: 0x0033F728
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
				HulkHoney_Tonic hulkHoney_Tonic = new HulkHoney_Tonic(GetTonicDurationEvent.GetFor(this.ParentObject, gameObjectParameter, gameObjectParameter2, "HulkHoney", Stat.Random(intParameter, 10 * intParameter) + 40 * intParameter, intParameter, false));
				if (!gameObjectParameter2.ApplyEffect(hulkHoney_Tonic, null))
				{
					return false;
				}
			}
			return base.FireEvent(E);
		}
	}
}
