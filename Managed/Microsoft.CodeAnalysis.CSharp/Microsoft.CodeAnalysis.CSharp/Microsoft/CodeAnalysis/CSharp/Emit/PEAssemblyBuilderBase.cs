using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Symbols.Metadata.PE;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Emit
{
	// Token: 0x02000491 RID: 1169
	internal abstract class PEAssemblyBuilderBase : PEModuleBuilder, IAssemblyReference, IModuleReference, IUnitReference, IReference, INamedEntity
	{
		// Token: 0x0600610C RID: 24844 RVA: 0x00175C74 File Offset: 0x00173E74
		public PEAssemblyBuilderBase(SourceAssemblySymbol sourceAssembly, EmitOptions emitOptions, OutputKind outputKind, ModulePropertiesForSerialization serializationProperties, IEnumerable<ResourceDescription> manifestResources, ImmutableArray<NamedTypeSymbol> additionalTypes)
			: base((SourceModuleSymbol)sourceAssembly.Modules[0], emitOptions, outputKind, serializationProperties, manifestResources)
		{
			this._sourceAssembly = sourceAssembly;
			this._additionalTypes = additionalTypes.NullToEmpty<NamedTypeSymbol>();
			this._metadataName = ((emitOptions.OutputNameOverride == null) ? sourceAssembly.MetadataName : FileNameUtilities.ChangeExtension(emitOptions.OutputNameOverride, null));
			this.AssemblyOrModuleSymbolToModuleRefMap.Add(sourceAssembly, this);
		}

		// Token: 0x170016AB RID: 5803
		// (get) Token: 0x0600610D RID: 24845 RVA: 0x00175CE4 File Offset: 0x00173EE4
		public sealed override ISourceAssemblySymbolInternal SourceAssemblyOpt
		{
			get
			{
				return this._sourceAssembly;
			}
		}

		// Token: 0x0600610E RID: 24846 RVA: 0x00175CEC File Offset: 0x00173EEC
		public sealed override ImmutableArray<NamedTypeSymbol> GetAdditionalTopLevelTypes()
		{
			return this._additionalTypes;
		}

		// Token: 0x0600610F RID: 24847 RVA: 0x00175CF4 File Offset: 0x00173EF4
		internal sealed override ImmutableArray<NamedTypeSymbol> GetEmbeddedTypes(BindingDiagnosticBag diagnostics)
		{
			ArrayBuilder<NamedTypeSymbol> instance = ArrayBuilder<NamedTypeSymbol>.GetInstance();
			this.CreateEmbeddedAttributesIfNeeded(diagnostics);
			instance.AddIfNotNull(this._lazyEmbeddedAttribute);
			instance.AddIfNotNull(this._lazyIsReadOnlyAttribute);
			instance.AddIfNotNull(this._lazyIsUnmanagedAttribute);
			instance.AddIfNotNull(this._lazyIsByRefLikeAttribute);
			instance.AddIfNotNull(this._lazyNullableAttribute);
			instance.AddIfNotNull(this._lazyNullableContextAttribute);
			instance.AddIfNotNull(this._lazyNullablePublicOnlyAttribute);
			instance.AddIfNotNull(this._lazyNativeIntegerAttribute);
			return instance.ToImmutableAndFree();
		}

		// Token: 0x06006110 RID: 24848 RVA: 0x00175D74 File Offset: 0x00173F74
		public sealed override IEnumerable<IFileReference> GetFiles(EmitContext context)
		{
			PEAssemblyBuilderBase.<>c__DisplayClass18_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.context = context;
			if (!CS$<>8__locals1.context.IsRefAssembly)
			{
				return this.<GetFiles>g__getFiles|18_0(ref this._lazyFiles, ref CS$<>8__locals1);
			}
			return this.<GetFiles>g__getFiles|18_0(ref this._lazyFilesWithoutManifestResources, ref CS$<>8__locals1);
		}

		// Token: 0x06006111 RID: 24849 RVA: 0x00175DC8 File Offset: 0x00173FC8
		protected override void AddEmbeddedResourcesFromAddedModules(ArrayBuilder<ManagedResource> builder, DiagnosticBag diagnostics)
		{
			ImmutableArray<ModuleSymbol> modules = this._sourceAssembly.Modules;
			int length = modules.Length;
			for (int i = 1; i < length; i++)
			{
				IFileReference fileReference = (IFileReference)base.Translate(modules[i], diagnostics);
				try
				{
					foreach (EmbeddedResource embeddedResource in ((PEModuleSymbol)modules[i]).Module.GetEmbeddedResourcesOrThrow())
					{
						builder.Add(new ManagedResource(embeddedResource.Name, (embeddedResource.Attributes & ManifestResourceAttributes.Public) > (ManifestResourceAttributes)0, null, fileReference, embeddedResource.Offset));
					}
				}
				catch (BadImageFormatException)
				{
					diagnostics.Add(new CSDiagnosticInfo(ErrorCode.ERR_BindToBogus, new object[] { modules[i] }), NoLocation.Singleton);
				}
			}
		}

		// Token: 0x170016AC RID: 5804
		// (get) Token: 0x06006112 RID: 24850 RVA: 0x00175EA8 File Offset: 0x001740A8
		public override string Name
		{
			get
			{
				return this._metadataName;
			}
		}

		// Token: 0x170016AD RID: 5805
		// (get) Token: 0x06006113 RID: 24851 RVA: 0x00175EB0 File Offset: 0x001740B0
		public AssemblyIdentity Identity
		{
			get
			{
				return this._sourceAssembly.Identity;
			}
		}

		// Token: 0x170016AE RID: 5806
		// (get) Token: 0x06006114 RID: 24852 RVA: 0x00175EBD File Offset: 0x001740BD
		public Version AssemblyVersionPattern
		{
			get
			{
				return this._sourceAssembly.AssemblyVersionPattern;
			}
		}

		// Token: 0x06006115 RID: 24853 RVA: 0x00175ECC File Offset: 0x001740CC
		internal override SynthesizedAttributeData SynthesizeEmbeddedAttribute()
		{
			return new SynthesizedAttributeData(this._lazyEmbeddedAttribute.Constructors[0], ImmutableArray<TypedConstant>.Empty, ImmutableArray<KeyValuePair<string, TypedConstant>>.Empty);
		}

		// Token: 0x06006116 RID: 24854 RVA: 0x00175EFC File Offset: 0x001740FC
		internal override SynthesizedAttributeData SynthesizeNullableAttribute(WellKnownMember member, ImmutableArray<TypedConstant> arguments)
		{
			if (this._lazyNullableAttribute != null)
			{
				int num = ((member == WellKnownMember.System_Runtime_CompilerServices_NullableAttribute__ctorTransformFlags) ? 1 : 0);
				return new SynthesizedAttributeData(this._lazyNullableAttribute.Constructors[num], arguments, ImmutableArray<KeyValuePair<string, TypedConstant>>.Empty);
			}
			return base.SynthesizeNullableAttribute(member, arguments);
		}

		// Token: 0x06006117 RID: 24855 RVA: 0x00175F48 File Offset: 0x00174148
		internal override SynthesizedAttributeData SynthesizeNullableContextAttribute(ImmutableArray<TypedConstant> arguments)
		{
			if (this._lazyNullableContextAttribute != null)
			{
				return new SynthesizedAttributeData(this._lazyNullableContextAttribute.Constructors[0], arguments, ImmutableArray<KeyValuePair<string, TypedConstant>>.Empty);
			}
			return base.SynthesizeNullableContextAttribute(arguments);
		}

		// Token: 0x06006118 RID: 24856 RVA: 0x00175F84 File Offset: 0x00174184
		internal override SynthesizedAttributeData SynthesizeNullablePublicOnlyAttribute(ImmutableArray<TypedConstant> arguments)
		{
			if (this._lazyNullablePublicOnlyAttribute != null)
			{
				return new SynthesizedAttributeData(this._lazyNullablePublicOnlyAttribute.Constructors[0], arguments, ImmutableArray<KeyValuePair<string, TypedConstant>>.Empty);
			}
			return base.SynthesizeNullablePublicOnlyAttribute(arguments);
		}

		// Token: 0x06006119 RID: 24857 RVA: 0x00175FC0 File Offset: 0x001741C0
		internal override SynthesizedAttributeData SynthesizeNativeIntegerAttribute(WellKnownMember member, ImmutableArray<TypedConstant> arguments)
		{
			if (this._lazyNativeIntegerAttribute != null)
			{
				int num = ((member == WellKnownMember.System_Runtime_CompilerServices_NativeIntegerAttribute__ctorTransformFlags) ? 1 : 0);
				return new SynthesizedAttributeData(this._lazyNativeIntegerAttribute.Constructors[num], arguments, ImmutableArray<KeyValuePair<string, TypedConstant>>.Empty);
			}
			return base.SynthesizeNativeIntegerAttribute(member, arguments);
		}

		// Token: 0x0600611A RID: 24858 RVA: 0x0017600C File Offset: 0x0017420C
		protected override SynthesizedAttributeData TrySynthesizeIsReadOnlyAttribute()
		{
			if (this._lazyIsReadOnlyAttribute != null)
			{
				return new SynthesizedAttributeData(this._lazyIsReadOnlyAttribute.Constructors[0], ImmutableArray<TypedConstant>.Empty, ImmutableArray<KeyValuePair<string, TypedConstant>>.Empty);
			}
			return base.TrySynthesizeIsReadOnlyAttribute();
		}

		// Token: 0x0600611B RID: 24859 RVA: 0x0017604C File Offset: 0x0017424C
		protected override SynthesizedAttributeData TrySynthesizeIsUnmanagedAttribute()
		{
			if (this._lazyIsUnmanagedAttribute != null)
			{
				return new SynthesizedAttributeData(this._lazyIsUnmanagedAttribute.Constructors[0], ImmutableArray<TypedConstant>.Empty, ImmutableArray<KeyValuePair<string, TypedConstant>>.Empty);
			}
			return base.TrySynthesizeIsUnmanagedAttribute();
		}

		// Token: 0x0600611C RID: 24860 RVA: 0x0017608C File Offset: 0x0017428C
		protected override SynthesizedAttributeData TrySynthesizeIsByRefLikeAttribute()
		{
			if (this._lazyIsByRefLikeAttribute != null)
			{
				return new SynthesizedAttributeData(this._lazyIsByRefLikeAttribute.Constructors[0], ImmutableArray<TypedConstant>.Empty, ImmutableArray<KeyValuePair<string, TypedConstant>>.Empty);
			}
			return base.TrySynthesizeIsByRefLikeAttribute();
		}

		// Token: 0x0600611D RID: 24861 RVA: 0x001760CC File Offset: 0x001742CC
		private void CreateEmbeddedAttributesIfNeeded(BindingDiagnosticBag diagnostics)
		{
			EmbeddableAttributes embeddableAttributes = base.GetNeedsGeneratedAttributes();
			if (base.ShouldEmitNullablePublicOnlyAttribute() && this.Compilation.CheckIfAttributeShouldBeEmbedded(EmbeddableAttributes.NullablePublicOnlyAttribute, diagnostics, Location.None))
			{
				embeddableAttributes |= EmbeddableAttributes.NullablePublicOnlyAttribute;
			}
			else if (embeddableAttributes == (EmbeddableAttributes)0)
			{
				return;
			}
			Func<string, NamespaceSymbol, BindingDiagnosticBag, SynthesizedEmbeddedAttributeSymbol> func = new Func<string, NamespaceSymbol, BindingDiagnosticBag, SynthesizedEmbeddedAttributeSymbol>(this.CreateParameterlessEmbeddedAttributeSymbol);
			this.CreateAttributeIfNeeded<SynthesizedEmbeddedAttributeSymbol>(ref this._lazyEmbeddedAttribute, diagnostics, AttributeDescription.CodeAnalysisEmbeddedAttribute, func);
			if ((embeddableAttributes & EmbeddableAttributes.IsReadOnlyAttribute) != (EmbeddableAttributes)0)
			{
				this.CreateAttributeIfNeeded<SynthesizedEmbeddedAttributeSymbol>(ref this._lazyIsReadOnlyAttribute, diagnostics, AttributeDescription.IsReadOnlyAttribute, func);
			}
			if ((embeddableAttributes & EmbeddableAttributes.IsByRefLikeAttribute) != (EmbeddableAttributes)0)
			{
				this.CreateAttributeIfNeeded<SynthesizedEmbeddedAttributeSymbol>(ref this._lazyIsByRefLikeAttribute, diagnostics, AttributeDescription.IsByRefLikeAttribute, func);
			}
			if ((embeddableAttributes & EmbeddableAttributes.IsUnmanagedAttribute) != (EmbeddableAttributes)0)
			{
				this.CreateAttributeIfNeeded<SynthesizedEmbeddedAttributeSymbol>(ref this._lazyIsUnmanagedAttribute, diagnostics, AttributeDescription.IsUnmanagedAttribute, func);
			}
			if ((embeddableAttributes & EmbeddableAttributes.NullableAttribute) != (EmbeddableAttributes)0)
			{
				this.CreateAttributeIfNeeded<SynthesizedEmbeddedNullableAttributeSymbol>(ref this._lazyNullableAttribute, diagnostics, AttributeDescription.NullableAttribute, new Func<string, NamespaceSymbol, BindingDiagnosticBag, SynthesizedEmbeddedNullableAttributeSymbol>(this.CreateNullableAttributeSymbol));
			}
			if ((embeddableAttributes & EmbeddableAttributes.NullableContextAttribute) != (EmbeddableAttributes)0)
			{
				this.CreateAttributeIfNeeded<SynthesizedEmbeddedNullableContextAttributeSymbol>(ref this._lazyNullableContextAttribute, diagnostics, AttributeDescription.NullableContextAttribute, new Func<string, NamespaceSymbol, BindingDiagnosticBag, SynthesizedEmbeddedNullableContextAttributeSymbol>(this.CreateNullableContextAttributeSymbol));
			}
			if ((embeddableAttributes & EmbeddableAttributes.NullablePublicOnlyAttribute) != (EmbeddableAttributes)0)
			{
				this.CreateAttributeIfNeeded<SynthesizedEmbeddedNullablePublicOnlyAttributeSymbol>(ref this._lazyNullablePublicOnlyAttribute, diagnostics, AttributeDescription.NullablePublicOnlyAttribute, new Func<string, NamespaceSymbol, BindingDiagnosticBag, SynthesizedEmbeddedNullablePublicOnlyAttributeSymbol>(this.CreateNullablePublicOnlyAttributeSymbol));
			}
			if ((embeddableAttributes & EmbeddableAttributes.NativeIntegerAttribute) != (EmbeddableAttributes)0)
			{
				this.CreateAttributeIfNeeded<SynthesizedEmbeddedNativeIntegerAttributeSymbol>(ref this._lazyNativeIntegerAttribute, diagnostics, AttributeDescription.NativeIntegerAttribute, new Func<string, NamespaceSymbol, BindingDiagnosticBag, SynthesizedEmbeddedNativeIntegerAttributeSymbol>(this.CreateNativeIntegerAttributeSymbol));
			}
		}

		// Token: 0x0600611E RID: 24862 RVA: 0x001761FF File Offset: 0x001743FF
		private SynthesizedEmbeddedAttributeSymbol CreateParameterlessEmbeddedAttributeSymbol(string name, NamespaceSymbol containingNamespace, BindingDiagnosticBag diagnostics)
		{
			return new SynthesizedEmbeddedAttributeSymbol(name, containingNamespace, this.SourceModule, this.GetWellKnownType(WellKnownType.System_Attribute, diagnostics));
		}

		// Token: 0x0600611F RID: 24863 RVA: 0x00176217 File Offset: 0x00174417
		private SynthesizedEmbeddedNullableAttributeSymbol CreateNullableAttributeSymbol(string name, NamespaceSymbol containingNamespace, BindingDiagnosticBag diagnostics)
		{
			return new SynthesizedEmbeddedNullableAttributeSymbol(name, containingNamespace, this.SourceModule, this.GetWellKnownType(WellKnownType.System_Attribute, diagnostics), this.GetSpecialType(SpecialType.System_Byte, diagnostics));
		}

		// Token: 0x06006120 RID: 24864 RVA: 0x00176238 File Offset: 0x00174438
		private SynthesizedEmbeddedNullableContextAttributeSymbol CreateNullableContextAttributeSymbol(string name, NamespaceSymbol containingNamespace, BindingDiagnosticBag diagnostics)
		{
			return new SynthesizedEmbeddedNullableContextAttributeSymbol(name, containingNamespace, this.SourceModule, this.GetWellKnownType(WellKnownType.System_Attribute, diagnostics), this.GetSpecialType(SpecialType.System_Byte, diagnostics));
		}

		// Token: 0x06006121 RID: 24865 RVA: 0x00176259 File Offset: 0x00174459
		private SynthesizedEmbeddedNullablePublicOnlyAttributeSymbol CreateNullablePublicOnlyAttributeSymbol(string name, NamespaceSymbol containingNamespace, BindingDiagnosticBag diagnostics)
		{
			return new SynthesizedEmbeddedNullablePublicOnlyAttributeSymbol(name, containingNamespace, this.SourceModule, this.GetWellKnownType(WellKnownType.System_Attribute, diagnostics), this.GetSpecialType(SpecialType.System_Boolean, diagnostics));
		}

		// Token: 0x06006122 RID: 24866 RVA: 0x00176279 File Offset: 0x00174479
		private SynthesizedEmbeddedNativeIntegerAttributeSymbol CreateNativeIntegerAttributeSymbol(string name, NamespaceSymbol containingNamespace, BindingDiagnosticBag diagnostics)
		{
			return new SynthesizedEmbeddedNativeIntegerAttributeSymbol(name, containingNamespace, this.SourceModule, this.GetWellKnownType(WellKnownType.System_Attribute, diagnostics), this.GetSpecialType(SpecialType.System_Boolean, diagnostics));
		}

		// Token: 0x06006123 RID: 24867 RVA: 0x0017629C File Offset: 0x0017449C
		private void CreateAttributeIfNeeded<T>(ref T symbol, BindingDiagnosticBag diagnostics, AttributeDescription description, Func<string, NamespaceSymbol, BindingDiagnosticBag, T> factory) where T : SynthesizedEmbeddedAttributeSymbolBase
		{
			if (symbol == null)
			{
				this.AddDiagnosticsForExistingAttribute(description, diagnostics);
				NamespaceSymbol orSynthesizeNamespace = this.GetOrSynthesizeNamespace(description.Namespace);
				symbol = factory.Invoke(description.Name, orSynthesizeNamespace, diagnostics);
				if (symbol.GetAttributeUsageInfo() != AttributeUsageInfo.Default)
				{
					this.EnsureAttributeUsageAttributeMembersAvailable(diagnostics);
				}
				base.AddSynthesizedDefinition(orSynthesizeNamespace, symbol);
			}
		}

		// Token: 0x06006124 RID: 24868 RVA: 0x00176314 File Offset: 0x00174514
		private void AddDiagnosticsForExistingAttribute(AttributeDescription description, BindingDiagnosticBag diagnostics)
		{
			MetadataTypeName metadataTypeName = MetadataTypeName.FromFullName(description.FullName, false, -1);
			NamedTypeSymbol namedTypeSymbol = this._sourceAssembly.SourceModule.LookupTopLevelMetadataType(ref metadataTypeName);
			if (!(namedTypeSymbol is MissingMetadataTypeSymbol))
			{
				diagnostics.Add(ErrorCode.ERR_TypeReserved, namedTypeSymbol.Locations[0], new object[] { description.FullName });
			}
		}

		// Token: 0x06006125 RID: 24869 RVA: 0x00176378 File Offset: 0x00174578
		private NamespaceSymbol GetOrSynthesizeNamespace(string namespaceFullName)
		{
			NamespaceSymbol namespaceSymbol = this.SourceModule.GlobalNamespace;
			foreach (string text in namespaceFullName.Split(new char[] { '.' }))
			{
				NamespaceSymbol namespaceSymbol2 = (NamespaceSymbol)namespaceSymbol.GetMembers(text).FirstOrDefault<Symbol>((Symbol m) => m.Kind == SymbolKind.Namespace);
				if (namespaceSymbol2 == null)
				{
					namespaceSymbol2 = new SynthesizedNamespaceSymbol(namespaceSymbol, text);
					base.AddSynthesizedDefinition(namespaceSymbol, namespaceSymbol2);
				}
				namespaceSymbol = namespaceSymbol2;
			}
			return namespaceSymbol;
		}

		// Token: 0x06006126 RID: 24870 RVA: 0x00176407 File Offset: 0x00174607
		private NamedTypeSymbol GetWellKnownType(WellKnownType type, BindingDiagnosticBag diagnostics)
		{
			NamedTypeSymbol wellKnownType = this._sourceAssembly.DeclaringCompilation.GetWellKnownType(type);
			Binder.ReportUseSite(wellKnownType, diagnostics, Location.None);
			return wellKnownType;
		}

		// Token: 0x06006127 RID: 24871 RVA: 0x00176427 File Offset: 0x00174627
		private NamedTypeSymbol GetSpecialType(SpecialType type, BindingDiagnosticBag diagnostics)
		{
			NamedTypeSymbol specialType = this._sourceAssembly.DeclaringCompilation.GetSpecialType(type);
			Binder.ReportUseSite(specialType, diagnostics, Location.None);
			return specialType;
		}

		// Token: 0x06006128 RID: 24872 RVA: 0x00176447 File Offset: 0x00174647
		private void EnsureAttributeUsageAttributeMembersAvailable(BindingDiagnosticBag diagnostics)
		{
			CSharpCompilation declaringCompilation = this._sourceAssembly.DeclaringCompilation;
			Binder.GetWellKnownTypeMember(declaringCompilation, WellKnownMember.System_AttributeUsageAttribute__ctor, diagnostics, Location.None, null, false);
			Binder.GetWellKnownTypeMember(declaringCompilation, WellKnownMember.System_AttributeUsageAttribute__AllowMultiple, diagnostics, Location.None, null, false);
			Binder.GetWellKnownTypeMember(declaringCompilation, WellKnownMember.System_AttributeUsageAttribute__Inherited, diagnostics, Location.None, null, false);
		}

		// Token: 0x06006129 RID: 24873 RVA: 0x00176488 File Offset: 0x00174688
		[CompilerGenerated]
		private ImmutableArray<IFileReference> <GetFiles>g__getFiles|18_0(ref ImmutableArray<IFileReference> lazyFiles, ref PEAssemblyBuilderBase.<>c__DisplayClass18_0 A_2)
		{
			if (lazyFiles.IsDefault)
			{
				ArrayBuilder<IFileReference> instance = ArrayBuilder<IFileReference>.GetInstance();
				try
				{
					ImmutableArray<ModuleSymbol> modules = this._sourceAssembly.Modules;
					for (int i = 1; i < modules.Length; i++)
					{
						instance.Add((IFileReference)base.Translate(modules[i], A_2.context.Diagnostics));
					}
					if (!A_2.context.IsRefAssembly)
					{
						foreach (ResourceDescription resourceDescription in this.ManifestResources)
						{
							if (!resourceDescription.IsEmbedded)
							{
								instance.Add(resourceDescription);
							}
						}
					}
					if (ImmutableInterlocked.InterlockedInitialize<IFileReference>(ref lazyFiles, instance.ToImmutable()) && lazyFiles.Length > 0 && !CryptographicHashProvider.IsSupportedAlgorithm(this._sourceAssembly.HashAlgorithm))
					{
						A_2.context.Diagnostics.Add(new CSDiagnostic(new CSDiagnosticInfo(ErrorCode.ERR_CryptoHashFailed), NoLocation.Singleton, false));
					}
				}
				finally
				{
					instance.Free();
				}
			}
			return lazyFiles;
		}

		// Token: 0x0400195F RID: 6495
		private readonly SourceAssemblySymbol _sourceAssembly;

		// Token: 0x04001960 RID: 6496
		private readonly ImmutableArray<NamedTypeSymbol> _additionalTypes;

		// Token: 0x04001961 RID: 6497
		private ImmutableArray<IFileReference> _lazyFiles;

		// Token: 0x04001962 RID: 6498
		private ImmutableArray<IFileReference> _lazyFilesWithoutManifestResources;

		// Token: 0x04001963 RID: 6499
		private SynthesizedEmbeddedAttributeSymbol _lazyEmbeddedAttribute;

		// Token: 0x04001964 RID: 6500
		private SynthesizedEmbeddedAttributeSymbol _lazyIsReadOnlyAttribute;

		// Token: 0x04001965 RID: 6501
		private SynthesizedEmbeddedAttributeSymbol _lazyIsByRefLikeAttribute;

		// Token: 0x04001966 RID: 6502
		private SynthesizedEmbeddedAttributeSymbol _lazyIsUnmanagedAttribute;

		// Token: 0x04001967 RID: 6503
		private SynthesizedEmbeddedNullableAttributeSymbol _lazyNullableAttribute;

		// Token: 0x04001968 RID: 6504
		private SynthesizedEmbeddedNullableContextAttributeSymbol _lazyNullableContextAttribute;

		// Token: 0x04001969 RID: 6505
		private SynthesizedEmbeddedNullablePublicOnlyAttributeSymbol _lazyNullablePublicOnlyAttribute;

		// Token: 0x0400196A RID: 6506
		private SynthesizedEmbeddedNativeIntegerAttributeSymbol _lazyNativeIntegerAttribute;

		// Token: 0x0400196B RID: 6507
		private readonly string _metadataName;
	}
}
