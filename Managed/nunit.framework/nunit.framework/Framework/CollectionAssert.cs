using System;
using System.Collections;
using System.ComponentModel;
using NUnit.Framework.Constraints;

namespace NUnit.Framework
{
	// Token: 0x02000047 RID: 71
	public class CollectionAssert
	{
		// Token: 0x0600021C RID: 540 RVA: 0x00006198 File Offset: 0x00004398
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new static bool Equals(object a, object b)
		{
			throw new InvalidOperationException("CollectionAssert.Equals should not be used for Assertions");
		}

		// Token: 0x0600021D RID: 541 RVA: 0x000061A4 File Offset: 0x000043A4
		public new static void ReferenceEquals(object a, object b)
		{
			throw new InvalidOperationException("CollectionAssert.ReferenceEquals should not be used for Assertions");
		}

		// Token: 0x0600021E RID: 542 RVA: 0x000061B0 File Offset: 0x000043B0
		public static void AllItemsAreInstancesOfType(IEnumerable collection, Type expectedType)
		{
			CollectionAssert.AllItemsAreInstancesOfType(collection, expectedType, string.Empty, null);
		}

		// Token: 0x0600021F RID: 543 RVA: 0x000061BF File Offset: 0x000043BF
		public static void AllItemsAreInstancesOfType(IEnumerable collection, Type expectedType, string message, params object[] args)
		{
			Assert.That<IEnumerable>(collection, Is.All.InstanceOf(expectedType), message, args);
		}

		// Token: 0x06000220 RID: 544 RVA: 0x000061D4 File Offset: 0x000043D4
		public static void AllItemsAreNotNull(IEnumerable collection)
		{
			CollectionAssert.AllItemsAreNotNull(collection, string.Empty, null);
		}

		// Token: 0x06000221 RID: 545 RVA: 0x000061E2 File Offset: 0x000043E2
		public static void AllItemsAreNotNull(IEnumerable collection, string message, params object[] args)
		{
			Assert.That<IEnumerable>(collection, Is.All.Not.Null, message, args);
		}

		// Token: 0x06000222 RID: 546 RVA: 0x000061FB File Offset: 0x000043FB
		public static void AllItemsAreUnique(IEnumerable collection)
		{
			CollectionAssert.AllItemsAreUnique(collection, string.Empty, null);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00006209 File Offset: 0x00004409
		public static void AllItemsAreUnique(IEnumerable collection, string message, params object[] args)
		{
			Assert.That<IEnumerable>(collection, Is.Unique, message, args);
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00006218 File Offset: 0x00004418
		public static void AreEqual(IEnumerable expected, IEnumerable actual)
		{
			CollectionAssert.AreEqual(expected, actual, string.Empty, null);
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00006227 File Offset: 0x00004427
		public static void AreEqual(IEnumerable expected, IEnumerable actual, IComparer comparer)
		{
			CollectionAssert.AreEqual(expected, actual, comparer, string.Empty, null);
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00006237 File Offset: 0x00004437
		public static void AreEqual(IEnumerable expected, IEnumerable actual, string message, params object[] args)
		{
			Assert.That<IEnumerable>(actual, Is.EqualTo(expected), message, args);
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00006247 File Offset: 0x00004447
		public static void AreEqual(IEnumerable expected, IEnumerable actual, IComparer comparer, string message, params object[] args)
		{
			Assert.That<IEnumerable>(actual, Is.EqualTo(expected).Using(comparer), message, args);
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000625E File Offset: 0x0000445E
		public static void AreEquivalent(IEnumerable expected, IEnumerable actual)
		{
			CollectionAssert.AreEquivalent(expected, actual, string.Empty, null);
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000626D File Offset: 0x0000446D
		public static void AreEquivalent(IEnumerable expected, IEnumerable actual, string message, params object[] args)
		{
			Assert.That<IEnumerable>(actual, Is.EquivalentTo(expected), message, args);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000627D File Offset: 0x0000447D
		public static void AreNotEqual(IEnumerable expected, IEnumerable actual)
		{
			CollectionAssert.AreNotEqual(expected, actual, string.Empty, null);
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000628C File Offset: 0x0000448C
		public static void AreNotEqual(IEnumerable expected, IEnumerable actual, IComparer comparer)
		{
			CollectionAssert.AreNotEqual(expected, actual, comparer, string.Empty, null);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000629C File Offset: 0x0000449C
		public static void AreNotEqual(IEnumerable expected, IEnumerable actual, string message, params object[] args)
		{
			Assert.That<IEnumerable>(actual, Is.Not.EqualTo(expected), message, args);
		}

		// Token: 0x0600022D RID: 557 RVA: 0x000062B1 File Offset: 0x000044B1
		public static void AreNotEqual(IEnumerable expected, IEnumerable actual, IComparer comparer, string message, params object[] args)
		{
			Assert.That<IEnumerable>(actual, Is.Not.EqualTo(expected).Using(comparer), message, args);
		}

		// Token: 0x0600022E RID: 558 RVA: 0x000062CD File Offset: 0x000044CD
		public static void AreNotEquivalent(IEnumerable expected, IEnumerable actual)
		{
			CollectionAssert.AreNotEquivalent(expected, actual, string.Empty, null);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x000062DC File Offset: 0x000044DC
		public static void AreNotEquivalent(IEnumerable expected, IEnumerable actual, string message, params object[] args)
		{
			Assert.That<IEnumerable>(actual, Is.Not.EquivalentTo(expected), message, args);
		}

		// Token: 0x06000230 RID: 560 RVA: 0x000062F1 File Offset: 0x000044F1
		public static void Contains(IEnumerable collection, object actual)
		{
			CollectionAssert.Contains(collection, actual, string.Empty, null);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00006300 File Offset: 0x00004500
		public static void Contains(IEnumerable collection, object actual, string message, params object[] args)
		{
			Assert.That<IEnumerable>(collection, Has.Member(actual), message, args);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00006310 File Offset: 0x00004510
		public static void DoesNotContain(IEnumerable collection, object actual)
		{
			CollectionAssert.DoesNotContain(collection, actual, string.Empty, null);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000631F File Offset: 0x0000451F
		public static void DoesNotContain(IEnumerable collection, object actual, string message, params object[] args)
		{
			Assert.That<IEnumerable>(collection, Has.No.Member(actual), message, args);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00006334 File Offset: 0x00004534
		public static void IsNotSubsetOf(IEnumerable subset, IEnumerable superset)
		{
			CollectionAssert.IsNotSubsetOf(subset, superset, string.Empty, null);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00006343 File Offset: 0x00004543
		public static void IsNotSubsetOf(IEnumerable subset, IEnumerable superset, string message, params object[] args)
		{
			Assert.That<IEnumerable>(subset, Is.Not.SubsetOf(superset), message, args);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00006358 File Offset: 0x00004558
		public static void IsSubsetOf(IEnumerable subset, IEnumerable superset)
		{
			CollectionAssert.IsSubsetOf(subset, superset, string.Empty, null);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00006367 File Offset: 0x00004567
		public static void IsSubsetOf(IEnumerable subset, IEnumerable superset, string message, params object[] args)
		{
			Assert.That<IEnumerable>(subset, Is.SubsetOf(superset), message, args);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00006377 File Offset: 0x00004577
		public static void IsNotSupersetOf(IEnumerable superset, IEnumerable subset)
		{
			CollectionAssert.IsNotSupersetOf(superset, subset, string.Empty, null);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00006386 File Offset: 0x00004586
		public static void IsNotSupersetOf(IEnumerable superset, IEnumerable subset, string message, params object[] args)
		{
			Assert.That<IEnumerable>(superset, Is.Not.SupersetOf(subset), message, args);
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000639B File Offset: 0x0000459B
		public static void IsSupersetOf(IEnumerable superset, IEnumerable subset)
		{
			CollectionAssert.IsSupersetOf(superset, subset, string.Empty, null);
		}

		// Token: 0x0600023B RID: 571 RVA: 0x000063AA File Offset: 0x000045AA
		public static void IsSupersetOf(IEnumerable superset, IEnumerable subset, string message, params object[] args)
		{
			Assert.That<IEnumerable>(superset, Is.SupersetOf(subset), message, args);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00002C0C File Offset: 0x00000E0C
		public static void IsEmpty(IEnumerable collection, string message, params object[] args)
		{
			Assert.That<IEnumerable>(collection, new EmptyCollectionConstraint(), message, args);
		}

		// Token: 0x0600023D RID: 573 RVA: 0x000063BA File Offset: 0x000045BA
		public static void IsEmpty(IEnumerable collection)
		{
			CollectionAssert.IsEmpty(collection, string.Empty, null);
		}

		// Token: 0x0600023E RID: 574 RVA: 0x000063C8 File Offset: 0x000045C8
		public static void IsNotEmpty(IEnumerable collection, string message, params object[] args)
		{
			Assert.That<IEnumerable>(collection, new NotConstraint(new EmptyCollectionConstraint()), message, args);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x000063DC File Offset: 0x000045DC
		public static void IsNotEmpty(IEnumerable collection)
		{
			CollectionAssert.IsNotEmpty(collection, string.Empty, null);
		}

		// Token: 0x06000240 RID: 576 RVA: 0x000063EA File Offset: 0x000045EA
		public static void IsOrdered(IEnumerable collection, string message, params object[] args)
		{
			Assert.That<IEnumerable>(collection, Is.Ordered, message, args);
		}

		// Token: 0x06000241 RID: 577 RVA: 0x000063F9 File Offset: 0x000045F9
		public static void IsOrdered(IEnumerable collection)
		{
			CollectionAssert.IsOrdered(collection, string.Empty, null);
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00006407 File Offset: 0x00004607
		public static void IsOrdered(IEnumerable collection, IComparer comparer, string message, params object[] args)
		{
			Assert.That<IEnumerable>(collection, Is.Ordered.Using(comparer), message, args);
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000641C File Offset: 0x0000461C
		public static void IsOrdered(IEnumerable collection, IComparer comparer)
		{
			CollectionAssert.IsOrdered(collection, comparer, string.Empty, null);
		}
	}
}
