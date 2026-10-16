using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000442 RID: 1090
	[NullableContext(2)]
	[Nullable(0)]
	internal sealed class IsNullOperation : Operation, IIsNullOperation, IOperation
	{
		// Token: 0x060029BB RID: 10683 RVA: 0x00078C3D File Offset: 0x00076E3D
		internal IsNullOperation([Nullable(1)] IOperation operand, SemanticModel semanticModel, [Nullable(1)] SyntaxNode syntax, ITypeSymbol type, ConstantValue constantValue, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.Operand = Operation.SetParentOperation<IOperation>(operand, this);
			this.OperationConstantValue = constantValue;
			this.Type = type;
		}

		// Token: 0x17000C5D RID: 3165
		// (get) Token: 0x060029BC RID: 10684 RVA: 0x00078C66 File Offset: 0x00076E66
		[Nullable(1)]
		public IOperation Operand
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x060029BD RID: 10685 RVA: 0x00078C70 File Offset: 0x00076E70
		[NullableContext(1)]
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot == 0 && this.Operand != null)
			{
				return this.Operand;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x060029BE RID: 10686 RVA: 0x00078CA4 File Offset: 0x00076EA4
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

		// Token: 0x17000C5E RID: 3166
		// (get) Token: 0x060029BF RID: 10687 RVA: 0x00078CDB File Offset: 0x00076EDB
		public override ITypeSymbol Type { get; }

		// Token: 0x17000C5F RID: 3167
		// (get) Token: 0x060029C0 RID: 10688 RVA: 0x00078CE3 File Offset: 0x00076EE3
		internal override ConstantValue OperationConstantValue { get; }

		// Token: 0x17000C60 RID: 3168
		// (get) Token: 0x060029C1 RID: 10689 RVA: 0x00078CEB File Offset: 0x00076EEB
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.IsNull;
			}
		}

		// Token: 0x060029C2 RID: 10690 RVA: 0x00078CEF File Offset: 0x00076EEF
		[NullableContext(1)]
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitIsNull(this);
		}

		// Token: 0x060029C3 RID: 10691 RVA: 0x00078CF8 File Offset: 0x00076EF8
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitIsNull(this, argument);
		}

		// Token: 0x060029C4 RID: 10692 RVA: 0x00078D02 File Offset: 0x00076F02
		[NullableContext(1)]
		public IsNullOperation(SyntaxNode syntax, IOperation operand, ITypeSymbol type, [Nullable(2)] ConstantValue constantValue)
			: this(operand, null, syntax, type, constantValue, true)
		{
		}
	}
}
