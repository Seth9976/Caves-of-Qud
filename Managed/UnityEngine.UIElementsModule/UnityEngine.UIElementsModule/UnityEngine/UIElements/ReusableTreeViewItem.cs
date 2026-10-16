using System;
using System.Diagnostics;

namespace UnityEngine.UIElements
{
	// Token: 0x0200007A RID: 122
	internal class ReusableTreeViewItem : ReusableCollectionItem
	{
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x00014ECD File Offset: 0x000130CD
		public override VisualElement rootElement
		{
			get
			{
				return this.m_Container ?? base.bindableElement;
			}
		}

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x0600047F RID: 1151 RVA: 0x00014EE0 File Offset: 0x000130E0
		// (remove) Token: 0x06000480 RID: 1152 RVA: 0x00014F18 File Offset: 0x00013118
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<PointerUpEvent> onPointerUp;

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06000481 RID: 1153 RVA: 0x00014F50 File Offset: 0x00013150
		// (remove) Token: 0x06000482 RID: 1154 RVA: 0x00014F88 File Offset: 0x00013188
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<ChangeEvent<bool>> onToggleValueChanged;

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x00014FC0 File Offset: 0x000131C0
		internal float indentWidth
		{
			get
			{
				return this.customIndentWidth ?? this.m_IndentWidth;
			}
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00014FEC File Offset: 0x000131EC
		public ReusableTreeViewItem()
		{
			this.m_PointerUpCallback = new EventCallback<PointerUpEvent>(this.OnPointerUp);
			this.m_ToggleValueChangedCallback = new EventCallback<ChangeEvent<bool>>(this.OnToggleValueChanged);
			this.m_ToggleGeometryChangedCallback = new EventCallback<GeometryChangedEvent>(this.OnToggleGeometryChanged);
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0001502C File Offset: 0x0001322C
		public override void Init(VisualElement item)
		{
			base.Init(item);
			VisualElement visualElement = new VisualElement
			{
				name = BaseTreeView.itemUssClassName
			};
			visualElement.AddToClassList(BaseTreeView.itemUssClassName);
			this.InitExpandHierarchy(visualElement, item);
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0001506C File Offset: 0x0001326C
		protected void InitExpandHierarchy(VisualElement root, VisualElement item)
		{
			this.m_Container = root;
			this.m_Container.style.flexDirection = FlexDirection.Row;
			this.m_IndentElement = new VisualElement
			{
				name = BaseTreeView.itemIndentUssClassName,
				style = 
				{
					flexDirection = FlexDirection.Row
				}
			};
			this.m_Container.hierarchy.Add(this.m_IndentElement);
			this.m_Toggle = new Toggle
			{
				name = BaseTreeView.itemToggleUssClassName,
				userData = this
			};
			this.m_Toggle.AddToClassList(Foldout.toggleUssClassName);
			this.m_Toggle.AddToClassList(BaseTreeView.itemToggleUssClassName);
			this.m_Toggle.visualInput.AddToClassList(Foldout.inputUssClassName);
			this.m_Checkmark = this.m_Toggle.visualInput.Q(null, Toggle.checkmarkUssClassName);
			this.m_Checkmark.AddToClassList(Foldout.checkmarkUssClassName);
			this.m_Container.hierarchy.Add(this.m_Toggle);
			this.m_BindableContainer = new VisualElement
			{
				name = BaseTreeView.itemContentContainerUssClassName,
				style = 
				{
					flexGrow = 1f
				}
			};
			this.m_BindableContainer.AddToClassList(BaseTreeView.itemContentContainerUssClassName);
			this.m_Container.Add(this.m_BindableContainer);
			this.m_BindableContainer.Add(item);
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x000151D8 File Offset: 0x000133D8
		public override void PreAttachElement()
		{
			base.PreAttachElement();
			this.rootElement.AddToClassList(BaseTreeView.itemUssClassName);
			VisualElement container = this.m_Container;
			if (container != null)
			{
				container.RegisterCallback<PointerUpEvent>(this.m_PointerUpCallback, TrickleDown.NoTrickleDown);
			}
			Toggle toggle = this.m_Toggle;
			if (toggle != null)
			{
				toggle.visualInput.Q(null, Toggle.checkmarkUssClassName).RegisterCallback<GeometryChangedEvent>(this.m_ToggleGeometryChangedCallback, TrickleDown.NoTrickleDown);
			}
			Toggle toggle2 = this.m_Toggle;
			if (toggle2 != null)
			{
				toggle2.RegisterValueChangedCallback<bool>(this.m_ToggleValueChangedCallback);
			}
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x00015258 File Offset: 0x00013458
		public override void DetachElement()
		{
			base.DetachElement();
			this.rootElement.RemoveFromClassList(BaseTreeView.itemUssClassName);
			VisualElement container = this.m_Container;
			if (container != null)
			{
				container.UnregisterCallback<PointerUpEvent>(this.m_PointerUpCallback, TrickleDown.NoTrickleDown);
			}
			Toggle toggle = this.m_Toggle;
			if (toggle != null)
			{
				toggle.visualInput.Q(null, Toggle.checkmarkUssClassName).UnregisterCallback<GeometryChangedEvent>(this.m_ToggleGeometryChangedCallback, TrickleDown.NoTrickleDown);
			}
			Toggle toggle2 = this.m_Toggle;
			if (toggle2 != null)
			{
				toggle2.UnregisterValueChangedCallback<bool>(this.m_ToggleValueChangedCallback);
			}
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x000152D8 File Offset: 0x000134D8
		public void Indent(int depth)
		{
			bool flag = this.m_IndentElement == null;
			if (!flag)
			{
				this.m_Depth = depth;
				this.UpdateIndentLayout();
			}
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00015303 File Offset: 0x00013503
		public void SetExpandedWithoutNotify(bool expanded)
		{
			Toggle toggle = this.m_Toggle;
			if (toggle != null)
			{
				toggle.SetValueWithoutNotify(expanded);
			}
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0001531C File Offset: 0x0001351C
		public void SetToggleVisibility(bool visible)
		{
			bool flag = this.m_Toggle != null;
			if (flag)
			{
				this.m_Toggle.visible = visible;
			}
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00015344 File Offset: 0x00013544
		private void OnToggleGeometryChanged(GeometryChangedEvent evt)
		{
			float num = this.m_Checkmark.resolvedStyle.width + this.m_Checkmark.resolvedStyle.marginLeft + this.m_Checkmark.resolvedStyle.marginRight;
			bool flag = Math.Abs(num - this.m_IndentWidth) < float.Epsilon;
			if (!flag)
			{
				this.m_IndentWidth = num;
				this.UpdateIndentLayout();
			}
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x000153B0 File Offset: 0x000135B0
		private void UpdateIndentLayout()
		{
			this.m_IndentElement.style.width = this.indentWidth * (float)this.m_Depth;
			this.m_IndentElement.EnableInClassList(BaseTreeView.itemIndentUssClassName, this.m_Depth > 0);
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x000153FC File Offset: 0x000135FC
		private void OnPointerUp(PointerUpEvent evt)
		{
			Action<PointerUpEvent> action = this.onPointerUp;
			if (action != null)
			{
				action(evt);
			}
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00015412 File Offset: 0x00013612
		private void OnToggleValueChanged(ChangeEvent<bool> evt)
		{
			Action<ChangeEvent<bool>> action = this.onToggleValueChanged;
			if (action != null)
			{
				action(evt);
			}
		}

		// Token: 0x04000232 RID: 562
		private Toggle m_Toggle;

		// Token: 0x04000233 RID: 563
		private VisualElement m_Container;

		// Token: 0x04000234 RID: 564
		internal VisualElement m_IndentElement;

		// Token: 0x04000235 RID: 565
		private VisualElement m_BindableContainer;

		// Token: 0x04000236 RID: 566
		private VisualElement m_Checkmark;

		// Token: 0x04000239 RID: 569
		internal int m_Depth;

		// Token: 0x0400023A RID: 570
		private float m_IndentWidth;

		// Token: 0x0400023B RID: 571
		internal float? customIndentWidth;

		// Token: 0x0400023C RID: 572
		private EventCallback<PointerUpEvent> m_PointerUpCallback;

		// Token: 0x0400023D RID: 573
		private EventCallback<ChangeEvent<bool>> m_ToggleValueChangedCallback;

		// Token: 0x0400023E RID: 574
		private EventCallback<GeometryChangedEvent> m_ToggleGeometryChangedCallback;
	}
}
