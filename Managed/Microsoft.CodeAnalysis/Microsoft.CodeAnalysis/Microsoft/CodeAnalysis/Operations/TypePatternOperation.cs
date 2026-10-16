using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000456 RID: 1110
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class TypePatternOperation : BasePatternOperation, ITypePatternOperation, IPatternOperation, IOperation
	{
		// Token: 0x06002A85 RID: 10885 RVA: 0x00079F04 File Offset: 0x00078104
		internal TypePatternOperation(ITypeSymbol matchedType, ITypeSymbol inputType, ITypeSymbol narrowedType, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, bool isImplicit)
			: base(inputType, narrowedType, semanticModel, syntax, isImplicit)
		{
			this.MatchedType = matchedType;
		}

		// Token: 0x17000CC0 RID: 3264
		// (get) Token: 0x06002A86 RID: 10886 RVA: 0x00079F1B File Offset: 0x0007811B
		public ITypeSymbol MatchedType { get; }

		// Token: 0x06002A87 RID: 10887 RVA: 0x00079F23 File Offset: 0x00078123
		protected override IOperation GetCurrent(int slot, int index)
		{
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x06002A88 RID: 10888 RVA: 0x00079F36 File Offset: 0x00078136
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			return new ValueTuple<bool, int, int>(false, int.MinValue, int.MinValue);
		}

		// Token: 0x17000CC1 RID: 3265
		// (get) Token: 0x06002A89 RID: 10889 RVA: 0x00079F48 File Offset: 0x00078148
		[Nullable(2)]
		public override ITypeSymbol Type
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000CC2 RID: 3266
		// (get) Token: 0x06002A8A RID: 10890 RVA: 0x00079F4B File Offset: 0x0007814B
		[Nullable(2)]
		internal override ConstantValue OperationConstantValue
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000CC3 RID: 3267
		// (get) Token: 0x06002A8B RID: 10891 RVA: 0x00079F4E File Offset: 0x0007814E
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.TypePattern;
			}
		}

		// Token: 0x06002A8C RID: 10892 RVA: 0x00079F52 File Offset: 0x00078152
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitTypePattern(this);
		}

		// Token: 0x06002A8D RID: 10893 RVA: 0x00079F5B File Offset: 0x0007815B
		[NullableContext(2)]
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitTypePattern(this, argument);
		}
	}
}
