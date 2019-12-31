namespace WebApplication5.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class FileContext : DbContext
    {
        // Your context has been configured to use a 'FileContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'WebApplication5.Models.FileContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'FileContext' 
        // connection string in the application configuration file.
        public FileContext()
            : base("name=FileContext")
        {
        }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<FileDetails> FileDetails  { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<Test> Tests { get; set; }
        public virtual DbSet<Video> Videos { get; set; }
        public virtual DbSet<TestResult> TestResults { get; set; }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}