using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Animations
{
	// Token: 0x02000054 RID: 84
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationLayerMixerPlayable.bindings.h")]
	[NativeHeader("Modules/Animation/Director/AnimationLayerMixerPlayable.h")]
	[RequiredByNativeCode]
	[StaticAccessor("AnimationLayerMixerPlayableBindings", StaticAccessorType.DoubleColon)]
	public struct AnimationLayerMixerPlayable : IPlayable, IEquatable<AnimationLayerMixerPlayable>
	{
		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x00008E98 File Offset: 0x00007098
		public static AnimationLayerMixerPlayable Null
		{
			get
			{
				return AnimationLayerMixerPlayable.m_NullPlayable;
			}
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00008EB0 File Offset: 0x000070B0
		public static AnimationLayerMixerPlayable Create(PlayableGraph graph, int inputCount = 0)
		{
			return AnimationLayerMixerPlayable.Create(graph, inputCount, true);
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00008ECC File Offset: 0x000070CC
		public static AnimationLayerMixerPlayable Create(PlayableGraph graph, int inputCount, bool singleLayerOptimization)
		{
			PlayableHandle playableHandle = AnimationLayerMixerPlayable.CreateHandle(graph, inputCount);
			AnimationLayerMixerPlayable animationLayerMixerPlayable = new AnimationLayerMixerPlayable(playableHandle, singleLayerOptimization);
			return animationLayerMixerPlayable;
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x00008EF0 File Offset: 0x000070F0
		private static PlayableHandle CreateHandle(PlayableGraph graph, int inputCount = 0)
		{
			PlayableHandle @null = PlayableHandle.Null;
			bool flag = !AnimationLayerMixerPlayable.CreateHandleInternal(graph, ref @null);
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

		// Token: 0x06000495 RID: 1173 RVA: 0x00008F2C File Offset: 0x0000712C
		internal AnimationLayerMixerPlayable(PlayableHandle handle, bool singleLayerOptimization = true)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOfType<AnimationLayerMixerPlayable>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an AnimationLayerMixerPlayable.");
				}
				AnimationLayerMixerPlayable.SetSingleLayerOptimizationInternal(ref handle, singleLayerOptimization);
			}
			this.m_Handle = handle;
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x00008F70 File Offset: 0x00007170
		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x00008F88 File Offset: 0x00007188
		public static implicit operator Playable(AnimationLayerMixerPlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x00008FA8 File Offset: 0x000071A8
		public static explicit operator AnimationLayerMixerPlayable(Playable playable)
		{
			return new AnimationLayerMixerPlayable(playable.GetHandle(), true);
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00008FC8 File Offset: 0x000071C8
		public bool Equals(AnimationLayerMixerPlayable other)
		{
			return this.GetHandle() == other.GetHandle();
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x00008FEC File Offset: 0x000071EC
		public bool IsLayerAdditive(uint layerIndex)
		{
			bool flag = (ulong)layerIndex >= (ulong)((long)this.m_Handle.GetInputCount());
			if (flag)
			{
				throw new ArgumentOutOfRangeException("layerIndex", string.Format("layerIndex {0} must be in the range of 0 to {1}.", layerIndex, this.m_Handle.GetInputCount() - 1));
			}
			return AnimationLayerMixerPlayable.IsLayerAdditiveInternal(ref this.m_Handle, layerIndex);
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00009050 File Offset: 0x00007250
		public void SetLayerAdditive(uint layerIndex, bool value)
		{
			bool flag = (ulong)layerIndex >= (ulong)((long)this.m_Handle.GetInputCount());
			if (flag)
			{
				throw new ArgumentOutOfRangeException("layerIndex", string.Format("layerIndex {0} must be in the range of 0 to {1}.", layerIndex, this.m_Handle.GetInputCount() - 1));
			}
			AnimationLayerMixerPlayable.SetLayerAdditiveInternal(ref this.m_Handle, layerIndex, value);
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x000090B0 File Offset: 0x000072B0
		public void SetLayerMaskFromAvatarMask(uint layerIndex, AvatarMask mask)
		{
			bool flag = (ulong)layerIndex >= (ulong)((long)this.m_Handle.GetInputCount());
			if (flag)
			{
				throw new ArgumentOutOfRangeException("layerIndex", string.Format("layerIndex {0} must be in the range of 0 to {1}.", layerIndex, this.m_Handle.GetInputCount() - 1));
			}
			bool flag2 = mask == null;
			if (flag2)
			{
				throw new ArgumentNullException("mask");
			}
			AnimationLayerMixerPlayable.SetLayerMaskFromAvatarMaskInternal(ref this.m_Handle, layerIndex, mask);
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x00009128 File Offset: 0x00007328
		[NativeThrows]
		private static bool CreateHandleInternal(PlayableGraph graph, ref PlayableHandle handle)
		{
			return AnimationLayerMixerPlayable.CreateHandleInternal_Injected(ref graph, ref handle);
		}

		// Token: 0x0600049E RID: 1182
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsLayerAdditiveInternal(ref PlayableHandle handle, uint layerIndex);

		// Token: 0x0600049F RID: 1183
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLayerAdditiveInternal(ref PlayableHandle handle, uint layerIndex, bool value);

		// Token: 0x060004A0 RID: 1184
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetSingleLayerOptimizationInternal(ref PlayableHandle handle, bool value);

		// Token: 0x060004A1 RID: 1185 RVA: 0x00009140 File Offset: 0x00007340
		[NativeThrows]
		private static void SetLayerMaskFromAvatarMaskInternal(ref PlayableHandle handle, uint layerIndex, AvatarMask mask)
		{
			AnimationLayerMixerPlayable.SetLayerMaskFromAvatarMaskInternal_Injected(ref handle, layerIndex, Object.MarshalledUnityObject.Marshal<AvatarMask>(mask));
		}

		// Token: 0x060004A3 RID: 1187
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CreateHandleInternal_Injected([In] ref PlayableGraph graph, ref PlayableHandle handle);

		// Token: 0x060004A4 RID: 1188
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLayerMaskFromAvatarMaskInternal_Injected(ref PlayableHandle handle, uint layerIndex, IntPtr mask);

		// Token: 0x04000159 RID: 345
		private PlayableHandle m_Handle;

		// Token: 0x0400015A RID: 346
		private static readonly AnimationLayerMixerPlayable m_NullPlayable = new AnimationLayerMixerPlayable(PlayableHandle.Null, true);
	}
}
