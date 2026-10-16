using System;

namespace Trivial.CodeSecurity
{
	// Token: 0x02000006 RID: 6
	public struct CodeSecurityReportEntry
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000033 RID: 51 RVA: 0x000029E0 File Offset: 0x00000BE0
		public CodeSecurityReportEntry.ReportEntryType EntryType
		{
			get
			{
				return this.entryType;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000034 RID: 52 RVA: 0x000029E8 File Offset: 0x00000BE8
		public CodeSecurityLocation Location
		{
			get
			{
				return this.location;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000035 RID: 53 RVA: 0x000029F0 File Offset: 0x00000BF0
		public string Message
		{
			get
			{
				return this.message;
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000029F8 File Offset: 0x00000BF8
		internal CodeSecurityReportEntry(CodeSecurityReportEntry.ReportEntryType entryType, CodeSecurityLocation location, string message)
		{
			this.entryType = entryType;
			this.location = location;
			this.message = message;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002A0F File Offset: 0x00000C0F
		public override string ToString()
		{
			return this.message;
		}

		// Token: 0x04000017 RID: 23
		private CodeSecurityReportEntry.ReportEntryType entryType;

		// Token: 0x04000018 RID: 24
		private CodeSecurityLocation location;

		// Token: 0x04000019 RID: 25
		private string message;

		// Token: 0x0200001F RID: 31
		public enum ReportEntryType
		{
			// Token: 0x0400006A RID: 106
			IllegalAssembly,
			// Token: 0x0400006B RID: 107
			IllegalNamespace,
			// Token: 0x0400006C RID: 108
			IllegalType,
			// Token: 0x0400006D RID: 109
			IllegalMember,
			// Token: 0x0400006E RID: 110
			IllegalOccurence
		}
	}
}
