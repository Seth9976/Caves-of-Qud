using System;
using System.Diagnostics;
using UnityEngine.Internal;

namespace UnityEngine.UIElements
{
	// Token: 0x02000191 RID: 401
	public class Slider : BaseSlider<float>
	{
		// Token: 0x06000C9F RID: 3231 RVA: 0x00038BD0 File Offset: 0x00036DD0
		public Slider()
			: this(null, 0f, 10f, SliderDirection.Horizontal, 0f)
		{
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x00038BEB File Offset: 0x00036DEB
		public Slider(float start, float end, SliderDirection direction = SliderDirection.Horizontal, float pageSize = 0f)
			: this(null, start, end, direction, pageSize)
		{
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x00038BFB File Offset: 0x00036DFB
		public Slider(string label, float start = 0f, float end = 10f, SliderDirection direction = SliderDirection.Horizontal, float pageSize = 0f)
			: base(label, start, end, direction, pageSize)
		{
			base.AddToClassList(Slider.ussClassName);
			base.labelElement.AddToClassList(Slider.labelUssClassName);
			base.visualInput.AddToClassList(Slider.inputUssClassName);
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x00038C3C File Offset: 0x00036E3C
		public override void ApplyInputDeviceDelta(Vector3 delta, DeltaSpeed speed, float startValue)
		{
			double num = NumericFieldDraggerUtility.CalculateFloatDragSensitivity((double)startValue, (double)base.lowValue, (double)base.highValue);
			float num2 = NumericFieldDraggerUtility.Acceleration(speed == DeltaSpeed.Fast, speed == DeltaSpeed.Slow);
			double num3 = (double)this.value;
			num3 += (double)NumericFieldDraggerUtility.NiceDelta(delta, num2) * num;
			this.value = (float)num3;
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x00038C94 File Offset: 0x00036E94
		internal override float SliderLerpUnclamped(float a, float b, float interpolant)
		{
			float num = Mathf.LerpUnclamped(a, b, interpolant);
			float num2 = Mathf.Abs((base.highValue - base.lowValue) / (base.dragContainer.resolvedStyle.width - base.dragElement.resolvedStyle.width));
			int num3 = ((num2 == 0f) ? Mathf.Clamp((int)(5.0 - (double)Mathf.Log10(Mathf.Abs(num2))), 0, 15) : Mathf.Clamp(-Mathf.FloorToInt(Mathf.Log10(Mathf.Abs(num2))), 0, 15));
			return (float)Math.Round((double)num, num3, MidpointRounding.AwayFromZero);
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x00038D38 File Offset: 0x00036F38
		internal override float SliderNormalizeValue(float currentValue, float lowerValue, float higherValue)
		{
			float num = higherValue - lowerValue;
			bool flag = Mathf.Approximately(num, 0f);
			float num2;
			if (flag)
			{
				num2 = 1f;
			}
			else
			{
				num2 = (currentValue - lowerValue) / num;
			}
			return num2;
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x00038D6C File Offset: 0x00036F6C
		internal override float SliderRange()
		{
			return Math.Abs(base.highValue - base.lowValue);
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x00038D90 File Offset: 0x00036F90
		internal override float ParseStringToValue(string previousValue, string newValue)
		{
			float num;
			ExpressionEvaluator.Expression expression;
			bool flag = UINumericFieldsUtils.TryConvertStringToFloat(newValue, previousValue, out num, out expression);
			Action<ExpressionEvaluator.Expression> expressionEvaluated = this.expressionEvaluated;
			if (expressionEvaluated != null)
			{
				expressionEvaluated(expression);
			}
			return flag ? num : 0f;
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x00038DCC File Offset: 0x00036FCC
		internal override void ComputeValueFromKey(BaseSlider<float>.SliderKey sliderKey, bool isShift)
		{
			if (sliderKey != BaseSlider<float>.SliderKey.None)
			{
				if (sliderKey != BaseSlider<float>.SliderKey.Lowest)
				{
					if (sliderKey != BaseSlider<float>.SliderKey.Highest)
					{
						bool flag = sliderKey == BaseSlider<float>.SliderKey.LowerPage || sliderKey == BaseSlider<float>.SliderKey.HigherPage;
						float num = BaseSlider<float>.GetClosestPowerOfTen(Mathf.Abs((base.highValue - base.lowValue) * 0.01f));
						bool flag2 = flag;
						if (flag2)
						{
							num *= this.pageSize;
						}
						else if (isShift)
						{
							num *= 10f;
						}
						bool flag3 = sliderKey == BaseSlider<float>.SliderKey.Lower || sliderKey == BaseSlider<float>.SliderKey.LowerPage;
						if (flag3)
						{
							num = -num;
						}
						this.value = BaseSlider<float>.RoundToMultipleOf(this.value + num * 0.5001f, Mathf.Abs(num));
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

		// Token: 0x040007D0 RID: 2000
		internal const float kDefaultHighValue = 10f;

		// Token: 0x040007D1 RID: 2001
		public new static readonly string ussClassName = "unity-slider";

		// Token: 0x040007D2 RID: 2002
		public new static readonly string labelUssClassName = Slider.ussClassName + "__label";

		// Token: 0x040007D3 RID: 2003
		public new static readonly string inputUssClassName = Slider.ussClassName + "__input";

		// Token: 0x02000192 RID: 402
		[ExcludeFromDocs]
		[Serializable]
		public new class UxmlSerializedData : BaseSlider<float>.UxmlSerializedData
		{
			// Token: 0x06000CA9 RID: 3241 RVA: 0x00038EC8 File Offset: 0x000370C8
			[Conditional("UNITY_EDITOR")]
			public new static void Register()
			{
				BaseSlider<float>.UxmlSerializedData.Register();
				UxmlDescriptionCache.RegisterType(typeof(Slider.UxmlSerializedData), new UxmlAttributeNames[]
				{
					new UxmlAttributeNames("lowValue", "low-value", null, Array.Empty<string>()),
					new UxmlAttributeNames("highValue", "high-value", null, Array.Empty<string>()),
					new UxmlAttributeNames("pageSize", "page-size", null, Array.Empty<string>()),
					new UxmlAttributeNames("showInputField", "show-input-field", null, Array.Empty<string>()),
					new UxmlAttributeNames("direction", "direction", null, Array.Empty<string>()),
					new UxmlAttributeNames("inverted", "inverted", null, Array.Empty<string>())
				});
			}

			// Token: 0x06000CAA RID: 3242 RVA: 0x00038F9A File Offset: 0x0003719A
			public override object CreateInstance()
			{
				return new Slider();
			}

			// Token: 0x06000CAB RID: 3243 RVA: 0x00038FA4 File Offset: 0x000371A4
			public override void Deserialize(object obj)
			{
				Slider slider = (Slider)obj;
				bool flag = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.lowValue_UxmlAttributeFlags);
				if (flag)
				{
					slider.lowValue = this.lowValue;
				}
				bool flag2 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.highValue_UxmlAttributeFlags);
				if (flag2)
				{
					slider.highValue = this.highValue;
				}
				bool flag3 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.direction_UxmlAttributeFlags);
				if (flag3)
				{
					slider.direction = this.direction;
				}
				bool flag4 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.pageSize_UxmlAttributeFlags);
				if (flag4)
				{
					slider.pageSize = this.pageSize;
				}
				bool flag5 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.showInputField_UxmlAttributeFlags);
				if (flag5)
				{
					slider.showInputField = this.showInputField;
				}
				bool flag6 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.inverted_UxmlAttributeFlags);
				if (flag6)
				{
					slider.inverted = this.inverted;
				}
				base.Deserialize(obj);
			}

			// Token: 0x040007D4 RID: 2004
			[SerializeField]
			private float lowValue;

			// Token: 0x040007D5 RID: 2005
			[SerializeField]
			private float highValue;

			// Token: 0x040007D6 RID: 2006
			[SerializeField]
			private float pageSize;

			// Token: 0x040007D7 RID: 2007
			[SerializeField]
			private SliderDirection direction;

			// Token: 0x040007D8 RID: 2008
			[SerializeField]
			private bool showInputField;

			// Token: 0x040007D9 RID: 2009
			[SerializeField]
			private bool inverted;

			// Token: 0x040007DA RID: 2010
			[SerializeField]
			[HideInInspector]
			[UxmlIgnore]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags lowValue_UxmlAttributeFlags;

			// Token: 0x040007DB RID: 2011
			[SerializeField]
			[UxmlIgnore]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags highValue_UxmlAttributeFlags;

			// Token: 0x040007DC RID: 2012
			[HideInInspector]
			[SerializeField]
			[UxmlIgnore]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags pageSize_UxmlAttributeFlags;

			// Token: 0x040007DD RID: 2013
			[SerializeField]
			[UxmlIgnore]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags showInputField_UxmlAttributeFlags;

			// Token: 0x040007DE RID: 2014
			[UxmlIgnore]
			[HideInInspector]
			[SerializeField]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags direction_UxmlAttributeFlags;

			// Token: 0x040007DF RID: 2015
			[UxmlIgnore]
			[HideInInspector]
			[SerializeField]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags inverted_UxmlAttributeFlags;
		}

		// Token: 0x02000193 RID: 403
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<Slider, Slider.UxmlTraits>
		{
		}

		// Token: 0x02000194 RID: 404
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : BaseSlider<float>.UxmlTraits<UxmlFloatAttributeDescription>
		{
			// Token: 0x06000CAE RID: 3246 RVA: 0x00039084 File Offset: 0x00037284
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				Slider slider = (Slider)ve;
				slider.lowValue = this.m_LowValue.GetValueFromBag(bag, cc);
				slider.highValue = this.m_HighValue.GetValueFromBag(bag, cc);
				slider.direction = this.m_Direction.GetValueFromBag(bag, cc);
				slider.pageSize = this.m_PageSize.GetValueFromBag(bag, cc);
				slider.showInputField = this.m_ShowInputField.GetValueFromBag(bag, cc);
				slider.inverted = this.m_Inverted.GetValueFromBag(bag, cc);
				base.Init(ve, bag, cc);
			}

			// Token: 0x040007E0 RID: 2016
			private UxmlFloatAttributeDescription m_LowValue = new UxmlFloatAttributeDescription
			{
				name = "low-value"
			};

			// Token: 0x040007E1 RID: 2017
			private UxmlFloatAttributeDescription m_HighValue = new UxmlFloatAttributeDescription
			{
				name = "high-value",
				defaultValue = 10f
			};

			// Token: 0x040007E2 RID: 2018
			private UxmlFloatAttributeDescription m_PageSize = new UxmlFloatAttributeDescription
			{
				name = "page-size",
				defaultValue = 0f
			};

			// Token: 0x040007E3 RID: 2019
			private UxmlBoolAttributeDescription m_ShowInputField = new UxmlBoolAttributeDescription
			{
				name = "show-input-field",
				defaultValue = false
			};

			// Token: 0x040007E4 RID: 2020
			private UxmlEnumAttributeDescription<SliderDirection> m_Direction = new UxmlEnumAttributeDescription<SliderDirection>
			{
				name = "direction",
				defaultValue = SliderDirection.Horizontal
			};

			// Token: 0x040007E5 RID: 2021
			private UxmlBoolAttributeDescription m_Inverted = new UxmlBoolAttributeDescription
			{
				name = "inverted",
				defaultValue = false
			};
		}
	}
}
