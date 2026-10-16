using System;
using UnityEngine;

// Token: 0x02000104 RID: 260
public class LeanAudioStream
{
	// Token: 0x06000994 RID: 2452 RVA: 0x0003CE44 File Offset: 0x0003B044
	public LeanAudioStream(float[] audioArr)
	{
		this.audioArr = audioArr;
	}

	// Token: 0x06000995 RID: 2453 RVA: 0x0003CE54 File Offset: 0x0003B054
	public void OnAudioRead(float[] data)
	{
		for (int i = 0; i < data.Length; i++)
		{
			data[i] = this.audioArr[this.position];
			this.position++;
		}
	}

	// Token: 0x06000996 RID: 2454 RVA: 0x0003CE8D File Offset: 0x0003B08D
	public void OnAudioSetPosition(int newPosition)
	{
		this.position = newPosition;
	}

	// Token: 0x04000649 RID: 1609
	public int position;

	// Token: 0x0400064A RID: 1610
	public AudioClip audioClip;

	// Token: 0x0400064B RID: 1611
	public float[] audioArr;
}
