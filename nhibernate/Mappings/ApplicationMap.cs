using FluentNHibernate.Mapping;
using FluentNHibernate.Utils;
using nhibernateSpike.Domain;

namespace nhibernateSpike.Mappings
{
    public class ApplicationMap : ClassMap<Application>
    {
        public ApplicationMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Type);

            HasMany(x => x.ApplicationInstances)
                .Inverse()
                .Cascade.All();
            HasManyToMany(x => x.Products)
                .Cascade.All()
                .Inverse()
                .Table("ProductApplication");
        }
    }
}