using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000108 RID: 264
	internal abstract class CSharpSemanticModel : SemanticModel
	{
		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06001499 RID: 5273
		public new abstract CSharpCompilation Compilation { get; }

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x0600149A RID: 5274
		internal new abstract CSharpSyntaxNode Root { get; }

		// Token: 0x0600149B RID: 5275 RVA: 0x0006FDFC File Offset: 0x0006DFFC
		internal static bool CanGetSemanticInfo(CSharpSyntaxNode node, bool allowNamedArgumentName = false, bool isSpeculative = false)
		{
			if (!isSpeculative && CSharpSemanticModel.IsInStructuredTriviaOtherThanCrefOrNameAttribute(node))
			{
				return false;
			}
			SyntaxKind syntaxKind = node.Kind();
			if (syntaxKind <= SyntaxKind.OmittedTypeArgument)
			{
				if (syntaxKind != SyntaxKind.IdentifierName)
				{
					if (syntaxKind != SyntaxKind.OmittedTypeArgument)
					{
						goto IL_0088;
					}
				}
				else
				{
					if (!isSpeculative && node.Parent != null && node.Parent.Kind() == SyntaxKind.NameEquals && node.Parent.Parent.Kind() == SyntaxKind.UsingDirective)
					{
						return false;
					}
					goto IL_0088;
				}
			}
			else
			{
				if (syntaxKind - SyntaxKind.ObjectInitializerExpression <= 1)
				{
					return false;
				}
				if (syntaxKind == SyntaxKind.ComplexElementInitializerExpression)
				{
					return false;
				}
				if (syntaxKind - SyntaxKind.RefExpression > 1)
				{
					goto IL_0088;
				}
			}
			return false;
			IL_0088:
			return !node.IsMissing && ((node is ExpressionSyntax && (isSpeculative || allowNamedArgumentName || !SyntaxFacts.IsNamedArgumentName(node))) || node is ConstructorInitializerSyntax || node is PrimaryConstructorBaseTypeSyntax || node is AttributeSyntax || node is CrefSyntax);
		}

		// Token: 0x0600149C RID: 5276
		internal abstract SymbolInfo GetSymbolInfoWorker(CSharpSyntaxNode node, CSharpSemanticModel.SymbolInfoOptions options, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x0600149D RID: 5277
		internal abstract SymbolInfo GetCollectionInitializerSymbolInfoWorker(InitializerExpressionSyntax collectionInitializer, ExpressionSyntax node, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x0600149E RID: 5278
		internal abstract CSharpTypeInfo GetTypeInfoWorker(CSharpSyntaxNode node, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x0600149F RID: 5279
		internal abstract BoundExpression GetSpeculativelyBoundExpression(int position, ExpressionSyntax expression, SpeculativeBindingOption bindingOption, out Binder binder, out ImmutableArray<Symbol> crefSymbols);

		// Token: 0x060014A0 RID: 5280
		internal abstract ImmutableArray<Symbol> GetMemberGroupWorker(CSharpSyntaxNode node, CSharpSemanticModel.SymbolInfoOptions options, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x060014A1 RID: 5281
		internal abstract ImmutableArray<IPropertySymbol> GetIndexerGroupWorker(CSharpSyntaxNode node, CSharpSemanticModel.SymbolInfoOptions options, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x060014A2 RID: 5282
		internal abstract Optional<object> GetConstantValueWorker(CSharpSyntaxNode node, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x060014A3 RID: 5283 RVA: 0x0006FED4 File Offset: 0x0006E0D4
		internal Binder GetSpeculativeBinder(int position, ExpressionSyntax expression, SpeculativeBindingOption bindingOption)
		{
			position = this.CheckAndAdjustPosition(position);
			if (bindingOption == SpeculativeBindingOption.BindAsTypeOrNamespace && !(expression is TypeSyntax))
			{
				return null;
			}
			Binder binder = this.GetEnclosingBinder(position);
			if (binder == null)
			{
				return null;
			}
			if (bindingOption == SpeculativeBindingOption.BindAsTypeOrNamespace && this.IsInTypeofExpression(position))
			{
				binder = new TypeofBinder(expression, binder);
			}
			binder = new WithNullableContextBinder(this.SyntaxTree, position, binder);
			return new ExecutableCodeBinder(expression, binder.ContainingMemberOrLambda, binder, null).GetBinder(expression);
		}

		// Token: 0x060014A4 RID: 5284 RVA: 0x0006FF3C File Offset: 0x0006E13C
		private Binder GetSpeculativeBinderForAttribute(int position, AttributeSyntax attribute)
		{
			position = this.CheckAndAdjustPositionForSpeculativeAttribute(position);
			Binder enclosingBinder = this.GetEnclosingBinder(position);
			if (enclosingBinder == null)
			{
				return null;
			}
			return new ExecutableCodeBinder(attribute, enclosingBinder.ContainingMemberOrLambda, enclosingBinder, null).GetBinder(attribute);
		}

		// Token: 0x060014A5 RID: 5285 RVA: 0x0006FF74 File Offset: 0x0006E174
		private static BoundExpression GetSpeculativelyBoundExpressionHelper(Binder binder, ExpressionSyntax expression, SpeculativeBindingOption bindingOption)
		{
			BoundExpression boundExpression;
			if (bindingOption == SpeculativeBindingOption.BindAsTypeOrNamespace || binder.Flags.Includes(BinderFlags.CrefParameterOrReturnType))
			{
				boundExpression = binder.BindNamespaceOrType(expression, BindingDiagnosticBag.Discarded);
			}
			else
			{
				boundExpression = binder.BindExpression(expression, BindingDiagnosticBag.Discarded);
			}
			return boundExpression;
		}

		// Token: 0x060014A6 RID: 5286 RVA: 0x0006FFB4 File Offset: 0x0006E1B4
		protected BoundExpression GetSpeculativelyBoundExpressionWithoutNullability(int position, ExpressionSyntax expression, SpeculativeBindingOption bindingOption, out Binder binder, out ImmutableArray<Symbol> crefSymbols)
		{
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			crefSymbols = default(ImmutableArray<Symbol>);
			expression = SyntaxFactory.GetStandaloneExpression(expression);
			binder = this.GetSpeculativeBinder(position, expression, bindingOption);
			if (binder == null)
			{
				return null;
			}
			if (binder.Flags.Includes(BinderFlags.CrefParameterOrReturnType))
			{
				crefSymbols = ImmutableArray.Create<Symbol>(binder.BindType(expression, BindingDiagnosticBag.Discarded, null, false).Type);
				return null;
			}
			if (binder.InCref)
			{
				if (expression.IsKind(SyntaxKind.QualifiedName))
				{
					QualifiedNameSyntax qualifiedNameSyntax = (QualifiedNameSyntax)expression;
					QualifiedCrefSyntax qualifiedCrefSyntax = SyntaxFactory.QualifiedCref(qualifiedNameSyntax.Left, SyntaxFactory.NameMemberCref(qualifiedNameSyntax.Right));
					crefSymbols = CSharpSemanticModel.BindCref(qualifiedCrefSyntax, binder);
				}
				else
				{
					TypeSyntax typeSyntax = expression as TypeSyntax;
					if (typeSyntax != null)
					{
						CrefSyntax crefSyntax = ((typeSyntax is PredefinedTypeSyntax) ? SyntaxFactory.TypeCref(typeSyntax) : SyntaxFactory.NameMemberCref(typeSyntax));
						crefSymbols = CSharpSemanticModel.BindCref(crefSyntax, binder);
					}
				}
				return null;
			}
			return CSharpSemanticModel.GetSpeculativelyBoundExpressionHelper(binder, expression, bindingOption);
		}

		// Token: 0x060014A7 RID: 5287 RVA: 0x000700B4 File Offset: 0x0006E2B4
		internal static ImmutableArray<Symbol> BindCref(CrefSyntax crefSyntax, Binder binder)
		{
			Symbol symbol;
			return binder.BindCref(crefSyntax, out symbol, BindingDiagnosticBag.Discarded);
		}

		// Token: 0x060014A8 RID: 5288 RVA: 0x000700D0 File Offset: 0x0006E2D0
		internal SymbolInfo GetCrefSymbolInfo(int position, CrefSyntax crefSyntax, CSharpSemanticModel.SymbolInfoOptions options, bool hasParameterList)
		{
			Binder enclosingBinder = this.GetEnclosingBinder(position);
			if (enclosingBinder != null && enclosingBinder.InCref)
			{
				return CSharpSemanticModel.GetCrefSymbolInfo(CSharpSemanticModel.BindCref(crefSyntax, enclosingBinder), options, hasParameterList);
			}
			return SymbolInfo.None;
		}

		// Token: 0x060014A9 RID: 5289 RVA: 0x00070108 File Offset: 0x0006E308
		internal static bool HasParameterList(CrefSyntax crefSyntax)
		{
			while (crefSyntax.Kind() == SyntaxKind.QualifiedCref)
			{
				crefSyntax = ((QualifiedCrefSyntax)crefSyntax).Member;
			}
			switch (crefSyntax.Kind())
			{
			case SyntaxKind.NameMemberCref:
				return ((NameMemberCrefSyntax)crefSyntax).Parameters != null;
			case SyntaxKind.IndexerMemberCref:
				return ((IndexerMemberCrefSyntax)crefSyntax).Parameters != null;
			case SyntaxKind.OperatorMemberCref:
				return ((OperatorMemberCrefSyntax)crefSyntax).Parameters != null;
			case SyntaxKind.ConversionOperatorMemberCref:
				return ((ConversionOperatorMemberCrefSyntax)crefSyntax).Parameters != null;
			default:
				return false;
			}
		}

		// Token: 0x060014AA RID: 5290 RVA: 0x00070194 File Offset: 0x0006E394
		private static SymbolInfo GetCrefSymbolInfo(ImmutableArray<Symbol> symbols, CSharpSemanticModel.SymbolInfoOptions options, bool hasParameterList)
		{
			int length = symbols.Length;
			if (length == 0)
			{
				return SymbolInfo.None;
			}
			if (length != 1)
			{
				if ((options & CSharpSemanticModel.SymbolInfoOptions.ResolveAliases) == CSharpSemanticModel.SymbolInfoOptions.ResolveAliases)
				{
					symbols = CSharpSemanticModel.UnwrapAliases(symbols);
				}
				LookupResultKind lookupResultKind = LookupResultKind.Ambiguous;
				SymbolKind firstCandidateKind = symbols[0].Kind;
				if (hasParameterList && symbols.All<Symbol>((Symbol s) => s.Kind == firstCandidateKind))
				{
					lookupResultKind = LookupResultKind.OverloadResolutionFailure;
				}
				return SymbolInfoFactory.Create(symbols, lookupResultKind, false);
			}
			return CSharpSemanticModel.GetSymbolInfoForSymbol(symbols[0], options);
		}

		// Token: 0x060014AB RID: 5291 RVA: 0x00070214 File Offset: 0x0006E414
		private BoundAttribute GetSpeculativelyBoundAttribute(int position, AttributeSyntax attribute, out Binder binder)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException("attribute");
			}
			binder = this.GetSpeculativeBinderForAttribute(position, attribute);
			if (binder == null)
			{
				return null;
			}
			Microsoft.CodeAnalysis.CSharp.Symbols.AliasSymbol aliasSymbol;
			Microsoft.CodeAnalysis.CSharp.Symbols.NamedTypeSymbol namedTypeSymbol = (Microsoft.CodeAnalysis.CSharp.Symbols.NamedTypeSymbol)binder.BindType(attribute.Name, BindingDiagnosticBag.Discarded, out aliasSymbol, null).Type;
			return new ExecutableCodeBinder(attribute, binder.ContainingMemberOrLambda, binder, null).BindAttribute(attribute, namedTypeSymbol, BindingDiagnosticBag.Discarded);
		}

		// Token: 0x060014AC RID: 5292 RVA: 0x00070280 File Offset: 0x0006E480
		private int CheckAndAdjustPositionForSpeculativeAttribute(int position)
		{
			position = this.CheckAndAdjustPosition(position);
			SyntaxToken syntaxToken = this.Root.FindToken(position, false);
			if (position == 0 && position != syntaxToken.SpanStart)
			{
				return position;
			}
			CSharpSyntaxNode csharpSyntaxNode = (CSharpSyntaxNode)syntaxToken.Parent;
			if (position == csharpSyntaxNode.SpanStart)
			{
				BaseTypeDeclarationSyntax baseTypeDeclarationSyntax = csharpSyntaxNode as BaseTypeDeclarationSyntax;
				if (baseTypeDeclarationSyntax != null)
				{
					position = baseTypeDeclarationSyntax.OpenBraceToken.SpanStart;
				}
				MethodDeclarationSyntax methodDeclarationSyntax = csharpSyntaxNode.FirstAncestorOrSelf<MethodDeclarationSyntax>(null, true);
				if (methodDeclarationSyntax != null && methodDeclarationSyntax.SpanStart == position)
				{
					position = methodDeclarationSyntax.Identifier.SpanStart;
				}
			}
			return position;
		}

		// Token: 0x060014AD RID: 5293 RVA: 0x0007030C File Offset: 0x0006E50C
		protected override IOperation GetOperationCore(SyntaxNode node, CancellationToken cancellationToken)
		{
			CSharpSyntaxNode csharpSyntaxNode = (CSharpSyntaxNode)node;
			this.CheckSyntaxNode(csharpSyntaxNode);
			return this.GetOperationWorker(csharpSyntaxNode, cancellationToken);
		}

		// Token: 0x060014AE RID: 5294 RVA: 0x0007032F File Offset: 0x0006E52F
		internal virtual IOperation GetOperationWorker(CSharpSyntaxNode node, CancellationToken cancellationToken)
		{
			return null;
		}

		// Token: 0x060014AF RID: 5295
		public abstract SymbolInfo GetSymbolInfo(OrderingSyntax node, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x060014B0 RID: 5296
		public abstract SymbolInfo GetSymbolInfo(SelectOrGroupClauseSyntax node, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x060014B1 RID: 5297 RVA: 0x00070332 File Offset: 0x0006E532
		public SymbolInfo GetSymbolInfo(PositionalPatternClauseSyntax node, CancellationToken cancellationToken = default(CancellationToken))
		{
			this.CheckSyntaxNode(node);
			return this.GetSymbolInfoWorker(node, CSharpSemanticModel.SymbolInfoOptions.DefaultOptions, cancellationToken);
		}

		// Token: 0x060014B2 RID: 5298 RVA: 0x00070344 File Offset: 0x0006E544
		public SymbolInfo GetSymbolInfo(ExpressionSyntax expression, CancellationToken cancellationToken = default(CancellationToken))
		{
			this.CheckSyntaxNode(expression);
			if (!CSharpSemanticModel.CanGetSemanticInfo(expression, true, false))
			{
				return SymbolInfo.None;
			}
			if (SyntaxFacts.IsNamedArgumentName(expression))
			{
				return this.GetNamedArgumentSymbolInfo((IdentifierNameSyntax)expression, cancellationToken);
			}
			DeclarationExpressionSyntax declarationExpressionSyntax;
			if (SyntaxFacts.IsDeclarationExpressionType(expression, out declarationExpressionSyntax))
			{
				SyntaxKind syntaxKind = declarationExpressionSyntax.Designation.Kind();
				if (syntaxKind == SyntaxKind.SingleVariableDesignation)
				{
					return CSharpSemanticModel.GetSymbolInfoFromSymbolOrNone(this.TypeFromVariable((SingleVariableDesignationSyntax)declarationExpressionSyntax.Designation, cancellationToken).Item1);
				}
				if (syntaxKind != SyntaxKind.ParenthesizedVariableDesignation)
				{
					if (syntaxKind == SyntaxKind.DiscardDesignation)
					{
						return CSharpSemanticModel.GetSymbolInfoFromSymbolOrNone(this.GetTypeInfoWorker(declarationExpressionSyntax, cancellationToken).Type.GetPublicSymbol());
					}
				}
				else if (((TypeSyntax)expression).IsVar)
				{
					return SymbolInfo.None;
				}
			}
			else
			{
				DeclarationExpressionSyntax declarationExpressionSyntax2 = expression as DeclarationExpressionSyntax;
				if (declarationExpressionSyntax2 != null)
				{
					if (declarationExpressionSyntax2.Designation.Kind() != SyntaxKind.SingleVariableDesignation)
					{
						return SymbolInfo.None;
					}
					ISymbol declaredSymbol = this.GetDeclaredSymbol((SingleVariableDesignationSyntax)declarationExpressionSyntax2.Designation, cancellationToken);
					if (declaredSymbol == null)
					{
						return SymbolInfo.None;
					}
					return new SymbolInfo(declaredSymbol);
				}
			}
			return this.GetSymbolInfoWorker(expression, CSharpSemanticModel.SymbolInfoOptions.DefaultOptions, cancellationToken);
		}

		// Token: 0x060014B3 RID: 5299 RVA: 0x00070447 File Offset: 0x0006E647
		private static SymbolInfo GetSymbolInfoFromSymbolOrNone(ITypeSymbol type)
		{
			if (type == null || type.Kind != SymbolKind.ErrorType)
			{
				return new SymbolInfo(type);
			}
			return SymbolInfo.None;
		}

		// Token: 0x060014B4 RID: 5300 RVA: 0x0007046C File Offset: 0x0006E66C
		[return: TupleElementNames(new string[] { "Type", "Annotation" })]
		private ValueTuple<ITypeSymbol, NullableAnnotation> TypeFromVariable(SingleVariableDesignationSyntax variableDesignation, CancellationToken cancellationToken)
		{
			ISymbol declaredSymbol = this.GetDeclaredSymbol(variableDesignation, cancellationToken);
			ILocalSymbol localSymbol = declaredSymbol as ILocalSymbol;
			if (localSymbol != null)
			{
				return new ValueTuple<ITypeSymbol, NullableAnnotation>(localSymbol.Type, localSymbol.NullableAnnotation);
			}
			IFieldSymbol fieldSymbol = declaredSymbol as IFieldSymbol;
			if (fieldSymbol == null)
			{
				return default(ValueTuple<ITypeSymbol, NullableAnnotation>);
			}
			return new ValueTuple<ITypeSymbol, NullableAnnotation>(fieldSymbol.Type, fieldSymbol.NullableAnnotation);
		}

		// Token: 0x060014B5 RID: 5301 RVA: 0x000704C8 File Offset: 0x0006E6C8
		public SymbolInfo GetCollectionInitializerSymbolInfo(ExpressionSyntax expression, CancellationToken cancellationToken = default(CancellationToken))
		{
			this.CheckSyntaxNode(expression);
			if (expression.Parent != null && expression.Parent.Kind() == SyntaxKind.CollectionInitializerExpression)
			{
				InitializerExpressionSyntax initializerExpressionSyntax = (InitializerExpressionSyntax)expression.Parent;
				while (initializerExpressionSyntax.Parent != null && initializerExpressionSyntax.Parent.Kind() == SyntaxKind.SimpleAssignmentExpression && ((AssignmentExpressionSyntax)initializerExpressionSyntax.Parent).Right == initializerExpressionSyntax && initializerExpressionSyntax.Parent.Parent != null && initializerExpressionSyntax.Parent.Parent.Kind() == SyntaxKind.ObjectInitializerExpression)
				{
					initializerExpressionSyntax = (InitializerExpressionSyntax)initializerExpressionSyntax.Parent.Parent;
				}
				BaseObjectCreationExpressionSyntax baseObjectCreationExpressionSyntax = initializerExpressionSyntax.Parent as BaseObjectCreationExpressionSyntax;
				if (baseObjectCreationExpressionSyntax != null && baseObjectCreationExpressionSyntax.Initializer == initializerExpressionSyntax && CSharpSemanticModel.CanGetSemanticInfo(baseObjectCreationExpressionSyntax, false, false))
				{
					return this.GetCollectionInitializerSymbolInfoWorker((InitializerExpressionSyntax)expression.Parent, expression, cancellationToken);
				}
			}
			return SymbolInfo.None;
		}

		// Token: 0x060014B6 RID: 5302 RVA: 0x000705A7 File Offset: 0x0006E7A7
		public SymbolInfo GetSymbolInfo(ConstructorInitializerSyntax constructorInitializer, CancellationToken cancellationToken = default(CancellationToken))
		{
			this.CheckSyntaxNode(constructorInitializer);
			if (!CSharpSemanticModel.CanGetSemanticInfo(constructorInitializer, false, false))
			{
				return SymbolInfo.None;
			}
			return this.GetSymbolInfoWorker(constructorInitializer, CSharpSemanticModel.SymbolInfoOptions.DefaultOptions, cancellationToken);
		}

		// Token: 0x060014B7 RID: 5303 RVA: 0x000705C9 File Offset: 0x0006E7C9
		public SymbolInfo GetSymbolInfo(PrimaryConstructorBaseTypeSyntax constructorInitializer, CancellationToken cancellationToken = default(CancellationToken))
		{
			this.CheckSyntaxNode(constructorInitializer);
			if (!CSharpSemanticModel.CanGetSemanticInfo(constructorInitializer, false, false))
			{
				return SymbolInfo.None;
			}
			return this.GetSymbolInfoWorker(constructorInitializer, CSharpSemanticModel.SymbolInfoOptions.DefaultOptions, cancellationToken);
		}

		// Token: 0x060014B8 RID: 5304 RVA: 0x000705EB File Offset: 0x0006E7EB
		public SymbolInfo GetSymbolInfo(AttributeSyntax attributeSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			this.CheckSyntaxNode(attributeSyntax);
			if (!CSharpSemanticModel.CanGetSemanticInfo(attributeSyntax, false, false))
			{
				return SymbolInfo.None;
			}
			return this.GetSymbolInfoWorker(attributeSyntax, CSharpSemanticModel.SymbolInfoOptions.DefaultOptions, cancellationToken);
		}

		// Token: 0x060014B9 RID: 5305 RVA: 0x0007060D File Offset: 0x0006E80D
		public SymbolInfo GetSymbolInfo(CrefSyntax crefSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			this.CheckSyntaxNode(crefSyntax);
			if (!CSharpSemanticModel.CanGetSemanticInfo(crefSyntax, false, false))
			{
				return SymbolInfo.None;
			}
			return this.GetSymbolInfoWorker(crefSyntax, CSharpSemanticModel.SymbolInfoOptions.DefaultOptions, cancellationToken);
		}

		// Token: 0x060014BA RID: 5306 RVA: 0x00070630 File Offset: 0x0006E830
		public SymbolInfo GetSpeculativeSymbolInfo(int position, ExpressionSyntax expression, SpeculativeBindingOption bindingOption)
		{
			if (!CSharpSemanticModel.CanGetSemanticInfo(expression, false, true))
			{
				return SymbolInfo.None;
			}
			Binder binder;
			ImmutableArray<Symbol> immutableArray;
			BoundNode speculativelyBoundExpression = this.GetSpeculativelyBoundExpression(position, expression, bindingOption, out binder, out immutableArray);
			if (speculativelyBoundExpression != null)
			{
				return this.GetSymbolInfoForNode(CSharpSemanticModel.SymbolInfoOptions.DefaultOptions, speculativelyBoundExpression, speculativelyBoundExpression, null, binder);
			}
			if (!immutableArray.IsDefault)
			{
				return CSharpSemanticModel.GetCrefSymbolInfo(immutableArray, CSharpSemanticModel.SymbolInfoOptions.DefaultOptions, false);
			}
			return SymbolInfo.None;
		}

		// Token: 0x060014BB RID: 5307 RVA: 0x00070684 File Offset: 0x0006E884
		public SymbolInfo GetSpeculativeSymbolInfo(int position, AttributeSyntax attribute)
		{
			Binder binder;
			BoundNode speculativelyBoundAttribute = this.GetSpeculativelyBoundAttribute(position, attribute, out binder);
			if (speculativelyBoundAttribute == null)
			{
				return SymbolInfo.None;
			}
			return this.GetSymbolInfoForNode(CSharpSemanticModel.SymbolInfoOptions.DefaultOptions, speculativelyBoundAttribute, speculativelyBoundAttribute, null, binder);
		}

		// Token: 0x060014BC RID: 5308 RVA: 0x000706B0 File Offset: 0x0006E8B0
		public SymbolInfo GetSpeculativeSymbolInfo(int position, ConstructorInitializerSyntax constructorInitializer)
		{
			position = this.CheckAndAdjustPosition(position);
			if (constructorInitializer == null)
			{
				throw new ArgumentNullException("constructorInitializer");
			}
			ConstructorInitializerSyntax constructorInitializerSyntax = Enumerable.FirstOrDefault<ConstructorInitializerSyntax>(Enumerable.OfType<ConstructorInitializerSyntax>(this.Root.FindToken(position, false).Parent.AncestorsAndSelf(true)));
			if (constructorInitializerSyntax == null)
			{
				return SymbolInfo.None;
			}
			MemberSemanticModel memberModel = this.GetMemberModel(constructorInitializerSyntax);
			if (memberModel == null)
			{
				return SymbolInfo.None;
			}
			Binder binder = memberModel.GetEnclosingBinder(position);
			if (binder != null)
			{
				binder = new ExecutableCodeBinder(constructorInitializer, binder.ContainingMemberOrLambda, binder, null);
				BoundExpressionStatement boundExpressionStatement = binder.BindConstructorInitializer(constructorInitializer, BindingDiagnosticBag.Discarded);
				return CSharpSemanticModel.GetSymbolInfoFromBoundConstructorInitializer(memberModel, binder, boundExpressionStatement);
			}
			return SymbolInfo.None;
		}

		// Token: 0x060014BD RID: 5309 RVA: 0x0007074C File Offset: 0x0006E94C
		private static SymbolInfo GetSymbolInfoFromBoundConstructorInitializer(MemberSemanticModel memberModel, Binder binder, BoundExpressionStatement bnode)
		{
			BoundExpression boundExpression = bnode.Expression;
			for (;;)
			{
				BoundSequence boundSequence = boundExpression as BoundSequence;
				if (boundSequence == null)
				{
					break;
				}
				boundExpression = boundSequence.Value;
			}
			return memberModel.GetSymbolInfoForNode(CSharpSemanticModel.SymbolInfoOptions.DefaultOptions, boundExpression, boundExpression, null, binder);
		}

		// Token: 0x060014BE RID: 5310 RVA: 0x00070780 File Offset: 0x0006E980
		public SymbolInfo GetSpeculativeSymbolInfo(int position, PrimaryConstructorBaseTypeSyntax constructorInitializer)
		{
			position = this.CheckAndAdjustPosition(position);
			if (constructorInitializer == null)
			{
				throw new ArgumentNullException("constructorInitializer");
			}
			PrimaryConstructorBaseTypeSyntax primaryConstructorBaseTypeSyntax = Enumerable.FirstOrDefault<PrimaryConstructorBaseTypeSyntax>(Enumerable.OfType<PrimaryConstructorBaseTypeSyntax>(this.Root.FindToken(position, false).Parent.AncestorsAndSelf(true)));
			if (primaryConstructorBaseTypeSyntax == null)
			{
				return SymbolInfo.None;
			}
			MemberSemanticModel memberModel = this.GetMemberModel(primaryConstructorBaseTypeSyntax);
			if (memberModel == null)
			{
				return SymbolInfo.None;
			}
			ArgumentListSyntax argumentList = primaryConstructorBaseTypeSyntax.ArgumentList;
			Binder binder = memberModel.GetEnclosingBinder(LookupPosition.IsBetweenTokens(position, argumentList.OpenParenToken, argumentList.CloseParenToken) ? position : argumentList.OpenParenToken.SpanStart);
			if (binder != null)
			{
				binder = new ExecutableCodeBinder(constructorInitializer, binder.ContainingMemberOrLambda, binder, null);
				BoundExpressionStatement boundExpressionStatement = binder.BindConstructorInitializer(constructorInitializer, BindingDiagnosticBag.Discarded);
				return CSharpSemanticModel.GetSymbolInfoFromBoundConstructorInitializer(memberModel, binder, boundExpressionStatement);
			}
			return SymbolInfo.None;
		}

		// Token: 0x060014BF RID: 5311 RVA: 0x00070848 File Offset: 0x0006EA48
		public SymbolInfo GetSpeculativeSymbolInfo(int position, CrefSyntax cref, CSharpSemanticModel.SymbolInfoOptions options = CSharpSemanticModel.SymbolInfoOptions.DefaultOptions)
		{
			position = this.CheckAndAdjustPosition(position);
			return this.GetCrefSymbolInfo(position, cref, options, CSharpSemanticModel.HasParameterList(cref));
		}

		// Token: 0x060014C0 RID: 5312 RVA: 0x00070862 File Offset: 0x0006EA62
		public TypeInfo GetTypeInfo(ConstructorInitializerSyntax constructorInitializer, CancellationToken cancellationToken = default(CancellationToken))
		{
			this.CheckSyntaxNode(constructorInitializer);
			return CSharpSemanticModel.CanGetSemanticInfo(constructorInitializer, false, false) ? this.GetTypeInfoWorker(constructorInitializer, cancellationToken) : CSharpTypeInfo.None;
		}

		// Token: 0x060014C1 RID: 5313
		public abstract TypeInfo GetTypeInfo(SelectOrGroupClauseSyntax node, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x060014C2 RID: 5314 RVA: 0x0007088C File Offset: 0x0006EA8C
		public TypeInfo GetTypeInfo(PatternSyntax pattern, CancellationToken cancellationToken = default(CancellationToken))
		{
			for (;;)
			{
				ParenthesizedPatternSyntax parenthesizedPatternSyntax = pattern as ParenthesizedPatternSyntax;
				if (parenthesizedPatternSyntax == null)
				{
					break;
				}
				pattern = parenthesizedPatternSyntax.Pattern;
			}
			this.CheckSyntaxNode(pattern);
			return this.GetTypeInfoWorker(pattern, cancellationToken);
		}

		// Token: 0x060014C3 RID: 5315 RVA: 0x000708C4 File Offset: 0x0006EAC4
		public TypeInfo GetTypeInfo(ExpressionSyntax expression, CancellationToken cancellationToken = default(CancellationToken))
		{
			this.CheckSyntaxNode(expression);
			if (!CSharpSemanticModel.CanGetSemanticInfo(expression, false, false))
			{
				return CSharpTypeInfo.None;
			}
			DeclarationExpressionSyntax declarationExpressionSyntax;
			if (SyntaxFacts.IsDeclarationExpressionType(expression, out declarationExpressionSyntax))
			{
				SyntaxKind syntaxKind = declarationExpressionSyntax.Designation.Kind();
				if (syntaxKind == SyntaxKind.SingleVariableDesignation)
				{
					ValueTuple<ITypeSymbol, NullableAnnotation> valueTuple = this.TypeFromVariable((SingleVariableDesignationSyntax)declarationExpressionSyntax.Designation, cancellationToken);
					ITypeSymbol item = valueTuple.Item1;
					NullableAnnotation item2 = valueTuple.Item2;
					Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol symbol = item.GetSymbol();
					NullabilityInfo nullabilityInfo = item2.ToNullabilityInfo(symbol);
					return new CSharpTypeInfo(symbol, symbol, nullabilityInfo, nullabilityInfo, Conversion.Identity);
				}
				if (syntaxKind != SyntaxKind.ParenthesizedVariableDesignation)
				{
					if (syntaxKind == SyntaxKind.DiscardDesignation)
					{
						CSharpTypeInfo typeInfoWorker = this.GetTypeInfoWorker(declarationExpressionSyntax, cancellationToken);
						return new CSharpTypeInfo(typeInfoWorker.Type, typeInfoWorker.Type, typeInfoWorker.Nullability, typeInfoWorker.Nullability, Conversion.Identity);
					}
				}
				else if (((TypeSyntax)expression).IsVar)
				{
					return CSharpTypeInfo.None;
				}
			}
			return this.GetTypeInfoWorker(expression, cancellationToken);
		}

		// Token: 0x060014C4 RID: 5316 RVA: 0x000709C5 File Offset: 0x0006EBC5
		public TypeInfo GetTypeInfo(AttributeSyntax attributeSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			this.CheckSyntaxNode(attributeSyntax);
			return CSharpSemanticModel.CanGetSemanticInfo(attributeSyntax, false, false) ? this.GetTypeInfoWorker(attributeSyntax, cancellationToken) : CSharpTypeInfo.None;
		}

		// Token: 0x060014C5 RID: 5317 RVA: 0x000709EC File Offset: 0x0006EBEC
		public Conversion GetConversion(SyntaxNode expression, CancellationToken cancellationToken = default(CancellationToken))
		{
			CSharpSyntaxNode csharpSyntaxNode = (CSharpSyntaxNode)expression;
			this.CheckSyntaxNode(csharpSyntaxNode);
			return (CSharpSemanticModel.CanGetSemanticInfo(csharpSyntaxNode, false, false) ? this.GetTypeInfoWorker(csharpSyntaxNode, cancellationToken) : CSharpTypeInfo.None).ImplicitConversion;
		}

		// Token: 0x060014C6 RID: 5318 RVA: 0x00070A25 File Offset: 0x0006EC25
		public TypeInfo GetSpeculativeTypeInfo(int position, ExpressionSyntax expression, SpeculativeBindingOption bindingOption)
		{
			return this.GetSpeculativeTypeInfoWorker(position, expression, bindingOption);
		}

		// Token: 0x060014C7 RID: 5319 RVA: 0x00070A38 File Offset: 0x0006EC38
		internal CSharpTypeInfo GetSpeculativeTypeInfoWorker(int position, ExpressionSyntax expression, SpeculativeBindingOption bindingOption)
		{
			if (!CSharpSemanticModel.CanGetSemanticInfo(expression, false, true))
			{
				return CSharpTypeInfo.None;
			}
			Binder binder;
			ImmutableArray<Symbol> immutableArray;
			BoundNode speculativelyBoundExpression = this.GetSpeculativelyBoundExpression(position, expression, bindingOption, out binder, out immutableArray);
			if (speculativelyBoundExpression != null)
			{
				return this.GetTypeInfoForNode(speculativelyBoundExpression, speculativelyBoundExpression, null);
			}
			if (immutableArray.IsDefault || immutableArray.Length != 1)
			{
				return CSharpTypeInfo.None;
			}
			return CSharpSemanticModel.GetTypeInfoForSymbol(immutableArray[0]);
		}

		// Token: 0x060014C8 RID: 5320 RVA: 0x00070A96 File Offset: 0x0006EC96
		public Conversion GetSpeculativeConversion(int position, ExpressionSyntax expression, SpeculativeBindingOption bindingOption)
		{
			return this.GetSpeculativeTypeInfoWorker(position, expression, bindingOption).ImplicitConversion;
		}

		// Token: 0x060014C9 RID: 5321 RVA: 0x00070AA6 File Offset: 0x0006ECA6
		public ImmutableArray<ISymbol> GetMemberGroup(ExpressionSyntax expression, CancellationToken cancellationToken = default(CancellationToken))
		{
			this.CheckSyntaxNode(expression);
			if (!CSharpSemanticModel.CanGetSemanticInfo(expression, false, false))
			{
				return ImmutableArray<ISymbol>.Empty;
			}
			return this.GetMemberGroupWorker(expression, CSharpSemanticModel.SymbolInfoOptions.DefaultOptions, cancellationToken).GetPublicSymbols();
		}

		// Token: 0x060014CA RID: 5322 RVA: 0x00070ACD File Offset: 0x0006ECCD
		public ImmutableArray<ISymbol> GetMemberGroup(AttributeSyntax attribute, CancellationToken cancellationToken = default(CancellationToken))
		{
			this.CheckSyntaxNode(attribute);
			if (!CSharpSemanticModel.CanGetSemanticInfo(attribute, false, false))
			{
				return ImmutableArray<ISymbol>.Empty;
			}
			return this.GetMemberGroupWorker(attribute, CSharpSemanticModel.SymbolInfoOptions.DefaultOptions, cancellationToken).GetPublicSymbols();
		}

		// Token: 0x060014CB RID: 5323 RVA: 0x00070AF4 File Offset: 0x0006ECF4
		public ImmutableArray<ISymbol> GetMemberGroup(ConstructorInitializerSyntax initializer, CancellationToken cancellationToken = default(CancellationToken))
		{
			this.CheckSyntaxNode(initializer);
			if (!CSharpSemanticModel.CanGetSemanticInfo(initializer, false, false))
			{
				return ImmutableArray<ISymbol>.Empty;
			}
			return this.GetMemberGroupWorker(initializer, CSharpSemanticModel.SymbolInfoOptions.DefaultOptions, cancellationToken).GetPublicSymbols();
		}

		// Token: 0x060014CC RID: 5324 RVA: 0x00070B1B File Offset: 0x0006ED1B
		public ImmutableArray<IPropertySymbol> GetIndexerGroup(ExpressionSyntax expression, CancellationToken cancellationToken = default(CancellationToken))
		{
			this.CheckSyntaxNode(expression);
			if (!CSharpSemanticModel.CanGetSemanticInfo(expression, false, false))
			{
				return ImmutableArray<IPropertySymbol>.Empty;
			}
			return this.GetIndexerGroupWorker(expression, CSharpSemanticModel.SymbolInfoOptions.DefaultOptions, cancellationToken);
		}

		// Token: 0x060014CD RID: 5325 RVA: 0x00070B40 File Offset: 0x0006ED40
		public Optional<object> GetConstantValue(ExpressionSyntax expression, CancellationToken cancellationToken = default(CancellationToken))
		{
			this.CheckSyntaxNode(expression);
			if (!CSharpSemanticModel.CanGetSemanticInfo(expression, false, false))
			{
				return default(Optional<object>);
			}
			return this.GetConstantValueWorker(expression, cancellationToken);
		}

		// Token: 0x060014CE RID: 5326
		public abstract QueryClauseInfo GetQueryClauseInfo(QueryClauseSyntax node, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x060014CF RID: 5327 RVA: 0x00070B70 File Offset: 0x0006ED70
		public IAliasSymbol GetAliasInfo(IdentifierNameSyntax nameSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			this.CheckSyntaxNode(nameSyntax);
			if (!CSharpSemanticModel.CanGetSemanticInfo(nameSyntax, false, false))
			{
				return null;
			}
			return this.GetSymbolInfoWorker(nameSyntax, CSharpSemanticModel.SymbolInfoOptions.PreferTypeToConstructors | CSharpSemanticModel.SymbolInfoOptions.PreserveAliases, cancellationToken).Symbol as IAliasSymbol;
		}

		// Token: 0x060014D0 RID: 5328 RVA: 0x00070BA8 File Offset: 0x0006EDA8
		public IAliasSymbol GetSpeculativeAliasInfo(int position, IdentifierNameSyntax nameSyntax, SpeculativeBindingOption bindingOption)
		{
			Binder binder;
			ImmutableArray<Symbol> immutableArray;
			BoundNode speculativelyBoundExpression = this.GetSpeculativelyBoundExpression(position, nameSyntax, bindingOption, out binder, out immutableArray);
			if (speculativelyBoundExpression != null)
			{
				return this.GetSymbolInfoForNode(CSharpSemanticModel.SymbolInfoOptions.PreferTypeToConstructors | CSharpSemanticModel.SymbolInfoOptions.PreserveAliases, speculativelyBoundExpression, speculativelyBoundExpression, null, binder).Symbol as IAliasSymbol;
			}
			if (immutableArray.IsDefault || immutableArray.Length != 1)
			{
				return null;
			}
			return (immutableArray[0] as Microsoft.CodeAnalysis.CSharp.Symbols.AliasSymbol).GetPublicSymbol();
		}

		// Token: 0x060014D1 RID: 5329 RVA: 0x00070C07 File Offset: 0x0006EE07
		internal Binder GetEnclosingBinder(int position)
		{
			return this.GetEnclosingBinderInternal(position);
		}

		// Token: 0x060014D2 RID: 5330
		internal abstract Binder GetEnclosingBinderInternal(int position);

		// Token: 0x060014D3 RID: 5331
		internal abstract MemberSemanticModel GetMemberModel(SyntaxNode node);

		// Token: 0x060014D4 RID: 5332 RVA: 0x00070C10 File Offset: 0x0006EE10
		internal bool IsInTree(SyntaxNode node)
		{
			return node.SyntaxTree == this.SyntaxTree;
		}

		// Token: 0x060014D5 RID: 5333 RVA: 0x00070C20 File Offset: 0x0006EE20
		private static bool IsInStructuredTriviaOtherThanCrefOrNameAttribute(CSharpSyntaxNode node)
		{
			while (node != null)
			{
				if (node.Kind() == SyntaxKind.XmlCrefAttribute || node.Kind() == SyntaxKind.XmlNameAttribute)
				{
					return false;
				}
				if (node.IsStructuredTrivia)
				{
					return true;
				}
				node = node.ParentOrStructuredTriviaParent;
			}
			return false;
		}

		// Token: 0x060014D6 RID: 5334 RVA: 0x00070C58 File Offset: 0x0006EE58
		protected int CheckAndAdjustPosition(int position)
		{
			SyntaxToken syntaxToken;
			return this.CheckAndAdjustPosition(position, out syntaxToken);
		}

		// Token: 0x060014D7 RID: 5335 RVA: 0x00070C70 File Offset: 0x0006EE70
		protected int CheckAndAdjustPosition(int position, out SyntaxToken token)
		{
			int position2 = this.Root.Position;
			int end = this.Root.FullSpan.End;
			bool flag = position == end && position == this.SyntaxTree.GetRoot(default(CancellationToken)).FullSpan.End;
			if ((position2 <= position && position < end) || flag)
			{
				token = (flag ? ((CSharpSyntaxNode)this.SyntaxTree.GetRoot(default(CancellationToken))) : this.Root).FindTokenIncludingCrefAndNameAttributes(position);
				if (position < token.SpanStart)
				{
					token = token.GetPreviousToken(false, false, false, false);
				}
				return Math.Max(token.SpanStart, position2);
			}
			if (position2 == end && position == end)
			{
				token = default(SyntaxToken);
				return position2;
			}
			throw new ArgumentOutOfRangeException("position", position, string.Format(CSharpResources.PositionIsNotWithinSyntax, this.Root.FullSpan));
		}

		// Token: 0x060014D8 RID: 5336 RVA: 0x00070D6C File Offset: 0x0006EF6C
		protected int GetAdjustedNodePosition(SyntaxNode node)
		{
			TextSpan fullSpan = this.Root.FullSpan;
			int num = node.SpanStart;
			SyntaxToken firstToken = node.GetFirstToken(false, false, false, false);
			if (firstToken.Node != null)
			{
				int spanStart = firstToken.SpanStart;
				if (spanStart < node.Span.End)
				{
					num = spanStart;
				}
			}
			if (fullSpan.IsEmpty)
			{
				return num;
			}
			if (num == fullSpan.End)
			{
				return this.CheckAndAdjustPosition(num - 1);
			}
			if (node.IsMissing || node.HasErrors || node.Width == 0 || node.IsPartOfStructuredTrivia())
			{
				return this.CheckAndAdjustPosition(num);
			}
			return num;
		}

		// Token: 0x060014D9 RID: 5337 RVA: 0x00070E04 File Offset: 0x0006F004
		[Conditional("DEBUG")]
		protected void AssertPositionAdjusted(int position)
		{
		}

		// Token: 0x060014DA RID: 5338 RVA: 0x00070E06 File Offset: 0x0006F006
		protected void CheckSyntaxNode(CSharpSyntaxNode syntax)
		{
			if (syntax == null)
			{
				throw new ArgumentNullException("syntax");
			}
			if (!this.IsInTree(syntax))
			{
				throw new ArgumentException(CSharpResources.SyntaxNodeIsNotWithinSynt);
			}
		}

		// Token: 0x060014DB RID: 5339 RVA: 0x00070E2C File Offset: 0x0006F02C
		private void CheckModelAndSyntaxNodeToSpeculate(CSharpSyntaxNode syntax)
		{
			if (syntax == null)
			{
				throw new ArgumentNullException("syntax");
			}
			if (this.IsSpeculativeSemanticModel)
			{
				throw new InvalidOperationException(CSharpResources.ChainingSpeculativeModelIsNotSupported);
			}
			if (this.Compilation.ContainsSyntaxTree(syntax.SyntaxTree))
			{
				throw new ArgumentException(CSharpResources.SpeculatedSyntaxNodeCannotBelongToCurrentCompilation);
			}
		}

		// Token: 0x060014DC RID: 5340 RVA: 0x00070E78 File Offset: 0x0006F078
		public ImmutableArray<ISymbol> LookupSymbols(int position, Microsoft.CodeAnalysis.CSharp.Symbols.NamespaceOrTypeSymbol container = null, string name = null, bool includeReducedExtensionMethods = false)
		{
			LookupOptions lookupOptions = (includeReducedExtensionMethods ? LookupOptions.IncludeExtensionMethods : LookupOptions.Default);
			return this.LookupSymbolsInternal(position, container, name, lookupOptions, false);
		}

		// Token: 0x060014DD RID: 5341 RVA: 0x00070E9D File Offset: 0x0006F09D
		public new ImmutableArray<ISymbol> LookupBaseMembers(int position, string name = null)
		{
			return this.LookupSymbolsInternal(position, null, name, LookupOptions.Default, true);
		}

		// Token: 0x060014DE RID: 5342 RVA: 0x00070EAA File Offset: 0x0006F0AA
		public ImmutableArray<ISymbol> LookupStaticMembers(int position, Microsoft.CodeAnalysis.CSharp.Symbols.NamespaceOrTypeSymbol container = null, string name = null)
		{
			return this.LookupSymbolsInternal(position, container, name, LookupOptions.MustNotBeInstance, false);
		}

		// Token: 0x060014DF RID: 5343 RVA: 0x00070EB8 File Offset: 0x0006F0B8
		public ImmutableArray<ISymbol> LookupNamespacesAndTypes(int position, Microsoft.CodeAnalysis.CSharp.Symbols.NamespaceOrTypeSymbol container = null, string name = null)
		{
			return this.LookupSymbolsInternal(position, container, name, LookupOptions.NamespacesOrTypesOnly, false);
		}

		// Token: 0x060014E0 RID: 5344 RVA: 0x00070EC5 File Offset: 0x0006F0C5
		public new ImmutableArray<ISymbol> LookupLabels(int position, string name = null)
		{
			return this.LookupSymbolsInternal(position, null, name, LookupOptions.LabelsOnly, false);
		}

		// Token: 0x060014E1 RID: 5345 RVA: 0x00070ED8 File Offset: 0x0006F0D8
		private ImmutableArray<ISymbol> LookupSymbolsInternal(int position, Microsoft.CodeAnalysis.CSharp.Symbols.NamespaceOrTypeSymbol container, string name, LookupOptions options, bool useBaseReferenceAccessibility)
		{
			if (useBaseReferenceAccessibility)
			{
				options |= LookupOptions.UseBaseReferenceAccessibility;
			}
			options.ThrowIfInvalid();
			SyntaxToken syntaxToken;
			position = this.CheckAndAdjustPosition(position, out syntaxToken);
			if (container == null || container.Kind == SymbolKind.Namespace)
			{
				options &= ~LookupOptions.IncludeExtensionMethods;
			}
			Binder enclosingBinder = this.GetEnclosingBinder(position);
			if (enclosingBinder == null)
			{
				return ImmutableArray<ISymbol>.Empty;
			}
			if (useBaseReferenceAccessibility)
			{
				Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol containingType = enclosingBinder.ContainingType;
				if (containingType != null && containingType.Kind == SymbolKind.NamedType && ((Microsoft.CodeAnalysis.CSharp.Symbols.NamedTypeSymbol)containingType).IsScriptClass)
				{
					return ImmutableArray<ISymbol>.Empty;
				}
				Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol baseTypeNoUseSiteDiagnostics;
				if (containingType == null || (baseTypeNoUseSiteDiagnostics = containingType.BaseTypeNoUseSiteDiagnostics) == null)
				{
					throw new ArgumentException("Not a valid position for a call to LookupBaseMembers (must be in a type with a base type)", "position");
				}
				container = baseTypeNoUseSiteDiagnostics;
			}
			if (!enclosingBinder.IsInMethodBody && (options & (LookupOptions.NamespaceAliasesOnly | LookupOptions.NamespacesOrTypesOnly | LookupOptions.LabelsOnly)) == LookupOptions.Default)
			{
				ExpressionSyntax expressionSyntax = syntaxToken.Parent as ExpressionSyntax;
				if (expressionSyntax != null && !(expressionSyntax.Parent is XmlNameAttributeSyntax) && !SyntaxFacts.IsInTypeOnlyContext(expressionSyntax))
				{
					options |= LookupOptions.MustNotBeMethodTypeParameter;
				}
			}
			LookupSymbolsInfo instance = LookupSymbolsInfo.GetInstance();
			instance.FilterName = name;
			if (container == null)
			{
				enclosingBinder.AddLookupSymbolsInfo(instance, options);
			}
			else
			{
				enclosingBinder.AddMemberLookupSymbolsInfo(instance, container, options, enclosingBinder);
			}
			ArrayBuilder<ISymbol> instance2 = ArrayBuilder<ISymbol>.GetInstance(instance.Count);
			if (name == null)
			{
				using (IEnumerator<string> enumerator = instance.Names.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string text = enumerator.Current;
						this.AppendSymbolsWithName(instance2, text, enclosingBinder, container, options, instance);
					}
					goto IL_0162;
				}
			}
			this.AppendSymbolsWithName(instance2, name, enclosingBinder, container, options, instance);
			IL_0162:
			instance.Free();
			if ((options & LookupOptions.IncludeExtensionMethods) != LookupOptions.Default)
			{
				LookupResult instance3 = LookupResult.GetInstance();
				options |= LookupOptions.AllMethodsOnArityZero;
				options &= ~LookupOptions.MustBeInstance;
				CompoundUseSiteInfo<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol> discarded = CompoundUseSiteInfo<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>.Discarded;
				enclosingBinder.LookupExtensionMethods(instance3, name, 0, options, ref discarded);
				if (instance3.IsMultiViable)
				{
					Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol typeSymbol = (Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol)container;
					foreach (Symbol symbol in instance3.Symbols)
					{
						Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol methodSymbol = ((Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol)symbol).ReduceExtensionMethod(typeSymbol, this.Compilation);
						if (methodSymbol != null)
						{
							instance2.Add(methodSymbol.GetPublicSymbol());
						}
					}
				}
				instance3.Free();
			}
			ImmutableArray<ISymbol> immutableArray = instance2.ToImmutableAndFree();
			if (name != null)
			{
				return immutableArray;
			}
			return CSharpSemanticModel.FilterNotReferencable(immutableArray);
		}

		// Token: 0x060014E2 RID: 5346 RVA: 0x0007110C File Offset: 0x0006F30C
		private void AppendSymbolsWithName(ArrayBuilder<ISymbol> results, string name, Binder binder, Microsoft.CodeAnalysis.CSharp.Symbols.NamespaceOrTypeSymbol container, LookupOptions options, LookupSymbolsInfo info)
		{
			AbstractLookupSymbolsInfo<Symbol>.IArityEnumerable arityEnumerable;
			Symbol symbol;
			if (info.TryGetAritiesAndUniqueSymbol(name, out arityEnumerable, out symbol))
			{
				if (symbol != null)
				{
					results.Add(this.RemapSymbolIfNecessary(symbol).GetPublicSymbol());
					return;
				}
				if (arityEnumerable != null)
				{
					using (AbstractLookupSymbolsInfo<Symbol>.ArityEnumerator enumerator = arityEnumerable.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							int num = enumerator.Current;
							this.AppendSymbolsWithNameAndArity(results, name, num, binder, container, options);
						}
						return;
					}
				}
				this.AppendSymbolsWithNameAndArity(results, name, 0, binder, container, options);
			}
		}

		// Token: 0x060014E3 RID: 5347 RVA: 0x00071198 File Offset: 0x0006F398
		private void AppendSymbolsWithNameAndArity(ArrayBuilder<ISymbol> results, string name, int arity, Binder binder, Microsoft.CodeAnalysis.CSharp.Symbols.NamespaceOrTypeSymbol container, LookupOptions options)
		{
			LookupResult instance = LookupResult.GetInstance();
			CompoundUseSiteInfo<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol> discarded = CompoundUseSiteInfo<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>.Discarded;
			binder.LookupSymbolsSimpleName(instance, container, name, arity, null, options & ~LookupOptions.IncludeExtensionMethods, false, ref discarded);
			if (instance.IsMultiViable)
			{
				if (instance.Symbols.Any<Symbol>((Symbol t) => t.Kind == SymbolKind.NamedType || t.Kind == SymbolKind.Namespace || t.Kind == SymbolKind.ErrorType))
				{
					bool flag;
					Symbol symbol = binder.ResultSymbol(instance, name, arity, this.Root, BindingDiagnosticBag.Discarded, true, out flag, container, options);
					if (!flag)
					{
						results.Add(this.RemapSymbolIfNecessary(symbol).GetPublicSymbol());
					}
					else
					{
						foreach (Symbol symbol2 in instance.Symbols)
						{
							results.Add(this.RemapSymbolIfNecessary(symbol2).GetPublicSymbol());
						}
					}
				}
				else
				{
					foreach (Symbol symbol3 in instance.Symbols)
					{
						results.Add(this.RemapSymbolIfNecessary(symbol3).GetPublicSymbol());
					}
				}
			}
			instance.Free();
		}

		// Token: 0x060014E4 RID: 5348 RVA: 0x000712A4 File Offset: 0x0006F4A4
		private Symbol RemapSymbolIfNecessary(Symbol symbol)
		{
			if (!(symbol is Microsoft.CodeAnalysis.CSharp.Symbols.LocalSymbol) && !(symbol is Microsoft.CodeAnalysis.CSharp.Symbols.ParameterSymbol))
			{
				Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol methodSymbol = symbol as Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol;
				if (methodSymbol == null || methodSymbol.MethodKind != MethodKind.AnonymousFunction)
				{
					return symbol;
				}
			}
			return this.RemapSymbolIfNecessaryCore(symbol);
		}

		// Token: 0x060014E5 RID: 5349
		internal abstract Symbol RemapSymbolIfNecessaryCore(Symbol symbol);

		// Token: 0x060014E6 RID: 5350 RVA: 0x000712DC File Offset: 0x0006F4DC
		private static ImmutableArray<ISymbol> FilterNotReferencable(ImmutableArray<ISymbol> sealedResults)
		{
			ArrayBuilder<ISymbol> arrayBuilder = null;
			int num = 0;
			foreach (ISymbol symbol in sealedResults)
			{
				if (symbol.CanBeReferencedByName)
				{
					if (arrayBuilder != null)
					{
						arrayBuilder.Add(symbol);
					}
				}
				else if (arrayBuilder == null)
				{
					arrayBuilder = ArrayBuilder<ISymbol>.GetInstance();
					arrayBuilder.AddRange(sealedResults, num);
				}
				num++;
			}
			if (arrayBuilder == null)
			{
				return sealedResults;
			}
			return arrayBuilder.ToImmutableAndFree();
		}

		// Token: 0x060014E7 RID: 5351 RVA: 0x0007133C File Offset: 0x0006F53C
		public bool IsAccessible(int position, Symbol symbol)
		{
			position = this.CheckAndAdjustPosition(position);
			if (symbol == null)
			{
				throw new ArgumentNullException("symbol");
			}
			Binder enclosingBinder = this.GetEnclosingBinder(position);
			if (enclosingBinder != null)
			{
				CompoundUseSiteInfo<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol> discarded = CompoundUseSiteInfo<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>.Discarded;
				return enclosingBinder.IsAccessible(symbol, ref discarded, null, null);
			}
			return false;
		}

		// Token: 0x060014E8 RID: 5352 RVA: 0x0007137E File Offset: 0x0006F57E
		public bool IsEventUsableAsField(int position, Microsoft.CodeAnalysis.CSharp.Symbols.EventSymbol symbol)
		{
			return symbol != null && symbol.HasAssociatedField && this.IsAccessible(position, symbol.AssociatedField);
		}

		// Token: 0x060014E9 RID: 5353 RVA: 0x0007139C File Offset: 0x0006F59C
		private bool IsInTypeofExpression(int position)
		{
			for (SyntaxNode syntaxNode = this.Root.FindToken(position, false).Parent; syntaxNode != this.Root; syntaxNode = syntaxNode.ParentOrStructuredTriviaParent)
			{
				if (syntaxNode.IsKind(SyntaxKind.TypeOfExpression))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060014EA RID: 5354 RVA: 0x000713E4 File Offset: 0x0006F5E4
		internal SymbolInfo GetSymbolInfoForNode(CSharpSemanticModel.SymbolInfoOptions options, BoundNode lowestBoundNode, BoundNode highestBoundNode, BoundNode boundNodeForSyntacticParent, Binder binderOpt)
		{
			BoundRecursivePattern boundRecursivePattern = highestBoundNode as BoundRecursivePattern;
			if (boundRecursivePattern != null)
			{
				return this.GetSymbolInfoForDeconstruction(boundRecursivePattern);
			}
			BoundSubpattern boundSubpattern = lowestBoundNode as BoundSubpattern;
			if (boundSubpattern != null)
			{
				return this.GetSymbolInfoForSubpattern(boundSubpattern);
			}
			BoundExpression boundExpression = lowestBoundNode as BoundExpression;
			if (boundExpression == null)
			{
				return SymbolInfo.None;
			}
			BoundExpression boundExpression2 = boundExpression;
			bool flag;
			LookupResultKind lookupResultKind;
			ImmutableArray<Symbol> immutableArray2;
			ImmutableArray<Symbol> immutableArray = this.GetSemanticSymbols(boundExpression2, boundNodeForSyntacticParent, binderOpt, options, out flag, out lookupResultKind, out immutableArray2);
			BoundExpression boundExpression3 = highestBoundNode as BoundExpression;
			if (boundExpression3 != null)
			{
				bool flag2;
				LookupResultKind lookupResultKind2;
				ImmutableArray<Symbol> immutableArray3;
				ImmutableArray<Symbol> semanticSymbols = this.GetSemanticSymbols(boundExpression3, boundNodeForSyntacticParent, binderOpt, options, out flag2, out lookupResultKind2, out immutableArray3);
				if ((immutableArray.Length != 1 || lookupResultKind == LookupResultKind.OverloadResolutionFailure) && semanticSymbols.Length > 0)
				{
					immutableArray = semanticSymbols;
					lookupResultKind = lookupResultKind2;
					flag = flag2;
				}
				else if (lookupResultKind2 != LookupResultKind.Empty && lookupResultKind2 < lookupResultKind)
				{
					lookupResultKind = lookupResultKind2;
					flag = flag2;
				}
				else if (boundExpression3.Kind == BoundKind.TypeOrValueExpression)
				{
					immutableArray = semanticSymbols;
					lookupResultKind = lookupResultKind2;
					flag = flag2;
				}
				else if (boundExpression3.Kind == BoundKind.UnaryOperator && CSharpSemanticModel.IsUserDefinedTrueOrFalse((BoundUnaryOperator)boundExpression3))
				{
					immutableArray = semanticSymbols;
					lookupResultKind = lookupResultKind2;
					flag = flag2;
				}
			}
			if (lookupResultKind == LookupResultKind.Empty)
			{
				return SymbolInfoFactory.Create(ImmutableArray<Symbol>.Empty, LookupResultKind.Empty, flag);
			}
			ArrayBuilder<Symbol> instance = ArrayBuilder<Symbol>.GetInstance(immutableArray.Length);
			foreach (Symbol symbol in immutableArray)
			{
				CSharpSemanticModel.AddUnwrappingErrorTypes(instance, symbol);
			}
			immutableArray = instance.ToImmutableAndFree();
			if ((options & CSharpSemanticModel.SymbolInfoOptions.ResolveAliases) != (CSharpSemanticModel.SymbolInfoOptions)0)
			{
				immutableArray = CSharpSemanticModel.UnwrapAliases(immutableArray);
			}
			if (lookupResultKind == LookupResultKind.Viable && immutableArray.Length > 1)
			{
				lookupResultKind = LookupResultKind.OverloadResolutionFailure;
			}
			return SymbolInfoFactory.Create(immutableArray, lookupResultKind, flag);
		}

		// Token: 0x060014EB RID: 5355 RVA: 0x0007154C File Offset: 0x0006F74C
		private SymbolInfo GetSymbolInfoForSubpattern(BoundSubpattern subpattern)
		{
			Symbol symbol = subpattern.Symbol;
			Microsoft.CodeAnalysis.CSharp.Symbols.ErrorTypeSymbol errorTypeSymbol = ((symbol != null) ? symbol.OriginalDefinition : null) as Microsoft.CodeAnalysis.CSharp.Symbols.ErrorTypeSymbol;
			if (errorTypeSymbol != null)
			{
				return new SymbolInfo(null, errorTypeSymbol.CandidateSymbols.GetPublicSymbols(), errorTypeSymbol.ResultKind.ToCandidateReason());
			}
			return new SymbolInfo(subpattern.Symbol.GetPublicSymbol(), CandidateReason.None);
		}

		// Token: 0x060014EC RID: 5356 RVA: 0x000715A2 File Offset: 0x0006F7A2
		private SymbolInfo GetSymbolInfoForDeconstruction(BoundRecursivePattern pat)
		{
			return new SymbolInfo(pat.DeconstructMethod.GetPublicSymbol(), CandidateReason.None);
		}

		// Token: 0x060014ED RID: 5357 RVA: 0x000715B8 File Offset: 0x0006F7B8
		private static void AddUnwrappingErrorTypes(ArrayBuilder<Symbol> builder, Symbol s)
		{
			Microsoft.CodeAnalysis.CSharp.Symbols.ErrorTypeSymbol errorTypeSymbol = s.OriginalDefinition as Microsoft.CodeAnalysis.CSharp.Symbols.ErrorTypeSymbol;
			if (errorTypeSymbol != null)
			{
				builder.AddRange(errorTypeSymbol.CandidateSymbols);
				return;
			}
			builder.Add(s);
		}

		// Token: 0x060014EE RID: 5358 RVA: 0x000715E8 File Offset: 0x0006F7E8
		private static bool IsUserDefinedTrueOrFalse(BoundUnaryOperator @operator)
		{
			UnaryOperatorKind operatorKind = @operator.OperatorKind;
			return operatorKind == UnaryOperatorKind.UserDefinedTrue || operatorKind == UnaryOperatorKind.UserDefinedFalse;
		}

		// Token: 0x060014EF RID: 5359 RVA: 0x00071610 File Offset: 0x0006F810
		internal CSharpTypeInfo GetTypeInfoForNode(BoundNode lowestBoundNode, BoundNode highestBoundNode, BoundNode boundNodeForSyntacticParent)
		{
			BoundPattern boundPattern;
			if ((boundPattern = lowestBoundNode as BoundPattern) == null && (boundPattern = highestBoundNode as BoundPattern) == null)
			{
				BoundSubpattern boundSubpattern = highestBoundNode as BoundSubpattern;
				boundPattern = ((boundSubpattern != null) ? boundSubpattern.Pattern : null);
			}
			BoundPattern boundPattern2 = boundPattern;
			if (boundPattern2 != null)
			{
				CompoundUseSiteInfo<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol> discarded = CompoundUseSiteInfo<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>.Discarded;
				return new CSharpTypeInfo(boundPattern2.InputType, boundPattern2.NarrowedType, default(NullabilityInfo), default(NullabilityInfo), this.Compilation.Conversions.ClassifyBuiltInConversion(boundPattern2.InputType, boundPattern2.NarrowedType, ref discarded));
			}
			BoundExpression boundExpression = lowestBoundNode as BoundExpression;
			BoundExpression boundExpression2 = highestBoundNode as BoundExpression;
			if (boundExpression != null && (boundNodeForSyntacticParent == null || boundNodeForSyntacticParent.Syntax.Kind() != SyntaxKind.ObjectCreationExpression || ((ObjectCreationExpressionSyntax)boundNodeForSyntacticParent.Syntax).Type != boundExpression.Syntax))
			{
				Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol typeSymbol = null;
				NullabilityInfo nullabilityInfo = boundExpression.TopLevelNullability;
				if (boundExpression.HasExpressionType())
				{
					typeSymbol = boundExpression.Type;
					BoundLocal boundLocal = boundExpression as BoundLocal;
					if (boundLocal == null)
					{
						BoundConvertedTupleLiteral boundConvertedTupleLiteral = boundExpression as BoundConvertedTupleLiteral;
						if (boundConvertedTupleLiteral != null)
						{
							BoundTupleLiteral sourceTuple = boundConvertedTupleLiteral.SourceTuple;
							if (sourceTuple != null)
							{
								typeSymbol = sourceTuple.Type;
							}
						}
					}
					else
					{
						ExtendedErrorTypeSymbol extendedErrorTypeSymbol = typeSymbol as ExtendedErrorTypeSymbol;
						if (extendedErrorTypeSymbol != null && extendedErrorTypeSymbol.VariableUsedBeforeDeclaration)
						{
							typeSymbol = boundLocal.LocalSymbol.Type;
							nullabilityInfo = boundLocal.LocalSymbol.TypeWithAnnotations.NullableAnnotation.ToNullabilityInfo(typeSymbol);
						}
					}
				}
				BoundKind boundKind = ((boundExpression2 != null) ? boundExpression2.Kind : BoundKind.NoOpStatement);
				Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol typeSymbol2;
				NullabilityInfo nullabilityInfo2;
				Conversion conversion;
				if (boundKind == BoundKind.Lambda)
				{
					BoundLambda boundLambda = (BoundLambda)boundExpression2;
					typeSymbol2 = boundLambda.Type;
					typeSymbol = null;
					nullabilityInfo = default(NullabilityInfo);
					nullabilityInfo2 = new NullabilityInfo(NullableAnnotation.NotAnnotated, NullableFlowState.NotNull);
					conversion = new Conversion(ConversionKind.AnonymousFunction, boundLambda.Symbol, false);
				}
				else
				{
					BoundConversion boundConversion = boundExpression2 as BoundConversion;
					if (boundConversion != null && boundConversion.Conversion.IsTupleLiteralConversion)
					{
						BoundConversion boundConversion2 = (BoundConversion)boundExpression2;
						if (boundConversion2.Operand.Kind == BoundKind.ConvertedTupleLiteral)
						{
							BoundConvertedTupleLiteral boundConvertedTupleLiteral2 = (BoundConvertedTupleLiteral)boundConversion2.Operand;
							typeSymbol = boundConvertedTupleLiteral2.SourceTuple.Type;
							nullabilityInfo = boundConvertedTupleLiteral2.TopLevelNullability;
						}
						else
						{
							ValueTuple<Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol, NullabilityInfo> valueTuple = CSharpSemanticModel.<GetTypeInfoForNode>g__getTypeAndNullability|88_0(boundConversion2.Operand);
							typeSymbol = valueTuple.Item1;
							nullabilityInfo = valueTuple.Item2;
						}
						ValueTuple<Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol, NullabilityInfo> valueTuple2 = CSharpSemanticModel.<GetTypeInfoForNode>g__getTypeAndNullability|88_0(boundConversion2);
						typeSymbol2 = valueTuple2.Item1;
						nullabilityInfo2 = valueTuple2.Item2;
						conversion = boundConversion2.Conversion;
					}
					else if (boundKind == BoundKind.FixedLocalCollectionInitializer)
					{
						BoundFixedLocalCollectionInitializer boundFixedLocalCollectionInitializer = (BoundFixedLocalCollectionInitializer)boundExpression2;
						ValueTuple<Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol, NullabilityInfo> valueTuple3 = CSharpSemanticModel.<GetTypeInfoForNode>g__getTypeAndNullability|88_0(boundFixedLocalCollectionInitializer);
						typeSymbol2 = valueTuple3.Item1;
						nullabilityInfo2 = valueTuple3.Item2;
						ValueTuple<Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol, NullabilityInfo> valueTuple4 = CSharpSemanticModel.<GetTypeInfoForNode>g__getTypeAndNullability|88_0(boundFixedLocalCollectionInitializer.Expression);
						typeSymbol = valueTuple4.Item1;
						nullabilityInfo = valueTuple4.Item2;
						conversion = boundFixedLocalCollectionInitializer.ElementPointerTypeConversion;
					}
					else
					{
						BoundConvertedSwitchExpression boundConvertedSwitchExpression = boundExpression as BoundConvertedSwitchExpression;
						if (boundConvertedSwitchExpression != null && boundConvertedSwitchExpression.WasTargetTyped)
						{
							BoundConversion boundConversion3 = boundExpression2 as BoundConversion;
							if (boundConversion3 != null && boundConversion3.ConversionKind == ConversionKind.SwitchExpression)
							{
								typeSymbol = boundConvertedSwitchExpression.NaturalTypeOpt;
								typeSymbol2 = boundConvertedSwitchExpression.Type;
								nullabilityInfo2 = boundConvertedSwitchExpression.TopLevelNullability;
								conversion = (boundConvertedSwitchExpression.Conversion.IsValid ? boundConvertedSwitchExpression.Conversion : Conversion.NoConversion);
							}
							else
							{
								typeSymbol = boundConvertedSwitchExpression.NaturalTypeOpt;
								Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol typeSymbol3 = typeSymbol;
								NullabilityInfo nullabilityInfo3 = nullabilityInfo;
								typeSymbol2 = typeSymbol3;
								nullabilityInfo2 = nullabilityInfo3;
								conversion = Conversion.Identity;
							}
						}
						else
						{
							BoundConditionalOperator boundConditionalOperator = boundExpression as BoundConditionalOperator;
							if (boundConditionalOperator != null && boundConditionalOperator.WasTargetTyped)
							{
								BoundConversion boundConversion3 = boundExpression2 as BoundConversion;
								if (boundConversion3 != null && boundConversion3.ConversionKind == ConversionKind.ConditionalExpression)
								{
									typeSymbol = boundConditionalOperator.NaturalTypeOpt;
									typeSymbol2 = boundConditionalOperator.Type;
									nullabilityInfo2 = nullabilityInfo;
									conversion = Conversion.MakeConditionalExpression(ImmutableArray<Conversion>.Empty);
								}
								else
								{
									typeSymbol = boundConditionalOperator.NaturalTypeOpt;
									Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol typeSymbol4 = typeSymbol;
									NullabilityInfo nullabilityInfo3 = nullabilityInfo;
									typeSymbol2 = typeSymbol4;
									nullabilityInfo2 = nullabilityInfo3;
									conversion = Conversion.Identity;
								}
							}
							else if (boundExpression2 != null && boundExpression2 != boundExpression && boundExpression2.HasExpressionType())
							{
								ValueTuple<Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol, NullabilityInfo> valueTuple5 = CSharpSemanticModel.<GetTypeInfoForNode>g__getTypeAndNullability|88_0(boundExpression2);
								typeSymbol2 = valueTuple5.Item1;
								nullabilityInfo2 = valueTuple5.Item2;
								if (boundKind != BoundKind.Conversion)
								{
									conversion = Conversion.Identity;
								}
								else if (((BoundConversion)boundExpression2).Operand.Kind != BoundKind.Conversion)
								{
									conversion = boundExpression2.GetConversion();
									if (conversion.Kind == ConversionKind.AnonymousFunction)
									{
										typeSymbol = null;
										nullabilityInfo = default(NullabilityInfo);
									}
								}
								else
								{
									Binder enclosingBinder = this.GetEnclosingBinder(boundExpression.Syntax.Span.Start);
									CompoundUseSiteInfo<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol> discarded2 = CompoundUseSiteInfo<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>.Discarded;
									conversion = enclosingBinder.Conversions.ClassifyConversionFromExpression(boundExpression, typeSymbol2, ref discarded2, false);
								}
							}
							else if (boundNodeForSyntacticParent != null && boundNodeForSyntacticParent.Kind == BoundKind.DelegateCreationExpression)
							{
								BoundDelegateCreationExpression boundDelegateCreationExpression = (BoundDelegateCreationExpression)boundNodeForSyntacticParent;
								ValueTuple<Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol, NullabilityInfo> valueTuple6 = CSharpSemanticModel.<GetTypeInfoForNode>g__getTypeAndNullability|88_0(boundDelegateCreationExpression);
								typeSymbol2 = valueTuple6.Item1;
								nullabilityInfo2 = valueTuple6.Item2;
								BoundKind kind = boundExpression.Kind;
								if (kind != BoundKind.MethodGroup)
								{
									if (kind != BoundKind.Lambda)
									{
										if (kind != BoundKind.UnboundLambda)
										{
											conversion = Conversion.Identity;
										}
										else
										{
											BoundLambda boundLambda2 = ((UnboundLambda)boundExpression).BindForErrorRecovery();
											conversion = new Conversion(ConversionKind.AnonymousFunction, boundLambda2.Symbol, boundDelegateCreationExpression.IsExtensionMethod);
										}
									}
									else
									{
										BoundLambda boundLambda3 = (BoundLambda)boundExpression;
										conversion = new Conversion(ConversionKind.AnonymousFunction, boundLambda3.Symbol, boundDelegateCreationExpression.IsExtensionMethod);
									}
								}
								else
								{
									conversion = new Conversion(ConversionKind.MethodGroup, boundDelegateCreationExpression.MethodOpt, boundDelegateCreationExpression.IsExtensionMethod);
								}
							}
							else
							{
								BoundConversion boundConversion3 = boundExpression as BoundConversion;
								if (boundConversion3 != null && boundConversion3.ConversionKind == ConversionKind.MethodGroup)
								{
									Conversion conversion2 = boundConversion3.Conversion;
									Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol type = boundConversion3.Type;
									if (type != null && type.TypeKind == TypeKind.FunctionPointer)
									{
										Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol symbolOpt = boundConversion3.SymbolOpt;
										typeSymbol2 = typeSymbol;
										nullabilityInfo2 = nullabilityInfo;
										conversion = conversion2;
										typeSymbol = null;
										nullabilityInfo = new NullabilityInfo(NullableAnnotation.NotAnnotated, NullableFlowState.NotNull);
										goto IL_0560;
									}
								}
								typeSymbol2 = typeSymbol;
								nullabilityInfo2 = nullabilityInfo;
								conversion = Conversion.Identity;
							}
						}
					}
				}
				IL_0560:
				return new CSharpTypeInfo(typeSymbol, typeSymbol2, nullabilityInfo, nullabilityInfo2, conversion);
			}
			return CSharpTypeInfo.None;
		}

		// Token: 0x060014F0 RID: 5360 RVA: 0x00071B94 File Offset: 0x0006FD94
		internal ImmutableArray<Symbol> GetMemberGroupForNode(CSharpSemanticModel.SymbolInfoOptions options, BoundNode lowestBoundNode, BoundNode boundNodeForSyntacticParent, Binder binderOpt)
		{
			BoundExpression boundExpression = lowestBoundNode as BoundExpression;
			if (boundExpression != null)
			{
				bool flag;
				LookupResultKind lookupResultKind;
				ImmutableArray<Symbol> immutableArray;
				this.GetSemanticSymbols(boundExpression, boundNodeForSyntacticParent, binderOpt, options, out flag, out lookupResultKind, out immutableArray);
				return immutableArray;
			}
			return ImmutableArray<Symbol>.Empty;
		}

		// Token: 0x060014F1 RID: 5361 RVA: 0x00071BC4 File Offset: 0x0006FDC4
		internal ImmutableArray<IPropertySymbol> GetIndexerGroupForNode(BoundNode lowestBoundNode, Binder binderOpt)
		{
			BoundExpression boundExpression = lowestBoundNode as BoundExpression;
			if (boundExpression != null && boundExpression.Kind != BoundKind.TypeExpression)
			{
				return this.GetIndexerGroupSemanticSymbols(boundExpression, binderOpt);
			}
			return ImmutableArray<IPropertySymbol>.Empty;
		}

		// Token: 0x060014F2 RID: 5362 RVA: 0x00071BF4 File Offset: 0x0006FDF4
		internal static SymbolInfo GetSymbolInfoForSymbol(Symbol symbol, CSharpSemanticModel.SymbolInfoOptions options)
		{
			Symbol symbol2 = CSharpSemanticModel.UnwrapAlias(symbol);
			Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol typeSymbol = symbol2 as Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol;
			Microsoft.CodeAnalysis.CSharp.Symbols.ErrorTypeSymbol errorTypeSymbol = ((typeSymbol != null) ? (typeSymbol.OriginalDefinition as Microsoft.CodeAnalysis.CSharp.Symbols.ErrorTypeSymbol) : null);
			if (errorTypeSymbol != null)
			{
				ImmutableArray<Symbol> immutableArray = ImmutableArray<Symbol>.Empty;
				LookupResultKind resultKind = errorTypeSymbol.ResultKind;
				if (resultKind != LookupResultKind.Empty)
				{
					immutableArray = errorTypeSymbol.CandidateSymbols;
				}
				if ((options & CSharpSemanticModel.SymbolInfoOptions.ResolveAliases) != (CSharpSemanticModel.SymbolInfoOptions)0)
				{
					immutableArray = CSharpSemanticModel.UnwrapAliases(immutableArray);
				}
				return SymbolInfoFactory.Create(immutableArray, resultKind, false);
			}
			return new SymbolInfo((((options & CSharpSemanticModel.SymbolInfoOptions.ResolveAliases) != (CSharpSemanticModel.SymbolInfoOptions)0) ? symbol2 : symbol).GetPublicSymbol(), ImmutableArray<ISymbol>.Empty, CandidateReason.None);
		}

		// Token: 0x060014F3 RID: 5363 RVA: 0x00071C6C File Offset: 0x0006FE6C
		internal static CSharpTypeInfo GetTypeInfoForSymbol(Symbol symbol)
		{
			Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol typeSymbol = CSharpSemanticModel.UnwrapAlias(symbol) as Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol;
			return new CSharpTypeInfo(typeSymbol, typeSymbol, default(NullabilityInfo), default(NullabilityInfo), Conversion.Identity);
		}

		// Token: 0x060014F4 RID: 5364 RVA: 0x00071CA4 File Offset: 0x0006FEA4
		protected static Symbol UnwrapAlias(Symbol symbol)
		{
			Microsoft.CodeAnalysis.CSharp.Symbols.AliasSymbol aliasSymbol = symbol as Microsoft.CodeAnalysis.CSharp.Symbols.AliasSymbol;
			if (aliasSymbol == null)
			{
				return symbol;
			}
			return aliasSymbol.Target;
		}

		// Token: 0x060014F5 RID: 5365 RVA: 0x00071CC4 File Offset: 0x0006FEC4
		protected static ImmutableArray<Symbol> UnwrapAliases(ImmutableArray<Symbol> symbols)
		{
			bool flag = false;
			ImmutableArray<Symbol>.Enumerator enumerator = symbols.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.Kind == SymbolKind.Alias)
				{
					flag = true;
				}
			}
			if (!flag)
			{
				return symbols;
			}
			ArrayBuilder<Symbol> instance = ArrayBuilder<Symbol>.GetInstance();
			foreach (Symbol symbol in symbols)
			{
				CSharpSemanticModel.AddUnwrappingErrorTypes(instance, CSharpSemanticModel.UnwrapAlias(symbol));
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x060014F6 RID: 5366 RVA: 0x00071D30 File Offset: 0x0006FF30
		internal virtual BoundNode Bind(Binder binder, CSharpSyntaxNode node, BindingDiagnosticBag diagnostics)
		{
			ExpressionSyntax expressionSyntax = node as ExpressionSyntax;
			if (expressionSyntax == null)
			{
				StatementSyntax statementSyntax = node as StatementSyntax;
				if (statementSyntax != null)
				{
					return binder.BindStatement(statementSyntax, diagnostics);
				}
				GlobalStatementSyntax globalStatementSyntax = node as GlobalStatementSyntax;
				if (globalStatementSyntax == null)
				{
					return null;
				}
				BoundStatement boundStatement = binder.BindStatement(globalStatementSyntax.Statement, diagnostics);
				return new BoundGlobalStatementInitializer(node, boundStatement, false);
			}
			else
			{
				if (!expressionSyntax.Parent.IsKind(SyntaxKind.GotoStatement))
				{
					return binder.BindNamespaceOrTypeOrExpression(expressionSyntax, diagnostics);
				}
				return binder.BindLabel(expressionSyntax, diagnostics);
			}
		}

		// Token: 0x060014F7 RID: 5367 RVA: 0x00071DA2 File Offset: 0x0006FFA2
		public virtual ControlFlowAnalysis AnalyzeControlFlow(StatementSyntax firstStatement, StatementSyntax lastStatement)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060014F8 RID: 5368 RVA: 0x00071DA9 File Offset: 0x0006FFA9
		public virtual ControlFlowAnalysis AnalyzeControlFlow(StatementSyntax statement)
		{
			return this.AnalyzeControlFlow(statement, statement);
		}

		// Token: 0x060014F9 RID: 5369 RVA: 0x00071DB3 File Offset: 0x0006FFB3
		public virtual DataFlowAnalysis AnalyzeDataFlow(ExpressionSyntax expression)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060014FA RID: 5370 RVA: 0x00071DBA File Offset: 0x0006FFBA
		public virtual DataFlowAnalysis AnalyzeDataFlow(StatementSyntax firstStatement, StatementSyntax lastStatement)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060014FB RID: 5371 RVA: 0x00071DC1 File Offset: 0x0006FFC1
		public virtual DataFlowAnalysis AnalyzeDataFlow(StatementSyntax statement)
		{
			return this.AnalyzeDataFlow(statement, statement);
		}

		// Token: 0x060014FC RID: 5372 RVA: 0x00071DCB File Offset: 0x0006FFCB
		public bool TryGetSpeculativeSemanticModelForMethodBody(int position, BaseMethodDeclarationSyntax method, out SemanticModel speculativeModel)
		{
			this.CheckModelAndSyntaxNodeToSpeculate(method);
			return this.TryGetSpeculativeSemanticModelForMethodBodyCore((SyntaxTreeSemanticModel)this, position, method, out speculativeModel);
		}

		// Token: 0x060014FD RID: 5373
		internal abstract bool TryGetSpeculativeSemanticModelForMethodBodyCore(SyntaxTreeSemanticModel parentModel, int position, BaseMethodDeclarationSyntax method, out SemanticModel speculativeModel);

		// Token: 0x060014FE RID: 5374 RVA: 0x00071DE3 File Offset: 0x0006FFE3
		public bool TryGetSpeculativeSemanticModelForMethodBody(int position, AccessorDeclarationSyntax accessor, out SemanticModel speculativeModel)
		{
			this.CheckModelAndSyntaxNodeToSpeculate(accessor);
			return this.TryGetSpeculativeSemanticModelForMethodBodyCore((SyntaxTreeSemanticModel)this, position, accessor, out speculativeModel);
		}

		// Token: 0x060014FF RID: 5375
		internal abstract bool TryGetSpeculativeSemanticModelForMethodBodyCore(SyntaxTreeSemanticModel parentModel, int position, AccessorDeclarationSyntax accessor, out SemanticModel speculativeModel);

		// Token: 0x06001500 RID: 5376 RVA: 0x00071DFB File Offset: 0x0006FFFB
		public bool TryGetSpeculativeSemanticModel(int position, TypeSyntax type, out SemanticModel speculativeModel, SpeculativeBindingOption bindingOption = SpeculativeBindingOption.BindAsExpression)
		{
			this.CheckModelAndSyntaxNodeToSpeculate(type);
			return this.TryGetSpeculativeSemanticModelCore((SyntaxTreeSemanticModel)this, position, type, bindingOption, out speculativeModel);
		}

		// Token: 0x06001501 RID: 5377
		internal abstract bool TryGetSpeculativeSemanticModelCore(SyntaxTreeSemanticModel parentModel, int position, TypeSyntax type, SpeculativeBindingOption bindingOption, out SemanticModel speculativeModel);

		// Token: 0x06001502 RID: 5378 RVA: 0x00071E15 File Offset: 0x00070015
		public bool TryGetSpeculativeSemanticModel(int position, StatementSyntax statement, out SemanticModel speculativeModel)
		{
			this.CheckModelAndSyntaxNodeToSpeculate(statement);
			return this.TryGetSpeculativeSemanticModelCore((SyntaxTreeSemanticModel)this, position, statement, out speculativeModel);
		}

		// Token: 0x06001503 RID: 5379
		internal abstract bool TryGetSpeculativeSemanticModelCore(SyntaxTreeSemanticModel parentModel, int position, StatementSyntax statement, out SemanticModel speculativeModel);

		// Token: 0x06001504 RID: 5380 RVA: 0x00071E2D File Offset: 0x0007002D
		public bool TryGetSpeculativeSemanticModel(int position, EqualsValueClauseSyntax initializer, out SemanticModel speculativeModel)
		{
			this.CheckModelAndSyntaxNodeToSpeculate(initializer);
			return this.TryGetSpeculativeSemanticModelCore((SyntaxTreeSemanticModel)this, position, initializer, out speculativeModel);
		}

		// Token: 0x06001505 RID: 5381
		internal abstract bool TryGetSpeculativeSemanticModelCore(SyntaxTreeSemanticModel parentModel, int position, EqualsValueClauseSyntax initializer, out SemanticModel speculativeModel);

		// Token: 0x06001506 RID: 5382 RVA: 0x00071E45 File Offset: 0x00070045
		public bool TryGetSpeculativeSemanticModel(int position, ArrowExpressionClauseSyntax expressionBody, out SemanticModel speculativeModel)
		{
			this.CheckModelAndSyntaxNodeToSpeculate(expressionBody);
			return this.TryGetSpeculativeSemanticModelCore((SyntaxTreeSemanticModel)this, position, expressionBody, out speculativeModel);
		}

		// Token: 0x06001507 RID: 5383
		internal abstract bool TryGetSpeculativeSemanticModelCore(SyntaxTreeSemanticModel parentModel, int position, ArrowExpressionClauseSyntax expressionBody, out SemanticModel speculativeModel);

		// Token: 0x06001508 RID: 5384 RVA: 0x00071E5D File Offset: 0x0007005D
		public bool TryGetSpeculativeSemanticModel(int position, ConstructorInitializerSyntax constructorInitializer, out SemanticModel speculativeModel)
		{
			this.CheckModelAndSyntaxNodeToSpeculate(constructorInitializer);
			return this.TryGetSpeculativeSemanticModelCore((SyntaxTreeSemanticModel)this, position, constructorInitializer, out speculativeModel);
		}

		// Token: 0x06001509 RID: 5385
		internal abstract bool TryGetSpeculativeSemanticModelCore(SyntaxTreeSemanticModel parentModel, int position, ConstructorInitializerSyntax constructorInitializer, out SemanticModel speculativeModel);

		// Token: 0x0600150A RID: 5386 RVA: 0x00071E75 File Offset: 0x00070075
		public bool TryGetSpeculativeSemanticModel(int position, PrimaryConstructorBaseTypeSyntax constructorInitializer, out SemanticModel speculativeModel)
		{
			this.CheckModelAndSyntaxNodeToSpeculate(constructorInitializer);
			return this.TryGetSpeculativeSemanticModelCore((SyntaxTreeSemanticModel)this, position, constructorInitializer, out speculativeModel);
		}

		// Token: 0x0600150B RID: 5387
		internal abstract bool TryGetSpeculativeSemanticModelCore(SyntaxTreeSemanticModel parentModel, int position, PrimaryConstructorBaseTypeSyntax constructorInitializer, out SemanticModel speculativeModel);

		// Token: 0x0600150C RID: 5388 RVA: 0x00071E8D File Offset: 0x0007008D
		public bool TryGetSpeculativeSemanticModel(int position, CrefSyntax crefSyntax, out SemanticModel speculativeModel)
		{
			this.CheckModelAndSyntaxNodeToSpeculate(crefSyntax);
			return this.TryGetSpeculativeSemanticModelCore((SyntaxTreeSemanticModel)this, position, crefSyntax, out speculativeModel);
		}

		// Token: 0x0600150D RID: 5389
		internal abstract bool TryGetSpeculativeSemanticModelCore(SyntaxTreeSemanticModel parentModel, int position, CrefSyntax crefSyntax, out SemanticModel speculativeModel);

		// Token: 0x0600150E RID: 5390 RVA: 0x00071EA8 File Offset: 0x000700A8
		public bool TryGetSpeculativeSemanticModel(int position, AttributeSyntax attribute, out SemanticModel speculativeModel)
		{
			this.CheckModelAndSyntaxNodeToSpeculate(attribute);
			Binder speculativeBinderForAttribute = this.GetSpeculativeBinderForAttribute(position, attribute);
			if (speculativeBinderForAttribute == null)
			{
				speculativeModel = null;
				return false;
			}
			Microsoft.CodeAnalysis.CSharp.Symbols.AliasSymbol aliasSymbol;
			Microsoft.CodeAnalysis.CSharp.Symbols.NamedTypeSymbol namedTypeSymbol = (Microsoft.CodeAnalysis.CSharp.Symbols.NamedTypeSymbol)speculativeBinderForAttribute.BindType(attribute.Name, BindingDiagnosticBag.Discarded, out aliasSymbol, null).Type;
			speculativeModel = ((SyntaxTreeSemanticModel)this).CreateSpeculativeAttributeSemanticModel(position, attribute, speculativeBinderForAttribute, aliasSymbol, namedTypeSymbol);
			return true;
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x0600150F RID: 5391
		public new abstract CSharpSemanticModel ParentModel { get; }

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06001510 RID: 5392
		public new abstract SyntaxTree SyntaxTree { get; }

		// Token: 0x06001511 RID: 5393
		public abstract Conversion ClassifyConversion(ExpressionSyntax expression, ITypeSymbol destination, bool isExplicitInSource = false);

		// Token: 0x06001512 RID: 5394 RVA: 0x00071F04 File Offset: 0x00070104
		public Conversion ClassifyConversion(int position, ExpressionSyntax expression, ITypeSymbol destination, bool isExplicitInSource = false)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol typeSymbol = destination.EnsureCSharpSymbolOrNull("destination");
			if (expression.Kind() == SyntaxKind.DeclarationExpression)
			{
				return Conversion.NoConversion;
			}
			if (isExplicitInSource)
			{
				return this.ClassifyConversionForCast(position, expression, typeSymbol);
			}
			position = this.CheckAndAdjustPosition(position);
			Binder enclosingBinder = this.GetEnclosingBinder(position);
			if (enclosingBinder != null)
			{
				BoundExpression boundExpression = enclosingBinder.BindExpression(expression, BindingDiagnosticBag.Discarded);
				if (boundExpression != null && !typeSymbol.IsErrorType())
				{
					CompoundUseSiteInfo<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol> discarded = CompoundUseSiteInfo<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>.Discarded;
					return enclosingBinder.Conversions.ClassifyConversionFromExpression(boundExpression, typeSymbol, ref discarded, false);
				}
			}
			return Conversion.NoConversion;
		}

		// Token: 0x06001513 RID: 5395
		internal abstract Conversion ClassifyConversionForCast(ExpressionSyntax expression, Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol destination);

		// Token: 0x06001514 RID: 5396 RVA: 0x00071F94 File Offset: 0x00070194
		internal Conversion ClassifyConversionForCast(int position, ExpressionSyntax expression, Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol destination)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			position = this.CheckAndAdjustPosition(position);
			Binder enclosingBinder = this.GetEnclosingBinder(position);
			if (enclosingBinder != null)
			{
				BoundExpression boundExpression = enclosingBinder.BindExpression(expression, BindingDiagnosticBag.Discarded);
				if (boundExpression != null && !destination.IsErrorType())
				{
					CompoundUseSiteInfo<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol> discarded = CompoundUseSiteInfo<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>.Discarded;
					return enclosingBinder.Conversions.ClassifyConversionFromExpression(boundExpression, destination, ref discarded, true);
				}
			}
			return Conversion.NoConversion;
		}

		// Token: 0x06001515 RID: 5397
		public abstract ISymbol GetDeclaredSymbol(MemberDeclarationSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x06001516 RID: 5398
		public abstract ISymbol GetDeclaredSymbol(LocalFunctionStatementSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x06001517 RID: 5399
		public abstract IMethodSymbol GetDeclaredSymbol(CompilationUnitSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x06001518 RID: 5400
		public abstract INamespaceSymbol GetDeclaredSymbol(NamespaceDeclarationSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x06001519 RID: 5401
		public abstract INamedTypeSymbol GetDeclaredSymbol(BaseTypeDeclarationSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x0600151A RID: 5402
		public abstract INamedTypeSymbol GetDeclaredSymbol(DelegateDeclarationSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x0600151B RID: 5403
		public abstract IFieldSymbol GetDeclaredSymbol(EnumMemberDeclarationSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x0600151C RID: 5404
		public abstract IMethodSymbol GetDeclaredSymbol(BaseMethodDeclarationSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x0600151D RID: 5405
		public abstract ISymbol GetDeclaredSymbol(BasePropertyDeclarationSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x0600151E RID: 5406
		public abstract IPropertySymbol GetDeclaredSymbol(PropertyDeclarationSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x0600151F RID: 5407
		public abstract IPropertySymbol GetDeclaredSymbol(IndexerDeclarationSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x06001520 RID: 5408
		public abstract IEventSymbol GetDeclaredSymbol(EventDeclarationSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x06001521 RID: 5409
		public abstract IPropertySymbol GetDeclaredSymbol(AnonymousObjectMemberDeclaratorSyntax declaratorSyntax, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x06001522 RID: 5410
		public abstract INamedTypeSymbol GetDeclaredSymbol(AnonymousObjectCreationExpressionSyntax declaratorSyntax, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x06001523 RID: 5411
		public abstract INamedTypeSymbol GetDeclaredSymbol(TupleExpressionSyntax declaratorSyntax, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x06001524 RID: 5412
		public abstract ISymbol GetDeclaredSymbol(ArgumentSyntax declaratorSyntax, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x06001525 RID: 5413
		public abstract IMethodSymbol GetDeclaredSymbol(AccessorDeclarationSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x06001526 RID: 5414
		public abstract IMethodSymbol GetDeclaredSymbol(ArrowExpressionClauseSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x06001527 RID: 5415
		public abstract ISymbol GetDeclaredSymbol(VariableDeclaratorSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x06001528 RID: 5416
		public abstract ISymbol GetDeclaredSymbol(SingleVariableDesignationSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x06001529 RID: 5417
		public abstract ILabelSymbol GetDeclaredSymbol(LabeledStatementSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x0600152A RID: 5418
		public abstract ILabelSymbol GetDeclaredSymbol(SwitchLabelSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x0600152B RID: 5419
		public abstract IAliasSymbol GetDeclaredSymbol(UsingDirectiveSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x0600152C RID: 5420
		public abstract IAliasSymbol GetDeclaredSymbol(ExternAliasDirectiveSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x0600152D RID: 5421
		public abstract IParameterSymbol GetDeclaredSymbol(ParameterSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x0600152E RID: 5422
		internal abstract ImmutableArray<ISymbol> GetDeclaredSymbols(BaseFieldDeclarationSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x0600152F RID: 5423 RVA: 0x00071FF8 File Offset: 0x000701F8
		protected Microsoft.CodeAnalysis.CSharp.Symbols.ParameterSymbol GetParameterSymbol(ImmutableArray<Microsoft.CodeAnalysis.CSharp.Symbols.ParameterSymbol> parameters, ParameterSyntax parameter, CancellationToken cancellationToken = default(CancellationToken))
		{
			foreach (Microsoft.CodeAnalysis.CSharp.Symbols.ParameterSymbol parameterSymbol in parameters)
			{
				cancellationToken.ThrowIfCancellationRequested();
				foreach (Location location in parameterSymbol.Locations)
				{
					cancellationToken.ThrowIfCancellationRequested();
					if (location.SourceTree == this.SyntaxTree && parameter.Span.Contains(location.SourceSpan))
					{
						return parameterSymbol;
					}
				}
			}
			return null;
		}

		// Token: 0x06001530 RID: 5424
		public abstract ITypeParameterSymbol GetDeclaredSymbol(TypeParameterSyntax typeParameter, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x06001531 RID: 5425 RVA: 0x0007207B File Offset: 0x0007027B
		internal BinderFlags GetSemanticModelBinderFlags()
		{
			if (!this.IgnoresAccessibility)
			{
				return BinderFlags.SemanticModel;
			}
			return BinderFlags.SemanticModel | BinderFlags.IgnoreAccessibility;
		}

		// Token: 0x06001532 RID: 5426 RVA: 0x00072090 File Offset: 0x00070290
		public ILocalSymbol GetDeclaredSymbol(ForEachStatementSyntax forEachStatement, CancellationToken cancellationToken = default(CancellationToken))
		{
			Binder enclosingBinder = this.GetEnclosingBinder(this.GetAdjustedNodePosition(forEachStatement));
			if (enclosingBinder == null)
			{
				return null;
			}
			Binder binder = enclosingBinder.GetBinder(forEachStatement);
			if (binder == null)
			{
				return null;
			}
			Microsoft.CodeAnalysis.CSharp.Symbols.LocalSymbol localSymbol = binder.GetDeclaredLocalsForScope(forEachStatement).FirstOrDefault<Microsoft.CodeAnalysis.CSharp.Symbols.LocalSymbol>();
			SourceLocalSymbol sourceLocalSymbol = localSymbol as SourceLocalSymbol;
			return ((sourceLocalSymbol != null && sourceLocalSymbol.DeclarationKind == LocalDeclarationKind.ForEachIterationVariable) ? this.GetAdjustedLocalSymbol(sourceLocalSymbol) : localSymbol).GetPublicSymbol();
		}

		// Token: 0x06001533 RID: 5427
		internal abstract Microsoft.CodeAnalysis.CSharp.Symbols.LocalSymbol GetAdjustedLocalSymbol(SourceLocalSymbol originalSymbol);

		// Token: 0x06001534 RID: 5428 RVA: 0x000720EC File Offset: 0x000702EC
		public ILocalSymbol GetDeclaredSymbol(CatchDeclarationSyntax catchDeclaration, CancellationToken cancellationToken = default(CancellationToken))
		{
			CSharpSyntaxNode parent = catchDeclaration.Parent;
			Binder enclosingBinder = this.GetEnclosingBinder(this.GetAdjustedNodePosition(parent));
			if (enclosingBinder == null)
			{
				return null;
			}
			if (enclosingBinder.GetBinder(parent) == null)
			{
				return null;
			}
			Microsoft.CodeAnalysis.CSharp.Symbols.LocalSymbol localSymbol = enclosingBinder.GetBinder(parent).GetDeclaredLocalsForScope(parent).FirstOrDefault<Microsoft.CodeAnalysis.CSharp.Symbols.LocalSymbol>();
			if (localSymbol == null || localSymbol.DeclarationKind != LocalDeclarationKind.CatchVariable)
			{
				return null;
			}
			return localSymbol.GetPublicSymbol();
		}

		// Token: 0x06001535 RID: 5429
		public abstract IRangeVariableSymbol GetDeclaredSymbol(QueryClauseSyntax queryClause, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x06001536 RID: 5430
		public abstract IRangeVariableSymbol GetDeclaredSymbol(JoinIntoClauseSyntax node, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x06001537 RID: 5431
		public abstract IRangeVariableSymbol GetDeclaredSymbol(QueryContinuationSyntax node, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x06001538 RID: 5432 RVA: 0x00072148 File Offset: 0x00070348
		private ImmutableArray<Symbol> GetSemanticSymbols(BoundExpression boundNode, BoundNode boundNodeForSyntacticParent, Binder binderOpt, CSharpSemanticModel.SymbolInfoOptions options, out bool isDynamic, out LookupResultKind resultKind, out ImmutableArray<Symbol> memberGroup)
		{
			memberGroup = ImmutableArray<Symbol>.Empty;
			ImmutableArray<Symbol> immutableArray = ImmutableArray<Symbol>.Empty;
			resultKind = LookupResultKind.Viable;
			isDynamic = false;
			BoundKind kind = boundNode.Kind;
			if (kind <= BoundKind.ThisReference)
			{
				if (kind <= BoundKind.ConditionalOperator)
				{
					switch (kind)
					{
					case BoundKind.BadExpression:
					{
						BoundBadExpression boundBadExpression = (BoundBadExpression)boundNode;
						resultKind = boundBadExpression.ResultKind;
						SyntaxKind syntaxKind = boundBadExpression.Syntax.Kind();
						if (syntaxKind == SyntaxKind.ObjectCreationExpression || syntaxKind == SyntaxKind.ImplicitObjectCreationExpression)
						{
							if (resultKind == LookupResultKind.NotCreatable)
							{
								return boundBadExpression.Symbols;
							}
							if (boundBadExpression.Type.IsDelegateType())
							{
								resultKind = LookupResultKind.Empty;
								return immutableArray;
							}
							memberGroup = boundBadExpression.Symbols;
						}
						return boundBadExpression.Symbols;
					}
					case BoundKind.BadStatement:
					case BoundKind.ExtractedFinallyBlock:
					case BoundKind.NamespaceExpression:
					case BoundKind.AddressOfOperator:
					case BoundKind.FunctionPointerLoad:
					case BoundKind.PointerIndirectionOperator:
					case BoundKind.PointerElementAccess:
					case BoundKind.RefTypeOperator:
					case BoundKind.MakeRefOperator:
					case BoundKind.RefValueOperator:
					case BoundKind.TupleBinaryOperator:
						goto IL_0885;
					case BoundKind.TypeExpression:
					{
						BoundTypeExpression boundTypeExpression = (BoundTypeExpression)boundNode;
						if (boundNodeForSyntacticParent != null && boundNodeForSyntacticParent.Syntax.Kind() == SyntaxKind.ObjectCreationExpression && ((ObjectCreationExpressionSyntax)boundNodeForSyntacticParent.Syntax).Type == boundTypeExpression.Syntax && boundNodeForSyntacticParent.Kind == BoundKind.BadExpression && ((BoundBadExpression)boundNodeForSyntacticParent).ResultKind == LookupResultKind.NotCreatable)
						{
							resultKind = LookupResultKind.NotCreatable;
						}
						Symbol symbol = boundTypeExpression.AliasOpt ?? boundTypeExpression.Type;
						Microsoft.CodeAnalysis.CSharp.Symbols.ErrorTypeSymbol errorTypeSymbol = symbol.OriginalDefinition as Microsoft.CodeAnalysis.CSharp.Symbols.ErrorTypeSymbol;
						if (errorTypeSymbol != null)
						{
							resultKind = errorTypeSymbol.ResultKind;
							immutableArray = errorTypeSymbol.CandidateSymbols;
							goto IL_08A2;
						}
						immutableArray = ImmutableArray.Create<Symbol>(symbol);
						goto IL_08A2;
					}
					case BoundKind.TypeOrValueExpression:
					{
						BoundExpression valueExpression = ((BoundTypeOrValueExpression)boundNode).Data.ValueExpression;
						return this.GetSemanticSymbols(valueExpression, boundNodeForSyntacticParent, binderOpt, options, out isDynamic, out resultKind, out memberGroup);
					}
					case BoundKind.UnaryOperator:
						CSharpSemanticModel.GetSymbolsAndResultKind((BoundUnaryOperator)boundNode, out isDynamic, ref resultKind, ref immutableArray);
						goto IL_08A2;
					case BoundKind.IncrementOperator:
						CSharpSemanticModel.GetSymbolsAndResultKind((BoundIncrementOperator)boundNode, out isDynamic, ref resultKind, ref immutableArray);
						goto IL_08A2;
					case BoundKind.UnconvertedAddressOfOperator:
					{
						ImmutableArray<Symbol> immutableArray2;
						immutableArray = this.GetMethodGroupSemanticSymbols(((BoundUnconvertedAddressOfOperator)boundNode).Operand, boundNodeForSyntacticParent, binderOpt, out resultKind, out isDynamic, out immutableArray2);
						goto IL_08A2;
					}
					case BoundKind.FunctionPointerInvocation:
					{
						BoundFunctionPointerInvocation boundFunctionPointerInvocation = (BoundFunctionPointerInvocation)boundNode;
						immutableArray = ImmutableArray.Create<Symbol>(boundFunctionPointerInvocation.FunctionPointer);
						resultKind = boundFunctionPointerInvocation.ResultKind;
						goto IL_08A2;
					}
					case BoundKind.FromEndIndexExpression:
					{
						BoundFromEndIndexExpression boundFromEndIndexExpression = (BoundFromEndIndexExpression)boundNode;
						if (boundFromEndIndexExpression.MethodOpt != null)
						{
							immutableArray = ImmutableArray.Create<Symbol>(boundFromEndIndexExpression.MethodOpt);
							goto IL_08A2;
						}
						goto IL_08A2;
					}
					case BoundKind.RangeExpression:
					{
						BoundRangeExpression boundRangeExpression = (BoundRangeExpression)boundNode;
						if (boundRangeExpression.MethodOpt != null)
						{
							immutableArray = ImmutableArray.Create<Symbol>(boundRangeExpression.MethodOpt);
							goto IL_08A2;
						}
						goto IL_08A2;
					}
					case BoundKind.BinaryOperator:
						CSharpSemanticModel.GetSymbolsAndResultKind((BoundBinaryOperator)boundNode, out isDynamic, ref resultKind, ref immutableArray);
						goto IL_08A2;
					case BoundKind.UserDefinedConditionalLogicalOperator:
					{
						BoundUserDefinedConditionalLogicalOperator boundUserDefinedConditionalLogicalOperator = (BoundUserDefinedConditionalLogicalOperator)boundNode;
						isDynamic = false;
						CSharpSemanticModel.GetSymbolsAndResultKind(boundUserDefinedConditionalLogicalOperator, boundUserDefinedConditionalLogicalOperator.LogicalOperator, boundUserDefinedConditionalLogicalOperator.OriginalUserDefinedOperatorsOpt, out immutableArray, out resultKind);
						goto IL_08A2;
					}
					case BoundKind.CompoundAssignmentOperator:
						CSharpSemanticModel.GetSymbolsAndResultKind((BoundCompoundAssignmentOperator)boundNode, out isDynamic, ref resultKind, ref immutableArray);
						goto IL_08A2;
					default:
					{
						if (kind != BoundKind.ConditionalOperator)
						{
							goto IL_0885;
						}
						BoundConditionalOperator boundConditionalOperator = (BoundConditionalOperator)boundNode;
						isDynamic = boundConditionalOperator.IsDynamic;
						goto IL_0885;
					}
					}
				}
				else
				{
					if (kind == BoundKind.AwaitExpression)
					{
						BoundAwaitExpression boundAwaitExpression = (BoundAwaitExpression)boundNode;
						isDynamic = boundAwaitExpression.AwaitableInfo.IsDynamic;
						goto IL_0885;
					}
					if (kind != BoundKind.Conversion)
					{
						if (kind != BoundKind.ThisReference)
						{
							goto IL_0885;
						}
					}
					else
					{
						BoundConversion boundConversion = (BoundConversion)boundNode;
						isDynamic = boundConversion.ConversionKind.IsDynamic();
						if (isDynamic)
						{
							goto IL_08A2;
						}
						if (boundConversion.ConversionKind == ConversionKind.MethodGroup && boundConversion.IsExtensionMethod)
						{
							immutableArray = ImmutableArray.Create<Symbol>(ReducedExtensionMethodSymbol.Create(boundConversion.SymbolOpt));
							resultKind = boundConversion.ResultKind;
							goto IL_08A2;
						}
						if (boundConversion.ConversionKind.IsUserDefinedConversion())
						{
							CSharpSemanticModel.GetSymbolsAndResultKind(boundConversion, boundConversion.SymbolOpt, boundConversion.OriginalUserDefinedConversionsOpt, out immutableArray, out resultKind);
							goto IL_08A2;
						}
						goto IL_0885;
					}
				}
			}
			else if (kind <= BoundKind.DynamicObjectCreationExpression)
			{
				if (kind != BoundKind.BaseReference)
				{
					switch (kind)
					{
					case BoundKind.DynamicMemberAccess:
						isDynamic = true;
						goto IL_08A2;
					case BoundKind.DynamicInvocation:
					{
						BoundDynamicInvocation boundDynamicInvocation = (BoundDynamicInvocation)boundNode;
						ImmutableArray<Symbol> immutableArray2 = (memberGroup = boundDynamicInvocation.ApplicableMethods.Cast<Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol, Symbol>());
						immutableArray = immutableArray2;
						isDynamic = true;
						goto IL_08A2;
					}
					case BoundKind.ConditionalAccess:
					case BoundKind.LoweredConditionalAccess:
					case BoundKind.ConditionalReceiver:
					case BoundKind.ComplexConditionalReceiver:
					case BoundKind.UnconvertedObjectCreationExpression:
					case BoundKind.TupleLiteral:
					case BoundKind.ConvertedTupleLiteral:
						goto IL_0885;
					case BoundKind.MethodGroup:
						immutableArray = this.GetMethodGroupSemanticSymbols((BoundMethodGroup)boundNode, boundNodeForSyntacticParent, binderOpt, out resultKind, out isDynamic, out memberGroup);
						goto IL_08A2;
					case BoundKind.PropertyGroup:
						immutableArray = this.GetPropertyGroupSemanticSymbols((BoundPropertyGroup)boundNode, boundNodeForSyntacticParent, binderOpt, out resultKind, out memberGroup);
						goto IL_08A2;
					case BoundKind.Call:
					{
						BoundCall boundCall = (BoundCall)boundNode;
						if (!boundCall.OriginalMethodsOpt.IsDefault)
						{
							immutableArray = StaticCast<Symbol>.From<Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol>(CSharpSemanticModel.CreateReducedExtensionMethodsFromOriginalsIfNecessary(boundCall, this.Compilation));
							resultKind = boundCall.ResultKind;
							goto IL_08A2;
						}
						if (boundCall.Method != null)
						{
							immutableArray = this.CreateReducedExtensionMethodIfPossible(boundCall);
							resultKind = boundCall.ResultKind;
							goto IL_08A2;
						}
						goto IL_08A2;
					}
					case BoundKind.EventAssignmentOperator:
					{
						BoundEventAssignmentOperator boundEventAssignmentOperator = (BoundEventAssignmentOperator)boundNode;
						isDynamic = boundEventAssignmentOperator.IsDynamic;
						Microsoft.CodeAnalysis.CSharp.Symbols.EventSymbol @event = boundEventAssignmentOperator.Event;
						Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol methodSymbol = (boundEventAssignmentOperator.IsAddition ? @event.AddMethod : @event.RemoveMethod);
						if (methodSymbol == null)
						{
							immutableArray = ImmutableArray<Symbol>.Empty;
							resultKind = LookupResultKind.Empty;
							goto IL_08A2;
						}
						immutableArray = ImmutableArray.Create<Symbol>(methodSymbol);
						resultKind = boundEventAssignmentOperator.ResultKind;
						goto IL_08A2;
					}
					case BoundKind.Attribute:
					{
						BoundAttribute boundAttribute = (BoundAttribute)boundNode;
						resultKind = boundAttribute.ResultKind;
						Microsoft.CodeAnalysis.CSharp.Symbols.NamedTypeSymbol namedTypeSymbol = (Microsoft.CodeAnalysis.CSharp.Symbols.NamedTypeSymbol)boundAttribute.Type;
						if (namedTypeSymbol.IsErrorType())
						{
							ImmutableArray<Symbol> candidateSymbols = ((Microsoft.CodeAnalysis.CSharp.Symbols.ErrorTypeSymbol)namedTypeSymbol).CandidateSymbols;
							if (candidateSymbols.Length != 1 || !(candidateSymbols[0] is Microsoft.CodeAnalysis.CSharp.Symbols.NamedTypeSymbol))
							{
								immutableArray = candidateSymbols;
								goto IL_08A2;
							}
							namedTypeSymbol = (Microsoft.CodeAnalysis.CSharp.Symbols.NamedTypeSymbol)candidateSymbols[0];
						}
						this.AdjustSymbolsForObjectCreation(boundAttribute, namedTypeSymbol, boundAttribute.Constructor, binderOpt, ref resultKind, ref immutableArray, ref memberGroup);
						goto IL_08A2;
					}
					case BoundKind.ObjectCreationExpression:
					{
						BoundObjectCreationExpression boundObjectCreationExpression = (BoundObjectCreationExpression)boundNode;
						if (boundObjectCreationExpression.Constructor != null)
						{
							immutableArray = ImmutableArray.Create<Symbol>(boundObjectCreationExpression.Constructor);
						}
						else if (boundObjectCreationExpression.ConstructorsGroup.Length > 0)
						{
							immutableArray = StaticCast<Symbol>.From<Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol>(boundObjectCreationExpression.ConstructorsGroup);
							resultKind = resultKind.WorseResultKind(LookupResultKind.OverloadResolutionFailure);
						}
						memberGroup = boundObjectCreationExpression.ConstructorsGroup.Cast<Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol, Symbol>();
						goto IL_08A2;
					}
					case BoundKind.DynamicObjectCreationExpression:
					{
						BoundDynamicObjectCreationExpression boundDynamicObjectCreationExpression = (BoundDynamicObjectCreationExpression)boundNode;
						ImmutableArray<Symbol> immutableArray2 = (memberGroup = boundDynamicObjectCreationExpression.ApplicableMethods.Cast<Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol, Symbol>());
						immutableArray = immutableArray2;
						isDynamic = true;
						goto IL_08A2;
					}
					default:
						goto IL_0885;
					}
				}
			}
			else
			{
				if (kind == BoundKind.DynamicCollectionElementInitializer)
				{
					BoundDynamicCollectionElementInitializer boundDynamicCollectionElementInitializer = (BoundDynamicCollectionElementInitializer)boundNode;
					ImmutableArray<Symbol> immutableArray2 = (memberGroup = boundDynamicCollectionElementInitializer.ApplicableMethods.Cast<Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol, Symbol>());
					immutableArray = immutableArray2;
					isDynamic = true;
					goto IL_08A2;
				}
				if (kind == BoundKind.DelegateCreationExpression)
				{
					goto IL_08A2;
				}
				switch (kind)
				{
				case BoundKind.EventAccess:
				{
					BoundEventAssignmentOperator boundEventAssignmentOperator2 = boundNodeForSyntacticParent as BoundEventAssignmentOperator;
					if (boundEventAssignmentOperator2 == null || boundEventAssignmentOperator2.ResultKind != LookupResultKind.Viable)
					{
						goto IL_0885;
					}
					Symbol expressionSymbol = boundNode.ExpressionSymbol;
					if (expressionSymbol != null && boundNode != boundEventAssignmentOperator2.Argument && boundEventAssignmentOperator2.Event.Equals(expressionSymbol, TypeCompareKind.AllNullableIgnoreOptions))
					{
						immutableArray = ImmutableArray.Create<Symbol>(boundEventAssignmentOperator2.Event);
						resultKind = boundEventAssignmentOperator2.ResultKind;
						goto IL_08A2;
					}
					goto IL_0885;
				}
				case BoundKind.IndexerAccess:
				{
					BoundIndexerAccess boundIndexerAccess = (BoundIndexerAccess)boundNode;
					resultKind = boundIndexerAccess.ResultKind;
					ImmutableArray<Microsoft.CodeAnalysis.CSharp.Symbols.PropertySymbol> originalIndexersOpt = boundIndexerAccess.OriginalIndexersOpt;
					immutableArray = (originalIndexersOpt.IsDefault ? ImmutableArray.Create<Symbol>(boundIndexerAccess.Indexer) : StaticCast<Symbol>.From<Microsoft.CodeAnalysis.CSharp.Symbols.PropertySymbol>(originalIndexersOpt));
					goto IL_08A2;
				}
				case BoundKind.IndexOrRangePatternIndexerAccess:
				{
					BoundIndexOrRangePatternIndexerAccess boundIndexOrRangePatternIndexerAccess = (BoundIndexOrRangePatternIndexerAccess)boundNode;
					resultKind = boundIndexOrRangePatternIndexerAccess.ResultKind;
					immutableArray = ImmutableArray.Create<Symbol>(boundIndexOrRangePatternIndexerAccess.PatternSymbol);
					goto IL_08A2;
				}
				case BoundKind.DynamicIndexerAccess:
				{
					BoundDynamicIndexerAccess boundDynamicIndexerAccess = (BoundDynamicIndexerAccess)boundNode;
					ImmutableArray<Symbol> immutableArray2 = (memberGroup = boundDynamicIndexerAccess.ApplicableIndexers.Cast<Microsoft.CodeAnalysis.CSharp.Symbols.PropertySymbol, Symbol>());
					immutableArray = immutableArray2;
					isDynamic = true;
					goto IL_08A2;
				}
				case BoundKind.Lambda:
				case BoundKind.UnboundLambda:
					goto IL_0885;
				case BoundKind.QueryClause:
				{
					BoundQueryClause boundQueryClause = (BoundQueryClause)boundNode;
					ArrayBuilder<Symbol> instance = ArrayBuilder<Symbol>.GetInstance();
					if (boundQueryClause.Operation != null && boundQueryClause.Operation.ExpressionSymbol != null)
					{
						instance.Add(boundQueryClause.Operation.ExpressionSymbol);
					}
					if (boundQueryClause.DefinedSymbol != null)
					{
						instance.Add(boundQueryClause.DefinedSymbol);
					}
					if (boundQueryClause.Cast != null && boundQueryClause.Cast.ExpressionSymbol != null)
					{
						instance.Add(boundQueryClause.Cast.ExpressionSymbol);
					}
					immutableArray = instance.ToImmutableAndFree();
					goto IL_08A2;
				}
				default:
					goto IL_0885;
				}
			}
			Binder binder = binderOpt ?? this.GetEnclosingBinder(this.GetAdjustedNodePosition(boundNode.Syntax));
			Microsoft.CodeAnalysis.CSharp.Symbols.NamedTypeSymbol containingType = binder.ContainingType;
			Symbol symbol2 = binder.ContainingMember();
			Microsoft.CodeAnalysis.CSharp.Symbols.ParameterSymbol thisParameter = CSharpSemanticModel.GetThisParameter(boundNode.Type, containingType, symbol2, out resultKind);
			immutableArray = ((thisParameter != null) ? ImmutableArray.Create<Symbol>(thisParameter) : ImmutableArray<Symbol>.Empty);
			goto IL_08A2;
			IL_0885:
			Symbol expressionSymbol2 = boundNode.ExpressionSymbol;
			if (expressionSymbol2 != null)
			{
				immutableArray = ImmutableArray.Create<Symbol>(expressionSymbol2);
				resultKind = boundNode.ResultKind;
			}
			IL_08A2:
			if (boundNodeForSyntacticParent != null && (options & CSharpSemanticModel.SymbolInfoOptions.PreferConstructorsToType) != (CSharpSemanticModel.SymbolInfoOptions)0)
			{
				this.AdjustSymbolsForObjectCreation(boundNode, boundNodeForSyntacticParent, binderOpt, ref resultKind, ref immutableArray, ref memberGroup);
			}
			return immutableArray;
		}

		// Token: 0x06001539 RID: 5433 RVA: 0x00072A10 File Offset: 0x00070C10
		private static Microsoft.CodeAnalysis.CSharp.Symbols.ParameterSymbol GetThisParameter(Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol typeOfThis, Microsoft.CodeAnalysis.CSharp.Symbols.NamedTypeSymbol containingType, Symbol containingMember, out LookupResultKind resultKind)
		{
			if (containingMember == null || containingType == null)
			{
				resultKind = LookupResultKind.NotReferencable;
				return new ThisParameterSymbol(containingMember as Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol, typeOfThis);
			}
			SymbolKind kind = containingMember.Kind;
			Microsoft.CodeAnalysis.CSharp.Symbols.ParameterSymbol parameterSymbol;
			if (kind == SymbolKind.Field || kind == SymbolKind.Method || kind == SymbolKind.Property)
			{
				if (containingMember.IsStatic)
				{
					resultKind = LookupResultKind.StaticInstanceMismatch;
					parameterSymbol = new ThisParameterSymbol(containingMember as Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol, containingType);
				}
				else if (typeOfThis == Microsoft.CodeAnalysis.CSharp.Symbols.ErrorTypeSymbol.UnknownResultType)
				{
					parameterSymbol = new ThisParameterSymbol(containingMember as Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol, containingType);
					resultKind = LookupResultKind.NotReferencable;
				}
				else
				{
					SymbolKind kind2 = containingMember.Kind;
					if (kind2 != SymbolKind.Field)
					{
						if (kind2 == SymbolKind.Method)
						{
							resultKind = LookupResultKind.Viable;
							return containingMember.EnclosingThisSymbol();
						}
						if (kind2 != SymbolKind.Property)
						{
							throw ExceptionUtilities.UnexpectedValue(containingMember.Kind);
						}
					}
					resultKind = LookupResultKind.NotReferencable;
					parameterSymbol = containingMember.EnclosingThisSymbol() ?? new ThisParameterSymbol(null, containingType);
				}
			}
			else
			{
				parameterSymbol = new ThisParameterSymbol(containingMember as Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol, typeOfThis);
				resultKind = LookupResultKind.NotReferencable;
			}
			return parameterSymbol;
		}

		// Token: 0x0600153A RID: 5434 RVA: 0x00072AE0 File Offset: 0x00070CE0
		private static void GetSymbolsAndResultKind(BoundUnaryOperator unaryOperator, out bool isDynamic, ref LookupResultKind resultKind, ref ImmutableArray<Symbol> symbols)
		{
			UnaryOperatorKind unaryOperatorKind = unaryOperator.OperatorKind.OperandTypes();
			isDynamic = unaryOperator.OperatorKind.IsDynamic();
			if (unaryOperatorKind == UnaryOperatorKind.Error || unaryOperatorKind == UnaryOperatorKind.UserDefined || unaryOperator.ResultKind != LookupResultKind.Viable)
			{
				if (!isDynamic)
				{
					CSharpSemanticModel.GetSymbolsAndResultKind(unaryOperator, unaryOperator.MethodOpt, unaryOperator.OriginalUserDefinedOperatorsOpt, out symbols, out resultKind);
					return;
				}
			}
			else
			{
				UnaryOperatorKind unaryOperatorKind2 = unaryOperator.OperatorKind.Operator();
				symbols = ImmutableArray.Create<Symbol>(new SynthesizedIntrinsicOperatorSymbol(unaryOperator.Operand.Type.StrippedType(), OperatorFacts.UnaryOperatorNameFromOperatorKind(unaryOperatorKind2), unaryOperator.Type.StrippedType(), unaryOperator.OperatorKind.IsChecked()));
				resultKind = unaryOperator.ResultKind;
			}
		}

		// Token: 0x0600153B RID: 5435 RVA: 0x00072B84 File Offset: 0x00070D84
		private static void GetSymbolsAndResultKind(BoundIncrementOperator increment, out bool isDynamic, ref LookupResultKind resultKind, ref ImmutableArray<Symbol> symbols)
		{
			UnaryOperatorKind unaryOperatorKind = increment.OperatorKind.OperandTypes();
			isDynamic = increment.OperatorKind.IsDynamic();
			if (unaryOperatorKind == UnaryOperatorKind.Error || unaryOperatorKind == UnaryOperatorKind.UserDefined || increment.ResultKind != LookupResultKind.Viable)
			{
				if (!isDynamic)
				{
					CSharpSemanticModel.GetSymbolsAndResultKind(increment, increment.MethodOpt, increment.OriginalUserDefinedOperatorsOpt, out symbols, out resultKind);
					return;
				}
			}
			else
			{
				UnaryOperatorKind unaryOperatorKind2 = increment.OperatorKind.Operator();
				symbols = ImmutableArray.Create<Symbol>(new SynthesizedIntrinsicOperatorSymbol(increment.Operand.Type.StrippedType(), OperatorFacts.UnaryOperatorNameFromOperatorKind(unaryOperatorKind2), increment.Type.StrippedType(), increment.OperatorKind.IsChecked()));
				resultKind = increment.ResultKind;
			}
		}

		// Token: 0x0600153C RID: 5436 RVA: 0x00072C28 File Offset: 0x00070E28
		private static void GetSymbolsAndResultKind(BoundBinaryOperator binaryOperator, out bool isDynamic, ref LookupResultKind resultKind, ref ImmutableArray<Symbol> symbols)
		{
			BinaryOperatorKind binaryOperatorKind = binaryOperator.OperatorKind.OperandTypes();
			BinaryOperatorKind binaryOperatorKind2 = binaryOperator.OperatorKind.Operator();
			isDynamic = binaryOperator.OperatorKind.IsDynamic();
			if (binaryOperatorKind == BinaryOperatorKind.Error || binaryOperatorKind == BinaryOperatorKind.UserDefined || binaryOperator.ResultKind != LookupResultKind.Viable || binaryOperator.OperatorKind.IsLogical())
			{
				if (!isDynamic)
				{
					CSharpSemanticModel.GetSymbolsAndResultKind(binaryOperator, binaryOperator.MethodOpt, binaryOperator.OriginalUserDefinedOperatorsOpt, out symbols, out resultKind);
					return;
				}
			}
			else
			{
				if (!isDynamic && (binaryOperatorKind2 == BinaryOperatorKind.Equal || binaryOperatorKind2 == BinaryOperatorKind.NotEqual) && ((binaryOperator.Left.IsLiteralNull() && binaryOperator.Right.Type.IsNullableType()) || (binaryOperator.Right.IsLiteralNull() && binaryOperator.Left.Type.IsNullableType())) && binaryOperator.Type.SpecialType == SpecialType.System_Boolean)
				{
					Microsoft.CodeAnalysis.CSharp.Symbols.NamedTypeSymbol specialType = binaryOperator.Type.ContainingAssembly.GetSpecialType(SpecialType.System_Object);
					symbols = ImmutableArray.Create<Symbol>(new SynthesizedIntrinsicOperatorSymbol(specialType, OperatorFacts.BinaryOperatorNameFromOperatorKind(binaryOperatorKind2), specialType, binaryOperator.Type, binaryOperator.OperatorKind.IsChecked()));
				}
				else
				{
					symbols = ImmutableArray.Create<Symbol>(CSharpSemanticModel.GetIntrinsicOperatorSymbol(binaryOperatorKind2, isDynamic, binaryOperator.Left.Type, binaryOperator.Right.Type, binaryOperator.Type, binaryOperator.OperatorKind.IsChecked()));
				}
				resultKind = binaryOperator.ResultKind;
			}
		}

		// Token: 0x0600153D RID: 5437 RVA: 0x00072D7A File Offset: 0x00070F7A
		private static Symbol GetIntrinsicOperatorSymbol(BinaryOperatorKind op, bool isDynamic, Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol leftType, Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol rightType, Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol returnType, bool isChecked)
		{
			if (!isDynamic)
			{
				leftType = leftType.StrippedType();
				rightType = rightType.StrippedType();
				returnType = returnType.StrippedType();
			}
			else if (leftType == null)
			{
				leftType = rightType;
			}
			else if (rightType == null)
			{
				rightType = leftType;
			}
			return new SynthesizedIntrinsicOperatorSymbol(leftType, OperatorFacts.BinaryOperatorNameFromOperatorKind(op), rightType, returnType, isChecked);
		}

		// Token: 0x0600153E RID: 5438 RVA: 0x00072DBC File Offset: 0x00070FBC
		private static void GetSymbolsAndResultKind(BoundCompoundAssignmentOperator compoundAssignment, out bool isDynamic, ref LookupResultKind resultKind, ref ImmutableArray<Symbol> symbols)
		{
			BinaryOperatorKind binaryOperatorKind = compoundAssignment.Operator.Kind.OperandTypes();
			BinaryOperatorKind binaryOperatorKind2 = compoundAssignment.Operator.Kind.Operator();
			isDynamic = compoundAssignment.Operator.Kind.IsDynamic();
			if (binaryOperatorKind == BinaryOperatorKind.Error || binaryOperatorKind == BinaryOperatorKind.UserDefined || compoundAssignment.ResultKind != LookupResultKind.Viable)
			{
				if (!isDynamic)
				{
					CSharpSemanticModel.GetSymbolsAndResultKind(compoundAssignment, compoundAssignment.Operator.Method, compoundAssignment.OriginalUserDefinedOperatorsOpt, out symbols, out resultKind);
					return;
				}
			}
			else
			{
				symbols = ImmutableArray.Create<Symbol>(CSharpSemanticModel.GetIntrinsicOperatorSymbol(binaryOperatorKind2, isDynamic, compoundAssignment.Operator.LeftType, compoundAssignment.Operator.RightType, compoundAssignment.Operator.ReturnType, compoundAssignment.Operator.Kind.IsChecked()));
				resultKind = compoundAssignment.ResultKind;
			}
		}

		// Token: 0x0600153F RID: 5439 RVA: 0x00072E7C File Offset: 0x0007107C
		private static void GetSymbolsAndResultKind(BoundExpression node, Symbol symbolOpt, ImmutableArray<Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol> originalCandidates, out ImmutableArray<Symbol> symbols, out LookupResultKind resultKind)
		{
			if (symbolOpt != null)
			{
				symbols = ImmutableArray.Create<Symbol>(symbolOpt);
				resultKind = node.ResultKind;
				return;
			}
			if (!originalCandidates.IsDefault)
			{
				symbols = StaticCast<Symbol>.From<Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol>(originalCandidates);
				resultKind = node.ResultKind;
				return;
			}
			symbols = ImmutableArray<Symbol>.Empty;
			resultKind = LookupResultKind.Empty;
		}

		// Token: 0x06001540 RID: 5440 RVA: 0x00072ED0 File Offset: 0x000710D0
		private void AdjustSymbolsForObjectCreation(BoundExpression boundNode, BoundNode boundNodeForSyntacticParent, Binder binderOpt, ref LookupResultKind resultKind, ref ImmutableArray<Symbol> symbols, ref ImmutableArray<Symbol> memberGroup)
		{
			Microsoft.CodeAnalysis.CSharp.Symbols.NamedTypeSymbol namedTypeSymbol = null;
			Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol methodSymbol = null;
			SyntaxNode syntax = boundNodeForSyntacticParent.Syntax;
			if (syntax != null && syntax == boundNode.Syntax.Parent && syntax.Kind() == SyntaxKind.Attribute && ((AttributeSyntax)syntax).Name == boundNode.Syntax)
			{
				ImmutableArray<Symbol> immutableArray = CSharpSemanticModel.UnwrapAliases(symbols);
				BoundKind kind = boundNodeForSyntacticParent.Kind;
				if (kind != BoundKind.BadExpression)
				{
					if (kind != BoundKind.Attribute)
					{
						throw ExceptionUtilities.UnexpectedValue(boundNodeForSyntacticParent.Kind);
					}
					BoundAttribute boundAttribute = (BoundAttribute)boundNodeForSyntacticParent;
					if (immutableArray.Length == 1 && immutableArray[0].Kind == SymbolKind.NamedType)
					{
						namedTypeSymbol = (Microsoft.CodeAnalysis.CSharp.Symbols.NamedTypeSymbol)immutableArray[0];
						methodSymbol = boundAttribute.Constructor;
						resultKind = resultKind.WorseResultKind(boundAttribute.ResultKind);
					}
				}
				else
				{
					BoundBadExpression boundBadExpression = (BoundBadExpression)boundNodeForSyntacticParent;
					if (immutableArray.Length == 1)
					{
						resultKind = resultKind.WorseResultKind(boundBadExpression.ResultKind);
						namedTypeSymbol = immutableArray[0] as Microsoft.CodeAnalysis.CSharp.Symbols.NamedTypeSymbol;
					}
				}
				this.AdjustSymbolsForObjectCreation(boundNode, namedTypeSymbol, methodSymbol, binderOpt, ref resultKind, ref symbols, ref memberGroup);
			}
		}

		// Token: 0x06001541 RID: 5441 RVA: 0x00072FF4 File Offset: 0x000711F4
		private void AdjustSymbolsForObjectCreation(BoundNode lowestBoundNode, Microsoft.CodeAnalysis.CSharp.Symbols.NamedTypeSymbol typeSymbolOpt, Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol constructorOpt, Binder binderOpt, ref LookupResultKind resultKind, ref ImmutableArray<Symbol> symbols, ref ImmutableArray<Symbol> memberGroup)
		{
			if (typeSymbolOpt != null)
			{
				Binder binder = binderOpt ?? this.GetEnclosingBinder(this.GetAdjustedNodePosition(lowestBoundNode.Syntax));
				ImmutableArray<Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol> immutableArray2;
				if (binder != null)
				{
					ImmutableArray<Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol> immutableArray = ((typeSymbolOpt.IsInterfaceType() && typeSymbolOpt.ComImportCoClass != null) ? typeSymbolOpt.ComImportCoClass.InstanceConstructors : typeSymbolOpt.InstanceConstructors);
					CompoundUseSiteInfo<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol> discarded = CompoundUseSiteInfo<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>.Discarded;
					immutableArray2 = binder.FilterInaccessibleConstructors(immutableArray, false, ref discarded);
					if ((constructorOpt == null) ? (!immutableArray2.Any<Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol>()) : (!immutableArray2.Contains(constructorOpt)))
					{
						immutableArray2 = immutableArray;
					}
				}
				else
				{
					immutableArray2 = ImmutableArray<Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol>.Empty;
				}
				if (constructorOpt != null)
				{
					symbols = ImmutableArray.Create<Symbol>(constructorOpt);
				}
				else if (immutableArray2.Length > 0)
				{
					symbols = StaticCast<Symbol>.From<Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol>(immutableArray2);
					resultKind = resultKind.WorseResultKind(LookupResultKind.OverloadResolutionFailure);
				}
				memberGroup = immutableArray2.Cast<Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol, Symbol>();
			}
		}

		// Token: 0x06001542 RID: 5442 RVA: 0x000730C0 File Offset: 0x000712C0
		private ImmutableArray<IPropertySymbol> GetIndexerGroupSemanticSymbols(BoundExpression boundNode, Binder binderOpt)
		{
			Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol type = boundNode.Type;
			if (type == null || type.IsStatic)
			{
				return ImmutableArray<IPropertySymbol>.Empty;
			}
			Binder binder = binderOpt ?? this.GetEnclosingBinder(this.GetAdjustedNodePosition(boundNode.Syntax));
			ArrayBuilder<ISymbol> instance = ArrayBuilder<ISymbol>.GetInstance();
			this.AppendSymbolsWithNameAndArity(instance, "this[]", 0, binder, type, LookupOptions.MustBeInstance);
			if (instance.Count == 0)
			{
				instance.Free();
				return ImmutableArray<IPropertySymbol>.Empty;
			}
			return CSharpSemanticModel.FilterOverriddenOrHiddenIndexers(instance.ToImmutableAndFree());
		}

		// Token: 0x06001543 RID: 5443 RVA: 0x00073134 File Offset: 0x00071334
		private static ImmutableArray<IPropertySymbol> FilterOverriddenOrHiddenIndexers(ImmutableArray<ISymbol> symbols)
		{
			PooledHashSet<Symbol> pooledHashSet = null;
			foreach (ISymbol symbol in symbols)
			{
				OverriddenOrHiddenMembersResult overriddenOrHiddenMembers = ((Microsoft.CodeAnalysis.CSharp.Symbols.PropertySymbol)symbol.GetSymbol()).OverriddenOrHiddenMembers;
				foreach (Symbol symbol2 in overriddenOrHiddenMembers.OverriddenMembers)
				{
					if (pooledHashSet == null)
					{
						pooledHashSet = PooledHashSet<Symbol>.GetInstance();
					}
					pooledHashSet.Add(symbol2);
				}
				foreach (Symbol symbol3 in overriddenOrHiddenMembers.HiddenMembers)
				{
					if (pooledHashSet == null)
					{
						pooledHashSet = PooledHashSet<Symbol>.GetInstance();
					}
					pooledHashSet.Add(symbol3);
				}
			}
			ArrayBuilder<IPropertySymbol> instance = ArrayBuilder<IPropertySymbol>.GetInstance();
			foreach (ISymbol symbol4 in symbols)
			{
				IPropertySymbol propertySymbol = (IPropertySymbol)symbol4;
				if (pooledHashSet == null || !pooledHashSet.Contains(propertySymbol.GetSymbol()))
				{
					instance.Add(propertySymbol);
				}
			}
			if (pooledHashSet != null)
			{
				pooledHashSet.Free();
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x06001544 RID: 5444 RVA: 0x00073234 File Offset: 0x00071434
		private static ImmutableArray<Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol> FilterOverriddenOrHiddenMethods(ImmutableArray<Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol> methods)
		{
			if (methods.Length <= 1)
			{
				return methods;
			}
			HashSet<Symbol> hashSet = new HashSet<Symbol>();
			foreach (Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol methodSymbol in methods)
			{
				OverriddenOrHiddenMembersResult overriddenOrHiddenMembers = methodSymbol.OverriddenOrHiddenMembers;
				foreach (Symbol symbol in overriddenOrHiddenMembers.OverriddenMembers)
				{
					hashSet.Add(symbol);
				}
				foreach (Symbol symbol2 in overriddenOrHiddenMembers.HiddenMembers)
				{
					hashSet.Add(symbol2);
				}
			}
			return methods.WhereAsArray((Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol m, HashSet<Symbol> hiddenSymbols) => !hiddenSymbols.Contains(m), hashSet);
		}

		// Token: 0x06001545 RID: 5445 RVA: 0x000732F4 File Offset: 0x000714F4
		private ImmutableArray<Symbol> GetMethodGroupSemanticSymbols(BoundMethodGroup boundNode, BoundNode boundNodeForSyntacticParent, Binder binderOpt, out LookupResultKind resultKind, out bool isDynamic, out ImmutableArray<Symbol> methodGroup)
		{
			ImmutableArray<Symbol> immutableArray = ImmutableArray<Symbol>.Empty;
			resultKind = boundNode.ResultKind;
			if (resultKind == LookupResultKind.Empty)
			{
				resultKind = LookupResultKind.Viable;
			}
			isDynamic = false;
			Binder binder = binderOpt ?? this.GetEnclosingBinder(this.GetAdjustedNodePosition(boundNode.Syntax));
			methodGroup = CSharpSemanticModel.GetReducedAndFilteredMethodGroupSymbols(binder, boundNode).Cast<Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol, Symbol>();
			if (boundNodeForSyntacticParent != null)
			{
				BoundKind kind = boundNodeForSyntacticParent.Kind;
				if (kind <= BoundKind.DynamicInvocation)
				{
					if (kind != BoundKind.BadExpression)
					{
						if (kind != BoundKind.Conversion)
						{
							if (kind == BoundKind.DynamicInvocation)
							{
								immutableArray = ((BoundDynamicInvocation)boundNodeForSyntacticParent).ApplicableMethods.Cast<Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol, Symbol>();
								isDynamic = true;
								goto IL_0282;
							}
						}
						else
						{
							BoundConversion boundConversion = (BoundConversion)boundNodeForSyntacticParent;
							Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol methodSymbol = boundConversion.SymbolOpt;
							if (methodSymbol != null)
							{
								if (boundConversion.IsExtensionMethod)
								{
									methodSymbol = ReducedExtensionMethodSymbol.Create(methodSymbol);
								}
								immutableArray = ImmutableArray.Create<Symbol>(methodSymbol);
								resultKind = boundConversion.ResultKind;
								goto IL_0282;
							}
						}
					}
					else
					{
						ImmutableArray<Symbol> immutableArray2 = methodGroup;
						immutableArray = ((BoundBadExpression)boundNodeForSyntacticParent).Symbols.WhereAsArray((Symbol sym, ImmutableArray<Symbol> myMethodGroup) => myMethodGroup.Contains(sym), immutableArray2);
						if (immutableArray.Any<Symbol>())
						{
							resultKind = ((BoundBadExpression)boundNodeForSyntacticParent).ResultKind;
							goto IL_0282;
						}
						goto IL_0282;
					}
				}
				else if (kind != BoundKind.Call)
				{
					if (kind != BoundKind.DelegateCreationExpression)
					{
						if (kind == BoundKind.NameOfOperator)
						{
							immutableArray = methodGroup;
							resultKind = resultKind.WorseResultKind(LookupResultKind.MemberGroup);
							goto IL_0282;
						}
					}
					else
					{
						BoundDelegateCreationExpression boundDelegateCreationExpression = (BoundDelegateCreationExpression)boundNodeForSyntacticParent;
						if (boundDelegateCreationExpression.Argument == boundNode && boundDelegateCreationExpression.MethodOpt != null)
						{
							immutableArray = this.CreateReducedExtensionMethodIfPossible(boundDelegateCreationExpression, boundNode.ReceiverOpt);
							goto IL_0282;
						}
						goto IL_0282;
					}
				}
				else
				{
					BoundCall boundCall = (BoundCall)boundNodeForSyntacticParent;
					InvocationExpressionSyntax invocationExpressionSyntax = boundCall.Syntax as InvocationExpressionSyntax;
					if (invocationExpressionSyntax == null || invocationExpressionSyntax.Expression.SkipParens() != ((ExpressionSyntax)boundNode.Syntax).SkipParens() || boundCall.Method == null)
					{
						goto IL_0282;
					}
					if (boundCall.OriginalMethodsOpt.IsDefault)
					{
						immutableArray = this.CreateReducedExtensionMethodIfPossible(boundCall);
						resultKind = LookupResultKind.Viable;
						goto IL_0282;
					}
					resultKind = boundCall.ResultKind.WorseResultKind(LookupResultKind.OverloadResolutionFailure);
					immutableArray = StaticCast<Symbol>.From<Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol>(CSharpSemanticModel.CreateReducedExtensionMethodsFromOriginalsIfNecessary(boundCall, this.Compilation));
					goto IL_0282;
				}
				immutableArray = methodGroup;
				if (immutableArray.Length > 0)
				{
					resultKind = resultKind.WorseResultKind(LookupResultKind.OverloadResolutionFailure);
				}
			}
			else if (methodGroup.Length == 1 && !boundNode.HasAnyErrors)
			{
				immutableArray = methodGroup;
				if (immutableArray.Length > 0)
				{
					resultKind = resultKind.WorseResultKind(LookupResultKind.OverloadResolutionFailure);
				}
			}
			IL_0282:
			if (!immutableArray.Any<Symbol>())
			{
				immutableArray = methodGroup;
				if (!isDynamic && resultKind > LookupResultKind.OverloadResolutionFailure)
				{
					resultKind = LookupResultKind.OverloadResolutionFailure;
				}
			}
			return immutableArray;
		}

		// Token: 0x06001546 RID: 5446 RVA: 0x000735A8 File Offset: 0x000717A8
		private ImmutableArray<Symbol> GetPropertyGroupSemanticSymbols(BoundPropertyGroup boundNode, BoundNode boundNodeForSyntacticParent, Binder binderOpt, out LookupResultKind resultKind, out ImmutableArray<Symbol> propertyGroup)
		{
			ImmutableArray<Symbol> immutableArray = ImmutableArray<Symbol>.Empty;
			resultKind = boundNode.ResultKind;
			if (resultKind == LookupResultKind.Empty)
			{
				resultKind = LookupResultKind.Viable;
			}
			propertyGroup = boundNode.Properties.Cast<Microsoft.CodeAnalysis.CSharp.Symbols.PropertySymbol, Symbol>();
			if (boundNodeForSyntacticParent != null)
			{
				BoundKind kind = boundNodeForSyntacticParent.Kind;
				if (kind != BoundKind.BadExpression)
				{
					if (kind == BoundKind.IndexerAccess)
					{
						BoundIndexerAccess boundIndexerAccess = (BoundIndexerAccess)boundNodeForSyntacticParent;
						ElementAccessExpressionSyntax elementAccessExpressionSyntax = boundIndexerAccess.Syntax as ElementAccessExpressionSyntax;
						if (elementAccessExpressionSyntax != null && elementAccessExpressionSyntax.Expression == boundNode.Syntax && boundIndexerAccess.Indexer != null)
						{
							if (boundIndexerAccess.OriginalIndexersOpt.IsDefault)
							{
								immutableArray = ImmutableArray.Create<Symbol>(boundIndexerAccess.Indexer);
								resultKind = LookupResultKind.Viable;
							}
							else
							{
								resultKind = boundIndexerAccess.ResultKind.WorseResultKind(LookupResultKind.OverloadResolutionFailure);
								immutableArray = StaticCast<Symbol>.From<Microsoft.CodeAnalysis.CSharp.Symbols.PropertySymbol>(boundIndexerAccess.OriginalIndexersOpt);
							}
						}
					}
				}
				else
				{
					ImmutableArray<Symbol> immutableArray2 = propertyGroup;
					immutableArray = ((BoundBadExpression)boundNodeForSyntacticParent).Symbols.WhereAsArray((Symbol sym, ImmutableArray<Symbol> myPropertyGroup) => myPropertyGroup.Contains(sym), immutableArray2);
					if (immutableArray.Any<Symbol>())
					{
						resultKind = ((BoundBadExpression)boundNodeForSyntacticParent).ResultKind;
					}
				}
			}
			else if (propertyGroup.Length == 1 && !boundNode.HasAnyErrors)
			{
				immutableArray = propertyGroup;
			}
			if (!immutableArray.Any<Symbol>())
			{
				immutableArray = propertyGroup;
				if (resultKind > LookupResultKind.OverloadResolutionFailure)
				{
					resultKind = LookupResultKind.OverloadResolutionFailure;
				}
			}
			return immutableArray;
		}

		// Token: 0x06001547 RID: 5447 RVA: 0x00073708 File Offset: 0x00071908
		private SymbolInfo GetNamedArgumentSymbolInfo(IdentifierNameSyntax identifierNameSyntax, CancellationToken cancellationToken)
		{
			string valueText = identifierNameSyntax.Identifier.ValueText;
			if (valueText.Length == 0)
			{
				return SymbolInfo.None;
			}
			CSharpSyntaxNode parent = identifierNameSyntax.Parent.Parent.Parent;
			if (parent.IsKind(SyntaxKind.TupleExpression))
			{
				ArgumentSyntax argumentSyntax = (ArgumentSyntax)identifierNameSyntax.Parent.Parent;
				ISymbol declaredSymbol = this.GetDeclaredSymbol(argumentSyntax, cancellationToken);
				if (declaredSymbol != null)
				{
					return new SymbolInfo(declaredSymbol, ImmutableArray<ISymbol>.Empty, CandidateReason.None);
				}
				return SymbolInfo.None;
			}
			else
			{
				if (parent.IsKind(SyntaxKind.PropertyPatternClause) || parent.IsKind(SyntaxKind.PositionalPatternClause))
				{
					return this.GetSymbolInfoWorker(identifierNameSyntax, CSharpSemanticModel.SymbolInfoOptions.DefaultOptions, cancellationToken);
				}
				CSharpSyntaxNode parent2 = parent.Parent;
				SymbolInfo symbolInfoWorker = this.GetSymbolInfoWorker(parent2, CSharpSemanticModel.SymbolInfoOptions.DefaultOptions, cancellationToken);
				if (symbolInfoWorker.Symbol != null)
				{
					Microsoft.CodeAnalysis.CSharp.Symbols.ParameterSymbol parameterSymbol = CSharpSemanticModel.FindNamedParameter(symbolInfoWorker.Symbol.GetSymbol().GetParameters(), valueText);
					if (parameterSymbol != null)
					{
						return new SymbolInfo(parameterSymbol.GetPublicSymbol(), ImmutableArray<ISymbol>.Empty, CandidateReason.None);
					}
					return SymbolInfo.None;
				}
				else
				{
					ArrayBuilder<ISymbol> instance = ArrayBuilder<ISymbol>.GetInstance();
					foreach (ISymbol symbol in symbolInfoWorker.CandidateSymbols)
					{
						SymbolKind kind = symbol.Kind;
						if (kind == SymbolKind.Method || kind == SymbolKind.Property)
						{
							Microsoft.CodeAnalysis.CSharp.Symbols.ParameterSymbol parameterSymbol2 = CSharpSemanticModel.FindNamedParameter(symbol.GetSymbol().GetParameters(), valueText);
							if (parameterSymbol2 != null)
							{
								instance.Add(parameterSymbol2.GetPublicSymbol());
							}
						}
					}
					if (instance.Count == 0)
					{
						instance.Free();
						return SymbolInfo.None;
					}
					return new SymbolInfo(null, instance.ToImmutableAndFree(), symbolInfoWorker.CandidateReason);
				}
			}
		}

		// Token: 0x06001548 RID: 5448 RVA: 0x0007388C File Offset: 0x00071A8C
		private static Microsoft.CodeAnalysis.CSharp.Symbols.ParameterSymbol FindNamedParameter(ImmutableArray<Microsoft.CodeAnalysis.CSharp.Symbols.ParameterSymbol> parameters, string argumentName)
		{
			foreach (Microsoft.CodeAnalysis.CSharp.Symbols.ParameterSymbol parameterSymbol in parameters)
			{
				if (parameterSymbol.Name == argumentName)
				{
					return parameterSymbol;
				}
			}
			return null;
		}

		// Token: 0x06001549 RID: 5449 RVA: 0x000738C8 File Offset: 0x00071AC8
		internal static ImmutableArray<Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol> GetReducedAndFilteredMethodGroupSymbols(Binder binder, BoundMethodGroup node)
		{
			ArrayBuilder<Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol> instance = ArrayBuilder<Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol>.GetInstance();
			ArrayBuilder<Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol> instance2 = ArrayBuilder<Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol>.GetInstance();
			LookupResultKind lookupResultKind = LookupResultKind.Empty;
			ImmutableArray<TypeWithAnnotations> typeArgumentsOpt = node.TypeArgumentsOpt;
			if (node.Methods.Any<Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol>())
			{
				foreach (Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol methodSymbol in CSharpSemanticModel.FilterOverriddenOrHiddenMethods(node.Methods))
				{
					CSharpSemanticModel.MergeReducedAndFilteredMethodGroupSymbol(instance, instance2, new SingleLookupResult(node.ResultKind, methodSymbol, node.LookupError), typeArgumentsOpt, null, ref lookupResultKind, binder.Compilation);
				}
			}
			else
			{
				Symbol lookupSymbolOpt = node.LookupSymbolOpt;
				if (lookupSymbolOpt != null && lookupSymbolOpt.Kind == SymbolKind.Method)
				{
					CSharpSemanticModel.MergeReducedAndFilteredMethodGroupSymbol(instance, instance2, new SingleLookupResult(node.ResultKind, lookupSymbolOpt, node.LookupError), typeArgumentsOpt, null, ref lookupResultKind, binder.Compilation);
				}
			}
			BoundExpression receiverOpt = node.ReceiverOpt;
			string name = node.Name;
			if (node.SearchExtensionMethods)
			{
				int num;
				LookupOptions lookupOptions;
				if (typeArgumentsOpt.IsDefault)
				{
					num = 0;
					lookupOptions = LookupOptions.AllMethodsOnArityZero;
				}
				else
				{
					num = typeArgumentsOpt.Length;
					lookupOptions = LookupOptions.Default;
				}
				binder = binder.WithAdditionalFlags(BinderFlags.SemanticModel);
				foreach (ref ExtensionMethodScope ptr in new ExtensionMethodScopes(binder))
				{
					ArrayBuilder<Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol> instance3 = ArrayBuilder<Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol>.GetInstance();
					ptr.Binder.GetCandidateExtensionMethods(instance3, name, num, lookupOptions, binder);
					foreach (Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol methodSymbol2 in instance3)
					{
						CompoundUseSiteInfo<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol> discarded = CompoundUseSiteInfo<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>.Discarded;
						CSharpSemanticModel.MergeReducedAndFilteredMethodGroupSymbol(instance, instance2, binder.CheckViability(methodSymbol2, num, lookupOptions, null, false, ref discarded, null), typeArgumentsOpt, receiverOpt.Type, ref lookupResultKind, binder.Compilation);
					}
					instance3.Free();
				}
			}
			instance.Free();
			return instance2.ToImmutableAndFree();
		}

		// Token: 0x0600154A RID: 5450 RVA: 0x00073A68 File Offset: 0x00071C68
		private static bool AddReducedAndFilteredMethodGroupSymbol(ArrayBuilder<Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol> methods, ArrayBuilder<Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol> filteredMethods, Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol method, ImmutableArray<TypeWithAnnotations> typeArguments, Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol receiverType, CSharpCompilation compilation)
		{
			Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol methodSymbol;
			if (!typeArguments.IsDefaultOrEmpty && method.Arity == typeArguments.Length)
			{
				methodSymbol = method.Construct(typeArguments);
			}
			else
			{
				methodSymbol = method;
			}
			if (receiverType != null)
			{
				methodSymbol = methodSymbol.ReduceExtensionMethod(receiverType, compilation);
				if (methodSymbol == null)
				{
					return false;
				}
			}
			if (filteredMethods.Contains(methodSymbol))
			{
				return false;
			}
			methods.Add(method);
			filteredMethods.Add(methodSymbol);
			return true;
		}

		// Token: 0x0600154B RID: 5451 RVA: 0x00073AC8 File Offset: 0x00071CC8
		private static void MergeReducedAndFilteredMethodGroupSymbol(ArrayBuilder<Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol> methods, ArrayBuilder<Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol> filteredMethods, SingleLookupResult singleResult, ImmutableArray<TypeWithAnnotations> typeArguments, Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol receiverType, ref LookupResultKind resultKind, CSharpCompilation compilation)
		{
			LookupResultKind kind = singleResult.Kind;
			if (resultKind > kind)
			{
				return;
			}
			if (resultKind < kind)
			{
				methods.Clear();
				filteredMethods.Clear();
				resultKind = LookupResultKind.Empty;
			}
			Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol methodSymbol = (Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol)singleResult.Symbol;
			if (CSharpSemanticModel.AddReducedAndFilteredMethodGroupSymbol(methods, filteredMethods, methodSymbol, typeArguments, receiverType, compilation) && resultKind < kind)
			{
				resultKind = kind;
			}
		}

		// Token: 0x0600154C RID: 5452 RVA: 0x00073B20 File Offset: 0x00071D20
		private static ImmutableArray<Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol> CreateReducedExtensionMethodsFromOriginalsIfNecessary(BoundCall call, CSharpCompilation compilation)
		{
			ImmutableArray<Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol> originalMethodsOpt = call.OriginalMethodsOpt;
			Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol typeSymbol = null;
			if (call.InvokedAsExtensionMethod)
			{
				if (call.ReceiverOpt != null)
				{
					typeSymbol = call.ReceiverOpt.Type;
				}
				else
				{
					typeSymbol = call.Arguments[0].Type;
				}
			}
			ArrayBuilder<Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol> instance = ArrayBuilder<Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol>.GetInstance();
			ArrayBuilder<Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol> instance2 = ArrayBuilder<Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol>.GetInstance();
			foreach (Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol methodSymbol in CSharpSemanticModel.FilterOverriddenOrHiddenMethods(originalMethodsOpt))
			{
				CSharpSemanticModel.AddReducedAndFilteredMethodGroupSymbol(instance, instance2, methodSymbol, default(ImmutableArray<TypeWithAnnotations>), typeSymbol, compilation);
			}
			instance.Free();
			return instance2.ToImmutableAndFree();
		}

		// Token: 0x0600154D RID: 5453 RVA: 0x00073BBC File Offset: 0x00071DBC
		private ImmutableArray<Symbol> CreateReducedExtensionMethodIfPossible(BoundCall call)
		{
			Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol methodSymbol = call.Method;
			if (call.InvokedAsExtensionMethod && methodSymbol.IsExtensionMethod && methodSymbol.MethodKind != MethodKind.ReducedExtension)
			{
				BoundExpression boundExpression = call.Arguments[0];
				methodSymbol = methodSymbol.ReduceExtensionMethod(boundExpression.Type, this.Compilation) ?? methodSymbol;
			}
			return ImmutableArray.Create<Symbol>(methodSymbol);
		}

		// Token: 0x0600154E RID: 5454 RVA: 0x00073C18 File Offset: 0x00071E18
		private ImmutableArray<Symbol> CreateReducedExtensionMethodIfPossible(BoundDelegateCreationExpression delegateCreation, BoundExpression receiverOpt)
		{
			Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol methodSymbol = delegateCreation.MethodOpt;
			if (delegateCreation.IsExtensionMethod && methodSymbol.IsExtensionMethod && receiverOpt != null)
			{
				methodSymbol = methodSymbol.ReduceExtensionMethod(receiverOpt.Type, this.Compilation) ?? methodSymbol;
			}
			return ImmutableArray.Create<Symbol>(methodSymbol);
		}

		// Token: 0x0600154F RID: 5455
		public abstract ForEachStatementInfo GetForEachStatementInfo(ForEachStatementSyntax node);

		// Token: 0x06001550 RID: 5456
		public abstract ForEachStatementInfo GetForEachStatementInfo(CommonForEachStatementSyntax node);

		// Token: 0x06001551 RID: 5457
		public abstract DeconstructionInfo GetDeconstructionInfo(AssignmentExpressionSyntax node);

		// Token: 0x06001552 RID: 5458
		public abstract DeconstructionInfo GetDeconstructionInfo(ForEachVariableStatementSyntax node);

		// Token: 0x06001553 RID: 5459
		public abstract AwaitExpressionInfo GetAwaitExpressionInfo(AwaitExpressionSyntax node);

		// Token: 0x06001554 RID: 5460 RVA: 0x00073C60 File Offset: 0x00071E60
		public PreprocessingSymbolInfo GetPreprocessingSymbolInfo(IdentifierNameSyntax node)
		{
			this.CheckSyntaxNode(node);
			if (Enumerable.Any<SyntaxNode>(node.Ancestors(true), (SyntaxNode n) => SyntaxFacts.IsPreprocessorDirective(n.Kind())))
			{
				bool flag = this.SyntaxTree.IsPreprocessorSymbolDefined(node.Identifier.ValueText, node.Identifier.SpanStart);
				return new PreprocessingSymbolInfo(new PreprocessingSymbol(node.Identifier.ValueText), flag);
			}
			return PreprocessingSymbolInfo.None;
		}

		// Token: 0x06001555 RID: 5461 RVA: 0x00073CE8 File Offset: 0x00071EE8
		internal static void ValidateSymbolInfoOptions(CSharpSemanticModel.SymbolInfoOptions options)
		{
		}

		// Token: 0x06001556 RID: 5462 RVA: 0x00073CEC File Offset: 0x00071EEC
		public new ISymbol GetEnclosingSymbol(int position, CancellationToken cancellationToken = default(CancellationToken))
		{
			position = this.CheckAndAdjustPosition(position);
			Binder enclosingBinder = this.GetEnclosingBinder(position);
			if (enclosingBinder != null)
			{
				return enclosingBinder.ContainingMemberOrLambda.GetPublicSymbol();
			}
			return null;
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06001557 RID: 5463 RVA: 0x00073D1A File Offset: 0x00071F1A
		public sealed override string Language
		{
			get
			{
				return "C#";
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06001558 RID: 5464 RVA: 0x00073D21 File Offset: 0x00071F21
		protected sealed override Compilation CompilationCore
		{
			get
			{
				return this.Compilation;
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06001559 RID: 5465 RVA: 0x00073D29 File Offset: 0x00071F29
		protected sealed override SemanticModel ParentModelCore
		{
			get
			{
				return this.ParentModel;
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x0600155A RID: 5466 RVA: 0x00073D31 File Offset: 0x00071F31
		protected sealed override SyntaxTree SyntaxTreeCore
		{
			get
			{
				return this.SyntaxTree;
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x0600155B RID: 5467 RVA: 0x00073D39 File Offset: 0x00071F39
		protected sealed override SyntaxNode RootCore
		{
			get
			{
				return this.Root;
			}
		}

		// Token: 0x0600155C RID: 5468 RVA: 0x00073D44 File Offset: 0x00071F44
		private SymbolInfo GetSymbolInfoFromNode(SyntaxNode node, CancellationToken cancellationToken)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			ExpressionSyntax expressionSyntax = node as ExpressionSyntax;
			if (expressionSyntax != null)
			{
				return this.GetSymbolInfo(expressionSyntax, cancellationToken);
			}
			ConstructorInitializerSyntax constructorInitializerSyntax = node as ConstructorInitializerSyntax;
			if (constructorInitializerSyntax != null)
			{
				return this.GetSymbolInfo(constructorInitializerSyntax, cancellationToken);
			}
			PrimaryConstructorBaseTypeSyntax primaryConstructorBaseTypeSyntax = node as PrimaryConstructorBaseTypeSyntax;
			if (primaryConstructorBaseTypeSyntax != null)
			{
				return this.GetSymbolInfo(primaryConstructorBaseTypeSyntax, cancellationToken);
			}
			AttributeSyntax attributeSyntax = node as AttributeSyntax;
			if (attributeSyntax != null)
			{
				return this.GetSymbolInfo(attributeSyntax, cancellationToken);
			}
			CrefSyntax crefSyntax = node as CrefSyntax;
			if (crefSyntax != null)
			{
				return this.GetSymbolInfo(crefSyntax, cancellationToken);
			}
			SelectOrGroupClauseSyntax selectOrGroupClauseSyntax = node as SelectOrGroupClauseSyntax;
			if (selectOrGroupClauseSyntax != null)
			{
				return this.GetSymbolInfo(selectOrGroupClauseSyntax, cancellationToken);
			}
			OrderingSyntax orderingSyntax = node as OrderingSyntax;
			if (orderingSyntax != null)
			{
				return this.GetSymbolInfo(orderingSyntax, cancellationToken);
			}
			PositionalPatternClauseSyntax positionalPatternClauseSyntax = node as PositionalPatternClauseSyntax;
			if (positionalPatternClauseSyntax == null)
			{
				return SymbolInfo.None;
			}
			return this.GetSymbolInfo(positionalPatternClauseSyntax, cancellationToken);
		}

		// Token: 0x0600155D RID: 5469 RVA: 0x00073E0C File Offset: 0x0007200C
		private TypeInfo GetTypeInfoFromNode(SyntaxNode node, CancellationToken cancellationToken)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			ExpressionSyntax expressionSyntax = node as ExpressionSyntax;
			if (expressionSyntax != null)
			{
				return this.GetTypeInfo(expressionSyntax, cancellationToken);
			}
			ConstructorInitializerSyntax constructorInitializerSyntax = node as ConstructorInitializerSyntax;
			if (constructorInitializerSyntax != null)
			{
				return this.GetTypeInfo(constructorInitializerSyntax, cancellationToken);
			}
			AttributeSyntax attributeSyntax = node as AttributeSyntax;
			if (attributeSyntax != null)
			{
				return this.GetTypeInfo(attributeSyntax, cancellationToken);
			}
			SelectOrGroupClauseSyntax selectOrGroupClauseSyntax = node as SelectOrGroupClauseSyntax;
			if (selectOrGroupClauseSyntax != null)
			{
				return this.GetTypeInfo(selectOrGroupClauseSyntax, cancellationToken);
			}
			PatternSyntax patternSyntax = node as PatternSyntax;
			if (patternSyntax == null)
			{
				return CSharpTypeInfo.None;
			}
			return this.GetTypeInfo(patternSyntax, cancellationToken);
		}

		// Token: 0x0600155E RID: 5470 RVA: 0x00073E98 File Offset: 0x00072098
		private ImmutableArray<ISymbol> GetMemberGroupFromNode(SyntaxNode node, CancellationToken cancellationToken)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			ExpressionSyntax expressionSyntax = node as ExpressionSyntax;
			if (expressionSyntax != null)
			{
				return this.GetMemberGroup(expressionSyntax, cancellationToken);
			}
			ConstructorInitializerSyntax constructorInitializerSyntax = node as ConstructorInitializerSyntax;
			if (constructorInitializerSyntax != null)
			{
				return this.GetMemberGroup(constructorInitializerSyntax, cancellationToken);
			}
			AttributeSyntax attributeSyntax = node as AttributeSyntax;
			if (attributeSyntax == null)
			{
				return ImmutableArray<ISymbol>.Empty;
			}
			return this.GetMemberGroup(attributeSyntax, cancellationToken);
		}

		// Token: 0x0600155F RID: 5471 RVA: 0x00073EF3 File Offset: 0x000720F3
		protected sealed override ImmutableArray<ISymbol> GetMemberGroupCore(SyntaxNode node, CancellationToken cancellationToken)
		{
			return StaticCast<ISymbol>.From<ISymbol>(this.GetMemberGroupFromNode(node, cancellationToken));
		}

		// Token: 0x06001560 RID: 5472 RVA: 0x00073F04 File Offset: 0x00072104
		protected sealed override SymbolInfo GetSpeculativeSymbolInfoCore(int position, SyntaxNode node, SpeculativeBindingOption bindingOption)
		{
			ExpressionSyntax expressionSyntax = node as ExpressionSyntax;
			if (expressionSyntax != null)
			{
				return this.GetSpeculativeSymbolInfo(position, expressionSyntax, bindingOption);
			}
			ConstructorInitializerSyntax constructorInitializerSyntax = node as ConstructorInitializerSyntax;
			if (constructorInitializerSyntax != null)
			{
				return this.GetSpeculativeSymbolInfo(position, constructorInitializerSyntax);
			}
			PrimaryConstructorBaseTypeSyntax primaryConstructorBaseTypeSyntax = node as PrimaryConstructorBaseTypeSyntax;
			if (primaryConstructorBaseTypeSyntax != null)
			{
				return this.GetSpeculativeSymbolInfo(position, primaryConstructorBaseTypeSyntax);
			}
			AttributeSyntax attributeSyntax = node as AttributeSyntax;
			if (attributeSyntax != null)
			{
				return this.GetSpeculativeSymbolInfo(position, attributeSyntax);
			}
			CrefSyntax crefSyntax = node as CrefSyntax;
			if (crefSyntax == null)
			{
				return SymbolInfo.None;
			}
			return this.GetSpeculativeSymbolInfo(position, crefSyntax, CSharpSemanticModel.SymbolInfoOptions.DefaultOptions);
		}

		// Token: 0x06001561 RID: 5473 RVA: 0x00073F7C File Offset: 0x0007217C
		protected sealed override TypeInfo GetSpeculativeTypeInfoCore(int position, SyntaxNode node, SpeculativeBindingOption bindingOption)
		{
			ExpressionSyntax expressionSyntax = node as ExpressionSyntax;
			if (expressionSyntax == null)
			{
				return CSharpTypeInfo.None;
			}
			return this.GetSpeculativeTypeInfo(position, expressionSyntax, bindingOption);
		}

		// Token: 0x06001562 RID: 5474 RVA: 0x00073FA8 File Offset: 0x000721A8
		protected sealed override IAliasSymbol GetSpeculativeAliasInfoCore(int position, SyntaxNode nameSyntax, SpeculativeBindingOption bindingOption)
		{
			IdentifierNameSyntax identifierNameSyntax = nameSyntax as IdentifierNameSyntax;
			if (identifierNameSyntax == null)
			{
				return null;
			}
			return this.GetSpeculativeAliasInfo(position, identifierNameSyntax, bindingOption);
		}

		// Token: 0x06001563 RID: 5475 RVA: 0x00073FCA File Offset: 0x000721CA
		protected sealed override SymbolInfo GetSymbolInfoCore(SyntaxNode node, CancellationToken cancellationToken)
		{
			return this.GetSymbolInfoFromNode(node, cancellationToken);
		}

		// Token: 0x06001564 RID: 5476 RVA: 0x00073FD4 File Offset: 0x000721D4
		protected sealed override TypeInfo GetTypeInfoCore(SyntaxNode node, CancellationToken cancellationToken)
		{
			return this.GetTypeInfoFromNode(node, cancellationToken);
		}

		// Token: 0x06001565 RID: 5477 RVA: 0x00073FE0 File Offset: 0x000721E0
		protected sealed override IAliasSymbol GetAliasInfoCore(SyntaxNode node, CancellationToken cancellationToken)
		{
			IdentifierNameSyntax identifierNameSyntax = node as IdentifierNameSyntax;
			if (identifierNameSyntax == null)
			{
				return null;
			}
			return this.GetAliasInfo(identifierNameSyntax, cancellationToken);
		}

		// Token: 0x06001566 RID: 5478 RVA: 0x00074004 File Offset: 0x00072204
		protected sealed override PreprocessingSymbolInfo GetPreprocessingSymbolInfoCore(SyntaxNode node)
		{
			IdentifierNameSyntax identifierNameSyntax = node as IdentifierNameSyntax;
			if (identifierNameSyntax == null)
			{
				return PreprocessingSymbolInfo.None;
			}
			return this.GetPreprocessingSymbolInfo(identifierNameSyntax);
		}

		// Token: 0x06001567 RID: 5479 RVA: 0x00074028 File Offset: 0x00072228
		protected sealed override ISymbol GetDeclaredSymbolCore(SyntaxNode node, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			AccessorDeclarationSyntax accessorDeclarationSyntax = node as AccessorDeclarationSyntax;
			if (accessorDeclarationSyntax != null)
			{
				return this.GetDeclaredSymbol(accessorDeclarationSyntax, cancellationToken);
			}
			BaseTypeDeclarationSyntax baseTypeDeclarationSyntax = node as BaseTypeDeclarationSyntax;
			if (baseTypeDeclarationSyntax != null)
			{
				return this.GetDeclaredSymbol(baseTypeDeclarationSyntax, cancellationToken);
			}
			QueryClauseSyntax queryClauseSyntax = node as QueryClauseSyntax;
			if (queryClauseSyntax != null)
			{
				return this.GetDeclaredSymbol(queryClauseSyntax, cancellationToken);
			}
			MemberDeclarationSyntax memberDeclarationSyntax = node as MemberDeclarationSyntax;
			if (memberDeclarationSyntax == null)
			{
				SyntaxKind syntaxKind = node.Kind();
				if (syntaxKind <= SyntaxKind.LabeledStatement)
				{
					if (syntaxKind <= SyntaxKind.AnonymousObjectCreationExpression)
					{
						if (syntaxKind == SyntaxKind.Argument)
						{
							return this.GetDeclaredSymbol((ArgumentSyntax)node, cancellationToken);
						}
						if (syntaxKind == SyntaxKind.AnonymousObjectMemberDeclarator)
						{
							return this.GetDeclaredSymbol((AnonymousObjectMemberDeclaratorSyntax)node, cancellationToken);
						}
						if (syntaxKind == SyntaxKind.AnonymousObjectCreationExpression)
						{
							return this.GetDeclaredSymbol((AnonymousObjectCreationExpressionSyntax)node, cancellationToken);
						}
					}
					else if (syntaxKind <= SyntaxKind.QueryContinuation)
					{
						if (syntaxKind == SyntaxKind.JoinIntoClause)
						{
							return this.GetDeclaredSymbol((JoinIntoClauseSyntax)node, cancellationToken);
						}
						if (syntaxKind == SyntaxKind.QueryContinuation)
						{
							return this.GetDeclaredSymbol((QueryContinuationSyntax)node, cancellationToken);
						}
					}
					else
					{
						if (syntaxKind == SyntaxKind.VariableDeclarator)
						{
							return this.GetDeclaredSymbol((VariableDeclaratorSyntax)node, cancellationToken);
						}
						if (syntaxKind == SyntaxKind.LabeledStatement)
						{
							return this.GetDeclaredSymbol((LabeledStatementSyntax)node, cancellationToken);
						}
					}
				}
				else if (syntaxKind <= SyntaxKind.LocalFunctionStatement)
				{
					if (syntaxKind <= SyntaxKind.DefaultSwitchLabel)
					{
						if (syntaxKind == SyntaxKind.ForEachStatement)
						{
							return this.GetDeclaredSymbol((ForEachStatementSyntax)node, cancellationToken);
						}
						if (syntaxKind - SyntaxKind.CaseSwitchLabel <= 1)
						{
							return this.GetDeclaredSymbol((SwitchLabelSyntax)node, cancellationToken);
						}
					}
					else
					{
						if (syntaxKind == SyntaxKind.CatchDeclaration)
						{
							return this.GetDeclaredSymbol((CatchDeclarationSyntax)node, cancellationToken);
						}
						if (syntaxKind == SyntaxKind.LocalFunctionStatement)
						{
							return this.GetDeclaredSymbol((LocalFunctionStatementSyntax)node, cancellationToken);
						}
					}
				}
				else if (syntaxKind <= SyntaxKind.Parameter)
				{
					switch (syntaxKind)
					{
					case SyntaxKind.CompilationUnit:
						return this.GetDeclaredSymbol((CompilationUnitSyntax)node, cancellationToken);
					case SyntaxKind.GlobalStatement:
						break;
					case SyntaxKind.NamespaceDeclaration:
						return this.GetDeclaredSymbol((NamespaceDeclarationSyntax)node, cancellationToken);
					case SyntaxKind.UsingDirective:
					{
						UsingDirectiveSyntax usingDirectiveSyntax = (UsingDirectiveSyntax)node;
						if (usingDirectiveSyntax.Alias != null)
						{
							return this.GetDeclaredSymbol(usingDirectiveSyntax, cancellationToken);
						}
						break;
					}
					default:
						if (syntaxKind == SyntaxKind.Parameter)
						{
							return this.GetDeclaredSymbol((ParameterSyntax)node, cancellationToken);
						}
						break;
					}
				}
				else
				{
					if (syntaxKind == SyntaxKind.TypeParameter)
					{
						return this.GetDeclaredSymbol((TypeParameterSyntax)node, cancellationToken);
					}
					switch (syntaxKind)
					{
					case SyntaxKind.TupleElement:
						return this.GetDeclaredSymbol((TupleElementSyntax)node, cancellationToken);
					case SyntaxKind.TupleExpression:
						return this.GetDeclaredSymbol((TupleExpressionSyntax)node, cancellationToken);
					case SyntaxKind.SingleVariableDesignation:
						return this.GetDeclaredSymbol((SingleVariableDesignationSyntax)node, cancellationToken);
					}
				}
				return null;
			}
			return this.GetDeclaredSymbol(memberDeclarationSyntax, cancellationToken);
		}

		// Token: 0x06001568 RID: 5480 RVA: 0x000742D4 File Offset: 0x000724D4
		public ISymbol GetDeclaredSymbol(TupleElementSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			this.CheckSyntaxNode(declarationSyntax);
			TupleTypeSyntax tupleTypeSyntax = declarationSyntax.Parent as TupleTypeSyntax;
			if (tupleTypeSyntax == null)
			{
				return null;
			}
			Microsoft.CodeAnalysis.CSharp.Symbols.NamedTypeSymbol namedTypeSymbol = this.GetSymbolInfo(tupleTypeSyntax, cancellationToken).Symbol.GetSymbol() as Microsoft.CodeAnalysis.CSharp.Symbols.NamedTypeSymbol;
			if (namedTypeSymbol == null)
			{
				return null;
			}
			return namedTypeSymbol.TupleElements.ElementAtOrDefault<Microsoft.CodeAnalysis.CSharp.Symbols.FieldSymbol>(tupleTypeSyntax.Elements.IndexOf(declarationSyntax)).GetPublicSymbol();
		}

		// Token: 0x06001569 RID: 5481 RVA: 0x00074338 File Offset: 0x00072538
		protected sealed override ImmutableArray<ISymbol> GetDeclaredSymbolsCore(SyntaxNode declaration, CancellationToken cancellationToken = default(CancellationToken))
		{
			cancellationToken.ThrowIfCancellationRequested();
			BaseFieldDeclarationSyntax baseFieldDeclarationSyntax = declaration as BaseFieldDeclarationSyntax;
			if (baseFieldDeclarationSyntax != null)
			{
				return this.GetDeclaredSymbols(baseFieldDeclarationSyntax, cancellationToken);
			}
			ISymbol declaredSymbolCore = this.GetDeclaredSymbolCore(declaration, cancellationToken);
			if (declaredSymbolCore != null)
			{
				return ImmutableArray.Create<ISymbol>(declaredSymbolCore);
			}
			return ImmutableArray.Create<ISymbol>();
		}

		// Token: 0x0600156A RID: 5482 RVA: 0x00074377 File Offset: 0x00072577
		internal override void ComputeDeclarationsInSpan(TextSpan span, bool getSymbol, ArrayBuilder<DeclarationInfo> builder, CancellationToken cancellationToken)
		{
			CSharpDeclarationComputer.ComputeDeclarationsInSpan(this, span, getSymbol, builder, cancellationToken);
		}

		// Token: 0x0600156B RID: 5483 RVA: 0x00074384 File Offset: 0x00072584
		internal override void ComputeDeclarationsInNode(SyntaxNode node, ISymbol associatedSymbol, bool getSymbol, ArrayBuilder<DeclarationInfo> builder, CancellationToken cancellationToken, int? levelsToCompute = null)
		{
			CSharpDeclarationComputer.ComputeDeclarationsInNode(this, associatedSymbol, node, getSymbol, builder, cancellationToken, levelsToCompute);
		}

		// Token: 0x0600156C RID: 5484
		internal abstract override Func<SyntaxNode, bool> GetSyntaxNodesToAnalyzeFilter(SyntaxNode declaredNode, ISymbol declaredSymbol);

		// Token: 0x0600156D RID: 5485 RVA: 0x00074398 File Offset: 0x00072598
		protected internal override SyntaxNode GetTopmostNodeForDiagnosticAnalysis(ISymbol symbol, SyntaxNode declaringSyntax)
		{
			SymbolKind kind = symbol.Kind;
			if (kind - SymbolKind.Event <= 1)
			{
				BaseFieldDeclarationSyntax baseFieldDeclarationSyntax = declaringSyntax.FirstAncestorOrSelf<BaseFieldDeclarationSyntax>(null, true);
				if (baseFieldDeclarationSyntax != null)
				{
					return baseFieldDeclarationSyntax;
				}
			}
			return declaringSyntax;
		}

		// Token: 0x0600156E RID: 5486 RVA: 0x000743C1 File Offset: 0x000725C1
		protected sealed override ImmutableArray<ISymbol> LookupSymbolsCore(int position, INamespaceOrTypeSymbol container, string name, bool includeReducedExtensionMethods)
		{
			return this.LookupSymbols(position, container.EnsureCSharpSymbolOrNull("container"), name, includeReducedExtensionMethods);
		}

		// Token: 0x0600156F RID: 5487 RVA: 0x000743D8 File Offset: 0x000725D8
		protected sealed override ImmutableArray<ISymbol> LookupBaseMembersCore(int position, string name)
		{
			return this.LookupBaseMembers(position, name);
		}

		// Token: 0x06001570 RID: 5488 RVA: 0x000743E2 File Offset: 0x000725E2
		protected sealed override ImmutableArray<ISymbol> LookupStaticMembersCore(int position, INamespaceOrTypeSymbol container, string name)
		{
			return this.LookupStaticMembers(position, container.EnsureCSharpSymbolOrNull("container"), name);
		}

		// Token: 0x06001571 RID: 5489 RVA: 0x000743F7 File Offset: 0x000725F7
		protected sealed override ImmutableArray<ISymbol> LookupNamespacesAndTypesCore(int position, INamespaceOrTypeSymbol container, string name)
		{
			return this.LookupNamespacesAndTypes(position, container.EnsureCSharpSymbolOrNull("container"), name);
		}

		// Token: 0x06001572 RID: 5490 RVA: 0x0007440C File Offset: 0x0007260C
		protected sealed override ImmutableArray<ISymbol> LookupLabelsCore(int position, string name)
		{
			return this.LookupLabels(position, name);
		}

		// Token: 0x06001573 RID: 5491 RVA: 0x00074418 File Offset: 0x00072618
		protected sealed override ControlFlowAnalysis AnalyzeControlFlowCore(SyntaxNode firstStatement, SyntaxNode lastStatement)
		{
			if (firstStatement == null)
			{
				throw new ArgumentNullException("firstStatement");
			}
			if (lastStatement == null)
			{
				throw new ArgumentNullException("lastStatement");
			}
			StatementSyntax statementSyntax = firstStatement as StatementSyntax;
			if (statementSyntax == null)
			{
				throw new ArgumentException("firstStatement is not a StatementSyntax.");
			}
			StatementSyntax statementSyntax2 = lastStatement as StatementSyntax;
			if (statementSyntax2 == null)
			{
				throw new ArgumentException("firstStatement is a StatementSyntax but lastStatement isn't.");
			}
			return this.AnalyzeControlFlow(statementSyntax, statementSyntax2);
		}

		// Token: 0x06001574 RID: 5492 RVA: 0x00074474 File Offset: 0x00072674
		protected sealed override ControlFlowAnalysis AnalyzeControlFlowCore(SyntaxNode statement)
		{
			if (statement == null)
			{
				throw new ArgumentNullException("statement");
			}
			StatementSyntax statementSyntax = statement as StatementSyntax;
			if (statementSyntax == null)
			{
				throw new ArgumentException("statement is not a StatementSyntax.");
			}
			return this.AnalyzeControlFlow(statementSyntax);
		}

		// Token: 0x06001575 RID: 5493 RVA: 0x000744AC File Offset: 0x000726AC
		protected sealed override DataFlowAnalysis AnalyzeDataFlowCore(SyntaxNode firstStatement, SyntaxNode lastStatement)
		{
			if (firstStatement == null)
			{
				throw new ArgumentNullException("firstStatement");
			}
			if (lastStatement == null)
			{
				throw new ArgumentNullException("lastStatement");
			}
			StatementSyntax statementSyntax = firstStatement as StatementSyntax;
			if (statementSyntax == null)
			{
				throw new ArgumentException("firstStatement is not a StatementSyntax.");
			}
			StatementSyntax statementSyntax2 = lastStatement as StatementSyntax;
			if (statementSyntax2 == null)
			{
				throw new ArgumentException("lastStatement is not a StatementSyntax.");
			}
			return this.AnalyzeDataFlow(statementSyntax, statementSyntax2);
		}

		// Token: 0x06001576 RID: 5494 RVA: 0x00074508 File Offset: 0x00072708
		protected sealed override DataFlowAnalysis AnalyzeDataFlowCore(SyntaxNode statementOrExpression)
		{
			if (statementOrExpression == null)
			{
				throw new ArgumentNullException("statementOrExpression");
			}
			StatementSyntax statementSyntax = statementOrExpression as StatementSyntax;
			if (statementSyntax != null)
			{
				return this.AnalyzeDataFlow(statementSyntax);
			}
			ExpressionSyntax expressionSyntax = statementOrExpression as ExpressionSyntax;
			if (expressionSyntax == null)
			{
				throw new ArgumentException("statementOrExpression is not a StatementSyntax or an ExpressionSyntax.");
			}
			return this.AnalyzeDataFlow(expressionSyntax);
		}

		// Token: 0x06001577 RID: 5495 RVA: 0x00074554 File Offset: 0x00072754
		protected sealed override Optional<object> GetConstantValueCore(SyntaxNode node, CancellationToken cancellationToken)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			ExpressionSyntax expressionSyntax = node as ExpressionSyntax;
			if (expressionSyntax == null)
			{
				return default(Optional<object>);
			}
			return this.GetConstantValue(expressionSyntax, cancellationToken);
		}

		// Token: 0x06001578 RID: 5496 RVA: 0x0007458B File Offset: 0x0007278B
		protected sealed override ISymbol GetEnclosingSymbolCore(int position, CancellationToken cancellationToken)
		{
			return this.GetEnclosingSymbol(position, cancellationToken);
		}

		// Token: 0x06001579 RID: 5497 RVA: 0x00074595 File Offset: 0x00072795
		protected sealed override bool IsAccessibleCore(int position, ISymbol symbol)
		{
			return this.IsAccessible(position, symbol.EnsureCSharpSymbolOrNull("symbol"));
		}

		// Token: 0x0600157A RID: 5498 RVA: 0x000745A9 File Offset: 0x000727A9
		protected sealed override bool IsEventUsableAsFieldCore(int position, IEventSymbol symbol)
		{
			return this.IsEventUsableAsField(position, symbol.EnsureCSharpSymbolOrNull("symbol"));
		}

		// Token: 0x0600157B RID: 5499 RVA: 0x000745C0 File Offset: 0x000727C0
		public sealed override NullableContext GetNullableContext(int position)
		{
			CSharpSyntaxTree csharpSyntaxTree = (CSharpSyntaxTree)this.Root.SyntaxTree;
			NullableContextState nullableContextState = csharpSyntaxTree.GetNullableContextState(position);
			NullableContextOptions nullableContextOptions = (csharpSyntaxTree.IsGeneratedCode(this.Compilation.Options.SyntaxTreeOptionsProvider, CancellationToken.None) ? NullableContextOptions.Disable : this.Compilation.Options.NullableContextOptions);
			return CSharpSemanticModel.<GetNullableContext>g__getFlag|236_0(nullableContextState.AnnotationsState, nullableContextOptions.AnnotationsEnabled(), NullableContext.AnnotationsContextInherited, NullableContext.AnnotationsEnabled) | CSharpSemanticModel.<GetNullableContext>g__getFlag|236_0(nullableContextState.WarningsState, nullableContextOptions.WarningsEnabled(), NullableContext.WarningsContextInherited, NullableContext.WarningsEnabled);
		}

		// Token: 0x0600157D RID: 5501 RVA: 0x00074646 File Offset: 0x00072846
		[CompilerGenerated]
		internal static ValueTuple<Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol, NullabilityInfo> <GetTypeInfoForNode>g__getTypeAndNullability|88_0(BoundExpression expr)
		{
			return new ValueTuple<Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol, NullabilityInfo>(expr.Type, expr.TopLevelNullability);
		}

		// Token: 0x0600157E RID: 5502 RVA: 0x0007465C File Offset: 0x0007285C
		[CompilerGenerated]
		internal static NullableContext <GetNullableContext>g__getFlag|236_0(NullableContextState.State contextState, bool defaultEnableState, NullableContext inheritedFlag, NullableContext enableFlag)
		{
			NullableContext nullableContext;
			if (contextState != NullableContextState.State.Disabled)
			{
				if (contextState == NullableContextState.State.Enabled)
				{
					nullableContext = enableFlag;
				}
				else if (defaultEnableState)
				{
					nullableContext = inheritedFlag | enableFlag;
				}
				else
				{
					nullableContext = inheritedFlag;
				}
			}
			else
			{
				nullableContext = NullableContext.Disabled;
			}
			return nullableContext;
		}

		// Token: 0x020006B1 RID: 1713
		[Flags]
		internal enum SymbolInfoOptions
		{
			// Token: 0x04002147 RID: 8519
			PreferTypeToConstructors = 1,
			// Token: 0x04002148 RID: 8520
			PreferConstructorsToType = 2,
			// Token: 0x04002149 RID: 8521
			ResolveAliases = 4,
			// Token: 0x0400214A RID: 8522
			PreserveAliases = 8,
			// Token: 0x0400214B RID: 8523
			DefaultOptions = 6
		}
	}
}
