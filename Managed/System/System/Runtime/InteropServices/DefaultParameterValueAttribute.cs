using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Sets the default value of a parameter when called from a language that supports default parameters. This class cannot be inherited. </summary>
	// Token: 0x02000187 RID: 391
	[AttributeUsage(AttributeTargets.Parameter)]
	public sealed class DefaultParameterValueAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.DefaultParameterValueAttribute" /> class with the default value of a parameter.</summary>
		/// <param name="value">An object that represents the default value of a parameter.</param>
		// Token: 0x06000A6D RID: 2669 RVA: 0x0002D4BD File Offset: 0x0002B6BD
		public DefaultParameterValueAttribute(object value)
		{
			this.value = value;
		}

		/// <summary>Gets the default value of a parameter.</summary>
		/// <returns>An object that represents the default value of a parameter.</returns>
		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000A6E RID: 2670 RVA: 0x0002D4CC File Offset: 0x0002B6CC
		public object Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x04000709 RID: 1801
		private object value;
	}
}
