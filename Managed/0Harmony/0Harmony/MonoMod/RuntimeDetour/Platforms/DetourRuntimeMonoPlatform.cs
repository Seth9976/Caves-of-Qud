using System;
using System.Reflection;
using System.Reflection.Emit;

namespace MonoMod.RuntimeDetour.Platforms
{
	// Token: 0x02000486 RID: 1158
	internal class DetourRuntimeMonoPlatform : DetourRuntimeILPlatform
	{
		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x060018F0 RID: 6384 RVA: 0x00018318 File Offset: 0x00016518
		public override bool OnMethodCompiledWillBeCalled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060018F1 RID: 6385 RVA: 0x00056E8C File Offset: 0x0005508C
		// (remove) Token: 0x060018F2 RID: 6386 RVA: 0x00056EC4 File Offset: 0x000550C4
		public override event OnMethodCompiledEvent OnMethodCompiled;

		// Token: 0x060018F3 RID: 6387 RVA: 0x00056EFC File Offset: 0x000550FC
		protected override RuntimeMethodHandle GetMethodHandle(MethodBase method)
		{
			if (method is DynamicMethod)
			{
				MethodInfo dynamicMethod_CreateDynMethod = DetourRuntimeMonoPlatform._DynamicMethod_CreateDynMethod;
				if (dynamicMethod_CreateDynMethod != null)
				{
					dynamicMethod_CreateDynMethod.Invoke(method, DetourRuntimeMonoPlatform._NoArgs);
				}
				if (DetourRuntimeMonoPlatform._DynamicMethod_mhandle != null)
				{
					return (RuntimeMethodHandle)DetourRuntimeMonoPlatform._DynamicMethod_mhandle.GetValue(method);
				}
			}
			return method.MethodHandle;
		}

		// Token: 0x060018F4 RID: 6388 RVA: 0x00056F4C File Offset: 0x0005514C
		protected unsafe override void DisableInlining(MethodBase method, RuntimeMethodHandle handle)
		{
			ushort* ptr = (long)handle.Value / 2L + 2L;
			ushort* ptr2 = ptr;
			*ptr2 |= 8;
		}

		// Token: 0x0400126E RID: 4718
		private static readonly object[] _NoArgs = new object[0];

		// Token: 0x0400126F RID: 4719
		private static readonly MethodInfo _DynamicMethod_CreateDynMethod = typeof(DynamicMethod).GetMethod("CreateDynMethod", BindingFlags.Instance | BindingFlags.NonPublic);

		// Token: 0x04001270 RID: 4720
		private static readonly FieldInfo _DynamicMethod_mhandle = typeof(DynamicMethod).GetField("mhandle", BindingFlags.Instance | BindingFlags.NonPublic);
	}
}
