using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Playables
{
	// Token: 0x0200044C RID: 1100
	[RequiredByNativeCode]
	public struct Playable : IPlayable, IEquatable<Playable>
	{
		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x06002A40 RID: 10816 RVA: 0x00051524 File Offset: 0x0004F724
		public static Playable Null
		{
			get
			{
				return Playable.m_NullPlayable;
			}
		}

		// Token: 0x06002A41 RID: 10817 RVA: 0x0005153C File Offset: 0x0004F73C
		public static Playable Create(PlayableGraph graph, int inputCount = 0)
		{
			Playable playable = new Playable(graph.CreatePlayableHandle());
			playable.SetInputCount(inputCount);
			return playable;
		}

		// Token: 0x06002A42 RID: 10818 RVA: 0x00051565 File Offset: 0x0004F765
		[VisibleToOtherModules]
		internal Playable(PlayableHandle handle)
		{
			this.m_Handle = handle;
		}

		// Token: 0x06002A43 RID: 10819 RVA: 0x00051570 File Offset: 0x0004F770
		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x06002A44 RID: 10820 RVA: 0x00051588 File Offset: 0x0004F788
		public bool IsPlayableOfType<T>() where T : struct, IPlayable
		{
			return this.GetHandle().IsPlayableOfType<T>();
		}

		// Token: 0x06002A45 RID: 10821 RVA: 0x000515A8 File Offset: 0x0004F7A8
		public Type GetPlayableType()
		{
			return this.GetHandle().GetPlayableType();
		}

		// Token: 0x06002A46 RID: 10822 RVA: 0x000515C8 File Offset: 0x0004F7C8
		public bool Equals(Playable other)
		{
			return this.GetHandle() == other.GetHandle();
		}

		// Token: 0x04000C1A RID: 3098
		private PlayableHandle m_Handle;

		// Token: 0x04000C1B RID: 3099
		private static readonly Playable m_NullPlayable = new Playable(PlayableHandle.Null);
	}
}
