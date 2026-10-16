using System;

namespace UnityEngine.Playables
{
	// Token: 0x02000004 RID: 4
	internal static class DataPlayableBinding
	{
		// Token: 0x0600000F RID: 15 RVA: 0x00002230 File Offset: 0x00000430
		public static PlayableBinding Create<TDataStream, TPlayer>(string name, Object key) where TDataStream : new() where TPlayer : Object
		{
			return PlayableBinding.CreateInternal(name, key, typeof(TPlayer), new PlayableBinding.CreateOutputMethod(DataPlayableBinding.CreateDataOutput<TDataStream>));
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002260 File Offset: 0x00000460
		private static PlayableOutput CreateDataOutput<TDataStream>(PlayableGraph graph, string name) where TDataStream : new()
		{
			return DataPlayableOutput.Create<TDataStream>(graph, name);
		}
	}
}
