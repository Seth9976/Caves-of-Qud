using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.Playables
{
	// Token: 0x02000589 RID: 1417
	[StaticAccessor("TextureMixerPlayableBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	[NativeHeader("Runtime/Export/Director/TextureMixerPlayable.bindings.h")]
	[NativeHeader("Runtime/Graphics/Director/TextureMixerPlayable.h")]
	[RequiredByNativeCode]
	public struct TextureMixerPlayable : IPlayable, IEquatable<TextureMixerPlayable>
	{
		// Token: 0x060036D9 RID: 14041 RVA: 0x0006C478 File Offset: 0x0006A678
		public static TextureMixerPlayable Create(PlayableGraph graph)
		{
			PlayableHandle playableHandle = TextureMixerPlayable.CreateHandle(graph);
			return new TextureMixerPlayable(playableHandle);
		}

		// Token: 0x060036DA RID: 14042 RVA: 0x0006C498 File Offset: 0x0006A698
		private static PlayableHandle CreateHandle(PlayableGraph graph)
		{
			PlayableHandle @null = PlayableHandle.Null;
			bool flag = !TextureMixerPlayable.CreateTextureMixerPlayableInternal(ref graph, ref @null);
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

		// Token: 0x060036DB RID: 14043 RVA: 0x0006C4CC File Offset: 0x0006A6CC
		internal TextureMixerPlayable(PlayableHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOfType<TextureMixerPlayable>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an TextureMixerPlayable.");
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x060036DC RID: 14044 RVA: 0x0006C508 File Offset: 0x0006A708
		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x060036DD RID: 14045 RVA: 0x0006C520 File Offset: 0x0006A720
		public static implicit operator Playable(TextureMixerPlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		// Token: 0x060036DE RID: 14046 RVA: 0x0006C540 File Offset: 0x0006A740
		public static explicit operator TextureMixerPlayable(Playable playable)
		{
			return new TextureMixerPlayable(playable.GetHandle());
		}

		// Token: 0x060036DF RID: 14047 RVA: 0x0006C560 File Offset: 0x0006A760
		public bool Equals(TextureMixerPlayable other)
		{
			return this.GetHandle() == other.GetHandle();
		}

		// Token: 0x060036E0 RID: 14048
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CreateTextureMixerPlayableInternal(ref PlayableGraph graph, ref PlayableHandle handle);

		// Token: 0x040012DE RID: 4830
		private PlayableHandle m_Handle;
	}
}
