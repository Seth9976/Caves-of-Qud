using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Playables
{
	// Token: 0x02000005 RID: 5
	[NativeHeader("Modules/Director/ScriptBindings/DataPlayableOutputExtensions.bindings.h")]
	[RequiredByNativeCode]
	[StaticAccessor("DataPlayableOutputBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Runtime/Director/Core/HPlayableOutput.h")]
	[NativeHeader("Runtime/Director/Core/HPlayableGraph.h")]
	[NativeHeader("Modules/Director/DataPlayableOutput.h")]
	[NativeHeader("Modules/Director/ScriptBindings/DataPlayableOutput.bindings.h")]
	internal struct DataPlayableOutput : IPlayableOutput
	{
		// Token: 0x06000011 RID: 17 RVA: 0x00002280 File Offset: 0x00000480
		public Type GetStreamType()
		{
			return DataPlayableOutput.InternalGetType(ref this.m_Handle);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022A0 File Offset: 0x000004A0
		public bool GetConnectionChanged()
		{
			return DataPlayableOutput.InternalGetConnectionChanged(ref this.m_Handle);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000022BD File Offset: 0x000004BD
		public void ClearConnectionChanged()
		{
			DataPlayableOutput.InternalClearConnectionChanged(ref this.m_Handle);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000022CC File Offset: 0x000004CC
		public TDataStream GetDataStream<TDataStream>() where TDataStream : new()
		{
			object obj = DataPlayableOutput.InternalGetStream(ref this.m_Handle);
			bool flag = obj is TDataStream;
			TDataStream tdataStream;
			if (flag)
			{
				tdataStream = (TDataStream)((object)obj);
			}
			else
			{
				tdataStream = default(TDataStream);
			}
			return tdataStream;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000230C File Offset: 0x0000050C
		public void SetDataStream<TDataStream>(TDataStream stream) where TDataStream : new()
		{
			Type streamType = this.GetStreamType();
			bool flag = !streamType.IsAssignableFrom(typeof(TDataStream));
			if (flag)
			{
				throw new ArgumentException(string.Format("{0} is of the wrong type. This output only accepts streams with type {1} or inheriting from type {2}", "stream", streamType, streamType), "stream");
			}
			DataPlayableOutput.InternalSetStream(ref this.m_Handle, stream);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002368 File Offset: 0x00000568
		public static DataPlayableOutput Create<TDataStream>(PlayableGraph graph, string name) where TDataStream : new()
		{
			PlayableOutputHandle playableOutputHandle;
			bool flag = !DataPlayableOutputExtensions.InternalCreateDataOutput(ref graph, name, typeof(TDataStream), out playableOutputHandle);
			DataPlayableOutput dataPlayableOutput;
			if (flag)
			{
				dataPlayableOutput = DataPlayableOutput.Null;
			}
			else
			{
				DataPlayableOutput dataPlayableOutput2 = new DataPlayableOutput(playableOutputHandle);
				dataPlayableOutput = dataPlayableOutput2;
			}
			return dataPlayableOutput;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000023A8 File Offset: 0x000005A8
		internal DataPlayableOutput(PlayableOutputHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOutputOfType<DataPlayableOutput>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not a DataPlayableOutput.");
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000023E4 File Offset: 0x000005E4
		public static DataPlayableOutput Null
		{
			get
			{
				return new DataPlayableOutput(PlayableOutputHandle.Null);
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002400 File Offset: 0x00000600
		public PlayableOutputHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002418 File Offset: 0x00000618
		public static implicit operator PlayableOutput(DataPlayableOutput output)
		{
			return new PlayableOutput(output.GetHandle());
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002438 File Offset: 0x00000638
		public static explicit operator DataPlayableOutput(PlayableOutput output)
		{
			return new DataPlayableOutput(output.GetHandle());
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002458 File Offset: 0x00000658
		public IDataPlayer GetPlayer()
		{
			return DataPlayableOutput.InternalGetPlayer(ref this.m_Handle) as IDataPlayer;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000247A File Offset: 0x0000067A
		public void SetPlayer<TPlayer>(TPlayer player) where TPlayer : Object, IDataPlayer
		{
			DataPlayableOutput.InternalSetPlayer(ref this.m_Handle, player);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002490 File Offset: 0x00000690
		[NativeThrows]
		private static Object InternalGetPlayer(ref PlayableOutputHandle handle)
		{
			return Unmarshal.UnmarshalUnityObject<Object>(DataPlayableOutput.InternalGetPlayer_Injected(ref handle));
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000024A8 File Offset: 0x000006A8
		[NativeThrows]
		private static void InternalSetPlayer(ref PlayableOutputHandle handle, Object player)
		{
			DataPlayableOutput.InternalSetPlayer_Injected(ref handle, Object.MarshalledUnityObject.Marshal<Object>(player));
		}

		// Token: 0x06000020 RID: 32
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Type InternalGetType(ref PlayableOutputHandle handle);

		// Token: 0x06000021 RID: 33
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetStream(ref PlayableOutputHandle handle, object stream);

		// Token: 0x06000022 RID: 34
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern object InternalGetStream(ref PlayableOutputHandle handle);

		// Token: 0x06000023 RID: 35
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool InternalGetConnectionChanged(ref PlayableOutputHandle handle);

		// Token: 0x06000024 RID: 36
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalClearConnectionChanged(ref PlayableOutputHandle handle);

		// Token: 0x06000025 RID: 37 RVA: 0x000024C4 File Offset: 0x000006C4
		[RequiredByNativeCode]
		private static void Internal_CallOnPlayerChanged(PlayableOutputHandle handle, object previousPlayer, object currentPlayer)
		{
			DataPlayableOutput dataPlayableOutput = new DataPlayableOutput(handle);
			IDataPlayer dataPlayer = previousPlayer as IDataPlayer;
			bool flag = dataPlayer != null;
			if (flag)
			{
				dataPlayer.Release(dataPlayableOutput);
			}
			IDataPlayer dataPlayer2 = currentPlayer as IDataPlayer;
			bool flag2 = dataPlayer2 != null;
			if (flag2)
			{
				dataPlayer2.Bind(dataPlayableOutput);
			}
		}

		// Token: 0x06000026 RID: 38
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr InternalGetPlayer_Injected(ref PlayableOutputHandle handle);

		// Token: 0x06000027 RID: 39
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetPlayer_Injected(ref PlayableOutputHandle handle, IntPtr player);

		// Token: 0x04000003 RID: 3
		private PlayableOutputHandle m_Handle;
	}
}
