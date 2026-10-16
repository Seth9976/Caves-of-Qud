using System;
using System.CodeDom;
using System.Web.UI;

namespace System.Web.Compilation
{
	// Token: 0x02000658 RID: 1624
	internal class MasterPageCompiler : UserControlCompiler
	{
		// Token: 0x06004593 RID: 17811 RVA: 0x000BC585 File Offset: 0x000BA785
		public MasterPageCompiler(MasterPageParser parser)
			: base(parser)
		{
			this.parser = parser;
		}

		// Token: 0x06004594 RID: 17812 RVA: 0x000BC598 File Offset: 0x000BA798
		protected internal override void CreateMethods()
		{
			base.CreateMethods();
			Type masterType = this.parser.MasterType;
			if (masterType != null)
			{
				CodeMemberProperty codeMemberProperty = new CodeMemberProperty();
				codeMemberProperty.Name = "Master";
				codeMemberProperty.Type = new CodeTypeReference(this.parser.MasterType);
				codeMemberProperty.Attributes = (MemberAttributes)24592;
				CodeExpression codeExpression = new CodePropertyReferenceExpression(new CodeBaseReferenceExpression(), "Master");
				codeExpression = new CodeCastExpression(this.parser.MasterType, codeExpression);
				codeMemberProperty.GetStatements.Add(new CodeMethodReturnStatement(codeExpression));
				this.mainClass.Members.Add(codeMemberProperty);
				base.AddReferencedAssembly(masterType.Assembly);
			}
		}

		// Token: 0x04002500 RID: 9472
		private MasterPageParser parser;
	}
}
