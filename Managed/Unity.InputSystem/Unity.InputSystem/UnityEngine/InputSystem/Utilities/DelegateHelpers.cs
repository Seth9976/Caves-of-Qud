using System;
using Unity.Profiling;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000131 RID: 305
	internal static class DelegateHelpers
	{
		// Token: 0x0600111B RID: 4379 RVA: 0x00051EC0 File Offset: 0x000500C0
		public static void InvokeCallbacksSafe(ref CallbackArray<Action> callbacks, ProfilerMarker marker, string callbackName, object context = null)
		{
			if (callbacks.length == 0)
			{
				return;
			}
			callbacks.LockForChanges();
			for (int i = 0; i < callbacks.length; i++)
			{
				try
				{
					callbacks[i]();
				}
				catch (Exception ex)
				{
					Debug.LogException(ex);
					if (context != null)
					{
						Debug.LogError(string.Format("{0} while executing '{1}' callbacks of '{2}'", ex.GetType().Name, callbackName, context));
					}
					else
					{
						Debug.LogError(ex.GetType().Name + " while executing '" + callbackName + "' callbacks");
					}
				}
			}
			callbacks.UnlockForChanges();
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x00051F5C File Offset: 0x0005015C
		public static void InvokeCallbacksSafe<TValue>(ref CallbackArray<Action<TValue>> callbacks, TValue argument, string callbackName, object context = null)
		{
			if (callbacks.length == 0)
			{
				return;
			}
			callbacks.LockForChanges();
			for (int i = 0; i < callbacks.length; i++)
			{
				try
				{
					callbacks[i](argument);
				}
				catch (Exception ex)
				{
					Debug.LogException(ex);
					if (context != null)
					{
						Debug.LogError(string.Format("{0} while executing '{1}' callbacks of '{2}'", ex.GetType().Name, callbackName, context));
					}
					else
					{
						Debug.LogError(ex.GetType().Name + " while executing '" + callbackName + "' callbacks");
					}
				}
			}
			callbacks.UnlockForChanges();
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x00051FFC File Offset: 0x000501FC
		public static void InvokeCallbacksSafe<TValue1, TValue2>(ref CallbackArray<Action<TValue1, TValue2>> callbacks, TValue1 argument1, TValue2 argument2, ProfilerMarker marker, string callbackName, object context = null)
		{
			if (callbacks.length == 0)
			{
				return;
			}
			callbacks.LockForChanges();
			for (int i = 0; i < callbacks.length; i++)
			{
				try
				{
					callbacks[i](argument1, argument2);
				}
				catch (Exception ex)
				{
					Debug.LogException(ex);
					if (context != null)
					{
						Debug.LogError(string.Format("{0} while executing '{1}' callbacks of '{2}'", ex.GetType().Name, callbackName, context));
					}
					else
					{
						Debug.LogError(ex.GetType().Name + " while executing '" + callbackName + "' callbacks");
					}
				}
			}
			callbacks.UnlockForChanges();
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x000520A0 File Offset: 0x000502A0
		public static bool InvokeCallbacksSafe_AnyCallbackReturnsTrue<TValue1, TValue2>(ref CallbackArray<Func<TValue1, TValue2, bool>> callbacks, TValue1 argument1, TValue2 argument2, string callbackName, object context = null)
		{
			if (callbacks.length == 0)
			{
				return true;
			}
			callbacks.LockForChanges();
			for (int i = 0; i < callbacks.length; i++)
			{
				try
				{
					if (callbacks[i](argument1, argument2))
					{
						callbacks.UnlockForChanges();
						return true;
					}
				}
				catch (Exception ex)
				{
					Debug.LogException(ex);
					if (context != null)
					{
						Debug.LogError(string.Format("{0} while executing '{1}' callbacks of '{2}'", ex.GetType().Name, callbackName, context));
					}
					else
					{
						Debug.LogError(ex.GetType().Name + " while executing '" + callbackName + "' callbacks");
					}
				}
			}
			callbacks.UnlockForChanges();
			return false;
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x00052150 File Offset: 0x00050350
		public static void InvokeCallbacksSafe_AndInvokeReturnedActions<TValue>(ref CallbackArray<Func<TValue, Action>> callbacks, TValue argument, string callbackName, object context = null)
		{
			if (callbacks.length == 0)
			{
				return;
			}
			callbacks.LockForChanges();
			for (int i = 0; i < callbacks.length; i++)
			{
				try
				{
					Action action = callbacks[i](argument);
					if (action != null)
					{
						action();
					}
				}
				catch (Exception ex)
				{
					Debug.LogException(ex);
					if (context != null)
					{
						Debug.LogError(string.Format("{0} while executing '{1}' callbacks of '{2}'", ex.GetType().Name, callbackName, context));
					}
					else
					{
						Debug.LogError(ex.GetType().Name + " while executing '" + callbackName + "' callbacks");
					}
				}
			}
			callbacks.UnlockForChanges();
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x000521F8 File Offset: 0x000503F8
		public static bool InvokeCallbacksSafe_AnyCallbackReturnsObject<TValue, TReturn>(ref CallbackArray<Func<TValue, TReturn>> callbacks, TValue argument, string callbackName, object context = null)
		{
			if (callbacks.length == 0)
			{
				return false;
			}
			callbacks.LockForChanges();
			for (int i = 0; i < callbacks.length; i++)
			{
				try
				{
					if (callbacks[i](argument) != null)
					{
						callbacks.UnlockForChanges();
						return true;
					}
				}
				catch (Exception ex)
				{
					Debug.LogException(ex);
					if (context != null)
					{
						Debug.LogError(string.Format("{0} while executing '{1}' callbacks of '{2}'", ex.GetType().Name, callbackName, context));
					}
					else
					{
						Debug.LogError(ex.GetType().Name + " while executing '" + callbackName + "' callbacks");
					}
				}
			}
			callbacks.UnlockForChanges();
			return false;
		}
	}
}
