using System;

namespace NUnit.Framework.Constraints
{
	// Token: 0x0200010D RID: 269
	[Obsolete("FileExistsConstraint is deprecated, please use FileOrDirectoryExistsConstraint instead.")]
	public class FileExistsConstraint : FileOrDirectoryExistsConstraint
	{
		// Token: 0x060008A5 RID: 2213 RVA: 0x00013F6E File Offset: 0x0001216E
		public FileExistsConstraint()
			: base(true)
		{
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x060008A6 RID: 2214 RVA: 0x00013F77 File Offset: 0x00012177
		public override string Description
		{
			get
			{
				return "file exists";
			}
		}
	}
}
