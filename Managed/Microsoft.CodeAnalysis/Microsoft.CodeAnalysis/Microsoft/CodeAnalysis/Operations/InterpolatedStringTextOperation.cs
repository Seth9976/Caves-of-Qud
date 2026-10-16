using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000436 RID: 1078
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class InterpolatedStringTextOperation : BaseInterpolatedStringContentOperation, IInterpolatedStringTextOperation, IInterpolatedStringContentOperation, IOperation
	{
		// Token: 0x06002952 RID: 10578 RVA: 0x000783B2 File Offset: 0x000765B2
		internal InterpolatedStringTextOperation(IOperation text, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.Text = Operation.SetParentOperation<IOperation>(text, this);
		}

		// Token: 0x17000C2A RID: 3114
		// (get) Token: 0x06002953 RID: 10579 RVA: 0x000783CB File Offset: 0x000765CB
		public IOperation Text { get; }

		// Token: 0x06002954 RID: 10580 RVA: 0x000783D4 File Offset: 0x000765D4
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot == 0 && this.Text != null)
			{
				return this.Text;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x06002955 RID: 10581 RVA: 0x00078408 File Offset: 0x00076608
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
			else if (this.Text != null)
			{
				return new ValueTuple<bool, int, int>(true, 0, 0);
			}
			return new ValueTuple<bool, int, int>(false, 1, 0);
		}

		// Token: 0x17000C2B RID: 3115
		// (get) Token: 0x06002956 RID: 10582 RVA: 0x0007843F File Offset: 0x0007663F
		[Nullable(2)]
		public override ITypeSymbol Type
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000C2C RID: 3116
		// (get) Token: 0x06002957 RID: 10583 RVA: 0x00078442 File Offset: 0x00076642
		[Nullable(2)]
		internal override ConstantValue OperationConstantValue
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000C2D RID: 3117
		// (get) Token: 0x06002958 RID: 10584 RVA: 0x00078445 File Offset: 0x00076645
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.InterpolatedStringText;
			}
		}

		// Token: 0x06002959 RID: 10585 RVA: 0x00078449 File Offset: 0x00076649
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitInterpolatedStringText(this);
		}

		// Token: 0x0600295A RID: 10586 RVA: 0x00078452 File Offset: 0x00076652
		[NullableContext(2)]
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitInterpolatedStringText(this, argument);
		}
	}
}
