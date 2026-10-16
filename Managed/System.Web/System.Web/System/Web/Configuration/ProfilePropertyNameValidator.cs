using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x020005C4 RID: 1476
	internal class ProfilePropertyNameValidator : StringValidator
	{
		// Token: 0x06003FA4 RID: 16292 RVA: 0x000A611D File Offset: 0x000A431D
		public ProfilePropertyNameValidator()
			: base(1)
		{
		}

		// Token: 0x06003FA5 RID: 16293 RVA: 0x000A6128 File Offset: 0x000A4328
		public override void Validate(object value)
		{
			base.Validate(value);
			string text = value as string;
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			string text2 = text.Trim();
			if (string.IsNullOrEmpty(text2))
			{
				throw new ArgumentException("name cannot be empty.");
			}
			if (text2.Contains("."))
			{
				throw new ArgumentException("name cannot contain period");
			}
		}
	}
}
