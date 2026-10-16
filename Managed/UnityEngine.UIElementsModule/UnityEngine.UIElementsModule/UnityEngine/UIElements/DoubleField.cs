using System;
using System.Diagnostics;
using System.Globalization;
using UnityEngine.Internal;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.UIElements
{
	// Token: 0x020000E2 RID: 226
	[MovedFrom(true, "UnityEditor.UIElements", "UnityEditor.UIElementsModule", null)]
	public class DoubleField : TextValueField<double>
	{
		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060007A3 RID: 1955 RVA: 0x00021FB6 File Offset: 0x000201B6
		private DoubleField.DoubleInput doubleInput
		{
			get
			{
				return (DoubleField.DoubleInput)base.textInputBase;
			}
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x00021FC4 File Offset: 0x000201C4
		protected override string ValueToString(double v)
		{
			return v.ToString(base.formatString, CultureInfo.InvariantCulture.NumberFormat);
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x00021FF0 File Offset: 0x000201F0
		protected override double StringToValue(string str)
		{
			double num;
			ExpressionEvaluator.Expression expression;
			bool flag = UINumericFieldsUtils.TryConvertStringToDouble(str, base.textInputBase.originalText, out num, out expression);
			Action<ExpressionEvaluator.Expression> expressionEvaluated = this.expressionEvaluated;
			if (expressionEvaluated != null)
			{
				expressionEvaluated(expression);
			}
			return flag ? num : base.rawValue;
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x00022037 File Offset: 0x00020237
		public DoubleField()
			: this(null, 1000)
		{
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x00022047 File Offset: 0x00020247
		public DoubleField(int maxLength)
			: this(null, maxLength)
		{
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x00022054 File Offset: 0x00020254
		public DoubleField(string label, int maxLength = 1000)
			: base(label, maxLength, new DoubleField.DoubleInput())
		{
			base.AddToClassList(DoubleField.ussClassName);
			base.labelElement.AddToClassList(DoubleField.labelUssClassName);
			base.visualInput.AddToClassList(DoubleField.inputUssClassName);
			base.AddLabelDragger<double>();
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x000220A8 File Offset: 0x000202A8
		internal override bool CanTryParse(string textString)
		{
			double num;
			return double.TryParse(textString, out num);
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x000220BD File Offset: 0x000202BD
		public override void ApplyInputDeviceDelta(Vector3 delta, DeltaSpeed speed, double startValue)
		{
			this.doubleInput.ApplyInputDeviceDelta(delta, speed, startValue);
		}

		// Token: 0x0400044C RID: 1100
		public new static readonly string ussClassName = "unity-double-field";

		// Token: 0x0400044D RID: 1101
		public new static readonly string labelUssClassName = DoubleField.ussClassName + "__label";

		// Token: 0x0400044E RID: 1102
		public new static readonly string inputUssClassName = DoubleField.ussClassName + "__input";

		// Token: 0x020000E3 RID: 227
		[ExcludeFromDocs]
		[Serializable]
		public new class UxmlSerializedData : TextInputBaseField<double>.UxmlSerializedData
		{
			// Token: 0x060007AC RID: 1964 RVA: 0x00022103 File Offset: 0x00020303
			[Conditional("UNITY_EDITOR")]
			public new static void Register()
			{
				TextInputBaseField<double>.UxmlSerializedData.Register();
			}

			// Token: 0x060007AD RID: 1965 RVA: 0x0002210C File Offset: 0x0002030C
			public override object CreateInstance()
			{
				return new DoubleField();
			}
		}

		// Token: 0x020000E4 RID: 228
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<DoubleField, DoubleField.UxmlTraits>
		{
		}

		// Token: 0x020000E5 RID: 229
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : TextValueFieldTraits<double, UxmlDoubleAttributeDescription>
		{
		}

		// Token: 0x020000E6 RID: 230
		private class DoubleInput : TextValueField<double>.TextValueInput
		{
			// Token: 0x17000117 RID: 279
			// (get) Token: 0x060007B1 RID: 1969 RVA: 0x0002212E File Offset: 0x0002032E
			private DoubleField parentDoubleField
			{
				get
				{
					return (DoubleField)base.parent;
				}
			}

			// Token: 0x060007B2 RID: 1970 RVA: 0x0002213B File Offset: 0x0002033B
			internal DoubleInput()
			{
				base.formatString = UINumericFieldsUtils.k_DoubleFieldFormatString;
			}

			// Token: 0x17000118 RID: 280
			// (get) Token: 0x060007B3 RID: 1971 RVA: 0x00022151 File Offset: 0x00020351
			protected override string allowedCharacters
			{
				get
				{
					return UINumericFieldsUtils.k_AllowedCharactersForFloat;
				}
			}

			// Token: 0x060007B4 RID: 1972 RVA: 0x00022158 File Offset: 0x00020358
			public override void ApplyInputDeviceDelta(Vector3 delta, DeltaSpeed speed, double startValue)
			{
				double num = NumericFieldDraggerUtility.CalculateFloatDragSensitivity(startValue);
				float num2 = NumericFieldDraggerUtility.Acceleration(speed == DeltaSpeed.Fast, speed == DeltaSpeed.Slow);
				double num3 = this.StringToValue(base.text);
				num3 += (double)NumericFieldDraggerUtility.NiceDelta(delta, num2) * num;
				num3 = Mathf.RoundBasedOnMinimumDifference(num3, num);
				bool isDelayed = this.parentDoubleField.isDelayed;
				if (isDelayed)
				{
					base.text = this.ValueToString(num3);
				}
				else
				{
					this.parentDoubleField.value = num3;
				}
			}

			// Token: 0x060007B5 RID: 1973 RVA: 0x000221D4 File Offset: 0x000203D4
			protected override string ValueToString(double v)
			{
				return v.ToString(base.formatString);
			}

			// Token: 0x060007B6 RID: 1974 RVA: 0x000221F4 File Offset: 0x000203F4
			protected override double StringToValue(string str)
			{
				return this.parentDoubleField.StringToValue(str);
			}
		}
	}
}
