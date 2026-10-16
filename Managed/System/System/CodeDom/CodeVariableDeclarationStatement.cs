using System;

namespace System.CodeDom
{
	/// <summary>Represents a variable declaration.</summary>
	// Token: 0x0200033F RID: 831
	[Serializable]
	public class CodeVariableDeclarationStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeVariableDeclarationStatement" /> class.</summary>
		// Token: 0x06001A56 RID: 6742 RVA: 0x0005F0B9 File Offset: 0x0005D2B9
		public CodeVariableDeclarationStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeVariableDeclarationStatement" /> class using the specified type and name.</summary>
		/// <param name="type">A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type of the variable. </param>
		/// <param name="name">The name of the variable. </param>
		// Token: 0x06001A57 RID: 6743 RVA: 0x00061737 File Offset: 0x0005F937
		public CodeVariableDeclarationStatement(CodeTypeReference type, string name)
		{
			this.Type = type;
			this.Name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeVariableDeclarationStatement" /> class using the specified data type name and variable name.</summary>
		/// <param name="type">The name of the data type of the variable. </param>
		/// <param name="name">The name of the variable. </param>
		// Token: 0x06001A58 RID: 6744 RVA: 0x0006174D File Offset: 0x0005F94D
		public CodeVariableDeclarationStatement(string type, string name)
		{
			this.Type = new CodeTypeReference(type);
			this.Name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeVariableDeclarationStatement" /> class using the specified data type and variable name.</summary>
		/// <param name="type">The data type for the variable. </param>
		/// <param name="name">The name of the variable. </param>
		// Token: 0x06001A59 RID: 6745 RVA: 0x00061768 File Offset: 0x0005F968
		public CodeVariableDeclarationStatement(Type type, string name)
		{
			this.Type = new CodeTypeReference(type);
			this.Name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeVariableDeclarationStatement" /> class using the specified data type, variable name, and initialization expression.</summary>
		/// <param name="type">A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the type of the variable. </param>
		/// <param name="name">The name of the variable. </param>
		/// <param name="initExpression">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the initialization expression for the variable. </param>
		// Token: 0x06001A5A RID: 6746 RVA: 0x00061783 File Offset: 0x0005F983
		public CodeVariableDeclarationStatement(CodeTypeReference type, string name, CodeExpression initExpression)
		{
			this.Type = type;
			this.Name = name;
			this.InitExpression = initExpression;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeVariableDeclarationStatement" /> class using the specified data type, variable name, and initialization expression.</summary>
		/// <param name="type">The name of the data type of the variable. </param>
		/// <param name="name">The name of the variable. </param>
		/// <param name="initExpression">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the initialization expression for the variable. </param>
		// Token: 0x06001A5B RID: 6747 RVA: 0x000617A0 File Offset: 0x0005F9A0
		public CodeVariableDeclarationStatement(string type, string name, CodeExpression initExpression)
		{
			this.Type = new CodeTypeReference(type);
			this.Name = name;
			this.InitExpression = initExpression;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeVariableDeclarationStatement" /> class using the specified data type, variable name, and initialization expression.</summary>
		/// <param name="type">The data type of the variable. </param>
		/// <param name="name">The name of the variable. </param>
		/// <param name="initExpression">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the initialization expression for the variable. </param>
		// Token: 0x06001A5C RID: 6748 RVA: 0x000617C2 File Offset: 0x0005F9C2
		public CodeVariableDeclarationStatement(Type type, string name, CodeExpression initExpression)
		{
			this.Type = new CodeTypeReference(type);
			this.Name = name;
			this.InitExpression = initExpression;
		}

		/// <summary>Gets or sets the initialization expression for the variable.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the initialization expression for the variable.</returns>
		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06001A5D RID: 6749 RVA: 0x000617E4 File Offset: 0x0005F9E4
		// (set) Token: 0x06001A5E RID: 6750 RVA: 0x000617EC File Offset: 0x0005F9EC
		public CodeExpression InitExpression { get; set; }

		/// <summary>Gets or sets the name of the variable.</summary>
		/// <returns>The name of the variable.</returns>
		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06001A5F RID: 6751 RVA: 0x000617F5 File Offset: 0x0005F9F5
		// (set) Token: 0x06001A60 RID: 6752 RVA: 0x00061806 File Offset: 0x0005FA06
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

		/// <summary>Gets or sets the data type of the variable.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type of the variable.</returns>
		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06001A61 RID: 6753 RVA: 0x00061810 File Offset: 0x0005FA10
		// (set) Token: 0x06001A62 RID: 6754 RVA: 0x0006183A File Offset: 0x0005FA3A
		public CodeTypeReference Type
		{
			get
			{
				CodeTypeReference codeTypeReference;
				if ((codeTypeReference = this._type) == null)
				{
					codeTypeReference = (this._type = new CodeTypeReference(""));
				}
				return codeTypeReference;
			}
			set
			{
				this._type = value;
			}
		}

		// Token: 0x04000E1F RID: 3615
		private CodeTypeReference _type;

		// Token: 0x04000E20 RID: 3616
		private string _name;
	}
}
