using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001D7 RID: 471
	[NullableContext(2)]
	[Nullable(0)]
	public class SourceFileResolver : SourceReferenceResolver, IEquatable<SourceFileResolver>
	{
		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x060013A2 RID: 5026 RVA: 0x0003D8A9 File Offset: 0x0003BAA9
		[Nullable(1)]
		public static SourceFileResolver Default
		{
			[NullableContext(1)]
			get;
		} = new SourceFileResolver(ImmutableArray<string>.Empty, null);

		// Token: 0x060013A3 RID: 5027 RVA: 0x0003D8B0 File Offset: 0x0003BAB0
		[NullableContext(1)]
		public SourceFileResolver(IEnumerable<string> searchPaths, [Nullable(2)] string baseDirectory)
			: this(searchPaths.AsImmutableOrNull<string>(), baseDirectory)
		{
		}

		// Token: 0x060013A4 RID: 5028 RVA: 0x0003D8BF File Offset: 0x0003BABF
		public SourceFileResolver([Nullable(new byte[] { 0, 1 })] ImmutableArray<string> searchPaths, string baseDirectory)
			: this(searchPaths, baseDirectory, ImmutableArray<KeyValuePair<string, string>>.Empty)
		{
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x0003D8D0 File Offset: 0x0003BAD0
		public SourceFileResolver([Nullable(new byte[] { 0, 1 })] ImmutableArray<string> searchPaths, string baseDirectory, [Nullable(new byte[] { 0, 0, 1, 1 })] ImmutableArray<KeyValuePair<string, string>> pathMap)
		{
			if (searchPaths.IsDefault)
			{
				throw new ArgumentNullException("searchPaths");
			}
			if (baseDirectory != null && PathUtilities.GetPathKind(baseDirectory) != PathKind.Absolute)
			{
				throw new ArgumentException(CodeAnalysisResources.AbsolutePathExpected, "baseDirectory");
			}
			this._baseDirectory = baseDirectory;
			this._searchPaths = searchPaths;
			if (!pathMap.IsDefaultOrEmpty)
			{
				ArrayBuilder<KeyValuePair<string, string>> instance = ArrayBuilder<KeyValuePair<string, string>>.GetInstance(pathMap.Length);
				foreach (KeyValuePair<string, string> keyValuePair in pathMap)
				{
					string text;
					string text2;
					keyValuePair.Deconstruct<string, string>(out text, out text2);
					string text3 = text;
					string text4 = text2;
					if (text3 == null || text3.Length == 0)
					{
						throw new ArgumentException(CodeAnalysisResources.EmptyKeyInPathMap, "pathMap");
					}
					if (text4 == null)
					{
						throw new ArgumentException(CodeAnalysisResources.NullValueInPathMap, "pathMap");
					}
					string text5 = PathUtilities.EnsureTrailingSeparator(text3);
					string text6 = PathUtilities.EnsureTrailingSeparator(text4);
					instance.Add(new KeyValuePair<string, string>(text5, text6));
				}
				this._pathMap = instance.ToImmutableAndFree();
				return;
			}
			this._pathMap = ImmutableArray<KeyValuePair<string, string>>.Empty;
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x060013A6 RID: 5030 RVA: 0x0003D9C5 File Offset: 0x0003BBC5
		public string BaseDirectory
		{
			get
			{
				return this._baseDirectory;
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x060013A7 RID: 5031 RVA: 0x0003D9CD File Offset: 0x0003BBCD
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<string> SearchPaths
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._searchPaths;
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x060013A8 RID: 5032 RVA: 0x0003D9D5 File Offset: 0x0003BBD5
		[Nullable(new byte[] { 0, 0, 1, 1 })]
		public ImmutableArray<KeyValuePair<string, string>> PathMap
		{
			[return: Nullable(new byte[] { 0, 0, 1, 1 })]
			get
			{
				return this._pathMap;
			}
		}

		// Token: 0x060013A9 RID: 5033 RVA: 0x0003D9E0 File Offset: 0x0003BBE0
		public override string NormalizePath([Nullable(1)] string path, string baseFilePath)
		{
			string text = FileUtilities.NormalizeRelativePath(path, baseFilePath, this._baseDirectory);
			if (text != null && !this._pathMap.IsDefaultOrEmpty)
			{
				return PathUtilities.NormalizePathPrefix(text, this._pathMap);
			}
			return text;
		}

		// Token: 0x060013AA RID: 5034 RVA: 0x0003DA1C File Offset: 0x0003BC1C
		public override string ResolveReference([Nullable(1)] string path, string baseFilePath)
		{
			string text = FileUtilities.ResolveRelativePath(path, baseFilePath, this._baseDirectory, this._searchPaths, new Func<string, bool>(this.FileExists));
			if (text == null)
			{
				return null;
			}
			return FileUtilities.TryNormalizeAbsolutePath(text);
		}

		// Token: 0x060013AB RID: 5035 RVA: 0x0003DA5A File Offset: 0x0003BC5A
		[NullableContext(1)]
		public override Stream OpenRead(string resolvedPath)
		{
			CompilerPathUtilities.RequireAbsolutePath(resolvedPath, "resolvedPath");
			return FileUtilities.OpenRead(resolvedPath);
		}

		// Token: 0x060013AC RID: 5036 RVA: 0x0003DA6D File Offset: 0x0003BC6D
		protected virtual bool FileExists([NotNullWhen(true)] string resolvedPath)
		{
			return File.Exists(resolvedPath);
		}

		// Token: 0x060013AD RID: 5037 RVA: 0x0003DA75 File Offset: 0x0003BC75
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((SourceFileResolver)obj);
		}

		// Token: 0x060013AE RID: 5038 RVA: 0x0003DA9C File Offset: 0x0003BC9C
		public bool Equals(SourceFileResolver other)
		{
			return other != null && (string.Equals(this._baseDirectory, other._baseDirectory, 4) && this._searchPaths.SequenceEqual<string, string>(other._searchPaths, StringComparer.Ordinal)) && this._pathMap.SequenceEqual<KeyValuePair<string, string>, KeyValuePair<string, string>>(other._pathMap, null);
		}

		// Token: 0x060013AF RID: 5039 RVA: 0x0003DAF0 File Offset: 0x0003BCF0
		public override int GetHashCode()
		{
			return Hash.Combine((this._baseDirectory != null) ? StringComparer.Ordinal.GetHashCode(this._baseDirectory) : 0, Hash.Combine(Hash.CombineValues(this._searchPaths, StringComparer.Ordinal, int.MaxValue), Hash.CombineValues<KeyValuePair<string, string>>(this._pathMap, int.MaxValue)));
		}

		// Token: 0x040005F3 RID: 1523
		private readonly string _baseDirectory;

		// Token: 0x040005F4 RID: 1524
		[Nullable(new byte[] { 0, 1 })]
		private readonly ImmutableArray<string> _searchPaths;

		// Token: 0x040005F5 RID: 1525
		[Nullable(new byte[] { 0, 0, 1, 1 })]
		private readonly ImmutableArray<KeyValuePair<string, string>> _pathMap;
	}
}
