using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x020000C7 RID: 199
	internal class PdbSlot
	{
		// Token: 0x060001B0 RID: 432 RVA: 0x000069A4 File Offset: 0x00004BA4
		internal PdbSlot(BitAccess bits)
		{
			AttrSlotSym attrSlotSym = default(AttrSlotSym);
			bits.ReadUInt32(out attrSlotSym.index);
			bits.ReadUInt32(out attrSlotSym.typind);
			bits.ReadUInt32(out attrSlotSym.offCod);
			bits.ReadUInt16(out attrSlotSym.segCod);
			bits.ReadUInt16(out attrSlotSym.flags);
			bits.ReadCString(out attrSlotSym.name);
			this.slot = attrSlotSym.index;
			this.typeToken = attrSlotSym.typind;
			this.name = attrSlotSym.name;
			this.flags = attrSlotSym.flags;
		}

		// Token: 0x0400038D RID: 909
		internal uint slot;

		// Token: 0x0400038E RID: 910
		internal uint typeToken;

		// Token: 0x0400038F RID: 911
		internal string name;

		// Token: 0x04000390 RID: 912
		internal ushort flags;
	}
}
