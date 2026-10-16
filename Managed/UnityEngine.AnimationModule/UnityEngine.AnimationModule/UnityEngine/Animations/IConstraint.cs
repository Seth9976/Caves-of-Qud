using System;
using System.Collections.Generic;

namespace UnityEngine.Animations
{
	// Token: 0x0200006D RID: 109
	public interface IConstraint
	{
		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060006A8 RID: 1704
		// (set) Token: 0x060006A9 RID: 1705
		float weight { get; set; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060006AA RID: 1706
		// (set) Token: 0x060006AB RID: 1707
		bool constraintActive { get; set; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060006AC RID: 1708
		// (set) Token: 0x060006AD RID: 1709
		bool locked { get; set; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060006AE RID: 1710
		int sourceCount { get; }

		// Token: 0x060006AF RID: 1711
		int AddSource(ConstraintSource source);

		// Token: 0x060006B0 RID: 1712
		void RemoveSource(int index);

		// Token: 0x060006B1 RID: 1713
		ConstraintSource GetSource(int index);

		// Token: 0x060006B2 RID: 1714
		void SetSource(int index, ConstraintSource source);

		// Token: 0x060006B3 RID: 1715
		void GetSources(List<ConstraintSource> sources);

		// Token: 0x060006B4 RID: 1716
		void SetSources(List<ConstraintSource> sources);
	}
}
