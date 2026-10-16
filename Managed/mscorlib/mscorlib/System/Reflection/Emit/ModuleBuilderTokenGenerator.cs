using System;

namespace System.Reflection.Emit
{
	// Token: 0x02000939 RID: 2361
	internal class ModuleBuilderTokenGenerator : TokenGenerator
	{
		// Token: 0x060051E9 RID: 20969 RVA: 0x00100503 File Offset: 0x000FE703
		public ModuleBuilderTokenGenerator(ModuleBuilder mb)
		{
			this.mb = mb;
		}

		// Token: 0x060051EA RID: 20970 RVA: 0x00100512 File Offset: 0x000FE712
		public int GetToken(string str)
		{
			return this.mb.GetToken(str);
		}

		// Token: 0x060051EB RID: 20971 RVA: 0x00100520 File Offset: 0x000FE720
		public int GetToken(MemberInfo member, bool create_open_instance)
		{
			return this.mb.GetToken(member, create_open_instance);
		}

		// Token: 0x060051EC RID: 20972 RVA: 0x0010052F File Offset: 0x000FE72F
		public int GetToken(MethodBase method, Type[] opt_param_types)
		{
			return this.mb.GetToken(method, opt_param_types);
		}

		// Token: 0x060051ED RID: 20973 RVA: 0x0010053E File Offset: 0x000FE73E
		public int GetToken(SignatureHelper helper)
		{
			return this.mb.GetToken(helper);
		}

		// Token: 0x040031FE RID: 12798
		private ModuleBuilder mb;
	}
}
