using System;

namespace Mono.CompilerServices.SymbolWriter
{
	// Token: 0x02000309 RID: 777
	public struct CapturedVariable
	{
		// Token: 0x06001369 RID: 4969 RVA: 0x0003EC8C File Offset: 0x0003CE8C
		public CapturedVariable(string name, string captured_name, CapturedVariable.CapturedKind kind)
		{
			this.Name = name;
			this.CapturedName = captured_name;
			this.Kind = kind;
		}

		// Token: 0x0600136A RID: 4970 RVA: 0x0003ECA3 File Offset: 0x0003CEA3
		internal CapturedVariable(MyBinaryReader reader)
		{
			this.Name = reader.ReadString();
			this.CapturedName = reader.ReadString();
			this.Kind = (CapturedVariable.CapturedKind)reader.ReadByte();
		}

		// Token: 0x0600136B RID: 4971 RVA: 0x0003ECC9 File Offset: 0x0003CEC9
		internal void Write(MyBinaryWriter bw)
		{
			bw.Write(this.Name);
			bw.Write(this.CapturedName);
			bw.Write((byte)this.Kind);
		}

		// Token: 0x0600136C RID: 4972 RVA: 0x0003ECEF File Offset: 0x0003CEEF
		public override string ToString()
		{
			return string.Format("[CapturedVariable {0}:{1}:{2}]", this.Name, this.CapturedName, this.Kind);
		}

		// Token: 0x040009FF RID: 2559
		public readonly string Name;

		// Token: 0x04000A00 RID: 2560
		public readonly string CapturedName;

		// Token: 0x04000A01 RID: 2561
		public readonly CapturedVariable.CapturedKind Kind;

		// Token: 0x0200030A RID: 778
		public enum CapturedKind : byte
		{
			// Token: 0x04000A03 RID: 2563
			Local,
			// Token: 0x04000A04 RID: 2564
			Parameter,
			// Token: 0x04000A05 RID: 2565
			This
		}
	}
}
