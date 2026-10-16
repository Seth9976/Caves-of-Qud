using System;

namespace NUnit.Framework.Constraints
{
	// Token: 0x0200013A RID: 314
	public class ReusableConstraint : IResolveConstraint
	{
		// Token: 0x06000970 RID: 2416 RVA: 0x00016CD5 File Offset: 0x00014ED5
		public ReusableConstraint(IResolveConstraint c)
		{
			this.constraint = c.Resolve();
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x00016CE9 File Offset: 0x00014EE9
		public static implicit operator ReusableConstraint(Constraint c)
		{
			return new ReusableConstraint(c);
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x00016CF1 File Offset: 0x00014EF1
		public override string ToString()
		{
			return this.constraint.ToString();
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x00016CFE File Offset: 0x00014EFE
		public IConstraint Resolve()
		{
			return this.constraint;
		}

		// Token: 0x040001ED RID: 493
		private readonly IConstraint constraint;
	}
}
