using System;
using XRL.World;

namespace XRL.UI.ObjectFinderClassifiers
{
	// Token: 0x020014DF RID: 5343
	public class Pools : ObjectFinder.Classifier
	{
		// Token: 0x0600D10C RID: 53516 RVA: 0x004DA5E4 File Offset: 0x004D87E4
		public override bool Check(GameObject go, ObjectFinder.Context context)
		{
			return go.IsOpenLiquidVolume();
		}
	}
}
