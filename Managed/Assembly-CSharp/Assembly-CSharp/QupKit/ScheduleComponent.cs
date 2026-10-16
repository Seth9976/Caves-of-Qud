using System;
using System.Collections.Generic;
using UnityEngine;

namespace QupKit
{
	// Token: 0x020003A9 RID: 937
	public class ScheduleComponent : MonoBehaviour
	{
		// Token: 0x06002304 RID: 8964 RVA: 0x000B1F74 File Offset: 0x000B0174
		private void LateUpdate()
		{
			if (this.Entries.Count == 0)
			{
				global::UnityEngine.Object.Destroy(this);
				return;
			}
			List<ScheduleEntry> list = null;
			for (int i = 0; i < this.Entries.Count; i++)
			{
				this.Entries[i].Time -= Time.deltaTime;
				if (this.Entries[i].Time <= 0f)
				{
					if (list == null)
					{
						list = new List<ScheduleEntry>();
					}
					bool flag = false;
					for (int j = 0; j < list.Count; j++)
					{
						if (list[j].ScheduledTime > this.Entries[i].ScheduledTime)
						{
							list.Insert(j, this.Entries[i]);
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						list.Add(this.Entries[i]);
					}
				}
			}
			if (list != null)
			{
				for (int k = 0; k < list.Count; k++)
				{
					list[k].Execute();
					this.Entries.Remove(list[k]);
				}
			}
		}

		// Token: 0x06002305 RID: 8965 RVA: 0x000B2088 File Offset: 0x000B0288
		public void After(float T, Action A)
		{
			this.Entries.Add(new ScheduleEntry(T, A, Time.time + T));
		}

		// Token: 0x040011A7 RID: 4519
		public List<ScheduleEntry> Entries = new List<ScheduleEntry>();
	}
}
