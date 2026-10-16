using System;
using System.Security.Cryptography;
using System.Text;

namespace Trivial.Mono.Cecil
{
	// Token: 0x02000013 RID: 19
	public class AssemblyNameReference : IMetadataScope, IMetadataTokenProvider
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00003139 File Offset: 0x00001339
		// (set) Token: 0x06000091 RID: 145 RVA: 0x00003141 File Offset: 0x00001341
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

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00003151 File Offset: 0x00001351
		// (set) Token: 0x06000093 RID: 147 RVA: 0x00003159 File Offset: 0x00001359
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

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00003169 File Offset: 0x00001369
		// (set) Token: 0x06000095 RID: 149 RVA: 0x00003171 File Offset: 0x00001371
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

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00003186 File Offset: 0x00001386
		// (set) Token: 0x06000097 RID: 151 RVA: 0x0000318E File Offset: 0x0000138E
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

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00003197 File Offset: 0x00001397
		// (set) Token: 0x06000099 RID: 153 RVA: 0x000031A5 File Offset: 0x000013A5
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

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600009A RID: 154 RVA: 0x000031BA File Offset: 0x000013BA
		// (set) Token: 0x0600009B RID: 155 RVA: 0x000031C8 File Offset: 0x000013C8
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

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600009C RID: 156 RVA: 0x000031DD File Offset: 0x000013DD
		// (set) Token: 0x0600009D RID: 157 RVA: 0x000031EF File Offset: 0x000013EF
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

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00003208 File Offset: 0x00001408
		// (set) Token: 0x0600009F RID: 159 RVA: 0x0000321A File Offset: 0x0000141A
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

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00003233 File Offset: 0x00001433
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x00003244 File Offset: 0x00001444
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
				this.public_key_token = Empty<byte>.Array;
				this.full_name = null;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00003274 File Offset: 0x00001474
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x000032D5 File Offset: 0x000014D5
		public byte[] PublicKeyToken
		{
			get
			{
				if (this.public_key_token.IsNullOrEmpty<byte>() && !this.public_key.IsNullOrEmpty<byte>())
				{
					byte[] array = this.HashPublicKey();
					byte[] array2 = new byte[8];
					Array.Copy(array, array.Length - 8, array2, 0, 8);
					Array.Reverse(array2, 0, 8);
					this.public_key_token = array2;
				}
				return this.public_key_token ?? Empty<byte>.Array;
			}
			set
			{
				this.public_key_token = value;
				this.full_name = null;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x000025EF File Offset: 0x000007EF
		public virtual MetadataScopeType MetadataScopeType
		{
			get
			{
				return MetadataScopeType.AssemblyNameReference;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x000032E8 File Offset: 0x000014E8
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
				return this.full_name = stringBuilder.ToString();
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00003409 File Offset: 0x00001609
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x00003411 File Offset: 0x00001611
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

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x0000341A File Offset: 0x0000161A
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x00003422 File Offset: 0x00001622
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

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000AA RID: 170 RVA: 0x0000342B File Offset: 0x0000162B
		// (set) Token: 0x060000AB RID: 171 RVA: 0x00003433 File Offset: 0x00001633
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

		// Token: 0x060000AC RID: 172 RVA: 0x0000343C File Offset: 0x0000163C
		private byte[] HashPublicKey()
		{
			AssemblyHashAlgorithm assemblyHashAlgorithm = this.hash_algorithm;
			HashAlgorithm hashAlgorithm = ((assemblyHashAlgorithm != AssemblyHashAlgorithm.Reserved) ? SHA1.Create() : MD5.Create());
			byte[] array;
			using (hashAlgorithm)
			{
				array = hashAlgorithm.ComputeHash(this.public_key);
			}
			return array;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003494 File Offset: 0x00001694
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
										assemblyNameReference.PublicKeyToken[j] = byte.Parse(text3.Substring(j * 2, 2), 515);
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

		// Token: 0x060000AE RID: 174 RVA: 0x000035FB File Offset: 0x000017FB
		internal AssemblyNameReference()
		{
			this.version = Mixin.ZeroVersion;
			this.token = new MetadataToken(TokenType.AssemblyRef);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x0000361E File Offset: 0x0000181E
		public AssemblyNameReference(string name, Version version)
		{
			Mixin.CheckName(name);
			this.name = name;
			this.version = Mixin.CheckVersion(version);
			this.hash_algorithm = AssemblyHashAlgorithm.None;
			this.token = new MetadataToken(TokenType.AssemblyRef);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003656 File Offset: 0x00001856
		public override string ToString()
		{
			return this.FullName;
		}

		// Token: 0x04000022 RID: 34
		private string name;

		// Token: 0x04000023 RID: 35
		private string culture;

		// Token: 0x04000024 RID: 36
		private Version version;

		// Token: 0x04000025 RID: 37
		private uint attributes;

		// Token: 0x04000026 RID: 38
		private byte[] public_key;

		// Token: 0x04000027 RID: 39
		private byte[] public_key_token;

		// Token: 0x04000028 RID: 40
		private AssemblyHashAlgorithm hash_algorithm;

		// Token: 0x04000029 RID: 41
		private byte[] hash;

		// Token: 0x0400002A RID: 42
		internal MetadataToken token;

		// Token: 0x0400002B RID: 43
		private string full_name;
	}
}
