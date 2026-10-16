using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003F4 RID: 1012
	[NullableContext(2)]
	[Nullable(0)]
	internal sealed class LiteralOperation : Operation, ILiteralOperation, IOperation
	{
		// Token: 0x060026D1 RID: 9937 RVA: 0x00074AC0 File Offset: 0x00072CC0
		internal LiteralOperation(SemanticModel semanticModel, [Nullable(1)] SyntaxNode syntax, ITypeSymbol type, ConstantValue constantValue, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.OperationConstantValue = constantValue;
			this.Type = type;
		}

		// Token: 0x060026D2 RID: 9938 RVA: 0x00074ADB File Offset: 0x00072CDB
		[NullableContext(1)]
		protected override IOperation GetCurrent(int slot, int index)
		{
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x060026D3 RID: 9939 RVA: 0x00074AEE File Offset: 0x00072CEE
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			return new ValueTuple<bool, int, int>(false, int.MinValue, int.MinValue);
		}

		// Token: 0x17000ADF RID: 2783
		// (get) Token: 0x060026D4 RID: 9940 RVA: 0x00074B00 File Offset: 0x00072D00
		public override ITypeSymbol Type { get; }

		// Token: 0x17000AE0 RID: 2784
		// (get) Token: 0x060026D5 RID: 9941 RVA: 0x00074B08 File Offset: 0x00072D08
		internal override ConstantValue OperationConstantValue { get; }

		// Token: 0x17000AE1 RID: 2785
		// (get) Token: 0x060026D6 RID: 9942 RVA: 0x00074B10 File Offset: 0x00072D10
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.Literal;
			}
		}

		// Token: 0x060026D7 RID: 9943 RVA: 0x00074B14 File Offset: 0x00072D14
		[NullableContext(1)]
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitLiteral(this);
		}

		// Token: 0x060026D8 RID: 9944 RVA: 0x00074B1D File Offset: 0x00072D1D
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitLiteral(this, argument);
		}
	}
}
