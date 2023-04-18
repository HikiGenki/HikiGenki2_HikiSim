using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public static class ReflectionAssistant {
   

    public static MethodInfo[] GetMethods(string typeName, bool ignoreVoid = true) {
        return GetMethods(GetType(typeName), ignoreVoid);
    }

    public static MethodInfo[] GetMethods(Type type, bool ignoreVoid = true) {
        MethodInfo[] methods = type.GetMethods();
        List<MethodInfo> methodList = new List<MethodInfo>(methods);
        if (ignoreVoid) {
            methodList.RemoveAll(method => {
                if (method.ReturnType == typeof(void)) {
                    return true;
                }
                return false;
            });
        }
        return methodList.ToArray();
    }

    public static List<string> GetMethodNames(string typeName) {
        MethodInfo[] methods = GetMethods(typeName);
        List<string> names = new List<string>();
        foreach (MethodInfo info in methods) {
            names.Add(info.Name);
        }
        return names;
    }


    public static ValueDropdownList<string> GetMethodDropdown(string type, bool ignoreVoid = true) {
        ValueDropdownList<string> dropdown = new ValueDropdownList<string>();

        MethodInfo[] methods = GetMethods(type, ignoreVoid);
        foreach (MethodInfo method in methods) {
            dropdown.Add(Signature(method), method.Name);
        }
        return dropdown;
    }

    internal static List<ParameterInfo> GetParameters(MethodInfo method) {
        return new List<ParameterInfo>(method.GetParameters());
    }

    public static string MethodSignature(string classAssemblyName, string methodName, string returnType, List<string> parameterTypes) {
        return Signature(FindMethod(classAssemblyName, methodName, returnType, parameterTypes));
    }
    public static string Signature(MemberInfo member) {
        if (member.MemberType == MemberTypes.Method) {
            MethodInfo method = member as MethodInfo;
            return method.ReturnType.Name + " " + method.Name + "(" + GetParameterTypeString(method) + ")";
        } else if (member.MemberType == MemberTypes.Field) {
            FieldInfo field = member as FieldInfo;

            return field.FieldType.Name + " " + member.Name;
        } else if (member.MemberType == MemberTypes.Property) {
            PropertyInfo property = member as PropertyInfo;
            return property.PropertyType.Name + " " + member.Name;
        } else {
            return member.Name;
        }

    }

    public static Type OutputType(MemberInfo member) {
        if (member.MemberType == MemberTypes.Method) {
            MethodInfo method = member as MethodInfo;
            return method.ReturnType;
        } else if (member.MemberType == MemberTypes.Field) {
            FieldInfo field = member as FieldInfo;

            return field.FieldType;
        } else if (member.MemberType == MemberTypes.Property) {
            PropertyInfo property = member as PropertyInfo;
            return property.PropertyType;
        } else {
            return null;
        }

    }
    public static bool IsStatic(MemberInfo member) {
        if (member.MemberType == MemberTypes.Method) {
            MethodInfo method = member as MethodInfo;
            return method.IsStatic;
        } else if (member.MemberType == MemberTypes.Field) {
            FieldInfo field = member as FieldInfo;

            return field.IsStatic;
        } else if (member.MemberType == MemberTypes.Property) {
            PropertyInfo property = member as PropertyInfo;
            Debug.LogWarning("Can't find static instance differentiation of property");
            return false;
        } else {
            return false;
        }

    }


    public static bool HasParameters(this MethodInfo method) {
        return method.GetParameters().Length > 0;
    }

    public static List<string> ParameterTypes(this MethodInfo method) {
        List<string> results = new List<string>();
        ParameterInfo[] parameters = method.GetParameters();
        for (int i = 0; i < parameters.Length; i++) {
            results.Add(parameters[i].ParameterType.AssemblyQualifiedName);
        }
        return results;
    }

    public static List<string> ParameterNames(this MethodInfo method) {
        List<string> results = new List<string>();
        ParameterInfo[] parameters = method.GetParameters();
        for (int i = 0; i < parameters.Length; i++) {
            results.Add(parameters[i].Name);
        }
        return results;
    }

    public static List<object> DefaultParameters(List<string> types) {
        List<Type> typesAsTypes = GetTypes(types);

        return DefaultParameters(typesAsTypes);
    }

    public static List<object> DefaultParameters(List<Type> types) {
        List<object> results = new List<object>();

        foreach (Type type in types) {
            if (type.IsValueType) {
                results.Add(InstanceOf(type));
            } else {
                results.Add(null);
            }
        }

        return results;
    }

    public static object InstanceOf(Type type) {
        Dictionary<Type, object> instances = new Dictionary<Type, object>(){
            {typeof(byte), (byte)0 },
            {typeof(short), (short)0 },
            {typeof(int), (int)0 },
            {typeof(long), (long)0 },
            {typeof(float), (float)0 },
            {typeof(double), (double)0 },
            {typeof(string), "" },
            {typeof(char), ' ' },
            {typeof(bool), false }

        };
        if (instances.ContainsKey(type)) {
            return instances[type];
        } else {
            return null;
        }
    }

    public static string GetParameterTypeString(MethodInfo method) {
        string paramString = "";
        ParameterInfo[] parameters = method.GetParameters();
        for (int i = 0; i < parameters.Length; i++) {
            ParameterInfo parameter = parameters[i];
            Type parameterType = parameter.ParameterType;
            string name = parameterType.Name;
            if (parameterType.IsGenericType) {
                name = parameterType.GetGenericTypeDefinition().Name;
                name += GetGenericString(parameterType);
            }
            paramString += name;
            if (i < parameters.Length - 1) {
                paramString += ", ";
            }
        }
        return paramString;
    }

    public static string GetGenericString(Type type) {
        Type[] generics = type.GetGenericArguments();
        string s = "";
        if (generics.Length > 0) {
            s += "<";
            for (int j = 0; j < generics.Length; j++) {
                Type genType = generics[j];
                s += genType.Name;
                if (j < generics.Length - 1) {
                    s += ", ";
                }
            }
            s += ">";
        } else {
            s = "< >";
        }
        return s;
    }

    public static Type GetType(string name) {
        Type type = Type.GetType(name);

        return type;
    }

    public static List<Type> GetTypes(List<string> types) {

        List<Type> results = new List<Type>();
        types.ForEach(type => results.Add(GetType(type)));
        return results;
    }

    public static MemberInfo FindMember(MemberTypes type, string declaringType, string memberName, string returnType = null, List<string> parameterTypes = null) {
        if (type == MemberTypes.Method) {
            return ReflectionAssistant.FindMethod(declaringType, memberName, returnType, parameterTypes);
        } else if (type == MemberTypes.Field) {
            return ReflectionAssistant.FindField(declaringType, memberName);
        } else if (type == MemberTypes.Property) {
            return ReflectionAssistant.FindProperty(declaringType, memberName);
        } else if (type == MemberTypes.Constructor) {
            return ReflectionAssistant.FindConstructor(declaringType, parameterTypes);
        }
        Debug.LogError("Member was of an unimplemented type " + type.ToString());
        return null;
    }

    public static ConstructorInfo FindConstructor(string declaringType, List<string> parameterTypes) {
        Type type = GetType(declaringType);
        List<ConstructorInfo> constructors = new List<ConstructorInfo>(type.GetConstructors());
        List<Type> types = GetTypes(parameterTypes);
        constructors.RemoveAll(constructor => !CorrectParameterTypes(constructor, types));
        if (constructors.Count == 0) {
            Debug.LogError("No valid constructor found for type " + type.FullName + " using parameters " + parameterTypes.AsString(", "));
            return null;
        } else if (constructors.Count > 1) {
            Debug.LogError("Too many valid constructors for type " + type.FullName + " using parameters " + parameterTypes.AsString(", "));
        }
        return constructors[0];
    }

    public static PropertyInfo FindProperty(string declaringType, string fieldName) {
        Type type = GetType(declaringType);
        List<PropertyInfo> properties = new List<PropertyInfo>(type.GetProperties());
        properties.RemoveAll(field => field.Name != fieldName);
        if (properties.Count == 0) {
            Debug.LogError("No valid property named " + fieldName + " was found for type " + type.FullName);
            return null;
        } else if (properties.Count > 1) {
            Debug.LogError("Too many valid property found for " + fieldName + " of type " + type.FullName);
        }
        return properties[0];
    }

    public static void SetFieldValue(FieldInfo field, object value, object obj) {
        field.SetMemberValue(obj, value);
    }

    public static FieldInfo FindField(Type declaringType, string fieldName) {
        Type type = declaringType;
        List<FieldInfo> fields = new List<FieldInfo>(type.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance));
        fields.RemoveAll(field => field.Name != fieldName);
        if (fields.Count == 0) {
            Debug.LogError("No valid field named " + fieldName + " was found for type " + type.FullName);
            return null;
        } else if (fields.Count > 1) {
            Debug.LogError("Too many valid fields found for " + fieldName + " of type " + type.FullName);
        }
        return fields[0];
    }

    public static FieldInfo FindField(string declaringType, string fieldName) {
        Type type = GetType(declaringType);
        return FindField(type, fieldName);

    }
    public static MethodInfo FindMethod(string typeName, string methodName, string returnTypeName, List<string> parameterTypeNames) {

        if (string.IsNullOrEmpty(typeName)) {
            Debug.LogError("Type Name (" + typeName + ") was null or empty");
            return null;
        } else if (string.IsNullOrEmpty(methodName)) {
            Debug.LogError("Method Name (" + methodName + ") was null or empty");
            return null;
        } else if (string.IsNullOrEmpty(returnTypeName)) {
            Debug.LogError("Return Type Name (" + returnTypeName + ") was null or empty");
            return null;
        }


        Type type = GetType(typeName);
        Type returnType = GetType(returnTypeName);
        List<Type> parameterTypes = GetTypes(parameterTypeNames);


        MethodInfo method = FindMethod(type, methodName, returnType, parameterTypes);
        return method;
    }

    public static bool HasMethod(string typeName, string methodName, string returnTypeName, List<string> parameterTypeNames) {
        return FindMethod(typeName, methodName, returnTypeName, parameterTypeNames) != null;
    }
    public static MethodInfo FindMethod(Type type, string methodName, Type returnType, List<Type> parameterTypes) {
        List<MethodInfo> methods = new List<MethodInfo>(type.GetMethods());
        methods.RemoveAll(method => method.ReturnType != returnType || method.Name != methodName || !CorrectParameterTypes(method, parameterTypes));
        if (methods.Count == 0) {
            //Debug.LogError("No valid method (" + methodName + ") was found for type " + type.FullName);
            return null;
        } else if (methods.Count > 1) {
            Debug.LogError("Too many valid methods were found for (" + methodName + ") of type " + type.FullName);
        }
        return methods[0];
    }



    static bool CorrectParameterTypes(ConstructorInfo constructor, List<Type> parameterTypes) {
        ParameterInfo[] parameters = constructor.GetParameters();
        return CorrectParameterTypes(parameters, parameterTypes);
    }

    static bool CorrectParameterTypes(MethodInfo method, List<Type> parameterTypes) {
        ParameterInfo[] parameters = method.GetParameters();
        return CorrectParameterTypes(parameters, parameterTypes);
    }

    static bool CorrectParameterTypes(ParameterInfo[] parameters, List<Type> parameterTypes) {
        if (parameters.Length != parameterTypes.Count) {
            return false;
        }
        for (int i = 0; i < parameters.Length; i++) {
            if (parameters[i].ParameterType != parameterTypes[i]) {
                return false;
            }
        }
        return true;
    }

    public static MethodInfo GetMethod(Type type, string methodName) {
        return type.GetMethod(methodName);
    }

    public static MethodInfo GetMethod(string typeName, string methodName) {
        return GetMethod(GetType(typeName), methodName);
    }

    public static List<string> TypesToFullNames(List<Type> types) {
        List<string> names = new List<string>();
        types.ForEach(type => names.Add(type.FullName));
        return names;
    }

    //Used to whitelist assemblies, missing Settings
    /*public static List<Assembly> GetSafeAssemblies() {
        AppDomain domain = AppDomain.CurrentDomain;
        List<Assembly> results = new List<Assembly>();
        results.AddRange(domain.GetAssemblies());
        results.RemoveAll(assembly => Settings.ReflectionBlockedAssemblies.Contains(assembly.GetName().FullName));

        return results;
    }*/
    /*
     * Gets all types, needs to know which assemblies to use
    public static List<Type> GetAllTypes() {
        AppDomain domain = AppDomain.CurrentDomain;
        List<Type> results = new List<Type>();

        List<Assembly> assemblies = GetSafeAssemblies();
        foreach (Assembly assembly in assemblies) {
            Type[] types = assembly.GetTypes();


            results.AddRange(types);

        }

        return results;
    }*/


    public static List<Type> GetAllDerivedTypes(Type baseType) {
        AppDomain domain = AppDomain.CurrentDomain;
        List<Type> results = new List<Type>();
        Assembly[] assemblies = domain.GetAssemblies();
        foreach (Assembly assembly in assemblies) {
            Type[] types = assembly.GetTypes();
            foreach (Type type in types) {
                if (type.IsSubclassOf(baseType))
                    results.Add(type);
            }
        }
        return results;
    }
    public static List<Type> GetAllDerivedTypes<T>() {
        return GetAllDerivedTypes(typeof(T));
    }
    public static List<Type> GetTypesWithInterface(Type interfaceType) {
        AppDomain domain = AppDomain.CurrentDomain;
        List<Type> result = new List<Type>();
        Assembly[] assemblies = domain.GetAssemblies();
        foreach (Assembly assembly in assemblies) {
            Type[] types = assembly.GetTypes();
            foreach (Type type in types) {
                if (interfaceType.IsAssignableFrom(type)) {
                    result.Add(type);
                }
            }
        }
        return result;
    }
    public static List<Type> GetTypesWithInterface<T>() {
        return GetTypesWithInterface(typeof(T));
    }

    public static bool IsSerializable(object obj) {

        Type serializeType = typeof(SerializableAttribute);
        Type objectType = obj.GetType();
        SerializableAttribute[] atts = (SerializableAttribute[])objectType.GetCustomAttributes(serializeType, false);
        return atts.Length > 0;
    }
    public static List<MemberInfo> GetMembersWithAttribute<T>(object obj) where T : Attribute {
        List<MemberInfo> members = new List<MemberInfo>();

        Type attributeType = typeof(T);

        Type objectType = obj.GetType();

        List<FieldInfo> fields = new List<FieldInfo>(objectType.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance));

        //Check if fields have the attribute
        foreach (FieldInfo field in fields) {
            T attribute = field.GetCustomAttribute<T>();
            if (attribute != null) {
                members.Add(field);
            }
        }


        return members;
    }
/*
    [System.Serializable]
    public class SerializableMethodSignature {

        public UnityEngine.GameObject target;

        public string typeName;
        public string methodName;
        public string returnTypeName;
        public List<string> parameterTypeNames;

        public MethodInfo ToMethodInfo() {
            return ReflectionAssistant.FindMethod(typeName, methodName, returnTypeName, parameterTypeNames);
        }
        private SerializableMethodSignature() {}
        public static SerializableMethodSignature Create(MethodInfo method) {
            SerializableMethodSignature sig = new SerializableMethodSignature();
            sig.typeName = method.DeclaringType.FullName;
            sig.methodName = method.Name;
            sig.returnTypeName = method.ReturnType.FullName;
            sig.parameterTypeNames = new List<string>();
            foreach(ParameterInfo param in ReflectionAssistant.GetParameters(method)) {
                sig.parameterTypeNames.Add(param.ParameterType.FullName);
            }

            return sig;
        }
    }*/
}