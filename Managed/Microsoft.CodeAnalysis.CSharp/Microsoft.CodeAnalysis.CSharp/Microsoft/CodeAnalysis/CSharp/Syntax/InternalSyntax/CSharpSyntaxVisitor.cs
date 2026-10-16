using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200036D RID: 877
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class CSharpSyntaxVisitor<[global::System.Runtime.CompilerServices.Nullable(2)] TResult>
	{
		// Token: 0x06004D45 RID: 19781 RVA: 0x00140184 File Offset: 0x0013E384
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public virtual TResult Visit(CSharpSyntaxNode node)
		{
			if (node == null)
			{
				return default(TResult);
			}
			return node.Accept<TResult>(this);
		}

		// Token: 0x06004D46 RID: 19782 RVA: 0x001401A5 File Offset: 0x0013E3A5
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public virtual TResult VisitToken(SyntaxToken token)
		{
			return this.DefaultVisit(token);
		}

		// Token: 0x06004D47 RID: 19783 RVA: 0x001401AE File Offset: 0x0013E3AE
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public virtual TResult VisitTrivia(SyntaxTrivia trivia)
		{
			return this.DefaultVisit(trivia);
		}

		// Token: 0x06004D48 RID: 19784 RVA: 0x001401B8 File Offset: 0x0013E3B8
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		protected virtual TResult DefaultVisit(CSharpSyntaxNode node)
		{
			return default(TResult);
		}

		// Token: 0x06004D49 RID: 19785 RVA: 0x001401CE File Offset: 0x0013E3CE
		public virtual TResult VisitIdentifierName(IdentifierNameSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D4A RID: 19786 RVA: 0x001401D7 File Offset: 0x0013E3D7
		public virtual TResult VisitQualifiedName(QualifiedNameSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D4B RID: 19787 RVA: 0x001401E0 File Offset: 0x0013E3E0
		public virtual TResult VisitGenericName(GenericNameSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D4C RID: 19788 RVA: 0x001401E9 File Offset: 0x0013E3E9
		public virtual TResult VisitTypeArgumentList(TypeArgumentListSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D4D RID: 19789 RVA: 0x001401F2 File Offset: 0x0013E3F2
		public virtual TResult VisitAliasQualifiedName(AliasQualifiedNameSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D4E RID: 19790 RVA: 0x001401FB File Offset: 0x0013E3FB
		public virtual TResult VisitPredefinedType(PredefinedTypeSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D4F RID: 19791 RVA: 0x00140204 File Offset: 0x0013E404
		public virtual TResult VisitArrayType(ArrayTypeSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D50 RID: 19792 RVA: 0x0014020D File Offset: 0x0013E40D
		public virtual TResult VisitArrayRankSpecifier(ArrayRankSpecifierSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D51 RID: 19793 RVA: 0x00140216 File Offset: 0x0013E416
		public virtual TResult VisitPointerType(PointerTypeSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D52 RID: 19794 RVA: 0x0014021F File Offset: 0x0013E41F
		public virtual TResult VisitFunctionPointerType(FunctionPointerTypeSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D53 RID: 19795 RVA: 0x00140228 File Offset: 0x0013E428
		public virtual TResult VisitFunctionPointerParameterList(FunctionPointerParameterListSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D54 RID: 19796 RVA: 0x00140231 File Offset: 0x0013E431
		public virtual TResult VisitFunctionPointerCallingConvention(FunctionPointerCallingConventionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D55 RID: 19797 RVA: 0x0014023A File Offset: 0x0013E43A
		public virtual TResult VisitFunctionPointerUnmanagedCallingConventionList(FunctionPointerUnmanagedCallingConventionListSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D56 RID: 19798 RVA: 0x00140243 File Offset: 0x0013E443
		public virtual TResult VisitFunctionPointerUnmanagedCallingConvention(FunctionPointerUnmanagedCallingConventionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D57 RID: 19799 RVA: 0x0014024C File Offset: 0x0013E44C
		public virtual TResult VisitNullableType(NullableTypeSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D58 RID: 19800 RVA: 0x00140255 File Offset: 0x0013E455
		public virtual TResult VisitTupleType(TupleTypeSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D59 RID: 19801 RVA: 0x0014025E File Offset: 0x0013E45E
		public virtual TResult VisitTupleElement(TupleElementSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D5A RID: 19802 RVA: 0x00140267 File Offset: 0x0013E467
		public virtual TResult VisitOmittedTypeArgument(OmittedTypeArgumentSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D5B RID: 19803 RVA: 0x00140270 File Offset: 0x0013E470
		public virtual TResult VisitRefType(RefTypeSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D5C RID: 19804 RVA: 0x00140279 File Offset: 0x0013E479
		public virtual TResult VisitParenthesizedExpression(ParenthesizedExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D5D RID: 19805 RVA: 0x00140282 File Offset: 0x0013E482
		public virtual TResult VisitTupleExpression(TupleExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D5E RID: 19806 RVA: 0x0014028B File Offset: 0x0013E48B
		public virtual TResult VisitPrefixUnaryExpression(PrefixUnaryExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D5F RID: 19807 RVA: 0x00140294 File Offset: 0x0013E494
		public virtual TResult VisitAwaitExpression(AwaitExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D60 RID: 19808 RVA: 0x0014029D File Offset: 0x0013E49D
		public virtual TResult VisitPostfixUnaryExpression(PostfixUnaryExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D61 RID: 19809 RVA: 0x001402A6 File Offset: 0x0013E4A6
		public virtual TResult VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D62 RID: 19810 RVA: 0x001402AF File Offset: 0x0013E4AF
		public virtual TResult VisitConditionalAccessExpression(ConditionalAccessExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D63 RID: 19811 RVA: 0x001402B8 File Offset: 0x0013E4B8
		public virtual TResult VisitMemberBindingExpression(MemberBindingExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D64 RID: 19812 RVA: 0x001402C1 File Offset: 0x0013E4C1
		public virtual TResult VisitElementBindingExpression(ElementBindingExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D65 RID: 19813 RVA: 0x001402CA File Offset: 0x0013E4CA
		public virtual TResult VisitRangeExpression(RangeExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D66 RID: 19814 RVA: 0x001402D3 File Offset: 0x0013E4D3
		public virtual TResult VisitImplicitElementAccess(ImplicitElementAccessSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D67 RID: 19815 RVA: 0x001402DC File Offset: 0x0013E4DC
		public virtual TResult VisitBinaryExpression(BinaryExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D68 RID: 19816 RVA: 0x001402E5 File Offset: 0x0013E4E5
		public virtual TResult VisitAssignmentExpression(AssignmentExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D69 RID: 19817 RVA: 0x001402EE File Offset: 0x0013E4EE
		public virtual TResult VisitConditionalExpression(ConditionalExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D6A RID: 19818 RVA: 0x001402F7 File Offset: 0x0013E4F7
		public virtual TResult VisitThisExpression(ThisExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D6B RID: 19819 RVA: 0x00140300 File Offset: 0x0013E500
		public virtual TResult VisitBaseExpression(BaseExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D6C RID: 19820 RVA: 0x00140309 File Offset: 0x0013E509
		public virtual TResult VisitLiteralExpression(LiteralExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D6D RID: 19821 RVA: 0x00140312 File Offset: 0x0013E512
		public virtual TResult VisitMakeRefExpression(MakeRefExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D6E RID: 19822 RVA: 0x0014031B File Offset: 0x0013E51B
		public virtual TResult VisitRefTypeExpression(RefTypeExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D6F RID: 19823 RVA: 0x00140324 File Offset: 0x0013E524
		public virtual TResult VisitRefValueExpression(RefValueExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D70 RID: 19824 RVA: 0x0014032D File Offset: 0x0013E52D
		public virtual TResult VisitCheckedExpression(CheckedExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D71 RID: 19825 RVA: 0x00140336 File Offset: 0x0013E536
		public virtual TResult VisitDefaultExpression(DefaultExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D72 RID: 19826 RVA: 0x0014033F File Offset: 0x0013E53F
		public virtual TResult VisitTypeOfExpression(TypeOfExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D73 RID: 19827 RVA: 0x00140348 File Offset: 0x0013E548
		public virtual TResult VisitSizeOfExpression(SizeOfExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D74 RID: 19828 RVA: 0x00140351 File Offset: 0x0013E551
		public virtual TResult VisitInvocationExpression(InvocationExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D75 RID: 19829 RVA: 0x0014035A File Offset: 0x0013E55A
		public virtual TResult VisitElementAccessExpression(ElementAccessExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D76 RID: 19830 RVA: 0x00140363 File Offset: 0x0013E563
		public virtual TResult VisitArgumentList(ArgumentListSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D77 RID: 19831 RVA: 0x0014036C File Offset: 0x0013E56C
		public virtual TResult VisitBracketedArgumentList(BracketedArgumentListSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D78 RID: 19832 RVA: 0x00140375 File Offset: 0x0013E575
		public virtual TResult VisitArgument(ArgumentSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D79 RID: 19833 RVA: 0x0014037E File Offset: 0x0013E57E
		public virtual TResult VisitNameColon(NameColonSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D7A RID: 19834 RVA: 0x00140387 File Offset: 0x0013E587
		public virtual TResult VisitDeclarationExpression(DeclarationExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D7B RID: 19835 RVA: 0x00140390 File Offset: 0x0013E590
		public virtual TResult VisitCastExpression(CastExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D7C RID: 19836 RVA: 0x00140399 File Offset: 0x0013E599
		public virtual TResult VisitAnonymousMethodExpression(AnonymousMethodExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D7D RID: 19837 RVA: 0x001403A2 File Offset: 0x0013E5A2
		public virtual TResult VisitSimpleLambdaExpression(SimpleLambdaExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D7E RID: 19838 RVA: 0x001403AB File Offset: 0x0013E5AB
		public virtual TResult VisitRefExpression(RefExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D7F RID: 19839 RVA: 0x001403B4 File Offset: 0x0013E5B4
		public virtual TResult VisitParenthesizedLambdaExpression(ParenthesizedLambdaExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D80 RID: 19840 RVA: 0x001403BD File Offset: 0x0013E5BD
		public virtual TResult VisitInitializerExpression(InitializerExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D81 RID: 19841 RVA: 0x001403C6 File Offset: 0x0013E5C6
		public virtual TResult VisitImplicitObjectCreationExpression(ImplicitObjectCreationExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D82 RID: 19842 RVA: 0x001403CF File Offset: 0x0013E5CF
		public virtual TResult VisitObjectCreationExpression(ObjectCreationExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D83 RID: 19843 RVA: 0x001403D8 File Offset: 0x0013E5D8
		public virtual TResult VisitWithExpression(WithExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D84 RID: 19844 RVA: 0x001403E1 File Offset: 0x0013E5E1
		public virtual TResult VisitAnonymousObjectMemberDeclarator(AnonymousObjectMemberDeclaratorSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D85 RID: 19845 RVA: 0x001403EA File Offset: 0x0013E5EA
		public virtual TResult VisitAnonymousObjectCreationExpression(AnonymousObjectCreationExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D86 RID: 19846 RVA: 0x001403F3 File Offset: 0x0013E5F3
		public virtual TResult VisitArrayCreationExpression(ArrayCreationExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D87 RID: 19847 RVA: 0x001403FC File Offset: 0x0013E5FC
		public virtual TResult VisitImplicitArrayCreationExpression(ImplicitArrayCreationExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D88 RID: 19848 RVA: 0x00140405 File Offset: 0x0013E605
		public virtual TResult VisitStackAllocArrayCreationExpression(StackAllocArrayCreationExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D89 RID: 19849 RVA: 0x0014040E File Offset: 0x0013E60E
		public virtual TResult VisitImplicitStackAllocArrayCreationExpression(ImplicitStackAllocArrayCreationExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D8A RID: 19850 RVA: 0x00140417 File Offset: 0x0013E617
		public virtual TResult VisitQueryExpression(QueryExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D8B RID: 19851 RVA: 0x00140420 File Offset: 0x0013E620
		public virtual TResult VisitQueryBody(QueryBodySyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D8C RID: 19852 RVA: 0x00140429 File Offset: 0x0013E629
		public virtual TResult VisitFromClause(FromClauseSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D8D RID: 19853 RVA: 0x00140432 File Offset: 0x0013E632
		public virtual TResult VisitLetClause(LetClauseSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D8E RID: 19854 RVA: 0x0014043B File Offset: 0x0013E63B
		public virtual TResult VisitJoinClause(JoinClauseSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D8F RID: 19855 RVA: 0x00140444 File Offset: 0x0013E644
		public virtual TResult VisitJoinIntoClause(JoinIntoClauseSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D90 RID: 19856 RVA: 0x0014044D File Offset: 0x0013E64D
		public virtual TResult VisitWhereClause(WhereClauseSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D91 RID: 19857 RVA: 0x00140456 File Offset: 0x0013E656
		public virtual TResult VisitOrderByClause(OrderByClauseSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D92 RID: 19858 RVA: 0x0014045F File Offset: 0x0013E65F
		public virtual TResult VisitOrdering(OrderingSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D93 RID: 19859 RVA: 0x00140468 File Offset: 0x0013E668
		public virtual TResult VisitSelectClause(SelectClauseSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D94 RID: 19860 RVA: 0x00140471 File Offset: 0x0013E671
		public virtual TResult VisitGroupClause(GroupClauseSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D95 RID: 19861 RVA: 0x0014047A File Offset: 0x0013E67A
		public virtual TResult VisitQueryContinuation(QueryContinuationSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D96 RID: 19862 RVA: 0x00140483 File Offset: 0x0013E683
		public virtual TResult VisitOmittedArraySizeExpression(OmittedArraySizeExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D97 RID: 19863 RVA: 0x0014048C File Offset: 0x0013E68C
		public virtual TResult VisitInterpolatedStringExpression(InterpolatedStringExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D98 RID: 19864 RVA: 0x00140495 File Offset: 0x0013E695
		public virtual TResult VisitIsPatternExpression(IsPatternExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D99 RID: 19865 RVA: 0x0014049E File Offset: 0x0013E69E
		public virtual TResult VisitThrowExpression(ThrowExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D9A RID: 19866 RVA: 0x001404A7 File Offset: 0x0013E6A7
		public virtual TResult VisitWhenClause(WhenClauseSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D9B RID: 19867 RVA: 0x001404B0 File Offset: 0x0013E6B0
		public virtual TResult VisitDiscardPattern(DiscardPatternSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D9C RID: 19868 RVA: 0x001404B9 File Offset: 0x0013E6B9
		public virtual TResult VisitDeclarationPattern(DeclarationPatternSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D9D RID: 19869 RVA: 0x001404C2 File Offset: 0x0013E6C2
		public virtual TResult VisitVarPattern(VarPatternSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D9E RID: 19870 RVA: 0x001404CB File Offset: 0x0013E6CB
		public virtual TResult VisitRecursivePattern(RecursivePatternSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004D9F RID: 19871 RVA: 0x001404D4 File Offset: 0x0013E6D4
		public virtual TResult VisitPositionalPatternClause(PositionalPatternClauseSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DA0 RID: 19872 RVA: 0x001404DD File Offset: 0x0013E6DD
		public virtual TResult VisitPropertyPatternClause(PropertyPatternClauseSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DA1 RID: 19873 RVA: 0x001404E6 File Offset: 0x0013E6E6
		public virtual TResult VisitSubpattern(SubpatternSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DA2 RID: 19874 RVA: 0x001404EF File Offset: 0x0013E6EF
		public virtual TResult VisitConstantPattern(ConstantPatternSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DA3 RID: 19875 RVA: 0x001404F8 File Offset: 0x0013E6F8
		public virtual TResult VisitParenthesizedPattern(ParenthesizedPatternSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DA4 RID: 19876 RVA: 0x00140501 File Offset: 0x0013E701
		public virtual TResult VisitRelationalPattern(RelationalPatternSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DA5 RID: 19877 RVA: 0x0014050A File Offset: 0x0013E70A
		public virtual TResult VisitTypePattern(TypePatternSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DA6 RID: 19878 RVA: 0x00140513 File Offset: 0x0013E713
		public virtual TResult VisitBinaryPattern(BinaryPatternSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DA7 RID: 19879 RVA: 0x0014051C File Offset: 0x0013E71C
		public virtual TResult VisitUnaryPattern(UnaryPatternSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DA8 RID: 19880 RVA: 0x00140525 File Offset: 0x0013E725
		public virtual TResult VisitInterpolatedStringText(InterpolatedStringTextSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DA9 RID: 19881 RVA: 0x0014052E File Offset: 0x0013E72E
		public virtual TResult VisitInterpolation(InterpolationSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DAA RID: 19882 RVA: 0x00140537 File Offset: 0x0013E737
		public virtual TResult VisitInterpolationAlignmentClause(InterpolationAlignmentClauseSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DAB RID: 19883 RVA: 0x00140540 File Offset: 0x0013E740
		public virtual TResult VisitInterpolationFormatClause(InterpolationFormatClauseSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DAC RID: 19884 RVA: 0x00140549 File Offset: 0x0013E749
		public virtual TResult VisitGlobalStatement(GlobalStatementSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DAD RID: 19885 RVA: 0x00140552 File Offset: 0x0013E752
		public virtual TResult VisitBlock(BlockSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DAE RID: 19886 RVA: 0x0014055B File Offset: 0x0013E75B
		public virtual TResult VisitLocalFunctionStatement(LocalFunctionStatementSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DAF RID: 19887 RVA: 0x00140564 File Offset: 0x0013E764
		public virtual TResult VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DB0 RID: 19888 RVA: 0x0014056D File Offset: 0x0013E76D
		public virtual TResult VisitVariableDeclaration(VariableDeclarationSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DB1 RID: 19889 RVA: 0x00140576 File Offset: 0x0013E776
		public virtual TResult VisitVariableDeclarator(VariableDeclaratorSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DB2 RID: 19890 RVA: 0x0014057F File Offset: 0x0013E77F
		public virtual TResult VisitEqualsValueClause(EqualsValueClauseSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DB3 RID: 19891 RVA: 0x00140588 File Offset: 0x0013E788
		public virtual TResult VisitSingleVariableDesignation(SingleVariableDesignationSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DB4 RID: 19892 RVA: 0x00140591 File Offset: 0x0013E791
		public virtual TResult VisitDiscardDesignation(DiscardDesignationSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DB5 RID: 19893 RVA: 0x0014059A File Offset: 0x0013E79A
		public virtual TResult VisitParenthesizedVariableDesignation(ParenthesizedVariableDesignationSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DB6 RID: 19894 RVA: 0x001405A3 File Offset: 0x0013E7A3
		public virtual TResult VisitExpressionStatement(ExpressionStatementSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DB7 RID: 19895 RVA: 0x001405AC File Offset: 0x0013E7AC
		public virtual TResult VisitEmptyStatement(EmptyStatementSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DB8 RID: 19896 RVA: 0x001405B5 File Offset: 0x0013E7B5
		public virtual TResult VisitLabeledStatement(LabeledStatementSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DB9 RID: 19897 RVA: 0x001405BE File Offset: 0x0013E7BE
		public virtual TResult VisitGotoStatement(GotoStatementSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DBA RID: 19898 RVA: 0x001405C7 File Offset: 0x0013E7C7
		public virtual TResult VisitBreakStatement(BreakStatementSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DBB RID: 19899 RVA: 0x001405D0 File Offset: 0x0013E7D0
		public virtual TResult VisitContinueStatement(ContinueStatementSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DBC RID: 19900 RVA: 0x001405D9 File Offset: 0x0013E7D9
		public virtual TResult VisitReturnStatement(ReturnStatementSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DBD RID: 19901 RVA: 0x001405E2 File Offset: 0x0013E7E2
		public virtual TResult VisitThrowStatement(ThrowStatementSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DBE RID: 19902 RVA: 0x001405EB File Offset: 0x0013E7EB
		public virtual TResult VisitYieldStatement(YieldStatementSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DBF RID: 19903 RVA: 0x001405F4 File Offset: 0x0013E7F4
		public virtual TResult VisitWhileStatement(WhileStatementSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DC0 RID: 19904 RVA: 0x001405FD File Offset: 0x0013E7FD
		public virtual TResult VisitDoStatement(DoStatementSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DC1 RID: 19905 RVA: 0x00140606 File Offset: 0x0013E806
		public virtual TResult VisitForStatement(ForStatementSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DC2 RID: 19906 RVA: 0x0014060F File Offset: 0x0013E80F
		public virtual TResult VisitForEachStatement(ForEachStatementSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DC3 RID: 19907 RVA: 0x00140618 File Offset: 0x0013E818
		public virtual TResult VisitForEachVariableStatement(ForEachVariableStatementSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DC4 RID: 19908 RVA: 0x00140621 File Offset: 0x0013E821
		public virtual TResult VisitUsingStatement(UsingStatementSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DC5 RID: 19909 RVA: 0x0014062A File Offset: 0x0013E82A
		public virtual TResult VisitFixedStatement(FixedStatementSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DC6 RID: 19910 RVA: 0x00140633 File Offset: 0x0013E833
		public virtual TResult VisitCheckedStatement(CheckedStatementSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DC7 RID: 19911 RVA: 0x0014063C File Offset: 0x0013E83C
		public virtual TResult VisitUnsafeStatement(UnsafeStatementSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DC8 RID: 19912 RVA: 0x00140645 File Offset: 0x0013E845
		public virtual TResult VisitLockStatement(LockStatementSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DC9 RID: 19913 RVA: 0x0014064E File Offset: 0x0013E84E
		public virtual TResult VisitIfStatement(IfStatementSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DCA RID: 19914 RVA: 0x00140657 File Offset: 0x0013E857
		public virtual TResult VisitElseClause(ElseClauseSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DCB RID: 19915 RVA: 0x00140660 File Offset: 0x0013E860
		public virtual TResult VisitSwitchStatement(SwitchStatementSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DCC RID: 19916 RVA: 0x00140669 File Offset: 0x0013E869
		public virtual TResult VisitSwitchSection(SwitchSectionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DCD RID: 19917 RVA: 0x00140672 File Offset: 0x0013E872
		public virtual TResult VisitCasePatternSwitchLabel(CasePatternSwitchLabelSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DCE RID: 19918 RVA: 0x0014067B File Offset: 0x0013E87B
		public virtual TResult VisitCaseSwitchLabel(CaseSwitchLabelSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DCF RID: 19919 RVA: 0x00140684 File Offset: 0x0013E884
		public virtual TResult VisitDefaultSwitchLabel(DefaultSwitchLabelSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DD0 RID: 19920 RVA: 0x0014068D File Offset: 0x0013E88D
		public virtual TResult VisitSwitchExpression(SwitchExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DD1 RID: 19921 RVA: 0x00140696 File Offset: 0x0013E896
		public virtual TResult VisitSwitchExpressionArm(SwitchExpressionArmSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DD2 RID: 19922 RVA: 0x0014069F File Offset: 0x0013E89F
		public virtual TResult VisitTryStatement(TryStatementSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DD3 RID: 19923 RVA: 0x001406A8 File Offset: 0x0013E8A8
		public virtual TResult VisitCatchClause(CatchClauseSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DD4 RID: 19924 RVA: 0x001406B1 File Offset: 0x0013E8B1
		public virtual TResult VisitCatchDeclaration(CatchDeclarationSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DD5 RID: 19925 RVA: 0x001406BA File Offset: 0x0013E8BA
		public virtual TResult VisitCatchFilterClause(CatchFilterClauseSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DD6 RID: 19926 RVA: 0x001406C3 File Offset: 0x0013E8C3
		public virtual TResult VisitFinallyClause(FinallyClauseSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DD7 RID: 19927 RVA: 0x001406CC File Offset: 0x0013E8CC
		public virtual TResult VisitCompilationUnit(CompilationUnitSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DD8 RID: 19928 RVA: 0x001406D5 File Offset: 0x0013E8D5
		public virtual TResult VisitExternAliasDirective(ExternAliasDirectiveSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DD9 RID: 19929 RVA: 0x001406DE File Offset: 0x0013E8DE
		public virtual TResult VisitUsingDirective(UsingDirectiveSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DDA RID: 19930 RVA: 0x001406E7 File Offset: 0x0013E8E7
		public virtual TResult VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DDB RID: 19931 RVA: 0x001406F0 File Offset: 0x0013E8F0
		public virtual TResult VisitAttributeList(AttributeListSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DDC RID: 19932 RVA: 0x001406F9 File Offset: 0x0013E8F9
		public virtual TResult VisitAttributeTargetSpecifier(AttributeTargetSpecifierSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DDD RID: 19933 RVA: 0x00140702 File Offset: 0x0013E902
		public virtual TResult VisitAttribute(AttributeSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DDE RID: 19934 RVA: 0x0014070B File Offset: 0x0013E90B
		public virtual TResult VisitAttributeArgumentList(AttributeArgumentListSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DDF RID: 19935 RVA: 0x00140714 File Offset: 0x0013E914
		public virtual TResult VisitAttributeArgument(AttributeArgumentSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DE0 RID: 19936 RVA: 0x0014071D File Offset: 0x0013E91D
		public virtual TResult VisitNameEquals(NameEqualsSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DE1 RID: 19937 RVA: 0x00140726 File Offset: 0x0013E926
		public virtual TResult VisitTypeParameterList(TypeParameterListSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DE2 RID: 19938 RVA: 0x0014072F File Offset: 0x0013E92F
		public virtual TResult VisitTypeParameter(TypeParameterSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DE3 RID: 19939 RVA: 0x00140738 File Offset: 0x0013E938
		public virtual TResult VisitClassDeclaration(ClassDeclarationSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DE4 RID: 19940 RVA: 0x00140741 File Offset: 0x0013E941
		public virtual TResult VisitStructDeclaration(StructDeclarationSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DE5 RID: 19941 RVA: 0x0014074A File Offset: 0x0013E94A
		public virtual TResult VisitInterfaceDeclaration(InterfaceDeclarationSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DE6 RID: 19942 RVA: 0x00140753 File Offset: 0x0013E953
		public virtual TResult VisitRecordDeclaration(RecordDeclarationSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DE7 RID: 19943 RVA: 0x0014075C File Offset: 0x0013E95C
		public virtual TResult VisitEnumDeclaration(EnumDeclarationSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DE8 RID: 19944 RVA: 0x00140765 File Offset: 0x0013E965
		public virtual TResult VisitDelegateDeclaration(DelegateDeclarationSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DE9 RID: 19945 RVA: 0x0014076E File Offset: 0x0013E96E
		public virtual TResult VisitEnumMemberDeclaration(EnumMemberDeclarationSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DEA RID: 19946 RVA: 0x00140777 File Offset: 0x0013E977
		public virtual TResult VisitBaseList(BaseListSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DEB RID: 19947 RVA: 0x00140780 File Offset: 0x0013E980
		public virtual TResult VisitSimpleBaseType(SimpleBaseTypeSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DEC RID: 19948 RVA: 0x00140789 File Offset: 0x0013E989
		public virtual TResult VisitPrimaryConstructorBaseType(PrimaryConstructorBaseTypeSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DED RID: 19949 RVA: 0x00140792 File Offset: 0x0013E992
		public virtual TResult VisitTypeParameterConstraintClause(TypeParameterConstraintClauseSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DEE RID: 19950 RVA: 0x0014079B File Offset: 0x0013E99B
		public virtual TResult VisitConstructorConstraint(ConstructorConstraintSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DEF RID: 19951 RVA: 0x001407A4 File Offset: 0x0013E9A4
		public virtual TResult VisitClassOrStructConstraint(ClassOrStructConstraintSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DF0 RID: 19952 RVA: 0x001407AD File Offset: 0x0013E9AD
		public virtual TResult VisitTypeConstraint(TypeConstraintSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DF1 RID: 19953 RVA: 0x001407B6 File Offset: 0x0013E9B6
		public virtual TResult VisitDefaultConstraint(DefaultConstraintSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DF2 RID: 19954 RVA: 0x001407BF File Offset: 0x0013E9BF
		public virtual TResult VisitFieldDeclaration(FieldDeclarationSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DF3 RID: 19955 RVA: 0x001407C8 File Offset: 0x0013E9C8
		public virtual TResult VisitEventFieldDeclaration(EventFieldDeclarationSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DF4 RID: 19956 RVA: 0x001407D1 File Offset: 0x0013E9D1
		public virtual TResult VisitExplicitInterfaceSpecifier(ExplicitInterfaceSpecifierSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DF5 RID: 19957 RVA: 0x001407DA File Offset: 0x0013E9DA
		public virtual TResult VisitMethodDeclaration(MethodDeclarationSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DF6 RID: 19958 RVA: 0x001407E3 File Offset: 0x0013E9E3
		public virtual TResult VisitOperatorDeclaration(OperatorDeclarationSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DF7 RID: 19959 RVA: 0x001407EC File Offset: 0x0013E9EC
		public virtual TResult VisitConversionOperatorDeclaration(ConversionOperatorDeclarationSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DF8 RID: 19960 RVA: 0x001407F5 File Offset: 0x0013E9F5
		public virtual TResult VisitConstructorDeclaration(ConstructorDeclarationSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DF9 RID: 19961 RVA: 0x001407FE File Offset: 0x0013E9FE
		public virtual TResult VisitConstructorInitializer(ConstructorInitializerSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DFA RID: 19962 RVA: 0x00140807 File Offset: 0x0013EA07
		public virtual TResult VisitDestructorDeclaration(DestructorDeclarationSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DFB RID: 19963 RVA: 0x00140810 File Offset: 0x0013EA10
		public virtual TResult VisitPropertyDeclaration(PropertyDeclarationSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DFC RID: 19964 RVA: 0x00140819 File Offset: 0x0013EA19
		public virtual TResult VisitArrowExpressionClause(ArrowExpressionClauseSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DFD RID: 19965 RVA: 0x00140822 File Offset: 0x0013EA22
		public virtual TResult VisitEventDeclaration(EventDeclarationSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DFE RID: 19966 RVA: 0x0014082B File Offset: 0x0013EA2B
		public virtual TResult VisitIndexerDeclaration(IndexerDeclarationSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004DFF RID: 19967 RVA: 0x00140834 File Offset: 0x0013EA34
		public virtual TResult VisitAccessorList(AccessorListSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004E00 RID: 19968 RVA: 0x0014083D File Offset: 0x0013EA3D
		public virtual TResult VisitAccessorDeclaration(AccessorDeclarationSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004E01 RID: 19969 RVA: 0x00140846 File Offset: 0x0013EA46
		public virtual TResult VisitParameterList(ParameterListSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004E02 RID: 19970 RVA: 0x0014084F File Offset: 0x0013EA4F
		public virtual TResult VisitBracketedParameterList(BracketedParameterListSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004E03 RID: 19971 RVA: 0x00140858 File Offset: 0x0013EA58
		public virtual TResult VisitParameter(ParameterSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004E04 RID: 19972 RVA: 0x00140861 File Offset: 0x0013EA61
		public virtual TResult VisitFunctionPointerParameter(FunctionPointerParameterSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004E05 RID: 19973 RVA: 0x0014086A File Offset: 0x0013EA6A
		public virtual TResult VisitIncompleteMember(IncompleteMemberSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004E06 RID: 19974 RVA: 0x00140873 File Offset: 0x0013EA73
		public virtual TResult VisitSkippedTokensTrivia(SkippedTokensTriviaSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004E07 RID: 19975 RVA: 0x0014087C File Offset: 0x0013EA7C
		public virtual TResult VisitDocumentationCommentTrivia(DocumentationCommentTriviaSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004E08 RID: 19976 RVA: 0x00140885 File Offset: 0x0013EA85
		public virtual TResult VisitTypeCref(TypeCrefSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004E09 RID: 19977 RVA: 0x0014088E File Offset: 0x0013EA8E
		public virtual TResult VisitQualifiedCref(QualifiedCrefSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004E0A RID: 19978 RVA: 0x00140897 File Offset: 0x0013EA97
		public virtual TResult VisitNameMemberCref(NameMemberCrefSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004E0B RID: 19979 RVA: 0x001408A0 File Offset: 0x0013EAA0
		public virtual TResult VisitIndexerMemberCref(IndexerMemberCrefSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004E0C RID: 19980 RVA: 0x001408A9 File Offset: 0x0013EAA9
		public virtual TResult VisitOperatorMemberCref(OperatorMemberCrefSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004E0D RID: 19981 RVA: 0x001408B2 File Offset: 0x0013EAB2
		public virtual TResult VisitConversionOperatorMemberCref(ConversionOperatorMemberCrefSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004E0E RID: 19982 RVA: 0x001408BB File Offset: 0x0013EABB
		public virtual TResult VisitCrefParameterList(CrefParameterListSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004E0F RID: 19983 RVA: 0x001408C4 File Offset: 0x0013EAC4
		public virtual TResult VisitCrefBracketedParameterList(CrefBracketedParameterListSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004E10 RID: 19984 RVA: 0x001408CD File Offset: 0x0013EACD
		public virtual TResult VisitCrefParameter(CrefParameterSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004E11 RID: 19985 RVA: 0x001408D6 File Offset: 0x0013EAD6
		public virtual TResult VisitXmlElement(XmlElementSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004E12 RID: 19986 RVA: 0x001408DF File Offset: 0x0013EADF
		public virtual TResult VisitXmlElementStartTag(XmlElementStartTagSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004E13 RID: 19987 RVA: 0x001408E8 File Offset: 0x0013EAE8
		public virtual TResult VisitXmlElementEndTag(XmlElementEndTagSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004E14 RID: 19988 RVA: 0x001408F1 File Offset: 0x0013EAF1
		public virtual TResult VisitXmlEmptyElement(XmlEmptyElementSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004E15 RID: 19989 RVA: 0x001408FA File Offset: 0x0013EAFA
		public virtual TResult VisitXmlName(XmlNameSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004E16 RID: 19990 RVA: 0x00140903 File Offset: 0x0013EB03
		public virtual TResult VisitXmlPrefix(XmlPrefixSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004E17 RID: 19991 RVA: 0x0014090C File Offset: 0x0013EB0C
		public virtual TResult VisitXmlTextAttribute(XmlTextAttributeSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004E18 RID: 19992 RVA: 0x00140915 File Offset: 0x0013EB15
		public virtual TResult VisitXmlCrefAttribute(XmlCrefAttributeSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004E19 RID: 19993 RVA: 0x0014091E File Offset: 0x0013EB1E
		public virtual TResult VisitXmlNameAttribute(XmlNameAttributeSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004E1A RID: 19994 RVA: 0x00140927 File Offset: 0x0013EB27
		public virtual TResult VisitXmlText(XmlTextSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004E1B RID: 19995 RVA: 0x00140930 File Offset: 0x0013EB30
		public virtual TResult VisitXmlCDataSection(XmlCDataSectionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004E1C RID: 19996 RVA: 0x00140939 File Offset: 0x0013EB39
		public virtual TResult VisitXmlProcessingInstruction(XmlProcessingInstructionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004E1D RID: 19997 RVA: 0x00140942 File Offset: 0x0013EB42
		public virtual TResult VisitXmlComment(XmlCommentSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004E1E RID: 19998 RVA: 0x0014094B File Offset: 0x0013EB4B
		public virtual TResult VisitIfDirectiveTrivia(IfDirectiveTriviaSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004E1F RID: 19999 RVA: 0x00140954 File Offset: 0x0013EB54
		public virtual TResult VisitElifDirectiveTrivia(ElifDirectiveTriviaSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004E20 RID: 20000 RVA: 0x0014095D File Offset: 0x0013EB5D
		public virtual TResult VisitElseDirectiveTrivia(ElseDirectiveTriviaSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004E21 RID: 20001 RVA: 0x00140966 File Offset: 0x0013EB66
		public virtual TResult VisitEndIfDirectiveTrivia(EndIfDirectiveTriviaSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004E22 RID: 20002 RVA: 0x0014096F File Offset: 0x0013EB6F
		public virtual TResult VisitRegionDirectiveTrivia(RegionDirectiveTriviaSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004E23 RID: 20003 RVA: 0x00140978 File Offset: 0x0013EB78
		public virtual TResult VisitEndRegionDirectiveTrivia(EndRegionDirectiveTriviaSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004E24 RID: 20004 RVA: 0x00140981 File Offset: 0x0013EB81
		public virtual TResult VisitErrorDirectiveTrivia(ErrorDirectiveTriviaSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004E25 RID: 20005 RVA: 0x0014098A File Offset: 0x0013EB8A
		public virtual TResult VisitWarningDirectiveTrivia(WarningDirectiveTriviaSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004E26 RID: 20006 RVA: 0x00140993 File Offset: 0x0013EB93
		public virtual TResult VisitBadDirectiveTrivia(BadDirectiveTriviaSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004E27 RID: 20007 RVA: 0x0014099C File Offset: 0x0013EB9C
		public virtual TResult VisitDefineDirectiveTrivia(DefineDirectiveTriviaSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004E28 RID: 20008 RVA: 0x001409A5 File Offset: 0x0013EBA5
		public virtual TResult VisitUndefDirectiveTrivia(UndefDirectiveTriviaSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004E29 RID: 20009 RVA: 0x001409AE File Offset: 0x0013EBAE
		public virtual TResult VisitLineDirectiveTrivia(LineDirectiveTriviaSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004E2A RID: 20010 RVA: 0x001409B7 File Offset: 0x0013EBB7
		public virtual TResult VisitPragmaWarningDirectiveTrivia(PragmaWarningDirectiveTriviaSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004E2B RID: 20011 RVA: 0x001409C0 File Offset: 0x0013EBC0
		public virtual TResult VisitPragmaChecksumDirectiveTrivia(PragmaChecksumDirectiveTriviaSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004E2C RID: 20012 RVA: 0x001409C9 File Offset: 0x0013EBC9
		public virtual TResult VisitReferenceDirectiveTrivia(ReferenceDirectiveTriviaSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004E2D RID: 20013 RVA: 0x001409D2 File Offset: 0x0013EBD2
		public virtual TResult VisitLoadDirectiveTrivia(LoadDirectiveTriviaSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004E2E RID: 20014 RVA: 0x001409DB File Offset: 0x0013EBDB
		public virtual TResult VisitShebangDirectiveTrivia(ShebangDirectiveTriviaSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06004E2F RID: 20015 RVA: 0x001409E4 File Offset: 0x0013EBE4
		public virtual TResult VisitNullableDirectiveTrivia(NullableDirectiveTriviaSyntax node)
		{
			return this.DefaultVisit(node);
		}
	}
}
