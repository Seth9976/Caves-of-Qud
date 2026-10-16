using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.Internal;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.UIElements
{
	// Token: 0x020000D3 RID: 211
	[MovedFrom(true, "UnityEditor.UIElements", "UnityEditor.UIElementsModule", null)]
	public class Vector4Field : BaseCompositeField<Vector4, FloatField, float>
	{
		// Token: 0x0600076A RID: 1898 RVA: 0x000215B8 File Offset: 0x0001F7B8
		internal override BaseCompositeField<Vector4, FloatField, float>.FieldDescription[] DescribeFields()
		{
			BaseCompositeField<Vector4, FloatField, float>.FieldDescription[] array = new BaseCompositeField<Vector4, FloatField, float>.FieldDescription[4];
			array[0] = new BaseCompositeField<Vector4, FloatField, float>.FieldDescription("X", "unity-x-input", (Vector4 r) => r.x, delegate(ref Vector4 r, float v)
			{
				r.x = v;
			});
			array[1] = new BaseCompositeField<Vector4, FloatField, float>.FieldDescription("Y", "unity-y-input", (Vector4 r) => r.y, delegate(ref Vector4 r, float v)
			{
				r.y = v;
			});
			array[2] = new BaseCompositeField<Vector4, FloatField, float>.FieldDescription("Z", "unity-z-input", (Vector4 r) => r.z, delegate(ref Vector4 r, float v)
			{
				r.z = v;
			});
			array[3] = new BaseCompositeField<Vector4, FloatField, float>.FieldDescription("W", "unity-w-input", (Vector4 r) => r.w, delegate(ref Vector4 r, float v)
			{
				r.w = v;
			});
			return array;
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x00021720 File Offset: 0x0001F920
		public Vector4Field()
			: this(null)
		{
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x0002172B File Offset: 0x0001F92B
		public Vector4Field(string label)
			: base(label, 4)
		{
			base.AddToClassList(Vector4Field.ussClassName);
			base.labelElement.AddToClassList(Vector4Field.labelUssClassName);
			base.visualInput.AddToClassList(Vector4Field.inputUssClassName);
		}

		// Token: 0x04000425 RID: 1061
		public new static readonly string ussClassName = "unity-vector4-field";

		// Token: 0x04000426 RID: 1062
		public new static readonly string labelUssClassName = Vector4Field.ussClassName + "__label";

		// Token: 0x04000427 RID: 1063
		public new static readonly string inputUssClassName = Vector4Field.ussClassName + "__input";

		// Token: 0x020000D4 RID: 212
		[ExcludeFromDocs]
		[Serializable]
		public new class UxmlSerializedData : BaseCompositeField<Vector4, FloatField, float>.UxmlSerializedData, IUxmlSerializedDataCustomAttributeHandler
		{
			// Token: 0x0600076E RID: 1902 RVA: 0x00021799 File Offset: 0x0001F999
			[Conditional("UNITY_EDITOR")]
			public new static void Register()
			{
				BaseCompositeField<Vector4, FloatField, float>.UxmlSerializedData.Register();
			}

			// Token: 0x0600076F RID: 1903 RVA: 0x000217A2 File Offset: 0x0001F9A2
			public override object CreateInstance()
			{
				return new Vector4Field();
			}

			// Token: 0x06000770 RID: 1904 RVA: 0x000217AC File Offset: 0x0001F9AC
			void IUxmlSerializedDataCustomAttributeHandler.SerializeCustomAttributes(IUxmlAttributes bag, HashSet<string> handledAttributes)
			{
				int num = 0;
				float num2 = UxmlUtility.TryParseFloatAttribute("x", bag, ref num);
				float num3 = UxmlUtility.TryParseFloatAttribute("y", bag, ref num);
				float num4 = UxmlUtility.TryParseFloatAttribute("z", bag, ref num);
				float num5 = UxmlUtility.TryParseFloatAttribute("w", bag, ref num);
				bool flag = num > 0;
				if (flag)
				{
					base.Value = new Vector4(num2, num3, num4, num5);
					handledAttributes.Add("value");
					UxmlAsset uxmlAsset = bag as UxmlAsset;
					bool flag2 = uxmlAsset != null;
					if (flag2)
					{
						uxmlAsset.RemoveAttribute("x");
						uxmlAsset.RemoveAttribute("y");
						uxmlAsset.RemoveAttribute("z");
						uxmlAsset.RemoveAttribute("w");
						uxmlAsset.SetAttribute("value", UxmlUtility.ValueToString(base.Value));
					}
				}
			}
		}

		// Token: 0x020000D5 RID: 213
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<Vector4Field, Vector4Field.UxmlTraits>
		{
		}

		// Token: 0x020000D6 RID: 214
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : BaseField<Vector4>.UxmlTraits
		{
			// Token: 0x06000773 RID: 1907 RVA: 0x00021894 File Offset: 0x0001FA94
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				Vector4Field vector4Field = (Vector4Field)ve;
				vector4Field.SetValueWithoutNotify(new Vector4(this.m_XValue.GetValueFromBag(bag, cc), this.m_YValue.GetValueFromBag(bag, cc), this.m_ZValue.GetValueFromBag(bag, cc), this.m_WValue.GetValueFromBag(bag, cc)));
			}

			// Token: 0x04000428 RID: 1064
			private UxmlFloatAttributeDescription m_XValue = new UxmlFloatAttributeDescription
			{
				name = "x"
			};

			// Token: 0x04000429 RID: 1065
			private UxmlFloatAttributeDescription m_YValue = new UxmlFloatAttributeDescription
			{
				name = "y"
			};

			// Token: 0x0400042A RID: 1066
			private UxmlFloatAttributeDescription m_ZValue = new UxmlFloatAttributeDescription
			{
				name = "z"
			};

			// Token: 0x0400042B RID: 1067
			private UxmlFloatAttributeDescription m_WValue = new UxmlFloatAttributeDescription
			{
				name = "w"
			};
		}
	}
}
