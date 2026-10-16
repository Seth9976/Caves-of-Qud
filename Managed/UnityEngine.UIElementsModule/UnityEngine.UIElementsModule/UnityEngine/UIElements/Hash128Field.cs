using System;
using System.Diagnostics;
using UnityEngine.Internal;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.UIElements
{
	// Token: 0x02000103 RID: 259
	[MovedFrom(true, "UnityEditor.UIElements", "UnityEditor.UIElementsModule", null)]
	public class Hash128Field : TextInputBaseField<Hash128>
	{
		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000859 RID: 2137 RVA: 0x00024F42 File Offset: 0x00023142
		private Hash128Field.Hash128Input integerInput
		{
			get
			{
				return (Hash128Field.Hash128Input)base.textInputBase;
			}
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x00024F4F File Offset: 0x0002314F
		public Hash128Field()
			: this(null, -1)
		{
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x00024F5B File Offset: 0x0002315B
		public Hash128Field(int maxLength)
			: this(null, maxLength)
		{
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x00024F68 File Offset: 0x00023168
		public Hash128Field(string label, int maxLength = -1)
			: base(label, maxLength, '\0', new Hash128Field.Hash128Input())
		{
			this.SetValueWithoutNotify(default(Hash128));
			base.AddToClassList(Hash128Field.ussClassName);
			base.labelElement.AddToClassList(Hash128Field.labelUssClassName);
			base.visualInput.AddToClassList(Hash128Field.inputUssClassName);
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600085D RID: 2141 RVA: 0x00024FC4 File Offset: 0x000231C4
		// (set) Token: 0x0600085E RID: 2142 RVA: 0x00024FDC File Offset: 0x000231DC
		public override Hash128 value
		{
			get
			{
				return base.value;
			}
			set
			{
				base.value = value;
				bool updateTextFromValue = this.m_UpdateTextFromValue;
				if (updateTextFromValue)
				{
					base.text = base.rawValue.ToString();
				}
			}
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x00025018 File Offset: 0x00023218
		internal override void UpdateValueFromText()
		{
			this.m_UpdateTextFromValue = false;
			try
			{
				this.value = this.StringToValue(base.text);
			}
			finally
			{
				this.m_UpdateTextFromValue = true;
			}
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x00025060 File Offset: 0x00023260
		internal override void UpdateTextFromValue()
		{
			base.text = this.ValueToString(base.rawValue);
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x00025078 File Offset: 0x00023278
		public override void SetValueWithoutNotify(Hash128 newValue)
		{
			base.SetValueWithoutNotify(newValue);
			bool updateTextFromValue = this.m_UpdateTextFromValue;
			if (updateTextFromValue)
			{
				base.text = base.rawValue.ToString();
			}
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x000250B8 File Offset: 0x000232B8
		protected override string ValueToString(Hash128 value)
		{
			return value.ToString();
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x000250D8 File Offset: 0x000232D8
		protected override Hash128 StringToValue(string str)
		{
			return Hash128Field.Hash128Input.Parse(str);
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x000250F0 File Offset: 0x000232F0
		[EventInterest(new Type[] { typeof(FocusOutEvent) })]
		protected override void HandleEventBubbleUp(EventBase evt)
		{
			base.HandleEventBubbleUp(evt);
			bool isReadOnly = base.isReadOnly;
			if (!isReadOnly)
			{
				bool flag = evt.eventTypeId == EventBase<FocusOutEvent>.TypeId();
				if (flag)
				{
					bool flag2 = string.IsNullOrEmpty(base.text);
					if (flag2)
					{
						this.value = default(Hash128);
					}
					else
					{
						base.textInputBase.UpdateValueFromText();
						base.textInputBase.UpdateTextFromValue();
					}
				}
			}
		}

		// Token: 0x040004B6 RID: 1206
		public new static readonly string ussClassName = "unity-hash128-field";

		// Token: 0x040004B7 RID: 1207
		public new static readonly string labelUssClassName = Hash128Field.ussClassName + "__label";

		// Token: 0x040004B8 RID: 1208
		public new static readonly string inputUssClassName = Hash128Field.ussClassName + "__input";

		// Token: 0x02000104 RID: 260
		[ExcludeFromDocs]
		[Serializable]
		public new class UxmlSerializedData : TextInputBaseField<Hash128>.UxmlSerializedData
		{
			// Token: 0x06000866 RID: 2150 RVA: 0x00025196 File Offset: 0x00023396
			[Conditional("UNITY_EDITOR")]
			public new static void Register()
			{
				TextInputBaseField<Hash128>.UxmlSerializedData.Register();
			}

			// Token: 0x06000867 RID: 2151 RVA: 0x0002519F File Offset: 0x0002339F
			public override object CreateInstance()
			{
				return new Hash128Field();
			}
		}

		// Token: 0x02000105 RID: 261
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<Hash128Field, Hash128Field.UxmlTraits>
		{
		}

		// Token: 0x02000106 RID: 262
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : TextValueFieldTraits<Hash128, UxmlHash128AttributeDescription>
		{
		}

		// Token: 0x02000107 RID: 263
		private class Hash128Input : TextInputBaseField<Hash128>.TextInputBase
		{
			// Token: 0x17000133 RID: 307
			// (get) Token: 0x0600086B RID: 2155 RVA: 0x000251C1 File Offset: 0x000233C1
			private Hash128Field hash128Field
			{
				get
				{
					return (Hash128Field)base.parent;
				}
			}

			// Token: 0x0600086C RID: 2156 RVA: 0x000251CE File Offset: 0x000233CE
			internal Hash128Input()
			{
				base.textEdition.AcceptCharacter = new Func<char, bool>(this.AcceptCharacter);
			}

			// Token: 0x17000134 RID: 308
			// (get) Token: 0x0600086D RID: 2157 RVA: 0x000251F1 File Offset: 0x000233F1
			protected string allowedCharacters
			{
				get
				{
					return "0123456789abcdefABCDEF";
				}
			}

			// Token: 0x0600086E RID: 2158 RVA: 0x000251F8 File Offset: 0x000233F8
			internal override bool AcceptCharacter(char c)
			{
				return base.AcceptCharacter(c) && c != '\0' && this.allowedCharacters.IndexOf(c) != -1;
			}

			// Token: 0x17000135 RID: 309
			// (get) Token: 0x0600086F RID: 2159 RVA: 0x0002522B File Offset: 0x0002342B
			public string formatString
			{
				get
				{
					return UINumericFieldsUtils.k_IntFieldFormatString;
				}
			}

			// Token: 0x06000870 RID: 2160 RVA: 0x00025234 File Offset: 0x00023434
			protected string ValueToString(Hash128 value)
			{
				return value.ToString();
			}

			// Token: 0x06000871 RID: 2161 RVA: 0x00025254 File Offset: 0x00023454
			protected override Hash128 StringToValue(string str)
			{
				return Hash128Field.Hash128Input.Parse(str);
			}

			// Token: 0x06000872 RID: 2162 RVA: 0x0002526C File Offset: 0x0002346C
			internal static Hash128 Parse(string str)
			{
				ulong num;
				bool flag = str.Length == 1 && ulong.TryParse(str, out num);
				Hash128 hash;
				if (flag)
				{
					hash = new Hash128(num, 0UL);
				}
				else
				{
					hash = Hash128.Parse(str);
				}
				return hash;
			}
		}
	}
}
