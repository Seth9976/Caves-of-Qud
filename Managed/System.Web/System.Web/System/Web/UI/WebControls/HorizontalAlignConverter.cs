using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003A9 RID: 937
	internal class HorizontalAlignConverter : EnumConverter
	{
		// Token: 0x0600263A RID: 9786 RVA: 0x000621EC File Offset: 0x000603EC
		public HorizontalAlignConverter()
			: base(typeof(HorizontalAlign))
		{
		}

		// Token: 0x0600263B RID: 9787 RVA: 0x000621FE File Offset: 0x000603FE
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x0600263C RID: 9788 RVA: 0x00062208 File Offset: 0x00060408
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x0600263D RID: 9789 RVA: 0x00062213 File Offset: 0x00060413
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return base.CanConvertTo(context, destinationType);
		}

		// Token: 0x0600263E RID: 9790 RVA: 0x0006221D File Offset: 0x0006041D
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
