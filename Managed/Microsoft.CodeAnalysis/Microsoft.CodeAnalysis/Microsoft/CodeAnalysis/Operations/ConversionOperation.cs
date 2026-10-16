using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003F5 RID: 1013
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class ConversionOperation : Operation, IConversionOperation, IOperation
	{
		// Token: 0x060026D9 RID: 9945 RVA: 0x00074B28 File Offset: 0x00072D28
		internal ConversionOperation(IOperation operand, IConvertibleConversion conversion, bool isTryCast, bool isChecked, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, [Nullable(2)] ITypeSymbol type, [Nullable(2)] ConstantValue constantValue, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.Operand = Operation.SetParentOperation<IOperation>(operand, this);
			this.ConversionConvertible = conversion;
			this.IsTryCast = isTryCast;
			this.IsChecked = isChecked;
			this.OperationConstantValue = constantValue;
			this.Type = type;
		}

		// Token: 0x17000AE2 RID: 2786
		// (get) Token: 0x060026DA RID: 9946 RVA: 0x00074B74 File Offset: 0x00072D74
		public IOperation Operand { get; }

		// Token: 0x17000AE3 RID: 2787
		// (get) Token: 0x060026DB RID: 9947 RVA: 0x00074B7C File Offset: 0x00072D7C
		internal IConvertibleConversion ConversionConvertible { get; }

		// Token: 0x17000AE4 RID: 2788
		// (get) Token: 0x060026DC RID: 9948 RVA: 0x00074B84 File Offset: 0x00072D84
		public CommonConversion Conversion
		{
			get
			{
				return this.ConversionConvertible.ToCommonConversion();
			}
		}

		// Token: 0x17000AE5 RID: 2789
		// (get) Token: 0x060026DD RID: 9949 RVA: 0x00074B91 File Offset: 0x00072D91
		public bool IsTryCast { get; }

		// Token: 0x17000AE6 RID: 2790
		// (get) Token: 0x060026DE RID: 9950 RVA: 0x00074B99 File Offset: 0x00072D99
		public bool IsChecked { get; }

		// Token: 0x060026DF RID: 9951 RVA: 0x00074BA4 File Offset: 0x00072DA4
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot == 0 && this.Operand != null)
			{
				return this.Operand;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x060026E0 RID: 9952 RVA: 0x00074BD8 File Offset: 0x00072DD8
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

		// Token: 0x17000AE7 RID: 2791
		// (get) Token: 0x060026E1 RID: 9953 RVA: 0x00074C0F File Offset: 0x00072E0F
		[Nullable(2)]
		public override ITypeSymbol Type
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x17000AE8 RID: 2792
		// (get) Token: 0x060026E2 RID: 9954 RVA: 0x00074C17 File Offset: 0x00072E17
		[Nullable(2)]
		internal override ConstantValue OperationConstantValue
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x17000AE9 RID: 2793
		// (get) Token: 0x060026E3 RID: 9955 RVA: 0x00074C1F File Offset: 0x00072E1F
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.Conversion;
			}
		}

		// Token: 0x060026E4 RID: 9956 RVA: 0x00074C23 File Offset: 0x00072E23
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitConversion(this);
		}

		// Token: 0x060026E5 RID: 9957 RVA: 0x00074C2C File Offset: 0x00072E2C
		[NullableContext(2)]
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitConversion(this, argument);
		}

		// Token: 0x17000AEA RID: 2794
		// (get) Token: 0x060026E6 RID: 9958 RVA: 0x00074C38 File Offset: 0x00072E38
		[Nullable(2)]
		public IMethodSymbol OperatorMethod
		{
			[NullableContext(2)]
			get
			{
				return this.Conversion.MethodSymbol;
			}
		}
	}
}
