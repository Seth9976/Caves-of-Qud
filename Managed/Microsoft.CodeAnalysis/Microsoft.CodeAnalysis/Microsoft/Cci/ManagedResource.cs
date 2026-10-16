using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis;
using Roslyn.Utilities;

namespace Microsoft.Cci
{
	// Token: 0x02000094 RID: 148
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class ManagedResource
	{
		// Token: 0x060003EC RID: 1004 RVA: 0x0000DD60 File Offset: 0x0000BF60
		internal ManagedResource(string name, bool isPublic, [Nullable(new byte[] { 2, 1 })] Func<Stream> streamProvider, [Nullable(2)] IFileReference fileReference, uint offset)
		{
			this._streamProvider = streamProvider;
			this._name = name;
			this._fileReference = fileReference;
			this._offset = offset;
			this._isPublic = isPublic;
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0000DD90 File Offset: 0x0000BF90
		public void WriteData(BlobBuilder resourceWriter)
		{
			if (this._fileReference == null)
			{
				try
				{
					using (Stream stream = this._streamProvider.Invoke())
					{
						if (stream == null)
						{
							throw new InvalidOperationException(CodeAnalysisResources.ResourceStreamProviderShouldReturnNonNullStream);
						}
						int num = (int)(stream.Length - stream.Position);
						resourceWriter.WriteInt32(num);
						int num2 = resourceWriter.TryWriteBytes(stream, num);
						if (num2 != num)
						{
							throw new EndOfStreamException(string.Format(CultureInfo.CurrentUICulture, CodeAnalysisResources.ResourceStreamEndedUnexpectedly, num2, num));
						}
						resourceWriter.Align(8);
					}
				}
				catch (Exception ex)
				{
					throw new ResourceException(this._name, ex);
				}
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x0000DE48 File Offset: 0x0000C048
		[Nullable(2)]
		public IFileReference ExternalFile
		{
			[NullableContext(2)]
			get
			{
				return this._fileReference;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x0000DE50 File Offset: 0x0000C050
		public uint Offset
		{
			get
			{
				return this._offset;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x0000DE58 File Offset: 0x0000C058
		public IEnumerable<ICustomAttribute> Attributes
		{
			get
			{
				return SpecializedCollections.EmptyEnumerable<ICustomAttribute>();
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x0000DE5F File Offset: 0x0000C05F
		public bool IsPublic
		{
			get
			{
				return this._isPublic;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x0000DE67 File Offset: 0x0000C067
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x04000199 RID: 409
		[Nullable(new byte[] { 2, 1 })]
		private readonly Func<Stream> _streamProvider;

		// Token: 0x0400019A RID: 410
		[Nullable(2)]
		private readonly IFileReference _fileReference;

		// Token: 0x0400019B RID: 411
		private readonly uint _offset;

		// Token: 0x0400019C RID: 412
		private readonly string _name;

		// Token: 0x0400019D RID: 413
		private readonly bool _isPublic;
	}
}
