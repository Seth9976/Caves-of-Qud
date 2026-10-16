using System;
using System.Collections.Immutable;
using System.Diagnostics;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000502 RID: 1282
	internal abstract class NonMissingModuleSymbol : ModuleSymbol
	{
		// Token: 0x17001C05 RID: 7173
		// (get) Token: 0x06006C60 RID: 27744 RVA: 0x00190599 File Offset: 0x0018E799
		internal sealed override bool IsMissing
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006C61 RID: 27745 RVA: 0x0019059C File Offset: 0x0018E79C
		internal sealed override ImmutableArray<AssemblyIdentity> GetReferencedAssemblies()
		{
			return this._moduleReferences.Identities;
		}

		// Token: 0x06006C62 RID: 27746 RVA: 0x001905A9 File Offset: 0x0018E7A9
		internal sealed override ImmutableArray<AssemblySymbol> GetReferencedAssemblySymbols()
		{
			return this._moduleReferences.Symbols;
		}

		// Token: 0x06006C63 RID: 27747 RVA: 0x001905B6 File Offset: 0x0018E7B6
		internal ImmutableArray<UnifiedAssembly<AssemblySymbol>> GetUnifiedAssemblies()
		{
			return this._moduleReferences.UnifiedAssemblies;
		}

		// Token: 0x17001C06 RID: 7174
		// (get) Token: 0x06006C64 RID: 27748 RVA: 0x001905C4 File Offset: 0x0018E7C4
		internal override bool HasUnifiedReferences
		{
			get
			{
				return this.GetUnifiedAssemblies().Length > 0;
			}
		}

		// Token: 0x06006C65 RID: 27749 RVA: 0x001905E4 File Offset: 0x0018E7E4
		internal override bool GetUnificationUseSiteDiagnostic(ref DiagnosticInfo result, TypeSymbol dependentType)
		{
			AssemblySymbol containingAssembly = this.ContainingAssembly;
			AssemblySymbol containingAssembly2 = dependentType.ContainingAssembly;
			if (containingAssembly == containingAssembly2)
			{
				return false;
			}
			foreach (UnifiedAssembly<AssemblySymbol> unifiedAssembly in this.GetUnifiedAssemblies())
			{
				if (unifiedAssembly.TargetAssembly == containingAssembly2)
				{
					AssemblyIdentity originalReference = unifiedAssembly.OriginalReference;
					AssemblyIdentity identity = containingAssembly2.Identity;
					ImmutableArray<Symbol> immutableArray = ImmutableArray.Create<Symbol>(containingAssembly, containingAssembly2);
					DiagnosticInfo diagnosticInfo;
					if (identity.Version > originalReference.Version)
					{
						diagnosticInfo = new CSDiagnosticInfo((identity.Version.Major == originalReference.Version.Major && identity.Version.Minor == originalReference.Version.Minor) ? ErrorCode.WRN_UnifyReferenceBldRev : ErrorCode.WRN_UnifyReferenceMajMin, new object[]
						{
							originalReference.GetDisplayName(false),
							containingAssembly.Name,
							identity.GetDisplayName(false),
							containingAssembly2.Name
						}, immutableArray, ImmutableArray<Location>.Empty);
					}
					else
					{
						diagnosticInfo = new CSDiagnosticInfo(ErrorCode.ERR_AssemblyMatchBadVersion, new object[]
						{
							containingAssembly.Name,
							containingAssembly.Identity.GetDisplayName(false),
							originalReference.GetDisplayName(false),
							containingAssembly2.Name,
							identity.GetDisplayName(false)
						}, immutableArray, ImmutableArray<Location>.Empty);
					}
					if (base.MergeUseSiteDiagnostics(ref result, diagnosticInfo))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06006C66 RID: 27750 RVA: 0x0019074B File Offset: 0x0018E94B
		internal override void SetReferences(ModuleReferences<AssemblySymbol> moduleReferences, SourceAssemblySymbol originatingSourceAssemblyDebugOnly = null)
		{
			this._moduleReferences = moduleReferences;
		}

		// Token: 0x06006C67 RID: 27751 RVA: 0x00190754 File Offset: 0x0018E954
		[Conditional("DEBUG")]
		internal void AssertReferencesUninitialized()
		{
		}

		// Token: 0x06006C68 RID: 27752 RVA: 0x00190756 File Offset: 0x0018E956
		[Conditional("DEBUG")]
		internal void AssertReferencesInitialized()
		{
		}

		// Token: 0x06006C69 RID: 27753 RVA: 0x00190758 File Offset: 0x0018E958
		internal sealed override NamedTypeSymbol LookupTopLevelMetadataType(ref MetadataTypeName emittedName)
		{
			NamespaceSymbol namespaceSymbol = this.GlobalNamespace.LookupNestedNamespace(emittedName.NamespaceSegments);
			NamedTypeSymbol namedTypeSymbol;
			if (namespaceSymbol == null)
			{
				namedTypeSymbol = new MissingMetadataTypeSymbol.TopLevel(this, ref emittedName, null);
			}
			else
			{
				namedTypeSymbol = namespaceSymbol.LookupMetadataType(ref emittedName);
			}
			return namedTypeSymbol;
		}

		// Token: 0x04001ACD RID: 6861
		private ModuleReferences<AssemblySymbol> _moduleReferences;
	}
}
