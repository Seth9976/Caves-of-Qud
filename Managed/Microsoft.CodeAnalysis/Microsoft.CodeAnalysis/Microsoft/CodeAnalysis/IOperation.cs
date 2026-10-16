using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Operations;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001C2 RID: 450
	[NullableContext(1)]
	[InternalImplementationOnly]
	public interface IOperation
	{
		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x060012DE RID: 4830
		[Nullable(2)]
		IOperation Parent
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x060012DF RID: 4831
		OperationKind Kind { get; }

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x060012E0 RID: 4832
		SyntaxNode Syntax { get; }

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x060012E1 RID: 4833
		[Nullable(2)]
		ITypeSymbol Type
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x060012E2 RID: 4834
		[Nullable(new byte[] { 0, 2 })]
		Optional<object> ConstantValue
		{
			[return: Nullable(new byte[] { 0, 2 })]
			get;
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x060012E3 RID: 4835
		IEnumerable<IOperation> Children { get; }

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x060012E4 RID: 4836
		string Language { get; }

		// Token: 0x060012E5 RID: 4837
		void Accept(OperationVisitor visitor);

		// Token: 0x060012E6 RID: 4838
		[NullableContext(2)]
		TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument);

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x060012E7 RID: 4839
		bool IsImplicit { get; }

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x060012E8 RID: 4840
		[Nullable(2)]
		SemanticModel SemanticModel
		{
			[NullableContext(2)]
			get;
		}
	}
}
