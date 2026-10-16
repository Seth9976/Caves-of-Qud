using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004BF RID: 1215
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class AliasSymbolFromSyntax : AliasSymbol
	{
		// Token: 0x060067DD RID: 26589 RVA: 0x00186FE0 File Offset: 0x001851E0
		internal AliasSymbolFromSyntax(SourceNamespaceSymbol containingSymbol, UsingDirectiveSyntax syntax)
			: base(syntax.Alias.Name.Identifier.ValueText, containingSymbol, ImmutableArray.Create<Location>(syntax.Alias.Name.Identifier.GetLocation()), false)
		{
			this._directive = syntax.GetReference();
		}

		// Token: 0x060067DE RID: 26590 RVA: 0x00187038 File Offset: 0x00185238
		internal AliasSymbolFromSyntax(SourceNamespaceSymbol containingSymbol, ExternAliasDirectiveSyntax syntax)
			: base(syntax.Identifier.ValueText, containingSymbol, ImmutableArray.Create<Location>(syntax.Identifier.GetLocation()), true)
		{
			this._directive = syntax.GetReference();
		}

		// Token: 0x17001A15 RID: 6677
		// (get) Token: 0x060067DF RID: 26591 RVA: 0x0018707A File Offset: 0x0018527A
		public override NamespaceOrTypeSymbol Target
		{
			get
			{
				return this.GetAliasTarget(null);
			}
		}

		// Token: 0x060067E0 RID: 26592 RVA: 0x00187084 File Offset: 0x00185284
		internal override NamespaceOrTypeSymbol GetAliasTarget([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ConsList<TypeSymbol> basesBeingResolved)
		{
			if (!this._state.HasComplete(CompletionPart.StartBaseType))
			{
				BindingDiagnosticBag instance = BindingDiagnosticBag.GetInstance();
				NamespaceOrTypeSymbol namespaceOrTypeSymbol = (this.IsExtern ? this.ResolveExternAliasTarget(instance) : this.ResolveAliasTarget(((UsingDirectiveSyntax)this._directive.GetSyntax(default(CancellationToken))).Name, instance, basesBeingResolved));
				if (Interlocked.CompareExchange<NamespaceOrTypeSymbol>(ref this._aliasTarget, namespaceOrTypeSymbol, null) == null)
				{
					Interlocked.Exchange<BindingDiagnosticBag>(ref this._aliasTargetDiagnostics, instance);
					this._state.NotePartComplete(CompletionPart.StartBaseType);
				}
				else
				{
					instance.Free();
					this._state.SpinWaitComplete(CompletionPart.StartBaseType, default(CancellationToken));
				}
			}
			return this._aliasTarget;
		}

		// Token: 0x17001A16 RID: 6678
		// (get) Token: 0x060067E1 RID: 26593 RVA: 0x0018712F File Offset: 0x0018532F
		internal BindingDiagnosticBag AliasTargetDiagnostics
		{
			get
			{
				this.GetAliasTarget(null);
				return this._aliasTargetDiagnostics;
			}
		}

		// Token: 0x060067E2 RID: 26594 RVA: 0x00187140 File Offset: 0x00185340
		private NamespaceSymbol ResolveExternAliasTarget(BindingDiagnosticBag diagnostics)
		{
			NamespaceSymbol namespaceSymbol;
			if (!this.ContainingSymbol.DeclaringCompilation.GetExternAliasTarget(this.Name, out namespaceSymbol))
			{
				diagnostics.Add(ErrorCode.ERR_BadExternAlias, this.Locations[0], new object[] { this.Name });
			}
			return namespaceSymbol;
		}

		// Token: 0x060067E3 RID: 26595 RVA: 0x00187194 File Offset: 0x00185394
		private NamespaceOrTypeSymbol ResolveAliasTarget(NameSyntax syntax, BindingDiagnosticBag diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ConsList<TypeSymbol> basesBeingResolved)
		{
			return this.ContainingSymbol.DeclaringCompilation.GetBinderFactory(syntax.SyntaxTree, false).GetBinder(syntax, null, null).WithAdditionalFlags(BinderFlags.SuppressConstraintChecks | BinderFlags.SuppressObsoleteChecks)
				.BindNamespaceOrTypeSymbol(syntax, diagnostics, basesBeingResolved)
				.NamespaceOrTypeSymbol;
		}

		// Token: 0x17001A17 RID: 6679
		// (get) Token: 0x060067E4 RID: 26596 RVA: 0x001871D6 File Offset: 0x001853D6
		internal override bool RequiresCompletion
		{
			get
			{
				return true;
			}
		}

		// Token: 0x040019C2 RID: 6594
		private readonly SyntaxReference _directive;

		// Token: 0x040019C3 RID: 6595
		private SymbolCompletionState _state;

		// Token: 0x040019C4 RID: 6596
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private NamespaceOrTypeSymbol _aliasTarget;

		// Token: 0x040019C5 RID: 6597
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private BindingDiagnosticBag _aliasTargetDiagnostics;
	}
}
