using System;
using System.Diagnostics;
using System.Globalization;
using UnityEngine.Internal;

namespace UnityEngine.UIElements
{
	// Token: 0x020001C6 RID: 454
	public class UnsignedIntegerField : TextValueField<uint>
	{
		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000E22 RID: 3618 RVA: 0x00040042 File Offset: 0x0003E242
		private UnsignedIntegerField.UnsignedIntegerInput integerInput
		{
			get
			{
				return (UnsignedIntegerField.UnsignedIntegerInput)base.textInputBase;
			}
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x00040050 File Offset: 0x0003E250
		protected override string ValueToString(uint v)
		{
			return v.ToString(base.formatString, CultureInfo.InvariantCulture.NumberFormat);
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x0004007C File Offset: 0x0003E27C
		protected override uint StringToValue(string str)
		{
			uint num;
			ExpressionEvaluator.Expression expression;
			bool flag = UINumericFieldsUtils.TryConvertStringToUInt(str, base.textInputBase.originalText, out num, out expression);
			Action<ExpressionEvaluator.Expression> expressionEvaluated = this.expressionEvaluated;
			if (expressionEvaluated != null)
			{
				expressionEvaluated(expression);
			}
			return flag ? num : base.rawValue;
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x000400C3 File Offset: 0x0003E2C3
		public UnsignedIntegerField()
			: this(null, 1000)
		{
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x000400D3 File Offset: 0x0003E2D3
		public UnsignedIntegerField(int maxLength)
			: this(null, maxLength)
		{
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x000400E0 File Offset: 0x0003E2E0
		public UnsignedIntegerField(string label, int maxLength = 1000)
			: base(label, maxLength, new UnsignedIntegerField.UnsignedIntegerInput())
		{
			base.AddToClassList(UnsignedIntegerField.ussClassName);
			base.labelElement.AddToClassList(UnsignedIntegerField.labelUssClassName);
			base.visualInput.AddToClassList(UnsignedIntegerField.inputUssClassName);
			base.AddLabelDragger<uint>();
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x00040134 File Offset: 0x0003E334
		internal override bool CanTryParse(string textString)
		{
			uint num;
			return uint.TryParse(textString, out num);
		}

		// Token: 0x06000E29 RID: 3625 RVA: 0x00040149 File Offset: 0x0003E349
		public override void ApplyInputDeviceDelta(Vector3 delta, DeltaSpeed speed, uint startValue)
		{
			this.integerInput.ApplyInputDeviceDelta(delta, speed, startValue);
		}

		// Token: 0x040008DE RID: 2270
		public new static readonly string ussClassName = "unity-unsigned-integer-field";

		// Token: 0x040008DF RID: 2271
		public new static readonly string labelUssClassName = UnsignedIntegerField.ussClassName + "__label";

		// Token: 0x040008E0 RID: 2272
		public new static readonly string inputUssClassName = UnsignedIntegerField.ussClassName + "__input";

		// Token: 0x020001C7 RID: 455
		[ExcludeFromDocs]
		[Serializable]
		public new class UxmlSerializedData : TextInputBaseField<uint>.UxmlSerializedData
		{
			// Token: 0x06000E2B RID: 3627 RVA: 0x0004018F File Offset: 0x0003E38F
			[Conditional("UNITY_EDITOR")]
			public new static void Register()
			{
				TextInputBaseField<uint>.UxmlSerializedData.Register();
			}

			// Token: 0x06000E2C RID: 3628 RVA: 0x00040198 File Offset: 0x0003E398
			public override object CreateInstance()
			{
				return new UnsignedIntegerField();
			}
		}

		// Token: 0x020001C8 RID: 456
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<UnsignedIntegerField, UnsignedIntegerField.UxmlTraits>
		{
		}

		// Token: 0x020001C9 RID: 457
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : TextValueFieldTraits<uint, UxmlUnsignedIntAttributeDescription>
		{
		}

		// Token: 0x020001CA RID: 458
		private class UnsignedIntegerInput : TextValueField<uint>.TextValueInput
		{
			// Token: 0x17000266 RID: 614
			// (get) Token: 0x06000E30 RID: 3632 RVA: 0x000401BA File Offset: 0x0003E3BA
			private UnsignedIntegerField parentUnsignedIntegerField
			{
				get
				{
					return (UnsignedIntegerField)base.parent;
				}
			}

			// Token: 0x06000E31 RID: 3633 RVA: 0x000401C7 File Offset: 0x0003E3C7
			internal UnsignedIntegerInput()
			{
				base.formatString = UINumericFieldsUtils.k_IntFieldFormatString;
			}

			// Token: 0x17000267 RID: 615
			// (get) Token: 0x06000E32 RID: 3634 RVA: 0x0002AE61 File Offset: 0x00029061
			protected override string allowedCharacters
			{
				get
				{
					return UINumericFieldsUtils.k_AllowedCharactersForInt;
				}
			}

			// Token: 0x06000E33 RID: 3635 RVA: 0x000401E0 File Offset: 0x0003E3E0
			public override void ApplyInputDeviceDelta(Vector3 delta, DeltaSpeed speed, uint startValue)
			{
				double num = (double)NumericFieldDraggerUtility.CalculateIntDragSensitivity((long)((ulong)startValue));
				float num2 = NumericFieldDraggerUtility.Acceleration(speed == DeltaSpeed.Fast, speed == DeltaSpeed.Slow);
				long num3 = (long)((ulong)this.StringToValue(base.text));
				num3 += (long)Math.Round((double)NumericFieldDraggerUtility.NiceDelta(delta, num2) * num);
				bool isDelayed = this.parentUnsignedIntegerField.isDelayed;
				if (isDelayed)
				{
					base.text = this.ValueToString(Mathf.ClampToUInt(num3));
				}
				else
				{
					this.parentUnsignedIntegerField.value = Mathf.ClampToUInt(num3);
				}
			}

			// Token: 0x06000E34 RID: 3636 RVA: 0x00040268 File Offset: 0x0003E468
			protected override string ValueToString(uint v)
			{
				return v.ToString(base.formatString);
			}

			// Token: 0x06000E35 RID: 3637 RVA: 0x00040288 File Offset: 0x0003E488
			protected override uint StringToValue(string str)
			{
				return this.parentUnsignedIntegerField.StringToValue(str);
			}
		}
	}
}
