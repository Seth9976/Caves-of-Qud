using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200011D RID: 285
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class CSharpCompilationOptions : CompilationOptions, IEquatable<CSharpCompilationOptions>
	{
		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06001778 RID: 6008 RVA: 0x0008111B File Offset: 0x0007F31B
		// (set) Token: 0x06001779 RID: 6009 RVA: 0x00081123 File Offset: 0x0007F323
		public bool AllowUnsafe { get; private set; }

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x0600177A RID: 6010 RVA: 0x0008112C File Offset: 0x0007F32C
		// (set) Token: 0x0600177B RID: 6011 RVA: 0x00081134 File Offset: 0x0007F334
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<string> Usings
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
			[param: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			private set;
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x0600177C RID: 6012 RVA: 0x0008113D File Offset: 0x0007F33D
		// (set) Token: 0x0600177D RID: 6013 RVA: 0x00081145 File Offset: 0x0007F345
		internal BinderFlags TopLevelBinderFlags { get; private set; }

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x0600177E RID: 6014 RVA: 0x0008114E File Offset: 0x0007F34E
		// (set) Token: 0x0600177F RID: 6015 RVA: 0x00081156 File Offset: 0x0007F356
		public override NullableContextOptions NullableContextOptions { get; protected set; }

		// Token: 0x06001780 RID: 6016 RVA: 0x00081160 File Offset: 0x0007F360
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public CSharpCompilationOptions(OutputKind outputKind, bool reportSuppressedDiagnostics = false, string moduleName = null, string mainTypeName = null, string scriptClassName = null, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] IEnumerable<string> usings = null, OptimizationLevel optimizationLevel = OptimizationLevel.Debug, bool checkOverflow = false, bool allowUnsafe = false, string cryptoKeyContainer = null, string cryptoKeyFile = null, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<byte> cryptoPublicKey = default(ImmutableArray<byte>), bool? delaySign = null, Platform platform = Platform.AnyCpu, ReportDiagnostic generalDiagnosticOption = ReportDiagnostic.Default, int warningLevel = 4, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 0, 1 })] IEnumerable<KeyValuePair<string, ReportDiagnostic>> specificDiagnosticOptions = null, bool concurrentBuild = true, bool deterministic = false, XmlReferenceResolver xmlReferenceResolver = null, SourceReferenceResolver sourceReferenceResolver = null, MetadataReferenceResolver metadataReferenceResolver = null, AssemblyIdentityComparer assemblyIdentityComparer = null, StrongNameProvider strongNameProvider = null, bool publicSign = false, MetadataImportOptions metadataImportOptions = MetadataImportOptions.Public, NullableContextOptions nullableContextOptions = NullableContextOptions.Disable)
			: this(outputKind, reportSuppressedDiagnostics, moduleName, mainTypeName, scriptClassName, usings, optimizationLevel, checkOverflow, allowUnsafe, cryptoKeyContainer, cryptoKeyFile, cryptoPublicKey, delaySign, platform, generalDiagnosticOption, warningLevel, specificDiagnosticOptions, concurrentBuild, deterministic, default(DateTime), false, xmlReferenceResolver, sourceReferenceResolver, null, metadataReferenceResolver, assemblyIdentityComparer, strongNameProvider, metadataImportOptions, false, publicSign, BinderFlags.None, nullableContextOptions)
		{
		}

		// Token: 0x06001781 RID: 6017 RVA: 0x000811B4 File Offset: 0x0007F3B4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public CSharpCompilationOptions(OutputKind outputKind, bool reportSuppressedDiagnostics, string moduleName, string mainTypeName, string scriptClassName, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] IEnumerable<string> usings, OptimizationLevel optimizationLevel, bool checkOverflow, bool allowUnsafe, string cryptoKeyContainer, string cryptoKeyFile, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<byte> cryptoPublicKey, bool? delaySign, Platform platform, ReportDiagnostic generalDiagnosticOption, int warningLevel, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 0, 1 })] IEnumerable<KeyValuePair<string, ReportDiagnostic>> specificDiagnosticOptions, bool concurrentBuild, bool deterministic, XmlReferenceResolver xmlReferenceResolver, SourceReferenceResolver sourceReferenceResolver, MetadataReferenceResolver metadataReferenceResolver, AssemblyIdentityComparer assemblyIdentityComparer, StrongNameProvider strongNameProvider, bool publicSign, MetadataImportOptions metadataImportOptions)
			: this(outputKind, reportSuppressedDiagnostics, moduleName, mainTypeName, scriptClassName, usings, optimizationLevel, checkOverflow, allowUnsafe, cryptoKeyContainer, cryptoKeyFile, cryptoPublicKey, delaySign, platform, generalDiagnosticOption, warningLevel, specificDiagnosticOptions, concurrentBuild, deterministic, xmlReferenceResolver, sourceReferenceResolver, metadataReferenceResolver, assemblyIdentityComparer, strongNameProvider, publicSign, metadataImportOptions, NullableContextOptions.Disable)
		{
		}

		// Token: 0x06001782 RID: 6018 RVA: 0x000811FC File Offset: 0x0007F3FC
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[EditorBrowsable(1)]
		public CSharpCompilationOptions(OutputKind outputKind, bool reportSuppressedDiagnostics, string moduleName, string mainTypeName, string scriptClassName, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] IEnumerable<string> usings, OptimizationLevel optimizationLevel, bool checkOverflow, bool allowUnsafe, string cryptoKeyContainer, string cryptoKeyFile, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<byte> cryptoPublicKey, bool? delaySign, Platform platform, ReportDiagnostic generalDiagnosticOption, int warningLevel, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 0, 1 })] IEnumerable<KeyValuePair<string, ReportDiagnostic>> specificDiagnosticOptions, bool concurrentBuild, bool deterministic, XmlReferenceResolver xmlReferenceResolver, SourceReferenceResolver sourceReferenceResolver, MetadataReferenceResolver metadataReferenceResolver, AssemblyIdentityComparer assemblyIdentityComparer, StrongNameProvider strongNameProvider, bool publicSign)
			: this(outputKind, reportSuppressedDiagnostics, moduleName, mainTypeName, scriptClassName, usings, optimizationLevel, checkOverflow, allowUnsafe, cryptoKeyContainer, cryptoKeyFile, cryptoPublicKey, delaySign, platform, generalDiagnosticOption, warningLevel, specificDiagnosticOptions, concurrentBuild, deterministic, xmlReferenceResolver, sourceReferenceResolver, metadataReferenceResolver, assemblyIdentityComparer, strongNameProvider, publicSign, MetadataImportOptions.Public)
		{
		}

		// Token: 0x06001783 RID: 6019 RVA: 0x00081240 File Offset: 0x0007F440
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal CSharpCompilationOptions(OutputKind outputKind, bool reportSuppressedDiagnostics, string moduleName, string mainTypeName, string scriptClassName, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] IEnumerable<string> usings, OptimizationLevel optimizationLevel, bool checkOverflow, bool allowUnsafe, string cryptoKeyContainer, string cryptoKeyFile, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<byte> cryptoPublicKey, bool? delaySign, Platform platform, ReportDiagnostic generalDiagnosticOption, int warningLevel, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 0, 1 })] IEnumerable<KeyValuePair<string, ReportDiagnostic>> specificDiagnosticOptions, bool concurrentBuild, bool deterministic, DateTime currentLocalTime, bool debugPlusMode, XmlReferenceResolver xmlReferenceResolver, SourceReferenceResolver sourceReferenceResolver, SyntaxTreeOptionsProvider syntaxTreeOptionsProvider, MetadataReferenceResolver metadataReferenceResolver, AssemblyIdentityComparer assemblyIdentityComparer, StrongNameProvider strongNameProvider, MetadataImportOptions metadataImportOptions, bool referencesSupersedeLowerVersions, bool publicSign, BinderFlags topLevelBinderFlags, NullableContextOptions nullableContextOptions)
			: base(outputKind, reportSuppressedDiagnostics, moduleName, mainTypeName, scriptClassName, cryptoKeyContainer, cryptoKeyFile, cryptoPublicKey, delaySign, publicSign, optimizationLevel, checkOverflow, platform, generalDiagnosticOption, warningLevel, specificDiagnosticOptions.ToImmutableDictionaryOrEmpty<string, ReportDiagnostic>(), concurrentBuild, deterministic, currentLocalTime, debugPlusMode, xmlReferenceResolver, sourceReferenceResolver, syntaxTreeOptionsProvider, metadataReferenceResolver, assemblyIdentityComparer, strongNameProvider, metadataImportOptions, referencesSupersedeLowerVersions)
		{
			this.Usings = usings.AsImmutableOrEmpty<string>();
			this.AllowUnsafe = allowUnsafe;
			this.TopLevelBinderFlags = topLevelBinderFlags;
			this.NullableContextOptions = nullableContextOptions;
		}

		// Token: 0x06001784 RID: 6020 RVA: 0x000812B4 File Offset: 0x0007F4B4
		private CSharpCompilationOptions(CSharpCompilationOptions other)
		{
			OutputKind outputKind = other.OutputKind;
			string moduleName = other.ModuleName;
			string mainTypeName = other.MainTypeName;
			string scriptClassName = other.ScriptClassName;
			IEnumerable<string> enumerable = other.Usings;
			OptimizationLevel optimizationLevel = other.OptimizationLevel;
			bool checkOverflow = other.CheckOverflow;
			bool allowUnsafe = other.AllowUnsafe;
			string cryptoKeyContainer = other.CryptoKeyContainer;
			string cryptoKeyFile = other.CryptoKeyFile;
			ImmutableArray<byte> cryptoPublicKey = other.CryptoPublicKey;
			bool? delaySign = other.DelaySign;
			Platform platform = other.Platform;
			ReportDiagnostic generalDiagnosticOption = other.GeneralDiagnosticOption;
			int warningLevel = other.WarningLevel;
			IEnumerable<KeyValuePair<string, ReportDiagnostic>> specificDiagnosticOptions = other.SpecificDiagnosticOptions;
			bool concurrentBuild = other.ConcurrentBuild;
			bool deterministic = other.Deterministic;
			DateTime currentLocalTime = other.CurrentLocalTime;
			bool debugPlusMode = other.DebugPlusMode;
			XmlReferenceResolver xmlReferenceResolver = other.XmlReferenceResolver;
			SourceReferenceResolver sourceReferenceResolver = other.SourceReferenceResolver;
			SyntaxTreeOptionsProvider syntaxTreeOptionsProvider = other.SyntaxTreeOptionsProvider;
			MetadataReferenceResolver metadataReferenceResolver = other.MetadataReferenceResolver;
			AssemblyIdentityComparer assemblyIdentityComparer = other.AssemblyIdentityComparer;
			StrongNameProvider strongNameProvider = other.StrongNameProvider;
			MetadataImportOptions metadataImportOptions = other.MetadataImportOptions;
			bool referencesSupersedeLowerVersions = other.ReferencesSupersedeLowerVersions;
			this..ctor(outputKind, other.ReportSuppressedDiagnostics, moduleName, mainTypeName, scriptClassName, enumerable, optimizationLevel, checkOverflow, allowUnsafe, cryptoKeyContainer, cryptoKeyFile, cryptoPublicKey, delaySign, platform, generalDiagnosticOption, warningLevel, specificDiagnosticOptions, concurrentBuild, deterministic, currentLocalTime, debugPlusMode, xmlReferenceResolver, sourceReferenceResolver, syntaxTreeOptionsProvider, metadataReferenceResolver, assemblyIdentityComparer, strongNameProvider, metadataImportOptions, referencesSupersedeLowerVersions, other.PublicSign, other.TopLevelBinderFlags, other.NullableContextOptions);
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06001785 RID: 6021 RVA: 0x000813F0 File Offset: 0x0007F5F0
		public override string Language
		{
			get
			{
				return "C#";
			}
		}

		// Token: 0x06001786 RID: 6022 RVA: 0x000813F7 File Offset: 0x0007F5F7
		internal CSharpCompilationOptions WithTopLevelBinderFlags(BinderFlags flags)
		{
			if (flags != this.TopLevelBinderFlags)
			{
				return new CSharpCompilationOptions(this)
				{
					TopLevelBinderFlags = flags
				};
			}
			return this;
		}

		// Token: 0x06001787 RID: 6023 RVA: 0x00081411 File Offset: 0x0007F611
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal override ImmutableArray<string> GetImports()
		{
			return this.Usings;
		}

		// Token: 0x06001788 RID: 6024 RVA: 0x00081419 File Offset: 0x0007F619
		public new CSharpCompilationOptions WithOutputKind(OutputKind kind)
		{
			if (kind == base.OutputKind)
			{
				return this;
			}
			return new CSharpCompilationOptions(this)
			{
				OutputKind = kind
			};
		}

		// Token: 0x06001789 RID: 6025 RVA: 0x00081433 File Offset: 0x0007F633
		public new CSharpCompilationOptions WithModuleName([global::System.Runtime.CompilerServices.Nullable(2)] string moduleName)
		{
			if (moduleName == base.ModuleName)
			{
				return this;
			}
			return new CSharpCompilationOptions(this)
			{
				ModuleName = moduleName
			};
		}

		// Token: 0x0600178A RID: 6026 RVA: 0x00081452 File Offset: 0x0007F652
		public new CSharpCompilationOptions WithScriptClassName([global::System.Runtime.CompilerServices.Nullable(2)] string name)
		{
			if (name == base.ScriptClassName)
			{
				return this;
			}
			return new CSharpCompilationOptions(this)
			{
				ScriptClassName = name
			};
		}

		// Token: 0x0600178B RID: 6027 RVA: 0x00081471 File Offset: 0x0007F671
		public new CSharpCompilationOptions WithMainTypeName([global::System.Runtime.CompilerServices.Nullable(2)] string name)
		{
			if (name == base.MainTypeName)
			{
				return this;
			}
			return new CSharpCompilationOptions(this)
			{
				MainTypeName = name
			};
		}

		// Token: 0x0600178C RID: 6028 RVA: 0x00081490 File Offset: 0x0007F690
		public new CSharpCompilationOptions WithCryptoKeyContainer([global::System.Runtime.CompilerServices.Nullable(2)] string name)
		{
			if (name == base.CryptoKeyContainer)
			{
				return this;
			}
			return new CSharpCompilationOptions(this)
			{
				CryptoKeyContainer = name
			};
		}

		// Token: 0x0600178D RID: 6029 RVA: 0x000814AF File Offset: 0x0007F6AF
		public new CSharpCompilationOptions WithCryptoKeyFile([global::System.Runtime.CompilerServices.Nullable(2)] string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				path = null;
			}
			if (path == base.CryptoKeyFile)
			{
				return this;
			}
			return new CSharpCompilationOptions(this)
			{
				CryptoKeyFile = path
			};
		}

		// Token: 0x0600178E RID: 6030 RVA: 0x000814D9 File Offset: 0x0007F6D9
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public new CSharpCompilationOptions WithCryptoPublicKey(ImmutableArray<byte> value)
		{
			if (value.IsDefault)
			{
				value = ImmutableArray<byte>.Empty;
			}
			if (value == base.CryptoPublicKey)
			{
				return this;
			}
			return new CSharpCompilationOptions(this)
			{
				CryptoPublicKey = value
			};
		}

		// Token: 0x0600178F RID: 6031 RVA: 0x00081508 File Offset: 0x0007F708
		public new CSharpCompilationOptions WithDelaySign(bool? value)
		{
			bool? flag = value;
			bool? delaySign = base.DelaySign;
			if ((flag.GetValueOrDefault() == delaySign.GetValueOrDefault()) & (flag != null == (delaySign != null)))
			{
				return this;
			}
			return new CSharpCompilationOptions(this)
			{
				DelaySign = value
			};
		}

		// Token: 0x06001790 RID: 6032 RVA: 0x00081550 File Offset: 0x0007F750
		public CSharpCompilationOptions WithUsings([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<string> usings)
		{
			if (this.Usings == usings)
			{
				return this;
			}
			return new CSharpCompilationOptions(this)
			{
				Usings = usings
			};
		}

		// Token: 0x06001791 RID: 6033 RVA: 0x0008156F File Offset: 0x0007F76F
		public CSharpCompilationOptions WithUsings([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] IEnumerable<string> usings)
		{
			return new CSharpCompilationOptions(this)
			{
				Usings = usings.AsImmutableOrEmpty<string>()
			};
		}

		// Token: 0x06001792 RID: 6034 RVA: 0x00081583 File Offset: 0x0007F783
		public CSharpCompilationOptions WithUsings([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] params string[] usings)
		{
			return this.WithUsings(usings);
		}

		// Token: 0x06001793 RID: 6035 RVA: 0x0008158C File Offset: 0x0007F78C
		public new CSharpCompilationOptions WithOptimizationLevel(OptimizationLevel value)
		{
			if (value == base.OptimizationLevel)
			{
				return this;
			}
			return new CSharpCompilationOptions(this)
			{
				OptimizationLevel = value
			};
		}

		// Token: 0x06001794 RID: 6036 RVA: 0x000815A6 File Offset: 0x0007F7A6
		public new CSharpCompilationOptions WithOverflowChecks(bool enabled)
		{
			if (enabled == base.CheckOverflow)
			{
				return this;
			}
			return new CSharpCompilationOptions(this)
			{
				CheckOverflow = enabled
			};
		}

		// Token: 0x06001795 RID: 6037 RVA: 0x000815C0 File Offset: 0x0007F7C0
		public CSharpCompilationOptions WithNullableContextOptions(NullableContextOptions options)
		{
			if (options == this.NullableContextOptions)
			{
				return this;
			}
			return new CSharpCompilationOptions(this)
			{
				NullableContextOptions = options
			};
		}

		// Token: 0x06001796 RID: 6038 RVA: 0x000815DA File Offset: 0x0007F7DA
		public CSharpCompilationOptions WithAllowUnsafe(bool enabled)
		{
			if (enabled == this.AllowUnsafe)
			{
				return this;
			}
			return new CSharpCompilationOptions(this)
			{
				AllowUnsafe = enabled
			};
		}

		// Token: 0x06001797 RID: 6039 RVA: 0x000815F4 File Offset: 0x0007F7F4
		public new CSharpCompilationOptions WithPlatform(Platform platform)
		{
			if (base.Platform == platform)
			{
				return this;
			}
			return new CSharpCompilationOptions(this)
			{
				Platform = platform
			};
		}

		// Token: 0x06001798 RID: 6040 RVA: 0x0008160E File Offset: 0x0007F80E
		public new CSharpCompilationOptions WithPublicSign(bool publicSign)
		{
			if (base.PublicSign == publicSign)
			{
				return this;
			}
			return new CSharpCompilationOptions(this)
			{
				PublicSign = publicSign
			};
		}

		// Token: 0x06001799 RID: 6041 RVA: 0x00081628 File Offset: 0x0007F828
		protected override CompilationOptions CommonWithGeneralDiagnosticOption(ReportDiagnostic value)
		{
			return this.WithGeneralDiagnosticOption(value);
		}

		// Token: 0x0600179A RID: 6042 RVA: 0x00081631 File Offset: 0x0007F831
		protected override CompilationOptions CommonWithSpecificDiagnosticOptions([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ImmutableDictionary<string, ReportDiagnostic> specificDiagnosticOptions)
		{
			return this.WithSpecificDiagnosticOptions(specificDiagnosticOptions);
		}

		// Token: 0x0600179B RID: 6043 RVA: 0x0008163A File Offset: 0x0007F83A
		protected override CompilationOptions CommonWithSpecificDiagnosticOptions([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 0, 1 })] IEnumerable<KeyValuePair<string, ReportDiagnostic>> specificDiagnosticOptions)
		{
			return this.WithSpecificDiagnosticOptions(specificDiagnosticOptions);
		}

		// Token: 0x0600179C RID: 6044 RVA: 0x00081643 File Offset: 0x0007F843
		protected override CompilationOptions CommonWithReportSuppressedDiagnostics(bool reportSuppressedDiagnostics)
		{
			return this.WithReportSuppressedDiagnostics(reportSuppressedDiagnostics);
		}

		// Token: 0x0600179D RID: 6045 RVA: 0x0008164C File Offset: 0x0007F84C
		public new CSharpCompilationOptions WithGeneralDiagnosticOption(ReportDiagnostic value)
		{
			if (base.GeneralDiagnosticOption == value)
			{
				return this;
			}
			return new CSharpCompilationOptions(this)
			{
				GeneralDiagnosticOption = value
			};
		}

		// Token: 0x0600179E RID: 6046 RVA: 0x00081666 File Offset: 0x0007F866
		public new CSharpCompilationOptions WithSpecificDiagnosticOptions([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ImmutableDictionary<string, ReportDiagnostic> values)
		{
			if (values == null)
			{
				values = ImmutableDictionary<string, ReportDiagnostic>.Empty;
			}
			if (base.SpecificDiagnosticOptions == values)
			{
				return this;
			}
			return new CSharpCompilationOptions(this)
			{
				SpecificDiagnosticOptions = values
			};
		}

		// Token: 0x0600179F RID: 6047 RVA: 0x0008168A File Offset: 0x0007F88A
		public new CSharpCompilationOptions WithSpecificDiagnosticOptions([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 0, 1 })] IEnumerable<KeyValuePair<string, ReportDiagnostic>> values)
		{
			return new CSharpCompilationOptions(this)
			{
				SpecificDiagnosticOptions = values.ToImmutableDictionaryOrEmpty<string, ReportDiagnostic>()
			};
		}

		// Token: 0x060017A0 RID: 6048 RVA: 0x0008169E File Offset: 0x0007F89E
		public new CSharpCompilationOptions WithReportSuppressedDiagnostics(bool reportSuppressedDiagnostics)
		{
			if (reportSuppressedDiagnostics == base.ReportSuppressedDiagnostics)
			{
				return this;
			}
			return new CSharpCompilationOptions(this)
			{
				ReportSuppressedDiagnostics = reportSuppressedDiagnostics
			};
		}

		// Token: 0x060017A1 RID: 6049 RVA: 0x000816B8 File Offset: 0x0007F8B8
		public CSharpCompilationOptions WithWarningLevel(int warningLevel)
		{
			if (warningLevel == base.WarningLevel)
			{
				return this;
			}
			return new CSharpCompilationOptions(this)
			{
				WarningLevel = warningLevel
			};
		}

		// Token: 0x060017A2 RID: 6050 RVA: 0x000816D2 File Offset: 0x0007F8D2
		public new CSharpCompilationOptions WithConcurrentBuild(bool concurrentBuild)
		{
			if (concurrentBuild == base.ConcurrentBuild)
			{
				return this;
			}
			return new CSharpCompilationOptions(this)
			{
				ConcurrentBuild = concurrentBuild
			};
		}

		// Token: 0x060017A3 RID: 6051 RVA: 0x000816EC File Offset: 0x0007F8EC
		public new CSharpCompilationOptions WithDeterministic(bool deterministic)
		{
			if (deterministic == base.Deterministic)
			{
				return this;
			}
			return new CSharpCompilationOptions(this)
			{
				Deterministic = deterministic
			};
		}

		// Token: 0x060017A4 RID: 6052 RVA: 0x00081706 File Offset: 0x0007F906
		internal CSharpCompilationOptions WithCurrentLocalTime(DateTime value)
		{
			if (value == base.CurrentLocalTime)
			{
				return this;
			}
			return new CSharpCompilationOptions(this)
			{
				CurrentLocalTime = value
			};
		}

		// Token: 0x060017A5 RID: 6053 RVA: 0x00081725 File Offset: 0x0007F925
		internal CSharpCompilationOptions WithDebugPlusMode(bool debugPlusMode)
		{
			if (debugPlusMode == base.DebugPlusMode)
			{
				return this;
			}
			return new CSharpCompilationOptions(this)
			{
				DebugPlusMode = debugPlusMode
			};
		}

		// Token: 0x060017A6 RID: 6054 RVA: 0x0008173F File Offset: 0x0007F93F
		public new CSharpCompilationOptions WithMetadataImportOptions(MetadataImportOptions value)
		{
			if (value == base.MetadataImportOptions)
			{
				return this;
			}
			return new CSharpCompilationOptions(this)
			{
				MetadataImportOptions = value
			};
		}

		// Token: 0x060017A7 RID: 6055 RVA: 0x00081759 File Offset: 0x0007F959
		internal CSharpCompilationOptions WithReferencesSupersedeLowerVersions(bool value)
		{
			if (value == base.ReferencesSupersedeLowerVersions)
			{
				return this;
			}
			return new CSharpCompilationOptions(this)
			{
				ReferencesSupersedeLowerVersions = value
			};
		}

		// Token: 0x060017A8 RID: 6056 RVA: 0x00081773 File Offset: 0x0007F973
		public new CSharpCompilationOptions WithXmlReferenceResolver([global::System.Runtime.CompilerServices.Nullable(2)] XmlReferenceResolver resolver)
		{
			if (resolver == base.XmlReferenceResolver)
			{
				return this;
			}
			return new CSharpCompilationOptions(this)
			{
				XmlReferenceResolver = resolver
			};
		}

		// Token: 0x060017A9 RID: 6057 RVA: 0x0008178D File Offset: 0x0007F98D
		public new CSharpCompilationOptions WithSourceReferenceResolver([global::System.Runtime.CompilerServices.Nullable(2)] SourceReferenceResolver resolver)
		{
			if (resolver == base.SourceReferenceResolver)
			{
				return this;
			}
			return new CSharpCompilationOptions(this)
			{
				SourceReferenceResolver = resolver
			};
		}

		// Token: 0x060017AA RID: 6058 RVA: 0x000817A7 File Offset: 0x0007F9A7
		public new CSharpCompilationOptions WithSyntaxTreeOptionsProvider([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxTreeOptionsProvider provider)
		{
			if (provider == base.SyntaxTreeOptionsProvider)
			{
				return this;
			}
			return new CSharpCompilationOptions(this)
			{
				SyntaxTreeOptionsProvider = provider
			};
		}

		// Token: 0x060017AB RID: 6059 RVA: 0x000817C1 File Offset: 0x0007F9C1
		public new CSharpCompilationOptions WithMetadataReferenceResolver([global::System.Runtime.CompilerServices.Nullable(2)] MetadataReferenceResolver resolver)
		{
			if (resolver == base.MetadataReferenceResolver)
			{
				return this;
			}
			return new CSharpCompilationOptions(this)
			{
				MetadataReferenceResolver = resolver
			};
		}

		// Token: 0x060017AC RID: 6060 RVA: 0x000817DB File Offset: 0x0007F9DB
		public new CSharpCompilationOptions WithAssemblyIdentityComparer([global::System.Runtime.CompilerServices.Nullable(2)] AssemblyIdentityComparer comparer)
		{
			comparer = comparer ?? AssemblyIdentityComparer.Default;
			if (comparer == base.AssemblyIdentityComparer)
			{
				return this;
			}
			return new CSharpCompilationOptions(this)
			{
				AssemblyIdentityComparer = comparer
			};
		}

		// Token: 0x060017AD RID: 6061 RVA: 0x00081801 File Offset: 0x0007FA01
		public new CSharpCompilationOptions WithStrongNameProvider([global::System.Runtime.CompilerServices.Nullable(2)] StrongNameProvider provider)
		{
			if (provider == base.StrongNameProvider)
			{
				return this;
			}
			return new CSharpCompilationOptions(this)
			{
				StrongNameProvider = provider
			};
		}

		// Token: 0x060017AE RID: 6062 RVA: 0x0008181B File Offset: 0x0007FA1B
		protected override CompilationOptions CommonWithConcurrentBuild(bool concurrent)
		{
			return this.WithConcurrentBuild(concurrent);
		}

		// Token: 0x060017AF RID: 6063 RVA: 0x00081824 File Offset: 0x0007FA24
		protected override CompilationOptions CommonWithDeterministic(bool deterministic)
		{
			return this.WithDeterministic(deterministic);
		}

		// Token: 0x060017B0 RID: 6064 RVA: 0x0008182D File Offset: 0x0007FA2D
		protected override CompilationOptions CommonWithOutputKind(OutputKind kind)
		{
			return this.WithOutputKind(kind);
		}

		// Token: 0x060017B1 RID: 6065 RVA: 0x00081836 File Offset: 0x0007FA36
		protected override CompilationOptions CommonWithPlatform(Platform platform)
		{
			return this.WithPlatform(platform);
		}

		// Token: 0x060017B2 RID: 6066 RVA: 0x0008183F File Offset: 0x0007FA3F
		protected override CompilationOptions CommonWithPublicSign(bool publicSign)
		{
			return this.WithPublicSign(publicSign);
		}

		// Token: 0x060017B3 RID: 6067 RVA: 0x00081848 File Offset: 0x0007FA48
		protected override CompilationOptions CommonWithOptimizationLevel(OptimizationLevel value)
		{
			return this.WithOptimizationLevel(value);
		}

		// Token: 0x060017B4 RID: 6068 RVA: 0x00081851 File Offset: 0x0007FA51
		protected override CompilationOptions CommonWithAssemblyIdentityComparer([global::System.Runtime.CompilerServices.Nullable(2)] AssemblyIdentityComparer comparer)
		{
			return this.WithAssemblyIdentityComparer(comparer);
		}

		// Token: 0x060017B5 RID: 6069 RVA: 0x0008185A File Offset: 0x0007FA5A
		protected override CompilationOptions CommonWithXmlReferenceResolver([global::System.Runtime.CompilerServices.Nullable(2)] XmlReferenceResolver resolver)
		{
			return this.WithXmlReferenceResolver(resolver);
		}

		// Token: 0x060017B6 RID: 6070 RVA: 0x00081863 File Offset: 0x0007FA63
		protected override CompilationOptions CommonWithSourceReferenceResolver([global::System.Runtime.CompilerServices.Nullable(2)] SourceReferenceResolver resolver)
		{
			return this.WithSourceReferenceResolver(resolver);
		}

		// Token: 0x060017B7 RID: 6071 RVA: 0x0008186C File Offset: 0x0007FA6C
		protected override CompilationOptions CommonWithSyntaxTreeOptionsProvider([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxTreeOptionsProvider provider)
		{
			return this.WithSyntaxTreeOptionsProvider(provider);
		}

		// Token: 0x060017B8 RID: 6072 RVA: 0x00081875 File Offset: 0x0007FA75
		protected override CompilationOptions CommonWithMetadataReferenceResolver([global::System.Runtime.CompilerServices.Nullable(2)] MetadataReferenceResolver resolver)
		{
			return this.WithMetadataReferenceResolver(resolver);
		}

		// Token: 0x060017B9 RID: 6073 RVA: 0x0008187E File Offset: 0x0007FA7E
		protected override CompilationOptions CommonWithStrongNameProvider([global::System.Runtime.CompilerServices.Nullable(2)] StrongNameProvider provider)
		{
			return this.WithStrongNameProvider(provider);
		}

		// Token: 0x060017BA RID: 6074 RVA: 0x00081887 File Offset: 0x0007FA87
		protected override CompilationOptions CommonWithMetadataImportOptions(MetadataImportOptions value)
		{
			return this.WithMetadataImportOptions(value);
		}

		// Token: 0x060017BB RID: 6075 RVA: 0x00081890 File Offset: 0x0007FA90
		[Obsolete]
		protected override CompilationOptions CommonWithFeatures([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<string> features)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060017BC RID: 6076 RVA: 0x00081898 File Offset: 0x0007FA98
		internal override void ValidateOptions(ArrayBuilder<Diagnostic> builder)
		{
			base.ValidateOptions(builder, MessageProvider.Instance);
			if (base.MainTypeName != null)
			{
				if (base.OutputKind.IsValid() && !base.OutputKind.IsApplication())
				{
					builder.Add(Diagnostic.Create(MessageProvider.Instance, 2017));
				}
				if (!base.MainTypeName.IsValidClrTypeName())
				{
					builder.Add(Diagnostic.Create(MessageProvider.Instance, 7088, new object[] { "MainTypeName", base.MainTypeName }));
				}
			}
			if (!base.Platform.IsValid())
			{
				builder.Add(Diagnostic.Create(MessageProvider.Instance, 1672, new object[] { base.Platform.ToString() }));
			}
			if (base.ModuleName != null)
			{
				MetadataHelpers.CheckAssemblyOrModuleName(base.ModuleName, MessageProvider.Instance, 7087, builder);
			}
			if (!base.OutputKind.IsValid())
			{
				builder.Add(Diagnostic.Create(MessageProvider.Instance, 7088, new object[]
				{
					"OutputKind",
					base.OutputKind.ToString()
				}));
			}
			if (!base.OptimizationLevel.IsValid())
			{
				builder.Add(Diagnostic.Create(MessageProvider.Instance, 7088, new object[]
				{
					"OptimizationLevel",
					base.OptimizationLevel.ToString()
				}));
			}
			if (base.ScriptClassName == null || !base.ScriptClassName.IsValidClrTypeName())
			{
				builder.Add(Diagnostic.Create(MessageProvider.Instance, 7088, new object[]
				{
					"ScriptClassName",
					base.ScriptClassName ?? "null"
				}));
			}
			if (base.WarningLevel < 0)
			{
				builder.Add(Diagnostic.Create(MessageProvider.Instance, 7088, new object[] { "WarningLevel", base.WarningLevel }));
			}
			if (new ImmutableArray<string>?(this.Usings) != default(ImmutableArray<string>?))
			{
				if (this.Usings.Any<string>((string u) => !u.IsValidClrNamespaceName()))
				{
					CommonMessageProvider instance = MessageProvider.Instance;
					int num = 7088;
					object[] array = new object[2];
					array[0] = "Usings";
					array[1] = Enumerable.First<string>(this.Usings.Where<string>((string u) => !u.IsValidClrNamespaceName())) ?? "null";
					builder.Add(Diagnostic.Create(instance, num, array));
				}
			}
			if (base.Platform == Platform.AnyCpu32BitPreferred && base.OutputKind.IsValid() && base.OutputKind != OutputKind.ConsoleApplication && base.OutputKind != OutputKind.WindowsApplication && base.OutputKind != OutputKind.WindowsRuntimeApplication)
			{
				builder.Add(Diagnostic.Create(MessageProvider.Instance, 4023));
			}
			if (!base.MetadataImportOptions.IsValid())
			{
				builder.Add(Diagnostic.Create(MessageProvider.Instance, 7088, new object[]
				{
					"MetadataImportOptions",
					base.MetadataImportOptions.ToString()
				}));
			}
		}

		// Token: 0x060017BD RID: 6077 RVA: 0x00081BC8 File Offset: 0x0007FDC8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public bool Equals(CSharpCompilationOptions other)
		{
			if (this == other)
			{
				return true;
			}
			if (!base.EqualsHelper(other))
			{
				return false;
			}
			if (this.AllowUnsafe != other.AllowUnsafe || this.TopLevelBinderFlags != other.TopLevelBinderFlags)
			{
				return false;
			}
			if (!(new ImmutableArray<string>?(this.Usings) == default(ImmutableArray<string>?)))
			{
				return this.Usings.SequenceEqual<string, string>(other.Usings, StringComparer.Ordinal) && this.NullableContextOptions == other.NullableContextOptions;
			}
			return new ImmutableArray<string>?(other.Usings) == default(ImmutableArray<string>?);
		}

		// Token: 0x060017BE RID: 6078 RVA: 0x00081C61 File Offset: 0x0007FE61
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as CSharpCompilationOptions);
		}

		// Token: 0x060017BF RID: 6079 RVA: 0x00081C70 File Offset: 0x0007FE70
		public override int GetHashCode()
		{
			return Hash.Combine(base.GetHashCodeHelper(), Hash.Combine(this.AllowUnsafe, Hash.Combine(Hash.CombineValues(this.Usings, StringComparer.Ordinal, int.MaxValue), Hash.Combine(this.TopLevelBinderFlags.GetHashCode(), this.NullableContextOptions.GetHashCode()))));
		}

		// Token: 0x060017C0 RID: 6080 RVA: 0x00081CDF File Offset: 0x0007FEDF
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		internal override Diagnostic FilterDiagnostic(Diagnostic diagnostic, CancellationToken cancellationToken)
		{
			return CSharpDiagnosticFilter.Filter(diagnostic, base.WarningLevel, this.NullableContextOptions, base.GeneralDiagnosticOption, base.SpecificDiagnosticOptions, base.SyntaxTreeOptionsProvider, cancellationToken);
		}

		// Token: 0x060017C1 RID: 6081 RVA: 0x00081D06 File Offset: 0x0007FF06
		protected override CompilationOptions CommonWithModuleName([global::System.Runtime.CompilerServices.Nullable(2)] string moduleName)
		{
			return this.WithModuleName(moduleName);
		}

		// Token: 0x060017C2 RID: 6082 RVA: 0x00081D0F File Offset: 0x0007FF0F
		protected override CompilationOptions CommonWithMainTypeName([global::System.Runtime.CompilerServices.Nullable(2)] string mainTypeName)
		{
			return this.WithMainTypeName(mainTypeName);
		}

		// Token: 0x060017C3 RID: 6083 RVA: 0x00081D18 File Offset: 0x0007FF18
		protected override CompilationOptions CommonWithScriptClassName([global::System.Runtime.CompilerServices.Nullable(2)] string scriptClassName)
		{
			return this.WithScriptClassName(scriptClassName);
		}

		// Token: 0x060017C4 RID: 6084 RVA: 0x00081D21 File Offset: 0x0007FF21
		protected override CompilationOptions CommonWithCryptoKeyContainer([global::System.Runtime.CompilerServices.Nullable(2)] string cryptoKeyContainer)
		{
			return this.WithCryptoKeyContainer(cryptoKeyContainer);
		}

		// Token: 0x060017C5 RID: 6085 RVA: 0x00081D2A File Offset: 0x0007FF2A
		protected override CompilationOptions CommonWithCryptoKeyFile([global::System.Runtime.CompilerServices.Nullable(2)] string cryptoKeyFile)
		{
			return this.WithCryptoKeyFile(cryptoKeyFile);
		}

		// Token: 0x060017C6 RID: 6086 RVA: 0x00081D33 File Offset: 0x0007FF33
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		protected override CompilationOptions CommonWithCryptoPublicKey(ImmutableArray<byte> cryptoPublicKey)
		{
			return this.WithCryptoPublicKey(cryptoPublicKey);
		}

		// Token: 0x060017C7 RID: 6087 RVA: 0x00081D3C File Offset: 0x0007FF3C
		protected override CompilationOptions CommonWithDelaySign(bool? delaySign)
		{
			return this.WithDelaySign(delaySign);
		}

		// Token: 0x060017C8 RID: 6088 RVA: 0x00081D45 File Offset: 0x0007FF45
		protected override CompilationOptions CommonWithCheckOverflow(bool checkOverflow)
		{
			return this.WithOverflowChecks(checkOverflow);
		}

		// Token: 0x060017C9 RID: 6089 RVA: 0x00081D50 File Offset: 0x0007FF50
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[EditorBrowsable(1)]
		public CSharpCompilationOptions(OutputKind outputKind, string moduleName, string mainTypeName, string scriptClassName, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] IEnumerable<string> usings, OptimizationLevel optimizationLevel, bool checkOverflow, bool allowUnsafe, string cryptoKeyContainer, string cryptoKeyFile, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<byte> cryptoPublicKey, bool? delaySign, Platform platform, ReportDiagnostic generalDiagnosticOption, int warningLevel, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 0, 1 })] IEnumerable<KeyValuePair<string, ReportDiagnostic>> specificDiagnosticOptions, bool concurrentBuild, bool deterministic, XmlReferenceResolver xmlReferenceResolver, SourceReferenceResolver sourceReferenceResolver, MetadataReferenceResolver metadataReferenceResolver, AssemblyIdentityComparer assemblyIdentityComparer, StrongNameProvider strongNameProvider)
			: this(outputKind, false, moduleName, mainTypeName, scriptClassName, usings, optimizationLevel, checkOverflow, allowUnsafe, cryptoKeyContainer, cryptoKeyFile, cryptoPublicKey, delaySign, platform, generalDiagnosticOption, warningLevel, specificDiagnosticOptions, concurrentBuild, deterministic, xmlReferenceResolver, sourceReferenceResolver, metadataReferenceResolver, assemblyIdentityComparer, strongNameProvider, false)
		{
		}

		// Token: 0x060017CA RID: 6090 RVA: 0x00081D90 File Offset: 0x0007FF90
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[EditorBrowsable(1)]
		public CSharpCompilationOptions(OutputKind outputKind, string moduleName, string mainTypeName, string scriptClassName, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] IEnumerable<string> usings, OptimizationLevel optimizationLevel, bool checkOverflow, bool allowUnsafe, string cryptoKeyContainer, string cryptoKeyFile, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<byte> cryptoPublicKey, bool? delaySign, Platform platform, ReportDiagnostic generalDiagnosticOption, int warningLevel, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 0, 1 })] IEnumerable<KeyValuePair<string, ReportDiagnostic>> specificDiagnosticOptions, bool concurrentBuild, XmlReferenceResolver xmlReferenceResolver, SourceReferenceResolver sourceReferenceResolver, MetadataReferenceResolver metadataReferenceResolver, AssemblyIdentityComparer assemblyIdentityComparer, StrongNameProvider strongNameProvider)
			: this(outputKind, moduleName, mainTypeName, scriptClassName, usings, optimizationLevel, checkOverflow, allowUnsafe, cryptoKeyContainer, cryptoKeyFile, cryptoPublicKey, delaySign, platform, generalDiagnosticOption, warningLevel, specificDiagnosticOptions, concurrentBuild, false, xmlReferenceResolver, sourceReferenceResolver, metadataReferenceResolver, assemblyIdentityComparer, strongNameProvider)
		{
		}

		// Token: 0x060017CB RID: 6091 RVA: 0x00081DD0 File Offset: 0x0007FFD0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[EditorBrowsable(1)]
		public CSharpCompilationOptions(OutputKind outputKind, bool reportSuppressedDiagnostics, string moduleName, string mainTypeName, string scriptClassName, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] IEnumerable<string> usings, OptimizationLevel optimizationLevel, bool checkOverflow, bool allowUnsafe, string cryptoKeyContainer, string cryptoKeyFile, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<byte> cryptoPublicKey, bool? delaySign, Platform platform, ReportDiagnostic generalDiagnosticOption, int warningLevel, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 0, 1 })] IEnumerable<KeyValuePair<string, ReportDiagnostic>> specificDiagnosticOptions, bool concurrentBuild, bool deterministic, XmlReferenceResolver xmlReferenceResolver, SourceReferenceResolver sourceReferenceResolver, MetadataReferenceResolver metadataReferenceResolver, AssemblyIdentityComparer assemblyIdentityComparer, StrongNameProvider strongNameProvider)
			: this(outputKind, false, moduleName, mainTypeName, scriptClassName, usings, optimizationLevel, checkOverflow, allowUnsafe, cryptoKeyContainer, cryptoKeyFile, cryptoPublicKey, delaySign, platform, generalDiagnosticOption, warningLevel, specificDiagnosticOptions, concurrentBuild, deterministic, default(DateTime), false, xmlReferenceResolver, sourceReferenceResolver, null, metadataReferenceResolver, assemblyIdentityComparer, strongNameProvider, MetadataImportOptions.Public, false, false, BinderFlags.None, NullableContextOptions.Disable)
		{
		}
	}
}
