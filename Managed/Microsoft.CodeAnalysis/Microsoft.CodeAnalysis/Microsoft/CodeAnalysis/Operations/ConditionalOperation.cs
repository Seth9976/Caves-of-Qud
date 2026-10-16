using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000401 RID: 1025
	[NullableContext(2)]
	[Nullable(0)]
	internal sealed class ConditionalOperation : Operation, IConditionalOperation, IOperation
	{
		// Token: 0x0600275B RID: 10075 RVA: 0x00075670 File Offset: 0x00073870
		internal ConditionalOperation([Nullable(1)] IOperation condition, [Nullable(1)] IOperation whenTrue, IOperation whenFalse, bool isRef, SemanticModel semanticModel, [Nullable(1)] SyntaxNode syntax, ITypeSymbol type, ConstantValue constantValue, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.Condition = Operation.SetParentOperation<IOperation>(condition, this);
			this.WhenTrue = Operation.SetParentOperation<IOperation>(whenTrue, this);
			this.WhenFalse = Operation.SetParentOperation<IOperation>(whenFalse, this);
			this.IsRef = isRef;
			this.OperationConstantValue = constantValue;
			this.Type = type;
		}

		// Token: 0x17000B2C RID: 2860
		// (get) Token: 0x0600275C RID: 10076 RVA: 0x000756C8 File Offset: 0x000738C8
		[Nullable(1)]
		public IOperation Condition
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x17000B2D RID: 2861
		// (get) Token: 0x0600275D RID: 10077 RVA: 0x000756D0 File Offset: 0x000738D0
		[Nullable(1)]
		public IOperation WhenTrue
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x17000B2E RID: 2862
		// (get) Token: 0x0600275E RID: 10078 RVA: 0x000756D8 File Offset: 0x000738D8
		public IOperation WhenFalse { get; }

		// Token: 0x17000B2F RID: 2863
		// (get) Token: 0x0600275F RID: 10079 RVA: 0x000756E0 File Offset: 0x000738E0
		public bool IsRef { get; }

		// Token: 0x06002760 RID: 10080 RVA: 0x000756E8 File Offset: 0x000738E8
		[NullableContext(1)]
		protected override IOperation GetCurrent(int slot, int index)
		{
			switch (slot)
			{
			case 0:
				if (this.Condition != null)
				{
					return this.Condition;
				}
				break;
			case 1:
				if (this.WhenTrue != null)
				{
					return this.WhenTrue;
				}
				break;
			case 2:
				if (this.WhenFalse != null)
				{
					return this.WhenFalse;
				}
				break;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x06002761 RID: 10081 RVA: 0x00075754 File Offset: 0x00073954
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			switch (previousSlot)
			{
			case -1:
				if (this.Condition != null)
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
			if (this.WhenTrue != null)
			{
				return new ValueTuple<bool, int, int>(true, 1, 0);
			}
			IL_0040:
			if (this.WhenFalse != null)
			{
				return new ValueTuple<bool, int, int>(true, 2, 0);
			}
			IL_0051:
			return new ValueTuple<bool, int, int>(false, 3, 0);
		}

		// Token: 0x17000B30 RID: 2864
		// (get) Token: 0x06002762 RID: 10082 RVA: 0x000757CC File Offset: 0x000739CC
		public override ITypeSymbol Type { get; }

		// Token: 0x17000B31 RID: 2865
		// (get) Token: 0x06002763 RID: 10083 RVA: 0x000757D4 File Offset: 0x000739D4
		internal override ConstantValue OperationConstantValue { get; }

		// Token: 0x17000B32 RID: 2866
		// (get) Token: 0x06002764 RID: 10084 RVA: 0x000757DC File Offset: 0x000739DC
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.Conditional;
			}
		}

		// Token: 0x06002765 RID: 10085 RVA: 0x000757E0 File Offset: 0x000739E0
		[NullableContext(1)]
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitConditional(this);
		}

		// Token: 0x06002766 RID: 10086 RVA: 0x000757E9 File Offset: 0x000739E9
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitConditional(this, argument);
		}
	}
}
