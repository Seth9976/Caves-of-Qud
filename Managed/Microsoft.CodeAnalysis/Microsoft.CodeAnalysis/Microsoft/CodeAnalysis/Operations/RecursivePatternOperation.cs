using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000449 RID: 1097
	[NullableContext(2)]
	[Nullable(0)]
	internal sealed class RecursivePatternOperation : BasePatternOperation, IRecursivePatternOperation, IPatternOperation, IOperation
	{
		// Token: 0x06002A00 RID: 10752 RVA: 0x00079260 File Offset: 0x00077460
		[NullableContext(1)]
		internal RecursivePatternOperation(ITypeSymbol matchedType, [Nullable(2)] ISymbol deconstructSymbol, [Nullable(new byte[] { 0, 1 })] ImmutableArray<IPatternOperation> deconstructionSubpatterns, [Nullable(new byte[] { 0, 1 })] ImmutableArray<IPropertySubpatternOperation> propertySubpatterns, [Nullable(2)] ISymbol declaredSymbol, ITypeSymbol inputType, ITypeSymbol narrowedType, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, bool isImplicit)
			: base(inputType, narrowedType, semanticModel, syntax, isImplicit)
		{
			this.MatchedType = matchedType;
			this.DeconstructSymbol = deconstructSymbol;
			this.DeconstructionSubpatterns = Operation.SetParentOperation<IPatternOperation>(deconstructionSubpatterns, this);
			this.PropertySubpatterns = Operation.SetParentOperation<IPropertySubpatternOperation>(propertySubpatterns, this);
			this.DeclaredSymbol = declaredSymbol;
		}

		// Token: 0x17000C7C RID: 3196
		// (get) Token: 0x06002A01 RID: 10753 RVA: 0x000792AE File Offset: 0x000774AE
		[Nullable(1)]
		public ITypeSymbol MatchedType
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x17000C7D RID: 3197
		// (get) Token: 0x06002A02 RID: 10754 RVA: 0x000792B6 File Offset: 0x000774B6
		public ISymbol DeconstructSymbol { get; }

		// Token: 0x17000C7E RID: 3198
		// (get) Token: 0x06002A03 RID: 10755 RVA: 0x000792BE File Offset: 0x000774BE
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<IPatternOperation> DeconstructionSubpatterns
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000C7F RID: 3199
		// (get) Token: 0x06002A04 RID: 10756 RVA: 0x000792C6 File Offset: 0x000774C6
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<IPropertySubpatternOperation> PropertySubpatterns
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000C80 RID: 3200
		// (get) Token: 0x06002A05 RID: 10757 RVA: 0x000792CE File Offset: 0x000774CE
		public ISymbol DeclaredSymbol { get; }

		// Token: 0x06002A06 RID: 10758 RVA: 0x000792D8 File Offset: 0x000774D8
		[NullableContext(1)]
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot != 0)
			{
				if (slot == 1)
				{
					if (index < this.PropertySubpatterns.Length)
					{
						return this.PropertySubpatterns[index];
					}
				}
			}
			else if (index < this.DeconstructionSubpatterns.Length)
			{
				return this.DeconstructionSubpatterns[index];
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x06002A07 RID: 10759 RVA: 0x0007934C File Offset: 0x0007754C
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			switch (previousSlot)
			{
			case -1:
				if (!this.DeconstructionSubpatterns.IsEmpty)
				{
					return new ValueTuple<bool, int, int>(true, 0, 0);
				}
				break;
			case 0:
				if (previousIndex + 1 < this.DeconstructionSubpatterns.Length)
				{
					return new ValueTuple<bool, int, int>(true, 0, previousIndex + 1);
				}
				break;
			case 1:
				if (previousIndex + 1 < this.PropertySubpatterns.Length)
				{
					return new ValueTuple<bool, int, int>(true, 1, previousIndex + 1);
				}
				goto IL_008A;
			case 2:
				goto IL_008A;
			default:
				throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(previousSlot, previousIndex));
			}
			if (!this.PropertySubpatterns.IsEmpty)
			{
				return new ValueTuple<bool, int, int>(true, 1, 0);
			}
			IL_008A:
			return new ValueTuple<bool, int, int>(false, 2, 0);
		}

		// Token: 0x17000C81 RID: 3201
		// (get) Token: 0x06002A08 RID: 10760 RVA: 0x000793FD File Offset: 0x000775FD
		public override ITypeSymbol Type
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000C82 RID: 3202
		// (get) Token: 0x06002A09 RID: 10761 RVA: 0x00079400 File Offset: 0x00077600
		internal override ConstantValue OperationConstantValue
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000C83 RID: 3203
		// (get) Token: 0x06002A0A RID: 10762 RVA: 0x00079403 File Offset: 0x00077603
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.RecursivePattern;
			}
		}

		// Token: 0x06002A0B RID: 10763 RVA: 0x00079407 File Offset: 0x00077607
		[NullableContext(1)]
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitRecursivePattern(this);
		}

		// Token: 0x06002A0C RID: 10764 RVA: 0x00079410 File Offset: 0x00077610
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitRecursivePattern(this, argument);
		}
	}
}
