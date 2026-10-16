using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CodeGen
{
	// Token: 0x020004D6 RID: 1238
	[NullableContext(1)]
	[Nullable(0)]
	internal class PermissionSetFileReadException : Exception
	{
		// Token: 0x060031C5 RID: 12741 RVA: 0x0008E1EB File Offset: 0x0008C3EB
		public PermissionSetFileReadException(string message, string file)
			: base(message)
		{
			this._file = file;
		}

		// Token: 0x17000E48 RID: 3656
		// (get) Token: 0x060031C6 RID: 12742 RVA: 0x0008E1FB File Offset: 0x0008C3FB
		public string FileName
		{
			get
			{
				return this._file;
			}
		}

		// Token: 0x17000E49 RID: 3657
		// (get) Token: 0x060031C7 RID: 12743 RVA: 0x0008E203 File Offset: 0x0008C403
		public string PropertyName
		{
			get
			{
				return "File";
			}
		}

		// Token: 0x04001340 RID: 4928
		private readonly string _file;
	}
}
