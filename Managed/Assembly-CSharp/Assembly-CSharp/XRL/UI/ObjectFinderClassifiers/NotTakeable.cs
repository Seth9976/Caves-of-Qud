using System;
using XRL.World;

namespace XRL.UI.ObjectFinderClassifiers
{
	// Token: 0x020014E1 RID: 5345
	public class NotTakeable : ObjectFinder.Classifier
	{
		// Token: 0x0600D110 RID: 53520 RVA: 0x004DA630 File Offset: 0x004D8830
		public override bool Check(GameObject go, ObjectFinder.Context context)
		{
			return !go.Takeable;
		}
	}
}
