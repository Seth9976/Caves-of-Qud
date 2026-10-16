using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200002A RID: 42
	[NativeHeader("Modules/Animation/ScriptBindings/AnimatorUtility.bindings.h")]
	public class AnimatorUtility
	{
		// Token: 0x060002F1 RID: 753 RVA: 0x00006B9C File Offset: 0x00004D9C
		[FreeFunction("AnimatorUtilityBindings::OptimizeTransformHierarchy")]
		public static void OptimizeTransformHierarchy([NotNull] GameObject go, string[] exposedTransforms)
		{
			if (go == null)
			{
				ThrowHelper.ThrowArgumentNullException(go, "go");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(go);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(go, "go");
			}
			AnimatorUtility.OptimizeTransformHierarchy_Injected(intPtr, exposedTransforms);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00006BD4 File Offset: 0x00004DD4
		[FreeFunction("AnimatorUtilityBindings::DeoptimizeTransformHierarchy")]
		public static void DeoptimizeTransformHierarchy([NotNull] GameObject go)
		{
			if (go == null)
			{
				ThrowHelper.ThrowArgumentNullException(go, "go");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(go);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(go, "go");
			}
			AnimatorUtility.DeoptimizeTransformHierarchy_Injected(intPtr);
		}

		// Token: 0x060002F4 RID: 756
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void OptimizeTransformHierarchy_Injected(IntPtr go, string[] exposedTransforms);

		// Token: 0x060002F5 RID: 757
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DeoptimizeTransformHierarchy_Injected(IntPtr go);
	}
}
