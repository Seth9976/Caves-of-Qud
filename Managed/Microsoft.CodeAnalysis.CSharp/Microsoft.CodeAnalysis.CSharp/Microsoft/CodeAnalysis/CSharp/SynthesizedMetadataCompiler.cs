using System;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200011A RID: 282
	internal sealed class SynthesizedMetadataCompiler : CSharpSymbolVisitor
	{
		// Token: 0x06001761 RID: 5985 RVA: 0x00080C71 File Offset: 0x0007EE71
		private SynthesizedMetadataCompiler(PEModuleBuilder moduleBeingBuilt, CancellationToken cancellationToken)
		{
			this._moduleBeingBuilt = moduleBeingBuilt;
			this._cancellationToken = cancellationToken;
		}

		// Token: 0x06001762 RID: 5986 RVA: 0x00080C87 File Offset: 0x0007EE87
		public static void ProcessSynthesizedMembers(CSharpCompilation compilation, PEModuleBuilder moduleBeingBuilt, CancellationToken cancellationToken)
		{
			new SynthesizedMetadataCompiler(moduleBeingBuilt, cancellationToken).Visit(compilation.SourceModule.GlobalNamespace);
		}

		// Token: 0x06001763 RID: 5987 RVA: 0x00080CA0 File Offset: 0x0007EEA0
		public override void VisitNamespace(NamespaceSymbol symbol)
		{
			this._cancellationToken.ThrowIfCancellationRequested();
			foreach (Symbol symbol2 in symbol.GetMembers())
			{
				symbol2.Accept(this);
			}
		}

		// Token: 0x06001764 RID: 5988 RVA: 0x00080CE4 File Offset: 0x0007EEE4
		public override void VisitNamedType(NamedTypeSymbol symbol)
		{
			this._cancellationToken.ThrowIfCancellationRequested();
			SourceMemberContainerTypeSymbol sourceMemberContainerTypeSymbol = symbol as SourceMemberContainerTypeSymbol;
			if (sourceMemberContainerTypeSymbol != null && this._moduleBeingBuilt != null)
			{
				foreach (SynthesizedExplicitImplementationForwardingMethod synthesizedExplicitImplementationForwardingMethod in sourceMemberContainerTypeSymbol.GetSynthesizedExplicitImplementations(this._cancellationToken))
				{
					this._moduleBeingBuilt.AddSynthesizedDefinition(symbol, synthesizedExplicitImplementationForwardingMethod.GetCciAdapter());
				}
			}
			foreach (Symbol symbol2 in symbol.GetMembers())
			{
				SymbolKind kind = symbol2.Kind;
				if (kind == SymbolKind.NamedType || kind == SymbolKind.Property)
				{
					symbol2.Accept(this);
				}
			}
		}

		// Token: 0x06001765 RID: 5989 RVA: 0x00080D90 File Offset: 0x0007EF90
		public override void VisitProperty(PropertySymbol symbol)
		{
			SourcePropertySymbolBase sourcePropertySymbolBase = symbol as SourcePropertySymbolBase;
			if (sourcePropertySymbolBase != null && sourcePropertySymbolBase.IsSealed)
			{
				SynthesizedSealedPropertyAccessor synthesizedSealedAccessorOpt = sourcePropertySymbolBase.SynthesizedSealedAccessorOpt;
				if (synthesizedSealedAccessorOpt != null)
				{
					this._moduleBeingBuilt.AddSynthesizedDefinition(sourcePropertySymbolBase.ContainingType, synthesizedSealedAccessorOpt.GetCciAdapter());
				}
			}
		}

		// Token: 0x04000679 RID: 1657
		private readonly PEModuleBuilder _moduleBeingBuilt;

		// Token: 0x0400067A RID: 1658
		private readonly CancellationToken _cancellationToken;
	}
}
