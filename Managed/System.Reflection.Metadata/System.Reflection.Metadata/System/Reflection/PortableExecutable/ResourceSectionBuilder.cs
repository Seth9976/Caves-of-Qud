using System;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

namespace System.Reflection.PortableExecutable
{
	/// <summary>Defines the base class for a PE resource section builder. Derive from <see cref="T:System.Reflection.PortableExecutable.ResourceSectionBuilder" /> to provide serialization logic for native resources.</summary>
	// Token: 0x02000025 RID: 37
	public abstract class ResourceSectionBuilder
	{
		/// <summary>Serializes the specified resource.</summary>
		/// <param name="builder">A blob that contains the data to serialize.</param>
		/// <param name="location">The location to which to serialize <paramref name="builder" />.</param>
		// Token: 0x0600016E RID: 366
		[NullableContext(1)]
		protected internal abstract void Serialize(BlobBuilder builder, SectionLocation location);
	}
}
