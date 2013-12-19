using System;

namespace nhibernateSpike.Domain
{
    public class Host
    {
        public virtual int Id { get; protected set; }
        public virtual string MachineName { get; set; }
        public virtual int ProcessorCount { get; set; }
        public virtual bool Is64BitProcess { get; set; }
        public virtual OperatingSystem OsVersion { get; set; }
        public virtual bool Is64BitOperatingSystem { get; set; }
        public virtual string ClrVersion { get; set; }
        public virtual string UserName { get; set; }
        public virtual ApplicationInstance ApplicationInstance { get; set; }


    }
}