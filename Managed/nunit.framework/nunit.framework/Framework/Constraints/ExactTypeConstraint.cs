using System;

namespace NUnit.Framework.Constraints
{
	// Token: 0x0200010A RID: 266
	public class ExactTypeConstraint : TypeConstraint
	{
		// Token: 0x0600089D RID: 2205 RVA: 0x00013EE2 File Offset: 0x000120E2
		public ExactTypeConstraint(Type type)
			: base(type, string.Empty)
		{
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x0600089E RID: 2206 RVA: 0x00013EF0 File Offset: 0x000120F0
		public override string DisplayName
		{
			get
			{
				return "TypeOf";
			}
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x00013EF7 File Offset: 0x000120F7
		protected override bool Matches(object actual)
		{
			return actual != null && actual.GetType() == this.expectedType;
		}
	}
}
