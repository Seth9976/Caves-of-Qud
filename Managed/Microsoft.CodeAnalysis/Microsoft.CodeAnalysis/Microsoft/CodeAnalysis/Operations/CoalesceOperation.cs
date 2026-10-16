using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000402 RID: 1026
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class CoalesceOperation : Operation, ICoalesceOperation, IOperation
	{
		// Token: 0x06002767 RID: 10087 RVA: 0x000757F3 File Offset: 0x000739F3
		internal CoalesceOperation(IOperation value, IOperation whenNull, IConvertibleConversion valueConversion, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, [Nullable(2)] ITypeSymbol type, [Nullable(2)] ConstantValue constantValue, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.Value = Operation.SetParentOperation<IOperation>(value, this);
			this.WhenNull = Operation.SetParentOperation<IOperation>(whenNull, this);
			this.ValueConversionConvertible = valueConversion;
			this.OperationConstantValue = constantValue;
			this.Type = type;
		}

		// Token: 0x17000B33 RID: 2867
		// (get) Token: 0x06002768 RID: 10088 RVA: 0x00075832 File Offset: 0x00073A32
		public IOperation Value { get; }

		// Token: 0x17000B34 RID: 2868
		// (get) Token: 0x06002769 RID: 10089 RVA: 0x0007583A File Offset: 0x00073A3A
		public IOperation WhenNull { get; }

		// Token: 0x17000B35 RID: 2869
		// (get) Token: 0x0600276A RID: 10090 RVA: 0x00075842 File Offset: 0x00073A42
		internal IConvertibleConversion ValueConversionConvertible { get; }

		// Token: 0x17000B36 RID: 2870
		// (get) Token: 0x0600276B RID: 10091 RVA: 0x0007584A File Offset: 0x00073A4A
		public CommonConversion ValueConversion
		{
			get
			{
				return this.ValueConversionConvertible.ToCommonConversion();
			}
		}

		// Token: 0x0600276C RID: 10092 RVA: 0x00075858 File Offset: 0x00073A58
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot != 0)
			{
				if (slot == 1)
				{
					if (this.WhenNull != null)
					{
						return this.WhenNull;
					}
				}
			}
			else if (this.Value != null)
			{
				return this.Value;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x0600276D RID: 10093 RVA: 0x000758A8 File Offset: 0x00073AA8
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			switch (previousSlot)
			{
			case -1:
				if (this.Value != null)
				{
					return new ValueTuple<bool, int, int>(true, 0, 0);
				}
				break;
			case 0:
				break;
			case 1:
			case 2:
				goto IL_003C;
			default:
				throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(previousSlot, previousIndex));
			}
			if (this.WhenNull != null)
			{
				return new ValueTuple<bool, int, int>(true, 1, 0);
			}
			IL_003C:
			return new ValueTuple<bool, int, int>(false, 2, 0);
		}

		// Token: 0x17000B37 RID: 2871
		// (get) Token: 0x0600276E RID: 10094 RVA: 0x0007590B File Offset: 0x00073B0B
		[Nullable(2)]
		public override ITypeSymbol Type
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x17000B38 RID: 2872
		// (get) Token: 0x0600276F RID: 10095 RVA: 0x00075913 File Offset: 0x00073B13
		[Nullable(2)]
		internal override ConstantValue OperationConstantValue
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x17000B39 RID: 2873
		// (get) Token: 0x06002770 RID: 10096 RVA: 0x0007591B File Offset: 0x00073B1B
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.Coalesce;
			}
		}

		// Token: 0x06002771 RID: 10097 RVA: 0x0007591F File Offset: 0x00073B1F
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitCoalesce(this);
		}

		// Token: 0x06002772 RID: 10098 RVA: 0x00075928 File Offset: 0x00073B28
		[NullableContext(2)]
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitCoalesce(this, argument);
		}
	}
}
