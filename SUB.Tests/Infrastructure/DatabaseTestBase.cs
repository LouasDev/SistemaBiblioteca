using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SUB.Tests.Infrastructure
{
    [TestClass]
    public abstract class DatabaseTestBase
    {
        protected SqlCeTestDatabase Db { get; private set; }

        [TestInitialize]
        public void BaseInitialize()
        {
            Db = new SqlCeTestDatabase();
            Db.CreateSchema();
            OnSeed(Db);
        }

        protected virtual void OnSeed(SqlCeTestDatabase db)
        {
        }

        [TestCleanup]
        public void BaseCleanup()
        {
            if (Db != null)
            {
                Db.Dispose();
                Db = null;
            }
        }
    }
}