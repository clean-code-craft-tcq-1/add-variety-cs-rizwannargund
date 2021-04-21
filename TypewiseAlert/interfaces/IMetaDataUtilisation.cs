using System;
using System.Collections.Generic;
using System.Text;

namespace TypewiseAlert
{
    public interface IMetaDataUtilisation
    {
        object CreateInstanceFromInterfaceAndAttribute(Type interfaceType, string attribute);
        List<object> CreateInstanceFromInterface(Type interfaceType, object[,] parameters);
    }
}
