using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200022E RID: 558
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public abstract class CSharpSyntaxVisitor
	{
		// Token: 0x06002DCC RID: 11724 RVA: 0x000EFAA9 File Offset: 0x000EDCA9
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public virtual void Visit(SyntaxNode node)
		{
			if (node != null)
			{
				((CSharpSyntaxNode)node).Accept(this);
			}
		}

		// Token: 0x06002DCD RID: 11725 RVA: 0x000EFABA File Offset: 0x000EDCBA
		public virtual void DefaultVisit(SyntaxNode node)
		{
		}

		// Token: 0x06002DCE RID: 11726 RVA: 0x000EFABC File Offset: 0x000EDCBC
		public virtual void VisitIdentifierName(IdentifierNameSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002DCF RID: 11727 RVA: 0x000EFAC5 File Offset: 0x000EDCC5
		public virtual void VisitQualifiedName(QualifiedNameSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002DD0 RID: 11728 RVA: 0x000EFACE File Offset: 0x000EDCCE
		public virtual void VisitGenericName(GenericNameSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002DD1 RID: 11729 RVA: 0x000EFAD7 File Offset: 0x000EDCD7
		public virtual void VisitTypeArgumentList(TypeArgumentListSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002DD2 RID: 11730 RVA: 0x000EFAE0 File Offset: 0x000EDCE0
		public virtual void VisitAliasQualifiedName(AliasQualifiedNameSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002DD3 RID: 11731 RVA: 0x000EFAE9 File Offset: 0x000EDCE9
		public virtual void VisitPredefinedType(PredefinedTypeSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002DD4 RID: 11732 RVA: 0x000EFAF2 File Offset: 0x000EDCF2
		public virtual void VisitArrayType(ArrayTypeSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002DD5 RID: 11733 RVA: 0x000EFAFB File Offset: 0x000EDCFB
		public virtual void VisitArrayRankSpecifier(ArrayRankSpecifierSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002DD6 RID: 11734 RVA: 0x000EFB04 File Offset: 0x000EDD04
		public virtual void VisitPointerType(PointerTypeSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002DD7 RID: 11735 RVA: 0x000EFB0D File Offset: 0x000EDD0D
		public virtual void VisitFunctionPointerType(FunctionPointerTypeSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002DD8 RID: 11736 RVA: 0x000EFB16 File Offset: 0x000EDD16
		public virtual void VisitFunctionPointerParameterList(FunctionPointerParameterListSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002DD9 RID: 11737 RVA: 0x000EFB1F File Offset: 0x000EDD1F
		public virtual void VisitFunctionPointerCallingConvention(FunctionPointerCallingConventionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002DDA RID: 11738 RVA: 0x000EFB28 File Offset: 0x000EDD28
		public virtual void VisitFunctionPointerUnmanagedCallingConventionList(FunctionPointerUnmanagedCallingConventionListSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002DDB RID: 11739 RVA: 0x000EFB31 File Offset: 0x000EDD31
		public virtual void VisitFunctionPointerUnmanagedCallingConvention(FunctionPointerUnmanagedCallingConventionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002DDC RID: 11740 RVA: 0x000EFB3A File Offset: 0x000EDD3A
		public virtual void VisitNullableType(NullableTypeSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002DDD RID: 11741 RVA: 0x000EFB43 File Offset: 0x000EDD43
		public virtual void VisitTupleType(TupleTypeSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002DDE RID: 11742 RVA: 0x000EFB4C File Offset: 0x000EDD4C
		public virtual void VisitTupleElement(TupleElementSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002DDF RID: 11743 RVA: 0x000EFB55 File Offset: 0x000EDD55
		public virtual void VisitOmittedTypeArgument(OmittedTypeArgumentSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002DE0 RID: 11744 RVA: 0x000EFB5E File Offset: 0x000EDD5E
		public virtual void VisitRefType(RefTypeSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002DE1 RID: 11745 RVA: 0x000EFB67 File Offset: 0x000EDD67
		public virtual void VisitParenthesizedExpression(ParenthesizedExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002DE2 RID: 11746 RVA: 0x000EFB70 File Offset: 0x000EDD70
		public virtual void VisitTupleExpression(TupleExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002DE3 RID: 11747 RVA: 0x000EFB79 File Offset: 0x000EDD79
		public virtual void VisitPrefixUnaryExpression(PrefixUnaryExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002DE4 RID: 11748 RVA: 0x000EFB82 File Offset: 0x000EDD82
		public virtual void VisitAwaitExpression(AwaitExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002DE5 RID: 11749 RVA: 0x000EFB8B File Offset: 0x000EDD8B
		public virtual void VisitPostfixUnaryExpression(PostfixUnaryExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002DE6 RID: 11750 RVA: 0x000EFB94 File Offset: 0x000EDD94
		public virtual void VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002DE7 RID: 11751 RVA: 0x000EFB9D File Offset: 0x000EDD9D
		public virtual void VisitConditionalAccessExpression(ConditionalAccessExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002DE8 RID: 11752 RVA: 0x000EFBA6 File Offset: 0x000EDDA6
		public virtual void VisitMemberBindingExpression(MemberBindingExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002DE9 RID: 11753 RVA: 0x000EFBAF File Offset: 0x000EDDAF
		public virtual void VisitElementBindingExpression(ElementBindingExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002DEA RID: 11754 RVA: 0x000EFBB8 File Offset: 0x000EDDB8
		public virtual void VisitRangeExpression(RangeExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002DEB RID: 11755 RVA: 0x000EFBC1 File Offset: 0x000EDDC1
		public virtual void VisitImplicitElementAccess(ImplicitElementAccessSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002DEC RID: 11756 RVA: 0x000EFBCA File Offset: 0x000EDDCA
		public virtual void VisitBinaryExpression(BinaryExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002DED RID: 11757 RVA: 0x000EFBD3 File Offset: 0x000EDDD3
		public virtual void VisitAssignmentExpression(AssignmentExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002DEE RID: 11758 RVA: 0x000EFBDC File Offset: 0x000EDDDC
		public virtual void VisitConditionalExpression(ConditionalExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002DEF RID: 11759 RVA: 0x000EFBE5 File Offset: 0x000EDDE5
		public virtual void VisitThisExpression(ThisExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002DF0 RID: 11760 RVA: 0x000EFBEE File Offset: 0x000EDDEE
		public virtual void VisitBaseExpression(BaseExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002DF1 RID: 11761 RVA: 0x000EFBF7 File Offset: 0x000EDDF7
		public virtual void VisitLiteralExpression(LiteralExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002DF2 RID: 11762 RVA: 0x000EFC00 File Offset: 0x000EDE00
		public virtual void VisitMakeRefExpression(MakeRefExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002DF3 RID: 11763 RVA: 0x000EFC09 File Offset: 0x000EDE09
		public virtual void VisitRefTypeExpression(RefTypeExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002DF4 RID: 11764 RVA: 0x000EFC12 File Offset: 0x000EDE12
		public virtual void VisitRefValueExpression(RefValueExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002DF5 RID: 11765 RVA: 0x000EFC1B File Offset: 0x000EDE1B
		public virtual void VisitCheckedExpression(CheckedExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002DF6 RID: 11766 RVA: 0x000EFC24 File Offset: 0x000EDE24
		public virtual void VisitDefaultExpression(DefaultExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002DF7 RID: 11767 RVA: 0x000EFC2D File Offset: 0x000EDE2D
		public virtual void VisitTypeOfExpression(TypeOfExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002DF8 RID: 11768 RVA: 0x000EFC36 File Offset: 0x000EDE36
		public virtual void VisitSizeOfExpression(SizeOfExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002DF9 RID: 11769 RVA: 0x000EFC3F File Offset: 0x000EDE3F
		public virtual void VisitInvocationExpression(InvocationExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002DFA RID: 11770 RVA: 0x000EFC48 File Offset: 0x000EDE48
		public virtual void VisitElementAccessExpression(ElementAccessExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002DFB RID: 11771 RVA: 0x000EFC51 File Offset: 0x000EDE51
		public virtual void VisitArgumentList(ArgumentListSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002DFC RID: 11772 RVA: 0x000EFC5A File Offset: 0x000EDE5A
		public virtual void VisitBracketedArgumentList(BracketedArgumentListSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002DFD RID: 11773 RVA: 0x000EFC63 File Offset: 0x000EDE63
		public virtual void VisitArgument(ArgumentSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002DFE RID: 11774 RVA: 0x000EFC6C File Offset: 0x000EDE6C
		public virtual void VisitNameColon(NameColonSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002DFF RID: 11775 RVA: 0x000EFC75 File Offset: 0x000EDE75
		public virtual void VisitDeclarationExpression(DeclarationExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E00 RID: 11776 RVA: 0x000EFC7E File Offset: 0x000EDE7E
		public virtual void VisitCastExpression(CastExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E01 RID: 11777 RVA: 0x000EFC87 File Offset: 0x000EDE87
		public virtual void VisitAnonymousMethodExpression(AnonymousMethodExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E02 RID: 11778 RVA: 0x000EFC90 File Offset: 0x000EDE90
		public virtual void VisitSimpleLambdaExpression(SimpleLambdaExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E03 RID: 11779 RVA: 0x000EFC99 File Offset: 0x000EDE99
		public virtual void VisitRefExpression(RefExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E04 RID: 11780 RVA: 0x000EFCA2 File Offset: 0x000EDEA2
		public virtual void VisitParenthesizedLambdaExpression(ParenthesizedLambdaExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E05 RID: 11781 RVA: 0x000EFCAB File Offset: 0x000EDEAB
		public virtual void VisitInitializerExpression(InitializerExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E06 RID: 11782 RVA: 0x000EFCB4 File Offset: 0x000EDEB4
		public virtual void VisitImplicitObjectCreationExpression(ImplicitObjectCreationExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E07 RID: 11783 RVA: 0x000EFCBD File Offset: 0x000EDEBD
		public virtual void VisitObjectCreationExpression(ObjectCreationExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E08 RID: 11784 RVA: 0x000EFCC6 File Offset: 0x000EDEC6
		public virtual void VisitWithExpression(WithExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E09 RID: 11785 RVA: 0x000EFCCF File Offset: 0x000EDECF
		public virtual void VisitAnonymousObjectMemberDeclarator(AnonymousObjectMemberDeclaratorSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E0A RID: 11786 RVA: 0x000EFCD8 File Offset: 0x000EDED8
		public virtual void VisitAnonymousObjectCreationExpression(AnonymousObjectCreationExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E0B RID: 11787 RVA: 0x000EFCE1 File Offset: 0x000EDEE1
		public virtual void VisitArrayCreationExpression(ArrayCreationExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E0C RID: 11788 RVA: 0x000EFCEA File Offset: 0x000EDEEA
		public virtual void VisitImplicitArrayCreationExpression(ImplicitArrayCreationExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E0D RID: 11789 RVA: 0x000EFCF3 File Offset: 0x000EDEF3
		public virtual void VisitStackAllocArrayCreationExpression(StackAllocArrayCreationExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E0E RID: 11790 RVA: 0x000EFCFC File Offset: 0x000EDEFC
		public virtual void VisitImplicitStackAllocArrayCreationExpression(ImplicitStackAllocArrayCreationExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E0F RID: 11791 RVA: 0x000EFD05 File Offset: 0x000EDF05
		public virtual void VisitQueryExpression(QueryExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E10 RID: 11792 RVA: 0x000EFD0E File Offset: 0x000EDF0E
		public virtual void VisitQueryBody(QueryBodySyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E11 RID: 11793 RVA: 0x000EFD17 File Offset: 0x000EDF17
		public virtual void VisitFromClause(FromClauseSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E12 RID: 11794 RVA: 0x000EFD20 File Offset: 0x000EDF20
		public virtual void VisitLetClause(LetClauseSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E13 RID: 11795 RVA: 0x000EFD29 File Offset: 0x000EDF29
		public virtual void VisitJoinClause(JoinClauseSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E14 RID: 11796 RVA: 0x000EFD32 File Offset: 0x000EDF32
		public virtual void VisitJoinIntoClause(JoinIntoClauseSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E15 RID: 11797 RVA: 0x000EFD3B File Offset: 0x000EDF3B
		public virtual void VisitWhereClause(WhereClauseSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E16 RID: 11798 RVA: 0x000EFD44 File Offset: 0x000EDF44
		public virtual void VisitOrderByClause(OrderByClauseSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E17 RID: 11799 RVA: 0x000EFD4D File Offset: 0x000EDF4D
		public virtual void VisitOrdering(OrderingSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E18 RID: 11800 RVA: 0x000EFD56 File Offset: 0x000EDF56
		public virtual void VisitSelectClause(SelectClauseSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E19 RID: 11801 RVA: 0x000EFD5F File Offset: 0x000EDF5F
		public virtual void VisitGroupClause(GroupClauseSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E1A RID: 11802 RVA: 0x000EFD68 File Offset: 0x000EDF68
		public virtual void VisitQueryContinuation(QueryContinuationSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E1B RID: 11803 RVA: 0x000EFD71 File Offset: 0x000EDF71
		public virtual void VisitOmittedArraySizeExpression(OmittedArraySizeExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E1C RID: 11804 RVA: 0x000EFD7A File Offset: 0x000EDF7A
		public virtual void VisitInterpolatedStringExpression(InterpolatedStringExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E1D RID: 11805 RVA: 0x000EFD83 File Offset: 0x000EDF83
		public virtual void VisitIsPatternExpression(IsPatternExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E1E RID: 11806 RVA: 0x000EFD8C File Offset: 0x000EDF8C
		public virtual void VisitThrowExpression(ThrowExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E1F RID: 11807 RVA: 0x000EFD95 File Offset: 0x000EDF95
		public virtual void VisitWhenClause(WhenClauseSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E20 RID: 11808 RVA: 0x000EFD9E File Offset: 0x000EDF9E
		public virtual void VisitDiscardPattern(DiscardPatternSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E21 RID: 11809 RVA: 0x000EFDA7 File Offset: 0x000EDFA7
		public virtual void VisitDeclarationPattern(DeclarationPatternSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E22 RID: 11810 RVA: 0x000EFDB0 File Offset: 0x000EDFB0
		public virtual void VisitVarPattern(VarPatternSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E23 RID: 11811 RVA: 0x000EFDB9 File Offset: 0x000EDFB9
		public virtual void VisitRecursivePattern(RecursivePatternSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E24 RID: 11812 RVA: 0x000EFDC2 File Offset: 0x000EDFC2
		public virtual void VisitPositionalPatternClause(PositionalPatternClauseSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E25 RID: 11813 RVA: 0x000EFDCB File Offset: 0x000EDFCB
		public virtual void VisitPropertyPatternClause(PropertyPatternClauseSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E26 RID: 11814 RVA: 0x000EFDD4 File Offset: 0x000EDFD4
		public virtual void VisitSubpattern(SubpatternSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E27 RID: 11815 RVA: 0x000EFDDD File Offset: 0x000EDFDD
		public virtual void VisitConstantPattern(ConstantPatternSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E28 RID: 11816 RVA: 0x000EFDE6 File Offset: 0x000EDFE6
		public virtual void VisitParenthesizedPattern(ParenthesizedPatternSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E29 RID: 11817 RVA: 0x000EFDEF File Offset: 0x000EDFEF
		public virtual void VisitRelationalPattern(RelationalPatternSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E2A RID: 11818 RVA: 0x000EFDF8 File Offset: 0x000EDFF8
		public virtual void VisitTypePattern(TypePatternSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E2B RID: 11819 RVA: 0x000EFE01 File Offset: 0x000EE001
		public virtual void VisitBinaryPattern(BinaryPatternSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E2C RID: 11820 RVA: 0x000EFE0A File Offset: 0x000EE00A
		public virtual void VisitUnaryPattern(UnaryPatternSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E2D RID: 11821 RVA: 0x000EFE13 File Offset: 0x000EE013
		public virtual void VisitInterpolatedStringText(InterpolatedStringTextSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E2E RID: 11822 RVA: 0x000EFE1C File Offset: 0x000EE01C
		public virtual void VisitInterpolation(InterpolationSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E2F RID: 11823 RVA: 0x000EFE25 File Offset: 0x000EE025
		public virtual void VisitInterpolationAlignmentClause(InterpolationAlignmentClauseSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E30 RID: 11824 RVA: 0x000EFE2E File Offset: 0x000EE02E
		public virtual void VisitInterpolationFormatClause(InterpolationFormatClauseSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E31 RID: 11825 RVA: 0x000EFE37 File Offset: 0x000EE037
		public virtual void VisitGlobalStatement(GlobalStatementSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E32 RID: 11826 RVA: 0x000EFE40 File Offset: 0x000EE040
		public virtual void VisitBlock(BlockSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E33 RID: 11827 RVA: 0x000EFE49 File Offset: 0x000EE049
		public virtual void VisitLocalFunctionStatement(LocalFunctionStatementSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E34 RID: 11828 RVA: 0x000EFE52 File Offset: 0x000EE052
		public virtual void VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E35 RID: 11829 RVA: 0x000EFE5B File Offset: 0x000EE05B
		public virtual void VisitVariableDeclaration(VariableDeclarationSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E36 RID: 11830 RVA: 0x000EFE64 File Offset: 0x000EE064
		public virtual void VisitVariableDeclarator(VariableDeclaratorSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E37 RID: 11831 RVA: 0x000EFE6D File Offset: 0x000EE06D
		public virtual void VisitEqualsValueClause(EqualsValueClauseSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E38 RID: 11832 RVA: 0x000EFE76 File Offset: 0x000EE076
		public virtual void VisitSingleVariableDesignation(SingleVariableDesignationSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E39 RID: 11833 RVA: 0x000EFE7F File Offset: 0x000EE07F
		public virtual void VisitDiscardDesignation(DiscardDesignationSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E3A RID: 11834 RVA: 0x000EFE88 File Offset: 0x000EE088
		public virtual void VisitParenthesizedVariableDesignation(ParenthesizedVariableDesignationSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E3B RID: 11835 RVA: 0x000EFE91 File Offset: 0x000EE091
		public virtual void VisitExpressionStatement(ExpressionStatementSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E3C RID: 11836 RVA: 0x000EFE9A File Offset: 0x000EE09A
		public virtual void VisitEmptyStatement(EmptyStatementSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E3D RID: 11837 RVA: 0x000EFEA3 File Offset: 0x000EE0A3
		public virtual void VisitLabeledStatement(LabeledStatementSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E3E RID: 11838 RVA: 0x000EFEAC File Offset: 0x000EE0AC
		public virtual void VisitGotoStatement(GotoStatementSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E3F RID: 11839 RVA: 0x000EFEB5 File Offset: 0x000EE0B5
		public virtual void VisitBreakStatement(BreakStatementSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E40 RID: 11840 RVA: 0x000EFEBE File Offset: 0x000EE0BE
		public virtual void VisitContinueStatement(ContinueStatementSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E41 RID: 11841 RVA: 0x000EFEC7 File Offset: 0x000EE0C7
		public virtual void VisitReturnStatement(ReturnStatementSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E42 RID: 11842 RVA: 0x000EFED0 File Offset: 0x000EE0D0
		public virtual void VisitThrowStatement(ThrowStatementSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E43 RID: 11843 RVA: 0x000EFED9 File Offset: 0x000EE0D9
		public virtual void VisitYieldStatement(YieldStatementSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E44 RID: 11844 RVA: 0x000EFEE2 File Offset: 0x000EE0E2
		public virtual void VisitWhileStatement(WhileStatementSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E45 RID: 11845 RVA: 0x000EFEEB File Offset: 0x000EE0EB
		public virtual void VisitDoStatement(DoStatementSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E46 RID: 11846 RVA: 0x000EFEF4 File Offset: 0x000EE0F4
		public virtual void VisitForStatement(ForStatementSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E47 RID: 11847 RVA: 0x000EFEFD File Offset: 0x000EE0FD
		public virtual void VisitForEachStatement(ForEachStatementSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E48 RID: 11848 RVA: 0x000EFF06 File Offset: 0x000EE106
		public virtual void VisitForEachVariableStatement(ForEachVariableStatementSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E49 RID: 11849 RVA: 0x000EFF0F File Offset: 0x000EE10F
		public virtual void VisitUsingStatement(UsingStatementSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E4A RID: 11850 RVA: 0x000EFF18 File Offset: 0x000EE118
		public virtual void VisitFixedStatement(FixedStatementSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E4B RID: 11851 RVA: 0x000EFF21 File Offset: 0x000EE121
		public virtual void VisitCheckedStatement(CheckedStatementSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E4C RID: 11852 RVA: 0x000EFF2A File Offset: 0x000EE12A
		public virtual void VisitUnsafeStatement(UnsafeStatementSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E4D RID: 11853 RVA: 0x000EFF33 File Offset: 0x000EE133
		public virtual void VisitLockStatement(LockStatementSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E4E RID: 11854 RVA: 0x000EFF3C File Offset: 0x000EE13C
		public virtual void VisitIfStatement(IfStatementSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E4F RID: 11855 RVA: 0x000EFF45 File Offset: 0x000EE145
		public virtual void VisitElseClause(ElseClauseSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E50 RID: 11856 RVA: 0x000EFF4E File Offset: 0x000EE14E
		public virtual void VisitSwitchStatement(SwitchStatementSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E51 RID: 11857 RVA: 0x000EFF57 File Offset: 0x000EE157
		public virtual void VisitSwitchSection(SwitchSectionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E52 RID: 11858 RVA: 0x000EFF60 File Offset: 0x000EE160
		public virtual void VisitCasePatternSwitchLabel(CasePatternSwitchLabelSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E53 RID: 11859 RVA: 0x000EFF69 File Offset: 0x000EE169
		public virtual void VisitCaseSwitchLabel(CaseSwitchLabelSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E54 RID: 11860 RVA: 0x000EFF72 File Offset: 0x000EE172
		public virtual void VisitDefaultSwitchLabel(DefaultSwitchLabelSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E55 RID: 11861 RVA: 0x000EFF7B File Offset: 0x000EE17B
		public virtual void VisitSwitchExpression(SwitchExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E56 RID: 11862 RVA: 0x000EFF84 File Offset: 0x000EE184
		public virtual void VisitSwitchExpressionArm(SwitchExpressionArmSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E57 RID: 11863 RVA: 0x000EFF8D File Offset: 0x000EE18D
		public virtual void VisitTryStatement(TryStatementSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E58 RID: 11864 RVA: 0x000EFF96 File Offset: 0x000EE196
		public virtual void VisitCatchClause(CatchClauseSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E59 RID: 11865 RVA: 0x000EFF9F File Offset: 0x000EE19F
		public virtual void VisitCatchDeclaration(CatchDeclarationSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E5A RID: 11866 RVA: 0x000EFFA8 File Offset: 0x000EE1A8
		public virtual void VisitCatchFilterClause(CatchFilterClauseSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E5B RID: 11867 RVA: 0x000EFFB1 File Offset: 0x000EE1B1
		public virtual void VisitFinallyClause(FinallyClauseSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E5C RID: 11868 RVA: 0x000EFFBA File Offset: 0x000EE1BA
		public virtual void VisitCompilationUnit(CompilationUnitSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E5D RID: 11869 RVA: 0x000EFFC3 File Offset: 0x000EE1C3
		public virtual void VisitExternAliasDirective(ExternAliasDirectiveSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E5E RID: 11870 RVA: 0x000EFFCC File Offset: 0x000EE1CC
		public virtual void VisitUsingDirective(UsingDirectiveSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E5F RID: 11871 RVA: 0x000EFFD5 File Offset: 0x000EE1D5
		public virtual void VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E60 RID: 11872 RVA: 0x000EFFDE File Offset: 0x000EE1DE
		public virtual void VisitAttributeList(AttributeListSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E61 RID: 11873 RVA: 0x000EFFE7 File Offset: 0x000EE1E7
		public virtual void VisitAttributeTargetSpecifier(AttributeTargetSpecifierSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E62 RID: 11874 RVA: 0x000EFFF0 File Offset: 0x000EE1F0
		public virtual void VisitAttribute(AttributeSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E63 RID: 11875 RVA: 0x000EFFF9 File Offset: 0x000EE1F9
		public virtual void VisitAttributeArgumentList(AttributeArgumentListSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E64 RID: 11876 RVA: 0x000F0002 File Offset: 0x000EE202
		public virtual void VisitAttributeArgument(AttributeArgumentSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E65 RID: 11877 RVA: 0x000F000B File Offset: 0x000EE20B
		public virtual void VisitNameEquals(NameEqualsSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E66 RID: 11878 RVA: 0x000F0014 File Offset: 0x000EE214
		public virtual void VisitTypeParameterList(TypeParameterListSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E67 RID: 11879 RVA: 0x000F001D File Offset: 0x000EE21D
		public virtual void VisitTypeParameter(TypeParameterSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E68 RID: 11880 RVA: 0x000F0026 File Offset: 0x000EE226
		public virtual void VisitClassDeclaration(ClassDeclarationSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E69 RID: 11881 RVA: 0x000F002F File Offset: 0x000EE22F
		public virtual void VisitStructDeclaration(StructDeclarationSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E6A RID: 11882 RVA: 0x000F0038 File Offset: 0x000EE238
		public virtual void VisitInterfaceDeclaration(InterfaceDeclarationSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E6B RID: 11883 RVA: 0x000F0041 File Offset: 0x000EE241
		public virtual void VisitRecordDeclaration(RecordDeclarationSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E6C RID: 11884 RVA: 0x000F004A File Offset: 0x000EE24A
		public virtual void VisitEnumDeclaration(EnumDeclarationSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E6D RID: 11885 RVA: 0x000F0053 File Offset: 0x000EE253
		public virtual void VisitDelegateDeclaration(DelegateDeclarationSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E6E RID: 11886 RVA: 0x000F005C File Offset: 0x000EE25C
		public virtual void VisitEnumMemberDeclaration(EnumMemberDeclarationSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E6F RID: 11887 RVA: 0x000F0065 File Offset: 0x000EE265
		public virtual void VisitBaseList(BaseListSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E70 RID: 11888 RVA: 0x000F006E File Offset: 0x000EE26E
		public virtual void VisitSimpleBaseType(SimpleBaseTypeSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E71 RID: 11889 RVA: 0x000F0077 File Offset: 0x000EE277
		public virtual void VisitPrimaryConstructorBaseType(PrimaryConstructorBaseTypeSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E72 RID: 11890 RVA: 0x000F0080 File Offset: 0x000EE280
		public virtual void VisitTypeParameterConstraintClause(TypeParameterConstraintClauseSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E73 RID: 11891 RVA: 0x000F0089 File Offset: 0x000EE289
		public virtual void VisitConstructorConstraint(ConstructorConstraintSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E74 RID: 11892 RVA: 0x000F0092 File Offset: 0x000EE292
		public virtual void VisitClassOrStructConstraint(ClassOrStructConstraintSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E75 RID: 11893 RVA: 0x000F009B File Offset: 0x000EE29B
		public virtual void VisitTypeConstraint(TypeConstraintSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E76 RID: 11894 RVA: 0x000F00A4 File Offset: 0x000EE2A4
		public virtual void VisitDefaultConstraint(DefaultConstraintSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E77 RID: 11895 RVA: 0x000F00AD File Offset: 0x000EE2AD
		public virtual void VisitFieldDeclaration(FieldDeclarationSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E78 RID: 11896 RVA: 0x000F00B6 File Offset: 0x000EE2B6
		public virtual void VisitEventFieldDeclaration(EventFieldDeclarationSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E79 RID: 11897 RVA: 0x000F00BF File Offset: 0x000EE2BF
		public virtual void VisitExplicitInterfaceSpecifier(ExplicitInterfaceSpecifierSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E7A RID: 11898 RVA: 0x000F00C8 File Offset: 0x000EE2C8
		public virtual void VisitMethodDeclaration(MethodDeclarationSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E7B RID: 11899 RVA: 0x000F00D1 File Offset: 0x000EE2D1
		public virtual void VisitOperatorDeclaration(OperatorDeclarationSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E7C RID: 11900 RVA: 0x000F00DA File Offset: 0x000EE2DA
		public virtual void VisitConversionOperatorDeclaration(ConversionOperatorDeclarationSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E7D RID: 11901 RVA: 0x000F00E3 File Offset: 0x000EE2E3
		public virtual void VisitConstructorDeclaration(ConstructorDeclarationSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E7E RID: 11902 RVA: 0x000F00EC File Offset: 0x000EE2EC
		public virtual void VisitConstructorInitializer(ConstructorInitializerSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E7F RID: 11903 RVA: 0x000F00F5 File Offset: 0x000EE2F5
		public virtual void VisitDestructorDeclaration(DestructorDeclarationSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E80 RID: 11904 RVA: 0x000F00FE File Offset: 0x000EE2FE
		public virtual void VisitPropertyDeclaration(PropertyDeclarationSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E81 RID: 11905 RVA: 0x000F0107 File Offset: 0x000EE307
		public virtual void VisitArrowExpressionClause(ArrowExpressionClauseSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E82 RID: 11906 RVA: 0x000F0110 File Offset: 0x000EE310
		public virtual void VisitEventDeclaration(EventDeclarationSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E83 RID: 11907 RVA: 0x000F0119 File Offset: 0x000EE319
		public virtual void VisitIndexerDeclaration(IndexerDeclarationSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E84 RID: 11908 RVA: 0x000F0122 File Offset: 0x000EE322
		public virtual void VisitAccessorList(AccessorListSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E85 RID: 11909 RVA: 0x000F012B File Offset: 0x000EE32B
		public virtual void VisitAccessorDeclaration(AccessorDeclarationSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E86 RID: 11910 RVA: 0x000F0134 File Offset: 0x000EE334
		public virtual void VisitParameterList(ParameterListSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E87 RID: 11911 RVA: 0x000F013D File Offset: 0x000EE33D
		public virtual void VisitBracketedParameterList(BracketedParameterListSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E88 RID: 11912 RVA: 0x000F0146 File Offset: 0x000EE346
		public virtual void VisitParameter(ParameterSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E89 RID: 11913 RVA: 0x000F014F File Offset: 0x000EE34F
		public virtual void VisitFunctionPointerParameter(FunctionPointerParameterSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E8A RID: 11914 RVA: 0x000F0158 File Offset: 0x000EE358
		public virtual void VisitIncompleteMember(IncompleteMemberSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E8B RID: 11915 RVA: 0x000F0161 File Offset: 0x000EE361
		public virtual void VisitSkippedTokensTrivia(SkippedTokensTriviaSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E8C RID: 11916 RVA: 0x000F016A File Offset: 0x000EE36A
		public virtual void VisitDocumentationCommentTrivia(DocumentationCommentTriviaSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E8D RID: 11917 RVA: 0x000F0173 File Offset: 0x000EE373
		public virtual void VisitTypeCref(TypeCrefSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E8E RID: 11918 RVA: 0x000F017C File Offset: 0x000EE37C
		public virtual void VisitQualifiedCref(QualifiedCrefSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E8F RID: 11919 RVA: 0x000F0185 File Offset: 0x000EE385
		public virtual void VisitNameMemberCref(NameMemberCrefSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E90 RID: 11920 RVA: 0x000F018E File Offset: 0x000EE38E
		public virtual void VisitIndexerMemberCref(IndexerMemberCrefSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E91 RID: 11921 RVA: 0x000F0197 File Offset: 0x000EE397
		public virtual void VisitOperatorMemberCref(OperatorMemberCrefSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E92 RID: 11922 RVA: 0x000F01A0 File Offset: 0x000EE3A0
		public virtual void VisitConversionOperatorMemberCref(ConversionOperatorMemberCrefSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E93 RID: 11923 RVA: 0x000F01A9 File Offset: 0x000EE3A9
		public virtual void VisitCrefParameterList(CrefParameterListSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E94 RID: 11924 RVA: 0x000F01B2 File Offset: 0x000EE3B2
		public virtual void VisitCrefBracketedParameterList(CrefBracketedParameterListSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E95 RID: 11925 RVA: 0x000F01BB File Offset: 0x000EE3BB
		public virtual void VisitCrefParameter(CrefParameterSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E96 RID: 11926 RVA: 0x000F01C4 File Offset: 0x000EE3C4
		public virtual void VisitXmlElement(XmlElementSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E97 RID: 11927 RVA: 0x000F01CD File Offset: 0x000EE3CD
		public virtual void VisitXmlElementStartTag(XmlElementStartTagSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E98 RID: 11928 RVA: 0x000F01D6 File Offset: 0x000EE3D6
		public virtual void VisitXmlElementEndTag(XmlElementEndTagSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E99 RID: 11929 RVA: 0x000F01DF File Offset: 0x000EE3DF
		public virtual void VisitXmlEmptyElement(XmlEmptyElementSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E9A RID: 11930 RVA: 0x000F01E8 File Offset: 0x000EE3E8
		public virtual void VisitXmlName(XmlNameSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E9B RID: 11931 RVA: 0x000F01F1 File Offset: 0x000EE3F1
		public virtual void VisitXmlPrefix(XmlPrefixSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E9C RID: 11932 RVA: 0x000F01FA File Offset: 0x000EE3FA
		public virtual void VisitXmlTextAttribute(XmlTextAttributeSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E9D RID: 11933 RVA: 0x000F0203 File Offset: 0x000EE403
		public virtual void VisitXmlCrefAttribute(XmlCrefAttributeSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E9E RID: 11934 RVA: 0x000F020C File Offset: 0x000EE40C
		public virtual void VisitXmlNameAttribute(XmlNameAttributeSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002E9F RID: 11935 RVA: 0x000F0215 File Offset: 0x000EE415
		public virtual void VisitXmlText(XmlTextSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002EA0 RID: 11936 RVA: 0x000F021E File Offset: 0x000EE41E
		public virtual void VisitXmlCDataSection(XmlCDataSectionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002EA1 RID: 11937 RVA: 0x000F0227 File Offset: 0x000EE427
		public virtual void VisitXmlProcessingInstruction(XmlProcessingInstructionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002EA2 RID: 11938 RVA: 0x000F0230 File Offset: 0x000EE430
		public virtual void VisitXmlComment(XmlCommentSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002EA3 RID: 11939 RVA: 0x000F0239 File Offset: 0x000EE439
		public virtual void VisitIfDirectiveTrivia(IfDirectiveTriviaSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002EA4 RID: 11940 RVA: 0x000F0242 File Offset: 0x000EE442
		public virtual void VisitElifDirectiveTrivia(ElifDirectiveTriviaSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002EA5 RID: 11941 RVA: 0x000F024B File Offset: 0x000EE44B
		public virtual void VisitElseDirectiveTrivia(ElseDirectiveTriviaSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002EA6 RID: 11942 RVA: 0x000F0254 File Offset: 0x000EE454
		public virtual void VisitEndIfDirectiveTrivia(EndIfDirectiveTriviaSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002EA7 RID: 11943 RVA: 0x000F025D File Offset: 0x000EE45D
		public virtual void VisitRegionDirectiveTrivia(RegionDirectiveTriviaSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002EA8 RID: 11944 RVA: 0x000F0266 File Offset: 0x000EE466
		public virtual void VisitEndRegionDirectiveTrivia(EndRegionDirectiveTriviaSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002EA9 RID: 11945 RVA: 0x000F026F File Offset: 0x000EE46F
		public virtual void VisitErrorDirectiveTrivia(ErrorDirectiveTriviaSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002EAA RID: 11946 RVA: 0x000F0278 File Offset: 0x000EE478
		public virtual void VisitWarningDirectiveTrivia(WarningDirectiveTriviaSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002EAB RID: 11947 RVA: 0x000F0281 File Offset: 0x000EE481
		public virtual void VisitBadDirectiveTrivia(BadDirectiveTriviaSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002EAC RID: 11948 RVA: 0x000F028A File Offset: 0x000EE48A
		public virtual void VisitDefineDirectiveTrivia(DefineDirectiveTriviaSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002EAD RID: 11949 RVA: 0x000F0293 File Offset: 0x000EE493
		public virtual void VisitUndefDirectiveTrivia(UndefDirectiveTriviaSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002EAE RID: 11950 RVA: 0x000F029C File Offset: 0x000EE49C
		public virtual void VisitLineDirectiveTrivia(LineDirectiveTriviaSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002EAF RID: 11951 RVA: 0x000F02A5 File Offset: 0x000EE4A5
		public virtual void VisitPragmaWarningDirectiveTrivia(PragmaWarningDirectiveTriviaSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002EB0 RID: 11952 RVA: 0x000F02AE File Offset: 0x000EE4AE
		public virtual void VisitPragmaChecksumDirectiveTrivia(PragmaChecksumDirectiveTriviaSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002EB1 RID: 11953 RVA: 0x000F02B7 File Offset: 0x000EE4B7
		public virtual void VisitReferenceDirectiveTrivia(ReferenceDirectiveTriviaSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002EB2 RID: 11954 RVA: 0x000F02C0 File Offset: 0x000EE4C0
		public virtual void VisitLoadDirectiveTrivia(LoadDirectiveTriviaSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002EB3 RID: 11955 RVA: 0x000F02C9 File Offset: 0x000EE4C9
		public virtual void VisitShebangDirectiveTrivia(ShebangDirectiveTriviaSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06002EB4 RID: 11956 RVA: 0x000F02D2 File Offset: 0x000EE4D2
		public virtual void VisitNullableDirectiveTrivia(NullableDirectiveTriviaSyntax node)
		{
			this.DefaultVisit(node);
		}
	}
}
