using System;
using System.IO;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Web.Instrumentation
{
	/// <summary>Provides information about the current position in the page execution cycle.</summary>
	// Token: 0x0200069A RID: 1690
	public class PageExecutionContext
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Instrumentation.PageExecutionContext" /> class.</summary>
		// Token: 0x06004789 RID: 18313 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public PageExecutionContext()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a flag that indicates whether the block contains literal content that should be deeply scanned for selection mapping purposes.</summary>
		/// <returns>true if the block contains literal content that should be deeply scanned for selection mapping purposes; otherwise, false;</returns>
		// Token: 0x1700161B RID: 5659
		// (get) Token: 0x0600478A RID: 18314 RVA: 0x000C7410 File Offset: 0x000C5610
		// (set) Token: 0x0600478B RID: 18315 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public bool IsLiteral
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			[CompilerGenerated]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the length, in characters, of the block.</summary>
		/// <returns>The length, in characters, of the block.</returns>
		// Token: 0x1700161C RID: 5660
		// (get) Token: 0x0600478C RID: 18316 RVA: 0x000C742C File Offset: 0x000C562C
		// (set) Token: 0x0600478D RID: 18317 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public int Length
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
			[CompilerGenerated]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the zero-based start position of the block from the start of the rendered document.</summary>
		/// <returns>The zero-based start position of the block from the start of the rendered document</returns>
		// Token: 0x1700161D RID: 5661
		// (get) Token: 0x0600478E RID: 18318 RVA: 0x000C7448 File Offset: 0x000C5648
		// (set) Token: 0x0600478F RID: 18319 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public int StartPosition
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
			[CompilerGenerated]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the text writer that is used to render the document. </summary>
		/// <returns>The text writer that is used to render the document.</returns>
		// Token: 0x1700161E RID: 5662
		// (get) Token: 0x06004790 RID: 18320 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004791 RID: 18321 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public TextWriter TextWriter
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			[CompilerGenerated]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the virtual path of the source file.</summary>
		/// <returns>The virtual path of the source file.</returns>
		// Token: 0x1700161F RID: 5663
		// (get) Token: 0x06004792 RID: 18322 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004793 RID: 18323 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public string VirtualPath
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			[CompilerGenerated]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}
	}
}
