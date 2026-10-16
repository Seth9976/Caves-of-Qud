using System;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000FC RID: 252
	internal sealed class CommandLineDiagnosticFormatter : CSharpDiagnosticFormatter
	{
		// Token: 0x06001321 RID: 4897 RVA: 0x0006503C File Offset: 0x0006323C
		internal CommandLineDiagnosticFormatter(string baseDirectory, bool displayFullPaths, bool displayEndLocations)
		{
			this._baseDirectory = baseDirectory;
			this._displayFullPaths = displayFullPaths;
			this._displayEndLocations = displayEndLocations;
			this._lazyNormalizedBaseDirectory = new Lazy<string>(() => FileUtilities.TryNormalizeAbsolutePath(baseDirectory));
		}

		// Token: 0x06001322 RID: 4898 RVA: 0x00065090 File Offset: 0x00063290
		internal override string FormatSourceSpan(LinePositionSpan span, IFormatProvider formatter)
		{
			if (this._displayEndLocations)
			{
				return string.Format(formatter, "({0},{1},{2},{3})", new object[]
				{
					span.Start.Line + 1,
					span.Start.Character + 1,
					span.End.Line + 1,
					span.End.Character + 1
				});
			}
			return string.Format(formatter, "({0},{1})", span.Start.Line + 1, span.Start.Character + 1);
		}

		// Token: 0x06001323 RID: 4899 RVA: 0x00065154 File Offset: 0x00063354
		internal override string FormatSourcePath(string path, string basePath, IFormatProvider formatter)
		{
			string text = FileUtilities.NormalizeRelativePath(path, basePath, this._baseDirectory);
			if (text == null)
			{
				return path;
			}
			if (!this._displayFullPaths)
			{
				return this.RelativizeNormalizedPath(text);
			}
			return text;
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x00065188 File Offset: 0x00063388
		internal string RelativizeNormalizedPath(string normalizedPath)
		{
			string value = this._lazyNormalizedBaseDirectory.Value;
			if (value == null)
			{
				return normalizedPath;
			}
			if (PathUtilities.IsSameDirectoryOrChildOf(PathUtilities.GetDirectoryName(normalizedPath), value))
			{
				return normalizedPath.Substring(PathUtilities.IsDirectorySeparator(value.Last()) ? value.Length : (value.Length + 1));
			}
			return normalizedPath;
		}

		// Token: 0x040005E7 RID: 1511
		private readonly string _baseDirectory;

		// Token: 0x040005E8 RID: 1512
		private readonly Lazy<string> _lazyNormalizedBaseDirectory;

		// Token: 0x040005E9 RID: 1513
		private readonly bool _displayFullPaths;

		// Token: 0x040005EA RID: 1514
		private readonly bool _displayEndLocations;
	}
}
