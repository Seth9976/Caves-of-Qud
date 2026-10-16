using System;
using System.Collections.Generic;

// Token: 0x02000043 RID: 67
public class CombatJuiceEntry
{
	// Token: 0x17000045 RID: 69
	// (get) Token: 0x06000253 RID: 595 RVA: 0x0000C1A9 File Offset: 0x0000A3A9
	public static GameManager gameManager
	{
		get
		{
			return GameManager.Instance;
		}
	}

	// Token: 0x06000254 RID: 596 RVA: 0x0000C1B0 File Offset: 0x0000A3B0
	public virtual bool canStart()
	{
		return true;
	}

	// Token: 0x06000255 RID: 597 RVA: 0x0000C1B3 File Offset: 0x0000A3B3
	public virtual bool canFinishUpToTurn()
	{
		return true;
	}

	// Token: 0x06000256 RID: 598 RVA: 0x0000C1B6 File Offset: 0x0000A3B6
	public virtual void start()
	{
	}

	// Token: 0x06000257 RID: 599 RVA: 0x0000C1B8 File Offset: 0x0000A3B8
	public virtual void update()
	{
	}

	// Token: 0x06000258 RID: 600 RVA: 0x0000C1BC File Offset: 0x0000A3BC
	public virtual void finish()
	{
		if (this.delayedEntries.Count > 0)
		{
			foreach (CombatJuiceEntry combatJuiceEntry in this.delayedEntries)
			{
				CombatJuiceManager.enqueueEntry(combatJuiceEntry, combatJuiceEntry.async);
			}
			this.delayedEntries.Clear();
		}
	}

	// Token: 0x04000134 RID: 308
	public long turn;

	// Token: 0x04000135 RID: 309
	public float duration;

	// Token: 0x04000136 RID: 310
	public float t;

	// Token: 0x04000137 RID: 311
	public bool async;

	// Token: 0x04000138 RID: 312
	public bool finished;

	// Token: 0x04000139 RID: 313
	public List<CombatJuiceEntry> delayedEntries = new List<CombatJuiceEntry>();
}
