using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000048 RID: 72
public class CombatJuiceSynchronizedEntry : CombatJuiceEntry
{
	// Token: 0x06000264 RID: 612 RVA: 0x0000C5A8 File Offset: 0x0000A7A8
	public CombatJuiceSynchronizedEntry()
	{
		this.Entries = new List<CombatJuiceEntry>();
	}

	// Token: 0x06000265 RID: 613 RVA: 0x0000C5BB File Offset: 0x0000A7BB
	public CombatJuiceSynchronizedEntry(IEnumerable<CombatJuiceEntry> Entries)
	{
		this.Entries = new List<CombatJuiceEntry>(Entries);
	}

	// Token: 0x06000266 RID: 614 RVA: 0x0000C5D0 File Offset: 0x0000A7D0
	public override bool canStart()
	{
		using (List<CombatJuiceEntry>.Enumerator enumerator = this.Entries.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.canStart())
				{
					return false;
				}
			}
		}
		return true;
	}

	// Token: 0x06000267 RID: 615 RVA: 0x0000C62C File Offset: 0x0000A82C
	public override void start()
	{
		foreach (CombatJuiceEntry combatJuiceEntry in this.Entries)
		{
			combatJuiceEntry.turn = this.turn;
			combatJuiceEntry.start();
			this.duration = Mathf.Max(this.duration, combatJuiceEntry.duration);
		}
	}

	// Token: 0x06000268 RID: 616 RVA: 0x0000C6A4 File Offset: 0x0000A8A4
	public override void update()
	{
		foreach (CombatJuiceEntry combatJuiceEntry in this.Entries)
		{
			combatJuiceEntry.t = this.t;
			combatJuiceEntry.update();
		}
	}

	// Token: 0x06000269 RID: 617 RVA: 0x0000C700 File Offset: 0x0000A900
	public override void finish()
	{
		foreach (CombatJuiceEntry combatJuiceEntry in this.Entries)
		{
			combatJuiceEntry.finish();
			combatJuiceEntry.finished = true;
		}
		base.finish();
	}

	// Token: 0x0400014A RID: 330
	public List<CombatJuiceEntry> Entries;
}
