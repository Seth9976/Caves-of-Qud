using System;
using XRL.World;

namespace XRL.UI.ObjectFinderClassifiers
{
	// Token: 0x020014E2 RID: 5346
	public class Walls : ObjectFinder.Classifier
	{
		// Token: 0x0600D112 RID: 53522 RVA: 0x004DA643 File Offset: 0x004D8843
		public override bool Check(GameObject go, ObjectFinder.Context context)
		{
			return go.IsWall();
		}
	}
}
