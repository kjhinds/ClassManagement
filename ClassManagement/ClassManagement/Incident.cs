using System;

namespace ClassManagement
{
    public class Incident : IComparable<Incident>
    {

        public DateTime Date {
            get;
            set;
        }

        public string Behavior {
            get;
            set;
        }

        public string Intervention {
            get;
            set;
        }

        public string Comment {
            get;
            set;
        }

        public Incident ()
        {
            Date = DateTime.Now;
            Behavior = "";
            Intervention = "";
            Comment = "";
        }

        public Incident (DateTime Date, String Behavior, String Intervention, String Comment)
        {
            this.Date = Date;
            this.Behavior = Behavior;
            this.Intervention = Intervention;
            this.Comment = Comment;
        }

        public int CompareTo (Incident other)
        {
            return DateTime.Compare (Date, other.Date);
        }

    }
}

