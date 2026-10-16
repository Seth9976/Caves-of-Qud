using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003F2 RID: 1010
	[NullableContext(2)]
	[Nullable(0)]
	internal sealed class EndOperation : Operation, IEndOperation, IOperation
	{
		// Token: 0x060026BF RID: 9919 RVA: 0x00074931 File Offset: 0x00072B31
		[NullableContext(1)]
		internal EndOperation([Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
		}

		// Token: 0x060026C0 RID: 9920 RVA: 0x0007493C File Offset: 0x00072B3C
		[NullableContext(1)]
		protected override IOperation GetCurrent(int slot, int index)
		{
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x060026C1 RID: 9921 RVA: 0x0007494F File Offset: 0x00072B4F
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			return new ValueTuple<bool, int, int>(false, int.MinValue, int.MinValue);
		}

		// Token: 0x17000AD7 RID: 2775
		// (get) Token: 0x060026C2 RID: 9922 RVA: 0x00074961 File Offset: 0x00072B61
		public override ITypeSymbol Type
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000AD8 RID: 2776
		// (get) Token: 0x060026C3 RID: 9923 RVA: 0x00074964 File Offset: 0x00072B64
		internal override ConstantValue OperationConstantValue
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000AD9 RID: 2777
		// (get) Token: 0x060026C4 RID: 9924 RVA: 0x00074967 File Offset: 0x00072B67
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.End;
			}
		}

		// Token: 0x060026C5 RID: 9925 RVA: 0x0007496B File Offset: 0x00072B6B
		[NullableContext(1)]
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitEnd(this);
		}

		// Token: 0x060026C6 RID: 9926 RVA: 0x00074974 File Offset: 0x00072B74
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitEnd(this, argument);
		}
	}
}
