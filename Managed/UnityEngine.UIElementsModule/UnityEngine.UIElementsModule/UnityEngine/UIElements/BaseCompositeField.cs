using System;
using System.Collections.Generic;
using UnityEngine.Internal;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.UIElements
{
	// Token: 0x02000081 RID: 129
	[MovedFrom(true, "UnityEditor.UIElements", "UnityEditor.UIElementsModule", null)]
	public abstract class BaseCompositeField<TValueType, TField, TFieldValue> : BaseField<TValueType> where TField : TextValueField<TFieldValue>, new()
	{
		// Token: 0x060004DF RID: 1247 RVA: 0x00016FCC File Offset: 0x000151CC
		private VisualElement GetSpacer()
		{
			VisualElement visualElement = new VisualElement();
			visualElement.AddToClassList(BaseCompositeField<TValueType, TField, TFieldValue>.spacerUssClassName);
			visualElement.visible = false;
			visualElement.focusable = false;
			return visualElement;
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060004E0 RID: 1248 RVA: 0x00017001 File Offset: 0x00015201
		internal List<TField> fields
		{
			get
			{
				return this.m_Fields;
			}
		}

		// Token: 0x060004E1 RID: 1249
		internal abstract BaseCompositeField<TValueType, TField, TFieldValue>.FieldDescription[] DescribeFields();

		// Token: 0x060004E2 RID: 1250 RVA: 0x0001700C File Offset: 0x0001520C
		protected BaseCompositeField(string label, int fieldsByLine)
			: base(label, null)
		{
			base.delegatesFocus = false;
			base.visualInput.focusable = false;
			base.AddToClassList(BaseCompositeField<TValueType, TField, TFieldValue>.ussClassName);
			base.labelElement.AddToClassList(BaseCompositeField<TValueType, TField, TFieldValue>.labelUssClassName);
			base.visualInput.AddToClassList(BaseCompositeField<TValueType, TField, TFieldValue>.inputUssClassName);
			this.m_ShouldUpdateDisplay = true;
			this.m_Fields = new List<TField>();
			BaseCompositeField<TValueType, TField, TFieldValue>.FieldDescription[] array = this.DescribeFields();
			int num = 1;
			bool flag = fieldsByLine > 1;
			if (flag)
			{
				num = array.Length / fieldsByLine;
			}
			bool flag2 = false;
			bool flag3 = num > 1;
			if (flag3)
			{
				flag2 = true;
				base.AddToClassList(BaseCompositeField<TValueType, TField, TFieldValue>.multilineVariantUssClassName);
			}
			for (int i = 0; i < num; i++)
			{
				VisualElement visualElement = null;
				bool flag4 = flag2;
				if (flag4)
				{
					visualElement = new VisualElement();
					visualElement.AddToClassList(BaseCompositeField<TValueType, TField, TFieldValue>.fieldGroupUssClassName);
				}
				bool flag5 = true;
				for (int j = i * fieldsByLine; j < i * fieldsByLine + fieldsByLine; j++)
				{
					BaseCompositeField<TValueType, TField, TFieldValue>.<>c__DisplayClass18_0 CS$<>8__locals1 = new BaseCompositeField<TValueType, TField, TFieldValue>.<>c__DisplayClass18_0();
					CS$<>8__locals1.<>4__this = this;
					CS$<>8__locals1.desc = array[j];
					BaseCompositeField<TValueType, TField, TFieldValue>.<>c__DisplayClass18_0 CS$<>8__locals2 = CS$<>8__locals1;
					TField tfield = new TField();
					tfield.name = CS$<>8__locals1.desc.ussName;
					CS$<>8__locals2.field = tfield;
					CS$<>8__locals1.field.delegatesFocus = true;
					CS$<>8__locals1.field.AddToClassList(BaseCompositeField<TValueType, TField, TFieldValue>.fieldUssClassName);
					bool flag6 = flag5;
					if (flag6)
					{
						CS$<>8__locals1.field.AddToClassList(BaseCompositeField<TValueType, TField, TFieldValue>.firstFieldVariantUssClassName);
						flag5 = false;
					}
					CS$<>8__locals1.field.label = CS$<>8__locals1.desc.name;
					CS$<>8__locals1.field.onValidateValue += delegate(TFieldValue newValue)
					{
						TValueType value = CS$<>8__locals1.<>4__this.value;
						CS$<>8__locals1.desc.write(ref value, newValue);
						TValueType tvalueType = CS$<>8__locals1.<>4__this.ValidatedValue(value);
						return CS$<>8__locals1.desc.read(tvalueType);
					};
					CS$<>8__locals1.field.RegisterValueChangedCallback<TFieldValue>(delegate(ChangeEvent<TFieldValue> e)
					{
						TValueType value2 = CS$<>8__locals1.<>4__this.value;
						CS$<>8__locals1.desc.write(ref value2, e.newValue);
						TFieldValue newValue = e.newValue;
						string text = newValue.ToString();
						string text2 = ((TField)((object)e.currentTarget)).text;
						bool flag11 = text != text2 || CS$<>8__locals1.field.CanTryParse(text2);
						if (flag11)
						{
							CS$<>8__locals1.<>4__this.m_ShouldUpdateDisplay = false;
						}
						CS$<>8__locals1.<>4__this.value = value2;
						CS$<>8__locals1.<>4__this.m_ShouldUpdateDisplay = true;
					});
					this.m_Fields.Add(CS$<>8__locals1.field);
					bool flag7 = flag2;
					if (flag7)
					{
						visualElement.Add(CS$<>8__locals1.field);
					}
					else
					{
						base.visualInput.hierarchy.Add(CS$<>8__locals1.field);
					}
				}
				bool flag8 = fieldsByLine < 3;
				if (flag8)
				{
					int num2 = 3 - fieldsByLine;
					for (int k = 0; k < num2; k++)
					{
						bool flag9 = flag2;
						if (flag9)
						{
							visualElement.Add(this.GetSpacer());
						}
						else
						{
							base.visualInput.hierarchy.Add(this.GetSpacer());
						}
					}
				}
				bool flag10 = flag2;
				if (flag10)
				{
					base.visualInput.hierarchy.Add(visualElement);
				}
			}
			this.UpdateDisplay();
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x000172DC File Offset: 0x000154DC
		private void UpdateDisplay()
		{
			bool flag = this.m_Fields.Count != 0;
			if (flag)
			{
				int num = 0;
				BaseCompositeField<TValueType, TField, TFieldValue>.FieldDescription[] array = this.DescribeFields();
				foreach (BaseCompositeField<TValueType, TField, TFieldValue>.FieldDescription fieldDescription in array)
				{
					this.m_Fields[num].SetValueWithoutNotify(fieldDescription.read(base.rawValue));
					num++;
				}
			}
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x00017358 File Offset: 0x00015558
		public override void SetValueWithoutNotify(TValueType newValue)
		{
			bool flag = this.m_ForceUpdateDisplay || (this.m_ShouldUpdateDisplay && !EqualityComparer<TValueType>.Default.Equals(base.rawValue, newValue));
			base.SetValueWithoutNotify(newValue);
			bool flag2 = flag;
			if (flag2)
			{
				this.UpdateDisplay();
			}
			this.m_ForceUpdateDisplay = false;
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x000173AE File Offset: 0x000155AE
		internal override void OnViewDataReady()
		{
			this.m_ForceUpdateDisplay = true;
			base.OnViewDataReady();
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x000173C0 File Offset: 0x000155C0
		protected override void UpdateMixedValueContent()
		{
			foreach (TField tfield in this.m_Fields)
			{
				tfield.showMixedValue = base.showMixedValue;
			}
		}

		// Token: 0x0400025E RID: 606
		private List<TField> m_Fields;

		// Token: 0x0400025F RID: 607
		private bool m_ShouldUpdateDisplay;

		// Token: 0x04000260 RID: 608
		private bool m_ForceUpdateDisplay;

		// Token: 0x04000261 RID: 609
		public new static readonly string ussClassName = "unity-composite-field";

		// Token: 0x04000262 RID: 610
		public new static readonly string labelUssClassName = BaseCompositeField<TValueType, TField, TFieldValue>.ussClassName + "__label";

		// Token: 0x04000263 RID: 611
		public new static readonly string inputUssClassName = BaseCompositeField<TValueType, TField, TFieldValue>.ussClassName + "__input";

		// Token: 0x04000264 RID: 612
		public static readonly string spacerUssClassName = BaseCompositeField<TValueType, TField, TFieldValue>.ussClassName + "__field-spacer";

		// Token: 0x04000265 RID: 613
		public static readonly string multilineVariantUssClassName = BaseCompositeField<TValueType, TField, TFieldValue>.ussClassName + "--multi-line";

		// Token: 0x04000266 RID: 614
		public static readonly string fieldGroupUssClassName = BaseCompositeField<TValueType, TField, TFieldValue>.ussClassName + "__field-group";

		// Token: 0x04000267 RID: 615
		public static readonly string fieldUssClassName = BaseCompositeField<TValueType, TField, TFieldValue>.ussClassName + "__field";

		// Token: 0x04000268 RID: 616
		public static readonly string firstFieldVariantUssClassName = BaseCompositeField<TValueType, TField, TFieldValue>.fieldUssClassName + "--first";

		// Token: 0x04000269 RID: 617
		public static readonly string twoLinesVariantUssClassName = BaseCompositeField<TValueType, TField, TFieldValue>.ussClassName + "--two-lines";

		// Token: 0x02000082 RID: 130
		internal struct FieldDescription
		{
			// Token: 0x060004E8 RID: 1256 RVA: 0x000174DB File Offset: 0x000156DB
			public FieldDescription(string name, string ussName, Func<TValueType, TFieldValue> read, BaseCompositeField<TValueType, TField, TFieldValue>.FieldDescription.WriteDelegate write)
			{
				this.name = name;
				this.ussName = ussName;
				this.read = read;
				this.write = write;
			}

			// Token: 0x0400026A RID: 618
			internal readonly string name;

			// Token: 0x0400026B RID: 619
			internal readonly string ussName;

			// Token: 0x0400026C RID: 620
			internal readonly Func<TValueType, TFieldValue> read;

			// Token: 0x0400026D RID: 621
			internal readonly BaseCompositeField<TValueType, TField, TFieldValue>.FieldDescription.WriteDelegate write;

			// Token: 0x02000083 RID: 131
			// (Invoke) Token: 0x060004EA RID: 1258
			public delegate void WriteDelegate(ref TValueType val, TFieldValue fieldValue);
		}

		// Token: 0x02000084 RID: 132
		[ExcludeFromDocs]
		[Serializable]
		public new abstract class UxmlSerializedData : BaseField<TValueType>.UxmlSerializedData
		{
			// Token: 0x060004ED RID: 1261 RVA: 0x000174FB File Offset: 0x000156FB
			public new static void Register()
			{
				BaseField<TValueType>.UxmlSerializedData.Register();
			}
		}
	}
}
