using System;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Properties;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine.UIElements
{
	// Token: 0x02000118 RID: 280
	public abstract class BaseField<TValueType> : BindableElement, INotifyValueChanged<TValueType>, IMixedValueSupport, IPrefixLabel, IEditableElement
	{
		// Token: 0x14000026 RID: 38
		// (add) Token: 0x060008BA RID: 2234 RVA: 0x000262FC File Offset: 0x000244FC
		// (remove) Token: 0x060008BB RID: 2235 RVA: 0x00026334 File Offset: 0x00024534
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal event Action viewDataRestored;

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060008BC RID: 2236 RVA: 0x0002636C File Offset: 0x0002456C
		// (set) Token: 0x060008BD RID: 2237 RVA: 0x00026384 File Offset: 0x00024584
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal VisualElement visualInput
		{
			get
			{
				return this.m_VisualInput;
			}
			set
			{
				bool flag = this.m_VisualInput != null;
				if (flag)
				{
					bool flag2 = this.m_VisualInput.parent == this;
					if (flag2)
					{
						this.m_VisualInput.RemoveFromHierarchy();
					}
					this.m_VisualInput = null;
				}
				bool flag3 = value != null;
				if (flag3)
				{
					this.m_VisualInput = value;
				}
				else
				{
					this.m_VisualInput = new VisualElement
					{
						pickingMode = PickingMode.Ignore
					};
				}
				this.m_VisualInput.focusable = true;
				this.m_VisualInput.AddToClassList(BaseField<TValueType>.inputUssClassName);
				base.Add(this.m_VisualInput);
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060008BE RID: 2238 RVA: 0x0002641C File Offset: 0x0002461C
		// (set) Token: 0x060008BF RID: 2239 RVA: 0x00026434 File Offset: 0x00024634
		protected TValueType rawValue
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				this.m_Value = value;
			}
		}

		// Token: 0x14000027 RID: 39
		// (add) Token: 0x060008C0 RID: 2240 RVA: 0x00026440 File Offset: 0x00024640
		// (remove) Token: 0x060008C1 RID: 2241 RVA: 0x00026478 File Offset: 0x00024678
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal event Func<TValueType, TValueType> onValidateValue;

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060008C2 RID: 2242 RVA: 0x000264AD File Offset: 0x000246AD
		// (set) Token: 0x060008C3 RID: 2243 RVA: 0x000264B5 File Offset: 0x000246B5
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal DispatchMode dispatchMode { get; set; } = DispatchMode.Default;

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060008C4 RID: 2244 RVA: 0x000264C0 File Offset: 0x000246C0
		// (set) Token: 0x060008C5 RID: 2245 RVA: 0x000264D8 File Offset: 0x000246D8
		[CreateProperty]
		public virtual TValueType value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				bool flag = !this.EqualsCurrentValue(value) || this.showMixedValue;
				if (flag)
				{
					TValueType value2 = this.m_Value;
					this.SetValueWithoutNotify(value);
					this.showMixedValue = false;
					bool flag2 = base.panel != null;
					if (flag2)
					{
						using (ChangeEvent<TValueType> pooled = ChangeEvent<TValueType>.GetPooled(value2, this.m_Value))
						{
							pooled.elementTarget = this;
							this.SendEvent(pooled, this.dispatchMode);
						}
						base.NotifyPropertyChanged(in BaseField<TValueType>.valueProperty);
					}
				}
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060008C6 RID: 2246 RVA: 0x00026574 File Offset: 0x00024774
		// (set) Token: 0x060008C7 RID: 2247 RVA: 0x0002657C File Offset: 0x0002477C
		public Label labelElement { get; private set; }

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060008C8 RID: 2248 RVA: 0x00026588 File Offset: 0x00024788
		// (set) Token: 0x060008C9 RID: 2249 RVA: 0x000265A8 File Offset: 0x000247A8
		[CreateProperty]
		public string label
		{
			get
			{
				return this.labelElement.text;
			}
			set
			{
				bool flag = this.labelElement.text != value;
				if (flag)
				{
					this.labelElement.text = value;
					bool flag2 = string.IsNullOrEmpty(this.labelElement.text);
					if (flag2)
					{
						base.AddToClassList(BaseField<TValueType>.noLabelVariantUssClassName);
						this.labelElement.RemoveFromHierarchy();
					}
					else
					{
						bool flag3 = !base.Contains(this.labelElement);
						if (flag3)
						{
							base.hierarchy.Insert(0, this.labelElement);
							base.RemoveFromClassList(BaseField<TValueType>.noLabelVariantUssClassName);
						}
					}
					base.NotifyPropertyChanged(in BaseField<TValueType>.labelProperty);
				}
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060008CA RID: 2250 RVA: 0x00026652 File Offset: 0x00024852
		// (set) Token: 0x060008CB RID: 2251 RVA: 0x0002665C File Offset: 0x0002485C
		[CreateProperty]
		public bool showMixedValue
		{
			get
			{
				return this.m_ShowMixedValue;
			}
			set
			{
				bool flag = value == this.m_ShowMixedValue;
				if (!flag)
				{
					bool flag2 = value && !this.canSwitchToMixedValue;
					if (!flag2)
					{
						this.m_ShowMixedValue = value;
						this.UpdateMixedValueContent();
						base.NotifyPropertyChanged(in BaseField<TValueType>.showMixedValueProperty);
					}
				}
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060008CC RID: 2252 RVA: 0x0000E724 File Offset: 0x0000C924
		private protected virtual bool canSwitchToMixedValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060008CD RID: 2253 RVA: 0x000266AC File Offset: 0x000248AC
		protected Label mixedValueLabel
		{
			get
			{
				bool flag = this.m_MixedValueLabel == null;
				if (flag)
				{
					this.m_MixedValueLabel = new Label(BaseField<TValueType>.mixedValueString)
					{
						focusable = true,
						tabIndex = -1
					};
					this.m_MixedValueLabel.AddToClassList(BaseField<TValueType>.labelUssClassName);
					this.m_MixedValueLabel.AddToClassList(BaseField<TValueType>.mixedValueLabelUssClassName);
				}
				return this.m_MixedValueLabel;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060008CE RID: 2254 RVA: 0x00026715 File Offset: 0x00024915
		// (set) Token: 0x060008CF RID: 2255 RVA: 0x0002671D File Offset: 0x0002491D
		Action IEditableElement.editingStarted { get; set; }

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060008D0 RID: 2256 RVA: 0x00026726 File Offset: 0x00024926
		// (set) Token: 0x060008D1 RID: 2257 RVA: 0x0002672E File Offset: 0x0002492E
		Action IEditableElement.editingEnded { get; set; }

		// Token: 0x060008D2 RID: 2258 RVA: 0x00026738 File Offset: 0x00024938
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal BaseField(string label)
		{
			base.isCompositeRoot = true;
			this.focusable = true;
			base.tabIndex = 0;
			base.excludeFromFocusRing = true;
			base.delegatesFocus = true;
			base.AddToClassList(BaseField<TValueType>.ussClassName);
			this.labelElement = new Label
			{
				focusable = true,
				tabIndex = -1
			};
			this.labelElement.AddToClassList(BaseField<TValueType>.labelUssClassName);
			bool flag = label != null;
			if (flag)
			{
				this.label = label;
			}
			else
			{
				base.AddToClassList(BaseField<TValueType>.noLabelVariantUssClassName);
			}
			base.RegisterCallback<AttachToPanelEvent>(new EventCallback<AttachToPanelEvent>(this.OnAttachToPanel), TrickleDown.NoTrickleDown);
			base.RegisterCallback<DetachFromPanelEvent>(new EventCallback<DetachFromPanelEvent>(this.OnDetachFromPanel), TrickleDown.NoTrickleDown);
			this.m_VisualInput = null;
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x00026806 File Offset: 0x00024A06
		protected BaseField(string label, VisualElement visualInput)
			: this(label)
		{
			this.visualInput = visualInput;
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x00026819 File Offset: 0x00024A19
		internal virtual bool EqualsCurrentValue(TValueType value)
		{
			return EqualityComparer<TValueType>.Default.Equals(this.m_Value, value);
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x0002682C File Offset: 0x00024A2C
		private void OnAttachToPanel(AttachToPanelEvent e)
		{
			this.RegisterEditingCallbacks();
			bool flag = e.destinationPanel == null;
			if (!flag)
			{
				bool flag2 = e.destinationPanel.contextType == ContextType.Player;
				if (!flag2)
				{
					this.m_CachedInspectorElement = null;
					this.m_CachedContextWidthElement = null;
					for (VisualElement visualElement = base.parent; visualElement != null; visualElement = visualElement.parent)
					{
						bool flag3 = visualElement.ClassListContains("unity-inspector-element");
						if (flag3)
						{
							this.m_CachedInspectorElement = visualElement;
						}
						bool flag4 = visualElement.ClassListContains("unity-inspector-main-container");
						if (flag4)
						{
							this.m_CachedContextWidthElement = visualElement;
							break;
						}
					}
					bool flag5 = this.m_CachedInspectorElement == null;
					if (flag5)
					{
						base.RemoveFromClassList(BaseField<TValueType>.inspectorFieldUssClassName);
					}
					else
					{
						this.m_LabelWidthRatio = 0.45f;
						this.m_LabelExtraPadding = 37f;
						this.m_LabelBaseMinWidth = 123f;
						base.RegisterCallback<CustomStyleResolvedEvent>(new EventCallback<CustomStyleResolvedEvent>(this.OnCustomStyleResolved), TrickleDown.NoTrickleDown);
						base.AddToClassList(BaseField<TValueType>.inspectorFieldUssClassName);
						base.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnInspectorFieldGeometryChanged), TrickleDown.NoTrickleDown);
					}
				}
			}
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x0002693C File Offset: 0x00024B3C
		private void OnDetachFromPanel(DetachFromPanelEvent e)
		{
			this.UnregisterEditingCallbacks();
			this.onValidateValue = null;
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x0002694D File Offset: 0x00024B4D
		internal virtual void RegisterEditingCallbacks()
		{
			base.RegisterCallback<FocusInEvent>(new EventCallback<FocusInEvent>(this.StartEditing), TrickleDown.NoTrickleDown);
			base.RegisterCallback<FocusOutEvent>(new EventCallback<FocusOutEvent>(this.EndEditing), TrickleDown.NoTrickleDown);
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x00026978 File Offset: 0x00024B78
		internal virtual void UnregisterEditingCallbacks()
		{
			base.UnregisterCallback<FocusInEvent>(new EventCallback<FocusInEvent>(this.StartEditing), TrickleDown.NoTrickleDown);
			base.UnregisterCallback<FocusOutEvent>(new EventCallback<FocusOutEvent>(this.EndEditing), TrickleDown.NoTrickleDown);
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x000269A3 File Offset: 0x00024BA3
		internal void StartEditing(EventBase e)
		{
			Action editingStarted = ((IEditableElement)this).editingStarted;
			if (editingStarted != null)
			{
				editingStarted();
			}
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x000269B8 File Offset: 0x00024BB8
		internal void EndEditing(EventBase e)
		{
			Action editingEnded = ((IEditableElement)this).editingEnded;
			if (editingEnded != null)
			{
				editingEnded();
			}
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x000269D0 File Offset: 0x00024BD0
		private void OnCustomStyleResolved(CustomStyleResolvedEvent evt)
		{
			float num;
			bool flag = evt.customStyle.TryGetValue(BaseField<TValueType>.s_LabelWidthRatioProperty, out num);
			if (flag)
			{
				this.m_LabelWidthRatio = num;
			}
			float num2;
			bool flag2 = evt.customStyle.TryGetValue(BaseField<TValueType>.s_LabelExtraPaddingProperty, out num2);
			if (flag2)
			{
				this.m_LabelExtraPadding = num2;
			}
			float num3;
			bool flag3 = evt.customStyle.TryGetValue(BaseField<TValueType>.s_LabelBaseMinWidthProperty, out num3);
			if (flag3)
			{
				this.m_LabelBaseMinWidth = num3;
			}
			this.AlignLabel();
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x00026A46 File Offset: 0x00024C46
		private void OnInspectorFieldGeometryChanged(GeometryChangedEvent e)
		{
			this.AlignLabel();
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x00026A50 File Offset: 0x00024C50
		private void AlignLabel()
		{
			bool flag = !base.ClassListContains(BaseField<TValueType>.alignedFieldUssClassName) || this.m_CachedInspectorElement == null;
			if (!flag)
			{
				float num = this.m_LabelExtraPadding;
				float num2 = base.worldBound.x - this.m_CachedInspectorElement.worldBound.x - this.m_CachedInspectorElement.resolvedStyle.paddingLeft;
				num += num2;
				num += base.resolvedStyle.paddingLeft;
				float num3 = this.m_LabelBaseMinWidth - num2 - base.resolvedStyle.paddingLeft;
				VisualElement visualElement = this.m_CachedContextWidthElement ?? this.m_CachedInspectorElement;
				this.labelElement.style.minWidth = Mathf.Max(num3, 0f);
				float num4 = Mathf.Ceil(visualElement.resolvedStyle.width * this.m_LabelWidthRatio) - num;
				bool flag2 = Mathf.Abs(this.labelElement.resolvedStyle.width - num4) > 1E-30f;
				if (flag2)
				{
					this.labelElement.style.width = Mathf.Max(0f, num4);
				}
			}
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x00026B80 File Offset: 0x00024D80
		private Rect ComputeTooltipRect()
		{
			bool flag = !string.IsNullOrEmpty(this.label);
			Rect rect;
			if (flag)
			{
				rect = (string.IsNullOrEmpty(this.labelElement.tooltip) ? this.labelElement.worldBound : base.worldBound);
			}
			else
			{
				rect = base.worldBound;
			}
			return rect;
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x00026BD3 File Offset: 0x00024DD3
		internal override Rect GetTooltipRect()
		{
			return this.ComputeTooltipRect();
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x00026BDC File Offset: 0x00024DDC
		internal TValueType ValidatedValue(TValueType value)
		{
			bool flag = this.onValidateValue != null;
			TValueType tvalueType;
			if (flag)
			{
				tvalueType = this.onValidateValue(value);
			}
			else
			{
				tvalueType = value;
			}
			return tvalueType;
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x00026C0C File Offset: 0x00024E0C
		[EventInterest(new Type[] { typeof(TooltipEvent) })]
		protected override void HandleEventBubbleUp(EventBase evt)
		{
			TooltipEvent tooltipEvent = evt as TooltipEvent;
			bool flag = tooltipEvent == null;
			if (flag)
			{
				base.HandleEventBubbleUp(evt);
			}
			else
			{
				bool flag2;
				if (tooltipEvent.elementTarget != this.labelElement)
				{
					Label labelElement = this.labelElement;
					if (string.IsNullOrEmpty((labelElement != null) ? labelElement.tooltip : null))
					{
						flag2 = string.IsNullOrEmpty(this.label);
						goto IL_0053;
					}
				}
				flag2 = true;
				IL_0053:
				bool flag3 = flag2;
				if (flag3)
				{
					tooltipEvent.rect = this.ComputeTooltipRect();
				}
				else
				{
					tooltipEvent.StopImmediatePropagation();
				}
			}
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x00026C86 File Offset: 0x00024E86
		protected virtual void UpdateMixedValueContent()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x00026C90 File Offset: 0x00024E90
		public virtual void SetValueWithoutNotify(TValueType newValue)
		{
			bool skipValidation = this.m_SkipValidation;
			if (skipValidation)
			{
				this.m_Value = newValue;
			}
			else
			{
				this.m_Value = this.ValidatedValue(newValue);
			}
			bool flag = !string.IsNullOrEmpty(base.viewDataKey);
			if (flag)
			{
				base.SaveViewData();
			}
			base.MarkDirtyRepaint();
			bool showMixedValue = this.showMixedValue;
			if (showMixedValue)
			{
				this.UpdateMixedValueContent();
			}
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x00026CF3 File Offset: 0x00024EF3
		internal void SetValueWithoutValidation(TValueType newValue)
		{
			this.m_SkipValidation = true;
			this.value = newValue;
			this.m_SkipValidation = false;
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x00026D0C File Offset: 0x00024F0C
		internal override void OnViewDataReady()
		{
			base.OnViewDataReady();
			bool flag = this.m_VisualInput != null;
			if (flag)
			{
				string fullHierarchicalViewDataKey = base.GetFullHierarchicalViewDataKey();
				TValueType value = this.m_Value;
				base.OverwriteFromViewData(this, fullHierarchicalViewDataKey);
				Action action = this.viewDataRestored;
				if (action != null)
				{
					action();
				}
				bool flag2 = !EqualityComparer<TValueType>.Default.Equals(value, this.m_Value);
				if (flag2)
				{
					using (ChangeEvent<TValueType> pooled = ChangeEvent<TValueType>.GetPooled(value, this.m_Value))
					{
						pooled.elementTarget = this;
						this.SetValueWithoutNotify(this.m_Value);
						this.SendEvent(pooled);
					}
				}
			}
		}

		// Token: 0x040004EA RID: 1258
		internal static readonly BindingId valueProperty = "value";

		// Token: 0x040004EB RID: 1259
		internal static readonly BindingId labelProperty = "label";

		// Token: 0x040004EC RID: 1260
		internal static readonly BindingId showMixedValueProperty = "showMixedValue";

		// Token: 0x040004ED RID: 1261
		public static readonly string ussClassName = "unity-base-field";

		// Token: 0x040004EE RID: 1262
		public static readonly string labelUssClassName = BaseField<TValueType>.ussClassName + "__label";

		// Token: 0x040004EF RID: 1263
		public static readonly string inputUssClassName = BaseField<TValueType>.ussClassName + "__input";

		// Token: 0x040004F0 RID: 1264
		public static readonly string noLabelVariantUssClassName = BaseField<TValueType>.ussClassName + "--no-label";

		// Token: 0x040004F1 RID: 1265
		public static readonly string labelDraggerVariantUssClassName = BaseField<TValueType>.labelUssClassName + "--with-dragger";

		// Token: 0x040004F2 RID: 1266
		public static readonly string mixedValueLabelUssClassName = BaseField<TValueType>.labelUssClassName + "--mixed-value";

		// Token: 0x040004F3 RID: 1267
		public static readonly string alignedFieldUssClassName = BaseField<TValueType>.ussClassName + "__aligned";

		// Token: 0x040004F4 RID: 1268
		private static readonly string inspectorFieldUssClassName = BaseField<TValueType>.ussClassName + "__inspector-field";

		// Token: 0x040004F5 RID: 1269
		protected internal static readonly string mixedValueString = "—";

		// Token: 0x040004F6 RID: 1270
		protected internal static readonly PropertyName serializedPropertyCopyName = "SerializedPropertyCopyName";

		// Token: 0x040004F7 RID: 1271
		private static CustomStyleProperty<float> s_LabelWidthRatioProperty = new CustomStyleProperty<float>("--unity-property-field-label-width-ratio");

		// Token: 0x040004F8 RID: 1272
		private static CustomStyleProperty<float> s_LabelExtraPaddingProperty = new CustomStyleProperty<float>("--unity-property-field-label-extra-padding");

		// Token: 0x040004F9 RID: 1273
		private static CustomStyleProperty<float> s_LabelBaseMinWidthProperty = new CustomStyleProperty<float>("--unity-property-field-label-base-min-width");

		// Token: 0x040004FA RID: 1274
		private float m_LabelWidthRatio;

		// Token: 0x040004FB RID: 1275
		private float m_LabelExtraPadding;

		// Token: 0x040004FC RID: 1276
		private float m_LabelBaseMinWidth;

		// Token: 0x040004FD RID: 1277
		private VisualElement m_VisualInput;

		// Token: 0x040004FE RID: 1278
		internal Action<ExpressionEvaluator.Expression> expressionEvaluated;

		// Token: 0x04000500 RID: 1280
		[SerializeField]
		[DontCreateProperty]
		private TValueType m_Value;

		// Token: 0x04000504 RID: 1284
		private bool m_ShowMixedValue;

		// Token: 0x04000505 RID: 1285
		private Label m_MixedValueLabel;

		// Token: 0x04000506 RID: 1286
		private bool m_SkipValidation;

		// Token: 0x04000507 RID: 1287
		private VisualElement m_CachedContextWidthElement;

		// Token: 0x04000508 RID: 1288
		private VisualElement m_CachedInspectorElement;

		// Token: 0x02000119 RID: 281
		[ExcludeFromDocs]
		[Serializable]
		public new abstract class UxmlSerializedData : BindableElement.UxmlSerializedData
		{
			// Token: 0x060008E7 RID: 2279 RVA: 0x00026EDC File Offset: 0x000250DC
			public new static void Register()
			{
				UxmlDescriptionCache.RegisterType(typeof(BaseField<TValueType>.UxmlSerializedData), new UxmlAttributeNames[]
				{
					new UxmlAttributeNames("label", "label", null, Array.Empty<string>()),
					new UxmlAttributeNames("value", "value", null, Array.Empty<string>())
				});
			}

			// Token: 0x17000154 RID: 340
			// (get) Token: 0x060008E9 RID: 2281 RVA: 0x00026F41 File Offset: 0x00025141
			// (set) Token: 0x060008E8 RID: 2280 RVA: 0x00026F38 File Offset: 0x00025138
			internal TValueType Value
			{
				get
				{
					return this.value;
				}
				set
				{
					this.value = value;
				}
			}

			// Token: 0x060008EA RID: 2282 RVA: 0x00026F4C File Offset: 0x0002514C
			public override void Deserialize(object obj)
			{
				base.Deserialize(obj);
				BaseField<TValueType> baseField = (BaseField<TValueType>)obj;
				bool flag = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.label_UxmlAttributeFlags);
				if (flag)
				{
					baseField.label = this.label;
				}
				bool flag2 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.value_UxmlAttributeFlags);
				if (flag2)
				{
					baseField.SetValueWithoutNotify(this.value);
				}
			}

			// Token: 0x0400050B RID: 1291
			[MultilineTextField]
			[SerializeField]
			private string label;

			// Token: 0x0400050C RID: 1292
			[SerializeField]
			[UxmlIgnore]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags label_UxmlAttributeFlags;

			// Token: 0x0400050D RID: 1293
			[SerializeField]
			private TValueType value;

			// Token: 0x0400050E RID: 1294
			[HideInInspector]
			[SerializeField]
			[UxmlIgnore]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags value_UxmlAttributeFlags;
		}

		// Token: 0x0200011A RID: 282
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : BindableElement.UxmlTraits
		{
			// Token: 0x060008EC RID: 2284 RVA: 0x00026FA1 File Offset: 0x000251A1
			public UxmlTraits()
			{
				base.focusIndex.defaultValue = 0;
				base.focusable.defaultValue = true;
			}

			// Token: 0x060008ED RID: 2285 RVA: 0x00026FDC File Offset: 0x000251DC
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				((BaseField<TValueType>)ve).label = this.m_Label.GetValueFromBag(bag, cc);
			}

			// Token: 0x0400050F RID: 1295
			private UxmlStringAttributeDescription m_Label = new UxmlStringAttributeDescription
			{
				name = "label"
			};
		}
	}
}
