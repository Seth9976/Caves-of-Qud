using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000013 RID: 19
	[RequiredByNativeCode]
	[Serializable]
	public sealed class AnimationEvent
	{
		// Token: 0x060000B4 RID: 180 RVA: 0x0000343C File Offset: 0x0000163C
		public AnimationEvent()
		{
			this.m_Time = 0f;
			this.m_FunctionName = "";
			this.m_StringParameter = "";
			this.m_ObjectReferenceParameter = null;
			this.m_FloatParameter = 0f;
			this.m_IntParameter = 0;
			this.m_MessageOptions = 0;
			this.m_Source = AnimationEventSource.NoSource;
			this.m_StateSender = null;
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x000034A0 File Offset: 0x000016A0
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x000034B8 File Offset: 0x000016B8
		[Obsolete("Use stringParameter instead")]
		public string data
		{
			get
			{
				return this.m_StringParameter;
			}
			set
			{
				this.m_StringParameter = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x000034C4 File Offset: 0x000016C4
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x000034B8 File Offset: 0x000016B8
		public string stringParameter
		{
			get
			{
				return this.m_StringParameter;
			}
			set
			{
				this.m_StringParameter = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x000034DC File Offset: 0x000016DC
		// (set) Token: 0x060000BA RID: 186 RVA: 0x000034F4 File Offset: 0x000016F4
		public float floatParameter
		{
			get
			{
				return this.m_FloatParameter;
			}
			set
			{
				this.m_FloatParameter = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00003500 File Offset: 0x00001700
		// (set) Token: 0x060000BC RID: 188 RVA: 0x00003518 File Offset: 0x00001718
		public int intParameter
		{
			get
			{
				return this.m_IntParameter;
			}
			set
			{
				this.m_IntParameter = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00003524 File Offset: 0x00001724
		// (set) Token: 0x060000BE RID: 190 RVA: 0x0000353C File Offset: 0x0000173C
		public Object objectReferenceParameter
		{
			get
			{
				return this.m_ObjectReferenceParameter;
			}
			set
			{
				this.m_ObjectReferenceParameter = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00003548 File Offset: 0x00001748
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x00003560 File Offset: 0x00001760
		public string functionName
		{
			get
			{
				return this.m_FunctionName;
			}
			set
			{
				this.m_FunctionName = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x0000356C File Offset: 0x0000176C
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x00003584 File Offset: 0x00001784
		public float time
		{
			get
			{
				return this.m_Time;
			}
			set
			{
				this.m_Time = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00003590 File Offset: 0x00001790
		// (set) Token: 0x060000C4 RID: 196 RVA: 0x000035A8 File Offset: 0x000017A8
		public SendMessageOptions messageOptions
		{
			get
			{
				return (SendMessageOptions)this.m_MessageOptions;
			}
			set
			{
				this.m_MessageOptions = (int)value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x000035B4 File Offset: 0x000017B4
		public bool isFiredByLegacy
		{
			get
			{
				return this.m_Source == AnimationEventSource.Legacy;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x000035D0 File Offset: 0x000017D0
		public bool isFiredByAnimator
		{
			get
			{
				return this.m_Source == AnimationEventSource.Animator;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x000035EC File Offset: 0x000017EC
		public AnimationState animationState
		{
			get
			{
				bool flag = !this.isFiredByLegacy;
				if (flag)
				{
					Debug.LogError("AnimationEvent was not fired by Animation component, you shouldn't use AnimationEvent.animationState");
				}
				return this.m_StateSender;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x0000361C File Offset: 0x0000181C
		public AnimatorStateInfo animatorStateInfo
		{
			get
			{
				bool flag = !this.isFiredByAnimator;
				if (flag)
				{
					Debug.LogError("AnimationEvent was not fired by Animator component, you shouldn't use AnimationEvent.animatorStateInfo");
				}
				return this.m_AnimatorStateInfo;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x0000364C File Offset: 0x0000184C
		public AnimatorClipInfo animatorClipInfo
		{
			get
			{
				bool flag = !this.isFiredByAnimator;
				if (flag)
				{
					Debug.LogError("AnimationEvent was not fired by Animator component, you shouldn't use AnimationEvent.animatorClipInfo");
				}
				return this.m_AnimatorClipInfo;
			}
		}

		// Token: 0x060000CA RID: 202 RVA: 0x0000367C File Offset: 0x0000187C
		internal int GetHash()
		{
			int hashCode = this.functionName.GetHashCode();
			return 33 * hashCode + this.time.GetHashCode();
		}

		// Token: 0x04000028 RID: 40
		internal float m_Time;

		// Token: 0x04000029 RID: 41
		internal string m_FunctionName;

		// Token: 0x0400002A RID: 42
		internal string m_StringParameter;

		// Token: 0x0400002B RID: 43
		internal Object m_ObjectReferenceParameter;

		// Token: 0x0400002C RID: 44
		internal float m_FloatParameter;

		// Token: 0x0400002D RID: 45
		internal int m_IntParameter;

		// Token: 0x0400002E RID: 46
		internal int m_MessageOptions;

		// Token: 0x0400002F RID: 47
		internal AnimationEventSource m_Source;

		// Token: 0x04000030 RID: 48
		internal AnimationState m_StateSender;

		// Token: 0x04000031 RID: 49
		internal AnimatorStateInfo m_AnimatorStateInfo;

		// Token: 0x04000032 RID: 50
		internal AnimatorClipInfo m_AnimatorClipInfo;
	}
}
