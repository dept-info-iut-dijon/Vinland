using Microsoft.EntityFrameworkCore;

namespace VinlandServ
{
    public class Context : DbContext
    {
        public Context()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            string con = "User Id =IQ_BD_GRITTI; Password =GRITTI0000; Data Source =srv-iq-ora:1521/orclpdb.iut21.u-bourgogne.fr";
            optionsBuilder.UseOracle(con);
        }

        public DbSet<Joueur> Joueurs { get; set; }
        public DbSet<MJ> MJs { get; set; }
        public DbSet<Campagne> Campagnes { get; set; }
        public DbSet<Personnage> Personnages { get; set; }
        public DbSet<Carte> Cartes { get; set; }

    }
}
