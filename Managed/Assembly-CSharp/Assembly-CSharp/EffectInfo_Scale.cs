using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000C8 RID: 200
[Serializable]
public class EffectInfo_Scale : EffectInfo_Base
{
	// Token: 0x060007FE RID: 2046 RVA: 0x00033734 File Offset: 0x00031934
	public Vector3 GetValue(EffectEventType _type)
	{
		for (int i = 0; i < this.propInfos.Count; i++)
		{
			EffectInfo_Scale.PropInfo propInfo = this.propInfos[i];
			if (propInfo.type == _type)
			{
				return propInfo.val;
			}
		}
		return this.normal;
	}

	// Token: 0x04000569 RID: 1385
	public Transform target;

	// Token: 0x0400056A RID: 1386
	public Vector3 normal = Vector3.one;

	// Token: 0x0400056B RID: 1387
	public List<EffectInfo_Scale.PropInfo> propInfos = new List<EffectInfo_Scale.PropInfo>();

	// Token: 0x020015C7 RID: 5575
	[Serializable]
	public class PropInfo
	{
		// Token: 0x040057BA RID: 22458
		public EffectEventType type;

		// Token: 0x040057BB RID: 22459
		public Vector3 val;
	}
}
