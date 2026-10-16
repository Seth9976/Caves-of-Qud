using System;

namespace Mono.Cecil
{
	// Token: 0x02000230 RID: 560
	public interface IAssemblyResolver : IDisposable
	{
		// Token: 0x06000C2F RID: 3119
		AssemblyDefinition Resolve(AssemblyNameReference name);

		// Token: 0x06000C30 RID: 3120
		AssemblyDefinition Resolve(AssemblyNameReference name, ReaderParameters parameters);
	}
}
