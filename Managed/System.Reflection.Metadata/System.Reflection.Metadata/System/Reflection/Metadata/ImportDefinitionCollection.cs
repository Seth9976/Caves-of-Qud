using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Internal;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x020000C7 RID: 199
	public readonly struct ImportDefinitionCollection : IEnumerable<ImportDefinition>, IEnumerable
	{
		// Token: 0x0600077B RID: 1915 RVA: 0x0001216F File Offset: 0x0001036F
		internal ImportDefinitionCollection(MemoryBlock block)
		{
			this._block = block;
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x00012178 File Offset: 0x00010378
		public ImportDefinitionCollection.Enumerator GetEnumerator()
		{
			return new ImportDefinitionCollection.Enumerator(this._block);
		}

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		// Token: 0x0600077D RID: 1917 RVA: 0x00012185 File Offset: 0x00010385
		IEnumerator<ImportDefinition> IEnumerable<ImportDefinition>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
		// Token: 0x0600077E RID: 1918 RVA: 0x00012192 File Offset: 0x00010392
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0400047C RID: 1148
		private readonly MemoryBlock _block;

		// Token: 0x020001B6 RID: 438
		public struct Enumerator : IEnumerator<ImportDefinition>, IEnumerator, IDisposable
		{
			// Token: 0x06000CEF RID: 3311 RVA: 0x00025E9B File Offset: 0x0002409B
			internal Enumerator(MemoryBlock block)
			{
				this._reader = new BlobReader(block);
				this._current = default(ImportDefinition);
			}

			/// <summary>Advances the enumerator to the next element of the collection.</summary>
			/// <exception cref="T:System.BadImageFormatException">Invalid blob format.</exception>
			/// <returns>
			///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.</returns>
			// Token: 0x06000CF0 RID: 3312 RVA: 0x00025EB8 File Offset: 0x000240B8
			public bool MoveNext()
			{
				if (this._reader.RemainingBytes == 0)
				{
					return false;
				}
				ImportDefinitionKind importDefinitionKind = (ImportDefinitionKind)this._reader.ReadByte();
				switch (importDefinitionKind)
				{
				case ImportDefinitionKind.ImportNamespace:
					this._current = new ImportDefinition(importDefinitionKind, default(BlobHandle), default(AssemblyReferenceHandle), MetadataTokens.BlobHandle(this._reader.ReadCompressedInteger()));
					break;
				case ImportDefinitionKind.ImportAssemblyNamespace:
					this._current = new ImportDefinition(importDefinitionKind, default(BlobHandle), MetadataTokens.AssemblyReferenceHandle(this._reader.ReadCompressedInteger()), MetadataTokens.BlobHandle(this._reader.ReadCompressedInteger()));
					break;
				case ImportDefinitionKind.ImportType:
					this._current = new ImportDefinition(importDefinitionKind, default(BlobHandle), default(AssemblyReferenceHandle), this._reader.ReadTypeHandle());
					break;
				case ImportDefinitionKind.ImportXmlNamespace:
				case ImportDefinitionKind.AliasNamespace:
					this._current = new ImportDefinition(importDefinitionKind, MetadataTokens.BlobHandle(this._reader.ReadCompressedInteger()), default(AssemblyReferenceHandle), MetadataTokens.BlobHandle(this._reader.ReadCompressedInteger()));
					break;
				case ImportDefinitionKind.ImportAssemblyReferenceAlias:
					this._current = new ImportDefinition(importDefinitionKind, MetadataTokens.BlobHandle(this._reader.ReadCompressedInteger()), default(AssemblyReferenceHandle), default(Handle));
					break;
				case ImportDefinitionKind.AliasAssemblyReference:
					this._current = new ImportDefinition(importDefinitionKind, MetadataTokens.BlobHandle(this._reader.ReadCompressedInteger()), MetadataTokens.AssemblyReferenceHandle(this._reader.ReadCompressedInteger()), default(Handle));
					break;
				case ImportDefinitionKind.AliasAssemblyNamespace:
					this._current = new ImportDefinition(importDefinitionKind, MetadataTokens.BlobHandle(this._reader.ReadCompressedInteger()), MetadataTokens.AssemblyReferenceHandle(this._reader.ReadCompressedInteger()), MetadataTokens.BlobHandle(this._reader.ReadCompressedInteger()));
					break;
				case ImportDefinitionKind.AliasType:
					this._current = new ImportDefinition(importDefinitionKind, MetadataTokens.BlobHandle(this._reader.ReadCompressedInteger()), default(AssemblyReferenceHandle), this._reader.ReadTypeHandle());
					break;
				default:
					throw new BadImageFormatException(SR.Format(SR.InvalidImportDefinitionKind, importDefinitionKind));
				}
				return true;
			}

			/// <summary>Gets the element in the collection at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator.</returns>
			// Token: 0x1700034B RID: 843
			// (get) Token: 0x06000CF1 RID: 3313 RVA: 0x000260F8 File Offset: 0x000242F8
			public ImportDefinition Current
			{
				get
				{
					return this._current;
				}
			}

			/// <summary>Gets the element in the collection at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator.</returns>
			// Token: 0x1700034C RID: 844
			// (get) Token: 0x06000CF2 RID: 3314 RVA: 0x00026100 File Offset: 0x00024300
			[Nullable(1)]
			object IEnumerator.Current
			{
				get
				{
					return this._current;
				}
			}

			/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
			// Token: 0x06000CF3 RID: 3315 RVA: 0x0002610D File Offset: 0x0002430D
			public void Reset()
			{
				this._reader.Reset();
				this._current = default(ImportDefinition);
			}

			/// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
			// Token: 0x06000CF4 RID: 3316 RVA: 0x00026126 File Offset: 0x00024326
			void IDisposable.Dispose()
			{
			}

			// Token: 0x04000A91 RID: 2705
			private BlobReader _reader;

			// Token: 0x04000A92 RID: 2706
			private ImportDefinition _current;
		}
	}
}
