using System;

namespace UnityEngine.Playables
{
	// Token: 0x0200045D RID: 1117
	public static class ScriptPlayableBinding
	{
		// Token: 0x06002B5F RID: 11103 RVA: 0x00052EB4 File Offset: 0x000510B4
		public static PlayableBinding Create(string name, Object key, Type type)
		{
			return PlayableBinding.CreateInternal(name, key, type, new PlayableBinding.CreateOutputMethod(ScriptPlayableBinding.CreateScriptOutput));
		}

		// Token: 0x06002B60 RID: 11104 RVA: 0x00052EDC File Offset: 0x000510DC
		private static PlayableOutput CreateScriptOutput(PlayableGraph graph, string name)
		{
			return ScriptPlayableOutput.Create(graph, name);
		}
	}
}
