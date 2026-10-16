using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000454 RID: 1108
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class NegatedPatternOperation : BasePatternOperation, INegatedPatternOperation, IPatternOperation, IOperation
	{
		// Token: 0x06002A71 RID: 10865 RVA: 0x00079D38 File Offset: 0x00077F38
		internal NegatedPatternOperation(IPatternOperation pattern, ITypeSymbol inputType, ITypeSymbol narrowedType, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, bool isImplicit)
			: base(inputType, narrowedType, semanticModel, syntax, isImplicit)
		{
			this.Pattern = Operation.SetParentOperation<IPatternOperation>(pattern, this);
		}

		// Token: 0x17000CB6 RID: 3254
		// (get) Token: 0x06002A72 RID: 10866 RVA: 0x00079D55 File Offset: 0x00077F55
		public IPatternOperation Pattern { get; }

		// Token: 0x06002A73 RID: 10867 RVA: 0x00079D60 File Offset: 0x00077F60
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot == 0 && this.Pattern != null)
			{
				return this.Pattern;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x06002A74 RID: 10868 RVA: 0x00079D94 File Offset: 0x00077F94
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
			else if (this.Pattern != null)
			{
				return new ValueTuple<bool, int, int>(true, 0, 0);
			}
			return new ValueTuple<bool, int, int>(false, 1, 0);
		}

		// Token: 0x17000CB7 RID: 3255
		// (get) Token: 0x06002A75 RID: 10869 RVA: 0x00079DCB File Offset: 0x00077FCB
		[Nullable(2)]
		public override ITypeSymbol Type
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000CB8 RID: 3256
		// (get) Token: 0x06002A76 RID: 10870 RVA: 0x00079DCE File Offset: 0x00077FCE
		[Nullable(2)]
		internal override ConstantValue OperationConstantValue
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000CB9 RID: 3257
		// (get) Token: 0x06002A77 RID: 10871 RVA: 0x00079DD1 File Offset: 0x00077FD1
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.NegatedPattern;
			}
		}

		// Token: 0x06002A78 RID: 10872 RVA: 0x00079DD5 File Offset: 0x00077FD5
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitNegatedPattern(this);
		}

		// Token: 0x06002A79 RID: 10873 RVA: 0x00079DDE File Offset: 0x00077FDE
		[NullableContext(2)]
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitNegatedPattern(this, argument);
		}
	}
}
