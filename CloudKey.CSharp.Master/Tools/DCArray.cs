//-----------------------------------------------------------------------
// <copyright file="DCArray.cs" company="Netquarks">
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
    using System.Collections;
    #endregion

    /// <summary>
    /// DCArray class
    /// </summary>
    public class DCArray : ArrayList
    {
        static public DCArray Create()
        {
            return new DCArray();
        }

        public DCArray Push(Object s)
        {
            this.Add(s);
            return this;
        }
    }
}
