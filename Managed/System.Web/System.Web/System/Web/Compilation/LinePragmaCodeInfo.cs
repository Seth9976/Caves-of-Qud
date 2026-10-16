using System;
using Unity;

namespace System.Web.Compilation
{
	/// <summary>Contains properties for a script block being parsed.</summary>
	// Token: 0x02000655 RID: 1621
	[Serializable]
	public sealed class LinePragmaCodeInfo
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Compilation.LinePragmaCodeInfo" /> class. </summary>
		// Token: 0x06004579 RID: 17785 RVA: 0x00002050 File Offset: 0x00000250
		public LinePragmaCodeInfo()
		{
		}

		/// <summary>Gets the length of the script block.</summary>
		/// <returns>The length of the script block.</returns>
		// Token: 0x170015C3 RID: 5571
		// (get) Token: 0x0600457A RID: 17786 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		public int CodeLength
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a value indicating whether the script block is located inside &lt;% %&gt; tags.</summary>
		/// <returns>true if the script block is contained inside &lt;% %&gt; tags; otherwise, false.</returns>
		// Token: 0x170015C4 RID: 5572
		// (get) Token: 0x0600457B RID: 17787 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		public bool IsCodeNugget
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the starting column of a script block in an .aspx file.</summary>
		/// <returns>The starting column of a script block in an .aspx file.</returns>
		// Token: 0x170015C5 RID: 5573
		// (get) Token: 0x0600457C RID: 17788 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		public int StartColumn
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the starting column of a script block in the generated source file.</summary>
		/// <returns>The starting column of a script block in the generated source file.</returns>
		// Token: 0x170015C6 RID: 5574
		// (get) Token: 0x0600457D RID: 17789 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		public int StartGeneratedColumn
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the starting line of a script block in an .aspx file.</summary>
		/// <returns>The starting line of a script block in an .aspx file.</returns>
		// Token: 0x170015C7 RID: 5575
		// (get) Token: 0x0600457E RID: 17790 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		public int StartLine
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Compilation.LinePragmaCodeInfo" /> class with parameters for initializing the <see cref="P:System.Web.Compilation.LinePragmaCodeInfo.StartLine" />, <see cref="P:System.Web.Compilation.LinePragmaCodeInfo.StartColumn" />, <see cref="P:System.Web.Compilation.LinePragmaCodeInfo.StartGeneratedColumn" />, <see cref="P:System.Web.Compilation.LinePragmaCodeInfo.CodeLength" />, and <see cref="P:System.Web.Compilation.LinePragmaCodeInfo.IsCodeNugget" /> properties.</summary>
		/// <param name="startLine">The starting line of a script block in an .aspx file.</param>
		/// <param name="startColumn">The starting column of a script block in an .aspx file.</param>
		/// <param name="startGeneratedColumn">The starting column of a script block in the generated source file.</param>
		/// <param name="codeLength">The length of the script block.</param>
		/// <param name="isCodeNugget">A value indicating whether the script block is located inside &lt;% %&gt; tags.</param>
		// Token: 0x0600457F RID: 17791 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public LinePragmaCodeInfo(int startLine, int startColumn, int startGeneratedColumn, int codeLength, bool isCodeNugget)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
