using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace NUnit.Framework.Internal
{
	// Token: 0x02000066 RID: 102
	public class ExceptionHelper
	{
		// Token: 0x0600036F RID: 879 RVA: 0x00007AE4 File Offset: 0x00005CE4
		static ExceptionHelper()
		{
			MethodInfo method = typeof(Exception).GetMethod("InternalPreserveStackTrace", BindingFlags.Instance | BindingFlags.NonPublic);
			if (method != null)
			{
				try
				{
					ExceptionHelper.PreserveStackTrace = (Action<Exception>)Delegate.CreateDelegate(typeof(Action<Exception>), method);
					return;
				}
				catch (InvalidOperationException)
				{
				}
			}
			ExceptionHelper.PreserveStackTrace = delegate(Exception _)
			{
			};
		}

		// Token: 0x06000370 RID: 880 RVA: 0x00007B58 File Offset: 0x00005D58
		public static void Rethrow(Exception exception)
		{
			ExceptionHelper.PreserveStackTrace(exception);
			throw exception;
		}

		// Token: 0x06000371 RID: 881 RVA: 0x00007B68 File Offset: 0x00005D68
		public static string BuildMessage(Exception exception)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat(CultureInfo.CurrentCulture, "{0} : {1}", new object[]
			{
				exception.GetType().ToString(),
				exception.Message
			});
			foreach (Exception ex in ExceptionHelper.FlattenExceptionHierarchy(exception))
			{
				stringBuilder.Append(Env.NewLine);
				stringBuilder.AppendFormat(CultureInfo.CurrentCulture, "  ----> {0} : {1}", new object[]
				{
					ex.GetType().ToString(),
					ex.Message
				});
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000372 RID: 882 RVA: 0x00007C28 File Offset: 0x00005E28
		public static string BuildStackTrace(Exception exception)
		{
			StringBuilder stringBuilder = new StringBuilder(ExceptionHelper.GetStackTrace(exception));
			foreach (Exception ex in ExceptionHelper.FlattenExceptionHierarchy(exception))
			{
				stringBuilder.Append(Env.NewLine);
				stringBuilder.Append("--");
				stringBuilder.Append(ex.GetType().Name);
				stringBuilder.Append(Env.NewLine);
				stringBuilder.Append(ExceptionHelper.GetStackTrace(ex));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000373 RID: 883 RVA: 0x00007CCC File Offset: 0x00005ECC
		public static string GetStackTrace(Exception exception)
		{
			string text;
			try
			{
				text = exception.StackTrace;
			}
			catch (Exception)
			{
				text = "No stack trace available";
			}
			return text;
		}

		// Token: 0x06000374 RID: 884 RVA: 0x00007CFC File Offset: 0x00005EFC
		private static List<Exception> FlattenExceptionHierarchy(Exception exception)
		{
			List<Exception> list = new List<Exception>();
			if (exception is AggregateException)
			{
				AggregateException ex = exception as AggregateException;
				list.AddRange(ex.InnerExceptions);
				using (IEnumerator<Exception> enumerator = ex.InnerExceptions.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Exception ex2 = enumerator.Current;
						list.AddRange(ExceptionHelper.FlattenExceptionHierarchy(ex2));
					}
					return list;
				}
			}
			if (exception.InnerException != null)
			{
				list.Add(exception.InnerException);
				list.AddRange(ExceptionHelper.FlattenExceptionHierarchy(exception.InnerException));
			}
			return list;
		}

		// Token: 0x04000070 RID: 112
		private static readonly Action<Exception> PreserveStackTrace;
	}
}
