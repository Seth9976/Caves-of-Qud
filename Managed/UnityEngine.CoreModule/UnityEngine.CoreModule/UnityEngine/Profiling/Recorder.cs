using System;
using Unity.Profiling;
using Unity.Profiling.LowLevel;
using Unity.Profiling.LowLevel.Unsafe;
using UnityEngine.Scripting;

namespace UnityEngine.Profiling
{
	// Token: 0x020002FF RID: 767
	[UsedByNativeCode]
	public sealed class Recorder
	{
		// Token: 0x06002635 RID: 9781 RVA: 0x00030B93 File Offset: 0x0002ED93
		internal Recorder()
		{
		}

		// Token: 0x06002636 RID: 9782 RVA: 0x00049AF0 File Offset: 0x00047CF0
		internal Recorder(ProfilerRecorderHandle handle)
		{
			bool flag = !handle.Valid;
			if (!flag)
			{
				this.m_RecorderCPU = new ProfilerRecorder(handle, 1, (ProfilerRecorderOptions)153);
				bool flag2 = (ProfilerRecorderHandle.GetDescription(handle).Flags & MarkerFlags.SampleGPU) > MarkerFlags.Default;
				if (flag2)
				{
					this.m_RecorderGPU = new ProfilerRecorder(handle, 1, (ProfilerRecorderOptions)217);
				}
			}
		}

		// Token: 0x06002637 RID: 9783 RVA: 0x00049B54 File Offset: 0x00047D54
		~Recorder()
		{
			this.m_RecorderCPU.Dispose();
			this.m_RecorderGPU.Dispose();
		}

		// Token: 0x06002638 RID: 9784 RVA: 0x00049B98 File Offset: 0x00047D98
		public static Recorder Get(string samplerName)
		{
			ProfilerRecorderHandle profilerRecorderHandle = ProfilerRecorderHandle.Get(ProfilerCategory.Any, samplerName);
			bool flag = !profilerRecorderHandle.Valid;
			Recorder recorder;
			if (flag)
			{
				recorder = Recorder.s_InvalidRecorder;
			}
			else
			{
				recorder = new Recorder(profilerRecorderHandle);
			}
			return recorder;
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x06002639 RID: 9785 RVA: 0x00049BD4 File Offset: 0x00047DD4
		public bool isValid
		{
			get
			{
				return this.m_RecorderCPU.handle > 0UL;
			}
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x0600263A RID: 9786 RVA: 0x00049BF8 File Offset: 0x00047DF8
		// (set) Token: 0x0600263B RID: 9787 RVA: 0x00049C15 File Offset: 0x00047E15
		public bool enabled
		{
			get
			{
				return this.m_RecorderCPU.IsRunning;
			}
			set
			{
				this.SetEnabled(value);
			}
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x0600263C RID: 9788 RVA: 0x00049C20 File Offset: 0x00047E20
		public long elapsedNanoseconds
		{
			get
			{
				bool flag = !this.m_RecorderCPU.Valid;
				long num;
				if (flag)
				{
					num = 0L;
				}
				else
				{
					num = this.m_RecorderCPU.LastValue;
				}
				return num;
			}
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x0600263D RID: 9789 RVA: 0x00049C54 File Offset: 0x00047E54
		public long gpuElapsedNanoseconds
		{
			get
			{
				bool flag = !this.m_RecorderGPU.Valid;
				long num;
				if (flag)
				{
					num = 0L;
				}
				else
				{
					num = this.m_RecorderGPU.LastValue;
				}
				return num;
			}
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x0600263E RID: 9790 RVA: 0x00049C88 File Offset: 0x00047E88
		public int sampleBlockCount
		{
			get
			{
				bool flag = !this.m_RecorderCPU.Valid;
				int num;
				if (flag)
				{
					num = 0;
				}
				else
				{
					bool flag2 = this.m_RecorderCPU.Count != 1;
					if (flag2)
					{
						num = 0;
					}
					else
					{
						num = (int)this.m_RecorderCPU.GetSample(0).Count;
					}
				}
				return num;
			}
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x0600263F RID: 9791 RVA: 0x00049CE0 File Offset: 0x00047EE0
		public int gpuSampleBlockCount
		{
			get
			{
				bool flag = !this.m_RecorderGPU.Valid;
				int num;
				if (flag)
				{
					num = 0;
				}
				else
				{
					bool flag2 = this.m_RecorderGPU.Count != 1;
					if (flag2)
					{
						num = 0;
					}
					else
					{
						num = (int)this.m_RecorderGPU.GetSample(0).Count;
					}
				}
				return num;
			}
		}

		// Token: 0x06002640 RID: 9792 RVA: 0x00049D38 File Offset: 0x00047F38
		public void FilterToCurrentThread()
		{
			bool flag = !this.m_RecorderCPU.Valid;
			if (!flag)
			{
				this.m_RecorderCPU.FilterToCurrentThread();
			}
		}

		// Token: 0x06002641 RID: 9793 RVA: 0x00049D68 File Offset: 0x00047F68
		public void CollectFromAllThreads()
		{
			bool flag = !this.m_RecorderCPU.Valid;
			if (!flag)
			{
				this.m_RecorderCPU.CollectFromAllThreads();
			}
		}

		// Token: 0x06002642 RID: 9794 RVA: 0x00049D98 File Offset: 0x00047F98
		private void SetEnabled(bool state)
		{
			if (state)
			{
				this.m_RecorderCPU.Start();
				bool valid = this.m_RecorderGPU.Valid;
				if (valid)
				{
					this.m_RecorderGPU.Start();
				}
			}
			else
			{
				this.m_RecorderCPU.Stop();
				bool valid2 = this.m_RecorderGPU.Valid;
				if (valid2)
				{
					this.m_RecorderGPU.Stop();
				}
			}
		}

		// Token: 0x04000AB8 RID: 2744
		private const ProfilerRecorderOptions s_RecorderDefaultOptions = (ProfilerRecorderOptions)153;

		// Token: 0x04000AB9 RID: 2745
		internal static Recorder s_InvalidRecorder = new Recorder();

		// Token: 0x04000ABA RID: 2746
		private ProfilerRecorder m_RecorderCPU;

		// Token: 0x04000ABB RID: 2747
		private ProfilerRecorder m_RecorderGPU;
	}
}
