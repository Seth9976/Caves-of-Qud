using System;

namespace NUnit
{
	// Token: 0x02000003 RID: 3
	public class Env
	{
		// Token: 0x0400000E RID: 14
		public static readonly string NewLine = Environment.NewLine;

		// Token: 0x0400000F RID: 15
		public static string DocumentFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

		// Token: 0x04000010 RID: 16
		public static readonly string DefaultWorkDirectory = Environment.CurrentDirectory;
	}
}
