using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000422 RID: 1058
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class DeclarationExpressionOperation : Operation, IDeclarationExpressionOperation, IOperation
	{
		// Token: 0x0600289B RID: 10395 RVA: 0x000773A1 File Offset: 0x000755A1
		internal DeclarationExpressionOperation(IOperation expression, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, [Nullable(2)] ITypeSymbol type, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.Expression = Operation.SetParentOperation<IOperation>(expression, this);
			this.Type = type;
		}

		// Token: 0x17000BCB RID: 3019
		// (get) Token: 0x0600289C RID: 10396 RVA: 0x000773C2 File Offset: 0x000755C2
		public IOperation Expression { get; }

		// Token: 0x0600289D RID: 10397 RVA: 0x000773CC File Offset: 0x000755CC
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot == 0 && this.Expression != null)
			{
				return this.Expression;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x0600289E RID: 10398 RVA: 0x00077400 File Offset: 0x00075600
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
			else if (this.Expression != null)
			{
				return new ValueTuple<bool, int, int>(true, 0, 0);
			}
			return new ValueTuple<bool, int, int>(false, 1, 0);
		}

		// Token: 0x17000BCC RID: 3020
		// (get) Token: 0x0600289F RID: 10399 RVA: 0x00077437 File Offset: 0x00075637
		[Nullable(2)]
		public override ITypeSymbol Type
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x17000BCD RID: 3021
		// (get) Token: 0x060028A0 RID: 10400 RVA: 0x0007743F File Offset: 0x0007563F
		[Nullable(2)]
		internal override ConstantValue OperationConstantValue
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000BCE RID: 3022
		// (get) Token: 0x060028A1 RID: 10401 RVA: 0x00077442 File Offset: 0x00075642
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.DeclarationExpression;
			}
		}

		// Token: 0x060028A2 RID: 10402 RVA: 0x00077446 File Offset: 0x00075646
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitDeclarationExpression(this);
		}

		// Token: 0x060028A3 RID: 10403 RVA: 0x0007744F File Offset: 0x0007564F
		[NullableContext(2)]
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitDeclarationExpression(this, argument);
		}
	}
}
