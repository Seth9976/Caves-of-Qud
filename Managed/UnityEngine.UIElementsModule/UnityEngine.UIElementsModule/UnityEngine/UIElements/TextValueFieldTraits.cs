using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.UIElements
{
	// Token: 0x020001AC RID: 428
	[Obsolete("TextValueFieldTraits<TValueType, TValueUxmlAttributeType> is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
	[MovedFrom(true, "UnityEditor.UIElements", "UnityEditor.UIElementsModule", null)]
	public class TextValueFieldTraits<TValueType, TValueUxmlAttributeType> : BaseFieldTraits<TValueType, TValueUxmlAttributeType> where TValueUxmlAttributeType : TypedUxmlAttributeDescription<TValueType>, new()
	{
		// Token: 0x06000D68 RID: 3432 RVA: 0x0003C200 File Offset: 0x0003A400
		public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
		{
			base.Init(ve, bag, cc);
			TextInputBaseField<TValueType> textInputBaseField = (TextInputBaseField<TValueType>)ve;
			bool flag = textInputBaseField != null;
			if (flag)
			{
				textInputBaseField.textEdition.placeholder = this.m_PlaceholderText.GetValueFromBag(bag, cc);
				textInputBaseField.textEdition.hidePlaceholderOnFocus = this.m_HidePlaceholderOnFocus.GetValueFromBag(bag, cc);
				textInputBaseField.isReadOnly = this.m_IsReadOnly.GetValueFromBag(bag, cc);
				textInputBaseField.isDelayed = this.m_IsDelayed.GetValueFromBag(bag, cc);
			}
		}

		// Token: 0x04000863 RID: 2147
		private UxmlStringAttributeDescription m_PlaceholderText = new UxmlStringAttributeDescription
		{
			name = "placeholder-text"
		};

		// Token: 0x04000864 RID: 2148
		private UxmlBoolAttributeDescription m_HidePlaceholderOnFocus = new UxmlBoolAttributeDescription
		{
			name = "hide-placeholder-on-focus"
		};

		// Token: 0x04000865 RID: 2149
		private UxmlBoolAttributeDescription m_IsReadOnly = new UxmlBoolAttributeDescription
		{
			name = "readonly"
		};

		// Token: 0x04000866 RID: 2150
		private UxmlBoolAttributeDescription m_IsDelayed = new UxmlBoolAttributeDescription
		{
			name = "is-delayed"
		};
	}
}
