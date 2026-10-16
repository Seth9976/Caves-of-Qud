using System;

namespace XRL.World.Parts
{
	// Token: 0x02000FE5 RID: 4069
	[Serializable]
	public abstract class IPlayerPart : IPart
	{
		// Token: 0x0600A1C2 RID: 41410 RVA: 0x003AFD71 File Offset: 0x003ADF71
		public override IPart DeepCopy(GameObject Parent)
		{
			return null;
		}

		// Token: 0x0600A1C3 RID: 41411 RVA: 0x003AFD74 File Offset: 0x003ADF74
		public override bool WantEvent(int ID, int Cascade)
		{
			return base.WantEvent(ID, Cascade) || ID == PooledEvent<AfterPlayerBodyChangeEvent>.ID;
		}

		// Token: 0x0600A1C4 RID: 41412 RVA: 0x003AFD8C File Offset: 0x003ADF8C
		public override bool HandleEvent(AfterPlayerBodyChangeEvent E)
		{
			if (E.OldBody == this.ParentObject)
			{
				E.OldBody.RemovePart(this);
			}
			if (E.NewBody != null && !E.NewBody.PartsList.Contains(this))
			{
				E.NewBody.AddPart<IPlayerPart>(this, true, false);
			}
			return base.HandleEvent(E);
		}
	}
}
