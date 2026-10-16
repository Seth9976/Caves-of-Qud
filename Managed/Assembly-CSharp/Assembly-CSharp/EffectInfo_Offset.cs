using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000CA RID: 202
[Serializable]
public class EffectInfo_Offset : EffectInfo_Base
{
	// Token: 0x06000802 RID: 2050 RVA: 0x000337FC File Offset: 0x000319FC
	public Vector2 GetValue(EffectEventType _type)
	{
		for (int i = 0; i < this.propInfos.Count; i++)
		{
			EffectInfo_Offset.PropInfo propInfo = this.propInfos[i];
			if (propInfo.type == _type)
			{
				return propInfo.val;
			}
		}
		return this.normal;
	}

	// Token: 0x0400056F RID: 1391
	public exSpriteBase target;

	// Token: 0x04000570 RID: 1392
	public Vector2 normal = Vector2.one;

	// Token: 0x04000571 RID: 1393
	public List<EffectInfo_Offset.PropInfo> propInfos = new List<EffectInfo_Offset.PropInfo>();

	// Token: 0x020015C9 RID: 5577
	[Serializable]
	public class PropInfo
	{
		// Token: 0x040057BE RID: 22462
		public EffectEventType type;

		// Token: 0x040057BF RID: 22463
		public Vector2 val;
	}
}
