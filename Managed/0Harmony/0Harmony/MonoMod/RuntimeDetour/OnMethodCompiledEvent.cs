using System;
using System.Reflection;

namespace MonoMod.RuntimeDetour
{
	// Token: 0x0200046A RID: 1130
	// (Invoke) Token: 0x06001866 RID: 6246
	public delegate void OnMethodCompiledEvent(MethodBase method, IntPtr codeStart, ulong codeSize);
}
