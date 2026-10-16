using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000186 RID: 390
	internal sealed class DocumentationCommentIncludeCache : CachingFactory<string, KeyValuePair<string, XDocument>>
	{
		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x0600101F RID: 4127 RVA: 0x0002F107 File Offset: 0x0002D307
		// (set) Token: 0x06001020 RID: 4128 RVA: 0x0002F10E File Offset: 0x0002D30E
		internal static int CacheMissCount { get; private set; }

		// Token: 0x06001021 RID: 4129 RVA: 0x0002F118 File Offset: 0x0002D318
		public DocumentationCommentIncludeCache(XmlReferenceResolver resolver)
			: base(5, (string key) => DocumentationCommentIncludeCache.MakeValue(resolver, key), new Func<string, int>(DocumentationCommentIncludeCache.KeyHashCode), new Func<string, KeyValuePair<string, XDocument>, bool>(DocumentationCommentIncludeCache.KeyValueEquality))
		{
			DocumentationCommentIncludeCache.CacheMissCount = 0;
		}

		// Token: 0x06001022 RID: 4130 RVA: 0x0002F164 File Offset: 0x0002D364
		public XDocument GetOrMakeDocument(string resolvedPath)
		{
			return base.GetOrMakeValue(resolvedPath).Value;
		}

		// Token: 0x06001023 RID: 4131 RVA: 0x0002F180 File Offset: 0x0002D380
		private static KeyValuePair<string, XDocument> MakeValue(XmlReferenceResolver resolver, string resolvedPath)
		{
			DocumentationCommentIncludeCache.CacheMissCount++;
			KeyValuePair<string, XDocument> keyValuePair;
			using (Stream stream = resolver.OpenReadChecked(resolvedPath))
			{
				using (XmlReader xmlReader = XmlReader.Create(stream, DocumentationCommentIncludeCache.s_xmlSettings))
				{
					XDocument xdocument = XDocument.Load(xmlReader, 5);
					keyValuePair = KeyValuePairUtil.Create<string, XDocument>(resolvedPath, xdocument);
				}
			}
			return keyValuePair;
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x0002F1F0 File Offset: 0x0002D3F0
		private static int KeyHashCode(string resolvedPath)
		{
			return resolvedPath.GetHashCode();
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x0002F1F8 File Offset: 0x0002D3F8
		private static bool KeyValueEquality(string resolvedPath, KeyValuePair<string, XDocument> pathAndDocument)
		{
			return resolvedPath == pathAndDocument.Key;
		}

		// Token: 0x04000475 RID: 1141
		private const int Size = 5;

		// Token: 0x04000477 RID: 1143
		private static readonly XmlReaderSettings s_xmlSettings = new XmlReaderSettings
		{
			DtdProcessing = 0
		};
	}
}
