using System;
using System.Collections.Generic;
using UnityEngine.Scripting;

namespace UnityEngine.Playables
{
	// Token: 0x0200044E RID: 1102
	[RequiredByNativeCode]
	[AssetFileNameExtension("playable", new string[] { })]
	[Serializable]
	public abstract class PlayableAsset : ScriptableObject, IPlayableAsset
	{
		// Token: 0x06002A4B RID: 10827
		public abstract Playable CreatePlayable(PlayableGraph graph, GameObject owner);

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x06002A4C RID: 10828 RVA: 0x00051600 File Offset: 0x0004F800
		public virtual double duration
		{
			get
			{
				return PlayableBinding.DefaultDuration;
			}
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x06002A4D RID: 10829 RVA: 0x00051618 File Offset: 0x0004F818
		public virtual IEnumerable<PlayableBinding> outputs
		{
			get
			{
				return PlayableBinding.None;
			}
		}

		// Token: 0x06002A4E RID: 10830 RVA: 0x00051630 File Offset: 0x0004F830
		[RequiredByNativeCode]
		internal unsafe static void Internal_CreatePlayable(PlayableAsset asset, PlayableGraph graph, GameObject go, IntPtr ptr)
		{
			bool flag = asset == null;
			Playable playable;
			if (flag)
			{
				playable = Playable.Null;
			}
			else
			{
				playable = asset.CreatePlayable(graph, go);
			}
			Playable* ptr2 = (Playable*)ptr.ToPointer();
			*ptr2 = playable;
		}

		// Token: 0x06002A4F RID: 10831 RVA: 0x0005166C File Offset: 0x0004F86C
		[RequiredByNativeCode]
		internal unsafe static void Internal_GetPlayableAssetDuration(PlayableAsset asset, IntPtr ptrToDouble)
		{
			double duration = asset.duration;
			double* ptr = (double*)ptrToDouble.ToPointer();
			*ptr = duration;
		}
	}
}
