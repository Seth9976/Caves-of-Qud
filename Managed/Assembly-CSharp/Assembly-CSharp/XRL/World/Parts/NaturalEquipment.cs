using System;

namespace XRL.World.Parts
{
	// Token: 0x0200106E RID: 4206
	[Serializable]
	public class NaturalEquipment : IPart
	{
		// Token: 0x0600A78E RID: 42894 RVA: 0x003D337C File Offset: 0x003D157C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<BeginBeingUnequippedEvent>.ID || ID == PooledEvent<CanBeUnequippedEvent>.ID || ID == PooledEvent<GetSlotsRequiredEvent>.ID;
		}

		// Token: 0x0600A78F RID: 42895 RVA: 0x003D33A2 File Offset: 0x003D15A2
		public override bool HandleEvent(CanBeUnequippedEvent E)
		{
			return E.Forced && base.HandleEvent(E);
		}

		// Token: 0x0600A790 RID: 42896 RVA: 0x003D33B8 File Offset: 0x003D15B8
		public override bool HandleEvent(BeginBeingUnequippedEvent E)
		{
			if (!E.Forced && this.ParentObject.Equipped != null)
			{
				E.AddFailureMessage("You can't remove " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".");
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A791 RID: 42897 RVA: 0x003D3422 File Offset: 0x003D1622
		public override bool HandleEvent(GetSlotsRequiredEvent E)
		{
			if (E.Object == this.ParentObject)
			{
				E.AllowReduction = false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A792 RID: 42898 RVA: 0x003D3440 File Offset: 0x003D1640
		public override bool AllowStaticRegistration()
		{
			return true;
		}
	}
}
