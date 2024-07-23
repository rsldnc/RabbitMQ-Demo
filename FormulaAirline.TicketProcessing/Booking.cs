using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaAirline.TicketProcessing
{
    public class Booking
    {
        public int Id { get; set; }
        public string PassengerName { get; set; }
        public string PassportNumber { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public int PassengerStatus { get; set; }
    }
}
