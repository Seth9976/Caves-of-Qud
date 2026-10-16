using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.Internal;

namespace UnityEngine.UIElements
{
	// Token: 0x020000E7 RID: 231
	public class DropdownField : PopupField<string>
	{
		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060007B7 RID: 1975 RVA: 0x00022212 File Offset: 0x00020412
		// (set) Token: 0x060007B8 RID: 1976 RVA: 0x0002221A File Offset: 0x0002041A
		internal int valueOverride { get; set; }

		// Token: 0x060007B9 RID: 1977 RVA: 0x00022223 File Offset: 0x00020423
		public DropdownField()
			: this(null)
		{
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x0002222E File Offset: 0x0002042E
		public DropdownField(string label)
			: base(label)
		{
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x00022239 File Offset: 0x00020439
		public DropdownField(List<string> choices, string defaultValue, Func<string, string> formatSelectedValueCallback = null, Func<string, string> formatListItemCallback = null)
			: this(null, choices, defaultValue, formatSelectedValueCallback, formatListItemCallback)
		{
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x00022249 File Offset: 0x00020449
		public DropdownField(string label, List<string> choices, string defaultValue, Func<string, string> formatSelectedValueCallback = null, Func<string, string> formatListItemCallback = null)
			: base(label, choices, defaultValue, formatSelectedValueCallback, formatListItemCallback)
		{
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x0002225A File Offset: 0x0002045A
		public DropdownField(List<string> choices, int defaultIndex, Func<string, string> formatSelectedValueCallback = null, Func<string, string> formatListItemCallback = null)
			: this(null, choices, defaultIndex, formatSelectedValueCallback, formatListItemCallback)
		{
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x0002226A File Offset: 0x0002046A
		public DropdownField(string label, List<string> choices, int defaultIndex, Func<string, string> formatSelectedValueCallback = null, Func<string, string> formatListItemCallback = null)
			: base(label, choices, defaultIndex, formatSelectedValueCallback, formatListItemCallback)
		{
		}

		// Token: 0x020000E8 RID: 232
		[ExcludeFromDocs]
		[Serializable]
		public new class UxmlSerializedData : BaseField<string>.UxmlSerializedData
		{
			// Token: 0x060007BF RID: 1983 RVA: 0x0002227C File Offset: 0x0002047C
			[Conditional("UNITY_EDITOR")]
			public new static void Register()
			{
				BaseField<string>.UxmlSerializedData.Register();
				UxmlDescriptionCache.RegisterType(typeof(DropdownField.UxmlSerializedData), new UxmlAttributeNames[]
				{
					new UxmlAttributeNames("index", "index", null, Array.Empty<string>()),
					new UxmlAttributeNames("choices", "choices", null, Array.Empty<string>()),
					new UxmlAttributeNames("valueOverride", "value", null, Array.Empty<string>())
				});
			}

			// Token: 0x060007C0 RID: 1984 RVA: 0x000222FA File Offset: 0x000204FA
			public override object CreateInstance()
			{
				return new DropdownField();
			}

			// Token: 0x060007C1 RID: 1985 RVA: 0x00022304 File Offset: 0x00020504
			public override void Deserialize(object obj)
			{
				base.Deserialize(obj);
				DropdownField dropdownField = (DropdownField)obj;
				bool flag = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.choices_UxmlAttributeFlags) && this.choices != null;
				if (flag)
				{
					dropdownField.choices = new List<string>(this.choices);
				}
				bool flag2 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.index_UxmlAttributeFlags) && this.index != -1;
				if (flag2)
				{
					dropdownField.index = this.index;
				}
				bool flag3 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.valueOverride_UxmlAttributeFlags);
				if (flag3)
				{
					dropdownField.valueOverride = this.valueOverride;
				}
			}

			// Token: 0x04000450 RID: 1104
			[SerializeField]
			[Delayed]
			private int index;

			// Token: 0x04000451 RID: 1105
			[HideInInspector]
			[UxmlIgnore]
			[SerializeField]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags index_UxmlAttributeFlags;

			// Token: 0x04000452 RID: 1106
			[SerializeField]
			private List<string> choices;

			// Token: 0x04000453 RID: 1107
			[UxmlIgnore]
			[HideInInspector]
			[SerializeField]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags choices_UxmlAttributeFlags;

			// Token: 0x04000454 RID: 1108
			[UxmlAttribute("value")]
			[HideInInspector]
			[SerializeField]
			private int valueOverride;

			// Token: 0x04000455 RID: 1109
			[SerializeField]
			[UxmlIgnore]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags valueOverride_UxmlAttributeFlags;
		}

		// Token: 0x020000E9 RID: 233
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<DropdownField, DropdownField.UxmlTraits>
		{
		}

		// Token: 0x020000EA RID: 234
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : BaseField<string>.UxmlTraits
		{
			// Token: 0x060007C4 RID: 1988 RVA: 0x000223B0 File Offset: 0x000205B0
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				DropdownField dropdownField = (DropdownField)ve;
				List<string> list = UxmlUtility.ParseStringListAttribute(this.m_Choices.GetValueFromBag(bag, cc));
				bool flag = list != null;
				if (flag)
				{
					dropdownField.choices = list;
				}
				dropdownField.index = this.m_Index.GetValueFromBag(bag, cc);
			}

			// Token: 0x04000456 RID: 1110
			private UxmlIntAttributeDescription m_Index = new UxmlIntAttributeDescription
			{
				name = "index"
			};

			// Token: 0x04000457 RID: 1111
			private UxmlStringAttributeDescription m_Choices = new UxmlStringAttributeDescription
			{
				name = "choices"
			};
		}
	}
}
