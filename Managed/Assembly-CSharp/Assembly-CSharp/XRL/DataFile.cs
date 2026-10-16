using System;

namespace XRL
{
	// Token: 0x020004D8 RID: 1240
	public class DataFile : IComparable<DataFile>
	{
		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06003535 RID: 13621 RVA: 0x0010108F File Offset: 0x000FF28F
		public bool IsBase
		{
			get
			{
				return this.Mod == null;
			}
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06003536 RID: 13622 RVA: 0x0010109A File Offset: 0x000FF29A
		public bool IsMod
		{
			get
			{
				return this.Mod != null;
			}
		}

		// Token: 0x06003537 RID: 13623 RVA: 0x001010A5 File Offset: 0x000FF2A5
		public static implicit operator string(DataFile File)
		{
			return File.Path;
		}

		// Token: 0x06003538 RID: 13624 RVA: 0x001010B0 File Offset: 0x000FF2B0
		public int CompareTo(DataFile Other)
		{
			int num;
			if (this.Mod == null)
			{
				if (Other.Mod != null)
				{
					return -1;
				}
			}
			else
			{
				if (Other.Mod == null)
				{
					return 1;
				}
				num = this.Mod.CompareTo(Other.Mod);
				if (num != 0)
				{
					return num;
				}
			}
			num = Other.Priority.CompareTo(this.Priority);
			if (num != 0)
			{
				return num;
			}
			return string.Compare(this.Path, Other.Path, StringComparison.Ordinal);
		}

		// Token: 0x06003539 RID: 13625 RVA: 0x0010111B File Offset: 0x000FF31B
		public override string ToString()
		{
			return this.Path;
		}

		// Token: 0x04001ADB RID: 6875
		public ModInfo Mod;

		// Token: 0x04001ADC RID: 6876
		public string Path;

		// Token: 0x04001ADD RID: 6877
		public int Priority;
	}
}
