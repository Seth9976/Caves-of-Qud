using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Web
{
	/// <summary>The exception that is thrown when a compiler error occurs.</summary>
	// Token: 0x02000089 RID: 137
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[Serializable]
	public sealed class HttpCompileException : HttpException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpCompileException" /> class.</summary>
		// Token: 0x06000614 RID: 1556 RVA: 0x0000955C File Offset: 0x0000775C
		public HttpCompileException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpCompileException" /> class.</summary>
		/// <param name="message">The exception message to specify when the error occurs.</param>
		// Token: 0x06000615 RID: 1557 RVA: 0x00009564 File Offset: 0x00007764
		public HttpCompileException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpCompileException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If <paramref name="innerException" /> is not null, the current exception is raised in a catch block that handles the inner exception.</param>
		// Token: 0x06000616 RID: 1558 RVA: 0x0000956D File Offset: 0x0000776D
		public HttpCompileException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpCompileException" /> class.</summary>
		/// <param name="results">A <see cref="T:System.CodeDom.Compiler.CompilerResults" /> containing compiler output and error information. </param>
		/// <param name="sourceCode">The path to the file that contains the source code being compiled when the error occurs.</param>
		// Token: 0x06000617 RID: 1559 RVA: 0x0000EEF6 File Offset: 0x0000D0F6
		public HttpCompileException(CompilerResults results, string sourceCode)
		{
			this.results = results;
			this.sourceCode = sourceCode;
		}

		/// <summary>Gets compiler output and error information for the exception.</summary>
		/// <returns>A <see cref="T:System.CodeDom.Compiler.CompilerResults" /> containing compiler output and error information.</returns>
		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000618 RID: 1560 RVA: 0x0000EF0C File Offset: 0x0000D10C
		public CompilerResults Results
		{
			[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.High)]
			get
			{
				return this.results;
			}
		}

		/// <summary>Gets a string containing the path to the file that contains the source code being compiled when the error occurs.</summary>
		/// <returns>The path of the source file being compiled when the error occurs. The default is null.</returns>
		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000619 RID: 1561 RVA: 0x0000EF14 File Offset: 0x0000D114
		public string SourceCode
		{
			[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.High)]
			get
			{
				return this.sourceCode;
			}
		}

		/// <summary>Gets a message that describes the reason for the current exception.</summary>
		/// <returns>A string that describes the first compilation error listed in the compiler results. If no compiler results were provided, the property returns the error message provided for this exception, or an empty string (""), if no error message was provided.</returns>
		// Token: 0x17000247 RID: 583
		// (get) Token: 0x0600061A RID: 1562 RVA: 0x0000EF1C File Offset: 0x0000D11C
		public override string Message
		{
			get
			{
				return base.Message;
			}
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with information about the exception.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		// Token: 0x0600061B RID: 1563 RVA: 0x0000EF24 File Offset: 0x0000D124
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			this.sourceCode = info.GetString("sourcecode");
			this.results = (CompilerResults)info.GetValue("results", typeof(CompilerResults));
		}

		// Token: 0x04000F30 RID: 3888
		private CompilerResults results;

		// Token: 0x04000F31 RID: 3889
		private string sourceCode;
	}
}
