using System;
using NUnit.Compatibility;

namespace NUnit.Framework.Constraints
{
	// Token: 0x020000E8 RID: 232
	public class AssignableFromConstraint : TypeConstraint
	{
		// Token: 0x06000745 RID: 1861 RVA: 0x00011AB6 File Offset: 0x0000FCB6
		public AssignableFromConstraint(Type type)
			: base(type, "assignable from ")
		{
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x00011AC4 File Offset: 0x0000FCC4
		protected override bool Matches(object actual)
		{
			return actual != null && actual.GetType().GetTypeInfo().IsAssignableFrom(this.expectedType.GetTypeInfo());
		}
	}
}
