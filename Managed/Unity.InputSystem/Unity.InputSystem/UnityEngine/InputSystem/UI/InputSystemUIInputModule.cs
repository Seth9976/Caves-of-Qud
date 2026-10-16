using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace UnityEngine.InputSystem.UI
{
	// Token: 0x0200008C RID: 140
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.inputsystem@1.19/manual/UISupport.html#setting-up-ui-input")]
	public class InputSystemUIInputModule : BaseInputModule
	{
		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000B28 RID: 2856 RVA: 0x0003BE32 File Offset: 0x0003A032
		// (set) Token: 0x06000B29 RID: 2857 RVA: 0x0003BE3A File Offset: 0x0003A03A
		public bool deselectOnBackgroundClick
		{
			get
			{
				return this.m_DeselectOnBackgroundClick;
			}
			set
			{
				this.m_DeselectOnBackgroundClick = value;
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000B2A RID: 2858 RVA: 0x0003BE43 File Offset: 0x0003A043
		// (set) Token: 0x06000B2B RID: 2859 RVA: 0x0003BE4B File Offset: 0x0003A04B
		public UIPointerBehavior pointerBehavior
		{
			get
			{
				return this.m_PointerBehavior;
			}
			set
			{
				this.m_PointerBehavior = value;
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000B2C RID: 2860 RVA: 0x0003BE54 File Offset: 0x0003A054
		// (set) Token: 0x06000B2D RID: 2861 RVA: 0x0003BE5C File Offset: 0x0003A05C
		public InputSystemUIInputModule.CursorLockBehavior cursorLockBehavior
		{
			get
			{
				return this.m_CursorLockBehavior;
			}
			set
			{
				this.m_CursorLockBehavior = value;
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000B2E RID: 2862 RVA: 0x0003BE65 File Offset: 0x0003A065
		// (set) Token: 0x06000B2F RID: 2863 RVA: 0x0003BE6D File Offset: 0x0003A06D
		internal GameObject localMultiPlayerRoot
		{
			get
			{
				return this.m_LocalMultiPlayerRoot;
			}
			set
			{
				this.m_LocalMultiPlayerRoot = value;
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000B30 RID: 2864 RVA: 0x0003BE76 File Offset: 0x0003A076
		// (set) Token: 0x06000B31 RID: 2865 RVA: 0x0003BE7E File Offset: 0x0003A07E
		public float scrollDeltaPerTick
		{
			get
			{
				return this.m_ScrollDeltaPerTick;
			}
			set
			{
				this.m_ScrollDeltaPerTick = value;
			}
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x0003BE88 File Offset: 0x0003A088
		public override void ActivateModule()
		{
			base.ActivateModule();
			GameObject gameObject = base.eventSystem.currentSelectedGameObject;
			if (gameObject == null)
			{
				gameObject = base.eventSystem.firstSelectedGameObject;
			}
			base.eventSystem.SetSelectedGameObject(gameObject, this.GetBaseEventData());
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x0003BED0 File Offset: 0x0003A0D0
		public override bool IsPointerOverGameObject(int pointerOrTouchId)
		{
			if (InputSystem.isProcessingEvents)
			{
				Debug.LogWarning("Calling IsPointerOverGameObject() from within event processing (such as from InputAction callbacks) will not work as expected; it will query UI state from the last frame");
			}
			int num = -1;
			if (pointerOrTouchId < 0)
			{
				if (this.m_CurrentPointerId != -1)
				{
					num = this.m_CurrentPointerIndex;
				}
				else if (this.m_PointerStates.length > 0)
				{
					num = 0;
				}
			}
			else
			{
				num = this.GetPointerStateIndexFor(pointerOrTouchId);
			}
			return num != -1 && this.m_PointerStates[num].eventData.pointerEnter != null;
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x0003BF44 File Offset: 0x0003A144
		public RaycastResult GetLastRaycastResult(int pointerOrTouchId)
		{
			int pointerStateIndexFor = this.GetPointerStateIndexFor(pointerOrTouchId);
			if (pointerStateIndexFor == -1)
			{
				return default(RaycastResult);
			}
			return this.m_PointerStates[pointerStateIndexFor].eventData.pointerCurrentRaycast;
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x0003BF80 File Offset: 0x0003A180
		private RaycastResult PerformRaycast(ExtendedPointerEventData eventData)
		{
			if (eventData == null)
			{
				throw new ArgumentNullException("eventData");
			}
			if (eventData.pointerType == UIPointerType.Tracked && TrackedDeviceRaycaster.s_Instances.length > 0)
			{
				for (int i = 0; i < TrackedDeviceRaycaster.s_Instances.length; i++)
				{
					TrackedDeviceRaycaster trackedDeviceRaycaster = TrackedDeviceRaycaster.s_Instances[i];
					this.m_RaycastResultCache.Clear();
					trackedDeviceRaycaster.PerformRaycast(eventData, this.m_RaycastResultCache);
					if (this.m_RaycastResultCache.Count > 0)
					{
						RaycastResult raycastResult = this.m_RaycastResultCache[0];
						this.m_RaycastResultCache.Clear();
						return raycastResult;
					}
				}
				return default(RaycastResult);
			}
			base.eventSystem.RaycastAll(eventData, this.m_RaycastResultCache);
			RaycastResult raycastResult2 = BaseInputModule.FindFirstRaycast(this.m_RaycastResultCache);
			this.m_RaycastResultCache.Clear();
			return raycastResult2;
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x0003C040 File Offset: 0x0003A240
		private void ProcessPointer(ref PointerModel state)
		{
			ExtendedPointerEventData eventData = state.eventData;
			UIPointerType pointerType = eventData.pointerType;
			if (pointerType == UIPointerType.MouseOrPen && Cursor.lockState == CursorLockMode.Locked)
			{
				eventData.position = ((this.m_CursorLockBehavior == InputSystemUIInputModule.CursorLockBehavior.OutsideScreen) ? new Vector2(-1f, -1f) : new Vector2((float)Screen.width / 2f, (float)Screen.height / 2f));
				eventData.delta = default(Vector2);
			}
			else if (pointerType == UIPointerType.Tracked)
			{
				Vector3 vector = state.worldPosition;
				Quaternion quaternion = state.worldOrientation;
				if (this.m_XRTrackingOrigin != null)
				{
					vector = this.m_XRTrackingOrigin.TransformPoint(vector);
					quaternion = this.m_XRTrackingOrigin.rotation * quaternion;
				}
				eventData.trackedDeviceOrientation = quaternion;
				eventData.trackedDevicePosition = vector;
			}
			else
			{
				eventData.delta = state.screenPosition - eventData.position;
				eventData.position = state.screenPosition;
			}
			eventData.Reset();
			eventData.pointerCurrentRaycast = this.PerformRaycast(eventData);
			if (pointerType == UIPointerType.Tracked && eventData.pointerCurrentRaycast.isValid)
			{
				Vector2 screenPosition = eventData.pointerCurrentRaycast.screenPosition;
				eventData.delta = screenPosition - eventData.position;
				eventData.position = eventData.pointerCurrentRaycast.screenPosition;
			}
			eventData.button = PointerEventData.InputButton.Left;
			state.leftButton.CopyPressStateTo(eventData);
			this.ProcessPointerMovement(ref state, eventData);
			if (!state.changedThisFrame && (this.xrTrackingOrigin == null || state.pointerType != UIPointerType.Tracked))
			{
				return;
			}
			this.ProcessPointerButton(ref state.leftButton, eventData);
			this.ProcessPointerButtonDrag(ref state.leftButton, eventData);
			InputSystemUIInputModule.ProcessPointerScroll(ref state, eventData);
			eventData.button = PointerEventData.InputButton.Right;
			state.rightButton.CopyPressStateTo(eventData);
			this.ProcessPointerButton(ref state.rightButton, eventData);
			this.ProcessPointerButtonDrag(ref state.rightButton, eventData);
			eventData.button = PointerEventData.InputButton.Middle;
			state.middleButton.CopyPressStateTo(eventData);
			this.ProcessPointerButton(ref state.middleButton, eventData);
			this.ProcessPointerButtonDrag(ref state.middleButton, eventData);
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x0003C23C File Offset: 0x0003A43C
		private bool PointerShouldIgnoreTransform(Transform t)
		{
			MultiplayerEventSystem multiplayerEventSystem = base.eventSystem as MultiplayerEventSystem;
			return multiplayerEventSystem != null && multiplayerEventSystem.playerRoot != null && !t.IsChildOf(multiplayerEventSystem.playerRoot.transform);
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x0003C27C File Offset: 0x0003A47C
		private void ProcessPointerMovement(ref PointerModel pointer, ExtendedPointerEventData eventData)
		{
			GameObject gameObject = ((eventData.pointerType == UIPointerType.Touch && !pointer.leftButton.isPressed && !pointer.leftButton.wasReleasedThisFrame) ? null : eventData.pointerCurrentRaycast.gameObject);
			this.ProcessPointerMovement(eventData, gameObject);
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x0003C2C8 File Offset: 0x0003A4C8
		private void ProcessPointerMovement(ExtendedPointerEventData eventData, GameObject currentPointerTarget)
		{
			bool flag = eventData.IsPointerMoving();
			if (flag)
			{
				for (int i = 0; i < eventData.hovered.Count; i++)
				{
					ExecuteEvents.Execute<IPointerMoveHandler>(eventData.hovered[i], eventData, ExecuteEvents.pointerMoveHandler);
				}
			}
			if (currentPointerTarget == null || eventData.pointerEnter == null)
			{
				for (int j = 0; j < eventData.hovered.Count; j++)
				{
					ExecuteEvents.Execute<IPointerExitHandler>(eventData.hovered[j], eventData, ExecuteEvents.pointerExitHandler);
				}
				eventData.hovered.Clear();
				if (currentPointerTarget == null)
				{
					eventData.pointerEnter = null;
					return;
				}
			}
			if (eventData.pointerEnter == currentPointerTarget && currentPointerTarget)
			{
				return;
			}
			GameObject gameObject = BaseInputModule.FindCommonRoot(eventData.pointerEnter, currentPointerTarget);
			Transform transform = ((gameObject != null) ? gameObject.transform : null);
			Component component = (Component)currentPointerTarget.GetComponentInParent<IPointerExitHandler>();
			Transform transform2 = ((component != null) ? component.transform : null);
			if (eventData.pointerEnter != null)
			{
				Transform transform3 = eventData.pointerEnter.transform;
				while (transform3 != null && (!this.sendPointerHoverToParent || !(transform3 == transform)) && (this.sendPointerHoverToParent || !(transform3 == transform2)))
				{
					eventData.fullyExited = transform3 != transform && eventData.pointerEnter != currentPointerTarget;
					ExecuteEvents.Execute<IPointerExitHandler>(transform3.gameObject, eventData, ExecuteEvents.pointerExitHandler);
					eventData.hovered.Remove(transform3.gameObject);
					if (this.sendPointerHoverToParent)
					{
						transform3 = transform3.parent;
					}
					if (transform3 == transform)
					{
						break;
					}
					if (!this.sendPointerHoverToParent)
					{
						transform3 = transform3.parent;
					}
				}
			}
			Transform transform4 = (eventData.pointerEnter ? eventData.pointerEnter.transform : null);
			eventData.pointerEnter = currentPointerTarget;
			if (currentPointerTarget != null)
			{
				Transform transform5 = currentPointerTarget.transform;
				while (transform5 != null && !this.PointerShouldIgnoreTransform(transform5))
				{
					eventData.reentered = transform5 == transform && transform5 != transform4;
					if (this.sendPointerHoverToParent && eventData.reentered)
					{
						break;
					}
					ExecuteEvents.Execute<IPointerEnterHandler>(transform5.gameObject, eventData, ExecuteEvents.pointerEnterHandler);
					if (flag)
					{
						ExecuteEvents.Execute<IPointerMoveHandler>(transform5.gameObject, eventData, ExecuteEvents.pointerMoveHandler);
					}
					eventData.hovered.Add(transform5.gameObject);
					if (!this.sendPointerHoverToParent && transform5.GetComponent<IPointerEnterHandler>() != null)
					{
						break;
					}
					if (this.sendPointerHoverToParent)
					{
						transform5 = transform5.parent;
					}
					if (transform5 == transform)
					{
						break;
					}
					if (!this.sendPointerHoverToParent)
					{
						transform5 = transform5.parent;
					}
				}
			}
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x0003C584 File Offset: 0x0003A784
		private void ProcessPointerButton(ref PointerModel.ButtonState button, PointerEventData eventData)
		{
			GameObject gameObject = eventData.pointerCurrentRaycast.gameObject;
			if (gameObject != null && this.PointerShouldIgnoreTransform(gameObject.transform))
			{
				return;
			}
			if (button.wasPressedThisFrame)
			{
				button.pressTime = InputRuntime.s_Instance.unscaledGameTime;
				eventData.delta = Vector2.zero;
				eventData.dragging = false;
				eventData.pressPosition = eventData.position;
				eventData.pointerPressRaycast = eventData.pointerCurrentRaycast;
				eventData.eligibleForClick = true;
				eventData.useDragThreshold = true;
				GameObject eventHandler = ExecuteEvents.GetEventHandler<ISelectHandler>(gameObject);
				if (eventHandler != base.eventSystem.currentSelectedGameObject && (eventHandler != null || this.m_DeselectOnBackgroundClick))
				{
					base.eventSystem.SetSelectedGameObject(null, eventData);
				}
				GameObject gameObject2 = ExecuteEvents.ExecuteHierarchy<IPointerDownHandler>(gameObject, eventData, ExecuteEvents.pointerDownHandler);
				GameObject eventHandler2 = ExecuteEvents.GetEventHandler<IPointerClickHandler>(gameObject);
				if (gameObject2 == null)
				{
					gameObject2 = eventHandler2;
				}
				button.clickedOnSameGameObject = gameObject2 == eventData.lastPress && button.pressTime - eventData.clickTime <= 0.3f;
				if (eventData.clickCount > 0 && !button.clickedOnSameGameObject)
				{
					eventData.clickCount = 0;
					eventData.clickTime = 0f;
				}
				eventData.pointerPress = gameObject2;
				eventData.pointerClick = eventHandler2;
				eventData.rawPointerPress = gameObject;
				eventData.pointerDrag = ExecuteEvents.GetEventHandler<IDragHandler>(gameObject);
				if (eventData.pointerDrag != null)
				{
					ExecuteEvents.Execute<IInitializePotentialDragHandler>(eventData.pointerDrag, eventData, ExecuteEvents.initializePotentialDrag);
				}
			}
			if (button.wasReleasedThisFrame)
			{
				GameObject eventHandler3 = ExecuteEvents.GetEventHandler<IPointerClickHandler>(gameObject);
				bool flag = eventData.pointerClick != null && eventData.pointerClick == eventHandler3 && eventData.eligibleForClick;
				if (flag)
				{
					if (button.clickedOnSameGameObject)
					{
						int num = eventData.clickCount + 1;
						eventData.clickCount = num;
					}
					else
					{
						eventData.clickCount = 1;
					}
					eventData.clickTime = InputRuntime.s_Instance.unscaledGameTime;
				}
				ExecuteEvents.Execute<IPointerUpHandler>(eventData.pointerPress, eventData, ExecuteEvents.pointerUpHandler);
				if (flag)
				{
					ExecuteEvents.Execute<IPointerClickHandler>(eventData.pointerClick, eventData, ExecuteEvents.pointerClickHandler);
				}
				else if (eventData.dragging && eventData.pointerDrag != null)
				{
					ExecuteEvents.ExecuteHierarchy<IDropHandler>(gameObject, eventData, ExecuteEvents.dropHandler);
				}
				eventData.eligibleForClick = false;
				eventData.pointerPress = null;
				eventData.rawPointerPress = null;
				if (eventData.dragging && eventData.pointerDrag != null)
				{
					ExecuteEvents.Execute<IEndDragHandler>(eventData.pointerDrag, eventData, ExecuteEvents.endDragHandler);
				}
				eventData.dragging = false;
				eventData.pointerDrag = null;
				button.ignoreNextClick = false;
			}
			button.CopyPressStateFrom(eventData);
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x0003C80C File Offset: 0x0003AA0C
		private void ProcessPointerButtonDrag(ref PointerModel.ButtonState button, ExtendedPointerEventData eventData)
		{
			if (!eventData.IsPointerMoving() || (eventData.pointerType == UIPointerType.MouseOrPen && Cursor.lockState == CursorLockMode.Locked) || eventData.pointerDrag == null)
			{
				return;
			}
			if (!eventData.dragging && (!eventData.useDragThreshold || (double)(eventData.pressPosition - eventData.position).sqrMagnitude >= (double)base.eventSystem.pixelDragThreshold * (double)base.eventSystem.pixelDragThreshold * (double)((eventData.pointerType == UIPointerType.Tracked) ? this.m_TrackedDeviceDragThresholdMultiplier : 1f)))
			{
				ExecuteEvents.Execute<IBeginDragHandler>(eventData.pointerDrag, eventData, ExecuteEvents.beginDragHandler);
				eventData.dragging = true;
			}
			if (eventData.dragging)
			{
				if (eventData.pointerPress != eventData.pointerDrag)
				{
					ExecuteEvents.Execute<IPointerUpHandler>(eventData.pointerPress, eventData, ExecuteEvents.pointerUpHandler);
					eventData.eligibleForClick = false;
					eventData.pointerPress = null;
					eventData.rawPointerPress = null;
				}
				ExecuteEvents.Execute<IDragHandler>(eventData.pointerDrag, eventData, ExecuteEvents.dragHandler);
				button.CopyPressStateFrom(eventData);
			}
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x0003C914 File Offset: 0x0003AB14
		private static void ProcessPointerScroll(ref PointerModel pointer, PointerEventData eventData)
		{
			Vector2 scrollDelta = pointer.scrollDelta;
			if (!Mathf.Approximately(scrollDelta.sqrMagnitude, 0f))
			{
				eventData.scrollDelta = scrollDelta;
				ExecuteEvents.ExecuteHierarchy<IScrollHandler>(ExecuteEvents.GetEventHandler<IScrollHandler>(eventData.pointerEnter), eventData, ExecuteEvents.scrollHandler);
			}
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x0003C95C File Offset: 0x0003AB5C
		internal void ProcessNavigation(ref NavigationModel navigationState)
		{
			bool flag = false;
			if (base.eventSystem.currentSelectedGameObject != null)
			{
				BaseEventData baseEventData = this.GetBaseEventData();
				ExecuteEvents.Execute<IUpdateSelectedHandler>(base.eventSystem.currentSelectedGameObject, baseEventData, ExecuteEvents.updateSelectedHandler);
				flag = baseEventData.used;
			}
			if (!base.eventSystem.sendNavigationEvents)
			{
				return;
			}
			Vector2 move = navigationState.move;
			if (!flag && (!Mathf.Approximately(move.x, 0f) || !Mathf.Approximately(move.y, 0f)))
			{
				float unscaledGameTime = InputRuntime.s_Instance.unscaledGameTime;
				Vector2 move2 = navigationState.move;
				MoveDirection moveDirection = MoveDirection.None;
				if (move2.sqrMagnitude > 0f)
				{
					if (Mathf.Abs(move2.x) > Mathf.Abs(move2.y))
					{
						moveDirection = ((move2.x > 0f) ? MoveDirection.Right : MoveDirection.Left);
					}
					else
					{
						moveDirection = ((move2.y > 0f) ? MoveDirection.Up : MoveDirection.Down);
					}
				}
				if (moveDirection != this.m_NavigationState.lastMoveDirection)
				{
					this.m_NavigationState.consecutiveMoveCount = 0;
				}
				if (moveDirection != MoveDirection.None)
				{
					bool flag2 = true;
					if (this.m_NavigationState.consecutiveMoveCount != 0)
					{
						if (this.m_NavigationState.consecutiveMoveCount > 1)
						{
							flag2 = unscaledGameTime > this.m_NavigationState.lastMoveTime + this.moveRepeatRate;
						}
						else
						{
							flag2 = unscaledGameTime > this.m_NavigationState.lastMoveTime + this.moveRepeatDelay;
						}
					}
					if (flag2)
					{
						ExtendedAxisEventData extendedAxisEventData = this.m_NavigationState.eventData as ExtendedAxisEventData;
						if (extendedAxisEventData == null)
						{
							extendedAxisEventData = new ExtendedAxisEventData(base.eventSystem);
							this.m_NavigationState.eventData = extendedAxisEventData;
						}
						extendedAxisEventData.Reset();
						extendedAxisEventData.moveVector = move2;
						extendedAxisEventData.moveDir = moveDirection;
						extendedAxisEventData.device = navigationState.device;
						if (this.IsMoveAllowed(extendedAxisEventData))
						{
							ExecuteEvents.Execute<IMoveHandler>(base.eventSystem.currentSelectedGameObject, extendedAxisEventData, ExecuteEvents.moveHandler);
							flag = extendedAxisEventData.used;
							this.m_NavigationState.consecutiveMoveCount = this.m_NavigationState.consecutiveMoveCount + 1;
							this.m_NavigationState.lastMoveTime = unscaledGameTime;
							this.m_NavigationState.lastMoveDirection = moveDirection;
						}
					}
				}
				else
				{
					this.m_NavigationState.consecutiveMoveCount = 0;
				}
			}
			else
			{
				this.m_NavigationState.consecutiveMoveCount = 0;
			}
			if (!flag && base.eventSystem.currentSelectedGameObject != null)
			{
				InputActionReference submitAction = this.m_SubmitAction;
				InputAction inputAction = ((submitAction != null) ? submitAction.action : null);
				InputActionReference cancelAction = this.m_CancelAction;
				InputAction inputAction2 = ((cancelAction != null) ? cancelAction.action : null);
				ExtendedSubmitCancelEventData extendedSubmitCancelEventData = this.m_SubmitCancelState.eventData as ExtendedSubmitCancelEventData;
				if (extendedSubmitCancelEventData == null)
				{
					extendedSubmitCancelEventData = new ExtendedSubmitCancelEventData(base.eventSystem);
					this.m_SubmitCancelState.eventData = extendedSubmitCancelEventData;
				}
				extendedSubmitCancelEventData.Reset();
				extendedSubmitCancelEventData.device = this.m_SubmitCancelState.device;
				if (inputAction2 != null && inputAction2.WasPerformedThisDynamicUpdate())
				{
					ExecuteEvents.Execute<ICancelHandler>(base.eventSystem.currentSelectedGameObject, extendedSubmitCancelEventData, ExecuteEvents.cancelHandler);
				}
				if (!extendedSubmitCancelEventData.used && inputAction != null && inputAction.WasPerformedThisDynamicUpdate())
				{
					ExecuteEvents.Execute<ISubmitHandler>(base.eventSystem.currentSelectedGameObject, extendedSubmitCancelEventData, ExecuteEvents.submitHandler);
				}
			}
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x0003CC74 File Offset: 0x0003AE74
		private bool IsMoveAllowed(AxisEventData eventData)
		{
			if (this.m_LocalMultiPlayerRoot == null)
			{
				return true;
			}
			if (base.eventSystem.currentSelectedGameObject == null)
			{
				return true;
			}
			Selectable component = base.eventSystem.currentSelectedGameObject.GetComponent<Selectable>();
			if (component == null)
			{
				return true;
			}
			Selectable selectable = null;
			switch (eventData.moveDir)
			{
			case MoveDirection.Left:
				selectable = component.FindSelectableOnLeft();
				break;
			case MoveDirection.Up:
				selectable = component.FindSelectableOnUp();
				break;
			case MoveDirection.Right:
				selectable = component.FindSelectableOnRight();
				break;
			case MoveDirection.Down:
				selectable = component.FindSelectableOnDown();
				break;
			}
			return selectable == null || selectable.transform.IsChildOf(this.m_LocalMultiPlayerRoot.transform);
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000B3F RID: 2879 RVA: 0x0003CD26 File Offset: 0x0003AF26
		// (set) Token: 0x06000B40 RID: 2880 RVA: 0x0003CD2E File Offset: 0x0003AF2E
		public float moveRepeatDelay
		{
			get
			{
				return this.m_MoveRepeatDelay;
			}
			set
			{
				this.m_MoveRepeatDelay = value;
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000B41 RID: 2881 RVA: 0x0003CD37 File Offset: 0x0003AF37
		// (set) Token: 0x06000B42 RID: 2882 RVA: 0x0003CD3F File Offset: 0x0003AF3F
		public float moveRepeatRate
		{
			get
			{
				return this.m_MoveRepeatRate;
			}
			set
			{
				this.m_MoveRepeatRate = value;
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000B43 RID: 2883 RVA: 0x0003CD48 File Offset: 0x0003AF48
		private bool explictlyIgnoreFocus
		{
			get
			{
				return InputSystem.settings.backgroundBehavior == InputSettings.BackgroundBehavior.IgnoreFocus;
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000B44 RID: 2884 RVA: 0x0003CD57 File Offset: 0x0003AF57
		private bool shouldIgnoreFocus
		{
			get
			{
				return this.explictlyIgnoreFocus || InputRuntime.s_Instance.runInBackground;
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000B45 RID: 2885 RVA: 0x0003CD6D File Offset: 0x0003AF6D
		// (set) Token: 0x06000B46 RID: 2886 RVA: 0x0003CD75 File Offset: 0x0003AF75
		[Obsolete("'repeatRate' has been obsoleted; use 'moveRepeatRate' instead. (UnityUpgradable) -> moveRepeatRate", false)]
		public float repeatRate
		{
			get
			{
				return this.moveRepeatRate;
			}
			set
			{
				this.moveRepeatRate = value;
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000B47 RID: 2887 RVA: 0x0003CD7E File Offset: 0x0003AF7E
		// (set) Token: 0x06000B48 RID: 2888 RVA: 0x0003CD86 File Offset: 0x0003AF86
		[Obsolete("'repeatDelay' has been obsoleted; use 'moveRepeatDelay' instead. (UnityUpgradable) -> moveRepeatDelay", false)]
		public float repeatDelay
		{
			get
			{
				return this.moveRepeatDelay;
			}
			set
			{
				this.moveRepeatDelay = value;
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000B49 RID: 2889 RVA: 0x0003CD8F File Offset: 0x0003AF8F
		// (set) Token: 0x06000B4A RID: 2890 RVA: 0x0003CD97 File Offset: 0x0003AF97
		public Transform xrTrackingOrigin
		{
			get
			{
				return this.m_XRTrackingOrigin;
			}
			set
			{
				this.m_XRTrackingOrigin = value;
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000B4B RID: 2891 RVA: 0x0003CDA0 File Offset: 0x0003AFA0
		// (set) Token: 0x06000B4C RID: 2892 RVA: 0x0003CDA8 File Offset: 0x0003AFA8
		public float trackedDeviceDragThresholdMultiplier
		{
			get
			{
				return this.m_TrackedDeviceDragThresholdMultiplier;
			}
			set
			{
				this.m_TrackedDeviceDragThresholdMultiplier = value;
			}
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x0003CDB4 File Offset: 0x0003AFB4
		private void SwapAction(ref InputActionReference property, InputActionReference newValue, bool actionsHooked, Action<InputAction.CallbackContext> actionCallback)
		{
			if (property == newValue || (property != null && newValue != null && property.action == newValue.action))
			{
				return;
			}
			if (property != null && actionCallback != null && actionsHooked)
			{
				property.action.performed -= actionCallback;
				property.action.canceled -= actionCallback;
			}
			InputActionReference inputActionReference = property;
			bool flag = ((inputActionReference != null) ? inputActionReference.action : null) == null;
			InputActionReference inputActionReference2 = property;
			bool flag2 = ((inputActionReference2 != null) ? inputActionReference2.action : null) != null && property.action.enabled;
			this.TryDisableInputAction(property, false);
			property = newValue;
			if (((newValue != null) ? newValue.action : null) != null && actionCallback != null && actionsHooked)
			{
				property.action.performed += actionCallback;
				property.action.canceled += actionCallback;
			}
			if (base.isActiveAndEnabled && ((newValue != null) ? newValue.action : null) != null && (flag2 || flag))
			{
				this.EnableInputAction(property);
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000B4E RID: 2894 RVA: 0x0003CEB7 File Offset: 0x0003B0B7
		// (set) Token: 0x06000B4F RID: 2895 RVA: 0x0003CEBF File Offset: 0x0003B0BF
		public InputActionReference point
		{
			get
			{
				return this.m_PointAction;
			}
			set
			{
				this.SwapAction(ref this.m_PointAction, value, this.m_ActionsHooked, this.m_OnPointDelegate);
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000B50 RID: 2896 RVA: 0x0003CEDA File Offset: 0x0003B0DA
		// (set) Token: 0x06000B51 RID: 2897 RVA: 0x0003CEE2 File Offset: 0x0003B0E2
		public InputActionReference scrollWheel
		{
			get
			{
				return this.m_ScrollWheelAction;
			}
			set
			{
				this.SwapAction(ref this.m_ScrollWheelAction, value, this.m_ActionsHooked, this.m_OnScrollWheelDelegate);
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000B52 RID: 2898 RVA: 0x0003CEFD File Offset: 0x0003B0FD
		// (set) Token: 0x06000B53 RID: 2899 RVA: 0x0003CF05 File Offset: 0x0003B105
		public InputActionReference leftClick
		{
			get
			{
				return this.m_LeftClickAction;
			}
			set
			{
				this.SwapAction(ref this.m_LeftClickAction, value, this.m_ActionsHooked, this.m_OnLeftClickDelegate);
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000B54 RID: 2900 RVA: 0x0003CF20 File Offset: 0x0003B120
		// (set) Token: 0x06000B55 RID: 2901 RVA: 0x0003CF28 File Offset: 0x0003B128
		public InputActionReference middleClick
		{
			get
			{
				return this.m_MiddleClickAction;
			}
			set
			{
				this.SwapAction(ref this.m_MiddleClickAction, value, this.m_ActionsHooked, this.m_OnMiddleClickDelegate);
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000B56 RID: 2902 RVA: 0x0003CF43 File Offset: 0x0003B143
		// (set) Token: 0x06000B57 RID: 2903 RVA: 0x0003CF4B File Offset: 0x0003B14B
		public InputActionReference rightClick
		{
			get
			{
				return this.m_RightClickAction;
			}
			set
			{
				this.SwapAction(ref this.m_RightClickAction, value, this.m_ActionsHooked, this.m_OnRightClickDelegate);
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000B58 RID: 2904 RVA: 0x0003CF66 File Offset: 0x0003B166
		// (set) Token: 0x06000B59 RID: 2905 RVA: 0x0003CF6E File Offset: 0x0003B16E
		public InputActionReference move
		{
			get
			{
				return this.m_MoveAction;
			}
			set
			{
				this.SwapAction(ref this.m_MoveAction, value, this.m_ActionsHooked, this.m_OnMoveDelegate);
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000B5A RID: 2906 RVA: 0x0003CF89 File Offset: 0x0003B189
		// (set) Token: 0x06000B5B RID: 2907 RVA: 0x0003CF91 File Offset: 0x0003B191
		public InputActionReference submit
		{
			get
			{
				return this.m_SubmitAction;
			}
			set
			{
				this.SwapAction(ref this.m_SubmitAction, value, this.m_ActionsHooked, this.m_OnSubmitCancelDelegate);
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000B5C RID: 2908 RVA: 0x0003CFAC File Offset: 0x0003B1AC
		// (set) Token: 0x06000B5D RID: 2909 RVA: 0x0003CFB4 File Offset: 0x0003B1B4
		public InputActionReference cancel
		{
			get
			{
				return this.m_CancelAction;
			}
			set
			{
				this.SwapAction(ref this.m_CancelAction, value, this.m_ActionsHooked, this.m_OnSubmitCancelDelegate);
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000B5E RID: 2910 RVA: 0x0003CFCF File Offset: 0x0003B1CF
		// (set) Token: 0x06000B5F RID: 2911 RVA: 0x0003CFD7 File Offset: 0x0003B1D7
		public InputActionReference trackedDeviceOrientation
		{
			get
			{
				return this.m_TrackedDeviceOrientationAction;
			}
			set
			{
				this.SwapAction(ref this.m_TrackedDeviceOrientationAction, value, this.m_ActionsHooked, this.m_OnTrackedDeviceOrientationDelegate);
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000B60 RID: 2912 RVA: 0x0003CFF2 File Offset: 0x0003B1F2
		// (set) Token: 0x06000B61 RID: 2913 RVA: 0x0003CFFA File Offset: 0x0003B1FA
		public InputActionReference trackedDevicePosition
		{
			get
			{
				return this.m_TrackedDevicePositionAction;
			}
			set
			{
				this.SwapAction(ref this.m_TrackedDevicePositionAction, value, this.m_ActionsHooked, this.m_OnTrackedDevicePositionDelegate);
			}
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x0003D015 File Offset: 0x0003B215
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void ResetDefaultActions()
		{
			if (InputSystemUIInputModule.defaultActions != null)
			{
				InputSystemUIInputModule.defaultActions.Dispose();
				InputSystemUIInputModule.defaultActions = null;
			}
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x0003D030 File Offset: 0x0003B230
		public void AssignDefaultActions()
		{
			if (InputSystemUIInputModule.defaultActions == null)
			{
				InputSystemUIInputModule.defaultActions = new DefaultInputActions();
			}
			this.actionsAsset = InputSystemUIInputModule.defaultActions.asset;
			this.cancel = InputActionReference.Create(InputSystemUIInputModule.defaultActions.UI.Cancel);
			this.submit = InputActionReference.Create(InputSystemUIInputModule.defaultActions.UI.Submit);
			this.move = InputActionReference.Create(InputSystemUIInputModule.defaultActions.UI.Navigate);
			this.leftClick = InputActionReference.Create(InputSystemUIInputModule.defaultActions.UI.Click);
			this.rightClick = InputActionReference.Create(InputSystemUIInputModule.defaultActions.UI.RightClick);
			this.middleClick = InputActionReference.Create(InputSystemUIInputModule.defaultActions.UI.MiddleClick);
			this.point = InputActionReference.Create(InputSystemUIInputModule.defaultActions.UI.Point);
			this.scrollWheel = InputActionReference.Create(InputSystemUIInputModule.defaultActions.UI.ScrollWheel);
			this.trackedDeviceOrientation = InputActionReference.Create(InputSystemUIInputModule.defaultActions.UI.TrackedDeviceOrientation);
			this.trackedDevicePosition = InputActionReference.Create(InputSystemUIInputModule.defaultActions.UI.TrackedDevicePosition);
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x0003D180 File Offset: 0x0003B380
		public void UnassignActions()
		{
			DefaultInputActions defaultInputActions = InputSystemUIInputModule.defaultActions;
			if (defaultInputActions != null)
			{
				defaultInputActions.Dispose();
			}
			InputSystemUIInputModule.defaultActions = null;
			this.actionsAsset = null;
			this.cancel = null;
			this.submit = null;
			this.move = null;
			this.leftClick = null;
			this.rightClick = null;
			this.middleClick = null;
			this.point = null;
			this.scrollWheel = null;
			this.trackedDeviceOrientation = null;
			this.trackedDevicePosition = null;
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000B65 RID: 2917 RVA: 0x0003D1F0 File Offset: 0x0003B3F0
		// (set) Token: 0x06000B66 RID: 2918 RVA: 0x0003D1F7 File Offset: 0x0003B3F7
		[Obsolete("'trackedDeviceSelect' has been obsoleted; use 'leftClick' instead.", true)]
		public InputActionReference trackedDeviceSelect
		{
			get
			{
				throw new InvalidOperationException();
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x0003D1FE File Offset: 0x0003B3FE
		protected override void Awake()
		{
			base.Awake();
			this.m_NavigationState.Reset();
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x0003D211 File Offset: 0x0003B411
		protected override void OnDestroy()
		{
			base.OnDestroy();
			this.UnhookActions();
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x0003D220 File Offset: 0x0003B420
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.m_OnControlsChangedDelegate == null)
			{
				this.m_OnControlsChangedDelegate = new Action<object>(this.OnControlsChanged);
			}
			InputActionState.s_GlobalState.onActionControlsChanged.AddCallback(this.m_OnControlsChangedDelegate);
			if (this.HasNoActions())
			{
				this.AssignDefaultActions();
			}
			this.ResetPointers();
			this.HookActions();
			this.EnableAllActions();
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x0003D284 File Offset: 0x0003B484
		protected override void OnDisable()
		{
			this.ResetPointers();
			InputActionState.s_GlobalState.onActionControlsChanged.RemoveCallback(this.m_OnControlsChangedDelegate);
			this.DisableAllActions();
			this.UnhookActions();
			if (InputSystemUIInputModule.defaultActions != null && InputSystemUIInputModule.defaultActions.asset == this.actionsAsset)
			{
				this.UnassignActions();
			}
			base.OnDisable();
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x0003D2E4 File Offset: 0x0003B4E4
		private void ResetPointers()
		{
			for (int i = 0; i < this.m_PointerStates.length; i++)
			{
				if (this.SendPointerExitEventsAndRemovePointer(i))
				{
					i--;
				}
			}
			this.m_CurrentPointerId = -1;
			this.m_CurrentPointerIndex = -1;
			this.m_CurrentPointerType = UIPointerType.None;
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x0003D32C File Offset: 0x0003B52C
		private bool HasNoActions()
		{
			if (this.m_ActionsAsset != null)
			{
				return false;
			}
			InputActionReference pointAction = this.m_PointAction;
			if (((pointAction != null) ? pointAction.action : null) == null)
			{
				InputActionReference leftClickAction = this.m_LeftClickAction;
				if (((leftClickAction != null) ? leftClickAction.action : null) == null)
				{
					InputActionReference rightClickAction = this.m_RightClickAction;
					if (((rightClickAction != null) ? rightClickAction.action : null) == null)
					{
						InputActionReference middleClickAction = this.m_MiddleClickAction;
						if (((middleClickAction != null) ? middleClickAction.action : null) == null)
						{
							InputActionReference submitAction = this.m_SubmitAction;
							if (((submitAction != null) ? submitAction.action : null) == null)
							{
								InputActionReference cancelAction = this.m_CancelAction;
								if (((cancelAction != null) ? cancelAction.action : null) == null)
								{
									InputActionReference scrollWheelAction = this.m_ScrollWheelAction;
									if (((scrollWheelAction != null) ? scrollWheelAction.action : null) == null)
									{
										InputActionReference trackedDeviceOrientationAction = this.m_TrackedDeviceOrientationAction;
										if (((trackedDeviceOrientationAction != null) ? trackedDeviceOrientationAction.action : null) == null)
										{
											InputActionReference trackedDevicePositionAction = this.m_TrackedDevicePositionAction;
											return ((trackedDevicePositionAction != null) ? trackedDevicePositionAction.action : null) == null;
										}
									}
								}
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x0003D408 File Offset: 0x0003B608
		private void EnableAllActions()
		{
			this.EnableInputAction(this.m_PointAction);
			this.EnableInputAction(this.m_LeftClickAction);
			this.EnableInputAction(this.m_RightClickAction);
			this.EnableInputAction(this.m_MiddleClickAction);
			this.EnableInputAction(this.m_MoveAction);
			this.EnableInputAction(this.m_SubmitAction);
			this.EnableInputAction(this.m_CancelAction);
			this.EnableInputAction(this.m_ScrollWheelAction);
			this.EnableInputAction(this.m_TrackedDeviceOrientationAction);
			this.EnableInputAction(this.m_TrackedDevicePositionAction);
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x0003D490 File Offset: 0x0003B690
		private void DisableAllActions()
		{
			this.TryDisableInputAction(this.m_PointAction, true);
			this.TryDisableInputAction(this.m_LeftClickAction, true);
			this.TryDisableInputAction(this.m_RightClickAction, true);
			this.TryDisableInputAction(this.m_MiddleClickAction, true);
			this.TryDisableInputAction(this.m_MoveAction, true);
			this.TryDisableInputAction(this.m_SubmitAction, true);
			this.TryDisableInputAction(this.m_CancelAction, true);
			this.TryDisableInputAction(this.m_ScrollWheelAction, true);
			this.TryDisableInputAction(this.m_TrackedDeviceOrientationAction, true);
			this.TryDisableInputAction(this.m_TrackedDevicePositionAction, true);
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x0003D520 File Offset: 0x0003B720
		private void EnableInputAction(InputActionReference inputActionReference)
		{
			InputAction inputAction = ((inputActionReference != null) ? inputActionReference.action : null);
			if (inputAction == null)
			{
				return;
			}
			InputSystemUIInputModule.InputActionReferenceState inputActionReferenceState;
			if (InputSystemUIInputModule.s_InputActionReferenceCounts.TryGetValue(inputAction, out inputActionReferenceState))
			{
				inputActionReferenceState.refCount++;
				InputSystemUIInputModule.s_InputActionReferenceCounts[inputAction] = inputActionReferenceState;
			}
			else
			{
				inputActionReferenceState = new InputSystemUIInputModule.InputActionReferenceState
				{
					refCount = 1,
					enabledByInputModule = !inputAction.enabled
				};
				InputSystemUIInputModule.s_InputActionReferenceCounts.Add(inputAction, inputActionReferenceState);
			}
			inputAction.Enable();
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x0003D59C File Offset: 0x0003B79C
		private void TryDisableInputAction(InputActionReference inputActionReference, bool isComponentDisabling = false)
		{
			InputAction inputAction = ((inputActionReference != null) ? inputActionReference.action : null);
			if (inputAction == null)
			{
				return;
			}
			if (!base.isActiveAndEnabled && !isComponentDisabling)
			{
				return;
			}
			InputSystemUIInputModule.InputActionReferenceState inputActionReferenceState;
			if (!InputSystemUIInputModule.s_InputActionReferenceCounts.TryGetValue(inputAction, out inputActionReferenceState))
			{
				return;
			}
			if (inputActionReferenceState.refCount - 1 == 0 && inputActionReferenceState.enabledByInputModule)
			{
				inputAction.Disable();
				InputSystemUIInputModule.s_InputActionReferenceCounts.Remove(inputAction);
				return;
			}
			inputActionReferenceState.refCount--;
			InputSystemUIInputModule.s_InputActionReferenceCounts[inputAction] = inputActionReferenceState;
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x0003D614 File Offset: 0x0003B814
		private int GetPointerStateIndexFor(int pointerOrTouchId)
		{
			if (pointerOrTouchId == this.m_CurrentPointerId)
			{
				return this.m_CurrentPointerIndex;
			}
			for (int i = 0; i < this.m_PointerIds.length; i++)
			{
				if (this.m_PointerIds[i] == pointerOrTouchId)
				{
					return i;
				}
			}
			for (int j = 0; j < this.m_PointerStates.length; j++)
			{
				ExtendedPointerEventData eventData = this.m_PointerStates[j].eventData;
				if (eventData.touchId == pointerOrTouchId || (eventData.touchId != 0 && eventData.device.deviceId == pointerOrTouchId))
				{
					return j;
				}
			}
			return -1;
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x0003D6A2 File Offset: 0x0003B8A2
		private ref PointerModel GetPointerStateForIndex(int index)
		{
			if (index == 0)
			{
				return ref this.m_PointerStates.firstValue;
			}
			return ref this.m_PointerStates.additionalValues[index - 1];
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x0003D6C8 File Offset: 0x0003B8C8
		private int GetDisplayIndexFor(InputControl control)
		{
			int num = 0;
			Pointer pointer = control.device as Pointer;
			if (pointer != null)
			{
				num = pointer.displayIndex.ReadValue();
			}
			return num;
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x0003D6F4 File Offset: 0x0003B8F4
		private int GetPointerStateIndexFor(ref InputAction.CallbackContext context)
		{
			if (this.CheckForRemovedDevice(ref context))
			{
				return -1;
			}
			InputActionPhase phase = context.phase;
			return this.GetPointerStateIndexFor(context.control, phase != InputActionPhase.Canceled);
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x0003D728 File Offset: 0x0003B928
		private unsafe int GetPointerStateIndexFor(InputControl control, bool createIfNotExists = true)
		{
			InputDevice device = control.device;
			InputControl parent = control.parent;
			int num = device.deviceId;
			int num2 = 0;
			Vector2 vector = Vector2.zero;
			TouchControl touchControl = parent as TouchControl;
			if (touchControl != null)
			{
				num2 = *touchControl.touchId.value;
				vector = *touchControl.position.value;
			}
			else
			{
				Touchscreen touchscreen = parent as Touchscreen;
				if (touchscreen != null)
				{
					num2 = *touchscreen.primaryTouch.touchId.value;
					vector = *touchscreen.primaryTouch.position.value;
				}
			}
			int displayIndexFor = this.GetDisplayIndexFor(control);
			if (num2 != 0)
			{
				num = ExtendedPointerEventData.MakePointerIdForTouch(num, num2);
			}
			if (this.m_CurrentPointerId == num)
			{
				return this.m_CurrentPointerIndex;
			}
			for (int i = 0; i < this.m_PointerIds.length; i++)
			{
				if (this.m_PointerIds[i] == num)
				{
					this.m_CurrentPointerId = num;
					this.m_CurrentPointerIndex = i;
					this.m_CurrentPointerType = this.m_PointerStates[i].pointerType;
					return i;
				}
			}
			if (!createIfNotExists)
			{
				return -1;
			}
			UIPointerType uipointerType = UIPointerType.None;
			if (num2 != 0)
			{
				uipointerType = UIPointerType.Touch;
			}
			else if (InputSystemUIInputModule.HaveControlForDevice(device, this.point))
			{
				uipointerType = UIPointerType.MouseOrPen;
			}
			else if (InputSystemUIInputModule.HaveControlForDevice(device, this.trackedDevicePosition))
			{
				uipointerType = UIPointerType.Tracked;
			}
			if ((this.m_PointerBehavior == UIPointerBehavior.SingleUnifiedPointer && uipointerType != UIPointerType.None) || (this.m_PointerBehavior == UIPointerBehavior.SingleMouseOrPenButMultiTouchAndTrack && uipointerType == UIPointerType.MouseOrPen))
			{
				if (this.m_CurrentPointerIndex == -1)
				{
					this.m_CurrentPointerIndex = this.AllocatePointer(num, displayIndexFor, num2, uipointerType, control, device, (num2 != 0) ? parent : null);
				}
				else
				{
					ref PointerModel pointerStateForIndex = ref this.GetPointerStateForIndex(this.m_CurrentPointerIndex);
					ExtendedPointerEventData eventData = pointerStateForIndex.eventData;
					eventData.control = control;
					eventData.device = device;
					eventData.pointerType = uipointerType;
					eventData.pointerId = num;
					eventData.touchId = num2;
					eventData.displayIndex = displayIndexFor;
					eventData.trackedDeviceOrientation = default(Quaternion);
					eventData.trackedDevicePosition = default(Vector3);
					if (this.m_PointerBehavior == UIPointerBehavior.SingleUnifiedPointer)
					{
						pointerStateForIndex.leftButton.OnEndFrame();
						pointerStateForIndex.rightButton.OnEndFrame();
						pointerStateForIndex.middleButton.OnEndFrame();
					}
				}
				if (uipointerType == UIPointerType.Touch)
				{
					this.GetPointerStateForIndex(this.m_CurrentPointerIndex).screenPosition = vector;
				}
				this.m_CurrentPointerId = num;
				this.m_CurrentPointerType = uipointerType;
				return this.m_CurrentPointerIndex;
			}
			int num3;
			if (uipointerType != UIPointerType.None)
			{
				num3 = this.AllocatePointer(num, displayIndexFor, num2, uipointerType, control, device, (num2 != 0) ? parent : null);
			}
			else
			{
				if (this.m_CurrentPointerId != -1)
				{
					return this.m_CurrentPointerIndex;
				}
				InputActionReference point = this.point;
				ReadOnlyArray<InputControl>? readOnlyArray;
				if (point == null)
				{
					readOnlyArray = null;
				}
				else
				{
					InputAction action = point.action;
					readOnlyArray = ((action != null) ? new ReadOnlyArray<InputControl>?(action.controls) : null);
				}
				ReadOnlyArray<InputControl>? readOnlyArray2 = readOnlyArray;
				InputDevice inputDevice = ((readOnlyArray2 != null && readOnlyArray2.Value.Count > 0) ? readOnlyArray2.Value[0].device : null);
				if (inputDevice != null && !(inputDevice is Touchscreen))
				{
					num3 = this.AllocatePointer(inputDevice.deviceId, displayIndexFor, 0, UIPointerType.MouseOrPen, readOnlyArray2.Value[0], inputDevice, null);
				}
				else
				{
					InputActionReference trackedDevicePosition = this.trackedDevicePosition;
					ReadOnlyArray<InputControl>? readOnlyArray3;
					if (trackedDevicePosition == null)
					{
						readOnlyArray3 = null;
					}
					else
					{
						InputAction action2 = trackedDevicePosition.action;
						readOnlyArray3 = ((action2 != null) ? new ReadOnlyArray<InputControl>?(action2.controls) : null);
					}
					ReadOnlyArray<InputControl>? readOnlyArray4 = readOnlyArray3;
					InputDevice inputDevice2 = ((readOnlyArray4 != null && readOnlyArray4.Value.Count > 0) ? readOnlyArray4.Value[0].device : null);
					if (inputDevice2 != null)
					{
						num3 = this.AllocatePointer(inputDevice2.deviceId, displayIndexFor, 0, UIPointerType.Tracked, readOnlyArray4.Value[0], inputDevice2, null);
					}
					else
					{
						num3 = this.AllocatePointer(num, displayIndexFor, 0, UIPointerType.None, control, device, null);
					}
				}
			}
			if (uipointerType == UIPointerType.Touch)
			{
				this.GetPointerStateForIndex(num3).screenPosition = vector;
			}
			this.m_CurrentPointerId = num;
			this.m_CurrentPointerIndex = num3;
			this.m_CurrentPointerType = uipointerType;
			return num3;
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x0003DB20 File Offset: 0x0003BD20
		private int AllocatePointer(int pointerId, int displayIndex, int touchId, UIPointerType pointerType, InputControl control, InputDevice device, InputControl touchControl = null)
		{
			ExtendedPointerEventData extendedPointerEventData = null;
			if (this.m_PointerStates.Capacity > this.m_PointerStates.length)
			{
				if (this.m_PointerStates.length == 0)
				{
					extendedPointerEventData = this.m_PointerStates.firstValue.eventData;
				}
				else
				{
					extendedPointerEventData = this.m_PointerStates.additionalValues[this.m_PointerStates.length - 1].eventData;
				}
			}
			if (extendedPointerEventData == null)
			{
				extendedPointerEventData = new ExtendedPointerEventData(base.eventSystem);
			}
			extendedPointerEventData.pointerId = pointerId;
			extendedPointerEventData.displayIndex = displayIndex;
			extendedPointerEventData.touchId = touchId;
			extendedPointerEventData.pointerType = pointerType;
			extendedPointerEventData.control = control;
			extendedPointerEventData.device = device;
			this.m_PointerIds.AppendWithCapacity(pointerId, 10);
			return this.m_PointerStates.AppendWithCapacity(new PointerModel(extendedPointerEventData), 10);
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x0003DBE8 File Offset: 0x0003BDE8
		private bool SendPointerExitEventsAndRemovePointer(int index)
		{
			ExtendedPointerEventData eventData = this.m_PointerStates[index].eventData;
			if (eventData.pointerEnter != null)
			{
				this.ProcessPointerMovement(eventData, null);
			}
			return this.RemovePointerAtIndex(index);
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x0003DC24 File Offset: 0x0003BE24
		private bool RemovePointerAtIndex(int index)
		{
			if (this.m_PointerStates.length == 0)
			{
				return false;
			}
			ExtendedPointerEventData eventData = this.m_PointerStates[index].eventData;
			if (index == this.m_CurrentPointerIndex)
			{
				this.m_CurrentPointerId = -1;
				this.m_CurrentPointerIndex = -1;
				this.m_CurrentPointerType = UIPointerType.None;
			}
			else if (this.m_CurrentPointerIndex == this.m_PointerIds.length - 1)
			{
				this.m_CurrentPointerIndex = index;
			}
			this.m_PointerIds.RemoveAtByMovingTailWithCapacity(index);
			this.m_PointerStates.RemoveAtByMovingTailWithCapacity(index);
			eventData.hovered.Clear();
			eventData.device = null;
			eventData.pointerCurrentRaycast = default(RaycastResult);
			eventData.pointerPressRaycast = default(RaycastResult);
			eventData.pointerPress = null;
			eventData.pointerPress = null;
			eventData.pointerDrag = null;
			eventData.pointerEnter = null;
			eventData.rawPointerPress = null;
			if (this.m_PointerStates.length == 0)
			{
				this.m_PointerStates.firstValue.eventData = eventData;
			}
			else
			{
				this.m_PointerStates.additionalValues[this.m_PointerStates.length - 1].eventData = eventData;
			}
			return true;
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x0003DD40 File Offset: 0x0003BF40
		private void PurgeStalePointers()
		{
			for (int i = 0; i < this.m_PointerStates.length; i++)
			{
				InputDevice device = this.GetPointerStateForIndex(i).eventData.device;
				if ((!device.added || (!InputSystemUIInputModule.HaveControlForDevice(device, this.point) && !InputSystemUIInputModule.HaveControlForDevice(device, this.trackedDevicePosition) && !InputSystemUIInputModule.HaveControlForDevice(device, this.trackedDeviceOrientation))) && this.SendPointerExitEventsAndRemovePointer(i))
				{
					i--;
				}
			}
			this.m_NeedToPurgeStalePointers = false;
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x0003DDBC File Offset: 0x0003BFBC
		private static bool HaveControlForDevice(InputDevice device, InputActionReference actionReference)
		{
			InputAction inputAction = ((actionReference != null) ? actionReference.action : null);
			if (inputAction == null)
			{
				return false;
			}
			ReadOnlyArray<InputControl> controls = inputAction.controls;
			for (int i = 0; i < controls.Count; i++)
			{
				if (controls[i].device == device)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x0003DE08 File Offset: 0x0003C008
		private void OnPointCallback(InputAction.CallbackContext context)
		{
			if (this.CheckForRemovedDevice(ref context) || context.canceled)
			{
				return;
			}
			int pointerStateIndexFor = this.GetPointerStateIndexFor(context.control, true);
			if (pointerStateIndexFor == -1)
			{
				return;
			}
			ref PointerModel pointerStateForIndex = ref this.GetPointerStateForIndex(pointerStateIndexFor);
			pointerStateForIndex.screenPosition = context.ReadValue<Vector2>();
			pointerStateForIndex.eventData.displayIndex = this.GetDisplayIndexFor(context.control);
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x0003DE68 File Offset: 0x0003C068
		private bool IgnoreNextClick(ref InputAction.CallbackContext context, bool wasPressed)
		{
			return !this.explictlyIgnoreFocus && (context.canceled && !InputRuntime.s_Instance.isPlayerFocused && !context.control.device.canRunInBackground && wasPressed);
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x0003DEA0 File Offset: 0x0003C0A0
		private void OnLeftClickCallback(InputAction.CallbackContext context)
		{
			int pointerStateIndexFor = this.GetPointerStateIndexFor(ref context);
			if (pointerStateIndexFor == -1)
			{
				return;
			}
			ref PointerModel pointerStateForIndex = ref this.GetPointerStateForIndex(pointerStateIndexFor);
			bool isPressed = pointerStateForIndex.leftButton.isPressed;
			pointerStateForIndex.leftButton.isPressed = context.ReadValueAsButton();
			pointerStateForIndex.changedThisFrame = true;
			if (this.IgnoreNextClick(ref context, isPressed))
			{
				pointerStateForIndex.leftButton.ignoreNextClick = true;
			}
			pointerStateForIndex.eventData.displayIndex = this.GetDisplayIndexFor(context.control);
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x0003DF18 File Offset: 0x0003C118
		private void OnRightClickCallback(InputAction.CallbackContext context)
		{
			int pointerStateIndexFor = this.GetPointerStateIndexFor(ref context);
			if (pointerStateIndexFor == -1)
			{
				return;
			}
			ref PointerModel pointerStateForIndex = ref this.GetPointerStateForIndex(pointerStateIndexFor);
			bool isPressed = pointerStateForIndex.rightButton.isPressed;
			pointerStateForIndex.rightButton.isPressed = context.ReadValueAsButton();
			pointerStateForIndex.changedThisFrame = true;
			if (this.IgnoreNextClick(ref context, isPressed))
			{
				pointerStateForIndex.rightButton.ignoreNextClick = true;
			}
			pointerStateForIndex.eventData.displayIndex = this.GetDisplayIndexFor(context.control);
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x0003DF90 File Offset: 0x0003C190
		private void OnMiddleClickCallback(InputAction.CallbackContext context)
		{
			int pointerStateIndexFor = this.GetPointerStateIndexFor(ref context);
			if (pointerStateIndexFor == -1)
			{
				return;
			}
			ref PointerModel pointerStateForIndex = ref this.GetPointerStateForIndex(pointerStateIndexFor);
			bool isPressed = pointerStateForIndex.middleButton.isPressed;
			pointerStateForIndex.middleButton.isPressed = context.ReadValueAsButton();
			pointerStateForIndex.changedThisFrame = true;
			if (this.IgnoreNextClick(ref context, isPressed))
			{
				pointerStateForIndex.middleButton.ignoreNextClick = true;
			}
			pointerStateForIndex.eventData.displayIndex = this.GetDisplayIndexFor(context.control);
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x0003E007 File Offset: 0x0003C207
		private bool CheckForRemovedDevice(ref InputAction.CallbackContext context)
		{
			if (context.canceled && !context.control.device.added)
			{
				this.m_NeedToPurgeStalePointers = true;
				return true;
			}
			return false;
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x0003E030 File Offset: 0x0003C230
		private void OnScrollCallback(InputAction.CallbackContext context)
		{
			int pointerStateIndexFor = this.GetPointerStateIndexFor(ref context);
			if (pointerStateIndexFor == -1)
			{
				return;
			}
			ref PointerModel pointerStateForIndex = ref this.GetPointerStateForIndex(pointerStateIndexFor);
			Vector2 vector = context.ReadValue<Vector2>();
			pointerStateForIndex.scrollDelta = vector / InputSystem.scrollWheelDeltaPerTick * this.scrollDeltaPerTick;
			pointerStateForIndex.eventData.displayIndex = this.GetDisplayIndexFor(context.control);
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x0003E08D File Offset: 0x0003C28D
		private void OnMoveCallback(InputAction.CallbackContext context)
		{
			this.m_NavigationState.move = context.ReadValue<Vector2>();
			this.m_NavigationState.device = context.control.device;
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x0003E0B8 File Offset: 0x0003C2B8
		private void OnSubmitCancelCallback(InputAction.CallbackContext context)
		{
			this.m_SubmitCancelState.device = context.control.device;
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x0003E0D4 File Offset: 0x0003C2D4
		private void OnTrackedDeviceOrientationCallback(InputAction.CallbackContext context)
		{
			int pointerStateIndexFor = this.GetPointerStateIndexFor(ref context);
			if (pointerStateIndexFor == -1)
			{
				return;
			}
			ref PointerModel pointerStateForIndex = ref this.GetPointerStateForIndex(pointerStateIndexFor);
			pointerStateForIndex.worldOrientation = context.ReadValue<Quaternion>();
			pointerStateForIndex.eventData.displayIndex = this.GetDisplayIndexFor(context.control);
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x0003E11C File Offset: 0x0003C31C
		private void OnTrackedDevicePositionCallback(InputAction.CallbackContext context)
		{
			int pointerStateIndexFor = this.GetPointerStateIndexFor(ref context);
			if (pointerStateIndexFor == -1)
			{
				return;
			}
			ref PointerModel pointerStateForIndex = ref this.GetPointerStateForIndex(pointerStateIndexFor);
			pointerStateForIndex.worldPosition = context.ReadValue<Vector3>();
			pointerStateForIndex.eventData.displayIndex = this.GetDisplayIndexFor(context.control);
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x0003E162 File Offset: 0x0003C362
		private void OnControlsChanged(object obj)
		{
			this.m_NeedToPurgeStalePointers = true;
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x0003E16C File Offset: 0x0003C36C
		private void FilterPointerStatesByType()
		{
			UIPointerType uipointerType = UIPointerType.None;
			for (int i = 0; i < this.m_PointerStates.length; i++)
			{
				ref PointerModel pointerStateForIndex = ref this.GetPointerStateForIndex(i);
				pointerStateForIndex.eventData.ReadDeviceState();
				pointerStateForIndex.CopyTouchOrPenStateFrom(pointerStateForIndex.eventData);
				if (pointerStateForIndex.changedThisFrame && uipointerType == UIPointerType.None)
				{
					uipointerType = pointerStateForIndex.pointerType;
				}
			}
			if (this.m_PointerBehavior == UIPointerBehavior.SingleMouseOrPenButMultiTouchAndTrack && uipointerType != UIPointerType.None)
			{
				if (uipointerType == UIPointerType.MouseOrPen)
				{
					for (int j = 0; j < this.m_PointerStates.length; j++)
					{
						if (this.m_PointerStates[j].pointerType != UIPointerType.MouseOrPen && this.SendPointerExitEventsAndRemovePointer(j))
						{
							j--;
						}
					}
					return;
				}
				for (int k = 0; k < this.m_PointerStates.length; k++)
				{
					if (this.m_PointerStates[k].pointerType == UIPointerType.MouseOrPen && this.SendPointerExitEventsAndRemovePointer(k))
					{
						k--;
					}
				}
			}
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x0003E258 File Offset: 0x0003C458
		public override void Process()
		{
			if (this.m_NeedToPurgeStalePointers)
			{
				this.PurgeStalePointers();
			}
			if (!base.eventSystem.isFocused && !this.shouldIgnoreFocus)
			{
				for (int i = 0; i < this.m_PointerStates.length; i++)
				{
					this.m_PointerStates[i].OnFrameFinished();
				}
				return;
			}
			this.ProcessNavigation(ref this.m_NavigationState);
			this.FilterPointerStatesByType();
			for (int j = 0; j < this.m_PointerStates.length; j++)
			{
				ref PointerModel pointerStateForIndex = ref this.GetPointerStateForIndex(j);
				this.ProcessPointer(ref pointerStateForIndex);
				if (pointerStateForIndex.pointerType == UIPointerType.Touch && !pointerStateForIndex.leftButton.isPressed && !pointerStateForIndex.leftButton.wasReleasedThisFrame)
				{
					if (this.RemovePointerAtIndex(j))
					{
						j--;
					}
				}
				else
				{
					pointerStateForIndex.OnFrameFinished();
				}
			}
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x0003E324 File Offset: 0x0003C524
		public override int ConvertUIToolkitPointerId(PointerEventData sourcePointerData)
		{
			if (this.m_PointerBehavior == UIPointerBehavior.SingleUnifiedPointer)
			{
				return PointerId.mousePointerId;
			}
			ExtendedPointerEventData extendedPointerEventData = sourcePointerData as ExtendedPointerEventData;
			if (extendedPointerEventData == null)
			{
				return base.ConvertUIToolkitPointerId(sourcePointerData);
			}
			return extendedPointerEventData.uiToolkitPointerId;
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x0003E358 File Offset: 0x0003C558
		public override Vector2 ConvertPointerEventScrollDeltaToTicks(Vector2 scrollDelta)
		{
			if (Mathf.Abs(this.scrollDeltaPerTick) < 1E-05f)
			{
				return Vector2.zero;
			}
			return scrollDelta / this.scrollDeltaPerTick;
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x0003E380 File Offset: 0x0003C580
		private void HookActions()
		{
			if (this.m_ActionsHooked)
			{
				return;
			}
			if (this.m_OnPointDelegate == null)
			{
				this.m_OnPointDelegate = new Action<InputAction.CallbackContext>(this.OnPointCallback);
			}
			if (this.m_OnLeftClickDelegate == null)
			{
				this.m_OnLeftClickDelegate = new Action<InputAction.CallbackContext>(this.OnLeftClickCallback);
			}
			if (this.m_OnRightClickDelegate == null)
			{
				this.m_OnRightClickDelegate = new Action<InputAction.CallbackContext>(this.OnRightClickCallback);
			}
			if (this.m_OnMiddleClickDelegate == null)
			{
				this.m_OnMiddleClickDelegate = new Action<InputAction.CallbackContext>(this.OnMiddleClickCallback);
			}
			if (this.m_OnScrollWheelDelegate == null)
			{
				this.m_OnScrollWheelDelegate = new Action<InputAction.CallbackContext>(this.OnScrollCallback);
			}
			if (this.m_OnMoveDelegate == null)
			{
				this.m_OnMoveDelegate = new Action<InputAction.CallbackContext>(this.OnMoveCallback);
			}
			if (this.m_OnSubmitCancelDelegate == null)
			{
				this.m_OnSubmitCancelDelegate = new Action<InputAction.CallbackContext>(this.OnSubmitCancelCallback);
			}
			if (this.m_OnTrackedDeviceOrientationDelegate == null)
			{
				this.m_OnTrackedDeviceOrientationDelegate = new Action<InputAction.CallbackContext>(this.OnTrackedDeviceOrientationCallback);
			}
			if (this.m_OnTrackedDevicePositionDelegate == null)
			{
				this.m_OnTrackedDevicePositionDelegate = new Action<InputAction.CallbackContext>(this.OnTrackedDevicePositionCallback);
			}
			this.SetActionCallbacks(true);
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x0003E487 File Offset: 0x0003C687
		private void UnhookActions()
		{
			if (!this.m_ActionsHooked)
			{
				return;
			}
			this.SetActionCallbacks(false);
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x0003E49C File Offset: 0x0003C69C
		private void SetActionCallbacks(bool install)
		{
			this.m_ActionsHooked = install;
			InputSystemUIInputModule.SetActionCallback(this.m_PointAction, this.m_OnPointDelegate, install);
			InputSystemUIInputModule.SetActionCallback(this.m_MoveAction, this.m_OnMoveDelegate, install);
			InputSystemUIInputModule.SetActionCallback(this.m_SubmitAction, this.m_OnSubmitCancelDelegate, install);
			InputSystemUIInputModule.SetActionCallback(this.m_CancelAction, this.m_OnSubmitCancelDelegate, install);
			InputSystemUIInputModule.SetActionCallback(this.m_LeftClickAction, this.m_OnLeftClickDelegate, install);
			InputSystemUIInputModule.SetActionCallback(this.m_RightClickAction, this.m_OnRightClickDelegate, install);
			InputSystemUIInputModule.SetActionCallback(this.m_MiddleClickAction, this.m_OnMiddleClickDelegate, install);
			InputSystemUIInputModule.SetActionCallback(this.m_ScrollWheelAction, this.m_OnScrollWheelDelegate, install);
			InputSystemUIInputModule.SetActionCallback(this.m_TrackedDeviceOrientationAction, this.m_OnTrackedDeviceOrientationDelegate, install);
			InputSystemUIInputModule.SetActionCallback(this.m_TrackedDevicePositionAction, this.m_OnTrackedDevicePositionDelegate, install);
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x0003E564 File Offset: 0x0003C764
		private static void SetActionCallback(InputActionReference actionReference, Action<InputAction.CallbackContext> callback, bool install)
		{
			if (!install && callback == null)
			{
				return;
			}
			if (actionReference == null)
			{
				return;
			}
			InputAction action = actionReference.action;
			if (action == null)
			{
				return;
			}
			if (install)
			{
				action.performed += callback;
				action.canceled += callback;
				return;
			}
			action.performed -= callback;
			action.canceled -= callback;
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x0003E5B0 File Offset: 0x0003C7B0
		private InputActionReference UpdateReferenceForNewAsset(InputActionReference actionReference)
		{
			InputAction inputAction = ((actionReference != null) ? actionReference.action : null);
			if (inputAction == null)
			{
				return null;
			}
			InputActionMap actionMap = inputAction.actionMap;
			InputActionAsset actionsAsset = this.m_ActionsAsset;
			InputActionMap inputActionMap = ((actionsAsset != null) ? actionsAsset.FindActionMap(actionMap.name, false) : null);
			if (inputActionMap == null)
			{
				return null;
			}
			InputAction inputAction2 = inputActionMap.FindAction(inputAction.name, false);
			if (inputAction2 == null)
			{
				return null;
			}
			return InputActionReference.Create(inputAction2);
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000B90 RID: 2960 RVA: 0x0003E60E File Offset: 0x0003C80E
		// (set) Token: 0x06000B91 RID: 2961 RVA: 0x0003E618 File Offset: 0x0003C818
		public InputActionAsset actionsAsset
		{
			get
			{
				return this.m_ActionsAsset;
			}
			set
			{
				if (value != this.m_ActionsAsset)
				{
					this.UnhookActions();
					this.m_ActionsAsset = value;
					this.point = this.UpdateReferenceForNewAsset(this.point);
					this.move = this.UpdateReferenceForNewAsset(this.move);
					this.leftClick = this.UpdateReferenceForNewAsset(this.leftClick);
					this.rightClick = this.UpdateReferenceForNewAsset(this.rightClick);
					this.middleClick = this.UpdateReferenceForNewAsset(this.middleClick);
					this.scrollWheel = this.UpdateReferenceForNewAsset(this.scrollWheel);
					this.submit = this.UpdateReferenceForNewAsset(this.submit);
					this.cancel = this.UpdateReferenceForNewAsset(this.cancel);
					this.trackedDeviceOrientation = this.UpdateReferenceForNewAsset(this.trackedDeviceOrientation);
					this.trackedDevicePosition = this.UpdateReferenceForNewAsset(this.trackedDevicePosition);
					this.HookActions();
				}
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000B92 RID: 2962 RVA: 0x0003E6FD File Offset: 0x0003C8FD
		// (set) Token: 0x06000B93 RID: 2963 RVA: 0x0003E705 File Offset: 0x0003C905
		internal new bool sendPointerHoverToParent
		{
			get
			{
				return base.sendPointerHoverToParent;
			}
			set
			{
				base.sendPointerHoverToParent = value;
			}
		}

		// Token: 0x040003FC RID: 1020
		private const float kClickSpeed = 0.3f;

		// Token: 0x040003FD RID: 1021
		[FormerlySerializedAs("m_RepeatDelay")]
		[Tooltip("The Initial delay (in seconds) between an initial move action and a repeated move action.")]
		[SerializeField]
		private float m_MoveRepeatDelay = 0.5f;

		// Token: 0x040003FE RID: 1022
		[FormerlySerializedAs("m_RepeatRate")]
		[Tooltip("The speed (in seconds) that the move action repeats itself once repeating (max 1 per frame).")]
		[SerializeField]
		private float m_MoveRepeatRate = 0.1f;

		// Token: 0x040003FF RID: 1023
		[Tooltip("Scales the Eventsystem.DragThreshold, for tracked devices, to make selection easier.")]
		private float m_TrackedDeviceDragThresholdMultiplier = 2f;

		// Token: 0x04000400 RID: 1024
		[Tooltip("Transform representing the real world origin for tracking devices. When using the XR Interaction Toolkit, this should be pointing to the XR Rig's Transform.")]
		[SerializeField]
		private Transform m_XRTrackingOrigin;

		// Token: 0x04000401 RID: 1025
		private static DefaultInputActions defaultActions;

		// Token: 0x04000402 RID: 1026
		private const float kSmallestScrollDeltaPerTick = 1E-05f;

		// Token: 0x04000403 RID: 1027
		[SerializeField]
		[HideInInspector]
		private InputActionAsset m_ActionsAsset;

		// Token: 0x04000404 RID: 1028
		[SerializeField]
		[HideInInspector]
		private InputActionReference m_PointAction;

		// Token: 0x04000405 RID: 1029
		[SerializeField]
		[HideInInspector]
		private InputActionReference m_MoveAction;

		// Token: 0x04000406 RID: 1030
		[SerializeField]
		[HideInInspector]
		private InputActionReference m_SubmitAction;

		// Token: 0x04000407 RID: 1031
		[SerializeField]
		[HideInInspector]
		private InputActionReference m_CancelAction;

		// Token: 0x04000408 RID: 1032
		[SerializeField]
		[HideInInspector]
		private InputActionReference m_LeftClickAction;

		// Token: 0x04000409 RID: 1033
		[SerializeField]
		[HideInInspector]
		private InputActionReference m_MiddleClickAction;

		// Token: 0x0400040A RID: 1034
		[SerializeField]
		[HideInInspector]
		private InputActionReference m_RightClickAction;

		// Token: 0x0400040B RID: 1035
		[SerializeField]
		[HideInInspector]
		private InputActionReference m_ScrollWheelAction;

		// Token: 0x0400040C RID: 1036
		[SerializeField]
		[HideInInspector]
		private InputActionReference m_TrackedDevicePositionAction;

		// Token: 0x0400040D RID: 1037
		[SerializeField]
		[HideInInspector]
		private InputActionReference m_TrackedDeviceOrientationAction;

		// Token: 0x0400040E RID: 1038
		[SerializeField]
		private bool m_DeselectOnBackgroundClick = true;

		// Token: 0x0400040F RID: 1039
		[SerializeField]
		private UIPointerBehavior m_PointerBehavior;

		// Token: 0x04000410 RID: 1040
		[SerializeField]
		[HideInInspector]
		internal InputSystemUIInputModule.CursorLockBehavior m_CursorLockBehavior;

		// Token: 0x04000411 RID: 1041
		[SerializeField]
		private float m_ScrollDeltaPerTick = 6f;

		// Token: 0x04000412 RID: 1042
		private static Dictionary<InputAction, InputSystemUIInputModule.InputActionReferenceState> s_InputActionReferenceCounts = new Dictionary<InputAction, InputSystemUIInputModule.InputActionReferenceState>();

		// Token: 0x04000413 RID: 1043
		[NonSerialized]
		private bool m_ActionsHooked;

		// Token: 0x04000414 RID: 1044
		[NonSerialized]
		private bool m_NeedToPurgeStalePointers;

		// Token: 0x04000415 RID: 1045
		private Action<InputAction.CallbackContext> m_OnPointDelegate;

		// Token: 0x04000416 RID: 1046
		private Action<InputAction.CallbackContext> m_OnMoveDelegate;

		// Token: 0x04000417 RID: 1047
		private Action<InputAction.CallbackContext> m_OnSubmitCancelDelegate;

		// Token: 0x04000418 RID: 1048
		private Action<InputAction.CallbackContext> m_OnLeftClickDelegate;

		// Token: 0x04000419 RID: 1049
		private Action<InputAction.CallbackContext> m_OnRightClickDelegate;

		// Token: 0x0400041A RID: 1050
		private Action<InputAction.CallbackContext> m_OnMiddleClickDelegate;

		// Token: 0x0400041B RID: 1051
		private Action<InputAction.CallbackContext> m_OnScrollWheelDelegate;

		// Token: 0x0400041C RID: 1052
		private Action<InputAction.CallbackContext> m_OnTrackedDevicePositionDelegate;

		// Token: 0x0400041D RID: 1053
		private Action<InputAction.CallbackContext> m_OnTrackedDeviceOrientationDelegate;

		// Token: 0x0400041E RID: 1054
		private Action<object> m_OnControlsChangedDelegate;

		// Token: 0x0400041F RID: 1055
		[NonSerialized]
		private int m_CurrentPointerId = -1;

		// Token: 0x04000420 RID: 1056
		[NonSerialized]
		private int m_CurrentPointerIndex = -1;

		// Token: 0x04000421 RID: 1057
		[NonSerialized]
		internal UIPointerType m_CurrentPointerType;

		// Token: 0x04000422 RID: 1058
		internal InlinedArray<int> m_PointerIds;

		// Token: 0x04000423 RID: 1059
		internal InlinedArray<PointerModel> m_PointerStates;

		// Token: 0x04000424 RID: 1060
		private NavigationModel m_NavigationState;

		// Token: 0x04000425 RID: 1061
		private SubmitCancelModel m_SubmitCancelState;

		// Token: 0x04000426 RID: 1062
		[NonSerialized]
		private GameObject m_LocalMultiPlayerRoot;

		// Token: 0x020001D6 RID: 470
		private struct InputActionReferenceState
		{
			// Token: 0x040009C3 RID: 2499
			public int refCount;

			// Token: 0x040009C4 RID: 2500
			public bool enabledByInputModule;
		}

		// Token: 0x020001D7 RID: 471
		public enum CursorLockBehavior
		{
			// Token: 0x040009C6 RID: 2502
			OutsideScreen,
			// Token: 0x040009C7 RID: 2503
			ScreenCenter
		}
	}
}
