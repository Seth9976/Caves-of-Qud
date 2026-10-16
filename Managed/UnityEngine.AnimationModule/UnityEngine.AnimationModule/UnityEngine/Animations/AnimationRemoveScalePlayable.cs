using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Animations
{
	// Token: 0x0200005C RID: 92
	[NativeHeader("Modules/Animation/Director/AnimationRemoveScalePlayable.h")]
	[RequiredByNativeCode]
	[StaticAccessor("AnimationRemoveScalePlayableBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationRemoveScalePlayable.bindings.h")]
	internal struct AnimationRemoveScalePlayable : IPlayable, IEquatable<AnimationRemoveScalePlayable>
	{
		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000506 RID: 1286 RVA: 0x00009AA4 File Offset: 0x00007CA4
		public static AnimationRemoveScalePlayable Null
		{
			get
			{
				return AnimationRemoveScalePlayable.m_NullPlayable;
			}
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00009ABC File Offset: 0x00007CBC
		public static AnimationRemoveScalePlayable Create(PlayableGraph graph, int inputCount)
		{
			PlayableHandle playableHandle = AnimationRemoveScalePlayable.CreateHandle(graph, inputCount);
			return new AnimationRemoveScalePlayable(playableHandle);
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x00009ADC File Offset: 0x00007CDC
		private static PlayableHandle CreateHandle(PlayableGraph graph, int inputCount)
		{
			PlayableHandle @null = PlayableHandle.Null;
			bool flag = !AnimationRemoveScalePlayable.CreateHandleInternal(graph, ref @null);
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

		// Token: 0x06000509 RID: 1289 RVA: 0x00009B18 File Offset: 0x00007D18
		internal AnimationRemoveScalePlayable(PlayableHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOfType<AnimationRemoveScalePlayable>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an AnimationRemoveScalePlayable.");
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x00009B54 File Offset: 0x00007D54
		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x00009B6C File Offset: 0x00007D6C
		public static implicit operator Playable(AnimationRemoveScalePlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x00009B8C File Offset: 0x00007D8C
		public static explicit operator AnimationRemoveScalePlayable(Playable playable)
		{
			return new AnimationRemoveScalePlayable(playable.GetHandle());
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x00009BAC File Offset: 0x00007DAC
		public bool Equals(AnimationRemoveScalePlayable other)
		{
			return this.Equals(other.GetHandle());
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x00009BD8 File Offset: 0x00007DD8
		[NativeThrows]
		private static bool CreateHandleInternal(PlayableGraph graph, ref PlayableHandle handle)
		{
			return AnimationRemoveScalePlayable.CreateHandleInternal_Injected(ref graph, ref handle);
		}

		// Token: 0x06000510 RID: 1296
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CreateHandleInternal_Injected([In] ref PlayableGraph graph, ref PlayableHandle handle);

		// Token: 0x04000164 RID: 356
		private PlayableHandle m_Handle;

		// Token: 0x04000165 RID: 357
		private static readonly AnimationRemoveScalePlayable m_NullPlayable = new AnimationRemoveScalePlayable(PlayableHandle.Null);
	}
}
