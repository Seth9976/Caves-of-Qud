using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200014B RID: 331
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class ParseOptions
	{
		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000D2F RID: 3375 RVA: 0x000270C9 File Offset: 0x000252C9
		// (set) Token: 0x06000D30 RID: 3376 RVA: 0x000270D1 File Offset: 0x000252D1
		public SourceCodeKind Kind { get; protected set; }

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000D31 RID: 3377 RVA: 0x000270DA File Offset: 0x000252DA
		// (set) Token: 0x06000D32 RID: 3378 RVA: 0x000270E2 File Offset: 0x000252E2
		public SourceCodeKind SpecifiedKind { get; protected set; }

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000D33 RID: 3379 RVA: 0x000270EB File Offset: 0x000252EB
		// (set) Token: 0x06000D34 RID: 3380 RVA: 0x000270F3 File Offset: 0x000252F3
		public DocumentationMode DocumentationMode { get; protected set; }

		// Token: 0x06000D35 RID: 3381 RVA: 0x000270FC File Offset: 0x000252FC
		internal ParseOptions(SourceCodeKind kind, DocumentationMode documentationMode)
		{
			this.SpecifiedKind = kind;
			this.Kind = kind.MapSpecifiedToEffectiveKind();
			this.DocumentationMode = documentationMode;
			this._lazyErrors = new Lazy<ImmutableArray<Diagnostic>>(delegate
			{
				ArrayBuilder<Diagnostic> instance = ArrayBuilder<Diagnostic>.GetInstance();
				this.ValidateOptions(instance);
				return instance.ToImmutableAndFree();
			});
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000D36 RID: 3382
		public abstract string Language { get; }

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000D37 RID: 3383 RVA: 0x00027135 File Offset: 0x00025335
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<Diagnostic> Errors
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._lazyErrors.Value;
			}
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x00027142 File Offset: 0x00025342
		public ParseOptions WithKind(SourceCodeKind kind)
		{
			return this.CommonWithKind(kind);
		}

		// Token: 0x06000D39 RID: 3385
		internal abstract void ValidateOptions(ArrayBuilder<Diagnostic> builder);

		// Token: 0x06000D3A RID: 3386 RVA: 0x0002714C File Offset: 0x0002534C
		internal void ValidateOptions(ArrayBuilder<Diagnostic> builder, CommonMessageProvider messageProvider)
		{
			if (!this.SpecifiedKind.IsValid())
			{
				builder.Add(messageProvider.CreateDiagnostic(messageProvider.ERR_BadSourceCodeKind, Location.None, new object[] { this.SpecifiedKind.ToString() }));
			}
			if (!this.DocumentationMode.IsValid())
			{
				builder.Add(messageProvider.CreateDiagnostic(messageProvider.ERR_BadDocumentationMode, Location.None, new object[] { this.DocumentationMode.ToString() }));
			}
		}

		// Token: 0x06000D3B RID: 3387
		[EditorBrowsable(1)]
		public abstract ParseOptions CommonWithKind(SourceCodeKind kind);

		// Token: 0x06000D3C RID: 3388 RVA: 0x000271DB File Offset: 0x000253DB
		public ParseOptions WithDocumentationMode(DocumentationMode documentationMode)
		{
			return this.CommonWithDocumentationMode(documentationMode);
		}

		// Token: 0x06000D3D RID: 3389
		protected abstract ParseOptions CommonWithDocumentationMode(DocumentationMode documentationMode);

		// Token: 0x06000D3E RID: 3390 RVA: 0x000271E4 File Offset: 0x000253E4
		public ParseOptions WithFeatures([Nullable(new byte[] { 1, 0, 1, 1 })] IEnumerable<KeyValuePair<string, string>> features)
		{
			return this.CommonWithFeatures(features);
		}

		// Token: 0x06000D3F RID: 3391
		protected abstract ParseOptions CommonWithFeatures([Nullable(new byte[] { 1, 0, 1, 1 })] IEnumerable<KeyValuePair<string, string>> features);

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000D40 RID: 3392
		public abstract IReadOnlyDictionary<string, string> Features { get; }

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000D41 RID: 3393
		public abstract IEnumerable<string> PreprocessorSymbolNames { get; }

		// Token: 0x06000D42 RID: 3394
		[NullableContext(2)]
		public abstract override bool Equals(object obj);

		// Token: 0x06000D43 RID: 3395 RVA: 0x000271F0 File Offset: 0x000253F0
		[NullableContext(2)]
		protected bool EqualsHelper([NotNullWhen(true)] ParseOptions other)
		{
			if (other == null)
			{
				return false;
			}
			if (this.SpecifiedKind != other.SpecifiedKind || this.DocumentationMode != other.DocumentationMode || !Enumerable.SequenceEqual<KeyValuePair<string, string>>(this.Features, other.Features))
			{
				return false;
			}
			if (this.PreprocessorSymbolNames != null)
			{
				return Enumerable.SequenceEqual<string>(this.PreprocessorSymbolNames, other.PreprocessorSymbolNames, StringComparer.Ordinal);
			}
			return other.PreprocessorSymbolNames == null;
		}

		// Token: 0x06000D44 RID: 3396
		public abstract override int GetHashCode();

		// Token: 0x06000D45 RID: 3397 RVA: 0x0002725C File Offset: 0x0002545C
		protected int GetHashCodeHelper()
		{
			return Hash.Combine((int)this.SpecifiedKind, Hash.Combine((int)this.DocumentationMode, Hash.Combine(ParseOptions.HashFeatures(this.Features), Hash.Combine(Hash.CombineValues(this.PreprocessorSymbolNames, StringComparer.Ordinal, int.MaxValue), 0))));
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x000272AC File Offset: 0x000254AC
		private static int HashFeatures(IReadOnlyDictionary<string, string> features)
		{
			int num = 0;
			foreach (KeyValuePair<string, string> keyValuePair in features)
			{
				num = Hash.Combine(keyValuePair.Key.GetHashCode(), Hash.Combine(keyValuePair.Value.GetHashCode(), num));
			}
			return num;
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x00027314 File Offset: 0x00025514
		[NullableContext(2)]
		public static bool operator ==(ParseOptions left, ParseOptions right)
		{
			return object.Equals(left, right);
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x0002731D File Offset: 0x0002551D
		[NullableContext(2)]
		public static bool operator !=(ParseOptions left, ParseOptions right)
		{
			return !object.Equals(left, right);
		}

		// Token: 0x040003A5 RID: 933
		[Nullable(new byte[] { 1, 0, 1 })]
		private readonly Lazy<ImmutableArray<Diagnostic>> _lazyErrors;
	}
}
