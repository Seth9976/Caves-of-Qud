using System;
using UnityEngine;
using UnityEngine.UI;

namespace Qud.UI
{
	// Token: 0x02000486 RID: 1158
	[RequireComponent(typeof(CanvasGroup))]
	[RequireComponent(typeof(GraphicRaycaster))]
	[RequireComponent(typeof(Canvas))]
	public class WindowBase : MonoBehaviour
	{
		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06002C77 RID: 11383 RVA: 0x000EFA2D File Offset: 0x000EDC2D
		public static long gameTimeMS
		{
			get
			{
				return GameManager.Time.ElapsedMilliseconds;
			}
		}

		// Token: 0x06002C78 RID: 11384 RVA: 0x000EFA39 File Offset: 0x000EDC39
		public static void queueGameAction(Action a)
		{
			GameManager.Instance.gameQueue.queueTask(a, 0);
		}

		// Token: 0x06002C79 RID: 11385 RVA: 0x000EFA4D File Offset: 0x000EDC4D
		public static void queueUIAction(Action a)
		{
			GameManager.Instance.uiQueue.queueTask(a, 0);
		}

		// Token: 0x06002C7A RID: 11386 RVA: 0x000EFA61 File Offset: 0x000EDC61
		public void DestroyWindow()
		{
			UIManager.instance.DestroyWindow(this);
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06002C7B RID: 11387 RVA: 0x000EFA6E File Offset: 0x000EDC6E
		public bool Visible
		{
			get
			{
				return !(base.gameObject == null) && !(this.canvas == null) && base.gameObject.activeInHierarchy && this.canvas.enabled;
			}
		}

		// Token: 0x06002C7C RID: 11388 RVA: 0x000EFAAA File Offset: 0x000EDCAA
		public void Toggle()
		{
			if (this.Visible)
			{
				this.Hide();
				return;
			}
			this.Show();
		}

		// Token: 0x06002C7D RID: 11389 RVA: 0x000EFAC1 File Offset: 0x000EDCC1
		public bool isCurrentWindow()
		{
			return this.raycaster.enabled;
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06002C7E RID: 11390 RVA: 0x000EFACE File Offset: 0x000EDCCE
		public CanvasGroup canvasGroup
		{
			get
			{
				if (this._canvasGroup == null)
				{
					GameObject gameObject = base.gameObject;
					this._canvasGroup = ((gameObject != null) ? gameObject.GetComponent<CanvasGroup>() : null);
				}
				return this._canvasGroup;
			}
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06002C7F RID: 11391 RVA: 0x000EFAFC File Offset: 0x000EDCFC
		public Canvas canvas
		{
			get
			{
				if (this._canvas == null)
				{
					GameObject gameObject = base.gameObject;
					this._canvas = ((gameObject != null) ? gameObject.GetComponent<Canvas>() : null);
				}
				return this._canvas;
			}
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x06002C80 RID: 11392 RVA: 0x000EFB2A File Offset: 0x000EDD2A
		public GraphicRaycaster raycaster
		{
			get
			{
				if (this._raycaster == null)
				{
					GameObject gameObject = base.gameObject;
					this._raycaster = ((gameObject != null) ? gameObject.GetComponent<GraphicRaycaster>() : null);
				}
				return this._raycaster;
			}
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x06002C81 RID: 11393 RVA: 0x000EFB58 File Offset: 0x000EDD58
		// (set) Token: 0x06002C82 RID: 11394 RVA: 0x000EFB60 File Offset: 0x000EDD60
		public bool takesInput
		{
			get
			{
				return this._takesInput;
			}
			set
			{
				this._takesInput = value;
				if (!value)
				{
					this.raycaster.enabled = false;
					return;
				}
				if (this.canvas.enabled)
				{
					this.raycaster.enabled = true;
				}
			}
		}

		// Token: 0x06002C83 RID: 11395 RVA: 0x000EFB94 File Offset: 0x000EDD94
		public virtual void Init()
		{
			foreach (ScrollRect scrollRect in base.gameObject.GetComponentsInChildren<ScrollRect>())
			{
				if (scrollRect != null)
				{
					scrollRect.scrollSensitivity = 15f;
				}
			}
		}

		// Token: 0x06002C84 RID: 11396 RVA: 0x000EFBD3 File Offset: 0x000EDDD3
		public virtual bool AllowPassthroughInput()
		{
			return false;
		}

		// Token: 0x06002C85 RID: 11397 RVA: 0x000EFBD8 File Offset: 0x000EDDD8
		public virtual void _HideWithoutLeave()
		{
			if (this.canvas != null && this.canvas.enabled)
			{
				this.canvas.enabled = false;
			}
			if (this.raycaster != null && this.raycaster.enabled)
			{
				this.raycaster.enabled = false;
			}
			if (this.canvasGroup != null && this.canvasGroup.interactable)
			{
				this.canvasGroup.interactable = false;
			}
		}

		// Token: 0x06002C86 RID: 11398 RVA: 0x000EFC5C File Offset: 0x000EDE5C
		public virtual void Hide()
		{
			if (this.canvas.enabled)
			{
				this.canvas.enabled = false;
			}
			if (this.raycaster.enabled)
			{
				this.raycaster.enabled = false;
			}
			if (this.canvasGroup != null && this.canvasGroup.interactable)
			{
				this.canvasGroup.interactable = false;
			}
		}

		// Token: 0x06002C87 RID: 11399 RVA: 0x000EFCC4 File Offset: 0x000EDEC4
		public virtual void Show()
		{
			if (!base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(true);
			}
			if (!this.canvas.enabled)
			{
				this.canvas.enabled = true;
			}
			if (!this.raycaster.enabled)
			{
				this.raycaster.enabled = this.takesInput;
			}
			if (this.canvasGroup != null && this.canvasGroup.alpha != 1f)
			{
				this.canvasGroup.alpha = 1f;
			}
			if (this.canvasGroup != null && !this.canvasGroup.interactable)
			{
				this.canvasGroup.interactable = true;
			}
		}

		// Token: 0x06002C88 RID: 11400 RVA: 0x000EFD78 File Offset: 0x000EDF78
		public void BringToFront()
		{
			base.transform.SetAsLastSibling();
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06002C89 RID: 11401 RVA: 0x000EFD85 File Offset: 0x000EDF85
		public RectTransform rectTransform
		{
			get
			{
				if (this._rectTransform == null)
				{
					this._rectTransform = base.gameObject.GetComponent<RectTransform>();
				}
				return this._rectTransform;
			}
		}

		// Token: 0x06002C8A RID: 11402 RVA: 0x000EFDAC File Offset: 0x000EDFAC
		public void ActivateAtWidth(float width, GameObject go)
		{
			if (go != null)
			{
				if (this.rectTransform.rect.width < width)
				{
					if (go.activeSelf)
					{
						go.SetActive(false);
						return;
					}
				}
				else if (!go.activeSelf)
				{
					go.SetActive(true);
				}
			}
		}

		// Token: 0x040018AA RID: 6314
		public CanvasGroup _canvasGroup;

		// Token: 0x040018AB RID: 6315
		public Canvas _canvas;

		// Token: 0x040018AC RID: 6316
		public GraphicRaycaster _raycaster;

		// Token: 0x040018AD RID: 6317
		public bool _takesInput = true;

		// Token: 0x040018AE RID: 6318
		private RectTransform _rectTransform;

		// Token: 0x040018AF RID: 6319
		public RectTransform topRectTransform;

		// Token: 0x040018B0 RID: 6320
		public RectTransform bottomRectTransform;
	}
}
