using FluentNHibernate.Mapping;
using nhibernateSpike.Domain;

namespace nhibernateSpike.Mappings
{
    public class ProductMap : ClassMap<Product>
    {
        public ProductMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);

            HasManyToMany(x => x.Applications)
                .Cascade.All()
                .Table("ProductApplication");
        }
    }
}