using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003F7 RID: 1015
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class ArrayElementReferenceOperation : Operation, IArrayElementReferenceOperation, IOperation
	{
		// Token: 0x060026F3 RID: 9971 RVA: 0x00074DC1 File Offset: 0x00072FC1
		internal ArrayElementReferenceOperation(IOperation arrayReference, [Nullable(new byte[] { 0, 1 })] ImmutableArray<IOperation> indices, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, [Nullable(2)] ITypeSymbol type, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.ArrayReference = Operation.SetParentOperation<IOperation>(arrayReference, this);
			this.Indices = Operation.SetParentOperation<IOperation>(indices, this);
			this.Type = type;
		}

		// Token: 0x17000AF2 RID: 2802
		// (get) Token: 0x060026F4 RID: 9972 RVA: 0x00074DF0 File Offset: 0x00072FF0
		public IOperation ArrayReference { get; }

		// Token: 0x17000AF3 RID: 2803
		// (get) Token: 0x060026F5 RID: 9973 RVA: 0x00074DF8 File Offset: 0x00072FF8
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<IOperation> Indices
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x060026F6 RID: 9974 RVA: 0x00074E00 File Offset: 0x00073000
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot != 0)
			{
				if (slot == 1)
				{
					if (index < this.Indices.Length)
					{
						return this.Indices[index];
					}
				}
			}
			else if (this.ArrayReference != null)
			{
				return this.ArrayReference;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x060026F7 RID: 9975 RVA: 0x00074E60 File Offset: 0x00073060
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			switch (previousSlot)
			{
			case -1:
				if (this.ArrayReference != null)
				{
					return new ValueTuple<bool, int, int>(true, 0, 0);
				}
				break;
			case 0:
				break;
			case 1:
				if (previousIndex + 1 < this.Indices.Length)
				{
					return new ValueTuple<bool, int, int>(true, 1, previousIndex + 1);
				}
				goto IL_0064;
			case 2:
				goto IL_0064;
			default:
				throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(previousSlot, previousIndex));
			}
			if (!this.Indices.IsEmpty)
			{
				return new ValueTuple<bool, int, int>(true, 1, 0);
			}
			IL_0064:
			return new ValueTuple<bool, int, int>(false, 2, 0);
		}

		// Token: 0x17000AF4 RID: 2804
		// (get) Token: 0x060026F8 RID: 9976 RVA: 0x00074EEB File Offset: 0x000730EB
		[Nullable(2)]
		public override ITypeSymbol Type
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x17000AF5 RID: 2805
		// (get) Token: 0x060026F9 RID: 9977 RVA: 0x00074EF3 File Offset: 0x000730F3
		[Nullable(2)]
		internal override ConstantValue OperationConstantValue
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000AF6 RID: 2806
		// (get) Token: 0x060026FA RID: 9978 RVA: 0x00074EF6 File Offset: 0x000730F6
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.ArrayElementReference;
			}
		}

		// Token: 0x060026FB RID: 9979 RVA: 0x00074EFA File Offset: 0x000730FA
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitArrayElementReference(this);
		}

		// Token: 0x060026FC RID: 9980 RVA: 0x00074F03 File Offset: 0x00073103
		[NullableContext(2)]
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitArrayElementReference(this, argument);
		}
	}
}
