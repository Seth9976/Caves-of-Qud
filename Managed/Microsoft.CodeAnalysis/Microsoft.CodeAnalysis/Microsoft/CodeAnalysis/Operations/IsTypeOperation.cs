using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000408 RID: 1032
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class IsTypeOperation : Operation, IIsTypeOperation, IOperation
	{
		// Token: 0x060027A4 RID: 10148 RVA: 0x00075DC3 File Offset: 0x00073FC3
		internal IsTypeOperation(IOperation valueOperand, ITypeSymbol typeOperand, bool isNegated, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, [Nullable(2)] ITypeSymbol type, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.ValueOperand = Operation.SetParentOperation<IOperation>(valueOperand, this);
			this.TypeOperand = typeOperand;
			this.IsNegated = isNegated;
			this.Type = type;
		}

		// Token: 0x17000B52 RID: 2898
		// (get) Token: 0x060027A5 RID: 10149 RVA: 0x00075DF4 File Offset: 0x00073FF4
		public IOperation ValueOperand { get; }

		// Token: 0x17000B53 RID: 2899
		// (get) Token: 0x060027A6 RID: 10150 RVA: 0x00075DFC File Offset: 0x00073FFC
		public ITypeSymbol TypeOperand { get; }

		// Token: 0x17000B54 RID: 2900
		// (get) Token: 0x060027A7 RID: 10151 RVA: 0x00075E04 File Offset: 0x00074004
		public bool IsNegated { get; }

		// Token: 0x060027A8 RID: 10152 RVA: 0x00075E0C File Offset: 0x0007400C
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot == 0 && this.ValueOperand != null)
			{
				return this.ValueOperand;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x060027A9 RID: 10153 RVA: 0x00075E40 File Offset: 0x00074040
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
			else if (this.ValueOperand != null)
			{
				return new ValueTuple<bool, int, int>(true, 0, 0);
			}
			return new ValueTuple<bool, int, int>(false, 1, 0);
		}

		// Token: 0x17000B55 RID: 2901
		// (get) Token: 0x060027AA RID: 10154 RVA: 0x00075E77 File Offset: 0x00074077
		[Nullable(2)]
		public override ITypeSymbol Type
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x17000B56 RID: 2902
		// (get) Token: 0x060027AB RID: 10155 RVA: 0x00075E7F File Offset: 0x0007407F
		[Nullable(2)]
		internal override ConstantValue OperationConstantValue
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000B57 RID: 2903
		// (get) Token: 0x060027AC RID: 10156 RVA: 0x00075E82 File Offset: 0x00074082
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.IsType;
			}
		}

		// Token: 0x060027AD RID: 10157 RVA: 0x00075E86 File Offset: 0x00074086
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitIsType(this);
		}

		// Token: 0x060027AE RID: 10158 RVA: 0x00075E8F File Offset: 0x0007408F
		[NullableContext(2)]
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitIsType(this, argument);
		}
	}
}
