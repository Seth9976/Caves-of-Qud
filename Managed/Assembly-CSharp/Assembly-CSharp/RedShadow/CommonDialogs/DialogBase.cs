using System;
using System.Collections;
using System.Collections.Generic;
using RedShadow.Tween;
using UnityEngine;

namespace RedShadow.CommonDialogs
{
	// Token: 0x020001F4 RID: 500
	public class DialogBase : MonoBehaviour
	{
		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x060011E4 RID: 4580 RVA: 0x0006AA95 File Offset: 0x00068C95
		// (set) Token: 0x060011E5 RID: 4581 RVA: 0x0006AA9D File Offset: 0x00068C9D
		public bool IsVisible { get; private set; }

		// Token: 0x060011E6 RID: 4582 RVA: 0x0006AAA8 File Offset: 0x00068CA8
		protected virtual void Awake()
		{
			if (base.transform.Find("BlockingPanel") != null)
			{
				this._blockingPanel = base.transform.Find("BlockingPanel").gameObject;
			}
			if (base.transform.Find("Window") != null)
			{
				this._mainWindow = base.transform.Find("Window").gameObject;
			}
		}

		// Token: 0x060011E7 RID: 4583 RVA: 0x0006AB1C File Offset: 0x00068D1C
		public virtual void Start()
		{
			if (this._blockingPanel != null)
			{
				this._blockingPanel.SetActive(false);
			}
			if (this._mainWindow != null)
			{
				this._mainWindow.SetActive(false);
				this._mainWindow.transform.eulerAngles = new Vector3(0f, 90f, 0f);
				return;
			}
			if (!this.DestroyOnClose)
			{
				base.gameObject.SetActive(false);
			}
		}

		// Token: 0x060011E8 RID: 4584 RVA: 0x0006AB96 File Offset: 0x00068D96
		public virtual void Update()
		{
			if (Input.GetKeyDown(KeyCode.Escape) && this.isTop())
			{
				this.cancel();
			}
		}

		// Token: 0x060011E9 RID: 4585 RVA: 0x0006ABAF File Offset: 0x00068DAF
		public virtual void cancel()
		{
			this.hide();
		}

		// Token: 0x060011EA RID: 4586 RVA: 0x0006ABB8 File Offset: 0x00068DB8
		protected virtual void show()
		{
			this.IsVisible = true;
			DialogBase.VisibleDialogs.Add(this);
			if (this._blockingPanel != null)
			{
				this._blockingPanel.SetActive(true);
			}
			if (this._mainWindow != null)
			{
				this._mainWindow.SetActive(true);
				this._mainWindow.transform.rotateTo(new Vector3(0f, 0f, 0f)).duration(0.2f);
				return;
			}
			if (!this.DestroyOnClose)
			{
				base.gameObject.SetActive(true);
			}
		}

		// Token: 0x060011EB RID: 4587 RVA: 0x0006AC4F File Offset: 0x00068E4F
		protected virtual IEnumerator show_co(float delay)
		{
			yield return new WaitForSeconds(delay);
			this.show();
			yield break;
		}

		// Token: 0x060011EC RID: 4588 RVA: 0x0006AC68 File Offset: 0x00068E68
		protected virtual void hide()
		{
			this.IsVisible = false;
			DialogBase.VisibleDialogs.Remove(this);
			if (this._blockingPanel != null)
			{
				this._blockingPanel.SetActive(false);
			}
			if (this._mainWindow != null)
			{
				TweenManager.delayedCall(0.2f, delegate
				{
					this._mainWindow.SetActive(false);
				});
				this._mainWindow.transform.rotateTo(new Vector3(0f, 90f, 0f)).duration(0.2f);
			}
			else if (!this.DestroyOnClose)
			{
				base.gameObject.SetActive(false);
			}
			if (this.DestroyOnClose)
			{
				global::UnityEngine.Object.Destroy(base.gameObject, 0.5f);
			}
		}

		// Token: 0x060011ED RID: 4589 RVA: 0x0006AD23 File Offset: 0x00068F23
		public bool isTop()
		{
			return DialogBase.VisibleDialogs.Count > 0 && DialogBase.VisibleDialogs[DialogBase.VisibleDialogs.Count - 1] == this;
		}

		// Token: 0x060011EE RID: 4590 RVA: 0x0006AD50 File Offset: 0x00068F50
		public static DialogBase getTopmost()
		{
			if (DialogBase.VisibleDialogs.Count > 0)
			{
				return DialogBase.VisibleDialogs[DialogBase.VisibleDialogs.Count - 1];
			}
			return null;
		}

		// Token: 0x060011EF RID: 4591 RVA: 0x0006AD78 File Offset: 0x00068F78
		public void ensureVisible()
		{
			RectTransform rectTransform;
			if (this._mainWindow != null)
			{
				rectTransform = this._mainWindow.GetComponent<RectTransform>();
			}
			else
			{
				rectTransform = base.GetComponent<RectTransform>();
			}
			Vector3[] array = new Vector3[4];
			rectTransform.GetWorldCorners(array);
			RectTransform component = base.GetComponent<RectTransform>();
			Vector3[] array2 = new Vector3[4];
			component.GetWorldCorners(array2);
			float num = rectTransform.position.x;
			float num2 = rectTransform.position.y;
			if (array[0].x < array2[0].x)
			{
				num += array2[0].x - array[0].x;
			}
			if (array[0].y < array2[0].y)
			{
				num2 += array2[0].y - array[0].y;
			}
			if (array[2].x > array2[2].x)
			{
				num -= array[2].x - array2[2].x;
			}
			if (array[2].y > array2[2].y)
			{
				num2 -= array[2].y - array2[2].y;
			}
			rectTransform.position = new Vector2(num, num2);
		}

		// Token: 0x04000BE5 RID: 3045
		public bool DestroyOnClose = true;

		// Token: 0x04000BE6 RID: 3046
		protected static List<DialogBase> VisibleDialogs = new List<DialogBase>();

		// Token: 0x04000BE7 RID: 3047
		protected GameObject _blockingPanel;

		// Token: 0x04000BE8 RID: 3048
		protected GameObject _mainWindow;
	}
}
