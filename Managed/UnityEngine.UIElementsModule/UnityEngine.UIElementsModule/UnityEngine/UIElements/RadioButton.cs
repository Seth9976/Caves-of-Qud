using System;
using System.Diagnostics;
using UnityEngine.Internal;

namespace UnityEngine.UIElements
{
	// Token: 0x02000174 RID: 372
	public class RadioButton : BaseBoolField, IGroupBoxOption
	{
		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000BCB RID: 3019 RVA: 0x00033B8E File Offset: 0x00031D8E
		// (set) Token: 0x06000BCC RID: 3020 RVA: 0x00033B98 File Offset: 0x00031D98
		public override bool value
		{
			get
			{
				return base.value;
			}
			set
			{
				bool flag = base.value != value;
				if (flag)
				{
					base.value = value;
					this.UpdateCheckmark();
					if (value)
					{
						this.OnOptionSelected<RadioButton>();
					}
				}
			}
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x00033BD5 File Offset: 0x00031DD5
		public RadioButton()
			: this(null)
		{
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x00033BE0 File Offset: 0x00031DE0
		public RadioButton(string label)
			: base(label)
		{
			base.AddToClassList(RadioButton.ussClassName);
			base.visualInput.AddToClassList(RadioButton.inputUssClassName);
			base.labelElement.AddToClassList(RadioButton.labelUssClassName);
			this.m_CheckMark.RemoveFromHierarchy();
			this.m_CheckmarkBackground = new VisualElement
			{
				pickingMode = PickingMode.Ignore
			};
			this.m_CheckmarkBackground.Add(this.m_CheckMark);
			this.m_CheckmarkBackground.AddToClassList(RadioButton.checkmarkBackgroundUssClassName);
			this.m_CheckMark.AddToClassList(RadioButton.checkmarkUssClassName);
			base.visualInput.Add(this.m_CheckmarkBackground);
			this.UpdateCheckmark();
			base.RegisterCallback<AttachToPanelEvent>(new EventCallback<AttachToPanelEvent>(this.OnOptionAttachToPanel), TrickleDown.NoTrickleDown);
			base.RegisterCallback<DetachFromPanelEvent>(new EventCallback<DetachFromPanelEvent>(this.OnOptionDetachFromPanel), TrickleDown.NoTrickleDown);
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x00033CB8 File Offset: 0x00031EB8
		private void OnOptionAttachToPanel(AttachToPanelEvent evt)
		{
			this.RegisterGroupBoxOption<RadioButton>();
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x00033CC2 File Offset: 0x00031EC2
		private void OnOptionDetachFromPanel(DetachFromPanelEvent evt)
		{
			this.UnregisterGroupBoxOption<RadioButton>();
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x00033CCC File Offset: 0x00031ECC
		protected override void InitLabel()
		{
			base.InitLabel();
			this.m_Label.AddToClassList(RadioButton.textUssClassName);
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x00033CE8 File Offset: 0x00031EE8
		protected override void ToggleValue()
		{
			bool flag = !this.value;
			if (flag)
			{
				this.value = true;
			}
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x00033D0D File Offset: 0x00031F0D
		[Obsolete("[UI Toolkit] Please set the value property instead.", false)]
		public void SetSelected(bool selected)
		{
			((IGroupBoxOption)this).SetSelected(selected);
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x00033D18 File Offset: 0x00031F18
		void IGroupBoxOption.SetSelected(bool selected)
		{
			this.value = selected;
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x00033D23 File Offset: 0x00031F23
		public override void SetValueWithoutNotify(bool newValue)
		{
			base.SetValueWithoutNotify(newValue);
			this.UpdateCheckmark();
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x00033D35 File Offset: 0x00031F35
		private void UpdateCheckmark()
		{
			this.m_CheckMark.style.display = (this.value ? DisplayStyle.Flex : DisplayStyle.None);
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x00033D5C File Offset: 0x00031F5C
		protected override void UpdateMixedValueContent()
		{
			base.UpdateMixedValueContent();
			bool showMixedValue = base.showMixedValue;
			if (showMixedValue)
			{
				this.m_CheckmarkBackground.RemoveFromHierarchy();
			}
			else
			{
				this.m_CheckmarkBackground.Add(this.m_CheckMark);
				base.visualInput.Add(this.m_CheckmarkBackground);
			}
		}

		// Token: 0x040006F8 RID: 1784
		public new static readonly string ussClassName = "unity-radio-button";

		// Token: 0x040006F9 RID: 1785
		public new static readonly string labelUssClassName = RadioButton.ussClassName + "__label";

		// Token: 0x040006FA RID: 1786
		public new static readonly string inputUssClassName = RadioButton.ussClassName + "__input";

		// Token: 0x040006FB RID: 1787
		public static readonly string checkmarkBackgroundUssClassName = RadioButton.ussClassName + "__checkmark-background";

		// Token: 0x040006FC RID: 1788
		public static readonly string checkmarkUssClassName = RadioButton.ussClassName + "__checkmark";

		// Token: 0x040006FD RID: 1789
		public static readonly string textUssClassName = RadioButton.ussClassName + "__text";

		// Token: 0x040006FE RID: 1790
		private VisualElement m_CheckmarkBackground;

		// Token: 0x02000175 RID: 373
		[ExcludeFromDocs]
		[Serializable]
		public new class UxmlSerializedData : BaseBoolField.UxmlSerializedData
		{
			// Token: 0x06000BD9 RID: 3033 RVA: 0x00033E2F File Offset: 0x0003202F
			[Conditional("UNITY_EDITOR")]
			public new static void Register()
			{
				UxmlDescriptionCache.RegisterType(typeof(RadioButton.UxmlSerializedData), new UxmlAttributeNames[]
				{
					new UxmlAttributeNames("text", "text", null, Array.Empty<string>())
				});
			}

			// Token: 0x06000BDA RID: 3034 RVA: 0x00033E64 File Offset: 0x00032064
			public override object CreateInstance()
			{
				return new RadioButton();
			}

			// Token: 0x06000BDB RID: 3035 RVA: 0x00033E6C File Offset: 0x0003206C
			public override void Deserialize(object obj)
			{
				base.Deserialize(obj);
				bool flag = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.text_UxmlAttributeFlags);
				if (flag)
				{
					RadioButton radioButton = (RadioButton)obj;
					radioButton.text = this.text;
				}
			}

			// Token: 0x040006FF RID: 1791
			[MultilineTextField]
			[SerializeField]
			private string text;

			// Token: 0x04000700 RID: 1792
			[HideInInspector]
			[UxmlIgnore]
			[SerializeField]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags text_UxmlAttributeFlags;
		}

		// Token: 0x02000176 RID: 374
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<RadioButton, RadioButton.UxmlTraits>
		{
		}

		// Token: 0x02000177 RID: 375
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : BaseFieldTraits<bool, UxmlBoolAttributeDescription>
		{
			// Token: 0x06000BDE RID: 3038 RVA: 0x00033EB9 File Offset: 0x000320B9
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				((RadioButton)ve).text = this.m_Text.GetValueFromBag(bag, cc);
			}

			// Token: 0x04000701 RID: 1793
			private UxmlStringAttributeDescription m_Text = new UxmlStringAttributeDescription
			{
				name = "text"
			};
		}
	}
}
