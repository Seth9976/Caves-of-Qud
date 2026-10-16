using System;

namespace Mono.Cecil
{
	// Token: 0x02000220 RID: 544
	public interface IReflectionImporterProvider
	{
		// Token: 0x06000BB5 RID: 2997
		IReflectionImporter GetReflectionImporter(ModuleDefinition module);
	}
}
