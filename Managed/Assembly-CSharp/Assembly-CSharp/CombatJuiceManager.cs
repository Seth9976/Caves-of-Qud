using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200004D RID: 77
public class CombatJuiceManager : MonoBehaviour
{
	// Token: 0x06000279 RID: 633 RVA: 0x0000CC1D File Offset: 0x0000AE1D
	public static bool AnyActive()
	{
		return CombatJuiceManager.instance.active.Count > 0;
	}

	// Token: 0x17000046 RID: 70
	// (get) Token: 0x0600027A RID: 634 RVA: 0x0000CC31 File Offset: 0x0000AE31
	public ex3DSprite2[,] tiles
	{
		get
		{
			return this.gameManager.ConsoleCharacter;
		}
	}

	// Token: 0x0600027B RID: 635 RVA: 0x0000CC3E File Offset: 0x0000AE3E
	public void Awake()
	{
		CombatJuiceManager.instance = this;
		base.gameObject.transform.position = new Vector3(0f, 0f, 0f);
	}

	// Token: 0x0600027C RID: 636 RVA: 0x0000CC6C File Offset: 0x0000AE6C
	public static void clearUpToTurn(long n)
	{
		Queue<CombatJuiceEntry> queue = CombatJuiceManager.instance.queue;
		lock (queue)
		{
			foreach (CombatJuiceEntry combatJuiceEntry in CombatJuiceManager.instance.active)
			{
				try
				{
					if (combatJuiceEntry.canFinishUpToTurn())
					{
						combatJuiceEntry.finish();
						combatJuiceEntry.finished = true;
					}
					else
					{
						combatJuiceEntry.async = true;
					}
				}
				catch (Exception ex)
				{
					MetricsManager.LogException("clearUpToTurn::entry::finish", ex, "game_exception");
					if (combatJuiceEntry != null)
					{
						combatJuiceEntry.finished = true;
					}
				}
			}
			CombatJuiceManager.instance.active.RemoveAll((CombatJuiceEntry e) => e.finished);
			while (CombatJuiceManager.instance.queue.Count > 0 && CombatJuiceManager.instance.queue.Peek().turn <= n)
			{
				CombatJuiceManager.instance.queue.Dequeue();
			}
			CombatJuiceManager.instance.waiting.RemoveAll((CombatJuiceEntry e) => e.turn <= n);
		}
	}

	// Token: 0x0600027D RID: 637 RVA: 0x0000CDF8 File Offset: 0x0000AFF8
	public static void finishAll()
	{
		if (CombatJuiceManager.instance == null)
		{
			return;
		}
		foreach (CombatJuiceEntry combatJuiceEntry in CombatJuiceManager.instance.active)
		{
			combatJuiceEntry.finish();
			combatJuiceEntry.finished = true;
		}
		CombatJuiceManager.instance.waiting.Clear();
		CombatJuiceManager.instance.queue.Clear();
		CombatJuiceManager.instance.active.Clear();
		if (CombatJuice.roots == null)
		{
			return;
		}
		if (!CombatJuice.roots.ContainsKey("_JuiceRoot"))
		{
			return;
		}
		foreach (object obj in CombatJuice.roots["_JuiceRoot"].transform)
		{
			foreach (object obj2 in ((Transform)obj))
			{
				((Transform)obj2).gameObject.SendMessage("Finish", SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	// Token: 0x0600027E RID: 638 RVA: 0x0000CF40 File Offset: 0x0000B140
	public static void pause()
	{
		CombatJuiceManager.clearUpToTurn(CombatJuice.juiceTurn - 1L);
	}

	// Token: 0x0600027F RID: 639 RVA: 0x0000CF4F File Offset: 0x0000B14F
	public static void startDelay()
	{
		bool flag = CombatJuiceManager.delaying;
		CombatJuiceManager.delaying = true;
		CombatJuiceManager.delayedSequence.Clear();
	}

	// Token: 0x06000280 RID: 640 RVA: 0x0000CF67 File Offset: 0x0000B167
	public static void endDelay()
	{
		CombatJuiceManager.delaying = false;
	}

	// Token: 0x06000281 RID: 641 RVA: 0x0000CF70 File Offset: 0x0000B170
	public static void enqueueEntry(CombatJuiceEntry newEntry, bool async = false)
	{
		if (CombatJuiceManager.delaying)
		{
			newEntry.async = async;
			newEntry.turn = CombatJuice.juiceTurn;
			CombatJuiceManager.delayedSequence.Add(newEntry);
			return;
		}
		if (CombatJuiceManager.delayedSequence.Count > 0)
		{
			newEntry.delayedEntries.Clear();
			newEntry.delayedEntries.AddRange(CombatJuiceManager.delayedSequence);
			CombatJuiceManager.delayedSequence.Clear();
		}
		Queue<CombatJuiceEntry> queue = CombatJuiceManager.instance.queue;
		lock (queue)
		{
			newEntry.turn = CombatJuice.juiceTurn;
			newEntry.async = async;
			if (async)
			{
				CombatJuiceManager.instance.waiting.Add(newEntry);
			}
			else
			{
				CombatJuiceManager.instance.queue.Enqueue(newEntry);
			}
		}
	}

	// Token: 0x06000282 RID: 642 RVA: 0x0000D03C File Offset: 0x0000B23C
	private static void begin(CombatJuiceEntry effect)
	{
		if (!effect.canStart())
		{
			CombatJuiceManager.instance.waiting.Add(effect);
			return;
		}
		effect.start();
		effect.update();
		if (effect.t < effect.duration)
		{
			CombatJuiceManager.instance.active.Add(effect);
			return;
		}
		effect.finish();
		effect.finished = true;
	}

	// Token: 0x06000283 RID: 643 RVA: 0x0000D09C File Offset: 0x0000B29C
	public static void update()
	{
		CombatJuiceManager.finishedEntries.Clear();
		if (CombatJuiceManager.instance.active.Count > 0)
		{
			foreach (CombatJuiceEntry combatJuiceEntry in CombatJuiceManager.instance.active)
			{
				combatJuiceEntry.t += Time.deltaTime;
				try
				{
					combatJuiceEntry.update();
				}
				catch (Exception ex)
				{
					MetricsManager.LogException("CombatJuiceActiveUpdate", ex, "game_exception");
					combatJuiceEntry.t = combatJuiceEntry.duration + float.Epsilon;
				}
				if (combatJuiceEntry.t > combatJuiceEntry.duration)
				{
					try
					{
						combatJuiceEntry.finish();
					}
					catch (Exception ex2)
					{
						MetricsManager.LogException("CombatJuiceActiveFinish", ex2, "game_exception");
					}
					combatJuiceEntry.finished = true;
					CombatJuiceManager.finishedEntries.Add(combatJuiceEntry);
				}
			}
		}
		foreach (CombatJuiceEntry combatJuiceEntry2 in CombatJuiceManager.finishedEntries)
		{
			CombatJuiceManager.instance.active.Remove(combatJuiceEntry2);
		}
		if (CombatJuiceManager.instance.queue.Count > 0 || CombatJuiceManager.instance.waiting.Count > 0)
		{
			Queue<CombatJuiceEntry> queue = CombatJuiceManager.instance.queue;
			lock (queue)
			{
				for (int i = 0; i < CombatJuiceManager.instance.waiting.Count; i++)
				{
					CombatJuiceEntry combatJuiceEntry3 = CombatJuiceManager.instance.waiting[i];
					if (combatJuiceEntry3.canStart())
					{
						try
						{
							CombatJuiceManager.begin(combatJuiceEntry3);
						}
						catch (Exception ex3)
						{
							MetricsManager.LogException("CombatJuiceWaitingBegin", ex3, "game_exception");
							CombatJuiceManager.instance.active.Remove(combatJuiceEntry3);
						}
						CombatJuiceManager.instance.waiting.RemoveAt(i);
						i--;
					}
				}
				while (!CombatJuiceManager.AnyNonAsync() && CombatJuiceManager.instance.queue.Count > 0)
				{
					CombatJuiceEntry combatJuiceEntry4 = CombatJuiceManager.instance.queue.Dequeue();
					try
					{
						CombatJuiceManager.begin(combatJuiceEntry4);
					}
					catch (Exception ex4)
					{
						MetricsManager.LogException("CombatJuiceQueueBegin", ex4, "game_exception");
						CombatJuiceManager.instance.active.Remove(combatJuiceEntry4);
						CombatJuiceManager.instance.waiting.Remove(combatJuiceEntry4);
					}
				}
			}
		}
	}

	// Token: 0x06000284 RID: 644 RVA: 0x0000D350 File Offset: 0x0000B550
	public static bool AnyNonAsync()
	{
		for (int i = CombatJuiceManager.instance.active.Count - 1; i >= 0; i--)
		{
			if (!CombatJuiceManager.instance.active[i].async)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x04000165 RID: 357
	public List<CombatJuiceEntry> active = new List<CombatJuiceEntry>();

	// Token: 0x04000166 RID: 358
	public List<CombatJuiceEntry> waiting = new List<CombatJuiceEntry>();

	// Token: 0x04000167 RID: 359
	public Queue<CombatJuiceEntry> queue = new Queue<CombatJuiceEntry>();

	// Token: 0x04000168 RID: 360
	public Dictionary<Type, Queue<CombatJuiceEntry>> pool = new Dictionary<Type, Queue<CombatJuiceEntry>>();

	// Token: 0x04000169 RID: 361
	public static bool NoPause = false;

	// Token: 0x0400016A RID: 362
	public GameManager gameManager;

	// Token: 0x0400016B RID: 363
	public static CombatJuiceManager instance;

	// Token: 0x0400016C RID: 364
	private static List<CombatJuiceEntry> delayedSequence = new List<CombatJuiceEntry>();

	// Token: 0x0400016D RID: 365
	private static bool delaying = false;

	// Token: 0x0400016E RID: 366
	private static List<CombatJuiceEntry> finishedEntries = new List<CombatJuiceEntry>();
}
