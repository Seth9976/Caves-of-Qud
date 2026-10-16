using System;

namespace Mono.CompilerServices.SymbolWriter
{
	// Token: 0x0200030B RID: 779
	public struct CapturedScope
	{
		// Token: 0x0600136D RID: 4973 RVA: 0x0003ED12 File Offset: 0x0003CF12
		public CapturedScope(int scope, string captured_name)
		{
			this.Scope = scope;
			this.CapturedName = captured_name;
		}

		// Token: 0x0600136E RID: 4974 RVA: 0x0003ED22 File Offset: 0x0003CF22
		internal CapturedScope(MyBinaryReader reader)
		{
			this.Scope = reader.ReadLeb128();
			this.CapturedName = reader.ReadString();
		}

		// Token: 0x0600136F RID: 4975 RVA: 0x0003ED3C File Offset: 0x0003CF3C
		internal void Write(MyBinaryWriter bw)
		{
			bw.WriteLeb128(this.Scope);
			bw.Write(this.CapturedName);
		}

		// Token: 0x06001370 RID: 4976 RVA: 0x0003ED56 File Offset: 0x0003CF56
		public override string ToString()
		{
			return string.Format("[CapturedScope {0}:{1}]", this.Scope, this.CapturedName);
		}

		// Token: 0x04000A06 RID: 2566
		public readonly int Scope;

		// Token: 0x04000A07 RID: 2567
		public readonly string CapturedName;
	}
}
