using System;
using NUnit.Compatibility;

namespace NUnit.Framework.Constraints
{
	// Token: 0x020000E9 RID: 233
	public class AssignableToConstraint : TypeConstraint
	{
		// Token: 0x06000747 RID: 1863 RVA: 0x00011AE6 File Offset: 0x0000FCE6
		public AssignableToConstraint(Type type)
			: base(type, "assignable to ")
		{
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x00011AF4 File Offset: 0x0000FCF4
		protected override bool Matches(object actual)
		{
			return this.expectedType != null && actual != null && this.expectedType.GetTypeInfo().IsAssignableFrom(actual.GetType().GetTypeInfo());
		}
	}
}
