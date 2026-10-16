using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.Internal;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.UIElements
{
	// Token: 0x020000AB RID: 171
	[MovedFrom(true, "UnityEditor.UIElements", "UnityEditor.UIElementsModule", null)]
	public class BoundsField : BaseField<Bounds>
	{
		// Token: 0x060006CF RID: 1743 RVA: 0x0001F3E1 File Offset: 0x0001D5E1
		public BoundsField()
			: this(null)
		{
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x0001F3EC File Offset: 0x0001D5EC
		public BoundsField(string label)
			: base(label, null)
		{
			base.delegatesFocus = false;
			base.visualInput.focusable = false;
			base.AddToClassList(BoundsField.ussClassName);
			base.visualInput.AddToClassList(BoundsField.inputUssClassName);
			base.labelElement.AddToClassList(BoundsField.labelUssClassName);
			this.m_CenterField = new Vector3Field("Center");
			this.m_CenterField.name = "unity-m_Center-input";
			this.m_CenterField.delegatesFocus = true;
			this.m_CenterField.AddToClassList(BoundsField.centerFieldUssClassName);
			this.m_CenterField.RegisterValueChangedCallback<Vector3>(delegate(ChangeEvent<Vector3> e)
			{
				Bounds value = this.value;
				value.center = e.newValue;
				this.value = value;
			});
			base.visualInput.hierarchy.Add(this.m_CenterField);
			this.m_ExtentsField = new Vector3Field("Extents");
			this.m_ExtentsField.name = "unity-m_Extent-input";
			this.m_ExtentsField.delegatesFocus = true;
			this.m_ExtentsField.AddToClassList(BoundsField.extentsFieldUssClassName);
			this.m_ExtentsField.RegisterValueChangedCallback<Vector3>(delegate(ChangeEvent<Vector3> e)
			{
				Bounds value2 = this.value;
				value2.extents = e.newValue;
				this.value = value2;
			});
			base.visualInput.hierarchy.Add(this.m_ExtentsField);
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x0001F528 File Offset: 0x0001D728
		public override void SetValueWithoutNotify(Bounds newValue)
		{
			base.SetValueWithoutNotify(newValue);
			this.m_CenterField.SetValueWithoutNotify(base.rawValue.center);
			this.m_ExtentsField.SetValueWithoutNotify(base.rawValue.extents);
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x0001F572 File Offset: 0x0001D772
		protected override void UpdateMixedValueContent()
		{
			this.m_CenterField.showMixedValue = base.showMixedValue;
			this.m_ExtentsField.showMixedValue = base.showMixedValue;
		}

		// Token: 0x040003BB RID: 955
		public new static readonly string ussClassName = "unity-bounds-field";

		// Token: 0x040003BC RID: 956
		public new static readonly string labelUssClassName = BoundsField.ussClassName + "__label";

		// Token: 0x040003BD RID: 957
		public new static readonly string inputUssClassName = BoundsField.ussClassName + "__input";

		// Token: 0x040003BE RID: 958
		public static readonly string centerFieldUssClassName = BoundsField.ussClassName + "__center-field";

		// Token: 0x040003BF RID: 959
		public static readonly string extentsFieldUssClassName = BoundsField.ussClassName + "__extents-field";

		// Token: 0x040003C0 RID: 960
		private Vector3Field m_CenterField;

		// Token: 0x040003C1 RID: 961
		private Vector3Field m_ExtentsField;

		// Token: 0x020000AC RID: 172
		[ExcludeFromDocs]
		[Serializable]
		public new class UxmlSerializedData : BaseField<Bounds>.UxmlSerializedData, IUxmlSerializedDataCustomAttributeHandler
		{
			// Token: 0x060006D6 RID: 1750 RVA: 0x0001F65B File Offset: 0x0001D85B
			[Conditional("UNITY_EDITOR")]
			public new static void Register()
			{
				BaseField<Bounds>.UxmlSerializedData.Register();
			}

			// Token: 0x060006D7 RID: 1751 RVA: 0x0001F664 File Offset: 0x0001D864
			public override object CreateInstance()
			{
				return new BoundsField();
			}

			// Token: 0x060006D8 RID: 1752 RVA: 0x0001F66C File Offset: 0x0001D86C
			void IUxmlSerializedDataCustomAttributeHandler.SerializeCustomAttributes(IUxmlAttributes bag, HashSet<string> handledAttributes)
			{
				int num = 0;
				float num2 = UxmlUtility.TryParseFloatAttribute("cx", bag, ref num);
				float num3 = UxmlUtility.TryParseFloatAttribute("cy", bag, ref num);
				float num4 = UxmlUtility.TryParseFloatAttribute("cz", bag, ref num);
				float num5 = UxmlUtility.TryParseFloatAttribute("ex", bag, ref num);
				float num6 = UxmlUtility.TryParseFloatAttribute("ey", bag, ref num);
				float num7 = UxmlUtility.TryParseFloatAttribute("ez", bag, ref num);
				bool flag = num > 0;
				if (flag)
				{
					base.Value = new Bounds(new Vector3(num2, num3, num4), new Vector3(num5, num6, num7));
					handledAttributes.Add("value");
					UxmlAsset uxmlAsset = bag as UxmlAsset;
					bool flag2 = uxmlAsset != null;
					if (flag2)
					{
						uxmlAsset.RemoveAttribute("cx");
						uxmlAsset.RemoveAttribute("cy");
						uxmlAsset.RemoveAttribute("cz");
						uxmlAsset.RemoveAttribute("ex");
						uxmlAsset.RemoveAttribute("ey");
						uxmlAsset.RemoveAttribute("ez");
						uxmlAsset.SetAttribute("value", UxmlUtility.ValueToString(base.Value));
					}
				}
			}
		}

		// Token: 0x020000AD RID: 173
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<BoundsField, BoundsField.UxmlTraits>
		{
		}

		// Token: 0x020000AE RID: 174
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : BaseField<Bounds>.UxmlTraits
		{
			// Token: 0x060006DB RID: 1755 RVA: 0x0001F79C File Offset: 0x0001D99C
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				BoundsField boundsField = (BoundsField)ve;
				boundsField.SetValueWithoutNotify(new Bounds(new Vector3(this.m_CenterXValue.GetValueFromBag(bag, cc), this.m_CenterYValue.GetValueFromBag(bag, cc), this.m_CenterZValue.GetValueFromBag(bag, cc)), new Vector3(this.m_ExtentsXValue.GetValueFromBag(bag, cc), this.m_ExtentsYValue.GetValueFromBag(bag, cc), this.m_ExtentsZValue.GetValueFromBag(bag, cc))));
			}

			// Token: 0x040003C2 RID: 962
			private UxmlFloatAttributeDescription m_CenterXValue = new UxmlFloatAttributeDescription
			{
				name = "cx"
			};

			// Token: 0x040003C3 RID: 963
			private UxmlFloatAttributeDescription m_CenterYValue = new UxmlFloatAttributeDescription
			{
				name = "cy"
			};

			// Token: 0x040003C4 RID: 964
			private UxmlFloatAttributeDescription m_CenterZValue = new UxmlFloatAttributeDescription
			{
				name = "cz"
			};

			// Token: 0x040003C5 RID: 965
			private UxmlFloatAttributeDescription m_ExtentsXValue = new UxmlFloatAttributeDescription
			{
				name = "ex"
			};

			// Token: 0x040003C6 RID: 966
			private UxmlFloatAttributeDescription m_ExtentsYValue = new UxmlFloatAttributeDescription
			{
				name = "ey"
			};

			// Token: 0x040003C7 RID: 967
			private UxmlFloatAttributeDescription m_ExtentsZValue = new UxmlFloatAttributeDescription
			{
				name = "ez"
			};
		}
	}
}
