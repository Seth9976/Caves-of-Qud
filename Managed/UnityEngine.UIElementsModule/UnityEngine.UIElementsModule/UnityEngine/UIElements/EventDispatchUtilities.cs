using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine.Pool;
using UnityEngine.UIElements.Experimental;

namespace UnityEngine.UIElements
{
	// Token: 0x0200023C RID: 572
	internal static class EventDispatchUtilities
	{
		// Token: 0x060010F4 RID: 4340 RVA: 0x00049244 File Offset: 0x00047444
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void PropagateEvent(EventBase evt, [NotNull] BaseVisualElementPanel panel, [NotNull] VisualElement target, bool isCapturingTarget)
		{
			IPointerEventInternal pointerEventInternal = evt as IPointerEventInternal;
			EventBase eventBase = ((pointerEventInternal != null) ? pointerEventInternal.compatibilityMouseEvent : null) as EventBase;
			bool flag = eventBase != null;
			if (flag)
			{
				EventDispatchUtilities.HandleEventAcrossPropagationPathWithCompatibilityEvent(evt, eventBase, panel, target, isCapturingTarget);
			}
			else
			{
				EventDispatchUtilities.HandleEventAcrossPropagationPath(evt, panel, target, isCapturingTarget);
			}
		}

		// Token: 0x060010F5 RID: 4341 RVA: 0x00049290 File Offset: 0x00047490
		public static void HandleEventAtTargetAndDefaultPhase(EventBase evt, [NotNull] BaseVisualElementPanel panel, [NotNull] VisualElement target)
		{
			int eventCategories = evt.eventCategories;
			bool flag = !target.HasSelfEventInterests(eventCategories) || evt.isPropagationStopped;
			if (!flag)
			{
				evt.currentTarget = target;
				try
				{
					IPointerEventInternal pointerEventInternal = evt as IPointerEventInternal;
					Debug.Assert(pointerEventInternal == null || pointerEventInternal.compatibilityMouseEvent == null, "!(evt is IPointerEventInternal pe) || pe.compatibilityMouseEvent == null");
					evt.propagationPhase = PropagationPhase.TrickleDown;
					bool flag2 = target.HasTrickleDownEventCallbacks(eventCategories);
					if (flag2)
					{
						EventDispatchUtilities.HandleEvent_TrickleDownCallbacks(evt, panel, target);
						bool isImmediatePropagationStopped = evt.isImmediatePropagationStopped;
						if (isImmediatePropagationStopped)
						{
							return;
						}
					}
					bool flag3 = target.HasTrickleDownHandleEvent(eventCategories);
					if (flag3)
					{
						EventDispatchUtilities.HandleEvent_TrickleDownHandleEvent(evt, panel, target, EventDispatchUtilities.Disabled(evt, target));
					}
					bool isPropagationStopped = evt.isPropagationStopped;
					if (!isPropagationStopped)
					{
						evt.propagationPhase = PropagationPhase.BubbleUp;
						bool flag4 = target.HasBubbleUpHandleEvent(eventCategories);
						if (flag4)
						{
							bool flag5 = EventDispatchUtilities.Disabled(evt, target);
							EventDispatchUtilities.HandleEvent_DefaultActionAtTarget(evt, panel, target, flag5);
							EventDispatchUtilities.HandleEvent_BubbleUpHandleEvent(evt, panel, target, flag5);
							EventDispatchUtilities.HandleEvent_DefaultAction(evt, panel, target, flag5);
							bool isImmediatePropagationStopped2 = evt.isImmediatePropagationStopped;
							if (isImmediatePropagationStopped2)
							{
								return;
							}
						}
						bool flag6 = target.HasBubbleUpEventCallbacks(eventCategories);
						if (flag6)
						{
							EventDispatchUtilities.HandleEvent_BubbleUpCallbacks(evt, panel, target);
						}
					}
				}
				finally
				{
					evt.currentTarget = null;
					evt.propagationPhase = PropagationPhase.None;
				}
			}
		}

		// Token: 0x060010F6 RID: 4342 RVA: 0x000493D4 File Offset: 0x000475D4
		private static void HandleEventAcrossPropagationPath(EventBase evt, [NotNull] BaseVisualElementPanel panel, [NotNull] VisualElement target, bool isCapturingTarget)
		{
			int eventCategories = evt.eventCategories;
			bool flag = !target.HasParentEventInterests(eventCategories) || evt.isPropagationStopped;
			if (!flag)
			{
				using (PropagationPaths propagationPaths = PropagationPaths.Build(target, evt, eventCategories))
				{
					try
					{
						Debug.Assert(!evt.dispatch, "Event is being dispatched recursively.");
						evt.dispatch = true;
						evt.propagationPhase = PropagationPhase.TrickleDown;
						int i = propagationPaths.trickleDownPath.Count - 1;
						bool flag2 = isCapturingTarget && i >= 0;
						if (flag2)
						{
							i = ((propagationPaths.trickleDownPath[0] == target) ? 0 : (-1));
						}
						while (i >= 0)
						{
							VisualElement visualElement = propagationPaths.trickleDownPath[i];
							evt.currentTarget = visualElement;
							bool flag3 = visualElement.HasTrickleDownEventCallbacks(eventCategories);
							if (flag3)
							{
								EventDispatchUtilities.HandleEvent_TrickleDownCallbacks(evt, panel, visualElement);
								bool isImmediatePropagationStopped = evt.isImmediatePropagationStopped;
								if (isImmediatePropagationStopped)
								{
									return;
								}
							}
							bool flag4 = visualElement.HasTrickleDownHandleEvent(eventCategories);
							if (flag4)
							{
								EventDispatchUtilities.HandleEvent_TrickleDownHandleEvent(evt, panel, visualElement, EventDispatchUtilities.Disabled(evt, visualElement));
							}
							bool isPropagationStopped = evt.isPropagationStopped;
							if (isPropagationStopped)
							{
								return;
							}
							i--;
						}
						evt.propagationPhase = PropagationPhase.BubbleUp;
						foreach (VisualElement visualElement2 in propagationPaths.bubbleUpPath)
						{
							evt.currentTarget = visualElement2;
							bool flag5 = visualElement2.HasBubbleUpHandleEvent(eventCategories);
							if (flag5)
							{
								EventDispatchUtilities.HandleEvent_BubbleUpAllDefaultActions(evt, panel, visualElement2, EventDispatchUtilities.Disabled(evt, visualElement2), isCapturingTarget);
								bool isImmediatePropagationStopped2 = evt.isImmediatePropagationStopped;
								if (isImmediatePropagationStopped2)
								{
									break;
								}
							}
							bool flag6 = visualElement2.HasBubbleUpEventCallbacks(eventCategories) && (!isCapturingTarget || visualElement2 == target);
							if (flag6)
							{
								EventDispatchUtilities.HandleEvent_BubbleUpCallbacks(evt, panel, visualElement2);
							}
							bool isPropagationStopped2 = evt.isPropagationStopped;
							if (isPropagationStopped2)
							{
								break;
							}
						}
					}
					finally
					{
						evt.currentTarget = null;
						evt.propagationPhase = PropagationPhase.None;
						evt.dispatch = false;
					}
				}
			}
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x00049624 File Offset: 0x00047824
		private static void HandleEventAcrossPropagationPathWithCompatibilityEvent(EventBase evt, [NotNull] EventBase compatibilityEvt, [NotNull] BaseVisualElementPanel panel, [NotNull] VisualElement target, bool isCapturingTarget)
		{
			int num = evt.eventCategories | compatibilityEvt.eventCategories;
			bool flag = !target.HasParentEventInterests(num) || evt.isPropagationStopped || compatibilityEvt.isPropagationStopped;
			if (!flag)
			{
				compatibilityEvt.elementTarget = target;
				compatibilityEvt.skipDisabledElements = evt.skipDisabledElements;
				using (PropagationPaths propagationPaths = PropagationPaths.Build(target, evt, num))
				{
					try
					{
						Debug.Assert(!evt.dispatch, "Event is being dispatched recursively.");
						evt.dispatch = true;
						evt.propagationPhase = PropagationPhase.TrickleDown;
						compatibilityEvt.propagationPhase = PropagationPhase.TrickleDown;
						int i = propagationPaths.trickleDownPath.Count - 1;
						bool flag2 = isCapturingTarget && i >= 0;
						if (flag2)
						{
							i = ((propagationPaths.trickleDownPath[0] == target) ? 0 : (-1));
						}
						while (i >= 0)
						{
							VisualElement visualElement = propagationPaths.trickleDownPath[i];
							evt.currentTarget = visualElement;
							compatibilityEvt.currentTarget = visualElement;
							bool flag3 = visualElement.HasTrickleDownEventCallbacks(num);
							if (flag3)
							{
								EventDispatchUtilities.HandleEvent_TrickleDownCallbacks(evt, panel, visualElement);
								bool isImmediatePropagationStopped = evt.isImmediatePropagationStopped;
								if (isImmediatePropagationStopped)
								{
									return;
								}
								bool flag4 = panel.ShouldSendCompatibilityMouseEvents((IPointerEvent)evt);
								if (flag4)
								{
									EventDispatchUtilities.HandleEvent_TrickleDownCallbacks(compatibilityEvt, panel, visualElement);
									bool isImmediatePropagationStopped2 = evt.isImmediatePropagationStopped;
									if (isImmediatePropagationStopped2)
									{
										return;
									}
								}
							}
							bool flag5 = visualElement.HasTrickleDownHandleEvent(num);
							if (flag5)
							{
								bool flag6 = EventDispatchUtilities.Disabled(evt, visualElement);
								EventDispatchUtilities.HandleEvent_TrickleDownHandleEvent(evt, panel, visualElement, flag6);
								bool isImmediatePropagationStopped3 = evt.isImmediatePropagationStopped;
								if (isImmediatePropagationStopped3)
								{
									return;
								}
								bool flag7 = panel.ShouldSendCompatibilityMouseEvents((IPointerEvent)evt);
								if (flag7)
								{
									EventDispatchUtilities.HandleEvent_TrickleDownHandleEvent(compatibilityEvt, panel, visualElement, flag6);
									bool isImmediatePropagationStopped4 = compatibilityEvt.isImmediatePropagationStopped;
									if (isImmediatePropagationStopped4)
									{
										return;
									}
								}
							}
							bool flag8 = evt.isPropagationStopped || compatibilityEvt.isPropagationStopped;
							if (flag8)
							{
								return;
							}
							i--;
						}
						evt.propagationPhase = PropagationPhase.BubbleUp;
						compatibilityEvt.propagationPhase = PropagationPhase.BubbleUp;
						foreach (VisualElement visualElement2 in propagationPaths.bubbleUpPath)
						{
							evt.currentTarget = visualElement2;
							compatibilityEvt.currentTarget = visualElement2;
							bool flag9 = visualElement2.HasBubbleUpHandleEvent(num);
							if (flag9)
							{
								bool flag10 = EventDispatchUtilities.Disabled(evt, visualElement2);
								EventDispatchUtilities.HandleEvent_BubbleUpAllDefaultActions(evt, panel, visualElement2, flag10, isCapturingTarget);
								bool isImmediatePropagationStopped5 = evt.isImmediatePropagationStopped;
								if (isImmediatePropagationStopped5)
								{
									break;
								}
								bool flag11 = panel.ShouldSendCompatibilityMouseEvents((IPointerEvent)evt);
								if (flag11)
								{
									EventDispatchUtilities.HandleEvent_BubbleUpAllDefaultActions(compatibilityEvt, panel, visualElement2, flag10, isCapturingTarget);
									bool isImmediatePropagationStopped6 = compatibilityEvt.isImmediatePropagationStopped;
									if (isImmediatePropagationStopped6)
									{
										break;
									}
								}
							}
							bool flag12 = visualElement2.HasBubbleUpEventCallbacks(num) && (!isCapturingTarget || visualElement2 == target);
							if (flag12)
							{
								EventDispatchUtilities.HandleEvent_BubbleUpCallbacks(evt, panel, visualElement2);
								bool isImmediatePropagationStopped7 = evt.isImmediatePropagationStopped;
								if (isImmediatePropagationStopped7)
								{
									break;
								}
								bool flag13 = panel.ShouldSendCompatibilityMouseEvents((IPointerEvent)evt);
								if (flag13)
								{
									EventDispatchUtilities.HandleEvent_BubbleUpCallbacks(compatibilityEvt, panel, visualElement2);
									bool isImmediatePropagationStopped8 = compatibilityEvt.isImmediatePropagationStopped;
									if (isImmediatePropagationStopped8)
									{
										break;
									}
								}
							}
							bool flag14 = evt.isPropagationStopped || compatibilityEvt.isPropagationStopped;
							if (flag14)
							{
								break;
							}
						}
					}
					finally
					{
						evt.currentTarget = null;
						evt.propagationPhase = PropagationPhase.None;
						compatibilityEvt.currentTarget = null;
						compatibilityEvt.propagationPhase = PropagationPhase.None;
						evt.dispatch = false;
					}
				}
			}
		}

		// Token: 0x060010F8 RID: 4344 RVA: 0x000499D0 File Offset: 0x00047BD0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void HandleEvent_DefaultActionAtTarget(EventBase evt, [NotNull] BaseVisualElementPanel panel, [NotNull] VisualElement element, bool disabled)
		{
			bool flag = element.elementPanel != panel;
			if (!flag)
			{
				using (new EventDebuggerLogExecuteDefaultAction(evt))
				{
					if (disabled)
					{
						element.ExecuteDefaultActionDisabledAtTargetInternal(evt);
					}
					else
					{
						element.ExecuteDefaultActionAtTargetInternal(evt);
					}
				}
			}
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x00049A34 File Offset: 0x00047C34
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void HandleEvent_DefaultAction(EventBase evt, [NotNull] BaseVisualElementPanel panel, [NotNull] VisualElement element, bool disabled)
		{
			bool flag = element.elementPanel != panel;
			if (!flag)
			{
				using (new EventDebuggerLogExecuteDefaultAction(evt))
				{
					if (disabled)
					{
						element.ExecuteDefaultActionDisabledInternal(evt);
					}
					else
					{
						element.ExecuteDefaultActionInternal(evt);
					}
				}
			}
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x00049A98 File Offset: 0x00047C98
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void HandleEvent_TrickleDownCallbacks(EventBase evt, [NotNull] BaseVisualElementPanel panel, [NotNull] VisualElement element)
		{
			EventCallbackRegistry callbackRegistry = element.m_CallbackRegistry;
			if (callbackRegistry != null)
			{
				callbackRegistry.m_TrickleDownCallbacks.Invoke(evt, panel, element);
			}
		}

		// Token: 0x060010FB RID: 4347 RVA: 0x00049AB5 File Offset: 0x00047CB5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void HandleEvent_BubbleUpCallbacks(EventBase evt, [NotNull] BaseVisualElementPanel panel, [NotNull] VisualElement element)
		{
			EventCallbackRegistry callbackRegistry = element.m_CallbackRegistry;
			if (callbackRegistry != null)
			{
				callbackRegistry.m_BubbleUpCallbacks.Invoke(evt, panel, element);
			}
		}

		// Token: 0x060010FC RID: 4348 RVA: 0x00049AD4 File Offset: 0x00047CD4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void HandleEvent_TrickleDownHandleEvent(EventBase evt, [NotNull] BaseVisualElementPanel panel, [NotNull] VisualElement element, bool disabled)
		{
			bool flag = element.elementPanel != panel;
			if (!flag)
			{
				if (disabled)
				{
					element.HandleEventTrickleDownDisabled(evt);
				}
				else
				{
					element.HandleEventTrickleDownInternal(evt);
				}
			}
		}

		// Token: 0x060010FD RID: 4349 RVA: 0x00049B0C File Offset: 0x00047D0C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void HandleEvent_BubbleUpHandleEvent(EventBase evt, [NotNull] BaseVisualElementPanel panel, [NotNull] VisualElement element, bool disabled)
		{
			bool flag = element.elementPanel != panel;
			if (!flag)
			{
				if (disabled)
				{
					element.HandleEventBubbleUpDisabled(evt);
				}
				else
				{
					element.HandleEventBubbleUpInternal(evt);
				}
			}
		}

		// Token: 0x060010FE RID: 4350 RVA: 0x00049B44 File Offset: 0x00047D44
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void HandleEvent_BubbleUpAllDefaultActions(EventBase evt, [NotNull] BaseVisualElementPanel panel, [NotNull] VisualElement element, bool disabled, bool isCapturingTarget)
		{
			bool flag = element == evt.target || !isCapturingTarget;
			bool flag2 = element == evt.target || element.isCompositeRoot;
			bool flag3 = flag2;
			if (flag3)
			{
				EventDispatchUtilities.HandleEvent_DefaultActionAtTarget(evt, panel, element, disabled);
			}
			bool flag4 = flag;
			if (flag4)
			{
				EventDispatchUtilities.HandleEvent_BubbleUpHandleEvent(evt, panel, element, disabled);
			}
			bool flag5 = flag2;
			if (flag5)
			{
				EventDispatchUtilities.HandleEvent_DefaultAction(evt, panel, element, disabled);
			}
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x00049BA8 File Offset: 0x00047DA8
		private static bool Disabled([NotNull] EventBase evt, [NotNull] VisualElement target)
		{
			return evt.skipDisabledElements && !target.enabledInHierarchy;
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x00049BD0 File Offset: 0x00047DD0
		public static void HandleEvent([NotNull] EventBase evt, [NotNull] VisualElement target)
		{
			bool isPropagationStopped = evt.isPropagationStopped;
			if (!isPropagationStopped)
			{
				BaseVisualElementPanel elementPanel = target.elementPanel;
				bool flag = EventDispatchUtilities.Disabled(evt, target);
				PropagationPhase propagationPhase = evt.propagationPhase;
				PropagationPhase propagationPhase2 = propagationPhase;
				if (propagationPhase2 != PropagationPhase.TrickleDown)
				{
					if (propagationPhase2 == PropagationPhase.BubbleUp)
					{
						EventDispatchUtilities.HandleEvent_BubbleUpAllDefaultActions(evt, elementPanel, target, flag, false);
						bool flag2 = !evt.isImmediatePropagationStopped;
						if (flag2)
						{
							EventDispatchUtilities.HandleEvent_BubbleUpCallbacks(evt, elementPanel, target);
						}
					}
				}
				else
				{
					EventDispatchUtilities.HandleEvent_TrickleDownCallbacks(evt, elementPanel, target);
					bool flag3 = !evt.isImmediatePropagationStopped;
					if (flag3)
					{
						EventDispatchUtilities.HandleEvent_TrickleDownHandleEvent(evt, elementPanel, target, flag);
					}
				}
			}
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x00049C5C File Offset: 0x00047E5C
		public static void DispatchToFocusedElementOrPanelRoot(EventBase evt, [NotNull] BaseVisualElementPanel panel)
		{
			bool flag = false;
			VisualElement visualElement = evt.elementTarget;
			bool flag2 = visualElement == null;
			if (flag2)
			{
				Focusable leafFocusedElement = panel.focusController.GetLeafFocusedElement();
				VisualElement visualElement2 = leafFocusedElement as VisualElement;
				bool flag3 = visualElement2 != null;
				if (flag3)
				{
					visualElement = visualElement2;
				}
				else
				{
					visualElement = panel.visualTree;
					flag = true;
				}
				VisualElement visualElement3 = panel.GetCapturingElement(PointerId.mousePointerId) as VisualElement;
				bool flag4 = visualElement3 != null && visualElement3 != visualElement && !visualElement3.Contains(visualElement) && visualElement3.HasSelfEventInterests(evt.eventCategories);
				if (flag4)
				{
					evt.elementTarget = visualElement3;
					bool skipDisabledElements = evt.skipDisabledElements;
					evt.skipDisabledElements = false;
					EventDispatchUtilities.HandleEventAtTargetAndDefaultPhase(evt, panel, visualElement3);
					evt.skipDisabledElements = skipDisabledElements;
				}
				evt.elementTarget = visualElement;
			}
			EventDispatchUtilities.PropagateEvent(evt, panel, visualElement, false);
			bool flag5 = flag && evt.propagateToIMGUI;
			if (flag5)
			{
				EventDispatchUtilities.PropagateToRemainingIMGUIContainers(evt, panel.visualTree);
			}
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x00049D4C File Offset: 0x00047F4C
		public static void DispatchToElementUnderPointerOrPanelRoot(EventBase evt, [NotNull] BaseVisualElementPanel panel, int pointerId, Vector2 position)
		{
			VisualElement visualElement = panel.RecomputeTopElementUnderPointer(pointerId, position, evt);
			bool flag = false;
			VisualElement visualElement2 = evt.elementTarget;
			bool flag2 = visualElement2 == null;
			if (flag2)
			{
				visualElement2 = visualElement;
				bool flag3 = visualElement2 == null;
				if (flag3)
				{
					visualElement2 = panel.visualTree;
					flag = true;
				}
				evt.elementTarget = visualElement2;
			}
			EventDispatchUtilities.PropagateEvent(evt, panel, visualElement2, false);
			bool flag4 = flag && evt.propagateToIMGUI;
			if (flag4)
			{
				EventDispatchUtilities.PropagateToRemainingIMGUIContainers(evt, panel.visualTree);
			}
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x00049DC0 File Offset: 0x00047FC0
		public static void DispatchToCachedElementUnderPointerOrPanelRoot(EventBase evt, [NotNull] BaseVisualElementPanel panel, int pointerId, Vector2 position)
		{
			bool flag = false;
			VisualElement visualElement = evt.elementTarget;
			bool flag2 = visualElement == null;
			if (flag2)
			{
				visualElement = panel.GetTopElementUnderPointer(pointerId);
				bool flag3 = visualElement == null;
				if (flag3)
				{
					visualElement = panel.visualTree;
					flag = true;
				}
				evt.elementTarget = visualElement;
			}
			EventDispatchUtilities.PropagateEvent(evt, panel, visualElement, false);
			bool flag4 = flag && evt.propagateToIMGUI;
			if (flag4)
			{
				EventDispatchUtilities.PropagateToRemainingIMGUIContainers(evt, panel.visualTree);
			}
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x00049E30 File Offset: 0x00048030
		public static void DispatchToAssignedTarget(EventBase evt, [NotNull] BaseVisualElementPanel panel)
		{
			VisualElement elementTarget = evt.elementTarget;
			bool flag = elementTarget == null;
			if (flag)
			{
				throw new ArgumentException(string.Format("Event target not set. Event type {0} requires a target.", evt.GetType()));
			}
			EventDispatchUtilities.PropagateEvent(evt, panel, elementTarget, false);
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x00049E70 File Offset: 0x00048070
		public static void DefaultDispatch(EventBase evt, [NotNull] BaseVisualElementPanel panel)
		{
			VisualElement elementTarget = evt.elementTarget;
			bool flag = elementTarget == null;
			if (!flag)
			{
				bool bubblesOrTricklesDown = evt.bubblesOrTricklesDown;
				if (bubblesOrTricklesDown)
				{
					EventDispatchUtilities.PropagateEvent(evt, panel, elementTarget, false);
				}
				else
				{
					EventDispatchUtilities.HandleEventAtTargetAndDefaultPhase(evt, panel, elementTarget);
				}
			}
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x00049EB0 File Offset: 0x000480B0
		public static void DispatchToCapturingElementOrElementUnderPointer(EventBase evt, [NotNull] BaseVisualElementPanel panel, int pointerId, Vector2 position)
		{
			panel.RecomputeTopElementUnderPointer(pointerId, position, evt);
			bool flag = EventDispatchUtilities.DispatchToCapturingElement(evt, panel, pointerId, position);
			if (!flag)
			{
				EventDispatchUtilities.DispatchToCachedElementUnderPointerOrPanelRoot(evt, panel, pointerId, position);
			}
		}

		// Token: 0x06001107 RID: 4359 RVA: 0x00049EE4 File Offset: 0x000480E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool DispatchToCapturingElement(EventBase evt, [NotNull] BaseVisualElementPanel panel, int pointerId, Vector2 position)
		{
			VisualElement visualElement = panel.GetCapturingElement(pointerId) as VisualElement;
			bool flag = visualElement == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = visualElement.panel == null;
				if (flag3)
				{
					panel.ReleasePointer(pointerId);
					flag2 = false;
				}
				else
				{
					bool flag4 = evt.target != null && evt.target != visualElement;
					if (flag4)
					{
						flag2 = false;
					}
					else
					{
						bool flag5 = visualElement.panel != panel;
						if (flag5)
						{
							flag2 = false;
						}
						else
						{
							evt.skipDisabledElements = false;
							evt.elementTarget = visualElement;
							EventDispatchUtilities.PropagateEvent(evt, panel, visualElement, true);
							flag2 = true;
						}
					}
				}
			}
			return flag2;
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x00049F80 File Offset: 0x00048180
		internal static void DispatchToPanelRoot(EventBase evt, [NotNull] BaseVisualElementPanel panel)
		{
			VisualElement visualElement = (evt.elementTarget = panel.visualTree);
			EventDispatchUtilities.PropagateEvent(evt, panel, visualElement, false);
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x00049FAC File Offset: 0x000481AC
		internal static void PropagateToRemainingIMGUIContainers(EventBase evt, [NotNull] VisualElement root)
		{
			bool flag = evt.imguiEvent != null && root.elementPanel.contextType > ContextType.Player;
			if (flag)
			{
				EventDispatchUtilities.PropagateToRemainingIMGUIContainerRecursive(evt, root);
			}
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x00049FE0 File Offset: 0x000481E0
		private static void PropagateToRemainingIMGUIContainerRecursive(EventBase evt, [NotNull] VisualElement root)
		{
			bool isIMGUIContainer = root.isIMGUIContainer;
			if (isIMGUIContainer)
			{
				bool flag = root != evt.target;
				if (flag)
				{
					IMGUIContainer imguicontainer = (IMGUIContainer)root;
					VisualElement elementTarget = evt.elementTarget;
					bool flag2 = elementTarget != null && elementTarget.focusable;
					bool flag3 = imguicontainer.SendEventToIMGUI(evt, !flag2, true);
					if (flag3)
					{
						evt.StopPropagation();
					}
					bool flag4 = evt.imguiEvent.rawType == EventType.Used;
					if (flag4)
					{
						Debug.Assert(evt.isPropagationStopped, "evt.isPropagationStopped");
					}
				}
			}
			else
			{
				bool flag5 = root.imguiContainerDescendantCount > 0;
				if (flag5)
				{
					List<VisualElement> list;
					using (CollectionPool<List<VisualElement>, VisualElement>.Get(out list))
					{
						list.AddRange(root.hierarchy.children);
						foreach (VisualElement visualElement in list)
						{
							bool flag6 = visualElement.hierarchy.parent != root;
							if (!flag6)
							{
								EventDispatchUtilities.PropagateToRemainingIMGUIContainerRecursive(evt, visualElement);
								bool isPropagationStopped = evt.isPropagationStopped;
								if (isPropagationStopped)
								{
									break;
								}
							}
						}
					}
				}
			}
		}
	}
}
