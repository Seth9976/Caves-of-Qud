using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks.Sources;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.ParticleSystemJobs;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x02000072 RID: 114
	public sealed class AsyncTriggerHandler<T> : IAsyncOneShotTrigger, IUniTaskSource<T>, IUniTaskSource, IValueTaskSource, IValueTaskSource<T>, ITriggerHandler<T>, IDisposable, IAsyncFixedUpdateHandler, IAsyncLateUpdateHandler, IAsyncOnAnimatorIKHandler, IAsyncOnAnimatorMoveHandler, IAsyncOnApplicationFocusHandler, IAsyncOnApplicationPauseHandler, IAsyncOnApplicationQuitHandler, IAsyncOnAudioFilterReadHandler, IAsyncOnBecameInvisibleHandler, IAsyncOnBecameVisibleHandler, IAsyncOnBeforeTransformParentChangedHandler, IAsyncOnCanvasGroupChangedHandler, IAsyncOnCollisionEnterHandler, IAsyncOnCollisionEnter2DHandler, IAsyncOnCollisionExitHandler, IAsyncOnCollisionExit2DHandler, IAsyncOnCollisionStayHandler, IAsyncOnCollisionStay2DHandler, IAsyncOnControllerColliderHitHandler, IAsyncOnDisableHandler, IAsyncOnDrawGizmosHandler, IAsyncOnDrawGizmosSelectedHandler, IAsyncOnEnableHandler, IAsyncOnGUIHandler, IAsyncOnJointBreakHandler, IAsyncOnJointBreak2DHandler, IAsyncOnMouseDownHandler, IAsyncOnMouseDragHandler, IAsyncOnMouseEnterHandler, IAsyncOnMouseExitHandler, IAsyncOnMouseOverHandler, IAsyncOnMouseUpHandler, IAsyncOnMouseUpAsButtonHandler, IAsyncOnParticleCollisionHandler, IAsyncOnParticleSystemStoppedHandler, IAsyncOnParticleTriggerHandler, IAsyncOnParticleUpdateJobScheduledHandler, IAsyncOnPostRenderHandler, IAsyncOnPreCullHandler, IAsyncOnPreRenderHandler, IAsyncOnRectTransformDimensionsChangeHandler, IAsyncOnRectTransformRemovedHandler, IAsyncOnRenderImageHandler, IAsyncOnRenderObjectHandler, IAsyncOnServerInitializedHandler, IAsyncOnTransformChildrenChangedHandler, IAsyncOnTransformParentChangedHandler, IAsyncOnTriggerEnterHandler, IAsyncOnTriggerEnter2DHandler, IAsyncOnTriggerExitHandler, IAsyncOnTriggerExit2DHandler, IAsyncOnTriggerStayHandler, IAsyncOnTriggerStay2DHandler, IAsyncOnValidateHandler, IAsyncOnWillRenderObjectHandler, IAsyncResetHandler, IAsyncUpdateHandler, IAsyncOnBeginDragHandler, IAsyncOnCancelHandler, IAsyncOnDeselectHandler, IAsyncOnDragHandler, IAsyncOnDropHandler, IAsyncOnEndDragHandler, IAsyncOnInitializePotentialDragHandler, IAsyncOnMoveHandler, IAsyncOnPointerClickHandler, IAsyncOnPointerDownHandler, IAsyncOnPointerEnterHandler, IAsyncOnPointerExitHandler, IAsyncOnPointerUpHandler, IAsyncOnScrollHandler, IAsyncOnSelectHandler, IAsyncOnSubmitHandler, IAsyncOnUpdateSelectedHandler
	{
		// Token: 0x06000390 RID: 912 RVA: 0x0000B0D4 File Offset: 0x000092D4
		UniTask IAsyncOneShotTrigger.OneShotAsync()
		{
			this.core.Reset();
			return new UniTask(this, this.core.Version);
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000391 RID: 913 RVA: 0x0000B0F2 File Offset: 0x000092F2
		internal CancellationToken CancellationToken
		{
			get
			{
				return this.cancellationToken;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000392 RID: 914 RVA: 0x0000B0FA File Offset: 0x000092FA
		// (set) Token: 0x06000393 RID: 915 RVA: 0x0000B102 File Offset: 0x00009302
		ITriggerHandler<T> ITriggerHandler<T>.Prev { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000394 RID: 916 RVA: 0x0000B10B File Offset: 0x0000930B
		// (set) Token: 0x06000395 RID: 917 RVA: 0x0000B113 File Offset: 0x00009313
		ITriggerHandler<T> ITriggerHandler<T>.Next { get; set; }

		// Token: 0x06000396 RID: 918 RVA: 0x0000B11C File Offset: 0x0000931C
		internal AsyncTriggerHandler(AsyncTriggerBase<T> trigger, bool callOnce)
		{
			if (this.cancellationToken.IsCancellationRequested)
			{
				this.isDisposed = true;
				return;
			}
			this.trigger = trigger;
			this.cancellationToken = default(CancellationToken);
			this.registration = default(CancellationTokenRegistration);
			this.callOnce = callOnce;
			trigger.AddHandler(this);
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0000B174 File Offset: 0x00009374
		internal AsyncTriggerHandler(AsyncTriggerBase<T> trigger, CancellationToken cancellationToken, bool callOnce)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				this.isDisposed = true;
				return;
			}
			this.trigger = trigger;
			this.cancellationToken = cancellationToken;
			this.callOnce = callOnce;
			trigger.AddHandler(this);
			if (cancellationToken.CanBeCanceled)
			{
				this.registration = cancellationToken.RegisterWithoutCaptureExecutionContext(AsyncTriggerHandler<T>.cancellationCallback, this);
			}
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000B1D0 File Offset: 0x000093D0
		private static void CancellationCallback(object state)
		{
			AsyncTriggerHandler<T> asyncTriggerHandler = (AsyncTriggerHandler<T>)state;
			asyncTriggerHandler.Dispose();
			asyncTriggerHandler.core.TrySetCanceled(asyncTriggerHandler.cancellationToken);
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000B1FC File Offset: 0x000093FC
		public void Dispose()
		{
			if (!this.isDisposed)
			{
				this.isDisposed = true;
				this.registration.Dispose();
				this.trigger.RemoveHandler(this);
			}
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000B224 File Offset: 0x00009424
		T IUniTaskSource<T>.GetResult(short token)
		{
			T result;
			try
			{
				result = this.core.GetResult(token);
			}
			finally
			{
				if (this.callOnce)
				{
					this.Dispose();
				}
			}
			return result;
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0000B260 File Offset: 0x00009460
		void ITriggerHandler<T>.OnNext(T value)
		{
			this.core.TrySetResult(value);
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000B26F File Offset: 0x0000946F
		void ITriggerHandler<T>.OnCanceled(CancellationToken cancellationToken)
		{
			this.core.TrySetCanceled(cancellationToken);
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000B27E File Offset: 0x0000947E
		void ITriggerHandler<T>.OnCompleted()
		{
			this.core.TrySetCanceled(CancellationToken.None);
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0000B291 File Offset: 0x00009491
		void ITriggerHandler<T>.OnError(Exception ex)
		{
			this.core.TrySetException(ex);
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000B2A0 File Offset: 0x000094A0
		void IUniTaskSource.GetResult(short token)
		{
			((IUniTaskSource<T>)this).GetResult(token);
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000B2AA File Offset: 0x000094AA
		UniTaskStatus IUniTaskSource.GetStatus(short token)
		{
			return this.core.GetStatus(token);
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000B2B8 File Offset: 0x000094B8
		UniTaskStatus IUniTaskSource.UnsafeGetStatus()
		{
			return this.core.UnsafeGetStatus();
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000B2C5 File Offset: 0x000094C5
		void IUniTaskSource.OnCompleted(Action<object> continuation, object state, short token)
		{
			this.core.OnCompleted(continuation, state, token);
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0000B2D5 File Offset: 0x000094D5
		UniTask IAsyncFixedUpdateHandler.FixedUpdateAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0000B2F8 File Offset: 0x000094F8
		UniTask IAsyncLateUpdateHandler.LateUpdateAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000B31B File Offset: 0x0000951B
		UniTask<int> IAsyncOnAnimatorIKHandler.OnAnimatorIKAsync()
		{
			this.core.Reset();
			return new UniTask<int>((IUniTaskSource<int>)this, this.core.Version);
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0000B33E File Offset: 0x0000953E
		UniTask IAsyncOnAnimatorMoveHandler.OnAnimatorMoveAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0000B361 File Offset: 0x00009561
		UniTask<bool> IAsyncOnApplicationFocusHandler.OnApplicationFocusAsync()
		{
			this.core.Reset();
			return new UniTask<bool>((IUniTaskSource<bool>)this, this.core.Version);
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0000B384 File Offset: 0x00009584
		UniTask<bool> IAsyncOnApplicationPauseHandler.OnApplicationPauseAsync()
		{
			this.core.Reset();
			return new UniTask<bool>((IUniTaskSource<bool>)this, this.core.Version);
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0000B3A7 File Offset: 0x000095A7
		UniTask IAsyncOnApplicationQuitHandler.OnApplicationQuitAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0000B3CA File Offset: 0x000095CA
		[return: TupleElementNames(new string[] { "data", "channels" })]
		UniTask<ValueTuple<float[], int>> IAsyncOnAudioFilterReadHandler.OnAudioFilterReadAsync()
		{
			this.core.Reset();
			return new UniTask<ValueTuple<float[], int>>((IUniTaskSource<ValueTuple<float[], int>>)this, this.core.Version);
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0000B3ED File Offset: 0x000095ED
		UniTask IAsyncOnBecameInvisibleHandler.OnBecameInvisibleAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0000B410 File Offset: 0x00009610
		UniTask IAsyncOnBecameVisibleHandler.OnBecameVisibleAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000B433 File Offset: 0x00009633
		UniTask IAsyncOnBeforeTransformParentChangedHandler.OnBeforeTransformParentChangedAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0000B456 File Offset: 0x00009656
		UniTask IAsyncOnCanvasGroupChangedHandler.OnCanvasGroupChangedAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0000B479 File Offset: 0x00009679
		UniTask<Collision> IAsyncOnCollisionEnterHandler.OnCollisionEnterAsync()
		{
			this.core.Reset();
			return new UniTask<Collision>((IUniTaskSource<Collision>)this, this.core.Version);
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0000B49C File Offset: 0x0000969C
		UniTask<Collision2D> IAsyncOnCollisionEnter2DHandler.OnCollisionEnter2DAsync()
		{
			this.core.Reset();
			return new UniTask<Collision2D>((IUniTaskSource<Collision2D>)this, this.core.Version);
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0000B4BF File Offset: 0x000096BF
		UniTask<Collision> IAsyncOnCollisionExitHandler.OnCollisionExitAsync()
		{
			this.core.Reset();
			return new UniTask<Collision>((IUniTaskSource<Collision>)this, this.core.Version);
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0000B4E2 File Offset: 0x000096E2
		UniTask<Collision2D> IAsyncOnCollisionExit2DHandler.OnCollisionExit2DAsync()
		{
			this.core.Reset();
			return new UniTask<Collision2D>((IUniTaskSource<Collision2D>)this, this.core.Version);
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0000B505 File Offset: 0x00009705
		UniTask<Collision> IAsyncOnCollisionStayHandler.OnCollisionStayAsync()
		{
			this.core.Reset();
			return new UniTask<Collision>((IUniTaskSource<Collision>)this, this.core.Version);
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0000B528 File Offset: 0x00009728
		UniTask<Collision2D> IAsyncOnCollisionStay2DHandler.OnCollisionStay2DAsync()
		{
			this.core.Reset();
			return new UniTask<Collision2D>((IUniTaskSource<Collision2D>)this, this.core.Version);
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0000B54B File Offset: 0x0000974B
		UniTask<ControllerColliderHit> IAsyncOnControllerColliderHitHandler.OnControllerColliderHitAsync()
		{
			this.core.Reset();
			return new UniTask<ControllerColliderHit>((IUniTaskSource<ControllerColliderHit>)this, this.core.Version);
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0000B56E File Offset: 0x0000976E
		UniTask IAsyncOnDisableHandler.OnDisableAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0000B591 File Offset: 0x00009791
		UniTask IAsyncOnDrawGizmosHandler.OnDrawGizmosAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0000B5B4 File Offset: 0x000097B4
		UniTask IAsyncOnDrawGizmosSelectedHandler.OnDrawGizmosSelectedAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0000B5D7 File Offset: 0x000097D7
		UniTask IAsyncOnEnableHandler.OnEnableAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0000B5FA File Offset: 0x000097FA
		UniTask IAsyncOnGUIHandler.OnGUIAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0000B61D File Offset: 0x0000981D
		UniTask<float> IAsyncOnJointBreakHandler.OnJointBreakAsync()
		{
			this.core.Reset();
			return new UniTask<float>((IUniTaskSource<float>)this, this.core.Version);
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0000B640 File Offset: 0x00009840
		UniTask<Joint2D> IAsyncOnJointBreak2DHandler.OnJointBreak2DAsync()
		{
			this.core.Reset();
			return new UniTask<Joint2D>((IUniTaskSource<Joint2D>)this, this.core.Version);
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0000B663 File Offset: 0x00009863
		UniTask IAsyncOnMouseDownHandler.OnMouseDownAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0000B686 File Offset: 0x00009886
		UniTask IAsyncOnMouseDragHandler.OnMouseDragAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0000B6A9 File Offset: 0x000098A9
		UniTask IAsyncOnMouseEnterHandler.OnMouseEnterAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0000B6CC File Offset: 0x000098CC
		UniTask IAsyncOnMouseExitHandler.OnMouseExitAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0000B6EF File Offset: 0x000098EF
		UniTask IAsyncOnMouseOverHandler.OnMouseOverAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0000B712 File Offset: 0x00009912
		UniTask IAsyncOnMouseUpHandler.OnMouseUpAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0000B735 File Offset: 0x00009935
		UniTask IAsyncOnMouseUpAsButtonHandler.OnMouseUpAsButtonAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0000B758 File Offset: 0x00009958
		UniTask<GameObject> IAsyncOnParticleCollisionHandler.OnParticleCollisionAsync()
		{
			this.core.Reset();
			return new UniTask<GameObject>((IUniTaskSource<GameObject>)this, this.core.Version);
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0000B77B File Offset: 0x0000997B
		UniTask IAsyncOnParticleSystemStoppedHandler.OnParticleSystemStoppedAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0000B79E File Offset: 0x0000999E
		UniTask IAsyncOnParticleTriggerHandler.OnParticleTriggerAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0000B7C1 File Offset: 0x000099C1
		UniTask<ParticleSystemJobData> IAsyncOnParticleUpdateJobScheduledHandler.OnParticleUpdateJobScheduledAsync()
		{
			this.core.Reset();
			return new UniTask<ParticleSystemJobData>((IUniTaskSource<ParticleSystemJobData>)this, this.core.Version);
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0000B7E4 File Offset: 0x000099E4
		UniTask IAsyncOnPostRenderHandler.OnPostRenderAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0000B807 File Offset: 0x00009A07
		UniTask IAsyncOnPreCullHandler.OnPreCullAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0000B82A File Offset: 0x00009A2A
		UniTask IAsyncOnPreRenderHandler.OnPreRenderAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0000B84D File Offset: 0x00009A4D
		UniTask IAsyncOnRectTransformDimensionsChangeHandler.OnRectTransformDimensionsChangeAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0000B870 File Offset: 0x00009A70
		UniTask IAsyncOnRectTransformRemovedHandler.OnRectTransformRemovedAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0000B893 File Offset: 0x00009A93
		[return: TupleElementNames(new string[] { "source", "destination" })]
		UniTask<ValueTuple<RenderTexture, RenderTexture>> IAsyncOnRenderImageHandler.OnRenderImageAsync()
		{
			this.core.Reset();
			return new UniTask<ValueTuple<RenderTexture, RenderTexture>>((IUniTaskSource<ValueTuple<RenderTexture, RenderTexture>>)this, this.core.Version);
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0000B8B6 File Offset: 0x00009AB6
		UniTask IAsyncOnRenderObjectHandler.OnRenderObjectAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0000B8D9 File Offset: 0x00009AD9
		UniTask IAsyncOnServerInitializedHandler.OnServerInitializedAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0000B8FC File Offset: 0x00009AFC
		UniTask IAsyncOnTransformChildrenChangedHandler.OnTransformChildrenChangedAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0000B91F File Offset: 0x00009B1F
		UniTask IAsyncOnTransformParentChangedHandler.OnTransformParentChangedAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0000B942 File Offset: 0x00009B42
		UniTask<Collider> IAsyncOnTriggerEnterHandler.OnTriggerEnterAsync()
		{
			this.core.Reset();
			return new UniTask<Collider>((IUniTaskSource<Collider>)this, this.core.Version);
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0000B965 File Offset: 0x00009B65
		UniTask<Collider2D> IAsyncOnTriggerEnter2DHandler.OnTriggerEnter2DAsync()
		{
			this.core.Reset();
			return new UniTask<Collider2D>((IUniTaskSource<Collider2D>)this, this.core.Version);
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0000B988 File Offset: 0x00009B88
		UniTask<Collider> IAsyncOnTriggerExitHandler.OnTriggerExitAsync()
		{
			this.core.Reset();
			return new UniTask<Collider>((IUniTaskSource<Collider>)this, this.core.Version);
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0000B9AB File Offset: 0x00009BAB
		UniTask<Collider2D> IAsyncOnTriggerExit2DHandler.OnTriggerExit2DAsync()
		{
			this.core.Reset();
			return new UniTask<Collider2D>((IUniTaskSource<Collider2D>)this, this.core.Version);
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0000B9CE File Offset: 0x00009BCE
		UniTask<Collider> IAsyncOnTriggerStayHandler.OnTriggerStayAsync()
		{
			this.core.Reset();
			return new UniTask<Collider>((IUniTaskSource<Collider>)this, this.core.Version);
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0000B9F1 File Offset: 0x00009BF1
		UniTask<Collider2D> IAsyncOnTriggerStay2DHandler.OnTriggerStay2DAsync()
		{
			this.core.Reset();
			return new UniTask<Collider2D>((IUniTaskSource<Collider2D>)this, this.core.Version);
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0000BA14 File Offset: 0x00009C14
		UniTask IAsyncOnValidateHandler.OnValidateAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0000BA37 File Offset: 0x00009C37
		UniTask IAsyncOnWillRenderObjectHandler.OnWillRenderObjectAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0000BA5A File Offset: 0x00009C5A
		UniTask IAsyncResetHandler.ResetAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0000BA7D File Offset: 0x00009C7D
		UniTask IAsyncUpdateHandler.UpdateAsync()
		{
			this.core.Reset();
			return new UniTask((IUniTaskSource)this, this.core.Version);
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0000BAA0 File Offset: 0x00009CA0
		UniTask<PointerEventData> IAsyncOnBeginDragHandler.OnBeginDragAsync()
		{
			this.core.Reset();
			return new UniTask<PointerEventData>((IUniTaskSource<PointerEventData>)this, this.core.Version);
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0000BAC3 File Offset: 0x00009CC3
		UniTask<BaseEventData> IAsyncOnCancelHandler.OnCancelAsync()
		{
			this.core.Reset();
			return new UniTask<BaseEventData>((IUniTaskSource<BaseEventData>)this, this.core.Version);
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0000BAE6 File Offset: 0x00009CE6
		UniTask<BaseEventData> IAsyncOnDeselectHandler.OnDeselectAsync()
		{
			this.core.Reset();
			return new UniTask<BaseEventData>((IUniTaskSource<BaseEventData>)this, this.core.Version);
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0000BB09 File Offset: 0x00009D09
		UniTask<PointerEventData> IAsyncOnDragHandler.OnDragAsync()
		{
			this.core.Reset();
			return new UniTask<PointerEventData>((IUniTaskSource<PointerEventData>)this, this.core.Version);
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0000BB2C File Offset: 0x00009D2C
		UniTask<PointerEventData> IAsyncOnDropHandler.OnDropAsync()
		{
			this.core.Reset();
			return new UniTask<PointerEventData>((IUniTaskSource<PointerEventData>)this, this.core.Version);
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0000BB4F File Offset: 0x00009D4F
		UniTask<PointerEventData> IAsyncOnEndDragHandler.OnEndDragAsync()
		{
			this.core.Reset();
			return new UniTask<PointerEventData>((IUniTaskSource<PointerEventData>)this, this.core.Version);
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0000BB72 File Offset: 0x00009D72
		UniTask<PointerEventData> IAsyncOnInitializePotentialDragHandler.OnInitializePotentialDragAsync()
		{
			this.core.Reset();
			return new UniTask<PointerEventData>((IUniTaskSource<PointerEventData>)this, this.core.Version);
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0000BB95 File Offset: 0x00009D95
		UniTask<AxisEventData> IAsyncOnMoveHandler.OnMoveAsync()
		{
			this.core.Reset();
			return new UniTask<AxisEventData>((IUniTaskSource<AxisEventData>)this, this.core.Version);
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0000BBB8 File Offset: 0x00009DB8
		UniTask<PointerEventData> IAsyncOnPointerClickHandler.OnPointerClickAsync()
		{
			this.core.Reset();
			return new UniTask<PointerEventData>((IUniTaskSource<PointerEventData>)this, this.core.Version);
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0000BBDB File Offset: 0x00009DDB
		UniTask<PointerEventData> IAsyncOnPointerDownHandler.OnPointerDownAsync()
		{
			this.core.Reset();
			return new UniTask<PointerEventData>((IUniTaskSource<PointerEventData>)this, this.core.Version);
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0000BBFE File Offset: 0x00009DFE
		UniTask<PointerEventData> IAsyncOnPointerEnterHandler.OnPointerEnterAsync()
		{
			this.core.Reset();
			return new UniTask<PointerEventData>((IUniTaskSource<PointerEventData>)this, this.core.Version);
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0000BC21 File Offset: 0x00009E21
		UniTask<PointerEventData> IAsyncOnPointerExitHandler.OnPointerExitAsync()
		{
			this.core.Reset();
			return new UniTask<PointerEventData>((IUniTaskSource<PointerEventData>)this, this.core.Version);
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0000BC44 File Offset: 0x00009E44
		UniTask<PointerEventData> IAsyncOnPointerUpHandler.OnPointerUpAsync()
		{
			this.core.Reset();
			return new UniTask<PointerEventData>((IUniTaskSource<PointerEventData>)this, this.core.Version);
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0000BC67 File Offset: 0x00009E67
		UniTask<PointerEventData> IAsyncOnScrollHandler.OnScrollAsync()
		{
			this.core.Reset();
			return new UniTask<PointerEventData>((IUniTaskSource<PointerEventData>)this, this.core.Version);
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0000BC8A File Offset: 0x00009E8A
		UniTask<BaseEventData> IAsyncOnSelectHandler.OnSelectAsync()
		{
			this.core.Reset();
			return new UniTask<BaseEventData>((IUniTaskSource<BaseEventData>)this, this.core.Version);
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0000BCAD File Offset: 0x00009EAD
		UniTask<BaseEventData> IAsyncOnSubmitHandler.OnSubmitAsync()
		{
			this.core.Reset();
			return new UniTask<BaseEventData>((IUniTaskSource<BaseEventData>)this, this.core.Version);
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0000BCD0 File Offset: 0x00009ED0
		UniTask<BaseEventData> IAsyncOnUpdateSelectedHandler.OnUpdateSelectedAsync()
		{
			this.core.Reset();
			return new UniTask<BaseEventData>((IUniTaskSource<BaseEventData>)this, this.core.Version);
		}

		// Token: 0x040000F3 RID: 243
		private static Action<object> cancellationCallback = new Action<object>(AsyncTriggerHandler<T>.CancellationCallback);

		// Token: 0x040000F4 RID: 244
		private readonly AsyncTriggerBase<T> trigger;

		// Token: 0x040000F5 RID: 245
		private CancellationToken cancellationToken;

		// Token: 0x040000F6 RID: 246
		private CancellationTokenRegistration registration;

		// Token: 0x040000F7 RID: 247
		private bool isDisposed;

		// Token: 0x040000F8 RID: 248
		private bool callOnce;

		// Token: 0x040000F9 RID: 249
		private UniTaskCompletionSourceCore<T> core;
	}
}
