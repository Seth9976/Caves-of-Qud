using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000011 RID: 17
	[NativeHeader("Runtime/Camera/Camera.h")]
	internal class CameraRaycastHelper
	{
		// Token: 0x06000059 RID: 89 RVA: 0x00002638 File Offset: 0x00000838
		[FreeFunction("CameraScripting::RaycastTry")]
		internal static GameObject RaycastTry(Camera cam, Ray ray, float distance, int layerMask)
		{
			return Unmarshal.UnmarshalUnityObject<GameObject>(CameraRaycastHelper.RaycastTry_Injected(Object.MarshalledUnityObject.Marshal<Camera>(cam), ref ray, distance, layerMask));
		}

		// Token: 0x0600005A RID: 90 RVA: 0x0000265C File Offset: 0x0000085C
		[FreeFunction("CameraScripting::RaycastTry2D")]
		internal static GameObject RaycastTry2D(Camera cam, Ray ray, float distance, int layerMask)
		{
			return Unmarshal.UnmarshalUnityObject<GameObject>(CameraRaycastHelper.RaycastTry2D_Injected(Object.MarshalledUnityObject.Marshal<Camera>(cam), ref ray, distance, layerMask));
		}

		// Token: 0x0600005C RID: 92
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr RaycastTry_Injected(IntPtr cam, [In] ref Ray ray, float distance, int layerMask);

		// Token: 0x0600005D RID: 93
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr RaycastTry2D_Injected(IntPtr cam, [In] ref Ray ray, float distance, int layerMask);
	}
}
