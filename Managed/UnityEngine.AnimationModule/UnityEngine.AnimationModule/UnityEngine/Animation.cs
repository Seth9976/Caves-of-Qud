using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x0200000E RID: 14
	[NativeHeader("Modules/Animation/Animation.h")]
	public sealed class Animation : Behaviour, IEnumerable
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002068 File Offset: 0x00000268
		// (set) Token: 0x06000015 RID: 21 RVA: 0x00002090 File Offset: 0x00000290
		public AnimationClip clip
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animation>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<AnimationClip>(Animation.get_clip_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animation>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Animation.set_clip_Injected(intPtr, Object.MarshalledUnityObject.Marshal<AnimationClip>(value));
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000020B8 File Offset: 0x000002B8
		// (set) Token: 0x06000017 RID: 23 RVA: 0x000020DC File Offset: 0x000002DC
		public bool playAutomatically
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animation>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animation.get_playAutomatically_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animation>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Animation.set_playAutomatically_Injected(intPtr, value);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002100 File Offset: 0x00000300
		// (set) Token: 0x06000019 RID: 25 RVA: 0x00002124 File Offset: 0x00000324
		public WrapMode wrapMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animation>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animation.get_wrapMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animation>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Animation.set_wrapMode_Injected(intPtr, value);
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002148 File Offset: 0x00000348
		public void Stop()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animation>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animation.Stop_Injected(intPtr);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000216A File Offset: 0x0000036A
		public void Stop(string name)
		{
			this.StopNamed(name);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002178 File Offset: 0x00000378
		[NativeName("Stop")]
		private unsafe void StopNamed(string name)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animation>(this);
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
				Animation.StopNamed_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000021DC File Offset: 0x000003DC
		public void Rewind()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animation>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animation.Rewind_Injected(intPtr);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000021FE File Offset: 0x000003FE
		public void Rewind(string name)
		{
			this.RewindNamed(name);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000220C File Offset: 0x0000040C
		[NativeName("Rewind")]
		private unsafe void RewindNamed(string name)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animation>(this);
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
				Animation.RewindNamed_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002270 File Offset: 0x00000470
		public void Sample()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animation>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animation.Sample_Injected(intPtr);
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002294 File Offset: 0x00000494
		public bool isPlaying
		{
			[NativeName("IsPlaying")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animation>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animation.get_isPlaying_Injected(intPtr);
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000022B8 File Offset: 0x000004B8
		public unsafe bool IsPlaying(string name)
		{
			bool flag;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animation>(this);
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
				flag = Animation.IsPlaying_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x17000005 RID: 5
		public AnimationState this[string name]
		{
			get
			{
				return this.GetState(name);
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000233C File Offset: 0x0000053C
		[ExcludeFromDocs]
		public bool Play()
		{
			return this.Play(PlayMode.StopSameLayer);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002358 File Offset: 0x00000558
		public bool Play([DefaultValue("PlayMode.StopSameLayer")] PlayMode mode)
		{
			return this.PlayDefaultAnimation(mode);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002374 File Offset: 0x00000574
		[NativeName("Play")]
		private bool PlayDefaultAnimation(PlayMode mode)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animation>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Animation.PlayDefaultAnimation_Injected(intPtr, mode);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002398 File Offset: 0x00000598
		[ExcludeFromDocs]
		public bool Play(string animation)
		{
			return this.Play(animation, PlayMode.StopSameLayer);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000023B4 File Offset: 0x000005B4
		public unsafe bool Play(string animation, [DefaultValue("PlayMode.StopSameLayer")] PlayMode mode)
		{
			bool flag;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animation>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(animation, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = animation.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				flag = Animation.Play_Injected(intPtr, ref managedSpanWrapper, mode);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000241C File Offset: 0x0000061C
		[ExcludeFromDocs]
		public void CrossFade(string animation)
		{
			this.CrossFade(animation, 0.3f);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000242C File Offset: 0x0000062C
		[ExcludeFromDocs]
		public void CrossFade(string animation, float fadeLength)
		{
			this.CrossFade(animation, fadeLength, PlayMode.StopSameLayer);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000243C File Offset: 0x0000063C
		public unsafe void CrossFade(string animation, [DefaultValue("0.3F")] float fadeLength, [DefaultValue("PlayMode.StopSameLayer")] PlayMode mode)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animation>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(animation, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = animation.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				Animation.CrossFade_Injected(intPtr, ref managedSpanWrapper, fadeLength, mode);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000024A4 File Offset: 0x000006A4
		[ExcludeFromDocs]
		public void Blend(string animation)
		{
			this.Blend(animation, 1f);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000024B4 File Offset: 0x000006B4
		[ExcludeFromDocs]
		public void Blend(string animation, float targetWeight)
		{
			this.Blend(animation, targetWeight, 0.3f);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000024C8 File Offset: 0x000006C8
		public unsafe void Blend(string animation, [DefaultValue("1.0F")] float targetWeight, [DefaultValue("0.3F")] float fadeLength)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animation>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(animation, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = animation.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				Animation.Blend_Injected(intPtr, ref managedSpanWrapper, targetWeight, fadeLength);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002530 File Offset: 0x00000730
		[ExcludeFromDocs]
		public AnimationState CrossFadeQueued(string animation)
		{
			return this.CrossFadeQueued(animation, 0.3f);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002550 File Offset: 0x00000750
		[ExcludeFromDocs]
		public AnimationState CrossFadeQueued(string animation, float fadeLength)
		{
			return this.CrossFadeQueued(animation, fadeLength, QueueMode.CompleteOthers);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000256C File Offset: 0x0000076C
		[ExcludeFromDocs]
		public AnimationState CrossFadeQueued(string animation, float fadeLength, QueueMode queue)
		{
			return this.CrossFadeQueued(animation, fadeLength, queue, PlayMode.StopSameLayer);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002588 File Offset: 0x00000788
		[FreeFunction("AnimationBindings::CrossFadeQueuedImpl", HasExplicitThis = true)]
		[return: Unmarshalled]
		public unsafe AnimationState CrossFadeQueued(string animation, [DefaultValue("0.3F")] float fadeLength, [DefaultValue("QueueMode.CompleteOthers")] QueueMode queue, [DefaultValue("PlayMode.StopSameLayer")] PlayMode mode)
		{
			AnimationState animationState;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animation>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(animation, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = animation.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				animationState = Animation.CrossFadeQueued_Injected(intPtr, ref managedSpanWrapper, fadeLength, queue, mode);
			}
			finally
			{
				char* ptr = null;
			}
			return animationState;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000025F4 File Offset: 0x000007F4
		[ExcludeFromDocs]
		public AnimationState PlayQueued(string animation)
		{
			return this.PlayQueued(animation, QueueMode.CompleteOthers);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002610 File Offset: 0x00000810
		[ExcludeFromDocs]
		public AnimationState PlayQueued(string animation, QueueMode queue)
		{
			return this.PlayQueued(animation, queue, PlayMode.StopSameLayer);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000262C File Offset: 0x0000082C
		[FreeFunction("AnimationBindings::PlayQueuedImpl", HasExplicitThis = true)]
		[return: Unmarshalled]
		public unsafe AnimationState PlayQueued(string animation, [DefaultValue("QueueMode.CompleteOthers")] QueueMode queue, [DefaultValue("PlayMode.StopSameLayer")] PlayMode mode)
		{
			AnimationState animationState;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animation>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(animation, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = animation.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				animationState = Animation.PlayQueued_Injected(intPtr, ref managedSpanWrapper, queue, mode);
			}
			finally
			{
				char* ptr = null;
			}
			return animationState;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002694 File Offset: 0x00000894
		public void AddClip(AnimationClip clip, string newName)
		{
			this.AddClip(clip, newName, int.MinValue, int.MaxValue);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000026AA File Offset: 0x000008AA
		[ExcludeFromDocs]
		public void AddClip(AnimationClip clip, string newName, int firstFrame, int lastFrame)
		{
			this.AddClip(clip, newName, firstFrame, lastFrame, false);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000026BC File Offset: 0x000008BC
		public unsafe void AddClip([NotNull] AnimationClip clip, string newName, int firstFrame, int lastFrame, [DefaultValue("false")] bool addLoopFrame)
		{
			if (clip == null)
			{
				ThrowHelper.ThrowArgumentNullException(clip, "clip");
			}
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animation>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<AnimationClip>(clip);
				if (intPtr2 == 0)
				{
					ThrowHelper.ThrowArgumentNullException(clip, "clip");
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(newName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = newName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				Animation.AddClip_Injected(intPtr, intPtr2, ref managedSpanWrapper, firstFrame, lastFrame, addLoopFrame);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000274C File Offset: 0x0000094C
		public void RemoveClip([NotNull] AnimationClip clip)
		{
			if (clip == null)
			{
				ThrowHelper.ThrowArgumentNullException(clip, "clip");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animation>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<AnimationClip>(clip);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(clip, "clip");
			}
			Animation.RemoveClip_Injected(intPtr, intPtr2);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002792 File Offset: 0x00000992
		public void RemoveClip(string clipName)
		{
			this.RemoveClipNamed(clipName);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000027A0 File Offset: 0x000009A0
		[NativeName("RemoveClip")]
		private unsafe void RemoveClipNamed(string clipName)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animation>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(clipName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = clipName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				Animation.RemoveClipNamed_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002804 File Offset: 0x00000A04
		public int GetClipCount()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animation>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Animation.GetClipCount_Injected(intPtr);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002828 File Offset: 0x00000A28
		[Obsolete("use PlayMode instead of AnimationPlayMode.")]
		public bool Play(AnimationPlayMode mode)
		{
			return this.PlayDefaultAnimation((PlayMode)mode);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002844 File Offset: 0x00000A44
		[Obsolete("use PlayMode instead of AnimationPlayMode.")]
		public bool Play(string animation, AnimationPlayMode mode)
		{
			return this.Play(animation, (PlayMode)mode);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002860 File Offset: 0x00000A60
		public void SyncLayer(int layer)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animation>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animation.SyncLayer_Injected(intPtr, layer);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002884 File Offset: 0x00000A84
		public IEnumerator GetEnumerator()
		{
			return new Animation.Enumerator(this);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x0000289C File Offset: 0x00000A9C
		[FreeFunction("AnimationBindings::GetState", HasExplicitThis = true)]
		[return: Unmarshalled]
		internal unsafe AnimationState GetState(string name)
		{
			AnimationState state_Injected;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animation>(this);
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
				state_Injected = Animation.GetState_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return state_Injected;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002904 File Offset: 0x00000B04
		[FreeFunction("AnimationBindings::GetStateAtIndex", HasExplicitThis = true, ThrowsException = true)]
		[return: Unmarshalled]
		internal AnimationState GetStateAtIndex(int index)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animation>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Animation.GetStateAtIndex_Injected(intPtr, index);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002928 File Offset: 0x00000B28
		[NativeName("GetAnimationStateCount")]
		internal int GetStateCount()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animation>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Animation.GetStateCount_Injected(intPtr);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x0000294C File Offset: 0x00000B4C
		public AnimationClip GetClip(string name)
		{
			AnimationState state = this.GetState(name);
			bool flag = state;
			AnimationClip animationClip;
			if (flag)
			{
				animationClip = state.clip;
			}
			else
			{
				animationClip = null;
			}
			return animationClip;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000045 RID: 69 RVA: 0x0000297C File Offset: 0x00000B7C
		// (set) Token: 0x06000046 RID: 70 RVA: 0x000029A0 File Offset: 0x00000BA0
		public bool animatePhysics
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animation>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animation.get_animatePhysics_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animation>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Animation.set_animatePhysics_Injected(intPtr, value);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000047 RID: 71 RVA: 0x000029C4 File Offset: 0x00000BC4
		// (set) Token: 0x06000048 RID: 72 RVA: 0x000029E8 File Offset: 0x00000BE8
		public AnimationUpdateMode updateMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animation>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animation.get_updateMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animation>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Animation.set_updateMode_Injected(intPtr, value);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002A0C File Offset: 0x00000C0C
		// (set) Token: 0x0600004A RID: 74 RVA: 0x00002A30 File Offset: 0x00000C30
		[Obsolete("Use cullingType instead")]
		public bool animateOnlyIfVisible
		{
			[FreeFunction("AnimationBindings::GetAnimateOnlyIfVisible", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animation>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animation.get_animateOnlyIfVisible_Injected(intPtr);
			}
			[FreeFunction("AnimationBindings::SetAnimateOnlyIfVisible", HasExplicitThis = true)]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animation>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Animation.set_animateOnlyIfVisible_Injected(intPtr, value);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002A54 File Offset: 0x00000C54
		// (set) Token: 0x0600004C RID: 76 RVA: 0x00002A78 File Offset: 0x00000C78
		public AnimationCullingType cullingType
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animation>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animation.get_cullingType_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animation>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Animation.set_cullingType_Injected(intPtr, value);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002A9C File Offset: 0x00000C9C
		// (set) Token: 0x0600004E RID: 78 RVA: 0x00002AC4 File Offset: 0x00000CC4
		public Bounds localBounds
		{
			[NativeName("GetLocalAABB")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animation>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Bounds bounds;
				Animation.get_localBounds_Injected(intPtr, out bounds);
				return bounds;
			}
			[NativeName("SetLocalAABB")]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animation>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Animation.set_localBounds_Injected(intPtr, ref value);
			}
		}

		// Token: 0x06000050 RID: 80
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_clip_Injected(IntPtr _unity_self);

		// Token: 0x06000051 RID: 81
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_clip_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x06000052 RID: 82
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_playAutomatically_Injected(IntPtr _unity_self);

		// Token: 0x06000053 RID: 83
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_playAutomatically_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000054 RID: 84
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern WrapMode get_wrapMode_Injected(IntPtr _unity_self);

		// Token: 0x06000055 RID: 85
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_wrapMode_Injected(IntPtr _unity_self, WrapMode value);

		// Token: 0x06000056 RID: 86
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Stop_Injected(IntPtr _unity_self);

		// Token: 0x06000057 RID: 87
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void StopNamed_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name);

		// Token: 0x06000058 RID: 88
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Rewind_Injected(IntPtr _unity_self);

		// Token: 0x06000059 RID: 89
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RewindNamed_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name);

		// Token: 0x0600005A RID: 90
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Sample_Injected(IntPtr _unity_self);

		// Token: 0x0600005B RID: 91
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isPlaying_Injected(IntPtr _unity_self);

		// Token: 0x0600005C RID: 92
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsPlaying_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name);

		// Token: 0x0600005D RID: 93
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool PlayDefaultAnimation_Injected(IntPtr _unity_self, PlayMode mode);

		// Token: 0x0600005E RID: 94
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Play_Injected(IntPtr _unity_self, ref ManagedSpanWrapper animation, [DefaultValue("PlayMode.StopSameLayer")] PlayMode mode);

		// Token: 0x0600005F RID: 95
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CrossFade_Injected(IntPtr _unity_self, ref ManagedSpanWrapper animation, [DefaultValue("0.3F")] float fadeLength, [DefaultValue("PlayMode.StopSameLayer")] PlayMode mode);

		// Token: 0x06000060 RID: 96
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Blend_Injected(IntPtr _unity_self, ref ManagedSpanWrapper animation, [DefaultValue("1.0F")] float targetWeight, [DefaultValue("0.3F")] float fadeLength);

		// Token: 0x06000061 RID: 97
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnimationState CrossFadeQueued_Injected(IntPtr _unity_self, ref ManagedSpanWrapper animation, [DefaultValue("0.3F")] float fadeLength, [DefaultValue("QueueMode.CompleteOthers")] QueueMode queue, [DefaultValue("PlayMode.StopSameLayer")] PlayMode mode);

		// Token: 0x06000062 RID: 98
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnimationState PlayQueued_Injected(IntPtr _unity_self, ref ManagedSpanWrapper animation, [DefaultValue("QueueMode.CompleteOthers")] QueueMode queue, [DefaultValue("PlayMode.StopSameLayer")] PlayMode mode);

		// Token: 0x06000063 RID: 99
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddClip_Injected(IntPtr _unity_self, IntPtr clip, ref ManagedSpanWrapper newName, int firstFrame, int lastFrame, [DefaultValue("false")] bool addLoopFrame);

		// Token: 0x06000064 RID: 100
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RemoveClip_Injected(IntPtr _unity_self, IntPtr clip);

		// Token: 0x06000065 RID: 101
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RemoveClipNamed_Injected(IntPtr _unity_self, ref ManagedSpanWrapper clipName);

		// Token: 0x06000066 RID: 102
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetClipCount_Injected(IntPtr _unity_self);

		// Token: 0x06000067 RID: 103
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SyncLayer_Injected(IntPtr _unity_self, int layer);

		// Token: 0x06000068 RID: 104
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnimationState GetState_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name);

		// Token: 0x06000069 RID: 105
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnimationState GetStateAtIndex_Injected(IntPtr _unity_self, int index);

		// Token: 0x0600006A RID: 106
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetStateCount_Injected(IntPtr _unity_self);

		// Token: 0x0600006B RID: 107
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_animatePhysics_Injected(IntPtr _unity_self);

		// Token: 0x0600006C RID: 108
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_animatePhysics_Injected(IntPtr _unity_self, bool value);

		// Token: 0x0600006D RID: 109
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnimationUpdateMode get_updateMode_Injected(IntPtr _unity_self);

		// Token: 0x0600006E RID: 110
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_updateMode_Injected(IntPtr _unity_self, AnimationUpdateMode value);

		// Token: 0x0600006F RID: 111
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_animateOnlyIfVisible_Injected(IntPtr _unity_self);

		// Token: 0x06000070 RID: 112
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_animateOnlyIfVisible_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000071 RID: 113
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnimationCullingType get_cullingType_Injected(IntPtr _unity_self);

		// Token: 0x06000072 RID: 114
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_cullingType_Injected(IntPtr _unity_self, AnimationCullingType value);

		// Token: 0x06000073 RID: 115
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_localBounds_Injected(IntPtr _unity_self, out Bounds ret);

		// Token: 0x06000074 RID: 116
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_localBounds_Injected(IntPtr _unity_self, [In] ref Bounds value);

		// Token: 0x0200000F RID: 15
		private sealed class Enumerator : IEnumerator
		{
			// Token: 0x06000075 RID: 117 RVA: 0x00002AF1 File Offset: 0x00000CF1
			internal Enumerator(Animation outer)
			{
				this.m_Outer = outer;
			}

			// Token: 0x1700000B RID: 11
			// (get) Token: 0x06000076 RID: 118 RVA: 0x00002B0C File Offset: 0x00000D0C
			public object Current
			{
				get
				{
					return this.m_Outer.GetStateAtIndex(this.m_CurrentIndex);
				}
			}

			// Token: 0x06000077 RID: 119 RVA: 0x00002B30 File Offset: 0x00000D30
			public bool MoveNext()
			{
				int stateCount = this.m_Outer.GetStateCount();
				this.m_CurrentIndex++;
				return this.m_CurrentIndex < stateCount;
			}

			// Token: 0x06000078 RID: 120 RVA: 0x00002B65 File Offset: 0x00000D65
			public void Reset()
			{
				this.m_CurrentIndex = -1;
			}

			// Token: 0x0400001A RID: 26
			private Animation m_Outer;

			// Token: 0x0400001B RID: 27
			private int m_CurrentIndex = -1;
		}
	}
}
