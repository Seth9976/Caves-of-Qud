using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x0200042D RID: 1069
	[NullableContext(2)]
	[Nullable(0)]
	internal sealed class CatchClauseOperation : Operation, ICatchClauseOperation, IOperation
	{
		// Token: 0x06002900 RID: 10496 RVA: 0x00077C90 File Offset: 0x00075E90
		[NullableContext(1)]
		internal CatchClauseOperation([Nullable(2)] IOperation exceptionDeclarationOrExpression, ITypeSymbol exceptionType, [Nullable(new byte[] { 0, 1 })] ImmutableArray<ILocalSymbol> locals, [Nullable(2)] IOperation filter, IBlockOperation handler, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.ExceptionDeclarationOrExpression = Operation.SetParentOperation<IOperation>(exceptionDeclarationOrExpression, this);
			this.ExceptionType = exceptionType;
			this.Locals = locals;
			this.Filter = Operation.SetParentOperation<IOperation>(filter, this);
			this.Handler = Operation.SetParentOperation<IBlockOperation>(handler, this);
		}

		// Token: 0x17000BFD RID: 3069
		// (get) Token: 0x06002901 RID: 10497 RVA: 0x00077CE0 File Offset: 0x00075EE0
		public IOperation ExceptionDeclarationOrExpression { get; }

		// Token: 0x17000BFE RID: 3070
		// (get) Token: 0x06002902 RID: 10498 RVA: 0x00077CE8 File Offset: 0x00075EE8
		[Nullable(1)]
		public ITypeSymbol ExceptionType
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x17000BFF RID: 3071
		// (get) Token: 0x06002903 RID: 10499 RVA: 0x00077CF0 File Offset: 0x00075EF0
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<ILocalSymbol> Locals
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000C00 RID: 3072
		// (get) Token: 0x06002904 RID: 10500 RVA: 0x00077CF8 File Offset: 0x00075EF8
		public IOperation Filter { get; }

		// Token: 0x17000C01 RID: 3073
		// (get) Token: 0x06002905 RID: 10501 RVA: 0x00077D00 File Offset: 0x00075F00
		[Nullable(1)]
		public IBlockOperation Handler
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x06002906 RID: 10502 RVA: 0x00077D08 File Offset: 0x00075F08
		[NullableContext(1)]
		protected override IOperation GetCurrent(int slot, int index)
		{
			switch (slot)
			{
			case 0:
				if (this.ExceptionDeclarationOrExpression != null)
				{
					return this.ExceptionDeclarationOrExpression;
				}
				break;
			case 1:
				if (this.Filter != null)
				{
					return this.Filter;
				}
				break;
			case 2:
				if (this.Handler != null)
				{
					return this.Handler;
				}
				break;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x06002907 RID: 10503 RVA: 0x00077D74 File Offset: 0x00075F74
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			switch (previousSlot)
			{
			case -1:
				if (this.ExceptionDeclarationOrExpression != null)
				{
					return new ValueTuple<bool, int, int>(true, 0, 0);
				}
				break;
			case 0:
				break;
			case 1:
				goto IL_0040;
			case 2:
			case 3:
				goto IL_0051;
			default:
				throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(previousSlot, previousIndex));
			}
			if (this.Filter != null)
			{
				return new ValueTuple<bool, int, int>(true, 1, 0);
			}
			IL_0040:
			if (this.Handler != null)
			{
				return new ValueTuple<bool, int, int>(true, 2, 0);
			}
			IL_0051:
			return new ValueTuple<bool, int, int>(false, 3, 0);
		}

		// Token: 0x17000C02 RID: 3074
		// (get) Token: 0x06002908 RID: 10504 RVA: 0x00077DEC File Offset: 0x00075FEC
		public override ITypeSymbol Type
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000C03 RID: 3075
		// (get) Token: 0x06002909 RID: 10505 RVA: 0x00077DEF File Offset: 0x00075FEF
		internal override ConstantValue OperationConstantValue
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000C04 RID: 3076
		// (get) Token: 0x0600290A RID: 10506 RVA: 0x00077DF2 File Offset: 0x00075FF2
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.CatchClause;
			}
		}

		// Token: 0x0600290B RID: 10507 RVA: 0x00077DF6 File Offset: 0x00075FF6
		[NullableContext(1)]
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitCatchClause(this);
		}

		// Token: 0x0600290C RID: 10508 RVA: 0x00077DFF File Offset: 0x00075FFF
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitCatchClause(this, argument);
		}
	}
}
