using System;

namespace XRL.World.Parts
{
	// Token: 0x02001201 RID: 4609
	[Serializable]
	public class ShouldAttackToReachTarget : IPart
	{
		// Token: 0x0600B757 RID: 46935 RVA: 0x0042BE35 File Offset: 0x0042A035
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<ShouldAttackToReachTargetEvent>.ID;
		}

		// Token: 0x0600B758 RID: 46936 RVA: 0x0042BE4B File Offset: 0x0042A04B
		public override bool HandleEvent(ShouldAttackToReachTargetEvent E)
		{
			if (E.Object == this.ParentObject)
			{
				E.ShouldAttack = true;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B759 RID: 46937 RVA: 0x0042BE69 File Offset: 0x0042A069
		public override bool AllowStaticRegistration()
		{
			return true;
		}
	}
}
