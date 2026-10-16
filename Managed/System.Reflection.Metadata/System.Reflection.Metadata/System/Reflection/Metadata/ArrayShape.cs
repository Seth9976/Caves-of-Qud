using System;
using System.Collections.Immutable;

namespace System.Reflection.Metadata
{
	/// <summary>Represents the shape of an array type.</summary>
	// Token: 0x02000079 RID: 121
	public readonly struct ArrayShape
	{
		/// <summary>Gets the number of dimensions in the array.</summary>
		/// <returns>The number of dimensions.</returns>
		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000500 RID: 1280 RVA: 0x0000C240 File Offset: 0x0000A440
		public int Rank { get; }

		/// <summary>Gets the sizes of all dimensions.</summary>
		/// <returns>An array of sizes.</returns>
		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x0000C248 File Offset: 0x0000A448
		public ImmutableArray<int> Sizes { get; }

		/// <summary>Gets the lower-bounds of all dimensions. Length may be smaller than rank, in which case the trailing dimensions have unspecified lower bounds.</summary>
		/// <returns>An array of lower-bounds.</returns>
		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000502 RID: 1282 RVA: 0x0000C250 File Offset: 0x0000A450
		public ImmutableArray<int> LowerBounds { get; }

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.Metadata.ArrayShape" /> structure.</summary>
		/// <param name="rank">The number of dimensions in the array.</param>
		/// <param name="sizes">The size of each dimension.</param>
		/// <param name="lowerBounds">The lower-bound of each dimension.</param>
		// Token: 0x06000503 RID: 1283 RVA: 0x0000C258 File Offset: 0x0000A458
		public ArrayShape(int rank, ImmutableArray<int> sizes, ImmutableArray<int> lowerBounds)
		{
			this.Rank = rank;
			this.Sizes = sizes;
			this.LowerBounds = lowerBounds;
		}
	}
}
