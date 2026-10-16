using System;
using UnityEngine;

namespace RedShadow.Tween
{
	// Token: 0x020001E8 RID: 488
	public class ColorUpdater : IUpdater
	{
		// Token: 0x0600118A RID: 4490 RVA: 0x0006991D File Offset: 0x00067B1D
		public ColorUpdater(Action<object> setter, object startValue, object endValue)
		{
			this._setter = setter;
			this._startValue = (Color)startValue;
			this._endValue = (Color)endValue;
		}

		// Token: 0x0600118B RID: 4491 RVA: 0x00069944 File Offset: 0x00067B44
		public void update(float progress)
		{
			Color color = this._startValue + (this._endValue - this._startValue) * progress;
			this._setter(color);
		}

		// Token: 0x04000B9C RID: 2972
		private readonly Action<object> _setter;

		// Token: 0x04000B9D RID: 2973
		private readonly Color _startValue;

		// Token: 0x04000B9E RID: 2974
		private readonly Color _endValue;
	}
}
