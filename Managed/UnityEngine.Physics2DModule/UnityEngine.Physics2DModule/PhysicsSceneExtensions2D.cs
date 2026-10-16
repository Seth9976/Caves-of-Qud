using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.SceneManagement;

namespace UnityEngine
{
	// Token: 0x02000003 RID: 3
	public static class PhysicsSceneExtensions2D
	{
		// Token: 0x06000099 RID: 153 RVA: 0x000036AC File Offset: 0x000018AC
		public static PhysicsScene2D GetPhysicsScene2D(this Scene scene)
		{
			bool flag = !scene.IsValid();
			if (flag)
			{
				throw new ArgumentException("Cannot get physics scene; Unity scene is invalid.", "scene");
			}
			PhysicsScene2D physicsScene_Internal = PhysicsSceneExtensions2D.GetPhysicsScene_Internal(scene);
			bool flag2 = physicsScene_Internal.IsValid();
			if (flag2)
			{
				return physicsScene_Internal;
			}
			throw new Exception("The physics scene associated with the Unity scene is invalid.");
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000036FC File Offset: 0x000018FC
		[StaticAccessor("GetPhysicsManager2D()", StaticAccessorType.Arrow)]
		[NativeMethod("GetPhysicsSceneFromUnityScene")]
		private static PhysicsScene2D GetPhysicsScene_Internal(Scene scene)
		{
			PhysicsScene2D physicsScene2D;
			PhysicsSceneExtensions2D.GetPhysicsScene_Internal_Injected(ref scene, out physicsScene2D);
			return physicsScene2D;
		}

		// Token: 0x0600009B RID: 155
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetPhysicsScene_Internal_Injected([In] ref Scene scene, out PhysicsScene2D ret);
	}
}
