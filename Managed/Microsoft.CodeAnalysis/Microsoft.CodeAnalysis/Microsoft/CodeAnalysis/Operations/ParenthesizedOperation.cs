using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x0200040D RID: 1037
	[NullableContext(2)]
	[Nullable(0)]
	internal sealed class ParenthesizedOperation : Operation, IParenthesizedOperation, IOperation
	{
		// Token: 0x060027D4 RID: 10196 RVA: 0x00076205 File Offset: 0x00074405
		internal ParenthesizedOperation([Nullable(1)] IOperation operand, SemanticModel semanticModel, [Nullable(1)] SyntaxNode syntax, ITypeSymbol type, ConstantValue constantValue, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.Operand = Operation.SetParentOperation<IOperation>(operand, this);
			this.OperationConstantValue = constantValue;
			this.Type = type;
		}

		// Token: 0x17000B6D RID: 2925
		// (get) Token: 0x060027D5 RID: 10197 RVA: 0x0007622E File Offset: 0x0007442E
		[Nullable(1)]
		public IOperation Operand
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x060027D6 RID: 10198 RVA: 0x00076238 File Offset: 0x00074438
		[NullableContext(1)]
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot == 0 && this.Operand != null)
			{
				return this.Operand;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x060027D7 RID: 10199 RVA: 0x0007626C File Offset: 0x0007446C
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
			else if (this.Operand != null)
			{
				return new ValueTuple<bool, int, int>(true, 0, 0);
			}
			return new ValueTuple<bool, int, int>(false, 1, 0);
		}

		// Token: 0x17000B6E RID: 2926
		// (get) Token: 0x060027D8 RID: 10200 RVA: 0x000762A3 File Offset: 0x000744A3
		public override ITypeSymbol Type { get; }

		// Token: 0x17000B6F RID: 2927
		// (get) Token: 0x060027D9 RID: 10201 RVA: 0x000762AB File Offset: 0x000744AB
		internal override ConstantValue OperationConstantValue { get; }

		// Token: 0x17000B70 RID: 2928
		// (get) Token: 0x060027DA RID: 10202 RVA: 0x000762B3 File Offset: 0x000744B3
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.Parenthesized;
			}
		}

		// Token: 0x060027DB RID: 10203 RVA: 0x000762B7 File Offset: 0x000744B7
		[NullableContext(1)]
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitParenthesized(this);
		}

		// Token: 0x060027DC RID: 10204 RVA: 0x000762C0 File Offset: 0x000744C0
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitParenthesized(this, argument);
		}
	}
}
