using System;
using UnityEngine.Scripting;

namespace UnityEngine.Playables
{
	// Token: 0x0200045E RID: 1118
	[RequiredByNativeCode]
	public struct ScriptPlayableOutput : IPlayableOutput
	{
		// Token: 0x06002B61 RID: 11105 RVA: 0x00052EFC File Offset: 0x000510FC
		public static ScriptPlayableOutput Create(PlayableGraph graph, string name)
		{
			PlayableOutputHandle playableOutputHandle;
			bool flag = !graph.CreateScriptOutputInternal(name, out playableOutputHandle);
			ScriptPlayableOutput scriptPlayableOutput;
			if (flag)
			{
				scriptPlayableOutput = ScriptPlayableOutput.Null;
			}
			else
			{
				scriptPlayableOutput = new ScriptPlayableOutput(playableOutputHandle);
			}
			return scriptPlayableOutput;
		}

		// Token: 0x06002B62 RID: 11106 RVA: 0x00052F30 File Offset: 0x00051130
		internal ScriptPlayableOutput(PlayableOutputHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOutputOfType<ScriptPlayableOutput>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not a ScriptPlayableOutput.");
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06002B63 RID: 11107 RVA: 0x00052F6C File Offset: 0x0005116C
		public static ScriptPlayableOutput Null
		{
			get
			{
				return new ScriptPlayableOutput(PlayableOutputHandle.Null);
			}
		}

		// Token: 0x06002B64 RID: 11108 RVA: 0x00052F88 File Offset: 0x00051188
		public PlayableOutputHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x06002B65 RID: 11109 RVA: 0x00052FA0 File Offset: 0x000511A0
		public static implicit operator PlayableOutput(ScriptPlayableOutput output)
		{
			return new PlayableOutput(output.GetHandle());
		}

		// Token: 0x06002B66 RID: 11110 RVA: 0x00052FC0 File Offset: 0x000511C0
		public static explicit operator ScriptPlayableOutput(PlayableOutput output)
		{
			return new ScriptPlayableOutput(output.GetHandle());
		}

		// Token: 0x04000C3F RID: 3135
		private PlayableOutputHandle m_Handle;
	}
}
