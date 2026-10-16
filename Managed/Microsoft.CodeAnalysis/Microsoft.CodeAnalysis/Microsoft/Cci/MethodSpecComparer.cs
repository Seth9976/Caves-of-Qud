using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.Cci
{
	// Token: 0x020000AF RID: 175
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class MethodSpecComparer : IEqualityComparer<IGenericMethodInstanceReference>
	{
		// Token: 0x060005D0 RID: 1488 RVA: 0x00015E8E File Offset: 0x0001408E
		internal MethodSpecComparer(MetadataWriter metadataWriter)
		{
			this._metadataWriter = metadataWriter;
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x00015EA0 File Offset: 0x000140A0
		[NullableContext(2)]
		public bool Equals(IGenericMethodInstanceReference x, IGenericMethodInstanceReference y)
		{
			return x == y || (this._metadataWriter.GetMethodDefinitionOrReferenceHandle(x.GetGenericMethod(this._metadataWriter.Context)) == this._metadataWriter.GetMethodDefinitionOrReferenceHandle(y.GetGenericMethod(this._metadataWriter.Context)) && this._metadataWriter.GetMethodSpecificationSignatureHandle(x) == this._metadataWriter.GetMethodSpecificationSignatureHandle(y));
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x00015F14 File Offset: 0x00014114
		public int GetHashCode(IGenericMethodInstanceReference methodInstanceReference)
		{
			return Hash.Combine(this._metadataWriter.GetMethodDefinitionOrReferenceHandle(methodInstanceReference.GetGenericMethod(this._metadataWriter.Context)).GetHashCode(), this._metadataWriter.GetMethodSpecificationSignatureHandle(methodInstanceReference).GetHashCode());
		}

		// Token: 0x040001E7 RID: 487
		private readonly MetadataWriter _metadataWriter;
	}
}
