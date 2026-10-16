using System;

namespace Trivial.Mono.Cecil
{
	// Token: 0x02000062 RID: 98
	public interface IReflectionImporterProvider
	{
		// Token: 0x060002CC RID: 716
		IReflectionImporter GetReflectionImporter(ModuleDefinition module);
	}
}
