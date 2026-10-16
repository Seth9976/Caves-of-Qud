using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x020003F6 RID: 1014
	internal class DbiModuleInfo
	{
		// Token: 0x060015A5 RID: 5541 RVA: 0x00044044 File Offset: 0x00042244
		internal DbiModuleInfo(BitAccess bits, bool readStrings)
		{
			bits.ReadInt32(out this.opened);
			new DbiSecCon(bits);
			bits.ReadUInt16(out this.flags);
			bits.ReadInt16(out this.stream);
			bits.ReadInt32(out this.cbSyms);
			bits.ReadInt32(out this.cbOldLines);
			bits.ReadInt32(out this.cbLines);
			bits.ReadInt16(out this.files);
			bits.ReadInt16(out this.pad1);
			bits.ReadUInt32(out this.offsets);
			bits.ReadInt32(out this.niSource);
			bits.ReadInt32(out this.niCompiler);
			if (readStrings)
			{
				bits.ReadCString(out this.moduleName);
				bits.ReadCString(out this.objectName);
			}
			else
			{
				bits.SkipCString(out this.moduleName);
				bits.SkipCString(out this.objectName);
			}
			bits.Align(4);
		}

		// Token: 0x04000F1F RID: 3871
		internal int opened;

		// Token: 0x04000F20 RID: 3872
		internal ushort flags;

		// Token: 0x04000F21 RID: 3873
		internal short stream;

		// Token: 0x04000F22 RID: 3874
		internal int cbSyms;

		// Token: 0x04000F23 RID: 3875
		internal int cbOldLines;

		// Token: 0x04000F24 RID: 3876
		internal int cbLines;

		// Token: 0x04000F25 RID: 3877
		internal short files;

		// Token: 0x04000F26 RID: 3878
		internal short pad1;

		// Token: 0x04000F27 RID: 3879
		internal uint offsets;

		// Token: 0x04000F28 RID: 3880
		internal int niSource;

		// Token: 0x04000F29 RID: 3881
		internal int niCompiler;

		// Token: 0x04000F2A RID: 3882
		internal string moduleName;

		// Token: 0x04000F2B RID: 3883
		internal string objectName;
	}
}
