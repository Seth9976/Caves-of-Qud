using System;

namespace UnityEngine.Playables
{
	// Token: 0x02000003 RID: 3
	internal struct DataPlayable<TPayload> : IPlayable, IEquatable<DataPlayable<TPayload>> where TPayload : struct
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002068 File Offset: 0x00000268
		public static DataPlayable<TPayload> Null
		{
			get
			{
				return DataPlayable<TPayload>.m_NullPlayable;
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002080 File Offset: 0x00000280
		public static DataPlayable<TPayload> Create(PlayableGraph graph, int inputCount = 0)
		{
			return DataPlayable<TPayload>.Create(graph, default(TPayload), inputCount);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020A4 File Offset: 0x000002A4
		public static DataPlayable<TPayload> Create(PlayableGraph graph, TPayload payload, int inputCount = 0)
		{
			PlayableHandle playableHandle = DataPlayable<TPayload>.CreateHandle(graph, payload, inputCount);
			return new DataPlayable<TPayload>(playableHandle);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020C8 File Offset: 0x000002C8
		private static PlayableHandle CreateHandle(PlayableGraph graph, TPayload payload, int inputCount)
		{
			PlayableHandle @null = PlayableHandle.Null;
			bool flag = !DataPlayableBindings.CreateHandleInternal(graph, ref @null);
			PlayableHandle playableHandle;
			if (flag)
			{
				playableHandle = PlayableHandle.Null;
			}
			else
			{
				@null.SetInputCount(inputCount);
				@null.SetScriptInstance(payload);
				playableHandle = @null;
			}
			return playableHandle;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002110 File Offset: 0x00000310
		internal DataPlayable(PlayableHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = typeof(TPayload) != handle.GetPlayableType();
				if (flag2)
				{
					throw new InvalidCastException(string.Format("Incompatible handle: Trying to assign a playable data of type `{0}` that is not compatible with the Payload of type `{1}`.", handle.GetPlayableType(), typeof(TPayload)));
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002170 File Offset: 0x00000370
		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002188 File Offset: 0x00000388
		public TPayload GetPayload()
		{
			return this.m_Handle.GetPayload<TPayload>();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021A5 File Offset: 0x000003A5
		public void SetPayload(TPayload payload)
		{
			this.m_Handle.SetPayload<TPayload>(payload);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021B8 File Offset: 0x000003B8
		public static implicit operator Playable(DataPlayable<TPayload> playable)
		{
			return new Playable(playable.GetHandle());
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021D8 File Offset: 0x000003D8
		public static explicit operator DataPlayable<TPayload>(Playable playable)
		{
			return new DataPlayable<TPayload>(playable.GetHandle());
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021F8 File Offset: 0x000003F8
		public bool Equals(DataPlayable<TPayload> other)
		{
			return this.GetHandle() == other.GetHandle();
		}

		// Token: 0x04000001 RID: 1
		private PlayableHandle m_Handle;

		// Token: 0x04000002 RID: 2
		private static readonly DataPlayable<TPayload> m_NullPlayable = new DataPlayable<TPayload>(PlayableHandle.Null);
	}
}
