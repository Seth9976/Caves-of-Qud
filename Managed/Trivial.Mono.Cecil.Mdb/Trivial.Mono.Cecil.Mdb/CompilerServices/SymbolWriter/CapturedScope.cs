using System;

namespace Trivial.Mono.CompilerServices.SymbolWriter
{
	// Token: 0x02000003 RID: 3
	public struct CapturedScope
	{
		// Token: 0x06000009 RID: 9 RVA: 0x00002257 File Offset: 0x00000457
		public CapturedScope(int scope, string captured_name)
		{
			this.Scope = scope;
			this.CapturedName = captured_name;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002267 File Offset: 0x00000467
		internal CapturedScope(MyBinaryReader reader)
		{
			this.Scope = reader.ReadLeb128();
			this.CapturedName = reader.ReadString();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002281 File Offset: 0x00000481
		internal void Write(MyBinaryWriter bw)
		{
			bw.WriteLeb128(this.Scope);
			bw.Write(this.CapturedName);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000229B File Offset: 0x0000049B
		public override string ToString()
		{
			return string.Format("[CapturedScope {0}:{1}]", this.Scope, this.CapturedName);
		}

		// Token: 0x04000004 RID: 4
		public readonly int Scope;

		// Token: 0x04000005 RID: 5
		public readonly string CapturedName;
	}
}
