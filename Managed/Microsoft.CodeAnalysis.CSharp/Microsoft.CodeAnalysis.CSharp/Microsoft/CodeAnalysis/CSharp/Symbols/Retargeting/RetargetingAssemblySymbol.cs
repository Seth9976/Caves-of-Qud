using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Symbols.Metadata.PE;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols.Retargeting
{
	// Token: 0x020005D3 RID: 1491
	internal sealed class RetargetingAssemblySymbol : NonMissingAssemblySymbol
	{
		// Token: 0x17002494 RID: 9364
		// (get) Token: 0x06007CA7 RID: 31911 RVA: 0x001C19C6 File Offset: 0x001BFBC6
		internal ConcurrentDictionary<NamedTypeSymbol, NamedTypeSymbol> NoPiaUnificationMap
		{
			get
			{
				return LazyInitializer.EnsureInitialized<ConcurrentDictionary<NamedTypeSymbol, NamedTypeSymbol>>(ref this._noPiaUnificationMap, () => new ConcurrentDictionary<NamedTypeSymbol, NamedTypeSymbol>(2, 0));
			}
		}

		// Token: 0x06007CA8 RID: 31912 RVA: 0x001C19F4 File Offset: 0x001BFBF4
		public RetargetingAssemblySymbol(SourceAssemblySymbol underlyingAssembly, bool isLinked)
		{
			this._underlyingAssembly = underlyingAssembly;
			ModuleSymbol[] array = new ModuleSymbol[underlyingAssembly.Modules.Length];
			array[0] = new RetargetingModuleSymbol(this, (SourceModuleSymbol)underlyingAssembly.Modules[0]);
			for (int i = 1; i < underlyingAssembly.Modules.Length; i++)
			{
				PEModuleSymbol pemoduleSymbol = (PEModuleSymbol)underlyingAssembly.Modules[i];
				array[i] = new PEModuleSymbol(this, pemoduleSymbol.Module, pemoduleSymbol.ImportOptions, i);
			}
			this._modules = array.AsImmutableOrNull<ModuleSymbol>();
			this._isLinked = isLinked;
		}

		// Token: 0x17002495 RID: 9365
		// (get) Token: 0x06007CA9 RID: 31913 RVA: 0x001C1A98 File Offset: 0x001BFC98
		private RetargetingModuleSymbol.RetargetingSymbolTranslator RetargetingTranslator
		{
			get
			{
				return ((RetargetingModuleSymbol)this._modules[0]).RetargetingTranslator;
			}
		}

		// Token: 0x17002496 RID: 9366
		// (get) Token: 0x06007CAA RID: 31914 RVA: 0x001C1ABE File Offset: 0x001BFCBE
		public SourceAssemblySymbol UnderlyingAssembly
		{
			get
			{
				return this._underlyingAssembly;
			}
		}

		// Token: 0x17002497 RID: 9367
		// (get) Token: 0x06007CAB RID: 31915 RVA: 0x001C1AC6 File Offset: 0x001BFCC6
		public override bool IsImplicitlyDeclared
		{
			get
			{
				return this._underlyingAssembly.IsImplicitlyDeclared;
			}
		}

		// Token: 0x17002498 RID: 9368
		// (get) Token: 0x06007CAC RID: 31916 RVA: 0x001C1AD3 File Offset: 0x001BFCD3
		public override AssemblyIdentity Identity
		{
			get
			{
				return this._underlyingAssembly.Identity;
			}
		}

		// Token: 0x17002499 RID: 9369
		// (get) Token: 0x06007CAD RID: 31917 RVA: 0x001C1AE0 File Offset: 0x001BFCE0
		public override Version AssemblyVersionPattern
		{
			get
			{
				return this._underlyingAssembly.AssemblyVersionPattern;
			}
		}

		// Token: 0x1700249A RID: 9370
		// (get) Token: 0x06007CAE RID: 31918 RVA: 0x001C1AED File Offset: 0x001BFCED
		internal override ImmutableArray<byte> PublicKey
		{
			get
			{
				return this._underlyingAssembly.PublicKey;
			}
		}

		// Token: 0x06007CAF RID: 31919 RVA: 0x001C1AFA File Offset: 0x001BFCFA
		public override string GetDocumentationCommentXml(CultureInfo preferredCulture = null, bool expandIncludes = false, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this._underlyingAssembly.GetDocumentationCommentXml(preferredCulture, expandIncludes, cancellationToken);
		}

		// Token: 0x1700249B RID: 9371
		// (get) Token: 0x06007CB0 RID: 31920 RVA: 0x001C1B0A File Offset: 0x001BFD0A
		public override ImmutableArray<ModuleSymbol> Modules
		{
			get
			{
				return this._modules;
			}
		}

		// Token: 0x1700249C RID: 9372
		// (get) Token: 0x06007CB1 RID: 31921 RVA: 0x001C1B12 File Offset: 0x001BFD12
		internal override bool KeepLookingForDeclaredSpecialTypes
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700249D RID: 9373
		// (get) Token: 0x06007CB2 RID: 31922 RVA: 0x001C1B15 File Offset: 0x001BFD15
		public override ImmutableArray<Location> Locations
		{
			get
			{
				return this._underlyingAssembly.Locations;
			}
		}

		// Token: 0x06007CB3 RID: 31923 RVA: 0x001C1B22 File Offset: 0x001BFD22
		internal override IEnumerable<ImmutableArray<byte>> GetInternalsVisibleToPublicKeys(string simpleName)
		{
			return this._underlyingAssembly.GetInternalsVisibleToPublicKeys(simpleName);
		}

		// Token: 0x06007CB4 RID: 31924 RVA: 0x001C1B30 File Offset: 0x001BFD30
		internal override bool AreInternalsVisibleToThisAssembly(AssemblySymbol other)
		{
			return this._underlyingAssembly.AreInternalsVisibleToThisAssembly(other);
		}

		// Token: 0x06007CB5 RID: 31925 RVA: 0x001C1B3E File Offset: 0x001BFD3E
		public override ImmutableArray<CSharpAttributeData> GetAttributes()
		{
			return this.RetargetingTranslator.GetRetargetedAttributes(this._underlyingAssembly.GetAttributes(), ref this._lazyCustomAttributes);
		}

		// Token: 0x06007CB6 RID: 31926 RVA: 0x001C1B5C File Offset: 0x001BFD5C
		internal override NamedTypeSymbol GetDeclaredSpecialType(SpecialType type)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06007CB7 RID: 31927 RVA: 0x001C1B63 File Offset: 0x001BFD63
		internal override ImmutableArray<AssemblySymbol> GetNoPiaResolutionAssemblies()
		{
			return this._noPiaResolutionAssemblies;
		}

		// Token: 0x06007CB8 RID: 31928 RVA: 0x001C1B6B File Offset: 0x001BFD6B
		internal override void SetNoPiaResolutionAssemblies(ImmutableArray<AssemblySymbol> assemblies)
		{
			this._noPiaResolutionAssemblies = assemblies;
		}

		// Token: 0x06007CB9 RID: 31929 RVA: 0x001C1B74 File Offset: 0x001BFD74
		internal override void SetLinkedReferencedAssemblies(ImmutableArray<AssemblySymbol> assemblies)
		{
			this._linkedReferencedAssemblies = assemblies;
		}

		// Token: 0x06007CBA RID: 31930 RVA: 0x001C1B7D File Offset: 0x001BFD7D
		internal override ImmutableArray<AssemblySymbol> GetLinkedReferencedAssemblies()
		{
			return this._linkedReferencedAssemblies;
		}

		// Token: 0x1700249E RID: 9374
		// (get) Token: 0x06007CBB RID: 31931 RVA: 0x001C1B85 File Offset: 0x001BFD85
		internal override bool IsLinked
		{
			get
			{
				return this._isLinked;
			}
		}

		// Token: 0x1700249F RID: 9375
		// (get) Token: 0x06007CBC RID: 31932 RVA: 0x001C1B8D File Offset: 0x001BFD8D
		public override ICollection<string> TypeNames
		{
			get
			{
				return this._underlyingAssembly.TypeNames;
			}
		}

		// Token: 0x170024A0 RID: 9376
		// (get) Token: 0x06007CBD RID: 31933 RVA: 0x001C1B9A File Offset: 0x001BFD9A
		public override ICollection<string> NamespaceNames
		{
			get
			{
				return this._underlyingAssembly.NamespaceNames;
			}
		}

		// Token: 0x170024A1 RID: 9377
		// (get) Token: 0x06007CBE RID: 31934 RVA: 0x001C1BA7 File Offset: 0x001BFDA7
		public override bool MightContainExtensionMethods
		{
			get
			{
				return this._underlyingAssembly.MightContainExtensionMethods;
			}
		}

		// Token: 0x170024A2 RID: 9378
		// (get) Token: 0x06007CBF RID: 31935 RVA: 0x001C1BB4 File Offset: 0x001BFDB4
		internal sealed override CSharpCompilation DeclaringCompilation
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06007CC0 RID: 31936 RVA: 0x001C1BB7 File Offset: 0x001BFDB7
		internal override bool GetGuidString(out string guidString)
		{
			return this._underlyingAssembly.GetGuidString(out guidString);
		}

		// Token: 0x06007CC1 RID: 31937 RVA: 0x001C1BC8 File Offset: 0x001BFDC8
		internal override NamedTypeSymbol TryLookupForwardedMetadataTypeWithCycleDetection(ref MetadataTypeName emittedName, ConsList<AssemblySymbol> visitedAssemblies)
		{
			NamedTypeSymbol namedTypeSymbol = this._underlyingAssembly.TryLookupForwardedMetadataType(ref emittedName);
			if (namedTypeSymbol == null)
			{
				return null;
			}
			return this.RetargetingTranslator.Retarget(namedTypeSymbol, RetargetOptions.RetargetPrimitiveTypesByName);
		}

		// Token: 0x06007CC2 RID: 31938 RVA: 0x001C1BF4 File Offset: 0x001BFDF4
		internal override IEnumerable<NamedTypeSymbol> GetAllTopLevelForwardedTypes()
		{
			foreach (NamedTypeSymbol namedTypeSymbol in this._underlyingAssembly.GetAllTopLevelForwardedTypes())
			{
				yield return this.RetargetingTranslator.Retarget(namedTypeSymbol, RetargetOptions.RetargetPrimitiveTypesByName);
			}
			IEnumerator<NamedTypeSymbol> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06007CC3 RID: 31939 RVA: 0x001C1C04 File Offset: 0x001BFE04
		public override AssemblyMetadata GetMetadata()
		{
			return this._underlyingAssembly.GetMetadata();
		}

		// Token: 0x04001D56 RID: 7510
		private readonly SourceAssemblySymbol _underlyingAssembly;

		// Token: 0x04001D57 RID: 7511
		private readonly ImmutableArray<ModuleSymbol> _modules;

		// Token: 0x04001D58 RID: 7512
		private ImmutableArray<AssemblySymbol> _noPiaResolutionAssemblies;

		// Token: 0x04001D59 RID: 7513
		private ImmutableArray<AssemblySymbol> _linkedReferencedAssemblies;

		// Token: 0x04001D5A RID: 7514
		private ConcurrentDictionary<NamedTypeSymbol, NamedTypeSymbol> _noPiaUnificationMap;

		// Token: 0x04001D5B RID: 7515
		private readonly bool _isLinked;

		// Token: 0x04001D5C RID: 7516
		private ImmutableArray<CSharpAttributeData> _lazyCustomAttributes;
	}
}
