using System;
using UnityEngine;

namespace RedShadow.Tween
{
	// Token: 0x020001E7 RID: 487
	public class Vector3Updater : IUpdater
	{
		// Token: 0x06001188 RID: 4488 RVA: 0x000698B2 File Offset: 0x00067AB2
		public Vector3Updater(Action<object> setter, object startValue, object endValue)
		{
			this._setter = setter;
			this._startValue = (Vector3)startValue;
			this._endValue = (Vector3)endValue;
		}

		// Token: 0x06001189 RID: 4489 RVA: 0x000698DC File Offset: 0x00067ADC
		public void update(float progress)
		{
			Vector3 vector = this._startValue + (this._endValue - this._startValue) * progress;
			this._setter(vector);
		}

		// Token: 0x04000B99 RID: 2969
		private readonly Action<object> _setter;

		// Token: 0x04000B9A RID: 2970
		private readonly Vector3 _startValue;

		// Token: 0x04000B9B RID: 2971
		private readonly Vector3 _endValue;
	}
}
