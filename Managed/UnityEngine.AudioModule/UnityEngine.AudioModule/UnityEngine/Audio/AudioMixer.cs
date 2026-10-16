using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Audio
{
	// Token: 0x02000031 RID: 49
	[ExcludeFromPreset]
	[ExcludeFromObjectFactory]
	[NativeHeader("Modules/Audio/Public/AudioMixer.h")]
	[NativeHeader("Modules/Audio/Public/ScriptBindings/AudioMixer.bindings.h")]
	public class AudioMixer : Object
	{
		// Token: 0x060002E7 RID: 743 RVA: 0x00005913 File Offset: 0x00003B13
		internal AudioMixer()
		{
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x00005E48 File Offset: 0x00004048
		// (set) Token: 0x060002E9 RID: 745 RVA: 0x00005E70 File Offset: 0x00004070
		[NativeProperty]
		public AudioMixerGroup outputAudioMixerGroup
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioMixer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<AudioMixerGroup>(AudioMixer.get_outputAudioMixerGroup_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioMixer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioMixer.set_outputAudioMixerGroup_Injected(intPtr, Object.MarshalledUnityObject.Marshal<AudioMixerGroup>(value));
			}
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00005E98 File Offset: 0x00004098
		[NativeMethod("FindSnapshotFromName")]
		public unsafe AudioMixerSnapshot FindSnapshot(string name)
		{
			AudioMixerSnapshot audioMixerSnapshot;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioMixer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				IntPtr intPtr2 = AudioMixer.FindSnapshot_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				IntPtr intPtr2;
				audioMixerSnapshot = Unmarshal.UnmarshalUnityObject<AudioMixerSnapshot>(intPtr2);
				char* ptr = null;
			}
			return audioMixerSnapshot;
		}

		// Token: 0x060002EB RID: 747 RVA: 0x00005F08 File Offset: 0x00004108
		[NativeMethod("AudioMixerBindings::FindMatchingGroups", IsFreeFunction = true, HasExplicitThis = true)]
		public unsafe AudioMixerGroup[] FindMatchingGroups(string subPath)
		{
			AudioMixerGroup[] array;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioMixer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(subPath, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = subPath.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				array = AudioMixer.FindMatchingGroups_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return array;
		}

		// Token: 0x060002EC RID: 748 RVA: 0x00005F70 File Offset: 0x00004170
		internal void TransitionToSnapshot(AudioMixerSnapshot snapshot, float timeToReach)
		{
			bool flag = snapshot == null;
			if (flag)
			{
				throw new ArgumentException("null Snapshot passed to AudioMixer.TransitionToSnapshot of AudioMixer '" + base.name + "'");
			}
			bool flag2 = snapshot.audioMixer != this;
			if (flag2)
			{
				throw new ArgumentException(string.Concat(new string[] { "Snapshot '", snapshot.name, "' passed to AudioMixer.TransitionToSnapshot is not a snapshot from AudioMixer '", base.name, "'" }));
			}
			this.TransitionToSnapshotInternal(snapshot, timeToReach);
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00005FF8 File Offset: 0x000041F8
		[NativeMethod("TransitionToSnapshot")]
		private void TransitionToSnapshotInternal(AudioMixerSnapshot snapshot, float timeToReach)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioMixer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			AudioMixer.TransitionToSnapshotInternal_Injected(intPtr, Object.MarshalledUnityObject.Marshal<AudioMixerSnapshot>(snapshot), timeToReach);
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00006024 File Offset: 0x00004224
		[NativeMethod("AudioMixerBindings::TransitionToSnapshots", IsFreeFunction = true, HasExplicitThis = true, ThrowsException = true)]
		public unsafe void TransitionToSnapshots(AudioMixerSnapshot[] snapshots, float[] weights, float timeToReach)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioMixer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<float> span = new Span<float>(weights);
			fixed (float* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				AudioMixer.TransitionToSnapshots_Injected(intPtr, snapshots, ref managedSpanWrapper, timeToReach);
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060002EF RID: 751 RVA: 0x00006070 File Offset: 0x00004270
		// (set) Token: 0x060002F0 RID: 752 RVA: 0x00006094 File Offset: 0x00004294
		[NativeProperty]
		public AudioMixerUpdateMode updateMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioMixer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioMixer.get_updateMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioMixer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioMixer.set_updateMode_Injected(intPtr, value);
			}
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x000060B8 File Offset: 0x000042B8
		[NativeMethod]
		public unsafe bool SetFloat(string name, float value)
		{
			bool flag;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioMixer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				flag = AudioMixer.SetFloat_Injected(intPtr, ref managedSpanWrapper, value);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00006120 File Offset: 0x00004320
		[NativeMethod]
		public unsafe bool ClearFloat(string name)
		{
			bool flag;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioMixer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				flag = AudioMixer.ClearFloat_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00006188 File Offset: 0x00004388
		[NativeMethod]
		public unsafe bool GetFloat(string name, out float value)
		{
			bool float_Injected;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioMixer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				float_Injected = AudioMixer.GetFloat_Injected(intPtr, ref managedSpanWrapper, out value);
			}
			finally
			{
				char* ptr = null;
			}
			return float_Injected;
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x000061F0 File Offset: 0x000043F0
		[NativeMethod("AudioMixerBindings::GetAbsoluteAudibilityFromGroup", HasExplicitThis = true, IsFreeFunction = true)]
		internal float GetAbsoluteAudibilityFromGroup(AudioMixerGroup group)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioMixer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return AudioMixer.GetAbsoluteAudibilityFromGroup_Injected(intPtr, Object.MarshalledUnityObject.Marshal<AudioMixerGroup>(group));
		}

		// Token: 0x060002F5 RID: 757
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_outputAudioMixerGroup_Injected(IntPtr _unity_self);

		// Token: 0x060002F6 RID: 758
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_outputAudioMixerGroup_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x060002F7 RID: 759
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr FindSnapshot_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name);

		// Token: 0x060002F8 RID: 760
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AudioMixerGroup[] FindMatchingGroups_Injected(IntPtr _unity_self, ref ManagedSpanWrapper subPath);

		// Token: 0x060002F9 RID: 761
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void TransitionToSnapshotInternal_Injected(IntPtr _unity_self, IntPtr snapshot, float timeToReach);

		// Token: 0x060002FA RID: 762
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void TransitionToSnapshots_Injected(IntPtr _unity_self, AudioMixerSnapshot[] snapshots, ref ManagedSpanWrapper weights, float timeToReach);

		// Token: 0x060002FB RID: 763
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AudioMixerUpdateMode get_updateMode_Injected(IntPtr _unity_self);

		// Token: 0x060002FC RID: 764
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_updateMode_Injected(IntPtr _unity_self, AudioMixerUpdateMode value);

		// Token: 0x060002FD RID: 765
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetFloat_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name, float value);

		// Token: 0x060002FE RID: 766
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ClearFloat_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name);

		// Token: 0x060002FF RID: 767
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetFloat_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name, out float value);

		// Token: 0x06000300 RID: 768
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetAbsoluteAudibilityFromGroup_Injected(IntPtr _unity_self, IntPtr group);
	}
}
