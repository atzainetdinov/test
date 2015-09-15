namespace MVCBlog.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class MessageContext : DbContext
    {
        // Your context has been configured to use a 'Message' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'MVCBlog.Models.Message' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Message' 
        // connection string in the application configuration file.
        public MessageContext()
          //  : base("name=Message")
            : base("DefaultConnection")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<MessageEntity> MessageEntities { get; set; }
    }

    public class MessageEntity
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Login { get; set; }
        public string Message { get; set; }
    }
}