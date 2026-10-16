using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	/// <summary>Provides string comparison helpers to query strings in metadata while avoiding allocation if possible.</summary>
	// Token: 0x020000A8 RID: 168
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct MetadataStringComparer
	{
		// Token: 0x06000684 RID: 1668 RVA: 0x00010AA2 File Offset: 0x0000ECA2
		internal MetadataStringComparer(MetadataReader reader)
		{
			this._reader = reader;
		}

		/// <param name="handle" />
		/// <param name="value" />
		// Token: 0x06000685 RID: 1669 RVA: 0x00010AAB File Offset: 0x0000ECAB
		public bool Equals(StringHandle handle, string value)
		{
			return this.Equals(handle, value, false);
		}

		/// <param name="handle" />
		/// <param name="value" />
		/// <param name="ignoreCase" />
		// Token: 0x06000686 RID: 1670 RVA: 0x00010AB6 File Offset: 0x0000ECB6
		public bool Equals(StringHandle handle, string value, bool ignoreCase)
		{
			if (value == null)
			{
				Throw.ValueArgumentNull();
			}
			return this._reader.StringHeap.Equals(handle, value, this._reader.UTF8Decoder, ignoreCase);
		}

		/// <param name="handle" />
		/// <param name="value" />
		// Token: 0x06000687 RID: 1671 RVA: 0x00010ADE File Offset: 0x0000ECDE
		public bool Equals(NamespaceDefinitionHandle handle, string value)
		{
			return this.Equals(handle, value, false);
		}

		/// <param name="handle" />
		/// <param name="value" />
		/// <param name="ignoreCase" />
		// Token: 0x06000688 RID: 1672 RVA: 0x00010AEC File Offset: 0x0000ECEC
		public bool Equals(NamespaceDefinitionHandle handle, string value, bool ignoreCase)
		{
			if (value == null)
			{
				Throw.ValueArgumentNull();
			}
			if (handle.HasFullName)
			{
				return this._reader.StringHeap.Equals(handle.GetFullName(), value, this._reader.UTF8Decoder, ignoreCase);
			}
			return value == this._reader.NamespaceCache.GetFullName(handle);
		}

		/// <param name="handle" />
		/// <param name="value" />
		// Token: 0x06000689 RID: 1673 RVA: 0x00010B46 File Offset: 0x0000ED46
		public bool Equals(DocumentNameBlobHandle handle, string value)
		{
			return this.Equals(handle, value, false);
		}

		/// <param name="handle" />
		/// <param name="value" />
		/// <param name="ignoreCase" />
		// Token: 0x0600068A RID: 1674 RVA: 0x00010B51 File Offset: 0x0000ED51
		public bool Equals(DocumentNameBlobHandle handle, string value, bool ignoreCase)
		{
			if (value == null)
			{
				Throw.ValueArgumentNull();
			}
			return this._reader.BlobHeap.DocumentNameEquals(handle, value, ignoreCase);
		}

		/// <param name="handle" />
		/// <param name="value" />
		// Token: 0x0600068B RID: 1675 RVA: 0x00010B6E File Offset: 0x0000ED6E
		public bool StartsWith(StringHandle handle, string value)
		{
			return this.StartsWith(handle, value, false);
		}

		/// <param name="handle" />
		/// <param name="value" />
		/// <param name="ignoreCase" />
		// Token: 0x0600068C RID: 1676 RVA: 0x00010B79 File Offset: 0x0000ED79
		public bool StartsWith(StringHandle handle, string value, bool ignoreCase)
		{
			if (value == null)
			{
				Throw.ValueArgumentNull();
			}
			return this._reader.StringHeap.StartsWith(handle, value, this._reader.UTF8Decoder, ignoreCase);
		}

		// Token: 0x0400041A RID: 1050
		private readonly MetadataReader _reader;
	}
}
