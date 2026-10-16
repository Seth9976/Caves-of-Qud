using System;

namespace XRL.World
{
	// Token: 0x0200098A RID: 2442
	public class IWorldBuilderExtension
	{
		// Token: 0x0600671B RID: 26395 RVA: 0x002008B4 File Offset: 0x001FEAB4
		public virtual void OnBeforeBuild(string world, object builder)
		{
		}

		// Token: 0x0600671C RID: 26396 RVA: 0x002008B6 File Offset: 0x001FEAB6
		public virtual void OnAfterBuild(string world, object builder)
		{
		}
	}
}
