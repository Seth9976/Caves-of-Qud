using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Web.Routing
{
	// Token: 0x020004D8 RID: 1240
	internal sealed class ContentPathSegment : PathSegment
	{
		// Token: 0x0600380C RID: 14348 RVA: 0x00094B47 File Offset: 0x00092D47
		public ContentPathSegment(IList<PathSubsegment> subsegments)
		{
			this.Subsegments = subsegments;
		}

		// Token: 0x17001194 RID: 4500
		// (get) Token: 0x0600380D RID: 14349 RVA: 0x00094B56 File Offset: 0x00092D56
		public bool IsCatchAll
		{
			get
			{
				return this.Subsegments.Any<PathSubsegment>((PathSubsegment seg) => seg is ParameterSubsegment && ((ParameterSubsegment)seg).IsCatchAll);
			}
		}

		// Token: 0x17001195 RID: 4501
		// (get) Token: 0x0600380E RID: 14350 RVA: 0x00094B82 File Offset: 0x00092D82
		// (set) Token: 0x0600380F RID: 14351 RVA: 0x00094B8A File Offset: 0x00092D8A
		public IList<PathSubsegment> Subsegments { get; private set; }
	}
}
