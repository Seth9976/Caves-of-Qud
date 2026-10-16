using System;
using System.Reflection;
using Mono.Cecil;

namespace MonoMod.Utils
{
	// Token: 0x0200043D RID: 1085
	internal class DynamicMethodReference : MethodReference
	{
		// Token: 0x060016D2 RID: 5842 RVA: 0x0004C668 File Offset: 0x0004A868
		public DynamicMethodReference(ModuleDefinition module, MethodInfo dm)
			: base("", module.TypeSystem.Void)
		{
			this.DynamicMethod = dm;
		}

		// Token: 0x04000FF5 RID: 4085
		public MethodInfo DynamicMethod;
	}
}
