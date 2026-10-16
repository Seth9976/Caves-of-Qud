using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000406 RID: 1030
	[NullableContext(2)]
	[Nullable(0)]
	internal sealed class ArrayCreationOperation : Operation, IArrayCreationOperation, IOperation
	{
		// Token: 0x06002791 RID: 10129 RVA: 0x00075C0D File Offset: 0x00073E0D
		internal ArrayCreationOperation([Nullable(new byte[] { 0, 1 })] ImmutableArray<IOperation> dimensionSizes, IArrayInitializerOperation initializer, SemanticModel semanticModel, [Nullable(1)] SyntaxNode syntax, ITypeSymbol type, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.DimensionSizes = Operation.SetParentOperation<IOperation>(dimensionSizes, this);
			this.Initializer = Operation.SetParentOperation<IArrayInitializerOperation>(initializer, this);
			this.Type = type;
		}

		// Token: 0x17000B49 RID: 2889
		// (get) Token: 0x06002792 RID: 10130 RVA: 0x00075C3C File Offset: 0x00073E3C
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<IOperation> DimensionSizes
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000B4A RID: 2890
		// (get) Token: 0x06002793 RID: 10131 RVA: 0x00075C44 File Offset: 0x00073E44
		public IArrayInitializerOperation Initializer { get; }

		// Token: 0x06002794 RID: 10132 RVA: 0x00075C4C File Offset: 0x00073E4C
		[NullableContext(1)]
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot != 0)
			{
				if (slot == 1)
				{
					if (this.Initializer != null)
					{
						return this.Initializer;
					}
				}
			}
			else if (index < this.DimensionSizes.Length)
			{
				return this.DimensionSizes[index];
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x06002795 RID: 10133 RVA: 0x00075CAC File Offset: 0x00073EAC
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			switch (previousSlot)
			{
			case -1:
				if (!this.DimensionSizes.IsEmpty)
				{
					return new ValueTuple<bool, int, int>(true, 0, 0);
				}
				break;
			case 0:
				if (previousIndex + 1 < this.DimensionSizes.Length)
				{
					return new ValueTuple<bool, int, int>(true, 0, previousIndex + 1);
				}
				break;
			case 1:
			case 2:
				IL_0064:
				return new ValueTuple<bool, int, int>(false, 2, 0);
			default:
				throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(previousSlot, previousIndex));
			}
			if (this.Initializer != null)
			{
				return new ValueTuple<bool, int, int>(true, 1, 0);
			}
			goto IL_0064;
		}

		// Token: 0x17000B4B RID: 2891
		// (get) Token: 0x06002796 RID: 10134 RVA: 0x00075D37 File Offset: 0x00073F37
		public override ITypeSymbol Type { get; }

		// Token: 0x17000B4C RID: 2892
		// (get) Token: 0x06002797 RID: 10135 RVA: 0x00075D3F File Offset: 0x00073F3F
		internal override ConstantValue OperationConstantValue
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000B4D RID: 2893
		// (get) Token: 0x06002798 RID: 10136 RVA: 0x00075D42 File Offset: 0x00073F42
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.ArrayCreation;
			}
		}

		// Token: 0x06002799 RID: 10137 RVA: 0x00075D46 File Offset: 0x00073F46
		[NullableContext(1)]
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitArrayCreation(this);
		}

		// Token: 0x0600279A RID: 10138 RVA: 0x00075D4F File Offset: 0x00073F4F
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitArrayCreation(this, argument);
		}
	}
}
