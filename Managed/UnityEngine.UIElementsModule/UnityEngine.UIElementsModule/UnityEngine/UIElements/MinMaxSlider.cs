using System;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Properties;
using UnityEngine.Internal;

namespace UnityEngine.UIElements
{
	// Token: 0x0200013A RID: 314
	public class MinMaxSlider : BaseField<Vector2>
	{
		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060009FB RID: 2555 RVA: 0x0002B61A File Offset: 0x0002981A
		// (set) Token: 0x060009FC RID: 2556 RVA: 0x0002B622 File Offset: 0x00029822
		internal VisualElement dragElement { get; private set; }

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060009FD RID: 2557 RVA: 0x0002B62B File Offset: 0x0002982B
		// (set) Token: 0x060009FE RID: 2558 RVA: 0x0002B633 File Offset: 0x00029833
		internal VisualElement dragMinThumb { get; private set; }

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060009FF RID: 2559 RVA: 0x0002B63C File Offset: 0x0002983C
		// (set) Token: 0x06000A00 RID: 2560 RVA: 0x0002B644 File Offset: 0x00029844
		internal VisualElement dragMaxThumb { get; private set; }

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000A01 RID: 2561 RVA: 0x0002B64D File Offset: 0x0002984D
		// (set) Token: 0x06000A02 RID: 2562 RVA: 0x0002B655 File Offset: 0x00029855
		internal ClampedDragger<float> clampedDragger { get; private set; }

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000A03 RID: 2563 RVA: 0x0002B660 File Offset: 0x00029860
		// (set) Token: 0x06000A04 RID: 2564 RVA: 0x0002B680 File Offset: 0x00029880
		[CreateProperty]
		public float minValue
		{
			get
			{
				return this.value.x;
			}
			set
			{
				float minValue = this.minValue;
				base.value = this.ClampValues(new Vector2(value, base.rawValue.y));
				bool flag = !Mathf.Approximately(minValue, this.minValue);
				if (flag)
				{
					base.NotifyPropertyChanged(in MinMaxSlider.minValueProperty);
				}
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000A05 RID: 2565 RVA: 0x0002B6D4 File Offset: 0x000298D4
		// (set) Token: 0x06000A06 RID: 2566 RVA: 0x0002B6F4 File Offset: 0x000298F4
		[CreateProperty]
		public float maxValue
		{
			get
			{
				return this.value.y;
			}
			set
			{
				float maxValue = this.maxValue;
				base.value = this.ClampValues(new Vector2(base.rawValue.x, value));
				bool flag = !Mathf.Approximately(maxValue, this.maxValue);
				if (flag)
				{
					base.NotifyPropertyChanged(in MinMaxSlider.maxValueProperty);
				}
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000A07 RID: 2567 RVA: 0x0002B748 File Offset: 0x00029948
		// (set) Token: 0x06000A08 RID: 2568 RVA: 0x0002B760 File Offset: 0x00029960
		public override Vector2 value
		{
			get
			{
				return base.value;
			}
			set
			{
				base.value = this.ClampValues(value);
			}
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x0002B771 File Offset: 0x00029971
		public override void SetValueWithoutNotify(Vector2 newValue)
		{
			base.SetValueWithoutNotify(this.ClampValues(newValue));
			this.UpdateDragElementPosition();
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000A0A RID: 2570 RVA: 0x0002B78C File Offset: 0x0002998C
		[CreateProperty(ReadOnly = true)]
		public float range
		{
			get
			{
				return Math.Abs(this.highLimit - this.lowLimit);
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000A0B RID: 2571 RVA: 0x0002B7B0 File Offset: 0x000299B0
		// (set) Token: 0x06000A0C RID: 2572 RVA: 0x0002B7C8 File Offset: 0x000299C8
		[CreateProperty]
		public float lowLimit
		{
			get
			{
				return this.m_MinLimit;
			}
			set
			{
				bool flag = !Mathf.Approximately(this.m_MinLimit, value);
				if (flag)
				{
					bool flag2 = value > this.m_MaxLimit;
					if (flag2)
					{
						throw new ArgumentException("lowLimit is greater than highLimit");
					}
					this.m_MinLimit = value;
					this.value = base.rawValue;
					this.UpdateDragElementPosition();
					bool flag3 = !string.IsNullOrEmpty(base.viewDataKey);
					if (flag3)
					{
						base.SaveViewData();
					}
					base.NotifyPropertyChanged(in MinMaxSlider.lowLimitProperty);
				}
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000A0D RID: 2573 RVA: 0x0002B844 File Offset: 0x00029A44
		// (set) Token: 0x06000A0E RID: 2574 RVA: 0x0002B85C File Offset: 0x00029A5C
		[CreateProperty]
		public float highLimit
		{
			get
			{
				return this.m_MaxLimit;
			}
			set
			{
				bool flag = !Mathf.Approximately(this.m_MaxLimit, value);
				if (flag)
				{
					bool flag2 = value < this.m_MinLimit;
					if (flag2)
					{
						throw new ArgumentException("highLimit is smaller than lowLimit");
					}
					this.m_MaxLimit = value;
					this.value = base.rawValue;
					this.UpdateDragElementPosition();
					bool flag3 = !string.IsNullOrEmpty(base.viewDataKey);
					if (flag3)
					{
						base.SaveViewData();
					}
					base.NotifyPropertyChanged(in MinMaxSlider.highLimitProperty);
				}
			}
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x0002B8D8 File Offset: 0x00029AD8
		public MinMaxSlider()
			: this(null, 0f, 10f, float.MinValue, float.MaxValue)
		{
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x0002B8F7 File Offset: 0x00029AF7
		public MinMaxSlider(float minValue, float maxValue, float minLimit, float maxLimit)
			: this(null, minValue, maxValue, minLimit, maxLimit)
		{
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x0002B908 File Offset: 0x00029B08
		public MinMaxSlider(string label, float minValue = 0f, float maxValue = 10f, float minLimit = -3.4028235E+38f, float maxLimit = 3.4028235E+38f)
			: base(label, null)
		{
			this.m_MinLimit = float.MinValue;
			this.m_MaxLimit = float.MaxValue;
			this.lowLimit = minLimit;
			this.highLimit = maxLimit;
			Vector2 vector = this.ClampValues(new Vector2(minValue, maxValue));
			this.minValue = vector.x;
			this.maxValue = vector.y;
			base.AddToClassList(MinMaxSlider.ussClassName);
			base.labelElement.AddToClassList(MinMaxSlider.labelUssClassName);
			base.visualInput.AddToClassList(MinMaxSlider.inputUssClassName);
			base.pickingMode = PickingMode.Ignore;
			this.m_DragState = MinMaxSlider.DragState.NoThumb;
			base.visualInput.pickingMode = PickingMode.Position;
			VisualElement visualElement = new VisualElement
			{
				name = "unity-tracker"
			};
			visualElement.AddToClassList(MinMaxSlider.trackerUssClassName);
			base.visualInput.Add(visualElement);
			this.dragElement = new VisualElement
			{
				name = "unity-dragger"
			};
			this.dragElement.AddToClassList(MinMaxSlider.draggerUssClassName);
			this.dragElement.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.UpdateDragElementPosition), TrickleDown.NoTrickleDown);
			base.visualInput.Add(this.dragElement);
			this.dragMinThumb = new VisualElement
			{
				name = "unity-thumb-min"
			};
			this.dragMaxThumb = new VisualElement
			{
				name = "unity-thumb-max"
			};
			this.dragMinThumb.AddToClassList(MinMaxSlider.minThumbUssClassName);
			this.dragMaxThumb.AddToClassList(MinMaxSlider.maxThumbUssClassName);
			this.dragElement.Add(this.dragMinThumb);
			this.dragElement.Add(this.dragMaxThumb);
			this.clampedDragger = new ClampedDragger<float>(null, new Action(this.SetSliderValueFromClick), new Action(this.SetSliderValueFromDrag));
			base.visualInput.AddManipulator(this.clampedDragger);
			this.m_MinLimit = minLimit;
			this.m_MaxLimit = maxLimit;
			base.rawValue = this.ClampValues(new Vector2(minValue, maxValue));
			this.UpdateDragElementPosition();
			base.RegisterCallback<FocusInEvent>(new EventCallback<FocusInEvent>(this.OnFocusIn), TrickleDown.NoTrickleDown);
			base.RegisterCallback<BlurEvent>(new EventCallback<BlurEvent>(this.OnBlur), TrickleDown.NoTrickleDown);
			base.RegisterCallback<NavigationSubmitEvent>(new EventCallback<NavigationSubmitEvent>(this.OnNavigationSubmit), TrickleDown.NoTrickleDown);
			base.RegisterCallback<NavigationMoveEvent>(new EventCallback<NavigationMoveEvent>(this.OnNavigationMove), TrickleDown.NoTrickleDown);
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x0002BB60 File Offset: 0x00029D60
		private Vector2 ClampValues(Vector2 valueToClamp)
		{
			bool flag = this.m_MinLimit > this.m_MaxLimit;
			if (flag)
			{
				this.m_MinLimit = this.m_MaxLimit;
			}
			Vector2 vector = default(Vector2);
			bool flag2 = valueToClamp.y > this.m_MaxLimit;
			if (flag2)
			{
				valueToClamp.y = this.m_MaxLimit;
			}
			vector.x = Mathf.Clamp(valueToClamp.x, this.m_MinLimit, valueToClamp.y);
			vector.y = Mathf.Clamp(valueToClamp.y, valueToClamp.x, this.m_MaxLimit);
			return vector;
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x0002BBF8 File Offset: 0x00029DF8
		private void UpdateDragElementPosition(GeometryChangedEvent evt)
		{
			bool flag = evt.oldRect.size == evt.newRect.size;
			if (!flag)
			{
				this.UpdateDragElementPosition();
			}
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x0002BC38 File Offset: 0x00029E38
		private void UpdateDragElementPosition()
		{
			bool flag = base.panel == null;
			if (!flag)
			{
				float num = this.dragElement.resolvedStyle.borderLeftWidth + this.dragElement.resolvedStyle.marginLeft;
				float num2 = this.dragElement.resolvedStyle.borderRightWidth + this.dragElement.resolvedStyle.marginRight;
				float num3 = num2 + num;
				float num4 = this.dragMinThumb.resolvedStyle.width + this.dragMaxThumb.resolvedStyle.width + num3;
				float num5 = this.RoundToPanelPixelSize(this.SliderLerpUnclamped(this.dragMinThumb.resolvedStyle.width, base.visualInput.layout.width - this.dragMaxThumb.resolvedStyle.width - num3, this.SliderNormalizeValue(this.minValue, this.lowLimit, this.highLimit)));
				float num6 = this.RoundToPanelPixelSize(this.SliderLerpUnclamped(this.dragMinThumb.resolvedStyle.width + num3, base.visualInput.layout.width - this.dragMaxThumb.resolvedStyle.width, this.SliderNormalizeValue(this.maxValue, this.lowLimit, this.highLimit)));
				this.dragElement.style.width = num6 - num5;
				this.dragElement.style.left = num5;
				this.dragMinThumb.style.left = -this.dragMinThumb.resolvedStyle.width - num;
				this.dragMaxThumb.style.right = -this.dragMaxThumb.resolvedStyle.width - num2;
			}
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x0002BE08 File Offset: 0x0002A008
		internal float SliderLerpUnclamped(float a, float b, float interpolant)
		{
			return Mathf.LerpUnclamped(a, b, interpolant);
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x0002BE24 File Offset: 0x0002A024
		internal float SliderNormalizeValue(float currentValue, float lowerValue, float higherValue)
		{
			return (currentValue - lowerValue) / (higherValue - lowerValue);
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x0002BE40 File Offset: 0x0002A040
		private float ComputeValueFromPosition(float positionToConvert)
		{
			float num = this.SliderNormalizeValue(positionToConvert, 0f, base.visualInput.layout.width);
			return this.SliderLerpUnclamped(this.lowLimit, this.highLimit, num);
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x0002BE88 File Offset: 0x0002A088
		[EventInterest(new Type[] { typeof(GeometryChangedEvent) })]
		protected override void HandleEventBubbleUp(EventBase evt)
		{
			base.HandleEventBubbleUp(evt);
			bool flag = evt == null;
			if (!flag)
			{
				bool flag2 = evt.eventTypeId == EventBase<GeometryChangedEvent>.TypeId();
				if (flag2)
				{
					this.UpdateDragElementPosition((GeometryChangedEvent)evt);
				}
			}
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x0002BECC File Offset: 0x0002A0CC
		private MinMaxSlider.DragState GetNavigationState()
		{
			bool flag = this.dragMinThumb.ClassListContains(MinMaxSlider.movableUssClassName);
			bool flag2 = this.dragMaxThumb.ClassListContains(MinMaxSlider.movableUssClassName);
			bool flag3 = flag;
			MinMaxSlider.DragState dragState;
			if (flag3)
			{
				dragState = (flag2 ? MinMaxSlider.DragState.MiddleThumb : MinMaxSlider.DragState.MinThumb);
			}
			else
			{
				bool flag4 = flag2;
				if (flag4)
				{
					dragState = MinMaxSlider.DragState.MaxThumb;
				}
				else
				{
					dragState = MinMaxSlider.DragState.NoThumb;
				}
			}
			return dragState;
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x0002BF1C File Offset: 0x0002A11C
		private void SetNavigationState(MinMaxSlider.DragState newState)
		{
			this.dragMinThumb.EnableInClassList(MinMaxSlider.movableUssClassName, newState == MinMaxSlider.DragState.MinThumb || newState == MinMaxSlider.DragState.MiddleThumb);
			this.dragMaxThumb.EnableInClassList(MinMaxSlider.movableUssClassName, newState == MinMaxSlider.DragState.MaxThumb || newState == MinMaxSlider.DragState.MiddleThumb);
			this.dragElement.EnableInClassList(MinMaxSlider.movableUssClassName, newState == MinMaxSlider.DragState.MiddleThumb);
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x0002BF78 File Offset: 0x0002A178
		private void OnFocusIn(FocusInEvent evt)
		{
			bool flag = this.GetNavigationState() == MinMaxSlider.DragState.NoThumb;
			if (flag)
			{
				this.SetNavigationState(MinMaxSlider.DragState.MinThumb);
			}
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x0002BF9B File Offset: 0x0002A19B
		private void OnBlur(BlurEvent evt)
		{
			this.SetNavigationState(MinMaxSlider.DragState.NoThumb);
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x0002BFA8 File Offset: 0x0002A1A8
		private void OnNavigationSubmit(NavigationSubmitEvent evt)
		{
			MinMaxSlider.DragState dragState = this.GetNavigationState() + 1;
			bool flag = dragState > MinMaxSlider.DragState.NoThumb;
			if (flag)
			{
				dragState = MinMaxSlider.DragState.MinThumb;
			}
			this.SetNavigationState(dragState);
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x0002BFD4 File Offset: 0x0002A1D4
		private void OnNavigationMove(NavigationMoveEvent evt)
		{
			MinMaxSlider.DragState navigationState = this.GetNavigationState();
			bool flag = navigationState == MinMaxSlider.DragState.NoThumb;
			if (!flag)
			{
				bool flag2 = evt.direction != NavigationMoveEvent.Direction.Left && evt.direction != NavigationMoveEvent.Direction.Right;
				if (!flag2)
				{
					this.ComputeValueFromKey(evt.direction == NavigationMoveEvent.Direction.Left, evt.shiftKey, navigationState);
					evt.StopPropagation();
					FocusController focusController = this.focusController;
					if (focusController != null)
					{
						focusController.IgnoreEvent(evt);
					}
				}
			}
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x0002C044 File Offset: 0x0002A244
		private void ComputeValueFromKey(bool leftDirection, bool isShift, MinMaxSlider.DragState moveState)
		{
			float num = BaseSlider<float>.GetClosestPowerOfTen(Mathf.Abs((this.highLimit - this.lowLimit) * 0.01f));
			if (isShift)
			{
				num *= 10f;
			}
			if (leftDirection)
			{
				num = -num;
			}
			switch (moveState)
			{
			case MinMaxSlider.DragState.MinThumb:
			{
				float num2 = BaseSlider<float>.RoundToMultipleOf(this.value.x + num * 0.5001f, Mathf.Abs(num));
				num2 = Math.Clamp(num2, this.lowLimit, this.value.y);
				this.value = new Vector2(num2, this.value.y);
				break;
			}
			case MinMaxSlider.DragState.MaxThumb:
			{
				float num3 = BaseSlider<float>.RoundToMultipleOf(this.value.y + num * 0.5001f, Mathf.Abs(num));
				num3 = Math.Clamp(num3, this.value.x, this.highLimit);
				this.value = new Vector2(this.value.x, num3);
				break;
			}
			case MinMaxSlider.DragState.MiddleThumb:
			{
				float num4 = this.value.y - this.value.x;
				bool flag = num > 0f;
				if (flag)
				{
					float num5 = BaseSlider<float>.RoundToMultipleOf(this.value.y + num * 0.5001f, Mathf.Abs(num));
					num5 = Math.Clamp(num5, this.value.x, this.highLimit);
					this.value = new Vector2(num5 - num4, num5);
				}
				else
				{
					float num6 = BaseSlider<float>.RoundToMultipleOf(this.value.x + num * 0.5001f, Mathf.Abs(num));
					num6 = Math.Clamp(num6, this.lowLimit, this.value.y);
					this.value = new Vector2(num6, num6 + num4);
				}
				break;
			}
			}
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x0002C220 File Offset: 0x0002A420
		private void SetSliderValueFromDrag()
		{
			bool flag = this.clampedDragger.dragDirection != ClampedDragger<float>.DragDirection.Free;
			if (!flag)
			{
				float x = this.m_DragElementStartPos.x;
				float num = x + this.clampedDragger.delta.x;
				this.ComputeValueFromDraggingThumb(x, num);
			}
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x0002C270 File Offset: 0x0002A470
		private void SetSliderValueFromClick()
		{
			bool flag = this.clampedDragger.dragDirection == ClampedDragger<float>.DragDirection.Free;
			if (!flag)
			{
				Vector2 vector = base.visualInput.LocalToWorld(this.clampedDragger.startMousePosition);
				bool flag2 = this.dragMinThumb.worldBound.Contains(vector);
				if (flag2)
				{
					this.m_DragState = MinMaxSlider.DragState.MinThumb;
				}
				else
				{
					bool flag3 = this.dragMaxThumb.worldBound.Contains(vector);
					if (flag3)
					{
						this.m_DragState = MinMaxSlider.DragState.MaxThumb;
					}
					else
					{
						bool flag4 = this.clampedDragger.startMousePosition.x > this.dragElement.layout.xMin && this.clampedDragger.startMousePosition.x < this.dragElement.layout.xMax;
						if (flag4)
						{
							this.m_DragState = MinMaxSlider.DragState.MiddleThumb;
						}
						else
						{
							this.m_DragState = MinMaxSlider.DragState.NoThumb;
						}
					}
				}
				bool flag5 = this.m_DragState == MinMaxSlider.DragState.NoThumb;
				if (flag5)
				{
					float num = this.ComputeValueFromPosition(this.clampedDragger.startMousePosition.x);
					bool flag6 = this.clampedDragger.startMousePosition.x < this.dragElement.layout.x;
					if (flag6)
					{
						this.m_DragState = MinMaxSlider.DragState.MinThumb;
						this.value = new Vector2(num, this.value.y);
					}
					else
					{
						this.m_DragState = MinMaxSlider.DragState.MaxThumb;
						this.value = new Vector2(this.value.x, num);
					}
				}
				this.SetNavigationState(this.m_DragState);
				this.m_ValueStartPos = this.value;
				this.clampedDragger.dragDirection = ClampedDragger<float>.DragDirection.Free;
				this.m_DragElementStartPos = this.clampedDragger.startMousePosition;
			}
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x0002C434 File Offset: 0x0002A634
		private void ComputeValueFromDraggingThumb(float dragElementStartPos, float dragElementEndPos)
		{
			float num = this.ComputeValueFromPosition(dragElementStartPos);
			float num2 = this.ComputeValueFromPosition(dragElementEndPos);
			float num3 = num2 - num;
			this.SetNavigationState(this.m_DragState);
			switch (this.m_DragState)
			{
			case MinMaxSlider.DragState.MinThumb:
			{
				float num4 = this.m_ValueStartPos.x + num3;
				bool flag = num4 > this.maxValue;
				if (flag)
				{
					num4 = this.maxValue;
				}
				else
				{
					bool flag2 = num4 < this.lowLimit;
					if (flag2)
					{
						num4 = this.lowLimit;
					}
				}
				this.value = new Vector2(num4, this.maxValue);
				break;
			}
			case MinMaxSlider.DragState.MaxThumb:
			{
				float num5 = this.m_ValueStartPos.y + num3;
				bool flag3 = num5 < this.minValue;
				if (flag3)
				{
					num5 = this.minValue;
				}
				else
				{
					bool flag4 = num5 > this.highLimit;
					if (flag4)
					{
						num5 = this.highLimit;
					}
				}
				this.value = new Vector2(this.minValue, num5);
				break;
			}
			case MinMaxSlider.DragState.MiddleThumb:
			{
				Vector2 value = this.value;
				value.x = this.m_ValueStartPos.x + num3;
				value.y = this.m_ValueStartPos.y + num3;
				float num6 = this.m_ValueStartPos.y - this.m_ValueStartPos.x;
				bool flag5 = value.x < this.lowLimit;
				if (flag5)
				{
					value.x = this.lowLimit;
					value.y = this.lowLimit + num6;
				}
				else
				{
					bool flag6 = value.y > this.highLimit;
					if (flag6)
					{
						value.y = this.highLimit;
						value.x = this.highLimit - num6;
					}
				}
				this.value = value;
				break;
			}
			}
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x00002152 File Offset: 0x00000352
		protected override void UpdateMixedValueContent()
		{
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x0002C5FF File Offset: 0x0002A7FF
		internal override void RegisterEditingCallbacks()
		{
			base.visualInput.RegisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(base.StartEditing), TrickleDown.TrickleDown);
			base.visualInput.RegisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(base.EndEditing), TrickleDown.NoTrickleDown);
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x0002C634 File Offset: 0x0002A834
		internal override void UnregisterEditingCallbacks()
		{
			base.visualInput.UnregisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(base.StartEditing), TrickleDown.TrickleDown);
			base.visualInput.UnregisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(base.EndEditing), TrickleDown.NoTrickleDown);
		}

		// Token: 0x040005A1 RID: 1441
		internal static readonly BindingId minValueProperty = "minValue";

		// Token: 0x040005A2 RID: 1442
		internal static readonly BindingId maxValueProperty = "maxValue";

		// Token: 0x040005A3 RID: 1443
		internal static readonly BindingId rangeProperty = "range";

		// Token: 0x040005A4 RID: 1444
		internal static readonly BindingId lowLimitProperty = "lowLimit";

		// Token: 0x040005A5 RID: 1445
		internal static readonly BindingId highLimitProperty = "highLimit";

		// Token: 0x040005AA RID: 1450
		private Vector2 m_DragElementStartPos;

		// Token: 0x040005AB RID: 1451
		private Vector2 m_ValueStartPos;

		// Token: 0x040005AC RID: 1452
		private MinMaxSlider.DragState m_DragState;

		// Token: 0x040005AD RID: 1453
		private float m_MinLimit;

		// Token: 0x040005AE RID: 1454
		private float m_MaxLimit;

		// Token: 0x040005AF RID: 1455
		internal const float kDefaultHighValue = 10f;

		// Token: 0x040005B0 RID: 1456
		public new static readonly string ussClassName = "unity-min-max-slider";

		// Token: 0x040005B1 RID: 1457
		public new static readonly string labelUssClassName = MinMaxSlider.ussClassName + "__label";

		// Token: 0x040005B2 RID: 1458
		public new static readonly string inputUssClassName = MinMaxSlider.ussClassName + "__input";

		// Token: 0x040005B3 RID: 1459
		public static readonly string trackerUssClassName = MinMaxSlider.ussClassName + "__tracker";

		// Token: 0x040005B4 RID: 1460
		public static readonly string draggerUssClassName = MinMaxSlider.ussClassName + "__dragger";

		// Token: 0x040005B5 RID: 1461
		public static readonly string minThumbUssClassName = MinMaxSlider.ussClassName + "__min-thumb";

		// Token: 0x040005B6 RID: 1462
		public static readonly string maxThumbUssClassName = MinMaxSlider.ussClassName + "__max-thumb";

		// Token: 0x040005B7 RID: 1463
		public static readonly string movableUssClassName = MinMaxSlider.ussClassName + "--movable";

		// Token: 0x0200013B RID: 315
		[ExcludeFromDocs]
		[Serializable]
		public new class UxmlSerializedData : BaseField<Vector2>.UxmlSerializedData, IUxmlSerializedDataCustomAttributeHandler
		{
			// Token: 0x06000A27 RID: 2599 RVA: 0x0002C75C File Offset: 0x0002A95C
			[Conditional("UNITY_EDITOR")]
			public new static void Register()
			{
				BaseField<Vector2>.UxmlSerializedData.Register();
				UxmlDescriptionCache.RegisterType(typeof(MinMaxSlider.UxmlSerializedData), new UxmlAttributeNames[]
				{
					new UxmlAttributeNames("lowLimit", "low-limit", null, Array.Empty<string>()),
					new UxmlAttributeNames("highLimit", "high-limit", null, Array.Empty<string>())
				});
			}

			// Token: 0x06000A28 RID: 2600 RVA: 0x0002C7C0 File Offset: 0x0002A9C0
			void IUxmlSerializedDataCustomAttributeHandler.SerializeCustomAttributes(IUxmlAttributes bag, HashSet<string> handledAttributes)
			{
				int num = 0;
				float num2 = UxmlUtility.TryParseFloatAttribute("min-value", bag, ref num);
				float num3 = UxmlUtility.TryParseFloatAttribute("max-value", bag, ref num);
				bool flag = num > 0;
				if (flag)
				{
					base.Value = new Vector2(num2, num3);
					handledAttributes.Add("value");
					UxmlAsset uxmlAsset = bag as UxmlAsset;
					bool flag2 = uxmlAsset != null;
					if (flag2)
					{
						uxmlAsset.RemoveAttribute("min-value");
						uxmlAsset.RemoveAttribute("max-value");
						uxmlAsset.SetAttribute("value", UxmlUtility.ValueToString(base.Value));
					}
				}
			}

			// Token: 0x06000A29 RID: 2601 RVA: 0x0002C857 File Offset: 0x0002AA57
			public override object CreateInstance()
			{
				return new MinMaxSlider();
			}

			// Token: 0x06000A2A RID: 2602 RVA: 0x0002C860 File Offset: 0x0002AA60
			public override void Deserialize(object obj)
			{
				base.Deserialize(obj);
				MinMaxSlider minMaxSlider = (MinMaxSlider)obj;
				bool flag = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.lowLimit_UxmlAttributeFlags);
				if (flag)
				{
					minMaxSlider.lowLimit = this.lowLimit;
				}
				bool flag2 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.highLimit_UxmlAttributeFlags);
				if (flag2)
				{
					minMaxSlider.highLimit = this.highLimit;
				}
			}

			// Token: 0x040005B8 RID: 1464
			[SerializeField]
			private float lowLimit;

			// Token: 0x040005B9 RID: 1465
			[SerializeField]
			private float highLimit;

			// Token: 0x040005BA RID: 1466
			[HideInInspector]
			[UxmlIgnore]
			[SerializeField]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags lowLimit_UxmlAttributeFlags;

			// Token: 0x040005BB RID: 1467
			[UxmlIgnore]
			[SerializeField]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags highLimit_UxmlAttributeFlags;
		}

		// Token: 0x0200013C RID: 316
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<MinMaxSlider, MinMaxSlider.UxmlTraits>
		{
		}

		// Token: 0x0200013D RID: 317
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : BaseField<Vector2>.UxmlTraits
		{
			// Token: 0x06000A2D RID: 2605 RVA: 0x0002C8C8 File Offset: 0x0002AAC8
			public UxmlTraits()
			{
				this.m_PickingMode.defaultValue = PickingMode.Ignore;
			}

			// Token: 0x06000A2E RID: 2606 RVA: 0x0002C978 File Offset: 0x0002AB78
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				MinMaxSlider minMaxSlider = (MinMaxSlider)ve;
				minMaxSlider.lowLimit = this.m_LowLimit.GetValueFromBag(bag, cc);
				minMaxSlider.highLimit = this.m_HighLimit.GetValueFromBag(bag, cc);
				Vector2 vector = new Vector2(this.m_MinValue.GetValueFromBag(bag, cc), this.m_MaxValue.GetValueFromBag(bag, cc));
				minMaxSlider.value = vector;
			}

			// Token: 0x040005BC RID: 1468
			private UxmlFloatAttributeDescription m_MinValue = new UxmlFloatAttributeDescription
			{
				name = "min-value",
				defaultValue = 0f
			};

			// Token: 0x040005BD RID: 1469
			private UxmlFloatAttributeDescription m_MaxValue = new UxmlFloatAttributeDescription
			{
				name = "max-value",
				defaultValue = 10f
			};

			// Token: 0x040005BE RID: 1470
			private UxmlFloatAttributeDescription m_LowLimit = new UxmlFloatAttributeDescription
			{
				name = "low-limit",
				defaultValue = float.MinValue
			};

			// Token: 0x040005BF RID: 1471
			private UxmlFloatAttributeDescription m_HighLimit = new UxmlFloatAttributeDescription
			{
				name = "high-limit",
				defaultValue = float.MaxValue
			};
		}

		// Token: 0x0200013E RID: 318
		private enum DragState
		{
			// Token: 0x040005C1 RID: 1473
			MinThumb,
			// Token: 0x040005C2 RID: 1474
			MaxThumb,
			// Token: 0x040005C3 RID: 1475
			MiddleThumb,
			// Token: 0x040005C4 RID: 1476
			NoThumb
		}
	}
}
