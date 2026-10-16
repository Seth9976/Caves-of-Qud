using System;

namespace NUnit.Framework.Constraints
{
	// Token: 0x0200014A RID: 330
	public abstract class TypeConstraint : Constraint
	{
		// Token: 0x060009B7 RID: 2487 RVA: 0x0001792E File Offset: 0x00015B2E
		protected TypeConstraint(Type type, string descriptionPrefix)
			: base(new object[] { type })
		{
			this.expectedType = type;
			this.Description = descriptionPrefix + MsgUtils.FormatValue(this.expectedType);
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x0001795E File Offset: 0x00015B5E
		public override ConstraintResult ApplyTo(object actual)
		{
			this.actualType = ((actual == null) ? null : actual.GetType());
			return new ConstraintResult(this, this.actualType, this.Matches(actual));
		}

		// Token: 0x060009B9 RID: 2489
		protected abstract bool Matches(object actual);

		// Token: 0x0400020B RID: 523
		protected Type expectedType;

		// Token: 0x0400020C RID: 524
		protected Type actualType;
	}
}
