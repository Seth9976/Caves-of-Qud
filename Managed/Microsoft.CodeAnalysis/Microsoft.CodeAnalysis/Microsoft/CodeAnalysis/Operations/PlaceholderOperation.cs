using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000451 RID: 1105
	[NullableContext(2)]
	[Nullable(0)]
	internal sealed class PlaceholderOperation : Operation, IPlaceholderOperation, IOperation
	{
		// Token: 0x06002A53 RID: 10835 RVA: 0x00079B00 File Offset: 0x00077D00
		internal PlaceholderOperation(PlaceholderKind placeholderKind, SemanticModel semanticModel, [Nullable(1)] SyntaxNode syntax, ITypeSymbol type, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.PlaceholderKind = placeholderKind;
			this.Type = type;
		}

		// Token: 0x17000CA7 RID: 3239
		// (get) Token: 0x06002A54 RID: 10836 RVA: 0x00079B1B File Offset: 0x00077D1B
		public PlaceholderKind PlaceholderKind { get; }

		// Token: 0x06002A55 RID: 10837 RVA: 0x00079B23 File Offset: 0x00077D23
		[NullableContext(1)]
		protected override IOperation GetCurrent(int slot, int index)
		{
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x06002A56 RID: 10838 RVA: 0x00079B36 File Offset: 0x00077D36
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			return new ValueTuple<bool, int, int>(false, int.MinValue, int.MinValue);
		}

		// Token: 0x17000CA8 RID: 3240
		// (get) Token: 0x06002A57 RID: 10839 RVA: 0x00079B48 File Offset: 0x00077D48
		public override ITypeSymbol Type { get; }

		// Token: 0x17000CA9 RID: 3241
		// (get) Token: 0x06002A58 RID: 10840 RVA: 0x00079B50 File Offset: 0x00077D50
		internal override ConstantValue OperationConstantValue
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000CAA RID: 3242
		// (get) Token: 0x06002A59 RID: 10841 RVA: 0x00079B53 File Offset: 0x00077D53
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.None;
			}
		}

		// Token: 0x06002A5A RID: 10842 RVA: 0x00079B56 File Offset: 0x00077D56
		[NullableContext(1)]
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitPlaceholder(this);
		}

		// Token: 0x06002A5B RID: 10843 RVA: 0x00079B5F File Offset: 0x00077D5F
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitPlaceholder(this, argument);
		}
	}
}
