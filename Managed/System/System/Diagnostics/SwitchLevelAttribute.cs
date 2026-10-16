using System;

namespace System.Diagnostics
{
	/// <summary>Identifies the level type for a switch. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200022B RID: 555
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class SwitchLevelAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.SwitchLevelAttribute" /> class, specifying the type that determines whether a trace should be written.</summary>
		/// <param name="switchLevelType">The <see cref="T:System.Type" /> that determines whether a trace should be written.</param>
		// Token: 0x0600101C RID: 4124 RVA: 0x00046DA1 File Offset: 0x00044FA1
		public SwitchLevelAttribute(Type switchLevelType)
		{
			this.SwitchLevelType = switchLevelType;
		}

		/// <summary>Gets or sets the type that determines whether a trace should be written.</summary>
		/// <returns>The <see cref="T:System.Type" /> that determines whether a trace should be written.</returns>
		/// <exception cref="T:System.ArgumentNullException">The set operation failed because the value is null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x0600101D RID: 4125 RVA: 0x00046DB0 File Offset: 0x00044FB0
		// (set) Token: 0x0600101E RID: 4126 RVA: 0x00046DB8 File Offset: 0x00044FB8
		public Type SwitchLevelType
		{
			get
			{
				return this.type;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.type = value;
			}
		}

		// Token: 0x040009EA RID: 2538
		private Type type;
	}
}
