using System;
using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Text;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000152 RID: 338
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class SourceReferenceResolver
	{
		// Token: 0x06000DA6 RID: 3494
		[NullableContext(2)]
		public abstract override bool Equals(object other);

		// Token: 0x06000DA7 RID: 3495
		public abstract override int GetHashCode();

		// Token: 0x06000DA8 RID: 3496
		[NullableContext(2)]
		public abstract string NormalizePath([Nullable(1)] string path, string baseFilePath);

		// Token: 0x06000DA9 RID: 3497
		[NullableContext(2)]
		public abstract string ResolveReference([Nullable(1)] string path, string baseFilePath);

		// Token: 0x06000DAA RID: 3498
		public abstract Stream OpenRead(string resolvedPath);

		// Token: 0x06000DAB RID: 3499 RVA: 0x000275DC File Offset: 0x000257DC
		internal Stream OpenReadChecked(string fullPath)
		{
			Stream stream = this.OpenRead(fullPath);
			if (stream == null || !stream.CanRead)
			{
				throw new InvalidOperationException(CodeAnalysisResources.ReferenceResolverShouldReturnReadableNonNullStream);
			}
			return stream;
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x00027608 File Offset: 0x00025808
		public virtual SourceText ReadText(string resolvedPath)
		{
			SourceText sourceText;
			using (Stream stream = this.OpenRead(resolvedPath))
			{
				sourceText = EncodedStringText.Create(stream, null, SourceHashAlgorithm.Sha1, false);
			}
			return sourceText;
		}
	}
}
