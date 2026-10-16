using System;
using System.Diagnostics;
using System.Globalization;
using UnityEngine.Internal;

namespace UnityEngine.UIElements
{
	// Token: 0x020001CB RID: 459
	public class UnsignedLongField : TextValueField<ulong>
	{
		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000E36 RID: 3638 RVA: 0x000402A6 File Offset: 0x0003E4A6
		private UnsignedLongField.UnsignedLongInput unsignedLongInput
		{
			get
			{
				return (UnsignedLongField.UnsignedLongInput)base.textInputBase;
			}
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x000402B4 File Offset: 0x0003E4B4
		protected override string ValueToString(ulong v)
		{
			return v.ToString(base.formatString, CultureInfo.InvariantCulture.NumberFormat);
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x000402E0 File Offset: 0x0003E4E0
		protected override ulong StringToValue(string str)
		{
			ulong num;
			ExpressionEvaluator.Expression expression;
			bool flag = UINumericFieldsUtils.TryConvertStringToULong(str, base.textInputBase.originalText, out num, out expression);
			Action<ExpressionEvaluator.Expression> expressionEvaluated = this.expressionEvaluated;
			if (expressionEvaluated != null)
			{
				expressionEvaluated(expression);
			}
			return flag ? num : base.rawValue;
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x00040327 File Offset: 0x0003E527
		public UnsignedLongField()
			: this(null, 1000)
		{
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x00040337 File Offset: 0x0003E537
		public UnsignedLongField(int maxLength)
			: this(null, maxLength)
		{
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x00040344 File Offset: 0x0003E544
		public UnsignedLongField(string label, int maxLength = 1000)
			: base(label, maxLength, new UnsignedLongField.UnsignedLongInput())
		{
			base.AddToClassList(UnsignedLongField.ussClassName);
			base.labelElement.AddToClassList(UnsignedLongField.labelUssClassName);
			base.visualInput.AddToClassList(UnsignedLongField.inputUssClassName);
			base.AddLabelDragger<ulong>();
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x00040398 File Offset: 0x0003E598
		internal override bool CanTryParse(string textString)
		{
			ulong num;
			return ulong.TryParse(textString, out num);
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x000403AD File Offset: 0x0003E5AD
		public override void ApplyInputDeviceDelta(Vector3 delta, DeltaSpeed speed, ulong startValue)
		{
			this.unsignedLongInput.ApplyInputDeviceDelta(delta, speed, startValue);
		}

		// Token: 0x040008E1 RID: 2273
		public new static readonly string ussClassName = "unity-unsigned-long-field";

		// Token: 0x040008E2 RID: 2274
		public new static readonly string labelUssClassName = UnsignedLongField.ussClassName + "__label";

		// Token: 0x040008E3 RID: 2275
		public new static readonly string inputUssClassName = UnsignedLongField.ussClassName + "__input";

		// Token: 0x020001CC RID: 460
		[ExcludeFromDocs]
		[Serializable]
		public new class UxmlSerializedData : TextInputBaseField<ulong>.UxmlSerializedData
		{
			// Token: 0x06000E3F RID: 3647 RVA: 0x000403F3 File Offset: 0x0003E5F3
			[Conditional("UNITY_EDITOR")]
			public new static void Register()
			{
				TextInputBaseField<ulong>.UxmlSerializedData.Register();
			}

			// Token: 0x06000E40 RID: 3648 RVA: 0x000403FC File Offset: 0x0003E5FC
			public override object CreateInstance()
			{
				return new UnsignedLongField();
			}
		}

		// Token: 0x020001CD RID: 461
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<UnsignedLongField, UnsignedLongField.UxmlTraits>
		{
		}

		// Token: 0x020001CE RID: 462
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : TextValueFieldTraits<ulong, UxmlUnsignedLongAttributeDescription>
		{
		}

		// Token: 0x020001CF RID: 463
		private class UnsignedLongInput : TextValueField<ulong>.TextValueInput
		{
			// Token: 0x17000269 RID: 617
			// (get) Token: 0x06000E44 RID: 3652 RVA: 0x0004041E File Offset: 0x0003E61E
			private UnsignedLongField parentUnsignedLongField
			{
				get
				{
					return (UnsignedLongField)base.parent;
				}
			}

			// Token: 0x06000E45 RID: 3653 RVA: 0x0004042B File Offset: 0x0003E62B
			internal UnsignedLongInput()
			{
				base.formatString = UINumericFieldsUtils.k_IntFieldFormatString;
			}

			// Token: 0x1700026A RID: 618
			// (get) Token: 0x06000E46 RID: 3654 RVA: 0x0002AE61 File Offset: 0x00029061
			protected override string allowedCharacters
			{
				get
				{
					return UINumericFieldsUtils.k_AllowedCharactersForInt;
				}
			}

			// Token: 0x06000E47 RID: 3655 RVA: 0x00040444 File Offset: 0x0003E644
			public override void ApplyInputDeviceDelta(Vector3 delta, DeltaSpeed speed, ulong startValue)
			{
				double num = NumericFieldDraggerUtility.CalculateIntDragSensitivity(startValue);
				float num2 = NumericFieldDraggerUtility.Acceleration(speed == DeltaSpeed.Fast, speed == DeltaSpeed.Slow);
				ulong num3 = this.StringToValue(base.text);
				long num4 = (long)Math.Round((double)NumericFieldDraggerUtility.NiceDelta(delta, num2) * num);
				num3 = this.ClampToMinMaxULongValue(num4, num3);
				bool isDelayed = this.parentUnsignedLongField.isDelayed;
				if (isDelayed)
				{
					base.text = this.ValueToString(num3);
				}
				else
				{
					this.parentUnsignedLongField.value = num3;
				}
			}

			// Token: 0x06000E48 RID: 3656 RVA: 0x000404C8 File Offset: 0x0003E6C8
			private ulong ClampToMinMaxULongValue(long niceDelta, ulong value)
			{
				ulong num = (ulong)Math.Abs(niceDelta);
				bool flag = niceDelta > 0L;
				ulong num2;
				if (flag)
				{
					bool flag2 = num > ulong.MaxValue - value;
					if (flag2)
					{
						num2 = ulong.MaxValue;
					}
					else
					{
						num2 = value + num;
					}
				}
				else
				{
					bool flag3 = num > value;
					if (flag3)
					{
						num2 = 0UL;
					}
					else
					{
						num2 = value - num;
					}
				}
				return num2;
			}

			// Token: 0x06000E49 RID: 3657 RVA: 0x00040518 File Offset: 0x0003E718
			protected override string ValueToString(ulong v)
			{
				return v.ToString(base.formatString);
			}

			// Token: 0x06000E4A RID: 3658 RVA: 0x00040538 File Offset: 0x0003E738
			protected override ulong StringToValue(string str)
			{
				return this.parentUnsignedLongField.StringToValue(str);
			}
		}
	}
}
