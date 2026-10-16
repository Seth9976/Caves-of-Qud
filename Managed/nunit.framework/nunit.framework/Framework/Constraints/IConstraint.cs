using System;

namespace NUnit.Framework.Constraints
{
	// Token: 0x02000112 RID: 274
	public interface IConstraint : IResolveConstraint
	{
		// Token: 0x1700020D RID: 525
		// (get) Token: 0x060008B8 RID: 2232
		string DisplayName { get; }

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x060008B9 RID: 2233
		string Description { get; }

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060008BA RID: 2234
		object[] Arguments { get; }

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x060008BB RID: 2235
		// (set) Token: 0x060008BC RID: 2236
		ConstraintBuilder Builder { get; set; }

		// Token: 0x060008BD RID: 2237
		ConstraintResult ApplyTo(object actual);

		// Token: 0x060008BE RID: 2238
		ConstraintResult ApplyTo<TActual>(ActualValueDelegate<TActual> del);

		// Token: 0x060008BF RID: 2239
		ConstraintResult ApplyTo<TActual>(ref TActual actual);
	}
}
