using System.Collections.Generic;

namespace nhibernateSpike.Domain
{
    public class Application
    {
        public virtual int Id { get; protected set; }
        public virtual string Name { get; set; }
        public virtual ApplicationType Type { get; set; }
        public virtual IList<Product> Products { get; set; }
        public virtual IList<ApplicationInstance> ApplicationInstances { get; set; }

        public Application()
        {
            ApplicationInstances = new List<ApplicationInstance>();
            Products = new List<Product>();
        }

        public virtual void AddApplicationInstances(ApplicationInstance instance)
        {
            instance.Application = this;
            ApplicationInstances.Add(instance);
        }
    }

    public enum ApplicationType
    {
        Web = 1,
        WinForm = 2,
        Service = 3
    }
}