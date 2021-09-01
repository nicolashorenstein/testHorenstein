﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Results
{
    public class Result
    {
        public string message_id { get; set; }
    }

    public class FirebaseResult
    {
        public long multicast_id { get; set; }
        public int success { get; set; }
        public int failure { get; set; }
        public int canonical_ids { get; set; }
        public List<Result> results { get; set; }
    }
}
