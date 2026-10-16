using System;

namespace UnityEngine.Timeline
{
	// Token: 0x02000039 RID: 57
	public interface ITimeControl
	{
		// Token: 0x060002A0 RID: 672
		void SetTime(double time);

		// Token: 0x060002A1 RID: 673
		void OnControlTimeStart();

		// Token: 0x060002A2 RID: 674
		void OnControlTimeStop();
	}
}
