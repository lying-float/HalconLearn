using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
//using CENJE_Vison.Models;

namespace CENJE_Vison.Common
{
    class OpenWindowMessage:NotificationMessage
    {
        //public FormulaModel FM { get; private set; }
        public OpenWindowMessage(string notification) : base(notification)
        {

        }

        public OpenWindowMessage(object sender, string notification) : base(sender, notification)
        {

        }

        public OpenWindowMessage(object sender, object target, string notification) : base(sender, target, notification)
        {

        }


        
    }
}
