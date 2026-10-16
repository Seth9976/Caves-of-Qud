using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000337 RID: 823
	public struct TimerState : IEquatable<TimerState>
	{
		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x060017DD RID: 6109 RVA: 0x0005C6AC File Offset: 0x0005A8AC
		// (set) Token: 0x060017DE RID: 6110 RVA: 0x0005C6B4 File Offset: 0x0005A8B4
		public long start { readonly get; set; }

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x060017DF RID: 6111 RVA: 0x0005C6BD File Offset: 0x0005A8BD
		// (set) Token: 0x060017E0 RID: 6112 RVA: 0x0005C6C5 File Offset: 0x0005A8C5
		public long now { readonly get; set; }

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x060017E1 RID: 6113 RVA: 0x0005C6D0 File Offset: 0x0005A8D0
		public long deltaTime
		{
			get
			{
				return this.now - this.start;
			}
		}

		// Token: 0x060017E2 RID: 6114 RVA: 0x0005C6F0 File Offset: 0x0005A8F0
		public override bool Equals(object obj)
		{
			return obj is TimerState && this.Equals((TimerState)obj);
		}

		// Token: 0x060017E3 RID: 6115 RVA: 0x0005C71C File Offset: 0x0005A91C
		public bool Equals(TimerState other)
		{
			return this.start == other.start && this.now == other.now && this.deltaTime == other.deltaTime;
		}

		// Token: 0x060017E4 RID: 6116 RVA: 0x0005C760 File Offset: 0x0005A960
		public override int GetHashCode()
		{
			int num = 540054806;
			num = num * -1521134295 + this.start.GetHashCode();
			num = num * -1521134295 + this.now.GetHashCode();
			return num * -1521134295 + this.deltaTime.GetHashCode();
		}

		// Token: 0x060017E5 RID: 6117 RVA: 0x0005C7C0 File Offset: 0x0005A9C0
		public static bool operator ==(TimerState state1, TimerState state2)
		{
			return state1.Equals(state2);
		}

		// Token: 0x060017E6 RID: 6118 RVA: 0x0005C7DC File Offset: 0x0005A9DC
		public static bool operator !=(TimerState state1, TimerState state2)
		{
			return !(state1 == state2);
		}
	}
}
