using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x0200005C RID: 92
	public class DefaultInputActions : IInputActionCollection2, IInputActionCollection, IEnumerable<InputAction>, IEnumerable, IDisposable
	{
		// Token: 0x1700024D RID: 589
		// (get) Token: 0x0600093E RID: 2366 RVA: 0x0003470E File Offset: 0x0003290E
		public InputActionAsset asset { get; }

		// Token: 0x0600093F RID: 2367 RVA: 0x00034718 File Offset: 0x00032918
		public DefaultInputActions()
		{
			this.asset = InputActionAsset.FromJson("{\n    \"version\": 1,\n    \"name\": \"DefaultInputActions\",\n    \"maps\": [\n        {\n            \"name\": \"Player\",\n            \"id\": \"df70fa95-8a34-4494-b137-73ab6b9c7d37\",\n            \"actions\": [\n                {\n                    \"name\": \"Move\",\n                    \"type\": \"Value\",\n                    \"id\": \"351f2ccd-1f9f-44bf-9bec-d62ac5c5f408\",\n                    \"expectedControlType\": \"Vector2\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"Look\",\n                    \"type\": \"Value\",\n                    \"id\": \"6b444451-8a00-4d00-a97e-f47457f736a8\",\n                    \"expectedControlType\": \"Vector2\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"Fire\",\n                    \"type\": \"Button\",\n                    \"id\": \"6c2ab1b8-8984-453a-af3d-a3c78ae1679a\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                }\n            ],\n            \"bindings\": [\n                {\n                    \"name\": \"\",\n                    \"id\": \"978bfe49-cc26-4a3d-ab7b-7d7a29327403\",\n                    \"path\": \"<Gamepad>/leftStick\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Move\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"WASD\",\n                    \"id\": \"00ca640b-d935-4593-8157-c05846ea39b3\",\n                    \"path\": \"Dpad\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Move\",\n                    \"isComposite\": true,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"up\",\n                    \"id\": \"e2062cb9-1b15-46a2-838c-2f8d72a0bdd9\",\n                    \"path\": \"<Keyboard>/w\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Move\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"up\",\n                    \"id\": \"8180e8bd-4097-4f4e-ab88-4523101a6ce9\",\n                    \"path\": \"<Keyboard>/upArrow\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Move\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"down\",\n                    \"id\": \"320bffee-a40b-4347-ac70-c210eb8bc73a\",\n                    \"path\": \"<Keyboard>/s\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Move\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"down\",\n                    \"id\": \"1c5327b5-f71c-4f60-99c7-4e737386f1d1\",\n                    \"path\": \"<Keyboard>/downArrow\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Move\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"left\",\n                    \"id\": \"d2581a9b-1d11-4566-b27d-b92aff5fabbc\",\n                    \"path\": \"<Keyboard>/a\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Move\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"left\",\n                    \"id\": \"2e46982e-44cc-431b-9f0b-c11910bf467a\",\n                    \"path\": \"<Keyboard>/leftArrow\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Move\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"right\",\n                    \"id\": \"fcfe95b8-67b9-4526-84b5-5d0bc98d6400\",\n                    \"path\": \"<Keyboard>/d\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Move\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"right\",\n                    \"id\": \"77bff152-3580-4b21-b6de-dcd0c7e41164\",\n                    \"path\": \"<Keyboard>/rightArrow\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Move\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"1635d3fe-58b6-4ba9-a4e2-f4b964f6b5c8\",\n                    \"path\": \"<XRController>/{Primary2DAxis}\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"XR\",\n                    \"action\": \"Move\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"3ea4d645-4504-4529-b061-ab81934c3752\",\n                    \"path\": \"<Joystick>/stick\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Joystick\",\n                    \"action\": \"Move\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"c1f7a91b-d0fd-4a62-997e-7fb9b69bf235\",\n                    \"path\": \"<Gamepad>/rightStick\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Look\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"8c8e490b-c610-4785-884f-f04217b23ca4\",\n                    \"path\": \"<Pointer>/delta\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse;Touch\",\n                    \"action\": \"Look\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"3e5f5442-8668-4b27-a940-df99bad7e831\",\n                    \"path\": \"<Joystick>/{Hatswitch}\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Joystick\",\n                    \"action\": \"Look\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"143bb1cd-cc10-4eca-a2f0-a3664166fe91\",\n                    \"path\": \"<Gamepad>/rightTrigger\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Fire\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"05f6913d-c316-48b2-a6bb-e225f14c7960\",\n                    \"path\": \"<Mouse>/leftButton\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Fire\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"886e731e-7071-4ae4-95c0-e61739dad6fd\",\n                    \"path\": \"<Touchscreen>/primaryTouch/tap\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Touch\",\n                    \"action\": \"Fire\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"ee3d0cd2-254e-47a7-a8cb-bc94d9658c54\",\n                    \"path\": \"<Joystick>/trigger\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Joystick\",\n                    \"action\": \"Fire\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"8255d333-5683-4943-a58a-ccb207ff1dce\",\n                    \"path\": \"<XRController>/{PrimaryAction}\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"XR\",\n                    \"action\": \"Fire\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                }\n            ]\n        },\n        {\n            \"name\": \"UI\",\n            \"id\": \"272f6d14-89ba-496f-b7ff-215263d3219f\",\n            \"actions\": [\n                {\n                    \"name\": \"Navigate\",\n                    \"type\": \"PassThrough\",\n                    \"id\": \"c95b2375-e6d9-4b88-9c4c-c5e76515df4b\",\n                    \"expectedControlType\": \"Vector2\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Submit\",\n                    \"type\": \"Button\",\n                    \"id\": \"7607c7b6-cd76-4816-beef-bd0341cfe950\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Cancel\",\n                    \"type\": \"Button\",\n                    \"id\": \"15cef263-9014-4fd5-94d9-4e4a6234a6ef\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Point\",\n                    \"type\": \"PassThrough\",\n                    \"id\": \"32b35790-4ed0-4e9a-aa41-69ac6d629449\",\n                    \"expectedControlType\": \"Vector2\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"Click\",\n                    \"type\": \"PassThrough\",\n                    \"id\": \"3c7022bf-7922-4f7c-a998-c437916075ad\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"ScrollWheel\",\n                    \"type\": \"PassThrough\",\n                    \"id\": \"0489e84a-4833-4c40-bfae-cea84b696689\",\n                    \"expectedControlType\": \"Vector2\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"MiddleClick\",\n                    \"type\": \"PassThrough\",\n                    \"id\": \"dad70c86-b58c-4b17-88ad-f5e53adf419e\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"RightClick\",\n                    \"type\": \"PassThrough\",\n                    \"id\": \"44b200b1-1557-4083-816c-b22cbdf77ddf\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"TrackedDevicePosition\",\n                    \"type\": \"PassThrough\",\n                    \"id\": \"24908448-c609-4bc3-a128-ea258674378a\",\n                    \"expectedControlType\": \"Vector3\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"TrackedDeviceOrientation\",\n                    \"type\": \"PassThrough\",\n                    \"id\": \"9caa3d8a-6b2f-4e8e-8bad-6ede561bd9be\",\n                    \"expectedControlType\": \"Quaternion\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                }\n            ],\n            \"bindings\": [\n                {\n                    \"name\": \"Gamepad\",\n                    \"id\": \"809f371f-c5e2-4e7a-83a1-d867598f40dd\",\n                    \"path\": \"2DVector\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": true,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"up\",\n                    \"id\": \"14a5d6e8-4aaf-4119-a9ef-34b8c2c548bf\",\n                    \"path\": \"<Gamepad>/leftStick/up\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"up\",\n                    \"id\": \"9144cbe6-05e1-4687-a6d7-24f99d23dd81\",\n                    \"path\": \"<Gamepad>/rightStick/up\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"down\",\n                    \"id\": \"2db08d65-c5fb-421b-983f-c71163608d67\",\n                    \"path\": \"<Gamepad>/leftStick/down\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"down\",\n                    \"id\": \"58748904-2ea9-4a80-8579-b500e6a76df8\",\n                    \"path\": \"<Gamepad>/rightStick/down\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"left\",\n                    \"id\": \"8ba04515-75aa-45de-966d-393d9bbd1c14\",\n                    \"path\": \"<Gamepad>/leftStick/left\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"left\",\n                    \"id\": \"712e721c-bdfb-4b23-a86c-a0d9fcfea921\",\n                    \"path\": \"<Gamepad>/rightStick/left\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"right\",\n                    \"id\": \"fcd248ae-a788-4676-a12e-f4d81205600b\",\n                    \"path\": \"<Gamepad>/leftStick/right\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"right\",\n                    \"id\": \"1f04d9bc-c50b-41a1-bfcc-afb75475ec20\",\n                    \"path\": \"<Gamepad>/rightStick/right\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"fb8277d4-c5cd-4663-9dc7-ee3f0b506d90\",\n                    \"path\": \"<Gamepad>/dpad\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"Joystick\",\n                    \"id\": \"e25d9774-381c-4a61-b47c-7b6b299ad9f9\",\n                    \"path\": \"2DVector\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": true,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"up\",\n                    \"id\": \"3db53b26-6601-41be-9887-63ac74e79d19\",\n                    \"path\": \"<Joystick>/stick/up\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Joystick\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"down\",\n                    \"id\": \"0cb3e13e-3d90-4178-8ae6-d9c5501d653f\",\n                    \"path\": \"<Joystick>/stick/down\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Joystick\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"left\",\n                    \"id\": \"0392d399-f6dd-4c82-8062-c1e9c0d34835\",\n                    \"path\": \"<Joystick>/stick/left\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Joystick\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfCo[...string is too long...]");
			this.m_Player = this.asset.FindActionMap("Player", true);
			this.m_Player_Move = this.m_Player.FindAction("Move", true);
			this.m_Player_Look = this.m_Player.FindAction("Look", true);
			this.m_Player_Fire = this.m_Player.FindAction("Fire", true);
			this.m_UI = this.asset.FindActionMap("UI", true);
			this.m_UI_Navigate = this.m_UI.FindAction("Navigate", true);
			this.m_UI_Submit = this.m_UI.FindAction("Submit", true);
			this.m_UI_Cancel = this.m_UI.FindAction("Cancel", true);
			this.m_UI_Point = this.m_UI.FindAction("Point", true);
			this.m_UI_Click = this.m_UI.FindAction("Click", true);
			this.m_UI_ScrollWheel = this.m_UI.FindAction("ScrollWheel", true);
			this.m_UI_MiddleClick = this.m_UI.FindAction("MiddleClick", true);
			this.m_UI_RightClick = this.m_UI.FindAction("RightClick", true);
			this.m_UI_TrackedDevicePosition = this.m_UI.FindAction("TrackedDevicePosition", true);
			this.m_UI_TrackedDeviceOrientation = this.m_UI.FindAction("TrackedDeviceOrientation", true);
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x000348B7 File Offset: 0x00032AB7
		public void Dispose()
		{
			Object.Destroy(this.asset);
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000941 RID: 2369 RVA: 0x000348C4 File Offset: 0x00032AC4
		// (set) Token: 0x06000942 RID: 2370 RVA: 0x000348D1 File Offset: 0x00032AD1
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

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000943 RID: 2371 RVA: 0x000348DF File Offset: 0x00032ADF
		// (set) Token: 0x06000944 RID: 2372 RVA: 0x000348EC File Offset: 0x00032AEC
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

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000945 RID: 2373 RVA: 0x000348FA File Offset: 0x00032AFA
		public ReadOnlyArray<InputControlScheme> controlSchemes
		{
			get
			{
				return this.asset.controlSchemes;
			}
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x00034907 File Offset: 0x00032B07
		public bool Contains(InputAction action)
		{
			return this.asset.Contains(action);
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x00034915 File Offset: 0x00032B15
		public IEnumerator<InputAction> GetEnumerator()
		{
			return this.asset.GetEnumerator();
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x00034922 File Offset: 0x00032B22
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x0003492A File Offset: 0x00032B2A
		public void Enable()
		{
			this.asset.Enable();
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x00034937 File Offset: 0x00032B37
		public void Disable()
		{
			this.asset.Disable();
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x0600094B RID: 2379 RVA: 0x00034944 File Offset: 0x00032B44
		public IEnumerable<InputBinding> bindings
		{
			get
			{
				return this.asset.bindings;
			}
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x00034951 File Offset: 0x00032B51
		public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
		{
			return this.asset.FindAction(actionNameOrId, throwIfNotFound);
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x00034960 File Offset: 0x00032B60
		public int FindBinding(InputBinding bindingMask, out InputAction action)
		{
			return this.asset.FindBinding(bindingMask, out action);
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x0600094E RID: 2382 RVA: 0x0003496F File Offset: 0x00032B6F
		public DefaultInputActions.PlayerActions Player
		{
			get
			{
				return new DefaultInputActions.PlayerActions(this);
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x0600094F RID: 2383 RVA: 0x00034977 File Offset: 0x00032B77
		public DefaultInputActions.UIActions UI
		{
			get
			{
				return new DefaultInputActions.UIActions(this);
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000950 RID: 2384 RVA: 0x00034980 File Offset: 0x00032B80
		public InputControlScheme KeyboardMouseScheme
		{
			get
			{
				if (this.m_KeyboardMouseSchemeIndex == -1)
				{
					this.m_KeyboardMouseSchemeIndex = this.asset.FindControlSchemeIndex("Keyboard&Mouse");
				}
				return this.asset.controlSchemes[this.m_KeyboardMouseSchemeIndex];
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000951 RID: 2385 RVA: 0x000349C8 File Offset: 0x00032BC8
		public InputControlScheme GamepadScheme
		{
			get
			{
				if (this.m_GamepadSchemeIndex == -1)
				{
					this.m_GamepadSchemeIndex = this.asset.FindControlSchemeIndex("Gamepad");
				}
				return this.asset.controlSchemes[this.m_GamepadSchemeIndex];
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000952 RID: 2386 RVA: 0x00034A10 File Offset: 0x00032C10
		public InputControlScheme TouchScheme
		{
			get
			{
				if (this.m_TouchSchemeIndex == -1)
				{
					this.m_TouchSchemeIndex = this.asset.FindControlSchemeIndex("Touch");
				}
				return this.asset.controlSchemes[this.m_TouchSchemeIndex];
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000953 RID: 2387 RVA: 0x00034A58 File Offset: 0x00032C58
		public InputControlScheme JoystickScheme
		{
			get
			{
				if (this.m_JoystickSchemeIndex == -1)
				{
					this.m_JoystickSchemeIndex = this.asset.FindControlSchemeIndex("Joystick");
				}
				return this.asset.controlSchemes[this.m_JoystickSchemeIndex];
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000954 RID: 2388 RVA: 0x00034AA0 File Offset: 0x00032CA0
		public InputControlScheme XRScheme
		{
			get
			{
				if (this.m_XRSchemeIndex == -1)
				{
					this.m_XRSchemeIndex = this.asset.FindControlSchemeIndex("XR");
				}
				return this.asset.controlSchemes[this.m_XRSchemeIndex];
			}
		}

		// Token: 0x040002DE RID: 734
		private readonly InputActionMap m_Player;

		// Token: 0x040002DF RID: 735
		private DefaultInputActions.IPlayerActions m_PlayerActionsCallbackInterface;

		// Token: 0x040002E0 RID: 736
		private readonly InputAction m_Player_Move;

		// Token: 0x040002E1 RID: 737
		private readonly InputAction m_Player_Look;

		// Token: 0x040002E2 RID: 738
		private readonly InputAction m_Player_Fire;

		// Token: 0x040002E3 RID: 739
		private readonly InputActionMap m_UI;

		// Token: 0x040002E4 RID: 740
		private DefaultInputActions.IUIActions m_UIActionsCallbackInterface;

		// Token: 0x040002E5 RID: 741
		private readonly InputAction m_UI_Navigate;

		// Token: 0x040002E6 RID: 742
		private readonly InputAction m_UI_Submit;

		// Token: 0x040002E7 RID: 743
		private readonly InputAction m_UI_Cancel;

		// Token: 0x040002E8 RID: 744
		private readonly InputAction m_UI_Point;

		// Token: 0x040002E9 RID: 745
		private readonly InputAction m_UI_Click;

		// Token: 0x040002EA RID: 746
		private readonly InputAction m_UI_ScrollWheel;

		// Token: 0x040002EB RID: 747
		private readonly InputAction m_UI_MiddleClick;

		// Token: 0x040002EC RID: 748
		private readonly InputAction m_UI_RightClick;

		// Token: 0x040002ED RID: 749
		private readonly InputAction m_UI_TrackedDevicePosition;

		// Token: 0x040002EE RID: 750
		private readonly InputAction m_UI_TrackedDeviceOrientation;

		// Token: 0x040002EF RID: 751
		private int m_KeyboardMouseSchemeIndex = -1;

		// Token: 0x040002F0 RID: 752
		private int m_GamepadSchemeIndex = -1;

		// Token: 0x040002F1 RID: 753
		private int m_TouchSchemeIndex = -1;

		// Token: 0x040002F2 RID: 754
		private int m_JoystickSchemeIndex = -1;

		// Token: 0x040002F3 RID: 755
		private int m_XRSchemeIndex = -1;

		// Token: 0x020001BB RID: 443
		public struct PlayerActions
		{
			// Token: 0x0600146F RID: 5231 RVA: 0x0005F2FB File Offset: 0x0005D4FB
			public PlayerActions(DefaultInputActions wrapper)
			{
				this.m_Wrapper = wrapper;
			}

			// Token: 0x17000580 RID: 1408
			// (get) Token: 0x06001470 RID: 5232 RVA: 0x0005F304 File Offset: 0x0005D504
			public InputAction Move
			{
				get
				{
					return this.m_Wrapper.m_Player_Move;
				}
			}

			// Token: 0x17000581 RID: 1409
			// (get) Token: 0x06001471 RID: 5233 RVA: 0x0005F311 File Offset: 0x0005D511
			public InputAction Look
			{
				get
				{
					return this.m_Wrapper.m_Player_Look;
				}
			}

			// Token: 0x17000582 RID: 1410
			// (get) Token: 0x06001472 RID: 5234 RVA: 0x0005F31E File Offset: 0x0005D51E
			public InputAction Fire
			{
				get
				{
					return this.m_Wrapper.m_Player_Fire;
				}
			}

			// Token: 0x06001473 RID: 5235 RVA: 0x0005F32B File Offset: 0x0005D52B
			public InputActionMap Get()
			{
				return this.m_Wrapper.m_Player;
			}

			// Token: 0x06001474 RID: 5236 RVA: 0x0005F338 File Offset: 0x0005D538
			public void Enable()
			{
				this.Get().Enable();
			}

			// Token: 0x06001475 RID: 5237 RVA: 0x0005F345 File Offset: 0x0005D545
			public void Disable()
			{
				this.Get().Disable();
			}

			// Token: 0x17000583 RID: 1411
			// (get) Token: 0x06001476 RID: 5238 RVA: 0x0005F352 File Offset: 0x0005D552
			public bool enabled
			{
				get
				{
					return this.Get().enabled;
				}
			}

			// Token: 0x06001477 RID: 5239 RVA: 0x0005F35F File Offset: 0x0005D55F
			public static implicit operator InputActionMap(DefaultInputActions.PlayerActions set)
			{
				return set.Get();
			}

			// Token: 0x06001478 RID: 5240 RVA: 0x0005F368 File Offset: 0x0005D568
			public void SetCallbacks(DefaultInputActions.IPlayerActions instance)
			{
				if (this.m_Wrapper.m_PlayerActionsCallbackInterface != null)
				{
					this.Move.started -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
					this.Move.performed -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
					this.Move.canceled -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
					this.Look.started -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
					this.Look.performed -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
					this.Look.canceled -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
					this.Fire.started -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnFire;
					this.Fire.performed -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnFire;
					this.Fire.canceled -= this.m_Wrapper.m_PlayerActionsCallbackInterface.OnFire;
				}
				this.m_Wrapper.m_PlayerActionsCallbackInterface = instance;
				if (instance != null)
				{
					this.Move.started += instance.OnMove;
					this.Move.performed += instance.OnMove;
					this.Move.canceled += instance.OnMove;
					this.Look.started += instance.OnLook;
					this.Look.performed += instance.OnLook;
					this.Look.canceled += instance.OnLook;
					this.Fire.started += instance.OnFire;
					this.Fire.performed += instance.OnFire;
					this.Fire.canceled += instance.OnFire;
				}
			}

			// Token: 0x04000962 RID: 2402
			private DefaultInputActions m_Wrapper;
		}

		// Token: 0x020001BC RID: 444
		public struct UIActions
		{
			// Token: 0x06001479 RID: 5241 RVA: 0x0005F5A1 File Offset: 0x0005D7A1
			public UIActions(DefaultInputActions wrapper)
			{
				this.m_Wrapper = wrapper;
			}

			// Token: 0x17000584 RID: 1412
			// (get) Token: 0x0600147A RID: 5242 RVA: 0x0005F5AA File Offset: 0x0005D7AA
			public InputAction Navigate
			{
				get
				{
					return this.m_Wrapper.m_UI_Navigate;
				}
			}

			// Token: 0x17000585 RID: 1413
			// (get) Token: 0x0600147B RID: 5243 RVA: 0x0005F5B7 File Offset: 0x0005D7B7
			public InputAction Submit
			{
				get
				{
					return this.m_Wrapper.m_UI_Submit;
				}
			}

			// Token: 0x17000586 RID: 1414
			// (get) Token: 0x0600147C RID: 5244 RVA: 0x0005F5C4 File Offset: 0x0005D7C4
			public InputAction Cancel
			{
				get
				{
					return this.m_Wrapper.m_UI_Cancel;
				}
			}

			// Token: 0x17000587 RID: 1415
			// (get) Token: 0x0600147D RID: 5245 RVA: 0x0005F5D1 File Offset: 0x0005D7D1
			public InputAction Point
			{
				get
				{
					return this.m_Wrapper.m_UI_Point;
				}
			}

			// Token: 0x17000588 RID: 1416
			// (get) Token: 0x0600147E RID: 5246 RVA: 0x0005F5DE File Offset: 0x0005D7DE
			public InputAction Click
			{
				get
				{
					return this.m_Wrapper.m_UI_Click;
				}
			}

			// Token: 0x17000589 RID: 1417
			// (get) Token: 0x0600147F RID: 5247 RVA: 0x0005F5EB File Offset: 0x0005D7EB
			public InputAction ScrollWheel
			{
				get
				{
					return this.m_Wrapper.m_UI_ScrollWheel;
				}
			}

			// Token: 0x1700058A RID: 1418
			// (get) Token: 0x06001480 RID: 5248 RVA: 0x0005F5F8 File Offset: 0x0005D7F8
			public InputAction MiddleClick
			{
				get
				{
					return this.m_Wrapper.m_UI_MiddleClick;
				}
			}

			// Token: 0x1700058B RID: 1419
			// (get) Token: 0x06001481 RID: 5249 RVA: 0x0005F605 File Offset: 0x0005D805
			public InputAction RightClick
			{
				get
				{
					return this.m_Wrapper.m_UI_RightClick;
				}
			}

			// Token: 0x1700058C RID: 1420
			// (get) Token: 0x06001482 RID: 5250 RVA: 0x0005F612 File Offset: 0x0005D812
			public InputAction TrackedDevicePosition
			{
				get
				{
					return this.m_Wrapper.m_UI_TrackedDevicePosition;
				}
			}

			// Token: 0x1700058D RID: 1421
			// (get) Token: 0x06001483 RID: 5251 RVA: 0x0005F61F File Offset: 0x0005D81F
			public InputAction TrackedDeviceOrientation
			{
				get
				{
					return this.m_Wrapper.m_UI_TrackedDeviceOrientation;
				}
			}

			// Token: 0x06001484 RID: 5252 RVA: 0x0005F62C File Offset: 0x0005D82C
			public InputActionMap Get()
			{
				return this.m_Wrapper.m_UI;
			}

			// Token: 0x06001485 RID: 5253 RVA: 0x0005F639 File Offset: 0x0005D839
			public void Enable()
			{
				this.Get().Enable();
			}

			// Token: 0x06001486 RID: 5254 RVA: 0x0005F646 File Offset: 0x0005D846
			public void Disable()
			{
				this.Get().Disable();
			}

			// Token: 0x1700058E RID: 1422
			// (get) Token: 0x06001487 RID: 5255 RVA: 0x0005F653 File Offset: 0x0005D853
			public bool enabled
			{
				get
				{
					return this.Get().enabled;
				}
			}

			// Token: 0x06001488 RID: 5256 RVA: 0x0005F660 File Offset: 0x0005D860
			public static implicit operator InputActionMap(DefaultInputActions.UIActions set)
			{
				return set.Get();
			}

			// Token: 0x06001489 RID: 5257 RVA: 0x0005F66C File Offset: 0x0005D86C
			public void SetCallbacks(DefaultInputActions.IUIActions instance)
			{
				if (this.m_Wrapper.m_UIActionsCallbackInterface != null)
				{
					this.Navigate.started -= this.m_Wrapper.m_UIActionsCallbackInterface.OnNavigate;
					this.Navigate.performed -= this.m_Wrapper.m_UIActionsCallbackInterface.OnNavigate;
					this.Navigate.canceled -= this.m_Wrapper.m_UIActionsCallbackInterface.OnNavigate;
					this.Submit.started -= this.m_Wrapper.m_UIActionsCallbackInterface.OnSubmit;
					this.Submit.performed -= this.m_Wrapper.m_UIActionsCallbackInterface.OnSubmit;
					this.Submit.canceled -= this.m_Wrapper.m_UIActionsCallbackInterface.OnSubmit;
					this.Cancel.started -= this.m_Wrapper.m_UIActionsCallbackInterface.OnCancel;
					this.Cancel.performed -= this.m_Wrapper.m_UIActionsCallbackInterface.OnCancel;
					this.Cancel.canceled -= this.m_Wrapper.m_UIActionsCallbackInterface.OnCancel;
					this.Point.started -= this.m_Wrapper.m_UIActionsCallbackInterface.OnPoint;
					this.Point.performed -= this.m_Wrapper.m_UIActionsCallbackInterface.OnPoint;
					this.Point.canceled -= this.m_Wrapper.m_UIActionsCallbackInterface.OnPoint;
					this.Click.started -= this.m_Wrapper.m_UIActionsCallbackInterface.OnClick;
					this.Click.performed -= this.m_Wrapper.m_UIActionsCallbackInterface.OnClick;
					this.Click.canceled -= this.m_Wrapper.m_UIActionsCallbackInterface.OnClick;
					this.ScrollWheel.started -= this.m_Wrapper.m_UIActionsCallbackInterface.OnScrollWheel;
					this.ScrollWheel.performed -= this.m_Wrapper.m_UIActionsCallbackInterface.OnScrollWheel;
					this.ScrollWheel.canceled -= this.m_Wrapper.m_UIActionsCallbackInterface.OnScrollWheel;
					this.MiddleClick.started -= this.m_Wrapper.m_UIActionsCallbackInterface.OnMiddleClick;
					this.MiddleClick.performed -= this.m_Wrapper.m_UIActionsCallbackInterface.OnMiddleClick;
					this.MiddleClick.canceled -= this.m_Wrapper.m_UIActionsCallbackInterface.OnMiddleClick;
					this.RightClick.started -= this.m_Wrapper.m_UIActionsCallbackInterface.OnRightClick;
					this.RightClick.performed -= this.m_Wrapper.m_UIActionsCallbackInterface.OnRightClick;
					this.RightClick.canceled -= this.m_Wrapper.m_UIActionsCallbackInterface.OnRightClick;
					this.TrackedDevicePosition.started -= this.m_Wrapper.m_UIActionsCallbackInterface.OnTrackedDevicePosition;
					this.TrackedDevicePosition.performed -= this.m_Wrapper.m_UIActionsCallbackInterface.OnTrackedDevicePosition;
					this.TrackedDevicePosition.canceled -= this.m_Wrapper.m_UIActionsCallbackInterface.OnTrackedDevicePosition;
					this.TrackedDeviceOrientation.started -= this.m_Wrapper.m_UIActionsCallbackInterface.OnTrackedDeviceOrientation;
					this.TrackedDeviceOrientation.performed -= this.m_Wrapper.m_UIActionsCallbackInterface.OnTrackedDeviceOrientation;
					this.TrackedDeviceOrientation.canceled -= this.m_Wrapper.m_UIActionsCallbackInterface.OnTrackedDeviceOrientation;
				}
				this.m_Wrapper.m_UIActionsCallbackInterface = instance;
				if (instance != null)
				{
					this.Navigate.started += instance.OnNavigate;
					this.Navigate.performed += instance.OnNavigate;
					this.Navigate.canceled += instance.OnNavigate;
					this.Submit.started += instance.OnSubmit;
					this.Submit.performed += instance.OnSubmit;
					this.Submit.canceled += instance.OnSubmit;
					this.Cancel.started += instance.OnCancel;
					this.Cancel.performed += instance.OnCancel;
					this.Cancel.canceled += instance.OnCancel;
					this.Point.started += instance.OnPoint;
					this.Point.performed += instance.OnPoint;
					this.Point.canceled += instance.OnPoint;
					this.Click.started += instance.OnClick;
					this.Click.performed += instance.OnClick;
					this.Click.canceled += instance.OnClick;
					this.ScrollWheel.started += instance.OnScrollWheel;
					this.ScrollWheel.performed += instance.OnScrollWheel;
					this.ScrollWheel.canceled += instance.OnScrollWheel;
					this.MiddleClick.started += instance.OnMiddleClick;
					this.MiddleClick.performed += instance.OnMiddleClick;
					this.MiddleClick.canceled += instance.OnMiddleClick;
					this.RightClick.started += instance.OnRightClick;
					this.RightClick.performed += instance.OnRightClick;
					this.RightClick.canceled += instance.OnRightClick;
					this.TrackedDevicePosition.started += instance.OnTrackedDevicePosition;
					this.TrackedDevicePosition.performed += instance.OnTrackedDevicePosition;
					this.TrackedDevicePosition.canceled += instance.OnTrackedDevicePosition;
					this.TrackedDeviceOrientation.started += instance.OnTrackedDeviceOrientation;
					this.TrackedDeviceOrientation.performed += instance.OnTrackedDeviceOrientation;
					this.TrackedDeviceOrientation.canceled += instance.OnTrackedDeviceOrientation;
				}
			}

			// Token: 0x04000963 RID: 2403
			private DefaultInputActions m_Wrapper;
		}

		// Token: 0x020001BD RID: 445
		public interface IPlayerActions
		{
			// Token: 0x0600148A RID: 5258
			void OnMove(InputAction.CallbackContext context);

			// Token: 0x0600148B RID: 5259
			void OnLook(InputAction.CallbackContext context);

			// Token: 0x0600148C RID: 5260
			void OnFire(InputAction.CallbackContext context);
		}

		// Token: 0x020001BE RID: 446
		public interface IUIActions
		{
			// Token: 0x0600148D RID: 5261
			void OnNavigate(InputAction.CallbackContext context);

			// Token: 0x0600148E RID: 5262
			void OnSubmit(InputAction.CallbackContext context);

			// Token: 0x0600148F RID: 5263
			void OnCancel(InputAction.CallbackContext context);

			// Token: 0x06001490 RID: 5264
			void OnPoint(InputAction.CallbackContext context);

			// Token: 0x06001491 RID: 5265
			void OnClick(InputAction.CallbackContext context);

			// Token: 0x06001492 RID: 5266
			void OnScrollWheel(InputAction.CallbackContext context);

			// Token: 0x06001493 RID: 5267
			void OnMiddleClick(InputAction.CallbackContext context);

			// Token: 0x06001494 RID: 5268
			void OnRightClick(InputAction.CallbackContext context);

			// Token: 0x06001495 RID: 5269
			void OnTrackedDevicePosition(InputAction.CallbackContext context);

			// Token: 0x06001496 RID: 5270
			void OnTrackedDeviceOrientation(InputAction.CallbackContext context);
		}
	}
}
