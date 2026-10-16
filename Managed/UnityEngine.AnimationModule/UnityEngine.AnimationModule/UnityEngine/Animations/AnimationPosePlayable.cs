using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Animations
{
	// Token: 0x0200005B RID: 91
	[NativeHeader("Modules/Animation/Director/AnimationPosePlayable.h")]
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	[StaticAccessor("AnimationPosePlayableBindings", StaticAccessorType.DoubleColon)]
	[RequiredByNativeCode]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationPosePlayable.bindings.h")]
	internal struct AnimationPosePlayable : IPlayable, IEquatable<AnimationPosePlayable>
	{
		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060004EF RID: 1263 RVA: 0x000098C4 File Offset: 0x00007AC4
		public static AnimationPosePlayable Null
		{
			get
			{
				return AnimationPosePlayable.m_NullPlayable;
			}
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x000098DC File Offset: 0x00007ADC
		public static AnimationPosePlayable Create(PlayableGraph graph)
		{
			PlayableHandle playableHandle = AnimationPosePlayable.CreateHandle(graph);
			return new AnimationPosePlayable(playableHandle);
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x000098FC File Offset: 0x00007AFC
		private static PlayableHandle CreateHandle(PlayableGraph graph)
		{
			PlayableHandle @null = PlayableHandle.Null;
			bool flag = !AnimationPosePlayable.CreateHandleInternal(graph, ref @null);
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

		// Token: 0x060004F2 RID: 1266 RVA: 0x0000992C File Offset: 0x00007B2C
		internal AnimationPosePlayable(PlayableHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOfType<AnimationPosePlayable>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an AnimationPosePlayable.");
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00009968 File Offset: 0x00007B68
		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x00009980 File Offset: 0x00007B80
		public static implicit operator Playable(AnimationPosePlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x000099A0 File Offset: 0x00007BA0
		public static explicit operator AnimationPosePlayable(Playable playable)
		{
			return new AnimationPosePlayable(playable.GetHandle());
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x000099C0 File Offset: 0x00007BC0
		public bool Equals(AnimationPosePlayable other)
		{
			return this.Equals(other.GetHandle());
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x000099EC File Offset: 0x00007BEC
		public bool GetMustReadPreviousPose()
		{
			return AnimationPosePlayable.GetMustReadPreviousPoseInternal(ref this.m_Handle);
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00009A09 File Offset: 0x00007C09
		public void SetMustReadPreviousPose(bool value)
		{
			AnimationPosePlayable.SetMustReadPreviousPoseInternal(ref this.m_Handle, value);
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x00009A1C File Offset: 0x00007C1C
		public bool GetReadDefaultPose()
		{
			return AnimationPosePlayable.GetReadDefaultPoseInternal(ref this.m_Handle);
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x00009A39 File Offset: 0x00007C39
		public void SetReadDefaultPose(bool value)
		{
			AnimationPosePlayable.SetReadDefaultPoseInternal(ref this.m_Handle, value);
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x00009A4C File Offset: 0x00007C4C
		public bool GetApplyFootIK()
		{
			return AnimationPosePlayable.GetApplyFootIKInternal(ref this.m_Handle);
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x00009A69 File Offset: 0x00007C69
		public void SetApplyFootIK(bool value)
		{
			AnimationPosePlayable.SetApplyFootIKInternal(ref this.m_Handle, value);
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x00009A7C File Offset: 0x00007C7C
		[NativeThrows]
		private static bool CreateHandleInternal(PlayableGraph graph, ref PlayableHandle handle)
		{
			return AnimationPosePlayable.CreateHandleInternal_Injected(ref graph, ref handle);
		}

		// Token: 0x060004FE RID: 1278
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetMustReadPreviousPoseInternal(ref PlayableHandle handle);

		// Token: 0x060004FF RID: 1279
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetMustReadPreviousPoseInternal(ref PlayableHandle handle, bool value);

		// Token: 0x06000500 RID: 1280
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetReadDefaultPoseInternal(ref PlayableHandle handle);

		// Token: 0x06000501 RID: 1281
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetReadDefaultPoseInternal(ref PlayableHandle handle, bool value);

		// Token: 0x06000502 RID: 1282
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetApplyFootIKInternal(ref PlayableHandle handle);

		// Token: 0x06000503 RID: 1283
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetApplyFootIKInternal(ref PlayableHandle handle, bool value);

		// Token: 0x06000505 RID: 1285
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CreateHandleInternal_Injected([In] ref PlayableGraph graph, ref PlayableHandle handle);

		// Token: 0x04000162 RID: 354
		private PlayableHandle m_Handle;

		// Token: 0x04000163 RID: 355
		private static readonly AnimationPosePlayable m_NullPlayable = new AnimationPosePlayable(PlayableHandle.Null);
	}
}
