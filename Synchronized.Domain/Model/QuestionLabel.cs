using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Synchronized.Repository.Model
{
    public class QuestionLabel
    {
        public string QuestionId { get; set; }
        public string LabelId { get; set; }
    }
}