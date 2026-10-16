using System;

namespace XRL.World.Parts
{
	// Token: 0x02000E27 RID: 3623
	[Serializable]
	public class AIReplica : AIBehaviorPart
	{
		// Token: 0x06009351 RID: 37713 RVA: 0x003511EC File Offset: 0x0034F3EC
		public AIReplica()
		{
		}

		// Token: 0x06009352 RID: 37714 RVA: 0x003511FB File Offset: 0x0034F3FB
		public AIReplica(GameObject Original)
		{
			this.OriginalID = Original.ID;
		}

		// Token: 0x06009353 RID: 37715 RVA: 0x00351216 File Offset: 0x0034F416
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			if (this.ForceAllied || Registrar.IsUnregister)
			{
				Registrar.Register(PooledEvent<GetFeelingEvent>.ID, 0, false);
				Registrar.Register(PooledEvent<BeforeSetFeelingEvent>.ID, 0, false);
			}
		}

		// Token: 0x06009354 RID: 37716 RVA: 0x00351242 File Offset: 0x0034F442
		public override bool HandleEvent(GetFeelingEvent E)
		{
			if (this.ForceAllied && E.Feeling < 100 && this.IsAlly(E.Target))
			{
				E.Feeling = 100;
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009355 RID: 37717 RVA: 0x00351278 File Offset: 0x0034F478
		public override bool HandleEvent(BeforeSetFeelingEvent E)
		{
			if (this.ForceAllied && E.Feeling < 0 && this.IsAlly(E.Target))
			{
				if (!E.Target.IsPlayer())
				{
					E.Target.StopFighting(this.ParentObject, false);
				}
				E.Feeling = 100;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009356 RID: 37718 RVA: 0x003512D4 File Offset: 0x0034F4D4
		public bool IsAlly(GameObject Object)
		{
			if (Object.IDMatch(this.OriginalID))
			{
				return true;
			}
			AIReplica part = Object.GetPart<AIReplica>();
			return part != null && part.OriginalID == this.OriginalID;
		}

		// Token: 0x04003CF3 RID: 15603
		public string OriginalID;

		// Token: 0x04003CF4 RID: 15604
		public bool ForceAllied = true;
	}
}
