using System;
using XRL.World;

namespace Sheeter
{
	// Token: 0x0200030B RID: 779
	public class BlueprintElement : Attribute
	{
		// Token: 0x06001CDD RID: 7389 RVA: 0x0008DDF9 File Offset: 0x0008BFF9
		public virtual string GetFrom(GameObjectBlueprint Blueprint)
		{
			return null;
		}

		// Token: 0x04000F53 RID: 3923
		public string Key;
	}
}
