using System;

namespace System.Diagnostics
{
	/// <summary>Holds instance data associated with a performance counter sample.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200026C RID: 620
	public class InstanceData
	{
		/// <summary>Initializes a new instance of the InstanceData class, using the specified sample and performance counter instance.</summary>
		/// <param name="instanceName">The name of an instance associated with the performance counter. </param>
		/// <param name="sample">A <see cref="T:System.Diagnostics.CounterSample" /> taken from the instance specified by the <paramref name="instanceName" /> parameter. </param>
		// Token: 0x06001374 RID: 4980 RVA: 0x000517B0 File Offset: 0x0004F9B0
		public InstanceData(string instanceName, CounterSample sample)
		{
			this.instanceName = instanceName;
			this.sample = sample;
		}

		/// <summary>Gets the instance name associated with this instance data.</summary>
		/// <returns>The name of an instance associated with the performance counter.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06001375 RID: 4981 RVA: 0x000517C6 File Offset: 0x0004F9C6
		public string InstanceName
		{
			get
			{
				return this.instanceName;
			}
		}

		/// <summary>Gets the raw data value associated with the performance counter sample.</summary>
		/// <returns>The raw value read by the performance counter sample associated with the <see cref="P:System.Diagnostics.InstanceData.Sample" /> property.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06001376 RID: 4982 RVA: 0x000517CE File Offset: 0x0004F9CE
		public long RawValue
		{
			get
			{
				return this.sample.RawValue;
			}
		}

		/// <summary>Gets the performance counter sample that generated this data.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.CounterSample" /> taken from the instance specified by the <see cref="P:System.Diagnostics.InstanceData.InstanceName" /> property.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06001377 RID: 4983 RVA: 0x000517DB File Offset: 0x0004F9DB
		public CounterSample Sample
		{
			get
			{
				return this.sample;
			}
		}

		// Token: 0x04000B22 RID: 2850
		private string instanceName;

		// Token: 0x04000B23 RID: 2851
		private CounterSample sample;
	}
}
