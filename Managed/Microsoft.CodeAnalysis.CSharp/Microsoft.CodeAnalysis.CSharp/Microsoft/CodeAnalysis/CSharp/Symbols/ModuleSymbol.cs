using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;
using Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel;
using Microsoft.CodeAnalysis.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004F9 RID: 1273
	internal abstract class ModuleSymbol : Symbol, IModuleSymbolInternal, ISymbolInternal
	{
		// Token: 0x17001BBE RID: 7102
		// (get) Token: 0x06006BC4 RID: 27588
		public abstract NamespaceSymbol GlobalNamespace { get; }

		// Token: 0x17001BBF RID: 7103
		// (get) Token: 0x06006BC5 RID: 27589 RVA: 0x0018F6DD File Offset: 0x0018D8DD
		public override AssemblySymbol ContainingAssembly
		{
			get
			{
				return (AssemblySymbol)this.ContainingSymbol;
			}
		}

		// Token: 0x17001BC0 RID: 7104
		// (get) Token: 0x06006BC6 RID: 27590 RVA: 0x0018F6EA File Offset: 0x0018D8EA
		internal sealed override ModuleSymbol ContainingModule
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001BC1 RID: 7105
		// (get) Token: 0x06006BC7 RID: 27591 RVA: 0x0018F6ED File Offset: 0x0018D8ED
		public sealed override SymbolKind Kind
		{
			get
			{
				return SymbolKind.NetModule;
			}
		}

		// Token: 0x06006BC8 RID: 27592 RVA: 0x0018F6F1 File Offset: 0x0018D8F1
		internal override TResult Accept<TArgument, TResult>(CSharpSymbolVisitor<TArgument, TResult> visitor, TArgument argument)
		{
			return visitor.VisitModule(this, argument);
		}

		// Token: 0x06006BC9 RID: 27593 RVA: 0x0018F6FB File Offset: 0x0018D8FB
		public override void Accept(CSharpSymbolVisitor visitor)
		{
			visitor.VisitModule(this);
		}

		// Token: 0x06006BCA RID: 27594 RVA: 0x0018F704 File Offset: 0x0018D904
		public override TResult Accept<TResult>(CSharpSymbolVisitor<TResult> visitor)
		{
			return visitor.VisitModule(this);
		}

		// Token: 0x06006BCB RID: 27595 RVA: 0x0018F70D File Offset: 0x0018D90D
		internal ModuleSymbol()
		{
		}

		// Token: 0x17001BC2 RID: 7106
		// (get) Token: 0x06006BCC RID: 27596
		internal abstract int Ordinal { get; }

		// Token: 0x17001BC3 RID: 7107
		// (get) Token: 0x06006BCD RID: 27597
		internal abstract Machine Machine { get; }

		// Token: 0x17001BC4 RID: 7108
		// (get) Token: 0x06006BCE RID: 27598
		internal abstract bool Bit32Required { get; }

		// Token: 0x17001BC5 RID: 7109
		// (get) Token: 0x06006BCF RID: 27599
		internal abstract bool IsMissing { get; }

		// Token: 0x17001BC6 RID: 7110
		// (get) Token: 0x06006BD0 RID: 27600 RVA: 0x0018F715 File Offset: 0x0018D915
		public sealed override Accessibility DeclaredAccessibility
		{
			get
			{
				return Accessibility.NotApplicable;
			}
		}

		// Token: 0x17001BC7 RID: 7111
		// (get) Token: 0x06006BD1 RID: 27601 RVA: 0x0018F718 File Offset: 0x0018D918
		public sealed override bool IsStatic
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001BC8 RID: 7112
		// (get) Token: 0x06006BD2 RID: 27602 RVA: 0x0018F71B File Offset: 0x0018D91B
		public sealed override bool IsVirtual
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001BC9 RID: 7113
		// (get) Token: 0x06006BD3 RID: 27603 RVA: 0x0018F71E File Offset: 0x0018D91E
		public sealed override bool IsOverride
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001BCA RID: 7114
		// (get) Token: 0x06006BD4 RID: 27604 RVA: 0x0018F721 File Offset: 0x0018D921
		public sealed override bool IsAbstract
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001BCB RID: 7115
		// (get) Token: 0x06006BD5 RID: 27605 RVA: 0x0018F724 File Offset: 0x0018D924
		public sealed override bool IsSealed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001BCC RID: 7116
		// (get) Token: 0x06006BD6 RID: 27606 RVA: 0x0018F727 File Offset: 0x0018D927
		public sealed override bool IsExtern
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001BCD RID: 7117
		// (get) Token: 0x06006BD7 RID: 27607 RVA: 0x0018F72A File Offset: 0x0018D92A
		internal sealed override ObsoleteAttributeData ObsoleteAttributeData
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001BCE RID: 7118
		// (get) Token: 0x06006BD8 RID: 27608 RVA: 0x0018F72D File Offset: 0x0018D92D
		public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			get
			{
				return ImmutableArray<SyntaxReference>.Empty;
			}
		}

		// Token: 0x17001BCF RID: 7119
		// (get) Token: 0x06006BD9 RID: 27609 RVA: 0x0018F734 File Offset: 0x0018D934
		public ImmutableArray<AssemblyIdentity> ReferencedAssemblies
		{
			get
			{
				return this.GetReferencedAssemblies();
			}
		}

		// Token: 0x06006BDA RID: 27610
		internal abstract ImmutableArray<AssemblyIdentity> GetReferencedAssemblies();

		// Token: 0x17001BD0 RID: 7120
		// (get) Token: 0x06006BDB RID: 27611 RVA: 0x0018F73C File Offset: 0x0018D93C
		public ImmutableArray<AssemblySymbol> ReferencedAssemblySymbols
		{
			get
			{
				return this.GetReferencedAssemblySymbols();
			}
		}

		// Token: 0x06006BDC RID: 27612
		internal abstract ImmutableArray<AssemblySymbol> GetReferencedAssemblySymbols();

		// Token: 0x06006BDD RID: 27613 RVA: 0x0018F744 File Offset: 0x0018D944
		internal AssemblySymbol GetReferencedAssemblySymbol(int referencedAssemblyIndex)
		{
			ImmutableArray<AssemblySymbol> referencedAssemblySymbols = this.GetReferencedAssemblySymbols();
			if (referencedAssemblyIndex < referencedAssemblySymbols.Length)
			{
				return referencedAssemblySymbols[referencedAssemblyIndex];
			}
			AssemblySymbol containingAssembly = this.ContainingAssembly;
			if (containingAssembly != containingAssembly.CorLibrary)
			{
				throw new ArgumentOutOfRangeException("referencedAssemblyIndex");
			}
			return null;
		}

		// Token: 0x06006BDE RID: 27614
		internal abstract void SetReferences(ModuleReferences<AssemblySymbol> moduleReferences, SourceAssemblySymbol originatingSourceAssemblyDebugOnly = null);

		// Token: 0x17001BD1 RID: 7121
		// (get) Token: 0x06006BDF RID: 27615
		internal abstract bool HasUnifiedReferences { get; }

		// Token: 0x06006BE0 RID: 27616
		internal abstract bool GetUnificationUseSiteDiagnostic(ref DiagnosticInfo result, TypeSymbol dependentType);

		// Token: 0x06006BE1 RID: 27617
		internal abstract NamedTypeSymbol LookupTopLevelMetadataType(ref MetadataTypeName emittedName);

		// Token: 0x17001BD2 RID: 7122
		// (get) Token: 0x06006BE2 RID: 27618
		internal abstract ICollection<string> TypeNames { get; }

		// Token: 0x17001BD3 RID: 7123
		// (get) Token: 0x06006BE3 RID: 27619
		internal abstract ICollection<string> NamespaceNames { get; }

		// Token: 0x17001BD4 RID: 7124
		// (get) Token: 0x06006BE4 RID: 27620
		internal abstract bool HasAssemblyCompilationRelaxationsAttribute { get; }

		// Token: 0x17001BD5 RID: 7125
		// (get) Token: 0x06006BE5 RID: 27621
		internal abstract bool HasAssemblyRuntimeCompatibilityAttribute { get; }

		// Token: 0x17001BD6 RID: 7126
		// (get) Token: 0x06006BE6 RID: 27622
		internal abstract CharSet? DefaultMarshallingCharSet { get; }

		// Token: 0x06006BE7 RID: 27623 RVA: 0x0018F787 File Offset: 0x0018D987
		internal virtual ImmutableArray<byte> GetHash(AssemblyHashAlgorithm algorithmId)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06006BE8 RID: 27624 RVA: 0x0018F790 File Offset: 0x0018D990
		public NamespaceSymbol GetModuleNamespace(INamespaceSymbol namespaceSymbol)
		{
			if (namespaceSymbol == null)
			{
				throw new ArgumentNullException("namespaceSymbol");
			}
			if (namespaceSymbol.NamespaceKind == NamespaceKind.Module)
			{
				NamespaceSymbol namespaceSymbol2 = namespaceSymbol as NamespaceSymbol;
				NamespaceSymbol namespaceSymbol3 = ((namespaceSymbol2 != null) ? namespaceSymbol2.UnderlyingNamespaceSymbol : null);
				if (namespaceSymbol3 != null && namespaceSymbol3.ContainingModule == this)
				{
					return namespaceSymbol3;
				}
			}
			if (namespaceSymbol.IsGlobalNamespace || namespaceSymbol.ContainingNamespace == null)
			{
				return this.GlobalNamespace;
			}
			NamespaceSymbol moduleNamespace = this.GetModuleNamespace(namespaceSymbol.ContainingNamespace);
			if (moduleNamespace != null)
			{
				return moduleNamespace.GetNestedNamespace(namespaceSymbol.Name);
			}
			return null;
		}

		// Token: 0x06006BE9 RID: 27625 RVA: 0x0018F810 File Offset: 0x0018DA10
		public NamespaceSymbol GetModuleNamespace(NamespaceSymbol namespaceSymbol)
		{
			if (namespaceSymbol == null)
			{
				throw new ArgumentNullException("namespaceSymbol");
			}
			if (namespaceSymbol.Extent.Kind == NamespaceKind.Module && namespaceSymbol.ContainingModule == this)
			{
				return namespaceSymbol;
			}
			if (namespaceSymbol.IsGlobalNamespace || namespaceSymbol.ContainingNamespace == null)
			{
				return this.GlobalNamespace;
			}
			NamespaceSymbol moduleNamespace = this.GetModuleNamespace(namespaceSymbol.ContainingNamespace);
			if (moduleNamespace != null)
			{
				return moduleNamespace.GetNestedNamespace(namespaceSymbol.Name);
			}
			return null;
		}

		// Token: 0x17001BD7 RID: 7127
		// (get) Token: 0x06006BEA RID: 27626
		public abstract bool AreLocalsZeroed { get; }

		// Token: 0x06006BEB RID: 27627
		public abstract ModuleMetadata GetMetadata();

		// Token: 0x06006BEC RID: 27628 RVA: 0x0018F887 File Offset: 0x0018DA87
		protected override ISymbol CreateISymbol()
		{
			return new ModuleSymbol(this);
		}
	}
}
