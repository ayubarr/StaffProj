using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffProj.ValidationHelper
{
    /// <summary>
    /// Object Validation.
    /// </summary>
    /// <typeparam name="T"> Data Type. </typeparam>
    public class ObjectValidator<T>
    {
        /// <summary>
        /// Checks if the object is not null.
        /// </summary>
        /// <param name="modelObject">The object to be checked.</param>
        /// <exception cref="ArgumentNullException">Thrown when the object is null.</exception>
        public static void CheckIsNotNullObject(T modelObject)
        {
            if (modelObject == null)
            {
                throw new ArgumentNullException(nameof(modelObject), "The object must not be Null");
            }
        }
    }
}
