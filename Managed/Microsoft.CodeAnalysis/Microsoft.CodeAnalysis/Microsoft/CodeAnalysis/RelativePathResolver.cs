using System;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200018F RID: 399
	internal class RelativePathResolver : IEquatable<RelativePathResolver>
	{
		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06001044 RID: 4164 RVA: 0x0002FB38 File Offset: 0x0002DD38
		public ImmutableArray<string> SearchPaths { get; }

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06001045 RID: 4165 RVA: 0x0002FB40 File Offset: 0x0002DD40
		public string BaseDirectory { get; }

		// Token: 0x06001046 RID: 4166 RVA: 0x0002FB48 File Offset: 0x0002DD48
		public RelativePathResolver(ImmutableArray<string> searchPaths, string baseDirectory)
		{
			this.SearchPaths = searchPaths;
			this.BaseDirectory = baseDirectory;
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x0002FB60 File Offset: 0x0002DD60
		public string ResolvePath(string reference, string baseFilePath)
		{
			string text = FileUtilities.ResolveRelativePath(reference, baseFilePath, this.BaseDirectory, this.SearchPaths, new Func<string, bool>(this.FileExists));
			if (text == null)
			{
				return null;
			}
			return FileUtilities.TryNormalizeAbsolutePath(text);
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x0002FB9E File Offset: 0x0002DD9E
		protected virtual bool FileExists(string fullPath)
		{
			return File.Exists(fullPath);
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x0002FBA6 File Offset: 0x0002DDA6
		public RelativePathResolver WithSearchPaths(ImmutableArray<string> searchPaths)
		{
			return new RelativePathResolver(searchPaths, this.BaseDirectory);
		}

		// Token: 0x0600104A RID: 4170 RVA: 0x0002FBB4 File Offset: 0x0002DDB4
		public RelativePathResolver WithBaseDirectory(string baseDirectory)
		{
			return new RelativePathResolver(this.SearchPaths, baseDirectory);
		}

		// Token: 0x0600104B RID: 4171 RVA: 0x0002FBC2 File Offset: 0x0002DDC2
		public bool Equals(RelativePathResolver other)
		{
			return this.BaseDirectory == other.BaseDirectory && this.SearchPaths.SequenceEqual<string, string>(other.SearchPaths, null);
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x0002FBEB File Offset: 0x0002DDEB
		public override int GetHashCode()
		{
			return Hash.Combine<string>(this.BaseDirectory, Hash.CombineValues<string>(this.SearchPaths, int.MaxValue));
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x0002FC08 File Offset: 0x0002DE08
		public override bool Equals(object obj)
		{
			return this.Equals(obj as RelativePathResolver);
		}
	}
}
