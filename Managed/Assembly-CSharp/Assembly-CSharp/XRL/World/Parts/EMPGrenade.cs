using System;
using XRL.World.Parts.Mutation;

namespace XRL.World.Parts
{
	// Token: 0x02000FD0 RID: 4048
	[Serializable]
	public class EMPGrenade : IGrenade
	{
		// Token: 0x0600A0ED RID: 41197 RVA: 0x003A8F1C File Offset: 0x003A711C
		public override bool SameAs(IPart p)
		{
			EMPGrenade empgrenade = p as EMPGrenade;
			return empgrenade.Radius == this.Radius && !(empgrenade.Duration != this.Duration) && base.SameAs(p);
		}

		// Token: 0x0600A0EE RID: 41198 RVA: 0x003A8F5C File Offset: 0x003A715C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetComponentNavigationWeightEvent.ID || ID == GetComponentAdjacentNavigationWeightEvent.ID;
		}

		// Token: 0x0600A0EF RID: 41199 RVA: 0x003A8F7A File Offset: 0x003A717A
		public override bool HandleEvent(GetComponentNavigationWeightEvent E)
		{
			E.MinWeight(3);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A0F0 RID: 41200 RVA: 0x003A8F8A File Offset: 0x003A718A
		public override bool HandleEvent(GetComponentAdjacentNavigationWeightEvent E)
		{
			E.MinWeight(2);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A0F1 RID: 41201 RVA: 0x003A8F9C File Offset: 0x003A719C
		protected override bool DoDetonate(Cell C, GameObject Actor = null, GameObject ApparentTarget = null, bool Indirect = false)
		{
			base.DidX("detonate", null, "!", null, null, null, null, false, false, null, null, false, false, false, false, false, null);
			base.PlayWorldSound(base.GetPropertyOrTag("DetonatedSound", null), 1f, 0f, true, null, 0f, 1f, 1f, int.MaxValue);
			ElectromagneticPulse.EMP(C, this.Radius, this.Duration.RollCached(), true, this.ParentObject.GetPhase());
			this.ParentObject.Destroy(null, true, false, null);
			return true;
		}

		// Token: 0x040042DA RID: 17114
		public int Radius = 4;

		// Token: 0x040042DB RID: 17115
		public string Duration = "1d2+4";
	}
}
