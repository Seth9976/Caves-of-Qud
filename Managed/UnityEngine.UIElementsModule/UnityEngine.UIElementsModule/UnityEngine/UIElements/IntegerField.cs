using System;
using System.Diagnostics;
using System.Globalization;
using UnityEngine.Internal;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.UIElements
{
	// Token: 0x02000128 RID: 296
	[MovedFrom(true, "UnityEditor.UIElements", "UnityEditor.UIElementsModule", null)]
	public class IntegerField : TextValueField<int>
	{
		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060009B2 RID: 2482 RVA: 0x0002ACC7 File Offset: 0x00028EC7
		private IntegerField.IntegerInput integerInput
		{
			get
			{
				return (IntegerField.IntegerInput)base.textInputBase;
			}
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x0002ACD4 File Offset: 0x00028ED4
		protected override string ValueToString(int v)
		{
			return v.ToString(base.formatString, CultureInfo.InvariantCulture.NumberFormat);
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x0002AD00 File Offset: 0x00028F00
		protected override int StringToValue(string str)
		{
			int num;
			ExpressionEvaluator.Expression expression;
			bool flag = UINumericFieldsUtils.TryConvertStringToInt(str, base.textInputBase.originalText, out num, out expression);
			Action<ExpressionEvaluator.Expression> expressionEvaluated = this.expressionEvaluated;
			if (expressionEvaluated != null)
			{
				expressionEvaluated(expression);
			}
			return flag ? num : base.rawValue;
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x0002AD47 File Offset: 0x00028F47
		public IntegerField()
			: this(null, 1000)
		{
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x0002AD57 File Offset: 0x00028F57
		public IntegerField(int maxLength)
			: this(null, maxLength)
		{
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x0002AD64 File Offset: 0x00028F64
		public IntegerField(string label, int maxLength = 1000)
			: base(label, maxLength, new IntegerField.IntegerInput())
		{
			base.AddToClassList(IntegerField.ussClassName);
			base.labelElement.AddToClassList(IntegerField.labelUssClassName);
			base.visualInput.AddToClassList(IntegerField.inputUssClassName);
			base.AddLabelDragger<int>();
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x0002ADB8 File Offset: 0x00028FB8
		internal override bool CanTryParse(string textString)
		{
			int num;
			return int.TryParse(textString, out num);
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x0002ADCD File Offset: 0x00028FCD
		public override void ApplyInputDeviceDelta(Vector3 delta, DeltaSpeed speed, int startValue)
		{
			this.integerInput.ApplyInputDeviceDelta(delta, speed, startValue);
		}

		// Token: 0x0400058C RID: 1420
		public new static readonly string ussClassName = "unity-integer-field";

		// Token: 0x0400058D RID: 1421
		public new static readonly string labelUssClassName = IntegerField.ussClassName + "__label";

		// Token: 0x0400058E RID: 1422
		public new static readonly string inputUssClassName = IntegerField.ussClassName + "__input";

		// Token: 0x02000129 RID: 297
		[ExcludeFromDocs]
		[Serializable]
		public new class UxmlSerializedData : TextInputBaseField<int>.UxmlSerializedData
		{
			// Token: 0x060009BB RID: 2491 RVA: 0x0002AE13 File Offset: 0x00029013
			[Conditional("UNITY_EDITOR")]
			public new static void Register()
			{
				TextInputBaseField<int>.UxmlSerializedData.Register();
			}

			// Token: 0x060009BC RID: 2492 RVA: 0x0002AE1C File Offset: 0x0002901C
			public override object CreateInstance()
			{
				return new IntegerField();
			}
		}

		// Token: 0x0200012A RID: 298
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<IntegerField, IntegerField.UxmlTraits>
		{
		}

		// Token: 0x0200012B RID: 299
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : TextValueFieldTraits<int, UxmlIntAttributeDescription>
		{
		}

		// Token: 0x0200012C RID: 300
		private class IntegerInput : TextValueField<int>.TextValueInput
		{
			// Token: 0x17000190 RID: 400
			// (get) Token: 0x060009C0 RID: 2496 RVA: 0x0002AE3E File Offset: 0x0002903E
			private IntegerField parentIntegerField
			{
				get
				{
					return (IntegerField)base.parent;
				}
			}

			// Token: 0x060009C1 RID: 2497 RVA: 0x0002AE4B File Offset: 0x0002904B
			internal IntegerInput()
			{
				base.formatString = UINumericFieldsUtils.k_IntFieldFormatString;
			}

			// Token: 0x17000191 RID: 401
			// (get) Token: 0x060009C2 RID: 2498 RVA: 0x0002AE61 File Offset: 0x00029061
			protected override string allowedCharacters
			{
				get
				{
					return UINumericFieldsUtils.k_AllowedCharactersForInt;
				}
			}

			// Token: 0x060009C3 RID: 2499 RVA: 0x0002AE68 File Offset: 0x00029068
			public override void ApplyInputDeviceDelta(Vector3 delta, DeltaSpeed speed, int startValue)
			{
				double num = (double)NumericFieldDraggerUtility.CalculateIntDragSensitivity((long)startValue);
				float num2 = NumericFieldDraggerUtility.Acceleration(speed == DeltaSpeed.Fast, speed == DeltaSpeed.Slow);
				long num3 = (long)this.StringToValue(base.text);
				num3 += (long)Math.Round((double)NumericFieldDraggerUtility.NiceDelta(delta, num2) * num);
				bool isDelayed = this.parentIntegerField.isDelayed;
				if (isDelayed)
				{
					base.text = this.ValueToString(Mathf.ClampToInt(num3));
				}
				else
				{
					this.parentIntegerField.value = Mathf.ClampToInt(num3);
				}
			}

			// Token: 0x060009C4 RID: 2500 RVA: 0x0002AEF0 File Offset: 0x000290F0
			protected override string ValueToString(int v)
			{
				return v.ToString(base.formatString);
			}

			// Token: 0x060009C5 RID: 2501 RVA: 0x0002AF10 File Offset: 0x00029110
			protected override int StringToValue(string str)
			{
				return this.parentIntegerField.StringToValue(str);
			}
		}
	}
}
