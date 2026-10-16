using System;

namespace System.Net.Mime
{
	// Token: 0x020007CE RID: 1998
	internal class Base64WriteStateInfo : WriteStateInfoBase
	{
		// Token: 0x06003FF2 RID: 16370 RVA: 0x000DA5E9 File Offset: 0x000D87E9
		internal Base64WriteStateInfo()
		{
		}

		// Token: 0x06003FF3 RID: 16371 RVA: 0x000DA5F1 File Offset: 0x000D87F1
		internal Base64WriteStateInfo(int bufferSize, byte[] header, byte[] footer, int maxLineLength, int mimeHeaderLength)
			: base(bufferSize, header, footer, maxLineLength, mimeHeaderLength)
		{
		}

		// Token: 0x17000E75 RID: 3701
		// (get) Token: 0x06003FF4 RID: 16372 RVA: 0x000DA600 File Offset: 0x000D8800
		// (set) Token: 0x06003FF5 RID: 16373 RVA: 0x000DA608 File Offset: 0x000D8808
		internal int Padding { get; set; }

		// Token: 0x17000E76 RID: 3702
		// (get) Token: 0x06003FF6 RID: 16374 RVA: 0x000DA611 File Offset: 0x000D8811
		// (set) Token: 0x06003FF7 RID: 16375 RVA: 0x000DA619 File Offset: 0x000D8819
		internal byte LastBits { get; set; }
	}
}
