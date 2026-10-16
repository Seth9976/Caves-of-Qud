using System;
using System.Collections.Generic;
using ConsoleLib.Console;
using Qud.UI;
using UnityEngine;

namespace QupKit
{
	// Token: 0x020003BC RID: 956
	public class LegacyViewManager
	{
		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x0600236F RID: 9071 RVA: 0x000B3E11 File Offset: 0x000B2011
		[HideInInspector]
		public Canvas MainCanvas
		{
			get
			{
				if (this._MainCanvas == null)
				{
					this._MainCanvas = GameObject.Find("Legacy Main Canvas").GetComponent<Canvas>();
				}
				return this._MainCanvas;
			}
		}

		// Token: 0x06002370 RID: 9072 RVA: 0x000B3E3C File Offset: 0x000B203C
		public void Create()
		{
			LegacyViewManager.Instance = this;
			foreach (string text in this.Views.Keys)
			{
				this.Views[text].OnCreate();
			}
		}

		// Token: 0x06002371 RID: 9073 RVA: 0x000B3EA4 File Offset: 0x000B20A4
		public void RegisterRemainingViews(List<GameObject> ViewCanvases)
		{
			foreach (GameObject gameObject in ViewCanvases)
			{
				if (gameObject != null && !this.ViewsByCanvas.ContainsKey(gameObject))
				{
					BaseView baseView = new BaseView();
					baseView.AttachTo(gameObject);
					this.AddView(gameObject.name, baseView);
				}
			}
		}

		// Token: 0x06002372 RID: 9074 RVA: 0x000B3F1C File Offset: 0x000B211C
		public void OnCommand(string Command)
		{
			if (this.ActiveView != null)
			{
				this.ActiveView.OnCommand(Command);
			}
		}

		// Token: 0x06002373 RID: 9075 RVA: 0x000B3F32 File Offset: 0x000B2132
		public void Awake()
		{
			LegacyViewManager.Instance = this;
		}

		// Token: 0x06002374 RID: 9076 RVA: 0x000B3F3A File Offset: 0x000B213A
		public void Update()
		{
			if (this.ActiveView != null)
			{
				this.ActiveView.Update();
			}
		}

		// Token: 0x06002375 RID: 9077 RVA: 0x000B3F50 File Offset: 0x000B2150
		public void AddView(string ID, BaseView V)
		{
			V.views = this;
			this.Views.Add(ID, V);
			if (V.rootObject == null)
			{
				GameObject gameObject = new GameObject();
				CanvasGroup canvasGroup = gameObject.AddComponent<CanvasGroup>();
				gameObject.AddComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
				gameObject.name = ID;
				canvasGroup.alpha = 1f;
				canvasGroup.interactable = true;
				canvasGroup.blocksRaycasts = true;
				gameObject.transform.SetParent(this.MainCanvas.transform, false);
				V.rootObject = gameObject;
				V.canvasGroup = canvasGroup;
				V.Layout = new ControlLayout(ControlAnchor.Fill);
			}
			this.ViewsByCanvas.Add(V.rootObject, V);
			V.rootObject.transform.SetParent(this.MainCanvas.gameObject.transform);
			V.rootObject.SetActive(false);
		}

		// Token: 0x06002376 RID: 9078 RVA: 0x000B403A File Offset: 0x000B223A
		public void OnGUI()
		{
			if (this.ActiveView == null)
			{
				return;
			}
			if (this.ActiveView != null)
			{
				this.ActiveView.OnGUI();
			}
		}

		// Token: 0x06002377 RID: 9079 RVA: 0x000B4058 File Offset: 0x000B2258
		public BaseView GetView(string ID)
		{
			return this.Views[ID];
		}

		// Token: 0x06002378 RID: 9080 RVA: 0x000B4068 File Offset: 0x000B2268
		public void SetActiveView(string ID, bool bHideOldView = true, bool bForceEnter = false)
		{
			if (ID == null)
			{
				foreach (BaseView baseView in this.overlappedViews)
				{
					baseView.Leave();
				}
				this.overlappedViews.Clear();
			}
			else
			{
				this.overlappedViews.RemoveAll((BaseView e) => ((e != null) ? e.Name : null) == ID);
			}
			if (!bForceEnter && this.ActiveView != null && this.ActiveView.Name == ID)
			{
				this.ActiveView.EnableInput();
				return;
			}
			Keyboard.ClearInput(false, false);
			if (this.ActiveView != null)
			{
				if (bHideOldView || this.ActiveView.Name.StartsWith("Popup:"))
				{
					this.ActiveView.Leave();
				}
				else
				{
					this.ActiveView.Overlapped();
					this.overlappedViews.Add(this.ActiveView);
				}
				this.ActiveView.DisableInput();
			}
			if (ID == null)
			{
				this.ActiveView = null;
				return;
			}
			if (ID == "ModToolkit")
			{
				UIManager.showWindow("ModToolkit", false);
				this.ActiveView = null;
				return;
			}
			if (!this.Views.ContainsKey(ID))
			{
				Debug.LogError("Unknown view ID:" + ID);
			}
			this.ActiveView = this.Views[ID];
			this.ActiveView.Enter();
			this.ActiveView.EnableInput();
		}

		// Token: 0x040011D9 RID: 4569
		public static LegacyViewManager Instance;

		// Token: 0x040011DA RID: 4570
		public Dictionary<GameObject, BaseView> ViewsByCanvas = new Dictionary<GameObject, BaseView>();

		// Token: 0x040011DB RID: 4571
		public Dictionary<string, BaseView> Views = new Dictionary<string, BaseView>();

		// Token: 0x040011DC RID: 4572
		[HideInInspector]
		private Canvas _MainCanvas;

		// Token: 0x040011DD RID: 4573
		public List<BaseView> overlappedViews = new List<BaseView>();

		// Token: 0x040011DE RID: 4574
		public BaseView ActiveView;
	}
}
