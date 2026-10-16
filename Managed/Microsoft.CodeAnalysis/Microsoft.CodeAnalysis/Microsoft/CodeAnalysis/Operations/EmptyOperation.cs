using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003EA RID: 1002
	[NullableContext(2)]
	[Nullable(0)]
	internal sealed class EmptyOperation : Operation, IEmptyOperation, IOperation
	{
		// Token: 0x0600266E RID: 9838 RVA: 0x00074237 File Offset: 0x00072437
		[NullableContext(1)]
		internal EmptyOperation([Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
		}

		// Token: 0x0600266F RID: 9839 RVA: 0x00074242 File Offset: 0x00072442
		[NullableContext(1)]
		protected override IOperation GetCurrent(int slot, int index)
		{
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x06002670 RID: 9840 RVA: 0x00074255 File Offset: 0x00072455
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			return new ValueTuple<bool, int, int>(false, int.MinValue, int.MinValue);
		}

		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x06002671 RID: 9841 RVA: 0x00074267 File Offset: 0x00072467
		public override ITypeSymbol Type
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x06002672 RID: 9842 RVA: 0x0007426A File Offset: 0x0007246A
		internal override ConstantValue OperationConstantValue
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x06002673 RID: 9843 RVA: 0x0007426D File Offset: 0x0007246D
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.Empty;
			}
		}

		// Token: 0x06002674 RID: 9844 RVA: 0x00074270 File Offset: 0x00072470
		[NullableContext(1)]
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitEmpty(this);
		}

		// Token: 0x06002675 RID: 9845 RVA: 0x00074279 File Offset: 0x00072479
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitEmpty(this, argument);
		}
	}
}
