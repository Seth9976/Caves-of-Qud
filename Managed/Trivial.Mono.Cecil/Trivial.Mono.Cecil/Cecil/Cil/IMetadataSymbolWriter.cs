using System;

namespace Trivial.Mono.Cecil.Cil
{
	// Token: 0x02000112 RID: 274
	internal interface IMetadataSymbolWriter : ISymbolWriter, IDisposable
	{
		// Token: 0x06000A8B RID: 2699
		void SetMetadata(MetadataBuilder metadata);

		// Token: 0x06000A8C RID: 2700
		void WriteModule();
	}
}
