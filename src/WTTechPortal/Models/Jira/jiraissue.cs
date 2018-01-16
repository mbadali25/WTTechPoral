using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WTTechPortal.Models.Jira
{
    [Table("jiraissue")]
    public class jiraissue
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Assignee")]
        public string ASSIGNEE { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Ticket Number")]
        public int issuenum { get; set; }


        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Project")]
        public int PROJECT { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Ticket Type")]
        public int issuetype { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Ticket Status")]
        public int issuestatus { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Summary")]
        public string SUMMARY { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Description")]
        public string DESCRIPTION { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Date")]

        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DUEDATE { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Resolution")]
        public int? RESOLUTION { get; set; }

        public decimal? TIMEORIGINALESTIMATE { get; set; }


        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Original Estimate")]
        public decimal? TIMEORIGINALESTIMATEhr
        {
            get
            {
                return TIMEORIGINALESTIMATE / 3600;
            }
        }


        public decimal? TIMESPENT { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Time Spent")]
        public decimal? TIMESPENThr
        {
            get
            {
                return TIMESPENT / 3600;
            }

        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Resolution Date")]
        public DateTime? RESOLUTIONDATE { get; set; }


        [ForeignKey("ID")]
        public customfieldvalue customfieldvalues { get; set; }

        [ForeignKey("PROJECT")]
        public project projects { get; set; }


        public string projectname
        {
            get
            {
                return projects.pname;
            }
        }

        public string properticketid
        {
            get
            {
                return string.Concat(projects.pkey, "-", issuenum);
            }
        }


        [ForeignKey("issuestatus")]
        public issuestatus issusestatusname { get; set; }

        public string issuestatusn
        {
            get
            {
                return issusestatusname.pname;
            }
        }

        [ForeignKey("RESOLUTION")]
        public resolution resolutions { get; set; }

        public string resolutionname
        {
            get
            {
                return resolutions.pname;
            }
        }

        [ForeignKey("ID")]
        public worklog worklogs { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Work Logs")]
        public string worklogboydmodded
        {
            get
            {
                return string.Concat(worklogs.CREATED, ": ", worklogs.worklogbody);

            }
        }


    }
}

