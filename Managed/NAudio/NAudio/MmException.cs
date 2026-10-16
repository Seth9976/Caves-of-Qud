using System;

namespace NAudio
{
	// Token: 0x02000174 RID: 372
	public class MmException : Exception
	{
		// Token: 0x06000787 RID: 1927 RVA: 0x00015F35 File Offset: 0x00014135
		public MmException(MmResult result, string function)
			: base(MmException.ErrorMessage(result, function))
		{
			this.result = result;
			this.function = function;
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x00015F52 File Offset: 0x00014152
		private static string ErrorMessage(MmResult result, string function)
		{
			return string.Format("{0} calling {1}", result, function);
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x00015F65 File Offset: 0x00014165
		public static void Try(MmResult result, string function)
		{
			if (result != MmResult.NoError)
			{
				throw new MmException(result, function);
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x0600078A RID: 1930 RVA: 0x00015F72 File Offset: 0x00014172
		public MmResult Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x040008C5 RID: 2245
		private MmResult result;

		// Token: 0x040008C6 RID: 2246
		private string function;
	}
}
