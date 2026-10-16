using System;
using System.Collections;
using NUnit.Framework.Constraints;

namespace NUnit.Framework
{
	// Token: 0x0200000E RID: 14
	public class AssertionHelper : ConstraintFactory
	{
		// Token: 0x0600010C RID: 268 RVA: 0x000037D5 File Offset: 0x000019D5
		public void Expect(bool condition, string message, params object[] args)
		{
			Assert.That<bool>(condition, Is.True, message, args);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000037E4 File Offset: 0x000019E4
		public void Expect(bool condition)
		{
			Assert.That<bool>(condition, Is.True, null, null);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x000037F3 File Offset: 0x000019F3
		public void Expect<TActual>(ActualValueDelegate<TActual> del, IResolveConstraint expr)
		{
			Assert.That<TActual>(del, expr.Resolve(), null, null);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00003803 File Offset: 0x00001A03
		public void Expect<TActual>(ActualValueDelegate<TActual> del, IResolveConstraint expr, string message, params object[] args)
		{
			Assert.That<TActual>(del, expr, message, args);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0000380F File Offset: 0x00001A0F
		public void Expect(TestDelegate code, IResolveConstraint constraint)
		{
			Assert.That<object>(code, constraint);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00003818 File Offset: 0x00001A18
		public static void Expect<TActual>(TActual actual, IResolveConstraint expression)
		{
			Assert.That<TActual>(actual, expression, null, null);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00003823 File Offset: 0x00001A23
		public static void Expect<TActual>(TActual actual, IResolveConstraint expression, string message, params object[] args)
		{
			Assert.That<TActual>(actual, expression, message, args);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x0000382E File Offset: 0x00001A2E
		public ListMapper Map(ICollection original)
		{
			return new ListMapper(original);
		}
	}
}
