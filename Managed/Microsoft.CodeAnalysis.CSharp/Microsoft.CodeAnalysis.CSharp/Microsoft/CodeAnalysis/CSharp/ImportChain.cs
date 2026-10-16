using System;
using System.Collections.Immutable;
using System.Diagnostics;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200002B RID: 43
	[DebuggerDisplay("{GetDebuggerDisplay(),nq}")]
	internal sealed class ImportChain : IImportScope
	{
		// Token: 0x060005ED RID: 1517 RVA: 0x00038771 File Offset: 0x00036971
		public ImportChain(Imports imports, ImportChain parentOpt)
		{
			this.Imports = imports;
			this.ParentOpt = parentOpt;
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x00038787 File Offset: 0x00036987
		private string GetDebuggerDisplay()
		{
			string text = "{0} ^ {1}";
			object debuggerDisplay = this.Imports.GetDebuggerDisplay();
			ImportChain parentOpt = this.ParentOpt;
			return string.Format(text, debuggerDisplay, (parentOpt != null) ? parentOpt.GetHashCode() : 0);
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x000387B5 File Offset: 0x000369B5
		ImmutableArray<UsedNamespaceOrType> IImportScope.GetUsedNamespaces()
		{
			return this._lazyTranslatedImports;
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x000387C0 File Offset: 0x000369C0
		public IImportScope Translate(PEModuleBuilder moduleBuilder, DiagnosticBag diagnostics)
		{
			ImportChain importChain = this;
			while (importChain != null && importChain._lazyTranslatedImports.IsDefault)
			{
				ImmutableInterlocked.InterlockedInitialize<UsedNamespaceOrType>(ref importChain._lazyTranslatedImports, importChain.TranslateImports(moduleBuilder, diagnostics));
				importChain = importChain.ParentOpt;
			}
			return this;
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x00038800 File Offset: 0x00036A00
		private ImmutableArray<UsedNamespaceOrType> TranslateImports(PEModuleBuilder moduleBuilder, DiagnosticBag diagnostics)
		{
			ArrayBuilder<UsedNamespaceOrType> instance = ArrayBuilder<UsedNamespaceOrType>.GetInstance();
			ImmutableArray<AliasAndExternAliasDirective> externAliases = this.Imports.ExternAliases;
			if (!externAliases.IsDefault)
			{
				foreach (AliasAndExternAliasDirective aliasAndExternAliasDirective in externAliases)
				{
					instance.Add(UsedNamespaceOrType.CreateExternAlias(aliasAndExternAliasDirective.Alias.Name));
				}
			}
			ImmutableArray<NamespaceOrTypeAndUsingDirective> usings = this.Imports.Usings;
			if (!usings.IsDefault)
			{
				foreach (NamespaceOrTypeAndUsingDirective namespaceOrTypeAndUsingDirective in usings)
				{
					NamespaceOrTypeSymbol namespaceOrType = namespaceOrTypeAndUsingDirective.NamespaceOrType;
					if (namespaceOrType.IsNamespace)
					{
						NamespaceSymbol namespaceSymbol = (NamespaceSymbol)namespaceOrType;
						IAssemblyReference assemblyReference = ImportChain.TryGetAssemblyScope(namespaceSymbol, moduleBuilder, diagnostics);
						instance.Add(UsedNamespaceOrType.CreateNamespace(namespaceSymbol.GetCciAdapter(), assemblyReference, null));
					}
					else if (!namespaceOrType.ContainingAssembly.IsLinked)
					{
						ITypeReference typeReference = ImportChain.GetTypeReference((TypeSymbol)namespaceOrType, namespaceOrTypeAndUsingDirective.UsingDirective, moduleBuilder, diagnostics);
						instance.Add(UsedNamespaceOrType.CreateType(typeReference, null));
					}
				}
			}
			ImmutableDictionary<string, AliasAndUsingDirective> usingAliases = this.Imports.UsingAliases;
			if (!usingAliases.IsEmpty)
			{
				ArrayBuilder<string> instance2 = ArrayBuilder<string>.GetInstance(usingAliases.Count);
				instance2.AddRange(usingAliases.Keys);
				instance2.Sort(StringComparer.Ordinal);
				foreach (string text in instance2)
				{
					AliasAndUsingDirective aliasAndUsingDirective = usingAliases[text];
					AliasSymbol alias = aliasAndUsingDirective.Alias;
					UsingDirectiveSyntax usingDirective = aliasAndUsingDirective.UsingDirective;
					NamespaceOrTypeSymbol target = alias.Target;
					if (target.Kind == SymbolKind.Namespace)
					{
						NamespaceSymbol namespaceSymbol2 = (NamespaceSymbol)target;
						IAssemblyReference assemblyReference2 = ImportChain.TryGetAssemblyScope(namespaceSymbol2, moduleBuilder, diagnostics);
						instance.Add(UsedNamespaceOrType.CreateNamespace(namespaceSymbol2.GetCciAdapter(), assemblyReference2, text));
					}
					else if (!target.ContainingAssembly.IsLinked)
					{
						ITypeReference typeReference2 = ImportChain.GetTypeReference((TypeSymbol)target, usingDirective, moduleBuilder, diagnostics);
						instance.Add(UsedNamespaceOrType.CreateType(typeReference2, text));
					}
				}
				instance2.Free();
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x000389F8 File Offset: 0x00036BF8
		private static ITypeReference GetTypeReference(TypeSymbol type, SyntaxNode syntaxNode, PEModuleBuilder moduleBuilder, DiagnosticBag diagnostics)
		{
			return moduleBuilder.Translate(type, syntaxNode, diagnostics);
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x00038A04 File Offset: 0x00036C04
		private static IAssemblyReference TryGetAssemblyScope(NamespaceSymbol @namespace, PEModuleBuilder moduleBuilder, DiagnosticBag diagnostics)
		{
			AssemblySymbol containingAssembly = @namespace.ContainingAssembly;
			if (containingAssembly != null && containingAssembly != moduleBuilder.CommonCompilation.Assembly)
			{
				CSharpCompilation.ReferenceManager boundReferenceManager = ((CSharpCompilation)moduleBuilder.CommonCompilation).GetBoundReferenceManager();
				for (int i = 0; i < boundReferenceManager.ReferencedAssemblies.Length; i++)
				{
					if (boundReferenceManager.ReferencedAssemblies[i] == containingAssembly && !boundReferenceManager.DeclarationsAccessibleWithoutAlias(i))
					{
						return moduleBuilder.Translate(containingAssembly, diagnostics);
					}
				}
			}
			return null;
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060005F4 RID: 1524 RVA: 0x00038A78 File Offset: 0x00036C78
		IImportScope IImportScope.Parent
		{
			get
			{
				return this.ParentOpt;
			}
		}

		// Token: 0x0400007A RID: 122
		public readonly Imports Imports;

		// Token: 0x0400007B RID: 123
		public readonly ImportChain ParentOpt;

		// Token: 0x0400007C RID: 124
		private ImmutableArray<UsedNamespaceOrType> _lazyTranslatedImports;
	}
}
