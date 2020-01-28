#include "il2cpp-config.h"

#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif


#include <cstring>
#include <string.h>
#include <stdio.h>
#include <cmath>
#include <limits>
#include <assert.h>
#include <stdint.h>

#include "il2cpp-class-internals.h"
#include "codegen/il2cpp-codegen.h"
#include "il2cpp-object-internals.h"

template <typename R>
struct VirtFuncInvoker0
{
	typedef R (*Func)(void*, const RuntimeMethod*);

	static inline R Invoke (Il2CppMethodSlot slot, RuntimeObject* obj)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_virtual_invoke_data(slot, obj);
		return ((Func)invokeData.methodPtr)(obj, invokeData.method);
	}
};

// Microsoft.MixedReality.Toolkit.BaseCoreSystem
struct BaseCoreSystem_t86E92055CF287B1D86F50C81455BDFA894B12E41;
// Microsoft.MixedReality.Toolkit.BaseMixedRealityProfile
struct BaseMixedRealityProfile_tB4DC16619B37D298D22571CE017070A78EF826E8;
// Microsoft.MixedReality.Toolkit.CameraSystem.MixedRealityCameraSystem
struct MixedRealityCameraSystem_tD88CC45FC5FEB7DE6B6EB7130347FFFC77DA2E1B;
// Microsoft.MixedReality.Toolkit.IMixedRealityServiceRegistrar
struct IMixedRealityServiceRegistrar_t7B6E2AF9599FB6847FE71FC6F5DE9AE0BC8ABB50;
// Microsoft.MixedReality.Toolkit.MixedRealityCameraProfile
struct MixedRealityCameraProfile_t8D764AB8B73D6B7B38310A8EB0CCAD2A1176E258;
// System.Char[]
struct CharU5BU5D_t4CC6ABF0AD71BEC97E3C2F1E9C5677E46D3A75C2;
// System.Collections.Generic.List`1<UnityEngine.GameObject>
struct List_1_t99909CDEDA6D21189884AEA74B1FD99FC9C6A4C0;
// System.String
struct String_t;
// System.Void
struct Void_t22962CB4C05B1D89B55A6E1139F0E87A90987017;
// UnityEngine.Camera
struct Camera_t48B2B9ECB3CE6108A98BF949A1CECF0FE3421F34;
// UnityEngine.Camera/CameraCallback
struct CameraCallback_t8BBB42AA08D7498DFC11F4128117055BC7F0B9D0;
// UnityEngine.Object
struct Object_tAE11E5E46CD5C37C9F3E8950C00CD8B45666A2D0;
// UnityEngine.WaitUntil
struct WaitUntil_t012561515C0E1D3DEA19DB3A05444B020C68E13F;

extern RuntimeClass* Mathf_tFBDE6467D269BFE410605C7D806FD9991D4A89CB_il2cpp_TypeInfo_var;
extern RuntimeClass* MixedRealityCameraProfile_t8D764AB8B73D6B7B38310A8EB0CCAD2A1176E258_il2cpp_TypeInfo_var;
extern RuntimeClass* Object_tAE11E5E46CD5C37C9F3E8950C00CD8B45666A2D0_il2cpp_TypeInfo_var;
extern String_t* _stringLiteral44E408FBB792F45EB849669B80782DAB5C91FDE4;
extern const uint32_t MixedRealityCameraSystem_System_Collections_IEqualityComparer_GetHashCode_mEC4FD9A6C0DE40D3D6F46B223D83453834FDA43D_MetadataUsageId;
extern const uint32_t MixedRealityCameraSystem__ctor_mB323AD4E2CEC744D89F4D53A65E7ACD58E4C00AF_MetadataUsageId;
extern const uint32_t MixedRealityCameraSystem_get_CameraProfile_mA84448436414F49C26F76D56276AE227973C3FDB_MetadataUsageId;



#ifndef U3CMODULEU3E_T99BC8F4FEE0544B08E2E5E3CFE403870195CFCBD_H
#define U3CMODULEU3E_T99BC8F4FEE0544B08E2E5E3CFE403870195CFCBD_H
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// <Module>
struct  U3CModuleU3E_t99BC8F4FEE0544B08E2E5E3CFE403870195CFCBD 
{
public:

public:
};

#ifdef __clang__
#pragma clang diagnostic pop
#endif
#endif // U3CMODULEU3E_T99BC8F4FEE0544B08E2E5E3CFE403870195CFCBD_H
#ifndef RUNTIMEOBJECT_H
#define RUNTIMEOBJECT_H
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// System.Object

#ifdef __clang__
#pragma clang diagnostic pop
#endif
#endif // RUNTIMEOBJECT_H
#ifndef BASESERVICE_T4603D47AD64FBAEF691CE4F2F2A6AF43967F8C10_H
#define BASESERVICE_T4603D47AD64FBAEF691CE4F2F2A6AF43967F8C10_H
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// Microsoft.MixedReality.Toolkit.BaseService
struct  BaseService_t4603D47AD64FBAEF691CE4F2F2A6AF43967F8C10  : public RuntimeObject
{
public:
	// System.String Microsoft.MixedReality.Toolkit.BaseService::<Name>k__BackingField
	String_t* ___U3CNameU3Ek__BackingField_1;
	// System.UInt32 Microsoft.MixedReality.Toolkit.BaseService::<Priority>k__BackingField
	uint32_t ___U3CPriorityU3Ek__BackingField_2;
	// Microsoft.MixedReality.Toolkit.BaseMixedRealityProfile Microsoft.MixedReality.Toolkit.BaseService::<ConfigurationProfile>k__BackingField
	BaseMixedRealityProfile_tB4DC16619B37D298D22571CE017070A78EF826E8 * ___U3CConfigurationProfileU3Ek__BackingField_3;
	// System.Boolean Microsoft.MixedReality.Toolkit.BaseService::disposed
	bool ___disposed_4;

public:
	inline static int32_t get_offset_of_U3CNameU3Ek__BackingField_1() { return static_cast<int32_t>(offsetof(BaseService_t4603D47AD64FBAEF691CE4F2F2A6AF43967F8C10, ___U3CNameU3Ek__BackingField_1)); }
	inline String_t* get_U3CNameU3Ek__BackingField_1() const { return ___U3CNameU3Ek__BackingField_1; }
	inline String_t** get_address_of_U3CNameU3Ek__BackingField_1() { return &___U3CNameU3Ek__BackingField_1; }
	inline void set_U3CNameU3Ek__BackingField_1(String_t* value)
	{
		___U3CNameU3Ek__BackingField_1 = value;
		Il2CppCodeGenWriteBarrier((&___U3CNameU3Ek__BackingField_1), value);
	}

	inline static int32_t get_offset_of_U3CPriorityU3Ek__BackingField_2() { return static_cast<int32_t>(offsetof(BaseService_t4603D47AD64FBAEF691CE4F2F2A6AF43967F8C10, ___U3CPriorityU3Ek__BackingField_2)); }
	inline uint32_t get_U3CPriorityU3Ek__BackingField_2() const { return ___U3CPriorityU3Ek__BackingField_2; }
	inline uint32_t* get_address_of_U3CPriorityU3Ek__BackingField_2() { return &___U3CPriorityU3Ek__BackingField_2; }
	inline void set_U3CPriorityU3Ek__BackingField_2(uint32_t value)
	{
		___U3CPriorityU3Ek__BackingField_2 = value;
	}

	inline static int32_t get_offset_of_U3CConfigurationProfileU3Ek__BackingField_3() { return static_cast<int32_t>(offsetof(BaseService_t4603D47AD64FBAEF691CE4F2F2A6AF43967F8C10, ___U3CConfigurationProfileU3Ek__BackingField_3)); }
	inline BaseMixedRealityProfile_tB4DC16619B37D298D22571CE017070A78EF826E8 * get_U3CConfigurationProfileU3Ek__BackingField_3() const { return ___U3CConfigurationProfileU3Ek__BackingField_3; }
	inline BaseMixedRealityProfile_tB4DC16619B37D298D22571CE017070A78EF826E8 ** get_address_of_U3CConfigurationProfileU3Ek__BackingField_3() { return &___U3CConfigurationProfileU3Ek__BackingField_3; }
	inline void set_U3CConfigurationProfileU3Ek__BackingField_3(BaseMixedRealityProfile_tB4DC16619B37D298D22571CE017070A78EF826E8 * value)
	{
		___U3CConfigurationProfileU3Ek__BackingField_3 = value;
		Il2CppCodeGenWriteBarrier((&___U3CConfigurationProfileU3Ek__BackingField_3), value);
	}

	inline static int32_t get_offset_of_disposed_4() { return static_cast<int32_t>(offsetof(BaseService_t4603D47AD64FBAEF691CE4F2F2A6AF43967F8C10, ___disposed_4)); }
	inline bool get_disposed_4() const { return ___disposed_4; }
	inline bool* get_address_of_disposed_4() { return &___disposed_4; }
	inline void set_disposed_4(bool value)
	{
		___disposed_4 = value;
	}
};

#ifdef __clang__
#pragma clang diagnostic pop
#endif
#endif // BASESERVICE_T4603D47AD64FBAEF691CE4F2F2A6AF43967F8C10_H
struct Il2CppArrayBounds;
#ifndef RUNTIMEARRAY_H
#define RUNTIMEARRAY_H
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// System.Array

#ifdef __clang__
#pragma clang diagnostic pop
#endif
#endif // RUNTIMEARRAY_H
#ifndef STRING_T_H
#define STRING_T_H
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// System.String
struct  String_t  : public RuntimeObject
{
public:
	// System.Int32 System.String::m_stringLength
	int32_t ___m_stringLength_0;
	// System.Char System.String::m_firstChar
	Il2CppChar ___m_firstChar_1;

public:
	inline static int32_t get_offset_of_m_stringLength_0() { return static_cast<int32_t>(offsetof(String_t, ___m_stringLength_0)); }
	inline int32_t get_m_stringLength_0() const { return ___m_stringLength_0; }
	inline int32_t* get_address_of_m_stringLength_0() { return &___m_stringLength_0; }
	inline void set_m_stringLength_0(int32_t value)
	{
		___m_stringLength_0 = value;
	}

	inline static int32_t get_offset_of_m_firstChar_1() { return static_cast<int32_t>(offsetof(String_t, ___m_firstChar_1)); }
	inline Il2CppChar get_m_firstChar_1() const { return ___m_firstChar_1; }
	inline Il2CppChar* get_address_of_m_firstChar_1() { return &___m_firstChar_1; }
	inline void set_m_firstChar_1(Il2CppChar value)
	{
		___m_firstChar_1 = value;
	}
};

struct String_t_StaticFields
{
public:
	// System.String System.String::Empty
	String_t* ___Empty_5;

public:
	inline static int32_t get_offset_of_Empty_5() { return static_cast<int32_t>(offsetof(String_t_StaticFields, ___Empty_5)); }
	inline String_t* get_Empty_5() const { return ___Empty_5; }
	inline String_t** get_address_of_Empty_5() { return &___Empty_5; }
	inline void set_Empty_5(String_t* value)
	{
		___Empty_5 = value;
		Il2CppCodeGenWriteBarrier((&___Empty_5), value);
	}
};

#ifdef __clang__
#pragma clang diagnostic pop
#endif
#endif // STRING_T_H
#ifndef VALUETYPE_T4D0C27076F7C36E76190FB3328E232BCB1CD1FFF_H
#define VALUETYPE_T4D0C27076F7C36E76190FB3328E232BCB1CD1FFF_H
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// System.ValueType
struct  ValueType_t4D0C27076F7C36E76190FB3328E232BCB1CD1FFF  : public RuntimeObject
{
public:

public:
};

#ifdef __clang__
#pragma clang diagnostic pop
#endif
// Native definition for P/Invoke marshalling of System.ValueType
struct ValueType_t4D0C27076F7C36E76190FB3328E232BCB1CD1FFF_marshaled_pinvoke
{
};
// Native definition for COM marshalling of System.ValueType
struct ValueType_t4D0C27076F7C36E76190FB3328E232BCB1CD1FFF_marshaled_com
{
};
#endif // VALUETYPE_T4D0C27076F7C36E76190FB3328E232BCB1CD1FFF_H
#ifndef BASEEVENTSYSTEM_T0D724E08B21A1E822BE73F1F7F29CA92B10AF9D2_H
#define BASEEVENTSYSTEM_T0D724E08B21A1E822BE73F1F7F29CA92B10AF9D2_H
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// Microsoft.MixedReality.Toolkit.BaseEventSystem
struct  BaseEventSystem_t0D724E08B21A1E822BE73F1F7F29CA92B10AF9D2  : public BaseService_t4603D47AD64FBAEF691CE4F2F2A6AF43967F8C10
{
public:
	// UnityEngine.WaitUntil Microsoft.MixedReality.Toolkit.BaseEventSystem::doneExecutingEvents
	WaitUntil_t012561515C0E1D3DEA19DB3A05444B020C68E13F * ___doneExecutingEvents_6;
	// System.Collections.Generic.List`1<UnityEngine.GameObject> Microsoft.MixedReality.Toolkit.BaseEventSystem::<EventListeners>k__BackingField
	List_1_t99909CDEDA6D21189884AEA74B1FD99FC9C6A4C0 * ___U3CEventListenersU3Ek__BackingField_7;

public:
	inline static int32_t get_offset_of_doneExecutingEvents_6() { return static_cast<int32_t>(offsetof(BaseEventSystem_t0D724E08B21A1E822BE73F1F7F29CA92B10AF9D2, ___doneExecutingEvents_6)); }
	inline WaitUntil_t012561515C0E1D3DEA19DB3A05444B020C68E13F * get_doneExecutingEvents_6() const { return ___doneExecutingEvents_6; }
	inline WaitUntil_t012561515C0E1D3DEA19DB3A05444B020C68E13F ** get_address_of_doneExecutingEvents_6() { return &___doneExecutingEvents_6; }
	inline void set_doneExecutingEvents_6(WaitUntil_t012561515C0E1D3DEA19DB3A05444B020C68E13F * value)
	{
		___doneExecutingEvents_6 = value;
		Il2CppCodeGenWriteBarrier((&___doneExecutingEvents_6), value);
	}

	inline static int32_t get_offset_of_U3CEventListenersU3Ek__BackingField_7() { return static_cast<int32_t>(offsetof(BaseEventSystem_t0D724E08B21A1E822BE73F1F7F29CA92B10AF9D2, ___U3CEventListenersU3Ek__BackingField_7)); }
	inline List_1_t99909CDEDA6D21189884AEA74B1FD99FC9C6A4C0 * get_U3CEventListenersU3Ek__BackingField_7() const { return ___U3CEventListenersU3Ek__BackingField_7; }
	inline List_1_t99909CDEDA6D21189884AEA74B1FD99FC9C6A4C0 ** get_address_of_U3CEventListenersU3Ek__BackingField_7() { return &___U3CEventListenersU3Ek__BackingField_7; }
	inline void set_U3CEventListenersU3Ek__BackingField_7(List_1_t99909CDEDA6D21189884AEA74B1FD99FC9C6A4C0 * value)
	{
		___U3CEventListenersU3Ek__BackingField_7 = value;
		Il2CppCodeGenWriteBarrier((&___U3CEventListenersU3Ek__BackingField_7), value);
	}
};

struct BaseEventSystem_t0D724E08B21A1E822BE73F1F7F29CA92B10AF9D2_StaticFields
{
public:
	// System.Int32 Microsoft.MixedReality.Toolkit.BaseEventSystem::eventExecutionDepth
	int32_t ___eventExecutionDepth_5;

public:
	inline static int32_t get_offset_of_eventExecutionDepth_5() { return static_cast<int32_t>(offsetof(BaseEventSystem_t0D724E08B21A1E822BE73F1F7F29CA92B10AF9D2_StaticFields, ___eventExecutionDepth_5)); }
	inline int32_t get_eventExecutionDepth_5() const { return ___eventExecutionDepth_5; }
	inline int32_t* get_address_of_eventExecutionDepth_5() { return &___eventExecutionDepth_5; }
	inline void set_eventExecutionDepth_5(int32_t value)
	{
		___eventExecutionDepth_5 = value;
	}
};

#ifdef __clang__
#pragma clang diagnostic pop
#endif
#endif // BASEEVENTSYSTEM_T0D724E08B21A1E822BE73F1F7F29CA92B10AF9D2_H
#ifndef BOOLEAN_TB53F6830F670160873277339AA58F15CAED4399C_H
#define BOOLEAN_TB53F6830F670160873277339AA58F15CAED4399C_H
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// System.Boolean
struct  Boolean_tB53F6830F670160873277339AA58F15CAED4399C 
{
public:
	// System.Boolean System.Boolean::m_value
	bool ___m_value_0;

public:
	inline static int32_t get_offset_of_m_value_0() { return static_cast<int32_t>(offsetof(Boolean_tB53F6830F670160873277339AA58F15CAED4399C, ___m_value_0)); }
	inline bool get_m_value_0() const { return ___m_value_0; }
	inline bool* get_address_of_m_value_0() { return &___m_value_0; }
	inline void set_m_value_0(bool value)
	{
		___m_value_0 = value;
	}
};

struct Boolean_tB53F6830F670160873277339AA58F15CAED4399C_StaticFields
{
public:
	// System.String System.Boolean::TrueString
	String_t* ___TrueString_5;
	// System.String System.Boolean::FalseString
	String_t* ___FalseString_6;

public:
	inline static int32_t get_offset_of_TrueString_5() { return static_cast<int32_t>(offsetof(Boolean_tB53F6830F670160873277339AA58F15CAED4399C_StaticFields, ___TrueString_5)); }
	inline String_t* get_TrueString_5() const { return ___TrueString_5; }
	inline String_t** get_address_of_TrueString_5() { return &___TrueString_5; }
	inline void set_TrueString_5(String_t* value)
	{
		___TrueString_5 = value;
		Il2CppCodeGenWriteBarrier((&___TrueString_5), value);
	}

	inline static int32_t get_offset_of_FalseString_6() { return static_cast<int32_t>(offsetof(Boolean_tB53F6830F670160873277339AA58F15CAED4399C_StaticFields, ___FalseString_6)); }
	inline String_t* get_FalseString_6() const { return ___FalseString_6; }
	inline String_t** get_address_of_FalseString_6() { return &___FalseString_6; }
	inline void set_FalseString_6(String_t* value)
	{
		___FalseString_6 = value;
		Il2CppCodeGenWriteBarrier((&___FalseString_6), value);
	}
};

#ifdef __clang__
#pragma clang diagnostic pop
#endif
#endif // BOOLEAN_TB53F6830F670160873277339AA58F15CAED4399C_H
#ifndef ENUM_T2AF27C02B8653AE29442467390005ABC74D8F521_H
#define ENUM_T2AF27C02B8653AE29442467390005ABC74D8F521_H
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// System.Enum
struct  Enum_t2AF27C02B8653AE29442467390005ABC74D8F521  : public ValueType_t4D0C27076F7C36E76190FB3328E232BCB1CD1FFF
{
public:

public:
};

struct Enum_t2AF27C02B8653AE29442467390005ABC74D8F521_StaticFields
{
public:
	// System.Char[] System.Enum::enumSeperatorCharArray
	CharU5BU5D_t4CC6ABF0AD71BEC97E3C2F1E9C5677E46D3A75C2* ___enumSeperatorCharArray_0;

public:
	inline static int32_t get_offset_of_enumSeperatorCharArray_0() { return static_cast<int32_t>(offsetof(Enum_t2AF27C02B8653AE29442467390005ABC74D8F521_StaticFields, ___enumSeperatorCharArray_0)); }
	inline CharU5BU5D_t4CC6ABF0AD71BEC97E3C2F1E9C5677E46D3A75C2* get_enumSeperatorCharArray_0() const { return ___enumSeperatorCharArray_0; }
	inline CharU5BU5D_t4CC6ABF0AD71BEC97E3C2F1E9C5677E46D3A75C2** get_address_of_enumSeperatorCharArray_0() { return &___enumSeperatorCharArray_0; }
	inline void set_enumSeperatorCharArray_0(CharU5BU5D_t4CC6ABF0AD71BEC97E3C2F1E9C5677E46D3A75C2* value)
	{
		___enumSeperatorCharArray_0 = value;
		Il2CppCodeGenWriteBarrier((&___enumSeperatorCharArray_0), value);
	}
};

#ifdef __clang__
#pragma clang diagnostic pop
#endif
// Native definition for P/Invoke marshalling of System.Enum
struct Enum_t2AF27C02B8653AE29442467390005ABC74D8F521_marshaled_pinvoke
{
};
// Native definition for COM marshalling of System.Enum
struct Enum_t2AF27C02B8653AE29442467390005ABC74D8F521_marshaled_com
{
};
#endif // ENUM_T2AF27C02B8653AE29442467390005ABC74D8F521_H
#ifndef INT32_T585191389E07734F19F3156FF88FB3EF4800D102_H
#define INT32_T585191389E07734F19F3156FF88FB3EF4800D102_H
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// System.Int32
struct  Int32_t585191389E07734F19F3156FF88FB3EF4800D102 
{
public:
	// System.Int32 System.Int32::m_value
	int32_t ___m_value_0;

public:
	inline static int32_t get_offset_of_m_value_0() { return static_cast<int32_t>(offsetof(Int32_t585191389E07734F19F3156FF88FB3EF4800D102, ___m_value_0)); }
	inline int32_t get_m_value_0() const { return ___m_value_0; }
	inline int32_t* get_address_of_m_value_0() { return &___m_value_0; }
	inline void set_m_value_0(int32_t value)
	{
		___m_value_0 = value;
	}
};

#ifdef __clang__
#pragma clang diagnostic pop
#endif
#endif // INT32_T585191389E07734F19F3156FF88FB3EF4800D102_H
#ifndef INTPTR_T_H
#define INTPTR_T_H
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// System.IntPtr
struct  IntPtr_t 
{
public:
	// System.Void* System.IntPtr::m_value
	void* ___m_value_0;

public:
	inline static int32_t get_offset_of_m_value_0() { return static_cast<int32_t>(offsetof(IntPtr_t, ___m_value_0)); }
	inline void* get_m_value_0() const { return ___m_value_0; }
	inline void** get_address_of_m_value_0() { return &___m_value_0; }
	inline void set_m_value_0(void* value)
	{
		___m_value_0 = value;
	}
};

struct IntPtr_t_StaticFields
{
public:
	// System.IntPtr System.IntPtr::Zero
	intptr_t ___Zero_1;

public:
	inline static int32_t get_offset_of_Zero_1() { return static_cast<int32_t>(offsetof(IntPtr_t_StaticFields, ___Zero_1)); }
	inline intptr_t get_Zero_1() const { return ___Zero_1; }
	inline intptr_t* get_address_of_Zero_1() { return &___Zero_1; }
	inline void set_Zero_1(intptr_t value)
	{
		___Zero_1 = value;
	}
};

#ifdef __clang__
#pragma clang diagnostic pop
#endif
#endif // INTPTR_T_H
#ifndef SINGLE_TDDDA9169C4E4E308AC6D7A824F9B28DC82204AE1_H
#define SINGLE_TDDDA9169C4E4E308AC6D7A824F9B28DC82204AE1_H
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// System.Single
struct  Single_tDDDA9169C4E4E308AC6D7A824F9B28DC82204AE1 
{
public:
	// System.Single System.Single::m_value
	float ___m_value_0;

public:
	inline static int32_t get_offset_of_m_value_0() { return static_cast<int32_t>(offsetof(Single_tDDDA9169C4E4E308AC6D7A824F9B28DC82204AE1, ___m_value_0)); }
	inline float get_m_value_0() const { return ___m_value_0; }
	inline float* get_address_of_m_value_0() { return &___m_value_0; }
	inline void set_m_value_0(float value)
	{
		___m_value_0 = value;
	}
};

#ifdef __clang__
#pragma clang diagnostic pop
#endif
#endif // SINGLE_TDDDA9169C4E4E308AC6D7A824F9B28DC82204AE1_H
#ifndef UINT32_T4980FA09003AFAAB5A6E361BA2748EA9A005709B_H
#define UINT32_T4980FA09003AFAAB5A6E361BA2748EA9A005709B_H
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// System.UInt32
struct  UInt32_t4980FA09003AFAAB5A6E361BA2748EA9A005709B 
{
public:
	// System.UInt32 System.UInt32::m_value
	uint32_t ___m_value_0;

public:
	inline static int32_t get_offset_of_m_value_0() { return static_cast<int32_t>(offsetof(UInt32_t4980FA09003AFAAB5A6E361BA2748EA9A005709B, ___m_value_0)); }
	inline uint32_t get_m_value_0() const { return ___m_value_0; }
	inline uint32_t* get_address_of_m_value_0() { return &___m_value_0; }
	inline void set_m_value_0(uint32_t value)
	{
		___m_value_0 = value;
	}
};

#ifdef __clang__
#pragma clang diagnostic pop
#endif
#endif // UINT32_T4980FA09003AFAAB5A6E361BA2748EA9A005709B_H
#ifndef VOID_T22962CB4C05B1D89B55A6E1139F0E87A90987017_H
#define VOID_T22962CB4C05B1D89B55A6E1139F0E87A90987017_H
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// System.Void
struct  Void_t22962CB4C05B1D89B55A6E1139F0E87A90987017 
{
public:
	union
	{
		struct
		{
		};
		uint8_t Void_t22962CB4C05B1D89B55A6E1139F0E87A90987017__padding[1];
	};

public:
};

#ifdef __clang__
#pragma clang diagnostic pop
#endif
#endif // VOID_T22962CB4C05B1D89B55A6E1139F0E87A90987017_H
#ifndef COLOR_T119BCA590009762C7223FDD3AF9706653AC84ED2_H
#define COLOR_T119BCA590009762C7223FDD3AF9706653AC84ED2_H
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// UnityEngine.Color
struct  Color_t119BCA590009762C7223FDD3AF9706653AC84ED2 
{
public:
	// System.Single UnityEngine.Color::r
	float ___r_0;
	// System.Single UnityEngine.Color::g
	float ___g_1;
	// System.Single UnityEngine.Color::b
	float ___b_2;
	// System.Single UnityEngine.Color::a
	float ___a_3;

public:
	inline static int32_t get_offset_of_r_0() { return static_cast<int32_t>(offsetof(Color_t119BCA590009762C7223FDD3AF9706653AC84ED2, ___r_0)); }
	inline float get_r_0() const { return ___r_0; }
	inline float* get_address_of_r_0() { return &___r_0; }
	inline void set_r_0(float value)
	{
		___r_0 = value;
	}

	inline static int32_t get_offset_of_g_1() { return static_cast<int32_t>(offsetof(Color_t119BCA590009762C7223FDD3AF9706653AC84ED2, ___g_1)); }
	inline float get_g_1() const { return ___g_1; }
	inline float* get_address_of_g_1() { return &___g_1; }
	inline void set_g_1(float value)
	{
		___g_1 = value;
	}

	inline static int32_t get_offset_of_b_2() { return static_cast<int32_t>(offsetof(Color_t119BCA590009762C7223FDD3AF9706653AC84ED2, ___b_2)); }
	inline float get_b_2() const { return ___b_2; }
	inline float* get_address_of_b_2() { return &___b_2; }
	inline void set_b_2(float value)
	{
		___b_2 = value;
	}

	inline static int32_t get_offset_of_a_3() { return static_cast<int32_t>(offsetof(Color_t119BCA590009762C7223FDD3AF9706653AC84ED2, ___a_3)); }
	inline float get_a_3() const { return ___a_3; }
	inline float* get_address_of_a_3() { return &___a_3; }
	inline void set_a_3(float value)
	{
		___a_3 = value;
	}
};

#ifdef __clang__
#pragma clang diagnostic pop
#endif
#endif // COLOR_T119BCA590009762C7223FDD3AF9706653AC84ED2_H
#ifndef BASECORESYSTEM_T86E92055CF287B1D86F50C81455BDFA894B12E41_H
#define BASECORESYSTEM_T86E92055CF287B1D86F50C81455BDFA894B12E41_H
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// Microsoft.MixedReality.Toolkit.BaseCoreSystem
struct  BaseCoreSystem_t86E92055CF287B1D86F50C81455BDFA894B12E41  : public BaseEventSystem_t0D724E08B21A1E822BE73F1F7F29CA92B10AF9D2
{
public:
	// Microsoft.MixedReality.Toolkit.IMixedRealityServiceRegistrar Microsoft.MixedReality.Toolkit.BaseCoreSystem::<Registrar>k__BackingField
	RuntimeObject* ___U3CRegistrarU3Ek__BackingField_8;

public:
	inline static int32_t get_offset_of_U3CRegistrarU3Ek__BackingField_8() { return static_cast<int32_t>(offsetof(BaseCoreSystem_t86E92055CF287B1D86F50C81455BDFA894B12E41, ___U3CRegistrarU3Ek__BackingField_8)); }
	inline RuntimeObject* get_U3CRegistrarU3Ek__BackingField_8() const { return ___U3CRegistrarU3Ek__BackingField_8; }
	inline RuntimeObject** get_address_of_U3CRegistrarU3Ek__BackingField_8() { return &___U3CRegistrarU3Ek__BackingField_8; }
	inline void set_U3CRegistrarU3Ek__BackingField_8(RuntimeObject* value)
	{
		___U3CRegistrarU3Ek__BackingField_8 = value;
		Il2CppCodeGenWriteBarrier((&___U3CRegistrarU3Ek__BackingField_8), value);
	}
};

#ifdef __clang__
#pragma clang diagnostic pop
#endif
#endif // BASECORESYSTEM_T86E92055CF287B1D86F50C81455BDFA894B12E41_H
#ifndef DISPLAYTYPE_T46A4ED5E1AEA75E295FB68F5EB4FFE4194CFAA62_H
#define DISPLAYTYPE_T46A4ED5E1AEA75E295FB68F5EB4FFE4194CFAA62_H
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// Microsoft.MixedReality.Toolkit.CameraSystem.MixedRealityCameraSystem_DisplayType
struct  DisplayType_t46A4ED5E1AEA75E295FB68F5EB4FFE4194CFAA62 
{
public:
	// System.Int32 Microsoft.MixedReality.Toolkit.CameraSystem.MixedRealityCameraSystem_DisplayType::value__
	int32_t ___value___2;

public:
	inline static int32_t get_offset_of_value___2() { return static_cast<int32_t>(offsetof(DisplayType_t46A4ED5E1AEA75E295FB68F5EB4FFE4194CFAA62, ___value___2)); }
	inline int32_t get_value___2() const { return ___value___2; }
	inline int32_t* get_address_of_value___2() { return &___value___2; }
	inline void set_value___2(int32_t value)
	{
		___value___2 = value;
	}
};

#ifdef __clang__
#pragma clang diagnostic pop
#endif
#endif // DISPLAYTYPE_T46A4ED5E1AEA75E295FB68F5EB4FFE4194CFAA62_H
#ifndef CAMERACLEARFLAGS_TAC22BD22D12708CBDC63F6CFB31109E5E17CF239_H
#define CAMERACLEARFLAGS_TAC22BD22D12708CBDC63F6CFB31109E5E17CF239_H
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// UnityEngine.CameraClearFlags
struct  CameraClearFlags_tAC22BD22D12708CBDC63F6CFB31109E5E17CF239 
{
public:
	// System.Int32 UnityEngine.CameraClearFlags::value__
	int32_t ___value___2;

public:
	inline static int32_t get_offset_of_value___2() { return static_cast<int32_t>(offsetof(CameraClearFlags_tAC22BD22D12708CBDC63F6CFB31109E5E17CF239, ___value___2)); }
	inline int32_t get_value___2() const { return ___value___2; }
	inline int32_t* get_address_of_value___2() { return &___value___2; }
	inline void set_value___2(int32_t value)
	{
		___value___2 = value;
	}
};

#ifdef __clang__
#pragma clang diagnostic pop
#endif
#endif // CAMERACLEARFLAGS_TAC22BD22D12708CBDC63F6CFB31109E5E17CF239_H
#ifndef OBJECT_TAE11E5E46CD5C37C9F3E8950C00CD8B45666A2D0_H
#define OBJECT_TAE11E5E46CD5C37C9F3E8950C00CD8B45666A2D0_H
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// UnityEngine.Object
struct  Object_tAE11E5E46CD5C37C9F3E8950C00CD8B45666A2D0  : public RuntimeObject
{
public:
	// System.IntPtr UnityEngine.Object::m_CachedPtr
	intptr_t ___m_CachedPtr_0;

public:
	inline static int32_t get_offset_of_m_CachedPtr_0() { return static_cast<int32_t>(offsetof(Object_tAE11E5E46CD5C37C9F3E8950C00CD8B45666A2D0, ___m_CachedPtr_0)); }
	inline intptr_t get_m_CachedPtr_0() const { return ___m_CachedPtr_0; }
	inline intptr_t* get_address_of_m_CachedPtr_0() { return &___m_CachedPtr_0; }
	inline void set_m_CachedPtr_0(intptr_t value)
	{
		___m_CachedPtr_0 = value;
	}
};

struct Object_tAE11E5E46CD5C37C9F3E8950C00CD8B45666A2D0_StaticFields
{
public:
	// System.Int32 UnityEngine.Object::OffsetOfInstanceIDInCPlusPlusObject
	int32_t ___OffsetOfInstanceIDInCPlusPlusObject_1;

public:
	inline static int32_t get_offset_of_OffsetOfInstanceIDInCPlusPlusObject_1() { return static_cast<int32_t>(offsetof(Object_tAE11E5E46CD5C37C9F3E8950C00CD8B45666A2D0_StaticFields, ___OffsetOfInstanceIDInCPlusPlusObject_1)); }
	inline int32_t get_OffsetOfInstanceIDInCPlusPlusObject_1() const { return ___OffsetOfInstanceIDInCPlusPlusObject_1; }
	inline int32_t* get_address_of_OffsetOfInstanceIDInCPlusPlusObject_1() { return &___OffsetOfInstanceIDInCPlusPlusObject_1; }
	inline void set_OffsetOfInstanceIDInCPlusPlusObject_1(int32_t value)
	{
		___OffsetOfInstanceIDInCPlusPlusObject_1 = value;
	}
};

#ifdef __clang__
#pragma clang diagnostic pop
#endif
// Native definition for P/Invoke marshalling of UnityEngine.Object
struct Object_tAE11E5E46CD5C37C9F3E8950C00CD8B45666A2D0_marshaled_pinvoke
{
	intptr_t ___m_CachedPtr_0;
};
// Native definition for COM marshalling of UnityEngine.Object
struct Object_tAE11E5E46CD5C37C9F3E8950C00CD8B45666A2D0_marshaled_com
{
	intptr_t ___m_CachedPtr_0;
};
#endif // OBJECT_TAE11E5E46CD5C37C9F3E8950C00CD8B45666A2D0_H
#ifndef MIXEDREALITYCAMERASYSTEM_TD88CC45FC5FEB7DE6B6EB7130347FFFC77DA2E1B_H
#define MIXEDREALITYCAMERASYSTEM_TD88CC45FC5FEB7DE6B6EB7130347FFFC77DA2E1B_H
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// Microsoft.MixedReality.Toolkit.CameraSystem.MixedRealityCameraSystem
struct  MixedRealityCameraSystem_tD88CC45FC5FEB7DE6B6EB7130347FFFC77DA2E1B  : public BaseCoreSystem_t86E92055CF287B1D86F50C81455BDFA894B12E41
{
public:
	// System.UInt32 Microsoft.MixedReality.Toolkit.CameraSystem.MixedRealityCameraSystem::<SourceId>k__BackingField
	uint32_t ___U3CSourceIdU3Ek__BackingField_9;
	// System.String Microsoft.MixedReality.Toolkit.CameraSystem.MixedRealityCameraSystem::<SourceName>k__BackingField
	String_t* ___U3CSourceNameU3Ek__BackingField_10;
	// Microsoft.MixedReality.Toolkit.MixedRealityCameraProfile Microsoft.MixedReality.Toolkit.CameraSystem.MixedRealityCameraSystem::cameraProfile
	MixedRealityCameraProfile_t8D764AB8B73D6B7B38310A8EB0CCAD2A1176E258 * ___cameraProfile_11;
	// Microsoft.MixedReality.Toolkit.CameraSystem.MixedRealityCameraSystem_DisplayType Microsoft.MixedReality.Toolkit.CameraSystem.MixedRealityCameraSystem::currentDisplayType
	int32_t ___currentDisplayType_12;
	// System.Boolean Microsoft.MixedReality.Toolkit.CameraSystem.MixedRealityCameraSystem::cameraOpaqueLastFrame
	bool ___cameraOpaqueLastFrame_13;

public:
	inline static int32_t get_offset_of_U3CSourceIdU3Ek__BackingField_9() { return static_cast<int32_t>(offsetof(MixedRealityCameraSystem_tD88CC45FC5FEB7DE6B6EB7130347FFFC77DA2E1B, ___U3CSourceIdU3Ek__BackingField_9)); }
	inline uint32_t get_U3CSourceIdU3Ek__BackingField_9() const { return ___U3CSourceIdU3Ek__BackingField_9; }
	inline uint32_t* get_address_of_U3CSourceIdU3Ek__BackingField_9() { return &___U3CSourceIdU3Ek__BackingField_9; }
	inline void set_U3CSourceIdU3Ek__BackingField_9(uint32_t value)
	{
		___U3CSourceIdU3Ek__BackingField_9 = value;
	}

	inline static int32_t get_offset_of_U3CSourceNameU3Ek__BackingField_10() { return static_cast<int32_t>(offsetof(MixedRealityCameraSystem_tD88CC45FC5FEB7DE6B6EB7130347FFFC77DA2E1B, ___U3CSourceNameU3Ek__BackingField_10)); }
	inline String_t* get_U3CSourceNameU3Ek__BackingField_10() const { return ___U3CSourceNameU3Ek__BackingField_10; }
	inline String_t** get_address_of_U3CSourceNameU3Ek__BackingField_10() { return &___U3CSourceNameU3Ek__BackingField_10; }
	inline void set_U3CSourceNameU3Ek__BackingField_10(String_t* value)
	{
		___U3CSourceNameU3Ek__BackingField_10 = value;
		Il2CppCodeGenWriteBarrier((&___U3CSourceNameU3Ek__BackingField_10), value);
	}

	inline static int32_t get_offset_of_cameraProfile_11() { return static_cast<int32_t>(offsetof(MixedRealityCameraSystem_tD88CC45FC5FEB7DE6B6EB7130347FFFC77DA2E1B, ___cameraProfile_11)); }
	inline MixedRealityCameraProfile_t8D764AB8B73D6B7B38310A8EB0CCAD2A1176E258 * get_cameraProfile_11() const { return ___cameraProfile_11; }
	inline MixedRealityCameraProfile_t8D764AB8B73D6B7B38310A8EB0CCAD2A1176E258 ** get_address_of_cameraProfile_11() { return &___cameraProfile_11; }
	inline void set_cameraProfile_11(MixedRealityCameraProfile_t8D764AB8B73D6B7B38310A8EB0CCAD2A1176E258 * value)
	{
		___cameraProfile_11 = value;
		Il2CppCodeGenWriteBarrier((&___cameraProfile_11), value);
	}

	inline static int32_t get_offset_of_currentDisplayType_12() { return static_cast<int32_t>(offsetof(MixedRealityCameraSystem_tD88CC45FC5FEB7DE6B6EB7130347FFFC77DA2E1B, ___currentDisplayType_12)); }
	inline int32_t get_currentDisplayType_12() const { return ___currentDisplayType_12; }
	inline int32_t* get_address_of_currentDisplayType_12() { return &___currentDisplayType_12; }
	inline void set_currentDisplayType_12(int32_t value)
	{
		___currentDisplayType_12 = value;
	}

	inline static int32_t get_offset_of_cameraOpaqueLastFrame_13() { return static_cast<int32_t>(offsetof(MixedRealityCameraSystem_tD88CC45FC5FEB7DE6B6EB7130347FFFC77DA2E1B, ___cameraOpaqueLastFrame_13)); }
	inline bool get_cameraOpaqueLastFrame_13() const { return ___cameraOpaqueLastFrame_13; }
	inline bool* get_address_of_cameraOpaqueLastFrame_13() { return &___cameraOpaqueLastFrame_13; }
	inline void set_cameraOpaqueLastFrame_13(bool value)
	{
		___cameraOpaqueLastFrame_13 = value;
	}
};

#ifdef __clang__
#pragma clang diagnostic pop
#endif
#endif // MIXEDREALITYCAMERASYSTEM_TD88CC45FC5FEB7DE6B6EB7130347FFFC77DA2E1B_H
#ifndef COMPONENT_T05064EF382ABCAF4B8C94F8A350EA85184C26621_H
#define COMPONENT_T05064EF382ABCAF4B8C94F8A350EA85184C26621_H
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// UnityEngine.Component
struct  Component_t05064EF382ABCAF4B8C94F8A350EA85184C26621  : public Object_tAE11E5E46CD5C37C9F3E8950C00CD8B45666A2D0
{
public:

public:
};

#ifdef __clang__
#pragma clang diagnostic pop
#endif
#endif // COMPONENT_T05064EF382ABCAF4B8C94F8A350EA85184C26621_H
#ifndef SCRIPTABLEOBJECT_TAB015486CEAB714DA0D5C1BA389B84FB90427734_H
#define SCRIPTABLEOBJECT_TAB015486CEAB714DA0D5C1BA389B84FB90427734_H
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// UnityEngine.ScriptableObject
struct  ScriptableObject_tAB015486CEAB714DA0D5C1BA389B84FB90427734  : public Object_tAE11E5E46CD5C37C9F3E8950C00CD8B45666A2D0
{
public:

public:
};

#ifdef __clang__
#pragma clang diagnostic pop
#endif
// Native definition for P/Invoke marshalling of UnityEngine.ScriptableObject
struct ScriptableObject_tAB015486CEAB714DA0D5C1BA389B84FB90427734_marshaled_pinvoke : public Object_tAE11E5E46CD5C37C9F3E8950C00CD8B45666A2D0_marshaled_pinvoke
{
};
// Native definition for COM marshalling of UnityEngine.ScriptableObject
struct ScriptableObject_tAB015486CEAB714DA0D5C1BA389B84FB90427734_marshaled_com : public Object_tAE11E5E46CD5C37C9F3E8950C00CD8B45666A2D0_marshaled_com
{
};
#endif // SCRIPTABLEOBJECT_TAB015486CEAB714DA0D5C1BA389B84FB90427734_H
#ifndef BASEMIXEDREALITYPROFILE_TB4DC16619B37D298D22571CE017070A78EF826E8_H
#define BASEMIXEDREALITYPROFILE_TB4DC16619B37D298D22571CE017070A78EF826E8_H
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// Microsoft.MixedReality.Toolkit.BaseMixedRealityProfile
struct  BaseMixedRealityProfile_tB4DC16619B37D298D22571CE017070A78EF826E8  : public ScriptableObject_tAB015486CEAB714DA0D5C1BA389B84FB90427734
{
public:
	// System.Boolean Microsoft.MixedReality.Toolkit.BaseMixedRealityProfile::isCustomProfile
	bool ___isCustomProfile_4;

public:
	inline static int32_t get_offset_of_isCustomProfile_4() { return static_cast<int32_t>(offsetof(BaseMixedRealityProfile_tB4DC16619B37D298D22571CE017070A78EF826E8, ___isCustomProfile_4)); }
	inline bool get_isCustomProfile_4() const { return ___isCustomProfile_4; }
	inline bool* get_address_of_isCustomProfile_4() { return &___isCustomProfile_4; }
	inline void set_isCustomProfile_4(bool value)
	{
		___isCustomProfile_4 = value;
	}
};

#ifdef __clang__
#pragma clang diagnostic pop
#endif
#endif // BASEMIXEDREALITYPROFILE_TB4DC16619B37D298D22571CE017070A78EF826E8_H
#ifndef BEHAVIOUR_TBDC7E9C3C898AD8348891B82D3E345801D920CA8_H
#define BEHAVIOUR_TBDC7E9C3C898AD8348891B82D3E345801D920CA8_H
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// UnityEngine.Behaviour
struct  Behaviour_tBDC7E9C3C898AD8348891B82D3E345801D920CA8  : public Component_t05064EF382ABCAF4B8C94F8A350EA85184C26621
{
public:

public:
};

#ifdef __clang__
#pragma clang diagnostic pop
#endif
#endif // BEHAVIOUR_TBDC7E9C3C898AD8348891B82D3E345801D920CA8_H
#ifndef MIXEDREALITYCAMERAPROFILE_T8D764AB8B73D6B7B38310A8EB0CCAD2A1176E258_H
#define MIXEDREALITYCAMERAPROFILE_T8D764AB8B73D6B7B38310A8EB0CCAD2A1176E258_H
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// Microsoft.MixedReality.Toolkit.MixedRealityCameraProfile
struct  MixedRealityCameraProfile_t8D764AB8B73D6B7B38310A8EB0CCAD2A1176E258  : public BaseMixedRealityProfile_tB4DC16619B37D298D22571CE017070A78EF826E8
{
public:
	// System.Single Microsoft.MixedReality.Toolkit.MixedRealityCameraProfile::nearClipPlaneOpaqueDisplay
	float ___nearClipPlaneOpaqueDisplay_5;
	// UnityEngine.CameraClearFlags Microsoft.MixedReality.Toolkit.MixedRealityCameraProfile::cameraClearFlagsOpaqueDisplay
	int32_t ___cameraClearFlagsOpaqueDisplay_6;
	// UnityEngine.Color Microsoft.MixedReality.Toolkit.MixedRealityCameraProfile::backgroundColorOpaqueDisplay
	Color_t119BCA590009762C7223FDD3AF9706653AC84ED2  ___backgroundColorOpaqueDisplay_7;
	// System.Int32 Microsoft.MixedReality.Toolkit.MixedRealityCameraProfile::opaqueQualityLevel
	int32_t ___opaqueQualityLevel_8;
	// System.Single Microsoft.MixedReality.Toolkit.MixedRealityCameraProfile::nearClipPlaneTransparentDisplay
	float ___nearClipPlaneTransparentDisplay_9;
	// UnityEngine.CameraClearFlags Microsoft.MixedReality.Toolkit.MixedRealityCameraProfile::cameraClearFlagsTransparentDisplay
	int32_t ___cameraClearFlagsTransparentDisplay_10;
	// UnityEngine.Color Microsoft.MixedReality.Toolkit.MixedRealityCameraProfile::backgroundColorTransparentDisplay
	Color_t119BCA590009762C7223FDD3AF9706653AC84ED2  ___backgroundColorTransparentDisplay_11;
	// System.Int32 Microsoft.MixedReality.Toolkit.MixedRealityCameraProfile::holoLensQualityLevel
	int32_t ___holoLensQualityLevel_12;

public:
	inline static int32_t get_offset_of_nearClipPlaneOpaqueDisplay_5() { return static_cast<int32_t>(offsetof(MixedRealityCameraProfile_t8D764AB8B73D6B7B38310A8EB0CCAD2A1176E258, ___nearClipPlaneOpaqueDisplay_5)); }
	inline float get_nearClipPlaneOpaqueDisplay_5() const { return ___nearClipPlaneOpaqueDisplay_5; }
	inline float* get_address_of_nearClipPlaneOpaqueDisplay_5() { return &___nearClipPlaneOpaqueDisplay_5; }
	inline void set_nearClipPlaneOpaqueDisplay_5(float value)
	{
		___nearClipPlaneOpaqueDisplay_5 = value;
	}

	inline static int32_t get_offset_of_cameraClearFlagsOpaqueDisplay_6() { return static_cast<int32_t>(offsetof(MixedRealityCameraProfile_t8D764AB8B73D6B7B38310A8EB0CCAD2A1176E258, ___cameraClearFlagsOpaqueDisplay_6)); }
	inline int32_t get_cameraClearFlagsOpaqueDisplay_6() const { return ___cameraClearFlagsOpaqueDisplay_6; }
	inline int32_t* get_address_of_cameraClearFlagsOpaqueDisplay_6() { return &___cameraClearFlagsOpaqueDisplay_6; }
	inline void set_cameraClearFlagsOpaqueDisplay_6(int32_t value)
	{
		___cameraClearFlagsOpaqueDisplay_6 = value;
	}

	inline static int32_t get_offset_of_backgroundColorOpaqueDisplay_7() { return static_cast<int32_t>(offsetof(MixedRealityCameraProfile_t8D764AB8B73D6B7B38310A8EB0CCAD2A1176E258, ___backgroundColorOpaqueDisplay_7)); }
	inline Color_t119BCA590009762C7223FDD3AF9706653AC84ED2  get_backgroundColorOpaqueDisplay_7() const { return ___backgroundColorOpaqueDisplay_7; }
	inline Color_t119BCA590009762C7223FDD3AF9706653AC84ED2 * get_address_of_backgroundColorOpaqueDisplay_7() { return &___backgroundColorOpaqueDisplay_7; }
	inline void set_backgroundColorOpaqueDisplay_7(Color_t119BCA590009762C7223FDD3AF9706653AC84ED2  value)
	{
		___backgroundColorOpaqueDisplay_7 = value;
	}

	inline static int32_t get_offset_of_opaqueQualityLevel_8() { return static_cast<int32_t>(offsetof(MixedRealityCameraProfile_t8D764AB8B73D6B7B38310A8EB0CCAD2A1176E258, ___opaqueQualityLevel_8)); }
	inline int32_t get_opaqueQualityLevel_8() const { return ___opaqueQualityLevel_8; }
	inline int32_t* get_address_of_opaqueQualityLevel_8() { return &___opaqueQualityLevel_8; }
	inline void set_opaqueQualityLevel_8(int32_t value)
	{
		___opaqueQualityLevel_8 = value;
	}

	inline static int32_t get_offset_of_nearClipPlaneTransparentDisplay_9() { return static_cast<int32_t>(offsetof(MixedRealityCameraProfile_t8D764AB8B73D6B7B38310A8EB0CCAD2A1176E258, ___nearClipPlaneTransparentDisplay_9)); }
	inline float get_nearClipPlaneTransparentDisplay_9() const { return ___nearClipPlaneTransparentDisplay_9; }
	inline float* get_address_of_nearClipPlaneTransparentDisplay_9() { return &___nearClipPlaneTransparentDisplay_9; }
	inline void set_nearClipPlaneTransparentDisplay_9(float value)
	{
		___nearClipPlaneTransparentDisplay_9 = value;
	}

	inline static int32_t get_offset_of_cameraClearFlagsTransparentDisplay_10() { return static_cast<int32_t>(offsetof(MixedRealityCameraProfile_t8D764AB8B73D6B7B38310A8EB0CCAD2A1176E258, ___cameraClearFlagsTransparentDisplay_10)); }
	inline int32_t get_cameraClearFlagsTransparentDisplay_10() const { return ___cameraClearFlagsTransparentDisplay_10; }
	inline int32_t* get_address_of_cameraClearFlagsTransparentDisplay_10() { return &___cameraClearFlagsTransparentDisplay_10; }
	inline void set_cameraClearFlagsTransparentDisplay_10(int32_t value)
	{
		___cameraClearFlagsTransparentDisplay_10 = value;
	}

	inline static int32_t get_offset_of_backgroundColorTransparentDisplay_11() { return static_cast<int32_t>(offsetof(MixedRealityCameraProfile_t8D764AB8B73D6B7B38310A8EB0CCAD2A1176E258, ___backgroundColorTransparentDisplay_11)); }
	inline Color_t119BCA590009762C7223FDD3AF9706653AC84ED2  get_backgroundColorTransparentDisplay_11() const { return ___backgroundColorTransparentDisplay_11; }
	inline Color_t119BCA590009762C7223FDD3AF9706653AC84ED2 * get_address_of_backgroundColorTransparentDisplay_11() { return &___backgroundColorTransparentDisplay_11; }
	inline void set_backgroundColorTransparentDisplay_11(Color_t119BCA590009762C7223FDD3AF9706653AC84ED2  value)
	{
		___backgroundColorTransparentDisplay_11 = value;
	}

	inline static int32_t get_offset_of_holoLensQualityLevel_12() { return static_cast<int32_t>(offsetof(MixedRealityCameraProfile_t8D764AB8B73D6B7B38310A8EB0CCAD2A1176E258, ___holoLensQualityLevel_12)); }
	inline int32_t get_holoLensQualityLevel_12() const { return ___holoLensQualityLevel_12; }
	inline int32_t* get_address_of_holoLensQualityLevel_12() { return &___holoLensQualityLevel_12; }
	inline void set_holoLensQualityLevel_12(int32_t value)
	{
		___holoLensQualityLevel_12 = value;
	}
};

#ifdef __clang__
#pragma clang diagnostic pop
#endif
#endif // MIXEDREALITYCAMERAPROFILE_T8D764AB8B73D6B7B38310A8EB0CCAD2A1176E258_H
#ifndef CAMERA_T48B2B9ECB3CE6108A98BF949A1CECF0FE3421F34_H
#define CAMERA_T48B2B9ECB3CE6108A98BF949A1CECF0FE3421F34_H
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// UnityEngine.Camera
struct  Camera_t48B2B9ECB3CE6108A98BF949A1CECF0FE3421F34  : public Behaviour_tBDC7E9C3C898AD8348891B82D3E345801D920CA8
{
public:

public:
};

struct Camera_t48B2B9ECB3CE6108A98BF949A1CECF0FE3421F34_StaticFields
{
public:
	// UnityEngine.Camera_CameraCallback UnityEngine.Camera::onPreCull
	CameraCallback_t8BBB42AA08D7498DFC11F4128117055BC7F0B9D0 * ___onPreCull_4;
	// UnityEngine.Camera_CameraCallback UnityEngine.Camera::onPreRender
	CameraCallback_t8BBB42AA08D7498DFC11F4128117055BC7F0B9D0 * ___onPreRender_5;
	// UnityEngine.Camera_CameraCallback UnityEngine.Camera::onPostRender
	CameraCallback_t8BBB42AA08D7498DFC11F4128117055BC7F0B9D0 * ___onPostRender_6;

public:
	inline static int32_t get_offset_of_onPreCull_4() { return static_cast<int32_t>(offsetof(Camera_t48B2B9ECB3CE6108A98BF949A1CECF0FE3421F34_StaticFields, ___onPreCull_4)); }
	inline CameraCallback_t8BBB42AA08D7498DFC11F4128117055BC7F0B9D0 * get_onPreCull_4() const { return ___onPreCull_4; }
	inline CameraCallback_t8BBB42AA08D7498DFC11F4128117055BC7F0B9D0 ** get_address_of_onPreCull_4() { return &___onPreCull_4; }
	inline void set_onPreCull_4(CameraCallback_t8BBB42AA08D7498DFC11F4128117055BC7F0B9D0 * value)
	{
		___onPreCull_4 = value;
		Il2CppCodeGenWriteBarrier((&___onPreCull_4), value);
	}

	inline static int32_t get_offset_of_onPreRender_5() { return static_cast<int32_t>(offsetof(Camera_t48B2B9ECB3CE6108A98BF949A1CECF0FE3421F34_StaticFields, ___onPreRender_5)); }
	inline CameraCallback_t8BBB42AA08D7498DFC11F4128117055BC7F0B9D0 * get_onPreRender_5() const { return ___onPreRender_5; }
	inline CameraCallback_t8BBB42AA08D7498DFC11F4128117055BC7F0B9D0 ** get_address_of_onPreRender_5() { return &___onPreRender_5; }
	inline void set_onPreRender_5(CameraCallback_t8BBB42AA08D7498DFC11F4128117055BC7F0B9D0 * value)
	{
		___onPreRender_5 = value;
		Il2CppCodeGenWriteBarrier((&___onPreRender_5), value);
	}

	inline static int32_t get_offset_of_onPostRender_6() { return static_cast<int32_t>(offsetof(Camera_t48B2B9ECB3CE6108A98BF949A1CECF0FE3421F34_StaticFields, ___onPostRender_6)); }
	inline CameraCallback_t8BBB42AA08D7498DFC11F4128117055BC7F0B9D0 * get_onPostRender_6() const { return ___onPostRender_6; }
	inline CameraCallback_t8BBB42AA08D7498DFC11F4128117055BC7F0B9D0 ** get_address_of_onPostRender_6() { return &___onPostRender_6; }
	inline void set_onPostRender_6(CameraCallback_t8BBB42AA08D7498DFC11F4128117055BC7F0B9D0 * value)
	{
		___onPostRender_6 = value;
		Il2CppCodeGenWriteBarrier((&___onPostRender_6), value);
	}
};

#ifdef __clang__
#pragma clang diagnostic pop
#endif
#endif // CAMERA_T48B2B9ECB3CE6108A98BF949A1CECF0FE3421F34_H



// System.Void Microsoft.MixedReality.Toolkit.BaseCoreSystem::.ctor(Microsoft.MixedReality.Toolkit.IMixedRealityServiceRegistrar,Microsoft.MixedReality.Toolkit.BaseMixedRealityProfile)
extern "C" IL2CPP_METHOD_ATTR void BaseCoreSystem__ctor_m02E2EEF1017481C5A7F2530877DD9FED02396A13 (BaseCoreSystem_t86E92055CF287B1D86F50C81455BDFA894B12E41 * __this, RuntimeObject* p0, BaseMixedRealityProfile_tB4DC16619B37D298D22571CE017070A78EF826E8 * p1, const RuntimeMethod* method);
// System.Boolean UnityEngine.XR.WSA.HolographicSettings::get_IsDisplayOpaque()
extern "C" IL2CPP_METHOD_ATTR bool HolographicSettings_get_IsDisplayOpaque_m1195AD8CB69591E29DEE780894136EEEA7D7A418 (const RuntimeMethod* method);
// System.Boolean UnityEngine.Object::op_Equality(UnityEngine.Object,UnityEngine.Object)
extern "C" IL2CPP_METHOD_ATTR bool Object_op_Equality_mBC2401774F3BE33E8CF6F0A8148E66C95D6CFF1C (Object_tAE11E5E46CD5C37C9F3E8950C00CD8B45666A2D0 * p0, Object_tAE11E5E46CD5C37C9F3E8950C00CD8B45666A2D0 * p1, const RuntimeMethod* method);
// System.Boolean Microsoft.MixedReality.Toolkit.CameraSystem.MixedRealityCameraSystem::get_IsOpaque()
extern "C" IL2CPP_METHOD_ATTR bool MixedRealityCameraSystem_get_IsOpaque_mC25180F8564AC074C65919C683F0977F2F2C9EBF (MixedRealityCameraSystem_tD88CC45FC5FEB7DE6B6EB7130347FFFC77DA2E1B * __this, const RuntimeMethod* method);
// System.Void Microsoft.MixedReality.Toolkit.CameraSystem.MixedRealityCameraSystem::ApplySettingsForOpaqueDisplay()
extern "C" IL2CPP_METHOD_ATTR void MixedRealityCameraSystem_ApplySettingsForOpaqueDisplay_m546B7975128A350595750C8BF385F1B4AE2846F1 (MixedRealityCameraSystem_tD88CC45FC5FEB7DE6B6EB7130347FFFC77DA2E1B * __this, const RuntimeMethod* method);
// System.Void Microsoft.MixedReality.Toolkit.CameraSystem.MixedRealityCameraSystem::ApplySettingsForTransparentDisplay()
extern "C" IL2CPP_METHOD_ATTR void MixedRealityCameraSystem_ApplySettingsForTransparentDisplay_mE2A6D0EAA660F39D71063E0C144CBBF83D4B30F2 (MixedRealityCameraSystem_tD88CC45FC5FEB7DE6B6EB7130347FFFC77DA2E1B * __this, const RuntimeMethod* method);
// UnityEngine.Camera Microsoft.MixedReality.Toolkit.Utilities.CameraCache::get_Main()
extern "C" IL2CPP_METHOD_ATTR Camera_t48B2B9ECB3CE6108A98BF949A1CECF0FE3421F34 * CameraCache_get_Main_m23FB3162F6476988FEE59F829DEAF08702D81554 (const RuntimeMethod* method);
// Microsoft.MixedReality.Toolkit.MixedRealityCameraProfile Microsoft.MixedReality.Toolkit.CameraSystem.MixedRealityCameraSystem::get_CameraProfile()
extern "C" IL2CPP_METHOD_ATTR MixedRealityCameraProfile_t8D764AB8B73D6B7B38310A8EB0CCAD2A1176E258 * MixedRealityCameraSystem_get_CameraProfile_mA84448436414F49C26F76D56276AE227973C3FDB (MixedRealityCameraSystem_tD88CC45FC5FEB7DE6B6EB7130347FFFC77DA2E1B * __this, const RuntimeMethod* method);
// UnityEngine.CameraClearFlags Microsoft.MixedReality.Toolkit.MixedRealityCameraProfile::get_CameraClearFlagsOpaqueDisplay()
extern "C" IL2CPP_METHOD_ATTR int32_t MixedRealityCameraProfile_get_CameraClearFlagsOpaqueDisplay_m7E4FFDB04F3844CA5FECBBD5E3F4BE58FF54F586 (MixedRealityCameraProfile_t8D764AB8B73D6B7B38310A8EB0CCAD2A1176E258 * __this, const RuntimeMethod* method);
// System.Void UnityEngine.Camera::set_clearFlags(UnityEngine.CameraClearFlags)
extern "C" IL2CPP_METHOD_ATTR void Camera_set_clearFlags_m805DFBD136AA3E1E46A2E61441965D174E87FE50 (Camera_t48B2B9ECB3CE6108A98BF949A1CECF0FE3421F34 * __this, int32_t p0, const RuntimeMethod* method);
// System.Single Microsoft.MixedReality.Toolkit.MixedRealityCameraProfile::get_NearClipPlaneOpaqueDisplay()
extern "C" IL2CPP_METHOD_ATTR float MixedRealityCameraProfile_get_NearClipPlaneOpaqueDisplay_mE5FFDA5B2A6E63F043C3BC5749460E7C9ADE059D (MixedRealityCameraProfile_t8D764AB8B73D6B7B38310A8EB0CCAD2A1176E258 * __this, const RuntimeMethod* method);
// System.Void UnityEngine.Camera::set_nearClipPlane(System.Single)
extern "C" IL2CPP_METHOD_ATTR void Camera_set_nearClipPlane_m9D81E50F8658C16319BEF3774E78B93DEB208C6B (Camera_t48B2B9ECB3CE6108A98BF949A1CECF0FE3421F34 * __this, float p0, const RuntimeMethod* method);
// UnityEngine.Color Microsoft.MixedReality.Toolkit.MixedRealityCameraProfile::get_BackgroundColorOpaqueDisplay()
extern "C" IL2CPP_METHOD_ATTR Color_t119BCA590009762C7223FDD3AF9706653AC84ED2  MixedRealityCameraProfile_get_BackgroundColorOpaqueDisplay_m5BC046DB871960BF763F6FC898C7E247A6BE4AC7 (MixedRealityCameraProfile_t8D764AB8B73D6B7B38310A8EB0CCAD2A1176E258 * __this, const RuntimeMethod* method);
// System.Void UnityEngine.Camera::set_backgroundColor(UnityEngine.Color)
extern "C" IL2CPP_METHOD_ATTR void Camera_set_backgroundColor_mDB9CA1B37FE2D52493823914AC5BC9F8C1935D6F (Camera_t48B2B9ECB3CE6108A98BF949A1CECF0FE3421F34 * __this, Color_t119BCA590009762C7223FDD3AF9706653AC84ED2  p0, const RuntimeMethod* method);
// System.Int32 Microsoft.MixedReality.Toolkit.MixedRealityCameraProfile::get_OpaqueQualityLevel()
extern "C" IL2CPP_METHOD_ATTR int32_t MixedRealityCameraProfile_get_OpaqueQualityLevel_mA6C0F2B6E4FD975D34D728A080D428CC2C94DF74 (MixedRealityCameraProfile_t8D764AB8B73D6B7B38310A8EB0CCAD2A1176E258 * __this, const RuntimeMethod* method);
// System.Void UnityEngine.QualitySettings::SetQualityLevel(System.Int32,System.Boolean)
extern "C" IL2CPP_METHOD_ATTR void QualitySettings_SetQualityLevel_mEE52F0941999C858D6424E363E42C26935166551 (int32_t p0, bool p1, const RuntimeMethod* method);
// UnityEngine.CameraClearFlags Microsoft.MixedReality.Toolkit.MixedRealityCameraProfile::get_CameraClearFlagsTransparentDisplay()
extern "C" IL2CPP_METHOD_ATTR int32_t MixedRealityCameraProfile_get_CameraClearFlagsTransparentDisplay_m922AA67DB2718C351E46724FEB76A8327A89A554 (MixedRealityCameraProfile_t8D764AB8B73D6B7B38310A8EB0CCAD2A1176E258 * __this, const RuntimeMethod* method);
// UnityEngine.Color Microsoft.MixedReality.Toolkit.MixedRealityCameraProfile::get_BackgroundColorTransparentDisplay()
extern "C" IL2CPP_METHOD_ATTR Color_t119BCA590009762C7223FDD3AF9706653AC84ED2  MixedRealityCameraProfile_get_BackgroundColorTransparentDisplay_m9B964BEDCCE76A615B204F093CD4FC608305CEF0 (MixedRealityCameraProfile_t8D764AB8B73D6B7B38310A8EB0CCAD2A1176E258 * __this, const RuntimeMethod* method);
// System.Single Microsoft.MixedReality.Toolkit.MixedRealityCameraProfile::get_NearClipPlaneTransparentDisplay()
extern "C" IL2CPP_METHOD_ATTR float MixedRealityCameraProfile_get_NearClipPlaneTransparentDisplay_m0A32ACF0E48E4B4332F51063128ECB9B3BF455BC (MixedRealityCameraProfile_t8D764AB8B73D6B7B38310A8EB0CCAD2A1176E258 * __this, const RuntimeMethod* method);
// System.Int32 Microsoft.MixedReality.Toolkit.MixedRealityCameraProfile::get_HoloLensQualityLevel()
extern "C" IL2CPP_METHOD_ATTR int32_t MixedRealityCameraProfile_get_HoloLensQualityLevel_mC943993AF44238F7F2B7B221BEBFF6CC81F6617F (MixedRealityCameraProfile_t8D764AB8B73D6B7B38310A8EB0CCAD2A1176E258 * __this, const RuntimeMethod* method);
// System.String Microsoft.MixedReality.Toolkit.CameraSystem.MixedRealityCameraSystem::get_SourceName()
extern "C" IL2CPP_METHOD_ATTR String_t* MixedRealityCameraSystem_get_SourceName_mC85AAC268D053A1F7B078260B6A989D5B7C60485 (MixedRealityCameraSystem_tD88CC45FC5FEB7DE6B6EB7130347FFFC77DA2E1B * __this, const RuntimeMethod* method);
// System.Int32 UnityEngine.Mathf::Abs(System.Int32)
extern "C" IL2CPP_METHOD_ATTR int32_t Mathf_Abs_mC7DD2FB3789B5409055836D0E7D8227AD2099FDC (int32_t p0, const RuntimeMethod* method);
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void Microsoft.MixedReality.Toolkit.CameraSystem.MixedRealityCameraSystem::.ctor(Microsoft.MixedReality.Toolkit.IMixedRealityServiceRegistrar,Microsoft.MixedReality.Toolkit.BaseMixedRealityProfile)
extern "C" IL2CPP_METHOD_ATTR void MixedRealityCameraSystem__ctor_mB323AD4E2CEC744D89F4D53A65E7ACD58E4C00AF (MixedRealityCameraSystem_tD88CC45FC5FEB7DE6B6EB7130347FFFC77DA2E1B * __this, RuntimeObject* ___registrar0, BaseMixedRealityProfile_tB4DC16619B37D298D22571CE017070A78EF826E8 * ___profile1, const RuntimeMethod* method)
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_method (MixedRealityCameraSystem__ctor_mB323AD4E2CEC744D89F4D53A65E7ACD58E4C00AF_MetadataUsageId);
		s_Il2CppMethodInitialized = true;
	}
	{
		// public string SourceName { get; } = "Mixed Reality Camera System";
		__this->set_U3CSourceNameU3Ek__BackingField_10(_stringLiteral44E408FBB792F45EB849669B80782DAB5C91FDE4);
		// BaseMixedRealityProfile profile = null) : base(registrar, profile)
		RuntimeObject* L_0 = ___registrar0;
		BaseMixedRealityProfile_tB4DC16619B37D298D22571CE017070A78EF826E8 * L_1 = ___profile1;
		BaseCoreSystem__ctor_m02E2EEF1017481C5A7F2530877DD9FED02396A13(__this, L_0, L_1, /*hidden argument*/NULL);
		// }
		return;
	}
}
// System.Boolean Microsoft.MixedReality.Toolkit.CameraSystem.MixedRealityCameraSystem::get_IsOpaque()
extern "C" IL2CPP_METHOD_ATTR bool MixedRealityCameraSystem_get_IsOpaque_mC25180F8564AC074C65919C683F0977F2F2C9EBF (MixedRealityCameraSystem_tD88CC45FC5FEB7DE6B6EB7130347FFFC77DA2E1B * __this, const RuntimeMethod* method)
{
	{
		// currentDisplayType = DisplayType.Opaque;
		__this->set_currentDisplayType_12(0);
		// if (!UnityEngine.XR.WSA.HolographicSettings.IsDisplayOpaque)
		bool L_0 = HolographicSettings_get_IsDisplayOpaque_m1195AD8CB69591E29DEE780894136EEEA7D7A418(/*hidden argument*/NULL);
		if (L_0)
		{
			goto IL_0015;
		}
	}
	{
		// currentDisplayType = DisplayType.Transparent;
		__this->set_currentDisplayType_12(1);
	}

IL_0015:
	{
		// return currentDisplayType == DisplayType.Opaque;
		int32_t L_1 = __this->get_currentDisplayType_12();
		return (bool)((((int32_t)L_1) == ((int32_t)0))? 1 : 0);
	}
}
// System.UInt32 Microsoft.MixedReality.Toolkit.CameraSystem.MixedRealityCameraSystem::get_SourceId()
extern "C" IL2CPP_METHOD_ATTR uint32_t MixedRealityCameraSystem_get_SourceId_m3E5D50974AE8939666D07E7AC724D825FBC31DC6 (MixedRealityCameraSystem_tD88CC45FC5FEB7DE6B6EB7130347FFFC77DA2E1B * __this, const RuntimeMethod* method)
{
	{
		// public uint SourceId { get; } = 0;
		uint32_t L_0 = __this->get_U3CSourceIdU3Ek__BackingField_9();
		return L_0;
	}
}
// System.String Microsoft.MixedReality.Toolkit.CameraSystem.MixedRealityCameraSystem::get_SourceName()
extern "C" IL2CPP_METHOD_ATTR String_t* MixedRealityCameraSystem_get_SourceName_mC85AAC268D053A1F7B078260B6A989D5B7C60485 (MixedRealityCameraSystem_tD88CC45FC5FEB7DE6B6EB7130347FFFC77DA2E1B * __this, const RuntimeMethod* method)
{
	{
		// public string SourceName { get; } = "Mixed Reality Camera System";
		String_t* L_0 = __this->get_U3CSourceNameU3Ek__BackingField_10();
		return L_0;
	}
}
// Microsoft.MixedReality.Toolkit.MixedRealityCameraProfile Microsoft.MixedReality.Toolkit.CameraSystem.MixedRealityCameraSystem::get_CameraProfile()
extern "C" IL2CPP_METHOD_ATTR MixedRealityCameraProfile_t8D764AB8B73D6B7B38310A8EB0CCAD2A1176E258 * MixedRealityCameraSystem_get_CameraProfile_mA84448436414F49C26F76D56276AE227973C3FDB (MixedRealityCameraSystem_tD88CC45FC5FEB7DE6B6EB7130347FFFC77DA2E1B * __this, const RuntimeMethod* method)
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_method (MixedRealityCameraSystem_get_CameraProfile_mA84448436414F49C26F76D56276AE227973C3FDB_MetadataUsageId);
		s_Il2CppMethodInitialized = true;
	}
	{
		// if (cameraProfile == null)
		MixedRealityCameraProfile_t8D764AB8B73D6B7B38310A8EB0CCAD2A1176E258 * L_0 = __this->get_cameraProfile_11();
		IL2CPP_RUNTIME_CLASS_INIT(Object_tAE11E5E46CD5C37C9F3E8950C00CD8B45666A2D0_il2cpp_TypeInfo_var);
		bool L_1 = Object_op_Equality_mBC2401774F3BE33E8CF6F0A8148E66C95D6CFF1C(L_0, (Object_tAE11E5E46CD5C37C9F3E8950C00CD8B45666A2D0 *)NULL, /*hidden argument*/NULL);
		if (!L_1)
		{
			goto IL_001f;
		}
	}
	{
		// cameraProfile = ConfigurationProfile as MixedRealityCameraProfile;
		BaseMixedRealityProfile_tB4DC16619B37D298D22571CE017070A78EF826E8 * L_2 = VirtFuncInvoker0< BaseMixedRealityProfile_tB4DC16619B37D298D22571CE017070A78EF826E8 * >::Invoke(19 /* Microsoft.MixedReality.Toolkit.BaseMixedRealityProfile Microsoft.MixedReality.Toolkit.BaseService::get_ConfigurationProfile() */, __this);
		__this->set_cameraProfile_11(((MixedRealityCameraProfile_t8D764AB8B73D6B7B38310A8EB0CCAD2A1176E258 *)IsInstClass((RuntimeObject*)L_2, MixedRealityCameraProfile_t8D764AB8B73D6B7B38310A8EB0CCAD2A1176E258_il2cpp_TypeInfo_var)));
	}

IL_001f:
	{
		// return cameraProfile;
		MixedRealityCameraProfile_t8D764AB8B73D6B7B38310A8EB0CCAD2A1176E258 * L_3 = __this->get_cameraProfile_11();
		return L_3;
	}
}
// System.Void Microsoft.MixedReality.Toolkit.CameraSystem.MixedRealityCameraSystem::Initialize()
extern "C" IL2CPP_METHOD_ATTR void MixedRealityCameraSystem_Initialize_m02AA1A918E7E847EA914DF054D2940052024C5AA (MixedRealityCameraSystem_tD88CC45FC5FEB7DE6B6EB7130347FFFC77DA2E1B * __this, const RuntimeMethod* method)
{
	{
		// cameraOpaqueLastFrame = IsOpaque;
		bool L_0 = MixedRealityCameraSystem_get_IsOpaque_mC25180F8564AC074C65919C683F0977F2F2C9EBF(__this, /*hidden argument*/NULL);
		__this->set_cameraOpaqueLastFrame_13(L_0);
		// if (IsOpaque)
		bool L_1 = MixedRealityCameraSystem_get_IsOpaque_mC25180F8564AC074C65919C683F0977F2F2C9EBF(__this, /*hidden argument*/NULL);
		if (!L_1)
		{
			goto IL_001b;
		}
	}
	{
		// ApplySettingsForOpaqueDisplay();
		MixedRealityCameraSystem_ApplySettingsForOpaqueDisplay_m546B7975128A350595750C8BF385F1B4AE2846F1(__this, /*hidden argument*/NULL);
		// }
		return;
	}

IL_001b:
	{
		// ApplySettingsForTransparentDisplay();
		MixedRealityCameraSystem_ApplySettingsForTransparentDisplay_mE2A6D0EAA660F39D71063E0C144CBBF83D4B30F2(__this, /*hidden argument*/NULL);
		// }
		return;
	}
}
// System.Void Microsoft.MixedReality.Toolkit.CameraSystem.MixedRealityCameraSystem::Update()
extern "C" IL2CPP_METHOD_ATTR void MixedRealityCameraSystem_Update_mFEEB4D7953C6BB3DDD7359D17B5D70C75CBF07C7 (MixedRealityCameraSystem_tD88CC45FC5FEB7DE6B6EB7130347FFFC77DA2E1B * __this, const RuntimeMethod* method)
{
	{
		// if (IsOpaque != cameraOpaqueLastFrame)
		bool L_0 = MixedRealityCameraSystem_get_IsOpaque_mC25180F8564AC074C65919C683F0977F2F2C9EBF(__this, /*hidden argument*/NULL);
		bool L_1 = __this->get_cameraOpaqueLastFrame_13();
		if ((((int32_t)L_0) == ((int32_t)L_1)))
		{
			goto IL_002f;
		}
	}
	{
		// cameraOpaqueLastFrame = IsOpaque;
		bool L_2 = MixedRealityCameraSystem_get_IsOpaque_mC25180F8564AC074C65919C683F0977F2F2C9EBF(__this, /*hidden argument*/NULL);
		__this->set_cameraOpaqueLastFrame_13(L_2);
		// if (IsOpaque)
		bool L_3 = MixedRealityCameraSystem_get_IsOpaque_mC25180F8564AC074C65919C683F0977F2F2C9EBF(__this, /*hidden argument*/NULL);
		if (!L_3)
		{
			goto IL_0029;
		}
	}
	{
		// ApplySettingsForOpaqueDisplay();
		MixedRealityCameraSystem_ApplySettingsForOpaqueDisplay_m546B7975128A350595750C8BF385F1B4AE2846F1(__this, /*hidden argument*/NULL);
		// }
		return;
	}

IL_0029:
	{
		// ApplySettingsForTransparentDisplay();
		MixedRealityCameraSystem_ApplySettingsForTransparentDisplay_mE2A6D0EAA660F39D71063E0C144CBBF83D4B30F2(__this, /*hidden argument*/NULL);
	}

IL_002f:
	{
		// }
		return;
	}
}
// System.Void Microsoft.MixedReality.Toolkit.CameraSystem.MixedRealityCameraSystem::ApplySettingsForOpaqueDisplay()
extern "C" IL2CPP_METHOD_ATTR void MixedRealityCameraSystem_ApplySettingsForOpaqueDisplay_m546B7975128A350595750C8BF385F1B4AE2846F1 (MixedRealityCameraSystem_tD88CC45FC5FEB7DE6B6EB7130347FFFC77DA2E1B * __this, const RuntimeMethod* method)
{
	{
		// CameraCache.Main.clearFlags = CameraProfile.CameraClearFlagsOpaqueDisplay;
		Camera_t48B2B9ECB3CE6108A98BF949A1CECF0FE3421F34 * L_0 = CameraCache_get_Main_m23FB3162F6476988FEE59F829DEAF08702D81554(/*hidden argument*/NULL);
		MixedRealityCameraProfile_t8D764AB8B73D6B7B38310A8EB0CCAD2A1176E258 * L_1 = MixedRealityCameraSystem_get_CameraProfile_mA84448436414F49C26F76D56276AE227973C3FDB(__this, /*hidden argument*/NULL);
		NullCheck(L_1);
		int32_t L_2 = MixedRealityCameraProfile_get_CameraClearFlagsOpaqueDisplay_m7E4FFDB04F3844CA5FECBBD5E3F4BE58FF54F586(L_1, /*hidden argument*/NULL);
		NullCheck(L_0);
		Camera_set_clearFlags_m805DFBD136AA3E1E46A2E61441965D174E87FE50(L_0, L_2, /*hidden argument*/NULL);
		// CameraCache.Main.nearClipPlane = CameraProfile.NearClipPlaneOpaqueDisplay;
		Camera_t48B2B9ECB3CE6108A98BF949A1CECF0FE3421F34 * L_3 = CameraCache_get_Main_m23FB3162F6476988FEE59F829DEAF08702D81554(/*hidden argument*/NULL);
		MixedRealityCameraProfile_t8D764AB8B73D6B7B38310A8EB0CCAD2A1176E258 * L_4 = MixedRealityCameraSystem_get_CameraProfile_mA84448436414F49C26F76D56276AE227973C3FDB(__this, /*hidden argument*/NULL);
		NullCheck(L_4);
		float L_5 = MixedRealityCameraProfile_get_NearClipPlaneOpaqueDisplay_mE5FFDA5B2A6E63F043C3BC5749460E7C9ADE059D(L_4, /*hidden argument*/NULL);
		NullCheck(L_3);
		Camera_set_nearClipPlane_m9D81E50F8658C16319BEF3774E78B93DEB208C6B(L_3, L_5, /*hidden argument*/NULL);
		// CameraCache.Main.backgroundColor = CameraProfile.BackgroundColorOpaqueDisplay;
		Camera_t48B2B9ECB3CE6108A98BF949A1CECF0FE3421F34 * L_6 = CameraCache_get_Main_m23FB3162F6476988FEE59F829DEAF08702D81554(/*hidden argument*/NULL);
		MixedRealityCameraProfile_t8D764AB8B73D6B7B38310A8EB0CCAD2A1176E258 * L_7 = MixedRealityCameraSystem_get_CameraProfile_mA84448436414F49C26F76D56276AE227973C3FDB(__this, /*hidden argument*/NULL);
		NullCheck(L_7);
		Color_t119BCA590009762C7223FDD3AF9706653AC84ED2  L_8 = MixedRealityCameraProfile_get_BackgroundColorOpaqueDisplay_m5BC046DB871960BF763F6FC898C7E247A6BE4AC7(L_7, /*hidden argument*/NULL);
		NullCheck(L_6);
		Camera_set_backgroundColor_mDB9CA1B37FE2D52493823914AC5BC9F8C1935D6F(L_6, L_8, /*hidden argument*/NULL);
		// QualitySettings.SetQualityLevel(CameraProfile.OpaqueQualityLevel, false);
		MixedRealityCameraProfile_t8D764AB8B73D6B7B38310A8EB0CCAD2A1176E258 * L_9 = MixedRealityCameraSystem_get_CameraProfile_mA84448436414F49C26F76D56276AE227973C3FDB(__this, /*hidden argument*/NULL);
		NullCheck(L_9);
		int32_t L_10 = MixedRealityCameraProfile_get_OpaqueQualityLevel_mA6C0F2B6E4FD975D34D728A080D428CC2C94DF74(L_9, /*hidden argument*/NULL);
		QualitySettings_SetQualityLevel_mEE52F0941999C858D6424E363E42C26935166551(L_10, (bool)0, /*hidden argument*/NULL);
		// }
		return;
	}
}
// System.Void Microsoft.MixedReality.Toolkit.CameraSystem.MixedRealityCameraSystem::ApplySettingsForTransparentDisplay()
extern "C" IL2CPP_METHOD_ATTR void MixedRealityCameraSystem_ApplySettingsForTransparentDisplay_mE2A6D0EAA660F39D71063E0C144CBBF83D4B30F2 (MixedRealityCameraSystem_tD88CC45FC5FEB7DE6B6EB7130347FFFC77DA2E1B * __this, const RuntimeMethod* method)
{
	{
		// CameraCache.Main.clearFlags = CameraProfile.CameraClearFlagsTransparentDisplay;
		Camera_t48B2B9ECB3CE6108A98BF949A1CECF0FE3421F34 * L_0 = CameraCache_get_Main_m23FB3162F6476988FEE59F829DEAF08702D81554(/*hidden argument*/NULL);
		MixedRealityCameraProfile_t8D764AB8B73D6B7B38310A8EB0CCAD2A1176E258 * L_1 = MixedRealityCameraSystem_get_CameraProfile_mA84448436414F49C26F76D56276AE227973C3FDB(__this, /*hidden argument*/NULL);
		NullCheck(L_1);
		int32_t L_2 = MixedRealityCameraProfile_get_CameraClearFlagsTransparentDisplay_m922AA67DB2718C351E46724FEB76A8327A89A554(L_1, /*hidden argument*/NULL);
		NullCheck(L_0);
		Camera_set_clearFlags_m805DFBD136AA3E1E46A2E61441965D174E87FE50(L_0, L_2, /*hidden argument*/NULL);
		// CameraCache.Main.backgroundColor = CameraProfile.BackgroundColorTransparentDisplay;
		Camera_t48B2B9ECB3CE6108A98BF949A1CECF0FE3421F34 * L_3 = CameraCache_get_Main_m23FB3162F6476988FEE59F829DEAF08702D81554(/*hidden argument*/NULL);
		MixedRealityCameraProfile_t8D764AB8B73D6B7B38310A8EB0CCAD2A1176E258 * L_4 = MixedRealityCameraSystem_get_CameraProfile_mA84448436414F49C26F76D56276AE227973C3FDB(__this, /*hidden argument*/NULL);
		NullCheck(L_4);
		Color_t119BCA590009762C7223FDD3AF9706653AC84ED2  L_5 = MixedRealityCameraProfile_get_BackgroundColorTransparentDisplay_m9B964BEDCCE76A615B204F093CD4FC608305CEF0(L_4, /*hidden argument*/NULL);
		NullCheck(L_3);
		Camera_set_backgroundColor_mDB9CA1B37FE2D52493823914AC5BC9F8C1935D6F(L_3, L_5, /*hidden argument*/NULL);
		// CameraCache.Main.nearClipPlane = CameraProfile.NearClipPlaneTransparentDisplay;
		Camera_t48B2B9ECB3CE6108A98BF949A1CECF0FE3421F34 * L_6 = CameraCache_get_Main_m23FB3162F6476988FEE59F829DEAF08702D81554(/*hidden argument*/NULL);
		MixedRealityCameraProfile_t8D764AB8B73D6B7B38310A8EB0CCAD2A1176E258 * L_7 = MixedRealityCameraSystem_get_CameraProfile_mA84448436414F49C26F76D56276AE227973C3FDB(__this, /*hidden argument*/NULL);
		NullCheck(L_7);
		float L_8 = MixedRealityCameraProfile_get_NearClipPlaneTransparentDisplay_m0A32ACF0E48E4B4332F51063128ECB9B3BF455BC(L_7, /*hidden argument*/NULL);
		NullCheck(L_6);
		Camera_set_nearClipPlane_m9D81E50F8658C16319BEF3774E78B93DEB208C6B(L_6, L_8, /*hidden argument*/NULL);
		// QualitySettings.SetQualityLevel(CameraProfile.HoloLensQualityLevel, false);
		MixedRealityCameraProfile_t8D764AB8B73D6B7B38310A8EB0CCAD2A1176E258 * L_9 = MixedRealityCameraSystem_get_CameraProfile_mA84448436414F49C26F76D56276AE227973C3FDB(__this, /*hidden argument*/NULL);
		NullCheck(L_9);
		int32_t L_10 = MixedRealityCameraProfile_get_HoloLensQualityLevel_mC943993AF44238F7F2B7B221BEBFF6CC81F6617F(L_9, /*hidden argument*/NULL);
		QualitySettings_SetQualityLevel_mEE52F0941999C858D6424E363E42C26935166551(L_10, (bool)0, /*hidden argument*/NULL);
		// }
		return;
	}
}
// System.Boolean Microsoft.MixedReality.Toolkit.CameraSystem.MixedRealityCameraSystem::System.Collections.IEqualityComparer.Equals(System.Object,System.Object)
extern "C" IL2CPP_METHOD_ATTR bool MixedRealityCameraSystem_System_Collections_IEqualityComparer_Equals_m7959EFD352809556B8E7D3719C49CDC833CB2850 (MixedRealityCameraSystem_tD88CC45FC5FEB7DE6B6EB7130347FFFC77DA2E1B * __this, RuntimeObject * ___x0, RuntimeObject * ___y1, const RuntimeMethod* method)
{
	{
		// return false;
		return (bool)0;
	}
}
// System.Int32 Microsoft.MixedReality.Toolkit.CameraSystem.MixedRealityCameraSystem::System.Collections.IEqualityComparer.GetHashCode(System.Object)
extern "C" IL2CPP_METHOD_ATTR int32_t MixedRealityCameraSystem_System_Collections_IEqualityComparer_GetHashCode_mEC4FD9A6C0DE40D3D6F46B223D83453834FDA43D (MixedRealityCameraSystem_tD88CC45FC5FEB7DE6B6EB7130347FFFC77DA2E1B * __this, RuntimeObject * ___obj0, const RuntimeMethod* method)
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_method (MixedRealityCameraSystem_System_Collections_IEqualityComparer_GetHashCode_mEC4FD9A6C0DE40D3D6F46B223D83453834FDA43D_MetadataUsageId);
		s_Il2CppMethodInitialized = true;
	}
	{
		// return Mathf.Abs(SourceName.GetHashCode());
		String_t* L_0 = MixedRealityCameraSystem_get_SourceName_mC85AAC268D053A1F7B078260B6A989D5B7C60485(__this, /*hidden argument*/NULL);
		NullCheck(L_0);
		int32_t L_1 = VirtFuncInvoker0< int32_t >::Invoke(2 /* System.Int32 System.Object::GetHashCode() */, L_0);
		IL2CPP_RUNTIME_CLASS_INIT(Mathf_tFBDE6467D269BFE410605C7D806FD9991D4A89CB_il2cpp_TypeInfo_var);
		int32_t L_2 = Mathf_Abs_mC7DD2FB3789B5409055836D0E7D8227AD2099FDC(L_1, /*hidden argument*/NULL);
		return L_2;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
