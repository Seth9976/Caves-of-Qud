using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x020000C2 RID: 194
	internal class PdbFunction
	{
		// Token: 0x0600019D RID: 413 RVA: 0x00005DF4 File Offset: 0x00003FF4
		private static string StripNamespace(string module)
		{
			int num = module.LastIndexOf('.');
			if (num > 0)
			{
				return module.Substring(num + 1);
			}
			return module;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00005E1C File Offset: 0x0000401C
		internal static PdbFunction[] LoadManagedFunctions(BitAccess bits, uint limit, bool readStrings)
		{
			int position = bits.Position;
			int num = 0;
			while ((long)bits.Position < (long)((ulong)limit))
			{
				ushort num2;
				bits.ReadUInt16(out num2);
				int position2 = bits.Position;
				int num3 = bits.Position + (int)num2;
				bits.Position = position2;
				ushort num4;
				bits.ReadUInt16(out num4);
				if (num4 != 6)
				{
					if (num4 - 4394 <= 1)
					{
						ManProcSym manProcSym = default(ManProcSym);
						bits.ReadUInt32(out manProcSym.parent);
						bits.ReadUInt32(out manProcSym.end);
						bits.Position = (int)manProcSym.end;
						num++;
					}
					else
					{
						bits.Position = num3;
					}
				}
				else
				{
					bits.Position = num3;
				}
			}
			if (num == 0)
			{
				return null;
			}
			bits.Position = position;
			PdbFunction[] array = new PdbFunction[num];
			int num5 = 0;
			while ((long)bits.Position < (long)((ulong)limit))
			{
				ushort num6;
				bits.ReadUInt16(out num6);
				int position3 = bits.Position;
				int num7 = bits.Position + (int)num6;
				ushort num8;
				bits.ReadUInt16(out num8);
				SYM sym = (SYM)num8;
				if (sym == SYM.S_GMANPROC || sym == SYM.S_LMANPROC)
				{
					ManProcSym manProcSym2 = default(ManProcSym);
					bits.ReadUInt32(out manProcSym2.parent);
					bits.ReadUInt32(out manProcSym2.end);
					bits.ReadUInt32(out manProcSym2.next);
					bits.ReadUInt32(out manProcSym2.len);
					bits.ReadUInt32(out manProcSym2.dbgStart);
					bits.ReadUInt32(out manProcSym2.dbgEnd);
					bits.ReadUInt32(out manProcSym2.token);
					bits.ReadUInt32(out manProcSym2.off);
					bits.ReadUInt16(out manProcSym2.seg);
					bits.ReadUInt8(out manProcSym2.flags);
					bits.ReadUInt16(out manProcSym2.retReg);
					if (readStrings)
					{
						bits.ReadCString(out manProcSym2.name);
					}
					else
					{
						bits.SkipCString(out manProcSym2.name);
					}
					bits.Position = num7;
					array[num5++] = new PdbFunction(manProcSym2, bits);
				}
				else
				{
					bits.Position = num7;
				}
			}
			return array;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00006004 File Offset: 0x00004204
		internal static void CountScopesAndSlots(BitAccess bits, uint limit, out int constants, out int scopes, out int slots, out int usedNamespaces)
		{
			int position = bits.Position;
			constants = 0;
			slots = 0;
			scopes = 0;
			usedNamespaces = 0;
			while ((long)bits.Position < (long)((ulong)limit))
			{
				ushort num;
				bits.ReadUInt16(out num);
				int position2 = bits.Position;
				int num2 = bits.Position + (int)num;
				bits.Position = position2;
				ushort num3;
				bits.ReadUInt16(out num3);
				if (num3 <= 4384)
				{
					if (num3 == 4355)
					{
						BlockSym32 blockSym = default(BlockSym32);
						bits.ReadUInt32(out blockSym.parent);
						bits.ReadUInt32(out blockSym.end);
						scopes++;
						bits.Position = (int)blockSym.end;
						continue;
					}
					if (num3 == 4384)
					{
						slots++;
						bits.Position = num2;
						continue;
					}
				}
				else
				{
					if (num3 == 4388)
					{
						usedNamespaces++;
						bits.Position = num2;
						continue;
					}
					if (num3 == 4397)
					{
						constants++;
						bits.Position = num2;
						continue;
					}
				}
				bits.Position = num2;
			}
			bits.Position = position;
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00002F43 File Offset: 0x00001143
		internal PdbFunction()
		{
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00006108 File Offset: 0x00004308
		internal PdbFunction(ManProcSym proc, BitAccess bits)
		{
			this.token = proc.token;
			this.segment = (uint)proc.seg;
			this.address = proc.off;
			this.length = proc.len;
			if (proc.seg != 1)
			{
				throw new PdbDebugException("Segment is {0}, not 1.", new object[] { proc.seg });
			}
			if (proc.parent != 0U || proc.next != 0U)
			{
				throw new PdbDebugException("Warning parent={0}, next={1}", new object[] { proc.parent, proc.next });
			}
			int num;
			int num2;
			int num3;
			int num4;
			PdbFunction.CountScopesAndSlots(bits, proc.end, out num, out num2, out num3, out num4);
			int num5 = ((num > 0 || num3 > 0 || num4 > 0) ? 1 : 0);
			int num6 = 0;
			int num7 = 0;
			int num8 = 0;
			this.scopes = new PdbScope[num2 + num5];
			this.slots = new PdbSlot[num3];
			this.constants = new PdbConstant[num];
			this.usedNamespaces = new string[num4];
			if (num5 > 0)
			{
				this.scopes[0] = new PdbScope(this.address, proc.len, this.slots, this.constants, this.usedNamespaces);
			}
			while ((long)bits.Position < (long)((ulong)proc.end))
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
					if (num11 != 1028)
					{
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
							this.scopes[num5++] = new PdbScope(this.address, blockSym, bits, out this.slotToken);
							bits.Position = (int)blockSym.end;
							continue;
						}
					}
					else
					{
						OemSymbol oemSymbol = default(OemSymbol);
						bits.ReadGuid(out oemSymbol.idOem);
						bits.ReadUInt32(out oemSymbol.typind);
						if (oemSymbol.idOem == PdbFunction.msilMetaData)
						{
							string text = bits.ReadString();
							if (text == "MD2")
							{
								byte b;
								bits.ReadUInt8(out b);
								if (b == 4)
								{
									byte b2;
									bits.ReadUInt8(out b2);
									bits.Align(4);
									for (;;)
									{
										byte b3 = b2;
										b2 = b3 - 1;
										if (b3 <= 0)
										{
											break;
										}
										this.ReadCustomMetadata(bits);
									}
								}
							}
							else if (text == "asyncMethodInfo")
							{
								this.synchronizationInformation = new PdbSynchronizationInformation(bits);
							}
							bits.Position = num10;
							continue;
						}
						throw new PdbDebugException("OEM section: guid={0} ti={1}", new object[] { oemSymbol.idOem, oemSymbol.typind });
					}
				}
				else
				{
					if (num11 == 4384)
					{
						this.slots[num6++] = new PdbSlot(bits);
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
						this.constants[num7++] = new PdbConstant(bits);
						bits.Position = num10;
						continue;
					}
				}
				bits.Position = num10;
			}
			if ((long)bits.Position != (long)((ulong)proc.end))
			{
				throw new PdbDebugException("Not at S_END", new object[0]);
			}
			ushort num12;
			bits.ReadUInt16(out num12);
			ushort num13;
			bits.ReadUInt16(out num13);
			if (num13 != 6)
			{
				throw new PdbDebugException("Missing S_END", new object[0]);
			}
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x000064F8 File Offset: 0x000046F8
		private void ReadCustomMetadata(BitAccess bits)
		{
			int position = bits.Position;
			byte b;
			bits.ReadUInt8(out b);
			if (b != 4)
			{
				throw new PdbDebugException("Unknown custom metadata item version: {0}", new object[] { b });
			}
			byte b2;
			bits.ReadUInt8(out b2);
			bits.Align(4);
			uint num;
			bits.ReadUInt32(out num);
			switch (b2)
			{
			case 0:
				this.ReadUsingInfo(bits);
				break;
			case 1:
				this.ReadForwardInfo(bits);
				break;
			case 3:
				this.ReadIteratorLocals(bits);
				break;
			case 4:
				this.ReadForwardIterator(bits);
				break;
			}
			bits.Position = position + (int)num;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00006590 File Offset: 0x00004790
		private void ReadForwardIterator(BitAccess bits)
		{
			this.iteratorClass = bits.ReadString();
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x000065A0 File Offset: 0x000047A0
		private void ReadIteratorLocals(BitAccess bits)
		{
			uint num;
			bits.ReadUInt32(out num);
			this.iteratorScopes = new List<ILocalScope>((int)num);
			while (num-- != 0U)
			{
				uint num2;
				bits.ReadUInt32(out num2);
				uint num3;
				bits.ReadUInt32(out num3);
				this.iteratorScopes.Add(new PdbIteratorScope(num2, num3 - num2));
			}
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x000065EE File Offset: 0x000047EE
		private void ReadForwardInfo(BitAccess bits)
		{
			bits.ReadUInt32(out this.tokenOfMethodWhoseUsingInfoAppliesToThisMethod);
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x000065FC File Offset: 0x000047FC
		private void ReadUsingInfo(BitAccess bits)
		{
			ushort num;
			bits.ReadUInt16(out num);
			this.usingCounts = new ushort[(int)num];
			for (ushort num2 = 0; num2 < num; num2 += 1)
			{
				bits.ReadUInt16(out this.usingCounts[(int)num2]);
			}
		}

		// Token: 0x0400036A RID: 874
		internal static readonly Guid msilMetaData = new Guid(3337240521U, 22963, 18902, 188, 37, 9, 2, 187, 171, 180, 96);

		// Token: 0x0400036B RID: 875
		internal static readonly IComparer byAddress = new PdbFunction.PdbFunctionsByAddress();

		// Token: 0x0400036C RID: 876
		internal static readonly IComparer byAddressAndToken = new PdbFunction.PdbFunctionsByAddressAndToken();

		// Token: 0x0400036D RID: 877
		internal uint token;

		// Token: 0x0400036E RID: 878
		internal uint slotToken;

		// Token: 0x0400036F RID: 879
		internal uint tokenOfMethodWhoseUsingInfoAppliesToThisMethod;

		// Token: 0x04000370 RID: 880
		internal uint segment;

		// Token: 0x04000371 RID: 881
		internal uint address;

		// Token: 0x04000372 RID: 882
		internal uint length;

		// Token: 0x04000373 RID: 883
		internal PdbScope[] scopes;

		// Token: 0x04000374 RID: 884
		internal PdbSlot[] slots;

		// Token: 0x04000375 RID: 885
		internal PdbConstant[] constants;

		// Token: 0x04000376 RID: 886
		internal string[] usedNamespaces;

		// Token: 0x04000377 RID: 887
		internal PdbLines[] lines;

		// Token: 0x04000378 RID: 888
		internal ushort[] usingCounts;

		// Token: 0x04000379 RID: 889
		internal IEnumerable<INamespaceScope> namespaceScopes;

		// Token: 0x0400037A RID: 890
		internal string iteratorClass;

		// Token: 0x0400037B RID: 891
		internal List<ILocalScope> iteratorScopes;

		// Token: 0x0400037C RID: 892
		internal PdbSynchronizationInformation synchronizationInformation;

		// Token: 0x020000EF RID: 239
		internal class PdbFunctionsByAddress : IComparer
		{
			// Token: 0x060001C3 RID: 451 RVA: 0x00006C74 File Offset: 0x00004E74
			public int Compare(object x, object y)
			{
				PdbFunction pdbFunction = (PdbFunction)x;
				PdbFunction pdbFunction2 = (PdbFunction)y;
				if (pdbFunction.segment < pdbFunction2.segment)
				{
					return -1;
				}
				if (pdbFunction.segment > pdbFunction2.segment)
				{
					return 1;
				}
				if (pdbFunction.address < pdbFunction2.address)
				{
					return -1;
				}
				if (pdbFunction.address > pdbFunction2.address)
				{
					return 1;
				}
				return 0;
			}
		}

		// Token: 0x020000F0 RID: 240
		internal class PdbFunctionsByAddressAndToken : IComparer
		{
			// Token: 0x060001C5 RID: 453 RVA: 0x00006CD0 File Offset: 0x00004ED0
			public int Compare(object x, object y)
			{
				PdbFunction pdbFunction = (PdbFunction)x;
				PdbFunction pdbFunction2 = (PdbFunction)y;
				if (pdbFunction.segment < pdbFunction2.segment)
				{
					return -1;
				}
				if (pdbFunction.segment > pdbFunction2.segment)
				{
					return 1;
				}
				if (pdbFunction.address < pdbFunction2.address)
				{
					return -1;
				}
				if (pdbFunction.address > pdbFunction2.address)
				{
					return 1;
				}
				if (pdbFunction.token < pdbFunction2.token)
				{
					return -1;
				}
				if (pdbFunction.token > pdbFunction2.token)
				{
					return 1;
				}
				return 0;
			}
		}
	}
}
