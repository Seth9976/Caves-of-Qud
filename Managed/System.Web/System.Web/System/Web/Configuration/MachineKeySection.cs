using System;
using System.ComponentModel;
using System.Configuration;
using System.Security.Cryptography;
using System.Web.Util;
using Unity;

namespace System.Web.Configuration
{
	/// <summary>Defines the configuration settings that control the key generation and algorithms that are used in encryption, decryption, and message authentication code (MAC) operations in Windows Forms authentication, view-state validation, and session-state application isolation. This class cannot be inherited.</summary>
	// Token: 0x020005B4 RID: 1460
	public sealed class MachineKeySection : ConfigurationSection
	{
		// Token: 0x06003E85 RID: 16005 RVA: 0x000A39F8 File Offset: 0x000A1BF8
		static MachineKeySection()
		{
			MachineKeySection.properties.Add(MachineKeySection.decryptionProp);
			MachineKeySection.properties.Add(MachineKeySection.decryptionKeyProp);
			MachineKeySection.properties.Add(MachineKeySection.validationProp);
			MachineKeySection.properties.Add(MachineKeySection.validationKeyProp);
			MachineKeySection.Config.AutoGenerate(MachineKeyRegistryStorage.KeyType.Encryption);
			MachineKeySection.Config.AutoGenerate(MachineKeyRegistryStorage.KeyType.Validation);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.MachineKeySection" /> class by using default settings.</summary>
		// Token: 0x06003E86 RID: 16006 RVA: 0x000A3B11 File Offset: 0x000A1D11
		public MachineKeySection()
		{
			this.validation = (MachineKeyValidation)MachineKeySection.converter.ConvertFrom(null, null, this.ValidationAlgorithm);
		}

		/// <summary>Gets or sets a value that specifies whether upgraded encryption methods for view state that were introduced after the .NET Framework version 2.0 Service Pack 1 release are used.</summary>
		/// <returns>A value that indicates whether encryption methods that were introduced after the .NET Framework 2.0 SP1 release are used. </returns>
		// Token: 0x170013B1 RID: 5041
		// (get) Token: 0x06003E87 RID: 16007 RVA: 0x000A3B36 File Offset: 0x000A1D36
		// (set) Token: 0x06003E88 RID: 16008 RVA: 0x000A3B3E File Offset: 0x000A1D3E
		[global::System.MonoTODO]
		public MachineKeyCompatibilityMode CompatibilityMode { get; set; }

		// Token: 0x06003E89 RID: 16009 RVA: 0x000A3B47 File Offset: 0x000A1D47
		protected internal override void Reset(ConfigurationElement parentElement)
		{
			base.Reset(parentElement);
			this.decryption_key = null;
			this.validation_key = null;
			this.decryption_template = null;
			this.validation_template = null;
		}

		/// <summary>Specifies the encryption algorithm that is used for encrypting and decrypting forms authentication data. </summary>
		/// <returns>A value that indicates the algorithm that is used to encrypt and decrypt forms authentication data. (For information about how to specify the algorithm that is used when view state is encrypted, see the <see cref="P:System.Web.Configuration.MachineKeySection.Validation" /> property.) Auto is the default value.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The selected value is not one of the decryption values.</exception>
		// Token: 0x170013B2 RID: 5042
		// (get) Token: 0x06003E8A RID: 16010 RVA: 0x000A3B6C File Offset: 0x000A1D6C
		// (set) Token: 0x06003E8B RID: 16011 RVA: 0x000A3B7E File Offset: 0x000A1D7E
		[TypeConverter(typeof(WhiteSpaceTrimStringConverter))]
		[StringValidator(MinLength = 1)]
		[ConfigurationProperty("decryption", DefaultValue = "Auto")]
		public string Decryption
		{
			get
			{
				return (string)base[MachineKeySection.decryptionProp];
			}
			set
			{
				this.decryption_template = MachineKeySectionUtils.GetDecryptionAlgorithm(value);
				base[MachineKeySection.decryptionProp] = value;
			}
		}

		/// <summary>Gets or sets the key that is used to encrypt and decrypt data, or the process by which the key is generated. </summary>
		/// <returns>A key value, or a value that indicates how the key is generated. The default is "AutoGenerate,IsolateApps".</returns>
		// Token: 0x170013B3 RID: 5043
		// (get) Token: 0x06003E8C RID: 16012 RVA: 0x000A3B98 File Offset: 0x000A1D98
		// (set) Token: 0x06003E8D RID: 16013 RVA: 0x000A3BAA File Offset: 0x000A1DAA
		[ConfigurationProperty("decryptionKey", DefaultValue = "AutoGenerate,IsolateApps")]
		[TypeConverter(typeof(WhiteSpaceTrimStringConverter))]
		[StringValidator(MinLength = 1)]
		public string DecryptionKey
		{
			get
			{
				return (string)base[MachineKeySection.decryptionKeyProp];
			}
			set
			{
				base[MachineKeySection.decryptionKeyProp] = value;
				this.SetDecryptionKey(value);
			}
		}

		/// <summary>Specifies the hashing algorithm that is used for validating forms authentication and view state data. </summary>
		/// <returns>A value that indicates the hashing algorithm that is used to validate forms authentication and view state data.</returns>
		// Token: 0x170013B4 RID: 5044
		// (get) Token: 0x06003E8E RID: 16014 RVA: 0x000A3BBF File Offset: 0x000A1DBF
		// (set) Token: 0x06003E8F RID: 16015 RVA: 0x000A3BC8 File Offset: 0x000A1DC8
		public MachineKeyValidation Validation
		{
			get
			{
				return this.validation;
			}
			set
			{
				if (value == MachineKeyValidation.Custom)
				{
					throw new ArgumentException();
				}
				string text = value.ToString();
				this.ValidationAlgorithm = ((text == "TripleDES") ? "3DES" : text);
			}
		}

		/// <summary>Gets or sets the name of a custom algorithm that is used to validate forms authentication and view state data.</summary>
		/// <returns>A string that contains the name of a predefined algorithm or the name of a custom algorithm. </returns>
		// Token: 0x170013B5 RID: 5045
		// (get) Token: 0x06003E90 RID: 16016 RVA: 0x000A3C08 File Offset: 0x000A1E08
		// (set) Token: 0x06003E91 RID: 16017 RVA: 0x000A3C1A File Offset: 0x000A1E1A
		[TypeConverter(typeof(WhiteSpaceTrimStringConverter))]
		[StringValidator(MinLength = 1)]
		[ConfigurationProperty("validation", DefaultValue = "HMACSHA256")]
		public string ValidationAlgorithm
		{
			get
			{
				return (string)base[MachineKeySection.validationProp];
			}
			set
			{
				if (value == null)
				{
					return;
				}
				if (value.StartsWith("alg:"))
				{
					this.validation = MachineKeyValidation.Custom;
				}
				else
				{
					this.validation = (MachineKeyValidation)MachineKeySection.converter.ConvertFrom(null, null, value);
				}
				base[MachineKeySection.validationProp] = value;
			}
		}

		/// <summary>Gets or sets the key that is used to validate forms authentication and view state data, or the process by which the key is generated. </summary>
		/// <returns>A key value, or a value that indicates how the key is generated. The default is "AutoGenerate,IsolateApps".</returns>
		// Token: 0x170013B6 RID: 5046
		// (get) Token: 0x06003E92 RID: 16018 RVA: 0x000A3C5A File Offset: 0x000A1E5A
		// (set) Token: 0x06003E93 RID: 16019 RVA: 0x000A3C6C File Offset: 0x000A1E6C
		[ConfigurationProperty("validationKey", DefaultValue = "AutoGenerate,IsolateApps")]
		[TypeConverter(typeof(WhiteSpaceTrimStringConverter))]
		[StringValidator(MinLength = 1)]
		public string ValidationKey
		{
			get
			{
				return (string)base[MachineKeySection.validationKeyProp];
			}
			set
			{
				base[MachineKeySection.validationKeyProp] = value;
				this.SetValidationKey(value);
			}
		}

		// Token: 0x170013B7 RID: 5047
		// (get) Token: 0x06003E94 RID: 16020 RVA: 0x000A3C81 File Offset: 0x000A1E81
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return MachineKeySection.properties;
			}
		}

		// Token: 0x170013B8 RID: 5048
		// (get) Token: 0x06003E95 RID: 16021 RVA: 0x000A3C88 File Offset: 0x000A1E88
		internal static MachineKeySection Config
		{
			get
			{
				return WebConfigurationManager.GetSection("system.web/machineKey") as MachineKeySection;
			}
		}

		// Token: 0x06003E96 RID: 16022 RVA: 0x000A3C99 File Offset: 0x000A1E99
		internal SymmetricAlgorithm GetDecryptionAlgorithm()
		{
			return MachineKeySectionUtils.GetDecryptionAlgorithm(this.Decryption);
		}

		// Token: 0x170013B9 RID: 5049
		// (get) Token: 0x06003E97 RID: 16023 RVA: 0x000A3CA6 File Offset: 0x000A1EA6
		private SymmetricAlgorithm DecryptionTemplate
		{
			get
			{
				if (this.decryption_template == null)
				{
					this.decryption_template = this.GetDecryptionAlgorithm();
				}
				return this.decryption_template;
			}
		}

		// Token: 0x06003E98 RID: 16024 RVA: 0x000A3CC2 File Offset: 0x000A1EC2
		internal byte[] GetDecryptionKey()
		{
			if (this.decryption_key == null)
			{
				this.SetDecryptionKey(this.DecryptionKey);
			}
			return this.decryption_key;
		}

		// Token: 0x06003E99 RID: 16025 RVA: 0x000A3CE0 File Offset: 0x000A1EE0
		private void SetDecryptionKey(string key)
		{
			if (key == null || key.StartsWith("AutoGenerate"))
			{
				this.decryption_key = this.AutoGenerate(MachineKeyRegistryStorage.KeyType.Encryption);
				return;
			}
			try
			{
				this.decryption_key = MachineKeySectionUtils.GetBytes(key, key.Length);
				this.DecryptionTemplate.Key = this.decryption_key;
			}
			catch
			{
				this.decryption_key = null;
				throw new ArgumentException("Invalid key length");
			}
		}

		// Token: 0x06003E9A RID: 16026 RVA: 0x000A3D54 File Offset: 0x000A1F54
		internal KeyedHashAlgorithm GetValidationAlgorithm()
		{
			return MachineKeySectionUtils.GetValidationAlgorithm(this);
		}

		// Token: 0x170013BA RID: 5050
		// (get) Token: 0x06003E9B RID: 16027 RVA: 0x000A3D5C File Offset: 0x000A1F5C
		private KeyedHashAlgorithm ValidationTemplate
		{
			get
			{
				if (this.validation_template == null)
				{
					this.validation_template = this.GetValidationAlgorithm();
				}
				return this.validation_template;
			}
		}

		// Token: 0x06003E9C RID: 16028 RVA: 0x000A3D78 File Offset: 0x000A1F78
		internal byte[] GetValidationKey()
		{
			if (this.validation_key == null)
			{
				this.SetValidationKey(this.ValidationKey);
			}
			return this.validation_key;
		}

		// Token: 0x06003E9D RID: 16029 RVA: 0x000A3D94 File Offset: 0x000A1F94
		private void SetValidationKey(string key)
		{
			if (key == null || key.StartsWith("AutoGenerate"))
			{
				this.validation_key = this.AutoGenerate(MachineKeyRegistryStorage.KeyType.Validation);
				return;
			}
			try
			{
				this.validation_key = MachineKeySectionUtils.GetBytes(key, key.Length);
				this.ValidationTemplate.Key = this.validation_key;
			}
			catch (CryptographicException)
			{
				try
				{
					byte[] array = new byte[this.ValidationTemplate.Key.Length];
					Array.Copy(this.validation_key, 0, array, 0, this.validation_key.Length);
					this.ValidationTemplate.Key = array;
					this.validation_key = array;
				}
				catch
				{
					this.validation_key = null;
					throw new ArgumentException("Invalid key length");
				}
			}
		}

		// Token: 0x06003E9E RID: 16030 RVA: 0x000A3E58 File Offset: 0x000A2058
		private byte[] AutoGenerate(MachineKeyRegistryStorage.KeyType type)
		{
			byte[] array = null;
			try
			{
				array = MachineKeyRegistryStorage.Retrieve(type);
				if (type == MachineKeyRegistryStorage.KeyType.Encryption)
				{
					this.DecryptionTemplate.Key = array;
				}
				else if (type == MachineKeyRegistryStorage.KeyType.Validation)
				{
					this.ValidationTemplate.Key = array;
				}
			}
			catch (Exception)
			{
				array = null;
			}
			if (array == null)
			{
				if (type == MachineKeyRegistryStorage.KeyType.Encryption)
				{
					array = this.DecryptionTemplate.Key;
				}
				else if (type == MachineKeyRegistryStorage.KeyType.Validation)
				{
					array = this.ValidationTemplate.Key;
				}
				MachineKeyRegistryStorage.Store(array, type);
			}
			return array;
		}

		/// <summary>Gets or sets the name of the application.</summary>
		/// <returns>The name of the application. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x170013BB RID: 5051
		// (get) Token: 0x06003E9F RID: 16031 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06003EA0 RID: 16032 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public string ApplicationName
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the name of the data protector type. The default is <see cref="F:System.String.Empty" />.</summary>
		/// <returns>The name of the data protector type.</returns>
		// Token: 0x170013BC RID: 5052
		// (get) Token: 0x06003EA1 RID: 16033 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06003EA2 RID: 16034 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public string DataProtectorType
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x0400223F RID: 8767
		private static ConfigurationProperty decryptionProp = new ConfigurationProperty("decryption", typeof(string), "Auto", PropertyHelper.WhiteSpaceTrimStringConverter, PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002240 RID: 8768
		private static ConfigurationProperty decryptionKeyProp = new ConfigurationProperty("decryptionKey", typeof(string), "AutoGenerate,IsolateApps", PropertyHelper.WhiteSpaceTrimStringConverter, PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002241 RID: 8769
		private static ConfigurationProperty validationProp = new ConfigurationProperty("validation", typeof(string), "HMACSHA256", PropertyHelper.WhiteSpaceTrimStringConverter, PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002242 RID: 8770
		private static ConfigurationProperty validationKeyProp = new ConfigurationProperty("validationKey", typeof(string), "AutoGenerate,IsolateApps", PropertyHelper.WhiteSpaceTrimStringConverter, PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002243 RID: 8771
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04002244 RID: 8772
		private static MachineKeyValidationConverter converter = new MachineKeyValidationConverter();

		// Token: 0x04002245 RID: 8773
		private MachineKeyValidation validation;

		// Token: 0x04002247 RID: 8775
		private byte[] decryption_key;

		// Token: 0x04002248 RID: 8776
		private byte[] validation_key;

		// Token: 0x04002249 RID: 8777
		private SymmetricAlgorithm decryption_template;

		// Token: 0x0400224A RID: 8778
		private KeyedHashAlgorithm validation_template;
	}
}
