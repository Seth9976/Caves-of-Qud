using System;

namespace XRL.World.Parts
{
	// Token: 0x02000F14 RID: 3860
	[Serializable]
	public class CursedCybernetics : IPart
	{
		// Token: 0x060099D4 RID: 39380 RVA: 0x0037E074 File Offset: 0x0037C274
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x060099D5 RID: 39381 RVA: 0x0037E077 File Offset: 0x0037C277
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<BeginBeingUnequippedEvent>.ID || ID == PooledEvent<CanBeUnequippedEvent>.ID || ID == PooledEvent<IsAfflictionEvent>.ID;
		}

		// Token: 0x060099D6 RID: 39382 RVA: 0x0037E09D File Offset: 0x0037C29D
		public override bool HandleEvent(CanBeUnequippedEvent E)
		{
			return (E.Forced || (this.ParentObject.IsImplant && this.ParentObject.Implantee == null)) && base.HandleEvent(E);
		}

		// Token: 0x060099D7 RID: 39383 RVA: 0x0037E0CC File Offset: 0x0037C2CC
		public override bool HandleEvent(BeginBeingUnequippedEvent E)
		{
			if (!E.Forced && (!this.ParentObject.IsImplant || this.ParentObject.Implantee != null))
			{
				E.AddFailureMessage("You can't remove " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".");
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060099D8 RID: 39384 RVA: 0x0037E143 File Offset: 0x0037C343
		public override bool HandleEvent(IsAfflictionEvent E)
		{
			return false;
		}
	}
}
