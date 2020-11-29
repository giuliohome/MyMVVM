using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq.Expressions;

namespace GiulioMVVM
{
 
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        #region Constructor

        protected ViewModelBase()
        {
        }
 
        #endregion // Constructor
 
        #region DisplayName
 
 
        public virtual string DisplayName { get; protected set; }
 
        #endregion // DisplayName
 
        #region Debugging Aides
 
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public void VerifyPropertyName(string propertyName)
        {
            // Verify that the property name matches a real,
            // public, instance property on this object.
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                string msg = "Invalid property name: " + propertyName;

                if (this.ThrowOnInvalidPropertyName)
                    throw new Exception(msg);
                else
                    Debug.Fail(msg);
            }
        }
 
        protected virtual bool ThrowOnInvalidPropertyName { get; private set; }
 
        #endregion // Debugging Aides
 
        #region INotifyPropertyChanged Members

 
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged<X, T>(Expression<Func<X, T>> selectorExpression)
        {
            if (selectorExpression == null)
                throw new ArgumentNullException("selectorExpression");
            MemberExpression body = selectorExpression.Body as MemberExpression;
            if (body == null)
                throw new ArgumentException("The body must be a member expression");
            OnPropertyChanged(body.Member.Name);
        }

        protected virtual void OnPropertyChanged<T>(Expression<Func<T>> selectorExpression)
        {
            if (selectorExpression == null)
                throw new ArgumentNullException("selectorExpression");
            MemberExpression body = selectorExpression.Body as MemberExpression;
            if (body == null)
                throw new ArgumentException("The body must be a member expression");
            OnPropertyChanged(body.Member.Name);
        }
 

        protected virtual void OnPropertyChanged(string propertyName)
        {
            this.VerifyPropertyName(propertyName);

            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }
 
        #endregion // INotifyPropertyChanged Members
 
    }

}