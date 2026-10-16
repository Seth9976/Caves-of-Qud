using System;
using System.Diagnostics;
using System.Globalization;
using UnityEngine.Internal;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.UIElements
{
	// Token: 0x02000135 RID: 309
	[MovedFrom(true, "UnityEditor.UIElements", "UnityEditor.UIElementsModule", null)]
	public class LongField : TextValueField<long>
	{
		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060009E6 RID: 2534 RVA: 0x0002B324 File Offset: 0x00029524
		private LongField.LongInput longInput
		{
			get
			{
				return (LongField.LongInput)base.textInputBase;
			}
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x0002B334 File Offset: 0x00029534
		protected override string ValueToString(long v)
		{
			return v.ToString(base.formatString, CultureInfo.InvariantCulture.NumberFormat);
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x0002B360 File Offset: 0x00029560
		protected override long StringToValue(string str)
		{
			long num;
			ExpressionEvaluator.Expression expression;
			bool flag = UINumericFieldsUtils.TryConvertStringToLong(str, base.textInputBase.originalText, out num, out expression);
			Action<ExpressionEvaluator.Expression> expressionEvaluated = this.expressionEvaluated;
			if (expressionEvaluated != null)
			{
				expressionEvaluated(expression);
			}
			return flag ? num : base.rawValue;
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x0002B3A7 File Offset: 0x000295A7
		public LongField()
			: this(null, 1000)
		{
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x0002B3B7 File Offset: 0x000295B7
		public LongField(int maxLength)
			: this(null, maxLength)
		{
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x0002B3C4 File Offset: 0x000295C4
		public LongField(string label, int maxLength = 1000)
			: base(label, maxLength, new LongField.LongInput())
		{
			base.AddToClassList(LongField.ussClassName);
			base.labelElement.AddToClassList(LongField.labelUssClassName);
			base.visualInput.AddToClassList(LongField.inputUssClassName);
			base.AddLabelDragger<long>();
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x0002B418 File Offset: 0x00029618
		internal override bool CanTryParse(string textString)
		{
			long num;
			return long.TryParse(textString, out num);
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x0002B42D File Offset: 0x0002962D
		public override void ApplyInputDeviceDelta(Vector3 delta, DeltaSpeed speed, long startValue)
		{
			this.longInput.ApplyInputDeviceDelta(delta, speed, startValue);
		}

		// Token: 0x0400059E RID: 1438
		public new static readonly string ussClassName = "unity-long-field";

		// Token: 0x0400059F RID: 1439
		public new static readonly string labelUssClassName = LongField.ussClassName + "__label";

		// Token: 0x040005A0 RID: 1440
		public new static readonly string inputUssClassName = LongField.ussClassName + "__input";

		// Token: 0x02000136 RID: 310
		[ExcludeFromDocs]
		[Serializable]
		public new class UxmlSerializedData : TextInputBaseField<long>.UxmlSerializedData
		{
			// Token: 0x060009EF RID: 2543 RVA: 0x0002B473 File Offset: 0x00029673
			[Conditional("UNITY_EDITOR")]
			public new static void Register()
			{
				TextInputBaseField<long>.UxmlSerializedData.Register();
			}

			// Token: 0x060009F0 RID: 2544 RVA: 0x0002B47C File Offset: 0x0002967C
			public override object CreateInstance()
			{
				return new LongField();
			}
		}

		// Token: 0x02000137 RID: 311
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<LongField, LongField.UxmlTraits>
		{
		}

		// Token: 0x02000138 RID: 312
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : TextValueFieldTraits<long, UxmlLongAttributeDescription>
		{
		}

		// Token: 0x02000139 RID: 313
		private class LongInput : TextValueField<long>.TextValueInput
		{
			// Token: 0x17000198 RID: 408
			// (get) Token: 0x060009F4 RID: 2548 RVA: 0x0002B49E File Offset: 0x0002969E
			private LongField parentLongField
			{
				get
				{
					return (LongField)base.parent;
				}
			}

			// Token: 0x060009F5 RID: 2549 RVA: 0x0002B4AB File Offset: 0x000296AB
			internal LongInput()
			{
				base.formatString = UINumericFieldsUtils.k_IntFieldFormatString;
			}

			// Token: 0x17000199 RID: 409
			// (get) Token: 0x060009F6 RID: 2550 RVA: 0x0002B4C4 File Offset: 0x000296C4
			protected override string allowedCharacters
			{
				get
				{
					return UINumericFieldsUtils.k_AllowedCharactersForInt;
				}
			}

			// Token: 0x060009F7 RID: 2551 RVA: 0x0002B4DC File Offset: 0x000296DC
			public override void ApplyInputDeviceDelta(Vector3 delta, DeltaSpeed speed, long startValue)
			{
				double num = (double)NumericFieldDraggerUtility.CalculateIntDragSensitivity(startValue);
				float num2 = NumericFieldDraggerUtility.Acceleration(speed == DeltaSpeed.Fast, speed == DeltaSpeed.Slow);
				long num3 = this.StringToValue(base.text);
				long num4 = (long)Math.Round((double)NumericFieldDraggerUtility.NiceDelta(delta, num2) * num);
				num3 = this.ClampMinMaxLongValue(num4, num3);
				bool isDelayed = this.parentLongField.isDelayed;
				if (isDelayed)
				{
					base.text = this.ValueToString(num3);
				}
				else
				{
					this.parentLongField.value = num3;
				}
			}

			// Token: 0x060009F8 RID: 2552 RVA: 0x0002B560 File Offset: 0x00029760
			private long ClampMinMaxLongValue(long niceDelta, long value)
			{
				long num = Math.Abs(niceDelta);
				bool flag = niceDelta > 0L;
				long num2;
				if (flag)
				{
					bool flag2 = value > 0L && num > long.MaxValue - value;
					if (flag2)
					{
						num2 = long.MaxValue;
					}
					else
					{
						num2 = value + niceDelta;
					}
				}
				else
				{
					bool flag3 = value < 0L && value < long.MinValue + num;
					if (flag3)
					{
						num2 = long.MinValue;
					}
					else
					{
						num2 = value - num;
					}
				}
				return num2;
			}

			// Token: 0x060009F9 RID: 2553 RVA: 0x0002B5DC File Offset: 0x000297DC
			protected override string ValueToString(long v)
			{
				return v.ToString(base.formatString);
			}

			// Token: 0x060009FA RID: 2554 RVA: 0x0002B5FC File Offset: 0x000297FC
			protected override long StringToValue(string str)
			{
				return this.parentLongField.StringToValue(str);
			}
		}
	}
}
