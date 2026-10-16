using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Indicates that a field should be treated as containing a fixed number of elements of the specified primitive type. This class cannot be inherited. </summary>
	// Token: 0x020007F2 RID: 2034
	[AttributeUsage(AttributeTargets.Field, Inherited = false)]
	public sealed class FixedBufferAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.FixedBufferAttribute" /> class. </summary>
		/// <param name="elementType">The type of the elements contained in the buffer.</param>
		/// <param name="length">The number of elements in the buffer.</param>
		// Token: 0x060045FF RID: 17919 RVA: 0x000E57F9 File Offset: 0x000E39F9
		public FixedBufferAttribute(Type elementType, int length)
		{
			this.ElementType = elementType;
			this.Length = length;
		}

		/// <summary>Gets the type of the elements contained in the fixed buffer. </summary>
		/// <returns>The type of the elements.</returns>
		// Token: 0x17000ABF RID: 2751
		// (get) Token: 0x06004600 RID: 17920 RVA: 0x000E580F File Offset: 0x000E3A0F
		public Type ElementType { get; }

		/// <summary>Gets the number of elements in the fixed buffer. </summary>
		/// <returns>The number of elements in the fixed buffer.</returns>
		// Token: 0x17000AC0 RID: 2752
		// (get) Token: 0x06004601 RID: 17921 RVA: 0x000E5817 File Offset: 0x000E3A17
		public int Length { get; }
	}
}
