using System;
using ConsoleLib.Console;
using UnityEngine;

namespace XRL.World.Parts
{
	// Token: 0x020011E3 RID: 4579
	public class PrefabImposterFinal : BasePrefabImposter
	{
		// Token: 0x0600B67B RID: 46715 RVA: 0x00429700 File Offset: 0x00427900
		public override bool FinalRender(RenderEvent E)
		{
			if (!this.Disabled)
			{
				E.Imposters.Add(new ImposterExtra.ImposterInfo(this.Prefab, new Vector3((float)this.X, (float)this.Y, (float)this.Z), this.Layer, this.Config));
			}
			return base.FinalRender(E);
		}
	}
}
