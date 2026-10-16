using System;
using System.ComponentModel;
using NUnit.Framework.Constraints;
using NUnit.Framework.Internal;

namespace NUnit.Framework
{
	// Token: 0x0200000F RID: 15
	public class Assume
	{
		// Token: 0x06000115 RID: 277 RVA: 0x0000383E File Offset: 0x00001A3E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new static bool Equals(object a, object b)
		{
			throw new InvalidOperationException("Assume.Equals should not be used for Assertions");
		}

		// Token: 0x06000116 RID: 278 RVA: 0x0000384A File Offset: 0x00001A4A
		public new static void ReferenceEquals(object a, object b)
		{
			throw new InvalidOperationException("Assume.ReferenceEquals should not be used for Assertions");
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00003856 File Offset: 0x00001A56
		public static void That<TActual>(ActualValueDelegate<TActual> del, IResolveConstraint expr)
		{
			Assume.That<TActual>(del, expr.Resolve(), null, null);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00003868 File Offset: 0x00001A68
		public static void That<TActual>(ActualValueDelegate<TActual> del, IResolveConstraint expr, string message, params object[] args)
		{
			ConstraintResult constraintResult = expr.Resolve().ApplyTo<TActual>(del);
			if (!constraintResult.IsSuccess)
			{
				MessageWriter messageWriter = new TextMessageWriter(message, args);
				constraintResult.WriteMessageTo(messageWriter);
				throw new InconclusiveException(messageWriter.ToString());
			}
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000038A5 File Offset: 0x00001AA5
		public static void That<TActual>(ActualValueDelegate<TActual> del, IResolveConstraint expr, Func<string> getExceptionMessage)
		{
			if (!expr.Resolve().ApplyTo<TActual>(del).IsSuccess)
			{
				throw new InconclusiveException(getExceptionMessage());
			}
		}

		// Token: 0x0600011A RID: 282 RVA: 0x000038C6 File Offset: 0x00001AC6
		public static void That(bool condition, string message, params object[] args)
		{
			Assume.That<bool>(condition, Is.True, message, args);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x000038D5 File Offset: 0x00001AD5
		public static void That(bool condition)
		{
			Assume.That<bool>(condition, Is.True, null, null);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x000038E4 File Offset: 0x00001AE4
		public static void That(bool condition, Func<string> getExceptionMessage)
		{
			Assume.That<bool>(condition, Is.True, getExceptionMessage);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x000038F2 File Offset: 0x00001AF2
		public static void That(Func<bool> condition, string message, params object[] args)
		{
			Assume.That<bool>(condition(), Is.True, message, args);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00003906 File Offset: 0x00001B06
		public static void That(Func<bool> condition)
		{
			Assume.That<bool>(condition(), Is.True, null, null);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000391A File Offset: 0x00001B1A
		public static void That(Func<bool> condition, Func<string> getExceptionMessage)
		{
			Assume.That<bool>(condition(), Is.True, getExceptionMessage);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x0000392D File Offset: 0x00001B2D
		public static void That(TestDelegate code, IResolveConstraint constraint)
		{
			Assume.That<object>(code, constraint);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00003936 File Offset: 0x00001B36
		public static void That<TActual>(TActual actual, IResolveConstraint expression)
		{
			Assume.That<TActual>(actual, expression, null, null);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00003944 File Offset: 0x00001B44
		public static void That<TActual>(TActual actual, IResolveConstraint expression, string message, params object[] args)
		{
			ConstraintResult constraintResult = expression.Resolve().ApplyTo(actual);
			if (!constraintResult.IsSuccess)
			{
				MessageWriter messageWriter = new TextMessageWriter(message, args);
				constraintResult.WriteMessageTo(messageWriter);
				throw new InconclusiveException(messageWriter.ToString());
			}
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00003986 File Offset: 0x00001B86
		public static void That<TActual>(TActual actual, IResolveConstraint expression, Func<string> getExceptionMessage)
		{
			if (!expression.Resolve().ApplyTo(actual).IsSuccess)
			{
				throw new InconclusiveException(getExceptionMessage());
			}
		}
	}
}
