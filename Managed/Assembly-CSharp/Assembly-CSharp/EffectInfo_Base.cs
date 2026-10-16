using System;
using UnityEngine;

// Token: 0x020000C7 RID: 199
[Serializable]
public class EffectInfo_Base
{
	// Token: 0x060007FB RID: 2043 RVA: 0x000336EE File Offset: 0x000318EE
	public Func<float, float> GetCurveFunction()
	{
		if (this.customCurve)
		{
			return (float _t) => this.curve.Evaluate(_t);
		}
		return exEase.GetEaseFunc(this.curveType);
	}

	// Token: 0x04000565 RID: 1381
	public float duration = 1f;

	// Token: 0x04000566 RID: 1382
	public exEase.Type curveType;

	// Token: 0x04000567 RID: 1383
	public bool customCurve;

	// Token: 0x04000568 RID: 1384
	public AnimationCurve curve;
}
