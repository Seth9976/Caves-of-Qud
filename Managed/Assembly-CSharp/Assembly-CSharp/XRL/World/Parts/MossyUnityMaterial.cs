using System;
using System.Collections.Generic;
using ConsoleLib.Console;
using UnityEngine;

namespace XRL.World.Parts
{
	// Token: 0x020012DE RID: 4830
	[Obsolete]
	[Serializable]
	public class MossyUnityMaterial : IPartWithPrefabImposter
	{
		// Token: 0x0600BE1D RID: 48669 RVA: 0x0045ABF6 File Offset: 0x00458DF6
		public MossyUnityMaterial()
		{
			this.prefabID = "Prefabs/Imposters/Mossy";
		}

		// Token: 0x0600BE1E RID: 48670 RVA: 0x0045AC0C File Offset: 0x00458E0C
		public override bool FinalRender(RenderEvent E)
		{
			if (this.ImposterActive && (!this.VisibleOnly || this.ParentObject.IsVisible()))
			{
				List<ImposterExtra.ImposterInfo> imposters = E.Imposters;
				string prefabID = this.prefabID;
				Vector3 vector = new Vector3((float)this.X, (float)this.Y, -1f);
				int num = 0;
				GameObject parentObject = this.ParentObject;
				string text = ((parentObject != null) ? parentObject.Render.Tile : null);
				string text2 = "~";
				GameObject parentObject2 = this.ParentObject;
				imposters.Add(new ImposterExtra.ImposterInfo(prefabID, vector, num, text + text2 + ((parentObject2 != null) ? parentObject2.ID : null)));
			}
			return base.FinalRender(E);
		}
	}
}
