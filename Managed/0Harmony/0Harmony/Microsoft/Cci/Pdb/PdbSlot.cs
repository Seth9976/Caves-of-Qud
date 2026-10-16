using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x0200040E RID: 1038
	internal class PdbSlot
	{
		// Token: 0x060015EE RID: 5614 RVA: 0x00046710 File Offset: 0x00044910
		internal PdbSlot(uint slot, uint typeToken, string name, ushort flags)
		{
			this.slot = slot;
			this.typeToken = typeToken;
			this.name = name;
			this.flags = flags;
		}

		// Token: 0x060015EF RID: 5615 RVA: 0x00046738 File Offset: 0x00044938
		internal PdbSlot(BitAccess bits)
		{
			AttrSlotSym attrSlotSym;
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

		// Token: 0x04000F7D RID: 3965
		internal uint slot;

		// Token: 0x04000F7E RID: 3966
		internal uint typeToken;

		// Token: 0x04000F7F RID: 3967
		internal string name;

		// Token: 0x04000F80 RID: 3968
		internal ushort flags;
	}
}
