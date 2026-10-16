using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200022D RID: 557
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public abstract class CSharpSyntaxVisitor<[global::System.Runtime.CompilerServices.Nullable(2)] TResult>
	{
		// Token: 0x06002CE2 RID: 11490 RVA: 0x000EF244 File Offset: 0x000ED444
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public virtual TResult Visit(SyntaxNode node)
		{
			if (node != null)
			{
				return ((CSharpSyntaxNode)node).Accept<TResult>(this);
			}
			return default(TResult);
		}

		// Token: 0x06002CE3 RID: 11491 RVA: 0x000EF26C File Offset: 0x000ED46C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult DefaultVisit(SyntaxNode node)
		{
			return default(TResult);
		}

		// Token: 0x06002CE4 RID: 11492 RVA: 0x000EF282 File Offset: 0x000ED482
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitIdentifierName(IdentifierNameSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002CE5 RID: 11493 RVA: 0x000EF28B File Offset: 0x000ED48B
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitQualifiedName(QualifiedNameSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002CE6 RID: 11494 RVA: 0x000EF294 File Offset: 0x000ED494
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitGenericName(GenericNameSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002CE7 RID: 11495 RVA: 0x000EF29D File Offset: 0x000ED49D
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitTypeArgumentList(TypeArgumentListSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002CE8 RID: 11496 RVA: 0x000EF2A6 File Offset: 0x000ED4A6
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitAliasQualifiedName(AliasQualifiedNameSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002CE9 RID: 11497 RVA: 0x000EF2AF File Offset: 0x000ED4AF
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitPredefinedType(PredefinedTypeSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002CEA RID: 11498 RVA: 0x000EF2B8 File Offset: 0x000ED4B8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitArrayType(ArrayTypeSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002CEB RID: 11499 RVA: 0x000EF2C1 File Offset: 0x000ED4C1
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitArrayRankSpecifier(ArrayRankSpecifierSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002CEC RID: 11500 RVA: 0x000EF2CA File Offset: 0x000ED4CA
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitPointerType(PointerTypeSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002CED RID: 11501 RVA: 0x000EF2D3 File Offset: 0x000ED4D3
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitFunctionPointerType(FunctionPointerTypeSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002CEE RID: 11502 RVA: 0x000EF2DC File Offset: 0x000ED4DC
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitFunctionPointerParameterList(FunctionPointerParameterListSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002CEF RID: 11503 RVA: 0x000EF2E5 File Offset: 0x000ED4E5
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitFunctionPointerCallingConvention(FunctionPointerCallingConventionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002CF0 RID: 11504 RVA: 0x000EF2EE File Offset: 0x000ED4EE
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitFunctionPointerUnmanagedCallingConventionList(FunctionPointerUnmanagedCallingConventionListSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002CF1 RID: 11505 RVA: 0x000EF2F7 File Offset: 0x000ED4F7
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitFunctionPointerUnmanagedCallingConvention(FunctionPointerUnmanagedCallingConventionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002CF2 RID: 11506 RVA: 0x000EF300 File Offset: 0x000ED500
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitNullableType(NullableTypeSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002CF3 RID: 11507 RVA: 0x000EF309 File Offset: 0x000ED509
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitTupleType(TupleTypeSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002CF4 RID: 11508 RVA: 0x000EF312 File Offset: 0x000ED512
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitTupleElement(TupleElementSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002CF5 RID: 11509 RVA: 0x000EF31B File Offset: 0x000ED51B
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitOmittedTypeArgument(OmittedTypeArgumentSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002CF6 RID: 11510 RVA: 0x000EF324 File Offset: 0x000ED524
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitRefType(RefTypeSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002CF7 RID: 11511 RVA: 0x000EF32D File Offset: 0x000ED52D
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitParenthesizedExpression(ParenthesizedExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002CF8 RID: 11512 RVA: 0x000EF336 File Offset: 0x000ED536
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitTupleExpression(TupleExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002CF9 RID: 11513 RVA: 0x000EF33F File Offset: 0x000ED53F
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitPrefixUnaryExpression(PrefixUnaryExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002CFA RID: 11514 RVA: 0x000EF348 File Offset: 0x000ED548
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitAwaitExpression(AwaitExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002CFB RID: 11515 RVA: 0x000EF351 File Offset: 0x000ED551
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitPostfixUnaryExpression(PostfixUnaryExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002CFC RID: 11516 RVA: 0x000EF35A File Offset: 0x000ED55A
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002CFD RID: 11517 RVA: 0x000EF363 File Offset: 0x000ED563
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitConditionalAccessExpression(ConditionalAccessExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002CFE RID: 11518 RVA: 0x000EF36C File Offset: 0x000ED56C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitMemberBindingExpression(MemberBindingExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002CFF RID: 11519 RVA: 0x000EF375 File Offset: 0x000ED575
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitElementBindingExpression(ElementBindingExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D00 RID: 11520 RVA: 0x000EF37E File Offset: 0x000ED57E
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitRangeExpression(RangeExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D01 RID: 11521 RVA: 0x000EF387 File Offset: 0x000ED587
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitImplicitElementAccess(ImplicitElementAccessSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D02 RID: 11522 RVA: 0x000EF390 File Offset: 0x000ED590
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitBinaryExpression(BinaryExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D03 RID: 11523 RVA: 0x000EF399 File Offset: 0x000ED599
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitAssignmentExpression(AssignmentExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D04 RID: 11524 RVA: 0x000EF3A2 File Offset: 0x000ED5A2
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitConditionalExpression(ConditionalExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D05 RID: 11525 RVA: 0x000EF3AB File Offset: 0x000ED5AB
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitThisExpression(ThisExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D06 RID: 11526 RVA: 0x000EF3B4 File Offset: 0x000ED5B4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitBaseExpression(BaseExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D07 RID: 11527 RVA: 0x000EF3BD File Offset: 0x000ED5BD
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitLiteralExpression(LiteralExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D08 RID: 11528 RVA: 0x000EF3C6 File Offset: 0x000ED5C6
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitMakeRefExpression(MakeRefExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D09 RID: 11529 RVA: 0x000EF3CF File Offset: 0x000ED5CF
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitRefTypeExpression(RefTypeExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D0A RID: 11530 RVA: 0x000EF3D8 File Offset: 0x000ED5D8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitRefValueExpression(RefValueExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D0B RID: 11531 RVA: 0x000EF3E1 File Offset: 0x000ED5E1
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitCheckedExpression(CheckedExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D0C RID: 11532 RVA: 0x000EF3EA File Offset: 0x000ED5EA
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitDefaultExpression(DefaultExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D0D RID: 11533 RVA: 0x000EF3F3 File Offset: 0x000ED5F3
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitTypeOfExpression(TypeOfExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D0E RID: 11534 RVA: 0x000EF3FC File Offset: 0x000ED5FC
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitSizeOfExpression(SizeOfExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D0F RID: 11535 RVA: 0x000EF405 File Offset: 0x000ED605
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitInvocationExpression(InvocationExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D10 RID: 11536 RVA: 0x000EF40E File Offset: 0x000ED60E
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitElementAccessExpression(ElementAccessExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D11 RID: 11537 RVA: 0x000EF417 File Offset: 0x000ED617
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitArgumentList(ArgumentListSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D12 RID: 11538 RVA: 0x000EF420 File Offset: 0x000ED620
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitBracketedArgumentList(BracketedArgumentListSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D13 RID: 11539 RVA: 0x000EF429 File Offset: 0x000ED629
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitArgument(ArgumentSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D14 RID: 11540 RVA: 0x000EF432 File Offset: 0x000ED632
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitNameColon(NameColonSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D15 RID: 11541 RVA: 0x000EF43B File Offset: 0x000ED63B
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitDeclarationExpression(DeclarationExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D16 RID: 11542 RVA: 0x000EF444 File Offset: 0x000ED644
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitCastExpression(CastExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D17 RID: 11543 RVA: 0x000EF44D File Offset: 0x000ED64D
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitAnonymousMethodExpression(AnonymousMethodExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D18 RID: 11544 RVA: 0x000EF456 File Offset: 0x000ED656
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitSimpleLambdaExpression(SimpleLambdaExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D19 RID: 11545 RVA: 0x000EF45F File Offset: 0x000ED65F
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitRefExpression(RefExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D1A RID: 11546 RVA: 0x000EF468 File Offset: 0x000ED668
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitParenthesizedLambdaExpression(ParenthesizedLambdaExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D1B RID: 11547 RVA: 0x000EF471 File Offset: 0x000ED671
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitInitializerExpression(InitializerExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D1C RID: 11548 RVA: 0x000EF47A File Offset: 0x000ED67A
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitImplicitObjectCreationExpression(ImplicitObjectCreationExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D1D RID: 11549 RVA: 0x000EF483 File Offset: 0x000ED683
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitObjectCreationExpression(ObjectCreationExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D1E RID: 11550 RVA: 0x000EF48C File Offset: 0x000ED68C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitWithExpression(WithExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D1F RID: 11551 RVA: 0x000EF495 File Offset: 0x000ED695
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitAnonymousObjectMemberDeclarator(AnonymousObjectMemberDeclaratorSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D20 RID: 11552 RVA: 0x000EF49E File Offset: 0x000ED69E
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitAnonymousObjectCreationExpression(AnonymousObjectCreationExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D21 RID: 11553 RVA: 0x000EF4A7 File Offset: 0x000ED6A7
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitArrayCreationExpression(ArrayCreationExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D22 RID: 11554 RVA: 0x000EF4B0 File Offset: 0x000ED6B0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitImplicitArrayCreationExpression(ImplicitArrayCreationExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D23 RID: 11555 RVA: 0x000EF4B9 File Offset: 0x000ED6B9
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitStackAllocArrayCreationExpression(StackAllocArrayCreationExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D24 RID: 11556 RVA: 0x000EF4C2 File Offset: 0x000ED6C2
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitImplicitStackAllocArrayCreationExpression(ImplicitStackAllocArrayCreationExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D25 RID: 11557 RVA: 0x000EF4CB File Offset: 0x000ED6CB
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitQueryExpression(QueryExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D26 RID: 11558 RVA: 0x000EF4D4 File Offset: 0x000ED6D4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitQueryBody(QueryBodySyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D27 RID: 11559 RVA: 0x000EF4DD File Offset: 0x000ED6DD
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitFromClause(FromClauseSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D28 RID: 11560 RVA: 0x000EF4E6 File Offset: 0x000ED6E6
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitLetClause(LetClauseSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D29 RID: 11561 RVA: 0x000EF4EF File Offset: 0x000ED6EF
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitJoinClause(JoinClauseSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D2A RID: 11562 RVA: 0x000EF4F8 File Offset: 0x000ED6F8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitJoinIntoClause(JoinIntoClauseSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D2B RID: 11563 RVA: 0x000EF501 File Offset: 0x000ED701
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitWhereClause(WhereClauseSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D2C RID: 11564 RVA: 0x000EF50A File Offset: 0x000ED70A
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitOrderByClause(OrderByClauseSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D2D RID: 11565 RVA: 0x000EF513 File Offset: 0x000ED713
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitOrdering(OrderingSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D2E RID: 11566 RVA: 0x000EF51C File Offset: 0x000ED71C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitSelectClause(SelectClauseSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D2F RID: 11567 RVA: 0x000EF525 File Offset: 0x000ED725
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitGroupClause(GroupClauseSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D30 RID: 11568 RVA: 0x000EF52E File Offset: 0x000ED72E
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitQueryContinuation(QueryContinuationSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D31 RID: 11569 RVA: 0x000EF537 File Offset: 0x000ED737
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitOmittedArraySizeExpression(OmittedArraySizeExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D32 RID: 11570 RVA: 0x000EF540 File Offset: 0x000ED740
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitInterpolatedStringExpression(InterpolatedStringExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D33 RID: 11571 RVA: 0x000EF549 File Offset: 0x000ED749
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitIsPatternExpression(IsPatternExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D34 RID: 11572 RVA: 0x000EF552 File Offset: 0x000ED752
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitThrowExpression(ThrowExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D35 RID: 11573 RVA: 0x000EF55B File Offset: 0x000ED75B
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitWhenClause(WhenClauseSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D36 RID: 11574 RVA: 0x000EF564 File Offset: 0x000ED764
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitDiscardPattern(DiscardPatternSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D37 RID: 11575 RVA: 0x000EF56D File Offset: 0x000ED76D
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitDeclarationPattern(DeclarationPatternSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D38 RID: 11576 RVA: 0x000EF576 File Offset: 0x000ED776
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitVarPattern(VarPatternSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D39 RID: 11577 RVA: 0x000EF57F File Offset: 0x000ED77F
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitRecursivePattern(RecursivePatternSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D3A RID: 11578 RVA: 0x000EF588 File Offset: 0x000ED788
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitPositionalPatternClause(PositionalPatternClauseSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D3B RID: 11579 RVA: 0x000EF591 File Offset: 0x000ED791
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitPropertyPatternClause(PropertyPatternClauseSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D3C RID: 11580 RVA: 0x000EF59A File Offset: 0x000ED79A
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitSubpattern(SubpatternSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D3D RID: 11581 RVA: 0x000EF5A3 File Offset: 0x000ED7A3
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitConstantPattern(ConstantPatternSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D3E RID: 11582 RVA: 0x000EF5AC File Offset: 0x000ED7AC
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitParenthesizedPattern(ParenthesizedPatternSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D3F RID: 11583 RVA: 0x000EF5B5 File Offset: 0x000ED7B5
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitRelationalPattern(RelationalPatternSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D40 RID: 11584 RVA: 0x000EF5BE File Offset: 0x000ED7BE
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitTypePattern(TypePatternSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D41 RID: 11585 RVA: 0x000EF5C7 File Offset: 0x000ED7C7
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitBinaryPattern(BinaryPatternSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D42 RID: 11586 RVA: 0x000EF5D0 File Offset: 0x000ED7D0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitUnaryPattern(UnaryPatternSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D43 RID: 11587 RVA: 0x000EF5D9 File Offset: 0x000ED7D9
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitInterpolatedStringText(InterpolatedStringTextSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D44 RID: 11588 RVA: 0x000EF5E2 File Offset: 0x000ED7E2
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitInterpolation(InterpolationSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D45 RID: 11589 RVA: 0x000EF5EB File Offset: 0x000ED7EB
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitInterpolationAlignmentClause(InterpolationAlignmentClauseSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D46 RID: 11590 RVA: 0x000EF5F4 File Offset: 0x000ED7F4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitInterpolationFormatClause(InterpolationFormatClauseSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D47 RID: 11591 RVA: 0x000EF5FD File Offset: 0x000ED7FD
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitGlobalStatement(GlobalStatementSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D48 RID: 11592 RVA: 0x000EF606 File Offset: 0x000ED806
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitBlock(BlockSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D49 RID: 11593 RVA: 0x000EF60F File Offset: 0x000ED80F
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitLocalFunctionStatement(LocalFunctionStatementSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D4A RID: 11594 RVA: 0x000EF618 File Offset: 0x000ED818
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D4B RID: 11595 RVA: 0x000EF621 File Offset: 0x000ED821
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitVariableDeclaration(VariableDeclarationSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D4C RID: 11596 RVA: 0x000EF62A File Offset: 0x000ED82A
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitVariableDeclarator(VariableDeclaratorSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D4D RID: 11597 RVA: 0x000EF633 File Offset: 0x000ED833
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitEqualsValueClause(EqualsValueClauseSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D4E RID: 11598 RVA: 0x000EF63C File Offset: 0x000ED83C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitSingleVariableDesignation(SingleVariableDesignationSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D4F RID: 11599 RVA: 0x000EF645 File Offset: 0x000ED845
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitDiscardDesignation(DiscardDesignationSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D50 RID: 11600 RVA: 0x000EF64E File Offset: 0x000ED84E
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitParenthesizedVariableDesignation(ParenthesizedVariableDesignationSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D51 RID: 11601 RVA: 0x000EF657 File Offset: 0x000ED857
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitExpressionStatement(ExpressionStatementSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D52 RID: 11602 RVA: 0x000EF660 File Offset: 0x000ED860
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitEmptyStatement(EmptyStatementSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D53 RID: 11603 RVA: 0x000EF669 File Offset: 0x000ED869
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitLabeledStatement(LabeledStatementSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D54 RID: 11604 RVA: 0x000EF672 File Offset: 0x000ED872
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitGotoStatement(GotoStatementSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D55 RID: 11605 RVA: 0x000EF67B File Offset: 0x000ED87B
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitBreakStatement(BreakStatementSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D56 RID: 11606 RVA: 0x000EF684 File Offset: 0x000ED884
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitContinueStatement(ContinueStatementSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D57 RID: 11607 RVA: 0x000EF68D File Offset: 0x000ED88D
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitReturnStatement(ReturnStatementSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D58 RID: 11608 RVA: 0x000EF696 File Offset: 0x000ED896
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitThrowStatement(ThrowStatementSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D59 RID: 11609 RVA: 0x000EF69F File Offset: 0x000ED89F
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitYieldStatement(YieldStatementSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D5A RID: 11610 RVA: 0x000EF6A8 File Offset: 0x000ED8A8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitWhileStatement(WhileStatementSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D5B RID: 11611 RVA: 0x000EF6B1 File Offset: 0x000ED8B1
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitDoStatement(DoStatementSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D5C RID: 11612 RVA: 0x000EF6BA File Offset: 0x000ED8BA
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitForStatement(ForStatementSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D5D RID: 11613 RVA: 0x000EF6C3 File Offset: 0x000ED8C3
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitForEachStatement(ForEachStatementSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D5E RID: 11614 RVA: 0x000EF6CC File Offset: 0x000ED8CC
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitForEachVariableStatement(ForEachVariableStatementSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D5F RID: 11615 RVA: 0x000EF6D5 File Offset: 0x000ED8D5
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitUsingStatement(UsingStatementSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D60 RID: 11616 RVA: 0x000EF6DE File Offset: 0x000ED8DE
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitFixedStatement(FixedStatementSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D61 RID: 11617 RVA: 0x000EF6E7 File Offset: 0x000ED8E7
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitCheckedStatement(CheckedStatementSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D62 RID: 11618 RVA: 0x000EF6F0 File Offset: 0x000ED8F0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitUnsafeStatement(UnsafeStatementSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D63 RID: 11619 RVA: 0x000EF6F9 File Offset: 0x000ED8F9
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitLockStatement(LockStatementSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D64 RID: 11620 RVA: 0x000EF702 File Offset: 0x000ED902
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitIfStatement(IfStatementSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D65 RID: 11621 RVA: 0x000EF70B File Offset: 0x000ED90B
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitElseClause(ElseClauseSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D66 RID: 11622 RVA: 0x000EF714 File Offset: 0x000ED914
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitSwitchStatement(SwitchStatementSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D67 RID: 11623 RVA: 0x000EF71D File Offset: 0x000ED91D
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitSwitchSection(SwitchSectionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D68 RID: 11624 RVA: 0x000EF726 File Offset: 0x000ED926
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitCasePatternSwitchLabel(CasePatternSwitchLabelSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D69 RID: 11625 RVA: 0x000EF72F File Offset: 0x000ED92F
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitCaseSwitchLabel(CaseSwitchLabelSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D6A RID: 11626 RVA: 0x000EF738 File Offset: 0x000ED938
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitDefaultSwitchLabel(DefaultSwitchLabelSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D6B RID: 11627 RVA: 0x000EF741 File Offset: 0x000ED941
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitSwitchExpression(SwitchExpressionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D6C RID: 11628 RVA: 0x000EF74A File Offset: 0x000ED94A
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitSwitchExpressionArm(SwitchExpressionArmSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D6D RID: 11629 RVA: 0x000EF753 File Offset: 0x000ED953
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitTryStatement(TryStatementSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D6E RID: 11630 RVA: 0x000EF75C File Offset: 0x000ED95C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitCatchClause(CatchClauseSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D6F RID: 11631 RVA: 0x000EF765 File Offset: 0x000ED965
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitCatchDeclaration(CatchDeclarationSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D70 RID: 11632 RVA: 0x000EF76E File Offset: 0x000ED96E
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitCatchFilterClause(CatchFilterClauseSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D71 RID: 11633 RVA: 0x000EF777 File Offset: 0x000ED977
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitFinallyClause(FinallyClauseSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D72 RID: 11634 RVA: 0x000EF780 File Offset: 0x000ED980
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitCompilationUnit(CompilationUnitSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D73 RID: 11635 RVA: 0x000EF789 File Offset: 0x000ED989
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitExternAliasDirective(ExternAliasDirectiveSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D74 RID: 11636 RVA: 0x000EF792 File Offset: 0x000ED992
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitUsingDirective(UsingDirectiveSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D75 RID: 11637 RVA: 0x000EF79B File Offset: 0x000ED99B
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D76 RID: 11638 RVA: 0x000EF7A4 File Offset: 0x000ED9A4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitAttributeList(AttributeListSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D77 RID: 11639 RVA: 0x000EF7AD File Offset: 0x000ED9AD
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitAttributeTargetSpecifier(AttributeTargetSpecifierSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D78 RID: 11640 RVA: 0x000EF7B6 File Offset: 0x000ED9B6
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitAttribute(AttributeSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D79 RID: 11641 RVA: 0x000EF7BF File Offset: 0x000ED9BF
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitAttributeArgumentList(AttributeArgumentListSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D7A RID: 11642 RVA: 0x000EF7C8 File Offset: 0x000ED9C8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitAttributeArgument(AttributeArgumentSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D7B RID: 11643 RVA: 0x000EF7D1 File Offset: 0x000ED9D1
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitNameEquals(NameEqualsSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D7C RID: 11644 RVA: 0x000EF7DA File Offset: 0x000ED9DA
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitTypeParameterList(TypeParameterListSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D7D RID: 11645 RVA: 0x000EF7E3 File Offset: 0x000ED9E3
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitTypeParameter(TypeParameterSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D7E RID: 11646 RVA: 0x000EF7EC File Offset: 0x000ED9EC
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitClassDeclaration(ClassDeclarationSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D7F RID: 11647 RVA: 0x000EF7F5 File Offset: 0x000ED9F5
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitStructDeclaration(StructDeclarationSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D80 RID: 11648 RVA: 0x000EF7FE File Offset: 0x000ED9FE
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitInterfaceDeclaration(InterfaceDeclarationSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D81 RID: 11649 RVA: 0x000EF807 File Offset: 0x000EDA07
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitRecordDeclaration(RecordDeclarationSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D82 RID: 11650 RVA: 0x000EF810 File Offset: 0x000EDA10
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitEnumDeclaration(EnumDeclarationSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D83 RID: 11651 RVA: 0x000EF819 File Offset: 0x000EDA19
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitDelegateDeclaration(DelegateDeclarationSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D84 RID: 11652 RVA: 0x000EF822 File Offset: 0x000EDA22
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitEnumMemberDeclaration(EnumMemberDeclarationSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D85 RID: 11653 RVA: 0x000EF82B File Offset: 0x000EDA2B
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitBaseList(BaseListSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D86 RID: 11654 RVA: 0x000EF834 File Offset: 0x000EDA34
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitSimpleBaseType(SimpleBaseTypeSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D87 RID: 11655 RVA: 0x000EF83D File Offset: 0x000EDA3D
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitPrimaryConstructorBaseType(PrimaryConstructorBaseTypeSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D88 RID: 11656 RVA: 0x000EF846 File Offset: 0x000EDA46
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitTypeParameterConstraintClause(TypeParameterConstraintClauseSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D89 RID: 11657 RVA: 0x000EF84F File Offset: 0x000EDA4F
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitConstructorConstraint(ConstructorConstraintSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D8A RID: 11658 RVA: 0x000EF858 File Offset: 0x000EDA58
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitClassOrStructConstraint(ClassOrStructConstraintSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D8B RID: 11659 RVA: 0x000EF861 File Offset: 0x000EDA61
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitTypeConstraint(TypeConstraintSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D8C RID: 11660 RVA: 0x000EF86A File Offset: 0x000EDA6A
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitDefaultConstraint(DefaultConstraintSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D8D RID: 11661 RVA: 0x000EF873 File Offset: 0x000EDA73
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitFieldDeclaration(FieldDeclarationSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D8E RID: 11662 RVA: 0x000EF87C File Offset: 0x000EDA7C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitEventFieldDeclaration(EventFieldDeclarationSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D8F RID: 11663 RVA: 0x000EF885 File Offset: 0x000EDA85
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitExplicitInterfaceSpecifier(ExplicitInterfaceSpecifierSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D90 RID: 11664 RVA: 0x000EF88E File Offset: 0x000EDA8E
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitMethodDeclaration(MethodDeclarationSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D91 RID: 11665 RVA: 0x000EF897 File Offset: 0x000EDA97
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitOperatorDeclaration(OperatorDeclarationSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D92 RID: 11666 RVA: 0x000EF8A0 File Offset: 0x000EDAA0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitConversionOperatorDeclaration(ConversionOperatorDeclarationSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D93 RID: 11667 RVA: 0x000EF8A9 File Offset: 0x000EDAA9
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitConstructorDeclaration(ConstructorDeclarationSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D94 RID: 11668 RVA: 0x000EF8B2 File Offset: 0x000EDAB2
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitConstructorInitializer(ConstructorInitializerSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D95 RID: 11669 RVA: 0x000EF8BB File Offset: 0x000EDABB
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitDestructorDeclaration(DestructorDeclarationSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D96 RID: 11670 RVA: 0x000EF8C4 File Offset: 0x000EDAC4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitPropertyDeclaration(PropertyDeclarationSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D97 RID: 11671 RVA: 0x000EF8CD File Offset: 0x000EDACD
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitArrowExpressionClause(ArrowExpressionClauseSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D98 RID: 11672 RVA: 0x000EF8D6 File Offset: 0x000EDAD6
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitEventDeclaration(EventDeclarationSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D99 RID: 11673 RVA: 0x000EF8DF File Offset: 0x000EDADF
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitIndexerDeclaration(IndexerDeclarationSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D9A RID: 11674 RVA: 0x000EF8E8 File Offset: 0x000EDAE8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitAccessorList(AccessorListSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D9B RID: 11675 RVA: 0x000EF8F1 File Offset: 0x000EDAF1
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitAccessorDeclaration(AccessorDeclarationSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D9C RID: 11676 RVA: 0x000EF8FA File Offset: 0x000EDAFA
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitParameterList(ParameterListSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D9D RID: 11677 RVA: 0x000EF903 File Offset: 0x000EDB03
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitBracketedParameterList(BracketedParameterListSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D9E RID: 11678 RVA: 0x000EF90C File Offset: 0x000EDB0C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitParameter(ParameterSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002D9F RID: 11679 RVA: 0x000EF915 File Offset: 0x000EDB15
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitFunctionPointerParameter(FunctionPointerParameterSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002DA0 RID: 11680 RVA: 0x000EF91E File Offset: 0x000EDB1E
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitIncompleteMember(IncompleteMemberSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002DA1 RID: 11681 RVA: 0x000EF927 File Offset: 0x000EDB27
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitSkippedTokensTrivia(SkippedTokensTriviaSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002DA2 RID: 11682 RVA: 0x000EF930 File Offset: 0x000EDB30
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitDocumentationCommentTrivia(DocumentationCommentTriviaSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002DA3 RID: 11683 RVA: 0x000EF939 File Offset: 0x000EDB39
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitTypeCref(TypeCrefSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002DA4 RID: 11684 RVA: 0x000EF942 File Offset: 0x000EDB42
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitQualifiedCref(QualifiedCrefSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002DA5 RID: 11685 RVA: 0x000EF94B File Offset: 0x000EDB4B
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitNameMemberCref(NameMemberCrefSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002DA6 RID: 11686 RVA: 0x000EF954 File Offset: 0x000EDB54
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitIndexerMemberCref(IndexerMemberCrefSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002DA7 RID: 11687 RVA: 0x000EF95D File Offset: 0x000EDB5D
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitOperatorMemberCref(OperatorMemberCrefSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002DA8 RID: 11688 RVA: 0x000EF966 File Offset: 0x000EDB66
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitConversionOperatorMemberCref(ConversionOperatorMemberCrefSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002DA9 RID: 11689 RVA: 0x000EF96F File Offset: 0x000EDB6F
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitCrefParameterList(CrefParameterListSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002DAA RID: 11690 RVA: 0x000EF978 File Offset: 0x000EDB78
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitCrefBracketedParameterList(CrefBracketedParameterListSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002DAB RID: 11691 RVA: 0x000EF981 File Offset: 0x000EDB81
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitCrefParameter(CrefParameterSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002DAC RID: 11692 RVA: 0x000EF98A File Offset: 0x000EDB8A
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitXmlElement(XmlElementSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002DAD RID: 11693 RVA: 0x000EF993 File Offset: 0x000EDB93
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitXmlElementStartTag(XmlElementStartTagSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002DAE RID: 11694 RVA: 0x000EF99C File Offset: 0x000EDB9C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitXmlElementEndTag(XmlElementEndTagSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002DAF RID: 11695 RVA: 0x000EF9A5 File Offset: 0x000EDBA5
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitXmlEmptyElement(XmlEmptyElementSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002DB0 RID: 11696 RVA: 0x000EF9AE File Offset: 0x000EDBAE
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitXmlName(XmlNameSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002DB1 RID: 11697 RVA: 0x000EF9B7 File Offset: 0x000EDBB7
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitXmlPrefix(XmlPrefixSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002DB2 RID: 11698 RVA: 0x000EF9C0 File Offset: 0x000EDBC0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitXmlTextAttribute(XmlTextAttributeSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002DB3 RID: 11699 RVA: 0x000EF9C9 File Offset: 0x000EDBC9
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitXmlCrefAttribute(XmlCrefAttributeSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002DB4 RID: 11700 RVA: 0x000EF9D2 File Offset: 0x000EDBD2
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitXmlNameAttribute(XmlNameAttributeSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002DB5 RID: 11701 RVA: 0x000EF9DB File Offset: 0x000EDBDB
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitXmlText(XmlTextSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002DB6 RID: 11702 RVA: 0x000EF9E4 File Offset: 0x000EDBE4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitXmlCDataSection(XmlCDataSectionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002DB7 RID: 11703 RVA: 0x000EF9ED File Offset: 0x000EDBED
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitXmlProcessingInstruction(XmlProcessingInstructionSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002DB8 RID: 11704 RVA: 0x000EF9F6 File Offset: 0x000EDBF6
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitXmlComment(XmlCommentSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002DB9 RID: 11705 RVA: 0x000EF9FF File Offset: 0x000EDBFF
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitIfDirectiveTrivia(IfDirectiveTriviaSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002DBA RID: 11706 RVA: 0x000EFA08 File Offset: 0x000EDC08
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitElifDirectiveTrivia(ElifDirectiveTriviaSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002DBB RID: 11707 RVA: 0x000EFA11 File Offset: 0x000EDC11
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitElseDirectiveTrivia(ElseDirectiveTriviaSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002DBC RID: 11708 RVA: 0x000EFA1A File Offset: 0x000EDC1A
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitEndIfDirectiveTrivia(EndIfDirectiveTriviaSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002DBD RID: 11709 RVA: 0x000EFA23 File Offset: 0x000EDC23
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitRegionDirectiveTrivia(RegionDirectiveTriviaSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002DBE RID: 11710 RVA: 0x000EFA2C File Offset: 0x000EDC2C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitEndRegionDirectiveTrivia(EndRegionDirectiveTriviaSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002DBF RID: 11711 RVA: 0x000EFA35 File Offset: 0x000EDC35
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitErrorDirectiveTrivia(ErrorDirectiveTriviaSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002DC0 RID: 11712 RVA: 0x000EFA3E File Offset: 0x000EDC3E
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitWarningDirectiveTrivia(WarningDirectiveTriviaSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002DC1 RID: 11713 RVA: 0x000EFA47 File Offset: 0x000EDC47
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitBadDirectiveTrivia(BadDirectiveTriviaSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002DC2 RID: 11714 RVA: 0x000EFA50 File Offset: 0x000EDC50
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitDefineDirectiveTrivia(DefineDirectiveTriviaSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002DC3 RID: 11715 RVA: 0x000EFA59 File Offset: 0x000EDC59
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitUndefDirectiveTrivia(UndefDirectiveTriviaSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002DC4 RID: 11716 RVA: 0x000EFA62 File Offset: 0x000EDC62
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitLineDirectiveTrivia(LineDirectiveTriviaSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002DC5 RID: 11717 RVA: 0x000EFA6B File Offset: 0x000EDC6B
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitPragmaWarningDirectiveTrivia(PragmaWarningDirectiveTriviaSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002DC6 RID: 11718 RVA: 0x000EFA74 File Offset: 0x000EDC74
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitPragmaChecksumDirectiveTrivia(PragmaChecksumDirectiveTriviaSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002DC7 RID: 11719 RVA: 0x000EFA7D File Offset: 0x000EDC7D
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitReferenceDirectiveTrivia(ReferenceDirectiveTriviaSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002DC8 RID: 11720 RVA: 0x000EFA86 File Offset: 0x000EDC86
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitLoadDirectiveTrivia(LoadDirectiveTriviaSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002DC9 RID: 11721 RVA: 0x000EFA8F File Offset: 0x000EDC8F
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitShebangDirectiveTrivia(ShebangDirectiveTriviaSyntax node)
		{
			return this.DefaultVisit(node);
		}

		// Token: 0x06002DCA RID: 11722 RVA: 0x000EFA98 File Offset: 0x000EDC98
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual TResult VisitNullableDirectiveTrivia(NullableDirectiveTriviaSyntax node)
		{
			return this.DefaultVisit(node);
		}
	}
}
