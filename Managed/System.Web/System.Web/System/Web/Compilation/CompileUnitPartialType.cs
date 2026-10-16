using System;
using System.CodeDom;

namespace System.Web.Compilation
{
	// Token: 0x0200062B RID: 1579
	internal class CompileUnitPartialType
	{
		// Token: 0x1700155C RID: 5468
		// (get) Token: 0x060043B0 RID: 17328 RVA: 0x000B4E60 File Offset: 0x000B3060
		public string TypeName
		{
			get
			{
				if (this.typeName == null)
				{
					if (this.ParentNamespace == null || this.PartialType == null)
					{
						return null;
					}
					this.typeName = this.ParentNamespace.Name;
					if (string.IsNullOrEmpty(this.typeName))
					{
						this.typeName = this.PartialType.Name;
					}
					else
					{
						this.typeName = this.typeName + "." + this.PartialType.Name;
					}
				}
				return this.typeName;
			}
		}

		// Token: 0x060043B1 RID: 17329 RVA: 0x000B4EDF File Offset: 0x000B30DF
		public CompileUnitPartialType(CodeCompileUnit unit, CodeNamespace parentNamespace, CodeTypeDeclaration type)
		{
			this.Unit = unit;
			this.ParentNamespace = parentNamespace;
			this.PartialType = type;
		}

		// Token: 0x04002453 RID: 9299
		public readonly CodeCompileUnit Unit;

		// Token: 0x04002454 RID: 9300
		public readonly CodeNamespace ParentNamespace;

		// Token: 0x04002455 RID: 9301
		public readonly CodeTypeDeclaration PartialType;

		// Token: 0x04002456 RID: 9302
		private string typeName;
	}
}
