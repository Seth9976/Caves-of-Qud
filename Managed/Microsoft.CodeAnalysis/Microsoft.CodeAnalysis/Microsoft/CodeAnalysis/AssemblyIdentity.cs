using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001B0 RID: 432
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
	public sealed class AssemblyIdentity : IEquatable<AssemblyIdentity>
	{
		// Token: 0x06001207 RID: 4615 RVA: 0x00037620 File Offset: 0x00035820
		private AssemblyIdentity(AssemblyIdentity other, Version version)
		{
			this._contentType = other.ContentType;
			this._name = other._name;
			this._cultureName = other._cultureName;
			this._publicKey = other._publicKey;
			this._lazyPublicKeyToken = other._lazyPublicKeyToken;
			this._isRetargetable = other._isRetargetable;
			this._version = version;
			this._lazyDisplayName = null;
			this._lazyHashCode = 0;
		}

		// Token: 0x06001208 RID: 4616 RVA: 0x00037690 File Offset: 0x00035890
		internal AssemblyIdentity WithVersion(Version version)
		{
			if (!(version == this._version))
			{
				return new AssemblyIdentity(this, version);
			}
			return this;
		}

		// Token: 0x06001209 RID: 4617 RVA: 0x000376AC File Offset: 0x000358AC
		[NullableContext(2)]
		public AssemblyIdentity(string name, Version version = null, string cultureName = null, [Nullable(0)] ImmutableArray<byte> publicKeyOrToken = default(ImmutableArray<byte>), bool hasPublicKey = false, bool isRetargetable = false, AssemblyContentType contentType = 0)
		{
			if (!AssemblyIdentity.IsValid(contentType))
			{
				throw new ArgumentOutOfRangeException("contentType", CodeAnalysisResources.InvalidContentType);
			}
			if (!AssemblyIdentity.IsValidName(name))
			{
				throw new ArgumentException(string.Format(CodeAnalysisResources.InvalidAssemblyName, name), "name");
			}
			if (!AssemblyIdentity.IsValidCultureName(cultureName))
			{
				throw new ArgumentException(string.Format(CodeAnalysisResources.InvalidCultureName, cultureName), "cultureName");
			}
			if (!AssemblyIdentity.IsValid(version))
			{
				throw new ArgumentOutOfRangeException("version");
			}
			if (hasPublicKey)
			{
				if (!MetadataHelpers.IsValidPublicKey(publicKeyOrToken))
				{
					throw new ArgumentException(CodeAnalysisResources.InvalidPublicKey, "publicKeyOrToken");
				}
			}
			else if (!publicKeyOrToken.IsDefaultOrEmpty && publicKeyOrToken.Length != 8)
			{
				throw new ArgumentException(CodeAnalysisResources.InvalidSizeOfPublicKeyToken, "publicKeyOrToken");
			}
			if (isRetargetable && contentType == 1)
			{
				throw new ArgumentException(CodeAnalysisResources.WinRTIdentityCantBeRetargetable, "isRetargetable");
			}
			this._name = name;
			this._version = version ?? AssemblyIdentity.NullVersion;
			this._cultureName = AssemblyIdentity.NormalizeCultureName(cultureName);
			this._isRetargetable = isRetargetable;
			this._contentType = contentType;
			AssemblyIdentity.InitializeKey(publicKeyOrToken, hasPublicKey, out this._publicKey, out this._lazyPublicKeyToken);
		}

		// Token: 0x0600120A RID: 4618 RVA: 0x000377C8 File Offset: 0x000359C8
		internal AssemblyIdentity(string name, Version version, [Nullable(2)] string cultureName, [Nullable(0)] ImmutableArray<byte> publicKeyOrToken, bool hasPublicKey)
		{
			this._name = name;
			this._version = version ?? AssemblyIdentity.NullVersion;
			this._cultureName = AssemblyIdentity.NormalizeCultureName(cultureName);
			this._isRetargetable = false;
			this._contentType = 0;
			AssemblyIdentity.InitializeKey(publicKeyOrToken, hasPublicKey, out this._publicKey, out this._lazyPublicKeyToken);
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x00037824 File Offset: 0x00035A24
		[NullableContext(2)]
		internal AssemblyIdentity(bool noThrow, [Nullable(1)] string name, Version version = null, string cultureName = null, [Nullable(0)] ImmutableArray<byte> publicKeyOrToken = default(ImmutableArray<byte>), bool hasPublicKey = false, bool isRetargetable = false, AssemblyContentType contentType = 0)
		{
			this._name = name;
			this._version = version ?? AssemblyIdentity.NullVersion;
			this._cultureName = AssemblyIdentity.NormalizeCultureName(cultureName);
			this._contentType = (AssemblyIdentity.IsValid(contentType) ? contentType : 0);
			this._isRetargetable = isRetargetable && this._contentType != 1;
			AssemblyIdentity.InitializeKey(publicKeyOrToken, hasPublicKey, out this._publicKey, out this._lazyPublicKeyToken);
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x0003789D File Offset: 0x00035A9D
		private static string NormalizeCultureName([Nullable(2)] string cultureName)
		{
			if (cultureName != null && !AssemblyIdentityComparer.CultureComparer.Equals(cultureName, "neutral"))
			{
				return cultureName;
			}
			return string.Empty;
		}

		// Token: 0x0600120D RID: 4621 RVA: 0x000378BB File Offset: 0x00035ABB
		[NullableContext(0)]
		private static void InitializeKey(ImmutableArray<byte> publicKeyOrToken, bool hasPublicKey, out ImmutableArray<byte> publicKey, out ImmutableArray<byte> publicKeyToken)
		{
			if (hasPublicKey)
			{
				publicKey = publicKeyOrToken;
				publicKeyToken = default(ImmutableArray<byte>);
				return;
			}
			publicKey = ImmutableArray<byte>.Empty;
			publicKeyToken = publicKeyOrToken.NullToEmpty<byte>();
		}

		// Token: 0x0600120E RID: 4622 RVA: 0x000378E6 File Offset: 0x00035AE6
		[NullableContext(2)]
		internal static bool IsValidCultureName(string name)
		{
			return name == null || name.IndexOf('\0') < 0;
		}

		// Token: 0x0600120F RID: 4623 RVA: 0x000378F7 File Offset: 0x00035AF7
		[NullableContext(2)]
		private static bool IsValidName([NotNullWhen(true)] string name)
		{
			return !string.IsNullOrEmpty(name) && name.IndexOf('\0') < 0;
		}

		// Token: 0x06001210 RID: 4624 RVA: 0x00037910 File Offset: 0x00035B10
		[NullableContext(2)]
		private static bool IsValid(Version value)
		{
			return value == null || (value.Major >= 0 && value.Minor >= 0 && value.Build >= 0 && value.Revision >= 0 && value.Major <= 65535 && value.Minor <= 65535 && value.Build <= 65535 && value.Revision <= 65535);
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x00037985 File Offset: 0x00035B85
		private static bool IsValid(AssemblyContentType value)
		{
			return value >= 0 && value <= 1;
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06001212 RID: 4626 RVA: 0x00037994 File Offset: 0x00035B94
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06001213 RID: 4627 RVA: 0x0003799C File Offset: 0x00035B9C
		public Version Version
		{
			get
			{
				return this._version;
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06001214 RID: 4628 RVA: 0x000379A4 File Offset: 0x00035BA4
		public string CultureName
		{
			get
			{
				return this._cultureName;
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06001215 RID: 4629 RVA: 0x000379AC File Offset: 0x00035BAC
		public AssemblyNameFlags Flags
		{
			get
			{
				return (this._isRetargetable ? 256 : 0) | (this.HasPublicKey ? 1 : 0);
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06001216 RID: 4630 RVA: 0x000379CB File Offset: 0x00035BCB
		public AssemblyContentType ContentType
		{
			get
			{
				return this._contentType;
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06001217 RID: 4631 RVA: 0x000379D4 File Offset: 0x00035BD4
		public bool HasPublicKey
		{
			get
			{
				return this._publicKey.Length > 0;
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06001218 RID: 4632 RVA: 0x000379F2 File Offset: 0x00035BF2
		[Nullable(0)]
		public ImmutableArray<byte> PublicKey
		{
			[NullableContext(0)]
			get
			{
				return this._publicKey;
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06001219 RID: 4633 RVA: 0x000379FC File Offset: 0x00035BFC
		[Nullable(0)]
		public ImmutableArray<byte> PublicKeyToken
		{
			[NullableContext(0)]
			get
			{
				if (this._lazyPublicKeyToken.IsDefault)
				{
					ImmutableInterlocked.InterlockedCompareExchange<byte>(ref this._lazyPublicKeyToken, AssemblyIdentity.CalculatePublicKeyToken(this._publicKey), default(ImmutableArray<byte>));
				}
				return this._lazyPublicKeyToken;
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x0600121A RID: 4634 RVA: 0x00037A3C File Offset: 0x00035C3C
		public bool IsStrongName
		{
			get
			{
				return this.HasPublicKey || this._lazyPublicKeyToken.Length > 0;
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x0600121B RID: 4635 RVA: 0x00037A56 File Offset: 0x00035C56
		public bool IsRetargetable
		{
			get
			{
				return this._isRetargetable;
			}
		}

		// Token: 0x0600121C RID: 4636 RVA: 0x00037A5E File Offset: 0x00035C5E
		internal static bool IsFullName(AssemblyIdentityParts parts)
		{
			return (parts & (AssemblyIdentityParts.Name | AssemblyIdentityParts.Version | AssemblyIdentityParts.Culture)) == (AssemblyIdentityParts.Name | AssemblyIdentityParts.Version | AssemblyIdentityParts.Culture) && (parts & AssemblyIdentityParts.PublicKeyOrToken) > (AssemblyIdentityParts)0;
		}

		// Token: 0x0600121D RID: 4637 RVA: 0x00037A74 File Offset: 0x00035C74
		[NullableContext(2)]
		public static bool operator ==(AssemblyIdentity left, AssemblyIdentity right)
		{
			return EqualityComparer<AssemblyIdentity>.Default.Equals(left, right);
		}

		// Token: 0x0600121E RID: 4638 RVA: 0x00037A82 File Offset: 0x00035C82
		[NullableContext(2)]
		public static bool operator !=(AssemblyIdentity left, AssemblyIdentity right)
		{
			return !(left == right);
		}

		// Token: 0x0600121F RID: 4639 RVA: 0x00037A90 File Offset: 0x00035C90
		[NullableContext(2)]
		public bool Equals(AssemblyIdentity obj)
		{
			if (obj != null && (this._lazyHashCode == 0 || obj._lazyHashCode == 0 || this._lazyHashCode == obj._lazyHashCode))
			{
				bool? flag = AssemblyIdentity.MemberwiseEqual(this, obj);
				bool flag2 = true;
				return (flag.GetValueOrDefault() == flag2) & (flag != null);
			}
			return false;
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x00037ADC File Offset: 0x00035CDC
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as AssemblyIdentity);
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x00037AEC File Offset: 0x00035CEC
		public override int GetHashCode()
		{
			if (this._lazyHashCode == 0)
			{
				this._lazyHashCode = Hash.Combine(AssemblyIdentityComparer.SimpleNameComparer.GetHashCode(this._name), Hash.Combine(this._version.GetHashCode(), this.GetHashCodeIgnoringNameAndVersion()));
			}
			return this._lazyHashCode;
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x00037B38 File Offset: 0x00035D38
		internal int GetHashCodeIgnoringNameAndVersion()
		{
			return Hash.Combine(this._contentType, Hash.Combine(this._isRetargetable, AssemblyIdentityComparer.CultureComparer.GetHashCode(this._cultureName)));
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x00037B60 File Offset: 0x00035D60
		[NullableContext(0)]
		internal static ImmutableArray<byte> CalculatePublicKeyToken(ImmutableArray<byte> publicKey)
		{
			ImmutableArray<byte> immutableArray = CryptographicHashProvider.ComputeSha1(publicKey);
			int num = immutableArray.Length - 1;
			ArrayBuilder<byte> instance = ArrayBuilder<byte>.GetInstance(8);
			for (int i = 0; i < 8; i++)
			{
				instance.Add(immutableArray[num - i]);
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x00037BA8 File Offset: 0x00035DA8
		internal static bool? MemberwiseEqual(AssemblyIdentity x, AssemblyIdentity y)
		{
			if (x == y)
			{
				return new bool?(true);
			}
			if (!AssemblyIdentityComparer.SimpleNameComparer.Equals(x._name, y._name))
			{
				return new bool?(false);
			}
			if (x._version.Equals(y._version) && AssemblyIdentity.EqualIgnoringNameAndVersion(x, y))
			{
				return new bool?(true);
			}
			return default(bool?);
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x00037C0B File Offset: 0x00035E0B
		internal static bool EqualIgnoringNameAndVersion(AssemblyIdentity x, AssemblyIdentity y)
		{
			return x.IsRetargetable == y.IsRetargetable && x.ContentType == y.ContentType && AssemblyIdentityComparer.CultureComparer.Equals(x.CultureName, y.CultureName) && AssemblyIdentity.KeysEqual(x, y);
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x00037C4C File Offset: 0x00035E4C
		internal static bool KeysEqual(AssemblyIdentity x, AssemblyIdentity y)
		{
			ImmutableArray<byte> lazyPublicKeyToken = x._lazyPublicKeyToken;
			ImmutableArray<byte> lazyPublicKeyToken2 = y._lazyPublicKeyToken;
			if (!lazyPublicKeyToken.IsDefault && !lazyPublicKeyToken2.IsDefault)
			{
				return lazyPublicKeyToken.SequenceEqual<byte, byte>(lazyPublicKeyToken2, null);
			}
			if (lazyPublicKeyToken.IsDefault && lazyPublicKeyToken2.IsDefault)
			{
				return x._publicKey.SequenceEqual<byte, byte>(y._publicKey, null);
			}
			if (lazyPublicKeyToken.IsDefault)
			{
				return x.PublicKeyToken.SequenceEqual<byte, byte>(lazyPublicKeyToken2, null);
			}
			return lazyPublicKeyToken.SequenceEqual<byte, byte>(y.PublicKeyToken, null);
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x00037CCB File Offset: 0x00035ECB
		public static AssemblyIdentity FromAssemblyDefinition(Assembly assembly)
		{
			if (assembly == null)
			{
				throw new ArgumentNullException("assembly");
			}
			return AssemblyIdentity.FromAssemblyDefinition(assembly.GetName());
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x00037CEC File Offset: 0x00035EEC
		internal static AssemblyIdentity FromAssemblyDefinition(AssemblyName name)
		{
			byte[] publicKey = name.GetPublicKey();
			ImmutableArray<byte> immutableArray = ((publicKey != null) ? ImmutableArray.Create<byte>(publicKey) : ImmutableArray<byte>.Empty);
			return new AssemblyIdentity(name.Name, name.Version, name.CultureName, immutableArray, immutableArray.Length > 0, (name.Flags & 256) > 0, name.ContentType);
		}

		// Token: 0x06001229 RID: 4649 RVA: 0x00037D48 File Offset: 0x00035F48
		internal static AssemblyIdentity FromAssemblyReference(AssemblyName name)
		{
			return new AssemblyIdentity(name.Name, name.Version, name.CultureName, ImmutableArray.Create<byte>(name.GetPublicKeyToken()), false, (name.Flags & 256) > 0, name.ContentType);
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x00037D82 File Offset: 0x00035F82
		public string GetDisplayName(bool fullKey = false)
		{
			if (fullKey)
			{
				return this.BuildDisplayName(true);
			}
			if (this._lazyDisplayName == null)
			{
				this._lazyDisplayName = this.BuildDisplayName(false);
			}
			return this._lazyDisplayName;
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x00037DAA File Offset: 0x00035FAA
		public override string ToString()
		{
			return this.GetDisplayName(false);
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x00037DB3 File Offset: 0x00035FB3
		[NullableContext(0)]
		[return: Nullable(1)]
		internal static string PublicKeyToString(ImmutableArray<byte> key)
		{
			if (key.IsDefaultOrEmpty)
			{
				return "";
			}
			PooledStringBuilder instance = PooledStringBuilder.GetInstance();
			StringBuilder builder = instance.Builder;
			AssemblyIdentity.AppendKey(instance, key);
			return instance.ToStringAndFree();
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x00037DE4 File Offset: 0x00035FE4
		private string BuildDisplayName(bool fullKey)
		{
			PooledStringBuilder instance = PooledStringBuilder.GetInstance();
			StringBuilder builder = instance.Builder;
			AssemblyIdentity.EscapeName(builder, this.Name);
			builder.Append(", Version=");
			builder.Append(this._version.Major);
			builder.Append(".");
			builder.Append(this._version.Minor);
			builder.Append(".");
			builder.Append(this._version.Build);
			builder.Append(".");
			builder.Append(this._version.Revision);
			builder.Append(", Culture=");
			if (this._cultureName.Length == 0)
			{
				builder.Append("neutral");
			}
			else
			{
				AssemblyIdentity.EscapeName(builder, this._cultureName);
			}
			if (fullKey && this.HasPublicKey)
			{
				builder.Append(", PublicKey=");
				AssemblyIdentity.AppendKey(builder, this._publicKey);
			}
			else
			{
				builder.Append(", PublicKeyToken=");
				if (this.PublicKeyToken.Length > 0)
				{
					AssemblyIdentity.AppendKey(builder, this.PublicKeyToken);
				}
				else
				{
					builder.Append("null");
				}
			}
			if (this.IsRetargetable)
			{
				builder.Append(", Retargetable=Yes");
			}
			AssemblyContentType contentType = this._contentType;
			if (contentType != null)
			{
				if (contentType != 1)
				{
					throw ExceptionUtilities.UnexpectedValue(this._contentType);
				}
				builder.Append(", ContentType=WindowsRuntime");
			}
			string text = builder.ToString();
			instance.Free();
			return text;
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x00037F60 File Offset: 0x00036160
		[NullableContext(0)]
		private static void AppendKey([Nullable(1)] StringBuilder sb, ImmutableArray<byte> key)
		{
			foreach (byte b in key)
			{
				sb.Append(b.ToString("x2"));
			}
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x00037F9B File Offset: 0x0003619B
		private string GetDebuggerDisplay()
		{
			return this.GetDisplayName(true);
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x00037FA4 File Offset: 0x000361A4
		public static bool TryParseDisplayName(string displayName, [Nullable(2)] [NotNullWhen(true)] out AssemblyIdentity identity)
		{
			if (displayName == null)
			{
				throw new ArgumentNullException("displayName");
			}
			AssemblyIdentityParts assemblyIdentityParts;
			return AssemblyIdentity.TryParseDisplayName(displayName, out identity, out assemblyIdentityParts);
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x00037FC8 File Offset: 0x000361C8
		public static bool TryParseDisplayName(string displayName, [Nullable(2)] [NotNullWhen(true)] out AssemblyIdentity identity, out AssemblyIdentityParts parts)
		{
			identity = null;
			parts = (AssemblyIdentityParts)0;
			if (displayName == null)
			{
				throw new ArgumentNullException("displayName");
			}
			if (displayName.IndexOf('\0') >= 0)
			{
				return false;
			}
			int i = 0;
			string text;
			if (!AssemblyIdentity.TryParseNameToken(displayName, ref i, out text))
			{
				return false;
			}
			AssemblyIdentityParts assemblyIdentityParts = AssemblyIdentityParts.Name;
			AssemblyIdentityParts assemblyIdentityParts2 = AssemblyIdentityParts.Name;
			Version version = null;
			string text2 = null;
			bool flag = false;
			AssemblyContentType assemblyContentType = 0;
			ImmutableArray<byte> immutableArray = default(ImmutableArray<byte>);
			ImmutableArray<byte> immutableArray2 = default(ImmutableArray<byte>);
			while (i < displayName.Length)
			{
				if (displayName.get_Chars(i) != ',')
				{
					return false;
				}
				i++;
				string text3;
				if (!AssemblyIdentity.TryParseNameToken(displayName, ref i, out text3))
				{
					return false;
				}
				if (i >= displayName.Length || displayName.get_Chars(i) != '=')
				{
					return false;
				}
				i++;
				string text4;
				if (!AssemblyIdentity.TryParseNameToken(displayName, ref i, out text4))
				{
					return false;
				}
				if (string.Equals(text3, "Version", 5))
				{
					if ((assemblyIdentityParts2 & AssemblyIdentityParts.Version) != (AssemblyIdentityParts)0)
					{
						return false;
					}
					assemblyIdentityParts2 |= AssemblyIdentityParts.Version;
					if (!(text4 == "*"))
					{
						ulong num;
						AssemblyIdentityParts assemblyIdentityParts3;
						if (!AssemblyIdentity.TryParseVersion(text4, out num, out assemblyIdentityParts3))
						{
							return false;
						}
						version = AssemblyIdentity.ToVersion(num);
						assemblyIdentityParts |= assemblyIdentityParts3;
					}
				}
				else if (string.Equals(text3, "Culture", 5) || string.Equals(text3, "Language", 5))
				{
					if ((assemblyIdentityParts2 & AssemblyIdentityParts.Culture) != (AssemblyIdentityParts)0)
					{
						return false;
					}
					assemblyIdentityParts2 |= AssemblyIdentityParts.Culture;
					if (!(text4 == "*"))
					{
						text2 = (string.Equals(text4, "neutral", 5) ? null : text4);
						assemblyIdentityParts |= AssemblyIdentityParts.Culture;
					}
				}
				else if (string.Equals(text3, "PublicKey", 5))
				{
					if ((assemblyIdentityParts2 & AssemblyIdentityParts.PublicKey) != (AssemblyIdentityParts)0)
					{
						return false;
					}
					assemblyIdentityParts2 |= AssemblyIdentityParts.PublicKey;
					if (!(text4 == "*"))
					{
						ImmutableArray<byte> immutableArray3;
						if (!AssemblyIdentity.TryParsePublicKey(text4, out immutableArray3))
						{
							return false;
						}
						immutableArray = immutableArray3;
						assemblyIdentityParts |= AssemblyIdentityParts.PublicKey;
					}
				}
				else if (string.Equals(text3, "PublicKeyToken", 5))
				{
					if ((assemblyIdentityParts2 & AssemblyIdentityParts.PublicKeyToken) != (AssemblyIdentityParts)0)
					{
						return false;
					}
					assemblyIdentityParts2 |= AssemblyIdentityParts.PublicKeyToken;
					if (!(text4 == "*"))
					{
						ImmutableArray<byte> immutableArray4;
						if (!AssemblyIdentity.TryParsePublicKeyToken(text4, out immutableArray4))
						{
							return false;
						}
						immutableArray2 = immutableArray4;
						assemblyIdentityParts |= AssemblyIdentityParts.PublicKeyToken;
					}
				}
				else if (string.Equals(text3, "Retargetable", 5))
				{
					if ((assemblyIdentityParts2 & AssemblyIdentityParts.Retargetability) != (AssemblyIdentityParts)0)
					{
						return false;
					}
					assemblyIdentityParts2 |= AssemblyIdentityParts.Retargetability;
					if (!(text4 == "*"))
					{
						if (string.Equals(text4, "Yes", 5))
						{
							flag = true;
						}
						else
						{
							if (!string.Equals(text4, "No", 5))
							{
								return false;
							}
							flag = false;
						}
						assemblyIdentityParts |= AssemblyIdentityParts.Retargetability;
					}
				}
				else if (string.Equals(text3, "ContentType", 5))
				{
					if ((assemblyIdentityParts2 & AssemblyIdentityParts.ContentType) != (AssemblyIdentityParts)0)
					{
						return false;
					}
					assemblyIdentityParts2 |= AssemblyIdentityParts.ContentType;
					if (!(text4 == "*"))
					{
						if (!string.Equals(text4, "WindowsRuntime", 5))
						{
							return false;
						}
						assemblyContentType = 1;
						assemblyIdentityParts |= AssemblyIdentityParts.ContentType;
					}
				}
				else
				{
					assemblyIdentityParts |= AssemblyIdentityParts.Unknown;
				}
			}
			if (flag && assemblyContentType == 1)
			{
				return false;
			}
			bool flag2 = !immutableArray.IsDefault;
			bool flag3 = !immutableArray2.IsDefault;
			identity = new AssemblyIdentity(text, version, text2, flag2 ? immutableArray : immutableArray2, flag2, flag, assemblyContentType);
			if (flag2 && flag3 && !identity.PublicKeyToken.SequenceEqual<byte, byte>(immutableArray2, null))
			{
				identity = null;
				return false;
			}
			parts = assemblyIdentityParts;
			return true;
		}

		// Token: 0x06001232 RID: 4658 RVA: 0x000382E0 File Offset: 0x000364E0
		private static bool TryParseNameToken(string displayName, ref int position, [Nullable(2)] [NotNullWhen(true)] out string value)
		{
			for (int i = position; i != displayName.Length; i++)
			{
				if (!AssemblyIdentity.IsWhiteSpace(displayName.get_Chars(i)))
				{
					char c;
					if (AssemblyIdentity.IsQuote(displayName.get_Chars(i)))
					{
						c = displayName.get_Chars(i++);
					}
					else
					{
						c = '\0';
					}
					int num = i;
					int num2 = displayName.Length;
					bool flag = false;
					while (i < displayName.Length)
					{
						char c2 = displayName.get_Chars(i);
						if (c2 == '\\')
						{
							flag = true;
							i += 2;
						}
						else
						{
							if (c == '\0')
							{
								if (AssemblyIdentity.IsNameTokenTerminator(c2))
								{
									goto IL_00A6;
								}
								if (AssemblyIdentity.IsQuote(c2))
								{
									value = null;
									return false;
								}
							}
							else if (c2 == c)
							{
								num2 = i;
								i++;
								goto IL_00A6;
							}
							i++;
							continue;
							IL_00A6:
							if (c == '\0')
							{
								int num3 = i - 1;
								while (num3 >= num && AssemblyIdentity.IsWhiteSpace(displayName.get_Chars(num3)))
								{
									num3--;
								}
								num2 = num3 + 1;
							}
							else
							{
								while (i < displayName.Length)
								{
									char c3 = displayName.get_Chars(i);
									if (!AssemblyIdentity.IsWhiteSpace(c3))
									{
										if (!AssemblyIdentity.IsNameTokenTerminator(c3))
										{
											value = null;
											return false;
										}
										break;
									}
									else
									{
										i++;
									}
								}
							}
							position = i;
							if (num2 == num)
							{
								value = null;
								return false;
							}
							if (!flag)
							{
								value = displayName.Substring(num, num2 - num);
								return true;
							}
							return AssemblyIdentity.TryUnescape(displayName, num, num2, out value);
						}
					}
					i = displayName.Length;
					goto IL_00A6;
				}
			}
			value = null;
			return false;
		}

		// Token: 0x06001233 RID: 4659 RVA: 0x00038412 File Offset: 0x00036612
		private static bool IsNameTokenTerminator(char c)
		{
			return c == '=' || c == ',';
		}

		// Token: 0x06001234 RID: 4660 RVA: 0x00038420 File Offset: 0x00036620
		private static bool IsQuote(char c)
		{
			return c == '"' || c == '\'';
		}

		// Token: 0x06001235 RID: 4661 RVA: 0x0003842E File Offset: 0x0003662E
		internal static Version ToVersion(ulong version)
		{
			return new Version((int)((ushort)(version >> 48)), (int)((ushort)(version >> 32)), (int)((ushort)(version >> 16)), (int)((ushort)version));
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x00038448 File Offset: 0x00036648
		internal static bool TryParseVersion(string str, out ulong result, out AssemblyIdentityParts parts)
		{
			parts = (AssemblyIdentityParts)0;
			result = 0UL;
			int num = 48;
			int num2 = 0;
			int num3 = 0;
			bool flag = false;
			bool flag2 = false;
			int num4 = 0;
			for (;;)
			{
				char c = ((num4 < str.Length) ? str.get_Chars(num4++) : '\0');
				if (c == '.' || c == '\0')
				{
					if (num2 == 4 || (flag && flag2))
					{
						break;
					}
					result |= (ulong)((ulong)((long)num3) << num);
					if (flag || flag2)
					{
						parts |= (AssemblyIdentityParts)(2 << num2);
					}
					if (c == '\0')
					{
						return true;
					}
					num3 = 0;
					num -= 16;
					num2++;
					flag = (flag2 = false);
				}
				else if (c >= '0' && c <= '9')
				{
					flag = true;
					num3 = num3 * 10 + (int)c - 48;
					if (num3 > 65535)
					{
						return false;
					}
				}
				else
				{
					if (c != '*')
					{
						return false;
					}
					flag2 = true;
				}
			}
			return false;
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x00038507 File Offset: 0x00036707
		[NullableContext(0)]
		private static bool TryParsePublicKey([Nullable(1)] string value, out ImmutableArray<byte> key)
		{
			if (!AssemblyIdentity.TryParseHexBytes(value, out key) || !MetadataHelpers.IsValidPublicKey(key))
			{
				key = default(ImmutableArray<byte>);
				return false;
			}
			return true;
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x0003852C File Offset: 0x0003672C
		[NullableContext(0)]
		private static bool TryParsePublicKeyToken([Nullable(1)] string value, out ImmutableArray<byte> token)
		{
			if (string.Equals(value, "null", 5) || string.Equals(value, "neutral", 5))
			{
				token = ImmutableArray<byte>.Empty;
				return true;
			}
			ImmutableArray<byte> immutableArray;
			if (value.Length != 16 || !AssemblyIdentity.TryParseHexBytes(value, out immutableArray))
			{
				token = default(ImmutableArray<byte>);
				return false;
			}
			token = immutableArray;
			return true;
		}

		// Token: 0x06001239 RID: 4665 RVA: 0x00038588 File Offset: 0x00036788
		[NullableContext(0)]
		private static bool TryParseHexBytes([Nullable(1)] string value, out ImmutableArray<byte> result)
		{
			if (value.Length == 0 || value.Length % 2 != 0)
			{
				result = default(ImmutableArray<byte>);
				return false;
			}
			int num = value.Length / 2;
			ArrayBuilder<byte> instance = ArrayBuilder<byte>.GetInstance(num);
			for (int i = 0; i < num; i++)
			{
				int num2 = AssemblyIdentity.HexValue(value.get_Chars(i * 2));
				int num3 = AssemblyIdentity.HexValue(value.get_Chars(i * 2 + 1));
				if (num2 < 0 || num3 < 0)
				{
					result = default(ImmutableArray<byte>);
					instance.Free();
					return false;
				}
				instance.Add((byte)((num2 << 4) | num3));
			}
			result = instance.ToImmutableAndFree();
			return true;
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x0003861F File Offset: 0x0003681F
		internal static int HexValue(char c)
		{
			if (c >= '0' && c <= '9')
			{
				return (int)(c - '0');
			}
			if (c >= 'a' && c <= 'f')
			{
				return (int)(c - 'a' + '\n');
			}
			if (c >= 'A' && c <= 'F')
			{
				return (int)(c - 'A' + '\n');
			}
			return -1;
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x00038655 File Offset: 0x00036855
		private static bool IsWhiteSpace(char c)
		{
			return c == ' ' || c == '\t' || c == '\r' || c == '\n';
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x00038670 File Offset: 0x00036870
		private static void EscapeName(StringBuilder result, [Nullable(2)] string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return;
			}
			bool flag = false;
			if (AssemblyIdentity.IsWhiteSpace(name.get_Chars(0)) || AssemblyIdentity.IsWhiteSpace(name.get_Chars(name.Length - 1)))
			{
				result.Append('"');
				flag = true;
			}
			int i = 0;
			while (i < name.Length)
			{
				char c = name.get_Chars(i);
				if (c <= '\'')
				{
					switch (c)
					{
					case '\t':
						result.Append("\\t");
						break;
					case '\n':
						result.Append("\\n");
						break;
					case '\v':
					case '\f':
						goto IL_00C2;
					case '\r':
						result.Append("\\r");
						break;
					default:
						if (c != '"' && c != '\'')
						{
							goto IL_00C2;
						}
						goto IL_0085;
					}
				}
				else
				{
					if (c == ',' || c == '=' || c == '\\')
					{
						goto IL_0085;
					}
					goto IL_00C2;
				}
				IL_00CA:
				i++;
				continue;
				IL_0085:
				result.Append('\\');
				result.Append(c);
				goto IL_00CA;
				IL_00C2:
				result.Append(c);
				goto IL_00CA;
			}
			if (flag)
			{
				result.Append('"');
			}
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x00038764 File Offset: 0x00036964
		private static bool TryUnescape(string str, int start, int end, [Nullable(2)] [NotNullWhen(true)] out string value)
		{
			PooledStringBuilder instance = PooledStringBuilder.GetInstance();
			int i = start;
			while (i < end)
			{
				char c = str.get_Chars(i++);
				if (c == '\\')
				{
					if (!AssemblyIdentity.Unescape(instance.Builder, str, ref i))
					{
						value = null;
						return false;
					}
				}
				else
				{
					instance.Builder.Append(c);
				}
			}
			value = instance.ToStringAndFree();
			return true;
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x000387BC File Offset: 0x000369BC
		private static bool Unescape(StringBuilder sb, string str, ref int i)
		{
			if (i == str.Length)
			{
				return false;
			}
			int num = i;
			i = num + 1;
			char c = str.get_Chars(num);
			if (c <= '/')
			{
				if (c <= '\'')
				{
					if (c != '"' && c != '\'')
					{
						return false;
					}
				}
				else if (c != ',' && c != '/')
				{
					return false;
				}
			}
			else if (c <= '\\')
			{
				if (c != '=' && c != '\\')
				{
					return false;
				}
			}
			else
			{
				if (c == 'n')
				{
					sb.Append("\n");
					return true;
				}
				switch (c)
				{
				case 'r':
					sb.Append("\r");
					return true;
				case 's':
					return false;
				case 't':
					sb.Append("\t");
					return true;
				case 'u':
				{
					int num2 = str.IndexOf(';', i);
					if (num2 == -1)
					{
						return false;
					}
					try
					{
						int num3 = Convert.ToInt32(str.Substring(i, num2 - i), 16);
						if (num3 == 0)
						{
							return false;
						}
						sb.Append(char.ConvertFromUtf32(num3));
					}
					catch
					{
						return false;
					}
					i = num2 + 1;
					return true;
				}
				default:
					return false;
				}
			}
			sb.Append(c);
			return true;
		}

		// Token: 0x0400056B RID: 1387
		private readonly AssemblyContentType _contentType;

		// Token: 0x0400056C RID: 1388
		private readonly string _name;

		// Token: 0x0400056D RID: 1389
		private readonly Version _version;

		// Token: 0x0400056E RID: 1390
		private readonly string _cultureName;

		// Token: 0x0400056F RID: 1391
		[Nullable(0)]
		private readonly ImmutableArray<byte> _publicKey;

		// Token: 0x04000570 RID: 1392
		[Nullable(0)]
		private ImmutableArray<byte> _lazyPublicKeyToken;

		// Token: 0x04000571 RID: 1393
		private readonly bool _isRetargetable;

		// Token: 0x04000572 RID: 1394
		[Nullable(2)]
		private string _lazyDisplayName;

		// Token: 0x04000573 RID: 1395
		private int _lazyHashCode;

		// Token: 0x04000574 RID: 1396
		internal const int PublicKeyTokenSize = 8;

		// Token: 0x04000575 RID: 1397
		internal static readonly Version NullVersion = new Version(0, 0, 0, 0);

		// Token: 0x04000576 RID: 1398
		internal const string InvariantCultureDisplay = "neutral";

		// Token: 0x04000577 RID: 1399
		private const int PublicKeyTokenBytes = 8;
	}
}
