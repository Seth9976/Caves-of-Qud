using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace Mono.Cecil
{
	// Token: 0x020001B8 RID: 440
	public class AssemblyNameReference : IMetadataScope, IMetadataTokenProvider
	{
		// Token: 0x17000211 RID: 529
		// (get) Token: 0x060007D6 RID: 2006 RVA: 0x0001AEDC File Offset: 0x000190DC
		// (set) Token: 0x060007D7 RID: 2007 RVA: 0x0001AEE4 File Offset: 0x000190E4
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
				this.full_name = null;
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x060007D8 RID: 2008 RVA: 0x0001AEF4 File Offset: 0x000190F4
		// (set) Token: 0x060007D9 RID: 2009 RVA: 0x0001AEFC File Offset: 0x000190FC
		public string Culture
		{
			get
			{
				return this.culture;
			}
			set
			{
				this.culture = value;
				this.full_name = null;
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x060007DA RID: 2010 RVA: 0x0001AF0C File Offset: 0x0001910C
		// (set) Token: 0x060007DB RID: 2011 RVA: 0x0001AF14 File Offset: 0x00019114
		public Version Version
		{
			get
			{
				return this.version;
			}
			set
			{
				this.version = Mixin.CheckVersion(value);
				this.full_name = null;
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x060007DC RID: 2012 RVA: 0x0001AF29 File Offset: 0x00019129
		// (set) Token: 0x060007DD RID: 2013 RVA: 0x0001AF31 File Offset: 0x00019131
		public AssemblyAttributes Attributes
		{
			get
			{
				return (AssemblyAttributes)this.attributes;
			}
			set
			{
				this.attributes = (uint)value;
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x060007DE RID: 2014 RVA: 0x0001AF3A File Offset: 0x0001913A
		// (set) Token: 0x060007DF RID: 2015 RVA: 0x0001AF48 File Offset: 0x00019148
		public bool HasPublicKey
		{
			get
			{
				return this.attributes.GetAttributes(1U);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(1U, value);
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x060007E0 RID: 2016 RVA: 0x0001AF5D File Offset: 0x0001915D
		// (set) Token: 0x060007E1 RID: 2017 RVA: 0x0001AF6B File Offset: 0x0001916B
		public bool IsSideBySideCompatible
		{
			get
			{
				return this.attributes.GetAttributes(0U);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(0U, value);
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x060007E2 RID: 2018 RVA: 0x0001AF80 File Offset: 0x00019180
		// (set) Token: 0x060007E3 RID: 2019 RVA: 0x0001AF92 File Offset: 0x00019192
		public bool IsRetargetable
		{
			get
			{
				return this.attributes.GetAttributes(256U);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(256U, value);
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x060007E4 RID: 2020 RVA: 0x0001AFAB File Offset: 0x000191AB
		// (set) Token: 0x060007E5 RID: 2021 RVA: 0x0001AFBD File Offset: 0x000191BD
		public bool IsWindowsRuntime
		{
			get
			{
				return this.attributes.GetAttributes(512U);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(512U, value);
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x060007E6 RID: 2022 RVA: 0x0001AFD6 File Offset: 0x000191D6
		// (set) Token: 0x060007E7 RID: 2023 RVA: 0x0001AFE7 File Offset: 0x000191E7
		public byte[] PublicKey
		{
			get
			{
				return this.public_key ?? Empty<byte>.Array;
			}
			set
			{
				this.public_key = value;
				this.HasPublicKey = !this.public_key.IsNullOrEmpty<byte>();
				this.public_key_token = null;
				this.full_name = null;
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x060007E8 RID: 2024 RVA: 0x0001B014 File Offset: 0x00019214
		// (set) Token: 0x060007E9 RID: 2025 RVA: 0x0001B077 File Offset: 0x00019277
		public byte[] PublicKeyToken
		{
			get
			{
				if (this.public_key_token == null && !this.public_key.IsNullOrEmpty<byte>())
				{
					byte[] array = this.HashPublicKey();
					byte[] array2 = new byte[8];
					Array.Copy(array, array.Length - 8, array2, 0, 8);
					Array.Reverse(array2, 0, 8);
					Interlocked.CompareExchange<byte[]>(ref this.public_key_token, array2, null);
				}
				return this.public_key_token ?? Empty<byte>.Array;
			}
			set
			{
				this.public_key_token = value;
				this.full_name = null;
			}
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x0001B088 File Offset: 0x00019288
		private byte[] HashPublicKey()
		{
			HashAlgorithm hashAlgorithm;
			if (this.hash_algorithm == AssemblyHashAlgorithm.MD5)
			{
				hashAlgorithm = MD5.Create();
			}
			else
			{
				hashAlgorithm = SHA1.Create();
			}
			byte[] array;
			using (hashAlgorithm)
			{
				array = hashAlgorithm.ComputeHash(this.public_key);
			}
			return array;
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x060007EB RID: 2027 RVA: 0x00018318 File Offset: 0x00016518
		public virtual MetadataScopeType MetadataScopeType
		{
			get
			{
				return MetadataScopeType.AssemblyNameReference;
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x060007EC RID: 2028 RVA: 0x0001B0DC File Offset: 0x000192DC
		public string FullName
		{
			get
			{
				if (this.full_name != null)
				{
					return this.full_name;
				}
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(this.name);
				stringBuilder.Append(", ");
				stringBuilder.Append("Version=");
				stringBuilder.Append(this.version.ToString(4));
				stringBuilder.Append(", ");
				stringBuilder.Append("Culture=");
				stringBuilder.Append(string.IsNullOrEmpty(this.culture) ? "neutral" : this.culture);
				stringBuilder.Append(", ");
				stringBuilder.Append("PublicKeyToken=");
				byte[] publicKeyToken = this.PublicKeyToken;
				if (!publicKeyToken.IsNullOrEmpty<byte>() && publicKeyToken.Length != 0)
				{
					for (int i = 0; i < publicKeyToken.Length; i++)
					{
						stringBuilder.Append(publicKeyToken[i].ToString("x2"));
					}
				}
				else
				{
					stringBuilder.Append("null");
				}
				if (this.IsRetargetable)
				{
					stringBuilder.Append(", ");
					stringBuilder.Append("Retargetable=Yes");
				}
				Interlocked.CompareExchange<string>(ref this.full_name, stringBuilder.ToString(), null);
				return this.full_name;
			}
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x0001B208 File Offset: 0x00019408
		public static AssemblyNameReference Parse(string fullName)
		{
			if (fullName == null)
			{
				throw new ArgumentNullException("fullName");
			}
			if (fullName.Length == 0)
			{
				throw new ArgumentException("Name can not be empty");
			}
			AssemblyNameReference assemblyNameReference = new AssemblyNameReference();
			string[] array = fullName.Split(new char[] { ',' });
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i].Trim();
				if (i == 0)
				{
					assemblyNameReference.Name = text;
				}
				else
				{
					string[] array2 = text.Split(new char[] { '=' });
					if (array2.Length != 2)
					{
						throw new ArgumentException("Malformed name");
					}
					string text2 = array2[0].ToLowerInvariant();
					if (!(text2 == "version"))
					{
						if (!(text2 == "culture"))
						{
							if (text2 == "publickeytoken")
							{
								string text3 = array2[1];
								if (!(text3 == "null"))
								{
									assemblyNameReference.PublicKeyToken = new byte[text3.Length / 2];
									for (int j = 0; j < assemblyNameReference.PublicKeyToken.Length; j++)
									{
										assemblyNameReference.PublicKeyToken[j] = byte.Parse(text3.Substring(j * 2, 2), NumberStyles.HexNumber);
									}
								}
							}
						}
						else
						{
							assemblyNameReference.Culture = ((array2[1] == "neutral") ? "" : array2[1]);
						}
					}
					else
					{
						assemblyNameReference.Version = new Version(array2[1]);
					}
				}
			}
			return assemblyNameReference;
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x060007EE RID: 2030 RVA: 0x0001B36F File Offset: 0x0001956F
		// (set) Token: 0x060007EF RID: 2031 RVA: 0x0001B377 File Offset: 0x00019577
		public AssemblyHashAlgorithm HashAlgorithm
		{
			get
			{
				return this.hash_algorithm;
			}
			set
			{
				this.hash_algorithm = value;
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x060007F0 RID: 2032 RVA: 0x0001B380 File Offset: 0x00019580
		// (set) Token: 0x060007F1 RID: 2033 RVA: 0x0001B388 File Offset: 0x00019588
		public virtual byte[] Hash
		{
			get
			{
				return this.hash;
			}
			set
			{
				this.hash = value;
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x060007F2 RID: 2034 RVA: 0x0001B391 File Offset: 0x00019591
		// (set) Token: 0x060007F3 RID: 2035 RVA: 0x0001B399 File Offset: 0x00019599
		public MetadataToken MetadataToken
		{
			get
			{
				return this.token;
			}
			set
			{
				this.token = value;
			}
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x0001B3A2 File Offset: 0x000195A2
		internal AssemblyNameReference()
		{
			this.version = Mixin.ZeroVersion;
			this.token = new MetadataToken(TokenType.AssemblyRef);
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x0001B3C5 File Offset: 0x000195C5
		public AssemblyNameReference(string name, Version version)
		{
			Mixin.CheckName(name);
			this.name = name;
			this.version = Mixin.CheckVersion(version);
			this.hash_algorithm = AssemblyHashAlgorithm.None;
			this.token = new MetadataToken(TokenType.AssemblyRef);
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x0001B3FD File Offset: 0x000195FD
		public override string ToString()
		{
			return this.FullName;
		}

		// Token: 0x04000284 RID: 644
		private string name;

		// Token: 0x04000285 RID: 645
		private string culture;

		// Token: 0x04000286 RID: 646
		private Version version;

		// Token: 0x04000287 RID: 647
		private uint attributes;

		// Token: 0x04000288 RID: 648
		private byte[] public_key;

		// Token: 0x04000289 RID: 649
		private byte[] public_key_token;

		// Token: 0x0400028A RID: 650
		private AssemblyHashAlgorithm hash_algorithm;

		// Token: 0x0400028B RID: 651
		private byte[] hash;

		// Token: 0x0400028C RID: 652
		internal MetadataToken token;

		// Token: 0x0400028D RID: 653
		private string full_name;
	}
}
