using System;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.UI;

namespace UnityEngine.InputSystem.UI
{
	// Token: 0x02000092 RID: 146
	[AddComponentMenu("Input/Virtual Mouse")]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.inputsystem@1.19/manual/UISupport.html#virtual-mouse-cursor-control")]
	public class VirtualMouseInput : MonoBehaviour
	{
		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000BC8 RID: 3016 RVA: 0x0003EFA9 File Offset: 0x0003D1A9
		// (set) Token: 0x06000BC9 RID: 3017 RVA: 0x0003EFB1 File Offset: 0x0003D1B1
		public RectTransform cursorTransform
		{
			get
			{
				return this.m_CursorTransform;
			}
			set
			{
				this.m_CursorTransform = value;
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000BCA RID: 3018 RVA: 0x0003EFBA File Offset: 0x0003D1BA
		// (set) Token: 0x06000BCB RID: 3019 RVA: 0x0003EFC2 File Offset: 0x0003D1C2
		public float cursorSpeed
		{
			get
			{
				return this.m_CursorSpeed;
			}
			set
			{
				this.m_CursorSpeed = value;
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000BCC RID: 3020 RVA: 0x0003EFCB File Offset: 0x0003D1CB
		// (set) Token: 0x06000BCD RID: 3021 RVA: 0x0003EFD4 File Offset: 0x0003D1D4
		public VirtualMouseInput.CursorMode cursorMode
		{
			get
			{
				return this.m_CursorMode;
			}
			set
			{
				if (this.m_CursorMode == value)
				{
					return;
				}
				if (this.m_CursorMode == VirtualMouseInput.CursorMode.HardwareCursorIfAvailable && this.m_SystemMouse != null)
				{
					InputSystem.EnableDevice(this.m_SystemMouse);
					this.m_SystemMouse = null;
				}
				this.m_CursorMode = value;
				if (this.m_CursorMode == VirtualMouseInput.CursorMode.HardwareCursorIfAvailable)
				{
					this.TryEnableHardwareCursor();
					return;
				}
				if (this.m_CursorGraphic != null)
				{
					this.m_CursorGraphic.enabled = true;
				}
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000BCE RID: 3022 RVA: 0x0003F03F File Offset: 0x0003D23F
		// (set) Token: 0x06000BCF RID: 3023 RVA: 0x0003F047 File Offset: 0x0003D247
		public Graphic cursorGraphic
		{
			get
			{
				return this.m_CursorGraphic;
			}
			set
			{
				this.m_CursorGraphic = value;
				this.TryFindCanvas();
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000BD0 RID: 3024 RVA: 0x0003F056 File Offset: 0x0003D256
		// (set) Token: 0x06000BD1 RID: 3025 RVA: 0x0003F05E File Offset: 0x0003D25E
		public float scrollSpeed
		{
			get
			{
				return this.m_ScrollSpeed;
			}
			set
			{
				this.m_ScrollSpeed = value;
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000BD2 RID: 3026 RVA: 0x0003F067 File Offset: 0x0003D267
		public Mouse virtualMouse
		{
			get
			{
				return this.m_VirtualMouse;
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000BD3 RID: 3027 RVA: 0x0003F06F File Offset: 0x0003D26F
		// (set) Token: 0x06000BD4 RID: 3028 RVA: 0x0003F077 File Offset: 0x0003D277
		public InputActionProperty stickAction
		{
			get
			{
				return this.m_StickAction;
			}
			set
			{
				VirtualMouseInput.SetAction(ref this.m_StickAction, value);
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000BD5 RID: 3029 RVA: 0x0003F085 File Offset: 0x0003D285
		// (set) Token: 0x06000BD6 RID: 3030 RVA: 0x0003F090 File Offset: 0x0003D290
		public InputActionProperty leftButtonAction
		{
			get
			{
				return this.m_LeftButtonAction;
			}
			set
			{
				if (this.m_ButtonActionTriggeredDelegate != null)
				{
					VirtualMouseInput.SetActionCallback(this.m_LeftButtonAction, this.m_ButtonActionTriggeredDelegate, false);
				}
				VirtualMouseInput.SetAction(ref this.m_LeftButtonAction, value);
				if (this.m_ButtonActionTriggeredDelegate != null)
				{
					VirtualMouseInput.SetActionCallback(this.m_LeftButtonAction, this.m_ButtonActionTriggeredDelegate, true);
				}
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000BD7 RID: 3031 RVA: 0x0003F0DD File Offset: 0x0003D2DD
		// (set) Token: 0x06000BD8 RID: 3032 RVA: 0x0003F0E8 File Offset: 0x0003D2E8
		public InputActionProperty rightButtonAction
		{
			get
			{
				return this.m_RightButtonAction;
			}
			set
			{
				if (this.m_ButtonActionTriggeredDelegate != null)
				{
					VirtualMouseInput.SetActionCallback(this.m_RightButtonAction, this.m_ButtonActionTriggeredDelegate, false);
				}
				VirtualMouseInput.SetAction(ref this.m_RightButtonAction, value);
				if (this.m_ButtonActionTriggeredDelegate != null)
				{
					VirtualMouseInput.SetActionCallback(this.m_RightButtonAction, this.m_ButtonActionTriggeredDelegate, true);
				}
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000BD9 RID: 3033 RVA: 0x0003F135 File Offset: 0x0003D335
		// (set) Token: 0x06000BDA RID: 3034 RVA: 0x0003F140 File Offset: 0x0003D340
		public InputActionProperty middleButtonAction
		{
			get
			{
				return this.m_MiddleButtonAction;
			}
			set
			{
				if (this.m_ButtonActionTriggeredDelegate != null)
				{
					VirtualMouseInput.SetActionCallback(this.m_MiddleButtonAction, this.m_ButtonActionTriggeredDelegate, false);
				}
				VirtualMouseInput.SetAction(ref this.m_MiddleButtonAction, value);
				if (this.m_ButtonActionTriggeredDelegate != null)
				{
					VirtualMouseInput.SetActionCallback(this.m_MiddleButtonAction, this.m_ButtonActionTriggeredDelegate, true);
				}
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000BDB RID: 3035 RVA: 0x0003F18D File Offset: 0x0003D38D
		// (set) Token: 0x06000BDC RID: 3036 RVA: 0x0003F198 File Offset: 0x0003D398
		public InputActionProperty forwardButtonAction
		{
			get
			{
				return this.m_ForwardButtonAction;
			}
			set
			{
				if (this.m_ButtonActionTriggeredDelegate != null)
				{
					VirtualMouseInput.SetActionCallback(this.m_ForwardButtonAction, this.m_ButtonActionTriggeredDelegate, false);
				}
				VirtualMouseInput.SetAction(ref this.m_ForwardButtonAction, value);
				if (this.m_ButtonActionTriggeredDelegate != null)
				{
					VirtualMouseInput.SetActionCallback(this.m_ForwardButtonAction, this.m_ButtonActionTriggeredDelegate, true);
				}
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000BDD RID: 3037 RVA: 0x0003F1E5 File Offset: 0x0003D3E5
		// (set) Token: 0x06000BDE RID: 3038 RVA: 0x0003F1F0 File Offset: 0x0003D3F0
		public InputActionProperty backButtonAction
		{
			get
			{
				return this.m_BackButtonAction;
			}
			set
			{
				if (this.m_ButtonActionTriggeredDelegate != null)
				{
					VirtualMouseInput.SetActionCallback(this.m_BackButtonAction, this.m_ButtonActionTriggeredDelegate, false);
				}
				VirtualMouseInput.SetAction(ref this.m_BackButtonAction, value);
				if (this.m_ButtonActionTriggeredDelegate != null)
				{
					VirtualMouseInput.SetActionCallback(this.m_BackButtonAction, this.m_ButtonActionTriggeredDelegate, true);
				}
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000BDF RID: 3039 RVA: 0x0003F23D File Offset: 0x0003D43D
		// (set) Token: 0x06000BE0 RID: 3040 RVA: 0x0003F245 File Offset: 0x0003D445
		public InputActionProperty scrollWheelAction
		{
			get
			{
				return this.m_ScrollWheelAction;
			}
			set
			{
				VirtualMouseInput.SetAction(ref this.m_ScrollWheelAction, value);
			}
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x0003F254 File Offset: 0x0003D454
		protected void OnEnable()
		{
			if (this.m_CursorMode == VirtualMouseInput.CursorMode.HardwareCursorIfAvailable)
			{
				this.TryEnableHardwareCursor();
			}
			if (this.m_VirtualMouse == null)
			{
				this.m_VirtualMouse = (Mouse)InputSystem.AddDevice("VirtualMouse", null, null);
				this.TryFindCanvas();
			}
			else if (!this.m_VirtualMouse.added)
			{
				InputSystem.AddDevice(this.m_VirtualMouse);
			}
			if (this.m_CursorTransform != null)
			{
				Vector2 anchoredPosition = this.m_CursorTransform.anchoredPosition;
				InputState.Change<Vector2>(this.m_VirtualMouse.position, anchoredPosition, InputUpdateType.None, default(InputEventPtr));
				Mouse systemMouse = this.m_SystemMouse;
				if (systemMouse != null)
				{
					systemMouse.WarpCursorPosition(anchoredPosition);
				}
			}
			if (this.m_AfterInputUpdateDelegate == null)
			{
				this.m_AfterInputUpdateDelegate = new Action(this.OnAfterInputUpdate);
			}
			InputSystem.onAfterUpdate += this.m_AfterInputUpdateDelegate;
			if (this.m_ButtonActionTriggeredDelegate == null)
			{
				this.m_ButtonActionTriggeredDelegate = new Action<InputAction.CallbackContext>(this.OnButtonActionTriggered);
			}
			VirtualMouseInput.SetActionCallback(this.m_LeftButtonAction, this.m_ButtonActionTriggeredDelegate, true);
			VirtualMouseInput.SetActionCallback(this.m_RightButtonAction, this.m_ButtonActionTriggeredDelegate, true);
			VirtualMouseInput.SetActionCallback(this.m_MiddleButtonAction, this.m_ButtonActionTriggeredDelegate, true);
			VirtualMouseInput.SetActionCallback(this.m_ForwardButtonAction, this.m_ButtonActionTriggeredDelegate, true);
			VirtualMouseInput.SetActionCallback(this.m_BackButtonAction, this.m_ButtonActionTriggeredDelegate, true);
			InputAction action = this.m_StickAction.action;
			if (action != null)
			{
				action.Enable();
			}
			InputAction action2 = this.m_LeftButtonAction.action;
			if (action2 != null)
			{
				action2.Enable();
			}
			InputAction action3 = this.m_RightButtonAction.action;
			if (action3 != null)
			{
				action3.Enable();
			}
			InputAction action4 = this.m_MiddleButtonAction.action;
			if (action4 != null)
			{
				action4.Enable();
			}
			InputAction action5 = this.m_ForwardButtonAction.action;
			if (action5 != null)
			{
				action5.Enable();
			}
			InputAction action6 = this.m_BackButtonAction.action;
			if (action6 != null)
			{
				action6.Enable();
			}
			InputAction action7 = this.m_ScrollWheelAction.action;
			if (action7 == null)
			{
				return;
			}
			action7.Enable();
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x0003F428 File Offset: 0x0003D628
		protected void OnDisable()
		{
			if (this.m_VirtualMouse != null && this.m_VirtualMouse.added)
			{
				InputSystem.RemoveDevice(this.m_VirtualMouse);
			}
			if (this.m_SystemMouse != null)
			{
				InputSystem.EnableDevice(this.m_SystemMouse);
				this.m_SystemMouse = null;
			}
			if (this.m_AfterInputUpdateDelegate != null)
			{
				InputSystem.onAfterUpdate -= this.m_AfterInputUpdateDelegate;
			}
			InputAction action = this.m_StickAction.action;
			if (action != null)
			{
				action.Disable();
			}
			InputAction action2 = this.m_LeftButtonAction.action;
			if (action2 != null)
			{
				action2.Disable();
			}
			InputAction action3 = this.m_RightButtonAction.action;
			if (action3 != null)
			{
				action3.Disable();
			}
			InputAction action4 = this.m_MiddleButtonAction.action;
			if (action4 != null)
			{
				action4.Disable();
			}
			InputAction action5 = this.m_ForwardButtonAction.action;
			if (action5 != null)
			{
				action5.Disable();
			}
			InputAction action6 = this.m_BackButtonAction.action;
			if (action6 != null)
			{
				action6.Disable();
			}
			InputAction action7 = this.m_ScrollWheelAction.action;
			if (action7 != null)
			{
				action7.Disable();
			}
			if (this.m_ButtonActionTriggeredDelegate != null)
			{
				VirtualMouseInput.SetActionCallback(this.m_LeftButtonAction, this.m_ButtonActionTriggeredDelegate, false);
				VirtualMouseInput.SetActionCallback(this.m_RightButtonAction, this.m_ButtonActionTriggeredDelegate, false);
				VirtualMouseInput.SetActionCallback(this.m_MiddleButtonAction, this.m_ButtonActionTriggeredDelegate, false);
				VirtualMouseInput.SetActionCallback(this.m_ForwardButtonAction, this.m_ButtonActionTriggeredDelegate, false);
				VirtualMouseInput.SetActionCallback(this.m_BackButtonAction, this.m_ButtonActionTriggeredDelegate, false);
			}
			this.m_LastTime = 0.0;
			this.m_LastStickValue = default(Vector2);
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x0003F599 File Offset: 0x0003D799
		private void TryFindCanvas()
		{
			Graphic cursorGraphic = this.m_CursorGraphic;
			this.m_Canvas = ((cursorGraphic != null) ? cursorGraphic.GetComponentInParent<Canvas>() : null);
			Graphic cursorGraphic2 = this.m_CursorGraphic;
			this.m_CanvasScaler = ((cursorGraphic2 != null) ? cursorGraphic2.GetComponentInParent<CanvasScaler>() : null);
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x0003F5CC File Offset: 0x0003D7CC
		private unsafe void TryEnableHardwareCursor()
		{
			ReadOnlyArray<InputDevice> devices = InputSystem.devices;
			for (int i = 0; i < devices.Count; i++)
			{
				InputDevice inputDevice = devices[i];
				if (inputDevice.native)
				{
					Mouse mouse = inputDevice as Mouse;
					if (mouse != null)
					{
						this.m_SystemMouse = mouse;
						break;
					}
				}
			}
			if (this.m_SystemMouse == null)
			{
				if (this.m_CursorGraphic != null)
				{
					this.m_CursorGraphic.enabled = true;
				}
				return;
			}
			InputSystem.DisableDevice(this.m_SystemMouse, false);
			if (this.m_VirtualMouse != null)
			{
				this.m_SystemMouse.WarpCursorPosition(*this.m_VirtualMouse.position.value);
			}
			if (this.m_CursorGraphic != null)
			{
				this.m_CursorGraphic.enabled = false;
			}
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x0003F688 File Offset: 0x0003D888
		private unsafe void UpdateMotion()
		{
			if (this.m_VirtualMouse == null)
			{
				return;
			}
			InputAction action = this.m_StickAction.action;
			if (action == null)
			{
				return;
			}
			Vector2 vector = action.ReadValue<Vector2>();
			if (Mathf.Approximately(0f, vector.x) && Mathf.Approximately(0f, vector.y))
			{
				this.m_LastTime = 0.0;
				this.m_LastStickValue = default(Vector2);
			}
			else
			{
				float num = 1f;
				float num2 = 1f;
				if (this.m_CanvasScaler != null)
				{
					num = this.m_Canvas.pixelRect.xMax / this.m_CanvasScaler.referenceResolution.x;
					num2 = this.m_Canvas.pixelRect.yMax / this.m_CanvasScaler.referenceResolution.y;
				}
				double currentTime = InputState.currentTime;
				if (Mathf.Approximately(0f, this.m_LastStickValue.x) && Mathf.Approximately(0f, this.m_LastStickValue.y))
				{
					this.m_LastTime = currentTime;
				}
				float num3 = (float)(currentTime - this.m_LastTime);
				Vector2 vector2 = new Vector2(this.m_CursorSpeed * num * vector.x * num3, this.m_CursorSpeed * num2 * vector.y * num3);
				Vector2 vector3 = *this.m_VirtualMouse.position.value + vector2;
				if (this.m_Canvas != null)
				{
					Rect pixelRect = this.m_Canvas.pixelRect;
					vector3.x = Mathf.Clamp(vector3.x, pixelRect.xMin, pixelRect.xMax);
					vector3.y = Mathf.Clamp(vector3.y, pixelRect.yMin, pixelRect.yMax);
				}
				InputState.Change<Vector2>(this.m_VirtualMouse.position, vector3, InputUpdateType.None, default(InputEventPtr));
				InputState.Change<Vector2>(this.m_VirtualMouse.delta, vector2, InputUpdateType.None, default(InputEventPtr));
				if (this.m_CursorTransform != null && (this.m_CursorMode == VirtualMouseInput.CursorMode.SoftwareCursor || (this.m_CursorMode == VirtualMouseInput.CursorMode.HardwareCursorIfAvailable && this.m_SystemMouse == null)))
				{
					this.m_CursorTransform.anchoredPosition = ((this.m_CanvasScaler == null) ? vector3 : new Vector2(vector3.x / num, vector3.y / num2));
				}
				this.m_LastStickValue = vector;
				this.m_LastTime = currentTime;
				Mouse systemMouse = this.m_SystemMouse;
				if (systemMouse != null)
				{
					systemMouse.WarpCursorPosition(vector3);
				}
			}
			InputAction action2 = this.m_ScrollWheelAction.action;
			if (action2 != null)
			{
				Vector2 vector4 = action2.ReadValue<Vector2>();
				vector4.x *= this.m_ScrollSpeed;
				vector4.y *= this.m_ScrollSpeed;
				InputState.Change<Vector2>(this.m_VirtualMouse.scroll, vector4, InputUpdateType.None, default(InputEventPtr));
			}
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x0003F960 File Offset: 0x0003DB60
		private void OnButtonActionTriggered(InputAction.CallbackContext context)
		{
			if (this.m_VirtualMouse == null)
			{
				return;
			}
			InputAction action = context.action;
			MouseButton? mouseButton = null;
			if (action == this.m_LeftButtonAction.action)
			{
				mouseButton = new MouseButton?(MouseButton.Left);
			}
			else if (action == this.m_RightButtonAction.action)
			{
				mouseButton = new MouseButton?(MouseButton.Right);
			}
			else if (action == this.m_MiddleButtonAction.action)
			{
				mouseButton = new MouseButton?(MouseButton.Middle);
			}
			else if (action == this.m_ForwardButtonAction.action)
			{
				mouseButton = new MouseButton?(MouseButton.Forward);
			}
			else if (action == this.m_BackButtonAction.action)
			{
				mouseButton = new MouseButton?(MouseButton.Back);
			}
			if (mouseButton != null)
			{
				bool flag = context.control.IsPressed(0f);
				MouseState mouseState;
				this.m_VirtualMouse.CopyState<MouseState>(out mouseState);
				mouseState.WithButton(mouseButton.Value, flag);
				InputState.Change<MouseState>(this.m_VirtualMouse, mouseState, InputUpdateType.None, default(InputEventPtr));
			}
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x0003FA4C File Offset: 0x0003DC4C
		private static void SetActionCallback(InputActionProperty field, Action<InputAction.CallbackContext> callback, bool install = true)
		{
			InputAction action = field.action;
			if (action == null)
			{
				return;
			}
			if (install)
			{
				action.started += callback;
				action.canceled += callback;
				return;
			}
			action.started -= callback;
			action.canceled -= callback;
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x0003FA88 File Offset: 0x0003DC88
		private static void SetAction(ref InputActionProperty field, InputActionProperty value)
		{
			InputActionProperty inputActionProperty = field;
			field = value;
			if (inputActionProperty.reference == null)
			{
				InputAction action = inputActionProperty.action;
				if (action != null && action.enabled)
				{
					action.Disable();
					if (value.reference == null)
					{
						InputAction action2 = value.action;
						if (action2 == null)
						{
							return;
						}
						action2.Enable();
					}
				}
			}
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x0003FAEB File Offset: 0x0003DCEB
		private void OnAfterInputUpdate()
		{
			this.UpdateMotion();
		}

		// Token: 0x04000447 RID: 1095
		[Header("Cursor")]
		[Tooltip("Whether the component should set the cursor position of the hardware mouse cursor, if one is available. If so, the software cursor pointed (to by 'Cursor Graphic') will be hidden.")]
		[SerializeField]
		private VirtualMouseInput.CursorMode m_CursorMode;

		// Token: 0x04000448 RID: 1096
		[Tooltip("The graphic that represents the software cursor. This is hidden if a hardware cursor (see 'Cursor Mode') is used.")]
		[SerializeField]
		private Graphic m_CursorGraphic;

		// Token: 0x04000449 RID: 1097
		[Tooltip("The transform for the software cursor. Will only be set if a software cursor is used (see 'Cursor Mode'). Moving the cursor updates the anchored position of the transform.")]
		[SerializeField]
		private RectTransform m_CursorTransform;

		// Token: 0x0400044A RID: 1098
		[Header("Motion")]
		[Tooltip("Speed in pixels per second with which to move the cursor. Scaled by the input from 'Stick Action'.")]
		[SerializeField]
		private float m_CursorSpeed = 400f;

		// Token: 0x0400044B RID: 1099
		[Tooltip("Scale factor to apply to 'Scroll Wheel Action' when setting the mouse 'scrollWheel' control.")]
		[SerializeField]
		private float m_ScrollSpeed = 45f;

		// Token: 0x0400044C RID: 1100
		[Space(10f)]
		[Tooltip("Vector2 action that moves the cursor left/right (X) and up/down (Y) on screen.")]
		[SerializeField]
		private InputActionProperty m_StickAction;

		// Token: 0x0400044D RID: 1101
		[Tooltip("Button action that triggers a left-click on the mouse.")]
		[SerializeField]
		private InputActionProperty m_LeftButtonAction;

		// Token: 0x0400044E RID: 1102
		[Tooltip("Button action that triggers a middle-click on the mouse.")]
		[SerializeField]
		private InputActionProperty m_MiddleButtonAction;

		// Token: 0x0400044F RID: 1103
		[Tooltip("Button action that triggers a right-click on the mouse.")]
		[SerializeField]
		private InputActionProperty m_RightButtonAction;

		// Token: 0x04000450 RID: 1104
		[Tooltip("Button action that triggers a forward button (button #4) click on the mouse.")]
		[SerializeField]
		private InputActionProperty m_ForwardButtonAction;

		// Token: 0x04000451 RID: 1105
		[Tooltip("Button action that triggers a back button (button #5) click on the mouse.")]
		[SerializeField]
		private InputActionProperty m_BackButtonAction;

		// Token: 0x04000452 RID: 1106
		[Tooltip("Vector2 action that feeds into the mouse 'scrollWheel' action (scaled by 'Scroll Speed').")]
		[SerializeField]
		private InputActionProperty m_ScrollWheelAction;

		// Token: 0x04000453 RID: 1107
		private Canvas m_Canvas;

		// Token: 0x04000454 RID: 1108
		private CanvasScaler m_CanvasScaler;

		// Token: 0x04000455 RID: 1109
		private Mouse m_VirtualMouse;

		// Token: 0x04000456 RID: 1110
		private Mouse m_SystemMouse;

		// Token: 0x04000457 RID: 1111
		private Action m_AfterInputUpdateDelegate;

		// Token: 0x04000458 RID: 1112
		private Action<InputAction.CallbackContext> m_ButtonActionTriggeredDelegate;

		// Token: 0x04000459 RID: 1113
		private double m_LastTime;

		// Token: 0x0400045A RID: 1114
		private Vector2 m_LastStickValue;

		// Token: 0x020001DB RID: 475
		public enum CursorMode
		{
			// Token: 0x040009DD RID: 2525
			SoftwareCursor,
			// Token: 0x040009DE RID: 2526
			HardwareCursorIfAvailable
		}
	}
}
