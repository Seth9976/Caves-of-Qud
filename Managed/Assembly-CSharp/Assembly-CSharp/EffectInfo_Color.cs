using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000C9 RID: 201
[Serializable]
public class EffectInfo_Color : EffectInfo_Base
{
	// Token: 0x06000800 RID: 2048 RVA: 0x00033798 File Offset: 0x00031998
	public Color GetValue(EffectEventType _type)
	{
		for (int i = 0; i < this.propInfos.Count; i++)
		{
			EffectInfo_Color.PropInfo propInfo = this.propInfos[i];
			if (propInfo.type == _type)
			{
				return propInfo.val;
			}
		}
		return this.normal;
	}

	// Token: 0x0400056C RID: 1388
	public exSpriteBase target;

	// Token: 0x0400056D RID: 1389
	public Color normal = Color.white;

	// Token: 0x0400056E RID: 1390
	public List<EffectInfo_Color.PropInfo> propInfos = new List<EffectInfo_Color.PropInfo>();

	// Token: 0x020015C8 RID: 5576
	[Serializable]
	public class PropInfo
	{
		// Token: 0x040057BC RID: 22460
		public EffectEventType type;

		// Token: 0x040057BD RID: 22461
		public Color val;
	}
}
