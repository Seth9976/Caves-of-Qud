using System;
using System.Globalization;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace NUnit.Framework
{
	// Token: 0x0200001C RID: 28
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	public class IgnoreAttribute : NUnitAttribute, IApplyToTest
	{
		// Token: 0x0600013F RID: 319 RVA: 0x00003E46 File Offset: 0x00002046
		public IgnoreAttribute(string reason)
		{
			this._reason = reason;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00003E55 File Offset: 0x00002055
		// (set) Token: 0x06000141 RID: 321 RVA: 0x00003E5D File Offset: 0x0000205D
		public string Reason
		{
			get
			{
				return this.Reason;
			}
			private set
			{
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00003E5F File Offset: 0x0000205F
		// (set) Token: 0x06000143 RID: 323 RVA: 0x00003E67 File Offset: 0x00002067
		public string Until
		{
			get
			{
				return this._until;
			}
			set
			{
				this._until = value;
				this._untilDate = new DateTime?(DateTime.Parse(value, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal));
			}
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00003E88 File Offset: 0x00002088
		public void ApplyToTest(Test test)
		{
			if (test.RunState != RunState.NotRunnable)
			{
				if (this._untilDate != null)
				{
					if (this._untilDate.Value > DateTime.Now)
					{
						test.RunState = RunState.Ignored;
						string text = string.Format("Ignoring until {0}. {1}", this._untilDate.Value.ToString("u"), this._reason);
						test.Properties.Set("_SKIPREASON", text);
					}
					test.Properties.Set("IgnoreUntilDate", this._untilDate.Value.ToString("u"));
					return;
				}
				test.RunState = RunState.Ignored;
				test.Properties.Set("_SKIPREASON", this._reason);
			}
		}

		// Token: 0x0400001D RID: 29
		private string _reason;

		// Token: 0x0400001E RID: 30
		private DateTime? _untilDate;

		// Token: 0x0400001F RID: 31
		private string _until;
	}
}
