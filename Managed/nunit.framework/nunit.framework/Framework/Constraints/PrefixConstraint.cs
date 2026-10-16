using System;

namespace NUnit.Framework.Constraints
{
	// Token: 0x02000134 RID: 308
	public abstract class PrefixConstraint : Constraint
	{
		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000956 RID: 2390 RVA: 0x00016975 File Offset: 0x00014B75
		// (set) Token: 0x06000957 RID: 2391 RVA: 0x0001697D File Offset: 0x00014B7D
		protected IConstraint BaseConstraint { get; set; }

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000958 RID: 2392 RVA: 0x00016986 File Offset: 0x00014B86
		// (set) Token: 0x06000959 RID: 2393 RVA: 0x0001698E File Offset: 0x00014B8E
		protected string DescriptionPrefix { get; set; }

		// Token: 0x0600095A RID: 2394 RVA: 0x00016997 File Offset: 0x00014B97
		protected PrefixConstraint(IResolveConstraint baseConstraint)
			: base(new object[] { baseConstraint })
		{
			Guard.ArgumentNotNull(baseConstraint, "baseConstraint");
			this.BaseConstraint = baseConstraint.Resolve();
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x0600095B RID: 2395 RVA: 0x000169C0 File Offset: 0x00014BC0
		public override string Description
		{
			get
			{
				return string.Format((this.BaseConstraint is EqualConstraint) ? "{0} equal to {1}" : "{0} {1}", this.DescriptionPrefix, this.BaseConstraint.Description);
			}
		}
	}
}
