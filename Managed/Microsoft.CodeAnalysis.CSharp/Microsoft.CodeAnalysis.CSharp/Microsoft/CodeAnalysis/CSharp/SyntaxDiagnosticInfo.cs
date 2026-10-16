using System;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000143 RID: 323
	internal class SyntaxDiagnosticInfo : DiagnosticInfo
	{
		// Token: 0x0600198B RID: 6539 RVA: 0x00088021 File Offset: 0x00086221
		static SyntaxDiagnosticInfo()
		{
			ObjectBinder.RegisterTypeReader(typeof(SyntaxDiagnosticInfo), (ObjectReader r) => new SyntaxDiagnosticInfo(r));
		}

		// Token: 0x0600198C RID: 6540 RVA: 0x00088042 File Offset: 0x00086242
		internal SyntaxDiagnosticInfo(int offset, int width, ErrorCode code, params object[] args)
			: base(Microsoft.CodeAnalysis.CSharp.MessageProvider.Instance, (int)code, args)
		{
			this.Offset = offset;
			this.Width = width;
		}

		// Token: 0x0600198D RID: 6541 RVA: 0x00088060 File Offset: 0x00086260
		internal SyntaxDiagnosticInfo(int offset, int width, ErrorCode code)
			: this(offset, width, code, Array.Empty<object>())
		{
		}

		// Token: 0x0600198E RID: 6542 RVA: 0x00088070 File Offset: 0x00086270
		internal SyntaxDiagnosticInfo(ErrorCode code, params object[] args)
			: this(0, 0, code, args)
		{
		}

		// Token: 0x0600198F RID: 6543 RVA: 0x0008807C File Offset: 0x0008627C
		internal SyntaxDiagnosticInfo(ErrorCode code)
			: this(0, 0, code)
		{
		}

		// Token: 0x06001990 RID: 6544 RVA: 0x00088087 File Offset: 0x00086287
		public SyntaxDiagnosticInfo WithOffset(int offset)
		{
			return new SyntaxDiagnosticInfo(offset, this.Width, (ErrorCode)base.Code, base.Arguments);
		}

		// Token: 0x06001991 RID: 6545 RVA: 0x000880A1 File Offset: 0x000862A1
		protected override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteInt32(this.Offset);
			writer.WriteInt32(this.Width);
		}

		// Token: 0x06001992 RID: 6546 RVA: 0x000880C2 File Offset: 0x000862C2
		protected SyntaxDiagnosticInfo(ObjectReader reader)
			: base(reader)
		{
			this.Offset = reader.ReadInt32();
			this.Width = reader.ReadInt32();
		}

		// Token: 0x04000DD0 RID: 3536
		internal readonly int Offset;

		// Token: 0x04000DD1 RID: 3537
		internal readonly int Width;
	}
}
