using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.Internal;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.UIElements
{
	// Token: 0x020000C4 RID: 196
	[MovedFrom(true, "UnityEditor.UIElements", "UnityEditor.UIElementsModule", null)]
	public class RectIntField : BaseCompositeField<RectInt, IntegerField, int>
	{
		// Token: 0x06000731 RID: 1841 RVA: 0x00020BA4 File Offset: 0x0001EDA4
		internal override BaseCompositeField<RectInt, IntegerField, int>.FieldDescription[] DescribeFields()
		{
			BaseCompositeField<RectInt, IntegerField, int>.FieldDescription[] array = new BaseCompositeField<RectInt, IntegerField, int>.FieldDescription[4];
			array[0] = new BaseCompositeField<RectInt, IntegerField, int>.FieldDescription("X", "unity-x-input", (RectInt r) => r.x, delegate(ref RectInt r, int v)
			{
				r.x = v;
			});
			array[1] = new BaseCompositeField<RectInt, IntegerField, int>.FieldDescription("Y", "unity-y-input", (RectInt r) => r.y, delegate(ref RectInt r, int v)
			{
				r.y = v;
			});
			array[2] = new BaseCompositeField<RectInt, IntegerField, int>.FieldDescription("W", "unity-width-input", (RectInt r) => r.width, delegate(ref RectInt r, int v)
			{
				r.width = v;
			});
			array[3] = new BaseCompositeField<RectInt, IntegerField, int>.FieldDescription("H", "unity-height-input", (RectInt r) => r.height, delegate(ref RectInt r, int v)
			{
				r.height = v;
			});
			return array;
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x00020D0C File Offset: 0x0001EF0C
		public RectIntField()
			: this(null)
		{
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x00020D18 File Offset: 0x0001EF18
		public RectIntField(string label)
			: base(label, 2)
		{
			base.AddToClassList(RectIntField.ussClassName);
			base.AddToClassList(BaseCompositeField<RectInt, IntegerField, int>.twoLinesVariantUssClassName);
			base.labelElement.AddToClassList(RectIntField.labelUssClassName);
			base.visualInput.AddToClassList(RectIntField.inputUssClassName);
		}

		// Token: 0x040003FE RID: 1022
		public new static readonly string ussClassName = "unity-rect-int-field";

		// Token: 0x040003FF RID: 1023
		public new static readonly string labelUssClassName = RectIntField.ussClassName + "__label";

		// Token: 0x04000400 RID: 1024
		public new static readonly string inputUssClassName = RectIntField.ussClassName + "__input";

		// Token: 0x020000C5 RID: 197
		[ExcludeFromDocs]
		[Serializable]
		public new class UxmlSerializedData : BaseCompositeField<RectInt, IntegerField, int>.UxmlSerializedData, IUxmlSerializedDataCustomAttributeHandler
		{
			// Token: 0x06000735 RID: 1845 RVA: 0x00020D9D File Offset: 0x0001EF9D
			[Conditional("UNITY_EDITOR")]
			public new static void Register()
			{
				BaseCompositeField<RectInt, IntegerField, int>.UxmlSerializedData.Register();
			}

			// Token: 0x06000736 RID: 1846 RVA: 0x00020DA6 File Offset: 0x0001EFA6
			public override object CreateInstance()
			{
				return new RectIntField();
			}

			// Token: 0x06000737 RID: 1847 RVA: 0x00020DB0 File Offset: 0x0001EFB0
			void IUxmlSerializedDataCustomAttributeHandler.SerializeCustomAttributes(IUxmlAttributes bag, HashSet<string> handledAttributes)
			{
				int num = 0;
				int num2 = UxmlUtility.TryParseIntAttribute("x", bag, ref num);
				int num3 = UxmlUtility.TryParseIntAttribute("y", bag, ref num);
				int num4 = UxmlUtility.TryParseIntAttribute("w", bag, ref num);
				int num5 = UxmlUtility.TryParseIntAttribute("h", bag, ref num);
				bool flag = num > 0;
				if (flag)
				{
					base.Value = new RectInt(num2, num3, num4, num5);
					handledAttributes.Add("value");
					UxmlAsset uxmlAsset = bag as UxmlAsset;
					bool flag2 = uxmlAsset != null;
					if (flag2)
					{
						uxmlAsset.RemoveAttribute("x");
						uxmlAsset.RemoveAttribute("y");
						uxmlAsset.RemoveAttribute("w");
						uxmlAsset.RemoveAttribute("h");
						uxmlAsset.SetAttribute("value", UxmlUtility.ValueToString(base.Value));
					}
				}
			}
		}

		// Token: 0x020000C6 RID: 198
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<RectIntField, RectIntField.UxmlTraits>
		{
		}

		// Token: 0x020000C7 RID: 199
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : BaseField<RectInt>.UxmlTraits
		{
			// Token: 0x0600073A RID: 1850 RVA: 0x00020E98 File Offset: 0x0001F098
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				RectIntField rectIntField = (RectIntField)ve;
				rectIntField.SetValueWithoutNotify(new RectInt(this.m_XValue.GetValueFromBag(bag, cc), this.m_YValue.GetValueFromBag(bag, cc), this.m_WValue.GetValueFromBag(bag, cc), this.m_HValue.GetValueFromBag(bag, cc)));
			}

			// Token: 0x04000401 RID: 1025
			private UxmlIntAttributeDescription m_XValue = new UxmlIntAttributeDescription
			{
				name = "x"
			};

			// Token: 0x04000402 RID: 1026
			private UxmlIntAttributeDescription m_YValue = new UxmlIntAttributeDescription
			{
				name = "y"
			};

			// Token: 0x04000403 RID: 1027
			private UxmlIntAttributeDescription m_WValue = new UxmlIntAttributeDescription
			{
				name = "w"
			};

			// Token: 0x04000404 RID: 1028
			private UxmlIntAttributeDescription m_HValue = new UxmlIntAttributeDescription
			{
				name = "h"
			};
		}
	}
}
