using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Cci;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Emit
{
	// Token: 0x0200047C RID: 1148
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class DebugDocumentsBuilder
	{
		// Token: 0x06002D0F RID: 11535 RVA: 0x0007E797 File Offset: 0x0007C997
		[NullableContext(2)]
		public DebugDocumentsBuilder(SourceReferenceResolver resolver, bool isDocumentNameCaseSensitive)
		{
			this._resolver = resolver;
			this._debugDocuments = new ConcurrentDictionary<string, DebugSourceDocument>(isDocumentNameCaseSensitive ? StringComparer.Ordinal : StringComparer.OrdinalIgnoreCase);
			this._normalizedPathsCache = new ConcurrentCache<ValueTuple<string, string>, string>(16);
		}

		// Token: 0x17000D16 RID: 3350
		// (get) Token: 0x06002D10 RID: 11536 RVA: 0x0007E7CD File Offset: 0x0007C9CD
		internal int DebugDocumentCount
		{
			get
			{
				return this._debugDocuments.Count;
			}
		}

		// Token: 0x06002D11 RID: 11537 RVA: 0x0007E7DA File Offset: 0x0007C9DA
		internal void AddDebugDocument(DebugSourceDocument document)
		{
			this._debugDocuments.Add(document.Location, document);
		}

		// Token: 0x17000D17 RID: 3351
		// (get) Token: 0x06002D12 RID: 11538 RVA: 0x0007E7EE File Offset: 0x0007C9EE
		internal IReadOnlyDictionary<string, DebugSourceDocument> DebugDocuments
		{
			get
			{
				return this._debugDocuments;
			}
		}

		// Token: 0x06002D13 RID: 11539 RVA: 0x0007E7F6 File Offset: 0x0007C9F6
		[return: Nullable(2)]
		internal DebugSourceDocument TryGetDebugDocument(string path, string basePath)
		{
			return this.TryGetDebugDocumentForNormalizedPath(this.NormalizeDebugDocumentPath(path, basePath));
		}

		// Token: 0x06002D14 RID: 11540 RVA: 0x0007E808 File Offset: 0x0007CA08
		[return: Nullable(2)]
		internal DebugSourceDocument TryGetDebugDocumentForNormalizedPath(string normalizedPath)
		{
			DebugSourceDocument debugSourceDocument;
			this._debugDocuments.TryGetValue(normalizedPath, ref debugSourceDocument);
			return debugSourceDocument;
		}

		// Token: 0x06002D15 RID: 11541 RVA: 0x0007E825 File Offset: 0x0007CA25
		internal DebugSourceDocument GetOrAddDebugDocument(string path, string basePath, Func<string, DebugSourceDocument> factory)
		{
			return this._debugDocuments.GetOrAdd(this.NormalizeDebugDocumentPath(path, basePath), factory);
		}

		// Token: 0x06002D16 RID: 11542 RVA: 0x0007E83C File Offset: 0x0007CA3C
		internal string NormalizeDebugDocumentPath(string path, [Nullable(2)] string basePath)
		{
			if (this._resolver == null)
			{
				return path;
			}
			ValueTuple<string, string> valueTuple = new ValueTuple<string, string>(path, basePath);
			string text;
			if (!this._normalizedPathsCache.TryGetValue(valueTuple, out text))
			{
				text = this._resolver.NormalizePath(path, basePath) ?? path;
				this._normalizedPathsCache.TryAdd(valueTuple, text);
			}
			return text;
		}

		// Token: 0x040011DC RID: 4572
		private readonly ConcurrentDictionary<string, DebugSourceDocument> _debugDocuments;

		// Token: 0x040011DD RID: 4573
		[Nullable(new byte[] { 1, 0, 1, 2, 1 })]
		private readonly ConcurrentCache<ValueTuple<string, string>, string> _normalizedPathsCache;

		// Token: 0x040011DE RID: 4574
		[Nullable(2)]
		private readonly SourceReferenceResolver _resolver;
	}
}
