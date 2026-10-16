using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.Internal;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.UIElements
{
	// Token: 0x020000DD RID: 221
	[MovedFrom(true, "UnityEditor.UIElements", "UnityEditor.UIElementsModule", null)]
	public class Vector3IntField : BaseCompositeField<Vector3Int, IntegerField, int>
	{
		// Token: 0x06000790 RID: 1936 RVA: 0x00021C5C File Offset: 0x0001FE5C
		internal override BaseCompositeField<Vector3Int, IntegerField, int>.FieldDescription[] DescribeFields()
		{
			BaseCompositeField<Vector3Int, IntegerField, int>.FieldDescription[] array = new BaseCompositeField<Vector3Int, IntegerField, int>.FieldDescription[3];
			array[0] = new BaseCompositeField<Vector3Int, IntegerField, int>.FieldDescription("X", "unity-x-input", (Vector3Int r) => r.x, delegate(ref Vector3Int r, int v)
			{
				r.x = v;
			});
			array[1] = new BaseCompositeField<Vector3Int, IntegerField, int>.FieldDescription("Y", "unity-y-input", (Vector3Int r) => r.y, delegate(ref Vector3Int r, int v)
			{
				r.y = v;
			});
			array[2] = new BaseCompositeField<Vector3Int, IntegerField, int>.FieldDescription("Z", "unity-z-input", (Vector3Int r) => r.z, delegate(ref Vector3Int r, int v)
			{
				r.z = v;
			});
			return array;
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x00021D70 File Offset: 0x0001FF70
		public Vector3IntField()
			: this(null)
		{
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x00021D7B File Offset: 0x0001FF7B
		public Vector3IntField(string label)
			: base(label, 3)
		{
			base.AddToClassList(Vector3IntField.ussClassName);
			base.labelElement.AddToClassList(Vector3IntField.labelUssClassName);
			base.visualInput.AddToClassList(Vector3IntField.inputUssClassName);
		}

		// Token: 0x0400043F RID: 1087
		public new static readonly string ussClassName = "unity-vector3-int-field";

		// Token: 0x04000440 RID: 1088
		public new static readonly string labelUssClassName = Vector3IntField.ussClassName + "__label";

		// Token: 0x04000441 RID: 1089
		public new static readonly string inputUssClassName = Vector3IntField.ussClassName + "__input";

		// Token: 0x020000DE RID: 222
		[ExcludeFromDocs]
		[Serializable]
		public new class UxmlSerializedData : BaseCompositeField<Vector3Int, IntegerField, int>.UxmlSerializedData, IUxmlSerializedDataCustomAttributeHandler
		{
			// Token: 0x06000794 RID: 1940 RVA: 0x00021DE9 File Offset: 0x0001FFE9
			[Conditional("UNITY_EDITOR")]
			public new static void Register()
			{
				BaseCompositeField<Vector3Int, IntegerField, int>.UxmlSerializedData.Register();
			}

			// Token: 0x06000795 RID: 1941 RVA: 0x00021DF2 File Offset: 0x0001FFF2
			public override object CreateInstance()
			{
				return new Vector3IntField();
			}

			// Token: 0x06000796 RID: 1942 RVA: 0x00021DFC File Offset: 0x0001FFFC
			void IUxmlSerializedDataCustomAttributeHandler.SerializeCustomAttributes(IUxmlAttributes bag, HashSet<string> handledAttributes)
			{
				int num = 0;
				int num2 = UxmlUtility.TryParseIntAttribute("x", bag, ref num);
				int num3 = UxmlUtility.TryParseIntAttribute("y", bag, ref num);
				int num4 = UxmlUtility.TryParseIntAttribute("z", bag, ref num);
				bool flag = num > 0;
				if (flag)
				{
					base.Value = new Vector3Int(num2, num3, num4);
					handledAttributes.Add("value");
					UxmlAsset uxmlAsset = bag as UxmlAsset;
					bool flag2 = uxmlAsset != null;
					if (flag2)
					{
						uxmlAsset.RemoveAttribute("x");
						uxmlAsset.RemoveAttribute("y");
						uxmlAsset.RemoveAttribute("z");
						uxmlAsset.SetAttribute("value", UxmlUtility.ValueToString(base.Value));
					}
				}
			}
		}

		// Token: 0x020000DF RID: 223
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<Vector3IntField, Vector3IntField.UxmlTraits>
		{
		}

		// Token: 0x020000E0 RID: 224
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : BaseField<Vector3Int>.UxmlTraits
		{
			// Token: 0x06000799 RID: 1945 RVA: 0x00021EC4 File Offset: 0x000200C4
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				Vector3IntField vector3IntField = (Vector3IntField)ve;
				vector3IntField.SetValueWithoutNotify(new Vector3Int(this.m_XValue.GetValueFromBag(bag, cc), this.m_YValue.GetValueFromBag(bag, cc), this.m_ZValue.GetValueFromBag(bag, cc)));
			}

			// Token: 0x04000442 RID: 1090
			private UxmlIntAttributeDescription m_XValue = new UxmlIntAttributeDescription
			{
				name = "x"
			};

			// Token: 0x04000443 RID: 1091
			private UxmlIntAttributeDescription m_YValue = new UxmlIntAttributeDescription
			{
				name = "y"
			};

			// Token: 0x04000444 RID: 1092
			private UxmlIntAttributeDescription m_ZValue = new UxmlIntAttributeDescription
			{
				name = "z"
			};
		}
	}
}
