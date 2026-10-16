using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x0200044E RID: 1102
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class AggregateQueryOperation : Operation, IAggregateQueryOperation, IOperation
	{
		// Token: 0x06002A35 RID: 10805 RVA: 0x00079820 File Offset: 0x00077A20
		internal AggregateQueryOperation(IOperation group, IOperation aggregation, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, [Nullable(2)] ITypeSymbol type, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.Group = Operation.SetParentOperation<IOperation>(group, this);
			this.Aggregation = Operation.SetParentOperation<IOperation>(aggregation, this);
			this.Type = type;
		}

		// Token: 0x17000C98 RID: 3224
		// (get) Token: 0x06002A36 RID: 10806 RVA: 0x0007984F File Offset: 0x00077A4F
		public IOperation Group { get; }

		// Token: 0x17000C99 RID: 3225
		// (get) Token: 0x06002A37 RID: 10807 RVA: 0x00079857 File Offset: 0x00077A57
		public IOperation Aggregation { get; }

		// Token: 0x06002A38 RID: 10808 RVA: 0x00079860 File Offset: 0x00077A60
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot != 0)
			{
				if (slot == 1)
				{
					if (this.Aggregation != null)
					{
						return this.Aggregation;
					}
				}
			}
			else if (this.Group != null)
			{
				return this.Group;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x06002A39 RID: 10809 RVA: 0x000798B0 File Offset: 0x00077AB0
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			switch (previousSlot)
			{
			case -1:
				if (this.Group != null)
				{
					return new ValueTuple<bool, int, int>(true, 0, 0);
				}
				break;
			case 0:
				break;
			case 1:
			case 2:
				goto IL_003C;
			default:
				throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(previousSlot, previousIndex));
			}
			if (this.Aggregation != null)
			{
				return new ValueTuple<bool, int, int>(true, 1, 0);
			}
			IL_003C:
			return new ValueTuple<bool, int, int>(false, 2, 0);
		}

		// Token: 0x17000C9A RID: 3226
		// (get) Token: 0x06002A3A RID: 10810 RVA: 0x00079913 File Offset: 0x00077B13
		[Nullable(2)]
		public override ITypeSymbol Type
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x17000C9B RID: 3227
		// (get) Token: 0x06002A3B RID: 10811 RVA: 0x0007991B File Offset: 0x00077B1B
		[Nullable(2)]
		internal override ConstantValue OperationConstantValue
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000C9C RID: 3228
		// (get) Token: 0x06002A3C RID: 10812 RVA: 0x0007991E File Offset: 0x00077B1E
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.None;
			}
		}

		// Token: 0x06002A3D RID: 10813 RVA: 0x00079921 File Offset: 0x00077B21
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitAggregateQuery(this);
		}

		// Token: 0x06002A3E RID: 10814 RVA: 0x0007992A File Offset: 0x00077B2A
		[NullableContext(2)]
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitAggregateQuery(this, argument);
		}
	}
}
