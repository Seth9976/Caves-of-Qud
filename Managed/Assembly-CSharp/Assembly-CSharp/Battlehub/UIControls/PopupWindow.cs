using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Battlehub.UIControls
{
	// Token: 0x020003D3 RID: 979
	public class PopupWindow : MonoBehaviour
	{
		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x0600247C RID: 9340 RVA: 0x000B7EC7 File Offset: 0x000B60C7
		public bool IsOpened
		{
			get
			{
				return this.m_isOpened;
			}
		}

		// Token: 0x0600247D RID: 9341 RVA: 0x000B7ECF File Offset: 0x000B60CF
		private void Awake()
		{
			if (this.Prefab != null)
			{
				PopupWindow.m_instance = this;
			}
		}

		// Token: 0x0600247E RID: 9342 RVA: 0x000B7EE8 File Offset: 0x000B60E8
		private void Start()
		{
			if (this.BtnCancel != null)
			{
				this.BtnCancel.onClick.AddListener(new UnityAction(this.OnBtnCancel));
			}
			if (this.BtnOk != null)
			{
				this.BtnOk.onClick.AddListener(new UnityAction(this.OnBtnOk));
			}
		}

		// Token: 0x0600247F RID: 9343 RVA: 0x000B7F49 File Offset: 0x000B6149
		private void Update()
		{
			if (this == PopupWindow.m_instance)
			{
				return;
			}
			if (Input.GetKeyDown(KeyCode.Return))
			{
				this.OnBtnOk();
				return;
			}
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				this.OnBtnCancel();
			}
		}

		// Token: 0x06002480 RID: 9344 RVA: 0x000B7F78 File Offset: 0x000B6178
		private void OnDestroy()
		{
			if (this.BtnCancel != null)
			{
				this.BtnCancel.onClick.RemoveListener(new UnityAction(this.OnBtnCancel));
			}
			if (this.BtnOk != null)
			{
				this.BtnOk.onClick.RemoveListener(new UnityAction(this.OnBtnOk));
			}
		}

		// Token: 0x06002481 RID: 9345 RVA: 0x000B7FDC File Offset: 0x000B61DC
		private void OnBtnOk()
		{
			if (this.OK != null)
			{
				PopupWindowArgs popupWindowArgs = new PopupWindowArgs();
				this.OK.Invoke(popupWindowArgs);
				if (popupWindowArgs.Cancel)
				{
					return;
				}
			}
			if (this.m_okCallback != null)
			{
				PopupWindowArgs popupWindowArgs2 = new PopupWindowArgs();
				this.m_okCallback(popupWindowArgs2);
				if (popupWindowArgs2.Cancel)
				{
					return;
				}
			}
			this.HidePopup();
		}

		// Token: 0x06002482 RID: 9346 RVA: 0x000B8038 File Offset: 0x000B6238
		private void OnBtnCancel()
		{
			if (this.Cancel != null)
			{
				PopupWindowArgs popupWindowArgs = new PopupWindowArgs();
				this.Cancel.Invoke(popupWindowArgs);
				if (popupWindowArgs.Cancel)
				{
					return;
				}
			}
			if (this.m_cancelCallback != null)
			{
				PopupWindowArgs popupWindowArgs2 = new PopupWindowArgs();
				this.m_cancelCallback(popupWindowArgs2);
				if (popupWindowArgs2.Cancel)
				{
					return;
				}
			}
			this.HidePopup();
		}

		// Token: 0x06002483 RID: 9347 RVA: 0x000B8094 File Offset: 0x000B6294
		private void HidePopup()
		{
			if (this.m_openedPopupWindows != null)
			{
				foreach (PopupWindow popupWindow in this.m_openedPopupWindows)
				{
					if (popupWindow != null)
					{
						popupWindow.gameObject.SetActive(true);
					}
				}
			}
			this.m_openedPopupWindows = null;
			base.gameObject.SetActive(false);
			global::UnityEngine.Object.Destroy(base.gameObject);
			this.m_okCallback = null;
			this.m_cancelCallback = null;
			this.m_isOpened = false;
		}

		// Token: 0x06002484 RID: 9348 RVA: 0x000B810C File Offset: 0x000B630C
		private void ShowPopup(string header, Transform body, string ok = null, PopupWindowAction okCallback = null, string cancel = null, PopupWindowAction cancelCallback = null, float width = 500f)
		{
			this.m_openedPopupWindows = (from wnd in global::UnityEngine.Object.FindObjectsByType<PopupWindow>(FindObjectsSortMode.None)
				where wnd.IsOpened && wnd.isActiveAndEnabled
				select wnd).ToArray<PopupWindow>();
			PopupWindow[] openedPopupWindows = this.m_openedPopupWindows;
			for (int i = 0; i < openedPopupWindows.Length; i++)
			{
				openedPopupWindows[i].gameObject.SetActive(false);
			}
			base.gameObject.SetActive(true);
			if (this.TxtHeader != null)
			{
				this.TxtHeader.text = header;
			}
			if (this.Body != null)
			{
				body.SetParent(this.Body, false);
			}
			if (this.BtnOk != null)
			{
				if (string.IsNullOrEmpty(ok))
				{
					this.BtnOk.gameObject.SetActive(false);
				}
				else
				{
					Text componentInChildren = this.BtnOk.GetComponentInChildren<Text>();
					if (componentInChildren != null)
					{
						componentInChildren.text = ok;
					}
				}
			}
			if (this.BtnCancel != null)
			{
				if (string.IsNullOrEmpty(cancel))
				{
					this.BtnCancel.gameObject.SetActive(false);
				}
				else
				{
					Text componentInChildren2 = this.BtnCancel.GetComponentInChildren<Text>();
					if (componentInChildren2 != null)
					{
						componentInChildren2.text = cancel;
					}
				}
			}
			if (this.Panel != null)
			{
				this.Panel.preferredWidth = width;
			}
			this.m_okCallback = okCallback;
			this.m_cancelCallback = cancelCallback;
			this.m_isOpened = true;
		}

		// Token: 0x06002485 RID: 9349 RVA: 0x000B8270 File Offset: 0x000B6470
		public void Close(bool result)
		{
			if (result)
			{
				this.OnBtnOk();
				return;
			}
			this.OnBtnCancel();
		}

		// Token: 0x06002486 RID: 9350 RVA: 0x000B8284 File Offset: 0x000B6484
		public static void Show(string header, string body, string ok, PopupWindowAction okCallback = null, string cancel = null, PopupWindowAction cancelCallback = null, float width = 530f)
		{
			if (PopupWindow.m_instance == null)
			{
				Debug.LogWarning("PopupWindows.m_instance is null");
				return;
			}
			PopupWindow popupWindow = global::UnityEngine.Object.Instantiate<PopupWindow>(PopupWindow.m_instance.Prefab);
			popupWindow.transform.position = Vector3.zero;
			popupWindow.transform.SetParent(PopupWindow.m_instance.transform, false);
			popupWindow.DefaultBody.text = body;
			popupWindow.ShowPopup(header, popupWindow.DefaultBody.transform, ok, okCallback, cancel, cancelCallback, width);
		}

		// Token: 0x06002487 RID: 9351 RVA: 0x000B8308 File Offset: 0x000B6508
		public static void Show(string header, Transform body, string ok, PopupWindowAction okCallback = null, string cancel = null, PopupWindowAction cancelCallback = null, float width = 530f)
		{
			if (PopupWindow.m_instance == null)
			{
				Debug.LogWarning("PopupWindows.m_instance is null");
				return;
			}
			PopupWindow popupWindow = global::UnityEngine.Object.Instantiate<PopupWindow>(PopupWindow.m_instance.Prefab);
			popupWindow.transform.position = Vector3.zero;
			popupWindow.transform.SetParent(PopupWindow.m_instance.transform, false);
			if (popupWindow.DefaultBody != null)
			{
				global::UnityEngine.Object.Destroy(popupWindow.DefaultBody);
			}
			popupWindow.ShowPopup(header, body, ok, okCallback, cancel, cancelCallback, width);
		}

		// Token: 0x04001249 RID: 4681
		[SerializeField]
		private PopupWindow Prefab;

		// Token: 0x0400124A RID: 4682
		[SerializeField]
		private Text DefaultBody;

		// Token: 0x0400124B RID: 4683
		[SerializeField]
		private Text TxtHeader;

		// Token: 0x0400124C RID: 4684
		[SerializeField]
		private Transform Body;

		// Token: 0x0400124D RID: 4685
		[SerializeField]
		private Button BtnCancel;

		// Token: 0x0400124E RID: 4686
		[SerializeField]
		private Button BtnOk;

		// Token: 0x0400124F RID: 4687
		[SerializeField]
		private LayoutElement Panel;

		// Token: 0x04001250 RID: 4688
		public PopupWindowEvent OK;

		// Token: 0x04001251 RID: 4689
		public PopupWindowEvent Cancel;

		// Token: 0x04001252 RID: 4690
		private PopupWindowAction m_okCallback;

		// Token: 0x04001253 RID: 4691
		private PopupWindowAction m_cancelCallback;

		// Token: 0x04001254 RID: 4692
		private PopupWindow[] m_openedPopupWindows;

		// Token: 0x04001255 RID: 4693
		private static PopupWindow m_instance;

		// Token: 0x04001256 RID: 4694
		private bool m_isOpened;
	}
}
