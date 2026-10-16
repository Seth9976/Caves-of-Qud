using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Animations;
using UnityEngine.Bindings;
using UnityEngine.Playables;

namespace UnityEngine.Experimental.Animations
{
	// Token: 0x02000043 RID: 67
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationPlayableOutputExtensions.bindings.h")]
	[NativeHeader("Modules/Animation/AnimatorDefines.h")]
	[StaticAccessor("AnimationPlayableOutputExtensionsBindings", StaticAccessorType.DoubleColon)]
	public static class AnimationPlayableOutputExtensions
	{
		// Token: 0x0600039B RID: 923 RVA: 0x00007C68 File Offset: 0x00005E68
		public static AnimationStreamSource GetAnimationStreamSource(this AnimationPlayableOutput output)
		{
			return AnimationPlayableOutputExtensions.InternalGetAnimationStreamSource(output.GetHandle());
		}

		// Token: 0x0600039C RID: 924 RVA: 0x00007C86 File Offset: 0x00005E86
		public static void SetAnimationStreamSource(this AnimationPlayableOutput output, AnimationStreamSource streamSource)
		{
			AnimationPlayableOutputExtensions.InternalSetAnimationStreamSource(output.GetHandle(), streamSource);
		}

		// Token: 0x0600039D RID: 925 RVA: 0x00007C98 File Offset: 0x00005E98
		public static ushort GetSortingOrder(this AnimationPlayableOutput output)
		{
			return (ushort)AnimationPlayableOutputExtensions.InternalGetSortingOrder(output.GetHandle());
		}

		// Token: 0x0600039E RID: 926 RVA: 0x00007CB7 File Offset: 0x00005EB7
		public static void SetSortingOrder(this AnimationPlayableOutput output, ushort sortingOrder)
		{
			AnimationPlayableOutputExtensions.InternalSetSortingOrder(output.GetHandle(), (int)sortingOrder);
		}

		// Token: 0x0600039F RID: 927 RVA: 0x00007CC8 File Offset: 0x00005EC8
		[NativeThrows]
		private static AnimationStreamSource InternalGetAnimationStreamSource(PlayableOutputHandle output)
		{
			return AnimationPlayableOutputExtensions.InternalGetAnimationStreamSource_Injected(ref output);
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x00007CDC File Offset: 0x00005EDC
		[NativeThrows]
		private static void InternalSetAnimationStreamSource(PlayableOutputHandle output, AnimationStreamSource streamSource)
		{
			AnimationPlayableOutputExtensions.InternalSetAnimationStreamSource_Injected(ref output, streamSource);
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x00007CF4 File Offset: 0x00005EF4
		[NativeThrows]
		private static int InternalGetSortingOrder(PlayableOutputHandle output)
		{
			return AnimationPlayableOutputExtensions.InternalGetSortingOrder_Injected(ref output);
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x00007D08 File Offset: 0x00005F08
		[NativeThrows]
		private static void InternalSetSortingOrder(PlayableOutputHandle output, int sortingOrder)
		{
			AnimationPlayableOutputExtensions.InternalSetSortingOrder_Injected(ref output, sortingOrder);
		}

		// Token: 0x060003A3 RID: 931
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnimationStreamSource InternalGetAnimationStreamSource_Injected([In] ref PlayableOutputHandle output);

		// Token: 0x060003A4 RID: 932
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetAnimationStreamSource_Injected([In] ref PlayableOutputHandle output, AnimationStreamSource streamSource);

		// Token: 0x060003A5 RID: 933
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int InternalGetSortingOrder_Injected([In] ref PlayableOutputHandle output);

		// Token: 0x060003A6 RID: 934
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetSortingOrder_Injected([In] ref PlayableOutputHandle output, int sortingOrder);
	}
}
