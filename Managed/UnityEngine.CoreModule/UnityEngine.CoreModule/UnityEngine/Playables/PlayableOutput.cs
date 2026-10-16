using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Playables
{
	// Token: 0x02000459 RID: 1113
	[RequiredByNativeCode]
	public struct PlayableOutput : IPlayableOutput, IEquatable<PlayableOutput>
	{
		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x06002B17 RID: 11031 RVA: 0x00052640 File Offset: 0x00050840
		public static PlayableOutput Null
		{
			get
			{
				return PlayableOutput.m_NullPlayableOutput;
			}
		}

		// Token: 0x06002B18 RID: 11032 RVA: 0x00052657 File Offset: 0x00050857
		[VisibleToOtherModules]
		internal PlayableOutput(PlayableOutputHandle handle)
		{
			this.m_Handle = handle;
		}

		// Token: 0x06002B19 RID: 11033 RVA: 0x00052664 File Offset: 0x00050864
		public PlayableOutputHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x06002B1A RID: 11034 RVA: 0x0005267C File Offset: 0x0005087C
		public bool IsPlayableOutputOfType<T>() where T : struct, IPlayableOutput
		{
			return this.GetHandle().IsPlayableOutputOfType<T>();
		}

		// Token: 0x06002B1B RID: 11035 RVA: 0x0005269C File Offset: 0x0005089C
		public Type GetPlayableOutputType()
		{
			return this.GetHandle().GetPlayableOutputType();
		}

		// Token: 0x06002B1C RID: 11036 RVA: 0x000526BC File Offset: 0x000508BC
		public bool Equals(PlayableOutput other)
		{
			return this.GetHandle() == other.GetHandle();
		}

		// Token: 0x04000C38 RID: 3128
		private PlayableOutputHandle m_Handle;

		// Token: 0x04000C39 RID: 3129
		private static readonly PlayableOutput m_NullPlayableOutput = new PlayableOutput(PlayableOutputHandle.Null);
	}
}
