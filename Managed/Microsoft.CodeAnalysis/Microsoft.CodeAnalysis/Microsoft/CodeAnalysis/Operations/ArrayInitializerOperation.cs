using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000429 RID: 1065
	[NullableContext(2)]
	[Nullable(0)]
	internal sealed class ArrayInitializerOperation : Operation, IArrayInitializerOperation, IOperation
	{
		// Token: 0x060028D2 RID: 10450 RVA: 0x00077774 File Offset: 0x00075974
		[NullableContext(1)]
		internal ArrayInitializerOperation([Nullable(new byte[] { 0, 1 })] ImmutableArray<IOperation> elementValues, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.ElementValues = Operation.SetParentOperation<IOperation>(elementValues, this);
		}

		// Token: 0x17000BE3 RID: 3043
		// (get) Token: 0x060028D3 RID: 10451 RVA: 0x0007778D File Offset: 0x0007598D
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<IOperation> ElementValues
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x060028D4 RID: 10452 RVA: 0x00077798 File Offset: 0x00075998
		[NullableContext(1)]
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot == 0 && index < this.ElementValues.Length)
			{
				return this.ElementValues[index];
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x060028D5 RID: 10453 RVA: 0x000777E0 File Offset: 0x000759E0
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			switch (previousSlot)
			{
			case -1:
				if (!this.ElementValues.IsEmpty)
				{
					return new ValueTuple<bool, int, int>(true, 0, 0);
				}
				break;
			case 0:
				if (previousIndex + 1 < this.ElementValues.Length)
				{
					return new ValueTuple<bool, int, int>(true, 0, previousIndex + 1);
				}
				break;
			case 1:
				break;
			default:
				throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(previousSlot, previousIndex));
			}
			return new ValueTuple<bool, int, int>(false, 1, 0);
		}

		// Token: 0x17000BE4 RID: 3044
		// (get) Token: 0x060028D6 RID: 10454 RVA: 0x00077856 File Offset: 0x00075A56
		public override ITypeSymbol Type
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000BE5 RID: 3045
		// (get) Token: 0x060028D7 RID: 10455 RVA: 0x00077859 File Offset: 0x00075A59
		internal override ConstantValue OperationConstantValue
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000BE6 RID: 3046
		// (get) Token: 0x060028D8 RID: 10456 RVA: 0x0007785C File Offset: 0x00075A5C
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.ArrayInitializer;
			}
		}

		// Token: 0x060028D9 RID: 10457 RVA: 0x00077860 File Offset: 0x00075A60
		[NullableContext(1)]
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitArrayInitializer(this);
		}

		// Token: 0x060028DA RID: 10458 RVA: 0x00077869 File Offset: 0x00075A69
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitArrayInitializer(this, argument);
		}
	}
}
