using System;

namespace Trivial.Mono.Cecil
{
	// Token: 0x02000054 RID: 84
	public interface IMetadataImporterProvider
	{
		// Token: 0x06000283 RID: 643
		IMetadataImporter GetMetadataImporter(ModuleDefinition module);
	}
}
