using System;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004BE RID: 1214
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class AliasSymbol : Symbol
	{
		// Token: 0x060067C2 RID: 26562 RVA: 0x00186DE2 File Offset: 0x00184FE2
		protected AliasSymbol(string aliasName, [global::System.Runtime.CompilerServices.Nullable(2)] Symbol containingSymbol, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<Location> locations, bool isExtern)
		{
			this._locations = locations;
			this._aliasName = aliasName;
			this._isExtern = isExtern;
			this._containingSymbol = containingSymbol;
		}

		// Token: 0x060067C3 RID: 26563 RVA: 0x00186E07 File Offset: 0x00185007
		internal static AliasSymbol CreateGlobalNamespaceAlias(NamespaceSymbol globalNamespace)
		{
			return new AliasSymbolFromResolvedTarget(globalNamespace, "global", globalNamespace, ImmutableArray<Location>.Empty, false);
		}

		// Token: 0x060067C4 RID: 26564 RVA: 0x00186E1B File Offset: 0x0018501B
		internal static AliasSymbol CreateCustomDebugInfoAlias(NamespaceOrTypeSymbol targetSymbol, SyntaxToken aliasToken, [global::System.Runtime.CompilerServices.Nullable(2)] Symbol containingSymbol, bool isExtern)
		{
			return new AliasSymbolFromResolvedTarget(targetSymbol, aliasToken.ValueText, containingSymbol, ImmutableArray.Create<Location>(aliasToken.GetLocation()), isExtern);
		}

		// Token: 0x060067C5 RID: 26565 RVA: 0x00186E38 File Offset: 0x00185038
		internal AliasSymbol ToNewSubmission(CSharpCompilation compilation)
		{
			NamespaceOrTypeSymbol target = this.Target;
			if (target.Kind != SymbolKind.Namespace)
			{
				return this;
			}
			NamespaceSymbol globalNamespace = compilation.GlobalNamespace;
			return new AliasSymbolFromResolvedTarget(Imports.ExpandPreviousSubmissionNamespace((NamespaceSymbol)target, globalNamespace), this.Name, this.ContainingSymbol, this._locations, this._isExtern);
		}

		// Token: 0x17001A06 RID: 6662
		// (get) Token: 0x060067C6 RID: 26566 RVA: 0x00186E88 File Offset: 0x00185088
		public sealed override string Name
		{
			get
			{
				return this._aliasName;
			}
		}

		// Token: 0x17001A07 RID: 6663
		// (get) Token: 0x060067C7 RID: 26567 RVA: 0x00186E90 File Offset: 0x00185090
		public override SymbolKind Kind
		{
			get
			{
				return SymbolKind.Alias;
			}
		}

		// Token: 0x17001A08 RID: 6664
		// (get) Token: 0x060067C8 RID: 26568
		public abstract NamespaceOrTypeSymbol Target { get; }

		// Token: 0x17001A09 RID: 6665
		// (get) Token: 0x060067C9 RID: 26569 RVA: 0x00186E93 File Offset: 0x00185093
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<Location> Locations
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._locations;
			}
		}

		// Token: 0x17001A0A RID: 6666
		// (get) Token: 0x060067CA RID: 26570 RVA: 0x00186E9B File Offset: 0x0018509B
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return Symbol.GetDeclaringSyntaxReferenceHelper<UsingDirectiveSyntax>(this._locations);
			}
		}

		// Token: 0x17001A0B RID: 6667
		// (get) Token: 0x060067CB RID: 26571 RVA: 0x00186EA8 File Offset: 0x001850A8
		public sealed override bool IsExtern
		{
			get
			{
				return this._isExtern;
			}
		}

		// Token: 0x17001A0C RID: 6668
		// (get) Token: 0x060067CC RID: 26572 RVA: 0x00186EB0 File Offset: 0x001850B0
		public override bool IsSealed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001A0D RID: 6669
		// (get) Token: 0x060067CD RID: 26573 RVA: 0x00186EB3 File Offset: 0x001850B3
		public override bool IsAbstract
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001A0E RID: 6670
		// (get) Token: 0x060067CE RID: 26574 RVA: 0x00186EB6 File Offset: 0x001850B6
		public override bool IsOverride
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001A0F RID: 6671
		// (get) Token: 0x060067CF RID: 26575 RVA: 0x00186EB9 File Offset: 0x001850B9
		public override bool IsVirtual
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001A10 RID: 6672
		// (get) Token: 0x060067D0 RID: 26576 RVA: 0x00186EBC File Offset: 0x001850BC
		public override bool IsStatic
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001A11 RID: 6673
		// (get) Token: 0x060067D1 RID: 26577 RVA: 0x00186EBF File Offset: 0x001850BF
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal sealed override ObsoleteAttributeData ObsoleteAttributeData
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17001A12 RID: 6674
		// (get) Token: 0x060067D2 RID: 26578 RVA: 0x00186EC2 File Offset: 0x001850C2
		public override Accessibility DeclaredAccessibility
		{
			get
			{
				return Accessibility.NotApplicable;
			}
		}

		// Token: 0x17001A13 RID: 6675
		// (get) Token: 0x060067D3 RID: 26579 RVA: 0x00186EC5 File Offset: 0x001850C5
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public sealed override Symbol ContainingSymbol
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this._containingSymbol;
			}
		}

		// Token: 0x060067D4 RID: 26580 RVA: 0x00186ECD File Offset: 0x001850CD
		internal override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TArg, [global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSymbolVisitor<TArg, TResult> visitor, TArg a)
		{
			return visitor.VisitAlias(this, a);
		}

		// Token: 0x060067D5 RID: 26581 RVA: 0x00186ED7 File Offset: 0x001850D7
		public override void Accept(CSharpSymbolVisitor visitor)
		{
			visitor.VisitAlias(this);
		}

		// Token: 0x060067D6 RID: 26582 RVA: 0x00186EE0 File Offset: 0x001850E0
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSymbolVisitor<TResult> visitor)
		{
			return visitor.VisitAlias(this);
		}

		// Token: 0x060067D7 RID: 26583
		internal abstract NamespaceOrTypeSymbol GetAliasTarget([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ConsList<TypeSymbol> basesBeingResolved);

		// Token: 0x060067D8 RID: 26584 RVA: 0x00186EEC File Offset: 0x001850EC
		internal void CheckConstraints(BindingDiagnosticBag diagnostics)
		{
			TypeSymbol typeSymbol = this.Target as TypeSymbol;
			if (typeSymbol != null && this.Locations.Length > 0)
			{
				TypeConversions typeConversions = new TypeConversions(this.ContainingAssembly.CorLibrary, false);
				typeSymbol.CheckAllConstraints(this.DeclaringCompilation, typeConversions, this.Locations[0], diagnostics);
			}
		}

		// Token: 0x060067D9 RID: 26585 RVA: 0x00186F48 File Offset: 0x00185148
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override bool Equals(Symbol obj, TypeCompareKind compareKind)
		{
			if (this == obj)
			{
				return true;
			}
			if (obj == null)
			{
				return false;
			}
			AliasSymbol aliasSymbol = obj as AliasSymbol;
			return aliasSymbol != null && object.Equals(this.Locations.FirstOrDefault<Location>(), aliasSymbol.Locations.FirstOrDefault<Location>()) && this.ContainingAssembly.Equals(aliasSymbol.ContainingAssembly, compareKind);
		}

		// Token: 0x060067DA RID: 26586 RVA: 0x00186F9C File Offset: 0x0018519C
		public override int GetHashCode()
		{
			if (this.Locations.Length > 0)
			{
				return this.Locations.First<Location>().GetHashCode();
			}
			return this.Name.GetHashCode();
		}

		// Token: 0x17001A14 RID: 6676
		// (get) Token: 0x060067DB RID: 26587
		internal abstract override bool RequiresCompletion { get; }

		// Token: 0x060067DC RID: 26588 RVA: 0x00186FD6 File Offset: 0x001851D6
		protected override ISymbol CreateISymbol()
		{
			return new AliasSymbol(this);
		}

		// Token: 0x040019BE RID: 6590
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private readonly ImmutableArray<Location> _locations;

		// Token: 0x040019BF RID: 6591
		private readonly string _aliasName;

		// Token: 0x040019C0 RID: 6592
		private readonly bool _isExtern;

		// Token: 0x040019C1 RID: 6593
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private readonly Symbol _containingSymbol;
	}
}
