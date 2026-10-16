using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.Internal;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.UIElements
{
	// Token: 0x020000D8 RID: 216
	[MovedFrom(true, "UnityEditor.UIElements", "UnityEditor.UIElementsModule", null)]
	public class Vector2IntField : BaseCompositeField<Vector2Int, IntegerField, int>
	{
		// Token: 0x0600077F RID: 1919 RVA: 0x000219B4 File Offset: 0x0001FBB4
		internal override BaseCompositeField<Vector2Int, IntegerField, int>.FieldDescription[] DescribeFields()
		{
			BaseCompositeField<Vector2Int, IntegerField, int>.FieldDescription[] array = new BaseCompositeField<Vector2Int, IntegerField, int>.FieldDescription[2];
			array[0] = new BaseCompositeField<Vector2Int, IntegerField, int>.FieldDescription("X", "unity-x-input", (Vector2Int r) => r.x, delegate(ref Vector2Int r, int v)
			{
				r.x = v;
			});
			array[1] = new BaseCompositeField<Vector2Int, IntegerField, int>.FieldDescription("Y", "unity-y-input", (Vector2Int r) => r.y, delegate(ref Vector2Int r, int v)
			{
				r.y = v;
			});
			return array;
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x00021A74 File Offset: 0x0001FC74
		public Vector2IntField()
			: this(null)
		{
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x00021A7F File Offset: 0x0001FC7F
		public Vector2IntField(string label)
			: base(label, 2)
		{
			base.AddToClassList(Vector2IntField.ussClassName);
			base.labelElement.AddToClassList(Vector2IntField.labelUssClassName);
			base.visualInput.AddToClassList(Vector2IntField.inputUssClassName);
		}

		// Token: 0x04000435 RID: 1077
		public new static readonly string ussClassName = "unity-vector2-int-field";

		// Token: 0x04000436 RID: 1078
		public new static readonly string labelUssClassName = Vector2IntField.ussClassName + "__label";

		// Token: 0x04000437 RID: 1079
		public new static readonly string inputUssClassName = Vector2IntField.ussClassName + "__input";

		// Token: 0x020000D9 RID: 217
		[ExcludeFromDocs]
		[Serializable]
		public new class UxmlSerializedData : BaseCompositeField<Vector2Int, IntegerField, int>.UxmlSerializedData, IUxmlSerializedDataCustomAttributeHandler
		{
			// Token: 0x06000783 RID: 1923 RVA: 0x00021AED File Offset: 0x0001FCED
			[Conditional("UNITY_EDITOR")]
			public new static void Register()
			{
				BaseCompositeField<Vector2Int, IntegerField, int>.UxmlSerializedData.Register();
			}

			// Token: 0x06000784 RID: 1924 RVA: 0x00021AF6 File Offset: 0x0001FCF6
			public override object CreateInstance()
			{
				return new Vector2IntField();
			}

			// Token: 0x06000785 RID: 1925 RVA: 0x00021B00 File Offset: 0x0001FD00
			void IUxmlSerializedDataCustomAttributeHandler.SerializeCustomAttributes(IUxmlAttributes bag, HashSet<string> handledAttributes)
			{
				int num = 0;
				int num2 = UxmlUtility.TryParseIntAttribute("x", bag, ref num);
				int num3 = UxmlUtility.TryParseIntAttribute("y", bag, ref num);
				bool flag = num > 0;
				if (flag)
				{
					base.Value = new Vector2Int(num2, num3);
					handledAttributes.Add("value");
					UxmlAsset uxmlAsset = bag as UxmlAsset;
					bool flag2 = uxmlAsset != null;
					if (flag2)
					{
						uxmlAsset.RemoveAttribute("x");
						uxmlAsset.RemoveAttribute("y");
						uxmlAsset.SetAttribute("value", UxmlUtility.ValueToString(base.Value));
					}
				}
			}
		}

		// Token: 0x020000DA RID: 218
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<Vector2IntField, Vector2IntField.UxmlTraits>
		{
		}

		// Token: 0x020000DB RID: 219
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : BaseField<Vector2Int>.UxmlTraits
		{
			// Token: 0x06000788 RID: 1928 RVA: 0x00021BAC File Offset: 0x0001FDAC
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				Vector2IntField vector2IntField = (Vector2IntField)ve;
				vector2IntField.SetValueWithoutNotify(new Vector2Int(this.m_XValue.GetValueFromBag(bag, cc), this.m_YValue.GetValueFromBag(bag, cc)));
			}

			// Token: 0x04000438 RID: 1080
			private UxmlIntAttributeDescription m_XValue = new UxmlIntAttributeDescription
			{
				name = "x"
			};

			// Token: 0x04000439 RID: 1081
			private UxmlIntAttributeDescription m_YValue = new UxmlIntAttributeDescription
			{
				name = "y"
			};
		}
	}
}
