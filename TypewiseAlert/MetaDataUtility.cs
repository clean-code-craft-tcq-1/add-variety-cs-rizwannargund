using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace TypewiseAlert
{
    public class MetaDataUtility
    {
        private static Assembly _assembly;
        private static Type[] _types;
        private static Assembly LoadAssembly(string name)
        => _assembly = Assembly.Load(name);

        private static Type[] GetTypes(string nameSpace)
        => _types = _assembly.GetTypes().Where(type => type.Namespace == nameSpace).ToArray();

        private static Type[] GetTypeNameContiainInterfaces(Type interfaces)
        => _types.Where(type => type.GetInterfaces().Contains(interfaces)).ToArray();

        public static object CreateInstanceWithInterfaceAndAttribute(string assemblyName, string nameSpace, Type interfaceType, string attribute)
        {
            LoadAssembly(assemblyName);
            GetTypes(nameSpace);
            Type[] types = GetTypeNameContiainInterfaces(interfaceType);
            types = types.OrderBy(t => t.GetCustomAttribute<CustomAttribute>().value).ToArray();
            Type type = types.FirstOrDefault(t => t.GetCustomAttribute<CustomAttribute>().Name == attribute);

            object obj = null;
            if(type != null)
                obj = Activator.CreateInstance(type);
            return obj;
        }

        public static List<object> CreateInstanceFromInterface(string assemblyName, string nameSpace, Type interfaceType, object[,] parameters)
        {
            LoadAssembly(assemblyName);
            GetTypes(nameSpace);
            Type[] types = GetTypeNameContiainInterfaces(interfaceType);
            types = types.OrderBy(t => t.GetCustomAttribute<CustomAttribute>().value).ToArray();

            List<object> instanceList = new List<object>();

            for (int i = 0; i < types.Length; i++)
            {
                instanceList.Add(Activator.CreateInstance(types[i], parameters[i, 0]));
            }
            return instanceList;
        }
    }
}
