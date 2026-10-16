using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000457 RID: 1111
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class RelationalPatternOperation : BasePatternOperation, IRelationalPatternOperation, IPatternOperation, IOperation
	{
		// Token: 0x06002A8E RID: 10894 RVA: 0x00079F65 File Offset: 0x00078165
		internal RelationalPatternOperation(BinaryOperatorKind operatorKind, IOperation value, ITypeSymbol inputType, ITypeSymbol narrowedType, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, bool isImplicit)
			: base(inputType, narrowedType, semanticModel, syntax, isImplicit)
		{
			this.OperatorKind = operatorKind;
			this.Value = Operation.SetParentOperation<IOperation>(value, this);
		}

		// Token: 0x17000CC4 RID: 3268
		// (get) Token: 0x06002A8F RID: 10895 RVA: 0x00079F8A File Offset: 0x0007818A
		public BinaryOperatorKind OperatorKind { get; }

		// Token: 0x17000CC5 RID: 3269
		// (get) Token: 0x06002A90 RID: 10896 RVA: 0x00079F92 File Offset: 0x00078192
		public IOperation Value { get; }

		// Token: 0x06002A91 RID: 10897 RVA: 0x00079F9C File Offset: 0x0007819C
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot == 0 && this.Value != null)
			{
				return this.Value;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x06002A92 RID: 10898 RVA: 0x00079FD0 File Offset: 0x000781D0
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			if (previousSlot != -1)
			{
				if (previousSlot > 1)
				{
					throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(previousSlot, previousIndex));
				}
			}
			else if (this.Value != null)
			{
				return new ValueTuple<bool, int, int>(true, 0, 0);
			}
			return new ValueTuple<bool, int, int>(false, 1, 0);
		}

		// Token: 0x17000CC6 RID: 3270
		// (get) Token: 0x06002A93 RID: 10899 RVA: 0x0007A007 File Offset: 0x00078207
		[Nullable(2)]
		public override ITypeSymbol Type
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000CC7 RID: 3271
		// (get) Token: 0x06002A94 RID: 10900 RVA: 0x0007A00A File Offset: 0x0007820A
		[Nullable(2)]
		internal override ConstantValue OperationConstantValue
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000CC8 RID: 3272
		// (get) Token: 0x06002A95 RID: 10901 RVA: 0x0007A00D File Offset: 0x0007820D
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.RelationalPattern;
			}
		}

		// Token: 0x06002A96 RID: 10902 RVA: 0x0007A011 File Offset: 0x00078211
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitRelationalPattern(this);
		}

		// Token: 0x06002A97 RID: 10903 RVA: 0x0007A01A File Offset: 0x0007821A
		[NullableContext(2)]
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitRelationalPattern(this, argument);
		}
	}
}
