using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x0200043B RID: 1083
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class TupleBinaryOperation : Operation, ITupleBinaryOperation, IOperation
	{
		// Token: 0x0600297D RID: 10621 RVA: 0x00078709 File Offset: 0x00076909
		internal TupleBinaryOperation(BinaryOperatorKind operatorKind, IOperation leftOperand, IOperation rightOperand, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, [Nullable(2)] ITypeSymbol type, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.OperatorKind = operatorKind;
			this.LeftOperand = Operation.SetParentOperation<IOperation>(leftOperand, this);
			this.RightOperand = Operation.SetParentOperation<IOperation>(rightOperand, this);
			this.Type = type;
		}

		// Token: 0x17000C40 RID: 3136
		// (get) Token: 0x0600297E RID: 10622 RVA: 0x00078740 File Offset: 0x00076940
		public BinaryOperatorKind OperatorKind { get; }

		// Token: 0x17000C41 RID: 3137
		// (get) Token: 0x0600297F RID: 10623 RVA: 0x00078748 File Offset: 0x00076948
		public IOperation LeftOperand { get; }

		// Token: 0x17000C42 RID: 3138
		// (get) Token: 0x06002980 RID: 10624 RVA: 0x00078750 File Offset: 0x00076950
		public IOperation RightOperand { get; }

		// Token: 0x06002981 RID: 10625 RVA: 0x00078758 File Offset: 0x00076958
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

		// Token: 0x06002982 RID: 10626 RVA: 0x000787A8 File Offset: 0x000769A8
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

		// Token: 0x17000C43 RID: 3139
		// (get) Token: 0x06002983 RID: 10627 RVA: 0x0007880B File Offset: 0x00076A0B
		[Nullable(2)]
		public override ITypeSymbol Type
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x17000C44 RID: 3140
		// (get) Token: 0x06002984 RID: 10628 RVA: 0x00078813 File Offset: 0x00076A13
		[Nullable(2)]
		internal override ConstantValue OperationConstantValue
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000C45 RID: 3141
		// (get) Token: 0x06002985 RID: 10629 RVA: 0x00078816 File Offset: 0x00076A16
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.TupleBinary;
			}
		}

		// Token: 0x06002986 RID: 10630 RVA: 0x0007881A File Offset: 0x00076A1A
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitTupleBinaryOperator(this);
		}

		// Token: 0x06002987 RID: 10631 RVA: 0x00078823 File Offset: 0x00076A23
		[NullableContext(2)]
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitTupleBinaryOperator(this, argument);
		}
	}
}
