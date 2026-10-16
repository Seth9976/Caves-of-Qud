using System;

namespace System.Diagnostics.Contracts
{
	/// <summary>Enables you to set contract and tool options at assembly, type, or method granularity.</summary>
	// Token: 0x020009CE RID: 2510
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
	[Conditional("CONTRACTS_FULL")]
	public sealed class ContractOptionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Contracts.ContractOptionAttribute" /> class by using the provided category, setting, and enable/disable value.</summary>
		/// <param name="category">The category for the option to be set.</param>
		/// <param name="setting">The option setting.</param>
		/// <param name="enabled">true to enable the option; false to disable the option.</param>
		// Token: 0x06005A0C RID: 23052 RVA: 0x00133DC7 File Offset: 0x00131FC7
		public ContractOptionAttribute(string category, string setting, bool enabled)
		{
			this._category = category;
			this._setting = setting;
			this._enabled = enabled;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Contracts.ContractOptionAttribute" /> class by using the provided category, setting, and value.</summary>
		/// <param name="category">The category of the option to be set.</param>
		/// <param name="setting">The option setting.</param>
		/// <param name="value">The value for the setting.</param>
		// Token: 0x06005A0D RID: 23053 RVA: 0x00133DE4 File Offset: 0x00131FE4
		public ContractOptionAttribute(string category, string setting, string value)
		{
			this._category = category;
			this._setting = setting;
			this._value = value;
		}

		/// <summary>Gets the category of the option.</summary>
		/// <returns>The category of the option.</returns>
		// Token: 0x17000F53 RID: 3923
		// (get) Token: 0x06005A0E RID: 23054 RVA: 0x00133E01 File Offset: 0x00132001
		public string Category
		{
			get
			{
				return this._category;
			}
		}

		/// <summary>Gets the setting for the option.</summary>
		/// <returns>The setting for the option.</returns>
		// Token: 0x17000F54 RID: 3924
		// (get) Token: 0x06005A0F RID: 23055 RVA: 0x00133E09 File Offset: 0x00132009
		public string Setting
		{
			get
			{
				return this._setting;
			}
		}

		/// <summary>Determines if an option is enabled.</summary>
		/// <returns>true if the option is enabled; otherwise, false.</returns>
		// Token: 0x17000F55 RID: 3925
		// (get) Token: 0x06005A10 RID: 23056 RVA: 0x00133E11 File Offset: 0x00132011
		public bool Enabled
		{
			get
			{
				return this._enabled;
			}
		}

		/// <summary>Gets the value for the option.</summary>
		/// <returns>The value for the option.</returns>
		// Token: 0x17000F56 RID: 3926
		// (get) Token: 0x06005A11 RID: 23057 RVA: 0x00133E19 File Offset: 0x00132019
		public string Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x040037A7 RID: 14247
		private string _category;

		// Token: 0x040037A8 RID: 14248
		private string _setting;

		// Token: 0x040037A9 RID: 14249
		private bool _enabled;

		// Token: 0x040037AA RID: 14250
		private string _value;
	}
}
