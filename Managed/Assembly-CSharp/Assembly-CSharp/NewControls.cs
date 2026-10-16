using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

// Token: 0x02000068 RID: 104
public class NewControls : IInputActionCollection2, IInputActionCollection, IEnumerable<InputAction>, IEnumerable, IDisposable
{
	// Token: 0x1700005D RID: 93
	// (get) Token: 0x06000387 RID: 903 RVA: 0x00017859 File Offset: 0x00015A59
	public InputActionAsset asset { get; }

	// Token: 0x06000388 RID: 904 RVA: 0x00017864 File Offset: 0x00015A64
	public NewControls()
	{
		this.asset = InputActionAsset.FromJson("{\r\n    \"version\": 1,\r\n    \"name\": \"New Controls\",\r\n    \"maps\": [\r\n        {\r\n            \"name\": \"UI\",\r\n            \"id\": \"918eb1a2-da56-438b-b107-11deb8d41e91\",\r\n            \"actions\": [\r\n                {\r\n                    \"name\": \"Navigation\",\r\n                    \"type\": \"Value\",\r\n                    \"id\": \"3e21a19d-7259-40f6-b85e-a8d4c587b9d1\",\r\n                    \"expectedControlType\": \"Vector2\",\r\n                    \"processors\": \"\",\r\n                    \"interactions\": \"\",\r\n                    \"initialStateCheck\": true\r\n                },\r\n                {\r\n                    \"name\": \"test\",\r\n                    \"type\": \"Button\",\r\n                    \"id\": \"b302569b-286d-475f-a103-8a50f4845361\",\r\n                    \"expectedControlType\": \"Button\",\r\n                    \"processors\": \"\",\r\n                    \"interactions\": \"\",\r\n                    \"initialStateCheck\": false\r\n                }\r\n            ],\r\n            \"bindings\": [\r\n                {\r\n                    \"name\": \"\",\r\n                    \"id\": \"aff6d164-39e0-481d-bc15-071f4d7471aa\",\r\n                    \"path\": \"<Gamepad>/dpad\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \"\",\r\n                    \"action\": \"Navigation\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": false\r\n                },\r\n                {\r\n                    \"name\": \"2D Vector\",\r\n                    \"id\": \"c3a69459-33b6-48ed-81d4-3ee3c2a5ebc8\",\r\n                    \"path\": \"2DVector\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \"\",\r\n                    \"action\": \"Navigation\",\r\n                    \"isComposite\": true,\r\n                    \"isPartOfComposite\": false\r\n                },\r\n                {\r\n                    \"name\": \"up\",\r\n                    \"id\": \"b5650407-fa16-4599-a3d0-4fda7628d183\",\r\n                    \"path\": \"\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \"\",\r\n                    \"action\": \"Navigation\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": true\r\n                },\r\n                {\r\n                    \"name\": \"down\",\r\n                    \"id\": \"8f57b9f3-6217-402e-b480-0d08e88cbd29\",\r\n                    \"path\": \"\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \"\",\r\n                    \"action\": \"Navigation\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": true\r\n                },\r\n                {\r\n                    \"name\": \"left\",\r\n                    \"id\": \"cb272385-bd67-486b-85c0-5b16d0e3d9a9\",\r\n                    \"path\": \"\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \"\",\r\n                    \"action\": \"Navigation\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": true\r\n                },\r\n                {\r\n                    \"name\": \"right\",\r\n                    \"id\": \"259bac90-0d48-4c03-a376-ccab66fb64ec\",\r\n                    \"path\": \"\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \"\",\r\n                    \"action\": \"Navigation\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": true\r\n                },\r\n                {\r\n                    \"name\": \"\",\r\n                    \"id\": \"20fd9dd6-2ed2-4f67-beb0-9e91adf1c082\",\r\n                    \"path\": \"<Keyboard>/numpad8\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \"\",\r\n                    \"action\": \"test\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": false\r\n                },\r\n                {\r\n                    \"name\": \"Button With One Modifier\",\r\n                    \"id\": \"179b372e-2abb-42e5-93ec-cf1ac10f7512\",\r\n                    \"path\": \"ButtonWithOneModifier\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \"\",\r\n                    \"action\": \"test\",\r\n                    \"isComposite\": true,\r\n                    \"isPartOfComposite\": false\r\n                },\r\n                {\r\n                    \"name\": \"Modifier\",\r\n                    \"id\": \"419607b9-565d-40a8-988e-e9533cf9c0a2\",\r\n                    \"path\": \"<Keyboard>/ctrl\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \"\",\r\n                    \"action\": \"test\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": true\r\n                },\r\n                {\r\n                    \"name\": \"Button\",\r\n                    \"id\": \"b9c10b7d-d672-4cad-b638-c1b9c041a0c4\",\r\n                    \"path\": \"<Keyboard>/w\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \"\",\r\n                    \"action\": \"test\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": true\r\n                },\r\n                {\r\n                    \"name\": \"One Modifier\",\r\n                    \"id\": \"11ec37e0-6dc4-47b0-897e-c31ded5ad80d\",\r\n                    \"path\": \"OneModifier\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \"\",\r\n                    \"action\": \"test\",\r\n                    \"isComposite\": true,\r\n                    \"isPartOfComposite\": false\r\n                },\r\n                {\r\n                    \"name\": \"modifier\",\r\n                    \"id\": \"63d3c4f9-151e-4cdb-9134-ff345368ed59\",\r\n                    \"path\": \"<Keyboard>/shift\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \"\",\r\n                    \"action\": \"test\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": true\r\n                },\r\n                {\r\n                    \"name\": \"binding\",\r\n                    \"id\": \"68c90fc4-218d-48b6-b3a0-de17b0121450\",\r\n                    \"path\": \"\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \"\",\r\n                    \"action\": \"test\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": true\r\n                },\r\n                {\r\n                    \"name\": \"\",\r\n                    \"id\": \"ec371edd-c5be-4ed8-9965-e5f97214408b\",\r\n                    \"path\": \"<Keyboard>/alt\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \"\",\r\n                    \"action\": \"test\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": false\r\n                }\r\n            ]\r\n        }\r\n    ],\r\n    \"controlSchemes\": []\r\n}");
		this.m_UI = this.asset.FindActionMap("UI", true);
		this.m_UI_Navigation = this.m_UI.FindAction("Navigation", true);
		this.m_UI_test = this.m_UI.FindAction("test", true);
	}

	// Token: 0x06000389 RID: 905 RVA: 0x000178D8 File Offset: 0x00015AD8
	~NewControls()
	{
	}

	// Token: 0x0600038A RID: 906 RVA: 0x00017900 File Offset: 0x00015B00
	public void Dispose()
	{
		global::UnityEngine.Object.Destroy(this.asset);
	}

	// Token: 0x1700005E RID: 94
	// (get) Token: 0x0600038B RID: 907 RVA: 0x0001790D File Offset: 0x00015B0D
	// (set) Token: 0x0600038C RID: 908 RVA: 0x0001791A File Offset: 0x00015B1A
	public InputBinding? bindingMask
	{
		get
		{
			return this.asset.bindingMask;
		}
		set
		{
			this.asset.bindingMask = value;
		}
	}

	// Token: 0x1700005F RID: 95
	// (get) Token: 0x0600038D RID: 909 RVA: 0x00017928 File Offset: 0x00015B28
	// (set) Token: 0x0600038E RID: 910 RVA: 0x00017935 File Offset: 0x00015B35
	public ReadOnlyArray<InputDevice>? devices
	{
		get
		{
			return this.asset.devices;
		}
		set
		{
			this.asset.devices = value;
		}
	}

	// Token: 0x17000060 RID: 96
	// (get) Token: 0x0600038F RID: 911 RVA: 0x00017943 File Offset: 0x00015B43
	public ReadOnlyArray<InputControlScheme> controlSchemes
	{
		get
		{
			return this.asset.controlSchemes;
		}
	}

	// Token: 0x06000390 RID: 912 RVA: 0x00017950 File Offset: 0x00015B50
	public bool Contains(InputAction action)
	{
		return this.asset.Contains(action);
	}

	// Token: 0x06000391 RID: 913 RVA: 0x0001795E File Offset: 0x00015B5E
	public IEnumerator<InputAction> GetEnumerator()
	{
		return this.asset.GetEnumerator();
	}

	// Token: 0x06000392 RID: 914 RVA: 0x0001796B File Offset: 0x00015B6B
	IEnumerator IEnumerable.GetEnumerator()
	{
		return this.GetEnumerator();
	}

	// Token: 0x06000393 RID: 915 RVA: 0x00017973 File Offset: 0x00015B73
	public void Enable()
	{
		this.asset.Enable();
	}

	// Token: 0x06000394 RID: 916 RVA: 0x00017980 File Offset: 0x00015B80
	public void Disable()
	{
		this.asset.Disable();
	}

	// Token: 0x17000061 RID: 97
	// (get) Token: 0x06000395 RID: 917 RVA: 0x0001798D File Offset: 0x00015B8D
	public IEnumerable<InputBinding> bindings
	{
		get
		{
			return this.asset.bindings;
		}
	}

	// Token: 0x06000396 RID: 918 RVA: 0x0001799A File Offset: 0x00015B9A
	public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
	{
		return this.asset.FindAction(actionNameOrId, throwIfNotFound);
	}

	// Token: 0x06000397 RID: 919 RVA: 0x000179A9 File Offset: 0x00015BA9
	public int FindBinding(InputBinding bindingMask, out InputAction action)
	{
		return this.asset.FindBinding(bindingMask, out action);
	}

	// Token: 0x17000062 RID: 98
	// (get) Token: 0x06000398 RID: 920 RVA: 0x000179B8 File Offset: 0x00015BB8
	public NewControls.UIActions UI
	{
		get
		{
			return new NewControls.UIActions(this);
		}
	}

	// Token: 0x040002C2 RID: 706
	private readonly InputActionMap m_UI;

	// Token: 0x040002C3 RID: 707
	private List<NewControls.IUIActions> m_UIActionsCallbackInterfaces = new List<NewControls.IUIActions>();

	// Token: 0x040002C4 RID: 708
	private readonly InputAction m_UI_Navigation;

	// Token: 0x040002C5 RID: 709
	private readonly InputAction m_UI_test;

	// Token: 0x02001582 RID: 5506
	public struct UIActions
	{
		// Token: 0x0600D55D RID: 54621 RVA: 0x004F42C6 File Offset: 0x004F24C6
		public UIActions(NewControls wrapper)
		{
			this.m_Wrapper = wrapper;
		}

		// Token: 0x17000CDC RID: 3292
		// (get) Token: 0x0600D55E RID: 54622 RVA: 0x004F42CF File Offset: 0x004F24CF
		public InputAction Navigation
		{
			get
			{
				return this.m_Wrapper.m_UI_Navigation;
			}
		}

		// Token: 0x17000CDD RID: 3293
		// (get) Token: 0x0600D55F RID: 54623 RVA: 0x004F42DC File Offset: 0x004F24DC
		public InputAction test
		{
			get
			{
				return this.m_Wrapper.m_UI_test;
			}
		}

		// Token: 0x0600D560 RID: 54624 RVA: 0x004F42E9 File Offset: 0x004F24E9
		public InputActionMap Get()
		{
			return this.m_Wrapper.m_UI;
		}

		// Token: 0x0600D561 RID: 54625 RVA: 0x004F42F6 File Offset: 0x004F24F6
		public void Enable()
		{
			this.Get().Enable();
		}

		// Token: 0x0600D562 RID: 54626 RVA: 0x004F4303 File Offset: 0x004F2503
		public void Disable()
		{
			this.Get().Disable();
		}

		// Token: 0x17000CDE RID: 3294
		// (get) Token: 0x0600D563 RID: 54627 RVA: 0x004F4310 File Offset: 0x004F2510
		public bool enabled
		{
			get
			{
				return this.Get().enabled;
			}
		}

		// Token: 0x0600D564 RID: 54628 RVA: 0x004F431D File Offset: 0x004F251D
		public static implicit operator InputActionMap(NewControls.UIActions set)
		{
			return set.Get();
		}

		// Token: 0x0600D565 RID: 54629 RVA: 0x004F4328 File Offset: 0x004F2528
		public void AddCallbacks(NewControls.IUIActions instance)
		{
			if (instance == null || this.m_Wrapper.m_UIActionsCallbackInterfaces.Contains(instance))
			{
				return;
			}
			this.m_Wrapper.m_UIActionsCallbackInterfaces.Add(instance);
			this.Navigation.started += instance.OnNavigation;
			this.Navigation.performed += instance.OnNavigation;
			this.Navigation.canceled += instance.OnNavigation;
			this.test.started += instance.OnTest;
			this.test.performed += instance.OnTest;
			this.test.canceled += instance.OnTest;
		}

		// Token: 0x0600D566 RID: 54630 RVA: 0x004F43F0 File Offset: 0x004F25F0
		private void UnregisterCallbacks(NewControls.IUIActions instance)
		{
			this.Navigation.started -= instance.OnNavigation;
			this.Navigation.performed -= instance.OnNavigation;
			this.Navigation.canceled -= instance.OnNavigation;
			this.test.started -= instance.OnTest;
			this.test.performed -= instance.OnTest;
			this.test.canceled -= instance.OnTest;
		}

		// Token: 0x0600D567 RID: 54631 RVA: 0x004F448D File Offset: 0x004F268D
		public void RemoveCallbacks(NewControls.IUIActions instance)
		{
			if (this.m_Wrapper.m_UIActionsCallbackInterfaces.Remove(instance))
			{
				this.UnregisterCallbacks(instance);
			}
		}

		// Token: 0x0600D568 RID: 54632 RVA: 0x004F44AC File Offset: 0x004F26AC
		public void SetCallbacks(NewControls.IUIActions instance)
		{
			foreach (NewControls.IUIActions iuiactions in this.m_Wrapper.m_UIActionsCallbackInterfaces)
			{
				this.UnregisterCallbacks(iuiactions);
			}
			this.m_Wrapper.m_UIActionsCallbackInterfaces.Clear();
			this.AddCallbacks(instance);
		}

		// Token: 0x0400569B RID: 22171
		private NewControls m_Wrapper;
	}

	// Token: 0x02001583 RID: 5507
	public interface IUIActions
	{
		// Token: 0x0600D569 RID: 54633
		void OnNavigation(InputAction.CallbackContext context);

		// Token: 0x0600D56A RID: 54634
		void OnTest(InputAction.CallbackContext context);
	}
}
