using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001D8 RID: 472
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class AdditionalSourcesCollection
	{
		// Token: 0x060013B1 RID: 5041 RVA: 0x0003DB5E File Offset: 0x0003BD5E
		internal AdditionalSourcesCollection(string fileExtension)
		{
			this._sourcesAdded = ArrayBuilder<GeneratedSourceText>.GetInstance();
			this._fileExtension = fileExtension;
		}

		// Token: 0x060013B2 RID: 5042 RVA: 0x0003DB78 File Offset: 0x0003BD78
		public void Add(string hintName, SourceText source)
		{
			if (string.IsNullOrWhiteSpace(hintName))
			{
				throw new ArgumentNullException("hintName");
			}
			for (int i = 0; i < hintName.Length; i++)
			{
				char c = hintName.get_Chars(i);
				if (!UnicodeCharacterUtilities.IsIdentifierPartCharacter(c) && c != '.' && c != ',' && c != '-' && c != '_' && c != ' ' && c != '(' && c != ')' && c != '[' && c != ']' && c != '{' && c != '}')
				{
					throw new ArgumentException(string.Format(CodeAnalysisResources.HintNameInvalidChar, c, i), "hintName");
				}
			}
			hintName = this.AppendExtensionIfRequired(hintName);
			if (this.Contains(hintName))
			{
				throw new ArgumentException(CodeAnalysisResources.HintNameUniquePerGenerator, "hintName");
			}
			if (source.Encoding == null)
			{
				throw new ArgumentException(CodeAnalysisResources.SourceTextRequiresEncoding, "source");
			}
			this._sourcesAdded.Add(new GeneratedSourceText(hintName, source));
		}

		// Token: 0x060013B3 RID: 5043 RVA: 0x0003DC5D File Offset: 0x0003BE5D
		[NullableContext(0)]
		public void AddRange(ImmutableArray<GeneratedSourceText> texts)
		{
			this._sourcesAdded.AddRange(texts);
		}

		// Token: 0x060013B4 RID: 5044 RVA: 0x0003DC6B File Offset: 0x0003BE6B
		[NullableContext(0)]
		public void AddRange(ImmutableArray<GeneratedSyntaxTree> trees)
		{
			this._sourcesAdded.AddRange(trees.SelectAsArray<GeneratedSyntaxTree, GeneratedSourceText>((GeneratedSyntaxTree t) => new GeneratedSourceText(t.HintName, t.Text)));
		}

		// Token: 0x060013B5 RID: 5045 RVA: 0x0003DCA0 File Offset: 0x0003BEA0
		public void RemoveSource(string hintName)
		{
			hintName = this.AppendExtensionIfRequired(hintName);
			for (int i = 0; i < this._sourcesAdded.Count; i++)
			{
				if (AdditionalSourcesCollection.s_hintNameComparer.Equals(this._sourcesAdded[i].HintName, hintName))
				{
					this._sourcesAdded.RemoveAt(i);
					return;
				}
			}
		}

		// Token: 0x060013B6 RID: 5046 RVA: 0x0003DCFC File Offset: 0x0003BEFC
		public bool Contains(string hintName)
		{
			hintName = this.AppendExtensionIfRequired(hintName);
			for (int i = 0; i < this._sourcesAdded.Count; i++)
			{
				if (AdditionalSourcesCollection.s_hintNameComparer.Equals(this._sourcesAdded[i].HintName, hintName))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060013B7 RID: 5047 RVA: 0x0003DD4C File Offset: 0x0003BF4C
		[NullableContext(0)]
		internal ImmutableArray<GeneratedSourceText> ToImmutableAndFree()
		{
			return this._sourcesAdded.ToImmutableAndFree();
		}

		// Token: 0x060013B8 RID: 5048 RVA: 0x0003DD59 File Offset: 0x0003BF59
		private string AppendExtensionIfRequired(string hintName)
		{
			if (!hintName.EndsWith(this._fileExtension, 5))
			{
				hintName += this._fileExtension;
			}
			return hintName;
		}

		// Token: 0x040005F6 RID: 1526
		private readonly ArrayBuilder<GeneratedSourceText> _sourcesAdded;

		// Token: 0x040005F7 RID: 1527
		private readonly string _fileExtension;

		// Token: 0x040005F8 RID: 1528
		private const StringComparison _hintNameComparison = 5;

		// Token: 0x040005F9 RID: 1529
		private static readonly StringComparer s_hintNameComparer = StringComparer.OrdinalIgnoreCase;
	}
}
