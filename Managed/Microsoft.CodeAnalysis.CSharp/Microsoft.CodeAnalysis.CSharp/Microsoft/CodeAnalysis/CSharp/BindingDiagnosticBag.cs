using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000012 RID: 18
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
	internal sealed class BindingDiagnosticBag : BindingDiagnosticBag<AssemblySymbol>
	{
		// Token: 0x060004D5 RID: 1237 RVA: 0x00032F54 File Offset: 0x00031154
		public BindingDiagnosticBag()
			: this(false)
		{
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x00032F5D File Offset: 0x0003115D
		private BindingDiagnosticBag(bool usePool)
			: base(usePool)
		{
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x00032F66 File Offset: 0x00031166
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public BindingDiagnosticBag(DiagnosticBag diagnosticBag)
			: base(diagnosticBag, null)
		{
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x00032F70 File Offset: 0x00031170
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public BindingDiagnosticBag(DiagnosticBag diagnosticBag, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ICollection<AssemblySymbol> dependenciesBag)
			: base(diagnosticBag, dependenciesBag)
		{
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00032F7A File Offset: 0x0003117A
		internal static BindingDiagnosticBag GetInstance()
		{
			return new BindingDiagnosticBag(true);
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x00032F82 File Offset: 0x00031182
		internal static BindingDiagnosticBag GetInstance(bool withDiagnostics, bool withDependencies)
		{
			if (withDiagnostics)
			{
				if (withDependencies)
				{
					return BindingDiagnosticBag.GetInstance();
				}
				return new BindingDiagnosticBag(DiagnosticBag.GetInstance());
			}
			else
			{
				if (withDependencies)
				{
					return new BindingDiagnosticBag(null, PooledHashSet<AssemblySymbol>.GetInstance());
				}
				return BindingDiagnosticBag.Discarded;
			}
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x00032FAF File Offset: 0x000311AF
		internal static BindingDiagnosticBag GetInstance(BindingDiagnosticBag template)
		{
			return BindingDiagnosticBag.GetInstance(template.AccumulatesDiagnostics, template.AccumulatesDependencies);
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x00032FC2 File Offset: 0x000311C2
		internal static BindingDiagnosticBag Create(BindingDiagnosticBag template)
		{
			if (template.AccumulatesDiagnostics)
			{
				if (template.AccumulatesDependencies)
				{
					return new BindingDiagnosticBag();
				}
				return new BindingDiagnosticBag(new DiagnosticBag());
			}
			else
			{
				if (template.AccumulatesDependencies)
				{
					return new BindingDiagnosticBag(null, new HashSet<AssemblySymbol>());
				}
				return BindingDiagnosticBag.Discarded;
			}
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00032FFE File Offset: 0x000311FE
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal void AddDependencies(Symbol symbol)
		{
			if (symbol != null && this.DependenciesBag != null)
			{
				base.AddDependencies(symbol.GetUseSiteInfo());
			}
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00033017 File Offset: 0x00031217
		internal bool ReportUseSite([global::System.Runtime.CompilerServices.Nullable(2)] Symbol symbol, SyntaxNode node)
		{
			return this.ReportUseSite(symbol, node.Location);
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00033026 File Offset: 0x00031226
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal bool ReportUseSite(Symbol symbol, SyntaxToken token)
		{
			return this.ReportUseSite(symbol, token.GetLocation());
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00033036 File Offset: 0x00031236
		internal bool ReportUseSite([global::System.Runtime.CompilerServices.Nullable(2)] Symbol symbol, Location location)
		{
			return symbol != null && base.Add(symbol.GetUseSiteInfo(), location);
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0003304A File Offset: 0x0003124A
		internal void AddAssembliesUsedByNamespaceReference(NamespaceSymbol ns)
		{
			if (this.DependenciesBag == null)
			{
				return;
			}
			this.<AddAssembliesUsedByNamespaceReference>g__addAssembliesUsedByNamespaceReferenceImpl|13_0(ns);
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0003305C File Offset: 0x0003125C
		protected override bool ReportUseSiteDiagnostic(DiagnosticInfo diagnosticInfo, DiagnosticBag diagnosticBag, Location location)
		{
			return Symbol.ReportUseSiteDiagnostic(diagnosticInfo, diagnosticBag, location);
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x00033068 File Offset: 0x00031268
		internal CSDiagnosticInfo Add(ErrorCode code, Location location)
		{
			CSDiagnosticInfo csdiagnosticInfo = new CSDiagnosticInfo(code);
			this.Add(csdiagnosticInfo, location);
			return csdiagnosticInfo;
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x00033088 File Offset: 0x00031288
		internal CSDiagnosticInfo Add(ErrorCode code, Location location, params object[] args)
		{
			CSDiagnosticInfo csdiagnosticInfo = new CSDiagnosticInfo(code, args);
			this.Add(csdiagnosticInfo, location);
			return csdiagnosticInfo;
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x000330A8 File Offset: 0x000312A8
		internal CSDiagnosticInfo Add(ErrorCode code, Location location, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<Symbol> symbols, params object[] args)
		{
			CSDiagnosticInfo csdiagnosticInfo = new CSDiagnosticInfo(code, args, symbols, ImmutableArray<Location>.Empty);
			this.Add(csdiagnosticInfo, location);
			return csdiagnosticInfo;
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x000330CD File Offset: 0x000312CD
		internal void Add([global::System.Runtime.CompilerServices.Nullable(2)] DiagnosticInfo info, Location location)
		{
			if (info != null)
			{
				DiagnosticBag diagnosticBag = this.DiagnosticBag;
				if (diagnosticBag == null)
				{
					return;
				}
				diagnosticBag.Add(info, location);
			}
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x000330F4 File Offset: 0x000312F4
		[CompilerGenerated]
		private void <AddAssembliesUsedByNamespaceReference>g__addAssembliesUsedByNamespaceReferenceImpl|13_0(NamespaceSymbol ns)
		{
			if (ns.Extent.Kind == NamespaceKind.Compilation)
			{
				foreach (NamespaceSymbol namespaceSymbol in ns.ConstituentNamespaces)
				{
					this.<AddAssembliesUsedByNamespaceReference>g__addAssembliesUsedByNamespaceReferenceImpl|13_0(namespaceSymbol);
				}
				return;
			}
			AssemblySymbol containingAssembly = ns.ContainingAssembly;
			if (containingAssembly != null && !containingAssembly.IsMissing)
			{
				this.DependenciesBag.Add(containingAssembly);
			}
		}

		// Token: 0x04000043 RID: 67
		public static readonly BindingDiagnosticBag Discarded = new BindingDiagnosticBag(null, null);
	}
}
