using System;
using UnityEngine;
using UnityEngine.UI;

namespace RedShadow.CommonDialogs
{
	// Token: 0x02000205 RID: 517
	public class StatusBar : MonoBehaviour
	{
		// Token: 0x06001294 RID: 4756 RVA: 0x0006E1A0 File Offset: 0x0006C3A0
		protected void Awake()
		{
			this._text = base.transform.Find("Panel/Text").GetComponent<Text>();
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x0006E1BD File Offset: 0x0006C3BD
		public void Update()
		{
			if (this._startTime > 0f && Time.timeSinceLevelLoad > this._startTime + this._duration)
			{
				this._startTime = 0f;
				this._text.text = "";
			}
		}

		// Token: 0x06001296 RID: 4758 RVA: 0x0006E1FB File Offset: 0x0006C3FB
		public StatusBar showText(string text, float duration = 5f)
		{
			this._text.text = text;
			this._duration = duration;
			this._startTime = Time.timeSinceLevelLoad;
			return this;
		}

		// Token: 0x04000C78 RID: 3192
		private Text _text;

		// Token: 0x04000C79 RID: 3193
		private float _startTime;

		// Token: 0x04000C7A RID: 3194
		private float _duration;
	}
}
