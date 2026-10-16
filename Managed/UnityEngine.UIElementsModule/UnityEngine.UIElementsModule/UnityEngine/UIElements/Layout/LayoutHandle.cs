using System;

namespace UnityEngine.UIElements.Layout
{
	// Token: 0x0200068B RID: 1675
	internal readonly struct LayoutHandle
	{
		// Token: 0x17000BA0 RID: 2976
		// (get) Token: 0x060030BD RID: 12477 RVA: 0x000B94D4 File Offset: 0x000B76D4
		public static LayoutHandle Undefined
		{
			get
			{
				return default(LayoutHandle);
			}
		}

		// Token: 0x060030BE RID: 12478 RVA: 0x000B94EA File Offset: 0x000B76EA
		internal LayoutHandle(int index, int version)
		{
			this.Index = index;
			this.Version = version;
		}

		// Token: 0x17000BA1 RID: 2977
		// (get) Token: 0x060030BF RID: 12479 RVA: 0x000B94FB File Offset: 0x000B76FB
		public bool IsUndefined
		{
			get
			{
				return this.Equals(LayoutHandle.Undefined);
			}
		}

		// Token: 0x060030C0 RID: 12480 RVA: 0x000B9508 File Offset: 0x000B7708
		public bool Equals(LayoutHandle other)
		{
			return this.Index == other.Index && this.Version == other.Version;
		}

		// Token: 0x060030C1 RID: 12481 RVA: 0x000B952C File Offset: 0x000B772C
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is LayoutHandle)
			{
				LayoutHandle layoutHandle = (LayoutHandle)obj;
				flag = this.Equals(layoutHandle);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060030C2 RID: 12482 RVA: 0x000B9554 File Offset: 0x000B7754
		public override int GetHashCode()
		{
			return (this.Index * 397) ^ this.Version;
		}

		// Token: 0x04001810 RID: 6160
		public readonly int Index;

		// Token: 0x04001811 RID: 6161
		public readonly int Version;
	}
}
