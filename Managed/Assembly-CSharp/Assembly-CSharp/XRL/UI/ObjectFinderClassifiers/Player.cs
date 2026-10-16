using System;
using XRL.World;

namespace XRL.UI.ObjectFinderClassifiers
{
	// Token: 0x020014DE RID: 5342
	public class Player : ObjectFinder.Classifier
	{
		// Token: 0x0600D10A RID: 53514 RVA: 0x004DA5D2 File Offset: 0x004D87D2
		public override bool Check(GameObject go, ObjectFinder.Context context)
		{
			return go == The.Player;
		}
	}
}
