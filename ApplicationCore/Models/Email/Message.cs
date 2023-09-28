using ApplicationCore.Models.Email;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models.Email
{
    public class Message
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public Message(IEnumerable<string> to, string subject, string content) 
        { 
            To = new List<MailboxAddress>();
            //To.AddRange(to.Select(x => new MailboxAddress("email",x)));
            To.AddRange(to.Select(x => MailboxAddress.Parse(x)));
            Subject = subject;
            Content = content;
        }
    }
}

public class Message<T> : Message where T : BaseEmailTemplateModel
{
    public Message(T templateModel)
        : base(new string[] { templateModel.To },
               templateModel.GetSubject(),
               templateModel.GetBody())
    {
    }
}
