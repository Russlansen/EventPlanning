using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace EventPlanning.Models
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext() : base("AppDb") { }

        static AppDbContext()
        {
            Database.SetInitializer<AppDbContext>(new AppDbInit());
        }

        public static AppDbContext Create()
        {
            return new AppDbContext();
        }
    }

    internal class AppDbInit : DropCreateDatabaseIfModelChanges<AppDbContext>
    {
        protected override void Seed(AppDbContext context)
        {
            AppUserManager userMgr = new AppUserManager(new UserStore<User>(context));
            string userName = "Admin";
            string password = "123456";

            var user = userMgr.FindByName(userName);
            if (user == null)
            {
                userMgr.Create(new User { UserName = userName }, password);
                user = userMgr.FindByName(userName);
            }
            context.SaveChanges();
            base.Seed(context);
        }
    }
}