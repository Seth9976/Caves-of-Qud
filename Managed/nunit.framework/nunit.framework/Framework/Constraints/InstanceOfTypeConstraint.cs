using System;

namespace NUnit.Framework.Constraints
{
	// Token: 0x02000113 RID: 275
	public class InstanceOfTypeConstraint : TypeConstraint
	{
		// Token: 0x060008C0 RID: 2240 RVA: 0x0001436D File Offset: 0x0001256D
		public InstanceOfTypeConstraint(Type type)
			: base(type, "instance of ")
		{
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x060008C1 RID: 2241 RVA: 0x0001437B File Offset: 0x0001257B
		public override string DisplayName
		{
			get
			{
				return "InstanceOf";
			}
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x00014382 File Offset: 0x00012582
		protected override bool Matches(object actual)
		{
			return actual != null && this.expectedType.IsInstanceOfType(actual);
		}
	}
}
