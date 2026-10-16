using System;

namespace UnityEngine
{
	// Token: 0x0200024A RID: 586
	public class ResourcesAPI
	{
		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06001EDC RID: 7900 RVA: 0x0003B5A2 File Offset: 0x000397A2
		internal static ResourcesAPI ActiveAPI
		{
			get
			{
				return ResourcesAPI.overrideAPI ?? ResourcesAPI.s_DefaultAPI;
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06001EDD RID: 7901 RVA: 0x0003B5B2 File Offset: 0x000397B2
		// (set) Token: 0x06001EDE RID: 7902 RVA: 0x0003B5B9 File Offset: 0x000397B9
		public static ResourcesAPI overrideAPI { get; set; }

		// Token: 0x06001EDF RID: 7903 RVA: 0x00030B93 File Offset: 0x0002ED93
		protected internal ResourcesAPI()
		{
		}

		// Token: 0x06001EE0 RID: 7904 RVA: 0x0003B5C1 File Offset: 0x000397C1
		protected internal virtual Object[] FindObjectsOfTypeAll(Type systemTypeInstance)
		{
			return ResourcesAPIInternal.FindObjectsOfTypeAll(systemTypeInstance);
		}

		// Token: 0x06001EE1 RID: 7905 RVA: 0x0003B5C9 File Offset: 0x000397C9
		protected internal virtual Shader FindShaderByName(string name)
		{
			return ResourcesAPIInternal.FindShaderByName(name);
		}

		// Token: 0x06001EE2 RID: 7906 RVA: 0x0003B5D1 File Offset: 0x000397D1
		protected internal virtual Object Load(string path, Type systemTypeInstance)
		{
			return ResourcesAPIInternal.Load(path, systemTypeInstance);
		}

		// Token: 0x06001EE3 RID: 7907 RVA: 0x0003B5DA File Offset: 0x000397DA
		protected internal virtual Object[] LoadAll(string path, Type systemTypeInstance)
		{
			return ResourcesAPIInternal.LoadAll(path, systemTypeInstance);
		}

		// Token: 0x06001EE4 RID: 7908 RVA: 0x0003B5E4 File Offset: 0x000397E4
		protected internal virtual ResourceRequest LoadAsync(string path, Type systemTypeInstance)
		{
			ResourceRequest resourceRequest = ResourcesAPIInternal.LoadAsyncInternal(path, systemTypeInstance);
			resourceRequest.m_Path = path;
			resourceRequest.m_Type = systemTypeInstance;
			return resourceRequest;
		}

		// Token: 0x06001EE5 RID: 7909 RVA: 0x0003B60D File Offset: 0x0003980D
		protected internal virtual void UnloadAsset(Object assetToUnload)
		{
			ResourcesAPIInternal.UnloadAsset(assetToUnload);
		}

		// Token: 0x0400091F RID: 2335
		private static ResourcesAPI s_DefaultAPI = new ResourcesAPI();
	}
}
