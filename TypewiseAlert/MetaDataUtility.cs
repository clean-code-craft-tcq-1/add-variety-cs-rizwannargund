using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace TypewiseAlert
{
    public class MetaDataUtility : IMetaDataUtilisation
    {
        private Assembly _assembly;
        private Type[] _types;
        public MetaDataUtility(string nameSpace)
        {
            if(nameSpace == "")
                throw new Exception("namespace parameter cannot be empty");

            _assembly = Assembly.GetExecutingAssembly();
            _types = _assembly.GetTypes().Where(type => type.Namespace == nameSpace).ToArray();
        }

        private Type[] GetTypeNameContiainInterfaces(Type interfaces)
        => _types.Where(type => type.GetInterfaces().Contains(interfaces)).ToArray();

        public object CreateInstanceFromInterfaceAndAttribute(Type interfaceType, string attribute)
        {
            Type[] types = GetTypeNameContiainInterfaces(interfaceType);
            Type type = types.FirstOrDefault(t => t.GetCustomAttribute<CustomAttribute>().Name == attribute);
            object obj = null;
            if (type != null)
                obj = Activator.CreateInstance(type);
            return obj;
        }

        public List<object> CreateInstanceFromInterface(Type interfaceType, object[,] parameters)
        {
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
