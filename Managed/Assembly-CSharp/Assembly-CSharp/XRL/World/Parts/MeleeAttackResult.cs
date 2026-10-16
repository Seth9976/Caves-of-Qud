using System;
using System.Runtime.InteropServices;

namespace XRL.World.Parts
{
	// Token: 0x02000E59 RID: 3673
	[StructLayout(LayoutKind.Auto)]
	public struct MeleeAttackResult
	{
		// Token: 0x060094AC RID: 38060 RVA: 0x0035BC79 File Offset: 0x00359E79
		public static explicit operator bool(MeleeAttackResult Result)
		{
			return Result.Attacks > 0;
		}

		// Token: 0x060094AD RID: 38061 RVA: 0x0035BC84 File Offset: 0x00359E84
		public static MeleeAttackResult operator +(MeleeAttackResult First, MeleeAttackResult Second)
		{
			First.Damage += Second.Damage;
			First.Attacks += Second.Attacks;
			First.Penetrations += Second.Penetrations;
			First.Criticals += Second.Criticals;
			First.Hits += Second.Hits;
			return First;
		}

		// Token: 0x04003D78 RID: 15736
		public int Damage;

		// Token: 0x04003D79 RID: 15737
		public int Attacks;

		// Token: 0x04003D7A RID: 15738
		public int Penetrations;

		// Token: 0x04003D7B RID: 15739
		public int Criticals;

		// Token: 0x04003D7C RID: 15740
		public int Hits;
	}
}
