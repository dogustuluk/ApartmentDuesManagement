using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class DDL:IEntity
    {
        public string Text { get; set; }
        public string Value { get; set; }

        //public DDL(string text, string value)
        //{
        //    Text = text;
        //    Value = value;
        //}
    }
}
