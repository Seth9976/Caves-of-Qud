using System;
using System.Reflection;

namespace MonoMod.Utils
{
	// Token: 0x0200042D RID: 1069
	internal interface _IDMDGenerator
	{
		// Token: 0x0600167E RID: 5758
		MethodInfo Generate(DynamicMethodDefinition dmd, object context);
	}
}
