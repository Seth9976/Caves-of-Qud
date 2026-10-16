using System;

namespace NUnit.Framework
{
	// Token: 0x0200001D RID: 29
	public abstract class IncludeExcludeAttribute : NUnitAttribute
	{
		// Token: 0x06000145 RID: 325 RVA: 0x00003DE1 File Offset: 0x00001FE1
		public IncludeExcludeAttribute()
		{
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00003F4E File Offset: 0x0000214E
		public IncludeExcludeAttribute(string include)
		{
			this.include = include;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00003F5D File Offset: 0x0000215D
		// (set) Token: 0x06000148 RID: 328 RVA: 0x00003F65 File Offset: 0x00002165
		public string Include
		{
			get
			{
				return this.include;
			}
			set
			{
				this.include = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00003F6E File Offset: 0x0000216E
		// (set) Token: 0x0600014A RID: 330 RVA: 0x00003F76 File Offset: 0x00002176
		public string Exclude
		{
			get
			{
				return this.exclude;
			}
			set
			{
				this.exclude = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00003F7F File Offset: 0x0000217F
		// (set) Token: 0x0600014C RID: 332 RVA: 0x00003F87 File Offset: 0x00002187
		public string Reason
		{
			get
			{
				return this.reason;
			}
			set
			{
				this.reason = value;
			}
		}

		// Token: 0x04000020 RID: 32
		private string include;

		// Token: 0x04000021 RID: 33
		private string exclude;

		// Token: 0x04000022 RID: 34
		private string reason;
	}
}
