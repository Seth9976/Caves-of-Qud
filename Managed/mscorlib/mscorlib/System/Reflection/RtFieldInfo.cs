using System;
using System.Globalization;

namespace System.Reflection
{
	// Token: 0x020008F5 RID: 2293
	internal abstract class RtFieldInfo : FieldInfo
	{
		// Token: 0x06004D17 RID: 19735
		internal abstract object UnsafeGetValue(object obj);

		// Token: 0x06004D18 RID: 19736
		internal abstract void UnsafeSetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture);

		// Token: 0x06004D19 RID: 19737
		internal abstract void CheckConsistency(object target);
	}
}
