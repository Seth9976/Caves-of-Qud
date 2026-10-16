using System;
using System.Collections.Immutable;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001A8 RID: 424
	[NonCopyable]
	internal struct MetadataTypeName
	{
		// Token: 0x060010F8 RID: 4344 RVA: 0x000333B4 File Offset: 0x000315B4
		public static MetadataTypeName FromFullName(string fullName, bool useCLSCompliantNameArityEncoding = false, int forcedArity = -1)
		{
			MetadataTypeName metadataTypeName;
			metadataTypeName._fullName = fullName;
			metadataTypeName._namespaceName = null;
			metadataTypeName._typeName = null;
			metadataTypeName._unmangledTypeName = null;
			metadataTypeName._inferredArity = -1;
			metadataTypeName._useCLSCompliantNameArityEncoding = useCLSCompliantNameArityEncoding;
			metadataTypeName._forcedArity = (short)forcedArity;
			metadataTypeName._namespaceSegments = default(ImmutableArray<string>);
			return metadataTypeName;
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x00033408 File Offset: 0x00031608
		public static MetadataTypeName FromNamespaceAndTypeName(string namespaceName, string typeName, bool useCLSCompliantNameArityEncoding = false, int forcedArity = -1)
		{
			MetadataTypeName metadataTypeName;
			metadataTypeName._fullName = null;
			metadataTypeName._namespaceName = namespaceName;
			metadataTypeName._typeName = typeName;
			metadataTypeName._unmangledTypeName = null;
			metadataTypeName._inferredArity = -1;
			metadataTypeName._useCLSCompliantNameArityEncoding = useCLSCompliantNameArityEncoding;
			metadataTypeName._forcedArity = (short)forcedArity;
			metadataTypeName._namespaceSegments = default(ImmutableArray<string>);
			return metadataTypeName;
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x0003345C File Offset: 0x0003165C
		public static MetadataTypeName FromTypeName(string typeName, bool useCLSCompliantNameArityEncoding = false, int forcedArity = -1)
		{
			MetadataTypeName metadataTypeName;
			metadataTypeName._fullName = typeName;
			metadataTypeName._namespaceName = string.Empty;
			metadataTypeName._typeName = typeName;
			metadataTypeName._unmangledTypeName = null;
			metadataTypeName._inferredArity = -1;
			metadataTypeName._useCLSCompliantNameArityEncoding = useCLSCompliantNameArityEncoding;
			metadataTypeName._forcedArity = (short)forcedArity;
			metadataTypeName._namespaceSegments = ImmutableArray<string>.Empty;
			return metadataTypeName;
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x060010FB RID: 4347 RVA: 0x000334B3 File Offset: 0x000316B3
		public string FullName
		{
			get
			{
				if (this._fullName == null)
				{
					this._fullName = MetadataHelpers.BuildQualifiedName(this._namespaceName, this._typeName);
				}
				return this._fullName;
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x060010FC RID: 4348 RVA: 0x000334DA File Offset: 0x000316DA
		public string NamespaceName
		{
			get
			{
				if (this._namespaceName == null)
				{
					this._typeName = MetadataHelpers.SplitQualifiedName(this._fullName, out this._namespaceName);
				}
				return this._namespaceName;
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x060010FD RID: 4349 RVA: 0x00033501 File Offset: 0x00031701
		public string TypeName
		{
			get
			{
				if (this._typeName == null)
				{
					this._typeName = MetadataHelpers.SplitQualifiedName(this._fullName, out this._namespaceName);
				}
				return this._typeName;
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x060010FE RID: 4350 RVA: 0x00033528 File Offset: 0x00031728
		public string UnmangledTypeName
		{
			get
			{
				if (this._unmangledTypeName == null)
				{
					this._unmangledTypeName = MetadataHelpers.InferTypeArityAndUnmangleMetadataName(this.TypeName, out this._inferredArity);
				}
				return this._unmangledTypeName;
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x060010FF RID: 4351 RVA: 0x0003354F File Offset: 0x0003174F
		public int InferredArity
		{
			get
			{
				if (this._inferredArity == -1)
				{
					this._unmangledTypeName = MetadataHelpers.InferTypeArityAndUnmangleMetadataName(this.TypeName, out this._inferredArity);
				}
				return (int)this._inferredArity;
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06001100 RID: 4352 RVA: 0x00033577 File Offset: 0x00031777
		public bool IsMangled
		{
			get
			{
				return this.InferredArity > 0;
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06001101 RID: 4353 RVA: 0x00033582 File Offset: 0x00031782
		public readonly bool UseCLSCompliantNameArityEncoding
		{
			get
			{
				return this._useCLSCompliantNameArityEncoding;
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06001102 RID: 4354 RVA: 0x0003358A File Offset: 0x0003178A
		public readonly int ForcedArity
		{
			get
			{
				return (int)this._forcedArity;
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06001103 RID: 4355 RVA: 0x00033592 File Offset: 0x00031792
		public ImmutableArray<string> NamespaceSegments
		{
			get
			{
				if (this._namespaceSegments.IsDefault)
				{
					this._namespaceSegments = MetadataHelpers.SplitQualifiedName(this.NamespaceName);
				}
				return this._namespaceSegments;
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06001104 RID: 4356 RVA: 0x000335B8 File Offset: 0x000317B8
		public readonly bool IsNull
		{
			get
			{
				return this._typeName == null && this._fullName == null;
			}
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x000335D0 File Offset: 0x000317D0
		public override string ToString()
		{
			if (this.IsNull)
			{
				return "{Null}";
			}
			return string.Format("{{{0},{1},{2},{3}}}", new object[]
			{
				this.NamespaceName,
				this.TypeName,
				this.UseCLSCompliantNameArityEncoding.ToString(),
				this._forcedArity.ToString()
			});
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x0003362C File Offset: 0x0003182C
		public readonly MetadataTypeName.Key ToKey()
		{
			return new MetadataTypeName.Key(in this);
		}

		// Token: 0x04000539 RID: 1337
		private string _fullName;

		// Token: 0x0400053A RID: 1338
		private string _namespaceName;

		// Token: 0x0400053B RID: 1339
		private string _typeName;

		// Token: 0x0400053C RID: 1340
		private string _unmangledTypeName;

		// Token: 0x0400053D RID: 1341
		private short _inferredArity;

		// Token: 0x0400053E RID: 1342
		private short _forcedArity;

		// Token: 0x0400053F RID: 1343
		private bool _useCLSCompliantNameArityEncoding;

		// Token: 0x04000540 RID: 1344
		private ImmutableArray<string> _namespaceSegments;

		// Token: 0x020005B7 RID: 1463
		public struct Key : IEquatable<MetadataTypeName.Key>
		{
			// Token: 0x06003617 RID: 13847 RVA: 0x000989F0 File Offset: 0x00096BF0
			internal Key(in MetadataTypeName mdTypeName)
			{
				if (mdTypeName.IsNull)
				{
					this._namespaceOrFullyQualifiedName = null;
					this._typeName = null;
					this._useCLSCompliantNameArityEncoding = 0;
					this._forcedArity = 0;
					return;
				}
				if (mdTypeName._fullName != null)
				{
					this._namespaceOrFullyQualifiedName = mdTypeName._fullName;
					this._typeName = null;
				}
				else
				{
					this._namespaceOrFullyQualifiedName = mdTypeName._namespaceName;
					this._typeName = mdTypeName._typeName;
				}
				this._useCLSCompliantNameArityEncoding = (mdTypeName.UseCLSCompliantNameArityEncoding ? 1 : 0);
				this._forcedArity = mdTypeName._forcedArity;
			}

			// Token: 0x17000F4C RID: 3916
			// (get) Token: 0x06003618 RID: 13848 RVA: 0x00098A75 File Offset: 0x00096C75
			private bool HasFullyQualifiedName
			{
				get
				{
					return this._typeName == null;
				}
			}

			// Token: 0x06003619 RID: 13849 RVA: 0x00098A80 File Offset: 0x00096C80
			public bool Equals(MetadataTypeName.Key other)
			{
				return this._useCLSCompliantNameArityEncoding == other._useCLSCompliantNameArityEncoding && this._forcedArity == other._forcedArity && this.EqualNames(ref other);
			}

			// Token: 0x0600361A RID: 13850 RVA: 0x00098AA8 File Offset: 0x00096CA8
			private bool EqualNames(ref MetadataTypeName.Key other)
			{
				if (this._typeName == other._typeName)
				{
					return this._namespaceOrFullyQualifiedName == other._namespaceOrFullyQualifiedName;
				}
				if (this.HasFullyQualifiedName)
				{
					return MetadataHelpers.SplitNameEqualsFullyQualifiedName(other._namespaceOrFullyQualifiedName, other._typeName, this._namespaceOrFullyQualifiedName);
				}
				return other.HasFullyQualifiedName && MetadataHelpers.SplitNameEqualsFullyQualifiedName(this._namespaceOrFullyQualifiedName, this._typeName, other._namespaceOrFullyQualifiedName);
			}

			// Token: 0x0600361B RID: 13851 RVA: 0x00098B1B File Offset: 0x00096D1B
			public override bool Equals(object obj)
			{
				return obj is MetadataTypeName.Key && this.Equals((MetadataTypeName.Key)obj);
			}

			// Token: 0x0600361C RID: 13852 RVA: 0x00098B33 File Offset: 0x00096D33
			public override int GetHashCode()
			{
				return Hash.Combine(this.GetHashCodeName(), Hash.Combine(this._useCLSCompliantNameArityEncoding > 0, (int)this._forcedArity));
			}

			// Token: 0x0600361D RID: 13853 RVA: 0x00098B54 File Offset: 0x00096D54
			private int GetHashCodeName()
			{
				int num = Hash.GetFNVHashCode(this._namespaceOrFullyQualifiedName);
				if (!this.HasFullyQualifiedName)
				{
					num = Hash.CombineFNVHash(num, '.');
					num = Hash.CombineFNVHash(num, this._typeName);
				}
				return num;
			}

			// Token: 0x04001675 RID: 5749
			private readonly string _namespaceOrFullyQualifiedName;

			// Token: 0x04001676 RID: 5750
			private readonly string _typeName;

			// Token: 0x04001677 RID: 5751
			private readonly byte _useCLSCompliantNameArityEncoding;

			// Token: 0x04001678 RID: 5752
			private readonly short _forcedArity;
		}
	}
}
