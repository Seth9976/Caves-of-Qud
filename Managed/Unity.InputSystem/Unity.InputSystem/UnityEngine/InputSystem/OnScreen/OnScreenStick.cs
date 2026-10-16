using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UnityEngine.InputSystem.OnScreen
{
	// Token: 0x02000099 RID: 153
	[AddComponentMenu("Input/On-Screen Stick")]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.inputsystem@1.19/manual/OnScreen.html#on-screen-sticks")]
	public class OnScreenStick : OnScreenControl, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler, IDragHandler
	{
		// Token: 0x06000C12 RID: 3090 RVA: 0x000408D5 File Offset: 0x0003EAD5
		public void OnPointerDown(PointerEventData eventData)
		{
			if (this.m_UseIsolatedInputActions)
			{
				return;
			}
			if (eventData == null)
			{
				throw new ArgumentNullException("eventData");
			}
			this.BeginInteraction(eventData.position, eventData.pressEventCamera);
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x00040900 File Offset: 0x0003EB00
		public void OnDrag(PointerEventData eventData)
		{
			if (this.m_UseIsolatedInputActions)
			{
				return;
			}
			if (eventData == null)
			{
				throw new ArgumentNullException("eventData");
			}
			this.MoveStick(eventData.position, eventData.pressEventCamera);
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x0004092B File Offset: 0x0003EB2B
		public void OnPointerUp(PointerEventData eventData)
		{
			if (this.m_UseIsolatedInputActions)
			{
				return;
			}
			this.EndInteraction();
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x0004093C File Offset: 0x0003EB3C
		private void Start()
		{
			if (this.m_UseIsolatedInputActions)
			{
				this.m_RaycastResults = new List<RaycastResult>();
				this.m_PointerEventData = new PointerEventData(EventSystem.current);
				if (this.m_PointerDownAction == null || this.m_PointerDownAction.bindings.Count == 0)
				{
					if (this.m_PointerDownAction == null)
					{
						this.m_PointerDownAction = new InputAction(null, InputActionType.PassThrough, null, null, null, null);
					}
					else if (this.m_PointerDownAction.m_Type != InputActionType.PassThrough)
					{
						this.m_PointerDownAction.m_Type = InputActionType.PassThrough;
					}
					this.m_PointerDownAction.AddBinding("<Mouse>/leftButton", null, null, null);
					this.m_PointerDownAction.AddBinding("<Pen>/tip", null, null, null);
					this.m_PointerDownAction.AddBinding("<Touchscreen>/touch*/press", null, null, null);
					this.m_PointerDownAction.AddBinding("<XRController>/trigger", null, null, null);
				}
				if (this.m_PointerMoveAction == null || this.m_PointerMoveAction.bindings.Count == 0)
				{
					if (this.m_PointerMoveAction == null)
					{
						this.m_PointerMoveAction = new InputAction();
					}
					this.m_PointerMoveAction.AddBinding("<Mouse>/position", null, null, null);
					this.m_PointerMoveAction.AddBinding("<Pen>/position", null, null, null);
					this.m_PointerMoveAction.AddBinding("<Touchscreen>/touch*/position", null, null, null);
				}
				this.m_PointerDownAction.performed += this.OnPointerChanged;
				this.m_PointerDownAction.Enable();
				this.m_PointerMoveAction.Enable();
			}
			if (!(base.transform is RectTransform))
			{
				return;
			}
			this.m_StartPos = ((RectTransform)base.transform).anchoredPosition;
			if (this.m_Behaviour != OnScreenStick.Behaviour.ExactPositionWithDynamicOrigin)
			{
				return;
			}
			this.m_PointerDownPos = this.m_StartPos;
			GameObject gameObject = new GameObject("DynamicOriginClickable", new Type[] { typeof(Image) });
			gameObject.transform.SetParent(base.transform);
			Image component = gameObject.GetComponent<Image>();
			component.color = new Color(1f, 1f, 1f, 0f);
			RectTransform rectTransform = (RectTransform)gameObject.transform;
			rectTransform.sizeDelta = new Vector2(this.m_DynamicOriginRange * 2f, this.m_DynamicOriginRange * 2f);
			rectTransform.localScale = new Vector3(1f, 1f, 0f);
			rectTransform.anchoredPosition3D = Vector3.zero;
			component.sprite = SpriteUtilities.CreateCircleSprite(16, new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue));
			component.alphaHitTestMinimumThreshold = 0.5f;
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x00040BC7 File Offset: 0x0003EDC7
		private void OnDestroy()
		{
			if (this.m_UseIsolatedInputActions)
			{
				this.m_PointerDownAction.performed -= this.OnPointerChanged;
			}
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x00040BE8 File Offset: 0x0003EDE8
		private void BeginInteraction(Vector2 pointerPosition, Camera uiCamera)
		{
			RectTransform canvasRectTransform = UGUIOnScreenControlUtils.GetCanvasRectTransform(base.transform);
			if (canvasRectTransform == null)
			{
				Debug.LogError(base.GetWarningMessage());
				return;
			}
			switch (this.m_Behaviour)
			{
			case OnScreenStick.Behaviour.RelativePositionWithStaticOrigin:
				RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, pointerPosition, uiCamera, out this.m_PointerDownPos);
				return;
			case OnScreenStick.Behaviour.ExactPositionWithStaticOrigin:
				RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, pointerPosition, uiCamera, out this.m_PointerDownPos);
				this.MoveStick(pointerPosition, uiCamera);
				return;
			case OnScreenStick.Behaviour.ExactPositionWithDynamicOrigin:
			{
				Vector2 vector;
				RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, pointerPosition, uiCamera, out vector);
				this.m_PointerDownPos = (((RectTransform)base.transform).anchoredPosition = vector);
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x00040C80 File Offset: 0x0003EE80
		private void MoveStick(Vector2 pointerPosition, Camera uiCamera)
		{
			RectTransform canvasRectTransform = UGUIOnScreenControlUtils.GetCanvasRectTransform(base.transform);
			if (canvasRectTransform == null)
			{
				Debug.LogError(base.GetWarningMessage());
				return;
			}
			Vector2 vector;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, pointerPosition, uiCamera, out vector);
			Vector2 vector2 = vector - this.m_PointerDownPos;
			switch (this.m_Behaviour)
			{
			case OnScreenStick.Behaviour.RelativePositionWithStaticOrigin:
				vector2 = Vector2.ClampMagnitude(vector2, this.movementRange);
				((RectTransform)base.transform).anchoredPosition = this.m_StartPos + vector2;
				break;
			case OnScreenStick.Behaviour.ExactPositionWithStaticOrigin:
				vector2 = vector - this.m_StartPos;
				vector2 = Vector2.ClampMagnitude(vector2, this.movementRange);
				((RectTransform)base.transform).anchoredPosition = this.m_StartPos + vector2;
				break;
			case OnScreenStick.Behaviour.ExactPositionWithDynamicOrigin:
				vector2 = Vector2.ClampMagnitude(vector2, this.movementRange);
				((RectTransform)base.transform).anchoredPosition = this.m_PointerDownPos + vector2;
				break;
			}
			Vector2 vector3 = new Vector2(vector2.x / this.movementRange, vector2.y / this.movementRange);
			base.SendValueToControl<Vector2>(vector3);
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x00040DAC File Offset: 0x0003EFAC
		private void EndInteraction()
		{
			((RectTransform)base.transform).anchoredPosition = (this.m_PointerDownPos = this.m_StartPos);
			base.SendValueToControl<Vector2>(Vector2.zero);
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x00040DE8 File Offset: 0x0003EFE8
		private void OnPointerDown(InputAction.CallbackContext ctx)
		{
			if (this.m_IsIsolationActive)
			{
				return;
			}
			Vector2 vector = Vector2.zero;
			TouchControl touchControl = null;
			InputControl control = ctx.control;
			TouchControl touchControl2 = ((control != null) ? control.parent : null) as TouchControl;
			if (touchControl2 != null)
			{
				touchControl = touchControl2;
				vector = touchControl2.position.ReadValue();
			}
			else
			{
				InputControl control2 = ctx.control;
				Pointer pointer = ((control2 != null) ? control2.device : null) as Pointer;
				if (pointer != null)
				{
					vector = pointer.position.ReadValue();
				}
			}
			this.m_PointerEventData.position = vector;
			EventSystem.current.RaycastAll(this.m_PointerEventData, this.m_RaycastResults);
			if (this.m_RaycastResults.Count == 0)
			{
				return;
			}
			bool flag = false;
			foreach (RaycastResult raycastResult in this.m_RaycastResults)
			{
				if (!(raycastResult.gameObject != base.gameObject))
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				return;
			}
			this.BeginInteraction(vector, this.GetCameraFromCanvas());
			if (touchControl != null)
			{
				this.m_TouchControl = touchControl;
				this.m_PointerMoveAction.ApplyBindingOverride(touchControl.path + "/position", null, "<Touchscreen>/touch*/position");
			}
			this.m_PointerMoveAction.performed += this.OnPointerMove;
			this.m_IsIsolationActive = true;
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x00040F44 File Offset: 0x0003F144
		private void OnPointerChanged(InputAction.CallbackContext ctx)
		{
			if (ctx.control.IsPressed(0f))
			{
				this.OnPointerDown(ctx);
				return;
			}
			this.OnPointerUp(ctx);
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x00040F68 File Offset: 0x0003F168
		private void OnPointerMove(InputAction.CallbackContext ctx)
		{
			Vector2 vector;
			if (this.m_TouchControl != null)
			{
				if (!this.m_TouchControl.isInProgress)
				{
					return;
				}
				vector = this.m_TouchControl.position.ReadValue();
			}
			else
			{
				vector = ((Pointer)ctx.control.device).position.ReadValue();
			}
			this.MoveStick(vector, this.GetCameraFromCanvas());
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x00040FC8 File Offset: 0x0003F1C8
		private void OnPointerUp(InputAction.CallbackContext ctx)
		{
			if (!this.m_IsIsolationActive)
			{
				return;
			}
			if (this.m_TouchControl != null)
			{
				if (this.m_TouchControl.isInProgress)
				{
					return;
				}
				this.m_PointerMoveAction.ApplyBindingOverride(null, null, "<Touchscreen>/touch*/position");
				this.m_TouchControl = null;
			}
			this.EndInteraction();
			this.m_PointerMoveAction.performed -= this.OnPointerMove;
			this.m_IsIsolationActive = false;
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x00041034 File Offset: 0x0003F234
		private Camera GetCameraFromCanvas()
		{
			Canvas componentInParent = base.GetComponentInParent<Canvas>();
			RenderMode? renderMode = ((componentInParent != null) ? new RenderMode?(componentInParent.renderMode) : null);
			RenderMode? renderMode2 = renderMode;
			RenderMode renderMode3 = RenderMode.ScreenSpaceOverlay;
			if (!((renderMode2.GetValueOrDefault() == renderMode3) & (renderMode2 != null)))
			{
				renderMode2 = renderMode;
				renderMode3 = RenderMode.ScreenSpaceCamera;
				if (!((renderMode2.GetValueOrDefault() == renderMode3) & (renderMode2 != null)) || !(((componentInParent != null) ? componentInParent.worldCamera : null) == null))
				{
					return ((componentInParent != null) ? componentInParent.worldCamera : null) ?? Camera.main;
				}
			}
			return null;
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x000410C0 File Offset: 0x0003F2C0
		private void OnDrawGizmosSelected()
		{
			RectTransform rectTransform = base.transform.parent as RectTransform;
			if (rectTransform == null)
			{
				return;
			}
			Gizmos.matrix = rectTransform.localToWorldMatrix;
			Vector2 vector = rectTransform.anchoredPosition;
			if (Application.isPlaying)
			{
				vector = this.m_StartPos;
			}
			Gizmos.color = new Color32(84, 173, 219, byte.MaxValue);
			Vector2 vector2 = vector;
			if (Application.isPlaying && this.m_Behaviour == OnScreenStick.Behaviour.ExactPositionWithDynamicOrigin)
			{
				vector2 = this.m_PointerDownPos;
			}
			this.DrawGizmoCircle(vector2, this.m_MovementRange);
			if (this.m_Behaviour != OnScreenStick.Behaviour.ExactPositionWithDynamicOrigin)
			{
				return;
			}
			Gizmos.color = new Color32(158, 84, 219, byte.MaxValue);
			this.DrawGizmoCircle(vector, this.m_DynamicOriginRange);
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x0004118C File Offset: 0x0003F38C
		private void DrawGizmoCircle(Vector2 center, float radius)
		{
			for (int i = 0; i < 32; i++)
			{
				float num = (float)i / 32f * 3.1415927f * 2f;
				float num2 = (float)(i + 1) / 32f * 3.1415927f * 2f;
				Gizmos.DrawLine(new Vector3(center.x + Mathf.Cos(num) * radius, center.y + Mathf.Sin(num) * radius, 0f), new Vector3(center.x + Mathf.Cos(num2) * radius, center.y + Mathf.Sin(num2) * radius, 0f));
			}
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x00041230 File Offset: 0x0003F430
		private void UpdateDynamicOriginClickableArea()
		{
			Transform transform = base.transform.Find("DynamicOriginClickable");
			if (transform)
			{
				((RectTransform)transform).sizeDelta = new Vector2(this.m_DynamicOriginRange * 2f, this.m_DynamicOriginRange * 2f);
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000C22 RID: 3106 RVA: 0x0004127E File Offset: 0x0003F47E
		// (set) Token: 0x06000C23 RID: 3107 RVA: 0x00041286 File Offset: 0x0003F486
		public float movementRange
		{
			get
			{
				return this.m_MovementRange;
			}
			set
			{
				this.m_MovementRange = value;
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000C24 RID: 3108 RVA: 0x0004128F File Offset: 0x0003F48F
		// (set) Token: 0x06000C25 RID: 3109 RVA: 0x00041297 File Offset: 0x0003F497
		public float dynamicOriginRange
		{
			get
			{
				return this.m_DynamicOriginRange;
			}
			set
			{
				if (this.m_DynamicOriginRange != value)
				{
					this.m_DynamicOriginRange = value;
					this.UpdateDynamicOriginClickableArea();
				}
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000C26 RID: 3110 RVA: 0x000412AF File Offset: 0x0003F4AF
		// (set) Token: 0x06000C27 RID: 3111 RVA: 0x000412B7 File Offset: 0x0003F4B7
		public bool useIsolatedInputActions
		{
			get
			{
				return this.m_UseIsolatedInputActions;
			}
			set
			{
				this.m_UseIsolatedInputActions = value;
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000C28 RID: 3112 RVA: 0x000412C0 File Offset: 0x0003F4C0
		// (set) Token: 0x06000C29 RID: 3113 RVA: 0x000412C8 File Offset: 0x0003F4C8
		protected override string controlPathInternal
		{
			get
			{
				return this.m_ControlPath;
			}
			set
			{
				this.m_ControlPath = value;
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000C2A RID: 3114 RVA: 0x000412D1 File Offset: 0x0003F4D1
		// (set) Token: 0x06000C2B RID: 3115 RVA: 0x000412D9 File Offset: 0x0003F4D9
		public OnScreenStick.Behaviour behaviour
		{
			get
			{
				return this.m_Behaviour;
			}
			set
			{
				this.m_Behaviour = value;
			}
		}

		// Token: 0x0400046F RID: 1135
		private const string kDynamicOriginClickable = "DynamicOriginClickable";

		// Token: 0x04000470 RID: 1136
		[FormerlySerializedAs("movementRange")]
		[SerializeField]
		[Min(0f)]
		private float m_MovementRange = 50f;

		// Token: 0x04000471 RID: 1137
		[SerializeField]
		[Tooltip("Defines the circular region where the onscreen control may have it's origin placed.")]
		[Min(0f)]
		private float m_DynamicOriginRange = 100f;

		// Token: 0x04000472 RID: 1138
		[InputControl(layout = "Vector2")]
		[SerializeField]
		private string m_ControlPath;

		// Token: 0x04000473 RID: 1139
		[SerializeField]
		[Tooltip("Choose how the onscreen stick will move relative to it's origin and the press position.\n\nRelativePositionWithStaticOrigin: The control's center of origin is fixed. The control will begin un-actuated at it's centered position and then move relative to the pointer or finger motion.\n\nExactPositionWithStaticOrigin: The control's center of origin is fixed. The stick will immediately jump to the exact position of the click or touch and begin tracking motion from there.\n\nExactPositionWithDynamicOrigin: The control's center of origin is determined by the initial press position. The stick will begin un-actuated at this center position and then track the current pointer or finger position.")]
		private OnScreenStick.Behaviour m_Behaviour;

		// Token: 0x04000474 RID: 1140
		[SerializeField]
		[Tooltip("Set this to true to prevent cancellation of pointer events due to device switching. Cancellation will appear as the stick jumping back and forth between the pointer position and the stick center.")]
		private bool m_UseIsolatedInputActions;

		// Token: 0x04000475 RID: 1141
		[SerializeField]
		[Tooltip("The action that will be used to detect pointer down events on the stick control. Note that if no bindings are set, default ones will be provided.")]
		private InputAction m_PointerDownAction;

		// Token: 0x04000476 RID: 1142
		[SerializeField]
		[Tooltip("The action that will be used to detect pointer movement on the stick control. Note that if no bindings are set, default ones will be provided.")]
		private InputAction m_PointerMoveAction;

		// Token: 0x04000477 RID: 1143
		private Vector3 m_StartPos;

		// Token: 0x04000478 RID: 1144
		private Vector2 m_PointerDownPos;

		// Token: 0x04000479 RID: 1145
		[NonSerialized]
		private List<RaycastResult> m_RaycastResults;

		// Token: 0x0400047A RID: 1146
		[NonSerialized]
		private PointerEventData m_PointerEventData;

		// Token: 0x0400047B RID: 1147
		[NonSerialized]
		private TouchControl m_TouchControl;

		// Token: 0x0400047C RID: 1148
		[NonSerialized]
		private bool m_IsIsolationActive;

		// Token: 0x020001E5 RID: 485
		public enum Behaviour
		{
			// Token: 0x04000A24 RID: 2596
			RelativePositionWithStaticOrigin,
			// Token: 0x04000A25 RID: 2597
			ExactPositionWithStaticOrigin,
			// Token: 0x04000A26 RID: 2598
			ExactPositionWithDynamicOrigin
		}
	}
}
