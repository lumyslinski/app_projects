using System;

namespace ImageApp.Data.Models
{
    public abstract class ImageServiceResultBase<T>
    {
        /// <summary>
        /// Result data
        /// </summary>
        public T Data { get; private set; }
        /// <summary>
        /// Error message
        /// </summary>
        public string Error { get; private set; }
        /// <summary>
        /// Id of item
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        /// Result status, it could be a new generated id from db or 1 for ok. Otherwise it is -1
        /// </summary>
        public bool IsOk => Id > 0;

        public ImageServiceResultBase()
        {
            this.Id = -1; // error code status by default
        }

        public void SetError(Exception exp)
        {
            this.Error = exp.Message + "\n" + exp.StackTrace;
        }

        public void SetId(int id)
        {
            this.Id = id;
        }

        public virtual void SetData(T data)
        {
            this.Data = data;
        }
    }
}
