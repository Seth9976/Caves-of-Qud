using System;
using System.Reflection;

namespace MonoMod.RuntimeDetour
{
	// Token: 0x02000469 RID: 1129
	internal interface IDetourRuntimePlatform
	{
		// Token: 0x0600185A RID: 6234
		MethodBase GetIdentifiable(MethodBase method);

		// Token: 0x0600185B RID: 6235
		IntPtr GetNativeStart(MethodBase method);

		// Token: 0x0600185C RID: 6236
		MethodInfo CreateCopy(MethodBase method);

		// Token: 0x0600185D RID: 6237
		bool TryCreateCopy(MethodBase method, out MethodInfo dm);

		// Token: 0x0600185E RID: 6238
		void Pin(MethodBase method);

		// Token: 0x0600185F RID: 6239
		void Unpin(MethodBase method);

		// Token: 0x06001860 RID: 6240
		MethodBase GetDetourTarget(MethodBase from, MethodBase to);

		// Token: 0x06001861 RID: 6241
		uint TryMemAllocScratchCloseTo(IntPtr target, out IntPtr ptr, int size);

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x06001862 RID: 6242
		bool OnMethodCompiledWillBeCalled { get; }

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06001863 RID: 6243
		// (remove) Token: 0x06001864 RID: 6244
		event OnMethodCompiledEvent OnMethodCompiled;
	}
}
