using System;
using System.IO;

namespace NUnit.Framework.Constraints
{
	// Token: 0x02000105 RID: 261
	public class EmptyDirectoryConstraint : Constraint
	{
		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000871 RID: 2161 RVA: 0x00013A4D File Offset: 0x00011C4D
		public override string Description
		{
			get
			{
				return "an empty directory";
			}
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x00013A54 File Offset: 0x00011C54
		public override ConstraintResult ApplyTo(object actual)
		{
			DirectoryInfo directoryInfo = actual as DirectoryInfo;
			if (directoryInfo == null)
			{
				throw new ArgumentException("The actual value must be a DirectoryInfo", "actual");
			}
			this.files = directoryInfo.GetFiles().Length;
			this.subdirs = directoryInfo.GetDirectories().Length;
			bool flag = this.files == 0 && this.subdirs == 0;
			return new ConstraintResult(this, actual, flag);
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x00011D90 File Offset: 0x0000FF90
		public EmptyDirectoryConstraint()
			: base(new object[0])
		{
		}

		// Token: 0x040001BF RID: 447
		private int files;

		// Token: 0x040001C0 RID: 448
		private int subdirs;
	}
}
