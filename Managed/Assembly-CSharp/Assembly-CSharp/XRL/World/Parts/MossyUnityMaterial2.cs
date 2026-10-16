using System;

namespace XRL.World.Parts
{
	// Token: 0x020012DF RID: 4831
	public class MossyUnityMaterial2 : PrefabImposter
	{
		// Token: 0x0600BE1F RID: 48671 RVA: 0x0045AC9F File Offset: 0x00458E9F
		public MossyUnityMaterial2()
		{
			this.Prefab = "Prefabs/Imposters/Mossy";
			this.Z = -1;
		}

		// Token: 0x0600BE20 RID: 48672 RVA: 0x0045ACB9 File Offset: 0x00458EB9
		public override void Initialize()
		{
			GameObject parentObject = this.ParentObject;
			string text = ((parentObject != null) ? parentObject.Render.Tile : null);
			string text2 = "~";
			GameObject parentObject2 = this.ParentObject;
			this.Config = text + text2 + ((parentObject2 != null) ? parentObject2.ID : null);
		}
	}
}
