using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Profiling;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x0200030D RID: 781
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal class Panel : BaseVisualElementPanel
	{
		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06001634 RID: 5684 RVA: 0x000567C4 File Offset: 0x000549C4
		public sealed override VisualElement visualTree
		{
			get
			{
				return this.m_RootContainer;
			}
		}

		// Token: 0x06001635 RID: 5685 RVA: 0x000567DC File Offset: 0x000549DC
		internal Panel.UIFrameState GetFrameState()
		{
			return new Panel.UIFrameState(this);
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06001636 RID: 5686 RVA: 0x000567F4 File Offset: 0x000549F4
		// (set) Token: 0x06001637 RID: 5687 RVA: 0x000567FC File Offset: 0x000549FC
		public sealed override EventDispatcher dispatcher { get; set; }

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06001638 RID: 5688 RVA: 0x00056808 File Offset: 0x00054A08
		public TimerEventScheduler timerEventScheduler
		{
			get
			{
				TimerEventScheduler timerEventScheduler;
				if ((timerEventScheduler = this.m_Scheduler) == null)
				{
					timerEventScheduler = (this.m_Scheduler = new TimerEventScheduler());
				}
				return timerEventScheduler;
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06001639 RID: 5689 RVA: 0x00056834 File Offset: 0x00054A34
		internal override IScheduler scheduler
		{
			get
			{
				return this.timerEventScheduler;
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x0600163A RID: 5690 RVA: 0x0005684C File Offset: 0x00054A4C
		internal VisualTreeUpdater visualTreeUpdater
		{
			get
			{
				return this.m_VisualTreeUpdater;
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x0600163B RID: 5691 RVA: 0x00056864 File Offset: 0x00054A64
		// (set) Token: 0x0600163C RID: 5692 RVA: 0x0005686C File Offset: 0x00054A6C
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal override IStylePropertyAnimationSystem styleAnimationSystem
		{
			get
			{
				return this.m_StylePropertyAnimationSystem;
			}
			set
			{
				bool flag = this.m_StylePropertyAnimationSystem == value;
				if (!flag)
				{
					IStylePropertyAnimationSystem stylePropertyAnimationSystem = this.m_StylePropertyAnimationSystem;
					if (stylePropertyAnimationSystem != null)
					{
						stylePropertyAnimationSystem.CancelAllAnimations();
					}
					this.m_StylePropertyAnimationSystem = value;
				}
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x0600163D RID: 5693 RVA: 0x000568A2 File Offset: 0x00054AA2
		// (set) Token: 0x0600163E RID: 5694 RVA: 0x000568AA File Offset: 0x00054AAA
		public override ScriptableObject ownerObject { get; protected set; }

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x0600163F RID: 5695 RVA: 0x000568B3 File Offset: 0x00054AB3
		public override ContextType contextType { get; }

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06001640 RID: 5696 RVA: 0x000568BB File Offset: 0x00054ABB
		// (set) Token: 0x06001641 RID: 5697 RVA: 0x000568C3 File Offset: 0x00054AC3
		public override SavePersistentViewData saveViewData { get; set; }

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06001642 RID: 5698 RVA: 0x000568CC File Offset: 0x00054ACC
		// (set) Token: 0x06001643 RID: 5699 RVA: 0x000568D4 File Offset: 0x00054AD4
		public override GetViewDataDictionary getViewDataDictionary { get; set; }

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06001644 RID: 5700 RVA: 0x000568DD File Offset: 0x00054ADD
		// (set) Token: 0x06001645 RID: 5701 RVA: 0x000568E5 File Offset: 0x00054AE5
		public sealed override FocusController focusController { get; set; }

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06001646 RID: 5702 RVA: 0x000568EE File Offset: 0x00054AEE
		// (set) Token: 0x06001647 RID: 5703 RVA: 0x000568F6 File Offset: 0x00054AF6
		public override EventInterests IMGUIEventInterests { get; set; }

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06001648 RID: 5704 RVA: 0x000568FF File Offset: 0x00054AFF
		// (set) Token: 0x06001649 RID: 5705 RVA: 0x00056906 File Offset: 0x00054B06
		internal static LoadResourceFunction loadResourceFunc { private get; set; }

		// Token: 0x0600164A RID: 5706 RVA: 0x00056910 File Offset: 0x00054B10
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal static Object LoadResource(string pathName, Type type, float dpiScaling)
		{
			bool flag = Panel.loadResourceFunc != null;
			Object @object;
			if (flag)
			{
				@object = Panel.loadResourceFunc(pathName, type, dpiScaling);
			}
			else
			{
				@object = Resources.Load(pathName, type);
			}
			return @object;
		}

		// Token: 0x0600164B RID: 5707 RVA: 0x0005694D File Offset: 0x00054B4D
		internal void Focus()
		{
			this.m_JustReceivedFocus = true;
		}

		// Token: 0x0600164C RID: 5708 RVA: 0x00056957 File Offset: 0x00054B57
		internal void Blur()
		{
			FocusController focusController = this.focusController;
			if (focusController != null)
			{
				focusController.BlurLastFocusedElement();
			}
		}

		// Token: 0x0600164D RID: 5709 RVA: 0x0005696C File Offset: 0x00054B6C
		public override void ValidateFocus()
		{
			bool justReceivedFocus = this.m_JustReceivedFocus;
			if (justReceivedFocus)
			{
				this.m_JustReceivedFocus = false;
				FocusController focusController = this.focusController;
				if (focusController != null)
				{
					focusController.SetFocusToLastFocusedElement();
				}
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x0600164E RID: 5710 RVA: 0x000569A0 File Offset: 0x00054BA0
		// (set) Token: 0x0600164F RID: 5711 RVA: 0x000569B8 File Offset: 0x00054BB8
		internal string name
		{
			get
			{
				return this.m_PanelName;
			}
			set
			{
				this.m_PanelName = value;
				this.CreateMarkers();
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06001650 RID: 5712 RVA: 0x000569C9 File Offset: 0x00054BC9
		// (set) Token: 0x06001651 RID: 5713 RVA: 0x000569D4 File Offset: 0x00054BD4
		public IDebugPanelChangeReceiver panelChangeReceiver
		{
			get
			{
				return this.m_PanelChangeReceiver;
			}
			set
			{
				this.m_PanelChangeReceiver = value;
				bool flag = value != null;
				if (flag)
				{
					Debug.LogWarning("IPanelChangeReceiver suscribed to panel '" + this.name + "' and may affect performance. The callback should be used only in debugging scenario and won't work outside development builds");
				}
			}
		}

		// Token: 0x06001652 RID: 5714 RVA: 0x00056A0C File Offset: 0x00054C0C
		private void CreateMarkers()
		{
			string text = (string.IsNullOrEmpty(this.m_PanelName) ? "" : ("." + this.m_PanelName));
			this.m_MarkerBeforeUpdate = new ProfilerMarker("Panel.BeforeUpdate" + text);
			this.m_MarkerUpdate = new ProfilerMarker("Panel.Update" + text);
			this.m_MarkerRender = new ProfilerMarker("Panel.Render" + text);
			this.m_MarkerLayout = new ProfilerMarker("Panel.Layout" + text);
			this.m_MarkerBindings = new ProfilerMarker("Panel.Bindings" + text);
			this.m_MarkerDataBinding = new ProfilerMarker("Panel.DataBinding" + text);
			this.m_MarkerAnimations = new ProfilerMarker("Panel.Animations" + text);
			this.m_MarkerPanelChangeReceiver = new ProfilerMarker("Panel.PanelChangeReceiver" + text);
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06001653 RID: 5715 RVA: 0x00056AEF File Offset: 0x00054CEF
		// (set) Token: 0x06001654 RID: 5716 RVA: 0x00056AF6 File Offset: 0x00054CF6
		internal static TimeMsFunction TimeSinceStartup { private get; set; }

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06001655 RID: 5717 RVA: 0x00056AFE File Offset: 0x00054CFE
		// (set) Token: 0x06001656 RID: 5718 RVA: 0x00056B06 File Offset: 0x00054D06
		public override int IMGUIContainersCount { get; set; }

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06001657 RID: 5719 RVA: 0x00056B0F File Offset: 0x00054D0F
		// (set) Token: 0x06001658 RID: 5720 RVA: 0x00056B17 File Offset: 0x00054D17
		public override IMGUIContainer rootIMGUIContainer { get; set; }

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06001659 RID: 5721 RVA: 0x00056B20 File Offset: 0x00054D20
		internal override uint version
		{
			get
			{
				return this.m_Version;
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x0600165A RID: 5722 RVA: 0x00056B28 File Offset: 0x00054D28
		internal override uint repaintVersion
		{
			get
			{
				return this.m_RepaintVersion;
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x0600165B RID: 5723 RVA: 0x00056B30 File Offset: 0x00054D30
		internal override uint hierarchyVersion
		{
			get
			{
				return this.m_HierarchyVersion;
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x0600165C RID: 5724 RVA: 0x00056B38 File Offset: 0x00054D38
		// (set) Token: 0x0600165D RID: 5725 RVA: 0x00056B50 File Offset: 0x00054D50
		public override AtlasBase atlas
		{
			get
			{
				return this.m_Atlas;
			}
			set
			{
				bool flag = this.m_Atlas != value;
				if (flag)
				{
					AtlasBase atlas = this.m_Atlas;
					if (atlas != null)
					{
						atlas.InvokeRemovedFromPanel(this);
					}
					this.m_Atlas = value;
					base.InvokeAtlasChanged();
					AtlasBase atlas2 = this.m_Atlas;
					if (atlas2 != null)
					{
						atlas2.InvokeAssignedToPanel(this);
					}
				}
			}
		}

		// Token: 0x0600165E RID: 5726 RVA: 0x00056BA4 File Offset: 0x00054DA4
		public Panel(ScriptableObject ownerObject, ContextType contextType, EventDispatcher dispatcher)
		{
			Debug.Assert(contextType == ContextType.Player, "In a player, panel context type must be set to Player.");
			contextType = ContextType.Player;
			this.ownerObject = ownerObject;
			this.contextType = contextType;
			this.dispatcher = dispatcher;
			this.repaintData = new RepaintData();
			this.cursorManager = new CursorManager();
			base.contextualMenuManager = null;
			this.dataBindingManager = new DataBindingManager(this);
			this.m_VisualTreeUpdater = new VisualTreeUpdater(this);
			base.SetSpecializedHierarchyFlagsUpdater();
			this.m_RootContainer = ((contextType == ContextType.Editor) ? new EditorPanelRootElement() : new PanelRootElement());
			this.visualTree.SetPanel(this);
			this.focusController = new FocusController(new VisualElementFocusRing(this.visualTree, VisualElementFocusRing.DefaultFocusOrder.ChildOrder));
			this.styleAnimationSystem = new StylePropertyAnimationSystem();
			this.CreateMarkers();
			base.InvokeHierarchyChanged(this.visualTree, HierarchyChangeType.AddedToParent, null);
			this.atlas = new DynamicAtlas();
		}

		// Token: 0x0600165F RID: 5727 RVA: 0x00056CA8 File Offset: 0x00054EA8
		protected override void Dispose(bool disposing)
		{
			bool disposed = base.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					this.atlas = null;
					this.visualTree.Clear();
					this.m_VisualTreeUpdater.Dispose();
				}
				base.Dispose(disposing);
			}
		}

		// Token: 0x06001660 RID: 5728 RVA: 0x00056CF4 File Offset: 0x00054EF4
		public static long TimeSinceStartupMs()
		{
			TimeMsFunction timeSinceStartup = Panel.TimeSinceStartup;
			return (timeSinceStartup != null) ? timeSinceStartup() : Panel.DefaultTimeSinceStartupMs();
		}

		// Token: 0x06001661 RID: 5729 RVA: 0x00056D1C File Offset: 0x00054F1C
		internal static long DefaultTimeSinceStartupMs()
		{
			return (long)(Time.realtimeSinceStartup * 1000f);
		}

		// Token: 0x06001662 RID: 5730 RVA: 0x00056D3C File Offset: 0x00054F3C
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal static VisualElement PickAllWithoutValidatingLayout(VisualElement root, Vector2 point)
		{
			return Panel.PickAll(root, point, null, false);
		}

		// Token: 0x06001663 RID: 5731 RVA: 0x00056D58 File Offset: 0x00054F58
		private static VisualElement PickAll(VisualElement root, Vector2 point, List<VisualElement> picked = null, bool includeIgnoredElement = false)
		{
			return Panel.PerformPick(root, point, picked, includeIgnoredElement);
		}

		// Token: 0x06001664 RID: 5732 RVA: 0x00056D78 File Offset: 0x00054F78
		private static VisualElement PerformPick(VisualElement root, Vector2 point, List<VisualElement> picked = null, bool includeIgnoredElement = false)
		{
			bool flag = root.resolvedStyle.display == DisplayStyle.None;
			VisualElement visualElement;
			if (flag)
			{
				visualElement = null;
			}
			else
			{
				bool flag2 = root.pickingMode == PickingMode.Ignore && root.hierarchy.childCount == 0 && !includeIgnoredElement;
				if (flag2)
				{
					visualElement = null;
				}
				else
				{
					bool flag3 = !root.worldBoundingBox.Contains(point);
					if (flag3)
					{
						visualElement = null;
					}
					else
					{
						Vector2 vector = root.WorldToLocal(point);
						bool flag4 = root.ContainsPoint(vector);
						bool flag5 = !flag4 && root.ShouldClip();
						if (flag5)
						{
							visualElement = null;
						}
						else
						{
							VisualElement visualElement2 = null;
							int childCount = root.hierarchy.childCount;
							for (int i = childCount - 1; i >= 0; i--)
							{
								VisualElement visualElement3 = root.hierarchy[i];
								VisualElement visualElement4 = Panel.PerformPick(visualElement3, point, picked, includeIgnoredElement);
								bool flag6 = visualElement2 == null && visualElement4 != null;
								if (flag6)
								{
									bool flag7 = picked == null;
									if (flag7)
									{
										return visualElement4;
									}
									visualElement2 = visualElement4;
								}
							}
							bool flag8 = root.visible && (root.pickingMode == PickingMode.Position || includeIgnoredElement) && flag4;
							if (flag8)
							{
								if (picked != null)
								{
									picked.Add(root);
								}
								bool flag9 = visualElement2 == null;
								if (flag9)
								{
									visualElement2 = root;
								}
							}
							visualElement = visualElement2;
						}
					}
				}
			}
			return visualElement;
		}

		// Token: 0x06001665 RID: 5733 RVA: 0x00056ED4 File Offset: 0x000550D4
		public override VisualElement PickAll(Vector2 point, List<VisualElement> picked)
		{
			this.ValidateLayout();
			bool flag = picked != null;
			if (flag)
			{
				picked.Clear();
			}
			return Panel.PickAll(this.visualTree, point, picked, false);
		}

		// Token: 0x06001666 RID: 5734 RVA: 0x00056F0C File Offset: 0x0005510C
		public override VisualElement Pick(Vector2 point)
		{
			this.ValidateLayout();
			Vector2 vector;
			bool flag;
			VisualElement topElementUnderPointer = this.m_TopElementUnderPointers.GetTopElementUnderPointer(PointerId.mousePointerId, out vector, out flag);
			bool flag2 = !flag && Panel.<Pick>g__PixelOf|105_0(vector) == Panel.<Pick>g__PixelOf|105_0(point);
			VisualElement visualElement;
			if (flag2)
			{
				visualElement = topElementUnderPointer;
			}
			else
			{
				visualElement = Panel.PickAll(this.visualTree, point, null, false);
			}
			return visualElement;
		}

		// Token: 0x06001667 RID: 5735 RVA: 0x00056F6C File Offset: 0x0005516C
		public override void ValidateLayout()
		{
			bool flag = !this.m_ValidatingLayout;
			if (flag)
			{
				this.m_ValidatingLayout = true;
				this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.Styles);
				this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.Layout);
				this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.TransformClip);
				this.m_ValidatingLayout = false;
			}
		}

		// Token: 0x06001668 RID: 5736 RVA: 0x00056FBE File Offset: 0x000551BE
		public override void UpdateAnimations()
		{
			this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.Animation);
		}

		// Token: 0x06001669 RID: 5737 RVA: 0x00056FCE File Offset: 0x000551CE
		public override void UpdateBindings()
		{
			this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.Bindings);
		}

		// Token: 0x0600166A RID: 5738 RVA: 0x00056FDE File Offset: 0x000551DE
		public override void UpdateDataBinding()
		{
			this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.DataBinding);
		}

		// Token: 0x0600166B RID: 5739 RVA: 0x00056FEE File Offset: 0x000551EE
		public override void ApplyStyles()
		{
			this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.Styles);
		}

		// Token: 0x0600166C RID: 5740 RVA: 0x00057000 File Offset: 0x00055200
		private void UpdateForRepaint()
		{
			this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.DataBinding);
			this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.Styles);
			this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.Layout);
			this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.TransformClip);
			this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.Repaint);
		}

		// Token: 0x0600166D RID: 5741 RVA: 0x00057050 File Offset: 0x00055250
		internal void UpdateWithoutRepaint()
		{
			this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.Bindings);
			this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.DataBinding);
			this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.Animation);
			this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.Styles);
			this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.Layout);
		}

		// Token: 0x14000048 RID: 72
		// (add) Token: 0x0600166E RID: 5742 RVA: 0x000570A0 File Offset: 0x000552A0
		// (remove) Token: 0x0600166F RID: 5743 RVA: 0x000570D4 File Offset: 0x000552D4
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal static event Action<Panel> beforeAnyRepaint;

		// Token: 0x06001670 RID: 5744 RVA: 0x00057108 File Offset: 0x00055308
		public override void Repaint(Event e)
		{
			this.m_RepaintVersion = this.version;
			this.repaintData.repaintEvent = e;
			using (this.m_MarkerBeforeUpdate.Auto())
			{
				base.InvokeBeforeUpdate();
			}
			Action<Panel> action = Panel.beforeAnyRepaint;
			if (action != null)
			{
				action(this);
			}
			using (this.m_MarkerUpdate.Auto())
			{
				this.UpdateForRepaint();
			}
		}

		// Token: 0x06001671 RID: 5745 RVA: 0x000571AC File Offset: 0x000553AC
		public override void Render()
		{
			base.Render();
		}

		// Token: 0x06001672 RID: 5746 RVA: 0x000571B8 File Offset: 0x000553B8
		internal override void OnVersionChanged(VisualElement ve, VersionChangeType versionChangeType)
		{
			this.m_Version += 1U;
			this.m_VisualTreeUpdater.OnVersionChanged(ve, versionChangeType);
			bool flag = this.panelChangeReceiver != null;
			if (flag)
			{
				using (this.m_MarkerPanelChangeReceiver.Auto())
				{
					this.panelChangeReceiver.OnVisualElementChange(ve, versionChangeType);
				}
			}
			bool flag2 = (versionChangeType & VersionChangeType.Hierarchy) == VersionChangeType.Hierarchy;
			if (flag2)
			{
				this.m_HierarchyVersion += 1U;
			}
		}

		// Token: 0x06001673 RID: 5747 RVA: 0x00057244 File Offset: 0x00055444
		internal override void SetUpdater(IVisualTreeUpdater updater, VisualTreeUpdatePhase phase)
		{
			this.m_VisualTreeUpdater.SetUpdater(updater, phase);
		}

		// Token: 0x06001674 RID: 5748 RVA: 0x00057258 File Offset: 0x00055458
		internal override IVisualTreeUpdater GetUpdater(VisualTreeUpdatePhase phase)
		{
			return this.m_VisualTreeUpdater.GetUpdater(phase);
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06001675 RID: 5749 RVA: 0x00057276 File Offset: 0x00055476
		internal virtual Color HyperlinkColor
		{
			get
			{
				return Color.blue;
			}
		}

		// Token: 0x06001677 RID: 5751 RVA: 0x0005728E File Offset: 0x0005548E
		[CompilerGenerated]
		internal static Vector2Int <Pick>g__PixelOf|105_0(Vector2 p)
		{
			return Vector2Int.FloorToInt(p);
		}

		// Token: 0x04000C30 RID: 3120
		internal const int k_DefaultPixelsPerUnit = 100;

		// Token: 0x04000C31 RID: 3121
		private VisualElement m_RootContainer;

		// Token: 0x04000C32 RID: 3122
		private VisualTreeUpdater m_VisualTreeUpdater;

		// Token: 0x04000C33 RID: 3123
		private IStylePropertyAnimationSystem m_StylePropertyAnimationSystem;

		// Token: 0x04000C34 RID: 3124
		private string m_PanelName;

		// Token: 0x04000C35 RID: 3125
		private uint m_Version = 0U;

		// Token: 0x04000C36 RID: 3126
		private uint m_RepaintVersion = 0U;

		// Token: 0x04000C37 RID: 3127
		private uint m_HierarchyVersion = 0U;

		// Token: 0x04000C38 RID: 3128
		private ProfilerMarker m_MarkerBeforeUpdate;

		// Token: 0x04000C39 RID: 3129
		private ProfilerMarker m_MarkerUpdate;

		// Token: 0x04000C3A RID: 3130
		private ProfilerMarker m_MarkerRender;

		// Token: 0x04000C3B RID: 3131
		private ProfilerMarker m_MarkerLayout;

		// Token: 0x04000C3C RID: 3132
		private ProfilerMarker m_MarkerBindings;

		// Token: 0x04000C3D RID: 3133
		private ProfilerMarker m_MarkerDataBinding;

		// Token: 0x04000C3E RID: 3134
		private ProfilerMarker m_MarkerAnimations;

		// Token: 0x04000C3F RID: 3135
		private ProfilerMarker m_MarkerPanelChangeReceiver;

		// Token: 0x04000C40 RID: 3136
		private static ProfilerMarker s_MarkerPickAll = new ProfilerMarker("Panel.PickAll");

		// Token: 0x04000C42 RID: 3138
		private TimerEventScheduler m_Scheduler;

		// Token: 0x04000C4A RID: 3146
		private bool m_JustReceivedFocus;

		// Token: 0x04000C4B RID: 3147
		private IDebugPanelChangeReceiver m_PanelChangeReceiver;

		// Token: 0x04000C4F RID: 3151
		private AtlasBase m_Atlas;

		// Token: 0x04000C50 RID: 3152
		private bool m_ValidatingLayout = false;

		// Token: 0x0200030E RID: 782
		internal class UIFrameState
		{
			// Token: 0x17000440 RID: 1088
			// (get) Token: 0x06001678 RID: 5752 RVA: 0x00057296 File Offset: 0x00055496
			internal virtual long[] updatersFrameCount { get; }

			// Token: 0x17000441 RID: 1089
			// (get) Token: 0x06001679 RID: 5753 RVA: 0x0005729E File Offset: 0x0005549E
			internal virtual long schedulerFrameCount { get; }

			// Token: 0x17000442 RID: 1090
			// (get) Token: 0x0600167A RID: 5754 RVA: 0x000572A6 File Offset: 0x000554A6
			internal virtual bool isPanelDirty { get; }

			// Token: 0x17000443 RID: 1091
			// (get) Token: 0x0600167B RID: 5755 RVA: 0x000572AE File Offset: 0x000554AE
			internal virtual ContextType panelContextType { get; }

			// Token: 0x0600167C RID: 5756 RVA: 0x000572B6 File Offset: 0x000554B6
			internal UIFrameState()
			{
			}

			// Token: 0x0600167D RID: 5757 RVA: 0x000572C0 File Offset: 0x000554C0
			internal UIFrameState(Panel panel)
			{
				this.isPanelDirty = panel.isDirty;
				this.panelContextType = panel.contextType;
				this.schedulerFrameCount = panel.scheduler.FrameCount;
				this.updatersFrameCount = panel.visualTreeUpdater.GetUpdatersFrameCount();
			}

			// Token: 0x0600167E RID: 5758 RVA: 0x00057310 File Offset: 0x00055510
			public static bool operator >(Panel.UIFrameState leftOperand, Panel.UIFrameState rightOperand)
			{
				return leftOperand.HasFullUIFrameOccurredSince(rightOperand);
			}

			// Token: 0x0600167F RID: 5759 RVA: 0x0005732C File Offset: 0x0005552C
			public static bool operator <(Panel.UIFrameState leftOperand, Panel.UIFrameState rightOperand)
			{
				return rightOperand.HasFullUIFrameOccurredSince(leftOperand);
			}

			// Token: 0x06001680 RID: 5760 RVA: 0x00057348 File Offset: 0x00055548
			private bool HasFullUIFrameOccurredSince(Panel.UIFrameState reference)
			{
				bool flag = this.panelContextType != reference.panelContextType;
				if (flag)
				{
					throw new NotSupportedException("Comparison is only valid for frames with the same ContextType.");
				}
				bool flag2 = this.schedulerFrameCount <= reference.schedulerFrameCount;
				bool flag3;
				if (flag2)
				{
					flag3 = false;
				}
				else
				{
					bool flag4 = !this.isPanelDirty && this.panelContextType == ContextType.Editor;
					if (!flag4)
					{
						for (int i = 0; i < this.updatersFrameCount.Length; i++)
						{
							bool flag5 = this.updatersFrameCount[i] <= reference.updatersFrameCount[i];
							if (flag5)
							{
								return false;
							}
						}
					}
					flag3 = true;
				}
				return flag3;
			}
		}
	}
}
