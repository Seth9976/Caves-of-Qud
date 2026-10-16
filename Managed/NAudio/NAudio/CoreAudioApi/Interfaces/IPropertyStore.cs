using System;
using System.Runtime.InteropServices;

namespace NAudio.CoreAudioApi.Interfaces
{
	// Token: 0x02000124 RID: 292
	[InterfaceType(1)]
	[Guid("886d8eeb-8cf2-4446-8d02-cdba1dbdcf99")]
	internal interface IPropertyStore
	{
		// Token: 0x06000641 RID: 1601
		int GetCount(out int propCount);

		// Token: 0x06000642 RID: 1602
		int GetAt(int property, out PropertyKey key);

		// Token: 0x06000643 RID: 1603
		int GetValue(ref PropertyKey key, out PropVariant value);

		// Token: 0x06000644 RID: 1604
		int SetValue(ref PropertyKey key, ref PropVariant value);

		// Token: 0x06000645 RID: 1605
		int Commit();
	}
}
