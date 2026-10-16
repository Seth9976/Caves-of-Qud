using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000131 RID: 305
	[NullableContext(1)]
	[Nullable(0)]
	internal abstract class SarifErrorLogger : ErrorLogger, IDisposable
	{
		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000B5B RID: 2907 RVA: 0x0002241A File Offset: 0x0002061A
		protected JsonWriter _writer { get; }

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000B5C RID: 2908 RVA: 0x00022422 File Offset: 0x00020622
		protected CultureInfo _culture { get; }

		// Token: 0x06000B5D RID: 2909 RVA: 0x0002242A File Offset: 0x0002062A
		protected SarifErrorLogger(Stream stream, CultureInfo culture)
		{
			this._writer = new JsonWriter(new StreamWriter(stream));
			this._culture = culture;
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000B5E RID: 2910
		protected abstract string PrimaryLocationPropertyName { get; }

		// Token: 0x06000B5F RID: 2911
		protected abstract void WritePhysicalLocation(Location diagnosticLocation);

		// Token: 0x06000B60 RID: 2912 RVA: 0x0002244A File Offset: 0x0002064A
		public virtual void Dispose()
		{
			this._writer.Dispose();
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x00022458 File Offset: 0x00020658
		protected void WriteRegion(FileLinePositionSpan span)
		{
			this._writer.WriteObjectStart("region");
			this._writer.Write("startLine", span.StartLinePosition.Line + 1);
			this._writer.Write("startColumn", span.StartLinePosition.Character + 1);
			this._writer.Write("endLine", span.EndLinePosition.Line + 1);
			this._writer.Write("endColumn", span.EndLinePosition.Character + 1);
			this._writer.WriteObjectEnd();
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x00022504 File Offset: 0x00020704
		protected static string GetLevel(DiagnosticSeverity severity)
		{
			switch (severity)
			{
			case DiagnosticSeverity.Info:
				return "note";
			case DiagnosticSeverity.Error:
				return "error";
			}
			return "warning";
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x00022530 File Offset: 0x00020730
		protected void WriteResultProperties(Diagnostic diagnostic)
		{
			if (diagnostic.WarningLevel > 0 || diagnostic.Properties.Count > 0)
			{
				this._writer.WriteObjectStart("properties");
				if (diagnostic.WarningLevel > 0)
				{
					this._writer.Write("warningLevel", diagnostic.WarningLevel);
				}
				if (diagnostic.Properties.Count > 0)
				{
					this._writer.WriteObjectStart("customProperties");
					foreach (KeyValuePair<string, string> keyValuePair in Enumerable.OrderBy<KeyValuePair<string, string>, string>(diagnostic.Properties, (KeyValuePair<string, string> x) => x.Key, StringComparer.Ordinal))
					{
						this._writer.Write(keyValuePair.Key, keyValuePair.Value);
					}
					this._writer.WriteObjectEnd();
				}
				this._writer.WriteObjectEnd();
			}
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x00022638 File Offset: 0x00020838
		protected static bool HasPath(Location location)
		{
			return !string.IsNullOrEmpty(location.GetLineSpan().Path);
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x0002265C File Offset: 0x0002085C
		protected static string GetUri(string path)
		{
			if (Path.IsPathRooted(path))
			{
				Uri uri;
				if (Uri.TryCreate(Path.GetFullPath(path), 1, ref uri))
				{
					return uri.AbsoluteUri;
				}
			}
			else
			{
				if (!PathUtilities.IsUnixLikePlatform)
				{
					path = path.Replace("\\\\", "\\");
					path = PathUtilities.NormalizeWithForwardSlash(path);
				}
				Uri uri2;
				if (Uri.TryCreate(path, 2, ref uri2))
				{
					return SarifErrorLogger.s_fileRoot.MakeRelativeUri(new Uri(SarifErrorLogger.s_fileRoot, uri2)).ToString();
				}
			}
			return WebUtility.UrlEncode(path);
		}

		// Token: 0x04000337 RID: 823
		private static readonly Uri s_fileRoot = new Uri("file:///");
	}
}
