using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Recipe.Payloads
{
    public class Error
    {
        public string Title { get; set; }
        public string Status { get; set; }

        public Error()
        {

        }

        public Error(string title, string status)
        {
            this.Title = title;
            this.Status = status;
        }
    }
}
