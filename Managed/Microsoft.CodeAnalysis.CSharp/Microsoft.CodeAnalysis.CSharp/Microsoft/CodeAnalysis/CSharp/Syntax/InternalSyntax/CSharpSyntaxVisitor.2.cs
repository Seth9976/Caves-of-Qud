using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200036E RID: 878
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class CSharpSyntaxVisitor
	{
		// Token: 0x06004E31 RID: 20017 RVA: 0x001409F5 File Offset: 0x0013EBF5
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public virtual void Visit(CSharpSyntaxNode node)
		{
			if (node == null)
			{
				return;
			}
			node.Accept(this);
		}

		// Token: 0x06004E32 RID: 20018 RVA: 0x00140A02 File Offset: 0x0013EC02
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public virtual void VisitToken(SyntaxToken token)
		{
			this.DefaultVisit(token);
		}

		// Token: 0x06004E33 RID: 20019 RVA: 0x00140A0B File Offset: 0x0013EC0B
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public virtual void VisitTrivia(SyntaxTrivia trivia)
		{
			this.DefaultVisit(trivia);
		}

		// Token: 0x06004E34 RID: 20020 RVA: 0x00140A14 File Offset: 0x0013EC14
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public virtual void DefaultVisit(CSharpSyntaxNode node)
		{
		}

		// Token: 0x06004E35 RID: 20021 RVA: 0x00140A16 File Offset: 0x0013EC16
		public virtual void VisitIdentifierName(IdentifierNameSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E36 RID: 20022 RVA: 0x00140A1F File Offset: 0x0013EC1F
		public virtual void VisitQualifiedName(QualifiedNameSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E37 RID: 20023 RVA: 0x00140A28 File Offset: 0x0013EC28
		public virtual void VisitGenericName(GenericNameSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E38 RID: 20024 RVA: 0x00140A31 File Offset: 0x0013EC31
		public virtual void VisitTypeArgumentList(TypeArgumentListSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E39 RID: 20025 RVA: 0x00140A3A File Offset: 0x0013EC3A
		public virtual void VisitAliasQualifiedName(AliasQualifiedNameSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E3A RID: 20026 RVA: 0x00140A43 File Offset: 0x0013EC43
		public virtual void VisitPredefinedType(PredefinedTypeSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E3B RID: 20027 RVA: 0x00140A4C File Offset: 0x0013EC4C
		public virtual void VisitArrayType(ArrayTypeSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E3C RID: 20028 RVA: 0x00140A55 File Offset: 0x0013EC55
		public virtual void VisitArrayRankSpecifier(ArrayRankSpecifierSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E3D RID: 20029 RVA: 0x00140A5E File Offset: 0x0013EC5E
		public virtual void VisitPointerType(PointerTypeSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E3E RID: 20030 RVA: 0x00140A67 File Offset: 0x0013EC67
		public virtual void VisitFunctionPointerType(FunctionPointerTypeSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E3F RID: 20031 RVA: 0x00140A70 File Offset: 0x0013EC70
		public virtual void VisitFunctionPointerParameterList(FunctionPointerParameterListSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E40 RID: 20032 RVA: 0x00140A79 File Offset: 0x0013EC79
		public virtual void VisitFunctionPointerCallingConvention(FunctionPointerCallingConventionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E41 RID: 20033 RVA: 0x00140A82 File Offset: 0x0013EC82
		public virtual void VisitFunctionPointerUnmanagedCallingConventionList(FunctionPointerUnmanagedCallingConventionListSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E42 RID: 20034 RVA: 0x00140A8B File Offset: 0x0013EC8B
		public virtual void VisitFunctionPointerUnmanagedCallingConvention(FunctionPointerUnmanagedCallingConventionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E43 RID: 20035 RVA: 0x00140A94 File Offset: 0x0013EC94
		public virtual void VisitNullableType(NullableTypeSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E44 RID: 20036 RVA: 0x00140A9D File Offset: 0x0013EC9D
		public virtual void VisitTupleType(TupleTypeSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E45 RID: 20037 RVA: 0x00140AA6 File Offset: 0x0013ECA6
		public virtual void VisitTupleElement(TupleElementSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E46 RID: 20038 RVA: 0x00140AAF File Offset: 0x0013ECAF
		public virtual void VisitOmittedTypeArgument(OmittedTypeArgumentSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E47 RID: 20039 RVA: 0x00140AB8 File Offset: 0x0013ECB8
		public virtual void VisitRefType(RefTypeSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E48 RID: 20040 RVA: 0x00140AC1 File Offset: 0x0013ECC1
		public virtual void VisitParenthesizedExpression(ParenthesizedExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E49 RID: 20041 RVA: 0x00140ACA File Offset: 0x0013ECCA
		public virtual void VisitTupleExpression(TupleExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E4A RID: 20042 RVA: 0x00140AD3 File Offset: 0x0013ECD3
		public virtual void VisitPrefixUnaryExpression(PrefixUnaryExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E4B RID: 20043 RVA: 0x00140ADC File Offset: 0x0013ECDC
		public virtual void VisitAwaitExpression(AwaitExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E4C RID: 20044 RVA: 0x00140AE5 File Offset: 0x0013ECE5
		public virtual void VisitPostfixUnaryExpression(PostfixUnaryExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E4D RID: 20045 RVA: 0x00140AEE File Offset: 0x0013ECEE
		public virtual void VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E4E RID: 20046 RVA: 0x00140AF7 File Offset: 0x0013ECF7
		public virtual void VisitConditionalAccessExpression(ConditionalAccessExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E4F RID: 20047 RVA: 0x00140B00 File Offset: 0x0013ED00
		public virtual void VisitMemberBindingExpression(MemberBindingExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E50 RID: 20048 RVA: 0x00140B09 File Offset: 0x0013ED09
		public virtual void VisitElementBindingExpression(ElementBindingExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E51 RID: 20049 RVA: 0x00140B12 File Offset: 0x0013ED12
		public virtual void VisitRangeExpression(RangeExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E52 RID: 20050 RVA: 0x00140B1B File Offset: 0x0013ED1B
		public virtual void VisitImplicitElementAccess(ImplicitElementAccessSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E53 RID: 20051 RVA: 0x00140B24 File Offset: 0x0013ED24
		public virtual void VisitBinaryExpression(BinaryExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E54 RID: 20052 RVA: 0x00140B2D File Offset: 0x0013ED2D
		public virtual void VisitAssignmentExpression(AssignmentExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E55 RID: 20053 RVA: 0x00140B36 File Offset: 0x0013ED36
		public virtual void VisitConditionalExpression(ConditionalExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E56 RID: 20054 RVA: 0x00140B3F File Offset: 0x0013ED3F
		public virtual void VisitThisExpression(ThisExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E57 RID: 20055 RVA: 0x00140B48 File Offset: 0x0013ED48
		public virtual void VisitBaseExpression(BaseExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E58 RID: 20056 RVA: 0x00140B51 File Offset: 0x0013ED51
		public virtual void VisitLiteralExpression(LiteralExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E59 RID: 20057 RVA: 0x00140B5A File Offset: 0x0013ED5A
		public virtual void VisitMakeRefExpression(MakeRefExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E5A RID: 20058 RVA: 0x00140B63 File Offset: 0x0013ED63
		public virtual void VisitRefTypeExpression(RefTypeExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E5B RID: 20059 RVA: 0x00140B6C File Offset: 0x0013ED6C
		public virtual void VisitRefValueExpression(RefValueExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E5C RID: 20060 RVA: 0x00140B75 File Offset: 0x0013ED75
		public virtual void VisitCheckedExpression(CheckedExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E5D RID: 20061 RVA: 0x00140B7E File Offset: 0x0013ED7E
		public virtual void VisitDefaultExpression(DefaultExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E5E RID: 20062 RVA: 0x00140B87 File Offset: 0x0013ED87
		public virtual void VisitTypeOfExpression(TypeOfExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E5F RID: 20063 RVA: 0x00140B90 File Offset: 0x0013ED90
		public virtual void VisitSizeOfExpression(SizeOfExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E60 RID: 20064 RVA: 0x00140B99 File Offset: 0x0013ED99
		public virtual void VisitInvocationExpression(InvocationExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E61 RID: 20065 RVA: 0x00140BA2 File Offset: 0x0013EDA2
		public virtual void VisitElementAccessExpression(ElementAccessExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E62 RID: 20066 RVA: 0x00140BAB File Offset: 0x0013EDAB
		public virtual void VisitArgumentList(ArgumentListSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E63 RID: 20067 RVA: 0x00140BB4 File Offset: 0x0013EDB4
		public virtual void VisitBracketedArgumentList(BracketedArgumentListSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E64 RID: 20068 RVA: 0x00140BBD File Offset: 0x0013EDBD
		public virtual void VisitArgument(ArgumentSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E65 RID: 20069 RVA: 0x00140BC6 File Offset: 0x0013EDC6
		public virtual void VisitNameColon(NameColonSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E66 RID: 20070 RVA: 0x00140BCF File Offset: 0x0013EDCF
		public virtual void VisitDeclarationExpression(DeclarationExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E67 RID: 20071 RVA: 0x00140BD8 File Offset: 0x0013EDD8
		public virtual void VisitCastExpression(CastExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E68 RID: 20072 RVA: 0x00140BE1 File Offset: 0x0013EDE1
		public virtual void VisitAnonymousMethodExpression(AnonymousMethodExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E69 RID: 20073 RVA: 0x00140BEA File Offset: 0x0013EDEA
		public virtual void VisitSimpleLambdaExpression(SimpleLambdaExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E6A RID: 20074 RVA: 0x00140BF3 File Offset: 0x0013EDF3
		public virtual void VisitRefExpression(RefExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E6B RID: 20075 RVA: 0x00140BFC File Offset: 0x0013EDFC
		public virtual void VisitParenthesizedLambdaExpression(ParenthesizedLambdaExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E6C RID: 20076 RVA: 0x00140C05 File Offset: 0x0013EE05
		public virtual void VisitInitializerExpression(InitializerExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E6D RID: 20077 RVA: 0x00140C0E File Offset: 0x0013EE0E
		public virtual void VisitImplicitObjectCreationExpression(ImplicitObjectCreationExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E6E RID: 20078 RVA: 0x00140C17 File Offset: 0x0013EE17
		public virtual void VisitObjectCreationExpression(ObjectCreationExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E6F RID: 20079 RVA: 0x00140C20 File Offset: 0x0013EE20
		public virtual void VisitWithExpression(WithExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E70 RID: 20080 RVA: 0x00140C29 File Offset: 0x0013EE29
		public virtual void VisitAnonymousObjectMemberDeclarator(AnonymousObjectMemberDeclaratorSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E71 RID: 20081 RVA: 0x00140C32 File Offset: 0x0013EE32
		public virtual void VisitAnonymousObjectCreationExpression(AnonymousObjectCreationExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E72 RID: 20082 RVA: 0x00140C3B File Offset: 0x0013EE3B
		public virtual void VisitArrayCreationExpression(ArrayCreationExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E73 RID: 20083 RVA: 0x00140C44 File Offset: 0x0013EE44
		public virtual void VisitImplicitArrayCreationExpression(ImplicitArrayCreationExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E74 RID: 20084 RVA: 0x00140C4D File Offset: 0x0013EE4D
		public virtual void VisitStackAllocArrayCreationExpression(StackAllocArrayCreationExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E75 RID: 20085 RVA: 0x00140C56 File Offset: 0x0013EE56
		public virtual void VisitImplicitStackAllocArrayCreationExpression(ImplicitStackAllocArrayCreationExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E76 RID: 20086 RVA: 0x00140C5F File Offset: 0x0013EE5F
		public virtual void VisitQueryExpression(QueryExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E77 RID: 20087 RVA: 0x00140C68 File Offset: 0x0013EE68
		public virtual void VisitQueryBody(QueryBodySyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E78 RID: 20088 RVA: 0x00140C71 File Offset: 0x0013EE71
		public virtual void VisitFromClause(FromClauseSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E79 RID: 20089 RVA: 0x00140C7A File Offset: 0x0013EE7A
		public virtual void VisitLetClause(LetClauseSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E7A RID: 20090 RVA: 0x00140C83 File Offset: 0x0013EE83
		public virtual void VisitJoinClause(JoinClauseSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E7B RID: 20091 RVA: 0x00140C8C File Offset: 0x0013EE8C
		public virtual void VisitJoinIntoClause(JoinIntoClauseSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E7C RID: 20092 RVA: 0x00140C95 File Offset: 0x0013EE95
		public virtual void VisitWhereClause(WhereClauseSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E7D RID: 20093 RVA: 0x00140C9E File Offset: 0x0013EE9E
		public virtual void VisitOrderByClause(OrderByClauseSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E7E RID: 20094 RVA: 0x00140CA7 File Offset: 0x0013EEA7
		public virtual void VisitOrdering(OrderingSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E7F RID: 20095 RVA: 0x00140CB0 File Offset: 0x0013EEB0
		public virtual void VisitSelectClause(SelectClauseSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E80 RID: 20096 RVA: 0x00140CB9 File Offset: 0x0013EEB9
		public virtual void VisitGroupClause(GroupClauseSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E81 RID: 20097 RVA: 0x00140CC2 File Offset: 0x0013EEC2
		public virtual void VisitQueryContinuation(QueryContinuationSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E82 RID: 20098 RVA: 0x00140CCB File Offset: 0x0013EECB
		public virtual void VisitOmittedArraySizeExpression(OmittedArraySizeExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E83 RID: 20099 RVA: 0x00140CD4 File Offset: 0x0013EED4
		public virtual void VisitInterpolatedStringExpression(InterpolatedStringExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E84 RID: 20100 RVA: 0x00140CDD File Offset: 0x0013EEDD
		public virtual void VisitIsPatternExpression(IsPatternExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E85 RID: 20101 RVA: 0x00140CE6 File Offset: 0x0013EEE6
		public virtual void VisitThrowExpression(ThrowExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E86 RID: 20102 RVA: 0x00140CEF File Offset: 0x0013EEEF
		public virtual void VisitWhenClause(WhenClauseSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E87 RID: 20103 RVA: 0x00140CF8 File Offset: 0x0013EEF8
		public virtual void VisitDiscardPattern(DiscardPatternSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E88 RID: 20104 RVA: 0x00140D01 File Offset: 0x0013EF01
		public virtual void VisitDeclarationPattern(DeclarationPatternSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E89 RID: 20105 RVA: 0x00140D0A File Offset: 0x0013EF0A
		public virtual void VisitVarPattern(VarPatternSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E8A RID: 20106 RVA: 0x00140D13 File Offset: 0x0013EF13
		public virtual void VisitRecursivePattern(RecursivePatternSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E8B RID: 20107 RVA: 0x00140D1C File Offset: 0x0013EF1C
		public virtual void VisitPositionalPatternClause(PositionalPatternClauseSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E8C RID: 20108 RVA: 0x00140D25 File Offset: 0x0013EF25
		public virtual void VisitPropertyPatternClause(PropertyPatternClauseSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E8D RID: 20109 RVA: 0x00140D2E File Offset: 0x0013EF2E
		public virtual void VisitSubpattern(SubpatternSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E8E RID: 20110 RVA: 0x00140D37 File Offset: 0x0013EF37
		public virtual void VisitConstantPattern(ConstantPatternSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E8F RID: 20111 RVA: 0x00140D40 File Offset: 0x0013EF40
		public virtual void VisitParenthesizedPattern(ParenthesizedPatternSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E90 RID: 20112 RVA: 0x00140D49 File Offset: 0x0013EF49
		public virtual void VisitRelationalPattern(RelationalPatternSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E91 RID: 20113 RVA: 0x00140D52 File Offset: 0x0013EF52
		public virtual void VisitTypePattern(TypePatternSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E92 RID: 20114 RVA: 0x00140D5B File Offset: 0x0013EF5B
		public virtual void VisitBinaryPattern(BinaryPatternSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E93 RID: 20115 RVA: 0x00140D64 File Offset: 0x0013EF64
		public virtual void VisitUnaryPattern(UnaryPatternSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E94 RID: 20116 RVA: 0x00140D6D File Offset: 0x0013EF6D
		public virtual void VisitInterpolatedStringText(InterpolatedStringTextSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E95 RID: 20117 RVA: 0x00140D76 File Offset: 0x0013EF76
		public virtual void VisitInterpolation(InterpolationSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E96 RID: 20118 RVA: 0x00140D7F File Offset: 0x0013EF7F
		public virtual void VisitInterpolationAlignmentClause(InterpolationAlignmentClauseSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E97 RID: 20119 RVA: 0x00140D88 File Offset: 0x0013EF88
		public virtual void VisitInterpolationFormatClause(InterpolationFormatClauseSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E98 RID: 20120 RVA: 0x00140D91 File Offset: 0x0013EF91
		public virtual void VisitGlobalStatement(GlobalStatementSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E99 RID: 20121 RVA: 0x00140D9A File Offset: 0x0013EF9A
		public virtual void VisitBlock(BlockSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E9A RID: 20122 RVA: 0x00140DA3 File Offset: 0x0013EFA3
		public virtual void VisitLocalFunctionStatement(LocalFunctionStatementSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E9B RID: 20123 RVA: 0x00140DAC File Offset: 0x0013EFAC
		public virtual void VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E9C RID: 20124 RVA: 0x00140DB5 File Offset: 0x0013EFB5
		public virtual void VisitVariableDeclaration(VariableDeclarationSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E9D RID: 20125 RVA: 0x00140DBE File Offset: 0x0013EFBE
		public virtual void VisitVariableDeclarator(VariableDeclaratorSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E9E RID: 20126 RVA: 0x00140DC7 File Offset: 0x0013EFC7
		public virtual void VisitEqualsValueClause(EqualsValueClauseSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004E9F RID: 20127 RVA: 0x00140DD0 File Offset: 0x0013EFD0
		public virtual void VisitSingleVariableDesignation(SingleVariableDesignationSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EA0 RID: 20128 RVA: 0x00140DD9 File Offset: 0x0013EFD9
		public virtual void VisitDiscardDesignation(DiscardDesignationSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EA1 RID: 20129 RVA: 0x00140DE2 File Offset: 0x0013EFE2
		public virtual void VisitParenthesizedVariableDesignation(ParenthesizedVariableDesignationSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EA2 RID: 20130 RVA: 0x00140DEB File Offset: 0x0013EFEB
		public virtual void VisitExpressionStatement(ExpressionStatementSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EA3 RID: 20131 RVA: 0x00140DF4 File Offset: 0x0013EFF4
		public virtual void VisitEmptyStatement(EmptyStatementSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EA4 RID: 20132 RVA: 0x00140DFD File Offset: 0x0013EFFD
		public virtual void VisitLabeledStatement(LabeledStatementSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EA5 RID: 20133 RVA: 0x00140E06 File Offset: 0x0013F006
		public virtual void VisitGotoStatement(GotoStatementSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EA6 RID: 20134 RVA: 0x00140E0F File Offset: 0x0013F00F
		public virtual void VisitBreakStatement(BreakStatementSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EA7 RID: 20135 RVA: 0x00140E18 File Offset: 0x0013F018
		public virtual void VisitContinueStatement(ContinueStatementSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EA8 RID: 20136 RVA: 0x00140E21 File Offset: 0x0013F021
		public virtual void VisitReturnStatement(ReturnStatementSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EA9 RID: 20137 RVA: 0x00140E2A File Offset: 0x0013F02A
		public virtual void VisitThrowStatement(ThrowStatementSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EAA RID: 20138 RVA: 0x00140E33 File Offset: 0x0013F033
		public virtual void VisitYieldStatement(YieldStatementSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EAB RID: 20139 RVA: 0x00140E3C File Offset: 0x0013F03C
		public virtual void VisitWhileStatement(WhileStatementSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EAC RID: 20140 RVA: 0x00140E45 File Offset: 0x0013F045
		public virtual void VisitDoStatement(DoStatementSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EAD RID: 20141 RVA: 0x00140E4E File Offset: 0x0013F04E
		public virtual void VisitForStatement(ForStatementSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EAE RID: 20142 RVA: 0x00140E57 File Offset: 0x0013F057
		public virtual void VisitForEachStatement(ForEachStatementSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EAF RID: 20143 RVA: 0x00140E60 File Offset: 0x0013F060
		public virtual void VisitForEachVariableStatement(ForEachVariableStatementSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EB0 RID: 20144 RVA: 0x00140E69 File Offset: 0x0013F069
		public virtual void VisitUsingStatement(UsingStatementSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EB1 RID: 20145 RVA: 0x00140E72 File Offset: 0x0013F072
		public virtual void VisitFixedStatement(FixedStatementSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EB2 RID: 20146 RVA: 0x00140E7B File Offset: 0x0013F07B
		public virtual void VisitCheckedStatement(CheckedStatementSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EB3 RID: 20147 RVA: 0x00140E84 File Offset: 0x0013F084
		public virtual void VisitUnsafeStatement(UnsafeStatementSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EB4 RID: 20148 RVA: 0x00140E8D File Offset: 0x0013F08D
		public virtual void VisitLockStatement(LockStatementSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EB5 RID: 20149 RVA: 0x00140E96 File Offset: 0x0013F096
		public virtual void VisitIfStatement(IfStatementSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EB6 RID: 20150 RVA: 0x00140E9F File Offset: 0x0013F09F
		public virtual void VisitElseClause(ElseClauseSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EB7 RID: 20151 RVA: 0x00140EA8 File Offset: 0x0013F0A8
		public virtual void VisitSwitchStatement(SwitchStatementSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EB8 RID: 20152 RVA: 0x00140EB1 File Offset: 0x0013F0B1
		public virtual void VisitSwitchSection(SwitchSectionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EB9 RID: 20153 RVA: 0x00140EBA File Offset: 0x0013F0BA
		public virtual void VisitCasePatternSwitchLabel(CasePatternSwitchLabelSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EBA RID: 20154 RVA: 0x00140EC3 File Offset: 0x0013F0C3
		public virtual void VisitCaseSwitchLabel(CaseSwitchLabelSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EBB RID: 20155 RVA: 0x00140ECC File Offset: 0x0013F0CC
		public virtual void VisitDefaultSwitchLabel(DefaultSwitchLabelSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EBC RID: 20156 RVA: 0x00140ED5 File Offset: 0x0013F0D5
		public virtual void VisitSwitchExpression(SwitchExpressionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EBD RID: 20157 RVA: 0x00140EDE File Offset: 0x0013F0DE
		public virtual void VisitSwitchExpressionArm(SwitchExpressionArmSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EBE RID: 20158 RVA: 0x00140EE7 File Offset: 0x0013F0E7
		public virtual void VisitTryStatement(TryStatementSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EBF RID: 20159 RVA: 0x00140EF0 File Offset: 0x0013F0F0
		public virtual void VisitCatchClause(CatchClauseSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EC0 RID: 20160 RVA: 0x00140EF9 File Offset: 0x0013F0F9
		public virtual void VisitCatchDeclaration(CatchDeclarationSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EC1 RID: 20161 RVA: 0x00140F02 File Offset: 0x0013F102
		public virtual void VisitCatchFilterClause(CatchFilterClauseSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EC2 RID: 20162 RVA: 0x00140F0B File Offset: 0x0013F10B
		public virtual void VisitFinallyClause(FinallyClauseSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EC3 RID: 20163 RVA: 0x00140F14 File Offset: 0x0013F114
		public virtual void VisitCompilationUnit(CompilationUnitSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EC4 RID: 20164 RVA: 0x00140F1D File Offset: 0x0013F11D
		public virtual void VisitExternAliasDirective(ExternAliasDirectiveSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EC5 RID: 20165 RVA: 0x00140F26 File Offset: 0x0013F126
		public virtual void VisitUsingDirective(UsingDirectiveSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EC6 RID: 20166 RVA: 0x00140F2F File Offset: 0x0013F12F
		public virtual void VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EC7 RID: 20167 RVA: 0x00140F38 File Offset: 0x0013F138
		public virtual void VisitAttributeList(AttributeListSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EC8 RID: 20168 RVA: 0x00140F41 File Offset: 0x0013F141
		public virtual void VisitAttributeTargetSpecifier(AttributeTargetSpecifierSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EC9 RID: 20169 RVA: 0x00140F4A File Offset: 0x0013F14A
		public virtual void VisitAttribute(AttributeSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004ECA RID: 20170 RVA: 0x00140F53 File Offset: 0x0013F153
		public virtual void VisitAttributeArgumentList(AttributeArgumentListSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004ECB RID: 20171 RVA: 0x00140F5C File Offset: 0x0013F15C
		public virtual void VisitAttributeArgument(AttributeArgumentSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004ECC RID: 20172 RVA: 0x00140F65 File Offset: 0x0013F165
		public virtual void VisitNameEquals(NameEqualsSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004ECD RID: 20173 RVA: 0x00140F6E File Offset: 0x0013F16E
		public virtual void VisitTypeParameterList(TypeParameterListSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004ECE RID: 20174 RVA: 0x00140F77 File Offset: 0x0013F177
		public virtual void VisitTypeParameter(TypeParameterSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004ECF RID: 20175 RVA: 0x00140F80 File Offset: 0x0013F180
		public virtual void VisitClassDeclaration(ClassDeclarationSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004ED0 RID: 20176 RVA: 0x00140F89 File Offset: 0x0013F189
		public virtual void VisitStructDeclaration(StructDeclarationSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004ED1 RID: 20177 RVA: 0x00140F92 File Offset: 0x0013F192
		public virtual void VisitInterfaceDeclaration(InterfaceDeclarationSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004ED2 RID: 20178 RVA: 0x00140F9B File Offset: 0x0013F19B
		public virtual void VisitRecordDeclaration(RecordDeclarationSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004ED3 RID: 20179 RVA: 0x00140FA4 File Offset: 0x0013F1A4
		public virtual void VisitEnumDeclaration(EnumDeclarationSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004ED4 RID: 20180 RVA: 0x00140FAD File Offset: 0x0013F1AD
		public virtual void VisitDelegateDeclaration(DelegateDeclarationSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004ED5 RID: 20181 RVA: 0x00140FB6 File Offset: 0x0013F1B6
		public virtual void VisitEnumMemberDeclaration(EnumMemberDeclarationSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004ED6 RID: 20182 RVA: 0x00140FBF File Offset: 0x0013F1BF
		public virtual void VisitBaseList(BaseListSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004ED7 RID: 20183 RVA: 0x00140FC8 File Offset: 0x0013F1C8
		public virtual void VisitSimpleBaseType(SimpleBaseTypeSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004ED8 RID: 20184 RVA: 0x00140FD1 File Offset: 0x0013F1D1
		public virtual void VisitPrimaryConstructorBaseType(PrimaryConstructorBaseTypeSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004ED9 RID: 20185 RVA: 0x00140FDA File Offset: 0x0013F1DA
		public virtual void VisitTypeParameterConstraintClause(TypeParameterConstraintClauseSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EDA RID: 20186 RVA: 0x00140FE3 File Offset: 0x0013F1E3
		public virtual void VisitConstructorConstraint(ConstructorConstraintSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EDB RID: 20187 RVA: 0x00140FEC File Offset: 0x0013F1EC
		public virtual void VisitClassOrStructConstraint(ClassOrStructConstraintSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EDC RID: 20188 RVA: 0x00140FF5 File Offset: 0x0013F1F5
		public virtual void VisitTypeConstraint(TypeConstraintSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EDD RID: 20189 RVA: 0x00140FFE File Offset: 0x0013F1FE
		public virtual void VisitDefaultConstraint(DefaultConstraintSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EDE RID: 20190 RVA: 0x00141007 File Offset: 0x0013F207
		public virtual void VisitFieldDeclaration(FieldDeclarationSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EDF RID: 20191 RVA: 0x00141010 File Offset: 0x0013F210
		public virtual void VisitEventFieldDeclaration(EventFieldDeclarationSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EE0 RID: 20192 RVA: 0x00141019 File Offset: 0x0013F219
		public virtual void VisitExplicitInterfaceSpecifier(ExplicitInterfaceSpecifierSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EE1 RID: 20193 RVA: 0x00141022 File Offset: 0x0013F222
		public virtual void VisitMethodDeclaration(MethodDeclarationSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EE2 RID: 20194 RVA: 0x0014102B File Offset: 0x0013F22B
		public virtual void VisitOperatorDeclaration(OperatorDeclarationSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EE3 RID: 20195 RVA: 0x00141034 File Offset: 0x0013F234
		public virtual void VisitConversionOperatorDeclaration(ConversionOperatorDeclarationSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EE4 RID: 20196 RVA: 0x0014103D File Offset: 0x0013F23D
		public virtual void VisitConstructorDeclaration(ConstructorDeclarationSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EE5 RID: 20197 RVA: 0x00141046 File Offset: 0x0013F246
		public virtual void VisitConstructorInitializer(ConstructorInitializerSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EE6 RID: 20198 RVA: 0x0014104F File Offset: 0x0013F24F
		public virtual void VisitDestructorDeclaration(DestructorDeclarationSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EE7 RID: 20199 RVA: 0x00141058 File Offset: 0x0013F258
		public virtual void VisitPropertyDeclaration(PropertyDeclarationSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EE8 RID: 20200 RVA: 0x00141061 File Offset: 0x0013F261
		public virtual void VisitArrowExpressionClause(ArrowExpressionClauseSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EE9 RID: 20201 RVA: 0x0014106A File Offset: 0x0013F26A
		public virtual void VisitEventDeclaration(EventDeclarationSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EEA RID: 20202 RVA: 0x00141073 File Offset: 0x0013F273
		public virtual void VisitIndexerDeclaration(IndexerDeclarationSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EEB RID: 20203 RVA: 0x0014107C File Offset: 0x0013F27C
		public virtual void VisitAccessorList(AccessorListSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EEC RID: 20204 RVA: 0x00141085 File Offset: 0x0013F285
		public virtual void VisitAccessorDeclaration(AccessorDeclarationSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EED RID: 20205 RVA: 0x0014108E File Offset: 0x0013F28E
		public virtual void VisitParameterList(ParameterListSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EEE RID: 20206 RVA: 0x00141097 File Offset: 0x0013F297
		public virtual void VisitBracketedParameterList(BracketedParameterListSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EEF RID: 20207 RVA: 0x001410A0 File Offset: 0x0013F2A0
		public virtual void VisitParameter(ParameterSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EF0 RID: 20208 RVA: 0x001410A9 File Offset: 0x0013F2A9
		public virtual void VisitFunctionPointerParameter(FunctionPointerParameterSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EF1 RID: 20209 RVA: 0x001410B2 File Offset: 0x0013F2B2
		public virtual void VisitIncompleteMember(IncompleteMemberSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EF2 RID: 20210 RVA: 0x001410BB File Offset: 0x0013F2BB
		public virtual void VisitSkippedTokensTrivia(SkippedTokensTriviaSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EF3 RID: 20211 RVA: 0x001410C4 File Offset: 0x0013F2C4
		public virtual void VisitDocumentationCommentTrivia(DocumentationCommentTriviaSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EF4 RID: 20212 RVA: 0x001410CD File Offset: 0x0013F2CD
		public virtual void VisitTypeCref(TypeCrefSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EF5 RID: 20213 RVA: 0x001410D6 File Offset: 0x0013F2D6
		public virtual void VisitQualifiedCref(QualifiedCrefSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EF6 RID: 20214 RVA: 0x001410DF File Offset: 0x0013F2DF
		public virtual void VisitNameMemberCref(NameMemberCrefSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EF7 RID: 20215 RVA: 0x001410E8 File Offset: 0x0013F2E8
		public virtual void VisitIndexerMemberCref(IndexerMemberCrefSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EF8 RID: 20216 RVA: 0x001410F1 File Offset: 0x0013F2F1
		public virtual void VisitOperatorMemberCref(OperatorMemberCrefSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EF9 RID: 20217 RVA: 0x001410FA File Offset: 0x0013F2FA
		public virtual void VisitConversionOperatorMemberCref(ConversionOperatorMemberCrefSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EFA RID: 20218 RVA: 0x00141103 File Offset: 0x0013F303
		public virtual void VisitCrefParameterList(CrefParameterListSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EFB RID: 20219 RVA: 0x0014110C File Offset: 0x0013F30C
		public virtual void VisitCrefBracketedParameterList(CrefBracketedParameterListSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EFC RID: 20220 RVA: 0x00141115 File Offset: 0x0013F315
		public virtual void VisitCrefParameter(CrefParameterSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EFD RID: 20221 RVA: 0x0014111E File Offset: 0x0013F31E
		public virtual void VisitXmlElement(XmlElementSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EFE RID: 20222 RVA: 0x00141127 File Offset: 0x0013F327
		public virtual void VisitXmlElementStartTag(XmlElementStartTagSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004EFF RID: 20223 RVA: 0x00141130 File Offset: 0x0013F330
		public virtual void VisitXmlElementEndTag(XmlElementEndTagSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004F00 RID: 20224 RVA: 0x00141139 File Offset: 0x0013F339
		public virtual void VisitXmlEmptyElement(XmlEmptyElementSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004F01 RID: 20225 RVA: 0x00141142 File Offset: 0x0013F342
		public virtual void VisitXmlName(XmlNameSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004F02 RID: 20226 RVA: 0x0014114B File Offset: 0x0013F34B
		public virtual void VisitXmlPrefix(XmlPrefixSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004F03 RID: 20227 RVA: 0x00141154 File Offset: 0x0013F354
		public virtual void VisitXmlTextAttribute(XmlTextAttributeSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004F04 RID: 20228 RVA: 0x0014115D File Offset: 0x0013F35D
		public virtual void VisitXmlCrefAttribute(XmlCrefAttributeSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004F05 RID: 20229 RVA: 0x00141166 File Offset: 0x0013F366
		public virtual void VisitXmlNameAttribute(XmlNameAttributeSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004F06 RID: 20230 RVA: 0x0014116F File Offset: 0x0013F36F
		public virtual void VisitXmlText(XmlTextSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004F07 RID: 20231 RVA: 0x00141178 File Offset: 0x0013F378
		public virtual void VisitXmlCDataSection(XmlCDataSectionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004F08 RID: 20232 RVA: 0x00141181 File Offset: 0x0013F381
		public virtual void VisitXmlProcessingInstruction(XmlProcessingInstructionSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004F09 RID: 20233 RVA: 0x0014118A File Offset: 0x0013F38A
		public virtual void VisitXmlComment(XmlCommentSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004F0A RID: 20234 RVA: 0x00141193 File Offset: 0x0013F393
		public virtual void VisitIfDirectiveTrivia(IfDirectiveTriviaSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004F0B RID: 20235 RVA: 0x0014119C File Offset: 0x0013F39C
		public virtual void VisitElifDirectiveTrivia(ElifDirectiveTriviaSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004F0C RID: 20236 RVA: 0x001411A5 File Offset: 0x0013F3A5
		public virtual void VisitElseDirectiveTrivia(ElseDirectiveTriviaSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004F0D RID: 20237 RVA: 0x001411AE File Offset: 0x0013F3AE
		public virtual void VisitEndIfDirectiveTrivia(EndIfDirectiveTriviaSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004F0E RID: 20238 RVA: 0x001411B7 File Offset: 0x0013F3B7
		public virtual void VisitRegionDirectiveTrivia(RegionDirectiveTriviaSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004F0F RID: 20239 RVA: 0x001411C0 File Offset: 0x0013F3C0
		public virtual void VisitEndRegionDirectiveTrivia(EndRegionDirectiveTriviaSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004F10 RID: 20240 RVA: 0x001411C9 File Offset: 0x0013F3C9
		public virtual void VisitErrorDirectiveTrivia(ErrorDirectiveTriviaSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004F11 RID: 20241 RVA: 0x001411D2 File Offset: 0x0013F3D2
		public virtual void VisitWarningDirectiveTrivia(WarningDirectiveTriviaSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004F12 RID: 20242 RVA: 0x001411DB File Offset: 0x0013F3DB
		public virtual void VisitBadDirectiveTrivia(BadDirectiveTriviaSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004F13 RID: 20243 RVA: 0x001411E4 File Offset: 0x0013F3E4
		public virtual void VisitDefineDirectiveTrivia(DefineDirectiveTriviaSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004F14 RID: 20244 RVA: 0x001411ED File Offset: 0x0013F3ED
		public virtual void VisitUndefDirectiveTrivia(UndefDirectiveTriviaSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004F15 RID: 20245 RVA: 0x001411F6 File Offset: 0x0013F3F6
		public virtual void VisitLineDirectiveTrivia(LineDirectiveTriviaSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004F16 RID: 20246 RVA: 0x001411FF File Offset: 0x0013F3FF
		public virtual void VisitPragmaWarningDirectiveTrivia(PragmaWarningDirectiveTriviaSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004F17 RID: 20247 RVA: 0x00141208 File Offset: 0x0013F408
		public virtual void VisitPragmaChecksumDirectiveTrivia(PragmaChecksumDirectiveTriviaSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004F18 RID: 20248 RVA: 0x00141211 File Offset: 0x0013F411
		public virtual void VisitReferenceDirectiveTrivia(ReferenceDirectiveTriviaSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004F19 RID: 20249 RVA: 0x0014121A File Offset: 0x0013F41A
		public virtual void VisitLoadDirectiveTrivia(LoadDirectiveTriviaSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004F1A RID: 20250 RVA: 0x00141223 File Offset: 0x0013F423
		public virtual void VisitShebangDirectiveTrivia(ShebangDirectiveTriviaSyntax node)
		{
			this.DefaultVisit(node);
		}

		// Token: 0x06004F1B RID: 20251 RVA: 0x0014122C File Offset: 0x0013F42C
		public virtual void VisitNullableDirectiveTrivia(NullableDirectiveTriviaSyntax node)
		{
			this.DefaultVisit(node);
		}
	}
}
