using System;

namespace System.Reflection.Emit
{
	// Token: 0x0200091F RID: 2335
	internal class DynamicMethodTokenGenerator : TokenGenerator
	{
		// Token: 0x06004FA3 RID: 20387 RVA: 0x000FA33F File Offset: 0x000F853F
		public DynamicMethodTokenGenerator(DynamicMethod m)
		{
			this.m = m;
		}

		// Token: 0x06004FA4 RID: 20388 RVA: 0x000FA34E File Offset: 0x000F854E
		public int GetToken(string str)
		{
			return this.m.AddRef(str);
		}

		// Token: 0x06004FA5 RID: 20389 RVA: 0x00084B95 File Offset: 0x00082D95
		public int GetToken(MethodBase method, Type[] opt_param_types)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06004FA6 RID: 20390 RVA: 0x000FA34E File Offset: 0x000F854E
		public int GetToken(MemberInfo member, bool create_open_instance)
		{
			return this.m.AddRef(member);
		}

		// Token: 0x06004FA7 RID: 20391 RVA: 0x000FA34E File Offset: 0x000F854E
		public int GetToken(SignatureHelper helper)
		{
			return this.m.AddRef(helper);
		}

		// Token: 0x0400314A RID: 12618
		private DynamicMethod m;
	}
}
