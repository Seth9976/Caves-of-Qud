using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Cci
{
	// Token: 0x020000B4 RID: 180
	internal interface IContextualNamedEntity : INamedEntity
	{
		// Token: 0x060005E3 RID: 1507
		[NullableContext(1)]
		void AssociateWithMetadataWriter(MetadataWriter metadataWriter);
	}
}
