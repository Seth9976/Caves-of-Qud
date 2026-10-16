using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Microsoft.Cci
{
	// Token: 0x020000E5 RID: 229
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class TypeSpecComparer : IEqualityComparer<ITypeReference>
	{
		// Token: 0x06000755 RID: 1877 RVA: 0x0001807B File Offset: 0x0001627B
		internal TypeSpecComparer(MetadataWriter metadataWriter)
		{
			this._metadataWriter = metadataWriter;
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x0001808C File Offset: 0x0001628C
		[NullableContext(2)]
		public bool Equals(ITypeReference x, ITypeReference y)
		{
			return x == y || this._metadataWriter.GetTypeSpecSignatureIndex(x).Equals(this._metadataWriter.GetTypeSpecSignatureIndex(y));
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x000180C0 File Offset: 0x000162C0
		public int GetHashCode(ITypeReference typeReference)
		{
			return this._metadataWriter.GetTypeSpecSignatureIndex(typeReference).GetHashCode();
		}

		// Token: 0x0400023F RID: 575
		private readonly MetadataWriter _metadataWriter;
	}
}
