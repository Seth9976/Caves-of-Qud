using System;
using UnityEngine;

// Token: 0x02000105 RID: 261
public class LeanAudio : MonoBehaviour
{
	// Token: 0x06000997 RID: 2455 RVA: 0x0003CE96 File Offset: 0x0003B096
	public static LeanAudioOptions options()
	{
		if (LeanAudio.generatedWaveDistances == null)
		{
			LeanAudio.generatedWaveDistances = new float[LeanAudio.PROCESSING_ITERATIONS_MAX];
			LeanAudio.longList = new float[LeanAudio.PROCESSING_ITERATIONS_MAX];
		}
		return new LeanAudioOptions();
	}

	// Token: 0x06000998 RID: 2456 RVA: 0x0003CEC2 File Offset: 0x0003B0C2
	public static LeanAudioStream createAudioStream(AnimationCurve volume, AnimationCurve frequency, LeanAudioOptions options = null)
	{
		if (options == null)
		{
			options = new LeanAudioOptions();
		}
		options.useSetData = false;
		LeanAudio.createAudioFromWave(LeanAudio.createAudioWave(volume, frequency, options), options);
		return options.stream;
	}

	// Token: 0x06000999 RID: 2457 RVA: 0x0003CEEA File Offset: 0x0003B0EA
	public static AudioClip createAudio(AnimationCurve volume, AnimationCurve frequency, LeanAudioOptions options = null)
	{
		if (options == null)
		{
			options = new LeanAudioOptions();
		}
		return LeanAudio.createAudioFromWave(LeanAudio.createAudioWave(volume, frequency, options), options);
	}

	// Token: 0x0600099A RID: 2458 RVA: 0x0003CF04 File Offset: 0x0003B104
	private static int createAudioWave(AnimationCurve volume, AnimationCurve frequency, LeanAudioOptions options)
	{
		float time = volume[volume.length - 1].time;
		int num = 0;
		float num2 = 0f;
		for (int i = 0; i < LeanAudio.PROCESSING_ITERATIONS_MAX; i++)
		{
			float num3 = frequency.Evaluate(num2);
			if (num3 < LeanAudio.MIN_FREQEUNCY_PERIOD)
			{
				num3 = LeanAudio.MIN_FREQEUNCY_PERIOD;
			}
			float num4 = volume.Evaluate(num2 + 0.5f * num3);
			if (options.vibrato != null)
			{
				for (int j = 0; j < options.vibrato.Length; j++)
				{
					float num5 = Mathf.Abs(Mathf.Sin(1.5708f + num2 * (1f / options.vibrato[j][0]) * 3.1415927f));
					float num6 = 1f - options.vibrato[j][1];
					num5 = options.vibrato[j][1] + num6 * num5;
					num4 *= num5;
				}
			}
			if (num2 + 0.5f * num3 >= time)
			{
				break;
			}
			if (num >= LeanAudio.PROCESSING_ITERATIONS_MAX - 1)
			{
				Debug.LogError("LeanAudio has reached it's processing cap. To avoid this error increase the number of iterations ex: LeanAudio.PROCESSING_ITERATIONS_MAX = " + (LeanAudio.PROCESSING_ITERATIONS_MAX * 2).ToString());
				break;
			}
			LeanAudio.generatedWaveDistances[num / 2] = num3;
			num2 += num3;
			LeanAudio.longList[num] = num2;
			LeanAudio.longList[num + 1] = ((i % 2 == 0) ? (-num4) : num4);
			num += 2;
		}
		num += -2;
		LeanAudio.generatedWaveDistancesCount = num / 2;
		return num;
	}

	// Token: 0x0600099B RID: 2459 RVA: 0x0003D084 File Offset: 0x0003B284
	private static AudioClip createAudioFromWave(int waveLength, LeanAudioOptions options)
	{
		float num = LeanAudio.longList[waveLength - 2];
		float[] array = new float[(int)((float)options.frequencyRate * num)];
		int num2 = 0;
		float num3 = LeanAudio.longList[num2];
		float num4 = 0f;
		float num5 = LeanAudio.longList[num2];
		float num6 = LeanAudio.longList[num2 + 1];
		for (int i = 0; i < array.Length; i++)
		{
			float num7 = (float)i / (float)options.frequencyRate;
			if (num7 > LeanAudio.longList[num2])
			{
				num4 = LeanAudio.longList[num2];
				num2 += 2;
				num3 = LeanAudio.longList[num2] - LeanAudio.longList[num2 - 2];
				num6 = LeanAudio.longList[num2 + 1];
			}
			float num8 = Mathf.Sin((num7 - num4) / num3 * 3.1415927f);
			num8 *= num6;
			array[i] = num8;
		}
		int num9 = array.Length;
		AudioClip audioClip;
		if (options.useSetData)
		{
			audioClip = AudioClip.Create("Generated Audio", num9, 1, options.frequencyRate, false, null, new AudioClip.PCMSetPositionCallback(LeanAudio.OnAudioSetPosition));
			audioClip.SetData(array, 0);
		}
		else
		{
			options.stream = new LeanAudioStream(array);
			Debug.Log(string.Concat(new string[]
			{
				"len:",
				array.Length.ToString(),
				" lengthSamples:",
				num9.ToString(),
				" freqRate:",
				options.frequencyRate.ToString()
			}));
			audioClip = AudioClip.Create("Generated Audio", num9, 1, options.frequencyRate, false, new AudioClip.PCMReaderCallback(options.stream.OnAudioRead), new AudioClip.PCMSetPositionCallback(options.stream.OnAudioSetPosition));
			options.stream.audioClip = audioClip;
		}
		return audioClip;
	}

	// Token: 0x0600099C RID: 2460 RVA: 0x0003D223 File Offset: 0x0003B423
	private static void OnAudioSetPosition(int newPosition)
	{
	}

	// Token: 0x0600099D RID: 2461 RVA: 0x0003D228 File Offset: 0x0003B428
	public static AudioClip generateAudioFromCurve(AnimationCurve curve, int frequencyRate = 44100)
	{
		float time = curve[curve.length - 1].time;
		float[] array = new float[(int)((float)frequencyRate * time)];
		for (int i = 0; i < array.Length; i++)
		{
			float num = (float)i / (float)frequencyRate;
			array[i] = curve.Evaluate(num);
		}
		int num2 = array.Length;
		AudioClip audioClip = AudioClip.Create("Generated Audio", num2, 1, frequencyRate, false);
		audioClip.SetData(array, 0);
		return audioClip;
	}

	// Token: 0x0600099E RID: 2462 RVA: 0x0003D298 File Offset: 0x0003B498
	public static AudioSource play(AudioClip audio, Vector3 pos = default(Vector3), float volume = 1f, float pitch = 1f)
	{
		GameObject gameObject = new GameObject();
		gameObject.transform.position = pos;
		AudioSource audioSource = gameObject.AddComponent<AudioSource>();
		audioSource.volume = volume;
		audioSource.pitch = pitch;
		audioSource.clip = audio;
		audioSource.Play();
		global::UnityEngine.Object.Destroy(gameObject, audio.length);
		return audioSource;
	}

	// Token: 0x0600099F RID: 2463 RVA: 0x0003D2E4 File Offset: 0x0003B4E4
	public static void printOutAudioClip(AudioClip audioClip, ref AnimationCurve curve, float scaleX = 1f)
	{
		float[] array = new float[audioClip.samples * audioClip.channels];
		audioClip.GetData(array, 0);
		int i = 0;
		Keyframe[] array2 = new Keyframe[array.Length];
		while (i < array.Length)
		{
			array2[i] = new Keyframe((float)i * scaleX, array[i]);
			i++;
		}
		curve = new AnimationCurve(array2);
	}

	// Token: 0x0400064C RID: 1612
	public static float MIN_FREQEUNCY_PERIOD = 0.000115f;

	// Token: 0x0400064D RID: 1613
	public static int PROCESSING_ITERATIONS_MAX = 50000;

	// Token: 0x0400064E RID: 1614
	public static float[] generatedWaveDistances;

	// Token: 0x0400064F RID: 1615
	public static int generatedWaveDistancesCount = 0;

	// Token: 0x04000650 RID: 1616
	private static float[] longList;
}
