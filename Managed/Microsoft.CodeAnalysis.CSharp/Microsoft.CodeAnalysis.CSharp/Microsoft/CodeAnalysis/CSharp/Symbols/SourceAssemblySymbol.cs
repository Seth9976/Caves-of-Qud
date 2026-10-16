using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Microsoft.CodeAnalysis.CSharp.Symbols.Metadata.PE;
using Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004B6 RID: 1206
	internal sealed class SourceAssemblySymbol : MetadataOrSourceAssemblySymbol, ISourceAssemblySymbolInternal, IAssemblySymbolInternal, ISymbolInternal, IAttributeTargetSymbol
	{
		// Token: 0x0600668F RID: 26255 RVA: 0x0018217C File Offset: 0x0018037C
		internal IEnumerable<CSharpAttributeData> GetCustomAttributesToEmit(PEModuleBuilder moduleBuilder, bool emittingRefAssembly, bool emittingAssemblyAttributesInNetModule)
		{
			ImmutableArray<CSharpAttributeData> attributes = this.GetAttributes();
			ArrayBuilder<SynthesizedAttributeData> arrayBuilder = null;
			this.AddSynthesizedAttributes(moduleBuilder, ref arrayBuilder);
			if (emittingRefAssembly && !this.HasReferenceAssemblyAttribute)
			{
				SynthesizedAttributeData synthesizedAttributeData = this.DeclaringCompilation.TrySynthesizeAttribute(WellKnownMember.System_Runtime_CompilerServices_ReferenceAssemblyAttribute__ctor, default(ImmutableArray<TypedConstant>), default(ImmutableArray<KeyValuePair<WellKnownMember, TypedConstant>>), true);
				Symbol.AddSynthesizedAttribute(ref arrayBuilder, synthesizedAttributeData);
			}
			return base.GetCustomAttributesToEmit(attributes, arrayBuilder, false, emittingAssemblyAttributesInNetModule);
		}

		// Token: 0x06006690 RID: 26256 RVA: 0x001821E0 File Offset: 0x001803E0
		internal SourceAssemblySymbol(CSharpCompilation compilation, string assemblySimpleName, string moduleName, ImmutableArray<PEModule> netModules)
		{
			this._compilation = compilation;
			this._assemblySimpleName = assemblySimpleName;
			ArrayBuilder<ModuleSymbol> arrayBuilder = new ArrayBuilder<ModuleSymbol>(1 + netModules.Length);
			arrayBuilder.Add(new SourceModuleSymbol(this, compilation.Declarations, moduleName));
			MetadataImportOptions metadataImportOptions = ((compilation.Options.MetadataImportOptions == MetadataImportOptions.All) ? MetadataImportOptions.All : MetadataImportOptions.Internal);
			foreach (PEModule pemodule in netModules)
			{
				arrayBuilder.Add(new PEModuleSymbol(this, pemodule, metadataImportOptions, arrayBuilder.Count));
			}
			this._modules = arrayBuilder.ToImmutableAndFree();
			if (!compilation.Options.CryptoPublicKey.IsEmpty)
			{
				this._lazyStrongNameKeys = StrongNameKeys.Create(compilation.Options.CryptoPublicKey, default(RSAParameters?), false, MessageProvider.Instance);
			}
		}

		// Token: 0x17001970 RID: 6512
		// (get) Token: 0x06006691 RID: 26257 RVA: 0x001822CE File Offset: 0x001804CE
		public override string Name
		{
			get
			{
				return this._assemblySimpleName;
			}
		}

		// Token: 0x17001971 RID: 6513
		// (get) Token: 0x06006692 RID: 26258 RVA: 0x001822D6 File Offset: 0x001804D6
		internal sealed override CSharpCompilation DeclaringCompilation
		{
			get
			{
				return this._compilation;
			}
		}

		// Token: 0x17001972 RID: 6514
		// (get) Token: 0x06006693 RID: 26259 RVA: 0x001822DE File Offset: 0x001804DE
		public override bool IsInteractive
		{
			get
			{
				return this._compilation.IsSubmission;
			}
		}

		// Token: 0x06006694 RID: 26260 RVA: 0x001822EC File Offset: 0x001804EC
		internal bool MightContainNoPiaLocalTypes()
		{
			for (int i = 1; i < this._modules.Length; i++)
			{
				if (((PEModuleSymbol)this._modules[i]).Module.ContainsNoPiaLocalTypes())
				{
					return true;
				}
			}
			return this.SourceModule.MightContainNoPiaLocalTypes();
		}

		// Token: 0x17001973 RID: 6515
		// (get) Token: 0x06006695 RID: 26261 RVA: 0x0018233F File Offset: 0x0018053F
		public override AssemblyIdentity Identity
		{
			get
			{
				if (this.lazyAssemblyIdentity == null)
				{
					Interlocked.CompareExchange<AssemblyIdentity>(ref this.lazyAssemblyIdentity, this.ComputeIdentity(), null);
				}
				return this.lazyAssemblyIdentity;
			}
		}

		// Token: 0x06006696 RID: 26262 RVA: 0x00182368 File Offset: 0x00180568
		internal override Symbol GetSpecialTypeMember(SpecialMember member)
		{
			if (!this._compilation.IsMemberMissing(member))
			{
				return base.GetSpecialTypeMember(member);
			}
			return null;
		}

		// Token: 0x06006697 RID: 26263 RVA: 0x00182384 File Offset: 0x00180584
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private string GetWellKnownAttributeDataStringField([global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 1, 0, 1 })] Func<CommonAssemblyWellKnownAttributeData<NamedTypeSymbol>, string> fieldGetter, string missingValue = null, QuickAttributes? attributeMatchesOpt = null)
		{
			string text = missingValue;
			CommonAssemblyWellKnownAttributeData<NamedTypeSymbol> commonAssemblyWellKnownAttributeData = ((attributeMatchesOpt == null) ? this.GetSourceDecodedWellKnownAttributeData() : this.GetSourceDecodedWellKnownAttributeData(attributeMatchesOpt.Value));
			if (commonAssemblyWellKnownAttributeData != null)
			{
				text = fieldGetter.Invoke(commonAssemblyWellKnownAttributeData);
			}
			if (text == missingValue)
			{
				commonAssemblyWellKnownAttributeData = ((attributeMatchesOpt == null || this._lazyNetModuleAttributesBag != null) ? this.GetNetModuleDecodedWellKnownAttributeData() : this.GetLimitedNetModuleDecodedWellKnownAttributeData(attributeMatchesOpt.Value));
				if (commonAssemblyWellKnownAttributeData != null)
				{
					text = fieldGetter.Invoke(commonAssemblyWellKnownAttributeData);
				}
			}
			return text;
		}

		// Token: 0x17001974 RID: 6516
		// (get) Token: 0x06006698 RID: 26264 RVA: 0x001823F4 File Offset: 0x001805F4
		internal bool RuntimeCompatibilityWrapNonExceptionThrows
		{
			get
			{
				CommonAssemblyWellKnownAttributeData<NamedTypeSymbol> commonAssemblyWellKnownAttributeData = this.GetSourceDecodedWellKnownAttributeData() ?? this.GetNetModuleDecodedWellKnownAttributeData();
				return commonAssemblyWellKnownAttributeData == null || commonAssemblyWellKnownAttributeData.RuntimeCompatibilityWrapNonExceptionThrows;
			}
		}

		// Token: 0x17001975 RID: 6517
		// (get) Token: 0x06006699 RID: 26265 RVA: 0x00182420 File Offset: 0x00180620
		internal string FileVersion
		{
			get
			{
				return this.GetWellKnownAttributeDataStringField(([global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 0 })] CommonAssemblyWellKnownAttributeData<NamedTypeSymbol> data) => data.AssemblyFileVersionAttributeSetting, null, default(QuickAttributes?));
			}
		}

		// Token: 0x17001976 RID: 6518
		// (get) Token: 0x0600669A RID: 26266 RVA: 0x0018245C File Offset: 0x0018065C
		internal string Title
		{
			get
			{
				return this.GetWellKnownAttributeDataStringField(([global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 0 })] CommonAssemblyWellKnownAttributeData<NamedTypeSymbol> data) => data.AssemblyTitleAttributeSetting, null, default(QuickAttributes?));
			}
		}

		// Token: 0x17001977 RID: 6519
		// (get) Token: 0x0600669B RID: 26267 RVA: 0x00182498 File Offset: 0x00180698
		internal string Description
		{
			get
			{
				return this.GetWellKnownAttributeDataStringField(([global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 0 })] CommonAssemblyWellKnownAttributeData<NamedTypeSymbol> data) => data.AssemblyDescriptionAttributeSetting, null, default(QuickAttributes?));
			}
		}

		// Token: 0x17001978 RID: 6520
		// (get) Token: 0x0600669C RID: 26268 RVA: 0x001824D4 File Offset: 0x001806D4
		internal string Company
		{
			get
			{
				return this.GetWellKnownAttributeDataStringField(([global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 0 })] CommonAssemblyWellKnownAttributeData<NamedTypeSymbol> data) => data.AssemblyCompanyAttributeSetting, null, default(QuickAttributes?));
			}
		}

		// Token: 0x17001979 RID: 6521
		// (get) Token: 0x0600669D RID: 26269 RVA: 0x00182510 File Offset: 0x00180710
		internal string Product
		{
			get
			{
				return this.GetWellKnownAttributeDataStringField(([global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 0 })] CommonAssemblyWellKnownAttributeData<NamedTypeSymbol> data) => data.AssemblyProductAttributeSetting, null, default(QuickAttributes?));
			}
		}

		// Token: 0x1700197A RID: 6522
		// (get) Token: 0x0600669E RID: 26270 RVA: 0x0018254C File Offset: 0x0018074C
		internal string InformationalVersion
		{
			get
			{
				return this.GetWellKnownAttributeDataStringField(([global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 0 })] CommonAssemblyWellKnownAttributeData<NamedTypeSymbol> data) => data.AssemblyInformationalVersionAttributeSetting, null, default(QuickAttributes?));
			}
		}

		// Token: 0x1700197B RID: 6523
		// (get) Token: 0x0600669F RID: 26271 RVA: 0x00182588 File Offset: 0x00180788
		internal string Copyright
		{
			get
			{
				return this.GetWellKnownAttributeDataStringField(([global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 0 })] CommonAssemblyWellKnownAttributeData<NamedTypeSymbol> data) => data.AssemblyCopyrightAttributeSetting, null, default(QuickAttributes?));
			}
		}

		// Token: 0x1700197C RID: 6524
		// (get) Token: 0x060066A0 RID: 26272 RVA: 0x001825C4 File Offset: 0x001807C4
		internal string Trademark
		{
			get
			{
				return this.GetWellKnownAttributeDataStringField(([global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 0 })] CommonAssemblyWellKnownAttributeData<NamedTypeSymbol> data) => data.AssemblyTrademarkAttributeSetting, null, default(QuickAttributes?));
			}
		}

		// Token: 0x1700197D RID: 6525
		// (get) Token: 0x060066A1 RID: 26273 RVA: 0x00182600 File Offset: 0x00180800
		private ThreeState AssemblyDelaySignAttributeSetting
		{
			get
			{
				ThreeState threeState = ThreeState.Unknown;
				ThreeState threeState2 = threeState;
				CommonAssemblyWellKnownAttributeData<NamedTypeSymbol> commonAssemblyWellKnownAttributeData = this.GetSourceDecodedWellKnownAttributeData();
				if (commonAssemblyWellKnownAttributeData != null)
				{
					threeState2 = commonAssemblyWellKnownAttributeData.AssemblyDelaySignAttributeSetting;
				}
				if (threeState2 == threeState)
				{
					commonAssemblyWellKnownAttributeData = this.GetNetModuleDecodedWellKnownAttributeData();
					if (commonAssemblyWellKnownAttributeData != null)
					{
						threeState2 = commonAssemblyWellKnownAttributeData.AssemblyDelaySignAttributeSetting;
					}
				}
				return threeState2;
			}
		}

		// Token: 0x1700197E RID: 6526
		// (get) Token: 0x060066A2 RID: 26274 RVA: 0x00182638 File Offset: 0x00180838
		private string AssemblyKeyContainerAttributeSetting
		{
			get
			{
				return this.GetWellKnownAttributeDataStringField(([global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 0 })] CommonAssemblyWellKnownAttributeData<NamedTypeSymbol> data) => data.AssemblyKeyContainerAttributeSetting, WellKnownAttributeData.StringMissingValue, new QuickAttributes?(QuickAttributes.AssemblyKeyName));
			}
		}

		// Token: 0x1700197F RID: 6527
		// (get) Token: 0x060066A3 RID: 26275 RVA: 0x0018266A File Offset: 0x0018086A
		private string AssemblyKeyFileAttributeSetting
		{
			get
			{
				return this.GetWellKnownAttributeDataStringField(([global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 0 })] CommonAssemblyWellKnownAttributeData<NamedTypeSymbol> data) => data.AssemblyKeyFileAttributeSetting, WellKnownAttributeData.StringMissingValue, new QuickAttributes?(QuickAttributes.AssemblyKeyFile));
			}
		}

		// Token: 0x17001980 RID: 6528
		// (get) Token: 0x060066A4 RID: 26276 RVA: 0x0018269C File Offset: 0x0018089C
		private string AssemblyCultureAttributeSetting
		{
			get
			{
				return this.GetWellKnownAttributeDataStringField(([global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 0 })] CommonAssemblyWellKnownAttributeData<NamedTypeSymbol> data) => data.AssemblyCultureAttributeSetting, null, default(QuickAttributes?));
			}
		}

		// Token: 0x17001981 RID: 6529
		// (get) Token: 0x060066A5 RID: 26277 RVA: 0x001826D8 File Offset: 0x001808D8
		public string SignatureKey
		{
			get
			{
				return this.GetWellKnownAttributeDataStringField(([global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 0 })] CommonAssemblyWellKnownAttributeData<NamedTypeSymbol> data) => data.AssemblySignatureKeyAttributeSetting, null, new QuickAttributes?(QuickAttributes.AssemblySignatureKey));
			}
		}

		// Token: 0x17001982 RID: 6530
		// (get) Token: 0x060066A6 RID: 26278 RVA: 0x00182708 File Offset: 0x00180908
		private Version AssemblyVersionAttributeSetting
		{
			get
			{
				Version version = null;
				Version version2 = version;
				CommonAssemblyWellKnownAttributeData<NamedTypeSymbol> commonAssemblyWellKnownAttributeData = this.GetSourceDecodedWellKnownAttributeData();
				if (commonAssemblyWellKnownAttributeData != null)
				{
					version2 = commonAssemblyWellKnownAttributeData.AssemblyVersionAttributeSetting;
				}
				if (version2 == version)
				{
					commonAssemblyWellKnownAttributeData = this.GetNetModuleDecodedWellKnownAttributeData();
					if (commonAssemblyWellKnownAttributeData != null)
					{
						version2 = commonAssemblyWellKnownAttributeData.AssemblyVersionAttributeSetting;
					}
				}
				return version2;
			}
		}

		// Token: 0x17001983 RID: 6531
		// (get) Token: 0x060066A7 RID: 26279 RVA: 0x00182748 File Offset: 0x00180948
		public override Version AssemblyVersionPattern
		{
			get
			{
				Version assemblyVersionAttributeSetting = this.AssemblyVersionAttributeSetting;
				if (assemblyVersionAttributeSetting != null && (assemblyVersionAttributeSetting.Build == 65535 || assemblyVersionAttributeSetting.Revision == 65535))
				{
					return assemblyVersionAttributeSetting;
				}
				return null;
			}
		}

		// Token: 0x17001984 RID: 6532
		// (get) Token: 0x060066A8 RID: 26280 RVA: 0x0018277C File Offset: 0x0018097C
		public AssemblyHashAlgorithm HashAlgorithm
		{
			get
			{
				AssemblyHashAlgorithm? assemblyAlgorithmIdAttributeSetting = this.AssemblyAlgorithmIdAttributeSetting;
				if (assemblyAlgorithmIdAttributeSetting == null)
				{
					return AssemblyHashAlgorithm.Sha1;
				}
				return assemblyAlgorithmIdAttributeSetting.GetValueOrDefault();
			}
		}

		// Token: 0x17001985 RID: 6533
		// (get) Token: 0x060066A9 RID: 26281 RVA: 0x001827A8 File Offset: 0x001809A8
		internal AssemblyHashAlgorithm? AssemblyAlgorithmIdAttributeSetting
		{
			get
			{
				AssemblyHashAlgorithm? assemblyHashAlgorithm = default(AssemblyHashAlgorithm?);
				CommonAssemblyWellKnownAttributeData<NamedTypeSymbol> commonAssemblyWellKnownAttributeData = this.GetSourceDecodedWellKnownAttributeData();
				if (commonAssemblyWellKnownAttributeData != null)
				{
					assemblyHashAlgorithm = commonAssemblyWellKnownAttributeData.AssemblyAlgorithmIdAttributeSetting;
				}
				if (assemblyHashAlgorithm == null)
				{
					commonAssemblyWellKnownAttributeData = this.GetNetModuleDecodedWellKnownAttributeData();
					if (commonAssemblyWellKnownAttributeData != null)
					{
						assemblyHashAlgorithm = commonAssemblyWellKnownAttributeData.AssemblyAlgorithmIdAttributeSetting;
					}
				}
				return assemblyHashAlgorithm;
			}
		}

		// Token: 0x17001986 RID: 6534
		// (get) Token: 0x060066AA RID: 26282 RVA: 0x001827EC File Offset: 0x001809EC
		public AssemblyFlags AssemblyFlags
		{
			get
			{
				AssemblyFlags assemblyFlags = (AssemblyFlags)0;
				CommonAssemblyWellKnownAttributeData<NamedTypeSymbol> commonAssemblyWellKnownAttributeData = this.GetSourceDecodedWellKnownAttributeData();
				if (commonAssemblyWellKnownAttributeData != null)
				{
					assemblyFlags = commonAssemblyWellKnownAttributeData.AssemblyFlagsAttributeSetting;
				}
				commonAssemblyWellKnownAttributeData = this.GetNetModuleDecodedWellKnownAttributeData();
				if (commonAssemblyWellKnownAttributeData != null)
				{
					assemblyFlags |= commonAssemblyWellKnownAttributeData.AssemblyFlagsAttributeSetting;
				}
				return assemblyFlags;
			}
		}

		// Token: 0x060066AB RID: 26283 RVA: 0x00182820 File Offset: 0x00180A20
		private StrongNameKeys ComputeStrongNameKeys()
		{
			string text = this._compilation.Options.CryptoKeyFile;
			if (!this.DeclaringCompilation.Options.PublicSign)
			{
				if (string.IsNullOrEmpty(text))
				{
					text = this.AssemblyKeyFileAttributeSetting;
					if (text == WellKnownAttributeData.StringMissingValue)
					{
						text = null;
					}
				}
				string text2 = this._compilation.Options.CryptoKeyContainer;
				if (string.IsNullOrEmpty(text2))
				{
					text2 = this.AssemblyKeyContainerAttributeSetting;
					if (text2 == WellKnownAttributeData.StringMissingValue)
					{
						text2 = null;
					}
				}
				bool flag = !string.IsNullOrEmpty(this.SignatureKey);
				return StrongNameKeys.Create(this.DeclaringCompilation.Options.StrongNameProvider, text, text2, flag, MessageProvider.Instance);
			}
			if (!string.IsNullOrEmpty(text) && !PathUtilities.IsAbsolute(text))
			{
				return StrongNameKeys.None;
			}
			return StrongNameKeys.Create(text, MessageProvider.Instance);
		}

		// Token: 0x17001987 RID: 6535
		// (get) Token: 0x060066AC RID: 26284 RVA: 0x001828E4 File Offset: 0x00180AE4
		internal StrongNameKeys StrongNameKeys
		{
			get
			{
				if (this._lazyStrongNameKeys == null)
				{
					try
					{
						SourceAssemblySymbol.t_assemblyForWhichCurrentThreadIsComputingKeys = this;
						Interlocked.CompareExchange<StrongNameKeys>(ref this._lazyStrongNameKeys, this.ComputeStrongNameKeys(), null);
					}
					finally
					{
						SourceAssemblySymbol.t_assemblyForWhichCurrentThreadIsComputingKeys = null;
					}
				}
				return this._lazyStrongNameKeys;
			}
		}

		// Token: 0x17001988 RID: 6536
		// (get) Token: 0x060066AD RID: 26285 RVA: 0x00182934 File Offset: 0x00180B34
		internal override ImmutableArray<byte> PublicKey
		{
			get
			{
				return this.StrongNameKeys.PublicKey;
			}
		}

		// Token: 0x17001989 RID: 6537
		// (get) Token: 0x060066AE RID: 26286 RVA: 0x00182941 File Offset: 0x00180B41
		public override ImmutableArray<ModuleSymbol> Modules
		{
			get
			{
				return this._modules;
			}
		}

		// Token: 0x1700198A RID: 6538
		// (get) Token: 0x060066AF RID: 26287 RVA: 0x00182949 File Offset: 0x00180B49
		public override ImmutableArray<Location> Locations
		{
			get
			{
				return Enumerable.SelectMany<ModuleSymbol, Location>(this.Modules, (ModuleSymbol m) => m.Locations).AsImmutable<Location>();
			}
		}

		// Token: 0x060066B0 RID: 26288 RVA: 0x00182980 File Offset: 0x00180B80
		private void ValidateAttributeSemantics(BindingDiagnosticBag diagnostics)
		{
			if (this.StrongNameKeys.DiagnosticOpt != null && !this._compilation.Options.OutputKind.IsNetModule())
			{
				diagnostics.Add(this.StrongNameKeys.DiagnosticOpt);
			}
			this.ValidateIVTPublicKeys(diagnostics);
			this.CheckOptimisticIVTAccessGrants(diagnostics);
			this.DetectAttributeAndOptionConflicts(diagnostics);
			if (this.IsDelaySigned && !this.Identity.HasPublicKey)
			{
				diagnostics.Add(ErrorCode.WRN_DelaySignButNoKey, NoLocation.Singleton);
			}
			if (this.DeclaringCompilation.Options.PublicSign)
			{
				if (this._compilation.Options.OutputKind.IsNetModule())
				{
					diagnostics.Add(ErrorCode.ERR_PublicSignNetModule, NoLocation.Singleton);
				}
				else if (!this.Identity.HasPublicKey)
				{
					diagnostics.Add(ErrorCode.ERR_PublicSignButNoKey, NoLocation.Singleton);
				}
			}
			if (this.DeclaringCompilation.Options.OutputKind != OutputKind.NetModule && this.DeclaringCompilation.Options.CryptoPublicKey.IsEmpty && this.Identity.HasPublicKey && !this.IsDelaySigned && !this.DeclaringCompilation.Options.PublicSign && !this.StrongNameKeys.CanSign && this.StrongNameKeys.DiagnosticOpt == null)
			{
				diagnostics.Add(ErrorCode.ERR_SignButNoPrivateKey, NoLocation.Singleton, new object[] { this.StrongNameKeys.KeyFilePath });
			}
			SourceAssemblySymbol.ReportDiagnosticsForSynthesizedAttributes(this._compilation, diagnostics);
		}

		// Token: 0x060066B1 RID: 26289 RVA: 0x00182AFC File Offset: 0x00180CFC
		private static void ReportDiagnosticsForSynthesizedAttributes(CSharpCompilation compilation, BindingDiagnosticBag diagnostics)
		{
			SourceAssemblySymbol.ReportDiagnosticsForUnsafeSynthesizedAttributes(compilation, diagnostics);
			if (!compilation.Options.OutputKind.IsNetModule())
			{
				if (!(compilation.GetWellKnownType(WellKnownType.System_Runtime_CompilerServices_CompilationRelaxationsAttribute) is MissingMetadataTypeSymbol))
				{
					Binder.ReportUseSiteDiagnosticForSynthesizedAttribute(compilation, WellKnownMember.System_Runtime_CompilerServices_CompilationRelaxationsAttribute__ctorInt32, diagnostics, NoLocation.Singleton, null);
				}
				if (!(compilation.GetWellKnownType(WellKnownType.System_Runtime_CompilerServices_RuntimeCompatibilityAttribute) is MissingMetadataTypeSymbol))
				{
					Binder.ReportUseSiteDiagnosticForSynthesizedAttribute(compilation, WellKnownMember.System_Runtime_CompilerServices_RuntimeCompatibilityAttribute__ctor, diagnostics, NoLocation.Singleton, null);
					Binder.ReportUseSiteDiagnosticForSynthesizedAttribute(compilation, WellKnownMember.System_Runtime_CompilerServices_RuntimeCompatibilityAttribute__WrapNonExceptionThrows, diagnostics, NoLocation.Singleton, null);
				}
			}
		}

		// Token: 0x060066B2 RID: 26290 RVA: 0x00182B74 File Offset: 0x00180D74
		private static void ReportDiagnosticsForUnsafeSynthesizedAttributes(CSharpCompilation compilation, BindingDiagnosticBag diagnostics)
		{
			if (!compilation.Options.AllowUnsafe)
			{
				return;
			}
			if (compilation.GetWellKnownType(WellKnownType.System_Security_UnverifiableCodeAttribute) is MissingMetadataTypeSymbol)
			{
				return;
			}
			Binder.ReportUseSiteDiagnosticForSynthesizedAttribute(compilation, WellKnownMember.System_Security_UnverifiableCodeAttribute__ctor, diagnostics, NoLocation.Singleton, null);
			if (compilation.GetWellKnownType(WellKnownType.System_Security_Permissions_SecurityPermissionAttribute) is MissingMetadataTypeSymbol)
			{
				return;
			}
			if (compilation.GetWellKnownType(WellKnownType.System_Security_Permissions_SecurityAction) is MissingMetadataTypeSymbol)
			{
				return;
			}
			Binder.ReportUseSiteDiagnosticForSynthesizedAttribute(compilation, WellKnownMember.System_Security_Permissions_SecurityPermissionAttribute__ctor, diagnostics, NoLocation.Singleton, null);
			Binder.ReportUseSiteDiagnosticForSynthesizedAttribute(compilation, WellKnownMember.System_Security_Permissions_SecurityPermissionAttribute__SkipVerification, diagnostics, NoLocation.Singleton, null);
		}

		// Token: 0x060066B3 RID: 26291 RVA: 0x00182BFC File Offset: 0x00180DFC
		private void ValidateIVTPublicKeys(BindingDiagnosticBag diagnostics)
		{
			this.EnsureAttributesAreBound();
			if (!this.Identity.IsStrongName)
			{
				return;
			}
			if (this._lazyInternalsVisibleToMap != null)
			{
				foreach (ConcurrentDictionary<ImmutableArray<byte>, Tuple<Location, string>> concurrentDictionary in this._lazyInternalsVisibleToMap.Values)
				{
					foreach (KeyValuePair<ImmutableArray<byte>, Tuple<Location, string>> keyValuePair in concurrentDictionary)
					{
						if (keyValuePair.Key.IsDefaultOrEmpty)
						{
							diagnostics.Add(ErrorCode.ERR_FriendAssemblySNReq, keyValuePair.Value.Item1, new object[] { keyValuePair.Value.Item2 });
						}
					}
				}
			}
		}

		// Token: 0x1700198B RID: 6539
		// (get) Token: 0x060066B4 RID: 26292 RVA: 0x00182CD4 File Offset: 0x00180ED4
		public bool InternalsAreVisible
		{
			get
			{
				this.EnsureAttributesAreBound();
				return this._lazyInternalsVisibleToMap != null;
			}
		}

		// Token: 0x060066B5 RID: 26293 RVA: 0x00182CE8 File Offset: 0x00180EE8
		private void DetectAttributeAndOptionConflicts(BindingDiagnosticBag diagnostics)
		{
			this.EnsureAttributesAreBound();
			ThreeState assemblyDelaySignAttributeSetting = this.AssemblyDelaySignAttributeSetting;
			if (this._compilation.Options.DelaySign != null && assemblyDelaySignAttributeSetting != ThreeState.Unknown && this.DeclaringCompilation.Options.DelaySign.Value != (assemblyDelaySignAttributeSetting == ThreeState.True))
			{
				diagnostics.Add(ErrorCode.WRN_CmdOptionConflictsSource, NoLocation.Singleton, new object[]
				{
					"DelaySign",
					AttributeDescription.AssemblyDelaySignAttribute.FullName
				});
			}
			if (this._compilation.Options.PublicSign && assemblyDelaySignAttributeSetting == ThreeState.True)
			{
				diagnostics.Add(ErrorCode.WRN_CmdOptionConflictsSource, NoLocation.Singleton, new object[]
				{
					"PublicSign",
					AttributeDescription.AssemblyDelaySignAttribute.FullName
				});
			}
			if (!string.IsNullOrEmpty(this._compilation.Options.CryptoKeyContainer))
			{
				string assemblyKeyContainerAttributeSetting = this.AssemblyKeyContainerAttributeSetting;
				if (assemblyKeyContainerAttributeSetting == WellKnownAttributeData.StringMissingValue)
				{
					if (this._compilation.Options.OutputKind == OutputKind.NetModule)
					{
						Binder.ReportUseSiteDiagnosticForSynthesizedAttribute(this._compilation, WellKnownMember.System_Reflection_AssemblyKeyNameAttribute__ctor, diagnostics, NoLocation.Singleton, null);
					}
				}
				else if (string.Compare(this._compilation.Options.CryptoKeyContainer, assemblyKeyContainerAttributeSetting, 5) != 0)
				{
					if (this._compilation.Options.OutputKind == OutputKind.NetModule)
					{
						diagnostics.Add(ErrorCode.ERR_CmdOptionConflictsSource, NoLocation.Singleton, new object[]
						{
							AttributeDescription.AssemblyKeyNameAttribute.FullName,
							"CryptoKeyContainer"
						});
					}
					else
					{
						diagnostics.Add(ErrorCode.WRN_CmdOptionConflictsSource, NoLocation.Singleton, new object[]
						{
							"CryptoKeyContainer",
							AttributeDescription.AssemblyKeyNameAttribute.FullName
						});
					}
				}
			}
			if (this._compilation.Options.PublicSign && !this._compilation.Options.OutputKind.IsNetModule() && this.AssemblyKeyContainerAttributeSetting != WellKnownAttributeData.StringMissingValue)
			{
				diagnostics.Add(ErrorCode.WRN_AttributeIgnoredWhenPublicSigning, NoLocation.Singleton, new object[] { AttributeDescription.AssemblyKeyNameAttribute.FullName });
			}
			if (!string.IsNullOrEmpty(this._compilation.Options.CryptoKeyFile))
			{
				string assemblyKeyFileAttributeSetting = this.AssemblyKeyFileAttributeSetting;
				if (assemblyKeyFileAttributeSetting == WellKnownAttributeData.StringMissingValue)
				{
					if (this._compilation.Options.OutputKind == OutputKind.NetModule)
					{
						Binder.ReportUseSiteDiagnosticForSynthesizedAttribute(this._compilation, WellKnownMember.System_Reflection_AssemblyKeyFileAttribute__ctor, diagnostics, NoLocation.Singleton, null);
					}
				}
				else if (string.Compare(this._compilation.Options.CryptoKeyFile, assemblyKeyFileAttributeSetting, 5) != 0)
				{
					if (this._compilation.Options.OutputKind == OutputKind.NetModule)
					{
						diagnostics.Add(ErrorCode.ERR_CmdOptionConflictsSource, NoLocation.Singleton, new object[]
						{
							AttributeDescription.AssemblyKeyFileAttribute.FullName,
							"CryptoKeyFile"
						});
					}
					else
					{
						diagnostics.Add(ErrorCode.WRN_CmdOptionConflictsSource, NoLocation.Singleton, new object[]
						{
							"CryptoKeyFile",
							AttributeDescription.AssemblyKeyFileAttribute.FullName
						});
					}
				}
			}
			if (this._compilation.Options.PublicSign && !this._compilation.Options.OutputKind.IsNetModule() && this.AssemblyKeyFileAttributeSetting != WellKnownAttributeData.StringMissingValue)
			{
				diagnostics.Add(ErrorCode.WRN_AttributeIgnoredWhenPublicSigning, NoLocation.Singleton, new object[] { AttributeDescription.AssemblyKeyFileAttribute.FullName });
			}
		}

		// Token: 0x1700198C RID: 6540
		// (get) Token: 0x060066B6 RID: 26294 RVA: 0x00183038 File Offset: 0x00181238
		internal bool IsDelaySigned
		{
			get
			{
				if (this._compilation.Options.DelaySign != null)
				{
					return this._compilation.Options.DelaySign.Value;
				}
				return !this._compilation.Options.PublicSign && this.AssemblyDelaySignAttributeSetting == ThreeState.True;
			}
		}

		// Token: 0x1700198D RID: 6541
		// (get) Token: 0x060066B7 RID: 26295 RVA: 0x00183098 File Offset: 0x00181298
		internal SourceModuleSymbol SourceModule
		{
			get
			{
				return (SourceModuleSymbol)this.Modules[0];
			}
		}

		// Token: 0x1700198E RID: 6542
		// (get) Token: 0x060066B8 RID: 26296 RVA: 0x001830B9 File Offset: 0x001812B9
		internal override bool RequiresCompletion
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060066B9 RID: 26297 RVA: 0x001830BC File Offset: 0x001812BC
		internal override bool HasComplete(CompletionPart part)
		{
			return this._state.HasComplete(part);
		}

		// Token: 0x060066BA RID: 26298 RVA: 0x001830CC File Offset: 0x001812CC
		internal override void ForceComplete(SourceLocation locationOpt, CancellationToken cancellationToken)
		{
			for (;;)
			{
				cancellationToken.ThrowIfCancellationRequested();
				CompletionPart nextIncompletePart = this._state.NextIncompletePart;
				if (nextIncompletePart <= CompletionPart.StartBaseType)
				{
					if (nextIncompletePart == CompletionPart.None)
					{
						return;
					}
					if (nextIncompletePart != CompletionPart.Attributes)
					{
						if (nextIncompletePart != CompletionPart.StartBaseType)
						{
							goto IL_00FB;
						}
						goto IL_0063;
					}
					else
					{
						this.EnsureAttributesAreBound();
					}
				}
				else if (nextIncompletePart <= CompletionPart.StartInterfaces)
				{
					if (nextIncompletePart == CompletionPart.FinishBaseType)
					{
						goto IL_0063;
					}
					if (nextIncompletePart != CompletionPart.StartInterfaces)
					{
						goto IL_00FB;
					}
					this.SourceModule.ForceComplete(locationOpt, cancellationToken);
					if (!this.SourceModule.HasComplete(CompletionPart.MembersCompleted))
					{
						break;
					}
					this._state.NotePartComplete(CompletionPart.StartInterfaces);
				}
				else
				{
					if (nextIncompletePart != CompletionPart.EnumUnderlyingType && nextIncompletePart != CompletionPart.TypeArguments)
					{
						goto IL_00FB;
					}
					if (this._state.NotePartComplete(CompletionPart.EnumUnderlyingType))
					{
						this.ReportDiagnosticsForAddedModules();
						this._state.NotePartComplete(CompletionPart.TypeArguments);
					}
				}
				IL_010C:
				this._state.SpinWaitComplete(nextIncompletePart, cancellationToken);
				continue;
				IL_0063:
				if (this._state.NotePartComplete(CompletionPart.StartBaseType))
				{
					BindingDiagnosticBag instance = BindingDiagnosticBag.GetInstance();
					this.ValidateAttributeSemantics(instance);
					this.AddDeclarationDiagnostics(instance);
					this._state.NotePartComplete(CompletionPart.FinishBaseType);
					instance.Free();
					goto IL_010C;
				}
				goto IL_010C;
				IL_00FB:
				this._state.NotePartComplete(CompletionPart.ReturnTypeAttributes | CompletionPart.Parameters | CompletionPart.Type | CompletionPart.FinishInterfaces | CompletionPart.TypeParameters | CompletionPart.Members | CompletionPart.TypeMembers | CompletionPart.SynthesizedExplicitImplementations | CompletionPart.StartMemberChecks | CompletionPart.FinishMemberChecks | CompletionPart.MembersCompleted);
				goto IL_010C;
			}
		}

		// Token: 0x060066BB RID: 26299 RVA: 0x001831F8 File Offset: 0x001813F8
		private void ReportDiagnosticsForAddedModules()
		{
			BindingDiagnosticBag instance = BindingDiagnosticBag.GetInstance();
			foreach (KeyValuePair<MetadataReference, int> keyValuePair in this._compilation.GetBoundReferenceManager().ReferencedModuleIndexMap)
			{
				PortableExecutableReference portableExecutableReference = keyValuePair.Key as PortableExecutableReference;
				if (portableExecutableReference != null && portableExecutableReference.FilePath != null)
				{
					string fileName = FileNameUtilities.GetFileName(portableExecutableReference.FilePath, true);
					string name = this._modules[keyValuePair.Value].Name;
					if (!string.Equals(fileName, name, 5))
					{
						instance.Add(ErrorCode.ERR_NetModuleNameMismatch, NoLocation.Singleton, new object[] { name, fileName });
					}
				}
			}
			if (this._modules.Length > 1 && !this._compilation.Options.OutputKind.IsNetModule())
			{
				Machine machine = base.Machine;
				bool flag = machine == Machine.I386 && !base.Bit32Required;
				HashSet<string> hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
				for (int i = 1; i < this._modules.Length; i++)
				{
					ModuleSymbol moduleSymbol = this._modules[i];
					if (!hashSet.Add(moduleSymbol.Name))
					{
						instance.Add(ErrorCode.ERR_NetModuleNameMustBeUnique, NoLocation.Singleton, new object[] { moduleSymbol.Name });
					}
					if (!((PEModuleSymbol)moduleSymbol).Module.IsCOFFOnly)
					{
						Machine machine2 = moduleSymbol.Machine;
						if (machine2 != Machine.I386 || moduleSymbol.Bit32Required)
						{
							if (flag)
							{
								instance.Add(ErrorCode.ERR_AgnosticToMachineModule, NoLocation.Singleton, new object[] { moduleSymbol });
							}
							else if (machine != machine2)
							{
								instance.Add(ErrorCode.ERR_ConflictingMachineModule, NoLocation.Singleton, new object[] { moduleSymbol });
							}
						}
					}
				}
				for (int j = 1; j < this._modules.Length; j++)
				{
					PEModuleSymbol pemoduleSymbol = (PEModuleSymbol)this._modules[j];
					try
					{
						foreach (string text in pemoduleSymbol.Module.GetReferencedManagedModulesOrThrow())
						{
							if (hashSet.Add(text))
							{
								instance.Add(ErrorCode.ERR_MissingNetModuleReference, NoLocation.Singleton, new object[] { text });
							}
						}
					}
					catch (BadImageFormatException)
					{
						instance.Add(new CSDiagnosticInfo(ErrorCode.ERR_BindToBogus, new object[] { pemoduleSymbol }), NoLocation.Singleton);
					}
				}
			}
			this.ReportNameCollisionDiagnosticsForAddedModules(this.GlobalNamespace, instance);
			this.AddDeclarationDiagnostics(instance);
			instance.Free();
		}

		// Token: 0x060066BC RID: 26300 RVA: 0x001834EC File Offset: 0x001816EC
		private void ReportNameCollisionDiagnosticsForAddedModules(NamespaceSymbol ns, BindingDiagnosticBag diagnostics)
		{
			MergedNamespaceSymbol mergedNamespaceSymbol = ns as MergedNamespaceSymbol;
			if (mergedNamespaceSymbol == null)
			{
				return;
			}
			ImmutableArray<NamespaceSymbol> constituentNamespaces = mergedNamespaceSymbol.ConstituentNamespaces;
			if (constituentNamespaces.Length > 2 || (constituentNamespaces.Length == 2 && constituentNamespaces[0].ContainingModule.Ordinal != 0 && constituentNamespaces[1].ContainingModule.Ordinal != 0))
			{
				ArrayBuilder<NamedTypeSymbol> instance = ArrayBuilder<NamedTypeSymbol>.GetInstance();
				foreach (NamespaceSymbol namespaceSymbol in constituentNamespaces)
				{
					if (namespaceSymbol.ContainingModule.Ordinal != 0)
					{
						instance.AddRange(namespaceSymbol.GetTypeMembers());
					}
				}
				instance.Sort(SourceAssemblySymbol.NameCollisionForAddedModulesTypeComparer.Singleton);
				bool flag = false;
				for (int i = 0; i < instance.Count - 1; i++)
				{
					NamedTypeSymbol namedTypeSymbol = instance[i];
					NamedTypeSymbol namedTypeSymbol2 = instance[i + 1];
					if (namedTypeSymbol.Arity == namedTypeSymbol2.Arity && namedTypeSymbol.Name == namedTypeSymbol2.Name)
					{
						if (!flag)
						{
							if (namedTypeSymbol.Arity != 0 || !namedTypeSymbol.ContainingNamespace.IsGlobalNamespace || namedTypeSymbol.Name != "<Module>")
							{
								diagnostics.Add(ErrorCode.ERR_DuplicateNameInNS, namedTypeSymbol2.Locations.FirstOrNone(), new object[]
								{
									namedTypeSymbol2.ToDisplayString(SymbolDisplayFormat.ShortFormat),
									namedTypeSymbol2.ContainingNamespace
								});
							}
							flag = true;
						}
					}
					else
					{
						flag = false;
					}
				}
				instance.Free();
				foreach (Symbol symbol in mergedNamespaceSymbol.GetMembers())
				{
					if (symbol.Kind == SymbolKind.Namespace)
					{
						this.ReportNameCollisionDiagnosticsForAddedModules((NamespaceSymbol)symbol, diagnostics);
					}
				}
			}
		}

		// Token: 0x060066BD RID: 26301 RVA: 0x001836A0 File Offset: 0x001818A0
		private bool IsKnownAssemblyAttribute(CSharpAttributeData attribute)
		{
			return attribute.IsTargetAttribute(this, AttributeDescription.AssemblyTitleAttribute) || attribute.IsTargetAttribute(this, AttributeDescription.AssemblyDescriptionAttribute) || attribute.IsTargetAttribute(this, AttributeDescription.AssemblyConfigurationAttribute) || attribute.IsTargetAttribute(this, AttributeDescription.AssemblyCultureAttribute) || attribute.IsTargetAttribute(this, AttributeDescription.AssemblyVersionAttribute) || attribute.IsTargetAttribute(this, AttributeDescription.AssemblyCompanyAttribute) || attribute.IsTargetAttribute(this, AttributeDescription.AssemblyProductAttribute) || attribute.IsTargetAttribute(this, AttributeDescription.AssemblyInformationalVersionAttribute) || attribute.IsTargetAttribute(this, AttributeDescription.AssemblyCopyrightAttribute) || attribute.IsTargetAttribute(this, AttributeDescription.AssemblyTrademarkAttribute) || attribute.IsTargetAttribute(this, AttributeDescription.AssemblyKeyFileAttribute) || attribute.IsTargetAttribute(this, AttributeDescription.AssemblyKeyNameAttribute) || attribute.IsTargetAttribute(this, AttributeDescription.AssemblyAlgorithmIdAttribute) || attribute.IsTargetAttribute(this, AttributeDescription.AssemblyFlagsAttribute) || attribute.IsTargetAttribute(this, AttributeDescription.AssemblyDelaySignAttribute) || attribute.IsTargetAttribute(this, AttributeDescription.AssemblyFileVersionAttribute) || attribute.IsTargetAttribute(this, AttributeDescription.SatelliteContractVersionAttribute) || attribute.IsTargetAttribute(this, AttributeDescription.AssemblySignatureKeyAttribute);
		}

		// Token: 0x060066BE RID: 26302 RVA: 0x001837C4 File Offset: 0x001819C4
		private void AddOmittedAttributeIndex(int index)
		{
			if (this._lazyOmittedAttributeIndices == null)
			{
				Interlocked.CompareExchange<ConcurrentSet<int>>(ref this._lazyOmittedAttributeIndices, new ConcurrentSet<int>(), null);
			}
			this._lazyOmittedAttributeIndices.Add(index);
		}

		// Token: 0x060066BF RID: 26303 RVA: 0x001837F0 File Offset: 0x001819F0
		private HashSet<CSharpAttributeData> GetUniqueSourceAssemblyAttributes()
		{
			ImmutableArray<CSharpAttributeData> attributes = this.GetSourceAttributesBag().Attributes;
			HashSet<CSharpAttributeData> hashSet = null;
			for (int i = 0; i < attributes.Length; i++)
			{
				CSharpAttributeData csharpAttributeData = attributes[i];
				if (!csharpAttributeData.HasErrors && !SourceAssemblySymbol.AddUniqueAssemblyAttribute(csharpAttributeData, ref hashSet))
				{
					this.AddOmittedAttributeIndex(i);
				}
			}
			return hashSet;
		}

		// Token: 0x060066C0 RID: 26304 RVA: 0x00183840 File Offset: 0x00181A40
		private static bool AddUniqueAssemblyAttribute(CSharpAttributeData attribute, ref HashSet<CSharpAttributeData> uniqueAttributes)
		{
			if (uniqueAttributes == null)
			{
				uniqueAttributes = new HashSet<CSharpAttributeData>(CommonAttributeDataComparer.Instance);
			}
			return uniqueAttributes.Add(attribute);
		}

		// Token: 0x060066C1 RID: 26305 RVA: 0x0018385C File Offset: 0x00181A5C
		private bool ValidateAttributeUsageForNetModuleAttribute(CSharpAttributeData attribute, string netModuleName, BindingDiagnosticBag diagnostics, ref HashSet<CSharpAttributeData> uniqueAttributes)
		{
			NamedTypeSymbol attributeClass = attribute.AttributeClass;
			if (attributeClass.GetAttributeUsageInfo().AllowMultiple)
			{
				return SourceAssemblySymbol.AddUniqueAssemblyAttribute(attribute, ref uniqueAttributes);
			}
			if (uniqueAttributes == null || !uniqueAttributes.Contains<CSharpAttributeData>((CSharpAttributeData a) => TypeSymbol.Equals(a.AttributeClass, attributeClass, TypeCompareKind.ConsiderEverything)))
			{
				SourceAssemblySymbol.AddUniqueAssemblyAttribute(attribute, ref uniqueAttributes);
				return true;
			}
			if (this.IsKnownAssemblyAttribute(attribute))
			{
				if (!uniqueAttributes.Contains(attribute))
				{
					diagnostics.Add(ErrorCode.WRN_AssemblyAttributeFromModuleIsOverridden, NoLocation.Singleton, new object[] { attribute.AttributeClass, netModuleName });
				}
			}
			else if (SourceAssemblySymbol.AddUniqueAssemblyAttribute(attribute, ref uniqueAttributes))
			{
				diagnostics.Add(ErrorCode.ERR_DuplicateAttributeInNetModule, NoLocation.Singleton, new object[]
				{
					attribute.AttributeClass.Name,
					netModuleName
				});
			}
			return false;
		}

		// Token: 0x060066C2 RID: 26306 RVA: 0x00183930 File Offset: 0x00181B30
		private ImmutableArray<CSharpAttributeData> GetNetModuleAttributes(out ImmutableArray<string> netModuleNames)
		{
			ArrayBuilder<CSharpAttributeData> arrayBuilder = null;
			ArrayBuilder<string> arrayBuilder2 = null;
			for (int i = 1; i < this._modules.Length; i++)
			{
				PEModuleSymbol pemoduleSymbol = (PEModuleSymbol)this._modules[i];
				string name = pemoduleSymbol.Name;
				foreach (CSharpAttributeData csharpAttributeData in pemoduleSymbol.GetAssemblyAttributes())
				{
					if (arrayBuilder2 == null)
					{
						arrayBuilder2 = ArrayBuilder<string>.GetInstance();
						arrayBuilder = ArrayBuilder<CSharpAttributeData>.GetInstance();
					}
					arrayBuilder2.Add(name);
					arrayBuilder.Add(csharpAttributeData);
				}
			}
			if (arrayBuilder2 == null)
			{
				netModuleNames = ImmutableArray<string>.Empty;
				return ImmutableArray<CSharpAttributeData>.Empty;
			}
			netModuleNames = arrayBuilder2.ToImmutableAndFree();
			return arrayBuilder.ToImmutableAndFree();
		}

		// Token: 0x060066C3 RID: 26307 RVA: 0x001839E0 File Offset: 0x00181BE0
		private WellKnownAttributeData ValidateAttributeUsageAndDecodeWellKnownAttributes(ImmutableArray<CSharpAttributeData> attributesFromNetModules, ImmutableArray<string> netModuleNames, BindingDiagnosticBag diagnostics)
		{
			int length = attributesFromNetModules.Length;
			int length2 = this.GetSourceAttributesBag().Attributes.Length;
			HashSet<CSharpAttributeData> uniqueSourceAssemblyAttributes = this.GetUniqueSourceAssemblyAttributes();
			DecodeWellKnownAttributeArguments<AttributeSyntax, CSharpAttributeData, AttributeLocation> decodeWellKnownAttributeArguments = default(DecodeWellKnownAttributeArguments<AttributeSyntax, CSharpAttributeData, AttributeLocation>);
			decodeWellKnownAttributeArguments.AttributesCount = length;
			decodeWellKnownAttributeArguments.Diagnostics = diagnostics;
			decodeWellKnownAttributeArguments.SymbolPart = AttributeLocation.None;
			for (int i = length - 1; i >= 0; i--)
			{
				int num = i + length2;
				CSharpAttributeData csharpAttributeData = attributesFromNetModules[i];
				if (!csharpAttributeData.HasErrors && this.ValidateAttributeUsageForNetModuleAttribute(csharpAttributeData, netModuleNames[i], diagnostics, ref uniqueSourceAssemblyAttributes))
				{
					decodeWellKnownAttributeArguments.Attribute = csharpAttributeData;
					decodeWellKnownAttributeArguments.Index = i;
					decodeWellKnownAttributeArguments.AttributeSyntaxOpt = null;
					this.DecodeWellKnownAttribute(ref decodeWellKnownAttributeArguments, num, true);
				}
				else
				{
					this.AddOmittedAttributeIndex(num);
				}
			}
			if (!decodeWellKnownAttributeArguments.HasDecodedData)
			{
				return null;
			}
			return decodeWellKnownAttributeArguments.DecodedData;
		}

		// Token: 0x060066C4 RID: 26308 RVA: 0x00183AB4 File Offset: 0x00181CB4
		private void LoadAndValidateNetModuleAttributes(ref CustomAttributesBag<CSharpAttributeData> lazyNetModuleAttributesBag)
		{
			if (this._compilation.Options.OutputKind.IsNetModule())
			{
				Interlocked.CompareExchange<CustomAttributesBag<CSharpAttributeData>>(ref lazyNetModuleAttributesBag, CustomAttributesBag<CSharpAttributeData>.Empty, null);
				return;
			}
			BindingDiagnosticBag instance = BindingDiagnosticBag.GetInstance();
			ImmutableArray<string> immutableArray;
			ImmutableArray<CSharpAttributeData> netModuleAttributes = this.GetNetModuleAttributes(out immutableArray);
			WellKnownAttributeData wellKnownAttributeData = null;
			if (netModuleAttributes.Any<CSharpAttributeData>())
			{
				wellKnownAttributeData = this.ValidateAttributeUsageAndDecodeWellKnownAttributes(netModuleAttributes, immutableArray, instance);
			}
			else
			{
				this.GetUniqueSourceAssemblyAttributes();
			}
			HashSet<NamedTypeSymbol> hashSet = null;
			for (int i = this._modules.Length - 1; i > 0; i--)
			{
				foreach (NamedTypeSymbol namedTypeSymbol in ((PEModuleSymbol)this._modules[i]).GetForwardedTypes())
				{
					if (hashSet == null)
					{
						if (wellKnownAttributeData == null)
						{
							wellKnownAttributeData = new CommonAssemblyWellKnownAttributeData<NamedTypeSymbol>();
						}
						hashSet = ((CommonAssemblyWellKnownAttributeData<NamedTypeSymbol>)wellKnownAttributeData).ForwardedTypes;
						if (hashSet == null)
						{
							hashSet = new HashSet<NamedTypeSymbol>();
							((CommonAssemblyWellKnownAttributeData<NamedTypeSymbol>)wellKnownAttributeData).ForwardedTypes = hashSet;
						}
					}
					if (hashSet.Add(namedTypeSymbol) && namedTypeSymbol.IsErrorType() && !instance.ReportUseSite(namedTypeSymbol, NoLocation.Singleton))
					{
						DiagnosticInfo errorInfo = ((ErrorTypeSymbol)namedTypeSymbol).ErrorInfo;
						if (errorInfo != null)
						{
							instance.Add(errorInfo, NoLocation.Singleton);
						}
					}
				}
			}
			CustomAttributesBag<CSharpAttributeData> customAttributesBag;
			if (wellKnownAttributeData != null || netModuleAttributes.Any<CSharpAttributeData>())
			{
				customAttributesBag = new CustomAttributesBag<CSharpAttributeData>();
				customAttributesBag.SetEarlyDecodedWellKnownAttributeData(null);
				customAttributesBag.SetDecodedWellKnownAttributeData(wellKnownAttributeData);
				customAttributesBag.SetAttributes(netModuleAttributes);
				if (customAttributesBag.IsEmpty)
				{
					customAttributesBag = CustomAttributesBag<CSharpAttributeData>.Empty;
				}
			}
			else
			{
				customAttributesBag = CustomAttributesBag<CSharpAttributeData>.Empty;
			}
			if (Interlocked.CompareExchange<CustomAttributesBag<CSharpAttributeData>>(ref lazyNetModuleAttributesBag, customAttributesBag, null) == null)
			{
				this.AddDeclarationDiagnostics(instance);
			}
			instance.Free();
		}

		// Token: 0x060066C5 RID: 26309 RVA: 0x00183C64 File Offset: 0x00181E64
		private CommonAssemblyWellKnownAttributeData<NamedTypeSymbol> GetLimitedNetModuleDecodedWellKnownAttributeData(QuickAttributes attributeMatches)
		{
			if (this._compilation.Options.OutputKind.IsNetModule())
			{
				return null;
			}
			ImmutableArray<string> immutableArray;
			ImmutableArray<CSharpAttributeData> netModuleAttributes = this.GetNetModuleAttributes(out immutableArray);
			WellKnownAttributeData wellKnownAttributeData = null;
			if (netModuleAttributes.Any<CSharpAttributeData>())
			{
				wellKnownAttributeData = this.<GetLimitedNetModuleDecodedWellKnownAttributeData>g__limitedDecodeWellKnownAttributes|103_0(netModuleAttributes, immutableArray, attributeMatches);
			}
			return (CommonAssemblyWellKnownAttributeData<NamedTypeSymbol>)wellKnownAttributeData;
		}

		// Token: 0x060066C6 RID: 26310 RVA: 0x00183CAD File Offset: 0x00181EAD
		private CustomAttributesBag<CSharpAttributeData> GetNetModuleAttributesBag()
		{
			if (this._lazyNetModuleAttributesBag == null)
			{
				this.LoadAndValidateNetModuleAttributes(ref this._lazyNetModuleAttributesBag);
			}
			return this._lazyNetModuleAttributesBag;
		}

		// Token: 0x060066C7 RID: 26311 RVA: 0x00183CC9 File Offset: 0x00181EC9
		internal CommonAssemblyWellKnownAttributeData<NamedTypeSymbol> GetNetModuleDecodedWellKnownAttributeData()
		{
			return (CommonAssemblyWellKnownAttributeData<NamedTypeSymbol>)this.GetNetModuleAttributesBag().DecodedWellKnownAttributeData;
		}

		// Token: 0x060066C8 RID: 26312 RVA: 0x00183CDC File Offset: 0x00181EDC
		internal ImmutableArray<SyntaxList<AttributeListSyntax>> GetAttributeDeclarations()
		{
			ArrayBuilder<SyntaxList<AttributeListSyntax>> instance = ArrayBuilder<SyntaxList<AttributeListSyntax>>.GetInstance();
			foreach (SingleNamespaceDeclaration singleNamespaceDeclaration in this.DeclaringCompilation.MergedRootDeclaration.Declarations)
			{
				RootSingleNamespaceDeclaration rootSingleNamespaceDeclaration = (RootSingleNamespaceDeclaration)singleNamespaceDeclaration;
				if (rootSingleNamespaceDeclaration.HasAssemblyAttributes)
				{
					CompilationUnitSyntax compilationUnitSyntax = (CompilationUnitSyntax)rootSingleNamespaceDeclaration.Location.SourceTree.GetRoot(default(CancellationToken));
					instance.Add(compilationUnitSyntax.AttributeLists);
				}
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x060066C9 RID: 26313 RVA: 0x00183D5C File Offset: 0x00181F5C
		private void EnsureAttributesAreBound()
		{
			if ((this._lazySourceAttributesBag == null || !this._lazySourceAttributesBag.IsSealed) && base.LoadAndValidateAttributes(OneOrMany.Create<SyntaxList<AttributeListSyntax>>(this.GetAttributeDeclarations()), ref this._lazySourceAttributesBag, AttributeLocation.None, false, null, null))
			{
				this._state.NotePartComplete(CompletionPart.Attributes);
			}
		}

		// Token: 0x060066CA RID: 26314 RVA: 0x00183DA8 File Offset: 0x00181FA8
		private CustomAttributesBag<CSharpAttributeData> GetSourceAttributesBag()
		{
			this.EnsureAttributesAreBound();
			return this._lazySourceAttributesBag;
		}

		// Token: 0x060066CB RID: 26315 RVA: 0x00183DB8 File Offset: 0x00181FB8
		public sealed override ImmutableArray<CSharpAttributeData> GetAttributes()
		{
			ImmutableArray<CSharpAttributeData> immutableArray = this.GetSourceAttributesBag().Attributes;
			ImmutableArray<CSharpAttributeData> attributes = this.GetNetModuleAttributesBag().Attributes;
			if (immutableArray.Length > 0)
			{
				if (attributes.Length > 0)
				{
					immutableArray = immutableArray.Concat<CSharpAttributeData>(attributes);
				}
			}
			else
			{
				immutableArray = attributes;
			}
			return immutableArray;
		}

		// Token: 0x060066CC RID: 26316 RVA: 0x00183DFE File Offset: 0x00181FFE
		internal bool IsIndexOfOmittedAssemblyAttribute(int index)
		{
			return this._lazyOmittedAttributeIndices != null && this._lazyOmittedAttributeIndices.Contains(index);
		}

		// Token: 0x060066CD RID: 26317 RVA: 0x00183E18 File Offset: 0x00182018
		internal CommonAssemblyWellKnownAttributeData<NamedTypeSymbol> GetSourceDecodedWellKnownAttributeData()
		{
			CustomAttributesBag<CSharpAttributeData> customAttributesBag = this._lazySourceAttributesBag;
			if (customAttributesBag == null || !customAttributesBag.IsDecodedWellKnownAttributeDataComputed)
			{
				customAttributesBag = this.GetSourceAttributesBag();
			}
			return (CommonAssemblyWellKnownAttributeData<NamedTypeSymbol>)customAttributesBag.DecodedWellKnownAttributeData;
		}

		// Token: 0x060066CE RID: 26318 RVA: 0x00183E4C File Offset: 0x0018204C
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 0 })]
		private CommonAssemblyWellKnownAttributeData<NamedTypeSymbol> GetSourceDecodedWellKnownAttributeData(QuickAttributes attribute)
		{
			CustomAttributesBag<CSharpAttributeData> customAttributesBag = this._lazySourceAttributesBag;
			if (customAttributesBag != null && customAttributesBag.IsDecodedWellKnownAttributeDataComputed)
			{
				return (CommonAssemblyWellKnownAttributeData<NamedTypeSymbol>)customAttributesBag.DecodedWellKnownAttributeData;
			}
			customAttributesBag = null;
			Func<AttributeSyntax, bool> func;
			if (attribute != QuickAttributes.AssemblyKeyName)
			{
				if (attribute != QuickAttributes.AssemblyKeyFile)
				{
					if (attribute != QuickAttributes.AssemblySignatureKey)
					{
						throw ExceptionUtilities.UnexpectedValue(attribute);
					}
					func = new Func<AttributeSyntax, bool>(this.<GetSourceDecodedWellKnownAttributeData>g__isPossibleAssemblySignatureKeyAttribute|112_0);
				}
				else
				{
					func = new Func<AttributeSyntax, bool>(this.<GetSourceDecodedWellKnownAttributeData>g__isPossibleAssemblyKeyFileAttribute|112_2);
				}
			}
			else
			{
				func = new Func<AttributeSyntax, bool>(this.<GetSourceDecodedWellKnownAttributeData>g__isPossibleAssemblyKeyNameAttribute|112_1);
			}
			Func<AttributeSyntax, bool> func2 = func;
			base.LoadAndValidateAttributes(OneOrMany.Create<SyntaxList<AttributeListSyntax>>(this.GetAttributeDeclarations()), ref customAttributesBag, AttributeLocation.None, false, null, func2);
			return (CommonAssemblyWellKnownAttributeData<NamedTypeSymbol>)((customAttributesBag != null) ? customAttributesBag.DecodedWellKnownAttributeData : null);
		}

		// Token: 0x060066CF RID: 26319 RVA: 0x00183EEC File Offset: 0x001820EC
		internal HashSet<NamedTypeSymbol> GetForwardedTypes()
		{
			CustomAttributesBag<CSharpAttributeData> customAttributesBag = this._lazySourceAttributesBag;
			if (customAttributesBag != null && customAttributesBag.IsDecodedWellKnownAttributeDataComputed)
			{
				CommonAssemblyWellKnownAttributeData<NamedTypeSymbol> commonAssemblyWellKnownAttributeData = (CommonAssemblyWellKnownAttributeData<NamedTypeSymbol>)customAttributesBag.DecodedWellKnownAttributeData;
				if (commonAssemblyWellKnownAttributeData == null)
				{
					return null;
				}
				return commonAssemblyWellKnownAttributeData.ForwardedTypes;
			}
			else
			{
				customAttributesBag = null;
				base.LoadAndValidateAttributes(OneOrMany.Create<SyntaxList<AttributeListSyntax>>(this.GetAttributeDeclarations()), ref customAttributesBag, AttributeLocation.None, false, null, new Func<AttributeSyntax, bool>(this.IsPossibleForwardedTypesAttribute));
				CommonAssemblyWellKnownAttributeData<NamedTypeSymbol> commonAssemblyWellKnownAttributeData2 = (CommonAssemblyWellKnownAttributeData<NamedTypeSymbol>)((customAttributesBag != null) ? customAttributesBag.DecodedWellKnownAttributeData : null);
				if (commonAssemblyWellKnownAttributeData2 == null)
				{
					return null;
				}
				return commonAssemblyWellKnownAttributeData2.ForwardedTypes;
			}
		}

		// Token: 0x060066D0 RID: 26320 RVA: 0x00183F63 File Offset: 0x00182163
		private bool IsPossibleForwardedTypesAttribute(AttributeSyntax node)
		{
			return this.DeclaringCompilation.GetBinderFactory(node.SyntaxTree, false).GetBinder(node, null, null).QuickAttributeChecker.IsPossibleMatch(node, QuickAttributes.TypeForwardedTo);
		}

		// Token: 0x060066D1 RID: 26321 RVA: 0x00183F8B File Offset: 0x0018218B
		private static IEnumerable<SecurityAttribute> GetSecurityAttributes(CustomAttributesBag<CSharpAttributeData> attributesBag)
		{
			CommonAssemblyWellKnownAttributeData<NamedTypeSymbol> commonAssemblyWellKnownAttributeData = (CommonAssemblyWellKnownAttributeData<NamedTypeSymbol>)attributesBag.DecodedWellKnownAttributeData;
			if (commonAssemblyWellKnownAttributeData != null)
			{
				SecurityWellKnownAttributeData securityInformation = commonAssemblyWellKnownAttributeData.SecurityInformation;
				if (securityInformation != null)
				{
					foreach (SecurityAttribute securityAttribute in securityInformation.GetSecurityAttributes<CSharpAttributeData>(attributesBag.Attributes))
					{
						yield return securityAttribute;
					}
					IEnumerator<SecurityAttribute> enumerator = null;
				}
			}
			yield break;
			yield break;
		}

		// Token: 0x060066D2 RID: 26322 RVA: 0x00183F9B File Offset: 0x0018219B
		internal IEnumerable<SecurityAttribute> GetSecurityAttributes()
		{
			foreach (SecurityAttribute securityAttribute in SourceAssemblySymbol.GetSecurityAttributes(this.GetSourceAttributesBag()))
			{
				yield return securityAttribute;
			}
			IEnumerator<SecurityAttribute> enumerator = null;
			foreach (SecurityAttribute securityAttribute2 in SourceAssemblySymbol.GetSecurityAttributes(this.GetNetModuleAttributesBag()))
			{
				yield return securityAttribute2;
			}
			enumerator = null;
			if (this._compilation.Options.AllowUnsafe && !(this._compilation.GetWellKnownType(WellKnownType.System_Security_UnverifiableCodeAttribute) is MissingMetadataTypeSymbol) && !(this._compilation.GetWellKnownType(WellKnownType.System_Security_Permissions_SecurityPermissionAttribute) is MissingMetadataTypeSymbol))
			{
				NamedTypeSymbol wellKnownType = this._compilation.GetWellKnownType(WellKnownType.System_Security_Permissions_SecurityAction);
				if (!(wellKnownType is MissingMetadataTypeSymbol))
				{
					FieldSymbol fieldSymbol = (FieldSymbol)this._compilation.GetWellKnownTypeMember(WellKnownMember.System_Security_Permissions_SecurityAction__RequestMinimum);
					object obj = ((fieldSymbol == null || fieldSymbol.HasUseSiteError) ? 0 : fieldSymbol.ConstantValue);
					TypedConstant typedConstant = new TypedConstant(wellKnownType, TypedConstantKind.Enum, obj);
					NamedTypeSymbol specialType = this._compilation.GetSpecialType(SpecialType.System_Boolean);
					TypedConstant typedConstant2 = new TypedConstant(specialType, TypedConstantKind.Primitive, true);
					SynthesizedAttributeData synthesizedAttributeData = this._compilation.TrySynthesizeAttribute(WellKnownMember.System_Security_Permissions_SecurityPermissionAttribute__ctor, ImmutableArray.Create<TypedConstant>(typedConstant), ImmutableArray.Create<KeyValuePair<WellKnownMember, TypedConstant>>(new KeyValuePair<WellKnownMember, TypedConstant>(WellKnownMember.System_Security_Permissions_SecurityPermissionAttribute__SkipVerification, typedConstant2)), false);
					if (synthesizedAttributeData != null)
					{
						yield return new SecurityAttribute((DeclarativeSecurityAction)((int)obj), synthesizedAttributeData);
					}
				}
			}
			yield break;
			yield break;
		}

		// Token: 0x060066D3 RID: 26323 RVA: 0x00183FAC File Offset: 0x001821AC
		internal override ImmutableArray<AssemblySymbol> GetNoPiaResolutionAssemblies()
		{
			return this._modules[0].GetReferencedAssemblySymbols();
		}

		// Token: 0x060066D4 RID: 26324 RVA: 0x00183FCD File Offset: 0x001821CD
		internal override void SetNoPiaResolutionAssemblies(ImmutableArray<AssemblySymbol> assemblies)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x060066D5 RID: 26325 RVA: 0x00183FD4 File Offset: 0x001821D4
		internal override ImmutableArray<AssemblySymbol> GetLinkedReferencedAssemblies()
		{
			return default(ImmutableArray<AssemblySymbol>);
		}

		// Token: 0x060066D6 RID: 26326 RVA: 0x00183FEA File Offset: 0x001821EA
		internal override void SetLinkedReferencedAssemblies(ImmutableArray<AssemblySymbol> assemblies)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x1700198F RID: 6543
		// (get) Token: 0x060066D7 RID: 26327 RVA: 0x00183FF1 File Offset: 0x001821F1
		internal override bool IsLinked
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001990 RID: 6544
		// (get) Token: 0x060066D8 RID: 26328 RVA: 0x00183FF4 File Offset: 0x001821F4
		internal bool DeclaresTheObjectClass
		{
			get
			{
				if (base.CorLibrary != this)
				{
					return false;
				}
				NamedTypeSymbol specialType = base.GetSpecialType(SpecialType.System_Object);
				return !specialType.IsErrorType() && specialType.DeclaredAccessibility == Accessibility.Public;
			}
		}

		// Token: 0x17001991 RID: 6545
		// (get) Token: 0x060066D9 RID: 26329 RVA: 0x00184027 File Offset: 0x00182227
		public override bool MightContainExtensionMethods
		{
			get
			{
				return !this._lazyContainsExtensionMethods.HasValue() || this._lazyContainsExtensionMethods.Value();
			}
		}

		// Token: 0x17001992 RID: 6546
		// (get) Token: 0x060066DA RID: 26330 RVA: 0x00184044 File Offset: 0x00182244
		private bool HasDebuggableAttribute
		{
			get
			{
				CommonAssemblyWellKnownAttributeData<NamedTypeSymbol> sourceDecodedWellKnownAttributeData = this.GetSourceDecodedWellKnownAttributeData();
				return sourceDecodedWellKnownAttributeData != null && sourceDecodedWellKnownAttributeData.HasDebuggableAttribute;
			}
		}

		// Token: 0x17001993 RID: 6547
		// (get) Token: 0x060066DB RID: 26331 RVA: 0x00184064 File Offset: 0x00182264
		private bool HasReferenceAssemblyAttribute
		{
			get
			{
				CommonAssemblyWellKnownAttributeData<NamedTypeSymbol> sourceDecodedWellKnownAttributeData = this.GetSourceDecodedWellKnownAttributeData();
				return sourceDecodedWellKnownAttributeData != null && sourceDecodedWellKnownAttributeData.HasReferenceAssemblyAttribute;
			}
		}

		// Token: 0x060066DC RID: 26332 RVA: 0x00184084 File Offset: 0x00182284
		internal override void AddSynthesizedAttributes(PEModuleBuilder moduleBuilder, ref ArrayBuilder<SynthesizedAttributeData> attributes)
		{
			base.AddSynthesizedAttributes(moduleBuilder, ref attributes);
			bool flag = this._compilation.Options.OutputKind.IsNetModule();
			if (this.ContainsExtensionMethods())
			{
				Symbol.AddSynthesizedAttribute(ref attributes, this._compilation.TrySynthesizeAttribute(WellKnownMember.System_Runtime_CompilerServices_ExtensionAttribute__ctor, default(ImmutableArray<TypedConstant>), default(ImmutableArray<KeyValuePair<WellKnownMember, TypedConstant>>), false));
			}
			bool flag2;
			if (!flag)
			{
				flag2 = !this.Modules.Any<ModuleSymbol>((ModuleSymbol m) => m.HasAssemblyCompilationRelaxationsAttribute);
			}
			else
			{
				flag2 = false;
			}
			if (flag2 && !(this._compilation.GetWellKnownType(WellKnownType.System_Runtime_CompilerServices_CompilationRelaxationsAttribute) is MissingMetadataTypeSymbol))
			{
				NamedTypeSymbol specialType = this._compilation.GetSpecialType(SpecialType.System_Int32);
				TypedConstant typedConstant = new TypedConstant(specialType, TypedConstantKind.Primitive, 8);
				Symbol.AddSynthesizedAttribute(ref attributes, this._compilation.TrySynthesizeAttribute(WellKnownMember.System_Runtime_CompilerServices_CompilationRelaxationsAttribute__ctorInt32, ImmutableArray.Create<TypedConstant>(typedConstant), default(ImmutableArray<KeyValuePair<WellKnownMember, TypedConstant>>), false));
			}
			bool flag3;
			if (!flag)
			{
				flag3 = !this.Modules.Any<ModuleSymbol>((ModuleSymbol m) => m.HasAssemblyRuntimeCompatibilityAttribute);
			}
			else
			{
				flag3 = false;
			}
			if (flag3 && !(this._compilation.GetWellKnownType(WellKnownType.System_Runtime_CompilerServices_RuntimeCompatibilityAttribute) is MissingMetadataTypeSymbol))
			{
				NamedTypeSymbol specialType2 = this._compilation.GetSpecialType(SpecialType.System_Boolean);
				TypedConstant typedConstant2 = new TypedConstant(specialType2, TypedConstantKind.Primitive, true);
				Symbol.AddSynthesizedAttribute(ref attributes, this._compilation.TrySynthesizeAttribute(WellKnownMember.System_Runtime_CompilerServices_RuntimeCompatibilityAttribute__ctor, ImmutableArray<TypedConstant>.Empty, ImmutableArray.Create<KeyValuePair<WellKnownMember, TypedConstant>>(new KeyValuePair<WellKnownMember, TypedConstant>(WellKnownMember.System_Runtime_CompilerServices_RuntimeCompatibilityAttribute__WrapNonExceptionThrows, typedConstant2)), false));
			}
			if (!flag && !this.HasDebuggableAttribute)
			{
				Symbol.AddSynthesizedAttribute(ref attributes, this._compilation.SynthesizeDebuggableAttribute());
			}
			if (this._compilation.Options.OutputKind == OutputKind.NetModule)
			{
				if (!string.IsNullOrEmpty(this._compilation.Options.CryptoKeyContainer) && this.AssemblyKeyContainerAttributeSetting == WellKnownAttributeData.StringMissingValue)
				{
					NamedTypeSymbol specialType3 = this._compilation.GetSpecialType(SpecialType.System_String);
					TypedConstant typedConstant3 = new TypedConstant(specialType3, TypedConstantKind.Primitive, this._compilation.Options.CryptoKeyContainer);
					Symbol.AddSynthesizedAttribute(ref attributes, this._compilation.TrySynthesizeAttribute(WellKnownMember.System_Reflection_AssemblyKeyNameAttribute__ctor, ImmutableArray.Create<TypedConstant>(typedConstant3), default(ImmutableArray<KeyValuePair<WellKnownMember, TypedConstant>>), false));
				}
				if (!string.IsNullOrEmpty(this._compilation.Options.CryptoKeyFile) && this.AssemblyKeyFileAttributeSetting == WellKnownAttributeData.StringMissingValue)
				{
					NamedTypeSymbol specialType4 = this._compilation.GetSpecialType(SpecialType.System_String);
					TypedConstant typedConstant4 = new TypedConstant(specialType4, TypedConstantKind.Primitive, this._compilation.Options.CryptoKeyFile);
					Symbol.AddSynthesizedAttribute(ref attributes, this._compilation.TrySynthesizeAttribute(WellKnownMember.System_Reflection_AssemblyKeyFileAttribute__ctor, ImmutableArray.Create<TypedConstant>(typedConstant4), default(ImmutableArray<KeyValuePair<WellKnownMember, TypedConstant>>), false));
				}
			}
		}

		// Token: 0x060066DD RID: 26333 RVA: 0x0018430C File Offset: 0x0018250C
		private bool ContainsExtensionMethods()
		{
			if (!this._lazyContainsExtensionMethods.HasValue())
			{
				this._lazyContainsExtensionMethods = SourceAssemblySymbol.ContainsExtensionMethods(this._modules).ToThreeState();
			}
			return this._lazyContainsExtensionMethods.Value();
		}

		// Token: 0x060066DE RID: 26334 RVA: 0x0018433C File Offset: 0x0018253C
		private static bool ContainsExtensionMethods(ImmutableArray<ModuleSymbol> modules)
		{
			ImmutableArray<ModuleSymbol>.Enumerator enumerator = modules.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (SourceAssemblySymbol.ContainsExtensionMethods(enumerator.Current.GlobalNamespace))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060066DF RID: 26335 RVA: 0x00184374 File Offset: 0x00182574
		private static bool ContainsExtensionMethods(NamespaceSymbol ns)
		{
			foreach (Symbol symbol in ns.GetMembersUnordered())
			{
				SymbolKind kind = symbol.Kind;
				if (kind != SymbolKind.NamedType)
				{
					if (kind != SymbolKind.Namespace)
					{
						throw ExceptionUtilities.UnexpectedValue(symbol.Kind);
					}
					if (SourceAssemblySymbol.ContainsExtensionMethods((NamespaceSymbol)symbol))
					{
						return true;
					}
				}
				else if (((NamedTypeSymbol)symbol).MightContainExtensionMethods)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060066E0 RID: 26336 RVA: 0x001843E4 File Offset: 0x001825E4
		private void CheckOptimisticIVTAccessGrants(BindingDiagnosticBag bag)
		{
			ConcurrentDictionary<AssemblySymbol, bool> optimisticallyGrantedInternalsAccess = this._optimisticallyGrantedInternalsAccess;
			if (optimisticallyGrantedInternalsAccess != null)
			{
				foreach (AssemblySymbol assemblySymbol in optimisticallyGrantedInternalsAccess.Keys)
				{
					IVTConclusion ivtconclusion = base.MakeFinalIVTDetermination(assemblySymbol);
					if (ivtconclusion == IVTConclusion.PublicKeyDoesntMatch)
					{
						bag.Add(ErrorCode.ERR_FriendRefNotEqualToThis, NoLocation.Singleton, new object[] { assemblySymbol.Identity, this.Identity });
					}
					else if (ivtconclusion == IVTConclusion.OneSignedOneNot)
					{
						bag.Add(ErrorCode.ERR_FriendRefSigningMismatch, NoLocation.Singleton, new object[] { assemblySymbol.Identity });
					}
				}
			}
		}

		// Token: 0x060066E1 RID: 26337 RVA: 0x00184494 File Offset: 0x00182694
		internal override IEnumerable<ImmutableArray<byte>> GetInternalsVisibleToPublicKeys(string simpleName)
		{
			this.EnsureAttributesAreBound();
			if (this._lazyInternalsVisibleToMap == null)
			{
				return SpecializedCollections.EmptyEnumerable<ImmutableArray<byte>>();
			}
			ConcurrentDictionary<ImmutableArray<byte>, Tuple<Location, string>> concurrentDictionary = null;
			this._lazyInternalsVisibleToMap.TryGetValue(simpleName, ref concurrentDictionary);
			if (concurrentDictionary == null)
			{
				return SpecializedCollections.EmptyEnumerable<ImmutableArray<byte>>();
			}
			return concurrentDictionary.Keys;
		}

		// Token: 0x060066E2 RID: 26338 RVA: 0x001844D8 File Offset: 0x001826D8
		internal override bool AreInternalsVisibleToThisAssembly(AssemblySymbol potentialGiverOfAccess)
		{
			if (this._lazyStrongNameKeys != null || SourceAssemblySymbol.t_assemblyForWhichCurrentThreadIsComputingKeys == null)
			{
				IVTConclusion ivtconclusion = base.MakeFinalIVTDetermination(potentialGiverOfAccess);
				return ivtconclusion == IVTConclusion.Match || ivtconclusion == IVTConclusion.OneSignedOneNot;
			}
			if (!potentialGiverOfAccess.GetInternalsVisibleToPublicKeys(this.Name).IsEmpty<ImmutableArray<byte>>())
			{
				if (this._optimisticallyGrantedInternalsAccess == null)
				{
					Interlocked.CompareExchange<ConcurrentDictionary<AssemblySymbol, bool>>(ref this._optimisticallyGrantedInternalsAccess, new ConcurrentDictionary<AssemblySymbol, bool>(), null);
				}
				this._optimisticallyGrantedInternalsAccess.TryAdd(potentialGiverOfAccess, true);
				return true;
			}
			return false;
		}

		// Token: 0x060066E3 RID: 26339 RVA: 0x00184544 File Offset: 0x00182744
		private AssemblyIdentity ComputeIdentity()
		{
			return new AssemblyIdentity(this._assemblySimpleName, VersionHelper.GenerateVersionFromPatternAndCurrentTime(this._compilation.Options.CurrentLocalTime, this.AssemblyVersionAttributeSetting), this.AssemblyCultureAttributeSetting, this.StrongNameKeys.PublicKey, !this.StrongNameKeys.PublicKey.IsDefault);
		}

		// Token: 0x060066E4 RID: 26340 RVA: 0x0018459E File Offset: 0x0018279E
		private static Location GetAssemblyAttributeLocationForDiagnostic(AttributeSyntax attributeSyntaxOpt)
		{
			if (attributeSyntaxOpt == null)
			{
				return NoLocation.Singleton;
			}
			return attributeSyntaxOpt.Location;
		}

		// Token: 0x060066E5 RID: 26341 RVA: 0x001845B0 File Offset: 0x001827B0
		private void DecodeTypeForwardedToAttribute(ref DecodeWellKnownAttributeArguments<AttributeSyntax, CSharpAttributeData, AttributeLocation> arguments)
		{
			BindingDiagnosticBag bindingDiagnosticBag = (BindingDiagnosticBag)arguments.Diagnostics;
			TypeSymbol typeSymbol = (TypeSymbol)arguments.Attribute.CommonConstructorArguments[0].ValueInternal;
			if (typeSymbol == null)
			{
				bindingDiagnosticBag.Add(ErrorCode.ERR_InvalidFwdType, SourceAssemblySymbol.GetAssemblyAttributeLocationForDiagnostic(arguments.AttributeSyntaxOpt));
				return;
			}
			UseSiteInfo<AssemblySymbol> useSiteInfo = typeSymbol.GetUseSiteInfo();
			DiagnosticInfo diagnosticInfo = useSiteInfo.DiagnosticInfo;
			if ((diagnosticInfo == null || diagnosticInfo.Code != 7003) && bindingDiagnosticBag.Add(useSiteInfo, (useSiteInfo.DiagnosticInfo != null) ? SourceAssemblySymbol.GetAssemblyAttributeLocationForDiagnostic(arguments.AttributeSyntaxOpt) : Location.None))
			{
				return;
			}
			if (typeSymbol.ContainingAssembly == this)
			{
				bindingDiagnosticBag.Add(ErrorCode.ERR_ForwardedTypeInThisAssembly, SourceAssemblySymbol.GetAssemblyAttributeLocationForDiagnostic(arguments.AttributeSyntaxOpt), new object[] { typeSymbol });
				return;
			}
			if (typeSymbol.ContainingType != null)
			{
				bindingDiagnosticBag.Add(ErrorCode.ERR_ForwardedTypeIsNested, SourceAssemblySymbol.GetAssemblyAttributeLocationForDiagnostic(arguments.AttributeSyntaxOpt), new object[] { typeSymbol, typeSymbol.ContainingType });
				return;
			}
			if (typeSymbol.Kind != SymbolKind.NamedType)
			{
				bindingDiagnosticBag.Add(ErrorCode.ERR_InvalidFwdType, SourceAssemblySymbol.GetAssemblyAttributeLocationForDiagnostic(arguments.AttributeSyntaxOpt));
				return;
			}
			CommonAssemblyWellKnownAttributeData<NamedTypeSymbol> orCreateData = arguments.GetOrCreateData<CommonAssemblyWellKnownAttributeData<NamedTypeSymbol>>();
			HashSet<NamedTypeSymbol> hashSet = orCreateData.ForwardedTypes;
			if (hashSet == null)
			{
				HashSet<NamedTypeSymbol> hashSet2 = new HashSet<NamedTypeSymbol>();
				hashSet2.Add((NamedTypeSymbol)typeSymbol);
				hashSet = hashSet2;
				orCreateData.ForwardedTypes = hashSet;
				return;
			}
			if (!hashSet.Add((NamedTypeSymbol)typeSymbol))
			{
				bindingDiagnosticBag.Add(ErrorCode.ERR_DuplicateTypeForwarder, SourceAssemblySymbol.GetAssemblyAttributeLocationForDiagnostic(arguments.AttributeSyntaxOpt), new object[] { typeSymbol });
			}
		}

		// Token: 0x060066E6 RID: 26342 RVA: 0x00184738 File Offset: 0x00182938
		private void DecodeOneInternalsVisibleToAttribute(AttributeSyntax nodeOpt, CSharpAttributeData attrData, BindingDiagnosticBag diagnostics, int index, ref ConcurrentDictionary<string, ConcurrentDictionary<ImmutableArray<byte>, Tuple<Location, string>>> lazyInternalsVisibleToMap)
		{
			string text = (string)attrData.CommonConstructorArguments[0].ValueInternal;
			if (text == null)
			{
				diagnostics.Add(ErrorCode.ERR_CannotPassNullForFriendAssembly, SourceAssemblySymbol.GetAssemblyAttributeLocationForDiagnostic(nodeOpt));
				return;
			}
			AssemblyIdentity assemblyIdentity;
			AssemblyIdentityParts assemblyIdentityParts;
			if (!AssemblyIdentity.TryParseDisplayName(text, out assemblyIdentity, out assemblyIdentityParts))
			{
				diagnostics.Add(ErrorCode.WRN_InvalidAssemblyName, SourceAssemblySymbol.GetAssemblyAttributeLocationForDiagnostic(nodeOpt), new object[] { text });
				this.AddOmittedAttributeIndex(index);
				return;
			}
			if ((assemblyIdentityParts & ~(AssemblyIdentityParts.Name | AssemblyIdentityParts.PublicKey | AssemblyIdentityParts.PublicKeyToken)) != (AssemblyIdentityParts)0)
			{
				diagnostics.Add(ErrorCode.ERR_FriendAssemblyBadArgs, SourceAssemblySymbol.GetAssemblyAttributeLocationForDiagnostic(nodeOpt), new object[] { text });
				return;
			}
			if (lazyInternalsVisibleToMap == null)
			{
				Interlocked.CompareExchange<ConcurrentDictionary<string, ConcurrentDictionary<ImmutableArray<byte>, Tuple<Location, string>>>>(ref lazyInternalsVisibleToMap, new ConcurrentDictionary<string, ConcurrentDictionary<ImmutableArray<byte>, Tuple<Location, string>>>(StringComparer.OrdinalIgnoreCase), null);
			}
			Tuple<Location, string> tuple = null;
			if (assemblyIdentity.PublicKey.IsEmpty)
			{
				tuple = new Tuple<Location, string>(SourceAssemblySymbol.GetAssemblyAttributeLocationForDiagnostic(nodeOpt), text);
			}
			ConcurrentDictionary<ImmutableArray<byte>, Tuple<Location, string>> concurrentDictionary = null;
			if (lazyInternalsVisibleToMap.TryGetValue(assemblyIdentity.Name, ref concurrentDictionary))
			{
				concurrentDictionary.TryAdd(assemblyIdentity.PublicKey, tuple);
				return;
			}
			concurrentDictionary = new ConcurrentDictionary<ImmutableArray<byte>, Tuple<Location, string>>();
			concurrentDictionary.TryAdd(assemblyIdentity.PublicKey, tuple);
			lazyInternalsVisibleToMap.TryAdd(assemblyIdentity.Name, concurrentDictionary);
		}

		// Token: 0x17001994 RID: 6548
		// (get) Token: 0x060066E7 RID: 26343 RVA: 0x00184855 File Offset: 0x00182A55
		IAttributeTargetSymbol IAttributeTargetSymbol.AttributesOwner
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17001995 RID: 6549
		// (get) Token: 0x060066E8 RID: 26344 RVA: 0x00184858 File Offset: 0x00182A58
		AttributeLocation IAttributeTargetSymbol.DefaultAttributeLocation
		{
			get
			{
				return AttributeLocation.Assembly;
			}
		}

		// Token: 0x17001996 RID: 6550
		// (get) Token: 0x060066E9 RID: 26345 RVA: 0x0018485B File Offset: 0x00182A5B
		AttributeLocation IAttributeTargetSymbol.AllowedAttributeLocations
		{
			get
			{
				if (!this.IsInteractive)
				{
					return AttributeLocation.Assembly | AttributeLocation.Module;
				}
				return AttributeLocation.None;
			}
		}

		// Token: 0x060066EA RID: 26346 RVA: 0x00184868 File Offset: 0x00182A68
		internal override void DecodeWellKnownAttribute(ref DecodeWellKnownAttributeArguments<AttributeSyntax, CSharpAttributeData, AttributeLocation> arguments)
		{
			this.DecodeWellKnownAttribute(ref arguments, arguments.Index, false);
		}

		// Token: 0x060066EB RID: 26347 RVA: 0x00184878 File Offset: 0x00182A78
		private void DecodeWellKnownAttribute(ref DecodeWellKnownAttributeArguments<AttributeSyntax, CSharpAttributeData, AttributeLocation> arguments, int index, bool isFromNetModule)
		{
			CSharpAttributeData attribute = arguments.Attribute;
			BindingDiagnosticBag bindingDiagnosticBag = (BindingDiagnosticBag)arguments.Diagnostics;
			if (attribute.IsTargetAttribute(this, AttributeDescription.InternalsVisibleToAttribute))
			{
				this.DecodeOneInternalsVisibleToAttribute(arguments.AttributeSyntaxOpt, attribute, bindingDiagnosticBag, index, ref this._lazyInternalsVisibleToMap);
				return;
			}
			if (attribute.IsTargetAttribute(this, AttributeDescription.AssemblySignatureKeyAttribute))
			{
				string text = (string)attribute.CommonConstructorArguments[0].ValueInternal;
				arguments.GetOrCreateData<CommonAssemblyWellKnownAttributeData<NamedTypeSymbol>>().AssemblySignatureKeyAttributeSetting = text;
				if (!StrongNameKeys.IsValidPublicKeyString(text))
				{
					bindingDiagnosticBag.Add(ErrorCode.ERR_InvalidSignaturePublicKey, attribute.GetAttributeArgumentSyntaxLocation(0, arguments.AttributeSyntaxOpt));
					return;
				}
			}
			else
			{
				if (attribute.IsTargetAttribute(this, AttributeDescription.AssemblyKeyFileAttribute))
				{
					arguments.GetOrCreateData<CommonAssemblyWellKnownAttributeData<NamedTypeSymbol>>().AssemblyKeyFileAttributeSetting = (string)attribute.CommonConstructorArguments[0].ValueInternal;
					return;
				}
				if (attribute.IsTargetAttribute(this, AttributeDescription.AssemblyKeyNameAttribute))
				{
					arguments.GetOrCreateData<CommonAssemblyWellKnownAttributeData<NamedTypeSymbol>>().AssemblyKeyContainerAttributeSetting = (string)attribute.CommonConstructorArguments[0].ValueInternal;
					return;
				}
				if (attribute.IsTargetAttribute(this, AttributeDescription.AssemblyDelaySignAttribute))
				{
					arguments.GetOrCreateData<CommonAssemblyWellKnownAttributeData<NamedTypeSymbol>>().AssemblyDelaySignAttributeSetting = (((bool)attribute.CommonConstructorArguments[0].ValueInternal) ? ThreeState.True : ThreeState.False);
					return;
				}
				if (attribute.IsTargetAttribute(this, AttributeDescription.AssemblyVersionAttribute))
				{
					string text2 = (string)attribute.CommonConstructorArguments[0].ValueInternal;
					Version version;
					if (!VersionHelper.TryParseAssemblyVersion(text2, !this._compilation.IsEmitDeterministic, out version))
					{
						Location attributeArgumentSyntaxLocation = attribute.GetAttributeArgumentSyntaxLocation(0, arguments.AttributeSyntaxOpt);
						bool flag = this._compilation.IsEmitDeterministic && text2 != null && Enumerable.Contains<char>(text2, '*');
						bindingDiagnosticBag.Add(flag ? ErrorCode.ERR_InvalidVersionFormatDeterministic : ErrorCode.ERR_InvalidVersionFormat, attributeArgumentSyntaxLocation);
					}
					arguments.GetOrCreateData<CommonAssemblyWellKnownAttributeData<NamedTypeSymbol>>().AssemblyVersionAttributeSetting = version;
					return;
				}
				if (attribute.IsTargetAttribute(this, AttributeDescription.AssemblyFileVersionAttribute))
				{
					string text3 = (string)attribute.CommonConstructorArguments[0].ValueInternal;
					Version version2;
					if (!VersionHelper.TryParse(text3, out version2))
					{
						Location attributeArgumentSyntaxLocation2 = attribute.GetAttributeArgumentSyntaxLocation(0, arguments.AttributeSyntaxOpt);
						bindingDiagnosticBag.Add(ErrorCode.WRN_InvalidVersionFormat, attributeArgumentSyntaxLocation2);
					}
					arguments.GetOrCreateData<CommonAssemblyWellKnownAttributeData<NamedTypeSymbol>>().AssemblyFileVersionAttributeSetting = text3;
					return;
				}
				if (attribute.IsTargetAttribute(this, AttributeDescription.AssemblyTitleAttribute))
				{
					arguments.GetOrCreateData<CommonAssemblyWellKnownAttributeData<NamedTypeSymbol>>().AssemblyTitleAttributeSetting = (string)attribute.CommonConstructorArguments[0].ValueInternal;
					return;
				}
				if (attribute.IsTargetAttribute(this, AttributeDescription.AssemblyDescriptionAttribute))
				{
					arguments.GetOrCreateData<CommonAssemblyWellKnownAttributeData<NamedTypeSymbol>>().AssemblyDescriptionAttributeSetting = (string)attribute.CommonConstructorArguments[0].ValueInternal;
					return;
				}
				if (attribute.IsTargetAttribute(this, AttributeDescription.AssemblyCultureAttribute))
				{
					string text4 = (string)attribute.CommonConstructorArguments[0].ValueInternal;
					if (!string.IsNullOrEmpty(text4))
					{
						if (this._compilation.Options.OutputKind.IsApplication())
						{
							bindingDiagnosticBag.Add(ErrorCode.ERR_InvalidAssemblyCultureForExe, attribute.GetAttributeArgumentSyntaxLocation(0, arguments.AttributeSyntaxOpt));
						}
						else if (!AssemblyIdentity.IsValidCultureName(text4))
						{
							bindingDiagnosticBag.Add(ErrorCode.ERR_InvalidAssemblyCulture, attribute.GetAttributeArgumentSyntaxLocation(0, arguments.AttributeSyntaxOpt));
							text4 = null;
						}
					}
					arguments.GetOrCreateData<CommonAssemblyWellKnownAttributeData<NamedTypeSymbol>>().AssemblyCultureAttributeSetting = text4;
					return;
				}
				if (attribute.IsTargetAttribute(this, AttributeDescription.AssemblyCompanyAttribute))
				{
					arguments.GetOrCreateData<CommonAssemblyWellKnownAttributeData<NamedTypeSymbol>>().AssemblyCompanyAttributeSetting = (string)attribute.CommonConstructorArguments[0].ValueInternal;
					return;
				}
				if (attribute.IsTargetAttribute(this, AttributeDescription.AssemblyProductAttribute))
				{
					arguments.GetOrCreateData<CommonAssemblyWellKnownAttributeData<NamedTypeSymbol>>().AssemblyProductAttributeSetting = (string)attribute.CommonConstructorArguments[0].ValueInternal;
					return;
				}
				if (attribute.IsTargetAttribute(this, AttributeDescription.AssemblyInformationalVersionAttribute))
				{
					arguments.GetOrCreateData<CommonAssemblyWellKnownAttributeData<NamedTypeSymbol>>().AssemblyInformationalVersionAttributeSetting = (string)attribute.CommonConstructorArguments[0].ValueInternal;
					return;
				}
				if (attribute.IsTargetAttribute(this, AttributeDescription.SatelliteContractVersionAttribute))
				{
					Version version3;
					if (!VersionHelper.TryParseAssemblyVersion((string)attribute.CommonConstructorArguments[0].ValueInternal, false, out version3))
					{
						Location attributeArgumentSyntaxLocation3 = attribute.GetAttributeArgumentSyntaxLocation(0, arguments.AttributeSyntaxOpt);
						bindingDiagnosticBag.Add(ErrorCode.ERR_InvalidVersionFormat2, attributeArgumentSyntaxLocation3);
						return;
					}
				}
				else
				{
					if (attribute.IsTargetAttribute(this, AttributeDescription.AssemblyCopyrightAttribute))
					{
						arguments.GetOrCreateData<CommonAssemblyWellKnownAttributeData<NamedTypeSymbol>>().AssemblyCopyrightAttributeSetting = (string)attribute.CommonConstructorArguments[0].ValueInternal;
						return;
					}
					if (attribute.IsTargetAttribute(this, AttributeDescription.AssemblyTrademarkAttribute))
					{
						arguments.GetOrCreateData<CommonAssemblyWellKnownAttributeData<NamedTypeSymbol>>().AssemblyTrademarkAttributeSetting = (string)attribute.CommonConstructorArguments[0].ValueInternal;
						return;
					}
					int num;
					if ((num = attribute.GetTargetAttributeSignatureIndex(this, AttributeDescription.AssemblyFlagsAttribute)) != -1)
					{
						object valueInternal = attribute.CommonConstructorArguments[0].ValueInternal;
						AssemblyFlags assemblyFlags;
						if (num == 0 || num == 1)
						{
							assemblyFlags = (AssemblyNameFlags)valueInternal;
						}
						else
						{
							assemblyFlags = (AssemblyFlags)((uint)valueInternal);
						}
						arguments.GetOrCreateData<CommonAssemblyWellKnownAttributeData<NamedTypeSymbol>>().AssemblyFlagsAttributeSetting = assemblyFlags;
						return;
					}
					if (attribute.IsSecurityAttribute(this._compilation))
					{
						attribute.DecodeSecurityAttribute<CommonAssemblyWellKnownAttributeData<NamedTypeSymbol>>(this, this._compilation, ref arguments);
						return;
					}
					if (attribute.IsTargetAttribute(this, AttributeDescription.ClassInterfaceAttribute))
					{
						attribute.DecodeClassInterfaceAttribute(arguments.AttributeSyntaxOpt, bindingDiagnosticBag);
						return;
					}
					if (attribute.IsTargetAttribute(this, AttributeDescription.TypeLibVersionAttribute))
					{
						SourceAssemblySymbol.ValidateIntegralAttributeNonNegativeArguments(attribute, arguments.AttributeSyntaxOpt, bindingDiagnosticBag);
						return;
					}
					if (attribute.IsTargetAttribute(this, AttributeDescription.ComCompatibleVersionAttribute))
					{
						SourceAssemblySymbol.ValidateIntegralAttributeNonNegativeArguments(attribute, arguments.AttributeSyntaxOpt, bindingDiagnosticBag);
						return;
					}
					if (attribute.IsTargetAttribute(this, AttributeDescription.GuidAttribute))
					{
						attribute.DecodeGuidAttribute(arguments.AttributeSyntaxOpt, bindingDiagnosticBag);
						return;
					}
					if (attribute.IsTargetAttribute(this, AttributeDescription.CompilationRelaxationsAttribute))
					{
						arguments.GetOrCreateData<CommonAssemblyWellKnownAttributeData<NamedTypeSymbol>>().HasCompilationRelaxationsAttribute = true;
						return;
					}
					if (attribute.IsTargetAttribute(this, AttributeDescription.ReferenceAssemblyAttribute))
					{
						arguments.GetOrCreateData<CommonAssemblyWellKnownAttributeData<NamedTypeSymbol>>().HasReferenceAssemblyAttribute = true;
						return;
					}
					if (attribute.IsTargetAttribute(this, AttributeDescription.RuntimeCompatibilityAttribute))
					{
						bool flag2 = true;
						foreach (KeyValuePair<string, TypedConstant> keyValuePair in attribute.CommonNamedArguments)
						{
							if (keyValuePair.Key == "WrapNonExceptionThrows")
							{
								flag2 = keyValuePair.Value.DecodeValue<bool>(SpecialType.System_Boolean);
							}
						}
						arguments.GetOrCreateData<CommonAssemblyWellKnownAttributeData<NamedTypeSymbol>>().RuntimeCompatibilityWrapNonExceptionThrows = flag2;
						return;
					}
					if (attribute.IsTargetAttribute(this, AttributeDescription.DebuggableAttribute))
					{
						arguments.GetOrCreateData<CommonAssemblyWellKnownAttributeData<NamedTypeSymbol>>().HasDebuggableAttribute = true;
						return;
					}
					if (!isFromNetModule && attribute.IsTargetAttribute(this, AttributeDescription.TypeForwardedToAttribute))
					{
						this.DecodeTypeForwardedToAttribute(ref arguments);
						return;
					}
					if (attribute.IsTargetAttribute(this, AttributeDescription.CaseSensitiveExtensionAttribute))
					{
						if (arguments.AttributeSyntaxOpt != null)
						{
							bindingDiagnosticBag.Add(ErrorCode.ERR_ExplicitExtension, arguments.AttributeSyntaxOpt.Location);
							return;
						}
					}
					else if ((num = attribute.GetTargetAttributeSignatureIndex(this, AttributeDescription.AssemblyAlgorithmIdAttribute)) != -1)
					{
						object valueInternal2 = attribute.CommonConstructorArguments[0].ValueInternal;
						AssemblyHashAlgorithm assemblyHashAlgorithm;
						if (num == 0)
						{
							assemblyHashAlgorithm = (AssemblyHashAlgorithm)valueInternal2;
						}
						else
						{
							assemblyHashAlgorithm = (AssemblyHashAlgorithm)((uint)valueInternal2);
						}
						arguments.GetOrCreateData<CommonAssemblyWellKnownAttributeData<NamedTypeSymbol>>().AssemblyAlgorithmIdAttributeSetting = new AssemblyHashAlgorithm?(assemblyHashAlgorithm);
					}
				}
			}
		}

		// Token: 0x060066EC RID: 26348 RVA: 0x00184F88 File Offset: 0x00183188
		private static void ValidateIntegralAttributeNonNegativeArguments(CSharpAttributeData attribute, AttributeSyntax nodeOpt, BindingDiagnosticBag diagnostics)
		{
			int length = attribute.CommonConstructorArguments.Length;
			for (int i = 0; i < length; i++)
			{
				if (attribute.GetConstructorArgument<int>(i, SpecialType.System_Int32) < 0)
				{
					Location attributeArgumentSyntaxLocation = attribute.GetAttributeArgumentSyntaxLocation(i, nodeOpt);
					diagnostics.Add(ErrorCode.ERR_InvalidAttributeArgument, attributeArgumentSyntaxLocation, new object[] { (nodeOpt != null) ? nodeOpt.GetErrorDisplayName() : "" });
				}
			}
		}

		// Token: 0x060066ED RID: 26349 RVA: 0x00184FEC File Offset: 0x001831EC
		internal void NoteFieldAccess(FieldSymbol field, bool read, bool write)
		{
			SourceMemberContainerTypeSymbol sourceMemberContainerTypeSymbol = field.ContainingType as SourceMemberContainerTypeSymbol;
			if (sourceMemberContainerTypeSymbol == null)
			{
				return;
			}
			sourceMemberContainerTypeSymbol.EnsureFieldDefinitionsNoted();
			if (this._unusedFieldWarnings.IsDefault)
			{
				if (read)
				{
					this._unreadFields.Remove(field);
				}
				if (write)
				{
					bool flag;
					this._unassignedFieldsMap.TryRemove(field, ref flag);
				}
			}
		}

		// Token: 0x060066EE RID: 26350 RVA: 0x0018503E File Offset: 0x0018323E
		internal void NoteFieldDefinition(FieldSymbol field, bool isInternal, bool isUnread)
		{
			this._unassignedFieldsMap.TryAdd(field, isInternal);
			if (isUnread)
			{
				this._unreadFields.Add(field);
			}
		}

		// Token: 0x060066EF RID: 26351 RVA: 0x0018505E File Offset: 0x0018325E
		internal override bool IsNetModule()
		{
			return this._compilation.Options.OutputKind.IsNetModule();
		}

		// Token: 0x060066F0 RID: 26352 RVA: 0x00185078 File Offset: 0x00183278
		internal ImmutableArray<Diagnostic> GetUnusedFieldWarnings(CancellationToken cancellationToken)
		{
			if (this._unusedFieldWarnings.IsDefault)
			{
				this.ForceComplete(null, cancellationToken);
				DiagnosticBag instance = DiagnosticBag.GetInstance();
				bool flag = this.InternalsAreVisible || this.IsNetModule();
				HashSet<FieldSymbol> hashSet = null;
				foreach (FieldSymbol fieldSymbol in this._unassignedFieldsMap.Keys)
				{
					bool flag2;
					this._unassignedFieldsMap.TryGetValue(fieldSymbol, ref flag2);
					if ((!flag2 || !flag) && fieldSymbol.CanBeReferencedByName)
					{
						SourceNamedTypeSymbol sourceNamedTypeSymbol = fieldSymbol.ContainingType as SourceNamedTypeSymbol;
						if (sourceNamedTypeSymbol != null && !(fieldSymbol is TupleErrorFieldSymbol))
						{
							bool flag3 = this._unreadFields.Contains(fieldSymbol);
							if (flag3)
							{
								if (hashSet == null)
								{
									hashSet = new HashSet<FieldSymbol>();
								}
								hashSet.Add(fieldSymbol);
							}
							if (!sourceNamedTypeSymbol.HasStructLayoutAttribute)
							{
								Symbol associatedSymbol = fieldSymbol.AssociatedSymbol;
								if (associatedSymbol != null && associatedSymbol.Kind == SymbolKind.Event)
								{
									if (flag3)
									{
										instance.Add(ErrorCode.WRN_UnreferencedEvent, associatedSymbol.Locations.FirstOrNone(), new object[] { associatedSymbol });
									}
								}
								else if (flag3)
								{
									instance.Add(ErrorCode.WRN_UnreferencedField, fieldSymbol.Locations.FirstOrNone(), new object[] { fieldSymbol });
								}
								else
								{
									instance.Add(ErrorCode.WRN_UnassignedInternalField, fieldSymbol.Locations.FirstOrNone(), new object[]
									{
										fieldSymbol,
										SourceAssemblySymbol.DefaultValue(fieldSymbol.Type)
									});
								}
							}
						}
					}
				}
				foreach (FieldSymbol fieldSymbol2 in this._unreadFields)
				{
					if ((hashSet == null || !hashSet.Contains(fieldSymbol2)) && fieldSymbol2.CanBeReferencedByName)
					{
						SourceNamedTypeSymbol sourceNamedTypeSymbol2 = fieldSymbol2.ContainingType as SourceNamedTypeSymbol;
						if (sourceNamedTypeSymbol2 != null && !sourceNamedTypeSymbol2.HasStructLayoutAttribute)
						{
							instance.Add(ErrorCode.WRN_UnreferencedFieldAssg, fieldSymbol2.Locations.FirstOrNone(), new object[] { fieldSymbol2 });
						}
					}
				}
				ImmutableInterlocked.InterlockedInitialize<Diagnostic>(ref this._unusedFieldWarnings, instance.ToReadOnlyAndFree());
			}
			return this._unusedFieldWarnings;
		}

		// Token: 0x060066F1 RID: 26353 RVA: 0x001852A4 File Offset: 0x001834A4
		private static string DefaultValue(TypeSymbol type)
		{
			if (type.IsReferenceType)
			{
				return "null";
			}
			SpecialType specialType = type.SpecialType;
			if (specialType == SpecialType.System_Boolean)
			{
				return "false";
			}
			if (specialType - SpecialType.System_SByte > 10)
			{
				return "";
			}
			return "0";
		}

		// Token: 0x060066F2 RID: 26354 RVA: 0x001852E8 File Offset: 0x001834E8
		internal override NamedTypeSymbol TryLookupForwardedMetadataTypeWithCycleDetection(ref MetadataTypeName emittedName, ConsList<AssemblySymbol> visitedAssemblies)
		{
			int num = emittedName.ForcedArity;
			if (emittedName.UseCLSCompliantNameArityEncoding)
			{
				if (num == -1)
				{
					num = emittedName.InferredArity;
				}
				else if (num != emittedName.InferredArity)
				{
					return null;
				}
			}
			if (this._lazyForwardedTypesFromSource == null)
			{
				HashSet<NamedTypeSymbol> forwardedTypes = this.GetForwardedTypes();
				IDictionary<string, NamedTypeSymbol> dictionary;
				if (forwardedTypes != null)
				{
					dictionary = new Dictionary<string, NamedTypeSymbol>(StringOrdinalComparer.Instance);
					using (HashSet<NamedTypeSymbol>.Enumerator enumerator = forwardedTypes.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							NamedTypeSymbol namedTypeSymbol = enumerator.Current;
							NamedTypeSymbol originalDefinition = namedTypeSymbol.OriginalDefinition;
							string text = MetadataHelpers.BuildQualifiedName(originalDefinition.ContainingSymbol.ToDisplayString(SymbolDisplayFormat.QualifiedNameOnlyFormat), originalDefinition.MetadataName);
							dictionary[text] = originalDefinition;
						}
						goto IL_00A4;
					}
				}
				dictionary = SpecializedCollections.EmptyDictionary<string, NamedTypeSymbol>();
				IL_00A4:
				this._lazyForwardedTypesFromSource = dictionary;
			}
			NamedTypeSymbol namedTypeSymbol2;
			if (this._lazyForwardedTypesFromSource.TryGetValue(emittedName.FullName, ref namedTypeSymbol2))
			{
				if ((num == -1 || namedTypeSymbol2.Arity == num) && (!emittedName.UseCLSCompliantNameArityEncoding || namedTypeSymbol2.Arity == 0 || namedTypeSymbol2.MangleName))
				{
					return namedTypeSymbol2;
				}
			}
			else if (!this._compilation.Options.OutputKind.IsNetModule())
			{
				int i = this._modules.Length - 1;
				while (i > 0)
				{
					PEModuleSymbol pemoduleSymbol = (PEModuleSymbol)this._modules[i];
					ValueTuple<AssemblySymbol, AssemblySymbol> assembliesForForwardedType = pemoduleSymbol.GetAssembliesForForwardedType(ref emittedName);
					AssemblySymbol item = assembliesForForwardedType.Item1;
					AssemblySymbol item2 = assembliesForForwardedType.Item2;
					if (item != null)
					{
						if (item2 != null)
						{
							return base.CreateMultipleForwardingErrorTypeSymbol(ref emittedName, pemoduleSymbol, item, item2);
						}
						if (visitedAssemblies != null && Enumerable.Contains<AssemblySymbol>(visitedAssemblies, item))
						{
							return base.CreateCycleInTypeForwarderErrorTypeSymbol(ref emittedName);
						}
						visitedAssemblies = new ConsList<AssemblySymbol>(this, visitedAssemblies ?? ConsList<AssemblySymbol>.Empty);
						return item.LookupTopLevelMetadataTypeWithCycleDetection(ref emittedName, visitedAssemblies, true);
					}
					else
					{
						i--;
					}
				}
			}
			return null;
		}

		// Token: 0x060066F3 RID: 26355 RVA: 0x001854A8 File Offset: 0x001836A8
		internal override IEnumerable<NamedTypeSymbol> GetAllTopLevelForwardedTypes()
		{
			return PEModuleBuilder.GetForwardedTypes(this, null);
		}

		// Token: 0x060066F4 RID: 26356 RVA: 0x001854B1 File Offset: 0x001836B1
		public override AssemblyMetadata GetMetadata()
		{
			return null;
		}

		// Token: 0x060066F5 RID: 26357 RVA: 0x001854B4 File Offset: 0x001836B4
		protected override ISymbol CreateISymbol()
		{
			return new SourceAssemblySymbol(this);
		}

		// Token: 0x060066F6 RID: 26358 RVA: 0x001854BC File Offset: 0x001836BC
		[CompilerGenerated]
		private WellKnownAttributeData <GetLimitedNetModuleDecodedWellKnownAttributeData>g__limitedDecodeWellKnownAttributes|103_0(ImmutableArray<CSharpAttributeData> attributesFromNetModules, ImmutableArray<string> netModuleNames, QuickAttributes attributeMatches)
		{
			int length = attributesFromNetModules.Length;
			HashSet<CSharpAttributeData> hashSet = null;
			CommonAssemblyWellKnownAttributeData<NamedTypeSymbol> commonAssemblyWellKnownAttributeData = null;
			for (int i = length - 1; i >= 0; i--)
			{
				CSharpAttributeData csharpAttributeData = attributesFromNetModules[i];
				if (!csharpAttributeData.HasErrors && this.ValidateAttributeUsageForNetModuleAttribute(csharpAttributeData, netModuleNames[i], BindingDiagnosticBag.Discarded, ref hashSet))
				{
					this.<GetLimitedNetModuleDecodedWellKnownAttributeData>g__limitedDecodeWellKnownAttribute|103_1(csharpAttributeData, attributeMatches, ref commonAssemblyWellKnownAttributeData);
				}
			}
			return commonAssemblyWellKnownAttributeData;
		}

		// Token: 0x060066F7 RID: 26359 RVA: 0x00185518 File Offset: 0x00183718
		[CompilerGenerated]
		private void <GetLimitedNetModuleDecodedWellKnownAttributeData>g__limitedDecodeWellKnownAttribute|103_1(CSharpAttributeData attribute, QuickAttributes attributeMatches, ref CommonAssemblyWellKnownAttributeData<NamedTypeSymbol> result)
		{
			if (attributeMatches == QuickAttributes.AssemblySignatureKey && attribute.IsTargetAttribute(this, AttributeDescription.AssemblySignatureKeyAttribute))
			{
				if (result == null)
				{
					result = new CommonAssemblyWellKnownAttributeData<NamedTypeSymbol>();
				}
				result.AssemblySignatureKeyAttributeSetting = (string)attribute.CommonConstructorArguments[0].ValueInternal;
				return;
			}
			if (attributeMatches == QuickAttributes.AssemblyKeyFile && attribute.IsTargetAttribute(this, AttributeDescription.AssemblyKeyFileAttribute))
			{
				if (result == null)
				{
					result = new CommonAssemblyWellKnownAttributeData<NamedTypeSymbol>();
				}
				result.AssemblyKeyFileAttributeSetting = (string)attribute.CommonConstructorArguments[0].ValueInternal;
				return;
			}
			if (attributeMatches == QuickAttributes.AssemblyKeyName && attribute.IsTargetAttribute(this, AttributeDescription.AssemblyKeyNameAttribute))
			{
				if (result == null)
				{
					result = new CommonAssemblyWellKnownAttributeData<NamedTypeSymbol>();
				}
				result.AssemblyKeyContainerAttributeSetting = (string)attribute.CommonConstructorArguments[0].ValueInternal;
			}
		}

		// Token: 0x060066F8 RID: 26360 RVA: 0x001855E8 File Offset: 0x001837E8
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		[CompilerGenerated]
		private bool <GetSourceDecodedWellKnownAttributeData>g__isPossibleAssemblySignatureKeyAttribute|112_0(AttributeSyntax node)
		{
			return this.DeclaringCompilation.GetBinderFactory(node.SyntaxTree, false).GetBinder(node, null, null).QuickAttributeChecker.IsPossibleMatch(node, QuickAttributes.AssemblySignatureKey);
		}

		// Token: 0x060066F9 RID: 26361 RVA: 0x00185611 File Offset: 0x00183811
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		[CompilerGenerated]
		private bool <GetSourceDecodedWellKnownAttributeData>g__isPossibleAssemblyKeyNameAttribute|112_1(AttributeSyntax node)
		{
			return this.DeclaringCompilation.GetBinderFactory(node.SyntaxTree, false).GetBinder(node, null, null).QuickAttributeChecker.IsPossibleMatch(node, QuickAttributes.AssemblyKeyName);
		}

		// Token: 0x060066FA RID: 26362 RVA: 0x00185639 File Offset: 0x00183839
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		[CompilerGenerated]
		private bool <GetSourceDecodedWellKnownAttributeData>g__isPossibleAssemblyKeyFileAttribute|112_2(AttributeSyntax node)
		{
			return this.DeclaringCompilation.GetBinderFactory(node.SyntaxTree, false).GetBinder(node, null, null).QuickAttributeChecker.IsPossibleMatch(node, QuickAttributes.AssemblyKeyFile);
		}

		// Token: 0x040019A3 RID: 6563
		private readonly CSharpCompilation _compilation;

		// Token: 0x040019A4 RID: 6564
		private SymbolCompletionState _state;

		// Token: 0x040019A5 RID: 6565
		internal AssemblyIdentity lazyAssemblyIdentity;

		// Token: 0x040019A6 RID: 6566
		private readonly string _assemblySimpleName;

		// Token: 0x040019A7 RID: 6567
		private StrongNameKeys _lazyStrongNameKeys;

		// Token: 0x040019A8 RID: 6568
		private readonly ImmutableArray<ModuleSymbol> _modules;

		// Token: 0x040019A9 RID: 6569
		private CustomAttributesBag<CSharpAttributeData> _lazySourceAttributesBag;

		// Token: 0x040019AA RID: 6570
		private CustomAttributesBag<CSharpAttributeData> _lazyNetModuleAttributesBag;

		// Token: 0x040019AB RID: 6571
		private IDictionary<string, NamedTypeSymbol> _lazyForwardedTypesFromSource;

		// Token: 0x040019AC RID: 6572
		private ConcurrentSet<int> _lazyOmittedAttributeIndices;

		// Token: 0x040019AD RID: 6573
		private ThreeState _lazyContainsExtensionMethods;

		// Token: 0x040019AE RID: 6574
		private readonly ConcurrentDictionary<FieldSymbol, bool> _unassignedFieldsMap = new ConcurrentDictionary<FieldSymbol, bool>();

		// Token: 0x040019AF RID: 6575
		private readonly ConcurrentSet<FieldSymbol> _unreadFields = new ConcurrentSet<FieldSymbol>();

		// Token: 0x040019B0 RID: 6576
		internal ConcurrentSet<TypeSymbol> TypesReferencedInExternalMethods = new ConcurrentSet<TypeSymbol>();

		// Token: 0x040019B1 RID: 6577
		private ImmutableArray<Diagnostic> _unusedFieldWarnings;

		// Token: 0x040019B2 RID: 6578
		private ConcurrentDictionary<AssemblySymbol, bool> _optimisticallyGrantedInternalsAccess;

		// Token: 0x040019B3 RID: 6579
		[ThreadStatic]
		private static AssemblySymbol t_assemblyForWhichCurrentThreadIsComputingKeys;

		// Token: 0x040019B4 RID: 6580
		private ConcurrentDictionary<string, ConcurrentDictionary<ImmutableArray<byte>, Tuple<Location, string>>> _lazyInternalsVisibleToMap;

		// Token: 0x020008E6 RID: 2278
		private class NameCollisionForAddedModulesTypeComparer : IComparer<NamedTypeSymbol>
		{
			// Token: 0x06008EDE RID: 36574 RVA: 0x001F7F53 File Offset: 0x001F6153
			private NameCollisionForAddedModulesTypeComparer()
			{
			}

			// Token: 0x06008EDF RID: 36575 RVA: 0x001F7F5C File Offset: 0x001F615C
			public int Compare(NamedTypeSymbol x, NamedTypeSymbol y)
			{
				int num = string.CompareOrdinal(x.Name, y.Name);
				if (num == 0)
				{
					num = x.Arity - y.Arity;
					if (num == 0)
					{
						num = x.ContainingModule.Ordinal - y.ContainingModule.Ordinal;
					}
				}
				return num;
			}

			// Token: 0x040026D9 RID: 9945
			public static readonly SourceAssemblySymbol.NameCollisionForAddedModulesTypeComparer Singleton = new SourceAssemblySymbol.NameCollisionForAddedModulesTypeComparer();
		}
	}
}
