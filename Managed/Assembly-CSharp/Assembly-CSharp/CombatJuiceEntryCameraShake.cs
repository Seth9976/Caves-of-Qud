using System;

// Token: 0x0200004A RID: 74
public class CombatJuiceEntryCameraShake : CombatJuiceEntry
{
	// Token: 0x0600026E RID: 622 RVA: 0x0000C9C6 File Offset: 0x0000ABC6
	public CombatJuiceEntryCameraShake(float shakeDuration)
	{
		this.t = 0f;
		this.shakeDuration = shakeDuration;
	}

	// Token: 0x0600026F RID: 623 RVA: 0x0000C9E0 File Offset: 0x0000ABE0
	public override void start()
	{
		CombatJuice._cameraShake(this.shakeDuration);
	}

	// Token: 0x04000156 RID: 342
	public float shakeDuration;
}
