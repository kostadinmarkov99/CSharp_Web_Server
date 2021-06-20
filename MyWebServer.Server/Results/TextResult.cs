﻿using MyWebServer.Server.Common;
using MyWebServer.Server.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebServer.Server.Results
{
    public class TextResult : ContentResult
    { 
        public TextResult(HttpResponse response, string text) : base(response,text, HttpContentType.PlainText)
        {
        }
    }
}
