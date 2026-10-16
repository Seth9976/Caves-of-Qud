using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.Bindings;
using UnityEngine.UIElements.Layout;

namespace UnityEngine.UIElements
{
	// Token: 0x02000308 RID: 776
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal abstract class BaseVisualElementPanel : IPanel, IDisposable, IGroupBox
	{
		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x060015C9 RID: 5577
		// (set) Token: 0x060015CA RID: 5578
		public abstract EventInterests IMGUIEventInterests { get; set; }

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x060015CB RID: 5579
		// (set) Token: 0x060015CC RID: 5580
		public abstract ScriptableObject ownerObject { get; protected set; }

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x060015CD RID: 5581
		// (set) Token: 0x060015CE RID: 5582
		public abstract SavePersistentViewData saveViewData { get; set; }

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x060015CF RID: 5583
		// (set) Token: 0x060015D0 RID: 5584
		public abstract GetViewDataDictionary getViewDataDictionary { get; set; }

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x060015D1 RID: 5585
		// (set) Token: 0x060015D2 RID: 5586
		public abstract int IMGUIContainersCount { get; set; }

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x060015D3 RID: 5587
		// (set) Token: 0x060015D4 RID: 5588
		public abstract FocusController focusController { get; set; }

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x060015D5 RID: 5589
		// (set) Token: 0x060015D6 RID: 5590
		public abstract IMGUIContainer rootIMGUIContainer { get; set; }

		// Token: 0x14000043 RID: 67
		// (add) Token: 0x060015D7 RID: 5591 RVA: 0x00055FDC File Offset: 0x000541DC
		// (remove) Token: 0x060015D8 RID: 5592 RVA: 0x00056014 File Offset: 0x00054214
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal event Action<BaseVisualElementPanel> panelDisposed;

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x060015D9 RID: 5593 RVA: 0x0005604C File Offset: 0x0005424C
		// (set) Token: 0x060015DA RID: 5594 RVA: 0x0005607C File Offset: 0x0005427C
		internal UIElementsBridge uiElementsBridge
		{
			get
			{
				bool flag = this.m_UIElementsBridge != null;
				if (flag)
				{
					return this.m_UIElementsBridge;
				}
				throw new Exception("Panel has no UIElementsBridge.");
			}
			set
			{
				this.m_UIElementsBridge = value;
			}
		}

		// Token: 0x060015DB RID: 5595 RVA: 0x00056088 File Offset: 0x00054288
		protected BaseVisualElementPanel()
		{
			this.layoutConfig = LayoutManager.SharedManager.CreateConfig();
			this.m_UIElementsBridge = new RuntimeUIElementsBridge();
		}

		// Token: 0x060015DC RID: 5596 RVA: 0x00056116 File Offset: 0x00054316
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060015DD RID: 5597 RVA: 0x00056128 File Offset: 0x00054328
		protected virtual void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					bool flag = this.ownerObject != null;
					if (flag)
					{
						UIElementsUtility.RemoveCachedPanel(this.ownerObject.GetInstanceID());
					}
					PointerDeviceState.RemovePanelData(this);
				}
				Action<BaseVisualElementPanel> action = this.panelDisposed;
				if (action != null)
				{
					action(this);
				}
				LayoutManager.SharedManager.DestroyConfig(ref this.layoutConfig);
				this.disposed = true;
			}
		}

		// Token: 0x060015DE RID: 5598
		public abstract void Repaint(Event e);

		// Token: 0x060015DF RID: 5599
		public abstract void ValidateFocus();

		// Token: 0x060015E0 RID: 5600
		public abstract void ValidateLayout();

		// Token: 0x060015E1 RID: 5601
		public abstract void UpdateAnimations();

		// Token: 0x060015E2 RID: 5602
		public abstract void UpdateBindings();

		// Token: 0x060015E3 RID: 5603
		public abstract void UpdateDataBinding();

		// Token: 0x060015E4 RID: 5604
		public abstract void ApplyStyles();

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x060015E5 RID: 5605 RVA: 0x000561A0 File Offset: 0x000543A0
		// (set) Token: 0x060015E6 RID: 5606 RVA: 0x000561B8 File Offset: 0x000543B8
		internal unsafe float scale
		{
			get
			{
				return this.m_Scale;
			}
			set
			{
				bool flag = !Mathf.Approximately(this.m_Scale, value);
				if (flag)
				{
					this.m_Scale = value;
					this.visualTree.IncrementVersion(VersionChangeType.Layout);
					*this.layoutConfig.PointScaleFactor = this.scaledPixelsPerPoint;
					this.visualTree.IncrementVersion(VersionChangeType.StyleSheet);
				}
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x060015E7 RID: 5607 RVA: 0x00056210 File Offset: 0x00054410
		// (set) Token: 0x060015E8 RID: 5608 RVA: 0x00056228 File Offset: 0x00054428
		internal unsafe float pixelsPerPoint
		{
			get
			{
				return this.m_PixelsPerPoint;
			}
			set
			{
				bool flag = !Mathf.Approximately(this.m_PixelsPerPoint, value);
				if (flag)
				{
					this.m_PixelsPerPoint = value;
					this.visualTree.IncrementVersion(VersionChangeType.Layout);
					*this.layoutConfig.PointScaleFactor = this.scaledPixelsPerPoint;
					this.visualTree.IncrementVersion(VersionChangeType.StyleSheet);
				}
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x060015E9 RID: 5609 RVA: 0x00056280 File Offset: 0x00054480
		public float scaledPixelsPerPoint
		{
			get
			{
				return this.m_PixelsPerPoint * this.m_Scale;
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x060015EA RID: 5610 RVA: 0x0005629F File Offset: 0x0005449F
		// (set) Token: 0x060015EB RID: 5611 RVA: 0x000562A7 File Offset: 0x000544A7
		public float referenceSpritePixelsPerUnit { get; set; } = 100f;

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x060015EC RID: 5612 RVA: 0x000562B0 File Offset: 0x000544B0
		// (set) Token: 0x060015ED RID: 5613 RVA: 0x000562B8 File Offset: 0x000544B8
		internal PanelClearSettings clearSettings { get; set; } = new PanelClearSettings
		{
			clearDepthStencil = true,
			clearColor = true,
			color = Color.clear
		};

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x060015EE RID: 5614 RVA: 0x000562C1 File Offset: 0x000544C1
		// (set) Token: 0x060015EF RID: 5615 RVA: 0x000562C9 File Offset: 0x000544C9
		internal bool duringLayoutPhase { get; set; }

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x060015F0 RID: 5616 RVA: 0x000562D4 File Offset: 0x000544D4
		public bool isDirty
		{
			get
			{
				return this.version != this.repaintVersion;
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x060015F1 RID: 5617
		internal abstract uint version { get; }

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x060015F2 RID: 5618
		internal abstract uint repaintVersion { get; }

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x060015F3 RID: 5619
		internal abstract uint hierarchyVersion { get; }

		// Token: 0x060015F4 RID: 5620
		internal abstract void OnVersionChanged(VisualElement ele, VersionChangeType changeTypeFlag);

		// Token: 0x060015F5 RID: 5621
		internal abstract void SetUpdater(IVisualTreeUpdater updater, VisualTreeUpdatePhase phase);

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x060015F6 RID: 5622 RVA: 0x000562F7 File Offset: 0x000544F7
		// (set) Token: 0x060015F7 RID: 5623 RVA: 0x000562FF File Offset: 0x000544FF
		internal virtual RepaintData repaintData { get; set; }

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x060015F8 RID: 5624 RVA: 0x00056308 File Offset: 0x00054508
		// (set) Token: 0x060015F9 RID: 5625 RVA: 0x00056310 File Offset: 0x00054510
		internal virtual ICursorManager cursorManager { get; set; }

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x060015FA RID: 5626 RVA: 0x00056319 File Offset: 0x00054519
		// (set) Token: 0x060015FB RID: 5627 RVA: 0x00056321 File Offset: 0x00054521
		public ContextualMenuManager contextualMenuManager { get; internal set; }

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x060015FC RID: 5628 RVA: 0x0005632A File Offset: 0x0005452A
		// (set) Token: 0x060015FD RID: 5629 RVA: 0x00056332 File Offset: 0x00054532
		internal virtual DataBindingManager dataBindingManager { get; set; }

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x060015FE RID: 5630
		public abstract VisualElement visualTree { get; }

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x060015FF RID: 5631
		// (set) Token: 0x06001600 RID: 5632
		public abstract EventDispatcher dispatcher { get; set; }

		// Token: 0x06001601 RID: 5633 RVA: 0x0005633B File Offset: 0x0005453B
		internal void SendEvent(EventBase e, DispatchMode dispatchMode = DispatchMode.Default)
		{
			Debug.Assert(this.dispatcher != null, "dispatcher != null");
			EventDispatcher dispatcher = this.dispatcher;
			if (dispatcher != null)
			{
				dispatcher.Dispatch(e, this, dispatchMode);
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06001602 RID: 5634
		internal abstract IScheduler scheduler { get; }

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06001603 RID: 5635
		// (set) Token: 0x06001604 RID: 5636
		internal abstract IStylePropertyAnimationSystem styleAnimationSystem
		{
			get; [VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			set;
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06001605 RID: 5637
		public abstract ContextType contextType { get; }

		// Token: 0x06001606 RID: 5638
		public abstract VisualElement Pick(Vector2 point);

		// Token: 0x06001607 RID: 5639
		public abstract VisualElement PickAll(Vector2 point, List<VisualElement> picked);

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06001608 RID: 5640 RVA: 0x00056367 File Offset: 0x00054567
		// (set) Token: 0x06001609 RID: 5641 RVA: 0x0005636F File Offset: 0x0005456F
		internal bool disposed { get; private set; }

		// Token: 0x0600160A RID: 5642
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal abstract IVisualTreeUpdater GetUpdater(VisualTreeUpdatePhase phase);

		// Token: 0x0600160B RID: 5643 RVA: 0x00056378 File Offset: 0x00054578
		internal VisualElement GetTopElementUnderPointer(int pointerId)
		{
			return this.m_TopElementUnderPointers.GetTopElementUnderPointer(pointerId);
		}

		// Token: 0x0600160C RID: 5644 RVA: 0x00056398 File Offset: 0x00054598
		internal VisualElement RecomputeTopElementUnderPointer(int pointerId, Vector2 pointerPos, EventBase triggerEvent)
		{
			VisualElement visualElement = null;
			bool flag = PointerDeviceState.GetPanel(pointerId, this.contextType) == this && !PointerDeviceState.HasLocationFlag(pointerId, this.contextType, PointerDeviceState.LocationFlag.OutsidePanel);
			if (flag)
			{
				visualElement = this.Pick(pointerPos);
			}
			this.m_TopElementUnderPointers.SetElementUnderPointer(visualElement, pointerId, triggerEvent);
			IPointerEventInternal pointerEventInternal = triggerEvent as IPointerEventInternal;
			bool flag2 = pointerEventInternal != null && pointerEventInternal.compatibilityMouseEvent != null;
			if (flag2)
			{
				this.m_TopElementUnderPointers.SetElementUnderPointer(visualElement, PointerId.mousePointerId, (EventBase)pointerEventInternal.compatibilityMouseEvent);
			}
			return visualElement;
		}

		// Token: 0x0600160D RID: 5645 RVA: 0x00056427 File Offset: 0x00054627
		internal void ClearCachedElementUnderPointer(int pointerId, EventBase triggerEvent)
		{
			this.m_TopElementUnderPointers.SetTemporaryElementUnderPointer(null, pointerId, triggerEvent);
		}

		// Token: 0x0600160E RID: 5646 RVA: 0x00056439 File Offset: 0x00054639
		internal void CommitElementUnderPointers()
		{
			this.m_TopElementUnderPointers.CommitElementUnderPointers(this.dispatcher, this.contextType);
		}

		// Token: 0x14000044 RID: 68
		// (add) Token: 0x0600160F RID: 5647 RVA: 0x00056454 File Offset: 0x00054654
		// (remove) Token: 0x06001610 RID: 5648 RVA: 0x0005648C File Offset: 0x0005468C
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal event Action isFlatChanged;

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06001611 RID: 5649 RVA: 0x000564C1 File Offset: 0x000546C1
		// (set) Token: 0x06001612 RID: 5650 RVA: 0x000564CC File Offset: 0x000546CC
		public bool isFlat
		{
			get
			{
				return this.m_IsFlat;
			}
			set
			{
				bool flag = this.m_IsFlat == value;
				if (!flag)
				{
					this.m_IsFlat = value;
					this.SetSpecializedHierarchyFlagsUpdater();
					Action action = this.isFlatChanged;
					if (action != null)
					{
						action();
					}
				}
			}
		}

		// Token: 0x06001613 RID: 5651 RVA: 0x0005650C File Offset: 0x0005470C
		internal void SetSpecializedHierarchyFlagsUpdater()
		{
			IVisualTreeUpdater updater = this.GetUpdater(VisualTreeUpdatePhase.TransformClip);
			bool flag = updater is VisualTreeWorldSpaceHierarchyFlagsUpdater;
			bool isFlat = this.isFlat;
			if (isFlat)
			{
				bool flag2 = flag;
				if (flag2)
				{
					this.SetUpdater(new VisualTreeHierarchyFlagsUpdater(), VisualTreeUpdatePhase.TransformClip);
				}
			}
			else
			{
				bool flag3 = !flag;
				if (flag3)
				{
					this.SetUpdater(new VisualTreeWorldSpaceHierarchyFlagsUpdater(), VisualTreeUpdatePhase.TransformClip);
				}
			}
		}

		// Token: 0x14000045 RID: 69
		// (add) Token: 0x06001614 RID: 5652 RVA: 0x00056568 File Offset: 0x00054768
		// (remove) Token: 0x06001615 RID: 5653 RVA: 0x000565A0 File Offset: 0x000547A0
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal event Action atlasChanged;

		// Token: 0x06001616 RID: 5654 RVA: 0x000565D5 File Offset: 0x000547D5
		protected void InvokeAtlasChanged()
		{
			Action action = this.atlasChanged;
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06001617 RID: 5655
		// (set) Token: 0x06001618 RID: 5656
		public abstract AtlasBase atlas { get; set; }

		// Token: 0x14000046 RID: 70
		// (add) Token: 0x06001619 RID: 5657 RVA: 0x000565EC File Offset: 0x000547EC
		// (remove) Token: 0x0600161A RID: 5658 RVA: 0x00056624 File Offset: 0x00054824
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal event HierarchyEvent hierarchyChanged;

		// Token: 0x0600161B RID: 5659 RVA: 0x0005665C File Offset: 0x0005485C
		internal void InvokeHierarchyChanged(VisualElement ve, HierarchyChangeType changeType, IReadOnlyList<VisualElement> additionalContext = null)
		{
			bool flag = this.hierarchyChanged != null;
			if (flag)
			{
				this.hierarchyChanged(ve, changeType, additionalContext);
			}
		}

		// Token: 0x14000047 RID: 71
		// (add) Token: 0x0600161C RID: 5660 RVA: 0x00056688 File Offset: 0x00054888
		// (remove) Token: 0x0600161D RID: 5661 RVA: 0x000566C0 File Offset: 0x000548C0
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal event Action<IPanel> beforeUpdate;

		// Token: 0x0600161E RID: 5662 RVA: 0x000566F5 File Offset: 0x000548F5
		internal void InvokeBeforeUpdate()
		{
			Action<IPanel> action = this.beforeUpdate;
			if (action != null)
			{
				action(this);
			}
		}

		// Token: 0x0600161F RID: 5663 RVA: 0x0005670C File Offset: 0x0005490C
		internal void UpdateElementUnderPointers()
		{
			foreach (int num in PointerId.hoveringPointers)
			{
				bool flag = PointerDeviceState.GetPanel(num, this.contextType) != this || PointerDeviceState.HasLocationFlag(num, this.contextType, PointerDeviceState.LocationFlag.OutsidePanel);
				if (flag)
				{
					this.m_TopElementUnderPointers.SetElementUnderPointer(null, num, new Vector2(-2.1474836E+09f, -2.1474836E+09f));
				}
				else
				{
					Vector2 pointerPosition = PointerDeviceState.GetPointerPosition(num, this.contextType);
					VisualElement visualElement = this.PickAll(pointerPosition, null);
					this.m_TopElementUnderPointers.SetElementUnderPointer(visualElement, num, pointerPosition);
				}
			}
			this.CommitElementUnderPointers();
		}

		// Token: 0x06001620 RID: 5664 RVA: 0x00002152 File Offset: 0x00000352
		void IGroupBox.OnOptionAdded(IGroupBoxOption option)
		{
		}

		// Token: 0x06001621 RID: 5665 RVA: 0x00002152 File Offset: 0x00000352
		void IGroupBox.OnOptionRemoved(IGroupBoxOption option)
		{
		}

		// Token: 0x06001622 RID: 5666 RVA: 0x000567AC File Offset: 0x000549AC
		public virtual void Render()
		{
			this.panelRenderer.Render();
		}

		// Token: 0x06001623 RID: 5667 RVA: 0x000567BA File Offset: 0x000549BA
		internal virtual IGenericMenu CreateMenu()
		{
			return new GenericDropdownMenu();
		}

		// Token: 0x04000C1D RID: 3101
		private UIElementsBridge m_UIElementsBridge;

		// Token: 0x04000C1E RID: 3102
		private float m_Scale = 1f;

		// Token: 0x04000C1F RID: 3103
		internal LayoutConfig layoutConfig;

		// Token: 0x04000C20 RID: 3104
		private float m_PixelsPerPoint = 1f;

		// Token: 0x04000C23 RID: 3107
		internal IPanelRenderer panelRenderer;

		// Token: 0x04000C2A RID: 3114
		internal ElementUnderPointer m_TopElementUnderPointers = new ElementUnderPointer();

		// Token: 0x04000C2C RID: 3116
		private bool m_IsFlat = true;
	}
}
