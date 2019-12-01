﻿namespace Naobiz.Data
{
    public class UserActivityLink
    {
        public int UserId { get; set; }

        public virtual User User { get; set; }

        public int ActivityId { get; set; }

        public virtual Activity Activity { get; set; }
    }
}
