using System;
using System.Collections.Generic;
using UnityEngine;
using XRL.World;

// Token: 0x0200004B RID: 75
public class CombatJuiceEntryText : CombatJuiceEntry
{
	// Token: 0x06000270 RID: 624 RVA: 0x0000C9F0 File Offset: 0x0000ABF0
	public CombatJuiceEntryText(Vector3 start, Vector3 end, float floatTime, string text, Color color, XRL.World.GameObject emittingObject, float scale)
	{
		this.startPosition = start;
		this.endPosition = end;
		this.duration = 0f;
		this.text = text;
		this.color = color;
		this.floatTime = floatTime;
		this.emittingObject = emittingObject;
		this.scale = scale;
	}

	// Token: 0x06000271 RID: 625 RVA: 0x0000CA43 File Offset: 0x0000AC43
	public bool outOfTheWayOnEmittingObject()
	{
		return false;
	}

	// Token: 0x06000272 RID: 626 RVA: 0x0000CA48 File Offset: 0x0000AC48
	public override bool canStart()
	{
		if (this.emittingObject == null)
		{
			return true;
		}
		int hashCode = this.emittingObject.GetHashCode();
		return !CombatJuiceEntryText.lastTextEmission.ContainsKey(hashCode) || Time.time - CombatJuiceEntryText.lastTextEmission[hashCode] >= 0.6f;
	}

	// Token: 0x06000273 RID: 627 RVA: 0x0000CA94 File Offset: 0x0000AC94
	public override void start()
	{
		if (this.emittingObject != null)
		{
			if (CombatJuiceEntryText.lastTextEmission.ContainsKey(this.emittingObject.GetHashCode()))
			{
				CombatJuiceEntryText.lastTextEmission[this.emittingObject.GetHashCode()] = Time.time;
			}
			else
			{
				CombatJuiceEntryText.lastTextEmission.Add(this.emittingObject.GetHashCode(), Time.time);
			}
		}
		for (;;)
		{
			IL_0055:
			foreach (KeyValuePair<int, float> keyValuePair in CombatJuiceEntryText.lastTextEmission)
			{
				if (Time.time - keyValuePair.Value > 5f)
				{
					CombatJuiceEntryText.lastTextEmission.Remove(keyValuePair.Key);
					goto IL_0055;
				}
			}
			break;
		}
		CombatJuice._text(this.startPosition, this.endPosition, this.text, this.color, this.floatTime, this.scale);
		this.emittingObject = null;
	}

	// Token: 0x04000157 RID: 343
	public float floatTime;

	// Token: 0x04000158 RID: 344
	public float scale;

	// Token: 0x04000159 RID: 345
	public string text;

	// Token: 0x0400015A RID: 346
	public Color color;

	// Token: 0x0400015B RID: 347
	public Vector3 startPosition;

	// Token: 0x0400015C RID: 348
	public Vector3 endPosition;

	// Token: 0x0400015D RID: 349
	public XRL.World.GameObject emittingObject;

	// Token: 0x0400015E RID: 350
	public Easing.Functions ease;

	// Token: 0x0400015F RID: 351
	private static Dictionary<int, float> lastTextEmission = new Dictionary<int, float>();
}
