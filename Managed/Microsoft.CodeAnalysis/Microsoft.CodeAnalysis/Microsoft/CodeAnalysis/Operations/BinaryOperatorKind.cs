using System;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x0200045D RID: 1117
	public enum BinaryOperatorKind
	{
		// Token: 0x04001148 RID: 4424
		None,
		// Token: 0x04001149 RID: 4425
		Add,
		// Token: 0x0400114A RID: 4426
		Subtract,
		// Token: 0x0400114B RID: 4427
		Multiply,
		// Token: 0x0400114C RID: 4428
		Divide,
		// Token: 0x0400114D RID: 4429
		IntegerDivide,
		// Token: 0x0400114E RID: 4430
		Remainder,
		// Token: 0x0400114F RID: 4431
		Power,
		// Token: 0x04001150 RID: 4432
		LeftShift,
		// Token: 0x04001151 RID: 4433
		RightShift,
		// Token: 0x04001152 RID: 4434
		And,
		// Token: 0x04001153 RID: 4435
		Or,
		// Token: 0x04001154 RID: 4436
		ExclusiveOr,
		// Token: 0x04001155 RID: 4437
		ConditionalAnd,
		// Token: 0x04001156 RID: 4438
		ConditionalOr,
		// Token: 0x04001157 RID: 4439
		Concatenate,
		// Token: 0x04001158 RID: 4440
		Equals,
		// Token: 0x04001159 RID: 4441
		ObjectValueEquals,
		// Token: 0x0400115A RID: 4442
		NotEquals,
		// Token: 0x0400115B RID: 4443
		ObjectValueNotEquals,
		// Token: 0x0400115C RID: 4444
		LessThan,
		// Token: 0x0400115D RID: 4445
		LessThanOrEqual,
		// Token: 0x0400115E RID: 4446
		GreaterThanOrEqual,
		// Token: 0x0400115F RID: 4447
		GreaterThan,
		// Token: 0x04001160 RID: 4448
		Like
	}
}
