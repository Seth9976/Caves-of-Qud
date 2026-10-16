using System;
using XRL.World;

namespace XRL.UI.ObjectFinderClassifiers
{
	// Token: 0x020014E3 RID: 5347
	public class Cosmetic : ObjectFinder.Classifier
	{
		// Token: 0x0600D114 RID: 53524 RVA: 0x004DA653 File Offset: 0x004D8853
		public override bool Check(GameObject go, ObjectFinder.Context context)
		{
			return go.HasTag("Cosmetic");
		}
	}
}
