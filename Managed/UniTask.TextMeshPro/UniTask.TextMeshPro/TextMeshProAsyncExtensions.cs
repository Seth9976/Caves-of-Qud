using System;
using System.Runtime.ExceptionServices;
using System.Threading;
using TMPro;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000003 RID: 3
	public static class TextMeshProAsyncExtensions
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BB File Offset: 0x000002BB
		public static IAsyncValueChangedEventHandler<string> GetAsyncValueChangedEventHandler(this TMP_InputField inputField)
		{
			return new AsyncUnityEventHandler<string>(inputField.onValueChanged, inputField.GetCancellationTokenOnDestroy(), false);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020CF File Offset: 0x000002CF
		public static IAsyncValueChangedEventHandler<string> GetAsyncValueChangedEventHandler(this TMP_InputField inputField, CancellationToken cancellationToken)
		{
			return new AsyncUnityEventHandler<string>(inputField.onValueChanged, cancellationToken, false);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020DE File Offset: 0x000002DE
		public static UniTask<string> OnValueChangedAsync(this TMP_InputField inputField)
		{
			return new AsyncUnityEventHandler<string>(inputField.onValueChanged, inputField.GetCancellationTokenOnDestroy(), true).OnInvokeAsync();
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020F7 File Offset: 0x000002F7
		public static UniTask<string> OnValueChangedAsync(this TMP_InputField inputField, CancellationToken cancellationToken)
		{
			return new AsyncUnityEventHandler<string>(inputField.onValueChanged, cancellationToken, true).OnInvokeAsync();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000210B File Offset: 0x0000030B
		public static IUniTaskAsyncEnumerable<string> OnValueChangedAsAsyncEnumerable(this TMP_InputField inputField)
		{
			return new UnityEventHandlerAsyncEnumerable<string>(inputField.onValueChanged, inputField.GetCancellationTokenOnDestroy());
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000211E File Offset: 0x0000031E
		public static IUniTaskAsyncEnumerable<string> OnValueChangedAsAsyncEnumerable(this TMP_InputField inputField, CancellationToken cancellationToken)
		{
			return new UnityEventHandlerAsyncEnumerable<string>(inputField.onValueChanged, cancellationToken);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000212C File Offset: 0x0000032C
		public static IAsyncEndEditEventHandler<string> GetAsyncEndEditEventHandler(this TMP_InputField inputField)
		{
			return new AsyncUnityEventHandler<string>(inputField.onEndEdit, inputField.GetCancellationTokenOnDestroy(), false);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002140 File Offset: 0x00000340
		public static IAsyncEndEditEventHandler<string> GetAsyncEndEditEventHandler(this TMP_InputField inputField, CancellationToken cancellationToken)
		{
			return new AsyncUnityEventHandler<string>(inputField.onEndEdit, cancellationToken, false);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000214F File Offset: 0x0000034F
		public static UniTask<string> OnEndEditAsync(this TMP_InputField inputField)
		{
			return new AsyncUnityEventHandler<string>(inputField.onEndEdit, inputField.GetCancellationTokenOnDestroy(), true).OnInvokeAsync();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002168 File Offset: 0x00000368
		public static UniTask<string> OnEndEditAsync(this TMP_InputField inputField, CancellationToken cancellationToken)
		{
			return new AsyncUnityEventHandler<string>(inputField.onEndEdit, cancellationToken, true).OnInvokeAsync();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000217C File Offset: 0x0000037C
		public static IUniTaskAsyncEnumerable<string> OnEndEditAsAsyncEnumerable(this TMP_InputField inputField)
		{
			return new UnityEventHandlerAsyncEnumerable<string>(inputField.onEndEdit, inputField.GetCancellationTokenOnDestroy());
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000218F File Offset: 0x0000038F
		public static IUniTaskAsyncEnumerable<string> OnEndEditAsAsyncEnumerable(this TMP_InputField inputField, CancellationToken cancellationToken)
		{
			return new UnityEventHandlerAsyncEnumerable<string>(inputField.onEndEdit, cancellationToken);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000219D File Offset: 0x0000039D
		public static IAsyncEndTextSelectionEventHandler<ValueTuple<string, int, int>> GetAsyncEndTextSelectionEventHandler(this TMP_InputField inputField)
		{
			return new AsyncUnityEventHandler<ValueTuple<string, int, int>>(new TextSelectionEventConverter(inputField.onEndTextSelection), inputField.GetCancellationTokenOnDestroy(), false);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021B6 File Offset: 0x000003B6
		public static IAsyncEndTextSelectionEventHandler<ValueTuple<string, int, int>> GetAsyncEndTextSelectionEventHandler(this TMP_InputField inputField, CancellationToken cancellationToken)
		{
			return new AsyncUnityEventHandler<ValueTuple<string, int, int>>(new TextSelectionEventConverter(inputField.onEndTextSelection), cancellationToken, false);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000021CA File Offset: 0x000003CA
		public static UniTask<ValueTuple<string, int, int>> OnEndTextSelectionAsync(this TMP_InputField inputField)
		{
			return new AsyncUnityEventHandler<ValueTuple<string, int, int>>(new TextSelectionEventConverter(inputField.onEndTextSelection), inputField.GetCancellationTokenOnDestroy(), true).OnInvokeAsync();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000021E8 File Offset: 0x000003E8
		public static UniTask<ValueTuple<string, int, int>> OnEndTextSelectionAsync(this TMP_InputField inputField, CancellationToken cancellationToken)
		{
			return new AsyncUnityEventHandler<ValueTuple<string, int, int>>(new TextSelectionEventConverter(inputField.onEndTextSelection), cancellationToken, true).OnInvokeAsync();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002201 File Offset: 0x00000401
		public static IUniTaskAsyncEnumerable<ValueTuple<string, int, int>> OnEndTextSelectionAsAsyncEnumerable(this TMP_InputField inputField)
		{
			return new UnityEventHandlerAsyncEnumerable<ValueTuple<string, int, int>>(new TextSelectionEventConverter(inputField.onEndTextSelection), inputField.GetCancellationTokenOnDestroy());
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002219 File Offset: 0x00000419
		public static IUniTaskAsyncEnumerable<ValueTuple<string, int, int>> OnEndTextSelectionAsAsyncEnumerable(this TMP_InputField inputField, CancellationToken cancellationToken)
		{
			return new UnityEventHandlerAsyncEnumerable<ValueTuple<string, int, int>>(new TextSelectionEventConverter(inputField.onEndTextSelection), cancellationToken);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000222C File Offset: 0x0000042C
		public static IAsyncTextSelectionEventHandler<ValueTuple<string, int, int>> GetAsyncTextSelectionEventHandler(this TMP_InputField inputField)
		{
			return new AsyncUnityEventHandler<ValueTuple<string, int, int>>(new TextSelectionEventConverter(inputField.onTextSelection), inputField.GetCancellationTokenOnDestroy(), false);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002245 File Offset: 0x00000445
		public static IAsyncTextSelectionEventHandler<ValueTuple<string, int, int>> GetAsyncTextSelectionEventHandler(this TMP_InputField inputField, CancellationToken cancellationToken)
		{
			return new AsyncUnityEventHandler<ValueTuple<string, int, int>>(new TextSelectionEventConverter(inputField.onTextSelection), cancellationToken, false);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002259 File Offset: 0x00000459
		public static UniTask<ValueTuple<string, int, int>> OnTextSelectionAsync(this TMP_InputField inputField)
		{
			return new AsyncUnityEventHandler<ValueTuple<string, int, int>>(new TextSelectionEventConverter(inputField.onTextSelection), inputField.GetCancellationTokenOnDestroy(), true).OnInvokeAsync();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002277 File Offset: 0x00000477
		public static UniTask<ValueTuple<string, int, int>> OnTextSelectionAsync(this TMP_InputField inputField, CancellationToken cancellationToken)
		{
			return new AsyncUnityEventHandler<ValueTuple<string, int, int>>(new TextSelectionEventConverter(inputField.onTextSelection), cancellationToken, true).OnInvokeAsync();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002290 File Offset: 0x00000490
		public static IUniTaskAsyncEnumerable<ValueTuple<string, int, int>> OnTextSelectionAsAsyncEnumerable(this TMP_InputField inputField)
		{
			return new UnityEventHandlerAsyncEnumerable<ValueTuple<string, int, int>>(new TextSelectionEventConverter(inputField.onTextSelection), inputField.GetCancellationTokenOnDestroy());
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000022A8 File Offset: 0x000004A8
		public static IUniTaskAsyncEnumerable<ValueTuple<string, int, int>> OnTextSelectionAsAsyncEnumerable(this TMP_InputField inputField, CancellationToken cancellationToken)
		{
			return new UnityEventHandlerAsyncEnumerable<ValueTuple<string, int, int>>(new TextSelectionEventConverter(inputField.onTextSelection), cancellationToken);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000022BB File Offset: 0x000004BB
		public static IAsyncDeselectEventHandler<string> GetAsyncDeselectEventHandler(this TMP_InputField inputField)
		{
			return new AsyncUnityEventHandler<string>(inputField.onDeselect, inputField.GetCancellationTokenOnDestroy(), false);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000022CF File Offset: 0x000004CF
		public static IAsyncDeselectEventHandler<string> GetAsyncDeselectEventHandler(this TMP_InputField inputField, CancellationToken cancellationToken)
		{
			return new AsyncUnityEventHandler<string>(inputField.onDeselect, cancellationToken, false);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000022DE File Offset: 0x000004DE
		public static UniTask<string> OnDeselectAsync(this TMP_InputField inputField)
		{
			return new AsyncUnityEventHandler<string>(inputField.onDeselect, inputField.GetCancellationTokenOnDestroy(), true).OnInvokeAsync();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000022F7 File Offset: 0x000004F7
		public static UniTask<string> OnDeselectAsync(this TMP_InputField inputField, CancellationToken cancellationToken)
		{
			return new AsyncUnityEventHandler<string>(inputField.onDeselect, cancellationToken, true).OnInvokeAsync();
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000230B File Offset: 0x0000050B
		public static IUniTaskAsyncEnumerable<string> OnDeselectAsAsyncEnumerable(this TMP_InputField inputField)
		{
			return new UnityEventHandlerAsyncEnumerable<string>(inputField.onDeselect, inputField.GetCancellationTokenOnDestroy());
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000231E File Offset: 0x0000051E
		public static IUniTaskAsyncEnumerable<string> OnDeselectAsAsyncEnumerable(this TMP_InputField inputField, CancellationToken cancellationToken)
		{
			return new UnityEventHandlerAsyncEnumerable<string>(inputField.onDeselect, cancellationToken);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000232C File Offset: 0x0000052C
		public static IAsyncSelectEventHandler<string> GetAsyncSelectEventHandler(this TMP_InputField inputField)
		{
			return new AsyncUnityEventHandler<string>(inputField.onSelect, inputField.GetCancellationTokenOnDestroy(), false);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002340 File Offset: 0x00000540
		public static IAsyncSelectEventHandler<string> GetAsyncSelectEventHandler(this TMP_InputField inputField, CancellationToken cancellationToken)
		{
			return new AsyncUnityEventHandler<string>(inputField.onSelect, cancellationToken, false);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000234F File Offset: 0x0000054F
		public static UniTask<string> OnSelectAsync(this TMP_InputField inputField)
		{
			return new AsyncUnityEventHandler<string>(inputField.onSelect, inputField.GetCancellationTokenOnDestroy(), true).OnInvokeAsync();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002368 File Offset: 0x00000568
		public static UniTask<string> OnSelectAsync(this TMP_InputField inputField, CancellationToken cancellationToken)
		{
			return new AsyncUnityEventHandler<string>(inputField.onSelect, cancellationToken, true).OnInvokeAsync();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000237C File Offset: 0x0000057C
		public static IUniTaskAsyncEnumerable<string> OnSelectAsAsyncEnumerable(this TMP_InputField inputField)
		{
			return new UnityEventHandlerAsyncEnumerable<string>(inputField.onSelect, inputField.GetCancellationTokenOnDestroy());
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000238F File Offset: 0x0000058F
		public static IUniTaskAsyncEnumerable<string> OnSelectAsAsyncEnumerable(this TMP_InputField inputField, CancellationToken cancellationToken)
		{
			return new UnityEventHandlerAsyncEnumerable<string>(inputField.onSelect, cancellationToken);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000239D File Offset: 0x0000059D
		public static IAsyncSubmitEventHandler<string> GetAsyncSubmitEventHandler(this TMP_InputField inputField)
		{
			return new AsyncUnityEventHandler<string>(inputField.onSubmit, inputField.GetCancellationTokenOnDestroy(), false);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000023B1 File Offset: 0x000005B1
		public static IAsyncSubmitEventHandler<string> GetAsyncSubmitEventHandler(this TMP_InputField inputField, CancellationToken cancellationToken)
		{
			return new AsyncUnityEventHandler<string>(inputField.onSubmit, cancellationToken, false);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000023C0 File Offset: 0x000005C0
		public static UniTask<string> OnSubmitAsync(this TMP_InputField inputField)
		{
			return new AsyncUnityEventHandler<string>(inputField.onSubmit, inputField.GetCancellationTokenOnDestroy(), true).OnInvokeAsync();
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000023D9 File Offset: 0x000005D9
		public static UniTask<string> OnSubmitAsync(this TMP_InputField inputField, CancellationToken cancellationToken)
		{
			return new AsyncUnityEventHandler<string>(inputField.onSubmit, cancellationToken, true).OnInvokeAsync();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000023ED File Offset: 0x000005ED
		public static IUniTaskAsyncEnumerable<string> OnSubmitAsAsyncEnumerable(this TMP_InputField inputField)
		{
			return new UnityEventHandlerAsyncEnumerable<string>(inputField.onSubmit, inputField.GetCancellationTokenOnDestroy());
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002400 File Offset: 0x00000600
		public static IUniTaskAsyncEnumerable<string> OnSubmitAsAsyncEnumerable(this TMP_InputField inputField, CancellationToken cancellationToken)
		{
			return new UnityEventHandlerAsyncEnumerable<string>(inputField.onSubmit, cancellationToken);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002410 File Offset: 0x00000610
		public static void BindTo(this IUniTaskAsyncEnumerable<string> source, TMP_Text text, bool rebindOnError = true)
		{
			TextMeshProAsyncExtensions.BindToCore(source, text, text.GetCancellationTokenOnDestroy(), rebindOnError).Forget();
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002434 File Offset: 0x00000634
		public static void BindTo(this IUniTaskAsyncEnumerable<string> source, TMP_Text text, CancellationToken cancellationToken, bool rebindOnError = true)
		{
			TextMeshProAsyncExtensions.BindToCore(source, text, cancellationToken, rebindOnError).Forget();
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002454 File Offset: 0x00000654
		private static async UniTaskVoid BindToCore(IUniTaskAsyncEnumerable<string> source, TMP_Text text, CancellationToken cancellationToken, bool rebindOnError)
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

		// Token: 0x06000030 RID: 48 RVA: 0x000024B0 File Offset: 0x000006B0
		public static void BindTo<T>(this IUniTaskAsyncEnumerable<T> source, TMP_Text text, bool rebindOnError = true)
		{
			TextMeshProAsyncExtensions.BindToCore<T>(source, text, text.GetCancellationTokenOnDestroy(), rebindOnError).Forget();
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000024D4 File Offset: 0x000006D4
		public static void BindTo<T>(this IUniTaskAsyncEnumerable<T> source, TMP_Text text, CancellationToken cancellationToken, bool rebindOnError = true)
		{
			TextMeshProAsyncExtensions.BindToCore<T>(source, text, cancellationToken, rebindOnError).Forget();
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000024F4 File Offset: 0x000006F4
		public static void BindTo<T>(this AsyncReactiveProperty<T> source, TMP_Text text, bool rebindOnError = true)
		{
			TextMeshProAsyncExtensions.BindToCore<T>(source, text, text.GetCancellationTokenOnDestroy(), rebindOnError).Forget();
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002518 File Offset: 0x00000718
		private static async UniTaskVoid BindToCore<T>(IUniTaskAsyncEnumerable<T> source, TMP_Text text, CancellationToken cancellationToken, bool rebindOnError)
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
	}
}
