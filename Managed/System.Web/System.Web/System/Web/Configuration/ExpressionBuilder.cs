using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Retrieves a dynamic resource during compilation.</summary>
	// Token: 0x02000597 RID: 1431
	public sealed class ExpressionBuilder : ConfigurationElement
	{
		// Token: 0x06003C9F RID: 15519 RVA: 0x0009F1A4 File Offset: 0x0009D3A4
		static ExpressionBuilder()
		{
			ExpressionBuilder.properties.Add(ExpressionBuilder.expressionPrefixProp);
			ExpressionBuilder.properties.Add(ExpressionBuilder.typeProp);
		}

		// Token: 0x06003CA0 RID: 15520 RVA: 0x0009CC6D File Offset: 0x0009AE6D
		internal ExpressionBuilder()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.ExpressionBuilder" /> class. </summary>
		/// <param name="expressionPrefix">A string that identifies the type of expression to retrieve.</param>
		/// <param name="theType">A string that specifies the expression type.</param>
		// Token: 0x06003CA1 RID: 15521 RVA: 0x0009F23F File Offset: 0x0009D43F
		public ExpressionBuilder(string expressionPrefix, string theType)
		{
			this.ExpressionPrefix = expressionPrefix;
			this.Type = theType;
		}

		/// <summary>Gets or sets a string that identifies the type of expression to retrieve.</summary>
		/// <returns>A string that identifies the type of expression to retrieve.</returns>
		// Token: 0x170012BF RID: 4799
		// (get) Token: 0x06003CA2 RID: 15522 RVA: 0x0009F255 File Offset: 0x0009D455
		// (set) Token: 0x06003CA3 RID: 15523 RVA: 0x0009F267 File Offset: 0x0009D467
		[StringValidator(MinLength = 1)]
		[ConfigurationProperty("expressionPrefix", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		public string ExpressionPrefix
		{
			get
			{
				return (string)base[ExpressionBuilder.expressionPrefixProp];
			}
			set
			{
				base[ExpressionBuilder.expressionPrefixProp] = value;
			}
		}

		/// <summary>Gets or sets a string that specifies the expression type.</summary>
		/// <returns>A string that specifies the expression type.</returns>
		// Token: 0x170012C0 RID: 4800
		// (get) Token: 0x06003CA4 RID: 15524 RVA: 0x0009F275 File Offset: 0x0009D475
		// (set) Token: 0x06003CA5 RID: 15525 RVA: 0x0009F287 File Offset: 0x0009D487
		[StringValidator(MinLength = 1)]
		[ConfigurationProperty("type", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired)]
		public string Type
		{
			get
			{
				return (string)base[ExpressionBuilder.typeProp];
			}
			set
			{
				base[ExpressionBuilder.typeProp] = value;
			}
		}

		// Token: 0x170012C1 RID: 4801
		// (get) Token: 0x06003CA6 RID: 15526 RVA: 0x0009F295 File Offset: 0x0009D495
		internal Type TypeInternal
		{
			get
			{
				return global::System.Type.GetType(this.Type, true);
			}
		}

		// Token: 0x170012C2 RID: 4802
		// (get) Token: 0x06003CA7 RID: 15527 RVA: 0x0009F2A3 File Offset: 0x0009D4A3
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ExpressionBuilder.properties;
			}
		}

		// Token: 0x040020D8 RID: 8408
		private static ConfigurationProperty expressionPrefixProp = new ConfigurationProperty("expressionPrefix", typeof(string), "", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x040020D9 RID: 8409
		private static ConfigurationProperty typeProp = new ConfigurationProperty("type", typeof(string), "", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x040020DA RID: 8410
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
