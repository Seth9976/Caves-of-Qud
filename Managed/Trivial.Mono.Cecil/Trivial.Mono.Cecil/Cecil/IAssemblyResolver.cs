using System;

namespace Trivial.Mono.Cecil
{
	// Token: 0x0200004A RID: 74
	public interface IAssemblyResolver : IDisposable
	{
		// Token: 0x0600025D RID: 605
		AssemblyDefinition Resolve(AssemblyNameReference name);

		// Token: 0x0600025E RID: 606
		AssemblyDefinition Resolve(AssemblyNameReference name, ReaderParameters parameters);
	}
}
