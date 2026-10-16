using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.Internal;

namespace UnityEngine.UIElements
{
	// Token: 0x020000BA RID: 186
	internal class ButtonStripField : BaseField<int>
	{
		// Token: 0x0600070B RID: 1803 RVA: 0x0002043C File Offset: 0x0001E63C
		public void AddButton(string text, string name = "")
		{
			Button button = this.CreateButton(name);
			button.text = text;
			base.Add(button);
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x00020464 File Offset: 0x0001E664
		public void AddButton(Background icon, string name = "")
		{
			Button button = this.CreateButton(name);
			VisualElement visualElement = new VisualElement();
			visualElement.AddToClassList("unity-button-strip-field__button-icon");
			visualElement.style.backgroundImage = icon;
			button.Add(visualElement);
			base.Add(button);
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x000204B0 File Offset: 0x0001E6B0
		private Button CreateButton(string name)
		{
			Button button = new Button
			{
				name = name
			};
			button.AddToClassList("unity-button-strip-field__button");
			button.RegisterCallback<DetachFromPanelEvent>(new EventCallback<DetachFromPanelEvent>(this.OnButtonDetachFromPanel), TrickleDown.NoTrickleDown);
			button.clicked += delegate
			{
				this.value = this.m_Buttons.IndexOf(button);
			};
			this.m_Buttons.Add(button);
			base.Add(button);
			this.RefreshButtonsStyling();
			return button;
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x00020550 File Offset: 0x0001E750
		private void OnButtonDetachFromPanel(DetachFromPanelEvent evt)
		{
			VisualElement visualElement = evt.currentTarget as VisualElement;
			ButtonStripField buttonStripField;
			bool flag;
			if (visualElement != null)
			{
				buttonStripField = visualElement.parent as ButtonStripField;
				flag = buttonStripField != null;
			}
			else
			{
				flag = false;
			}
			bool flag2 = flag;
			if (flag2)
			{
				buttonStripField.RefreshButtonsStyling();
				buttonStripField.EnsureValueIsValid();
			}
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x00020594 File Offset: 0x0001E794
		private void RefreshButtonsStyling()
		{
			for (int i = 0; i < this.m_Buttons.Count; i++)
			{
				Button button = this.m_Buttons[i];
				bool flag = this.m_Buttons.Count == 1;
				bool flag2 = i == 0;
				bool flag3 = i == this.m_Buttons.Count - 1;
				button.EnableInClassList("unity-button-strip-field__button--alone", flag);
				button.EnableInClassList("unity-button-strip-field__button--left", !flag && flag2);
				button.EnableInClassList("unity-button-strip-field__button--right", !flag && flag3);
				button.EnableInClassList("unity-button-strip-field__button--middle", !flag && !flag2 && !flag3);
			}
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x00020645 File Offset: 0x0001E845
		public ButtonStripField()
			: base(null)
		{
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x0002065B File Offset: 0x0001E85B
		public ButtonStripField(string label)
			: base(label)
		{
			base.AddToClassList("unity-button-strip-field");
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x0002067D File Offset: 0x0001E87D
		public override void SetValueWithoutNotify(int newValue)
		{
			newValue = Mathf.Clamp(newValue, 0, this.m_Buttons.Count - 1);
			base.SetValueWithoutNotify(newValue);
			this.RefreshButtonsState();
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x000206A5 File Offset: 0x0001E8A5
		private void EnsureValueIsValid()
		{
			this.SetValueWithoutNotify(Mathf.Clamp(this.value, 0, this.m_Buttons.Count - 1));
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x000206C8 File Offset: 0x0001E8C8
		private void RefreshButtonsState()
		{
			for (int i = 0; i < this.m_Buttons.Count; i++)
			{
				bool flag = i == this.value;
				if (flag)
				{
					this.m_Buttons[i].pseudoStates |= PseudoStates.Checked;
				}
				else
				{
					this.m_Buttons[i].pseudoStates &= ~PseudoStates.Checked;
				}
			}
		}

		// Token: 0x040003E4 RID: 996
		public const string className = "unity-button-strip-field";

		// Token: 0x040003E5 RID: 997
		private const string k_ButtonClass = "unity-button-strip-field__button";

		// Token: 0x040003E6 RID: 998
		private const string k_IconClass = "unity-button-strip-field__button-icon";

		// Token: 0x040003E7 RID: 999
		private const string k_ButtonLeftClass = "unity-button-strip-field__button--left";

		// Token: 0x040003E8 RID: 1000
		private const string k_ButtonMiddleClass = "unity-button-strip-field__button--middle";

		// Token: 0x040003E9 RID: 1001
		private const string k_ButtonRightClass = "unity-button-strip-field__button--right";

		// Token: 0x040003EA RID: 1002
		private const string k_ButtonAloneClass = "unity-button-strip-field__button--alone";

		// Token: 0x040003EB RID: 1003
		private readonly List<Button> m_Buttons = new List<Button>();

		// Token: 0x020000BB RID: 187
		[ExcludeFromDocs]
		[Serializable]
		public new class UxmlSerializedData : BaseField<int>.UxmlSerializedData
		{
			// Token: 0x06000715 RID: 1813 RVA: 0x00020736 File Offset: 0x0001E936
			[Conditional("UNITY_EDITOR")]
			public new static void Register()
			{
				BaseField<int>.UxmlSerializedData.Register();
			}

			// Token: 0x06000716 RID: 1814 RVA: 0x0002073F File Offset: 0x0001E93F
			public override object CreateInstance()
			{
				return new ButtonStripField();
			}
		}

		// Token: 0x020000BC RID: 188
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<ButtonStripField, ButtonStripField.UxmlTraits>
		{
		}

		// Token: 0x020000BD RID: 189
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : BaseField<int>.UxmlTraits
		{
		}
	}
}
