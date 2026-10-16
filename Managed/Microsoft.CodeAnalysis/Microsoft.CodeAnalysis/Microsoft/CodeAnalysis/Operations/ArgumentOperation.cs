using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x0200042C RID: 1068
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class ArgumentOperation : Operation, IArgumentOperation, IOperation
	{
		// Token: 0x060028F1 RID: 10481 RVA: 0x00077B8A File Offset: 0x00075D8A
		internal ArgumentOperation(ArgumentKind argumentKind, [Nullable(2)] IParameterSymbol parameter, IOperation value, IConvertibleConversion inConversion, IConvertibleConversion outConversion, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.ArgumentKind = argumentKind;
			this.Parameter = parameter;
			this.Value = Operation.SetParentOperation<IOperation>(value, this);
			this.InConversionConvertible = inConversion;
			this.OutConversionConvertible = outConversion;
		}

		// Token: 0x17000BF3 RID: 3059
		// (get) Token: 0x060028F2 RID: 10482 RVA: 0x00077BC3 File Offset: 0x00075DC3
		public ArgumentKind ArgumentKind { get; }

		// Token: 0x17000BF4 RID: 3060
		// (get) Token: 0x060028F3 RID: 10483 RVA: 0x00077BCB File Offset: 0x00075DCB
		[Nullable(2)]
		public IParameterSymbol Parameter
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x17000BF5 RID: 3061
		// (get) Token: 0x060028F4 RID: 10484 RVA: 0x00077BD3 File Offset: 0x00075DD3
		public IOperation Value { get; }

		// Token: 0x17000BF6 RID: 3062
		// (get) Token: 0x060028F5 RID: 10485 RVA: 0x00077BDB File Offset: 0x00075DDB
		internal IConvertibleConversion InConversionConvertible { get; }

		// Token: 0x17000BF7 RID: 3063
		// (get) Token: 0x060028F6 RID: 10486 RVA: 0x00077BE3 File Offset: 0x00075DE3
		public CommonConversion InConversion
		{
			get
			{
				return this.InConversionConvertible.ToCommonConversion();
			}
		}

		// Token: 0x17000BF8 RID: 3064
		// (get) Token: 0x060028F7 RID: 10487 RVA: 0x00077BF0 File Offset: 0x00075DF0
		internal IConvertibleConversion OutConversionConvertible { get; }

		// Token: 0x17000BF9 RID: 3065
		// (get) Token: 0x060028F8 RID: 10488 RVA: 0x00077BF8 File Offset: 0x00075DF8
		public CommonConversion OutConversion
		{
			get
			{
				return this.OutConversionConvertible.ToCommonConversion();
			}
		}

		// Token: 0x060028F9 RID: 10489 RVA: 0x00077C08 File Offset: 0x00075E08
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot == 0 && this.Value != null)
			{
				return this.Value;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x060028FA RID: 10490 RVA: 0x00077C3C File Offset: 0x00075E3C
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
			else if (this.Value != null)
			{
				return new ValueTuple<bool, int, int>(true, 0, 0);
			}
			return new ValueTuple<bool, int, int>(false, 1, 0);
		}

		// Token: 0x17000BFA RID: 3066
		// (get) Token: 0x060028FB RID: 10491 RVA: 0x00077C73 File Offset: 0x00075E73
		[Nullable(2)]
		public override ITypeSymbol Type
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000BFB RID: 3067
		// (get) Token: 0x060028FC RID: 10492 RVA: 0x00077C76 File Offset: 0x00075E76
		[Nullable(2)]
		internal override ConstantValue OperationConstantValue
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000BFC RID: 3068
		// (get) Token: 0x060028FD RID: 10493 RVA: 0x00077C79 File Offset: 0x00075E79
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.Argument;
			}
		}

		// Token: 0x060028FE RID: 10494 RVA: 0x00077C7D File Offset: 0x00075E7D
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitArgument(this);
		}

		// Token: 0x060028FF RID: 10495 RVA: 0x00077C86 File Offset: 0x00075E86
		[NullableContext(2)]
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitArgument(this, argument);
		}
	}
}
