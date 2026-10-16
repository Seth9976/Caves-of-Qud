using System;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.UIElements
{
	// Token: 0x0200008C RID: 140
	[MovedFrom(true, "UnityEditor.UIElements", "UnityEditor.UIElementsModule", null)]
	public abstract class BasePopupField<TValueType, TValueChoice> : BaseField<TValueType>
	{
		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600054A RID: 1354 RVA: 0x0001942C File Offset: 0x0001762C
		protected TextElement textElement
		{
			get
			{
				return this.m_TextElement;
			}
		}

		// Token: 0x0600054B RID: 1355
		internal abstract string GetValueToDisplay();

		// Token: 0x0600054C RID: 1356
		internal abstract string GetListItemToDisplay(TValueType item);

		// Token: 0x0600054D RID: 1357
		internal abstract void AddMenuItems(IGenericMenu menu);

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600054E RID: 1358 RVA: 0x00019444 File Offset: 0x00017644
		// (set) Token: 0x0600054F RID: 1359 RVA: 0x0001945C File Offset: 0x0001765C
		[CreateProperty]
		public virtual List<TValueChoice> choices
		{
			get
			{
				return this.m_Choices;
			}
			set
			{
				bool flag = value == null;
				if (flag)
				{
					throw new ArgumentNullException("value");
				}
				this.m_Choices = value;
				this.SetValueWithoutNotify(base.rawValue);
				base.NotifyPropertyChanged(in BasePopupField<TValueType, TValueChoice>.choicesProperty);
			}
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x0001949D File Offset: 0x0001769D
		public override void SetValueWithoutNotify(TValueType newValue)
		{
			base.SetValueWithoutNotify(newValue);
			((INotifyValueChanged<string>)this.m_TextElement).SetValueWithoutNotify(this.GetValueToDisplay());
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000551 RID: 1361 RVA: 0x000194BC File Offset: 0x000176BC
		[CreateProperty(ReadOnly = true)]
		public string text
		{
			get
			{
				return this.m_TextElement.text;
			}
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x000194D9 File Offset: 0x000176D9
		internal BasePopupField()
			: this(null)
		{
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x000194E4 File Offset: 0x000176E4
		internal BasePopupField(string label)
			: base(label, null)
		{
			base.AddToClassList(BasePopupField<TValueType, TValueChoice>.ussClassName);
			base.labelElement.AddToClassList(BasePopupField<TValueType, TValueChoice>.labelUssClassName);
			this.m_TextElement = new BasePopupField<TValueType, TValueChoice>.PopupTextElement
			{
				pickingMode = PickingMode.Ignore
			};
			this.m_TextElement.AddToClassList(BasePopupField<TValueType, TValueChoice>.textUssClassName);
			base.visualInput.AddToClassList(BasePopupField<TValueType, TValueChoice>.inputUssClassName);
			base.visualInput.Add(this.m_TextElement);
			this.m_ArrowElement = new VisualElement();
			this.m_ArrowElement.AddToClassList(BasePopupField<TValueType, TValueChoice>.arrowUssClassName);
			this.m_ArrowElement.pickingMode = PickingMode.Ignore;
			base.visualInput.Add(this.m_ArrowElement);
			this.choices = new List<TValueChoice>();
			base.RegisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDownEvent), TrickleDown.NoTrickleDown);
			base.RegisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUpEvent), TrickleDown.NoTrickleDown);
			base.RegisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMoveEvent), TrickleDown.NoTrickleDown);
			base.RegisterCallback<MouseDownEvent>(delegate(MouseDownEvent e)
			{
				bool flag = e.button == 0;
				if (flag)
				{
					e.StopPropagation();
				}
			}, TrickleDown.NoTrickleDown);
			base.RegisterCallback<NavigationSubmitEvent>(new EventCallback<NavigationSubmitEvent>(this.OnNavigationSubmit), TrickleDown.NoTrickleDown);
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x00019624 File Offset: 0x00017824
		private void OnPointerDownEvent(PointerDownEvent evt)
		{
			this.ProcessPointerDown<PointerDownEvent>(evt);
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x00019630 File Offset: 0x00017830
		private void OnPointerUpEvent(PointerUpEvent evt)
		{
			bool flag = evt.button == 0 && this.ContainsPointer(evt.pointerId);
			if (flag)
			{
				evt.StopPropagation();
			}
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x00019664 File Offset: 0x00017864
		private void OnPointerMoveEvent(PointerMoveEvent evt)
		{
			bool flag = evt.button == 0;
			if (flag)
			{
				bool flag2 = (evt.pressedButtons & 1) != 0;
				if (flag2)
				{
					this.ProcessPointerDown<PointerMoveEvent>(evt);
				}
			}
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x0001969C File Offset: 0x0001789C
		private bool ContainsPointer(int pointerId)
		{
			VisualElement topElementUnderPointer = base.elementPanel.GetTopElementUnderPointer(pointerId);
			return this == topElementUnderPointer || base.visualInput == topElementUnderPointer;
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x000196CC File Offset: 0x000178CC
		private void ProcessPointerDown<T>(PointerEventBase<T> evt) where T : PointerEventBase<T>, new()
		{
			bool flag = evt.button == 0;
			if (flag)
			{
				bool flag2 = this.ContainsPointer(evt.pointerId);
				if (flag2)
				{
					this.m_ScheduledShowMenuItem = base.schedule.Execute(new Action(this.ShowMenu));
					evt.StopPropagation();
				}
			}
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x0001971F File Offset: 0x0001791F
		private void OnNavigationSubmit(NavigationSubmitEvent evt)
		{
			this.ShowMenu();
			evt.StopPropagation();
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x00019730 File Offset: 0x00017930
		internal void ShowMenu()
		{
			bool flag = this.createMenuCallback != null;
			if (flag)
			{
				this.m_GenericMenu = this.createMenuCallback();
			}
			else
			{
				BaseVisualElementPanel elementPanel = base.elementPanel;
				IGenericMenu genericMenu;
				if (elementPanel == null || elementPanel.contextType != ContextType.Player)
				{
					genericMenu = DropdownUtility.CreateDropdown();
				}
				else
				{
					IGenericMenu genericMenu2 = new GenericDropdownMenu();
					genericMenu = genericMenu2;
				}
				this.m_GenericMenu = genericMenu;
			}
			this.AddMenuItems(this.m_GenericMenu);
			this.m_GenericMenu.DropDown(base.visualInput.worldBound, this, true);
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x000197B4 File Offset: 0x000179B4
		protected override void UpdateMixedValueContent()
		{
			bool showMixedValue = base.showMixedValue;
			if (showMixedValue)
			{
				((INotifyValueChanged<string>)this.m_TextElement).SetValueWithoutNotify(BaseField<TValueType>.mixedValueString);
			}
			this.textElement.EnableInClassList(BaseField<TValueType>.mixedValueLabelUssClassName, base.showMixedValue);
		}

		// Token: 0x040002DB RID: 731
		internal static readonly BindingId choicesProperty = "choices";

		// Token: 0x040002DC RID: 732
		internal static readonly BindingId textProperty = "text";

		// Token: 0x040002DD RID: 733
		internal List<TValueChoice> m_Choices;

		// Token: 0x040002DE RID: 734
		private TextElement m_TextElement;

		// Token: 0x040002DF RID: 735
		private VisualElement m_ArrowElement;

		// Token: 0x040002E0 RID: 736
		private IVisualElementScheduledItem m_ScheduledShowMenuItem;

		// Token: 0x040002E1 RID: 737
		internal Func<TValueChoice, string> m_FormatSelectedValueCallback;

		// Token: 0x040002E2 RID: 738
		internal Func<TValueChoice, string> m_FormatListItemCallback;

		// Token: 0x040002E3 RID: 739
		internal Func<IGenericMenu> createMenuCallback;

		// Token: 0x040002E4 RID: 740
		internal IGenericMenu m_GenericMenu;

		// Token: 0x040002E5 RID: 741
		internal bool m_AutoCloseMenu = true;

		// Token: 0x040002E6 RID: 742
		public new static readonly string ussClassName = "unity-base-popup-field";

		// Token: 0x040002E7 RID: 743
		public static readonly string textUssClassName = BasePopupField<TValueType, TValueChoice>.ussClassName + "__text";

		// Token: 0x040002E8 RID: 744
		public static readonly string arrowUssClassName = BasePopupField<TValueType, TValueChoice>.ussClassName + "__arrow";

		// Token: 0x040002E9 RID: 745
		public new static readonly string labelUssClassName = BasePopupField<TValueType, TValueChoice>.ussClassName + "__label";

		// Token: 0x040002EA RID: 746
		public new static readonly string inputUssClassName = BasePopupField<TValueType, TValueChoice>.ussClassName + "__input";

		// Token: 0x0200008D RID: 141
		private class PopupTextElement : TextElement
		{
			// Token: 0x0600055D RID: 1373 RVA: 0x00019880 File Offset: 0x00017A80
			protected internal override Vector2 DoMeasure(float desiredWidth, VisualElement.MeasureMode widthMode, float desiredHeight, VisualElement.MeasureMode heightMode)
			{
				string text = this.text;
				bool flag = string.IsNullOrEmpty(text);
				if (flag)
				{
					text = " ";
				}
				return base.MeasureTextSize(text, desiredWidth, widthMode, desiredHeight, heightMode);
			}
		}
	}
}
