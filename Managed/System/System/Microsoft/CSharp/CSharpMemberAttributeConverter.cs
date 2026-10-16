using System;
using System.CodeDom;

namespace Microsoft.CSharp
{
	// Token: 0x02000140 RID: 320
	internal sealed class CSharpMemberAttributeConverter : CSharpModifierAttributeConverter
	{
		// Token: 0x0600087B RID: 2171 RVA: 0x0001E5F0 File Offset: 0x0001C7F0
		private CSharpMemberAttributeConverter()
		{
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x0600087C RID: 2172 RVA: 0x0001E684 File Offset: 0x0001C884
		public static CSharpMemberAttributeConverter Default { get; } = new CSharpMemberAttributeConverter();

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x0600087D RID: 2173 RVA: 0x0001E68B File Offset: 0x0001C88B
		protected override string[] Names { get; } = new string[] { "Public", "Protected", "Protected Internal", "Internal", "Private" };

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600087E RID: 2174 RVA: 0x0001E693 File Offset: 0x0001C893
		protected override object[] Values { get; } = new object[]
		{
			MemberAttributes.Public,
			MemberAttributes.Family,
			MemberAttributes.FamilyOrAssembly,
			MemberAttributes.Assembly,
			MemberAttributes.Private
		};

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x0600087F RID: 2175 RVA: 0x00018437 File Offset: 0x00016637
		protected override object DefaultValue
		{
			get
			{
				return MemberAttributes.Private;
			}
		}
	}
}
