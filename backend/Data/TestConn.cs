namespace temalabor_2021.Data
{
    public static class TestConn
    {
        public static string SqlConnectionString
        {
            //get { return @"data source=GERGOLAPTOP\SQLEXPRESS;initial catalog=temalab;integrated security=SSPI"; }
            get { return @"Server=localhost\SQLEXPRESS;Database=temalab;Trusted_Connection=True;"; }
        }
    }
}
