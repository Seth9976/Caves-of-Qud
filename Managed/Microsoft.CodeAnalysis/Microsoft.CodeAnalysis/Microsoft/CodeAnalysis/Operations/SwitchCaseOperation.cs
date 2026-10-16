using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x0200042E RID: 1070
	[NullableContext(2)]
	[Nullable(0)]
	internal sealed class SwitchCaseOperation : Operation, ISwitchCaseOperation, IOperation
	{
		// Token: 0x0600290D RID: 10509 RVA: 0x00077E09 File Offset: 0x00076009
		internal SwitchCaseOperation([Nullable(new byte[] { 0, 1 })] ImmutableArray<ICaseClauseOperation> clauses, [Nullable(new byte[] { 0, 1 })] ImmutableArray<IOperation> body, [Nullable(new byte[] { 0, 1 })] ImmutableArray<ILocalSymbol> locals, IOperation condition, SemanticModel semanticModel, [Nullable(1)] SyntaxNode syntax, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.Clauses = Operation.SetParentOperation<ICaseClauseOperation>(clauses, this);
			this.Body = Operation.SetParentOperation<IOperation>(body, this);
			this.Locals = locals;
			this.Condition = Operation.SetParentOperation<IOperation>(condition, this);
		}

		// Token: 0x17000C05 RID: 3077
		// (get) Token: 0x0600290E RID: 10510 RVA: 0x00077E46 File Offset: 0x00076046
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<ICaseClauseOperation> Clauses
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000C06 RID: 3078
		// (get) Token: 0x0600290F RID: 10511 RVA: 0x00077E4E File Offset: 0x0007604E
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<IOperation> Body
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000C07 RID: 3079
		// (get) Token: 0x06002910 RID: 10512 RVA: 0x00077E56 File Offset: 0x00076056
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<ILocalSymbol> Locals
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000C08 RID: 3080
		// (get) Token: 0x06002911 RID: 10513 RVA: 0x00077E5E File Offset: 0x0007605E
		public IOperation Condition { get; }

		// Token: 0x06002912 RID: 10514 RVA: 0x00077E68 File Offset: 0x00076068
		[NullableContext(1)]
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot != 0)
			{
				if (slot == 1)
				{
					if (index < this.Body.Length)
					{
						return this.Body[index];
					}
				}
			}
			else if (index < this.Clauses.Length)
			{
				return this.Clauses[index];
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x06002913 RID: 10515 RVA: 0x00077EDC File Offset: 0x000760DC
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			switch (previousSlot)
			{
			case -1:
				if (!this.Clauses.IsEmpty)
				{
					return new ValueTuple<bool, int, int>(true, 0, 0);
				}
				break;
			case 0:
				if (previousIndex + 1 < this.Clauses.Length)
				{
					return new ValueTuple<bool, int, int>(true, 0, previousIndex + 1);
				}
				break;
			case 1:
				if (previousIndex + 1 < this.Body.Length)
				{
					return new ValueTuple<bool, int, int>(true, 1, previousIndex + 1);
				}
				goto IL_008A;
			case 2:
				goto IL_008A;
			default:
				throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(previousSlot, previousIndex));
			}
			if (!this.Body.IsEmpty)
			{
				return new ValueTuple<bool, int, int>(true, 1, 0);
			}
			IL_008A:
			return new ValueTuple<bool, int, int>(false, 2, 0);
		}

		// Token: 0x17000C09 RID: 3081
		// (get) Token: 0x06002914 RID: 10516 RVA: 0x00077F8D File Offset: 0x0007618D
		public override ITypeSymbol Type
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000C0A RID: 3082
		// (get) Token: 0x06002915 RID: 10517 RVA: 0x00077F90 File Offset: 0x00076190
		internal override ConstantValue OperationConstantValue
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000C0B RID: 3083
		// (get) Token: 0x06002916 RID: 10518 RVA: 0x00077F93 File Offset: 0x00076193
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.SwitchCase;
			}
		}

		// Token: 0x06002917 RID: 10519 RVA: 0x00077F97 File Offset: 0x00076197
		[NullableContext(1)]
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitSwitchCase(this);
		}

		// Token: 0x06002918 RID: 10520 RVA: 0x00077FA0 File Offset: 0x000761A0
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitSwitchCase(this, argument);
		}
	}
}
