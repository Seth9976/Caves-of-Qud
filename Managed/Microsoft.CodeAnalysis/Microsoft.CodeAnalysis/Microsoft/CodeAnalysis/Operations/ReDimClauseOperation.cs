using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000448 RID: 1096
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class ReDimClauseOperation : Operation, IReDimClauseOperation, IOperation
	{
		// Token: 0x060029F6 RID: 10742 RVA: 0x0007911F File Offset: 0x0007731F
		internal ReDimClauseOperation(IOperation operand, [Nullable(new byte[] { 0, 1 })] ImmutableArray<IOperation> dimensionSizes, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.Operand = Operation.SetParentOperation<IOperation>(operand, this);
			this.DimensionSizes = Operation.SetParentOperation<IOperation>(dimensionSizes, this);
		}

		// Token: 0x17000C77 RID: 3191
		// (get) Token: 0x060029F7 RID: 10743 RVA: 0x00079146 File Offset: 0x00077346
		public IOperation Operand { get; }

		// Token: 0x17000C78 RID: 3192
		// (get) Token: 0x060029F8 RID: 10744 RVA: 0x0007914E File Offset: 0x0007734E
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<IOperation> DimensionSizes
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x060029F9 RID: 10745 RVA: 0x00079158 File Offset: 0x00077358
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot != 0)
			{
				if (slot == 1)
				{
					if (index < this.DimensionSizes.Length)
					{
						return this.DimensionSizes[index];
					}
				}
			}
			else if (this.Operand != null)
			{
				return this.Operand;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x060029FA RID: 10746 RVA: 0x000791B8 File Offset: 0x000773B8
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			switch (previousSlot)
			{
			case -1:
				if (this.Operand != null)
				{
					return new ValueTuple<bool, int, int>(true, 0, 0);
				}
				break;
			case 0:
				break;
			case 1:
				if (previousIndex + 1 < this.DimensionSizes.Length)
				{
					return new ValueTuple<bool, int, int>(true, 1, previousIndex + 1);
				}
				goto IL_0064;
			case 2:
				goto IL_0064;
			default:
				throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(previousSlot, previousIndex));
			}
			if (!this.DimensionSizes.IsEmpty)
			{
				return new ValueTuple<bool, int, int>(true, 1, 0);
			}
			IL_0064:
			return new ValueTuple<bool, int, int>(false, 2, 0);
		}

		// Token: 0x17000C79 RID: 3193
		// (get) Token: 0x060029FB RID: 10747 RVA: 0x00079243 File Offset: 0x00077443
		[Nullable(2)]
		public override ITypeSymbol Type
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000C7A RID: 3194
		// (get) Token: 0x060029FC RID: 10748 RVA: 0x00079246 File Offset: 0x00077446
		[Nullable(2)]
		internal override ConstantValue OperationConstantValue
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000C7B RID: 3195
		// (get) Token: 0x060029FD RID: 10749 RVA: 0x00079249 File Offset: 0x00077449
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.ReDimClause;
			}
		}

		// Token: 0x060029FE RID: 10750 RVA: 0x0007924D File Offset: 0x0007744D
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitReDimClause(this);
		}

		// Token: 0x060029FF RID: 10751 RVA: 0x00079256 File Offset: 0x00077456
		[NullableContext(2)]
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitReDimClause(this, argument);
		}
	}
}
