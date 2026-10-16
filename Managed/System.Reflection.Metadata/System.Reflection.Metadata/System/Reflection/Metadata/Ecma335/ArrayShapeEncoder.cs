using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000102 RID: 258
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct ArrayShapeEncoder
	{
		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x060009B3 RID: 2483 RVA: 0x0001B1BF File Offset: 0x000193BF
		public BlobBuilder Builder { get; }

		/// <param name="builder" />
		// Token: 0x060009B4 RID: 2484 RVA: 0x0001B1C7 File Offset: 0x000193C7
		public ArrayShapeEncoder(BlobBuilder builder)
		{
			this.Builder = builder;
		}

		/// <summary>Encodes array shape.</summary>
		/// <param name="rank">The number of dimensions in the array (shall be 1 or more).</param>
		/// <param name="sizes">Dimension sizes. The array may be shorter than <paramref name="rank" /> but not longer.</param>
		/// <param name="lowerBounds">Dimension lower bounds, or default(<see cref="T:System.Collections.Immutable.ImmutableArray`1" />) to set all <paramref name="rank" /> lower bounds to 0.
		///  The array may be shorter than <paramref name="rank" /> but not longer.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="rank" /> is outside of range [1, 0xffff], smaller than <see langword="sizes.Length" />, or smaller than <see langword="lowerBounds.Length" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sizes" /> is <see langword="null" />.</exception>
		// Token: 0x060009B5 RID: 2485 RVA: 0x0001B1D0 File Offset: 0x000193D0
		[NullableContext(0)]
		public void Shape(int rank, ImmutableArray<int> sizes, ImmutableArray<int> lowerBounds)
		{
			if (rank - 1 > 65534)
			{
				Throw.ArgumentOutOfRange("rank");
			}
			if (sizes.IsDefault)
			{
				Throw.ArgumentNull("sizes");
			}
			this.Builder.WriteCompressedInteger(rank);
			if (sizes.Length > rank)
			{
				Throw.ArgumentOutOfRange("rank");
			}
			this.Builder.WriteCompressedInteger(sizes.Length);
			foreach (int num in sizes)
			{
				this.Builder.WriteCompressedInteger(num);
			}
			if (lowerBounds.IsDefault)
			{
				this.Builder.WriteCompressedInteger(rank);
				for (int i = 0; i < rank; i++)
				{
					this.Builder.WriteCompressedSignedInteger(0);
				}
				return;
			}
			if (lowerBounds.Length > rank)
			{
				Throw.ArgumentOutOfRange("rank");
			}
			this.Builder.WriteCompressedInteger(lowerBounds.Length);
			foreach (int num2 in lowerBounds)
			{
				this.Builder.WriteCompressedSignedInteger(num2);
			}
		}
	}
}
