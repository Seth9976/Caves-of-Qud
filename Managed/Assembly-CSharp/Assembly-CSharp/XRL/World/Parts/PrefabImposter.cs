using System;
using ConsoleLib.Console;
using UnityEngine;

namespace XRL.World.Parts
{
	// Token: 0x020011E2 RID: 4578
	public class PrefabImposter : BasePrefabImposter
	{
		// Token: 0x0600B679 RID: 46713 RVA: 0x00429694 File Offset: 0x00427894
		public override bool Render(RenderEvent E)
		{
			if (!this.Disabled && E.Visible && E.Lit > LightLevel.None)
			{
				E.Imposters.Add(new ImposterExtra.ImposterInfo(this.Prefab, new Vector3((float)this.X, (float)this.Y, (float)this.Z), this.Layer, this.Config));
			}
			return true;
		}
	}
}
