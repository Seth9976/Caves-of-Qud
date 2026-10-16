using System;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000E9 RID: 233
	public struct InputEventListener : IObservable<InputEventPtr>
	{
		// Token: 0x06000DE7 RID: 3559 RVA: 0x000461D4 File Offset: 0x000443D4
		public static InputEventListener operator +(InputEventListener _, Action<InputEventPtr, InputDevice> callback)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			InputManager s_Manager = InputSystem.s_Manager;
			lock (s_Manager)
			{
				InputSystem.s_Manager.onEvent += callback;
			}
			return default(InputEventListener);
		}

		// Token: 0x06000DE8 RID: 3560 RVA: 0x00046230 File Offset: 0x00044430
		public static InputEventListener operator -(InputEventListener _, Action<InputEventPtr, InputDevice> callback)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			InputManager s_Manager = InputSystem.s_Manager;
			lock (s_Manager)
			{
				InputSystem.s_Manager.onEvent -= callback;
			}
			return default(InputEventListener);
		}

		// Token: 0x06000DE9 RID: 3561 RVA: 0x0004628C File Offset: 0x0004448C
		public IDisposable Subscribe(IObserver<InputEventPtr> observer)
		{
			if (InputEventListener.s_ObserverState == null)
			{
				InputEventListener.s_ObserverState = new InputEventListener.ObserverState();
			}
			if (InputEventListener.s_ObserverState.observers.length == 0)
			{
				InputSystem.s_Manager.onEvent += InputEventListener.s_ObserverState.onEventDelegate;
			}
			InputEventListener.s_ObserverState.observers.AppendWithCapacity(observer, 10);
			return new InputEventListener.DisposableObserver
			{
				observer = observer
			};
		}

		// Token: 0x040005C4 RID: 1476
		internal static InputEventListener.ObserverState s_ObserverState;

		// Token: 0x02000217 RID: 535
		internal class ObserverState
		{
			// Token: 0x0600151B RID: 5403 RVA: 0x00062B8A File Offset: 0x00060D8A
			public ObserverState()
			{
				this.onEventDelegate = delegate(InputEventPtr eventPtr, InputDevice device)
				{
					for (int i = this.observers.length - 1; i >= 0; i--)
					{
						this.observers[i].OnNext(eventPtr);
					}
				};
			}

			// Token: 0x04000BA9 RID: 2985
			public InlinedArray<IObserver<InputEventPtr>> observers;

			// Token: 0x04000BAA RID: 2986
			public Action<InputEventPtr, InputDevice> onEventDelegate;
		}

		// Token: 0x02000218 RID: 536
		private class DisposableObserver : IDisposable
		{
			// Token: 0x0600151D RID: 5405 RVA: 0x00062BDC File Offset: 0x00060DDC
			public void Dispose()
			{
				int num = InputEventListener.s_ObserverState.observers.IndexOfReference(this.observer);
				if (num >= 0)
				{
					InputEventListener.s_ObserverState.observers.RemoveAtWithCapacity(num);
				}
				if (InputEventListener.s_ObserverState.observers.length == 0)
				{
					InputSystem.s_Manager.onEvent -= InputEventListener.s_ObserverState.onEventDelegate;
				}
			}

			// Token: 0x04000BAB RID: 2987
			public IObserver<InputEventPtr> observer;
		}
	}
}
