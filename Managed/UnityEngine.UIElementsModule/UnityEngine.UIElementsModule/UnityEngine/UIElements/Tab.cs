using System;
using System.Diagnostics;
using Unity.Properties;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine.UIElements
{
	// Token: 0x02000199 RID: 409
	public class Tab : VisualElement
	{
		// Token: 0x1400003D RID: 61
		// (add) Token: 0x06000CC4 RID: 3268 RVA: 0x00039914 File Offset: 0x00037B14
		// (remove) Token: 0x06000CC5 RID: 3269 RVA: 0x0003994C File Offset: 0x00037B4C
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<Tab> selected;

		// Token: 0x1400003E RID: 62
		// (add) Token: 0x06000CC6 RID: 3270 RVA: 0x00039984 File Offset: 0x00037B84
		// (remove) Token: 0x06000CC7 RID: 3271 RVA: 0x000399BC File Offset: 0x00037BBC
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Func<bool> closing;

		// Token: 0x1400003F RID: 63
		// (add) Token: 0x06000CC8 RID: 3272 RVA: 0x000399F4 File Offset: 0x00037BF4
		// (remove) Token: 0x06000CC9 RID: 3273 RVA: 0x00039A2C File Offset: 0x00037C2C
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<Tab> closed;

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000CCA RID: 3274 RVA: 0x00039A64 File Offset: 0x00037C64
		// (set) Token: 0x06000CCB RID: 3275 RVA: 0x00039A7F File Offset: 0x00037C7F
		private Object iconImageReference
		{
			get
			{
				return this.iconImage.GetSelectedImage();
			}
			set
			{
				this.iconImage = Background.FromObject(value);
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000CCC RID: 3276 RVA: 0x00039A8E File Offset: 0x00037C8E
		internal Label headerLabel
		{
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			get
			{
				return this.m_TabHeaderLabel;
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000CCD RID: 3277 RVA: 0x00039A96 File Offset: 0x00037C96
		public VisualElement tabHeader
		{
			get
			{
				return this.m_TabHeader;
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000CCE RID: 3278 RVA: 0x00039A9E File Offset: 0x00037C9E
		internal TabDragger dragger { get; }

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000CCF RID: 3279 RVA: 0x00039AA6 File Offset: 0x00037CA6
		// (set) Token: 0x06000CD0 RID: 3280 RVA: 0x00039AAE File Offset: 0x00037CAE
		internal int index { get; set; }

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000CD1 RID: 3281 RVA: 0x00039AB7 File Offset: 0x00037CB7
		// (set) Token: 0x06000CD2 RID: 3282 RVA: 0x00039AC0 File Offset: 0x00037CC0
		[CreateProperty]
		public string label
		{
			get
			{
				return this.m_Label;
			}
			set
			{
				bool flag = string.CompareOrdinal(value, this.m_Label) == 0;
				if (!flag)
				{
					this.m_TabHeaderLabel.text = value;
					this.m_TabHeaderLabel.EnableInClassList(Tab.tabHeaderEmptyLabeUssClassName, string.IsNullOrEmpty(value));
					this.m_TabHeaderImage.EnableInClassList(Tab.tabHeaderStandaloneImageUssClassName, string.IsNullOrEmpty(value));
					this.m_Label = value;
					base.NotifyPropertyChanged(in Tab.labelProperty);
				}
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000CD3 RID: 3283 RVA: 0x00039B31 File Offset: 0x00037D31
		// (set) Token: 0x06000CD4 RID: 3284 RVA: 0x00039B3C File Offset: 0x00037D3C
		[CreateProperty]
		public Background iconImage
		{
			get
			{
				return this.m_IconImage;
			}
			set
			{
				bool flag = value == this.m_IconImage;
				if (!flag)
				{
					bool flag2 = value.IsEmpty();
					if (flag2)
					{
						this.m_TabHeaderImage.image = null;
						this.m_TabHeaderImage.sprite = null;
						this.m_TabHeaderImage.vectorImage = null;
						this.m_TabHeaderImage.AddToClassList(Tab.tabHeaderEmptyImageUssClassName);
						this.m_TabHeaderImage.RemoveFromClassList(Tab.tabHeaderStandaloneImageUssClassName);
						this.m_IconImage = value;
						base.NotifyPropertyChanged(in Tab.iconImageProperty);
					}
					else
					{
						bool flag3 = value.texture;
						if (flag3)
						{
							this.m_TabHeaderImage.image = value.texture;
						}
						else
						{
							bool flag4 = value.sprite;
							if (flag4)
							{
								this.m_TabHeaderImage.sprite = value.sprite;
							}
							else
							{
								bool flag5 = value.renderTexture;
								if (flag5)
								{
									this.m_TabHeaderImage.image = value.renderTexture;
								}
								else
								{
									this.m_TabHeaderImage.vectorImage = value.vectorImage;
								}
							}
						}
						this.m_TabHeaderImage.RemoveFromClassList(Tab.tabHeaderEmptyImageUssClassName);
						this.m_TabHeaderImage.EnableInClassList(Tab.tabHeaderStandaloneImageUssClassName, string.IsNullOrEmpty(this.m_Label));
						this.m_IconImage = value;
						base.NotifyPropertyChanged(in Tab.iconImageProperty);
					}
				}
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000CD5 RID: 3285 RVA: 0x00039C90 File Offset: 0x00037E90
		// (set) Token: 0x06000CD6 RID: 3286 RVA: 0x00039C98 File Offset: 0x00037E98
		[CreateProperty]
		public bool closeable
		{
			get
			{
				return this.m_Closeable;
			}
			set
			{
				bool flag = this.m_Closeable == value;
				if (!flag)
				{
					this.m_Closeable = value;
					this.m_TabHeader.EnableInClassList(Tab.closeableUssClassName, value);
					this.EnableTabCloseButton(value);
					base.NotifyPropertyChanged(in Tab.closeableProperty);
				}
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000CD7 RID: 3287 RVA: 0x00039CE2 File Offset: 0x00037EE2
		public override VisualElement contentContainer
		{
			get
			{
				return this.m_ContentContainer;
			}
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x00039CEA File Offset: 0x00037EEA
		public Tab()
			: this(null, null)
		{
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x00039CFB File Offset: 0x00037EFB
		public Tab(string label)
			: this(label, null)
		{
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x00039D0C File Offset: 0x00037F0C
		public Tab(Background iconImage)
			: this(null, iconImage)
		{
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x00039D18 File Offset: 0x00037F18
		public Tab(string label, Background iconImage)
		{
			base.AddToClassList(Tab.ussClassName);
			this.m_TabHeader = new VisualElement
			{
				classList = { Tab.tabHeaderUssClassName },
				name = Tab.tabHeaderUssClassName
			};
			this.m_DragHandle = new VisualElement
			{
				name = Tab.reorderableItemHandleUssClassName,
				classList = { Tab.reorderableItemHandleUssClassName }
			};
			this.m_DragHandle.AddToClassList(Tab.reorderableItemHandleUssClassName);
			this.m_DragHandle.Add(new VisualElement
			{
				name = Tab.reorderableItemHandleBarUssClassName,
				classList = 
				{
					Tab.reorderableItemHandleBarUssClassName,
					Tab.reorderableItemHandleBarUssClassName + "--left"
				}
			});
			this.m_DragHandle.Add(new VisualElement
			{
				name = Tab.reorderableItemHandleBarUssClassName,
				classList = { Tab.reorderableItemHandleBarUssClassName }
			});
			this.m_TabHeaderImage = new Image
			{
				name = Tab.tabHeaderImageUssClassName,
				classList = 
				{
					Tab.tabHeaderImageUssClassName,
					Tab.tabHeaderEmptyImageUssClassName
				}
			};
			this.m_TabHeader.Add(this.m_TabHeaderImage);
			this.m_TabHeaderLabel = new Label
			{
				name = Tab.tabHeaderLabelUssClassName,
				classList = { Tab.tabHeaderLabelUssClassName }
			};
			this.m_TabHeader.Add(this.m_TabHeaderLabel);
			this.m_TabHeader.RegisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnTabClicked), TrickleDown.NoTrickleDown);
			this.m_TabHeader.Add(new VisualElement
			{
				name = Tab.tabHeaderUnderlineUssClassName,
				classList = { Tab.tabHeaderUnderlineUssClassName }
			});
			this.m_CloseButton = new VisualElement
			{
				name = Tab.closeButtonUssClassName,
				classList = { Tab.closeButtonUssClassName }
			};
			this.m_CloseButton.RegisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnCloseButtonClicked), TrickleDown.NoTrickleDown);
			base.hierarchy.Add(this.m_TabHeader);
			this.m_ContentContainer = new VisualElement
			{
				name = Tab.contentUssClassName,
				classList = { Tab.contentUssClassName },
				userData = this.m_TabHeader
			};
			base.hierarchy.Add(this.m_ContentContainer);
			this.label = label;
			this.iconImage = iconImage;
			this.m_DragHandle.AddManipulator(this.dragger = new TabDragger());
			this.m_TabHeader.RegisterCallback<TooltipEvent>(new EventCallback<TooltipEvent>(this.UpdateTooltip), TrickleDown.NoTrickleDown);
			base.RegisterCallback<TooltipEvent>(delegate(TooltipEvent evt)
			{
				evt.StopImmediatePropagation();
			}, TrickleDown.NoTrickleDown);
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x00039FE8 File Offset: 0x000381E8
		private void UpdateTooltip(TooltipEvent evt)
		{
			VisualElement visualElement = evt.currentTarget as VisualElement;
			bool flag = visualElement != null && !string.IsNullOrEmpty(base.tooltip);
			if (flag)
			{
				evt.rect = visualElement.GetTooltipRect();
				evt.tooltip = base.tooltip;
				evt.StopImmediatePropagation();
			}
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x0003A03D File Offset: 0x0003823D
		private void AddDragHandles()
		{
			this.m_TabHeader.Insert(0, this.m_DragHandle);
		}

		// Token: 0x06000CDE RID: 3294 RVA: 0x0003A054 File Offset: 0x00038254
		private void RemoveDragHandles()
		{
			bool flag = this.m_TabHeader.Contains(this.m_DragHandle);
			if (flag)
			{
				this.m_TabHeader.Remove(this.m_DragHandle);
			}
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x0003A08C File Offset: 0x0003828C
		internal void EnableTabDragHandles(bool enable)
		{
			if (enable)
			{
				this.AddDragHandles();
			}
			else
			{
				this.RemoveDragHandles();
			}
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x0003A0AF File Offset: 0x000382AF
		private void AddCloseButton()
		{
			this.m_TabHeader.Add(this.m_CloseButton);
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x0003A0C4 File Offset: 0x000382C4
		private void RemoveCloseButton()
		{
			bool flag = this.m_TabHeader.Contains(this.m_CloseButton);
			if (flag)
			{
				this.m_TabHeader.Remove(this.m_CloseButton);
			}
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x0003A0FC File Offset: 0x000382FC
		internal void EnableTabCloseButton(bool enable)
		{
			if (enable)
			{
				this.AddCloseButton();
			}
			else
			{
				this.RemoveCloseButton();
			}
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x0003A11F File Offset: 0x0003831F
		internal void SetActive()
		{
			this.m_TabHeader.pseudoStates |= PseudoStates.Checked;
			base.pseudoStates |= PseudoStates.Checked;
		}

		// Token: 0x06000CE4 RID: 3300 RVA: 0x0003A145 File Offset: 0x00038345
		internal void SetInactive()
		{
			this.m_TabHeader.pseudoStates &= ~PseudoStates.Checked;
			base.pseudoStates &= ~PseudoStates.Checked;
		}

		// Token: 0x06000CE5 RID: 3301 RVA: 0x0003A16D File Offset: 0x0003836D
		private void OnTabClicked(PointerDownEvent _)
		{
			Action<Tab> action = this.selected;
			if (action != null)
			{
				action(this);
			}
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x0003A184 File Offset: 0x00038384
		private void OnCloseButtonClicked(PointerDownEvent evt)
		{
			Func<bool> func = this.closing;
			bool flag = func == null || func();
			bool flag2 = flag;
			if (flag2)
			{
				base.RemoveFromHierarchy();
				Action<Tab> action = this.closed;
				if (action != null)
				{
					action(this);
				}
			}
			evt.StopPropagation();
		}

		// Token: 0x040007FC RID: 2044
		internal static readonly BindingId labelProperty = "label";

		// Token: 0x040007FD RID: 2045
		internal static readonly BindingId iconImageProperty = "iconImage";

		// Token: 0x040007FE RID: 2046
		internal static readonly BindingId closeableProperty = "closeable";

		// Token: 0x040007FF RID: 2047
		public static readonly string ussClassName = "unity-tab";

		// Token: 0x04000800 RID: 2048
		public static readonly string tabHeaderUssClassName = Tab.ussClassName + "__header";

		// Token: 0x04000801 RID: 2049
		public static readonly string tabHeaderImageUssClassName = Tab.tabHeaderUssClassName + "-image";

		// Token: 0x04000802 RID: 2050
		public static readonly string tabHeaderEmptyImageUssClassName = Tab.tabHeaderImageUssClassName + "--empty";

		// Token: 0x04000803 RID: 2051
		public static readonly string tabHeaderStandaloneImageUssClassName = Tab.tabHeaderImageUssClassName + "--standalone";

		// Token: 0x04000804 RID: 2052
		public static readonly string tabHeaderLabelUssClassName = Tab.tabHeaderUssClassName + "-label";

		// Token: 0x04000805 RID: 2053
		public static readonly string tabHeaderEmptyLabeUssClassName = Tab.tabHeaderLabelUssClassName + "--empty";

		// Token: 0x04000806 RID: 2054
		public static readonly string tabHeaderUnderlineUssClassName = Tab.tabHeaderUssClassName + "-underline";

		// Token: 0x04000807 RID: 2055
		public static readonly string contentUssClassName = Tab.ussClassName + "__content-container";

		// Token: 0x04000808 RID: 2056
		public static readonly string draggingUssClassName = Tab.ussClassName + "--dragging";

		// Token: 0x04000809 RID: 2057
		public static readonly string reorderableUssClassName = Tab.ussClassName + "__reorderable";

		// Token: 0x0400080A RID: 2058
		public static readonly string reorderableItemHandleUssClassName = Tab.reorderableUssClassName + "-handle";

		// Token: 0x0400080B RID: 2059
		public static readonly string reorderableItemHandleBarUssClassName = Tab.reorderableItemHandleUssClassName + "-bar";

		// Token: 0x0400080C RID: 2060
		public static readonly string closeableUssClassName = Tab.tabHeaderUssClassName + "__closeable";

		// Token: 0x0400080D RID: 2061
		public static readonly string closeButtonUssClassName = Tab.ussClassName + "__close-button";

		// Token: 0x04000811 RID: 2065
		private string m_Label;

		// Token: 0x04000812 RID: 2066
		private Background m_IconImage;

		// Token: 0x04000813 RID: 2067
		private bool m_Closeable;

		// Token: 0x04000814 RID: 2068
		private VisualElement m_ContentContainer;

		// Token: 0x04000815 RID: 2069
		private VisualElement m_DragHandle;

		// Token: 0x04000816 RID: 2070
		private VisualElement m_CloseButton;

		// Token: 0x04000817 RID: 2071
		private VisualElement m_TabHeader;

		// Token: 0x04000818 RID: 2072
		private Image m_TabHeaderImage;

		// Token: 0x04000819 RID: 2073
		private Label m_TabHeaderLabel;

		// Token: 0x0200019A RID: 410
		[ExcludeFromDocs]
		[Serializable]
		public new class UxmlSerializedData : VisualElement.UxmlSerializedData
		{
			// Token: 0x06000CE8 RID: 3304 RVA: 0x0003A32C File Offset: 0x0003852C
			[Conditional("UNITY_EDITOR")]
			public new static void Register()
			{
				UxmlDescriptionCache.RegisterType(typeof(Tab.UxmlSerializedData), new UxmlAttributeNames[]
				{
					new UxmlAttributeNames("label", "label", null, Array.Empty<string>()),
					new UxmlAttributeNames("iconImageReference", "icon-image", null, Array.Empty<string>()),
					new UxmlAttributeNames("closeable", "closeable", null, Array.Empty<string>())
				});
			}

			// Token: 0x06000CE9 RID: 3305 RVA: 0x0003A3A4 File Offset: 0x000385A4
			public override object CreateInstance()
			{
				return new Tab();
			}

			// Token: 0x06000CEA RID: 3306 RVA: 0x0003A3AC File Offset: 0x000385AC
			public override void Deserialize(object obj)
			{
				base.Deserialize(obj);
				Tab tab = (Tab)obj;
				bool flag = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.label_UxmlAttributeFlags);
				if (flag)
				{
					tab.label = this.label;
				}
				bool flag2 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.iconImageReference_UxmlAttributeFlags);
				if (flag2)
				{
					tab.iconImageReference = this.iconImageReference;
				}
				bool flag3 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.closeable_UxmlAttributeFlags);
				if (flag3)
				{
					tab.closeable = this.closeable;
				}
			}

			// Token: 0x0400081C RID: 2076
			[MultilineTextField]
			[SerializeField]
			private string label;

			// Token: 0x0400081D RID: 2077
			[UxmlAttributeBindingPath("iconImage")]
			[UxmlAttribute("icon-image")]
			[ImageFieldValueDecorator]
			[SerializeField]
			private Object iconImageReference;

			// Token: 0x0400081E RID: 2078
			[SerializeField]
			private bool closeable;

			// Token: 0x0400081F RID: 2079
			[HideInInspector]
			[SerializeField]
			[UxmlIgnore]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags label_UxmlAttributeFlags;

			// Token: 0x04000820 RID: 2080
			[HideInInspector]
			[UxmlIgnore]
			[SerializeField]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags iconImageReference_UxmlAttributeFlags;

			// Token: 0x04000821 RID: 2081
			[HideInInspector]
			[UxmlIgnore]
			[SerializeField]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags closeable_UxmlAttributeFlags;
		}

		// Token: 0x0200019B RID: 411
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<Tab, Tab.UxmlTraits>
		{
		}

		// Token: 0x0200019C RID: 412
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : VisualElement.UxmlTraits
		{
			// Token: 0x06000CED RID: 3309 RVA: 0x0003A428 File Offset: 0x00038628
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				Tab tab = (Tab)ve;
				tab.label = this.m_Label.GetValueFromBag(bag, cc);
				tab.iconImage = this.m_IconImage.GetValueFromBag(bag, cc);
				tab.closeable = this.m_Closeable.GetValueFromBag(bag, cc);
			}

			// Token: 0x04000822 RID: 2082
			private readonly UxmlStringAttributeDescription m_Label = new UxmlStringAttributeDescription
			{
				name = "label"
			};

			// Token: 0x04000823 RID: 2083
			private readonly UxmlImageAttributeDescription m_IconImage = new UxmlImageAttributeDescription
			{
				name = "icon-image"
			};

			// Token: 0x04000824 RID: 2084
			private readonly UxmlBoolAttributeDescription m_Closeable = new UxmlBoolAttributeDescription
			{
				name = "closeable",
				defaultValue = false
			};
		}
	}
}
