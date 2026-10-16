using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000446 RID: 1094
	[NullableContext(2)]
	[Nullable(0)]
	internal sealed class RangeOperation : Operation, IRangeOperation, IOperation
	{
		// Token: 0x060029E0 RID: 10720 RVA: 0x00078EDD File Offset: 0x000770DD
		internal RangeOperation(IOperation leftOperand, IOperation rightOperand, bool isLifted, IMethodSymbol method, SemanticModel semanticModel, [Nullable(1)] SyntaxNode syntax, ITypeSymbol type, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.LeftOperand = Operation.SetParentOperation<IOperation>(leftOperand, this);
			this.RightOperand = Operation.SetParentOperation<IOperation>(rightOperand, this);
			this.IsLifted = isLifted;
			this.Method = method;
			this.Type = type;
		}

		// Token: 0x17000C6B RID: 3179
		// (get) Token: 0x060029E1 RID: 10721 RVA: 0x00078F1C File Offset: 0x0007711C
		public IOperation LeftOperand { get; }

		// Token: 0x17000C6C RID: 3180
		// (get) Token: 0x060029E2 RID: 10722 RVA: 0x00078F24 File Offset: 0x00077124
		public IOperation RightOperand { get; }

		// Token: 0x17000C6D RID: 3181
		// (get) Token: 0x060029E3 RID: 10723 RVA: 0x00078F2C File Offset: 0x0007712C
		public bool IsLifted { get; }

		// Token: 0x17000C6E RID: 3182
		// (get) Token: 0x060029E4 RID: 10724 RVA: 0x00078F34 File Offset: 0x00077134
		public IMethodSymbol Method { get; }

		// Token: 0x060029E5 RID: 10725 RVA: 0x00078F3C File Offset: 0x0007713C
		[NullableContext(1)]
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot != 0)
			{
				if (slot == 1)
				{
					if (this.RightOperand != null)
					{
						return this.RightOperand;
					}
				}
			}
			else if (this.LeftOperand != null)
			{
				return this.LeftOperand;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x060029E6 RID: 10726 RVA: 0x00078F8C File Offset: 0x0007718C
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			switch (previousSlot)
			{
			case -1:
				if (this.LeftOperand != null)
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
			if (this.RightOperand != null)
			{
				return new ValueTuple<bool, int, int>(true, 1, 0);
			}
			IL_003C:
			return new ValueTuple<bool, int, int>(false, 2, 0);
		}

		// Token: 0x17000C6F RID: 3183
		// (get) Token: 0x060029E7 RID: 10727 RVA: 0x00078FEF File Offset: 0x000771EF
		public override ITypeSymbol Type { get; }

		// Token: 0x17000C70 RID: 3184
		// (get) Token: 0x060029E8 RID: 10728 RVA: 0x00078FF7 File Offset: 0x000771F7
		internal override ConstantValue OperationConstantValue
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000C71 RID: 3185
		// (get) Token: 0x060029E9 RID: 10729 RVA: 0x00078FFA File Offset: 0x000771FA
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.Range;
			}
		}

		// Token: 0x060029EA RID: 10730 RVA: 0x00078FFE File Offset: 0x000771FE
		[NullableContext(1)]
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitRangeOperation(this);
		}

		// Token: 0x060029EB RID: 10731 RVA: 0x00079007 File Offset: 0x00077207
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitRangeOperation(this, argument);
		}
	}
}
