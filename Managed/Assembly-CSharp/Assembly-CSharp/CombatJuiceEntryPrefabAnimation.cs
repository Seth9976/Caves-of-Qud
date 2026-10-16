using System;
using UnityEngine;

// Token: 0x0200004C RID: 76
public class CombatJuiceEntryPrefabAnimation : CombatJuiceEntry
{
	// Token: 0x06000275 RID: 629 RVA: 0x0000CB98 File Offset: 0x0000AD98
	public CombatJuiceEntryPrefabAnimation(Vector3 location, string animation)
	{
		this.location = location;
		this.animation = animation;
	}

	// Token: 0x06000276 RID: 630 RVA: 0x0000CBAE File Offset: 0x0000ADAE
	public CombatJuiceEntryPrefabAnimation(Vector3 location, string animation, string objectId)
	{
		this.location = location;
		this.animation = animation;
		this.objectId = objectId;
	}

	// Token: 0x06000277 RID: 631 RVA: 0x0000CBCB File Offset: 0x0000ADCB
	public CombatJuiceEntryPrefabAnimation(Vector3 location, string animation, string objectId, string configurationString, object configurationObject)
	{
		this.location = location;
		this.animation = animation;
		this.objectId = objectId;
		this.configurationString = configurationString;
		this.configurationObject = configurationObject;
	}

	// Token: 0x06000278 RID: 632 RVA: 0x0000CBF8 File Offset: 0x0000ADF8
	public override void start()
	{
		CombatJuice._playPrefabAnimation(this.location, this.animation, this.objectId, this.configurationString, this.configurationObject);
	}

	// Token: 0x04000160 RID: 352
	public Vector3 location;

	// Token: 0x04000161 RID: 353
	public string animation;

	// Token: 0x04000162 RID: 354
	public string objectId;

	// Token: 0x04000163 RID: 355
	public string configurationString;

	// Token: 0x04000164 RID: 356
	public object configurationObject;
}
