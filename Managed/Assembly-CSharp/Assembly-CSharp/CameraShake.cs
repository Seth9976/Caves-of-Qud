using System;
using UnityEngine;

// Token: 0x02000158 RID: 344
public class CameraShake : MonoBehaviour
{
	// Token: 0x06000DAC RID: 3500 RVA: 0x000589B4 File Offset: 0x00056BB4
	private void Awake()
	{
		if (this.camTransform == null)
		{
			this.camTransform = base.GetComponent(typeof(Transform)) as Transform;
		}
	}

	// Token: 0x06000DAD RID: 3501 RVA: 0x000589E0 File Offset: 0x00056BE0
	private void Update()
	{
		if (this.TestShakeDuration > 0f)
		{
			CameraShake.shakeDuration = this.TestShakeDuration;
			this.TestShakeDuration = 0f;
		}
		if (CameraShake.shakeDuration > 0f && !this.shaking)
		{
			this.originalPos = this.camTransform.localPosition;
			this.shaking = true;
			CameraShake.currentShakeAmount = this.initialShake;
		}
		if (!this.shaking)
		{
			return;
		}
		if (CameraShake.shakeDuration > 0f)
		{
			this.camTransform.localPosition = this.originalPos + global::UnityEngine.Random.insideUnitSphere * CameraShake.currentShakeAmount;
			CameraShake.shakeDuration -= Time.deltaTime * this.decreaseFactor;
			CameraShake.currentShakeAmount -= Time.deltaTime * this.shakeFalloff;
			return;
		}
		this.shaking = false;
		CameraShake.shakeDuration = 0f;
		this.camTransform.localPosition = this.originalPos;
	}

	// Token: 0x04000978 RID: 2424
	public Transform camTransform;

	// Token: 0x04000979 RID: 2425
	public static float shakeDuration = 0f;

	// Token: 0x0400097A RID: 2426
	public float initialShake = 2f;

	// Token: 0x0400097B RID: 2427
	public float shakeFalloff = 0.7f;

	// Token: 0x0400097C RID: 2428
	public static float currentShakeAmount = 0.7f;

	// Token: 0x0400097D RID: 2429
	public float decreaseFactor = 1f;

	// Token: 0x0400097E RID: 2430
	public bool shaking;

	// Token: 0x0400097F RID: 2431
	public float TestShakeDuration;

	// Token: 0x04000980 RID: 2432
	private Vector3 originalPos;
}
