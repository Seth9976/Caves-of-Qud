using System;
using System.Collections.Generic;
using System.Threading;
using ConsoleLib.Console;
using Qud.API;
using UnityEngine;
using UnityEngine.Events;
using XRL;
using XRL.Core;
using XRL.Rules;
using XRL.UI;
using XRL.UI.Framework;
using XRL.UI.ObjectFinderContexts;
using XRL.World;

namespace Qud.UI
{
	// Token: 0x0200049D RID: 1181
	[HasModSensitiveStaticCache]
	[HasGameBasedStaticCache]
	[UIView("NearbyItems", false, false, false, null, null, false, 0, false, NavCategory = "Adventure", UICanvas = "NearbyItems", UICanvasHost = 1)]
	public class NearbyItemsWindow : MovableSceneFrameWindowBase<NearbyItemsWindow>
	{
		// Token: 0x06002D49 RID: 11593 RVA: 0x000F4FA4 File Offset: 0x000F31A4
		[ModSensitiveCacheInit]
		public static void SystemInit()
		{
			XRLCore.RegisterOnBeginPlayerTurnCallback(delegate(XRLCore _)
			{
				SingletonWindowBase<NearbyItemsWindow>.instance.Dirty = true;
			});
			XRLCore.RegisterOnEndPlayerTurnCallback(delegate(XRLCore _)
			{
				SingletonWindowBase<NearbyItemsWindow>.instance.Dirty = true;
			}, true);
		}

		// Token: 0x06002D4A RID: 11594 RVA: 0x000F4FFC File Offset: 0x000F31FC
		[GameBasedCacheInit]
		public static void GameInit()
		{
			List<ObjectFinderLine.Data> objects = SingletonWindowBase<NearbyItemsWindow>.instance._objects;
			lock (objects)
			{
				SingletonWindowBase<NearbyItemsWindow>.instance.currentObjectCount = 0;
				SingletonWindowBase<NearbyItemsWindow>.instance.Dirty = true;
			}
			SingletonWindowBase<NearbyItemsWindow>.instance.FinderInitialized = false;
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06002D4B RID: 11595 RVA: 0x000F505C File Offset: 0x000F325C
		public static ObjectFinder Finder
		{
			get
			{
				return ObjectFinder.instance;
			}
		}

		// Token: 0x06002D4C RID: 11596 RVA: 0x000F5063 File Offset: 0x000F3263
		public override bool AllowPassthroughInput()
		{
			return true;
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06002D4D RID: 11597 RVA: 0x000F5066 File Offset: 0x000F3266
		public IEnumerable<ObjectFinderLine.Data> objects
		{
			get
			{
				if (this.currentObjectCount > this._objects.Count)
				{
					MetricsManager.LogError("currentObjectCount / _objects length dsync");
					yield break;
				}
				int num;
				for (int x = 0; x < this.currentObjectCount; x = num + 1)
				{
					yield return this._objects[x];
					num = x;
				}
				yield break;
			}
		}

		// Token: 0x06002D4E RID: 11598 RVA: 0x000F5076 File Offset: 0x000F3276
		public void StartupFinder()
		{
			if (!this.FinderInitialized && NearbyItemsWindow.Finder != null)
			{
				NearbyItemsWindow.Finder.LoadDefaults();
				NearbyItemsWindow.Finder.ReadOptions();
				this.FinderInitialized = true;
			}
		}

		// Token: 0x06002D4F RID: 11599 RVA: 0x000F50A2 File Offset: 0x000F32A2
		public void TogglePreferredState()
		{
			this.StartupFinder();
			base.Toggle();
			this.SaveOptions();
		}

		// Token: 0x06002D50 RID: 11600 RVA: 0x000F50B6 File Offset: 0x000F32B6
		public void SaveOptions()
		{
			Options.SetOption("OptionOverlayNearbyObjects", base.Visible);
		}

		// Token: 0x06002D51 RID: 11601 RVA: 0x000F50C8 File Offset: 0x000F32C8
		public void ShowIfEnabled()
		{
			if (Options.OverlayNearbyObjects)
			{
				this.Show();
				this.StartupFinder();
				return;
			}
			this.Hide();
			if (this.FinderInitialized)
			{
				ObjectFinder.Reset();
				this.FinderInitialized = false;
			}
		}

		// Token: 0x06002D52 RID: 11602 RVA: 0x000F50FC File Offset: 0x000F32FC
		public override void Update()
		{
			if (!this.KeyboardMode && !ControlManager.LastInputFromMouse && this.scrollController.scrollContext.IsActive(true))
			{
				NavigationController.instance.activeContext = null;
			}
			try
			{
				if (Options.OverlayNearbyObjects && Monitor.TryEnter(this._objects))
				{
					try
					{
						if (this._objects != null)
						{
							if (this.currentObjectCount > this._objects.Count)
							{
								MetricsManager.LogError("currentObjectCount / _objects length dsync");
								return;
							}
							for (int i = 0; i < this.currentObjectCount; i++)
							{
								this.UpdateObject(this._objects[i]);
							}
						}
						if (this.Dirty)
						{
							this.scrollController.BeforeShow(null, this.objects);
							this.scrollController.onHighlight.RemoveListener(new UnityAction<FrameworkDataElement>(this.OnHighlight));
							this.scrollController.onHighlight.AddListener(new UnityAction<FrameworkDataElement>(this.OnHighlight));
							this.scrollController.onSelected.RemoveListener(new UnityAction<FrameworkDataElement>(this.OnSelect));
							this.scrollController.onSelected.AddListener(new UnityAction<FrameworkDataElement>(this.OnSelect));
							this.Dirty = false;
						}
					}
					finally
					{
						Monitor.Exit(this._objects);
					}
				}
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
			}
			base.Update();
		}

		// Token: 0x06002D53 RID: 11603 RVA: 0x000F5284 File Offset: 0x000F3484
		public void UpdateObject(ObjectFinderLine.Data data)
		{
			if (data.context is NearbyItems)
			{
				Cell playerCell = The.PlayerCell;
				string text;
				if (playerCell == null)
				{
					text = null;
				}
				else
				{
					global::XRL.World.GameObject go = data.go;
					text = playerCell.GetDirectionFromCell((go != null) ? go.CurrentCell : null, false);
				}
				string text2 = Directions.GetUITextArrowForDirection(text);
				if (Options.OverlayNearbyObjectsLocal)
				{
					text2 = "";
				}
				if (text2 != data.PrefixText)
				{
					data.PrefixText = text2;
					this.Dirty = true;
				}
			}
		}

		// Token: 0x06002D54 RID: 11604 RVA: 0x000F52F4 File Offset: 0x000F34F4
		public void OnHighlight(FrameworkDataElement e)
		{
			ObjectFinderLine.Data data = e as ObjectFinderLine.Data;
			if (data != null && !this.KeyboardMode)
			{
				this.ShowTooltip(data);
			}
		}

		// Token: 0x06002D55 RID: 11605 RVA: 0x000F531C File Offset: 0x000F351C
		public void OnSelect(FrameworkDataElement e)
		{
			ObjectFinderLine.Data data = e as ObjectFinderLine.Data;
			if (data != null)
			{
				GameManager.Instance.gameQueue.queueSingletonTask("nearby items twiddle", delegate
				{
					EquipmentAPI.TwiddleObject(data.go, null, false, false);
				}, false);
			}
		}

		// Token: 0x06002D56 RID: 11606 RVA: 0x000F5365 File Offset: 0x000F3565
		public void ShowTooltip(ObjectFinderLine.Data data)
		{
		}

		// Token: 0x06002D57 RID: 11607 RVA: 0x000F5368 File Offset: 0x000F3568
		public void UpdateGameContext()
		{
			List<ObjectFinderLine.Data> objects = this._objects;
			lock (objects)
			{
				if (NearbyItemsWindow.Finder != null && NearbyItemsWindow.Finder.UpdateFilter(false))
				{
					this.currentObjectCount = 0;
					foreach (ValueTuple<global::XRL.World.GameObject, ObjectFinder.Context> valueTuple in NearbyItemsWindow.Finder.readItems(false))
					{
						ObjectFinderLine.Data data;
						if (this.currentObjectCount < this._objects.Count)
						{
							data = this._objects[this.currentObjectCount];
						}
						else
						{
							data = new ObjectFinderLine.Data();
							this._objects.Add(data);
						}
						this.currentObjectCount++;
						ObjectFinderLine.Data data2 = data;
						if (data2.Icon == null)
						{
							data2.Icon = new Renderable();
						}
						data.Icon.Copy(valueTuple.Item1.RenderForUI(null, false));
						data.go = valueTuple.Item1;
						data.context = valueTuple.Item2;
						data.Description = valueTuple.Item1.DisplayName;
						if (valueTuple.Item1.IsTakeable())
						{
							data.RightText = Markup.Color("K", valueTuple.Item1.Weight.ToString() + "lbs.");
						}
						else
						{
							data.RightText = null;
						}
						if (data.context is AutogotItems)
						{
							data.PrefixText = "{{K|<autogot>}}";
						}
						else if (data.context is NearbyItems)
						{
							data.PrefixText = Directions.GetUITextArrowForDirection(The.PlayerCell.GetDirectionFromCell(valueTuple.Item1.CurrentCell, false));
						}
					}
					this.Dirty = true;
				}
			}
		}

		// Token: 0x04001964 RID: 6500
		public bool Dirty;

		// Token: 0x04001965 RID: 6501
		public bool KeyboardMode;

		// Token: 0x04001966 RID: 6502
		public float hoverTimer;

		// Token: 0x04001967 RID: 6503
		private bool FinderInitialized;

		// Token: 0x04001968 RID: 6504
		public FrameworkScroller scrollController;

		// Token: 0x04001969 RID: 6505
		private List<ObjectFinderLine.Data> _objects = new List<ObjectFinderLine.Data>();

		// Token: 0x0400196A RID: 6506
		public int currentObjectCount;
	}
}
