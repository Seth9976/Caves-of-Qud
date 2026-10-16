using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x020000C6 RID: 198
	internal class PdbScope
	{
		// Token: 0x060001AE RID: 430 RVA: 0x00006727 File Offset: 0x00004927
		internal PdbScope(uint address, uint length, PdbSlot[] slots, PdbConstant[] constants, string[] usedNamespaces)
		{
			this.constants = constants;
			this.slots = slots;
			this.scopes = new PdbScope[0];
			this.usedNamespaces = usedNamespaces;
			this.address = address;
			this.offset = 0U;
			this.length = length;
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00006768 File Offset: 0x00004968
		internal PdbScope(uint funcOffset, BlockSym32 block, BitAccess bits, out uint typind)
		{
			this.address = block.off;
			this.offset = block.off - funcOffset;
			this.length = block.len;
			typind = 0U;
			int num;
			int num2;
			int num3;
			int num4;
			PdbFunction.CountScopesAndSlots(bits, block.end, out num, out num2, out num3, out num4);
			this.constants = new PdbConstant[num];
			this.scopes = new PdbScope[num2];
			this.slots = new PdbSlot[num3];
			this.usedNamespaces = new string[num4];
			int num5 = 0;
			int num6 = 0;
			int num7 = 0;
			int num8 = 0;
			while ((long)bits.Position < (long)((ulong)block.end))
			{
				ushort num9;
				bits.ReadUInt16(out num9);
				int position = bits.Position;
				int num10 = bits.Position + (int)num9;
				bits.Position = position;
				ushort num11;
				bits.ReadUInt16(out num11);
				if (num11 <= 4355)
				{
					if (num11 == 6)
					{
						bits.Position = num10;
						continue;
					}
					if (num11 == 4355)
					{
						BlockSym32 blockSym = default(BlockSym32);
						bits.ReadUInt32(out blockSym.parent);
						bits.ReadUInt32(out blockSym.end);
						bits.ReadUInt32(out blockSym.len);
						bits.ReadUInt32(out blockSym.off);
						bits.ReadUInt16(out blockSym.seg);
						bits.SkipCString(out blockSym.name);
						bits.Position = num10;
						this.scopes[num6++] = new PdbScope(funcOffset, blockSym, bits, out typind);
						continue;
					}
				}
				else
				{
					if (num11 == 4384)
					{
						this.slots[num7++] = new PdbSlot(bits);
						bits.Position = num10;
						continue;
					}
					if (num11 == 4388)
					{
						bits.ReadCString(out this.usedNamespaces[num8++]);
						bits.Position = num10;
						continue;
					}
					if (num11 == 4397)
					{
						this.constants[num5++] = new PdbConstant(bits);
						bits.Position = num10;
						continue;
					}
				}
				bits.Position = num10;
			}
			if ((long)bits.Position != (long)((ulong)block.end))
			{
				throw new Exception("Not at S_END");
			}
			ushort num12;
			bits.ReadUInt16(out num12);
			ushort num13;
			bits.ReadUInt16(out num13);
			if (num13 != 6)
			{
				throw new Exception("Missing S_END");
			}
		}

		// Token: 0x04000386 RID: 902
		internal PdbConstant[] constants;

		// Token: 0x04000387 RID: 903
		internal PdbSlot[] slots;

		// Token: 0x04000388 RID: 904
		internal PdbScope[] scopes;

		// Token: 0x04000389 RID: 905
		internal string[] usedNamespaces;

		// Token: 0x0400038A RID: 906
		internal uint address;

		// Token: 0x0400038B RID: 907
		internal uint offset;

		// Token: 0x0400038C RID: 908
		internal uint length;
	}
}
