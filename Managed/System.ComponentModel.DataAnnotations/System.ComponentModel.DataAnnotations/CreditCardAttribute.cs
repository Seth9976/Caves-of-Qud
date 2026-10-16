using System;
using System.Linq;

namespace System.ComponentModel.DataAnnotations
{
	/// <summary>Specifies that a data field value is a credit card number.</summary>
	// Token: 0x0200000D RID: 13
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
	public sealed class CreditCardAttribute : DataTypeAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataAnnotations.CreditCardAttribute" /> class.</summary>
		// Token: 0x0600002F RID: 47 RVA: 0x000026BC File Offset: 0x000008BC
		public CreditCardAttribute()
			: base(DataType.CreditCard)
		{
			base.DefaultErrorMessage = "The {0} field is not a valid credit card number.";
		}

		/// <summary>Determines whether the specified credit card number is valid. </summary>
		/// <returns>true if the credit card number is valid; otherwise, false.</returns>
		/// <param name="value">The value to validate.</param>
		// Token: 0x06000030 RID: 48 RVA: 0x000026D4 File Offset: 0x000008D4
		public override bool IsValid(object value)
		{
			if (value == null)
			{
				return true;
			}
			string text = value as string;
			if (text == null)
			{
				return false;
			}
			text = text.Replace("-", "");
			text = text.Replace(" ", "");
			int num = 0;
			bool flag = false;
			foreach (char c in text.Reverse<char>())
			{
				if (c < '0' || c > '9')
				{
					return false;
				}
				int i = (int)((c - '0') * (flag ? '\u0002' : '\u0001'));
				flag = !flag;
				while (i > 0)
				{
					num += i % 10;
					i /= 10;
				}
			}
			return num % 10 == 0;
		}
	}
}
