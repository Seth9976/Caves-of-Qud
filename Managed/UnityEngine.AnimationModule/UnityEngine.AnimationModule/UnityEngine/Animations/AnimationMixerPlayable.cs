using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Animations
{
	// Token: 0x02000055 RID: 85
	[StaticAccessor("AnimationMixerPlayableBindings", StaticAccessorType.DoubleColon)]
	[RequiredByNativeCode]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationMixerPlayable.bindings.h")]
	[NativeHeader("Modules/Animation/Director/AnimationMixerPlayable.h")]
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	public struct AnimationMixerPlayable : IPlayable, IEquatable<AnimationMixerPlayable>
	{
		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060004A5 RID: 1189 RVA: 0x0000916C File Offset: 0x0000736C
		public static AnimationMixerPlayable Null
		{
			get
			{
				return AnimationMixerPlayable.m_NullPlayable;
			}
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00009184 File Offset: 0x00007384
		[Obsolete("normalizeWeights is obsolete. It has no effect and will be removed.")]
		public static AnimationMixerPlayable Create(PlayableGraph graph, int inputCount, bool normalizeWeights)
		{
			return AnimationMixerPlayable.Create(graph, inputCount);
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x000091A0 File Offset: 0x000073A0
		public static AnimationMixerPlayable Create(PlayableGraph graph, int inputCount = 0)
		{
			PlayableHandle playableHandle = AnimationMixerPlayable.CreateHandle(graph, inputCount);
			return new AnimationMixerPlayable(playableHandle);
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x000091C0 File Offset: 0x000073C0
		private static PlayableHandle CreateHandle(PlayableGraph graph, int inputCount = 0)
		{
			PlayableHandle @null = PlayableHandle.Null;
			bool flag = !AnimationMixerPlayable.CreateHandleInternal(graph, ref @null);
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

		// Token: 0x060004A9 RID: 1193 RVA: 0x000091FC File Offset: 0x000073FC
		internal AnimationMixerPlayable(PlayableHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOfType<AnimationMixerPlayable>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an AnimationMixerPlayable.");
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x00009238 File Offset: 0x00007438
		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x00009250 File Offset: 0x00007450
		public static implicit operator Playable(AnimationMixerPlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00009270 File Offset: 0x00007470
		public static explicit operator AnimationMixerPlayable(Playable playable)
		{
			return new AnimationMixerPlayable(playable.GetHandle());
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x00009290 File Offset: 0x00007490
		public bool Equals(AnimationMixerPlayable other)
		{
			return this.GetHandle() == other.GetHandle();
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x000092B4 File Offset: 0x000074B4
		[NativeThrows]
		private static bool CreateHandleInternal(PlayableGraph graph, ref PlayableHandle handle)
		{
			return AnimationMixerPlayable.CreateHandleInternal_Injected(ref graph, ref handle);
		}

		// Token: 0x060004B0 RID: 1200
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CreateHandleInternal_Injected([In] ref PlayableGraph graph, ref PlayableHandle handle);

		// Token: 0x0400015B RID: 347
		private PlayableHandle m_Handle;

		// Token: 0x0400015C RID: 348
		private static readonly AnimationMixerPlayable m_NullPlayable = new AnimationMixerPlayable(PlayableHandle.Null);
	}
}
