using System;
using System.Runtime.InteropServices;

namespace Microsoft.DiaSymReader
{
	// Token: 0x020000FD RID: 253
	[StructLayout(2)]
	internal struct VariantStructure
	{
		// Token: 0x060008B3 RID: 2227 RVA: 0x00018EEC File Offset: 0x000170EC
		public VariantStructure(DateTime date)
		{
			this = default(VariantStructure);
			this._longValue = date.Ticks;
			this._type = 7;
		}

		// Token: 0x04000267 RID: 615
		[FieldOffset(0)]
		private readonly short _type;

		// Token: 0x04000268 RID: 616
		[FieldOffset(8)]
		private readonly long _longValue;

		// Token: 0x04000269 RID: 617
		[FieldOffset(8)]
		private readonly VariantPadding _padding;

		// Token: 0x0400026A RID: 618
		[FieldOffset(0)]
		private readonly decimal _decimalValue;

		// Token: 0x0400026B RID: 619
		[FieldOffset(8)]
		private readonly bool _boolValue;

		// Token: 0x0400026C RID: 620
		[FieldOffset(8)]
		private readonly long _intValue;

		// Token: 0x0400026D RID: 621
		[FieldOffset(8)]
		private readonly double _doubleValue;
	}
}
