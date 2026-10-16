using System;
using ConsoleLib.Console;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using XRL.UI;

namespace Qud.UI
{
	// Token: 0x0200049B RID: 1179
	public class MovableSceneFrameWindowBase<T> : SingletonWindowBase<T> where T : class, new()
	{
		// Token: 0x06002D30 RID: 11568 RVA: 0x000F4364 File Offset: 0x000F2564
		public float LeftSideTransform()
		{
			return (float)((this.currentSide == GameManager.PreferredSidebarPosition.Left) ? 1 : (-1));
		}

		// Token: 0x06002D31 RID: 11569 RVA: 0x000F4374 File Offset: 0x000F2574
		public void Awake()
		{
			this._initcomplete = false;
		}

		// Token: 0x06002D32 RID: 11570 RVA: 0x000F4380 File Offset: 0x000F2580
		public virtual void Update()
		{
			if (!this._initcomplete)
			{
				this.Init();
			}
			if (!this._initcomplete || !base.gameObject.activeInHierarchy)
			{
				return;
			}
			bool flag = false;
			if (Options.ShiftHidesSidebar && Keyboard.bShift)
			{
				GameManager instance = GameManager.Instance;
				if (((instance != null) ? instance.CurrentGameView : null) == "Stage")
				{
					flag = true;
				}
			}
			if (flag)
			{
				base.canvas.enabled = false;
			}
			else
			{
				base.canvas.enabled = base.raycaster.enabled;
			}
			if (this.Docked)
			{
				if (this.sceneFrameHeader.gameObject.activeSelf)
				{
					this.sceneFrameHeader.ToggleBackgroundOpacity(2);
					this.sceneFrameHeader.gameObject.SetActive(false);
					this.cornerTab.gameObject.SetActive(false);
				}
				return;
			}
			if (UIManager._WindowFramePin == 0 != this.sceneFrameHeader.gameObject.activeSelf)
			{
				MoveableSceneFrameHeader moveableSceneFrameHeader = this.sceneFrameHeader;
				if (moveableSceneFrameHeader != null)
				{
					moveableSceneFrameHeader.gameObject.SetActive(UIManager._WindowFramePin == 0);
				}
				MovableSceneFrameCornerTab movableSceneFrameCornerTab = this.cornerTab;
				if (movableSceneFrameCornerTab != null)
				{
					movableSceneFrameCornerTab.gameObject.SetActive(UIManager._WindowFramePin == 0);
				}
			}
			if (this.preferredXOffset != this.windowFrame.anchoredPosition.x * this.LeftSideTransform() || this.preferredYOffset != this.windowFrame.anchoredPosition.y || this.preferredWidth != this.windowFrame.sizeDelta.x || this.preferredHeight != this.windowFrame.sizeDelta.y)
			{
				this.EnsureOnscreen();
				this.preferredXOffset = this.windowFrame.anchoredPosition.x * this.LeftSideTransform();
				this.preferredYOffset = this.windowFrame.anchoredPosition.y;
				this.preferredWidth = this.windowFrame.sizeDelta.x;
				this.preferredHeight = this.windowFrame.sizeDelta.y;
				this.SavePreferences();
			}
			if (this.preferredSide != this.currentSide && !this.sceneFrameHeader.sideMovePin)
			{
				this.OnPreferredSidebarPositionChanged(this.preferredSide);
			}
			if (Keyboard.bAlt != this.Masked && !this.Docked)
			{
				if (this.Masked)
				{
					this.toggle.GetComponent<Mask>().Destroy();
				}
				else
				{
					this.toggle.AddComponent<Mask>();
				}
				this.Masked = !this.Masked;
			}
		}

		// Token: 0x06002D33 RID: 11571 RVA: 0x000F45EC File Offset: 0x000F27EC
		public void EnsureOnscreen()
		{
			if (!Application.isPlaying)
			{
				return;
			}
			if (this.windowFrame.sizeDelta.x > UIManager.width)
			{
				this.windowFrame.sizeDelta = new Vector2(UIManager.width, this.windowFrame.sizeDelta.y);
			}
			if (this.windowFrame.sizeDelta.y > 0f)
			{
				this.windowFrame.sizeDelta = new Vector2(this.windowFrame.sizeDelta.x, 0f);
			}
			if (this.windowFrame.sizeDelta.x < 200f)
			{
				this.windowFrame.sizeDelta = new Vector2(200f, this.windowFrame.sizeDelta.y);
			}
			if (this.windowFrame.sizeDelta.y < -(UIManager.height - 100f))
			{
				this.windowFrame.sizeDelta = new Vector2(this.windowFrame.sizeDelta.x, -(UIManager.height - 100f));
			}
			if (this.windowFrame.anchorMin.x <= 0f)
			{
				if (this.windowFrame.anchoredPosition.x < 0f)
				{
					this.windowFrame.anchoredPosition = new Vector2(0f, this.windowFrame.anchoredPosition.y);
				}
			}
			else if (this.windowFrame.anchoredPosition.x > 0f)
			{
				this.windowFrame.anchoredPosition = new Vector2(0f, this.windowFrame.anchoredPosition.y);
			}
			if (Mathf.Abs(this.windowFrame.sizeDelta.x) + Mathf.Abs(this.windowFrame.anchoredPosition.x) > UIManager.width)
			{
				if (this.windowFrame.anchorMin.x <= 0f)
				{
					this.windowFrame.anchoredPosition = new Vector2(UIManager.width - this.windowFrame.sizeDelta.x, this.windowFrame.anchoredPosition.y);
				}
				else
				{
					this.windowFrame.anchoredPosition = new Vector2(this.windowFrame.sizeDelta.x - UIManager.width, this.windowFrame.anchoredPosition.y);
				}
			}
			if (this.windowFrame.anchoredPosition.y < 0f)
			{
				this.windowFrame.anchoredPosition = new Vector2(this.windowFrame.anchoredPosition.x, 0f);
			}
			if (this.windowFrame.anchoredPosition.y > -this.windowFrame.sizeDelta.y)
			{
				this.windowFrame.anchoredPosition = new Vector2(this.windowFrame.anchoredPosition.x, -this.windowFrame.sizeDelta.y);
			}
		}

		// Token: 0x06002D34 RID: 11572 RVA: 0x000F48DA File Offset: 0x000F2ADA
		public void UpdateSideMovePin(bool pinned)
		{
			this.SavePreferences();
		}

		// Token: 0x06002D35 RID: 11573 RVA: 0x000F48E4 File Offset: 0x000F2AE4
		public override void Init()
		{
			if (!UIManager.initComplete)
			{
				return;
			}
			if (this._initcomplete)
			{
				return;
			}
			this._initcomplete = true;
			this.sceneFrameHeader.Init();
			this.sceneFrameHeader.sideMovePinToggled.AddListener(new UnityAction<bool>(this.UpdateSideMovePin));
			if (!GameManager.OnPreferredSidebarPositionUpdatedCallbacks.Contains(new Action<GameManager.PreferredSidebarPosition>(this.OnPreferredSidebarPositionChanged)))
			{
				GameManager.OnPreferredSidebarPositionUpdatedCallbacks.Add(new Action<GameManager.PreferredSidebarPosition>(this.OnPreferredSidebarPositionChanged));
			}
			this.LoadPreferences();
			this.LoadPosition();
			this.EnsureOnscreen();
			base.Init();
		}

		// Token: 0x06002D36 RID: 11574 RVA: 0x000F4978 File Offset: 0x000F2B78
		public virtual void LoadPosition()
		{
			if (this.preferredXOffset != -3.4028235E+38f && this.preferredYOffset != -3.4028235E+38f && this.preferredWidth != -3.4028235E+38f && this.preferredHeight != -3.4028235E+38f)
			{
				if (this.sceneFrameHeader.sideMovePin)
				{
					bool sideMovePin = this.sceneFrameHeader.sideMovePin;
					this.sceneFrameHeader.sideMovePin = false;
					try
					{
						this.OnPreferredSidebarPositionChanged((this.preferredPinnedSide == "left") ? GameManager.PreferredSidebarPosition.Left : GameManager.PreferredSidebarPosition.Right);
					}
					finally
					{
						this.sceneFrameHeader.sideMovePin = sideMovePin;
					}
				}
				this.windowFrame.sizeDelta = new Vector2(this.preferredWidth, this.preferredHeight);
				this.windowFrame.anchoredPosition = new Vector2(this.preferredXOffset, this.preferredYOffset);
			}
		}

		// Token: 0x06002D37 RID: 11575 RVA: 0x000F4A60 File Offset: 0x000F2C60
		public virtual void OnPreferredSidebarPositionChanged(GameManager.PreferredSidebarPosition newSide)
		{
			this.preferredSide = newSide;
			if (this.sceneFrameHeader.sideMovePin || this.Docked)
			{
				return;
			}
			if (this.currentSide != newSide)
			{
				this.currentSide = newSide;
				if (newSide == GameManager.PreferredSidebarPosition.Left)
				{
					this.windowFrame.anchorMin = new Vector2(0f, 0f);
					this.windowFrame.anchorMax = new Vector2(0f, 1f);
					this.windowFrame.pivot = new Vector2(0f, 0f);
					this.windowFrame.anchoredPosition = new Vector2(-this.windowFrame.anchoredPosition.x, this.windowFrame.anchoredPosition.y);
				}
				else if (newSide == GameManager.PreferredSidebarPosition.Right)
				{
					this.windowFrame.anchorMin = new Vector2(1f, 0f);
					this.windowFrame.anchorMax = new Vector2(1f, 1f);
					this.windowFrame.pivot = new Vector2(1f, 0f);
					this.windowFrame.anchoredPosition = new Vector2(-this.windowFrame.anchoredPosition.x, this.windowFrame.anchoredPosition.y);
				}
			}
			this.EnsureOnscreen();
		}

		// Token: 0x06002D38 RID: 11576 RVA: 0x000F4BB4 File Offset: 0x000F2DB4
		public void SavePreferences()
		{
			if (GameManager.Instance.DockMovable > 0)
			{
				return;
			}
			string text = base.gameObject.name + "_windpref_x";
			string text2 = base.gameObject.name + "_windpref_y";
			string text3 = base.gameObject.name + "_windpref_w";
			string text4 = base.gameObject.name + "_windpref_h";
			this.SetFloatPref(text, this.preferredXOffset);
			this.SetFloatPref(text2, this.preferredYOffset);
			this.SetFloatPref(text3, this.preferredWidth);
			this.SetFloatPref(text4, this.preferredHeight);
			PlayerPrefs.SetString(base.gameObject.name + "_windpref_side", (this.currentSide == GameManager.PreferredSidebarPosition.Left) ? "left" : "right");
		}

		// Token: 0x06002D39 RID: 11577 RVA: 0x000F4C8C File Offset: 0x000F2E8C
		public void LoadPreferences()
		{
			string text = base.gameObject.name + "_windpref_x";
			string text2 = base.gameObject.name + "_windpref_y";
			string text3 = base.gameObject.name + "_windpref_w";
			string text4 = base.gameObject.name + "_windpref_h";
			this.preferredXOffset = this.GetFloatPref(text, float.MinValue);
			this.preferredYOffset = this.GetFloatPref(text2, float.MinValue);
			this.preferredWidth = this.GetFloatPref(text3, float.MinValue);
			this.preferredHeight = this.GetFloatPref(text4, float.MinValue);
			this.preferredPinnedSide = PlayerPrefs.GetString(base.gameObject.name + "_windpref_side", null);
		}

		// Token: 0x06002D3A RID: 11578 RVA: 0x000F4D5A File Offset: 0x000F2F5A
		public void SetFloatPref(string id, float value)
		{
			PlayerPrefs.SetFloat(id, value);
		}

		// Token: 0x06002D3B RID: 11579 RVA: 0x000F4D63 File Offset: 0x000F2F63
		public float GetFloatPref(string id, float def)
		{
			return PlayerPrefs.GetFloat(id, def);
		}

		// Token: 0x0400194B RID: 6475
		public GameObject content;

		// Token: 0x0400194C RID: 6476
		public Canvas contentCanvas;

		// Token: 0x0400194D RID: 6477
		public RectTransform windowFrame;

		// Token: 0x0400194E RID: 6478
		public GameObject toggle;

		// Token: 0x0400194F RID: 6479
		public MoveableSceneFrameHeader sceneFrameHeader;

		// Token: 0x04001950 RID: 6480
		public MovableSceneFrameCornerTab cornerTab;

		// Token: 0x04001951 RID: 6481
		public bool Docked;

		// Token: 0x04001952 RID: 6482
		public bool Masked;

		// Token: 0x04001953 RID: 6483
		private bool _initcomplete;

		// Token: 0x04001954 RID: 6484
		private GameManager.PreferredSidebarPosition preferredSide = GameManager.PreferredSidebarPosition.Left;

		// Token: 0x04001955 RID: 6485
		private GameManager.PreferredSidebarPosition currentSide = GameManager.PreferredSidebarPosition.Left;

		// Token: 0x04001956 RID: 6486
		public float preferredXOffset = float.MinValue;

		// Token: 0x04001957 RID: 6487
		public float preferredYOffset = float.MinValue;

		// Token: 0x04001958 RID: 6488
		public float preferredWidth = float.MinValue;

		// Token: 0x04001959 RID: 6489
		public float preferredHeight = float.MinValue;

		// Token: 0x0400195A RID: 6490
		public string preferredPinnedSide;
	}
}
