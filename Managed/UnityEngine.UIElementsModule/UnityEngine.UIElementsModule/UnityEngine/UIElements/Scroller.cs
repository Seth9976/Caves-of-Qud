using System;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Properties;
using UnityEngine.Internal;

namespace UnityEngine.UIElements
{
	// Token: 0x02000181 RID: 385
	public class Scroller : VisualElement
	{
		// Token: 0x1400003C RID: 60
		// (add) Token: 0x06000C13 RID: 3091 RVA: 0x00034BA0 File Offset: 0x00032DA0
		// (remove) Token: 0x06000C14 RID: 3092 RVA: 0x00034BD8 File Offset: 0x00032DD8
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<float> valueChanged;

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000C15 RID: 3093 RVA: 0x00034C0D File Offset: 0x00032E0D
		public Slider slider { get; }

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000C16 RID: 3094 RVA: 0x00034C15 File Offset: 0x00032E15
		public RepeatButton lowButton { get; }

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000C17 RID: 3095 RVA: 0x00034C1D File Offset: 0x00032E1D
		public RepeatButton highButton { get; }

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000C18 RID: 3096 RVA: 0x00034C28 File Offset: 0x00032E28
		// (set) Token: 0x06000C19 RID: 3097 RVA: 0x00034C48 File Offset: 0x00032E48
		[CreateProperty]
		public float value
		{
			get
			{
				return this.slider.value;
			}
			set
			{
				float value2 = this.slider.value;
				this.slider.value = value;
				bool flag = !Mathf.Approximately(value2, this.slider.value);
				if (flag)
				{
					base.NotifyPropertyChanged(in Scroller.valueProperty);
				}
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000C1A RID: 3098 RVA: 0x00034C94 File Offset: 0x00032E94
		// (set) Token: 0x06000C1B RID: 3099 RVA: 0x00034CB4 File Offset: 0x00032EB4
		[CreateProperty]
		public float lowValue
		{
			get
			{
				return this.slider.lowValue;
			}
			set
			{
				float lowValue = this.slider.lowValue;
				this.slider.lowValue = value;
				bool flag = !Mathf.Approximately(lowValue, this.slider.lowValue);
				if (flag)
				{
					base.NotifyPropertyChanged(in Scroller.lowValueProperty);
				}
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000C1C RID: 3100 RVA: 0x00034D00 File Offset: 0x00032F00
		// (set) Token: 0x06000C1D RID: 3101 RVA: 0x00034D20 File Offset: 0x00032F20
		[CreateProperty]
		public float highValue
		{
			get
			{
				return this.slider.highValue;
			}
			set
			{
				float highValue = this.slider.highValue;
				this.slider.highValue = value;
				bool flag = !Mathf.Approximately(highValue, this.slider.highValue);
				if (flag)
				{
					base.NotifyPropertyChanged(in Scroller.highValueProperty);
				}
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000C1E RID: 3102 RVA: 0x00034D6C File Offset: 0x00032F6C
		// (set) Token: 0x06000C1F RID: 3103 RVA: 0x00034D90 File Offset: 0x00032F90
		[CreateProperty]
		public SliderDirection direction
		{
			get
			{
				return (base.resolvedStyle.flexDirection == FlexDirection.Row) ? SliderDirection.Horizontal : SliderDirection.Vertical;
			}
			set
			{
				SliderDirection direction = this.slider.direction;
				this.slider.direction = value;
				this.slider.inverted = value == SliderDirection.Vertical;
				bool flag = value == SliderDirection.Horizontal;
				if (flag)
				{
					base.style.flexDirection = FlexDirection.Row;
					base.AddToClassList(Scroller.horizontalVariantUssClassName);
					base.RemoveFromClassList(Scroller.verticalVariantUssClassName);
				}
				else
				{
					base.style.flexDirection = FlexDirection.Column;
					base.AddToClassList(Scroller.verticalVariantUssClassName);
					base.RemoveFromClassList(Scroller.horizontalVariantUssClassName);
				}
				bool flag2 = direction != this.slider.direction;
				if (flag2)
				{
					base.NotifyPropertyChanged(in Scroller.directionProperty);
				}
			}
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x00034E4A File Offset: 0x0003304A
		public Scroller()
			: this(0f, 0f, null, SliderDirection.Vertical)
		{
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x00034E60 File Offset: 0x00033060
		public Scroller(float lowValue, float highValue, Action<float> valueChanged, SliderDirection direction = SliderDirection.Vertical)
		{
			base.AddToClassList(Scroller.ussClassName);
			this.slider = new Scroller.ScrollerSlider(lowValue, highValue, direction, 20f)
			{
				name = "unity-slider",
				viewDataKey = "Slider"
			};
			this.slider.AddToClassList(Scroller.sliderUssClassName);
			this.slider.RegisterValueChangedCallback<float>(new EventCallback<ChangeEvent<float>>(this.OnSliderValueChange));
			this.lowButton = new RepeatButton(new Action(this.ScrollPageUp), 250L, 30L)
			{
				name = "unity-low-button"
			};
			this.lowButton.AddToClassList(Scroller.lowButtonUssClassName);
			base.Add(this.lowButton);
			this.highButton = new RepeatButton(new Action(this.ScrollPageDown), 250L, 30L)
			{
				name = "unity-high-button"
			};
			this.highButton.AddToClassList(Scroller.highButtonUssClassName);
			base.Add(this.highButton);
			base.Add(this.slider);
			this.direction = direction;
			this.valueChanged = valueChanged;
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x00034F87 File Offset: 0x00033187
		public void Adjust(float factor)
		{
			base.SetEnabled(factor < 1f);
			this.slider.AdjustDragElement(factor);
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x00034FA6 File Offset: 0x000331A6
		private void OnSliderValueChange(ChangeEvent<float> evt)
		{
			this.value = evt.newValue;
			Action<float> action = this.valueChanged;
			if (action != null)
			{
				action(this.slider.value);
			}
			base.IncrementVersion(VersionChangeType.Repaint);
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x00034FDF File Offset: 0x000331DF
		public void ScrollPageUp()
		{
			this.ScrollPageUp(1f);
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x00034FEE File Offset: 0x000331EE
		public void ScrollPageDown()
		{
			this.ScrollPageDown(1f);
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x00035000 File Offset: 0x00033200
		public void ScrollPageUp(float factor)
		{
			this.value -= factor * (this.slider.pageSize * ((this.slider.lowValue < this.slider.highValue) ? 1f : (-1f)));
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x00035050 File Offset: 0x00033250
		public void ScrollPageDown(float factor)
		{
			this.value += factor * (this.slider.pageSize * ((this.slider.lowValue < this.slider.highValue) ? 1f : (-1f)));
		}

		// Token: 0x04000723 RID: 1827
		internal static readonly BindingId valueProperty = "value";

		// Token: 0x04000724 RID: 1828
		internal static readonly BindingId lowValueProperty = "lowValue";

		// Token: 0x04000725 RID: 1829
		internal static readonly BindingId highValueProperty = "highValue";

		// Token: 0x04000726 RID: 1830
		internal static readonly BindingId directionProperty = "direction";

		// Token: 0x0400072B RID: 1835
		internal const float kDefaultPageSize = 20f;

		// Token: 0x0400072C RID: 1836
		public static readonly string ussClassName = "unity-scroller";

		// Token: 0x0400072D RID: 1837
		public static readonly string horizontalVariantUssClassName = Scroller.ussClassName + "--horizontal";

		// Token: 0x0400072E RID: 1838
		public static readonly string verticalVariantUssClassName = Scroller.ussClassName + "--vertical";

		// Token: 0x0400072F RID: 1839
		public static readonly string sliderUssClassName = Scroller.ussClassName + "__slider";

		// Token: 0x04000730 RID: 1840
		public static readonly string lowButtonUssClassName = Scroller.ussClassName + "__low-button";

		// Token: 0x04000731 RID: 1841
		public static readonly string highButtonUssClassName = Scroller.ussClassName + "__high-button";

		// Token: 0x02000182 RID: 386
		private class ScrollerSlider : Slider
		{
			// Token: 0x06000C29 RID: 3113 RVA: 0x00035157 File Offset: 0x00033357
			public ScrollerSlider(float start, float end, SliderDirection direction, float pageSize)
				: base(start, end, direction, pageSize)
			{
			}

			// Token: 0x06000C2A RID: 3114 RVA: 0x00035168 File Offset: 0x00033368
			internal override float SliderNormalizeValue(float currentValue, float lowerValue, float higherValue)
			{
				return Mathf.Clamp(base.SliderNormalizeValue(currentValue, lowerValue, higherValue), 0f, 1f);
			}
		}

		// Token: 0x02000183 RID: 387
		[ExcludeFromDocs]
		[Serializable]
		public new class UxmlSerializedData : VisualElement.UxmlSerializedData
		{
			// Token: 0x06000C2B RID: 3115 RVA: 0x00035194 File Offset: 0x00033394
			[Conditional("UNITY_EDITOR")]
			public new static void Register()
			{
				UxmlDescriptionCache.RegisterType(typeof(Scroller.UxmlSerializedData), new UxmlAttributeNames[]
				{
					new UxmlAttributeNames("lowValue", "low-value", null, new string[] { "lowValue" }),
					new UxmlAttributeNames("highValue", "high-value", null, new string[] { "highValue" }),
					new UxmlAttributeNames("direction", "direction", null, Array.Empty<string>()),
					new UxmlAttributeNames("value", "value", null, Array.Empty<string>())
				});
			}

			// Token: 0x06000C2C RID: 3116 RVA: 0x0003523A File Offset: 0x0003343A
			public override object CreateInstance()
			{
				return new Scroller();
			}

			// Token: 0x06000C2D RID: 3117 RVA: 0x00035244 File Offset: 0x00033444
			public override void Deserialize(object obj)
			{
				base.Deserialize(obj);
				Scroller scroller = (Scroller)obj;
				bool flag = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.lowValue_UxmlAttributeFlags);
				if (flag)
				{
					scroller.slider.lowValue = this.lowValue;
				}
				bool flag2 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.highValue_UxmlAttributeFlags);
				if (flag2)
				{
					scroller.slider.highValue = this.highValue;
				}
				bool flag3 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.direction_UxmlAttributeFlags);
				if (flag3)
				{
					scroller.direction = this.direction;
				}
				bool flag4 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.value_UxmlAttributeFlags);
				if (flag4)
				{
					scroller.value = this.value;
				}
			}

			// Token: 0x04000732 RID: 1842
			[UxmlAttribute("low-value", new string[] { "lowValue" })]
			[SerializeField]
			private float lowValue;

			// Token: 0x04000733 RID: 1843
			[SerializeField]
			[UxmlIgnore]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags lowValue_UxmlAttributeFlags;

			// Token: 0x04000734 RID: 1844
			[UxmlAttribute("high-value", new string[] { "highValue" })]
			[SerializeField]
			private float highValue;

			// Token: 0x04000735 RID: 1845
			[SerializeField]
			[HideInInspector]
			[UxmlIgnore]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags highValue_UxmlAttributeFlags;

			// Token: 0x04000736 RID: 1846
			[SerializeField]
			private SliderDirection direction;

			// Token: 0x04000737 RID: 1847
			[HideInInspector]
			[UxmlIgnore]
			[SerializeField]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags direction_UxmlAttributeFlags;

			// Token: 0x04000738 RID: 1848
			[SerializeField]
			private float value;

			// Token: 0x04000739 RID: 1849
			[SerializeField]
			[UxmlIgnore]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags value_UxmlAttributeFlags;
		}

		// Token: 0x02000184 RID: 388
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<Scroller, Scroller.UxmlTraits>
		{
		}

		// Token: 0x02000185 RID: 389
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : VisualElement.UxmlTraits
		{
			// Token: 0x17000204 RID: 516
			// (get) Token: 0x06000C30 RID: 3120 RVA: 0x000352E8 File Offset: 0x000334E8
			public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
			{
				get
				{
					yield break;
				}
			}

			// Token: 0x06000C31 RID: 3121 RVA: 0x00035308 File Offset: 0x00033508
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				Scroller scroller = (Scroller)ve;
				scroller.slider.lowValue = this.m_LowValue.GetValueFromBag(bag, cc);
				scroller.slider.highValue = this.m_HighValue.GetValueFromBag(bag, cc);
				scroller.direction = this.m_Direction.GetValueFromBag(bag, cc);
				scroller.value = this.m_Value.GetValueFromBag(bag, cc);
			}

			// Token: 0x0400073A RID: 1850
			private UxmlFloatAttributeDescription m_LowValue = new UxmlFloatAttributeDescription
			{
				name = "low-value",
				obsoleteNames = new string[] { "lowValue" }
			};

			// Token: 0x0400073B RID: 1851
			private UxmlFloatAttributeDescription m_HighValue = new UxmlFloatAttributeDescription
			{
				name = "high-value",
				obsoleteNames = new string[] { "highValue" }
			};

			// Token: 0x0400073C RID: 1852
			private UxmlEnumAttributeDescription<SliderDirection> m_Direction = new UxmlEnumAttributeDescription<SliderDirection>
			{
				name = "direction",
				defaultValue = SliderDirection.Vertical
			};

			// Token: 0x0400073D RID: 1853
			private UxmlFloatAttributeDescription m_Value = new UxmlFloatAttributeDescription
			{
				name = "value"
			};
		}
	}
}
