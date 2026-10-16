using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x02000058 RID: 88
	internal class DbiModuleInfo
	{
		// Token: 0x0600017C RID: 380 RVA: 0x000045E0 File Offset: 0x000027E0
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

		// Token: 0x04000189 RID: 393
		internal int opened;

		// Token: 0x0400018A RID: 394
		internal ushort flags;

		// Token: 0x0400018B RID: 395
		internal short stream;

		// Token: 0x0400018C RID: 396
		internal int cbSyms;

		// Token: 0x0400018D RID: 397
		internal int cbOldLines;

		// Token: 0x0400018E RID: 398
		internal int cbLines;

		// Token: 0x0400018F RID: 399
		internal short files;

		// Token: 0x04000190 RID: 400
		internal short pad1;

		// Token: 0x04000191 RID: 401
		internal uint offsets;

		// Token: 0x04000192 RID: 402
		internal int niSource;

		// Token: 0x04000193 RID: 403
		internal int niCompiler;

		// Token: 0x04000194 RID: 404
		internal string moduleName;

		// Token: 0x04000195 RID: 405
		internal string objectName;
	}
}
