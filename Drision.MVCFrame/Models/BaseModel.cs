using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Drision.MVCFrame.Models
{
    public class BaseModel
    {
        public BaseModel()
        {
            CreateTime = DateTime.Now;
        }
        [Key]
        public int ID { get; set; }
        public DateTime CreateTime { get; set; }
        public int CreateUserId { get; set; }
        public DateTime? UpdateTime { get; set; }
        public int UpdateUserId { get; set; }
        public int OwnerId { get; set; }
    }
}