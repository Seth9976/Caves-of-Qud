using System;
using System.ComponentModel;

namespace System.Diagnostics
{
	/// <summary>Specifies a description for a property or event.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000270 RID: 624
	[AttributeUsage(AttributeTargets.All)]
	public class MonitoringDescriptionAttribute : DescriptionAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.MonitoringDescriptionAttribute" /> class, using the specified description.</summary>
		/// <param name="description">The application-defined description text. </param>
		// Token: 0x060013A9 RID: 5033 RVA: 0x0002BAB8 File Offset: 0x00029CB8
		public MonitoringDescriptionAttribute(string description)
			: base(description)
		{
		}

		/// <summary>Gets description text associated with the item monitored.</summary>
		/// <returns>An application-defined description.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003AE RID: 942
		// (get) Token: 0x060013AA RID: 5034 RVA: 0x000522E4 File Offset: 0x000504E4
		public override string Description
		{
			get
			{
				return base.Description;
			}
		}
	}
}
