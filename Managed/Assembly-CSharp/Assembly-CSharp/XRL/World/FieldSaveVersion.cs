using System;

namespace XRL.World
{
	// Token: 0x02000970 RID: 2416
	[AttributeUsage(AttributeTargets.Field)]
	public class FieldSaveVersion : Attribute
	{
		// Token: 0x06006409 RID: 25609 RVA: 0x001EDD52 File Offset: 0x001EBF52
		public FieldSaveVersion()
		{
		}

		// Token: 0x0600640A RID: 25610 RVA: 0x001EDD61 File Offset: 0x001EBF61
		public FieldSaveVersion(int min)
		{
			this._minimumSaveVersion = min;
		}

		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x0600640B RID: 25611 RVA: 0x001EDD77 File Offset: 0x001EBF77
		public virtual int minimumSaveVersion
		{
			get
			{
				return this._minimumSaveVersion;
			}
		}

		// Token: 0x04002EF8 RID: 12024
		private int _minimumSaveVersion = -1;
	}
}
