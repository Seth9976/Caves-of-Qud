using System;
using XRL.World.AI;

namespace XRL.World.Parts
{
	// Token: 0x02000E53 RID: 3667
	[Serializable]
	public class Calming : IPart
	{
		// Token: 0x06009474 RID: 38004 RVA: 0x0035704D File Offset: 0x0035524D
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x06009475 RID: 38005 RVA: 0x00357050 File Offset: 0x00355250
		public override bool HandleEvent(GetFeelingEvent E)
		{
			if (E.Faction)
			{
				E.Feeling = this.Feeling;
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009476 RID: 38006 RVA: 0x0035706F File Offset: 0x0035526F
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009477 RID: 38007 RVA: 0x00357072 File Offset: 0x00355272
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AITargetCreateKill");
			Registrar.Register(PooledEvent<GetFeelingEvent>.ID, 0, false);
		}

		// Token: 0x06009478 RID: 38008 RVA: 0x0035708C File Offset: 0x0035528C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "AITargetCreateKill")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Actor");
				if (!gameObjectParameter.HasIntProperty("Calmed") && !gameObjectParameter.IsPlayerControlled())
				{
					gameObjectParameter.StopFighting(this.ParentObject, false);
					gameObjectParameter.AddOpinion<OpinionMollify>(this.ParentObject, (float)this.Feeling);
					gameObjectParameter.SetIntProperty("Calmed", 1, false);
					return false;
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003D64 RID: 15716
		public int Feeling = 50;
	}
}
