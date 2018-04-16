using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WTTechPortal.Models.Jira
{
    [Table("project")]
    public class project
    {
        [Key]
        public int ID { get; set; }

        public string pid
        {
            get
            {
                return ID.ToString();
            }
        }

        public string pname { get; set; }

        public string pkey { get; set; }
    }
}
