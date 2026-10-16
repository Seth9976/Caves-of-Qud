using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Animations
{
	// Token: 0x02000052 RID: 82
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationClipPlayable.bindings.h")]
	[NativeHeader("Modules/Animation/Director/AnimationClipPlayable.h")]
	[RequiredByNativeCode]
	[StaticAccessor("AnimationClipPlayableBindings", StaticAccessorType.DoubleColon)]
	public struct AnimationClipPlayable : IPlayable, IEquatable<AnimationClipPlayable>
	{
		// Token: 0x06000403 RID: 1027 RVA: 0x00008574 File Offset: 0x00006774
		public static AnimationClipPlayable Create(PlayableGraph graph, AnimationClip clip)
		{
			PlayableHandle playableHandle = AnimationClipPlayable.CreateHandle(graph, clip);
			return new AnimationClipPlayable(playableHandle);
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00008594 File Offset: 0x00006794
		private static PlayableHandle CreateHandle(PlayableGraph graph, AnimationClip clip)
		{
			PlayableHandle @null = PlayableHandle.Null;
			bool flag = !AnimationClipPlayable.CreateHandleInternal(graph, clip, ref @null);
			PlayableHandle playableHandle;
			if (flag)
			{
				playableHandle = PlayableHandle.Null;
			}
			else
			{
				playableHandle = @null;
			}
			return playableHandle;
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x000085C8 File Offset: 0x000067C8
		internal AnimationClipPlayable(PlayableHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOfType<AnimationClipPlayable>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an AnimationClipPlayable.");
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00008604 File Offset: 0x00006804
		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0000861C File Offset: 0x0000681C
		public static implicit operator Playable(AnimationClipPlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0000863C File Offset: 0x0000683C
		public static explicit operator AnimationClipPlayable(Playable playable)
		{
			return new AnimationClipPlayable(playable.GetHandle());
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0000865C File Offset: 0x0000685C
		public bool Equals(AnimationClipPlayable other)
		{
			return this.GetHandle() == other.GetHandle();
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x00008680 File Offset: 0x00006880
		public AnimationClip GetAnimationClip()
		{
			return AnimationClipPlayable.GetAnimationClipInternal(ref this.m_Handle);
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x000086A0 File Offset: 0x000068A0
		public bool GetApplyFootIK()
		{
			return AnimationClipPlayable.GetApplyFootIKInternal(ref this.m_Handle);
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x000086BD File Offset: 0x000068BD
		public void SetApplyFootIK(bool value)
		{
			AnimationClipPlayable.SetApplyFootIKInternal(ref this.m_Handle, value);
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x000086D0 File Offset: 0x000068D0
		public bool GetApplyPlayableIK()
		{
			return AnimationClipPlayable.GetApplyPlayableIKInternal(ref this.m_Handle);
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x000086ED File Offset: 0x000068ED
		public void SetApplyPlayableIK(bool value)
		{
			AnimationClipPlayable.SetApplyPlayableIKInternal(ref this.m_Handle, value);
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x00008700 File Offset: 0x00006900
		internal bool GetRemoveStartOffset()
		{
			return AnimationClipPlayable.GetRemoveStartOffsetInternal(ref this.m_Handle);
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0000871D File Offset: 0x0000691D
		internal void SetRemoveStartOffset(bool value)
		{
			AnimationClipPlayable.SetRemoveStartOffsetInternal(ref this.m_Handle, value);
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x00008730 File Offset: 0x00006930
		internal bool GetOverrideLoopTime()
		{
			return AnimationClipPlayable.GetOverrideLoopTimeInternal(ref this.m_Handle);
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0000874D File Offset: 0x0000694D
		internal void SetOverrideLoopTime(bool value)
		{
			AnimationClipPlayable.SetOverrideLoopTimeInternal(ref this.m_Handle, value);
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x00008760 File Offset: 0x00006960
		internal bool GetLoopTime()
		{
			return AnimationClipPlayable.GetLoopTimeInternal(ref this.m_Handle);
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0000877D File Offset: 0x0000697D
		internal void SetLoopTime(bool value)
		{
			AnimationClipPlayable.SetLoopTimeInternal(ref this.m_Handle, value);
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x00008790 File Offset: 0x00006990
		internal float GetSampleRate()
		{
			return AnimationClipPlayable.GetSampleRateInternal(ref this.m_Handle);
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x000087AD File Offset: 0x000069AD
		internal void SetSampleRate(float value)
		{
			AnimationClipPlayable.SetSampleRateInternal(ref this.m_Handle, value);
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x000087C0 File Offset: 0x000069C0
		[NativeThrows]
		private static bool CreateHandleInternal(PlayableGraph graph, AnimationClip clip, ref PlayableHandle handle)
		{
			return AnimationClipPlayable.CreateHandleInternal_Injected(ref graph, Object.MarshalledUnityObject.Marshal<AnimationClip>(clip), ref handle);
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x000087DC File Offset: 0x000069DC
		[NativeThrows]
		private static AnimationClip GetAnimationClipInternal(ref PlayableHandle handle)
		{
			return Unmarshal.UnmarshalUnityObject<AnimationClip>(AnimationClipPlayable.GetAnimationClipInternal_Injected(ref handle));
		}

		// Token: 0x06000419 RID: 1049
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetApplyFootIKInternal(ref PlayableHandle handle);

		// Token: 0x0600041A RID: 1050
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetApplyFootIKInternal(ref PlayableHandle handle, bool value);

		// Token: 0x0600041B RID: 1051
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetApplyPlayableIKInternal(ref PlayableHandle handle);

		// Token: 0x0600041C RID: 1052
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetApplyPlayableIKInternal(ref PlayableHandle handle, bool value);

		// Token: 0x0600041D RID: 1053
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetRemoveStartOffsetInternal(ref PlayableHandle handle);

		// Token: 0x0600041E RID: 1054
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetRemoveStartOffsetInternal(ref PlayableHandle handle, bool value);

		// Token: 0x0600041F RID: 1055
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetOverrideLoopTimeInternal(ref PlayableHandle handle);

		// Token: 0x06000420 RID: 1056
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetOverrideLoopTimeInternal(ref PlayableHandle handle, bool value);

		// Token: 0x06000421 RID: 1057
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetLoopTimeInternal(ref PlayableHandle handle);

		// Token: 0x06000422 RID: 1058
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLoopTimeInternal(ref PlayableHandle handle, bool value);

		// Token: 0x06000423 RID: 1059
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetSampleRateInternal(ref PlayableHandle handle);

		// Token: 0x06000424 RID: 1060
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetSampleRateInternal(ref PlayableHandle handle, float value);

		// Token: 0x06000425 RID: 1061
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CreateHandleInternal_Injected([In] ref PlayableGraph graph, IntPtr clip, ref PlayableHandle handle);

		// Token: 0x06000426 RID: 1062
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetAnimationClipInternal_Injected(ref PlayableHandle handle);

		// Token: 0x04000157 RID: 343
		private PlayableHandle m_Handle;
	}
}
