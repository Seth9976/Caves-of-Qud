using System;
using System.CodeDom;
using System.Web.UI;

namespace System.Web.Compilation
{
	// Token: 0x0200066D RID: 1645
	internal class UserControlCompiler : TemplateControlCompiler
	{
		// Token: 0x06004686 RID: 18054 RVA: 0x000C3D51 File Offset: 0x000C1F51
		public UserControlCompiler(UserControlParser parser)
			: base(parser)
		{
			this.parser = parser;
		}

		// Token: 0x06004687 RID: 18055 RVA: 0x000C3D61 File Offset: 0x000C1F61
		public static Type CompileUserControlType(UserControlParser parser)
		{
			return new UserControlCompiler(parser).GetCompiledType();
		}

		// Token: 0x06004688 RID: 18056 RVA: 0x000C3D6E File Offset: 0x000C1F6E
		protected override void AddClassAttributes()
		{
			if (this.parser.OutputCache)
			{
				this.AddOutputCacheAttribute();
			}
		}

		// Token: 0x06004689 RID: 18057 RVA: 0x000C3D83 File Offset: 0x000C1F83
		protected internal override void CreateMethods()
		{
			base.CreateMethods();
			base.CreateProfileProperty();
		}

		// Token: 0x0600468A RID: 18058 RVA: 0x000C3D94 File Offset: 0x000C1F94
		private void AddOutputCacheAttribute()
		{
			CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration("System.Web.UI.PartialCachingAttribute");
			CodeAttributeArgumentCollection arguments = codeAttributeDeclaration.Arguments;
			this.AddPrimitiveArgument(arguments, this.parser.OutputCacheDuration);
			this.AddPrimitiveArgument(arguments, this.parser.OutputCacheVaryByParam);
			this.AddPrimitiveArgument(arguments, this.parser.OutputCacheVaryByControls);
			this.AddPrimitiveArgument(arguments, this.parser.OutputCacheVaryByCustom);
			this.AddPrimitiveArgument(arguments, this.parser.OutputCacheSqlDependency);
			this.AddPrimitiveArgument(arguments, this.parser.OutputCacheShared);
			arguments.Add(new CodeAttributeArgument("ProviderName", new CodePrimitiveExpression(this.parser.ProviderName)));
			this.mainClass.CustomAttributes.Add(codeAttributeDeclaration);
		}

		// Token: 0x0600468B RID: 18059 RVA: 0x000C3E5C File Offset: 0x000C205C
		private void AddPrimitiveArgument(CodeAttributeArgumentCollection arguments, object obj)
		{
			arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(obj)));
		}

		// Token: 0x0600468C RID: 18060 RVA: 0x000C3E70 File Offset: 0x000C2070
		protected override void AddStatementsToInitMethodTop(ControlBuilder builder, CodeMemberMethod method)
		{
			base.AddStatementsToInitMethodTop(builder, method);
			if (this.parser.MasterPageFile != null)
			{
				CodeExpression codeExpression = new CodePropertyReferenceExpression(new CodeArgumentReferenceExpression("__ctrl"), "MasterPageFile");
				CodeExpression codeExpression2 = new CodePrimitiveExpression(this.parser.MasterPageFile);
				method.Statements.Add(base.AddLinePragma(new CodeAssignStatement(codeExpression, codeExpression2), this.parser.DirectiveLocation));
			}
		}

		// Token: 0x04002548 RID: 9544
		private UserControlParser parser;
	}
}
