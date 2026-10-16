using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000430 RID: 1072
	[NullableContext(2)]
	[Nullable(0)]
	internal sealed class DefaultCaseClauseOperation : BaseCaseClauseOperation, IDefaultCaseClauseOperation, ICaseClauseOperation, IOperation
	{
		// Token: 0x0600291C RID: 10524 RVA: 0x00077FC5 File Offset: 0x000761C5
		internal DefaultCaseClauseOperation(ILabelSymbol label, SemanticModel semanticModel, [Nullable(1)] SyntaxNode syntax, bool isImplicit)
			: base(label, semanticModel, syntax, isImplicit)
		{
		}

		// Token: 0x0600291D RID: 10525 RVA: 0x00077FD2 File Offset: 0x000761D2
		[NullableContext(1)]
		protected override IOperation GetCurrent(int slot, int index)
		{
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x0600291E RID: 10526 RVA: 0x00077FE5 File Offset: 0x000761E5
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			return new ValueTuple<bool, int, int>(false, int.MinValue, int.MinValue);
		}

		// Token: 0x17000C0E RID: 3086
		// (get) Token: 0x0600291F RID: 10527 RVA: 0x00077FF7 File Offset: 0x000761F7
		public override ITypeSymbol Type
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000C0F RID: 3087
		// (get) Token: 0x06002920 RID: 10528 RVA: 0x00077FFA File Offset: 0x000761FA
		internal override ConstantValue OperationConstantValue
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000C10 RID: 3088
		// (get) Token: 0x06002921 RID: 10529 RVA: 0x00077FFD File Offset: 0x000761FD
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.CaseClause;
			}
		}

		// Token: 0x06002922 RID: 10530 RVA: 0x00078001 File Offset: 0x00076201
		[NullableContext(1)]
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitDefaultCaseClause(this);
		}

		// Token: 0x06002923 RID: 10531 RVA: 0x0007800A File Offset: 0x0007620A
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitDefaultCaseClause(this, argument);
		}

		// Token: 0x17000C11 RID: 3089
		// (get) Token: 0x06002924 RID: 10532 RVA: 0x00078014 File Offset: 0x00076214
		public override CaseKind CaseKind
		{
			get
			{
				return CaseKind.Default;
			}
		}
	}
}
