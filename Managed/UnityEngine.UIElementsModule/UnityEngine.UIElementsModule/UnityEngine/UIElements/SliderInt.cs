using System;
using System.Diagnostics;
using UnityEngine.Internal;

namespace UnityEngine.UIElements
{
	// Token: 0x02000195 RID: 405
	public class SliderInt : BaseSlider<int>
	{
		// Token: 0x06000CB0 RID: 3248 RVA: 0x000391EA File Offset: 0x000373EA
		public SliderInt()
			: this(null, 0, 10, SliderDirection.Horizontal, 0f)
		{
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x000391FE File Offset: 0x000373FE
		public SliderInt(int start, int end, SliderDirection direction = SliderDirection.Horizontal, float pageSize = 0f)
			: this(null, start, end, direction, pageSize)
		{
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x0003920E File Offset: 0x0003740E
		public SliderInt(string label, int start = 0, int end = 10, SliderDirection direction = SliderDirection.Horizontal, float pageSize = 0f)
			: base(label, start, end, direction, pageSize)
		{
			base.AddToClassList(SliderInt.ussClassName);
			base.labelElement.AddToClassList(SliderInt.labelUssClassName);
			base.visualInput.AddToClassList(SliderInt.inputUssClassName);
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000CB3 RID: 3251 RVA: 0x00039250 File Offset: 0x00037450
		// (set) Token: 0x06000CB4 RID: 3252 RVA: 0x00039268 File Offset: 0x00037468
		public override float pageSize
		{
			get
			{
				return base.pageSize;
			}
			set
			{
				base.pageSize = (float)Mathf.RoundToInt(value);
			}
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x0003927C File Offset: 0x0003747C
		public override void ApplyInputDeviceDelta(Vector3 delta, DeltaSpeed speed, int startValue)
		{
			double num = (double)NumericFieldDraggerUtility.CalculateIntDragSensitivity((long)startValue, (long)base.lowValue, (long)base.highValue);
			float num2 = NumericFieldDraggerUtility.Acceleration(speed == DeltaSpeed.Fast, speed == DeltaSpeed.Slow);
			long num3 = (long)this.value;
			num3 += (long)Math.Round((double)NumericFieldDraggerUtility.NiceDelta(delta, num2) * num);
			this.value = (int)num3;
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x000392D8 File Offset: 0x000374D8
		internal override int SliderLerpUnclamped(int a, int b, float interpolant)
		{
			return Mathf.RoundToInt(Mathf.LerpUnclamped((float)a, (float)b, interpolant));
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x000392FC File Offset: 0x000374FC
		internal override float SliderNormalizeValue(int currentValue, int lowerValue, int higherValue)
		{
			bool flag = higherValue - lowerValue == 0;
			float num;
			if (flag)
			{
				num = 1f;
			}
			else
			{
				num = ((float)currentValue - (float)lowerValue) / ((float)higherValue - (float)lowerValue);
			}
			return num;
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x0003932C File Offset: 0x0003752C
		internal override int SliderRange()
		{
			return Math.Abs(base.highValue - base.lowValue);
		}

		// Token: 0x06000CB9 RID: 3257 RVA: 0x00039350 File Offset: 0x00037550
		internal override int ParseStringToValue(string previousValue, string newValue)
		{
			int num;
			ExpressionEvaluator.Expression expression;
			bool flag = UINumericFieldsUtils.TryConvertStringToInt(newValue, previousValue, out num, out expression);
			Action<ExpressionEvaluator.Expression> expressionEvaluated = this.expressionEvaluated;
			if (expressionEvaluated != null)
			{
				expressionEvaluated(expression);
			}
			return flag ? num : 0;
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x00039388 File Offset: 0x00037588
		internal override void ComputeValueAndDirectionFromClick(float sliderLength, float dragElementLength, float dragElementPos, float dragElementLastPos)
		{
			bool flag = Mathf.Approximately(this.pageSize, 0f);
			if (flag)
			{
				base.ComputeValueAndDirectionFromClick(sliderLength, dragElementLength, dragElementPos, dragElementLastPos);
			}
			else
			{
				float num = sliderLength - dragElementLength;
				bool flag2 = Mathf.Abs(num) < 1E-30f;
				if (!flag2)
				{
					int num2 = (int)this.pageSize;
					bool flag3 = (base.lowValue > base.highValue && !base.inverted) || (base.lowValue < base.highValue && base.inverted) || (base.direction == SliderDirection.Vertical && !base.inverted);
					if (flag3)
					{
						num2 = -num2;
					}
					bool flag4 = dragElementLastPos < dragElementPos;
					bool flag5 = dragElementLastPos > dragElementPos + dragElementLength;
					bool flag6 = (base.inverted ? flag5 : flag4);
					bool flag7 = (base.inverted ? flag4 : flag5);
					bool flag8 = flag6 && base.clampedDragger.dragDirection != ClampedDragger<int>.DragDirection.LowToHigh;
					if (flag8)
					{
						base.clampedDragger.dragDirection = ClampedDragger<int>.DragDirection.HighToLow;
						this.value -= num2;
					}
					else
					{
						bool flag9 = flag7 && base.clampedDragger.dragDirection != ClampedDragger<int>.DragDirection.HighToLow;
						if (flag9)
						{
							base.clampedDragger.dragDirection = ClampedDragger<int>.DragDirection.LowToHigh;
							this.value += num2;
						}
					}
				}
			}
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x000394DC File Offset: 0x000376DC
		internal override void ComputeValueFromKey(BaseSlider<int>.SliderKey sliderKey, bool isShift)
		{
			if (sliderKey != BaseSlider<int>.SliderKey.None)
			{
				if (sliderKey != BaseSlider<int>.SliderKey.Lowest)
				{
					if (sliderKey != BaseSlider<int>.SliderKey.Highest)
					{
						bool flag = sliderKey == BaseSlider<int>.SliderKey.LowerPage || sliderKey == BaseSlider<int>.SliderKey.HigherPage;
						float num = BaseSlider<int>.GetClosestPowerOfTen(Mathf.Abs((float)(base.highValue - base.lowValue) * 0.01f));
						bool flag2 = num < 1f;
						if (flag2)
						{
							num = 1f;
						}
						bool flag3 = flag;
						if (flag3)
						{
							num *= this.pageSize;
						}
						else if (isShift)
						{
							num *= 10f;
						}
						bool flag4 = sliderKey == BaseSlider<int>.SliderKey.Lower || sliderKey == BaseSlider<int>.SliderKey.LowerPage;
						if (flag4)
						{
							num = -num;
						}
						this.value = Mathf.RoundToInt(BaseSlider<int>.RoundToMultipleOf((float)this.value + num * 0.5001f, Mathf.Abs(num)));
					}
					else
					{
						this.value = base.highValue;
					}
				}
				else
				{
					this.value = base.lowValue;
				}
			}
		}

		// Token: 0x040007E6 RID: 2022
		internal const int kDefaultHighValue = 10;

		// Token: 0x040007E7 RID: 2023
		public new static readonly string ussClassName = "unity-slider-int";

		// Token: 0x040007E8 RID: 2024
		public new static readonly string labelUssClassName = SliderInt.ussClassName + "__label";

		// Token: 0x040007E9 RID: 2025
		public new static readonly string inputUssClassName = SliderInt.ussClassName + "__input";

		// Token: 0x02000196 RID: 406
		[ExcludeFromDocs]
		[Serializable]
		public new class UxmlSerializedData : BaseSlider<int>.UxmlSerializedData
		{
			// Token: 0x06000CBD RID: 3261 RVA: 0x000395F8 File Offset: 0x000377F8
			[Conditional("UNITY_EDITOR")]
			public new static void Register()
			{
				BaseSlider<int>.UxmlSerializedData.Register();
				UxmlDescriptionCache.RegisterType(typeof(SliderInt.UxmlSerializedData), new UxmlAttributeNames[]
				{
					new UxmlAttributeNames("lowValue", "low-value", null, Array.Empty<string>()),
					new UxmlAttributeNames("highValue", "high-value", null, Array.Empty<string>()),
					new UxmlAttributeNames("pageSize", "page-size", null, Array.Empty<string>()),
					new UxmlAttributeNames("showInputField", "show-input-field", null, Array.Empty<string>()),
					new UxmlAttributeNames("direction", "direction", null, Array.Empty<string>()),
					new UxmlAttributeNames("inverted", "inverted", null, Array.Empty<string>())
				});
			}

			// Token: 0x06000CBE RID: 3262 RVA: 0x000396CA File Offset: 0x000378CA
			public override object CreateInstance()
			{
				return new SliderInt();
			}

			// Token: 0x06000CBF RID: 3263 RVA: 0x000396D4 File Offset: 0x000378D4
			public override void Deserialize(object obj)
			{
				SliderInt sliderInt = (SliderInt)obj;
				bool flag = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.lowValue_UxmlAttributeFlags);
				if (flag)
				{
					sliderInt.lowValue = this.lowValue;
				}
				bool flag2 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.highValue_UxmlAttributeFlags);
				if (flag2)
				{
					sliderInt.highValue = this.highValue;
				}
				bool flag3 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.direction_UxmlAttributeFlags);
				if (flag3)
				{
					sliderInt.direction = this.direction;
				}
				bool flag4 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.pageSize_UxmlAttributeFlags);
				if (flag4)
				{
					sliderInt.pageSize = this.pageSize;
				}
				bool flag5 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.showInputField_UxmlAttributeFlags);
				if (flag5)
				{
					sliderInt.showInputField = this.showInputField;
				}
				bool flag6 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.inverted_UxmlAttributeFlags);
				if (flag6)
				{
					sliderInt.inverted = this.inverted;
				}
				base.Deserialize(obj);
			}

			// Token: 0x040007EA RID: 2026
			[SerializeField]
			private int lowValue;

			// Token: 0x040007EB RID: 2027
			[SerializeField]
			private int highValue;

			// Token: 0x040007EC RID: 2028
			[SerializeField]
			private float pageSize;

			// Token: 0x040007ED RID: 2029
			[SerializeField]
			private SliderDirection direction;

			// Token: 0x040007EE RID: 2030
			[SerializeField]
			private bool showInputField;

			// Token: 0x040007EF RID: 2031
			[SerializeField]
			private bool inverted;

			// Token: 0x040007F0 RID: 2032
			[SerializeField]
			[UxmlIgnore]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags lowValue_UxmlAttributeFlags;

			// Token: 0x040007F1 RID: 2033
			[HideInInspector]
			[UxmlIgnore]
			[SerializeField]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags highValue_UxmlAttributeFlags;

			// Token: 0x040007F2 RID: 2034
			[SerializeField]
			[HideInInspector]
			[UxmlIgnore]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags pageSize_UxmlAttributeFlags;

			// Token: 0x040007F3 RID: 2035
			[HideInInspector]
			[UxmlIgnore]
			[SerializeField]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags showInputField_UxmlAttributeFlags;

			// Token: 0x040007F4 RID: 2036
			[UxmlIgnore]
			[SerializeField]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags direction_UxmlAttributeFlags;

			// Token: 0x040007F5 RID: 2037
			[HideInInspector]
			[SerializeField]
			[UxmlIgnore]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags inverted_UxmlAttributeFlags;
		}

		// Token: 0x02000197 RID: 407
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<SliderInt, SliderInt.UxmlTraits>
		{
		}

		// Token: 0x02000198 RID: 408
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : BaseSlider<int>.UxmlTraits<UxmlIntAttributeDescription>
		{
			// Token: 0x06000CC2 RID: 3266 RVA: 0x000397B4 File Offset: 0x000379B4
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				SliderInt sliderInt = (SliderInt)ve;
				sliderInt.lowValue = this.m_LowValue.GetValueFromBag(bag, cc);
				sliderInt.highValue = this.m_HighValue.GetValueFromBag(bag, cc);
				sliderInt.direction = this.m_Direction.GetValueFromBag(bag, cc);
				sliderInt.pageSize = (float)this.m_PageSize.GetValueFromBag(bag, cc);
				sliderInt.showInputField = this.m_ShowInputField.GetValueFromBag(bag, cc);
				sliderInt.inverted = this.m_Inverted.GetValueFromBag(bag, cc);
				base.Init(ve, bag, cc);
			}

			// Token: 0x040007F6 RID: 2038
			private UxmlIntAttributeDescription m_LowValue = new UxmlIntAttributeDescription
			{
				name = "low-value"
			};

			// Token: 0x040007F7 RID: 2039
			private UxmlIntAttributeDescription m_HighValue = new UxmlIntAttributeDescription
			{
				name = "high-value",
				defaultValue = 10
			};

			// Token: 0x040007F8 RID: 2040
			private UxmlIntAttributeDescription m_PageSize = new UxmlIntAttributeDescription
			{
				name = "page-size",
				defaultValue = 0
			};

			// Token: 0x040007F9 RID: 2041
			private UxmlBoolAttributeDescription m_ShowInputField = new UxmlBoolAttributeDescription
			{
				name = "show-input-field",
				defaultValue = false
			};

			// Token: 0x040007FA RID: 2042
			private UxmlEnumAttributeDescription<SliderDirection> m_Direction = new UxmlEnumAttributeDescription<SliderDirection>
			{
				name = "direction",
				defaultValue = SliderDirection.Horizontal
			};

			// Token: 0x040007FB RID: 2043
			private UxmlBoolAttributeDescription m_Inverted = new UxmlBoolAttributeDescription
			{
				name = "inverted",
				defaultValue = false
			};
		}
	}
}
