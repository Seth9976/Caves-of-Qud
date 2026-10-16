using System;

namespace System.Web.Compilation
{
	/// <summary>Specifies the design-time editor of the expression builder. This class cannot be inherited.</summary>
	// Token: 0x020005FF RID: 1535
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public sealed class ExpressionEditorAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Compilation.ExpressionEditorAttribute" /> class using the specified type object.</summary>
		/// <param name="type">The type reference to associate with the <see cref="T:System.Web.UI.Design.ExpressionEditor" />.</param>
		// Token: 0x06004267 RID: 16999 RVA: 0x000AD136 File Offset: 0x000AB336
		public ExpressionEditorAttribute(Type type)
			: this((type != null) ? type.AssemblyQualifiedName : null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Compilation.ExpressionEditorAttribute" /> class using the specified type name.</summary>
		/// <param name="typeName">The name of the type to associate with the <see cref="T:System.Web.UI.Design.ExpressionEditor" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> is null (Nothing in Visual Basic).</exception>
		// Token: 0x06004268 RID: 17000 RVA: 0x000AD150 File Offset: 0x000AB350
		public ExpressionEditorAttribute(string typeName)
		{
			if (string.IsNullOrEmpty(typeName))
			{
				throw new ArgumentNullException("typeName");
			}
			this._editorTypeName = typeName;
		}

		/// <summary>Used by an expression editor to retrieve the editor type name.</summary>
		/// <returns>The name of the editor type.</returns>
		// Token: 0x17001522 RID: 5410
		// (get) Token: 0x06004269 RID: 17001 RVA: 0x000AD172 File Offset: 0x000AB372
		public string EditorTypeName
		{
			get
			{
				return this._editorTypeName;
			}
		}

		/// <summary>Indicates whether this instance of the <see cref="T:System.Web.Compilation.ExpressionEditorAttribute" /> class and a specified object are equal.</summary>
		/// <returns>true if value is not null and <see cref="P:System.Web.Compilation.ExpressionEditorAttribute.EditorTypeName" /> is equal; otherwise, false.</returns>
		/// <param name="obj">An instance of the <see cref="T:System.Web.Compilation.ExpressionEditorAttribute" /> class or a class that derives from it.</param>
		// Token: 0x0600426A RID: 17002 RVA: 0x000AD17C File Offset: 0x000AB37C
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ExpressionEditorAttribute expressionEditorAttribute = obj as ExpressionEditorAttribute;
			return expressionEditorAttribute != null && expressionEditorAttribute.EditorTypeName == this.EditorTypeName;
		}

		/// <summary>Retrieves the hash code for the value of this <see cref="T:System.Web.Compilation.ExpressionEditorAttribute" /> attribute.</summary>
		/// <returns>The hash code of the value of this <see cref="T:System.Web.Compilation.ExpressionEditorAttribute" />.</returns>
		// Token: 0x0600426B RID: 17003 RVA: 0x000AD1AC File Offset: 0x000AB3AC
		public override int GetHashCode()
		{
			return this.EditorTypeName.GetHashCode();
		}

		// Token: 0x040023A6 RID: 9126
		private string _editorTypeName;
	}
}
