using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;

namespace UnityEngine.Animations
{
	// Token: 0x02000059 RID: 89
	[StaticAccessor("AnimationPlayableGraphExtensionsBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Modules/Animation/Animator.h")]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationPlayableGraphExtensions.bindings.h")]
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	[NativeHeader("Runtime/Director/Core/HPlayableOutput.h")]
	internal static class AnimationPlayableGraphExtensions
	{
		// Token: 0x060004DA RID: 1242 RVA: 0x000096C5 File Offset: 0x000078C5
		internal static void SyncUpdateAndTimeMode(this PlayableGraph graph, Animator animator)
		{
			AnimationPlayableGraphExtensions.InternalSyncUpdateAndTimeMode(ref graph, animator);
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x000096D1 File Offset: 0x000078D1
		internal static void DestroyOutput(this PlayableGraph graph, PlayableOutputHandle handle)
		{
			AnimationPlayableGraphExtensions.InternalDestroyOutput(ref graph, ref handle);
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x000096E0 File Offset: 0x000078E0
		[NativeThrows]
		internal unsafe static bool InternalCreateAnimationOutput(ref PlayableGraph graph, string name, out PlayableOutputHandle handle)
		{
			bool flag;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				flag = AnimationPlayableGraphExtensions.InternalCreateAnimationOutput_Injected(ref graph, ref managedSpanWrapper, out handle);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00009738 File Offset: 0x00007938
		[NativeThrows]
		internal static void InternalSyncUpdateAndTimeMode(ref PlayableGraph graph, [NotNull] Animator animator)
		{
			if (animator == null)
			{
				ThrowHelper.ThrowArgumentNullException(animator, "animator");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(animator);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(animator, "animator");
			}
			AnimationPlayableGraphExtensions.InternalSyncUpdateAndTimeMode_Injected(ref graph, intPtr);
		}

		// Token: 0x060004DE RID: 1246
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalDestroyOutput(ref PlayableGraph graph, ref PlayableOutputHandle handle);

		// Token: 0x060004DF RID: 1247
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int InternalAnimationOutputCount(ref PlayableGraph graph);

		// Token: 0x060004E0 RID: 1248
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool InternalGetAnimationOutput(ref PlayableGraph graph, int index, out PlayableOutputHandle handle);

		// Token: 0x060004E1 RID: 1249
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool InternalCreateAnimationOutput_Injected(ref PlayableGraph graph, ref ManagedSpanWrapper name, out PlayableOutputHandle handle);

		// Token: 0x060004E2 RID: 1250
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSyncUpdateAndTimeMode_Injected(ref PlayableGraph graph, IntPtr animator);
	}
}
