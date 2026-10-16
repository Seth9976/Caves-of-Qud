using System;

namespace NUnit.Framework.Constraints
{
	// Token: 0x02000139 RID: 313
	public class ResolvableConstraintExpression : ConstraintExpression, IResolveConstraint
	{
		// Token: 0x0600096B RID: 2411 RVA: 0x00016C9D File Offset: 0x00014E9D
		public ResolvableConstraintExpression()
		{
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x00016CA5 File Offset: 0x00014EA5
		public ResolvableConstraintExpression(ConstraintBuilder builder)
			: base(builder)
		{
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x0600096D RID: 2413 RVA: 0x00016CAE File Offset: 0x00014EAE
		public ConstraintExpression And
		{
			get
			{
				return base.Append(new AndOperator());
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x0600096E RID: 2414 RVA: 0x00016CBB File Offset: 0x00014EBB
		public ConstraintExpression Or
		{
			get
			{
				return base.Append(new OrOperator());
			}
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x00016CC8 File Offset: 0x00014EC8
		IConstraint IResolveConstraint.Resolve()
		{
			return this.builder.Resolve();
		}
	}
}
