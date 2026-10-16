using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000400 RID: 1024
	[NullableContext(2)]
	[Nullable(0)]
	internal sealed class BinaryOperation : Operation, IBinaryOperation, IOperation
	{
		// Token: 0x0600274B RID: 10059 RVA: 0x000754E0 File Offset: 0x000736E0
		internal BinaryOperation(BinaryOperatorKind operatorKind, [Nullable(1)] IOperation leftOperand, [Nullable(1)] IOperation rightOperand, bool isLifted, bool isChecked, bool isCompareText, IMethodSymbol operatorMethod, IMethodSymbol unaryOperatorMethod, SemanticModel semanticModel, [Nullable(1)] SyntaxNode syntax, ITypeSymbol type, ConstantValue constantValue, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.OperatorKind = operatorKind;
			this.LeftOperand = Operation.SetParentOperation<IOperation>(leftOperand, this);
			this.RightOperand = Operation.SetParentOperation<IOperation>(rightOperand, this);
			this.IsLifted = isLifted;
			this.IsChecked = isChecked;
			this.IsCompareText = isCompareText;
			this.OperatorMethod = operatorMethod;
			this.UnaryOperatorMethod = unaryOperatorMethod;
			this.OperationConstantValue = constantValue;
			this.Type = type;
		}

		// Token: 0x17000B21 RID: 2849
		// (get) Token: 0x0600274C RID: 10060 RVA: 0x00075552 File Offset: 0x00073752
		public BinaryOperatorKind OperatorKind { get; }

		// Token: 0x17000B22 RID: 2850
		// (get) Token: 0x0600274D RID: 10061 RVA: 0x0007555A File Offset: 0x0007375A
		[Nullable(1)]
		public IOperation LeftOperand
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x17000B23 RID: 2851
		// (get) Token: 0x0600274E RID: 10062 RVA: 0x00075562 File Offset: 0x00073762
		[Nullable(1)]
		public IOperation RightOperand
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x17000B24 RID: 2852
		// (get) Token: 0x0600274F RID: 10063 RVA: 0x0007556A File Offset: 0x0007376A
		public bool IsLifted { get; }

		// Token: 0x17000B25 RID: 2853
		// (get) Token: 0x06002750 RID: 10064 RVA: 0x00075572 File Offset: 0x00073772
		public bool IsChecked { get; }

		// Token: 0x17000B26 RID: 2854
		// (get) Token: 0x06002751 RID: 10065 RVA: 0x0007557A File Offset: 0x0007377A
		public bool IsCompareText { get; }

		// Token: 0x17000B27 RID: 2855
		// (get) Token: 0x06002752 RID: 10066 RVA: 0x00075582 File Offset: 0x00073782
		public IMethodSymbol OperatorMethod { get; }

		// Token: 0x17000B28 RID: 2856
		// (get) Token: 0x06002753 RID: 10067 RVA: 0x0007558A File Offset: 0x0007378A
		public IMethodSymbol UnaryOperatorMethod { get; }

		// Token: 0x06002754 RID: 10068 RVA: 0x00075594 File Offset: 0x00073794
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

		// Token: 0x06002755 RID: 10069 RVA: 0x000755E4 File Offset: 0x000737E4
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

		// Token: 0x17000B29 RID: 2857
		// (get) Token: 0x06002756 RID: 10070 RVA: 0x00075647 File Offset: 0x00073847
		public override ITypeSymbol Type { get; }

		// Token: 0x17000B2A RID: 2858
		// (get) Token: 0x06002757 RID: 10071 RVA: 0x0007564F File Offset: 0x0007384F
		internal override ConstantValue OperationConstantValue { get; }

		// Token: 0x17000B2B RID: 2859
		// (get) Token: 0x06002758 RID: 10072 RVA: 0x00075657 File Offset: 0x00073857
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.Binary;
			}
		}

		// Token: 0x06002759 RID: 10073 RVA: 0x0007565B File Offset: 0x0007385B
		[NullableContext(1)]
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitBinaryOperator(this);
		}

		// Token: 0x0600275A RID: 10074 RVA: 0x00075664 File Offset: 0x00073864
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitBinaryOperator(this, argument);
		}
	}
}
