using System;
using System.CodeDom;

namespace Microsoft.VisualBasic
{
	// Token: 0x0200013A RID: 314
	internal sealed class VBMemberAttributeConverter : VBModifierAttributeConverter
	{
		// Token: 0x060007BA RID: 1978 RVA: 0x0001838C File Offset: 0x0001658C
		private VBMemberAttributeConverter()
		{
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060007BB RID: 1979 RVA: 0x00018420 File Offset: 0x00016620
		public static VBMemberAttributeConverter Default { get; } = new VBMemberAttributeConverter();

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060007BC RID: 1980 RVA: 0x00018427 File Offset: 0x00016627
		protected override string[] Names { get; } = new string[] { "Public", "Protected", "Protected Friend", "Friend", "Private" };

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060007BD RID: 1981 RVA: 0x0001842F File Offset: 0x0001662F
		protected override object[] Values { get; } = new object[]
		{
			MemberAttributes.Public,
			MemberAttributes.Family,
			MemberAttributes.FamilyOrAssembly,
			MemberAttributes.Assembly,
			MemberAttributes.Private
		};

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060007BE RID: 1982 RVA: 0x00018437 File Offset: 0x00016637
		protected override object DefaultValue
		{
			get
			{
				return MemberAttributes.Private;
			}
		}
	}
}
