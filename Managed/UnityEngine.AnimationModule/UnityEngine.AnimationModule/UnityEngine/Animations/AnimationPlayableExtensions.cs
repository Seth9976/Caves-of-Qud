using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;

namespace UnityEngine.Animations
{
	// Token: 0x02000058 RID: 88
	[NativeHeader("Modules/Animation/AnimationClip.h")]
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	[NativeHeader("Modules/Animation/Director/AnimationPlayableExtensions.h")]
	public static class AnimationPlayableExtensions
	{
		// Token: 0x060004D7 RID: 1239 RVA: 0x00009684 File Offset: 0x00007884
		public static void SetAnimatedProperties<U>(this U playable, AnimationClip clip) where U : struct, IPlayable
		{
			PlayableHandle handle = playable.GetHandle();
			AnimationPlayableExtensions.SetAnimatedPropertiesInternal(ref handle, clip);
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x000096AC File Offset: 0x000078AC
		[NativeThrows]
		internal static void SetAnimatedPropertiesInternal(ref PlayableHandle playable, AnimationClip animatedProperties)
		{
			AnimationPlayableExtensions.SetAnimatedPropertiesInternal_Injected(ref playable, Object.MarshalledUnityObject.Marshal<AnimationClip>(animatedProperties));
		}

		// Token: 0x060004D9 RID: 1241
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetAnimatedPropertiesInternal_Injected(ref PlayableHandle playable, IntPtr animatedProperties);
	}
}
