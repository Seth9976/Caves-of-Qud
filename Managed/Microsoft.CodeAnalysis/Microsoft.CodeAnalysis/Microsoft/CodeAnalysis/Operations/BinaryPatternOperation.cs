using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000455 RID: 1109
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class BinaryPatternOperation : BasePatternOperation, IBinaryPatternOperation, IPatternOperation, IOperation
	{
		// Token: 0x06002A7A RID: 10874 RVA: 0x00079DE8 File Offset: 0x00077FE8
		internal BinaryPatternOperation(BinaryOperatorKind operatorKind, IPatternOperation leftPattern, IPatternOperation rightPattern, ITypeSymbol inputType, ITypeSymbol narrowedType, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, bool isImplicit)
			: base(inputType, narrowedType, semanticModel, syntax, isImplicit)
		{
			this.OperatorKind = operatorKind;
			this.LeftPattern = Operation.SetParentOperation<IPatternOperation>(leftPattern, this);
			this.RightPattern = Operation.SetParentOperation<IPatternOperation>(rightPattern, this);
		}

		// Token: 0x17000CBA RID: 3258
		// (get) Token: 0x06002A7B RID: 10875 RVA: 0x00079E1B File Offset: 0x0007801B
		public BinaryOperatorKind OperatorKind { get; }

		// Token: 0x17000CBB RID: 3259
		// (get) Token: 0x06002A7C RID: 10876 RVA: 0x00079E23 File Offset: 0x00078023
		public IPatternOperation LeftPattern { get; }

		// Token: 0x17000CBC RID: 3260
		// (get) Token: 0x06002A7D RID: 10877 RVA: 0x00079E2B File Offset: 0x0007802B
		public IPatternOperation RightPattern { get; }

		// Token: 0x06002A7E RID: 10878 RVA: 0x00079E34 File Offset: 0x00078034
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot != 0)
			{
				if (slot == 1)
				{
					if (this.RightPattern != null)
					{
						return this.RightPattern;
					}
				}
			}
			else if (this.LeftPattern != null)
			{
				return this.LeftPattern;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x06002A7F RID: 10879 RVA: 0x00079E84 File Offset: 0x00078084
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			switch (previousSlot)
			{
			case -1:
				if (this.LeftPattern != null)
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
			if (this.RightPattern != null)
			{
				return new ValueTuple<bool, int, int>(true, 1, 0);
			}
			IL_003C:
			return new ValueTuple<bool, int, int>(false, 2, 0);
		}

		// Token: 0x17000CBD RID: 3261
		// (get) Token: 0x06002A80 RID: 10880 RVA: 0x00079EE7 File Offset: 0x000780E7
		[Nullable(2)]
		public override ITypeSymbol Type
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000CBE RID: 3262
		// (get) Token: 0x06002A81 RID: 10881 RVA: 0x00079EEA File Offset: 0x000780EA
		[Nullable(2)]
		internal override ConstantValue OperationConstantValue
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000CBF RID: 3263
		// (get) Token: 0x06002A82 RID: 10882 RVA: 0x00079EED File Offset: 0x000780ED
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.BinaryPattern;
			}
		}

		// Token: 0x06002A83 RID: 10883 RVA: 0x00079EF1 File Offset: 0x000780F1
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitBinaryPattern(this);
		}

		// Token: 0x06002A84 RID: 10884 RVA: 0x00079EFA File Offset: 0x000780FA
		[NullableContext(2)]
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitBinaryPattern(this, argument);
		}
	}
}
