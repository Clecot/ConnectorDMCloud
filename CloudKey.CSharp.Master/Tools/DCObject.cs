//-----------------------------------------------------------------------
// <copyright file="DCObject.cs" company="Netquarks">
//     Netquarks : All rights reserved 2013.
// </copyright>
//-----------------------------------------------------------------------

namespace DMCloud.CSharp.Master.Tools
{
    #region using
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    #endregion

    /// <summary>
    /// DCObject class
    /// </summary>
    public class DCObject : Dictionary<Object, Object>
    {
        /// <summary>
        /// Create an empty dictionary
        /// </summary>
        /// <returns></returns>
        public static  DCObject Create()
        {
            return new DCObject();
        }

        /// <summary>
        /// Initialize the dictionary
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static  DCObject Create(Dictionary<object, object> dic)
        {
            DCObject obj = Create();
            obj = (DCObject)dic;
            return obj;
        }

        /// <summary>
        /// Add the key/value to the dictionary 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public DCObject Push(object key, object value)
        {
            this.Add(key, value);
            return this;
        }

        /// <summary>
        /// Get the value associated to the key
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string Pull(string path)
        {
            if (!path.Contains("."))
            {
                return this[path].ToString();
            }
            string[] tokens = Regex.Split(path, "\\.");
            Dictionary<object, object> dic = (Dictionary<object, object>)this[tokens[0]];
            for (int i = 1; i < tokens.Length; i++)
            {
                if (dic[tokens[i]] is Dictionary<object, object>)
                {
                    dic = (Dictionary<object, object>)dic[tokens[i]];
                }
                else
                {
                    return dic[tokens[i]].ToString();
                }
            }
            return "null";
        }
    }
}
