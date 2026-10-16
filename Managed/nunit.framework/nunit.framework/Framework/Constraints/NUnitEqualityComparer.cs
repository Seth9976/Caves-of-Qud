using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using NUnit.Compatibility;

namespace NUnit.Framework.Constraints
{
	// Token: 0x02000121 RID: 289
	public class NUnitEqualityComparer
	{
		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000909 RID: 2313 RVA: 0x0001576E File Offset: 0x0001396E
		public static NUnitEqualityComparer Default
		{
			get
			{
				return new NUnitEqualityComparer();
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x0600090A RID: 2314 RVA: 0x00015775 File Offset: 0x00013975
		// (set) Token: 0x0600090B RID: 2315 RVA: 0x0001577D File Offset: 0x0001397D
		public bool IgnoreCase
		{
			get
			{
				return this.caseInsensitive;
			}
			set
			{
				this.caseInsensitive = value;
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x0600090C RID: 2316 RVA: 0x00015786 File Offset: 0x00013986
		// (set) Token: 0x0600090D RID: 2317 RVA: 0x0001578E File Offset: 0x0001398E
		public bool CompareAsCollection
		{
			get
			{
				return this.compareAsCollection;
			}
			set
			{
				this.compareAsCollection = value;
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x0600090E RID: 2318 RVA: 0x00015797 File Offset: 0x00013997
		public IList<EqualityAdapter> ExternalComparers
		{
			get
			{
				return this.externalComparers;
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x0600090F RID: 2319 RVA: 0x0001579F File Offset: 0x0001399F
		public IList<NUnitEqualityComparer.FailurePoint> FailurePoints
		{
			get
			{
				return this.failurePoints;
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000910 RID: 2320 RVA: 0x000157A7 File Offset: 0x000139A7
		// (set) Token: 0x06000911 RID: 2321 RVA: 0x000157AF File Offset: 0x000139AF
		public bool WithSameOffset { get; set; }

		// Token: 0x06000912 RID: 2322 RVA: 0x000157B8 File Offset: 0x000139B8
		public bool AreEqual(object x, object y, ref Tolerance tolerance)
		{
			this.failurePoints = new List<NUnitEqualityComparer.FailurePoint>();
			NUnitEqualityComparer.CheckGameObjectReference<object>(ref x);
			NUnitEqualityComparer.CheckGameObjectReference<object>(ref y);
			if (x == null && y == null)
			{
				return true;
			}
			if (x == null || y == null)
			{
				return false;
			}
			if (x == y)
			{
				return true;
			}
			Type type = x.GetType();
			Type type2 = y.GetType();
			Type type3 = (type.GetTypeInfo().IsGenericType ? type.GetGenericTypeDefinition() : null);
			Type type4 = (type2.GetTypeInfo().IsGenericType ? type2.GetGenericTypeDefinition() : null);
			EqualityAdapter externalComparer = this.GetExternalComparer(x, y);
			if (externalComparer != null)
			{
				return externalComparer.AreEqual(x, y);
			}
			if (type.IsArray && type2.IsArray && !this.compareAsCollection)
			{
				return this.ArraysEqual((Array)x, (Array)y, ref tolerance);
			}
			if (x is IDictionary && y is IDictionary)
			{
				return this.DictionariesEqual((IDictionary)x, (IDictionary)y, ref tolerance);
			}
			if (x is DictionaryEntry && y is DictionaryEntry)
			{
				return this.DictionaryEntriesEqual((DictionaryEntry)x, (DictionaryEntry)y, ref tolerance);
			}
			if (type3 == typeof(KeyValuePair<, >) && type4 == typeof(KeyValuePair<, >))
			{
				Tolerance exact = Tolerance.Exact;
				object value = type.GetProperty("Key").GetValue(x, null);
				object value2 = type2.GetProperty("Key").GetValue(y, null);
				object value3 = type.GetProperty("Value").GetValue(x, null);
				object value4 = type2.GetProperty("Value").GetValue(y, null);
				return this.AreEqual(value, value2, ref exact) && this.AreEqual(value3, value4, ref tolerance);
			}
			if (x is string && y is string)
			{
				return this.StringsEqual((string)x, (string)y);
			}
			if (x is Stream && y is Stream)
			{
				return this.StreamsEqual((Stream)x, (Stream)y);
			}
			if (x is char && y is char)
			{
				return this.CharsEqual((char)x, (char)y);
			}
			if (x is DirectoryInfo && y is DirectoryInfo)
			{
				return NUnitEqualityComparer.DirectoriesEqual((DirectoryInfo)x, (DirectoryInfo)y);
			}
			if (Numerics.IsNumericType(x) && Numerics.IsNumericType(y))
			{
				return Numerics.AreEqual(x, y, ref tolerance);
			}
			if (x is DateTimeOffset && y is DateTimeOffset)
			{
				DateTimeOffset dateTimeOffset = (DateTimeOffset)x;
				DateTimeOffset dateTimeOffset2 = (DateTimeOffset)y;
				bool flag;
				if (tolerance != null && tolerance.Value is TimeSpan)
				{
					TimeSpan timeSpan = (TimeSpan)tolerance.Value;
					flag = (dateTimeOffset - dateTimeOffset2).Duration() <= timeSpan;
				}
				else
				{
					flag = dateTimeOffset == dateTimeOffset2;
				}
				if (flag && this.WithSameOffset)
				{
					flag = dateTimeOffset.Offset == dateTimeOffset2.Offset;
				}
				return flag;
			}
			if (tolerance != null && tolerance.Value is TimeSpan)
			{
				TimeSpan timeSpan2 = (TimeSpan)tolerance.Value;
				if (x is DateTime && y is DateTime)
				{
					return ((DateTime)x - (DateTime)y).Duration() <= timeSpan2;
				}
				if (x is TimeSpan && y is TimeSpan)
				{
					return ((TimeSpan)x - (TimeSpan)y).Duration() <= timeSpan2;
				}
			}
			MethodInfo methodInfo = NUnitEqualityComparer.FirstImplementsIEquatableOfSecond(type, type2);
			if (methodInfo != null)
			{
				return NUnitEqualityComparer.InvokeFirstIEquatableEqualsSecond(x, y, methodInfo);
			}
			if (type != type2 && (methodInfo = NUnitEqualityComparer.FirstImplementsIEquatableOfSecond(type2, type)) != null)
			{
				return NUnitEqualityComparer.InvokeFirstIEquatableEqualsSecond(y, x, methodInfo);
			}
			if (x is IEnumerable && y is IEnumerable)
			{
				return this.EnumerablesEqual((IEnumerable)x, (IEnumerable)y, ref tolerance);
			}
			return x.Equals(y);
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x00015B80 File Offset: 0x00013D80
		private static MethodInfo FirstImplementsIEquatableOfSecond(Type first, Type second)
		{
			KeyValuePair<Type, MethodInfo> keyValuePair = default(KeyValuePair<Type, MethodInfo>);
			foreach (KeyValuePair<Type, MethodInfo> keyValuePair2 in NUnitEqualityComparer.GetEquatableGenericArguments(first))
			{
				if (keyValuePair2.Key.IsAssignableFrom(second) && (keyValuePair.Key == null || keyValuePair.Key.IsAssignableFrom(keyValuePair2.Key)))
				{
					keyValuePair = keyValuePair2;
				}
			}
			return keyValuePair.Value;
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x00015C0C File Offset: 0x00013E0C
		private static IList<KeyValuePair<Type, MethodInfo>> GetEquatableGenericArguments(Type type)
		{
			List<KeyValuePair<Type, MethodInfo>> list = new List<KeyValuePair<Type, MethodInfo>>();
			foreach (Type type2 in type.GetInterfaces())
			{
				if (type2.GetTypeInfo().IsGenericType && type2.GetGenericTypeDefinition().Equals(typeof(IEquatable<>)))
				{
					list.Add(new KeyValuePair<Type, MethodInfo>(type2.GetGenericArguments()[0], type2.GetMethod("Equals")));
				}
			}
			return list;
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x00015C7C File Offset: 0x00013E7C
		private static bool InvokeFirstIEquatableEqualsSecond(object first, object second, MethodInfo equals)
		{
			bool flag;
			try
			{
				flag = equals != null && (bool)equals.Invoke(first, new object[] { second });
			}
			catch (TargetInvocationException)
			{
				flag = first.Equals(second);
			}
			return flag;
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x00015CCC File Offset: 0x00013ECC
		private EqualityAdapter GetExternalComparer(object x, object y)
		{
			foreach (EqualityAdapter equalityAdapter in this.externalComparers)
			{
				if (equalityAdapter.CanCompare(x, y))
				{
					return equalityAdapter;
				}
			}
			return null;
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x00015D2C File Offset: 0x00013F2C
		private bool ArraysEqual(Array x, Array y, ref Tolerance tolerance)
		{
			int rank = x.Rank;
			if (rank != y.Rank)
			{
				return false;
			}
			for (int i = 1; i < rank; i++)
			{
				if (x.GetLength(i) != y.GetLength(i))
				{
					return false;
				}
			}
			return this.EnumerablesEqual(x, y, ref tolerance);
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x00015D74 File Offset: 0x00013F74
		private bool DictionariesEqual(IDictionary x, IDictionary y, ref Tolerance tolerance)
		{
			if (x.Count != y.Count)
			{
				return false;
			}
			CollectionTally collectionTally = new CollectionTally(this, x.Keys);
			if (!collectionTally.TryRemove(y.Keys) || collectionTally.Count > 0)
			{
				return false;
			}
			foreach (object obj in x.Keys)
			{
				if (!this.AreEqual(x[obj], y[obj], ref tolerance))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x00015E18 File Offset: 0x00014018
		private bool DictionaryEntriesEqual(DictionaryEntry x, DictionaryEntry y, ref Tolerance tolerance)
		{
			Tolerance exact = Tolerance.Exact;
			return this.AreEqual(x.Key, y.Key, ref exact) && this.AreEqual(x.Value, y.Value, ref tolerance);
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x00015E5C File Offset: 0x0001405C
		private bool CollectionsEqual(ICollection x, ICollection y, ref Tolerance tolerance)
		{
			IEnumerator enumerator = null;
			IEnumerator enumerator2 = null;
			bool flag3;
			try
			{
				enumerator = x.GetEnumerator();
				enumerator2 = y.GetEnumerator();
				int num = 0;
				bool flag;
				bool flag2;
				for (;;)
				{
					flag = enumerator.MoveNext();
					flag2 = enumerator2.MoveNext();
					if (!flag && !flag2)
					{
						break;
					}
					if (flag != flag2 || !this.AreEqual(enumerator.Current, enumerator2.Current, ref tolerance))
					{
						goto IL_004C;
					}
					num++;
				}
				return true;
				IL_004C:
				NUnitEqualityComparer.FailurePoint failurePoint = new NUnitEqualityComparer.FailurePoint();
				failurePoint.Position = (long)num;
				failurePoint.ExpectedHasData = flag;
				if (flag)
				{
					failurePoint.ExpectedValue = enumerator.Current;
				}
				failurePoint.ActualHasData = flag2;
				if (flag2)
				{
					failurePoint.ActualValue = enumerator2.Current;
				}
				this.failurePoints.Insert(0, failurePoint);
				flag3 = false;
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
				IDisposable disposable2 = enumerator2 as IDisposable;
				if (disposable2 != null)
				{
					disposable2.Dispose();
				}
			}
			return flag3;
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x00015F4C File Offset: 0x0001414C
		private bool StringsEqual(string x, string y)
		{
			string text = (this.caseInsensitive ? x.ToLower() : x);
			string text2 = (this.caseInsensitive ? y.ToLower() : y);
			return text.Equals(text2);
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x00015F84 File Offset: 0x00014184
		private bool CharsEqual(char x, char y)
		{
			char c = (this.caseInsensitive ? char.ToLower(x) : x);
			char c2 = (this.caseInsensitive ? char.ToLower(y) : y);
			return c == c2;
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x00015FB8 File Offset: 0x000141B8
		private bool EnumerablesEqual(IEnumerable x, IEnumerable y, ref Tolerance tolerance)
		{
			IEnumerator enumerator = null;
			IEnumerator enumerator2 = null;
			bool flag3;
			try
			{
				enumerator = x.GetEnumerator();
				enumerator2 = y.GetEnumerator();
				int num = 0;
				bool flag;
				bool flag2;
				for (;;)
				{
					flag = enumerator.MoveNext();
					flag2 = enumerator2.MoveNext();
					if (!flag && !flag2)
					{
						break;
					}
					if (flag != flag2 || !this.AreEqual(enumerator.Current, enumerator2.Current, ref tolerance))
					{
						goto IL_004C;
					}
					num++;
				}
				return true;
				IL_004C:
				NUnitEqualityComparer.FailurePoint failurePoint = new NUnitEqualityComparer.FailurePoint();
				failurePoint.Position = (long)num;
				failurePoint.ExpectedHasData = flag;
				if (flag)
				{
					failurePoint.ExpectedValue = enumerator.Current;
				}
				failurePoint.ActualHasData = flag2;
				if (flag2)
				{
					failurePoint.ActualValue = enumerator2.Current;
				}
				this.failurePoints.Insert(0, failurePoint);
				flag3 = false;
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
				IDisposable disposable2 = enumerator2 as IDisposable;
				if (disposable2 != null)
				{
					disposable2.Dispose();
				}
			}
			return flag3;
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x000160A8 File Offset: 0x000142A8
		private static bool DirectoriesEqual(DirectoryInfo x, DirectoryInfo y)
		{
			return x.Attributes == y.Attributes && !(x.CreationTime != y.CreationTime) && !(x.LastAccessTime != y.LastAccessTime) && new SamePathConstraint(x.FullName).ApplyTo(y.FullName).IsSuccess;
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x00016108 File Offset: 0x00014308
		private bool StreamsEqual(Stream x, Stream y)
		{
			if (x == y)
			{
				return true;
			}
			if (!x.CanRead)
			{
				throw new ArgumentException("Stream is not readable", "expected");
			}
			if (!y.CanRead)
			{
				throw new ArgumentException("Stream is not readable", "actual");
			}
			if (!x.CanSeek)
			{
				throw new ArgumentException("Stream is not seekable", "expected");
			}
			if (!y.CanSeek)
			{
				throw new ArgumentException("Stream is not seekable", "actual");
			}
			if (x.Length != y.Length)
			{
				return false;
			}
			byte[] array = new byte[NUnitEqualityComparer.BUFFER_SIZE];
			byte[] array2 = new byte[NUnitEqualityComparer.BUFFER_SIZE];
			BinaryReader binaryReader = new BinaryReader(x);
			BinaryReader binaryReader2 = new BinaryReader(y);
			long position = x.Position;
			long position2 = y.Position;
			try
			{
				binaryReader.BaseStream.Seek(0L, SeekOrigin.Begin);
				binaryReader2.BaseStream.Seek(0L, SeekOrigin.Begin);
				for (long num = 0L; num < x.Length; num += (long)NUnitEqualityComparer.BUFFER_SIZE)
				{
					binaryReader.Read(array, 0, NUnitEqualityComparer.BUFFER_SIZE);
					binaryReader2.Read(array2, 0, NUnitEqualityComparer.BUFFER_SIZE);
					for (int i = 0; i < NUnitEqualityComparer.BUFFER_SIZE; i++)
					{
						if (array[i] != array2[i])
						{
							NUnitEqualityComparer.FailurePoint failurePoint = new NUnitEqualityComparer.FailurePoint();
							failurePoint.Position = num + (long)i;
							failurePoint.ExpectedHasData = true;
							failurePoint.ExpectedValue = array[i];
							failurePoint.ActualHasData = true;
							failurePoint.ActualValue = array2[i];
							this.failurePoints.Insert(0, failurePoint);
							return false;
						}
					}
				}
			}
			finally
			{
				x.Position = position;
				y.Position = position2;
			}
			return true;
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x000162B8 File Offset: 0x000144B8
		internal static void CheckGameObjectReference<T>(ref T value)
		{
			if (value != null && NUnitEqualityComparer.GameObjectType != null && NUnitEqualityComparer.GameObjectType.IsInstanceOfType(value))
			{
				MethodInfo method = NUnitEqualityComparer.GameObjectType.GetMethod("GetInstanceID");
				if (method != null)
				{
					if (method.ReturnType == typeof(int))
					{
						if ((int)method.Invoke(value, null) == 0)
						{
							value = default(T);
							return;
						}
					}
					else if (method.ReturnType == NUnitEqualityComparer.InstanceIDType)
					{
						MethodInfo method2 = method.ReturnType.GetMethod("IsValid");
						if (method2 != null && !(bool)method2.Invoke(method.Invoke(value, null), null))
						{
							value = default(T);
							return;
						}
					}
					else
					{
						value = default(T);
					}
				}
			}
		}

		// Token: 0x040001D2 RID: 466
		private bool caseInsensitive;

		// Token: 0x040001D3 RID: 467
		private bool compareAsCollection;

		// Token: 0x040001D4 RID: 468
		private List<EqualityAdapter> externalComparers = new List<EqualityAdapter>();

		// Token: 0x040001D5 RID: 469
		private List<NUnitEqualityComparer.FailurePoint> failurePoints;

		// Token: 0x040001D6 RID: 470
		private static readonly int BUFFER_SIZE = 4096;

		// Token: 0x040001D8 RID: 472
		private static readonly Type GameObjectType = Type.GetType("UnityEngine.Object, UnityEngine, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null");

		// Token: 0x040001D9 RID: 473
		private static readonly Type InstanceIDType = Type.GetType("UnityEngine.InstanceID, UnityEngine, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null");

		// Token: 0x020001B4 RID: 436
		public class FailurePoint
		{
			// Token: 0x040002C7 RID: 711
			public long Position;

			// Token: 0x040002C8 RID: 712
			public object ExpectedValue;

			// Token: 0x040002C9 RID: 713
			public object ActualValue;

			// Token: 0x040002CA RID: 714
			public bool ExpectedHasData;

			// Token: 0x040002CB RID: 715
			public bool ActualHasData;
		}
	}
}
