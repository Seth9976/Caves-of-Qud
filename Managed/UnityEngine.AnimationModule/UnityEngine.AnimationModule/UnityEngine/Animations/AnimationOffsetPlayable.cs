using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Animations
{
	// Token: 0x02000057 RID: 87
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationOffsetPlayable.bindings.h")]
	[NativeHeader("Modules/Animation/Director/AnimationOffsetPlayable.h")]
	[RequiredByNativeCode]
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	[StaticAccessor("AnimationOffsetPlayableBindings", StaticAccessorType.DoubleColon)]
	internal struct AnimationOffsetPlayable : IPlayable, IEquatable<AnimationOffsetPlayable>
	{
		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x00009460 File Offset: 0x00007660
		public static AnimationOffsetPlayable Null
		{
			get
			{
				return AnimationOffsetPlayable.m_NullPlayable;
			}
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x00009478 File Offset: 0x00007678
		public static AnimationOffsetPlayable Create(PlayableGraph graph, Vector3 position, Quaternion rotation, int inputCount)
		{
			PlayableHandle playableHandle = AnimationOffsetPlayable.CreateHandle(graph, position, rotation, inputCount);
			return new AnimationOffsetPlayable(playableHandle);
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x0000949C File Offset: 0x0000769C
		private static PlayableHandle CreateHandle(PlayableGraph graph, Vector3 position, Quaternion rotation, int inputCount)
		{
			PlayableHandle @null = PlayableHandle.Null;
			bool flag = !AnimationOffsetPlayable.CreateHandleInternal(graph, position, rotation, ref @null);
			PlayableHandle playableHandle;
			if (flag)
			{
				playableHandle = PlayableHandle.Null;
			}
			else
			{
				@null.SetInputCount(inputCount);
				playableHandle = @null;
			}
			return playableHandle;
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x000094D8 File Offset: 0x000076D8
		internal AnimationOffsetPlayable(PlayableHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOfType<AnimationOffsetPlayable>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an AnimationOffsetPlayable.");
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x00009514 File Offset: 0x00007714
		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x0000952C File Offset: 0x0000772C
		public static implicit operator Playable(AnimationOffsetPlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x0000954C File Offset: 0x0000774C
		public static explicit operator AnimationOffsetPlayable(Playable playable)
		{
			return new AnimationOffsetPlayable(playable.GetHandle());
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x0000956C File Offset: 0x0000776C
		public bool Equals(AnimationOffsetPlayable other)
		{
			return this.Equals(other.GetHandle());
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x00009598 File Offset: 0x00007798
		public Vector3 GetPosition()
		{
			return AnimationOffsetPlayable.GetPositionInternal(ref this.m_Handle);
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x000095B5 File Offset: 0x000077B5
		public void SetPosition(Vector3 value)
		{
			AnimationOffsetPlayable.SetPositionInternal(ref this.m_Handle, value);
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x000095C8 File Offset: 0x000077C8
		public Quaternion GetRotation()
		{
			return AnimationOffsetPlayable.GetRotationInternal(ref this.m_Handle);
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x000095E5 File Offset: 0x000077E5
		public void SetRotation(Quaternion value)
		{
			AnimationOffsetPlayable.SetRotationInternal(ref this.m_Handle, value);
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x000095F8 File Offset: 0x000077F8
		[NativeThrows]
		private static bool CreateHandleInternal(PlayableGraph graph, Vector3 position, Quaternion rotation, ref PlayableHandle handle)
		{
			return AnimationOffsetPlayable.CreateHandleInternal_Injected(ref graph, ref position, ref rotation, ref handle);
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x00009614 File Offset: 0x00007814
		[NativeThrows]
		private static Vector3 GetPositionInternal(ref PlayableHandle handle)
		{
			Vector3 vector;
			AnimationOffsetPlayable.GetPositionInternal_Injected(ref handle, out vector);
			return vector;
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x0000962C File Offset: 0x0000782C
		[NativeThrows]
		private static void SetPositionInternal(ref PlayableHandle handle, Vector3 value)
		{
			AnimationOffsetPlayable.SetPositionInternal_Injected(ref handle, ref value);
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x00009644 File Offset: 0x00007844
		[NativeThrows]
		private static Quaternion GetRotationInternal(ref PlayableHandle handle)
		{
			Quaternion quaternion;
			AnimationOffsetPlayable.GetRotationInternal_Injected(ref handle, out quaternion);
			return quaternion;
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x0000965C File Offset: 0x0000785C
		[NativeThrows]
		private static void SetRotationInternal(ref PlayableHandle handle, Quaternion value)
		{
			AnimationOffsetPlayable.SetRotationInternal_Injected(ref handle, ref value);
		}

		// Token: 0x060004D2 RID: 1234
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CreateHandleInternal_Injected([In] ref PlayableGraph graph, [In] ref Vector3 position, [In] ref Quaternion rotation, ref PlayableHandle handle);

		// Token: 0x060004D3 RID: 1235
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetPositionInternal_Injected(ref PlayableHandle handle, out Vector3 ret);

		// Token: 0x060004D4 RID: 1236
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPositionInternal_Injected(ref PlayableHandle handle, [In] ref Vector3 value);

		// Token: 0x060004D5 RID: 1237
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetRotationInternal_Injected(ref PlayableHandle handle, out Quaternion ret);

		// Token: 0x060004D6 RID: 1238
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetRotationInternal_Injected(ref PlayableHandle handle, [In] ref Quaternion value);

		// Token: 0x0400015F RID: 351
		private PlayableHandle m_Handle;

		// Token: 0x04000160 RID: 352
		private static readonly AnimationOffsetPlayable m_NullPlayable = new AnimationOffsetPlayable(PlayableHandle.Null);
	}
}
