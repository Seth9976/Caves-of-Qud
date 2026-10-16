using System;
using System.Reflection;

namespace System.Web.UI
{
	// Token: 0x02000239 RID: 569
	internal class UnknownAttributeDescriptor
	{
		// Token: 0x06001777 RID: 6007 RVA: 0x0003DCB3 File Offset: 0x0003BEB3
		public UnknownAttributeDescriptor(MemberInfo memberInfo, object value)
		{
			this.Info = memberInfo;
			this.Value = value;
		}

		// Token: 0x040015E9 RID: 5609
		public MemberInfo Info;

		// Token: 0x040015EA RID: 5610
		public object Value;
	}
}
