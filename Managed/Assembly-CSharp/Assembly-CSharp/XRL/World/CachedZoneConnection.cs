using System;

namespace XRL.World
{
	// Token: 0x02000991 RID: 2449
	[Serializable]
	public class CachedZoneConnection : ZoneConnection
	{
		// Token: 0x060068EC RID: 26860 RVA: 0x0020EC67 File Offset: 0x0020CE67
		public CachedZoneConnection(string _TD, int _X, int _Y, string _Type, string _Object)
		{
			this.TargetDirection = _TD;
			this.X = _X;
			this.Y = _Y;
			this.Type = _Type;
			this.Object = _Object;
		}

		// Token: 0x060068ED RID: 26861 RVA: 0x0020EC94 File Offset: 0x0020CE94
		public override string ToString()
		{
			return string.Concat(new string[] { this.TargetDirection, " ", this.Type, " ", this.Object });
		}

		// Token: 0x04003042 RID: 12354
		public string TargetDirection;
	}
}
