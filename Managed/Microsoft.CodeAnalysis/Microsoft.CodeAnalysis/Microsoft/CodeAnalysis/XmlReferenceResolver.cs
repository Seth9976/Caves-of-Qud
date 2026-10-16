using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200015A RID: 346
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class XmlReferenceResolver
	{
		// Token: 0x06000DDD RID: 3549
		[NullableContext(2)]
		public abstract override bool Equals(object other);

		// Token: 0x06000DDE RID: 3550
		public abstract override int GetHashCode();

		// Token: 0x06000DDF RID: 3551
		[NullableContext(2)]
		public abstract string ResolveReference([Nullable(1)] string path, string baseFilePath);

		// Token: 0x06000DE0 RID: 3552
		public abstract Stream OpenRead(string resolvedPath);

		// Token: 0x06000DE1 RID: 3553 RVA: 0x00027C20 File Offset: 0x00025E20
		internal Stream OpenReadChecked(string fullPath)
		{
			Stream stream = this.OpenRead(fullPath);
			if (stream == null || !stream.CanRead)
			{
				throw new InvalidOperationException(CodeAnalysisResources.ReferenceResolverShouldReturnReadableNonNullStream);
			}
			return stream;
		}
	}
}
