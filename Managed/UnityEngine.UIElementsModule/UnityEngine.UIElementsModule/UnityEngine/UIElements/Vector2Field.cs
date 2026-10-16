using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.Internal;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.UIElements
{
	// Token: 0x020000C9 RID: 201
	[MovedFrom(true, "UnityEditor.UIElements", "UnityEditor.UIElementsModule", null)]
	public class Vector2Field : BaseCompositeField<Vector2, FloatField, float>
	{
		// Token: 0x06000746 RID: 1862 RVA: 0x00020FC0 File Offset: 0x0001F1C0
		internal override BaseCompositeField<Vector2, FloatField, float>.FieldDescription[] DescribeFields()
		{
			BaseCompositeField<Vector2, FloatField, float>.FieldDescription[] array = new BaseCompositeField<Vector2, FloatField, float>.FieldDescription[2];
			array[0] = new BaseCompositeField<Vector2, FloatField, float>.FieldDescription("X", "unity-x-input", (Vector2 r) => r.x, delegate(ref Vector2 r, float v)
			{
				r.x = v;
			});
			array[1] = new BaseCompositeField<Vector2, FloatField, float>.FieldDescription("Y", "unity-y-input", (Vector2 r) => r.y, delegate(ref Vector2 r, float v)
			{
				r.y = v;
			});
			return array;
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x00021080 File Offset: 0x0001F280
		public Vector2Field()
			: this(null)
		{
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x0002108B File Offset: 0x0001F28B
		public Vector2Field(string label)
			: base(label, 2)
		{
			base.AddToClassList(Vector2Field.ussClassName);
			base.labelElement.AddToClassList(Vector2Field.labelUssClassName);
			base.visualInput.AddToClassList(Vector2Field.inputUssClassName);
		}

		// Token: 0x0400040E RID: 1038
		public new static readonly string ussClassName = "unity-vector2-field";

		// Token: 0x0400040F RID: 1039
		public new static readonly string labelUssClassName = Vector2Field.ussClassName + "__label";

		// Token: 0x04000410 RID: 1040
		public new static readonly string inputUssClassName = Vector2Field.ussClassName + "__input";

		// Token: 0x020000CA RID: 202
		[ExcludeFromDocs]
		[Serializable]
		public new class UxmlSerializedData : BaseCompositeField<Vector2, FloatField, float>.UxmlSerializedData, IUxmlSerializedDataCustomAttributeHandler
		{
			// Token: 0x0600074A RID: 1866 RVA: 0x000210F9 File Offset: 0x0001F2F9
			[Conditional("UNITY_EDITOR")]
			public new static void Register()
			{
				BaseCompositeField<Vector2, FloatField, float>.UxmlSerializedData.Register();
			}

			// Token: 0x0600074B RID: 1867 RVA: 0x00021102 File Offset: 0x0001F302
			public override object CreateInstance()
			{
				return new Vector2Field();
			}

			// Token: 0x0600074C RID: 1868 RVA: 0x0002110C File Offset: 0x0001F30C
			void IUxmlSerializedDataCustomAttributeHandler.SerializeCustomAttributes(IUxmlAttributes bag, HashSet<string> handledAttributes)
			{
				int num = 0;
				float num2 = UxmlUtility.TryParseFloatAttribute("x", bag, ref num);
				float num3 = UxmlUtility.TryParseFloatAttribute("y", bag, ref num);
				bool flag = num > 0;
				if (flag)
				{
					base.Value = new Vector2(num2, num3);
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

		// Token: 0x020000CB RID: 203
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<Vector2Field, Vector2Field.UxmlTraits>
		{
		}

		// Token: 0x020000CC RID: 204
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : BaseField<Vector2>.UxmlTraits
		{
			// Token: 0x0600074F RID: 1871 RVA: 0x000211B8 File Offset: 0x0001F3B8
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				Vector2Field vector2Field = (Vector2Field)ve;
				vector2Field.SetValueWithoutNotify(new Vector2(this.m_XValue.GetValueFromBag(bag, cc), this.m_YValue.GetValueFromBag(bag, cc)));
			}

			// Token: 0x04000411 RID: 1041
			private UxmlFloatAttributeDescription m_XValue = new UxmlFloatAttributeDescription
			{
				name = "x"
			};

			// Token: 0x04000412 RID: 1042
			private UxmlFloatAttributeDescription m_YValue = new UxmlFloatAttributeDescription
			{
				name = "y"
			};
		}
	}
}
