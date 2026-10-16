using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.Internal;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.UIElements
{
	// Token: 0x020000BF RID: 191
	[MovedFrom(true, "UnityEditor.UIElements", "UnityEditor.UIElementsModule", null)]
	public class RectField : BaseCompositeField<Rect, FloatField, float>
	{
		// Token: 0x0600071C RID: 1820 RVA: 0x00020788 File Offset: 0x0001E988
		internal override BaseCompositeField<Rect, FloatField, float>.FieldDescription[] DescribeFields()
		{
			BaseCompositeField<Rect, FloatField, float>.FieldDescription[] array = new BaseCompositeField<Rect, FloatField, float>.FieldDescription[4];
			array[0] = new BaseCompositeField<Rect, FloatField, float>.FieldDescription("X", "unity-x-input", (Rect r) => r.x, delegate(ref Rect r, float v)
			{
				r.x = v;
			});
			array[1] = new BaseCompositeField<Rect, FloatField, float>.FieldDescription("Y", "unity-y-input", (Rect r) => r.y, delegate(ref Rect r, float v)
			{
				r.y = v;
			});
			array[2] = new BaseCompositeField<Rect, FloatField, float>.FieldDescription("W", "unity-width-input", (Rect r) => r.width, delegate(ref Rect r, float v)
			{
				r.width = v;
			});
			array[3] = new BaseCompositeField<Rect, FloatField, float>.FieldDescription("H", "unity-height-input", (Rect r) => r.height, delegate(ref Rect r, float v)
			{
				r.height = v;
			});
			return array;
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x000208F0 File Offset: 0x0001EAF0
		public RectField()
			: this(null)
		{
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x000208FC File Offset: 0x0001EAFC
		public RectField(string label)
			: base(label, 2)
		{
			base.AddToClassList(RectField.ussClassName);
			base.AddToClassList(BaseCompositeField<Rect, FloatField, float>.twoLinesVariantUssClassName);
			base.labelElement.AddToClassList(RectField.labelUssClassName);
			base.visualInput.AddToClassList(RectField.inputUssClassName);
		}

		// Token: 0x040003EE RID: 1006
		public new static readonly string ussClassName = "unity-rect-field";

		// Token: 0x040003EF RID: 1007
		public new static readonly string labelUssClassName = RectField.ussClassName + "__label";

		// Token: 0x040003F0 RID: 1008
		public new static readonly string inputUssClassName = RectField.ussClassName + "__input";

		// Token: 0x020000C0 RID: 192
		[ExcludeFromDocs]
		[Serializable]
		public new class UxmlSerializedData : BaseCompositeField<Rect, FloatField, float>.UxmlSerializedData, IUxmlSerializedDataCustomAttributeHandler
		{
			// Token: 0x06000720 RID: 1824 RVA: 0x00020981 File Offset: 0x0001EB81
			[Conditional("UNITY_EDITOR")]
			public new static void Register()
			{
				BaseCompositeField<Rect, FloatField, float>.UxmlSerializedData.Register();
			}

			// Token: 0x06000721 RID: 1825 RVA: 0x0002098A File Offset: 0x0001EB8A
			public override object CreateInstance()
			{
				return new RectField();
			}

			// Token: 0x06000722 RID: 1826 RVA: 0x00020994 File Offset: 0x0001EB94
			void IUxmlSerializedDataCustomAttributeHandler.SerializeCustomAttributes(IUxmlAttributes bag, HashSet<string> handledAttributes)
			{
				int num = 0;
				float num2 = UxmlUtility.TryParseFloatAttribute("x", bag, ref num);
				float num3 = UxmlUtility.TryParseFloatAttribute("y", bag, ref num);
				float num4 = UxmlUtility.TryParseFloatAttribute("w", bag, ref num);
				float num5 = UxmlUtility.TryParseFloatAttribute("h", bag, ref num);
				bool flag = num > 0;
				if (flag)
				{
					base.Value = new Rect(num2, num3, num4, num5);
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

		// Token: 0x020000C1 RID: 193
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<RectField, RectField.UxmlTraits>
		{
		}

		// Token: 0x020000C2 RID: 194
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : BaseField<Rect>.UxmlTraits
		{
			// Token: 0x06000725 RID: 1829 RVA: 0x00020A7C File Offset: 0x0001EC7C
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				RectField rectField = (RectField)ve;
				rectField.SetValueWithoutNotify(new Rect(this.m_XValue.GetValueFromBag(bag, cc), this.m_YValue.GetValueFromBag(bag, cc), this.m_WValue.GetValueFromBag(bag, cc), this.m_HValue.GetValueFromBag(bag, cc)));
			}

			// Token: 0x040003F1 RID: 1009
			private UxmlFloatAttributeDescription m_XValue = new UxmlFloatAttributeDescription
			{
				name = "x"
			};

			// Token: 0x040003F2 RID: 1010
			private UxmlFloatAttributeDescription m_YValue = new UxmlFloatAttributeDescription
			{
				name = "y"
			};

			// Token: 0x040003F3 RID: 1011
			private UxmlFloatAttributeDescription m_WValue = new UxmlFloatAttributeDescription
			{
				name = "w"
			};

			// Token: 0x040003F4 RID: 1012
			private UxmlFloatAttributeDescription m_HValue = new UxmlFloatAttributeDescription
			{
				name = "h"
			};
		}
	}
}
