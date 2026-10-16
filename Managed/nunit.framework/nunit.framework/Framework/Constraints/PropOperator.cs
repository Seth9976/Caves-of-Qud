using System;

namespace NUnit.Framework.Constraints
{
	// Token: 0x0200012C RID: 300
	public class PropOperator : SelfResolvingOperator
	{
		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000940 RID: 2368 RVA: 0x000165C0 File Offset: 0x000147C0
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x000165C8 File Offset: 0x000147C8
		public PropOperator(string name)
		{
			this.name = name;
			this.left_precedence = (this.right_precedence = 1);
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x000165F2 File Offset: 0x000147F2
		public override void Reduce(ConstraintBuilder.ConstraintStack stack)
		{
			if (base.RightContext == null || base.RightContext is BinaryOperator)
			{
				stack.Push(new PropertyExistsConstraint(this.name));
				return;
			}
			stack.Push(new PropertyConstraint(this.name, stack.Pop()));
		}

		// Token: 0x040001DF RID: 479
		private readonly string name;
	}
}
