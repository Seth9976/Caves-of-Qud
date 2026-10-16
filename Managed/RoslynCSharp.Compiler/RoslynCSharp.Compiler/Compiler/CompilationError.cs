using System;
using Microsoft.CodeAnalysis;

namespace RoslynCSharp.Compiler
{
	// Token: 0x0200000A RID: 10
	public sealed class CompilationError
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000028E5 File Offset: 0x00000AE5
		public string Code
		{
			get
			{
				return this.code;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000028ED File Offset: 0x00000AED
		public string Message
		{
			get
			{
				return this.message;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000028F5 File Offset: 0x00000AF5
		public string SourceFile
		{
			get
			{
				return this.location.SourceTree.FilePath;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002908 File Offset: 0x00000B08
		public int SourceLine
		{
			get
			{
				return this.location.GetLineSpan().StartLinePosition.Line;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002930 File Offset: 0x00000B30
		public int SourceColumn
		{
			get
			{
				return this.location.GetLineSpan().StartLinePosition.Character;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002958 File Offset: 0x00000B58
		public bool IsInfo
		{
			get
			{
				return this.severity == 1;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002963 File Offset: 0x00000B63
		public bool IsWarning
		{
			get
			{
				return this.severity == 2;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600002D RID: 45 RVA: 0x0000296E File Offset: 0x00000B6E
		public bool IsError
		{
			get
			{
				return this.severity == 3;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002979 File Offset: 0x00000B79
		public bool IsWarningAsError
		{
			get
			{
				return this.isWarningAsError;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002981 File Offset: 0x00000B81
		public bool IsSuppressed
		{
			get
			{
				return this.isSupressed;
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000298C File Offset: 0x00000B8C
		internal CompilationError(Diagnostic diagnostic)
		{
			this.diagnostic = diagnostic;
			this.code = diagnostic.Id;
			this.message = diagnostic.GetMessage(null);
			this.location = diagnostic.Location;
			this.severity = diagnostic.Severity;
			this.isWarningAsError = diagnostic.IsWarningAsError;
			this.isSupressed = diagnostic.IsSuppressed;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002A01 File Offset: 0x00000C01
		public override string ToString()
		{
			return this.diagnostic.ToString();
		}

		// Token: 0x0400000C RID: 12
		private Diagnostic diagnostic;

		// Token: 0x0400000D RID: 13
		private string code;

		// Token: 0x0400000E RID: 14
		private string message;

		// Token: 0x0400000F RID: 15
		private Location location = Location.None;

		// Token: 0x04000010 RID: 16
		private DiagnosticSeverity severity = 1;

		// Token: 0x04000011 RID: 17
		private bool isWarningAsError;

		// Token: 0x04000012 RID: 18
		private bool isSupressed;
	}
}
