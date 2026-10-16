using System;

namespace System.Text
{
	// Token: 0x02000002 RID: 2
	public sealed class CodePagesEncodingProvider : EncodingProvider
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		private CodePagesEncodingProvider()
		{
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		public static EncodingProvider Instance
		{
			get
			{
				return CodePagesEncodingProvider.instance;
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x0000205F File Offset: 0x0000025F
		public override Encoding GetEncoding(string name)
		{
			return null;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002062 File Offset: 0x00000262
		public override Encoding GetEncoding(int codepage)
		{
			return null;
		}

		// Token: 0x04000001 RID: 1
		private static readonly CodePagesEncodingProvider instance = new CodePagesEncodingProvider();
	}
}
