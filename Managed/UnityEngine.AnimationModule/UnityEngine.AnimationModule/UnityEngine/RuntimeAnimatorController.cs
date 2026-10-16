using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000041 RID: 65
	[NativeHeader("Modules/Animation/RuntimeAnimatorController.h")]
	[ExcludeFromObjectFactory]
	[UsedByNativeCode]
	public class RuntimeAnimatorController : Object
	{
		// Token: 0x06000398 RID: 920 RVA: 0x00006C0A File Offset: 0x00004E0A
		protected RuntimeAnimatorController()
		{
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000399 RID: 921 RVA: 0x00007C44 File Offset: 0x00005E44
		public AnimationClip[] animationClips
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RuntimeAnimatorController>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return RuntimeAnimatorController.get_animationClips_Injected(intPtr);
			}
		}

		// Token: 0x0600039A RID: 922
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnimationClip[] get_animationClips_Injected(IntPtr _unity_self);
	}
}
