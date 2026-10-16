using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols.Metadata.PE
{
	// Token: 0x020005E6 RID: 1510
	internal sealed class PEAssemblySymbol : MetadataOrSourceAssemblySymbol
	{
		// Token: 0x06007DDC RID: 32220 RVA: 0x001C4DC4 File Offset: 0x001C2FC4
		internal PEAssemblySymbol(PEAssembly assembly, DocumentationProvider documentationProvider, bool isLinked, MetadataImportOptions importOptions)
		{
			this._assembly = assembly;
			this._documentationProvider = documentationProvider;
			ModuleSymbol[] array = new ModuleSymbol[assembly.Modules.Length];
			for (int i = 0; i < assembly.Modules.Length; i++)
			{
				array[i] = new PEModuleSymbol(this, assembly.Modules[i], importOptions, i);
			}
			this._modules = array.AsImmutableOrNull<ModuleSymbol>();
			this._isLinked = isLinked;
		}

		// Token: 0x17002520 RID: 9504
		// (get) Token: 0x06007DDD RID: 32221 RVA: 0x001C4E40 File Offset: 0x001C3040
		internal PEAssembly Assembly
		{
			get
			{
				return this._assembly;
			}
		}

		// Token: 0x17002521 RID: 9505
		// (get) Token: 0x06007DDE RID: 32222 RVA: 0x001C4E48 File Offset: 0x001C3048
		public override AssemblyIdentity Identity
		{
			get
			{
				return this._assembly.Identity;
			}
		}

		// Token: 0x17002522 RID: 9506
		// (get) Token: 0x06007DDF RID: 32223 RVA: 0x001C4E55 File Offset: 0x001C3055
		public override Version AssemblyVersionPattern
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17002523 RID: 9507
		// (get) Token: 0x06007DE0 RID: 32224 RVA: 0x001C4E58 File Offset: 0x001C3058
		public override ImmutableArray<ModuleSymbol> Modules
		{
			get
			{
				return this._modules;
			}
		}

		// Token: 0x17002524 RID: 9508
		// (get) Token: 0x06007DE1 RID: 32225 RVA: 0x001C4E60 File Offset: 0x001C3060
		public override ImmutableArray<Location> Locations
		{
			get
			{
				return this.PrimaryModule.MetadataLocation.Cast<MetadataLocation, Location>();
			}
		}

		// Token: 0x06007DE2 RID: 32226 RVA: 0x001C4E74 File Offset: 0x001C3074
		public override ImmutableArray<CSharpAttributeData> GetAttributes()
		{
			if (this._lazyCustomAttributes.IsDefault)
			{
				if (this.MightContainExtensionMethods)
				{
					this.PrimaryModule.LoadCustomAttributesFilterExtensions(this._assembly.Handle, ref this._lazyCustomAttributes);
				}
				else
				{
					this.PrimaryModule.LoadCustomAttributes(this._assembly.Handle, ref this._lazyCustomAttributes);
				}
			}
			return this._lazyCustomAttributes;
		}

		// Token: 0x06007DE3 RID: 32227 RVA: 0x001C4ED6 File Offset: 0x001C30D6
		[return: TupleElementNames(new string[] { "FirstSymbol", "SecondSymbol" })]
		internal ValueTuple<AssemblySymbol, AssemblySymbol> LookupAssembliesForForwardedMetadataType(ref MetadataTypeName emittedName)
		{
			return this.PrimaryModule.GetAssembliesForForwardedType(ref emittedName);
		}

		// Token: 0x06007DE4 RID: 32228 RVA: 0x001C4EE4 File Offset: 0x001C30E4
		internal override IEnumerable<NamedTypeSymbol> GetAllTopLevelForwardedTypes()
		{
			return this.PrimaryModule.GetForwardedTypes();
		}

		// Token: 0x06007DE5 RID: 32229 RVA: 0x001C4EF4 File Offset: 0x001C30F4
		internal override NamedTypeSymbol TryLookupForwardedMetadataTypeWithCycleDetection(ref MetadataTypeName emittedName, ConsList<AssemblySymbol> visitedAssemblies)
		{
			ValueTuple<AssemblySymbol, AssemblySymbol> valueTuple = this.LookupAssembliesForForwardedMetadataType(ref emittedName);
			AssemblySymbol item = valueTuple.Item1;
			AssemblySymbol item2 = valueTuple.Item2;
			if (item == null)
			{
				return null;
			}
			if (item2 != null)
			{
				return base.CreateMultipleForwardingErrorTypeSymbol(ref emittedName, this.PrimaryModule, item, item2);
			}
			if (visitedAssemblies != null && Enumerable.Contains<AssemblySymbol>(visitedAssemblies, item))
			{
				return base.CreateCycleInTypeForwarderErrorTypeSymbol(ref emittedName);
			}
			visitedAssemblies = new ConsList<AssemblySymbol>(this, visitedAssemblies ?? ConsList<AssemblySymbol>.Empty);
			return item.LookupTopLevelMetadataTypeWithCycleDetection(ref emittedName, visitedAssemblies, true);
		}

		// Token: 0x06007DE6 RID: 32230 RVA: 0x001C4F5C File Offset: 0x001C315C
		internal override ImmutableArray<AssemblySymbol> GetNoPiaResolutionAssemblies()
		{
			return this._noPiaResolutionAssemblies;
		}

		// Token: 0x06007DE7 RID: 32231 RVA: 0x001C4F64 File Offset: 0x001C3164
		internal override void SetNoPiaResolutionAssemblies(ImmutableArray<AssemblySymbol> assemblies)
		{
			this._noPiaResolutionAssemblies = assemblies;
		}

		// Token: 0x06007DE8 RID: 32232 RVA: 0x001C4F6D File Offset: 0x001C316D
		internal override void SetLinkedReferencedAssemblies(ImmutableArray<AssemblySymbol> assemblies)
		{
			this._linkedReferencedAssemblies = assemblies;
		}

		// Token: 0x06007DE9 RID: 32233 RVA: 0x001C4F76 File Offset: 0x001C3176
		internal override ImmutableArray<AssemblySymbol> GetLinkedReferencedAssemblies()
		{
			return this._linkedReferencedAssemblies;
		}

		// Token: 0x17002525 RID: 9509
		// (get) Token: 0x06007DEA RID: 32234 RVA: 0x001C4F7E File Offset: 0x001C317E
		internal override ImmutableArray<byte> PublicKey
		{
			get
			{
				return this.Identity.PublicKey;
			}
		}

		// Token: 0x06007DEB RID: 32235 RVA: 0x001C4F8C File Offset: 0x001C318C
		internal override bool GetGuidString(out string guidString)
		{
			return this.Assembly.Modules[0].HasGuidAttribute(this.Assembly.Handle, out guidString);
		}

		// Token: 0x06007DEC RID: 32236 RVA: 0x001C4FC0 File Offset: 0x001C31C0
		internal override bool AreInternalsVisibleToThisAssembly(AssemblySymbol potentialGiverOfAccess)
		{
			IVTConclusion ivtconclusion = base.MakeFinalIVTDetermination(potentialGiverOfAccess);
			return ivtconclusion == IVTConclusion.Match || ivtconclusion == IVTConclusion.OneSignedOneNot;
		}

		// Token: 0x06007DED RID: 32237 RVA: 0x001C4FDE File Offset: 0x001C31DE
		internal override IEnumerable<ImmutableArray<byte>> GetInternalsVisibleToPublicKeys(string simpleName)
		{
			return this.Assembly.GetInternalsVisibleToPublicKeys(simpleName);
		}

		// Token: 0x17002526 RID: 9510
		// (get) Token: 0x06007DEE RID: 32238 RVA: 0x001C4FEC File Offset: 0x001C31EC
		internal DocumentationProvider DocumentationProvider
		{
			get
			{
				return this._documentationProvider;
			}
		}

		// Token: 0x17002527 RID: 9511
		// (get) Token: 0x06007DEF RID: 32239 RVA: 0x001C4FF4 File Offset: 0x001C31F4
		internal override bool IsLinked
		{
			get
			{
				return this._isLinked;
			}
		}

		// Token: 0x17002528 RID: 9512
		// (get) Token: 0x06007DF0 RID: 32240 RVA: 0x001C4FFC File Offset: 0x001C31FC
		public override bool MightContainExtensionMethods
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17002529 RID: 9513
		// (get) Token: 0x06007DF1 RID: 32241 RVA: 0x001C5000 File Offset: 0x001C3200
		internal PEModuleSymbol PrimaryModule
		{
			get
			{
				return (PEModuleSymbol)this._modules[0];
			}
		}

		// Token: 0x1700252A RID: 9514
		// (get) Token: 0x06007DF2 RID: 32242 RVA: 0x001C5021 File Offset: 0x001C3221
		internal sealed override CSharpCompilation DeclaringCompilation
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06007DF3 RID: 32243 RVA: 0x001C5024 File Offset: 0x001C3224
		public override AssemblyMetadata GetMetadata()
		{
			return this._assembly.GetNonDisposableMetadata();
		}

		// Token: 0x04001DA6 RID: 7590
		private readonly PEAssembly _assembly;

		// Token: 0x04001DA7 RID: 7591
		private readonly DocumentationProvider _documentationProvider;

		// Token: 0x04001DA8 RID: 7592
		private readonly ImmutableArray<ModuleSymbol> _modules;

		// Token: 0x04001DA9 RID: 7593
		private ImmutableArray<AssemblySymbol> _noPiaResolutionAssemblies;

		// Token: 0x04001DAA RID: 7594
		private ImmutableArray<AssemblySymbol> _linkedReferencedAssemblies;

		// Token: 0x04001DAB RID: 7595
		private readonly bool _isLinked;

		// Token: 0x04001DAC RID: 7596
		private ImmutableArray<CSharpAttributeData> _lazyCustomAttributes;
	}
}
