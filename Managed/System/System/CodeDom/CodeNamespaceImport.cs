using System;

namespace System.CodeDom
{
	/// <summary>Represents a namespace import directive that indicates a namespace to use.</summary>
	// Token: 0x02000321 RID: 801
	[Serializable]
	public class CodeNamespaceImport : CodeObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeNamespaceImport" /> class.</summary>
		// Token: 0x0600196C RID: 6508 RVA: 0x0005F669 File Offset: 0x0005D869
		public CodeNamespaceImport()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeNamespaceImport" /> class using the specified namespace to import.</summary>
		/// <param name="nameSpace">The name of the namespace to import. </param>
		// Token: 0x0600196D RID: 6509 RVA: 0x000606E0 File Offset: 0x0005E8E0
		public CodeNamespaceImport(string nameSpace)
		{
			this.Namespace = nameSpace;
		}

		/// <summary>Gets or sets the line and file the statement occurs on.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeLinePragma" /> that indicates the context of the statement.</returns>
		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x0600196E RID: 6510 RVA: 0x000606EF File Offset: 0x0005E8EF
		// (set) Token: 0x0600196F RID: 6511 RVA: 0x000606F7 File Offset: 0x0005E8F7
		public CodeLinePragma LinePragma { get; set; }

		/// <summary>Gets or sets the namespace to import.</summary>
		/// <returns>The name of the namespace to import.</returns>
		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06001970 RID: 6512 RVA: 0x00060700 File Offset: 0x0005E900
		// (set) Token: 0x06001971 RID: 6513 RVA: 0x00060711 File Offset: 0x0005E911
		public string Namespace
		{
			get
			{
				return this._nameSpace ?? string.Empty;
			}
			set
			{
				this._nameSpace = value;
			}
		}

		// Token: 0x04000DE3 RID: 3555
		private string _nameSpace;
	}
}
