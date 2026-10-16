using System;
using XRL.World;

namespace XRL.UI.ObjectFinderClassifiers
{
	// Token: 0x020014DD RID: 5341
	public class Everything : ObjectFinder.Classifier
	{
		// Token: 0x0600D108 RID: 53512 RVA: 0x004DA5C7 File Offset: 0x004D87C7
		public override bool Check(GameObject gameObject, ObjectFinder.Context context)
		{
			return true;
		}
	}
}
