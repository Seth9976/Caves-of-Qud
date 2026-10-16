using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200013D RID: 317
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class CompilationOptions
	{
		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000C70 RID: 3184 RVA: 0x0002633E File Offset: 0x0002453E
		// (set) Token: 0x06000C71 RID: 3185 RVA: 0x00026346 File Offset: 0x00024546
		public OutputKind OutputKind { get; protected set; }

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000C72 RID: 3186 RVA: 0x0002634F File Offset: 0x0002454F
		// (set) Token: 0x06000C73 RID: 3187 RVA: 0x00026357 File Offset: 0x00024557
		[Nullable(2)]
		public string ModuleName
		{
			[NullableContext(2)]
			get;
			[NullableContext(2)]
			protected set;
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000C74 RID: 3188 RVA: 0x00026360 File Offset: 0x00024560
		// (set) Token: 0x06000C75 RID: 3189 RVA: 0x00026368 File Offset: 0x00024568
		[Nullable(2)]
		public string ScriptClassName
		{
			[NullableContext(2)]
			get;
			[NullableContext(2)]
			protected set;
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000C76 RID: 3190 RVA: 0x00026371 File Offset: 0x00024571
		// (set) Token: 0x06000C77 RID: 3191 RVA: 0x00026379 File Offset: 0x00024579
		[Nullable(2)]
		public string MainTypeName
		{
			[NullableContext(2)]
			get;
			[NullableContext(2)]
			protected set;
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000C78 RID: 3192 RVA: 0x00026382 File Offset: 0x00024582
		// (set) Token: 0x06000C79 RID: 3193 RVA: 0x0002638A File Offset: 0x0002458A
		[Nullable(0)]
		public ImmutableArray<byte> CryptoPublicKey
		{
			[NullableContext(0)]
			get;
			[NullableContext(0)]
			protected set;
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000C7A RID: 3194 RVA: 0x00026393 File Offset: 0x00024593
		// (set) Token: 0x06000C7B RID: 3195 RVA: 0x0002639B File Offset: 0x0002459B
		[Nullable(2)]
		public string CryptoKeyFile
		{
			[NullableContext(2)]
			get;
			[NullableContext(2)]
			protected set;
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000C7C RID: 3196 RVA: 0x000263A4 File Offset: 0x000245A4
		// (set) Token: 0x06000C7D RID: 3197 RVA: 0x000263AC File Offset: 0x000245AC
		[Nullable(2)]
		public string CryptoKeyContainer
		{
			[NullableContext(2)]
			get;
			[NullableContext(2)]
			protected set;
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000C7E RID: 3198 RVA: 0x000263B5 File Offset: 0x000245B5
		// (set) Token: 0x06000C7F RID: 3199 RVA: 0x000263BD File Offset: 0x000245BD
		public bool? DelaySign { get; protected set; }

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000C80 RID: 3200 RVA: 0x000263C6 File Offset: 0x000245C6
		// (set) Token: 0x06000C81 RID: 3201 RVA: 0x000263CE File Offset: 0x000245CE
		public bool PublicSign { get; protected set; }

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000C82 RID: 3202 RVA: 0x000263D7 File Offset: 0x000245D7
		// (set) Token: 0x06000C83 RID: 3203 RVA: 0x000263DF File Offset: 0x000245DF
		public bool CheckOverflow { get; protected set; }

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000C84 RID: 3204 RVA: 0x000263E8 File Offset: 0x000245E8
		// (set) Token: 0x06000C85 RID: 3205 RVA: 0x000263F0 File Offset: 0x000245F0
		public Platform Platform { get; protected set; }

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000C86 RID: 3206 RVA: 0x000263F9 File Offset: 0x000245F9
		// (set) Token: 0x06000C87 RID: 3207 RVA: 0x00026401 File Offset: 0x00024601
		public OptimizationLevel OptimizationLevel { get; protected set; }

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000C88 RID: 3208 RVA: 0x0002640A File Offset: 0x0002460A
		// (set) Token: 0x06000C89 RID: 3209 RVA: 0x00026412 File Offset: 0x00024612
		public ReportDiagnostic GeneralDiagnosticOption { get; protected set; }

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000C8A RID: 3210 RVA: 0x0002641B File Offset: 0x0002461B
		// (set) Token: 0x06000C8B RID: 3211 RVA: 0x00026423 File Offset: 0x00024623
		public int WarningLevel { get; protected set; }

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000C8C RID: 3212 RVA: 0x0002642C File Offset: 0x0002462C
		// (set) Token: 0x06000C8D RID: 3213 RVA: 0x00026434 File Offset: 0x00024634
		public bool ConcurrentBuild { get; protected set; }

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000C8E RID: 3214 RVA: 0x0002643D File Offset: 0x0002463D
		// (set) Token: 0x06000C8F RID: 3215 RVA: 0x00026445 File Offset: 0x00024645
		public bool Deterministic { get; protected set; }

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000C90 RID: 3216 RVA: 0x0002644E File Offset: 0x0002464E
		// (set) Token: 0x06000C91 RID: 3217 RVA: 0x00026456 File Offset: 0x00024656
		protected internal DateTime CurrentLocalTime { internal get; private protected set; }

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000C92 RID: 3218 RVA: 0x0002645F File Offset: 0x0002465F
		// (set) Token: 0x06000C93 RID: 3219 RVA: 0x00026467 File Offset: 0x00024667
		internal bool DebugPlusMode { get; set; }

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000C94 RID: 3220 RVA: 0x00026470 File Offset: 0x00024670
		// (set) Token: 0x06000C95 RID: 3221 RVA: 0x00026478 File Offset: 0x00024678
		public MetadataImportOptions MetadataImportOptions { get; protected set; }

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000C96 RID: 3222 RVA: 0x00026481 File Offset: 0x00024681
		// (set) Token: 0x06000C97 RID: 3223 RVA: 0x00026489 File Offset: 0x00024689
		protected internal bool ReferencesSupersedeLowerVersions { internal get; private protected set; }

		// Token: 0x06000C98 RID: 3224
		[return: Nullable(2)]
		internal abstract Diagnostic FilterDiagnostic(Diagnostic diagnostic, CancellationToken cancellationToken);

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000C99 RID: 3225 RVA: 0x00026492 File Offset: 0x00024692
		// (set) Token: 0x06000C9A RID: 3226 RVA: 0x0002649A File Offset: 0x0002469A
		public ImmutableDictionary<string, ReportDiagnostic> SpecificDiagnosticOptions { get; protected set; }

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000C9B RID: 3227 RVA: 0x000264A3 File Offset: 0x000246A3
		// (set) Token: 0x06000C9C RID: 3228 RVA: 0x000264AB File Offset: 0x000246AB
		[Nullable(2)]
		public SyntaxTreeOptionsProvider SyntaxTreeOptionsProvider
		{
			[NullableContext(2)]
			get;
			[NullableContext(2)]
			protected set;
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000C9D RID: 3229 RVA: 0x000264B4 File Offset: 0x000246B4
		// (set) Token: 0x06000C9E RID: 3230 RVA: 0x000264BC File Offset: 0x000246BC
		public bool ReportSuppressedDiagnostics { get; protected set; }

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000C9F RID: 3231 RVA: 0x000264C5 File Offset: 0x000246C5
		// (set) Token: 0x06000CA0 RID: 3232 RVA: 0x000264CD File Offset: 0x000246CD
		[Nullable(2)]
		public MetadataReferenceResolver MetadataReferenceResolver
		{
			[NullableContext(2)]
			get;
			[NullableContext(2)]
			protected set;
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000CA1 RID: 3233 RVA: 0x000264D6 File Offset: 0x000246D6
		// (set) Token: 0x06000CA2 RID: 3234 RVA: 0x000264DE File Offset: 0x000246DE
		[Nullable(2)]
		public XmlReferenceResolver XmlReferenceResolver
		{
			[NullableContext(2)]
			get;
			[NullableContext(2)]
			protected set;
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000CA3 RID: 3235 RVA: 0x000264E7 File Offset: 0x000246E7
		// (set) Token: 0x06000CA4 RID: 3236 RVA: 0x000264EF File Offset: 0x000246EF
		[Nullable(2)]
		public SourceReferenceResolver SourceReferenceResolver
		{
			[NullableContext(2)]
			get;
			[NullableContext(2)]
			protected set;
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000CA5 RID: 3237 RVA: 0x000264F8 File Offset: 0x000246F8
		// (set) Token: 0x06000CA6 RID: 3238 RVA: 0x00026500 File Offset: 0x00024700
		[Nullable(2)]
		public StrongNameProvider StrongNameProvider
		{
			[NullableContext(2)]
			get;
			[NullableContext(2)]
			protected set;
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000CA7 RID: 3239 RVA: 0x00026509 File Offset: 0x00024709
		// (set) Token: 0x06000CA8 RID: 3240 RVA: 0x00026511 File Offset: 0x00024711
		public AssemblyIdentityComparer AssemblyIdentityComparer { get; protected set; }

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000CA9 RID: 3241
		// (set) Token: 0x06000CAA RID: 3242
		public abstract NullableContextOptions NullableContextOptions { get; protected set; }

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000CAB RID: 3243 RVA: 0x0002651A File Offset: 0x0002471A
		// (set) Token: 0x06000CAC RID: 3244 RVA: 0x00026521 File Offset: 0x00024721
		[Nullable(new byte[] { 0, 1 })]
		[Obsolete]
		protected internal ImmutableArray<string> Features
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				throw new NotImplementedException();
			}
			[param: Nullable(new byte[] { 0, 1 })]
			protected set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x00026528 File Offset: 0x00024728
		[NullableContext(2)]
		internal CompilationOptions(OutputKind outputKind, bool reportSuppressedDiagnostics, string moduleName, string mainTypeName, string scriptClassName, string cryptoKeyContainer, string cryptoKeyFile, [Nullable(0)] ImmutableArray<byte> cryptoPublicKey, bool? delaySign, bool publicSign, OptimizationLevel optimizationLevel, bool checkOverflow, Platform platform, ReportDiagnostic generalDiagnosticOption, int warningLevel, [Nullable(1)] ImmutableDictionary<string, ReportDiagnostic> specificDiagnosticOptions, bool concurrentBuild, bool deterministic, DateTime currentLocalTime, bool debugPlusMode, XmlReferenceResolver xmlReferenceResolver, SourceReferenceResolver sourceReferenceResolver, SyntaxTreeOptionsProvider syntaxTreeOptionsProvider, MetadataReferenceResolver metadataReferenceResolver, AssemblyIdentityComparer assemblyIdentityComparer, StrongNameProvider strongNameProvider, MetadataImportOptions metadataImportOptions, bool referencesSupersedeLowerVersions)
		{
			this.OutputKind = outputKind;
			this.ModuleName = moduleName;
			this.MainTypeName = mainTypeName;
			this.ScriptClassName = scriptClassName ?? "Script";
			this.CryptoKeyContainer = cryptoKeyContainer;
			this.CryptoKeyFile = (string.IsNullOrEmpty(cryptoKeyFile) ? null : cryptoKeyFile);
			this.CryptoPublicKey = cryptoPublicKey.NullToEmpty<byte>();
			this.DelaySign = delaySign;
			this.CheckOverflow = checkOverflow;
			this.Platform = platform;
			this.GeneralDiagnosticOption = generalDiagnosticOption;
			this.WarningLevel = warningLevel;
			this.SpecificDiagnosticOptions = specificDiagnosticOptions;
			this.ReportSuppressedDiagnostics = reportSuppressedDiagnostics;
			this.OptimizationLevel = optimizationLevel;
			this.ConcurrentBuild = concurrentBuild;
			this.Deterministic = deterministic;
			this.CurrentLocalTime = currentLocalTime;
			this.DebugPlusMode = debugPlusMode;
			this.XmlReferenceResolver = xmlReferenceResolver;
			this.SourceReferenceResolver = sourceReferenceResolver;
			this.SyntaxTreeOptionsProvider = syntaxTreeOptionsProvider;
			this.MetadataReferenceResolver = metadataReferenceResolver;
			this.StrongNameProvider = strongNameProvider;
			this.AssemblyIdentityComparer = assemblyIdentityComparer ?? AssemblyIdentityComparer.Default;
			this.MetadataImportOptions = metadataImportOptions;
			this.ReferencesSupersedeLowerVersions = referencesSupersedeLowerVersions;
			this.PublicSign = publicSign;
			this._lazyErrors = new Lazy<ImmutableArray<Diagnostic>>(delegate
			{
				ArrayBuilder<Diagnostic> instance = ArrayBuilder<Diagnostic>.GetInstance();
				this.ValidateOptions(instance);
				return instance.ToImmutableAndFree();
			});
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x00026654 File Offset: 0x00024854
		internal bool CanReuseCompilationReferenceManager(CompilationOptions other)
		{
			return this.MetadataImportOptions == other.MetadataImportOptions && this.ReferencesSupersedeLowerVersions == other.ReferencesSupersedeLowerVersions && this.OutputKind.IsNetModule() == other.OutputKind.IsNetModule() && object.Equals(this.XmlReferenceResolver, other.XmlReferenceResolver) && object.Equals(this.MetadataReferenceResolver, other.MetadataReferenceResolver) && object.Equals(this.AssemblyIdentityComparer, other.AssemblyIdentityComparer);
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000CAF RID: 3247
		public abstract string Language { get; }

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000CB0 RID: 3248 RVA: 0x000266CE File Offset: 0x000248CE
		internal bool EnableEditAndContinue
		{
			get
			{
				return this.OptimizationLevel == OptimizationLevel.Debug;
			}
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x000266D9 File Offset: 0x000248D9
		internal static bool IsValidFileAlignment(int value)
		{
			if (value <= 1024)
			{
				if (value != 512 && value != 1024)
				{
					return false;
				}
			}
			else if (value != 2048 && value != 4096 && value != 8192)
			{
				return false;
			}
			return true;
		}

		// Token: 0x06000CB2 RID: 3250
		[return: Nullable(new byte[] { 0, 1 })]
		internal abstract ImmutableArray<string> GetImports();

		// Token: 0x06000CB3 RID: 3251 RVA: 0x00026710 File Offset: 0x00024910
		public CompilationOptions WithGeneralDiagnosticOption(ReportDiagnostic value)
		{
			return this.CommonWithGeneralDiagnosticOption(value);
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x00026719 File Offset: 0x00024919
		public CompilationOptions WithSpecificDiagnosticOptions([Nullable(new byte[] { 2, 1 })] ImmutableDictionary<string, ReportDiagnostic> value)
		{
			return this.CommonWithSpecificDiagnosticOptions(value);
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x00026722 File Offset: 0x00024922
		public CompilationOptions WithSpecificDiagnosticOptions([Nullable(new byte[] { 1, 0, 1 })] IEnumerable<KeyValuePair<string, ReportDiagnostic>> value)
		{
			return this.CommonWithSpecificDiagnosticOptions(value);
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x0002672B File Offset: 0x0002492B
		public CompilationOptions WithReportSuppressedDiagnostics(bool value)
		{
			return this.CommonWithReportSuppressedDiagnostics(value);
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x00026734 File Offset: 0x00024934
		public CompilationOptions WithConcurrentBuild(bool concurrent)
		{
			return this.CommonWithConcurrentBuild(concurrent);
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x0002673D File Offset: 0x0002493D
		public CompilationOptions WithDeterministic(bool deterministic)
		{
			return this.CommonWithDeterministic(deterministic);
		}

		// Token: 0x06000CB9 RID: 3257 RVA: 0x00026746 File Offset: 0x00024946
		public CompilationOptions WithOutputKind(OutputKind kind)
		{
			return this.CommonWithOutputKind(kind);
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x0002674F File Offset: 0x0002494F
		public CompilationOptions WithPlatform(Platform platform)
		{
			return this.CommonWithPlatform(platform);
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x00026758 File Offset: 0x00024958
		public CompilationOptions WithPublicSign(bool publicSign)
		{
			return this.CommonWithPublicSign(publicSign);
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x00026761 File Offset: 0x00024961
		public CompilationOptions WithOptimizationLevel(OptimizationLevel value)
		{
			return this.CommonWithOptimizationLevel(value);
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x0002676A File Offset: 0x0002496A
		public CompilationOptions WithXmlReferenceResolver([Nullable(2)] XmlReferenceResolver resolver)
		{
			return this.CommonWithXmlReferenceResolver(resolver);
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x00026773 File Offset: 0x00024973
		public CompilationOptions WithSourceReferenceResolver([Nullable(2)] SourceReferenceResolver resolver)
		{
			return this.CommonWithSourceReferenceResolver(resolver);
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x0002677C File Offset: 0x0002497C
		public CompilationOptions WithSyntaxTreeOptionsProvider([Nullable(2)] SyntaxTreeOptionsProvider provider)
		{
			return this.CommonWithSyntaxTreeOptionsProvider(provider);
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x00026785 File Offset: 0x00024985
		public CompilationOptions WithMetadataReferenceResolver([Nullable(2)] MetadataReferenceResolver resolver)
		{
			return this.CommonWithMetadataReferenceResolver(resolver);
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x0002678E File Offset: 0x0002498E
		public CompilationOptions WithAssemblyIdentityComparer(AssemblyIdentityComparer comparer)
		{
			return this.CommonWithAssemblyIdentityComparer(comparer);
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x00026797 File Offset: 0x00024997
		public CompilationOptions WithStrongNameProvider([Nullable(2)] StrongNameProvider provider)
		{
			return this.CommonWithStrongNameProvider(provider);
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x000267A0 File Offset: 0x000249A0
		public CompilationOptions WithModuleName([Nullable(2)] string moduleName)
		{
			return this.CommonWithModuleName(moduleName);
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x000267A9 File Offset: 0x000249A9
		public CompilationOptions WithMainTypeName([Nullable(2)] string mainTypeName)
		{
			return this.CommonWithMainTypeName(mainTypeName);
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x000267B2 File Offset: 0x000249B2
		public CompilationOptions WithScriptClassName(string scriptClassName)
		{
			return this.CommonWithScriptClassName(scriptClassName);
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x000267BB File Offset: 0x000249BB
		public CompilationOptions WithCryptoKeyContainer([Nullable(2)] string cryptoKeyContainer)
		{
			return this.CommonWithCryptoKeyContainer(cryptoKeyContainer);
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x000267C4 File Offset: 0x000249C4
		public CompilationOptions WithCryptoKeyFile([Nullable(2)] string cryptoKeyFile)
		{
			return this.CommonWithCryptoKeyFile(cryptoKeyFile);
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x000267CD File Offset: 0x000249CD
		[NullableContext(0)]
		[return: Nullable(1)]
		public CompilationOptions WithCryptoPublicKey(ImmutableArray<byte> cryptoPublicKey)
		{
			return this.CommonWithCryptoPublicKey(cryptoPublicKey);
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x000267D6 File Offset: 0x000249D6
		public CompilationOptions WithDelaySign(bool? delaySign)
		{
			return this.CommonWithDelaySign(delaySign);
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x000267DF File Offset: 0x000249DF
		public CompilationOptions WithOverflowChecks(bool checkOverflow)
		{
			return this.CommonWithCheckOverflow(checkOverflow);
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x000267E8 File Offset: 0x000249E8
		public CompilationOptions WithMetadataImportOptions(MetadataImportOptions value)
		{
			return this.CommonWithMetadataImportOptions(value);
		}

		// Token: 0x06000CCC RID: 3276
		protected abstract CompilationOptions CommonWithConcurrentBuild(bool concurrent);

		// Token: 0x06000CCD RID: 3277
		protected abstract CompilationOptions CommonWithDeterministic(bool deterministic);

		// Token: 0x06000CCE RID: 3278
		protected abstract CompilationOptions CommonWithOutputKind(OutputKind kind);

		// Token: 0x06000CCF RID: 3279
		protected abstract CompilationOptions CommonWithPlatform(Platform platform);

		// Token: 0x06000CD0 RID: 3280
		protected abstract CompilationOptions CommonWithPublicSign(bool publicSign);

		// Token: 0x06000CD1 RID: 3281
		protected abstract CompilationOptions CommonWithOptimizationLevel(OptimizationLevel value);

		// Token: 0x06000CD2 RID: 3282
		protected abstract CompilationOptions CommonWithXmlReferenceResolver([Nullable(2)] XmlReferenceResolver resolver);

		// Token: 0x06000CD3 RID: 3283
		protected abstract CompilationOptions CommonWithSourceReferenceResolver([Nullable(2)] SourceReferenceResolver resolver);

		// Token: 0x06000CD4 RID: 3284
		protected abstract CompilationOptions CommonWithSyntaxTreeOptionsProvider([Nullable(2)] SyntaxTreeOptionsProvider resolver);

		// Token: 0x06000CD5 RID: 3285
		protected abstract CompilationOptions CommonWithMetadataReferenceResolver([Nullable(2)] MetadataReferenceResolver resolver);

		// Token: 0x06000CD6 RID: 3286
		protected abstract CompilationOptions CommonWithAssemblyIdentityComparer([Nullable(2)] AssemblyIdentityComparer comparer);

		// Token: 0x06000CD7 RID: 3287
		protected abstract CompilationOptions CommonWithStrongNameProvider([Nullable(2)] StrongNameProvider provider);

		// Token: 0x06000CD8 RID: 3288
		protected abstract CompilationOptions CommonWithGeneralDiagnosticOption(ReportDiagnostic generalDiagnosticOption);

		// Token: 0x06000CD9 RID: 3289
		protected abstract CompilationOptions CommonWithSpecificDiagnosticOptions([Nullable(new byte[] { 2, 1 })] ImmutableDictionary<string, ReportDiagnostic> specificDiagnosticOptions);

		// Token: 0x06000CDA RID: 3290
		protected abstract CompilationOptions CommonWithSpecificDiagnosticOptions([Nullable(new byte[] { 1, 0, 1 })] IEnumerable<KeyValuePair<string, ReportDiagnostic>> specificDiagnosticOptions);

		// Token: 0x06000CDB RID: 3291
		protected abstract CompilationOptions CommonWithReportSuppressedDiagnostics(bool reportSuppressedDiagnostics);

		// Token: 0x06000CDC RID: 3292
		protected abstract CompilationOptions CommonWithModuleName([Nullable(2)] string moduleName);

		// Token: 0x06000CDD RID: 3293
		protected abstract CompilationOptions CommonWithMainTypeName([Nullable(2)] string mainTypeName);

		// Token: 0x06000CDE RID: 3294
		protected abstract CompilationOptions CommonWithScriptClassName(string scriptClassName);

		// Token: 0x06000CDF RID: 3295
		protected abstract CompilationOptions CommonWithCryptoKeyContainer([Nullable(2)] string cryptoKeyContainer);

		// Token: 0x06000CE0 RID: 3296
		protected abstract CompilationOptions CommonWithCryptoKeyFile([Nullable(2)] string cryptoKeyFile);

		// Token: 0x06000CE1 RID: 3297
		[NullableContext(0)]
		[return: Nullable(1)]
		protected abstract CompilationOptions CommonWithCryptoPublicKey(ImmutableArray<byte> cryptoPublicKey);

		// Token: 0x06000CE2 RID: 3298
		protected abstract CompilationOptions CommonWithDelaySign(bool? delaySign);

		// Token: 0x06000CE3 RID: 3299
		protected abstract CompilationOptions CommonWithCheckOverflow(bool checkOverflow);

		// Token: 0x06000CE4 RID: 3300
		protected abstract CompilationOptions CommonWithMetadataImportOptions(MetadataImportOptions value);

		// Token: 0x06000CE5 RID: 3301
		[Obsolete]
		protected abstract CompilationOptions CommonWithFeatures([Nullable(new byte[] { 0, 1 })] ImmutableArray<string> features);

		// Token: 0x06000CE6 RID: 3302
		internal abstract void ValidateOptions(ArrayBuilder<Diagnostic> builder);

		// Token: 0x06000CE7 RID: 3303 RVA: 0x000267F4 File Offset: 0x000249F4
		internal void ValidateOptions(ArrayBuilder<Diagnostic> builder, CommonMessageProvider messageProvider)
		{
			if (!this.CryptoPublicKey.IsEmpty)
			{
				if (this.CryptoKeyFile != null)
				{
					builder.Add(messageProvider.CreateDiagnostic(messageProvider.ERR_MutuallyExclusiveOptions, Location.None, new object[] { "CryptoPublicKey", "CryptoKeyFile" }));
				}
				if (this.CryptoKeyContainer != null)
				{
					builder.Add(messageProvider.CreateDiagnostic(messageProvider.ERR_MutuallyExclusiveOptions, Location.None, new object[] { "CryptoPublicKey", "CryptoKeyContainer" }));
				}
			}
			if (this.PublicSign)
			{
				if (this.CryptoKeyFile != null && !PathUtilities.IsAbsolute(this.CryptoKeyFile))
				{
					builder.Add(messageProvider.CreateDiagnostic(messageProvider.ERR_OptionMustBeAbsolutePath, Location.None, new object[] { "CryptoKeyFile" }));
				}
				if (this.CryptoKeyContainer != null)
				{
					builder.Add(messageProvider.CreateDiagnostic(messageProvider.ERR_MutuallyExclusiveOptions, Location.None, new object[] { "PublicSign", "CryptoKeyContainer" }));
				}
				bool? delaySign = this.DelaySign;
				bool flag = true;
				if ((delaySign.GetValueOrDefault() == flag) & (delaySign != null))
				{
					builder.Add(messageProvider.CreateDiagnostic(messageProvider.ERR_MutuallyExclusiveOptions, Location.None, new object[] { "PublicSign", "DelaySign" }));
				}
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000CE8 RID: 3304 RVA: 0x0002693F File Offset: 0x00024B3F
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<Diagnostic> Errors
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._lazyErrors.Value;
			}
		}

		// Token: 0x06000CE9 RID: 3305
		[NullableContext(2)]
		public abstract override bool Equals(object obj);

		// Token: 0x06000CEA RID: 3306 RVA: 0x0002694C File Offset: 0x00024B4C
		[NullableContext(2)]
		protected bool EqualsHelper([NotNullWhen(true)] CompilationOptions other)
		{
			if (other == null)
			{
				return false;
			}
			if (this.CheckOverflow == other.CheckOverflow && this.ConcurrentBuild == other.ConcurrentBuild && this.Deterministic == other.Deterministic && this.CurrentLocalTime == other.CurrentLocalTime && this.DebugPlusMode == other.DebugPlusMode && string.Equals(this.CryptoKeyContainer, other.CryptoKeyContainer, 4) && string.Equals(this.CryptoKeyFile, other.CryptoKeyFile, 4) && this.CryptoPublicKey.SequenceEqual<byte, byte>(other.CryptoPublicKey, null))
			{
				bool? delaySign = this.DelaySign;
				bool? delaySign2 = other.DelaySign;
				if (((delaySign.GetValueOrDefault() == delaySign2.GetValueOrDefault()) & (delaySign != null == (delaySign2 != null))) && this.GeneralDiagnosticOption == other.GeneralDiagnosticOption && string.Equals(this.MainTypeName, other.MainTypeName, 4) && this.MetadataImportOptions == other.MetadataImportOptions && this.ReferencesSupersedeLowerVersions == other.ReferencesSupersedeLowerVersions && string.Equals(this.ModuleName, other.ModuleName, 4) && this.OptimizationLevel == other.OptimizationLevel && this.OutputKind == other.OutputKind && this.Platform == other.Platform && this.ReportSuppressedDiagnostics == other.ReportSuppressedDiagnostics && string.Equals(this.ScriptClassName, other.ScriptClassName, 4))
				{
					if (this.SpecificDiagnosticOptions.SequenceEqual<KeyValuePair<string, ReportDiagnostic>>(other.SpecificDiagnosticOptions, (KeyValuePair<string, ReportDiagnostic> left, KeyValuePair<string, ReportDiagnostic> right) => left.Key == right.Key && left.Value == right.Value) && this.WarningLevel == other.WarningLevel && object.Equals(this.MetadataReferenceResolver, other.MetadataReferenceResolver) && object.Equals(this.XmlReferenceResolver, other.XmlReferenceResolver) && object.Equals(this.SourceReferenceResolver, other.SourceReferenceResolver) && object.Equals(this.SyntaxTreeOptionsProvider, other.SyntaxTreeOptionsProvider) && object.Equals(this.StrongNameProvider, other.StrongNameProvider) && object.Equals(this.AssemblyIdentityComparer, other.AssemblyIdentityComparer) && this.PublicSign == other.PublicSign)
					{
						return this.NullableContextOptions == other.NullableContextOptions;
					}
				}
			}
			return false;
		}

		// Token: 0x06000CEB RID: 3307
		public abstract override int GetHashCode();

		// Token: 0x06000CEC RID: 3308 RVA: 0x00026BC4 File Offset: 0x00024DC4
		protected int GetHashCodeHelper()
		{
			return Hash.Combine(this.CheckOverflow, Hash.Combine(this.ConcurrentBuild, Hash.Combine(this.Deterministic, Hash.Combine(this.CurrentLocalTime.GetHashCode(), Hash.Combine(this.DebugPlusMode, Hash.Combine((this.CryptoKeyContainer != null) ? StringComparer.Ordinal.GetHashCode(this.CryptoKeyContainer) : 0, Hash.Combine((this.CryptoKeyFile != null) ? StringComparer.Ordinal.GetHashCode(this.CryptoKeyFile) : 0, Hash.Combine(Hash.CombineValues<byte>(this.CryptoPublicKey, 16), Hash.Combine((int)this.GeneralDiagnosticOption, Hash.Combine((this.MainTypeName != null) ? StringComparer.Ordinal.GetHashCode(this.MainTypeName) : 0, Hash.Combine((int)this.MetadataImportOptions, Hash.Combine(this.ReferencesSupersedeLowerVersions, Hash.Combine((this.ModuleName != null) ? StringComparer.Ordinal.GetHashCode(this.ModuleName) : 0, Hash.Combine((int)this.OptimizationLevel, Hash.Combine((int)this.OutputKind, Hash.Combine((int)this.Platform, Hash.Combine(this.ReportSuppressedDiagnostics, Hash.Combine((this.ScriptClassName != null) ? StringComparer.Ordinal.GetHashCode(this.ScriptClassName) : 0, Hash.Combine(Hash.CombineValues<KeyValuePair<string, ReportDiagnostic>>(this.SpecificDiagnosticOptions, int.MaxValue), Hash.Combine(this.WarningLevel, Hash.Combine<MetadataReferenceResolver>(this.MetadataReferenceResolver, Hash.Combine<XmlReferenceResolver>(this.XmlReferenceResolver, Hash.Combine<SourceReferenceResolver>(this.SourceReferenceResolver, Hash.Combine<SyntaxTreeOptionsProvider>(this.SyntaxTreeOptionsProvider, Hash.Combine<StrongNameProvider>(this.StrongNameProvider, Hash.Combine<AssemblyIdentityComparer>(this.AssemblyIdentityComparer, Hash.Combine(this.PublicSign, Hash.Combine((int)this.NullableContextOptions, 0))))))))))))))))))))))))))));
		}

		// Token: 0x06000CED RID: 3309 RVA: 0x00026D88 File Offset: 0x00024F88
		[NullableContext(2)]
		public static bool operator ==(CompilationOptions left, CompilationOptions right)
		{
			return object.Equals(left, right);
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x00026D91 File Offset: 0x00024F91
		[NullableContext(2)]
		public static bool operator !=(CompilationOptions left, CompilationOptions right)
		{
			return !object.Equals(left, right);
		}

		// Token: 0x0400038A RID: 906
		[Nullable(new byte[] { 1, 0, 1 })]
		private readonly Lazy<ImmutableArray<Diagnostic>> _lazyErrors;
	}
}
