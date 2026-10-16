using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	/// <summary>Provides methods for encoding literals.</summary>
	// Token: 0x020000F6 RID: 246
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct LiteralEncoder
	{
		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000956 RID: 2390 RVA: 0x0001A951 File Offset: 0x00018B51
		public BlobBuilder Builder { get; }

		/// <param name="builder">Creates a new instance of the <see cref="T:System.Reflection.Metadata.Ecma335.LiteralEncoder" /> class with the specified blob builder.</param>
		// Token: 0x06000957 RID: 2391 RVA: 0x0001A959 File Offset: 0x00018B59
		public LiteralEncoder(BlobBuilder builder)
		{
			this.Builder = builder;
		}

		/// <summary>Gets a vector encoder used to encode the items of a vector.</summary>
		/// <returns>A vector encoder used to encode the items of a vector.</returns>
		// Token: 0x06000958 RID: 2392 RVA: 0x0001A962 File Offset: 0x00018B62
		public VectorEncoder Vector()
		{
			return new VectorEncoder(this.Builder);
		}

		/// <summary>Returns a pair of encoders that must be used to encode the type and the items of a vector literal in the order they appear in the parameter list.</summary>
		/// <param name="arrayType">When this method returns, a custom attribute array type encoder used to encode the type of the vector.</param>
		/// <param name="vector">When this method returns, a vector encoder used to encode the items of the vector.</param>
		// Token: 0x06000959 RID: 2393 RVA: 0x0001A96F File Offset: 0x00018B6F
		public void TaggedVector(out CustomAttributeArrayTypeEncoder arrayType, out VectorEncoder vector)
		{
			arrayType = new CustomAttributeArrayTypeEncoder(this.Builder);
			vector = new VectorEncoder(this.Builder);
		}

		/// <summary>Encodes the type and the items of a vector literal using the specified delegates.</summary>
		/// <param name="arrayType">A delegate used to encode the type of the vector. Called first by this method.</param>
		/// <param name="vector">A delegate used to encode the items of the vector. Called second by this method.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="arrayType" /> or <paramref name="vector" /> is <see langword="null" />.</exception>
		// Token: 0x0600095A RID: 2394 RVA: 0x0001A994 File Offset: 0x00018B94
		public void TaggedVector(Action<CustomAttributeArrayTypeEncoder> arrayType, Action<VectorEncoder> vector)
		{
			if (arrayType == null)
			{
				Throw.ArgumentNull("arrayType");
			}
			if (vector == null)
			{
				Throw.ArgumentNull("vector");
			}
			CustomAttributeArrayTypeEncoder customAttributeArrayTypeEncoder;
			VectorEncoder vectorEncoder;
			this.TaggedVector(out customAttributeArrayTypeEncoder, out vectorEncoder);
			arrayType.Invoke(customAttributeArrayTypeEncoder);
			vector.Invoke(vectorEncoder);
		}

		/// <summary>Returns the encoder used to encode the literal value.</summary>
		/// <returns>The encoder of the literal value.</returns>
		// Token: 0x0600095B RID: 2395 RVA: 0x0001A9D3 File Offset: 0x00018BD3
		public ScalarEncoder Scalar()
		{
			return new ScalarEncoder(this.Builder);
		}

		/// <summary>Returns a pair of encoders that must be used to encode the type and value of a literal in the order they appear in the parameter list.</summary>
		/// <param name="type">When this method returns, a custom attribute element type encoder used to encode the type of the literal.</param>
		/// <param name="scalar">When this method returns, a scalar encoded used to encode the value of the literal.</param>
		// Token: 0x0600095C RID: 2396 RVA: 0x0001A9E0 File Offset: 0x00018BE0
		public void TaggedScalar(out CustomAttributeElementTypeEncoder type, out ScalarEncoder scalar)
		{
			type = new CustomAttributeElementTypeEncoder(this.Builder);
			scalar = new ScalarEncoder(this.Builder);
		}

		/// <summary>Encodes the type and the value of a literal using the specified delegates.</summary>
		/// <param name="type">A delegate used to encode the type of the literal. Called first by this method.</param>
		/// <param name="scalar">A delegate used to encode the value of the literal. Called second by this method.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> or <paramref name="scalar" /> is <see langword="null" />.</exception>
		// Token: 0x0600095D RID: 2397 RVA: 0x0001AA04 File Offset: 0x00018C04
		public void TaggedScalar(Action<CustomAttributeElementTypeEncoder> type, Action<ScalarEncoder> scalar)
		{
			if (type == null)
			{
				Throw.ArgumentNull("type");
			}
			if (scalar == null)
			{
				Throw.ArgumentNull("scalar");
			}
			CustomAttributeElementTypeEncoder customAttributeElementTypeEncoder;
			ScalarEncoder scalarEncoder;
			this.TaggedScalar(out customAttributeElementTypeEncoder, out scalarEncoder);
			type.Invoke(customAttributeElementTypeEncoder);
			scalar.Invoke(scalarEncoder);
		}
	}
}
