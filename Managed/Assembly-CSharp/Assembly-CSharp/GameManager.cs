using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Assets.Game.UI.Windows.Stage;
using ConsoleLib.Console;
using Cysharp.Text;
using Genkit;
using Kobold;
using LaundryBear;
using LaundryBear.PlatformServices;
using ModelShark;
using Qud.API;
using Qud.UI;
using QupKit;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XRL;
using XRL.Collections;
using XRL.Core;
using XRL.Rules;
using XRL.UI;
using XRL.World;
using XRL.World.Effects;
using XRL.World.Parts;

// Token: 0x0200005F RID: 95
public class GameManager : BaseGameController<GameManager>
{
	// Token: 0x17000048 RID: 72
	// (get) Token: 0x060002D1 RID: 721 RVA: 0x000106D4 File Offset: 0x0000E8D4
	// (set) Token: 0x060002D2 RID: 722 RVA: 0x000106DC File Offset: 0x0000E8DC
	public static bool focused
	{
		get
		{
			return GameManager._focused;
		}
		set
		{
			if (!GameManager._focused && value)
			{
				Keyboard.ClearInput(false, false);
				Keyboard.ClearMouseEvents(false);
				GameManager.mouseDisable = 0.25f;
			}
			GameManager._focused = value;
			if (value)
			{
				GameManager.focusedEvent.Set();
				return;
			}
			GameManager.focusedEvent.Reset();
		}
	}

	// Token: 0x060002D3 RID: 723 RVA: 0x0001072C File Offset: 0x0000E92C
	public void SetPlayerCell(Point2D C, bool updateCamera = false)
	{
		this.playerTracker.transform.position = this.getTileCenter(C.x, C.y, 20);
		if (this.CurrentPlayerCell != C)
		{
			this.TargetCameraLocation = this.CellToWorldspace(C);
			this.CurrentPlayerCell = C;
		}
		this.AudioListener.transform.position = this.getTileCenter(C.x, C.y, 0);
		this.UpdatePreferredSidebarPosition();
		if (updateCamera)
		{
			this.RefreshLayout(false);
			GameManager.MainCameraLetterbox.OnUpdate();
		}
	}

	// Token: 0x060002D4 RID: 724 RVA: 0x000107BC File Offset: 0x0000E9BC
	public void cinematicPan(string id)
	{
		GameManager.MainCamera.AddComponent<CinematicPan>().Begin(id);
		UIManager.getWindow("Stage").gameObject.SetActive(false);
		UIManager.getWindow("AbilityBar").gameObject.SetActive(false);
		UIManager.getWindow("PlayerStatusBar").gameObject.SetActive(false);
		this.pauseUpdates = true;
	}

	// Token: 0x060002D5 RID: 725 RVA: 0x00010820 File Offset: 0x0000EA20
	public Vector3 CellToWorldspace(Point2D C)
	{
		return new Vector3((float)(40 * -(float)this.tileWidth + C.x * this.tileWidth + 8), 12.5f * (float)this.tileHeight - (float)(C.y * this.tileHeight) - 12f, -10f);
	}

	// Token: 0x060002D6 RID: 726 RVA: 0x00010874 File Offset: 0x0000EA74
	public Vector2 WorldspaceToCanvasSpace(Vector3 WorldSpace)
	{
		return UIManager.instance.WorldspaceToCanvasSpace(WorldSpace, GameManager.cameraMainCamera);
	}

	// Token: 0x060002D7 RID: 727 RVA: 0x00010888 File Offset: 0x0000EA88
	public void UpdatePreferredSidebarPosition()
	{
		if (this.DockMovable > 0 && this.DockMovable != 3)
		{
			return;
		}
		ref Vector2 ptr = this.WorldspaceToCanvasSpace(this.CellToWorldspace(this.CurrentPlayerCell));
		float num = 0.45f;
		float num2 = 0.55f;
		GameManager.PreferredSidebarPosition preferredSidebarPosition = this.currentSidebarPosition;
		float num3 = ptr.x / (float)Screen.width;
		if (this.currentSidebarPosition == GameManager.PreferredSidebarPosition.None)
		{
			preferredSidebarPosition = GameManager.PreferredSidebarPosition.Left;
		}
		else if (this.currentSidebarPosition == GameManager.PreferredSidebarPosition.Right)
		{
			if (num3 > num2)
			{
				preferredSidebarPosition = GameManager.PreferredSidebarPosition.Left;
			}
		}
		else if (this.currentSidebarPosition == GameManager.PreferredSidebarPosition.Left && num3 < num)
		{
			preferredSidebarPosition = GameManager.PreferredSidebarPosition.Right;
		}
		if (preferredSidebarPosition != this.currentSidebarPosition)
		{
			if (preferredSidebarPosition == GameManager.PreferredSidebarPosition.Left)
			{
				global::UnityEngine.Debug.Log("Sidebar swap to left");
			}
			if (preferredSidebarPosition == GameManager.PreferredSidebarPosition.Right)
			{
				global::UnityEngine.Debug.Log("Sidebar swap to right");
			}
			this.currentSidebarPosition = preferredSidebarPosition;
			if (this.DockMovable != 3)
			{
				for (int i = 0; i < GameManager.OnPreferredSidebarPositionUpdatedCallbacks.Count; i++)
				{
					GameManager.OnPreferredSidebarPositionUpdatedCallbacks[i](preferredSidebarPosition);
				}
			}
		}
	}

	// Token: 0x060002D8 RID: 728 RVA: 0x00010968 File Offset: 0x0000EB68
	private global::UnityEngine.GameObject GameObjectFind(string id)
	{
		if (this.findCache.ContainsKey(id))
		{
			if (!(this.findCache[id] == null))
			{
				return this.findCache[id];
			}
			this.findCache.Remove(id);
		}
		global::UnityEngine.GameObject gameObject = global::UnityEngine.GameObject.Find(id);
		if (gameObject != null)
		{
			this.findCache.Add(id, gameObject);
		}
		gameObject == null;
		return gameObject;
	}

	// Token: 0x060002D9 RID: 729 RVA: 0x000109DC File Offset: 0x0000EBDC
	private void SetPlayerCell(int x, int y)
	{
		this.TargetCameraLocation = new Vector3((float)(40 * -(float)this.tileWidth + x * this.tileWidth + 8), 12.5f * (float)this.tileHeight - (float)(y * this.tileHeight) - 12f, -10f);
	}

	// Token: 0x060002DA RID: 730 RVA: 0x00010A2C File Offset: 0x0000EC2C
	public bool ListsDiffer(QudItemList l1, QudItemList l2)
	{
		if (l1 == null || l2 == null)
		{
			return true;
		}
		if (l1.objects.Count != l2.objects.Count)
		{
			return true;
		}
		for (int i = 0; i < l1.objects.Count; i++)
		{
			if (l1.objects[i].go != l2.objects[i].go)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060002DB RID: 731 RVA: 0x00010A98 File Offset: 0x0000EC98
	public void ProcessBufferExtra(IScreenBufferExtra extra)
	{
		QudScreenBufferExtra qudScreenBufferExtra = (QudScreenBufferExtra)extra;
		if (qudScreenBufferExtra.playerPosition != Point2D.invalid)
		{
			this.SetPlayerCell(qudScreenBufferExtra.playerPosition, false);
			this.RefreshLayout(false);
		}
		extra.Free();
	}

	// Token: 0x060002DC RID: 732 RVA: 0x00010AD8 File Offset: 0x0000ECD8
	public void LoadCommandLine()
	{
		string[] commandLineArgs = Environment.GetCommandLineArgs();
		if (commandLineArgs != null && commandLineArgs.Length != 0)
		{
			for (int i = 0; i < commandLineArgs.Length - 1; i++)
			{
				if (commandLineArgs[i].ToUpper().Contains("NOMETRICS"))
				{
					Globals.ForceMetricsOff = true;
					return;
				}
			}
		}
	}

	// Token: 0x060002DD RID: 733 RVA: 0x00010B20 File Offset: 0x0000ED20
	public static void InitializeServicesForStandalone()
	{
		ServiceLocator serviceLocator;
		global::UnityEngine.Object.Instantiate<global::UnityEngine.GameObject>(GameManager.Instance.serviceLocatorPrefabForStandalone).TryGetComponent<ServiceLocator>(out serviceLocator);
		InitializeServices.AddRuntimeServices(serviceLocator);
		IEnumerator enumerator = serviceLocator.Initialize(true);
		while (enumerator.MoveNext())
		{
		}
		PlatformService platformService;
		ServiceLocator.TryGetService<PlatformService>(out platformService);
		if (platformService.SetupLaunchUserAndStorage().Result)
		{
			global::UnityEngine.Debug.Log("Laundry Bear's Platform Layer is ready for use.");
			return;
		}
		global::UnityEngine.Debug.LogError("An error occurred while setting up the user or storage. See logs for details. Halting load.");
	}

	// Token: 0x060002DE RID: 734 RVA: 0x00010B84 File Offset: 0x0000ED84
	private void Awake()
	{
		base.StartCoroutine(this.AwakeRoutine());
	}

	// Token: 0x060002DF RID: 735 RVA: 0x00010B93 File Offset: 0x0000ED93
	private IEnumerator AwakeRoutine()
	{
		GameManager.<AwakeRoutine>d__49 <AwakeRoutine>d__ = new GameManager.<AwakeRoutine>d__49(0);
		<AwakeRoutine>d__.<>4__this = this;
		return <AwakeRoutine>d__;
	}

	// Token: 0x060002E0 RID: 736 RVA: 0x00010BA2 File Offset: 0x0000EDA2
	public void DirtyControlPanel()
	{
		this._overlayOptionsUpdated = true;
	}

	// Token: 0x060002E1 RID: 737 RVA: 0x00010BAC File Offset: 0x0000EDAC
	public void UpdateMinimap()
	{
		Color32[] array = GameManager.minimapColors;
		lock (array)
		{
			this.minimapTexture.SetPixels32(GameManager.minimapColors);
			this.minimapTexture.Apply();
		}
	}

	// Token: 0x17000049 RID: 73
	// (get) Token: 0x060002E2 RID: 738 RVA: 0x00010C00 File Offset: 0x0000EE00
	// (set) Token: 0x060002E3 RID: 739 RVA: 0x00010C08 File Offset: 0x0000EE08
	public bool bAlt
	{
		get
		{
			return this._bAlt;
		}
		set
		{
			this._bAlt = value;
			if (this._bAlt)
			{
				this.GameObjectFind("AltButton").GetComponent<Image>().color = Color.red;
			}
			if (!this._bAlt)
			{
				this.GameObjectFind("AltButton").GetComponent<Image>().color = Color.white;
			}
		}
	}

	// Token: 0x060002E4 RID: 740 RVA: 0x00010C60 File Offset: 0x0000EE60
	private void Quitting()
	{
		GameManager.Playing = false;
	}

	// Token: 0x060002E5 RID: 741 RVA: 0x00010C68 File Offset: 0x0000EE68
	public void Quit()
	{
		if (this.platform.SupportsQuit)
		{
			this.platform.Quit();
		}
	}

	// Token: 0x060002E6 RID: 742 RVA: 0x00010C82 File Offset: 0x0000EE82
	private void OnShutdownRequest()
	{
		this.HandleOnDestroy();
	}

	// Token: 0x060002E7 RID: 743 RVA: 0x00010C8A File Offset: 0x0000EE8A
	private void OnDestroy()
	{
		this.HandleOnDestroy();
	}

	// Token: 0x060002E8 RID: 744 RVA: 0x00010C94 File Offset: 0x0000EE94
	private void HandleOnDestroy()
	{
		if (this._destroyed)
		{
			return;
		}
		try
		{
			XRLGame game = The.Game;
			Task task = ((game != null) ? game.SaveTask : null);
			if (task != null && !task.IsCompleted)
			{
				task.Wait();
			}
			XRLCore.Stop();
			if (XRLCore.CoreThread != null)
			{
				XRLCore.CoreThread.Interrupt();
				XRLCore.CoreThread.Abort();
			}
		}
		catch
		{
		}
		try
		{
			PlatformManager.Shutdown();
		}
		catch (Exception ex)
		{
			MetricsManager.LogException("Platform Shutdown", ex, "game_exception");
		}
		try
		{
			DataManager.Shutdown();
		}
		catch (Exception ex2)
		{
			MetricsManager.LogException("ZoneManager Shutdown", ex2, "game_exception");
		}
		try
		{
			PlatformService platformService;
			ServiceLocator.TryGetService<PlatformService>(out platformService);
			platformService.EndShutdownNoticeSection();
		}
		catch (Exception ex3)
		{
			MetricsManager.LogException("PlatformService Shutdown", ex3, "game_exception");
		}
	}

	// Token: 0x1700004A RID: 74
	// (get) Token: 0x060002E9 RID: 745 RVA: 0x00010D84 File Offset: 0x0000EF84
	[Obsolete("The ViewData property will soon be removed, update to using XRL.UI.UIView on a class.")]
	public Dictionary<string, GameManager.ViewInfo> ViewData
	{
		get
		{
			return this._ViewData;
		}
	}

	// Token: 0x060002EA RID: 746 RVA: 0x00010D8C File Offset: 0x0000EF8C
	public bool HasViewData(string V)
	{
		return V.StartsWith("ModernPopup") || this._ViewData.ContainsKey(V);
	}

	// Token: 0x060002EB RID: 747 RVA: 0x00010DAC File Offset: 0x0000EFAC
	public bool AnyViewInStackIsStage()
	{
		if (this.CurrentGameView == "Stage")
		{
			return true;
		}
		RingDeque<string> gameViewStack = this.GameViewStack;
		lock (gameViewStack)
		{
			using (RingDeque<string>.Enumerator enumerator = this.GameViewStack.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current == "Stage")
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x060002EC RID: 748 RVA: 0x00010E48 File Offset: 0x0000F048
	public bool AnyViewInStackForcefullscreenInModernOrLegacy()
	{
		GameManager.ViewInfo viewData = this.GetViewData(this.CurrentGameView);
		if (viewData == null || !viewData.ForceFullscreen)
		{
			if (!Options.ModernUI)
			{
				GameManager.ViewInfo viewData2 = this.GetViewData(this.CurrentGameView);
				if (viewData2 != null && viewData2.ForceFullscreenInLegacy)
				{
					return true;
				}
			}
			RingDeque<string> gameViewStack = this.GameViewStack;
			lock (gameViewStack)
			{
				foreach (string text in this.GameViewStack)
				{
					GameManager.ViewInfo viewData3 = this.GetViewData(text);
					if (viewData3 == null || !viewData3.ForceFullscreen)
					{
						if (Options.ModernUI)
						{
							continue;
						}
						GameManager.ViewInfo viewData4 = this.GetViewData(text);
						if (viewData4 == null || !viewData4.ForceFullscreenInLegacy)
						{
							continue;
						}
					}
					return true;
				}
			}
			return false;
		}
		return true;
	}

	// Token: 0x060002ED RID: 749 RVA: 0x00010F34 File Offset: 0x0000F134
	public bool AnyViewInStackForcefullscreen()
	{
		try
		{
			GameManager.ViewInfo viewData = this.GetViewData(this.CurrentGameView);
			if (viewData != null && viewData.ForceFullscreen)
			{
				return true;
			}
			RingDeque<string> gameViewStack = this.GameViewStack;
			lock (gameViewStack)
			{
				foreach (string text in this.GameViewStack)
				{
					GameManager.ViewInfo viewData2 = this.GetViewData(text);
					if (viewData2 != null && viewData2.ForceFullscreen)
					{
						return true;
					}
				}
			}
		}
		catch
		{
			return false;
		}
		return false;
	}

	// Token: 0x060002EE RID: 750 RVA: 0x00010FF8 File Offset: 0x0000F1F8
	public bool AnyViewInStack(Predicate<string> test)
	{
		if (test(this.CurrentGameView))
		{
			return true;
		}
		RingDeque<string> gameViewStack = this.GameViewStack;
		lock (gameViewStack)
		{
			foreach (string text in this.GameViewStack)
			{
				if (test(text))
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060002EF RID: 751 RVA: 0x00011090 File Offset: 0x0000F290
	public bool AnyViewInStack(Predicate<GameManager.ViewInfo> test)
	{
		return this.AnyViewInStack((string s) => test(this.GetViewData(s)));
	}

	// Token: 0x060002F0 RID: 752 RVA: 0x000110C4 File Offset: 0x0000F2C4
	public GameManager.ViewInfo GetViewData(string V)
	{
		GameManager.ViewInfo viewInfo;
		if (this._ViewData.TryGetValue(V, out viewInfo))
		{
			return viewInfo;
		}
		if (V.StartsWith("ModernPopup"))
		{
			return this._ViewData["ModernPopup*"];
		}
		return this._ViewData["*Default"];
	}

	// Token: 0x060002F1 RID: 753 RVA: 0x00011111 File Offset: 0x0000F311
	public void OnEndDrag(PointerEventData eventData)
	{
		this.TargetCameraLocation = GameManager.MainCameraLetterbox.DesiredPan;
	}

	// Token: 0x060002F2 RID: 754 RVA: 0x00011124 File Offset: 0x0000F324
	public void OnDrag(Vector2 Delta)
	{
		if (!XRLCore.bThreadFocus)
		{
			return;
		}
		this.TargetCameraLocation -= new Vector3(Delta.x * GameManager.MainCameraLetterbox.UnitsPerPixel, Delta.y * GameManager.MainCameraLetterbox.UnitsPerPixel, 0f);
		if (GameManager.MainCameraLetterbox.Pannable)
		{
			GameManager.MainCameraLetterbox.OnUpdate();
		}
	}

	// Token: 0x060002F3 RID: 755 RVA: 0x0001118C File Offset: 0x0000F38C
	public void OnTileOver(int x, int y)
	{
		if (GameManager.mouseDisable > 0f)
		{
			return;
		}
		if ((this.GetViewData(this.CurrentGameView).WantsTileOver || !Options.ModernUI) && (this.LastTileOver.x != x || this.LastTileOver.y != y))
		{
			this.LastTileOver = new Point2D(x, y);
			List<GameManager.ClickRegion> regions = this.Regions;
			lock (regions)
			{
				for (int i = 0; i < this.nRegions; i++)
				{
					if (this.Regions[i].OverCommand != null && this.Regions[i].Contains(x, y))
					{
						Keyboard.PushMouseEvent(this.Regions[i].OverCommand, x, y, null);
						return;
					}
				}
			}
			Keyboard.PushMouseEvent("PointerOver", x, y, null);
		}
	}

	// Token: 0x060002F4 RID: 756 RVA: 0x00011280 File Offset: 0x0000F480
	public void OnTileClicked(string Event, int x, int y)
	{
		if (GameManager.mouseDisable > 0f)
		{
			return;
		}
		List<GameManager.ClickRegion> regions = this.Regions;
		lock (regions)
		{
			for (int i = 0; i < this.nRegions; i++)
			{
				if (Event == "LeftClick" && this.Regions[i].LeftClickCommand != null && this.Regions[i].Contains(x, y))
				{
					Keyboard.PushMouseEvent(this.Regions[i].LeftClickCommand, x, y, null);
					return;
				}
				if (Event == "RightClick" && this.Regions[i].RightClickCommand != null && this.Regions[i].Contains(x, y))
				{
					Keyboard.PushMouseEvent(this.Regions[i].RightClickCommand, x, y, null);
					return;
				}
			}
		}
		if (!(Event == "LeftClick") && !(Event == "RightClick"))
		{
			Keyboard.PushMouseEvent(Event, x, y, null);
		}
	}

	// Token: 0x060002F5 RID: 757 RVA: 0x000113A4 File Offset: 0x0000F5A4
	public void ZoomIn()
	{
		if (this.TargetZoomFactor < 1f)
		{
			this.TargetZoomFactor = 1f;
			return;
		}
		this.TargetZoomFactor = GameManager.RoundToNearestQuarter(Mathf.Max(1f, Camera.main.GetComponent<LetterboxCamera>().DesiredZoomFactor + 0.25f));
	}

	// Token: 0x060002F6 RID: 758 RVA: 0x000113F4 File Offset: 0x0000F5F4
	public void ZoomOut()
	{
		if (this.TargetZoomFactor == 1f && this.NeedsToPan)
		{
			this.TargetZoomFactor = -1f;
			return;
		}
		if (this.TargetZoomFactor <= 1f)
		{
			if (Options.GetOption("OptionAllowFramelessZoomOut", "") == "Yes")
			{
				this.TargetZoomFactor = -2f;
				return;
			}
		}
		else
		{
			this.TargetZoomFactor = GameManager.RoundToNearestQuarter(Mathf.Max(1f, Camera.main.GetComponent<LetterboxCamera>().DesiredZoomFactor - 0.25f));
		}
	}

	// Token: 0x060002F7 RID: 759 RVA: 0x00011480 File Offset: 0x0000F680
	public static float RoundToNearestQuarter(float value)
	{
		return (float)Math.Round((double)(value * 4f), MidpointRounding.AwayFromZero) / 4f;
	}

	// Token: 0x060002F8 RID: 760 RVA: 0x00011498 File Offset: 0x0000F698
	public void OnScroll(Vector2 Amount)
	{
		if (!Options.MouseScrollWheel)
		{
			return;
		}
		if (!XRLCore.bThreadFocus)
		{
			return;
		}
		if (this.CurrentGameView != "MapEditor" && this.currentUIView != null && this.CurrentGameView != "Popup:Item" && this.currentUIView.UICanvas != "Looker" && this.currentUIView.UICanvas != "Keybinds" && this.currentUIView.UICanvas != Options.StageViewID)
		{
			return;
		}
		GameManager.ViewInfo viewData = this.GetViewData(this.CurrentGameView);
		if ((!viewData.ForceFullscreenInLegacy && !viewData.ForceFullscreen && !viewData.TakesScroll) || this._ActiveGameView == Options.StageViewID || this.CurrentGameView == "MapEditor")
		{
			if (Amount.y < 0f && this.TargetZoomFactor <= 1f)
			{
				this.ZoomOut();
				return;
			}
			if (Amount.y > 0f && this.TargetZoomFactor < 1f)
			{
				this.ZoomIn();
				return;
			}
			float num = 1f;
			float.TryParse(Options.GetOption("OptionZoomSensitivity", ""), out num);
			this.TargetZoomFactor = Mathf.Max(1f, Camera.main.GetComponent<LetterboxCamera>().DesiredZoomFactor + Mathf.Sign(Amount.y) * (0.25f / num));
			return;
		}
		else
		{
			if (Amount.y > 0f)
			{
				Keyboard.PushKey(new Keyboard.XRLKeyEvent(global::UnityEngine.KeyCode.Keypad8, '8', false, false, false), false);
				return;
			}
			Keyboard.PushKey(new Keyboard.XRLKeyEvent(global::UnityEngine.KeyCode.Keypad2, '2', false, false, false), false);
			return;
		}
	}

	// Token: 0x060002F9 RID: 761 RVA: 0x00011640 File Offset: 0x0000F840
	public void OnControlPanelButton(string ID)
	{
		if (ID == "CmdZoomIn")
		{
			this.ZoomIn();
		}
		if (ID == "CmdZoomOut")
		{
			this.ZoomOut();
		}
		if (ID == "CmdAlt")
		{
			this.bAlt = !this.bAlt;
			return;
		}
		if (this.CurrentGameView != Options.StageViewID)
		{
			if (ID == "CmdMoveU")
			{
				Keyboard.PushKey(new Keyboard.XRLKeyEvent(global::UnityEngine.KeyCode.Space, 'y', false, false, false), false);
				return;
			}
			if (ID == "CmdMoveD")
			{
				Keyboard.PushKey(new Keyboard.XRLKeyEvent(global::UnityEngine.KeyCode.N, 'n', false, false, false), false);
				return;
			}
			if (ID == "CmdWait")
			{
				Keyboard.PushKey(new Keyboard.XRLKeyEvent(global::UnityEngine.KeyCode.Space, ' ', false, false, false), false);
				return;
			}
			if (ID == "CmdMoveN")
			{
				Keyboard.PushKey(new Keyboard.XRLKeyEvent(global::UnityEngine.KeyCode.Keypad8, '\0', false, false, false), false);
				return;
			}
			if (ID == "CmdMoveS")
			{
				Keyboard.PushKey(new Keyboard.XRLKeyEvent(global::UnityEngine.KeyCode.Keypad2, '\0', false, false, false), false);
				return;
			}
			if (ID == "CmdMoveE")
			{
				Keyboard.PushKey(new Keyboard.XRLKeyEvent(global::UnityEngine.KeyCode.Keypad6, '\0', false, false, false), false);
				return;
			}
			if (ID == "CmdMoveW")
			{
				Keyboard.PushKey(new Keyboard.XRLKeyEvent(global::UnityEngine.KeyCode.Keypad4, '\0', false, false, false), false);
				return;
			}
			if (ID == "CmdMoveNW")
			{
				Keyboard.PushKey(new Keyboard.XRLKeyEvent(global::UnityEngine.KeyCode.Keypad7, '\0', false, false, false), false);
				return;
			}
			if (ID == "CmdMoveNE")
			{
				Keyboard.PushKey(new Keyboard.XRLKeyEvent(global::UnityEngine.KeyCode.Keypad9, '\0', false, false, false), false);
				return;
			}
			if (ID == "CmdMoveSW")
			{
				Keyboard.PushKey(new Keyboard.XRLKeyEvent(global::UnityEngine.KeyCode.Keypad1, '\0', false, false, false), false);
				return;
			}
			if (ID == "CmdMoveSE")
			{
				Keyboard.PushKey(new Keyboard.XRLKeyEvent(global::UnityEngine.KeyCode.Keypad3, '\0', false, false, false), false);
				return;
			}
		}
		else
		{
			if (ID.StartsWith("CmdMove") && this.bAlt)
			{
				Keyboard.PushMouseEvent(ID.Replace("CmdMove", "CmdAttack"), -1, -1, null);
				return;
			}
			if (ID == "CmdWaitUntilHealed" && this.bAlt)
			{
				Keyboard.PushMouseEvent(ID.Replace("CmdAutoExplore", "CmdAttack"), -1, -1, null);
				return;
			}
		}
		if (ID == "CmdEscape")
		{
			Keyboard.PushKey(new Keyboard.XRLKeyEvent(global::UnityEngine.KeyCode.Escape, '\0', false, false, false), false);
			return;
		}
		if (ID == "CmdReturn")
		{
			Keyboard.PushKey(new Keyboard.XRLKeyEvent(global::UnityEngine.KeyCode.Return, '\r', false, false, false), false);
			return;
		}
		Keyboard.PushMouseEvent(ID, -1, -1, null);
	}

	// Token: 0x1700004B RID: 75
	// (get) Token: 0x060002FA RID: 762 RVA: 0x000118B9 File Offset: 0x0000FAB9
	// (set) Token: 0x060002FB RID: 763 RVA: 0x000118C1 File Offset: 0x0000FAC1
	public string _CurrentGameView
	{
		get
		{
			return this.__CurrentGameView;
		}
		set
		{
			if (value != this.__CurrentGameView)
			{
				GameManager.UpdateCountFromChangedToActiveView = 0;
				this.__CurrentGameView = value;
			}
		}
	}

	// Token: 0x060002FC RID: 764 RVA: 0x000118E0 File Offset: 0x0000FAE0
	public void ClearRegions()
	{
		List<GameManager.ClickRegion> regions = this.Regions;
		lock (regions)
		{
			this.nRegions = 0;
		}
	}

	// Token: 0x060002FD RID: 765 RVA: 0x00011924 File Offset: 0x0000FB24
	public void AddRegion(int x1, int y1, int x2, int y2, string LeftCommand = null, string RightCommand = null, string OverCommand = null)
	{
		List<GameManager.ClickRegion> regions = this.Regions;
		lock (regions)
		{
			if (this.nRegions >= this.Regions.Count)
			{
				global::UnityEngine.Debug.Log("Add region");
				this.Regions.Add(new GameManager.ClickRegion());
			}
			this.Regions[this.nRegions].x1 = x1;
			this.Regions[this.nRegions].y1 = y1;
			this.Regions[this.nRegions].x2 = x2;
			this.Regions[this.nRegions].y2 = y2;
			this.Regions[this.nRegions].LeftClickCommand = LeftCommand;
			this.Regions[this.nRegions].RightClickCommand = RightCommand;
			this.Regions[this.nRegions].OverCommand = OverCommand;
			this.nRegions++;
		}
	}

	// Token: 0x060002FE RID: 766 RVA: 0x00011A40 File Offset: 0x0000FC40
	public void ShowTooltipForTile(int x, int y, string mode = "rightclick")
	{
		if (!this.ModernUI)
		{
			return;
		}
		if (UIManager.instance.currentWindow != null && UIManager.instance.currentWindow.name == "Stage")
		{
			this.tileTooltip.ForceHideTooltip();
			this.gameQueue.queueSingletonTask("TileHover", delegate
			{
				Look.QueueLookerTooltip(x, y, mode, 0);
			}, false);
		}
	}

	// Token: 0x060002FF RID: 767 RVA: 0x00011AC7 File Offset: 0x0000FCC7
	public void TileHover(int x, int y)
	{
	}

	// Token: 0x06000300 RID: 768 RVA: 0x00011ACC File Offset: 0x0000FCCC
	public bool InGameView()
	{
		RingDeque<string> gameViewStack = this.GameViewStack;
		bool flag2;
		lock (gameViewStack)
		{
			flag2 = this.GameViewStack.Count > 0;
		}
		return flag2;
	}

	// Token: 0x06000301 RID: 769 RVA: 0x00011B18 File Offset: 0x0000FD18
	public void ClearViewStack()
	{
		this.GameViewStack.Clear();
	}

	// Token: 0x06000302 RID: 770 RVA: 0x00011B25 File Offset: 0x0000FD25
	public static void EnsureGameView(string view)
	{
		GameManager.Instance.bViewUpdated = true;
	}

	// Token: 0x06000303 RID: 771 RVA: 0x00011B34 File Offset: 0x0000FD34
	public string[] GetGameViewStackCopy()
	{
		RingDeque<string> gameViewStack = this.GameViewStack;
		string[] array2;
		lock (gameViewStack)
		{
			RingDeque<string> gameViewStack2 = this.GameViewStack;
			bool flag2 = !this.CurrentGameView.IsNullOrEmpty();
			int num = gameViewStack2.Count;
			if (flag2)
			{
				num++;
			}
			string[] array = new string[num];
			if (flag2)
			{
				array[--num] = this.CurrentGameView;
			}
			for (int i = 0; i < num; i++)
			{
				array[i] = gameViewStack2[i];
			}
			array2 = array;
		}
		return array2;
	}

	// Token: 0x06000304 RID: 772 RVA: 0x00011BCC File Offset: 0x0000FDCC
	public void SetGameViewStack(string[] Views)
	{
		ControlManager.ResetInput(false, true);
		RingDeque<string> gameViewStack = this.GameViewStack;
		lock (gameViewStack)
		{
			this.ClearViewStack();
			for (int i = Views.Length - 2; i >= 0; i--)
			{
				this.GameViewStack.Push(Views[i]);
			}
			this.CurrentGameView = Views[Views.Length - 1];
			this.bViewUpdated = true;
			TextConsole.BufferUpdated = true;
		}
	}

	// Token: 0x06000305 RID: 773 RVA: 0x00011C4C File Offset: 0x0000FE4C
	public void SetGameViewStack(string NewView)
	{
		ControlManager.ResetInput(false, true);
		RingDeque<string> gameViewStack = this.GameViewStack;
		lock (gameViewStack)
		{
			this.ClearViewStack();
			this.CurrentGameView = NewView;
			this.bViewUpdated = true;
			TextConsole.BufferUpdated = true;
		}
	}

	// Token: 0x06000306 RID: 774 RVA: 0x00011CA8 File Offset: 0x0000FEA8
	public void PushGameView(string NewView, bool bHard = true)
	{
		ControlManager.ResetInput(false, true);
		RingDeque<string> gameViewStack = this.GameViewStack;
		lock (gameViewStack)
		{
			if (this.LeftGameViews.Contains(NewView))
			{
				this.LeftGameViews.Remove(NewView);
			}
			this.GameViewStack.Enqueue(this.CurrentGameView);
			this.CurrentGameView = NewView;
			this.bViewUpdated = true;
			if (bHard)
			{
				TextConsole.BufferUpdated = true;
			}
		}
	}

	// Token: 0x06000307 RID: 775 RVA: 0x00011D2C File Offset: 0x0000FF2C
	public void ForceGameView()
	{
		RingDeque<string> gameViewStack = this.GameViewStack;
		lock (gameViewStack)
		{
			this.GameViewStack.Clear();
			this.CurrentGameView = Options.StageViewID;
			TextConsole.BufferUpdated = true;
			this.bViewUpdated = true;
		}
	}

	// Token: 0x06000308 RID: 776 RVA: 0x00011D8C File Offset: 0x0000FF8C
	public void PopGameView(bool bHard = false)
	{
		ControlManager.ResetInput(false, true);
		RingDeque<string> gameViewStack = this.GameViewStack;
		lock (gameViewStack)
		{
			if (bHard)
			{
				TextConsole.BufferUpdated = true;
			}
			if (this.GameViewStack.Count > 0)
			{
				if (this.CurrentGameView != null && !this.LeftGameViews.Contains(this.CurrentGameView))
				{
					this.LeftGameViews.Add(this.CurrentGameView);
				}
				this.CurrentGameView = this.GameViewStack.Eject();
			}
			this.bViewUpdated = true;
		}
	}

	// Token: 0x06000309 RID: 777 RVA: 0x00011E28 File Offset: 0x00010028
	public void RemoveGameView(string Name, bool Hard = false)
	{
		RingDeque<string> gameViewStack = this.GameViewStack;
		lock (gameViewStack)
		{
			if (this.GameViewStack.Count > 0)
			{
				if (Name == this.CurrentGameView)
				{
					ControlManager.ResetInput(false, true);
					if (this.CurrentGameView != null && !this.LeftGameViews.Contains(this.CurrentGameView))
					{
						this.LeftGameViews.Add(this.CurrentGameView);
					}
					this.CurrentGameView = this.GameViewStack.Eject();
					this.bViewUpdated = true;
					if (Hard)
					{
						TextConsole.BufferUpdated = true;
					}
				}
				else
				{
					this.GameViewStack.Remove(Name);
				}
			}
		}
	}

	// Token: 0x1700004C RID: 76
	// (get) Token: 0x0600030A RID: 778 RVA: 0x00011EE4 File Offset: 0x000100E4
	// (set) Token: 0x0600030B RID: 779 RVA: 0x00011EEC File Offset: 0x000100EC
	public string CurrentGameView
	{
		get
		{
			return this._CurrentGameView;
		}
		set
		{
			if (this._CurrentGameView == value)
			{
				return;
			}
			this._CurrentGameView = value;
			this.ClearRegions();
		}
	}

	// Token: 0x0600030C RID: 780 RVA: 0x00011F0C File Offset: 0x0001010C
	private void UpdateView()
	{
		this.ShowNongameViews();
		this.LeftGameViews.Clear();
		if (this._ActiveGameView != this._CurrentGameView)
		{
			ControlManager.ResetInput(false, true);
			Keyboard.ClearInput(false, false);
		}
		this._ActiveGameView = this._CurrentGameView;
		if (this._ActiveGameView == Options.StageViewID)
		{
			this.currentUIView = null;
			this.HideNongameViews(false);
			this.currentUIView = this.GetViewData(this._ActiveGameView);
			if (Options.ModernUI && this.currentUIView.UICanvasHost != 0 && this.currentUIView.UICanvasHost == 1 && this.ModernUI && this.currentUIView.UICanvas != "_manuallyshown")
			{
				UIManager.showWindow(this.currentUIView.UICanvas, true);
				return;
			}
		}
		else
		{
			this.currentUIView = this.GetViewData(this._ActiveGameView);
			if (this._ActiveGameView == null || this.currentUIView == null)
			{
				UIManager.showWindow("PassthroughDefault", true);
				return;
			}
			if (this.currentUIView.UICanvasHost != 0)
			{
				if (this.currentUIView.UICanvasHost == 1)
				{
					if (this.currentUIView.UICanvas != "_manuallyshown" && this.ModernUI)
					{
						UIManager.showWindow(this.currentUIView.UICanvas, false);
						return;
					}
					UIManager.instance.currentWindow = null;
					return;
				}
				else
				{
					MetricsManager.LogError("Bad view canvas host: " + this.currentUIView.UICanvasHost.ToString());
				}
			}
		}
	}

	// Token: 0x0600030D RID: 781 RVA: 0x00012094 File Offset: 0x00010294
	private void HideNongameViews(bool forShowingLater = true)
	{
		if (!forShowingLater)
		{
			RingDeque<string> gameViewStack = this.GameViewStack;
			lock (gameViewStack)
			{
				this.GameViewStack.Clear();
			}
		}
	}

	// Token: 0x0600030E RID: 782 RVA: 0x000120DC File Offset: 0x000102DC
	private void ShowNongameViews()
	{
		this.HiddenNongameViews.Clear();
	}

	// Token: 0x0600030F RID: 783 RVA: 0x000120E9 File Offset: 0x000102E9
	public ex3DSprite2 getTile(int x, int y)
	{
		return this.ConsoleCharacter[x, y];
	}

	// Token: 0x06000310 RID: 784 RVA: 0x000120F8 File Offset: 0x000102F8
	public ex3DSprite2 GetOverlayTile(int X, int Y)
	{
		if (X < 0 || X > 79 || Y < 0 || Y > 24)
		{
			return null;
		}
		return this.OverlayCharacter[X + 1, Y + 1];
	}

	// Token: 0x06000311 RID: 785 RVA: 0x0001211F File Offset: 0x0001031F
	public Vector3 getTileCenter(int x, int y, int z = 0)
	{
		return new Vector3((float)(40 * -(float)this.tileWidth + x * this.tileWidth + 8), 12.5f * (float)this.tileHeight - (float)(y * this.tileHeight) - 12f, (float)(100 - z));
	}

	// Token: 0x06000312 RID: 786 RVA: 0x0001215E File Offset: 0x0001035E
	public Vector3 getScreenTileCenter(int x, int y, int z = 0)
	{
		return GameManager.MainCamera.GetComponent<Camera>().WorldToScreenPoint(this.getTileCenter(x, y, z));
	}

	// Token: 0x06000313 RID: 787 RVA: 0x00012178 File Offset: 0x00010378
	public Vector3 getTileCenter(float x, float y, float z = 0f)
	{
		return new Vector3((float)(40 * -(float)this.tileWidth) + x * (float)this.tileWidth + 8f, 12.5f * (float)this.tileHeight - y * (float)this.tileHeight - 12f, 100f - z);
	}

	// Token: 0x06000314 RID: 788 RVA: 0x000121CC File Offset: 0x000103CC
	private void StartGameThread()
	{
		global::UnityEngine.Debug.Log("Starting game thread...");
		base.useGUILayout = false;
		GameManager.MainCamera = this.GameObjectFind("Main Camera");
		GameManager.cameraMainCamera = GameManager.MainCamera.GetComponent<Camera>();
		GameManager.MainCameraLetterbox = GameManager.MainCamera.GetComponent<LetterboxCamera>();
		WaveCollapseTools.LoadTemplates(false);
		SoundManager.Init();
		Thread thread = XRLCore.Start();
		this.gameQueue.threadContext = thread;
		this.TV = GameManager.MainCamera.GetComponent<CC_AnalogTV>();
		this.TargetIntensity = this.TV.noiseIntensity;
		this.TV.noiseIntensity = 1f;
		this.TileRoot = new global::UnityEngine.GameObject();
		this.TileRoot.name = "_tileroot";
		this.TileRoot.AddComponent<CameraShake>();
		this.playerTracker = global::UnityEngine.Object.Instantiate<global::UnityEngine.GameObject>(this.playerTrackerPrefab).GetComponent<PlayerTracker>();
		this.playerTracker.transform.SetParent(this.TileRoot.transform, false);
		this.OverlayRoot = new global::UnityEngine.GameObject();
		this.OverlayRoot.name = "_overlayroot";
		for (int i = 0; i < 80; i++)
		{
			for (int j = 0; j < 25; j++)
			{
				this.CurrentTile[i, j] = null;
			}
		}
		for (int k = 0; k < 80; k++)
		{
			for (int l = 0; l < 25; l++)
			{
				this.CurrentShadermode[k, l] = 0;
				this.ConsoleCharacter[k, l] = SpriteManager.CreateSprite("assets_content_textures_text_1.bmp", false).GetComponent<ex3DSprite2>();
				this.ConsoleCharacter[k, l].gameObject.transform.SetParent(this.TileRoot.transform);
				this.ConsoleCharacter[k, l].gameObject.transform.position = this.getTileCenter(k, l, 0);
				this.ConsoleCharacter[k, l].gameObject.AddComponent<TileBehavior>().SetPosition(k, l);
				Transform[,] consoleCharacterTransform = this.ConsoleCharacterTransform;
				int num = k;
				int num2 = l;
				ex3DSprite2 ex3DSprite = this.ConsoleCharacter[k, l];
				consoleCharacterTransform[num, num2] = ((ex3DSprite != null) ? ex3DSprite.transform : null);
			}
		}
		this.Backdrop = SpriteManager.CreateSprite("assets_content_textures_text_0.bmp", false);
		this.Backdrop.name = "Backdrop";
		this.Backdrop.transform.SetParent(this.TileRoot.transform);
		this.Backdrop.transform.position = new Vector3(0f, 0f, 1000f);
		this.Backdrop.GetComponent<ex3DSprite2>().width = 1280f;
		this.Backdrop.GetComponent<ex3DSprite2>().height = 600f;
		this.Backdrop.GetComponent<ex3DSprite2>().color = The.Color.DarkBlack;
		this.Backdrop.GetComponent<ex3DSprite2>().detailcolor = The.Color.DarkBlack;
		this.Backdrop.GetComponent<ex3DSprite2>().backcolor = The.Color.DarkBlack;
		for (int m = 0; m < 82; m++)
		{
			for (int n = 0; n < 27; n++)
			{
				if (m == 0 || m == 81 || n == 0 || n == 26)
				{
					if (m == 0 && n == 0)
					{
						this.OverlayCharacter[m, n] = SpriteManager.CreateSprite(Options.UseTiles ? "Assets_Content_Textures_UI_ui_upleft.png" : "assets_content_textures_text_27.bmp", false).GetComponent<ex3DSprite2>();
					}
					else if (m == 81 && n == 0)
					{
						this.OverlayCharacter[m, n] = SpriteManager.CreateSprite(Options.UseTiles ? "Assets_Content_Textures_UI_ui_upright.png" : "assets_content_textures_text_26.bmp", false).GetComponent<ex3DSprite2>();
					}
					else if (m == 0 && n == 26)
					{
						this.OverlayCharacter[m, n] = SpriteManager.CreateSprite(Options.UseTiles ? "Assets_Content_Textures_UI_ui_downleft.png" : "assets_content_textures_text_24.bmp", false).GetComponent<ex3DSprite2>();
					}
					else if (m == 81 && n == 26)
					{
						this.OverlayCharacter[m, n] = SpriteManager.CreateSprite(Options.UseTiles ? "Assets_Content_Textures_UI_ui_downright.png" : "assets_content_textures_text_25.bmp", false).GetComponent<ex3DSprite2>();
					}
					else if (m == 0)
					{
						this.OverlayCharacter[m, n] = SpriteManager.CreateSprite(Options.UseTiles ? "Assets_Content_Textures_UI_ui_left.png" : "assets_content_textures_text_27.bmp", false).GetComponent<ex3DSprite2>();
					}
					else if (m == 81)
					{
						this.OverlayCharacter[m, n] = SpriteManager.CreateSprite(Options.UseTiles ? "Assets_Content_Textures_UI_ui_right.png" : "assets_content_textures_text_26.bmp", false).GetComponent<ex3DSprite2>();
					}
					else if (n == 0)
					{
						this.OverlayCharacter[m, n] = SpriteManager.CreateSprite(Options.UseTiles ? "Assets_Content_Textures_UI_ui_up.png" : "assets_content_textures_text_24.bmp", false).GetComponent<ex3DSprite2>();
					}
					else if (n == 26)
					{
						this.OverlayCharacter[m, n] = SpriteManager.CreateSprite(Options.UseTiles ? "Assets_Content_Textures_UI_ui_down.png" : "assets_content_textures_text_25.bmp", false).GetComponent<ex3DSprite2>();
					}
					if (m == 0)
					{
						this.OverlayCharacter[m, n].gameObject.AddComponent<BorderTileBehavior>().SetDirection("CmdMoveW", m - 1, n - 1);
					}
					else if (m == 81)
					{
						this.OverlayCharacter[m, n].gameObject.AddComponent<BorderTileBehavior>().SetDirection("CmdMoveE", m - 1, n - 1);
					}
					else if (n == 0)
					{
						this.OverlayCharacter[m, n].gameObject.AddComponent<BorderTileBehavior>().SetDirection("CmdMoveN", m - 1, n - 1);
					}
					else if (n == 26)
					{
						this.OverlayCharacter[m, n].gameObject.AddComponent<BorderTileBehavior>().SetDirection("CmdMoveS", m - 1, n - 1);
					}
					if (m == 0 && n == 0)
					{
						this.OverlayCharacter[m, n].GetComponent<BorderTileBehavior>().SetDirection("CmdMoveNW", m - 1, n - 1);
					}
					if (m == 81 && n == 0)
					{
						this.OverlayCharacter[m, n].GetComponent<BorderTileBehavior>().SetDirection("CmdMoveNE", m - 1, n - 1);
					}
					if (m == 0 && n == 26)
					{
						this.OverlayCharacter[m, n].GetComponent<BorderTileBehavior>().SetDirection("CmdMoveSW", m - 1, n - 1);
					}
					if (m == 81 && n == 26)
					{
						this.OverlayCharacter[m, n].GetComponent<BorderTileBehavior>().SetDirection("CmdMoveSE", m - 1, n - 1);
					}
					this.OverlayCharacter[m, n].gameObject.transform.parent = this.OverlayRoot.transform;
					this.OverlayCharacter[m, n].gameObject.transform.position = this.getTileCenter(m - 1, n - 1, 0) + new Vector3(0f, 0f, -10f);
					this.OverlayCharacter[m, n].shader = SpriteManager.GetShaderMode(0);
				}
			}
		}
		for (int num3 = 0; num3 < 256; num3++)
		{
			this.CharInfos[num3] = SpriteManager.GetTextureInfo("assets_content_textures_text_" + num3.ToString() + ".bmp", true);
		}
		global::UnityEngine.Debug.Log("Initilization complete...");
		this.bInitComplete = true;
	}

	// Token: 0x06000315 RID: 789 RVA: 0x0001292C File Offset: 0x00010B2C
	public void OnGUI()
	{
		if (!XRLCore.bThreadFocus)
		{
			return;
		}
		GameManager.capslock = global::UnityEngine.Event.current.capsLock;
		Keyboard._bAlt = Input.GetKey(global::UnityEngine.KeyCode.RightAlt) || Input.GetKey(global::UnityEngine.KeyCode.LeftAlt) || GameManager.Instance.player.GetButton("Highlight", false);
		Keyboard._bCtrl = Input.GetKey(global::UnityEngine.KeyCode.RightControl) || Input.GetKey(global::UnityEngine.KeyCode.LeftControl) || Input.GetKey(global::UnityEngine.KeyCode.RightMeta) || Input.GetKey(global::UnityEngine.KeyCode.LeftMeta);
		Keyboard._bShift = Input.GetKey(global::UnityEngine.KeyCode.RightShift) || Input.GetKey(global::UnityEngine.KeyCode.LeftShift);
		if (!this.ModernUI && (this.CurrentGameView == "Popup:Text" || this.CurrentGameView == "Popup:AskString"))
		{
			GameManager.pushKeyEvents(true);
			return;
		}
		if (this.ModernUI && !Options.ModernCharacterSheet)
		{
			bool flag = false;
			if (GameManager.Instance._ActiveGameView == "Equipment")
			{
				flag = true;
			}
			if (GameManager.Instance._ActiveGameView == "Inventory")
			{
				flag = true;
			}
			if (GameManager.Instance._ActiveGameView == "Status")
			{
				flag = true;
			}
			if (GameManager.Instance._ActiveGameView == "Factions")
			{
				flag = true;
			}
			if (GameManager.Instance._ActiveGameView == "Quests")
			{
				flag = true;
			}
			if (GameManager.Instance._ActiveGameView == "Journal")
			{
				flag = true;
			}
			if (GameManager.Instance._ActiveGameView == "Tinkering")
			{
				flag = true;
			}
			if (GameManager.Instance._ActiveGameView == "SkillsAndPowers")
			{
				flag = true;
			}
			if (flag)
			{
				GameManager.pushKeyEvents(false);
				return;
			}
		}
		if (UIManager.instance == null || !UIManager.instance.AllowPassthroughInput())
		{
			return;
		}
		if (this.ModernUI && this.currentUIView != null)
		{
			if (this.currentUIView.UICanvas != null)
			{
				GameManager.ViewInfo viewInfo = this.currentUIView;
				bool? flag2;
				if (viewInfo == null)
				{
					flag2 = null;
				}
				else
				{
					string navCategory = viewInfo.NavCategory;
					flag2 = ((navCategory != null) ? new bool?(navCategory.Contains("*AllowLegacyInputPassthrough")) : null);
				}
				bool? flag3 = flag2;
				if (!flag3.GetValueOrDefault())
				{
					return;
				}
			}
			string currentGameView = this.CurrentGameView;
			if (currentGameView != null && currentGameView.StartsWith("ModernPopup"))
			{
				return;
			}
		}
		GameManager.pushKeyEvents(false);
	}

	// Token: 0x06000316 RID: 790 RVA: 0x00012B88 File Offset: 0x00010D88
	public static void pushKeyEvents(bool always = false)
	{
		GameManager.capslock = global::UnityEngine.Event.current.capsLock;
		Keyboard._bAlt = Input.GetKey(global::UnityEngine.KeyCode.RightAlt) || Input.GetKey(global::UnityEngine.KeyCode.LeftAlt) || GameManager.Instance.player.GetButton("Highlight", false);
		Keyboard._bCtrl = Input.GetKey(global::UnityEngine.KeyCode.RightControl) || Input.GetKey(global::UnityEngine.KeyCode.LeftControl) || Input.GetKey(global::UnityEngine.KeyCode.RightMeta) || Input.GetKey(global::UnityEngine.KeyCode.LeftMeta);
		Keyboard._bShift = Input.GetKey(global::UnityEngine.KeyCode.RightShift) || Input.GetKey(global::UnityEngine.KeyCode.LeftShift);
		if (global::UnityEngine.Event.current.isKey && global::UnityEngine.Event.current.type == EventType.KeyDown && global::UnityEngine.Event.current.keyCode != global::UnityEngine.KeyCode.RightMeta && global::UnityEngine.Event.current.keyCode != global::UnityEngine.KeyCode.LeftMeta && global::UnityEngine.Event.current.keyCode != global::UnityEngine.KeyCode.LeftControl && global::UnityEngine.Event.current.keyCode != global::UnityEngine.KeyCode.RightControl && global::UnityEngine.Event.current.keyCode != global::UnityEngine.KeyCode.LeftAlt && global::UnityEngine.Event.current.keyCode != global::UnityEngine.KeyCode.RightAlt && global::UnityEngine.Event.current.keyCode != global::UnityEngine.KeyCode.LeftShift && global::UnityEngine.Event.current.keyCode != global::UnityEngine.KeyCode.RightShift && global::UnityEngine.Event.current.keyCode != global::UnityEngine.KeyCode.None)
		{
			if (!Options.ModernUI && global::UnityEngine.Event.current.keyCode >= global::UnityEngine.KeyCode.A && global::UnityEngine.Event.current.keyCode <= global::UnityEngine.KeyCode.Z && Options.GetOption("OptionPushAZHotkeys", "") == "Yes")
			{
				always = true;
			}
			if (!ControlManager.isKeyMapped(global::UnityEngine.Event.current.keyCode, Keyboard._bAlt, Keyboard._bCtrl, Keyboard._bShift) || always)
			{
				if (global::UnityEngine.Event.current.keyCode == global::UnityEngine.KeyCode.KeypadEnter)
				{
					global::UnityEngine.Event.current.keyCode = global::UnityEngine.KeyCode.Return;
				}
				if (global::UnityEngine.Event.current.keyCode == global::UnityEngine.KeyCode.Return)
				{
					ControlManager.WaitForKeyup("Accept");
				}
				Keyboard.PushKey(new Keyboard.XRLKeyEvent(global::UnityEngine.Event.current), true);
			}
		}
	}

	// Token: 0x06000317 RID: 791 RVA: 0x00012DA6 File Offset: 0x00010FA6
	private void EditorInit()
	{
	}

	// Token: 0x06000318 RID: 792 RVA: 0x00012DA8 File Offset: 0x00010FA8
	private void OnApplicationFocus(bool focus)
	{
		XRLCore.bThreadFocus = focus;
		GameManager.focused = focus;
	}

	// Token: 0x06000319 RID: 793 RVA: 0x00012DB6 File Offset: 0x00010FB6
	private void OnApplicationPause(bool pauseStatus)
	{
		GameManager.focused = !pauseStatus;
	}

	// Token: 0x0600031A RID: 794 RVA: 0x00012DC1 File Offset: 0x00010FC1
	public bool ColorMatch(Color C1, Color C2)
	{
		return C1.r == C2.r && C1.g == C2.g && C1.b == C2.b;
	}

	// Token: 0x1700004D RID: 77
	// (get) Token: 0x0600031B RID: 795 RVA: 0x00012DEF File Offset: 0x00010FEF
	// (set) Token: 0x0600031C RID: 796 RVA: 0x00012DF7 File Offset: 0x00010FF7
	public int TileScale
	{
		get
		{
			return this._TileScale;
		}
		set
		{
			if (this._TileScale != value)
			{
				this._TileScale = value;
				if (GameManager.MainCameraLetterbox)
				{
					GameManager.MainCameraLetterbox.Refresh();
				}
			}
		}
	}

	// Token: 0x1700004E RID: 78
	// (get) Token: 0x0600031D RID: 797 RVA: 0x00012E1F File Offset: 0x0001101F
	// (set) Token: 0x0600031E RID: 798 RVA: 0x00012E27 File Offset: 0x00011027
	public int DockMovable
	{
		get
		{
			return this._DockMovable;
		}
		set
		{
			if (this._DockMovable != value)
			{
				this._DockMovable = value;
				if (GameManager.MainCameraLetterbox)
				{
					GameManager.MainCameraLetterbox.Refresh();
				}
			}
		}
	}

	// Token: 0x1700004F RID: 79
	// (set) Token: 0x0600031F RID: 799 RVA: 0x00012E4F File Offset: 0x0001104F
	public float Spin
	{
		set
		{
			if (this._Spin <= 0f)
			{
				this._Spin = value;
				this._CurrentSpin = 0f;
			}
		}
	}

	// Token: 0x17000050 RID: 80
	// (set) Token: 0x06000320 RID: 800 RVA: 0x00012E70 File Offset: 0x00011070
	public float ThinWorldDistorting
	{
		set
		{
			this._ThinWorldDistoring = value;
		}
	}

	// Token: 0x17000051 RID: 81
	// (set) Token: 0x06000321 RID: 801 RVA: 0x00012E79 File Offset: 0x00011079
	public int StaticEffecting
	{
		set
		{
			if (value > 0 && this._StaticEffectTime > 0f)
			{
				this._StaticEffectTime = 0f;
				this._NextStaticEffectGlitch = 0f;
			}
			if (value > this._StaticEffecting)
			{
				this._StaticEffecting = value;
			}
		}
	}

	// Token: 0x17000052 RID: 82
	// (get) Token: 0x06000322 RID: 802 RVA: 0x00012EB2 File Offset: 0x000110B2
	// (set) Token: 0x06000323 RID: 803 RVA: 0x00012EBA File Offset: 0x000110BA
	public bool Spacefolding
	{
		get
		{
			return this._spacefolding;
		}
		set
		{
			this._spacefolding = value;
			if (value)
			{
				this._spacefoldingT = 0f;
			}
		}
	}

	// Token: 0x17000053 RID: 83
	// (get) Token: 0x06000324 RID: 804 RVA: 0x00012ED1 File Offset: 0x000110D1
	// (set) Token: 0x06000325 RID: 805 RVA: 0x00012ED9 File Offset: 0x000110D9
	public bool Fuzzing
	{
		get
		{
			return this._fuzzing;
		}
		set
		{
			this._fuzzing = value;
			if (value)
			{
				this._fuzzingT = 0f;
			}
		}
	}

	// Token: 0x06000326 RID: 806 RVA: 0x00012EF0 File Offset: 0x000110F0
	public override void RegisterViews()
	{
		foreach (Type type in ModManager.GetTypesWithAttribute(typeof(UIView), true))
		{
			string text = null;
			foreach (UIView uiview in type.GetCustomAttributes(typeof(UIView), false))
			{
				string text2;
				if ((text2 = text) == null)
				{
					text2 = uiview.UICanvas ?? uiview.ID;
				}
				text = text2;
				GameManager.ViewInfo viewInfo;
				if (this._ViewData.TryGetValue(uiview.ID, out viewInfo))
				{
					if (!viewInfo.IsSame(uiview.AsGameManagerViewInfo()))
					{
						global::UnityEngine.Debug.LogError(string.Concat(new string[]
						{
							"Found a second UIView with ID ",
							uiview.ID,
							" in ",
							type.ToString(),
							" with different parameters from the last one."
						}));
					}
				}
				else
				{
					this._ViewData[uiview.ID] = uiview.AsGameManagerViewInfo();
				}
			}
		}
	}

	// Token: 0x06000327 RID: 807 RVA: 0x00013018 File Offset: 0x00011218
	public void SetActiveView(string id)
	{
	}

	// Token: 0x06000328 RID: 808 RVA: 0x0001301A File Offset: 0x0001121A
	public override void OnStart()
	{
		this.RegisterViews();
		UIManager.showWindow("PassthroughDefault", false);
		base.gameObject.AddComponent<CombatJuiceManager>().gameManager = this;
	}

	// Token: 0x06000329 RID: 809 RVA: 0x0001303E File Offset: 0x0001123E
	public Vector3 GetCellCenter(int x, int y, int z = -10)
	{
		return new Vector3((float)(40 * -(float)this.tileWidth + x * this.tileWidth + 8), 12.5f * (float)this.tileHeight - (float)(y * this.tileHeight) - 12f, (float)z);
	}

	// Token: 0x0600032A RID: 810 RVA: 0x0001307C File Offset: 0x0001127C
	public Vector3 CenterOnCell(int x, int y)
	{
		if (!this.NeedsToPan)
		{
			return this.GetCellCenter(x, y, -10);
		}
		GameManager.MainCameraLetterbox.SetPositionImmediately(new Vector3((float)(40 * -(float)this.tileWidth + x * this.tileWidth + 8), 12.5f * (float)this.tileHeight - (float)(y * this.tileHeight) - 12f, -10f));
		this.TargetCameraLocation = GameManager.MainCameraLetterbox.DesiredPan;
		return new Vector3(this.TargetCameraLocation.x, this.TargetCameraLocation.y, 0f);
	}

	// Token: 0x0600032B RID: 811 RVA: 0x00013114 File Offset: 0x00011314
	public void RefreshLayout(bool updateForceFullscreenIfSwapped = false)
	{
		if (GameManager.MainCameraLetterbox == null || this.CurrentGameView == "MapEditor")
		{
			return;
		}
		this.ShouldForceFullscreen = this.TargetZoomFactor == -2f || this.AnyViewInStackForcefullscreenInModernOrLegacy();
		bool flag = GameManager.MainCameraLetterbox.Pannable;
		if (this.CurrentGameView == null || !this.GetViewData(this.CurrentGameView).KeepCurrentFullscreen)
		{
			if (!this.ShouldForceFullscreen)
			{
				if (GameManager.MainCameraLetterbox.DesiredZoomFactor != this.TargetZoomFactor || this.DockMovable == 3)
				{
					this.UpdatePreferredSidebarPosition();
				}
				if (!this.NeedsToPan)
				{
					flag = false;
					this.stageDock.maxWidth = this.stageRect.rect.width * 0.4f;
					Vector3 letterboxTotalArea = GameManager.MainCameraLetterbox.GetLetterboxTotalArea();
					float num = this.stageDock.safeArea.rect.width - letterboxTotalArea.x;
					this.stageDock.maxWidth = Mathf.Max(this.stageDock.maxWidth, this.stageRect.rect.width - num);
				}
				else
				{
					flag = true;
				}
			}
			else
			{
				this.ShouldForceFullscreen = true;
			}
		}
		if (!Options.ModernUI)
		{
			string currentGameView = this.CurrentGameView;
			if (currentGameView != null && currentGameView.StartsWith("PopupMessage"))
			{
				this.ShouldForceFullscreen = true;
				flag = false;
			}
		}
		if (!Options.ModernUI)
		{
			this.ShouldForceFullscreen = true;
			flag = false;
		}
		if (this.ShouldForceFullscreen != GameManager.MainCameraLetterbox.ForcingFullscreen)
		{
			GameManager.MainCameraLetterbox.UpdateDelay = 10;
		}
		if (updateForceFullscreenIfSwapped || this.ShouldForceFullscreen == GameManager.MainCameraLetterbox.ForcingFullscreen)
		{
			if (this.ShouldForceFullscreen)
			{
				GameManager.MainCameraLetterbox.SetTargetArea(this.stageRect);
				GameManager.MainCameraLetterbox.ForcingFullscreen = true;
				GameManager.MainCameraLetterbox.DesiredZoomFactor = 1f;
				return;
			}
			GameManager.MainCameraLetterbox.Pannable = flag;
			if (flag)
			{
				GameManager.MainCameraLetterbox.DesiredPan = this.TargetCameraLocation;
			}
			GameManager.MainCameraLetterbox.SetTargetArea(this.stageDock.safeArea);
			GameManager.MainCameraLetterbox.ForcingFullscreen = false;
			GameManager.MainCameraLetterbox.DesiredZoomFactor = Math.Max(1f, this.TargetZoomFactor);
		}
	}

	// Token: 0x17000054 RID: 84
	// (get) Token: 0x0600032C RID: 812 RVA: 0x00013345 File Offset: 0x00011545
	// (set) Token: 0x0600032D RID: 813 RVA: 0x0001334D File Offset: 0x0001154D
	public int selectedAbility
	{
		get
		{
			return this._selectedAbility;
		}
		set
		{
			this._selectedAbility = value;
		}
	}

	// Token: 0x0600032E RID: 814 RVA: 0x00013358 File Offset: 0x00011558
	public void UpdateSelectedAbility()
	{
		if (this._selectedAbility == -1)
		{
			return;
		}
		this.selectedAbilitybuilder.Length = 0;
		this.currentlySelectedAbility = AbilityAPI.GetAbility(this.selectedAbility);
		if (this.currentlySelectedAbility == null)
		{
			this.selectedAbilitybuilder.Append("<color=#666666>");
			this.selectedAbilitybuilder.Append(ControlManager.getCommandInputDescription("Previous Ability", true, false, ControlManager.InputDeviceType.Unknown, false));
			this.selectedAbilitybuilder.Append(ControlManager.getCommandInputDescription("Next Ability", true, false, ControlManager.InputDeviceType.Unknown, false));
			this.selectedAbilitybuilder.Append("</color>");
			this.selectedAbilitybuilder.Append(" <none>");
			this.selectedAbilityText.text = this.selectedAbilitybuilder.ToString();
			return;
		}
		this.selectedAbilitybuilder.Append("<color=#666666>");
		this.selectedAbilitybuilder.Append(ControlManager.getCommandInputDescription("Previous Ability", true, false, ControlManager.InputDeviceType.Unknown, false));
		this.selectedAbilitybuilder.Append(ControlManager.getCommandInputDescription("Next Ability", true, false, ControlManager.InputDeviceType.Unknown, false));
		this.selectedAbilitybuilder.Append("</color>");
		this.selectedAbilitybuilder.Append(" ");
		if (this.currentlySelectedAbility.Cooldown > 0 || !this.currentlySelectedAbility.Enabled)
		{
			this.selectedAbilitybuilder.Append("<color=#999999>");
		}
		else
		{
			this.selectedAbilitybuilder.Append("<color=#FFFFFF>");
		}
		this.selectedAbilitybuilder.Append("<color=#FFFF00>");
		this.selectedAbilitybuilder.Append(ControlManager.getCommandInputDescription("Use Ability", true, false, ControlManager.InputDeviceType.Unknown, false));
		this.selectedAbilitybuilder.Append("</color>");
		this.selectedAbilitybuilder.Append(" ");
		this.selectedAbilitybuilder.Append(this.currentlySelectedAbility.DisplayName);
		if (this.currentlySelectedAbility.Cooldown > 0)
		{
			this.selectedAbilitybuilder.Append(" [");
			this.selectedAbilitybuilder.Append(this.currentlySelectedAbility.Cooldown / 10 + 1);
			this.selectedAbilitybuilder.Append(" turns]");
		}
		this.selectedAbilitybuilder.Append("</color>");
		this.selectedAbilityText.text = this.selectedAbilitybuilder.ToString();
	}

	// Token: 0x0600032F RID: 815 RVA: 0x00013590 File Offset: 0x00011790
	public string LongDirectionToShortDirection(string dir)
	{
		if (dir == "North" || dir == "N")
		{
			return "N";
		}
		if (dir == "South" || dir == "S")
		{
			return "S";
		}
		if (dir == "East" || dir == "E")
		{
			return "E";
		}
		if (dir == "West" || dir == "W")
		{
			return "W";
		}
		if (dir == "Northeast" || dir == "NE")
		{
			return "NE";
		}
		if (dir == "Northwest" || dir == "NW")
		{
			return "NW";
		}
		if (dir == "Southeast" || dir == "SE")
		{
			return "SE";
		}
		if (dir == "Southwest" || dir == "SW")
		{
			return "SW";
		}
		return ".";
	}

	// Token: 0x06000330 RID: 816 RVA: 0x000136A4 File Offset: 0x000118A4
	public void SetActiveLayersForNavCategory(string navCategory, bool setCurrentCategory = true)
	{
		if (setCurrentCategory)
		{
			this.currentNavCategory = navCategory;
		}
		NavCategory navCategory2;
		List<string> list;
		if (CommandBindingManager.NavCategories.TryGetValue(navCategory, out navCategory2))
		{
			list = navCategory2.Layers;
		}
		else
		{
			MetricsManager.LogError("NavCategory '" + navCategory + "' not found, defaulting to Menu");
			list = CommandBindingManager.NavCategories["Menu"].Layers;
		}
		ControlManager.EnableOnlyLayers(list);
	}

	// Token: 0x06000331 RID: 817 RVA: 0x00013704 File Offset: 0x00011904
	public void UpdateInput()
	{
		if (!XRLCore.bThreadFocus)
		{
			return;
		}
		if (this._ActiveGameView == "MapEditor")
		{
			return;
		}
		if (this.currentNavDirectionDisplay != null && this.currentNavDirectionDisplay.text != "")
		{
			this.currentNavDirectionDisplay.text = "";
		}
		string text = this.GetViewData(this.CurrentGameView).NavCategory;
		if (this.CurrentGameView == "EmbarkBuilder")
		{
			text = "Chargen";
		}
		this.DebugNavCategory = text;
		if (text == null || text == "StringInput")
		{
			text = "Menu";
		}
		if (text == "Targeting" || text == "Looker")
		{
			if (ControlManager.isCommandDown("CmdFire", true, false, false))
			{
				Keyboard.PushCommand("CmdFire", null);
			}
			if (ControlManager.isCommandDown("CmdThrow", true, false, false))
			{
				Keyboard.PushCommand("CmdFire", null);
			}
			if (PickTargetWindow.currentMode == PickTargetWindow.TargetMode.PickDirection)
			{
				if (this.player.GetButtonDown("Accept", false, false))
				{
					Keyboard.PushMouseEvent("Meta:Navigate" + this.currentNavDirection, null);
				}
				if (this.player.GetButtonDown("Fire", false, false))
				{
					Keyboard.PushMouseEvent("Meta:Navigate" + this.currentNavDirection, null);
				}
				if (this.player.GetButtonDownRepeating("CmdMoveN", true))
				{
					Keyboard.PushMouseEvent("Meta:NavigateNorth", null);
				}
				if (this.player.GetButtonDownRepeating("CmdMoveS", true))
				{
					Keyboard.PushMouseEvent("Meta:NavigateSouth", null);
				}
				if (this.player.GetButtonDownRepeating("CmdMoveE", true))
				{
					Keyboard.PushMouseEvent("Meta:NavigateEast", null);
				}
				if (this.player.GetButtonDownRepeating("CmdMoveW", true))
				{
					Keyboard.PushMouseEvent("Meta:NavigateWest", null);
				}
				if (this.player.GetButtonDownRepeating("CmdMoveNW", true))
				{
					Keyboard.PushMouseEvent("Meta:NavigateNorthwest", null);
				}
				if (this.player.GetButtonDownRepeating("CmdMoveNE", true))
				{
					Keyboard.PushMouseEvent("Meta:NavigateNortheast", null);
				}
				if (this.player.GetButtonDownRepeating("CmdMoveSW", true))
				{
					Keyboard.PushMouseEvent("Meta:NavigateSouthwest", null);
				}
				if (this.player.GetButtonDownRepeating("CmdMoveSE", true))
				{
					Keyboard.PushMouseEvent("Meta:NavigateSoutheast", null);
				}
				this.currentNavDirection = GameManager.ResolveMovementDirection("IndicateDirection");
				if (this.currentNavDirectionDisplay == null)
				{
					global::UnityEngine.GameObject gameObject = global::UnityEngine.GameObject.Find("CurrentNavDirection");
					if (gameObject != null)
					{
						this.currentNavDirectionDisplay = gameObject.GetComponent<global::UnityEngine.UI.Text>();
					}
				}
				this.playerTracker.setActiveDirection(this.currentNavDirection);
				if (this.currentNavDirectionDisplay != null)
				{
					if (this.currentNavDirection != null)
					{
						this.currentNavDirectionDisplay.text = this.currentNavDirection;
					}
					else
					{
						this.currentNavDirectionDisplay.text = "";
					}
				}
				if (this.player.GetButtonDownRepeating("Take A Step", false) || this.player.GetButtonDownRepeating("Wait", false))
				{
					Keyboard.PushMouseEvent("Meta:Navigate" + this.currentNavDirection, null);
				}
			}
			else
			{
				if (text != "Looker")
				{
					string text2 = GameManager.ResolveMovementDirection("IndicateDirection");
					if (text2 != null)
					{
						if (this.lookDelay == 0f && this.lookRepeat == 0f)
						{
							Keyboard.PushMouseEvent("Meta:Navigate" + text2, null);
						}
						this.lookRepeat -= global::UnityEngine.Time.deltaTime;
						if (this.lookDelay <= ControlManager.delaytime)
						{
							this.lookDelay += global::UnityEngine.Time.deltaTime;
						}
						if (this.lookRepeat <= 0f && this.lookDelay >= ControlManager.delaytime)
						{
							Keyboard.PushMouseEvent("Meta:Navigate" + text2, null);
							this.lookRepeat = ControlManager.repeattime;
						}
					}
					else
					{
						this.lookRepeat = 0f;
						this.lookDelay = 0f;
					}
				}
				this.currentNavDirection = null;
				this.playerTracker.setActiveDirection(this.currentNavDirection);
				if (this.currentNavDirectionDisplay != null)
				{
					if (this.currentNavDirection != null)
					{
						this.currentNavDirectionDisplay.text = this.currentNavDirection;
					}
					else
					{
						this.currentNavDirectionDisplay.text = "";
					}
				}
				if (this.player.GetButtonDownRepeating("Navigate Vertical", false) || this.player.GetNegativeButtonDownRepeating("Navigate Vertical", false))
				{
					if (this.player.GetAxis("Navigate Vertical") > 0f)
					{
						Keyboard.PushMouseEvent("Meta:NavigateNorth", null);
					}
					if (this.player.GetAxis("Navigate Vertical") < 0f)
					{
						Keyboard.PushMouseEvent("Meta:NavigateSouth", null);
					}
				}
				else if (this.player.GetButtonDownRepeating("Navigate Horizontal", false) || this.player.GetNegativeButtonDownRepeating("Navigate Horizontal", false))
				{
					if (this.player.GetAxis("Navigate Horizontal") > 0f)
					{
						Keyboard.PushMouseEvent("Meta:NavigateEast", null);
					}
					if (this.player.GetAxis("Navigate Horizontal") < 0f)
					{
						Keyboard.PushMouseEvent("Meta:NavigateWest", null);
					}
				}
				else
				{
					if (this.player.GetButtonDownRepeating("CmdMoveN", true))
					{
						Keyboard.PushMouseEvent("Meta:NavigateNorth", null);
					}
					if (this.player.GetButtonDownRepeating("CmdMoveS", true))
					{
						Keyboard.PushMouseEvent("Meta:NavigateSouth", null);
					}
					if (this.player.GetButtonDownRepeating("CmdMoveE", true))
					{
						Keyboard.PushMouseEvent("Meta:NavigateEast", null);
					}
					if (this.player.GetButtonDownRepeating("CmdMoveW", true))
					{
						Keyboard.PushMouseEvent("Meta:NavigateWest", null);
					}
					if (this.player.GetButtonDownRepeating("CmdMoveNW", true))
					{
						Keyboard.PushMouseEvent("Meta:NavigateNorthwest", null);
					}
					if (this.player.GetButtonDownRepeating("CmdMoveNE", true))
					{
						Keyboard.PushMouseEvent("Meta:NavigateNortheast", null);
					}
					if (this.player.GetButtonDownRepeating("CmdMoveSW", true))
					{
						Keyboard.PushMouseEvent("Meta:NavigateSouthwest", null);
					}
					if (this.player.GetButtonDownRepeating("CmdMoveSE", true))
					{
						Keyboard.PushMouseEvent("Meta:NavigateSoutheast", null);
					}
				}
				if (this.player.GetButtonDown("Take A Step", false, false))
				{
					Keyboard.PushMouseEvent("Command:CmdFire", null);
				}
			}
		}
		if (ControlManager.SkipFrames > 0)
		{
			return;
		}
		if (GameManager.Instance.player.GetButton("Highlight", false))
		{
			return;
		}
		global::XRL.World.GameObject gameObject2 = The.Player;
		bool flag;
		if (gameObject2 == null)
		{
			flag = null != null;
		}
		else
		{
			ActivatedAbilities activatedAbilities = gameObject2.ActivatedAbilities;
			flag = ((activatedAbilities != null) ? activatedAbilities.AbilityByGuid : null) != null;
		}
		if (flag && this.CurrentGameView == "Stage" && CapabilityManager.AllowKeyboardHotkeys)
		{
			if (AbilityManager.PlayerAbilityLock.TryEnterReadLock(0))
			{
				try
				{
					foreach (ActivatedAbilityEntry activatedAbilityEntry in AbilityManager.PlayerAbilities)
					{
						if (ControlManager.isCommandDown(activatedAbilityEntry.Command, true, false, false))
						{
							activatedAbilityEntry.TrySendCommandEventOnPlayer();
							return;
						}
					}
				}
				finally
				{
					AbilityManager.PlayerAbilityLock.ExitReadLock();
				}
			}
			for (int i = 0; i < AbilityBar.ABILITY_COMMANDS.Count; i++)
			{
				ActivatedAbilityEntry activatedAbilityEntry2;
				if (ControlManager.isCommandDown(AbilityBar.ABILITY_COMMANDS[i], true, false, false) && AbilityBar.hotkeyAbilityCommands.TryGetValue(AbilityBar.ABILITY_COMMANDS[i], out activatedAbilityEntry2))
				{
					activatedAbilityEntry2.TrySendCommandEventOnPlayer();
					return;
				}
			}
		}
		if (this.CurrentGameView == "Stage")
		{
			if (ControlManager.isCommandDown("CmdAbilityBar1", true, false, false))
			{
				SingletonWindowBase<AbilityBar>.instance.MoveToPage(0);
			}
			if (ControlManager.isCommandDown("CmdAbilityBar2", true, false, false))
			{
				SingletonWindowBase<AbilityBar>.instance.MoveToPage(1);
			}
			if (ControlManager.isCommandDown("CmdAbilityBar3", true, false, false))
			{
				SingletonWindowBase<AbilityBar>.instance.MoveToPage(2);
			}
			if (ControlManager.isCommandDown("CmdAbilityBar4", true, false, false))
			{
				SingletonWindowBase<AbilityBar>.instance.MoveToPage(3);
			}
			if (ControlManager.isCommandDown("CmdAbilityBar5", true, false, false))
			{
				SingletonWindowBase<AbilityBar>.instance.MoveToPage(4);
			}
			if (ControlManager.isCommandDown("CmdAbilityNextPage", true, false, false))
			{
				SingletonWindowBase<AbilityBar>.instance.MoveToNextPage();
			}
			if (ControlManager.isCommandDown("CmdAbilityPrevPage", true, false, false))
			{
				SingletonWindowBase<AbilityBar>.instance.MoveToPreviousPage();
			}
		}
		for (int j = 0; j < CommandBindingManager.AutoRepeatInputActions.Count; j++)
		{
			if (ControlManager.isCommandDown(CommandBindingManager.AutoRepeatInputActions[j], true, false, false))
			{
				Keyboard.PushCommand(CommandBindingManager.AutoRepeatInputActions[j], null);
				return;
			}
		}
		if (text == "Adventure")
		{
			for (int k = 0; k < CommandBindingManager.AutoDownAdventureInputActions.Count; k++)
			{
				CommandBinding commandBinding = CommandBindingManager.AutoDownAdventureInputActions[k];
				if (ControlManager.isCommandDown(commandBinding.name, true, false, false))
				{
					Keyboard.PushCommand(commandBinding.name, null);
					return;
				}
			}
		}
		for (int l = 0; l < CommandBindingManager.AutoDownPassInputActions.Count; l++)
		{
			CommandBinding commandBinding2 = CommandBindingManager.AutoDownPassInputActions[l];
			if (ControlManager.isCommandDown(commandBinding2.name, true, false, false))
			{
				Keyboard.PushCommand(commandBinding2.name, null);
			}
		}
		for (int m = 0; m < CommandBindingManager.AutoDownInputActions.Count; m++)
		{
			CommandBinding commandBinding3 = CommandBindingManager.AutoDownInputActions[m];
			if (ControlManager.isCommandDown(commandBinding3.name, true, false, false))
			{
				Keyboard.PushCommand(commandBinding3.name, null);
				return;
			}
		}
		for (int n = 0; n < CommandBindingManager.AutoDownUIInputActions.Count; n++)
		{
			CommandBinding commandBinding4 = CommandBindingManager.AutoDownUIInputActions[n];
			if (ControlManager.isCommandDown(commandBinding4.name, true, false, false))
			{
				Keyboard.PushCommand(commandBinding4.name, null);
				return;
			}
		}
		if (ControlManager.IsLayerEnabled("Menus") && UIManager.instance.PassthroughOnTop())
		{
			if (this.player.GetButtonDown("Accept", false, false))
			{
				Keyboard.PushKey(new Keyboard.XRLKeyEvent(global::UnityEngine.KeyCode.Space, '\0', false, false, false), false);
			}
			if (this.player.GetButtonDown("Cancel", false, false))
			{
				Keyboard.PushKey(new Keyboard.XRLKeyEvent(global::UnityEngine.KeyCode.Escape, '\0', false, false, false), false);
			}
			if (Input.GetMouseButtonDown(1))
			{
				Keyboard.PushKey(global::UnityEngine.KeyCode.Escape);
			}
		}
		if (this.currentNavCategory != text)
		{
			this.SetActiveLayersForNavCategory(text, true);
			return;
		}
		if (!UIManager.instance.AllowPassthroughInput())
		{
			return;
		}
		if (this.currentNavCategory != null && Options.ModernUI && ControlManager.IsLayerEnabled("Conversation"))
		{
			if (this.player.GetButtonDown("Cancel", false, false))
			{
				Keyboard.PushKey(new Keyboard.XRLKeyEvent(global::UnityEngine.KeyCode.Escape, '\0', false, false, false), false);
			}
			if (this.player.GetButtonDown("Trade", false, false))
			{
				Keyboard.PushKey(global::UnityEngine.KeyCode.Tab);
			}
			return;
		}
		if (this.currentUIView != null && !(this.currentUIView.NavCategory == "Menu") && !(this.currentUIView.UICanvas == "Stage") && !(this.currentUIView.UICanvas == "Popup:Text") && !(this.currentUIView.UICanvas == "Looker") && text == "StringInput")
		{
			if (this.player.GetButtonDown("Accept", false, false))
			{
				Keyboard.PushKey(new Keyboard.XRLKeyEvent(global::UnityEngine.KeyCode.Return, '\0', false, false, false), false);
			}
			if (this.player.GetButtonDown("Cancel", false, false))
			{
				Keyboard.PushMouseEvent("Meta:Cancel", null);
			}
			return;
		}
		if (this.player.GetButtonDown("Accept", false, false) && this.CurrentGameView == "AbilityManager")
		{
			Keyboard.PushKey(new Keyboard.XRLKeyEvent(global::UnityEngine.KeyCode.Space, ' ', false, false, false), false);
		}
		if (ControlManager.IsLayerEnabled("Menus"))
		{
			if (ControlManager.GetButtonDownRepeating("Page Up"))
			{
				Keyboard.PushKey(global::UnityEngine.KeyCode.PageUp);
			}
			if (ControlManager.GetButtonDownRepeating("Page Down"))
			{
				Keyboard.PushKey(global::UnityEngine.KeyCode.PageDown);
			}
			if (this.player.GetButtonDown("V Positive", false, false))
			{
				Keyboard.PushKey(global::UnityEngine.KeyCode.KeypadPlus);
			}
			if (this.player.GetButtonDown("V Negative", false, false))
			{
				Keyboard.PushKey(global::UnityEngine.KeyCode.KeypadMinus);
			}
		}
		if (ControlManager.IsLayerEnabled("Menus") && !ControlManager.IsLayerEnabled("Targeting") && !ControlManager.IsLayerEnabled("Looker"))
		{
			if (this.player.GetButtonDownRepeating("Navigate Vertical", false) || this.player.GetNegativeButtonDownRepeating("Navigate Vertical", false))
			{
				if (this.player.GetAxis("Navigate Vertical") > 0f)
				{
					Keyboard.PushMouseEvent("Meta:NavigateNorth", null);
				}
				if (this.player.GetAxis("Navigate Vertical") < 0f)
				{
					Keyboard.PushMouseEvent("Meta:NavigateSouth", null);
				}
			}
			else if (this.player.GetButtonDownRepeating("Navigate Horizontal", false) || this.player.GetNegativeButtonDownRepeating("Navigate Horizontal", false))
			{
				if (this.player.GetAxis("Navigate Horizontal") > 0f)
				{
					Keyboard.PushMouseEvent("Meta:NavigateEast", null);
				}
				if (this.player.GetAxis("Navigate Horizontal") < 0f)
				{
					Keyboard.PushMouseEvent("Meta:NavigateWest", null);
				}
			}
			if (ControlManager.isCommandDown("Navigate Up", true, false, false))
			{
				Keyboard.PushKey(global::UnityEngine.KeyCode.Keypad8);
			}
			if (ControlManager.isCommandDown("Navigate Down", true, false, false))
			{
				Keyboard.PushKey(global::UnityEngine.KeyCode.Keypad2);
			}
			if (ControlManager.isCommandDown("Navigate Left", true, false, false))
			{
				Keyboard.PushKey(global::UnityEngine.KeyCode.Keypad4);
			}
			if (ControlManager.isCommandDown("Navigate Right", true, false, false))
			{
				Keyboard.PushKey(global::UnityEngine.KeyCode.Keypad6);
			}
			if (ControlManager.GetButtonDownRepeating("Page Left"))
			{
				Keyboard.PushKey(global::UnityEngine.KeyCode.Keypad7);
			}
			if (ControlManager.GetButtonDownRepeating("Page Right"))
			{
				Keyboard.PushKey(global::UnityEngine.KeyCode.Keypad9);
			}
			if (this.player.GetButtonDown("New Map Pin", false, false))
			{
				Keyboard.PushCommand("New Map Pin", null);
			}
		}
		if (ControlManager.IsLayerEnabled("Conversation") && this.player.GetButtonDown("Trade", false, false))
		{
			Keyboard.PushKey(global::UnityEngine.KeyCode.Tab);
		}
		if (text == "Adventure")
		{
			GameManager.<UpdateInput>g__DoNavigate|230_0("CmdMoveN", "CmdMoveN", "CmdAutoMoveN");
			GameManager.<UpdateInput>g__DoNavigate|230_0("CmdMoveS", "CmdMoveS", "CmdAutoMoveS");
			GameManager.<UpdateInput>g__DoNavigate|230_0("CmdMoveE", "CmdMoveE", "CmdAutoMoveE");
			GameManager.<UpdateInput>g__DoNavigate|230_0("CmdMoveW", "CmdMoveW", "CmdAutoMoveW");
			GameManager.<UpdateInput>g__DoNavigate|230_0("CmdMoveNE", "CmdMoveNE", "CmdAutoMoveNE");
			GameManager.<UpdateInput>g__DoNavigate|230_0("CmdMoveNW", "CmdMoveNW", "CmdAutoMoveNW");
			GameManager.<UpdateInput>g__DoNavigate|230_0("CmdMoveSE", "CmdMoveSE", "CmdAutoMoveSE");
			GameManager.<UpdateInput>g__DoNavigate|230_0("CmdMoveSW", "CmdMoveSW", "CmdAutoMoveSW");
			if (this.player.GetButtonDown("CmdSystemMenu", false, false))
			{
				Keyboard.PushCommand("CmdSystemMenu", null);
			}
			if (this.player.GetButtonDown("System", false, false))
			{
				Keyboard.PushMouseEvent("Meta:System", null);
				ControlManager.ResetInput(false, true);
			}
			if (!Options.ModernUI)
			{
				if (this.player.GetButtonDown("Next Ability", false, false))
				{
					int num = this.selectedAbility;
					this.selectedAbility = num + 1;
					if (this.selectedAbility >= AbilityAPI.abilityCount)
					{
						this.selectedAbility = 0;
					}
					this.UpdateSelectedAbility();
				}
				if (this.player.GetButtonDown("Previous Ability", false, false))
				{
					int num = this.selectedAbility;
					this.selectedAbility = num - 1;
					if (this.selectedAbility < 0)
					{
						this.selectedAbility = AbilityAPI.abilityCount - 1;
					}
					this.UpdateSelectedAbility();
				}
				if (this.player.GetButtonDown("Use Ability", false, false))
				{
					ActivatedAbilityEntry activatedAbilityEntry3 = this.currentlySelectedAbility;
					if (activatedAbilityEntry3 != null)
					{
						activatedAbilityEntry3.TrySendCommandEventOnPlayer();
					}
				}
			}
			if (this.player.GetButtonDown("CmdMoveU", false, false))
			{
				Keyboard.PushMouseEvent("Command:CmdMoveU", null);
			}
			if (this.player.GetButtonDown("CmdMoveD", false, false))
			{
				Keyboard.PushMouseEvent("Command:CmdMoveD", null);
			}
			if (this.player.GetButtonDownRepeating("CmdAttackN", false))
			{
				Keyboard.PushMouseEvent("Command:CmdAttackN", null);
			}
			if (this.player.GetButtonDownRepeating("CmdAttackS", false))
			{
				Keyboard.PushMouseEvent("Command:CmdAttackS", null);
			}
			if (this.player.GetButtonDownRepeating("CmdAttackE", false))
			{
				Keyboard.PushMouseEvent("Command:CmdAttackE", null);
			}
			if (this.player.GetButtonDownRepeating("CmdAttackW", false))
			{
				Keyboard.PushMouseEvent("Command:CmdAttackW", null);
			}
			if (this.player.GetButtonDownRepeating("CmdAttackNW", false))
			{
				Keyboard.PushMouseEvent("Command:CmdAttackNW", null);
			}
			if (this.player.GetButtonDownRepeating("CmdAttackNE", false))
			{
				Keyboard.PushMouseEvent("Command:CmdAttackNE", null);
			}
			if (this.player.GetButtonDownRepeating("CmdAttackSW", false))
			{
				Keyboard.PushMouseEvent("Command:CmdAttackSW", null);
			}
			if (this.player.GetButtonDownRepeating("CmdAttackSE", false))
			{
				Keyboard.PushMouseEvent("Command:CmdAttackSE", null);
			}
			if (this.player.GetButtonDownRepeating("CmdAttackU", false))
			{
				Keyboard.PushMouseEvent("Command:CmdAttackU", null);
			}
			if (this.player.GetButtonDownRepeating("CmdAttackD", false))
			{
				Keyboard.PushMouseEvent("Command:CmdAttackD", null);
			}
			if (this.player.GetButtonDown("CmdMoveToPointOfInterest", false, false))
			{
				Keyboard.PushMouseEvent("Command:CmdMoveToPointOfInterest", null);
			}
			if (this.player.GetButtonDown("Character Sheet", false, false))
			{
				Keyboard.PushMouseEvent("Meta:CharacterSheet", null);
			}
			if (this.player.GetButtonDown("CmdZoomIn", false, false))
			{
				this.ZoomIn();
			}
			if (this.player.GetButtonDown("CmdZoomOut", false, false))
			{
				this.ZoomOut();
			}
			if (this.player.GetButtonDown("CmdWish", false, false))
			{
				Keyboard.PushMouseEvent("Command:CmdWish", null);
			}
			if (this.player.GetButtonDown("CmdQuests", false, false))
			{
				Keyboard.PushMouseEvent("Command:CmdQuests", null);
			}
			if (this.player.GetButtonDown("CmdSave", false, false))
			{
				Keyboard.PushMouseEvent("Command:CmdSave", null);
			}
			if (this.player.GetButtonDown("CmdLoad", false, false))
			{
				Keyboard.PushMouseEvent("Command:CmdLoad", null);
			}
			this.currentNavDirection = GameManager.ResolveMovementDirection("IndicateDirection");
			this.playerTracker.setActiveDirection(this.currentNavDirection);
			if (this.player.GetButtonDownRepeating("ForceAttack", false) && !(this.currentNavDirection == ".") && this.currentNavDirection != null)
			{
				Keyboard.PushMouseEvent("Command:CmdAttack" + this.LongDirectionToShortDirection(this.currentNavDirection), null);
			}
			int num2 = ControlManager.isCommandDownValue("Take A Step", true, false, false);
			if (num2 > 0)
			{
				if (this.currentNavDirection == "." || this.currentNavDirection == null)
				{
					if (GameManager.TakeAStepFilterMode <= 1)
					{
						GameManager.TakeAStepFilterMode = 1;
						Keyboard.PushCommand("CmdWait", null);
					}
				}
				else
				{
					GameManager.TakeAStepFilterMode = 2;
					if (this.player.GetButton("GamepadAlt", false))
					{
						Keyboard.PushMouseEvent("Command:CmdAttack" + this.LongDirectionToShortDirection(this.currentNavDirection), null);
					}
					else
					{
						if (num2 == 1)
						{
							Keyboard.PushCommand("CmdMove" + this.currentNavDirection, null);
						}
						if (num2 == 2)
						{
							Keyboard.PushCommand("CmdAutoMove" + this.currentNavDirection, null);
						}
					}
				}
			}
			if (GameManager.TakeAStepFilterMode > 0 && !ControlManager.isCommandPressed("Take A Step"))
			{
				GameManager.TakeAStepFilterMode = 0;
				ControlManager.ConsumeAllInput("Take A Step");
			}
			if (this.player.GetButtonDown("CmdGetFrom", false, false))
			{
				if (this.currentNavDirection == "." || this.currentNavDirection == null)
				{
					Keyboard.PushCommand("CmdGetFrom", null);
				}
				else
				{
					Keyboard.PushCommand("CmdGetFrom" + this.LongDirectionToShortDirection(this.currentNavDirection), null);
				}
			}
			if (this.player.GetButtonDown("CmdUse", false, false))
			{
				if (this.currentNavDirection == "." || this.currentNavDirection == null)
				{
					Keyboard.PushCommand("CmdUse", null);
				}
				else
				{
					Keyboard.PushCommand("CmdUse" + this.LongDirectionToShortDirection(this.currentNavDirection), null);
				}
			}
			if (GameManager.ResolveMovementDirection("LookDirection") != null)
			{
				Look.bLocked = false;
				Keyboard.PushMouseEvent("Command:CmdLook", null);
			}
		}
		else if (!ControlManager.IsLayerEnabled("Targeting"))
		{
			this.playerTracker.setActiveDirection(null);
		}
		if (text == "Looker")
		{
			if (this.player.GetButtonDown("Accept", false, false))
			{
				Keyboard.PushCommand("Interact", null);
			}
			if (this.player.GetButtonDown("GamepadAlt", false, false))
			{
				Look.bLocked = !Look.bLocked;
			}
			if (ControlManager.GetButtonDown("CmdWalk", false))
			{
				Keyboard.PushCommand("CmdWalk", null);
			}
			string text3 = GameManager.ResolveMovementDirection("LookDirection");
			if (text3 != null)
			{
				if (this.lookDelay == 0f && this.lookRepeat == 0f)
				{
					Keyboard.PushMouseEvent("Meta:Navigate" + text3, null);
				}
				this.lookRepeat -= global::UnityEngine.Time.deltaTime;
				this.lookDelay += global::UnityEngine.Time.deltaTime;
				if (this.lookRepeat <= 0f && this.lookDelay > ControlManager.delaytime)
				{
					Keyboard.PushMouseEvent("Meta:Navigate" + text3, null);
					this.lookRepeat = ControlManager.repeattime;
					return;
				}
			}
			else
			{
				this.lookRepeat = 0f;
				this.lookDelay = 0f;
			}
		}
	}

	// Token: 0x06000332 RID: 818 RVA: 0x00014C00 File Offset: 0x00012E00
	public static string ResolveMovementDirection(string Axis)
	{
		return ControlManager.ResolveAxisDirection(Axis);
	}

	// Token: 0x06000333 RID: 819 RVA: 0x00014C08 File Offset: 0x00012E08
	public static bool IsOnUIContext()
	{
		return SynchronizationContext.Current == The.UiContext;
	}

	// Token: 0x06000334 RID: 820 RVA: 0x00014C16 File Offset: 0x00012E16
	public static bool IsOnGameContext()
	{
		return SynchronizationContext.Current == The.GameContext;
	}

	// Token: 0x17000055 RID: 85
	// (get) Token: 0x06000335 RID: 821 RVA: 0x00014C24 File Offset: 0x00012E24
	// (set) Token: 0x06000336 RID: 822 RVA: 0x00014C2B File Offset: 0x00012E2B
	public static bool runPlayerTurnOnUIThread
	{
		get
		{
			return GameManager._runPlayerTurnOnUIThread;
		}
		set
		{
			GameManager._runPlayerTurnOnUIThread = value;
			global::UnityEngine.Debug.LogWarning("runPlayerTurnSet:" + GameManager._runPlayerTurnOnUIThread.ToString());
		}
	}

	// Token: 0x17000056 RID: 86
	// (get) Token: 0x06000337 RID: 823 RVA: 0x00014C4C File Offset: 0x00012E4C
	// (set) Token: 0x06000338 RID: 824 RVA: 0x00014C54 File Offset: 0x00012E54
	public bool fadeToBlack
	{
		get
		{
			return this._fadingToBlack;
		}
		set
		{
			if (value)
			{
				this.originalBrightness = float.MinValue;
				this.fadeToBlackStage = 0;
				this.fadingToBlackTimer = 0f;
			}
			this._fadingToBlack = value;
		}
	}

	// Token: 0x17000057 RID: 87
	// (get) Token: 0x06000339 RID: 825 RVA: 0x00014C7D File Offset: 0x00012E7D
	public bool NeedsToPan
	{
		get
		{
			return this.TargetZoomFactor > 1f || (Options.PlayScale != Options.PlayAreaScaleTypes.Fit && GameManager.MainCameraLetterbox.IsOverflowing);
		}
	}

	// Token: 0x17000058 RID: 88
	// (get) Token: 0x0600033A RID: 826 RVA: 0x00014CA1 File Offset: 0x00012EA1
	public bool NeedsArrowBorder
	{
		get
		{
			return this.TargetZoomFactor > 1f || Options.PlayScale > Options.PlayAreaScaleTypes.Fit;
		}
	}

	// Token: 0x0600033B RID: 827 RVA: 0x00014CBC File Offset: 0x00012EBC
	public void ShowStage()
	{
		if (Options.ModernUI)
		{
			if (!UIManager.getWindow("Stage").canvas.enabled || UIManager.getWindow("Stage").canvasGroup.alpha == 0f)
			{
				UIManager.getWindow("Stage").Show();
				UIManager.getWindow("PlayerStatusBar").Show();
				UIManager.getWindow("AbilityBar").Show();
				UIManager.getWindow("MessageLog").Show();
				UIManager.getWindow<MessageLogWindow>("MessageLog").Show();
				UIManager.getWindow<NearbyItemsWindow>("NearbyItems").ShowIfEnabled();
				UIManager.getWindow<MinimapWindow>("Minimap").ShowIfEnabled();
				return;
			}
		}
		else
		{
			this.HideStage();
		}
	}

	// Token: 0x0600033C RID: 828 RVA: 0x00014D74 File Offset: 0x00012F74
	public void HideStage()
	{
		UIManager.getWindow("Stage").Hide();
		UIManager.getWindow("PlayerStatusBar").Hide();
		UIManager.getWindow("AbilityBar").Hide();
		UIManager.getWindow("MessageLog").Hide();
		UIManager.getWindow("NearbyItems").Hide();
		UIManager.getWindow("Minimap").Hide();
	}

	// Token: 0x0600033D RID: 829 RVA: 0x00014DDC File Offset: 0x00012FDC
	public override void OnUpdate()
	{
		GameManager.dirtyFocused = Application.isFocused;
		if (The.Game != null && Application.isFocused)
		{
			The.Game.realtimeSinceStartup = global::UnityEngine.Time.realtimeSinceStartup;
			The.Game.frameDelta = global::UnityEngine.Time.deltaTime;
		}
		ControlManager.OnUpdate();
		GameManager.UpdateCountFromChangedToActiveView++;
		GameManager.frame += 1UL;
		if (this.consoleTileAreaHandler.activeSelf != !Options.ModernUI)
		{
			this.consoleTileAreaHandler.SetActive(!Options.ModernUI);
		}
		if (this.fadeToBlack)
		{
			if (this.originalBrightness == -3.4028235E+38f)
			{
				this.originalBrightness = global::UnityEngine.GameObject.Find("Main Camera").GetComponent<CC_BrightnessContrastGamma>().brightness;
			}
			if (this.fadeToBlackStage == 0)
			{
				this.fadingToBlackTimer += global::UnityEngine.Time.deltaTime;
				if (this.fadingToBlackTimer < 2f)
				{
					global::UnityEngine.GameObject.Find("Main Camera").GetComponent<CC_BrightnessContrastGamma>().brightness = Mathf.Lerp(this.originalBrightness, -100f, this.fadingToBlackTimer / 2f);
				}
				else if (this.fadingToBlackTimer < 3f)
				{
					global::UnityEngine.GameObject.Find("Main Camera").GetComponent<CC_BrightnessContrastGamma>().brightness = -100f;
					this.fadeToBlackStage = 1;
				}
			}
			if (this.fadeToBlackStage == 3)
			{
				this.fadingToBlackTimer += global::UnityEngine.Time.deltaTime;
				if (this.fadingToBlackTimer < 5f)
				{
					global::UnityEngine.GameObject.Find("Main Camera").GetComponent<CC_BrightnessContrastGamma>().brightness = Mathf.Lerp(-100f, this.originalBrightness, (this.fadingToBlackTimer - 3f) / 2f);
				}
				else
				{
					global::UnityEngine.GameObject.Find("Main Camera").GetComponent<CC_BrightnessContrastGamma>().brightness = this.originalBrightness;
					this.fadeToBlack = false;
					this.fadingToBlackTimer = 0f;
					this.originalBrightness = float.MinValue;
					this.gameQueue.queueTask(delegate
					{
						Thread.Sleep(2000);
						XRLCore.Core.Game.FinishQuestStep("Tomb of the Eaters", "Ascend the Tomb and Cross into Brightsheol", -1, true, null);
					}, 0);
				}
			}
		}
		if (GameManager.mouseDisable > 0f)
		{
			GameManager.mouseDisable -= global::UnityEngine.Time.deltaTime;
		}
		this.UpdateInput();
		if (Input.GetKeyDown(global::UnityEngine.KeyCode.F8) && (Input.GetKey(global::UnityEngine.KeyCode.LeftControl) || Input.GetKey(global::UnityEngine.KeyCode.RightControl)))
		{
			TutorialManager.ToggleHide();
		}
		this.uiQueue.executeTasks();
		if (this.pauseUpdates)
		{
			if (ControlManager.GetButtonDown("Cancel", false))
			{
				this.pauseUpdates = false;
				return;
			}
			return;
		}
		else
		{
			if (XRLCore.bStarted && this.lastFullscreen != Screen.fullScreen)
			{
				if (Screen.fullScreen != Options.DisplayFullscreen)
				{
					if (Options.HasOption("OptionDisplayFullscreen"))
					{
						Options.SetOption("OptionDisplayFullscreen", Screen.fullScreen ? "Yes" : "No");
					}
					else
					{
						Options.SetOption("OptionDisplayFullscreen", "Yes");
						Screen.fullScreen = true;
					}
				}
				this.lastFullscreen = Screen.fullScreen;
			}
			if (this.Spacefolding)
			{
				GameManager.MainCamera.gameObject.GetComponent<CC_RadialBlur>().enabled = true;
				this._spacefoldingT += global::UnityEngine.Time.deltaTime;
				GameManager.MainCamera.gameObject.GetComponent<CC_RadialBlur>().amount = 1f - Easing.BounceEaseInOut(this._spacefoldingT / 2f);
				if (this._spacefoldingT > 2f)
				{
					GameManager.MainCamera.gameObject.GetComponent<CC_RadialBlur>().enabled = false;
					this._spacefolding = false;
				}
			}
			if (this._Spin > 0f)
			{
				this._CurrentSpin += global::UnityEngine.Time.deltaTime;
				GameManager.MainCamera.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 360f * (Math.Min(this._Spin, this._CurrentSpin) - this._Spin) / this._Spin);
				if (this._CurrentSpin >= this._Spin)
				{
					GameManager.MainCamera.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
					this._Spin = 0f;
				}
			}
			if (this.Fuzzing)
			{
				CC_AnalogTV component = GameManager.MainCamera.gameObject.GetComponent<CC_AnalogTV>();
				component.enabled = true;
				component.noiseIntensity = (0.5f - this._fuzzingT) * 2f;
				this._fuzzingT += global::UnityEngine.Time.deltaTime;
				if ((double)this._fuzzingT > 0.5)
				{
					component.enabled = Options.DisplayScanlines;
					component.noiseIntensity = 0.045f;
					this.Fuzzing = false;
				}
			}
			if (this._ThinWorldDistoring > 0f && !Options.DisableFullscreenWarpEffects)
			{
				CC_AnalogTV component2 = GameManager.MainCamera.gameObject.GetComponent<CC_AnalogTV>();
				this._ThinWorldDistoring -= global::UnityEngine.Time.deltaTime;
				if (this._ThinWorldDistoring <= 0f)
				{
					component2.distortion = 0f;
					component2.cubicDistortion = 0f;
					component2.scale = 1f;
					component2.noiseIntensity = 0.045f;
				}
				else
				{
					component2.distortion = Mathf.Lerp(-2f, 0f, 1f - this._ThinWorldDistoring / this.MAX_THIN_WORLD_DISTORTING);
					component2.cubicDistortion = Mathf.Lerp(-2f, 0f, 1f - this._ThinWorldDistoring / this.MAX_THIN_WORLD_DISTORTING);
					component2.scale = 1f - this._ThinWorldDistoring / this.MAX_THIN_WORLD_DISTORTING;
					component2.noiseIntensity = Mathf.Lerp(1f, 0.045f, 1f - this._ThinWorldDistoring / this.MAX_THIN_WORLD_DISTORTING);
				}
			}
			if (this._StaticEffecting > 0 && !Options.DisableFullscreenWarpEffects)
			{
				CC_Pixelate component3 = GameManager.MainCamera.gameObject.GetComponent<CC_Pixelate>();
				if ((double)this._StaticEffectTime > (double)this._StaticEffecting * 0.25)
				{
					this._StaticEffecting = 0;
					this._StaticEffectTime = 0f;
					component3.enabled = false;
				}
				else
				{
					component3.enabled = true;
					this._StaticEffectTime += global::UnityEngine.Time.deltaTime;
					this._NextStaticEffectGlitch -= global::UnityEngine.Time.deltaTime;
					if (this._NextStaticEffectGlitch <= 0f)
					{
						this._NextStaticEffectGlitch = (float)Stat.RandomCosmetic(50, 200) / 1000f;
						component3.scale = (float)Stat.RandomCosmetic(140, 200) + this._StaticEffectTime * 200f;
					}
				}
			}
			if (!this.Greyscaling && this.GreyscaleLevel > 0)
			{
				this.Greyscaling = true;
				GameManager.MainCamera.gameObject.GetComponent<CC_Grayscale>().amount = 0f;
				GameManager.MainCamera.gameObject.GetComponent<CC_Grayscale>().enabled = true;
			}
			else if (this.Greyscaling && this.GreyscaleLevel <= 0)
			{
				if (GameManager.MainCamera.gameObject.GetComponent<CC_Grayscale>().amount > 0f)
				{
					GameManager.MainCamera.gameObject.GetComponent<CC_Grayscale>().amount -= global::UnityEngine.Time.deltaTime * 0.5f;
				}
				else
				{
					this.Greyscaling = false;
					GameManager.MainCamera.gameObject.GetComponent<CC_Grayscale>().amount = 0f;
					GameManager.MainCamera.gameObject.GetComponent<CC_Grayscale>().enabled = false;
				}
			}
			if (this.Greyscaling && GameManager.MainCamera.gameObject.GetComponent<CC_Grayscale>().amount < 1f)
			{
				GameManager.MainCamera.gameObject.GetComponent<CC_Grayscale>().amount += global::UnityEngine.Time.deltaTime * 0.2f;
			}
			if (Options.DisableFullscreenWarpEffects)
			{
				if (this.Hallucinating)
				{
					this.Hallucinating = false;
					GameManager.MainCamera.gameObject.GetComponent<CC_Wiggle>().enabled = false;
				}
			}
			else if (!this.Hallucinating && FungalVisionary.VisionLevel > 0)
			{
				this.Hallucinating = true;
				GameManager.MainCamera.gameObject.GetComponent<CC_Wiggle>().enabled = true;
			}
			else if (this.Hallucinating && FungalVisionary.VisionLevel <= 0)
			{
				this.Hallucinating = false;
				GameManager.MainCamera.gameObject.GetComponent<CC_Wiggle>().enabled = false;
			}
			if (Globals.RenderMode != RenderModeType.Text)
			{
				bool disableImposters = Options.DisableImposters;
			}
			bool hallucinating = this.Hallucinating;
			if (!XRLCore.bThreadFocus)
			{
				SoundManager.Update();
				Thread.Sleep(250);
				return;
			}
			if (this.bFirstUpdate)
			{
				GameManager.Instance = this;
				GameManager.MainCamera = this.GameObjectFind("Main Camera");
				this.MainCanvas = this.GameObjectFind("UI Manager").GetComponent<Canvas>();
				this.bFirstUpdate = false;
			}
			ClipboardHelper.UpdateFromMainThread();
			SoundManager.Update();
			if (Screen.width != this.LastWidth || Screen.height != this.LastHeight)
			{
				Sidebar.bOverlayUpdated = true;
				this.LastWidth = Screen.width;
				this.LastHeight = Screen.height;
				this.UpdatePreferredSidebarPosition();
			}
			if (this._overlayOptionsUpdated)
			{
				RingDeque<string> ringDeque = this.GameViewStack;
				lock (ringDeque)
				{
					this._overlayOptionsUpdated = false;
					this.UpdateView();
				}
			}
			this.RefreshLayout(true);
			if (!string.IsNullOrEmpty(Social.TweetThis))
			{
				string tweetThis = Social.TweetThis;
				Social.TweetThis = null;
				Application.OpenURL("http://www.twitter.com/intent/tweet?text=" + Uri.EscapeDataString(tweetThis) + "&via=cavesofqud&hashtags=cavesofqud");
			}
			bool flag2 = this.bEditorMode;
			if (!this.bInitStarted)
			{
				this.StartGameThread();
				this.bInitStarted = true;
			}
			if (this.TV.noiseIntensity != this.TargetIntensity)
			{
				this.TV.noiseIntensity = Mathf.MoveTowards(this.TV.noiseIntensity, this.TargetIntensity, global::UnityEngine.Time.deltaTime * 1f);
			}
			if (!this.bInitComplete)
			{
				return;
			}
			if ((XRLCore.bStarted && XRLCore.CoreThread.ThreadState == global::System.Threading.ThreadState.Stopped) || !XRLCore.bStarted)
			{
				global::UnityEngine.Debug.Log("Exiting main thread due to stopped game thread." + XRLCore.CoreThread.ThreadState.ToString());
				if (this.platform.SupportsQuit)
				{
					this.platform.Quit();
				}
			}
			if (MetricsManager.ManagerObject == null && Globals.EnableMetrics)
			{
				MetricsManager.Init();
			}
			MetricsManager.Update();
			if (TextConsole.BufferUpdated)
			{
				RingDeque<string> ringDeque = this.GameViewStack;
				lock (ringDeque)
				{
					object bufferCS = TextConsole.BufferCS;
					lock (bufferCS)
					{
						if (this._ActiveGameView != this.CurrentGameView || this.bViewUpdated)
						{
							ControlManager.ResetInput(false, true);
							this.bViewUpdated = false;
							this.UpdateView();
						}
						if (!this.FadeSplash)
						{
							global::UnityEngine.GameObject gameObject = global::UnityEngine.GameObject.Find("Splash");
							if (gameObject != null)
							{
								this.FadeSplash = true;
								LeanTween.alpha(gameObject, 0f, 2f);
								gameObject.AddComponent<global::Temporary>().Duration = 2f;
								gameObject.GetComponent<global::Temporary>().BeforeDestroy = delegate
								{
									this.GameObjectFind("Splash").Destroy();
								};
							}
						}
						while (TextConsole.bufferExtras.Count > 0)
						{
							this.ProcessBufferExtra(TextConsole.bufferExtras.Dequeue());
						}
						if (TextConsole.CurrentBuffer.focusPosition != Point2D.invalid)
						{
							this.SetPlayerCell(TextConsole.CurrentBuffer.focusPosition, false);
						}
						this.RequestedBackdrops.Clear();
						int num = 0;
						for (int i = 0; i < 80; i++)
						{
							for (int j = 0; j < 25; j++)
							{
								ConsoleChar consoleChar = TextConsole.CurrentBuffer.Buffer[i, j];
								ex3DSprite2 ex3DSprite = this.ConsoleCharacter[i, j];
								Transform transform = this.ConsoleCharacterTransform[i, j];
								consoleChar.BeforeRender(i, j, ex3DSprite, TextConsole.CurrentBuffer);
								if (!(consoleChar.WantsBackdrop == "backdropBlack") && !this.RequestedBackdrops.Contains(consoleChar.WantsBackdrop) && !string.IsNullOrEmpty(consoleChar.WantsBackdrop))
								{
									this.RequestedBackdrops.Add(consoleChar.WantsBackdrop);
									global::UnityEngine.GameObject gameObject2;
									if (!this.BackdropsByName.TryGetValue(consoleChar.WantsBackdrop, out gameObject2))
									{
										gameObject2 = global::UnityEngine.Object.Instantiate<global::UnityEngine.GameObject>(Resources.Load(consoleChar.WantsBackdrop) as global::UnityEngine.GameObject);
										gameObject2.SetActive(true);
										gameObject2.name = consoleChar.WantsBackdrop;
										gameObject2.transform.position = new Vector3(0f, 0f, gameObject2.transform.position.z);
										this.BackdropsByName.Add(consoleChar.WantsBackdrop, gameObject2);
										this.Backdrops.Add(gameObject2);
									}
									if (!gameObject2.activeSelf)
									{
										gameObject2.SetActive(true);
									}
								}
								if (!consoleChar.HFlip && !consoleChar.VFlip)
								{
									if (transform.localScale != Vector3.one)
									{
										transform.localScale = Vector3.one;
										BoxCollider component4 = ex3DSprite.GetComponent<BoxCollider>();
										component4.size = new Vector3(Math.Abs(component4.size.x), Math.Abs(component4.size.y), Math.Abs(component4.size.z));
									}
								}
								else if (consoleChar.HFlip && consoleChar.VFlip)
								{
									if (transform.localScale != -Vector3.one)
									{
										transform.localScale = -Vector3.one;
										BoxCollider component5 = ex3DSprite.GetComponent<BoxCollider>();
										component5.size = new Vector3(-Math.Abs(component5.size.x), -Math.Abs(component5.size.y), -Math.Abs(component5.size.z));
									}
								}
								else if (consoleChar.HFlip)
								{
									if (transform.localScale != new Vector3(-1f, 1f, 1f))
									{
										transform.localScale = new Vector3(-1f, 1f, 1f);
										BoxCollider component6 = ex3DSprite.GetComponent<BoxCollider>();
										component6.size = new Vector3(-Math.Abs(component6.size.x), Math.Abs(component6.size.y), Math.Abs(component6.size.z));
									}
								}
								else if (consoleChar.VFlip && transform.localScale != new Vector3(1f, -1f, 1f))
								{
									transform.localScale = new Vector3(1f, -1f, 1f);
									BoxCollider component7 = ex3DSprite.GetComponent<BoxCollider>();
									component7.size = new Vector3(Math.Abs(component7.size.x), -Math.Abs(component7.size.y), Math.Abs(component7.size.z));
								}
								if (consoleChar.Char == '\0')
								{
									string tile = consoleChar.Tile;
									Color foreground = consoleChar.Foreground;
									Color color = consoleChar.Background;
									Color detail = consoleChar.Detail;
									if (consoleChar.BackdropBleedthrough)
									{
										color = Color.clear;
									}
									if (consoleChar.WantsBackdrop == "backdropBlack")
									{
										color = GameManager.backdropBlackColor;
									}
									bool flag4 = this.CurrentTile[i, j] != tile;
									if (flag4 || !this.ColorMatch(ex3DSprite.color, foreground) || !this.ColorMatch(ex3DSprite.backcolor, color) || !this.ColorMatch(ex3DSprite.detailcolor, detail))
									{
										if (flag4)
										{
											exTextureInfo textureInfo = SpriteManager.GetTextureInfo(tile, true);
											if (textureInfo == null)
											{
												global::UnityEngine.Debug.LogError("Invalid TextureID: " + tile);
												this.CurrentTile[i, j] = tile;
												this.ConsoleCharacter[i, j].textureInfo = null;
											}
											else
											{
												this.CurrentTile[i, j] = tile;
												ex3DSprite.textureInfo = textureInfo;
												if (textureInfo.ShaderMode != this.CurrentShadermode[i, j])
												{
													ex3DSprite.shader = SpriteManager.GetShaderMode(textureInfo.ShaderMode);
													this.CurrentShadermode[i, j] = textureInfo.ShaderMode;
												}
											}
										}
										num++;
										ex3DSprite.color = foreground;
										ex3DSprite.detailcolor = detail;
										ex3DSprite.backcolor = color;
									}
								}
								else
								{
									char c = consoleChar.Char;
									if (c < '\0' || c > 'ÿ')
									{
										c = ' ';
									}
									exTextureInfo exTextureInfo = this.CharInfos[(int)c];
									Color foreground2 = consoleChar.Foreground;
									Color background = consoleChar.Background;
									if (exTextureInfo != null && exTextureInfo.ShaderMode != this.CurrentShadermode[i, j])
									{
										ex3DSprite.shader = SpriteManager.GetShaderMode(exTextureInfo.ShaderMode);
										this.CurrentShadermode[i, j] = exTextureInfo.ShaderMode;
									}
									if (ex3DSprite.textureInfo != exTextureInfo || !this.ColorMatch(ex3DSprite.backcolor, foreground2) || !this.ColorMatch(ex3DSprite.detailcolor, foreground2) || !this.ColorMatch(ex3DSprite.color, background))
									{
										num++;
										this.CurrentTile[i, j] = null;
										ex3DSprite.textureInfo = exTextureInfo;
										ex3DSprite.backcolor = foreground2;
										ex3DSprite.detailcolor = foreground2;
										ex3DSprite.color = background;
									}
								}
								consoleChar.AfterRender(i, j, ex3DSprite, TextConsole.CurrentBuffer);
							}
						}
						for (int k = 0; k < this.Backdrops.Count; k++)
						{
							if (!this.RequestedBackdrops.Contains(this.Backdrops[k].name))
							{
								this.Backdrops[k].SetActive(false);
							}
						}
						this.UpdateSelectedAbility();
						TextConsole.BufferUpdated = false;
						if (this.LastViewTag != TextConsole.CurrentBuffer.ViewTag)
						{
							GameManager.MainCameraLetterbox.UpdateDelay = 0;
						}
						this.LastViewTag = TextConsole.CurrentBuffer.ViewTag;
						for (int l = 0; l < 82; l++)
						{
							for (int m = 0; m < 27; m++)
							{
								if (l == 0 || l == 81 || m == 0 || m == 26)
								{
									this.OverlayCharacter[l, m].color = new Color(0.05f, 0.2f, 0.2f);
									this.OverlayCharacter[l, m].detailcolor = new Color(0.05f, 0.2f, 0.2f);
									this.OverlayCharacter[l, m].backcolor = new Color(0.05f, 0.23f, 0.23f, 1f);
								}
							}
						}
					}
				}
			}
			if (this.ShouldForceFullscreen)
			{
				this.HideStage();
			}
			else if (this.AnyViewInStackIsStage())
			{
				this.ShowStage();
			}
			else
			{
				this.HideStage();
			}
			if (this.AnyViewInStackForcefullscreen() && !CombatJuiceManager.NoPause)
			{
				GameManager.MainCameraLetterbox.tilesWide = 80f;
				GameManager.MainCameraLetterbox.tilesHigh = 25f;
				if (this.OverlayRoot.activeInHierarchy)
				{
					this.OverlayRoot.SetActive(false);
				}
				CombatJuiceManager.pause();
			}
			else
			{
				if (GameManager.OverlayRootForceoff)
				{
					if (this.OverlayRoot.activeInHierarchy)
					{
						this.OverlayRoot.SetActive(false);
					}
				}
				else if (!this.NeedsArrowBorder || (!Options.DisplayMousableZoneTransitionBorder && !TutorialManager.IsActive))
				{
					GameManager.MainCameraLetterbox.tilesWide = 80f;
					GameManager.MainCameraLetterbox.tilesHigh = 25f;
					if (this.OverlayRoot.activeInHierarchy)
					{
						this.OverlayRoot.SetActive(false);
					}
				}
				else
				{
					GameManager.MainCameraLetterbox.tilesWide = 82f;
					GameManager.MainCameraLetterbox.tilesHigh = 27f;
					if (!this.OverlayRoot.activeInHierarchy)
					{
						this.OverlayRoot.SetActive(true);
					}
				}
				CombatJuiceManager.update();
			}
			PlatformManager.Update();
			AchievementManager.Update();
			if (GameManager.MainCamera != null && GameManager.MainCameraLetterbox.CurrentPosition != this.lastCameraPosition)
			{
				this.lastCameraPosition = GameManager.MainCameraLetterbox.CurrentPosition;
				this.UpdatePreferredSidebarPosition();
			}
			if (GameManager.Instance.DockMovable == 1 && MessageLogWindow.Shown)
			{
				GameManager.MainCameraLetterbox.TileAreaAlignment = LetterboxCamera.TileAreaAlignmentType.Left;
			}
			else if (GameManager.Instance.DockMovable == 2 && MessageLogWindow.Shown)
			{
				GameManager.MainCameraLetterbox.TileAreaAlignment = LetterboxCamera.TileAreaAlignmentType.Right;
			}
			else
			{
				GameManager.MainCameraLetterbox.TileAreaAlignment = LetterboxCamera.TileAreaAlignmentType.Center;
			}
			LetterboxCamera mainCameraLetterbox = GameManager.MainCameraLetterbox;
			if (mainCameraLetterbox == null)
			{
				return;
			}
			mainCameraLetterbox.OnUpdate();
			return;
		}
	}

	// Token: 0x0600033E RID: 830 RVA: 0x000162A8 File Offset: 0x000144A8
	public bool StringBuilderContentsEquals(StringBuilder sb1, StringBuilder sb2, int maxcheck = 800)
	{
		if (sb1.Length != sb2.Length)
		{
			return false;
		}
		int num = 0;
		while (num < sb1.Length && num < maxcheck)
		{
			if (sb1[num] != sb2[num])
			{
				return false;
			}
			num++;
		}
		return true;
	}

	// Token: 0x17000059 RID: 89
	// (get) Token: 0x0600033F RID: 831 RVA: 0x000162ED File Offset: 0x000144ED
	public static Camera MapCamera
	{
		get
		{
			return GameManager.MainCamera.GetComponent<Camera>();
		}
	}

	// Token: 0x06000340 RID: 832 RVA: 0x000162FC File Offset: 0x000144FC
	public static void Restart()
	{
		if (!GameManager.IsOnUIContext())
		{
			GameManager.Instance.uiQueue.queueTask(new Action(GameManager.Restart), 0);
			return;
		}
		using (Loading.StartTask("Restarting application...", null, true))
		{
			string[] commandLineArgs = Environment.GetCommandLineArgs();
			Utf16ValueStringBuilder utf16ValueStringBuilder = ZString.CreateStringBuilder();
			for (int i = 1; i < commandLineArgs.Length - 1; i++)
			{
				utf16ValueStringBuilder.Append('"');
				utf16ValueStringBuilder.Append(commandLineArgs[i]);
				utf16ValueStringBuilder.Append("\" ");
			}
			if (commandLineArgs.Length > 1)
			{
				utf16ValueStringBuilder.Append('"');
				utf16ValueStringBuilder.Append(commandLineArgs[commandLineArgs.Length - 1]);
				utf16ValueStringBuilder.Append('"');
			}
			ProcessStartInfo startInfo = Process.GetCurrentProcess().StartInfo;
			startInfo.FileName = commandLineArgs[0];
			if (utf16ValueStringBuilder.Length > 0)
			{
				startInfo.Arguments = utf16ValueStringBuilder.ToString();
			}
			Process.Start(startInfo);
			GameManager.Instance.Quit();
		}
	}

	// Token: 0x06000343 RID: 835 RVA: 0x000166B8 File Offset: 0x000148B8
	[CompilerGenerated]
	internal static void <UpdateInput>g__DoNavigate|230_0(string command, string pressCommand, string repeatCommand)
	{
		int num = ControlManager.isCommandDownValue(command, true, false, false);
		if (num == 1)
		{
			Keyboard.PushCommand(pressCommand, null);
			return;
		}
		if (num == 2)
		{
			Keyboard.PushCommand(repeatCommand, null);
		}
	}

	// Token: 0x0400021B RID: 539
	public static bool _focused = true;

	// Token: 0x0400021C RID: 540
	public static float mouseDisable = 0f;

	// Token: 0x0400021D RID: 541
	public static global::UnityEngine.GameObject MainCamera;

	// Token: 0x0400021E RID: 542
	public static Camera cameraMainCamera;

	// Token: 0x0400021F RID: 543
	public static LetterboxCamera MainCameraLetterbox;

	// Token: 0x04000220 RID: 544
	public static Stopwatch Time = new Stopwatch();

	// Token: 0x04000221 RID: 545
	public static bool Playing;

	// Token: 0x04000222 RID: 546
	public global::UnityEngine.GameObject AudioListener;

	// Token: 0x04000223 RID: 547
	public static bool OverlayRootForceoff = false;

	// Token: 0x04000224 RID: 548
	public global::UnityEngine.GameObject Backdrop;

	// Token: 0x04000225 RID: 549
	public global::UnityEngine.GameObject TileRoot;

	// Token: 0x04000226 RID: 550
	public global::UnityEngine.GameObject OverlayRoot;

	// Token: 0x04000227 RID: 551
	public static GameManager Instance;

	// Token: 0x04000228 RID: 552
	public global::UnityEngine.GameObject Minimap;

	// Token: 0x04000229 RID: 553
	public static ManualResetEvent focusedEvent = new ManualResetEvent(true);

	// Token: 0x0400022A RID: 554
	public ThreadTaskQueue uiQueue = new ThreadTaskQueue();

	// Token: 0x0400022B RID: 555
	public ThreadTaskQueue gameQueue = new ThreadTaskQueue();

	// Token: 0x0400022C RID: 556
	public static bool bCapInputBuffer = false;

	// Token: 0x0400022D RID: 557
	public int tileWidth = 16;

	// Token: 0x0400022E RID: 558
	public int tileHeight = 24;

	// Token: 0x0400022F RID: 559
	private Point2D CurrentPlayerCell = new Point2D(-1, -1);

	// Token: 0x04000230 RID: 560
	public bool pauseUpdates;

	// Token: 0x04000231 RID: 561
	public static List<Action<GameManager.PreferredSidebarPosition>> OnPreferredSidebarPositionUpdatedCallbacks = new List<Action<GameManager.PreferredSidebarPosition>>();

	// Token: 0x04000232 RID: 562
	public GameManager.PreferredSidebarPosition currentSidebarPosition;

	// Token: 0x04000233 RID: 563
	public Dictionary<string, global::UnityEngine.GameObject> findCache = new Dictionary<string, global::UnityEngine.GameObject>();

	// Token: 0x04000234 RID: 564
	public UIManager uiManager;

	// Token: 0x04000235 RID: 565
	public static List<Resolution> resolutions;

	// Token: 0x04000236 RID: 566
	public SynchronizationContext uiSynchronizationContext;

	// Token: 0x04000237 RID: 567
	public SynchronizationContext gameThreadSynchronizationContext;

	// Token: 0x04000238 RID: 568
	public PlayerInputManager player;

	// Token: 0x04000239 RID: 569
	private PlatformService platformService;

	// Token: 0x0400023A RID: 570
	private IPlatform platform;

	// Token: 0x0400023B RID: 571
	public global::UnityEngine.GameObject serviceLocatorPrefabForStandalone;

	// Token: 0x0400023C RID: 572
	public UITextSkin minimapLabel;

	// Token: 0x0400023D RID: 573
	public Texture2D minimapTexture;

	// Token: 0x0400023E RID: 574
	public static Color32[] minimapColors = new Color32[4000];

	// Token: 0x0400023F RID: 575
	public bool _bAlt;

	// Token: 0x04000240 RID: 576
	private bool _destroyed;

	// Token: 0x04000241 RID: 577
	public exTextureInfo[] CharInfos = new exTextureInfo[256];

	// Token: 0x04000242 RID: 578
	public ex3DSprite2[,] ConsoleCharacter = new ex3DSprite2[80, 25];

	// Token: 0x04000243 RID: 579
	public Transform[,] ConsoleCharacterTransform = new Transform[80, 25];

	// Token: 0x04000244 RID: 580
	public ex3DSprite2[,] OverlayCharacter = new ex3DSprite2[82, 27];

	// Token: 0x04000245 RID: 581
	public int[,] CurrentShadermode = new int[80, 25];

	// Token: 0x04000246 RID: 582
	public string[,] CurrentTile = new string[80, 25];

	// Token: 0x04000247 RID: 583
	public bool MouseInput;

	// Token: 0x04000248 RID: 584
	private Dictionary<string, GameManager.ViewInfo> _ViewData = new Dictionary<string, GameManager.ViewInfo>
	{
		{
			"*Default",
			new GameManager.ViewInfo(false, true, "Menu", null, 0, false, false, 0, false, false)
		},
		{
			"ModernPopup*",
			new GameManager.ViewInfo(false, false, "Menu", null, 0, false, false, 0, false, false)
		}
	};

	// Token: 0x04000249 RID: 585
	public Point2D LastTileOver = new Point2D(-1, -1);

	// Token: 0x0400024A RID: 586
	public global::UnityEngine.GameObject consoleTileAreaHandler;

	// Token: 0x0400024B RID: 587
	public float TargetZoomFactor = 1f;

	// Token: 0x0400024C RID: 588
	public Vector3 TargetCameraLocation = new Vector3(0f, 0f, -10f);

	// Token: 0x0400024D RID: 589
	private bool _overlayOptionsUpdated;

	// Token: 0x0400024E RID: 590
	public bool ModernUI;

	// Token: 0x0400024F RID: 591
	public double StageScale = 1.0;

	// Token: 0x04000250 RID: 592
	private float TargetIntensity;

	// Token: 0x04000251 RID: 593
	private CC_AnalogTV TV;

	// Token: 0x04000252 RID: 594
	public string _ActiveGameView;

	// Token: 0x04000253 RID: 595
	public string __CurrentGameView = "MainMenu";

	// Token: 0x04000254 RID: 596
	private RingDeque<string> GameViewStack = new RingDeque<string>();

	// Token: 0x04000255 RID: 597
	private List<GameManager.ClickRegion> Regions = new List<GameManager.ClickRegion>();

	// Token: 0x04000256 RID: 598
	private int nRegions;

	// Token: 0x04000257 RID: 599
	public bool bViewUpdated;

	// Token: 0x04000258 RID: 600
	public TooltipTrigger generalTooltip;

	// Token: 0x04000259 RID: 601
	public TooltipTrigger lookerTooltip;

	// Token: 0x0400025A RID: 602
	public TooltipTrigger tileTooltip;

	// Token: 0x0400025B RID: 603
	public TooltipTrigger compareLookerTooltip;

	// Token: 0x0400025C RID: 604
	public global::Vector2i lastHover = new global::Vector2i(-1, -1);

	// Token: 0x0400025D RID: 605
	public List<string> LeftGameViews = new List<string>();

	// Token: 0x0400025E RID: 606
	public string LastGameView = "";

	// Token: 0x0400025F RID: 607
	public GameManager.ViewInfo currentUIView;

	// Token: 0x04000260 RID: 608
	private List<string> HiddenNongameViews = new List<string>();

	// Token: 0x04000261 RID: 609
	public static bool capslock = false;

	// Token: 0x04000262 RID: 610
	public static int bDraw = 0;

	// Token: 0x04000263 RID: 611
	public static bool AwakeComplete = false;

	// Token: 0x04000264 RID: 612
	public bool bInitComplete;

	// Token: 0x04000265 RID: 613
	public bool bInitStarted;

	// Token: 0x04000266 RID: 614
	public bool bEditorMode;

	// Token: 0x04000267 RID: 615
	public bool bEditorInit;

	// Token: 0x04000268 RID: 616
	public bool FadeSplash;

	// Token: 0x04000269 RID: 617
	public global::UnityEngine.GameObject SplashCanvas;

	// Token: 0x0400026A RID: 618
	public float minimapScale = 1f;

	// Token: 0x0400026B RID: 619
	public float nearbyObjectsListScale = 1f;

	// Token: 0x0400026C RID: 620
	public float compassScale = 1f;

	// Token: 0x0400026D RID: 621
	public static bool dirtyFocused = false;

	// Token: 0x0400026E RID: 622
	private bool bFirstUpdate = true;

	// Token: 0x0400026F RID: 623
	public bool DisplayMinimap;

	// Token: 0x04000270 RID: 624
	public int LastWidth;

	// Token: 0x04000271 RID: 625
	public int LastHeight;

	// Token: 0x04000272 RID: 626
	private int _TileScale;

	// Token: 0x04000273 RID: 627
	private int _DockMovable;

	// Token: 0x04000274 RID: 628
	public bool Hallucinating;

	// Token: 0x04000275 RID: 629
	public float _Spin;

	// Token: 0x04000276 RID: 630
	public float _CurrentSpin;

	// Token: 0x04000277 RID: 631
	public float MAX_THIN_WORLD_DISTORTING = 3f;

	// Token: 0x04000278 RID: 632
	public float _ThinWorldDistoring;

	// Token: 0x04000279 RID: 633
	private int _StaticEffecting;

	// Token: 0x0400027A RID: 634
	private float _StaticEffectTime;

	// Token: 0x0400027B RID: 635
	private float _NextStaticEffectGlitch;

	// Token: 0x0400027C RID: 636
	public bool Greyscaling;

	// Token: 0x0400027D RID: 637
	public int GreyscaleLevel;

	// Token: 0x0400027E RID: 638
	public float _spacefoldingT;

	// Token: 0x0400027F RID: 639
	public bool _spacefolding;

	// Token: 0x04000280 RID: 640
	public float _fuzzingT;

	// Token: 0x04000281 RID: 641
	public bool _fuzzing;

	// Token: 0x04000282 RID: 642
	public RectTransform stageRect;

	// Token: 0x04000283 RID: 643
	public StageDock stageDock;

	// Token: 0x04000284 RID: 644
	public bool ShouldForceFullscreen;

	// Token: 0x04000285 RID: 645
	public int _selectedAbility = -1;

	// Token: 0x04000286 RID: 646
	public TextMeshProUGUI selectedAbilityText;

	// Token: 0x04000287 RID: 647
	public ActivatedAbilityEntry currentlySelectedAbility;

	// Token: 0x04000288 RID: 648
	public global::UnityEngine.GameObject playerTrackerPrefab;

	// Token: 0x04000289 RID: 649
	public PlayerTracker playerTracker;

	// Token: 0x0400028A RID: 650
	public MonoBehaviour InputManagerModuleNew;

	// Token: 0x0400028B RID: 651
	public MonoBehaviour InputManagerModuleOld;

	// Token: 0x0400028C RID: 652
	public string currentNavCategory;

	// Token: 0x0400028D RID: 653
	public string currentNavDirection;

	// Token: 0x0400028E RID: 654
	public StringBuilder selectedAbilitybuilder = new StringBuilder();

	// Token: 0x0400028F RID: 655
	public string DebugNavLayers;

	// Token: 0x04000290 RID: 656
	public string DebugNavCategory;

	// Token: 0x04000291 RID: 657
	private static int TakeAStepFilterMode = 0;

	// Token: 0x04000292 RID: 658
	public float lookDelay;

	// Token: 0x04000293 RID: 659
	public float lookRepeat;

	// Token: 0x04000294 RID: 660
	public global::UnityEngine.UI.Text currentNavDirectionDisplay;

	// Token: 0x04000295 RID: 661
	public static bool _runPlayerTurnOnUIThread = false;

	// Token: 0x04000296 RID: 662
	private ScreenBuffer ClearBuffer;

	// Token: 0x04000297 RID: 663
	public bool _fadingToBlack;

	// Token: 0x04000298 RID: 664
	public int fadeToBlackStage;

	// Token: 0x04000299 RID: 665
	public float originalBrightness = float.MinValue;

	// Token: 0x0400029A RID: 666
	public float fadingToBlackTimer;

	// Token: 0x0400029B RID: 667
	public static int runWholeTurnOnUIThread = 0;

	// Token: 0x0400029C RID: 668
	public Vector3 lastCameraPosition = Vector3.zero;

	// Token: 0x0400029D RID: 669
	private bool lastFullscreen = true;

	// Token: 0x0400029E RID: 670
	public static int UpdateCountFromChangedToActiveView = 0;

	// Token: 0x0400029F RID: 671
	public static Color backdropBlackColor = new Color(0.050980393f, 0.18359375f, 0.1796875f, 1f);

	// Token: 0x040002A0 RID: 672
	public static ulong frame = 0UL;

	// Token: 0x040002A1 RID: 673
	public string LastViewTag;

	// Token: 0x040002A2 RID: 674
	private List<global::UnityEngine.GameObject> Backdrops = new List<global::UnityEngine.GameObject>();

	// Token: 0x040002A3 RID: 675
	private Dictionary<string, global::UnityEngine.GameObject> BackdropsByName = new Dictionary<string, global::UnityEngine.GameObject>();

	// Token: 0x040002A4 RID: 676
	private HashSet<string> RequestedBackdrops = new HashSet<string>();

	// Token: 0x02001572 RID: 5490
	public enum PreferredSidebarPosition
	{
		// Token: 0x04005650 RID: 22096
		None,
		// Token: 0x04005651 RID: 22097
		Left,
		// Token: 0x04005652 RID: 22098
		Right
	}

	// Token: 0x02001573 RID: 5491
	public class ViewInfo
	{
		// Token: 0x0600D537 RID: 54583 RVA: 0x004F3448 File Offset: 0x004F1648
		public ViewInfo(bool WantsTileOver, bool ForceFullscreen, string NavCategory = null, string UICanvas = null, int OverlayMode = 0, bool ExecuteActions = false, bool TakesScroll = false, int UICanvasHost = 0, bool IgnoreForceFullscreen = false, bool ForceFullscreenInLegacy = false)
		{
			this.WantsTileOver = WantsTileOver;
			this.ForceFullscreen = ForceFullscreen;
			this.ForceFullscreenInLegacy = ForceFullscreenInLegacy;
			this.KeepCurrentFullscreen = IgnoreForceFullscreen;
			this.UICanvas = UICanvas;
			this.UICanvasHost = UICanvasHost;
			this.OverlayMode = OverlayMode;
			this.ExecuteActions = ExecuteActions;
			this.NavCategory = NavCategory;
			this.TakesScroll = TakesScroll;
		}

		// Token: 0x0600D538 RID: 54584 RVA: 0x004F34A8 File Offset: 0x004F16A8
		public bool IsSame(GameManager.ViewInfo other)
		{
			return this.WantsTileOver == other.WantsTileOver && this.ForceFullscreen == other.ForceFullscreen && this.ForceFullscreenInLegacy == other.ForceFullscreenInLegacy && this.UICanvas == other.UICanvas && this.UICanvasHost == other.UICanvasHost && this.OverlayMode == other.OverlayMode && this.ExecuteActions == other.ExecuteActions && this.NavCategory == other.NavCategory && this.TakesScroll == other.TakesScroll;
		}

		// Token: 0x04005653 RID: 22099
		public string NavCategory;

		// Token: 0x04005654 RID: 22100
		public string UICanvas;

		// Token: 0x04005655 RID: 22101
		public int UICanvasHost;

		// Token: 0x04005656 RID: 22102
		public bool WantsTileOver;

		// Token: 0x04005657 RID: 22103
		public bool ForceFullscreen;

		// Token: 0x04005658 RID: 22104
		public bool ForceFullscreenInLegacy;

		// Token: 0x04005659 RID: 22105
		public bool KeepCurrentFullscreen;

		// Token: 0x0400565A RID: 22106
		public bool ExecuteActions;

		// Token: 0x0400565B RID: 22107
		public int OverlayMode;

		// Token: 0x0400565C RID: 22108
		public bool TakesScroll;
	}

	// Token: 0x02001574 RID: 5492
	public class ClickRegion
	{
		// Token: 0x0600D539 RID: 54585 RVA: 0x004F353F File Offset: 0x004F173F
		public bool Contains(int x, int y)
		{
			return x >= this.x1 && x <= this.x2 && y >= this.y1 && y <= this.y2;
		}

		// Token: 0x0400565D RID: 22109
		public int x1;

		// Token: 0x0400565E RID: 22110
		public int y1;

		// Token: 0x0400565F RID: 22111
		public int x2;

		// Token: 0x04005660 RID: 22112
		public int y2;

		// Token: 0x04005661 RID: 22113
		public string OverCommand;

		// Token: 0x04005662 RID: 22114
		public string LeftClickCommand;

		// Token: 0x04005663 RID: 22115
		public string RightClickCommand;
	}
}
