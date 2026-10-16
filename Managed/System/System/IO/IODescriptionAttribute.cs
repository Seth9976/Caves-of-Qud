using System;
using System.ComponentModel;

namespace System.IO
{
	/// <summary>Sets the description visual designers can display when referencing an event, extender, or property.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200051A RID: 1306
	[AttributeUsage(AttributeTargets.All)]
	public class IODescriptionAttribute : DescriptionAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.IODescriptionAttribute" /> class.</summary>
		/// <param name="description">The description to use. </param>
		// Token: 0x06002A52 RID: 10834 RVA: 0x0002BAB8 File Offset: 0x00029CB8
		public IODescriptionAttribute(string description)
			: base(description)
		{
		}

		/// <summary>Gets the description.</summary>
		/// <returns>The description for the event, extender, or property.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x06002A53 RID: 10835 RVA: 0x00068003 File Offset: 0x00066203
		public override string Description
		{
			get
			{
				return base.DescriptionValue;
			}
		}
	}
}
