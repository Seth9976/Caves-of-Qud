using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x0200043A RID: 1082
	[NullableContext(2)]
	[Nullable(0)]
	internal sealed class DeclarationPatternOperation : BasePatternOperation, IDeclarationPatternOperation, IPatternOperation, IOperation
	{
		// Token: 0x06002972 RID: 10610 RVA: 0x00078688 File Offset: 0x00076888
		[NullableContext(1)]
		internal DeclarationPatternOperation([Nullable(2)] ITypeSymbol matchedType, bool matchesNull, [Nullable(2)] ISymbol declaredSymbol, ITypeSymbol inputType, ITypeSymbol narrowedType, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, bool isImplicit)
			: base(inputType, narrowedType, semanticModel, syntax, isImplicit)
		{
			this.MatchedType = matchedType;
			this.MatchesNull = matchesNull;
			this.DeclaredSymbol = declaredSymbol;
		}

		// Token: 0x17000C3A RID: 3130
		// (get) Token: 0x06002973 RID: 10611 RVA: 0x000786AF File Offset: 0x000768AF
		public ITypeSymbol MatchedType { get; }

		// Token: 0x17000C3B RID: 3131
		// (get) Token: 0x06002974 RID: 10612 RVA: 0x000786B7 File Offset: 0x000768B7
		public bool MatchesNull { get; }

		// Token: 0x17000C3C RID: 3132
		// (get) Token: 0x06002975 RID: 10613 RVA: 0x000786BF File Offset: 0x000768BF
		public ISymbol DeclaredSymbol { get; }

		// Token: 0x06002976 RID: 10614 RVA: 0x000786C7 File Offset: 0x000768C7
		[NullableContext(1)]
		protected override IOperation GetCurrent(int slot, int index)
		{
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x06002977 RID: 10615 RVA: 0x000786DA File Offset: 0x000768DA
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			return new ValueTuple<bool, int, int>(false, int.MinValue, int.MinValue);
		}

		// Token: 0x17000C3D RID: 3133
		// (get) Token: 0x06002978 RID: 10616 RVA: 0x000786EC File Offset: 0x000768EC
		public override ITypeSymbol Type
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000C3E RID: 3134
		// (get) Token: 0x06002979 RID: 10617 RVA: 0x000786EF File Offset: 0x000768EF
		internal override ConstantValue OperationConstantValue
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000C3F RID: 3135
		// (get) Token: 0x0600297A RID: 10618 RVA: 0x000786F2 File Offset: 0x000768F2
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.DeclarationPattern;
			}
		}

		// Token: 0x0600297B RID: 10619 RVA: 0x000786F6 File Offset: 0x000768F6
		[NullableContext(1)]
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitDeclarationPattern(this);
		}

		// Token: 0x0600297C RID: 10620 RVA: 0x000786FF File Offset: 0x000768FF
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitDeclarationPattern(this, argument);
		}
	}
}
