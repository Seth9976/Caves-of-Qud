using System;
using System.Diagnostics;
using UnityEngine.Internal;

namespace UnityEngine.UIElements
{
	// Token: 0x020001AD RID: 429
	public class Toggle : BaseBoolField
	{
		// Token: 0x06000D6A RID: 3434 RVA: 0x0003C2F4 File Offset: 0x0003A4F4
		public Toggle()
			: this(null)
		{
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x0003C300 File Offset: 0x0003A500
		public Toggle(string label)
			: base(label)
		{
			base.AddToClassList(Toggle.ussClassName);
			base.visualInput.AddToClassList(Toggle.inputUssClassName);
			base.labelElement.AddToClassList(Toggle.labelUssClassName);
			this.m_CheckMark.AddToClassList(Toggle.checkmarkUssClassName);
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x0003C355 File Offset: 0x0003A555
		protected override void InitLabel()
		{
			base.InitLabel();
			this.m_Label.AddToClassList(Toggle.textUssClassName);
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x0003C370 File Offset: 0x0003A570
		protected override void UpdateMixedValueContent()
		{
			bool showMixedValue = base.showMixedValue;
			if (showMixedValue)
			{
				base.visualInput.pseudoStates &= ~PseudoStates.Checked;
				base.pseudoStates &= ~PseudoStates.Checked;
				this.m_CheckMark.AddToClassList(Toggle.mixedValuesUssClassName);
			}
			else
			{
				this.m_CheckMark.RemoveFromClassList(Toggle.mixedValuesUssClassName);
				bool value = this.value;
				if (value)
				{
					base.visualInput.pseudoStates |= PseudoStates.Checked;
					base.pseudoStates |= PseudoStates.Checked;
				}
				else
				{
					base.visualInput.pseudoStates &= ~PseudoStates.Checked;
					base.pseudoStates &= ~PseudoStates.Checked;
				}
			}
		}

		// Token: 0x04000867 RID: 2151
		public new static readonly string ussClassName = "unity-toggle";

		// Token: 0x04000868 RID: 2152
		public new static readonly string labelUssClassName = Toggle.ussClassName + "__label";

		// Token: 0x04000869 RID: 2153
		public new static readonly string inputUssClassName = Toggle.ussClassName + "__input";

		// Token: 0x0400086A RID: 2154
		[Obsolete]
		public static readonly string noTextVariantUssClassName = Toggle.ussClassName + "--no-text";

		// Token: 0x0400086B RID: 2155
		public static readonly string checkmarkUssClassName = Toggle.ussClassName + "__checkmark";

		// Token: 0x0400086C RID: 2156
		public static readonly string textUssClassName = Toggle.ussClassName + "__text";

		// Token: 0x0400086D RID: 2157
		public static readonly string mixedValuesUssClassName = Toggle.ussClassName + "__mixed-values";

		// Token: 0x020001AE RID: 430
		[ExcludeFromDocs]
		[Serializable]
		public new class UxmlSerializedData : BaseBoolField.UxmlSerializedData
		{
			// Token: 0x06000D6F RID: 3439 RVA: 0x0003C4BF File Offset: 0x0003A6BF
			[Conditional("UNITY_EDITOR")]
			public new static void Register()
			{
				UxmlDescriptionCache.RegisterType(typeof(Toggle.UxmlSerializedData), new UxmlAttributeNames[]
				{
					new UxmlAttributeNames("text", "text", null, Array.Empty<string>())
				});
			}

			// Token: 0x06000D70 RID: 3440 RVA: 0x0003C4F4 File Offset: 0x0003A6F4
			public override object CreateInstance()
			{
				return new Toggle();
			}

			// Token: 0x06000D71 RID: 3441 RVA: 0x0003C4FC File Offset: 0x0003A6FC
			public override void Deserialize(object obj)
			{
				base.Deserialize(obj);
				bool flag = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.text_UxmlAttributeFlags);
				if (flag)
				{
					Toggle toggle = (Toggle)obj;
					toggle.text = this.text;
				}
			}

			// Token: 0x0400086E RID: 2158
			[SerializeField]
			[MultilineTextField]
			private string text;

			// Token: 0x0400086F RID: 2159
			[SerializeField]
			[UxmlIgnore]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags text_UxmlAttributeFlags;
		}

		// Token: 0x020001AF RID: 431
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<Toggle, Toggle.UxmlTraits>
		{
		}

		// Token: 0x020001B0 RID: 432
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : BaseFieldTraits<bool, UxmlBoolAttributeDescription>
		{
			// Token: 0x06000D74 RID: 3444 RVA: 0x0003C540 File Offset: 0x0003A740
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				((Toggle)ve).text = this.m_Text.GetValueFromBag(bag, cc);
			}

			// Token: 0x04000870 RID: 2160
			private UxmlStringAttributeDescription m_Text = new UxmlStringAttributeDescription
			{
				name = "text"
			};
		}
	}
}
