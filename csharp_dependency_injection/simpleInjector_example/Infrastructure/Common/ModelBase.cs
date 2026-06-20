using System;
using System.ComponentModel;

/*
 * Use of IEditableObject when class used with BindingSource - https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.ieditableobject
 * fills the gap on CancelEdit and restores the original class property values.
 * thanks Dan Douglas for this piece of code https://wp.me/puPy2-3X (Add Reusable (Inheritable) Commit/Rollback)
 */
namespace posokanei.Infrastructure.Common
{
    [Serializable]
    public abstract class ModelBase : IEditableObject
    {
        protected ModelBase m_oOldVersion;
        bool m_bInEditMode;

        //public IList ParentList;

        //public ModelBase Parent;

        /// <summary>
        /// Is the current instance of the business object a new instance that has not been committed to the database.
        /// This is usually implemented by checking the value of an identifier property that would relate to a primary key in the database
        /// </summary>
        /// <returns></returns>
        //public abstract bool IsNew();

        public void BeginEdit()
        {
            if (!m_bInEditMode)
            {
                m_bInEditMode = true;
                if (m_oOldVersion == null)
                {
                    m_oOldVersion = (ModelBase)this.MemberwiseClone();
                }
            }
        }

        public void CancelEdit()
        {
            ModelBase o = this;
            //if (o.IsNew())
            //{
            //    ParentList.Remove(this);
            //}
            //else
            {
                RevertObject();
            }
            m_bInEditMode = false;
        }

        private void RevertObject()
        {
            if (m_oOldVersion == null)
                return;

            Type t = this.GetType();
            foreach (var p in t.GetProperties())
            {
                if (p.CanWrite && p.CanRead)
                {
                    object oValue = p.GetValue(m_oOldVersion, null);
                    p.SetValue(this, oValue, null);
                }
            }
        }

        public void EndEdit()
        {
            m_bInEditMode = false;
            m_oOldVersion = null;
        }
    }
}