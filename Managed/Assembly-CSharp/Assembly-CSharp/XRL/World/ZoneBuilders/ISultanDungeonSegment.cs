using System;
using Genkit;
using UnityEngine;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AED RID: 2797
	public class ISultanDungeonSegment
	{
		// Token: 0x06007021 RID: 28705 RVA: 0x0027C13A File Offset: 0x0027A33A
		public virtual int width()
		{
			return 0;
		}

		// Token: 0x06007022 RID: 28706 RVA: 0x0027C13D File Offset: 0x0027A33D
		public virtual int height()
		{
			return 0;
		}

		// Token: 0x06007023 RID: 28707 RVA: 0x0027C140 File Offset: 0x0027A340
		public virtual bool contains(int x, int y)
		{
			return false;
		}

		// Token: 0x06007024 RID: 28708 RVA: 0x0027C143 File Offset: 0x0027A343
		public virtual bool contains(Location2D pos)
		{
			return false;
		}

		// Token: 0x06007025 RID: 28709 RVA: 0x0027C146 File Offset: 0x0027A346
		public virtual bool HasCustomColor(int x, int y)
		{
			return false;
		}

		// Token: 0x06007026 RID: 28710 RVA: 0x0027C149 File Offset: 0x0027A349
		public virtual Color32 GetCustomColor(int x, int y)
		{
			return new Color32(0, 0, 0, 0);
		}

		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x06007027 RID: 28711 RVA: 0x0027C154 File Offset: 0x0027A354
		public virtual int x1
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x06007028 RID: 28712 RVA: 0x0027C157 File Offset: 0x0027A357
		public virtual int y1
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x06007029 RID: 28713 RVA: 0x0027C15A File Offset: 0x0027A35A
		public virtual int x2
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x0600702A RID: 28714 RVA: 0x0027C15D File Offset: 0x0027A35D
		public virtual int y2
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x040033B2 RID: 13234
		public string mutator;
	}
}
