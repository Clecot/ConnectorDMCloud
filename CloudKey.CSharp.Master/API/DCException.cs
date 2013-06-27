//-----------------------------------------------------------------------
// <copyright file="DCException.cs" company="Netquarks">
//     Netquarks : All rights reserved 2013.
// </copyright>
//-----------------------------------------------------------------------

namespace DMCloud.CSharp.Master.API
{
    #region using
    using System;
    #endregion

    /// <summary>
    /// Exception managment class
    /// </summary>
    public class DCException : Exception
    {
        int code = 0;

        public DCException(string message)
            : base(message)
        { }
        /// <summary>
        /// Get the error code
        /// </summary>
        /// <returns></returns>
        public int GetCode()
        {
            return this.code;
        }
    }
}
