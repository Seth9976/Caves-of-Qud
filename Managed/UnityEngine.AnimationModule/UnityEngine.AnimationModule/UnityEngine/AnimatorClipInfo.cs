using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000021 RID: 33
	[NativeHeader("Modules/Animation/ScriptBindings/Animation.bindings.h")]
	[NativeHeader("Modules/Animation/AnimatorInfo.h")]
	[UsedByNativeCode]
	public struct AnimatorClipInfo
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00003D98 File Offset: 0x00001F98
		public AnimationClip clip
		{
			get
			{
				return (this.m_ClipInstanceID != 0) ? AnimatorClipInfo.InstanceIDToAnimationClipPPtr(this.m_ClipInstanceID) : null;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00003DC0 File Offset: 0x00001FC0
		public float weight
		{
			get
			{
				return this.m_Weight;
			}
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00003DD8 File Offset: 0x00001FD8
		[FreeFunction("AnimationBindings::InstanceIDToAnimationClipPPtr")]
		private static AnimationClip InstanceIDToAnimationClipPPtr(int instanceID)
		{
			return Unmarshal.UnmarshalUnityObject<AnimationClip>(AnimatorClipInfo.InstanceIDToAnimationClipPPtr_Injected(instanceID));
		}

		// Token: 0x0600010B RID: 267
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr InstanceIDToAnimationClipPPtr_Injected(int instanceID);

		// Token: 0x04000064 RID: 100
		private int m_ClipInstanceID;

		// Token: 0x04000065 RID: 101
		private float m_Weight;
	}
}
