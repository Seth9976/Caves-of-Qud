using System;

namespace RedShadow.Tween
{
	// Token: 0x020001E9 RID: 489
	public class FloatUpdater : IUpdater
	{
		// Token: 0x0600118C RID: 4492 RVA: 0x00069985 File Offset: 0x00067B85
		public FloatUpdater(Action<object> setter, object startValue, object endValue)
		{
			this._setter = setter;
			this._startValue = (float)startValue;
			this._endValue = (float)endValue;
		}

		// Token: 0x0600118D RID: 4493 RVA: 0x000699AC File Offset: 0x00067BAC
		public void update(float progress)
		{
			float num = this._startValue + (this._endValue - this._startValue) * progress;
			this._setter(num);
		}

		// Token: 0x04000B9F RID: 2975
		private readonly Action<object> _setter;

		// Token: 0x04000BA0 RID: 2976
		private readonly float _startValue;

		// Token: 0x04000BA1 RID: 2977
		private readonly float _endValue;
	}
}
