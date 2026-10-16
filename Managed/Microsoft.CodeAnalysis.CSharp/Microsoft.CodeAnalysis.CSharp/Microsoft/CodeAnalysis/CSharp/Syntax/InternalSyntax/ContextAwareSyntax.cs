using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000369 RID: 873
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class ContextAwareSyntax
	{
		// Token: 0x06004B49 RID: 19273 RVA: 0x001380E0 File Offset: 0x001362E0
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public GlobalStatementSyntax GlobalStatement(StatementSyntax statement)
		{
			return this.GlobalStatement(default(SyntaxList<AttributeListSyntax>), default(SyntaxList<SyntaxToken>), statement);
		}

		// Token: 0x06004B4A RID: 19274 RVA: 0x00138106 File Offset: 0x00136306
		public ContextAwareSyntax(SyntaxFactoryContext context)
		{
			this.context = context;
		}

		// Token: 0x06004B4B RID: 19275 RVA: 0x00138118 File Offset: 0x00136318
		public IdentifierNameSyntax IdentifierName(SyntaxToken identifier)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8616, identifier, this.context, out num);
			if (greenNode != null)
			{
				return (IdentifierNameSyntax)greenNode;
			}
			IdentifierNameSyntax identifierNameSyntax = new IdentifierNameSyntax(SyntaxKind.IdentifierName, identifier, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(identifierNameSyntax, num);
			}
			return identifierNameSyntax;
		}

		// Token: 0x06004B4C RID: 19276 RVA: 0x00138164 File Offset: 0x00136364
		public QualifiedNameSyntax QualifiedName(NameSyntax left, SyntaxToken dotToken, SimpleNameSyntax right)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8617, left, dotToken, right, this.context, out num);
			if (greenNode != null)
			{
				return (QualifiedNameSyntax)greenNode;
			}
			QualifiedNameSyntax qualifiedNameSyntax = new QualifiedNameSyntax(SyntaxKind.QualifiedName, left, dotToken, right, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(qualifiedNameSyntax, num);
			}
			return qualifiedNameSyntax;
		}

		// Token: 0x06004B4D RID: 19277 RVA: 0x001381B4 File Offset: 0x001363B4
		public GenericNameSyntax GenericName(SyntaxToken identifier, TypeArgumentListSyntax typeArgumentList)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8618, identifier, typeArgumentList, this.context, out num);
			if (greenNode != null)
			{
				return (GenericNameSyntax)greenNode;
			}
			GenericNameSyntax genericNameSyntax = new GenericNameSyntax(SyntaxKind.GenericName, identifier, typeArgumentList, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(genericNameSyntax, num);
			}
			return genericNameSyntax;
		}

		// Token: 0x06004B4E RID: 19278 RVA: 0x00138200 File Offset: 0x00136400
		public TypeArgumentListSyntax TypeArgumentList(SyntaxToken lessThanToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<TypeSyntax> arguments, SyntaxToken greaterThanToken)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8619, lessThanToken, arguments.Node, greaterThanToken, this.context, out num);
			if (greenNode != null)
			{
				return (TypeArgumentListSyntax)greenNode;
			}
			TypeArgumentListSyntax typeArgumentListSyntax = new TypeArgumentListSyntax(SyntaxKind.TypeArgumentList, lessThanToken, arguments.Node, greaterThanToken, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(typeArgumentListSyntax, num);
			}
			return typeArgumentListSyntax;
		}

		// Token: 0x06004B4F RID: 19279 RVA: 0x0013825C File Offset: 0x0013645C
		public AliasQualifiedNameSyntax AliasQualifiedName(IdentifierNameSyntax alias, SyntaxToken colonColonToken, SimpleNameSyntax name)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8620, alias, colonColonToken, name, this.context, out num);
			if (greenNode != null)
			{
				return (AliasQualifiedNameSyntax)greenNode;
			}
			AliasQualifiedNameSyntax aliasQualifiedNameSyntax = new AliasQualifiedNameSyntax(SyntaxKind.AliasQualifiedName, alias, colonColonToken, name, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(aliasQualifiedNameSyntax, num);
			}
			return aliasQualifiedNameSyntax;
		}

		// Token: 0x06004B50 RID: 19280 RVA: 0x001382AC File Offset: 0x001364AC
		public PredefinedTypeSyntax PredefinedType(SyntaxToken keyword)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8621, keyword, this.context, out num);
			if (greenNode != null)
			{
				return (PredefinedTypeSyntax)greenNode;
			}
			PredefinedTypeSyntax predefinedTypeSyntax = new PredefinedTypeSyntax(SyntaxKind.PredefinedType, keyword, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(predefinedTypeSyntax, num);
			}
			return predefinedTypeSyntax;
		}

		// Token: 0x06004B51 RID: 19281 RVA: 0x001382F8 File Offset: 0x001364F8
		public ArrayTypeSyntax ArrayType(TypeSyntax elementType, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<ArrayRankSpecifierSyntax> rankSpecifiers)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8622, elementType, rankSpecifiers.Node, this.context, out num);
			if (greenNode != null)
			{
				return (ArrayTypeSyntax)greenNode;
			}
			ArrayTypeSyntax arrayTypeSyntax = new ArrayTypeSyntax(SyntaxKind.ArrayType, elementType, rankSpecifiers.Node, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(arrayTypeSyntax, num);
			}
			return arrayTypeSyntax;
		}

		// Token: 0x06004B52 RID: 19282 RVA: 0x00138350 File Offset: 0x00136550
		public ArrayRankSpecifierSyntax ArrayRankSpecifier(SyntaxToken openBracketToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<ExpressionSyntax> sizes, SyntaxToken closeBracketToken)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8623, openBracketToken, sizes.Node, closeBracketToken, this.context, out num);
			if (greenNode != null)
			{
				return (ArrayRankSpecifierSyntax)greenNode;
			}
			ArrayRankSpecifierSyntax arrayRankSpecifierSyntax = new ArrayRankSpecifierSyntax(SyntaxKind.ArrayRankSpecifier, openBracketToken, sizes.Node, closeBracketToken, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(arrayRankSpecifierSyntax, num);
			}
			return arrayRankSpecifierSyntax;
		}

		// Token: 0x06004B53 RID: 19283 RVA: 0x001383AC File Offset: 0x001365AC
		public PointerTypeSyntax PointerType(TypeSyntax elementType, SyntaxToken asteriskToken)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8624, elementType, asteriskToken, this.context, out num);
			if (greenNode != null)
			{
				return (PointerTypeSyntax)greenNode;
			}
			PointerTypeSyntax pointerTypeSyntax = new PointerTypeSyntax(SyntaxKind.PointerType, elementType, asteriskToken, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(pointerTypeSyntax, num);
			}
			return pointerTypeSyntax;
		}

		// Token: 0x06004B54 RID: 19284 RVA: 0x001383F7 File Offset: 0x001365F7
		public FunctionPointerTypeSyntax FunctionPointerType(SyntaxToken delegateKeyword, SyntaxToken asteriskToken, [global::System.Runtime.CompilerServices.Nullable(2)] FunctionPointerCallingConventionSyntax callingConvention, FunctionPointerParameterListSyntax parameterList)
		{
			return new FunctionPointerTypeSyntax(SyntaxKind.FunctionPointerType, delegateKeyword, asteriskToken, callingConvention, parameterList, this.context);
		}

		// Token: 0x06004B55 RID: 19285 RVA: 0x00138410 File Offset: 0x00136610
		public FunctionPointerParameterListSyntax FunctionPointerParameterList(SyntaxToken lessThanToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<FunctionPointerParameterSyntax> parameters, SyntaxToken greaterThanToken)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(9058, lessThanToken, parameters.Node, greaterThanToken, this.context, out num);
			if (greenNode != null)
			{
				return (FunctionPointerParameterListSyntax)greenNode;
			}
			FunctionPointerParameterListSyntax functionPointerParameterListSyntax = new FunctionPointerParameterListSyntax(SyntaxKind.FunctionPointerParameterList, lessThanToken, parameters.Node, greaterThanToken, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(functionPointerParameterListSyntax, num);
			}
			return functionPointerParameterListSyntax;
		}

		// Token: 0x06004B56 RID: 19286 RVA: 0x0013846C File Offset: 0x0013666C
		public FunctionPointerCallingConventionSyntax FunctionPointerCallingConvention(SyntaxToken managedOrUnmanagedKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] FunctionPointerUnmanagedCallingConventionListSyntax unmanagedCallingConventionList)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(9059, managedOrUnmanagedKeyword, unmanagedCallingConventionList, this.context, out num);
			if (greenNode != null)
			{
				return (FunctionPointerCallingConventionSyntax)greenNode;
			}
			FunctionPointerCallingConventionSyntax functionPointerCallingConventionSyntax = new FunctionPointerCallingConventionSyntax(SyntaxKind.FunctionPointerCallingConvention, managedOrUnmanagedKeyword, unmanagedCallingConventionList, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(functionPointerCallingConventionSyntax, num);
			}
			return functionPointerCallingConventionSyntax;
		}

		// Token: 0x06004B57 RID: 19287 RVA: 0x001384B8 File Offset: 0x001366B8
		public FunctionPointerUnmanagedCallingConventionListSyntax FunctionPointerUnmanagedCallingConventionList(SyntaxToken openBracketToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<FunctionPointerUnmanagedCallingConventionSyntax> callingConventions, SyntaxToken closeBracketToken)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(9066, openBracketToken, callingConventions.Node, closeBracketToken, this.context, out num);
			if (greenNode != null)
			{
				return (FunctionPointerUnmanagedCallingConventionListSyntax)greenNode;
			}
			FunctionPointerUnmanagedCallingConventionListSyntax functionPointerUnmanagedCallingConventionListSyntax = new FunctionPointerUnmanagedCallingConventionListSyntax(SyntaxKind.FunctionPointerUnmanagedCallingConventionList, openBracketToken, callingConventions.Node, closeBracketToken, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(functionPointerUnmanagedCallingConventionListSyntax, num);
			}
			return functionPointerUnmanagedCallingConventionListSyntax;
		}

		// Token: 0x06004B58 RID: 19288 RVA: 0x00138514 File Offset: 0x00136714
		public FunctionPointerUnmanagedCallingConventionSyntax FunctionPointerUnmanagedCallingConvention(SyntaxToken name)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(9067, name, this.context, out num);
			if (greenNode != null)
			{
				return (FunctionPointerUnmanagedCallingConventionSyntax)greenNode;
			}
			FunctionPointerUnmanagedCallingConventionSyntax functionPointerUnmanagedCallingConventionSyntax = new FunctionPointerUnmanagedCallingConventionSyntax(SyntaxKind.FunctionPointerUnmanagedCallingConvention, name, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(functionPointerUnmanagedCallingConventionSyntax, num);
			}
			return functionPointerUnmanagedCallingConventionSyntax;
		}

		// Token: 0x06004B59 RID: 19289 RVA: 0x00138560 File Offset: 0x00136760
		public NullableTypeSyntax NullableType(TypeSyntax elementType, SyntaxToken questionToken)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8625, elementType, questionToken, this.context, out num);
			if (greenNode != null)
			{
				return (NullableTypeSyntax)greenNode;
			}
			NullableTypeSyntax nullableTypeSyntax = new NullableTypeSyntax(SyntaxKind.NullableType, elementType, questionToken, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(nullableTypeSyntax, num);
			}
			return nullableTypeSyntax;
		}

		// Token: 0x06004B5A RID: 19290 RVA: 0x001385AC File Offset: 0x001367AC
		public TupleTypeSyntax TupleType(SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<TupleElementSyntax> elements, SyntaxToken closeParenToken)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8924, openParenToken, elements.Node, closeParenToken, this.context, out num);
			if (greenNode != null)
			{
				return (TupleTypeSyntax)greenNode;
			}
			TupleTypeSyntax tupleTypeSyntax = new TupleTypeSyntax(SyntaxKind.TupleType, openParenToken, elements.Node, closeParenToken, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(tupleTypeSyntax, num);
			}
			return tupleTypeSyntax;
		}

		// Token: 0x06004B5B RID: 19291 RVA: 0x00138608 File Offset: 0x00136808
		public TupleElementSyntax TupleElement(TypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken identifier)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8925, type, identifier, this.context, out num);
			if (greenNode != null)
			{
				return (TupleElementSyntax)greenNode;
			}
			TupleElementSyntax tupleElementSyntax = new TupleElementSyntax(SyntaxKind.TupleElement, type, identifier, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(tupleElementSyntax, num);
			}
			return tupleElementSyntax;
		}

		// Token: 0x06004B5C RID: 19292 RVA: 0x00138654 File Offset: 0x00136854
		public OmittedTypeArgumentSyntax OmittedTypeArgument(SyntaxToken omittedTypeArgumentToken)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8626, omittedTypeArgumentToken, this.context, out num);
			if (greenNode != null)
			{
				return (OmittedTypeArgumentSyntax)greenNode;
			}
			OmittedTypeArgumentSyntax omittedTypeArgumentSyntax = new OmittedTypeArgumentSyntax(SyntaxKind.OmittedTypeArgument, omittedTypeArgumentToken, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(omittedTypeArgumentSyntax, num);
			}
			return omittedTypeArgumentSyntax;
		}

		// Token: 0x06004B5D RID: 19293 RVA: 0x001386A0 File Offset: 0x001368A0
		public RefTypeSyntax RefType(SyntaxToken refKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken readOnlyKeyword, TypeSyntax type)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(9051, refKeyword, readOnlyKeyword, type, this.context, out num);
			if (greenNode != null)
			{
				return (RefTypeSyntax)greenNode;
			}
			RefTypeSyntax refTypeSyntax = new RefTypeSyntax(SyntaxKind.RefType, refKeyword, readOnlyKeyword, type, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(refTypeSyntax, num);
			}
			return refTypeSyntax;
		}

		// Token: 0x06004B5E RID: 19294 RVA: 0x001386F0 File Offset: 0x001368F0
		public ParenthesizedExpressionSyntax ParenthesizedExpression(SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken closeParenToken)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8632, openParenToken, expression, closeParenToken, this.context, out num);
			if (greenNode != null)
			{
				return (ParenthesizedExpressionSyntax)greenNode;
			}
			ParenthesizedExpressionSyntax parenthesizedExpressionSyntax = new ParenthesizedExpressionSyntax(SyntaxKind.ParenthesizedExpression, openParenToken, expression, closeParenToken, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(parenthesizedExpressionSyntax, num);
			}
			return parenthesizedExpressionSyntax;
		}

		// Token: 0x06004B5F RID: 19295 RVA: 0x00138740 File Offset: 0x00136940
		public TupleExpressionSyntax TupleExpression(SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<ArgumentSyntax> arguments, SyntaxToken closeParenToken)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8926, openParenToken, arguments.Node, closeParenToken, this.context, out num);
			if (greenNode != null)
			{
				return (TupleExpressionSyntax)greenNode;
			}
			TupleExpressionSyntax tupleExpressionSyntax = new TupleExpressionSyntax(SyntaxKind.TupleExpression, openParenToken, arguments.Node, closeParenToken, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(tupleExpressionSyntax, num);
			}
			return tupleExpressionSyntax;
		}

		// Token: 0x06004B60 RID: 19296 RVA: 0x0013879C File Offset: 0x0013699C
		public PrefixUnaryExpressionSyntax PrefixUnaryExpression(SyntaxKind kind, SyntaxToken operatorToken, ExpressionSyntax operand)
		{
			if (kind - SyntaxKind.UnaryPlusExpression > 7 && kind != SyntaxKind.IndexExpression)
			{
				throw new ArgumentException("kind");
			}
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode((int)kind, operatorToken, operand, this.context, out num);
			if (greenNode != null)
			{
				return (PrefixUnaryExpressionSyntax)greenNode;
			}
			PrefixUnaryExpressionSyntax prefixUnaryExpressionSyntax = new PrefixUnaryExpressionSyntax(kind, operatorToken, operand, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(prefixUnaryExpressionSyntax, num);
			}
			return prefixUnaryExpressionSyntax;
		}

		// Token: 0x06004B61 RID: 19297 RVA: 0x001387FC File Offset: 0x001369FC
		public AwaitExpressionSyntax AwaitExpression(SyntaxToken awaitKeyword, ExpressionSyntax expression)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8740, awaitKeyword, expression, this.context, out num);
			if (greenNode != null)
			{
				return (AwaitExpressionSyntax)greenNode;
			}
			AwaitExpressionSyntax awaitExpressionSyntax = new AwaitExpressionSyntax(SyntaxKind.AwaitExpression, awaitKeyword, expression, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(awaitExpressionSyntax, num);
			}
			return awaitExpressionSyntax;
		}

		// Token: 0x06004B62 RID: 19298 RVA: 0x00138848 File Offset: 0x00136A48
		public PostfixUnaryExpressionSyntax PostfixUnaryExpression(SyntaxKind kind, ExpressionSyntax operand, SyntaxToken operatorToken)
		{
			if (kind - SyntaxKind.PostIncrementExpression > 1 && kind != SyntaxKind.SuppressNullableWarningExpression)
			{
				throw new ArgumentException("kind");
			}
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode((int)kind, operand, operatorToken, this.context, out num);
			if (greenNode != null)
			{
				return (PostfixUnaryExpressionSyntax)greenNode;
			}
			PostfixUnaryExpressionSyntax postfixUnaryExpressionSyntax = new PostfixUnaryExpressionSyntax(kind, operand, operatorToken, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(postfixUnaryExpressionSyntax, num);
			}
			return postfixUnaryExpressionSyntax;
		}

		// Token: 0x06004B63 RID: 19299 RVA: 0x001388A8 File Offset: 0x00136AA8
		public MemberAccessExpressionSyntax MemberAccessExpression(SyntaxKind kind, ExpressionSyntax expression, SyntaxToken operatorToken, SimpleNameSyntax name)
		{
			if (kind - SyntaxKind.SimpleMemberAccessExpression > 1)
			{
				throw new ArgumentException("kind");
			}
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode((int)kind, expression, operatorToken, name, this.context, out num);
			if (greenNode != null)
			{
				return (MemberAccessExpressionSyntax)greenNode;
			}
			MemberAccessExpressionSyntax memberAccessExpressionSyntax = new MemberAccessExpressionSyntax(kind, expression, operatorToken, name, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(memberAccessExpressionSyntax, num);
			}
			return memberAccessExpressionSyntax;
		}

		// Token: 0x06004B64 RID: 19300 RVA: 0x00138904 File Offset: 0x00136B04
		public ConditionalAccessExpressionSyntax ConditionalAccessExpression(ExpressionSyntax expression, SyntaxToken operatorToken, ExpressionSyntax whenNotNull)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8691, expression, operatorToken, whenNotNull, this.context, out num);
			if (greenNode != null)
			{
				return (ConditionalAccessExpressionSyntax)greenNode;
			}
			ConditionalAccessExpressionSyntax conditionalAccessExpressionSyntax = new ConditionalAccessExpressionSyntax(SyntaxKind.ConditionalAccessExpression, expression, operatorToken, whenNotNull, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(conditionalAccessExpressionSyntax, num);
			}
			return conditionalAccessExpressionSyntax;
		}

		// Token: 0x06004B65 RID: 19301 RVA: 0x00138954 File Offset: 0x00136B54
		public MemberBindingExpressionSyntax MemberBindingExpression(SyntaxToken operatorToken, SimpleNameSyntax name)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8707, operatorToken, name, this.context, out num);
			if (greenNode != null)
			{
				return (MemberBindingExpressionSyntax)greenNode;
			}
			MemberBindingExpressionSyntax memberBindingExpressionSyntax = new MemberBindingExpressionSyntax(SyntaxKind.MemberBindingExpression, operatorToken, name, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(memberBindingExpressionSyntax, num);
			}
			return memberBindingExpressionSyntax;
		}

		// Token: 0x06004B66 RID: 19302 RVA: 0x001389A0 File Offset: 0x00136BA0
		public ElementBindingExpressionSyntax ElementBindingExpression(BracketedArgumentListSyntax argumentList)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8708, argumentList, this.context, out num);
			if (greenNode != null)
			{
				return (ElementBindingExpressionSyntax)greenNode;
			}
			ElementBindingExpressionSyntax elementBindingExpressionSyntax = new ElementBindingExpressionSyntax(SyntaxKind.ElementBindingExpression, argumentList, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(elementBindingExpressionSyntax, num);
			}
			return elementBindingExpressionSyntax;
		}

		// Token: 0x06004B67 RID: 19303 RVA: 0x001389EC File Offset: 0x00136BEC
		public RangeExpressionSyntax RangeExpression([global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax leftOperand, SyntaxToken operatorToken, [global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax rightOperand)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8658, leftOperand, operatorToken, rightOperand, this.context, out num);
			if (greenNode != null)
			{
				return (RangeExpressionSyntax)greenNode;
			}
			RangeExpressionSyntax rangeExpressionSyntax = new RangeExpressionSyntax(SyntaxKind.RangeExpression, leftOperand, operatorToken, rightOperand, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(rangeExpressionSyntax, num);
			}
			return rangeExpressionSyntax;
		}

		// Token: 0x06004B68 RID: 19304 RVA: 0x00138A3C File Offset: 0x00136C3C
		public ImplicitElementAccessSyntax ImplicitElementAccess(BracketedArgumentListSyntax argumentList)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8656, argumentList, this.context, out num);
			if (greenNode != null)
			{
				return (ImplicitElementAccessSyntax)greenNode;
			}
			ImplicitElementAccessSyntax implicitElementAccessSyntax = new ImplicitElementAccessSyntax(SyntaxKind.ImplicitElementAccess, argumentList, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(implicitElementAccessSyntax, num);
			}
			return implicitElementAccessSyntax;
		}

		// Token: 0x06004B69 RID: 19305 RVA: 0x00138A88 File Offset: 0x00136C88
		public BinaryExpressionSyntax BinaryExpression(SyntaxKind kind, ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right)
		{
			if (kind - SyntaxKind.AddExpression > 20)
			{
				throw new ArgumentException("kind");
			}
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode((int)kind, left, operatorToken, right, this.context, out num);
			if (greenNode != null)
			{
				return (BinaryExpressionSyntax)greenNode;
			}
			BinaryExpressionSyntax binaryExpressionSyntax = new BinaryExpressionSyntax(kind, left, operatorToken, right, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(binaryExpressionSyntax, num);
			}
			return binaryExpressionSyntax;
		}

		// Token: 0x06004B6A RID: 19306 RVA: 0x00138AE8 File Offset: 0x00136CE8
		public AssignmentExpressionSyntax AssignmentExpression(SyntaxKind kind, ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right)
		{
			if (kind - SyntaxKind.SimpleAssignmentExpression > 11)
			{
				throw new ArgumentException("kind");
			}
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode((int)kind, left, operatorToken, right, this.context, out num);
			if (greenNode != null)
			{
				return (AssignmentExpressionSyntax)greenNode;
			}
			AssignmentExpressionSyntax assignmentExpressionSyntax = new AssignmentExpressionSyntax(kind, left, operatorToken, right, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(assignmentExpressionSyntax, num);
			}
			return assignmentExpressionSyntax;
		}

		// Token: 0x06004B6B RID: 19307 RVA: 0x00138B45 File Offset: 0x00136D45
		public ConditionalExpressionSyntax ConditionalExpression(ExpressionSyntax condition, SyntaxToken questionToken, ExpressionSyntax whenTrue, SyntaxToken colonToken, ExpressionSyntax whenFalse)
		{
			return new ConditionalExpressionSyntax(SyntaxKind.ConditionalExpression, condition, questionToken, whenTrue, colonToken, whenFalse, this.context);
		}

		// Token: 0x06004B6C RID: 19308 RVA: 0x00138B60 File Offset: 0x00136D60
		public ThisExpressionSyntax ThisExpression(SyntaxToken token)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8746, token, this.context, out num);
			if (greenNode != null)
			{
				return (ThisExpressionSyntax)greenNode;
			}
			ThisExpressionSyntax thisExpressionSyntax = new ThisExpressionSyntax(SyntaxKind.ThisExpression, token, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(thisExpressionSyntax, num);
			}
			return thisExpressionSyntax;
		}

		// Token: 0x06004B6D RID: 19309 RVA: 0x00138BAC File Offset: 0x00136DAC
		public BaseExpressionSyntax BaseExpression(SyntaxToken token)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8747, token, this.context, out num);
			if (greenNode != null)
			{
				return (BaseExpressionSyntax)greenNode;
			}
			BaseExpressionSyntax baseExpressionSyntax = new BaseExpressionSyntax(SyntaxKind.BaseExpression, token, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(baseExpressionSyntax, num);
			}
			return baseExpressionSyntax;
		}

		// Token: 0x06004B6E RID: 19310 RVA: 0x00138BF8 File Offset: 0x00136DF8
		public LiteralExpressionSyntax LiteralExpression(SyntaxKind kind, SyntaxToken token)
		{
			if (kind - SyntaxKind.ArgListExpression > 7)
			{
				throw new ArgumentException("kind");
			}
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode((int)kind, token, this.context, out num);
			if (greenNode != null)
			{
				return (LiteralExpressionSyntax)greenNode;
			}
			LiteralExpressionSyntax literalExpressionSyntax = new LiteralExpressionSyntax(kind, token, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(literalExpressionSyntax, num);
			}
			return literalExpressionSyntax;
		}

		// Token: 0x06004B6F RID: 19311 RVA: 0x00138C4E File Offset: 0x00136E4E
		public MakeRefExpressionSyntax MakeRefExpression(SyntaxToken keyword, SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken closeParenToken)
		{
			return new MakeRefExpressionSyntax(SyntaxKind.MakeRefExpression, keyword, openParenToken, expression, closeParenToken, this.context);
		}

		// Token: 0x06004B70 RID: 19312 RVA: 0x00138C65 File Offset: 0x00136E65
		public RefTypeExpressionSyntax RefTypeExpression(SyntaxToken keyword, SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken closeParenToken)
		{
			return new RefTypeExpressionSyntax(SyntaxKind.RefTypeExpression, keyword, openParenToken, expression, closeParenToken, this.context);
		}

		// Token: 0x06004B71 RID: 19313 RVA: 0x00138C7C File Offset: 0x00136E7C
		public RefValueExpressionSyntax RefValueExpression(SyntaxToken keyword, SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken comma, TypeSyntax type, SyntaxToken closeParenToken)
		{
			return new RefValueExpressionSyntax(SyntaxKind.RefValueExpression, keyword, openParenToken, expression, comma, type, closeParenToken, this.context);
		}

		// Token: 0x06004B72 RID: 19314 RVA: 0x00138C97 File Offset: 0x00136E97
		public CheckedExpressionSyntax CheckedExpression(SyntaxKind kind, SyntaxToken keyword, SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken closeParenToken)
		{
			if (kind - SyntaxKind.CheckedExpression > 1)
			{
				throw new ArgumentException("kind");
			}
			return new CheckedExpressionSyntax(kind, keyword, openParenToken, expression, closeParenToken, this.context);
		}

		// Token: 0x06004B73 RID: 19315 RVA: 0x00138CC0 File Offset: 0x00136EC0
		public DefaultExpressionSyntax DefaultExpression(SyntaxToken keyword, SyntaxToken openParenToken, TypeSyntax type, SyntaxToken closeParenToken)
		{
			return new DefaultExpressionSyntax(SyntaxKind.DefaultExpression, keyword, openParenToken, type, closeParenToken, this.context);
		}

		// Token: 0x06004B74 RID: 19316 RVA: 0x00138CD7 File Offset: 0x00136ED7
		public TypeOfExpressionSyntax TypeOfExpression(SyntaxToken keyword, SyntaxToken openParenToken, TypeSyntax type, SyntaxToken closeParenToken)
		{
			return new TypeOfExpressionSyntax(SyntaxKind.TypeOfExpression, keyword, openParenToken, type, closeParenToken, this.context);
		}

		// Token: 0x06004B75 RID: 19317 RVA: 0x00138CEE File Offset: 0x00136EEE
		public SizeOfExpressionSyntax SizeOfExpression(SyntaxToken keyword, SyntaxToken openParenToken, TypeSyntax type, SyntaxToken closeParenToken)
		{
			return new SizeOfExpressionSyntax(SyntaxKind.SizeOfExpression, keyword, openParenToken, type, closeParenToken, this.context);
		}

		// Token: 0x06004B76 RID: 19318 RVA: 0x00138D08 File Offset: 0x00136F08
		public InvocationExpressionSyntax InvocationExpression(ExpressionSyntax expression, ArgumentListSyntax argumentList)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8634, expression, argumentList, this.context, out num);
			if (greenNode != null)
			{
				return (InvocationExpressionSyntax)greenNode;
			}
			InvocationExpressionSyntax invocationExpressionSyntax = new InvocationExpressionSyntax(SyntaxKind.InvocationExpression, expression, argumentList, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(invocationExpressionSyntax, num);
			}
			return invocationExpressionSyntax;
		}

		// Token: 0x06004B77 RID: 19319 RVA: 0x00138D54 File Offset: 0x00136F54
		public ElementAccessExpressionSyntax ElementAccessExpression(ExpressionSyntax expression, BracketedArgumentListSyntax argumentList)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8635, expression, argumentList, this.context, out num);
			if (greenNode != null)
			{
				return (ElementAccessExpressionSyntax)greenNode;
			}
			ElementAccessExpressionSyntax elementAccessExpressionSyntax = new ElementAccessExpressionSyntax(SyntaxKind.ElementAccessExpression, expression, argumentList, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(elementAccessExpressionSyntax, num);
			}
			return elementAccessExpressionSyntax;
		}

		// Token: 0x06004B78 RID: 19320 RVA: 0x00138DA0 File Offset: 0x00136FA0
		public ArgumentListSyntax ArgumentList(SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<ArgumentSyntax> arguments, SyntaxToken closeParenToken)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8636, openParenToken, arguments.Node, closeParenToken, this.context, out num);
			if (greenNode != null)
			{
				return (ArgumentListSyntax)greenNode;
			}
			ArgumentListSyntax argumentListSyntax = new ArgumentListSyntax(SyntaxKind.ArgumentList, openParenToken, arguments.Node, closeParenToken, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(argumentListSyntax, num);
			}
			return argumentListSyntax;
		}

		// Token: 0x06004B79 RID: 19321 RVA: 0x00138DFC File Offset: 0x00136FFC
		public BracketedArgumentListSyntax BracketedArgumentList(SyntaxToken openBracketToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<ArgumentSyntax> arguments, SyntaxToken closeBracketToken)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8637, openBracketToken, arguments.Node, closeBracketToken, this.context, out num);
			if (greenNode != null)
			{
				return (BracketedArgumentListSyntax)greenNode;
			}
			BracketedArgumentListSyntax bracketedArgumentListSyntax = new BracketedArgumentListSyntax(SyntaxKind.BracketedArgumentList, openBracketToken, arguments.Node, closeBracketToken, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(bracketedArgumentListSyntax, num);
			}
			return bracketedArgumentListSyntax;
		}

		// Token: 0x06004B7A RID: 19322 RVA: 0x00138E58 File Offset: 0x00137058
		public ArgumentSyntax Argument([global::System.Runtime.CompilerServices.Nullable(2)] NameColonSyntax nameColon, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken refKindKeyword, ExpressionSyntax expression)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8638, nameColon, refKindKeyword, expression, this.context, out num);
			if (greenNode != null)
			{
				return (ArgumentSyntax)greenNode;
			}
			ArgumentSyntax argumentSyntax = new ArgumentSyntax(SyntaxKind.Argument, nameColon, refKindKeyword, expression, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(argumentSyntax, num);
			}
			return argumentSyntax;
		}

		// Token: 0x06004B7B RID: 19323 RVA: 0x00138EA8 File Offset: 0x001370A8
		public NameColonSyntax NameColon(IdentifierNameSyntax name, SyntaxToken colonToken)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8639, name, colonToken, this.context, out num);
			if (greenNode != null)
			{
				return (NameColonSyntax)greenNode;
			}
			NameColonSyntax nameColonSyntax = new NameColonSyntax(SyntaxKind.NameColon, name, colonToken, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(nameColonSyntax, num);
			}
			return nameColonSyntax;
		}

		// Token: 0x06004B7C RID: 19324 RVA: 0x00138EF4 File Offset: 0x001370F4
		public DeclarationExpressionSyntax DeclarationExpression(TypeSyntax type, VariableDesignationSyntax designation)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(9040, type, designation, this.context, out num);
			if (greenNode != null)
			{
				return (DeclarationExpressionSyntax)greenNode;
			}
			DeclarationExpressionSyntax declarationExpressionSyntax = new DeclarationExpressionSyntax(SyntaxKind.DeclarationExpression, type, designation, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(declarationExpressionSyntax, num);
			}
			return declarationExpressionSyntax;
		}

		// Token: 0x06004B7D RID: 19325 RVA: 0x00138F3F File Offset: 0x0013713F
		public CastExpressionSyntax CastExpression(SyntaxToken openParenToken, TypeSyntax type, SyntaxToken closeParenToken, ExpressionSyntax expression)
		{
			return new CastExpressionSyntax(SyntaxKind.CastExpression, openParenToken, type, closeParenToken, expression, this.context);
		}

		// Token: 0x06004B7E RID: 19326 RVA: 0x00138F56 File Offset: 0x00137156
		public AnonymousMethodExpressionSyntax AnonymousMethodExpression([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, SyntaxToken delegateKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] ParameterListSyntax parameterList, BlockSyntax block, [global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax expressionBody)
		{
			return new AnonymousMethodExpressionSyntax(SyntaxKind.AnonymousMethodExpression, modifiers.Node, delegateKeyword, parameterList, block, expressionBody, this.context);
		}

		// Token: 0x06004B7F RID: 19327 RVA: 0x00138F75 File Offset: 0x00137175
		public SimpleLambdaExpressionSyntax SimpleLambdaExpression([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, ParameterSyntax parameter, SyntaxToken arrowToken, [global::System.Runtime.CompilerServices.Nullable(2)] BlockSyntax block, [global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax expressionBody)
		{
			return new SimpleLambdaExpressionSyntax(SyntaxKind.SimpleLambdaExpression, attributeLists.Node, modifiers.Node, parameter, arrowToken, block, expressionBody, this.context);
		}

		// Token: 0x06004B80 RID: 19328 RVA: 0x00138F9C File Offset: 0x0013719C
		public RefExpressionSyntax RefExpression(SyntaxToken refKeyword, ExpressionSyntax expression)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(9050, refKeyword, expression, this.context, out num);
			if (greenNode != null)
			{
				return (RefExpressionSyntax)greenNode;
			}
			RefExpressionSyntax refExpressionSyntax = new RefExpressionSyntax(SyntaxKind.RefExpression, refKeyword, expression, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(refExpressionSyntax, num);
			}
			return refExpressionSyntax;
		}

		// Token: 0x06004B81 RID: 19329 RVA: 0x00138FE7 File Offset: 0x001371E7
		public ParenthesizedLambdaExpressionSyntax ParenthesizedLambdaExpression([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, ParameterListSyntax parameterList, SyntaxToken arrowToken, [global::System.Runtime.CompilerServices.Nullable(2)] BlockSyntax block, [global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax expressionBody)
		{
			return new ParenthesizedLambdaExpressionSyntax(SyntaxKind.ParenthesizedLambdaExpression, attributeLists.Node, modifiers.Node, parameterList, arrowToken, block, expressionBody, this.context);
		}

		// Token: 0x06004B82 RID: 19330 RVA: 0x00139010 File Offset: 0x00137210
		public InitializerExpressionSyntax InitializerExpression(SyntaxKind kind, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<ExpressionSyntax> expressions, SyntaxToken closeBraceToken)
		{
			if (kind - SyntaxKind.ObjectInitializerExpression > 2 && kind != SyntaxKind.ComplexElementInitializerExpression && kind != SyntaxKind.WithInitializerExpression)
			{
				throw new ArgumentException("kind");
			}
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode((int)kind, openBraceToken, expressions.Node, closeBraceToken, this.context, out num);
			if (greenNode != null)
			{
				return (InitializerExpressionSyntax)greenNode;
			}
			InitializerExpressionSyntax initializerExpressionSyntax = new InitializerExpressionSyntax(kind, openBraceToken, expressions.Node, closeBraceToken, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(initializerExpressionSyntax, num);
			}
			return initializerExpressionSyntax;
		}

		// Token: 0x06004B83 RID: 19331 RVA: 0x00139088 File Offset: 0x00137288
		public ImplicitObjectCreationExpressionSyntax ImplicitObjectCreationExpression(SyntaxToken newKeyword, ArgumentListSyntax argumentList, [global::System.Runtime.CompilerServices.Nullable(2)] InitializerExpressionSyntax initializer)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8659, newKeyword, argumentList, initializer, this.context, out num);
			if (greenNode != null)
			{
				return (ImplicitObjectCreationExpressionSyntax)greenNode;
			}
			ImplicitObjectCreationExpressionSyntax implicitObjectCreationExpressionSyntax = new ImplicitObjectCreationExpressionSyntax(SyntaxKind.ImplicitObjectCreationExpression, newKeyword, argumentList, initializer, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(implicitObjectCreationExpressionSyntax, num);
			}
			return implicitObjectCreationExpressionSyntax;
		}

		// Token: 0x06004B84 RID: 19332 RVA: 0x001390D5 File Offset: 0x001372D5
		public ObjectCreationExpressionSyntax ObjectCreationExpression(SyntaxToken newKeyword, TypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(2)] ArgumentListSyntax argumentList, [global::System.Runtime.CompilerServices.Nullable(2)] InitializerExpressionSyntax initializer)
		{
			return new ObjectCreationExpressionSyntax(SyntaxKind.ObjectCreationExpression, newKeyword, type, argumentList, initializer, this.context);
		}

		// Token: 0x06004B85 RID: 19333 RVA: 0x001390EC File Offset: 0x001372EC
		public WithExpressionSyntax WithExpression(ExpressionSyntax expression, SyntaxToken withKeyword, InitializerExpressionSyntax initializer)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(9061, expression, withKeyword, initializer, this.context, out num);
			if (greenNode != null)
			{
				return (WithExpressionSyntax)greenNode;
			}
			WithExpressionSyntax withExpressionSyntax = new WithExpressionSyntax(SyntaxKind.WithExpression, expression, withKeyword, initializer, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(withExpressionSyntax, num);
			}
			return withExpressionSyntax;
		}

		// Token: 0x06004B86 RID: 19334 RVA: 0x0013913C File Offset: 0x0013733C
		public AnonymousObjectMemberDeclaratorSyntax AnonymousObjectMemberDeclarator([global::System.Runtime.CompilerServices.Nullable(2)] NameEqualsSyntax nameEquals, ExpressionSyntax expression)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8647, nameEquals, expression, this.context, out num);
			if (greenNode != null)
			{
				return (AnonymousObjectMemberDeclaratorSyntax)greenNode;
			}
			AnonymousObjectMemberDeclaratorSyntax anonymousObjectMemberDeclaratorSyntax = new AnonymousObjectMemberDeclaratorSyntax(SyntaxKind.AnonymousObjectMemberDeclarator, nameEquals, expression, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(anonymousObjectMemberDeclaratorSyntax, num);
			}
			return anonymousObjectMemberDeclaratorSyntax;
		}

		// Token: 0x06004B87 RID: 19335 RVA: 0x00139187 File Offset: 0x00137387
		public AnonymousObjectCreationExpressionSyntax AnonymousObjectCreationExpression(SyntaxToken newKeyword, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<AnonymousObjectMemberDeclaratorSyntax> initializers, SyntaxToken closeBraceToken)
		{
			return new AnonymousObjectCreationExpressionSyntax(SyntaxKind.AnonymousObjectCreationExpression, newKeyword, openBraceToken, initializers.Node, closeBraceToken, this.context);
		}

		// Token: 0x06004B88 RID: 19336 RVA: 0x001391A4 File Offset: 0x001373A4
		public ArrayCreationExpressionSyntax ArrayCreationExpression(SyntaxToken newKeyword, ArrayTypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(2)] InitializerExpressionSyntax initializer)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8651, newKeyword, type, initializer, this.context, out num);
			if (greenNode != null)
			{
				return (ArrayCreationExpressionSyntax)greenNode;
			}
			ArrayCreationExpressionSyntax arrayCreationExpressionSyntax = new ArrayCreationExpressionSyntax(SyntaxKind.ArrayCreationExpression, newKeyword, type, initializer, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(arrayCreationExpressionSyntax, num);
			}
			return arrayCreationExpressionSyntax;
		}

		// Token: 0x06004B89 RID: 19337 RVA: 0x001391F1 File Offset: 0x001373F1
		public ImplicitArrayCreationExpressionSyntax ImplicitArrayCreationExpression(SyntaxToken newKeyword, SyntaxToken openBracketToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> commas, SyntaxToken closeBracketToken, InitializerExpressionSyntax initializer)
		{
			return new ImplicitArrayCreationExpressionSyntax(SyntaxKind.ImplicitArrayCreationExpression, newKeyword, openBracketToken, commas.Node, closeBracketToken, initializer, this.context);
		}

		// Token: 0x06004B8A RID: 19338 RVA: 0x00139210 File Offset: 0x00137410
		public StackAllocArrayCreationExpressionSyntax StackAllocArrayCreationExpression(SyntaxToken stackAllocKeyword, TypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(2)] InitializerExpressionSyntax initializer)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8653, stackAllocKeyword, type, initializer, this.context, out num);
			if (greenNode != null)
			{
				return (StackAllocArrayCreationExpressionSyntax)greenNode;
			}
			StackAllocArrayCreationExpressionSyntax stackAllocArrayCreationExpressionSyntax = new StackAllocArrayCreationExpressionSyntax(SyntaxKind.StackAllocArrayCreationExpression, stackAllocKeyword, type, initializer, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(stackAllocArrayCreationExpressionSyntax, num);
			}
			return stackAllocArrayCreationExpressionSyntax;
		}

		// Token: 0x06004B8B RID: 19339 RVA: 0x0013925D File Offset: 0x0013745D
		public ImplicitStackAllocArrayCreationExpressionSyntax ImplicitStackAllocArrayCreationExpression(SyntaxToken stackAllocKeyword, SyntaxToken openBracketToken, SyntaxToken closeBracketToken, InitializerExpressionSyntax initializer)
		{
			return new ImplicitStackAllocArrayCreationExpressionSyntax(SyntaxKind.ImplicitStackAllocArrayCreationExpression, stackAllocKeyword, openBracketToken, closeBracketToken, initializer, this.context);
		}

		// Token: 0x06004B8C RID: 19340 RVA: 0x00139274 File Offset: 0x00137474
		public QueryExpressionSyntax QueryExpression(FromClauseSyntax fromClause, QueryBodySyntax body)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8774, fromClause, body, this.context, out num);
			if (greenNode != null)
			{
				return (QueryExpressionSyntax)greenNode;
			}
			QueryExpressionSyntax queryExpressionSyntax = new QueryExpressionSyntax(SyntaxKind.QueryExpression, fromClause, body, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(queryExpressionSyntax, num);
			}
			return queryExpressionSyntax;
		}

		// Token: 0x06004B8D RID: 19341 RVA: 0x001392C0 File Offset: 0x001374C0
		public QueryBodySyntax QueryBody([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<QueryClauseSyntax> clauses, SelectOrGroupClauseSyntax selectOrGroup, [global::System.Runtime.CompilerServices.Nullable(2)] QueryContinuationSyntax continuation)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8775, clauses.Node, selectOrGroup, continuation, this.context, out num);
			if (greenNode != null)
			{
				return (QueryBodySyntax)greenNode;
			}
			QueryBodySyntax queryBodySyntax = new QueryBodySyntax(SyntaxKind.QueryBody, clauses.Node, selectOrGroup, continuation, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(queryBodySyntax, num);
			}
			return queryBodySyntax;
		}

		// Token: 0x06004B8E RID: 19342 RVA: 0x00139319 File Offset: 0x00137519
		public FromClauseSyntax FromClause(SyntaxToken fromKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSyntax type, SyntaxToken identifier, SyntaxToken inKeyword, ExpressionSyntax expression)
		{
			return new FromClauseSyntax(SyntaxKind.FromClause, fromKeyword, type, identifier, inKeyword, expression, this.context);
		}

		// Token: 0x06004B8F RID: 19343 RVA: 0x00139332 File Offset: 0x00137532
		public LetClauseSyntax LetClause(SyntaxToken letKeyword, SyntaxToken identifier, SyntaxToken equalsToken, ExpressionSyntax expression)
		{
			return new LetClauseSyntax(SyntaxKind.LetClause, letKeyword, identifier, equalsToken, expression, this.context);
		}

		// Token: 0x06004B90 RID: 19344 RVA: 0x0013934C File Offset: 0x0013754C
		public JoinClauseSyntax JoinClause(SyntaxToken joinKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSyntax type, SyntaxToken identifier, SyntaxToken inKeyword, ExpressionSyntax inExpression, SyntaxToken onKeyword, ExpressionSyntax leftExpression, SyntaxToken equalsKeyword, ExpressionSyntax rightExpression, [global::System.Runtime.CompilerServices.Nullable(2)] JoinIntoClauseSyntax into)
		{
			return new JoinClauseSyntax(SyntaxKind.JoinClause, joinKeyword, type, identifier, inKeyword, inExpression, onKeyword, leftExpression, equalsKeyword, rightExpression, into, this.context);
		}

		// Token: 0x06004B91 RID: 19345 RVA: 0x0013937C File Offset: 0x0013757C
		public JoinIntoClauseSyntax JoinIntoClause(SyntaxToken intoKeyword, SyntaxToken identifier)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8779, intoKeyword, identifier, this.context, out num);
			if (greenNode != null)
			{
				return (JoinIntoClauseSyntax)greenNode;
			}
			JoinIntoClauseSyntax joinIntoClauseSyntax = new JoinIntoClauseSyntax(SyntaxKind.JoinIntoClause, intoKeyword, identifier, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(joinIntoClauseSyntax, num);
			}
			return joinIntoClauseSyntax;
		}

		// Token: 0x06004B92 RID: 19346 RVA: 0x001393C8 File Offset: 0x001375C8
		public WhereClauseSyntax WhereClause(SyntaxToken whereKeyword, ExpressionSyntax condition)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8780, whereKeyword, condition, this.context, out num);
			if (greenNode != null)
			{
				return (WhereClauseSyntax)greenNode;
			}
			WhereClauseSyntax whereClauseSyntax = new WhereClauseSyntax(SyntaxKind.WhereClause, whereKeyword, condition, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(whereClauseSyntax, num);
			}
			return whereClauseSyntax;
		}

		// Token: 0x06004B93 RID: 19347 RVA: 0x00139414 File Offset: 0x00137614
		public OrderByClauseSyntax OrderByClause(SyntaxToken orderByKeyword, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<OrderingSyntax> orderings)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8781, orderByKeyword, orderings.Node, this.context, out num);
			if (greenNode != null)
			{
				return (OrderByClauseSyntax)greenNode;
			}
			OrderByClauseSyntax orderByClauseSyntax = new OrderByClauseSyntax(SyntaxKind.OrderByClause, orderByKeyword, orderings.Node, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(orderByClauseSyntax, num);
			}
			return orderByClauseSyntax;
		}

		// Token: 0x06004B94 RID: 19348 RVA: 0x0013946C File Offset: 0x0013766C
		public OrderingSyntax Ordering(SyntaxKind kind, ExpressionSyntax expression, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken ascendingOrDescendingKeyword)
		{
			if (kind - SyntaxKind.AscendingOrdering > 1)
			{
				throw new ArgumentException("kind");
			}
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode((int)kind, expression, ascendingOrDescendingKeyword, this.context, out num);
			if (greenNode != null)
			{
				return (OrderingSyntax)greenNode;
			}
			OrderingSyntax orderingSyntax = new OrderingSyntax(kind, expression, ascendingOrDescendingKeyword, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(orderingSyntax, num);
			}
			return orderingSyntax;
		}

		// Token: 0x06004B95 RID: 19349 RVA: 0x001394C4 File Offset: 0x001376C4
		public SelectClauseSyntax SelectClause(SyntaxToken selectKeyword, ExpressionSyntax expression)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8784, selectKeyword, expression, this.context, out num);
			if (greenNode != null)
			{
				return (SelectClauseSyntax)greenNode;
			}
			SelectClauseSyntax selectClauseSyntax = new SelectClauseSyntax(SyntaxKind.SelectClause, selectKeyword, expression, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(selectClauseSyntax, num);
			}
			return selectClauseSyntax;
		}

		// Token: 0x06004B96 RID: 19350 RVA: 0x0013950F File Offset: 0x0013770F
		public GroupClauseSyntax GroupClause(SyntaxToken groupKeyword, ExpressionSyntax groupExpression, SyntaxToken byKeyword, ExpressionSyntax byExpression)
		{
			return new GroupClauseSyntax(SyntaxKind.GroupClause, groupKeyword, groupExpression, byKeyword, byExpression, this.context);
		}

		// Token: 0x06004B97 RID: 19351 RVA: 0x00139528 File Offset: 0x00137728
		public QueryContinuationSyntax QueryContinuation(SyntaxToken intoKeyword, SyntaxToken identifier, QueryBodySyntax body)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8786, intoKeyword, identifier, body, this.context, out num);
			if (greenNode != null)
			{
				return (QueryContinuationSyntax)greenNode;
			}
			QueryContinuationSyntax queryContinuationSyntax = new QueryContinuationSyntax(SyntaxKind.QueryContinuation, intoKeyword, identifier, body, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(queryContinuationSyntax, num);
			}
			return queryContinuationSyntax;
		}

		// Token: 0x06004B98 RID: 19352 RVA: 0x00139578 File Offset: 0x00137778
		public OmittedArraySizeExpressionSyntax OmittedArraySizeExpression(SyntaxToken omittedArraySizeExpressionToken)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8654, omittedArraySizeExpressionToken, this.context, out num);
			if (greenNode != null)
			{
				return (OmittedArraySizeExpressionSyntax)greenNode;
			}
			OmittedArraySizeExpressionSyntax omittedArraySizeExpressionSyntax = new OmittedArraySizeExpressionSyntax(SyntaxKind.OmittedArraySizeExpression, omittedArraySizeExpressionToken, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(omittedArraySizeExpressionSyntax, num);
			}
			return omittedArraySizeExpressionSyntax;
		}

		// Token: 0x06004B99 RID: 19353 RVA: 0x001395C4 File Offset: 0x001377C4
		public InterpolatedStringExpressionSyntax InterpolatedStringExpression(SyntaxToken stringStartToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<InterpolatedStringContentSyntax> contents, SyntaxToken stringEndToken)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8655, stringStartToken, contents.Node, stringEndToken, this.context, out num);
			if (greenNode != null)
			{
				return (InterpolatedStringExpressionSyntax)greenNode;
			}
			InterpolatedStringExpressionSyntax interpolatedStringExpressionSyntax = new InterpolatedStringExpressionSyntax(SyntaxKind.InterpolatedStringExpression, stringStartToken, contents.Node, stringEndToken, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(interpolatedStringExpressionSyntax, num);
			}
			return interpolatedStringExpressionSyntax;
		}

		// Token: 0x06004B9A RID: 19354 RVA: 0x00139620 File Offset: 0x00137820
		public IsPatternExpressionSyntax IsPatternExpression(ExpressionSyntax expression, SyntaxToken isKeyword, PatternSyntax pattern)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8657, expression, isKeyword, pattern, this.context, out num);
			if (greenNode != null)
			{
				return (IsPatternExpressionSyntax)greenNode;
			}
			IsPatternExpressionSyntax isPatternExpressionSyntax = new IsPatternExpressionSyntax(SyntaxKind.IsPatternExpression, expression, isKeyword, pattern, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(isPatternExpressionSyntax, num);
			}
			return isPatternExpressionSyntax;
		}

		// Token: 0x06004B9B RID: 19355 RVA: 0x00139670 File Offset: 0x00137870
		public ThrowExpressionSyntax ThrowExpression(SyntaxToken throwKeyword, ExpressionSyntax expression)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(9052, throwKeyword, expression, this.context, out num);
			if (greenNode != null)
			{
				return (ThrowExpressionSyntax)greenNode;
			}
			ThrowExpressionSyntax throwExpressionSyntax = new ThrowExpressionSyntax(SyntaxKind.ThrowExpression, throwKeyword, expression, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(throwExpressionSyntax, num);
			}
			return throwExpressionSyntax;
		}

		// Token: 0x06004B9C RID: 19356 RVA: 0x001396BC File Offset: 0x001378BC
		public WhenClauseSyntax WhenClause(SyntaxToken whenKeyword, ExpressionSyntax condition)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(9013, whenKeyword, condition, this.context, out num);
			if (greenNode != null)
			{
				return (WhenClauseSyntax)greenNode;
			}
			WhenClauseSyntax whenClauseSyntax = new WhenClauseSyntax(SyntaxKind.WhenClause, whenKeyword, condition, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(whenClauseSyntax, num);
			}
			return whenClauseSyntax;
		}

		// Token: 0x06004B9D RID: 19357 RVA: 0x00139708 File Offset: 0x00137908
		public DiscardPatternSyntax DiscardPattern(SyntaxToken underscoreToken)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(9024, underscoreToken, this.context, out num);
			if (greenNode != null)
			{
				return (DiscardPatternSyntax)greenNode;
			}
			DiscardPatternSyntax discardPatternSyntax = new DiscardPatternSyntax(SyntaxKind.DiscardPattern, underscoreToken, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(discardPatternSyntax, num);
			}
			return discardPatternSyntax;
		}

		// Token: 0x06004B9E RID: 19358 RVA: 0x00139754 File Offset: 0x00137954
		public DeclarationPatternSyntax DeclarationPattern(TypeSyntax type, VariableDesignationSyntax designation)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(9000, type, designation, this.context, out num);
			if (greenNode != null)
			{
				return (DeclarationPatternSyntax)greenNode;
			}
			DeclarationPatternSyntax declarationPatternSyntax = new DeclarationPatternSyntax(SyntaxKind.DeclarationPattern, type, designation, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(declarationPatternSyntax, num);
			}
			return declarationPatternSyntax;
		}

		// Token: 0x06004B9F RID: 19359 RVA: 0x001397A0 File Offset: 0x001379A0
		public VarPatternSyntax VarPattern(SyntaxToken varKeyword, VariableDesignationSyntax designation)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(9027, varKeyword, designation, this.context, out num);
			if (greenNode != null)
			{
				return (VarPatternSyntax)greenNode;
			}
			VarPatternSyntax varPatternSyntax = new VarPatternSyntax(SyntaxKind.VarPattern, varKeyword, designation, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(varPatternSyntax, num);
			}
			return varPatternSyntax;
		}

		// Token: 0x06004BA0 RID: 19360 RVA: 0x001397EB File Offset: 0x001379EB
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public RecursivePatternSyntax RecursivePattern(TypeSyntax type, PositionalPatternClauseSyntax positionalPatternClause, PropertyPatternClauseSyntax propertyPatternClause, VariableDesignationSyntax designation)
		{
			return new RecursivePatternSyntax(SyntaxKind.RecursivePattern, type, positionalPatternClause, propertyPatternClause, designation, this.context);
		}

		// Token: 0x06004BA1 RID: 19361 RVA: 0x00139804 File Offset: 0x00137A04
		public PositionalPatternClauseSyntax PositionalPatternClause(SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<SubpatternSyntax> subpatterns, SyntaxToken closeParenToken)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(9023, openParenToken, subpatterns.Node, closeParenToken, this.context, out num);
			if (greenNode != null)
			{
				return (PositionalPatternClauseSyntax)greenNode;
			}
			PositionalPatternClauseSyntax positionalPatternClauseSyntax = new PositionalPatternClauseSyntax(SyntaxKind.PositionalPatternClause, openParenToken, subpatterns.Node, closeParenToken, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(positionalPatternClauseSyntax, num);
			}
			return positionalPatternClauseSyntax;
		}

		// Token: 0x06004BA2 RID: 19362 RVA: 0x00139860 File Offset: 0x00137A60
		public PropertyPatternClauseSyntax PropertyPatternClause(SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<SubpatternSyntax> subpatterns, SyntaxToken closeBraceToken)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(9021, openBraceToken, subpatterns.Node, closeBraceToken, this.context, out num);
			if (greenNode != null)
			{
				return (PropertyPatternClauseSyntax)greenNode;
			}
			PropertyPatternClauseSyntax propertyPatternClauseSyntax = new PropertyPatternClauseSyntax(SyntaxKind.PropertyPatternClause, openBraceToken, subpatterns.Node, closeBraceToken, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(propertyPatternClauseSyntax, num);
			}
			return propertyPatternClauseSyntax;
		}

		// Token: 0x06004BA3 RID: 19363 RVA: 0x001398BC File Offset: 0x00137ABC
		public SubpatternSyntax Subpattern([global::System.Runtime.CompilerServices.Nullable(2)] NameColonSyntax nameColon, PatternSyntax pattern)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(9022, nameColon, pattern, this.context, out num);
			if (greenNode != null)
			{
				return (SubpatternSyntax)greenNode;
			}
			SubpatternSyntax subpatternSyntax = new SubpatternSyntax(SyntaxKind.Subpattern, nameColon, pattern, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(subpatternSyntax, num);
			}
			return subpatternSyntax;
		}

		// Token: 0x06004BA4 RID: 19364 RVA: 0x00139908 File Offset: 0x00137B08
		public ConstantPatternSyntax ConstantPattern(ExpressionSyntax expression)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(9002, expression, this.context, out num);
			if (greenNode != null)
			{
				return (ConstantPatternSyntax)greenNode;
			}
			ConstantPatternSyntax constantPatternSyntax = new ConstantPatternSyntax(SyntaxKind.ConstantPattern, expression, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(constantPatternSyntax, num);
			}
			return constantPatternSyntax;
		}

		// Token: 0x06004BA5 RID: 19365 RVA: 0x00139954 File Offset: 0x00137B54
		public ParenthesizedPatternSyntax ParenthesizedPattern(SyntaxToken openParenToken, PatternSyntax pattern, SyntaxToken closeParenToken)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(9028, openParenToken, pattern, closeParenToken, this.context, out num);
			if (greenNode != null)
			{
				return (ParenthesizedPatternSyntax)greenNode;
			}
			ParenthesizedPatternSyntax parenthesizedPatternSyntax = new ParenthesizedPatternSyntax(SyntaxKind.ParenthesizedPattern, openParenToken, pattern, closeParenToken, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(parenthesizedPatternSyntax, num);
			}
			return parenthesizedPatternSyntax;
		}

		// Token: 0x06004BA6 RID: 19366 RVA: 0x001399A4 File Offset: 0x00137BA4
		public RelationalPatternSyntax RelationalPattern(SyntaxToken operatorToken, ExpressionSyntax expression)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(9029, operatorToken, expression, this.context, out num);
			if (greenNode != null)
			{
				return (RelationalPatternSyntax)greenNode;
			}
			RelationalPatternSyntax relationalPatternSyntax = new RelationalPatternSyntax(SyntaxKind.RelationalPattern, operatorToken, expression, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(relationalPatternSyntax, num);
			}
			return relationalPatternSyntax;
		}

		// Token: 0x06004BA7 RID: 19367 RVA: 0x001399F0 File Offset: 0x00137BF0
		public TypePatternSyntax TypePattern(TypeSyntax type)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(9030, type, this.context, out num);
			if (greenNode != null)
			{
				return (TypePatternSyntax)greenNode;
			}
			TypePatternSyntax typePatternSyntax = new TypePatternSyntax(SyntaxKind.TypePattern, type, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(typePatternSyntax, num);
			}
			return typePatternSyntax;
		}

		// Token: 0x06004BA8 RID: 19368 RVA: 0x00139A3C File Offset: 0x00137C3C
		public BinaryPatternSyntax BinaryPattern(SyntaxKind kind, PatternSyntax left, SyntaxToken operatorToken, PatternSyntax right)
		{
			if (kind - SyntaxKind.OrPattern > 1)
			{
				throw new ArgumentException("kind");
			}
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode((int)kind, left, operatorToken, right, this.context, out num);
			if (greenNode != null)
			{
				return (BinaryPatternSyntax)greenNode;
			}
			BinaryPatternSyntax binaryPatternSyntax = new BinaryPatternSyntax(kind, left, operatorToken, right, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(binaryPatternSyntax, num);
			}
			return binaryPatternSyntax;
		}

		// Token: 0x06004BA9 RID: 19369 RVA: 0x00139A98 File Offset: 0x00137C98
		public UnaryPatternSyntax UnaryPattern(SyntaxToken operatorToken, PatternSyntax pattern)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(9033, operatorToken, pattern, this.context, out num);
			if (greenNode != null)
			{
				return (UnaryPatternSyntax)greenNode;
			}
			UnaryPatternSyntax unaryPatternSyntax = new UnaryPatternSyntax(SyntaxKind.NotPattern, operatorToken, pattern, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(unaryPatternSyntax, num);
			}
			return unaryPatternSyntax;
		}

		// Token: 0x06004BAA RID: 19370 RVA: 0x00139AE4 File Offset: 0x00137CE4
		public InterpolatedStringTextSyntax InterpolatedStringText(SyntaxToken textToken)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8919, textToken, this.context, out num);
			if (greenNode != null)
			{
				return (InterpolatedStringTextSyntax)greenNode;
			}
			InterpolatedStringTextSyntax interpolatedStringTextSyntax = new InterpolatedStringTextSyntax(SyntaxKind.InterpolatedStringText, textToken, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(interpolatedStringTextSyntax, num);
			}
			return interpolatedStringTextSyntax;
		}

		// Token: 0x06004BAB RID: 19371 RVA: 0x00139B2D File Offset: 0x00137D2D
		public InterpolationSyntax Interpolation(SyntaxToken openBraceToken, ExpressionSyntax expression, [global::System.Runtime.CompilerServices.Nullable(2)] InterpolationAlignmentClauseSyntax alignmentClause, [global::System.Runtime.CompilerServices.Nullable(2)] InterpolationFormatClauseSyntax formatClause, SyntaxToken closeBraceToken)
		{
			return new InterpolationSyntax(SyntaxKind.Interpolation, openBraceToken, expression, alignmentClause, formatClause, closeBraceToken, this.context);
		}

		// Token: 0x06004BAC RID: 19372 RVA: 0x00139B48 File Offset: 0x00137D48
		public InterpolationAlignmentClauseSyntax InterpolationAlignmentClause(SyntaxToken commaToken, ExpressionSyntax value)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8920, commaToken, value, this.context, out num);
			if (greenNode != null)
			{
				return (InterpolationAlignmentClauseSyntax)greenNode;
			}
			InterpolationAlignmentClauseSyntax interpolationAlignmentClauseSyntax = new InterpolationAlignmentClauseSyntax(SyntaxKind.InterpolationAlignmentClause, commaToken, value, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(interpolationAlignmentClauseSyntax, num);
			}
			return interpolationAlignmentClauseSyntax;
		}

		// Token: 0x06004BAD RID: 19373 RVA: 0x00139B94 File Offset: 0x00137D94
		public InterpolationFormatClauseSyntax InterpolationFormatClause(SyntaxToken colonToken, SyntaxToken formatStringToken)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8921, colonToken, formatStringToken, this.context, out num);
			if (greenNode != null)
			{
				return (InterpolationFormatClauseSyntax)greenNode;
			}
			InterpolationFormatClauseSyntax interpolationFormatClauseSyntax = new InterpolationFormatClauseSyntax(SyntaxKind.InterpolationFormatClause, colonToken, formatStringToken, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(interpolationFormatClauseSyntax, num);
			}
			return interpolationFormatClauseSyntax;
		}

		// Token: 0x06004BAE RID: 19374 RVA: 0x00139BE0 File Offset: 0x00137DE0
		public GlobalStatementSyntax GlobalStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, StatementSyntax statement)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8841, attributeLists.Node, modifiers.Node, statement, this.context, out num);
			if (greenNode != null)
			{
				return (GlobalStatementSyntax)greenNode;
			}
			GlobalStatementSyntax globalStatementSyntax = new GlobalStatementSyntax(SyntaxKind.GlobalStatement, attributeLists.Node, modifiers.Node, statement, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(globalStatementSyntax, num);
			}
			return globalStatementSyntax;
		}

		// Token: 0x06004BAF RID: 19375 RVA: 0x00139C45 File Offset: 0x00137E45
		public BlockSyntax Block([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<StatementSyntax> statements, SyntaxToken closeBraceToken)
		{
			return new BlockSyntax(SyntaxKind.Block, attributeLists.Node, openBraceToken, statements.Node, closeBraceToken, this.context);
		}

		// Token: 0x06004BB0 RID: 19376 RVA: 0x00139C68 File Offset: 0x00137E68
		public LocalFunctionStatementSyntax LocalFunctionStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, TypeSyntax returnType, SyntaxToken identifier, [global::System.Runtime.CompilerServices.Nullable(2)] TypeParameterListSyntax typeParameterList, ParameterListSyntax parameterList, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, [global::System.Runtime.CompilerServices.Nullable(2)] BlockSyntax body, [global::System.Runtime.CompilerServices.Nullable(2)] ArrowExpressionClauseSyntax expressionBody, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken semicolonToken)
		{
			return new LocalFunctionStatementSyntax(SyntaxKind.LocalFunctionStatement, attributeLists.Node, modifiers.Node, returnType, identifier, typeParameterList, parameterList, constraintClauses.Node, body, expressionBody, semicolonToken, this.context);
		}

		// Token: 0x06004BB1 RID: 19377 RVA: 0x00139CA7 File Offset: 0x00137EA7
		public LocalDeclarationStatementSyntax LocalDeclarationStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken awaitKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken usingKeyword, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, VariableDeclarationSyntax declaration, SyntaxToken semicolonToken)
		{
			return new LocalDeclarationStatementSyntax(SyntaxKind.LocalDeclarationStatement, attributeLists.Node, awaitKeyword, usingKeyword, modifiers.Node, declaration, semicolonToken, this.context);
		}

		// Token: 0x06004BB2 RID: 19378 RVA: 0x00139CD0 File Offset: 0x00137ED0
		public VariableDeclarationSyntax VariableDeclaration(TypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<VariableDeclaratorSyntax> variables)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8794, type, variables.Node, this.context, out num);
			if (greenNode != null)
			{
				return (VariableDeclarationSyntax)greenNode;
			}
			VariableDeclarationSyntax variableDeclarationSyntax = new VariableDeclarationSyntax(SyntaxKind.VariableDeclaration, type, variables.Node, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(variableDeclarationSyntax, num);
			}
			return variableDeclarationSyntax;
		}

		// Token: 0x06004BB3 RID: 19379 RVA: 0x00139D28 File Offset: 0x00137F28
		public VariableDeclaratorSyntax VariableDeclarator(SyntaxToken identifier, [global::System.Runtime.CompilerServices.Nullable(2)] BracketedArgumentListSyntax argumentList, [global::System.Runtime.CompilerServices.Nullable(2)] EqualsValueClauseSyntax initializer)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8795, identifier, argumentList, initializer, this.context, out num);
			if (greenNode != null)
			{
				return (VariableDeclaratorSyntax)greenNode;
			}
			VariableDeclaratorSyntax variableDeclaratorSyntax = new VariableDeclaratorSyntax(SyntaxKind.VariableDeclarator, identifier, argumentList, initializer, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(variableDeclaratorSyntax, num);
			}
			return variableDeclaratorSyntax;
		}

		// Token: 0x06004BB4 RID: 19380 RVA: 0x00139D78 File Offset: 0x00137F78
		public EqualsValueClauseSyntax EqualsValueClause(SyntaxToken equalsToken, ExpressionSyntax value)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8796, equalsToken, value, this.context, out num);
			if (greenNode != null)
			{
				return (EqualsValueClauseSyntax)greenNode;
			}
			EqualsValueClauseSyntax equalsValueClauseSyntax = new EqualsValueClauseSyntax(SyntaxKind.EqualsValueClause, equalsToken, value, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(equalsValueClauseSyntax, num);
			}
			return equalsValueClauseSyntax;
		}

		// Token: 0x06004BB5 RID: 19381 RVA: 0x00139DC4 File Offset: 0x00137FC4
		public SingleVariableDesignationSyntax SingleVariableDesignation(SyntaxToken identifier)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8927, identifier, this.context, out num);
			if (greenNode != null)
			{
				return (SingleVariableDesignationSyntax)greenNode;
			}
			SingleVariableDesignationSyntax singleVariableDesignationSyntax = new SingleVariableDesignationSyntax(SyntaxKind.SingleVariableDesignation, identifier, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(singleVariableDesignationSyntax, num);
			}
			return singleVariableDesignationSyntax;
		}

		// Token: 0x06004BB6 RID: 19382 RVA: 0x00139E10 File Offset: 0x00138010
		public DiscardDesignationSyntax DiscardDesignation(SyntaxToken underscoreToken)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(9014, underscoreToken, this.context, out num);
			if (greenNode != null)
			{
				return (DiscardDesignationSyntax)greenNode;
			}
			DiscardDesignationSyntax discardDesignationSyntax = new DiscardDesignationSyntax(SyntaxKind.DiscardDesignation, underscoreToken, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(discardDesignationSyntax, num);
			}
			return discardDesignationSyntax;
		}

		// Token: 0x06004BB7 RID: 19383 RVA: 0x00139E5C File Offset: 0x0013805C
		public ParenthesizedVariableDesignationSyntax ParenthesizedVariableDesignation(SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<VariableDesignationSyntax> variables, SyntaxToken closeParenToken)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8928, openParenToken, variables.Node, closeParenToken, this.context, out num);
			if (greenNode != null)
			{
				return (ParenthesizedVariableDesignationSyntax)greenNode;
			}
			ParenthesizedVariableDesignationSyntax parenthesizedVariableDesignationSyntax = new ParenthesizedVariableDesignationSyntax(SyntaxKind.ParenthesizedVariableDesignation, openParenToken, variables.Node, closeParenToken, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(parenthesizedVariableDesignationSyntax, num);
			}
			return parenthesizedVariableDesignationSyntax;
		}

		// Token: 0x06004BB8 RID: 19384 RVA: 0x00139EB8 File Offset: 0x001380B8
		public ExpressionStatementSyntax ExpressionStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, ExpressionSyntax expression, SyntaxToken semicolonToken)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8797, attributeLists.Node, expression, semicolonToken, this.context, out num);
			if (greenNode != null)
			{
				return (ExpressionStatementSyntax)greenNode;
			}
			ExpressionStatementSyntax expressionStatementSyntax = new ExpressionStatementSyntax(SyntaxKind.ExpressionStatement, attributeLists.Node, expression, semicolonToken, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(expressionStatementSyntax, num);
			}
			return expressionStatementSyntax;
		}

		// Token: 0x06004BB9 RID: 19385 RVA: 0x00139F14 File Offset: 0x00138114
		public EmptyStatementSyntax EmptyStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken semicolonToken)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8798, attributeLists.Node, semicolonToken, this.context, out num);
			if (greenNode != null)
			{
				return (EmptyStatementSyntax)greenNode;
			}
			EmptyStatementSyntax emptyStatementSyntax = new EmptyStatementSyntax(SyntaxKind.EmptyStatement, attributeLists.Node, semicolonToken, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(emptyStatementSyntax, num);
			}
			return emptyStatementSyntax;
		}

		// Token: 0x06004BBA RID: 19386 RVA: 0x00139F6B File Offset: 0x0013816B
		public LabeledStatementSyntax LabeledStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken identifier, SyntaxToken colonToken, StatementSyntax statement)
		{
			return new LabeledStatementSyntax(SyntaxKind.LabeledStatement, attributeLists.Node, identifier, colonToken, statement, this.context);
		}

		// Token: 0x06004BBB RID: 19387 RVA: 0x00139F88 File Offset: 0x00138188
		public GotoStatementSyntax GotoStatement(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken gotoKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken caseOrDefaultKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax expression, SyntaxToken semicolonToken)
		{
			if (kind - SyntaxKind.GotoStatement > 2)
			{
				throw new ArgumentException("kind");
			}
			return new GotoStatementSyntax(kind, attributeLists.Node, gotoKeyword, caseOrDefaultKeyword, expression, semicolonToken, this.context);
		}

		// Token: 0x06004BBC RID: 19388 RVA: 0x00139FBC File Offset: 0x001381BC
		public BreakStatementSyntax BreakStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken breakKeyword, SyntaxToken semicolonToken)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8803, attributeLists.Node, breakKeyword, semicolonToken, this.context, out num);
			if (greenNode != null)
			{
				return (BreakStatementSyntax)greenNode;
			}
			BreakStatementSyntax breakStatementSyntax = new BreakStatementSyntax(SyntaxKind.BreakStatement, attributeLists.Node, breakKeyword, semicolonToken, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(breakStatementSyntax, num);
			}
			return breakStatementSyntax;
		}

		// Token: 0x06004BBD RID: 19389 RVA: 0x0013A018 File Offset: 0x00138218
		public ContinueStatementSyntax ContinueStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken continueKeyword, SyntaxToken semicolonToken)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8804, attributeLists.Node, continueKeyword, semicolonToken, this.context, out num);
			if (greenNode != null)
			{
				return (ContinueStatementSyntax)greenNode;
			}
			ContinueStatementSyntax continueStatementSyntax = new ContinueStatementSyntax(SyntaxKind.ContinueStatement, attributeLists.Node, continueKeyword, semicolonToken, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(continueStatementSyntax, num);
			}
			return continueStatementSyntax;
		}

		// Token: 0x06004BBE RID: 19390 RVA: 0x0013A071 File Offset: 0x00138271
		public ReturnStatementSyntax ReturnStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken returnKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax expression, SyntaxToken semicolonToken)
		{
			return new ReturnStatementSyntax(SyntaxKind.ReturnStatement, attributeLists.Node, returnKeyword, expression, semicolonToken, this.context);
		}

		// Token: 0x06004BBF RID: 19391 RVA: 0x0013A08E File Offset: 0x0013828E
		public ThrowStatementSyntax ThrowStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken throwKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax expression, SyntaxToken semicolonToken)
		{
			return new ThrowStatementSyntax(SyntaxKind.ThrowStatement, attributeLists.Node, throwKeyword, expression, semicolonToken, this.context);
		}

		// Token: 0x06004BC0 RID: 19392 RVA: 0x0013A0AB File Offset: 0x001382AB
		public YieldStatementSyntax YieldStatement(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken yieldKeyword, SyntaxToken returnOrBreakKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax expression, SyntaxToken semicolonToken)
		{
			if (kind - SyntaxKind.YieldReturnStatement > 1)
			{
				throw new ArgumentException("kind");
			}
			return new YieldStatementSyntax(kind, attributeLists.Node, yieldKeyword, returnOrBreakKeyword, expression, semicolonToken, this.context);
		}

		// Token: 0x06004BC1 RID: 19393 RVA: 0x0013A0DC File Offset: 0x001382DC
		public WhileStatementSyntax WhileStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken whileKeyword, SyntaxToken openParenToken, ExpressionSyntax condition, SyntaxToken closeParenToken, StatementSyntax statement)
		{
			return new WhileStatementSyntax(SyntaxKind.WhileStatement, attributeLists.Node, whileKeyword, openParenToken, condition, closeParenToken, statement, this.context);
		}

		// Token: 0x06004BC2 RID: 19394 RVA: 0x0013A100 File Offset: 0x00138300
		public DoStatementSyntax DoStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken doKeyword, StatementSyntax statement, SyntaxToken whileKeyword, SyntaxToken openParenToken, ExpressionSyntax condition, SyntaxToken closeParenToken, SyntaxToken semicolonToken)
		{
			return new DoStatementSyntax(SyntaxKind.DoStatement, attributeLists.Node, doKeyword, statement, whileKeyword, openParenToken, condition, closeParenToken, semicolonToken, this.context);
		}

		// Token: 0x06004BC3 RID: 19395 RVA: 0x0013A130 File Offset: 0x00138330
		public ForStatementSyntax ForStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken forKeyword, SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(2)] VariableDeclarationSyntax declaration, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<ExpressionSyntax> initializers, SyntaxToken firstSemicolonToken, [global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax condition, SyntaxToken secondSemicolonToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<ExpressionSyntax> incrementors, SyntaxToken closeParenToken, StatementSyntax statement)
		{
			return new ForStatementSyntax(SyntaxKind.ForStatement, attributeLists.Node, forKeyword, openParenToken, declaration, initializers.Node, firstSemicolonToken, condition, secondSemicolonToken, incrementors.Node, closeParenToken, statement, this.context);
		}

		// Token: 0x06004BC4 RID: 19396 RVA: 0x0013A170 File Offset: 0x00138370
		public ForEachStatementSyntax ForEachStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken awaitKeyword, SyntaxToken forEachKeyword, SyntaxToken openParenToken, TypeSyntax type, SyntaxToken identifier, SyntaxToken inKeyword, ExpressionSyntax expression, SyntaxToken closeParenToken, StatementSyntax statement)
		{
			return new ForEachStatementSyntax(SyntaxKind.ForEachStatement, attributeLists.Node, awaitKeyword, forEachKeyword, openParenToken, type, identifier, inKeyword, expression, closeParenToken, statement, this.context);
		}

		// Token: 0x06004BC5 RID: 19397 RVA: 0x0013A1A4 File Offset: 0x001383A4
		public ForEachVariableStatementSyntax ForEachVariableStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken awaitKeyword, SyntaxToken forEachKeyword, SyntaxToken openParenToken, ExpressionSyntax variable, SyntaxToken inKeyword, ExpressionSyntax expression, SyntaxToken closeParenToken, StatementSyntax statement)
		{
			return new ForEachVariableStatementSyntax(SyntaxKind.ForEachVariableStatement, attributeLists.Node, awaitKeyword, forEachKeyword, openParenToken, variable, inKeyword, expression, closeParenToken, statement, this.context);
		}

		// Token: 0x06004BC6 RID: 19398 RVA: 0x0013A1D8 File Offset: 0x001383D8
		public UsingStatementSyntax UsingStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken awaitKeyword, SyntaxToken usingKeyword, SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(2)] VariableDeclarationSyntax declaration, [global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax expression, SyntaxToken closeParenToken, StatementSyntax statement)
		{
			return new UsingStatementSyntax(SyntaxKind.UsingStatement, attributeLists.Node, awaitKeyword, usingKeyword, openParenToken, declaration, expression, closeParenToken, statement, this.context);
		}

		// Token: 0x06004BC7 RID: 19399 RVA: 0x0013A208 File Offset: 0x00138408
		public FixedStatementSyntax FixedStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken fixedKeyword, SyntaxToken openParenToken, VariableDeclarationSyntax declaration, SyntaxToken closeParenToken, StatementSyntax statement)
		{
			return new FixedStatementSyntax(SyntaxKind.FixedStatement, attributeLists.Node, fixedKeyword, openParenToken, declaration, closeParenToken, statement, this.context);
		}

		// Token: 0x06004BC8 RID: 19400 RVA: 0x0013A22C File Offset: 0x0013842C
		public CheckedStatementSyntax CheckedStatement(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken keyword, BlockSyntax block)
		{
			if (kind - SyntaxKind.CheckedStatement > 1)
			{
				throw new ArgumentException("kind");
			}
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode((int)kind, attributeLists.Node, keyword, block, this.context, out num);
			if (greenNode != null)
			{
				return (CheckedStatementSyntax)greenNode;
			}
			CheckedStatementSyntax checkedStatementSyntax = new CheckedStatementSyntax(kind, attributeLists.Node, keyword, block, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(checkedStatementSyntax, num);
			}
			return checkedStatementSyntax;
		}

		// Token: 0x06004BC9 RID: 19401 RVA: 0x0013A294 File Offset: 0x00138494
		public UnsafeStatementSyntax UnsafeStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken unsafeKeyword, BlockSyntax block)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8817, attributeLists.Node, unsafeKeyword, block, this.context, out num);
			if (greenNode != null)
			{
				return (UnsafeStatementSyntax)greenNode;
			}
			UnsafeStatementSyntax unsafeStatementSyntax = new UnsafeStatementSyntax(SyntaxKind.UnsafeStatement, attributeLists.Node, unsafeKeyword, block, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(unsafeStatementSyntax, num);
			}
			return unsafeStatementSyntax;
		}

		// Token: 0x06004BCA RID: 19402 RVA: 0x0013A2ED File Offset: 0x001384ED
		public LockStatementSyntax LockStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken lockKeyword, SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken closeParenToken, StatementSyntax statement)
		{
			return new LockStatementSyntax(SyntaxKind.LockStatement, attributeLists.Node, lockKeyword, openParenToken, expression, closeParenToken, statement, this.context);
		}

		// Token: 0x06004BCB RID: 19403 RVA: 0x0013A310 File Offset: 0x00138510
		public IfStatementSyntax IfStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken ifKeyword, SyntaxToken openParenToken, ExpressionSyntax condition, SyntaxToken closeParenToken, StatementSyntax statement, [global::System.Runtime.CompilerServices.Nullable(2)] ElseClauseSyntax @else)
		{
			return new IfStatementSyntax(SyntaxKind.IfStatement, attributeLists.Node, ifKeyword, openParenToken, condition, closeParenToken, statement, @else, this.context);
		}

		// Token: 0x06004BCC RID: 19404 RVA: 0x0013A340 File Offset: 0x00138540
		public ElseClauseSyntax ElseClause(SyntaxToken elseKeyword, StatementSyntax statement)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8820, elseKeyword, statement, this.context, out num);
			if (greenNode != null)
			{
				return (ElseClauseSyntax)greenNode;
			}
			ElseClauseSyntax elseClauseSyntax = new ElseClauseSyntax(SyntaxKind.ElseClause, elseKeyword, statement, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(elseClauseSyntax, num);
			}
			return elseClauseSyntax;
		}

		// Token: 0x06004BCD RID: 19405 RVA: 0x0013A38C File Offset: 0x0013858C
		public SwitchStatementSyntax SwitchStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken switchKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken openParenToken, ExpressionSyntax expression, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken closeParenToken, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SwitchSectionSyntax> sections, SyntaxToken closeBraceToken)
		{
			return new SwitchStatementSyntax(SyntaxKind.SwitchStatement, attributeLists.Node, switchKeyword, openParenToken, expression, closeParenToken, openBraceToken, sections.Node, closeBraceToken, this.context);
		}

		// Token: 0x06004BCE RID: 19406 RVA: 0x0013A3C4 File Offset: 0x001385C4
		public SwitchSectionSyntax SwitchSection([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SwitchLabelSyntax> labels, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<StatementSyntax> statements)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8822, labels.Node, statements.Node, this.context, out num);
			if (greenNode != null)
			{
				return (SwitchSectionSyntax)greenNode;
			}
			SwitchSectionSyntax switchSectionSyntax = new SwitchSectionSyntax(SyntaxKind.SwitchSection, labels.Node, statements.Node, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(switchSectionSyntax, num);
			}
			return switchSectionSyntax;
		}

		// Token: 0x06004BCF RID: 19407 RVA: 0x0013A427 File Offset: 0x00138627
		public CasePatternSwitchLabelSyntax CasePatternSwitchLabel(SyntaxToken keyword, PatternSyntax pattern, [global::System.Runtime.CompilerServices.Nullable(2)] WhenClauseSyntax whenClause, SyntaxToken colonToken)
		{
			return new CasePatternSwitchLabelSyntax(SyntaxKind.CasePatternSwitchLabel, keyword, pattern, whenClause, colonToken, this.context);
		}

		// Token: 0x06004BD0 RID: 19408 RVA: 0x0013A440 File Offset: 0x00138640
		public CaseSwitchLabelSyntax CaseSwitchLabel(SyntaxToken keyword, ExpressionSyntax value, SyntaxToken colonToken)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8823, keyword, value, colonToken, this.context, out num);
			if (greenNode != null)
			{
				return (CaseSwitchLabelSyntax)greenNode;
			}
			CaseSwitchLabelSyntax caseSwitchLabelSyntax = new CaseSwitchLabelSyntax(SyntaxKind.CaseSwitchLabel, keyword, value, colonToken, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(caseSwitchLabelSyntax, num);
			}
			return caseSwitchLabelSyntax;
		}

		// Token: 0x06004BD1 RID: 19409 RVA: 0x0013A490 File Offset: 0x00138690
		public DefaultSwitchLabelSyntax DefaultSwitchLabel(SyntaxToken keyword, SyntaxToken colonToken)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8824, keyword, colonToken, this.context, out num);
			if (greenNode != null)
			{
				return (DefaultSwitchLabelSyntax)greenNode;
			}
			DefaultSwitchLabelSyntax defaultSwitchLabelSyntax = new DefaultSwitchLabelSyntax(SyntaxKind.DefaultSwitchLabel, keyword, colonToken, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(defaultSwitchLabelSyntax, num);
			}
			return defaultSwitchLabelSyntax;
		}

		// Token: 0x06004BD2 RID: 19410 RVA: 0x0013A4DB File Offset: 0x001386DB
		public SwitchExpressionSyntax SwitchExpression(ExpressionSyntax governingExpression, SyntaxToken switchKeyword, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<SwitchExpressionArmSyntax> arms, SyntaxToken closeBraceToken)
		{
			return new SwitchExpressionSyntax(SyntaxKind.SwitchExpression, governingExpression, switchKeyword, openBraceToken, arms.Node, closeBraceToken, this.context);
		}

		// Token: 0x06004BD3 RID: 19411 RVA: 0x0013A4F9 File Offset: 0x001386F9
		public SwitchExpressionArmSyntax SwitchExpressionArm(PatternSyntax pattern, [global::System.Runtime.CompilerServices.Nullable(2)] WhenClauseSyntax whenClause, SyntaxToken equalsGreaterThanToken, ExpressionSyntax expression)
		{
			return new SwitchExpressionArmSyntax(SyntaxKind.SwitchExpressionArm, pattern, whenClause, equalsGreaterThanToken, expression, this.context);
		}

		// Token: 0x06004BD4 RID: 19412 RVA: 0x0013A510 File Offset: 0x00138710
		public TryStatementSyntax TryStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken tryKeyword, BlockSyntax block, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<CatchClauseSyntax> catches, [global::System.Runtime.CompilerServices.Nullable(2)] FinallyClauseSyntax @finally)
		{
			return new TryStatementSyntax(SyntaxKind.TryStatement, attributeLists.Node, tryKeyword, block, catches.Node, @finally, this.context);
		}

		// Token: 0x06004BD5 RID: 19413 RVA: 0x0013A534 File Offset: 0x00138734
		public CatchClauseSyntax CatchClause(SyntaxToken catchKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] CatchDeclarationSyntax declaration, [global::System.Runtime.CompilerServices.Nullable(2)] CatchFilterClauseSyntax filter, BlockSyntax block)
		{
			return new CatchClauseSyntax(SyntaxKind.CatchClause, catchKeyword, declaration, filter, block, this.context);
		}

		// Token: 0x06004BD6 RID: 19414 RVA: 0x0013A54B File Offset: 0x0013874B
		public CatchDeclarationSyntax CatchDeclaration(SyntaxToken openParenToken, TypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken identifier, SyntaxToken closeParenToken)
		{
			return new CatchDeclarationSyntax(SyntaxKind.CatchDeclaration, openParenToken, type, identifier, closeParenToken, this.context);
		}

		// Token: 0x06004BD7 RID: 19415 RVA: 0x0013A562 File Offset: 0x00138762
		public CatchFilterClauseSyntax CatchFilterClause(SyntaxToken whenKeyword, SyntaxToken openParenToken, ExpressionSyntax filterExpression, SyntaxToken closeParenToken)
		{
			return new CatchFilterClauseSyntax(SyntaxKind.CatchFilterClause, whenKeyword, openParenToken, filterExpression, closeParenToken, this.context);
		}

		// Token: 0x06004BD8 RID: 19416 RVA: 0x0013A57C File Offset: 0x0013877C
		public FinallyClauseSyntax FinallyClause(SyntaxToken finallyKeyword, BlockSyntax block)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8829, finallyKeyword, block, this.context, out num);
			if (greenNode != null)
			{
				return (FinallyClauseSyntax)greenNode;
			}
			FinallyClauseSyntax finallyClauseSyntax = new FinallyClauseSyntax(SyntaxKind.FinallyClause, finallyKeyword, block, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(finallyClauseSyntax, num);
			}
			return finallyClauseSyntax;
		}

		// Token: 0x06004BD9 RID: 19417 RVA: 0x0013A5C7 File Offset: 0x001387C7
		public CompilationUnitSyntax CompilationUnit([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<ExternAliasDirectiveSyntax> externs, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<UsingDirectiveSyntax> usings, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<MemberDeclarationSyntax> members, SyntaxToken endOfFileToken)
		{
			return new CompilationUnitSyntax(SyntaxKind.CompilationUnit, externs.Node, usings.Node, attributeLists.Node, members.Node, endOfFileToken, this.context);
		}

		// Token: 0x06004BDA RID: 19418 RVA: 0x0013A5F7 File Offset: 0x001387F7
		public ExternAliasDirectiveSyntax ExternAliasDirective(SyntaxToken externKeyword, SyntaxToken aliasKeyword, SyntaxToken identifier, SyntaxToken semicolonToken)
		{
			return new ExternAliasDirectiveSyntax(SyntaxKind.ExternAliasDirective, externKeyword, aliasKeyword, identifier, semicolonToken, this.context);
		}

		// Token: 0x06004BDB RID: 19419 RVA: 0x0013A60E File Offset: 0x0013880E
		public UsingDirectiveSyntax UsingDirective([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken globalKeyword, SyntaxToken usingKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken staticKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] NameEqualsSyntax alias, NameSyntax name, SyntaxToken semicolonToken)
		{
			return new UsingDirectiveSyntax(SyntaxKind.UsingDirective, globalKeyword, usingKeyword, staticKeyword, alias, name, semicolonToken, this.context);
		}

		// Token: 0x06004BDC RID: 19420 RVA: 0x0013A62C File Offset: 0x0013882C
		public NamespaceDeclarationSyntax NamespaceDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, SyntaxToken namespaceKeyword, NameSyntax name, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<ExternAliasDirectiveSyntax> externs, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<UsingDirectiveSyntax> usings, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<MemberDeclarationSyntax> members, SyntaxToken closeBraceToken, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken semicolonToken)
		{
			return new NamespaceDeclarationSyntax(SyntaxKind.NamespaceDeclaration, attributeLists.Node, modifiers.Node, namespaceKeyword, name, openBraceToken, externs.Node, usings.Node, members.Node, closeBraceToken, semicolonToken, this.context);
		}

		// Token: 0x06004BDD RID: 19421 RVA: 0x0013A675 File Offset: 0x00138875
		public AttributeListSyntax AttributeList(SyntaxToken openBracketToken, [global::System.Runtime.CompilerServices.Nullable(2)] AttributeTargetSpecifierSyntax target, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<AttributeSyntax> attributes, SyntaxToken closeBracketToken)
		{
			return new AttributeListSyntax(SyntaxKind.AttributeList, openBracketToken, target, attributes.Node, closeBracketToken, this.context);
		}

		// Token: 0x06004BDE RID: 19422 RVA: 0x0013A694 File Offset: 0x00138894
		public AttributeTargetSpecifierSyntax AttributeTargetSpecifier(SyntaxToken identifier, SyntaxToken colonToken)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8848, identifier, colonToken, this.context, out num);
			if (greenNode != null)
			{
				return (AttributeTargetSpecifierSyntax)greenNode;
			}
			AttributeTargetSpecifierSyntax attributeTargetSpecifierSyntax = new AttributeTargetSpecifierSyntax(SyntaxKind.AttributeTargetSpecifier, identifier, colonToken, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(attributeTargetSpecifierSyntax, num);
			}
			return attributeTargetSpecifierSyntax;
		}

		// Token: 0x06004BDF RID: 19423 RVA: 0x0013A6E0 File Offset: 0x001388E0
		public AttributeSyntax Attribute(NameSyntax name, [global::System.Runtime.CompilerServices.Nullable(2)] AttributeArgumentListSyntax argumentList)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8849, name, argumentList, this.context, out num);
			if (greenNode != null)
			{
				return (AttributeSyntax)greenNode;
			}
			AttributeSyntax attributeSyntax = new AttributeSyntax(SyntaxKind.Attribute, name, argumentList, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(attributeSyntax, num);
			}
			return attributeSyntax;
		}

		// Token: 0x06004BE0 RID: 19424 RVA: 0x0013A72C File Offset: 0x0013892C
		public AttributeArgumentListSyntax AttributeArgumentList(SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<AttributeArgumentSyntax> arguments, SyntaxToken closeParenToken)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8850, openParenToken, arguments.Node, closeParenToken, this.context, out num);
			if (greenNode != null)
			{
				return (AttributeArgumentListSyntax)greenNode;
			}
			AttributeArgumentListSyntax attributeArgumentListSyntax = new AttributeArgumentListSyntax(SyntaxKind.AttributeArgumentList, openParenToken, arguments.Node, closeParenToken, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(attributeArgumentListSyntax, num);
			}
			return attributeArgumentListSyntax;
		}

		// Token: 0x06004BE1 RID: 19425 RVA: 0x0013A788 File Offset: 0x00138988
		public AttributeArgumentSyntax AttributeArgument([global::System.Runtime.CompilerServices.Nullable(2)] NameEqualsSyntax nameEquals, [global::System.Runtime.CompilerServices.Nullable(2)] NameColonSyntax nameColon, ExpressionSyntax expression)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8851, nameEquals, nameColon, expression, this.context, out num);
			if (greenNode != null)
			{
				return (AttributeArgumentSyntax)greenNode;
			}
			AttributeArgumentSyntax attributeArgumentSyntax = new AttributeArgumentSyntax(SyntaxKind.AttributeArgument, nameEquals, nameColon, expression, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(attributeArgumentSyntax, num);
			}
			return attributeArgumentSyntax;
		}

		// Token: 0x06004BE2 RID: 19426 RVA: 0x0013A7D8 File Offset: 0x001389D8
		public NameEqualsSyntax NameEquals(IdentifierNameSyntax name, SyntaxToken equalsToken)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8852, name, equalsToken, this.context, out num);
			if (greenNode != null)
			{
				return (NameEqualsSyntax)greenNode;
			}
			NameEqualsSyntax nameEqualsSyntax = new NameEqualsSyntax(SyntaxKind.NameEquals, name, equalsToken, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(nameEqualsSyntax, num);
			}
			return nameEqualsSyntax;
		}

		// Token: 0x06004BE3 RID: 19427 RVA: 0x0013A824 File Offset: 0x00138A24
		public TypeParameterListSyntax TypeParameterList(SyntaxToken lessThanToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<TypeParameterSyntax> parameters, SyntaxToken greaterThanToken)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8909, lessThanToken, parameters.Node, greaterThanToken, this.context, out num);
			if (greenNode != null)
			{
				return (TypeParameterListSyntax)greenNode;
			}
			TypeParameterListSyntax typeParameterListSyntax = new TypeParameterListSyntax(SyntaxKind.TypeParameterList, lessThanToken, parameters.Node, greaterThanToken, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(typeParameterListSyntax, num);
			}
			return typeParameterListSyntax;
		}

		// Token: 0x06004BE4 RID: 19428 RVA: 0x0013A880 File Offset: 0x00138A80
		public TypeParameterSyntax TypeParameter([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken varianceKeyword, SyntaxToken identifier)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8910, attributeLists.Node, varianceKeyword, identifier, this.context, out num);
			if (greenNode != null)
			{
				return (TypeParameterSyntax)greenNode;
			}
			TypeParameterSyntax typeParameterSyntax = new TypeParameterSyntax(SyntaxKind.TypeParameter, attributeLists.Node, varianceKeyword, identifier, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(typeParameterSyntax, num);
			}
			return typeParameterSyntax;
		}

		// Token: 0x06004BE5 RID: 19429 RVA: 0x0013A8DC File Offset: 0x00138ADC
		public ClassDeclarationSyntax ClassDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, SyntaxToken keyword, SyntaxToken identifier, [global::System.Runtime.CompilerServices.Nullable(2)] TypeParameterListSyntax typeParameterList, [global::System.Runtime.CompilerServices.Nullable(2)] BaseListSyntax baseList, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<MemberDeclarationSyntax> members, SyntaxToken closeBraceToken, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken semicolonToken)
		{
			return new ClassDeclarationSyntax(SyntaxKind.ClassDeclaration, attributeLists.Node, modifiers.Node, keyword, identifier, typeParameterList, baseList, constraintClauses.Node, openBraceToken, members.Node, closeBraceToken, semicolonToken, this.context);
		}

		// Token: 0x06004BE6 RID: 19430 RVA: 0x0013A924 File Offset: 0x00138B24
		public StructDeclarationSyntax StructDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, SyntaxToken keyword, SyntaxToken identifier, [global::System.Runtime.CompilerServices.Nullable(2)] TypeParameterListSyntax typeParameterList, [global::System.Runtime.CompilerServices.Nullable(2)] BaseListSyntax baseList, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<MemberDeclarationSyntax> members, SyntaxToken closeBraceToken, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken semicolonToken)
		{
			return new StructDeclarationSyntax(SyntaxKind.StructDeclaration, attributeLists.Node, modifiers.Node, keyword, identifier, typeParameterList, baseList, constraintClauses.Node, openBraceToken, members.Node, closeBraceToken, semicolonToken, this.context);
		}

		// Token: 0x06004BE7 RID: 19431 RVA: 0x0013A96C File Offset: 0x00138B6C
		public InterfaceDeclarationSyntax InterfaceDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, SyntaxToken keyword, SyntaxToken identifier, [global::System.Runtime.CompilerServices.Nullable(2)] TypeParameterListSyntax typeParameterList, [global::System.Runtime.CompilerServices.Nullable(2)] BaseListSyntax baseList, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<MemberDeclarationSyntax> members, SyntaxToken closeBraceToken, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken semicolonToken)
		{
			return new InterfaceDeclarationSyntax(SyntaxKind.InterfaceDeclaration, attributeLists.Node, modifiers.Node, keyword, identifier, typeParameterList, baseList, constraintClauses.Node, openBraceToken, members.Node, closeBraceToken, semicolonToken, this.context);
		}

		// Token: 0x06004BE8 RID: 19432 RVA: 0x0013A9B4 File Offset: 0x00138BB4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public RecordDeclarationSyntax RecordDeclaration(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken keyword, SyntaxToken classOrStructKeyword, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken identifier, TypeParameterListSyntax typeParameterList, ParameterListSyntax parameterList, BaseListSyntax baseList, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<MemberDeclarationSyntax> members, SyntaxToken closeBraceToken, SyntaxToken semicolonToken)
		{
			if (kind != SyntaxKind.RecordDeclaration && kind != SyntaxKind.RecordStructDeclaration)
			{
				throw new ArgumentException("kind");
			}
			return new RecordDeclarationSyntax(kind, attributeLists.Node, modifiers.Node, keyword, classOrStructKeyword, identifier, typeParameterList, parameterList, baseList, constraintClauses.Node, openBraceToken, members.Node, closeBraceToken, semicolonToken, this.context);
		}

		// Token: 0x06004BE9 RID: 19433 RVA: 0x0013AA18 File Offset: 0x00138C18
		public EnumDeclarationSyntax EnumDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, SyntaxToken enumKeyword, SyntaxToken identifier, [global::System.Runtime.CompilerServices.Nullable(2)] BaseListSyntax baseList, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<EnumMemberDeclarationSyntax> members, SyntaxToken closeBraceToken, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken semicolonToken)
		{
			return new EnumDeclarationSyntax(SyntaxKind.EnumDeclaration, attributeLists.Node, modifiers.Node, enumKeyword, identifier, baseList, openBraceToken, members.Node, closeBraceToken, semicolonToken, this.context);
		}

		// Token: 0x06004BEA RID: 19434 RVA: 0x0013AA58 File Offset: 0x00138C58
		public DelegateDeclarationSyntax DelegateDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, SyntaxToken delegateKeyword, TypeSyntax returnType, SyntaxToken identifier, [global::System.Runtime.CompilerServices.Nullable(2)] TypeParameterListSyntax typeParameterList, ParameterListSyntax parameterList, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, SyntaxToken semicolonToken)
		{
			return new DelegateDeclarationSyntax(SyntaxKind.DelegateDeclaration, attributeLists.Node, modifiers.Node, delegateKeyword, returnType, identifier, typeParameterList, parameterList, constraintClauses.Node, semicolonToken, this.context);
		}

		// Token: 0x06004BEB RID: 19435 RVA: 0x0013AA95 File Offset: 0x00138C95
		public EnumMemberDeclarationSyntax EnumMemberDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, SyntaxToken identifier, [global::System.Runtime.CompilerServices.Nullable(2)] EqualsValueClauseSyntax equalsValue)
		{
			return new EnumMemberDeclarationSyntax(SyntaxKind.EnumMemberDeclaration, attributeLists.Node, modifiers.Node, identifier, equalsValue, this.context);
		}

		// Token: 0x06004BEC RID: 19436 RVA: 0x0013AAB8 File Offset: 0x00138CB8
		public BaseListSyntax BaseList(SyntaxToken colonToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<BaseTypeSyntax> types)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8864, colonToken, types.Node, this.context, out num);
			if (greenNode != null)
			{
				return (BaseListSyntax)greenNode;
			}
			BaseListSyntax baseListSyntax = new BaseListSyntax(SyntaxKind.BaseList, colonToken, types.Node, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(baseListSyntax, num);
			}
			return baseListSyntax;
		}

		// Token: 0x06004BED RID: 19437 RVA: 0x0013AB10 File Offset: 0x00138D10
		public SimpleBaseTypeSyntax SimpleBaseType(TypeSyntax type)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8865, type, this.context, out num);
			if (greenNode != null)
			{
				return (SimpleBaseTypeSyntax)greenNode;
			}
			SimpleBaseTypeSyntax simpleBaseTypeSyntax = new SimpleBaseTypeSyntax(SyntaxKind.SimpleBaseType, type, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(simpleBaseTypeSyntax, num);
			}
			return simpleBaseTypeSyntax;
		}

		// Token: 0x06004BEE RID: 19438 RVA: 0x0013AB5C File Offset: 0x00138D5C
		public PrimaryConstructorBaseTypeSyntax PrimaryConstructorBaseType(TypeSyntax type, ArgumentListSyntax argumentList)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(9065, type, argumentList, this.context, out num);
			if (greenNode != null)
			{
				return (PrimaryConstructorBaseTypeSyntax)greenNode;
			}
			PrimaryConstructorBaseTypeSyntax primaryConstructorBaseTypeSyntax = new PrimaryConstructorBaseTypeSyntax(SyntaxKind.PrimaryConstructorBaseType, type, argumentList, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(primaryConstructorBaseTypeSyntax, num);
			}
			return primaryConstructorBaseTypeSyntax;
		}

		// Token: 0x06004BEF RID: 19439 RVA: 0x0013ABA7 File Offset: 0x00138DA7
		public TypeParameterConstraintClauseSyntax TypeParameterConstraintClause(SyntaxToken whereKeyword, IdentifierNameSyntax name, SyntaxToken colonToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<TypeParameterConstraintSyntax> constraints)
		{
			return new TypeParameterConstraintClauseSyntax(SyntaxKind.TypeParameterConstraintClause, whereKeyword, name, colonToken, constraints.Node, this.context);
		}

		// Token: 0x06004BF0 RID: 19440 RVA: 0x0013ABC4 File Offset: 0x00138DC4
		public ConstructorConstraintSyntax ConstructorConstraint(SyntaxToken newKeyword, SyntaxToken openParenToken, SyntaxToken closeParenToken)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8867, newKeyword, openParenToken, closeParenToken, this.context, out num);
			if (greenNode != null)
			{
				return (ConstructorConstraintSyntax)greenNode;
			}
			ConstructorConstraintSyntax constructorConstraintSyntax = new ConstructorConstraintSyntax(SyntaxKind.ConstructorConstraint, newKeyword, openParenToken, closeParenToken, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(constructorConstraintSyntax, num);
			}
			return constructorConstraintSyntax;
		}

		// Token: 0x06004BF1 RID: 19441 RVA: 0x0013AC14 File Offset: 0x00138E14
		public ClassOrStructConstraintSyntax ClassOrStructConstraint(SyntaxKind kind, SyntaxToken classOrStructKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken questionToken)
		{
			if (kind - SyntaxKind.ClassConstraint > 1)
			{
				throw new ArgumentException("kind");
			}
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode((int)kind, classOrStructKeyword, questionToken, this.context, out num);
			if (greenNode != null)
			{
				return (ClassOrStructConstraintSyntax)greenNode;
			}
			ClassOrStructConstraintSyntax classOrStructConstraintSyntax = new ClassOrStructConstraintSyntax(kind, classOrStructKeyword, questionToken, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(classOrStructConstraintSyntax, num);
			}
			return classOrStructConstraintSyntax;
		}

		// Token: 0x06004BF2 RID: 19442 RVA: 0x0013AC6C File Offset: 0x00138E6C
		public TypeConstraintSyntax TypeConstraint(TypeSyntax type)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8870, type, this.context, out num);
			if (greenNode != null)
			{
				return (TypeConstraintSyntax)greenNode;
			}
			TypeConstraintSyntax typeConstraintSyntax = new TypeConstraintSyntax(SyntaxKind.TypeConstraint, type, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(typeConstraintSyntax, num);
			}
			return typeConstraintSyntax;
		}

		// Token: 0x06004BF3 RID: 19443 RVA: 0x0013ACB8 File Offset: 0x00138EB8
		public DefaultConstraintSyntax DefaultConstraint(SyntaxToken defaultKeyword)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(9064, defaultKeyword, this.context, out num);
			if (greenNode != null)
			{
				return (DefaultConstraintSyntax)greenNode;
			}
			DefaultConstraintSyntax defaultConstraintSyntax = new DefaultConstraintSyntax(SyntaxKind.DefaultConstraint, defaultKeyword, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(defaultConstraintSyntax, num);
			}
			return defaultConstraintSyntax;
		}

		// Token: 0x06004BF4 RID: 19444 RVA: 0x0013AD01 File Offset: 0x00138F01
		public FieldDeclarationSyntax FieldDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, VariableDeclarationSyntax declaration, SyntaxToken semicolonToken)
		{
			return new FieldDeclarationSyntax(SyntaxKind.FieldDeclaration, attributeLists.Node, modifiers.Node, declaration, semicolonToken, this.context);
		}

		// Token: 0x06004BF5 RID: 19445 RVA: 0x0013AD24 File Offset: 0x00138F24
		public EventFieldDeclarationSyntax EventFieldDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, SyntaxToken eventKeyword, VariableDeclarationSyntax declaration, SyntaxToken semicolonToken)
		{
			return new EventFieldDeclarationSyntax(SyntaxKind.EventFieldDeclaration, attributeLists.Node, modifiers.Node, eventKeyword, declaration, semicolonToken, this.context);
		}

		// Token: 0x06004BF6 RID: 19446 RVA: 0x0013AD4C File Offset: 0x00138F4C
		public ExplicitInterfaceSpecifierSyntax ExplicitInterfaceSpecifier(NameSyntax name, SyntaxToken dotToken)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8871, name, dotToken, this.context, out num);
			if (greenNode != null)
			{
				return (ExplicitInterfaceSpecifierSyntax)greenNode;
			}
			ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifierSyntax = new ExplicitInterfaceSpecifierSyntax(SyntaxKind.ExplicitInterfaceSpecifier, name, dotToken, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(explicitInterfaceSpecifierSyntax, num);
			}
			return explicitInterfaceSpecifierSyntax;
		}

		// Token: 0x06004BF7 RID: 19447 RVA: 0x0013AD98 File Offset: 0x00138F98
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public MethodDeclarationSyntax MethodDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] TypeSyntax returnType, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken identifier, TypeParameterListSyntax typeParameterList, [global::System.Runtime.CompilerServices.Nullable(1)] ParameterListSyntax parameterList, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
		{
			return new MethodDeclarationSyntax(SyntaxKind.MethodDeclaration, attributeLists.Node, modifiers.Node, returnType, explicitInterfaceSpecifier, identifier, typeParameterList, parameterList, constraintClauses.Node, body, expressionBody, semicolonToken, this.context);
		}

		// Token: 0x06004BF8 RID: 19448 RVA: 0x0013ADDC File Offset: 0x00138FDC
		public OperatorDeclarationSyntax OperatorDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, TypeSyntax returnType, SyntaxToken operatorKeyword, SyntaxToken operatorToken, ParameterListSyntax parameterList, [global::System.Runtime.CompilerServices.Nullable(2)] BlockSyntax body, [global::System.Runtime.CompilerServices.Nullable(2)] ArrowExpressionClauseSyntax expressionBody, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken semicolonToken)
		{
			return new OperatorDeclarationSyntax(SyntaxKind.OperatorDeclaration, attributeLists.Node, modifiers.Node, returnType, operatorKeyword, operatorToken, parameterList, body, expressionBody, semicolonToken, this.context);
		}

		// Token: 0x06004BF9 RID: 19449 RVA: 0x0013AE14 File Offset: 0x00139014
		public ConversionOperatorDeclarationSyntax ConversionOperatorDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, SyntaxToken implicitOrExplicitKeyword, SyntaxToken operatorKeyword, TypeSyntax type, ParameterListSyntax parameterList, [global::System.Runtime.CompilerServices.Nullable(2)] BlockSyntax body, [global::System.Runtime.CompilerServices.Nullable(2)] ArrowExpressionClauseSyntax expressionBody, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken semicolonToken)
		{
			return new ConversionOperatorDeclarationSyntax(SyntaxKind.ConversionOperatorDeclaration, attributeLists.Node, modifiers.Node, implicitOrExplicitKeyword, operatorKeyword, type, parameterList, body, expressionBody, semicolonToken, this.context);
		}

		// Token: 0x06004BFA RID: 19450 RVA: 0x0013AE4C File Offset: 0x0013904C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public ConstructorDeclarationSyntax ConstructorDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken identifier, [global::System.Runtime.CompilerServices.Nullable(1)] ParameterListSyntax parameterList, ConstructorInitializerSyntax initializer, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
		{
			return new ConstructorDeclarationSyntax(SyntaxKind.ConstructorDeclaration, attributeLists.Node, modifiers.Node, identifier, parameterList, initializer, body, expressionBody, semicolonToken, this.context);
		}

		// Token: 0x06004BFB RID: 19451 RVA: 0x0013AE84 File Offset: 0x00139084
		public ConstructorInitializerSyntax ConstructorInitializer(SyntaxKind kind, SyntaxToken colonToken, SyntaxToken thisOrBaseKeyword, ArgumentListSyntax argumentList)
		{
			if (kind - SyntaxKind.BaseConstructorInitializer > 1)
			{
				throw new ArgumentException("kind");
			}
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode((int)kind, colonToken, thisOrBaseKeyword, argumentList, this.context, out num);
			if (greenNode != null)
			{
				return (ConstructorInitializerSyntax)greenNode;
			}
			ConstructorInitializerSyntax constructorInitializerSyntax = new ConstructorInitializerSyntax(kind, colonToken, thisOrBaseKeyword, argumentList, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(constructorInitializerSyntax, num);
			}
			return constructorInitializerSyntax;
		}

		// Token: 0x06004BFC RID: 19452 RVA: 0x0013AEE0 File Offset: 0x001390E0
		public DestructorDeclarationSyntax DestructorDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, SyntaxToken tildeToken, SyntaxToken identifier, ParameterListSyntax parameterList, [global::System.Runtime.CompilerServices.Nullable(2)] BlockSyntax body, [global::System.Runtime.CompilerServices.Nullable(2)] ArrowExpressionClauseSyntax expressionBody, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken semicolonToken)
		{
			return new DestructorDeclarationSyntax(SyntaxKind.DestructorDeclaration, attributeLists.Node, modifiers.Node, tildeToken, identifier, parameterList, body, expressionBody, semicolonToken, this.context);
		}

		// Token: 0x06004BFD RID: 19453 RVA: 0x0013AF18 File Offset: 0x00139118
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public PropertyDeclarationSyntax PropertyDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] TypeSyntax type, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken identifier, AccessorListSyntax accessorList, ArrowExpressionClauseSyntax expressionBody, EqualsValueClauseSyntax initializer, SyntaxToken semicolonToken)
		{
			return new PropertyDeclarationSyntax(SyntaxKind.PropertyDeclaration, attributeLists.Node, modifiers.Node, type, explicitInterfaceSpecifier, identifier, accessorList, expressionBody, initializer, semicolonToken, this.context);
		}

		// Token: 0x06004BFE RID: 19454 RVA: 0x0013AF50 File Offset: 0x00139150
		public ArrowExpressionClauseSyntax ArrowExpressionClause(SyntaxToken arrowToken, ExpressionSyntax expression)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8917, arrowToken, expression, this.context, out num);
			if (greenNode != null)
			{
				return (ArrowExpressionClauseSyntax)greenNode;
			}
			ArrowExpressionClauseSyntax arrowExpressionClauseSyntax = new ArrowExpressionClauseSyntax(SyntaxKind.ArrowExpressionClause, arrowToken, expression, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(arrowExpressionClauseSyntax, num);
			}
			return arrowExpressionClauseSyntax;
		}

		// Token: 0x06004BFF RID: 19455 RVA: 0x0013AF9C File Offset: 0x0013919C
		public EventDeclarationSyntax EventDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, SyntaxToken eventKeyword, TypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(2)] ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, SyntaxToken identifier, [global::System.Runtime.CompilerServices.Nullable(2)] AccessorListSyntax accessorList, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken semicolonToken)
		{
			return new EventDeclarationSyntax(SyntaxKind.EventDeclaration, attributeLists.Node, modifiers.Node, eventKeyword, type, explicitInterfaceSpecifier, identifier, accessorList, semicolonToken, this.context);
		}

		// Token: 0x06004C00 RID: 19456 RVA: 0x0013AFD4 File Offset: 0x001391D4
		public IndexerDeclarationSyntax IndexerDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, TypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(2)] ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, SyntaxToken thisKeyword, BracketedParameterListSyntax parameterList, [global::System.Runtime.CompilerServices.Nullable(2)] AccessorListSyntax accessorList, [global::System.Runtime.CompilerServices.Nullable(2)] ArrowExpressionClauseSyntax expressionBody, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken semicolonToken)
		{
			return new IndexerDeclarationSyntax(SyntaxKind.IndexerDeclaration, attributeLists.Node, modifiers.Node, type, explicitInterfaceSpecifier, thisKeyword, parameterList, accessorList, expressionBody, semicolonToken, this.context);
		}

		// Token: 0x06004C01 RID: 19457 RVA: 0x0013B00C File Offset: 0x0013920C
		public AccessorListSyntax AccessorList(SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AccessorDeclarationSyntax> accessors, SyntaxToken closeBraceToken)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8895, openBraceToken, accessors.Node, closeBraceToken, this.context, out num);
			if (greenNode != null)
			{
				return (AccessorListSyntax)greenNode;
			}
			AccessorListSyntax accessorListSyntax = new AccessorListSyntax(SyntaxKind.AccessorList, openBraceToken, accessors.Node, closeBraceToken, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(accessorListSyntax, num);
			}
			return accessorListSyntax;
		}

		// Token: 0x06004C02 RID: 19458 RVA: 0x0013B068 File Offset: 0x00139268
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public AccessorDeclarationSyntax AccessorDeclaration(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken keyword, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
		{
			if (kind - SyntaxKind.GetAccessorDeclaration > 4 && kind != SyntaxKind.InitAccessorDeclaration)
			{
				throw new ArgumentException("kind");
			}
			return new AccessorDeclarationSyntax(kind, attributeLists.Node, modifiers.Node, keyword, body, expressionBody, semicolonToken, this.context);
		}

		// Token: 0x06004C03 RID: 19459 RVA: 0x0013B0B4 File Offset: 0x001392B4
		public ParameterListSyntax ParameterList(SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<ParameterSyntax> parameters, SyntaxToken closeParenToken)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8906, openParenToken, parameters.Node, closeParenToken, this.context, out num);
			if (greenNode != null)
			{
				return (ParameterListSyntax)greenNode;
			}
			ParameterListSyntax parameterListSyntax = new ParameterListSyntax(SyntaxKind.ParameterList, openParenToken, parameters.Node, closeParenToken, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(parameterListSyntax, num);
			}
			return parameterListSyntax;
		}

		// Token: 0x06004C04 RID: 19460 RVA: 0x0013B110 File Offset: 0x00139310
		public BracketedParameterListSyntax BracketedParameterList(SyntaxToken openBracketToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<ParameterSyntax> parameters, SyntaxToken closeBracketToken)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8907, openBracketToken, parameters.Node, closeBracketToken, this.context, out num);
			if (greenNode != null)
			{
				return (BracketedParameterListSyntax)greenNode;
			}
			BracketedParameterListSyntax bracketedParameterListSyntax = new BracketedParameterListSyntax(SyntaxKind.BracketedParameterList, openBracketToken, parameters.Node, closeBracketToken, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(bracketedParameterListSyntax, num);
			}
			return bracketedParameterListSyntax;
		}

		// Token: 0x06004C05 RID: 19461 RVA: 0x0013B169 File Offset: 0x00139369
		public ParameterSyntax Parameter([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSyntax type, SyntaxToken identifier, [global::System.Runtime.CompilerServices.Nullable(2)] EqualsValueClauseSyntax @default)
		{
			return new ParameterSyntax(SyntaxKind.Parameter, attributeLists.Node, modifiers.Node, type, identifier, @default, this.context);
		}

		// Token: 0x06004C06 RID: 19462 RVA: 0x0013B190 File Offset: 0x00139390
		public FunctionPointerParameterSyntax FunctionPointerParameter([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, TypeSyntax type)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(9057, attributeLists.Node, modifiers.Node, type, this.context, out num);
			if (greenNode != null)
			{
				return (FunctionPointerParameterSyntax)greenNode;
			}
			FunctionPointerParameterSyntax functionPointerParameterSyntax = new FunctionPointerParameterSyntax(SyntaxKind.FunctionPointerParameter, attributeLists.Node, modifiers.Node, type, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(functionPointerParameterSyntax, num);
			}
			return functionPointerParameterSyntax;
		}

		// Token: 0x06004C07 RID: 19463 RVA: 0x0013B1F5 File Offset: 0x001393F5
		public IncompleteMemberSyntax IncompleteMember([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSyntax type)
		{
			return new IncompleteMemberSyntax(SyntaxKind.IncompleteMember, attributeLists.Node, modifiers.Node, type, this.context);
		}

		// Token: 0x06004C08 RID: 19464 RVA: 0x0013B216 File Offset: 0x00139416
		public SkippedTokensTriviaSyntax SkippedTokensTrivia([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> tokens)
		{
			return new SkippedTokensTriviaSyntax(SyntaxKind.SkippedTokensTrivia, tokens.Node, this.context);
		}

		// Token: 0x06004C09 RID: 19465 RVA: 0x0013B22F File Offset: 0x0013942F
		public DocumentationCommentTriviaSyntax DocumentationCommentTrivia(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<XmlNodeSyntax> content, SyntaxToken endOfComment)
		{
			if (kind - SyntaxKind.SingleLineDocumentationCommentTrivia > 1)
			{
				throw new ArgumentException("kind");
			}
			return new DocumentationCommentTriviaSyntax(kind, content.Node, endOfComment, this.context);
		}

		// Token: 0x06004C0A RID: 19466 RVA: 0x0013B25C File Offset: 0x0013945C
		public TypeCrefSyntax TypeCref(TypeSyntax type)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8597, type, this.context, out num);
			if (greenNode != null)
			{
				return (TypeCrefSyntax)greenNode;
			}
			TypeCrefSyntax typeCrefSyntax = new TypeCrefSyntax(SyntaxKind.TypeCref, type, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(typeCrefSyntax, num);
			}
			return typeCrefSyntax;
		}

		// Token: 0x06004C0B RID: 19467 RVA: 0x0013B2A8 File Offset: 0x001394A8
		public QualifiedCrefSyntax QualifiedCref(TypeSyntax container, SyntaxToken dotToken, MemberCrefSyntax member)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8598, container, dotToken, member, this.context, out num);
			if (greenNode != null)
			{
				return (QualifiedCrefSyntax)greenNode;
			}
			QualifiedCrefSyntax qualifiedCrefSyntax = new QualifiedCrefSyntax(SyntaxKind.QualifiedCref, container, dotToken, member, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(qualifiedCrefSyntax, num);
			}
			return qualifiedCrefSyntax;
		}

		// Token: 0x06004C0C RID: 19468 RVA: 0x0013B2F8 File Offset: 0x001394F8
		public NameMemberCrefSyntax NameMemberCref(TypeSyntax name, [global::System.Runtime.CompilerServices.Nullable(2)] CrefParameterListSyntax parameters)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8599, name, parameters, this.context, out num);
			if (greenNode != null)
			{
				return (NameMemberCrefSyntax)greenNode;
			}
			NameMemberCrefSyntax nameMemberCrefSyntax = new NameMemberCrefSyntax(SyntaxKind.NameMemberCref, name, parameters, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(nameMemberCrefSyntax, num);
			}
			return nameMemberCrefSyntax;
		}

		// Token: 0x06004C0D RID: 19469 RVA: 0x0013B344 File Offset: 0x00139544
		public IndexerMemberCrefSyntax IndexerMemberCref(SyntaxToken thisKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] CrefBracketedParameterListSyntax parameters)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8600, thisKeyword, parameters, this.context, out num);
			if (greenNode != null)
			{
				return (IndexerMemberCrefSyntax)greenNode;
			}
			IndexerMemberCrefSyntax indexerMemberCrefSyntax = new IndexerMemberCrefSyntax(SyntaxKind.IndexerMemberCref, thisKeyword, parameters, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(indexerMemberCrefSyntax, num);
			}
			return indexerMemberCrefSyntax;
		}

		// Token: 0x06004C0E RID: 19470 RVA: 0x0013B390 File Offset: 0x00139590
		public OperatorMemberCrefSyntax OperatorMemberCref(SyntaxToken operatorKeyword, SyntaxToken operatorToken, [global::System.Runtime.CompilerServices.Nullable(2)] CrefParameterListSyntax parameters)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8601, operatorKeyword, operatorToken, parameters, this.context, out num);
			if (greenNode != null)
			{
				return (OperatorMemberCrefSyntax)greenNode;
			}
			OperatorMemberCrefSyntax operatorMemberCrefSyntax = new OperatorMemberCrefSyntax(SyntaxKind.OperatorMemberCref, operatorKeyword, operatorToken, parameters, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(operatorMemberCrefSyntax, num);
			}
			return operatorMemberCrefSyntax;
		}

		// Token: 0x06004C0F RID: 19471 RVA: 0x0013B3DD File Offset: 0x001395DD
		public ConversionOperatorMemberCrefSyntax ConversionOperatorMemberCref(SyntaxToken implicitOrExplicitKeyword, SyntaxToken operatorKeyword, TypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(2)] CrefParameterListSyntax parameters)
		{
			return new ConversionOperatorMemberCrefSyntax(SyntaxKind.ConversionOperatorMemberCref, implicitOrExplicitKeyword, operatorKeyword, type, parameters, this.context);
		}

		// Token: 0x06004C10 RID: 19472 RVA: 0x0013B3F4 File Offset: 0x001395F4
		public CrefParameterListSyntax CrefParameterList(SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<CrefParameterSyntax> parameters, SyntaxToken closeParenToken)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8603, openParenToken, parameters.Node, closeParenToken, this.context, out num);
			if (greenNode != null)
			{
				return (CrefParameterListSyntax)greenNode;
			}
			CrefParameterListSyntax crefParameterListSyntax = new CrefParameterListSyntax(SyntaxKind.CrefParameterList, openParenToken, parameters.Node, closeParenToken, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(crefParameterListSyntax, num);
			}
			return crefParameterListSyntax;
		}

		// Token: 0x06004C11 RID: 19473 RVA: 0x0013B450 File Offset: 0x00139650
		public CrefBracketedParameterListSyntax CrefBracketedParameterList(SyntaxToken openBracketToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<CrefParameterSyntax> parameters, SyntaxToken closeBracketToken)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8604, openBracketToken, parameters.Node, closeBracketToken, this.context, out num);
			if (greenNode != null)
			{
				return (CrefBracketedParameterListSyntax)greenNode;
			}
			CrefBracketedParameterListSyntax crefBracketedParameterListSyntax = new CrefBracketedParameterListSyntax(SyntaxKind.CrefBracketedParameterList, openBracketToken, parameters.Node, closeBracketToken, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(crefBracketedParameterListSyntax, num);
			}
			return crefBracketedParameterListSyntax;
		}

		// Token: 0x06004C12 RID: 19474 RVA: 0x0013B4AC File Offset: 0x001396AC
		public CrefParameterSyntax CrefParameter([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken refKindKeyword, TypeSyntax type)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8605, refKindKeyword, type, this.context, out num);
			if (greenNode != null)
			{
				return (CrefParameterSyntax)greenNode;
			}
			CrefParameterSyntax crefParameterSyntax = new CrefParameterSyntax(SyntaxKind.CrefParameter, refKindKeyword, type, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(crefParameterSyntax, num);
			}
			return crefParameterSyntax;
		}

		// Token: 0x06004C13 RID: 19475 RVA: 0x0013B4F8 File Offset: 0x001396F8
		public XmlElementSyntax XmlElement(XmlElementStartTagSyntax startTag, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<XmlNodeSyntax> content, XmlElementEndTagSyntax endTag)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8574, startTag, content.Node, endTag, this.context, out num);
			if (greenNode != null)
			{
				return (XmlElementSyntax)greenNode;
			}
			XmlElementSyntax xmlElementSyntax = new XmlElementSyntax(SyntaxKind.XmlElement, startTag, content.Node, endTag, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(xmlElementSyntax, num);
			}
			return xmlElementSyntax;
		}

		// Token: 0x06004C14 RID: 19476 RVA: 0x0013B551 File Offset: 0x00139751
		public XmlElementStartTagSyntax XmlElementStartTag(SyntaxToken lessThanToken, XmlNameSyntax name, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<XmlAttributeSyntax> attributes, SyntaxToken greaterThanToken)
		{
			return new XmlElementStartTagSyntax(SyntaxKind.XmlElementStartTag, lessThanToken, name, attributes.Node, greaterThanToken, this.context);
		}

		// Token: 0x06004C15 RID: 19477 RVA: 0x0013B570 File Offset: 0x00139770
		public XmlElementEndTagSyntax XmlElementEndTag(SyntaxToken lessThanSlashToken, XmlNameSyntax name, SyntaxToken greaterThanToken)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8576, lessThanSlashToken, name, greaterThanToken, this.context, out num);
			if (greenNode != null)
			{
				return (XmlElementEndTagSyntax)greenNode;
			}
			XmlElementEndTagSyntax xmlElementEndTagSyntax = new XmlElementEndTagSyntax(SyntaxKind.XmlElementEndTag, lessThanSlashToken, name, greaterThanToken, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(xmlElementEndTagSyntax, num);
			}
			return xmlElementEndTagSyntax;
		}

		// Token: 0x06004C16 RID: 19478 RVA: 0x0013B5BD File Offset: 0x001397BD
		public XmlEmptyElementSyntax XmlEmptyElement(SyntaxToken lessThanToken, XmlNameSyntax name, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<XmlAttributeSyntax> attributes, SyntaxToken slashGreaterThanToken)
		{
			return new XmlEmptyElementSyntax(SyntaxKind.XmlEmptyElement, lessThanToken, name, attributes.Node, slashGreaterThanToken, this.context);
		}

		// Token: 0x06004C17 RID: 19479 RVA: 0x0013B5DC File Offset: 0x001397DC
		public XmlNameSyntax XmlName([global::System.Runtime.CompilerServices.Nullable(2)] XmlPrefixSyntax prefix, SyntaxToken localName)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8581, prefix, localName, this.context, out num);
			if (greenNode != null)
			{
				return (XmlNameSyntax)greenNode;
			}
			XmlNameSyntax xmlNameSyntax = new XmlNameSyntax(SyntaxKind.XmlName, prefix, localName, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(xmlNameSyntax, num);
			}
			return xmlNameSyntax;
		}

		// Token: 0x06004C18 RID: 19480 RVA: 0x0013B628 File Offset: 0x00139828
		public XmlPrefixSyntax XmlPrefix(SyntaxToken prefix, SyntaxToken colonToken)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8582, prefix, colonToken, this.context, out num);
			if (greenNode != null)
			{
				return (XmlPrefixSyntax)greenNode;
			}
			XmlPrefixSyntax xmlPrefixSyntax = new XmlPrefixSyntax(SyntaxKind.XmlPrefix, prefix, colonToken, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(xmlPrefixSyntax, num);
			}
			return xmlPrefixSyntax;
		}

		// Token: 0x06004C19 RID: 19481 RVA: 0x0013B673 File Offset: 0x00139873
		public XmlTextAttributeSyntax XmlTextAttribute(XmlNameSyntax name, SyntaxToken equalsToken, SyntaxToken startQuoteToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> textTokens, SyntaxToken endQuoteToken)
		{
			return new XmlTextAttributeSyntax(SyntaxKind.XmlTextAttribute, name, equalsToken, startQuoteToken, textTokens.Node, endQuoteToken, this.context);
		}

		// Token: 0x06004C1A RID: 19482 RVA: 0x0013B691 File Offset: 0x00139891
		public XmlCrefAttributeSyntax XmlCrefAttribute(XmlNameSyntax name, SyntaxToken equalsToken, SyntaxToken startQuoteToken, CrefSyntax cref, SyntaxToken endQuoteToken)
		{
			return new XmlCrefAttributeSyntax(SyntaxKind.XmlCrefAttribute, name, equalsToken, startQuoteToken, cref, endQuoteToken, this.context);
		}

		// Token: 0x06004C1B RID: 19483 RVA: 0x0013B6AA File Offset: 0x001398AA
		public XmlNameAttributeSyntax XmlNameAttribute(XmlNameSyntax name, SyntaxToken equalsToken, SyntaxToken startQuoteToken, IdentifierNameSyntax identifier, SyntaxToken endQuoteToken)
		{
			return new XmlNameAttributeSyntax(SyntaxKind.XmlNameAttribute, name, equalsToken, startQuoteToken, identifier, endQuoteToken, this.context);
		}

		// Token: 0x06004C1C RID: 19484 RVA: 0x0013B6C4 File Offset: 0x001398C4
		public XmlTextSyntax XmlText([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> textTokens)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8583, textTokens.Node, this.context, out num);
			if (greenNode != null)
			{
				return (XmlTextSyntax)greenNode;
			}
			XmlTextSyntax xmlTextSyntax = new XmlTextSyntax(SyntaxKind.XmlText, textTokens.Node, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(xmlTextSyntax, num);
			}
			return xmlTextSyntax;
		}

		// Token: 0x06004C1D RID: 19485 RVA: 0x0013B71C File Offset: 0x0013991C
		public XmlCDataSectionSyntax XmlCDataSection(SyntaxToken startCDataToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> textTokens, SyntaxToken endCDataToken)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8584, startCDataToken, textTokens.Node, endCDataToken, this.context, out num);
			if (greenNode != null)
			{
				return (XmlCDataSectionSyntax)greenNode;
			}
			XmlCDataSectionSyntax xmlCDataSectionSyntax = new XmlCDataSectionSyntax(SyntaxKind.XmlCDataSection, startCDataToken, textTokens.Node, endCDataToken, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(xmlCDataSectionSyntax, num);
			}
			return xmlCDataSectionSyntax;
		}

		// Token: 0x06004C1E RID: 19486 RVA: 0x0013B775 File Offset: 0x00139975
		public XmlProcessingInstructionSyntax XmlProcessingInstruction(SyntaxToken startProcessingInstructionToken, XmlNameSyntax name, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> textTokens, SyntaxToken endProcessingInstructionToken)
		{
			return new XmlProcessingInstructionSyntax(SyntaxKind.XmlProcessingInstruction, startProcessingInstructionToken, name, textTokens.Node, endProcessingInstructionToken, this.context);
		}

		// Token: 0x06004C1F RID: 19487 RVA: 0x0013B794 File Offset: 0x00139994
		public XmlCommentSyntax XmlComment(SyntaxToken lessThanExclamationMinusMinusToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> textTokens, SyntaxToken minusMinusGreaterThanToken)
		{
			int num;
			GreenNode greenNode = CSharpSyntaxNodeCache.TryGetNode(8585, lessThanExclamationMinusMinusToken, textTokens.Node, minusMinusGreaterThanToken, this.context, out num);
			if (greenNode != null)
			{
				return (XmlCommentSyntax)greenNode;
			}
			XmlCommentSyntax xmlCommentSyntax = new XmlCommentSyntax(SyntaxKind.XmlComment, lessThanExclamationMinusMinusToken, textTokens.Node, minusMinusGreaterThanToken, this.context);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(xmlCommentSyntax, num);
			}
			return xmlCommentSyntax;
		}

		// Token: 0x06004C20 RID: 19488 RVA: 0x0013B7F0 File Offset: 0x001399F0
		public IfDirectiveTriviaSyntax IfDirectiveTrivia(SyntaxToken hashToken, SyntaxToken ifKeyword, ExpressionSyntax condition, SyntaxToken endOfDirectiveToken, bool isActive, bool branchTaken, bool conditionValue)
		{
			return new IfDirectiveTriviaSyntax(SyntaxKind.IfDirectiveTrivia, hashToken, ifKeyword, condition, endOfDirectiveToken, isActive, branchTaken, conditionValue, this.context);
		}

		// Token: 0x06004C21 RID: 19489 RVA: 0x0013B818 File Offset: 0x00139A18
		public ElifDirectiveTriviaSyntax ElifDirectiveTrivia(SyntaxToken hashToken, SyntaxToken elifKeyword, ExpressionSyntax condition, SyntaxToken endOfDirectiveToken, bool isActive, bool branchTaken, bool conditionValue)
		{
			return new ElifDirectiveTriviaSyntax(SyntaxKind.ElifDirectiveTrivia, hashToken, elifKeyword, condition, endOfDirectiveToken, isActive, branchTaken, conditionValue, this.context);
		}

		// Token: 0x06004C22 RID: 19490 RVA: 0x0013B840 File Offset: 0x00139A40
		public ElseDirectiveTriviaSyntax ElseDirectiveTrivia(SyntaxToken hashToken, SyntaxToken elseKeyword, SyntaxToken endOfDirectiveToken, bool isActive, bool branchTaken)
		{
			return new ElseDirectiveTriviaSyntax(SyntaxKind.ElseDirectiveTrivia, hashToken, elseKeyword, endOfDirectiveToken, isActive, branchTaken, this.context);
		}

		// Token: 0x06004C23 RID: 19491 RVA: 0x0013B859 File Offset: 0x00139A59
		public EndIfDirectiveTriviaSyntax EndIfDirectiveTrivia(SyntaxToken hashToken, SyntaxToken endIfKeyword, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			return new EndIfDirectiveTriviaSyntax(SyntaxKind.EndIfDirectiveTrivia, hashToken, endIfKeyword, endOfDirectiveToken, isActive, this.context);
		}

		// Token: 0x06004C24 RID: 19492 RVA: 0x0013B870 File Offset: 0x00139A70
		public RegionDirectiveTriviaSyntax RegionDirectiveTrivia(SyntaxToken hashToken, SyntaxToken regionKeyword, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			return new RegionDirectiveTriviaSyntax(SyntaxKind.RegionDirectiveTrivia, hashToken, regionKeyword, endOfDirectiveToken, isActive, this.context);
		}

		// Token: 0x06004C25 RID: 19493 RVA: 0x0013B887 File Offset: 0x00139A87
		public EndRegionDirectiveTriviaSyntax EndRegionDirectiveTrivia(SyntaxToken hashToken, SyntaxToken endRegionKeyword, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			return new EndRegionDirectiveTriviaSyntax(SyntaxKind.EndRegionDirectiveTrivia, hashToken, endRegionKeyword, endOfDirectiveToken, isActive, this.context);
		}

		// Token: 0x06004C26 RID: 19494 RVA: 0x0013B89E File Offset: 0x00139A9E
		public ErrorDirectiveTriviaSyntax ErrorDirectiveTrivia(SyntaxToken hashToken, SyntaxToken errorKeyword, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			return new ErrorDirectiveTriviaSyntax(SyntaxKind.ErrorDirectiveTrivia, hashToken, errorKeyword, endOfDirectiveToken, isActive, this.context);
		}

		// Token: 0x06004C27 RID: 19495 RVA: 0x0013B8B5 File Offset: 0x00139AB5
		public WarningDirectiveTriviaSyntax WarningDirectiveTrivia(SyntaxToken hashToken, SyntaxToken warningKeyword, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			return new WarningDirectiveTriviaSyntax(SyntaxKind.WarningDirectiveTrivia, hashToken, warningKeyword, endOfDirectiveToken, isActive, this.context);
		}

		// Token: 0x06004C28 RID: 19496 RVA: 0x0013B8CC File Offset: 0x00139ACC
		public BadDirectiveTriviaSyntax BadDirectiveTrivia(SyntaxToken hashToken, SyntaxToken identifier, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			return new BadDirectiveTriviaSyntax(SyntaxKind.BadDirectiveTrivia, hashToken, identifier, endOfDirectiveToken, isActive, this.context);
		}

		// Token: 0x06004C29 RID: 19497 RVA: 0x0013B8E3 File Offset: 0x00139AE3
		public DefineDirectiveTriviaSyntax DefineDirectiveTrivia(SyntaxToken hashToken, SyntaxToken defineKeyword, SyntaxToken name, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			return new DefineDirectiveTriviaSyntax(SyntaxKind.DefineDirectiveTrivia, hashToken, defineKeyword, name, endOfDirectiveToken, isActive, this.context);
		}

		// Token: 0x06004C2A RID: 19498 RVA: 0x0013B8FC File Offset: 0x00139AFC
		public UndefDirectiveTriviaSyntax UndefDirectiveTrivia(SyntaxToken hashToken, SyntaxToken undefKeyword, SyntaxToken name, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			return new UndefDirectiveTriviaSyntax(SyntaxKind.UndefDirectiveTrivia, hashToken, undefKeyword, name, endOfDirectiveToken, isActive, this.context);
		}

		// Token: 0x06004C2B RID: 19499 RVA: 0x0013B915 File Offset: 0x00139B15
		public LineDirectiveTriviaSyntax LineDirectiveTrivia(SyntaxToken hashToken, SyntaxToken lineKeyword, SyntaxToken line, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken file, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			return new LineDirectiveTriviaSyntax(SyntaxKind.LineDirectiveTrivia, hashToken, lineKeyword, line, file, endOfDirectiveToken, isActive, this.context);
		}

		// Token: 0x06004C2C RID: 19500 RVA: 0x0013B930 File Offset: 0x00139B30
		public PragmaWarningDirectiveTriviaSyntax PragmaWarningDirectiveTrivia(SyntaxToken hashToken, SyntaxToken pragmaKeyword, SyntaxToken warningKeyword, SyntaxToken disableOrRestoreKeyword, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<ExpressionSyntax> errorCodes, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			return new PragmaWarningDirectiveTriviaSyntax(SyntaxKind.PragmaWarningDirectiveTrivia, hashToken, pragmaKeyword, warningKeyword, disableOrRestoreKeyword, errorCodes.Node, endOfDirectiveToken, isActive, this.context);
		}

		// Token: 0x06004C2D RID: 19501 RVA: 0x0013B960 File Offset: 0x00139B60
		public PragmaChecksumDirectiveTriviaSyntax PragmaChecksumDirectiveTrivia(SyntaxToken hashToken, SyntaxToken pragmaKeyword, SyntaxToken checksumKeyword, SyntaxToken file, SyntaxToken guid, SyntaxToken bytes, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			return new PragmaChecksumDirectiveTriviaSyntax(SyntaxKind.PragmaChecksumDirectiveTrivia, hashToken, pragmaKeyword, checksumKeyword, file, guid, bytes, endOfDirectiveToken, isActive, this.context);
		}

		// Token: 0x06004C2E RID: 19502 RVA: 0x0013B98A File Offset: 0x00139B8A
		public ReferenceDirectiveTriviaSyntax ReferenceDirectiveTrivia(SyntaxToken hashToken, SyntaxToken referenceKeyword, SyntaxToken file, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			return new ReferenceDirectiveTriviaSyntax(SyntaxKind.ReferenceDirectiveTrivia, hashToken, referenceKeyword, file, endOfDirectiveToken, isActive, this.context);
		}

		// Token: 0x06004C2F RID: 19503 RVA: 0x0013B9A3 File Offset: 0x00139BA3
		public LoadDirectiveTriviaSyntax LoadDirectiveTrivia(SyntaxToken hashToken, SyntaxToken loadKeyword, SyntaxToken file, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			return new LoadDirectiveTriviaSyntax(SyntaxKind.LoadDirectiveTrivia, hashToken, loadKeyword, file, endOfDirectiveToken, isActive, this.context);
		}

		// Token: 0x06004C30 RID: 19504 RVA: 0x0013B9BC File Offset: 0x00139BBC
		public ShebangDirectiveTriviaSyntax ShebangDirectiveTrivia(SyntaxToken hashToken, SyntaxToken exclamationToken, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			return new ShebangDirectiveTriviaSyntax(SyntaxKind.ShebangDirectiveTrivia, hashToken, exclamationToken, endOfDirectiveToken, isActive, this.context);
		}

		// Token: 0x06004C31 RID: 19505 RVA: 0x0013B9D3 File Offset: 0x00139BD3
		public NullableDirectiveTriviaSyntax NullableDirectiveTrivia(SyntaxToken hashToken, SyntaxToken nullableKeyword, SyntaxToken settingToken, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken targetToken, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			return new NullableDirectiveTriviaSyntax(SyntaxKind.NullableDirectiveTrivia, hashToken, nullableKeyword, settingToken, targetToken, endOfDirectiveToken, isActive, this.context);
		}

		// Token: 0x040015BB RID: 5563
		private SyntaxFactoryContext context;
	}
}
