using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x0200009C RID: 156
	public sealed class HandleComparer : IEqualityComparer<Handle>, IComparer<Handle>, IEqualityComparer<EntityHandle>, IComparer<EntityHandle>
	{
		// Token: 0x060005B9 RID: 1465 RVA: 0x0000D0AE File Offset: 0x0000B2AE
		private HandleComparer()
		{
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x0000D0B6 File Offset: 0x0000B2B6
		[Nullable(1)]
		public static HandleComparer Default
		{
			[NullableContext(1)]
			get
			{
				return HandleComparer.s_default;
			}
		}

		/// <summary>Determines whether the specified objects are equal.</summary>
		/// <param name="x">The first object of type <paramref name="T" /> to compare.</param>
		/// <param name="y">The second object of type <paramref name="T" /> to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the specified objects are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x060005BB RID: 1467 RVA: 0x0000D0BD File Offset: 0x0000B2BD
		public bool Equals(Handle x, Handle y)
		{
			return x.Equals(y);
		}

		/// <summary>Determines whether the specified objects are equal.</summary>
		/// <param name="x">The first object of type <paramref name="T" /> to compare.</param>
		/// <param name="y">The second object of type <paramref name="T" /> to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the specified objects are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x060005BC RID: 1468 RVA: 0x0000D0C7 File Offset: 0x0000B2C7
		public bool Equals(EntityHandle x, EntityHandle y)
		{
			return x.Equals(y);
		}

		/// <summary>Returns a hash code for the specified object.</summary>
		/// <param name="obj">The <see cref="T:System.Object" /> for which a hash code is to be returned.</param>
		/// <returns>A hash code for the specified object.</returns>
		// Token: 0x060005BD RID: 1469 RVA: 0x0000D0D1 File Offset: 0x0000B2D1
		public int GetHashCode(Handle obj)
		{
			return obj.GetHashCode();
		}

		/// <summary>Returns a hash code for the specified object.</summary>
		/// <param name="obj">The <see cref="T:System.Object" /> for which a hash code is to be returned.</param>
		/// <returns>A hash code for the specified object.</returns>
		// Token: 0x060005BE RID: 1470 RVA: 0x0000D0E0 File Offset: 0x0000B2E0
		public int GetHashCode(EntityHandle obj)
		{
			return obj.GetHashCode();
		}

		/// <summary>Compares two handles.</summary>
		/// <param name="x">The first handle to compare.</param>
		/// <param name="y">The second handle to compare.</param>
		/// <returns>Zero if the two handles are equal, and a non-zero value if they are not.</returns>
		// Token: 0x060005BF RID: 1471 RVA: 0x0000D0EF File Offset: 0x0000B2EF
		public int Compare(Handle x, Handle y)
		{
			return Handle.Compare(x, y);
		}

		/// <summary>Compares two entity handles.</summary>
		/// <param name="x">The first entity handle to compare.</param>
		/// <param name="y">The second entity handle to compare.</param>
		/// <returns>Zero if the two entity handles are equal, and a non-zero value of they are not.</returns>
		// Token: 0x060005C0 RID: 1472 RVA: 0x0000D0F8 File Offset: 0x0000B2F8
		public int Compare(EntityHandle x, EntityHandle y)
		{
			return EntityHandle.Compare(x, y);
		}

		// Token: 0x0400038E RID: 910
		private static readonly HandleComparer s_default = new HandleComparer();
	}
}
