using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003E9 RID: 1001
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class BranchOperation : Operation, IBranchOperation, IOperation
	{
		// Token: 0x06002664 RID: 9828 RVA: 0x000741CB File Offset: 0x000723CB
		internal BranchOperation(ILabelSymbol target, BranchKind branchKind, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.Target = target;
			this.BranchKind = branchKind;
		}

		// Token: 0x17000AA9 RID: 2729
		// (get) Token: 0x06002665 RID: 9829 RVA: 0x000741E6 File Offset: 0x000723E6
		public ILabelSymbol Target { get; }

		// Token: 0x17000AAA RID: 2730
		// (get) Token: 0x06002666 RID: 9830 RVA: 0x000741EE File Offset: 0x000723EE
		public BranchKind BranchKind { get; }

		// Token: 0x06002667 RID: 9831 RVA: 0x000741F6 File Offset: 0x000723F6
		protected override IOperation GetCurrent(int slot, int index)
		{
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x06002668 RID: 9832 RVA: 0x00074209 File Offset: 0x00072409
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			return new ValueTuple<bool, int, int>(false, int.MinValue, int.MinValue);
		}

		// Token: 0x17000AAB RID: 2731
		// (get) Token: 0x06002669 RID: 9833 RVA: 0x0007421B File Offset: 0x0007241B
		[Nullable(2)]
		public override ITypeSymbol Type
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x0600266A RID: 9834 RVA: 0x0007421E File Offset: 0x0007241E
		[Nullable(2)]
		internal override ConstantValue OperationConstantValue
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x0600266B RID: 9835 RVA: 0x00074221 File Offset: 0x00072421
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.Branch;
			}
		}

		// Token: 0x0600266C RID: 9836 RVA: 0x00074224 File Offset: 0x00072424
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitBranch(this);
		}

		// Token: 0x0600266D RID: 9837 RVA: 0x0007422D File Offset: 0x0007242D
		[NullableContext(2)]
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitBranch(this, argument);
		}
	}
}
