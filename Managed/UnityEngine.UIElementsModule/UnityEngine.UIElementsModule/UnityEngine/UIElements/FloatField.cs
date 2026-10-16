using System;
using System.Diagnostics;
using System.Globalization;
using UnityEngine.Internal;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.UIElements
{
	// Token: 0x020000F1 RID: 241
	[MovedFrom(true, "UnityEditor.UIElements", "UnityEditor.UIElementsModule", null)]
	public class FloatField : TextValueField<float>
	{
		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060007ED RID: 2029 RVA: 0x00022E06 File Offset: 0x00021006
		private FloatField.FloatInput floatInput
		{
			get
			{
				return (FloatField.FloatInput)base.textInputBase;
			}
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x00022E14 File Offset: 0x00021014
		protected override string ValueToString(float v)
		{
			return v.ToString(base.formatString, CultureInfo.InvariantCulture.NumberFormat);
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x00022E40 File Offset: 0x00021040
		protected override float StringToValue(string str)
		{
			float num;
			ExpressionEvaluator.Expression expression;
			bool flag = UINumericFieldsUtils.TryConvertStringToFloat(str, base.textInputBase.originalText, out num, out expression);
			Action<ExpressionEvaluator.Expression> expressionEvaluated = this.expressionEvaluated;
			if (expressionEvaluated != null)
			{
				expressionEvaluated(expression);
			}
			return flag ? num : base.rawValue;
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x00022E88 File Offset: 0x00021088
		internal override void UpdateValueFromText()
		{
			string text = this.ValueToString(base.rawValue);
			bool flag = text != base.text;
			if (flag)
			{
				base.UpdateValueFromText();
			}
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x00022EBC File Offset: 0x000210BC
		public FloatField()
			: this(null, 1000)
		{
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x00022ECC File Offset: 0x000210CC
		public FloatField(int maxLength)
			: this(null, maxLength)
		{
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x00022ED8 File Offset: 0x000210D8
		public FloatField(string label, int maxLength = 1000)
			: base(label, maxLength, new FloatField.FloatInput())
		{
			base.AddToClassList(FloatField.ussClassName);
			base.labelElement.AddToClassList(FloatField.labelUssClassName);
			base.visualInput.AddToClassList(FloatField.inputUssClassName);
			base.AddLabelDragger<float>();
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x00022F2C File Offset: 0x0002112C
		internal override bool CanTryParse(string textString)
		{
			float num;
			return float.TryParse(textString, out num);
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x00022F41 File Offset: 0x00021141
		public override void ApplyInputDeviceDelta(Vector3 delta, DeltaSpeed speed, float startValue)
		{
			this.floatInput.ApplyInputDeviceDelta(delta, speed, startValue);
		}

		// Token: 0x04000472 RID: 1138
		public new static readonly string ussClassName = "unity-float-field";

		// Token: 0x04000473 RID: 1139
		public new static readonly string labelUssClassName = FloatField.ussClassName + "__label";

		// Token: 0x04000474 RID: 1140
		public new static readonly string inputUssClassName = FloatField.ussClassName + "__input";

		// Token: 0x020000F2 RID: 242
		[ExcludeFromDocs]
		[Serializable]
		public new class UxmlSerializedData : TextInputBaseField<float>.UxmlSerializedData
		{
			// Token: 0x060007F7 RID: 2039 RVA: 0x00022F87 File Offset: 0x00021187
			[Conditional("UNITY_EDITOR")]
			public new static void Register()
			{
				TextInputBaseField<float>.UxmlSerializedData.Register();
			}

			// Token: 0x060007F8 RID: 2040 RVA: 0x00022F90 File Offset: 0x00021190
			public override object CreateInstance()
			{
				return new FloatField();
			}
		}

		// Token: 0x020000F3 RID: 243
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<FloatField, FloatField.UxmlTraits>
		{
		}

		// Token: 0x020000F4 RID: 244
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : TextValueFieldTraits<float, UxmlFloatAttributeDescription>
		{
		}

		// Token: 0x020000F5 RID: 245
		private class FloatInput : TextValueField<float>.TextValueInput
		{
			// Token: 0x17000120 RID: 288
			// (get) Token: 0x060007FC RID: 2044 RVA: 0x00022FB2 File Offset: 0x000211B2
			private FloatField parentFloatField
			{
				get
				{
					return (FloatField)base.parent;
				}
			}

			// Token: 0x060007FD RID: 2045 RVA: 0x00022FBF File Offset: 0x000211BF
			internal FloatInput()
			{
				base.formatString = UINumericFieldsUtils.k_FloatFieldFormatString;
			}

			// Token: 0x17000121 RID: 289
			// (get) Token: 0x060007FE RID: 2046 RVA: 0x00022151 File Offset: 0x00020351
			protected override string allowedCharacters
			{
				get
				{
					return UINumericFieldsUtils.k_AllowedCharactersForFloat;
				}
			}

			// Token: 0x060007FF RID: 2047 RVA: 0x00022FD8 File Offset: 0x000211D8
			public override void ApplyInputDeviceDelta(Vector3 delta, DeltaSpeed speed, float startValue)
			{
				double num = NumericFieldDraggerUtility.CalculateFloatDragSensitivity((double)startValue);
				float num2 = NumericFieldDraggerUtility.Acceleration(speed == DeltaSpeed.Fast, speed == DeltaSpeed.Slow);
				double num3 = (double)this.StringToValue(base.text);
				num3 += (double)NumericFieldDraggerUtility.NiceDelta(delta, num2) * num;
				num3 = Mathf.RoundBasedOnMinimumDifference(num3, num);
				bool isDelayed = this.parentFloatField.isDelayed;
				if (isDelayed)
				{
					base.text = this.ValueToString(Mathf.ClampToFloat(num3));
				}
				else
				{
					this.parentFloatField.value = Mathf.ClampToFloat(num3);
				}
			}

			// Token: 0x06000800 RID: 2048 RVA: 0x00023060 File Offset: 0x00021260
			protected override string ValueToString(float v)
			{
				return v.ToString(base.formatString);
			}

			// Token: 0x06000801 RID: 2049 RVA: 0x00023080 File Offset: 0x00021280
			protected override float StringToValue(string str)
			{
				return this.parentFloatField.StringToValue(str);
			}
		}
	}
}
