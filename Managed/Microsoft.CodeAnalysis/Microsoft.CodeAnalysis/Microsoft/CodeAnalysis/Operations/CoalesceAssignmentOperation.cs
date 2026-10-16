using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000445 RID: 1093
	[NullableContext(2)]
	[Nullable(0)]
	internal sealed class CoalesceAssignmentOperation : BaseAssignmentOperation, ICoalesceAssignmentOperation, IAssignmentOperation, IOperation
	{
		// Token: 0x060029D8 RID: 10712 RVA: 0x00078DEE File Offset: 0x00076FEE
		[NullableContext(1)]
		internal CoalesceAssignmentOperation(IOperation target, IOperation value, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, [Nullable(2)] ITypeSymbol type, bool isImplicit)
			: base(target, value, semanticModel, syntax, isImplicit)
		{
			this.Type = type;
		}

		// Token: 0x060029D9 RID: 10713 RVA: 0x00078E08 File Offset: 0x00077008
		[NullableContext(1)]
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot != 0)
			{
				if (slot == 1)
				{
					if (base.Value != null)
					{
						return base.Value;
					}
				}
			}
			else if (base.Target != null)
			{
				return base.Target;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x060029DA RID: 10714 RVA: 0x00078E58 File Offset: 0x00077058
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			switch (previousSlot)
			{
			case -1:
				if (base.Target != null)
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
			if (base.Value != null)
			{
				return new ValueTuple<bool, int, int>(true, 1, 0);
			}
			IL_003C:
			return new ValueTuple<bool, int, int>(false, 2, 0);
		}

		// Token: 0x17000C68 RID: 3176
		// (get) Token: 0x060029DB RID: 10715 RVA: 0x00078EBB File Offset: 0x000770BB
		public override ITypeSymbol Type { get; }

		// Token: 0x17000C69 RID: 3177
		// (get) Token: 0x060029DC RID: 10716 RVA: 0x00078EC3 File Offset: 0x000770C3
		internal override ConstantValue OperationConstantValue
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000C6A RID: 3178
		// (get) Token: 0x060029DD RID: 10717 RVA: 0x00078EC6 File Offset: 0x000770C6
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.CoalesceAssignment;
			}
		}

		// Token: 0x060029DE RID: 10718 RVA: 0x00078ECA File Offset: 0x000770CA
		[NullableContext(1)]
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitCoalesceAssignment(this);
		}

		// Token: 0x060029DF RID: 10719 RVA: 0x00078ED3 File Offset: 0x000770D3
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitCoalesceAssignment(this, argument);
		}
	}
}
