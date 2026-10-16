using System;
using System.Runtime.ExceptionServices;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x0200006A RID: 106
	public static class UnityBindingExtensions
	{
		// Token: 0x060002C8 RID: 712 RVA: 0x0000A4C0 File Offset: 0x000086C0
		public static void BindTo(this IUniTaskAsyncEnumerable<string> source, Text text, bool rebindOnError = true)
		{
			UnityBindingExtensions.BindToCore(source, text, text.GetCancellationTokenOnDestroy(), rebindOnError).Forget();
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000A4E4 File Offset: 0x000086E4
		public static void BindTo(this IUniTaskAsyncEnumerable<string> source, Text text, CancellationToken cancellationToken, bool rebindOnError = true)
		{
			UnityBindingExtensions.BindToCore(source, text, cancellationToken, rebindOnError).Forget();
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000A504 File Offset: 0x00008704
		private static async UniTaskVoid BindToCore(IUniTaskAsyncEnumerable<string> source, Text text, CancellationToken cancellationToken, bool rebindOnError)
		{
			bool repeat = false;
			object obj2;
			int num2;
			for (;;)
			{
				IUniTaskAsyncEnumerator<string> e = source.GetAsyncEnumerator(cancellationToken);
				object obj = null;
				int num = 0;
				try
				{
					for (;;)
					{
						bool flag;
						try
						{
							flag = await e.MoveNextAsync();
							repeat = false;
						}
						catch (Exception ex)
						{
							if (ex is OperationCanceledException)
							{
								goto IL_00FC;
							}
							if (rebindOnError && !repeat)
							{
								repeat = true;
								break;
							}
							throw;
						}
						if (!flag)
						{
							goto IL_00FC;
						}
						text.text = e.Current;
					}
					num = 1;
					goto IL_0111;
					IL_00FC:
					num = 2;
				}
				catch (object obj)
				{
				}
				IL_0111:
				if (e != null)
				{
					await e.DisposeAsync();
				}
				obj2 = obj;
				if (obj2 != null)
				{
					Exception ex2 = obj2 as Exception;
					if (ex2 == null)
					{
						break;
					}
					ExceptionDispatchInfo.Capture(ex2).Throw();
				}
				num2 = num;
				if (num2 != 1)
				{
					goto Block_5;
				}
			}
			throw obj2;
			Block_5:
			if (num2 != 2)
			{
				object obj = null;
				IUniTaskAsyncEnumerator<string> e = null;
			}
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000A560 File Offset: 0x00008760
		public static void BindTo<T>(this IUniTaskAsyncEnumerable<T> source, Text text, bool rebindOnError = true)
		{
			UnityBindingExtensions.BindToCore<T>(source, text, text.GetCancellationTokenOnDestroy(), rebindOnError).Forget();
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000A584 File Offset: 0x00008784
		public static void BindTo<T>(this IUniTaskAsyncEnumerable<T> source, Text text, CancellationToken cancellationToken, bool rebindOnError = true)
		{
			UnityBindingExtensions.BindToCore<T>(source, text, cancellationToken, rebindOnError).Forget();
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000A5A4 File Offset: 0x000087A4
		public static void BindTo<T>(this AsyncReactiveProperty<T> source, Text text, bool rebindOnError = true)
		{
			UnityBindingExtensions.BindToCore<T>(source, text, text.GetCancellationTokenOnDestroy(), rebindOnError).Forget();
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000A5C8 File Offset: 0x000087C8
		private static async UniTaskVoid BindToCore<T>(IUniTaskAsyncEnumerable<T> source, Text text, CancellationToken cancellationToken, bool rebindOnError)
		{
			bool repeat = false;
			object obj2;
			int num2;
			for (;;)
			{
				IUniTaskAsyncEnumerator<T> e = source.GetAsyncEnumerator(cancellationToken);
				object obj = null;
				int num = 0;
				try
				{
					for (;;)
					{
						bool flag;
						try
						{
							flag = await e.MoveNextAsync();
							repeat = false;
						}
						catch (Exception ex)
						{
							if (ex is OperationCanceledException)
							{
								goto IL_010B;
							}
							if (rebindOnError && !repeat)
							{
								repeat = true;
								break;
							}
							throw;
						}
						if (!flag)
						{
							goto IL_010B;
						}
						T t = e.Current;
						text.text = t.ToString();
					}
					num = 1;
					goto IL_0120;
					IL_010B:
					num = 2;
				}
				catch (object obj)
				{
				}
				IL_0120:
				if (e != null)
				{
					await e.DisposeAsync();
				}
				obj2 = obj;
				if (obj2 != null)
				{
					Exception ex2 = obj2 as Exception;
					if (ex2 == null)
					{
						break;
					}
					ExceptionDispatchInfo.Capture(ex2).Throw();
				}
				num2 = num;
				if (num2 != 1)
				{
					goto Block_5;
				}
			}
			throw obj2;
			Block_5:
			if (num2 != 2)
			{
				object obj = null;
				IUniTaskAsyncEnumerator<T> e = null;
			}
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000A624 File Offset: 0x00008824
		public static void BindTo(this IUniTaskAsyncEnumerable<bool> source, Selectable selectable, bool rebindOnError = true)
		{
			UnityBindingExtensions.BindToCore(source, selectable, selectable.GetCancellationTokenOnDestroy(), rebindOnError).Forget();
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000A648 File Offset: 0x00008848
		public static void BindTo(this IUniTaskAsyncEnumerable<bool> source, Selectable selectable, CancellationToken cancellationToken, bool rebindOnError = true)
		{
			UnityBindingExtensions.BindToCore(source, selectable, cancellationToken, rebindOnError).Forget();
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000A668 File Offset: 0x00008868
		private static async UniTaskVoid BindToCore(IUniTaskAsyncEnumerable<bool> source, Selectable selectable, CancellationToken cancellationToken, bool rebindOnError)
		{
			bool repeat = false;
			object obj2;
			int num2;
			for (;;)
			{
				IUniTaskAsyncEnumerator<bool> e = source.GetAsyncEnumerator(cancellationToken);
				object obj = null;
				int num = 0;
				try
				{
					for (;;)
					{
						bool flag;
						try
						{
							flag = await e.MoveNextAsync();
							repeat = false;
						}
						catch (Exception ex)
						{
							if (ex is OperationCanceledException)
							{
								goto IL_00FC;
							}
							if (rebindOnError && !repeat)
							{
								repeat = true;
								break;
							}
							throw;
						}
						if (!flag)
						{
							goto IL_00FC;
						}
						selectable.interactable = e.Current;
					}
					num = 1;
					goto IL_0111;
					IL_00FC:
					num = 2;
				}
				catch (object obj)
				{
				}
				IL_0111:
				if (e != null)
				{
					await e.DisposeAsync();
				}
				obj2 = obj;
				if (obj2 != null)
				{
					Exception ex2 = obj2 as Exception;
					if (ex2 == null)
					{
						break;
					}
					ExceptionDispatchInfo.Capture(ex2).Throw();
				}
				num2 = num;
				if (num2 != 1)
				{
					goto Block_5;
				}
			}
			throw obj2;
			Block_5:
			if (num2 != 2)
			{
				object obj = null;
				IUniTaskAsyncEnumerator<bool> e = null;
			}
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000A6C4 File Offset: 0x000088C4
		public static void BindTo<TSource, TObject>(this IUniTaskAsyncEnumerable<TSource> source, TObject monoBehaviour, Action<TObject, TSource> bindAction, bool rebindOnError = true) where TObject : MonoBehaviour
		{
			UnityBindingExtensions.BindToCore<TSource, TObject>(source, monoBehaviour, bindAction, monoBehaviour.GetCancellationTokenOnDestroy(), rebindOnError).Forget();
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000A6F0 File Offset: 0x000088F0
		public static void BindTo<TSource, TObject>(this IUniTaskAsyncEnumerable<TSource> source, TObject bindTarget, Action<TObject, TSource> bindAction, CancellationToken cancellationToken, bool rebindOnError = true)
		{
			UnityBindingExtensions.BindToCore<TSource, TObject>(source, bindTarget, bindAction, cancellationToken, rebindOnError).Forget();
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000A710 File Offset: 0x00008910
		private static async UniTaskVoid BindToCore<TSource, TObject>(IUniTaskAsyncEnumerable<TSource> source, TObject bindTarget, Action<TObject, TSource> bindAction, CancellationToken cancellationToken, bool rebindOnError)
		{
			bool repeat = false;
			object obj2;
			int num2;
			for (;;)
			{
				IUniTaskAsyncEnumerator<TSource> e = source.GetAsyncEnumerator(cancellationToken);
				object obj = null;
				int num = 0;
				try
				{
					for (;;)
					{
						bool flag;
						try
						{
							flag = await e.MoveNextAsync();
							repeat = false;
						}
						catch (Exception ex)
						{
							if (ex is OperationCanceledException)
							{
								goto IL_0102;
							}
							if (rebindOnError && !repeat)
							{
								repeat = true;
								break;
							}
							throw;
						}
						if (!flag)
						{
							goto IL_0102;
						}
						bindAction(bindTarget, e.Current);
					}
					num = 1;
					goto IL_0117;
					IL_0102:
					num = 2;
				}
				catch (object obj)
				{
				}
				IL_0117:
				if (e != null)
				{
					await e.DisposeAsync();
				}
				obj2 = obj;
				if (obj2 != null)
				{
					Exception ex2 = obj2 as Exception;
					if (ex2 == null)
					{
						break;
					}
					ExceptionDispatchInfo.Capture(ex2).Throw();
				}
				num2 = num;
				if (num2 != 1)
				{
					goto Block_5;
				}
			}
			throw obj2;
			Block_5:
			if (num2 != 2)
			{
				object obj = null;
				IUniTaskAsyncEnumerator<TSource> e = null;
			}
		}
	}
}
