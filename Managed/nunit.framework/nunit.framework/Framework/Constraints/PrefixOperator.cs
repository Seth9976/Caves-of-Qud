using System;

namespace NUnit.Framework.Constraints
{
	// Token: 0x0200012B RID: 299
	public abstract class PrefixOperator : ConstraintOperator
	{
		// Token: 0x0600093D RID: 2365 RVA: 0x000165AC File Offset: 0x000147AC
		public override void Reduce(ConstraintBuilder.ConstraintStack stack)
		{
			stack.Push(this.ApplyPrefix(stack.Pop()));
		}

		// Token: 0x0600093E RID: 2366
		public abstract IConstraint ApplyPrefix(IConstraint constraint);
	}
}
