using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            HasOne(x => x.Host)
                .Cascade
                .All();

            References(x => x.Application);
        }
    }
}