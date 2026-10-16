using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x0200041F RID: 1055
	[NullableContext(2)]
	[Nullable(0)]
	internal sealed class IncrementOrDecrementOperation : Operation, IIncrementOrDecrementOperation, IOperation
	{
		// Token: 0x0600287D RID: 10365 RVA: 0x000770F0 File Offset: 0x000752F0
		internal IncrementOrDecrementOperation(bool isPostfix, bool isLifted, bool isChecked, [Nullable(1)] IOperation target, IMethodSymbol operatorMethod, OperationKind kind, SemanticModel semanticModel, [Nullable(1)] SyntaxNode syntax, ITypeSymbol type, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.IsPostfix = isPostfix;
			this.IsLifted = isLifted;
			this.IsChecked = isChecked;
			this.Target = Operation.SetParentOperation<IOperation>(target, this);
			this.OperatorMethod = operatorMethod;
			this.Type = type;
			this.Kind = kind;
		}

		// Token: 0x17000BBC RID: 3004
		// (get) Token: 0x0600287E RID: 10366 RVA: 0x00077144 File Offset: 0x00075344
		public bool IsPostfix { get; }

		// Token: 0x17000BBD RID: 3005
		// (get) Token: 0x0600287F RID: 10367 RVA: 0x0007714C File Offset: 0x0007534C
		public bool IsLifted { get; }

		// Token: 0x17000BBE RID: 3006
		// (get) Token: 0x06002880 RID: 10368 RVA: 0x00077154 File Offset: 0x00075354
		public bool IsChecked { get; }

		// Token: 0x17000BBF RID: 3007
		// (get) Token: 0x06002881 RID: 10369 RVA: 0x0007715C File Offset: 0x0007535C
		[Nullable(1)]
		public IOperation Target
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x17000BC0 RID: 3008
		// (get) Token: 0x06002882 RID: 10370 RVA: 0x00077164 File Offset: 0x00075364
		public IMethodSymbol OperatorMethod { get; }

		// Token: 0x06002883 RID: 10371 RVA: 0x0007716C File Offset: 0x0007536C
		[NullableContext(1)]
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot == 0 && this.Target != null)
			{
				return this.Target;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x06002884 RID: 10372 RVA: 0x000771A0 File Offset: 0x000753A0
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
			else if (this.Target != null)
			{
				return new ValueTuple<bool, int, int>(true, 0, 0);
			}
			return new ValueTuple<bool, int, int>(false, 1, 0);
		}

		// Token: 0x17000BC1 RID: 3009
		// (get) Token: 0x06002885 RID: 10373 RVA: 0x000771D7 File Offset: 0x000753D7
		public override ITypeSymbol Type { get; }

		// Token: 0x17000BC2 RID: 3010
		// (get) Token: 0x06002886 RID: 10374 RVA: 0x000771DF File Offset: 0x000753DF
		internal override ConstantValue OperationConstantValue
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000BC3 RID: 3011
		// (get) Token: 0x06002887 RID: 10375 RVA: 0x000771E2 File Offset: 0x000753E2
		public override OperationKind Kind { get; }

		// Token: 0x06002888 RID: 10376 RVA: 0x000771EA File Offset: 0x000753EA
		[NullableContext(1)]
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitIncrementOrDecrement(this);
		}

		// Token: 0x06002889 RID: 10377 RVA: 0x000771F3 File Offset: 0x000753F3
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitIncrementOrDecrement(this, argument);
		}
	}
}
