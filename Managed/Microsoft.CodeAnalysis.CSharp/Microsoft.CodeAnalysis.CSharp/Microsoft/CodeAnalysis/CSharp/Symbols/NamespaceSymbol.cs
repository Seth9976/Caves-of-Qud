using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Symbols;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004B2 RID: 1202
	internal abstract class NamespaceSymbol : NamespaceOrTypeSymbol, INamespace, INamedEntity, INamespaceSymbolInternal, INamespaceOrTypeSymbolInternal, ISymbolInternal
	{
		// Token: 0x170018D3 RID: 6355
		// (get) Token: 0x060065A8 RID: 26024 RVA: 0x001811EC File Offset: 0x0017F3EC
		INamespace INamespace.ContainingNamespace
		{
			get
			{
				NamespaceSymbol containingNamespace = this.AdaptedNamespaceSymbol.ContainingNamespace;
				if (containingNamespace == null)
				{
					return null;
				}
				return containingNamespace.GetCciAdapter();
			}
		}

		// Token: 0x170018D4 RID: 6356
		// (get) Token: 0x060065A9 RID: 26025 RVA: 0x00181204 File Offset: 0x0017F404
		string INamedEntity.Name
		{
			get
			{
				return this.AdaptedNamespaceSymbol.MetadataName;
			}
		}

		// Token: 0x060065AA RID: 26026 RVA: 0x00181211 File Offset: 0x0017F411
		INamespaceSymbolInternal INamespace.GetInternalSymbol()
		{
			return this.AdaptedNamespaceSymbol;
		}

		// Token: 0x170018D5 RID: 6357
		// (get) Token: 0x060065AB RID: 26027 RVA: 0x00181219 File Offset: 0x0017F419
		internal NamespaceSymbol AdaptedNamespaceSymbol
		{
			get
			{
				return this;
			}
		}

		// Token: 0x060065AC RID: 26028 RVA: 0x0018121C File Offset: 0x0017F41C
		internal new NamespaceSymbol GetCciAdapter()
		{
			return this;
		}

		// Token: 0x060065AD RID: 26029 RVA: 0x00181220 File Offset: 0x0017F420
		public IEnumerable<NamespaceSymbol> GetNamespaceMembers()
		{
			return this.GetMembers().OfType<NamespaceSymbol>();
		}

		// Token: 0x170018D6 RID: 6358
		// (get) Token: 0x060065AE RID: 26030 RVA: 0x0018123B File Offset: 0x0017F43B
		public virtual bool IsGlobalNamespace
		{
			get
			{
				return this.ContainingNamespace == null;
			}
		}

		// Token: 0x170018D7 RID: 6359
		// (get) Token: 0x060065AF RID: 26031
		internal abstract NamespaceExtent Extent { get; }

		// Token: 0x170018D8 RID: 6360
		// (get) Token: 0x060065B0 RID: 26032 RVA: 0x00181248 File Offset: 0x0017F448
		public NamespaceKind NamespaceKind
		{
			get
			{
				return this.Extent.Kind;
			}
		}

		// Token: 0x170018D9 RID: 6361
		// (get) Token: 0x060065B1 RID: 26033 RVA: 0x00181264 File Offset: 0x0017F464
		public CSharpCompilation ContainingCompilation
		{
			get
			{
				if (this.NamespaceKind != NamespaceKind.Compilation)
				{
					return null;
				}
				return this.Extent.Compilation;
			}
		}

		// Token: 0x170018DA RID: 6362
		// (get) Token: 0x060065B2 RID: 26034 RVA: 0x0018128A File Offset: 0x0017F48A
		public virtual ImmutableArray<NamespaceSymbol> ConstituentNamespaces
		{
			get
			{
				return ImmutableArray.Create<NamespaceSymbol>(this);
			}
		}

		// Token: 0x170018DB RID: 6363
		// (get) Token: 0x060065B3 RID: 26035 RVA: 0x00181292 File Offset: 0x0017F492
		public sealed override NamedTypeSymbol ContainingType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170018DC RID: 6364
		// (get) Token: 0x060065B4 RID: 26036
		public abstract override AssemblySymbol ContainingAssembly { get; }

		// Token: 0x170018DD RID: 6365
		// (get) Token: 0x060065B5 RID: 26037 RVA: 0x00181298 File Offset: 0x0017F498
		internal override ModuleSymbol ContainingModule
		{
			get
			{
				NamespaceExtent extent = this.Extent;
				if (extent.Kind == NamespaceKind.Module)
				{
					return extent.Module;
				}
				return null;
			}
		}

		// Token: 0x170018DE RID: 6366
		// (get) Token: 0x060065B6 RID: 26038 RVA: 0x001812BF File Offset: 0x0017F4BF
		public sealed override SymbolKind Kind
		{
			get
			{
				return SymbolKind.Namespace;
			}
		}

		// Token: 0x170018DF RID: 6367
		// (get) Token: 0x060065B7 RID: 26039 RVA: 0x001812C3 File Offset: 0x0017F4C3
		public sealed override bool IsImplicitlyDeclared
		{
			get
			{
				return this.IsGlobalNamespace;
			}
		}

		// Token: 0x060065B8 RID: 26040 RVA: 0x001812CB File Offset: 0x0017F4CB
		internal override TResult Accept<TArgument, TResult>(CSharpSymbolVisitor<TArgument, TResult> visitor, TArgument argument)
		{
			return visitor.VisitNamespace(this, argument);
		}

		// Token: 0x060065B9 RID: 26041 RVA: 0x001812D5 File Offset: 0x0017F4D5
		public override void Accept(CSharpSymbolVisitor visitor)
		{
			visitor.VisitNamespace(this);
		}

		// Token: 0x060065BA RID: 26042 RVA: 0x001812DE File Offset: 0x0017F4DE
		public override TResult Accept<TResult>(CSharpSymbolVisitor<TResult> visitor)
		{
			return visitor.VisitNamespace(this);
		}

		// Token: 0x060065BB RID: 26043 RVA: 0x001812E7 File Offset: 0x0017F4E7
		internal NamespaceSymbol()
		{
		}

		// Token: 0x170018E0 RID: 6368
		// (get) Token: 0x060065BC RID: 26044 RVA: 0x001812EF File Offset: 0x0017F4EF
		public sealed override Accessibility DeclaredAccessibility
		{
			get
			{
				return Accessibility.Public;
			}
		}

		// Token: 0x170018E1 RID: 6369
		// (get) Token: 0x060065BD RID: 26045 RVA: 0x001812F2 File Offset: 0x0017F4F2
		public sealed override bool IsStatic
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170018E2 RID: 6370
		// (get) Token: 0x060065BE RID: 26046 RVA: 0x001812F5 File Offset: 0x0017F4F5
		public sealed override bool IsAbstract
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170018E3 RID: 6371
		// (get) Token: 0x060065BF RID: 26047 RVA: 0x001812F8 File Offset: 0x0017F4F8
		public sealed override bool IsSealed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170018E4 RID: 6372
		// (get) Token: 0x060065C0 RID: 26048 RVA: 0x001812FB File Offset: 0x0017F4FB
		internal sealed override ObsoleteAttributeData ObsoleteAttributeData
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170018E5 RID: 6373
		// (get) Token: 0x060065C1 RID: 26049 RVA: 0x00181300 File Offset: 0x0017F500
		internal NamedTypeSymbol ImplicitType
		{
			get
			{
				ImmutableArray<NamedTypeSymbol> typeMembers = this.GetTypeMembers("<invalid-global-code>");
				if (typeMembers.Length == 0)
				{
					return null;
				}
				return typeMembers[0];
			}
		}

		// Token: 0x060065C2 RID: 26050 RVA: 0x0018132C File Offset: 0x0017F52C
		internal NamespaceSymbol LookupNestedNamespace(ImmutableArray<string> names)
		{
			NamespaceSymbol namespaceSymbol = this;
			foreach (string text in names)
			{
				NamespaceSymbol namespaceSymbol2 = null;
				foreach (Symbol symbol in namespaceSymbol.GetMembers(text))
				{
					NamespaceSymbol namespaceSymbol3 = ((NamespaceOrTypeSymbol)symbol) as NamespaceSymbol;
					if (namespaceSymbol3 != null)
					{
						if (namespaceSymbol2 != null)
						{
							namespaceSymbol2 = null;
							break;
						}
						namespaceSymbol2 = namespaceSymbol3;
					}
				}
				namespaceSymbol = namespaceSymbol2;
				if (namespaceSymbol == null)
				{
					break;
				}
			}
			return namespaceSymbol;
		}

		// Token: 0x060065C3 RID: 26051 RVA: 0x0018139C File Offset: 0x0017F59C
		internal NamespaceSymbol GetNestedNamespace(string name)
		{
			foreach (Symbol symbol in this.GetMembers(name))
			{
				if (symbol.Kind == SymbolKind.Namespace)
				{
					return (NamespaceSymbol)symbol;
				}
			}
			return null;
		}

		// Token: 0x060065C4 RID: 26052 RVA: 0x001813E0 File Offset: 0x0017F5E0
		internal NamespaceSymbol GetNestedNamespace(NameSyntax name)
		{
			switch (name.Kind())
			{
			case SyntaxKind.IdentifierName:
			case SyntaxKind.GenericName:
				return this.GetNestedNamespace(((SimpleNameSyntax)name).Identifier.ValueText);
			case SyntaxKind.QualifiedName:
			{
				QualifiedNameSyntax qualifiedNameSyntax = (QualifiedNameSyntax)name;
				NamespaceSymbol nestedNamespace = this.GetNestedNamespace(qualifiedNameSyntax.Left);
				if (nestedNamespace != null)
				{
					return nestedNamespace.GetNestedNamespace(qualifiedNameSyntax.Right);
				}
				break;
			}
			case SyntaxKind.AliasQualifiedName:
				return this.GetNestedNamespace(name.GetUnqualifiedName().Identifier.ValueText);
			}
			return null;
		}

		// Token: 0x170018E6 RID: 6374
		// (get) Token: 0x060065C5 RID: 26053 RVA: 0x00181470 File Offset: 0x0017F670
		private ImmutableArray<NamedTypeSymbol> TypesMightContainExtensionMethods
		{
			get
			{
				ImmutableArray<NamedTypeSymbol> immutableArray = this._lazyTypesMightContainExtensionMethods;
				if (immutableArray.IsDefault)
				{
					this._lazyTypesMightContainExtensionMethods = this.GetTypeMembersUnordered().WhereAsArray<NamedTypeSymbol>((NamedTypeSymbol t) => t.MightContainExtensionMethods);
					immutableArray = this._lazyTypesMightContainExtensionMethods;
				}
				return immutableArray;
			}
		}

		// Token: 0x060065C6 RID: 26054 RVA: 0x001814C8 File Offset: 0x0017F6C8
		internal virtual void GetExtensionMethods(ArrayBuilder<MethodSymbol> methods, string nameOpt, int arity, LookupOptions options)
		{
			if (!this.ContainingAssembly.MightContainExtensionMethods)
			{
				return;
			}
			foreach (NamedTypeSymbol namedTypeSymbol in this.TypesMightContainExtensionMethods)
			{
				namedTypeSymbol.DoGetExtensionMethods(methods, nameOpt, arity, options);
			}
		}

		// Token: 0x170018E7 RID: 6375
		// (get) Token: 0x060065C7 RID: 26055 RVA: 0x00181510 File Offset: 0x0017F710
		internal string QualifiedName
		{
			get
			{
				string text;
				if ((text = this._lazyQualifiedName) == null)
				{
					text = (this._lazyQualifiedName = base.ToDisplayString(SymbolDisplayFormat.QualifiedNameOnlyFormat));
				}
				return text;
			}
		}

		// Token: 0x060065C8 RID: 26056 RVA: 0x0018153B File Offset: 0x0017F73B
		protected sealed override ISymbol CreateISymbol()
		{
			return new NamespaceSymbol(this);
		}

		// Token: 0x170018E8 RID: 6376
		// (get) Token: 0x060065C9 RID: 26057 RVA: 0x00181543 File Offset: 0x0017F743
		bool INamespaceSymbolInternal.IsGlobalNamespace
		{
			get
			{
				return this.IsGlobalNamespace;
			}
		}

		// Token: 0x0400199E RID: 6558
		private ImmutableArray<NamedTypeSymbol> _lazyTypesMightContainExtensionMethods;

		// Token: 0x0400199F RID: 6559
		private string _lazyQualifiedName;
	}
}
