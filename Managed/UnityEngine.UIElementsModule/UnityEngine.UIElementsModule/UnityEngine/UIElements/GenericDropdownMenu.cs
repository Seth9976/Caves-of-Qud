using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Pool;

namespace UnityEngine.UIElements
{
	// Token: 0x020000FB RID: 251
	public class GenericDropdownMenu : IGenericMenu
	{
		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000820 RID: 2080 RVA: 0x000237C4 File Offset: 0x000219C4
		internal List<GenericDropdownMenu.MenuItem> items
		{
			get
			{
				return this.m_Items;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000821 RID: 2081 RVA: 0x000237CC File Offset: 0x000219CC
		internal VisualElement menuContainer
		{
			get
			{
				return this.m_MenuContainer;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000822 RID: 2082 RVA: 0x000237D4 File Offset: 0x000219D4
		internal VisualElement outerContainer
		{
			get
			{
				return this.m_OuterContainer;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000823 RID: 2083 RVA: 0x000237DC File Offset: 0x000219DC
		internal ScrollView scrollView
		{
			get
			{
				return this.m_ScrollView;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000824 RID: 2084 RVA: 0x000237E4 File Offset: 0x000219E4
		// (set) Token: 0x06000825 RID: 2085 RVA: 0x000237EC File Offset: 0x000219EC
		internal bool isSingleSelectionDropdown { get; set; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000826 RID: 2086 RVA: 0x000237F5 File Offset: 0x000219F5
		// (set) Token: 0x06000827 RID: 2087 RVA: 0x000237FD File Offset: 0x000219FD
		internal bool closeOnParentResize { get; set; }

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000828 RID: 2088 RVA: 0x00023806 File Offset: 0x00021A06
		public VisualElement contentContainer
		{
			get
			{
				return this.m_ScrollView.contentContainer;
			}
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x00023814 File Offset: 0x00021A14
		public GenericDropdownMenu()
		{
			this.m_MenuContainer = new VisualElement();
			this.m_MenuContainer.AddToClassList(GenericDropdownMenu.ussClassName);
			this.m_OuterContainer = new VisualElement();
			this.m_OuterContainer.AddToClassList(GenericDropdownMenu.containerOuterUssClassName);
			this.m_MenuContainer.Add(this.m_OuterContainer);
			this.m_ScrollView = new ScrollView();
			this.m_ScrollView.AddToClassList(GenericDropdownMenu.containerInnerUssClassName);
			this.m_ScrollView.pickingMode = PickingMode.Position;
			this.m_ScrollView.contentContainer.focusable = true;
			this.m_ScrollView.touchScrollBehavior = ScrollView.TouchScrollBehavior.Clamped;
			this.m_ScrollView.mode = ScrollViewMode.VerticalAndHorizontal;
			this.m_OuterContainer.hierarchy.Add(this.m_ScrollView);
			this.m_MenuContainer.RegisterCallback<AttachToPanelEvent>(new EventCallback<AttachToPanelEvent>(this.OnAttachToPanel), TrickleDown.NoTrickleDown);
			this.m_MenuContainer.RegisterCallback<DetachFromPanelEvent>(new EventCallback<DetachFromPanelEvent>(this.OnDetachFromPanel), TrickleDown.NoTrickleDown);
			this.isSingleSelectionDropdown = true;
			this.closeOnParentResize = true;
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x00023930 File Offset: 0x00021B30
		private void OnAttachToPanel(AttachToPanelEvent evt)
		{
			bool flag = evt.destinationPanel == null;
			if (!flag)
			{
				this.contentContainer.AddManipulator(this.m_NavigationManipulator = new KeyboardNavigationManipulator(new Action<KeyboardNavigationOperation, EventBase>(this.Apply)));
				this.m_MenuContainer.RegisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDown), TrickleDown.NoTrickleDown);
				this.m_MenuContainer.RegisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMove), TrickleDown.NoTrickleDown);
				this.m_MenuContainer.RegisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUp), TrickleDown.NoTrickleDown);
				evt.destinationPanel.visualTree.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnParentResized), TrickleDown.NoTrickleDown);
				this.m_ScrollView.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnInitialDisplay), InvokePolicy.Once, TrickleDown.NoTrickleDown);
				this.m_ScrollView.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnContainerGeometryChanged), TrickleDown.NoTrickleDown);
				this.m_ScrollView.RegisterCallback<FocusOutEvent>(new EventCallback<FocusOutEvent>(this.OnFocusOut), TrickleDown.NoTrickleDown);
			}
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x00023A2C File Offset: 0x00021C2C
		private void OnDetachFromPanel(DetachFromPanelEvent evt)
		{
			bool flag = evt.originPanel == null;
			if (!flag)
			{
				this.contentContainer.RemoveManipulator(this.m_NavigationManipulator);
				this.m_MenuContainer.UnregisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDown), TrickleDown.NoTrickleDown);
				this.m_MenuContainer.UnregisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMove), TrickleDown.NoTrickleDown);
				this.m_MenuContainer.UnregisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUp), TrickleDown.NoTrickleDown);
				evt.originPanel.visualTree.UnregisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnParentResized), TrickleDown.NoTrickleDown);
				this.m_ScrollView.UnregisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnContainerGeometryChanged), TrickleDown.NoTrickleDown);
				this.m_ScrollView.UnregisterCallback<FocusOutEvent>(new EventCallback<FocusOutEvent>(this.OnFocusOut), TrickleDown.NoTrickleDown);
			}
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x00023AFC File Offset: 0x00021CFC
		private void Hide(bool giveFocusBack = false)
		{
			this.m_MenuContainer.RemoveFromHierarchy();
			bool flag = this.m_TargetElement != null;
			if (flag)
			{
				this.m_TargetElement.UnregisterCallback<DetachFromPanelEvent>(new EventCallback<DetachFromPanelEvent>(this.OnTargetElementDetachFromPanel), TrickleDown.NoTrickleDown);
				this.m_TargetElement.pseudoStates ^= PseudoStates.Active;
				bool flag2 = giveFocusBack && this.m_TargetElement.canGrabFocus;
				if (flag2)
				{
					this.m_TargetElement.Focus();
				}
			}
			this.m_TargetElement = null;
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x00023B7C File Offset: 0x00021D7C
		private void Apply(KeyboardNavigationOperation op, EventBase sourceEvent)
		{
			bool flag = this.Apply(op);
			if (flag)
			{
				sourceEvent.StopPropagation();
			}
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x00023BA0 File Offset: 0x00021DA0
		private bool Apply(KeyboardNavigationOperation op)
		{
			GenericDropdownMenu.<>c__DisplayClass48_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.selectedIndex = this.GetSelectedIndex();
			switch (op)
			{
			case KeyboardNavigationOperation.Cancel:
				this.Hide(true);
				return true;
			case KeyboardNavigationOperation.Submit:
			{
				GenericDropdownMenu.MenuItem menuItem = ((CS$<>8__locals1.selectedIndex != -1) ? this.m_Items[CS$<>8__locals1.selectedIndex] : null);
				bool flag = CS$<>8__locals1.selectedIndex >= 0 && menuItem.element.enabledSelf;
				if (flag)
				{
					Action action = menuItem.action;
					if (action != null)
					{
						action();
					}
					Action<object> actionUserData = menuItem.actionUserData;
					if (actionUserData != null)
					{
						actionUserData(menuItem.element.userData);
					}
				}
				this.Hide(true);
				return true;
			}
			case KeyboardNavigationOperation.Previous:
				this.<Apply>g__UpdateSelectionUp|48_1((CS$<>8__locals1.selectedIndex < 0) ? (this.m_Items.Count - 1) : (CS$<>8__locals1.selectedIndex - 1), ref CS$<>8__locals1);
				return true;
			case KeyboardNavigationOperation.Next:
				this.<Apply>g__UpdateSelectionDown|48_0(CS$<>8__locals1.selectedIndex + 1, ref CS$<>8__locals1);
				return true;
			case KeyboardNavigationOperation.PageUp:
			case KeyboardNavigationOperation.Begin:
				this.<Apply>g__UpdateSelectionDown|48_0(0, ref CS$<>8__locals1);
				return true;
			case KeyboardNavigationOperation.PageDown:
			case KeyboardNavigationOperation.End:
				this.<Apply>g__UpdateSelectionUp|48_1(this.m_Items.Count - 1, ref CS$<>8__locals1);
				return true;
			}
			return false;
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x00023CFC File Offset: 0x00021EFC
		private void OnPointerDown(PointerDownEvent evt)
		{
			this.m_MousePosition = this.m_ScrollView.WorldToLocal(evt.position);
			this.UpdateSelection(evt.elementTarget);
			bool flag = evt.pointerId != PointerId.mousePointerId;
			if (flag)
			{
				this.m_MenuContainer.panel.PreventCompatibilityMouseEvents(evt.pointerId);
			}
			evt.StopPropagation();
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x00023D68 File Offset: 0x00021F68
		private void OnPointerMove(PointerMoveEvent evt)
		{
			this.m_MousePosition = this.m_ScrollView.WorldToLocal(evt.position);
			this.UpdateSelection(evt.elementTarget);
			bool flag = evt.pointerId != PointerId.mousePointerId;
			if (flag)
			{
				this.m_MenuContainer.panel.PreventCompatibilityMouseEvents(evt.pointerId);
			}
			evt.StopPropagation();
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x00023DD4 File Offset: 0x00021FD4
		private void OnPointerUp(PointerUpEvent evt)
		{
			int selectedIndex = this.GetSelectedIndex();
			bool flag = selectedIndex != -1;
			if (flag)
			{
				GenericDropdownMenu.MenuItem menuItem = this.m_Items[selectedIndex];
				Action action = menuItem.action;
				if (action != null)
				{
					action();
				}
				Action<object> actionUserData = menuItem.actionUserData;
				if (actionUserData != null)
				{
					actionUserData(menuItem.element.userData);
				}
				bool isSingleSelectionDropdown = this.isSingleSelectionDropdown;
				if (isSingleSelectionDropdown)
				{
					this.Hide(true);
				}
			}
			bool flag2 = evt.pointerId != PointerId.mousePointerId;
			if (flag2)
			{
				this.m_MenuContainer.panel.PreventCompatibilityMouseEvents(evt.pointerId);
			}
			evt.StopPropagation();
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x00023E7C File Offset: 0x0002207C
		private void OnFocusOut(FocusOutEvent evt)
		{
			bool flag = !this.m_ScrollView.ContainsPoint(this.m_MousePosition);
			if (flag)
			{
				this.Hide(false);
			}
			else
			{
				this.m_MenuContainer.schedule.Execute(new Action(this.contentContainer.Focus));
			}
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x00023ED4 File Offset: 0x000220D4
		private void OnParentResized(GeometryChangedEvent evt)
		{
			bool closeOnParentResize = this.closeOnParentResize;
			if (closeOnParentResize)
			{
				this.Hide(true);
			}
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x00023EF8 File Offset: 0x000220F8
		private void UpdateSelection(VisualElement target)
		{
			bool flag = !this.m_ScrollView.ContainsPoint(this.m_MousePosition);
			if (flag)
			{
				int selectedIndex = this.GetSelectedIndex();
				bool flag2 = selectedIndex >= 0;
				if (flag2)
				{
					this.m_Items[selectedIndex].element.pseudoStates &= ~PseudoStates.Hover;
				}
			}
			else
			{
				bool flag3 = target == null;
				if (!flag3)
				{
					bool flag4 = (target.pseudoStates & PseudoStates.Hover) != PseudoStates.Hover;
					if (flag4)
					{
						int selectedIndex2 = this.GetSelectedIndex();
						bool flag5 = selectedIndex2 >= 0;
						if (flag5)
						{
							this.m_Items[selectedIndex2].element.pseudoStates &= ~PseudoStates.Hover;
						}
						target.pseudoStates |= PseudoStates.Hover;
					}
				}
			}
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x00023FBC File Offset: 0x000221BC
		private void ChangeSelectedIndex(int newIndex, int previousIndex)
		{
			bool flag = previousIndex >= 0 && previousIndex < this.m_Items.Count;
			if (flag)
			{
				this.m_Items[previousIndex].element.pseudoStates &= ~PseudoStates.Hover;
			}
			bool flag2 = newIndex >= 0 && newIndex < this.m_Items.Count;
			if (flag2)
			{
				this.m_Items[newIndex].element.pseudoStates |= PseudoStates.Hover;
				this.m_ScrollView.ScrollTo(this.m_Items[newIndex].element);
			}
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x0002405C File Offset: 0x0002225C
		private int GetSelectedIndex()
		{
			for (int i = 0; i < this.m_Items.Count; i++)
			{
				bool flag = (this.m_Items[i].element.pseudoStates & PseudoStates.Hover) == PseudoStates.Hover;
				if (flag)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x000240B0 File Offset: 0x000222B0
		public void AddItem(string itemName, bool isChecked, Action action)
		{
			GenericDropdownMenu.MenuItem menuItem = this.AddItem(itemName, isChecked, true, null);
			bool flag = menuItem != null;
			if (flag)
			{
				menuItem.action = action;
			}
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x000240DC File Offset: 0x000222DC
		public void AddItem(string itemName, bool isChecked, Action<object> action, object data)
		{
			GenericDropdownMenu.MenuItem menuItem = this.AddItem(itemName, isChecked, true, data);
			bool flag = menuItem != null;
			if (flag)
			{
				menuItem.actionUserData = action;
			}
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x00024107 File Offset: 0x00022307
		public void AddDisabledItem(string itemName, bool isChecked)
		{
			this.AddItem(itemName, isChecked, false, null);
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x00024118 File Offset: 0x00022318
		public void AddSeparator(string path)
		{
			VisualElement visualElement = new VisualElement();
			visualElement.AddToClassList(GenericDropdownMenu.separatorUssClassName);
			visualElement.pickingMode = PickingMode.Ignore;
			this.m_ScrollView.Add(visualElement);
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x00024150 File Offset: 0x00022350
		private GenericDropdownMenu.MenuItem AddItem(string itemName, bool isChecked, bool isEnabled, object data = null)
		{
			bool flag = string.IsNullOrEmpty(itemName) || itemName.EndsWith("/");
			GenericDropdownMenu.MenuItem menuItem;
			if (flag)
			{
				this.AddSeparator(itemName);
				menuItem = null;
			}
			else
			{
				for (int i = 0; i < this.m_Items.Count; i++)
				{
					bool flag2 = itemName == this.m_Items[i].name;
					if (flag2)
					{
						return null;
					}
				}
				VisualElement visualElement = new VisualElement();
				visualElement.AddToClassList(GenericDropdownMenu.itemUssClassName);
				visualElement.SetEnabled(isEnabled);
				visualElement.userData = data;
				VisualElement visualElement2 = new VisualElement
				{
					pickingMode = PickingMode.Ignore
				};
				visualElement2.AddToClassList(GenericDropdownMenu.itemContentUssClassName);
				VisualElement visualElement3 = new VisualElement();
				visualElement3.AddToClassList(GenericDropdownMenu.checkmarkUssClassName);
				visualElement3.pickingMode = PickingMode.Ignore;
				visualElement2.Add(visualElement3);
				if (isChecked)
				{
					visualElement.pseudoStates |= PseudoStates.Checked;
				}
				Label label = new Label(itemName);
				label.AddToClassList(GenericDropdownMenu.labelUssClassName);
				label.pickingMode = PickingMode.Ignore;
				visualElement2.Add(label);
				visualElement.Add(visualElement2);
				this.m_ScrollView.Add(visualElement);
				GenericDropdownMenu.MenuItem menuItem2 = new GenericDropdownMenu.MenuItem
				{
					name = itemName,
					element = visualElement
				};
				this.m_Items.Add(menuItem2);
				menuItem = menuItem2;
			}
			return menuItem;
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x000242AC File Offset: 0x000224AC
		internal void UpdateItem(string itemName, bool isChecked)
		{
			GenericDropdownMenu.MenuItem menuItem = this.m_Items.Find((GenericDropdownMenu.MenuItem x) => x.name == itemName);
			bool flag = menuItem == null;
			if (!flag)
			{
				if (isChecked)
				{
					menuItem.element.pseudoStates |= PseudoStates.Checked;
				}
				else
				{
					menuItem.element.pseudoStates &= ~PseudoStates.Checked;
				}
			}
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x00024320 File Offset: 0x00022520
		public void DropDown(Rect position, VisualElement targetElement = null, bool anchored = false)
		{
			bool flag = targetElement == null;
			if (flag)
			{
				Debug.LogError("VisualElement Generic Menu needs a target to find a root to attach to.");
			}
			else
			{
				this.m_TargetElement = targetElement;
				this.m_TargetElement.RegisterCallback<DetachFromPanelEvent>(new EventCallback<DetachFromPanelEvent>(this.OnTargetElementDetachFromPanel), TrickleDown.NoTrickleDown);
				this.m_PanelRootVisualContainer = this.m_TargetElement.GetRootVisualContainer(false);
				bool flag2 = this.m_PanelRootVisualContainer == null;
				if (flag2)
				{
					Debug.LogError("Could not find rootVisualContainer...");
				}
				else
				{
					this.m_PanelRootVisualContainer.Add(this.m_MenuContainer);
					this.m_MenuContainer.style.left = this.m_PanelRootVisualContainer.layout.x;
					this.m_MenuContainer.style.top = this.m_PanelRootVisualContainer.layout.y;
					this.m_MenuContainer.style.width = this.m_PanelRootVisualContainer.layout.width;
					this.m_MenuContainer.style.height = this.m_PanelRootVisualContainer.layout.height;
					this.m_MenuContainer.style.fontSize = this.m_TargetElement.computedStyle.fontSize;
					this.m_MenuContainer.style.unityFont = this.m_TargetElement.computedStyle.unityFont;
					this.m_MenuContainer.style.unityFontDefinition = this.m_TargetElement.computedStyle.unityFontDefinition;
					Rect rect = this.m_PanelRootVisualContainer.WorldToLocal(position);
					this.m_PositionTop = rect.y + position.height - this.m_PanelRootVisualContainer.layout.y;
					this.m_PositionLeft = rect.x - this.m_PanelRootVisualContainer.layout.x;
					this.m_OuterContainer.style.left = this.m_PositionLeft;
					this.m_OuterContainer.style.top = this.m_PositionTop;
					this.m_OuterContainer.style.maxHeight = Length.None();
					this.m_OuterContainer.style.maxWidth = Length.None();
					this.m_DesiredRect = (anchored ? position : Rect.zero);
					this.m_MenuContainer.schedule.Execute(new Action(this.contentContainer.Focus));
					this.m_ShownAboveTarget = false;
					this.EnsureVisibilityInParent();
					bool flag3 = targetElement != null;
					if (flag3)
					{
						targetElement.pseudoStates |= PseudoStates.Active;
					}
				}
			}
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x000245DF File Offset: 0x000227DF
		public void DropDown(Rect position, VisualElement targetElement = null, bool anchored = false, bool fitContentWidthIfAnchored = false)
		{
			this.m_FitContentWidth = anchored && fitContentWidthIfAnchored;
			this.m_OuterContainer.EnableInClassList(GenericDropdownMenu.contentWidthUssClassName, this.m_FitContentWidth);
			this.DropDown(position, targetElement, anchored);
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x0002460D File Offset: 0x0002280D
		private void OnTargetElementDetachFromPanel(DetachFromPanelEvent evt)
		{
			this.Hide(false);
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x00024618 File Offset: 0x00022818
		private void OnContainerGeometryChanged(GeometryChangedEvent evt)
		{
			this.EnsureVisibilityInParent();
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x00024622 File Offset: 0x00022822
		private void OnInitialDisplay(GeometryChangedEvent evt)
		{
			this.m_ContentWidth = this.GetLargestItemWidth() + 20f;
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x00024638 File Offset: 0x00022838
		private void EnsureVisibilityInParent()
		{
			bool flag = this.m_PanelRootVisualContainer != null && !float.IsNaN(this.m_OuterContainer.layout.width) && !float.IsNaN(this.m_OuterContainer.layout.height);
			if (flag)
			{
				bool flag2 = this.m_DesiredRect == Rect.zero;
				if (flag2)
				{
					float num = Math.Max(0f, Mathf.Min(this.m_PositionLeft, this.m_PanelRootVisualContainer.layout.width - this.m_OuterContainer.layout.width));
					float num2 = Mathf.Min(this.m_PositionTop, Mathf.Max(0f, this.m_PanelRootVisualContainer.layout.height - this.m_OuterContainer.layout.height));
					this.m_OuterContainer.style.left = num;
					this.m_OuterContainer.style.top = num2;
				}
				else
				{
					float num3 = this.m_ContentWidth;
					bool isVerticalScrollDisplayed = this.m_ScrollView.isVerticalScrollDisplayed;
					if (isVerticalScrollDisplayed)
					{
						num3 += Mathf.Ceil(this.m_ScrollView.verticalScroller.computedStyle.width.value);
					}
					num3 = (this.m_FitContentWidth ? num3 : this.m_DesiredRect.width);
					this.m_OuterContainer.style.width = num3;
					float num4 = this.m_PanelRootVisualContainer.layout.width - this.m_PositionLeft;
					bool flag3 = num4 <= num3;
					if (flag3)
					{
						this.m_PositionLeft -= num3 - num4 + 2f;
					}
					this.m_PositionLeft = Math.Max(this.m_PositionLeft, 0f);
					bool flag4 = this.m_PositionLeft == 0f;
					if (flag4)
					{
						this.m_OuterContainer.style.maxWidth = Math.Min(this.m_PanelRootVisualContainer.layout.width, num3);
					}
					this.m_OuterContainer.style.left = this.m_PositionLeft;
				}
				Rect rect = this.m_MenuContainer.WorldToLocal(this.m_TargetElement.worldBound);
				float num5 = ((this.m_Items.Count == 0) ? 20f : (this.m_Items[0].element.layout.height + 20f));
				float height = this.m_OuterContainer.layout.height;
				float y = rect.y;
				float y2 = this.m_OuterContainer.worldBound.y;
				float num6 = (this.m_ShownAboveTarget ? (y - y2) : (this.m_PanelRootVisualContainer.worldBound.height - y2));
				float num7 = (this.m_ShownAboveTarget ? (this.m_PanelRootVisualContainer.worldBound.height - y2) : y);
				bool flag5 = num6 < height;
				bool flag6 = flag5 && num7 > num6;
				if (flag6)
				{
					this.m_PositionTop = this.m_OuterContainer.RoundToPanelPixelSize(Math.Max(y - height, 0f));
					this.m_OuterContainer.style.maxHeight = ((this.m_PositionTop == 0f) ? Math.Max(y, num5) : Length.None());
					this.m_OuterContainer.style.top = this.m_PositionTop;
					this.m_ShownAboveTarget = true;
				}
				else
				{
					bool flag7 = flag5;
					if (flag7)
					{
						bool flag8 = num6 < num5;
						if (flag8)
						{
							this.m_OuterContainer.style.maxHeight = num5;
							this.m_PositionTop = this.m_PanelRootVisualContainer.worldBound.height - num5;
						}
						else
						{
							this.m_OuterContainer.style.maxHeight = num6;
						}
						this.m_OuterContainer.style.top = this.m_PositionTop;
					}
				}
			}
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x00024A7C File Offset: 0x00022C7C
		private float GetLargestItemWidth()
		{
			float num = 0f;
			bool flag = this.m_Items.Count == 0 && this.m_ScrollView.contentContainer.childCount > 0;
			float num2;
			if (flag)
			{
				List<GenericDropdownMenu.MenuItem> list = CollectionPool<List<GenericDropdownMenu.MenuItem>, GenericDropdownMenu.MenuItem>.Get();
				foreach (VisualElement visualElement in this.m_ScrollView.contentContainer.Children())
				{
					list.Add(new GenericDropdownMenu.MenuItem
					{
						element = visualElement
					});
					num = Math.Max(num, visualElement.layout.width);
				}
				this.m_Items.AddRange(list);
				CollectionPool<List<GenericDropdownMenu.MenuItem>, GenericDropdownMenu.MenuItem>.Release(list);
				num2 = num;
			}
			else
			{
				foreach (GenericDropdownMenu.MenuItem menuItem in this.m_Items)
				{
					VisualElement visualElement2 = menuItem.element.Q(null, new string[] { GenericDropdownMenu.itemContentUssClassName });
					float num3 = ((visualElement2 != null) ? visualElement2.layout.width : menuItem.element.layout.width);
					num = Math.Max(num, num3);
				}
				num2 = num;
			}
			return num2;
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x00024CA4 File Offset: 0x00022EA4
		[CompilerGenerated]
		private void <Apply>g__UpdateSelectionDown|48_0(int newIndex, ref GenericDropdownMenu.<>c__DisplayClass48_0 A_2)
		{
			while (newIndex < this.m_Items.Count)
			{
				bool enabledSelf = this.m_Items[newIndex].element.enabledSelf;
				if (enabledSelf)
				{
					this.ChangeSelectedIndex(newIndex, A_2.selectedIndex);
					break;
				}
				newIndex++;
			}
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x00024CF8 File Offset: 0x00022EF8
		[CompilerGenerated]
		private void <Apply>g__UpdateSelectionUp|48_1(int newIndex, ref GenericDropdownMenu.<>c__DisplayClass48_0 A_2)
		{
			while (newIndex >= 0)
			{
				bool enabledSelf = this.m_Items[newIndex].element.enabledSelf;
				if (enabledSelf)
				{
					this.ChangeSelectedIndex(newIndex, A_2.selectedIndex);
					break;
				}
				newIndex--;
			}
		}

		// Token: 0x0400048D RID: 1165
		public static readonly string ussClassName = "unity-base-dropdown";

		// Token: 0x0400048E RID: 1166
		public static readonly string itemUssClassName = GenericDropdownMenu.ussClassName + "__item";

		// Token: 0x0400048F RID: 1167
		public static readonly string itemContentUssClassName = GenericDropdownMenu.ussClassName + "__item-content";

		// Token: 0x04000490 RID: 1168
		public static readonly string labelUssClassName = GenericDropdownMenu.ussClassName + "__label";

		// Token: 0x04000491 RID: 1169
		public static readonly string containerInnerUssClassName = GenericDropdownMenu.ussClassName + "__container-inner";

		// Token: 0x04000492 RID: 1170
		public static readonly string containerOuterUssClassName = GenericDropdownMenu.ussClassName + "__container-outer";

		// Token: 0x04000493 RID: 1171
		public static readonly string checkmarkUssClassName = GenericDropdownMenu.ussClassName + "__checkmark";

		// Token: 0x04000494 RID: 1172
		public static readonly string separatorUssClassName = GenericDropdownMenu.ussClassName + "__separator";

		// Token: 0x04000495 RID: 1173
		public static readonly string contentWidthUssClassName = GenericDropdownMenu.ussClassName + "--content-width-menu";

		// Token: 0x04000496 RID: 1174
		private const float k_MenuItemPadding = 20f;

		// Token: 0x04000497 RID: 1175
		private const float k_MenuPadding = 2f;

		// Token: 0x04000498 RID: 1176
		private List<GenericDropdownMenu.MenuItem> m_Items = new List<GenericDropdownMenu.MenuItem>();

		// Token: 0x04000499 RID: 1177
		private VisualElement m_MenuContainer;

		// Token: 0x0400049A RID: 1178
		private VisualElement m_OuterContainer;

		// Token: 0x0400049B RID: 1179
		private ScrollView m_ScrollView;

		// Token: 0x0400049C RID: 1180
		private VisualElement m_PanelRootVisualContainer;

		// Token: 0x0400049D RID: 1181
		private VisualElement m_TargetElement;

		// Token: 0x0400049E RID: 1182
		private Rect m_DesiredRect;

		// Token: 0x0400049F RID: 1183
		private KeyboardNavigationManipulator m_NavigationManipulator;

		// Token: 0x040004A0 RID: 1184
		private float m_PositionTop;

		// Token: 0x040004A1 RID: 1185
		private float m_PositionLeft;

		// Token: 0x040004A2 RID: 1186
		private float m_ContentWidth;

		// Token: 0x040004A3 RID: 1187
		private bool m_FitContentWidth;

		// Token: 0x040004A4 RID: 1188
		private bool m_ShownAboveTarget;

		// Token: 0x040004A7 RID: 1191
		private Vector2 m_MousePosition;

		// Token: 0x020000FC RID: 252
		internal class MenuItem
		{
			// Token: 0x040004A8 RID: 1192
			public string name;

			// Token: 0x040004A9 RID: 1193
			public VisualElement element;

			// Token: 0x040004AA RID: 1194
			public Action action;

			// Token: 0x040004AB RID: 1195
			public Action<object> actionUserData;
		}
	}
}
