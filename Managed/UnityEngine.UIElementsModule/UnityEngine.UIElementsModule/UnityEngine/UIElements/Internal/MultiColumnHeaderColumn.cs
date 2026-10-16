using System;

namespace UnityEngine.UIElements.Internal
{
	// Token: 0x02000735 RID: 1845
	internal class MultiColumnHeaderColumn : VisualElement
	{
		// Token: 0x17000D0C RID: 3340
		// (get) Token: 0x060035A1 RID: 13729 RVA: 0x000D0D30 File Offset: 0x000CEF30
		// (set) Token: 0x060035A2 RID: 13730 RVA: 0x000D0D38 File Offset: 0x000CEF38
		public Clickable clickable { get; private set; }

		// Token: 0x17000D0D RID: 3341
		// (get) Token: 0x060035A3 RID: 13731 RVA: 0x000D0D41 File Offset: 0x000CEF41
		// (set) Token: 0x060035A4 RID: 13732 RVA: 0x000D0D49 File Offset: 0x000CEF49
		public ColumnMover mover { get; private set; }

		// Token: 0x17000D0E RID: 3342
		// (get) Token: 0x060035A5 RID: 13733 RVA: 0x000D0D52 File Offset: 0x000CEF52
		// (set) Token: 0x060035A6 RID: 13734 RVA: 0x000D0D5F File Offset: 0x000CEF5F
		public string sortOrderLabel
		{
			get
			{
				return this.m_SortIndicatorContainer.sortOrderLabel;
			}
			set
			{
				this.m_SortIndicatorContainer.sortOrderLabel = value;
			}
		}

		// Token: 0x17000D0F RID: 3343
		// (get) Token: 0x060035A7 RID: 13735 RVA: 0x000D0D6E File Offset: 0x000CEF6E
		// (set) Token: 0x060035A8 RID: 13736 RVA: 0x000D0D76 File Offset: 0x000CEF76
		public Column column { get; private set; }

		// Token: 0x17000D10 RID: 3344
		// (get) Token: 0x060035A9 RID: 13737 RVA: 0x000D0D7F File Offset: 0x000CEF7F
		internal Label title
		{
			get
			{
				VisualElement content = this.content;
				return (content != null) ? content.Q<Label>(MultiColumnHeaderColumn.titleElementName, null) : null;
			}
		}

		// Token: 0x17000D11 RID: 3345
		// (get) Token: 0x060035AA RID: 13738 RVA: 0x000D0D99 File Offset: 0x000CEF99
		// (set) Token: 0x060035AB RID: 13739 RVA: 0x000D0DA4 File Offset: 0x000CEFA4
		public VisualElement content
		{
			get
			{
				return this.m_Content;
			}
			set
			{
				bool flag = this.m_Content != null;
				if (flag)
				{
					bool flag2 = this.m_Content.parent == this.m_ContentContainer;
					if (flag2)
					{
						this.m_Content.RemoveFromHierarchy();
					}
					this.DestroyHeaderContent();
					this.m_Content = null;
				}
				this.m_Content = value;
				bool flag3 = this.m_Content != null;
				if (flag3)
				{
					this.m_Content.AddToClassList(MultiColumnHeaderColumn.contentUssClassName);
					this.m_ContentContainer.Add(this.m_Content);
				}
			}
		}

		// Token: 0x17000D12 RID: 3346
		// (get) Token: 0x060035AC RID: 13740 RVA: 0x000D0E2D File Offset: 0x000CF02D
		// (set) Token: 0x060035AD RID: 13741 RVA: 0x000D0E54 File Offset: 0x000CF054
		private bool isContentBound
		{
			get
			{
				return this.m_Content != null && (bool)this.m_Content.GetProperty(MultiColumnHeaderColumn.s_BoundVEPropertyName);
			}
			set
			{
				VisualElement content = this.m_Content;
				if (content != null)
				{
					content.SetProperty(MultiColumnHeaderColumn.s_BoundVEPropertyName, value);
				}
			}
		}

		// Token: 0x060035AE RID: 13742 RVA: 0x000D0E78 File Offset: 0x000CF078
		public MultiColumnHeaderColumn()
			: this(new Column())
		{
		}

		// Token: 0x060035AF RID: 13743 RVA: 0x000D0E88 File Offset: 0x000CF088
		public MultiColumnHeaderColumn(Column column)
		{
			this.column = column;
			this.column.changed += this.OnColumnChanged;
			this.column.resized += this.OnColumnResized;
			base.AddToClassList(MultiColumnHeaderColumn.ussClassName);
			base.style.marginLeft = 0f;
			base.style.marginTop = 0f;
			base.style.marginRight = 0f;
			base.style.marginBottom = 0f;
			base.style.paddingLeft = 0f;
			base.style.paddingTop = 0f;
			base.style.paddingRight = 0f;
			base.style.paddingBottom = 0f;
			base.Add(this.m_SortIndicatorContainer = new MultiColumnHeaderColumnSortIndicator());
			this.m_ContentContainer = new VisualElement();
			this.m_ContentContainer.style.flexGrow = 1f;
			this.m_ContentContainer.style.flexShrink = 1f;
			this.m_ContentContainer.AddToClassList(MultiColumnHeaderColumn.contentContainerUssClassName);
			base.Add(this.m_ContentContainer);
			this.UpdateHeaderTemplate();
			this.UpdateGeometryFromColumn();
			this.InitManipulators();
		}

		// Token: 0x060035B0 RID: 13744 RVA: 0x000D101C File Offset: 0x000CF21C
		private void OnColumnChanged(Column c, ColumnDataType role)
		{
			bool flag = this.column != c;
			if (!flag)
			{
				bool flag2 = role == ColumnDataType.HeaderTemplate;
				if (flag2)
				{
					IVisualElementScheduledItem scheduledHeaderTemplateUpdate = this.m_ScheduledHeaderTemplateUpdate;
					if (scheduledHeaderTemplateUpdate != null)
					{
						scheduledHeaderTemplateUpdate.Pause();
					}
					this.m_ScheduledHeaderTemplateUpdate = base.schedule.Execute(new Action(this.UpdateHeaderTemplate));
				}
				else
				{
					this.UpdateDataFromColumn();
				}
			}
		}

		// Token: 0x060035B1 RID: 13745 RVA: 0x000D1081 File Offset: 0x000CF281
		private void OnColumnResized(Column c)
		{
			this.UpdateGeometryFromColumn();
		}

		// Token: 0x060035B2 RID: 13746 RVA: 0x000D108C File Offset: 0x000CF28C
		private void InitManipulators()
		{
			this.AddManipulator(this.mover = new ColumnMover());
			this.mover.movingChanged += this.OnMoverChanged;
			this.AddManipulator(this.clickable = new Clickable(null));
			this.clickable.activators.Add(new ManipulatorActivationFilter
			{
				button = MouseButton.LeftMouse,
				modifiers = EventModifiers.Shift
			});
			EventModifiers eventModifiers = EventModifiers.Control;
			RuntimePlatform platform = Application.platform;
			bool flag = platform == RuntimePlatform.OSXEditor || platform == RuntimePlatform.OSXPlayer;
			if (flag)
			{
				eventModifiers = EventModifiers.Command;
			}
			this.clickable.activators.Add(new ManipulatorActivationFilter
			{
				button = MouseButton.LeftMouse,
				modifiers = eventModifiers
			});
		}

		// Token: 0x060035B3 RID: 13747 RVA: 0x000D115C File Offset: 0x000CF35C
		private void OnMoverChanged(ColumnMover mv)
		{
			bool moving = this.mover.moving;
			if (moving)
			{
				base.AddToClassList(MultiColumnHeaderColumn.movingUssClassName);
			}
			else
			{
				base.RemoveFromClassList(MultiColumnHeaderColumn.movingUssClassName);
			}
		}

		// Token: 0x060035B4 RID: 13748 RVA: 0x000D1194 File Offset: 0x000CF394
		private void UpdateDataFromColumn()
		{
			bool flag = this.column == null;
			if (!flag)
			{
				base.name = this.column.name;
				this.UnbindHeaderContent();
				this.BindHeaderContent();
			}
		}

		// Token: 0x060035B5 RID: 13749 RVA: 0x000D11D4 File Offset: 0x000CF3D4
		private void BindHeaderContent()
		{
			bool flag = !this.isContentBound;
			if (flag)
			{
				Action<VisualElement> action = this.content.GetProperty(MultiColumnHeaderColumn.s_BindingCallbackVEPropertyName) as Action<VisualElement>;
				if (action != null)
				{
					action(this.content);
				}
				this.isContentBound = true;
			}
		}

		// Token: 0x060035B6 RID: 13750 RVA: 0x000D1228 File Offset: 0x000CF428
		private void UnbindHeaderContent()
		{
			bool isContentBound = this.isContentBound;
			if (isContentBound)
			{
				Action<VisualElement> action = this.content.GetProperty(MultiColumnHeaderColumn.s_UnbindingCallbackVEPropertyName) as Action<VisualElement>;
				if (action != null)
				{
					action(this.content);
				}
				this.isContentBound = false;
			}
		}

		// Token: 0x060035B7 RID: 13751 RVA: 0x000D1278 File Offset: 0x000CF478
		private void DestroyHeaderContent()
		{
			this.UnbindHeaderContent();
			Action<VisualElement> action = this.content.GetProperty(MultiColumnHeaderColumn.s_DestroyCallbackVEPropertyName) as Action<VisualElement>;
			this.content.ClearProperty(MultiColumnHeaderColumn.s_BindingCallbackVEPropertyName);
			this.content.ClearProperty(MultiColumnHeaderColumn.s_UnbindingCallbackVEPropertyName);
			this.content.ClearProperty(MultiColumnHeaderColumn.s_DestroyCallbackVEPropertyName);
			this.content.ClearProperty(MultiColumnHeaderColumn.s_BoundVEPropertyName);
			if (action != null)
			{
				action(this.content);
			}
		}

		// Token: 0x060035B8 RID: 13752 RVA: 0x000D1314 File Offset: 0x000CF514
		private VisualElement CreateDefaultHeaderContent()
		{
			VisualElement visualElement = new VisualElement
			{
				pickingMode = PickingMode.Ignore
			};
			visualElement.AddToClassList(MultiColumnHeaderColumn.defaultContentUssClassName);
			MultiColumnHeaderColumnIcon multiColumnHeaderColumnIcon = new MultiColumnHeaderColumnIcon
			{
				name = MultiColumnHeaderColumn.iconElementName,
				pickingMode = PickingMode.Ignore
			};
			Label label = new Label
			{
				name = MultiColumnHeaderColumn.titleElementName,
				pickingMode = PickingMode.Ignore
			};
			label.AddToClassList(MultiColumnHeaderColumn.titleUssClassName);
			visualElement.Add(multiColumnHeaderColumnIcon);
			visualElement.Add(label);
			return visualElement;
		}

		// Token: 0x060035B9 RID: 13753 RVA: 0x000D1394 File Offset: 0x000CF594
		private void DefaultBindHeaderContent(VisualElement ve)
		{
			Label label = ve.Q<Label>(MultiColumnHeaderColumn.titleElementName, null);
			MultiColumnHeaderColumnIcon multiColumnHeaderColumnIcon = ve.Q<MultiColumnHeaderColumnIcon>(null, null);
			ve.RemoveFromClassList(MultiColumnHeaderColumn.hasTitleUssClassName);
			bool flag = label != null;
			if (flag)
			{
				label.text = this.column.title;
			}
			bool flag2 = !string.IsNullOrEmpty(this.column.title);
			if (flag2)
			{
				ve.AddToClassList(MultiColumnHeaderColumn.hasTitleUssClassName);
			}
			bool flag3 = multiColumnHeaderColumnIcon != null;
			if (flag3)
			{
				bool flag4 = this.column.icon.texture != null || this.column.icon.sprite != null || this.column.icon.vectorImage != null;
				if (flag4)
				{
					multiColumnHeaderColumnIcon.isImageInline = true;
					multiColumnHeaderColumnIcon.image = this.column.icon.texture;
					multiColumnHeaderColumnIcon.sprite = this.column.icon.sprite;
					multiColumnHeaderColumnIcon.vectorImage = this.column.icon.vectorImage;
				}
				else
				{
					bool isImageInline = multiColumnHeaderColumnIcon.isImageInline;
					if (isImageInline)
					{
						multiColumnHeaderColumnIcon.image = null;
						multiColumnHeaderColumnIcon.sprite = null;
						multiColumnHeaderColumnIcon.vectorImage = null;
					}
				}
				multiColumnHeaderColumnIcon.UpdateClassList();
			}
		}

		// Token: 0x060035BA RID: 13754 RVA: 0x000D14F8 File Offset: 0x000CF6F8
		private void UpdateHeaderTemplate()
		{
			bool flag = this.column == null;
			if (!flag)
			{
				Func<VisualElement> func = this.column.makeHeader;
				Action<VisualElement> action = this.column.bindHeader;
				Action<VisualElement> action2 = this.column.unbindHeader;
				Action<VisualElement> action3 = this.column.destroyHeader;
				bool flag2 = func == null;
				if (flag2)
				{
					func = new Func<VisualElement>(this.CreateDefaultHeaderContent);
					action = new Action<VisualElement>(this.DefaultBindHeaderContent);
					action2 = null;
					action3 = null;
				}
				this.content = func();
				this.content.SetProperty(MultiColumnHeaderColumn.s_BindingCallbackVEPropertyName, action);
				this.content.SetProperty(MultiColumnHeaderColumn.s_UnbindingCallbackVEPropertyName, action2);
				this.content.SetProperty(MultiColumnHeaderColumn.s_DestroyCallbackVEPropertyName, action3);
				this.isContentBound = false;
				this.m_ScheduledHeaderTemplateUpdate = null;
				this.UpdateDataFromColumn();
			}
		}

		// Token: 0x060035BB RID: 13755 RVA: 0x000D15DC File Offset: 0x000CF7DC
		private void UpdateGeometryFromColumn()
		{
			bool flag = float.IsNaN(this.column.desiredWidth);
			if (!flag)
			{
				base.style.width = this.column.desiredWidth;
			}
		}

		// Token: 0x060035BC RID: 13756 RVA: 0x000D161C File Offset: 0x000CF81C
		public void Dispose()
		{
			this.mover.movingChanged -= this.OnMoverChanged;
			this.column.changed -= this.OnColumnChanged;
			this.column.resized -= this.OnColumnResized;
			this.RemoveManipulator(this.mover);
			this.RemoveManipulator(this.clickable);
			this.mover = null;
			this.column = null;
			this.content = null;
		}

		// Token: 0x04001B52 RID: 6994
		public static readonly string ussClassName = MultiColumnCollectionHeader.ussClassName + "__column";

		// Token: 0x04001B53 RID: 6995
		public static readonly string sortableUssClassName = MultiColumnHeaderColumn.ussClassName + "--sortable";

		// Token: 0x04001B54 RID: 6996
		public static readonly string sortedAscendingUssClassName = MultiColumnHeaderColumn.ussClassName + "--sorted-ascending";

		// Token: 0x04001B55 RID: 6997
		public static readonly string sortedDescendingUssClassName = MultiColumnHeaderColumn.ussClassName + "--sorted-descending";

		// Token: 0x04001B56 RID: 6998
		public static readonly string movingUssClassName = MultiColumnHeaderColumn.ussClassName + "--moving";

		// Token: 0x04001B57 RID: 6999
		public static readonly string contentContainerUssClassName = MultiColumnHeaderColumn.ussClassName + "__content-container";

		// Token: 0x04001B58 RID: 7000
		public static readonly string contentUssClassName = MultiColumnHeaderColumn.ussClassName + "__content";

		// Token: 0x04001B59 RID: 7001
		public static readonly string defaultContentUssClassName = MultiColumnHeaderColumn.ussClassName + "__default-content";

		// Token: 0x04001B5A RID: 7002
		public static readonly string hasIconUssClassName = MultiColumnHeaderColumn.contentUssClassName + "--has-icon";

		// Token: 0x04001B5B RID: 7003
		public static readonly string hasTitleUssClassName = MultiColumnHeaderColumn.contentUssClassName + "--has-title";

		// Token: 0x04001B5C RID: 7004
		public static readonly string titleUssClassName = MultiColumnHeaderColumn.ussClassName + "__title";

		// Token: 0x04001B5D RID: 7005
		public static readonly string iconElementName = "unity-multi-column-header-column-icon";

		// Token: 0x04001B5E RID: 7006
		public static readonly string titleElementName = "unity-multi-column-header-column-title";

		// Token: 0x04001B5F RID: 7007
		private static readonly string s_BoundVEPropertyName = "__bound";

		// Token: 0x04001B60 RID: 7008
		private static readonly string s_BindingCallbackVEPropertyName = "__binding-callback";

		// Token: 0x04001B61 RID: 7009
		private static readonly string s_UnbindingCallbackVEPropertyName = "__unbinding-callback";

		// Token: 0x04001B62 RID: 7010
		private static readonly string s_DestroyCallbackVEPropertyName = "__destroy-callback";

		// Token: 0x04001B63 RID: 7011
		private VisualElement m_ContentContainer;

		// Token: 0x04001B64 RID: 7012
		private VisualElement m_Content;

		// Token: 0x04001B65 RID: 7013
		private MultiColumnHeaderColumnSortIndicator m_SortIndicatorContainer;

		// Token: 0x04001B66 RID: 7014
		private IVisualElementScheduledItem m_ScheduledHeaderTemplateUpdate;
	}
}
