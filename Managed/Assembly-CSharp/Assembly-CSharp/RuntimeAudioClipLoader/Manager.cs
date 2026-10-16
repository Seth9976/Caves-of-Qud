using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;

namespace RuntimeAudioClipLoader
{
	// Token: 0x02000191 RID: 401
	public class Manager : MonoBehaviour
	{
		// Token: 0x06000F49 RID: 3913 RVA: 0x0006054C File Offset: 0x0005E74C
		public static AudioClip Load(string filePath, bool doStream = false, bool loadInBackground = true, bool useCache = true)
		{
			if (!Manager.IsSupportedFormat(filePath))
			{
				Debug.LogError("Could not load AudioClip at path '" + filePath + "' it's extensions marks unsupported format, supported formats are: " + string.Join(", ", Enum.GetNames(typeof(AudioFormat))));
				return null;
			}
			AudioClip audioClip = null;
			if (useCache && Manager.cache.TryGetValue(filePath, out audioClip) && audioClip)
			{
				return audioClip;
			}
			audioClip = Manager.Load(new StreamReader(filePath).BaseStream, Manager.GetAudioFormat(filePath), filePath, doStream, loadInBackground, true);
			if (useCache)
			{
				Manager.cache[filePath] = audioClip;
			}
			return audioClip;
		}

		// Token: 0x06000F4A RID: 3914 RVA: 0x000605DC File Offset: 0x0005E7DC
		public static AudioClip Load(Stream dataStream, AudioFormat audioFormat, string unityAudioClipName, bool doStream = false, bool loadInBackground = true, bool diposeDataStreamIfNotNeeded = true)
		{
			AudioClip audioClip = null;
			CustomAudioFileReader reader = null;
			try
			{
				reader = new CustomAudioFileReader(dataStream, audioFormat);
				Manager.AudioInstance audioInstance = new Manager.AudioInstance
				{
					reader = reader,
					samplesCount = (int)(reader.Length / (long)(reader.WaveFormat.BitsPerSample / 8))
				};
				if (doStream)
				{
					audioClip = AudioClip.Create(unityAudioClipName, audioInstance.samplesCount / audioInstance.channels, audioInstance.channels, audioInstance.sampleRate, doStream, delegate(float[] target)
					{
						reader.Read(target, 0, target.Length);
					}, delegate(int target)
					{
						reader.Seek((long)target, SeekOrigin.Begin);
					});
					audioInstance.audioClip = audioClip;
					Manager.SetAudioClipLoadType(audioInstance, AudioClipLoadType.Streaming);
					Manager.SetAudioClipLoadState(audioInstance, AudioDataLoadState.Loaded);
				}
				else
				{
					audioClip = AudioClip.Create(unityAudioClipName, audioInstance.samplesCount / audioInstance.channels, audioInstance.channels, audioInstance.sampleRate, doStream);
					audioInstance.audioClip = audioClip;
					if (diposeDataStreamIfNotNeeded)
					{
						audioInstance.streamToDisposeOnceDone = dataStream;
					}
					Manager.SetAudioClipLoadType(audioInstance, AudioClipLoadType.DecompressOnLoad);
					Manager.SetAudioClipLoadState(audioInstance, AudioDataLoadState.Loading);
					if (loadInBackground)
					{
						Queue<Manager.AudioInstance> queue = Manager.deferredLoadQueue;
						lock (queue)
						{
							Manager.deferredLoadQueue.Enqueue(audioInstance);
						}
						Manager.RunDeferredLoaderThread();
						Manager.EnsureInstanceExists();
					}
					else
					{
						audioInstance.dataToSet = new float[audioInstance.samplesCount];
						audioInstance.reader.Read(audioInstance.dataToSet, 0, audioInstance.dataToSet.Length);
						audioInstance.audioClip.SetData(audioInstance.dataToSet, 0);
						Manager.SetAudioClipLoadState(audioInstance, AudioDataLoadState.Loaded);
					}
				}
			}
			catch (Exception ex)
			{
				Manager.SetAudioClipLoadState(audioClip, AudioDataLoadState.Failed);
				string text = "Could not load AudioClip named '";
				string text2 = "', exception:";
				Exception ex2 = ex;
				Debug.LogError(text + unityAudioClipName + text2 + ((ex2 != null) ? ex2.ToString() : null));
			}
			return audioClip;
		}

		// Token: 0x06000F4B RID: 3915 RVA: 0x000607D4 File Offset: 0x0005E9D4
		private static void RunDeferredLoaderThread()
		{
			if (Manager.deferredLoaderThread == null || !Manager.deferredLoaderThread.IsAlive)
			{
				Manager.deferredLoaderThread = new Thread(new ThreadStart(Manager.DeferredLoaderMain));
				Manager.deferredLoaderThread.IsBackground = true;
				Manager.deferredLoaderThread.Start();
			}
		}

		// Token: 0x06000F4C RID: 3916 RVA: 0x00060814 File Offset: 0x0005EA14
		private static void DeferredLoaderMain()
		{
			Manager.AudioInstance audioInstance = null;
			bool flag = true;
			long num = 100000L;
			while (flag || num > 0L)
			{
				num -= 1L;
				Queue<Manager.AudioInstance> queue = Manager.deferredLoadQueue;
				lock (queue)
				{
					flag = Manager.deferredLoadQueue.Count > 0;
					if (!flag)
					{
						continue;
					}
					audioInstance = Manager.deferredLoadQueue.Dequeue();
				}
				num = 100000L;
				try
				{
					audioInstance.dataToSet = new float[audioInstance.samplesCount];
					audioInstance.reader.Read(audioInstance.dataToSet, 0, audioInstance.dataToSet.Length);
					audioInstance.reader.Close();
					audioInstance.reader.Dispose();
					if (audioInstance.streamToDisposeOnceDone != null)
					{
						audioInstance.streamToDisposeOnceDone.Close();
						audioInstance.streamToDisposeOnceDone.Dispose();
						audioInstance.streamToDisposeOnceDone = null;
					}
					queue = Manager.deferredSetDataQueue;
					lock (queue)
					{
						Manager.deferredSetDataQueue.Enqueue(audioInstance);
					}
				}
				catch (Exception ex)
				{
					Debug.LogException(ex);
					queue = Manager.deferredSetFail;
					lock (queue)
					{
						Manager.deferredSetFail.Enqueue(audioInstance);
					}
				}
			}
		}

		// Token: 0x06000F4D RID: 3917 RVA: 0x00060980 File Offset: 0x0005EB80
		private void Update()
		{
			Manager.AudioInstance audioInstance = null;
			bool flag = true;
			Queue<Manager.AudioInstance> queue;
			while (flag)
			{
				queue = Manager.deferredSetDataQueue;
				lock (queue)
				{
					flag = Manager.deferredSetDataQueue.Count > 0;
					if (!flag)
					{
						break;
					}
					audioInstance = Manager.deferredSetDataQueue.Dequeue();
				}
				audioInstance.audioClip.SetData(audioInstance.dataToSet, 0);
				Manager.SetAudioClipLoadState(audioInstance, AudioDataLoadState.Loaded);
				audioInstance.audioClip = null;
				audioInstance.dataToSet = null;
			}
			queue = Manager.deferredSetFail;
			lock (queue)
			{
				while (Manager.deferredSetFail.Count > 0)
				{
					audioInstance = Manager.deferredSetFail.Dequeue();
					Manager.SetAudioClipLoadState(audioInstance, AudioDataLoadState.Failed);
				}
			}
		}

		// Token: 0x06000F4E RID: 3918 RVA: 0x00060A5C File Offset: 0x0005EC5C
		private static void EnsureInstanceExists()
		{
			if (!Manager.managerInstance)
			{
				Manager.managerInstance = new GameObject("Runtime AudioClip Loader Manger singleton instance");
				Manager.managerInstance.hideFlags = HideFlags.HideAndDontSave;
				Manager.managerInstance.AddComponent<Manager>();
			}
		}

		// Token: 0x06000F4F RID: 3919 RVA: 0x00060A90 File Offset: 0x0005EC90
		public static void SetAudioClipLoadState(AudioClip audioClip, AudioDataLoadState newLoadState)
		{
			Manager.audioLoadState[audioClip] = newLoadState;
		}

		// Token: 0x06000F50 RID: 3920 RVA: 0x00060AA0 File Offset: 0x0005ECA0
		public static AudioDataLoadState GetAudioClipLoadState(AudioClip audioClip)
		{
			AudioDataLoadState audioDataLoadState = AudioDataLoadState.Failed;
			if (audioClip != null)
			{
				audioDataLoadState = audioClip.loadState;
				Manager.audioLoadState.TryGetValue(audioClip, out audioDataLoadState);
			}
			return audioDataLoadState;
		}

		// Token: 0x06000F51 RID: 3921 RVA: 0x00060ACE File Offset: 0x0005ECCE
		public static void SetAudioClipLoadType(AudioClip audioClip, AudioClipLoadType newLoadType)
		{
			Manager.audioClipLoadType[audioClip] = newLoadType;
		}

		// Token: 0x06000F52 RID: 3922 RVA: 0x00060ADC File Offset: 0x0005ECDC
		public static AudioClipLoadType GetAudioClipLoadType(AudioClip audioClip)
		{
			AudioClipLoadType audioClipLoadType = (AudioClipLoadType)(-1);
			if (audioClip != null)
			{
				audioClipLoadType = audioClip.loadType;
				Manager.audioClipLoadType.TryGetValue(audioClip, out audioClipLoadType);
			}
			return audioClipLoadType;
		}

		// Token: 0x06000F53 RID: 3923 RVA: 0x00060B0A File Offset: 0x0005ED0A
		private static string GetExtension(string filePath)
		{
			return Path.GetExtension(filePath).Substring(1).ToLower();
		}

		// Token: 0x06000F54 RID: 3924 RVA: 0x00060B1D File Offset: 0x0005ED1D
		public static bool IsSupportedFormat(string filePath)
		{
			return Manager.supportedFormats.Contains(Manager.GetExtension(filePath));
		}

		// Token: 0x06000F55 RID: 3925 RVA: 0x00060B30 File Offset: 0x0005ED30
		public static AudioFormat GetAudioFormat(string filePath)
		{
			AudioFormat audioFormat = AudioFormat.unknown;
			try
			{
				audioFormat = (AudioFormat)Enum.Parse(typeof(AudioFormat), Manager.GetExtension(filePath), true);
			}
			catch
			{
			}
			return audioFormat;
		}

		// Token: 0x06000F56 RID: 3926 RVA: 0x00060B74 File Offset: 0x0005ED74
		public static void ClearCache()
		{
			Manager.cache.Clear();
		}

		// Token: 0x04000A7F RID: 2687
		private static readonly string[] supportedFormats = Enum.GetNames(typeof(AudioFormat));

		// Token: 0x04000A80 RID: 2688
		private static Dictionary<string, AudioClip> cache = new Dictionary<string, AudioClip>();

		// Token: 0x04000A81 RID: 2689
		private static Queue<Manager.AudioInstance> deferredLoadQueue = new Queue<Manager.AudioInstance>();

		// Token: 0x04000A82 RID: 2690
		private static Queue<Manager.AudioInstance> deferredSetDataQueue = new Queue<Manager.AudioInstance>();

		// Token: 0x04000A83 RID: 2691
		private static Queue<Manager.AudioInstance> deferredSetFail = new Queue<Manager.AudioInstance>();

		// Token: 0x04000A84 RID: 2692
		private static Thread deferredLoaderThread;

		// Token: 0x04000A85 RID: 2693
		private static GameObject managerInstance;

		// Token: 0x04000A86 RID: 2694
		private static Dictionary<AudioClip, AudioClipLoadType> audioClipLoadType = new Dictionary<AudioClip, AudioClipLoadType>();

		// Token: 0x04000A87 RID: 2695
		private static Dictionary<AudioClip, AudioDataLoadState> audioLoadState = new Dictionary<AudioClip, AudioDataLoadState>();

		// Token: 0x0200161A RID: 5658
		private class AudioInstance
		{
			// Token: 0x17000D12 RID: 3346
			// (get) Token: 0x0600D6D4 RID: 54996 RVA: 0x004FA2CD File Offset: 0x004F84CD
			public int channels
			{
				get
				{
					return this.reader.WaveFormat.Channels;
				}
			}

			// Token: 0x17000D13 RID: 3347
			// (get) Token: 0x0600D6D5 RID: 54997 RVA: 0x004FA2DF File Offset: 0x004F84DF
			public int sampleRate
			{
				get
				{
					return this.reader.WaveFormat.SampleRate;
				}
			}

			// Token: 0x0600D6D6 RID: 54998 RVA: 0x004FA2F1 File Offset: 0x004F84F1
			public static implicit operator AudioClip(Manager.AudioInstance ai)
			{
				return ai.audioClip;
			}

			// Token: 0x0400591F RID: 22815
			public AudioClip audioClip;

			// Token: 0x04005920 RID: 22816
			public CustomAudioFileReader reader;

			// Token: 0x04005921 RID: 22817
			public float[] dataToSet;

			// Token: 0x04005922 RID: 22818
			public int samplesCount;

			// Token: 0x04005923 RID: 22819
			public Stream streamToDisposeOnceDone;
		}
	}
}
