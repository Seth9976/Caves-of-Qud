using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x0200043F RID: 1087
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class DiscardOperation : Operation, IDiscardOperation, IOperation
	{
		// Token: 0x0600299D RID: 10653 RVA: 0x00078A7D File Offset: 0x00076C7D
		internal DiscardOperation(IDiscardSymbol discardSymbol, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, [Nullable(2)] ITypeSymbol type, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.DiscardSymbol = discardSymbol;
			this.Type = type;
		}

		// Token: 0x17000C50 RID: 3152
		// (get) Token: 0x0600299E RID: 10654 RVA: 0x00078A98 File Offset: 0x00076C98
		public IDiscardSymbol DiscardSymbol { get; }

		// Token: 0x0600299F RID: 10655 RVA: 0x00078AA0 File Offset: 0x00076CA0
		protected override IOperation GetCurrent(int slot, int index)
		{
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x060029A0 RID: 10656 RVA: 0x00078AB3 File Offset: 0x00076CB3
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			return new ValueTuple<bool, int, int>(false, int.MinValue, int.MinValue);
		}

		// Token: 0x17000C51 RID: 3153
		// (get) Token: 0x060029A1 RID: 10657 RVA: 0x00078AC5 File Offset: 0x00076CC5
		[Nullable(2)]
		public override ITypeSymbol Type
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x17000C52 RID: 3154
		// (get) Token: 0x060029A2 RID: 10658 RVA: 0x00078ACD File Offset: 0x00076CCD
		[Nullable(2)]
		internal override ConstantValue OperationConstantValue
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000C53 RID: 3155
		// (get) Token: 0x060029A3 RID: 10659 RVA: 0x00078AD0 File Offset: 0x00076CD0
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.Discard;
			}
		}

		// Token: 0x060029A4 RID: 10660 RVA: 0x00078AD4 File Offset: 0x00076CD4
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitDiscardOperation(this);
		}

		// Token: 0x060029A5 RID: 10661 RVA: 0x00078ADD File Offset: 0x00076CDD
		[NullableContext(2)]
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitDiscardOperation(this, argument);
		}
	}
}
