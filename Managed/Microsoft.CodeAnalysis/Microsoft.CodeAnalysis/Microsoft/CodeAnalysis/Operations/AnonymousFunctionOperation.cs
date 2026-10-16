using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000403 RID: 1027
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class AnonymousFunctionOperation : Operation, IAnonymousFunctionOperation, IOperation
	{
		// Token: 0x06002773 RID: 10099 RVA: 0x00075932 File Offset: 0x00073B32
		internal AnonymousFunctionOperation(IMethodSymbol symbol, IBlockOperation body, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.Symbol = symbol;
			this.Body = Operation.SetParentOperation<IBlockOperation>(body, this);
		}

		// Token: 0x17000B3A RID: 2874
		// (get) Token: 0x06002774 RID: 10100 RVA: 0x00075953 File Offset: 0x00073B53
		public IMethodSymbol Symbol { get; }

		// Token: 0x17000B3B RID: 2875
		// (get) Token: 0x06002775 RID: 10101 RVA: 0x0007595B File Offset: 0x00073B5B
		public IBlockOperation Body { get; }

		// Token: 0x06002776 RID: 10102 RVA: 0x00075964 File Offset: 0x00073B64
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot == 0 && this.Body != null)
			{
				return this.Body;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x06002777 RID: 10103 RVA: 0x00075998 File Offset: 0x00073B98
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
			else if (this.Body != null)
			{
				return new ValueTuple<bool, int, int>(true, 0, 0);
			}
			return new ValueTuple<bool, int, int>(false, 1, 0);
		}

		// Token: 0x17000B3C RID: 2876
		// (get) Token: 0x06002778 RID: 10104 RVA: 0x000759CF File Offset: 0x00073BCF
		[Nullable(2)]
		public override ITypeSymbol Type
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000B3D RID: 2877
		// (get) Token: 0x06002779 RID: 10105 RVA: 0x000759D2 File Offset: 0x00073BD2
		[Nullable(2)]
		internal override ConstantValue OperationConstantValue
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000B3E RID: 2878
		// (get) Token: 0x0600277A RID: 10106 RVA: 0x000759D5 File Offset: 0x00073BD5
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.AnonymousFunction;
			}
		}

		// Token: 0x0600277B RID: 10107 RVA: 0x000759D9 File Offset: 0x00073BD9
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitAnonymousFunction(this);
		}

		// Token: 0x0600277C RID: 10108 RVA: 0x000759E2 File Offset: 0x00073BE2
		[NullableContext(2)]
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitAnonymousFunction(this, argument);
		}
	}
}
