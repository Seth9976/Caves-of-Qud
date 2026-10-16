using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x0200044D RID: 1101
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class PropertySubpatternOperation : Operation, IPropertySubpatternOperation, IOperation
	{
		// Token: 0x06002A2B RID: 10795 RVA: 0x00079719 File Offset: 0x00077919
		internal PropertySubpatternOperation(IOperation member, IPatternOperation pattern, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.Member = Operation.SetParentOperation<IOperation>(member, this);
			this.Pattern = Operation.SetParentOperation<IPatternOperation>(pattern, this);
		}

		// Token: 0x17000C93 RID: 3219
		// (get) Token: 0x06002A2C RID: 10796 RVA: 0x00079740 File Offset: 0x00077940
		public IOperation Member { get; }

		// Token: 0x17000C94 RID: 3220
		// (get) Token: 0x06002A2D RID: 10797 RVA: 0x00079748 File Offset: 0x00077948
		public IPatternOperation Pattern { get; }

		// Token: 0x06002A2E RID: 10798 RVA: 0x00079750 File Offset: 0x00077950
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot != 0)
			{
				if (slot == 1)
				{
					if (this.Pattern != null)
					{
						return this.Pattern;
					}
				}
			}
			else if (this.Member != null)
			{
				return this.Member;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x06002A2F RID: 10799 RVA: 0x000797A0 File Offset: 0x000779A0
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			switch (previousSlot)
			{
			case -1:
				if (this.Member != null)
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
			if (this.Pattern != null)
			{
				return new ValueTuple<bool, int, int>(true, 1, 0);
			}
			IL_003C:
			return new ValueTuple<bool, int, int>(false, 2, 0);
		}

		// Token: 0x17000C95 RID: 3221
		// (get) Token: 0x06002A30 RID: 10800 RVA: 0x00079803 File Offset: 0x00077A03
		[Nullable(2)]
		public override ITypeSymbol Type
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000C96 RID: 3222
		// (get) Token: 0x06002A31 RID: 10801 RVA: 0x00079806 File Offset: 0x00077A06
		[Nullable(2)]
		internal override ConstantValue OperationConstantValue
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000C97 RID: 3223
		// (get) Token: 0x06002A32 RID: 10802 RVA: 0x00079809 File Offset: 0x00077A09
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.PropertySubpattern;
			}
		}

		// Token: 0x06002A33 RID: 10803 RVA: 0x0007980D File Offset: 0x00077A0D
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitPropertySubpattern(this);
		}

		// Token: 0x06002A34 RID: 10804 RVA: 0x00079816 File Offset: 0x00077A16
		[NullableContext(2)]
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitPropertySubpattern(this, argument);
		}
	}
}
