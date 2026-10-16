using System;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Internal
{
	// Token: 0x02000110 RID: 272
	internal static class RuntimeHelpersAbstraction
	{
		// Token: 0x0600064A RID: 1610 RVA: 0x0000E820 File Offset: 0x0000CA20
		public static bool IsWellKnownNoReferenceContainsType<T>()
		{
			return RuntimeHelpersAbstraction.WellKnownNoReferenceContainsType<T>.IsWellKnownType;
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x0000E828 File Offset: 0x0000CA28
		private static bool WellKnownNoReferenceContainsTypeInitialize(Type t)
		{
			if (t.IsPrimitive)
			{
				return true;
			}
			if (t.IsEnum)
			{
				return true;
			}
			if (t == typeof(DateTime))
			{
				return true;
			}
			if (t == typeof(DateTimeOffset))
			{
				return true;
			}
			if (t == typeof(Guid))
			{
				return true;
			}
			if (t == typeof(decimal))
			{
				return true;
			}
			if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>))
			{
				return RuntimeHelpersAbstraction.WellKnownNoReferenceContainsTypeInitialize(t.GetGenericArguments()[0]);
			}
			return t == typeof(Vector2) || t == typeof(Vector3) || t == typeof(Vector4) || t == typeof(Color) || t == typeof(Rect) || t == typeof(Bounds) || t == typeof(Quaternion) || t == typeof(Vector2Int) || t == typeof(Vector3Int);
		}

		// Token: 0x02000211 RID: 529
		private static class WellKnownNoReferenceContainsType<T>
		{
			// Token: 0x04000559 RID: 1369
			public static readonly bool IsWellKnownType = RuntimeHelpersAbstraction.WellKnownNoReferenceContainsTypeInitialize(typeof(T));
		}
	}
}
