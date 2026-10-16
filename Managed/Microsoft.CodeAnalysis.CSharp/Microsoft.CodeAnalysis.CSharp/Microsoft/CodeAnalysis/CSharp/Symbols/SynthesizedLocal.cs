using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000598 RID: 1432
	[DebuggerDisplay("{GetDebuggerDisplay(),nq}")]
	internal sealed class SynthesizedLocal : LocalSymbol
	{
		// Token: 0x060077DB RID: 30683 RVA: 0x001B876E File Offset: 0x001B696E
		internal SynthesizedLocal(MethodSymbol containingMethodOpt, TypeWithAnnotations type, SynthesizedLocalKind kind, SyntaxNode syntaxOpt = null, bool isPinned = false, RefKind refKind = RefKind.None)
		{
			this._containingMethodOpt = containingMethodOpt;
			this._type = type;
			this._kind = kind;
			this._syntaxOpt = syntaxOpt;
			this._isPinned = isPinned;
			this._refKind = refKind;
		}

		// Token: 0x170021F2 RID: 8690
		// (get) Token: 0x060077DC RID: 30684 RVA: 0x001B87A3 File Offset: 0x001B69A3
		public SyntaxNode SyntaxOpt
		{
			get
			{
				return this._syntaxOpt;
			}
		}

		// Token: 0x060077DD RID: 30685 RVA: 0x001B87AB File Offset: 0x001B69AB
		internal override LocalSymbol WithSynthesizedLocalKindAndSyntax(SynthesizedLocalKind kind, SyntaxNode syntax)
		{
			return new SynthesizedLocal(this._containingMethodOpt, this._type, kind, syntax, this._isPinned, this._refKind);
		}

		// Token: 0x170021F3 RID: 8691
		// (get) Token: 0x060077DE RID: 30686 RVA: 0x001B87CC File Offset: 0x001B69CC
		public override RefKind RefKind
		{
			get
			{
				return this._refKind;
			}
		}

		// Token: 0x170021F4 RID: 8692
		// (get) Token: 0x060077DF RID: 30687 RVA: 0x001B87D4 File Offset: 0x001B69D4
		internal override bool IsImportedFromMetadata
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170021F5 RID: 8693
		// (get) Token: 0x060077E0 RID: 30688 RVA: 0x001B87D7 File Offset: 0x001B69D7
		internal override LocalDeclarationKind DeclarationKind
		{
			get
			{
				return LocalDeclarationKind.None;
			}
		}

		// Token: 0x170021F6 RID: 8694
		// (get) Token: 0x060077E1 RID: 30689 RVA: 0x001B87DA File Offset: 0x001B69DA
		internal override SynthesizedLocalKind SynthesizedKind
		{
			get
			{
				return this._kind;
			}
		}

		// Token: 0x170021F7 RID: 8695
		// (get) Token: 0x060077E2 RID: 30690 RVA: 0x001B87E2 File Offset: 0x001B69E2
		internal override SyntaxNode ScopeDesignatorOpt
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170021F8 RID: 8696
		// (get) Token: 0x060077E3 RID: 30691 RVA: 0x001B87E8 File Offset: 0x001B69E8
		internal override SyntaxToken IdentifierToken
		{
			get
			{
				return default(SyntaxToken);
			}
		}

		// Token: 0x170021F9 RID: 8697
		// (get) Token: 0x060077E4 RID: 30692 RVA: 0x001B87FE File Offset: 0x001B69FE
		public override Symbol ContainingSymbol
		{
			get
			{
				return this._containingMethodOpt;
			}
		}

		// Token: 0x170021FA RID: 8698
		// (get) Token: 0x060077E5 RID: 30693 RVA: 0x001B8806 File Offset: 0x001B6A06
		public override string Name
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170021FB RID: 8699
		// (get) Token: 0x060077E6 RID: 30694 RVA: 0x001B8809 File Offset: 0x001B6A09
		public override TypeWithAnnotations TypeWithAnnotations
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x170021FC RID: 8700
		// (get) Token: 0x060077E7 RID: 30695 RVA: 0x001B8811 File Offset: 0x001B6A11
		public override ImmutableArray<Location> Locations
		{
			get
			{
				if (this._syntaxOpt != null)
				{
					return ImmutableArray.Create<Location>(this._syntaxOpt.GetLocation());
				}
				return ImmutableArray<Location>.Empty;
			}
		}

		// Token: 0x170021FD RID: 8701
		// (get) Token: 0x060077E8 RID: 30696 RVA: 0x001B8831 File Offset: 0x001B6A31
		public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			get
			{
				if (this._syntaxOpt != null)
				{
					return ImmutableArray.Create<SyntaxReference>(this._syntaxOpt.GetReference());
				}
				return ImmutableArray<SyntaxReference>.Empty;
			}
		}

		// Token: 0x060077E9 RID: 30697 RVA: 0x001B8851 File Offset: 0x001B6A51
		internal override SyntaxNode GetDeclaratorSyntax()
		{
			return this._syntaxOpt;
		}

		// Token: 0x170021FE RID: 8702
		// (get) Token: 0x060077EA RID: 30698 RVA: 0x001B8859 File Offset: 0x001B6A59
		public override bool IsImplicitlyDeclared
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170021FF RID: 8703
		// (get) Token: 0x060077EB RID: 30699 RVA: 0x001B885C File Offset: 0x001B6A5C
		internal override bool IsPinned
		{
			get
			{
				return this._isPinned;
			}
		}

		// Token: 0x17002200 RID: 8704
		// (get) Token: 0x060077EC RID: 30700 RVA: 0x001B8864 File Offset: 0x001B6A64
		internal override bool IsCompilerGenerated
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17002201 RID: 8705
		// (get) Token: 0x060077ED RID: 30701 RVA: 0x001B8867 File Offset: 0x001B6A67
		internal override uint ValEscapeScope
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17002202 RID: 8706
		// (get) Token: 0x060077EE RID: 30702 RVA: 0x001B886E File Offset: 0x001B6A6E
		internal override uint RefEscapeScope
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x060077EF RID: 30703 RVA: 0x001B8875 File Offset: 0x001B6A75
		internal override ConstantValue GetConstantValue(SyntaxNode node, LocalSymbol inProgress, BindingDiagnosticBag diagnostics)
		{
			return null;
		}

		// Token: 0x060077F0 RID: 30704 RVA: 0x001B8878 File Offset: 0x001B6A78
		internal override ImmutableBindingDiagnostic<AssemblySymbol> GetConstantValueDiagnostics(BoundExpression boundInitValue)
		{
			return ImmutableBindingDiagnostic<AssemblySymbol>.Empty;
		}

		// Token: 0x060077F1 RID: 30705 RVA: 0x001B8880 File Offset: 0x001B6A80
		internal override string GetDebuggerDisplay()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('<');
			stringBuilder.Append(this._kind.ToString());
			stringBuilder.Append('>');
			stringBuilder.Append(' ');
			stringBuilder.Append(this._type.ToDisplayString(SymbolDisplayFormat.TestFormat));
			return stringBuilder.ToString();
		}

		// Token: 0x04001CE8 RID: 7400
		private readonly MethodSymbol _containingMethodOpt;

		// Token: 0x04001CE9 RID: 7401
		private readonly TypeWithAnnotations _type;

		// Token: 0x04001CEA RID: 7402
		private readonly SynthesizedLocalKind _kind;

		// Token: 0x04001CEB RID: 7403
		private readonly SyntaxNode _syntaxOpt;

		// Token: 0x04001CEC RID: 7404
		private readonly bool _isPinned;

		// Token: 0x04001CED RID: 7405
		private readonly RefKind _refKind;
	}
}
