using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200017F RID: 383
	internal sealed class NoLocation : Location
	{
		// Token: 0x06000FED RID: 4077 RVA: 0x0002E90C File Offset: 0x0002CB0C
		private NoLocation()
		{
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000FEE RID: 4078 RVA: 0x0002E914 File Offset: 0x0002CB14
		public override LocationKind Kind
		{
			get
			{
				return LocationKind.None;
			}
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x0002E917 File Offset: 0x0002CB17
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return this == obj;
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x0002E91D File Offset: 0x0002CB1D
		public override int GetHashCode()
		{
			return 373847894;
		}

		// Token: 0x0400045C RID: 1116
		[Nullable(1)]
		public static readonly Location Singleton = new NoLocation();
	}
}
