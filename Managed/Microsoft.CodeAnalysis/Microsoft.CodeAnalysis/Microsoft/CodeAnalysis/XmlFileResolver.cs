using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000294 RID: 660
	[NullableContext(2)]
	[Nullable(0)]
	public class XmlFileResolver : XmlReferenceResolver
	{
		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x06001B0A RID: 6922 RVA: 0x00052014 File Offset: 0x00050214
		[Nullable(1)]
		public static XmlFileResolver Default
		{
			[NullableContext(1)]
			get;
		} = new XmlFileResolver(null);

		// Token: 0x06001B0B RID: 6923 RVA: 0x0005201B File Offset: 0x0005021B
		public XmlFileResolver(string baseDirectory)
		{
			if (baseDirectory != null && PathUtilities.GetPathKind(baseDirectory) != PathKind.Absolute)
			{
				throw new ArgumentException(CodeAnalysisResources.AbsolutePathExpected, "baseDirectory");
			}
			this._baseDirectory = baseDirectory;
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06001B0C RID: 6924 RVA: 0x00052046 File Offset: 0x00050246
		public string BaseDirectory
		{
			get
			{
				return this._baseDirectory;
			}
		}

		// Token: 0x06001B0D RID: 6925 RVA: 0x00052050 File Offset: 0x00050250
		public override string ResolveReference([Nullable(1)] string path, string baseFilePath)
		{
			if (baseFilePath != null)
			{
				string text = FileUtilities.ResolveRelativePath(path, baseFilePath, this._baseDirectory);
				if (this.FileExists(text))
				{
					return FileUtilities.TryNormalizeAbsolutePath(text);
				}
			}
			if (this._baseDirectory != null)
			{
				string text = FileUtilities.ResolveRelativePath(path, this._baseDirectory);
				if (this.FileExists(text))
				{
					return FileUtilities.TryNormalizeAbsolutePath(text);
				}
			}
			return null;
		}

		// Token: 0x06001B0E RID: 6926 RVA: 0x000520A4 File Offset: 0x000502A4
		[NullableContext(1)]
		public override Stream OpenRead(string resolvedPath)
		{
			CompilerPathUtilities.RequireAbsolutePath(resolvedPath, "resolvedPath");
			return FileUtilities.OpenRead(resolvedPath);
		}

		// Token: 0x06001B0F RID: 6927 RVA: 0x000520B7 File Offset: 0x000502B7
		protected virtual bool FileExists([NotNullWhen(true)] string resolvedPath)
		{
			return File.Exists(resolvedPath);
		}

		// Token: 0x06001B10 RID: 6928 RVA: 0x000520C0 File Offset: 0x000502C0
		public override bool Equals(object obj)
		{
			if (obj == null || base.GetType() != obj.GetType())
			{
				return false;
			}
			XmlFileResolver xmlFileResolver = (XmlFileResolver)obj;
			return string.Equals(this._baseDirectory, xmlFileResolver._baseDirectory, 4);
		}

		// Token: 0x06001B11 RID: 6929 RVA: 0x000520FE File Offset: 0x000502FE
		public override int GetHashCode()
		{
			if (this._baseDirectory == null)
			{
				return 0;
			}
			return StringComparer.Ordinal.GetHashCode(this._baseDirectory);
		}

		// Token: 0x04000D2C RID: 3372
		private readonly string _baseDirectory;
	}
}
