using System;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x0200002F RID: 47
	[ExcludeFromDocs]
	[NativeHeader("Modules/Marshalling/MarshallingTests.h")]
	internal struct StructWithStringIntAndFloat
	{
		// Token: 0x06000136 RID: 310 RVA: 0x0000348C File Offset: 0x0000168C
		public override bool Equals(object other)
		{
			bool flag = other == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = other is StructWithStringIntAndFloat;
				if (flag3)
				{
					StructWithStringIntAndFloat structWithStringIntAndFloat = (StructWithStringIntAndFloat)other;
					flag2 = this.a.Equals(structWithStringIntAndFloat.a) && this.b == structWithStringIntAndFloat.b && this.c == structWithStringIntAndFloat.c;
				}
				else
				{
					flag2 = false;
				}
			}
			return flag2;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x000034F8 File Offset: 0x000016F8
		public override int GetHashCode()
		{
			return this.a.GetHashCode();
		}

		// Token: 0x0400001F RID: 31
		public string a;

		// Token: 0x04000020 RID: 32
		public int b;

		// Token: 0x04000021 RID: 33
		public float c;
	}
}
