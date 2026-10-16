using System;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x02000053 RID: 83
	public interface IPropertyPreview
	{
		// Token: 0x06000304 RID: 772
		void GatherProperties(PlayableDirector director, IPropertyCollector driver);
	}
}
