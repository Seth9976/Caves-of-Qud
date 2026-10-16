using System;
using System.Collections.Generic;
using System.Reflection;

namespace System.CodeDom.Compiler
{
	/// <summary>Represents the configuration settings of a language provider. This class cannot be inherited.</summary>
	// Token: 0x0200034E RID: 846
	public sealed class CompilerInfo
	{
		// Token: 0x06001BD1 RID: 7121 RVA: 0x0006629C File Offset: 0x0006449C
		private CompilerInfo()
		{
		}

		/// <summary>Gets the language names supported by the language provider.</summary>
		/// <returns>An array of language names supported by the language provider.</returns>
		// Token: 0x06001BD2 RID: 7122 RVA: 0x000662AF File Offset: 0x000644AF
		public string[] GetLanguages()
		{
			return this.CloneCompilerLanguages();
		}

		/// <summary>Returns the file name extensions supported by the language provider.</summary>
		/// <returns>An array of file name extensions supported by the language provider.</returns>
		// Token: 0x06001BD3 RID: 7123 RVA: 0x000662B7 File Offset: 0x000644B7
		public string[] GetExtensions()
		{
			return this.CloneCompilerExtensions();
		}

		/// <summary>Gets the type of the configured <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> implementation.</summary>
		/// <returns>A read-only <see cref="T:System.Type" /> instance that represents the configured language provider type.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationException">The language provider is not configured on this computer. </exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">Cannot locate the type because it is a null or empty string.-or-Cannot locate the type because the name for the <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> cannot be found in the configuration file.</exception>
		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06001BD4 RID: 7124 RVA: 0x000662C0 File Offset: 0x000644C0
		public Type CodeDomProviderType
		{
			get
			{
				if (this._type == null)
				{
					lock (this)
					{
						if (this._type == null)
						{
							this._type = Type.GetType(this._codeDomProviderTypeName);
						}
					}
				}
				return this._type;
			}
		}

		/// <summary>Returns a value indicating whether the language provider implementation is configured on the computer.</summary>
		/// <returns>true if the language provider implementation type is configured on the computer; otherwise, false.</returns>
		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06001BD5 RID: 7125 RVA: 0x00066328 File Offset: 0x00064528
		public bool IsCodeDomProviderTypeValid
		{
			get
			{
				return Type.GetType(this._codeDomProviderTypeName) != null;
			}
		}

		/// <summary>Returns a <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> instance for the current language provider settings.</summary>
		/// <returns>A CodeDOM provider associated with the language provider configuration. </returns>
		// Token: 0x06001BD6 RID: 7126 RVA: 0x0006633C File Offset: 0x0006453C
		public CodeDomProvider CreateProvider()
		{
			if (this._providerOptions.Count > 0)
			{
				ConstructorInfo constructor = this.CodeDomProviderType.GetConstructor(new Type[] { typeof(IDictionary<string, string>) });
				if (constructor != null)
				{
					return (CodeDomProvider)constructor.Invoke(new object[] { this._providerOptions });
				}
			}
			return (CodeDomProvider)Activator.CreateInstance(this.CodeDomProviderType);
		}

		/// <summary>Returns a <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> instance for the current language provider settings and specified options.</summary>
		/// <returns>A CodeDOM provider associated with the language provider configuration and specified options.</returns>
		/// <param name="providerOptions">A collection of provider options from the configuration file.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="providerOptions " />is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The provider does not support options.</exception>
		// Token: 0x06001BD7 RID: 7127 RVA: 0x000663AC File Offset: 0x000645AC
		public CodeDomProvider CreateProvider(IDictionary<string, string> providerOptions)
		{
			if (providerOptions == null)
			{
				throw new ArgumentNullException("providerOptions");
			}
			ConstructorInfo constructor = this.CodeDomProviderType.GetConstructor(new Type[] { typeof(IDictionary<string, string>) });
			if (constructor != null)
			{
				return (CodeDomProvider)constructor.Invoke(new object[] { providerOptions });
			}
			throw new InvalidOperationException(SR.Format("This CodeDomProvider type does not have a constructor that takes providerOptions - \"{0}\"", this.CodeDomProviderType.ToString()));
		}

		/// <summary>Gets the configured compiler settings for the language provider implementation.</summary>
		/// <returns>A read-only <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> instance that contains the compiler options and settings configured for the language provider. </returns>
		// Token: 0x06001BD8 RID: 7128 RVA: 0x0006641F File Offset: 0x0006461F
		public CompilerParameters CreateDefaultCompilerParameters()
		{
			return this.CloneCompilerParameters();
		}

		// Token: 0x06001BD9 RID: 7129 RVA: 0x00066427 File Offset: 0x00064627
		internal CompilerInfo(CompilerParameters compilerParams, string codeDomProviderTypeName, string[] compilerLanguages, string[] compilerExtensions)
		{
			this._compilerLanguages = compilerLanguages;
			this._compilerExtensions = compilerExtensions;
			this._codeDomProviderTypeName = codeDomProviderTypeName;
			this._compilerParams = compilerParams ?? new CompilerParameters();
		}

		// Token: 0x06001BDA RID: 7130 RVA: 0x00066460 File Offset: 0x00064660
		internal CompilerInfo(CompilerParameters compilerParams, string codeDomProviderTypeName)
		{
			this._codeDomProviderTypeName = codeDomProviderTypeName;
			this._compilerParams = compilerParams ?? new CompilerParameters();
		}

		/// <summary>Returns the hash code for the current instance.</summary>
		/// <returns>A 32-bit signed integer hash code for the current <see cref="T:System.CodeDom.Compiler.CompilerInfo" /> instance, suitable for use in hashing algorithms and data structures such as a hash table. </returns>
		// Token: 0x06001BDB RID: 7131 RVA: 0x0006648A File Offset: 0x0006468A
		public override int GetHashCode()
		{
			return this._codeDomProviderTypeName.GetHashCode();
		}

		/// <summary>Determines whether the specified object represents the same language provider and compiler settings as the current <see cref="T:System.CodeDom.Compiler.CompilerInfo" />.</summary>
		/// <returns>true if <paramref name="o" /> is a <see cref="T:System.CodeDom.Compiler.CompilerInfo" /> object and its value is the same as this instance; otherwise, false.</returns>
		/// <param name="o">The object to compare with the current <see cref="T:System.CodeDom.Compiler.CompilerInfo" />. </param>
		// Token: 0x06001BDC RID: 7132 RVA: 0x00066498 File Offset: 0x00064698
		public override bool Equals(object o)
		{
			CompilerInfo compilerInfo = o as CompilerInfo;
			return compilerInfo != null && (this.CodeDomProviderType == compilerInfo.CodeDomProviderType && this.CompilerParams.WarningLevel == compilerInfo.CompilerParams.WarningLevel && this.CompilerParams.IncludeDebugInformation == compilerInfo.CompilerParams.IncludeDebugInformation) && this.CompilerParams.CompilerOptions == compilerInfo.CompilerParams.CompilerOptions;
		}

		// Token: 0x06001BDD RID: 7133 RVA: 0x00066514 File Offset: 0x00064714
		private CompilerParameters CloneCompilerParameters()
		{
			return new CompilerParameters
			{
				IncludeDebugInformation = this._compilerParams.IncludeDebugInformation,
				TreatWarningsAsErrors = this._compilerParams.TreatWarningsAsErrors,
				WarningLevel = this._compilerParams.WarningLevel,
				CompilerOptions = this._compilerParams.CompilerOptions
			};
		}

		// Token: 0x06001BDE RID: 7134 RVA: 0x0006656A File Offset: 0x0006476A
		private string[] CloneCompilerLanguages()
		{
			return (string[])this._compilerLanguages.Clone();
		}

		// Token: 0x06001BDF RID: 7135 RVA: 0x0006657C File Offset: 0x0006477C
		private string[] CloneCompilerExtensions()
		{
			return (string[])this._compilerExtensions.Clone();
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06001BE0 RID: 7136 RVA: 0x0006658E File Offset: 0x0006478E
		internal CompilerParameters CompilerParams
		{
			get
			{
				return this._compilerParams;
			}
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06001BE1 RID: 7137 RVA: 0x00066596 File Offset: 0x00064796
		internal IDictionary<string, string> ProviderOptions
		{
			get
			{
				return this._providerOptions;
			}
		}

		// Token: 0x04000E51 RID: 3665
		internal readonly IDictionary<string, string> _providerOptions = new Dictionary<string, string>();

		// Token: 0x04000E52 RID: 3666
		internal string _codeDomProviderTypeName;

		// Token: 0x04000E53 RID: 3667
		internal CompilerParameters _compilerParams;

		// Token: 0x04000E54 RID: 3668
		internal string[] _compilerLanguages;

		// Token: 0x04000E55 RID: 3669
		internal string[] _compilerExtensions;

		// Token: 0x04000E56 RID: 3670
		private Type _type;
	}
}
