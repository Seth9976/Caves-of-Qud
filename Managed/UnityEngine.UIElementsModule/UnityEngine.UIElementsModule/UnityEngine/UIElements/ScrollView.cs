using System;
using System.Diagnostics;
using Unity.Properties;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements
{
	// Token: 0x02000189 RID: 393
	public class ScrollView : VisualElement
	{
		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000C3B RID: 3131 RVA: 0x000354C4 File Offset: 0x000336C4
		// (set) Token: 0x06000C3C RID: 3132 RVA: 0x000354DC File Offset: 0x000336DC
		[CreateProperty]
		public ScrollerVisibility horizontalScrollerVisibility
		{
			get
			{
				return this.m_HorizontalScrollerVisibility;
			}
			set
			{
				ScrollerVisibility horizontalScrollerVisibility = this.m_HorizontalScrollerVisibility;
				this.m_HorizontalScrollerVisibility = value;
				this.UpdateScrollers(this.needsHorizontal, this.needsVertical);
				bool flag = horizontalScrollerVisibility != this.m_HorizontalScrollerVisibility;
				if (flag)
				{
					base.NotifyPropertyChanged(in ScrollView.horizontalScrollerVisibilityProperty);
				}
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000C3D RID: 3133 RVA: 0x00035528 File Offset: 0x00033728
		// (set) Token: 0x06000C3E RID: 3134 RVA: 0x00035540 File Offset: 0x00033740
		[CreateProperty]
		public ScrollerVisibility verticalScrollerVisibility
		{
			get
			{
				return this.m_VerticalScrollerVisibility;
			}
			set
			{
				ScrollerVisibility verticalScrollerVisibility = this.m_VerticalScrollerVisibility;
				this.m_VerticalScrollerVisibility = value;
				this.UpdateScrollers(this.needsHorizontal, this.needsVertical);
				bool flag = verticalScrollerVisibility != this.m_VerticalScrollerVisibility;
				if (flag)
				{
					base.NotifyPropertyChanged(in ScrollView.verticalScrollerVisibilityProperty);
				}
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000C3F RID: 3135 RVA: 0x0003558C File Offset: 0x0003378C
		// (set) Token: 0x06000C40 RID: 3136 RVA: 0x000355A4 File Offset: 0x000337A4
		[CreateProperty]
		public long elasticAnimationIntervalMs
		{
			get
			{
				return this.m_ElasticAnimationIntervalMs;
			}
			set
			{
				long elasticAnimationIntervalMs = this.m_ElasticAnimationIntervalMs;
				this.m_ElasticAnimationIntervalMs = value;
				bool flag = elasticAnimationIntervalMs != this.m_ElasticAnimationIntervalMs;
				if (flag)
				{
					base.NotifyPropertyChanged(in ScrollView.elasticAnimationIntervalMsProperty);
					this.m_PostPointerUpAnimation = base.schedule.Execute(new Action(this.PostPointerUpAnimation)).Every(this.m_ElasticAnimationIntervalMs);
				}
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000C41 RID: 3137 RVA: 0x00035606 File Offset: 0x00033806
		// (set) Token: 0x06000C42 RID: 3138 RVA: 0x00035611 File Offset: 0x00033811
		[Obsolete("showHorizontal is obsolete. Use horizontalScrollerVisibility instead")]
		public bool showHorizontal
		{
			get
			{
				return this.horizontalScrollerVisibility == ScrollerVisibility.AlwaysVisible;
			}
			set
			{
				this.m_HorizontalScrollerVisibility = (value ? ScrollerVisibility.AlwaysVisible : ScrollerVisibility.Auto);
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000C43 RID: 3139 RVA: 0x00035620 File Offset: 0x00033820
		// (set) Token: 0x06000C44 RID: 3140 RVA: 0x0003562B File Offset: 0x0003382B
		[Obsolete("showVertical is obsolete. Use verticalScrollerVisibility instead")]
		public bool showVertical
		{
			get
			{
				return this.verticalScrollerVisibility == ScrollerVisibility.AlwaysVisible;
			}
			set
			{
				this.m_VerticalScrollerVisibility = (value ? ScrollerVisibility.AlwaysVisible : ScrollerVisibility.Auto);
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000C45 RID: 3141 RVA: 0x0003563A File Offset: 0x0003383A
		internal bool needsHorizontal
		{
			get
			{
				return (this.mode != ScrollViewMode.Vertical && this.horizontalScrollerVisibility == ScrollerVisibility.AlwaysVisible) || (this.horizontalScrollerVisibility == ScrollerVisibility.Auto && this.scrollableWidth > 0.001f);
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000C46 RID: 3142 RVA: 0x00035668 File Offset: 0x00033868
		internal bool needsVertical
		{
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			get
			{
				return (this.mode != ScrollViewMode.Horizontal && this.verticalScrollerVisibility == ScrollerVisibility.AlwaysVisible) || (this.verticalScrollerVisibility == ScrollerVisibility.Auto && this.scrollableHeight > 0.001f);
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000C47 RID: 3143 RVA: 0x000356A8 File Offset: 0x000338A8
		internal bool isVerticalScrollDisplayed
		{
			get
			{
				return this.verticalScroller.resolvedStyle.display == DisplayStyle.Flex;
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000C48 RID: 3144 RVA: 0x000356D0 File Offset: 0x000338D0
		internal bool isHorizontalScrollDisplayed
		{
			get
			{
				return this.horizontalScroller.resolvedStyle.display == DisplayStyle.Flex;
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000C49 RID: 3145 RVA: 0x000356F8 File Offset: 0x000338F8
		// (set) Token: 0x06000C4A RID: 3146 RVA: 0x00035710 File Offset: 0x00033910
		[CreateProperty]
		public Vector2 scrollOffset
		{
			get
			{
				return this.m_ScrollOffset;
			}
			set
			{
				bool flag = value != this.m_ScrollOffset;
				if (flag)
				{
					this.horizontalScroller.value = value.x;
					this.verticalScroller.value = value.y;
					this.m_ScrollOffset = new Vector2(this.horizontalScroller.value, this.verticalScroller.value);
					base.SaveViewData();
					bool flag2 = base.panel != null;
					if (flag2)
					{
						this.UpdateScrollers(this.needsHorizontal, this.needsVertical);
						this.UpdateContentViewTransform();
					}
					base.NotifyPropertyChanged(in ScrollView.scrollOffsetProperty);
				}
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000C4B RID: 3147 RVA: 0x000357B4 File Offset: 0x000339B4
		// (set) Token: 0x06000C4C RID: 3148 RVA: 0x000357CC File Offset: 0x000339CC
		[CreateProperty]
		public float horizontalPageSize
		{
			get
			{
				return this.m_HorizontalPageSize;
			}
			set
			{
				float horizontalPageSize = this.m_HorizontalPageSize;
				this.m_HorizontalPageSize = value;
				this.UpdateHorizontalSliderPageSize();
				bool flag = !Mathf.Approximately(horizontalPageSize, this.m_HorizontalPageSize);
				if (flag)
				{
					base.NotifyPropertyChanged(in ScrollView.horizontalPageSizeProperty);
				}
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000C4D RID: 3149 RVA: 0x00035810 File Offset: 0x00033A10
		// (set) Token: 0x06000C4E RID: 3150 RVA: 0x00035828 File Offset: 0x00033A28
		[CreateProperty]
		public float verticalPageSize
		{
			get
			{
				return this.m_VerticalPageSize;
			}
			set
			{
				float verticalPageSize = this.m_VerticalPageSize;
				this.m_VerticalPageSize = value;
				this.UpdateVerticalSliderPageSize();
				bool flag = !Mathf.Approximately(verticalPageSize, this.m_VerticalPageSize);
				if (flag)
				{
					base.NotifyPropertyChanged(in ScrollView.verticalPageSizeProperty);
				}
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000C4F RID: 3151 RVA: 0x0003586C File Offset: 0x00033A6C
		// (set) Token: 0x06000C50 RID: 3152 RVA: 0x00035884 File Offset: 0x00033A84
		[CreateProperty]
		public float mouseWheelScrollSize
		{
			get
			{
				return this.m_MouseWheelScrollSize;
			}
			set
			{
				float mouseWheelScrollSize = this.m_MouseWheelScrollSize;
				bool flag = Math.Abs(this.m_MouseWheelScrollSize - value) > float.Epsilon;
				if (flag)
				{
					this.m_MouseWheelScrollSizeIsInline = true;
					this.m_MouseWheelScrollSize = value;
					base.NotifyPropertyChanged(in ScrollView.mouseWheelScrollSizeProperty);
				}
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000C51 RID: 3153 RVA: 0x000358D0 File Offset: 0x00033AD0
		internal float scrollableWidth
		{
			get
			{
				return this.contentContainer.boundingBox.width - this.contentViewport.layout.width;
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000C52 RID: 3154 RVA: 0x0003590C File Offset: 0x00033B0C
		internal float scrollableHeight
		{
			get
			{
				return this.contentContainer.boundingBox.height - this.contentViewport.layout.height;
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000C53 RID: 3155 RVA: 0x00035945 File Offset: 0x00033B45
		private bool hasInertia
		{
			get
			{
				return this.scrollDecelerationRate > 0f;
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000C54 RID: 3156 RVA: 0x00035954 File Offset: 0x00033B54
		// (set) Token: 0x06000C55 RID: 3157 RVA: 0x0003596C File Offset: 0x00033B6C
		[CreateProperty]
		public float scrollDecelerationRate
		{
			get
			{
				return this.m_ScrollDecelerationRate;
			}
			set
			{
				float scrollDecelerationRate = this.m_ScrollDecelerationRate;
				this.m_ScrollDecelerationRate = Mathf.Max(0f, value);
				bool flag = !Mathf.Approximately(scrollDecelerationRate, this.m_ScrollDecelerationRate);
				if (flag)
				{
					base.NotifyPropertyChanged(in ScrollView.scrollDecelerationRateProperty);
				}
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000C56 RID: 3158 RVA: 0x000359B4 File Offset: 0x00033BB4
		// (set) Token: 0x06000C57 RID: 3159 RVA: 0x000359CC File Offset: 0x00033BCC
		[CreateProperty]
		public float elasticity
		{
			get
			{
				return this.m_Elasticity;
			}
			set
			{
				float elasticity = this.m_Elasticity;
				this.m_Elasticity = Mathf.Max(0f, value);
				bool flag = !Mathf.Approximately(elasticity, this.m_Elasticity);
				if (flag)
				{
					base.NotifyPropertyChanged(in ScrollView.elasticityProperty);
				}
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000C58 RID: 3160 RVA: 0x00035A14 File Offset: 0x00033C14
		// (set) Token: 0x06000C59 RID: 3161 RVA: 0x00035A2C File Offset: 0x00033C2C
		[CreateProperty]
		public ScrollView.TouchScrollBehavior touchScrollBehavior
		{
			get
			{
				return this.m_TouchScrollBehavior;
			}
			set
			{
				ScrollView.TouchScrollBehavior touchScrollBehavior = this.m_TouchScrollBehavior;
				this.m_TouchScrollBehavior = value;
				bool flag = this.m_TouchScrollBehavior == ScrollView.TouchScrollBehavior.Clamped;
				if (flag)
				{
					this.horizontalScroller.slider.clamped = true;
					this.verticalScroller.slider.clamped = true;
				}
				else
				{
					this.horizontalScroller.slider.clamped = false;
					this.verticalScroller.slider.clamped = false;
				}
				bool flag2 = touchScrollBehavior != this.m_TouchScrollBehavior;
				if (flag2)
				{
					base.NotifyPropertyChanged(in ScrollView.touchScrollBehaviorProperty);
				}
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000C5A RID: 3162 RVA: 0x00035ABF File Offset: 0x00033CBF
		// (set) Token: 0x06000C5B RID: 3163 RVA: 0x00035AC8 File Offset: 0x00033CC8
		[CreateProperty]
		public ScrollView.NestedInteractionKind nestedInteractionKind
		{
			get
			{
				return this.m_NestedInteractionKind;
			}
			set
			{
				ScrollView.NestedInteractionKind nestedInteractionKind = this.m_NestedInteractionKind;
				this.m_NestedInteractionKind = value;
				bool flag = nestedInteractionKind != this.m_NestedInteractionKind;
				if (flag)
				{
					base.NotifyPropertyChanged(in ScrollView.nestedInteractionKindProperty);
				}
			}
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x00035B00 File Offset: 0x00033D00
		private void OnHorizontalScrollDragElementChanged(GeometryChangedEvent evt)
		{
			bool flag = evt.oldRect.size == evt.newRect.size;
			if (!flag)
			{
				this.UpdateHorizontalSliderPageSize();
			}
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x00035B40 File Offset: 0x00033D40
		private void OnVerticalScrollDragElementChanged(GeometryChangedEvent evt)
		{
			bool flag = evt.oldRect.size == evt.newRect.size;
			if (!flag)
			{
				this.UpdateVerticalSliderPageSize();
			}
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x00035B80 File Offset: 0x00033D80
		private void UpdateHorizontalSliderPageSize()
		{
			float width = this.horizontalScroller.resolvedStyle.width;
			float num = this.m_HorizontalPageSize;
			bool flag = width > 0f;
			if (flag)
			{
				bool flag2 = Mathf.Approximately(this.m_HorizontalPageSize, -1f);
				if (flag2)
				{
					float width2 = this.horizontalScroller.slider.dragElement.resolvedStyle.width;
					num = width2 * 0.9f;
				}
			}
			bool flag3 = num >= 0f;
			if (flag3)
			{
				this.horizontalScroller.slider.pageSize = num;
			}
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x00035C14 File Offset: 0x00033E14
		private void UpdateVerticalSliderPageSize()
		{
			float height = this.verticalScroller.resolvedStyle.height;
			float num = this.m_VerticalPageSize;
			bool flag = height > 0f;
			if (flag)
			{
				bool flag2 = Mathf.Approximately(this.m_VerticalPageSize, -1f);
				if (flag2)
				{
					float height2 = this.verticalScroller.slider.dragElement.resolvedStyle.height;
					num = height2 * 0.9f;
				}
			}
			bool flag3 = num >= 0f;
			if (flag3)
			{
				this.verticalScroller.slider.pageSize = num;
			}
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x00035CA8 File Offset: 0x00033EA8
		internal void UpdateContentViewTransform()
		{
			Vector3 position = this.contentContainer.transform.position;
			Vector2 scrollOffset = this.scrollOffset;
			bool needsVertical = this.needsVertical;
			if (needsVertical)
			{
				scrollOffset.y += this.contentContainer.resolvedStyle.top;
			}
			position.x = GUIUtility.RoundToPixelGrid(-scrollOffset.x);
			position.y = GUIUtility.RoundToPixelGrid(-scrollOffset.y);
			this.contentContainer.transform.position = position;
			base.IncrementVersion(VersionChangeType.Repaint);
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x00035D38 File Offset: 0x00033F38
		public void ScrollTo(VisualElement child)
		{
			bool flag = child == null;
			if (flag)
			{
				throw new ArgumentNullException("child");
			}
			bool flag2 = !this.contentContainer.Contains(child);
			if (flag2)
			{
				throw new ArgumentException("Cannot scroll to a VisualElement that's not a child of the ScrollView content-container.");
			}
			this.m_Velocity = Vector2.zero;
			float num = 0f;
			float num2 = 0f;
			bool flag3 = this.scrollableHeight > 0f;
			if (flag3)
			{
				num = this.GetYDeltaOffset(child);
				this.verticalScroller.value = this.scrollOffset.y + num;
			}
			bool flag4 = this.scrollableWidth > 0f;
			if (flag4)
			{
				num2 = this.GetXDeltaOffset(child);
				this.horizontalScroller.value = this.scrollOffset.x + num2;
			}
			bool flag5 = num == 0f && num2 == 0f;
			if (!flag5)
			{
				this.UpdateContentViewTransform();
			}
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x00035E1C File Offset: 0x0003401C
		private float GetXDeltaOffset(VisualElement child)
		{
			float num = this.contentContainer.transform.position.x * -1f;
			Rect worldBound = this.contentViewport.worldBound;
			float num2 = worldBound.xMin + num;
			float num3 = worldBound.xMax + num;
			Rect worldBound2 = child.worldBound;
			float num4 = worldBound2.xMin + num;
			float num5 = worldBound2.xMax + num;
			bool flag = (num4 >= num2 && num5 <= num3) || float.IsNaN(num4) || float.IsNaN(num5);
			float num6;
			if (flag)
			{
				num6 = 0f;
			}
			else
			{
				float deltaDistance = this.GetDeltaDistance(num2, num3, num4, num5);
				num6 = deltaDistance * this.horizontalScroller.highValue / this.scrollableWidth;
			}
			return num6;
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x00035EDC File Offset: 0x000340DC
		private float GetYDeltaOffset(VisualElement child)
		{
			float num = this.contentContainer.transform.position.y * -1f;
			Rect worldBound = this.contentViewport.worldBound;
			float num2 = worldBound.yMin + num;
			float num3 = worldBound.yMax + num;
			Rect worldBound2 = child.worldBound;
			float num4 = worldBound2.yMin + num;
			float num5 = worldBound2.yMax + num;
			bool flag = (num4 >= num2 && num5 <= num3) || float.IsNaN(num4) || float.IsNaN(num5);
			float num6;
			if (flag)
			{
				num6 = 0f;
			}
			else
			{
				float deltaDistance = this.GetDeltaDistance(num2, num3, num4, num5);
				num6 = deltaDistance * this.verticalScroller.highValue / this.scrollableHeight;
			}
			return num6;
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x00035F9C File Offset: 0x0003419C
		private float GetDeltaDistance(float viewMin, float viewMax, float childBoundaryMin, float childBoundaryMax)
		{
			float num = viewMax - viewMin;
			float num2 = childBoundaryMax - childBoundaryMin;
			bool flag = num2 > num;
			float num3;
			if (flag)
			{
				bool flag2 = viewMin > childBoundaryMin && childBoundaryMax > viewMax;
				if (flag2)
				{
					num3 = 0f;
				}
				else
				{
					num3 = ((childBoundaryMin > viewMin) ? (childBoundaryMin - viewMin) : (childBoundaryMax - viewMax));
				}
			}
			else
			{
				float num4 = childBoundaryMax - viewMax;
				bool flag3 = num4 < -1f;
				if (flag3)
				{
					num4 = childBoundaryMin - viewMin;
				}
				num3 = num4;
			}
			return num3;
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000C65 RID: 3173 RVA: 0x00036008 File Offset: 0x00034208
		public VisualElement contentViewport { get; }

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000C66 RID: 3174 RVA: 0x00036010 File Offset: 0x00034210
		public Scroller horizontalScroller { get; }

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000C67 RID: 3175 RVA: 0x00036018 File Offset: 0x00034218
		public Scroller verticalScroller { get; }

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000C68 RID: 3176 RVA: 0x00036020 File Offset: 0x00034220
		public override VisualElement contentContainer
		{
			get
			{
				return this.m_ContentContainer;
			}
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x00036038 File Offset: 0x00034238
		public ScrollView()
			: this(ScrollViewMode.Vertical)
		{
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x00036044 File Offset: 0x00034244
		public ScrollView(ScrollViewMode scrollViewMode)
		{
			base.AddToClassList(ScrollView.ussClassName);
			this.m_ContentAndVerticalScrollContainer = new VisualElement
			{
				name = "unity-content-and-vertical-scroll-container"
			};
			this.m_ContentAndVerticalScrollContainer.AddToClassList(ScrollView.contentAndVerticalScrollUssClassName);
			base.hierarchy.Add(this.m_ContentAndVerticalScrollContainer);
			this.contentViewport = new VisualElement
			{
				name = "unity-content-viewport"
			};
			this.contentViewport.AddToClassList(ScrollView.viewportUssClassName);
			this.contentViewport.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnGeometryChanged), TrickleDown.NoTrickleDown);
			this.contentViewport.pickingMode = PickingMode.Ignore;
			this.m_ContentAndVerticalScrollContainer.RegisterCallback<AttachToPanelEvent>(new EventCallback<AttachToPanelEvent>(this.OnAttachToPanel), TrickleDown.NoTrickleDown);
			this.m_ContentAndVerticalScrollContainer.RegisterCallback<DetachFromPanelEvent>(new EventCallback<DetachFromPanelEvent>(this.OnDetachFromPanel), TrickleDown.NoTrickleDown);
			this.m_ContentAndVerticalScrollContainer.Add(this.contentViewport);
			this.m_ContentContainer = new VisualElement
			{
				name = "unity-content-container"
			};
			this.m_ContentContainer.disableClipping = true;
			this.m_ContentContainer.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnGeometryChanged), TrickleDown.NoTrickleDown);
			this.m_ContentContainer.AddToClassList(ScrollView.contentUssClassName);
			this.m_ContentContainer.usageHints = UsageHints.GroupTransform;
			this.contentViewport.Add(this.m_ContentContainer);
			this.SetScrollViewMode(scrollViewMode);
			this.horizontalScroller = new Scroller(0f, 2.1474836E+09f, delegate(float value)
			{
				this.scrollOffset = new Vector2(value, this.scrollOffset.y);
				this.UpdateContentViewTransform();
			}, SliderDirection.Horizontal)
			{
				viewDataKey = "HorizontalScroller"
			};
			this.horizontalScroller.AddToClassList(ScrollView.hScrollerUssClassName);
			this.horizontalScroller.style.display = DisplayStyle.None;
			base.hierarchy.Add(this.horizontalScroller);
			this.verticalScroller = new Scroller(0f, 2.1474836E+09f, delegate(float value)
			{
				this.scrollOffset = new Vector2(this.scrollOffset.x, value);
				this.UpdateContentViewTransform();
			}, SliderDirection.Vertical)
			{
				viewDataKey = "VerticalScroller"
			};
			this.verticalScroller.slider.viewDataRestored += this.OnVerticalSliderViewDataRestored;
			this.horizontalScroller.slider.viewDataRestored += this.OnHorizontalSliderViewDataRestored;
			this.horizontalScroller.slider.onSetValueWithoutNotify += this.OnHorizontalScrollerSetValueWithoutNotify;
			this.verticalScroller.slider.onSetValueWithoutNotify += this.OnVerticalScrollerSetValueWithoutNotify;
			this.horizontalScroller.slider.clampedDragger.draggingEnded += this.UpdateElasticBehaviour;
			this.verticalScroller.slider.clampedDragger.draggingEnded += this.UpdateElasticBehaviour;
			this.horizontalScroller.lowButton.AddAction(new Action(this.UpdateElasticBehaviour));
			this.horizontalScroller.highButton.AddAction(new Action(this.UpdateElasticBehaviour));
			this.verticalScroller.lowButton.AddAction(new Action(this.UpdateElasticBehaviour));
			this.verticalScroller.highButton.AddAction(new Action(this.UpdateElasticBehaviour));
			this.verticalScroller.AddToClassList(ScrollView.vScrollerUssClassName);
			this.verticalScroller.style.display = DisplayStyle.None;
			this.m_ContentAndVerticalScrollContainer.Add(this.verticalScroller);
			this.touchScrollBehavior = ScrollView.TouchScrollBehavior.Clamped;
			base.RegisterCallback<WheelEvent>(new EventCallback<WheelEvent>(this.OnScrollWheel), InvokePolicy.IncludeDisabled, TrickleDown.NoTrickleDown);
			this.verticalScroller.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnScrollersGeometryChanged), TrickleDown.NoTrickleDown);
			this.horizontalScroller.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnScrollersGeometryChanged), TrickleDown.NoTrickleDown);
			this.horizontalPageSize = -1f;
			this.verticalPageSize = -1f;
			this.horizontalScroller.slider.dragElement.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnHorizontalScrollDragElementChanged), TrickleDown.NoTrickleDown);
			this.verticalScroller.slider.dragElement.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnVerticalScrollDragElementChanged), TrickleDown.NoTrickleDown);
			this.m_CapturedTargetPointerMoveCallback = new EventCallback<PointerMoveEvent>(this.OnPointerMove);
			this.m_CapturedTargetPointerUpCallback = new EventCallback<PointerUpEvent>(this.OnPointerUp);
			this.scrollOffset = Vector2.zero;
			this.m_ContentContainer.receivesHierarchyGeometryChangedEvents = true;
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000C6B RID: 3179 RVA: 0x00036510 File Offset: 0x00034710
		// (set) Token: 0x06000C6C RID: 3180 RVA: 0x00036518 File Offset: 0x00034718
		[CreateProperty]
		public ScrollViewMode mode
		{
			get
			{
				return this.m_Mode;
			}
			set
			{
				ScrollViewMode mode = this.m_Mode;
				this.SetScrollViewMode(value);
				bool flag = mode != this.m_Mode;
				if (flag)
				{
					base.NotifyPropertyChanged(in ScrollView.modeProperty);
				}
			}
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x00036554 File Offset: 0x00034754
		private void SetScrollViewMode(ScrollViewMode mode)
		{
			this.m_Mode = mode;
			base.RemoveFromClassList(ScrollView.verticalVariantUssClassName);
			base.RemoveFromClassList(ScrollView.horizontalVariantUssClassName);
			base.RemoveFromClassList(ScrollView.verticalHorizontalVariantUssClassName);
			base.RemoveFromClassList(ScrollView.scrollVariantUssClassName);
			this.contentContainer.RemoveFromClassList(ScrollView.verticalVariantContentUssClassName);
			this.contentContainer.RemoveFromClassList(ScrollView.horizontalVariantContentUssClassName);
			this.contentContainer.RemoveFromClassList(ScrollView.verticalHorizontalVariantContentUssClassName);
			this.contentViewport.RemoveFromClassList(ScrollView.verticalVariantViewportUssClassName);
			this.contentViewport.RemoveFromClassList(ScrollView.horizontalVariantViewportUssClassName);
			this.contentViewport.RemoveFromClassList(ScrollView.verticalHorizontalVariantViewportUssClassName);
			switch (mode)
			{
			case ScrollViewMode.Vertical:
				base.AddToClassList(ScrollView.scrollVariantUssClassName);
				base.AddToClassList(ScrollView.verticalVariantUssClassName);
				this.contentViewport.AddToClassList(ScrollView.verticalVariantViewportUssClassName);
				this.contentContainer.AddToClassList(ScrollView.verticalVariantContentUssClassName);
				break;
			case ScrollViewMode.Horizontal:
				base.AddToClassList(ScrollView.scrollVariantUssClassName);
				base.AddToClassList(ScrollView.horizontalVariantUssClassName);
				this.contentViewport.AddToClassList(ScrollView.horizontalVariantViewportUssClassName);
				this.contentContainer.AddToClassList(ScrollView.horizontalVariantContentUssClassName);
				break;
			case ScrollViewMode.VerticalAndHorizontal:
				base.AddToClassList(ScrollView.scrollVariantUssClassName);
				base.AddToClassList(ScrollView.verticalHorizontalVariantUssClassName);
				this.contentViewport.AddToClassList(ScrollView.verticalHorizontalVariantViewportUssClassName);
				this.contentContainer.AddToClassList(ScrollView.verticalHorizontalVariantContentUssClassName);
				break;
			}
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x000366D0 File Offset: 0x000348D0
		private void OnAttachToPanel(AttachToPanelEvent evt)
		{
			bool flag = evt.destinationPanel == null;
			if (!flag)
			{
				this.m_AttachedRootVisualContainer = base.GetRootVisualContainer(false) ?? evt.destinationPanel.visualTree;
				VisualElement attachedRootVisualContainer = this.m_AttachedRootVisualContainer;
				if (attachedRootVisualContainer != null)
				{
					attachedRootVisualContainer.RegisterCallback<CustomStyleResolvedEvent>(new EventCallback<CustomStyleResolvedEvent>(this.OnRootCustomStyleResolved), TrickleDown.NoTrickleDown);
				}
				base.RegisterCallback<CustomStyleResolvedEvent>(new EventCallback<CustomStyleResolvedEvent>(this.OnRootCustomStyleResolved), TrickleDown.NoTrickleDown);
				this.MarkSingleLineHeightDirty();
				bool flag2 = evt.destinationPanel.contextType == ContextType.Player;
				if (flag2)
				{
					this.m_ContentAndVerticalScrollContainer.RegisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMove), TrickleDown.NoTrickleDown);
					this.contentContainer.RegisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDown), TrickleDown.TrickleDown);
					this.contentContainer.RegisterCallback<PointerCancelEvent>(new EventCallback<PointerCancelEvent>(this.OnPointerCancel), TrickleDown.NoTrickleDown);
					this.contentContainer.RegisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUp), TrickleDown.TrickleDown);
					this.contentContainer.RegisterCallback<PointerCaptureEvent>(new EventCallback<PointerCaptureEvent>(this.OnPointerCapture), TrickleDown.NoTrickleDown);
					this.contentContainer.RegisterCallback<PointerCaptureOutEvent>(new EventCallback<PointerCaptureOutEvent>(this.OnPointerCaptureOut), TrickleDown.NoTrickleDown);
					evt.destinationPanel.visualTree.RegisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnRootPointerUp), TrickleDown.TrickleDown);
				}
			}
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x00036814 File Offset: 0x00034A14
		private void OnDetachFromPanel(DetachFromPanelEvent evt)
		{
			IVisualElementScheduledItem scheduledLayoutPassResetItem = this.m_ScheduledLayoutPassResetItem;
			if (scheduledLayoutPassResetItem != null)
			{
				scheduledLayoutPassResetItem.Pause();
			}
			this.ResetLayoutPass();
			bool flag = evt.originPanel == null;
			if (!flag)
			{
				VisualElement attachedRootVisualContainer = this.m_AttachedRootVisualContainer;
				if (attachedRootVisualContainer != null)
				{
					attachedRootVisualContainer.UnregisterCallback<CustomStyleResolvedEvent>(new EventCallback<CustomStyleResolvedEvent>(this.OnRootCustomStyleResolved), TrickleDown.NoTrickleDown);
				}
				this.m_AttachedRootVisualContainer = null;
				base.UnregisterCallback<CustomStyleResolvedEvent>(new EventCallback<CustomStyleResolvedEvent>(this.OnRootCustomStyleResolved), TrickleDown.NoTrickleDown);
				bool flag2 = evt.originPanel.contextType == ContextType.Player;
				if (flag2)
				{
					this.m_ContentAndVerticalScrollContainer.UnregisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMove), TrickleDown.NoTrickleDown);
					this.contentContainer.UnregisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDown), TrickleDown.TrickleDown);
					this.contentContainer.UnregisterCallback<PointerCancelEvent>(new EventCallback<PointerCancelEvent>(this.OnPointerCancel), TrickleDown.NoTrickleDown);
					this.contentContainer.UnregisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUp), TrickleDown.TrickleDown);
					this.contentContainer.UnregisterCallback<PointerCaptureEvent>(new EventCallback<PointerCaptureEvent>(this.OnPointerCapture), TrickleDown.NoTrickleDown);
					this.contentContainer.UnregisterCallback<PointerCaptureOutEvent>(new EventCallback<PointerCaptureOutEvent>(this.OnPointerCaptureOut), TrickleDown.NoTrickleDown);
					evt.originPanel.visualTree.UnregisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnRootPointerUp), TrickleDown.TrickleDown);
				}
			}
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x00036954 File Offset: 0x00034B54
		private void OnPointerCapture(PointerCaptureEvent evt)
		{
			this.m_CapturedTarget = evt.elementTarget;
			bool flag = this.m_CapturedTarget == null;
			if (!flag)
			{
				this.m_TouchPointerMoveAllowed = true;
				this.m_CapturedTarget.RegisterCallback<PointerMoveEvent>(this.m_CapturedTargetPointerMoveCallback, TrickleDown.NoTrickleDown);
				this.m_CapturedTarget.RegisterCallback<PointerUpEvent>(this.m_CapturedTargetPointerUpCallback, TrickleDown.NoTrickleDown);
			}
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x000369AC File Offset: 0x00034BAC
		private void OnPointerCaptureOut(PointerCaptureOutEvent evt)
		{
			this.ReleaseScrolling(evt.pointerId, evt.target);
			bool flag = this.m_CapturedTarget == null;
			if (!flag)
			{
				this.m_CapturedTarget.UnregisterCallback<PointerMoveEvent>(this.m_CapturedTargetPointerMoveCallback, TrickleDown.NoTrickleDown);
				this.m_CapturedTarget.UnregisterCallback<PointerUpEvent>(this.m_CapturedTargetPointerUpCallback, TrickleDown.NoTrickleDown);
				this.m_CapturedTarget = null;
			}
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x00036A0C File Offset: 0x00034C0C
		private void OnGeometryChanged(GeometryChangedEvent evt)
		{
			bool flag = evt.oldRect.size == evt.newRect.size;
			if (!flag)
			{
				bool flag2 = this.needsVertical;
				bool flag3 = this.needsHorizontal;
				bool flag4 = this.m_FirstLayoutPass == -1;
				if (flag4)
				{
					this.m_FirstLayoutPass = evt.layoutPass;
				}
				else
				{
					bool flag5 = evt.layoutPass - this.m_FirstLayoutPass > 5;
					if (flag5)
					{
						flag2 = flag2 || this.isVerticalScrollDisplayed;
						flag3 = flag3 || this.isHorizontalScrollDisplayed;
					}
				}
				this.UpdateScrollers(flag3, flag2);
				this.UpdateContentViewTransform();
				this.ScheduleResetLayoutPass();
			}
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x00036AB8 File Offset: 0x00034CB8
		private void OnVerticalSliderViewDataRestored()
		{
			this.verticalScroller.highValue = (float.IsNaN(this.scrollableHeight) ? this.verticalScroller.highValue : this.scrollableHeight);
			this.UpdateContentViewTransform();
		}

		// Token: 0x06000C74 RID: 3188 RVA: 0x00036AEE File Offset: 0x00034CEE
		private void OnHorizontalSliderViewDataRestored()
		{
			this.horizontalScroller.highValue = (float.IsNaN(this.scrollableWidth) ? this.horizontalScroller.highValue : this.scrollableWidth);
			this.UpdateContentViewTransform();
		}

		// Token: 0x06000C75 RID: 3189 RVA: 0x00036B24 File Offset: 0x00034D24
		private void OnVerticalScrollerSetValueWithoutNotify(float value)
		{
			this.m_ScrollOffset = new Vector2(this.scrollOffset.x, value);
			base.SaveViewData();
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x00036B45 File Offset: 0x00034D45
		private void OnHorizontalScrollerSetValueWithoutNotify(float value)
		{
			this.m_ScrollOffset = new Vector2(value, this.scrollOffset.y);
			base.SaveViewData();
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x00036B68 File Offset: 0x00034D68
		private void ScheduleResetLayoutPass()
		{
			bool flag = this.m_ScheduledLayoutPassResetItem == null;
			if (flag)
			{
				this.m_ScheduledLayoutPassResetItem = base.schedule.Execute(new Action(this.ResetLayoutPass));
			}
			else
			{
				this.m_ScheduledLayoutPassResetItem.Pause();
				this.m_ScheduledLayoutPassResetItem.Resume();
			}
		}

		// Token: 0x06000C78 RID: 3192 RVA: 0x00036BBE File Offset: 0x00034DBE
		private void ResetLayoutPass()
		{
			this.m_FirstLayoutPass = -1;
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x00036BC8 File Offset: 0x00034DC8
		private static float ComputeElasticOffset(float deltaPointer, float initialScrollOffset, float lowLimit, float hardLowLimit, float highLimit, float hardHighLimit)
		{
			initialScrollOffset = Mathf.Max(initialScrollOffset, hardLowLimit * 0.95f);
			initialScrollOffset = Mathf.Min(initialScrollOffset, hardHighLimit * 0.95f);
			bool flag = initialScrollOffset < lowLimit && hardLowLimit < lowLimit;
			float num;
			float num3;
			if (flag)
			{
				num = lowLimit - hardLowLimit;
				float num2 = (lowLimit - initialScrollOffset) / num;
				num3 = num2 * num / (1f - num2);
				num3 += deltaPointer;
				initialScrollOffset = lowLimit;
			}
			else
			{
				bool flag2 = initialScrollOffset > highLimit && hardHighLimit > highLimit;
				if (flag2)
				{
					num = hardHighLimit - highLimit;
					float num4 = (initialScrollOffset - highLimit) / num;
					num3 = -1f * num4 * num / (1f - num4);
					num3 += deltaPointer;
					initialScrollOffset = highLimit;
				}
				else
				{
					num3 = deltaPointer;
				}
			}
			float num5 = initialScrollOffset - num3;
			bool flag3 = num5 < lowLimit;
			float num6;
			if (flag3)
			{
				num3 = lowLimit - num5;
				initialScrollOffset = lowLimit;
				num = lowLimit - hardLowLimit;
				num6 = 1f;
			}
			else
			{
				bool flag4 = num5 <= highLimit;
				if (flag4)
				{
					return num5;
				}
				num3 = num5 - highLimit;
				initialScrollOffset = highLimit;
				num = hardHighLimit - highLimit;
				num6 = -1f;
			}
			bool flag5 = Mathf.Abs(num3) < 1E-30f;
			float num7;
			if (flag5)
			{
				num7 = initialScrollOffset;
			}
			else
			{
				float num8 = num3 / (num3 + num);
				num8 *= num;
				num8 *= num6;
				num5 = initialScrollOffset - num8;
				num7 = num5;
			}
			return num7;
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x00036CF8 File Offset: 0x00034EF8
		private void ComputeInitialSpringBackVelocity()
		{
			bool flag = this.touchScrollBehavior != ScrollView.TouchScrollBehavior.Elastic;
			if (flag)
			{
				this.m_SpringBackVelocity = Vector2.zero;
			}
			else
			{
				bool flag2 = this.scrollOffset.x < this.m_LowBounds.x;
				if (flag2)
				{
					this.m_SpringBackVelocity.x = this.m_LowBounds.x - this.scrollOffset.x;
				}
				else
				{
					bool flag3 = this.scrollOffset.x > this.m_HighBounds.x;
					if (flag3)
					{
						this.m_SpringBackVelocity.x = this.m_HighBounds.x - this.scrollOffset.x;
					}
					else
					{
						this.m_SpringBackVelocity.x = 0f;
					}
				}
				bool flag4 = this.scrollOffset.y < this.m_LowBounds.y;
				if (flag4)
				{
					this.m_SpringBackVelocity.y = this.m_LowBounds.y - this.scrollOffset.y;
				}
				else
				{
					bool flag5 = this.scrollOffset.y > this.m_HighBounds.y;
					if (flag5)
					{
						this.m_SpringBackVelocity.y = this.m_HighBounds.y - this.scrollOffset.y;
					}
					else
					{
						this.m_SpringBackVelocity.y = 0f;
					}
				}
			}
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x00036E58 File Offset: 0x00035058
		private void SpringBack()
		{
			bool flag = this.touchScrollBehavior != ScrollView.TouchScrollBehavior.Elastic;
			if (flag)
			{
				this.m_SpringBackVelocity = Vector2.zero;
			}
			else
			{
				Vector2 scrollOffset = this.scrollOffset;
				bool flag2 = scrollOffset.x < this.m_LowBounds.x;
				if (flag2)
				{
					scrollOffset.x = Mathf.SmoothDamp(scrollOffset.x, this.m_LowBounds.x, ref this.m_SpringBackVelocity.x, this.elasticity, float.PositiveInfinity, this.elapsedTimeSinceLastHorizontalTouchScroll);
					bool flag3 = Mathf.Abs(this.m_SpringBackVelocity.x) < base.scaledPixelsPerPoint;
					if (flag3)
					{
						this.m_SpringBackVelocity.x = 0f;
					}
				}
				else
				{
					bool flag4 = scrollOffset.x > this.m_HighBounds.x;
					if (flag4)
					{
						scrollOffset.x = Mathf.SmoothDamp(scrollOffset.x, this.m_HighBounds.x, ref this.m_SpringBackVelocity.x, this.elasticity, float.PositiveInfinity, this.elapsedTimeSinceLastHorizontalTouchScroll);
						bool flag5 = Mathf.Abs(this.m_SpringBackVelocity.x) < base.scaledPixelsPerPoint;
						if (flag5)
						{
							this.m_SpringBackVelocity.x = 0f;
						}
					}
					else
					{
						this.m_SpringBackVelocity.x = 0f;
					}
				}
				bool flag6 = scrollOffset.y < this.m_LowBounds.y;
				if (flag6)
				{
					scrollOffset.y = Mathf.SmoothDamp(scrollOffset.y, this.m_LowBounds.y, ref this.m_SpringBackVelocity.y, this.elasticity, float.PositiveInfinity, this.elapsedTimeSinceLastVerticalTouchScroll);
					bool flag7 = Mathf.Abs(this.m_SpringBackVelocity.y) < base.scaledPixelsPerPoint;
					if (flag7)
					{
						this.m_SpringBackVelocity.y = 0f;
					}
				}
				else
				{
					bool flag8 = scrollOffset.y > this.m_HighBounds.y;
					if (flag8)
					{
						scrollOffset.y = Mathf.SmoothDamp(scrollOffset.y, this.m_HighBounds.y, ref this.m_SpringBackVelocity.y, this.elasticity, float.PositiveInfinity, this.elapsedTimeSinceLastVerticalTouchScroll);
						bool flag9 = Mathf.Abs(this.m_SpringBackVelocity.y) < base.scaledPixelsPerPoint;
						if (flag9)
						{
							this.m_SpringBackVelocity.y = 0f;
						}
					}
					else
					{
						this.m_SpringBackVelocity.y = 0f;
					}
				}
				this.scrollOffset = scrollOffset;
			}
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x000370D4 File Offset: 0x000352D4
		internal void ApplyScrollInertia()
		{
			bool flag = this.hasInertia && this.m_Velocity != Vector2.zero;
			if (flag)
			{
				Vector2 vector = Vector2.zero;
				float num = 0f;
				while (num < this.elapsedTimeSinceLastVerticalTouchScroll)
				{
					this.m_Velocity *= Mathf.Pow(this.scrollDecelerationRate, this.k_TouchScrollInertiaBaseTimeInterval);
					num += this.k_TouchScrollInertiaBaseTimeInterval;
					vector += this.m_Velocity * this.k_TouchScrollInertiaBaseTimeInterval;
				}
				float num2 = this.elapsedTimeSinceLastVerticalTouchScroll - num;
				bool flag2 = num2 > 0f && num2 < this.k_TouchScrollInertiaBaseTimeInterval;
				if (flag2)
				{
					this.m_Velocity *= Mathf.Pow(this.scrollDecelerationRate, num2);
					vector += this.m_Velocity * num2;
				}
				float num3 = base.scaledPixelsPerPoint * this.k_ScaledPixelsPerPointMultiplier;
				bool flag3 = Mathf.Abs(this.m_Velocity.x) <= num3 || (this.touchScrollBehavior == ScrollView.TouchScrollBehavior.Elastic && (this.scrollOffset.x < this.m_LowBounds.x || this.scrollOffset.x > this.m_HighBounds.x));
				if (flag3)
				{
					this.m_Velocity.x = 0f;
				}
				bool flag4 = Mathf.Abs(this.m_Velocity.y) <= num3 || (this.touchScrollBehavior == ScrollView.TouchScrollBehavior.Elastic && (this.scrollOffset.y < this.m_LowBounds.y || this.scrollOffset.y > this.m_HighBounds.y));
				if (flag4)
				{
					this.m_Velocity.y = 0f;
				}
				this.scrollOffset += vector;
			}
			else
			{
				this.m_Velocity = Vector2.zero;
			}
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x000372CC File Offset: 0x000354CC
		private void PostPointerUpAnimation()
		{
			this.elapsedTimeSinceLastVerticalTouchScroll = Time.unscaledTime - this.previousVerticalTouchScrollTimeStamp;
			this.previousVerticalTouchScrollTimeStamp = Time.unscaledTime;
			this.elapsedTimeSinceLastHorizontalTouchScroll = Time.unscaledTime - this.previousHorizontalTouchScrollTimeStamp;
			this.previousHorizontalTouchScrollTimeStamp = Time.unscaledTime;
			this.ApplyScrollInertia();
			this.SpringBack();
			bool flag = this.m_SpringBackVelocity == Vector2.zero && this.m_Velocity == Vector2.zero;
			if (flag)
			{
				this.m_PostPointerUpAnimation.Pause();
				this.elapsedTimeSinceLastVerticalTouchScroll = 0f;
				this.elapsedTimeSinceLastHorizontalTouchScroll = 0f;
				this.previousVerticalTouchScrollTimeStamp = 0f;
				this.previousHorizontalTouchScrollTimeStamp = 0f;
			}
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x00037388 File Offset: 0x00035588
		private void OnPointerDown(PointerDownEvent evt)
		{
			bool flag = evt.pointerType == PointerType.mouse || !evt.isPrimary;
			if (!flag)
			{
				bool flag2 = evt.pointerId != PointerId.invalidPointerId;
				if (flag2)
				{
					this.ReleaseScrolling(evt.pointerId, evt.target);
				}
				IVisualElementScheduledItem postPointerUpAnimation = this.m_PostPointerUpAnimation;
				if (postPointerUpAnimation != null)
				{
					postPointerUpAnimation.Pause();
				}
				bool flag3 = Mathf.Abs(this.m_Velocity.x) > 10f || Mathf.Abs(this.m_Velocity.y) > 10f;
				this.m_TouchPointerMoveAllowed = true;
				this.m_StartedMoving = false;
				this.InitTouchScrolling(evt.position);
				bool flag4 = flag3;
				if (flag4)
				{
					this.contentContainer.CapturePointer(evt.pointerId);
					this.contentContainer.panel.PreventCompatibilityMouseEvents(evt.pointerId);
					evt.StopPropagation();
					this.m_TouchStoppedVelocity = true;
				}
			}
		}

		// Token: 0x06000C7F RID: 3199 RVA: 0x00037488 File Offset: 0x00035688
		private void OnPointerMove(PointerMoveEvent evt)
		{
			bool flag = evt.pointerType == PointerType.mouse || !evt.isPrimary || !this.m_TouchPointerMoveAllowed;
			if (!flag)
			{
				bool isHandledByDraggable = evt.isHandledByDraggable;
				if (isHandledByDraggable)
				{
					this.m_PointerStartPosition = evt.position;
					this.m_StartPosition = this.scrollOffset;
				}
				else
				{
					Vector2 vector = evt.position;
					Vector2 vector2 = vector - this.m_PointerStartPosition;
					bool flag2 = this.mode == ScrollViewMode.Horizontal;
					if (flag2)
					{
						vector2.y = 0f;
					}
					else
					{
						bool flag3 = this.mode == ScrollViewMode.Vertical;
						if (flag3)
						{
							vector2.x = 0f;
						}
					}
					bool flag4 = !this.m_TouchStoppedVelocity && !this.m_StartedMoving && vector2.sqrMagnitude < 100f;
					if (!flag4)
					{
						ScrollView.TouchScrollingResult touchScrollingResult = this.ComputeTouchScrolling(evt.position);
						bool flag5 = touchScrollingResult != ScrollView.TouchScrollingResult.Forward;
						if (flag5)
						{
							evt.isHandledByDraggable = true;
							evt.StopPropagation();
							bool flag6 = !this.contentContainer.HasPointerCapture(evt.pointerId);
							if (flag6)
							{
								this.contentContainer.CapturePointer(evt.pointerId);
							}
						}
						else
						{
							this.m_Velocity = Vector2.zero;
						}
					}
				}
			}
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x000375D9 File Offset: 0x000357D9
		private void OnPointerCancel(PointerCancelEvent evt)
		{
			this.ReleaseScrolling(evt.pointerId, evt.target);
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x000375F0 File Offset: 0x000357F0
		private void OnPointerUp(PointerUpEvent evt)
		{
			bool flag = this.ReleaseScrolling(evt.pointerId, evt.target);
			if (flag)
			{
				this.contentContainer.panel.PreventCompatibilityMouseEvents(evt.pointerId);
				evt.StopPropagation();
			}
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x00037634 File Offset: 0x00035834
		internal void InitTouchScrolling(Vector2 position)
		{
			this.m_PointerStartPosition = position;
			this.m_StartPosition = this.scrollOffset;
			this.m_Velocity = Vector2.zero;
			this.m_SpringBackVelocity = Vector2.zero;
			this.m_LowBounds = new Vector2(Mathf.Min(this.horizontalScroller.lowValue, this.horizontalScroller.highValue), Mathf.Min(this.verticalScroller.lowValue, this.verticalScroller.highValue));
			this.m_HighBounds = new Vector2(Mathf.Max(this.horizontalScroller.lowValue, this.horizontalScroller.highValue), Mathf.Max(this.verticalScroller.lowValue, this.verticalScroller.highValue));
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x000376F0 File Offset: 0x000358F0
		internal ScrollView.TouchScrollingResult ComputeTouchScrolling(Vector2 position)
		{
			bool flag = this.touchScrollBehavior == ScrollView.TouchScrollBehavior.Clamped;
			Vector2 vector;
			if (flag)
			{
				vector = this.m_StartPosition - (position - this.m_PointerStartPosition);
				vector = Vector2.Max(vector, this.m_LowBounds);
				vector = Vector2.Min(vector, this.m_HighBounds);
			}
			else
			{
				bool flag2 = this.touchScrollBehavior == ScrollView.TouchScrollBehavior.Elastic;
				if (flag2)
				{
					Vector2 vector2 = position - this.m_PointerStartPosition;
					vector.x = ScrollView.ComputeElasticOffset(vector2.x, this.m_StartPosition.x, this.m_LowBounds.x, this.m_LowBounds.x - this.contentViewport.resolvedStyle.width, this.m_HighBounds.x, this.m_HighBounds.x + this.contentViewport.resolvedStyle.width);
					vector.y = ScrollView.ComputeElasticOffset(vector2.y, this.m_StartPosition.y, this.m_LowBounds.y, this.m_LowBounds.y - this.contentViewport.resolvedStyle.height, this.m_HighBounds.y, this.m_HighBounds.y + this.contentViewport.resolvedStyle.height);
					this.previousVerticalTouchScrollTimeStamp = Time.unscaledTime;
					this.previousHorizontalTouchScrollTimeStamp = Time.unscaledTime;
				}
				else
				{
					vector = this.m_StartPosition - (position - this.m_PointerStartPosition);
				}
			}
			bool flag3 = this.mode == ScrollViewMode.Vertical;
			if (flag3)
			{
				vector.x = this.m_LowBounds.x;
			}
			else
			{
				bool flag4 = this.mode == ScrollViewMode.Horizontal;
				if (flag4)
				{
					vector.y = this.m_LowBounds.y;
				}
			}
			bool flag5 = this.scrollOffset != vector;
			bool flag6 = flag5;
			ScrollView.TouchScrollingResult touchScrollingResult;
			if (flag6)
			{
				touchScrollingResult = (this.ApplyTouchScrolling(vector) ? ScrollView.TouchScrollingResult.Apply : ScrollView.TouchScrollingResult.Forward);
			}
			else
			{
				touchScrollingResult = ((this.m_StartedMoving && this.nestedInteractionKind != ScrollView.NestedInteractionKind.ForwardScrolling) ? ScrollView.TouchScrollingResult.Block : ScrollView.TouchScrollingResult.Forward);
			}
			return touchScrollingResult;
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x00037900 File Offset: 0x00035B00
		private bool ApplyTouchScrolling(Vector2 newScrollOffset)
		{
			this.m_StartedMoving = true;
			bool hasInertia = this.hasInertia;
			if (hasInertia)
			{
				bool flag = newScrollOffset == this.m_LowBounds || newScrollOffset == this.m_HighBounds;
				if (flag)
				{
					this.m_Velocity = Vector2.zero;
					this.scrollOffset = newScrollOffset;
					return false;
				}
				bool flag2 = this.m_LastVelocityLerpTime > 0f;
				if (flag2)
				{
					float num = Time.unscaledTime - this.m_LastVelocityLerpTime;
					this.m_Velocity = Vector2.Lerp(this.m_Velocity, Vector2.zero, num * 10f);
				}
				this.m_LastVelocityLerpTime = Time.unscaledTime;
				float num2 = this.k_TouchScrollInertiaBaseTimeInterval;
				Vector2 vector = (newScrollOffset - this.scrollOffset) / num2;
				this.m_Velocity = Vector2.Lerp(this.m_Velocity, vector, num2 * 10f);
			}
			bool flag3 = this.scrollOffset != newScrollOffset;
			this.scrollOffset = newScrollOffset;
			return flag3;
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x00037A00 File Offset: 0x00035C00
		private bool ReleaseScrolling(int pointerId, IEventHandler target)
		{
			this.m_TouchStoppedVelocity = false;
			this.m_StartedMoving = false;
			this.m_TouchPointerMoveAllowed = false;
			bool flag = target != this.contentContainer || !this.contentContainer.HasPointerCapture(pointerId);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				this.previousVerticalTouchScrollTimeStamp = Time.unscaledTime;
				this.previousHorizontalTouchScrollTimeStamp = Time.unscaledTime;
				bool flag3 = this.touchScrollBehavior == ScrollView.TouchScrollBehavior.Elastic || this.hasInertia;
				if (flag3)
				{
					this.ExecuteElasticSpringAnimation();
				}
				this.contentContainer.ReleasePointer(pointerId);
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x06000C86 RID: 3206 RVA: 0x00037A90 File Offset: 0x00035C90
		private void ExecuteElasticSpringAnimation()
		{
			this.ComputeInitialSpringBackVelocity();
			bool flag = this.m_PostPointerUpAnimation == null;
			if (flag)
			{
				this.m_PostPointerUpAnimation = base.schedule.Execute(new Action(this.PostPointerUpAnimation)).Every(this.m_ElasticAnimationIntervalMs);
			}
			else
			{
				this.m_PostPointerUpAnimation.Resume();
			}
		}

		// Token: 0x06000C87 RID: 3207 RVA: 0x00037AEC File Offset: 0x00035CEC
		private void AdjustScrollers()
		{
			float num = ((this.contentContainer.boundingBox.width > 1E-30f) ? (this.contentViewport.layout.width / this.contentContainer.boundingBox.width) : 1f);
			float num2 = ((this.contentContainer.boundingBox.height > 1E-30f) ? (this.contentViewport.layout.height / this.contentContainer.boundingBox.height) : 1f);
			this.horizontalScroller.Adjust(num);
			this.verticalScroller.Adjust(num2);
		}

		// Token: 0x06000C88 RID: 3208 RVA: 0x00037BA8 File Offset: 0x00035DA8
		internal void UpdateScrollers(bool displayHorizontal, bool displayVertical)
		{
			this.AdjustScrollers();
			bool flag = this.scrollableHeight > 0f;
			bool flag2 = this.scrollableWidth > 0f;
			this.horizontalScroller.SetEnabled(flag2);
			this.verticalScroller.SetEnabled(flag);
			this.verticalScroller.slider.clampedDragger.acceptClicksIfDisabled = flag;
			this.verticalScroller.lowButton.acceptClicksIfDisabled = flag;
			this.verticalScroller.highButton.acceptClicksIfDisabled = flag;
			this.horizontalScroller.slider.clampedDragger.acceptClicksIfDisabled = flag2;
			this.horizontalScroller.lowButton.acceptClicksIfDisabled = flag2;
			this.horizontalScroller.highButton.acceptClicksIfDisabled = flag2;
			bool flag3 = displayHorizontal && this.m_HorizontalScrollerVisibility != ScrollerVisibility.Hidden;
			bool flag4 = displayVertical && this.m_VerticalScrollerVisibility != ScrollerVisibility.Hidden;
			DisplayStyle displayStyle = (flag3 ? DisplayStyle.Flex : DisplayStyle.None);
			DisplayStyle displayStyle2 = (flag4 ? DisplayStyle.Flex : DisplayStyle.None);
			bool flag5 = displayStyle != this.horizontalScroller.style.display;
			if (flag5)
			{
				this.horizontalScroller.style.display = displayStyle;
			}
			bool flag6 = displayStyle2 != this.verticalScroller.style.display;
			if (flag6)
			{
				this.verticalScroller.style.display = displayStyle2;
			}
			this.verticalScroller.lowValue = 0f;
			this.verticalScroller.highValue = (float.IsNaN(this.scrollableHeight) ? this.verticalScroller.highValue : this.scrollableHeight);
			this.horizontalScroller.lowValue = 0f;
			this.horizontalScroller.highValue = (float.IsNaN(this.scrollableWidth) ? this.horizontalScroller.highValue : this.scrollableWidth);
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x00037D94 File Offset: 0x00035F94
		private void OnScrollersGeometryChanged(GeometryChangedEvent evt)
		{
			bool flag = evt.oldRect.size == evt.newRect.size;
			if (!flag)
			{
				bool flag2 = this.needsHorizontal && this.m_HorizontalScrollerVisibility != ScrollerVisibility.Hidden;
				bool flag3 = flag2;
				if (flag3)
				{
					this.horizontalScroller.style.marginRight = this.verticalScroller.layout.width;
				}
				this.AdjustScrollers();
			}
		}

		// Token: 0x06000C8A RID: 3210 RVA: 0x00037E1C File Offset: 0x0003601C
		private void OnScrollWheel(WheelEvent evt)
		{
			bool flag = Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer;
			if (flag)
			{
				bool commandKey = evt.commandKey;
				if (commandKey)
				{
					return;
				}
			}
			else
			{
				bool ctrlKey = evt.ctrlKey;
				if (ctrlKey)
				{
					return;
				}
			}
			bool flag2 = false;
			bool flag3 = this.mode != ScrollViewMode.Horizontal && this.scrollableHeight > 0f;
			bool flag4 = this.mode != ScrollViewMode.Vertical && this.scrollableWidth > 0f;
			float num = ((flag4 && !flag3) ? evt.delta.y : evt.delta.x);
			bool flag5 = (flag4 || flag3) && !this.m_MouseWheelScrollSizeIsInline;
			if (flag5)
			{
				bool flag6;
				if (!this.m_SingleLineHeightDirtyFlag)
				{
					VisualElement parent = base.parent;
					flag6 = parent != null && parent.isRootVisualContainer && base.parent != this.m_AttachedRootVisualContainer;
				}
				else
				{
					flag6 = true;
				}
				bool flag7 = flag6;
				if (flag7)
				{
					this.ReadSingleLineHeight();
				}
			}
			float num2 = (this.m_MouseWheelScrollSizeIsInline ? this.mouseWheelScrollSize : this.m_SingleLineHeight);
			bool flag8 = flag3;
			if (flag8)
			{
				float value = this.verticalScroller.value;
				this.verticalScroller.value += evt.delta.y * ((this.verticalScroller.lowValue < this.verticalScroller.highValue) ? 1f : (-1f)) * num2;
				bool flag9 = this.nestedInteractionKind == ScrollView.NestedInteractionKind.StopScrolling || !Mathf.Approximately(this.verticalScroller.value, value);
				if (flag9)
				{
					evt.StopPropagation();
					flag2 = true;
				}
			}
			bool flag10 = flag4;
			if (flag10)
			{
				float value2 = this.horizontalScroller.value;
				this.horizontalScroller.value += num * ((this.horizontalScroller.lowValue < this.horizontalScroller.highValue) ? 1f : (-1f)) * num2;
				bool flag11 = this.nestedInteractionKind == ScrollView.NestedInteractionKind.StopScrolling || !Mathf.Approximately(this.horizontalScroller.value, value2);
				if (flag11)
				{
					evt.StopPropagation();
					flag2 = true;
				}
			}
			bool flag12 = flag2;
			if (flag12)
			{
				this.UpdateElasticBehaviour();
				this.UpdateContentViewTransform();
			}
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x0003805D File Offset: 0x0003625D
		private void OnRootCustomStyleResolved(CustomStyleResolvedEvent evt)
		{
			this.MarkSingleLineHeightDirty();
		}

		// Token: 0x06000C8C RID: 3212 RVA: 0x00038067 File Offset: 0x00036267
		private void MarkSingleLineHeightDirty()
		{
			this.m_SingleLineHeightDirtyFlag = true;
		}

		// Token: 0x06000C8D RID: 3213 RVA: 0x00038071 File Offset: 0x00036271
		private void OnRootPointerUp(PointerUpEvent evt)
		{
			this.m_TouchPointerMoveAllowed = false;
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x0003807C File Offset: 0x0003627C
		private unsafe void ReadSingleLineHeight()
		{
			StylePropertyValue stylePropertyValue;
			bool flag = base.computedStyle.customProperties != null && base.computedStyle.customProperties.TryGetValue("--unity-metrics-single_line-height", out stylePropertyValue);
			if (flag)
			{
				this.m_SingleLineHeightDirtyFlag = false;
				Dimension dimension;
				bool flag2 = stylePropertyValue.sheet.TryReadDimension(stylePropertyValue.handle, out dimension);
				if (flag2)
				{
					this.m_SingleLineHeight = dimension.value;
					return;
				}
			}
			VisualElement firstAncestorWhere = base.GetFirstAncestorWhere((VisualElement x) => x.isRootVisualContainer);
			bool flag3 = firstAncestorWhere != this.m_AttachedRootVisualContainer;
			if (flag3)
			{
				this.m_AttachedRootVisualContainer.UnregisterCallback<CustomStyleResolvedEvent>(new EventCallback<CustomStyleResolvedEvent>(this.OnRootCustomStyleResolved), TrickleDown.NoTrickleDown);
				this.m_AttachedRootVisualContainer = firstAncestorWhere;
				this.m_AttachedRootVisualContainer.RegisterCallback<CustomStyleResolvedEvent>(new EventCallback<CustomStyleResolvedEvent>(this.OnRootCustomStyleResolved), TrickleDown.NoTrickleDown);
			}
			VisualElement attachedRootVisualContainer = this.m_AttachedRootVisualContainer;
			StylePropertyValue stylePropertyValue2;
			bool flag4 = attachedRootVisualContainer != null && attachedRootVisualContainer.computedStyle->customProperties != null && this.m_AttachedRootVisualContainer.computedStyle.customProperties.TryGetValue("--unity-metrics-single_line-height", out stylePropertyValue2);
			if (flag4)
			{
				this.m_SingleLineHeightDirtyFlag = false;
				Dimension dimension2;
				bool flag5 = stylePropertyValue2.sheet.TryReadDimension(stylePropertyValue2.handle, out dimension2);
				if (flag5)
				{
					this.m_SingleLineHeight = dimension2.value;
					return;
				}
			}
			this.m_SingleLineHeight = UIElementsUtility.singleLineHeight;
			this.m_SingleLineHeightDirtyFlag = false;
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x000381E8 File Offset: 0x000363E8
		private void UpdateElasticBehaviour()
		{
			bool flag = this.touchScrollBehavior == ScrollView.TouchScrollBehavior.Elastic;
			if (flag)
			{
				this.m_LowBounds = new Vector2(Mathf.Min(this.horizontalScroller.lowValue, this.horizontalScroller.highValue), Mathf.Min(this.verticalScroller.lowValue, this.verticalScroller.highValue));
				this.m_HighBounds = new Vector2(Mathf.Max(this.horizontalScroller.lowValue, this.horizontalScroller.highValue), Mathf.Max(this.verticalScroller.lowValue, this.verticalScroller.highValue));
				this.ExecuteElasticSpringAnimation();
			}
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x00038294 File Offset: 0x00036494
		internal void SetScrollOffsetWithoutNotify(Vector2 value)
		{
			this.horizontalScroller.slider.SetValueWithoutNotify(value.x);
			this.verticalScroller.slider.SetValueWithoutNotify(value.y);
			this.m_ScrollOffset = new Vector2(this.horizontalScroller.value, this.verticalScroller.value);
			base.SaveViewData();
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x000382F8 File Offset: 0x000364F8
		internal override void OnViewDataReady()
		{
			base.OnViewDataReady();
			bool flag = string.IsNullOrEmpty(this.verticalScroller.viewDataKey) && string.IsNullOrEmpty(this.verticalScroller.slider.viewDataKey) && string.IsNullOrEmpty(this.horizontalScroller.viewDataKey) && string.IsNullOrEmpty(this.horizontalScroller.slider.viewDataKey);
			if (!flag)
			{
				string fullHierarchicalViewDataKey = base.GetFullHierarchicalViewDataKey();
				base.OverwriteFromViewData(this, fullHierarchicalViewDataKey);
				this.UpdateContentViewTransform();
			}
		}

		// Token: 0x0400074A RID: 1866
		internal static readonly BindingId horizontalScrollerVisibilityProperty = "horizontalScrollerVisibility";

		// Token: 0x0400074B RID: 1867
		internal static readonly BindingId verticalScrollerVisibilityProperty = "verticalScrollerVisibility";

		// Token: 0x0400074C RID: 1868
		internal static readonly BindingId scrollOffsetProperty = "scrollOffset";

		// Token: 0x0400074D RID: 1869
		internal static readonly BindingId horizontalPageSizeProperty = "horizontalPageSize";

		// Token: 0x0400074E RID: 1870
		internal static readonly BindingId verticalPageSizeProperty = "verticalPageSize";

		// Token: 0x0400074F RID: 1871
		internal static readonly BindingId mouseWheelScrollSizeProperty = "mouseWheelScrollSize";

		// Token: 0x04000750 RID: 1872
		internal static readonly BindingId scrollDecelerationRateProperty = "scrollDecelerationRate";

		// Token: 0x04000751 RID: 1873
		internal static readonly BindingId elasticityProperty = "elasticity";

		// Token: 0x04000752 RID: 1874
		internal static readonly BindingId touchScrollBehaviorProperty = "touchScrollBehavior";

		// Token: 0x04000753 RID: 1875
		internal static readonly BindingId nestedInteractionKindProperty = "nestedInteractionKind";

		// Token: 0x04000754 RID: 1876
		internal static readonly BindingId modeProperty = "mode";

		// Token: 0x04000755 RID: 1877
		internal static readonly BindingId elasticAnimationIntervalMsProperty = "elasticAnimationIntervalMs";

		// Token: 0x04000756 RID: 1878
		private const int k_MaxLocalLayoutPassCount = 5;

		// Token: 0x04000757 RID: 1879
		private int m_FirstLayoutPass = -1;

		// Token: 0x04000758 RID: 1880
		private ScrollerVisibility m_HorizontalScrollerVisibility;

		// Token: 0x04000759 RID: 1881
		private ScrollerVisibility m_VerticalScrollerVisibility;

		// Token: 0x0400075A RID: 1882
		private long m_ElasticAnimationIntervalMs = 16L;

		// Token: 0x0400075B RID: 1883
		private const float k_SizeThreshold = 0.001f;

		// Token: 0x0400075C RID: 1884
		private VisualElement m_AttachedRootVisualContainer;

		// Token: 0x0400075D RID: 1885
		private float m_SingleLineHeight = UIElementsUtility.singleLineHeight;

		// Token: 0x0400075E RID: 1886
		private bool m_SingleLineHeightDirtyFlag;

		// Token: 0x0400075F RID: 1887
		private const string k_SingleLineHeightPropertyName = "--unity-metrics-single_line-height";

		// Token: 0x04000760 RID: 1888
		private const float k_ScrollPageOverlapFactor = 0.1f;

		// Token: 0x04000761 RID: 1889
		internal const float k_UnsetPageSizeValue = -1f;

		// Token: 0x04000762 RID: 1890
		internal const float k_MouseWheelScrollSizeDefaultValue = 18f;

		// Token: 0x04000763 RID: 1891
		internal const float k_MouseWheelScrollSizeUnset = -1f;

		// Token: 0x04000764 RID: 1892
		internal bool m_MouseWheelScrollSizeIsInline;

		// Token: 0x04000765 RID: 1893
		[DontCreateProperty]
		[SerializeField]
		private Vector2 m_ScrollOffset;

		// Token: 0x04000766 RID: 1894
		private float m_HorizontalPageSize;

		// Token: 0x04000767 RID: 1895
		private float m_VerticalPageSize;

		// Token: 0x04000768 RID: 1896
		private float m_MouseWheelScrollSize = 18f;

		// Token: 0x04000769 RID: 1897
		private static readonly float k_DefaultScrollDecelerationRate = 0.135f;

		// Token: 0x0400076A RID: 1898
		private float m_ScrollDecelerationRate = ScrollView.k_DefaultScrollDecelerationRate;

		// Token: 0x0400076B RID: 1899
		private float k_ScaledPixelsPerPointMultiplier = 10f;

		// Token: 0x0400076C RID: 1900
		private float k_TouchScrollInertiaBaseTimeInterval = 0.004167f;

		// Token: 0x0400076D RID: 1901
		private static readonly float k_DefaultElasticity = 0.1f;

		// Token: 0x0400076E RID: 1902
		private float m_Elasticity = ScrollView.k_DefaultElasticity;

		// Token: 0x0400076F RID: 1903
		private ScrollView.TouchScrollBehavior m_TouchScrollBehavior;

		// Token: 0x04000770 RID: 1904
		private ScrollView.NestedInteractionKind m_NestedInteractionKind;

		// Token: 0x04000774 RID: 1908
		private VisualElement m_ContentContainer;

		// Token: 0x04000775 RID: 1909
		private VisualElement m_ContentAndVerticalScrollContainer;

		// Token: 0x04000776 RID: 1910
		private float previousVerticalTouchScrollTimeStamp = 0f;

		// Token: 0x04000777 RID: 1911
		private float previousHorizontalTouchScrollTimeStamp = 0f;

		// Token: 0x04000778 RID: 1912
		private float elapsedTimeSinceLastVerticalTouchScroll = 0f;

		// Token: 0x04000779 RID: 1913
		private float elapsedTimeSinceLastHorizontalTouchScroll = 0f;

		// Token: 0x0400077A RID: 1914
		public static readonly string ussClassName = "unity-scroll-view";

		// Token: 0x0400077B RID: 1915
		public static readonly string viewportUssClassName = ScrollView.ussClassName + "__content-viewport";

		// Token: 0x0400077C RID: 1916
		public static readonly string horizontalVariantViewportUssClassName = ScrollView.viewportUssClassName + "--horizontal";

		// Token: 0x0400077D RID: 1917
		public static readonly string verticalVariantViewportUssClassName = ScrollView.viewportUssClassName + "--vertical";

		// Token: 0x0400077E RID: 1918
		public static readonly string verticalHorizontalVariantViewportUssClassName = ScrollView.viewportUssClassName + "--vertical-horizontal";

		// Token: 0x0400077F RID: 1919
		public static readonly string contentAndVerticalScrollUssClassName = ScrollView.ussClassName + "__content-and-vertical-scroll-container";

		// Token: 0x04000780 RID: 1920
		public static readonly string contentUssClassName = ScrollView.ussClassName + "__content-container";

		// Token: 0x04000781 RID: 1921
		public static readonly string horizontalVariantContentUssClassName = ScrollView.contentUssClassName + "--horizontal";

		// Token: 0x04000782 RID: 1922
		public static readonly string verticalVariantContentUssClassName = ScrollView.contentUssClassName + "--vertical";

		// Token: 0x04000783 RID: 1923
		public static readonly string verticalHorizontalVariantContentUssClassName = ScrollView.contentUssClassName + "--vertical-horizontal";

		// Token: 0x04000784 RID: 1924
		public static readonly string hScrollerUssClassName = ScrollView.ussClassName + "__horizontal-scroller";

		// Token: 0x04000785 RID: 1925
		public static readonly string vScrollerUssClassName = ScrollView.ussClassName + "__vertical-scroller";

		// Token: 0x04000786 RID: 1926
		public static readonly string horizontalVariantUssClassName = ScrollView.ussClassName + "--horizontal";

		// Token: 0x04000787 RID: 1927
		public static readonly string verticalVariantUssClassName = ScrollView.ussClassName + "--vertical";

		// Token: 0x04000788 RID: 1928
		public static readonly string verticalHorizontalVariantUssClassName = ScrollView.ussClassName + "--vertical-horizontal";

		// Token: 0x04000789 RID: 1929
		public static readonly string scrollVariantUssClassName = ScrollView.ussClassName + "--scroll";

		// Token: 0x0400078A RID: 1930
		private ScrollViewMode m_Mode;

		// Token: 0x0400078B RID: 1931
		private IVisualElementScheduledItem m_ScheduledLayoutPassResetItem;

		// Token: 0x0400078C RID: 1932
		private const float k_VelocityLerpTimeFactor = 10f;

		// Token: 0x0400078D RID: 1933
		internal const float ScrollThresholdSquared = 100f;

		// Token: 0x0400078E RID: 1934
		private Vector2 m_StartPosition;

		// Token: 0x0400078F RID: 1935
		private Vector2 m_PointerStartPosition;

		// Token: 0x04000790 RID: 1936
		private Vector2 m_Velocity;

		// Token: 0x04000791 RID: 1937
		private Vector2 m_SpringBackVelocity;

		// Token: 0x04000792 RID: 1938
		private Vector2 m_LowBounds;

		// Token: 0x04000793 RID: 1939
		private Vector2 m_HighBounds;

		// Token: 0x04000794 RID: 1940
		private float m_LastVelocityLerpTime;

		// Token: 0x04000795 RID: 1941
		private bool m_StartedMoving;

		// Token: 0x04000796 RID: 1942
		private bool m_TouchPointerMoveAllowed;

		// Token: 0x04000797 RID: 1943
		private bool m_TouchStoppedVelocity;

		// Token: 0x04000798 RID: 1944
		private VisualElement m_CapturedTarget;

		// Token: 0x04000799 RID: 1945
		private EventCallback<PointerMoveEvent> m_CapturedTargetPointerMoveCallback;

		// Token: 0x0400079A RID: 1946
		private EventCallback<PointerUpEvent> m_CapturedTargetPointerUpCallback;

		// Token: 0x0400079B RID: 1947
		internal IVisualElementScheduledItem m_PostPointerUpAnimation;

		// Token: 0x0200018A RID: 394
		[ExcludeFromDocs]
		[Serializable]
		public new class UxmlSerializedData : VisualElement.UxmlSerializedData
		{
			// Token: 0x06000C95 RID: 3221 RVA: 0x000385D0 File Offset: 0x000367D0
			[Conditional("UNITY_EDITOR")]
			public new static void Register()
			{
				UxmlDescriptionCache.RegisterType(typeof(ScrollView.UxmlSerializedData), new UxmlAttributeNames[]
				{
					new UxmlAttributeNames("mode", "mode", null, Array.Empty<string>()),
					new UxmlAttributeNames("nestedInteractionKind", "nested-interaction-kind", null, Array.Empty<string>()),
					new UxmlAttributeNames("showHorizontal", "show-horizontal-scroller", null, Array.Empty<string>()),
					new UxmlAttributeNames("showVertical", "show-vertical-scroller", null, Array.Empty<string>()),
					new UxmlAttributeNames("horizontalScrollerVisibility", "horizontal-scroller-visibility", null, Array.Empty<string>()),
					new UxmlAttributeNames("verticalScrollerVisibility", "vertical-scroller-visibility", null, Array.Empty<string>()),
					new UxmlAttributeNames("horizontalPageSize", "horizontal-page-size", null, Array.Empty<string>()),
					new UxmlAttributeNames("verticalPageSize", "vertical-page-size", null, Array.Empty<string>()),
					new UxmlAttributeNames("mouseWheelScrollSize", "mouse-wheel-scroll-size", null, Array.Empty<string>()),
					new UxmlAttributeNames("touchScrollBehavior", "touch-scroll-type", null, Array.Empty<string>()),
					new UxmlAttributeNames("scrollDecelerationRate", "scroll-deceleration-rate", null, Array.Empty<string>()),
					new UxmlAttributeNames("elasticity", "elasticity", null, Array.Empty<string>()),
					new UxmlAttributeNames("elasticAnimationIntervalMs", "elastic-animation-interval-ms", null, Array.Empty<string>())
				});
			}

			// Token: 0x06000C96 RID: 3222 RVA: 0x00038765 File Offset: 0x00036965
			public override object CreateInstance()
			{
				return new ScrollView();
			}

			// Token: 0x06000C97 RID: 3223 RVA: 0x0003876C File Offset: 0x0003696C
			public override void Deserialize(object obj)
			{
				base.Deserialize(obj);
				ScrollView scrollView = (ScrollView)obj;
				bool flag = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.mode_UxmlAttributeFlags);
				if (flag)
				{
					scrollView.mode = this.mode;
				}
				bool flag2 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.horizontalScrollerVisibility_UxmlAttributeFlags);
				if (flag2)
				{
					scrollView.horizontalScrollerVisibility = this.horizontalScrollerVisibility;
				}
				else
				{
					bool flag3 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.showHorizontal_UxmlAttributeFlags);
					if (flag3)
					{
						scrollView.showHorizontal = this.showHorizontal;
					}
				}
				bool flag4 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.verticalScrollerVisibility_UxmlAttributeFlags);
				if (flag4)
				{
					scrollView.verticalScrollerVisibility = this.verticalScrollerVisibility;
				}
				else
				{
					bool flag5 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.showVertical_UxmlAttributeFlags);
					if (flag5)
					{
						scrollView.showVertical = this.showVertical;
					}
				}
				bool flag6 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.nestedInteractionKind_UxmlAttributeFlags);
				if (flag6)
				{
					scrollView.nestedInteractionKind = this.nestedInteractionKind;
				}
				bool flag7 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.horizontalPageSize_UxmlAttributeFlags);
				if (flag7)
				{
					scrollView.horizontalPageSize = this.horizontalPageSize;
				}
				bool flag8 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.verticalPageSize_UxmlAttributeFlags);
				if (flag8)
				{
					scrollView.verticalPageSize = this.verticalPageSize;
				}
				bool flag9 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.mouseWheelScrollSize_UxmlAttributeFlags);
				if (flag9)
				{
					scrollView.mouseWheelScrollSize = this.mouseWheelScrollSize;
				}
				bool flag10 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.scrollDecelerationRate_UxmlAttributeFlags);
				if (flag10)
				{
					scrollView.scrollDecelerationRate = this.scrollDecelerationRate;
				}
				bool flag11 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.touchScrollBehavior_UxmlAttributeFlags);
				if (flag11)
				{
					scrollView.touchScrollBehavior = this.touchScrollBehavior;
				}
				bool flag12 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.elasticity_UxmlAttributeFlags);
				if (flag12)
				{
					scrollView.elasticity = this.elasticity;
				}
				bool flag13 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.elasticAnimationIntervalMs_UxmlAttributeFlags);
				if (flag13)
				{
					scrollView.elasticAnimationIntervalMs = this.elasticAnimationIntervalMs;
				}
			}

			// Token: 0x0400079C RID: 1948
			[SerializeField]
			private long elasticAnimationIntervalMs;

			// Token: 0x0400079D RID: 1949
			[SerializeField]
			private ScrollViewMode mode;

			// Token: 0x0400079E RID: 1950
			[SerializeField]
			private ScrollView.NestedInteractionKind nestedInteractionKind;

			// Token: 0x0400079F RID: 1951
			[SerializeField]
			private ScrollerVisibility horizontalScrollerVisibility;

			// Token: 0x040007A0 RID: 1952
			[SerializeField]
			private ScrollerVisibility verticalScrollerVisibility;

			// Token: 0x040007A1 RID: 1953
			[SerializeField]
			private float horizontalPageSize;

			// Token: 0x040007A2 RID: 1954
			[SerializeField]
			private float verticalPageSize;

			// Token: 0x040007A3 RID: 1955
			[SerializeField]
			private float mouseWheelScrollSize;

			// Token: 0x040007A4 RID: 1956
			[UxmlAttribute("touch-scroll-type")]
			[SerializeField]
			private ScrollView.TouchScrollBehavior touchScrollBehavior;

			// Token: 0x040007A5 RID: 1957
			[SerializeField]
			private float scrollDecelerationRate;

			// Token: 0x040007A6 RID: 1958
			[SerializeField]
			private float elasticity;

			// Token: 0x040007A7 RID: 1959
			[SerializeField]
			[HideInInspector]
			[UxmlAttribute("show-horizontal-scroller")]
			private bool showHorizontal;

			// Token: 0x040007A8 RID: 1960
			[HideInInspector]
			[SerializeField]
			[UxmlAttribute("show-vertical-scroller")]
			private bool showVertical;

			// Token: 0x040007A9 RID: 1961
			[UxmlIgnore]
			[SerializeField]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags mode_UxmlAttributeFlags;

			// Token: 0x040007AA RID: 1962
			[SerializeField]
			[UxmlIgnore]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags nestedInteractionKind_UxmlAttributeFlags;

			// Token: 0x040007AB RID: 1963
			[HideInInspector]
			[SerializeField]
			[UxmlIgnore]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags showHorizontal_UxmlAttributeFlags;

			// Token: 0x040007AC RID: 1964
			[SerializeField]
			[UxmlIgnore]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags showVertical_UxmlAttributeFlags;

			// Token: 0x040007AD RID: 1965
			[HideInInspector]
			[UxmlIgnore]
			[SerializeField]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags horizontalScrollerVisibility_UxmlAttributeFlags;

			// Token: 0x040007AE RID: 1966
			[HideInInspector]
			[UxmlIgnore]
			[SerializeField]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags verticalScrollerVisibility_UxmlAttributeFlags;

			// Token: 0x040007AF RID: 1967
			[SerializeField]
			[UxmlIgnore]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags horizontalPageSize_UxmlAttributeFlags;

			// Token: 0x040007B0 RID: 1968
			[HideInInspector]
			[UxmlIgnore]
			[SerializeField]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags verticalPageSize_UxmlAttributeFlags;

			// Token: 0x040007B1 RID: 1969
			[UxmlIgnore]
			[SerializeField]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags mouseWheelScrollSize_UxmlAttributeFlags;

			// Token: 0x040007B2 RID: 1970
			[SerializeField]
			[UxmlIgnore]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags touchScrollBehavior_UxmlAttributeFlags;

			// Token: 0x040007B3 RID: 1971
			[UxmlIgnore]
			[SerializeField]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags scrollDecelerationRate_UxmlAttributeFlags;

			// Token: 0x040007B4 RID: 1972
			[SerializeField]
			[UxmlIgnore]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags elasticity_UxmlAttributeFlags;

			// Token: 0x040007B5 RID: 1973
			[HideInInspector]
			[UxmlIgnore]
			[SerializeField]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags elasticAnimationIntervalMs_UxmlAttributeFlags;
		}

		// Token: 0x0200018B RID: 395
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<ScrollView, ScrollView.UxmlTraits>
		{
		}

		// Token: 0x0200018C RID: 396
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : VisualElement.UxmlTraits
		{
			// Token: 0x06000C9A RID: 3226 RVA: 0x00038918 File Offset: 0x00036B18
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				ScrollView scrollView = (ScrollView)ve;
				scrollView.mode = this.m_ScrollViewMode.GetValueFromBag(bag, cc);
				ScrollerVisibility scrollerVisibility = ScrollerVisibility.Auto;
				bool flag = this.m_HorizontalScrollerVisibility.TryGetValueFromBag(bag, cc, ref scrollerVisibility);
				if (flag)
				{
					scrollView.horizontalScrollerVisibility = scrollerVisibility;
				}
				else
				{
					scrollView.showHorizontal = this.m_ShowHorizontal.GetValueFromBag(bag, cc);
				}
				ScrollerVisibility scrollerVisibility2 = ScrollerVisibility.Auto;
				bool flag2 = this.m_VerticalScrollerVisibility.TryGetValueFromBag(bag, cc, ref scrollerVisibility2);
				if (flag2)
				{
					scrollView.verticalScrollerVisibility = scrollerVisibility2;
				}
				else
				{
					scrollView.showVertical = this.m_ShowVertical.GetValueFromBag(bag, cc);
				}
				scrollView.nestedInteractionKind = this.m_NestedInteractionKind.GetValueFromBag(bag, cc);
				scrollView.horizontalPageSize = this.m_HorizontalPageSize.GetValueFromBag(bag, cc);
				scrollView.verticalPageSize = this.m_VerticalPageSize.GetValueFromBag(bag, cc);
				scrollView.mouseWheelScrollSize = this.m_MouseWheelScrollSize.GetValueFromBag(bag, cc);
				scrollView.scrollDecelerationRate = this.m_ScrollDecelerationRate.GetValueFromBag(bag, cc);
				scrollView.touchScrollBehavior = this.m_TouchScrollBehavior.GetValueFromBag(bag, cc);
				scrollView.elasticity = this.m_Elasticity.GetValueFromBag(bag, cc);
			}

			// Token: 0x040007B6 RID: 1974
			private UxmlEnumAttributeDescription<ScrollViewMode> m_ScrollViewMode = new UxmlEnumAttributeDescription<ScrollViewMode>
			{
				name = "mode",
				defaultValue = ScrollViewMode.Vertical
			};

			// Token: 0x040007B7 RID: 1975
			private UxmlEnumAttributeDescription<ScrollView.NestedInteractionKind> m_NestedInteractionKind = new UxmlEnumAttributeDescription<ScrollView.NestedInteractionKind>
			{
				name = "nested-interaction-kind",
				defaultValue = ScrollView.NestedInteractionKind.Default
			};

			// Token: 0x040007B8 RID: 1976
			private UxmlBoolAttributeDescription m_ShowHorizontal = new UxmlBoolAttributeDescription
			{
				name = "show-horizontal-scroller"
			};

			// Token: 0x040007B9 RID: 1977
			private UxmlBoolAttributeDescription m_ShowVertical = new UxmlBoolAttributeDescription
			{
				name = "show-vertical-scroller"
			};

			// Token: 0x040007BA RID: 1978
			private UxmlEnumAttributeDescription<ScrollerVisibility> m_HorizontalScrollerVisibility = new UxmlEnumAttributeDescription<ScrollerVisibility>
			{
				name = "horizontal-scroller-visibility"
			};

			// Token: 0x040007BB RID: 1979
			private UxmlEnumAttributeDescription<ScrollerVisibility> m_VerticalScrollerVisibility = new UxmlEnumAttributeDescription<ScrollerVisibility>
			{
				name = "vertical-scroller-visibility"
			};

			// Token: 0x040007BC RID: 1980
			private UxmlFloatAttributeDescription m_HorizontalPageSize = new UxmlFloatAttributeDescription
			{
				name = "horizontal-page-size",
				defaultValue = -1f
			};

			// Token: 0x040007BD RID: 1981
			private UxmlFloatAttributeDescription m_VerticalPageSize = new UxmlFloatAttributeDescription
			{
				name = "vertical-page-size",
				defaultValue = -1f
			};

			// Token: 0x040007BE RID: 1982
			private UxmlFloatAttributeDescription m_MouseWheelScrollSize = new UxmlFloatAttributeDescription
			{
				name = "mouse-wheel-scroll-size",
				defaultValue = 18f
			};

			// Token: 0x040007BF RID: 1983
			private UxmlEnumAttributeDescription<ScrollView.TouchScrollBehavior> m_TouchScrollBehavior = new UxmlEnumAttributeDescription<ScrollView.TouchScrollBehavior>
			{
				name = "touch-scroll-type",
				defaultValue = ScrollView.TouchScrollBehavior.Clamped
			};

			// Token: 0x040007C0 RID: 1984
			private UxmlFloatAttributeDescription m_ScrollDecelerationRate = new UxmlFloatAttributeDescription
			{
				name = "scroll-deceleration-rate",
				defaultValue = ScrollView.k_DefaultScrollDecelerationRate
			};

			// Token: 0x040007C1 RID: 1985
			private UxmlFloatAttributeDescription m_Elasticity = new UxmlFloatAttributeDescription
			{
				name = "elasticity",
				defaultValue = ScrollView.k_DefaultElasticity
			};
		}

		// Token: 0x0200018D RID: 397
		public enum TouchScrollBehavior
		{
			// Token: 0x040007C3 RID: 1987
			Unrestricted,
			// Token: 0x040007C4 RID: 1988
			Elastic,
			// Token: 0x040007C5 RID: 1989
			Clamped
		}

		// Token: 0x0200018E RID: 398
		public enum NestedInteractionKind
		{
			// Token: 0x040007C7 RID: 1991
			Default,
			// Token: 0x040007C8 RID: 1992
			StopScrolling,
			// Token: 0x040007C9 RID: 1993
			ForwardScrolling
		}

		// Token: 0x0200018F RID: 399
		internal enum TouchScrollingResult
		{
			// Token: 0x040007CB RID: 1995
			Apply,
			// Token: 0x040007CC RID: 1996
			Forward,
			// Token: 0x040007CD RID: 1997
			Block
		}
	}
}
