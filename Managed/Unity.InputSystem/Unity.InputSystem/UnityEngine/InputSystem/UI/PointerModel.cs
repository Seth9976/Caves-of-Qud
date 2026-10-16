using System;
using UnityEngine.EventSystems;

namespace UnityEngine.InputSystem.UI
{
	// Token: 0x02000090 RID: 144
	internal struct PointerModel
	{
		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000B9E RID: 2974 RVA: 0x0003E819 File Offset: 0x0003CA19
		public UIPointerType pointerType
		{
			get
			{
				return this.eventData.pointerType;
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000B9F RID: 2975 RVA: 0x0003E826 File Offset: 0x0003CA26
		// (set) Token: 0x06000BA0 RID: 2976 RVA: 0x0003E82E File Offset: 0x0003CA2E
		public Vector2 screenPosition
		{
			get
			{
				return this.m_ScreenPosition;
			}
			set
			{
				if (this.m_ScreenPosition != value)
				{
					this.m_ScreenPosition = value;
					this.changedThisFrame = true;
				}
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000BA1 RID: 2977 RVA: 0x0003E84C File Offset: 0x0003CA4C
		// (set) Token: 0x06000BA2 RID: 2978 RVA: 0x0003E854 File Offset: 0x0003CA54
		public Vector3 worldPosition
		{
			get
			{
				return this.m_WorldPosition;
			}
			set
			{
				if (this.m_WorldPosition != value)
				{
					this.m_WorldPosition = value;
					this.changedThisFrame = true;
				}
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000BA3 RID: 2979 RVA: 0x0003E872 File Offset: 0x0003CA72
		// (set) Token: 0x06000BA4 RID: 2980 RVA: 0x0003E87A File Offset: 0x0003CA7A
		public Quaternion worldOrientation
		{
			get
			{
				return this.m_WorldOrientation;
			}
			set
			{
				if (this.m_WorldOrientation != value)
				{
					this.m_WorldOrientation = value;
					this.changedThisFrame = true;
				}
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000BA5 RID: 2981 RVA: 0x0003E898 File Offset: 0x0003CA98
		// (set) Token: 0x06000BA6 RID: 2982 RVA: 0x0003E8A0 File Offset: 0x0003CAA0
		public Vector2 scrollDelta
		{
			get
			{
				return this.m_ScrollDelta;
			}
			set
			{
				if (this.m_ScrollDelta != value)
				{
					this.changedThisFrame = true;
					this.m_ScrollDelta = value;
				}
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000BA7 RID: 2983 RVA: 0x0003E8BE File Offset: 0x0003CABE
		// (set) Token: 0x06000BA8 RID: 2984 RVA: 0x0003E8C6 File Offset: 0x0003CAC6
		public float pressure
		{
			get
			{
				return this.m_Pressure;
			}
			set
			{
				if (this.m_Pressure != value)
				{
					this.changedThisFrame = true;
					this.m_Pressure = value;
				}
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000BA9 RID: 2985 RVA: 0x0003E8DF File Offset: 0x0003CADF
		// (set) Token: 0x06000BAA RID: 2986 RVA: 0x0003E8E7 File Offset: 0x0003CAE7
		public float azimuthAngle
		{
			get
			{
				return this.m_AzimuthAngle;
			}
			set
			{
				if (this.m_AzimuthAngle != value)
				{
					this.changedThisFrame = true;
					this.m_AzimuthAngle = value;
				}
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000BAB RID: 2987 RVA: 0x0003E900 File Offset: 0x0003CB00
		// (set) Token: 0x06000BAC RID: 2988 RVA: 0x0003E908 File Offset: 0x0003CB08
		public float altitudeAngle
		{
			get
			{
				return this.m_AltitudeAngle;
			}
			set
			{
				if (this.m_AltitudeAngle != value)
				{
					this.changedThisFrame = true;
					this.m_AltitudeAngle = value;
				}
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000BAD RID: 2989 RVA: 0x0003E921 File Offset: 0x0003CB21
		// (set) Token: 0x06000BAE RID: 2990 RVA: 0x0003E929 File Offset: 0x0003CB29
		public float twist
		{
			get
			{
				return this.m_Twist;
			}
			set
			{
				if (this.m_Twist != value)
				{
					this.changedThisFrame = true;
					this.m_Twist = value;
				}
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000BAF RID: 2991 RVA: 0x0003E942 File Offset: 0x0003CB42
		// (set) Token: 0x06000BB0 RID: 2992 RVA: 0x0003E94A File Offset: 0x0003CB4A
		public Vector2 radius
		{
			get
			{
				return this.m_Radius;
			}
			set
			{
				if (this.m_Radius != value)
				{
					this.changedThisFrame = true;
					this.m_Radius = value;
				}
			}
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x0003E968 File Offset: 0x0003CB68
		public PointerModel(ExtendedPointerEventData eventData)
		{
			this.eventData = eventData;
			this.changedThisFrame = false;
			this.leftButton = default(PointerModel.ButtonState);
			this.leftButton.OnEndFrame();
			this.rightButton = default(PointerModel.ButtonState);
			this.rightButton.OnEndFrame();
			this.middleButton = default(PointerModel.ButtonState);
			this.middleButton.OnEndFrame();
			this.m_ScreenPosition = default(Vector2);
			this.m_ScrollDelta = default(Vector2);
			this.m_WorldOrientation = default(Quaternion);
			this.m_WorldPosition = default(Vector3);
			this.m_Pressure = 0f;
			this.m_AzimuthAngle = 0f;
			this.m_AltitudeAngle = 0f;
			this.m_Twist = 0f;
			this.m_Radius = default(Vector2);
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x0003EA30 File Offset: 0x0003CC30
		public void OnFrameFinished()
		{
			this.changedThisFrame = false;
			this.scrollDelta = default(Vector2);
			this.leftButton.OnEndFrame();
			this.rightButton.OnEndFrame();
			this.middleButton.OnEndFrame();
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x0003EA74 File Offset: 0x0003CC74
		public void CopyTouchOrPenStateFrom(PointerEventData eventData)
		{
			this.pressure = eventData.pressure;
			this.azimuthAngle = eventData.azimuthAngle;
			this.altitudeAngle = eventData.altitudeAngle;
			this.twist = eventData.twist;
			this.radius = eventData.radius;
		}

		// Token: 0x04000430 RID: 1072
		public bool changedThisFrame;

		// Token: 0x04000431 RID: 1073
		public PointerModel.ButtonState leftButton;

		// Token: 0x04000432 RID: 1074
		public PointerModel.ButtonState rightButton;

		// Token: 0x04000433 RID: 1075
		public PointerModel.ButtonState middleButton;

		// Token: 0x04000434 RID: 1076
		public ExtendedPointerEventData eventData;

		// Token: 0x04000435 RID: 1077
		private Vector2 m_ScreenPosition;

		// Token: 0x04000436 RID: 1078
		private Vector2 m_ScrollDelta;

		// Token: 0x04000437 RID: 1079
		private Vector3 m_WorldPosition;

		// Token: 0x04000438 RID: 1080
		private Quaternion m_WorldOrientation;

		// Token: 0x04000439 RID: 1081
		private float m_Pressure;

		// Token: 0x0400043A RID: 1082
		private float m_AzimuthAngle;

		// Token: 0x0400043B RID: 1083
		private float m_AltitudeAngle;

		// Token: 0x0400043C RID: 1084
		private float m_Twist;

		// Token: 0x0400043D RID: 1085
		private Vector2 m_Radius;

		// Token: 0x020001D8 RID: 472
		public struct ButtonState
		{
			// Token: 0x17000591 RID: 1425
			// (get) Token: 0x060014AA RID: 5290 RVA: 0x0005FFAE File Offset: 0x0005E1AE
			// (set) Token: 0x060014AB RID: 5291 RVA: 0x0005FFB8 File Offset: 0x0005E1B8
			public bool isPressed
			{
				get
				{
					return this.m_IsPressed;
				}
				set
				{
					if (this.m_IsPressed != value)
					{
						this.m_IsPressed = value;
						if (this.m_FramePressState == PointerEventData.FramePressState.NotChanged && value)
						{
							this.m_FramePressState = PointerEventData.FramePressState.Pressed;
							return;
						}
						if (this.m_FramePressState == PointerEventData.FramePressState.NotChanged && !value)
						{
							this.m_FramePressState = PointerEventData.FramePressState.Released;
							return;
						}
						if (this.m_FramePressState == PointerEventData.FramePressState.Pressed && !value)
						{
							this.m_FramePressState = PointerEventData.FramePressState.PressedAndReleased;
						}
					}
				}
			}

			// Token: 0x17000592 RID: 1426
			// (get) Token: 0x060014AC RID: 5292 RVA: 0x00060010 File Offset: 0x0005E210
			// (set) Token: 0x060014AD RID: 5293 RVA: 0x00060018 File Offset: 0x0005E218
			public bool ignoreNextClick
			{
				get
				{
					return this.m_IgnoreNextClick;
				}
				set
				{
					this.m_IgnoreNextClick = value;
				}
			}

			// Token: 0x17000593 RID: 1427
			// (get) Token: 0x060014AE RID: 5294 RVA: 0x00060021 File Offset: 0x0005E221
			// (set) Token: 0x060014AF RID: 5295 RVA: 0x00060029 File Offset: 0x0005E229
			public float pressTime
			{
				get
				{
					return this.m_PressTime;
				}
				set
				{
					this.m_PressTime = value;
				}
			}

			// Token: 0x17000594 RID: 1428
			// (get) Token: 0x060014B0 RID: 5296 RVA: 0x00060032 File Offset: 0x0005E232
			// (set) Token: 0x060014B1 RID: 5297 RVA: 0x0006003A File Offset: 0x0005E23A
			public bool clickedOnSameGameObject
			{
				get
				{
					return this.m_ClickedOnSameGameObject;
				}
				set
				{
					this.m_ClickedOnSameGameObject = value;
				}
			}

			// Token: 0x17000595 RID: 1429
			// (get) Token: 0x060014B2 RID: 5298 RVA: 0x00060043 File Offset: 0x0005E243
			public bool wasPressedThisFrame
			{
				get
				{
					return this.m_FramePressState == PointerEventData.FramePressState.Pressed || this.m_FramePressState == PointerEventData.FramePressState.PressedAndReleased;
				}
			}

			// Token: 0x17000596 RID: 1430
			// (get) Token: 0x060014B3 RID: 5299 RVA: 0x00060058 File Offset: 0x0005E258
			public bool wasReleasedThisFrame
			{
				get
				{
					return this.m_FramePressState == PointerEventData.FramePressState.Released || this.m_FramePressState == PointerEventData.FramePressState.PressedAndReleased;
				}
			}

			// Token: 0x060014B4 RID: 5300 RVA: 0x00060070 File Offset: 0x0005E270
			public void CopyPressStateTo(PointerEventData eventData)
			{
				eventData.pointerPressRaycast = this.m_PressRaycast;
				eventData.pressPosition = this.m_PressPosition;
				eventData.clickCount = this.m_ClickCount;
				eventData.clickTime = this.m_ClickTime;
				eventData.pointerPress = this.m_LastPressObject;
				eventData.pointerPress = this.m_PressObject;
				eventData.rawPointerPress = this.m_RawPressObject;
				eventData.pointerDrag = this.m_DragObject;
				eventData.dragging = this.m_Dragging;
				if (this.ignoreNextClick)
				{
					eventData.eligibleForClick = false;
				}
			}

			// Token: 0x060014B5 RID: 5301 RVA: 0x000600F8 File Offset: 0x0005E2F8
			public void CopyPressStateFrom(PointerEventData eventData)
			{
				this.m_PressRaycast = eventData.pointerPressRaycast;
				this.m_PressObject = eventData.pointerPress;
				this.m_RawPressObject = eventData.rawPointerPress;
				this.m_LastPressObject = eventData.lastPress;
				this.m_PressPosition = eventData.pressPosition;
				this.m_ClickTime = eventData.clickTime;
				this.m_ClickCount = eventData.clickCount;
				this.m_DragObject = eventData.pointerDrag;
				this.m_Dragging = eventData.dragging;
			}

			// Token: 0x060014B6 RID: 5302 RVA: 0x00060171 File Offset: 0x0005E371
			public void OnEndFrame()
			{
				this.m_FramePressState = PointerEventData.FramePressState.NotChanged;
			}

			// Token: 0x040009C8 RID: 2504
			private bool m_IsPressed;

			// Token: 0x040009C9 RID: 2505
			private PointerEventData.FramePressState m_FramePressState;

			// Token: 0x040009CA RID: 2506
			private float m_PressTime;

			// Token: 0x040009CB RID: 2507
			private RaycastResult m_PressRaycast;

			// Token: 0x040009CC RID: 2508
			private GameObject m_PressObject;

			// Token: 0x040009CD RID: 2509
			private GameObject m_RawPressObject;

			// Token: 0x040009CE RID: 2510
			private GameObject m_LastPressObject;

			// Token: 0x040009CF RID: 2511
			private GameObject m_DragObject;

			// Token: 0x040009D0 RID: 2512
			private Vector2 m_PressPosition;

			// Token: 0x040009D1 RID: 2513
			private float m_ClickTime;

			// Token: 0x040009D2 RID: 2514
			private int m_ClickCount;

			// Token: 0x040009D3 RID: 2515
			private bool m_Dragging;

			// Token: 0x040009D4 RID: 2516
			private bool m_ClickedOnSameGameObject;

			// Token: 0x040009D5 RID: 2517
			private bool m_IgnoreNextClick;
		}
	}
}
