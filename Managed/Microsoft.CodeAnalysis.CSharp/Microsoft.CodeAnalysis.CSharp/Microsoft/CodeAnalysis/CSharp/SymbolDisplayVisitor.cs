using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.SymbolDisplay;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200021C RID: 540
	internal class SymbolDisplayVisitor : AbstractSymbolDisplayVisitor
	{
		// Token: 0x06002770 RID: 10096 RVA: 0x000D6FC7 File Offset: 0x000D51C7
		internal SymbolDisplayVisitor(ArrayBuilder<SymbolDisplayPart> builder, SymbolDisplayFormat format, SemanticModel semanticModelOpt, int positionOpt)
			: base(builder, format, true, semanticModelOpt, positionOpt, false)
		{
			this._escapeKeywordIdentifiers = format.MiscellaneousOptions.IncludesOption(SymbolDisplayMiscellaneousOptions.EscapeKeywordIdentifiers);
		}

		// Token: 0x06002771 RID: 10097 RVA: 0x000D6FE8 File Offset: 0x000D51E8
		private SymbolDisplayVisitor(ArrayBuilder<SymbolDisplayPart> builder, SymbolDisplayFormat format, SemanticModel semanticModelOpt, int positionOpt, bool escapeKeywordIdentifiers, IDictionary<INamespaceOrTypeSymbol, IAliasSymbol> aliasMap, bool isFirstSymbolVisited, bool inNamespaceOrType = false)
			: base(builder, format, isFirstSymbolVisited, semanticModelOpt, positionOpt, inNamespaceOrType)
		{
			this._escapeKeywordIdentifiers = escapeKeywordIdentifiers;
			this._lazyAliasMap = aliasMap;
		}

		// Token: 0x06002772 RID: 10098 RVA: 0x000D7009 File Offset: 0x000D5209
		protected override AbstractSymbolDisplayVisitor MakeNotFirstVisitor(bool inNamespaceOrType = false)
		{
			return new SymbolDisplayVisitor(this.builder, this.format, this.semanticModelOpt, this.positionOpt, this._escapeKeywordIdentifiers, this._lazyAliasMap, false, inNamespaceOrType);
		}

		// Token: 0x06002773 RID: 10099 RVA: 0x000D7036 File Offset: 0x000D5236
		internal SymbolDisplayPart CreatePart(SymbolDisplayPartKind kind, ISymbol symbol, string text)
		{
			text = ((text == null) ? "?" : ((this._escapeKeywordIdentifiers && SymbolDisplayVisitor.IsEscapable(kind)) ? SymbolDisplayVisitor.EscapeIdentifier(text) : text));
			return new SymbolDisplayPart(kind, symbol, text);
		}

		// Token: 0x06002774 RID: 10100 RVA: 0x000D7068 File Offset: 0x000D5268
		private static bool IsEscapable(SymbolDisplayPartKind kind)
		{
			if (kind <= SymbolDisplayPartKind.StructName)
			{
				switch (kind)
				{
				case SymbolDisplayPartKind.AliasName:
				case SymbolDisplayPartKind.ClassName:
				case SymbolDisplayPartKind.DelegateName:
				case SymbolDisplayPartKind.EnumName:
				case SymbolDisplayPartKind.FieldName:
				case SymbolDisplayPartKind.InterfaceName:
				case SymbolDisplayPartKind.LocalName:
				case SymbolDisplayPartKind.MethodName:
				case SymbolDisplayPartKind.NamespaceName:
				case SymbolDisplayPartKind.ParameterName:
				case SymbolDisplayPartKind.PropertyName:
					break;
				case SymbolDisplayPartKind.AssemblyName:
				case SymbolDisplayPartKind.ErrorTypeName:
				case SymbolDisplayPartKind.EventName:
				case SymbolDisplayPartKind.Keyword:
				case SymbolDisplayPartKind.LabelName:
				case SymbolDisplayPartKind.LineBreak:
				case SymbolDisplayPartKind.NumericLiteral:
				case SymbolDisplayPartKind.StringLiteral:
				case SymbolDisplayPartKind.ModuleName:
				case SymbolDisplayPartKind.Operator:
					return false;
				default:
					if (kind != SymbolDisplayPartKind.StructName)
					{
						return false;
					}
					break;
				}
			}
			else if (kind != SymbolDisplayPartKind.TypeParameterName && kind != SymbolDisplayPartKind.RecordClassName)
			{
				return false;
			}
			return true;
		}

		// Token: 0x06002775 RID: 10101 RVA: 0x000D70E8 File Offset: 0x000D52E8
		private static string EscapeIdentifier(string identifier)
		{
			if (SyntaxFacts.GetKeywordKind(identifier) != SyntaxKind.None)
			{
				return "@" + identifier;
			}
			return identifier;
		}

		// Token: 0x06002776 RID: 10102 RVA: 0x000D7100 File Offset: 0x000D5300
		public override void VisitAssembly(IAssemblySymbol symbol)
		{
			string text = ((this.format.TypeQualificationStyle == SymbolDisplayTypeQualificationStyle.NameOnly) ? symbol.Identity.Name : symbol.Identity.GetDisplayName(false));
			this.builder.Add(this.CreatePart(SymbolDisplayPartKind.AssemblyName, symbol, text));
		}

		// Token: 0x06002777 RID: 10103 RVA: 0x000D7148 File Offset: 0x000D5348
		public override void VisitModule(IModuleSymbol symbol)
		{
			this.builder.Add(this.CreatePart(SymbolDisplayPartKind.ModuleName, symbol, symbol.Name));
		}

		// Token: 0x06002778 RID: 10104 RVA: 0x000D7164 File Offset: 0x000D5364
		public override void VisitNamespace(INamespaceSymbol symbol)
		{
			if (base.IsMinimizing)
			{
				if (this.TryAddAlias(symbol, this.builder))
				{
					return;
				}
				this.MinimallyQualify(symbol);
				return;
			}
			else
			{
				if (this.isFirstSymbolVisited && this.format.KindOptions.IncludesOption(SymbolDisplayKindOptions.IncludeNamespaceKeyword))
				{
					this.AddKeyword(SyntaxKind.NamespaceKeyword);
					this.AddSpace();
				}
				if (this.format.TypeQualificationStyle == SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces)
				{
					INamespaceSymbol containingNamespace = symbol.ContainingNamespace;
					if (this.ShouldVisitNamespace(containingNamespace))
					{
						containingNamespace.Accept(base.NotFirstVisitor);
						this.AddPunctuation(containingNamespace.IsGlobalNamespace ? SyntaxKind.ColonColonToken : SyntaxKind.DotToken);
					}
				}
				if (symbol.IsGlobalNamespace)
				{
					this.AddGlobalNamespace(symbol);
					return;
				}
				this.builder.Add(this.CreatePart(SymbolDisplayPartKind.NamespaceName, symbol, symbol.Name));
				return;
			}
		}

		// Token: 0x06002779 RID: 10105 RVA: 0x000D722C File Offset: 0x000D542C
		private void AddGlobalNamespace(INamespaceSymbol globalNamespace)
		{
			switch (this.format.GlobalNamespaceStyle)
			{
			case SymbolDisplayGlobalNamespaceStyle.Omitted:
				return;
			case SymbolDisplayGlobalNamespaceStyle.OmittedAsContaining:
				this.builder.Add(this.CreatePart(SymbolDisplayPartKind.Text, globalNamespace, "<global namespace>"));
				return;
			case SymbolDisplayGlobalNamespaceStyle.Included:
				if (this.isFirstSymbolVisited)
				{
					this.builder.Add(this.CreatePart(SymbolDisplayPartKind.Text, globalNamespace, "<global namespace>"));
					return;
				}
				this.builder.Add(this.CreatePart(SymbolDisplayPartKind.Keyword, globalNamespace, SyntaxFacts.GetText(SyntaxKind.GlobalKeyword)));
				return;
			default:
				throw ExceptionUtilities.UnexpectedValue(this.format.GlobalNamespaceStyle);
			}
		}

		// Token: 0x0600277A RID: 10106 RVA: 0x000D72CC File Offset: 0x000D54CC
		public override void VisitLocal(ILocalSymbol symbol)
		{
			if (symbol.IsRef && this.format.LocalOptions.IncludesOption(SymbolDisplayLocalOptions.IncludeRef))
			{
				this.AddKeyword(SyntaxKind.RefKeyword);
				this.AddSpace();
				if (symbol.RefKind == RefKind.In)
				{
					this.AddKeyword(SyntaxKind.ReadOnlyKeyword);
					this.AddSpace();
				}
			}
			if (this.format.LocalOptions.IncludesOption(SymbolDisplayLocalOptions.IncludeType))
			{
				symbol.Type.Accept(base.NotFirstVisitor);
				this.AddSpace();
			}
			if (symbol.IsConst)
			{
				this.builder.Add(this.CreatePart(SymbolDisplayPartKind.ConstantName, symbol, symbol.Name));
			}
			else
			{
				this.builder.Add(this.CreatePart(SymbolDisplayPartKind.LocalName, symbol, symbol.Name));
			}
			if (this.format.LocalOptions.IncludesOption(SymbolDisplayLocalOptions.IncludeConstantValue) && symbol.IsConst && symbol.HasConstantValue && SymbolDisplayVisitor.CanAddConstant(symbol.Type, symbol.ConstantValue))
			{
				this.AddSpace();
				this.AddPunctuation(SyntaxKind.EqualsToken);
				this.AddSpace();
				this.AddConstantValue(symbol.Type, symbol.ConstantValue, false);
			}
		}

		// Token: 0x0600277B RID: 10107 RVA: 0x000D73E8 File Offset: 0x000D55E8
		public override void VisitDiscard(IDiscardSymbol symbol)
		{
			if (this.format.LocalOptions.IncludesOption(SymbolDisplayLocalOptions.IncludeType))
			{
				symbol.Type.Accept(base.NotFirstVisitor);
				this.AddSpace();
			}
			this.builder.Add(this.CreatePart(SymbolDisplayPartKind.Punctuation, symbol, "_"));
		}

		// Token: 0x0600277C RID: 10108 RVA: 0x000D7438 File Offset: 0x000D5638
		public override void VisitRangeVariable(IRangeVariableSymbol symbol)
		{
			if (this.format.LocalOptions.IncludesOption(SymbolDisplayLocalOptions.IncludeType))
			{
				ITypeSymbol rangeVariableType = this.GetRangeVariableType(symbol);
				if (rangeVariableType != null && rangeVariableType.TypeKind != TypeKind.Error)
				{
					rangeVariableType.Accept(this);
				}
				else
				{
					this.builder.Add(this.CreatePart(SymbolDisplayPartKind.ErrorTypeName, rangeVariableType, "?"));
				}
				this.AddSpace();
			}
			this.builder.Add(this.CreatePart(SymbolDisplayPartKind.RangeVariableName, symbol, symbol.Name));
		}

		// Token: 0x0600277D RID: 10109 RVA: 0x000D74AD File Offset: 0x000D56AD
		public override void VisitLabel(ILabelSymbol symbol)
		{
			this.builder.Add(this.CreatePart(SymbolDisplayPartKind.LabelName, symbol, symbol.Name));
		}

		// Token: 0x0600277E RID: 10110 RVA: 0x000D74CC File Offset: 0x000D56CC
		public override void VisitAlias(IAliasSymbol symbol)
		{
			this.builder.Add(this.CreatePart(SymbolDisplayPartKind.AliasName, symbol, symbol.Name));
			if (this.format.LocalOptions.IncludesOption(SymbolDisplayLocalOptions.IncludeType))
			{
				this.AddPunctuation(SyntaxKind.EqualsToken);
				symbol.Target.Accept(this);
			}
		}

		// Token: 0x0600277F RID: 10111 RVA: 0x000D751C File Offset: 0x000D571C
		protected override void AddSpace()
		{
			this.builder.Add(this.CreatePart(SymbolDisplayPartKind.Space, null, " "));
		}

		// Token: 0x06002780 RID: 10112 RVA: 0x000D7537 File Offset: 0x000D5737
		private void AddPunctuation(SyntaxKind punctuationKind)
		{
			this.builder.Add(this.CreatePart(SymbolDisplayPartKind.Punctuation, null, SyntaxFacts.GetText(punctuationKind)));
		}

		// Token: 0x06002781 RID: 10113 RVA: 0x000D7553 File Offset: 0x000D5753
		private void AddKeyword(SyntaxKind keywordKind)
		{
			this.builder.Add(this.CreatePart(SymbolDisplayPartKind.Keyword, null, SyntaxFacts.GetText(keywordKind)));
		}

		// Token: 0x06002782 RID: 10114 RVA: 0x000D7570 File Offset: 0x000D5770
		private void AddAccessibilityIfRequired(ISymbol symbol)
		{
			INamedTypeSymbol containingType = symbol.ContainingType;
			if (this.format.MemberOptions.IncludesOption(SymbolDisplayMemberOptions.IncludeAccessibility) && (containingType == null || (containingType.TypeKind != TypeKind.Interface && (!SymbolDisplayVisitor.IsEnumMember(symbol) & !SymbolDisplayVisitor.IsLocalFunction(symbol)))))
			{
				this.AddAccessibility(symbol);
			}
		}

		// Token: 0x06002783 RID: 10115 RVA: 0x000D75BF File Offset: 0x000D57BF
		private static bool IsLocalFunction(ISymbol symbol)
		{
			return symbol.Kind == SymbolKind.Method && ((IMethodSymbol)symbol).MethodKind == MethodKind.LocalFunction;
		}

		// Token: 0x06002784 RID: 10116 RVA: 0x000D75DC File Offset: 0x000D57DC
		private void AddAccessibility(ISymbol symbol)
		{
			switch (symbol.DeclaredAccessibility)
			{
			case Accessibility.Private:
				this.AddKeyword(SyntaxKind.PrivateKeyword);
				break;
			case Accessibility.ProtectedAndInternal:
				this.AddKeyword(SyntaxKind.PrivateKeyword);
				this.AddSpace();
				this.AddKeyword(SyntaxKind.ProtectedKeyword);
				break;
			case Accessibility.Protected:
				this.AddKeyword(SyntaxKind.ProtectedKeyword);
				break;
			case Accessibility.Internal:
				this.AddKeyword(SyntaxKind.InternalKeyword);
				break;
			case Accessibility.ProtectedOrInternal:
				this.AddKeyword(SyntaxKind.ProtectedKeyword);
				this.AddSpace();
				this.AddKeyword(SyntaxKind.InternalKeyword);
				break;
			case Accessibility.Public:
				this.AddKeyword(SyntaxKind.PublicKeyword);
				break;
			default:
				throw ExceptionUtilities.UnexpectedValue(symbol.DeclaredAccessibility);
			}
			this.AddSpace();
		}

		// Token: 0x06002785 RID: 10117 RVA: 0x000D769C File Offset: 0x000D589C
		private bool ShouldVisitNamespace(ISymbol containingSymbol)
		{
			INamespaceSymbol namespaceSymbol = containingSymbol as INamespaceSymbol;
			return namespaceSymbol != null && this.format.TypeQualificationStyle == SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces && (!namespaceSymbol.IsGlobalNamespace || this.format.GlobalNamespaceStyle == SymbolDisplayGlobalNamespaceStyle.Included);
		}

		// Token: 0x06002786 RID: 10118 RVA: 0x000D76E0 File Offset: 0x000D58E0
		private bool IncludeNamedType(INamedTypeSymbol namedType)
		{
			if (namedType == null)
			{
				return false;
			}
			if (namedType.IsScriptClass && !this.format.CompilerInternalOptions.IncludesOption(SymbolDisplayCompilerInternalOptions.IncludeScriptType))
			{
				return false;
			}
			SemanticModel semanticModelOpt = this.semanticModelOpt;
			return namedType != ((semanticModelOpt != null) ? semanticModelOpt.Compilation.ScriptGlobalsType : null);
		}

		// Token: 0x06002787 RID: 10119 RVA: 0x000D772C File Offset: 0x000D592C
		private static bool IsEnumMember(ISymbol symbol)
		{
			return symbol != null && symbol.Kind == SymbolKind.Field && symbol.ContainingType != null && symbol.ContainingType.TypeKind == TypeKind.Enum && symbol.Name != "value__";
		}

		// Token: 0x06002788 RID: 10120 RVA: 0x000D7762 File Offset: 0x000D5962
		private void VisitFieldType(IFieldSymbol symbol)
		{
			symbol.Type.Accept(base.NotFirstVisitor);
		}

		// Token: 0x06002789 RID: 10121 RVA: 0x000D7778 File Offset: 0x000D5978
		public override void VisitField(IFieldSymbol symbol)
		{
			this.AddAccessibilityIfRequired(symbol);
			this.AddMemberModifiersIfRequired(symbol);
			this.AddFieldModifiersIfRequired(symbol);
			if (this.format.MemberOptions.IncludesOption(SymbolDisplayMemberOptions.IncludeType) && this.isFirstSymbolVisited && !SymbolDisplayVisitor.IsEnumMember(symbol))
			{
				this.VisitFieldType(symbol);
				this.AddSpace();
				this.AddCustomModifiersIfRequired(symbol.CustomModifiers, false, true);
			}
			if (this.format.MemberOptions.IncludesOption(SymbolDisplayMemberOptions.IncludeContainingType) && this.IncludeNamedType(symbol.ContainingType))
			{
				symbol.ContainingType.Accept(base.NotFirstVisitor);
				this.AddPunctuation(SyntaxKind.DotToken);
			}
			if (symbol.ContainingType.TypeKind == TypeKind.Enum)
			{
				this.builder.Add(this.CreatePart(SymbolDisplayPartKind.EnumMemberName, symbol, symbol.Name));
			}
			else if (symbol.IsConst)
			{
				this.builder.Add(this.CreatePart(SymbolDisplayPartKind.ConstantName, symbol, symbol.Name));
			}
			else
			{
				this.builder.Add(this.CreatePart(SymbolDisplayPartKind.FieldName, symbol, symbol.Name));
			}
			if (this.isFirstSymbolVisited && this.format.MemberOptions.IncludesOption(SymbolDisplayMemberOptions.IncludeConstantValue) && symbol.IsConst && symbol.HasConstantValue && SymbolDisplayVisitor.CanAddConstant(symbol.Type, symbol.ConstantValue))
			{
				this.AddSpace();
				this.AddPunctuation(SyntaxKind.EqualsToken);
				this.AddSpace();
				this.AddConstantValue(symbol.Type, symbol.ConstantValue, SymbolDisplayVisitor.IsEnumMember(symbol));
			}
		}

		// Token: 0x0600278A RID: 10122 RVA: 0x000D78EC File Offset: 0x000D5AEC
		private static bool ShouldPropertyDisplayReadOnly(IPropertySymbol property)
		{
			INamedTypeSymbol containingType = property.ContainingType;
			if (containingType != null && containingType.IsReadOnly)
			{
				return false;
			}
			IMethodSymbol getMethod = property.GetMethod;
			if (getMethod != null && !SymbolDisplayVisitor.ShouldMethodDisplayReadOnly(getMethod, property))
			{
				return false;
			}
			IMethodSymbol setMethod = property.SetMethod;
			return (setMethod == null || SymbolDisplayVisitor.ShouldMethodDisplayReadOnly(setMethod, property)) && (getMethod != null || setMethod != null);
		}

		// Token: 0x0600278B RID: 10123 RVA: 0x000D7944 File Offset: 0x000D5B44
		private static bool ShouldMethodDisplayReadOnly(IMethodSymbol method, IPropertySymbol propertyOpt = null)
		{
			INamedTypeSymbol containingType = method.ContainingType;
			if (containingType != null && containingType.IsReadOnly)
			{
				return false;
			}
			Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel.MethodSymbol methodSymbol = method as Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel.MethodSymbol;
			SourcePropertyAccessorSymbol sourcePropertyAccessorSymbol = ((methodSymbol != null) ? methodSymbol.UnderlyingMethodSymbol : null) as SourcePropertyAccessorSymbol;
			if (sourcePropertyAccessorSymbol != null)
			{
				Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel.PropertySymbol propertySymbol = propertyOpt as Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel.PropertySymbol;
				SourcePropertySymbolBase sourcePropertySymbolBase = ((propertySymbol != null) ? propertySymbol.UnderlyingSymbol : null) as SourcePropertySymbolBase;
				if (sourcePropertySymbolBase != null)
				{
					return sourcePropertyAccessorSymbol.LocalDeclaredReadOnly || sourcePropertySymbolBase.HasReadOnlyModifier;
				}
			}
			Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel.MethodSymbol methodSymbol2 = method as Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel.MethodSymbol;
			return methodSymbol2 != null && methodSymbol2.UnderlyingMethodSymbol.IsDeclaredReadOnly;
		}

		// Token: 0x0600278C RID: 10124 RVA: 0x000D79C8 File Offset: 0x000D5BC8
		public override void VisitProperty(IPropertySymbol symbol)
		{
			this.AddAccessibilityIfRequired(symbol);
			this.AddMemberModifiersIfRequired(symbol);
			if (SymbolDisplayVisitor.ShouldPropertyDisplayReadOnly(symbol))
			{
				this.AddReadOnlyIfRequired();
			}
			if (this.format.MemberOptions.IncludesOption(SymbolDisplayMemberOptions.IncludeType))
			{
				if (symbol.ReturnsByRef)
				{
					this.AddRefIfRequired();
				}
				else if (symbol.ReturnsByRefReadonly)
				{
					this.AddRefReadonlyIfRequired();
				}
				this.AddCustomModifiersIfRequired(symbol.RefCustomModifiers, false, true);
				symbol.Type.Accept(base.NotFirstVisitor);
				this.AddSpace();
				this.AddCustomModifiersIfRequired(symbol.TypeCustomModifiers, false, true);
			}
			if (this.format.MemberOptions.IncludesOption(SymbolDisplayMemberOptions.IncludeContainingType) && this.IncludeNamedType(symbol.ContainingType))
			{
				symbol.ContainingType.Accept(base.NotFirstVisitor);
				this.AddPunctuation(SyntaxKind.DotToken);
			}
			this.AddPropertyNameAndParameters(symbol);
			if (this.format.PropertyStyle == SymbolDisplayPropertyStyle.ShowReadWriteDescriptor)
			{
				this.AddSpace();
				this.AddPunctuation(SyntaxKind.OpenBraceToken);
				this.AddAccessor(symbol, symbol.GetMethod, SyntaxKind.GetKeyword);
				SyntaxKind syntaxKind = (SymbolDisplayVisitor.IsInitOnly(symbol.SetMethod) ? SyntaxKind.InitKeyword : SyntaxKind.SetKeyword);
				this.AddAccessor(symbol, symbol.SetMethod, syntaxKind);
				this.AddSpace();
				this.AddPunctuation(SyntaxKind.CloseBraceToken);
			}
		}

		// Token: 0x0600278D RID: 10125 RVA: 0x000D7B04 File Offset: 0x000D5D04
		private static bool IsInitOnly(IMethodSymbol symbol)
		{
			return symbol != null && symbol.IsInitOnly;
		}

		// Token: 0x0600278E RID: 10126 RVA: 0x000D7B14 File Offset: 0x000D5D14
		private void AddPropertyNameAndParameters(IPropertySymbol symbol)
		{
			bool flag = symbol.Name.LastIndexOf('.') > 0;
			if (flag)
			{
				this.AddExplicitInterfaceIfRequired<IPropertySymbol>(symbol.ExplicitInterfaceImplementations);
			}
			if (symbol.IsIndexer)
			{
				this.AddKeyword(SyntaxKind.ThisKeyword);
			}
			else if (flag)
			{
				this.builder.Add(this.CreatePart(SymbolDisplayPartKind.PropertyName, symbol, ExplicitInterfaceHelpers.GetMemberNameWithoutInterfaceName(symbol.Name)));
			}
			else
			{
				this.builder.Add(this.CreatePart(SymbolDisplayPartKind.PropertyName, symbol, symbol.Name));
			}
			if (this.format.MemberOptions.IncludesOption(SymbolDisplayMemberOptions.IncludeParameters) && symbol.Parameters.Any<IParameterSymbol>())
			{
				this.AddPunctuation(SyntaxKind.OpenBracketToken);
				this.AddParametersIfRequired(false, false, symbol.Parameters);
				this.AddPunctuation(SyntaxKind.CloseBracketToken);
			}
		}

		// Token: 0x0600278F RID: 10127 RVA: 0x000D7BDC File Offset: 0x000D5DDC
		public override void VisitEvent(IEventSymbol symbol)
		{
			this.AddAccessibilityIfRequired(symbol);
			this.AddMemberModifiersIfRequired(symbol);
			IMethodSymbol methodSymbol = symbol.AddMethod ?? symbol.RemoveMethod;
			if (methodSymbol != null && SymbolDisplayVisitor.ShouldMethodDisplayReadOnly(methodSymbol, null))
			{
				this.AddReadOnlyIfRequired();
			}
			if (this.format.KindOptions.IncludesOption(SymbolDisplayKindOptions.IncludeMemberKeyword))
			{
				this.AddKeyword(SyntaxKind.EventKeyword);
				this.AddSpace();
			}
			if (this.format.MemberOptions.IncludesOption(SymbolDisplayMemberOptions.IncludeType))
			{
				symbol.Type.Accept(base.NotFirstVisitor);
				this.AddSpace();
			}
			if (this.format.MemberOptions.IncludesOption(SymbolDisplayMemberOptions.IncludeContainingType) && this.IncludeNamedType(symbol.ContainingType))
			{
				symbol.ContainingType.Accept(base.NotFirstVisitor);
				this.AddPunctuation(SyntaxKind.DotToken);
			}
			this.AddEventName(symbol);
		}

		// Token: 0x06002790 RID: 10128 RVA: 0x000D7CB0 File Offset: 0x000D5EB0
		private void AddEventName(IEventSymbol symbol)
		{
			if (symbol.Name.LastIndexOf('.') > 0)
			{
				this.AddExplicitInterfaceIfRequired<IEventSymbol>(symbol.ExplicitInterfaceImplementations);
				this.builder.Add(this.CreatePart(SymbolDisplayPartKind.EventName, symbol, ExplicitInterfaceHelpers.GetMemberNameWithoutInterfaceName(symbol.Name)));
				return;
			}
			this.builder.Add(this.CreatePart(SymbolDisplayPartKind.EventName, symbol, symbol.Name));
		}

		// Token: 0x06002791 RID: 10129 RVA: 0x000D7D14 File Offset: 0x000D5F14
		public override void VisitMethod(IMethodSymbol symbol)
		{
			if (symbol.MethodKind == MethodKind.AnonymousFunction)
			{
				this.builder.Add(this.CreatePart(SymbolDisplayPartKind.NumericLiteral, symbol, "lambda expression"));
				return;
			}
			Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel.MethodSymbol methodSymbol = symbol as Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel.MethodSymbol;
			if (((methodSymbol != null) ? methodSymbol.UnderlyingMethodSymbol : null) is SynthesizedGlobalMethodSymbol)
			{
				this.builder.Add(this.CreatePart(SymbolDisplayPartKind.NumericLiteral, symbol, symbol.Name));
				return;
			}
			if (symbol.MethodKind == MethodKind.FunctionPointerSignature)
			{
				this.<VisitMethod>g__visitFunctionPointerSignature|37_0(symbol);
				return;
			}
			if (symbol.IsExtensionMethod && this.format.ExtensionMethodStyle != SymbolDisplayExtensionMethodStyle.Default)
			{
				if (symbol.MethodKind == MethodKind.ReducedExtension && this.format.ExtensionMethodStyle == SymbolDisplayExtensionMethodStyle.StaticMethod)
				{
					symbol = symbol.GetConstructedReducedFrom();
				}
				else if (symbol.MethodKind != MethodKind.ReducedExtension && this.format.ExtensionMethodStyle == SymbolDisplayExtensionMethodStyle.InstanceMethod)
				{
					symbol = symbol.ReduceExtensionMethod(symbol.Parameters.First<IParameterSymbol>().Type) ?? symbol;
				}
			}
			if (symbol.ContainingType != null || symbol.ContainingSymbol is ITypeSymbol)
			{
				this.AddAccessibilityIfRequired(symbol);
				this.AddMemberModifiersIfRequired(symbol);
				if (SymbolDisplayVisitor.ShouldMethodDisplayReadOnly(symbol, null))
				{
					this.AddReadOnlyIfRequired();
				}
				if (this.format.MemberOptions.IncludesOption(SymbolDisplayMemberOptions.IncludeType))
				{
					MethodKind methodKind = symbol.MethodKind;
					switch (methodKind)
					{
					case MethodKind.Constructor:
						goto IL_01BC;
					case MethodKind.Conversion:
					case MethodKind.Destructor:
						if (!this.format.CompilerInternalOptions.IncludesOption(SymbolDisplayCompilerInternalOptions.UseMetadataMethodNames))
						{
							goto IL_01BC;
						}
						break;
					case MethodKind.DelegateInvoke:
						break;
					default:
						if (methodKind == MethodKind.StaticConstructor)
						{
							goto IL_01BC;
						}
						break;
					}
					if (symbol.ReturnsByRef)
					{
						this.AddRefIfRequired();
					}
					else if (symbol.ReturnsByRefReadonly)
					{
						this.AddRefReadonlyIfRequired();
					}
					this.AddCustomModifiersIfRequired(symbol.RefCustomModifiers, false, true);
					if (symbol.ReturnsVoid)
					{
						this.AddKeyword(SyntaxKind.VoidKeyword);
					}
					else if (symbol.ReturnType != null)
					{
						this.AddReturnType(symbol);
					}
					this.AddSpace();
					this.AddCustomModifiersIfRequired(symbol.ReturnTypeCustomModifiers, false, true);
				}
				IL_01BC:
				if (this.format.MemberOptions.IncludesOption(SymbolDisplayMemberOptions.IncludeContainingType))
				{
					bool flag;
					ITypeSymbol typeSymbol;
					if (symbol.MethodKind == MethodKind.LocalFunction)
					{
						flag = false;
						typeSymbol = null;
					}
					else if (symbol.MethodKind == MethodKind.ReducedExtension)
					{
						typeSymbol = symbol.ReceiverType;
						flag = true;
					}
					else
					{
						typeSymbol = symbol.ContainingType;
						if (typeSymbol != null)
						{
							flag = this.IncludeNamedType(symbol.ContainingType);
						}
						else
						{
							typeSymbol = (ITypeSymbol)symbol.ContainingSymbol;
							flag = true;
						}
					}
					if (flag)
					{
						typeSymbol.Accept(base.NotFirstVisitor);
						this.AddPunctuation(SyntaxKind.DotToken);
					}
				}
			}
			bool flag2 = false;
			switch (symbol.MethodKind)
			{
			case MethodKind.Constructor:
			case MethodKind.StaticConstructor:
			{
				string text = ((this.format.CompilerInternalOptions.IncludesOption(SymbolDisplayCompilerInternalOptions.UseMetadataMethodNames) || symbol.ContainingType == null || symbol.ContainingType.IsAnonymousType) ? symbol.Name : symbol.ContainingType.Name);
				SymbolDisplayPartKind partKindForConstructorOrDestructor = SymbolDisplayVisitor.GetPartKindForConstructorOrDestructor(symbol);
				this.builder.Add(this.CreatePart(partKindForConstructorOrDestructor, symbol, text));
				goto IL_05E3;
			}
			case MethodKind.Conversion:
				if (this.format.CompilerInternalOptions.IncludesOption(SymbolDisplayCompilerInternalOptions.UseMetadataMethodNames))
				{
					this.builder.Add(this.CreatePart(SymbolDisplayPartKind.MethodName, symbol, symbol.MetadataName));
					goto IL_05E3;
				}
				if (symbol.MetadataName == "op_Explicit")
				{
					this.AddKeyword(SyntaxKind.ExplicitKeyword);
				}
				else if (symbol.MetadataName == "op_Implicit")
				{
					this.AddKeyword(SyntaxKind.ImplicitKeyword);
				}
				else
				{
					this.builder.Add(this.CreatePart(SymbolDisplayPartKind.MethodName, symbol, SyntaxFacts.GetText(SyntaxFacts.GetOperatorKind(symbol.MetadataName))));
				}
				this.AddSpace();
				this.AddKeyword(SyntaxKind.OperatorKeyword);
				this.AddSpace();
				this.AddReturnType(symbol);
				goto IL_05E3;
			case MethodKind.DelegateInvoke:
			case MethodKind.Ordinary:
			case MethodKind.LocalFunction:
				break;
			case MethodKind.Destructor:
			{
				SymbolDisplayPartKind partKindForConstructorOrDestructor2 = SymbolDisplayVisitor.GetPartKindForConstructorOrDestructor(symbol);
				if (this.format.CompilerInternalOptions.IncludesOption(SymbolDisplayCompilerInternalOptions.UseMetadataMethodNames) || symbol.ContainingType == null)
				{
					this.builder.Add(this.CreatePart(partKindForConstructorOrDestructor2, symbol, symbol.Name));
					goto IL_05E3;
				}
				this.AddPunctuation(SyntaxKind.TildeToken);
				this.builder.Add(this.CreatePart(partKindForConstructorOrDestructor2, symbol, symbol.ContainingType.Name));
				goto IL_05E3;
			}
			case MethodKind.EventAdd:
			case MethodKind.EventRemove:
			{
				flag2 = true;
				IEventSymbol eventSymbol = (IEventSymbol)symbol.AssociatedSymbol;
				if (eventSymbol != null)
				{
					this.AddEventName(eventSymbol);
					this.AddPunctuation(SyntaxKind.DotToken);
					this.AddKeyword((symbol.MethodKind == MethodKind.EventAdd) ? SyntaxKind.AddKeyword : SyntaxKind.RemoveKeyword);
					goto IL_05E3;
				}
				break;
			}
			case MethodKind.EventRaise:
			case MethodKind.DeclareMethod:
				goto IL_05D2;
			case MethodKind.ExplicitInterfaceImplementation:
				this.AddExplicitInterfaceIfRequired<IMethodSymbol>(symbol.ExplicitInterfaceImplementations);
				this.builder.Add(this.CreatePart(SymbolDisplayPartKind.MethodName, symbol, ExplicitInterfaceHelpers.GetMemberNameWithoutInterfaceName(symbol.Name)));
				goto IL_05E3;
			case MethodKind.UserDefinedOperator:
			case MethodKind.BuiltinOperator:
				if (this.format.CompilerInternalOptions.IncludesOption(SymbolDisplayCompilerInternalOptions.UseMetadataMethodNames))
				{
					this.builder.Add(this.CreatePart(SymbolDisplayPartKind.MethodName, symbol, symbol.MetadataName));
					goto IL_05E3;
				}
				this.AddKeyword(SyntaxKind.OperatorKeyword);
				this.AddSpace();
				if (symbol.MetadataName == "op_True")
				{
					this.AddKeyword(SyntaxKind.TrueKeyword);
					goto IL_05E3;
				}
				if (symbol.MetadataName == "op_False")
				{
					this.AddKeyword(SyntaxKind.FalseKeyword);
					goto IL_05E3;
				}
				this.builder.Add(this.CreatePart(SymbolDisplayPartKind.MethodName, symbol, SyntaxFacts.GetText(SyntaxFacts.GetOperatorKind(symbol.MetadataName))));
				goto IL_05E3;
			case MethodKind.PropertyGet:
			case MethodKind.PropertySet:
			{
				flag2 = true;
				IPropertySymbol propertySymbol = (IPropertySymbol)symbol.AssociatedSymbol;
				if (propertySymbol != null)
				{
					this.AddPropertyNameAndParameters(propertySymbol);
					this.AddPunctuation(SyntaxKind.DotToken);
					this.AddKeyword((symbol.MethodKind == MethodKind.PropertyGet) ? SyntaxKind.GetKeyword : (SymbolDisplayVisitor.IsInitOnly(symbol) ? SyntaxKind.InitKeyword : SyntaxKind.SetKeyword));
					goto IL_05E3;
				}
				break;
			}
			case MethodKind.ReducedExtension:
				this.builder.Add(this.CreatePart(SymbolDisplayPartKind.ExtensionMethodName, symbol, symbol.Name));
				goto IL_05E3;
			default:
				goto IL_05D2;
			}
			this.builder.Add(this.CreatePart(SymbolDisplayPartKind.MethodName, symbol, symbol.Name));
			goto IL_05E3;
			IL_05D2:
			throw ExceptionUtilities.UnexpectedValue(symbol.MethodKind);
			IL_05E3:
			if (!flag2)
			{
				this.AddTypeArguments(symbol, default(ImmutableArray<ImmutableArray<CustomModifier>>));
				this.AddParameters(symbol);
				this.AddTypeParameterConstraints(symbol);
			}
		}

		// Token: 0x06002792 RID: 10130 RVA: 0x000D8326 File Offset: 0x000D6526
		private static SymbolDisplayPartKind GetPartKindForConstructorOrDestructor(IMethodSymbol symbol)
		{
			if (symbol.ContainingType == null)
			{
				return SymbolDisplayPartKind.MethodName;
			}
			return SymbolDisplayVisitor.GetPartKind(symbol.ContainingType);
		}

		// Token: 0x06002793 RID: 10131 RVA: 0x000D833E File Offset: 0x000D653E
		private void AddReturnType(IMethodSymbol symbol)
		{
			symbol.ReturnType.Accept(base.NotFirstVisitor);
		}

		// Token: 0x06002794 RID: 10132 RVA: 0x000D8351 File Offset: 0x000D6551
		private void AddTypeParameterConstraints(IMethodSymbol symbol)
		{
			if (this.format.GenericsOptions.IncludesOption(SymbolDisplayGenericsOptions.IncludeTypeConstraints))
			{
				this.AddTypeParameterConstraints(symbol.TypeArguments);
			}
		}

		// Token: 0x06002795 RID: 10133 RVA: 0x000D8374 File Offset: 0x000D6574
		private void AddParameters(IMethodSymbol symbol)
		{
			if (this.format.MemberOptions.IncludesOption(SymbolDisplayMemberOptions.IncludeParameters))
			{
				this.AddPunctuation(SyntaxKind.OpenParenToken);
				this.AddParametersIfRequired(symbol.IsExtensionMethod && symbol.MethodKind != MethodKind.ReducedExtension, symbol.IsVararg, symbol.Parameters);
				this.AddPunctuation(SyntaxKind.CloseParenToken);
			}
		}

		// Token: 0x06002796 RID: 10134 RVA: 0x000D83D8 File Offset: 0x000D65D8
		public override void VisitParameter(IParameterSymbol symbol)
		{
			bool flag = this.format.ParameterOptions.IncludesOption(SymbolDisplayParameterOptions.IncludeType);
			bool flag2 = this.format.ParameterOptions.IncludesOption(SymbolDisplayParameterOptions.IncludeName) && symbol.Name.Length != 0;
			bool flag3 = this.format.ParameterOptions.IncludesOption(SymbolDisplayParameterOptions.IncludeOptionalBrackets);
			bool flag4 = this.format.ParameterOptions.IncludesOption(SymbolDisplayParameterOptions.IncludeDefaultValue) && this.format.ParameterOptions.IncludesOption(SymbolDisplayParameterOptions.IncludeName) && symbol.HasExplicitDefaultValue && SymbolDisplayVisitor.CanAddConstant(symbol.Type, symbol.ExplicitDefaultValue);
			if (flag3 && symbol.IsOptional)
			{
				this.AddPunctuation(SyntaxKind.OpenBracketToken);
			}
			if (flag)
			{
				this.AddParameterRefKindIfRequired(symbol.RefKind);
				this.AddCustomModifiersIfRequired(symbol.RefCustomModifiers, false, true);
				if (symbol.IsParams && this.format.ParameterOptions.IncludesOption(SymbolDisplayParameterOptions.IncludeParamsRefOut))
				{
					this.AddKeyword(SyntaxKind.ParamsKeyword);
					this.AddSpace();
				}
				symbol.Type.Accept(base.NotFirstVisitor);
				this.AddCustomModifiersIfRequired(symbol.CustomModifiers, true, false);
			}
			if (flag2)
			{
				if (flag)
				{
					this.AddSpace();
				}
				SymbolDisplayPartKind symbolDisplayPartKind = (symbol.IsThis ? SymbolDisplayPartKind.Keyword : SymbolDisplayPartKind.ParameterName);
				this.builder.Add(this.CreatePart(symbolDisplayPartKind, symbol, symbol.Name));
			}
			if (flag4)
			{
				if (flag2 || flag)
				{
					this.AddSpace();
				}
				this.AddPunctuation(SyntaxKind.EqualsToken);
				this.AddSpace();
				this.AddConstantValue(symbol.Type, symbol.ExplicitDefaultValue, false);
			}
			if (flag3 && symbol.IsOptional)
			{
				this.AddPunctuation(SyntaxKind.CloseBracketToken);
			}
		}

		// Token: 0x06002797 RID: 10135 RVA: 0x000D856B File Offset: 0x000D676B
		private static bool CanAddConstant(ITypeSymbol type, object value)
		{
			return type.TypeKind == TypeKind.Enum || value == null || IntrospectionExtensions.GetTypeInfo(value.GetType()).IsPrimitive || value is string || value is decimal;
		}

		// Token: 0x06002798 RID: 10136 RVA: 0x000D85A4 File Offset: 0x000D67A4
		private void AddFieldModifiersIfRequired(IFieldSymbol symbol)
		{
			if (this.format.MemberOptions.IncludesOption(SymbolDisplayMemberOptions.IncludeModifiers) && !SymbolDisplayVisitor.IsEnumMember(symbol))
			{
				if (symbol.IsConst)
				{
					this.AddKeyword(SyntaxKind.ConstKeyword);
					this.AddSpace();
				}
				if (symbol.IsReadOnly)
				{
					this.AddKeyword(SyntaxKind.ReadOnlyKeyword);
					this.AddSpace();
				}
				if (symbol.IsVolatile)
				{
					this.AddKeyword(SyntaxKind.VolatileKeyword);
					this.AddSpace();
				}
			}
		}

		// Token: 0x06002799 RID: 10137 RVA: 0x000D8618 File Offset: 0x000D6818
		private void AddMemberModifiersIfRequired(ISymbol symbol)
		{
			INamedTypeSymbol containingType = symbol.ContainingType;
			if (this.format.MemberOptions.IncludesOption(SymbolDisplayMemberOptions.IncludeModifiers) && (containingType == null || (containingType.TypeKind != TypeKind.Interface && !SymbolDisplayVisitor.IsEnumMember(symbol) && !SymbolDisplayVisitor.IsLocalFunction(symbol))))
			{
				bool flag = symbol is IFieldSymbol && ((IFieldSymbol)symbol).IsConst;
				if (symbol.IsStatic && !flag)
				{
					this.AddKeyword(SyntaxKind.StaticKeyword);
					this.AddSpace();
				}
				if (symbol.IsOverride)
				{
					this.AddKeyword(SyntaxKind.OverrideKeyword);
					this.AddSpace();
				}
				if (symbol.IsAbstract)
				{
					this.AddKeyword(SyntaxKind.AbstractKeyword);
					this.AddSpace();
				}
				if (symbol.IsSealed)
				{
					this.AddKeyword(SyntaxKind.SealedKeyword);
					this.AddSpace();
				}
				if (symbol.IsExtern)
				{
					this.AddKeyword(SyntaxKind.ExternKeyword);
					this.AddSpace();
				}
				if (symbol.IsVirtual)
				{
					this.AddKeyword(SyntaxKind.VirtualKeyword);
					this.AddSpace();
				}
			}
		}

		// Token: 0x0600279A RID: 10138 RVA: 0x000D8718 File Offset: 0x000D6918
		private void AddParametersIfRequired(bool hasThisParameter, bool isVarargs, ImmutableArray<IParameterSymbol> parameters)
		{
			if (this.format.ParameterOptions == SymbolDisplayParameterOptions.None)
			{
				return;
			}
			bool flag = true;
			if (!parameters.IsDefault)
			{
				foreach (ISymbol symbol in parameters)
				{
					if (!flag)
					{
						this.AddPunctuation(SyntaxKind.CommaToken);
						this.AddSpace();
					}
					else if (hasThisParameter && this.format.ParameterOptions.IncludesOption(SymbolDisplayParameterOptions.IncludeExtensionThis))
					{
						this.AddKeyword(SyntaxKind.ThisKeyword);
						this.AddSpace();
					}
					flag = false;
					symbol.Accept(base.NotFirstVisitor);
				}
			}
			if (isVarargs)
			{
				if (!flag)
				{
					this.AddPunctuation(SyntaxKind.CommaToken);
					this.AddSpace();
				}
				this.AddKeyword(SyntaxKind.ArgListKeyword);
			}
		}

		// Token: 0x0600279B RID: 10139 RVA: 0x000D87C4 File Offset: 0x000D69C4
		private void AddAccessor(IPropertySymbol property, IMethodSymbol method, SyntaxKind keyword)
		{
			if (method != null)
			{
				this.AddSpace();
				if (method.DeclaredAccessibility != property.DeclaredAccessibility)
				{
					this.AddAccessibility(method);
				}
				if (!SymbolDisplayVisitor.ShouldPropertyDisplayReadOnly(property) && SymbolDisplayVisitor.ShouldMethodDisplayReadOnly(method, property))
				{
					this.AddReadOnlyIfRequired();
				}
				this.AddKeyword(keyword);
				this.AddPunctuation(SyntaxKind.SemicolonToken);
			}
		}

		// Token: 0x0600279C RID: 10140 RVA: 0x000D8818 File Offset: 0x000D6A18
		private void AddExplicitInterfaceIfRequired<T>(ImmutableArray<T> implementedMembers) where T : ISymbol
		{
			if (this.format.MemberOptions.IncludesOption(SymbolDisplayMemberOptions.IncludeExplicitInterface) && !implementedMembers.IsEmpty)
			{
				T t = implementedMembers[0];
				INamedTypeSymbol containingType = t.ContainingType;
				if (containingType != null)
				{
					containingType.Accept(base.NotFirstVisitor);
					this.AddPunctuation(SyntaxKind.DotToken);
				}
			}
		}

		// Token: 0x0600279D RID: 10141 RVA: 0x000D8874 File Offset: 0x000D6A74
		private void AddCustomModifiersIfRequired(ImmutableArray<CustomModifier> customModifiers, bool leadingSpace = false, bool trailingSpace = true)
		{
			if (this.format.CompilerInternalOptions.IncludesOption(SymbolDisplayCompilerInternalOptions.IncludeCustomModifiers) && !customModifiers.IsEmpty)
			{
				bool flag = true;
				foreach (CustomModifier customModifier in customModifiers)
				{
					if (!flag || leadingSpace)
					{
						this.AddSpace();
					}
					flag = false;
					this.builder.Add(this.CreatePart((SymbolDisplayPartKind)34, null, customModifier.IsOptional ? "modopt" : "modreq"));
					this.AddPunctuation(SyntaxKind.OpenParenToken);
					customModifier.Modifier.Accept(base.NotFirstVisitor);
					this.AddPunctuation(SyntaxKind.CloseParenToken);
				}
				if (trailingSpace)
				{
					this.AddSpace();
				}
			}
		}

		// Token: 0x0600279E RID: 10142 RVA: 0x000D8929 File Offset: 0x000D6B29
		private void AddRefIfRequired()
		{
			if (this.format.MemberOptions.IncludesOption(SymbolDisplayMemberOptions.IncludeRef))
			{
				this.AddRef();
			}
		}

		// Token: 0x0600279F RID: 10143 RVA: 0x000D8948 File Offset: 0x000D6B48
		private void AddRef()
		{
			this.AddKeyword(SyntaxKind.RefKeyword);
			this.AddSpace();
		}

		// Token: 0x060027A0 RID: 10144 RVA: 0x000D895B File Offset: 0x000D6B5B
		private void AddRefReadonlyIfRequired()
		{
			if (this.format.MemberOptions.IncludesOption(SymbolDisplayMemberOptions.IncludeRef))
			{
				this.AddRefReadonly();
			}
		}

		// Token: 0x060027A1 RID: 10145 RVA: 0x000D897A File Offset: 0x000D6B7A
		private void AddRefReadonly()
		{
			this.AddKeyword(SyntaxKind.RefKeyword);
			this.AddSpace();
			this.AddKeyword(SyntaxKind.ReadOnlyKeyword);
			this.AddSpace();
		}

		// Token: 0x060027A2 RID: 10146 RVA: 0x000D899E File Offset: 0x000D6B9E
		private void AddReadOnlyIfRequired()
		{
			if (this.format.MemberOptions.IncludesOption(SymbolDisplayMemberOptions.IncludeRef))
			{
				this.AddKeyword(SyntaxKind.ReadOnlyKeyword);
				this.AddSpace();
			}
		}

		// Token: 0x060027A3 RID: 10147 RVA: 0x000D89C8 File Offset: 0x000D6BC8
		private void AddParameterRefKindIfRequired(RefKind refKind)
		{
			if (this.format.ParameterOptions.IncludesOption(SymbolDisplayParameterOptions.IncludeParamsRefOut))
			{
				this.AddParameterRefKind(refKind);
			}
		}

		// Token: 0x060027A4 RID: 10148 RVA: 0x000D89E4 File Offset: 0x000D6BE4
		private void AddParameterRefKind(RefKind refKind)
		{
			switch (refKind)
			{
			case RefKind.Ref:
				this.AddKeyword(SyntaxKind.RefKeyword);
				this.AddSpace();
				return;
			case RefKind.Out:
				this.AddKeyword(SyntaxKind.OutKeyword);
				this.AddSpace();
				return;
			case RefKind.In:
				this.AddKeyword(SyntaxKind.InKeyword);
				this.AddSpace();
				return;
			default:
				return;
			}
		}

		// Token: 0x060027A5 RID: 10149 RVA: 0x000D8A3B File Offset: 0x000D6C3B
		public override void VisitArrayType(IArrayTypeSymbol symbol)
		{
			this.VisitArrayTypeWithoutNullability(symbol);
			this.AddNullableAnnotations(symbol);
		}

		// Token: 0x060027A6 RID: 10150 RVA: 0x000D8A4C File Offset: 0x000D6C4C
		private void VisitArrayTypeWithoutNullability(IArrayTypeSymbol symbol)
		{
			if (this.TryAddAlias(symbol, this.builder))
			{
				return;
			}
			if (this.format.CompilerInternalOptions.IncludesOption(SymbolDisplayCompilerInternalOptions.ReverseArrayRankSpecifiers))
			{
				symbol.ElementType.Accept(this);
				this.AddArrayRank(symbol);
				return;
			}
			ITypeSymbol typeSymbol = symbol;
			do
			{
				typeSymbol = ((IArrayTypeSymbol)typeSymbol).ElementType;
			}
			while (typeSymbol.Kind == SymbolKind.ArrayType && !this.ShouldAddNullableAnnotation(typeSymbol));
			typeSymbol.Accept(base.NotFirstVisitor);
			IArrayTypeSymbol arrayTypeSymbol = symbol;
			while (arrayTypeSymbol != null && arrayTypeSymbol != typeSymbol)
			{
				if (!this.isFirstSymbolVisited)
				{
					this.AddCustomModifiersIfRequired(arrayTypeSymbol.CustomModifiers, true, true);
				}
				this.AddArrayRank(arrayTypeSymbol);
				arrayTypeSymbol = arrayTypeSymbol.ElementType as IArrayTypeSymbol;
			}
		}

		// Token: 0x060027A7 RID: 10151 RVA: 0x000D8AF1 File Offset: 0x000D6CF1
		private void AddNullableAnnotations(ITypeSymbol type)
		{
			if (this.ShouldAddNullableAnnotation(type))
			{
				this.AddPunctuation((type.NullableAnnotation == NullableAnnotation.Annotated) ? SyntaxKind.QuestionToken : SyntaxKind.ExclamationToken);
			}
		}

		// Token: 0x060027A8 RID: 10152 RVA: 0x000D8B18 File Offset: 0x000D6D18
		private bool ShouldAddNullableAnnotation(ITypeSymbol type)
		{
			NullableAnnotation nullableAnnotation = type.NullableAnnotation;
			if (nullableAnnotation != NullableAnnotation.NotAnnotated)
			{
				if (nullableAnnotation == NullableAnnotation.Annotated && this.format.MiscellaneousOptions.IncludesOption(SymbolDisplayMiscellaneousOptions.IncludeNullableReferenceTypeModifier) && !ITypeSymbolHelpers.IsNullableType(type) && !type.IsValueType)
				{
					return true;
				}
			}
			else if (this.format.MiscellaneousOptions.IncludesOption(SymbolDisplayMiscellaneousOptions.IncludeNotNullableReferenceTypeModifier) && !type.IsValueType)
			{
				Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel.TypeSymbol typeSymbol = type as Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel.TypeSymbol;
				if (typeSymbol == null || !typeSymbol.UnderlyingTypeSymbol.IsTypeParameterDisallowingAnnotationInCSharp8())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060027A9 RID: 10153 RVA: 0x000D8B98 File Offset: 0x000D6D98
		private void AddArrayRank(IArrayTypeSymbol symbol)
		{
			bool flag = this.format.MiscellaneousOptions.IncludesOption(SymbolDisplayMiscellaneousOptions.UseAsterisksInMultiDimensionalArrays);
			this.AddPunctuation(SyntaxKind.OpenBracketToken);
			if (symbol.Rank > 1)
			{
				if (flag)
				{
					this.AddPunctuation(SyntaxKind.AsteriskToken);
				}
			}
			else if (!symbol.IsSZArray)
			{
				this.AddPunctuation(SyntaxKind.AsteriskToken);
			}
			for (int i = 0; i < symbol.Rank - 1; i++)
			{
				this.AddPunctuation(SyntaxKind.CommaToken);
				if (flag)
				{
					this.AddPunctuation(SyntaxKind.AsteriskToken);
				}
			}
			this.AddPunctuation(SyntaxKind.CloseBracketToken);
		}

		// Token: 0x060027AA RID: 10154 RVA: 0x000D8C25 File Offset: 0x000D6E25
		public override void VisitPointerType(IPointerTypeSymbol symbol)
		{
			symbol.PointedAtType.Accept(base.NotFirstVisitor);
			this.AddNullableAnnotations(symbol);
			if (!this.isFirstSymbolVisited)
			{
				this.AddCustomModifiersIfRequired(symbol.CustomModifiers, true, true);
			}
			this.AddPunctuation(SyntaxKind.AsteriskToken);
		}

		// Token: 0x060027AB RID: 10155 RVA: 0x000D8C60 File Offset: 0x000D6E60
		public override void VisitFunctionPointerType(IFunctionPointerTypeSymbol symbol)
		{
			this.VisitMethod(symbol.Signature);
		}

		// Token: 0x060027AC RID: 10156 RVA: 0x000D8C6E File Offset: 0x000D6E6E
		public override void VisitTypeParameter(ITypeParameterSymbol symbol)
		{
			if (this.isFirstSymbolVisited)
			{
				this.AddTypeParameterVarianceIfRequired(symbol);
			}
			this.builder.Add(this.CreatePart(SymbolDisplayPartKind.TypeParameterName, symbol, symbol.Name));
			this.AddNullableAnnotations(symbol);
		}

		// Token: 0x060027AD RID: 10157 RVA: 0x000D8CA0 File Offset: 0x000D6EA0
		public override void VisitDynamicType(IDynamicTypeSymbol symbol)
		{
			this.builder.Add(this.CreatePart(SymbolDisplayPartKind.Keyword, symbol, symbol.Name));
			this.AddNullableAnnotations(symbol);
		}

		// Token: 0x060027AE RID: 10158 RVA: 0x000D8CC3 File Offset: 0x000D6EC3
		public override void VisitNamedType(INamedTypeSymbol symbol)
		{
			this.VisitNamedTypeWithoutNullability(symbol);
			this.AddNullableAnnotations(symbol);
		}

		// Token: 0x060027AF RID: 10159 RVA: 0x000D8CD4 File Offset: 0x000D6ED4
		private void VisitNamedTypeWithoutNullability(INamedTypeSymbol symbol)
		{
			if (base.IsMinimizing && this.TryAddAlias(symbol, this.builder))
			{
				return;
			}
			if ((this.format.MiscellaneousOptions.IncludesOption(SymbolDisplayMiscellaneousOptions.UseSpecialTypes) || (symbol.IsNativeIntegerType && !this.format.CompilerInternalOptions.IncludesOption(SymbolDisplayCompilerInternalOptions.UseNativeIntegerUnderlyingType))) && this.AddSpecialTypeKeyword(symbol))
			{
				return;
			}
			if (!this.format.MiscellaneousOptions.IncludesOption(SymbolDisplayMiscellaneousOptions.ExpandNullable) && ITypeSymbolHelpers.IsNullableType(symbol) && !symbol.IsDefinition)
			{
				ITypeSymbol typeSymbol = symbol.TypeArguments[0];
				if (typeSymbol.TypeKind != TypeKind.Pointer)
				{
					typeSymbol.Accept(base.NotFirstVisitor);
					this.AddCustomModifiersIfRequired(symbol.GetTypeArgumentCustomModifiers(0), true, false);
					this.AddPunctuation(SyntaxKind.QuestionToken);
					return;
				}
			}
			if (base.IsMinimizing || (symbol.IsTupleType && !this.ShouldDisplayAsValueTuple(symbol)))
			{
				this.MinimallyQualify(symbol);
				return;
			}
			this.AddTypeKind(symbol);
			if (this.CanShowDelegateSignature(symbol) && this.format.DelegateStyle == SymbolDisplayDelegateStyle.NameAndSignature)
			{
				IMethodSymbol delegateInvokeMethod = symbol.DelegateInvokeMethod;
				if (delegateInvokeMethod.ReturnsByRef)
				{
					this.AddRefIfRequired();
				}
				else if (delegateInvokeMethod.ReturnsByRefReadonly)
				{
					this.AddRefReadonlyIfRequired();
				}
				if (delegateInvokeMethod.ReturnsVoid)
				{
					this.AddKeyword(SyntaxKind.VoidKeyword);
				}
				else
				{
					this.AddReturnType(symbol.DelegateInvokeMethod);
				}
				this.AddSpace();
			}
			ISymbol containingSymbol = symbol.ContainingSymbol;
			if (this.ShouldVisitNamespace(containingSymbol))
			{
				INamespaceSymbol namespaceSymbol = (INamespaceSymbol)containingSymbol;
				if (!namespaceSymbol.IsGlobalNamespace || symbol.TypeKind != TypeKind.Error)
				{
					namespaceSymbol.Accept(base.NotFirstVisitor);
					this.AddPunctuation(namespaceSymbol.IsGlobalNamespace ? SyntaxKind.ColonColonToken : SyntaxKind.DotToken);
				}
			}
			if ((this.format.TypeQualificationStyle == SymbolDisplayTypeQualificationStyle.NameAndContainingTypes || this.format.TypeQualificationStyle == SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces) && this.IncludeNamedType(symbol.ContainingType))
			{
				symbol.ContainingType.Accept(base.NotFirstVisitor);
				this.AddPunctuation(SyntaxKind.DotToken);
			}
			this.AddNameAndTypeArgumentsOrParameters(symbol);
		}

		// Token: 0x060027B0 RID: 10160 RVA: 0x000D8EC9 File Offset: 0x000D70C9
		private bool ShouldDisplayAsValueTuple(INamedTypeSymbol symbol)
		{
			return this.format.CompilerInternalOptions.IncludesOption(SymbolDisplayCompilerInternalOptions.UseValueTuple) || !this.CanUseTupleSyntax(symbol);
		}

		// Token: 0x060027B1 RID: 10161 RVA: 0x000D8EEC File Offset: 0x000D70EC
		private void AddNameAndTypeArgumentsOrParameters(INamedTypeSymbol symbol)
		{
			if (symbol.IsAnonymousType)
			{
				this.AddAnonymousTypeName(symbol);
				return;
			}
			if (symbol.IsTupleType && !this.ShouldDisplayAsValueTuple(symbol))
			{
				this.AddTupleTypeName(symbol);
				return;
			}
			string text = null;
			Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel.NamedTypeSymbol namedTypeSymbol = symbol as Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel.NamedTypeSymbol;
			Microsoft.CodeAnalysis.CSharp.Symbols.NamedTypeSymbol namedTypeSymbol2 = ((namedTypeSymbol != null) ? namedTypeSymbol.UnderlyingNamedTypeSymbol : null);
			NoPiaIllegalGenericInstantiationSymbol noPiaIllegalGenericInstantiationSymbol = namedTypeSymbol2 as NoPiaIllegalGenericInstantiationSymbol;
			if (noPiaIllegalGenericInstantiationSymbol != null)
			{
				symbol = noPiaIllegalGenericInstantiationSymbol.UnderlyingSymbol.GetPublicSymbol();
			}
			else
			{
				NoPiaAmbiguousCanonicalTypeSymbol noPiaAmbiguousCanonicalTypeSymbol = namedTypeSymbol2 as NoPiaAmbiguousCanonicalTypeSymbol;
				if (noPiaAmbiguousCanonicalTypeSymbol != null)
				{
					symbol = noPiaAmbiguousCanonicalTypeSymbol.FirstCandidate.GetPublicSymbol();
				}
				else
				{
					NoPiaMissingCanonicalTypeSymbol noPiaMissingCanonicalTypeSymbol = namedTypeSymbol2 as NoPiaMissingCanonicalTypeSymbol;
					if (noPiaMissingCanonicalTypeSymbol != null)
					{
						text = noPiaMissingCanonicalTypeSymbol.FullTypeName;
					}
				}
			}
			SymbolDisplayPartKind partKind = SymbolDisplayVisitor.GetPartKind(symbol);
			if (text == null)
			{
				text = symbol.Name;
			}
			if (this.format.MiscellaneousOptions.IncludesOption(SymbolDisplayMiscellaneousOptions.UseErrorTypeSymbolName) && partKind == SymbolDisplayPartKind.ErrorTypeName && string.IsNullOrEmpty(text))
			{
				this.builder.Add(this.CreatePart(partKind, symbol, "?"));
			}
			else
			{
				text = this.RemoveAttributeSufficeIfNecessary(symbol, text);
				this.builder.Add(this.CreatePart(partKind, symbol, text));
			}
			if (this.format.CompilerInternalOptions.IncludesOption(SymbolDisplayCompilerInternalOptions.UseArityForGenericTypes))
			{
				if (namedTypeSymbol2 != null && namedTypeSymbol2.MangleName)
				{
					this.builder.Add(this.CreatePart((SymbolDisplayPartKind)33, null, MetadataHelpers.GetAritySuffix(symbol.Arity)));
				}
			}
			else if (symbol.Arity > 0 && this.format.GenericsOptions.IncludesOption(SymbolDisplayGenericsOptions.IncludeTypeParameters))
			{
				if (namedTypeSymbol2 is UnsupportedMetadataTypeSymbol || namedTypeSymbol2 is MissingMetadataTypeSymbol || symbol.IsUnboundGenericType)
				{
					this.AddPunctuation(SyntaxKind.LessThanToken);
					for (int i = 0; i < symbol.Arity - 1; i++)
					{
						this.AddPunctuation(SyntaxKind.CommaToken);
					}
					this.AddPunctuation(SyntaxKind.GreaterThanToken);
				}
				else
				{
					ImmutableArray<ImmutableArray<CustomModifier>> typeArgumentsModifiers = this.GetTypeArgumentsModifiers(namedTypeSymbol2);
					this.AddTypeArguments(symbol, typeArgumentsModifiers);
					this.AddDelegateParameters(symbol);
					this.AddTypeParameterConstraints(symbol.TypeArguments);
				}
			}
			else
			{
				this.AddDelegateParameters(symbol);
			}
			if (((namedTypeSymbol2 != null) ? namedTypeSymbol2.OriginalDefinition : null) is MissingMetadataTypeSymbol && this.format.CompilerInternalOptions.IncludesOption(SymbolDisplayCompilerInternalOptions.FlagMissingMetadataTypes))
			{
				this.AddPunctuation(SyntaxKind.OpenBracketToken);
				this.builder.Add(this.CreatePart((SymbolDisplayPartKind)34, symbol, "missing"));
				this.AddPunctuation(SyntaxKind.CloseBracketToken);
			}
		}

		// Token: 0x060027B2 RID: 10162 RVA: 0x000D9124 File Offset: 0x000D7324
		private ImmutableArray<ImmutableArray<CustomModifier>> GetTypeArgumentsModifiers(Microsoft.CodeAnalysis.CSharp.Symbols.NamedTypeSymbol underlyingTypeSymbol)
		{
			if (this.format.CompilerInternalOptions.IncludesOption(SymbolDisplayCompilerInternalOptions.IncludeCustomModifiers) && underlyingTypeSymbol != null)
			{
				return underlyingTypeSymbol.TypeArgumentsWithAnnotationsNoUseSiteDiagnostics.SelectAsArray<TypeWithAnnotations, ImmutableArray<CustomModifier>>((TypeWithAnnotations a) => a.CustomModifiers);
			}
			return default(ImmutableArray<ImmutableArray<CustomModifier>>);
		}

		// Token: 0x060027B3 RID: 10163 RVA: 0x000D917C File Offset: 0x000D737C
		private void AddDelegateParameters(INamedTypeSymbol symbol)
		{
			if (this.CanShowDelegateSignature(symbol) && (this.format.DelegateStyle == SymbolDisplayDelegateStyle.NameAndParameters || this.format.DelegateStyle == SymbolDisplayDelegateStyle.NameAndSignature))
			{
				IMethodSymbol delegateInvokeMethod = symbol.DelegateInvokeMethod;
				this.AddPunctuation(SyntaxKind.OpenParenToken);
				this.AddParametersIfRequired(false, delegateInvokeMethod.IsVararg, delegateInvokeMethod.Parameters);
				this.AddPunctuation(SyntaxKind.CloseParenToken);
			}
		}

		// Token: 0x060027B4 RID: 10164 RVA: 0x000D91E0 File Offset: 0x000D73E0
		private void AddAnonymousTypeName(INamedTypeSymbol symbol)
		{
			string text = string.Join(", ", Enumerable.Select<IPropertySymbol, string>(symbol.GetMembers().OfType<IPropertySymbol>(), new Func<IPropertySymbol, string>(this.CreateAnonymousTypeMember)));
			if (text.Length == 0)
			{
				this.builder.Add(new SymbolDisplayPart(SymbolDisplayPartKind.ClassName, symbol, "<empty anonymous type>"));
				return;
			}
			string text2 = "<anonymous type: " + text + ">";
			this.builder.Add(new SymbolDisplayPart(SymbolDisplayPartKind.ClassName, symbol, text2));
		}

		// Token: 0x060027B5 RID: 10165 RVA: 0x000D925C File Offset: 0x000D745C
		private bool CanUseTupleSyntax(INamedTypeSymbol tupleSymbol)
		{
			if (this.<CanUseTupleSyntax>g__containsModopt|73_0(tupleSymbol))
			{
				return false;
			}
			INamedTypeSymbol namedTypeSymbol = SymbolDisplayVisitor.GetTupleUnderlyingTypeOrSelf(tupleSymbol);
			if (namedTypeSymbol.Arity <= 1)
			{
				return false;
			}
			while (namedTypeSymbol.Arity == 8)
			{
				tupleSymbol = (INamedTypeSymbol)namedTypeSymbol.TypeArguments[7];
				if (tupleSymbol.TypeKind == TypeKind.Error || SymbolDisplayVisitor.HasNonDefaultTupleElements(tupleSymbol) || this.<CanUseTupleSyntax>g__containsModopt|73_0(tupleSymbol))
				{
					return false;
				}
				namedTypeSymbol = SymbolDisplayVisitor.GetTupleUnderlyingTypeOrSelf(tupleSymbol);
			}
			return true;
		}

		// Token: 0x060027B6 RID: 10166 RVA: 0x000D92C9 File Offset: 0x000D74C9
		private static INamedTypeSymbol GetTupleUnderlyingTypeOrSelf(INamedTypeSymbol type)
		{
			return type.TupleUnderlyingType ?? type;
		}

		// Token: 0x060027B7 RID: 10167 RVA: 0x000D92D6 File Offset: 0x000D74D6
		private static bool HasNonDefaultTupleElements(INamedTypeSymbol tupleSymbol)
		{
			return tupleSymbol.TupleElements.Any<IFieldSymbol>((IFieldSymbol e) => !e.IsDefaultTupleElement());
		}

		// Token: 0x060027B8 RID: 10168 RVA: 0x000D9304 File Offset: 0x000D7504
		private void AddTupleTypeName(INamedTypeSymbol symbol)
		{
			ImmutableArray<IFieldSymbol> tupleElements = symbol.TupleElements;
			this.AddPunctuation(SyntaxKind.OpenParenToken);
			for (int i = 0; i < tupleElements.Length; i++)
			{
				IFieldSymbol fieldSymbol = tupleElements[i];
				if (i != 0)
				{
					this.AddPunctuation(SyntaxKind.CommaToken);
					this.AddSpace();
				}
				this.VisitFieldType(fieldSymbol);
				if (fieldSymbol.IsExplicitlyNamedTupleElement)
				{
					this.AddSpace();
					this.builder.Add(this.CreatePart(SymbolDisplayPartKind.FieldName, symbol, fieldSymbol.Name));
				}
			}
			this.AddPunctuation(SyntaxKind.CloseParenToken);
			if (symbol.TypeKind == TypeKind.Error && this.format.CompilerInternalOptions.IncludesOption(SymbolDisplayCompilerInternalOptions.FlagMissingMetadataTypes))
			{
				this.AddPunctuation(SyntaxKind.OpenBracketToken);
				this.builder.Add(this.CreatePart((SymbolDisplayPartKind)34, symbol, "missing"));
				this.AddPunctuation(SyntaxKind.CloseBracketToken);
			}
		}

		// Token: 0x060027B9 RID: 10169 RVA: 0x000D93D6 File Offset: 0x000D75D6
		private string CreateAnonymousTypeMember(IPropertySymbol property)
		{
			return property.Type.ToDisplayString(this.format) + " " + property.Name;
		}

		// Token: 0x060027BA RID: 10170 RVA: 0x000D93F9 File Offset: 0x000D75F9
		private bool CanShowDelegateSignature(INamedTypeSymbol symbol)
		{
			return this.isFirstSymbolVisited && symbol.TypeKind == TypeKind.Delegate && this.format.DelegateStyle != SymbolDisplayDelegateStyle.NameOnly && symbol.DelegateInvokeMethod != null;
		}

		// Token: 0x060027BB RID: 10171 RVA: 0x000D9424 File Offset: 0x000D7624
		private static SymbolDisplayPartKind GetPartKind(INamedTypeSymbol symbol)
		{
			switch (symbol.TypeKind)
			{
			case TypeKind.Class:
				if (symbol.IsRecord)
				{
					return SymbolDisplayPartKind.RecordClassName;
				}
				break;
			case TypeKind.Delegate:
				return SymbolDisplayPartKind.DelegateName;
			case TypeKind.Dynamic:
			case TypeKind.Pointer:
			case TypeKind.TypeParameter:
				goto IL_0060;
			case TypeKind.Enum:
				return SymbolDisplayPartKind.EnumName;
			case TypeKind.Error:
				return SymbolDisplayPartKind.ErrorTypeName;
			case TypeKind.Interface:
				return SymbolDisplayPartKind.InterfaceName;
			case TypeKind.Module:
			case TypeKind.Submission:
				break;
			case TypeKind.Struct:
				if (symbol.IsRecord)
				{
					return SymbolDisplayPartKind.RecordStructName;
				}
				return SymbolDisplayPartKind.StructName;
			default:
				goto IL_0060;
			}
			return SymbolDisplayPartKind.ClassName;
			IL_0060:
			throw ExceptionUtilities.UnexpectedValue(symbol.TypeKind);
		}

		// Token: 0x060027BC RID: 10172 RVA: 0x000D94A4 File Offset: 0x000D76A4
		private bool AddSpecialTypeKeyword(INamedTypeSymbol symbol)
		{
			string specialTypeName = SymbolDisplayVisitor.GetSpecialTypeName(symbol);
			if (specialTypeName == null)
			{
				return false;
			}
			this.builder.Add(this.CreatePart(SymbolDisplayPartKind.Keyword, symbol, specialTypeName));
			return true;
		}

		// Token: 0x060027BD RID: 10173 RVA: 0x000D94D4 File Offset: 0x000D76D4
		private static string GetSpecialTypeName(INamedTypeSymbol symbol)
		{
			switch (symbol.SpecialType)
			{
			case SpecialType.System_Object:
				return "object";
			case SpecialType.System_Void:
				return "void";
			case SpecialType.System_Boolean:
				return "bool";
			case SpecialType.System_Char:
				return "char";
			case SpecialType.System_SByte:
				return "sbyte";
			case SpecialType.System_Byte:
				return "byte";
			case SpecialType.System_Int16:
				return "short";
			case SpecialType.System_UInt16:
				return "ushort";
			case SpecialType.System_Int32:
				return "int";
			case SpecialType.System_UInt32:
				return "uint";
			case SpecialType.System_Int64:
				return "long";
			case SpecialType.System_UInt64:
				return "ulong";
			case SpecialType.System_Decimal:
				return "decimal";
			case SpecialType.System_Single:
				return "float";
			case SpecialType.System_Double:
				return "double";
			case SpecialType.System_String:
				return "string";
			case SpecialType.System_IntPtr:
				if (symbol.IsNativeIntegerType)
				{
					return "nint";
				}
				break;
			case SpecialType.System_UIntPtr:
				if (symbol.IsNativeIntegerType)
				{
					return "nuint";
				}
				break;
			}
			return null;
		}

		// Token: 0x060027BE RID: 10174 RVA: 0x000D95C8 File Offset: 0x000D77C8
		private void AddTypeKind(INamedTypeSymbol symbol)
		{
			if (this.isFirstSymbolVisited && this.format.KindOptions.IncludesOption(SymbolDisplayKindOptions.IncludeTypeKeyword))
			{
				if (symbol.IsAnonymousType)
				{
					this.builder.Add(new SymbolDisplayPart(SymbolDisplayPartKind.AnonymousTypeIndicator, null, "AnonymousType"));
					this.AddSpace();
					return;
				}
				if (symbol.IsTupleType && !this.ShouldDisplayAsValueTuple(symbol))
				{
					this.builder.Add(new SymbolDisplayPart(SymbolDisplayPartKind.AnonymousTypeIndicator, null, "Tuple"));
					this.AddSpace();
					return;
				}
				switch (symbol.TypeKind)
				{
				case TypeKind.Class:
					if (symbol.IsRecord)
					{
						this.AddKeyword(SyntaxKind.RecordKeyword);
						this.AddSpace();
						return;
					}
					break;
				case TypeKind.Delegate:
					this.AddKeyword(SyntaxKind.DelegateKeyword);
					this.AddSpace();
					return;
				case TypeKind.Dynamic:
				case TypeKind.Error:
				case TypeKind.Pointer:
					return;
				case TypeKind.Enum:
					this.AddKeyword(SyntaxKind.EnumKeyword);
					this.AddSpace();
					return;
				case TypeKind.Interface:
					this.AddKeyword(SyntaxKind.InterfaceKeyword);
					this.AddSpace();
					return;
				case TypeKind.Module:
					break;
				case TypeKind.Struct:
					if (symbol.IsRecord)
					{
						this.AddKeyword(SyntaxKind.RecordKeyword);
						this.AddSpace();
						this.AddKeyword(SyntaxKind.StructKeyword);
						this.AddSpace();
						return;
					}
					if (symbol.IsReadOnly)
					{
						this.AddKeyword(SyntaxKind.ReadOnlyKeyword);
						this.AddSpace();
					}
					if (symbol.IsRefLikeType)
					{
						this.AddKeyword(SyntaxKind.RefKeyword);
						this.AddSpace();
					}
					this.AddKeyword(SyntaxKind.StructKeyword);
					this.AddSpace();
					return;
				default:
					return;
				}
				this.AddKeyword(SyntaxKind.ClassKeyword);
				this.AddSpace();
				return;
			}
		}

		// Token: 0x060027BF RID: 10175 RVA: 0x000D9754 File Offset: 0x000D7954
		private void AddTypeParameterVarianceIfRequired(ITypeParameterSymbol symbol)
		{
			if (this.format.GenericsOptions.IncludesOption(SymbolDisplayGenericsOptions.IncludeVariance))
			{
				VarianceKind variance = symbol.Variance;
				if (variance != VarianceKind.Out)
				{
					if (variance == VarianceKind.In)
					{
						this.AddKeyword(SyntaxKind.InKeyword);
						this.AddSpace();
						return;
					}
				}
				else
				{
					this.AddKeyword(SyntaxKind.OutKeyword);
					this.AddSpace();
				}
			}
		}

		// Token: 0x060027C0 RID: 10176 RVA: 0x000D97A8 File Offset: 0x000D79A8
		private void AddTypeArguments(ISymbol owner, ImmutableArray<ImmutableArray<CustomModifier>> modifiers)
		{
			ImmutableArray<ITypeSymbol> immutableArray;
			if (owner.Kind == SymbolKind.Method)
			{
				immutableArray = ((IMethodSymbol)owner).TypeArguments;
			}
			else
			{
				immutableArray = ((INamedTypeSymbol)owner).TypeArguments;
			}
			if (immutableArray.Length > 0 && this.format.GenericsOptions.IncludesOption(SymbolDisplayGenericsOptions.IncludeTypeParameters))
			{
				this.AddPunctuation(SyntaxKind.LessThanToken);
				bool flag = true;
				for (int i = 0; i < immutableArray.Length; i++)
				{
					ITypeSymbol typeSymbol = immutableArray[i];
					if (!flag)
					{
						this.AddPunctuation(SyntaxKind.CommaToken);
						this.AddSpace();
					}
					flag = false;
					AbstractSymbolDisplayVisitor abstractSymbolDisplayVisitor;
					if (typeSymbol.Kind == SymbolKind.TypeParameter)
					{
						ITypeParameterSymbol typeParameterSymbol = (ITypeParameterSymbol)typeSymbol;
						this.AddTypeParameterVarianceIfRequired(typeParameterSymbol);
						abstractSymbolDisplayVisitor = base.NotFirstVisitor;
					}
					else
					{
						abstractSymbolDisplayVisitor = base.NotFirstVisitorNamespaceOrType;
					}
					typeSymbol.Accept(abstractSymbolDisplayVisitor);
					if (!modifiers.IsDefault)
					{
						this.AddCustomModifiersIfRequired(modifiers[i], true, false);
					}
				}
				this.AddPunctuation(SyntaxKind.GreaterThanToken);
			}
		}

		// Token: 0x060027C1 RID: 10177 RVA: 0x000D9894 File Offset: 0x000D7A94
		private static bool TypeParameterHasConstraints(ITypeParameterSymbol typeParam)
		{
			return !typeParam.ConstraintTypes.IsEmpty || typeParam.HasConstructorConstraint || typeParam.HasReferenceTypeConstraint || typeParam.HasValueTypeConstraint || typeParam.HasNotNullConstraint;
		}

		// Token: 0x060027C2 RID: 10178 RVA: 0x000D98D4 File Offset: 0x000D7AD4
		private void AddTypeParameterConstraints(ImmutableArray<ITypeSymbol> typeArguments)
		{
			if (this.isFirstSymbolVisited && this.format.GenericsOptions.IncludesOption(SymbolDisplayGenericsOptions.IncludeTypeConstraints))
			{
				foreach (ITypeSymbol typeSymbol in typeArguments)
				{
					if (typeSymbol.Kind == SymbolKind.TypeParameter)
					{
						ITypeParameterSymbol typeParameterSymbol = (ITypeParameterSymbol)typeSymbol;
						if (SymbolDisplayVisitor.TypeParameterHasConstraints(typeParameterSymbol))
						{
							this.AddSpace();
							this.AddKeyword(SyntaxKind.WhereKeyword);
							this.AddSpace();
							typeParameterSymbol.Accept(base.NotFirstVisitor);
							this.AddSpace();
							this.AddPunctuation(SyntaxKind.ColonToken);
							this.AddSpace();
							bool flag = false;
							if (typeParameterSymbol.HasReferenceTypeConstraint)
							{
								this.AddKeyword(SyntaxKind.ClassKeyword);
								NullableAnnotation referenceTypeConstraintNullableAnnotation = typeParameterSymbol.ReferenceTypeConstraintNullableAnnotation;
								if (referenceTypeConstraintNullableAnnotation != NullableAnnotation.NotAnnotated)
								{
									if (referenceTypeConstraintNullableAnnotation == NullableAnnotation.Annotated && this.format.MiscellaneousOptions.IncludesOption(SymbolDisplayMiscellaneousOptions.IncludeNullableReferenceTypeModifier))
									{
										this.AddPunctuation(SyntaxKind.QuestionToken);
									}
								}
								else if (this.format.MiscellaneousOptions.IncludesOption(SymbolDisplayMiscellaneousOptions.IncludeNotNullableReferenceTypeModifier))
								{
									this.AddPunctuation(SyntaxKind.ExclamationToken);
								}
								flag = true;
							}
							else if (typeParameterSymbol.HasUnmanagedTypeConstraint)
							{
								this.builder.Add(new SymbolDisplayPart(SymbolDisplayPartKind.Keyword, null, "unmanaged"));
								flag = true;
							}
							else if (typeParameterSymbol.HasValueTypeConstraint)
							{
								this.AddKeyword(SyntaxKind.StructKeyword);
								flag = true;
							}
							else if (typeParameterSymbol.HasNotNullConstraint)
							{
								this.builder.Add(new SymbolDisplayPart(SymbolDisplayPartKind.Keyword, null, "notnull"));
								flag = true;
							}
							for (int i = 0; i < typeParameterSymbol.ConstraintTypes.Length; i++)
							{
								ISymbol symbol = typeParameterSymbol.ConstraintTypes[i];
								if (flag)
								{
									this.AddPunctuation(SyntaxKind.CommaToken);
									this.AddSpace();
								}
								symbol.Accept(base.NotFirstVisitor);
								flag = true;
							}
							if (typeParameterSymbol.HasConstructorConstraint)
							{
								if (flag)
								{
									this.AddPunctuation(SyntaxKind.CommaToken);
									this.AddSpace();
								}
								this.AddKeyword(SyntaxKind.NewKeyword);
								this.AddPunctuation(SyntaxKind.OpenParenToken);
								this.AddPunctuation(SyntaxKind.CloseParenToken);
							}
						}
					}
				}
			}
		}

		// Token: 0x060027C3 RID: 10179 RVA: 0x000D9AD4 File Offset: 0x000D7CD4
		private void AddConstantValue(ITypeSymbol type, object constantValue, bool preferNumericValueOrExpandedFlagsForEnum = false)
		{
			if (constantValue != null)
			{
				base.AddNonNullConstantValue(type, constantValue, preferNumericValueOrExpandedFlagsForEnum);
				return;
			}
			if (type.IsReferenceType || type.TypeKind == TypeKind.Pointer || ITypeSymbolHelpers.IsNullableType(type))
			{
				this.AddKeyword(SyntaxKind.NullKeyword);
				return;
			}
			this.AddKeyword(SyntaxKind.DefaultKeyword);
			if (!this.format.MiscellaneousOptions.IncludesOption(SymbolDisplayMiscellaneousOptions.AllowDefaultLiteral))
			{
				this.AddPunctuation(SyntaxKind.OpenParenToken);
				type.Accept(base.NotFirstVisitor);
				this.AddPunctuation(SyntaxKind.CloseParenToken);
			}
		}

		// Token: 0x060027C4 RID: 10180 RVA: 0x000D9B58 File Offset: 0x000D7D58
		protected override void AddExplicitlyCastedLiteralValue(INamedTypeSymbol namedType, SpecialType type, object value)
		{
			this.AddPunctuation(SyntaxKind.OpenParenToken);
			namedType.Accept(base.NotFirstVisitor);
			this.AddPunctuation(SyntaxKind.CloseParenToken);
			this.AddLiteralValue(type, value);
		}

		// Token: 0x060027C5 RID: 10181 RVA: 0x000D9B84 File Offset: 0x000D7D84
		protected override void AddLiteralValue(SpecialType type, object value)
		{
			string text = SymbolDisplay.FormatPrimitive(value, true, false);
			SymbolDisplayPartKind symbolDisplayPartKind = SymbolDisplayPartKind.NumericLiteral;
			if (type != SpecialType.System_Boolean)
			{
				if (type == SpecialType.System_Char || type == SpecialType.System_String)
				{
					symbolDisplayPartKind = SymbolDisplayPartKind.StringLiteral;
				}
			}
			else
			{
				symbolDisplayPartKind = SymbolDisplayPartKind.Keyword;
			}
			this.builder.Add(this.CreatePart(symbolDisplayPartKind, null, text));
		}

		// Token: 0x060027C6 RID: 10182 RVA: 0x000D9BC8 File Offset: 0x000D7DC8
		protected override void AddBitwiseOr()
		{
			this.AddPunctuation(SyntaxKind.BarToken);
		}

		// Token: 0x060027C7 RID: 10183 RVA: 0x000D9BD8 File Offset: 0x000D7DD8
		private bool TryAddAlias(INamespaceOrTypeSymbol symbol, ArrayBuilder<SymbolDisplayPart> builder)
		{
			IAliasSymbol aliasSymbol = this.GetAliasSymbol(symbol);
			if (aliasSymbol != null)
			{
				string name = aliasSymbol.Name;
				ImmutableArray<ISymbol> immutableArray = this.semanticModelOpt.LookupNamespacesAndTypes(this.positionOpt, null, name);
				if (immutableArray.Length == 1 && immutableArray[0] is IAliasSymbol && aliasSymbol.Target.Equals(symbol))
				{
					builder.Add(this.CreatePart(SymbolDisplayPartKind.AliasName, aliasSymbol, name));
					return true;
				}
			}
			return false;
		}

		// Token: 0x060027C8 RID: 10184 RVA: 0x000D9C44 File Offset: 0x000D7E44
		protected override bool ShouldRestrictMinimallyQualifyLookupToNamespacesAndTypes()
		{
			SyntaxToken syntaxToken = this.semanticModelOpt.SyntaxTree.GetRoot(default(CancellationToken)).FindToken(this.positionOpt, false);
			return SyntaxFacts.IsInNamespaceOrTypeContext(syntaxToken.Parent as ExpressionSyntax) || syntaxToken.IsKind(SyntaxKind.NewKeyword) || this.inNamespaceOrType;
		}

		// Token: 0x060027C9 RID: 10185 RVA: 0x000D9CA0 File Offset: 0x000D7EA0
		private void MinimallyQualify(INamespaceSymbol symbol)
		{
			if (symbol.IsGlobalNamespace)
			{
				return;
			}
			ImmutableArray<ISymbol> immutableArray = (this.ShouldRestrictMinimallyQualifyLookupToNamespacesAndTypes() ? this.semanticModelOpt.LookupNamespacesAndTypes(this.positionOpt, null, symbol.Name) : this.semanticModelOpt.LookupSymbols(this.positionOpt, null, symbol.Name, false));
			ISymbol symbol2 = Enumerable.FirstOrDefault<ISymbol>(immutableArray.OfType<ISymbol>());
			if (immutableArray.Length != 1 || symbol2 == null || !symbol2.Equals(symbol))
			{
				INamespaceSymbol namespaceSymbol = ((symbol.ContainingNamespace == null) ? null : this.semanticModelOpt.Compilation.GetCompilationNamespace(symbol.ContainingNamespace));
				if (namespaceSymbol != null)
				{
					if (namespaceSymbol.IsGlobalNamespace)
					{
						if (this.format.GlobalNamespaceStyle == SymbolDisplayGlobalNamespaceStyle.Included)
						{
							this.AddGlobalNamespace(namespaceSymbol);
							this.AddPunctuation(SyntaxKind.ColonColonToken);
						}
					}
					else
					{
						namespaceSymbol.Accept(base.NotFirstVisitor);
						this.AddPunctuation(SyntaxKind.DotToken);
					}
				}
			}
			this.builder.Add(this.CreatePart(SymbolDisplayPartKind.NamespaceName, symbol, symbol.Name));
		}

		// Token: 0x060027CA RID: 10186 RVA: 0x000D9D98 File Offset: 0x000D7F98
		private void MinimallyQualify(INamedTypeSymbol symbol)
		{
			if (!symbol.IsAnonymousType && !symbol.IsTupleType && !base.NameBoundSuccessfullyToSameSymbol(symbol))
			{
				if (this.IncludeNamedType(symbol.ContainingType))
				{
					symbol.ContainingType.Accept(base.NotFirstVisitor);
					this.AddPunctuation(SyntaxKind.DotToken);
				}
				else
				{
					INamespaceSymbol namespaceSymbol = ((symbol.ContainingNamespace == null) ? null : this.semanticModelOpt.Compilation.GetCompilationNamespace(symbol.ContainingNamespace));
					if (namespaceSymbol != null)
					{
						if (namespaceSymbol.IsGlobalNamespace)
						{
							if (symbol.TypeKind != TypeKind.Error)
							{
								this.AddKeyword(SyntaxKind.GlobalKeyword);
								this.AddPunctuation(SyntaxKind.ColonColonToken);
							}
						}
						else
						{
							namespaceSymbol.Accept(base.NotFirstVisitor);
							this.AddPunctuation(SyntaxKind.DotToken);
						}
					}
				}
			}
			this.AddNameAndTypeArgumentsOrParameters(symbol);
		}

		// Token: 0x060027CB RID: 10187 RVA: 0x000D9E60 File Offset: 0x000D8060
		private IDictionary<INamespaceOrTypeSymbol, IAliasSymbol> CreateAliasMap()
		{
			if (!base.IsMinimizing)
			{
				return SpecializedCollections.EmptyDictionary<INamespaceOrTypeSymbol, IAliasSymbol>();
			}
			SemanticModel semanticModel;
			int num;
			if (this.semanticModelOpt.IsSpeculativeSemanticModel)
			{
				semanticModel = this.semanticModelOpt.ParentModel;
				num = this.semanticModelOpt.OriginalPositionForSpeculation;
			}
			else
			{
				semanticModel = this.semanticModelOpt;
				num = this.positionOpt;
			}
			SyntaxNode syntaxNode = semanticModel.SyntaxTree.GetRoot(default(CancellationToken)).FindToken(num, false).Parent;
			UsingDirectiveSyntax ancestorOrThis = SymbolDisplayVisitor.GetAncestorOrThis<UsingDirectiveSyntax>(syntaxNode);
			if (ancestorOrThis != null)
			{
				syntaxNode = ancestorOrThis.Parent.Parent;
			}
			IEnumerable<IAliasSymbol> enumerable = Enumerable.Where<IAliasSymbol>(Enumerable.Select<UsingDirectiveSyntax, IAliasSymbol>(Enumerable.Where<UsingDirectiveSyntax>(Enumerable.Concat<UsingDirectiveSyntax>(Enumerable.SelectMany<NamespaceDeclarationSyntax, UsingDirectiveSyntax>(SymbolDisplayVisitor.GetAncestorsOrThis<NamespaceDeclarationSyntax>(syntaxNode), (NamespaceDeclarationSyntax n) => n.Usings), Enumerable.SelectMany<CompilationUnitSyntax, UsingDirectiveSyntax>(SymbolDisplayVisitor.GetAncestorsOrThis<CompilationUnitSyntax>(syntaxNode), (CompilationUnitSyntax c) => c.Usings)), (UsingDirectiveSyntax u) => u.Alias != null), (UsingDirectiveSyntax u) => semanticModel.GetDeclaredSymbol(u, default(CancellationToken))), (IAliasSymbol u) => u != null);
			ImmutableDictionary<INamespaceOrTypeSymbol, IAliasSymbol>.Builder builder = ImmutableDictionary.CreateBuilder<INamespaceOrTypeSymbol, IAliasSymbol>();
			foreach (IAliasSymbol aliasSymbol in enumerable)
			{
				if (!builder.ContainsKey(aliasSymbol.Target))
				{
					builder.Add(aliasSymbol.Target, aliasSymbol);
				}
			}
			return builder.ToImmutable();
		}

		// Token: 0x060027CC RID: 10188 RVA: 0x000DA01C File Offset: 0x000D821C
		private ITypeSymbol GetRangeVariableType(IRangeVariableSymbol symbol)
		{
			ITypeSymbol typeSymbol = null;
			if (base.IsMinimizing && !symbol.Locations.IsEmpty)
			{
				Location location = symbol.Locations.First<Location>();
				if (location.IsInSource && location.SourceTree == this.semanticModelOpt.SyntaxTree)
				{
					SyntaxToken syntaxToken = location.SourceTree.GetRoot(default(CancellationToken)).FindToken(this.positionOpt, false);
					QueryBodySyntax queryBody = SymbolDisplayVisitor.GetQueryBody(syntaxToken);
					if (queryBody != null)
					{
						IdentifierNameSyntax identifierNameSyntax = SyntaxFactory.IdentifierName(symbol.Name);
						typeSymbol = this.semanticModelOpt.GetSpeculativeTypeInfo(queryBody.SelectOrGroup.Span.End - 1, identifierNameSyntax, SpeculativeBindingOption.BindAsExpression).Type;
					}
					IdentifierNameSyntax identifierNameSyntax2 = syntaxToken.Parent as IdentifierNameSyntax;
					if (identifierNameSyntax2 != null)
					{
						typeSymbol = this.semanticModelOpt.GetTypeInfo(identifierNameSyntax2, default(CancellationToken)).Type;
					}
				}
			}
			return typeSymbol;
		}

		// Token: 0x060027CD RID: 10189 RVA: 0x000DA114 File Offset: 0x000D8314
		private static QueryBodySyntax GetQueryBody(SyntaxToken token)
		{
			SyntaxNode parent = token.Parent;
			FromClauseSyntax fromClauseSyntax = parent as FromClauseSyntax;
			if (fromClauseSyntax == null)
			{
				LetClauseSyntax letClauseSyntax = parent as LetClauseSyntax;
				if (letClauseSyntax == null)
				{
					JoinClauseSyntax joinClauseSyntax = parent as JoinClauseSyntax;
					if (joinClauseSyntax == null)
					{
						QueryContinuationSyntax queryContinuationSyntax = parent as QueryContinuationSyntax;
						if (queryContinuationSyntax != null)
						{
							if (queryContinuationSyntax.Identifier == token)
							{
								return queryContinuationSyntax.Body;
							}
						}
					}
					else if (joinClauseSyntax.Identifier == token)
					{
						return joinClauseSyntax.Parent as QueryBodySyntax;
					}
				}
				else if (letClauseSyntax.Identifier == token)
				{
					return letClauseSyntax.Parent as QueryBodySyntax;
				}
			}
			else if (fromClauseSyntax.Identifier == token)
			{
				return (fromClauseSyntax.Parent as QueryBodySyntax) ?? ((QueryExpressionSyntax)fromClauseSyntax.Parent).Body;
			}
			return null;
		}

		// Token: 0x060027CE RID: 10190 RVA: 0x000DA1E4 File Offset: 0x000D83E4
		private string RemoveAttributeSufficeIfNecessary(INamedTypeSymbol symbol, string symbolName)
		{
			string text;
			if (base.IsMinimizing && this.format.MiscellaneousOptions.IncludesOption(SymbolDisplayMiscellaneousOptions.RemoveAttributeSuffix) && this.semanticModelOpt.Compilation.IsAttributeType(symbol) && symbolName.TryGetWithoutAttributeSuffix(out text) && SyntaxFactory.ParseToken(text, 0).IsKind(SyntaxKind.IdentifierToken))
			{
				symbolName = text;
			}
			return symbolName;
		}

		// Token: 0x060027CF RID: 10191 RVA: 0x000DA241 File Offset: 0x000D8441
		private static T GetAncestorOrThis<T>(SyntaxNode node) where T : SyntaxNode
		{
			return Enumerable.FirstOrDefault<T>(SymbolDisplayVisitor.GetAncestorsOrThis<T>(node));
		}

		// Token: 0x060027D0 RID: 10192 RVA: 0x000DA24E File Offset: 0x000D844E
		private static IEnumerable<T> GetAncestorsOrThis<T>(SyntaxNode node) where T : SyntaxNode
		{
			if (node != null)
			{
				return Enumerable.OfType<T>(node.AncestorsAndSelf(true));
			}
			return SpecializedCollections.EmptyEnumerable<T>();
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x060027D1 RID: 10193 RVA: 0x000DA268 File Offset: 0x000D8468
		private IDictionary<INamespaceOrTypeSymbol, IAliasSymbol> AliasMap
		{
			get
			{
				IDictionary<INamespaceOrTypeSymbol, IAliasSymbol> dictionary = this._lazyAliasMap;
				if (dictionary != null)
				{
					return dictionary;
				}
				dictionary = this.CreateAliasMap();
				return Interlocked.CompareExchange<IDictionary<INamespaceOrTypeSymbol, IAliasSymbol>>(ref this._lazyAliasMap, dictionary, null) ?? dictionary;
			}
		}

		// Token: 0x060027D2 RID: 10194 RVA: 0x000DA29C File Offset: 0x000D849C
		private IAliasSymbol GetAliasSymbol(INamespaceOrTypeSymbol symbol)
		{
			IAliasSymbol aliasSymbol;
			if (!this.AliasMap.TryGetValue(symbol, ref aliasSymbol))
			{
				return null;
			}
			return aliasSymbol;
		}

		// Token: 0x060027D3 RID: 10195 RVA: 0x000DA2BC File Offset: 0x000D84BC
		[CompilerGenerated]
		private void <VisitMethod>g__visitFunctionPointerSignature|37_0(IMethodSymbol symbol)
		{
			this.AddKeyword(SyntaxKind.DelegateKeyword);
			this.AddPunctuation(SyntaxKind.AsteriskToken);
			if (symbol.CallingConvention != SignatureCallingConvention.Default)
			{
				this.AddSpace();
				this.AddKeyword(SyntaxKind.UnmanagedKeyword);
				ImmutableArray<INamedTypeSymbol> unmanagedCallingConventionTypes = symbol.UnmanagedCallingConventionTypes;
				if (symbol.CallingConvention != SignatureCallingConvention.Unmanaged || !unmanagedCallingConventionTypes.IsEmpty)
				{
					this.AddPunctuation(SyntaxKind.OpenBracketToken);
					switch (symbol.CallingConvention)
					{
					case SignatureCallingConvention.CDecl:
						this.builder.Add(this.CreatePart(SymbolDisplayPartKind.ClassName, symbol, "Cdecl"));
						break;
					case SignatureCallingConvention.StdCall:
						this.builder.Add(this.CreatePart(SymbolDisplayPartKind.ClassName, symbol, "Stdcall"));
						break;
					case SignatureCallingConvention.ThisCall:
						this.builder.Add(this.CreatePart(SymbolDisplayPartKind.ClassName, symbol, "Thiscall"));
						break;
					case SignatureCallingConvention.FastCall:
						this.builder.Add(this.CreatePart(SymbolDisplayPartKind.ClassName, symbol, "Fastcall"));
						break;
					case SignatureCallingConvention.Unmanaged:
					{
						bool flag = true;
						foreach (INamedTypeSymbol namedTypeSymbol in unmanagedCallingConventionTypes)
						{
							if (!flag)
							{
								this.AddPunctuation(SyntaxKind.CommaToken);
								this.AddSpace();
							}
							flag = false;
							ArrayBuilder<SymbolDisplayPart> builder = this.builder;
							SymbolDisplayPartKind symbolDisplayPartKind = SymbolDisplayPartKind.ClassName;
							ISymbol symbol2 = namedTypeSymbol;
							string name = namedTypeSymbol.Name;
							int length = name.Length;
							int num = 8;
							int num2 = length - num;
							builder.Add(this.CreatePart(symbolDisplayPartKind, symbol2, name.Substring(num, num2)));
						}
						break;
					}
					}
					this.AddPunctuation(SyntaxKind.CloseBracketToken);
				}
			}
			this.AddPunctuation(SyntaxKind.LessThanToken);
			foreach (IParameterSymbol parameterSymbol in symbol.Parameters)
			{
				this.AddParameterRefKind(parameterSymbol.RefKind);
				this.AddCustomModifiersIfRequired(parameterSymbol.RefCustomModifiers, false, true);
				parameterSymbol.Type.Accept(base.NotFirstVisitor);
				this.AddCustomModifiersIfRequired(parameterSymbol.CustomModifiers, true, false);
				this.AddPunctuation(SyntaxKind.CommaToken);
				this.AddSpace();
			}
			if (symbol.ReturnsByRef)
			{
				this.AddRef();
			}
			else if (symbol.ReturnsByRefReadonly)
			{
				this.AddRefReadonly();
			}
			this.AddCustomModifiersIfRequired(symbol.RefCustomModifiers, false, true);
			symbol.ReturnType.Accept(base.NotFirstVisitor);
			this.AddCustomModifiersIfRequired(symbol.ReturnTypeCustomModifiers, true, false);
			this.AddPunctuation(SyntaxKind.GreaterThanToken);
		}

		// Token: 0x060027D4 RID: 10196 RVA: 0x000DA514 File Offset: 0x000D8714
		[CompilerGenerated]
		private bool <CanUseTupleSyntax>g__containsModopt|73_0(INamedTypeSymbol symbol)
		{
			Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel.NamedTypeSymbol namedTypeSymbol = symbol as Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel.NamedTypeSymbol;
			Microsoft.CodeAnalysis.CSharp.Symbols.NamedTypeSymbol namedTypeSymbol2 = ((namedTypeSymbol != null) ? namedTypeSymbol.UnderlyingNamedTypeSymbol : null);
			ImmutableArray<ImmutableArray<CustomModifier>> typeArgumentsModifiers = this.GetTypeArgumentsModifiers(namedTypeSymbol2);
			if (typeArgumentsModifiers.IsDefault)
			{
				return false;
			}
			return typeArgumentsModifiers.Any<ImmutableArray<CustomModifier>>((ImmutableArray<CustomModifier> m) => !m.IsEmpty);
		}

		// Token: 0x04001127 RID: 4391
		private readonly bool _escapeKeywordIdentifiers;

		// Token: 0x04001128 RID: 4392
		private IDictionary<INamespaceOrTypeSymbol, IAliasSymbol> _lazyAliasMap;

		// Token: 0x04001129 RID: 4393
		private const string IL_KEYWORD_MODOPT = "modopt";

		// Token: 0x0400112A RID: 4394
		private const string IL_KEYWORD_MODREQ = "modreq";
	}
}
