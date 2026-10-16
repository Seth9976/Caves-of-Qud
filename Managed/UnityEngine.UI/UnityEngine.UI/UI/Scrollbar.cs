using System;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x02000034 RID: 52
	[AddComponentMenu("UI/Scrollbar", 36)]
	[ExecuteAlways]
	[RequireComponent(typeof(RectTransform))]
	public class Scrollbar : Selectable, IBeginDragHandler, IEventSystemHandler, IDragHandler, IInitializePotentialDragHandler, ICanvasElement
	{
		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000391 RID: 913 RVA: 0x00012A92 File Offset: 0x00010C92
		// (set) Token: 0x06000392 RID: 914 RVA: 0x00012A9A File Offset: 0x00010C9A
		public RectTransform handleRect
		{
			get
			{
				return this.m_HandleRect;
			}
			set
			{
				if (SetPropertyUtility.SetClass<RectTransform>(ref this.m_HandleRect, value))
				{
					this.UpdateCachedReferences();
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000393 RID: 915 RVA: 0x00012AB6 File Offset: 0x00010CB6
		// (set) Token: 0x06000394 RID: 916 RVA: 0x00012ABE File Offset: 0x00010CBE
		public Scrollbar.Direction direction
		{
			get
			{
				return this.m_Direction;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<Scrollbar.Direction>(ref this.m_Direction, value))
				{
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x06000395 RID: 917 RVA: 0x00012AD4 File Offset: 0x00010CD4
		protected Scrollbar()
		{
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000396 RID: 918 RVA: 0x00012B00 File Offset: 0x00010D00
		// (set) Token: 0x06000397 RID: 919 RVA: 0x00012B39 File Offset: 0x00010D39
		public float value
		{
			get
			{
				float num = this.m_Value;
				if (this.m_NumberOfSteps > 1)
				{
					num = Mathf.Round(num * (float)(this.m_NumberOfSteps - 1)) / (float)(this.m_NumberOfSteps - 1);
				}
				return num;
			}
			set
			{
				this.Set(value, true);
			}
		}

		// Token: 0x06000398 RID: 920 RVA: 0x00012B43 File Offset: 0x00010D43
		public virtual void SetValueWithoutNotify(float input)
		{
			this.Set(input, false);
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000399 RID: 921 RVA: 0x00012B4D File Offset: 0x00010D4D
		// (set) Token: 0x0600039A RID: 922 RVA: 0x00012B55 File Offset: 0x00010D55
		public float size
		{
			get
			{
				return this.m_Size;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_Size, Mathf.Clamp01(value)))
				{
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600039B RID: 923 RVA: 0x00012B70 File Offset: 0x00010D70
		// (set) Token: 0x0600039C RID: 924 RVA: 0x00012B78 File Offset: 0x00010D78
		public int numberOfSteps
		{
			get
			{
				return this.m_NumberOfSteps;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<int>(ref this.m_NumberOfSteps, value))
				{
					this.Set(this.m_Value, true);
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600039D RID: 925 RVA: 0x00012B9B File Offset: 0x00010D9B
		// (set) Token: 0x0600039E RID: 926 RVA: 0x00012BA3 File Offset: 0x00010DA3
		public Scrollbar.ScrollEvent onValueChanged
		{
			get
			{
				return this.m_OnValueChanged;
			}
			set
			{
				this.m_OnValueChanged = value;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600039F RID: 927 RVA: 0x00012BAC File Offset: 0x00010DAC
		private float stepSize
		{
			get
			{
				if (this.m_NumberOfSteps <= 1)
				{
					return 0.1f;
				}
				return 1f / (float)(this.m_NumberOfSteps - 1);
			}
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x00012BCC File Offset: 0x00010DCC
		public virtual void Rebuild(CanvasUpdate executing)
		{
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x00012BCE File Offset: 0x00010DCE
		public virtual void LayoutComplete()
		{
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x00012BD0 File Offset: 0x00010DD0
		public virtual void GraphicUpdateComplete()
		{
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x00012BD2 File Offset: 0x00010DD2
		protected override void OnEnable()
		{
			base.OnEnable();
			this.UpdateCachedReferences();
			this.Set(this.m_Value, false);
			this.UpdateVisuals();
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x00012BF3 File Offset: 0x00010DF3
		protected override void OnDisable()
		{
			this.m_Tracker.Clear();
			base.OnDisable();
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00012C06 File Offset: 0x00010E06
		protected virtual void Update()
		{
			if (this.m_DelayedUpdateVisuals)
			{
				this.m_DelayedUpdateVisuals = false;
				this.UpdateVisuals();
			}
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00012C1D File Offset: 0x00010E1D
		private void UpdateCachedReferences()
		{
			if (this.m_HandleRect && this.m_HandleRect.parent != null)
			{
				this.m_ContainerRect = this.m_HandleRect.parent.GetComponent<RectTransform>();
				return;
			}
			this.m_ContainerRect = null;
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x00012C5D File Offset: 0x00010E5D
		private void Set(float input, bool sendCallback = true)
		{
			float value = this.m_Value;
			this.m_Value = input;
			if (value == this.value)
			{
				return;
			}
			this.UpdateVisuals();
			if (sendCallback)
			{
				UISystemProfilerApi.AddMarker("Scrollbar.value", this);
				this.m_OnValueChanged.Invoke(this.value);
			}
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00012C9A File Offset: 0x00010E9A
		protected override void OnRectTransformDimensionsChange()
		{
			base.OnRectTransformDimensionsChange();
			if (!this.IsActive())
			{
				return;
			}
			this.UpdateVisuals();
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x00012CB1 File Offset: 0x00010EB1
		private Scrollbar.Axis axis
		{
			get
			{
				if (this.m_Direction != Scrollbar.Direction.LeftToRight && this.m_Direction != Scrollbar.Direction.RightToLeft)
				{
					return Scrollbar.Axis.Vertical;
				}
				return Scrollbar.Axis.Horizontal;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060003AA RID: 938 RVA: 0x00012CC7 File Offset: 0x00010EC7
		private bool reverseValue
		{
			get
			{
				return this.m_Direction == Scrollbar.Direction.RightToLeft || this.m_Direction == Scrollbar.Direction.TopToBottom;
			}
		}

		// Token: 0x060003AB RID: 939 RVA: 0x00012CE0 File Offset: 0x00010EE0
		private void UpdateVisuals()
		{
			this.m_Tracker.Clear();
			if (this.m_ContainerRect != null)
			{
				this.m_Tracker.Add(this, this.m_HandleRect, DrivenTransformProperties.Anchors);
				Vector2 zero = Vector2.zero;
				Vector2 one = Vector2.one;
				float num = Mathf.Clamp01(this.value) * (1f - this.size);
				if (this.reverseValue)
				{
					zero[(int)this.axis] = 1f - num - this.size;
					one[(int)this.axis] = 1f - num;
				}
				else
				{
					zero[(int)this.axis] = num;
					one[(int)this.axis] = num + this.size;
				}
				this.m_HandleRect.anchorMin = zero;
				this.m_HandleRect.anchorMax = one;
			}
		}

		// Token: 0x060003AC RID: 940 RVA: 0x00012DBC File Offset: 0x00010FBC
		private void UpdateDrag(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			if (this.m_ContainerRect == null)
			{
				return;
			}
			Vector2 zero = Vector2.zero;
			if (!MultipleDisplayUtilities.GetRelativeMousePositionForDrag(eventData, ref zero))
			{
				return;
			}
			this.UpdateDrag(this.m_ContainerRect, zero, eventData.pressEventCamera);
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00012E08 File Offset: 0x00011008
		private void UpdateDrag(RectTransform containerRect, Vector2 position, Camera camera)
		{
			Vector2 vector;
			if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(containerRect, position, camera, out vector))
			{
				return;
			}
			Vector2 vector2 = vector - this.m_Offset - this.m_ContainerRect.rect.position - (this.m_HandleRect.rect.size - this.m_HandleRect.sizeDelta) * 0.5f;
			float num = ((this.axis == Scrollbar.Axis.Horizontal) ? this.m_ContainerRect.rect.width : this.m_ContainerRect.rect.height) * (1f - this.size);
			if (num <= 0f)
			{
				return;
			}
			this.DoUpdateDrag(vector2, num);
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00012EC8 File Offset: 0x000110C8
		private void DoUpdateDrag(Vector2 handleCorner, float remainingSize)
		{
			switch (this.m_Direction)
			{
			case Scrollbar.Direction.LeftToRight:
				this.Set(Mathf.Clamp01(handleCorner.x / remainingSize), true);
				return;
			case Scrollbar.Direction.RightToLeft:
				this.Set(Mathf.Clamp01(1f - handleCorner.x / remainingSize), true);
				return;
			case Scrollbar.Direction.BottomToTop:
				this.Set(Mathf.Clamp01(handleCorner.y / remainingSize), true);
				return;
			case Scrollbar.Direction.TopToBottom:
				this.Set(Mathf.Clamp01(1f - handleCorner.y / remainingSize), true);
				return;
			default:
				return;
			}
		}

		// Token: 0x060003AF RID: 943 RVA: 0x00012F52 File Offset: 0x00011152
		private bool MayDrag(PointerEventData eventData)
		{
			return this.IsActive() && this.IsInteractable() && eventData.button == PointerEventData.InputButton.Left;
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00012F70 File Offset: 0x00011170
		public virtual void OnBeginDrag(PointerEventData eventData)
		{
			this.isPointerDownAndNotDragging = false;
			if (!this.MayDrag(eventData))
			{
				return;
			}
			if (this.m_ContainerRect == null)
			{
				return;
			}
			this.m_Offset = Vector2.zero;
			Vector2 vector;
			if (RectTransformUtility.RectangleContainsScreenPoint(this.m_HandleRect, eventData.pointerPressRaycast.screenPosition, eventData.enterEventCamera) && RectTransformUtility.ScreenPointToLocalPointInRectangle(this.m_HandleRect, eventData.pointerPressRaycast.screenPosition, eventData.pressEventCamera, out vector))
			{
				this.m_Offset = vector - this.m_HandleRect.rect.center;
			}
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00013005 File Offset: 0x00011205
		public virtual void OnDrag(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			if (this.m_ContainerRect != null)
			{
				this.UpdateDrag(eventData);
			}
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00013026 File Offset: 0x00011226
		public override void OnPointerDown(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			base.OnPointerDown(eventData);
			this.isPointerDownAndNotDragging = true;
			this.m_PointerDownRepeat = base.StartCoroutine(this.ClickRepeat(eventData.pointerPressRaycast.screenPosition, eventData.enterEventCamera));
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x00013063 File Offset: 0x00011263
		protected IEnumerator ClickRepeat(PointerEventData eventData)
		{
			return this.ClickRepeat(eventData.pointerPressRaycast.screenPosition, eventData.enterEventCamera);
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0001307C File Offset: 0x0001127C
		protected IEnumerator ClickRepeat(Vector2 screenPosition, Camera camera)
		{
			while (this.isPointerDownAndNotDragging)
			{
				if (!RectTransformUtility.RectangleContainsScreenPoint(this.m_HandleRect, screenPosition, camera))
				{
					this.UpdateDrag(this.m_ContainerRect, screenPosition, camera);
				}
				yield return new WaitForEndOfFrame();
			}
			base.StopCoroutine(this.m_PointerDownRepeat);
			yield break;
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00013099 File Offset: 0x00011299
		public override void OnPointerUp(PointerEventData eventData)
		{
			base.OnPointerUp(eventData);
			this.isPointerDownAndNotDragging = false;
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x000130AC File Offset: 0x000112AC
		public override void OnMove(AxisEventData eventData)
		{
			if (!this.IsActive() || !this.IsInteractable())
			{
				base.OnMove(eventData);
				return;
			}
			switch (eventData.moveDir)
			{
			case MoveDirection.Left:
				if (this.axis == Scrollbar.Axis.Horizontal && this.FindSelectableOnLeft() == null)
				{
					this.Set(Mathf.Clamp01(this.reverseValue ? (this.value + this.stepSize) : (this.value - this.stepSize)), true);
					return;
				}
				base.OnMove(eventData);
				return;
			case MoveDirection.Up:
				if (this.axis == Scrollbar.Axis.Vertical && this.FindSelectableOnUp() == null)
				{
					this.Set(Mathf.Clamp01(this.reverseValue ? (this.value - this.stepSize) : (this.value + this.stepSize)), true);
					return;
				}
				base.OnMove(eventData);
				return;
			case MoveDirection.Right:
				if (this.axis == Scrollbar.Axis.Horizontal && this.FindSelectableOnRight() == null)
				{
					this.Set(Mathf.Clamp01(this.reverseValue ? (this.value - this.stepSize) : (this.value + this.stepSize)), true);
					return;
				}
				base.OnMove(eventData);
				return;
			case MoveDirection.Down:
				if (this.axis == Scrollbar.Axis.Vertical && this.FindSelectableOnDown() == null)
				{
					this.Set(Mathf.Clamp01(this.reverseValue ? (this.value + this.stepSize) : (this.value - this.stepSize)), true);
					return;
				}
				base.OnMove(eventData);
				return;
			default:
				return;
			}
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0001322C File Offset: 0x0001142C
		public override Selectable FindSelectableOnLeft()
		{
			if (base.navigation.mode == Navigation.Mode.Automatic && this.axis == Scrollbar.Axis.Horizontal)
			{
				return null;
			}
			return base.FindSelectableOnLeft();
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0001325C File Offset: 0x0001145C
		public override Selectable FindSelectableOnRight()
		{
			if (base.navigation.mode == Navigation.Mode.Automatic && this.axis == Scrollbar.Axis.Horizontal)
			{
				return null;
			}
			return base.FindSelectableOnRight();
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0001328C File Offset: 0x0001148C
		public override Selectable FindSelectableOnUp()
		{
			if (base.navigation.mode == Navigation.Mode.Automatic && this.axis == Scrollbar.Axis.Vertical)
			{
				return null;
			}
			return base.FindSelectableOnUp();
		}

		// Token: 0x060003BA RID: 954 RVA: 0x000132BC File Offset: 0x000114BC
		public override Selectable FindSelectableOnDown()
		{
			if (base.navigation.mode == Navigation.Mode.Automatic && this.axis == Scrollbar.Axis.Vertical)
			{
				return null;
			}
			return base.FindSelectableOnDown();
		}

		// Token: 0x060003BB RID: 955 RVA: 0x000132EB File Offset: 0x000114EB
		public virtual void OnInitializePotentialDrag(PointerEventData eventData)
		{
			eventData.useDragThreshold = false;
		}

		// Token: 0x060003BC RID: 956 RVA: 0x000132F4 File Offset: 0x000114F4
		public void SetDirection(Scrollbar.Direction direction, bool includeRectLayouts)
		{
			Scrollbar.Axis axis = this.axis;
			bool reverseValue = this.reverseValue;
			this.direction = direction;
			if (!includeRectLayouts)
			{
				return;
			}
			if (this.axis != axis)
			{
				RectTransformUtility.FlipLayoutAxes(base.transform as RectTransform, true, true);
			}
			if (this.reverseValue != reverseValue)
			{
				RectTransformUtility.FlipLayoutOnAxis(base.transform as RectTransform, (int)this.axis, true, true);
			}
		}

		// Token: 0x060003BD RID: 957 RVA: 0x00013356 File Offset: 0x00011556
		Transform ICanvasElement.get_transform()
		{
			return base.transform;
		}

		// Token: 0x0400013E RID: 318
		[SerializeField]
		private RectTransform m_HandleRect;

		// Token: 0x0400013F RID: 319
		[SerializeField]
		private Scrollbar.Direction m_Direction;

		// Token: 0x04000140 RID: 320
		[Range(0f, 1f)]
		[SerializeField]
		private float m_Value;

		// Token: 0x04000141 RID: 321
		[Range(0f, 1f)]
		[SerializeField]
		private float m_Size = 0.2f;

		// Token: 0x04000142 RID: 322
		[Range(0f, 11f)]
		[SerializeField]
		private int m_NumberOfSteps;

		// Token: 0x04000143 RID: 323
		[Space(6f)]
		[SerializeField]
		private Scrollbar.ScrollEvent m_OnValueChanged = new Scrollbar.ScrollEvent();

		// Token: 0x04000144 RID: 324
		private RectTransform m_ContainerRect;

		// Token: 0x04000145 RID: 325
		private Vector2 m_Offset = Vector2.zero;

		// Token: 0x04000146 RID: 326
		private DrivenRectTransformTracker m_Tracker;

		// Token: 0x04000147 RID: 327
		private Coroutine m_PointerDownRepeat;

		// Token: 0x04000148 RID: 328
		private bool isPointerDownAndNotDragging;

		// Token: 0x04000149 RID: 329
		private bool m_DelayedUpdateVisuals;

		// Token: 0x020000A6 RID: 166
		public enum Direction
		{
			// Token: 0x040002F7 RID: 759
			LeftToRight,
			// Token: 0x040002F8 RID: 760
			RightToLeft,
			// Token: 0x040002F9 RID: 761
			BottomToTop,
			// Token: 0x040002FA RID: 762
			TopToBottom
		}

		// Token: 0x020000A7 RID: 167
		[Serializable]
		public class ScrollEvent : UnityEvent<float>
		{
		}

		// Token: 0x020000A8 RID: 168
		private enum Axis
		{
			// Token: 0x040002FC RID: 764
			Horizontal,
			// Token: 0x040002FD RID: 765
			Vertical
		}
	}
}
