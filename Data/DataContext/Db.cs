namespace Data.DataContext
{
    public static class Db
    {

        public static FinancifyContext Create()
        {
            try
            {
                FinancifyContext db = new FinancifyContext();
                return db;
            }
            catch { return null; }
        }
    }
}
