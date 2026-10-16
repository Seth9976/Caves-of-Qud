using System;
using System.CodeDom.Compiler;

namespace System.Web.Compilation
{
	/// <summary>Receives status information about a build from the <see cref="T:System.Web.Compilation.ClientBuildManager" /> object.</summary>
	// Token: 0x02000644 RID: 1604
	public class ClientBuildManagerCallback : MarshalByRefObject
	{
		/// <summary>Reports compilation errors and warnings that occur during an application build.</summary>
		/// <param name="error">The error or warning encountered during compilation. </param>
		// Token: 0x0600450B RID: 17675 RVA: 0x00003946 File Offset: 0x00001B46
		public virtual void ReportCompilerError(CompilerError error)
		{
		}

		/// <summary>Reports parsing errors and warnings that occur during an application build.</summary>
		/// <param name="error">The error or warning encountered during parsing.</param>
		// Token: 0x0600450C RID: 17676 RVA: 0x00003946 File Offset: 0x00001B46
		public virtual void ReportParseError(ParserError error)
		{
		}

		/// <summary>Reports the progress of an application build.</summary>
		/// <param name="message">A <see cref="T:System.String" /> containing the current status of the build.</param>
		// Token: 0x0600450D RID: 17677 RVA: 0x00003946 File Offset: 0x00001B46
		public virtual void ReportProgress(string message)
		{
		}
	}
}
