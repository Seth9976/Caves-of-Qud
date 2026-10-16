using System;

namespace Mono.Cecil
{
	// Token: 0x0200021E RID: 542
	public interface IMetadataImporterProvider
	{
		// Token: 0x06000BB0 RID: 2992
		IMetadataImporter GetMetadataImporter(ModuleDefinition module);
	}
}
