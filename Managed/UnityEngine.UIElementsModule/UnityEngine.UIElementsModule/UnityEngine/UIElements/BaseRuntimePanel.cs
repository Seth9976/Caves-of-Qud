using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.Pool;

namespace UnityEngine.UIElements
{
	// Token: 0x0200030F RID: 783
	internal abstract class BaseRuntimePanel : Panel
	{
		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06001681 RID: 5761 RVA: 0x000573F5 File Offset: 0x000555F5
		// (set) Token: 0x06001682 RID: 5762 RVA: 0x00057400 File Offset: 0x00055600
		public GameObject selectableGameObject
		{
			get
			{
				return this.m_SelectableGameObject;
			}
			set
			{
				bool flag = this.m_SelectableGameObject != value;
				if (flag)
				{
					this.AssignPanelToComponents(null);
					this.m_SelectableGameObject = value;
					this.AssignPanelToComponents(this);
				}
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06001683 RID: 5763 RVA: 0x00057437 File Offset: 0x00055637
		// (set) Token: 0x06001684 RID: 5764 RVA: 0x00057440 File Offset: 0x00055640
		public float sortingPriority
		{
			get
			{
				return this.m_SortingPriority;
			}
			set
			{
				bool flag = !Mathf.Approximately(this.m_SortingPriority, value);
				if (flag)
				{
					this.m_SortingPriority = value;
					bool flag2 = this.contextType == ContextType.Player;
					if (flag2)
					{
						UIElementsRuntimeUtility.SetPanelOrderingDirty();
					}
				}
			}
		}

		// Token: 0x14000049 RID: 73
		// (add) Token: 0x06001685 RID: 5765 RVA: 0x00057480 File Offset: 0x00055680
		// (remove) Token: 0x06001686 RID: 5766 RVA: 0x000574B8 File Offset: 0x000556B8
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action destroyed;

		// Token: 0x06001687 RID: 5767 RVA: 0x000574F0 File Offset: 0x000556F0
		protected BaseRuntimePanel(ScriptableObject ownerObject, EventDispatcher dispatcher = null)
			: base(ownerObject, ContextType.Player, dispatcher)
		{
			this.m_RuntimePanelCreationIndex = BaseRuntimePanel.s_CurrentRuntimePanelCounter++;
		}

		// Token: 0x06001688 RID: 5768 RVA: 0x00057554 File Offset: 0x00055754
		protected override void Dispose(bool disposing)
		{
			bool disposed = base.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					Action action = this.destroyed;
					if (action != null)
					{
						action();
					}
				}
				base.Dispose(disposing);
			}
		}

		// Token: 0x1400004A RID: 74
		// (add) Token: 0x06001689 RID: 5769 RVA: 0x00057590 File Offset: 0x00055790
		// (remove) Token: 0x0600168A RID: 5770 RVA: 0x000575C8 File Offset: 0x000557C8
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal event Action drawsInCamerasChanged;

		// Token: 0x0600168B RID: 5771 RVA: 0x000575FD File Offset: 0x000557FD
		private void InvokeDrawsInCamerasChanged()
		{
			Action action = this.drawsInCamerasChanged;
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x0600168C RID: 5772 RVA: 0x00057614 File Offset: 0x00055814
		// (set) Token: 0x0600168D RID: 5773 RVA: 0x0005762C File Offset: 0x0005582C
		internal bool drawsInCameras
		{
			get
			{
				return this.m_DrawsInCameras;
			}
			set
			{
				bool flag = this.m_DrawsInCameras != value;
				if (flag)
				{
					this.m_DrawsInCameras = value;
					this.InvokeDrawsInCamerasChanged();
				}
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x0600168E RID: 5774 RVA: 0x0005765C File Offset: 0x0005585C
		// (set) Token: 0x0600168F RID: 5775 RVA: 0x00057674 File Offset: 0x00055874
		internal float pixelsPerUnit
		{
			get
			{
				return this.m_PixelsPerUnit;
			}
			set
			{
				this.m_PixelsPerUnit = value;
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06001690 RID: 5776 RVA: 0x0005767E File Offset: 0x0005587E
		// (set) Token: 0x06001691 RID: 5777 RVA: 0x00057686 File Offset: 0x00055886
		internal int targetDisplay { get; set; }

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06001692 RID: 5778 RVA: 0x0005768F File Offset: 0x0005588F
		internal int screenRenderingWidth
		{
			get
			{
				return BaseRuntimePanel.getScreenRenderingWidth(this.targetDisplay);
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06001693 RID: 5779 RVA: 0x0005769C File Offset: 0x0005589C
		internal int screenRenderingHeight
		{
			get
			{
				return BaseRuntimePanel.getScreenRenderingHeight(this.targetDisplay);
			}
		}

		// Token: 0x06001694 RID: 5780 RVA: 0x000576A9 File Offset: 0x000558A9
		internal virtual void Update()
		{
			this.scheduler.UpdateScheduledEvents();
			this.ValidateFocus();
			this.ValidateLayout();
			this.UpdateAnimations();
			this.UpdateBindings();
		}

		// Token: 0x06001695 RID: 5781 RVA: 0x000576D4 File Offset: 0x000558D4
		internal static int getScreenRenderingHeight(int display)
		{
			return (display >= 0 && display < Display.displays.Length) ? Display.displays[display].renderingHeight : Screen.height;
		}

		// Token: 0x06001696 RID: 5782 RVA: 0x00057708 File Offset: 0x00055908
		internal static int getScreenRenderingWidth(int display)
		{
			return (display >= 0 && display < Display.displays.Length) ? Display.displays[display].renderingWidth : Screen.width;
		}

		// Token: 0x06001697 RID: 5783 RVA: 0x0005773C File Offset: 0x0005593C
		public override void Render()
		{
			bool drawsInCameras = this.drawsInCameras;
			if (drawsInCameras)
			{
				Debug.LogError("Panel.Render() must not be called on a panel that draws in cameras.");
			}
			else
			{
				bool flag = this.targetTexture == null;
				if (flag)
				{
					RenderTexture active = RenderTexture.active;
					int num = ((active != null) ? active.width : this.screenRenderingWidth);
					int num2 = ((active != null) ? active.height : this.screenRenderingHeight);
					GL.Viewport(new Rect(0f, 0f, (float)num, (float)num2));
					base.Render();
				}
				else
				{
					Camera current = Camera.current;
					RenderTexture active2 = RenderTexture.active;
					Camera.SetupCurrent(null);
					RenderTexture.active = this.targetTexture;
					GL.Viewport(new Rect(0f, 0f, (float)this.targetTexture.width, (float)this.targetTexture.height));
					base.Render();
					Camera.SetupCurrent(current);
					RenderTexture.active = active2;
				}
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06001698 RID: 5784 RVA: 0x00057838 File Offset: 0x00055A38
		// (set) Token: 0x06001699 RID: 5785 RVA: 0x00057840 File Offset: 0x00055A40
		public Func<Vector2, Vector2> screenToPanelSpace
		{
			get
			{
				return this.m_ScreenToPanelSpace;
			}
			set
			{
				this.m_ScreenToPanelSpace = value ?? BaseRuntimePanel.DefaultScreenToPanelSpace;
			}
		}

		// Token: 0x0600169A RID: 5786 RVA: 0x00057854 File Offset: 0x00055A54
		internal Vector2 ScreenToPanel(Vector2 screen)
		{
			return this.screenToPanelSpace(screen) / base.scale;
		}

		// Token: 0x0600169B RID: 5787 RVA: 0x00057880 File Offset: 0x00055A80
		internal bool ScreenToPanel(Vector2 screenPosition, Vector2 screenDelta, out Vector2 panelPosition, out Vector2 panelDelta, bool allowOutside = false)
		{
			panelPosition = this.ScreenToPanel(screenPosition);
			bool flag = !allowOutside;
			Vector2 vector;
			if (flag)
			{
				Rect layout = this.visualTree.layout;
				bool flag2 = !layout.Contains(panelPosition);
				if (flag2)
				{
					panelDelta = screenDelta;
					return false;
				}
				vector = this.ScreenToPanel(screenPosition - screenDelta);
				bool flag3 = !layout.Contains(vector);
				if (flag3)
				{
					panelDelta = screenDelta;
					return true;
				}
			}
			else
			{
				vector = this.ScreenToPanel(screenPosition - screenDelta);
			}
			panelDelta = panelPosition - vector;
			return true;
		}

		// Token: 0x0600169C RID: 5788 RVA: 0x00057930 File Offset: 0x00055B30
		private void AssignPanelToComponents(BaseRuntimePanel panel)
		{
			bool flag = this.selectableGameObject == null;
			if (!flag)
			{
				List<IRuntimePanelComponent> list;
				using (CollectionPool<List<IRuntimePanelComponent>, IRuntimePanelComponent>.Get(out list))
				{
					this.selectableGameObject.GetComponents<IRuntimePanelComponent>(list);
					foreach (IRuntimePanelComponent runtimePanelComponent in list)
					{
						runtimePanelComponent.panel = panel;
					}
				}
			}
		}

		// Token: 0x0600169D RID: 5789 RVA: 0x000579CC File Offset: 0x00055BCC
		internal void PointerLeavesPanel(int pointerId, Vector2 position)
		{
			base.ClearCachedElementUnderPointer(pointerId, null);
			base.CommitElementUnderPointers();
			PointerDeviceState.SavePointerPosition(pointerId, position, null, this.contextType);
		}

		// Token: 0x0600169E RID: 5790 RVA: 0x000579EE File Offset: 0x00055BEE
		internal void PointerEntersPanel(int pointerId, Vector2 position)
		{
			PointerDeviceState.SavePointerPosition(pointerId, position, this, this.contextType);
		}

		// Token: 0x04000C56 RID: 3158
		private GameObject m_SelectableGameObject;

		// Token: 0x04000C57 RID: 3159
		private static int s_CurrentRuntimePanelCounter = 0;

		// Token: 0x04000C58 RID: 3160
		internal readonly int m_RuntimePanelCreationIndex;

		// Token: 0x04000C59 RID: 3161
		private float m_SortingPriority = 0f;

		// Token: 0x04000C5A RID: 3162
		internal int resolvedSortingIndex = 0;

		// Token: 0x04000C5D RID: 3165
		private bool m_DrawsInCameras;

		// Token: 0x04000C5E RID: 3166
		private float m_PixelsPerUnit = 100f;

		// Token: 0x04000C5F RID: 3167
		internal RenderTexture targetTexture = null;

		// Token: 0x04000C60 RID: 3168
		internal int worldSpaceLayer = 0;

		// Token: 0x04000C62 RID: 3170
		internal static readonly Func<Vector2, Vector2> DefaultScreenToPanelSpace = (Vector2 p) => p;

		// Token: 0x04000C63 RID: 3171
		private Func<Vector2, Vector2> m_ScreenToPanelSpace = BaseRuntimePanel.DefaultScreenToPanelSpace;
	}
}
