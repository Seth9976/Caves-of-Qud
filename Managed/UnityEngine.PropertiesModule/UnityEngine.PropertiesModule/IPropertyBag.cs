using System;

namespace Unity.Properties
{
	// Token: 0x02000042 RID: 66
	public interface IPropertyBag
	{
		// Token: 0x06000131 RID: 305
		void Accept(ITypeVisitor visitor);

		// Token: 0x06000132 RID: 306
		void Accept(IPropertyBagVisitor visitor, ref object container);
	}
}
