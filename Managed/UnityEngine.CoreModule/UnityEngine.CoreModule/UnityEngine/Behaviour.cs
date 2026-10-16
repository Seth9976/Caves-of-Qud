using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000285 RID: 645
	[UsedByNativeCode]
	[NativeHeader("Runtime/Mono/MonoBehaviour.h")]
	public class Behaviour : Component
	{
		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06002032 RID: 8242 RVA: 0x0003E08C File Offset: 0x0003C28C
		// (set) Token: 0x06002033 RID: 8243 RVA: 0x0003E0B0 File Offset: 0x0003C2B0
		[NativeProperty]
		[RequiredByNativeCode]
		public bool enabled
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Behaviour>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Behaviour.get_enabled_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Behaviour>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Behaviour.set_enabled_Injected(intPtr, value);
			}
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06002034 RID: 8244 RVA: 0x0003E0D4 File Offset: 0x0003C2D4
		[NativeProperty]
		public bool isActiveAndEnabled
		{
			[NativeMethod("IsAddedToManager")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Behaviour>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Behaviour.get_isActiveAndEnabled_Injected(intPtr);
			}
		}

		// Token: 0x06002036 RID: 8246
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_enabled_Injected(IntPtr _unity_self);

		// Token: 0x06002037 RID: 8247
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_enabled_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06002038 RID: 8248
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isActiveAndEnabled_Injected(IntPtr _unity_self);
	}
}
