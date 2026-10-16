using System;

namespace NUnit.Framework.Constraints
{
	// Token: 0x020000EC RID: 236
	public abstract class BinaryConstraint : Constraint
	{
		// Token: 0x0600074F RID: 1871 RVA: 0x00011CC3 File Offset: 0x0000FEC3
		protected BinaryConstraint(IConstraint left, IConstraint right)
			: base(new object[] { left, right })
		{
			Guard.ArgumentNotNull(left, "left");
			this.Left = left;
			Guard.ArgumentNotNull(right, "right");
			this.Right = right;
		}

		// Token: 0x04000198 RID: 408
		protected IConstraint Left;

		// Token: 0x04000199 RID: 409
		protected IConstraint Right;
	}
}
