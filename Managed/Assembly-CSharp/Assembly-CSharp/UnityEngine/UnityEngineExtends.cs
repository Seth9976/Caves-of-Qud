using System;

namespace UnityEngine
{
	// Token: 0x020003E9 RID: 1001
	public static class UnityEngineExtends
	{
		// Token: 0x0600256E RID: 9582 RVA: 0x000BEFA0 File Offset: 0x000BD1A0
		public static T Instantiate<T>(this T original) where T : Object
		{
			return Object.Instantiate<T>(original);
		}

		// Token: 0x0600256F RID: 9583 RVA: 0x000BEFA8 File Offset: 0x000BD1A8
		public static T Instantiate<T>(this T original, Vector3 position, Quaternion rotation) where T : Object
		{
			return Object.Instantiate<T>(original, position, rotation);
		}

		// Token: 0x06002570 RID: 9584 RVA: 0x000BEFB2 File Offset: 0x000BD1B2
		public static void Destroy(this GameObject obj)
		{
			obj.transform.SetParent(null);
			Object.Destroy(obj);
		}

		// Token: 0x06002571 RID: 9585 RVA: 0x000BEFC6 File Offset: 0x000BD1C6
		public static void Destroy(this Object obj)
		{
			Object.Destroy(obj);
		}

		// Token: 0x06002572 RID: 9586 RVA: 0x000BEFCE File Offset: 0x000BD1CE
		public static void Destroy(this GameObject obj, float waitTime)
		{
			obj.transform.parent = null;
			obj.Destroy(waitTime);
		}

		// Token: 0x06002573 RID: 9587 RVA: 0x000BEFE3 File Offset: 0x000BD1E3
		public static void Destroy(this Object obj, float waitTime)
		{
			Object.Destroy(obj, waitTime);
		}

		// Token: 0x06002574 RID: 9588 RVA: 0x000BEFEC File Offset: 0x000BD1EC
		public static void DestroyImmediate(this GameObject obj)
		{
			obj.transform.SetParent(null);
			Object.DestroyImmediate(obj);
		}

		// Token: 0x06002575 RID: 9589 RVA: 0x000BF000 File Offset: 0x000BD200
		public static void DestroyImmediate(this GameObject obj, bool allowDestroyingAssets)
		{
			obj.transform.parent = null;
			Object.DestroyImmediate(obj, allowDestroyingAssets);
		}

		// Token: 0x06002576 RID: 9590 RVA: 0x000BF015 File Offset: 0x000BD215
		public static void DestroyImmediate(this Object obj)
		{
			Object.DestroyImmediate(obj);
		}

		// Token: 0x06002577 RID: 9591 RVA: 0x000BF01D File Offset: 0x000BD21D
		public static void DestroyImmediate(this Object obj, bool allowDestroyingAssets)
		{
			Object.DestroyImmediate(obj, allowDestroyingAssets);
		}

		// Token: 0x06002578 RID: 9592 RVA: 0x000BF028 File Offset: 0x000BD228
		public static void SetLossyScale(this Transform trans, Vector3 worldScale)
		{
			Vector3 lossyScale = trans.lossyScale;
			Vector3 localScale = trans.localScale;
			trans.localScale = new Vector3(worldScale.x / lossyScale.x * localScale.x, worldScale.y / lossyScale.y * localScale.y, worldScale.z / lossyScale.z * localScale.z);
		}

		// Token: 0x06002579 RID: 9593 RVA: 0x000BF08C File Offset: 0x000BD28C
		public static T GetComponentUpwards<T>(this GameObject _go) where T : Component
		{
			Transform transform = _go.transform.parent;
			while (transform != null)
			{
				T t = transform.GetComponent(typeof(T)) as T;
				if (t != null)
				{
					return t;
				}
				transform = transform.parent;
			}
			return default(T);
		}

		// Token: 0x0600257A RID: 9594 RVA: 0x000BF0EB File Offset: 0x000BD2EB
		public static T FindParentComponent<T>(this T component) where T : Component
		{
			return component.gameObject.GetComponentUpwards<T>();
		}

		// Token: 0x0600257B RID: 9595 RVA: 0x000BF100 File Offset: 0x000BD300
		public static T GetComponentInChildrenFast<T>(this GameObject go) where T : Component
		{
			if (go.activeInHierarchy)
			{
				Component component = go.GetComponent(typeof(T));
				if (component != null)
				{
					return component as T;
				}
			}
			Transform transform = go.transform;
			if (transform != null)
			{
				int childCount = transform.childCount;
				for (int i = 0; i < childCount; i++)
				{
					T componentInChildrenFast = transform.GetChild(i).gameObject.GetComponentInChildrenFast<T>();
					if (componentInChildrenFast != null)
					{
						return componentInChildrenFast;
					}
				}
			}
			return default(T);
		}

		// Token: 0x0600257C RID: 9596 RVA: 0x000BF18D File Offset: 0x000BD38D
		public static T GetComponentInChildrenFast<T>(this Component component) where T : Component
		{
			return component.gameObject.GetComponentInChildrenFast<T>();
		}
	}
}
