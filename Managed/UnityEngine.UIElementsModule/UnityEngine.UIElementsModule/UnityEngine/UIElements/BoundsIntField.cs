using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.Internal;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.UIElements
{
	// Token: 0x020000AF RID: 175
	[MovedFrom(true, "UnityEditor.UIElements", "UnityEditor.UIElementsModule", null)]
	public class BoundsIntField : BaseField<BoundsInt>
	{
		// Token: 0x060006DD RID: 1757 RVA: 0x0001F8BE File Offset: 0x0001DABE
		public BoundsIntField()
			: this(null)
		{
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x0001F8CC File Offset: 0x0001DACC
		public BoundsIntField(string label)
			: base(label, null)
		{
			base.delegatesFocus = false;
			base.visualInput.focusable = false;
			base.AddToClassList(BoundsIntField.ussClassName);
			base.visualInput.AddToClassList(BoundsIntField.inputUssClassName);
			base.labelElement.AddToClassList(BoundsIntField.labelUssClassName);
			this.m_PositionField = new Vector3IntField("Position");
			this.m_PositionField.name = "unity-m_Position-input";
			this.m_PositionField.delegatesFocus = true;
			this.m_PositionField.AddToClassList(BoundsIntField.positionUssClassName);
			this.m_PositionField.RegisterValueChangedCallback<Vector3Int>(delegate(ChangeEvent<Vector3Int> e)
			{
				BoundsInt value = this.value;
				value.position = e.newValue;
				this.value = value;
			});
			base.visualInput.hierarchy.Add(this.m_PositionField);
			this.m_SizeField = new Vector3IntField("Size");
			this.m_SizeField.name = "unity-m_Size-input";
			this.m_SizeField.delegatesFocus = true;
			this.m_SizeField.AddToClassList(BoundsIntField.sizeUssClassName);
			this.m_SizeField.RegisterValueChangedCallback<Vector3Int>(delegate(ChangeEvent<Vector3Int> e)
			{
				BoundsInt value2 = this.value;
				value2.size = e.newValue;
				this.value = value2;
			});
			base.visualInput.hierarchy.Add(this.m_SizeField);
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x0001FA08 File Offset: 0x0001DC08
		public override void SetValueWithoutNotify(BoundsInt newValue)
		{
			base.SetValueWithoutNotify(newValue);
			this.m_PositionField.SetValueWithoutNotify(base.rawValue.position);
			this.m_SizeField.SetValueWithoutNotify(base.rawValue.size);
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x0001FA52 File Offset: 0x0001DC52
		protected override void UpdateMixedValueContent()
		{
			this.m_PositionField.showMixedValue = base.showMixedValue;
			this.m_SizeField.showMixedValue = base.showMixedValue;
		}

		// Token: 0x040003C8 RID: 968
		private Vector3IntField m_PositionField;

		// Token: 0x040003C9 RID: 969
		private Vector3IntField m_SizeField;

		// Token: 0x040003CA RID: 970
		public new static readonly string ussClassName = "unity-bounds-int-field";

		// Token: 0x040003CB RID: 971
		public new static readonly string labelUssClassName = BoundsIntField.ussClassName + "__label";

		// Token: 0x040003CC RID: 972
		public new static readonly string inputUssClassName = BoundsIntField.ussClassName + "__input";

		// Token: 0x040003CD RID: 973
		public static readonly string positionUssClassName = BoundsIntField.ussClassName + "__position-field";

		// Token: 0x040003CE RID: 974
		public static readonly string sizeUssClassName = BoundsIntField.ussClassName + "__size-field";

		// Token: 0x020000B0 RID: 176
		[ExcludeFromDocs]
		[Serializable]
		public new class UxmlSerializedData : BaseField<BoundsInt>.UxmlSerializedData, IUxmlSerializedDataCustomAttributeHandler
		{
			// Token: 0x060006E4 RID: 1764 RVA: 0x0001FB3B File Offset: 0x0001DD3B
			[Conditional("UNITY_EDITOR")]
			public new static void Register()
			{
				BaseField<BoundsInt>.UxmlSerializedData.Register();
			}

			// Token: 0x060006E5 RID: 1765 RVA: 0x0001FB44 File Offset: 0x0001DD44
			public override object CreateInstance()
			{
				return new BoundsIntField();
			}

			// Token: 0x060006E6 RID: 1766 RVA: 0x0001FB4C File Offset: 0x0001DD4C
			void IUxmlSerializedDataCustomAttributeHandler.SerializeCustomAttributes(IUxmlAttributes bag, HashSet<string> handledAttributes)
			{
				int num = 0;
				int num2 = UxmlUtility.TryParseIntAttribute("px", bag, ref num);
				int num3 = UxmlUtility.TryParseIntAttribute("py", bag, ref num);
				int num4 = UxmlUtility.TryParseIntAttribute("pz", bag, ref num);
				int num5 = UxmlUtility.TryParseIntAttribute("sx", bag, ref num);
				int num6 = UxmlUtility.TryParseIntAttribute("sy", bag, ref num);
				int num7 = UxmlUtility.TryParseIntAttribute("sz", bag, ref num);
				bool flag = num > 0;
				if (flag)
				{
					base.Value = new BoundsInt(new Vector3Int(num2, num3, num4), new Vector3Int(num5, num6, num7));
					handledAttributes.Add("value");
					UxmlAsset uxmlAsset = bag as UxmlAsset;
					bool flag2 = uxmlAsset != null;
					if (flag2)
					{
						uxmlAsset.RemoveAttribute("px");
						uxmlAsset.RemoveAttribute("py");
						uxmlAsset.RemoveAttribute("pz");
						uxmlAsset.RemoveAttribute("sx");
						uxmlAsset.RemoveAttribute("sy");
						uxmlAsset.RemoveAttribute("sz");
						uxmlAsset.SetAttribute("value", UxmlUtility.ValueToString(base.Value));
					}
				}
			}
		}

		// Token: 0x020000B1 RID: 177
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<BoundsIntField, BoundsIntField.UxmlTraits>
		{
		}

		// Token: 0x020000B2 RID: 178
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : BaseField<BoundsInt>.UxmlTraits
		{
			// Token: 0x060006E9 RID: 1769 RVA: 0x0001FC7C File Offset: 0x0001DE7C
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				BoundsIntField boundsIntField = (BoundsIntField)ve;
				boundsIntField.SetValueWithoutNotify(new BoundsInt(new Vector3Int(this.m_PositionXValue.GetValueFromBag(bag, cc), this.m_PositionYValue.GetValueFromBag(bag, cc), this.m_PositionZValue.GetValueFromBag(bag, cc)), new Vector3Int(this.m_SizeXValue.GetValueFromBag(bag, cc), this.m_SizeYValue.GetValueFromBag(bag, cc), this.m_SizeZValue.GetValueFromBag(bag, cc))));
			}

			// Token: 0x040003CF RID: 975
			private UxmlIntAttributeDescription m_PositionXValue = new UxmlIntAttributeDescription
			{
				name = "px"
			};

			// Token: 0x040003D0 RID: 976
			private UxmlIntAttributeDescription m_PositionYValue = new UxmlIntAttributeDescription
			{
				name = "py"
			};

			// Token: 0x040003D1 RID: 977
			private UxmlIntAttributeDescription m_PositionZValue = new UxmlIntAttributeDescription
			{
				name = "pz"
			};

			// Token: 0x040003D2 RID: 978
			private UxmlIntAttributeDescription m_SizeXValue = new UxmlIntAttributeDescription
			{
				name = "sx"
			};

			// Token: 0x040003D3 RID: 979
			private UxmlIntAttributeDescription m_SizeYValue = new UxmlIntAttributeDescription
			{
				name = "sy"
			};

			// Token: 0x040003D4 RID: 980
			private UxmlIntAttributeDescription m_SizeZValue = new UxmlIntAttributeDescription
			{
				name = "sz"
			};
		}
	}
}
