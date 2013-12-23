using System;

namespace nhibernateSpike.Domain
{
    public class ApplicationInstance
    {
        public virtual int Id { get; protected set; }
        public virtual string Version { get; set; }
        public virtual DateTime UpdatDate { get; set; }
        public virtual bool UpdateStatus { get; set; }
        public virtual string UpdateNotes { get; set; }
        public virtual Application Application { get; set; }
        public virtual Host Host { get; set; }
    }
}