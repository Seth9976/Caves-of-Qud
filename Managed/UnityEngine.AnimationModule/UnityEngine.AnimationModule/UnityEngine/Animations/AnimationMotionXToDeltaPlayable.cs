using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Animations
{
	// Token: 0x02000056 RID: 86
	[RequiredByNativeCode]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationMotionXToDeltaPlayable.bindings.h")]
	[StaticAccessor("AnimationMotionXToDeltaPlayableBindings", StaticAccessorType.DoubleColon)]
	internal struct AnimationMotionXToDeltaPlayable : IPlayable, IEquatable<AnimationMotionXToDeltaPlayable>
	{
		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060004B1 RID: 1201 RVA: 0x000092DC File Offset: 0x000074DC
		public static AnimationMotionXToDeltaPlayable Null
		{
			get
			{
				return AnimationMotionXToDeltaPlayable.m_NullPlayable;
			}
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x000092F4 File Offset: 0x000074F4
		public static AnimationMotionXToDeltaPlayable Create(PlayableGraph graph)
		{
			PlayableHandle playableHandle = AnimationMotionXToDeltaPlayable.CreateHandle(graph);
			return new AnimationMotionXToDeltaPlayable(playableHandle);
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x00009314 File Offset: 0x00007514
		private static PlayableHandle CreateHandle(PlayableGraph graph)
		{
			PlayableHandle @null = PlayableHandle.Null;
			bool flag = !AnimationMotionXToDeltaPlayable.CreateHandleInternal(graph, ref @null);
			PlayableHandle playableHandle;
			if (flag)
			{
				playableHandle = PlayableHandle.Null;
			}
			else
			{
				@null.SetInputCount(1);
				playableHandle = @null;
			}
			return playableHandle;
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x00009350 File Offset: 0x00007550
		private AnimationMotionXToDeltaPlayable(PlayableHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOfType<AnimationMotionXToDeltaPlayable>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an AnimationMotionXToDeltaPlayable.");
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x0000938C File Offset: 0x0000758C
		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x000093A4 File Offset: 0x000075A4
		public static implicit operator Playable(AnimationMotionXToDeltaPlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x000093C4 File Offset: 0x000075C4
		public static explicit operator AnimationMotionXToDeltaPlayable(Playable playable)
		{
			return new AnimationMotionXToDeltaPlayable(playable.GetHandle());
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x000093E4 File Offset: 0x000075E4
		public bool Equals(AnimationMotionXToDeltaPlayable other)
		{
			return this.GetHandle() == other.GetHandle();
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x00009408 File Offset: 0x00007608
		public bool IsAbsoluteMotion()
		{
			return AnimationMotionXToDeltaPlayable.IsAbsoluteMotionInternal(ref this.m_Handle);
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x00009425 File Offset: 0x00007625
		public void SetAbsoluteMotion(bool value)
		{
			AnimationMotionXToDeltaPlayable.SetAbsoluteMotionInternal(ref this.m_Handle, value);
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x00009438 File Offset: 0x00007638
		[NativeThrows]
		private static bool CreateHandleInternal(PlayableGraph graph, ref PlayableHandle handle)
		{
			return AnimationMotionXToDeltaPlayable.CreateHandleInternal_Injected(ref graph, ref handle);
		}

		// Token: 0x060004BC RID: 1212
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsAbsoluteMotionInternal(ref PlayableHandle handle);

		// Token: 0x060004BD RID: 1213
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetAbsoluteMotionInternal(ref PlayableHandle handle, bool value);

		// Token: 0x060004BF RID: 1215
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CreateHandleInternal_Injected([In] ref PlayableGraph graph, ref PlayableHandle handle);

		// Token: 0x0400015D RID: 349
		private PlayableHandle m_Handle;

		// Token: 0x0400015E RID: 350
		private static readonly AnimationMotionXToDeltaPlayable m_NullPlayable = new AnimationMotionXToDeltaPlayable(PlayableHandle.Null);
	}
}
