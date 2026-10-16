using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Playables
{
	// Token: 0x02000009 RID: 9
	[StaticAccessor("PlayableSystemsBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Modules/Director/ScriptBindings/PlayableSystems.bindings.h")]
	internal static class PlayableSystems
	{
		// Token: 0x06000084 RID: 132 RVA: 0x00002D29 File Offset: 0x00000F29
		public static void RegisterSystemPhaseDelegate<TDataStream>(PlayableSystems.PlayableSystemStage stage, PlayableSystems.PlayableSystemDelegate systemDelegate) where TDataStream : new()
		{
			PlayableSystems.RegisterSystemPhaseDelegate(typeof(TDataStream), stage, systemDelegate);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00002D40 File Offset: 0x00000F40
		private static void RegisterSystemPhaseDelegate(Type streamType, PlayableSystems.PlayableSystemStage stage, PlayableSystems.PlayableSystemDelegate systemDelegate)
		{
			int num = PlayableSystems.RegisterStreamStage(streamType, (int)stage);
			try
			{
				PlayableSystems.s_RWLock.EnterWriteLock();
				PlayableSystems.s_SystemTypes.TryAdd(num, streamType);
				int num2 = PlayableSystems.CombineTypeAndIndex(num, stage);
				bool flag = !PlayableSystems.s_Delegates.TryAdd(num2, systemDelegate);
				if (flag)
				{
					PlayableSystems.s_Delegates[num2] = systemDelegate;
				}
			}
			finally
			{
				PlayableSystems.s_RWLock.ExitWriteLock();
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00002DBC File Offset: 0x00000FBC
		private static int CombineTypeAndIndex(int typeIndex, PlayableSystems.PlayableSystemStage stage)
		{
			return (typeIndex << 16) | (int)stage;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00002DD4 File Offset: 0x00000FD4
		[RequiredByNativeCode]
		private unsafe static bool Internal_CallSystemDelegate(int systemIndex, PlayableSystems.PlayableSystemStage stage, IntPtr outputsPtr, int numOutputs)
		{
			PlayableOutputHandle* ptr = (PlayableOutputHandle*)(void*)outputsPtr;
			int num = PlayableSystems.CombineTypeAndIndex(systemIndex, stage);
			bool flag = false;
			PlayableSystems.PlayableSystemDelegate playableSystemDelegate = null;
			PlayableSystems.s_RWLock.EnterReadLock();
			Type type;
			bool flag2 = PlayableSystems.s_SystemTypes.TryGetValue(systemIndex, out type);
			bool flag3 = flag2;
			if (flag3)
			{
				flag = PlayableSystems.s_Delegates.TryGetValue(num, out playableSystemDelegate) && playableSystemDelegate != null;
			}
			PlayableSystems.s_RWLock.ExitReadLock();
			bool flag4 = !flag2 || !flag;
			bool flag5;
			if (flag4)
			{
				flag5 = false;
			}
			else
			{
				PlayableSystems.DataPlayableOutputList dataPlayableOutputList = new PlayableSystems.DataPlayableOutputList(ptr, numOutputs);
				playableSystemDelegate(dataPlayableOutputList);
				flag5 = true;
			}
			return flag5;
		}

		// Token: 0x06000088 RID: 136
		[ThreadAndSerializationSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int RegisterStreamStage(Type streamType, int stage);

		// Token: 0x04000007 RID: 7
		private static Dictionary<int, Type> s_SystemTypes = new Dictionary<int, Type>();

		// Token: 0x04000008 RID: 8
		private static Dictionary<int, PlayableSystems.PlayableSystemDelegate> s_Delegates = new Dictionary<int, PlayableSystems.PlayableSystemDelegate>();

		// Token: 0x04000009 RID: 9
		private static ReaderWriterLockSlim s_RWLock = new ReaderWriterLockSlim();

		// Token: 0x0200000A RID: 10
		// (Invoke) Token: 0x0600008B RID: 139
		public delegate void PlayableSystemDelegate(IReadOnlyList<DataPlayableOutput> outputs);

		// Token: 0x0200000B RID: 11
		public enum PlayableSystemStage : ushort
		{
			// Token: 0x0400000B RID: 11
			FixedUpdate,
			// Token: 0x0400000C RID: 12
			FixedUpdatePostPhysics,
			// Token: 0x0400000D RID: 13
			Update,
			// Token: 0x0400000E RID: 14
			AnimationBegin,
			// Token: 0x0400000F RID: 15
			AnimationEnd,
			// Token: 0x04000010 RID: 16
			LateUpdate,
			// Token: 0x04000011 RID: 17
			Render
		}

		// Token: 0x0200000C RID: 12
		private class DataPlayableOutputList : IReadOnlyList<DataPlayableOutput>, IEnumerable<DataPlayableOutput>, IEnumerable, IReadOnlyCollection<DataPlayableOutput>
		{
			// Token: 0x0600008E RID: 142 RVA: 0x00002E8D File Offset: 0x0000108D
			public unsafe DataPlayableOutputList(PlayableOutputHandle* outputs, int count)
			{
				this.m_Outputs = outputs;
				this.m_Count = count;
			}

			// Token: 0x1700000C RID: 12
			public unsafe DataPlayableOutput this[int index]
			{
				get
				{
					bool flag = index >= this.m_Count;
					if (flag)
					{
						throw new IndexOutOfRangeException(string.Format("index {0} is greater than the number of items: {1}", index, this.m_Count));
					}
					bool flag2 = index < 0;
					if (flag2)
					{
						throw new IndexOutOfRangeException("index cannot be negative");
					}
					return new DataPlayableOutput(this.m_Outputs[index]);
				}
			}

			// Token: 0x1700000D RID: 13
			// (get) Token: 0x06000090 RID: 144 RVA: 0x00002F18 File Offset: 0x00001118
			public int Count
			{
				get
				{
					return this.m_Count;
				}
			}

			// Token: 0x06000091 RID: 145 RVA: 0x00002F20 File Offset: 0x00001120
			public IEnumerator<DataPlayableOutput> GetEnumerator()
			{
				return new PlayableSystems.DataPlayableOutputList.DataPlayableOutputEnumerator(this);
			}

			// Token: 0x06000092 RID: 146 RVA: 0x00002F38 File Offset: 0x00001138
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x04000012 RID: 18
			private unsafe PlayableOutputHandle* m_Outputs;

			// Token: 0x04000013 RID: 19
			private int m_Count;

			// Token: 0x0200000D RID: 13
			private class DataPlayableOutputEnumerator : IEnumerator<DataPlayableOutput>, IEnumerator, IDisposable
			{
				// Token: 0x06000093 RID: 147 RVA: 0x00002F50 File Offset: 0x00001150
				public DataPlayableOutputEnumerator(PlayableSystems.DataPlayableOutputList list)
				{
					this.m_List = list;
					this.m_Index = -1;
				}

				// Token: 0x1700000E RID: 14
				// (get) Token: 0x06000094 RID: 148 RVA: 0x00002F68 File Offset: 0x00001168
				public DataPlayableOutput Current
				{
					get
					{
						DataPlayableOutput dataPlayableOutput;
						try
						{
							dataPlayableOutput = this.m_List[this.m_Index];
						}
						catch (IndexOutOfRangeException)
						{
							throw new InvalidOperationException("Enumeration has either not started or has already finished.");
						}
						return dataPlayableOutput;
					}
				}

				// Token: 0x1700000F RID: 15
				// (get) Token: 0x06000095 RID: 149 RVA: 0x00002FAC File Offset: 0x000011AC
				object IEnumerator.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x06000096 RID: 150 RVA: 0x00002FB9 File Offset: 0x000011B9
				public void Dispose()
				{
					this.m_List = null;
				}

				// Token: 0x06000097 RID: 151 RVA: 0x00002FC4 File Offset: 0x000011C4
				public bool MoveNext()
				{
					this.m_Index++;
					return this.m_Index < this.m_List.Count;
				}

				// Token: 0x06000098 RID: 152 RVA: 0x00002FF7 File Offset: 0x000011F7
				public void Reset()
				{
					this.m_Index = -1;
				}

				// Token: 0x04000014 RID: 20
				private PlayableSystems.DataPlayableOutputList m_List;

				// Token: 0x04000015 RID: 21
				private int m_Index;
			}
		}
	}
}
