using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x0200044A RID: 1098
	internal sealed class DiscardPatternOperation : BasePatternOperation, IDiscardPatternOperation, IPatternOperation, IOperation
	{
		// Token: 0x06002A0D RID: 10765 RVA: 0x0007941A File Offset: 0x0007761A
		[NullableContext(1)]
		internal DiscardPatternOperation(ITypeSymbol inputType, ITypeSymbol narrowedType, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, bool isImplicit)
			: base(inputType, narrowedType, semanticModel, syntax, isImplicit)
		{
		}

		// Token: 0x06002A0E RID: 10766 RVA: 0x00079429 File Offset: 0x00077629
		[NullableContext(1)]
		protected override IOperation GetCurrent(int slot, int index)
		{
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x06002A0F RID: 10767 RVA: 0x0007943C File Offset: 0x0007763C
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			return new ValueTuple<bool, int, int>(false, int.MinValue, int.MinValue);
		}

		// Token: 0x17000C84 RID: 3204
		// (get) Token: 0x06002A10 RID: 10768 RVA: 0x0007944E File Offset: 0x0007764E
		[Nullable(2)]
		public override ITypeSymbol Type
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000C85 RID: 3205
		// (get) Token: 0x06002A11 RID: 10769 RVA: 0x00079451 File Offset: 0x00077651
		[Nullable(2)]
		internal override ConstantValue OperationConstantValue
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000C86 RID: 3206
		// (get) Token: 0x06002A12 RID: 10770 RVA: 0x00079454 File Offset: 0x00077654
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.DiscardPattern;
			}
		}

		// Token: 0x06002A13 RID: 10771 RVA: 0x00079458 File Offset: 0x00077658
		[NullableContext(1)]
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitDiscardPattern(this);
		}

		// Token: 0x06002A14 RID: 10772 RVA: 0x00079461 File Offset: 0x00077661
		[NullableContext(2)]
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitDiscardPattern(this, argument);
		}
	}
}
