using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Unity.Properties;
using UnityEngine.Internal;

namespace UnityEngine.UIElements
{
	// Token: 0x02000090 RID: 144
	public abstract class BaseSlider<TValueType> : BaseField<TValueType>, IValueField<TValueType> where TValueType : IComparable<TValueType>
	{
		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000562 RID: 1378 RVA: 0x000198EE File Offset: 0x00017AEE
		// (set) Token: 0x06000563 RID: 1379 RVA: 0x000198F6 File Offset: 0x00017AF6
		internal VisualElement dragContainer { get; private set; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000564 RID: 1380 RVA: 0x000198FF File Offset: 0x00017AFF
		// (set) Token: 0x06000565 RID: 1381 RVA: 0x00019907 File Offset: 0x00017B07
		internal VisualElement dragElement { get; private set; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000566 RID: 1382 RVA: 0x00019910 File Offset: 0x00017B10
		// (set) Token: 0x06000567 RID: 1383 RVA: 0x00019918 File Offset: 0x00017B18
		internal VisualElement trackElement { get; private set; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000568 RID: 1384 RVA: 0x00019921 File Offset: 0x00017B21
		// (set) Token: 0x06000569 RID: 1385 RVA: 0x00019929 File Offset: 0x00017B29
		internal VisualElement dragBorderElement { get; private set; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600056A RID: 1386 RVA: 0x00019932 File Offset: 0x00017B32
		// (set) Token: 0x0600056B RID: 1387 RVA: 0x0001993A File Offset: 0x00017B3A
		internal TextField inputTextField { get; private set; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600056C RID: 1388 RVA: 0x00019943 File Offset: 0x00017B43
		// (set) Token: 0x0600056D RID: 1389 RVA: 0x0001994B File Offset: 0x00017B4B
		internal VisualElement fillElement { get; private set; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600056E RID: 1390 RVA: 0x00019954 File Offset: 0x00017B54
		private protected override bool canSwitchToMixedValue
		{
			get
			{
				bool flag = this.inputTextField == null;
				return flag || !this.inputTextField.textInputBase.textElement.hasFocus;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600056F RID: 1391 RVA: 0x00019990 File Offset: 0x00017B90
		// (set) Token: 0x06000570 RID: 1392 RVA: 0x000199A8 File Offset: 0x00017BA8
		[CreateProperty]
		public TValueType lowValue
		{
			get
			{
				return this.m_LowValue;
			}
			set
			{
				bool flag = !EqualityComparer<TValueType>.Default.Equals(this.m_LowValue, value);
				if (flag)
				{
					this.m_LowValue = value;
					this.ClampValue();
					this.UpdateDragElementPosition();
					base.SaveViewData();
					base.NotifyPropertyChanged(in BaseSlider<TValueType>.lowValueProperty);
				}
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x000199F8 File Offset: 0x00017BF8
		// (set) Token: 0x06000572 RID: 1394 RVA: 0x00019A10 File Offset: 0x00017C10
		[CreateProperty]
		public TValueType highValue
		{
			get
			{
				return this.m_HighValue;
			}
			set
			{
				bool flag = !EqualityComparer<TValueType>.Default.Equals(this.m_HighValue, value);
				if (flag)
				{
					this.m_HighValue = value;
					this.ClampValue();
					this.UpdateDragElementPosition();
					base.SaveViewData();
					base.NotifyPropertyChanged(in BaseSlider<TValueType>.highValueProperty);
				}
			}
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x00019A60 File Offset: 0x00017C60
		internal void SetHighValueWithoutNotify(TValueType newHighValue)
		{
			this.m_HighValue = newHighValue;
			TValueType tvalueType = (this.clamped ? this.GetClampedValue(this.value) : this.value);
			this.SetValueWithoutNotify(tvalueType);
			this.UpdateDragElementPosition();
			base.SaveViewData();
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x00019AA8 File Offset: 0x00017CA8
		[CreateProperty(ReadOnly = true)]
		public TValueType range
		{
			get
			{
				return this.SliderRange();
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x00019AC0 File Offset: 0x00017CC0
		// (set) Token: 0x06000576 RID: 1398 RVA: 0x00019AD8 File Offset: 0x00017CD8
		[CreateProperty]
		public virtual float pageSize
		{
			get
			{
				return this.m_PageSize;
			}
			set
			{
				bool flag = this.m_PageSize == value;
				if (!flag)
				{
					this.m_PageSize = value;
					base.NotifyPropertyChanged(in BaseSlider<TValueType>.pageSizeProperty);
				}
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x00019B08 File Offset: 0x00017D08
		// (set) Token: 0x06000578 RID: 1400 RVA: 0x00019B20 File Offset: 0x00017D20
		[CreateProperty]
		public virtual bool showInputField
		{
			get
			{
				return this.m_ShowInputField;
			}
			set
			{
				bool flag = this.m_ShowInputField != value;
				if (flag)
				{
					this.m_ShowInputField = value;
					this.UpdateTextFieldVisibility();
					base.NotifyPropertyChanged(in BaseSlider<TValueType>.showInputFieldProperty);
				}
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000579 RID: 1401 RVA: 0x00019B5A File Offset: 0x00017D5A
		// (set) Token: 0x0600057A RID: 1402 RVA: 0x00019B64 File Offset: 0x00017D64
		[CreateProperty]
		public bool fill
		{
			get
			{
				return this.m_Fill;
			}
			set
			{
				bool flag = this.m_Fill == value;
				if (!flag)
				{
					this.m_Fill = value;
					if (value)
					{
						this.UpdateDragElementPosition();
					}
					else
					{
						bool flag2 = this.fillElement != null;
						if (flag2)
						{
							this.fillElement.RemoveFromHierarchy();
							this.fillElement = null;
						}
					}
					base.NotifyPropertyChanged(in BaseSlider<TValueType>.fillProperty);
				}
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600057B RID: 1403 RVA: 0x00019BC7 File Offset: 0x00017DC7
		// (set) Token: 0x0600057C RID: 1404 RVA: 0x00019BCF File Offset: 0x00017DCF
		internal bool clamped { get; set; } = true;

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600057D RID: 1405 RVA: 0x00019BD8 File Offset: 0x00017DD8
		// (set) Token: 0x0600057E RID: 1406 RVA: 0x00019BE0 File Offset: 0x00017DE0
		internal ClampedDragger<TValueType> clampedDragger { get; private set; }

		// Token: 0x0600057F RID: 1407 RVA: 0x00019BEC File Offset: 0x00017DEC
		private TValueType Clamp(TValueType value, TValueType lowBound, TValueType highBound)
		{
			TValueType tvalueType = value;
			bool flag = lowBound.CompareTo(value) > 0;
			if (flag)
			{
				tvalueType = lowBound;
			}
			else
			{
				bool flag2 = highBound.CompareTo(value) < 0;
				if (flag2)
				{
					tvalueType = highBound;
				}
			}
			return tvalueType;
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x00019C38 File Offset: 0x00017E38
		private TValueType GetClampedValue(TValueType newValue)
		{
			TValueType tvalueType = this.lowValue;
			TValueType tvalueType2 = this.highValue;
			bool flag = tvalueType.CompareTo(tvalueType2) > 0;
			if (flag)
			{
				TValueType tvalueType3 = tvalueType;
				tvalueType = tvalueType2;
				tvalueType2 = tvalueType3;
			}
			return this.Clamp(newValue, tvalueType, tvalueType2);
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000581 RID: 1409 RVA: 0x00019C80 File Offset: 0x00017E80
		// (set) Token: 0x06000582 RID: 1410 RVA: 0x00019C98 File Offset: 0x00017E98
		public override TValueType value
		{
			get
			{
				return base.value;
			}
			set
			{
				TValueType tvalueType = (this.clamped ? this.GetClampedValue(value) : value);
				base.value = tvalueType;
			}
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00002152 File Offset: 0x00000352
		public virtual void ApplyInputDeviceDelta(Vector3 delta, DeltaSpeed speed, TValueType startValue)
		{
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x00002152 File Offset: 0x00000352
		void IValueField<TValueType>.StartDragging()
		{
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x00002152 File Offset: 0x00000352
		void IValueField<TValueType>.StopDragging()
		{
		}

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06000586 RID: 1414 RVA: 0x00019CC4 File Offset: 0x00017EC4
		// (remove) Token: 0x06000587 RID: 1415 RVA: 0x00019CFC File Offset: 0x00017EFC
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal event Action<TValueType> onSetValueWithoutNotify;

		// Token: 0x06000588 RID: 1416 RVA: 0x00019D34 File Offset: 0x00017F34
		public override void SetValueWithoutNotify(TValueType newValue)
		{
			TValueType tvalueType = (this.clamped ? this.GetClampedValue(newValue) : newValue);
			base.SetValueWithoutNotify(tvalueType);
			Action<TValueType> action = this.onSetValueWithoutNotify;
			if (action != null)
			{
				action(tvalueType);
			}
			this.UpdateDragElementPosition();
			this.UpdateTextFieldValue();
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x00019D80 File Offset: 0x00017F80
		// (set) Token: 0x0600058A RID: 1418 RVA: 0x00019D98 File Offset: 0x00017F98
		[CreateProperty]
		public SliderDirection direction
		{
			get
			{
				return this.m_Direction;
			}
			set
			{
				SliderDirection direction = this.m_Direction;
				this.m_Direction = value;
				bool flag = this.m_Direction == SliderDirection.Horizontal;
				if (flag)
				{
					base.RemoveFromClassList(BaseSlider<TValueType>.verticalVariantUssClassName);
					base.AddToClassList(BaseSlider<TValueType>.horizontalVariantUssClassName);
				}
				else
				{
					base.RemoveFromClassList(BaseSlider<TValueType>.horizontalVariantUssClassName);
					base.AddToClassList(BaseSlider<TValueType>.verticalVariantUssClassName);
				}
				bool flag2 = direction != this.m_Direction;
				if (flag2)
				{
					base.NotifyPropertyChanged(in BaseSlider<TValueType>.directionProperty);
				}
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600058B RID: 1419 RVA: 0x00019E14 File Offset: 0x00018014
		// (set) Token: 0x0600058C RID: 1420 RVA: 0x00019E2C File Offset: 0x0001802C
		[CreateProperty]
		public bool inverted
		{
			get
			{
				return this.m_Inverted;
			}
			set
			{
				bool flag = this.m_Inverted != value;
				if (flag)
				{
					this.m_Inverted = value;
					this.UpdateDragElementPosition();
					base.NotifyPropertyChanged(in BaseSlider<TValueType>.invertedProperty);
				}
			}
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x00019E68 File Offset: 0x00018068
		internal BaseSlider(string label, TValueType start, TValueType end, SliderDirection direction = SliderDirection.Horizontal, float pageSize = 0f)
			: base(label, null)
		{
			base.AddToClassList(BaseSlider<TValueType>.ussClassName);
			base.labelElement.AddToClassList(BaseSlider<TValueType>.labelUssClassName);
			base.visualInput.AddToClassList(BaseSlider<TValueType>.inputUssClassName);
			this.direction = direction;
			this.pageSize = pageSize;
			this.lowValue = start;
			this.highValue = end;
			base.pickingMode = PickingMode.Ignore;
			this.dragContainer = new VisualElement
			{
				name = "unity-drag-container"
			};
			this.dragContainer.AddToClassList(BaseSlider<TValueType>.dragContainerUssClassName);
			this.dragContainer.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.UpdateDragElementPosition), TrickleDown.NoTrickleDown);
			base.visualInput.Add(this.dragContainer);
			this.trackElement = new VisualElement
			{
				name = "unity-tracker",
				usageHints = UsageHints.DynamicColor
			};
			this.trackElement.AddToClassList(BaseSlider<TValueType>.trackerUssClassName);
			this.dragContainer.Add(this.trackElement);
			this.dragBorderElement = new VisualElement
			{
				name = "unity-dragger-border"
			};
			this.dragBorderElement.AddToClassList(BaseSlider<TValueType>.draggerBorderUssClassName);
			this.dragContainer.Add(this.dragBorderElement);
			this.dragElement = new VisualElement
			{
				name = "unity-dragger",
				usageHints = UsageHints.DynamicTransform
			};
			this.dragElement.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.UpdateDragElementPosition), TrickleDown.NoTrickleDown);
			this.dragElement.AddToClassList(BaseSlider<TValueType>.draggerUssClassName);
			this.dragContainer.Add(this.dragElement);
			this.clampedDragger = new ClampedDragger<TValueType>(this, new Action(this.SetSliderValueFromClick), new Action(this.SetSliderValueFromDrag));
			this.dragContainer.pickingMode = PickingMode.Position;
			this.dragContainer.AddManipulator(this.clampedDragger);
			base.RegisterCallback<KeyDownEvent>(new EventCallback<KeyDownEvent>(this.OnKeyDown), TrickleDown.NoTrickleDown);
			base.RegisterCallback<FocusInEvent>(new EventCallback<FocusInEvent>(this.OnFocusIn), TrickleDown.NoTrickleDown);
			base.RegisterCallback<FocusOutEvent>(new EventCallback<FocusOutEvent>(this.OnFocusOut), TrickleDown.NoTrickleDown);
			base.RegisterCallback<NavigationSubmitEvent>(new EventCallback<NavigationSubmitEvent>(this.OnNavigationSubmit), TrickleDown.NoTrickleDown);
			base.RegisterCallback<NavigationMoveEvent>(new EventCallback<NavigationMoveEvent>(this.OnNavigationMove), TrickleDown.NoTrickleDown);
			this.UpdateTextFieldVisibility();
			FieldMouseDragger<TValueType> fieldMouseDragger = new FieldMouseDragger<TValueType>(this);
			fieldMouseDragger.SetDragZone(base.labelElement);
			base.labelElement.AddToClassList(BaseField<TValueType>.labelDraggerVariantUssClassName);
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x0001A0FC File Offset: 0x000182FC
		protected internal static float GetClosestPowerOfTen(float positiveNumber)
		{
			bool flag = positiveNumber <= 0f;
			float num;
			if (flag)
			{
				num = 1f;
			}
			else
			{
				num = Mathf.Pow(10f, (float)Mathf.RoundToInt(Mathf.Log10(positiveNumber)));
			}
			return num;
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x0001A13C File Offset: 0x0001833C
		protected internal static float RoundToMultipleOf(float value, float roundingValue)
		{
			bool flag = roundingValue == 0f;
			float num;
			if (flag)
			{
				num = value;
			}
			else
			{
				num = Mathf.Round(value / roundingValue) * roundingValue;
			}
			return num;
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x0001A168 File Offset: 0x00018368
		private void ClampValue()
		{
			this.value = base.rawValue;
		}

		// Token: 0x06000591 RID: 1425
		internal abstract TValueType SliderLerpUnclamped(TValueType a, TValueType b, float interpolant);

		// Token: 0x06000592 RID: 1426
		internal abstract float SliderNormalizeValue(TValueType currentValue, TValueType lowerValue, TValueType higherValue);

		// Token: 0x06000593 RID: 1427
		internal abstract TValueType SliderRange();

		// Token: 0x06000594 RID: 1428
		internal abstract TValueType ParseStringToValue(string previousValue, string newValue);

		// Token: 0x06000595 RID: 1429
		internal abstract void ComputeValueFromKey(BaseSlider<TValueType>.SliderKey sliderKey, bool isShift);

		// Token: 0x06000596 RID: 1430 RVA: 0x0001A178 File Offset: 0x00018378
		private TValueType SliderLerpDirectionalUnclamped(TValueType a, TValueType b, float positionInterpolant)
		{
			float num = ((this.direction == SliderDirection.Vertical) ? (1f - positionInterpolant) : positionInterpolant);
			bool inverted = this.inverted;
			TValueType tvalueType;
			if (inverted)
			{
				tvalueType = this.SliderLerpUnclamped(b, a, num);
			}
			else
			{
				tvalueType = this.SliderLerpUnclamped(a, b, num);
			}
			return tvalueType;
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x0001A1C0 File Offset: 0x000183C0
		private void SetSliderValueFromDrag()
		{
			bool flag = this.clampedDragger.dragDirection != ClampedDragger<TValueType>.DragDirection.Free;
			if (!flag)
			{
				Vector2 delta = this.clampedDragger.delta;
				bool flag2 = this.direction == SliderDirection.Horizontal;
				if (flag2)
				{
					this.ComputeValueAndDirectionFromDrag(this.dragContainer.resolvedStyle.width, this.dragElement.resolvedStyle.width, this.m_DragElementStartPos.x + delta.x);
				}
				else
				{
					this.ComputeValueAndDirectionFromDrag(this.dragContainer.resolvedStyle.height, this.dragElement.resolvedStyle.height, this.m_DragElementStartPos.y + delta.y);
				}
			}
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x0001A278 File Offset: 0x00018478
		private void ComputeValueAndDirectionFromDrag(float sliderLength, float dragElementLength, float dragElementPos)
		{
			float num = sliderLength - dragElementLength;
			bool flag = Mathf.Abs(num) < 1E-30f;
			if (!flag)
			{
				bool clamped = this.clamped;
				float num2;
				if (clamped)
				{
					num2 = Mathf.Max(0f, Mathf.Min(dragElementPos, num)) / num;
				}
				else
				{
					num2 = dragElementPos / num;
				}
				TValueType value = this.value;
				this.value = this.SliderLerpDirectionalUnclamped(this.lowValue, this.highValue, num2);
				bool flag2 = EqualityComparer<TValueType>.Default.Equals(this.value, value);
				if (flag2)
				{
					this.UpdateDragElementPosition();
				}
			}
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x0001A304 File Offset: 0x00018504
		private void SetSliderValueFromClick()
		{
			bool flag = this.clampedDragger.dragDirection == ClampedDragger<TValueType>.DragDirection.Free;
			if (!flag)
			{
				bool flag2 = this.clampedDragger.dragDirection == ClampedDragger<TValueType>.DragDirection.None;
				if (flag2)
				{
					bool flag3 = Mathf.Approximately(this.pageSize, 0f);
					if (flag3)
					{
						bool flag4 = this.direction == SliderDirection.Horizontal;
						float num;
						float num2;
						float num5;
						float num6;
						float num7;
						if (flag4)
						{
							num = this.dragContainer.resolvedStyle.width;
							num2 = this.dragElement.resolvedStyle.width;
							float num3 = num - num2;
							float num4 = this.clampedDragger.startMousePosition.x - num2 / 2f;
							num5 = Mathf.Max(0f, Mathf.Min(num4, num3));
							num6 = this.dragElement.transform.position.y;
							num7 = num5;
						}
						else
						{
							num = this.dragContainer.resolvedStyle.height;
							num2 = this.dragElement.resolvedStyle.height;
							float num8 = num - num2;
							float num9 = this.clampedDragger.startMousePosition.y - num2 / 2f;
							num5 = this.dragElement.transform.position.x;
							num6 = Mathf.Max(0f, Mathf.Min(num9, num8));
							num7 = num6;
						}
						Vector3 vector = new Vector3(num5, num6, 0f);
						this.dragElement.transform.position = vector;
						this.dragBorderElement.transform.position = vector;
						this.m_DragElementStartPos = new Rect(num5, num6, this.dragElement.resolvedStyle.width, this.dragElement.resolvedStyle.height);
						this.clampedDragger.dragDirection = ClampedDragger<TValueType>.DragDirection.Free;
						this.ComputeValueAndDirectionFromDrag(num, num2, num7);
						return;
					}
					this.m_DragElementStartPos = new Rect(this.dragElement.transform.position.x, this.dragElement.transform.position.y, this.dragElement.resolvedStyle.width, this.dragElement.resolvedStyle.height);
				}
				bool flag5 = this.direction == SliderDirection.Horizontal;
				if (flag5)
				{
					this.ComputeValueAndDirectionFromClick(this.dragContainer.resolvedStyle.width, this.dragElement.resolvedStyle.width, this.dragElement.transform.position.x, this.clampedDragger.lastMousePosition.x);
				}
				else
				{
					this.ComputeValueAndDirectionFromClick(this.dragContainer.resolvedStyle.height, this.dragElement.resolvedStyle.height, this.dragElement.transform.position.y, this.clampedDragger.lastMousePosition.y);
				}
			}
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x0001A5D8 File Offset: 0x000187D8
		private void OnKeyDown(KeyDownEvent evt)
		{
			BaseSlider<TValueType>.SliderKey sliderKey = BaseSlider<TValueType>.SliderKey.None;
			bool flag = this.direction == SliderDirection.Horizontal;
			bool flag2 = (flag && evt.keyCode == KeyCode.Home) || (!flag && evt.keyCode == KeyCode.End);
			if (flag2)
			{
				sliderKey = (this.inverted ? BaseSlider<TValueType>.SliderKey.Highest : BaseSlider<TValueType>.SliderKey.Lowest);
			}
			else
			{
				bool flag3 = (flag && evt.keyCode == KeyCode.End) || (!flag && evt.keyCode == KeyCode.Home);
				if (flag3)
				{
					sliderKey = (this.inverted ? BaseSlider<TValueType>.SliderKey.Lowest : BaseSlider<TValueType>.SliderKey.Highest);
				}
				else
				{
					bool flag4 = (flag && evt.keyCode == KeyCode.PageUp) || (!flag && evt.keyCode == KeyCode.PageDown);
					if (flag4)
					{
						sliderKey = (this.inverted ? BaseSlider<TValueType>.SliderKey.HigherPage : BaseSlider<TValueType>.SliderKey.LowerPage);
					}
					else
					{
						bool flag5 = (flag && evt.keyCode == KeyCode.PageDown) || (!flag && evt.keyCode == KeyCode.PageUp);
						if (flag5)
						{
							sliderKey = (this.inverted ? BaseSlider<TValueType>.SliderKey.LowerPage : BaseSlider<TValueType>.SliderKey.HigherPage);
						}
					}
				}
			}
			bool flag6 = sliderKey == BaseSlider<TValueType>.SliderKey.None;
			if (!flag6)
			{
				this.ComputeValueFromKey(sliderKey, evt.shiftKey);
				evt.StopPropagation();
			}
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x0001A6FC File Offset: 0x000188FC
		private void OnNavigationMove(NavigationMoveEvent evt)
		{
			bool flag = !this.dragElement.ClassListContains(BaseSlider<TValueType>.movableUssClassName);
			if (!flag)
			{
				BaseSlider<TValueType>.SliderKey sliderKey = BaseSlider<TValueType>.SliderKey.None;
				bool flag2 = this.direction == SliderDirection.Horizontal;
				bool flag3 = evt.direction == (flag2 ? NavigationMoveEvent.Direction.Left : NavigationMoveEvent.Direction.Down);
				if (flag3)
				{
					sliderKey = (this.inverted ? BaseSlider<TValueType>.SliderKey.Higher : BaseSlider<TValueType>.SliderKey.Lower);
				}
				else
				{
					bool flag4 = evt.direction == (flag2 ? NavigationMoveEvent.Direction.Right : NavigationMoveEvent.Direction.Up);
					if (flag4)
					{
						sliderKey = (this.inverted ? BaseSlider<TValueType>.SliderKey.Lower : BaseSlider<TValueType>.SliderKey.Higher);
					}
				}
				bool flag5 = sliderKey == BaseSlider<TValueType>.SliderKey.None;
				if (!flag5)
				{
					this.ComputeValueFromKey(sliderKey, evt.shiftKey);
					evt.StopPropagation();
					FocusController focusController = this.focusController;
					if (focusController != null)
					{
						focusController.IgnoreEvent(evt);
					}
				}
			}
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x0001A7AC File Offset: 0x000189AC
		private void OnNavigationSubmit(NavigationSubmitEvent evt)
		{
			bool isEditingTextField = this.m_IsEditingTextField;
			if (!isEditingTextField)
			{
				this.dragElement.EnableInClassList(BaseSlider<TValueType>.movableUssClassName, !this.dragElement.ClassListContains(BaseSlider<TValueType>.movableUssClassName));
			}
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x0001A7EC File Offset: 0x000189EC
		internal virtual void ComputeValueAndDirectionFromClick(float sliderLength, float dragElementLength, float dragElementPos, float dragElementLastPos)
		{
			float num = sliderLength - dragElementLength;
			bool flag = Mathf.Abs(num) < 1E-30f;
			if (!flag)
			{
				bool flag2 = dragElementLastPos < dragElementPos;
				bool flag3 = dragElementLastPos > dragElementPos + dragElementLength;
				bool flag4 = (this.inverted ? flag3 : flag2);
				bool flag5 = (this.inverted ? flag2 : flag3);
				this.m_AdjustedPageSizeFromClick = (this.inverted ? (this.m_AdjustedPageSizeFromClick - this.pageSize) : (this.m_AdjustedPageSizeFromClick + this.pageSize));
				bool flag6 = flag4 && this.clampedDragger.dragDirection != ClampedDragger<TValueType>.DragDirection.LowToHigh;
				if (flag6)
				{
					this.clampedDragger.dragDirection = ClampedDragger<TValueType>.DragDirection.HighToLow;
					float num2 = Mathf.Max(0f, Mathf.Min(dragElementPos - this.m_AdjustedPageSizeFromClick, num)) / num;
					this.value = this.SliderLerpDirectionalUnclamped(this.lowValue, this.highValue, num2);
				}
				else
				{
					bool flag7 = flag5 && this.clampedDragger.dragDirection != ClampedDragger<TValueType>.DragDirection.HighToLow;
					if (flag7)
					{
						this.clampedDragger.dragDirection = ClampedDragger<TValueType>.DragDirection.LowToHigh;
						float num3 = Mathf.Max(0f, Mathf.Min(dragElementPos + this.m_AdjustedPageSizeFromClick, num)) / num;
						this.value = this.SliderLerpDirectionalUnclamped(this.lowValue, this.highValue, num3);
					}
				}
			}
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x0001A934 File Offset: 0x00018B34
		public void AdjustDragElement(float factor)
		{
			bool flag = factor < 1f;
			bool flag2 = flag;
			if (flag2)
			{
				this.dragElement.style.visibility = new StyleEnum<Visibility>(Visibility.Visible, StyleKeyword.Null);
				IStyle style = this.dragElement.style;
				bool flag3 = this.direction == SliderDirection.Horizontal;
				if (flag3)
				{
					float num = ((base.resolvedStyle.minWidth == StyleKeyword.Auto) ? 0f : base.resolvedStyle.minWidth.value);
					style.width = Mathf.Round(Mathf.Max(this.dragContainer.layout.width * factor, num));
				}
				else
				{
					float num2 = ((base.resolvedStyle.minHeight == StyleKeyword.Auto) ? 0f : base.resolvedStyle.minHeight.value);
					style.height = Mathf.Round(Mathf.Max(this.dragContainer.layout.height * factor, num2));
				}
			}
			else
			{
				this.dragElement.style.visibility = new StyleEnum<Visibility>(Visibility.Hidden, StyleKeyword.Undefined);
			}
			this.dragBorderElement.visible = this.dragElement.visible;
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x0001AA88 File Offset: 0x00018C88
		private void UpdateDragElementPosition(GeometryChangedEvent evt)
		{
			bool flag = evt.oldRect.size == evt.newRect.size;
			if (!flag)
			{
				this.UpdateDragElementPosition();
			}
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x0001AAC8 File Offset: 0x00018CC8
		internal override void OnViewDataReady()
		{
			base.OnViewDataReady();
			string fullHierarchicalViewDataKey = base.GetFullHierarchicalViewDataKey();
			base.OverwriteFromViewData(this, fullHierarchicalViewDataKey);
			this.UpdateDragElementPosition();
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x0001AAF4 File Offset: 0x00018CF4
		private bool SameValues(float a, float b, float epsilon)
		{
			return Mathf.Abs(b - a) < epsilon;
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x0001AB14 File Offset: 0x00018D14
		private void UpdateDragElementPosition()
		{
			bool flag = base.panel == null;
			if (!flag)
			{
				float num = this.SliderNormalizeValue(this.value, this.lowValue, this.highValue);
				float num2 = (this.inverted ? (1f - num) : num);
				float num3 = base.scaledPixelsPerPoint * 0.5f;
				bool flag2 = this.direction == SliderDirection.Horizontal;
				if (flag2)
				{
					float width = this.dragElement.resolvedStyle.width;
					float num4 = -this.dragElement.resolvedStyle.marginLeft - this.dragElement.resolvedStyle.marginRight;
					float num5 = this.dragContainer.layout.width - width + num4;
					float num6 = num2 * num5;
					bool flag3 = float.IsNaN(num6);
					if (flag3)
					{
						return;
					}
					float x = this.dragElement.transform.position.x;
					bool flag4 = !this.SameValues(x, num6, num3);
					if (flag4)
					{
						Vector3 vector = new Vector3(num6, 0f, 0f);
						this.dragElement.transform.position = vector;
						this.dragBorderElement.transform.position = vector;
						this.m_AdjustedPageSizeFromClick = 0f;
					}
				}
				else
				{
					float height = this.dragElement.resolvedStyle.height;
					float num7 = this.dragContainer.resolvedStyle.height - height;
					float num8 = (1f - num2) * num7;
					bool flag5 = float.IsNaN(num8);
					if (flag5)
					{
						return;
					}
					float y = this.dragElement.transform.position.y;
					bool flag6 = !this.SameValues(y, num8, num3);
					if (flag6)
					{
						Vector3 vector2 = new Vector3(0f, num8, 0f);
						this.dragElement.transform.position = vector2;
						this.dragBorderElement.transform.position = vector2;
						this.m_AdjustedPageSizeFromClick = 0f;
					}
				}
				this.UpdateFill(num);
			}
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x0001AD20 File Offset: 0x00018F20
		private void UpdateFill(float normalizedValue)
		{
			bool flag = !this.fill;
			if (!flag)
			{
				bool flag2 = this.fillElement == null;
				if (flag2)
				{
					this.fillElement = new VisualElement
					{
						name = "unity-fill",
						usageHints = UsageHints.DynamicColor
					};
					this.fillElement.AddToClassList(BaseSlider<TValueType>.fillUssClassName);
					this.trackElement.Add(this.fillElement);
				}
				float num = 1f - normalizedValue;
				Length length = Length.Percent(num * 100f);
				bool flag3 = this.direction == SliderDirection.Vertical;
				if (flag3)
				{
					this.fillElement.style.right = 0f;
					this.fillElement.style.left = 0f;
					this.fillElement.style.bottom = (this.inverted ? length : 0f);
					this.fillElement.style.top = (this.inverted ? 0f : length);
				}
				else
				{
					this.fillElement.style.top = 0f;
					this.fillElement.style.bottom = 0f;
					this.fillElement.style.left = (this.inverted ? length : 0f);
					this.fillElement.style.right = (this.inverted ? 0f : length);
				}
			}
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x0001AEDC File Offset: 0x000190DC
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

		// Token: 0x060005A5 RID: 1445 RVA: 0x00002152 File Offset: 0x00000352
		[EventInterest(EventInterestOptions.Inherit)]
		[Obsolete("ExecuteDefaultAction override has been removed because default event handling was migrated to HandleEventBubbleUp. Please use HandleEventBubbleUp.", false)]
		protected override void ExecuteDefaultAction(EventBase evt)
		{
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x0001AF20 File Offset: 0x00019120
		private void UpdateTextFieldVisibility()
		{
			bool showInputField = this.showInputField;
			if (showInputField)
			{
				bool flag = this.inputTextField == null;
				if (flag)
				{
					this.inputTextField = new TextField
					{
						name = "unity-text-field"
					};
					this.inputTextField.AddToClassList(BaseSlider<TValueType>.textFieldClassName);
					this.inputTextField.RegisterValueChangedCallback<string>(new EventCallback<ChangeEvent<string>>(this.OnTextFieldValueChange));
					this.inputTextField.RegisterCallback<FocusInEvent>(new EventCallback<FocusInEvent>(this.OnTextFieldFocusIn), TrickleDown.NoTrickleDown);
					this.inputTextField.RegisterCallback<FocusOutEvent>(new EventCallback<FocusOutEvent>(this.OnTextFieldFocusOut), TrickleDown.NoTrickleDown);
					base.visualInput.Add(this.inputTextField);
					this.UpdateTextFieldValue();
				}
			}
			else
			{
				bool flag2 = this.inputTextField != null && this.inputTextField.panel != null;
				if (flag2)
				{
					bool flag3 = this.inputTextField.panel != null;
					if (flag3)
					{
						this.inputTextField.RemoveFromHierarchy();
					}
					this.inputTextField.UnregisterValueChangedCallback<string>(new EventCallback<ChangeEvent<string>>(this.OnTextFieldValueChange));
					this.inputTextField.UnregisterCallback<FocusInEvent>(new EventCallback<FocusInEvent>(this.OnTextFieldFocusIn), TrickleDown.NoTrickleDown);
					this.inputTextField.UnregisterCallback<FocusOutEvent>(new EventCallback<FocusOutEvent>(this.OnTextFieldFocusOut), TrickleDown.NoTrickleDown);
					this.inputTextField = null;
				}
			}
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x0001B070 File Offset: 0x00019270
		private void UpdateTextFieldValue()
		{
			bool flag = this.inputTextField == null || this.m_IsEditingTextField;
			if (!flag)
			{
				this.inputTextField.SetValueWithoutNotify(string.Format(CultureInfo.InvariantCulture, "{0:g7}", this.value));
			}
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x0001B0BB File Offset: 0x000192BB
		private void OnFocusIn(FocusInEvent evt)
		{
			this.dragElement.AddToClassList(BaseSlider<TValueType>.movableUssClassName);
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x0001B0CE File Offset: 0x000192CE
		private void OnFocusOut(FocusOutEvent evt)
		{
			this.dragElement.RemoveFromClassList(BaseSlider<TValueType>.movableUssClassName);
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x0001B0E1 File Offset: 0x000192E1
		private void OnTextFieldFocusIn(FocusInEvent evt)
		{
			this.m_IsEditingTextField = true;
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x0001B0EB File Offset: 0x000192EB
		private void OnTextFieldFocusOut(FocusOutEvent evt)
		{
			this.m_IsEditingTextField = false;
			this.UpdateTextFieldValue();
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x0001B0FC File Offset: 0x000192FC
		private void OnTextFieldValueChange(ChangeEvent<string> evt)
		{
			TValueType clampedValue = this.GetClampedValue(this.ParseStringToValue(evt.previousValue, evt.newValue));
			bool flag = !EqualityComparer<TValueType>.Default.Equals(clampedValue, this.value);
			if (flag)
			{
				this.value = clampedValue;
				evt.StopPropagation();
				bool flag2 = base.elementPanel != null;
				if (flag2)
				{
					this.OnViewDataReady();
				}
			}
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x0001B160 File Offset: 0x00019360
		protected override void UpdateMixedValueContent()
		{
			bool showMixedValue = base.showMixedValue;
			if (showMixedValue)
			{
				VisualElement dragElement = this.dragElement;
				if (dragElement != null)
				{
					dragElement.RemoveFromHierarchy();
				}
				bool flag = this.inputTextField != null;
				if (flag)
				{
					this.inputTextField.showMixedValue = true;
				}
			}
			else
			{
				this.dragContainer.Add(this.dragElement);
				bool flag2 = this.inputTextField != null;
				if (flag2)
				{
					this.inputTextField.showMixedValue = false;
				}
			}
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x0001B1DC File Offset: 0x000193DC
		internal override void RegisterEditingCallbacks()
		{
			base.labelElement.RegisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(base.StartEditing), TrickleDown.TrickleDown);
			this.dragContainer.RegisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(base.StartEditing), TrickleDown.TrickleDown);
			this.dragContainer.RegisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(base.EndEditing), TrickleDown.NoTrickleDown);
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x0001B238 File Offset: 0x00019438
		internal override void UnregisterEditingCallbacks()
		{
			base.labelElement.UnregisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(base.StartEditing), TrickleDown.TrickleDown);
			this.dragContainer.UnregisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(base.StartEditing), TrickleDown.TrickleDown);
			this.dragContainer.UnregisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(base.EndEditing), TrickleDown.NoTrickleDown);
		}

		// Token: 0x040002F0 RID: 752
		internal static readonly BindingId lowValueProperty = "lowValue";

		// Token: 0x040002F1 RID: 753
		internal static readonly BindingId highValueProperty = "highValue";

		// Token: 0x040002F2 RID: 754
		internal static readonly BindingId rangeProperty = "range";

		// Token: 0x040002F3 RID: 755
		internal static readonly BindingId pageSizeProperty = "pageSize";

		// Token: 0x040002F4 RID: 756
		internal static readonly BindingId showInputFieldProperty = "showInputField";

		// Token: 0x040002F5 RID: 757
		internal static readonly BindingId directionProperty = "direction";

		// Token: 0x040002F6 RID: 758
		internal static readonly BindingId invertedProperty = "inverted";

		// Token: 0x040002F7 RID: 759
		internal static readonly BindingId fillProperty = "fill";

		// Token: 0x040002FE RID: 766
		private float m_AdjustedPageSizeFromClick = 0f;

		// Token: 0x040002FF RID: 767
		private bool m_IsEditingTextField;

		// Token: 0x04000300 RID: 768
		private bool m_Fill;

		// Token: 0x04000301 RID: 769
		[SerializeField]
		[DontCreateProperty]
		private TValueType m_LowValue;

		// Token: 0x04000302 RID: 770
		[DontCreateProperty]
		[SerializeField]
		private TValueType m_HighValue;

		// Token: 0x04000303 RID: 771
		private float m_PageSize;

		// Token: 0x04000304 RID: 772
		private bool m_ShowInputField = false;

		// Token: 0x04000307 RID: 775
		private Rect m_DragElementStartPos;

		// Token: 0x04000309 RID: 777
		private SliderDirection m_Direction;

		// Token: 0x0400030A RID: 778
		private bool m_Inverted = false;

		// Token: 0x0400030B RID: 779
		internal const float kDefaultPageSize = 0f;

		// Token: 0x0400030C RID: 780
		internal const bool kDefaultShowInputField = false;

		// Token: 0x0400030D RID: 781
		internal const bool kDefaultInverted = false;

		// Token: 0x0400030E RID: 782
		public new static readonly string ussClassName = "unity-base-slider";

		// Token: 0x0400030F RID: 783
		public new static readonly string labelUssClassName = BaseSlider<TValueType>.ussClassName + "__label";

		// Token: 0x04000310 RID: 784
		public new static readonly string inputUssClassName = BaseSlider<TValueType>.ussClassName + "__input";

		// Token: 0x04000311 RID: 785
		public static readonly string horizontalVariantUssClassName = BaseSlider<TValueType>.ussClassName + "--horizontal";

		// Token: 0x04000312 RID: 786
		public static readonly string verticalVariantUssClassName = BaseSlider<TValueType>.ussClassName + "--vertical";

		// Token: 0x04000313 RID: 787
		public static readonly string dragContainerUssClassName = BaseSlider<TValueType>.ussClassName + "__drag-container";

		// Token: 0x04000314 RID: 788
		public static readonly string trackerUssClassName = BaseSlider<TValueType>.ussClassName + "__tracker";

		// Token: 0x04000315 RID: 789
		public static readonly string draggerUssClassName = BaseSlider<TValueType>.ussClassName + "__dragger";

		// Token: 0x04000316 RID: 790
		public static readonly string draggerBorderUssClassName = BaseSlider<TValueType>.ussClassName + "__dragger-border";

		// Token: 0x04000317 RID: 791
		public static readonly string textFieldClassName = BaseSlider<TValueType>.ussClassName + "__text-field";

		// Token: 0x04000318 RID: 792
		public static readonly string fillUssClassName = BaseSlider<TValueType>.ussClassName + "__fill";

		// Token: 0x04000319 RID: 793
		public static readonly string movableUssClassName = BaseSlider<TValueType>.ussClassName + "--movable";

		// Token: 0x0400031A RID: 794
		internal const string k_FillElementName = "unity-fill";

		// Token: 0x02000091 RID: 145
		[ExcludeFromDocs]
		[Serializable]
		public new abstract class UxmlSerializedData : BaseField<TValueType>.UxmlSerializedData
		{
			// Token: 0x060005B1 RID: 1457 RVA: 0x0001B3FF File Offset: 0x000195FF
			public new static void Register()
			{
				BaseField<TValueType>.UxmlSerializedData.Register();
				UxmlDescriptionCache.RegisterType(typeof(BaseSlider<TValueType>.UxmlSerializedData), new UxmlAttributeNames[]
				{
					new UxmlAttributeNames("fill", "fill", null, Array.Empty<string>())
				});
			}

			// Token: 0x060005B2 RID: 1458 RVA: 0x0001B43C File Offset: 0x0001963C
			public override void Deserialize(object obj)
			{
				base.Deserialize(obj);
				BaseSlider<TValueType> baseSlider = (BaseSlider<TValueType>)obj;
				bool flag = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.fill_UxmlAttributeFlags);
				if (flag)
				{
					baseSlider.fill = this.fill;
				}
			}

			// Token: 0x0400031B RID: 795
			[SerializeField]
			private bool fill;

			// Token: 0x0400031C RID: 796
			[HideInInspector]
			[UxmlIgnore]
			[SerializeField]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags fill_UxmlAttributeFlags;
		}

		// Token: 0x02000092 RID: 146
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : BaseField<TValueType>.UxmlTraits
		{
		}

		// Token: 0x02000093 RID: 147
		[Obsolete("UxmlTraits<TValueUxmlAttributeType> is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public class UxmlTraits<TValueUxmlAttributeType> : BaseFieldTraits<TValueType, TValueUxmlAttributeType> where TValueUxmlAttributeType : TypedUxmlAttributeDescription<TValueType>, new()
		{
			// Token: 0x060005B5 RID: 1461 RVA: 0x0001B47E File Offset: 0x0001967E
			public UxmlTraits()
			{
				this.m_PickingMode.defaultValue = PickingMode.Ignore;
			}
		}

		// Token: 0x02000094 RID: 148
		internal enum SliderKey
		{
			// Token: 0x0400031E RID: 798
			None,
			// Token: 0x0400031F RID: 799
			Lowest,
			// Token: 0x04000320 RID: 800
			LowerPage,
			// Token: 0x04000321 RID: 801
			Lower,
			// Token: 0x04000322 RID: 802
			Higher,
			// Token: 0x04000323 RID: 803
			HigherPage,
			// Token: 0x04000324 RID: 804
			Highest
		}
	}
}
