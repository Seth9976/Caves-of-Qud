using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000165 RID: 357
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DesktopAssemblyIdentityComparer : AssemblyIdentityComparer
	{
		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000E68 RID: 3688 RVA: 0x000298AC File Offset: 0x00027AAC
		public new static DesktopAssemblyIdentityComparer Default { get; } = new DesktopAssemblyIdentityComparer(default(AssemblyPortabilityPolicy));

		// Token: 0x06000E69 RID: 3689 RVA: 0x000298B3 File Offset: 0x00027AB3
		internal DesktopAssemblyIdentityComparer(AssemblyPortabilityPolicy policy)
		{
			this.policy = policy;
		}

		// Token: 0x06000E6A RID: 3690 RVA: 0x000298C2 File Offset: 0x00027AC2
		public static DesktopAssemblyIdentityComparer LoadFromXml(Stream input)
		{
			return new DesktopAssemblyIdentityComparer(AssemblyPortabilityPolicy.LoadFromXml(input));
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000E6B RID: 3691 RVA: 0x000298CF File Offset: 0x00027ACF
		internal AssemblyPortabilityPolicy PortabilityPolicy
		{
			get
			{
				return this.policy;
			}
		}

		// Token: 0x06000E6C RID: 3692 RVA: 0x000298D8 File Offset: 0x00027AD8
		internal override bool ApplyUnificationPolicies(ref AssemblyIdentity reference, ref AssemblyIdentity definition, AssemblyIdentityParts referenceParts, out bool isDefinitionFxAssembly)
		{
			if (reference.ContentType == null && AssemblyIdentityComparer.SimpleNameComparer.Equals(reference.Name, definition.Name) && AssemblyIdentityComparer.SimpleNameComparer.Equals(reference.Name, "mscorlib"))
			{
				isDefinitionFxAssembly = true;
				reference = definition;
				return true;
			}
			if (!reference.IsRetargetable && definition.IsRetargetable)
			{
				isDefinitionFxAssembly = false;
				return false;
			}
			reference = this.Port(reference);
			definition = this.Port(definition);
			if (reference.IsRetargetable && !definition.IsRetargetable)
			{
				if (!AssemblyIdentity.IsFullName(referenceParts))
				{
					isDefinitionFxAssembly = false;
					return false;
				}
				if (!DesktopAssemblyIdentityComparer.IsOptionallyRetargetableAssembly(reference) || !AssemblyIdentity.KeysEqual(reference, definition))
				{
					reference = DesktopAssemblyIdentityComparer.Retarget(reference);
				}
			}
			if (reference.IsRetargetable && definition.IsRetargetable)
			{
				isDefinitionFxAssembly = DesktopAssemblyIdentityComparer.IsRetargetableAssembly(definition);
			}
			else
			{
				isDefinitionFxAssembly = DesktopAssemblyIdentityComparer.IsFrameworkAssembly(definition);
			}
			return true;
		}

		// Token: 0x06000E6D RID: 3693 RVA: 0x000299C0 File Offset: 0x00027BC0
		private static bool IsFrameworkAssembly(AssemblyIdentity identity)
		{
			if (identity.ContentType != null)
			{
				return false;
			}
			DesktopAssemblyIdentityComparer.FrameworkAssemblyDictionary.Value value;
			if (!DesktopAssemblyIdentityComparer.s_arFxPolicy.TryGetValue(identity.Name, ref value) || !value.PublicKeyToken.SequenceEqual<byte, byte>(identity.PublicKeyToken, null))
			{
				return false;
			}
			uint num = (uint)((identity.Version.Major << 16) | identity.Version.Minor);
			uint num2 = (uint)((value.Version.Major << 16) | value.Version.Minor);
			return num <= num2;
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x00029A44 File Offset: 0x00027C44
		private static bool IsRetargetableAssembly(AssemblyIdentity identity)
		{
			bool flag;
			bool flag2;
			DesktopAssemblyIdentityComparer.IsRetargetableAssembly(identity, out flag, out flag2);
			return flag;
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x00029A5C File Offset: 0x00027C5C
		private static bool IsOptionallyRetargetableAssembly(AssemblyIdentity identity)
		{
			if (!identity.IsRetargetable)
			{
				return false;
			}
			bool flag;
			bool flag2;
			DesktopAssemblyIdentityComparer.IsRetargetableAssembly(identity, out flag, out flag2);
			return flag && flag2;
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x00029A80 File Offset: 0x00027C80
		private static bool IsTriviallyNonRetargetable(AssemblyIdentity identity)
		{
			return identity.CultureName.Length != 0 || identity.ContentType != null || !identity.IsStrongName;
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x00029AA4 File Offset: 0x00027CA4
		private static void IsRetargetableAssembly(AssemblyIdentity identity, out bool retargetable, out bool portable)
		{
			retargetable = (portable = false);
			if (DesktopAssemblyIdentityComparer.IsTriviallyNonRetargetable(identity))
			{
				return;
			}
			DesktopAssemblyIdentityComparer.FrameworkRetargetingDictionary.Value value;
			retargetable = DesktopAssemblyIdentityComparer.s_arRetargetPolicy.TryGetValue(identity, out value);
			portable = value.IsPortable;
		}

		// Token: 0x06000E72 RID: 3698 RVA: 0x00029ADC File Offset: 0x00027CDC
		private static AssemblyIdentity Retarget(AssemblyIdentity identity)
		{
			if (DesktopAssemblyIdentityComparer.IsTriviallyNonRetargetable(identity))
			{
				return identity;
			}
			DesktopAssemblyIdentityComparer.FrameworkRetargetingDictionary.Value value;
			if (DesktopAssemblyIdentityComparer.s_arRetargetPolicy.TryGetValue(identity, out value))
			{
				return new AssemblyIdentity(value.NewName ?? identity.Name, (Version)value.NewVersion, identity.CultureName, value.NewPublicKeyToken, false, identity.IsRetargetable, 0);
			}
			return identity;
		}

		// Token: 0x06000E73 RID: 3699 RVA: 0x00029B38 File Offset: 0x00027D38
		private AssemblyIdentity Port(AssemblyIdentity identity)
		{
			if (identity.IsRetargetable || !identity.IsStrongName || identity.ContentType != null)
			{
				return identity;
			}
			Version version = null;
			ImmutableArray<byte> immutableArray = default(ImmutableArray<byte>);
			AssemblyVersion assemblyVersion = (AssemblyVersion)identity.Version;
			if (assemblyVersion >= new AssemblyVersion(2, 0, 0, 0) && assemblyVersion <= new AssemblyVersion(5, 9, 0, 0))
			{
				if (identity.PublicKeyToken.SequenceEqual<byte, byte>(DesktopAssemblyIdentityComparer.s_SILVERLIGHT_PLATFORM_PUBLICKEY_STR_L, null))
				{
					if (!this.policy.SuppressSilverlightPlatformAssembliesPortability && (AssemblyIdentityComparer.SimpleNameComparer.Equals(identity.Name, "System") || AssemblyIdentityComparer.SimpleNameComparer.Equals(identity.Name, "System.Core")))
					{
						version = (Version)DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L;
						immutableArray = DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L;
					}
				}
				else if (identity.PublicKeyToken.SequenceEqual<byte, byte>(DesktopAssemblyIdentityComparer.s_SILVERLIGHT_PUBLICKEY_STR_L, null) && !this.policy.SuppressSilverlightLibraryAssembliesPortability)
				{
					if (AssemblyIdentityComparer.SimpleNameComparer.Equals(identity.Name, "Microsoft.VisualBasic"))
					{
						version = new Version(10, 0, 0, 0);
						immutableArray = DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L;
					}
					if (AssemblyIdentityComparer.SimpleNameComparer.Equals(identity.Name, "System.ComponentModel.Composition"))
					{
						version = (Version)DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L;
						immutableArray = DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L;
					}
				}
			}
			if (version == null)
			{
				return identity;
			}
			return new AssemblyIdentity(identity.Name, version, identity.CultureName, immutableArray, false, identity.IsRetargetable, 0);
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x00029C9C File Offset: 0x00027E9C
		// Note: this type is marked as 'beforefieldinit'.
		static DesktopAssemblyIdentityComparer()
		{
			DesktopAssemblyIdentityComparer.FrameworkRetargetingDictionary frameworkRetargetingDictionary = new DesktopAssemblyIdentityComparer.FrameworkRetargetingDictionary();
			frameworkRetargetingDictionary.Add("System", DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, new AssemblyVersion(1, 0, 0, 0), null, null, DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("System.Xml", DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, new AssemblyVersion(1, 0, 0, 0), null, null, DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("System", DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_1, new AssemblyVersion(1, 0, 5000, 0), null, null, DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("System", DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_2, new AssemblyVersion(1, 0, 5000, 0), null, null, DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("System.Drawing", DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_2, new AssemblyVersion(1, 0, 5000, 0), null, null, DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("System.Web.Services", DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_2, new AssemblyVersion(1, 0, 5000, 0), null, null, DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("System.Windows.Forms", DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_2, new AssemblyVersion(1, 0, 5000, 0), null, null, DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("System.Xml", DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_2, new AssemblyVersion(1, 0, 5000, 0), null, null, DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("System", DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_3, new AssemblyVersion(1, 0, 5000, 0), null, null, DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("System.Data", DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_3, new AssemblyVersion(1, 0, 5000, 0), null, null, DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("System.Drawing", DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_3, new AssemblyVersion(1, 0, 5000, 0), null, null, DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("System.Web.Services", DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_3, new AssemblyVersion(1, 0, 5000, 0), null, null, DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("System.Windows.Forms", DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_3, new AssemblyVersion(1, 0, 5000, 0), null, null, DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("System.Xml", DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_3, new AssemblyVersion(1, 0, 5000, 0), null, null, DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("Microsoft.VisualBasic", DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_3, new AssemblyVersion(7, 0, 5000, 0), null, null, DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_VS_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("System", DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_1, new AssemblyVersion(1, 0, 5500, 0), null, null, DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("System", DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_2, new AssemblyVersion(1, 0, 5500, 0), null, null, DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("System.Drawing", DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_2, new AssemblyVersion(1, 0, 5500, 0), null, null, DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("System.Web.Services", DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_2, new AssemblyVersion(1, 0, 5500, 0), null, null, DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("System.Windows.Forms", DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_2, new AssemblyVersion(1, 0, 5500, 0), null, null, DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("System.Xml", DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_2, new AssemblyVersion(1, 0, 5500, 0), null, null, DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("System", DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_3, new AssemblyVersion(1, 0, 5500, 0), null, null, DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("System.Data", DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_3, new AssemblyVersion(1, 0, 5500, 0), null, null, DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("System.Drawing", DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_3, new AssemblyVersion(1, 0, 5500, 0), null, null, DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("System.Web.Services", DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_3, new AssemblyVersion(1, 0, 5500, 0), null, null, DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("System.Windows.Forms", DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_3, new AssemblyVersion(1, 0, 5500, 0), null, null, DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("System.Xml", DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_3, new AssemblyVersion(1, 0, 5500, 0), null, null, DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("Microsoft.VisualBasic", DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_3, new AssemblyVersion(7, 0, 5500, 0), null, null, DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_VS_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("Microsoft.WindowsCE.Forms", DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_1, new AssemblyVersion(1, 0, 5000, 0), null, null, DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_3, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("Microsoft.WindowsCE.Forms", DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_1, new AssemblyVersion(1, 0, 5500, 0), null, null, DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_3, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("Microsoft.WindowsCE.Forms", DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_2, new AssemblyVersion(1, 0, 5000, 0), null, null, DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_3, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("Microsoft.WindowsCE.Forms", DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_2, new AssemblyVersion(1, 0, 5500, 0), null, null, DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_3, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("Microsoft.WindowsCE.Forms", DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_3, new AssemblyVersion(1, 0, 5000, 0), null, null, DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_3, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("Microsoft.WindowsCE.Forms", DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_3, new AssemblyVersion(1, 0, 5500, 0), null, null, DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_3, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("System.Data.SqlClient", DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_3, new AssemblyVersion(1, 0, 5000, 0), null, "System.Data", DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("System.Data.SqlClient", DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_3, new AssemblyVersion(1, 0, 5500, 0), null, "System.Data", DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("System.Data.Common", DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_3, new AssemblyVersion(1, 0, 5000, 0), null, "System.Data", DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("System.Data.Common", DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_3, new AssemblyVersion(1, 0, 5500, 0), null, "System.Data", DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("System.Windows.Forms.DataGrid", DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_3, new AssemblyVersion(1, 0, 5000, 0), null, "System.Windows.Forms", DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("System.Windows.Forms.DataGrid", DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_3, new AssemblyVersion(1, 0, 5500, 0), null, "System.Windows.Forms", DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("System", DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_3, new AssemblyVersion(2, 0, 0, 0), new AssemblyVersion(2, 0, 10, 0), null, DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("System.Xml", DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_3, new AssemblyVersion(2, 0, 0, 0), new AssemblyVersion(2, 0, 10, 0), null, DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("System.Drawing", DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_3, new AssemblyVersion(2, 0, 0, 0), new AssemblyVersion(2, 0, 10, 0), null, DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("System.Web.Services", DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_3, new AssemblyVersion(2, 0, 0, 0), new AssemblyVersion(2, 0, 10, 0), null, DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("System.Windows.Forms", DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_3, new AssemblyVersion(2, 0, 0, 0), new AssemblyVersion(2, 0, 10, 0), null, DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("System.Data", DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_3, new AssemblyVersion(2, 0, 0, 0), new AssemblyVersion(2, 0, 10, 0), null, DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("System.Messaging", DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_3, new AssemblyVersion(2, 0, 0, 0), new AssemblyVersion(2, 0, 10, 0), null, DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("System.Data.SqlClient", DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_3, new AssemblyVersion(2, 0, 0, 0), new AssemblyVersion(2, 0, 10, 0), "System.Data", DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("System.Data.Common", DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_3, new AssemblyVersion(2, 0, 0, 0), new AssemblyVersion(2, 0, 10, 0), "System.Data", DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("System.Windows.Forms.DataGrid", DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_3, new AssemblyVersion(2, 0, 0, 0), new AssemblyVersion(2, 0, 10, 0), "System.Windows.Forms", DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("Microsoft.VisualBasic", DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_3, new AssemblyVersion(8, 0, 0, 0), new AssemblyVersion(8, 0, 10, 0), null, DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_VS_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("System", DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_3, new AssemblyVersion(3, 5, 0, 0), new AssemblyVersion(3, 9, 0, 0), null, DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("System.Xml", DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_3, new AssemblyVersion(3, 5, 0, 0), new AssemblyVersion(3, 9, 0, 0), null, DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("System.Drawing", DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_3, new AssemblyVersion(3, 5, 0, 0), new AssemblyVersion(3, 9, 0, 0), null, DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("System.Web.Services", DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_3, new AssemblyVersion(3, 5, 0, 0), new AssemblyVersion(3, 9, 0, 0), null, DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("System.Windows.Forms", DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_3, new AssemblyVersion(3, 5, 0, 0), new AssemblyVersion(3, 9, 0, 0), null, DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("System.Data", DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_3, new AssemblyVersion(3, 5, 0, 0), new AssemblyVersion(3, 9, 0, 0), null, DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("System.Messaging", DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_3, new AssemblyVersion(3, 5, 0, 0), new AssemblyVersion(3, 9, 0, 0), null, DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("System.Data.SqlClient", DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_3, new AssemblyVersion(3, 5, 0, 0), new AssemblyVersion(3, 9, 0, 0), "System.Data", DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("System.Windows.Forms.DataGrid", DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_3, new AssemblyVersion(3, 5, 0, 0), new AssemblyVersion(3, 9, 0, 0), "System.Windows.Forms", DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("Microsoft.VisualBasic", DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_3, new AssemblyVersion(8, 1, 0, 0), new AssemblyVersion(8, 1, 5, 0), "Microsoft.VisualBasic", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_VS_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("System.Data.SqlClient", DesktopAssemblyIdentityComparer.s_SQL_MOBILE_PUBLIC_KEY_TOKEN, new AssemblyVersion(3, 5, 0, 0), null, "System.Data", DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("System.Data.SqlServerCe", DesktopAssemblyIdentityComparer.s_SQL_MOBILE_PUBLIC_KEY_TOKEN, new AssemblyVersion(3, 5, 0, 0), null, null, DesktopAssemblyIdentityComparer.s_SQL_PUBLIC_KEY_TOKEN, DesktopAssemblyIdentityComparer.s_VER_SQL_ORCAS_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("System.Data.SqlServerCe", DesktopAssemblyIdentityComparer.s_SQL_MOBILE_PUBLIC_KEY_TOKEN, new AssemblyVersion(3, 5, 1, 0), new AssemblyVersion(3, 5, 200, 999), null, DesktopAssemblyIdentityComparer.s_SQL_PUBLIC_KEY_TOKEN, DesktopAssemblyIdentityComparer.s_VER_SQL_ORCAS_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("System.Data.SqlClient", DesktopAssemblyIdentityComparer.s_SQL_MOBILE_PUBLIC_KEY_TOKEN, new AssemblyVersion(3, 0, 3600, 0), null, "System.Data", DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("System.Data.SqlServerCe", DesktopAssemblyIdentityComparer.s_SQL_MOBILE_PUBLIC_KEY_TOKEN, new AssemblyVersion(3, 0, 3600, 0), null, null, DesktopAssemblyIdentityComparer.s_SQL_PUBLIC_KEY_TOKEN, DesktopAssemblyIdentityComparer.s_VER_SQL_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("system.xml.linq", DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_3, new AssemblyVersion(3, 5, 0, 0), new AssemblyVersion(3, 9, 0, 0), null, DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_LINQ_ASSEMBLYVERSION_STR_2_L);
			frameworkRetargetingDictionary.Add("system.data.DataSetExtensions", DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_3, new AssemblyVersion(3, 5, 0, 0), new AssemblyVersion(3, 9, 0, 0), null, DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_LINQ_ASSEMBLYVERSION_STR_2_L);
			frameworkRetargetingDictionary.Add("System.Core", DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_3, new AssemblyVersion(3, 5, 0, 0), new AssemblyVersion(3, 9, 0, 0), null, DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_LINQ_ASSEMBLYVERSION_STR_2_L);
			frameworkRetargetingDictionary.Add("System.ServiceModel", DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_3, new AssemblyVersion(3, 5, 0, 0), new AssemblyVersion(3, 9, 0, 0), null, DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_LINQ_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("System.Runtime.Serialization", DesktopAssemblyIdentityComparer.s_NETCF_PUBLIC_KEY_TOKEN_3, new AssemblyVersion(3, 5, 0, 0), new AssemblyVersion(3, 9, 0, 0), null, DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_LINQ_ASSEMBLYVERSION_STR_L);
			frameworkRetargetingDictionary.Add("mscorlib", DesktopAssemblyIdentityComparer.s_SILVERLIGHT_PLATFORM_PUBLICKEY_STR_L, new AssemblyVersion(2, 0, 5, 0), new AssemblyVersion(99, 0, 0, 0), null, DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L, true);
			frameworkRetargetingDictionary.Add("System", DesktopAssemblyIdentityComparer.s_SILVERLIGHT_PLATFORM_PUBLICKEY_STR_L, new AssemblyVersion(2, 0, 5, 0), new AssemblyVersion(99, 0, 0, 0), null, DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L, true);
			frameworkRetargetingDictionary.Add("System.ComponentModel.Composition", DesktopAssemblyIdentityComparer.s_SILVERLIGHT_PUBLICKEY_STR_L, new AssemblyVersion(2, 0, 5, 0), new AssemblyVersion(99, 0, 0, 0), null, DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L, true);
			frameworkRetargetingDictionary.Add("System.ComponentModel.DataAnnotations", DesktopAssemblyIdentityComparer.s_RIA_SERVICES_KEY_TOKEN, new AssemblyVersion(2, 0, 5, 0), new AssemblyVersion(99, 0, 0, 0), null, DesktopAssemblyIdentityComparer.s_SHAREDLIB_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L, true);
			frameworkRetargetingDictionary.Add("System.Core", DesktopAssemblyIdentityComparer.s_SILVERLIGHT_PLATFORM_PUBLICKEY_STR_L, new AssemblyVersion(2, 0, 5, 0), new AssemblyVersion(99, 0, 0, 0), null, DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L, true);
			frameworkRetargetingDictionary.Add("System.Net", DesktopAssemblyIdentityComparer.s_SILVERLIGHT_PLATFORM_PUBLICKEY_STR_L, new AssemblyVersion(2, 0, 5, 0), new AssemblyVersion(99, 0, 0, 0), null, DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L, true);
			frameworkRetargetingDictionary.Add("System.Numerics", DesktopAssemblyIdentityComparer.s_SILVERLIGHT_PUBLICKEY_STR_L, new AssemblyVersion(2, 0, 5, 0), new AssemblyVersion(99, 0, 0, 0), null, DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L, true);
			frameworkRetargetingDictionary.Add("Microsoft.CSharp", DesktopAssemblyIdentityComparer.s_SILVERLIGHT_PUBLICKEY_STR_L, new AssemblyVersion(2, 0, 5, 0), new AssemblyVersion(99, 0, 0, 0), null, DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L, true);
			frameworkRetargetingDictionary.Add("System.Runtime.Serialization", DesktopAssemblyIdentityComparer.s_SILVERLIGHT_PLATFORM_PUBLICKEY_STR_L, new AssemblyVersion(2, 0, 5, 0), new AssemblyVersion(99, 0, 0, 0), null, DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L, true);
			frameworkRetargetingDictionary.Add("System.ServiceModel", DesktopAssemblyIdentityComparer.s_SILVERLIGHT_PUBLICKEY_STR_L, new AssemblyVersion(2, 0, 5, 0), new AssemblyVersion(99, 0, 0, 0), null, DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L, true);
			frameworkRetargetingDictionary.Add("System.ServiceModel.Web", DesktopAssemblyIdentityComparer.s_SILVERLIGHT_PLATFORM_PUBLICKEY_STR_L, new AssemblyVersion(2, 0, 5, 0), new AssemblyVersion(99, 0, 0, 0), null, DesktopAssemblyIdentityComparer.s_SHAREDLIB_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L, true);
			frameworkRetargetingDictionary.Add("System.Xml", DesktopAssemblyIdentityComparer.s_SILVERLIGHT_PLATFORM_PUBLICKEY_STR_L, new AssemblyVersion(2, 0, 5, 0), new AssemblyVersion(99, 0, 0, 0), null, DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L, true);
			frameworkRetargetingDictionary.Add("System.Xml.Linq", DesktopAssemblyIdentityComparer.s_SILVERLIGHT_PUBLICKEY_STR_L, new AssemblyVersion(2, 0, 5, 0), new AssemblyVersion(99, 0, 0, 0), null, DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L, true);
			frameworkRetargetingDictionary.Add("System.Xml.Serialization", DesktopAssemblyIdentityComparer.s_SILVERLIGHT_PUBLICKEY_STR_L, new AssemblyVersion(2, 0, 5, 0), new AssemblyVersion(99, 0, 0, 0), null, DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L, true);
			frameworkRetargetingDictionary.Add("System.Windows", DesktopAssemblyIdentityComparer.s_SILVERLIGHT_PLATFORM_PUBLICKEY_STR_L, new AssemblyVersion(2, 0, 5, 0), new AssemblyVersion(99, 0, 0, 0), null, DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L, true);
			DesktopAssemblyIdentityComparer.s_arRetargetPolicy = frameworkRetargetingDictionary;
			DesktopAssemblyIdentityComparer.FrameworkAssemblyDictionary frameworkAssemblyDictionary = new DesktopAssemblyIdentityComparer.FrameworkAssemblyDictionary();
			frameworkAssemblyDictionary.Add("Accessibility", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("CustomMarshalers", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("ISymWrapper", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("Microsoft.JScript", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_VS_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("Microsoft.VisualBasic", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_VS_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("Microsoft.VisualBasic.Compatibility", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_VS_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("Microsoft.VisualBasic.Compatibility.Data", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_VS_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("Microsoft.VisualC", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_VS_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("mscorlib", DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System", DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Configuration", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Configuration.Install", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Data", DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Data.OracleClient", DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Data.SqlXml", DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Deployment", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Design", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.DirectoryServices", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.DirectoryServices.Protocols", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Drawing", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Drawing.Design", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.EnterpriseServices", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Management", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Messaging", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Runtime.Remoting", DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Runtime.Serialization.Formatters.Soap", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Security", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.ServiceProcess", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Transactions", DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Web", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Web.Mobile", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Web.RegularExpressions", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Web.Services", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Windows.Forms", DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Xml", DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("AspNetMMCExt", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("sysglobl", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("Microsoft.Build.Engine", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("Microsoft.Build.Framework", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("PresentationCFFRasterizer", DesktopAssemblyIdentityComparer.s_SHAREDLIB_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("PresentationCore", DesktopAssemblyIdentityComparer.s_SHAREDLIB_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("PresentationFramework", DesktopAssemblyIdentityComparer.s_SHAREDLIB_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("PresentationFramework.Aero", DesktopAssemblyIdentityComparer.s_SHAREDLIB_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("PresentationFramework.Classic", DesktopAssemblyIdentityComparer.s_SHAREDLIB_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("PresentationFramework.Luna", DesktopAssemblyIdentityComparer.s_SHAREDLIB_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("PresentationFramework.Royale", DesktopAssemblyIdentityComparer.s_SHAREDLIB_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("PresentationUI", DesktopAssemblyIdentityComparer.s_SHAREDLIB_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("ReachFramework", DesktopAssemblyIdentityComparer.s_SHAREDLIB_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Printing", DesktopAssemblyIdentityComparer.s_SHAREDLIB_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Speech", DesktopAssemblyIdentityComparer.s_SHAREDLIB_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("UIAutomationClient", DesktopAssemblyIdentityComparer.s_SHAREDLIB_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("UIAutomationClientsideProviders", DesktopAssemblyIdentityComparer.s_SHAREDLIB_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("UIAutomationProvider", DesktopAssemblyIdentityComparer.s_SHAREDLIB_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("UIAutomationTypes", DesktopAssemblyIdentityComparer.s_SHAREDLIB_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("WindowsBase", DesktopAssemblyIdentityComparer.s_SHAREDLIB_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("WindowsFormsIntegration", DesktopAssemblyIdentityComparer.s_SHAREDLIB_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("SMDiagnostics", DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.IdentityModel", DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.IdentityModel.Selectors", DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.IO.Log", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Runtime.Serialization", DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.ServiceModel", DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.ServiceModel.Install", DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.ServiceModel.WasHosting", DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Workflow.Activities", DesktopAssemblyIdentityComparer.s_SHAREDLIB_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Workflow.ComponentModel", DesktopAssemblyIdentityComparer.s_SHAREDLIB_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Workflow.Runtime", DesktopAssemblyIdentityComparer.s_SHAREDLIB_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("Microsoft.Transactions.Bridge", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("Microsoft.Transactions.Bridge.Dtc", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.AddIn", DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.AddIn.Contract", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.ComponentModel.Composition", DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Core", DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Data.DataSetExtensions", DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Data.Linq", DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Xml.Linq", DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.DirectoryServices.AccountManagement", DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Management.Instrumentation", DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Net", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.ServiceModel.Web", DesktopAssemblyIdentityComparer.s_SHAREDLIB_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Web.Extensions", DesktopAssemblyIdentityComparer.s_SHAREDLIB_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Web.Extensions.Design", DesktopAssemblyIdentityComparer.s_SHAREDLIB_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Windows.Presentation", DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.WorkflowServices", DesktopAssemblyIdentityComparer.s_SHAREDLIB_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.ComponentModel.DataAnnotations", DesktopAssemblyIdentityComparer.s_SHAREDLIB_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Data.Entity", DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Data.Entity.Design", DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Data.Services", DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Data.Services.Client", DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Data.Services.Design", DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Web.Abstractions", DesktopAssemblyIdentityComparer.s_SHAREDLIB_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Web.DynamicData", DesktopAssemblyIdentityComparer.s_SHAREDLIB_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Web.DynamicData.Design", DesktopAssemblyIdentityComparer.s_SHAREDLIB_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Web.Entity", DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Web.Entity.Design", DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Web.Routing", DesktopAssemblyIdentityComparer.s_SHAREDLIB_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("Microsoft.Build", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("Microsoft.CSharp", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Dynamic", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Numerics", DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Xaml", DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("Microsoft.Workflow.Compiler", DesktopAssemblyIdentityComparer.s_SHAREDLIB_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("Microsoft.Activities.Build", DesktopAssemblyIdentityComparer.s_SHAREDLIB_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("Microsoft.Build.Conversion.v4.0", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("Microsoft.Build.Tasks.v4.0", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("Microsoft.Build.Utilities.v4.0", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("Microsoft.Internal.Tasks.Dataflow", DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("Microsoft.VisualBasic.Activities.Compiler", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_VS_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("Microsoft.VisualC.STLCLR", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_VC_STLCLR_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("Microsoft.Windows.ApplicationServer.Applications", DesktopAssemblyIdentityComparer.s_SHAREDLIB_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("PresentationBuildTasks", DesktopAssemblyIdentityComparer.s_SHAREDLIB_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("PresentationFramework.Aero2", DesktopAssemblyIdentityComparer.s_SHAREDLIB_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("PresentationFramework.AeroLite", DesktopAssemblyIdentityComparer.s_SHAREDLIB_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("PresentationFramework-SystemCore", DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("PresentationFramework-SystemData", DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("PresentationFramework-SystemDrawing", DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("PresentationFramework-SystemXml", DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("PresentationFramework-SystemXmlLinq", DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Activities", DesktopAssemblyIdentityComparer.s_SHAREDLIB_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Activities.Core.Presentation", DesktopAssemblyIdentityComparer.s_SHAREDLIB_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Activities.DurableInstancing", DesktopAssemblyIdentityComparer.s_SHAREDLIB_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Activities.Presentation", DesktopAssemblyIdentityComparer.s_SHAREDLIB_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.ComponentModel.Composition.Registration", DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Device", DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.IdentityModel.Services", DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.IO.Compression", DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.IO.Compression.FileSystem", DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Net.Http", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Net.Http.WebRequest", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Reflection.Context", DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Runtime.Caching", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Runtime.DurableInstancing", DesktopAssemblyIdentityComparer.s_SHAREDLIB_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Runtime.WindowsRuntime", DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Runtime.WindowsRuntime.UI.Xaml", DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.ServiceModel.Activation", DesktopAssemblyIdentityComparer.s_SHAREDLIB_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.ServiceModel.Activities", DesktopAssemblyIdentityComparer.s_SHAREDLIB_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.ServiceModel.Channels", DesktopAssemblyIdentityComparer.s_SHAREDLIB_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.ServiceModel.Discovery", DesktopAssemblyIdentityComparer.s_SHAREDLIB_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.ServiceModel.Internals", DesktopAssemblyIdentityComparer.s_SHAREDLIB_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.ServiceModel.Routing", DesktopAssemblyIdentityComparer.s_SHAREDLIB_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.ServiceModel.ServiceMoniker40", DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Web.ApplicationServices", DesktopAssemblyIdentityComparer.s_SHAREDLIB_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Web.DataVisualization", DesktopAssemblyIdentityComparer.s_SHAREDLIB_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Web.DataVisualization.Design", DesktopAssemblyIdentityComparer.s_SHAREDLIB_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Windows.Controls.Ribbon", DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Windows.Forms.DataVisualization", DesktopAssemblyIdentityComparer.s_SHAREDLIB_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Windows.Forms.DataVisualization.Design", DesktopAssemblyIdentityComparer.s_SHAREDLIB_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Windows.Input.Manipulations", DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Xaml.Hosting", DesktopAssemblyIdentityComparer.s_SHAREDLIB_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("XamlBuildTask", DesktopAssemblyIdentityComparer.s_SHAREDLIB_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("XsdBuildTask", DesktopAssemblyIdentityComparer.s_SHAREDLIB_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Numerics.Vectors", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Collections", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Collections.Concurrent", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.ComponentModel", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.ComponentModel.Annotations", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.ComponentModel.EventBasedAsync", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Diagnostics.Contracts", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Diagnostics.Debug", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Diagnostics.Tools", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Diagnostics.Tracing", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Dynamic.Runtime", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Globalization", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.IO", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Linq", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Linq.Expressions", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Linq.Parallel", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Linq.Queryable", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Net.Http.Rtc", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Net.NetworkInformation", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Net.Primitives", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Net.Requests", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.ObjectModel", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Reflection", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Reflection.Emit", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Reflection.Emit.ILGeneration", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Reflection.Emit.Lightweight", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Reflection.Extensions", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Reflection.Primitives", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Resources.ResourceManager", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Runtime", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Runtime.Extensions", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Runtime.Handles", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Runtime.InteropServices", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Runtime.InteropServices.WindowsRuntime", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Runtime.Numerics", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Runtime.Serialization.Json", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Runtime.Serialization.Primitives", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Runtime.Serialization.Xml", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Security.Principal", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.ServiceModel.Duplex", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.ServiceModel.Http", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.ServiceModel.NetTcp", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.ServiceModel.Primitives", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.ServiceModel.Security", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Text.Encoding", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Text.Encoding.Extensions", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Text.RegularExpressions", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Threading", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Threading.Tasks", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Threading.Tasks.Parallel", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Threading.Timer", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Xml.ReaderWriter", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Xml.XDocument", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Xml.XmlSerializer", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Windows", DesktopAssemblyIdentityComparer.s_MICROSOFT_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			frameworkAssemblyDictionary.Add("System.Xml.Serialization", DesktopAssemblyIdentityComparer.s_ECMA_PUBLICKEY_STR_L, DesktopAssemblyIdentityComparer.s_VER_ASSEMBLYVERSION_STR_L);
			DesktopAssemblyIdentityComparer.s_arFxPolicy = frameworkAssemblyDictionary;
		}

		// Token: 0x04000401 RID: 1025
		internal readonly AssemblyPortabilityPolicy policy;

		// Token: 0x04000402 RID: 1026
		[Nullable(0)]
		private static readonly ImmutableArray<byte> s_NETCF_PUBLIC_KEY_TOKEN_1 = ImmutableArray.Create<byte>(new byte[] { 28, 158, 37, 150, 134, 249, 33, 224 });

		// Token: 0x04000403 RID: 1027
		[Nullable(0)]
		private static readonly ImmutableArray<byte> s_NETCF_PUBLIC_KEY_TOKEN_2 = ImmutableArray.Create<byte>(new byte[] { 95, 213, 124, 84, 58, 156, 2, 71 });

		// Token: 0x04000404 RID: 1028
		[Nullable(0)]
		private static readonly ImmutableArray<byte> s_NETCF_PUBLIC_KEY_TOKEN_3 = ImmutableArray.Create<byte>(new byte[] { 150, 157, 184, 5, 61, 51, 34, 172 });

		// Token: 0x04000405 RID: 1029
		[Nullable(0)]
		private static readonly ImmutableArray<byte> s_SQL_PUBLIC_KEY_TOKEN = ImmutableArray.Create<byte>(new byte[] { 137, 132, 93, 205, 128, 128, 204, 145 });

		// Token: 0x04000406 RID: 1030
		[Nullable(0)]
		private static readonly ImmutableArray<byte> s_SQL_MOBILE_PUBLIC_KEY_TOKEN = ImmutableArray.Create<byte>(new byte[] { 59, 226, 53, 223, 28, 141, 42, 211 });

		// Token: 0x04000407 RID: 1031
		[Nullable(0)]
		private static readonly ImmutableArray<byte> s_ECMA_PUBLICKEY_STR_L = ImmutableArray.Create<byte>(new byte[] { 183, 122, 92, 86, 25, 52, 224, 137 });

		// Token: 0x04000408 RID: 1032
		[Nullable(0)]
		private static readonly ImmutableArray<byte> s_SHAREDLIB_PUBLICKEY_STR_L = ImmutableArray.Create<byte>(new byte[] { 49, 191, 56, 86, 173, 54, 78, 53 });

		// Token: 0x04000409 RID: 1033
		[Nullable(0)]
		private static readonly ImmutableArray<byte> s_MICROSOFT_PUBLICKEY_STR_L = ImmutableArray.Create<byte>(new byte[] { 176, 63, 95, 127, 17, 213, 10, 58 });

		// Token: 0x0400040A RID: 1034
		[Nullable(0)]
		private static readonly ImmutableArray<byte> s_SILVERLIGHT_PLATFORM_PUBLICKEY_STR_L = ImmutableArray.Create<byte>(new byte[] { 124, 236, 133, 215, 190, 167, 121, 142 });

		// Token: 0x0400040B RID: 1035
		[Nullable(0)]
		private static readonly ImmutableArray<byte> s_SILVERLIGHT_PUBLICKEY_STR_L = ImmutableArray.Create<byte>(new byte[] { 49, 191, 56, 86, 173, 54, 78, 53 });

		// Token: 0x0400040C RID: 1036
		[Nullable(0)]
		private static readonly ImmutableArray<byte> s_RIA_SERVICES_KEY_TOKEN = ImmutableArray.Create<byte>(new byte[] { 221, 208, 218, 77, 62, 103, 130, 23 });

		// Token: 0x0400040D RID: 1037
		private static readonly AssemblyVersion s_VER_VS_COMPATIBILITY_ASSEMBLYVERSION_STR_L = new AssemblyVersion(8, 0, 0, 0);

		// Token: 0x0400040E RID: 1038
		private static readonly AssemblyVersion s_VER_VS_ASSEMBLYVERSION_STR_L = new AssemblyVersion(10, 0, 0, 0);

		// Token: 0x0400040F RID: 1039
		private static readonly AssemblyVersion s_VER_SQL_ASSEMBLYVERSION_STR_L = new AssemblyVersion(9, 0, 242, 0);

		// Token: 0x04000410 RID: 1040
		private static readonly AssemblyVersion s_VER_LINQ_ASSEMBLYVERSION_STR_L = new AssemblyVersion(3, 0, 0, 0);

		// Token: 0x04000411 RID: 1041
		private static readonly AssemblyVersion s_VER_LINQ_ASSEMBLYVERSION_STR_2_L = new AssemblyVersion(3, 5, 0, 0);

		// Token: 0x04000412 RID: 1042
		private static readonly AssemblyVersion s_VER_SQL_ORCAS_ASSEMBLYVERSION_STR_L = new AssemblyVersion(3, 5, 0, 0);

		// Token: 0x04000413 RID: 1043
		private static readonly AssemblyVersion s_VER_ASSEMBLYVERSION_STR_L = new AssemblyVersion(4, 0, 0, 0);

		// Token: 0x04000414 RID: 1044
		private static readonly AssemblyVersion s_VER_VC_STLCLR_ASSEMBLYVERSION_STR_L = new AssemblyVersion(2, 0, 0, 0);

		// Token: 0x04000415 RID: 1045
		private const string NULL = null;

		// Token: 0x04000416 RID: 1046
		private const bool TRUE = true;

		// Token: 0x04000417 RID: 1047
		private static readonly DesktopAssemblyIdentityComparer.FrameworkRetargetingDictionary s_arRetargetPolicy;

		// Token: 0x04000418 RID: 1048
		private static readonly DesktopAssemblyIdentityComparer.FrameworkAssemblyDictionary s_arFxPolicy;

		// Token: 0x0200059E RID: 1438
		[NullableContext(0)]
		[Nullable(new byte[] { 0, 1 })]
		private sealed class FrameworkAssemblyDictionary : Dictionary<string, DesktopAssemblyIdentityComparer.FrameworkAssemblyDictionary.Value>
		{
			// Token: 0x06003567 RID: 13671 RVA: 0x0009657C File Offset: 0x0009477C
			public FrameworkAssemblyDictionary()
				: base(AssemblyIdentityComparer.SimpleNameComparer)
			{
			}

			// Token: 0x06003568 RID: 13672 RVA: 0x00096589 File Offset: 0x00094789
			public void Add([Nullable(1)] string name, ImmutableArray<byte> publicKeyToken, AssemblyVersion version)
			{
				base.Add(name, new DesktopAssemblyIdentityComparer.FrameworkAssemblyDictionary.Value(publicKeyToken, version));
			}

			// Token: 0x020007B8 RID: 1976
			public struct Value
			{
				// Token: 0x06003FB8 RID: 16312 RVA: 0x000B2203 File Offset: 0x000B0403
				public Value(ImmutableArray<byte> publicKeyToken, AssemblyVersion version)
				{
					this.PublicKeyToken = publicKeyToken;
					this.Version = version;
				}

				// Token: 0x04001DC4 RID: 7620
				public readonly ImmutableArray<byte> PublicKeyToken;

				// Token: 0x04001DC5 RID: 7621
				public readonly AssemblyVersion Version;
			}
		}

		// Token: 0x0200059F RID: 1439
		[NullableContext(0)]
		[Nullable(new byte[] { 0, 1 })]
		private sealed class FrameworkRetargetingDictionary : Dictionary<DesktopAssemblyIdentityComparer.FrameworkRetargetingDictionary.Key, List<DesktopAssemblyIdentityComparer.FrameworkRetargetingDictionary.Value>>
		{
			// Token: 0x0600356A RID: 13674 RVA: 0x000965A4 File Offset: 0x000947A4
			[NullableContext(1)]
			public void Add(string name, [Nullable(0)] ImmutableArray<byte> publicKeyToken, AssemblyVersion versionLow, object versionHighNull, string newName, [Nullable(0)] ImmutableArray<byte> newPublicKeyToken, AssemblyVersion newVersion)
			{
				DesktopAssemblyIdentityComparer.FrameworkRetargetingDictionary.Key key = new DesktopAssemblyIdentityComparer.FrameworkRetargetingDictionary.Key(name, publicKeyToken);
				List<DesktopAssemblyIdentityComparer.FrameworkRetargetingDictionary.Value> list;
				if (!base.TryGetValue(key, ref list))
				{
					base.Add(key, list = new List<DesktopAssemblyIdentityComparer.FrameworkRetargetingDictionary.Value>());
				}
				list.Add(new DesktopAssemblyIdentityComparer.FrameworkRetargetingDictionary.Value(versionLow, default(AssemblyVersion), newName, newPublicKeyToken, newVersion, false));
			}

			// Token: 0x0600356B RID: 13675 RVA: 0x000965F0 File Offset: 0x000947F0
			public void Add([Nullable(1)] string name, ImmutableArray<byte> publicKeyToken, AssemblyVersion versionLow, AssemblyVersion versionHigh, [Nullable(1)] string newName, ImmutableArray<byte> newPublicKeyToken, AssemblyVersion newVersion, bool isPortable)
			{
				DesktopAssemblyIdentityComparer.FrameworkRetargetingDictionary.Key key = new DesktopAssemblyIdentityComparer.FrameworkRetargetingDictionary.Key(name, publicKeyToken);
				List<DesktopAssemblyIdentityComparer.FrameworkRetargetingDictionary.Value> list;
				if (!base.TryGetValue(key, ref list))
				{
					base.Add(key, list = new List<DesktopAssemblyIdentityComparer.FrameworkRetargetingDictionary.Value>());
				}
				list.Add(new DesktopAssemblyIdentityComparer.FrameworkRetargetingDictionary.Value(versionLow, versionHigh, newName, newPublicKeyToken, newVersion, isPortable));
			}

			// Token: 0x0600356C RID: 13676 RVA: 0x00096638 File Offset: 0x00094838
			[NullableContext(1)]
			public bool TryGetValue(AssemblyIdentity identity, out DesktopAssemblyIdentityComparer.FrameworkRetargetingDictionary.Value value)
			{
				List<DesktopAssemblyIdentityComparer.FrameworkRetargetingDictionary.Value> list;
				if (!base.TryGetValue(new DesktopAssemblyIdentityComparer.FrameworkRetargetingDictionary.Key(identity.Name, identity.PublicKeyToken), ref list))
				{
					value = default(DesktopAssemblyIdentityComparer.FrameworkRetargetingDictionary.Value);
					return false;
				}
				for (int i = 0; i < list.Count; i++)
				{
					value = list[i];
					AssemblyVersion assemblyVersion = (AssemblyVersion)identity.Version;
					if (value.VersionHigh.Major == 0)
					{
						if (assemblyVersion == value.VersionLow)
						{
							return true;
						}
					}
					else if (assemblyVersion >= value.VersionLow && assemblyVersion <= value.VersionHigh)
					{
						return true;
					}
				}
				value = default(DesktopAssemblyIdentityComparer.FrameworkRetargetingDictionary.Value);
				return false;
			}

			// Token: 0x020007B9 RID: 1977
			public struct Key : IEquatable<DesktopAssemblyIdentityComparer.FrameworkRetargetingDictionary.Key>
			{
				// Token: 0x06003FB9 RID: 16313 RVA: 0x000B2213 File Offset: 0x000B0413
				public Key([Nullable(1)] string name, ImmutableArray<byte> publicKeyToken)
				{
					this.Name = name;
					this.PublicKeyToken = publicKeyToken;
				}

				// Token: 0x06003FBA RID: 16314 RVA: 0x000B2223 File Offset: 0x000B0423
				public bool Equals(DesktopAssemblyIdentityComparer.FrameworkRetargetingDictionary.Key other)
				{
					return AssemblyIdentityComparer.SimpleNameComparer.Equals(this.Name, other.Name) && this.PublicKeyToken.SequenceEqual<byte, byte>(other.PublicKeyToken, null);
				}

				// Token: 0x06003FBB RID: 16315 RVA: 0x000B2251 File Offset: 0x000B0451
				[NullableContext(2)]
				public override bool Equals(object obj)
				{
					return obj is DesktopAssemblyIdentityComparer.FrameworkRetargetingDictionary.Key && this.Equals((DesktopAssemblyIdentityComparer.FrameworkRetargetingDictionary.Key)obj);
				}

				// Token: 0x06003FBC RID: 16316 RVA: 0x000B226C File Offset: 0x000B046C
				public override int GetHashCode()
				{
					return AssemblyIdentityComparer.SimpleNameComparer.GetHashCode(this.Name) ^ (int)this.PublicKeyToken[0];
				}

				// Token: 0x04001DC6 RID: 7622
				[Nullable(1)]
				public readonly string Name;

				// Token: 0x04001DC7 RID: 7623
				public readonly ImmutableArray<byte> PublicKeyToken;
			}

			// Token: 0x020007BA RID: 1978
			public struct Value
			{
				// Token: 0x06003FBD RID: 16317 RVA: 0x000B2299 File Offset: 0x000B0499
				public Value(AssemblyVersion versionLow, AssemblyVersion versionHigh, [Nullable(1)] string newName, ImmutableArray<byte> newPublicKeyToken, AssemblyVersion newVersion, bool isPortable)
				{
					this.VersionLow = versionLow;
					this.VersionHigh = versionHigh;
					this.NewName = newName;
					this.NewPublicKeyToken = newPublicKeyToken;
					this.NewVersion = newVersion;
					this.IsPortable = isPortable;
				}

				// Token: 0x04001DC8 RID: 7624
				public readonly AssemblyVersion VersionLow;

				// Token: 0x04001DC9 RID: 7625
				public readonly AssemblyVersion VersionHigh;

				// Token: 0x04001DCA RID: 7626
				[Nullable(1)]
				public readonly string NewName;

				// Token: 0x04001DCB RID: 7627
				public readonly ImmutableArray<byte> NewPublicKeyToken;

				// Token: 0x04001DCC RID: 7628
				public readonly AssemblyVersion NewVersion;

				// Token: 0x04001DCD RID: 7629
				public readonly bool IsPortable;
			}
		}
	}
}
