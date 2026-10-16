using System;
using Mono.Cecil;

namespace MonoMod.Utils
{
	// Token: 0x0200044A RID: 1098
	public interface ICallSiteGenerator
	{
		// Token: 0x06001758 RID: 5976
		CallSite ToCallSite(ModuleDefinition module);
	}
}
