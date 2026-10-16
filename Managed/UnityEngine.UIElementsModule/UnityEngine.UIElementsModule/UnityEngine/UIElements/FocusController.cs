using System;
using System.Collections.Generic;
using UnityEngine.Bindings;
using UnityEngine.Pool;

namespace UnityEngine.UIElements
{
	// Token: 0x020002BA RID: 698
	public class FocusController
	{
		// Token: 0x060013E7 RID: 5095 RVA: 0x0004FACF File Offset: 0x0004DCCF
		public FocusController(IFocusRing focusRing)
		{
			this.focusRing = focusRing;
			this.imguiKeyboardControl = 0;
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x060013E8 RID: 5096 RVA: 0x0004FAFA File Offset: 0x0004DCFA
		private IFocusRing focusRing { get; }

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x060013E9 RID: 5097 RVA: 0x0004FB02 File Offset: 0x0004DD02
		// (set) Token: 0x060013EA RID: 5098 RVA: 0x0004FB0C File Offset: 0x0004DD0C
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal TextElement selectedTextElement
		{
			get
			{
				return this.m_SelectedTextElement;
			}
			set
			{
				bool flag = this.m_SelectedTextElement == value;
				if (!flag)
				{
					TextElement selectedTextElement = this.m_SelectedTextElement;
					if (selectedTextElement != null)
					{
						selectedTextElement.selection.SelectNone();
					}
					this.m_SelectedTextElement = value;
				}
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x060013EB RID: 5099 RVA: 0x0004FB48 File Offset: 0x0004DD48
		public Focusable focusedElement
		{
			get
			{
				Focusable retargetedFocusedElement = this.GetRetargetedFocusedElement(null);
				return this.IsLocalElement(retargetedFocusedElement) ? retargetedFocusedElement : null;
			}
		}

		// Token: 0x060013EC RID: 5100 RVA: 0x0004FB70 File Offset: 0x0004DD70
		public void IgnoreEvent(EventBase evt)
		{
			evt.processedByFocusController = true;
			IMouseEventInternal mouseEventInternal = evt as IMouseEventInternal;
			EventBase eventBase;
			bool flag;
			if (mouseEventInternal != null)
			{
				eventBase = mouseEventInternal.sourcePointerEvent as EventBase;
				flag = eventBase != null;
			}
			else
			{
				flag = false;
			}
			bool flag2 = flag;
			if (flag2)
			{
				eventBase.processedByFocusController = true;
			}
		}

		// Token: 0x060013ED RID: 5101 RVA: 0x0004FBB0 File Offset: 0x0004DDB0
		internal bool IsFocused(Focusable f)
		{
			bool flag = !this.IsLocalElement(f);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				foreach (FocusController.FocusedElement focusedElement in this.m_FocusedElements)
				{
					bool flag3 = focusedElement.m_FocusedElement == f;
					if (flag3)
					{
						return true;
					}
				}
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x060013EE RID: 5102 RVA: 0x0004FC2C File Offset: 0x0004DE2C
		internal Focusable GetRetargetedFocusedElement(VisualElement retargetAgainst)
		{
			VisualElement visualElement = ((retargetAgainst != null) ? retargetAgainst.hierarchy.parent : null);
			bool flag = visualElement == null;
			if (flag)
			{
				bool flag2 = this.m_FocusedElements.Count > 0;
				if (flag2)
				{
					return this.m_FocusedElements[this.m_FocusedElements.Count - 1].m_FocusedElement;
				}
			}
			else
			{
				while (!visualElement.isCompositeRoot && visualElement.hierarchy.parent != null)
				{
					visualElement = visualElement.hierarchy.parent;
				}
				foreach (FocusController.FocusedElement focusedElement in this.m_FocusedElements)
				{
					bool flag3 = focusedElement.m_SubTreeRoot == visualElement;
					if (flag3)
					{
						return focusedElement.m_FocusedElement;
					}
				}
			}
			return null;
		}

		// Token: 0x060013EF RID: 5103 RVA: 0x0004FD34 File Offset: 0x0004DF34
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal Focusable GetLeafFocusedElement()
		{
			bool flag = this.m_FocusedElements.Count > 0;
			Focusable focusable;
			if (flag)
			{
				VisualElement focusedElement = this.m_FocusedElements[0].m_FocusedElement;
				focusable = (this.IsLocalElement(focusedElement) ? focusedElement : null);
			}
			else
			{
				focusable = null;
			}
			return focusable;
		}

		// Token: 0x060013F0 RID: 5104 RVA: 0x0004FD7C File Offset: 0x0004DF7C
		private bool IsLocalElement(Focusable f)
		{
			return ((f != null) ? f.focusController : null) == this;
		}

		// Token: 0x060013F1 RID: 5105 RVA: 0x0004FD9D File Offset: 0x0004DF9D
		internal void ClearPendingFocusEvents()
		{
			this.m_PendingFocusCount = 0;
			this.m_LastPendingFocusedElement = null;
		}

		// Token: 0x060013F2 RID: 5106 RVA: 0x0004FDB0 File Offset: 0x0004DFB0
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal bool IsPendingFocus(Focusable f)
		{
			for (VisualElement visualElement = this.m_LastPendingFocusedElement as VisualElement; visualElement != null; visualElement = visualElement.hierarchy.parent)
			{
				bool flag = f == visualElement;
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060013F3 RID: 5107 RVA: 0x0004FDF8 File Offset: 0x0004DFF8
		internal void SetFocusToLastFocusedElement()
		{
			bool flag = this.m_LastFocusedElement != null && !(this.m_LastFocusedElement is IMGUIContainer);
			if (flag)
			{
				this.m_LastFocusedElement.Focus();
			}
		}

		// Token: 0x060013F4 RID: 5108 RVA: 0x0004FE34 File Offset: 0x0004E034
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal void BlurLastFocusedElement()
		{
			this.selectedTextElement = null;
			bool flag = this.m_LastFocusedElement != null && !(this.m_LastFocusedElement is IMGUIContainer);
			if (flag)
			{
				Focusable lastFocusedElement = this.m_LastFocusedElement;
				this.m_LastFocusedElement.Blur();
				this.m_LastFocusedElement = lastFocusedElement;
			}
		}

		// Token: 0x060013F5 RID: 5109 RVA: 0x0004FE86 File Offset: 0x0004E086
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal void DoFocusChange(Focusable f)
		{
			this.m_FocusedElements.Clear();
			FocusController.GetFocusTargets(f, this.m_FocusedElements);
		}

		// Token: 0x060013F6 RID: 5110 RVA: 0x0004FEA4 File Offset: 0x0004E0A4
		internal void ProcessPendingFocusChange(Focusable f)
		{
			this.m_PendingFocusCount--;
			bool flag = this.m_PendingFocusCount == 0;
			if (flag)
			{
				this.m_LastPendingFocusedElement = null;
			}
			foreach (FocusController.FocusedElement focusedElement in this.m_FocusedElements)
			{
				focusedElement.m_FocusedElement.pseudoStates &= ~PseudoStates.Focus;
			}
			this.DoFocusChange(f);
			foreach (FocusController.FocusedElement focusedElement2 in this.m_FocusedElements)
			{
				focusedElement2.m_FocusedElement.pseudoStates |= PseudoStates.Focus;
			}
		}

		// Token: 0x060013F7 RID: 5111 RVA: 0x0004FF88 File Offset: 0x0004E188
		private static void GetFocusTargets(Focusable f, List<FocusController.FocusedElement> outTargets)
		{
			VisualElement visualElement = f as VisualElement;
			for (VisualElement visualElement2 = visualElement; visualElement2 != null; visualElement2 = visualElement2.hierarchy.parent)
			{
				bool flag = visualElement2.hierarchy.parent == null || visualElement2.isCompositeRoot;
				if (flag)
				{
					outTargets.Add(new FocusController.FocusedElement
					{
						m_SubTreeRoot = visualElement2,
						m_FocusedElement = visualElement
					});
					visualElement = visualElement2;
				}
			}
		}

		// Token: 0x060013F8 RID: 5112 RVA: 0x00050000 File Offset: 0x0004E200
		internal Focusable FocusNextInDirection(Focusable currentFocusable, FocusChangeDirection direction)
		{
			Focusable nextFocusable = this.focusRing.GetNextFocusable(currentFocusable, direction);
			direction.ApplyTo(this, nextFocusable);
			return nextFocusable;
		}

		// Token: 0x060013F9 RID: 5113 RVA: 0x0005002C File Offset: 0x0004E22C
		private void AboutToReleaseFocus(Focusable focusable, Focusable willGiveFocusTo, FocusChangeDirection direction, DispatchMode dispatchMode)
		{
			using (FocusOutEvent pooled = FocusEventBase<FocusOutEvent>.GetPooled(focusable, willGiveFocusTo, direction, this, false))
			{
				focusable.SendEvent(pooled, dispatchMode);
			}
		}

		// Token: 0x060013FA RID: 5114 RVA: 0x00050070 File Offset: 0x0004E270
		private void ReleaseFocus(Focusable focusable, Focusable willGiveFocusTo, FocusChangeDirection direction, DispatchMode dispatchMode)
		{
			List<FocusController.FocusedElement> list;
			using (CollectionPool<List<FocusController.FocusedElement>, FocusController.FocusedElement>.Get(out list))
			{
				FocusController.GetFocusTargets(focusable, list);
				foreach (FocusController.FocusedElement focusedElement in list)
				{
					using (BlurEvent pooled = FocusEventBase<BlurEvent>.GetPooled(focusedElement.m_FocusedElement, willGiveFocusTo, direction, this, false))
					{
						pooled.target = focusedElement.m_FocusedElement;
						focusable.SendEvent(pooled, dispatchMode);
					}
				}
			}
		}

		// Token: 0x060013FB RID: 5115 RVA: 0x00050130 File Offset: 0x0004E330
		private void AboutToGrabFocus(Focusable focusable, Focusable willTakeFocusFrom, FocusChangeDirection direction, DispatchMode dispatchMode)
		{
			using (FocusInEvent pooled = FocusEventBase<FocusInEvent>.GetPooled(focusable, willTakeFocusFrom, direction, this, false))
			{
				focusable.SendEvent(pooled, dispatchMode);
			}
		}

		// Token: 0x060013FC RID: 5116 RVA: 0x00050174 File Offset: 0x0004E374
		private void GrabFocus(Focusable focusable, Focusable willTakeFocusFrom, FocusChangeDirection direction, bool bIsFocusDelegated, DispatchMode dispatchMode)
		{
			List<FocusController.FocusedElement> list;
			using (CollectionPool<List<FocusController.FocusedElement>, FocusController.FocusedElement>.Get(out list))
			{
				FocusController.GetFocusTargets(focusable, list);
				foreach (FocusController.FocusedElement focusedElement in list)
				{
					using (FocusEvent pooled = FocusEventBase<FocusEvent>.GetPooled(focusedElement.m_FocusedElement, willTakeFocusFrom, direction, this, bIsFocusDelegated))
					{
						pooled.target = focusedElement.m_FocusedElement;
						focusable.SendEvent(pooled, dispatchMode);
					}
				}
			}
		}

		// Token: 0x060013FD RID: 5117 RVA: 0x00050238 File Offset: 0x0004E438
		internal void Blur(Focusable focusable, bool bIsFocusDelegated = false, DispatchMode dispatchMode = DispatchMode.Default)
		{
			bool flag = ((this.m_PendingFocusCount > 0) ? this.IsPendingFocus(focusable) : this.IsFocused(focusable));
			bool flag2 = flag;
			if (flag2)
			{
				this.SwitchFocus(null, bIsFocusDelegated, dispatchMode);
			}
		}

		// Token: 0x060013FE RID: 5118 RVA: 0x00050271 File Offset: 0x0004E471
		internal void SwitchFocus(Focusable newFocusedElement, bool bIsFocusDelegated = false, DispatchMode dispatchMode = DispatchMode.Default)
		{
			this.SwitchFocus(newFocusedElement, FocusChangeDirection.unspecified, bIsFocusDelegated, dispatchMode);
		}

		// Token: 0x060013FF RID: 5119 RVA: 0x00050284 File Offset: 0x0004E484
		internal void SwitchFocus(Focusable newFocusedElement, FocusChangeDirection direction, bool bIsFocusDelegated = false, DispatchMode dispatchMode = DispatchMode.Default)
		{
			this.m_LastFocusedElement = newFocusedElement;
			Focusable focusable = ((this.m_PendingFocusCount > 0) ? this.m_LastPendingFocusedElement : this.GetLeafFocusedElement());
			bool flag = focusable == newFocusedElement;
			if (!flag)
			{
				VisualElement visualElement = newFocusedElement as VisualElement;
				IPanel panel;
				bool flag2;
				if (visualElement != null && newFocusedElement.canGrabFocus)
				{
					panel = visualElement.panel;
					flag2 = panel != null;
				}
				else
				{
					flag2 = false;
				}
				bool flag3 = flag2;
				if (flag3)
				{
					Focusable focusable2 = ((visualElement != null) ? visualElement.RetargetElement(focusable as VisualElement) : null) ?? newFocusedElement;
					VisualElement visualElement2 = focusable as VisualElement;
					Focusable focusable3 = ((visualElement2 != null) ? visualElement2.RetargetElement(visualElement) : null) ?? focusable;
					this.m_LastPendingFocusedElement = newFocusedElement;
					this.m_PendingFocusCount++;
					using (new EventDispatcherGate(panel.dispatcher))
					{
						bool flag4 = focusable != null;
						if (flag4)
						{
							this.AboutToReleaseFocus(focusable, focusable2, direction, dispatchMode);
						}
						this.AboutToGrabFocus(newFocusedElement, focusable3, direction, dispatchMode);
						bool flag5 = focusable != null;
						if (flag5)
						{
							this.ReleaseFocus(focusable, focusable2, direction, dispatchMode);
						}
						this.GrabFocus(newFocusedElement, focusable3, direction, bIsFocusDelegated, dispatchMode);
					}
				}
				else
				{
					VisualElement visualElement3 = focusable as VisualElement;
					bool flag6 = visualElement3 != null;
					if (flag6)
					{
						BaseVisualElementPanel elementPanel = visualElement3.elementPanel;
						bool flag7 = elementPanel != null;
						if (flag7)
						{
							this.m_LastPendingFocusedElement = null;
							this.m_PendingFocusCount++;
							using (new EventDispatcherGate(elementPanel.dispatcher))
							{
								this.AboutToReleaseFocus(focusable, null, direction, dispatchMode);
								this.ReleaseFocus(focusable, null, direction, dispatchMode);
							}
						}
						else
						{
							this.ProcessPendingFocusChange(null);
						}
					}
				}
			}
		}

		// Token: 0x06001400 RID: 5120 RVA: 0x00050444 File Offset: 0x0004E644
		internal void SwitchFocusOnEvent(Focusable currentFocusable, EventBase e)
		{
			bool processedByFocusController = e.processedByFocusController;
			if (!processedByFocusController)
			{
				using (FocusChangeDirection focusChangeDirection = this.focusRing.GetFocusChangeDirection(currentFocusable, e))
				{
					bool flag = focusChangeDirection != FocusChangeDirection.none;
					if (flag)
					{
						this.FocusNextInDirection(currentFocusable, focusChangeDirection);
						e.processedByFocusController = true;
					}
				}
			}
		}

		// Token: 0x06001401 RID: 5121 RVA: 0x000504B0 File Offset: 0x0004E6B0
		internal void ReevaluateFocus()
		{
			VisualElement visualElement = this.focusedElement as VisualElement;
			bool flag = visualElement != null;
			if (flag)
			{
				bool flag2 = !visualElement.areAncestorsAndSelfDisplayed || !visualElement.visible;
				if (flag2)
				{
					visualElement.Blur();
				}
			}
		}

		// Token: 0x06001402 RID: 5122 RVA: 0x000504F4 File Offset: 0x0004E6F4
		internal bool GetFocusableParentForPointerEvent(Focusable target, out Focusable effectiveTarget)
		{
			bool flag = target == null || !target.focusable;
			bool flag2;
			if (flag)
			{
				effectiveTarget = target;
				flag2 = target != null;
			}
			else
			{
				effectiveTarget = target;
				for (;;)
				{
					VisualElement visualElement = effectiveTarget as VisualElement;
					bool flag3 = visualElement != null && (!visualElement.enabledInHierarchy || !visualElement.focusable || !visualElement.isEligibleToReceiveFocusFromDisabledChild) && visualElement.hierarchy.parent != null;
					if (!flag3)
					{
						break;
					}
					effectiveTarget = visualElement.hierarchy.parent;
				}
				flag2 = !this.IsFocused(effectiveTarget);
			}
			return flag2;
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06001403 RID: 5123 RVA: 0x00050583 File Offset: 0x0004E783
		// (set) Token: 0x06001404 RID: 5124 RVA: 0x0005058B File Offset: 0x0004E78B
		internal int imguiKeyboardControl { get; set; }

		// Token: 0x06001405 RID: 5125 RVA: 0x00050594 File Offset: 0x0004E794
		internal void SyncIMGUIFocus(int imguiKeyboardControlID, Focusable imguiContainerHavingKeyboardControl, bool forceSwitch)
		{
			this.imguiKeyboardControl = imguiKeyboardControlID;
			bool flag = forceSwitch || this.imguiKeyboardControl != 0;
			if (flag)
			{
				this.SwitchFocus(imguiContainerHavingKeyboardControl, FocusChangeDirection.unspecified, false, DispatchMode.Default);
			}
			else
			{
				this.SwitchFocus(null, FocusChangeDirection.unspecified, false, DispatchMode.Default);
			}
		}

		// Token: 0x04000ACD RID: 2765
		private TextElement m_SelectedTextElement;

		// Token: 0x04000ACE RID: 2766
		private List<FocusController.FocusedElement> m_FocusedElements = new List<FocusController.FocusedElement>();

		// Token: 0x04000ACF RID: 2767
		private Focusable m_LastFocusedElement;

		// Token: 0x04000AD0 RID: 2768
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal Focusable m_LastPendingFocusedElement;

		// Token: 0x04000AD1 RID: 2769
		private int m_PendingFocusCount = 0;

		// Token: 0x020002BB RID: 699
		private struct FocusedElement
		{
			// Token: 0x04000AD3 RID: 2771
			public VisualElement m_SubTreeRoot;

			// Token: 0x04000AD4 RID: 2772
			public VisualElement m_FocusedElement;
		}
	}
}
