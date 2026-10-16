using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000120 RID: 288
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class CSharpParseOptions : ParseOptions, IEquatable<CSharpParseOptions>
	{
		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x0600183C RID: 6204 RVA: 0x00082D0B File Offset: 0x00080F0B
		public static CSharpParseOptions Default { get; } = new CSharpParseOptions(LanguageVersion.Default, DocumentationMode.Parse, SourceCodeKind.Regular, null);

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x0600183D RID: 6205 RVA: 0x00082D12 File Offset: 0x00080F12
		// (set) Token: 0x0600183E RID: 6206 RVA: 0x00082D1A File Offset: 0x00080F1A
		public LanguageVersion LanguageVersion { get; private set; }

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x0600183F RID: 6207 RVA: 0x00082D23 File Offset: 0x00080F23
		// (set) Token: 0x06001840 RID: 6208 RVA: 0x00082D2B File Offset: 0x00080F2B
		public LanguageVersion SpecifiedLanguageVersion { get; private set; }

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06001841 RID: 6209 RVA: 0x00082D34 File Offset: 0x00080F34
		// (set) Token: 0x06001842 RID: 6210 RVA: 0x00082D3C File Offset: 0x00080F3C
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal ImmutableArray<string> PreprocessorSymbols
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
			[param: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			private set;
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06001843 RID: 6211 RVA: 0x00082D45 File Offset: 0x00080F45
		public override IEnumerable<string> PreprocessorSymbolNames
		{
			get
			{
				return this.PreprocessorSymbols;
			}
		}

		// Token: 0x06001844 RID: 6212 RVA: 0x00082D52 File Offset: 0x00080F52
		public CSharpParseOptions(LanguageVersion languageVersion = LanguageVersion.Default, DocumentationMode documentationMode = DocumentationMode.Parse, SourceCodeKind kind = SourceCodeKind.Regular, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] IEnumerable<string> preprocessorSymbols = null)
			: this(languageVersion, documentationMode, kind, preprocessorSymbols.ToImmutableArrayOrEmpty<string>(), ImmutableDictionary<string, string>.Empty)
		{
		}

		// Token: 0x06001845 RID: 6213 RVA: 0x00082D6C File Offset: 0x00080F6C
		internal CSharpParseOptions(LanguageVersion languageVersion, DocumentationMode documentationMode, SourceCodeKind kind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<string> preprocessorSymbols, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1, 1 })] IReadOnlyDictionary<string, string> features)
			: base(kind, documentationMode)
		{
			this.SpecifiedLanguageVersion = languageVersion;
			this.LanguageVersion = languageVersion.MapSpecifiedToEffectiveVersion();
			this.PreprocessorSymbols = preprocessorSymbols.ToImmutableArrayOrEmpty<string>();
			this._features = ((features != null) ? features.ToImmutableDictionary<string, string>() : null) ?? ImmutableDictionary<string, string>.Empty;
		}

		// Token: 0x06001846 RID: 6214 RVA: 0x00082DBE File Offset: 0x00080FBE
		private CSharpParseOptions(CSharpParseOptions other)
			: this(other.SpecifiedLanguageVersion, other.DocumentationMode, other.Kind, other.PreprocessorSymbols, other.Features)
		{
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06001847 RID: 6215 RVA: 0x00082DE4 File Offset: 0x00080FE4
		public override string Language
		{
			get
			{
				return "C#";
			}
		}

		// Token: 0x06001848 RID: 6216 RVA: 0x00082DEC File Offset: 0x00080FEC
		public new CSharpParseOptions WithKind(SourceCodeKind kind)
		{
			if (kind == base.SpecifiedKind)
			{
				return this;
			}
			SourceCodeKind sourceCodeKind = kind.MapSpecifiedToEffectiveKind();
			return new CSharpParseOptions(this)
			{
				SpecifiedKind = kind,
				Kind = sourceCodeKind
			};
		}

		// Token: 0x06001849 RID: 6217 RVA: 0x00082E20 File Offset: 0x00081020
		public CSharpParseOptions WithLanguageVersion(LanguageVersion version)
		{
			if (version == this.SpecifiedLanguageVersion)
			{
				return this;
			}
			LanguageVersion languageVersion = version.MapSpecifiedToEffectiveVersion();
			return new CSharpParseOptions(this)
			{
				SpecifiedLanguageVersion = version,
				LanguageVersion = languageVersion
			};
		}

		// Token: 0x0600184A RID: 6218 RVA: 0x00082E53 File Offset: 0x00081053
		public CSharpParseOptions WithPreprocessorSymbols([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] IEnumerable<string> preprocessorSymbols)
		{
			return this.WithPreprocessorSymbols(preprocessorSymbols.AsImmutableOrNull<string>());
		}

		// Token: 0x0600184B RID: 6219 RVA: 0x00082E61 File Offset: 0x00081061
		public CSharpParseOptions WithPreprocessorSymbols([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] params string[] preprocessorSymbols)
		{
			return this.WithPreprocessorSymbols(preprocessorSymbols.AsImmutableOrNull<string>());
		}

		// Token: 0x0600184C RID: 6220 RVA: 0x00082E6F File Offset: 0x0008106F
		public CSharpParseOptions WithPreprocessorSymbols([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<string> symbols)
		{
			if (symbols.IsDefault)
			{
				symbols = ImmutableArray<string>.Empty;
			}
			if (symbols.Equals(this.PreprocessorSymbols))
			{
				return this;
			}
			return new CSharpParseOptions(this)
			{
				PreprocessorSymbols = symbols
			};
		}

		// Token: 0x0600184D RID: 6221 RVA: 0x00082E9F File Offset: 0x0008109F
		public new CSharpParseOptions WithDocumentationMode(DocumentationMode documentationMode)
		{
			if (documentationMode == base.DocumentationMode)
			{
				return this;
			}
			return new CSharpParseOptions(this)
			{
				DocumentationMode = documentationMode
			};
		}

		// Token: 0x0600184E RID: 6222 RVA: 0x00082EB9 File Offset: 0x000810B9
		public override ParseOptions CommonWithKind(SourceCodeKind kind)
		{
			return this.WithKind(kind);
		}

		// Token: 0x0600184F RID: 6223 RVA: 0x00082EC2 File Offset: 0x000810C2
		protected override ParseOptions CommonWithDocumentationMode(DocumentationMode documentationMode)
		{
			return this.WithDocumentationMode(documentationMode);
		}

		// Token: 0x06001850 RID: 6224 RVA: 0x00082ECB File Offset: 0x000810CB
		protected override ParseOptions CommonWithFeatures([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 0, 1, 1 })] IEnumerable<KeyValuePair<string, string>> features)
		{
			return this.WithFeatures(features);
		}

		// Token: 0x06001851 RID: 6225 RVA: 0x00082ED4 File Offset: 0x000810D4
		public new CSharpParseOptions WithFeatures([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 0, 1, 1 })] IEnumerable<KeyValuePair<string, string>> features)
		{
			ImmutableDictionary<string, string> immutableDictionary = ((features != null) ? features.ToImmutableDictionary<string, string>(StringComparer.OrdinalIgnoreCase) : null) ?? ImmutableDictionary<string, string>.Empty;
			return new CSharpParseOptions(this)
			{
				_features = immutableDictionary
			};
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06001852 RID: 6226 RVA: 0x00082F09 File Offset: 0x00081109
		public override IReadOnlyDictionary<string, string> Features
		{
			get
			{
				return this._features;
			}
		}

		// Token: 0x06001853 RID: 6227 RVA: 0x00082F14 File Offset: 0x00081114
		internal override void ValidateOptions(ArrayBuilder<Diagnostic> builder)
		{
			base.ValidateOptions(builder, MessageProvider.Instance);
			if (!this.LanguageVersion.IsValid())
			{
				builder.Add(Diagnostic.Create(MessageProvider.Instance, 8192, new object[] { this.LanguageVersion.ToString() }));
			}
			if (!this.PreprocessorSymbols.IsDefaultOrEmpty)
			{
				foreach (string text in this.PreprocessorSymbols)
				{
					if (text == null)
					{
						builder.Add(Diagnostic.Create(MessageProvider.Instance, 8301, new object[] { "null" }));
					}
					else if (!SyntaxFacts.IsValidIdentifier(text))
					{
						builder.Add(Diagnostic.Create(MessageProvider.Instance, 8301, new object[] { text }));
					}
				}
			}
		}

		// Token: 0x06001854 RID: 6228 RVA: 0x00082FF0 File Offset: 0x000811F0
		internal bool IsFeatureEnabled(MessageID feature)
		{
			string text = feature.RequiredFeature();
			if (text != null)
			{
				return this.Features.ContainsKey(text);
			}
			LanguageVersion languageVersion = this.LanguageVersion;
			LanguageVersion languageVersion2 = feature.RequiredVersion();
			return languageVersion >= languageVersion2;
		}

		// Token: 0x06001855 RID: 6229 RVA: 0x00083027 File Offset: 0x00081227
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as CSharpParseOptions);
		}

		// Token: 0x06001856 RID: 6230 RVA: 0x00083035 File Offset: 0x00081235
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public bool Equals(CSharpParseOptions other)
		{
			return this == other || (base.EqualsHelper(other) && this.SpecifiedLanguageVersion == other.SpecifiedLanguageVersion);
		}

		// Token: 0x06001857 RID: 6231 RVA: 0x00083056 File Offset: 0x00081256
		public override int GetHashCode()
		{
			return Hash.Combine(base.GetHashCodeHelper(), Hash.Combine((int)this.SpecifiedLanguageVersion, 0));
		}

		// Token: 0x0400068C RID: 1676
		private ImmutableDictionary<string, string> _features;
	}
}
