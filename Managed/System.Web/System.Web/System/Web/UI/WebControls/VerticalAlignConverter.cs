using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000436 RID: 1078
	internal class VerticalAlignConverter : EnumConverter
	{
		// Token: 0x060031B0 RID: 12720 RVA: 0x00082F67 File Offset: 0x00081167
		public VerticalAlignConverter()
			: base(typeof(VerticalAlign))
		{
		}

		// Token: 0x060031B1 RID: 12721 RVA: 0x000621FE File Offset: 0x000603FE
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x060031B2 RID: 12722 RVA: 0x00062208 File Offset: 0x00060408
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x060031B3 RID: 12723 RVA: 0x00062213 File Offset: 0x00060413
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return base.CanConvertTo(context, destinationType);
		}

		// Token: 0x060031B4 RID: 12724 RVA: 0x0006221D File Offset: 0x0006041D
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
