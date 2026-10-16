using System;

namespace System.CodeDom
{
	/// <summary>Represents a namespace declaration.</summary>
	// Token: 0x0200031F RID: 799
	[Serializable]
	public class CodeNamespace : CodeObject
	{
		/// <summary>An event that will be raised the first time the <see cref="P:System.CodeDom.CodeNamespace.Comments" /> collection is accessed.</summary>
		// Token: 0x14000020 RID: 32
		// (add) Token: 0x06001952 RID: 6482 RVA: 0x000603D8 File Offset: 0x0005E5D8
		// (remove) Token: 0x06001953 RID: 6483 RVA: 0x00060410 File Offset: 0x0005E610
		public event EventHandler PopulateComments;

		/// <summary>An event that will be raised the first time the <see cref="P:System.CodeDom.CodeNamespace.Imports" /> collection is accessed.</summary>
		// Token: 0x14000021 RID: 33
		// (add) Token: 0x06001954 RID: 6484 RVA: 0x00060448 File Offset: 0x0005E648
		// (remove) Token: 0x06001955 RID: 6485 RVA: 0x00060480 File Offset: 0x0005E680
		public event EventHandler PopulateImports;

		/// <summary>An event that will be raised the first time the <see cref="P:System.CodeDom.CodeNamespace.Types" /> collection is accessed.</summary>
		// Token: 0x14000022 RID: 34
		// (add) Token: 0x06001956 RID: 6486 RVA: 0x000604B8 File Offset: 0x0005E6B8
		// (remove) Token: 0x06001957 RID: 6487 RVA: 0x000604F0 File Offset: 0x0005E6F0
		public event EventHandler PopulateTypes;

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeNamespace" /> class.</summary>
		// Token: 0x06001958 RID: 6488 RVA: 0x00060525 File Offset: 0x0005E725
		public CodeNamespace()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeNamespace" /> class using the specified name.</summary>
		/// <param name="name">The name of the namespace being declared. </param>
		// Token: 0x06001959 RID: 6489 RVA: 0x0006054E File Offset: 0x0005E74E
		public CodeNamespace(string name)
		{
			this.Name = name;
		}

		/// <summary>Gets the collection of types that the namespace contains.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeDeclarationCollection" /> that indicates the types contained in the namespace.</returns>
		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x0600195A RID: 6490 RVA: 0x0006057E File Offset: 0x0005E77E
		public CodeTypeDeclarationCollection Types
		{
			get
			{
				if ((this._populated & 4) == 0)
				{
					this._populated |= 4;
					EventHandler populateTypes = this.PopulateTypes;
					if (populateTypes != null)
					{
						populateTypes(this, EventArgs.Empty);
					}
				}
				return this._classes;
			}
		}

		/// <summary>Gets the collection of namespace import directives used by the namespace.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeNamespaceImportCollection" /> that indicates the namespace import directives used by the namespace.</returns>
		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x0600195B RID: 6491 RVA: 0x000605B5 File Offset: 0x0005E7B5
		public CodeNamespaceImportCollection Imports
		{
			get
			{
				if ((this._populated & 1) == 0)
				{
					this._populated |= 1;
					EventHandler populateImports = this.PopulateImports;
					if (populateImports != null)
					{
						populateImports(this, EventArgs.Empty);
					}
				}
				return this._imports;
			}
		}

		/// <summary>Gets or sets the name of the namespace.</summary>
		/// <returns>The name of the namespace.</returns>
		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x0600195C RID: 6492 RVA: 0x000605EC File Offset: 0x0005E7EC
		// (set) Token: 0x0600195D RID: 6493 RVA: 0x000605FD File Offset: 0x0005E7FD
		public string Name
		{
			get
			{
				return this._name ?? string.Empty;
			}
			set
			{
				this._name = value;
			}
		}

		/// <summary>Gets the comments for the namespace.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeCommentStatementCollection" /> that indicates the comments for the namespace.</returns>
		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x0600195E RID: 6494 RVA: 0x00060606 File Offset: 0x0005E806
		public CodeCommentStatementCollection Comments
		{
			get
			{
				if ((this._populated & 2) == 0)
				{
					this._populated |= 2;
					EventHandler populateComments = this.PopulateComments;
					if (populateComments != null)
					{
						populateComments(this, EventArgs.Empty);
					}
				}
				return this._comments;
			}
		}

		// Token: 0x04000DD8 RID: 3544
		private string _name;

		// Token: 0x04000DD9 RID: 3545
		private readonly CodeNamespaceImportCollection _imports = new CodeNamespaceImportCollection();

		// Token: 0x04000DDA RID: 3546
		private readonly CodeCommentStatementCollection _comments = new CodeCommentStatementCollection();

		// Token: 0x04000DDB RID: 3547
		private readonly CodeTypeDeclarationCollection _classes = new CodeTypeDeclarationCollection();

		// Token: 0x04000DDC RID: 3548
		private int _populated;

		// Token: 0x04000DDD RID: 3549
		private const int ImportsCollection = 1;

		// Token: 0x04000DDE RID: 3550
		private const int CommentsCollection = 2;

		// Token: 0x04000DDF RID: 3551
		private const int TypesCollection = 4;
	}
}
