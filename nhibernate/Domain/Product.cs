using System.Collections.Generic;

namespace nhibernateSpike.Domain
{
    public class Product
    {
        public virtual int Id { get; protected set; }
        public virtual string Name { get; set; }
        public virtual IList<Application> Applications { get; set; }

        public Product()
        {
            Applications = new List<Application>();
        }

        public virtual void AddApplication(Application application)
        {
            application.Products.Add(this);
            Applications.Add(application);
        }
    }
}