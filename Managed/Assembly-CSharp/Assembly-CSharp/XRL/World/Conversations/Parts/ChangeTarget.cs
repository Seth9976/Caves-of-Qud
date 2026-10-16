using System;

namespace XRL.World.Conversations.Parts
{
	/// <summary>Change target element if all/any predicates are satisfied.</summary>
	// Token: 0x02000BF6 RID: 3062
	public class ChangeTarget : IPredicatePart
	{
		// Token: 0x06007B09 RID: 31497 RVA: 0x002E3290 File Offset: 0x002E1490
		public override bool WantEvent(int ID, int Propagation)
		{
			return base.WantEvent(ID, Propagation) || ID == GetTargetElementEvent.ID;
		}

		// Token: 0x06007B0A RID: 31498 RVA: 0x002E32A8 File Offset: 0x002E14A8
		public override bool HandleEvent(GetTargetElementEvent E)
		{
			if (this.Check(this.Any, null) != this.Not)
			{
				E.Target = this.Target;
			}
			return base.HandleEvent(E);
		}

		/// <summary>The alternate navigation target if predicates match.</summary>
		// Token: 0x04003814 RID: 14356
		public string Target;

		/// <summary>Require one predicate to match rather than all.</summary>
		// Token: 0x04003815 RID: 14357
		public new bool Any;

		// Token: 0x04003816 RID: 14358
		public bool Not;
	}
}
