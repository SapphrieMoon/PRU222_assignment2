using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class TagDTO
    {
        public class CreateTagDTO
        {
            public required string TagName { get; set; }
            public required string Note { get; set; }
        }

        public class UpdateTagDTO
        {
            public string? TagName { get; set; }
            public string? Note { get; set; }
        }

        public class TagResponseDTO
        {
            public int TagId { get; set; }
            public required string TagName { get; set; }
            public required string Note { get; set; }
        }
    }
}
