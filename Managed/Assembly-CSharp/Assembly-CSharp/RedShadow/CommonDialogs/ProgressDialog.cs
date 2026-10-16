using System;
using UnityEngine;
using UnityEngine.UI;

namespace RedShadow.CommonDialogs
{
	// Token: 0x02000204 RID: 516
	public class ProgressDialog : DialogBase
	{
		// Token: 0x0600128B RID: 4747 RVA: 0x0006E040 File Offset: 0x0006C240
		protected override void Awake()
		{
			base.Awake();
			this._messageText = base.transform.Find("Window/MessagePanel/Text").GetComponent<Text>();
			this._bar = base.transform.Find("Window/ProgressBar").GetComponent<RectTransform>();
			this._progress = base.transform.Find("Window/ProgressBar/Panel").GetComponent<RectTransform>();
		}

		// Token: 0x0600128C RID: 4748 RVA: 0x0006E0A4 File Offset: 0x0006C2A4
		public override void Update()
		{
			base.Update();
			if (base.IsVisible && this.IsIndeterminate)
			{
				this._indeterminateValue += Time.deltaTime / 5f;
				if (this._indeterminateValue > 1f)
				{
					this._indeterminateValue = 0f;
				}
				this.setProgress(this._indeterminateValue);
			}
		}

		// Token: 0x0600128D RID: 4749 RVA: 0x0006E104 File Offset: 0x0006C304
		public void show(string text)
		{
			this.show(text, true);
		}

		// Token: 0x0600128E RID: 4750 RVA: 0x0006E10E File Offset: 0x0006C30E
		public void show(string text, bool indeterminate)
		{
			this.setText(text);
			this.IsIndeterminate = indeterminate;
			this._indeterminateValue = 0f;
			base.StartCoroutine(this.show_co(0.01f));
		}

		// Token: 0x0600128F RID: 4751 RVA: 0x0006E13C File Offset: 0x0006C33C
		public void close()
		{
			this.hide();
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x0006E144 File Offset: 0x0006C344
		public ProgressDialog setText(string text)
		{
			this._messageText.text = text;
			return this;
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x0006E154 File Offset: 0x0006C354
		public ProgressDialog setProgress(float value)
		{
			float x = this._bar.sizeDelta.x;
			this._progress.sizeDelta = new Vector2(value * x, this._progress.sizeDelta.y);
			return this;
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x0006E196 File Offset: 0x0006C396
		public override void cancel()
		{
		}

		// Token: 0x04000C73 RID: 3187
		public bool IsIndeterminate;

		// Token: 0x04000C74 RID: 3188
		private Text _messageText;

		// Token: 0x04000C75 RID: 3189
		private RectTransform _bar;

		// Token: 0x04000C76 RID: 3190
		private RectTransform _progress;

		// Token: 0x04000C77 RID: 3191
		private float _indeterminateValue;
	}
}
