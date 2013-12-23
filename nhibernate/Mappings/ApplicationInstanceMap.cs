using FluentNHibernate.Mapping;
using nhibernateSpike.Domain;

namespace nhibernateSpike.Mappings
{
    public class ApplicationInstanceMap : ClassMap<ApplicationInstance>
    {
        public ApplicationInstanceMap()
        {
            Id(x => x.Id);
            Map(x => x.Version);
            Map(x => x.UpdatDate);
            Map(x => x.UpdateStatus);
            Map(x => x.UpdateNotes);

            References(x => x.Host)
                .Cascade.SaveUpdate();
            References(x => x.Application);
        }
    }
}