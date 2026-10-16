using System;

namespace NUnit.Framework.Constraints
{
	// Token: 0x02000106 RID: 262
	public class EmptyStringConstraint : StringConstraint
	{
		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000874 RID: 2164 RVA: 0x000139B7 File Offset: 0x00011BB7
		public override string Description
		{
			get
			{
				return "<empty>";
			}
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x00013AB4 File Offset: 0x00011CB4
		protected override bool Matches(string actual)
		{
			return actual == string.Empty;
		}
	}
}
