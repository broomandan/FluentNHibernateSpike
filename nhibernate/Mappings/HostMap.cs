using FluentNHibernate.Mapping;
using nhibernateSpike.Domain;

namespace nhibernateSpike.Mappings
{
    public class HostMap : ClassMap<Host>
    {
        public HostMap()
        {
            Id(x => x.Id);
            Map(x => x.MachineName);
            Map(x => x.ProcessorCount);
            Map(x => x.Is64BitProcess);
            Map(x => x.OsVersion);
            Map(x => x.Is64BitOperatingSystem);
            Map(x => x.ClrVersion);
            Map(x => x.UserName);

            References(x => x.ApplicationInstance)
                .Unique();
        }
    }
}