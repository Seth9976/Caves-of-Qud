using System;

namespace System.Reflection.Emit
{
	// Token: 0x0200092C RID: 2348
	internal interface TokenGenerator
	{
		// Token: 0x0600509F RID: 20639
		int GetToken(string str);

		// Token: 0x060050A0 RID: 20640
		int GetToken(MemberInfo member, bool create_open_instance);

		// Token: 0x060050A1 RID: 20641
		int GetToken(MethodBase method, Type[] opt_param_types);

		// Token: 0x060050A2 RID: 20642
		int GetToken(SignatureHelper helper);
	}
}
