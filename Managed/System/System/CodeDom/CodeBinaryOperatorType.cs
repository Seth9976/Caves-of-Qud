using System;

namespace System.CodeDom
{
	/// <summary>Defines identifiers for supported binary operators.</summary>
	// Token: 0x020002FD RID: 765
	public enum CodeBinaryOperatorType
	{
		/// <summary>Addition operator.</summary>
		// Token: 0x04000D7C RID: 3452
		Add,
		/// <summary>Subtraction operator.</summary>
		// Token: 0x04000D7D RID: 3453
		Subtract,
		/// <summary>Multiplication operator.</summary>
		// Token: 0x04000D7E RID: 3454
		Multiply,
		/// <summary>Division operator.</summary>
		// Token: 0x04000D7F RID: 3455
		Divide,
		/// <summary>Modulus operator.</summary>
		// Token: 0x04000D80 RID: 3456
		Modulus,
		/// <summary>Assignment operator.</summary>
		// Token: 0x04000D81 RID: 3457
		Assign,
		/// <summary>Identity not equal operator.</summary>
		// Token: 0x04000D82 RID: 3458
		IdentityInequality,
		/// <summary>Identity equal operator.</summary>
		// Token: 0x04000D83 RID: 3459
		IdentityEquality,
		/// <summary>Value equal operator.</summary>
		// Token: 0x04000D84 RID: 3460
		ValueEquality,
		/// <summary>Bitwise or operator.</summary>
		// Token: 0x04000D85 RID: 3461
		BitwiseOr,
		/// <summary>Bitwise and operator.</summary>
		// Token: 0x04000D86 RID: 3462
		BitwiseAnd,
		/// <summary>Boolean or operator. This represents a short circuiting operator. A short circuiting operator will evaluate only as many expressions as necessary before returning a correct value.</summary>
		// Token: 0x04000D87 RID: 3463
		BooleanOr,
		/// <summary>Boolean and operator. This represents a short circuiting operator. A short circuiting operator will evaluate only as many expressions as necessary before returning a correct value.</summary>
		// Token: 0x04000D88 RID: 3464
		BooleanAnd,
		/// <summary>Less than operator.</summary>
		// Token: 0x04000D89 RID: 3465
		LessThan,
		/// <summary>Less than or equal operator.</summary>
		// Token: 0x04000D8A RID: 3466
		LessThanOrEqual,
		/// <summary>Greater than operator.</summary>
		// Token: 0x04000D8B RID: 3467
		GreaterThan,
		/// <summary>Greater than or equal operator.</summary>
		// Token: 0x04000D8C RID: 3468
		GreaterThanOrEqual
	}
}
