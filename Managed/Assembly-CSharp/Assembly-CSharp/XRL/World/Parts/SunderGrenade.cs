using System;
using XRL.World.Parts.Mutation;

namespace XRL.World.Parts
{
	// Token: 0x02000FD7 RID: 4055
	[Serializable]
	public class SunderGrenade : IGrenade
	{
		// Token: 0x0600A120 RID: 41248 RVA: 0x003AA458 File Offset: 0x003A8658
		public override bool SameAs(IPart p)
		{
			SunderGrenade sunderGrenade = p as SunderGrenade;
			return sunderGrenade.Radius == this.Radius && sunderGrenade.Level == this.Level && base.SameAs(p);
		}

		// Token: 0x0600A121 RID: 41249 RVA: 0x003AA493 File Offset: 0x003A8693
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetComponentNavigationWeightEvent.ID || ID == GetComponentAdjacentNavigationWeightEvent.ID;
		}

		// Token: 0x0600A122 RID: 41250 RVA: 0x003AA4B1 File Offset: 0x003A86B1
		public override bool HandleEvent(GetComponentNavigationWeightEvent E)
		{
			E.MinWeight(8);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A123 RID: 41251 RVA: 0x003AA4C1 File Offset: 0x003A86C1
		public override bool HandleEvent(GetComponentAdjacentNavigationWeightEvent E)
		{
			E.MinWeight(8);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A124 RID: 41252 RVA: 0x003AA4D4 File Offset: 0x003A86D4
		protected override bool DoDetonate(Cell C, GameObject Actor = null, GameObject ApparentTarget = null, bool Indirect = false)
		{
			base.DidX("detonate", null, "!", null, null, null, null, false, false, null, null, false, false, false, false, false, null);
			Disintegration.Disintegrate(C, this.Radius, this.Level, this.ParentObject, Actor, this.ParentObject, true, Indirect);
			this.ParentObject.Destroy(null, true, false, null);
			return true;
		}

		// Token: 0x040042EB RID: 17131
		public int Radius = 1;

		// Token: 0x040042EC RID: 17132
		public int Level = 1;
	}
}
