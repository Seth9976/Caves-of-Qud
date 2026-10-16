using System;
using System.Globalization;

namespace System.ComponentModel.DataAnnotations
{
	/// <summary>Specifies the minimum length of array of string data allowed in a property.</summary>
	// Token: 0x02000024 RID: 36
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
	public class MinLengthAttribute : ValidationAttribute
	{
		/// <summary>Gets or sets the minimum allowable length of the array or string data.</summary>
		/// <returns>The minimum allowable length of the array or string data.</returns>
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x000039E1 File Offset: 0x00001BE1
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x000039E9 File Offset: 0x00001BE9
		public int Length { get; private set; }

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataAnnotations.MinLengthAttribute" /> class.</summary>
		/// <param name="length">The length of the array or string data.</param>
		// Token: 0x060000C9 RID: 201 RVA: 0x000039F2 File Offset: 0x00001BF2
		public MinLengthAttribute(int length)
			: base("The field {0} must be a string or array type with a minimum length of '{1}'.")
		{
			this.Length = length;
		}

		/// <summary>Determines whether a specified object is valid.</summary>
		/// <returns>true if the specified object is valid; otherwise, false.</returns>
		/// <param name="value">The object to validate.</param>
		// Token: 0x060000CA RID: 202 RVA: 0x00003A08 File Offset: 0x00001C08
		public override bool IsValid(object value)
		{
			this.EnsureLegalLengths();
			if (value == null)
			{
				return true;
			}
			string text = value as string;
			int num;
			if (text != null)
			{
				num = text.Length;
			}
			else
			{
				int num2;
				if (!CountPropertyHelper.TryGetCount(value, out num2))
				{
					throw new InvalidCastException(string.Format("The field of type {0} must be a string, array or ICollection type.", value.GetType()));
				}
				num = num2;
			}
			return num >= this.Length;
		}

		/// <summary>Applies formatting to a specified error message.</summary>
		/// <returns>A localized string to describe the minimum acceptable length.</returns>
		/// <param name="name">The name to include in the formatted string.</param>
		// Token: 0x060000CB RID: 203 RVA: 0x00003A65 File Offset: 0x00001C65
		public override string FormatErrorMessage(string name)
		{
			return string.Format(CultureInfo.CurrentCulture, base.ErrorMessageString, name, this.Length);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00003A83 File Offset: 0x00001C83
		private void EnsureLegalLengths()
		{
			if (this.Length < 0)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "MinLengthAttribute must have a Length value that is zero or greater.", Array.Empty<object>()));
			}
		}
	}
}
