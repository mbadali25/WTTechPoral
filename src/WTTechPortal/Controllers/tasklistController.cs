using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WTTechPortal.Data;
using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;
using WTTechPortal.Models;
using WTTechPortal.Services;
using System;
using Microsoft.AspNetCore.Http;


namespace WTTechPortal.Controllers
{
    public class tasklistController : Controller
    {
        private readonly WttechportalDbContext _context;
      

        public tasklistController(WttechportalDbContext context)
        {
            _context = context;
          
        }

        // GET: tasklist
       public async Task<IActionResult> Index(int? id, int? status, int? owner , int? prioity, bool? closedcheck)
        {
            var priorities = _context.priority_select.OrderBy(c => c.priorityid).Select(x => new { Id = x.priorityid, Value = x.priorityname });
            var statuslist = _context.status_select.OrderBy(c => c.statusid).Select(x => new { Id = x.statusid, Value = x.statusname });
            var ownerlist = _context.owner_select.OrderBy(c => c.ownerid).Select(x => new { Id = x.ownerid, Value = x.ownername });
            ViewBag.priorities = new SelectList(priorities, "Id", "Value");
            ViewBag.statuslist = new SelectList(statuslist, "Id", "Value");
            ViewBag.ownerlist = new SelectList(ownerlist, "Id", "Value");
            string EmailStatus = null;
            
            //Handling Email Status TempData passed from Edit or Create
            if ((TempData["EmailStatus"]) != null)
            {
                EmailStatus = (TempData["EmailStatus"].ToString());
                if (EmailStatus.Length > 0)
                {
                    ViewBag.EmailStatus = EmailStatus;
                }
            }
            else
            {
                ViewBag.EmailStatus = "";
            }
           


                if ( closedcheck != false)
            {
                closedcheck = true;
            }
         
            var list = (from a in _context.tasklist
                        select a );

            list = list.Include(p => p.prioritites).Include(o => o.owners).Include(s => s.statuses).OrderBy( or => or.status);

            if (status != null)
            {
                if (closedcheck == true)
                {
                    list = list.Where(l => l.status.Equals(status)).Where(ch =>!ch.status.Equals(10));
                }
                else
                {
                    list = list.Where(l => l.status.Equals(status));
                }
            }
            else if(closedcheck==true)
            {
                
                list = list.Where(l => !l.status.Equals(10));
                

            }

            if (owner != null)
            {
                list = list.Where(l => l.owner.Equals(owner));
            }

            if (prioity != null)
            {
                list = list.Where(l => l.priority.Equals(prioity));
            }

            return View(await list.ToListAsync());
        }

        //Send Email



        // GET: tasklist/Details/5


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tasklist = await _context.tasklist.SingleOrDefaultAsync(m => m.id == id);
            if (tasklist == null)
            {
                return NotFound();
            }

            return View(tasklist);
        }




        // GET: tasklist/Create

        public IActionResult Create()
        {

            var priorities = _context.priority_select.OrderBy(c => c.priorityid).Select(x => new { Id = x.priorityid, Value = x.priorityname });
            var statuslist = _context.status_select.OrderBy(c => c.statusid).Select(x => new { Id = x.statusid, Value = x.statusname });
            var ownerlist = _context.owner_select.OrderBy(c => c.ownerid).Select(x => new { Id = x.ownerid, Value = x.ownername });
            ViewBag.priorities = new SelectList(priorities, "Id", "Value");
            ViewBag.statuslist = new SelectList(statuslist, "Id", "Value");
            ViewBag.ownerlist = new SelectList(ownerlist, "Id", "Value");
            return View();
            
        }


 

        // POST: tasklist/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,actionitem,comments,completedate,owner,priority,status,task")] tasklist tasklist)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tasklist);
                await _context.SaveChangesAsync();
                //Setup Values from Database Prepare for Email
                string taskitem = tasklist.task.ToString();
                int taskownerint = tasklist.owner;
                string taskowner = _context.owner_select.Where(x => x.ownerid.Equals(taskownerint)).Select(s => s.ownername).First();

                int taskstatusint = tasklist.status;
                string taskstatus = _context.status_select.Where(x => x.statusid.Equals(taskstatusint)).Select(s => s.statusname).First();

                int taskpriorityint = tasklist.priority;
                string taskpriority = _context.priority_select.Where(x => x.priorityid.Equals(taskpriorityint)).Select(s => s.priorityname).First();

                string actionitem = tasklist.actionitem.ToString();
                


                int smtpport = _context.site_config.Select(x => x.smtpport).First();
                string smtpserver = _context.site_config.Select(x => x.smtpserver).First();
                string smtpuser = _context.site_config.Select(x => x.smtpuser).First();
                string smtppass = _context.site_config.Select(x => x.smtppassword).First();
                string fromadd = _context.site_config.Select(x => x.sendasemail).First();
                string adminadd = _context.site_config.Select(x => x.adminemail).First();
                string mailbody = "A New Task was Created with the Following Detaions<br/> <b>Task: </b>" + taskitem +"</br><b>Action Item: </b>" + actionitem + "<br/><b>Owner: </b>" + taskowner + "<br/><b>Status: </b>" + taskstatus + "<br/><b>Priority: </b>" + taskpriority;
                string subject = "Create: A New Task was Created";

                //var EmailConstruct = TaskSendEmail;

                await SendEmail("Create", smtpuser, smtpserver, smtppass, fromadd, adminadd, mailbody, subject, smtpport, true, true);
                TempData["EmailStatus"] = HttpContext.Session.GetString("estatus");

                return RedirectToAction("Index");
            }
            return View(tasklist);
        }

        // GET: tasklist/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tasklist = await _context.tasklist.SingleOrDefaultAsync(m => m.id == id);
            if (tasklist == null)
            {
                return NotFound();
            }
            var priorities = _context.priority_select.OrderBy(c => c.priorityid).Select(x => new { Id = x.priorityid, Value = x.priorityname });
            var statuslist = _context.status_select.OrderBy(c => c.statusid).Select(x => new { Id = x.statusid, Value = x.statusname });
            var ownerlist = _context.owner_select.OrderBy(c => c.ownerid).Select(x => new { Id = x.ownerid, Value = x.ownername });
            ViewBag.priorities = new SelectList(priorities, "Id", "Value");
            ViewBag.statuslist = new SelectList(statuslist, "Id", "Value");
            ViewBag.ownerlist = new SelectList(ownerlist, "Id", "Value");
            return View(tasklist);
        }

        // POST: tasklist/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,actionitem,comments,completedate,owner,priority,status,task")] tasklist tasklist)
        {
            if (id != tasklist.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tasklist);
                    await _context.SaveChangesAsync();
                    string taskitem = _context.tasklist.Where(x => x.id.Equals(id)).Select(s => s.task).First();
                                     
                        
                        

                        //Setup Values from Database Prepare for Email
                        int smtpport = _context.site_config.Select(x => x.smtpport).First();
                        string smtpserver = _context.site_config.Select(x => x.smtpserver).First();
                        string smtpuser = _context.site_config.Select(x => x.smtpuser).First();
                        string smtppass = _context.site_config.Select(x => x.smtppassword).First();
                        string fromadd = _context.site_config.Select(x => x.sendasemail).First();
                        string adminadd = _context.site_config.Select(x => x.adminemail).First();
                        string mailbody = "Task: <b>" + taskitem + "</b>  : was updated on a Edit";
                        string subject = "Edit: Task was updated";

                    //var EmailConstruct = TaskSendEmail;

                     await SendEmail("Edit", smtpuser, smtpserver, smtppass, fromadd, adminadd, mailbody, subject, smtpport, true, true);
                    TempData["EmailStatus"] = HttpContext.Session.GetString("estatus");
                    
                        
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!tasklistExists(tasklist.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }



                return RedirectToAction("Index");
            }

            

            return View(tasklist);
        }

        // GET: tasklist/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tasklist = await _context.tasklist.SingleOrDefaultAsync(m => m.id == id);
            if (tasklist == null)
            {
                return NotFound();
            }


            

            return View(tasklist);
        }

// POST: tasklist/Delete/5
[HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tasklist = await _context.tasklist.SingleOrDefaultAsync(m => m.id == id);
            _context.tasklist.Remove(tasklist);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool tasklistExists(int id)
        {
            return _context.tasklist.Any(e => e.id == id);
        }


        public async Task SendEmail(string action, string smtpuser, string smtpserver, string smtppass, string fromadd, string toadd, string mailbody, string subject, int smtpport, bool smtpauth, bool ssl)
        {
            string emailstatus = "";
            try
            {
                var emailMessage = new MimeMessage();
                

                emailMessage.From.Add(new MailboxAddress(fromadd, fromadd));
                emailMessage.To.Add(new MailboxAddress(toadd, toadd));
                emailMessage.Subject = subject;
                emailMessage.Body = new TextPart("html") { Text = mailbody };

                using (var client = new SmtpClient())
                {
                    if (ssl == true)
                    {
                        await client.ConnectAsync(smtpserver, smtpport, SecureSocketOptions.StartTls).ConfigureAwait(true);
                    }

                    if (ssl == false)
                    {
                        await client.ConnectAsync(smtpserver, smtpport, SecureSocketOptions.None).ConfigureAwait(true);
                    }
                    if (smtpauth == true)
                    {
                        await client.AuthenticateAsync(smtpuser, smtppass).ConfigureAwait(true);

                    }
                    await client.SendAsync(emailMessage).ConfigureAwait(true);
                    await client.DisconnectAsync(true).ConfigureAwait(true);
                }
                emailstatus = (action + ": Successful, Email Notification was sent");
                HttpContext.Session.SetString("estatus",emailstatus);
            }

            catch (Exception ex)
            {
                emailstatus = "Email Failed!: " + ex.ToString();
                HttpContext.Session.SetString("estatus", emailstatus);
                
            }

            
            

        }
    }
}

