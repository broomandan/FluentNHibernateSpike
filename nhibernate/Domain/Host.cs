using System.Collections.Generic;

namespace nhibernateSpike.Domain
{
    public class Host
    {
        public virtual int Id { get; protected set; }
        public virtual string MachineName { get; set; }
        public virtual int ProcessorCount { get; set; }
        public virtual bool Is64BitProcess { get; set; }
        public virtual string OsVersion { get; set; }
        public virtual string Platform { get; set; }
        public virtual bool Is64BitOperatingSystem { get; set; }
        public virtual string ClrVersion { get; set; }
        public virtual string UserName { get; set; }
        public virtual IList<ApplicationInstance> ApplicationInstances { get; protected set; }

        public Host()
        {
            ApplicationInstances = new List<ApplicationInstance>();

        }
        public virtual void AddApplicationInstances(ApplicationInstance instance)
        {
            instance.Host = this;
            ApplicationInstances.Add(instance);
        }
    }
}