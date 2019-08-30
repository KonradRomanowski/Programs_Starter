using Programs_Starter.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Programs_Starter.Models
{
    public class Option<T> : BaseOption
    {     
        #region Properties        
        /// <summary>
        /// Option value
        /// </summary>
        public T Value { get; private set; }
        #endregion

        #region Constructor
        public Option(string _name, T _value)
        {
            ValidateAndSetName(_name);
            Value = _value;
        }
        #endregion

        #region PublicMethods
        /// <summary>
        /// Set the option value
        /// </summary>
        /// <param name="_value"></param>
        public void SetValue(T _value)
        {
            Value = _value;
        }
        
        #endregion

        #region PrivateMethods
        /// <summary>
        /// Validates if option name is ok and sets the name
        /// </summary>
        /// <param name="name"></param>
        private void ValidateAndSetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new Exception("Name of option cannot be null, empty or white space!");
            }

            if (name.Any(Char.IsWhiteSpace))
            {
                throw new Exception("Name of option cannot contain any white spaces!");
            }

            Name = name;
        }    
        #endregion
    }    
}
