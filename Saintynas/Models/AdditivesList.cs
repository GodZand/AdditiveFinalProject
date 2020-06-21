using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Saintynas.Models
{

    /// <summary>
    /// Represents Additive.
    /// </summary>
    public class AdditivesList
    {
        /// <summary>
        /// Additive name
        /// </summary>
        public string Name;
        public void SetName(string tempname)
        {
            Name = tempname;
        }
        public string GetName()
        {
            return Name;
        }

    }
}