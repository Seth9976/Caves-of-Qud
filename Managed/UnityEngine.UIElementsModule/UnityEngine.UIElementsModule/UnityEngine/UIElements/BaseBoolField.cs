using System;
using System.Diagnostics;
using Unity.Properties;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine.UIElements
{
	// Token: 0x0200007F RID: 127
	public abstract class BaseBoolField : BaseField<bool>
	{
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x00016A34 File Offset: 0x00014C34
		internal Label boolFieldLabelElement
		{
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			get
			{
				return this.m_Label;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x00016A3C File Offset: 0x00014C3C
		// (set) Token: 0x060004CA RID: 1226 RVA: 0x00016A49 File Offset: 0x00014C49
		internal bool acceptClicksIfDisabled
		{
			get
			{
				return this.m_Clickable.acceptClicksIfDisabled;
			}
			set
			{
				this.m_Clickable.acceptClicksIfDisabled = value;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x00016A58 File Offset: 0x00014C58
		// (set) Token: 0x060004CC RID: 1228 RVA: 0x00016A60 File Offset: 0x00014C60
		[CreateProperty]
		public bool toggleOnLabelClick { get; set; } = true;

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x00016A69 File Offset: 0x00014C69
		// (set) Token: 0x060004CE RID: 1230 RVA: 0x00016A71 File Offset: 0x00014C71
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal bool toggleOnTextClick { get; set; } = true;

		// Token: 0x060004CF RID: 1231 RVA: 0x00016A7C File Offset: 0x00014C7C
		public BaseBoolField(string label)
			: base(label, null)
		{
			this.m_CheckMark = new VisualElement
			{
				name = "unity-checkmark",
				pickingMode = PickingMode.Ignore
			};
			base.visualInput.Add(this.m_CheckMark);
			base.visualInput.pickingMode = PickingMode.Position;
			base.labelElement.focusable = false;
			this.text = null;
			this.AddManipulator(this.m_Clickable = new Clickable(new Action<EventBase>(this.OnClickEvent)));
			base.RegisterCallback<NavigationSubmitEvent>(new EventCallback<NavigationSubmitEvent>(this.OnNavigationSubmit), TrickleDown.NoTrickleDown);
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x00016B29 File Offset: 0x00014D29
		private void OnNavigationSubmit(NavigationSubmitEvent evt)
		{
			this.ToggleValue();
			evt.StopPropagation();
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x00016B3C File Offset: 0x00014D3C
		// (set) Token: 0x060004D2 RID: 1234 RVA: 0x00016B60 File Offset: 0x00014D60
		[CreateProperty]
		public string text
		{
			get
			{
				Label label = this.m_Label;
				return (label != null) ? label.text : null;
			}
			set
			{
				Label label = this.m_Label;
				bool flag = string.CompareOrdinal((label != null) ? label.text : null, value) == 0;
				if (!flag)
				{
					bool flag2 = !string.IsNullOrEmpty(value);
					if (flag2)
					{
						this.InitLabel();
						this.m_Label.text = value;
					}
					else
					{
						bool flag3 = this.m_Label != null;
						if (flag3)
						{
							this.m_Label.RemoveFromHierarchy();
							this.m_Label.text = value;
						}
					}
					base.NotifyPropertyChanged(in BaseBoolField.textProperty);
				}
			}
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x00016BE8 File Offset: 0x00014DE8
		protected virtual void InitLabel()
		{
			bool flag = this.m_Label == null;
			if (flag)
			{
				this.m_Label = new Label();
			}
			else
			{
				bool flag2 = this.m_Label.parent != null;
				if (flag2)
				{
					return;
				}
			}
			bool flag3 = this.m_CheckMark.hierarchy.parent != base.visualInput;
			if (flag3)
			{
				base.visualInput.Add(this.m_Label);
			}
			else
			{
				int num = base.visualInput.IndexOf(this.m_CheckMark);
				base.visualInput.Insert(num + 1, this.m_Label);
			}
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x00016C88 File Offset: 0x00014E88
		public override void SetValueWithoutNotify(bool newValue)
		{
			if (newValue)
			{
				base.visualInput.pseudoStates |= PseudoStates.Checked;
				base.pseudoStates |= PseudoStates.Checked;
			}
			else
			{
				base.visualInput.pseudoStates &= ~PseudoStates.Checked;
				base.pseudoStates &= ~PseudoStates.Checked;
			}
			base.SetValueWithoutNotify(newValue);
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x00016CF4 File Offset: 0x00014EF4
		private void OnClickEvent(EventBase evt)
		{
			bool flag = evt.eventTypeId == EventBase<MouseUpEvent>.TypeId();
			if (flag)
			{
				IMouseEvent mouseEvent = (IMouseEvent)evt;
				bool flag2 = this.ShouldIgnoreClick(mouseEvent.mousePosition);
				if (!flag2)
				{
					bool flag3 = mouseEvent.button == 0;
					if (flag3)
					{
						this.ToggleValue();
					}
				}
			}
			else
			{
				bool flag4 = evt.eventTypeId == EventBase<PointerUpEvent>.TypeId() || evt.eventTypeId == EventBase<ClickEvent>.TypeId();
				if (flag4)
				{
					IPointerEvent pointerEvent = (IPointerEvent)evt;
					bool flag5 = this.ShouldIgnoreClick(pointerEvent.position);
					if (!flag5)
					{
						bool flag6 = pointerEvent.button == 0;
						if (flag6)
						{
							this.ToggleValue();
						}
					}
				}
			}
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x00016DA8 File Offset: 0x00014FA8
		private bool ShouldIgnoreClick(Vector3 position)
		{
			bool flag = !this.toggleOnLabelClick && base.labelElement.worldBound.Contains(position);
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				bool flag3;
				if (!this.toggleOnTextClick)
				{
					Label label = this.m_Label;
					flag3 = label != null && label.worldBound.Contains(position);
				}
				else
				{
					flag3 = false;
				}
				bool flag4 = flag3;
				flag2 = flag4;
			}
			return flag2;
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x00016E10 File Offset: 0x00015010
		protected virtual void ToggleValue()
		{
			this.value = !this.value;
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x00016E24 File Offset: 0x00015024
		protected override void UpdateMixedValueContent()
		{
			bool showMixedValue = base.showMixedValue;
			if (showMixedValue)
			{
				base.visualInput.pseudoStates &= ~PseudoStates.Checked;
				base.pseudoStates &= ~PseudoStates.Checked;
				this.m_CheckMark.RemoveFromHierarchy();
				base.visualInput.Add(base.mixedValueLabel);
				this.m_OriginalText = this.text;
				this.text = "";
			}
			else
			{
				base.mixedValueLabel.RemoveFromHierarchy();
				base.visualInput.Add(this.m_CheckMark);
				bool flag = this.m_OriginalText != null;
				if (flag)
				{
					this.text = this.m_OriginalText;
				}
			}
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00016ED5 File Offset: 0x000150D5
		internal override void RegisterEditingCallbacks()
		{
			base.RegisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(base.StartEditing), TrickleDown.NoTrickleDown);
			base.RegisterCallback<FocusOutEvent>(new EventCallback<FocusOutEvent>(base.EndEditing), TrickleDown.NoTrickleDown);
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x00016F00 File Offset: 0x00015100
		internal override void UnregisterEditingCallbacks()
		{
			base.UnregisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(base.StartEditing), TrickleDown.NoTrickleDown);
			base.UnregisterCallback<FocusOutEvent>(new EventCallback<FocusOutEvent>(base.EndEditing), TrickleDown.NoTrickleDown);
		}

		// Token: 0x04000254 RID: 596
		internal static readonly BindingId textProperty = "text";

		// Token: 0x04000255 RID: 597
		internal static readonly BindingId toggleOnLabelClickProperty = "toggleOnLabelClick";

		// Token: 0x04000256 RID: 598
		protected Label m_Label;

		// Token: 0x04000257 RID: 599
		protected internal readonly VisualElement m_CheckMark;

		// Token: 0x04000258 RID: 600
		internal readonly Clickable m_Clickable;

		// Token: 0x0400025B RID: 603
		private string m_OriginalText;

		// Token: 0x02000080 RID: 128
		[ExcludeFromDocs]
		[Serializable]
		public new abstract class UxmlSerializedData : BaseField<bool>.UxmlSerializedData
		{
			// Token: 0x060004DC RID: 1244 RVA: 0x00016F4B File Offset: 0x0001514B
			[Conditional("UNITY_EDITOR")]
			public new static void Register()
			{
				BaseField<bool>.UxmlSerializedData.Register();
				UxmlDescriptionCache.RegisterType(typeof(BaseBoolField.UxmlSerializedData), new UxmlAttributeNames[]
				{
					new UxmlAttributeNames("toggleOnLabelClick", "toggle-on-label-click", null, Array.Empty<string>())
				});
			}

			// Token: 0x060004DD RID: 1245 RVA: 0x00016F88 File Offset: 0x00015188
			public override void Deserialize(object obj)
			{
				base.Deserialize(obj);
				BaseBoolField baseBoolField = (BaseBoolField)obj;
				bool flag = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.toggleOnLabelClick_UxmlAttributeFlags);
				if (flag)
				{
					baseBoolField.toggleOnLabelClick = this.toggleOnLabelClick;
				}
			}

			// Token: 0x0400025C RID: 604
			[SerializeField]
			private bool toggleOnLabelClick;

			// Token: 0x0400025D RID: 605
			[SerializeField]
			[UxmlIgnore]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags toggleOnLabelClick_UxmlAttributeFlags;
		}
	}
}
