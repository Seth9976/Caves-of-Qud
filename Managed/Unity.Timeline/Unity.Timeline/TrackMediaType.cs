using System;

namespace UnityEngine.Timeline
{
	// Token: 0x02000040 RID: 64
	[AttributeUsage(AttributeTargets.Class)]
	[Obsolete("TrackMediaType has been deprecated. It is no longer required, and will be removed in a future release.", false)]
	public class TrackMediaType : Attribute
	{
		// Token: 0x060002CA RID: 714 RVA: 0x00009DE4 File Offset: 0x00007FE4
		public TrackMediaType(TimelineAsset.MediaType mt)
		{
			this.m_MediaType = mt;
		}

		// Token: 0x040000EF RID: 239
		public readonly TimelineAsset.MediaType m_MediaType;
	}
}
