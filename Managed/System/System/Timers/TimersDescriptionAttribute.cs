using System;
using System.ComponentModel;

namespace System.Timers
{
	/// <summary>Sets the description that visual designers can display when referencing an event, extender, or property.</summary>
	// Token: 0x02000197 RID: 407
	[AttributeUsage(AttributeTargets.All)]
	public class TimersDescriptionAttribute : DescriptionAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Timers.TimersDescriptionAttribute" /> class.</summary>
		/// <param name="description">The description to use. </param>
		// Token: 0x06000AA9 RID: 2729 RVA: 0x0002BAB8 File Offset: 0x00029CB8
		public TimersDescriptionAttribute(string description)
			: base(description)
		{
		}

		/// <summary>Gets the description that visual designers can display when referencing an event, extender, or property.</summary>
		/// <returns>The description for the event, extender, or property.</returns>
		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000AAA RID: 2730 RVA: 0x0002DBD0 File Offset: 0x0002BDD0
		public override string Description
		{
			get
			{
				if (!this.replaced)
				{
					this.replaced = true;
					base.DescriptionValue = SR.GetString(base.Description);
				}
				return base.Description;
			}
		}

		// Token: 0x04000742 RID: 1858
		private bool replaced;
	}
}
