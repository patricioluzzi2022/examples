using Microsoft.EntityFrameworkCore;

namespace Library.Optionals
{
    public class HellowButterfly
    {
        private readonly List<string> aGeniz = new List<string>();
        private readonly DbContext dbContext;

        public HellowButterfly(DbContext _dbContext)
        {
            dbContext = _dbContext;
            aGeniz.Add("HelloButterfly");
        }

        public List<string> GetTypesOfMutations()
        {
            return aGeniz;
        }
    }
}
