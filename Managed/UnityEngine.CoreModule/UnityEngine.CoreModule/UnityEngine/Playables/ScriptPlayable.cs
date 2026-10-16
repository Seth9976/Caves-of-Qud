using System;

namespace UnityEngine.Playables
{
	// Token: 0x0200045C RID: 1116
	public struct ScriptPlayable<T> : IPlayable, IEquatable<ScriptPlayable<T>> where T : class, IPlayableBehaviour, new()
	{
		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x06002B50 RID: 11088 RVA: 0x00052B9C File Offset: 0x00050D9C
		public static ScriptPlayable<T> Null
		{
			get
			{
				return ScriptPlayable<T>.m_NullPlayable;
			}
		}

		// Token: 0x06002B51 RID: 11089 RVA: 0x00052BB4 File Offset: 0x00050DB4
		public static ScriptPlayable<T> Create(PlayableGraph graph, int inputCount = 0)
		{
			PlayableHandle playableHandle = ScriptPlayable<T>.CreateHandle(graph, default(T), inputCount);
			return new ScriptPlayable<T>(playableHandle);
		}

		// Token: 0x06002B52 RID: 11090 RVA: 0x00052BE0 File Offset: 0x00050DE0
		public static ScriptPlayable<T> Create(PlayableGraph graph, T template, int inputCount = 0)
		{
			PlayableHandle playableHandle = ScriptPlayable<T>.CreateHandle(graph, template, inputCount);
			return new ScriptPlayable<T>(playableHandle);
		}

		// Token: 0x06002B53 RID: 11091 RVA: 0x00052C04 File Offset: 0x00050E04
		private static PlayableHandle CreateHandle(PlayableGraph graph, T template, int inputCount)
		{
			bool flag = template == null;
			object obj;
			if (flag)
			{
				obj = ScriptPlayable<T>.CreateScriptInstance();
			}
			else
			{
				obj = ScriptPlayable<T>.CloneScriptInstance(template);
			}
			bool flag2 = obj == null;
			PlayableHandle playableHandle;
			if (flag2)
			{
				string text = "Could not create a ScriptPlayable of Type ";
				Type typeFromHandle = typeof(T);
				Debug.LogError(text + ((typeFromHandle != null) ? typeFromHandle.ToString() : null));
				playableHandle = PlayableHandle.Null;
			}
			else
			{
				PlayableHandle playableHandle2 = graph.CreatePlayableHandle();
				bool flag3 = !playableHandle2.IsValid();
				if (flag3)
				{
					playableHandle = PlayableHandle.Null;
				}
				else
				{
					playableHandle2.SetInputCount(inputCount);
					playableHandle2.SetScriptInstance(obj);
					playableHandle = playableHandle2;
				}
			}
			return playableHandle;
		}

		// Token: 0x06002B54 RID: 11092 RVA: 0x00052CAC File Offset: 0x00050EAC
		private static object CreateScriptInstance()
		{
			bool flag = typeof(ScriptableObject).IsAssignableFrom(typeof(T));
			IPlayableBehaviour playableBehaviour;
			if (flag)
			{
				playableBehaviour = ScriptableObject.CreateInstance(typeof(T)) as T;
			}
			else
			{
				playableBehaviour = new T();
			}
			return playableBehaviour;
		}

		// Token: 0x06002B55 RID: 11093 RVA: 0x00052D0C File Offset: 0x00050F0C
		private static object CloneScriptInstance(IPlayableBehaviour source)
		{
			Object @object = source as Object;
			bool flag = @object != null;
			object obj;
			if (flag)
			{
				obj = ScriptPlayable<T>.CloneScriptInstanceFromEngineObject(@object);
			}
			else
			{
				ICloneable cloneable = source as ICloneable;
				bool flag2 = cloneable != null;
				if (flag2)
				{
					obj = ScriptPlayable<T>.CloneScriptInstanceFromIClonable(cloneable);
				}
				else
				{
					obj = null;
				}
			}
			return obj;
		}

		// Token: 0x06002B56 RID: 11094 RVA: 0x00052D54 File Offset: 0x00050F54
		private static object CloneScriptInstanceFromEngineObject(Object source)
		{
			Object @object = Object.Instantiate(source);
			bool flag = @object != null;
			if (flag)
			{
				@object.hideFlags |= HideFlags.DontSave;
			}
			return @object;
		}

		// Token: 0x06002B57 RID: 11095 RVA: 0x00052D8C File Offset: 0x00050F8C
		private static object CloneScriptInstanceFromIClonable(ICloneable source)
		{
			return source.Clone();
		}

		// Token: 0x06002B58 RID: 11096 RVA: 0x00052DA4 File Offset: 0x00050FA4
		internal ScriptPlayable(PlayableHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !typeof(T).IsAssignableFrom(handle.GetPlayableType());
				if (flag2)
				{
					throw new InvalidCastException(string.Format("Incompatible handle: Trying to assign a playable data of type `{0}` that is not compatible with the PlayableBehaviour of type `{1}`.", handle.GetPlayableType(), typeof(T)));
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x06002B59 RID: 11097 RVA: 0x00052E04 File Offset: 0x00051004
		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x06002B5A RID: 11098 RVA: 0x00052E1C File Offset: 0x0005101C
		public T GetBehaviour()
		{
			return this.m_Handle.GetObject<T>();
		}

		// Token: 0x06002B5B RID: 11099 RVA: 0x00052E3C File Offset: 0x0005103C
		public static implicit operator Playable(ScriptPlayable<T> playable)
		{
			return new Playable(playable.GetHandle());
		}

		// Token: 0x06002B5C RID: 11100 RVA: 0x00052E5C File Offset: 0x0005105C
		public static explicit operator ScriptPlayable<T>(Playable playable)
		{
			return new ScriptPlayable<T>(playable.GetHandle());
		}

		// Token: 0x06002B5D RID: 11101 RVA: 0x00052E7C File Offset: 0x0005107C
		public bool Equals(ScriptPlayable<T> other)
		{
			return this.GetHandle() == other.GetHandle();
		}

		// Token: 0x04000C3D RID: 3133
		private PlayableHandle m_Handle;

		// Token: 0x04000C3E RID: 3134
		private static readonly ScriptPlayable<T> m_NullPlayable = new ScriptPlayable<T>(PlayableHandle.Null);
	}
}
