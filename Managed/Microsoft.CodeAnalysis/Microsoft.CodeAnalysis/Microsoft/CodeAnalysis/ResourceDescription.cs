using System;
using System.Collections.Immutable;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.Emit;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001CF RID: 463
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ResourceDescription : IFileReference
	{
		// Token: 0x06001373 RID: 4979 RVA: 0x0003CC27 File Offset: 0x0003AE27
		public ResourceDescription(string resourceName, Func<Stream> dataProvider, bool isPublic)
			: this(resourceName, null, dataProvider, isPublic, true, true)
		{
		}

		// Token: 0x06001374 RID: 4980 RVA: 0x0003CC35 File Offset: 0x0003AE35
		public ResourceDescription(string resourceName, [Nullable(2)] string fileName, Func<Stream> dataProvider, bool isPublic)
			: this(resourceName, fileName, dataProvider, isPublic, false, true)
		{
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x0003CC44 File Offset: 0x0003AE44
		internal ResourceDescription(string resourceName, [Nullable(2)] string fileName, Func<Stream> dataProvider, bool isPublic, bool isEmbedded, bool checkArgs)
		{
			if (checkArgs)
			{
				if (dataProvider == null)
				{
					throw new ArgumentNullException("dataProvider");
				}
				if (resourceName == null)
				{
					throw new ArgumentNullException("resourceName");
				}
				if (!MetadataHelpers.IsValidMetadataIdentifier(resourceName))
				{
					throw new ArgumentException(CodeAnalysisResources.EmptyOrInvalidResourceName, "resourceName");
				}
				if (!isEmbedded)
				{
					if (fileName == null)
					{
						throw new ArgumentNullException("fileName");
					}
					if (!MetadataHelpers.IsValidMetadataFileName(fileName))
					{
						throw new ArgumentException(CodeAnalysisResources.EmptyOrInvalidFileName, "fileName");
					}
				}
			}
			this.ResourceName = resourceName;
			this.DataProvider = dataProvider;
			this.FileName = (isEmbedded ? null : fileName);
			this.IsPublic = isPublic;
			this._hashes = new ResourceDescription.ResourceHashProvider(this);
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06001376 RID: 4982 RVA: 0x0003CCE9 File Offset: 0x0003AEE9
		internal bool IsEmbedded
		{
			get
			{
				return this.FileName == null;
			}
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x0003CCF4 File Offset: 0x0003AEF4
		internal ManagedResource ToManagedResource(CommonPEModuleBuilder moduleBeingBuilt)
		{
			return new ManagedResource(this.ResourceName, this.IsPublic, this.IsEmbedded ? this.DataProvider : null, this.IsEmbedded ? null : this, 0U);
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x0003CD25 File Offset: 0x0003AF25
		[NullableContext(0)]
		ImmutableArray<byte> IFileReference.GetHashValue(AssemblyHashAlgorithm algorithmId)
		{
			return this._hashes.GetHash(algorithmId);
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06001379 RID: 4985 RVA: 0x0003CD33 File Offset: 0x0003AF33
		[Nullable(2)]
		string IFileReference.FileName
		{
			[NullableContext(2)]
			get
			{
				return this.FileName;
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x0600137A RID: 4986 RVA: 0x0003CD3B File Offset: 0x0003AF3B
		bool IFileReference.HasMetadata
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040005D0 RID: 1488
		internal readonly string ResourceName;

		// Token: 0x040005D1 RID: 1489
		[Nullable(2)]
		internal readonly string FileName;

		// Token: 0x040005D2 RID: 1490
		internal readonly bool IsPublic;

		// Token: 0x040005D3 RID: 1491
		internal readonly Func<Stream> DataProvider;

		// Token: 0x040005D4 RID: 1492
		private readonly CryptographicHashProvider _hashes;

		// Token: 0x020005DD RID: 1501
		[NullableContext(0)]
		private sealed class ResourceHashProvider : CryptographicHashProvider
		{
			// Token: 0x060036C3 RID: 14019 RVA: 0x00099E27 File Offset: 0x00098027
			[NullableContext(1)]
			public ResourceHashProvider(ResourceDescription resource)
			{
				this._resource = resource;
			}

			// Token: 0x060036C4 RID: 14020 RVA: 0x00099E38 File Offset: 0x00098038
			internal override ImmutableArray<byte> ComputeHash([Nullable(1)] HashAlgorithm algorithm)
			{
				ImmutableArray<byte> immutableArray;
				try
				{
					using (Stream stream = this._resource.DataProvider.Invoke())
					{
						if (stream == null)
						{
							throw new InvalidOperationException(CodeAnalysisResources.ResourceDataProviderShouldReturnNonNullStream);
						}
						immutableArray = ImmutableArray.CreateRange<byte>(algorithm.ComputeHash(stream));
					}
				}
				catch (Exception ex)
				{
					throw new ResourceException(this._resource.FileName, ex);
				}
				return immutableArray;
			}

			// Token: 0x040016CF RID: 5839
			[Nullable(1)]
			private readonly ResourceDescription _resource;
		}
	}
}
