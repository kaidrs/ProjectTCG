using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CustomExtension
{
    public static class ExtensionMethod
    {
        public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> self) => self.Select((item, index) => (item, index));

    }

}
