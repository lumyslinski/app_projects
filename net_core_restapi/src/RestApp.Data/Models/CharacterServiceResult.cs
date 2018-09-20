using System;
using System.Collections.Generic;
using System.Text;

namespace RestApp.Data.Models
{
    public class CharacterServiceResult
    {
        /// <summary>
        /// Error message
        /// </summary>
        public string Error { get; set; }
        /// <summary>
        /// Result status, it could be a new generated id from db or 1 for ok. Otherwise it is -1
        /// </summary>
        public int ResultStatus { get; set; }

        public CharacterServiceResult()
        {
            this.ResultStatus = -1; // error code status by default
        }
    }
}
