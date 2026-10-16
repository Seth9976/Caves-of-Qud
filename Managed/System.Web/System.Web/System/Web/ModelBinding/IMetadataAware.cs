using System;

namespace System.Web.ModelBinding
{
	/// <summary>Defines a method that must be implemented by classes that are metadata-aware.</summary>
	// Token: 0x02000718 RID: 1816
	public interface IMetadataAware
	{
		/// <summary>Enables metadata-aware attributes to perform required processing of metadata after the metadata is created.</summary>
		/// <param name="metadata">The metadata.</param>
		// Token: 0x06004BDA RID: 19418
		void OnMetadataCreated(ModelMetadata metadata);
	}
}
