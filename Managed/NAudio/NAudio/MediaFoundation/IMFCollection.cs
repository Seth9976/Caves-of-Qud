using System;
using System.Runtime.InteropServices;

namespace NAudio.MediaFoundation
{
	// Token: 0x02000049 RID: 73
	[Guid("5BC8A76B-869A-46A3-9B03-FA218A66AEBE")]
	[InterfaceType(1)]
	[ComImport]
	public interface IMFCollection
	{
		// Token: 0x0600016B RID: 363
		void GetElementCount(out int pcElements);

		// Token: 0x0600016C RID: 364
		void GetElement([In] int dwElementIndex, [MarshalAs(25)] out object ppUnkElement);

		// Token: 0x0600016D RID: 365
		void AddElement([MarshalAs(25)] [In] object pUnkElement);

		// Token: 0x0600016E RID: 366
		void RemoveElement([In] int dwElementIndex, [MarshalAs(25)] out object ppUnkElement);

		// Token: 0x0600016F RID: 367
		void InsertElementAt([In] int dwIndex, [MarshalAs(25)] [In] object pUnknown);

		// Token: 0x06000170 RID: 368
		void RemoveAllElements();
	}
}
