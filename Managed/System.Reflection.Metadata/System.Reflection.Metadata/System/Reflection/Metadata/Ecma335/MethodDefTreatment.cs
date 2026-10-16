using System;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x0200015B RID: 347
	[Flags]
	internal enum MethodDefTreatment : byte
	{
		// Token: 0x040008B3 RID: 2227
		None = 0,
		// Token: 0x040008B4 RID: 2228
		KindMask = 15,
		// Token: 0x040008B5 RID: 2229
		Other = 1,
		// Token: 0x040008B6 RID: 2230
		DelegateMethod = 2,
		// Token: 0x040008B7 RID: 2231
		AttributeMethod = 3,
		// Token: 0x040008B8 RID: 2232
		InterfaceMethod = 4,
		// Token: 0x040008B9 RID: 2233
		Implementation = 5,
		// Token: 0x040008BA RID: 2234
		HiddenInterfaceImplementation = 6,
		// Token: 0x040008BB RID: 2235
		DisposeMethod = 7,
		// Token: 0x040008BC RID: 2236
		MarkAbstractFlag = 16,
		// Token: 0x040008BD RID: 2237
		MarkPublicFlag = 32
	}
}
