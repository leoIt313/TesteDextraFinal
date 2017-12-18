using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TesteDextra.MVC.Models
{
    public class ExecutionResult
    {
        public string Mensagem { get; set; }
        public int HttpStatusCode { get; set; }
        public object Object { get; set; }

        public ExecutionResult(int code, string msg, object obj = null)
        {
            HttpStatusCode = code;
            Mensagem = msg;
            Object = obj;
        }
    }
}
