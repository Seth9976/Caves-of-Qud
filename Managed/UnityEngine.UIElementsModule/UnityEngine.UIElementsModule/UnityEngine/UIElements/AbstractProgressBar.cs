using System;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Properties;
using UnityEngine.Internal;

namespace UnityEngine.UIElements
{
	// Token: 0x0200016E RID: 366
	public abstract class AbstractProgressBar : BindableElement, INotifyValueChanged<float>
	{
		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000BB4 RID: 2996 RVA: 0x00033467 File Offset: 0x00031667
		// (set) Token: 0x06000BB5 RID: 2997 RVA: 0x00033474 File Offset: 0x00031674
		[CreateProperty]
		public string title
		{
			get
			{
				return this.m_Title.text;
			}
			set
			{
				string title = this.title;
				this.m_Title.text = value;
				bool flag = string.CompareOrdinal(title, this.title) != 0;
				if (flag)
				{
					base.NotifyPropertyChanged(in AbstractProgressBar.titleProperty);
				}
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000BB6 RID: 2998 RVA: 0x000334B5 File Offset: 0x000316B5
		// (set) Token: 0x06000BB7 RID: 2999 RVA: 0x000334C0 File Offset: 0x000316C0
		[CreateProperty]
		public float lowValue
		{
			get
			{
				return this.m_LowValue;
			}
			set
			{
				float lowValue = this.lowValue;
				this.m_LowValue = value;
				this.SetProgress(this.m_Value);
				bool flag = !Mathf.Approximately(lowValue, this.lowValue);
				if (flag)
				{
					base.NotifyPropertyChanged(in AbstractProgressBar.lowValueProperty);
				}
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000BB8 RID: 3000 RVA: 0x00033508 File Offset: 0x00031708
		// (set) Token: 0x06000BB9 RID: 3001 RVA: 0x00033510 File Offset: 0x00031710
		[CreateProperty]
		public float highValue
		{
			get
			{
				return this.m_HighValue;
			}
			set
			{
				float highValue = this.highValue;
				this.m_HighValue = value;
				this.SetProgress(this.m_Value);
				bool flag = !Mathf.Approximately(highValue, this.highValue);
				if (flag)
				{
					base.NotifyPropertyChanged(in AbstractProgressBar.highValueProperty);
				}
			}
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x00033558 File Offset: 0x00031758
		public AbstractProgressBar()
		{
			base.AddToClassList(AbstractProgressBar.ussClassName);
			VisualElement visualElement = new VisualElement
			{
				name = AbstractProgressBar.ussClassName
			};
			this.m_Background = new VisualElement();
			this.m_Background.AddToClassList(AbstractProgressBar.backgroundUssClassName);
			visualElement.Add(this.m_Background);
			this.m_Progress = new VisualElement();
			this.m_Progress.AddToClassList(AbstractProgressBar.progressUssClassName);
			this.m_Background.Add(this.m_Progress);
			VisualElement visualElement2 = new VisualElement();
			visualElement2.AddToClassList(AbstractProgressBar.titleContainerUssClassName);
			this.m_Background.Add(visualElement2);
			this.m_Title = new Label();
			this.m_Title.AddToClassList(AbstractProgressBar.titleUssClassName);
			visualElement2.Add(this.m_Title);
			visualElement.AddToClassList(AbstractProgressBar.containerUssClassName);
			base.hierarchy.Add(visualElement);
			base.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnGeometryChanged), TrickleDown.NoTrickleDown);
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x00033665 File Offset: 0x00031865
		private void OnGeometryChanged(GeometryChangedEvent e)
		{
			this.SetProgress(this.value);
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000BBC RID: 3004 RVA: 0x00033678 File Offset: 0x00031878
		// (set) Token: 0x06000BBD RID: 3005 RVA: 0x00033690 File Offset: 0x00031890
		[CreateProperty]
		public virtual float value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				bool flag = !EqualityComparer<float>.Default.Equals(this.m_Value, value);
				if (flag)
				{
					bool flag2 = base.panel != null;
					if (flag2)
					{
						using (ChangeEvent<float> pooled = ChangeEvent<float>.GetPooled(this.m_Value, value))
						{
							pooled.elementTarget = this;
							this.SetValueWithoutNotify(value);
							this.SendEvent(pooled);
							base.NotifyPropertyChanged(in AbstractProgressBar.valueProperty);
						}
					}
					else
					{
						this.SetValueWithoutNotify(value);
					}
				}
			}
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x00033724 File Offset: 0x00031924
		public void SetValueWithoutNotify(float newValue)
		{
			this.m_Value = newValue;
			this.SetProgress(this.value);
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x0003373C File Offset: 0x0003193C
		private void SetProgress(float p)
		{
			bool flag = p < this.lowValue;
			float num;
			if (flag)
			{
				num = this.lowValue;
			}
			else
			{
				bool flag2 = p > this.highValue;
				if (flag2)
				{
					num = this.highValue;
				}
				else
				{
					num = p;
				}
			}
			num = this.CalculateOppositeProgressWidth(num);
			bool flag3 = num >= 0f;
			if (flag3)
			{
				this.m_Progress.style.right = num;
			}
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x000337B0 File Offset: 0x000319B0
		private float CalculateOppositeProgressWidth(float width)
		{
			bool flag = this.m_Background == null || this.m_Progress == null;
			float num;
			if (flag)
			{
				num = 0f;
			}
			else
			{
				bool flag2 = float.IsNaN(this.m_Background.layout.width);
				if (flag2)
				{
					num = 0f;
				}
				else
				{
					float num2 = Mathf.Floor(this.m_Background.layout.width - 2f);
					float num3 = Mathf.Max(num2 * width / this.highValue, 0f);
					float num4 = num2 - num3;
					this.m_Progress.style.width = ((Mathf.Abs(num2 - num4) < 0.1f) ? new StyleLength(0f) : new StyleLength(StyleKeyword.Auto));
					num = num4;
				}
			}
			return num;
		}

		// Token: 0x040006DA RID: 1754
		internal static readonly BindingId titleProperty = "title";

		// Token: 0x040006DB RID: 1755
		internal static readonly BindingId lowValueProperty = "lowValue";

		// Token: 0x040006DC RID: 1756
		internal static readonly BindingId highValueProperty = "highValue";

		// Token: 0x040006DD RID: 1757
		internal static readonly BindingId valueProperty = "value";

		// Token: 0x040006DE RID: 1758
		public static readonly string ussClassName = "unity-progress-bar";

		// Token: 0x040006DF RID: 1759
		public static readonly string containerUssClassName = AbstractProgressBar.ussClassName + "__container";

		// Token: 0x040006E0 RID: 1760
		public static readonly string titleUssClassName = AbstractProgressBar.ussClassName + "__title";

		// Token: 0x040006E1 RID: 1761
		public static readonly string titleContainerUssClassName = AbstractProgressBar.ussClassName + "__title-container";

		// Token: 0x040006E2 RID: 1762
		public static readonly string progressUssClassName = AbstractProgressBar.ussClassName + "__progress";

		// Token: 0x040006E3 RID: 1763
		public static readonly string backgroundUssClassName = AbstractProgressBar.ussClassName + "__background";

		// Token: 0x040006E4 RID: 1764
		private readonly VisualElement m_Background;

		// Token: 0x040006E5 RID: 1765
		private readonly VisualElement m_Progress;

		// Token: 0x040006E6 RID: 1766
		private readonly Label m_Title;

		// Token: 0x040006E7 RID: 1767
		private float m_LowValue;

		// Token: 0x040006E8 RID: 1768
		private float m_HighValue = 100f;

		// Token: 0x040006E9 RID: 1769
		private float m_Value;

		// Token: 0x040006EA RID: 1770
		private const float k_MinVisibleProgress = 0f;

		// Token: 0x040006EB RID: 1771
		private const float k_AcceptedWidthEpsilon = 0.1f;

		// Token: 0x0200016F RID: 367
		[ExcludeFromDocs]
		[Serializable]
		public new class UxmlSerializedData : BindableElement.UxmlSerializedData
		{
			// Token: 0x06000BC2 RID: 3010 RVA: 0x00033938 File Offset: 0x00031B38
			[Conditional("UNITY_EDITOR")]
			public new static void Register()
			{
				UxmlDescriptionCache.RegisterType(typeof(AbstractProgressBar.UxmlSerializedData), new UxmlAttributeNames[]
				{
					new UxmlAttributeNames("lowValue", "low-value", null, Array.Empty<string>()),
					new UxmlAttributeNames("highValue", "high-value", null, Array.Empty<string>()),
					new UxmlAttributeNames("value", "value", null, Array.Empty<string>()),
					new UxmlAttributeNames("title", "title", null, Array.Empty<string>())
				});
			}

			// Token: 0x06000BC3 RID: 3011 RVA: 0x000339CC File Offset: 0x00031BCC
			public override void Deserialize(object obj)
			{
				base.Deserialize(obj);
				AbstractProgressBar abstractProgressBar = (AbstractProgressBar)obj;
				bool flag = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.lowValue_UxmlAttributeFlags);
				if (flag)
				{
					abstractProgressBar.lowValue = this.lowValue;
				}
				bool flag2 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.highValue_UxmlAttributeFlags);
				if (flag2)
				{
					abstractProgressBar.highValue = this.highValue;
				}
				bool flag3 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.value_UxmlAttributeFlags);
				if (flag3)
				{
					abstractProgressBar.value = this.value;
				}
				bool flag4 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.title_UxmlAttributeFlags);
				if (flag4)
				{
					abstractProgressBar.title = this.title;
				}
			}

			// Token: 0x040006EC RID: 1772
			[SerializeField]
			private float lowValue;

			// Token: 0x040006ED RID: 1773
			[SerializeField]
			[HideInInspector]
			[UxmlIgnore]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags lowValue_UxmlAttributeFlags;

			// Token: 0x040006EE RID: 1774
			[SerializeField]
			private float highValue;

			// Token: 0x040006EF RID: 1775
			[HideInInspector]
			[UxmlIgnore]
			[SerializeField]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags highValue_UxmlAttributeFlags;

			// Token: 0x040006F0 RID: 1776
			[SerializeField]
			private float value;

			// Token: 0x040006F1 RID: 1777
			[UxmlIgnore]
			[HideInInspector]
			[SerializeField]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags value_UxmlAttributeFlags;

			// Token: 0x040006F2 RID: 1778
			[SerializeField]
			private string title;

			// Token: 0x040006F3 RID: 1779
			[HideInInspector]
			[UxmlIgnore]
			[SerializeField]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags title_UxmlAttributeFlags;
		}

		// Token: 0x02000170 RID: 368
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : BindableElement.UxmlTraits
		{
			// Token: 0x06000BC5 RID: 3013 RVA: 0x00033A5C File Offset: 0x00031C5C
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				AbstractProgressBar abstractProgressBar = ve as AbstractProgressBar;
				abstractProgressBar.lowValue = this.m_LowValue.GetValueFromBag(bag, cc);
				abstractProgressBar.highValue = this.m_HighValue.GetValueFromBag(bag, cc);
				abstractProgressBar.value = this.m_Value.GetValueFromBag(bag, cc);
				abstractProgressBar.title = this.m_Title.GetValueFromBag(bag, cc);
			}

			// Token: 0x040006F4 RID: 1780
			private UxmlFloatAttributeDescription m_LowValue = new UxmlFloatAttributeDescription
			{
				name = "low-value",
				defaultValue = 0f
			};

			// Token: 0x040006F5 RID: 1781
			private UxmlFloatAttributeDescription m_HighValue = new UxmlFloatAttributeDescription
			{
				name = "high-value",
				defaultValue = 100f
			};

			// Token: 0x040006F6 RID: 1782
			private UxmlFloatAttributeDescription m_Value = new UxmlFloatAttributeDescription
			{
				name = "value",
				defaultValue = 0f
			};

			// Token: 0x040006F7 RID: 1783
			private UxmlStringAttributeDescription m_Title = new UxmlStringAttributeDescription
			{
				name = "title",
				defaultValue = string.Empty
			};
		}
	}
}
